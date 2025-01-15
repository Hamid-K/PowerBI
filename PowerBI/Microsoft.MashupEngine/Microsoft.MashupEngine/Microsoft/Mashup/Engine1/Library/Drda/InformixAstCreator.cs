using System;
using System.Globalization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Drda
{
	// Token: 0x02000CA7 RID: 3239
	internal sealed class InformixAstCreator : DrdaAstCreator
	{
		// Token: 0x0600577A RID: 22394 RVA: 0x001304CC File Offset: 0x0012E6CC
		private InformixAstCreator(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, DbEnvironment environment)
			: base(expression, cursor, environment)
		{
		}

		// Token: 0x0600577B RID: 22395 RVA: 0x001304D7 File Offset: 0x0012E6D7
		public static DbAstCreator Create(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, DbEnvironment externalEnvironment)
		{
			return new InformixAstCreator(expression, cursor, externalEnvironment);
		}

		// Token: 0x0600577C RID: 22396 RVA: 0x001304E4 File Offset: 0x0012E6E4
		protected override SqlExpression ConvertNumberToDate(SqlExpression number)
		{
			SqlExpression sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.TruncSqlString), new SqlExpression[] { number }), InformixAstCreator.InformixIntervalExpression.Day(SqlConstant.One));
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(base.BaseOADateTime, sqlExpression);
		}

		// Token: 0x0600577D RID: 22397 RVA: 0x00130528 File Offset: 0x0012E728
		protected override SqlExpression ConvertNumberToDateTime(SqlExpression number)
		{
			SqlExpression sqlExpression = this.ConvertNumberToDate(number);
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Abs(number), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Floor(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Abs(number))), SqlConstant.SecondsPerDay), InformixAstCreator.InformixIntervalExpression.Second(SqlConstant.One));
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression, sqlExpression2);
		}

		// Token: 0x0600577E RID: 22398 RVA: 0x00130574 File Offset: 0x0012E774
		protected override SqlExpression ConvertDateToNumber(SqlExpression expression)
		{
			return new CastCall
			{
				Type = SqlDataType.Int,
				Expression = new CastCall
				{
					Type = new SqlDataType(TypeValue.Text, new ConstantSqlString("VarChar(12)")),
					Expression = new CastCall
					{
						Type = new SqlDataType(TypeValue.Duration, new ConstantSqlString("INTERVAL DAY(9) TO DAY")),
						Expression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(expression, base.BaseOADateTime)
					}
				}
			};
		}

		// Token: 0x0600577F RID: 22399 RVA: 0x001305F0 File Offset: 0x0012E7F0
		protected override SqlExpression ConvertDateTimeToNumber(SqlExpression expression)
		{
			SqlExpression sqlExpression = this.ConvertDateToNumber(expression);
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(expression, base.BaseOADateTime);
			SqlExpression sqlExpression3 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(sqlExpression2, new CastCall
			{
				Type = new SqlDataType(TypeValue.Duration, new ConstantSqlString("INTERVAL DAY(9) TO DAY")),
				Expression = sqlExpression2
			});
			SqlExpression sqlExpression4 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(new CastCall
			{
				Type = SqlDataType.Float,
				Expression = new CastCall
				{
					Type = new SqlDataType(TypeValue.Text, new ConstantSqlString("VarChar(18)")),
					Expression = new CastCall
					{
						Type = new SqlDataType(TypeValue.Duration, new ConstantSqlString("INTERVAL SECOND(9) TO FRACTION(5)")),
						Expression = sqlExpression3
					}
				}
			}, SqlConstant.SecondsPerDay);
			SqlExpression sqlExpression5 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(base.CreateOADateTimeSignExpression(expression), sqlExpression4);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression, sqlExpression5);
		}

		// Token: 0x06005780 RID: 22400 RVA: 0x001306C0 File Offset: 0x0012E8C0
		protected override SqlExpression CreateAddOperation(IBinaryExpression add)
		{
			if (base.GetType(add.Left).TypeKind == ValueKind.Text)
			{
				return this.CreateBinaryScalarOperation(BinaryScalarOperator.Concatenate, add);
			}
			SqlExpression sqlExpression;
			SqlExpression sqlExpression2;
			this.ExtendDateTime(add, out sqlExpression, out sqlExpression2);
			return new BinaryScalarOperation(sqlExpression, BinaryScalarOperator.Add, sqlExpression2);
		}

		// Token: 0x06005781 RID: 22401 RVA: 0x00130700 File Offset: 0x0012E900
		protected override SqlExpression CreateSubtractOperation(IBinaryExpression subtract)
		{
			SqlExpression sqlExpression;
			SqlExpression sqlExpression2;
			this.ExtendDateTime(subtract, out sqlExpression, out sqlExpression2);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(sqlExpression, sqlExpression2);
		}

		// Token: 0x06005782 RID: 22402 RVA: 0x00130720 File Offset: 0x0012E920
		private void ExtendDateTime(IBinaryExpression invocation, out SqlExpression left, out SqlExpression right)
		{
			TypeValue type = base.GetType(invocation.Left);
			TypeValue type2 = base.GetType(invocation.Right);
			left = base.CreateScalarExpression(invocation.Left);
			right = base.CreateScalarExpression(invocation.Right);
			if (DbAstExpressionChecker.IsDateTimeCompatibleType(type))
			{
				left = InformixAstCreator.ExtendDateTimeExpression.YearToFraction(left);
			}
			if (DbAstExpressionChecker.IsDateTimeCompatibleType(type2))
			{
				right = InformixAstCreator.ExtendDateTimeExpression.YearToFraction(right);
			}
		}

		// Token: 0x06005783 RID: 22403 RVA: 0x00130784 File Offset: 0x0012E984
		protected override SqlExpression CreateDivideOperation(IBinaryExpression divide)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(divide.Left);
			SqlExpression sqlExpression2 = base.CreateScalarExpression(divide.Right);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(new CastCall
			{
				Type = SqlDataType.Float,
				Expression = sqlExpression
			}, new CastCall
			{
				Type = SqlDataType.Float,
				Expression = sqlExpression2
			});
		}

		// Token: 0x06005784 RID: 22404 RVA: 0x001307E0 File Offset: 0x0012E9E0
		protected override SqlExpression CreateToText(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			SqlExpression sqlExpression;
			if (base.GetType(expression).TypeKind == ValueKind.Logical)
			{
				Condition condition = base.GetValue(expression) as Condition;
				if (condition != null)
				{
					return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Case().When(condition).Then(SqlConstant.StringTrue)
						.When(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Not(condition))
						.Then(SqlConstant.StringFalse)
						.Else(SqlConstant.Null);
				}
				sqlExpression = new CaseFunction
				{
					CaseExpression = base.CreateScalarExpression(expression),
					ElseExpression = SqlConstant.Null,
					WhenItems = 
					{
						new WhenItem
						{
							When = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant("t"),
							Then = SqlConstant.StringTrue
						},
						new WhenItem
						{
							When = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant("f"),
							Then = SqlConstant.StringFalse
						}
					}
				};
			}
			else
			{
				sqlExpression = base.CreateScalarExpression(expression);
			}
			return new CastCall
			{
				Type = new SqlDataType(TypeValue.Text, SqlLanguageStrings.VarChar255SqlString),
				Expression = sqlExpression
			};
		}

		// Token: 0x06005785 RID: 22405 RVA: 0x00130908 File Offset: 0x0012EB08
		protected override ScalarExpression Constant(Value constant, TypeValue type)
		{
			if (!constant.IsNull)
			{
				type = type.NonNullable;
				if (type.TypeKind == ValueKind.Time)
				{
					return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.ToDateSqlString), new SqlExpression[]
					{
						new SqlConstant(ConstantType.AnsiString, new DateTime(constant.AsTime.AsClrTimeSpan.Ticks).ToString("HH:mm:ss.fffff", CultureInfo.InvariantCulture)),
						new SqlConstant(ConstantType.AnsiString, "%H:%M:%S.%F5")
					});
				}
			}
			return base.Constant(constant, type);
		}

		// Token: 0x06005786 RID: 22406 RVA: 0x00046303 File Offset: 0x00044503
		protected override SqlExpression CreateNumberArcTangent2(IInvocationExpression invocation)
		{
			return base.CreateFunctionCall(SqlLanguageStrings.Atan2SqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x06005787 RID: 22407 RVA: 0x0008055A File Offset: 0x0007E75A
		protected override SqlExpression CastToDecimal(SqlExpression expression)
		{
			return new CastCall
			{
				Type = SqlDataType.Decimal,
				Expression = expression
			};
		}

		// Token: 0x06005788 RID: 22408 RVA: 0x0013098D File Offset: 0x0012EB8D
		protected override SqlExpression CreateDurationFrom(IInvocationExpression invocation)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(base.CreateScalarExpression(invocation.Arguments[0]), SqlConstant.SecondsPerDay), InformixAstCreator.InformixIntervalExpression.Second(SqlConstant.One));
		}

		// Token: 0x06005789 RID: 22409 RVA: 0x001309BC File Offset: 0x0012EBBC
		protected override SqlConstant IntervalConstant(TimeSpan value)
		{
			return new SqlConstant(ConstantType.Interval, string.Format(CultureInfo.InvariantCulture, "{0:00} {1:00}:{2:00}:{3:00}.{4:00000}", new object[]
			{
				value.Days,
				value.Hours,
				value.Minutes,
				value.Seconds,
				value.Ticks % 10000000L
			}));
		}

		// Token: 0x0600578A RID: 22410 RVA: 0x00130A38 File Offset: 0x0012EC38
		protected override SqlExpression CreateDateTimeStartOfDay(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			IExpression expression = invocation.Arguments[0];
			ValueKind typeKind = base.GetType(expression).TypeKind;
			if (typeKind == ValueKind.Date)
			{
				return sqlExpression;
			}
			if (typeKind == ValueKind.DateTime)
			{
				return InformixAstCreator.ExtendDateTimeExpression.YearToSecond(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DateSqlString), new SqlExpression[] { sqlExpression }));
			}
			throw new InvalidOperationException(Strings.UnreachableCodePath);
		}

		// Token: 0x0600578B RID: 22411 RVA: 0x00130AAC File Offset: 0x0012ECAC
		protected override SqlExpression CreateDurationTotalDays(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			return new CastCall
			{
				Type = SqlDataType.Int,
				Expression = new CastCall
				{
					Type = new SqlDataType(TypeValue.Text, new ConstantSqlString("VarChar(12)")),
					Expression = new CastCall
					{
						Type = new SqlDataType(TypeValue.Duration, new ConstantSqlString("INTERVAL DAY(9) TO DAY")),
						Expression = sqlExpression
					}
				}
			};
		}

		// Token: 0x04003173 RID: 12659
		private const string TimeFormat = "HH:mm:ss.fffff";

		// Token: 0x04003174 RID: 12660
		private const string InformixTimeMask = "%H:%M:%S.%F5";

		// Token: 0x02000CA8 RID: 3240
		private sealed class ExtendDateTimeExpression : SqlExpression
		{
			// Token: 0x0600578C RID: 22412 RVA: 0x00130B2D File Offset: 0x0012ED2D
			private ExtendDateTimeExpression(SqlExpression expression, ConstantSqlString qualifier)
			{
				this.expression = expression;
				this.qualifier = qualifier;
			}

			// Token: 0x0600578D RID: 22413 RVA: 0x00130B43 File Offset: 0x0012ED43
			public static InformixAstCreator.ExtendDateTimeExpression YearToFraction(SqlExpression expression)
			{
				return new InformixAstCreator.ExtendDateTimeExpression(expression, new ConstantSqlString("year to fraction(5)"));
			}

			// Token: 0x0600578E RID: 22414 RVA: 0x00130B55 File Offset: 0x0012ED55
			public static InformixAstCreator.ExtendDateTimeExpression YearToSecond(SqlExpression expression)
			{
				return new InformixAstCreator.ExtendDateTimeExpression(expression, new ConstantSqlString("year to second"));
			}

			// Token: 0x17001A5B RID: 6747
			// (get) Token: 0x0600578F RID: 22415 RVA: 0x00002105 File Offset: 0x00000305
			public override int Precedence
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x06005790 RID: 22416 RVA: 0x00130B68 File Offset: 0x0012ED68
			public override void WriteCreateScript(ScriptWriter writer)
			{
				writer.WriteSpaceAfter(SqlLanguageStrings.ExtendSqlString);
				writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
				writer.WriteSubexpression(10, this.expression);
				writer.WriteSpaceAfter(SqlLanguageSymbols.CommaSqlString);
				writer.Write(this.qualifier);
				writer.Write(SqlLanguageSymbols.CloseParenthesisSqlString);
			}

			// Token: 0x04003175 RID: 12661
			private readonly ConstantSqlString qualifier;

			// Token: 0x04003176 RID: 12662
			private readonly SqlExpression expression;
		}

		// Token: 0x02000CA9 RID: 3241
		private sealed class InformixIntervalExpression : SqlExpression
		{
			// Token: 0x06005791 RID: 22417 RVA: 0x00130BBB File Offset: 0x0012EDBB
			private InformixIntervalExpression(SqlExpression expression, ConstantSqlString intervalUnit)
			{
				this.expression = expression;
				this.intervalUnit = intervalUnit;
			}

			// Token: 0x06005792 RID: 22418 RVA: 0x00130BD1 File Offset: 0x0012EDD1
			public static InformixAstCreator.InformixIntervalExpression Day(SqlExpression expression)
			{
				return new InformixAstCreator.InformixIntervalExpression(expression, new ConstantSqlString("day to day"));
			}

			// Token: 0x06005793 RID: 22419 RVA: 0x00130BE3 File Offset: 0x0012EDE3
			public static InformixAstCreator.InformixIntervalExpression Second(SqlExpression expression)
			{
				return new InformixAstCreator.InformixIntervalExpression(expression, new ConstantSqlString("second to second"));
			}

			// Token: 0x17001A5C RID: 6748
			// (get) Token: 0x06005794 RID: 22420 RVA: 0x00002105 File Offset: 0x00000305
			public override int Precedence
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x06005795 RID: 22421 RVA: 0x00130BF5 File Offset: 0x0012EDF5
			public override void WriteCreateScript(ScriptWriter writer)
			{
				writer.WriteSpaceAfter(SqlLanguageStrings.IntervalSqlString);
				writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
				writer.WriteSubexpression(10, this.expression);
				writer.Write(SqlLanguageSymbols.CloseParenthesisSqlString);
				writer.WriteSpaceBefore(this.intervalUnit);
			}

			// Token: 0x04003177 RID: 12663
			private readonly ConstantSqlString intervalUnit;

			// Token: 0x04003178 RID: 12664
			private readonly SqlExpression expression;
		}
	}
}
