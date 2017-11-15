using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapObject.Test
{
    public class TestDBObjectSimple
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime TestDate { get; set; }
        public bool Valid { get; set; }
        public bool Ok { get; set; }
        public string understated { get; set; }

        public int Count { get; set; }
        public long Longer { get; set; }
        private int PrivateField  { get; set; }

        public float Floater { get; set; }
    }

    public class TestDTOObjectSimple
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime TestDate { get; set; }
        public bool Valid { get; set; }
        public bool ok { get; set; }
        public string Understated { get; set; }
        public int Count { get; set; }
        public long Longer { get; set; }
        private int PrivateField { get; set; }
        public float Floater { get; set; }

       

    }


    public class TestClassObjectSimple
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime TestDate { get; set; }

        public string Description { get; set; }
        public bool Valid { get; set; }
        public bool ok { get; set; }
        public string Understated { get; set; }
        public int Count { get; set; }
        public long Longer { get; set; }

        public string Extra { get; private set; }
        private int PrivateField { get; set; }
        public float Floater { get; set; }

        public TestClassObjectSimple()
        {

        }

        public TestClassObjectSimple(string Extra)
        {
            this.Extra = Extra;
        }

    }


    public class TestClassObjectComplex
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime TestDate { get; set; }

        public string Description { get; set; }
        public bool Valid { get; set; }
        public bool ok { get; set; }
        public string Understated { get; set; }
        public int Count { get; set; }
        public long Longer { get; set; }

        public string Extra { get; private set; }
        private int PrivateField { get; set; }
        public float Floater { get; set; }

        public TestSubClass SubClass { get; set; }
        public TestClassObjectComplex()
        {
            SubClass = new TestSubClass();
        }

        public TestClassObjectComplex(string Extra)
        {
            this.Extra = Extra;
        }

    }
    public class TestDTOObjectcomplex
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime TestDate { get; set; }
        public bool Valid { get; set; }
        public bool ok { get; set; }
        public string Understated { get; set; }
        public int Count { get; set; }
        public long Longer { get; set; }
        private int PrivateField { get; set; }
        public float Floater { get; set; }

        public TestSubClass SubClass { get; set; }
        public TestSubClass Sub { get; set; }
    }
    public class TestSubClass
    {
        public string Name { get; set; }

        public DateTime Time { get; set; }
    }
}
