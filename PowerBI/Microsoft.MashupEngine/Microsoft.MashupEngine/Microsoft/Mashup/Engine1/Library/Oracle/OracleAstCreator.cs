using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Oracle
{
	// Token: 0x02000559 RID: 1369
	internal sealed class OracleAstCreator : DbAstCreator
	{
		// Token: 0x06002B90 RID: 11152 RVA: 0x00083E99 File Offset: 0x00082099
		private OracleAstCreator(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, DbEnvironment environment)
			: base(expression, cursor, environment)
		{
		}

		// Token: 0x06002B91 RID: 11153 RVA: 0x00083EB0 File Offset: 0x000820B0
		protected override Dictionary<FunctionValue, Func<IInvocationExpression, SqlExpression>> GetFunctions()
		{
			Dictionary<FunctionValue, Func<IInvocationExpression, SqlExpression>> functions = base.GetFunctions();
			functions.AddRange(new Dictionary<FunctionValue, Func<IInvocationExpression, SqlExpression>>
			{
				{
					Library.Date.Month,
					new Func<IInvocationExpression, SqlExpression>(this.CreateDateMonth)
				},
				{
					Library.Date.Year,
					new Func<IInvocationExpression, SqlExpression>(this.CreateDateYear)
				},
				{
					TimeSpecificFunction.DateTimeZoneLocalNow,
					new Func<IInvocationExpression, SqlExpression>(this.CreateSysDateTimeOffset)
				},
				{
					TimeSpecificFunction.DateTimeZoneUtcNow,
					new Func<IInvocationExpression, SqlExpression>(this.CreateInstantUtcNow)
				}
			});
			return functions;
		}

		// Token: 0x06002B92 RID: 11154 RVA: 0x00083F2C File Offset: 0x0008212C
		protected override Dictionary<FunctionValue, Func<IInvocationExpression, SqlStatement>> GetStatementFunctions()
		{
			return new Dictionary<FunctionValue, Func<IInvocationExpression, SqlStatement>>
			{
				{
					ActionModule.Action.Bind,
					new Func<IInvocationExpression, SqlStatement>(base.CreateBind)
				},
				{
					ActionModule.TableAction.InsertRows,
					new Func<IInvocationExpression, SqlStatement>(this.CreateInsertRows)
				},
				{
					ActionModule.TableAction.UpdateRows,
					new Func<IInvocationExpression, SqlStatement>(this.CreateUpdateRows)
				},
				{
					ActionModule.TableAction.DeleteRows,
					new Func<IInvocationExpression, SqlStatement>(this.CreateDeleteRows)
				}
			};
		}

		// Token: 0x17001047 RID: 4167
		// (get) Token: 0x06002B93 RID: 11155 RVA: 0x00002139 File Offset: 0x00000339
		protected override bool PivotRequiresAlias
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002B94 RID: 11156 RVA: 0x00083FA0 File Offset: 0x000821A0
		public Dictionary<int, ValueException> GetErrorColumns(SqlQueryExpression query)
		{
			if (this.customTypeColumns.Count == 0)
			{
				return null;
			}
			QuerySpecification querySpecification = query as QuerySpecification;
			if (querySpecification == null || querySpecification.SelectItems == null || querySpecification.SelectItems.Count == 0)
			{
				return null;
			}
			Dictionary<int, ValueException> dictionary = new Dictionary<int, ValueException>(this.customTypeColumns.Count);
			for (int i = 0; i < querySpecification.SelectItems.Count; i++)
			{
				SelectItem selectItem = querySpecification.SelectItems[i];
				string text;
				if (this.customTypeColumns.TryGetValue(selectItem.Name.Name, out text))
				{
					dictionary.Add(i, ValueException.NewDataSourceError<Message2>(Strings.UDTNotSupport(selectItem.Name.Name, text), TextValue.New(selectItem.Name.Name), null));
				}
			}
			return dictionary;
		}

		// Token: 0x06002B95 RID: 11157 RVA: 0x0008405C File Offset: 0x0008225C
		protected override SelectItem CreateSelectItem(SqlAstCreatorBase<DbAstCreator.SqlVariable>.XColumnReference columnReference)
		{
			string name = columnReference.Name.Name;
			if (this.customTypeColumns.ContainsKey(name))
			{
				return new SelectItem(SqlConstant.Null, Alias.NewNativeAlias(name));
			}
			return base.CreateSelectItem(columnReference);
		}

		// Token: 0x06002B96 RID: 11158 RVA: 0x000840A0 File Offset: 0x000822A0
		protected override void AdjustFieldAccessForType(TypeValue type, ref SqlExpression expression)
		{
			if (type.Facets.NativeTypeName == "interval year to month")
			{
				expression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(this.GetDateMonthExpression(expression), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(this.GetDateYearExpression(expression), SqlConstant.Twelve));
			}
		}

		// Token: 0x06002B97 RID: 11159 RVA: 0x000020FA File Offset: 0x000002FA
		protected override SqlDataType[] AdjustArgumentsForType(TypeValue[] types)
		{
			return null;
		}

		// Token: 0x06002B98 RID: 11160 RVA: 0x000840DA File Offset: 0x000822DA
		public static OracleAstCreator Create(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, DbEnvironment externalEnvironment)
		{
			return new OracleAstCreator(expression, cursor, externalEnvironment);
		}

		// Token: 0x06002B99 RID: 11161 RVA: 0x000840E4 File Offset: 0x000822E4
		protected override SqlStatement CreateExecuteStoredProcedure(StoredProcedureReference procedureCall)
		{
			return new OracleExecuteStoredProcedureStatement(procedureCall);
		}

		// Token: 0x06002B9A RID: 11162 RVA: 0x000840EC File Offset: 0x000822EC
		protected override SqlExpression CreateAddOperation(IBinaryExpression add)
		{
			IExpression left = add.Left;
			if (base.GetType(left).TypeKind == ValueKind.Text)
			{
				return this.CreateBinaryScalarOperation(BinaryScalarOperator.Concatenate, add);
			}
			return this.CreateBinaryScalarOperation(BinaryScalarOperator.Add, add);
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x00084120 File Offset: 0x00082320
		protected override SqlExpression CreateListAverage(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			ValueKind typeKind = base.GetType(expression).AsListType.ItemType.TypeKind;
			SqlConstant sqlConstant;
			if (typeKind != ValueKind.DateTime)
			{
				if (typeKind != ValueKind.DateTimeZone)
				{
					return base.CreateListAverage(invocation);
				}
				sqlConstant = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(OracleAstCreator.BaseDateTime.AddDefaultTimeZone(base.ExternalEnvironment.Host));
			}
			else
			{
				sqlConstant = OracleAstCreator.DateTimeAggregateBase;
			}
			SqlExpression sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(base.CreateListAggregateInput(expression), sqlConstant);
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DaySqlString, SqlLanguageStrings.ExtractSqlString), new SqlExpression[] { sqlExpression }), SqlConstant.SecondsPerDay);
			SqlExpression sqlExpression3 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.HourSqlString, SqlLanguageStrings.ExtractSqlString), new SqlExpression[] { sqlExpression }), SqlConstant.SecondsPerHour);
			SqlExpression sqlExpression4 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.MinuteSqlString, SqlLanguageStrings.ExtractSqlString), new SqlExpression[] { sqlExpression }), SqlConstant.SecondsPerMinute);
			SqlExpression sqlExpression5 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.SecondSqlString, SqlLanguageStrings.ExtractSqlString), new SqlExpression[] { sqlExpression });
			SqlExpression sqlExpression6 = base.LiftForGroup(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression2, sqlExpression3), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression4, sqlExpression5)));
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlConstant, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.NumToDaySecondIntervalSqlString), new SqlExpression[]
			{
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.Avg(sqlExpression6),
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(SqlLanguageStrings.SecondSqlString.String)
			}));
		}

		// Token: 0x06002B9C RID: 11164 RVA: 0x00084290 File Offset: 0x00082490
		protected override SqlExpression CreateTextContains(IInvocationExpression invocation)
		{
			IList<IExpression> arguments = invocation.Arguments;
			return new BinaryLogicalOperation(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.InStrSqlString), new SqlExpression[]
			{
				base.CreateScalarExpression(arguments[0]),
				base.CreateScalarExpression(arguments[1])
			}), BinaryLogicalOperator.GreaterThan, SqlConstant.Zero);
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x0004628D File Offset: 0x0004448D
		protected override SqlExpression CreateDateTimeAddMonths(IInvocationExpression invocation)
		{
			return base.CreateFunctionCall(SqlLanguageStrings.AddMonthsSqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x06002B9E RID: 11166 RVA: 0x000842E4 File Offset: 0x000824E4
		protected override SqlExpression CreateDateTimeAddYears(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			SqlExpression sqlExpression2 = new BinaryScalarOperation(base.CreateScalarExpression(invocation.Arguments[1]), BinaryScalarOperator.Multiply, SqlConstant.Twelve);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.AddMonthsSqlString), new SqlExpression[] { sqlExpression, sqlExpression2 });
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x0008433F File Offset: 0x0008253F
		protected override SqlExpression CreateSysDateTimeOffset(IInvocationExpression invocation)
		{
			return base.CreateFunctionCall(SqlLanguageStrings.SysTimestampSqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x00084358 File Offset: 0x00082558
		protected override SqlExpression CreateDurationFrom(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.NumToDaySecondIntervalSqlString), new SqlExpression[]
			{
				sqlExpression,
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(SqlLanguageStrings.DaySqlString.String)
			});
		}

		// Token: 0x06002BA1 RID: 11169 RVA: 0x000843A8 File Offset: 0x000825A8
		protected override SqlExpression CreateInstantUtcNow(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateFunctionCall(SqlLanguageStrings.SysTimestampSqlString, Array.Empty<SqlExpression>())(invocation);
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.SysExtractUtcSqlString), new SqlExpression[] { sqlExpression });
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.FromTzSqlString), new SqlExpression[]
			{
				sqlExpression2,
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant("0:00")
			});
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x00046303 File Offset: 0x00044503
		protected override SqlExpression CreateNumberArcTangent2(IInvocationExpression invocation)
		{
			return base.CreateFunctionCall(SqlLanguageStrings.Atan2SqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x0008440C File Offset: 0x0008260C
		protected override SqlExpression CreateNumberLogBase10(IInvocationExpression invocation)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.LogSqlString), new SqlExpression[]
			{
				SqlConstant.Ten,
				base.CreateScalarExpression(invocation.Arguments[0])
			});
		}

		// Token: 0x06002BA4 RID: 11172 RVA: 0x00084440 File Offset: 0x00082640
		protected override SqlExpression CreateNumberMod(SqlExpression number, SqlExpression divisor, IConstantExpression precision = null)
		{
			Func<SqlExpression, SqlExpression> func = base.CreateNumericCastFromPrecision(precision);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.ModSqlString), new SqlExpression[]
			{
				func(number),
				func(divisor)
			});
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x00046345 File Offset: 0x00044545
		protected override SqlExpression CreateNumberNaturalLogarithm(IInvocationExpression invocation)
		{
			return base.CreateFunctionCall(SqlLanguageStrings.LnSqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x0008447E File Offset: 0x0008267E
		protected override SqlExpression ConvertNumberToDate(SqlExpression number)
		{
			return this.AddDaysToDateTime(base.BaseOADateTime, number);
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x00084490 File Offset: 0x00082690
		protected override SqlExpression ConvertNumberToDateTime(SqlExpression number)
		{
			SqlExpression sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.RoundSqlString), new SqlExpression[] { SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(number, SqlConstant.SecondsPerDay) });
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Case().When(new BinaryLogicalOperation(number, BinaryLogicalOperator.LessThan, SqlConstant.Zero)).Then(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(sqlExpression, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(this.CreateNumberMod(sqlExpression, SqlConstant.SecondsPerDay, null), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(2))))
				.Else(sqlExpression);
			return this.AddDaysToDateTime(base.BaseOADateTime, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(sqlExpression2, SqlConstant.SecondsPerDay));
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x00084528 File Offset: 0x00082728
		private SqlExpression AddDaysToDateTime(SqlExpression dateTime, SqlExpression days)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(dateTime, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.NumToDaySecondIntervalSqlString), new SqlExpression[]
			{
				days,
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(SqlLanguageStrings.DaySqlString.String)
			}));
		}

		// Token: 0x06002BA9 RID: 11177 RVA: 0x00084569 File Offset: 0x00082769
		protected override SqlExpression CreateRoundUp(IInvocationExpression invocation)
		{
			return base.CreateFunctionCall(SqlLanguageStrings.CeilSqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x00084584 File Offset: 0x00082784
		protected override SqlExpression CreateToText(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			if (base.GetType(expression).TypeKind != ValueKind.Logical)
			{
				return base.CreateFunctionCall(SqlLanguageStrings.ToCharSqlString, Array.Empty<SqlExpression>())(invocation);
			}
			SqlExpression sqlExpression = base.GetValue(expression);
			if (sqlExpression is Condition)
			{
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Case().When((Condition)sqlExpression).Then(SqlConstant.StringTrue)
					.When(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Not((Condition)sqlExpression))
					.Then(SqlConstant.StringFalse)
					.Else(SqlConstant.Null);
			}
			sqlExpression = base.CreateScalarExpression(expression);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Case().When(new BinaryLogicalOperation(sqlExpression, BinaryLogicalOperator.Equals, SqlConstant.One)).Then(SqlConstant.StringTrue)
				.When(new BinaryLogicalOperation(sqlExpression, BinaryLogicalOperator.Equals, SqlConstant.Zero))
				.Then(SqlConstant.StringFalse)
				.Else(SqlConstant.Null);
		}

		// Token: 0x06002BAB RID: 11179 RVA: 0x0008468F File Offset: 0x0008288F
		protected override SqlExpression Len(SqlExpression expression)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.LengthSqlString), new SqlExpression[] { expression });
		}

		// Token: 0x06002BAC RID: 11180 RVA: 0x0005028C File Offset: 0x0004E48C
		protected override SqlExpression CreateTextTrim(IInvocationExpression invocation)
		{
			return base.CreateFunctionCall(SqlLanguageStrings.TrimSqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x06002BAD RID: 11181 RVA: 0x000846AC File Offset: 0x000828AC
		protected override SqlExpression CreateTextStartsWith(IInvocationExpression invocation)
		{
			IList<IExpression> arguments = invocation.Arguments;
			return new BinaryLogicalOperation(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.InStrSqlString), new SqlExpression[]
			{
				base.CreateScalarExpression(arguments[0]),
				base.CreateScalarExpression(arguments[1])
			}), BinaryLogicalOperator.Equals, SqlConstant.One);
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x00084700 File Offset: 0x00082900
		protected override SqlExpression CreateTextEndsWith(IInvocationExpression invocation)
		{
			IList<IExpression> arguments = invocation.Arguments;
			SqlExpression sqlExpression = base.CreateScalarExpression(arguments[1]);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Equals(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.SubstrSqlString), new SqlExpression[]
			{
				base.CreateScalarExpression(arguments[0]),
				new BinaryScalarOperation(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.LengthSqlString), new SqlExpression[] { sqlExpression }), BinaryScalarOperator.Multiply, SqlConstant.MinusOne)
			}), sqlExpression);
		}

		// Token: 0x06002BAF RID: 11183 RVA: 0x0008477C File Offset: 0x0008297C
		protected override SqlExpression CreateDivideOperation(IBinaryExpression divide)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(divide.Left);
			SqlExpression sqlExpression2 = base.CreateScalarExpression(divide.Right);
			return new BinaryScalarOperation(sqlExpression, BinaryScalarOperator.Divide, sqlExpression2);
		}

		// Token: 0x06002BB0 RID: 11184 RVA: 0x000847A9 File Offset: 0x000829A9
		protected override SqlExpression CreateBinaryFromText(IInvocationExpression invocation)
		{
			return base.CreateFunctionCall(SqlLanguageStrings.CastToRawSqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x06002BB1 RID: 11185 RVA: 0x000847C4 File Offset: 0x000829C4
		protected override OutputClause CreateOutputClause(Alias alias, TableTypeValue tableType)
		{
			if (!this.countOnly)
			{
				return new OracleAstCreator.OracleOutputClause(tableType.ItemType.Fields.Keys.Select((string c) => new SelectItem(new ColumnReference(Alias.NewNativeAlias(c)))).ToList<SelectItem>());
			}
			return OutputClause.Null;
		}

		// Token: 0x06002BB2 RID: 11186 RVA: 0x00084820 File Offset: 0x00082A20
		protected override SelectItem MitigateColumn(string name, TypeValue type, ref bool mitigateColumns)
		{
			string nativeTypeName = type.Facets.NativeTypeName;
			TypeValue typeValue;
			if (!string.IsNullOrEmpty(nativeTypeName) && !this.dbEnvironment.TryGetClrTypeValue(nativeTypeName, out typeValue))
			{
				this.customTypeColumns.Add(name, nativeTypeName);
				return new SelectItem(SqlConstant.Null, Alias.NewNativeAlias(name));
			}
			return base.MitigateColumn(name, type, ref mitigateColumns);
		}

		// Token: 0x06002BB3 RID: 11187 RVA: 0x00084878 File Offset: 0x00082A78
		protected override IEnumerable<SelectItem> GetPagingSourceQueryOrderedSelectItems(SqlQueryExpression queryExpression, string[] columnNames)
		{
			PagingQuerySpecification pagingQuerySpecification = queryExpression as PagingQuerySpecification;
			if (pagingQuerySpecification != null && pagingQuerySpecification.SelectItems != null && pagingQuerySpecification.SelectItems.Count > 0)
			{
				return pagingQuerySpecification.SelectItems.Select(delegate(SelectItem selectItem)
				{
					if (selectItem.Expression == SqlConstant.Null)
					{
						return selectItem;
					}
					return base.CreatePagingSourceQueryOrderedSelectItem(selectItem.Name.Name);
				});
			}
			return base.GetPagingSourceQueryOrderedSelectItems(queryExpression, columnNames);
		}

		// Token: 0x06002BB4 RID: 11188 RVA: 0x000848C8 File Offset: 0x00082AC8
		private bool TryAsDate(TypeValue type, SqlExpression expression, out SqlExpression newExpression)
		{
			SqlConstant sqlConstant = expression as SqlConstant;
			DateTimeValue dateTimeValue;
			if (type.TypeKind == ValueKind.DateTime && type.Facets.NativeTypeName != null && type.Facets.NativeTypeName.Equals("date") && sqlConstant != null && sqlConstant.Type == ConstantType.DateTime && DateTimeValue.TryParseFromText(sqlConstant.Literal, CultureInfo.InvariantCulture, out dateTimeValue))
			{
				string text = dateTimeValue.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
				newExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.ToDateSqlString), new SqlExpression[]
				{
					new SqlConstant(ConstantType.AnsiString, text),
					new SqlConstant(ConstantType.AnsiString, "YYYY-MM-DD HH24:MI:SS")
				});
				return true;
			}
			newExpression = null;
			return false;
		}

		// Token: 0x06002BB5 RID: 11189 RVA: 0x00084974 File Offset: 0x00082B74
		protected override void AdjustArgumentsForType(BinaryOperator2 binaryOperator, ref TypeValue leftType, ref TypeValue rightType, ref SqlExpression leftExpression, ref SqlExpression rightExpression)
		{
			SqlExpression sqlExpression;
			if (this.TryAsDate(leftType, rightExpression, out sqlExpression))
			{
				rightExpression = sqlExpression;
				return;
			}
			if (this.TryAsDate(rightType, leftExpression, out sqlExpression))
			{
				leftExpression = sqlExpression;
				return;
			}
			base.AdjustArgumentsForType(binaryOperator, ref leftType, ref rightType, ref leftExpression, ref rightExpression);
		}

		// Token: 0x06002BB6 RID: 11190 RVA: 0x000849B8 File Offset: 0x00082BB8
		protected override ScalarExpression Constant(Value constant, TypeValue type)
		{
			if (!constant.IsNull)
			{
				type = type.NonNullable;
				switch (type.TypeKind)
				{
				case ValueKind.Date:
					return SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateConstant(constant.AsDate.AsClrDateTime);
				case ValueKind.DateTime:
				{
					ValueKind typeKind = constant.Type.TypeKind;
					if (typeKind == ValueKind.Date || typeKind == ValueKind.DateTimeZone)
					{
						return base.Constant(constant, constant.Type);
					}
					break;
				}
				case ValueKind.Number:
					if (constant.Type.TypeKind == ValueKind.Logical)
					{
						return base.Constant(constant, constant.Type);
					}
					break;
				}
			}
			return base.Constant(constant, type);
		}

		// Token: 0x06002BB7 RID: 11191 RVA: 0x00084A54 File Offset: 0x00082C54
		protected override SqlExpression CreateToDate(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			TypeValue type = base.GetType(expression);
			SqlExpression sqlExpression = base.CreateScalarExpression(expression);
			ValueKind typeKind = type.TypeKind;
			if (typeKind == ValueKind.DateTime)
			{
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.TruncSqlString), new SqlExpression[] { sqlExpression });
			}
			if (typeKind == ValueKind.Number)
			{
				return this.ConvertNumberToDate(sqlExpression);
			}
			return new CastCall
			{
				Type = SqlDataType.Date,
				Expression = sqlExpression
			};
		}

		// Token: 0x06002BB8 RID: 11192 RVA: 0x00084AC4 File Offset: 0x00082CC4
		private SqlExpression CreateDateMonth(IInvocationExpression invocation)
		{
			return this.GetDateMonthExpression(base.CreateScalarExpression(invocation.Arguments[0]));
		}

		// Token: 0x06002BB9 RID: 11193 RVA: 0x00084ADE File Offset: 0x00082CDE
		private SqlExpression GetDateMonthExpression(SqlExpression dateExpression)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.MonthSqlString, SqlLanguageStrings.ExtractSqlString), new SqlExpression[] { dateExpression });
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x00084AFE File Offset: 0x00082CFE
		private SqlExpression CreateDateYear(IInvocationExpression invocation)
		{
			return this.GetDateYearExpression(base.CreateScalarExpression(invocation.Arguments[0]));
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x00084B18 File Offset: 0x00082D18
		private SqlExpression GetDateYearExpression(SqlExpression dateExpression)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.YearSqlString, SqlLanguageStrings.ExtractSqlString), new SqlExpression[] { dateExpression });
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x00084B38 File Offset: 0x00082D38
		protected override SqlExpression CreateToDateTime(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			TypeValue type = base.GetType(expression);
			SqlExpression sqlExpression = base.CreateScalarExpression(expression);
			if (type.TypeKind == ValueKind.Number)
			{
				return this.ConvertNumberToDateTime(sqlExpression);
			}
			return new CastCall
			{
				Type = SqlDataType.Timestamp,
				Expression = sqlExpression
			};
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x00084B88 File Offset: 0x00082D88
		protected override SqlExpression CreateToDateTimeWithTimeZone(IInvocationExpression invocation)
		{
			return new CastCall
			{
				Type = new SqlDataType(TypeValue.DateTimeZone, SqlLanguageStrings.TimestampWithTimeZoneSqlString),
				Expression = base.CreateScalarExpression(invocation.Arguments[0])
			};
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x00084BBC File Offset: 0x00082DBC
		protected override SqlExpression ConvertDateToNumber(SqlExpression expression)
		{
			SqlExpression sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(expression, base.BaseOADateTime);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DaySqlString, SqlLanguageStrings.ExtractSqlString), new SqlExpression[] { sqlExpression });
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x00084BF4 File Offset: 0x00082DF4
		protected override SqlExpression ConvertDateTimeToNumber(SqlExpression expression)
		{
			SqlExpression sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(expression, base.BaseOADateTime);
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DaySqlString, SqlLanguageStrings.ExtractSqlString), new SqlExpression[] { sqlExpression });
			SqlExpression sqlExpression3 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.HourSqlString, SqlLanguageStrings.ExtractSqlString), new SqlExpression[] { sqlExpression }), SqlConstant.SecondsPerHour);
			SqlExpression sqlExpression4 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.MinuteSqlString, SqlLanguageStrings.ExtractSqlString), new SqlExpression[] { sqlExpression }), SqlConstant.SecondsPerMinute);
			SqlExpression sqlExpression5 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.SecondSqlString, SqlLanguageStrings.ExtractSqlString), new SqlExpression[] { sqlExpression });
			SqlExpression sqlExpression6 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression3, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression4, sqlExpression5)), SqlConstant.SecondsPerDay);
			SqlExpression sqlExpression7 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(base.CreateOADateTimeSignExpression(expression), sqlExpression6);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression2, sqlExpression7);
		}

		// Token: 0x06002BC0 RID: 11200 RVA: 0x00084CD4 File Offset: 0x00082ED4
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
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.ToTimestampSqlString), new SqlExpression[] { SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.TruncSqlString), new SqlExpression[] { sqlExpression }) });
			}
			throw new InvalidOperationException(Strings.UnreachableCodePath);
		}

		// Token: 0x06002BC1 RID: 11201 RVA: 0x00084D5C File Offset: 0x00082F5C
		protected override SqlExpression CreateDurationTotalDays(IInvocationExpression invocation)
		{
			IBinaryExpression binaryExpression;
			if (DbAstCreator.TryAsBinaryExpression(invocation.Arguments[0], out binaryExpression))
			{
				SqlExpression sqlExpression = base.CreateScalarExpression(binaryExpression.Left);
				SqlExpression sqlExpression2 = base.CreateScalarExpression(binaryExpression.Right);
				sqlExpression = new CastCall
				{
					Type = SqlDataType.Timestamp,
					Expression = sqlExpression
				};
				sqlExpression2 = new CastCall
				{
					Type = SqlDataType.Timestamp,
					Expression = sqlExpression2
				};
				return this.ConvertDurationToNumber(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(sqlExpression, sqlExpression2));
			}
			throw new InvalidOperationException(Strings.UnreachableCodePath);
		}

		// Token: 0x06002BC2 RID: 11202 RVA: 0x00080CC3 File Offset: 0x0007EEC3
		private SqlExpression ConvertDurationToNumber(SqlExpression expression)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DaySqlString, SqlLanguageStrings.ExtractSqlString), new SqlExpression[] { expression });
		}

		// Token: 0x040012FD RID: 4861
		private static readonly DateTime BaseDateTime = new DateTime(2000, 1, 1);

		// Token: 0x040012FE RID: 4862
		private static readonly SqlConstant DateTimeAggregateBase = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(OracleAstCreator.BaseDateTime);

		// Token: 0x040012FF RID: 4863
		private readonly Dictionary<string, string> customTypeColumns = new Dictionary<string, string>();

		// Token: 0x04001300 RID: 4864
		private const string DateType = "date";

		// Token: 0x04001301 RID: 4865
		private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

		// Token: 0x04001302 RID: 4866
		private const string OracleDateFormat = "YYYY-MM-DD HH24:MI:SS";

		// Token: 0x0200055A RID: 1370
		private sealed class OracleOutputClause : OutputClause
		{
			// Token: 0x06002BC5 RID: 11205 RVA: 0x00084E2C File Offset: 0x0008302C
			public OracleOutputClause(List<SelectItem> columnList)
				: base(columnList)
			{
				this.outputParameters = new Alias[columnList.Count];
				for (int i = 0; i < this.outputParameters.Count; i++)
				{
					this.outputParameters[i] = Alias.NewNativeAlias(":param" + i.ToString());
				}
			}

			// Token: 0x17001048 RID: 4168
			// (get) Token: 0x06002BC6 RID: 11206 RVA: 0x00084E89 File Offset: 0x00083089
			public override IList<Alias> OutputParameters
			{
				get
				{
					return this.outputParameters;
				}
			}

			// Token: 0x06002BC7 RID: 11207 RVA: 0x00084E91 File Offset: 0x00083091
			public override void WriteSuffixScript(ScriptWriter writer)
			{
				writer.WriteLine();
				writer.WriteSpaceAfter(SqlLanguageStrings.ReturningSqlString);
				base.WriteColumns(writer);
				writer.WriteSpace();
				writer.WriteSpaceAfter(SqlLanguageStrings.IntoSqlString);
				base.WriteOutputParameters(writer);
			}

			// Token: 0x04001303 RID: 4867
			private IList<Alias> outputParameters;
		}
	}
}
