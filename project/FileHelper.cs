using System.Collections.Generic;
using System.IO;

namespace RectangleSolver
{
	public class FileHelper
	{
		public static List<List<int>> LoadRules(string rulesDirectory)
		{
			List<List<int>> _rulesList = new List<List<int>>();

			StreamReader _stream = new StreamReader(rulesDirectory);
			string _ruleStr = null;

			while ((_ruleStr = _stream.ReadLine()) != null)
			{
				_rulesList.Add(AddRule(_ruleStr));
			}

			_stream.Dispose();

			return _rulesList;
		}

		private static List<int> AddRule(string ruleStr)
		{
			List<int> _rule = new List<int>(Statics.ATTRIBUTE.Length);

			for (int i = 0; i < Statics.ATTRIBUTE.Length; i++)
				_rule.Add(Statics.NOT_RELATE);

			int _currentPosition = 0;
			int _currentAttributeLength = 0;

			int _state = Statics.IN_ASSUMPTIONS;

			while (ruleStr[_currentPosition == 0 ? _currentPosition : _currentPosition - 1] != Statics.RULED_DELIMITER)
			{
				if (ruleStr[_currentPosition] >= 'A' && ruleStr[_currentPosition] <= 'z')
				{
					_currentAttributeLength++;
				}
				else if (_currentPosition > 0)
				{
					if (ruleStr[_currentPosition - 1] == '-' && ruleStr[_currentPosition] == '>')
						_state = Statics.IN_CONCLUSION;

					if (_currentAttributeLength != 0)
					{
						string _attributeStr = ruleStr.Substring(_currentPosition - _currentAttributeLength, _currentAttributeLength);

						int _index = System.Array.FindIndex(Statics.ATTRIBUTE, item => item == _attributeStr);
						_rule[_index] = _state;

						_currentAttributeLength = 0;
					}
				}

				_currentPosition++;
			}

			return _rule;
		}
	}
}
