using System;
using System.Linq;
using Lab5_AllTasks;

Console.OutputEncoding = System.Text.Encoding.UTF8;

RunTasks1And2();
RunTask3();
RunTask4();

Console.WriteLine("\nНатисніть Enter для завершення програми...");
Console.ReadLine();

void RunTasks1And2()
{
    Console.WriteLine("\n>>> ЗАВДАННЯ 1.14 та 2.14: ІЄРАРХІЯ ТВАРИН <<<");
    Animal[] animals = new Animal[]
    {
        new Mammal("Тигр", 200, true),
        new Bird("Орел", 5, 2.2),
        new Artiodactyl("Олень", 150, false, 60),
        new Mammal("Кріль"),
        new Bird()
    };

    Console.WriteLine("\n--- Впорядкований масив (за вагою) ---");
    var sortedAnimals = animals.OrderBy(a => a.Weight).ToArray();
    foreach (var animal in sortedAnimals)
    {
        animal.Show();
    }
}

void RunTask3()
{
    Console.WriteLine("\n>>> ЗАВДАННЯ 3.4: ТРАНСПОРТНІ ЗАСОБИ <<<");
    Trans[] vehicles = {
        new PassengerCar("Toyota Corolla", "AA1234BB", 180, 450),
        new Motorcycle("Yamaha", "BB5678AA", 220, 150, false),
        new Motorcycle("Дніпро", "CE9012XX", 110, 200, true),
        new Truck("MAN", "KA3344OO", 120, 5000, false),
        new Truck("Volvo", "AT5566II", 110, 6000, true)
    };

    foreach (var v in vehicles) v.ShowInfo();

    double requiredPayload = 300;
    Console.WriteLine($"\n--- Пошук транспорту (Вантажопідйомність >= {requiredPayload}кг) ---");
    var suitableVehicles = vehicles.Where(v => v.GetPayload() >= requiredPayload);
    foreach (var v in suitableVehicles) v.ShowInfo();
}

void RunTask4()
{
    Console.WriteLine("\n>>> ЗАВДАННЯ 4: РОМБ (partial sealed клас) <<<");
    Romb myRomb = new Romb(5, 6, 1);
    myRomb.Show();
}

namespace Lab5_AllTasks
{
    abstract class Animal
    {
        public string Name { get; set; }
        public double Weight { get; set; }

        public Animal() { Name = "Невідомо"; Weight = 0; Console.WriteLine("Викликано Animal()"); }
        public Animal(string name) { Name = name; Weight = 0; Console.WriteLine($"Викликано Animal(string) для {Name}"); }
        public Animal(string name, double weight) { Name = name; Weight = weight; Console.WriteLine($"Викликано Animal(string, double) для {Name}"); }

        ~Animal() { Console.WriteLine($"[Деструктор] Знищено тварину: {Name}"); }

        public abstract void Show();
    }

    class Mammal : Animal
    {
        public bool IsPredator { get; set; }

        public Mammal() : base() { IsPredator = false; Console.WriteLine("Викликано Mammal()"); }
        public Mammal(string name) : base(name) { IsPredator = false; Console.WriteLine("Викликано Mammal(string)"); }
        public Mammal(string name, double weight, bool isPredator) : base(name, weight)
        {
            IsPredator = isPredator;
            Console.WriteLine("Викликано Mammal(string, double, bool)");
        }

        ~Mammal() { Console.WriteLine($"[Деструктор] Знищено ссавця: {Name}"); }

        public override void Show()
        {
            Console.WriteLine($"[Савець] Ім'я: {Name}, Вага: {Weight}кг, Хижак: {IsPredator}");
        }
    }

    class Artiodactyl : Mammal
    {
        public int HornLength { get; set; }

        public Artiodactyl() : base() { HornLength = 0; Console.WriteLine("Викликано Artiodactyl()"); }
        public Artiodactyl(string name) : base(name) { HornLength = 0; Console.WriteLine("Викликано Artiodactyl(string)"); }
        public Artiodactyl(string name, double weight, bool isPredator, int hornLength) : base(name, weight, isPredator)
        {
            HornLength = hornLength;
            Console.WriteLine("Викликано Artiodactyl(повний)");
        }

        ~Artiodactyl() { Console.WriteLine($"[Деструктор] Знищено парнокопитне: {Name}"); }

        public override void Show()
        {
            Console.WriteLine($"[Парнокопитне] Ім'я: {Name}, Вага: {Weight}кг, Довжина рогів: {HornLength}см");
        }
    }

    class Bird : Animal
    {
        public double Wingspan { get; set; }

        public Bird() : base() { Wingspan = 0; Console.WriteLine("Викликано Bird()"); }
        public Bird(string name) : base(name) { Wingspan = 0; Console.WriteLine("Викликано Bird(string)"); }
        public Bird(string name, double weight, double wingspan) : base(name, weight)
        {
            Wingspan = wingspan;
            Console.WriteLine("Викликано Bird(повний)");
        }

        ~Bird() { Console.WriteLine($"[Деструктор] Знищено птаха: {Name}"); }

        public override void Show()
        {
            Console.WriteLine($"[Птах] Ім'я: {Name}, Вага: {Weight}кг, Розмах крил: {Wingspan}м");
        }
    }

    abstract class Trans
    {
        public string Brand { get; set; }
        public string Number { get; set; }
        public double Speed { get; set; }
        protected double BasePayload { get; set; }

        public Trans(string brand, string number, double speed, double payload)
        {
            Brand = brand;
            Number = number;
            Speed = speed;
            BasePayload = payload;
        }

        public abstract void ShowInfo();
        public abstract double GetPayload();
    }

    class PassengerCar : Trans
    {
        public PassengerCar(string brand, string number, double speed, double payload)
            : base(brand, number, speed, payload) { }

        public override double GetPayload() => BasePayload;

        public override void ShowInfo()
        {
            Console.WriteLine($"[Легкова] {Brand} ({Number}) | Швидкість: {Speed} | Вантажопідйомність: {GetPayload()}кг");
        }
    }

    class Motorcycle : Trans
    {
        public bool HasSidecar { get; set; }

        public Motorcycle(string brand, string number, double speed, double payload, bool hasSidecar)
            : base(brand, number, speed, payload)
        {
            HasSidecar = hasSidecar;
        }

        public override double GetPayload() => HasSidecar ? BasePayload : 0;

        public override void ShowInfo()
        {
            Console.WriteLine($"[Мотоцикл] {Brand} ({Number}) | Коляска: {(HasSidecar ? "Так" : "Ні")} | Вантажопідйомність: {GetPayload()}кг");
        }
    }

    class Truck : Trans
    {
        public bool HasTrailer { get; set; }

        public Truck(string brand, string number, double speed, double payload, bool hasTrailer)
            : base(brand, number, speed, payload)
        {
            HasTrailer = hasTrailer;
        }

        public override double GetPayload() => HasTrailer ? BasePayload * 2 : BasePayload;

        public override void ShowInfo()
        {
            Console.WriteLine($"[Вантажівка] {Brand} ({Number}) | Причіп: {(HasTrailer ? "Так" : "Ні")} | Вантажопідйомність: {GetPayload()}кг");
        }
    }

    public sealed partial class Romb
    {
        private int a;
        private int d1;
        private int c;

        public Romb(int side, int diagonal, int color)
        {
            a = side;
            d1 = diagonal;
            c = color;
        }
    }

    public sealed partial class Romb
    {
        public int GetPerimeter() => 4 * a;

        public double GetArea()
        {
            double halfD1 = d1 / 2.0;
            if (halfD1 >= a) return 0;
            double halfD2 = Math.Sqrt(Math.Pow(a, 2) - Math.Pow(halfD1, 2));
            return (d1 * (2 * halfD2)) / 2.0;
        }

        public void Show()
        {
            Console.WriteLine($"Ромб: Сторона={a}, Діагональ={d1}, Колір={c}, P={GetPerimeter()}, S={GetArea():F2}");
        }
    }
}
