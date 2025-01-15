using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.Common.Creators
{
	// Token: 0x0200118B RID: 4491
	internal abstract class DbAstExpressionChecker : AstExpressionChecker<DbAstExpressionChecker.SqlVariable>
	{
		// Token: 0x06007666 RID: 30310 RVA: 0x001980AF File Offset: 0x001962AF
		protected DbAstExpressionChecker(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, EnvironmentBase externalEnvironment)
			: base(expression, cursor, externalEnvironment)
		{
		}

		// Token: 0x170020AA RID: 8362
		// (get) Token: 0x06007667 RID: 30311 RVA: 0x001980D0 File Offset: 0x001962D0
		protected Dictionary<FunctionValue, Action<IInvocationExpression>> FunctionLookup
		{
			get
			{
				if (this.functionLookup == null)
				{
					this.functionLookup = this.GetFunctions();
				}
				return this.functionLookup;
			}
		}

		// Token: 0x170020AB RID: 8363
		// (get) Token: 0x06007668 RID: 30312 RVA: 0x001980EC File Offset: 0x001962EC
		protected Dictionary<FunctionValue, Action<IInvocationExpression>> StatementFunctionLookup
		{
			get
			{
				if (this.statementFunctionLookup == null)
				{
					this.statementFunctionLookup = this.GetStatementFunctions();
				}
				return this.statementFunctionLookup;
			}
		}

		// Token: 0x170020AC RID: 8364
		// (get) Token: 0x06007669 RID: 30313 RVA: 0x00178ECC File Offset: 0x001770CC
		public virtual int MaxCharacterStringLiteralLength
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x170020AD RID: 8365
		// (get) Token: 0x0600766A RID: 30314 RVA: 0x00002105 File Offset: 0x00000305
		protected virtual bool CanCastNumericPrecision
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600766B RID: 30315 RVA: 0x00198108 File Offset: 0x00196308
		protected virtual Dictionary<FunctionValue, Action<IInvocationExpression>> GetFunctions()
		{
			return new Dictionary<FunctionValue, Action<IInvocationExpression>>
			{
				{
					TableModule.Table.Join,
					new Action<IInvocationExpression>(this.CheckTableJoin)
				},
				{
					Library.Binary.FromText,
					new Action<IInvocationExpression>(this.CheckBinaryFromText)
				},
				{
					Library.Date.AddMonths,
					this.CheckArgumentTypesForDateFunctions("Date.AddMonths", TypeValue.Number)
				},
				{
					Library.Date.AddYears,
					this.CheckArgumentTypesForDateFunctions("Date.AddYears", TypeValue.Number)
				},
				{
					Library.Date.StartOfDay,
					new Action<IInvocationExpression>(this.CheckArgumentForDateStartOfDay)
				},
				{
					Library.Duration.TotalDays,
					new Action<IInvocationExpression>(this.CheckArgumentForDurationTotalDays)
				},
				{
					Library.List.Average,
					new Action<IInvocationExpression>(this.CheckListAverage)
				},
				{
					Library.List.Combine,
					new Action<IInvocationExpression>(this.CheckListCombine)
				},
				{
					Library.List.Contains,
					new Action<IInvocationExpression>(this.CheckListContains)
				},
				{
					Library.List.First,
					new Action<IInvocationExpression>(this.CheckListFirst)
				},
				{
					Library.List.Max,
					new Action<IInvocationExpression>(this.CheckMathAggregateFunctionOneArgument)
				},
				{
					Library.List.Min,
					new Action<IInvocationExpression>(this.CheckMathAggregateFunctionOneArgument)
				},
				{
					Library.List.StandardDeviation,
					new Action<IInvocationExpression>(this.CheckMathAggregateFunctionOneArgument)
				},
				{
					Library.List.Sum,
					new Action<IInvocationExpression>(this.CheckListSum)
				},
				{
					LanguageLibrary.List.Count,
					new Action<IInvocationExpression>(this.CheckMathAggregateFunctionOneArgument)
				},
				{
					Library.List.CountOfNull,
					new Action<IInvocationExpression>(this.CheckMathAggregateFunctionOneArgument)
				},
				{
					Library.List.CountOfNotNull,
					new Action<IInvocationExpression>(this.CheckMathAggregateFunctionOneArgument)
				},
				{
					Library.List.CountOfDistinct,
					new Action<IInvocationExpression>(this.CheckMathAggregateFunctionOneArgument)
				},
				{
					Library.List.CountOfDistinctNull,
					new Action<IInvocationExpression>(this.CheckMathAggregateFunctionOneArgument)
				},
				{
					Library.List.CountOfDistinctNotNull,
					new Action<IInvocationExpression>(this.CheckMathAggregateFunctionOneArgument)
				},
				{
					TableModule.Table.Group,
					new Action<IInvocationExpression>(this.CheckListAggregateGroupBy)
				},
				{
					TableModule.Table.RowCount,
					new Action<IInvocationExpression>(this.CheckTableRowCount)
				},
				{
					TableModule.Table.Distinct,
					new Action<IInvocationExpression>(this.CheckListDistinct)
				},
				{
					TableModule.Table.SelectRows,
					new Action<IInvocationExpression>(base.CheckListSelectInvocation)
				},
				{
					TableModule.Table.Sort,
					new Action<IInvocationExpression>(base.CheckListSortInvocation)
				},
				{
					Library.ListRuntime.Transform,
					new Action<IInvocationExpression>(this.CheckListTransform)
				},
				{
					TableModule.Table.First,
					new Action<IInvocationExpression>(this.CheckTableFirst)
				},
				{
					Library.Number.Abs,
					this.CheckArgumentTypes("Number.Abs", new TypeValue[] { NullableTypeValue.Number })
				},
				{
					Library.Number.Acos,
					this.CheckArgumentTypes("Number.Acos", new TypeValue[] { NullableTypeValue.Number })
				},
				{
					Library.Number.Asin,
					this.CheckArgumentTypes("Number.Asin", new TypeValue[] { NullableTypeValue.Number })
				},
				{
					Library.Number.Atan,
					this.CheckArgumentTypes("Number.Atan", new TypeValue[] { NullableTypeValue.Number })
				},
				{
					Library.Number.Atan2,
					this.CheckArgumentTypes("Number.Atan2", new TypeValue[]
					{
						NullableTypeValue.Number,
						NullableTypeValue.Number
					})
				},
				{
					Library.Number.Cos,
					this.CheckArgumentTypes("Number.Cos", new TypeValue[] { NullableTypeValue.Number })
				},
				{
					Library.Number.Exp,
					this.CheckArgumentTypes("Number.Exp", new TypeValue[] { NullableTypeValue.Number })
				},
				{
					Library.Number.Log,
					this.CheckArgumentTypes("Number.Log", 1, new TypeValue[]
					{
						NullableTypeValue.Number,
						NullableTypeValue.Number
					})
				},
				{
					Library.Number.Log10,
					this.CheckArgumentTypes("Number.Log10", new TypeValue[] { NullableTypeValue.Number })
				},
				{
					Library.Number.Mod,
					new Action<IInvocationExpression>(this.CheckArgumentsHavingOptionalPrecision)
				},
				{
					Library.Number.Power,
					this.CheckArgumentTypes("Number.Power", new TypeValue[]
					{
						NullableTypeValue.Number,
						NullableTypeValue.Number
					})
				},
				{
					Library.Number.Sign,
					this.CheckArgumentTypes("Number.Sign", new TypeValue[] { NullableTypeValue.Number })
				},
				{
					Library.Number.Sin,
					this.CheckArgumentTypes("Number.Sin", new TypeValue[] { NullableTypeValue.Number })
				},
				{
					Library.Number.Sqrt,
					this.CheckArgumentTypes("Number.Sqrt", new TypeValue[] { NullableTypeValue.Number })
				},
				{
					Library.Number.Tan,
					this.CheckArgumentTypes("Number.Tan", new TypeValue[] { NullableTypeValue.Number })
				},
				{
					Library.Number.Round,
					new Action<IInvocationExpression>(this.CheckArgumentsForNumberRound)
				},
				{
					Library.Text.Contains,
					this.CheckTextFunctionTwoArguments("Text.Contains")
				},
				{
					Library.Text.EndsWith,
					this.CheckTextFunctionTwoArguments("Text.EndsWith")
				},
				{
					Library.Text.Length,
					new Action<IInvocationExpression>(this.CheckTextLengthFunction)
				},
				{
					Library.Text.StartsWith,
					this.CheckTextFunctionTwoArguments("Text.StartsWith")
				},
				{
					Library.Text.Trim,
					new Action<IInvocationExpression>(this.CheckTextTrimFunction)
				},
				{
					Library.Text.TrimEnd,
					new Action<IInvocationExpression>(this.CheckTextTrimEndFunction)
				},
				{
					Library.Text.TrimStart,
					new Action<IInvocationExpression>(this.CheckTextTrimStartFunction)
				},
				{
					Library._Value.As,
					new Action<IInvocationExpression>(this.CheckAsFunctionArgument)
				},
				{
					Library._Value.Equals,
					new Action<IInvocationExpression>(this.CheckArgumentsHavingOptionalPrecision)
				},
				{
					Library._Value.NativeQuery,
					new Action<IInvocationExpression>(this.CheckNativeQueryInvocation)
				},
				{
					Library._Value.NullableEquals,
					new Action<IInvocationExpression>(this.CheckArgumentsHavingOptionalPrecision)
				},
				{
					Library._Value.Add,
					new Action<IInvocationExpression>(this.CheckArgumentsHavingOptionalPrecision)
				},
				{
					Library._Value.Subtract,
					new Action<IInvocationExpression>(this.CheckArgumentsHavingOptionalPrecision)
				},
				{
					Library._Value.Multiply,
					new Action<IInvocationExpression>(this.CheckArgumentsHavingOptionalPrecision)
				},
				{
					Library._Value.Divide,
					new Action<IInvocationExpression>(this.CheckArgumentsHavingOptionalPrecision)
				},
				{
					CultureSpecificFunction.NumberFrom,
					new Action<IInvocationExpression>(this.CheckArgumentForNumberFrom)
				},
				{
					CultureSpecificFunction.NumberToText,
					new Action<IInvocationExpression>(this.CheckArgumentsAreValid)
				},
				{
					CultureSpecificFunction.SingleFrom,
					new Action<IInvocationExpression>(this.CheckSingleFromHasFoldableArguments)
				},
				{
					CultureSpecificFunction.DoubleFrom,
					new Action<IInvocationExpression>(this.CheckDoubleFromHasFoldableArguments)
				},
				{
					CultureSpecificFunction.DecimalFrom,
					new Action<IInvocationExpression>(this.CheckDecimalFromHasFoldableArguments)
				},
				{
					CultureSpecificFunction.TextFrom,
					new Action<IInvocationExpression>(this.CheckArgumentForTextFrom)
				},
				{
					CultureSpecificFunction.DateFrom,
					new Action<IInvocationExpression>(this.CheckArgumentForDateFromTypes)
				},
				{
					CultureSpecificFunction.DateTimeFrom,
					new Action<IInvocationExpression>(this.CheckArgumentForDateTimeFromTypes)
				},
				{
					CultureSpecificFunction.DateTimeZoneFrom,
					new Action<IInvocationExpression>(this.CheckArgumentForDateTimeZoneFromTypes)
				},
				{
					CultureSpecificFunction.TextLower,
					new Action<IInvocationExpression>(this.CheckTextLowerFunction)
				},
				{
					CultureSpecificFunction.TextUpper,
					new Action<IInvocationExpression>(this.CheckTextUpperFunction)
				},
				{
					Library.Duration.From,
					new Action<IInvocationExpression>(this.CheckArgumentForDurationFrom)
				}
			};
		}

		// Token: 0x0600766C RID: 30316 RVA: 0x0019884A File Offset: 0x00196A4A
		protected virtual Dictionary<FunctionValue, Action<IInvocationExpression>> GetStatementFunctions()
		{
			return new Dictionary<FunctionValue, Action<IInvocationExpression>>();
		}

		// Token: 0x0600766D RID: 30317 RVA: 0x00198851 File Offset: 0x00196A51
		private bool AreSameSqlScalarTypesOrNull(TypeValue a, TypeValue b)
		{
			if (a.TypeKind == ValueKind.Null)
			{
				return TypeServices.IsScalar(b);
			}
			if (b.TypeKind == ValueKind.Null)
			{
				return TypeServices.IsScalar(a);
			}
			return this.AreSameSqlScalarTypes(a, b);
		}

		// Token: 0x0600766E RID: 30318 RVA: 0x00198879 File Offset: 0x00196A79
		private bool AreSameSqlScalarTypes(TypeValue a, TypeValue b)
		{
			return a.TypeKind == b.TypeKind && TypeServices.IsScalar(a);
		}

		// Token: 0x0600766F RID: 30319 RVA: 0x00198891 File Offset: 0x00196A91
		protected Action<IInvocationExpression> CheckArgumentTypesForDateFunctions(string functionName, TypeValue secondParameterType)
		{
			return delegate(IInvocationExpression invocation)
			{
				using (this.FoldingTracingService.NewScope("SqlAstExpressionChecker.CheckArgumentTypesForDateFunctions"))
				{
					if (invocation.Arguments.Count != 2)
					{
						throw this.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount(functionName, 2));
					}
					IExpression expression = invocation.Arguments[0];
					ValueKind typeKind = this.GetType(expression).TypeKind;
					if (typeKind - ValueKind.Date > 2)
					{
						throw this.FoldingTracingService.NewFoldingFailureException(null);
					}
					if (!this.GetType(invocation.Arguments[1]).IsCompatibleWith(secondParameterType))
					{
						throw this.FoldingTracingService.NewFoldingFailureException(null);
					}
					this.CheckArgumentsAreValid(invocation);
				}
			};
		}

		// Token: 0x06007670 RID: 30320 RVA: 0x001988B8 File Offset: 0x00196AB8
		protected void CheckArgumentForDurationTotalDays(IInvocationExpression expression)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckArgumentForDurationTotalDays"))
			{
				if (expression.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("Duration.TotalDays", 1));
				}
				this.InternalCheckArgumentForDurationFromTypes(expression);
			}
		}

		// Token: 0x06007671 RID: 30321 RVA: 0x00198920 File Offset: 0x00196B20
		protected void InternalCheckArgumentForDurationFromTypes(IInvocationExpression expression)
		{
			IBinaryExpression binaryExpression = expression.Arguments[0] as IBinaryExpression;
			if (binaryExpression == null || binaryExpression.Operator != BinaryOperator2.Subtract)
			{
				throw base.FoldingTracingService.NewFoldingFailureException(null);
			}
			base.Visit(binaryExpression.Left);
			base.Visit(binaryExpression.Right);
			TypeValue type = base.GetType(binaryExpression.Left);
			TypeValue type2 = base.GetType(binaryExpression.Right);
			if (type.TypeKind != type2.TypeKind || !this.IsDateOrDateTimeCompatibleType(type))
			{
				throw base.FoldingTracingService.NewFoldingFailureException(null);
			}
		}

		// Token: 0x06007672 RID: 30322 RVA: 0x001989B0 File Offset: 0x00196BB0
		protected virtual void CheckArgumentForDateStartOfDay(IInvocationExpression expression)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckArgumentForDateStartOfDay"))
			{
				if (expression.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("Date.StartOfDay", 1));
				}
				this.CheckArgumentsAreValid(expression);
				TypeValue type = base.GetType(expression.Arguments[0]);
				if (!this.IsDateOrDateTimeCompatibleType(type))
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x06007673 RID: 30323 RVA: 0x00198A40 File Offset: 0x00196C40
		private bool AreCompatibleSqlScalarTypesOrNull(TypeValue targetType, TypeValue sourceType)
		{
			if (targetType.TypeKind == ValueKind.Null)
			{
				return TypeServices.IsScalar(sourceType);
			}
			if (sourceType.TypeKind == ValueKind.Null)
			{
				return TypeServices.IsScalar(targetType);
			}
			return this.AreCompatibleSqlScalarTypes(targetType, sourceType);
		}

		// Token: 0x06007674 RID: 30324 RVA: 0x00198A68 File Offset: 0x00196C68
		private bool AreCompatibleSqlScalarTypes(TypeValue targetType, TypeValue sourceType)
		{
			return TypeServices.IsScalar(targetType) && TypeServices.IsScalar(sourceType) && this.AreScalarTypesCompatible(targetType, sourceType);
		}

		// Token: 0x06007675 RID: 30325 RVA: 0x00198A84 File Offset: 0x00196C84
		protected virtual bool AreScalarTypesCompatible(TypeValue targetType, TypeValue sourceType)
		{
			return targetType.TypeKind == sourceType.TypeKind;
		}

		// Token: 0x170020AE RID: 8366
		// (get) Token: 0x06007676 RID: 30326 RVA: 0x00198A94 File Offset: 0x00196C94
		protected new DbEnvironment ExternalEnvironment
		{
			get
			{
				return (DbEnvironment)base.ExternalEnvironment;
			}
		}

		// Token: 0x06007677 RID: 30327 RVA: 0x00198AA4 File Offset: 0x00196CA4
		protected override void CheckInternal()
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckInternal"))
			{
				base.CheckInternal();
				using (IEnumerator<DbAstExpressionChecker.SqlVariable> enumerator = this.variableGraph.CyclicItems.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						DbAstExpressionChecker.SqlVariable sqlVariable = enumerator.Current;
						throw base.FoldingTracingService.NewFoldingFailureException(null);
					}
				}
			}
		}

		// Token: 0x06007678 RID: 30328 RVA: 0x00198B2C File Offset: 0x00196D2C
		protected override void CheckStatementInternal()
		{
			this.CheckStatement(base.RootExpression);
		}

		// Token: 0x06007679 RID: 30329 RVA: 0x00198B3C File Offset: 0x00196D3C
		protected void CheckStatement(IExpression expression)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckStatement"))
			{
				IInvocationExpression invocationExpression = expression as IInvocationExpression;
				if (invocationExpression != null)
				{
					IConstantExpression constantExpression = invocationExpression.Function as IConstantExpression;
					Action<IInvocationExpression> action;
					if (constantExpression != null && constantExpression.Value.IsFunction && this.StatementFunctionLookup.TryGetValue(constantExpression.Value.AsFunction, out action))
					{
						action(invocationExpression);
						using (IEnumerator<DbAstExpressionChecker.SqlVariable> enumerator = this.variableGraph.CyclicItems.GetEnumerator())
						{
							if (!enumerator.MoveNext())
							{
								return;
							}
							DbAstExpressionChecker.SqlVariable sqlVariable = enumerator.Current;
							throw base.FoldingTracingService.NewFoldingFailureException(null);
						}
					}
				}
				throw base.FoldingTracingService.NewFoldingFailureException(null);
			}
		}

		// Token: 0x0600767A RID: 30330 RVA: 0x00198C14 File Offset: 0x00196E14
		protected void CheckArgumentsForRoundUpAndRoundDown(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckArgumentsForRoundUpAndRoundDown"))
			{
				if (invocation.Arguments.Count >= 2)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				this.CheckArgumentsAreValid(invocation);
			}
		}

		// Token: 0x0600767B RID: 30331 RVA: 0x00198C70 File Offset: 0x00196E70
		protected virtual void CheckArgumentsForNumberRound(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckArgumentsForNumberRound"))
			{
				if (invocation.Arguments.Count > 2)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int, int>>(FoldingWarnings.InvalidArgumentsCount("Number.Round", 1, 2));
				}
				this.CheckArgumentsAreValid(invocation);
			}
		}

		// Token: 0x0600767C RID: 30332 RVA: 0x00198CD8 File Offset: 0x00196ED8
		protected virtual void CheckArgumentForNumberFrom(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckArgumentForNumberFrom"))
			{
				if (invocation.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("Number.From", 1));
				}
				ValueKind typeKind = base.GetType(invocation.Arguments[0]).TypeKind;
				if (typeKind - ValueKind.Date > 1 && typeKind != ValueKind.Number)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				this.CheckArgumentsAreValid(invocation);
			}
		}

		// Token: 0x0600767D RID: 30333 RVA: 0x00198D70 File Offset: 0x00196F70
		protected virtual void CheckArgumentForDurationFrom(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckArgumentForDurationFrom"))
			{
				if (invocation.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("Duration.From", 1));
				}
				if (base.GetType(invocation.Arguments[0]).TypeKind != ValueKind.Number)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x0600767E RID: 30334 RVA: 0x00198DF8 File Offset: 0x00196FF8
		protected virtual void CheckArgumentForTextFrom(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckArgumentForTextFrom"))
			{
				if (invocation.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("Text.From", 1));
				}
				this.CheckArgumentsAreValid(invocation);
				TypeValue type = base.GetType(invocation.Arguments[0]);
				if (!DbTypeServices.IsComparable(type) || type.TypeKind == ValueKind.Binary)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x0600767F RID: 30335 RVA: 0x00198E90 File Offset: 0x00197090
		protected virtual void CheckArgumentForDateFromTypes(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckArgumentForDateFromTypes"))
			{
				if (invocation.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("Date.From", 1));
				}
				if (base.GetType(invocation.Arguments[0]).TypeKind != ValueKind.Number)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				this.CheckArgumentsAreValid(invocation);
			}
		}

		// Token: 0x06007680 RID: 30336 RVA: 0x00198F20 File Offset: 0x00197120
		protected virtual void CheckArgumentForDateTimeFromTypes(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckArgumentForDateTimeFromTypes"))
			{
				if (invocation.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("DateTime.From", 1));
				}
				ValueKind typeKind = base.GetType(invocation.Arguments[0]).TypeKind;
				if (typeKind != ValueKind.DateTime && typeKind != ValueKind.Number)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				this.CheckArgumentsAreValid(invocation);
			}
		}

		// Token: 0x06007681 RID: 30337 RVA: 0x00198FB4 File Offset: 0x001971B4
		protected virtual void CheckArgumentForDateTimeZoneFromTypes(IInvocationExpression invocation)
		{
			IDisposable disposable = base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckArgumentForDateTimeZoneFromTypes");
			try
			{
				throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string>>(FoldingWarnings.FunctionNotImplemented("DateTimeZone.From"));
			}
			finally
			{
				if (disposable != null)
				{
					disposable.Dispose();
					goto IL_0030;
				}
				goto IL_0030;
				IL_0030:;
			}
		}

		// Token: 0x06007682 RID: 30338 RVA: 0x00199010 File Offset: 0x00197210
		protected virtual void CheckSingleFromHasFoldableArguments(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckSingleFromHasFoldableArguments"))
			{
				if (invocation.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("Single.From", 1));
				}
				this.CheckNonIntTypeArguments(invocation);
			}
		}

		// Token: 0x06007683 RID: 30339 RVA: 0x00199078 File Offset: 0x00197278
		protected virtual void CheckDoubleFromHasFoldableArguments(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckDoubleFromHasFoldableArguments"))
			{
				if (invocation.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("Double.From", 1));
				}
				this.CheckNonIntTypeArguments(invocation);
			}
		}

		// Token: 0x06007684 RID: 30340 RVA: 0x001990E0 File Offset: 0x001972E0
		protected virtual void CheckDecimalFromHasFoldableArguments(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckDecimalFromHasFoldableArguments"))
			{
				if (invocation.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("Decimal.From", 1));
				}
				this.CheckNonIntTypeArguments(invocation);
			}
		}

		// Token: 0x06007685 RID: 30341 RVA: 0x00199148 File Offset: 0x00197348
		protected virtual void CheckNonIntTypeArguments(IInvocationExpression invocation)
		{
			ValueKind typeKind = base.GetType(invocation.Arguments[0]).TypeKind;
			if (typeKind - ValueKind.Date > 1)
			{
				throw base.FoldingTracingService.NewFoldingFailureException(null);
			}
			this.CheckArgumentsAreValid(invocation);
		}

		// Token: 0x06007686 RID: 30342 RVA: 0x00199188 File Offset: 0x00197388
		protected void CheckArgumentsAreValid(IInvocationExpression invocation)
		{
			foreach (IExpression expression in invocation.Arguments)
			{
				base.Visit(expression);
			}
		}

		// Token: 0x06007687 RID: 30343 RVA: 0x001991D8 File Offset: 0x001973D8
		protected void CheckArgumentsHavingOptionalPrecision(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckArgumentsHavingOptionalPrecision"))
			{
				if (invocation.Arguments.Count != 2 && invocation.Arguments.Count != 3)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				this.VisitExpression(invocation.Arguments[0]);
				this.VisitExpression(invocation.Arguments[1]);
				if (invocation.Arguments.Count == 3)
				{
					this.CheckPrecisionExpression(invocation.Arguments[2]);
				}
			}
		}

		// Token: 0x06007688 RID: 30344 RVA: 0x00199284 File Offset: 0x00197484
		protected void CheckBinaryFromText(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckBinaryFromText"))
			{
				if (invocation.Arguments[1].Kind != ExpressionKind.Constant)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				if (!((IConstantExpression)invocation.Arguments[1]).Value.Equals(Library.BinaryEncoding.Hex))
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x06007689 RID: 30345 RVA: 0x00199310 File Offset: 0x00197510
		protected void CheckTextReplace(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckTextReplace"))
			{
				this.CheckArgumentCount(3);
				TypeValue type = base.GetType(invocation.Arguments[0]);
				TypeValue type2 = base.GetType(invocation.Arguments[1]);
				TypeValue type3 = base.GetType(invocation.Arguments[2]);
				if (!type.IsCompatibleWith(TypeValue.Text.Nullable) || !type2.IsCompatibleWith(TypeValue.Text.NonNullable) || !type3.IsCompatibleWith(TypeValue.Text.NonNullable))
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x0600768A RID: 30346 RVA: 0x001993CC File Offset: 0x001975CC
		protected Action<IInvocationExpression> CheckArgumentTypes(string functionName, params TypeValue[] argTypes)
		{
			return this.CheckArgumentTypes(functionName, argTypes.Length, argTypes);
		}

		// Token: 0x0600768B RID: 30347 RVA: 0x001993D9 File Offset: 0x001975D9
		protected Action<IInvocationExpression> CheckArgumentTypes(string functionName, int minArgCount, params TypeValue[] argTypes)
		{
			return delegate(IInvocationExpression invocation)
			{
				using (this.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckArgumentTypes"))
				{
					if (invocation.Arguments.Count < minArgCount || invocation.Arguments.Count > argTypes.Length)
					{
						if (minArgCount == argTypes.Length)
						{
							throw this.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount(functionName, minArgCount));
						}
						throw this.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int, int>>(FoldingWarnings.InvalidArgumentsCount(functionName, minArgCount, argTypes.Length));
					}
					else
					{
						for (int i = 0; i < invocation.Arguments.Count; i++)
						{
							if (!this.GetType(invocation.Arguments[i]).IsCompatibleWith(argTypes[i]))
							{
								throw this.FoldingTracingService.NewFoldingFailureException(null);
							}
						}
						this.CheckArgumentsAreValid(invocation);
					}
				}
			};
		}

		// Token: 0x0600768C RID: 30348 RVA: 0x00199408 File Offset: 0x00197608
		private void CheckCompatibleOperands(IBinaryExpression binary)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckCompatibleOperands"))
			{
				TypeValue type = base.GetType(binary.Left);
				TypeValue type2 = base.GetType(binary.Right);
				if (!DbTypeServices.IsCompatibleType(type, type2, binary.Operator))
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x0600768D RID: 30349 RVA: 0x00199478 File Offset: 0x00197678
		private void CheckElementTypeIsNotCollection(TypeValue elementType)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckElementTypeIsNotCollection"))
			{
				if (elementType.TypeKind == ValueKind.List)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x0600768E RID: 30350 RVA: 0x001994CC File Offset: 0x001976CC
		private void CheckGeneralTypeErrors(TypeValue type, IExpression errorTerm)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckGeneralTypeErrors"))
			{
				if (!TypeServices.IsScalar(type))
				{
					ValueKind typeKind = type.TypeKind;
					if (typeKind != ValueKind.Record)
					{
						if (typeKind == ValueKind.Table)
						{
							RecordTypeValue itemType = type.AsTableType.ItemType;
							this.CheckElementTypeIsNotCollection(itemType);
							this.CheckGeneralTypeErrors(itemType, errorTerm);
							return;
						}
					}
					else
					{
						RecordTypeValue asRecordType = type.AsRecordType;
						this.CheckRecordTypeHasAtLeastOneField(asRecordType, errorTerm);
						using (Keys.StringKeysEnumerator enumerator = asRecordType.Fields.Keys.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								string text = enumerator.Current;
								this.CheckGeneralTypeErrors(RecordTypeAlgebra.Field(asRecordType, text), errorTerm);
							}
							return;
						}
					}
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x0600768F RID: 30351 RVA: 0x001995AC File Offset: 0x001977AC
		private bool CheckGroupByColumnAccess(IExpression list)
		{
			IFieldAccessExpression fieldAccessExpression = EnvironmentAstVisitor<DbAstExpressionChecker.SqlVariable>.Reduce(list) as IFieldAccessExpression;
			if (fieldAccessExpression != null)
			{
				IIdentifierExpression identifierExpression = EnvironmentAstVisitor<DbAstExpressionChecker.SqlVariable>.Reduce(fieldAccessExpression.Expression) as IIdentifierExpression;
				DbAstExpressionChecker.SqlVariable sqlVariable;
				if (identifierExpression != null && base.Environment.TryGetValue(identifierExpression.Name, identifierExpression.IsInclusive, out sqlVariable) && sqlVariable.Kind == DbAstExpressionChecker.SqlVariableKind.Group)
				{
					base.ValidateExpressionTypeIsValid(fieldAccessExpression);
					return true;
				}
			}
			else
			{
				IIdentifierExpression identifierExpression2 = EnvironmentAstVisitor<DbAstExpressionChecker.SqlVariable>.Reduce(list) as IIdentifierExpression;
				DbAstExpressionChecker.SqlVariable sqlVariable2;
				if (identifierExpression2 != null && base.Environment.TryGetValue(identifierExpression2.Name, identifierExpression2.IsInclusive, out sqlVariable2) && sqlVariable2.Kind == DbAstExpressionChecker.SqlVariableKind.Group)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06007690 RID: 30352 RVA: 0x00199640 File Offset: 0x00197840
		private bool CheckGroupByKeysAccess(IExpression list)
		{
			bool flag;
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckGroupByKeysAccess"))
			{
				IFieldAccessExpression fieldAccessExpression = EnvironmentAstVisitor<DbAstExpressionChecker.SqlVariable>.Reduce(list) as IFieldAccessExpression;
				if (fieldAccessExpression != null)
				{
					IIdentifierExpression identifierExpression = EnvironmentAstVisitor<DbAstExpressionChecker.SqlVariable>.Reduce(fieldAccessExpression.Expression) as IIdentifierExpression;
					DbAstExpressionChecker.SqlVariable sqlVariable;
					if (identifierExpression != null && base.Environment.TryGetValue(identifierExpression.Name, identifierExpression.IsInclusive, out sqlVariable) && sqlVariable.Kind == DbAstExpressionChecker.SqlVariableKind.Group)
					{
						if (base.ValidateExpressionTypeIsValid(fieldAccessExpression) && !sqlVariable.GroupKeys.Contains(fieldAccessExpression.MemberName))
						{
							throw base.FoldingTracingService.NewFoldingFailureException(null);
						}
						return true;
					}
				}
				else
				{
					IMultiFieldRecordProjectionExpression multiFieldRecordProjectionExpression = EnvironmentAstVisitor<DbAstExpressionChecker.SqlVariable>.Reduce(list) as IMultiFieldRecordProjectionExpression;
					if (multiFieldRecordProjectionExpression != null)
					{
						IIdentifierExpression identifierExpression2 = EnvironmentAstVisitor<DbAstExpressionChecker.SqlVariable>.Reduce(multiFieldRecordProjectionExpression.Expression) as IIdentifierExpression;
						DbAstExpressionChecker.SqlVariable sqlVariable2;
						if (identifierExpression2 != null && base.Environment.TryGetValue(identifierExpression2.Name, identifierExpression2.IsInclusive, out sqlVariable2) && sqlVariable2.Kind == DbAstExpressionChecker.SqlVariableKind.Group)
						{
							if (base.ValidateExpressionTypeIsValid(multiFieldRecordProjectionExpression))
							{
								foreach (Identifier identifier in multiFieldRecordProjectionExpression.MemberNames)
								{
									if (!sqlVariable2.GroupKeys.Contains(identifier))
									{
										throw base.FoldingTracingService.NewFoldingFailureException(null);
									}
								}
							}
							return true;
						}
					}
					else
					{
						IIdentifierExpression identifierExpression3 = EnvironmentAstVisitor<DbAstExpressionChecker.SqlVariable>.Reduce(list) as IIdentifierExpression;
						DbAstExpressionChecker.SqlVariable sqlVariable3;
						if (identifierExpression3 != null && base.Environment.TryGetValue(identifierExpression3.Name, identifierExpression3.IsInclusive, out sqlVariable3) && sqlVariable3.Kind == DbAstExpressionChecker.SqlVariableKind.Group)
						{
							return true;
						}
					}
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x06007691 RID: 30353 RVA: 0x00199818 File Offset: 0x00197A18
		protected void CheckListAggregateGroupBy(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckListAggregateGroupBy"))
			{
				using (base.Context.Enter(ContextLabel.Query, base.FoldingTracingService))
				{
					IExpression expression = invocation.Arguments[0];
					this.VisitExpression(expression);
					IFunctionExpression functionExpression = (IFunctionExpression)invocation.Arguments[1];
					IExpression expression2 = EnvironmentAstVisitor<DbAstExpressionChecker.SqlVariable>.Reduce(functionExpression.Expression);
					IList<Identifier> list;
					if (expression2.Kind == ExpressionKind.MultiFieldRecordProjection)
					{
						IMultiFieldRecordProjectionExpression multiFieldRecordProjectionExpression = (IMultiFieldRecordProjectionExpression)expression2;
						IIdentifierExpression identifierExpression = EnvironmentAstVisitor<DbAstExpressionChecker.SqlVariable>.Reduce(multiFieldRecordProjectionExpression.Expression) as IIdentifierExpression;
						if (identifierExpression == null || identifierExpression.Name != functionExpression.FunctionType.Parameters[0].Identifier)
						{
							throw base.FoldingTracingService.NewFoldingFailureException(null);
						}
						list = multiFieldRecordProjectionExpression.MemberNames;
					}
					else
					{
						if (expression2.Kind != ExpressionKind.FieldAccess)
						{
							throw base.FoldingTracingService.NewFoldingFailureException(null);
						}
						IFieldAccessExpression fieldAccessExpression = (IFieldAccessExpression)expression2;
						IIdentifierExpression identifierExpression2 = EnvironmentAstVisitor<DbAstExpressionChecker.SqlVariable>.Reduce(fieldAccessExpression.Expression) as IIdentifierExpression;
						if (identifierExpression2 == null || identifierExpression2.Name != functionExpression.FunctionType.Parameters[0].Identifier)
						{
							throw base.FoldingTracingService.NewFoldingFailureException(null);
						}
						list = new List<Identifier>(1) { fieldAccessExpression.MemberName };
					}
					IExpression expression3 = EnvironmentAstVisitor<DbAstExpressionChecker.SqlVariable>.Reduce(((IFunctionExpression)invocation.Arguments[2]).Expression);
					if (expression3.Kind == ExpressionKind.Record)
					{
						foreach (VariableInitializer variableInitializer in ((IRecordExpression)expression3).Members)
						{
							this.CheckListAggregateGroupBySelector(variableInitializer.Value, list);
						}
					}
					base.VisitListFunctionArgumentAsLambda(invocation, 2, new Func<IExpression, IExpression>(this.VisitExpression), new DbAstExpressionChecker.SqlVariable[] { DbAstExpressionChecker.SqlVariable.CreateGroup(list) });
				}
			}
		}

		// Token: 0x06007692 RID: 30354 RVA: 0x00199A54 File Offset: 0x00197C54
		protected Action<IInvocationExpression> CheckArgumentCount(int argCount)
		{
			return this.CheckArgumentCount(argCount, argCount);
		}

		// Token: 0x06007693 RID: 30355 RVA: 0x00199A5E File Offset: 0x00197C5E
		protected Action<IInvocationExpression> CheckArgumentCount(int minArgCount, int maxArgCount)
		{
			return delegate(IInvocationExpression invocation)
			{
				using (this.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckArgumentCount"))
				{
					if (invocation.Arguments.Count < minArgCount || invocation.Arguments.Count > maxArgCount)
					{
						throw this.FoldingTracingService.NewFoldingFailureException(null);
					}
					this.CheckArgumentsAreValid(invocation);
				}
			};
		}

		// Token: 0x06007694 RID: 30356 RVA: 0x00199A88 File Offset: 0x00197C88
		protected void CheckListAggregateGroupBySelector(IExpression fieldValue, IList<Identifier> groupKeys)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckListAggregateGroupBySelector"))
			{
				TypeValue type = base.GetType(fieldValue);
				if (!this.IsValidSelectorType(type))
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				ExpressionKind kind = fieldValue.Kind;
				switch (kind)
				{
				case ExpressionKind.Binary:
				{
					IBinaryExpression binaryExpression = fieldValue as IBinaryExpression;
					if (binaryExpression.Operator == BinaryOperator2.Add || binaryExpression.Operator == BinaryOperator2.Subtract || binaryExpression.Operator == BinaryOperator2.Multiply || binaryExpression.Operator == BinaryOperator2.Divide)
					{
						this.CheckListAggregateGroupBySelector(binaryExpression.Left, groupKeys);
						this.CheckListAggregateGroupBySelector(binaryExpression.Right, groupKeys);
						return;
					}
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				case ExpressionKind.Constant:
					if (!TypeServices.IsScalar(type))
					{
						throw base.FoldingTracingService.NewFoldingFailureException(null);
					}
					goto IL_0207;
				case ExpressionKind.ElementAccess:
				case ExpressionKind.Exports:
					break;
				case ExpressionKind.FieldAccess:
				{
					IFieldAccessExpression fieldAccessExpression = (IFieldAccessExpression)fieldValue;
					if (!groupKeys.Contains(fieldAccessExpression.MemberName))
					{
						throw base.FoldingTracingService.NewFoldingFailureException(null);
					}
					if (fieldAccessExpression.Expression.Kind == ExpressionKind.Invocation)
					{
						IInvocationExpression invocationExpression = (IInvocationExpression)fieldAccessExpression.Expression;
						if (invocationExpression.Function.Kind == ExpressionKind.Constant && ((IConstantExpression)invocationExpression.Function).Value.Equals(TableModule.Table.First) && invocationExpression.Arguments[0].Simplify().Kind == ExpressionKind.Identifier)
						{
							return;
						}
					}
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				default:
					if (kind != ExpressionKind.Invocation)
					{
						if (kind == ExpressionKind.Unary)
						{
							IUnaryExpression unaryExpression = fieldValue as IUnaryExpression;
							if (unaryExpression.Operator == UnaryOperator2.Positive || unaryExpression.Operator == UnaryOperator2.Negative)
							{
								this.CheckListAggregateGroupBySelector(unaryExpression.Expression, groupKeys);
								return;
							}
							throw base.FoldingTracingService.NewFoldingFailureException(null);
						}
					}
					else
					{
						IInvocationExpression invocationExpression2 = (IInvocationExpression)fieldValue;
						if (invocationExpression2.Function.Kind != ExpressionKind.Constant)
						{
							throw base.FoldingTracingService.NewFoldingFailureException(null);
						}
						Value value = ((IConstantExpression)invocationExpression2.Function).Value;
						if (!DbAstExpressionChecker.supportedAggregateFunctions.Contains(value))
						{
							throw base.FoldingTracingService.NewFoldingFailureException(null);
						}
						goto IL_0207;
					}
					break;
				}
				throw base.FoldingTracingService.NewFoldingFailureException(null);
				IL_0207:;
			}
		}

		// Token: 0x06007695 RID: 30357 RVA: 0x00199CC4 File Offset: 0x00197EC4
		protected bool IsValidSelectorType(TypeValue type)
		{
			bool flag;
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.IsValidSelectorType"))
			{
				flag = DbTypeServices.IsComparable(type) && type.TypeKind != ValueKind.Binary;
			}
			return flag;
		}

		// Token: 0x06007696 RID: 30358 RVA: 0x00199D18 File Offset: 0x00197F18
		protected void CheckListCombine(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckListCombine"))
			{
				if (invocation.Arguments.Count == 1)
				{
					IListExpression listExpression = invocation.Arguments[0] as IListExpression;
					if (listExpression != null)
					{
						using (base.Context.Enter(ContextLabel.Combine, base.FoldingTracingService))
						{
							using (IEnumerator<IExpression> enumerator = listExpression.Members.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									IExpression expression = enumerator.Current;
									this.VisitExpression(expression);
									ValueKind typeKind = base.GetType(expression).TypeKind;
									if (typeKind != ValueKind.List && typeKind != ValueKind.Table)
									{
										throw base.FoldingTracingService.NewFoldingFailureException(null);
									}
								}
								return;
							}
						}
					}
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				throw base.FoldingTracingService.NewFoldingFailureException(null);
			}
		}

		// Token: 0x06007697 RID: 30359 RVA: 0x00199E24 File Offset: 0x00198024
		protected void CheckListContains(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckListContains"))
			{
				if (invocation.Arguments.Count < 2 || invocation.Arguments.Count > 3)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int, int>>(FoldingWarnings.InvalidArgumentsCount("List.Contains", 2, 3));
				}
				IExpression expression = invocation.Arguments[1];
				this.VisitExpression(expression);
				base.ValidateRequireComparableType(expression);
				Value value;
				if (!invocation.Arguments[0].TryGetConstant(out value))
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				if (!value.IsList)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				this.CheckListContainsArrayHasIdenticalLiterals(value.AsList);
			}
		}

		// Token: 0x06007698 RID: 30360 RVA: 0x00199EF4 File Offset: 0x001980F4
		private void CheckListContainsArrayHasIdenticalLiterals(ListValue members)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckListContainsArrayHasIdenticalLiterals"))
			{
				HashSet<ValueKind> hashSet = new HashSet<ValueKind>();
				foreach (IValueReference valueReference in members)
				{
					if (!TypeServices.IsScalar(valueReference.Value.Type))
					{
						throw base.FoldingTracingService.NewFoldingFailureException(null);
					}
					hashSet.Add(valueReference.Value.Kind);
				}
				if (hashSet.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x06007699 RID: 30361 RVA: 0x00199FAC File Offset: 0x001981AC
		protected void CheckTableRowCount(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckListCount"))
			{
				if (invocation.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("Table.RowCount", 1));
				}
				IExpression expression = invocation.Arguments[0];
				using (base.Context.Enter(ContextLabel.Query, base.FoldingTracingService))
				{
					if (!this.CheckGroupByColumnAccess(expression))
					{
						this.VisitExpression(expression);
					}
				}
			}
		}

		// Token: 0x0600769A RID: 30362 RVA: 0x0019A054 File Offset: 0x00198254
		protected void CheckListDistinct(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckListDistinct"))
			{
				if (invocation.Arguments.Count == 0 || (invocation.Arguments.Count > 1 && !this.IsNull(invocation.Arguments[1])))
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				IExpression expression = invocation.Arguments[0];
				using (base.Context.Enter(ContextLabel.Query, base.FoldingTracingService))
				{
					this.VisitExpression(expression);
				}
				this.CheckTableTypeHasAllComparableColumns(expression);
			}
		}

		// Token: 0x0600769B RID: 30363 RVA: 0x0019A114 File Offset: 0x00198314
		protected void CheckTableTypeHasAllComparableColumns(IExpression list)
		{
			RecordTypeValue itemType = base.GetType(list).AsTableType.ItemType;
			foreach (string text in itemType.Fields.Keys)
			{
				TypeValue typeValue = RecordTypeAlgebra.Field(itemType, text);
				base.ValidateRequireComparableType(typeValue);
			}
		}

		// Token: 0x0600769C RID: 30364 RVA: 0x0019A188 File Offset: 0x00198388
		protected void CheckListFirst(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckListFirst"))
			{
				if (invocation.Arguments.Count == 0 || (invocation.Arguments.Count > 1 && !this.IsNull(invocation.Arguments[1])))
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				IExpression expression = invocation.Arguments[0];
				using (base.Context.Enter(ContextLabel.Query, base.FoldingTracingService))
				{
					if (!this.CheckGroupByKeysAccess(invocation.Arguments[0]))
					{
						this.VisitExpression(expression);
					}
				}
			}
		}

		// Token: 0x0600769D RID: 30365 RVA: 0x0019A250 File Offset: 0x00198450
		protected void CheckTableFirst(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckTableFirst"))
			{
				if (invocation.Arguments.Count < 2)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int, int>>(FoldingWarnings.InvalidArgumentsCount("Table.First", 1, 2));
				}
				IExpression expression = invocation.Arguments[0];
				IExpression expression2 = invocation.Arguments[1];
				using (base.Context.Enter(ContextLabel.Query, base.FoldingTracingService))
				{
					if (base.GetType(expression2).TypeKind != ValueKind.Null)
					{
						throw base.FoldingTracingService.NewFoldingFailureException(null);
					}
					if (!this.CheckGroupByKeysAccess(invocation.Arguments[0]))
					{
						this.VisitExpression(expression);
						this.VisitExpression(expression2);
					}
				}
			}
		}

		// Token: 0x0600769E RID: 30366 RVA: 0x0019A334 File Offset: 0x00198534
		protected void CheckTablePivot(IInvocationExpression invocation)
		{
			FoldingTracingService foldingTracingService = base.FoldingTracingService;
			using (foldingTracingService.NewScope("DbAstExpressionChecker.CheckTablePivot"))
			{
				if (!this.ExternalEnvironment.SqlSettings.SupportsTableRotationFunctions)
				{
					throw foldingTracingService.NewFoldingFailureException("This data source does not support pivot or unpivot operations.");
				}
				if (invocation.Arguments.Count != 5)
				{
					throw foldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("Table.Pivot", 5));
				}
				IExpression expression = invocation.Arguments[0];
				this.CheckTableTypeHasAllComparableColumns(expression);
				this.VisitExpression(expression);
				IExpression expression2 = invocation.Arguments[1];
				string notused;
				if (expression2.Kind != ExpressionKind.List || ((IListExpression)expression2).Members.Any((IExpression m) => !m.TryGetStringConstant(out notused)))
				{
					throw foldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, string, string>>(FoldingWarnings.InvalidFunctionArgument("pivotValues", "Table.Pivot", "a list of constant text values"));
				}
				if (!invocation.Arguments[2].TryGetStringConstant(out notused))
				{
					throw foldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, string, string>>(FoldingWarnings.InvalidFunctionArgument("attributeColumn", "Table.Pivot", "a constant text value"));
				}
				if (!invocation.Arguments[3].TryGetStringConstant(out notused))
				{
					throw foldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, string, string>>(FoldingWarnings.InvalidFunctionArgument("valueColumn", "Table.Pivot", "a constant text value"));
				}
				IExpression expression3 = invocation.Arguments[4];
				if (expression3.Kind != ExpressionKind.Constant)
				{
					throw foldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, string, string>>(FoldingWarnings.InvalidFunctionArgument("aggregationFunction", "Table.Pivot", "a constant function value"));
				}
				Value value = ((IConstantExpression)expression3).Value;
				if (!value.Equals(Library.List.Max) && !value.Equals(Library.List.Min) && !value.Equals(Library.List.Average) && !value.Equals(Library.List.Sum) && !value.Equals(TableModule.Table.RowCount))
				{
					throw foldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, string, string>>(FoldingWarnings.InvalidFunctionArgument("aggregationFunction", "Table.Pivot", "one of the following values: List.Max, List.Min, List.Average, List.Sum, or Table.RowCount"));
				}
			}
		}

		// Token: 0x0600769F RID: 30367 RVA: 0x0019A534 File Offset: 0x00198734
		protected void CheckTableUnpivot(IInvocationExpression invocation)
		{
			FoldingTracingService foldingTracingService = base.FoldingTracingService;
			using (foldingTracingService.NewScope("DbAstExpressionChecker.CheckTableUnpivot"))
			{
				if (!this.ExternalEnvironment.SqlSettings.SupportsTableRotationFunctions)
				{
					throw foldingTracingService.NewFoldingFailureException("This data source does not support pivot or unpivot operations.");
				}
				if (invocation.Arguments.Count != 4)
				{
					throw foldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("Table.Unpivot", 4));
				}
				IExpression expression = invocation.Arguments[0];
				this.VisitExpression(expression);
				IListExpression listExpression = invocation.Arguments[1] as IListExpression;
				string notused;
				if (listExpression == null || listExpression.Members.Any((IExpression m) => !m.TryGetStringConstant(out notused)))
				{
					throw foldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, string, string>>(FoldingWarnings.InvalidFunctionArgument("pivotColumns", "Table.Unpivot", "a list of constant text values"));
				}
				IEnumerable<string> enumerable = listExpression.Members.Select((IExpression mbr) => ((IConstantExpression)mbr).Value.AsString);
				RecordValue fields = (base.Cursor.GetResult(expression) as TableTypeValue).ItemType.Fields;
				ValueKind valueKind = ValueKind.None;
				foreach (string text in enumerable)
				{
					ValueKind typeKind = fields[text]["Type"].AsType.TypeKind;
					if (typeKind != valueKind)
					{
						if (valueKind != ValueKind.None)
						{
							throw foldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, string, string, string>>(FoldingWarnings.MismatchedPivotColumnTypes(enumerable.First<string>(), valueKind.ToString(), text, typeKind.ToString()));
						}
						valueKind = typeKind;
					}
				}
				if (!invocation.Arguments[2].TryGetStringConstant(out notused))
				{
					throw foldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, string, string>>(FoldingWarnings.InvalidFunctionArgument("attributeColumn", "Table.Unpivot", "a constant text value"));
				}
				if (!invocation.Arguments[3].TryGetStringConstant(out notused))
				{
					throw foldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, string, string>>(FoldingWarnings.InvalidFunctionArgument("valueColumn", "Table.Unpivot", "a constant text value"));
				}
			}
		}

		// Token: 0x060076A0 RID: 30368 RVA: 0x0019A77C File Offset: 0x0019897C
		protected void CheckListTransform(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckListTransform"))
			{
				if (base.CheckListTransformInvocation(invocation).TypeKind == ValueKind.List)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x060076A1 RID: 30369 RVA: 0x0019A7D4 File Offset: 0x001989D4
		private void CheckRecordTypeHasAtLeastOneField(RecordTypeValue mapType, IExpression errorTerm)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckRecordTypeHasAtLeastOneField"))
			{
				if (mapType.Fields.Count == 0)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x060076A2 RID: 30370 RVA: 0x0019A828 File Offset: 0x00198A28
		protected virtual void CheckListAverage(IInvocationExpression invocation)
		{
			this.CheckMathAggregateWithPrecision(invocation);
		}

		// Token: 0x060076A3 RID: 30371 RVA: 0x0019A828 File Offset: 0x00198A28
		protected void CheckListSum(IInvocationExpression invocation)
		{
			this.CheckMathAggregateWithPrecision(invocation);
		}

		// Token: 0x060076A4 RID: 30372 RVA: 0x0019A834 File Offset: 0x00198A34
		protected void CheckMathAggregateFunctionOneArgument(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckMathAggregateFunctionOneArgument"))
			{
				if (invocation.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				this.CheckMathAggregateFunction(invocation);
			}
		}

		// Token: 0x060076A5 RID: 30373 RVA: 0x0019A890 File Offset: 0x00198A90
		protected void CheckMathAggregateWithPrecision(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckMathAggregateWithPrecision"))
			{
				int count = invocation.Arguments.Count;
				if (count != 1)
				{
					if (count != 2)
					{
						throw base.FoldingTracingService.NewFoldingFailureException(null);
					}
					this.CheckListOfNumbersType(invocation.Arguments[0]);
					this.CheckPrecisionExpression(invocation.Arguments[1]);
				}
				this.CheckMathAggregateFunction(invocation);
			}
		}

		// Token: 0x060076A6 RID: 30374 RVA: 0x0019A918 File Offset: 0x00198B18
		protected void CheckMathAggregateFunction(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckMathAggregateFunction"))
			{
				using (base.Context.Enter(ContextLabel.Query, base.FoldingTracingService))
				{
					IExpression expression = invocation.Arguments[0];
					if (!this.IsListOfSqlScalarValues(base.GetType(expression)))
					{
						throw base.FoldingTracingService.NewFoldingFailureException(null);
					}
					if (!this.CheckGroupByColumnAccess(expression))
					{
						this.VisitExpression(expression);
					}
				}
			}
		}

		// Token: 0x060076A7 RID: 30375 RVA: 0x0019A9B8 File Offset: 0x00198BB8
		protected void CheckListOfNumbersType(IExpression argument)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckListOfNumbersType"))
			{
				TypeValue type = base.GetType(argument);
				if (!type.IsListType || type.AsListType.ItemType.TypeKind != ValueKind.Number)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x060076A8 RID: 30376 RVA: 0x0019AA24 File Offset: 0x00198C24
		protected virtual void CheckTableJoin(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckTableJoin"))
			{
				TableTypeAlgebra.JoinKind joinKind = this.CheckTableJoinAndGetKind(invocation);
				if (joinKind > TableTypeAlgebra.JoinKind.RightOuter)
				{
					int num = joinKind - TableTypeAlgebra.JoinKind.LeftAnti;
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x060076A9 RID: 30377 RVA: 0x0019AA80 File Offset: 0x00198C80
		protected TableTypeAlgebra.JoinKind CheckTableJoinAndGetKind(IInvocationExpression invocation)
		{
			TableTypeAlgebra.JoinKind joinKind2;
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckTableJoinAndGetKind"))
			{
				using (base.Context.Enter(ContextLabel.Query, base.FoldingTracingService))
				{
					Value value;
					TableTypeAlgebra.JoinKind joinKind;
					if (invocation.Arguments.Count < 5 || invocation.Arguments.Count > 7 || !invocation.Arguments[4].TryGetConstant(out value) || !Library.JoinKind.Type.TryGetValue(value, out joinKind))
					{
						throw base.FoldingTracingService.NewFoldingFailureException(null);
					}
					this.VisitExpression(invocation.Arguments[0]);
					TypeValue type = base.GetType(invocation.Arguments[0]);
					if (type.TypeKind != ValueKind.Table)
					{
						throw base.FoldingTracingService.NewFoldingFailureException(null);
					}
					this.VisitExpression(invocation.Arguments[2]);
					TypeValue result = base.Cursor.GetResult(invocation.Arguments[2]);
					if (result.TypeKind != ValueKind.Table)
					{
						throw base.FoldingTracingService.NewFoldingFailureException(null);
					}
					List<string> list = base.CheckRelationalAlgebraFunctionArgumentAsColumns((IListExpression)invocation.Arguments[1]);
					List<string> list2 = base.CheckRelationalAlgebraFunctionArgumentAsColumns((IListExpression)invocation.Arguments[3]);
					if (list != null && list2 != null)
					{
						if (list.Count != list2.Count)
						{
							throw base.FoldingTracingService.NewFoldingFailureException(null);
						}
						if (list.Count == 0)
						{
							throw base.FoldingTracingService.NewFoldingFailureException(null);
						}
						RecordTypeValue itemType = type.AsTableType.ItemType;
						RecordTypeValue itemType2 = result.AsTableType.ItemType;
						for (int i = 0; i < list.Count; i++)
						{
							TypeValue typeValue = RecordTypeAlgebra.FieldOrDefault(itemType, list[i], null);
							TypeValue typeValue2 = RecordTypeAlgebra.FieldOrDefault(itemType2, list2[i], null);
							if (typeValue2 == null || typeValue == null || TypeAlgebra.Intersect(typeValue2, typeValue).TypeKind == ValueKind.None)
							{
								throw base.FoldingTracingService.NewFoldingFailureException(null);
							}
						}
					}
					if (invocation.Arguments.Count > 6)
					{
						if (invocation.Arguments[6].Kind == ExpressionKind.List)
						{
							IListExpression listExpression = (IListExpression)invocation.Arguments[6];
							if (listExpression.Members.Count != list2.Count)
							{
								throw base.FoldingTracingService.NewFoldingFailureException(null);
							}
							for (int j = 0; j < listExpression.Members.Count; j++)
							{
								Value value2;
								if (!listExpression.Members[j].TryGetConstant(out value2) || (!value2.IsNull && !value2.AsFunction.FunctionIdentity.Equals(Library._Value.NullableEquals.FunctionIdentity) && !value2.AsFunction.FunctionIdentity.Equals(Library._Value.Equals.FunctionIdentity)))
								{
									throw base.FoldingTracingService.NewFoldingFailureException(null);
								}
							}
						}
						else
						{
							if (invocation.Arguments[6].Kind != ExpressionKind.Constant)
							{
								throw base.FoldingTracingService.NewFoldingFailureException(null);
							}
							if (!((IConstantExpression)invocation.Arguments[6]).Value.IsNull)
							{
								throw base.FoldingTracingService.NewFoldingFailureException(null);
							}
						}
					}
					joinKind2 = joinKind;
				}
			}
			return joinKind2;
		}

		// Token: 0x060076AA RID: 30378 RVA: 0x0019ADF0 File Offset: 0x00198FF0
		protected void CheckRequiredScalarType(IBinaryExpression binary)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckRequiredScalarType"))
			{
				TypeValue type = base.GetType(binary.Left);
				TypeValue type2 = base.GetType(binary.Right);
				if (!TypeServices.IsScalar(type))
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				if (!TypeServices.IsScalar(type2))
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x060076AB RID: 30379 RVA: 0x0019AE6C File Offset: 0x0019906C
		protected virtual void CheckTextTrimFunction(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckTextTrimFunction"))
			{
				if (invocation.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("Text.Trim", 1));
				}
				this.VisitExpression(invocation.Arguments[0]);
				base.ValidateRequireComparableType(invocation.Arguments[0]);
			}
		}

		// Token: 0x060076AC RID: 30380 RVA: 0x0019AEF0 File Offset: 0x001990F0
		protected virtual void CheckTextTrimEndFunction(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckTextTrimEndFunction"))
			{
				if (invocation.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("Text.TrimEnd", 1));
				}
				this.VisitExpression(invocation.Arguments[0]);
				base.ValidateRequireComparableType(invocation.Arguments[0]);
			}
		}

		// Token: 0x060076AD RID: 30381 RVA: 0x0019AF74 File Offset: 0x00199174
		protected virtual void CheckTextTrimStartFunction(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckTextTrimStartFunction"))
			{
				if (invocation.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("Text.TrimStart", 1));
				}
				this.VisitExpression(invocation.Arguments[0]);
				base.ValidateRequireComparableType(invocation.Arguments[0]);
			}
		}

		// Token: 0x060076AE RID: 30382 RVA: 0x0019AFF8 File Offset: 0x001991F8
		protected void CheckTextLengthFunction(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckTextLengthFunction"))
			{
				if (invocation.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("Text.Length", 1));
				}
				this.VisitExpression(invocation.Arguments[0]);
				base.ValidateRequireComparableType(invocation.Arguments[0]);
			}
		}

		// Token: 0x060076AF RID: 30383 RVA: 0x0019B07C File Offset: 0x0019927C
		protected void CheckTextLowerFunction(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckTextLowerFunction"))
			{
				if (invocation.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("Text.Lower", 1));
				}
				this.VisitExpression(invocation.Arguments[0]);
				base.ValidateRequireComparableType(invocation.Arguments[0]);
			}
		}

		// Token: 0x060076B0 RID: 30384 RVA: 0x0019B100 File Offset: 0x00199300
		protected void CheckTextUpperFunction(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckTextUpperFunction"))
			{
				if (invocation.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("Text.Upper", 1));
				}
				this.VisitExpression(invocation.Arguments[0]);
				base.ValidateRequireComparableType(invocation.Arguments[0]);
			}
		}

		// Token: 0x060076B1 RID: 30385 RVA: 0x0019B184 File Offset: 0x00199384
		protected Action<IInvocationExpression> CheckTextFunctionTwoArguments(string functionName)
		{
			return delegate(IInvocationExpression invocation)
			{
				using (this.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckTextFunctionTwoArguments"))
				{
					if (invocation.Arguments.Count != 2)
					{
						throw this.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount(functionName, 2));
					}
					this.VisitExpression(invocation.Arguments[0]);
					this.ValidateRequireComparableType(invocation.Arguments[0]);
					this.Visit(invocation.Arguments[1]);
					this.ValidateRequireComparableType(invocation.Arguments[1]);
				}
			};
		}

		// Token: 0x060076B2 RID: 30386 RVA: 0x0019B1A4 File Offset: 0x001993A4
		private void CheckAsFunctionArgument(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckAsFunctionArgument"))
			{
				if (invocation.Arguments.Count == 2 && invocation.Arguments[1] is IConstantExpression)
				{
					IConstantExpression constantExpression = (IConstantExpression)invocation.Arguments[1];
					if (constantExpression.Value.IsType && base.GetType(invocation.Arguments[0]).IsCompatibleWith(constantExpression.Value.AsType))
					{
						this.VisitExpression(invocation.Arguments[0]);
						return;
					}
				}
				throw base.FoldingTracingService.NewFoldingFailureException(null);
			}
		}

		// Token: 0x060076B3 RID: 30387 RVA: 0x0019B264 File Offset: 0x00199464
		protected override DbAstExpressionChecker.SqlVariable[] CreateDefaultBindingsFromParameterTypes(FunctionTypeValue functionType)
		{
			DbAstExpressionChecker.SqlVariable[] array = new DbAstExpressionChecker.SqlVariable[functionType.ParameterCount];
			for (int i = 0; i < functionType.ParameterCount; i++)
			{
				array[i] = DbAstExpressionChecker.SqlVariable.CreateOther();
			}
			return array;
		}

		// Token: 0x060076B4 RID: 30388 RVA: 0x0019B298 File Offset: 0x00199498
		private void FoundIdentifierReference(IIdentifierExpression identifier)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.FoundIdentifierReference"))
			{
				DbAstExpressionChecker.SqlVariable value = base.Environment.GetValue(identifier.Name, identifier.IsInclusive);
				this.variableGraph.AddVertex(value);
				foreach (DbAstExpressionChecker.SqlVariable sqlVariable in this.variableInitializerAncestors)
				{
					this.variableGraph.AddEdge(value, sqlVariable);
				}
				if (value.Kind == DbAstExpressionChecker.SqlVariableKind.Group)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x060076B5 RID: 30389 RVA: 0x0019B354 File Offset: 0x00199554
		private bool IsListOfSqlScalarValues(TypeValue type)
		{
			return type.TypeKind == ValueKind.List && TypeServices.IsScalar(type.AsListType.ItemType);
		}

		// Token: 0x060076B6 RID: 30390 RVA: 0x0019B372 File Offset: 0x00199572
		private bool IsNull(IExpression expression)
		{
			expression = EnvironmentAstVisitor<DbAstExpressionChecker.SqlVariable>.Reduce(expression);
			if (expression.Kind == ExpressionKind.Constant)
			{
				return ((IConstantExpression)expression).Value.IsNull;
			}
			return base.GetType(expression).TypeKind == ValueKind.Null;
		}

		// Token: 0x060076B7 RID: 30391 RVA: 0x0019B3A8 File Offset: 0x001995A8
		protected void IsFoldableDateFromTypes(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.IsFoldableDateFromTypes"))
			{
				if (invocation.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				ValueKind typeKind = base.GetType(invocation.Arguments[0]).TypeKind;
				if (typeKind - ValueKind.Date > 2 && typeKind != ValueKind.Number)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				this.CheckArgumentsAreValid(invocation);
			}
		}

		// Token: 0x060076B8 RID: 30392 RVA: 0x0019B434 File Offset: 0x00199634
		public static bool IsDateTimeCompatibleType(TypeValue type)
		{
			return type.TypeKind == ValueKind.Date || type.TypeKind == ValueKind.DateTime || type.TypeKind == ValueKind.DateTimeZone;
		}

		// Token: 0x060076B9 RID: 30393 RVA: 0x0019B453 File Offset: 0x00199653
		protected virtual bool IsDateOrDateTimeCompatibleType(TypeValue type)
		{
			return DbAstExpressionChecker.IsDateTimeCompatibleType(type);
		}

		// Token: 0x060076BA RID: 30394 RVA: 0x0019B45C File Offset: 0x0019965C
		protected override IExpression VisitBinary(IBinaryExpression binary)
		{
			IExpression expression;
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.VisitBinary"))
			{
				if (binary.Operator == BinaryOperator2.As)
				{
					expression = this.VisitExpression(binary.Left);
				}
				else if (this.TryVisitDateTimeDurationBinaryArithmetic(binary))
				{
					expression = binary;
				}
				else
				{
					base.VisitBinary(binary);
					TypeValue type = base.GetType(binary.Left);
					TypeValue type2 = base.GetType(binary.Right);
					switch (binary.Operator)
					{
					case BinaryOperator2.Add:
					case BinaryOperator2.Multiply:
					case BinaryOperator2.Divide:
					case BinaryOperator2.GreaterThan:
					case BinaryOperator2.LessThan:
					case BinaryOperator2.GreaterThanOrEquals:
					case BinaryOperator2.LessThanOrEquals:
						this.CheckRequiredScalarType(binary);
						this.CheckCompatibleOperands(binary);
						base.ValidateRequireComparableType(binary.Left);
						base.ValidateRequireComparableType(binary.Right);
						break;
					case BinaryOperator2.Subtract:
						this.CheckSubtractOperation(binary);
						break;
					case BinaryOperator2.Equals:
					case BinaryOperator2.NotEquals:
						this.CheckRequiredScalarType(binary);
						if (type.TypeKind == type2.TypeKind && type.TypeKind != ValueKind.Null && type2.TypeKind != ValueKind.Null)
						{
							base.ValidateRequireComparableType(binary.Left);
							base.ValidateRequireComparableType(binary.Right);
						}
						break;
					case BinaryOperator2.And:
					case BinaryOperator2.Or:
						break;
					case BinaryOperator2.MetadataAdd:
						throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string>>(FoldingWarnings.UnsupportedOperator("'meta'"));
					case BinaryOperator2.Range:
						throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string>>(FoldingWarnings.UnsupportedOperator("range"));
					case BinaryOperator2.Concatenate:
						this.CheckGeneralTypeErrors(type, binary.Left);
						this.CheckGeneralTypeErrors(type2, binary.Right);
						if (type.IsRecordType || type2.IsRecordType)
						{
							throw base.FoldingTracingService.NewFoldingFailureException(null);
						}
						break;
					case BinaryOperator2.As:
						throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string>>(FoldingWarnings.UnsupportedOperator("'as'"));
					case BinaryOperator2.Is:
						throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string>>(FoldingWarnings.UnsupportedOperator("'is'"));
					case BinaryOperator2.Coalesce:
						if (!this.AreSameSqlScalarTypesOrNull(type, type2))
						{
							throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, string, string>>(FoldingWarnings.UnsupportedOperatorTypes("coalesce", type.TypeKind.ToString(), type2.TypeKind.ToString()));
						}
						break;
					default:
						throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string>>(FoldingWarnings.UnknownOperator(binary.Operator.ToString()));
					}
					expression = binary;
				}
			}
			return expression;
		}

		// Token: 0x060076BB RID: 30395 RVA: 0x0019B6D4 File Offset: 0x001998D4
		private bool TryVisitDateTimeDurationBinaryArithmetic(IBinaryExpression binary)
		{
			TypeValue type = base.GetType(binary.Left);
			TypeValue type2 = base.GetType(binary.Right);
			if ((binary.Operator == BinaryOperator2.Add || binary.Operator == BinaryOperator2.Subtract) && DbAstExpressionChecker.IsDateTimeCompatibleType(type) && type2.TypeKind == ValueKind.Duration)
			{
				Value value;
				if (binary.Right.TryGetConstant(out value) && value.IsDuration && !this.ExternalEnvironment.SqlSettings.SupportsIntervalConstants)
				{
					this.VisitExpression(binary.Left);
				}
				else
				{
					base.VisitBinary(binary);
				}
				this.CheckRequiredScalarType(binary);
				return true;
			}
			if (binary.Operator == BinaryOperator2.Add && type.TypeKind == ValueKind.Duration && DbAstExpressionChecker.IsDateTimeCompatibleType(type2))
			{
				Value value2;
				if (binary.Left.TryGetConstant(out value2) && value2.IsDuration && !this.ExternalEnvironment.SqlSettings.SupportsIntervalConstants)
				{
					this.VisitExpression(binary.Right);
				}
				else
				{
					base.VisitBinary(binary);
				}
				this.CheckRequiredScalarType(binary);
				return true;
			}
			return false;
		}

		// Token: 0x060076BC RID: 30396 RVA: 0x0019B7CC File Offset: 0x001999CC
		protected virtual void CheckSubtractOperation(IBinaryExpression binary)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckSubtractOperation"))
			{
				this.CheckRequiredScalarType(binary);
				if (base.GetType(binary).TypeKind == ValueKind.Duration)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x060076BD RID: 30397 RVA: 0x0019B82C File Offset: 0x00199A2C
		protected override IExpression VisitConstant(IConstantExpression constant)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.VisitConstant"))
			{
				if (base.Context.Milestone == ContextLabel.SortBody)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				IQueryResultValue queryResultValue = constant.Value as IQueryResultValue;
				if (queryResultValue != null)
				{
					base.CheckQueryResultValueHasConsistentEnvironment(queryResultValue, constant);
				}
				else
				{
					base.CheckRequiredScalarType(base.GetType(constant));
					if (constant.Value.IsNumber)
					{
						NumberValue asNumber = constant.Value.AsNumber;
						if (asNumber.IsNaN || asNumber.Equals(Library.Number.PositiveInfinity) || asNumber.Equals(Library.Number.NegativeInfinity))
						{
							throw base.FoldingTracingService.NewFoldingFailureException(null);
						}
					}
					if (constant.Value.IsText && constant.Value.AsText.Length > this.MaxCharacterStringLiteralLength)
					{
						throw base.FoldingTracingService.NewFoldingFailureException(null);
					}
				}
			}
			return constant;
		}

		// Token: 0x060076BE RID: 30398 RVA: 0x0019B928 File Offset: 0x00199B28
		protected override IExpression VisitIdentifier(IIdentifierExpression identifier)
		{
			base.VisitIdentifier(identifier);
			this.FoundIdentifierReference(identifier);
			return identifier;
		}

		// Token: 0x060076BF RID: 30399 RVA: 0x0019B93C File Offset: 0x00199B3C
		protected override IExpression VisitIf(IIfExpression @if)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.VisitIf"))
			{
				base.VisitIf(@if);
				if (!this.AreSameSqlScalarTypesOrNull(base.GetType(@if.TrueCase), base.GetType(@if.FalseCase)))
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
			return @if;
		}

		// Token: 0x060076C0 RID: 30400 RVA: 0x0019B9B0 File Offset: 0x00199BB0
		protected override VariableInitializer VisitInitializer(VariableInitializer member)
		{
			VariableInitializer variableInitializer;
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.VisitInitializer"))
			{
				base.Cursor.Push(base.Context.CurrentRecord, member.Name);
				DbAstExpressionChecker.SqlVariable value = base.Environment.GetValue(member.Name.Name, true);
				this.variableInitializerAncestors.Push(value);
				this.variableGraph.AddVertex(value);
				this.CheckInitializerType(base.GetType(member.Value));
				base.VisitInitializer(member);
				this.variableInitializerAncestors.Pop();
				base.Cursor.Pop();
				variableInitializer = member;
			}
			return variableInitializer;
		}

		// Token: 0x060076C1 RID: 30401 RVA: 0x0019BA74 File Offset: 0x00199C74
		protected virtual void CheckInitializerType(TypeValue type)
		{
			if (!TypeServices.IsScalar(type))
			{
				throw base.FoldingTracingService.NewFoldingFailureException(null);
			}
		}

		// Token: 0x060076C2 RID: 30402 RVA: 0x0019BA8C File Offset: 0x00199C8C
		protected override IExpression VisitInvocation(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.VisitInvocation"))
			{
				if (base.GetType(invocation).TypeKind == ValueKind.None)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				IExpression expression = EnvironmentAstVisitor<DbAstExpressionChecker.SqlVariable>.Reduce(invocation.Function);
				if (expression.Kind != ExpressionKind.Constant)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				IConstantExpression constantExpression = (IConstantExpression)expression;
				Action<IInvocationExpression> action;
				if (constantExpression.Value.IsFunction && this.FunctionLookup.TryGetValue(constantExpression.Value.AsFunction, out action))
				{
					action(invocation);
				}
				else
				{
					if (!(constantExpression.Value is QueryResultFunctionValue))
					{
						throw base.FoldingTracingService.NewFoldingFailureException(null);
					}
					base.VisitInvocation(invocation);
				}
			}
			return invocation;
		}

		// Token: 0x060076C3 RID: 30403 RVA: 0x0019BB68 File Offset: 0x00199D68
		protected override IExpression VisitMultiFieldRecordProjection(IMultiFieldRecordProjectionExpression multiFieldRecordProjection)
		{
			using (base.Context.Enter(ContextLabel.Query, base.FoldingTracingService))
			{
				this.CheckGeneralTypeErrors(base.GetType(multiFieldRecordProjection), multiFieldRecordProjection);
				base.VisitMultiFieldRecordProjection(multiFieldRecordProjection);
			}
			return multiFieldRecordProjection;
		}

		// Token: 0x060076C4 RID: 30404 RVA: 0x0019BBC0 File Offset: 0x00199DC0
		protected override IExpression VisitRecord(IRecordExpression record)
		{
			List<DbAstExpressionChecker.SqlVariable> list = new List<DbAstExpressionChecker.SqlVariable>(record.Members.Count);
			foreach (VariableInitializer variableInitializer in record.Members)
			{
				list.Add(DbAstExpressionChecker.SqlVariable.CreateOther());
			}
			return base.VisitRecord(record, DbAstExpressionChecker.SqlVariable.CreateOther(), list);
		}

		// Token: 0x060076C5 RID: 30405 RVA: 0x0019BC30 File Offset: 0x00199E30
		protected override IExpression VisitUnary(IUnaryExpression unary)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.VisitUnary"))
			{
				base.VisitUnary(unary);
				switch (unary.Operator)
				{
				case UnaryOperator2.Not:
					break;
				case UnaryOperator2.Negative:
					base.CheckRequiredScalarType(base.GetType(unary));
					break;
				case UnaryOperator2.Positive:
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string>>(FoldingWarnings.UnsupportedOperator("unary '+'"));
				default:
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string>>(FoldingWarnings.UnknownOperator(unary.Operator.ToString()));
				}
			}
			return unary;
		}

		// Token: 0x060076C6 RID: 30406 RVA: 0x0019BCDC File Offset: 0x00199EDC
		protected void CheckBind(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckBind"))
			{
				if (invocation.Arguments.Count != 2)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("Action.Bind", 2));
				}
				Value value;
				if (!invocation.Arguments[1].TryGetConstant(out value) || !value.IsFunction || !value.AsFunction.FunctionIdentity.Equals(ReturnRowCountFunctionValue.Instance.FunctionIdentity))
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				bool flag = this.countOnly;
				this.countOnly = true;
				try
				{
					this.CheckStatement(invocation.Arguments[0]);
				}
				finally
				{
					this.countOnly = flag;
				}
			}
		}

		// Token: 0x060076C7 RID: 30407 RVA: 0x0019BDB8 File Offset: 0x00199FB8
		protected void CheckInsertRows(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckInsertRows"))
			{
				if (invocation.Arguments.Count != 2)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("Action.InsertRows", 2));
				}
				IExpression expression = this.VisitExpression(invocation.Arguments[0]);
				TypeValue type = base.GetType(invocation.Arguments[0]);
				if (type.TypeKind != ValueKind.Table)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				QueryResultTableValue queryResultTableValue;
				IFunctionExpression functionExpression;
				if (!DbAstExpressionChecker.TryGetTargetTable(expression, out queryResultTableValue, out functionExpression) || functionExpression != null)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				TypeValue typeValue;
				try
				{
					this.VisitExpression(invocation.Arguments[1]);
					typeValue = base.GetType(invocation.Arguments[1]);
					if (typeValue.TypeKind != ValueKind.Table)
					{
						throw base.FoldingTracingService.NewFoldingFailureException(null);
					}
				}
				catch (FoldingFailureException)
				{
					IList<IExpression> list;
					if (!DbAstExpressionChecker.TryGetInvocation(invocation.Arguments[1], StatementBuilder.BatchedRows, 1, out list) || list[0].Kind != ExpressionKind.Constant)
					{
						throw base.FoldingTracingService.NewFoldingFailureException(null);
					}
					typeValue = ((IConstantExpression)list[0]).Value.Type;
				}
				if (!this.countOnly && !this.CanReturnUpdatedRows(type, typeValue, false))
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				this.CheckTableTypeHasScalarColumns(typeValue);
				this.CheckTableTypesAreCompatible(type.AsTableType, typeValue.AsTableType);
			}
		}

		// Token: 0x060076C8 RID: 30408 RVA: 0x0019BF64 File Offset: 0x0019A164
		protected void CheckUpdateRows(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckUpdateRows"))
			{
				if (invocation.Arguments.Count != 2)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("Action.UpdateRows", 2));
				}
				base.Cursor.GetParameterResults(invocation);
				IExpression expression = this.VisitExpression(invocation.Arguments[0]);
				TypeValue type = base.GetType(invocation.Arguments[0]);
				if (type.TypeKind != ValueKind.Table)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				QueryResultTableValue queryResultTableValue;
				IFunctionExpression functionExpression;
				if (!DbAstExpressionChecker.TryGetTargetTable(expression, out queryResultTableValue, out functionExpression))
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				IList<IExpression> list = null;
				if (functionExpression != null)
				{
					FunctionTypeValue asFunctionType = base.Cursor.GetResult(functionExpression).AsFunctionType;
					base.VisitLambda(functionExpression, asFunctionType, EnvironmentAstVisitor<DbAstExpressionChecker.SqlVariable>.GetParameterTypes(asFunctionType), new Func<IExpression, IExpression>(this.VisitExpression), this.CreateDefaultBindingsFromParameterTypes(asFunctionType));
					list = this.GetPredicates(functionExpression.Expression);
				}
				bool flag = false;
				TableTypeValue tableTypeValue = type as TableTypeValue;
				TableKey primaryKey = tableTypeValue.GetPrimaryKey();
				List<string> list2 = null;
				if (primaryKey != null && primaryKey.Columns != null)
				{
					list2 = tableTypeValue.ItemType.Fields.Keys.ToList<string>().Where((string t, int i) => primaryKey.Columns.Contains(i)).ToList<string>();
				}
				IListExpression listExpression = EnvironmentAstVisitor<DbAstExpressionChecker.SqlVariable>.Reduce(invocation.Arguments[1]) as IListExpression;
				if (listExpression == null)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				foreach (IExpression expression2 in listExpression.Members)
				{
					IListExpression listExpression2 = expression2 as IListExpression;
					if (listExpression2 == null || listExpression2.Members.Count != 2 || listExpression2.Members[0].Kind != ExpressionKind.Constant || !((IConstantExpression)listExpression2.Members[0]).Value.IsText || listExpression2.Members[1].Kind != ExpressionKind.Function)
					{
						throw base.FoldingTracingService.NewFoldingFailureException(null);
					}
					IFunctionExpression functionExpression2 = (IFunctionExpression)listExpression2.Members[1];
					FunctionTypeValue asFunctionType2 = base.Cursor.GetResult(functionExpression2).AsFunctionType;
					base.VisitLambda(functionExpression2, asFunctionType2, EnvironmentAstVisitor<DbAstExpressionChecker.SqlVariable>.GetParameterTypes(asFunctionType2), new Func<IExpression, IExpression>(this.VisitExpression), this.CreateDefaultBindingsFromParameterTypes(asFunctionType2));
					string asString = ((IConstantExpression)listExpression2.Members[0]).Value.AsString;
					if (list2 != null && list2.Contains(asString))
					{
						IFieldAccessExpression fieldAccessExpression = functionExpression2.Expression as IFieldAccessExpression;
						if (fieldAccessExpression == null)
						{
							Value value;
							Value value2;
							flag |= !this.TryGetSelectedRowKey(list, asString, out value) || !functionExpression2.Expression.TryGetConstant(out value2) || !value2.Equals(value);
						}
						else
						{
							flag |= fieldAccessExpression.MemberName.Name != asString;
						}
					}
					TypeValue asType = type.AsTableType.ItemType.Fields[asString]["Type"].AsType;
					if (!this.AreCompatibleSqlScalarTypesOrNull(asType, asFunctionType2.ReturnType))
					{
						throw new FoldingFailureException(ValueException.NewExpressionError<Message1>(Strings.Table_UpdateNotSupportedWrongColumnType(asString), null, null));
					}
				}
				if (!this.countOnly && !this.CanReturnUpdatedRows(type, null, flag))
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x060076C9 RID: 30409 RVA: 0x0019C30C File Offset: 0x0019A50C
		private IList<IExpression> GetPredicates(IExpression expression)
		{
			List<IExpression> list = new List<IExpression>();
			Queue<IExpression> queue = new Queue<IExpression>();
			queue.Enqueue(expression);
			while (queue.Count > 0)
			{
				IExpression expression2 = queue.Dequeue();
				if (expression2.Kind == ExpressionKind.Binary)
				{
					BinaryExpressionSyntaxNode binaryExpressionSyntaxNode = (BinaryExpressionSyntaxNode)expression2;
					if (binaryExpressionSyntaxNode.Operator == BinaryOperator2.And)
					{
						queue.Enqueue(binaryExpressionSyntaxNode.Left);
						queue.Enqueue(binaryExpressionSyntaxNode.Right);
					}
					else
					{
						list.Add(expression2);
					}
				}
				else
				{
					list.Add(expression2);
				}
			}
			return list;
		}

		// Token: 0x060076CA RID: 30410 RVA: 0x0019C384 File Offset: 0x0019A584
		private bool TryGetSelectedRowKey(IList<IExpression> conjunctedFilters, string columnName, out Value value)
		{
			value = null;
			if (conjunctedFilters == null || string.IsNullOrEmpty(columnName))
			{
				return false;
			}
			foreach (IExpression expression in conjunctedFilters)
			{
				if (this.TryGetColumnConstantComparison(expression, columnName, out value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060076CB RID: 30411 RVA: 0x0019C3E8 File Offset: 0x0019A5E8
		private bool TryGetColumnConstantComparison(IExpression expression, string columnName, out Value value)
		{
			value = null;
			EqualsBinaryExpressionSyntaxNode equalsBinaryExpressionSyntaxNode = expression as EqualsBinaryExpressionSyntaxNode;
			if (equalsBinaryExpressionSyntaxNode == null)
			{
				return false;
			}
			string text;
			Value value2;
			if ((this.TryGetColumnAndConstant(equalsBinaryExpressionSyntaxNode.Left, equalsBinaryExpressionSyntaxNode.Right, out text, out value2) || this.TryGetColumnAndConstant(equalsBinaryExpressionSyntaxNode.Right, equalsBinaryExpressionSyntaxNode.Left, out text, out value2)) && text == columnName)
			{
				value = value2;
				return true;
			}
			return false;
		}

		// Token: 0x060076CC RID: 30412 RVA: 0x0019C444 File Offset: 0x0019A644
		private bool TryGetColumnAndConstant(IExpression left, IExpression right, out string fieldName, out Value constantValue)
		{
			fieldName = null;
			constantValue = null;
			if (left == null || right == null)
			{
				return false;
			}
			IFieldAccessExpression fieldAccessExpression = left as IFieldAccessExpression;
			IConstantExpression constantExpression = right as IConstantExpression;
			if (fieldAccessExpression != null && constantExpression != null)
			{
				fieldName = fieldAccessExpression.MemberName.Name;
				constantValue = constantExpression.Value;
			}
			return fieldName != null && constantValue != null;
		}

		// Token: 0x060076CD RID: 30413 RVA: 0x0019C498 File Offset: 0x0019A698
		protected void CheckDeleteRows(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckDeleteRows"))
			{
				if (invocation.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("TableAction.DeleteRows", 1));
				}
				IExpression expression = this.VisitExpression(invocation.Arguments[0]);
				TypeValue type = base.GetType(invocation.Arguments[0]);
				if (type.TypeKind != ValueKind.Table)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				QueryResultTableValue queryResultTableValue;
				IFunctionExpression functionExpression;
				if (!DbAstExpressionChecker.TryGetTargetTable(expression, out queryResultTableValue, out functionExpression))
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				if (functionExpression != null)
				{
					FunctionTypeValue asFunctionType = base.Cursor.GetResult(functionExpression).AsFunctionType;
					base.VisitLambda(functionExpression, asFunctionType, EnvironmentAstVisitor<DbAstExpressionChecker.SqlVariable>.GetParameterTypes(asFunctionType), new Func<IExpression, IExpression>(this.VisitExpression), this.CreateDefaultBindingsFromParameterTypes(asFunctionType));
				}
				if (!this.countOnly && !this.CanReturnUpdatedRows(type, null, false))
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x060076CE RID: 30414 RVA: 0x0019C5A8 File Offset: 0x0019A7A8
		protected virtual bool CanReturnUpdatedRows(TypeValue targetType, TypeValue sourceType = null, bool updatesPrimaryKey = false)
		{
			return this.ExternalEnvironment.SqlSettings.SupportsOutputClause;
		}

		// Token: 0x060076CF RID: 30415 RVA: 0x0019C5BC File Offset: 0x0019A7BC
		public static bool TryGetInvocation(IExpression expression, FunctionValue function, int argumentCount, out IList<IExpression> arguments)
		{
			IInvocationExpression invocationExpression = EnvironmentAstVisitor<DbAstExpressionChecker.SqlVariable>.Reduce(expression) as IInvocationExpression;
			if (invocationExpression != null && invocationExpression.Arguments.Count == argumentCount)
			{
				IConstantExpression constantExpression = invocationExpression.Function as IConstantExpression;
				if (constantExpression != null && constantExpression.Value.IsFunction && constantExpression.Value.AsFunction.FunctionIdentity.Equals(function.FunctionIdentity))
				{
					arguments = invocationExpression.Arguments;
					return true;
				}
			}
			arguments = null;
			return false;
		}

		// Token: 0x060076D0 RID: 30416 RVA: 0x0019C630 File Offset: 0x0019A830
		public static bool TryGetConstantFunction(IInvocationExpression expression, out FunctionValue function)
		{
			Value value;
			if (expression.Function.TryGetConstant(out value) && value.IsFunction)
			{
				function = value.AsFunction;
				return true;
			}
			function = null;
			return false;
		}

		// Token: 0x060076D1 RID: 30417 RVA: 0x0019C664 File Offset: 0x0019A864
		public static bool TryGetTargetTable(IExpression expression, out QueryResultTableValue table, out IFunctionExpression predicate)
		{
			expression = EnvironmentAstVisitor<DbAstExpressionChecker.SqlVariable>.Reduce(expression);
			ExpressionKind kind = expression.Kind;
			if (kind != ExpressionKind.Constant)
			{
				if (kind == ExpressionKind.Invocation)
				{
					IInvocationExpression invocationExpression = (IInvocationExpression)expression;
					IConstantExpression constantExpression = EnvironmentAstVisitor<DbAstExpressionChecker.SqlVariable>.Reduce(invocationExpression.Function) as IConstantExpression;
					if (constantExpression != null && constantExpression.Value.IsFunction && constantExpression.Value.AsFunction.FunctionIdentity.Equals(TableModule.Table.SelectRows.FunctionIdentity) && invocationExpression.Arguments.Count == 2)
					{
						IFunctionExpression functionExpression = EnvironmentAstVisitor<DbAstExpressionChecker.SqlVariable>.Reduce(invocationExpression.Arguments[1]) as IFunctionExpression;
						if (functionExpression != null && functionExpression.FunctionType.Parameters.Count == 1 && DbAstExpressionChecker.TryGetTargetTable(invocationExpression.Arguments[0], out table, out predicate))
						{
							bool flag = false;
							Value value;
							if (functionExpression.Expression.TryGetConstant(out value) && value.IsLogical)
							{
								flag = value.AsBoolean;
							}
							if (predicate == null)
							{
								predicate = (flag ? null : functionExpression);
								return true;
							}
							if (flag)
							{
								return true;
							}
						}
					}
				}
			}
			else
			{
				QueryResultTableValue queryResultTableValue = ((IConstantExpression)expression).Value as QueryResultTableValue;
				if (queryResultTableValue != null && queryResultTableValue.SyntaxTree.Kind == ExpressionKind.Constant)
				{
					table = queryResultTableValue;
					predicate = null;
					return true;
				}
			}
			table = null;
			predicate = null;
			return false;
		}

		// Token: 0x060076D2 RID: 30418 RVA: 0x0019C7A8 File Offset: 0x0019A9A8
		protected void CheckPrecisionExpression(IExpression argument)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckPrecisionExpression"))
			{
				IConstantExpression constantExpression = argument as IConstantExpression;
				if (constantExpression == null)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				if (!constantExpression.Value.IsNull)
				{
					if (!this.CanCastNumericPrecision || (!constantExpression.Value.Equals(Library.PrecisionEnum.Decimal) && !constantExpression.Value.Equals(Library.PrecisionEnum.Double)))
					{
						throw base.FoldingTracingService.NewFoldingFailureException(null);
					}
				}
			}
		}

		// Token: 0x060076D3 RID: 30419 RVA: 0x0019C844 File Offset: 0x0019AA44
		private void CheckTableTypesAreCompatible(TableTypeValue targetType, TableTypeValue sourceType)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckTableTypesAreCompatible"))
			{
				RecordValue fields = targetType.ItemType.Fields;
				RecordValue fields2 = sourceType.ItemType.Fields;
				if (fields2.Keys.Length > fields.Keys.Length)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				for (int i = 0; i < fields2.Keys.Length; i++)
				{
					string text = fields2.Keys[i];
					TypeValue asType = fields2[text]["Type"].AsType;
					TypeValue asType2 = fields[text]["Type"].AsType;
					if (!this.AreCompatibleSqlScalarTypes(asType2, asType))
					{
						throw new FoldingFailureException(ValueException.NewExpressionError<Message1>(Strings.Table_UpdateNotSupportedWrongColumnType(text), null, null));
					}
				}
			}
		}

		// Token: 0x060076D4 RID: 30420 RVA: 0x0019C934 File Offset: 0x0019AB34
		protected void CheckTableTypeHasScalarColumns(TypeValue tableType)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckTableTypeHasScalarColumns"))
			{
				if (tableType.TypeKind != ValueKind.Table)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				RecordValue fields = tableType.AsTableType.ItemType.Fields;
				for (int i = 0; i < fields.Keys.Length; i++)
				{
					try
					{
						base.CheckRequiredScalarType(fields[i]["Type"].AsType);
					}
					catch (FoldingFailureException)
					{
						throw new FoldingFailureException(ValueException.NewExpressionError<Message1>(Strings.Table_UpdateNotSupportedWrongColumnType(fields.Keys[i]), null, null));
					}
				}
			}
		}

		// Token: 0x060076D5 RID: 30421 RVA: 0x0019C9F8 File Offset: 0x0019ABF8
		private void CheckNativeQueryInvocation(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("AstExpressionChecker.CheckNativeQueryInvocation"))
			{
				if (invocation.Arguments.Count < 2)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				Value value = ((IConstantExpression)invocation.Arguments[0]).Value;
				Value value2 = ((IConstantExpression)invocation.Arguments[1]).Value;
				Value value3 = ((invocation.Arguments.Count >= 3) ? ((IConstantExpression)invocation.Arguments[2]).Value : Value.Null);
				Value value4 = ((invocation.Arguments.Count == 4) ? ((IConstantExpression)invocation.Arguments[3]).Value : Value.Null);
				DbCatalogTableValue dbCatalogTableValue = value as DbCatalogTableValue;
				if (dbCatalogTableValue == null || !dbCatalogTableValue.Environment.IsSameDataSourceEnvironment(this.ExternalEnvironment) || !value2.IsText || !Options.IsNativeQueryFoldingEnabled(value3, value4))
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				if (this.ExternalEnvironment.UserOptions.GetBool("EnableCrossDatabaseFolding", false) || dbCatalogTableValue.Environment.UserOptions.GetBool("EnableCrossDatabaseFolding", false))
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x040040AF RID: 16559
		private Dictionary<FunctionValue, Action<IInvocationExpression>> functionLookup;

		// Token: 0x040040B0 RID: 16560
		private Dictionary<FunctionValue, Action<IInvocationExpression>> statementFunctionLookup;

		// Token: 0x040040B1 RID: 16561
		private readonly VariableGraph<DbAstExpressionChecker.SqlVariable> variableGraph = new VariableGraph<DbAstExpressionChecker.SqlVariable>();

		// Token: 0x040040B2 RID: 16562
		private readonly Stack<DbAstExpressionChecker.SqlVariable> variableInitializerAncestors = new Stack<DbAstExpressionChecker.SqlVariable>();

		// Token: 0x040040B3 RID: 16563
		private bool countOnly;

		// Token: 0x040040B4 RID: 16564
		private static readonly HashSet<Value> supportedAggregateFunctions = new HashSet<Value>
		{
			TableModule.Table.RowCount,
			LanguageLibrary.List.Count,
			Library.List.CountOfNull,
			Library.List.CountOfNotNull,
			Library.List.CountOfDistinct,
			Library.List.CountOfDistinctNull,
			Library.List.CountOfDistinctNotNull,
			Library.List.Average,
			Library.List.Max,
			Library.List.Min,
			Library.List.StandardDeviation,
			Library.List.Sum
		};

		// Token: 0x0200118C RID: 4492
		internal enum SqlVariableKind
		{
			// Token: 0x040040B6 RID: 16566
			Group,
			// Token: 0x040040B7 RID: 16567
			Other
		}

		// Token: 0x0200118D RID: 4493
		internal class SqlVariable
		{
			// Token: 0x170020AF RID: 8367
			// (get) Token: 0x060076D7 RID: 30423 RVA: 0x0019CC03 File Offset: 0x0019AE03
			// (set) Token: 0x060076D8 RID: 30424 RVA: 0x0019CC0B File Offset: 0x0019AE0B
			public IList<Identifier> GroupKeys { get; private set; }

			// Token: 0x170020B0 RID: 8368
			// (get) Token: 0x060076D9 RID: 30425 RVA: 0x0019CC14 File Offset: 0x0019AE14
			// (set) Token: 0x060076DA RID: 30426 RVA: 0x0019CC1C File Offset: 0x0019AE1C
			public DbAstExpressionChecker.SqlVariableKind Kind { get; private set; }

			// Token: 0x060076DB RID: 30427 RVA: 0x0019CC25 File Offset: 0x0019AE25
			public static DbAstExpressionChecker.SqlVariable CreateGroup(IList<Identifier> keys)
			{
				return new DbAstExpressionChecker.SqlVariable
				{
					GroupKeys = keys,
					Kind = DbAstExpressionChecker.SqlVariableKind.Group
				};
			}

			// Token: 0x060076DC RID: 30428 RVA: 0x0019CC3A File Offset: 0x0019AE3A
			public static DbAstExpressionChecker.SqlVariable CreateOther()
			{
				return new DbAstExpressionChecker.SqlVariable
				{
					Kind = DbAstExpressionChecker.SqlVariableKind.Other
				};
			}
		}

		// Token: 0x0200118E RID: 4494
		protected sealed class SqlCheckerContext : CheckerContext
		{
			// Token: 0x060076DE RID: 30430 RVA: 0x0019CC48 File Offset: 0x0019AE48
			public SqlCheckerContext(DbAstExpressionChecker checker)
				: base(ContextLabel.None, new Action<CheckerContext>(checker.SetCurrentContext))
			{
			}

			// Token: 0x060076DF RID: 30431 RVA: 0x000DB9B5 File Offset: 0x000D9BB5
			private SqlCheckerContext(ContextLabel milestone, CheckerContext parentContext)
				: base(milestone, parentContext)
			{
			}

			// Token: 0x170020B1 RID: 8369
			// (get) Token: 0x060076E0 RID: 30432 RVA: 0x0019CC60 File Offset: 0x0019AE60
			protected override bool IsValid
			{
				get
				{
					switch (base.Milestone)
					{
					case ContextLabel.Query:
					case ContextLabel.Select:
					case ContextLabel.Transform:
					case ContextLabel.Combine:
						return true;
					case ContextLabel.Sort:
					{
						CheckerContext checkerContext = base.Parent;
						bool flag = false;
						while (checkerContext.Milestone != ContextLabel.Root)
						{
							ContextLabel milestone = checkerContext.Milestone;
							if (milestone != ContextLabel.Query)
							{
								switch (milestone)
								{
								case ContextLabel.Transform:
									goto IL_0065;
								case ContextLabel.Sort:
									if (flag)
									{
										return false;
									}
									goto IL_0070;
								}
								return false;
							}
							goto IL_0065;
							IL_0070:
							checkerContext = checkerContext.Parent;
							continue;
							IL_0065:
							flag = true;
							goto IL_0070;
						}
						return true;
					}
					}
					return base.IsValid;
				}
			}

			// Token: 0x060076E1 RID: 30433 RVA: 0x0019CCF7 File Offset: 0x0019AEF7
			protected override CheckerContext EnterHelper(ContextLabel milestone)
			{
				return new DbAstExpressionChecker.SqlCheckerContext(milestone, this);
			}
		}
	}
}
