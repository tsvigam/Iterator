using System;

namespace Iterator
{
    public interface IEnumerator
    {
        public object CurrentItem();
        public object First();
        public bool IsDone();
        public object Next();
    }

    public class ConcreateEnumerator : IEnumerator
    {
        readonly IEnumerable enumerable;
        int current = -1;

        public ConcreateEnumerator(IEnumerable valutE)
        {
            enumerable = valutE;
        }

        public object CurrentItem()
        {
            return enumerable[current];
        }

        public object First()
        {
            return enumerable[0];
        }

        public object Next()
        {
            object nullO = null;
            current++;
            if (current < enumerable.Count)
                return enumerable[current];
            else return nullO;
        }

        public bool IsDone()
        {
            if (current + 1 < enumerable.Count)
                return true;
            else return false;
        }
    }

    public abstract class IEnumerable
    {
        public abstract IEnumerator CreateEnumerator();
        public abstract object this[int index] { get; set; }
        public abstract int Count { get; }
    }

    class Bank : IEnumerable
    {
        public Currency[] currencies;
        public override int Count
        {
            get { return currencies.Length; }
        }

        public Bank()
        {
            currencies = new Currency[]
            {
                new Currency{Name="RUB", Country="Russia", Course=0.014 },
                new Currency{Name="AUS", Country="Australia", Course=0.74 },
                new Currency{Name="Frk", Country="Switzeland", Course=1.08 }
            };
        }

        public override IEnumerator CreateEnumerator()
        {
            return new ConcreateEnumerator(this);
        }

        public override object this[int index]
        {
            get { return currencies[index]; }
            set { }
        }
    }

    class Currency
    {
        public string Name { set; get; } = "Dollar";
        public string Country { set; get; } = "USA";
        public double Course { set; get; } = 1.0;
    }

    class Person
    {
        public void SeeCurrencies(Bank b)
        {
            ConcreateEnumerator enumerator = new ConcreateEnumerator(b);
            while (enumerator.IsDone())
            {
                Currency v = (Currency)enumerator.Next();
                Console.WriteLine(v.Name);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Bank b = new Bank();
            Person p = new Person();
            p.SeeCurrencies(b);
        }
    }
}
