namespace lab05
{
	internal class Fraction
	{
		private int _numerator, _denominator;

		public Fraction(int numerator, int denominator)
		{
			this.Numerator = numerator;
			this.Denominator = denominator;
			this.Simplify(); // Сразу попробуем упростить дробь
							 //Console.WriteLine("Создана дробь: " + this.ToString());
		}

		public Fraction() : this(0, 1) { }

		public Fraction(int number) : this(number, 1) { } // Дробь из целого числа

		public Fraction(Fraction c) // Копировать Дробь
		{
			this.Numerator = c.Numerator;
			this.Denominator = c.Denominator;
			// Думаю, бедут разумнее не упрощать копированную дробь, чтобы они "полностью" соответствовали как и ожидается
		}

		public int Numerator
		{
			get { return _numerator; }
			set { _numerator = value; }
		}

		public int Denominator
		{
			get { return _denominator; }
			set
			{
				if (value != 0)
				{
					_denominator = value;
				}
				else
				{
					throw new ArgumentException("Знаменатель дроби не может быть равен нулю.");
				}
			}
		}

		public Fraction Simplify() // Мутирует экземпляр. Возвращает Дробь вместо войда чтобы можно было реализовывать "цепочку вызовов методов"
		{
			if (Numerator == 0)
			{
				Denominator = 1;
			}
			else
			{
				if (Numerator < 0 && Denominator < 0)
				{
					Numerator *= -1;
					Denominator *= -1;
				}
				bool doAnotherAteration = true;
				while (doAnotherAteration)
				{
					double divider = 1.0; // "проверялка" - Дабл потому что надо будет делить потом
					bool canSimplify;
					do
					{
						divider += 1.0;
						canSimplify = Math.Abs(Numerator / divider) % 1 < 0.000001 && Math.Abs(Denominator / divider) % 1 < 0.000001; // Сравниваем с малым числом потому что дабл неточный тип
					}
					while (divider <= Math.Abs(Numerator) && divider <= Math.Abs(Denominator) && !canSimplify);
					if (canSimplify)
					{
						Numerator /= (int)divider;
						Denominator /= (int)divider;
						// Console.WriteLine($"Упростили на {divider}");
						// doAnotherIteration = true;
					}
					else
					{
						// Console.WriteLine($"Нельзя упростить на {divider}, заканчиваем");
						doAnotherAteration = false;
					}
				}
			}
			return this;
		}

		public Fraction Simplified() // Возвращает новый упрощённый экземпляр
		{
			return new Fraction(this).Simplify();
		}

		public Fraction Flip() // Перевернуть дробь
		{
			if (Numerator == 0)
			{
				throw new DivideByZeroException("Попытка перевернуть дробь, числитель которой равен нулю");
			}
			int temp = Numerator;
			Numerator = Denominator;
			Denominator = temp;
			return this;
		}

		public Fraction Flipped() // Создать новую дробь и перевернуть её
		{
			return new Fraction(this).Flip();
		}

		public void Print()
		{
			Console.WriteLine(this.ToString());
		}

		public override string ToString()
		{
			if (Numerator == 0)
			{
				return "0";
			}
			else if (Denominator == 1)
			{
				return Numerator.ToString();
			}
			else if (Denominator == -1)
			{
				return (-Numerator).ToString();
			}
			else
			{
				if (Numerator * Denominator > 0)
				{
					// Либо оба положительные, либо оба отрицательные
					return String.Format("{0}/{1}", Numerator, Denominator);
				}
				else
				{
					return String.Format("-{0}/{1}", Math.Abs(Numerator), Math.Abs(Denominator));
				}
			}
		}

		public double ToDouble()
		{
			return (double)Numerator / (double)Denominator;
		}

		// Весь функционал сложения, вычитания и т.д. уже описан в переопределении операторов.
		// Последующие методы служат лишь обёртывающими функциями
		public Fraction Add(Fraction f)
		{
			Fraction result = this + f;
			Numerator = result.Numerator;
			Denominator = result.Denominator;
			return this;
		}

		public Fraction Subtract(Fraction f)
		{
			Fraction result = this - f;
			Numerator = result.Numerator;
			Denominator = result.Denominator;
			return this;
		}

		public Fraction Multiply(Fraction f)
		{
			Fraction result = this * f;
			Numerator = result.Numerator;
			Denominator = result.Denominator;
			return this;
		}

		public Fraction Divide(Fraction f)
		{
			Fraction result = this / f;
			Numerator = result.Numerator;
			Denominator = result.Denominator;
			return this;
		}

		// Переопределение операторов
		public static Fraction operator +(Fraction a, Fraction b)
		{
			// По сути, приводит их к общему знаменателю, перемножая знаменатели дробей, и затем склаывает числители
			return new Fraction(a.Numerator * b.Denominator + b.Numerator * a.Denominator, a.Denominator * b.Denominator).Simplify();
		}

		public static Fraction operator -(Fraction a, Fraction b)
		{
			return a + (-b);
		}

		public static Fraction operator -(Fraction f)
		{
			return new Fraction(-f.Numerator, f.Denominator);
		}

		public static Fraction operator *(Fraction a, Fraction b)
		{
			return new Fraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator).Simplify();
		}

		public static Fraction operator /(Fraction a, Fraction b)
		{
			if (b.Numerator == 0)
			{
				throw new DivideByZeroException("Нельзя делить на ноль!");
			}
			return a * b.Flipped();
		}

		// Статические методы
		public static Fraction AddFractions(Fraction a, Fraction b)
		{
			return a + b;
		}

		public static Fraction SubtractFractions(Fraction a, Fraction b)
		{
			return a - b;
		}

		public static Fraction MultiplyFractions(Fraction a, Fraction b)
		{
			return a * b;
		}

		public static Fraction DivideFractions(Fraction a, Fraction b)
		{
			return a / b;
		}
	}
}
