using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RectangleSolver
{
	public class Stdout
	{
		List<int> m_assumptions;
		int m_conclusion;

		public string m_requirements;
		public string m_results;

		Processor m_processors;

		public Stdout(Processor processors, List<int> assumptions, int conclusion)
		{
			m_processors = processors;
			m_assumptions = assumptions;
			m_conclusion = conclusion;

			m_requirements = "";
			m_results = "";
		}

		public void Out()
		{
			//RepresentRequirements();
			RepresentSolutions();
		}

		private void RepresentRequirements()
		{
			m_requirements += "Bài toán Tam giác\n\n" + "Giả thiết: \n";
			for (int i = 0; i < Statics.ATTRIBUTE.Length; i++)
			{
				if (m_assumptions[i] == 0)
				{
					m_requirements += "- " + Statics.ATTRIBUTE_STR[i] + " = \t" + m_processors.ListValues[i] + ".\n";
				}
			}

			m_requirements += "\nKết luận:\n";
			m_requirements += "- Tính " + Statics.ATTRIBUTE_STR[m_conclusion] + "?\n";
		}

		private void RepresentSolutions()
		{
			for (int i = 0; i < m_processors.m_listRulesUsed.Count; i++)
			{
				if (m_processors.m_listRulesUsed.Count > 1)
					m_results += "* Bước " + (i + 1) + ": ";
				else
					m_results += "* ";

				for (int j = 0; j < Statics.ATTRIBUTE.Length; j++)
				{
					if (m_processors.m_listRules[m_processors.m_listRulesUsed[i]][j] == 1)
					{
						m_results += "Tính " + Statics.ATTRIBUTE_STR[j] + "\n";
						m_results += "- Từ công thức: " + Statics.ATTRIBUTE[j] + " = " + GetFormula(m_processors.m_listRulesUsed[i]) + "\n"
									+ "   Ta suy ra: " + Statics.ATTRIBUTE_STR[j] + " = " + m_processors.ListValues[j] + "\n";

						if (i == m_processors.m_listRulesUsed.Count - 1)
							m_results += "\nKết luận: Vậy giá trị của " + Statics.ATTRIBUTE_STR[j] + " cần tìm là "
												+ m_processors.ListValues[j] + ".\n";

						break;
					}
				}
			}
		}

		private string GetFormula(int expressionIndex)
		{
			string _formula = File.ReadLines(Statics.RULES_DIRECTORY).Skip(expressionIndex).Take(1).First();
			_formula = _formula.Substring(_formula.IndexOf(Statics.RULED_DELIMITER) + 1);

			return _formula;
		}
	}
}
