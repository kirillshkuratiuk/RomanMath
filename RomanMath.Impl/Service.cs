using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace RomanMath.Impl
{
	public static class Service
	{
		/// <summary>
		/// See TODO.txt file for task details.
		/// Do not change contracts: input and output arguments, method name and access modifiers
		/// </summary>
		/// <param name="expression"></param>
		/// <returns></returns>

		public static int Evaluate(string expression)
		{
			if (string.IsNullOrEmpty(expression))
				return 0;

			Regex romanMathExpressionPattern = new Regex("^(M{0,3}(CM|CD|D?C{0,3})(XC|XL|L?X{0,3})(IX|IV|V?I{0,3})[-+*]?)*$");
			Regex romanCharacterPattern = new Regex("M{0,3}(CM|CD|D?C{0,3})(XC|XL|L?X{0,3})(IX|IV|V?I{0,3})");
			Regex replace;

			if (!romanMathExpressionPattern.IsMatch(expression))
				return 0;

			var result = romanCharacterPattern.Matches(expression).Cast<Match>().Select(match => match.Value).ToList();

			for (int i = 0; i < result.Count; i++)
			{
				string a = result[i];
				replace = new Regex(result[i].ToString());
				expression = replace.Replace(expression, RomanToArabic(result[i]).ToString(), 1);
			}

			return (int)new DataTable().Compute(expression, null);

		}

		private static int RomanToArabic(string romanNumeral)
		{
			int arabicNumeral = 0;
			int lastCharacter = 0;
			romanNumeral = romanNumeral.ToUpper();

			for (int i = romanNumeral.Length - 1; i >= 0; i--)
			{
				char character = romanNumeral[i];

				switch (character)
				{
					case 'M':
						arabicNumeral = convertDecimal(1000, lastCharacter, arabicNumeral);
						lastCharacter = 1000;
						break;
					case 'D':
						arabicNumeral = convertDecimal(500, lastCharacter, arabicNumeral);
						lastCharacter = 500;
						break;
					case 'C':
						arabicNumeral = convertDecimal(100, lastCharacter, arabicNumeral);
						lastCharacter = 100;
						break;
					case 'L':
						arabicNumeral = convertDecimal(50, lastCharacter, arabicNumeral);
						lastCharacter = 50;
						break;
					case 'X':
						arabicNumeral = convertDecimal(10, lastCharacter, arabicNumeral);
						lastCharacter = 10;
						break;
					case 'V':
						arabicNumeral = convertDecimal(5, lastCharacter, arabicNumeral);
						lastCharacter = 5;
						break;
					case 'I':
						arabicNumeral = convertDecimal(1, lastCharacter, arabicNumeral);
						lastCharacter = 1;
						break;
				}
			}
			return arabicNumeral;
		}

		private static int convertDecimal(int arabicNumeral, int lastCharacter, int lastArabicNumeral)
		{
			if (lastCharacter > arabicNumeral)
				return lastArabicNumeral - arabicNumeral;
			else
				return lastArabicNumeral + arabicNumeral;
		}
	}
}
