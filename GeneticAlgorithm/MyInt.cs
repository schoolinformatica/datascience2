using System;

namespace GeneticAlgorithm
{
    public class MyInt
    {
        private readonly int _integer;

        public MyInt(int integer)
        {
            _integer = integer;
        }

        public int AsInt()
        {
            return _integer;
        }

        public override string ToString()
        {
            return _integer.ToString();
        }
        
        public static MyInt operator <<(MyInt value, int shift)
        {
            return new MyInt(value._integer << shift);
        }
        
        public static MyInt operator >>(MyInt value, int shift)
        {
            return new MyInt(value._integer >> shift);
        }
        
        public static MyInt operator &(MyInt value, int shift)
        {
            return new MyInt(value._integer & shift);
        }
        
        public static MyInt operator |(MyInt value, int shift)
        {
            return new MyInt(value._integer | shift);
        }
        
        public static MyInt operator |(MyInt value, MyInt value2)
        {
            return new MyInt(value._integer | value2._integer);
        }
        
        public static MyInt operator ^(MyInt value, int shift)
        {
            return new MyInt(value._integer ^ shift);
        }
        
    }
}