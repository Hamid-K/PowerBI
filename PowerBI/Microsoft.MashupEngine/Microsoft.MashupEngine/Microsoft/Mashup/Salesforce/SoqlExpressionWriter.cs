using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Salesforce
{
	// Token: 0x02000200 RID: 512
	internal class SoqlExpressionWriter
	{
		// Token: 0x06000A63 RID: 2659 RVA: 0x00016FDF File Offset: 0x000151DF
		private static SoqlExpressionWriter.InvocationWriter Static(Token name)
		{
			return new SoqlExpressionWriter.StaticInvocationWriter(name);
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x00016FE7 File Offset: 0x000151E7
		private static SoqlExpressionWriter.InvocationWriter Like(bool wildAtStart, bool wildAtEnd)
		{
			return new SoqlExpressionWriter.LikeInvocationWriter(wildAtStart, wildAtEnd);
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00016FF0 File Offset: 0x000151F0
		public SoqlExpressionWriter(SoqlWriter writer, Keys columns, string keyField)
		{
			this.writer = writer;
			this.columns = columns;
			this.keyField = keyField;
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x0001700D File Offset: 0x0001520D
		public SoqlWriter Writer
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x00017015 File Offset: 0x00015215
		public void Write(SoqlExpression expression)
		{
			this.Write(expression.Expression);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00017024 File Offset: 0x00015224
		private QueryExpression Write(QueryExpression queryExpression)
		{
			switch (queryExpression.Kind)
			{
			case QueryExpressionKind.Binary:
				return this.VisitBinary((BinaryQueryExpression)queryExpression);
			case QueryExpressionKind.Constant:
				return this.VisitConstant((ConstantQueryExpression)queryExpression);
			case QueryExpressionKind.ColumnAccess:
				return this.VisitColumnAccess((ColumnAccessQueryExpression)queryExpression);
			case QueryExpressionKind.If:
				throw new InvalidOperationException();
			case QueryExpressionKind.Invocation:
				return this.VisitInvocation((InvocationQueryExpression)queryExpression);
			case QueryExpressionKind.Unary:
				return this.VisitUnary((UnaryQueryExpression)queryExpression);
			case QueryExpressionKind.ArgumentAccess:
				return this.VisitArgumentAccess((ArgumentAccessQueryExpression)queryExpression);
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x000170B5 File Offset: 0x000152B5
		private QueryExpression VisitArgumentAccess(ArgumentAccessQueryExpression expression)
		{
			this.writer.WriteIdentifier(this.keyField);
			return expression;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x000170CC File Offset: 0x000152CC
		private QueryExpression VisitBinary(BinaryQueryExpression binaryExpression)
		{
			this.writer.Write(SoqlTokens.LeftParen);
			this.Write(binaryExpression.Left);
			BinaryOperator2 @operator = binaryExpression.Operator;
			if (@operator - BinaryOperator2.And <= 1)
			{
				this.writer.WriteSpace();
				this.writer.Write(this.GetToken(binaryExpression.Operator));
				this.writer.WriteSpace();
			}
			else
			{
				this.writer.Write(this.GetToken(binaryExpression.Operator));
			}
			this.Write(binaryExpression.Right);
			this.writer.Write(SoqlTokens.RightParen);
			return binaryExpression;
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00017168 File Offset: 0x00015368
		private QueryExpression VisitConstant(ConstantQueryExpression constantExpression)
		{
			this.VisitConstant(constantExpression.Value);
			return constantExpression;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00017178 File Offset: 0x00015378
		private void VisitConstant(Value value)
		{
			switch (value.Kind)
			{
			case ValueKind.Null:
				this.writer.Write(SoqlTokens.Null);
				return;
			case ValueKind.Date:
				this.writer.WriteDate(value.AsDate.AsClrDateTime);
				return;
			case ValueKind.DateTime:
				this.writer.WriteDateTime(value.AsDateTime.AsClrDateTime);
				return;
			case ValueKind.Number:
				switch (value.AsNumber.NumberKind)
				{
				case NumberKind.Int32:
					this.writer.WriteInt32(value.AsNumber.AsInteger32);
					return;
				case NumberKind.Double:
					this.writer.WriteDouble(value.AsNumber.AsDouble);
					return;
				case NumberKind.Decimal:
					this.writer.WriteDecimal(value.AsNumber.AsDecimal);
					return;
				default:
					return;
				}
				break;
			case ValueKind.Logical:
				this.writer.WriteBool(value.AsBoolean);
				return;
			case ValueKind.Text:
				this.writer.WriteString(value.AsString);
				return;
			case ValueKind.List:
				this.VisitListConstant(value.AsList);
				return;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0001729C File Offset: 0x0001549C
		private void VisitListConstant(ListValue list)
		{
			bool flag = true;
			foreach (IValueReference valueReference in list)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					this.writer.Write(SoqlTokens.Comma);
				}
				this.VisitConstant(valueReference.Value);
			}
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00017304 File Offset: 0x00015504
		private QueryExpression VisitColumnAccess(ColumnAccessQueryExpression columnAccessExpression)
		{
			this.writer.WriteIdentifier(this.columns[columnAccessExpression.Column]);
			return columnAccessExpression;
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00017324 File Offset: 0x00015524
		private QueryExpression VisitInvocation(InvocationQueryExpression invocationExpression)
		{
			Value value;
			if (!invocationExpression.Function.TryGetConstant(out value))
			{
				throw new InvalidOperationException();
			}
			SoqlExpressionWriter.InvocationWriter invocationWriter;
			if (!SoqlExpressionWriter.functions.TryGetValue(value, out invocationWriter))
			{
				throw new InvalidOperationException();
			}
			invocationWriter.Write(this, invocationExpression.Arguments);
			return invocationExpression;
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0001736C File Offset: 0x0001556C
		private QueryExpression VisitUnary(UnaryQueryExpression unaryExpression)
		{
			this.writer.Write(SoqlTokens.LeftParen);
			this.writer.Write(this.GetToken(unaryExpression.Operator));
			this.Write(unaryExpression.Expression);
			this.writer.Write(SoqlTokens.RightParen);
			return unaryExpression;
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x000173BE File Offset: 0x000155BE
		private Token GetToken(UnaryOperator2 op)
		{
			if (op == UnaryOperator2.Not)
			{
				return SoqlTokens.Not;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x000173D0 File Offset: 0x000155D0
		private Token GetToken(BinaryOperator2 op)
		{
			switch (op)
			{
			case BinaryOperator2.GreaterThan:
				return SoqlTokens.GreaterThan;
			case BinaryOperator2.LessThan:
				return SoqlTokens.LessThan;
			case BinaryOperator2.GreaterThanOrEquals:
				return SoqlTokens.GreaterThanOrEquals;
			case BinaryOperator2.LessThanOrEquals:
				return SoqlTokens.LessThanOrEquals;
			case BinaryOperator2.Equals:
				return SoqlTokens.Equals;
			case BinaryOperator2.NotEquals:
				return SoqlTokens.NotEquals;
			case BinaryOperator2.And:
				return SoqlTokens.And;
			case BinaryOperator2.Or:
				return SoqlTokens.Or;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x04000628 RID: 1576
		private static Dictionary<Value, SoqlExpressionWriter.InvocationWriter> functions = new Dictionary<Value, SoqlExpressionWriter.InvocationWriter>
		{
			{
				SoqlFunctionValue.CalendarMonth,
				SoqlExpressionWriter.Static(SoqlTokens.CalendarMonth)
			},
			{
				SoqlFunctionValue.CalendarQuarter,
				SoqlExpressionWriter.Static(SoqlTokens.CalendarQuarter)
			},
			{
				SoqlFunctionValue.CalendarYear,
				SoqlExpressionWriter.Static(SoqlTokens.CalendarYear)
			},
			{
				SoqlFunctionValue.DayInMonth,
				SoqlExpressionWriter.Static(SoqlTokens.DayInMonth)
			},
			{
				SoqlFunctionValue.DayInWeek,
				SoqlExpressionWriter.Static(SoqlTokens.DayInWeek)
			},
			{
				SoqlFunctionValue.DayInYear,
				SoqlExpressionWriter.Static(SoqlTokens.DayInYear)
			},
			{
				SoqlFunctionValue.WeekInMonth,
				SoqlExpressionWriter.Static(SoqlTokens.WeekInMonth)
			},
			{
				SoqlFunctionValue.WeekInYear,
				SoqlExpressionWriter.Static(SoqlTokens.WeekInYear)
			},
			{
				SoqlFunctionValue.HourInDay,
				SoqlExpressionWriter.Static(SoqlTokens.HourInDay)
			},
			{
				SoqlFunctionValue.Avg,
				SoqlExpressionWriter.Static(SoqlTokens.Avg)
			},
			{
				SoqlFunctionValue.Max,
				SoqlExpressionWriter.Static(SoqlTokens.Max)
			},
			{
				SoqlFunctionValue.Min,
				SoqlExpressionWriter.Static(SoqlTokens.Min)
			},
			{
				SoqlFunctionValue.Sum,
				SoqlExpressionWriter.Static(SoqlTokens.Sum)
			},
			{
				SoqlFunctionValue.ListContains,
				new SoqlExpressionWriter.InInvocationWriter()
			},
			{
				SoqlFunctionValue.TextContains,
				SoqlExpressionWriter.Like(true, true)
			},
			{
				SoqlFunctionValue.TextStartsWith,
				SoqlExpressionWriter.Like(false, true)
			},
			{
				SoqlFunctionValue.TextEndsWith,
				SoqlExpressionWriter.Like(true, false)
			},
			{
				SoqlFunctionValue.Count,
				SoqlExpressionWriter.Static(SoqlTokens.Count)
			},
			{
				SoqlFunctionValue.DateTimeToDate,
				SoqlExpressionWriter.Static(SoqlTokens.DayOnly)
			}
		};

		// Token: 0x04000629 RID: 1577
		private readonly SoqlWriter writer;

		// Token: 0x0400062A RID: 1578
		private readonly Keys columns;

		// Token: 0x0400062B RID: 1579
		private readonly string keyField;

		// Token: 0x02000201 RID: 513
		private abstract class InvocationWriter
		{
			// Token: 0x06000A74 RID: 2676
			public abstract void Write(SoqlExpressionWriter writer, IList<QueryExpression> arguments);
		}

		// Token: 0x02000202 RID: 514
		private class StaticInvocationWriter : SoqlExpressionWriter.InvocationWriter
		{
			// Token: 0x06000A76 RID: 2678 RVA: 0x000175D4 File Offset: 0x000157D4
			public StaticInvocationWriter(Token name)
			{
				this.name = name;
			}

			// Token: 0x06000A77 RID: 2679 RVA: 0x000175E4 File Offset: 0x000157E4
			public override void Write(SoqlExpressionWriter writer, IList<QueryExpression> arguments)
			{
				SoqlWriter writer2 = writer.Writer;
				writer2.Write(this.name);
				writer2.Write(SoqlTokens.LeftParen);
				for (int i = 0; i < arguments.Count; i++)
				{
					if (i != 0)
					{
						writer2.Write(SoqlTokens.Comma);
					}
					writer.Write(arguments[i]);
				}
				writer2.Write(SoqlTokens.RightParen);
			}

			// Token: 0x0400062C RID: 1580
			private readonly Token name;
		}

		// Token: 0x02000203 RID: 515
		private class LikeInvocationWriter : SoqlExpressionWriter.InvocationWriter
		{
			// Token: 0x06000A78 RID: 2680 RVA: 0x00017647 File Offset: 0x00015847
			public LikeInvocationWriter(bool wildAtStart, bool wildAtEnd)
			{
				this.wildAtStart = wildAtStart;
				this.wildAtEnd = wildAtEnd;
			}

			// Token: 0x06000A79 RID: 2681 RVA: 0x00017660 File Offset: 0x00015860
			public override void Write(SoqlExpressionWriter writer, IList<QueryExpression> arguments)
			{
				Value value;
				if (!((ConstantQueryExpression)arguments[1]).TryGetConstant(out value) || !value.IsText)
				{
					throw new InvalidOperationException();
				}
				SoqlWriter writer2 = writer.Writer;
				writer2.Write(SoqlTokens.LeftParen);
				writer.Write(arguments[0]);
				writer2.WriteSpace();
				writer2.Write(SoqlTokens.Like);
				writer2.WriteSpace();
				writer2.WriteLikeString(value.AsString, this.wildAtStart, this.wildAtEnd);
				writer2.Write(SoqlTokens.RightParen);
			}

			// Token: 0x0400062D RID: 1581
			private readonly bool wildAtStart;

			// Token: 0x0400062E RID: 1582
			private readonly bool wildAtEnd;
		}

		// Token: 0x02000204 RID: 516
		private class InInvocationWriter : SoqlExpressionWriter.InvocationWriter
		{
			// Token: 0x06000A7A RID: 2682 RVA: 0x000176E8 File Offset: 0x000158E8
			public override void Write(SoqlExpressionWriter writer, IList<QueryExpression> arguments)
			{
				SoqlWriter writer2 = writer.Writer;
				writer.Write(arguments[1]);
				writer2.WriteSpace();
				writer2.Write(SoqlTokens.In);
				writer2.WriteSpace();
				writer2.Write(SoqlTokens.LeftParen);
				writer.Write(arguments[0]);
				writer2.Write(SoqlTokens.RightParen);
			}
		}
	}
}
