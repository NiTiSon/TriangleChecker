using System.Text.RegularExpressions;
using System.Windows;

namespace TriangleChecker
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void OnCalculate(object sender, RoutedEventArgs e)
		{
			inputBoxA.Text = inputBoxA.Text.Trim();
			inputBoxB.Text = inputBoxB.Text.Trim();
			inputBoxC.Text = inputBoxC.Text.Trim();
			resultText.Text = string.Empty;

			if (!UInt128.TryParse(inputBoxA.Text, out UInt128 valueA))
			{
				CheckField('A', inputBoxA.Text);
				return;
			}

			if (!UInt128.TryParse(inputBoxB.Text, out UInt128 valueB))
			{
				CheckField('B', inputBoxB.Text);
				return;
			}

			if (!UInt128.TryParse(inputBoxC.Text, out UInt128 valueC))
			{
				CheckField('C', inputBoxC.Text);
				return;
			}

			if (valueA == 0 || valueB == 0 || valueC == 0)
			{
				resultText.Text = "Стороны должны быть больше нуля";
				return;
			}

			if (valueA == valueB && valueA == valueC)
			{
				resultText.Text = "Треугольник — равносторонний";
				return;
			}

			try
			{
				checked
				{
					if (valueA > valueB + valueC)
					{
						resultText.Text = "Сторона A слишком большая";
						return;
					}
					if (valueB > valueA + valueC)
					{
						resultText.Text = "Сторона B слишком большая";
						return;
					}
					if (valueC > valueB + valueA)
					{
						resultText.Text = "Сторона C слишком большая";
						return;
					}
				}
			}
			catch (OverflowException)
			{
				resultText.Text = "Числа слишком большие";
				return;
			}

			if (valueA == valueB || valueA == valueC || valueB == valueC)
			{
				resultText.Text = "Треугольник — равнобедренный";
				return;
			}

			resultText.Text = "Треугольник — разносторонний";
		}

		[GeneratedRegex(@"^[\d]+$", RegexOptions.Multiline)]
		private static partial Regex DecimalRegex();

		[GeneratedRegex(@"^[\d\.,]+$", RegexOptions.Multiline)]
		private static partial Regex FloatRegex();

		private void CheckField(char sideName, string fieldContent)
		{
			if (fieldContent[0] == '-')
			{
				resultText.Text = $"Сторона {sideName} не может быть отрицательной.";
			}
			else if (FloatRegex().IsMatch(fieldContent))
			{
				resultText.Text = $"Сторона {sideName} содержит дробное число.";
			}
			else if (DecimalRegex().IsMatch(fieldContent))
			{
				resultText.Text = $"Сторона {sideName} слишком велика.";
			}
			else
			{
				resultText.Text = $"Неверно задана сторона {sideName}";
			}
		}
	}
}