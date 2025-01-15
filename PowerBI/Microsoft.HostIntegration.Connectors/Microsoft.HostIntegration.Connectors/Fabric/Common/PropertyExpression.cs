using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003FD RID: 1021
	internal class PropertyExpression
	{
		// Token: 0x060023BC RID: 9148 RVA: 0x0006D6E1 File Offset: 0x0006B8E1
		public PropertyExpression(PropertyFunc func)
		{
			this.m_func = func;
			this.m_children = PropertyExpression.s_nullChildren;
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x060023BD RID: 9149 RVA: 0x0006D6FB File Offset: 0x0006B8FB
		public PropertyFunc Func
		{
			get
			{
				return this.m_func;
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x060023BE RID: 9150 RVA: 0x0006D703 File Offset: 0x0006B903
		public bool IsLiteral
		{
			get
			{
				return this.m_func is Literal;
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x060023BF RID: 9151 RVA: 0x0006D713 File Offset: 0x0006B913
		public IList<PropertyExpression> Children
		{
			get
			{
				return this.m_children;
			}
		}

		// Token: 0x060023C0 RID: 9152 RVA: 0x0006D71B File Offset: 0x0006B91B
		public void AddChild(PropertyExpression child)
		{
			if (child == null)
			{
				throw new ArgumentNullException("child");
			}
			if (this.m_children == PropertyExpression.s_nullChildren)
			{
				this.m_children = new FuncArguments();
			}
			this.m_children.Add(child);
		}

		// Token: 0x060023C1 RID: 9153 RVA: 0x0006D750 File Offset: 0x0006B950
		public object Eval(IReadablePropertyContext context)
		{
			object obj;
			try
			{
				obj = this.m_func.Invoke(context, this.m_children);
			}
			catch (Exception ex)
			{
				throw new ExpressionException("Error evaluating the expression", ex);
			}
			return obj;
		}

		// Token: 0x060023C2 RID: 9154 RVA: 0x0006D790 File Offset: 0x0006B990
		public void Bind()
		{
			bool flag = true;
			if (this.m_children != PropertyExpression.s_nullChildren)
			{
				for (int i = 0; i < this.m_children.Count; i++)
				{
					this.m_children[i].Bind();
					flag = flag && this.m_children[i].m_func is Literal;
				}
				this.m_func = this.m_func.Bind(this.m_children);
			}
			if (flag && this.m_func is StandaloneFunc && !(this.m_func is Literal))
			{
				this.m_func = new Literal(this.Eval(null));
			}
			if (this.m_func is Literal)
			{
				this.m_children = PropertyExpression.s_nullChildren;
			}
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x0006D854 File Offset: 0x0006BA54
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			stringBuilder.AppendFormat("{0}", this.m_func);
			if (this.m_children.Count != 0)
			{
				stringBuilder.Append("(");
				foreach (PropertyExpression propertyExpression in this.m_children)
				{
					stringBuilder.AppendFormat("{0},", propertyExpression);
				}
				stringBuilder.Length--;
				stringBuilder.Append(")");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060023C4 RID: 9156 RVA: 0x0006D904 File Offset: 0x0006BB04
		private static List<PropertyExpression.Operator> CreateOperators()
		{
			List<PropertyExpression.Operator> list = new List<PropertyExpression.Operator>();
			PropertyExpression.SetOperator(list, new PropertyExpression.Operator("(", Literal.Empty, 1));
			PropertyExpression.SetOperator(list, new PropertyExpression.Operator(")", Literal.Empty, 1));
			PropertyExpression.SetOperator(list, new PropertyExpression.Operator("||", OrFunc.Singleton, 2));
			PropertyExpression.SetOperator(list, new PropertyExpression.Operator("&&", AndFunc.Singleton, 3));
			PropertyExpression.SetOperator(list, new PropertyExpression.Operator("=", ComparisonFunc.EQ, 4));
			PropertyExpression.SetOperator(list, new PropertyExpression.Operator("!=", ComparisonFunc.NE, 4));
			PropertyExpression.SetOperator(list, new PropertyExpression.Operator(">", ComparisonFunc.GT, 4));
			PropertyExpression.SetOperator(list, new PropertyExpression.Operator("<", ComparisonFunc.LT, 4));
			PropertyExpression.SetOperator(list, new PropertyExpression.Operator(">=", ComparisonFunc.GE, 4));
			PropertyExpression.SetOperator(list, new PropertyExpression.Operator("<=", ComparisonFunc.LE, 4));
			PropertyExpression.SetOperator(list, new PropertyExpression.Operator("~", MatchFunc.Singleton, 4));
			PropertyExpression.SetOperator(list, new PropertyExpression.Operator("!", NotFunc.Singleton, 6));
			PropertyExpression.SetOperator(list, new PropertyExpression.Operator("%", GetFunc.Singleton, 6));
			return list;
		}

		// Token: 0x060023C5 RID: 9157 RVA: 0x0006DA38 File Offset: 0x0006BC38
		private static void SetOperator(List<PropertyExpression.Operator> table, PropertyExpression.Operator propertyOperator)
		{
			int length = propertyOperator.Op.Length;
			int num = 0;
			while (num < table.Count && length <= table[num].Op.Length)
			{
				if (table[num].Op == propertyOperator.Op)
				{
					table[num] = propertyOperator;
					return;
				}
				num++;
			}
			table.Insert(num, propertyOperator);
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x0006DAA0 File Offset: 0x0006BCA0
		public static void SetOperator(PropertyExpression.Operator propertyOperator)
		{
			lock (PropertyExpression.s_sentinel)
			{
				List<PropertyExpression.Operator> list = new List<PropertyExpression.Operator>(PropertyExpression.s_operators);
				PropertyExpression.SetOperator(list, propertyOperator);
				PropertyExpression.s_operators = list;
			}
		}

		// Token: 0x060023C7 RID: 9159 RVA: 0x0006DAEC File Offset: 0x0006BCEC
		public static PropertyExpression.Operator GetOperator(PropertyFunc func)
		{
			foreach (PropertyExpression.Operator @operator in PropertyExpression.s_operators)
			{
				if (@operator.Func == func)
				{
					return @operator;
				}
			}
			return null;
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x0006DB48 File Offset: 0x0006BD48
		private static PropertyExpression.Operator ReadOperator(string exp, ref int start)
		{
			foreach (PropertyExpression.Operator @operator in PropertyExpression.s_operators)
			{
				if (@operator.Match(exp, ref start))
				{
					return @operator;
				}
			}
			return null;
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x0006DBA4 File Offset: 0x0006BDA4
		private static PropertyExpression ReadFunc(string exp, string name, ref int start)
		{
			PropertyFunc propertyFunc = PropertyFunc.Get(name);
			if (propertyFunc == null)
			{
				throw new ArgumentException("function " + name + " not found");
			}
			PropertyExpression propertyExpression = new PropertyExpression(propertyFunc);
			int num = 0;
			for (int i = start; i < exp.Length; i++)
			{
				char c = exp[i];
				if (c == '(')
				{
					num++;
				}
				else if (c == '"' && i + 1 < exp.Length)
				{
					int num2 = exp.IndexOf('"', i + 1);
					if (num2 > 0)
					{
						i = num2;
					}
				}
				else if ((c == ',' || c == ')') && num == 0)
				{
					string text = exp.Substring(start, i - start).Trim();
					PropertyExpression propertyExpression2 = PropertyExpression.Build(text);
					propertyExpression.AddChild(propertyExpression2);
					start = i + 1;
				}
				if (c == ')')
				{
					if (num == 0)
					{
						start = i + 1;
						return propertyExpression;
					}
					num--;
				}
			}
			throw new ArgumentException("Invalid function expression: " + exp);
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x0006DC8C File Offset: 0x0006BE8C
		private static PropertyExpression ReadToken(string exp, ref int start)
		{
			if (start >= exp.Length)
			{
				return null;
			}
			int i;
			for (i = start; i < exp.Length; i++)
			{
				char c = exp[i];
				if (!char.IsLetterOrDigit(c))
				{
					if (c == '"' && i + 1 < exp.Length)
					{
						int num = exp.IndexOf('"', i + 1);
						if (num > 0)
						{
							i = num;
						}
					}
					else
					{
						if (c == '(')
						{
							string text = exp.Substring(start, i - start).Trim();
							if (text.Length > 0)
							{
								start = i + 1;
								return PropertyExpression.ReadFunc(exp, text, ref start);
							}
						}
						int num2 = i;
						if (PropertyExpression.ReadOperator(exp, ref num2) != null)
						{
							break;
						}
					}
				}
			}
			string text2 = exp.Substring(start, i - start).Trim();
			start = i;
			if (text2.Length == 0)
			{
				return null;
			}
			text2 = text2.Replace("\"", string.Empty);
			return new PropertyExpression(new Literal(text2));
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x0006DD68 File Offset: 0x0006BF68
		private static void Process(Stack<PropertyExpression.Operator> operators, Stack<PropertyExpression> tokens)
		{
			PropertyExpression.Operator @operator = operators.Pop();
			PropertyExpression propertyExpression = new PropertyExpression(@operator.Func);
			if (tokens.Count < 2)
			{
				throw new ArgumentException("Expression not well formed");
			}
			PropertyExpression propertyExpression2 = tokens.Pop();
			PropertyExpression propertyExpression3 = tokens.Pop();
			if (propertyExpression3 != null)
			{
				propertyExpression.AddChild(propertyExpression3);
			}
			if (propertyExpression2 != null)
			{
				propertyExpression.AddChild(propertyExpression2);
			}
			tokens.Push(propertyExpression);
		}

		// Token: 0x060023CC RID: 9164 RVA: 0x0006DDC8 File Offset: 0x0006BFC8
		private static PropertyExpression PrivateBuild(string exp, bool canBind)
		{
			Stack<PropertyExpression.Operator> stack = new Stack<PropertyExpression.Operator>();
			Stack<PropertyExpression> stack2 = new Stack<PropertyExpression>();
			stack.Push(PropertyExpression.s_sentinel);
			int num = 0;
			for (bool flag = true; flag || num < exp.Length; flag = !flag)
			{
				if (flag)
				{
					PropertyExpression propertyExpression = PropertyExpression.ReadToken(exp, ref num);
					stack2.Push(propertyExpression);
				}
				else
				{
					while (num < exp.Length && char.IsWhiteSpace(exp[num]))
					{
						num++;
					}
					PropertyExpression.Operator @operator = PropertyExpression.ReadOperator(exp, ref num);
					if (@operator == null)
					{
						string text = string.Format(CultureInfo.InvariantCulture, "Expression {0} not well formed. Unrecognized operator: {1}", new object[]
						{
							exp,
							exp.Substring(num)
						});
						throw new ArgumentException(text);
					}
					if (@operator.Op == "(")
					{
						ReleaseAssert.IsTrue<string>(stack2.Pop() == null, "Expression: {0}", exp);
						stack.Push(@operator);
					}
					else if (@operator.Op == ")")
					{
						while (stack.Peek().Op != "(")
						{
							PropertyExpression.Process(stack, stack2);
						}
						stack.Pop();
						flag = true;
					}
					else
					{
						while (stack.Peek().Level >= @operator.Level)
						{
							PropertyExpression.Process(stack, stack2);
						}
						stack.Push(@operator);
					}
				}
			}
			while (stack.Count > 1)
			{
				PropertyExpression.Process(stack, stack2);
			}
			ReleaseAssert.IsTrue<string>(stack2.Count == 1, "Expression: {0}", exp);
			PropertyExpression propertyExpression2 = stack2.Pop();
			if (canBind)
			{
				propertyExpression2.Bind();
			}
			return propertyExpression2;
		}

		// Token: 0x060023CD RID: 9165 RVA: 0x0006DF4E File Offset: 0x0006C14E
		public static PropertyExpression Build(string exp)
		{
			return PropertyExpression.Build(exp, true);
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x0006DF58 File Offset: 0x0006C158
		public static PropertyExpression Build(string exp, bool canBind)
		{
			PropertyExpression propertyExpression;
			try
			{
				propertyExpression = PropertyExpression.PrivateBuild(exp, canBind);
			}
			catch (Exception ex)
			{
				throw new ArgumentException("Invalid expression: " + exp, ex);
			}
			return propertyExpression;
		}

		// Token: 0x04001631 RID: 5681
		private PropertyFunc m_func;

		// Token: 0x04001632 RID: 5682
		private FuncArguments m_children;

		// Token: 0x04001633 RID: 5683
		private static readonly FuncArguments s_nullChildren = new FuncArguments();

		// Token: 0x04001634 RID: 5684
		private static readonly PropertyExpression.Operator s_sentinel = new PropertyExpression.Operator("Dummy", Literal.Empty, 0);

		// Token: 0x04001635 RID: 5685
		private static List<PropertyExpression.Operator> s_operators = PropertyExpression.CreateOperators();

		// Token: 0x020003FE RID: 1022
		internal class Operator
		{
			// Token: 0x060023D0 RID: 9168 RVA: 0x0006DFBF File Offset: 0x0006C1BF
			public Operator(string op, PropertyFunc func, int level)
			{
				this.m_op = op;
				this.m_func = func;
				this.m_level = level;
			}

			// Token: 0x17000731 RID: 1841
			// (get) Token: 0x060023D1 RID: 9169 RVA: 0x0006DFDC File Offset: 0x0006C1DC
			public string Op
			{
				get
				{
					return this.m_op;
				}
			}

			// Token: 0x17000732 RID: 1842
			// (get) Token: 0x060023D2 RID: 9170 RVA: 0x0006DFE4 File Offset: 0x0006C1E4
			public PropertyFunc Func
			{
				get
				{
					return this.m_func;
				}
			}

			// Token: 0x17000733 RID: 1843
			// (get) Token: 0x060023D3 RID: 9171 RVA: 0x0006DFEC File Offset: 0x0006C1EC
			public int Level
			{
				get
				{
					return this.m_level;
				}
			}

			// Token: 0x060023D4 RID: 9172 RVA: 0x0006DFF4 File Offset: 0x0006C1F4
			public bool Match(string exp, ref int start)
			{
				if (start + this.m_op.Length > exp.Length)
				{
					return false;
				}
				for (int i = 0; i < this.m_op.Length; i++)
				{
					if (this.m_op[i] != exp[start + i])
					{
						return false;
					}
				}
				start += this.m_op.Length;
				return true;
			}

			// Token: 0x060023D5 RID: 9173 RVA: 0x0006DFDC File Offset: 0x0006C1DC
			public override string ToString()
			{
				return this.m_op;
			}

			// Token: 0x04001636 RID: 5686
			private string m_op;

			// Token: 0x04001637 RID: 5687
			private PropertyFunc m_func;

			// Token: 0x04001638 RID: 5688
			private int m_level;
		}
	}
}
