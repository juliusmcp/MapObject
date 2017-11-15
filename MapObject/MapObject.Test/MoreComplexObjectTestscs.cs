using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MapObject.core;
namespace MapObject.Test
{
    [TestClass]
    public class MoreComplexObjectTestscs
    {
        [TestMethod]
        public void TestMappingWithSubClass()
        {
            
            Mapper mapobject = new Mapper();
            TestClassObjectComplex simple = GetTestDBObjectComplex();
            TestDTOObjectcomplex mapped = mapobject.MapFrom<TestClassObjectComplex>(simple).MapTo<TestDTOObjectcomplex>();
            Assert.IsNotNull(mapped);
            Assert.AreEqual(simple.Name, mapped.Name);
            Assert.AreEqual(simple.Id, mapped.Id);
            Assert.AreEqual(simple.Longer, mapped.Longer);
            Assert.AreEqual(simple.Count, mapped.Count);
            Assert.AreEqual(simple.Floater, mapped.Floater);
            Assert.AreEqual(simple.Valid, mapped.Valid);
            Assert.AreEqual(simple.Understated, mapped.Understated);
            Assert.AreEqual(simple.SubClass.Name, mapped.SubClass.Name);
        }

        private TestClassObjectComplex GetTestDBObjectComplex()
        {
            TestClassObjectComplex simple = new TestClassObjectComplex();
            simple.Count = 1;
            simple.Description = "full description.";
            simple.Floater = 2.3F;
            simple.Id = Guid.NewGuid();
            simple.Longer = 8989232;
            simple.Name = "aname";
            simple.ok = true;
            simple.TestDate = DateTime.Today.Date;
            simple.Understated= "underestimated";
            simple.Valid = false;
            simple.SubClass = new TestSubClass();
            simple.SubClass.Name = "subway";
            simple.SubClass.Time = DateTime.Now;
            return simple;
        }
    }
}
