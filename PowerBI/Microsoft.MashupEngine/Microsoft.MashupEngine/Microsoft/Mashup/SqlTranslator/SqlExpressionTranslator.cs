using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.ScriptDom;

namespace Microsoft.Mashup.SqlTranslator
{
	// Token: 0x0200202B RID: 8235
	public class SqlExpressionTranslator
	{
		// Token: 0x0600C875 RID: 51317 RVA: 0x0027E0B9 File Offset: 0x0027C2B9
		public static SqlExpressionTranslationResult Translate(IEngine engine, IEngineHost host, IRecordValue environment, SqlParseResult parseResult)
		{
			return new SqlExpressionTranslator(engine, host, environment).Translate(parseResult);
		}

		// Token: 0x0600C876 RID: 51318 RVA: 0x0027E0C9 File Offset: 0x0027C2C9
		private SqlExpressionTranslator(IEngine engine, IEngineHost host, IRecordValue environment)
		{
			this.engine = engine;
			this.host = host;
			this.environment = environment;
		}

		// Token: 0x0600C877 RID: 51319 RVA: 0x0027E0E6 File Offset: 0x0027C2E6
		private IHostTrace CreateTrace(string method, TraceEventType level = TraceEventType.Information)
		{
			return TracingService.ScopedPerformanceTrace(this.host, method, level, null);
		}

		// Token: 0x0600C878 RID: 51320 RVA: 0x0027E0F8 File Offset: 0x0027C2F8
		private SqlExpressionTranslationResult Translate(SqlParseResult parseResult)
		{
			SqlExpressionTranslationResult sqlExpressionTranslationResult;
			using (IHostTrace hostTrace = this.CreateTrace("SqlExpressionTranslator/Translate", TraceEventType.Information))
			{
				hostTrace.Add("IsRecognized", parseResult.IsRecognized, false);
				if (parseResult.IsRecognized)
				{
					string text = this.Translate(parseResult.Select);
					if (text != null)
					{
						hostTrace.Add("Result", text, true);
						return SqlExpressionTranslationResult.NewSupported(text, parseResult.ResourceNames);
					}
				}
				sqlExpressionTranslationResult = SqlExpressionTranslationResult.NewUnrecognized();
			}
			return sqlExpressionTranslationResult;
		}

		// Token: 0x0600C879 RID: 51321 RVA: 0x0027E184 File Offset: 0x0027C384
		private string Translate(SelectStatement select)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(environment) => ");
			if (this.TranslateSelectStatement(stringBuilder, select))
			{
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x0600C87A RID: 51322 RVA: 0x0027E1B8 File Offset: 0x0027C3B8
		private bool TranslateSelectStatement(StringBuilder sb, SelectStatement select)
		{
			WithCtesAndXmlNamespaces withCtesAndXmlNamespaces = select.WithCtesAndXmlNamespaces;
			if (select.ComputeClauses.Count == 0 && select.Into == null && (withCtesAndXmlNamespaces == null || (withCtesAndXmlNamespaces.XmlNamespaces == null && withCtesAndXmlNamespaces.ChangeTrackingContext == null)))
			{
				IExpression expression = new SqlExpressionTranslator.SqlToMTranslator(this.host, this.engine, this.environment).Translate(select);
				sb.Append(new ExpressionToMVisitor(this.engine, null).Visit(expression));
				return true;
			}
			return false;
		}

		// Token: 0x04006649 RID: 26185
		private readonly IEngine engine;

		// Token: 0x0400664A RID: 26186
		private readonly IEngineHost host;

		// Token: 0x0400664B RID: 26187
		private readonly IRecordValue environment;

		// Token: 0x0200202C RID: 8236
		private class SqlToMTranslator : SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>
		{
			// Token: 0x1700308A RID: 12426
			// (get) Token: 0x0600C87B RID: 51323 RVA: 0x0027E22E File Offset: 0x0027C42E
			private Microsoft.Mashup.Engine.Interface.Identifier RowParameter
			{
				get
				{
					return this.rowParameters[this.rowParameters.Count - 1];
				}
			}

			// Token: 0x0600C87C RID: 51324 RVA: 0x0027E248 File Offset: 0x0027C448
			protected override void BeginScalarExpression(SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table table)
			{
				Microsoft.Mashup.Engine.Interface.Identifier rowParameter = Microsoft.Mashup.Engine.Interface.Identifier.New();
				this.rowParameters.Add(rowParameter);
				table = new SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table
				{
					Inputs = table.Inputs,
					Columns = table.Columns.Select((SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding c) => new SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding
					{
						Identifier = c.Identifier,
						Value = new SqlExpressionTranslator.RowParameterAndType
						{
							RowParameter = rowParameter,
							Type = c.Value.Type
						}
					}).ToArray<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding>(),
					Expression = table.Expression
				};
				base.BeginScalarExpression(table);
			}

			// Token: 0x0600C87D RID: 51325 RVA: 0x0027E2BF File Offset: 0x0027C4BF
			protected override IExpression EndScalarExpression(IExpression expression)
			{
				expression = new FunctionExpressionSyntaxNode(SqlExpressionTranslator.SqlToMTranslator.NewFunctionType(this.RowParameter), expression);
				this.rowParameters.RemoveAt(this.rowParameters.Count - 1);
				return base.EndScalarExpression(expression);
			}

			// Token: 0x0600C87E RID: 51326 RVA: 0x0027E2F4 File Offset: 0x0027C4F4
			public SqlToMTranslator(IEngineHost host, IEngine engine, IRecordValue environment)
			{
				this.host = host;
				this.engine = engine;
				this.environment = environment;
				this.aggregateDetectionVisitor = new SqlExpressionTranslator.SqlToMTranslator.AggregateDetectionVisitor(this);
				this.aggregateFunctionMap = new Dictionary<string, Func<bool, IList<IExpression>, IExpression>>(StringComparer.OrdinalIgnoreCase)
				{
					{
						"sum",
						(bool d, IList<IExpression> p) => this.NewAggregateWithPrecision("List.Sum", d, p)
					},
					{
						"avg",
						(bool d, IList<IExpression> p) => this.NewAggregateWithPrecision("List.Average", d, p)
					},
					{
						"min",
						(bool d, IList<IExpression> p) => this.NewAggregate("List.Min", d, p)
					},
					{
						"max",
						(bool d, IList<IExpression> p) => this.NewAggregate("List.Max", d, p)
					},
					{
						"count",
						new Func<bool, IList<IExpression>, IExpression>(this.NewCount)
					},
					{
						"count_big",
						new Func<bool, IList<IExpression>, IExpression>(this.NewCount)
					},
					{
						"approx_count_distinct",
						new Func<bool, IList<IExpression>, IExpression>(this.NewApproximateDistinctCount)
					},
					{
						"stdev",
						(bool d, IList<IExpression> p) => this.NewAggregate("List.StandardDeviation", d, p)
					}
				};
				this.functionMap = new Dictionary<string, Func<IList<IExpression>, IExpression>>(StringComparer.OrdinalIgnoreCase)
				{
					{
						"list",
						new Func<IList<IExpression>, IExpression>(this.NewList)
					},
					{
						"recordref",
						new Func<IList<IExpression>, IExpression>(this.NewRecordRef)
					},
					{
						"listref",
						new Func<IList<IExpression>, IExpression>(this.NewListRef)
					},
					{
						"tableref",
						new Func<IList<IExpression>, IExpression>(this.NewTableRef)
					},
					{
						"cube.applymeasure",
						new Func<IList<IExpression>, IExpression>(this.NewCubeApplyMeasure)
					},
					{
						"cube.dimension",
						new Func<IList<IExpression>, IExpression>(this.NewCubeDimension)
					},
					{
						"cube.applyparameter",
						new Func<IList<IExpression>, IExpression>(this.NewCubeApplyParameter)
					},
					{
						"isnull",
						new Func<IList<IExpression>, IExpression>(this.NewIsNull)
					},
					{
						"day",
						(IList<IExpression> p) => this.NewCall("Date.Day", this.engine.Type(TypeHandle.Int32), p)
					},
					{
						"month",
						(IList<IExpression> p) => this.NewCall("Date.Month", this.engine.Type(TypeHandle.Int32), p)
					},
					{
						"year",
						(IList<IExpression> p) => this.NewCall("Date.Year", this.engine.Type(TypeHandle.Int32), p)
					},
					{
						"dateadd",
						new Func<IList<IExpression>, IExpression>(this.NewDateAdd)
					},
					{
						"datediff",
						this.NewDateDiff(this.engine.Type(TypeHandle.Int32))
					},
					{
						"datediff_big",
						this.NewDateDiff(this.engine.Type(TypeHandle.Int64))
					},
					{
						"datepart",
						new Func<IList<IExpression>, IExpression>(this.NewDatePart)
					},
					{
						"char",
						(IList<IExpression> p) => this.NewCall("Character.FromNumber", this.engine.Type(TypeHandle.Text), p)
					},
					{
						"charindex",
						new Func<IList<IExpression>, IExpression>(this.NewCharIndex)
					},
					{
						"len",
						(IList<IExpression> p) => this.NewCall("Text.Length", this.GetType(p[0]), p)
					},
					{
						"lower",
						(IList<IExpression> p) => this.NewCall("Text.Lower", this.GetType(p[0]), p)
					},
					{
						"ltrim",
						new Func<IList<IExpression>, IExpression>(this.NewLTrim)
					},
					{
						"replace",
						(IList<IExpression> p) => this.NewCall("Text.Replace", this.GetType(p[0]), p)
					},
					{
						"rtrim",
						new Func<IList<IExpression>, IExpression>(this.NewRTrim)
					},
					{
						"substring",
						new Func<IList<IExpression>, IExpression>(this.NewSubstring)
					},
					{
						"upper",
						(IList<IExpression> p) => this.NewCall("Text.Upper", this.GetType(p[0]), p)
					},
					{
						"abs",
						(IList<IExpression> p) => this.NewCall("Number.Abs", this.GetType(p[0]), p)
					},
					{
						"ceiling",
						(IList<IExpression> p) => this.NewCall("Number.RoundUp", this.GetType(p[0]), p)
					},
					{
						"exp",
						(IList<IExpression> p) => this.NewCall("Number.Exp", this.engine.Type(TypeHandle.Double), p)
					},
					{
						"floor",
						(IList<IExpression> p) => this.NewCall("Number.RoundDown", this.GetType(p[0]), p)
					},
					{
						"log",
						(IList<IExpression> p) => this.NewCall("Number.Log", this.engine.Type(TypeHandle.Double), p)
					},
					{
						"log10",
						(IList<IExpression> p) => this.NewCall("Number.Log10", this.engine.Type(TypeHandle.Double), p)
					},
					{
						"round",
						new Func<IList<IExpression>, IExpression>(this.NewRound)
					},
					{
						"power",
						(IList<IExpression> p) => this.NewCall("Number.Power", this.GetType(p[0]), p)
					},
					{
						"sign",
						(IList<IExpression> p) => this.NewCall("Number.Sign", this.GetSignReturnType(this.GetType(p[0])), p)
					},
					{
						"sqrt",
						(IList<IExpression> p) => this.NewCall("Number.Sqrt", this.engine.Type(TypeHandle.Double), p)
					},
					{
						"square",
						(IList<IExpression> p) => this.NewCall("Number.Power", this.engine.Type(TypeHandle.Double), new IExpression[]
						{
							p[0],
							this.NewConstant(this.engine.Number(2.0))
						})
					},
					{
						"cos",
						(IList<IExpression> p) => this.NewCall("Number.Cos", this.engine.Type(TypeHandle.Double), p)
					},
					{
						"sin",
						(IList<IExpression> p) => this.NewCall("Number.Sin", this.engine.Type(TypeHandle.Double), p)
					},
					{
						"tan",
						(IList<IExpression> p) => this.NewCall("Number.Tan", this.engine.Type(TypeHandle.Double), p)
					},
					{
						"acos",
						(IList<IExpression> p) => this.NewCall("Number.Acos", this.engine.Type(TypeHandle.Double), p)
					},
					{
						"asin",
						(IList<IExpression> p) => this.NewCall("Number.Asin", this.engine.Type(TypeHandle.Double), p)
					},
					{
						"atan",
						(IList<IExpression> p) => this.NewCall("Number.Atan", this.engine.Type(TypeHandle.Double), p)
					},
					{
						"atn2",
						(IList<IExpression> p) => this.NewCall("Number.Atan2", this.engine.Type(TypeHandle.Double), p)
					}
				};
				this.typeReferenceMap = new Dictionary<ITypeValue, IExpression>
				{
					{
						this.engine.Type(TypeHandle.Int64),
						SqlExpressionTranslator.SqlToMTranslator.Int64TypeReference
					},
					{
						this.engine.Type(TypeHandle.Int32),
						SqlExpressionTranslator.SqlToMTranslator.Int32TypeReference
					},
					{
						this.engine.Type(TypeHandle.Int16),
						SqlExpressionTranslator.SqlToMTranslator.Int16TypeReference
					},
					{
						this.engine.Type(TypeHandle.Int8),
						SqlExpressionTranslator.SqlToMTranslator.Int8TypeReference
					},
					{
						this.engine.Type(TypeHandle.Logical),
						SqlExpressionTranslator.SqlToMTranslator.LogicalTypeReference
					},
					{
						this.engine.Type(TypeHandle.Decimal),
						SqlExpressionTranslator.SqlToMTranslator.DecimalTypeReference
					},
					{
						this.engine.Type(TypeHandle.Currency),
						SqlExpressionTranslator.SqlToMTranslator.CurrencyTypeReference
					},
					{
						this.engine.Type(TypeHandle.Double),
						SqlExpressionTranslator.SqlToMTranslator.DoubleTypeReference
					},
					{
						this.engine.Type(TypeHandle.Single),
						SqlExpressionTranslator.SqlToMTranslator.SingleTypeReference
					},
					{
						this.engine.Type(TypeHandle.Date),
						SqlExpressionTranslator.SqlToMTranslator.DateTypeReference
					},
					{
						this.engine.Type(TypeHandle.Time),
						SqlExpressionTranslator.SqlToMTranslator.TimeTypeReference
					},
					{
						this.engine.Type(TypeHandle.DateTime),
						SqlExpressionTranslator.SqlToMTranslator.DateTimeTypeReference
					},
					{
						this.engine.Type(TypeHandle.DateTimeZone),
						SqlExpressionTranslator.SqlToMTranslator.DateTimeZoneTypeReference
					},
					{
						this.engine.Type(TypeHandle.Duration),
						SqlExpressionTranslator.SqlToMTranslator.DurationTypeReference
					},
					{
						this.engine.Type(TypeHandle.Binary),
						SqlExpressionTranslator.SqlToMTranslator.BinaryTypeReference
					},
					{
						this.engine.Type(TypeHandle.Text),
						SqlExpressionTranslator.SqlToMTranslator.TextTypeReference
					}
				};
			}

			// Token: 0x1700308B RID: 12427
			// (get) Token: 0x0600C87F RID: 51327 RVA: 0x0027E957 File Offset: 0x0027CB57
			private IConstantExpression2 DurationConstructor
			{
				get
				{
					if (this.durationConstructor == null)
					{
						this.durationConstructor = this.GetConstantExpression("#duration");
					}
					return this.durationConstructor;
				}
			}

			// Token: 0x0600C880 RID: 51328 RVA: 0x0027E978 File Offset: 0x0027CB78
			private IConstantExpression2 GetConstantExpression(string expression)
			{
				IValue value;
				if (this.engine.TryParseSourceValue(expression, out value))
				{
					return (IConstantExpression2)this.NewConstant(value);
				}
				throw new NotSupportedException();
			}

			// Token: 0x0600C881 RID: 51329 RVA: 0x0027E9A8 File Offset: 0x0027CBA8
			private IExpressionDocument CompileExpression(string expression)
			{
				ITokens tokens = this.engine.Tokenize(expression);
				Action<IError> action = delegate(IError e)
				{
					throw new InvalidOperationException(e.Message);
				};
				return (IExpressionDocument)this.engine.Parse(tokens, new TextDocumentHost(expression), action);
			}

			// Token: 0x0600C882 RID: 51330 RVA: 0x0027E9FA File Offset: 0x0027CBFA
			private IFunctionValue CompileFunction(string expression)
			{
				return this.CompileFunction(this.CompileExpression(expression));
			}

			// Token: 0x0600C883 RID: 51331 RVA: 0x0027EA09 File Offset: 0x0027CC09
			private IFunctionValue CompileFunction(IExpression expression)
			{
				return this.CompileFunction(new ExpressionDocumentSyntaxNode(expression));
			}

			// Token: 0x0600C884 RID: 51332 RVA: 0x0027EA18 File Offset: 0x0027CC18
			private IFunctionValue CompileFunction(IExpressionDocument expression)
			{
				IRecordValue library = this.engine.GetLibrary(this.host, null);
				IModule module = this.engine.Compile(expression, library, CompileOptions.None, delegate(IError e)
				{
					throw new InvalidOperationException(e.Message);
				});
				return this.engine.Assemble(new IModule[] { module }, library, this.host, delegate(IError e)
				{
					throw new InvalidOperationException(e.Message);
				}).Function;
			}

			// Token: 0x0600C885 RID: 51333 RVA: 0x0027EAA8 File Offset: 0x0027CCA8
			private IExpression NewIsNull(IList<IExpression> parameters)
			{
				if (parameters.Count != 2)
				{
					throw new NotSupportedException();
				}
				IExpression expression = new IfExpressionSyntaxNode(BinaryExpressionSyntaxNode.New(BinaryOperator2.Equals, parameters[0], this.NewConstant(this.engine.Null), TokenRange.Null), parameters[1], parameters[0], TokenRange.Null);
				return this.Result(expression, this.UnionTypes(this.GetType(parameters[0]).NonNullable, this.GetType(parameters[1])));
			}

			// Token: 0x0600C886 RID: 51334 RVA: 0x0027EB2C File Offset: 0x0027CD2C
			private IExpression NewCharIndex(IList<IExpression> parameters)
			{
				if (parameters.Count != 2)
				{
					throw new NotSupportedException();
				}
				IExpression expression = parameters[0];
				IExpression expression2 = parameters[1];
				return this.Result(this.Add(SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Text.PositionOf", expression2, expression), this.NewConstant(this.engine.Number(1.0))), this.engine.Type(TypeHandle.Int64));
			}

			// Token: 0x0600C887 RID: 51335 RVA: 0x0027EB98 File Offset: 0x0027CD98
			private IExpression NewLTrim(IList<IExpression> parameters)
			{
				if (parameters.Count != 1)
				{
					throw new NotSupportedException();
				}
				IExpression expression = parameters[0];
				return this.Result(SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Text.TrimStart", expression, this.NewConstant(" ")), this.GetType(expression));
			}

			// Token: 0x0600C888 RID: 51336 RVA: 0x0027EBE0 File Offset: 0x0027CDE0
			private IExpression NewRTrim(IList<IExpression> parameters)
			{
				if (parameters.Count != 1)
				{
					throw new NotSupportedException();
				}
				IExpression expression = parameters[0];
				return this.Result(SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Text.TrimEnd", expression, this.NewConstant(" ")), this.GetType(expression));
			}

			// Token: 0x0600C889 RID: 51337 RVA: 0x0027EC28 File Offset: 0x0027CE28
			private IExpression NewSubstring(IList<IExpression> parameters)
			{
				if (parameters.Count != 3)
				{
					throw new NotSupportedException();
				}
				IExpression expression = parameters[0];
				IExpression expression2 = parameters[1];
				IExpression expression3 = parameters[2];
				return this.Result(SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Text.Middle", new IExpression[]
				{
					expression,
					this.Subtract(expression2, this.NewConstant(this.engine.Number(1.0))),
					expression3
				}), this.GetType(expression));
			}

			// Token: 0x0600C88A RID: 51338 RVA: 0x0027ECA8 File Offset: 0x0027CEA8
			private IExpression NewDateAdd(IList<IExpression> parameters)
			{
				if (parameters.Count != 3)
				{
					throw new NotSupportedException();
				}
				SqlExpressionTranslator.SqlToMTranslator.DatePart datePart = SqlExpressionTranslator.SqlToMTranslator.GetDatePart(parameters[0]);
				IExpression expression = parameters[1];
				IExpression expression2 = parameters[2];
				string dateAddSimpleFunction = SqlExpressionTranslator.SqlToMTranslator.GetDateAddSimpleFunction(datePart);
				IExpression expression3;
				if (dateAddSimpleFunction != null)
				{
					expression3 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation(dateAddSimpleFunction, expression2, expression);
				}
				else
				{
					expression3 = this.Add(expression2, this.GetDateAddDuration(datePart, expression));
				}
				return this.Result(expression3, this.GetType(expression2));
			}

			// Token: 0x0600C88B RID: 51339 RVA: 0x0027ED18 File Offset: 0x0027CF18
			private static string GetDateAddSimpleFunction(SqlExpressionTranslator.SqlToMTranslator.DatePart part)
			{
				switch (part)
				{
				case SqlExpressionTranslator.SqlToMTranslator.DatePart.Year:
					return "Date.AddYears";
				case SqlExpressionTranslator.SqlToMTranslator.DatePart.Quarter:
					return "Date.AddQuarters";
				case SqlExpressionTranslator.SqlToMTranslator.DatePart.Month:
					return "Date.AddMonths";
				case SqlExpressionTranslator.SqlToMTranslator.DatePart.Week:
					return "Date.AddWeeks";
				case SqlExpressionTranslator.SqlToMTranslator.DatePart.Dayofyear:
				case SqlExpressionTranslator.SqlToMTranslator.DatePart.Day:
				case SqlExpressionTranslator.SqlToMTranslator.DatePart.Weekday:
					return "Date.AddDays";
				default:
					return null;
				}
			}

			// Token: 0x0600C88C RID: 51340 RVA: 0x0027ED68 File Offset: 0x0027CF68
			private IExpression GetDateAddDuration(SqlExpressionTranslator.SqlToMTranslator.DatePart datepart, IExpression number)
			{
				switch (datepart)
				{
				case SqlExpressionTranslator.SqlToMTranslator.DatePart.Millisecond:
					number = this.Divide(number, this.NewConstant(1000.0));
					break;
				case SqlExpressionTranslator.SqlToMTranslator.DatePart.Microsecond:
					number = this.Divide(number, this.NewConstant(1000000.0));
					break;
				case SqlExpressionTranslator.SqlToMTranslator.DatePart.Nanosecond:
					number = this.Divide(number, this.NewConstant(1000000000.0));
					break;
				}
				IExpression[] array = new IExpression[]
				{
					this.NewConstant(0.0),
					this.NewConstant(0.0),
					this.NewConstant(0.0),
					this.NewConstant(0.0)
				};
				switch (datepart)
				{
				case SqlExpressionTranslator.SqlToMTranslator.DatePart.Hour:
					array[1] = number;
					break;
				case SqlExpressionTranslator.SqlToMTranslator.DatePart.Minute:
					array[2] = number;
					break;
				case SqlExpressionTranslator.SqlToMTranslator.DatePart.Second:
				case SqlExpressionTranslator.SqlToMTranslator.DatePart.Millisecond:
				case SqlExpressionTranslator.SqlToMTranslator.DatePart.Microsecond:
				case SqlExpressionTranslator.SqlToMTranslator.DatePart.Nanosecond:
					array[3] = number;
					break;
				default:
					throw new NotSupportedException(datepart.ToString());
				}
				return SqlExpressionTranslator.SqlToMTranslator.NewInvocation(this.DurationConstructor, array);
			}

			// Token: 0x0600C88D RID: 51341 RVA: 0x0027EE7A File Offset: 0x0027D07A
			private Func<IList<IExpression>, IExpression> NewDateDiff(ITypeValue returnType)
			{
				return delegate(IList<IExpression> parameters)
				{
					if (parameters.Count != 3)
					{
						throw new NotSupportedException();
					}
					SqlExpressionTranslator.SqlToMTranslator.DatePart datePart = SqlExpressionTranslator.SqlToMTranslator.GetDatePart(parameters[0]);
					IExpression expression = parameters[1];
					IExpression expression2 = parameters[2];
					string text = ((returnType == this.engine.Type(TypeHandle.Int64)) ? "Int64.From" : "Int32.From");
					IExpression expression3;
					switch (datePart)
					{
					case SqlExpressionTranslator.SqlToMTranslator.DatePart.Year:
						expression3 = this.Subtract(SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Date.Year", expression2), SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Date.Year", expression));
						break;
					case SqlExpressionTranslator.SqlToMTranslator.DatePart.Quarter:
						expression3 = this.Subtract(this.TotalQuarters(expression2), this.TotalQuarters(expression));
						break;
					case SqlExpressionTranslator.SqlToMTranslator.DatePart.Month:
						expression3 = this.Subtract(this.TotalMonths(expression2), this.TotalMonths(expression));
						break;
					case SqlExpressionTranslator.SqlToMTranslator.DatePart.Week:
						expression3 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation(text, this.Divide(SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Duration.TotalDays", this.Subtract(SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Date.StartOfWeek", expression2), SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Date.StartOfWeek", expression))), this.NewConstant(7.0)));
						break;
					case SqlExpressionTranslator.SqlToMTranslator.DatePart.Dayofyear:
					case SqlExpressionTranslator.SqlToMTranslator.DatePart.Day:
					case SqlExpressionTranslator.SqlToMTranslator.DatePart.Weekday:
						expression3 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation(text, SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Duration.TotalDays", this.Subtract(SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Date.StartOfDay", expression2), SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Date.StartOfDay", expression))));
						break;
					case SqlExpressionTranslator.SqlToMTranslator.DatePart.Hour:
						expression3 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation(text, SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Duration.TotalHours", this.Subtract(SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Time.StartOfHour", expression2), SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Time.StartOfHour", expression))));
						break;
					case SqlExpressionTranslator.SqlToMTranslator.DatePart.Minute:
						expression3 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation(text, SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Duration.TotalMinutes", this.Subtract(this.TimeStartOfMinute(expression2), this.TimeStartOfMinute(expression))));
						break;
					case SqlExpressionTranslator.SqlToMTranslator.DatePart.Second:
						expression3 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation(text, SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Duration.TotalSeconds", this.Subtract(this.TimeStartOfSecond(expression2, 0), this.TimeStartOfSecond(expression, 0))));
						break;
					case SqlExpressionTranslator.SqlToMTranslator.DatePart.Millisecond:
						expression3 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation(text, this.Multiply(SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Duration.TotalSeconds", this.Subtract(this.TimeStartOfSecond(expression2, 3), this.TimeStartOfSecond(expression, 3))), this.NewConstant(1000.0)));
						break;
					case SqlExpressionTranslator.SqlToMTranslator.DatePart.Microsecond:
						expression3 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation(text, this.Multiply(SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Duration.TotalSeconds", this.Subtract(this.TimeStartOfSecond(expression2, 6), this.TimeStartOfSecond(expression, 6))), this.NewConstant(1000000.0)));
						break;
					case SqlExpressionTranslator.SqlToMTranslator.DatePart.Nanosecond:
						expression3 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation(text, this.Multiply(SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Duration.TotalSeconds", this.Subtract(expression2, expression)), this.NewConstant(1000000000.0)));
						break;
					default:
						throw new InvalidOperationException(datePart.ToString());
					}
					return this.Result(expression3, returnType);
				};
			}

			// Token: 0x0600C88E RID: 51342 RVA: 0x0027EE9A File Offset: 0x0027D09A
			private IExpression TotalQuarters(IExpression date)
			{
				return this.Add(this.Multiply(SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Date.Year", date), this.NewConstant(4.0)), SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Date.QuarterOfYear", date));
			}

			// Token: 0x0600C88F RID: 51343 RVA: 0x0027EECD File Offset: 0x0027D0CD
			private IExpression TotalMonths(IExpression date)
			{
				return this.Add(this.Multiply(SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Date.Year", date), this.NewConstant(12.0)), SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Date.Month", date));
			}

			// Token: 0x0600C890 RID: 51344 RVA: 0x0027EF00 File Offset: 0x0027D100
			private IExpression TimeStartOfMinute(IExpression time)
			{
				return this.Subtract(time, SqlExpressionTranslator.SqlToMTranslator.NewInvocation(this.DurationConstructor, new IExpression[]
				{
					this.NewConstant(0.0),
					this.NewConstant(0.0),
					this.NewConstant(0.0),
					SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Time.Second", time)
				}));
			}

			// Token: 0x0600C891 RID: 51345 RVA: 0x0027EF6C File Offset: 0x0027D16C
			private IExpression TimeStartOfSecond(IExpression time, int scale = 0)
			{
				IExpression expression = SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Time.Second", time);
				IExpression expression2 = ((scale == 0) ? SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Number.RoundDown", expression) : SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Number.RoundDown", expression, this.NewConstant((double)scale)));
				return this.Subtract(time, SqlExpressionTranslator.SqlToMTranslator.NewInvocation(this.DurationConstructor, new IExpression[]
				{
					this.NewConstant(0.0),
					this.NewConstant(0.0),
					this.NewConstant(0.0),
					this.Subtract(expression, expression2)
				}));
			}

			// Token: 0x0600C892 RID: 51346 RVA: 0x0027F004 File Offset: 0x0027D204
			private IExpression NewDatePart(IList<IExpression> parameters)
			{
				if (parameters.Count != 2)
				{
					throw new NotSupportedException();
				}
				SqlExpressionTranslator.SqlToMTranslator.DatePart datePart = SqlExpressionTranslator.SqlToMTranslator.GetDatePart(parameters[0]);
				IExpression expression = parameters[1];
				if (datePart == SqlExpressionTranslator.SqlToMTranslator.DatePart.TZoffset)
				{
					return this.Result(this.Add(this.Multiply(SqlExpressionTranslator.SqlToMTranslator.NewInvocation("DateTimeZone.ZoneHours", expression), this.NewConstant(60.0)), SqlExpressionTranslator.SqlToMTranslator.NewInvocation("DateTimeZone.ZoneMinutes", expression)), this.engine.Type(TypeHandle.Int32));
				}
				IExpression expression2 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation(SqlExpressionTranslator.SqlToMTranslator.GetDatePartFunction(datePart), expression);
				if (datePart == SqlExpressionTranslator.SqlToMTranslator.DatePart.Second)
				{
					expression2 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Number.RoundDown", expression2);
				}
				else if (datePart == SqlExpressionTranslator.SqlToMTranslator.DatePart.Millisecond || datePart == SqlExpressionTranslator.SqlToMTranslator.DatePart.Microsecond || datePart == SqlExpressionTranslator.SqlToMTranslator.DatePart.Nanosecond)
				{
					int num;
					switch (datePart)
					{
					case SqlExpressionTranslator.SqlToMTranslator.DatePart.Millisecond:
						num = 1000;
						break;
					case SqlExpressionTranslator.SqlToMTranslator.DatePart.Microsecond:
						num = 1000000;
						break;
					case SqlExpressionTranslator.SqlToMTranslator.DatePart.Nanosecond:
						num = 1000000000;
						break;
					default:
						throw new InvalidOperationException();
					}
					IExpression expression3 = this.NewConstant((double)num);
					expression2 = this.Multiply(expression2, expression3);
					expression2 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Number.RoundDown", expression2);
					expression2 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Number.Mod", expression2, expression3);
				}
				return this.Result(expression2, this.engine.Type(TypeHandle.Int32));
			}

			// Token: 0x0600C893 RID: 51347 RVA: 0x0027F124 File Offset: 0x0027D324
			private static string GetDatePartFunction(SqlExpressionTranslator.SqlToMTranslator.DatePart part)
			{
				switch (part)
				{
				case SqlExpressionTranslator.SqlToMTranslator.DatePart.Year:
					return "Date.Year";
				case SqlExpressionTranslator.SqlToMTranslator.DatePart.Quarter:
					return "Date.QuarterOfYear";
				case SqlExpressionTranslator.SqlToMTranslator.DatePart.Month:
					return "Date.Month";
				case SqlExpressionTranslator.SqlToMTranslator.DatePart.Week:
					return "Date.WeekOfYear";
				case SqlExpressionTranslator.SqlToMTranslator.DatePart.Dayofyear:
					return "Date.DayOfYear";
				case SqlExpressionTranslator.SqlToMTranslator.DatePart.Day:
					return "Date.Day";
				case SqlExpressionTranslator.SqlToMTranslator.DatePart.Weekday:
					return "Date.DayOfWeek";
				case SqlExpressionTranslator.SqlToMTranslator.DatePart.Hour:
					return "Time.Hour";
				case SqlExpressionTranslator.SqlToMTranslator.DatePart.Minute:
					return "Time.Minute";
				case SqlExpressionTranslator.SqlToMTranslator.DatePart.Second:
				case SqlExpressionTranslator.SqlToMTranslator.DatePart.Millisecond:
				case SqlExpressionTranslator.SqlToMTranslator.DatePart.Microsecond:
				case SqlExpressionTranslator.SqlToMTranslator.DatePart.Nanosecond:
					return "Time.Second";
				case SqlExpressionTranslator.SqlToMTranslator.DatePart.ISO_WEEK:
					throw new NotSupportedException("ISO_WEEK");
				}
				throw new InvalidOperationException(part.ToString());
			}

			// Token: 0x0600C894 RID: 51348 RVA: 0x0027F1D0 File Offset: 0x0027D3D0
			private static SqlExpressionTranslator.SqlToMTranslator.DatePart GetDatePart(IExpression datepart)
			{
				string text = ((IIdentifierExpression)datepart).Name.Name.ToLowerInvariant();
				if (text != null)
				{
					switch (text.Length)
					{
					case 1:
					{
						char c = text[0];
						if (c == 'd')
						{
							return SqlExpressionTranslator.SqlToMTranslator.DatePart.Day;
						}
						switch (c)
						{
						case 'm':
							return SqlExpressionTranslator.SqlToMTranslator.DatePart.Month;
						case 'n':
							return SqlExpressionTranslator.SqlToMTranslator.DatePart.Minute;
						case 'o':
						case 'p':
						case 'r':
							goto IL_048F;
						case 'q':
							return SqlExpressionTranslator.SqlToMTranslator.DatePart.Quarter;
						case 's':
							return SqlExpressionTranslator.SqlToMTranslator.DatePart.Second;
						default:
							if (c != 'y')
							{
								goto IL_048F;
							}
							return SqlExpressionTranslator.SqlToMTranslator.DatePart.Dayofyear;
						}
						break;
					}
					case 2:
					{
						char c = text[1];
						if (c <= 'm')
						{
							if (c != 'd')
							{
								switch (c)
								{
								case 'h':
									if (!(text == "hh"))
									{
										goto IL_048F;
									}
									return SqlExpressionTranslator.SqlToMTranslator.DatePart.Hour;
								case 'i':
									if (!(text == "mi"))
									{
										goto IL_048F;
									}
									return SqlExpressionTranslator.SqlToMTranslator.DatePart.Minute;
								case 'j':
								case 'l':
									goto IL_048F;
								case 'k':
									if (!(text == "wk"))
									{
										goto IL_048F;
									}
									return SqlExpressionTranslator.SqlToMTranslator.DatePart.Week;
								case 'm':
									if (!(text == "mm"))
									{
										goto IL_048F;
									}
									return SqlExpressionTranslator.SqlToMTranslator.DatePart.Month;
								default:
									goto IL_048F;
								}
							}
							else
							{
								if (!(text == "dd"))
								{
									goto IL_048F;
								}
								return SqlExpressionTranslator.SqlToMTranslator.DatePart.Day;
							}
						}
						else if (c != 'q')
						{
							if (c != 's')
							{
								switch (c)
								{
								case 'w':
									if (text == "ww")
									{
										return SqlExpressionTranslator.SqlToMTranslator.DatePart.Week;
									}
									if (!(text == "dw"))
									{
										goto IL_048F;
									}
									return SqlExpressionTranslator.SqlToMTranslator.DatePart.Weekday;
								case 'x':
									goto IL_048F;
								case 'y':
									if (!(text == "yy"))
									{
										if (!(text == "dy"))
										{
											goto IL_048F;
										}
										return SqlExpressionTranslator.SqlToMTranslator.DatePart.Dayofyear;
									}
									break;
								case 'z':
									if (!(text == "tz"))
									{
										goto IL_048F;
									}
									return SqlExpressionTranslator.SqlToMTranslator.DatePart.TZoffset;
								default:
									goto IL_048F;
								}
							}
							else
							{
								if (text == "ss")
								{
									return SqlExpressionTranslator.SqlToMTranslator.DatePart.Second;
								}
								if (text == "ms")
								{
									return SqlExpressionTranslator.SqlToMTranslator.DatePart.Millisecond;
								}
								if (!(text == "ns"))
								{
									goto IL_048F;
								}
								return SqlExpressionTranslator.SqlToMTranslator.DatePart.Nanosecond;
							}
						}
						else
						{
							if (!(text == "qq"))
							{
								goto IL_048F;
							}
							return SqlExpressionTranslator.SqlToMTranslator.DatePart.Quarter;
						}
						break;
					}
					case 3:
					{
						char c = text[0];
						if (c != 'd')
						{
							if (c != 'm')
							{
								goto IL_048F;
							}
							if (!(text == "mcs"))
							{
								goto IL_048F;
							}
							return SqlExpressionTranslator.SqlToMTranslator.DatePart.Microsecond;
						}
						else
						{
							if (!(text == "day"))
							{
								goto IL_048F;
							}
							return SqlExpressionTranslator.SqlToMTranslator.DatePart.Day;
						}
						break;
					}
					case 4:
					{
						char c = text[2];
						if (c <= 'e')
						{
							if (c != 'a')
							{
								if (c != 'e')
								{
									goto IL_048F;
								}
								if (!(text == "week"))
								{
									goto IL_048F;
								}
								return SqlExpressionTranslator.SqlToMTranslator.DatePart.Week;
							}
							else if (!(text == "year"))
							{
								goto IL_048F;
							}
						}
						else if (c != 'u')
						{
							if (c != 'y')
							{
								goto IL_048F;
							}
							if (!(text == "yyyy"))
							{
								goto IL_048F;
							}
						}
						else
						{
							if (!(text == "hour"))
							{
								goto IL_048F;
							}
							return SqlExpressionTranslator.SqlToMTranslator.DatePart.Hour;
						}
						break;
					}
					case 5:
					{
						char c = text[4];
						if (c != 'h')
						{
							if (c != 'k')
							{
								if (c != 'w')
								{
									goto IL_048F;
								}
								if (!(text == "isoww"))
								{
									goto IL_048F;
								}
								return SqlExpressionTranslator.SqlToMTranslator.DatePart.ISO_WEEK;
							}
							else
							{
								if (!(text == "isowk"))
								{
									goto IL_048F;
								}
								return SqlExpressionTranslator.SqlToMTranslator.DatePart.ISO_WEEK;
							}
						}
						else
						{
							if (!(text == "month"))
							{
								goto IL_048F;
							}
							return SqlExpressionTranslator.SqlToMTranslator.DatePart.Month;
						}
						break;
					}
					case 6:
					{
						char c = text[0];
						if (c != 'm')
						{
							if (c != 's')
							{
								goto IL_048F;
							}
							if (!(text == "second"))
							{
								goto IL_048F;
							}
							return SqlExpressionTranslator.SqlToMTranslator.DatePart.Second;
						}
						else
						{
							if (!(text == "minute"))
							{
								goto IL_048F;
							}
							return SqlExpressionTranslator.SqlToMTranslator.DatePart.Minute;
						}
						break;
					}
					case 7:
					{
						char c = text[0];
						if (c != 'q')
						{
							if (c != 'w')
							{
								goto IL_048F;
							}
							if (!(text == "weekday"))
							{
								goto IL_048F;
							}
							return SqlExpressionTranslator.SqlToMTranslator.DatePart.Weekday;
						}
						else
						{
							if (!(text == "quarter"))
							{
								goto IL_048F;
							}
							return SqlExpressionTranslator.SqlToMTranslator.DatePart.Quarter;
						}
						break;
					}
					case 8:
					{
						char c = text[0];
						if (c != 'i')
						{
							if (c != 't')
							{
								goto IL_048F;
							}
							if (!(text == "tzoffset"))
							{
								goto IL_048F;
							}
							return SqlExpressionTranslator.SqlToMTranslator.DatePart.TZoffset;
						}
						else
						{
							if (!(text == "iso_week"))
							{
								goto IL_048F;
							}
							return SqlExpressionTranslator.SqlToMTranslator.DatePart.ISO_WEEK;
						}
						break;
					}
					case 9:
						if (!(text == "dayofyear"))
						{
							goto IL_048F;
						}
						return SqlExpressionTranslator.SqlToMTranslator.DatePart.Dayofyear;
					case 10:
						if (!(text == "nanosecond"))
						{
							goto IL_048F;
						}
						return SqlExpressionTranslator.SqlToMTranslator.DatePart.Nanosecond;
					case 11:
					{
						char c = text[2];
						if (c != 'c')
						{
							if (c != 'l')
							{
								goto IL_048F;
							}
							if (!(text == "millisecond"))
							{
								goto IL_048F;
							}
							return SqlExpressionTranslator.SqlToMTranslator.DatePart.Millisecond;
						}
						else
						{
							if (!(text == "microsecond"))
							{
								goto IL_048F;
							}
							return SqlExpressionTranslator.SqlToMTranslator.DatePart.Microsecond;
						}
						break;
					}
					default:
						goto IL_048F;
					}
					return SqlExpressionTranslator.SqlToMTranslator.DatePart.Year;
				}
				IL_048F:
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "'{0}' is not a recognized datepart option.", text));
			}

			// Token: 0x0600C895 RID: 51349 RVA: 0x0027F684 File Offset: 0x0027D884
			private IExpression NewRound(IList<IExpression> parameters)
			{
				int count = parameters.Count;
				IExpression[] array;
				if (count != 2)
				{
					if (count != 3)
					{
						throw new NotSupportedException();
					}
					IExpression expression = parameters[2];
					IExpression expression2 = new IfExpressionSyntaxNode(BinaryExpressionSyntaxNode.New(BinaryOperator2.Equals, expression, this.NewConstant(0.0), TokenRange.Null), this.NewConstant(this.engine.Null), SqlExpressionTranslator.SqlToMTranslator.NewReference("RoundingMode.TowardZero"), TokenRange.Null);
					array = new IExpression[]
					{
						parameters[0],
						parameters[1],
						expression2
					};
				}
				else
				{
					array = parameters.ToArray<IExpression>();
				}
				ITypeValue roundReturnType = this.GetRoundReturnType(this.GetType(parameters[0]));
				return this.Result(SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Number.Round", array), roundReturnType);
			}

			// Token: 0x0600C896 RID: 51350 RVA: 0x0027F744 File Offset: 0x0027D944
			private ITypeValue GetRoundReturnType(ITypeValue parameter)
			{
				if (this.roundReturnTypeMap == null)
				{
					this.roundReturnTypeMap = new Dictionary<ITypeValue, ITypeValue>
					{
						{
							this.engine.Type(TypeHandle.Int8),
							this.engine.Type(TypeHandle.Int32)
						},
						{
							this.engine.Type(TypeHandle.Byte),
							this.engine.Type(TypeHandle.Int32)
						},
						{
							this.engine.Type(TypeHandle.Int16),
							this.engine.Type(TypeHandle.Int32)
						},
						{
							this.engine.Type(TypeHandle.Int32),
							this.engine.Type(TypeHandle.Int32)
						},
						{
							this.engine.Type(TypeHandle.Int64),
							this.engine.Type(TypeHandle.Int64)
						},
						{
							this.engine.Type(TypeHandle.Currency),
							this.engine.Type(TypeHandle.Currency)
						},
						{
							this.engine.Type(TypeHandle.Decimal),
							this.engine.Type(TypeHandle.Decimal)
						}
					};
				}
				ITypeValue typeValue;
				if (!this.roundReturnTypeMap.TryGetValue(parameter.NonNullable, out typeValue))
				{
					typeValue = this.engine.Type(TypeHandle.Double);
				}
				if (!parameter.IsNullable)
				{
					return typeValue;
				}
				return typeValue.Nullable;
			}

			// Token: 0x0600C897 RID: 51351 RVA: 0x0027F874 File Offset: 0x0027DA74
			private ITypeValue GetSignReturnType(ITypeValue parameter)
			{
				if (this.signReturnTypeMap == null)
				{
					this.signReturnTypeMap = new Dictionary<ITypeValue, ITypeValue>
					{
						{
							this.engine.Type(TypeHandle.Int64),
							this.engine.Type(TypeHandle.Int64)
						},
						{
							this.engine.Type(TypeHandle.Int32),
							this.engine.Type(TypeHandle.Int32)
						},
						{
							this.engine.Type(TypeHandle.Int16),
							this.engine.Type(TypeHandle.Int32)
						},
						{
							this.engine.Type(TypeHandle.Int8),
							this.engine.Type(TypeHandle.Int32)
						},
						{
							this.engine.Type(TypeHandle.Byte),
							this.engine.Type(TypeHandle.Int32)
						},
						{
							this.engine.Type(TypeHandle.Currency),
							this.engine.Type(TypeHandle.Currency)
						},
						{
							this.engine.Type(TypeHandle.Decimal),
							this.engine.Type(TypeHandle.Decimal)
						}
					};
				}
				ITypeValue typeValue;
				if (!this.signReturnTypeMap.TryGetValue(parameter.NonNullable, out typeValue))
				{
					typeValue = this.engine.Type(TypeHandle.Double);
				}
				if (!parameter.IsNullable)
				{
					return typeValue;
				}
				return typeValue.Nullable;
			}

			// Token: 0x0600C898 RID: 51352 RVA: 0x0027F9A4 File Offset: 0x0027DBA4
			public IExpression Translate(SelectStatement select)
			{
				this.types = new Dictionary<IExpression, ITypeValue>();
				this.tables = new Dictionary<Microsoft.Mashup.Engine.Interface.Identifier, SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table>();
				this.identifiers = new Dictionary<Microsoft.Mashup.Engine.Interface.Identifier, MultiPartIdentifier>();
				this.interfaceIdentifiers = new Dictionary<MultiPartIdentifier, Microsoft.Mashup.Engine.Interface.Identifier>(SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.MultiPartIdentifierEqualityComparer.Instance);
				this.identifiersMap = new Dictionary<string, string>();
				this.identifierPrefix = Guid.NewGuid().ToString("N") + ".";
				this.identifierIndex = 1;
				this.rowParameters = new List<Microsoft.Mashup.Engine.Interface.Identifier>();
				WithCtesAndXmlNamespaces withCtesAndXmlNamespaces = select.WithCtesAndXmlNamespaces;
				if (withCtesAndXmlNamespaces != null)
				{
					this.tableDecls = new List<VariableInitializer>(withCtesAndXmlNamespaces.CommonTableExpressions.Count);
					for (int i = 0; i < withCtesAndXmlNamespaces.CommonTableExpressions.Count; i++)
					{
						CommonTableExpression commonTableExpression = withCtesAndXmlNamespaces.CommonTableExpressions[i];
						MultiPartIdentifier multiPartIdentifier = SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.NewIdentifier(commonTableExpression.ExpressionName);
						SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table table = this.Translate(commonTableExpression.QueryExpression, multiPartIdentifier);
						Microsoft.Mashup.Engine.Interface.Identifier identifier = this.NewInterfaceIdentifier(multiPartIdentifier);
						this.tableDecls.Add(new VariableInitializer(identifier, table.Expression));
						this.tables.Add(identifier, new SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table
						{
							Inputs = table.Inputs,
							Columns = table.Columns,
							Expression = SqlExpressionTranslator.SqlToMTranslator.NewReference(identifier)
						});
					}
				}
				SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table table2 = this.Translate(select.QueryExpression, new MultiPartIdentifier());
				return this.AddTableDecls(table2.Expression);
			}

			// Token: 0x0600C899 RID: 51353 RVA: 0x0027FB08 File Offset: 0x0027DD08
			private SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table Translate(QueryExpression query, MultiPartIdentifier prefix)
			{
				SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table table = this.VisitQueryExpression(query);
				if (table.Columns.Count > 0)
				{
					table = this.ReplaceColumnPrefix(table, prefix, prefix.Identifiers.Count == 0);
				}
				table.Expression = InliningVisitor.Inline(this.engine, table.Expression, 5000);
				table.Expression = new LetSimplifier().Simplify(table.Expression);
				return table;
			}

			// Token: 0x0600C89A RID: 51354 RVA: 0x0027FB75 File Offset: 0x0027DD75
			private IExpression AddTableDecls(IExpression expression)
			{
				if (this.tableDecls != null)
				{
					expression = new LetExpressionSyntaxNode(this.tableDecls, expression);
				}
				return expression;
			}

			// Token: 0x0600C89B RID: 51355 RVA: 0x0027FB90 File Offset: 0x0027DD90
			private IExpression NewPrecisionBinaryExpression(BinaryOperator2 op, string func, ITypeValue type, IExpression left, IExpression right)
			{
				IExpression precision = this.GetPrecision(type);
				if (this.IsDecimal(precision))
				{
					return SqlExpressionTranslator.SqlToMTranslator.NewInvocation(func, new IExpression[] { left, right, precision });
				}
				if (func == "Value.NullableEquals")
				{
					return SqlExpressionTranslator.SqlToMTranslator.NewInvocation(func, left, right);
				}
				return BinaryExpressionSyntaxNode.New(op, left, right, TokenRange.Null);
			}

			// Token: 0x0600C89C RID: 51356 RVA: 0x0027FBF0 File Offset: 0x0027DDF0
			private bool TryGetInvocationArguments(IExpression expression, string functionName, out IList<IExpression> arguments)
			{
				IInvocationExpression invocationExpression = expression as IInvocationExpression;
				Microsoft.Mashup.Engine.Interface.Identifier identifier;
				if (invocationExpression != null && SqlExpressionTranslator.SqlToMTranslator.TryGetIdentifier(invocationExpression.Function, out identifier) && identifier.Name == functionName)
				{
					arguments = invocationExpression.Arguments;
					return true;
				}
				arguments = null;
				return false;
			}

			// Token: 0x0600C89D RID: 51357 RVA: 0x0027FC34 File Offset: 0x0027DE34
			private bool TryGetBinaryCombineArguments(IExpression expression, out IList<IExpression> arguments)
			{
				IList<IExpression> list;
				if (this.TryGetInvocationArguments(expression, "Binary.Combine", out list) && list.Count == 1)
				{
					IListExpression listExpression = list[0] as IListExpression;
					if (listExpression != null)
					{
						arguments = listExpression.Members;
						return true;
					}
				}
				arguments = null;
				return false;
			}

			// Token: 0x0600C89E RID: 51358 RVA: 0x0027FC78 File Offset: 0x0027DE78
			protected override IExpression NewBinaryExpression(BinaryExpressionType type, IExpression left, IExpression right)
			{
				ITypeValue typeValue = this.GetType(left);
				ITypeValue typeValue2 = this.GetType(right);
				ITypeValue typeValue3 = this.UnionTypes(typeValue, typeValue2);
				if (type != BinaryExpressionType.Add)
				{
					if (type == BinaryExpressionType.Subtract)
					{
						if ((this.IsDateType(typeValue) || this.IsDateTimeType(typeValue)) && this.IsNumericType(typeValue2))
						{
							right = this.NewCastCall(SqlExpressionTranslator.SqlToMTranslator.DurationTypeReference, right);
							typeValue2 = this.engine.Type(TypeHandle.Duration);
							typeValue3 = typeValue;
						}
						else if ((this.IsDateType(typeValue2) || this.IsDateTimeType(typeValue2)) && this.IsNumericType(typeValue))
						{
							right = this.NewCastCall(SqlExpressionTranslator.SqlToMTranslator.DateTimeTypeReference, right);
							typeValue2 = this.engine.Type(TypeHandle.Duration);
							typeValue3 = typeValue2;
						}
					}
				}
				else if ((this.IsDateType(typeValue) || this.IsDateTimeType(typeValue)) && this.IsNumericType(typeValue2))
				{
					right = this.NewCastCall(SqlExpressionTranslator.SqlToMTranslator.DurationTypeReference, right);
					typeValue2 = this.engine.Type(TypeHandle.Duration);
					typeValue3 = typeValue;
				}
				else if ((this.IsDateType(typeValue2) || this.IsDateTimeType(typeValue2)) && this.IsNumericType(typeValue))
				{
					left = this.NewCastCall(SqlExpressionTranslator.SqlToMTranslator.DurationTypeReference, left);
					typeValue = this.engine.Type(TypeHandle.Duration);
					typeValue3 = typeValue2;
				}
				IExpression expression;
				switch (type)
				{
				case BinaryExpressionType.Add:
					if (typeValue3.IsCompatibleWith(this.engine.Type(TypeHandle.Text).Nullable))
					{
						expression = this.Result(BinaryExpressionSyntaxNode.New(BinaryOperator2.Concatenate, left, right, TokenRange.Null), typeValue3);
					}
					else if (typeValue.IsCompatibleWith(this.engine.Type(TypeHandle.Binary).Nullable) && typeValue2.IsCompatibleWith(this.engine.Type(TypeHandle.Binary).Nullable))
					{
						List<IExpression> list = new List<IExpression>();
						IList<IExpression> list2;
						if (this.TryGetBinaryCombineArguments(left, out list2))
						{
							list.AddRange(list2);
						}
						else
						{
							list.Add(left);
						}
						IList<IExpression> list3;
						if (this.TryGetBinaryCombineArguments(right, out list3))
						{
							list.AddRange(list3);
						}
						else
						{
							list.Add(right);
						}
						expression = SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Binary.Combine", this.NewList(list));
					}
					else
					{
						expression = this.NewPrecisionBinaryExpression(BinaryOperator2.Add, "Value.Add", typeValue3, left, right);
					}
					break;
				case BinaryExpressionType.Subtract:
					expression = this.NewPrecisionBinaryExpression(BinaryOperator2.Subtract, "Value.Subtract", typeValue3, left, right);
					break;
				case BinaryExpressionType.Multiply:
					expression = this.NewPrecisionBinaryExpression(BinaryOperator2.Multiply, "Value.Multiply", typeValue3, left, right);
					break;
				case BinaryExpressionType.Divide:
					expression = this.NewPrecisionBinaryExpression(BinaryOperator2.Divide, "Value.Divide", typeValue3, left, right);
					break;
				case BinaryExpressionType.Modulo:
				{
					IExpression precision = this.GetPrecision(typeValue3);
					if (this.IsDecimal(precision))
					{
						expression = SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Number.Mod", new IExpression[] { left, right, precision });
					}
					else
					{
						expression = SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Number.Mod", left, right);
					}
					break;
				}
				case BinaryExpressionType.BitwiseAnd:
					expression = SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Number.BitwiseAnd", left, right);
					break;
				case BinaryExpressionType.BitwiseOr:
					expression = SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Number.BitwiseOr", left, right);
					break;
				case BinaryExpressionType.BitwiseXor:
					expression = SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Number.BitwiseXor", left, right);
					break;
				default:
					throw new NotSupportedException();
				}
				return this.Result(expression, typeValue3);
			}

			// Token: 0x0600C89F RID: 51359 RVA: 0x0027FF5C File Offset: 0x0027E15C
			protected override IExpression NewBooleanBinaryExpression(BooleanBinaryExpressionType type, IExpression left, IExpression right)
			{
				BinaryOperator2 binaryOperator;
				if (type != BooleanBinaryExpressionType.And)
				{
					if (type != BooleanBinaryExpressionType.Or)
					{
						throw new NotSupportedException();
					}
					binaryOperator = BinaryOperator2.Or;
				}
				else
				{
					binaryOperator = BinaryOperator2.And;
				}
				IExpression expression;
				if (this.TryGetSqlEquals(binaryOperator, left, right, out expression))
				{
					return this.Result(expression, this.engine.Type(TypeHandle.Logical));
				}
				return this.Result(BinaryExpressionSyntaxNode.New(binaryOperator, left, right, TokenRange.Null), this.UnionTypes(this.GetType(left), this.GetType(right)));
			}

			// Token: 0x0600C8A0 RID: 51360 RVA: 0x0027FFCC File Offset: 0x0027E1CC
			private bool TryGetSqlEquals(BinaryOperator2 binaryOperator, IExpression binaryLeft, IExpression binaryRight, out IExpression result)
			{
				if (binaryOperator == BinaryOperator2.Or && binaryLeft.Kind == ExpressionKind.Invocation && binaryRight.Kind == ExpressionKind.Binary)
				{
					IInvocationExpression invocationExpression = binaryLeft as IInvocationExpression;
					IBinaryExpression binaryExpression = binaryRight as IBinaryExpression;
					Microsoft.Mashup.Engine.Interface.Identifier identifier;
					if (SqlExpressionTranslator.SqlToMTranslator.TryGetIdentifier(invocationExpression.Function, out identifier) && identifier.Name == "Value.NullableEquals" && (invocationExpression.Arguments.Count == 2 || invocationExpression.Arguments.Count == 3) && binaryExpression.Operator == BinaryOperator2.And && binaryExpression.Left.Kind == ExpressionKind.Binary && binaryExpression.Right.Kind == ExpressionKind.Binary)
					{
						IBinaryExpression binaryExpression2 = binaryExpression.Left as IBinaryExpression;
						IBinaryExpression binaryExpression3 = binaryExpression.Right as IBinaryExpression;
						ExpressionComparer expressionComparer = new ExpressionComparer(false);
						IValue value;
						IValue value2;
						if (binaryExpression2.Operator == BinaryOperator2.Equals && SqlExpressionTranslator.SqlToMTranslator.TryGetConstant(binaryExpression2.Right, out value) && value.IsNull && binaryExpression3.Operator == BinaryOperator2.Equals && SqlExpressionTranslator.SqlToMTranslator.TryGetConstant(binaryExpression3.Right, out value2) && value2.IsNull && expressionComparer.Equals(invocationExpression.Arguments[0], binaryExpression2.Left) && expressionComparer.Equals(invocationExpression.Arguments[1], binaryExpression3.Left))
						{
							ITypeValue typeValue = this.engine.Type(TypeHandle.Double);
							if (invocationExpression.Arguments.Count == 3 && this.IsDecimal(invocationExpression.Arguments[2]))
							{
								typeValue = this.engine.Type(TypeHandle.Decimal);
							}
							result = this.NewPrecisionBinaryExpression(BinaryOperator2.Equals, "Value.Equals", typeValue, invocationExpression.Arguments[0], invocationExpression.Arguments[1]);
							return true;
						}
					}
				}
				result = null;
				return false;
			}

			// Token: 0x0600C8A1 RID: 51361 RVA: 0x0028019C File Offset: 0x0027E39C
			protected override IExpression NewBooleanComparisonExpression(BooleanComparisonType type, IExpression left, IExpression right)
			{
				ITypeValue typeValue = this.UnionTypesAndPromoteExpressions(ref left, ref right);
				IExpression expression;
				switch (type)
				{
				case BooleanComparisonType.Equals:
					expression = this.NewPrecisionBinaryExpression(BinaryOperator2.Equals, "Value.NullableEquals", typeValue, left, right);
					break;
				case BooleanComparisonType.GreaterThan:
					expression = BinaryExpressionSyntaxNode.New(BinaryOperator2.GreaterThan, left, right, TokenRange.Null);
					break;
				case BooleanComparisonType.LessThan:
					expression = BinaryExpressionSyntaxNode.New(BinaryOperator2.LessThan, left, right, TokenRange.Null);
					break;
				case BooleanComparisonType.GreaterThanOrEqualTo:
				case BooleanComparisonType.NotLessThan:
					expression = BinaryExpressionSyntaxNode.New(BinaryOperator2.GreaterThanOrEquals, left, right, TokenRange.Null);
					break;
				case BooleanComparisonType.LessThanOrEqualTo:
				case BooleanComparisonType.NotGreaterThan:
					expression = BinaryExpressionSyntaxNode.New(BinaryOperator2.LessThanOrEquals, left, right, TokenRange.Null);
					break;
				case BooleanComparisonType.NotEqualToBrackets:
				case BooleanComparisonType.NotEqualToExclamation:
					expression = UnaryExpressionSyntaxNode.New(UnaryOperator2.Not, this.NewPrecisionBinaryExpression(BinaryOperator2.Equals, "Value.NullableEquals", typeValue, left, right), TokenRange.Null);
					break;
				default:
					throw new NotSupportedException();
				}
				return this.Result(expression, this.engine.Type(TypeHandle.Logical));
			}

			// Token: 0x0600C8A2 RID: 51362 RVA: 0x0028026C File Offset: 0x0027E46C
			protected override IExpression NewBooleanIsNullExpression(bool isNot, IExpression expression)
			{
				BinaryOperator2 binaryOperator = (isNot ? BinaryOperator2.NotEquals : BinaryOperator2.Equals);
				return this.Result(BinaryExpressionSyntaxNode.New(binaryOperator, expression, this.NewConstant(this.engine.Null), TokenRange.Null), this.engine.Type(TypeHandle.Logical));
			}

			// Token: 0x0600C8A3 RID: 51363 RVA: 0x002802B2 File Offset: 0x0027E4B2
			protected override IExpression NewBooleanNotExpression(IExpression expression)
			{
				return this.Result(UnaryExpressionSyntaxNode.New(UnaryOperator2.Not, expression, TokenRange.Null), this.GetType(expression));
			}

			// Token: 0x0600C8A4 RID: 51364 RVA: 0x0000A6A5 File Offset: 0x000088A5
			protected override IExpression NewBooleanParenthesisExpression(IExpression expression)
			{
				return expression;
			}

			// Token: 0x0600C8A5 RID: 51365 RVA: 0x002802D0 File Offset: 0x0027E4D0
			protected override IExpression NewCastCall(IExpression dataType, IExpression expression)
			{
				IRecordExpression recordExpression = null;
				IInvocationExpression invocationExpression = dataType as IInvocationExpression;
				Microsoft.Mashup.Engine.Interface.Identifier identifier;
				if (invocationExpression != null && invocationExpression.Arguments.Count == 2 && SqlExpressionTranslator.SqlToMTranslator.TryGetIdentifier(invocationExpression.Function, out identifier) && identifier.Name == "Type.ReplaceFacets")
				{
					recordExpression = invocationExpression.Arguments[1] as IRecordExpression;
					if (recordExpression != null)
					{
						dataType = invocationExpression.Arguments[0];
					}
				}
				if (SqlExpressionTranslator.SqlToMTranslator.TryGetIdentifier(dataType, out identifier))
				{
					string name = identifier.Name;
					if (name != null)
					{
						IExpression expression2;
						ITypeValue typeValue;
						switch (name.Length)
						{
						case 9:
						{
							char c = name[1];
							if (c <= 'e')
							{
								if (c != 'a')
								{
									if (c != 'e')
									{
										goto IL_057E;
									}
									if (!(name == "Text.Type"))
									{
										goto IL_057E;
									}
									IValue value;
									expression2 = ((this.TryGetNodeExpressionValue(expression, out value) && value.IsBinary) ? SqlExpressionTranslator.SqlToMTranslator.NewInvocation(SqlExpressionTranslator.SqlToMTranslator.TextFromBinary, expression) : SqlExpressionTranslator.SqlToMTranslator.NewInvocation(SqlExpressionTranslator.SqlToMTranslator.TextFromReference, expression));
									typeValue = this.engine.Type(TypeHandle.Text);
									IExpression facet = SqlExpressionTranslator.SqlToMTranslator.GetFacet(recordExpression, "MaxLength");
									if (facet != null)
									{
										expression2 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Text.Start", expression2, facet);
									}
								}
								else
								{
									if (!(name == "Date.Type"))
									{
										goto IL_057E;
									}
									expression2 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation(SqlExpressionTranslator.SqlToMTranslator.DateFromReference, expression);
									typeValue = this.engine.Type(TypeHandle.Date);
								}
							}
							else if (c != 'i')
							{
								if (c != 'n')
								{
									goto IL_057E;
								}
								if (!(name == "Int8.Type"))
								{
									goto IL_057E;
								}
								expression2 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation(SqlExpressionTranslator.SqlToMTranslator.Int8FromReference, expression);
								typeValue = this.engine.Type(TypeHandle.Int8);
							}
							else
							{
								if (!(name == "Time.Type"))
								{
									goto IL_057E;
								}
								expression2 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation(SqlExpressionTranslator.SqlToMTranslator.TimeFromReference, expression);
								typeValue = this.engine.Type(TypeHandle.Time);
							}
							break;
						}
						case 10:
						{
							char c = name[3];
							if (c != '1')
							{
								if (c != '3')
								{
									if (c != '6')
									{
										goto IL_057E;
									}
									if (!(name == "Int64.Type"))
									{
										goto IL_057E;
									}
									expression2 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation(SqlExpressionTranslator.SqlToMTranslator.Int64FromReference, expression);
									typeValue = this.engine.Type(TypeHandle.Int64);
								}
								else
								{
									if (!(name == "Int32.Type"))
									{
										goto IL_057E;
									}
									expression2 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation(SqlExpressionTranslator.SqlToMTranslator.Int32FromReference, expression);
									typeValue = this.engine.Type(TypeHandle.Int32);
								}
							}
							else
							{
								if (!(name == "Int16.Type"))
								{
									goto IL_057E;
								}
								expression2 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation(SqlExpressionTranslator.SqlToMTranslator.Int16FromReference, expression);
								typeValue = this.engine.Type(TypeHandle.Int16);
							}
							break;
						}
						case 11:
						{
							char c = name[0];
							if (c != 'B')
							{
								if (c != 'D')
								{
									if (c != 'S')
									{
										goto IL_057E;
									}
									if (!(name == "Single.Type"))
									{
										goto IL_057E;
									}
									expression2 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation(SqlExpressionTranslator.SqlToMTranslator.SingleFromReference, expression);
									typeValue = this.engine.Type(TypeHandle.Single);
								}
								else
								{
									if (!(name == "Double.Type"))
									{
										goto IL_057E;
									}
									expression2 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation(SqlExpressionTranslator.SqlToMTranslator.DoubleFromReference, expression);
									typeValue = this.engine.Type(TypeHandle.Double);
								}
							}
							else
							{
								if (!(name == "Binary.Type"))
								{
									goto IL_057E;
								}
								IValue value;
								expression2 = ((this.TryGetNodeExpressionValue(expression, out value) && value.IsText) ? SqlExpressionTranslator.SqlToMTranslator.NewInvocation(SqlExpressionTranslator.SqlToMTranslator.TextToBinary, expression) : SqlExpressionTranslator.SqlToMTranslator.NewInvocation(SqlExpressionTranslator.SqlToMTranslator.BinaryFromReference, expression));
								typeValue = this.engine.Type(TypeHandle.Binary);
							}
							break;
						}
						case 12:
						{
							char c = name[0];
							if (c != 'D')
							{
								if (c != 'L')
								{
									goto IL_057E;
								}
								if (!(name == "Logical.Type"))
								{
									goto IL_057E;
								}
								expression2 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation(SqlExpressionTranslator.SqlToMTranslator.LogicalFromReference, expression);
								typeValue = this.engine.Type(TypeHandle.Logical);
							}
							else
							{
								if (!(name == "Decimal.Type"))
								{
									goto IL_057E;
								}
								expression2 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation(SqlExpressionTranslator.SqlToMTranslator.DecimalFromReference, expression);
								typeValue = this.engine.Type(TypeHandle.Decimal);
							}
							break;
						}
						case 13:
						{
							char c = name[3];
							if (c != 'a')
							{
								if (c != 'e')
								{
									if (c != 'r')
									{
										goto IL_057E;
									}
									if (!(name == "Currency.Type"))
									{
										goto IL_057E;
									}
									expression2 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation(SqlExpressionTranslator.SqlToMTranslator.CurrencyFromReference, expression);
									typeValue = this.engine.Type(TypeHandle.Currency);
								}
								else
								{
									if (!(name == "DateTime.Type"))
									{
										goto IL_057E;
									}
									expression2 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation(SqlExpressionTranslator.SqlToMTranslator.DateTimeFromReference, expression);
									typeValue = this.engine.Type(TypeHandle.DateTime);
								}
							}
							else
							{
								if (!(name == "Duration.Type"))
								{
									goto IL_057E;
								}
								expression2 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation(SqlExpressionTranslator.SqlToMTranslator.DurationFromReference, expression);
								typeValue = this.engine.Type(TypeHandle.Duration);
							}
							break;
						}
						case 14:
						case 16:
							goto IL_057E;
						case 15:
							if (!(name == "Percentage.Type"))
							{
								goto IL_057E;
							}
							expression2 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation(SqlExpressionTranslator.SqlToMTranslator.PercentageFromReference, expression);
							typeValue = this.engine.Type(TypeHandle.Percentage);
							break;
						case 17:
							if (!(name == "DateTimeZone.Type"))
							{
								goto IL_057E;
							}
							expression2 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation(SqlExpressionTranslator.SqlToMTranslator.DateTimeZoneFromReference, expression);
							typeValue = this.engine.Type(TypeHandle.DateTimeZone);
							break;
						default:
							goto IL_057E;
						}
						if (this.GetType(expression).IsNullable)
						{
							typeValue = typeValue.Nullable;
						}
						return this.Result(expression2, typeValue);
					}
					IL_057E:
					throw new NotSupportedException();
				}
				throw new NotSupportedException();
			}

			// Token: 0x0600C8A6 RID: 51366 RVA: 0x00280887 File Offset: 0x0027EA87
			private bool TryGetNodeExpressionValue(IExpression expression, out IValue value)
			{
				value = null;
				return expression.Kind == ExpressionKind.FieldAccess && this.environment.TryGetValue(((IFieldAccessExpression)expression).MemberName, out value);
			}

			// Token: 0x0600C8A7 RID: 51367 RVA: 0x002808B4 File Offset: 0x0027EAB4
			private static IExpression GetFacet(IRecordExpression facets, string name)
			{
				if (facets == null)
				{
					return null;
				}
				return facets.Members.Where((VariableInitializer m) => m.Name.Name == name).FirstOrDefault<VariableInitializer>().Value;
			}

			// Token: 0x0600C8A8 RID: 51368 RVA: 0x002808F8 File Offset: 0x0027EAF8
			protected override IExpression NewCoalesceExpression(IList<IExpression> expressions)
			{
				IExpression expression = null;
				for (int i = expressions.Count - 1; i >= 0; i--)
				{
					if (expression == null)
					{
						expression = expressions[i];
					}
					else
					{
						IExpression expression2 = expressions[i];
						IExpression expression3 = expression;
						ITypeValue typeValue = this.UnionTypesAndPromoteExpressions(ref expression2, ref expression3);
						expression = this.Result(new IfExpressionSyntaxNode(BinaryExpressionSyntaxNode.New(BinaryOperator2.NotEquals, expressions[i], this.NewConstant(this.engine.Null), TokenRange.Null), expression2, expression3, TokenRange.Null), typeValue);
					}
				}
				if (expression == null)
				{
					throw new NotSupportedException();
				}
				return expression;
			}

			// Token: 0x0600C8A9 RID: 51369 RVA: 0x00280980 File Offset: 0x0027EB80
			protected override IExpression NewColumnReferenceExpression(ColumnType type, MultiPartIdentifier identifier)
			{
				if (type != ColumnType.Regular)
				{
					if (type == ColumnType.Wildcard)
					{
						return this.Result(SqlExpressionTranslator.SqlToMTranslator.NewReference(this.RowParameter), this.engine.Type(TypeHandle.Table));
					}
					throw new NotSupportedException();
				}
				else
				{
					SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding columnBinding;
					if (!base.Context.TryGetBinding(identifier, out columnBinding))
					{
						return this.Result(SqlExpressionTranslator.SqlToMTranslator.NewReference(this.NewInterfaceIdentifier(identifier)), this.engine.Type(TypeHandle.Any));
					}
					return this.Result(new RequiredFieldAccessExpressionSyntaxNode(SqlExpressionTranslator.SqlToMTranslator.NewReference(columnBinding.Value.RowParameter), this.NewInterfaceIdentifier(columnBinding.Identifier)), columnBinding.Value.Type);
				}
			}

			// Token: 0x0600C8AA RID: 51370 RVA: 0x00280A21 File Offset: 0x0027EC21
			protected override IExpression NewConvertCall(IExpression dataType, IExpression expression)
			{
				return this.NewCastCall(dataType, expression);
			}

			// Token: 0x0600C8AB RID: 51371 RVA: 0x00280A2B File Offset: 0x0027EC2B
			protected override SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table NewComputedColumnsClause(SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table table, IList<Tuple<Microsoft.Mashup.ScriptDom.Identifier, IExpression>> computedColumns)
			{
				return this.NewGroupByClause(table, new IExpression[0], computedColumns);
			}

			// Token: 0x0600C8AC RID: 51372 RVA: 0x00280A3C File Offset: 0x0027EC3C
			protected override SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table NewDistinctClause(SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table table)
			{
				return new SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table
				{
					Inputs = table.Inputs,
					Columns = table.Columns,
					Expression = SqlExpressionTranslator.SqlToMTranslator.AddStep((ILetExpression)table.Expression, new FunctionExpressionSyntaxNode(SqlExpressionTranslator.SqlToMTranslator.tableFunctionType, SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Table.Distinct", SqlExpressionTranslator.SqlToMTranslator.NewReference(SqlExpressionTranslator.SqlToMTranslator.tableFunctionType.Parameters[0].Identifier))))
				};
			}

			// Token: 0x0600C8AD RID: 51373 RVA: 0x00280AAC File Offset: 0x0027ECAC
			protected override IExpression NewExpressionWithSortOrder(SortOrder sortOrder, IExpression expression)
			{
				IExpression expression2;
				if (sortOrder > SortOrder.Ascending)
				{
					if (sortOrder != SortOrder.Descending)
					{
						throw new NotSupportedException();
					}
					expression2 = SqlExpressionTranslator.SqlToMTranslator.NewReference("Order.Descending");
				}
				else
				{
					expression2 = SqlExpressionTranslator.SqlToMTranslator.NewReference("Order.Ascending");
				}
				return this.Result(new ListExpressionSyntaxNode(new IExpression[] { expression, expression2 }), this.engine.Type(TypeHandle.List));
			}

			// Token: 0x0600C8AE RID: 51374 RVA: 0x00280B08 File Offset: 0x0027ED08
			protected override SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table NewFromClause(IList<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table> tables)
			{
				if (tables == null)
				{
					tables = new SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table[]
					{
						new SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table
						{
							Inputs = new SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table[0],
							Columns = new SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding[0],
							Expression = SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Table.FromRows", new ListExpressionSyntaxNode(new IExpression[] { SqlExpressionTranslator.SqlToMTranslator.EmptyList }), SqlExpressionTranslator.SqlToMTranslator.EmptyList)
						}
					};
				}
				List<VariableInitializer> list = new List<VariableInitializer>();
				for (int i = 0; i < tables.Count; i++)
				{
					list.Add(new VariableInitializer(Microsoft.Mashup.Engine.Interface.Identifier.New(), tables[i].Expression));
				}
				IList<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding> list2 = tables[0].Columns;
				for (int j = 1; j < tables.Count; j++)
				{
					int num = ((j == 1) ? 0 : (list.Count - 1));
					list.Add(new VariableInitializer(Microsoft.Mashup.Engine.Interface.Identifier.New(), SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Table.Join", new IExpression[]
					{
						SqlExpressionTranslator.SqlToMTranslator.NewReference(list[num].Name),
						SqlExpressionTranslator.SqlToMTranslator.EmptyList,
						SqlExpressionTranslator.SqlToMTranslator.NewReference(list[j].Name),
						SqlExpressionTranslator.SqlToMTranslator.EmptyList
					})));
					list2 = SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ConcatenateColumns(list2, tables[j].Columns);
				}
				return new SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table
				{
					Inputs = this.FlattenTableInputs(tables),
					Columns = list2,
					Expression = new LetExpressionSyntaxNode(list, SqlExpressionTranslator.SqlToMTranslator.NewReference(list[list.Count - 1].Name))
				};
			}

			// Token: 0x0600C8AF RID: 51375 RVA: 0x00280C90 File Offset: 0x0027EE90
			protected override IExpression NewFunctionCall(Microsoft.Mashup.ScriptDom.Identifier function, UniqueRowFilter uniqueRowFilter, IList<IExpression> parameters)
			{
				string text = function.Value.Trim();
				bool flag = false;
				if (uniqueRowFilter == UniqueRowFilter.Distinct && parameters.Count == 1)
				{
					flag = true;
				}
				else if (uniqueRowFilter != UniqueRowFilter.NotSpecified)
				{
					throw new NotSupportedException("Function " + text + " with distinct rows is not supported.");
				}
				Func<IList<IExpression>, IExpression> func;
				if (!flag && this.functionMap.TryGetValue(text, out func))
				{
					return func(parameters);
				}
				Func<bool, IList<IExpression>, IExpression> func2;
				if (this.aggregateFunctionMap.TryGetValue(text, out func2))
				{
					return func2(flag, parameters);
				}
				throw new NotSupportedException("Function " + text + " is not supported.");
			}

			// Token: 0x0600C8B0 RID: 51376 RVA: 0x00280D20 File Offset: 0x0027EF20
			private bool IsDecimal(IExpression precision)
			{
				Microsoft.Mashup.Engine.Interface.Identifier identifier;
				return SqlExpressionTranslator.SqlToMTranslator.TryGetIdentifier(precision, out identifier) && identifier.Name == "Precision.Decimal";
			}

			// Token: 0x0600C8B1 RID: 51377 RVA: 0x00280D4C File Offset: 0x0027EF4C
			private IExpression GetPrecision(ITypeValue type)
			{
				if (type.NonNullable.Equals(this.engine.Type(TypeHandle.Decimal)) || type.NonNullable.Equals(this.engine.Type(TypeHandle.Currency)))
				{
					return SqlExpressionTranslator.SqlToMTranslator.NewReference("Precision.Decimal");
				}
				return SqlExpressionTranslator.SqlToMTranslator.NewReference("Precision.Double");
			}

			// Token: 0x0600C8B2 RID: 51378 RVA: 0x00280DA4 File Offset: 0x0027EFA4
			private void AddColumn(List<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding> columns, Microsoft.Mashup.Engine.Interface.Identifier name, ITypeValue type)
			{
				columns.Add(new SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding
				{
					Identifier = this.GetIdentifier(name),
					Value = new SqlExpressionTranslator.RowParameterAndType
					{
						Type = type
					}
				});
			}

			// Token: 0x0600C8B3 RID: 51379 RVA: 0x00280DE8 File Offset: 0x0027EFE8
			private void RenameColumn(List<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding> columns, MultiPartIdentifier from, MultiPartIdentifier to)
			{
				for (int i = 0; i < columns.Count; i++)
				{
					SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding columnBinding = columns[i];
					if (SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.MultiPartIdentifierEqualityComparer.Instance.Equals(columnBinding.Identifier, from))
					{
						columns[i] = new SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding
						{
							Identifier = to,
							Value = columnBinding.Value
						};
						return;
					}
				}
			}

			// Token: 0x0600C8B4 RID: 51380 RVA: 0x00280E4C File Offset: 0x0027F04C
			protected override SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table NewGroupByClause(SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table table, IList<IExpression> groupingSpecs, IList<Tuple<Microsoft.Mashup.ScriptDom.Identifier, IExpression>> computedColumns)
			{
				List<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding> list = new List<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding>(table.Columns);
				List<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding> list2 = new List<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding>();
				List<VariableInitializer> list3 = new List<VariableInitializer>();
				SqlExpressionTranslator.SqlToMTranslator.AddStep(list3, SqlExpressionTranslator.SqlToMTranslator.NewReference(SqlExpressionTranslator.SqlToMTranslator.tableFunctionType.Parameters[0].Identifier));
				List<IExpression> list4 = new List<IExpression>();
				for (int i = 0; i < groupingSpecs.Count; i++)
				{
					IFunctionExpression functionExpression = (IFunctionExpression)groupingSpecs[i];
					Microsoft.Mashup.Engine.Interface.Identifier identifier;
					if (!SqlExpressionTranslator.SqlToMTranslator.TryGetColumnReference(functionExpression.FunctionType.Parameters[0].Identifier, functionExpression.Expression, out identifier))
					{
						identifier = this.NewInterfaceIdentifier(SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.NewIdentifier(this.NewColumnIdentifier()));
						IExpression expression = new FunctionExpressionSyntaxNode(SqlExpressionTranslator.SqlToMTranslator.tableFunctionType, SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Table.AddColumn", new IExpression[]
						{
							SqlExpressionTranslator.SqlToMTranslator.NewReference(SqlExpressionTranslator.SqlToMTranslator.tableFunctionType.Parameters[0].Identifier),
							this.NewConstant(this.engine.Text(identifier.Name)),
							functionExpression,
							this.NewConstant(this.GetType(functionExpression.Expression))
						}));
						SqlExpressionTranslator.SqlToMTranslator.AddStep(list3, expression);
					}
					list4.Add(this.NewConstant(this.engine.Text(identifier.Name)));
					this.AddColumn(list2, identifier, this.GetType(functionExpression.Expression));
				}
				bool flag = list4.Count > 0;
				bool flag2 = false;
				IExpression[] array = new IExpression[computedColumns.Count];
				IDictionary<Microsoft.Mashup.Engine.Interface.Identifier, IExpression>[] array2 = new IDictionary<Microsoft.Mashup.Engine.Interface.Identifier, IExpression>[computedColumns.Count];
				for (int j = 0; j < computedColumns.Count; j++)
				{
					IExpression expression2;
					IDictionary<Microsoft.Mashup.Engine.Interface.Identifier, IExpression> dictionary;
					bool? flag3 = this.IsAggregate(list4, groupingSpecs, computedColumns[j].Item2, out expression2, out dictionary);
					bool flag4 = flag;
					bool? flag5 = flag3;
					bool flag6 = true;
					flag = flag4 | ((flag5.GetValueOrDefault() == flag6) & (flag5 != null));
					bool flag7 = flag2;
					flag5 = flag3;
					flag6 = false;
					flag2 = flag7 | ((flag5.GetValueOrDefault() == flag6) & (flag5 != null));
					if (flag && flag2)
					{
						throw new InvalidOperationException("Can't mix aggregate and non-aggregate expressions.");
					}
					Microsoft.Mashup.Engine.Interface.Identifier identifier2 = this.NewInterfaceIdentifier(SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.NewIdentifier(computedColumns[j].Item1)).Name;
					ITypeValue type = this.GetType(((IFunctionExpression)expression2).Expression);
					array[j] = new ListExpressionSyntaxNode(new IExpression[]
					{
						this.NewConstant(this.engine.Text(identifier2.Name)),
						expression2
					});
					array2[j] = dictionary;
					this.AddColumn(list, identifier2, type);
					this.AddColumn(list2, identifier2, type);
				}
				for (int k = 0; k < array2.Length; k++)
				{
					if (array2[k] != null)
					{
						foreach (KeyValuePair<Microsoft.Mashup.Engine.Interface.Identifier, IExpression> keyValuePair in array2[k])
						{
							IFunctionExpression functionExpression2 = (IFunctionExpression)keyValuePair.Value;
							ITypeValue type2 = this.GetType(functionExpression2.Expression);
							IExpression expression3 = new FunctionExpressionSyntaxNode(SqlExpressionTranslator.SqlToMTranslator.tableFunctionType, SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Table.AddColumn", new IExpression[]
							{
								SqlExpressionTranslator.SqlToMTranslator.NewReference(SqlExpressionTranslator.SqlToMTranslator.tableFunctionType.Parameters[0].Identifier),
								this.NewConstant(this.engine.Text(keyValuePair.Key.Name)),
								functionExpression2,
								this.NewConstant(type2)
							}));
							SqlExpressionTranslator.SqlToMTranslator.AddStep(list3, expression3);
						}
					}
				}
				List<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding> list5;
				if (flag)
				{
					IExpression expression4 = new FunctionExpressionSyntaxNode(SqlExpressionTranslator.SqlToMTranslator.tableFunctionType, SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Table.Group", new IExpression[]
					{
						SqlExpressionTranslator.SqlToMTranslator.NewReference(SqlExpressionTranslator.SqlToMTranslator.tableFunctionType.Parameters[0].Identifier),
						new ListExpressionSyntaxNode(list4),
						new ListExpressionSyntaxNode(array)
					}));
					SqlExpressionTranslator.SqlToMTranslator.AddStep(list3, expression4);
					list5 = list2;
				}
				else
				{
					List<IExpression> list6 = new List<IExpression>();
					HashSet<string> hashSet = new HashSet<string>(StringComparer.Ordinal);
					for (int l = 0; l < array.Length; l++)
					{
						IExpression expression5 = ((IListExpression)array[l]).Members[0];
						IFunctionExpression functionExpression3 = (IFunctionExpression)((IListExpression)array[l]).Members[1];
						Microsoft.Mashup.Engine.Interface.Identifier identifier3;
						if (SqlExpressionTranslator.SqlToMTranslator.TryGetColumnReference(functionExpression3.FunctionType.Parameters[0].Identifier, functionExpression3.Expression, out identifier3) && hashSet.Add(identifier3.Name))
						{
							list6.Add(new ListExpressionSyntaxNode(new IExpression[]
							{
								this.NewConstant(this.engine.Text(identifier3.Name)),
								expression5
							}));
						}
						else
						{
							ITypeValue type3 = this.GetType(functionExpression3.Expression);
							IExpression expression6 = new FunctionExpressionSyntaxNode(SqlExpressionTranslator.SqlToMTranslator.tableFunctionType, SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Table.AddColumn", new IExpression[]
							{
								SqlExpressionTranslator.SqlToMTranslator.NewReference(SqlExpressionTranslator.SqlToMTranslator.tableFunctionType.Parameters[0].Identifier),
								expression5,
								functionExpression3,
								this.NewConstant(type3)
							}));
							SqlExpressionTranslator.SqlToMTranslator.AddStep(list3, expression6);
						}
					}
					if (list6.Count > 0)
					{
						IExpression expression7 = new FunctionExpressionSyntaxNode(SqlExpressionTranslator.SqlToMTranslator.tableFunctionType, SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Table.RenameColumns", SqlExpressionTranslator.SqlToMTranslator.NewReference(SqlExpressionTranslator.SqlToMTranslator.tableFunctionType.Parameters[0].Identifier), new ListExpressionSyntaxNode(list6)));
						SqlExpressionTranslator.SqlToMTranslator.AddStep(list3, expression7);
					}
					list5 = list;
				}
				IExpression expression8;
				if (list3.Count == 1)
				{
					expression8 = list3.Last<VariableInitializer>().Value;
				}
				else
				{
					expression8 = new LetExpressionSyntaxNode(list3, SqlExpressionTranslator.SqlToMTranslator.NewReference(list3.Last<VariableInitializer>().Name));
				}
				return new SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table
				{
					Inputs = table.Inputs,
					Columns = list5,
					Expression = SqlExpressionTranslator.SqlToMTranslator.AddStep((ILetExpression)table.Expression, new FunctionExpressionSyntaxNode(SqlExpressionTranslator.SqlToMTranslator.tableFunctionType, expression8))
				};
			}

			// Token: 0x0600C8B5 RID: 51381 RVA: 0x0000A6A5 File Offset: 0x000088A5
			protected override SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table NewJoinParenthesisTableReference(SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table table)
			{
				return table;
			}

			// Token: 0x0600C8B6 RID: 51382 RVA: 0x00281420 File Offset: 0x0027F620
			protected override IExpression NewInPredicate(IExpression expression, IList<IExpression> values, IExpression subquery)
			{
				if (values != null && values.Count > 0 && subquery != null)
				{
					throw new NotSupportedException();
				}
				IExpression expression2;
				if (values != null && values.Count > 0)
				{
					IExpression[] array = new IExpression[values.Count];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = values[i];
						this.UnionTypesAndPromoteExpressions(ref expression, ref array[i]);
					}
					expression2 = new ListExpressionSyntaxNode(array);
				}
				else
				{
					expression2 = subquery;
				}
				return this.Result(SqlExpressionTranslator.SqlToMTranslator.NewInvocation("List.Contains", expression2, expression), this.engine.Type(TypeHandle.Logical));
			}

			// Token: 0x0600C8B7 RID: 51383 RVA: 0x002814AC File Offset: 0x0027F6AC
			protected override IExpression NewLeftFunctionCall(IExpression expression, IExpression count)
			{
				return this.Result(SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Text.Start", expression, count), this.GetType(expression));
			}

			// Token: 0x0600C8B8 RID: 51384 RVA: 0x002814C8 File Offset: 0x0027F6C8
			protected override IExpression NewLiteralExpression(LiteralType type, string value)
			{
				switch (type)
				{
				case LiteralType.Integer:
				{
					long num;
					if (long.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
					{
						return this.Result(this.NewConstant(this.engine.Decimal(num)), this.engine.Type(TypeHandle.Int64));
					}
					break;
				}
				case LiteralType.Real:
				{
					double num2;
					if (double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out num2))
					{
						return this.NewConstant(this.engine.Number(num2));
					}
					break;
				}
				case LiteralType.Binary:
					return this.NewBinaryConstant(value);
				case LiteralType.String:
					return this.NewConstant(this.engine.Text(value));
				case LiteralType.Null:
					return this.NewConstant(this.engine.Null);
				case LiteralType.Max:
					return this.NewConstant(double.PositiveInfinity);
				case LiteralType.Numeric:
				{
					decimal num3;
					if (decimal.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out num3))
					{
						return this.Result(this.NewConstant(this.engine.Decimal(num3)), this.engine.Type(TypeHandle.Decimal));
					}
					break;
				}
				}
				throw new NotSupportedException();
			}

			// Token: 0x0600C8B9 RID: 51385 RVA: 0x002815F0 File Offset: 0x0027F7F0
			protected override SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table NewNamedTableReference(Microsoft.Mashup.ScriptDom.Identifier alias, MultiPartIdentifier table)
			{
				string text = null;
				Microsoft.Mashup.Engine.Interface.Identifier identifier = this.NewInterfaceIdentifier(table, out text);
				if (alias != null)
				{
					table = SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.NewIdentifier(alias);
				}
				SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table table2;
				if (!this.tables.TryGetValue(identifier, out table2))
				{
					IExpression expression = new RequiredFieldAccessExpressionSyntaxNode(SqlExpressionTranslator.SqlToMTranslator.NewReference(SqlExpressionTranslator.SqlToMTranslator.environmentFunctionType.Parameters[0].Identifier), text);
					ITableTypeValue asTableType = this.environment[text].Type.AsTableType;
					table2 = this.NewNamedTable(expression, asTableType, table, EmptyArray<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table>.Instance);
				}
				return table2;
			}

			// Token: 0x0600C8BA RID: 51386 RVA: 0x00281674 File Offset: 0x0027F874
			private SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table NewNamedTable(IExpression expression, ITableTypeValue type, MultiPartIdentifier tableName, IList<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table> inputs)
			{
				List<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding> list = new List<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding>();
				IRecordValue fields = type.ItemType.Fields;
				List<IValue> list2 = new List<IValue>();
				for (int i = 0; i < fields.Keys.Length; i++)
				{
					string text = fields.Keys[i];
					string text2 = SqlExpressionTranslator.SqlToMTranslator.EncodeIdentifierPart(text);
					if (!string.Equals(text, text2, StringComparison.Ordinal))
					{
						list2.Add(this.engine.List(new IValue[]
						{
							this.engine.Text(text),
							this.engine.Text(text2)
						}));
					}
					MultiPartIdentifier multiPartIdentifier = SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.NewIdentifier(tableName, new Microsoft.Mashup.ScriptDom.Identifier
					{
						Value = text
					});
					list.Add(new SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding
					{
						Identifier = multiPartIdentifier,
						Value = new SqlExpressionTranslator.RowParameterAndType
						{
							Type = fields[i].AsRecord["Type"].AsType
						}
					});
				}
				if (list2.Count > 0)
				{
					expression = SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Table.RenameColumns", expression, this.NewConstant(this.engine.List(list2.ToArray())));
				}
				SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table table = new SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table
				{
					Inputs = inputs,
					Columns = list,
					Expression = expression
				};
				if (list.Count > 0)
				{
					expression = SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Table.PrefixColumns", expression, this.NewConstant(this.engine.Text(this.NewInterfaceIdentifier(tableName).Name)));
					table = new SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table
					{
						Inputs = this.FlattenTableInputs(new SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table[] { table }),
						Columns = table.Columns,
						Expression = expression
					};
				}
				return table;
			}

			// Token: 0x0600C8BB RID: 51387 RVA: 0x0028182C File Offset: 0x0027FA2C
			protected override SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table NewOrderByClause(SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table table, IList<IExpression> orderByElements)
			{
				if (orderByElements.Count > 0)
				{
					return new SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table
					{
						Inputs = table.Inputs,
						Columns = table.Columns,
						Expression = SqlExpressionTranslator.SqlToMTranslator.AddStep((ILetExpression)table.Expression, new FunctionExpressionSyntaxNode(SqlExpressionTranslator.SqlToMTranslator.tableFunctionType, SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Table.Sort", SqlExpressionTranslator.SqlToMTranslator.NewReference(SqlExpressionTranslator.SqlToMTranslator.tableFunctionType.Parameters[0].Identifier), new ListExpressionSyntaxNode(orderByElements))))
					};
				}
				return table;
			}

			// Token: 0x0600C8BC RID: 51388 RVA: 0x0000A6A5 File Offset: 0x000088A5
			protected override IExpression NewParenthesisExpression(IExpression expression)
			{
				return expression;
			}

			// Token: 0x0600C8BD RID: 51389 RVA: 0x002818AC File Offset: 0x0027FAAC
			private static IExpression CombineFilters(List<IExpression> filters)
			{
				IExpression expression = null;
				foreach (IExpression expression2 in filters)
				{
					if (expression == null)
					{
						expression = expression2;
					}
					else
					{
						expression = BinaryExpressionSyntaxNode.New(BinaryOperator2.And, expression, expression2, TokenRange.Null);
					}
				}
				return expression;
			}

			// Token: 0x0600C8BE RID: 51390 RVA: 0x0028190C File Offset: 0x0027FB0C
			private static List<IExpression> GetConjunctedFilters(IExpression expression)
			{
				List<IExpression> list = new List<IExpression>();
				SqlExpressionTranslator.SqlToMTranslator.GetConjunctedFilters(list, expression);
				return list;
			}

			// Token: 0x0600C8BF RID: 51391 RVA: 0x0028191C File Offset: 0x0027FB1C
			private static void GetConjunctedFilters(List<IExpression> conjunctedFilters, IExpression expression)
			{
				if (expression.Kind != ExpressionKind.Binary)
				{
					conjunctedFilters.Add(expression);
					return;
				}
				IBinaryExpression binaryExpression = (IBinaryExpression)expression;
				if (binaryExpression.Operator == BinaryOperator2.And)
				{
					SqlExpressionTranslator.SqlToMTranslator.GetConjunctedFilters(conjunctedFilters, binaryExpression.Left);
					SqlExpressionTranslator.SqlToMTranslator.GetConjunctedFilters(conjunctedFilters, binaryExpression.Right);
					return;
				}
				conjunctedFilters.Add(expression);
			}

			// Token: 0x0600C8C0 RID: 51392 RVA: 0x0028196C File Offset: 0x0027FB6C
			private bool TryGetEquijoin(IExpression searchCondition, IList<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding> leftColumns, IList<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding> rightColumns, out IListExpression leftKey, out IListExpression rightKey, out IListExpression keyComparers, out IExpression remainingJoinCondition)
			{
				IFunctionExpression functionExpression = searchCondition as IFunctionExpression;
				if (functionExpression != null && functionExpression.FunctionType.Parameters.Count == 1)
				{
					Microsoft.Mashup.Engine.Interface.Identifier identifier = functionExpression.FunctionType.Parameters[0].Identifier;
					List<IExpression> conjunctedFilters = SqlExpressionTranslator.SqlToMTranslator.GetConjunctedFilters(functionExpression.Expression);
					List<IExpression> list = new List<IExpression>();
					List<IExpression> list2 = new List<IExpression>();
					List<IExpression> list3 = new List<IExpression>();
					List<IExpression> list4 = null;
					for (int i = 0; i < conjunctedFilters.Count; i++)
					{
						IExpression expression;
						IExpression expression2;
						bool flag;
						if (this.TryGetColumnEquality(identifier, conjunctedFilters[i], leftColumns, rightColumns, out expression, out expression2, out flag))
						{
							list.Add(expression);
							list2.Add(expression2);
							list3.Add(SqlExpressionTranslator.SqlToMTranslator.NewReference(flag ? "Value.NullableEquals" : "Value.Equals"));
						}
						else if (!this.TryGetTrue(conjunctedFilters[i]))
						{
							if (list4 == null)
							{
								list4 = new List<IExpression>();
							}
							list4.Add(conjunctedFilters[i]);
						}
					}
					leftKey = new ListExpressionSyntaxNode(list);
					rightKey = new ListExpressionSyntaxNode(list2);
					keyComparers = new ListExpressionSyntaxNode(list3);
					remainingJoinCondition = ((list4 != null) ? SqlExpressionTranslator.SqlToMTranslator.CombineFilters(list4) : null);
					if (remainingJoinCondition != null)
					{
						remainingJoinCondition = new FunctionExpressionSyntaxNode(functionExpression.FunctionType, remainingJoinCondition);
					}
					return true;
				}
				leftKey = null;
				rightKey = null;
				keyComparers = null;
				remainingJoinCondition = null;
				return false;
			}

			// Token: 0x0600C8C1 RID: 51393 RVA: 0x00281AB8 File Offset: 0x0027FCB8
			private void AlignColumns(IList<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding> leftColumns, IList<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding> rightColumns, ref Microsoft.Mashup.Engine.Interface.Identifier leftColumn, ref Microsoft.Mashup.Engine.Interface.Identifier rightColumn)
			{
				for (int i = 0; i < leftColumns.Count; i++)
				{
					if (this.NewInterfaceIdentifier(leftColumns[i].Identifier).Equals(rightColumn))
					{
						Microsoft.Mashup.Engine.Interface.Identifier identifier = leftColumn;
						leftColumn = rightColumn;
						rightColumn = identifier;
						return;
					}
				}
			}

			// Token: 0x0600C8C2 RID: 51394 RVA: 0x00281B04 File Offset: 0x0027FD04
			private bool TryGetColumnEquality(Microsoft.Mashup.Engine.Interface.Identifier rowParameter, IExpression expression, IList<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding> leftColumns, IList<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding> rightColumns, out IExpression left, out IExpression right, out bool sqlSemantics)
			{
				IBinaryExpression binaryExpression = expression as IBinaryExpression;
				IInvocationExpression invocationExpression = expression as IInvocationExpression;
				Microsoft.Mashup.Engine.Interface.Identifier identifier;
				Microsoft.Mashup.Engine.Interface.Identifier identifier2;
				if (binaryExpression != null && binaryExpression.Operator == BinaryOperator2.Equals && SqlExpressionTranslator.SqlToMTranslator.TryGetColumnReference(rowParameter, binaryExpression.Left, out identifier) && SqlExpressionTranslator.SqlToMTranslator.TryGetColumnReference(rowParameter, binaryExpression.Right, out identifier2))
				{
					this.AlignColumns(leftColumns, rightColumns, ref identifier, ref identifier2);
					left = this.NewConstant(this.engine.Text(identifier.Name));
					right = this.NewConstant(this.engine.Text(identifier2.Name));
					sqlSemantics = false;
					return true;
				}
				Microsoft.Mashup.Engine.Interface.Identifier identifier3;
				if (invocationExpression != null && SqlExpressionTranslator.SqlToMTranslator.TryGetIdentifier(invocationExpression.Function, out identifier3) && (identifier3.Name == "Value.Equals" || identifier3.Name == "Value.NullableEquals") && SqlExpressionTranslator.SqlToMTranslator.TryGetColumnReference(rowParameter, invocationExpression.Arguments[0], out identifier) && SqlExpressionTranslator.SqlToMTranslator.TryGetColumnReference(rowParameter, invocationExpression.Arguments[1], out identifier2))
				{
					this.AlignColumns(leftColumns, rightColumns, ref identifier, ref identifier2);
					left = this.NewConstant(this.engine.Text(identifier.Name));
					right = this.NewConstant(this.engine.Text(identifier2.Name));
					sqlSemantics = identifier3.Name == "Value.NullableEquals";
					return true;
				}
				left = null;
				right = null;
				sqlSemantics = false;
				return false;
			}

			// Token: 0x0600C8C3 RID: 51395 RVA: 0x00281C64 File Offset: 0x0027FE64
			private bool TryGetTrue(IExpression expression)
			{
				IBinaryExpression binaryExpression = expression as IBinaryExpression;
				IInvocationExpression invocationExpression = expression as IInvocationExpression;
				IValue value;
				IValue value2;
				if (binaryExpression != null)
				{
					return binaryExpression.Operator == BinaryOperator2.Equals && SqlExpressionTranslator.SqlToMTranslator.TryGetConstant(binaryExpression.Left, out value) && SqlExpressionTranslator.SqlToMTranslator.TryGetConstant(binaryExpression.Right, out value2) && value.Equals(value2);
				}
				Microsoft.Mashup.Engine.Interface.Identifier identifier;
				return invocationExpression != null && (SqlExpressionTranslator.SqlToMTranslator.TryGetIdentifier(invocationExpression.Function, out identifier) && identifier.Name == "Value.NullableEquals" && SqlExpressionTranslator.SqlToMTranslator.TryGetConstant(invocationExpression.Arguments[0], out value) && SqlExpressionTranslator.SqlToMTranslator.TryGetConstant(invocationExpression.Arguments[1], out value2)) && value.Equals(value2);
			}

			// Token: 0x0600C8C4 RID: 51396 RVA: 0x00281D10 File Offset: 0x0027FF10
			protected override SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table NewQualifiedJoin(SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table firstTable, SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table secondTable, QualifiedJoinType joinType, IExpression searchCondition)
			{
				IList<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding> list = firstTable.Columns;
				IList<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding> list2 = secondTable.Columns;
				IListExpression listExpression;
				IListExpression listExpression2;
				IListExpression listExpression3;
				IExpression expression;
				if (this.TryGetEquijoin(searchCondition, list, list2, out listExpression, out listExpression2, out listExpression3, out expression))
				{
					string text;
					switch (joinType)
					{
					case QualifiedJoinType.Inner:
						text = "JoinKind.Inner";
						break;
					case QualifiedJoinType.LeftOuter:
						text = "JoinKind.LeftOuter";
						list2 = this.MakeNullable(list2);
						break;
					case QualifiedJoinType.RightOuter:
						text = "JoinKind.RightOuter";
						list = this.MakeNullable(list);
						break;
					case QualifiedJoinType.FullOuter:
						text = "JoinKind.FullOuter";
						list = this.MakeNullable(list);
						list2 = this.MakeNullable(list2);
						break;
					default:
						throw new NotSupportedException();
					}
					List<VariableInitializer> list3 = new List<VariableInitializer>();
					list3.Add(new VariableInitializer(Microsoft.Mashup.Engine.Interface.Identifier.New(), firstTable.Expression));
					list3.Add(new VariableInitializer(Microsoft.Mashup.Engine.Interface.Identifier.New(), secondTable.Expression));
					list3.Add(new VariableInitializer(Microsoft.Mashup.Engine.Interface.Identifier.New(), SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Table.Join", new IExpression[]
					{
						SqlExpressionTranslator.SqlToMTranslator.NewReference(list3[0].Name),
						listExpression,
						SqlExpressionTranslator.SqlToMTranslator.NewReference(list3[1].Name),
						listExpression2,
						SqlExpressionTranslator.SqlToMTranslator.NewReference(text),
						this.NewConstant(this.engine.Null),
						listExpression3
					})));
					if (expression != null)
					{
						if (joinType != QualifiedJoinType.Inner)
						{
							throw new NotSupportedException();
						}
						list3.Add(new VariableInitializer(Microsoft.Mashup.Engine.Interface.Identifier.New(), SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Table.SelectRows", SqlExpressionTranslator.SqlToMTranslator.NewReference(list3[2].Name), expression)));
					}
					VariableInitializer[] array = list3.ToArray();
					return new SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table
					{
						Inputs = SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ConcatenateTables(firstTable.Inputs, secondTable.Inputs),
						Columns = SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ConcatenateColumns(list, list2),
						Expression = new LetExpressionSyntaxNode(array, SqlExpressionTranslator.SqlToMTranslator.NewReference(array[array.Length - 1].Name))
					};
				}
				throw new NotSupportedException();
			}

			// Token: 0x0600C8C5 RID: 51397 RVA: 0x00281EF4 File Offset: 0x002800F4
			protected override SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table NewBinaryQueryExpression(SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table firstTable, SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table secondTable, BinaryQueryExpressionType binaryQueryExpressionType, bool binaryQueryAll)
			{
				if (binaryQueryExpressionType != BinaryQueryExpressionType.Union)
				{
					if (binaryQueryExpressionType - BinaryQueryExpressionType.Except > 1)
					{
					}
					throw new NotSupportedException("Only 'union all' is supported.");
				}
				if (!binaryQueryAll)
				{
					throw new NotSupportedException("Only 'union all' is supported.");
				}
				if (firstTable.Columns.Count != secondTable.Columns.Count)
				{
					throw new NotSupportedException("Table column counts should match for 'union all'.");
				}
				int count = firstTable.Columns.Count;
				for (int i = 0; i < count; i++)
				{
					if (!firstTable.Columns[i].Value.Type.IsCompatibleWith(secondTable.Columns[i].Value.Type))
					{
						throw new NotSupportedException("Table column types should match for 'union all'.");
					}
					if (firstTable.Columns[i].Identifier != secondTable.Columns[i].Identifier)
					{
						throw new NotSupportedException("Table column aliases should match for 'union all'.");
					}
				}
				VariableInitializer[] array = new VariableInitializer[3];
				array[0] = new VariableInitializer(Microsoft.Mashup.Engine.Interface.Identifier.New(), firstTable.Expression);
				array[1] = new VariableInitializer(Microsoft.Mashup.Engine.Interface.Identifier.New(), secondTable.Expression);
				array[2] = new VariableInitializer(Microsoft.Mashup.Engine.Interface.Identifier.New(), SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Table.Combine", new ListExpressionSyntaxNode(new List<IExpression>
				{
					SqlExpressionTranslator.SqlToMTranslator.NewReference(array[0].Name),
					SqlExpressionTranslator.SqlToMTranslator.NewReference(array[1].Name)
				})));
				IExpression expression = new LetExpressionSyntaxNode(array, SqlExpressionTranslator.SqlToMTranslator.NewReference(array[2].Name));
				return new SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table
				{
					Inputs = SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ConcatenateTables(firstTable.Inputs, secondTable.Inputs),
					Columns = firstTable.Columns,
					Expression = expression
				};
			}

			// Token: 0x0600C8C6 RID: 51398 RVA: 0x002820BC File Offset: 0x002802BC
			private SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table SelectColumns(SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table table, IList<MultiPartIdentifier> selection)
			{
				if (selection.Count > 0)
				{
					List<IExpression> list = new List<IExpression>();
					List<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding> list2 = new List<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding>();
					for (int i = 0; i < selection.Count; i++)
					{
						for (int j = table.Columns.Count - 1; j >= 0; j--)
						{
							if (SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.EndsWith(table.Columns[j].Identifier, selection[i]))
							{
								list2.Add(table.Columns[j]);
								list.Add(this.NewConstant(this.engine.Text(this.NewInterfaceIdentifier(table.Columns[j].Identifier))));
								break;
							}
						}
						if (list.Count != i + 1)
						{
							list2.Add(new SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding
							{
								Identifier = selection[i],
								Value = new SqlExpressionTranslator.RowParameterAndType
								{
									Type = this.engine.Type(TypeHandle.Any)
								}
							});
							list.Add(this.NewConstant(this.engine.Text(this.NewInterfaceIdentifier(selection[i]).Name)));
						}
					}
					return new SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table
					{
						Inputs = table.Inputs,
						Columns = list2,
						Expression = SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Table.SelectColumns", table.Expression, new ListExpressionSyntaxNode(list))
					};
				}
				return table;
			}

			// Token: 0x0600C8C7 RID: 51399 RVA: 0x0028222C File Offset: 0x0028042C
			private SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table ReplaceColumnPrefix(SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table table, MultiPartIdentifier newPrefix, bool backToOriginalName)
			{
				IExpression[] array = new IExpression[table.Columns.Count];
				SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding[] array2 = new SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding[table.Columns.Count];
				for (int i = 0; i < array.Length; i++)
				{
					string text = null;
					MultiPartIdentifier multiPartIdentifier = SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.NewIdentifier(newPrefix, table.Columns[i].Identifier.Identifiers.Last<Microsoft.Mashup.ScriptDom.Identifier>());
					Microsoft.Mashup.Engine.Interface.Identifier identifier = this.NewInterfaceIdentifier(table.Columns[i].Identifier);
					Microsoft.Mashup.Engine.Interface.Identifier identifier2 = this.NewInterfaceIdentifier(multiPartIdentifier, backToOriginalName, out text);
					array[i] = new ListExpressionSyntaxNode(new IExpression[]
					{
						this.NewConstant(this.engine.Text(identifier.Name)),
						this.NewConstant(this.engine.Text(backToOriginalName ? text : identifier2.Name))
					});
					array2[i] = new SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding
					{
						Identifier = multiPartIdentifier,
						Value = table.Columns[i].Value
					};
				}
				return new SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table
				{
					Inputs = table.Inputs,
					Columns = array2,
					Expression = SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Table.RenameColumns", table.Expression, new ListExpressionSyntaxNode(array))
				};
			}

			// Token: 0x0600C8C8 RID: 51400 RVA: 0x00282378 File Offset: 0x00280578
			protected override SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table NewQueryDerivedTable(Microsoft.Mashup.ScriptDom.Identifier alias, IList<Microsoft.Mashup.ScriptDom.Identifier> columns, SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table table)
			{
				table = this.SelectColumns(table, columns.Select((Microsoft.Mashup.ScriptDom.Identifier c) => SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.NewIdentifier(c)).ToArray<MultiPartIdentifier>());
				return this.ReplaceColumnPrefix(table, SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.NewIdentifier(alias), false);
			}

			// Token: 0x0600C8C9 RID: 51401 RVA: 0x0000A6A5 File Offset: 0x000088A5
			protected override SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table NewQueryParenthesisExpression(SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table table)
			{
				return table;
			}

			// Token: 0x0600C8CA RID: 51402 RVA: 0x0000A6A5 File Offset: 0x000088A5
			protected override SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table NewQuerySpecification(SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table table)
			{
				return table;
			}

			// Token: 0x0600C8CB RID: 51403 RVA: 0x002823C6 File Offset: 0x002805C6
			protected override IExpression NewRightFunctionCall(IExpression expression, IExpression count)
			{
				return this.Result(SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Text.End", expression, count), this.GetType(expression));
			}

			// Token: 0x0600C8CC RID: 51404 RVA: 0x002823E4 File Offset: 0x002805E4
			protected override IExpression NewScalarSubquery(SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table table, bool list)
			{
				IExpression expression2;
				if (list)
				{
					IExpression expression = this.NewConstant(this.engine.Text(this.NewInterfaceIdentifier(table.Columns[0].Identifier)));
					expression2 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Table.Column", table.Expression, expression);
				}
				else
				{
					expression2 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Table.FirstValue", table.Expression);
				}
				expression2 = InliningVisitor.Inline(this.engine, expression2, 5000);
				expression2 = ScalarSubqueryInliner.Inline(this.engine, expression2);
				return this.Result(expression2, table.Columns[0].Value.Type);
			}

			// Token: 0x0600C8CD RID: 51405 RVA: 0x0028248C File Offset: 0x0028068C
			protected override SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table NewSchemaObjectFunctionTableReference(Microsoft.Mashup.ScriptDom.Identifier alias, IList<Microsoft.Mashup.ScriptDom.Identifier> columnAliases, MultiPartIdentifier function, IList<IExpression> parameters)
			{
				if (columnAliases.Count != 0 || function.Count != 1)
				{
					throw new NotSupportedException();
				}
				IList<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table> list = this.PushExpressionTableRefs();
				MultiPartIdentifier multiPartIdentifier = SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.NewIdentifier(alias ?? this.NewTableIdentifier());
				string text = function.Identifiers[0].Value.Trim();
				IValue value;
				IExpression expression2;
				if (this.environment.TryGetValue(text, out value) && value.IsFunction)
				{
					if (value.Type.AsFunctionType.ParameterCount != parameters.Count)
					{
						throw new NotSupportedException("Function " + text + " has incorrect number of parameters.");
					}
					IExpression expression = new RequiredFieldAccessExpressionSyntaxNode(SqlExpressionTranslator.SqlToMTranslator.NewReference(SqlExpressionTranslator.SqlToMTranslator.environmentFunctionType.Parameters[0].Identifier), text);
					this.AddExpressionTableRefs(new List<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table>());
					ITableTypeValue tableTypeValue = this.InvokeAndGetType(value, parameters);
					expression2 = this.NewCall(expression, tableTypeValue, parameters);
				}
				else
				{
					expression2 = this.NewFunctionCall(function.Identifiers[0], UniqueRowFilter.NotSpecified, parameters);
				}
				ITableTypeValue asTableType = this.GetType(expression2).AsTableType;
				IList<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table> list2 = this.PopExpressionTableRefs(list);
				return this.NewNamedTable(expression2, asTableType, multiPartIdentifier, list2);
			}

			// Token: 0x0600C8CE RID: 51406 RVA: 0x002825B4 File Offset: 0x002807B4
			protected override IExpression NewSearchedCaseExpression(IList<Tuple<IExpression, IExpression>> whenExpressions, IExpression elseExpression)
			{
				IExpression expression = elseExpression;
				for (int i = whenExpressions.Count - 1; i >= 0; i--)
				{
					IExpression expression2 = expression;
					IExpression item = whenExpressions[i].Item2;
					ITypeValue typeValue = this.UnionTypesAndPromoteExpressions(ref expression2, ref item);
					expression = this.Result(new IfExpressionSyntaxNode(whenExpressions[i].Item1, item, expression2, TokenRange.Null), typeValue);
				}
				return expression;
			}

			// Token: 0x0600C8CF RID: 51407 RVA: 0x00282614 File Offset: 0x00280814
			protected override SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table NewSelectedColumnsClause(SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table table, IList<MultiPartIdentifier> selection)
			{
				SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table table2 = this.SelectColumns(new SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table
				{
					Inputs = table.Inputs,
					Columns = table.Columns,
					Expression = SqlExpressionTranslator.SqlToMTranslator.NewReference(SqlExpressionTranslator.SqlToMTranslator.tableFunctionType.Parameters[0].Identifier)
				}, selection);
				return new SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table
				{
					Inputs = table.Inputs,
					Columns = table2.Columns,
					Expression = SqlExpressionTranslator.SqlToMTranslator.AddStep((ILetExpression)table.Expression, new FunctionExpressionSyntaxNode(SqlExpressionTranslator.SqlToMTranslator.tableFunctionType, table2.Expression))
				};
			}

			// Token: 0x0600C8D0 RID: 51408 RVA: 0x002826AC File Offset: 0x002808AC
			protected override IExpression NewSqlDataTypeReference(MultiPartIdentifier name, SqlDataTypeOption sqlDataTypeOption, IList<IExpression> parameters)
			{
				VariableInitializer[] array = null;
				IExpression expression;
				switch (sqlDataTypeOption)
				{
				case SqlDataTypeOption.BigInt:
					expression = SqlExpressionTranslator.SqlToMTranslator.Int64TypeReference;
					goto IL_0179;
				case SqlDataTypeOption.Int:
					expression = SqlExpressionTranslator.SqlToMTranslator.Int32TypeReference;
					goto IL_0179;
				case SqlDataTypeOption.SmallInt:
					expression = SqlExpressionTranslator.SqlToMTranslator.Int16TypeReference;
					goto IL_0179;
				case SqlDataTypeOption.TinyInt:
					expression = SqlExpressionTranslator.SqlToMTranslator.Int8TypeReference;
					goto IL_0179;
				case SqlDataTypeOption.Bit:
					expression = SqlExpressionTranslator.SqlToMTranslator.LogicalTypeReference;
					goto IL_0179;
				case SqlDataTypeOption.Decimal:
					expression = SqlExpressionTranslator.SqlToMTranslator.DecimalTypeReference;
					goto IL_0179;
				case SqlDataTypeOption.Money:
				case SqlDataTypeOption.SmallMoney:
					expression = SqlExpressionTranslator.SqlToMTranslator.CurrencyTypeReference;
					goto IL_0179;
				case SqlDataTypeOption.Float:
					expression = SqlExpressionTranslator.SqlToMTranslator.DoubleTypeReference;
					goto IL_0179;
				case SqlDataTypeOption.Real:
					expression = SqlExpressionTranslator.SqlToMTranslator.SingleTypeReference;
					goto IL_0179;
				case SqlDataTypeOption.DateTime:
				case SqlDataTypeOption.SmallDateTime:
				case SqlDataTypeOption.DateTime2:
					expression = SqlExpressionTranslator.SqlToMTranslator.DateTimeTypeReference;
					goto IL_0179;
				case SqlDataTypeOption.Char:
				case SqlDataTypeOption.VarChar:
				case SqlDataTypeOption.NChar:
				case SqlDataTypeOption.NVarChar:
				{
					expression = SqlExpressionTranslator.SqlToMTranslator.TextTypeReference;
					long num;
					if (parameters.Count == 1 && SqlExpressionTranslator.SqlToMTranslator.TryGetInt64(parameters[0], out num))
					{
						array = new VariableInitializer[]
						{
							new VariableInitializer(Microsoft.Mashup.Engine.Interface.Identifier.New("MaxLength"), this.NewConstant(this.engine.Number((double)num)))
						};
						goto IL_0179;
					}
					goto IL_0179;
				}
				case SqlDataTypeOption.Text:
				case SqlDataTypeOption.NText:
					expression = SqlExpressionTranslator.SqlToMTranslator.TextTypeReference;
					goto IL_0179;
				case SqlDataTypeOption.Binary:
				case SqlDataTypeOption.VarBinary:
				case SqlDataTypeOption.Image:
					expression = SqlExpressionTranslator.SqlToMTranslator.BinaryTypeReference;
					goto IL_0179;
				case SqlDataTypeOption.Date:
					expression = SqlExpressionTranslator.SqlToMTranslator.DateTypeReference;
					goto IL_0179;
				case SqlDataTypeOption.Time:
					expression = SqlExpressionTranslator.SqlToMTranslator.TimeTypeReference;
					goto IL_0179;
				case SqlDataTypeOption.DateTimeOffset:
					expression = SqlExpressionTranslator.SqlToMTranslator.DateTimeZoneTypeReference;
					goto IL_0179;
				}
				throw new NotSupportedException();
				IL_0179:
				if (array != null)
				{
					expression = SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Type.ReplaceFacets", expression, new RecordExpressionSyntaxNode(array));
				}
				return this.Result(expression, this.engine.Type(TypeHandle.Type));
			}

			// Token: 0x0600C8D1 RID: 51409 RVA: 0x0028285C File Offset: 0x00280A5C
			protected override SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table NewTopClause(SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table table, IExpression limit)
			{
				return new SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table
				{
					Inputs = table.Inputs,
					Columns = table.Columns,
					Expression = SqlExpressionTranslator.SqlToMTranslator.AddStep((ILetExpression)table.Expression, new FunctionExpressionSyntaxNode(SqlExpressionTranslator.SqlToMTranslator.tableFunctionType, SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Table.FirstN", SqlExpressionTranslator.SqlToMTranslator.NewReference(SqlExpressionTranslator.SqlToMTranslator.tableFunctionType.Parameters[0].Identifier), SqlExpressionTranslator.SqlToMTranslator.NewInvocation(limit, this.NewConstant(this.engine.Null)))))
				};
			}

			// Token: 0x0600C8D2 RID: 51410 RVA: 0x002828E4 File Offset: 0x00280AE4
			protected override IExpression NewUnaryExpression(UnaryExpressionType type, IExpression expression)
			{
				if (type == UnaryExpressionType.Negative)
				{
					UnaryOperator2 unaryOperator = UnaryOperator2.Negative;
					return this.Result(UnaryExpressionSyntaxNode.New(unaryOperator, expression, TokenRange.Null), this.GetType(expression));
				}
				throw new NotSupportedException();
			}

			// Token: 0x0600C8D3 RID: 51411 RVA: 0x00282918 File Offset: 0x00280B18
			protected override IExpression NewVariableReferenceExpression(string name)
			{
				return this.Result(new RequiredFieldAccessExpressionSyntaxNode(SqlExpressionTranslator.SqlToMTranslator.NewReference(SqlExpressionTranslator.SqlToMTranslator.environmentFunctionType.Parameters[0].Identifier), Microsoft.Mashup.Engine.Interface.Identifier.New(name)), this.environment[name].Type);
			}

			// Token: 0x0600C8D4 RID: 51412 RVA: 0x00282958 File Offset: 0x00280B58
			protected override SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table NewVariableTableReference(Microsoft.Mashup.ScriptDom.Identifier alias, string name)
			{
				MultiPartIdentifier multiPartIdentifier = SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.NewIdentifier(new Microsoft.Mashup.ScriptDom.Identifier
				{
					Value = name
				});
				return this.NewNamedTableReference(alias, multiPartIdentifier);
			}

			// Token: 0x0600C8D5 RID: 51413 RVA: 0x00282980 File Offset: 0x00280B80
			protected override SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table NewWhereClause(SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table table, IExpression searchCondition)
			{
				return new SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table
				{
					Inputs = table.Inputs,
					Columns = table.Columns,
					Expression = SqlExpressionTranslator.SqlToMTranslator.AddStep((ILetExpression)table.Expression, new FunctionExpressionSyntaxNode(SqlExpressionTranslator.SqlToMTranslator.tableFunctionType, SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Table.SelectRows", SqlExpressionTranslator.SqlToMTranslator.NewReference(SqlExpressionTranslator.SqlToMTranslator.tableFunctionType.Parameters[0].Identifier), searchCondition)))
				};
			}

			// Token: 0x0600C8D6 RID: 51414 RVA: 0x002829F0 File Offset: 0x00280BF0
			private IExpression NewAggregate(string function, bool distinct, IList<IExpression> parameters)
			{
				IExpression expression = this.NewDistinct(distinct, parameters[0]);
				return this.Result(SqlExpressionTranslator.SqlToMTranslator.NewInvocation(function, expression), this.GetType(expression));
			}

			// Token: 0x0600C8D7 RID: 51415 RVA: 0x00282A20 File Offset: 0x00280C20
			private IExpression NewAggregateWithPrecision(string function, bool distinct, IList<IExpression> parameters)
			{
				IExpression expression = this.NewDistinct(distinct, parameters[0]);
				ITypeValue type = this.GetType(expression);
				IExpression precision = this.GetPrecision(type);
				IExpression expression2;
				if (this.IsDecimal(precision))
				{
					expression2 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation(function, expression, precision);
				}
				else
				{
					expression2 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation(function, expression);
				}
				return this.Result(expression2, type);
			}

			// Token: 0x0600C8D8 RID: 51416 RVA: 0x00282A70 File Offset: 0x00280C70
			private IExpression NewList(IList<IExpression> parameters)
			{
				return this.Result(new ListExpressionSyntaxNode(parameters), this.ListOfAnyType);
			}

			// Token: 0x1700308C RID: 12428
			// (get) Token: 0x0600C8D9 RID: 51417 RVA: 0x00282A84 File Offset: 0x00280C84
			private ITypeValue ListOfAnyType
			{
				get
				{
					if (this.listOfAnyType == null)
					{
						IFunctionValue functionValue = this.CompileFunction("type list");
						this.listOfAnyType = this.engine.Invoke(functionValue, Array.Empty<IValue>()).AsType;
					}
					return this.listOfAnyType;
				}
			}

			// Token: 0x1700308D RID: 12429
			// (get) Token: 0x0600C8DA RID: 51418 RVA: 0x00282AC8 File Offset: 0x00280CC8
			private ITypeValue RecordOfAnyType
			{
				get
				{
					if (this.recordOfAnyType == null)
					{
						IFunctionValue functionValue = this.CompileFunction("type record");
						this.recordOfAnyType = this.engine.Invoke(functionValue, Array.Empty<IValue>()).AsType;
					}
					return this.recordOfAnyType;
				}
			}

			// Token: 0x0600C8DB RID: 51419 RVA: 0x00282B0C File Offset: 0x00280D0C
			private SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table ValidateRefFunctionInput(IList<IExpression> parameters, string functionFriendlyName)
			{
				if (parameters.Count > 1)
				{
					throw new NotSupportedException(functionFriendlyName + " can only have zero or one parameters.");
				}
				if (parameters.Count != 1)
				{
					return null;
				}
				IValue value;
				SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table table;
				if (!SqlExpressionTranslator.SqlToMTranslator.TryGetConstant(parameters[0], out value) || !value.IsText || !this.tables.TryGetValue(value.AsString, out table))
				{
					throw new NotSupportedException(functionFriendlyName + " argument must be a valid table reference.");
				}
				return table;
			}

			// Token: 0x0600C8DC RID: 51420 RVA: 0x00282B84 File Offset: 0x00280D84
			private IExpression NewRecordRef(IList<IExpression> parameters)
			{
				SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table table = this.ValidateRefFunctionInput(parameters, "recordref()");
				if (table != null)
				{
					IExpression expression = SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Table.First", this.ReplaceColumnPrefix(table, new MultiPartIdentifier(), true).Expression);
					return this.Result(expression, this.RecordOfAnyType);
				}
				return this.engine.ConstantExpression(this.engine.EmptyRecord);
			}

			// Token: 0x0600C8DD RID: 51421 RVA: 0x00282BE4 File Offset: 0x00280DE4
			private IExpression NewListRef(IList<IExpression> parameters)
			{
				SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table table = this.ValidateRefFunctionInput(parameters, "listref()");
				if (table != null)
				{
					IExpression expression = SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Table.Column", table.Expression, new RequiredElementAccessExpressionSyntaxNode(SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Table.ColumnNames", table.Expression), this.NewConstant(0.0)));
					return this.Result(expression, this.ListOfAnyType);
				}
				return this.engine.ConstantExpression(this.engine.List(Array.Empty<IValue>()));
			}

			// Token: 0x0600C8DE RID: 51422 RVA: 0x00282C5F File Offset: 0x00280E5F
			private IExpression NewTableRef(IList<IExpression> parameters)
			{
				if (parameters.Count != 1)
				{
					throw new NotSupportedException("tableref() can only have one parameter.");
				}
				this.ValidateRefFunctionInput(parameters, "tableref()");
				return this.NewTableReference(parameters[0]);
			}

			// Token: 0x1700308E RID: 12430
			// (get) Token: 0x0600C8DF RID: 51423 RVA: 0x00282C90 File Offset: 0x00280E90
			private IFunctionValue TableTypeFromFieldsCtor
			{
				get
				{
					if (this.tableTypeFromFieldsCtor == null)
					{
						IFunctionValue functionValue = this.CompileFunction("(fields) => let rowType = Type.ForRecord(fields, false) in type table rowType");
						this.tableTypeFromFieldsCtor = this.engine.Invoke(functionValue, Array.Empty<IValue>()).AsFunction;
					}
					return this.tableTypeFromFieldsCtor;
				}
			}

			// Token: 0x0600C8E0 RID: 51424 RVA: 0x00282CD4 File Offset: 0x00280ED4
			private ITableTypeValue NewTableType(SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table table)
			{
				IValue value = this.engine.Logical(false);
				IKeys keys = this.engine.Keys(new string[] { "Type", "Optional" });
				string[] array = new string[table.Columns.Count];
				IValue[] array2 = new IValue[table.Columns.Count];
				for (int i = 0; i < array.Length; i++)
				{
					SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding columnBinding = table.Columns[i];
					array[i] = this.NewInterfaceIdentifier(columnBinding.Identifier);
					array2[i] = this.engine.Record(keys, new IValue[]
					{
						columnBinding.Value.Type,
						value
					});
				}
				IRecordValue recordValue = this.engine.Record(this.engine.Keys(array), array2);
				return this.engine.Invoke(this.TableTypeFromFieldsCtor, new IValue[] { recordValue }).AsType.AsTableType;
			}

			// Token: 0x0600C8E1 RID: 51425 RVA: 0x00282DD4 File Offset: 0x00280FD4
			private IExpression NewTableReference(IExpression table)
			{
				IValue value;
				if (SqlExpressionTranslator.SqlToMTranslator.TryGetConstant(table, out value) && value.IsText)
				{
					MultiPartIdentifier multiPartIdentifier = SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.NewIdentifier(new Microsoft.Mashup.ScriptDom.Identifier
					{
						Value = value.AsString
					});
					Microsoft.Mashup.Engine.Interface.Identifier identifier = this.NewInterfaceIdentifier(multiPartIdentifier);
					SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table table2;
					if (this.tables.TryGetValue(identifier, out table2))
					{
						this.AddExpressionTableRefs(table2.Inputs);
					}
					else
					{
						IExpression expression = new RequiredFieldAccessExpressionSyntaxNode(SqlExpressionTranslator.SqlToMTranslator.NewReference(SqlExpressionTranslator.SqlToMTranslator.environmentFunctionType.Parameters[0].Identifier), value.AsString);
						ITableTypeValue asTableType = this.environment[value.AsString].Type.AsTableType;
						table2 = this.NewNamedTable(expression, asTableType, multiPartIdentifier, EmptyArray<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table>.Instance);
						this.AddExpressionTableRefs(new SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table[] { table2 });
					}
					return this.Result(table2.Expression, this.NewTableType(table2));
				}
				throw new NotSupportedException();
			}

			// Token: 0x0600C8E2 RID: 51426 RVA: 0x00282EBC File Offset: 0x002810BC
			private ITableTypeValue InvokeAndGetType(IValue functionValue, IList<IExpression> parameters)
			{
				IExpression expression = SqlExpressionTranslator.SqlToMTranslator.NewInvocation(this.engine.GetExpression(functionValue), parameters.ToArray<IExpression>());
				expression = this.AddTableDecls(expression);
				return this.engine.Invoke(this.CompileFunction(expression), Array.Empty<IValue>()).Type.AsTableType;
			}

			// Token: 0x0600C8E3 RID: 51427 RVA: 0x00282F0C File Offset: 0x0028110C
			private IExpression NewCubeApplyParameter(IList<IExpression> parameters)
			{
				IExpression expression = this.NewTableReference(parameters[0]);
				IExpression expression2 = parameters[1];
				IExpression expression3 = parameters[2];
				IValue value;
				if (SqlExpressionTranslator.SqlToMTranslator.TryGetConstant(expression2, out value) && value.IsText)
				{
					IExpression expression4 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Cube.ApplyParameter", new IExpression[] { expression, expression2, expression3 });
					return this.Result(expression4, this.GetType(expression));
				}
				throw new NotSupportedException();
			}

			// Token: 0x1700308F RID: 12431
			// (get) Token: 0x0600C8E4 RID: 51428 RVA: 0x00282F7C File Offset: 0x0028117C
			private IFunctionValue MeasureFunctionTypeFilter
			{
				get
				{
					if (this.measureFunctionTypeFilter == null)
					{
						IFunctionValue functionValue = this.CompileFunction("(cube, id) => Value.Type(Cube.Measures(cube){[Id = id]}[Data])");
						this.measureFunctionTypeFilter = this.engine.Invoke(functionValue, Array.Empty<IValue>()).AsFunction;
					}
					return this.measureFunctionTypeFilter;
				}
			}

			// Token: 0x0600C8E5 RID: 51429 RVA: 0x00282FC0 File Offset: 0x002811C0
			private IExpression NewCubeApplyMeasure(IList<IExpression> parameters)
			{
				IValue value;
				if (SqlExpressionTranslator.SqlToMTranslator.TryGetConstant(parameters[0], out value) && value.IsText)
				{
					IExpression expression = new RequiredFieldAccessExpressionSyntaxNode(new RequiredElementAccessExpressionSyntaxNode(SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Cube.Measures", SqlExpressionTranslator.SqlToMTranslator.NewReference(this.RowParameter)), new RecordExpressionSyntaxNode(new VariableInitializer[]
					{
						new VariableInitializer(Microsoft.Mashup.Engine.Interface.Identifier.New("Id"), this.NewConstant(value))
					})), Microsoft.Mashup.Engine.Interface.Identifier.New("Data"));
					ITypeValue typeValue = this.engine.Type(TypeHandle.Any);
					IValue value2;
					if (this.TryGetCubeValue(base.Context.Table.Inputs, out value2))
					{
						typeValue = this.engine.Invoke(this.MeasureFunctionTypeFilter, new IValue[] { value2, value }).AsType.AsFunctionType.ReturnType;
					}
					IExpression expression2 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Cube.ApplyMeasure", SqlExpressionTranslator.SqlToMTranslator.NewInvocation(expression, SqlExpressionTranslator.SqlToMTranslator.NewReference(this.RowParameter)));
					return this.Result(expression2, typeValue);
				}
				throw new NotSupportedException();
			}

			// Token: 0x17003090 RID: 12432
			// (get) Token: 0x0600C8E6 RID: 51430 RVA: 0x002830C0 File Offset: 0x002812C0
			private IFunctionValue DimensionTableTypeFilter
			{
				get
				{
					if (this.dimensionTableTypeFilter == null)
					{
						IFunctionValue functionValue = this.CompileFunction("(cube, id) => Value.Type(Table.FirstN(Cube.Dimensions(cube){[Id = id]}[Data], 0))");
						this.dimensionTableTypeFilter = this.engine.Invoke(functionValue, Array.Empty<IValue>()).AsFunction;
					}
					return this.dimensionTableTypeFilter;
				}
			}

			// Token: 0x0600C8E7 RID: 51431 RVA: 0x00283104 File Offset: 0x00281304
			private IExpression NewCubeDimension(IList<IExpression> parameters)
			{
				IExpression expression = this.NewTableReference(parameters[0]);
				IExpression expression2 = parameters[1];
				IValue value;
				if (SqlExpressionTranslator.SqlToMTranslator.TryGetConstant(expression2, out value) && value.IsText)
				{
					IExpression expression3 = new RequiredFieldAccessExpressionSyntaxNode(new RequiredElementAccessExpressionSyntaxNode(SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Cube.Dimensions", expression), new RecordExpressionSyntaxNode(new VariableInitializer[]
					{
						new VariableInitializer(Microsoft.Mashup.Engine.Interface.Identifier.New("Id"), expression2)
					})), Microsoft.Mashup.Engine.Interface.Identifier.New("Data"));
					IValue value2;
					if (this.TryGetCubeValue(expression, out value2))
					{
						ITableTypeValue asTableType = this.engine.Invoke(this.DimensionTableTypeFilter, new IValue[] { value2, value }).AsType.AsTableType;
						return this.Result(expression3, asTableType);
					}
				}
				throw new NotSupportedException();
			}

			// Token: 0x0600C8E8 RID: 51432 RVA: 0x002831C8 File Offset: 0x002813C8
			private IExpression NewCount(bool distinct, IList<IExpression> parameters)
			{
				string text = null;
				IExpression expression = parameters[0];
				ITypeValue type = this.GetType(expression);
				if (type.IsNullable)
				{
					expression = this.Result(SqlExpressionTranslator.SqlToMTranslator.NewInvocation("List.Select", expression, new FunctionExpressionSyntaxNode(SqlExpressionTranslator.SqlToMTranslator.NewFunctionType(Microsoft.Mashup.Engine.Interface.Identifier.Underscore), BinaryExpressionSyntaxNode.New(BinaryOperator2.NotEquals, SqlExpressionTranslator.SqlToMTranslator.NewReference(Microsoft.Mashup.Engine.Interface.Identifier.Underscore), this.NewConstant(this.engine.Null), TokenRange.Null))), type.NonNullable);
				}
				expression = this.NewDistinct(distinct, expression);
				if (text == null)
				{
					text = (this.IsTableType(this.GetType(parameters[0])) ? "Table.RowCount" : "List.Count");
				}
				return this.Result(SqlExpressionTranslator.SqlToMTranslator.NewInvocation(text, expression), this.engine.Type(TypeHandle.Int64));
			}

			// Token: 0x0600C8E9 RID: 51433 RVA: 0x00283284 File Offset: 0x00281484
			private IExpression NewApproximateDistinctCount(bool distinct, IList<IExpression> parameters)
			{
				if (parameters.Count != 1)
				{
					throw new NotSupportedException("'approx_count_distinct' supports exactly one argument.");
				}
				IExpression expression = parameters[0];
				IExpression expression2;
				IList<IExpression> list;
				if (!this.TryGetOriginalApproxCountDistinctPattern(expression, out expression2, out list))
				{
					if (!(expression is IFieldAccessExpression))
					{
						throw new NotSupportedException("'approx_count_distinct' supports only field access.");
					}
					IFieldAccessExpression fieldAccessExpression = expression as IFieldAccessExpression;
					expression2 = fieldAccessExpression.Expression;
					list = new IExpression[] { this.NewConstant(fieldAccessExpression.MemberName) };
				}
				IExpression expression3 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Table.ApproximateRowCount", SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Table.Distinct", SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Table.SelectColumns", expression2, this.NewList(list))));
				return this.Result(expression3, this.engine.Type(TypeHandle.Int64));
			}

			// Token: 0x0600C8EA RID: 51434 RVA: 0x00283338 File Offset: 0x00281538
			private bool TryGetOriginalApproxCountDistinctPattern(IExpression input, out IExpression tableName, out IList<IExpression> tableColumns)
			{
				tableName = null;
				tableColumns = null;
				if (!this.GetType(input).IsCompatibleWith(this.engine.Type(TypeHandle.Binary).Nullable))
				{
					return false;
				}
				List<IExpression> list = new List<IExpression>();
				IList<IExpression> list2;
				if (this.TryGetBinaryCombineArguments(input, out list2))
				{
					list.AddRange(list2);
				}
				else
				{
					list.Add(input);
				}
				tableColumns = new List<IExpression>();
				foreach (IExpression expression in list)
				{
					IList<IExpression> list3;
					if (this.TryGetInvocationArguments(expression, "Binary.From", out list3))
					{
						expression = list3[0];
					}
					IExpression expression2;
					IExpression expression3;
					if (SqlExpressionTranslator.SqlToMTranslator.TryGetIsNull(expression, out expression2, out expression3))
					{
						expression = expression2;
					}
					if (!(expression is IFieldAccessExpression))
					{
						return false;
					}
					IExpression expression4 = ((IFieldAccessExpression)expression).Expression;
					Microsoft.Mashup.Engine.Interface.Identifier memberName = ((IFieldAccessExpression)expression).MemberName;
					Microsoft.Mashup.Engine.Interface.Identifier identifier;
					Microsoft.Mashup.Engine.Interface.Identifier identifier2;
					if (tableName == null)
					{
						tableName = expression4;
					}
					else if (!SqlExpressionTranslator.SqlToMTranslator.TryGetIdentifier(tableName, out identifier) || !SqlExpressionTranslator.SqlToMTranslator.TryGetIdentifier(expression4, out identifier2) || identifier.Name != identifier2.Name)
					{
						return false;
					}
					tableColumns.Add(this.NewConstant(memberName));
				}
				return true;
			}

			// Token: 0x0600C8EB RID: 51435 RVA: 0x00283478 File Offset: 0x00281678
			private IExpression NewCall(string function, ITypeValue returnType, IList<IExpression> parameters)
			{
				return this.Result(SqlExpressionTranslator.SqlToMTranslator.NewInvocation(function, parameters.ToArray<IExpression>()), returnType);
			}

			// Token: 0x0600C8EC RID: 51436 RVA: 0x0028348D File Offset: 0x0028168D
			private IExpression NewCall(IExpression function, ITypeValue returnType, IList<IExpression> parameters)
			{
				return this.Result(SqlExpressionTranslator.SqlToMTranslator.NewInvocation(function, parameters.ToArray<IExpression>()), returnType);
			}

			// Token: 0x0600C8ED RID: 51437 RVA: 0x002834A2 File Offset: 0x002816A2
			private IExpression NewDistinct(bool distinct, IExpression input)
			{
				if (distinct)
				{
					input = this.Result(SqlExpressionTranslator.SqlToMTranslator.NewInvocation("List.Distinct", input), this.GetType(input));
				}
				return input;
			}

			// Token: 0x0600C8EE RID: 51438 RVA: 0x002834C4 File Offset: 0x002816C4
			protected override Microsoft.Mashup.ScriptDom.Identifier NewColumnIdentifier()
			{
				Microsoft.Mashup.ScriptDom.Identifier identifier = new Microsoft.Mashup.ScriptDom.Identifier();
				string text = this.identifierPrefix;
				int num = this.identifierIndex;
				this.identifierIndex = num + 1;
				identifier.Value = text + num.ToString();
				return identifier;
			}

			// Token: 0x0600C8EF RID: 51439 RVA: 0x00283500 File Offset: 0x00281700
			protected override Microsoft.Mashup.ScriptDom.Identifier NewTableIdentifier()
			{
				Microsoft.Mashup.ScriptDom.Identifier identifier = new Microsoft.Mashup.ScriptDom.Identifier();
				string text = this.identifierPrefix;
				int num = this.identifierIndex;
				this.identifierIndex = num + 1;
				identifier.Value = text + num.ToString();
				return identifier;
			}

			// Token: 0x0600C8F0 RID: 51440 RVA: 0x0028353C File Offset: 0x0028173C
			private Microsoft.Mashup.Engine.Interface.Identifier NewInterfaceIdentifier(MultiPartIdentifier identifier)
			{
				string text;
				return this.NewInterfaceIdentifier(identifier, false, out text);
			}

			// Token: 0x0600C8F1 RID: 51441 RVA: 0x00283553 File Offset: 0x00281753
			private Microsoft.Mashup.Engine.Interface.Identifier NewInterfaceIdentifier(MultiPartIdentifier identifier, out string originalIdentifierName)
			{
				return this.NewInterfaceIdentifier(identifier, true, out originalIdentifierName);
			}

			// Token: 0x0600C8F2 RID: 51442 RVA: 0x00283560 File Offset: 0x00281760
			private Microsoft.Mashup.Engine.Interface.Identifier NewInterfaceIdentifier(MultiPartIdentifier identifier, bool alwaysLookup, out string originalIdentifierName)
			{
				originalIdentifierName = null;
				Microsoft.Mashup.Engine.Interface.Identifier identifier2;
				if (!this.interfaceIdentifiers.TryGetValue(identifier, out identifier2))
				{
					string text = string.Empty;
					for (int i = 0; i < identifier.Count; i++)
					{
						if (i != 0)
						{
							text += ".";
						}
						text += SqlExpressionTranslator.SqlToMTranslator.EncodeIdentifierPart(identifier[i].Value);
					}
					string text2 = SqlExpressionTranslator.SqlToMTranslator.DecodeIdentifierPart(text);
					if (!string.Equals(text, text2, StringComparison.Ordinal))
					{
						this.identifiersMap.Add(text, text2);
					}
					originalIdentifierName = text2;
					identifier2 = Microsoft.Mashup.Engine.Interface.Identifier.New(text);
					this.interfaceIdentifiers.Add(identifier, identifier2);
					this.identifiers.Add(identifier2, identifier);
				}
				else if (alwaysLookup && !this.identifiersMap.TryGetValue(identifier2, out originalIdentifierName))
				{
					originalIdentifierName = identifier2;
				}
				return identifier2;
			}

			// Token: 0x0600C8F3 RID: 51443 RVA: 0x00283628 File Offset: 0x00281828
			private MultiPartIdentifier GetIdentifier(Microsoft.Mashup.Engine.Interface.Identifier interfaceIdentifier)
			{
				MultiPartIdentifier multiPartIdentifier;
				if (!this.identifiers.TryGetValue(interfaceIdentifier, out multiPartIdentifier))
				{
					multiPartIdentifier = SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.NewIdentifier(new Microsoft.Mashup.ScriptDom.Identifier
					{
						Value = interfaceIdentifier.Name
					});
					this.interfaceIdentifiers.Add(multiPartIdentifier, interfaceIdentifier);
					this.identifiers.Add(interfaceIdentifier, multiPartIdentifier);
				}
				return multiPartIdentifier;
			}

			// Token: 0x0600C8F4 RID: 51444 RVA: 0x00283678 File Offset: 0x00281878
			private IList<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table> FlattenTableInputs(IList<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table> tables)
			{
				List<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table> list = null;
				for (int i = 0; i < tables.Count; i++)
				{
					SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table table = tables[i];
					if (table.Inputs.Count == 0)
					{
						if (list != null)
						{
							list.Add(table);
						}
					}
					else if (list == null)
					{
						list = new List<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table>();
						i = -1;
					}
					else
					{
						for (int j = 0; j < table.Inputs.Count; j++)
						{
							list.Add(table.Inputs[j]);
						}
					}
				}
				IList<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table> list2 = list;
				return list2 ?? tables;
			}

			// Token: 0x0600C8F5 RID: 51445 RVA: 0x002836F8 File Offset: 0x002818F8
			private IList<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table> PushExpressionTableRefs()
			{
				IList<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table> list = this.expressionTableRefs;
				this.expressionTableRefs = null;
				return list;
			}

			// Token: 0x0600C8F6 RID: 51446 RVA: 0x00283707 File Offset: 0x00281907
			private IList<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table> PopExpressionTableRefs(IList<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table> oldExpressionTableRefs)
			{
				IList<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table> list = this.expressionTableRefs;
				this.expressionTableRefs = oldExpressionTableRefs;
				return list;
			}

			// Token: 0x0600C8F7 RID: 51447 RVA: 0x00283718 File Offset: 0x00281918
			private void AddExpressionTableRefs(IList<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table> inputs)
			{
				if (this.expressionTableRefs == null)
				{
					this.expressionTableRefs = new List<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table>(4);
				}
				for (int i = 0; i < inputs.Count; i++)
				{
					this.expressionTableRefs.Add(inputs[i]);
				}
			}

			// Token: 0x0600C8F8 RID: 51448 RVA: 0x0028375C File Offset: 0x0028195C
			private static void AddStep(List<VariableInitializer> variables, IExpression step)
			{
				if (step != null)
				{
					if (variables.Count > 0)
					{
						step = SqlExpressionTranslator.SqlToMTranslator.NewInvocation(step, SqlExpressionTranslator.SqlToMTranslator.NewReference(variables.Last<VariableInitializer>().Name));
					}
					variables.Add(new VariableInitializer(Microsoft.Mashup.Engine.Interface.Identifier.New(), step));
				}
			}

			// Token: 0x0600C8F9 RID: 51449 RVA: 0x002837A4 File Offset: 0x002819A4
			private static IExpression AddStep(ILetExpression let, IExpression step)
			{
				VariableInitializer[] array = new VariableInitializer[let.Variables.Count + 1];
				for (int i = 0; i < let.Variables.Count; i++)
				{
					array[i] = let.Variables[i];
				}
				array[let.Variables.Count] = new VariableInitializer(Microsoft.Mashup.Engine.Interface.Identifier.New(), SqlExpressionTranslator.SqlToMTranslator.NewInvocation(step, SqlExpressionTranslator.SqlToMTranslator.NewReference(array[let.Variables.Count - 1].Name)));
				return new LetExpressionSyntaxNode(array, SqlExpressionTranslator.SqlToMTranslator.NewReference(array[array.Length - 1].Name));
			}

			// Token: 0x0600C8FA RID: 51450 RVA: 0x00283848 File Offset: 0x00281A48
			private static IExpression CombineColumnSelection(IExpression left, IExpression right)
			{
				IListExpression listExpression = left as IListExpression;
				if (listExpression != null)
				{
					if (right is IConstantExpression2)
					{
						return new ListExpressionSyntaxNode(listExpression.Members.Concat(new IExpression[] { right }).ToArray<IExpression>());
					}
					if (listExpression.Members.Count == 0)
					{
						return right;
					}
				}
				return BinaryExpressionSyntaxNode.New(BinaryOperator2.Concatenate, left, right, TokenRange.Null);
			}

			// Token: 0x0600C8FB RID: 51451 RVA: 0x002838A4 File Offset: 0x00281AA4
			private IExpression NewBinaryConstant(string value)
			{
				if (value == null || !value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
				{
					throw new NotSupportedException();
				}
				value = value.Substring(2);
				if (value.Length % 2 != 0)
				{
					value = "0" + value;
				}
				return this.Result(SqlExpressionTranslator.SqlToMTranslator.NewInvocation("Binary.FromText", this.NewConstant(value), SqlExpressionTranslator.SqlToMTranslator.NewReference("BinaryEncoding.Hex")), this.engine.Type(TypeHandle.Binary));
			}

			// Token: 0x0600C8FC RID: 51452 RVA: 0x00283915 File Offset: 0x00281B15
			private IExpression NewConstant(string value)
			{
				return this.NewConstant(this.engine.Text(value));
			}

			// Token: 0x0600C8FD RID: 51453 RVA: 0x00283929 File Offset: 0x00281B29
			private IExpression NewConstant(double value)
			{
				return this.NewConstant(this.engine.Number(value));
			}

			// Token: 0x0600C8FE RID: 51454 RVA: 0x0028393D File Offset: 0x00281B3D
			private IExpression NewConstant(IValue value)
			{
				return this.engine.ConstantExpression(value);
			}

			// Token: 0x0600C8FF RID: 51455 RVA: 0x0028394B File Offset: 0x00281B4B
			private static IInvocationExpression NewInvocation(string function, IExpression argument0)
			{
				return SqlExpressionTranslator.SqlToMTranslator.NewInvocation(SqlExpressionTranslator.SqlToMTranslator.NewReference(function), argument0);
			}

			// Token: 0x0600C900 RID: 51456 RVA: 0x00283959 File Offset: 0x00281B59
			private static IInvocationExpression NewInvocation(string function, IExpression argument0, IExpression argument1)
			{
				return SqlExpressionTranslator.SqlToMTranslator.NewInvocation(SqlExpressionTranslator.SqlToMTranslator.NewReference(function), argument0, argument1);
			}

			// Token: 0x0600C901 RID: 51457 RVA: 0x00283968 File Offset: 0x00281B68
			private static IInvocationExpression NewInvocation(string function, params IExpression[] arguments)
			{
				return SqlExpressionTranslator.SqlToMTranslator.NewInvocation(SqlExpressionTranslator.SqlToMTranslator.NewReference(function), arguments);
			}

			// Token: 0x0600C902 RID: 51458 RVA: 0x00283976 File Offset: 0x00281B76
			private static IInvocationExpression NewInvocation(IExpression function, IExpression argument0)
			{
				return new InvocationExpressionSyntaxNode1(function, argument0);
			}

			// Token: 0x0600C903 RID: 51459 RVA: 0x0028397F File Offset: 0x00281B7F
			private static IInvocationExpression NewInvocation(IExpression function, IExpression argument0, IExpression argument1)
			{
				return new InvocationExpressionSyntaxNode2(function, argument0, argument1);
			}

			// Token: 0x0600C904 RID: 51460 RVA: 0x00283989 File Offset: 0x00281B89
			private static IInvocationExpression NewInvocation(IExpression function, params IExpression[] arguments)
			{
				return new InvocationExpressionSyntaxNodeN(function, arguments);
			}

			// Token: 0x0600C905 RID: 51461 RVA: 0x00283992 File Offset: 0x00281B92
			private IBinaryExpression Add(IExpression left, IExpression right)
			{
				return BinaryExpressionSyntaxNode.New(BinaryOperator2.Add, left, right, TokenRange.Null);
			}

			// Token: 0x0600C906 RID: 51462 RVA: 0x002839A1 File Offset: 0x00281BA1
			private IBinaryExpression Subtract(IExpression left, IExpression right)
			{
				return BinaryExpressionSyntaxNode.New(BinaryOperator2.Subtract, left, right, TokenRange.Null);
			}

			// Token: 0x0600C907 RID: 51463 RVA: 0x002839B0 File Offset: 0x00281BB0
			private IBinaryExpression Multiply(IExpression left, IExpression right)
			{
				return BinaryExpressionSyntaxNode.New(BinaryOperator2.Multiply, left, right, TokenRange.Null);
			}

			// Token: 0x0600C908 RID: 51464 RVA: 0x002839BF File Offset: 0x00281BBF
			private IBinaryExpression Divide(IExpression left, IExpression right)
			{
				return BinaryExpressionSyntaxNode.New(BinaryOperator2.Divide, left, right, TokenRange.Null);
			}

			// Token: 0x0600C909 RID: 51465 RVA: 0x002839CE File Offset: 0x00281BCE
			private static IExpression NewReference(string name)
			{
				return SqlExpressionTranslator.SqlToMTranslator.NewReference(Microsoft.Mashup.Engine.Interface.Identifier.New(name));
			}

			// Token: 0x0600C90A RID: 51466 RVA: 0x002839DB File Offset: 0x00281BDB
			private static IExpression NewReference(Microsoft.Mashup.Engine.Interface.Identifier identifier)
			{
				return new ExclusiveIdentifierExpressionSyntaxNode(identifier);
			}

			// Token: 0x0600C90B RID: 51467 RVA: 0x002839E3 File Offset: 0x00281BE3
			private static IFunctionTypeExpression NewFunctionType(Microsoft.Mashup.Engine.Interface.Identifier parameter)
			{
				return new FunctionTypeSyntaxNode(null, new IParameter[]
				{
					new ParameterSyntaxNode(parameter, null)
				}, 1);
			}

			// Token: 0x0600C90C RID: 51468 RVA: 0x002839FC File Offset: 0x00281BFC
			private bool IsTableType(ITypeValue type)
			{
				return type.NonNullable.IsCompatibleWith(this.engine.Type(TypeHandle.Table));
			}

			// Token: 0x0600C90D RID: 51469 RVA: 0x00283A18 File Offset: 0x00281C18
			private IList<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding> MakeNullable(IList<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding> columns)
			{
				SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding[] array = new SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding[columns.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.ColumnBinding
					{
						Identifier = columns[i].Identifier,
						Value = new SqlExpressionTranslator.RowParameterAndType
						{
							RowParameter = columns[i].Value.RowParameter,
							Type = columns[i].Value.Type.Nullable
						}
					};
				}
				return array;
			}

			// Token: 0x0600C90E RID: 51470 RVA: 0x00283AB4 File Offset: 0x00281CB4
			private ITypeValue UnionTypesAndPromoteExpressions(ref IExpression expression1, ref IExpression expression2)
			{
				ITypeValue typeValue = this.GetType(expression1);
				ITypeValue typeValue2 = this.GetType(expression2);
				if (this.IsDateTimeType(typeValue) && (this.IsDateType(typeValue2) || this.IsTimeType(typeValue2)))
				{
					expression2 = this.NewCastCall(SqlExpressionTranslator.SqlToMTranslator.DateTimeTypeReference, expression2);
					typeValue2 = this.engine.Type(TypeHandle.DateTime);
				}
				if (this.IsDateTimeType(typeValue2) && (this.IsDateType(typeValue) || this.IsTimeType(typeValue)))
				{
					expression1 = this.NewCastCall(SqlExpressionTranslator.SqlToMTranslator.DateTimeTypeReference, expression1);
					typeValue = this.engine.Type(TypeHandle.DateTime);
				}
				if (this.EqualsType(typeValue, TypeHandle.Logical) && this.IsNumericType(typeValue2))
				{
					expression1 = this.NewCastCall(this.GetTypeReference(typeValue2), expression1);
					typeValue = (typeValue.IsNullable ? typeValue2.Nullable : typeValue2);
				}
				else if (this.EqualsType(typeValue2, TypeHandle.Logical) && this.IsNumericType(typeValue))
				{
					expression2 = this.NewCastCall(this.GetTypeReference(typeValue), expression2);
					typeValue2 = (typeValue2.IsNullable ? typeValue.Nullable : typeValue);
				}
				return this.UnionTypes(typeValue, typeValue2);
			}

			// Token: 0x0600C90F RID: 51471 RVA: 0x00283BBC File Offset: 0x00281DBC
			private ITypeValue UnionTypes(ITypeValue type1, ITypeValue type2)
			{
				if (this.IsNumericType(type1) && ((type1.NonNullable.Equals(this.engine.Type(TypeHandle.Decimal)) && !type2.NonNullable.Equals(this.engine.Type(TypeHandle.Double))) || (type2.NonNullable.Equals(this.engine.Type(TypeHandle.Decimal)) && !type1.NonNullable.Equals(this.engine.Type(TypeHandle.Double)))))
				{
					ITypeValue typeValue = this.engine.Type(TypeHandle.Decimal);
					if (type1.IsNullable || type2.IsNullable)
					{
						typeValue = typeValue.Nullable;
					}
					return typeValue;
				}
				if (type2.IsNullable && !type1.IsNullable)
				{
					type1 = type1.Nullable;
				}
				if (type1.IsNullable && !type2.IsNullable)
				{
					type2 = type2.Nullable;
				}
				if (type2.IsCompatibleWith(type1))
				{
					return type1;
				}
				return this.engine.Type(TypeHandle.Any);
			}

			// Token: 0x0600C910 RID: 51472 RVA: 0x00283CAC File Offset: 0x00281EAC
			private bool IsDateTimeType(ITypeValue type)
			{
				return type.IsCompatibleWith(this.engine.Type(TypeHandle.DateTime).Nullable);
			}

			// Token: 0x0600C911 RID: 51473 RVA: 0x00283CC6 File Offset: 0x00281EC6
			private bool IsDateType(ITypeValue type)
			{
				return type.IsCompatibleWith(this.engine.Type(TypeHandle.Date).Nullable);
			}

			// Token: 0x0600C912 RID: 51474 RVA: 0x00283CE0 File Offset: 0x00281EE0
			private bool IsTimeType(ITypeValue type)
			{
				return type.IsCompatibleWith(this.engine.Type(TypeHandle.Time).Nullable);
			}

			// Token: 0x0600C913 RID: 51475 RVA: 0x00283CFA File Offset: 0x00281EFA
			private bool IsNumericType(ITypeValue type)
			{
				return type.IsCompatibleWith(this.engine.Type(TypeHandle.Number).Nullable);
			}

			// Token: 0x0600C914 RID: 51476 RVA: 0x00283D13 File Offset: 0x00281F13
			private bool EqualsType(ITypeValue type1, TypeHandle type2)
			{
				return type1.NonNullable.Equals(this.engine.Type(type2));
			}

			// Token: 0x0600C915 RID: 51477 RVA: 0x00283D2C File Offset: 0x00281F2C
			private IExpression GetTypeReference(ITypeValue type)
			{
				IExpression expression;
				if (!this.TryGetTypeReference(type, out expression))
				{
					throw new InvalidOperationException();
				}
				return expression;
			}

			// Token: 0x0600C916 RID: 51478 RVA: 0x00283D4B File Offset: 0x00281F4B
			private bool TryGetTypeReference(ITypeValue type, out IExpression typeReference)
			{
				return this.typeReferenceMap.TryGetValue(type.NonNullable, out typeReference);
			}

			// Token: 0x0600C917 RID: 51479 RVA: 0x00283D60 File Offset: 0x00281F60
			private ITypeValue GetType(IExpression expression)
			{
				ITypeValue typeValue;
				if (!this.TryGetType(expression, out typeValue))
				{
					throw new InvalidOperationException();
				}
				return typeValue;
			}

			// Token: 0x0600C918 RID: 51480 RVA: 0x00283D80 File Offset: 0x00281F80
			private bool TryGetType(IExpression expression, out ITypeValue type)
			{
				if (this.types.TryGetValue(expression, out type))
				{
					return true;
				}
				ExpressionKind kind = expression.Kind;
				if (kind != ExpressionKind.Constant)
				{
					if (kind == ExpressionKind.Invocation)
					{
						IInvocationExpression invocationExpression = (IInvocationExpression)expression;
						Microsoft.Mashup.Engine.Interface.Identifier identifier;
						if (SqlExpressionTranslator.SqlToMTranslator.TryGetIdentifier(invocationExpression.Function, out identifier))
						{
							string name = identifier.Name;
							if (name != null)
							{
								switch (name.Length)
								{
								case 9:
								{
									char c = name[1];
									if (c <= 'e')
									{
										if (c != 'a')
										{
											if (c == 'e')
											{
												if (name == "Text.From")
												{
													type = this.engine.Type(TypeHandle.Text);
													goto IL_03FF;
												}
											}
										}
										else if (name == "Date.From")
										{
											type = this.engine.Type(TypeHandle.Date);
											goto IL_03FF;
										}
									}
									else if (c != 'i')
									{
										if (c == 'n')
										{
											if (name == "Int8.From")
											{
												type = this.engine.Type(TypeHandle.Int8);
												goto IL_03FF;
											}
										}
									}
									else if (name == "Time.From")
									{
										type = this.engine.Type(TypeHandle.Time);
										goto IL_03FF;
									}
									break;
								}
								case 10:
								{
									char c = name[3];
									if (c != '1')
									{
										if (c != '3')
										{
											if (c == '6')
											{
												if (name == "Int64.From")
												{
													type = this.engine.Type(TypeHandle.Int64);
													goto IL_03FF;
												}
											}
										}
										else if (name == "Int32.From")
										{
											type = this.engine.Type(TypeHandle.Int32);
											goto IL_03FF;
										}
									}
									else if (name == "Int16.From")
									{
										type = this.engine.Type(TypeHandle.Int16);
										goto IL_03FF;
									}
									break;
								}
								case 11:
								{
									char c = name[0];
									if (c != 'B')
									{
										if (c != 'D')
										{
											if (c == 'S')
											{
												if (name == "Single.From")
												{
													type = this.engine.Type(TypeHandle.Single);
													goto IL_03FF;
												}
											}
										}
										else if (name == "Double.From")
										{
											type = this.engine.Type(TypeHandle.Double);
											goto IL_03FF;
										}
									}
									else if (name == "Binary.From")
									{
										type = this.engine.Type(TypeHandle.Binary);
										goto IL_03FF;
									}
									break;
								}
								case 12:
								{
									char c = name[0];
									if (c != 'D')
									{
										if (c == 'L')
										{
											if (name == "Logical.From")
											{
												type = this.engine.Type(TypeHandle.Logical);
												goto IL_03FF;
											}
										}
									}
									else if (name == "Decimal.From")
									{
										type = this.engine.Type(TypeHandle.Decimal);
										goto IL_03FF;
									}
									break;
								}
								case 13:
								{
									char c = name[3];
									if (c != 'a')
									{
										if (c != 'e')
										{
											if (c == 'r')
											{
												if (name == "Currency.From")
												{
													type = this.engine.Type(TypeHandle.Currency);
													goto IL_03FF;
												}
											}
										}
										else if (name == "DateTime.From")
										{
											type = this.engine.Type(TypeHandle.DateTime);
											goto IL_03FF;
										}
									}
									else if (name == "Duration.From")
									{
										type = this.engine.Type(TypeHandle.Duration);
										goto IL_03FF;
									}
									break;
								}
								case 17:
									if (name == "DateTimeZone.From")
									{
										type = this.engine.Type(TypeHandle.DateTimeZone);
										goto IL_03FF;
									}
									break;
								}
							}
							type = null;
							IL_03FF:
							ITypeValue typeValue;
							if (type != null && (invocationExpression.Arguments.Count < 1 || !this.TryGetType(invocationExpression.Arguments[0], out typeValue) || typeValue.IsNullable))
							{
								type = type.Nullable;
							}
							return true;
						}
					}
					type = null;
					return false;
				}
				type = ((IConstantExpression2)expression).Value.Type;
				return true;
			}

			// Token: 0x0600C919 RID: 51481 RVA: 0x002841CB File Offset: 0x002823CB
			private IExpression Result(IExpression expression, ITypeValue type)
			{
				this.types[expression] = type;
				return expression;
			}

			// Token: 0x0600C91A RID: 51482 RVA: 0x002841DC File Offset: 0x002823DC
			private bool TryGetCubeValue(IList<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table> tables, out IValue value)
			{
				for (int i = 0; i < tables.Count; i++)
				{
					if (this.TryGetCubeValue(tables[i].Expression, out value))
					{
						return true;
					}
				}
				value = null;
				return false;
			}

			// Token: 0x0600C91B RID: 51483 RVA: 0x00284218 File Offset: 0x00282418
			private bool TryGetCubeValue(IExpression expression, out IValue value)
			{
				IIdentifierExpression identifierExpression = expression as IIdentifierExpression;
				if (identifierExpression != null)
				{
					value = null;
					SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table table;
					return this.tables.TryGetValue(identifierExpression.Name, out table) && this.TryGetCubeValue(table.Inputs, out value);
				}
				IFieldAccessExpression fieldAccessExpression = expression as IFieldAccessExpression;
				if (fieldAccessExpression != null && !fieldAccessExpression.IsOptional)
				{
					IIdentifierExpression identifierExpression2 = fieldAccessExpression.Expression as IIdentifierExpression;
					if (identifierExpression2 != null && identifierExpression2.Name.Equals(SqlExpressionTranslator.SqlToMTranslator.environmentFunctionType.Parameters[0].Identifier))
					{
						value = this.environment[fieldAccessExpression.MemberName.Name];
						return true;
					}
				}
				IInvocationExpression invocationExpression = expression as IInvocationExpression;
				if (invocationExpression != null)
				{
					IFieldAccessExpression fieldAccessExpression2 = invocationExpression.Function as IFieldAccessExpression;
					if (fieldAccessExpression2 != null && !fieldAccessExpression2.IsOptional)
					{
						IIdentifierExpression identifierExpression3 = fieldAccessExpression2.Expression as IIdentifierExpression;
						IValue value2;
						if (identifierExpression3 != null && identifierExpression3.Name.Equals(SqlExpressionTranslator.SqlToMTranslator.environmentFunctionType.Parameters[0].Identifier) && this.environment.TryGetValue(fieldAccessExpression2.MemberName.Name, out value2) && value2.IsFunction)
						{
							IExpression expression2 = SqlExpressionTranslator.SqlToMTranslator.NewInvocation(this.engine.GetExpression(value2), invocationExpression.Arguments.ToArray<IExpression>());
							expression2 = this.AddTableDecls(expression2);
							value = this.engine.Invoke(this.CompileFunction(expression2), Array.Empty<IValue>());
							return true;
						}
					}
				}
				value = null;
				return false;
			}

			// Token: 0x0600C91C RID: 51484 RVA: 0x0028438C File Offset: 0x0028258C
			private static bool TryGetIsNull(IExpression expression, out IExpression baseExpression, out IExpression ifNullExpression)
			{
				if (expression is IIfExpression && ((IIfExpression)expression).Condition is IBinaryExpression)
				{
					IIfExpression ifExpression = (IIfExpression)expression;
					IBinaryExpression binaryExpression = (IBinaryExpression)ifExpression.Condition;
					IValue value;
					if (binaryExpression.Operator == BinaryOperator2.Equals && SqlExpressionTranslator.SqlToMTranslator.TryGetConstant(binaryExpression.Right, out value) && value.IsNull && binaryExpression.Left == ifExpression.FalseCase)
					{
						baseExpression = binaryExpression.Left;
						ifNullExpression = ifExpression.TrueCase;
						return true;
					}
				}
				baseExpression = null;
				ifNullExpression = null;
				return false;
			}

			// Token: 0x0600C91D RID: 51485 RVA: 0x00284410 File Offset: 0x00282610
			private static bool TryGetInt64(IExpression expression, out long longVal)
			{
				IValue value;
				if (SqlExpressionTranslator.SqlToMTranslator.TryGetConstant(expression, out value) && value.IsNumber)
				{
					INumberValue asNumber = value.AsNumber;
					if (asNumber.IsInteger64)
					{
						longVal = asNumber.AsInteger64;
						return true;
					}
				}
				longVal = 0L;
				return false;
			}

			// Token: 0x0600C91E RID: 51486 RVA: 0x00284450 File Offset: 0x00282650
			private static bool TryGetConstant(IExpression expression, out IValue value)
			{
				IConstantExpression2 constantExpression = expression as IConstantExpression2;
				if (constantExpression != null)
				{
					value = constantExpression.Value;
					return true;
				}
				value = null;
				return false;
			}

			// Token: 0x0600C91F RID: 51487 RVA: 0x00284478 File Offset: 0x00282678
			private static bool TryGetIdentifier(IExpression expression, out Microsoft.Mashup.Engine.Interface.Identifier identifier)
			{
				IIdentifierExpression identifierExpression = expression as IIdentifierExpression;
				if (identifierExpression != null)
				{
					identifier = identifierExpression.Name;
					return true;
				}
				identifier = null;
				return false;
			}

			// Token: 0x0600C920 RID: 51488 RVA: 0x002844A0 File Offset: 0x002826A0
			private static bool TryGetColumnReference(Microsoft.Mashup.Engine.Interface.Identifier parameter, IExpression expression, out Microsoft.Mashup.Engine.Interface.Identifier column)
			{
				IFieldAccessExpression fieldAccessExpression = expression as IFieldAccessExpression;
				if (fieldAccessExpression != null && !fieldAccessExpression.IsOptional)
				{
					IIdentifierExpression identifierExpression = fieldAccessExpression.Expression as IIdentifierExpression;
					if (identifierExpression != null && identifierExpression.Name.Equals(parameter))
					{
						column = fieldAccessExpression.MemberName;
						return true;
					}
				}
				column = null;
				return false;
			}

			// Token: 0x0600C921 RID: 51489 RVA: 0x002844EA File Offset: 0x002826EA
			private static string EncodeIdentifierPart(string input)
			{
				return input.Replace(".", "..");
			}

			// Token: 0x0600C922 RID: 51490 RVA: 0x002844FC File Offset: 0x002826FC
			private static string DecodeIdentifierPart(string input)
			{
				return input.Replace("..", ".");
			}

			// Token: 0x0600C923 RID: 51491 RVA: 0x0028450E File Offset: 0x0028270E
			private bool? IsAggregate(IList<IExpression> groupKey, IList<IExpression> groupingSpecs, IExpression function, out IExpression newFunction, out IDictionary<Microsoft.Mashup.Engine.Interface.Identifier, IExpression> newInputs)
			{
				return this.aggregateDetectionVisitor.IsAggregate(groupKey, groupingSpecs, function, out newFunction, out newInputs);
			}

			// Token: 0x0400664C RID: 26188
			private const int scalarSubqueryComplexityLimit = 5000;

			// Token: 0x0400664D RID: 26189
			private const string cubeApplyMeasureFunction = "Cube.ApplyMeasure";

			// Token: 0x0400664E RID: 26190
			private static readonly IListExpression EmptyList = new ListExpressionSyntaxNode(EmptyArray<IExpression>.Instance);

			// Token: 0x0400664F RID: 26191
			private static readonly IFunctionTypeExpression environmentFunctionType = SqlExpressionTranslator.SqlToMTranslator.NewFunctionType(Microsoft.Mashup.Engine.Interface.Identifier.New("environment"));

			// Token: 0x04006650 RID: 26192
			private static readonly IFunctionTypeExpression tableFunctionType = SqlExpressionTranslator.SqlToMTranslator.NewFunctionType(Microsoft.Mashup.Engine.Interface.Identifier.New("table"));

			// Token: 0x04006651 RID: 26193
			private static readonly IExpression Int64FromReference = SqlExpressionTranslator.SqlToMTranslator.NewReference("Int64.From");

			// Token: 0x04006652 RID: 26194
			private static readonly IExpression Int32FromReference = SqlExpressionTranslator.SqlToMTranslator.NewReference("Int32.From");

			// Token: 0x04006653 RID: 26195
			private static readonly IExpression Int16FromReference = SqlExpressionTranslator.SqlToMTranslator.NewReference("Int16.From");

			// Token: 0x04006654 RID: 26196
			private static readonly IExpression Int8FromReference = SqlExpressionTranslator.SqlToMTranslator.NewReference("Int8.From");

			// Token: 0x04006655 RID: 26197
			private static readonly IExpression LogicalFromReference = SqlExpressionTranslator.SqlToMTranslator.NewReference("Logical.From");

			// Token: 0x04006656 RID: 26198
			private static readonly IExpression DecimalFromReference = SqlExpressionTranslator.SqlToMTranslator.NewReference("Decimal.From");

			// Token: 0x04006657 RID: 26199
			private static readonly IExpression CurrencyFromReference = SqlExpressionTranslator.SqlToMTranslator.NewReference("Currency.From");

			// Token: 0x04006658 RID: 26200
			private static readonly IExpression PercentageFromReference = SqlExpressionTranslator.SqlToMTranslator.NewReference("Percentage.From");

			// Token: 0x04006659 RID: 26201
			private static readonly IExpression DoubleFromReference = SqlExpressionTranslator.SqlToMTranslator.NewReference("Double.From");

			// Token: 0x0400665A RID: 26202
			private static readonly IExpression SingleFromReference = SqlExpressionTranslator.SqlToMTranslator.NewReference("Single.From");

			// Token: 0x0400665B RID: 26203
			private static readonly IExpression DateFromReference = SqlExpressionTranslator.SqlToMTranslator.NewReference("Date.From");

			// Token: 0x0400665C RID: 26204
			private static readonly IExpression TimeFromReference = SqlExpressionTranslator.SqlToMTranslator.NewReference("Time.From");

			// Token: 0x0400665D RID: 26205
			private static readonly IExpression DateTimeFromReference = SqlExpressionTranslator.SqlToMTranslator.NewReference("DateTime.From");

			// Token: 0x0400665E RID: 26206
			private static readonly IExpression DateTimeZoneFromReference = SqlExpressionTranslator.SqlToMTranslator.NewReference("DateTimeZone.From");

			// Token: 0x0400665F RID: 26207
			private static readonly IExpression DurationFromReference = SqlExpressionTranslator.SqlToMTranslator.NewReference("Duration.From");

			// Token: 0x04006660 RID: 26208
			private static readonly IExpression BinaryFromReference = SqlExpressionTranslator.SqlToMTranslator.NewReference("Binary.From");

			// Token: 0x04006661 RID: 26209
			private static readonly IExpression TextFromReference = SqlExpressionTranslator.SqlToMTranslator.NewReference("Text.From");

			// Token: 0x04006662 RID: 26210
			private static readonly IExpression TextFromBinary = SqlExpressionTranslator.SqlToMTranslator.NewReference("Text.FromBinary");

			// Token: 0x04006663 RID: 26211
			private static readonly IExpression TextToBinary = SqlExpressionTranslator.SqlToMTranslator.NewReference("Text.ToBinary");

			// Token: 0x04006664 RID: 26212
			private static readonly IExpression Int64TypeReference = SqlExpressionTranslator.SqlToMTranslator.NewReference("Int64.Type");

			// Token: 0x04006665 RID: 26213
			private static readonly IExpression Int32TypeReference = SqlExpressionTranslator.SqlToMTranslator.NewReference("Int32.Type");

			// Token: 0x04006666 RID: 26214
			private static readonly IExpression Int16TypeReference = SqlExpressionTranslator.SqlToMTranslator.NewReference("Int16.Type");

			// Token: 0x04006667 RID: 26215
			private static readonly IExpression Int8TypeReference = SqlExpressionTranslator.SqlToMTranslator.NewReference("Int8.Type");

			// Token: 0x04006668 RID: 26216
			private static readonly IExpression LogicalTypeReference = SqlExpressionTranslator.SqlToMTranslator.NewReference("Logical.Type");

			// Token: 0x04006669 RID: 26217
			private static readonly IExpression DecimalTypeReference = SqlExpressionTranslator.SqlToMTranslator.NewReference("Decimal.Type");

			// Token: 0x0400666A RID: 26218
			private static readonly IExpression CurrencyTypeReference = SqlExpressionTranslator.SqlToMTranslator.NewReference("Currency.Type");

			// Token: 0x0400666B RID: 26219
			private static readonly IExpression DoubleTypeReference = SqlExpressionTranslator.SqlToMTranslator.NewReference("Double.Type");

			// Token: 0x0400666C RID: 26220
			private static readonly IExpression SingleTypeReference = SqlExpressionTranslator.SqlToMTranslator.NewReference("Single.Type");

			// Token: 0x0400666D RID: 26221
			private static readonly IExpression DateTypeReference = SqlExpressionTranslator.SqlToMTranslator.NewReference("Date.Type");

			// Token: 0x0400666E RID: 26222
			private static readonly IExpression TimeTypeReference = SqlExpressionTranslator.SqlToMTranslator.NewReference("Time.Type");

			// Token: 0x0400666F RID: 26223
			private static readonly IExpression DateTimeTypeReference = SqlExpressionTranslator.SqlToMTranslator.NewReference("DateTime.Type");

			// Token: 0x04006670 RID: 26224
			private static readonly IExpression DateTimeZoneTypeReference = SqlExpressionTranslator.SqlToMTranslator.NewReference("DateTimeZone.Type");

			// Token: 0x04006671 RID: 26225
			private static readonly IExpression DurationTypeReference = SqlExpressionTranslator.SqlToMTranslator.NewReference("Duration.Type");

			// Token: 0x04006672 RID: 26226
			private static readonly IExpression BinaryTypeReference = SqlExpressionTranslator.SqlToMTranslator.NewReference("Binary.Type");

			// Token: 0x04006673 RID: 26227
			private static readonly IExpression TextTypeReference = SqlExpressionTranslator.SqlToMTranslator.NewReference("Text.Type");

			// Token: 0x04006674 RID: 26228
			private readonly Dictionary<string, Func<IList<IExpression>, IExpression>> functionMap;

			// Token: 0x04006675 RID: 26229
			private readonly Dictionary<string, Func<bool, IList<IExpression>, IExpression>> aggregateFunctionMap;

			// Token: 0x04006676 RID: 26230
			private readonly Dictionary<ITypeValue, IExpression> typeReferenceMap;

			// Token: 0x04006677 RID: 26231
			private readonly IEngineHost host;

			// Token: 0x04006678 RID: 26232
			private readonly IEngine engine;

			// Token: 0x04006679 RID: 26233
			private readonly IRecordValue environment;

			// Token: 0x0400667A RID: 26234
			private readonly SqlExpressionTranslator.SqlToMTranslator.AggregateDetectionVisitor aggregateDetectionVisitor;

			// Token: 0x0400667B RID: 26235
			private string identifierPrefix;

			// Token: 0x0400667C RID: 26236
			private int identifierIndex;

			// Token: 0x0400667D RID: 26237
			private Dictionary<IExpression, ITypeValue> types;

			// Token: 0x0400667E RID: 26238
			private Dictionary<Microsoft.Mashup.Engine.Interface.Identifier, SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table> tables;

			// Token: 0x0400667F RID: 26239
			private List<VariableInitializer> tableDecls;

			// Token: 0x04006680 RID: 26240
			private Dictionary<Microsoft.Mashup.Engine.Interface.Identifier, MultiPartIdentifier> identifiers;

			// Token: 0x04006681 RID: 26241
			private Dictionary<MultiPartIdentifier, Microsoft.Mashup.Engine.Interface.Identifier> interfaceIdentifiers;

			// Token: 0x04006682 RID: 26242
			private Dictionary<string, string> identifiersMap;

			// Token: 0x04006683 RID: 26243
			private List<Microsoft.Mashup.Engine.Interface.Identifier> rowParameters;

			// Token: 0x04006684 RID: 26244
			private IConstantExpression2 durationConstructor;

			// Token: 0x04006685 RID: 26245
			private ITypeValue listOfAnyType;

			// Token: 0x04006686 RID: 26246
			private ITypeValue recordOfAnyType;

			// Token: 0x04006687 RID: 26247
			private IFunctionValue tableTypeFromFieldsCtor;

			// Token: 0x04006688 RID: 26248
			private IFunctionValue dimensionTableTypeFilter;

			// Token: 0x04006689 RID: 26249
			private IFunctionValue measureFunctionTypeFilter;

			// Token: 0x0400668A RID: 26250
			private Dictionary<ITypeValue, ITypeValue> signReturnTypeMap;

			// Token: 0x0400668B RID: 26251
			private Dictionary<ITypeValue, ITypeValue> roundReturnTypeMap;

			// Token: 0x0400668C RID: 26252
			private IList<SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.Table> expressionTableRefs;

			// Token: 0x0200202D RID: 8237
			private sealed class AggregateDetectionVisitor : AstVisitor2
			{
				// Token: 0x0600C943 RID: 51523 RVA: 0x00284A9F File Offset: 0x00282C9F
				public AggregateDetectionVisitor(SqlExpressionTranslator.SqlToMTranslator translator)
				{
					this.translator = translator;
					this.expressionComparer = new ExpressionComparer(true);
				}

				// Token: 0x0600C944 RID: 51524 RVA: 0x00284ABC File Offset: 0x00282CBC
				public bool? IsAggregate(IList<IExpression> groupKey, IList<IExpression> groupingSpecs, IExpression function, out IExpression newFunction, out IDictionary<Microsoft.Mashup.Engine.Interface.Identifier, IExpression> newInputs)
				{
					this.groupKey = groupKey;
					this.groupingSpecs = groupingSpecs;
					this.functionType = ((IFunctionExpression)function).FunctionType;
					this.mutableFunction = null;
					this.newInputs = null;
					this.insideAggregate = false;
					this.haveAggregates = false;
					this.haveNonAggregates = false;
					newFunction = this.VisitExpression(function);
					newInputs = this.newInputs;
					if (this.haveAggregates && this.haveNonAggregates)
					{
						throw new InvalidOperationException();
					}
					if (this.haveAggregates)
					{
						return new bool?(true);
					}
					if (this.haveNonAggregates)
					{
						return new bool?(false);
					}
					return null;
				}

				// Token: 0x0600C945 RID: 51525 RVA: 0x00284B5C File Offset: 0x00282D5C
				protected override IExpression VisitExpression(IExpression expression)
				{
					if (!this.insideAggregate)
					{
						for (int i = 0; i < this.groupingSpecs.Count; i++)
						{
							using (SqlExpressionTranslator.SqlToMTranslator.AggregateDetectionVisitor.FunctionWrapper functionWrapper = new SqlExpressionTranslator.SqlToMTranslator.AggregateDetectionVisitor.FunctionWrapper(this, expression))
							{
								if (this.expressionComparer.Equals(this.groupingSpecs[i], functionWrapper.Function))
								{
									this.haveAggregates = true;
									return this.translator.Result(SqlExpressionTranslator.SqlToMTranslator.NewInvocation("List.First", new RequiredFieldAccessExpressionSyntaxNode(new ExclusiveIdentifierExpressionSyntaxNode(this.functionType.Parameters[0].Identifier), Microsoft.Mashup.Engine.Interface.Identifier.New(((IConstantExpression2)this.groupKey[i]).Value.AsString))), this.translator.GetType(expression));
								}
							}
						}
					}
					IExpression expression2 = base.VisitExpression(expression);
					ITypeValue typeValue;
					if (expression2 != expression && this.translator.TryGetType(expression, out typeValue))
					{
						this.translator.Result(expression2, typeValue);
					}
					return expression2;
				}

				// Token: 0x0600C946 RID: 51526 RVA: 0x00284C74 File Offset: 0x00282E74
				protected override IExpression VisitInvocation(IInvocationExpression invocation)
				{
					bool flag = this.insideAggregate;
					bool flag2 = false;
					Microsoft.Mashup.Engine.Interface.Identifier identifier;
					if (SqlExpressionTranslator.SqlToMTranslator.TryGetIdentifier(invocation.Function, out identifier) && SqlExpressionTranslator.SqlToMTranslator.AggregateDetectionVisitor.IsAggregateFunction(identifier))
					{
						this.haveAggregates = true;
						this.insideAggregate = true;
						flag2 = identifier.Name == "Cube.ApplyMeasure";
					}
					invocation = (IInvocationExpression)base.VisitInvocation(invocation);
					IExpression expression = invocation;
					if (flag2)
					{
						expression = invocation.Arguments[0];
						invocation = expression as IInvocationExpression;
					}
					if (!flag2 && !flag && this.insideAggregate && invocation != null && SqlExpressionTranslator.SqlToMTranslator.AggregateDetectionVisitor.IsNonAggregateExpression(invocation.Arguments[0]))
					{
						if (this.newInputs == null)
						{
							this.newInputs = new Dictionary<Microsoft.Mashup.Engine.Interface.Identifier, IExpression>();
						}
						Microsoft.Mashup.Engine.Interface.Identifier identifier2 = this.translator.NewInterfaceIdentifier(SqlExpressionVisitor<IExpression, SqlExpressionTranslator.RowParameterAndType>.NewIdentifier(this.translator.NewColumnIdentifier()));
						IFunctionExpression functionExpression = new FunctionExpressionSyntaxNode(this.functionType, invocation.Arguments[0]);
						this.newInputs.Add(identifier2, functionExpression);
						expression = this.translator.Result(SqlExpressionTranslator.SqlToMTranslator.NewInvocation(invocation.Function, new RequiredFieldAccessExpressionSyntaxNode(SqlExpressionTranslator.SqlToMTranslator.NewReference(this.functionType.Parameters[0].Identifier), identifier2)), this.translator.GetType(invocation));
					}
					this.insideAggregate = flag;
					return expression;
				}

				// Token: 0x0600C947 RID: 51527 RVA: 0x00284DC0 File Offset: 0x00282FC0
				protected override IExpression VisitIdentifier(IIdentifierExpression identifier)
				{
					if (!this.insideAggregate && identifier.Name.Equals(this.functionType.Parameters[0].Identifier))
					{
						this.haveNonAggregates = true;
					}
					return base.VisitIdentifier(identifier);
				}

				// Token: 0x0600C948 RID: 51528 RVA: 0x001AD352 File Offset: 0x001AB552
				protected override IExpression VisitImplicitIdentifier(IImplicitIdentifierExpression identifier)
				{
					return this.VisitIdentifier(identifier);
				}

				// Token: 0x0600C949 RID: 51529 RVA: 0x00284DFB File Offset: 0x00282FFB
				private ITypeValue GetType(IExpression expression)
				{
					return this.translator.GetType(expression);
				}

				// Token: 0x0600C94A RID: 51530 RVA: 0x00284E09 File Offset: 0x00283009
				private IExpression Result(IExpression expression, ITypeValue type)
				{
					return this.translator.Result(expression, type);
				}

				// Token: 0x0600C94B RID: 51531 RVA: 0x00284E18 File Offset: 0x00283018
				private static bool IsNonAggregateExpression(IExpression expression)
				{
					ExpressionKind kind = expression.Kind;
					if (kind != ExpressionKind.Constant)
					{
						switch (kind)
						{
						case ExpressionKind.FieldAccess:
							return SqlExpressionTranslator.SqlToMTranslator.AggregateDetectionVisitor.IsNonAggregateExpression(((IFieldAccessExpression)expression).Expression);
						case ExpressionKind.Identifier:
							return false;
						case ExpressionKind.Invocation:
						{
							IInvocationExpression invocationExpression = (IInvocationExpression)expression;
							Microsoft.Mashup.Engine.Interface.Identifier identifier;
							if (SqlExpressionTranslator.SqlToMTranslator.TryGetIdentifier(invocationExpression.Function, out identifier) && SqlExpressionTranslator.SqlToMTranslator.AggregateDetectionVisitor.IsNeutralFunction(identifier))
							{
								return SqlExpressionTranslator.SqlToMTranslator.AggregateDetectionVisitor.IsNonAggregateExpression(invocationExpression.Arguments[0]);
							}
							break;
						}
						}
						return true;
					}
					return false;
				}

				// Token: 0x0600C94C RID: 51532 RVA: 0x00284E92 File Offset: 0x00283092
				private static bool IsAggregateFunction(Microsoft.Mashup.Engine.Interface.Identifier functionName)
				{
					return SqlExpressionTranslator.SqlToMTranslator.AggregateDetectionVisitor.aggregateFunctions.Contains(functionName.Name);
				}

				// Token: 0x0600C94D RID: 51533 RVA: 0x00284EA4 File Offset: 0x002830A4
				private static bool IsNeutralFunction(Microsoft.Mashup.Engine.Interface.Identifier functionName)
				{
					return SqlExpressionTranslator.SqlToMTranslator.AggregateDetectionVisitor.neutralFunctions.Contains(functionName.Name);
				}

				// Token: 0x0400668D RID: 26253
				private readonly SqlExpressionTranslator.SqlToMTranslator translator;

				// Token: 0x0400668E RID: 26254
				private readonly ExpressionComparer expressionComparer;

				// Token: 0x0400668F RID: 26255
				private IList<IExpression> groupKey;

				// Token: 0x04006690 RID: 26256
				private IList<IExpression> groupingSpecs;

				// Token: 0x04006691 RID: 26257
				private IFunctionTypeExpression functionType;

				// Token: 0x04006692 RID: 26258
				private SqlExpressionTranslator.SqlToMTranslator.AggregateDetectionVisitor.MutableFunctionExpressionSyntaxNode mutableFunction;

				// Token: 0x04006693 RID: 26259
				private Dictionary<Microsoft.Mashup.Engine.Interface.Identifier, IExpression> newInputs;

				// Token: 0x04006694 RID: 26260
				private bool insideAggregate;

				// Token: 0x04006695 RID: 26261
				private bool haveAggregates;

				// Token: 0x04006696 RID: 26262
				private bool haveNonAggregates;

				// Token: 0x04006697 RID: 26263
				private static readonly HashSet<string> aggregateFunctions = new HashSet<string> { "List.Sum", "List.Average", "List.Min", "List.Max", "List.Count", "List.StandardDeviation", "Table.RowCount", "Table.ApproximateRowCount", "Cube.ApplyMeasure" };

				// Token: 0x04006698 RID: 26264
				private static readonly HashSet<string> neutralFunctions = new HashSet<string> { "List.Distinct", "List.Select", "Table.Distinct", "Table.SelectColumns" };

				// Token: 0x0200202E RID: 8238
				private sealed class MutableFunctionExpressionSyntaxNode : NullRangeSyntaxNode, IFunctionExpression, IExpression, ISyntaxNode, IDeclarator
				{
					// Token: 0x0600C94F RID: 51535 RVA: 0x00284F75 File Offset: 0x00283175
					public MutableFunctionExpressionSyntaxNode(IFunctionTypeExpression functionType)
					{
						this.functionType = functionType;
					}

					// Token: 0x17003091 RID: 12433
					// (get) Token: 0x0600C950 RID: 51536 RVA: 0x00002E8B File Offset: 0x0000108B
					public TokenRange Range
					{
						get
						{
							return TokenRange.Null;
						}
					}

					// Token: 0x17003092 RID: 12434
					// (get) Token: 0x0600C951 RID: 51537 RVA: 0x00075E2C File Offset: 0x0007402C
					public ExpressionKind Kind
					{
						get
						{
							return ExpressionKind.Function;
						}
					}

					// Token: 0x17003093 RID: 12435
					// (get) Token: 0x0600C952 RID: 51538 RVA: 0x00284F84 File Offset: 0x00283184
					public IFunctionTypeExpression FunctionType
					{
						get
						{
							return this.functionType;
						}
					}

					// Token: 0x17003094 RID: 12436
					// (get) Token: 0x0600C953 RID: 51539 RVA: 0x00284F8C File Offset: 0x0028318C
					// (set) Token: 0x0600C954 RID: 51540 RVA: 0x00284F94 File Offset: 0x00283194
					public IExpression Expression
					{
						get
						{
							return this.expression;
						}
						set
						{
							this.expression = value;
						}
					}

					// Token: 0x17003095 RID: 12437
					// (get) Token: 0x0600C955 RID: 51541 RVA: 0x00284F9D File Offset: 0x0028319D
					public int Count
					{
						get
						{
							return this.functionType.Parameters.Count;
						}
					}

					// Token: 0x17003096 RID: 12438
					public Microsoft.Mashup.Engine.Interface.Identifier this[int index]
					{
						get
						{
							return this.functionType.Parameters[index].Identifier;
						}
					}

					// Token: 0x04006699 RID: 26265
					private readonly IFunctionTypeExpression functionType;

					// Token: 0x0400669A RID: 26266
					private IExpression expression;
				}

				// Token: 0x0200202F RID: 8239
				private struct FunctionWrapper : IDisposable
				{
					// Token: 0x0600C957 RID: 51543 RVA: 0x00284FC8 File Offset: 0x002831C8
					public FunctionWrapper(SqlExpressionTranslator.SqlToMTranslator.AggregateDetectionVisitor visitor, IExpression expression)
					{
						this.visitor = visitor;
						this.mutableFunction = this.visitor.mutableFunction;
						this.visitor.mutableFunction = null;
						if (this.mutableFunction == null)
						{
							this.mutableFunction = new SqlExpressionTranslator.SqlToMTranslator.AggregateDetectionVisitor.MutableFunctionExpressionSyntaxNode(this.visitor.functionType);
						}
						this.mutableFunction.Expression = expression;
					}

					// Token: 0x17003097 RID: 12439
					// (get) Token: 0x0600C958 RID: 51544 RVA: 0x00285023 File Offset: 0x00283223
					public IFunctionExpression Function
					{
						get
						{
							return this.mutableFunction;
						}
					}

					// Token: 0x0600C959 RID: 51545 RVA: 0x0028502B File Offset: 0x0028322B
					public void Dispose()
					{
						this.mutableFunction.Expression = null;
						if (this.visitor.mutableFunction == null)
						{
							this.visitor.mutableFunction = this.mutableFunction;
						}
					}

					// Token: 0x0400669B RID: 26267
					private readonly SqlExpressionTranslator.SqlToMTranslator.AggregateDetectionVisitor visitor;

					// Token: 0x0400669C RID: 26268
					private readonly SqlExpressionTranslator.SqlToMTranslator.AggregateDetectionVisitor.MutableFunctionExpressionSyntaxNode mutableFunction;
				}
			}

			// Token: 0x02002030 RID: 8240
			private enum DatePart
			{
				// Token: 0x0400669E RID: 26270
				Year,
				// Token: 0x0400669F RID: 26271
				Quarter,
				// Token: 0x040066A0 RID: 26272
				Month,
				// Token: 0x040066A1 RID: 26273
				Week,
				// Token: 0x040066A2 RID: 26274
				Dayofyear,
				// Token: 0x040066A3 RID: 26275
				Day,
				// Token: 0x040066A4 RID: 26276
				Weekday,
				// Token: 0x040066A5 RID: 26277
				Hour,
				// Token: 0x040066A6 RID: 26278
				Minute,
				// Token: 0x040066A7 RID: 26279
				Second,
				// Token: 0x040066A8 RID: 26280
				Millisecond,
				// Token: 0x040066A9 RID: 26281
				Microsecond,
				// Token: 0x040066AA RID: 26282
				Nanosecond,
				// Token: 0x040066AB RID: 26283
				TZoffset,
				// Token: 0x040066AC RID: 26284
				ISO_WEEK
			}
		}

		// Token: 0x02002035 RID: 8245
		private struct RowParameterAndType
		{
			// Token: 0x040066B6 RID: 26294
			public Microsoft.Mashup.Engine.Interface.Identifier RowParameter;

			// Token: 0x040066B7 RID: 26295
			public ITypeValue Type;
		}

		// Token: 0x02002036 RID: 8246
		public enum Status
		{
			// Token: 0x040066B9 RID: 26297
			Success,
			// Token: 0x040066BA RID: 26298
			UnsupportedSql,
			// Token: 0x040066BB RID: 26299
			Unrecognized
		}
	}
}
