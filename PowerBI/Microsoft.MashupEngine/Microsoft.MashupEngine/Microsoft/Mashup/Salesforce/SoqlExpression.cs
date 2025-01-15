using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Salesforce
{
	// Token: 0x020001F2 RID: 498
	internal class SoqlExpression
	{
		// Token: 0x060009F1 RID: 2545 RVA: 0x00015A04 File Offset: 0x00013C04
		public SoqlExpression(RecordTypeValue recordType, SoqlColumns columns, TypeValue argumentType, QueryExpression expression)
		{
			if (columns != null)
			{
				expression = SoqlExpression.ColumnAccessRewriter.Rewrite(expression, columns);
			}
			TypedQueryExpression typedQueryExpression = new SoqlExpression.SoqlQueryExpressionVisitor(recordType, argumentType).Visit(expression);
			this.expression = typedQueryExpression.Expression;
			this.type = typedQueryExpression.Type;
			this.columnNames = recordType.Fields.Keys;
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x00015A5E File Offset: 0x00013C5E
		public QueryExpression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x00015A66 File Offset: 0x00013C66
		public TypeValue Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x00015A6E File Offset: 0x00013C6E
		public Keys Columns
		{
			get
			{
				return this.columnNames;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x00015A76 File Offset: 0x00013C76
		public bool IsConstant
		{
			get
			{
				return this.expression.Kind == QueryExpressionKind.Constant;
			}
		}

		// Token: 0x040005FA RID: 1530
		private readonly QueryExpression expression;

		// Token: 0x040005FB RID: 1531
		private readonly TypeValue type;

		// Token: 0x040005FC RID: 1532
		private readonly Keys columnNames;

		// Token: 0x020001F3 RID: 499
		private class ColumnAccessRewriter : QueryExpressionVisitor
		{
			// Token: 0x060009F6 RID: 2550 RVA: 0x00015A86 File Offset: 0x00013C86
			private ColumnAccessRewriter(SoqlColumns columns)
			{
				this.columns = columns;
			}

			// Token: 0x060009F7 RID: 2551 RVA: 0x00015A95 File Offset: 0x00013C95
			public static QueryExpression Rewrite(QueryExpression expression, SoqlColumns columns)
			{
				return new SoqlExpression.ColumnAccessRewriter(columns).Visit(expression);
			}

			// Token: 0x060009F8 RID: 2552 RVA: 0x00015AA3 File Offset: 0x00013CA3
			protected override QueryExpression VisitColumnAccess(ColumnAccessQueryExpression columnAccess)
			{
				return this.columns.Expressions[columnAccess.Column].expression;
			}

			// Token: 0x040005FD RID: 1533
			private readonly SoqlColumns columns;
		}

		// Token: 0x020001F4 RID: 500
		private class SoqlQueryExpressionVisitor
		{
			// Token: 0x060009F9 RID: 2553 RVA: 0x00015AC0 File Offset: 0x00013CC0
			private Dictionary<FunctionValue, Func<InvocationQueryExpression, TypedQueryExpression>> GetFunctionVisitors()
			{
				return new Dictionary<FunctionValue, Func<InvocationQueryExpression, TypedQueryExpression>>
				{
					{
						Library._Value.As,
						new Func<InvocationQueryExpression, TypedQueryExpression>(this.VisitValueAs)
					},
					{
						CultureSpecificFunction.DateFrom,
						new Func<InvocationQueryExpression, TypedQueryExpression>(this.VisitDateFrom)
					},
					{
						CultureSpecificFunction.DateTimeFrom,
						new Func<InvocationQueryExpression, TypedQueryExpression>(this.VisitDateTimeFrom)
					}
				};
			}

			// Token: 0x060009FA RID: 2554 RVA: 0x00015B17 File Offset: 0x00013D17
			public SoqlQueryExpressionVisitor(RecordTypeValue recordType, TypeValue argumentType)
			{
				this.recordType = recordType;
				this.argumentType = argumentType;
				this.functionVisitors = this.GetFunctionVisitors();
			}

			// Token: 0x060009FB RID: 2555 RVA: 0x00015B3C File Offset: 0x00013D3C
			public TypedQueryExpression Visit(QueryExpression expression)
			{
				switch (expression.Kind)
				{
				case QueryExpressionKind.Binary:
					return this.VisitBinary((BinaryQueryExpression)expression);
				case QueryExpressionKind.Constant:
					return this.VisitConstant((ConstantQueryExpression)expression);
				case QueryExpressionKind.ColumnAccess:
					return this.VisitColumnAccess((ColumnAccessQueryExpression)expression);
				case QueryExpressionKind.If:
					return this.VisitIf((IfQueryExpression)expression);
				case QueryExpressionKind.Invocation:
					return this.VisitInvocation((InvocationQueryExpression)expression);
				case QueryExpressionKind.Unary:
					return this.VisitUnary((UnaryQueryExpression)expression);
				case QueryExpressionKind.ArgumentAccess:
					return this.VisitArgumentAccess((ArgumentAccessQueryExpression)expression);
				default:
					throw new InvalidOperationException();
				}
			}

			// Token: 0x060009FC RID: 2556 RVA: 0x00015BD4 File Offset: 0x00013DD4
			private TypedQueryExpression VisitArgumentAccess(ArgumentAccessQueryExpression argument)
			{
				return new TypedQueryExpression(this.argumentType, argument);
			}

			// Token: 0x060009FD RID: 2557 RVA: 0x00015BE4 File Offset: 0x00013DE4
			private TypedQueryExpression VisitBinary(BinaryQueryExpression binaryExpression)
			{
				BinaryOperator2 @operator = binaryExpression.Operator;
				if (@operator - BinaryOperator2.GreaterThan > 7)
				{
					throw new NotSupportedException();
				}
				TypedQueryExpression typedQueryExpression = this.Visit(binaryExpression.Left);
				TypedQueryExpression typedQueryExpression2 = this.Visit(binaryExpression.Right);
				if (!SalesforceTypes.IsScalar(typedQueryExpression.Type) || !SalesforceTypes.IsScalar(typedQueryExpression2.Type) || !SalesforceTypes.AreCompatible(typedQueryExpression.Type, typedQueryExpression2.Type))
				{
					throw new NotSupportedException();
				}
				return new TypedQueryExpression(SoqlExpression.SoqlQueryExpressionVisitor.CheckType(OperatorTypeflowModels.Binary(binaryExpression.Operator, typedQueryExpression.Type, typedQueryExpression2.Type)), new BinaryQueryExpression(binaryExpression.Operator, typedQueryExpression.Expression, typedQueryExpression2.Expression));
			}

			// Token: 0x060009FE RID: 2558 RVA: 0x00015C94 File Offset: 0x00013E94
			private TypedQueryExpression VisitConstant(ConstantQueryExpression constantExpression)
			{
				Value value = constantExpression.Value;
				switch (value.Kind)
				{
				case ValueKind.Null:
				case ValueKind.Date:
				case ValueKind.DateTime:
				case ValueKind.Duration:
				case ValueKind.Number:
				case ValueKind.Logical:
				case ValueKind.Text:
				case ValueKind.List:
					return new TypedQueryExpression(value.Type, constantExpression);
				}
				throw new NotSupportedException();
			}

			// Token: 0x060009FF RID: 2559 RVA: 0x00015CF8 File Offset: 0x00013EF8
			private TypedQueryExpression VisitColumnAccess(ColumnAccessQueryExpression columnAccessExpression)
			{
				TypeValue typeValue = this.recordType.Fields[columnAccessExpression.Column]["Type"].AsType;
				if (this.argumentType.TypeKind == ValueKind.Table)
				{
					typeValue = ListTypeValue.New(typeValue);
				}
				return new TypedQueryExpression(typeValue, columnAccessExpression);
			}

			// Token: 0x06000A00 RID: 2560 RVA: 0x000033E7 File Offset: 0x000015E7
			private TypedQueryExpression VisitIf(IfQueryExpression ifExpression)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000A01 RID: 2561 RVA: 0x00015D48 File Offset: 0x00013F48
			private TypedQueryExpression VisitInvocation(InvocationQueryExpression invocationExpression)
			{
				Value value;
				if (!invocationExpression.Function.TryGetConstant(out value))
				{
					throw new NotSupportedException();
				}
				Func<InvocationQueryExpression, TypedQueryExpression> func;
				if (value.IsFunction && this.functionVisitors.TryGetValue(value.AsFunction, out func))
				{
					return func(invocationExpression);
				}
				FunctionValue functionValue;
				if (!SoqlExpression.SoqlQueryExpressionVisitor.functions.TryGetValue(value, out functionValue) && !SoqlExpression.SoqlQueryExpressionVisitor.types.TryGetValue(value.GetType(), out functionValue))
				{
					throw new NotSupportedException();
				}
				if (functionValue.Type.AsFunctionType.ParameterCount != invocationExpression.Arguments.Count)
				{
					throw new NotSupportedException();
				}
				QueryExpression[] array = new QueryExpression[invocationExpression.Arguments.Count];
				TypeValue[] array2 = new TypeValue[array.Length];
				int num = 0;
				for (int i = 0; i < array.Length; i++)
				{
					TypedQueryExpression typedQueryExpression = this.Visit(invocationExpression.Arguments[i]);
					array2[i] = typedQueryExpression.Type;
					array[i] = typedQueryExpression.Expression;
					if (array[i].Kind == QueryExpressionKind.Constant)
					{
						num++;
					}
				}
				if (num == array.Length)
				{
					throw new NotSupportedException();
				}
				IArgumentValuesChecker argumentValuesChecker = functionValue as IArgumentValuesChecker;
				if (argumentValuesChecker != null)
				{
					TypedQueryExpression[] array3 = new TypedQueryExpression[array.Length];
					for (int j = 0; j < array.Length; j++)
					{
						array3[j] = new TypedQueryExpression(array2[j], array[j]);
					}
					argumentValuesChecker.CheckArguments(array3);
				}
				FunctionValue functionValue2 = functionValue;
				Value[] array4 = array2;
				return new TypedQueryExpression(functionValue2.Invoke(array4).AsType, new InvocationQueryExpression(new ConstantQueryExpression(functionValue), array));
			}

			// Token: 0x06000A02 RID: 2562 RVA: 0x00015EC0 File Offset: 0x000140C0
			private TypedQueryExpression VisitUnary(UnaryQueryExpression unaryExpression)
			{
				TypedQueryExpression typedQueryExpression = this.Visit(unaryExpression.Expression);
				return new TypedQueryExpression(SoqlExpression.SoqlQueryExpressionVisitor.CheckType(OperatorTypeflowModels.Unary(unaryExpression.Operator, typedQueryExpression.Type)), new UnaryQueryExpression(unaryExpression.Operator, typedQueryExpression.Expression));
			}

			// Token: 0x06000A03 RID: 2563 RVA: 0x00015F08 File Offset: 0x00014108
			private TypedQueryExpression VisitValueAs(InvocationQueryExpression invocationExpression)
			{
				if (invocationExpression.Arguments.Count == 2 && invocationExpression.Arguments[1] is ConstantQueryExpression)
				{
					TypedQueryExpression typedQueryExpression = this.Visit(invocationExpression.Arguments[0]);
					ConstantQueryExpression constantQueryExpression = (ConstantQueryExpression)invocationExpression.Arguments[1];
					if (constantQueryExpression.Value.IsType && typedQueryExpression.Type.IsCompatibleWith(constantQueryExpression.Value.AsType))
					{
						return new TypedQueryExpression(constantQueryExpression.Value.AsType, typedQueryExpression.Expression);
					}
				}
				throw new NotSupportedException();
			}

			// Token: 0x06000A04 RID: 2564 RVA: 0x00015FA0 File Offset: 0x000141A0
			private TypedQueryExpression VisitDateFrom(InvocationQueryExpression invocationExpression)
			{
				if (invocationExpression.Arguments.Count == 1)
				{
					TypedQueryExpression typedQueryExpression = this.Visit(invocationExpression.Arguments[0]);
					if (typedQueryExpression.Type.TypeKind == ValueKind.DateTime && typedQueryExpression.Expression.Kind == QueryExpressionKind.ColumnAccess)
					{
						FunctionValue dateTimeToDate = SoqlFunctionValue.DateTimeToDate;
						QueryExpression[] array = new QueryExpression[] { typedQueryExpression.Expression };
						return new TypedQueryExpression(TypeValue.Date, new InvocationQueryExpression(new ConstantQueryExpression(dateTimeToDate), array));
					}
					if (typedQueryExpression.Type.TypeKind == ValueKind.Date && typedQueryExpression.Expression.Kind == QueryExpressionKind.ColumnAccess)
					{
						return this.VisitColumnAccess((ColumnAccessQueryExpression)typedQueryExpression.Expression);
					}
				}
				throw new NotSupportedException();
			}

			// Token: 0x06000A05 RID: 2565 RVA: 0x00016054 File Offset: 0x00014254
			private TypedQueryExpression VisitDateTimeFrom(InvocationQueryExpression invocationExpression)
			{
				if (invocationExpression.Arguments.Count == 1)
				{
					TypedQueryExpression typedQueryExpression = this.Visit(invocationExpression.Arguments[0]);
					if (typedQueryExpression.Type.TypeKind == ValueKind.DateTime && typedQueryExpression.Expression.Kind == QueryExpressionKind.ColumnAccess)
					{
						return this.VisitColumnAccess((ColumnAccessQueryExpression)typedQueryExpression.Expression);
					}
				}
				throw new NotSupportedException();
			}

			// Token: 0x06000A06 RID: 2566 RVA: 0x000160B8 File Offset: 0x000142B8
			private static TypeValue CheckType(TypeValue type)
			{
				if (type.TypeKind == ValueKind.None)
				{
					throw new NotSupportedException();
				}
				return type;
			}

			// Token: 0x040005FE RID: 1534
			private static Dictionary<Value, FunctionValue> functions = new Dictionary<Value, FunctionValue>
			{
				{
					Library.Date.Month,
					SoqlFunctionValue.CalendarMonth
				},
				{
					Library.Date.QuarterOfYear,
					SoqlFunctionValue.CalendarQuarter
				},
				{
					Library.Date.Year,
					SoqlFunctionValue.CalendarYear
				},
				{
					Library.Date.Day,
					SoqlFunctionValue.DayInMonth
				},
				{
					Library.Date.DayOfYear,
					SoqlFunctionValue.DayInYear
				},
				{
					Library.Time.Hour,
					SoqlFunctionValue.HourInDay
				},
				{
					Library.List.Average,
					SoqlFunctionValue.Avg
				},
				{
					Library.List.Max,
					SoqlFunctionValue.Max
				},
				{
					Library.List.Min,
					SoqlFunctionValue.Min
				},
				{
					Library.List.Sum,
					SoqlFunctionValue.Sum
				},
				{
					Library.List.Contains,
					SoqlFunctionValue.ListContains
				},
				{
					Library.Text.Contains,
					SoqlFunctionValue.TextContains
				},
				{
					Library.Text.StartsWith,
					SoqlFunctionValue.TextStartsWith
				},
				{
					Library.Text.EndsWith,
					SoqlFunctionValue.TextEndsWith
				},
				{
					TableModule.Table.RowCount,
					SoqlFunctionValue.Count
				}
			};

			// Token: 0x040005FF RID: 1535
			private static Dictionary<Type, FunctionValue> types = new Dictionary<Type, FunctionValue>
			{
				{
					typeof(Library.Date.DayOfWeekFunctionValue),
					SoqlFunctionValue.DayInWeek
				},
				{
					typeof(Library.Date.WeekOfMonthFunctionValue),
					SoqlFunctionValue.WeekInMonth
				},
				{
					typeof(Library.Date.WeekOfYearFunctionValue),
					SoqlFunctionValue.WeekInYear
				}
			};

			// Token: 0x04000600 RID: 1536
			private readonly RecordTypeValue recordType;

			// Token: 0x04000601 RID: 1537
			private readonly TypeValue argumentType;

			// Token: 0x04000602 RID: 1538
			private readonly Dictionary<FunctionValue, Func<InvocationQueryExpression, TypedQueryExpression>> functionVisitors;
		}
	}
}
