// Copyright (c) 2026 OopsItsACoder

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UJect.Injection;

namespace UJect.Tests.InjectionTests
{
    [TestFixture]
    public class InjectionTests
    {
        private class InjectableFieldsType
        {
            [Inject]
            public IInterface field1;

            [Inject("A")]
            public IInterface field2;

            public IInterface field3;

            public InjectableFieldsType(IInterface param1) { }

            public InjectableFieldsType([Inject("A")] IInterface param1, [Inject("B")] IInterface param2) { }
        }
        
        [Test]
        public void TestInjectorContainsAllInjectableFields()
        {
            var injector = new Injector(typeof(InjectableFieldsType));
            Assert.AreEqual(2, injector.InjectableFields.Count, "Injector should recognize 2 fields!");

            var orderedFields = injector.InjectableFields.OrderBy(injField => injField.FieldInfo.Name).ToList();
            
            Assert.AreEqual(typeof(IInterface), orderedFields[0].InjectionKey.InjectedResourceType, "Field InjectionKey resource type should match");
            Assert.AreEqual("field1", orderedFields[0].FieldInfo.Name);
            
            Assert.AreEqual(typeof(IInterface), orderedFields[1].InjectionKey.InjectedResourceType, "Field InjectionKey resource type should match");
            Assert.AreEqual("field2", orderedFields[1].FieldInfo.Name);
            
            Assert.AreEqual("A", orderedFields[1].InjectionKey.InjectedResourceName, "Constructor param 1 name should match");

        }
        
        [Test]
        public void TestInjectorContainsAllInjectableConstructors()
        {
            var injector = new Injector(typeof(InjectableFieldsType));
            Assert.AreEqual(1, injector.InjectableConstructors.Count, "Injector should recognize 1 constructor!");

            var injectableConstructor = injector.InjectableConstructors.First();
            
            Assert.AreEqual(2, injectableConstructor.ParamKeys.Length, "Constructor should have two params");

            Assert.AreEqual(typeof(IInterface), injectableConstructor.ParamKeys[0].InjectedResourceType, "Constructor param 0 type should match!");
            Assert.AreEqual("A", injectableConstructor.ParamKeys[0].InjectedResourceName, "Constructor param 0 name should match");
            
            Assert.AreEqual(typeof(IInterface), injectableConstructor.ParamKeys[1].InjectedResourceType, "Constructor param 1 type should match!");
            Assert.AreEqual("B", injectableConstructor.ParamKeys[1].InjectedResourceName, "Constructor param 1 name should match");
        }

        [Test]
        public void TestInjectorCache()
        {
            InjectorCache.ClearCache();
            
            //Should reuse the first instance of an injector
            var instance = InjectorCache.GetOrCreateInjector(typeof(IInterface));
            Assert.IsNotNull(instance, "Injector instance should not be null");
            Assert.AreEqual(1, InjectorCache.CachedInjectorCount);
            var secondInstance = InjectorCache.GetOrCreateInjector(typeof(IInterface));
            Assert.AreEqual(1, InjectorCache.CachedInjectorCount);
            Assert.IsTrue(ReferenceEquals(instance, secondInstance), "Should reuse injector instance!");
            
            //Clear should reset to zero
            InjectorCache.ClearCache();
            Assert.AreEqual(0, InjectorCache.CachedInjectorCount);
        }

        [Test]
        public void TestFieldsOnBaseClasses()
        {
            InjectorCache.ClearCache();
            
            //Should reuse the first instance of an injector
            var instance = InjectorCache.GetOrCreateInjector(typeof(ChildClass));
            Assert.IsNotNull(instance, "Injector instance should not be null");
            Assert.AreEqual(3, InjectorCache.CachedInjectorCount);
            Assert.That(instance.InjectableFields.Count, Is.EqualTo(3));
            var expectedDependsOn = new HashSet<InjectionKey>()
            {
                new InjectionKey(typeof(IInterface1)),
                new InjectionKey(typeof(IInterface2)),
                new InjectionKey(typeof(IInterface3)),
            };
            Assert.That(instance.DependsOn, Is.EquivalentTo(expectedDependsOn));
            
            Assert.That(InjectorCache.TryGetInjector(typeof(BaseClass1WithInjectableFields), out _), Is.True);
            Assert.That(InjectorCache.TryGetInjector(typeof(BaseClass2WithNoInjectableFields), out _), Is.False, "Should not have an Injector for BaseClass2");
            Assert.That(InjectorCache.TryGetInjector(typeof(BaseClass3WithInjectableFields), out _), Is.True);
            Assert.That(InjectorCache.TryGetInjector(typeof(ChildClass), out _), Is.True);

            void AssertContainsFieldNamed(string fieldName)
            {
                var fields = instance.InjectableFields;
                var fieldNames = fields.Select(f => f.FieldInfo.Name);
                var hasField = fieldNames.Any(candidateName => candidateName == fieldName);
                Assert.That(hasField, Is.True, $"Expected to find field '{fieldName}' but none found in [{string.Join(", ", fieldNames)}]");
            }

            AssertContainsFieldNamed("field1");
            AssertContainsFieldNamed("field2");
            AssertContainsFieldNamed("<Property1>k__BackingField");
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
        
        private interface IInterface
        {
            
        }
        
    }
}
