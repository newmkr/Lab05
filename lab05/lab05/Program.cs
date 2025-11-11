namespace lab05
{
	internal class Program
	{
		static void Main()
		{
			Console.WriteLine("Лабораторная работа 05. Выполнил студент 6104-020302D Круглов Данил\n");
			int menuOption;
			do
			{
				Fraction a, b;
				Console.WriteLine("Введите основную дробь \"A\" - целое число или дробь в формате \"[Делитель]/[Знаменатель]\". Числа должны быть целыми.");
				a = InputFraction();
				//a = new Fraction(1, 2);
				Console.WriteLine("Введите дополнительную дробь \"B\" - целое число или дробь в формате \"[Делитель]/[Знаменатель]\". Числа должны быть целыми.");
				b = InputFraction();
				//b = new Fraction(2, 3);
				Console.Write("Дробь A: ");
				a.Print();
				Console.Write("Дробь B: ");
				b.Print();
				Console.Write("Упрощённая A: ");
				a.Simplified().Print();
				Console.Write("Перевернутая A: ");
				a.Flipped().Print();
				Console.Write("A в виде double: ");
				Console.WriteLine(a.ToDouble());
				Console.WriteLine("### Использование переопределения операторов ###");
				Console.Write("A + B: ");
				(a + b).Print();
				Console.Write("A - B: ");
				(a - b).Print();
				Console.Write("A * B: ");
				(a * b).Print();
				Console.Write("A / B: ");
				try
				{
					(a / b).Print();
				}
				catch (DivideByZeroException e)
				{
					Console.WriteLine("Нельзя выполнить операцию: " + e.Message);
				}
				Console.WriteLine("### Использование метода экземпляра ###");
				Fraction old_a = new Fraction(a); // Копируем нынешнее А в новую переменную, чтобы к ней обращаться для сброса
				Console.Write("A + B: ");
				a.Add(b).Print();
				a = new Fraction(old_a); // Таким образом сбросим к предыдущему значению A, так как метод модифицирует экземпляр
				Console.Write("A - B: ");
				a.Subtract(b).Print();
				a = new Fraction(old_a);
				Console.Write("A * B: ");
				a.Multiply(b).Print();
				a = new Fraction(old_a);
				Console.Write("A / B: ");
				try
				{
					a.Divide(b).Print();
				}
				catch (DivideByZeroException e)
				{
					Console.WriteLine("Нельзя выполнить операцию: " + e.Message);
				}
				finally
				{
					a = new Fraction(old_a); // На всякий случай сбросим A к предыдущему значению именно в блоке finally
				}
				Console.WriteLine("### Использование статического метода ###");
				Console.Write("A + B: ");
				Fraction.AddFractions(a, b).Print();
				Console.Write("A - B: ");
				Fraction.SubtractFractions(a, b).Print();
				Console.Write("A * B: ");
				Fraction.MultiplyFractions(a, b).Print();
				Console.Write("A / B: ");
				try
				{
					Fraction.DivideFractions(a, b).Print();
				}
				catch (DivideByZeroException e)
				{
					Console.WriteLine("Нельзя выполнить операцию: " + e.Message);
				}
				Console.WriteLine("### Использование статического метода из другого класса ###");
				Console.Write("A + B: ");
				FractionHelper.AddFractions(a, b).Print();
				Console.Write("A - B: ");
				FractionHelper.SubtractFractions(a, b).Print();
				Console.Write("A * B: ");
				FractionHelper.MultiplyFractions(a, b).Print();
				Console.Write("A / B: ");
				try
				{
					FractionHelper.DivideFractions(a, b).Print();
				}
				catch (DivideByZeroException e)
				{
					Console.WriteLine("Нельзя выполнить операцию: " + e.Message);
				}
				Console.WriteLine("\nДемонстрация окончена. Выберите опцию:\n1 - Задать новую дробь\n\n0 - Завершение работы");
				menuOption = GetOption(0, 1);

			} while (menuOption != 0);
		}

		static int GetOption(int lowerBound, int upperBound)
		{
			int option;
			do
			{
				option = Convert.ToInt32(Console.ReadLine());
			} while (option < lowerBound || option > upperBound);
			return option;
		}

		static Fraction InputFraction()
		{
			Fraction result = new Fraction(); // Если не задать значение, компилятор жалуется
			bool success = false;
			do
			{
				try
				{
					result = FractionFromString(Console.ReadLine());
					success = true;
				}
				catch (Exception e)
				{
					Console.WriteLine("Произошла ошибка: " + e.Message);
				}
			} while (!success);
			return result;
		}

		static Fraction FractionFromString(string s)
		{
			string[] parts = s.Trim().Split('/');
			if (parts.Length == 1) // Было введено просто число
			{
				return new Fraction(Convert.ToInt32(parts[0]));

			}
			else if (parts.Length == 2) // Введена дробь
			{
				return new Fraction(Convert.ToInt32(parts[0]), Convert.ToInt32(parts[1]));
			}
			else
			{
				throw new FormatException("Неверный формат строки: " + s);
			}
		}
	}
}
