using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RectangleSolver
{
	public class Processor
	{
		List<string> m_operators;

		List<double> m_listValues;
		public List<double> ListValues
		{
			get { return m_listValues; }
			set { m_listValues = value; }
		}

		public List<int> m_listRulesUsed;
		public List<List<int>> m_listRules;

		public List<Attribute> m_attributesInfo;

		public Processor(List<Attribute> attributesInfo, List<List<int>> ListRules, List<int> ListRulesUsed)
		{
			m_attributesInfo = attributesInfo;
			m_listRulesUsed = ListRulesUsed;
			m_listRules = ListRules;

			m_listValues = new List<double>();
			m_operators = new List<string>();
		}

		public void ProcessCaculate()
		{
			m_listValues = new List<double>();
			Stack<double> _stackProcessing = new Stack<double>();

			for (int i = 0; i < Statics.ATTRIBUTE.Length; i++)
			{
				m_listValues.Add(Statics.NOT_RELATE);

				if (m_attributesInfo[i].m_value != null && m_attributesInfo[i].m_value != "?")
					m_listValues[i] = double.Parse(m_attributesInfo[i].m_value);
			}


			for (int i = 0; i < m_listRulesUsed.Count; i++)
			{
				string _postfixFomular = ConvertToPostfixExpression(m_listRulesUsed[i]);
				int _currentPosition = 0;

				for (int j = 0; j < _postfixFomular.Length; j++)
				{
					string _currentExpress = "";

					if (_postfixFomular[j] == ' ')
					{
						_currentExpress = _postfixFomular.Substring(_currentPosition, j - _currentPosition);
						_currentPosition = j + 1;

						switch (_currentExpress)
						{
							case "90":
								_stackProcessing.Push(90);
								break;
							case "2":
								_stackProcessing.Push(2);
								break;
							case "+":
								_stackProcessing.Push(_stackProcessing.Pop() + _stackProcessing.Pop());
								break;
							case "-":
								_stackProcessing.Push(-_stackProcessing.Pop() + _stackProcessing.Pop());
								break;
							case "*":
								_stackProcessing.Push(_stackProcessing.Pop() * _stackProcessing.Pop());
								break;
							case "/":
								_stackProcessing.Push((1 / _stackProcessing.Pop()) * _stackProcessing.Pop());
								break;
							case "sqrt":
								_stackProcessing.Push(Math.Sqrt(_stackProcessing.Pop()));
								break;
							default:
								int _index = System.Array.FindIndex(Statics.ATTRIBUTE, item => item == _currentExpress);
								_stackProcessing.Push(m_listValues[_index]);
								break;
						}
					}
				}

				for (int j = 0; j < Statics.ATTRIBUTE.Length; j++)
				{
					if (m_listRules[m_listRulesUsed[i]][j] == 1)
					{
						m_listValues[j] = Math.Round(_stackProcessing.Pop(), 2);
						Console.WriteLine(m_listValues[j]);
						break;
					}
				}
			}
		}

		private string ConvertToPostfixExpression(int expressionIndex)
		{
			Stack<string> _stackStr = new Stack<string>();

			string _infixExpression = File.ReadLines(Statics.RULES_DIRECTORY).Skip(expressionIndex).Take(1).First();
			string _postfixExpression = "";

			_infixExpression = _infixExpression.Substring(_infixExpression.IndexOf(Statics.RULED_DELIMITER) + 1);

			int _currentPosition = 0;

			for (int i = 0; i < _infixExpression.Length; i++)
			{
				if (_infixExpression[i] == ' ' || i == _infixExpression.Length - 1)
				{
					string _currentElement = "";

					if (_infixExpression.Length - 1 != i)
						_currentElement = _infixExpression.Substring(_currentPosition, i - _currentPosition);
					else
						_currentElement = _infixExpression.Substring(_currentPosition, i - _currentPosition + 1);

					_currentPosition = i + 1;

					switch (_currentElement)
					{
						case "+":
						case "-":
							while (_stackStr.Count != 0 && _stackStr.Peek() != "(")
								_postfixExpression += _stackStr.Pop() + " ";
							_stackStr.Push(_currentElement);
							break;
						case "*":
						case "/":
							while (_stackStr.Count != 0)
							{
								if (_stackStr.Peek() == "*" || _stackStr.Peek() == "/" ||
									_stackStr.Peek() == "sin" || _stackStr.Peek() == "cos" ||
									_stackStr.Peek() == "arcsin" || _stackStr.Peek() == "sqrt")
									_postfixExpression += _stackStr.Pop() + " ";
								else
									break;
							}
							_stackStr.Push(_currentElement);
							break;
						case "sqrt":
							if (_stackStr.Count != 0)
							{
								while (_stackStr.Peek() == "sqrt")
									_postfixExpression += _stackStr.Pop() + " ";
							}
							_stackStr.Push(_currentElement);
							break;
						case "(":
							_stackStr.Push(_currentElement);
							break;
						case ")":
							if (_stackStr.Count != 0)
							{
								while (_stackStr.Peek() != "(")
									_postfixExpression += _stackStr.Pop() + " ";
								_stackStr.Pop();
							}
							break;
						default:
							_postfixExpression += _currentElement + " ";
							break;
					}
				}
			}
			while (_stackStr.Count != 0)
				_postfixExpression += _stackStr.Pop() + " ";

			return _postfixExpression;
		}

		public static List<int> ForwardChaining(List<List<int>> rulesList, List<int> knownList, int conclusion)
		{
			List<int> _listRulesUsed = new List<int>();

			List<int> _rulesListState = new List<int>();
			for (int i = 0; i < rulesList.Count; i++)
			{
				_rulesListState.Add(Statics.NOT_USED_YET);
			}

			while (knownList[conclusion] == Statics.NOT_RELATE)
			{
				bool _executed = false;

				for (int i = 0; i < rulesList.Count; i++)
				{
					if (_rulesListState[i] == Statics.NOT_USED_YET)
					{
						bool _canUseThis = true;

						for (int j = 0; j < Statics.ATTRIBUTE.Length; j++)
						{
							if ((rulesList[i][j] == Statics.IN_ASSUMPTIONS && knownList[j] != Statics.IN_ASSUMPTIONS) ||
								(rulesList[i][j] == Statics.IN_CONCLUSION && knownList[j] == Statics.IN_ASSUMPTIONS))
							{
								_canUseThis = false;
								break;
							}
						}

						if (_canUseThis)
						{
							for (int j = 0; j < Statics.ATTRIBUTE.Length; j++)
							{
								if (rulesList[i][j] == Statics.IN_CONCLUSION)
								{
									knownList[j] = Statics.IN_ASSUMPTIONS;
									break;
								}
							}

							_executed = true;
							_listRulesUsed.Add(i);

							if (knownList[conclusion] == Statics.IN_ASSUMPTIONS)
								break;

							_rulesListState[i] = Statics.USED;
						}
					}
				}

				if (!_executed)
				{
					_listRulesUsed.Clear();
					break;
				}
			}

			_listRulesUsed = OptimizeListRulesUsed(_listRulesUsed, rulesList);

			return _listRulesUsed;
		}

		private static List<int> OptimizeListRulesUsed(List<int> listRulesUsed, List<List<int>> rulesList)
		{
			if (listRulesUsed.Count == 0)
				return listRulesUsed;

			List<int> _listRulesOptimized = new List<int>();
			int _currentRulePosition = listRulesUsed.Count - 1;

			_listRulesOptimized.Add(listRulesUsed[_currentRulePosition]);

			for (int i = listRulesUsed.Count - 2; i >= 0; i--)
			{
				for (int j = 0; j < Statics.ATTRIBUTE.Length; j++)
				{
					if (rulesList[listRulesUsed[i]][j] == Statics.IN_CONCLUSION &&
						rulesList[_listRulesOptimized[_listRulesOptimized.Count - 1]][j] == Statics.IN_ASSUMPTIONS)
					{
						_currentRulePosition = i;
						_listRulesOptimized.Add(listRulesUsed[_currentRulePosition]);

						break;
					}
				}
			}

			_listRulesOptimized.Reverse();
			return _listRulesOptimized;
		}
	}
}
