using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;

namespace FelixTests
{

    public class ObjectEvaluator
    {
        public static bool IsEqual(PropertyInfo pInfo, object lhs, object rhs)
        {
            Evaluator evaluator = GetEvaluator(pInfo, lhs, rhs);

            if (evaluator == null)
            {
                Debugger.Break();
            }

            return evaluator.AreEqual;
        }

        private static Evaluator GetEvaluator(PropertyInfo pInfo, object lhs, object rhs)
        {
            Evaluator retEvaluator = null;

            if (lhs == null && rhs == null)
            {
                retEvaluator = new NullItemEvaluator(pInfo, lhs, rhs);
            }
            else
            {
                Debug.WriteLine($"PropertyType: {pInfo.PropertyType.Name.ToLower()}");

                switch (pInfo.PropertyType.Name.ToLower())
                {
                    case "int32": retEvaluator = new IntEvaluator(pInfo, lhs, rhs); break;
                    case "string": retEvaluator = new StringEvaluator(pInfo, lhs, rhs); break;
                    case "datetime": retEvaluator = new DateTimeEvaluator(pInfo, lhs, rhs); break;
                    case "double": retEvaluator = new DoubleEvaluator(pInfo, lhs, rhs); break;
                    case "ienumerable`1": retEvaluator = new EnumerableEvaluator(pInfo, lhs, rhs); break;
                    case "nullable`1": retEvaluator = new NullableEvaluator(pInfo, lhs, rhs); break;

                }
            }

            return retEvaluator;

        }
    }


    public abstract class Evaluator
    {
        protected Object Lhs { get; set; }
        protected Object Rhs { get; set; }

        protected PropertyInfo PInfo { get; set; }

        public Evaluator(PropertyInfo pInfo, Object lhs, object rhs)
        {
            PInfo = pInfo;

            Lhs = lhs;

            Rhs = rhs;
        }

        public abstract bool AreEqual { get; }
    }

    public class IntEvaluator : Evaluator
    {
        public IntEvaluator(PropertyInfo pInfo, Object lhs, object rhs) : base(pInfo, lhs, rhs) { }

        public override bool AreEqual
        {
            get
            {
                return Convert.ToInt64(Lhs) == Convert.ToInt64(Rhs);
            }
        }
    }

    public class StringEvaluator : Evaluator
    {
        public StringEvaluator(PropertyInfo pInfo, Object lhs, object rhs) : base(pInfo, lhs, rhs) { }

        public override bool AreEqual
        {
            get
            {
                return Lhs.ToString().Equals(Rhs.ToString());
            }
        }
    }

    public class DateTimeEvaluator : Evaluator
    {
        public DateTimeEvaluator(PropertyInfo pInfo, Object lhs, object rhs) : base(pInfo, lhs, rhs) { }

        public override bool AreEqual
        {
            get
            {
                return Lhs.ToString().Equals(Rhs.ToString());
            }
        }
    }

    public class DoubleEvaluator : Evaluator
    {
        public DoubleEvaluator(PropertyInfo pInfo, Object lhs, object rhs) : base(pInfo, lhs, rhs) { }

        public override bool AreEqual
        {
            get
            {
                return Convert.ToDouble(Lhs) == Convert.ToDouble(Rhs);
            }
        }
    }

    public class EnumerableEvaluator : Evaluator
    {
        public EnumerableEvaluator(PropertyInfo pInfo, object lhs, object rhs) : base(pInfo, lhs, rhs) { }

        public override bool AreEqual
        {
            get
            {
                IEnumerable<object> left = Lhs as IEnumerable<object>;

                IEnumerable<object> right = Rhs as IEnumerable<object>;

                bool bEqual = false;

                if (left != null && right != null)
                {
                    bEqual = left.Count() == right.Count();
                }

                return bEqual;
            }
        }
    }

    public class NullItemEvaluator: Evaluator
    {
        public NullItemEvaluator(PropertyInfo pInfo, object lhs, object rhs) : base(pInfo, lhs, rhs) { }

        public override bool AreEqual =>  true;
    }

    public class NullableEvaluator : Evaluator
    {
        public NullableEvaluator(PropertyInfo pInfo, object lhs, object rhs) : base(pInfo, lhs, rhs) { }

        public override bool AreEqual
        { 
            get
            {
                bool bEqual = Lhs == null && Rhs == null;

                if (bEqual == false)
                {
                    Type type = PInfo.PropertyType.GenericTypeArguments[0];

                    if (type.Name.ToLower().Equals("double"))
                    {
                        double lhsValue = (double)Convert.ChangeType(Lhs, type);

                        double rhsValue = (Double) Convert.ChangeType(Rhs, type);

                        bEqual = lhsValue == rhsValue;
                    }
                    else
                    {
                        int lhsValue = (int)Convert.ChangeType(Lhs, type);

                        int rhsValue = (int)Convert.ChangeType(Rhs, type);

                        bEqual = lhsValue == rhsValue;
                    }
                }
                
                return bEqual;
            }
        }
    }

    //public class MarketEvaluator : Evaluator
    //{
    //    public MarketEvaluator(PropertyInfo pInfo, object lhs, object rhs) : base(pInfo, lhs, rhs)
    //    {
    //        Properties = typeof(IMarket).GetProperties().ToList();
    //    }

    //    private List<PropertyInfo> Properties { get; set; }
    //    public override bool AreEqual
    //    {
    //        get
    //        {
    //            IMarket left = Lhs as IMarket;

    //            IMarket right = Rhs as IMarket;

    //            bool bEqual = false;

    //           foreach ( var prop in Properties)
    //            {
    //                bEqual = 
    //            }

    //            return bEqual;
    //        }
    //    }
    //}
}
