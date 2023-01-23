using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReflectionHelpers.Tests.ExampleModels;
using BufTools.DI.ReflectionHelpers;

namespace ReflectionHelpers.Tests
{
    [TestClass]
    public class RegistrationTests
    {
        private readonly Assembly _assembly;

        public RegistrationTests()
        {
            _assembly = typeof(RegistrationTests).Assembly;
        }

        [TestMethod]
        public void AddScopedClasses_WithValidBaseClassType_RegistersClasses()
        {
            var sc = new ServiceCollection();
            
            sc.AddScopedClasses<BaseClass>(_assembly);
            var provider = sc.BuildServiceProvider();

            Assert.IsFalse(CanMakeInstance<PlainClass>(provider));
            Assert.IsTrue(CanMakeInstance<BaseClass>(provider));
            Assert.IsTrue(CanMakeInstance<SuperClassOne>(provider));
            Assert.IsTrue(CanMakeInstance<SuperClassTwo>(provider));
        }

        [TestMethod]
        public void AddScopedClasses_WithValidSuperClassType_RegistersJustSuperClass()
        {
            var sc = new ServiceCollection();

            sc.AddScopedClasses<SuperClassOne>(_assembly);
            var provider = sc.BuildServiceProvider();

            Assert.IsFalse(CanMakeInstance<PlainClass>(provider));
            Assert.IsFalse(CanMakeInstance<BaseClass>(provider));
            Assert.IsTrue(CanMakeInstance<SuperClassOne>(provider));
            Assert.IsFalse(CanMakeInstance<SuperClassTwo>(provider));
        }

        [TestMethod]
        public void AddScopedClasses_WithAbstractClassType_RegistersRegistersSuperClasses()
        {
            var sc = new ServiceCollection();

            sc.AddScopedClasses<AbstractBaseClass>(_assembly);
            var provider = sc.BuildServiceProvider();

            Assert.IsFalse(CanMakeInstance<PlainClass>(provider));
            Assert.IsTrue(CanMakeInstance<BaseClass>(provider));
            Assert.IsTrue(CanMakeInstance<SuperClassOne>(provider));
            Assert.IsTrue(CanMakeInstance<SuperClassTwo>(provider));
        }

        [TestMethod]
        public void AddScopedClasses_WithInterfaceType_RegistersClasses()
        {
            var sc = new ServiceCollection();

            sc.AddScopedClasses<IInterface>(_assembly);
            var provider = sc.BuildServiceProvider();

            Assert.IsFalse(CanMakeInstance<PlainClass>(provider));
            Assert.IsTrue(CanMakeInstance<BaseClass>(provider));
            Assert.IsTrue(CanMakeInstance<SuperClassOne>(provider));
            Assert.IsTrue(CanMakeInstance<SuperClassTwo>(provider));
        }

        [TestMethod]
        public void AddScopedClasses_WithNonInheritedClassType_RegistersClass()
        {
            var sc = new ServiceCollection();

            sc.AddScopedClasses<PlainClass>(_assembly);
            var provider = sc.BuildServiceProvider();

            Assert.IsTrue(CanMakeInstance<PlainClass>(provider));
            Assert.IsFalse(CanMakeInstance<BaseClass>(provider));
            Assert.IsFalse(CanMakeInstance<SuperClassOne>(provider));
            Assert.IsFalse(CanMakeInstance<SuperClassTwo>(provider));
        }

        private bool CanMakeInstance<TClass>(IServiceProvider provider)
            where TClass : class
        {
            return provider.GetService<TClass>() != null;
        }
    }
}