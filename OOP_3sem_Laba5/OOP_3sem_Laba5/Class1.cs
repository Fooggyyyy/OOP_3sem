using OOP_3sem_Laba5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_3sem_Laba5
{

    public abstract partial class Vehicle : IDrive, IWheels
    {
        abstract class Driving_Vehicle // Управление транспортным средством
        {
            protected internal bool Flag = false;

            public bool Start()
            {
                if (!this.Flag)
                {
                    Console.WriteLine("Заводим машину!!!");
                    this.Flag = true;
                    return this.Flag;
                }
                else
                {
                    Console.WriteLine("Машина уже заведена");
                    return this.Flag;
                }
            }
        }
        abstract class Engine //Двигатель
        {
            public void EngineIsWoundUp(Driving_Vehicle dv)
            {
                if (dv.Start())
                    Console.WriteLine("Двигатель заведен");
                else
                    Console.WriteLine("Двигатель не заведен");
            }
        }
    }
}
