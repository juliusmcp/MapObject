using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using MapObject.core;
namespace MapObject.Test
{
    [TestClass]
    public class SimpleMapping
    {
        [TestMethod]
        public void TestSimpleMapping()
        {
            Mapper mapobject = new Mapper();
            TestDBObjectSimple simple = GetTestDBObjectSimple();
            TestDTOObjectSimple mapped = mapobject.MapFrom<TestDBObjectSimple>(simple).MapTo<TestDTOObjectSimple>();
            Assert.IsNotNull(mapped);
            Assert.AreEqual(simple.Name, mapped.Name);
            Assert.AreEqual(simple.Id, mapped.Id);
            Assert.AreEqual(simple.Longer, mapped.Longer);
            Assert.AreEqual(simple.Count, mapped.Count);
            Assert.AreEqual(simple.Floater, mapped.Floater);
            Assert.AreEqual(simple.Valid, mapped.Valid);
            Assert.AreEqual(simple.understated, mapped.Understated);
        }
        [TestMethod]
        public void TestSimpleMappingNullables()
        {
            Mapper mapobject = new Mapper();
            TestDBObjectSimple simple = GetTestDBObjectSimple();
            TestDTOObjectSimple mapped = mapobject.MapFrom<TestDBObjectSimple>(simple).MapTo<TestDTOObjectSimple>();
            Assert.IsNotNull(mapped);
            Assert.AreEqual(simple.Name, mapped.Name);
            Assert.AreEqual(simple.Id, mapped.Id);
            Assert.AreEqual(simple.Longer, mapped.Longer);
            Assert.AreEqual(simple.Count, mapped.Count);
            Assert.AreEqual(simple.Floater, mapped.Floater);
            Assert.AreEqual(simple.Valid, mapped.Valid);
            Assert.AreEqual(simple.understated, mapped.Understated);
            Assert.AreEqual(simple.Canbenull, mapped.Canbenull);
            Assert.AreEqual(simple.CanbenullDate, mapped.CanbenullDate);
            
        }
        [TestMethod]
        public void TestSimpleMappingAltSignature()
        {
            Mapper mapobject = new Mapper();
            TestDBObjectSimple simple = GetTestDBObjectSimple();
            TestDTOObjectSimple mapped = mapobject.MapFrom<TestDBObjectSimple>(simple).MapTo<TestDTOObjectSimple>();
            Assert.IsNotNull(mapped);
            Assert.AreEqual(simple.Name, mapped.Name);
            Assert.AreEqual(simple.Id, mapped.Id);
            Assert.AreEqual(simple.Longer, mapped.Longer);
            Assert.AreEqual(simple.Count, mapped.Count);
            Assert.AreEqual(simple.Floater, mapped.Floater);
            Assert.AreEqual(simple.Valid, mapped.Valid);
            Assert.AreEqual(simple.understated, mapped.Understated);
        }
        [TestMethod]
        public void TestSimpleMappingWithMappingBindings()
        {
            Mapper mapobject = new Mapper();
            TestDBObjectSimple simple = GetTestDBObjectSimple();
            Dictionary<string, string> mappings = new Dictionary<string, string>();
            mappings.Add("Name", "Description");
            mappings.Add("Description", "understated");
            mappings.Add("Ok", "Valid");
            TestDTOObjectSimple mapped = mapobject.MapFrom<TestDBObjectSimple>(simple).WithMappings(mappings).MapTo<TestDTOObjectSimple>();
         
            Assert.IsNotNull(mapped);
            

            Assert.AreEqual(simple.Id, mapped.Id);
            Assert.AreEqual(simple.Longer, mapped.Longer);
            Assert.AreEqual(simple.Count, mapped.Count);
            Assert.AreEqual(simple.Floater, mapped.Floater);
            Assert.AreEqual(simple.Ok, mapped.Valid);
            Assert.AreEqual(simple.Description, mapped.Understated);
        }
        [TestMethod]
        public void TestSimpleMappingWithMappingBindingsWithConstructor()
        {
            Mapper mapobject = new Mapper();
            TestDBObjectSimple simple = GetTestDBObjectSimple();
            Dictionary<string, string> mappings = new Dictionary<string, string>();
            mappings.Add("Name", "Description");
            mappings.Add("Description", "understated");
            mappings.Add("Ok", "Valid");
            List<string> con = new List<string>();
            con.Add("EXTRA");

            TestClassObjectSimple mapped = mapobject.MapFrom<TestDBObjectSimple>(simple).WithMappings(mappings).MapTo<TestClassObjectSimple>(con.ToArray());

            Assert.IsNotNull(mapped);


            Assert.AreEqual(simple.Id, mapped.Id);
            Assert.AreEqual(simple.Longer, mapped.Longer);
            Assert.AreEqual(simple.Count, mapped.Count);
            Assert.AreEqual(simple.Floater, mapped.Floater);
            Assert.AreEqual(simple.Ok, mapped.Valid);
            Assert.AreEqual(simple.Description, mapped.Understated);
            Assert.AreEqual("EXTRA", mapped.Extra);
        }

        [TestMethod]
        public void TestSimpleMappingWithConstructor()
        {
            Mapper mapobject = new Mapper();
            TestDBObjectSimple simple = GetTestDBObjectSimple();
         
            List<string> con = new List<string>();
            con.Add("EXTRA");

            TestClassObjectSimple mapped = mapobject.MapFrom<TestDBObjectSimple>(simple).MapTo<TestClassObjectSimple>(con.ToArray());


            Assert.IsNotNull(mapped);
            Assert.AreEqual(simple.Name, mapped.Name);

            Assert.AreEqual(simple.Id, mapped.Id);
            Assert.AreEqual(simple.Longer, mapped.Longer);
            Assert.AreEqual(simple.Count, mapped.Count);
            Assert.AreEqual(simple.Floater, mapped.Floater);
            Assert.AreEqual(simple.Ok, mapped.ok);
            Assert.AreEqual("EXTRA", mapped.Extra);
        }


        [TestMethod]
        public void TestSimpleMappingWithDictionaryObject()
        {
            Mapper mapobject = new Mapper();
            Guid id = Guid.NewGuid();
            Dictionary<string, object> from = new Dictionary<string, object>();
            from.Add("Name", "name");
            from.Add("Id", id);
            from.Add("Longer", 123123);
            from.Add("Valid", true);
             TestDTOObjectSimple mapped = mapobject.MapFrom(from).MapTo<TestDTOObjectSimple>();
            //TestDTOObjectSimple mapped = mapobject.MapTo<TestDTOObjectSimple>(from);
            Assert.IsNotNull(mapped);
            Assert.AreEqual("name", mapped.Name);
            Assert.AreEqual(id, mapped.Id);
            Assert.AreEqual(123123, mapped.Longer);
            Assert.AreEqual(true, mapped.Valid);

        }
        [TestMethod]
        public void TestSimpleMappingWithDictionaryAndWithMappingBindingsWithConstructor()
        {
            Mapper mapobject = new Mapper();
            Guid id = Guid.NewGuid();
            Dictionary<string, string> mappings = new Dictionary<string, string>();
            mappings.Add("Name", "Description");
            mappings.Add("Ok", "Valid");
            Dictionary<string, object> from = new Dictionary<string, object>();
            from.Add("Name", "name");
            from.Add("Id", id);
            from.Add("Longer", 123123);
            from.Add("Ok", true);
            List<string> con = new List<string>();
            con.Add("EXTRA");

            TestClassObjectSimple mapped = mapobject.MapFrom(from).WithMappings(mappings).MapTo<TestClassObjectSimple>(con.ToArray());
           

            Assert.AreEqual("name", mapped.Description);
            Assert.AreEqual(id, mapped.Id);
            Assert.AreEqual(123123, mapped.Longer);
            Assert.AreEqual(true, mapped.Valid);
            Assert.AreEqual("EXTRA", mapped.Extra);
        }
        [TestMethod]
        public void TestMapToShortcut()
        {
            Mapper mapobject = new Mapper();
            TestDBObjectSimple simple = GetTestDBObjectSimple();
            TestDTOObjectSimple mapped = mapobject.MapTo<TestDTOObjectSimple>(simple);
            Assert.IsNotNull(mapped);
            Assert.AreEqual(simple.Name, mapped.Name);
            Assert.AreEqual(simple.Id, mapped.Id);
            Assert.AreEqual(simple.Longer, mapped.Longer);
            Assert.AreEqual(simple.Count, mapped.Count);
            Assert.AreEqual(simple.Floater, mapped.Floater);
            Assert.AreEqual(simple.Valid, mapped.Valid);
            Assert.AreEqual(simple.understated, mapped.Understated);
        }

        [TestMethod]
        public void TestMapToWithExistingObject()
        {
            Mapper mapobject = new Mapper();
            TestDBObjectSimple simple = GetTestDBObjectSimple();
            TestDTOObjectSimple existing = new Test.TestDTOObjectSimple();
            existing.Name = "Existing";
            TestDTOObjectSimple mapped = mapobject.MapFrom<TestDBObjectSimple>(simple).MapTo<TestDTOObjectSimple>(existing);
            Assert.IsNotNull(mapped);
            Assert.AreEqual("Existing", mapped.Name);
            Assert.AreEqual(simple.Id, mapped.Id);
            Assert.AreEqual(simple.Longer, mapped.Longer);
            Assert.AreEqual(simple.Count, mapped.Count);
            Assert.AreEqual(simple.Floater, mapped.Floater);
            Assert.AreEqual(simple.Valid, mapped.Valid);
            Assert.AreEqual(simple.understated, mapped.Understated);
        }
        private TestDBObjectSimple GetTestDBObjectSimple()
        {
            TestDBObjectSimple simple = new TestDBObjectSimple();
            simple.Count = 1;
            simple.Description = "full description.";
            simple.Floater = 2.3F;
            simple.Id = Guid.NewGuid();
            simple.Longer = 8989232;
            simple.Name = "aname";
            simple.Ok = true;
            simple.TestDate = DateTime.Today.Date;
            simple.understated = "underestimated";
            simple.Valid = false;
       
            return simple;
        }
    }
}
