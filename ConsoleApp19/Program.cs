using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp19
{
    class Program
    {
        static void Main(string[] args)
        {
            List<PC> persСomp = new List<PC>
            {
                new PC {Id = 1, Name = "Офисный", TypeCPU = "Intel", FrequencyCPU = 3200, CapacityRAM = 8000, CapacityHDD = 320, CapacityVideoRAM = 1000, Price = 300, Amount = 30},
                new PC {Id = 2, Name = "Бытовой", TypeCPU = "AMD", FrequencyCPU = 3600, CapacityRAM = 8000, CapacityHDD = 500, CapacityVideoRAM = 2000, Price = 400, Amount = 10 },
                new PC {Id = 3, Name = "Домашний", TypeCPU = "AMD", FrequencyCPU = 3200, CapacityRAM = 16000, CapacityHDD = 1000, CapacityVideoRAM = 1000, Price = 350, Amount = 15 },
                new PC {Id = 4, Name = "Рабочий", TypeCPU = "Intel", FrequencyCPU = 4000, CapacityRAM = 16000, CapacityHDD = 1000, CapacityVideoRAM = 8000, Price = 1000, Amount = 17 },
                new PC {Id = 5, Name = "Проф", TypeCPU = "Эльбрус", FrequencyCPU = 3200, CapacityRAM = 8000, CapacityHDD = 320, CapacityVideoRAM = 1000, Price = 300, Amount = 33 },
                new PC {Id = 6, Name = "Военный", TypeCPU = "Эльбрус", FrequencyCPU = 2000, CapacityRAM = 8000, CapacityHDD = 320, CapacityVideoRAM = 1000, Price = 10000, Amount = 2 },
                new PC {Id = 7, Name = "Сервер", TypeCPU = "Intel", FrequencyCPU = 3800, CapacityRAM = 32000, CapacityHDD = 8000, CapacityVideoRAM = 1000, Price = 5000, Amount = 5 }
            };
            //Вводные данные от пользователя
            Console.WriteLine("Введите необходимую операцию\n1 - Отбор ПК по процессору\n2 - Отбор ПК по объему ОЗУ\n3 - Список по увеличению стоимости\n4 - Группировка по типу ПК\n5 - Поиск самого дорогого ПК" +
                "\n6 - Поиск самого бюджетного ПК\n7 - Определение наличия количества ПК на остатке");
            int option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                //отбор данных по типу CPU
                case 1:
                    string nameCPU;
                    Console.WriteLine("Введите наименовие процессора ПК (Intel/AMD/Эльбрус)");
                    nameCPU = Console.ReadLine();
                    List<PC> PC_1 = persСomp
                        .Where(d => d.TypeCPU == nameCPU)
                        .ToList();
                    break;
                //отбор данных по объему RAM
                case 2:
                    int capacityRAM;
                    Console.WriteLine("Введите минимальный объем RAM");
                    capacityRAM = Convert.ToInt32(Console.ReadLine());
                    List<PC> PC_2 = persСomp
                        .Where(d => d.CapacityRAM >= capacityRAM)
                        .ToList();
                    break;
                //сортировка по увеличению стоимости
                case 3:
                    List<PC> PC_3 = persСomp
                        .OrderBy(d => d.Price)
                        .ToList();
                    break;
                //группировка по типу процессора
                case 4:
                    IEnumerable<IGrouping<string, PC>> PC_4 = persСomp.GroupBy(p => p.TypeCPU);
                    foreach (IGrouping<string, PC> igr in PC_4)
                    {
                        Console.WriteLine(igr.Key);
                        foreach (PC p in igr)
                        {
                            Console.WriteLine($"{p.Id} {p.Name} Процессор-{p.TypeCPU} Частота CPU-{p.FrequencyCPU}МГц ОЗУ-{p.CapacityRAM}Мб HDD-{p.CapacityHDD}Гб VRAM-{p.CapacityVideoRAM}Мб Цена-{p.Price} У.Е, на складе - {p.Amount } шт.");
                        }
                    }
                    break;
                //Самый недорогой компьютер. Сделал дополнительно проверку со след. элементом, т.к. может несколько объектов с самой низкой ценой
                case 5:
                    List<PC> PC_5 = persСomp
                        .OrderBy(d => d.Price)
                        .ToList();
                    bool flagMin = true;
                    int i = 0;
                    do
                    {
                        i++;
                        if (PC_5[i].Price > PC_5[i - 1].Price) flagMin = false;
                    }
                    while (flagMin == true);
                    break;
                //Самый дорогой компьютер. Сделал дополнительно проверку со след. элементом, т.к. может несколько объектов с самой высокой ценой
                case 6:
                    List<PC> PC_6 = persСomp
                        .OrderByDescending(d => d.Price)
                        .ToList();
                    bool flagMax = true;
                    int j = 0;
                    do
                    {
                        j++;
                        if (PC_6[j].Price < PC_6[j - 1].Price) flagMax = false;
                    }
                    while (flagMax == true);
                    break;
                //Проверка на наличие одной позиции в количестве, указанном пользователем
                case 7:
                    Console.WriteLine("Укажите необходимое количество ПК на остатке");
                    int amount = Convert.ToInt32(Console.ReadLine());
                    bool flagAmount = persСomp.Any(d => d.Amount >= amount);
                    if (flagAmount == true) Console.WriteLine("На складе есть позиция с числом ПК более " + amount);
                    else Console.WriteLine("На складе отсутствуют позиции с числом ПК более " + amount);
                    break;
                default:
                    Console.WriteLine($"Некорректно введены данные");
                    break;
            }
            Console.ReadKey();
        }
    }
}
