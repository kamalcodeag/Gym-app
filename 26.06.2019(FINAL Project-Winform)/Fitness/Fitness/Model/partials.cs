using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Model
{
    public partial class Position
    {
        public override string ToString()
        {
            return Name;
        }
    }
    public partial class Service
    {
        public override string ToString()
        {
            return Name + " / " + Time + " / " + Price__per_hour_ + " AZN";
        }
    }
    public partial class Time
    {
        public override string ToString()
        {
            return Name + " | " + Hours;
        }
    }
    public partial class Employee
    {
        public override string ToString()
        {
            return Name + " " + Surname;
        }
    }

    public partial class Package
    {
        public override string ToString()
        {
            return Name + " " + Price__per_month_;
        }
    }

    public partial class Customer
    {
        public override string ToString()
        {
            return Name + " " + Surname;
        }
    }

    public partial class Order
    {
        public override string ToString()
        {
            return Customer + " " + Employee + " " + Date + " " + Price;
        }
    }

    public partial class OrderDetail
    {
        public override string ToString()
        {
            return Order + " " + Package + " " + Service;
        }
    }

}
