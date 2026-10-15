
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reliability
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("ПРАКТИЧЕСКАЯ РАБОТА №3");
            Console.WriteLine("ЗАДАНИЕ №1 — ПРОВЕРКА ДАННЫХ ЗАКАЗА");
            Console.WriteLine();

            Console.WriteLine("=== ПРОВЕРКА НЕПРАВИЛЬНЫХ ДАННЫХ ===");

            CarWashOrder badOrder = new CarWashOrder
            {
                OrderId = 0,
                ClientName = "",
                Price = -500,
                ServiceCount = 0,
                Email = "wrong-email"
            };

            CheckOrder(badOrder);

            Console.WriteLine();

            Console.WriteLine("=== ПРОВЕРКА ПРАВИЛЬНЫХ ДАННЫХ ===");

            CarWashOrder goodOrder = new CarWashOrder
            {
                OrderId = 15,
                ClientName = "Иван Петров",
                Price = 1500,
                ServiceCount = 2,
                Email = "ivan@example.com"
            };

            CheckOrder(goodOrder);

            Console.WriteLine();
            Console.WriteLine("ЗАДАНИЕ №2 — ОБРАБОТКА ИСКЛЮЧЕНИЙ");
            Console.WriteLine();

            CarWashOrderProcessor.ProcessOrder("А123АА", 1500, 1500);

            CarWashOrderProcessor.ProcessOrder("", 1500, 1500);

            CarWashOrderProcessor.ProcessOrder("В456ВВ", -500, 1000);

            CarWashOrderProcessor.ProcessOrder("С789СС", 1500, 500);

            Console.WriteLine();
            Console.WriteLine("Проверка завершена.");
            Console.ReadKey();
        }

        public static void CheckOrder(CarWashOrder order)
        {
            ValidationContext context = new ValidationContext(order);
            List<ValidationResult> results =
                new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(
                order,
                context,
                results,
                true
            );

            if (isValid)
            {
                Console.WriteLine("Заказ прошел проверку.");
                Console.WriteLine("Номер заказа: " + order.OrderId);
                Console.WriteLine("Клиент: " + order.ClientName);
                Console.WriteLine("Стоимость: " + order.Price + " руб.");
                Console.WriteLine("Количество услуг: " + order.ServiceCount);
                Console.WriteLine("Email: " + order.Email);
            }
            else
            {
                Console.WriteLine("Обнаружены ошибки:");

                foreach (ValidationResult error in results)
                {
                    Console.WriteLine("- " + error.ErrorMessage);
                }
            }
        }
    }

    public class CarWashOrder
    {
        [Range(1, 100000,
            ErrorMessage = "Номер заказа должен быть от 1 до 100000.")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Имя клиента обязательно для заполнения.")]
        [StringLength(100, MinimumLength = 2,
            ErrorMessage = "Имя клиента должно содержать от 2 до 100 символов.")]
        public string ClientName { get; set; }

        [Range(typeof(decimal), "0,01", "100000",
     ErrorMessage = "Стоимость должна быть от 0,01 до 100000 рублей.")]
        public decimal Price { get; set; }

        [Range(1, 20,
            ErrorMessage = "Количество услуг должно быть от 1 до 20.")]
        public int ServiceCount { get; set; }

        [Required(ErrorMessage = "Email клиента обязателен.")]
        [EmailAddress(ErrorMessage = "Указан неправильный формат email.")]
        public string Email { get; set; }
    }
}
