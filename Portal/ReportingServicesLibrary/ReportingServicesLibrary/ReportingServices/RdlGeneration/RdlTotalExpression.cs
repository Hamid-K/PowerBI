using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.RdlGeneration
{
	// Token: 0x0200037F RID: 895
	internal class RdlTotalExpression
	{
		// Token: 0x06001D89 RID: 7561 RVA: 0x00078411 File Offset: 0x00076611
		public RdlTotalExpression(Expression originalExpression)
			: this(originalExpression, RdlTotalExpression.DecompositionMode.Normal)
		{
		}

		// Token: 0x06001D8A RID: 7562 RVA: 0x0007841B File Offset: 0x0007661B
		public RdlTotalExpression(Expression originalExpression, bool forceServerTotals)
			: this(originalExpression, forceServerTotals ? RdlTotalExpression.DecompositionMode.ForceServerTotals : RdlTotalExpression.DecompositionMode.Normal)
		{
		}

		// Token: 0x06001D8B RID: 7563 RVA: 0x0007842B File Offset: 0x0007662B
		private RdlTotalExpression(Expression originalExpression, RdlTotalExpression.DecompositionMode mode)
		{
			this.m_originalExpression = originalExpression;
			this.m_mode = mode;
			this.Update();
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06001D8C RID: 7564 RVA: 0x00078459 File Offset: 0x00076659
		public Expression OriginalExpression
		{
			get
			{
				return this.m_originalExpression;
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06001D8D RID: 7565 RVA: 0x00078464 File Offset: 0x00076664
		public bool CanTotal
		{
			get
			{
				return this.m_originalExpression != null && this.m_originalExpression.TryGetResultType(true) != null && !this.m_originalExpression.IsSubtreeAnchored();
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06001D8E RID: 7566 RVA: 0x0007849F File Offset: 0x0007669F
		public bool CanTotalInRdl
		{
			get
			{
				return !this.m_decomposeFailed;
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06001D8F RID: 7567 RVA: 0x000784AA File Offset: 0x000766AA
		public bool IsDecomposed
		{
			get
			{
				return this.m_inputExpressions.Count == 0 || (this.m_inputExpressions.Count > 0 && this.m_inputExpressions[0] != this.m_originalExpression);
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06001D90 RID: 7568 RVA: 0x000784E2 File Offset: 0x000766E2
		public bool AllowDecomposeVolatile
		{
			get
			{
				return this.m_mode == RdlTotalExpression.DecompositionMode.ConstantsOnly;
			}
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x000784F0 File Offset: 0x000766F0
		public IList<Expression> GetQueryExpressions(bool usingTotals)
		{
			List<Expression> list = new List<Expression>();
			if (usingTotals)
			{
				list.AddRange(this.m_inputExpressions);
			}
			else
			{
				list.Add(this.m_originalExpression);
			}
			return list;
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x00078524 File Offset: 0x00076724
		public string BuildRdlExpression(RdlUtility rdlUtil, bool usingTotals, bool forTotalScope, object rdlScopeKey)
		{
			if (!usingTotals)
			{
				return rdlUtil.FieldRdlSubExpression(this.m_originalExpression);
			}
			if (!this.CanTotal)
			{
				throw new InvalidOperationException();
			}
			string text = ((rdlScopeKey == null) ? "" : (", \"" + rdlUtil.GetRdlScopeName(rdlScopeKey) + "\""));
			object[] array = new object[this.m_inputExpressions.Count];
			for (int i = 0; i < this.m_inputExpressions.Count; i++)
			{
				array[i] = rdlUtil.FieldRdlSubExpression(this.m_inputExpressions[i]) + text;
			}
			string text2;
			if (this.CanTotalInRdl)
			{
				text2 = (forTotalScope ? this.m_totalExpressionTemplate : this.m_detailExpressionTemplate);
			}
			else
			{
				text2 = (forTotalScope ? "AGGREGATE({0})" : "{0}");
			}
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant(text2, array);
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x000785EC File Offset: 0x000767EC
		public static string DecomposeConstant(Expression constantExpr)
		{
			RdlTotalExpression rdlTotalExpression = new RdlTotalExpression(constantExpr, RdlTotalExpression.DecompositionMode.ConstantsOnly);
			if (rdlTotalExpression.IsDecomposed)
			{
				return rdlTotalExpression.BuildRdlExpression(null, true, false, null);
			}
			return null;
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x00078618 File Offset: 0x00076818
		private void Update()
		{
			this.m_detailExpressionTemplate = null;
			this.m_totalExpressionTemplate = null;
			this.m_inputExpressions.Clear();
			this.m_decomposeFailed = true;
			if (this.m_originalExpression != null)
			{
				if (this.m_mode != RdlTotalExpression.DecompositionMode.ForceServerTotals && this.m_originalExpression.TryGetResultType(true) != null && !this.m_originalExpression.IsSubtreeAnchored())
				{
					this.m_decomposeFailed = false;
					this.m_totalExpressionTemplate = this.DecomposeCore(null, this.m_originalExpression, false, true);
					if (this.m_mode == RdlTotalExpression.DecompositionMode.ConstantsOnly && this.m_inputExpressions.Count > 0)
					{
						this.m_decomposeFailed = true;
					}
				}
				if (!this.m_decomposeFailed)
				{
					if (this.m_inputExpressions.Count == 0 && this.m_mode != RdlTotalExpression.DecompositionMode.ConstantsOnly)
					{
						this.m_inputExpressions.Add(this.m_originalExpression);
						this.m_totalExpressionTemplate = "FIRST({0})";
						this.m_detailExpressionTemplate = "{0}";
						return;
					}
					this.m_inputExpressions.Clear();
					this.m_detailExpressionTemplate = this.DecomposeCore(null, this.m_originalExpression, false, false);
					if (!this.IsDecomposed)
					{
						return;
					}
					using (List<Expression>.Enumerator enumerator = this.m_inputExpressions.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Expression expression = enumerator.Current;
							expression.Name = this.m_originalExpression.Name + "_" + expression.NodeAsFunction.FunctionName.ToString();
						}
						return;
					}
				}
				this.m_inputExpressions.Clear();
				this.m_inputExpressions.Add(this.m_originalExpression);
				this.m_totalExpressionTemplate = null;
			}
		}

		// Token: 0x06001D95 RID: 7573 RVA: 0x000787C0 File Offset: 0x000769C0
		private string DecomposeCore(AggregationPath aggPath, Expression expr, bool ignorePath, bool asTotal)
		{
			if ((!expr.Path.IsEmpty && !ignorePath) || expr.NodeAsEntityRef != null)
			{
				return this.DecomposeFailed();
			}
			if (expr.NodeAsAttributeRef != null)
			{
				return this.DecomposeAttributeRef(aggPath, expr.NodeAsAttributeRef, asTotal);
			}
			if (expr.NodeAsFunction != null)
			{
				return this.DecomposeFunction(aggPath, expr, asTotal);
			}
			if (expr.NodeAsLiteral != null)
			{
				return this.DecomposeLiteral(expr.NodeAsLiteral);
			}
			if (expr.NodeAsNull != null)
			{
				return "Nothing";
			}
			return this.DecomposeFailed();
		}

		// Token: 0x06001D96 RID: 7574 RVA: 0x00078840 File Offset: 0x00076A40
		private string DecomposeAttributeRef(AggregationPath aggPath, AttributeRefNode attribRef, bool asTotal)
		{
			Expression expression = attribRef.CalculatedAttribute;
			if (expression == null)
			{
				expression = attribRef.ModelAttribute.Expression;
				if (expression == null)
				{
					return this.DecomposeFailed();
				}
			}
			if (expression.IsSubtreeAnchored())
			{
				return this.DecomposeFailed();
			}
			return this.DecomposeCore(aggPath, expression, false, asTotal);
		}

		// Token: 0x06001D97 RID: 7575 RVA: 0x00078888 File Offset: 0x00076A88
		private string DecomposeFunction(AggregationPath aggPath, Expression expr, bool asTotal)
		{
			bool? isAggregate = expr.NodeAsFunction.IsAggregate;
			if (isAggregate == null)
			{
				return this.DecomposeFailed();
			}
			if (isAggregate.Value)
			{
				Expression expression;
				AggregationPath aggregationPath = AggregationPath.FromExpression(expr, out expression);
				if (aggPath != null)
				{
					aggregationPath = AggregationPath.Combine(aggPath, aggregationPath);
				}
				if (aggregationPath.AreSubtotalsNonUnique)
				{
					return this.DecomposeFailed();
				}
				if (expr.NodeAsFunction.FunctionName == FunctionName.Aggregate)
				{
					return this.DecomposeCore(aggregationPath, expression, true, asTotal);
				}
			}
			IList<Expression> arguments = expr.NodeAsFunction.Arguments;
			switch (expr.NodeAsFunction.FunctionName)
			{
			case FunctionName.Add:
				return this.BuildInfix("+", aggPath, arguments, asTotal);
			case FunctionName.Subtract:
				return this.BuildInfix("-", aggPath, arguments, asTotal);
			case FunctionName.Multiply:
				return this.BuildInfix("*", aggPath, arguments, asTotal);
			case FunctionName.Divide:
				return this.BuildInfix("/", aggPath, arguments, asTotal);
			case FunctionName.Negate:
				return this.BuildRdlExpr("(-{0})", aggPath, arguments, asTotal);
			case FunctionName.Mod:
				return this.BuildInfix("Mod", aggPath, arguments, asTotal);
			case FunctionName.Equals:
				return this.BuildInfix("=", aggPath, arguments, asTotal);
			case FunctionName.NotEquals:
				return this.BuildInfix("<>", aggPath, arguments, asTotal);
			case FunctionName.GreaterThan:
				return this.BuildInfix(">", aggPath, arguments, asTotal);
			case FunctionName.GreaterThanOrEquals:
				return this.BuildInfix(">=", aggPath, arguments, asTotal);
			case FunctionName.LessThan:
				return this.BuildInfix("<", aggPath, arguments, asTotal);
			case FunctionName.LessThanOrEquals:
				return this.BuildInfix("<=", aggPath, arguments, asTotal);
			case FunctionName.And:
				return this.BuildInfix("AndAlso", aggPath, arguments, asTotal);
			case FunctionName.Or:
				return this.BuildInfix("OrElse", aggPath, arguments, asTotal);
			case FunctionName.Not:
				return this.BuildRdlExpr("(Not {0})", aggPath, arguments, asTotal);
			case FunctionName.Integer:
				return this.BuildRdlExpr("Convert.ToInt64({0})", aggPath, arguments, asTotal);
			case FunctionName.Decimal:
				return this.BuildRdlExpr("Convert.ToDecimal({0})", aggPath, arguments, asTotal);
			case FunctionName.Float:
				return this.BuildRdlExpr("Convert.ToDouble({0})", aggPath, arguments, asTotal);
			case FunctionName.String:
				return this.BuildRdlExpr("Convert.ToString({0}, System.Globalization.CultureInfo.InvariantCulture)", aggPath, arguments, asTotal);
			case FunctionName.Length:
				return this.BuildRdlExpr("{0}.Length", aggPath, arguments, asTotal);
			case FunctionName.Left:
				return this.BuildRdlExpr("{0}.Substring(0, {1})", aggPath, arguments, asTotal);
			case FunctionName.Concat:
				return this.BuildInfix("&", aggPath, arguments, asTotal);
			case FunctionName.Date:
				if (arguments.Count == 1)
				{
					return this.BuildRdlExpr("{0}.Date", aggPath, arguments, asTotal);
				}
				return this.BuildRdlExpr("(new DateTime({0}, {1}, {2}))", aggPath, arguments, asTotal);
			case FunctionName.DateTime:
				return this.BuildRdlExpr("(new DateTime({0}, {1}, {2}, {3}, {4}, 0).AddSeconds({5}))", aggPath, arguments, asTotal);
			case FunctionName.Year:
				return this.BuildRdlExpr("{0}.Year", aggPath, arguments, asTotal);
			case FunctionName.Quarter:
				return this.BuildRdlExpr("{0}.Month / 3)", aggPath, arguments, asTotal);
			case FunctionName.Month:
				return this.BuildRdlExpr("{0}.Month", aggPath, arguments, asTotal);
			case FunctionName.Day:
				return this.BuildRdlExpr("{0}.Day", aggPath, arguments, asTotal);
			case FunctionName.Hour:
				return this.BuildRdlExpr("{0}.Hour", aggPath, arguments, asTotal);
			case FunctionName.Minute:
				return this.BuildRdlExpr("{0}.Minute", aggPath, arguments, asTotal);
			case FunctionName.Second:
				return this.BuildRdlExpr("{0}.Second", aggPath, arguments, asTotal);
			case FunctionName.Now:
				if (!this.AllowDecomposeVolatile)
				{
					return this.DecomposeFailed();
				}
				return "DateTime.Now";
			case FunctionName.Today:
				if (!this.AllowDecomposeVolatile)
				{
					return this.DecomposeFailed();
				}
				return "DateTime.Today";
			case FunctionName.DateAdd:
				return this.BuildDateAddExpr(aggPath, arguments, asTotal);
			case FunctionName.Sum:
			case FunctionName.Count:
				return this.BuildSimpleInputRef(aggPath, expr, "SUM", asTotal);
			case FunctionName.Avg:
			{
				string text = this.BuildInputRef(this.CreateAggregateExpr(FunctionName.Sum, aggPath, arguments[0]));
				string text2 = this.BuildInputRef(this.CreateAggregateExpr(FunctionName.Count, aggPath, arguments[0]));
				return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant(asTotal ? "IIF(SUM({1})=0,Nothing,SUM({0})/SUM({1}))" : "IIF({1}=0,Nothing,{0}/{1})", new object[] { text, text2 });
			}
			case FunctionName.Max:
				return this.BuildSimpleInputRef(aggPath, expr, "MAX", asTotal);
			case FunctionName.Min:
				return this.BuildSimpleInputRef(aggPath, expr, "MIN", asTotal);
			case FunctionName.CountDistinct:
				return this.DecomposeFailed();
			case FunctionName.If:
				return this.BuildRdlExpr("IIf({0}, {1}, {2})", aggPath, arguments, asTotal);
			case FunctionName.Time:
				return this.BuildRdlExpr("{0}.TimeOfDay", aggPath, arguments, asTotal);
			}
			return this.DecomposeFailed();
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x00078CF8 File Offset: 0x00076EF8
		private string DecomposeLiteral(LiteralNode node)
		{
			if (node.Cardinality == Cardinality.One)
			{
				return this.DecomposeLiteralValue(node.DataType, node.Value);
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in node.ToObjectArray())
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(this.DecomposeLiteralValue(node.DataType, obj));
			}
			Type type = DataTypeMapper.TranslateDataType(node.DataType);
			stringBuilder.Insert(0, "New " + type.Name + "() {");
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x00078DA8 File Offset: 0x00076FA8
		private string DecomposeLiteralValue(DataType dt, object value)
		{
			switch (dt)
			{
			case DataType.String:
				return RdlUtility.GetRdlStringLiteral((string)value).Replace("{", "{{").Replace("}", "}}");
			case DataType.Integer:
				return ((long)value).ToString(CultureInfo.InvariantCulture);
			case DataType.Decimal:
				return ((decimal)value).ToString(CultureInfo.InvariantCulture);
			case DataType.Float:
				return this.GetDoubleString((double)value);
			case DataType.Boolean:
				if (!(bool)value)
				{
					return bool.FalseString;
				}
				return bool.TrueString;
			case DataType.DateTime:
				return "#" + this.GetMinimalVBDate((DateTime)value) + "#";
			case DataType.Binary:
				return this.DecomposeFailed();
			case DataType.EntityKey:
				return this.DecomposeFailed();
			case DataType.Time:
			{
				TimeSpan timeSpan = (TimeSpan)value;
				return string.Format(CultureInfo.InvariantCulture, "(new TimeSpan({0}, {1}, {2}, {3}, {4}))", new object[] { timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds });
			}
			}
			throw new ArgumentException("Unknown literal type!");
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x00078EF8 File Offset: 0x000770F8
		private string BuildSimpleInputRef(AggregationPath aggPath, Expression aggExpr, string rdlFunction, bool asTotal)
		{
			if (aggPath == null)
			{
				aggExpr = aggExpr.Clone();
			}
			else
			{
				Expression expression = aggPath.AddToExpression(aggExpr.NodeAsFunction.Arguments[0]);
				aggExpr = new Expression(new FunctionNode(aggExpr.NodeAsFunction.FunctionName, new Expression[] { expression }));
			}
			string text = this.BuildInputRef(aggExpr);
			if (!asTotal)
			{
				return text;
			}
			return rdlFunction + "(" + text + ")";
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x00078F6A File Offset: 0x0007716A
		private string BuildInputRef(Expression expr)
		{
			this.m_inputExpressions.Add(expr);
			return this.BuildExprRef(this.m_inputExpressions.Count - 1);
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x00078F8B File Offset: 0x0007718B
		private string BuildExprRef(int inputIndex)
		{
			return "{" + inputIndex + "}";
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x00078FA4 File Offset: 0x000771A4
		private string BuildDateAddExpr(AggregationPath aggPath, IList<Expression> args, bool asTotal)
		{
			string valueAsString = args[0].NodeAsLiteral.ValueAsString;
			List<Expression> list = new List<Expression>(args);
			list.RemoveAt(0);
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(valueAsString);
			if (num <= 1230250653U)
			{
				if (num <= 641982345U)
				{
					if (num != 358540348U)
					{
						if (num == 641982345U)
						{
							if (valueAsString == "Minute")
							{
								return this.BuildRdlExpr("{1}.AddMinutes({0})", aggPath, list, asTotal);
							}
						}
					}
					else if (valueAsString == "Year")
					{
						return this.BuildRdlExpr("{1}.AddYears({0})", aggPath, list, asTotal);
					}
				}
				else if (num != 909183791U)
				{
					if (num == 1230250653U)
					{
						if (valueAsString == "Day")
						{
							return this.BuildRdlExpr("{1}.AddDays({0})", aggPath, list, asTotal);
						}
					}
				}
				else if (valueAsString == "Hour")
				{
					return this.BuildRdlExpr("{1}.AddHours({0})", aggPath, list, asTotal);
				}
			}
			else if (num <= 2322310791U)
			{
				if (num != 1441981053U)
				{
					if (num == 2322310791U)
					{
						if (valueAsString == "Quarter")
						{
							return this.BuildRdlExpr("{1}.AddMonths({0} * 3)", aggPath, list, asTotal);
						}
					}
				}
				else if (valueAsString == "Week")
				{
					return this.BuildRdlExpr("{1}.AddDays({0} * 7)", aggPath, list, asTotal);
				}
			}
			else if (num != 2826312613U)
			{
				if (num == 4102469629U)
				{
					if (valueAsString == "Second")
					{
						return this.BuildRdlExpr("{1}.AddSeconds({0})", aggPath, list, asTotal);
					}
				}
			}
			else if (valueAsString == "Month")
			{
				return this.BuildRdlExpr("{1}.AddMonths({0})", aggPath, list, asTotal);
			}
			throw new ArgumentException("Unexpected DateAdd interval");
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x00079162 File Offset: 0x00077362
		private Expression CreateAggregateExpr(FunctionName functionName, AggregationPath aggPath, Expression inputExpr)
		{
			if (aggPath != null)
			{
				inputExpr = aggPath.AddToExpression(inputExpr);
			}
			return new Expression(new FunctionNode(functionName, new Expression[] { inputExpr }));
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x00079188 File Offset: 0x00077388
		private string BuildRdlExpr(string format, AggregationPath aggPath, IList<Expression> args, bool asTotal)
		{
			object[] array = new object[args.Count];
			for (int i = 0; i < args.Count; i++)
			{
				array[i] = this.DecomposeCore(aggPath, args[i], false, asTotal);
			}
			return string.Format(CultureInfo.InvariantCulture, format, array);
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x000791D2 File Offset: 0x000773D2
		private string BuildInfix(string op, AggregationPath aggPath, IList<Expression> args, bool asTotal)
		{
			if (args.Count != 2)
			{
				throw new ArgumentException();
			}
			return this.BuildRdlExpr("({0} " + op + " {1})", aggPath, args, asTotal);
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x00079200 File Offset: 0x00077400
		private string GetMinimalVBDate(DateTime dt)
		{
			TimeSpan timeOfDay = dt.TimeOfDay;
			if (timeOfDay == TimeSpan.Zero)
			{
				return dt.ToString("M/d/yyyy", CultureInfo.InvariantCulture);
			}
			double totalSeconds = timeOfDay.TotalSeconds;
			if (totalSeconds - Math.Truncate(totalSeconds) == 0.0)
			{
				return dt.ToString("M/d/yyyy H:mm:ss", CultureInfo.InvariantCulture);
			}
			return dt.ToString("M/d/yyyy H:mm:ss.fffffff", CultureInfo.InvariantCulture);
		}

		// Token: 0x06001DA2 RID: 7586 RVA: 0x00079270 File Offset: 0x00077470
		private string GetDoubleString(double dbl)
		{
			if (double.IsPositiveInfinity(dbl))
			{
				return "Double.PositiveInfinity";
			}
			if (double.IsNegativeInfinity(dbl))
			{
				return "Double.NegativeInfinity";
			}
			if (double.IsNaN(dbl))
			{
				return "Double.NaN";
			}
			return dbl.ToString("g", CultureInfo.InvariantCulture);
		}

		// Token: 0x06001DA3 RID: 7587 RVA: 0x000792AD File Offset: 0x000774AD
		private string DecomposeFailed()
		{
			this.m_decomposeFailed = true;
			return string.Empty;
		}

		// Token: 0x04000C6F RID: 3183
		private readonly Expression m_originalExpression;

		// Token: 0x04000C70 RID: 3184
		private readonly RdlTotalExpression.DecompositionMode m_mode;

		// Token: 0x04000C71 RID: 3185
		private string m_detailExpressionTemplate;

		// Token: 0x04000C72 RID: 3186
		private string m_totalExpressionTemplate;

		// Token: 0x04000C73 RID: 3187
		private readonly List<Expression> m_inputExpressions = new List<Expression>();

		// Token: 0x04000C74 RID: 3188
		private bool m_decomposeFailed = true;

		// Token: 0x02000505 RID: 1285
		private enum DecompositionMode
		{
			// Token: 0x04001229 RID: 4649
			Normal,
			// Token: 0x0400122A RID: 4650
			ForceServerTotals,
			// Token: 0x0400122B RID: 4651
			ConstantsOnly
		}
	}
}
