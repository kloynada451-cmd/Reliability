
using System;

namespace Reliability
{
    // Ошибка, если автомобиль не выбран
    public class CarNotSelectedException : Exception
    {
        public CarNotSelectedException(string message)
            : base(message)
        {
        }
    }

    // Ошибка, если стоимость заказа неправильная
    public class InvalidOrderPriceException : Exception
    {
        public InvalidOrderPriceException(string message)
            : base(message)
        {
        }
    }

    public class CarWashOrderProcessor
    {
        private static CarWashLogger logger = new CarWashLogger();
        public static void ProcessOrder(
            string carNumber,
            decimal price,
            decimal payment)
        {
            try
            {
                Console.WriteLine("Оформление заказа на автомойке...");
                logger.LogInformation("Начато оформление заказа для автомобиля {CarNumber}","CarNumber",carNumber);

                if (string.IsNullOrWhiteSpace(carNumber))
                {
                    throw new CarNotSelectedException(
                        "Не выбран автомобиль для мойки.");
                }

                if (price <= 0)
                {
                    throw new InvalidOrderPriceException(
                        "Стоимость услуги должна быть больше нуля.");
                }

                if (payment < price)
                {
                    logger.LogWarning(
                        "Недостаточно денег для оплаты заказа {CarNumber}",
                        "CarNumber",
                        carNumber
                    );

                    throw new Exception(
                        "Недостаточно денег для оплаты заказа.");
                }

                Console.WriteLine("Номер автомобиля: " + carNumber);
                Console.WriteLine("Стоимость мойки: " + price + " руб.");
                Console.WriteLine("Оплата принята: " + payment + " руб.");
                Console.WriteLine("Заказ успешно оформлен.");
                logger.LogInformation("Заказ успешно оформлен для автомобиля {CarNumber}","CarNumber",carNumber);
            }
            catch (CarNotSelectedException ex)
            {
                Console.WriteLine("Ошибка выбора автомобиля: " + ex.Message);
                logger.LogWarning("Не удалось выбрать автомобиль: {ErrorMessage}","ErrorMessage",ex.Message);
            }
            catch (InvalidOrderPriceException ex)
            {
                Console.WriteLine("Ошибка стоимости: " + ex.Message);
                logger.LogError("Ошибка стоимости заказа: {ErrorMessage}","ErrorMessage",ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка оформления заказа: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Завершена попытка оформления заказа.");
                Console.WriteLine("--------------------------------");
            }
        }
    }
}

