using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Praktika
{
    [Serializable]
    public class Nomer
    {
        int id ;
        
        double square;
        string type_Name ;
        string status ;
        DateTime end_Time ;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public double Square
        {
            get { return square; }
            set { square = value; }
        }
        public string Type_Name
        {
            get { return type_Name; }
            set { type_Name = value; }
        }
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        public DateTime End_Time
        {
            get { return end_Time; }
            set { end_Time = value; }
        }
        public Nomer(int _id, double _square, string _Type_Name, string _Status, DateTime _End_Time)
        {
            Id = _id;
            Square = _square;
            Type_Name = _Type_Name;
            Status = _Status;
            End_Time = _End_Time;
        }
        public Nomer()
        {
            Id = 1;
            Square = 40.2;
            Type_Name = "Стандартный";
            Status = "Booked";
            End_Time = Convert.ToDateTime("10.06.2020 14:00:00");

        }

    }
}
