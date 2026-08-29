using NUnit.Framework;
using UJect.Injection;

namespace UJect.Tests
{
    [TestFixture]
    public class DiContainerFieldInjectionTests
    {
        [Test]
        public void TestInjectInto()
        {
            var diContainer = new DiContainer();
            diContainer.Bind<IInterface1>().ToNewInstance<Impl1>();
            diContainer.Bind<IInterface2>().ToNewInstance<Impl2>();
            diContainer.Bind<IInterface3>().ToNewInstance<Impl3>();

            var childInstance = new ChildClass();
            diContainer.InjectInto(childInstance);

            Assert.That(childInstance.GetInjected1(), Is.Not.Null);
            Assert.That(childInstance.GetInjected1().GetType(), Is.EqualTo(typeof(Impl1)));
            Assert.That(childInstance.GetInjected2(), Is.Not.Null);
            Assert.That(childInstance.GetInjected2().GetType(), Is.EqualTo(typeof(Impl2)));
            Assert.That(childInstance.GetInjected3(), Is.Not.Null);
            Assert.That(childInstance.GetInjected3().GetType(), Is.EqualTo(typeof(Impl3)));
        }


        private abstract class BaseClass1WithInjectableFields
        {
            [Inject] protected IInterface1 field1;
        }

        private abstract class BaseClass2WithNoInjectableFields : BaseClass1WithInjectableFields
        {
        }

        private abstract class BaseClass3WithInjectableFields : BaseClass2WithNoInjectableFields
        {
            [field: Inject] protected IInterface2 Property1 { get; private set; }
        }

        private class ChildClass : BaseClass3WithInjectableFields
        {
            [Inject] protected IInterface3 field2;

            public IInterface1 GetInjected1() => field1;
            public IInterface2 GetInjected2() => Property1;
            public IInterface3 GetInjected3() => field2;
        }

        private interface IInterface1
        {
        }

        private class Impl1 : IInterface1
        {
        }

        private interface IInterface2
        {
        }

        private class Impl2 : IInterface2
        {
        }

        private interface IInterface3
        {
        }

        private class Impl3 : IInterface3
        {
        }
    }
}