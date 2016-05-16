using System;

namespace RectangleSolver
{
	public class Attribute
	{
		public int m_attribute;
		public string m_value;

		public void Init(int id, string value)
		{
			m_attribute = id;
			m_value = value;
		}
	}

	public static class Statics
	{
		public static readonly string RULES_DIRECTORY = @"..\..\data\Rules.txt";
		public static readonly char RULED_DELIMITER = '.';
		public static readonly string EMPTY_STR = "";

		public static readonly string[] ATTRIBUTE =
		{
			"A",
			"B",
			"C",
			"D",
			"a",
			"b",
			"S",
			"P",
			"p",
			"m",
			"n",
			"r"
		};

		public static readonly string[] ATTRIBUTE_STR =
		{
			"A",
			"B",
			"C",
			"D",
			"a",
			"b",
			"S",
			"P",
			"p",
			"m",
			"n",
			"r"
		};

		public static readonly int NOT_RELATE = -1;
		public static readonly int IN_ASSUMPTIONS = 0;
		public static readonly int IN_CONCLUSION = 1;

		public static readonly int NOT_USED_YET = -1;
		public static readonly int USED = 0;
	}
}
