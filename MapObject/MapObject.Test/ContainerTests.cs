using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
namespace MapObject.Test
{
    [TestClass]
    public class ContainerTests
    {
        [TestMethod]
        public void TestBasicContainer()
        {
      
            Dictionary<string, string> mappings = new Dictionary<string, string>();
            mappings.Add("Name", "Description");
            mappings.Add("Description", "understated");
            mappings.Add("Ok", "Valid");
            List<string> con = new List<string>();
            con.Add("EXTRA");
            MappingsContainerBuilder.RegisterMapping<TestClassObjectSimple>(mappings, "", con.ToArray());
            var container=MappingsContainerBuilder.Build();
            var simple = GetTestDBObjectSimple();
            TestClassObjectSimple mapped =container.Map<TestClassObjectSimple>(simple);

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
        public void TestBasicContainerNameMappings()
        {

            Dictionary<string, string> mappings = new Dictionary<string, string>();
            mappings.Add("Name", "Description");
            mappings.Add("Description", "understated");
            mappings.Add("Ok", "Valid");
            List<string> con = new List<string>();
            con.Add("EXTRA");
            MappingsContainerBuilder.RegisterMapping<TestClassObjectSimple>(mappings, "test1", con.ToArray());
            MappingsContainerBuilder.RegisterMapping<TestClassObjectSimple>(mappings, "test2");
            var container = MappingsContainerBuilder.Build();
            var simple = GetTestDBObjectSimple();
            TestClassObjectSimple mapped = container.Map<TestClassObjectSimple>(simple,"test1");
            TestClassObjectSimple mappedb = container.Map<TestClassObjectSimple>(simple, "test2");
            Assert.IsNotNull(mapped);


            Assert.AreEqual(simple.Id, mapped.Id);
            Assert.AreEqual(simple.Longer, mapped.Longer);
            Assert.AreEqual(simple.Count, mapped.Count);
            Assert.AreEqual(simple.Floater, mapped.Floater);
            Assert.AreEqual(simple.Ok, mapped.Valid);
            Assert.AreEqual(simple.Description, mapped.Understated);
            Assert.AreEqual("EXTRA", mapped.Extra);
            Assert.AreEqual(simple.Description, mappedb.Understated);
            Assert.IsNull(mappedb.Extra);

        }
        [TestMethod]
        public void TestBasicContainerBuilder()
        {
            Dictionary<string, string> mappings = new Dictionary<string, string>();
            mappings.Add("Name", "Description");
            mappings.Add("Description", "understated");
            mappings.Add("Ok", "Valid");
            List<string> con = new List<string>();
            con.Add("EXTRA");
            MappingsBuilder builder = new MappingsBuilder();
            builder.WithMappings(mappings).RegisterMapTo<TestClassObjectSimple>("test1", con.ToArray());
            builder.RegisterMapTo<TestClassObjectSimple>("test2");
            var container = builder.Build();
            var simple = GetTestDBObjectSimple();
            TestClassObjectSimple mapped = container.Map<TestClassObjectSimple>(simple, "test1");
            TestClassObjectSimple mappedb = container.Map<TestClassObjectSimple>(simple, "test2");
            Assert.IsNotNull(mapped);


            Assert.AreEqual(simple.Id, mapped.Id);
            Assert.AreEqual(simple.Longer, mapped.Longer);
            Assert.AreEqual(simple.Count, mapped.Count);
            Assert.AreEqual(simple.Floater, mapped.Floater);
            Assert.AreEqual(simple.Ok, mapped.Valid);
            Assert.AreEqual(simple.Description, mapped.Understated);
            Assert.AreEqual("EXTRA", mapped.Extra);
            Assert.AreEqual(simple.Description, mappedb.Understated);
            Assert.IsNull(mappedb.Extra);

        }


        [TestMethod]
        public void TestBasicContainerBuilderUsingFrom()
        {
            Dictionary<string, string> mappings = new Dictionary<string, string>();
            mappings.Add("Name", "Description");
            mappings.Add("Description", "understated");
            mappings.Add("Ok", "Valid");
            List<string> con = new List<string>();
            con.Add("EXTRA");
            MappingsBuilder builder = new MappingsBuilder();
            var simple = GetTestDBObjectSimple();
            builder.RegisterMapTo<TestClassObjectSimple>("test1");
            builder.WithMappings(mappings).RegisterMapTo<TestClassObjectSimple>("test3");
            builder.RegisterMapFrom<TestDBObjectSimple>(simple).WithMappings(mappings).RegisterMapTo<TestClassObjectSimple>("test2", con.ToArray());
           
            var container = builder.Build();
          
            TestClassObjectSimple mapped = container.Map<TestClassObjectSimple>(simple, "test1");
            TestClassObjectSimple mappedb = container.Map<TestClassObjectSimple>("test2");
            TestClassObjectSimple mappedc = container.Map<TestClassObjectSimple>(simple,"test3");
            Assert.IsNotNull(mapped);
            Assert.IsNotNull(mappedb);

            Assert.AreEqual(simple.Id, mapped.Id);
            Assert.AreEqual(simple.Longer, mapped.Longer);
            Assert.AreEqual(simple.Count, mapped.Count);
            Assert.AreEqual(simple.Floater, mapped.Floater);
            Assert.AreEqual(simple.Ok, mapped.ok);
            Assert.AreEqual(simple.Description, mapped.Description);
            Assert.AreEqual("EXTRA", mappedb.Extra);
            Assert.AreEqual("TestDBObjectSimple", mappedb.Description);
            Assert.AreEqual("full description.", mappedc.Understated);
            Assert.IsNull(mapped.Extra);

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
