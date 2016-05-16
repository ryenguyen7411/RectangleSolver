using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RectangleSolver
{
	public partial class MainForm : Form
    {
		public List<int> m_listRulesUsed = new List<int>();


		// Assumptions and conclusion info
		List<Attribute> m_attributesInfo = new List<Attribute>();

		// Assumptions
		List<int> m_assumptions;
		public List<int> Assumptions
		{
			get { return m_assumptions; }
			set { m_assumptions = value; }
		}

		// Conclusion
		int m_conclusion;
		public int Conclusion
		{
			get { return m_conclusion; }
			set { m_conclusion = value; }
		}


		// Store all Rules in text file
		List<List<int>> m_rulesList;
		public List<List<int>> RulesList
		{
			get { return m_rulesList; }
			set { m_rulesList = value; }
		}


		Processor m_processors;
		public Processor Processors
		{
			get { return m_processors; }
			set { m_processors = value; }
		}

		Stdout m_stdouts;
		public Stdout Stdouts
		{
			get { return m_stdouts; }
			set { m_stdouts = value; }
		}


		public MainForm()
        {
            InitializeComponent();
			m_rulesList = FileHelper.LoadRules(Statics.RULES_DIRECTORY);

			m_assumptions = new List<int>();

			for (int i = 0; i < Statics.ATTRIBUTE.Length; i++)
			{
				Attribute _attribute = new Attribute();
				_attribute.Init(i, null);

				m_attributesInfo.Add(_attribute);
			}

			m_attributesInfo[0].m_value = "90";
			m_attributesInfo[1].m_value = "90";
			m_attributesInfo[2].m_value = "90";
			m_attributesInfo[3].m_value = "90";
		}

		private void btn_add_attribute_Click(object sender, EventArgs e)
		{
			AddAttribute();
		}

		private void btn_solve_Click(object sender, EventArgs e)
		{
			UpdateRequirements();

			m_listRulesUsed.Clear();
			List<int> _knownList = new List<int>(m_assumptions);

			if (m_conclusion != -1)
			{
				m_listRulesUsed = Processor.ForwardChaining(m_rulesList, _knownList, m_conclusion);
			}

			if (m_listRulesUsed.Count > 0)
			{
				m_processors = new Processor(m_attributesInfo, RulesList, m_listRulesUsed);
				m_processors.ProcessCaculate();

				Stdouts = new Stdout(Processors, Assumptions, Conclusion);
				Stdouts.Out();

				txt_result.Text = Stdouts.m_results;
			}
			else
			{
				MessageBox.Show("Dữ liệu đầu vào không đủ, vui lòng cung cấp thêm để thực hiện bài toán!", "Warning");
			}
		}

		private void AddAttribute()
		{
			float n;
			bool isNumeric = float.TryParse(txt_value.Text, out n);

			try
			{
				if (txt_value.Text != "" && (isNumeric == true || txt_value.Text == "?"))
				{
					if (m_attributesInfo[cbb_attributes.SelectedIndex + 4].m_value == null)
					{
						m_attributesInfo[cbb_attributes.SelectedIndex + 4].m_value = txt_value.Text;
						string _item = cbb_attributes.SelectedItem.ToString() + " = " + txt_value.Text;

						txt_info.Text += _item + Environment.NewLine;
						txt_value.Text = "";
					}
					else
					{
						MessageBox.Show("Change value of attribute is not implement!");
					}
				}
				else if (!isNumeric)
				{
					if (m_attributesInfo[cbb_attributes.SelectedIndex + 4].m_value == null)
					{
						string _postfixFomular = Processor.ConvertToPostfixExpression(txt_value.Text);
						int _currentPosition = 0;

						Stack<double> _stackProcessing = new Stack<double>();

						for (int j = 0; j < _postfixFomular.Length; j++)
						{
							string _currentExpress = "";

							if (_postfixFomular[j] == ' ')
							{
								_currentExpress = _postfixFomular.Substring(_currentPosition, j - _currentPosition);
								_currentPosition = j + 1;

								switch (_currentExpress)
								{
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
										int _temp;
										int.TryParse(_currentExpress, out _temp);

										if (_temp == 0)
										{
											int _index = System.Array.FindIndex(Statics.ATTRIBUTE, item => item == _currentExpress);
											int.TryParse(m_attributesInfo[_index].m_value, out _temp);
										}
										_stackProcessing.Push(_temp);

										break;
								}
							}
						}

						m_attributesInfo[cbb_attributes.SelectedIndex + 4].m_value = Math.Round(_stackProcessing.Pop(), 2).ToString();
						string _item = cbb_attributes.SelectedItem.ToString() + " = " + txt_value.Text;

						txt_info.Text += _item + Environment.NewLine;
						txt_value.Text = "";
					}
					else
					{
						MessageBox.Show("Change value of attribute is not implement!");
					}

					//MessageBox.Show("Value must be number!");
				}
			}
			catch
			{
				MessageBox.Show("Invalid input. Please check your input!");
			}
		}

		private void UpdateRequirements()
		{
			m_assumptions.Clear();

			for (int i = 0; i < Statics.ATTRIBUTE.Length; i++)
			{
				if (m_attributesInfo[i].m_value != null && m_attributesInfo[i].m_value != "?")
				{
					m_assumptions.Add(Statics.IN_ASSUMPTIONS);
				}
				else
				{
					m_assumptions.Add(Statics.NOT_RELATE);
					if (m_attributesInfo[i].m_value == "?")
					{
						m_conclusion = i;
					}
				}
			}
		}

		private void txt_value_KeyDown(object sender, KeyEventArgs e)
		{
			if ((e.KeyCode & Keys.Enter) == Keys.Enter)
			{
				AddAttribute();
			}
		}
	}
}
