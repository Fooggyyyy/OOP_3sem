using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_laba2_3sem
{
    partial class Student
    {
        private static int instanceCount = 0;

        private const string university = "BSTU";//Константное поле
        private string name;
        private string surname;
        private string dadname;
        private string adress;
        private string fakultat;

        private readonly int id;//Только для чтения
        private int course;
        private int group;
        private long phoneNumber;

        private DateTime birthdayDate;
    }
}
