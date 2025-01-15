using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OleDb;
using Microsoft.OleDb.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x02000628 RID: 1576
	internal class OdbcQueryExpressionVisitor
	{
		// Token: 0x060031C2 RID: 12738 RVA: 0x000990EC File Offset: 0x000972EC
		public OdbcQueryExpressionVisitor(OdbcDataSource dataSource, IList<SelectItem> selectItems, OdbcQueryColumnInfo[] columns, bool allowAggregates, bool softNumbers, bool tolerateConcatOverflow, int[] groupKey = null)
		{
			this.dataSource = dataSource;
			this.columns = columns;
			this.selectItems = selectItems;
			this.allowAggregates = allowAggregates;
			this.softNumbers = softNumbers;
			this.tolerateConcatOverflow = tolerateConcatOverflow;
			this.groupKey = groupKey;
			this.trace = new OdbcFoldingTracingService(dataSource);
			this.captures = new QueryExpression[2];
			Value value;
			this.useBetterEquality = dataSource.Host.GetConfigurationProperty("MashupFlight_BetterOdbcEquality", false) || (this.dataSource.Options.TryGetOptionalCapability("MashupFlight_BetterOdbcEquality", out value) && value.IsLogical && value.AsBoolean);
		}

		// Token: 0x17001248 RID: 4680
		// (get) Token: 0x060031C3 RID: 12739 RVA: 0x00099192 File Offset: 0x00097392
		protected int[] GroupKeys
		{
			get
			{
				if (!this.allowAggregates)
				{
					return null;
				}
				return this.groupKey;
			}
		}

		// Token: 0x17001249 RID: 4681
		// (get) Token: 0x060031C4 RID: 12740 RVA: 0x000991A4 File Offset: 0x000973A4
		protected OdbcQueryColumnInfo[] Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x1700124A RID: 4682
		// (get) Token: 0x060031C5 RID: 12741 RVA: 0x000991AC File Offset: 0x000973AC
		private Dictionary<FunctionValue, Func<InvocationQueryExpression, OdbcSqlExpression>> FunctionVisitors
		{
			get
			{
				if (this.functionVisitors == null)
				{
					this.functionVisitors = this.GetFunctionVisitors();
				}
				return this.functionVisitors;
			}
		}

		// Token: 0x1700124B RID: 4683
		// (get) Token: 0x060031C6 RID: 12742 RVA: 0x000991C8 File Offset: 0x000973C8
		private string LikeEscapeCharacter
		{
			get
			{
				if (this.likeEscapeCharacter == null)
				{
					using (this.trace.NewScope("LikeEscapeCharacter"))
					{
						foreach (string text in OdbcQueryExpressionVisitor.likeEscapeCharacterCandidates)
						{
							if (!this.dataSource.Info.StringLiteralEscapeCharacters.ContainsKey(text))
							{
								this.likeEscapeCharacter = text;
								return this.likeEscapeCharacter;
							}
						}
						throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<string[]>, FoldingWarnings.StringFormatter<IDictionary<string, string>>>>(FoldingWarnings.NoLikeEscapeCharacter(OdbcQueryExpressionVisitor.likeEscapeCharacterCandidates, this.dataSource.Info.StringLiteralEscapeCharacters));
					}
				}
				return this.likeEscapeCharacter;
			}
		}

		// Token: 0x1700124C RID: 4684
		// (get) Token: 0x060031C7 RID: 12743 RVA: 0x0009927C File Offset: 0x0009747C
		private bool SupportsParameters
		{
			get
			{
				if (this.supportsParameters == null)
				{
					this.dataSource.ConnectForMetadata(delegate(IOdbcConnection connection)
					{
						this.supportsParameters = new bool?(connection.GetFunctions(Odbc32.SQL_API.SQL_API_SQLBINDPARAMETER));
					});
				}
				return this.supportsParameters.Value;
			}
		}

		// Token: 0x1700124D RID: 4685
		// (get) Token: 0x060031C8 RID: 12744 RVA: 0x000992AD File Offset: 0x000974AD
		private OdbcTypeInfo BigIntType
		{
			get
			{
				if (this.bigIntType == null)
				{
					this.bigIntType = this.GetTypeInfo(Odbc32.SQL_TYPE.BIGINT);
				}
				return this.bigIntType;
			}
		}

		// Token: 0x1700124E RID: 4686
		// (get) Token: 0x060031C9 RID: 12745 RVA: 0x000992CC File Offset: 0x000974CC
		protected OdbcTypeInfo IntegerType
		{
			get
			{
				if (this.integerType == null)
				{
					OdbcTypeInfo odbcTypeInfo;
					if (this.TryGetTypeInfo(Odbc32.SQL_TYPE.INTEGER, out odbcTypeInfo))
					{
						this.integerType = odbcTypeInfo;
					}
					else
					{
						this.integerType = this.GetTypeInfo(Odbc32.SQL_TYPE.BIGINT);
					}
				}
				return this.integerType;
			}
		}

		// Token: 0x1700124F RID: 4687
		// (get) Token: 0x060031CA RID: 12746 RVA: 0x0009930C File Offset: 0x0009750C
		protected OdbcTypeInfo DoubleType
		{
			get
			{
				if (this.doubleType == null)
				{
					using (this.trace.NewScope("DoubleType"))
					{
						if (!this.TryGetDoubleType(out this.doubleType))
						{
							throw this.trace.NewFoldingFailureException(null);
						}
					}
				}
				return this.doubleType;
			}
		}

		// Token: 0x17001250 RID: 4688
		// (get) Token: 0x060031CB RID: 12747 RVA: 0x00099370 File Offset: 0x00097570
		private OdbcTypeInfo DateType
		{
			get
			{
				if (this.dateType == null)
				{
					this.dateType = this.GetTypeInfo(Odbc32.SQL_TYPE.TYPE_DATE);
				}
				return this.dateType;
			}
		}

		// Token: 0x17001251 RID: 4689
		// (get) Token: 0x060031CC RID: 12748 RVA: 0x00099390 File Offset: 0x00097590
		private OdbcDerivedColumnTypeInfo DurationType
		{
			get
			{
				if (this.durationType == null)
				{
					this.durationType = this.NewColumnType(Odbc32.SQL_TYPE.BIGINT, true).AddColumnConversion(new ColumnConversion(typeof(TimeSpan), delegate(object ticks, Column column)
					{
						column.AddValue(new TimeSpan((long)ticks));
					}), TypeValue.Duration);
				}
				return this.durationType;
			}
		}

		// Token: 0x17001252 RID: 4690
		// (get) Token: 0x060031CD RID: 12749 RVA: 0x000993F2 File Offset: 0x000975F2
		protected OdbcTypeInfo TimestampType
		{
			get
			{
				if (this.timestampType == null)
				{
					this.timestampType = this.GetTypeInfo(Odbc32.SQL_TYPE.TYPE_TIMESTAMP);
				}
				return this.timestampType;
			}
		}

		// Token: 0x17001253 RID: 4691
		// (get) Token: 0x060031CE RID: 12750 RVA: 0x00099410 File Offset: 0x00097610
		protected OdbcTypeInfo TimeType
		{
			get
			{
				if (this.timeType == null)
				{
					this.timeType = this.GetTypeInfo(Odbc32.SQL_TYPE.TYPE_TIME);
				}
				return this.timeType;
			}
		}

		// Token: 0x17001254 RID: 4692
		// (get) Token: 0x060031CF RID: 12751 RVA: 0x0009942E File Offset: 0x0009762E
		private OdbcTypeInfo VarcharType
		{
			get
			{
				if (this.varcharType == null)
				{
					this.varcharType = this.GetTypeInfo(Odbc32.SQL_TYPE.VARCHAR);
				}
				return this.varcharType;
			}
		}

		// Token: 0x17001255 RID: 4693
		// (get) Token: 0x060031D0 RID: 12752 RVA: 0x0009944C File Offset: 0x0009764C
		private OdbcTypeInfo LogicalType
		{
			get
			{
				if (this.logicalType == null)
				{
					this.logicalType = this.GetTypeInfo(Odbc32.SQL_TYPE.BIT);
				}
				return this.logicalType;
			}
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x0009946C File Offset: 0x0009766C
		public OdbcConditionExpression GetCondition(QueryExpression expression)
		{
			OdbcSqlExpression odbcSqlExpression = this.Visit(expression);
			if (odbcSqlExpression.Kind == OdbcSqlExpressionKind.Condition)
			{
				return odbcSqlExpression.AsCondition;
			}
			return this.ConvertToCondition(odbcSqlExpression);
		}

		// Token: 0x060031D2 RID: 12754 RVA: 0x00099498 File Offset: 0x00097698
		public OdbcSqlExpression Visit(QueryExpression expression)
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

		// Token: 0x060031D3 RID: 12755 RVA: 0x00099530 File Offset: 0x00097730
		protected virtual SqlExpression VisitConstant(OdbcTypeInfo typeInfo, TextValue value)
		{
			SqlExpression sqlExpression2;
			using (this.trace.NewScope("VisitConstant/TextValue"))
			{
				SqlExpression sqlExpression;
				if (this.TryGetStringLiteral(typeInfo, value, out sqlExpression))
				{
					sqlExpression2 = sqlExpression;
				}
				else
				{
					sqlExpression2 = this.Parameter(typeInfo, value.AsString);
				}
			}
			return sqlExpression2;
		}

		// Token: 0x060031D4 RID: 12756 RVA: 0x0009958C File Offset: 0x0009778C
		protected virtual SqlExpression VisitConstant(OdbcTypeInfo typeInfo, BinaryValue value)
		{
			return this.Parameter(typeInfo, value.AsBytes);
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x0009959C File Offset: 0x0009779C
		protected virtual SqlExpression VisitConstant(OdbcTypeInfo typeInfo, DateValue value)
		{
			if (this.dataSource.Info.SupportsOdbcDateLiterals)
			{
				string text = value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
				return new OdbcQueryExpressionVisitor.OdbcDateTimeLiteral(OdbcQueryExpressionVisitor.OdbcDateTimeLiteral.d, text);
			}
			return this.Parameter(typeInfo, value.AsClrDateTime);
		}

		// Token: 0x060031D6 RID: 12758 RVA: 0x000995EC File Offset: 0x000977EC
		protected virtual SqlExpression VisitConstant(OdbcTypeInfo typeInfo, DateTimeValue value)
		{
			if (this.dataSource.Info.SupportsOdbcTimestampLiterals)
			{
				string text = value.ToString("yyyy-MM-dd HH:mm:ss.FFFFFFF", CultureInfo.InvariantCulture);
				return new OdbcQueryExpressionVisitor.OdbcDateTimeLiteral(OdbcQueryExpressionVisitor.OdbcDateTimeLiteral.ts, text);
			}
			return this.Parameter(typeInfo, value.AsClrDateTime);
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x0009963C File Offset: 0x0009783C
		protected virtual SqlExpression VisitConstant(OdbcTypeInfo typeInfo, TimeValue value)
		{
			if (this.dataSource.Info.SupportsOdbcTimeLiterals)
			{
				string text = value.ToString("HH:mm:ss.FFFFFFF", CultureInfo.InvariantCulture);
				return new OdbcQueryExpressionVisitor.OdbcDateTimeLiteral(OdbcQueryExpressionVisitor.OdbcDateTimeLiteral.t, text);
			}
			return this.Parameter(typeInfo, value.AsClrTimeSpan);
		}

		// Token: 0x060031D8 RID: 12760 RVA: 0x0009968C File Offset: 0x0009788C
		protected virtual SqlExpression VisitConstant(OdbcTypeInfo typeInfo, DurationValue duration)
		{
			SqlExpression sqlExpression2;
			using (this.trace.NewScope("VisitConstant/DurationValue"))
			{
				SqlExpression sqlExpression;
				if (this.TryGetLiteral(duration.AsTimeSpan.Ticks, out sqlExpression))
				{
					sqlExpression2 = sqlExpression;
				}
				else
				{
					sqlExpression2 = this.Parameter(typeInfo, duration.AsTimeSpan.Ticks);
				}
			}
			return sqlExpression2;
		}

		// Token: 0x060031D9 RID: 12761 RVA: 0x00099700 File Offset: 0x00097900
		protected virtual SqlExpression VisitConstant(OdbcTypeInfo typeInfo, LogicalValue value)
		{
			SqlExpression sqlExpression2;
			using (this.trace.NewScope("VisitConstant/LogicalValue"))
			{
				SqlExpression sqlExpression;
				if (this.TryGetLiteral((value.AsBoolean > false) ? 1L : 0L, out sqlExpression))
				{
					sqlExpression2 = sqlExpression;
				}
				else
				{
					sqlExpression2 = this.Parameter(typeInfo, value.AsBoolean);
				}
			}
			return sqlExpression2;
		}

		// Token: 0x060031DA RID: 12762 RVA: 0x00099768 File Offset: 0x00097968
		protected virtual SqlExpression VisitConstantInt32(OdbcTypeInfo typeInfo, NumberValue value)
		{
			SqlExpression sqlExpression2;
			using (this.trace.NewScope("VisitConstant/Int32"))
			{
				SqlExpression sqlExpression;
				if (this.TryGetLiteral((long)value.AsInteger32, out sqlExpression))
				{
					sqlExpression2 = sqlExpression;
				}
				else
				{
					sqlExpression2 = this.Parameter(typeInfo, value.AsInteger32);
				}
			}
			return sqlExpression2;
		}

		// Token: 0x060031DB RID: 12763 RVA: 0x000997CC File Offset: 0x000979CC
		protected virtual SqlExpression VisitConstantInt64(OdbcTypeInfo typeInfo, NumberValue value)
		{
			SqlExpression sqlExpression2;
			using (this.trace.NewScope("VisitConstant/Int64"))
			{
				SqlExpression sqlExpression;
				if (this.TryGetLiteral(value.AsInteger64, out sqlExpression))
				{
					sqlExpression2 = sqlExpression;
				}
				else
				{
					sqlExpression2 = this.Parameter(typeInfo, value.AsInteger64);
				}
			}
			return sqlExpression2;
		}

		// Token: 0x060031DC RID: 12764 RVA: 0x00099830 File Offset: 0x00097A30
		protected virtual SqlExpression VisitConstantDecimal(OdbcTypeInfo typeInfo, NumberValue value)
		{
			SqlExpression sqlExpression2;
			using (this.trace.NewScope("VisitConstant/Decimal"))
			{
				SqlExpression sqlExpression;
				if (this.TryGetLiteral(value.AsDecimal, out sqlExpression))
				{
					sqlExpression2 = sqlExpression;
				}
				else
				{
					sqlExpression2 = this.Parameter(typeInfo, value.AsDecimal);
				}
			}
			return sqlExpression2;
		}

		// Token: 0x060031DD RID: 12765 RVA: 0x00099894 File Offset: 0x00097A94
		protected virtual SqlExpression VisitConstantDouble(OdbcTypeInfo typeInfo, NumberValue value)
		{
			SqlExpression sqlExpression2;
			using (this.trace.NewScope("VisitConstant/Double"))
			{
				double asDouble = value.AsDouble;
				if (double.IsNaN(asDouble) || double.IsInfinity(asDouble))
				{
					throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<Value>, string>>(FoldingWarnings.InvalidValue(value, "invalid number value"));
				}
				SqlExpression sqlExpression;
				if (this.TryGetLiteral(asDouble, out sqlExpression))
				{
					sqlExpression2 = sqlExpression;
				}
				else
				{
					sqlExpression2 = this.Parameter(typeInfo, asDouble);
				}
			}
			return sqlExpression2;
		}

		// Token: 0x060031DE RID: 12766 RVA: 0x0009991C File Offset: 0x00097B1C
		protected bool TryGetType(TypeValue type, out OdbcTypeInfo typeInfo)
		{
			string nativeTypeName = type.Facets.NativeTypeName;
			if (nativeTypeName != null)
			{
				typeInfo = this.GetTypeInfo(nativeTypeName);
				return true;
			}
			switch (type.TypeKind)
			{
			case ValueKind.Number:
				if (!this.TryGetNumberTypeInfo(type, out typeInfo))
				{
					typeInfo = this.DoubleType;
				}
				return true;
			case ValueKind.Logical:
				typeInfo = this.LogicalType;
				return true;
			case ValueKind.Text:
				typeInfo = this.VarcharType;
				return true;
			default:
				typeInfo = null;
				return false;
			}
		}

		// Token: 0x060031DF RID: 12767 RVA: 0x0009998C File Offset: 0x00097B8C
		private bool TryGetStringLiteral(OdbcTypeInfo typeInfo, TextValue value, out SqlExpression expression)
		{
			if (this.trace.WhenNot<FoldingWarnings.FoldingWarning<string>>(this.dataSource.Info.SupportsStringLiterals, FoldingWarnings.SqlCapabilities("SupportsStringLiterals")) && this.trace.WhenNot<FoldingWarnings.FoldingWarning<string, string>>(!string.IsNullOrEmpty(typeInfo.LiteralPrefix), OdbcFoldingWarnings.DataTypeNotCompatible(typeInfo.Name, "LiteralPrefix doesn't have value")) && this.trace.WhenNot<FoldingWarnings.FoldingWarning<string, string>>(!string.IsNullOrEmpty(typeInfo.LiteralSuffix), OdbcFoldingWarnings.DataTypeNotCompatible(typeInfo.Name, "LiteralSuffix doesn't have value")))
			{
				string text = value.AsString;
				if (!this.dataSource.Info.StringLiteralEscapeCharacters.ContainsKey(typeInfo.LiteralSuffix))
				{
					this.dataSource.Info.StringLiteralEscapeCharacters.Add(typeInfo.LiteralSuffix, typeInfo.LiteralSuffix + typeInfo.LiteralSuffix);
				}
				foreach (KeyValuePair<string, string> keyValuePair in this.dataSource.Info.StringLiteralEscapeCharacters)
				{
					text = OdbcQueryExpressionVisitor.Escape(text, keyValuePair.Key, keyValuePair.Value);
				}
				string text2 = typeInfo.LiteralPrefix + text + typeInfo.LiteralSuffix;
				expression = new LiteralExpression(new ConstantSqlString(text2));
				return true;
			}
			expression = null;
			return false;
		}

		// Token: 0x060031E0 RID: 12768 RVA: 0x00099AF0 File Offset: 0x00097CF0
		private bool TryGetLiteral(long value, out SqlExpression expression)
		{
			if (this.trace.WhenNot<FoldingWarnings.FoldingWarning<string>>(this.dataSource.Info.SupportsNumericLiterals, FoldingWarnings.SqlCapabilities("SupportsNumericLiterals")))
			{
				string text = value.ToString(CultureInfo.InvariantCulture);
				expression = new SqlConstant(ConstantType.Integer, text);
				return true;
			}
			expression = null;
			return false;
		}

		// Token: 0x060031E1 RID: 12769 RVA: 0x00099B40 File Offset: 0x00097D40
		private bool TryGetLiteral(decimal value, out SqlExpression expression)
		{
			if (this.trace.WhenNot<FoldingWarnings.FoldingWarning<string>>(this.dataSource.Info.SupportsNumericLiterals, FoldingWarnings.SqlCapabilities("SupportsNumericLiterals")))
			{
				string text = value.ToString(CultureInfo.InvariantCulture);
				if (!text.Contains("."))
				{
					text += ".";
				}
				expression = new SqlConstant(ConstantType.Decimal, text);
				return true;
			}
			expression = null;
			return false;
		}

		// Token: 0x060031E2 RID: 12770 RVA: 0x00099BAC File Offset: 0x00097DAC
		private bool TryGetLiteral(double value, out SqlExpression expression)
		{
			if (this.trace.WhenNot<FoldingWarnings.FoldingWarning<string>>(this.dataSource.Info.SupportsNumericLiterals, FoldingWarnings.SqlCapabilities("SupportsNumericLiterals")))
			{
				expression = new SqlConstant(ConstantType.Float, value.ToString("E15", CultureInfo.InvariantCulture));
				return true;
			}
			expression = null;
			return false;
		}

		// Token: 0x060031E3 RID: 12771 RVA: 0x00099C00 File Offset: 0x00097E00
		protected SqlExpression VisitConvert(OdbcTypeInfo fromType, OdbcTypeInfo toType, SqlExpression expression)
		{
			SqlExpression sqlExpression2;
			using (this.trace.NewScope("VisitConvert"))
			{
				SqlExpression sqlExpression;
				if (!this.TryVisitConvert(fromType, toType, expression, out sqlExpression))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				sqlExpression2 = sqlExpression;
			}
			return sqlExpression2;
		}

		// Token: 0x060031E4 RID: 12772 RVA: 0x00099C58 File Offset: 0x00097E58
		protected virtual bool TryVisitConvert(OdbcTypeInfo fromType, OdbcTypeInfo toType, SqlExpression expression, out SqlExpression convertedExpression)
		{
			if (this.trace.When<FoldingWarnings.FoldingWarning<string, string, FoldingWarnings.StringFormatter<IScriptable, SqlSettings>, Odbc32.SQL_TYPE, Odbc32.SQL_TYPE>>(!this.dataSource.Info.SupportsConversion(fromType.SqlType, toType.SqlType), OdbcFoldingWarnings.Conversion(this.dataSource.SqlSettings, fromType, toType, expression)))
			{
				convertedExpression = null;
				return false;
			}
			OdbcTypeMap odbcTypeMap = OdbcTypeMap.FromSqlType(toType.SqlType);
			convertedExpression = this.CallConvertOrCast(expression, odbcTypeMap);
			return true;
		}

		// Token: 0x060031E5 RID: 12773 RVA: 0x00099CC4 File Offset: 0x00097EC4
		public SqlExpression CallConvertOrCast(SqlExpression from, OdbcTypeMap toType)
		{
			RecordValue recordValue;
			if (this.GetDefaultTypeParameters().TryGetValue(toType.SqlType, out recordValue))
			{
				if (this.DataSourceSupportsCast())
				{
					return this.CallCast(from, toType, recordValue.AsRecord);
				}
				throw this.trace.NewFoldingFailureException("Not implemented");
			}
			else
			{
				if (this.DataSourceSupportsConvert())
				{
					return OdbcQueryExpressionVisitor.CallConvert(from, toType);
				}
				if (this.DataSourceSupportsCast())
				{
					return this.CallCast(from, toType, null);
				}
				throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, Odbc32.SQL_INFO>>(OdbcFoldingWarnings.SqlGetInfo<string>(Odbc32.SQL_INFO.SQL_CONVERT_FUNCTIONS, "SQL_FN_CVT_CONVERT or SQL_FN_CVT_CAST"));
			}
		}

		// Token: 0x060031E6 RID: 12774 RVA: 0x00099D47 File Offset: 0x00097F47
		private bool DataSourceSupportsCast()
		{
			return (this.dataSource.Info.SupportedConvertFunctions & Odbc32.SQL_FN_CVT.SQL_FN_CVT_CAST) > Odbc32.SQL_FN_CVT.None;
		}

		// Token: 0x060031E7 RID: 12775 RVA: 0x00099D5E File Offset: 0x00097F5E
		private bool DataSourceSupportsConvert()
		{
			return (this.dataSource.Info.SupportedConvertFunctions & Odbc32.SQL_FN_CVT.SQL_FN_CVT_CONVERT) > Odbc32.SQL_FN_CVT.None;
		}

		// Token: 0x060031E8 RID: 12776 RVA: 0x00099D75 File Offset: 0x00097F75
		private static SqlExpression CallConvert(SqlExpression from, OdbcTypeMap toType)
		{
			return OdbcQueryExpressionVisitor.Call(new OdbcScalarFunctionReference(SqlLanguageStrings.ConvertSqlString), new SqlExpression[]
			{
				from,
				new ColumnTypeExpression(new SqlDataType(toType.TypeValue, new ConstantSqlString(toType.Token)))
			});
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x00099DB0 File Offset: 0x00097FB0
		private SqlExpression CallCast(SqlExpression from, OdbcTypeMap toType, RecordValue precision = null)
		{
			OdbcTypeInfo odbcTypeInfo;
			if (this.dataSource.Types.TryGetType(toType.SqlType, out odbcTypeInfo))
			{
				TypeValue typeValue = toType.TypeValue;
				byte? b = OdbcQueryExpressionVisitor.GetByteValue(precision, "Precision");
				int? num = ((b != null) ? new int?((int)b.GetValueOrDefault()) : null);
				if (num != null)
				{
					b = OdbcQueryExpressionVisitor.GetByteValue(precision, "Scale");
					int? num2 = ((b != null) ? new int?((int)b.GetValueOrDefault()) : null);
					typeValue = typeValue.NewFacets(TypeFacets.NewNumeric(new int?(10), num, num2, null));
				}
				return new CastCall
				{
					Expression = from,
					Type = new SqlDataType(typeValue, new ConstantSqlString(odbcTypeInfo.Name))
				};
			}
			throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<Odbc32.SQL_TYPE>>(OdbcFoldingWarnings.DataTypeNotFound(toType.SqlType));
		}

		// Token: 0x060031EA RID: 12778 RVA: 0x00099E98 File Offset: 0x00098098
		private static byte? GetByteValue(RecordValue record, string key)
		{
			Value value;
			if (record != null && record.TryGetValue(key, out value) && value.IsNumber)
			{
				return new byte?(value.AsNumber.ToByte());
			}
			return null;
		}

		// Token: 0x060031EB RID: 12779 RVA: 0x00099ED8 File Offset: 0x000980D8
		private Dictionary<Odbc32.SQL_TYPE, RecordValue> GetDefaultTypeParameters()
		{
			Dictionary<Odbc32.SQL_TYPE, RecordValue> dictionary = new Dictionary<Odbc32.SQL_TYPE, RecordValue>();
			foreach (NamedValue namedValue in this.dataSource.Info.DefaultTypeParameters.GetFields())
			{
				OdbcTypeInfo odbcTypeInfo;
				if (this.dataSource.Types.TryGetType(namedValue.Key, out odbcTypeInfo))
				{
					dictionary.Add(odbcTypeInfo.SqlType, namedValue.Value.AsRecord);
				}
			}
			return dictionary;
		}

		// Token: 0x060031EC RID: 12780 RVA: 0x00099F74 File Offset: 0x00098174
		protected virtual bool TryGetMatchingType(OdbcTypeMatchCriteria criteria, out OdbcTypeInfo typeInfo)
		{
			if (this.TryGetTypeInfo(criteria.SqlType, out typeInfo) && typeInfo.ColumnSize != null)
			{
				int size = criteria.Size;
				int? columnSize = typeInfo.ColumnSize;
				if ((size <= columnSize.GetValueOrDefault()) & (columnSize != null))
				{
					return typeInfo.Searchable == Odbc32.SQL_SEARCHABLE.SEARCHABLE || typeInfo.Searchable == Odbc32.SQL_SEARCHABLE.ALL_EXCEPT_LIKE;
				}
			}
			return false;
		}

		// Token: 0x060031ED RID: 12781 RVA: 0x00099FE0 File Offset: 0x000981E0
		private OdbcSqlExpression VisitBinary(BinaryQueryExpression binaryExpression)
		{
			OdbcSqlExpression odbcSqlExpression2;
			using (this.trace.NewScope("VisitBinary"))
			{
				OdbcSqlExpression odbcSqlExpression;
				if (this.dataSource.Options.TryRecoverDateDiff && binaryExpression.Operator == BinaryOperator2.Subtract && this.trace.DataSourceInfo.Supports(Odbc32.SQL_FN_TD.SQL_FN_TD_TIMESTAMPDIFF) && this.TryRecoverDateDiffFromBinary(binaryExpression, out odbcSqlExpression))
				{
					odbcSqlExpression2 = odbcSqlExpression;
				}
				else
				{
					OdbcSqlExpression odbcSqlExpression3 = this.Visit(binaryExpression.Left);
					OdbcSqlExpression odbcSqlExpression4 = this.Visit(binaryExpression.Right);
					TypeValue typeValue;
					if (this.trace.When<FoldingWarnings.FoldingWarning<BinaryOperator2, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>>>(!this.TryCheckType(OperatorTypeflowModels.Binary(binaryExpression.Operator, odbcSqlExpression3.TypeValue, odbcSqlExpression4.TypeValue), out typeValue), OdbcFoldingWarnings.InvalidBinaryOperator(this.dataSource.SqlSettings, binaryExpression.Operator, odbcSqlExpression3, odbcSqlExpression4)))
					{
						throw this.trace.NewFoldingFailureException(null);
					}
					if (typeValue.TypeKind != ValueKind.Null)
					{
						switch (binaryExpression.Operator)
						{
						case BinaryOperator2.Add:
						case BinaryOperator2.Subtract:
						case BinaryOperator2.Multiply:
						case BinaryOperator2.Divide:
							return this.VisitBinaryScalarOperation(odbcSqlExpression3.AsScalar, binaryExpression.Operator, odbcSqlExpression4.AsScalar);
						case BinaryOperator2.GreaterThan:
						case BinaryOperator2.LessThan:
						case BinaryOperator2.GreaterThanOrEquals:
						case BinaryOperator2.LessThanOrEquals:
							return new OdbcConditionExpression(this.VisitLogicalOperation(odbcSqlExpression3.AsScalar, binaryExpression.Operator, odbcSqlExpression4.AsScalar));
						case BinaryOperator2.Equals:
							return new OdbcConditionExpression(this.VisitEquals(odbcSqlExpression3.AsScalar, odbcSqlExpression4.AsScalar, Precision.Double, false));
						case BinaryOperator2.NotEquals:
							return new OdbcConditionExpression(this.VisitNotEquals(odbcSqlExpression3, odbcSqlExpression4, Precision.Double));
						case BinaryOperator2.And:
							return new OdbcConditionExpression(this.VisitCondition(this.ConvertToCondition(odbcSqlExpression3), ConditionOperator.And, this.ConvertToCondition(odbcSqlExpression4)));
						case BinaryOperator2.Or:
							return new OdbcConditionExpression(this.VisitCondition(this.ConvertToCondition(odbcSqlExpression3), ConditionOperator.Or, this.ConvertToCondition(odbcSqlExpression4)));
						case BinaryOperator2.Concatenate:
							return this.VisitConcat(odbcSqlExpression3.AsScalar, odbcSqlExpression4.AsScalar);
						case BinaryOperator2.Coalesce:
							return this.VisitCoalesce(odbcSqlExpression3.AsScalar, odbcSqlExpression4.AsScalar);
						}
						throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<BinaryOperator2, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>>>(OdbcFoldingWarnings.InvalidBinaryOperator(this.dataSource.SqlSettings, binaryExpression.Operator, odbcSqlExpression3, odbcSqlExpression4));
					}
					odbcSqlExpression2 = OdbcQueryExpressionVisitor.nullTypedExpression;
				}
			}
			return odbcSqlExpression2;
		}

		// Token: 0x060031EE RID: 12782 RVA: 0x0009A258 File Offset: 0x00098458
		protected OdbcScalarExpression Convert(OdbcTypeInfo typeInfo, OdbcScalarExpression expression)
		{
			OdbcScalarExpression odbcScalarExpression2;
			using (this.trace.NewScope("Convert"))
			{
				OdbcScalarExpression odbcScalarExpression;
				if (!this.TryConvert(typeInfo, expression, out odbcScalarExpression))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				odbcScalarExpression2 = odbcScalarExpression;
			}
			return odbcScalarExpression2;
		}

		// Token: 0x060031EF RID: 12783 RVA: 0x0009A2B0 File Offset: 0x000984B0
		private bool TryConvert(Odbc32.SQL_TYPE sqlType, OdbcScalarExpression expression, out OdbcScalarExpression converted)
		{
			OdbcTypeInfo odbcTypeInfo;
			if (this.TryGetTypeInfo(sqlType, out odbcTypeInfo) && this.TryConvert(odbcTypeInfo, expression, out converted))
			{
				return true;
			}
			converted = null;
			return false;
		}

		// Token: 0x060031F0 RID: 12784 RVA: 0x0009A2DC File Offset: 0x000984DC
		protected bool TryConvert(OdbcTypeInfo typeInfo, OdbcScalarExpression expression, out OdbcScalarExpression convertedExpression)
		{
			if (typeInfo.Equals(expression.TypeInfo.DataSourceType))
			{
				convertedExpression = expression;
				return true;
			}
			Value value;
			if (OdbcTypeMap.FromSqlType(typeInfo.SqlType).TypeValue.TypeKind == expression.TypeValue.TypeKind && this.TryGetValue(expression.Expression, out value) && expression.Expression is ConstantAnnotationExpression)
			{
				if (this.TryConvertNumberConstant(typeInfo, value, out convertedExpression))
				{
					return true;
				}
				SqlExpression sqlExpression = null;
				if (expression.TypeValue.TypeKind == ValueKind.Text)
				{
					sqlExpression = this.VisitConstant(typeInfo, value.AsText);
				}
				if (expression.TypeValue.TypeKind == ValueKind.Binary)
				{
					sqlExpression = this.VisitConstant(typeInfo, value.AsBinary);
				}
				if (sqlExpression != null)
				{
					convertedExpression = new OdbcScalarExpression(new OdbcDerivedColumnTypeInfo(typeInfo, expression.TypeInfo.IsNullable, expression.TypeInfo.ColumnSize, null), new ConstantAnnotationExpression(value, sqlExpression));
					return true;
				}
			}
			SqlExpression sqlExpression2;
			if (this.TryVisitConvert(expression.TypeInfo.DataSourceType, typeInfo, expression.Expression, out sqlExpression2))
			{
				convertedExpression = new OdbcScalarExpression(new OdbcDerivedColumnTypeInfo(typeInfo, expression.TypeInfo.IsNullable, typeInfo.ColumnSize, null), sqlExpression2);
				return true;
			}
			convertedExpression = null;
			return false;
		}

		// Token: 0x060031F1 RID: 12785 RVA: 0x0009A414 File Offset: 0x00098614
		private bool TryConvertNumberConstant(OdbcTypeInfo typeInfo, Value value, out OdbcScalarExpression convertedExpression)
		{
			if (typeInfo.Equals(this.DoubleType))
			{
				convertedExpression = this.Constant(value.AsNumber.AsDouble);
				return true;
			}
			Odbc32.SQL_TYPE sqlType = typeInfo.SqlType;
			if (sqlType != Odbc32.SQL_TYPE.BIGINT)
			{
				switch (sqlType)
				{
				case Odbc32.SQL_TYPE.NUMERIC:
					return this.TryGetNumericConstant(value.AsNumber, out convertedExpression);
				case Odbc32.SQL_TYPE.DECIMAL:
					return this.TryGetDecimalConstant(value.AsNumber, out convertedExpression);
				case Odbc32.SQL_TYPE.INTEGER:
					return this.TryGetInt32Constant(value.AsNumber, out convertedExpression);
				case Odbc32.SQL_TYPE.FLOAT:
					convertedExpression = this.Constant(value.AsNumber.AsDouble);
					return true;
				}
				convertedExpression = null;
				return false;
			}
			return this.TryGetInt64Constant(value.AsNumber, out convertedExpression);
		}

		// Token: 0x060031F2 RID: 12786 RVA: 0x0009A4C0 File Offset: 0x000986C0
		private OdbcScalarExpression VisitConcat(OdbcScalarExpression left, OdbcScalarExpression right)
		{
			OdbcScalarExpression odbcScalarExpression2;
			using (this.trace.NewScope("VisitConcat"))
			{
				if (left.TypeInfo.TypeValue.TypeKind != ValueKind.Text || right.TypeInfo.TypeValue.TypeKind != ValueKind.Text || left.TypeInfo.ColumnSize == null || right.TypeInfo.ColumnSize == null)
				{
					throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<BinaryOperator2, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>>>(OdbcFoldingWarnings.InvalidBinaryOperator(this.dataSource.SqlSettings, BinaryOperator2.Concatenate, left, right));
				}
				int value = left.TypeInfo.ColumnSize.Value;
				int value2 = right.TypeInfo.ColumnSize.Value;
				int num;
				if (value - value2 >= 2147483647 - value || value2 - value >= 2147483647 - value2)
				{
					num = int.MaxValue;
				}
				else
				{
					num = value + value2;
				}
				OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult compatibilityAdjustmentResult = this.ConvertForSize(left, right, num, this.tolerateConcatOverflow);
				OdbcScalarExpression odbcScalarExpression = new OdbcScalarExpression(compatibilityAdjustmentResult.TypeInfo, this.Concat(compatibilityAdjustmentResult.Left.Expression, compatibilityAdjustmentResult.Right.Expression));
				Odbc32.SQL_CB? stringConcatNullBehavior = this.dataSource.Info.StringConcatNullBehavior;
				Odbc32.SQL_CB sql_CB = Odbc32.SQL_CB.SQL_CB_NULL;
				if ((stringConcatNullBehavior.GetValueOrDefault() == sql_CB) & (stringConcatNullBehavior != null))
				{
					odbcScalarExpression2 = odbcScalarExpression;
				}
				else
				{
					odbcScalarExpression2 = this.NullLift(odbcScalarExpression, new OdbcScalarExpression[] { left, right });
				}
			}
			return odbcScalarExpression2;
		}

		// Token: 0x060031F3 RID: 12787 RVA: 0x0009A650 File Offset: 0x00098850
		private OdbcScalarExpression VisitCoalesce(OdbcScalarExpression left, OdbcScalarExpression right)
		{
			OdbcScalarExpression odbcScalarExpression;
			using (this.trace.NewScope("VisitCoalesce"))
			{
				if (left.TypeValue.TypeKind != right.TypeValue.TypeKind)
				{
					throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<BinaryOperator2, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>>>(OdbcFoldingWarnings.InvalidBinaryOperator(this.dataSource.SqlSettings, BinaryOperator2.Coalesce, left, right));
				}
				OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult compatibilityAdjustmentResult = this.AdjustForCompatibility(left, right, Precision.Double);
				odbcScalarExpression = new OdbcScalarExpression(compatibilityAdjustmentResult.TypeInfo, this.Coalesce(compatibilityAdjustmentResult.Left.Expression, compatibilityAdjustmentResult.Right.Expression));
			}
			return odbcScalarExpression;
		}

		// Token: 0x060031F4 RID: 12788 RVA: 0x0009A6FC File Offset: 0x000988FC
		private OdbcScalarExpression NullLift(OdbcScalarExpression result, params OdbcScalarExpression[] expressions)
		{
			ArrayBuilder<Condition> arrayBuilder = default(ArrayBuilder<Condition>);
			foreach (OdbcScalarExpression odbcScalarExpression in expressions)
			{
				if (odbcScalarExpression.TypeInfo.IsNullable)
				{
					arrayBuilder.Add(this.IsNull(odbcScalarExpression.Expression));
				}
			}
			if (arrayBuilder.Count == 0)
			{
				return result;
			}
			CaseFunction caseFunction = this.Case();
			caseFunction.WhenItems.Add(new WhenItem
			{
				When = this.Condition(ConditionOperator.Or, arrayBuilder.ToArray()),
				Then = SqlConstant.Null
			});
			caseFunction.ElseExpression = result.Expression;
			return new OdbcScalarExpression(result.TypeInfo.AsNullable, caseFunction);
		}

		// Token: 0x060031F5 RID: 12789 RVA: 0x0009A7A8 File Offset: 0x000989A8
		private bool IsWideCharacterType(Odbc32.SQL_TYPE sqlType)
		{
			return OdbcQueryExpressionVisitor.wcharTypes.Where((OdbcTypeMap m) => m.SqlType == sqlType).FirstOrDefault<OdbcTypeMap>() != null;
		}

		// Token: 0x060031F6 RID: 12790 RVA: 0x0009A7E0 File Offset: 0x000989E0
		private bool TryGetTimeSpanFromDurationExpression(SqlExpression duration, out TimeSpan timeSpan)
		{
			DurationValue durationValue = ((!(duration is ConstantAnnotationExpression)) ? null : ((duration as ConstantAnnotationExpression).Value as DurationValue));
			if (durationValue != null)
			{
				timeSpan = durationValue.AsTimeSpan;
				return true;
			}
			timeSpan = default(TimeSpan);
			return false;
		}

		// Token: 0x060031F7 RID: 12791 RVA: 0x0009A824 File Offset: 0x00098A24
		private bool TryVisitDateDurationBinaryArithmetic(OdbcScalarExpression left, OdbcScalarExpression right, BinaryOperator2 op, out SqlExpression datetime, out TimeSpan timeSpan, ValueKind dateKind)
		{
			if (this.trace.WhenNot<FoldingWarnings.FoldingWarning<BinaryOperator2, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, string>>(op == BinaryOperator2.Subtract || op == BinaryOperator2.Add, OdbcFoldingWarnings.BinaryOperation(this.dataSource.SqlSettings, op, left, right, "Add, Subtract")))
			{
				if (left.TypeInfo.TypeValue.TypeKind == dateKind && right.TypeInfo.TypeValue.TypeKind == ValueKind.Duration)
				{
					datetime = left.Expression;
					SqlExpression sqlExpression = right.Expression;
					if (this.TryGetTimeSpanFromDurationExpression(sqlExpression, out timeSpan))
					{
						if (op == BinaryOperator2.Subtract)
						{
							timeSpan = timeSpan.Negate();
						}
						return true;
					}
				}
				else if (left.TypeInfo.TypeValue.TypeKind == ValueKind.Duration && right.TypeInfo.TypeValue.TypeKind == dateKind && this.trace.WhenNot<FoldingWarnings.FoldingWarning<BinaryOperator2, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, string>>(op == BinaryOperator2.Add, OdbcFoldingWarnings.BinaryOperation(this.dataSource.SqlSettings, op, left, right, "Add")))
				{
					datetime = right.Expression;
					SqlExpression sqlExpression = left.Expression;
					if (this.TryGetTimeSpanFromDurationExpression(sqlExpression, out timeSpan))
					{
						return true;
					}
				}
				this.trace.Trace<FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, BinaryOperator2, ValueKind>>(OdbcFoldingWarnings.DurationOperation(this.dataSource.SqlSettings, left, right, op, dateKind));
			}
			datetime = null;
			timeSpan = default(TimeSpan);
			return false;
		}

		// Token: 0x060031F8 RID: 12792 RVA: 0x0009A95C File Offset: 0x00098B5C
		private bool TryVisitDateDurationBinaryArithmetic(OdbcScalarExpression left, OdbcScalarExpression right, BinaryOperator2 op, out SqlExpression datetime, out SqlExpression duration, ValueKind dateKind)
		{
			if (this.trace.WhenNot<FoldingWarnings.FoldingWarning<BinaryOperator2, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, string>>(op == BinaryOperator2.Subtract || op == BinaryOperator2.Add, OdbcFoldingWarnings.BinaryOperation(this.dataSource.SqlSettings, op, left, right, "Add, Subtract")))
			{
				if (left.TypeInfo.TypeValue.TypeKind == dateKind && right.TypeInfo.TypeValue.TypeKind == ValueKind.Duration)
				{
					datetime = left.Expression;
					if (op == BinaryOperator2.Subtract)
					{
						duration = new BinaryScalarOperation(this.VisitConstantInt64(this.BigIntType, NumberValue.New(0)), BinaryScalarOperator.Subtract, right.Expression);
					}
					else
					{
						duration = right.Expression;
					}
					return true;
				}
				if (left.TypeInfo.TypeValue.TypeKind == ValueKind.Duration && right.TypeInfo.TypeValue.TypeKind == dateKind && this.trace.WhenNot<FoldingWarnings.FoldingWarning<BinaryOperator2, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, string>>(op == BinaryOperator2.Add, OdbcFoldingWarnings.BinaryOperation(this.dataSource.SqlSettings, op, left, right, "Add")))
				{
					datetime = right.Expression;
					duration = left.Expression;
					return true;
				}
				this.trace.Trace<FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, BinaryOperator2, ValueKind>>(OdbcFoldingWarnings.DurationOperation(this.dataSource.SqlSettings, left, right, op, dateKind));
			}
			datetime = null;
			duration = null;
			return false;
		}

		// Token: 0x060031F9 RID: 12793 RVA: 0x0009AA8C File Offset: 0x00098C8C
		private SqlExpression VisitTimestampDiff(SqlExpression left, SqlExpression right, Odbc32.SQL_TSI dimension)
		{
			SqlExpression sqlExpression;
			using (this.trace.NewScope("VisitTimestampDiff"))
			{
				if (!this.trace.DataSourceInfo.Supports(Odbc32.SQL_FN_TD.SQL_FN_TD_TIMESTAMPDIFF) || this.trace.When<FoldingWarnings.FoldingWarning<Odbc32.SQL_TSI, Odbc32.SQL_INFO>>(!this.dataSource.Info.SupportsTimedateDiffInterval(dimension), OdbcFoldingWarnings.SqlGetInfo<Odbc32.SQL_TSI>(Odbc32.SQL_INFO.SQL_TIMEDATE_DIFF_INTERVALS, dimension)))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				sqlExpression = OdbcQueryExpressionVisitor.Call(new OdbcScalarFunctionReference(SqlLanguageStrings.TimestampDiffSqlString), new SqlExpression[]
				{
					OdbcQueryExpressionVisitor.GetTSIConstant(dimension),
					right,
					left
				});
			}
			return sqlExpression;
		}

		// Token: 0x060031FA RID: 12794 RVA: 0x0009AB3C File Offset: 0x00098D3C
		private OdbcScalarExpression VisitTimestampDiff(OdbcScalarExpression left, OdbcScalarExpression right, Odbc32.SQL_TSI dimension)
		{
			return new OdbcScalarExpression(this.NewColumnType(this.IntegerType.SqlType, left.TypeValue.IsNullable || right.TypeValue.IsNullable), this.VisitTimestampDiff(left.Expression, right.Expression, dimension));
		}

		// Token: 0x060031FB RID: 12795 RVA: 0x0009AB90 File Offset: 0x00098D90
		private SqlExpression VisitTimestampAdd(Odbc32.SQL_TSI dimension, SqlExpression count, SqlExpression timestamp)
		{
			SqlExpression sqlExpression;
			using (this.trace.NewScope("VisitTimestampAdd"))
			{
				if (!this.trace.DataSourceInfo.Supports(Odbc32.SQL_FN_TD.SQL_FN_TD_TIMESTAMPADD) || this.trace.When<FoldingWarnings.FoldingWarning<Odbc32.SQL_TSI, Odbc32.SQL_INFO>>(!this.dataSource.Info.SupportsTimedateAddInterval(dimension), OdbcFoldingWarnings.SqlGetInfo<Odbc32.SQL_TSI>(Odbc32.SQL_INFO.SQL_TIMEDATE_ADD_INTERVALS, dimension)))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				sqlExpression = OdbcQueryExpressionVisitor.Call(new OdbcScalarFunctionReference(SqlLanguageStrings.TimestampAddSqlString), new SqlExpression[]
				{
					OdbcQueryExpressionVisitor.GetTSIConstant(dimension),
					count,
					timestamp
				});
			}
			return sqlExpression;
		}

		// Token: 0x060031FC RID: 12796 RVA: 0x0009AC40 File Offset: 0x00098E40
		private OdbcScalarExpression VisitTimestampAdd(Odbc32.SQL_TSI dimension, OdbcScalarExpression count, OdbcScalarExpression timestamp)
		{
			OdbcDerivedColumnTypeInfo odbcDerivedColumnTypeInfo = timestamp.TypeInfo;
			if (count.TypeInfo.IsNullable)
			{
				odbcDerivedColumnTypeInfo = odbcDerivedColumnTypeInfo.AsNullable;
			}
			return new OdbcScalarExpression(odbcDerivedColumnTypeInfo, this.VisitTimestampAdd(dimension, count.Expression, timestamp.Expression));
		}

		// Token: 0x060031FD RID: 12797 RVA: 0x0009AC81 File Offset: 0x00098E81
		private SqlExpression DaysDifferenceDuration(SqlExpression left, SqlExpression right)
		{
			return new BinaryScalarOperation(this.VisitConvert(this.IntegerType, this.BigIntType, this.VisitTimestampDiff(left, right, Odbc32.SQL_TSI.SQL_TSI_DAY)), BinaryScalarOperator.Multiply, this.VisitConstantInt64(this.BigIntType, NumberValue.New(864000000000L)));
		}

		// Token: 0x060031FE RID: 12798 RVA: 0x0009ACBF File Offset: 0x00098EBF
		private OdbcScalarExpression VisitNumberNumberBinaryScalarOperation(OdbcScalarExpression left, BinaryOperator2 op, OdbcScalarExpression right)
		{
			return this.VisitNumberNumberBinaryScalarOperation(Precision.Double, left, op, right);
		}

		// Token: 0x060031FF RID: 12799 RVA: 0x0009ACD0 File Offset: 0x00098ED0
		private OdbcScalarExpression VisitNumberNumberBinaryScalarOperation(Precision precision, OdbcScalarExpression left, BinaryOperator2 op, OdbcScalarExpression right)
		{
			OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult compatibilityAdjustmentResult = this.AdjustNumberValuesToPreventOverflow(left, right, precision);
			return new OdbcScalarExpression(compatibilityAdjustmentResult.TypeInfo, new BinaryScalarOperation(compatibilityAdjustmentResult.Left.Expression, this.GetBinaryScalarOperator(op, left, right), compatibilityAdjustmentResult.Right.Expression));
		}

		// Token: 0x06003200 RID: 12800 RVA: 0x0009AD18 File Offset: 0x00098F18
		private OdbcScalarExpression VisitDurationDurationBinaryScalarOperation(OdbcScalarExpression left, BinaryOperator2 op, OdbcScalarExpression right)
		{
			OdbcScalarExpression odbcScalarExpression;
			using (this.trace.NewScope("VisitDurationDurationBinaryScalarOperation"))
			{
				if (op > BinaryOperator2.Subtract && op != BinaryOperator2.Divide)
				{
					throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<BinaryOperator2, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>>>(OdbcFoldingWarnings.InvalidBinaryOperator(this.dataSource.SqlSettings, op, left, right));
				}
				odbcScalarExpression = this.VisitNumberNumberBinaryScalarOperation(left, op, right);
			}
			return odbcScalarExpression;
		}

		// Token: 0x06003201 RID: 12801 RVA: 0x0009AD84 File Offset: 0x00098F84
		private OdbcScalarExpression VisitDurationDateBinaryScalarOperation(OdbcScalarExpression left, BinaryOperator2 op, OdbcScalarExpression right)
		{
			OdbcScalarExpression odbcScalarExpression;
			using (this.trace.NewScope("VisitDurationDateBinaryScalarOperation"))
			{
				SqlExpression sqlExpression;
				SqlExpression sqlExpression2;
				if (!this.TryVisitDateDurationBinaryArithmetic(left, right, op, out sqlExpression, out sqlExpression2, ValueKind.Date))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				SqlExpression sqlExpression3 = new BinaryScalarOperation(sqlExpression2, BinaryScalarOperator.Divide, this.VisitConstantInt64(this.BigIntType, NumberValue.New(864000000000L)));
				odbcScalarExpression = new OdbcScalarExpression(this.NewColumnType(Odbc32.SQL_TYPE.TYPE_DATE, true), this.VisitTimestampAdd(Odbc32.SQL_TSI.SQL_TSI_DAY, sqlExpression3, sqlExpression));
			}
			return odbcScalarExpression;
		}

		// Token: 0x06003202 RID: 12802 RVA: 0x0009AE1C File Offset: 0x0009901C
		private OdbcScalarExpression VisitDurationDatetimeBinaryScalarOperation(OdbcScalarExpression left, BinaryOperator2 op, OdbcScalarExpression right)
		{
			OdbcScalarExpression odbcScalarExpression;
			using (this.trace.NewScope("VisitDurationDatetimeBinaryScalarOperation"))
			{
				SqlExpression sqlExpression;
				TimeSpan timeSpan;
				if (!this.TryVisitDateDurationBinaryArithmetic(left, right, op, out sqlExpression, out timeSpan, ValueKind.DateTime))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				SqlExpression sqlExpression2 = sqlExpression;
				if (timeSpan.Days != 0)
				{
					sqlExpression2 = this.VisitTimestampAdd(Odbc32.SQL_TSI.SQL_TSI_DAY, this.VisitConstantInt32(this.IntegerType, NumberValue.New(timeSpan.Days)), sqlExpression2);
				}
				if (timeSpan.Hours != 0)
				{
					sqlExpression2 = this.VisitTimestampAdd(Odbc32.SQL_TSI.SQL_TSI_HOUR, this.VisitConstantInt32(this.IntegerType, NumberValue.New(timeSpan.Hours)), sqlExpression2);
				}
				if (timeSpan.Minutes != 0)
				{
					sqlExpression2 = this.VisitTimestampAdd(Odbc32.SQL_TSI.SQL_TSI_MINUTE, this.VisitConstantInt32(this.IntegerType, NumberValue.New(timeSpan.Minutes)), sqlExpression2);
				}
				if (timeSpan.Seconds != 0)
				{
					sqlExpression2 = this.VisitTimestampAdd(Odbc32.SQL_TSI.SQL_TSI_SECOND, this.VisitConstantInt32(this.IntegerType, NumberValue.New(timeSpan.Seconds)), sqlExpression2);
				}
				if (timeSpan.Milliseconds != 0)
				{
					if (this.dataSource.Info.FractionalSecondsPerSecond == null)
					{
						throw this.trace.NewFoldingFailureException("Fraction of a second specified in query but FractionalSecondsScale not set in connector.");
					}
					long num = (long)timeSpan.Milliseconds * ((long)this.dataSource.Info.FractionalSecondsPerSecond.Value / 1000L);
					sqlExpression2 = this.VisitTimestampAdd(Odbc32.SQL_TSI.SQL_TSI_FRAC_SECOND, this.VisitConstantInt32(this.IntegerType, NumberValue.New(num)), sqlExpression2);
				}
				odbcScalarExpression = new OdbcScalarExpression(this.NewColumnType(Odbc32.SQL_TYPE.TYPE_TIMESTAMP, true), sqlExpression2);
			}
			return odbcScalarExpression;
		}

		// Token: 0x06003203 RID: 12803 RVA: 0x0009AFC0 File Offset: 0x000991C0
		private OdbcScalarExpression VisitDateDateBinaryScalarOperation(OdbcScalarExpression left, BinaryOperator2 op, OdbcScalarExpression right)
		{
			OdbcScalarExpression odbcScalarExpression;
			using (this.trace.NewScope("VisitDateDateBinaryScalarOperation"))
			{
				if (op != BinaryOperator2.Subtract)
				{
					throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<BinaryOperator2, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, string>>(OdbcFoldingWarnings.BinaryOperation(this.dataSource.SqlSettings, op, left, right, "Subtract"));
				}
				odbcScalarExpression = new OdbcScalarExpression(this.DurationType, this.DaysDifferenceDuration(left.Expression, right.Expression));
			}
			return odbcScalarExpression;
		}

		// Token: 0x06003204 RID: 12804 RVA: 0x0009B044 File Offset: 0x00099244
		private OdbcScalarExpression VisitDatetimeDatetimeBinaryScalarOperation(OdbcScalarExpression left, BinaryOperator2 op, OdbcScalarExpression right)
		{
			OdbcScalarExpression odbcScalarExpression;
			using (this.trace.NewScope("VisitDatetimeDatetimeBinaryScalarOperation"))
			{
				if (!this.trace.WhenNot<FoldingWarnings.FoldingWarning<BinaryOperator2, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, string>>(op == BinaryOperator2.Subtract, OdbcFoldingWarnings.BinaryOperation(this.dataSource.SqlSettings, op, left, right, "Subtract")) || !this.trace.WhenNot<FoldingWarnings.FoldingWarning<string>>(this.dataSource.Info.TicksPerFractionalSecond != null, FoldingWarnings.SqlCapabilities("FractionalSecondsScale")))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				SqlExpression sqlExpression = this.DaysDifferenceDuration(left.Expression, right.Expression);
				SqlExpression sqlExpression2 = new BinaryScalarOperation(this.CallConvertOrCast(this.VisitTimestampDiff(left.Expression, this.VisitTimestampAdd(Odbc32.SQL_TSI.SQL_TSI_DAY, this.VisitTimestampDiff(left.Expression, right.Expression, Odbc32.SQL_TSI.SQL_TSI_DAY), right.Expression), Odbc32.SQL_TSI.SQL_TSI_FRAC_SECOND), OdbcTypeMap.BigInt), BinaryScalarOperator.Multiply, this.VisitConstantInt64(this.BigIntType, NumberValue.New(this.dataSource.Info.TicksPerFractionalSecond.Value)));
				SqlExpression sqlExpression3 = new BinaryScalarOperation(sqlExpression, BinaryScalarOperator.Add, sqlExpression2);
				odbcScalarExpression = new OdbcScalarExpression(this.DurationType, sqlExpression3);
			}
			return odbcScalarExpression;
		}

		// Token: 0x06003205 RID: 12805 RVA: 0x0009B180 File Offset: 0x00099380
		private OdbcScalarExpression VisitDurationNumberBinaryScalarOperation(OdbcScalarExpression left, BinaryOperator2 op, OdbcScalarExpression right)
		{
			OdbcScalarExpression odbcScalarExpression3;
			using (this.trace.NewScope("VisitDurationNumberBinaryScalarOperation"))
			{
				if (op != BinaryOperator2.Multiply && (left.TypeInfo.TypeValue.TypeKind != ValueKind.Duration || op != BinaryOperator2.Divide || right.TypeInfo.TypeValue.TypeKind != ValueKind.Number))
				{
					throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<BinaryOperator2, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>>>(OdbcFoldingWarnings.InvalidBinaryOperator(this.dataSource.SqlSettings, op, left, right));
				}
				OdbcScalarExpression odbcScalarExpression = this.VisitNumberNumberBinaryScalarOperation(left, op, right);
				OdbcScalarExpression odbcScalarExpression2 = this.Convert(this.BigIntType, odbcScalarExpression);
				odbcScalarExpression3 = new OdbcScalarExpression(this.DurationType, odbcScalarExpression2.Expression);
			}
			return odbcScalarExpression3;
		}

		// Token: 0x06003206 RID: 12806 RVA: 0x0009B234 File Offset: 0x00099434
		private OdbcScalarExpression VisitBinaryScalarOperation(OdbcScalarExpression left, BinaryOperator2 op, OdbcScalarExpression right)
		{
			OdbcScalarExpression odbcScalarExpression;
			using (this.trace.NewScope("VisitBinaryScalarOperation"))
			{
				ValueKind typeKind = left.TypeInfo.TypeValue.TypeKind;
				ValueKind typeKind2 = right.TypeInfo.TypeValue.TypeKind;
				if (typeKind == ValueKind.Number && typeKind2 == ValueKind.Number)
				{
					odbcScalarExpression = this.VisitNumberNumberBinaryScalarOperation(left, op, right);
				}
				else if (typeKind == ValueKind.Duration && typeKind2 == ValueKind.Duration)
				{
					odbcScalarExpression = this.VisitDurationDurationBinaryScalarOperation(left, op, right);
				}
				else if ((typeKind == ValueKind.Duration && typeKind2 == ValueKind.DateTime) || (typeKind == ValueKind.DateTime && typeKind2 == ValueKind.Duration))
				{
					odbcScalarExpression = this.VisitDurationDatetimeBinaryScalarOperation(left, op, right);
				}
				else if ((typeKind == ValueKind.Duration && typeKind2 == ValueKind.Date) || (typeKind == ValueKind.Date && typeKind2 == ValueKind.Duration))
				{
					odbcScalarExpression = this.VisitDurationDateBinaryScalarOperation(left, op, right);
				}
				else if (typeKind == ValueKind.Date && typeKind2 == ValueKind.Date)
				{
					odbcScalarExpression = this.VisitDateDateBinaryScalarOperation(left, op, right);
				}
				else if (typeKind == ValueKind.DateTime && typeKind2 == ValueKind.DateTime)
				{
					odbcScalarExpression = this.VisitDatetimeDatetimeBinaryScalarOperation(left, op, right);
				}
				else
				{
					if ((typeKind != ValueKind.Duration || typeKind2 != ValueKind.Number) && (typeKind != ValueKind.Number || typeKind2 != ValueKind.Duration))
					{
						throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<BinaryOperator2, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>>>(OdbcFoldingWarnings.InvalidBinaryOperator(this.dataSource.SqlSettings, op, left, right));
					}
					odbcScalarExpression = this.VisitDurationNumberBinaryScalarOperation(left, op, right);
				}
			}
			return odbcScalarExpression;
		}

		// Token: 0x06003207 RID: 12807 RVA: 0x0009B35C File Offset: 0x0009955C
		private BinaryScalarOperator GetBinaryScalarOperator(BinaryOperator2 op, OdbcScalarExpression left, OdbcScalarExpression right)
		{
			BinaryScalarOperator binaryScalarOperator;
			using (this.trace.NewScope("GetBinaryScalarOperator"))
			{
				switch (op)
				{
				case BinaryOperator2.Add:
					binaryScalarOperator = BinaryScalarOperator.Add;
					break;
				case BinaryOperator2.Subtract:
					binaryScalarOperator = BinaryScalarOperator.Subtract;
					break;
				case BinaryOperator2.Multiply:
					binaryScalarOperator = BinaryScalarOperator.Multiply;
					break;
				case BinaryOperator2.Divide:
					binaryScalarOperator = BinaryScalarOperator.Divide;
					break;
				default:
					throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<BinaryOperator2, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>>>(OdbcFoldingWarnings.InvalidBinaryOperator(this.dataSource.SqlSettings, op, left, right));
				}
			}
			return binaryScalarOperator;
		}

		// Token: 0x06003208 RID: 12808 RVA: 0x0009B3DC File Offset: 0x000995DC
		private Condition VisitCondition(OdbcSqlExpression left, ConditionOperator op, OdbcSqlExpression right)
		{
			return this.Condition(op, new Condition[]
			{
				this.ConvertToCondition(left).Expression,
				this.ConvertToCondition(right).Expression
			});
		}

		// Token: 0x06003209 RID: 12809 RVA: 0x0009B40C File Offset: 0x0009960C
		private Condition VisitLogicalOperation(OdbcScalarExpression left, BinaryOperator2 op, OdbcScalarExpression right)
		{
			Condition condition;
			using (this.trace.NewScope("VisitLogicalOperation"))
			{
				OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult compatibilityAdjustmentResult = this.AdjustForCompatibility(left, right, Precision.Double);
				if (!compatibilityAdjustmentResult.AreCompatible)
				{
					throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<BinaryOperator2, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>>>(OdbcFoldingWarnings.InvalidBinaryOperator(this.dataSource.SqlSettings, op, left, right));
				}
				condition = this.BinaryLogicalOperation(compatibilityAdjustmentResult.Left.Expression, this.GetBinaryLogicalOperator(left, op, right), compatibilityAdjustmentResult.Right.Expression);
			}
			return condition;
		}

		// Token: 0x0600320A RID: 12810 RVA: 0x0009B4A4 File Offset: 0x000996A4
		private Func<InvocationQueryExpression, OdbcSqlExpression> NewTextLocateCompareVisitor(BinaryOperator2 op, NumberValue expectedLocation, string patternPrefix, string patternSuffix)
		{
			return (InvocationQueryExpression e) => this.VisitTextContains(e, op, expectedLocation, patternPrefix, patternSuffix);
		}

		// Token: 0x0600320B RID: 12811 RVA: 0x0009B4DC File Offset: 0x000996DC
		private OdbcSqlExpression VisitTextLength(InvocationQueryExpression expression)
		{
			OdbcSqlExpression odbcSqlExpression;
			using (this.trace.NewScope("VisitTextLength"))
			{
				if (!this.trace.ArgumentCountEquals(expression, 1))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				OdbcScalarExpression asScalar = this.Visit(expression.Arguments[0]).AsScalar;
				odbcSqlExpression = this.VisitTextLength(expression, 0, asScalar);
			}
			return odbcSqlExpression;
		}

		// Token: 0x0600320C RID: 12812 RVA: 0x0009B558 File Offset: 0x00099758
		private OdbcSqlExpression VisitTextLength(InvocationQueryExpression expression, int argumentIndex, OdbcScalarExpression text)
		{
			OdbcSqlExpression odbcSqlExpression;
			using (this.trace.NewScope("VisitTextLength"))
			{
				if (!this.trace.ArgumentTypeKindIs(expression, argumentIndex, text, ValueKind.Text) || !this.trace.DataSourceInfo.Supports(Odbc32.SQL_FN_STR.SQL_FN_STR_LENGTH))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				odbcSqlExpression = new OdbcScalarExpression(this.NewColumnType(this.IntegerType.SqlType, text.TypeInfo.IsNullable), OdbcQueryExpressionVisitor.Call(new OdbcScalarFunctionReference(SqlLanguageStrings.LengthSqlString), new SqlExpression[] { text.Expression }));
			}
			return odbcSqlExpression;
		}

		// Token: 0x0600320D RID: 12813 RVA: 0x0009B608 File Offset: 0x00099808
		private OdbcSqlExpression VisitCharacterFromNumber(InvocationQueryExpression expression)
		{
			OdbcSqlExpression odbcSqlExpression;
			using (this.trace.NewScope("VisitCharacterFromNumber"))
			{
				if (!this.trace.ArgumentCountEquals(expression, 1) || !this.trace.DataSourceInfo.Supports(Odbc32.SQL_FN_STR.SQL_FN_STR_CHAR))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				OdbcScalarExpression odbcScalarExpression = this.Visit(expression.Arguments[0]).AsScalar;
				if (!this.trace.ArgumentTypeKindIs(expression, 0, odbcScalarExpression, ValueKind.Number))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				odbcScalarExpression = this.Convert(this.IntegerType, odbcScalarExpression);
				odbcSqlExpression = new OdbcScalarExpression(this.NewColumnType(Odbc32.SQL_TYPE.CHAR, odbcScalarExpression.TypeInfo.IsNullable), OdbcQueryExpressionVisitor.Call(new OdbcScalarFunctionReference(SqlLanguageStrings.CharSqlString), new SqlExpression[] { odbcScalarExpression.Expression }));
			}
			return odbcSqlExpression;
		}

		// Token: 0x0600320E RID: 12814 RVA: 0x0009B6F0 File Offset: 0x000998F0
		private OdbcSqlExpression VisitCharacterToNumber(InvocationQueryExpression expression)
		{
			OdbcSqlExpression odbcSqlExpression;
			using (this.trace.NewScope("VisitCharacterToNumber"))
			{
				if (!this.trace.ArgumentCountEquals(expression, 1) || !this.trace.DataSourceInfo.Supports(Odbc32.SQL_FN_STR.SQL_FN_STR_ASCII))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				OdbcScalarExpression asScalar = this.Visit(expression.Arguments[0]).AsScalar;
				if (!this.trace.ArgumentTypeKindIs(expression, 0, asScalar, ValueKind.Text))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				odbcSqlExpression = new OdbcScalarExpression(this.NewColumnType(this.IntegerType.SqlType, asScalar.TypeInfo.IsNullable), OdbcQueryExpressionVisitor.Call(new OdbcScalarFunctionReference(SqlLanguageStrings.AsciiSqlString), new SqlExpression[] { asScalar.Expression }));
			}
			return odbcSqlExpression;
		}

		// Token: 0x0600320F RID: 12815 RVA: 0x0009B7D4 File Offset: 0x000999D4
		private OdbcSqlExpression VisitTextReplace(InvocationQueryExpression expression)
		{
			OdbcSqlExpression odbcSqlExpression;
			using (this.trace.NewScope("VisitTextReplace"))
			{
				if (!this.trace.ArgumentCountEquals(expression, 3) || !this.trace.DataSourceInfo.Supports(Odbc32.SQL_FN_STR.SQL_FN_STR_REPLACE))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				OdbcScalarExpression asScalar = this.Visit(expression.Arguments[0]).AsScalar;
				OdbcScalarExpression asScalar2 = this.Visit(expression.Arguments[1]).AsScalar;
				OdbcScalarExpression asScalar3 = this.Visit(expression.Arguments[2]).AsScalar;
				if (!this.trace.ArgumentTypeKindIs(expression, 0, asScalar, ValueKind.Text) || !this.trace.ArgumentTypeKindIs(expression, 1, asScalar2, ValueKind.Text) || !this.trace.ArgumentTypeKindIs(expression, 2, asScalar3, ValueKind.Text) || !this.trace.ArgumentNotNullable(expression, 1, asScalar2) || !this.trace.ArgumentNotNullable(expression, 2, asScalar3))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				odbcSqlExpression = new OdbcScalarExpression(asScalar.TypeInfo, OdbcQueryExpressionVisitor.Call(new OdbcScalarFunctionReference(SqlLanguageStrings.ReplaceSqlString), new SqlExpression[] { asScalar.Expression, asScalar2.Expression, asScalar3.Expression }));
			}
			return odbcSqlExpression;
		}

		// Token: 0x06003210 RID: 12816 RVA: 0x0009B934 File Offset: 0x00099B34
		private OdbcSqlExpression VisitTextRemoveRange(InvocationQueryExpression expression)
		{
			OdbcSqlExpression odbcSqlExpression;
			using (this.trace.NewScope("VisitTextRemoveRange"))
			{
				if (!this.trace.ArgumentCountBetween(expression, 2, 3) || !this.trace.DataSourceInfo.Supports(Odbc32.SQL_FN_STR.SQL_FN_STR_INSERT))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				OdbcScalarExpression asScalar = this.Visit(expression.Arguments[0]).AsScalar;
				OdbcScalarExpression asScalar2 = this.Visit(expression.Arguments[1]).AsScalar;
				if (!this.trace.ArgumentTypeKindIs(expression, 0, asScalar, ValueKind.Text) || !this.trace.ArgumentTypeKindIs(expression, 1, asScalar2, ValueKind.Number) || !this.trace.ArgumentNotNullable(expression, 1, asScalar2))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				SqlExpression sqlExpression = new BinaryScalarOperation(this.Convert(this.IntegerType, asScalar2).Expression, BinaryScalarOperator.Add, SqlConstant.One);
				OdbcScalarExpression odbcScalarExpression;
				if (expression.Arguments.Count == 3)
				{
					odbcScalarExpression = this.Visit(expression.Arguments[2]).AsScalar;
					if (!this.trace.ArgumentTypeKindIs(expression, 2, odbcScalarExpression, ValueKind.Number))
					{
						throw this.trace.NewFoldingFailureException(null);
					}
					odbcScalarExpression = this.Convert(this.IntegerType, odbcScalarExpression);
					if (odbcScalarExpression.TypeInfo.IsNullable)
					{
						odbcScalarExpression = this.VisitNullCoalesce(expression, 2, odbcScalarExpression, new OdbcScalarExpression(this.NewColumnType(this.IntegerType.SqlType, false), SqlConstant.One)).AsScalar;
					}
				}
				else
				{
					odbcScalarExpression = new OdbcScalarExpression(this.NewColumnType(this.IntegerType.SqlType, false), SqlConstant.One);
				}
				odbcSqlExpression = new OdbcScalarExpression(asScalar.TypeInfo, OdbcQueryExpressionVisitor.Call(new OdbcScalarFunctionReference(SqlLanguageStrings.InsertSqlString), new SqlExpression[]
				{
					asScalar.Expression,
					sqlExpression,
					odbcScalarExpression.Expression,
					SqlConstant.EmptyString
				}));
			}
			return odbcSqlExpression;
		}

		// Token: 0x06003211 RID: 12817 RVA: 0x0009BB30 File Offset: 0x00099D30
		private OdbcSqlExpression VisitTextRepeat(InvocationQueryExpression expression)
		{
			OdbcSqlExpression odbcSqlExpression;
			using (this.trace.NewScope("VisitTextRepeat"))
			{
				if (!this.trace.ArgumentCountEquals(expression, 2) || !this.trace.DataSourceInfo.Supports(Odbc32.SQL_FN_STR.SQL_FN_STR_REPEAT))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				OdbcScalarExpression asScalar = this.Visit(expression.Arguments[0]).AsScalar;
				OdbcScalarExpression odbcScalarExpression = this.Visit(expression.Arguments[1]).AsScalar;
				if (!this.trace.ArgumentTypeKindIs(expression, 0, asScalar, ValueKind.Text) || !this.trace.ArgumentTypeKindIs(expression, 1, odbcScalarExpression, ValueKind.Number) || !this.trace.ArgumentNotNullable(expression, 1, odbcScalarExpression))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				odbcScalarExpression = this.Convert(this.IntegerType, odbcScalarExpression);
				odbcSqlExpression = new OdbcScalarExpression(asScalar.TypeInfo, OdbcQueryExpressionVisitor.Call(new OdbcScalarFunctionReference(SqlLanguageStrings.RepeatSqlString), new SqlExpression[] { asScalar.Expression, odbcScalarExpression.Expression }));
			}
			return odbcSqlExpression;
		}

		// Token: 0x06003212 RID: 12818 RVA: 0x0009BC50 File Offset: 0x00099E50
		private OdbcScalarExpression VisitLog(InvocationQueryExpression expression, int argumentIndex, OdbcScalarExpression number)
		{
			OdbcScalarExpression odbcScalarExpression;
			using (this.trace.NewScope("VisitLog"))
			{
				if (!this.trace.DataSourceInfo.Supports(Odbc32.SQL_FUN_NUM.SQL_FN_NUM_LOG) || !this.trace.ArgumentTypeKindIs(expression, argumentIndex, number, ValueKind.Number))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				odbcScalarExpression = new OdbcScalarExpression(number.TypeInfo, OdbcQueryExpressionVisitor.Call(new OdbcScalarFunctionReference(SqlLanguageStrings.LogSqlString), new SqlExpression[] { number.Expression }));
			}
			return odbcScalarExpression;
		}

		// Token: 0x06003213 RID: 12819 RVA: 0x0009BCEC File Offset: 0x00099EEC
		private OdbcSqlExpression VisitNumberLog(InvocationQueryExpression expression)
		{
			OdbcSqlExpression odbcSqlExpression;
			using (this.trace.NewScope("VisitNumberLog"))
			{
				if (expression.Arguments.Count == 1)
				{
					odbcSqlExpression = this.VisitLog(expression, 0, this.SoftConvert(this.Visit(expression.Arguments[0]).AsScalar, Precision.Double));
				}
				else
				{
					if (expression.Arguments.Count != 2)
					{
						throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<InvocationQueryExpression>, int, int>>(FoldingWarnings.InvalidArgumentsCount(expression, 1, 2));
					}
					odbcSqlExpression = this.VisitBinaryScalarOperation(this.VisitLog(expression, 0, this.SoftConvert(this.Visit(expression.Arguments[0]).AsScalar, Precision.Double)), BinaryOperator2.Divide, this.VisitLog(expression, 1, this.SoftConvert(this.Visit(expression.Arguments[1]).AsScalar, Precision.Double)));
				}
			}
			return odbcSqlExpression;
		}

		// Token: 0x06003214 RID: 12820 RVA: 0x0009BDE4 File Offset: 0x00099FE4
		private Func<InvocationQueryExpression, OdbcSqlExpression> NewTextCaseTransformVisitor(Odbc32.SQL_FN_STR capability, ConstantSqlString functionName)
		{
			return (InvocationQueryExpression e) => this.VisitTextCaseTransform(e, capability, functionName);
		}

		// Token: 0x06003215 RID: 12821 RVA: 0x0009BE0C File Offset: 0x0009A00C
		private OdbcSqlExpression VisitTextCaseTransform(InvocationQueryExpression expression, Odbc32.SQL_FN_STR capability, ConstantSqlString functionName)
		{
			OdbcSqlExpression odbcSqlExpression;
			using (this.trace.NewScope("VisitTextCaseTransform"))
			{
				if (!this.trace.ArgumentCountEquals(expression, 1) || !this.trace.DataSourceInfo.Supports(capability))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				OdbcScalarExpression asScalar = this.Visit(expression.Arguments[0]).AsScalar;
				if (!this.trace.ArgumentTypeKindIs(expression, 0, asScalar, ValueKind.Text))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				odbcSqlExpression = new OdbcScalarExpression(asScalar.TypeInfo, OdbcQueryExpressionVisitor.Call(new OdbcScalarFunctionReference(functionName), new SqlExpression[] { asScalar.Expression }));
			}
			return odbcSqlExpression;
		}

		// Token: 0x06003216 RID: 12822 RVA: 0x0009BED4 File Offset: 0x0009A0D4
		private Func<InvocationQueryExpression, OdbcSqlExpression> NewTextTrimFunctionVisitor(Odbc32.SQL_FN_STR capability, ConstantSqlString functionName)
		{
			return (InvocationQueryExpression e) => this.VisitTextTrimFunction(e, capability, functionName);
		}

		// Token: 0x06003217 RID: 12823 RVA: 0x0009BEFC File Offset: 0x0009A0FC
		private OdbcSqlExpression VisitTextTrimFunction(InvocationQueryExpression expression, Odbc32.SQL_FN_STR capability, ConstantSqlString functionName)
		{
			OdbcSqlExpression odbcSqlExpression;
			using (this.trace.NewScope("VisitTextTrimFunction"))
			{
				if (!this.trace.ArgumentCountBetween(expression, 1, 2) || !this.trace.DataSourceInfo.Supports(capability))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				Value value;
				if (expression.Arguments.Count == 2 && !this.trace.ArgumentValueWhen(expression.Arguments[1].TryGetConstant(out value) && value.IsText && string.Equals(value.AsString, " ", StringComparison.OrdinalIgnoreCase), expression, 1, "being blank"))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				OdbcScalarExpression asScalar = this.Visit(expression.Arguments[0]).AsScalar;
				if (!this.trace.ArgumentTypeKindIs(expression, 0, asScalar, ValueKind.Text))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				odbcSqlExpression = new OdbcScalarExpression(asScalar.TypeInfo, OdbcQueryExpressionVisitor.Call(new OdbcScalarFunctionReference(functionName), new SqlExpression[] { asScalar.Expression }));
			}
			return odbcSqlExpression;
		}

		// Token: 0x06003218 RID: 12824 RVA: 0x0009C024 File Offset: 0x0009A224
		private Func<InvocationQueryExpression, OdbcSqlExpression> NewTextExtremityFunctionVisitor(Odbc32.SQL_FN_STR capability, ConstantSqlString functionName)
		{
			return (InvocationQueryExpression e) => this.VisitTextExtremityFunction(e, capability, functionName);
		}

		// Token: 0x06003219 RID: 12825 RVA: 0x0009C04C File Offset: 0x0009A24C
		private OdbcSqlExpression VisitTextExtremityFunction(InvocationQueryExpression expression, Odbc32.SQL_FN_STR capability, ConstantSqlString functionName)
		{
			OdbcSqlExpression odbcSqlExpression;
			using (this.trace.NewScope("VisitTextExtremityFunction"))
			{
				if (!this.trace.ArgumentCountEquals(expression, 2) || !this.trace.DataSourceInfo.Supports(capability))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				OdbcScalarExpression asScalar = this.Visit(expression.Arguments[0]).AsScalar;
				OdbcScalarExpression odbcScalarExpression = this.Visit(expression.Arguments[1]).AsScalar;
				if (!this.trace.ArgumentTypeKindIs(expression, 0, asScalar, ValueKind.Text) || !this.trace.ArgumentTypeKindIs(expression, 1, odbcScalarExpression, ValueKind.Number) || !this.trace.ArgumentNotNullable(expression, 1, odbcScalarExpression))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				odbcScalarExpression = this.Convert(this.IntegerType, odbcScalarExpression);
				odbcSqlExpression = new OdbcScalarExpression(asScalar.TypeInfo, OdbcQueryExpressionVisitor.Call(new OdbcScalarFunctionReference(functionName), new SqlExpression[] { asScalar.Expression, odbcScalarExpression.Expression }));
			}
			return odbcSqlExpression;
		}

		// Token: 0x0600321A RID: 12826 RVA: 0x0009C164 File Offset: 0x0009A364
		private OdbcSqlExpression VisitNullCoalesce(InvocationQueryExpression expression, int argumentIndex, OdbcScalarExpression nullableValue, OdbcScalarExpression replacement)
		{
			OdbcSqlExpression odbcSqlExpression;
			using (this.trace.NewScope("VisitNullCoalesce"))
			{
				if (!this.trace.ArgumentTypeKindIs(expression, argumentIndex, nullableValue, replacement.TypeInfo.TypeValue.TypeKind) || !this.trace.ArgumentDataSourceType(expression, argumentIndex, nullableValue, replacement.TypeInfo.DataSourceType) || this.trace.ArgumentNotNullable(expression, argumentIndex, nullableValue) || !this.trace.DataSourceInfo.Supports(Odbc32.SQL_FN_SYSTEM.SQL_FN_SYS_IFNULL))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				odbcSqlExpression = new OdbcScalarExpression(replacement.TypeInfo, OdbcQueryExpressionVisitor.Call(new OdbcScalarFunctionReference(SqlLanguageStrings.IfnullSqlString), new SqlExpression[] { nullableValue.Expression, replacement.Expression }));
			}
			return odbcSqlExpression;
		}

		// Token: 0x0600321B RID: 12827 RVA: 0x0009C244 File Offset: 0x0009A444
		private OdbcSqlExpression VisitTextMiddle(InvocationQueryExpression expression)
		{
			OdbcSqlExpression odbcSqlExpression;
			using (this.trace.NewScope("VisitTextMiddle"))
			{
				if (!this.trace.ArgumentCountBetween(expression, 2, 3) || !this.trace.DataSourceInfo.Supports(Odbc32.SQL_FN_STR.SQL_FN_STR_SUBSTRING))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				OdbcScalarExpression asScalar = this.Visit(expression.Arguments[0]).AsScalar;
				OdbcScalarExpression odbcScalarExpression = this.Visit(expression.Arguments[1]).AsScalar;
				OdbcScalarExpression odbcScalarExpression2 = null;
				if (!this.trace.ArgumentTypeKindIs(expression, 0, asScalar, ValueKind.Text) || !this.trace.ArgumentTypeKindIs(expression, 1, odbcScalarExpression, ValueKind.Number) || !this.trace.ArgumentNotNullable(expression, 1, odbcScalarExpression))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				odbcScalarExpression = this.Convert(this.IntegerType, odbcScalarExpression);
				SqlExpression sqlExpression = new BinaryScalarOperation(odbcScalarExpression.Expression, BinaryScalarOperator.Add, SqlConstant.One);
				if (expression.Arguments.Count == 3)
				{
					odbcScalarExpression2 = this.Visit(expression.Arguments[2]).AsScalar;
					odbcScalarExpression2 = this.Convert(this.IntegerType, odbcScalarExpression2);
					if (!this.trace.ArgumentTypeKindIs(expression, 2, odbcScalarExpression2, ValueKind.Number))
					{
						throw this.trace.NewFoldingFailureException(null);
					}
					if (this.trace.ArgumentNotNullable(expression, 2, odbcScalarExpression2))
					{
						return new OdbcScalarExpression(asScalar.TypeInfo, OdbcQueryExpressionVisitor.Call(new OdbcScalarFunctionReference(SqlLanguageStrings.SubstringSqlString), new SqlExpression[] { asScalar.Expression, sqlExpression, odbcScalarExpression2.Expression }));
					}
				}
				SqlExpression expression2 = this.VisitTextLength(expression, 0, asScalar).AsScalar.Expression;
				OdbcScalarExpression odbcScalarExpression3 = new OdbcScalarExpression(this.NewColumnType(this.IntegerType.SqlType, true), new BinaryScalarOperation(expression2, BinaryScalarOperator.Subtract, odbcScalarExpression.Expression));
				OdbcScalarExpression odbcScalarExpression4 = ((odbcScalarExpression2 == null) ? odbcScalarExpression3 : this.VisitNullCoalesce(expression, 2, odbcScalarExpression2, odbcScalarExpression3).AsScalar);
				odbcSqlExpression = new OdbcScalarExpression(asScalar.TypeInfo, OdbcQueryExpressionVisitor.Call(new OdbcScalarFunctionReference(SqlLanguageStrings.SubstringSqlString), new SqlExpression[] { asScalar.Expression, sqlExpression, odbcScalarExpression4.Expression }));
			}
			return odbcSqlExpression;
		}

		// Token: 0x0600321C RID: 12828 RVA: 0x0009C484 File Offset: 0x0009A684
		private OdbcSqlExpression VisitTextEndsWith(InvocationQueryExpression expression)
		{
			OdbcSqlExpression odbcSqlExpression;
			using (this.trace.NewScope("VisitTextEndsWith"))
			{
				this.EnsureConformance(Odbc32.SQL_SC.SQL_SC_SQL92_ENTRY);
				if (!this.trace.ArgumentCountEquals(expression, 2) || !this.trace.DataSourceInfo.Supports(Odbc32.SQL_FN_STR.SQL_FN_STR_RIGHT))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				OdbcScalarExpression asScalar = this.Visit(expression.Arguments[0]).AsScalar;
				OdbcScalarExpression asScalar2 = this.Visit(expression.Arguments[1]).AsScalar;
				if (!this.trace.ArgumentTypeKindIs(expression, 0, asScalar, ValueKind.Text) || !this.trace.ArgumentTypeKindIs(expression, 1, asScalar2, ValueKind.Text))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult compatibilityAdjustmentResult = this.AdjustForCompatibility(asScalar, asScalar2, Precision.Double);
				odbcSqlExpression = new OdbcConditionExpression(this.BinaryLogicalOperation(OdbcQueryExpressionVisitor.Call(new OdbcScalarFunctionReference(SqlLanguageStrings.RightSqlString), new SqlExpression[]
				{
					compatibilityAdjustmentResult.Left.Expression,
					this.Len(asScalar2).Expression
				}), BinaryLogicalOperator.Equals, compatibilityAdjustmentResult.Right.Expression));
			}
			return odbcSqlExpression;
		}

		// Token: 0x0600321D RID: 12829 RVA: 0x0009C5B4 File Offset: 0x0009A7B4
		private OdbcScalarExpression VisitTextPositionOf(InvocationQueryExpression expression)
		{
			OdbcScalarExpression odbcScalarExpression3;
			using (this.trace.NewScope("VisitTextPositionOf"))
			{
				if (!this.trace.ArgumentCountEquals(expression, 2))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				OdbcScalarExpression asScalar = this.Visit(expression.Arguments[0]).AsScalar;
				OdbcScalarExpression asScalar2 = this.Visit(expression.Arguments[1]).AsScalar;
				OdbcScalarExpression odbcScalarExpression;
				if (!this.TryLocate(expression, 1, asScalar2, 0, asScalar, out odbcScalarExpression))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				OdbcScalarExpression odbcScalarExpression2 = this.VisitConstant(NumberValue.One);
				odbcScalarExpression3 = this.VisitBinaryScalarOperation(odbcScalarExpression, BinaryOperator2.Subtract, odbcScalarExpression2);
			}
			return odbcScalarExpression3;
		}

		// Token: 0x0600321E RID: 12830 RVA: 0x0009C674 File Offset: 0x0009A874
		private bool TryLike(InvocationQueryExpression expression, int valueArgumentIndex, OdbcScalarExpression value, OdbcScalarExpression pattern, OdbcScalarExpression escapeCharacter, out OdbcConditionExpression result)
		{
			if (this.trace.DataSourceInfo.Supports(Odbc32.SQL_SP.SQL_SP_LIKE) && this.trace.ArgumentTypeKindIs(expression, valueArgumentIndex, value, ValueKind.Text) && pattern.TypeValue.TypeKind == ValueKind.Text && escapeCharacter.TypeValue.TypeKind == ValueKind.Text && this.trace.WhenNot<FoldingWarnings.FoldingWarning<string>>(this.dataSource.Info.SupportsStringLiterals, FoldingWarnings.SqlCapabilities("SupportsStringLiterals")))
			{
				OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult compatibilityAdjustmentResult = this.AdjustTextValuesForCompatibility(value, pattern);
				result = new OdbcConditionExpression(new LikePredicate(compatibilityAdjustmentResult.Left.Expression, compatibilityAdjustmentResult.Right.Expression, escapeCharacter.Expression));
				return true;
			}
			result = null;
			return false;
		}

		// Token: 0x0600321F RID: 12831 RVA: 0x0009C72C File Offset: 0x0009A92C
		protected virtual bool TryLocate(InvocationQueryExpression expression, int substringArgumentIndex, OdbcScalarExpression substring, int valueArgumentIndex, OdbcScalarExpression value, out OdbcScalarExpression result)
		{
			if (this.trace.DataSourceInfo.Supports(Odbc32.SQL_FN_STR.SQL_FN_STR_LOCATE_2) && this.trace.ArgumentTypeKindIs(expression, substringArgumentIndex, substring, ValueKind.Text) && this.trace.ArgumentTypeKindIs(expression, valueArgumentIndex, value, ValueKind.Text) && this.trace.ArgumentNotNullable(expression, substringArgumentIndex, substring))
			{
				OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult compatibilityAdjustmentResult = this.AdjustTextValuesForCompatibility(substring, value);
				OdbcDerivedColumnTypeInfo odbcDerivedColumnTypeInfo;
				if (this.TryGetColumnType(this.IntegerType.SqlType, value.TypeInfo.IsNullable, out odbcDerivedColumnTypeInfo))
				{
					result = new OdbcScalarExpression(this.NewColumnType(this.IntegerType.SqlType, value.TypeInfo.IsNullable), OdbcQueryExpressionVisitor.Call(new OdbcScalarFunctionReference(SqlLanguageStrings.LocateSqlString), new SqlExpression[]
					{
						compatibilityAdjustmentResult.Left.Expression,
						compatibilityAdjustmentResult.Right.Expression
					}));
					return true;
				}
			}
			return this.TryLocate(expression, substringArgumentIndex, substring, valueArgumentIndex, value, this.VisitConstant(NumberValue.One), out result);
		}

		// Token: 0x06003220 RID: 12832 RVA: 0x0009C830 File Offset: 0x0009AA30
		protected virtual bool TryLocate(InvocationQueryExpression expression, int substringArgumentIndex, OdbcScalarExpression substring, int valueArgumentIndex, OdbcScalarExpression value, OdbcScalarExpression start, out OdbcScalarExpression result)
		{
			if (this.trace.DataSourceInfo.Supports(Odbc32.SQL_FN_STR.SQL_FN_STR_LOCATE) && this.trace.ArgumentTypeKindIs(expression, substringArgumentIndex, substring, ValueKind.Text) && this.trace.ArgumentTypeKindIs(expression, valueArgumentIndex, value, ValueKind.Text) && this.trace.ArgumentNotNullable(expression, substringArgumentIndex, substring))
			{
				OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult compatibilityAdjustmentResult = this.AdjustTextValuesForCompatibility(substring, value);
				OdbcDerivedColumnTypeInfo odbcDerivedColumnTypeInfo;
				if (this.TryGetColumnType(this.IntegerType.SqlType, value.TypeInfo.IsNullable, out odbcDerivedColumnTypeInfo))
				{
					result = new OdbcScalarExpression(odbcDerivedColumnTypeInfo, OdbcQueryExpressionVisitor.Call(new OdbcScalarFunctionReference(SqlLanguageStrings.LocateSqlString), new SqlExpression[]
					{
						compatibilityAdjustmentResult.Left.Expression,
						compatibilityAdjustmentResult.Right.Expression,
						start.Expression
					}));
					return true;
				}
			}
			result = null;
			return false;
		}

		// Token: 0x06003221 RID: 12833 RVA: 0x0009C904 File Offset: 0x0009AB04
		private OdbcScalarExpression VisitListOrTableRowCount(InvocationQueryExpression invocation)
		{
			OdbcScalarExpression odbcScalarExpression3;
			using (this.trace.NewScope("VisitListOrTableRowCount"))
			{
				if (!this.trace.ArgumentCountEquals(invocation, 1) || this.trace.When(!this.allowAggregates, "Aggregates are not allowed.") || !this.trace.DataSourceInfo.Supports(Odbc32.SQL_AF.SQL_AF_COUNT))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				bool flag;
				bool flag2;
				OdbcScalarExpression odbcScalarExpression = new OdbcScalarExpression(this.NewColumnType(this.IntegerType.SqlType, false), OdbcQueryExpressionVisitor.Call(new BuiltInFunctionReference(SqlLanguageStrings.CountSqlString), new SqlExpression[] { this.GetCountArgument(invocation, true, out flag, out flag2) }));
				if (flag2)
				{
					QueryExpression queryExpression = ((InvocationQueryExpression)invocation.Arguments[0]).Arguments[0];
					BinaryQueryExpression binaryQueryExpression = new BinaryQueryExpression(BinaryOperator2.Equals, queryExpression, ConstantQueryExpression.Null);
					OdbcScalarExpression odbcScalarExpression2 = this.VisitMax(invocation, new IfQueryExpression(binaryQueryExpression, OdbcQueryExpressionVisitor.oneExpression, OdbcQueryExpressionVisitor.zeroExpression));
					odbcScalarExpression3 = this.VisitBinaryScalarOperation(odbcScalarExpression, BinaryOperator2.Add, odbcScalarExpression2);
				}
				else
				{
					odbcScalarExpression3 = odbcScalarExpression;
				}
			}
			return odbcScalarExpression3;
		}

		// Token: 0x06003222 RID: 12834 RVA: 0x0009CA24 File Offset: 0x0009AC24
		private SqlExpression GetCountArgument(InvocationQueryExpression expression, bool countNulls, out bool nullable, out bool addOneIfNull)
		{
			using (this.trace.NewScope("GetCountArgument"))
			{
				nullable = true;
				addOneIfNull = false;
				QueryExpression queryExpression = expression.Arguments[0];
				switch (queryExpression.Kind)
				{
				case QueryExpressionKind.ColumnAccess:
					if (!countNulls)
					{
						return this.Visit(queryExpression).AsScalar.Expression;
					}
					break;
				case QueryExpressionKind.If:
				case QueryExpressionKind.Unary:
					goto IL_0193;
				case QueryExpressionKind.Invocation:
				{
					InvocationQueryExpression invocationQueryExpression = queryExpression as InvocationQueryExpression;
					Value value;
					if (invocationQueryExpression == null || !invocationQueryExpression.Function.TryGetConstant(out value) || !value.IsFunction)
					{
						goto IL_0193;
					}
					if (value.AsFunction.FunctionIdentity.Equals(LanguageLibrary.List.Distinct.FunctionIdentity) && invocationQueryExpression.Arguments.Count == 1)
					{
						if (!this.trace.DataSourceInfo.Supports(Odbc32.SQL_AF.SQL_AF_DISTINCT))
						{
							throw this.trace.NewFoldingFailureException(null);
						}
						bool flag;
						bool flag2;
						SqlExpression countArgument = this.GetCountArgument(invocationQueryExpression, false, out flag, out flag2);
						addOneIfNull = countNulls && flag;
						return OdbcQueryExpressionVisitor.Call(new BuiltInFunctionReference(SqlLanguageStrings.DistinctSqlString), new SqlExpression[] { countArgument });
					}
					else
					{
						Value value2;
						Dictionary<string, IExpression> dictionary;
						if (value.AsFunction.FunctionIdentity.Equals(LanguageLibrary.List.Select.FunctionIdentity) && invocationQueryExpression.Arguments.Count == 2 && invocationQueryExpression.Arguments[1].TryGetConstant(out value2) && OdbcQueryExpressionVisitor.valueIsNotNullPattern.TryMatch(value2.Expression, out dictionary))
						{
							nullable = false;
							bool flag3;
							return this.GetCountArgument(invocationQueryExpression, false, out flag3, out flag3);
						}
						goto IL_0193;
					}
					break;
				}
				case QueryExpressionKind.ArgumentAccess:
					break;
				default:
					goto IL_0193;
				}
				return this.VisitConstant(NumberValue.One).Expression;
				IL_0193:
				throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<int, FoldingWarnings.StringFormatter<InvocationQueryExpression>, string>>(FoldingWarnings.InvalidArgumentValue(expression, 0, "the requirement of function specification"));
			}
			SqlExpression sqlExpression;
			return sqlExpression;
		}

		// Token: 0x06003223 RID: 12835 RVA: 0x0009CC04 File Offset: 0x0009AE04
		private OdbcScalarExpression VisitListFirst(InvocationQueryExpression expression)
		{
			using (this.trace.NewScope("VisitListFirst"))
			{
				if (this.trace.ArgumentCountEquals(expression, 1) && this.trace.ArgumentQueryExpressionKindIs(expression, 0, QueryExpressionKind.ColumnAccess) && this.trace.WhenNot(this.allowAggregates, "Aggregates are not allowed."))
				{
					ColumnAccessQueryExpression columnAccessQueryExpression = expression.Arguments[0] as ColumnAccessQueryExpression;
					if (this.trace.WhenNot<FoldingWarnings.FoldingWarning<int, FoldingWarnings.StringFormatter<InvocationQueryExpression>, string>>(this.IsGroupKeyColumn(columnAccessQueryExpression.Column), FoldingWarnings.InvalidArgumentValue(expression, 0, "being group key column")))
					{
						return this.Visit(expression.Arguments[0]).AsScalar;
					}
				}
				throw this.trace.NewFoldingFailureException(null);
			}
			OdbcScalarExpression odbcScalarExpression;
			return odbcScalarExpression;
		}

		// Token: 0x06003224 RID: 12836 RVA: 0x0009CCD4 File Offset: 0x0009AED4
		private bool IsGroupKeyColumn(int column)
		{
			return this.groupKey != null && Array.IndexOf<int>(this.groupKey, column) != -1;
		}

		// Token: 0x06003225 RID: 12837 RVA: 0x0009CCF4 File Offset: 0x0009AEF4
		private OdbcConditionExpression VisitListContains(InvocationQueryExpression expression)
		{
			using (this.trace.NewScope("VisitListContains"))
			{
				if (this.trace.ArgumentCountEquals(expression, 2))
				{
					ConstantQueryExpression constantQueryExpression = expression.Arguments[0] as ConstantQueryExpression;
					if (this.trace.WhenNot<FoldingWarnings.FoldingWarning<int, FoldingWarnings.StringFormatter<InvocationQueryExpression>, string>>(constantQueryExpression != null && constantQueryExpression.Value.IsList, FoldingWarnings.InvalidArgumentValue(expression, 0, "being constant List value")))
					{
						OdbcScalarExpression asScalar = this.Visit(expression.Arguments[1]).AsScalar;
						List<OdbcScalarExpression> list = new List<OdbcScalarExpression>();
						Condition condition = null;
						foreach (IValueReference valueReference in constantQueryExpression.Value.AsList)
						{
							if (valueReference.Value.IsNull)
							{
								if (condition == null)
								{
									condition = this.IsNull(asScalar.Expression);
								}
							}
							else
							{
								list.Add(this.VisitConstant(valueReference.Value));
							}
						}
						if (list.Count == 0 && condition != null)
						{
							return new OdbcConditionExpression(condition);
						}
						Condition condition2;
						if (list.Count != 1)
						{
							condition2 = this.In(asScalar.Expression, new InArrayExpression(list.Select((OdbcScalarExpression element) => element.Expression)));
						}
						else
						{
							condition2 = this.VisitEquals(asScalar, list[0], Precision.Double, true);
						}
						Condition condition3 = condition2;
						return new OdbcConditionExpression((condition != null) ? this.Condition(ConditionOperator.Or, new Condition[] { condition3, condition }) : condition3);
					}
				}
				throw this.trace.NewFoldingFailureException(null);
			}
			OdbcConditionExpression odbcConditionExpression;
			return odbcConditionExpression;
		}

		// Token: 0x06003226 RID: 12838 RVA: 0x0009CED4 File Offset: 0x0009B0D4
		private OdbcSqlExpression VisitValueAs(InvocationQueryExpression expression)
		{
			using (this.trace.NewScope("VisitValueAs"))
			{
				if (this.trace.ArgumentCountEquals(expression, 2) && this.trace.ArgumentQueryExpressionKindIs(expression, 1, QueryExpressionKind.Constant))
				{
					OdbcSqlExpression odbcSqlExpression = this.Visit(expression.Arguments[0]);
					ConstantQueryExpression constantQueryExpression = expression.Arguments[1] as ConstantQueryExpression;
					if (this.trace.WhenNot<FoldingWarnings.FoldingWarning<int, FoldingWarnings.StringFormatter<InvocationQueryExpression>, string>>(constantQueryExpression != null && constantQueryExpression.Value.IsType, FoldingWarnings.InvalidArgumentValue(expression, 1, "being Type value")) && this.trace.WhenNot<FoldingWarnings.FoldingWarning<int, FoldingWarnings.StringFormatter<InvocationQueryExpression>, string>>(odbcSqlExpression.TypeValue.IsCompatibleWith(constantQueryExpression.Value.AsType), FoldingWarnings.InvalidArgumentValue(expression, 0, "being compatible type")))
					{
						return odbcSqlExpression;
					}
				}
				throw this.trace.NewFoldingFailureException(null);
			}
			OdbcSqlExpression odbcSqlExpression2;
			return odbcSqlExpression2;
		}

		// Token: 0x06003227 RID: 12839 RVA: 0x0009CFC4 File Offset: 0x0009B1C4
		private OdbcSqlExpression VisitValueReplaceType(InvocationQueryExpression expression)
		{
			return this.VisitValueAs(expression);
		}

		// Token: 0x06003228 RID: 12840 RVA: 0x0009CFD0 File Offset: 0x0009B1D0
		private OdbcSqlExpression VisitDateFrom(InvocationQueryExpression invocation)
		{
			using (this.trace.NewScope("VisitDateFrom"))
			{
				if (this.trace.ArgumentCountEquals(invocation, 1))
				{
					OdbcScalarExpression asScalar = this.Visit(invocation.Arguments[0]).AsScalar;
					switch (asScalar.TypeValue.TypeKind)
					{
					case ValueKind.Date:
						return asScalar;
					case ValueKind.DateTime:
						return this.Convert(this.DateType, asScalar);
					case ValueKind.Number:
						return new OdbcScalarExpression(new OdbcDerivedColumnTypeInfo(this.DateType, asScalar.TypeInfo.IsNullable, null, null), this.VisitTimestampAdd(Odbc32.SQL_TSI.SQL_TSI_DAY, this.Convert(this.IntegerType, asScalar).Expression, this.VisitConstant(this.DateType, DateValue.New(OdbcQueryExpressionVisitor.oleDBEpoch))));
					}
					this.trace.Trace<FoldingWarnings.FoldingWarning<int, FoldingWarnings.StringFormatter<InvocationQueryExpression>, string>>(FoldingWarnings.InvalidArgumentValue(invocation, 0, "being Number, Date or DateTime"));
				}
				throw this.trace.NewFoldingFailureException(null);
			}
			OdbcSqlExpression odbcSqlExpression;
			return odbcSqlExpression;
		}

		// Token: 0x06003229 RID: 12841 RVA: 0x0009D0FC File Offset: 0x0009B2FC
		private OdbcSqlExpression VisitDateTimeFrom(InvocationQueryExpression invocation)
		{
			using (this.trace.NewScope("VisitDateTimeFrom"))
			{
				if (this.trace.ArgumentCountEquals(invocation, 1))
				{
					OdbcScalarExpression asScalar = this.Visit(invocation.Arguments[0]).AsScalar;
					switch (asScalar.TypeValue.TypeKind)
					{
					case ValueKind.Time:
						return this.VisitDateTimeFromTime(invocation.Arguments[0], asScalar);
					case ValueKind.Date:
						return this.VisitDateTimeFromDate(asScalar);
					case ValueKind.DateTime:
						return asScalar;
					case ValueKind.Number:
					{
						if (this.dataSource.Info.FractionalSecondsPerSecond == null)
						{
							throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string>>(FoldingWarnings.SqlCapabilities("FractionalSecondsPerSecond"));
						}
						if (!this.trace.DataSourceInfo.Supports(Odbc32.SQL_FUN_NUM.SQL_FN_NUM_ABS) || !this.trace.DataSourceInfo.Supports(Odbc32.SQL_FUN_NUM.SQL_FN_NUM_FLOOR))
						{
							throw this.trace.NewFoldingFailureException(null);
						}
						SqlExpression sqlExpression = OdbcQueryExpressionVisitor.Call(new OdbcScalarFunctionReference(SqlLanguageStrings.FloorSqlString), new SqlExpression[] { asScalar.Expression });
						SqlExpression sqlExpression2 = this.VisitTimestampAdd(Odbc32.SQL_TSI.SQL_TSI_DAY, sqlExpression, this.VisitConstant(this.TimestampType, DateTimeValue.New(OdbcQueryExpressionVisitor.oleDBEpoch)));
						SqlExpression sqlExpression3 = new BinaryScalarOperation(OdbcQueryExpressionVisitor.Call(new OdbcScalarFunctionReference(SqlLanguageStrings.AbsSqlString), new SqlExpression[] { asScalar.Expression }), BinaryScalarOperator.Subtract, OdbcQueryExpressionVisitor.Call(new OdbcScalarFunctionReference(SqlLanguageStrings.FloorSqlString), new SqlExpression[] { OdbcQueryExpressionVisitor.Call(new OdbcScalarFunctionReference(SqlLanguageStrings.AbsSqlString), new SqlExpression[] { asScalar.Expression }) }));
						CaseFunction caseFunction = this.Case();
						caseFunction.WhenItems.Add(new WhenItem
						{
							When = this.Condition(ConditionOperator.And, new Condition[]
							{
								new BinaryLogicalOperation(asScalar.Expression, BinaryLogicalOperator.LessThan, SqlConstant.Zero),
								new BinaryLogicalOperation(sqlExpression3, BinaryLogicalOperator.NotEqualTo, SqlConstant.Zero)
							}),
							Then = SqlConstant.One
						});
						caseFunction.ElseExpression = SqlConstant.Zero;
						SqlExpression sqlExpression4 = this.VisitConstantDouble(this.DoubleType, NumberValue.New(86400L * (long)this.dataSource.Info.FractionalSecondsPerSecond.Value));
						SqlExpression sqlExpression5 = new BinaryScalarOperation(new BinaryScalarOperation(sqlExpression3, BinaryScalarOperator.Add, caseFunction), BinaryScalarOperator.Multiply, sqlExpression4);
						SqlExpression sqlExpression6 = this.VisitTimestampAdd(Odbc32.SQL_TSI.SQL_TSI_FRAC_SECOND, sqlExpression5, sqlExpression2);
						return new OdbcScalarExpression(new OdbcDerivedColumnTypeInfo(this.TimestampType, asScalar.TypeInfo.IsNullable, null, null), sqlExpression6);
					}
					}
					this.trace.Trace<FoldingWarnings.FoldingWarning<int, FoldingWarnings.StringFormatter<InvocationQueryExpression>, string>>(FoldingWarnings.InvalidArgumentValue(invocation, 0, "being Number, Date or DateTime"));
				}
				throw this.trace.NewFoldingFailureException(null);
			}
			OdbcSqlExpression odbcSqlExpression;
			return odbcSqlExpression;
		}

		// Token: 0x0600322A RID: 12842 RVA: 0x0009D3E4 File Offset: 0x0009B5E4
		private OdbcScalarExpression VisitDateTimeFromDate(OdbcScalarExpression expression)
		{
			OdbcScalarExpression odbcScalarExpression;
			if (this.TryConvert(this.TimestampType, expression, out odbcScalarExpression))
			{
				return odbcScalarExpression;
			}
			return this.VisitTimestampAdd(Odbc32.SQL_TSI.SQL_TSI_DAY, this.VisitTimestampDiff(expression, this.VisitConstant(DateValue.New(OdbcQueryExpressionVisitor.startOfYearDateTime)), Odbc32.SQL_TSI.SQL_TSI_DAY), this.VisitConstant(DateTimeValue.New(OdbcQueryExpressionVisitor.startOfYearDateTime)));
		}

		// Token: 0x0600322B RID: 12843 RVA: 0x0009D438 File Offset: 0x0009B638
		private OdbcScalarExpression VisitDateTimeFromTime(QueryExpression timeExpression, OdbcScalarExpression odbcExpression)
		{
			OdbcScalarExpression odbcScalarExpression;
			if (this.TryConvert(this.TimestampType, odbcExpression, out odbcScalarExpression))
			{
				return odbcScalarExpression;
			}
			return this.VisitTimestampAdd(Odbc32.SQL_TSI.SQL_TSI_SECOND, this.VisitInvocation(Library.Time.Second, new QueryExpression[] { timeExpression }).AsScalar, this.VisitTimestampAdd(Odbc32.SQL_TSI.SQL_TSI_MINUTE, this.VisitInvocation(Library.Time.Minute, new QueryExpression[] { timeExpression }).AsScalar, this.VisitTimestampAdd(Odbc32.SQL_TSI.SQL_TSI_HOUR, this.VisitInvocation(Library.Time.Hour, new QueryExpression[] { timeExpression }).AsScalar, this.VisitConstant(DateTimeValue.New(OdbcQueryExpressionVisitor.oleDBEpoch)))));
		}

		// Token: 0x0600322C RID: 12844 RVA: 0x0009D4CC File Offset: 0x0009B6CC
		private OdbcSqlExpression VisitDurationConstructor(InvocationQueryExpression invocation)
		{
			OdbcSqlExpression odbcSqlExpression;
			using (this.trace.NewScope("VisitDurationConstructor"))
			{
				if (!this.trace.ArgumentCountEquals(invocation, 4))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				long num = 0L;
				long[] array = new long[] { 864000000000L, 36000000000L, 600000000L, 10000000L };
				bool[] array2 = new bool[4];
				for (int i = 0; i < invocation.Arguments.Count; i++)
				{
					Value value;
					if (invocation.Arguments[i].TryGetConstant(out value))
					{
						if (this.trace.When<FoldingWarnings.FoldingWarning<int, FoldingWarnings.StringFormatter<InvocationQueryExpression>, string>>(!value.IsNumber, FoldingWarnings.InvalidArgumentType(invocation, i, "Number")) || this.trace.When<FoldingWarnings.FoldingWarning<int, FoldingWarnings.StringFormatter<InvocationQueryExpression>, string>>(!value.AsNumber.IsInteger32 && i != 3, FoldingWarnings.InvalidArgumentType(invocation, i, "Integer32")))
						{
							throw this.trace.NewFoldingFailureException(null);
						}
						array2[i] = true;
						num += (long)((double)array[i] * value.AsNumber.AsDouble);
					}
				}
				SqlExpression sqlExpression = null;
				for (int j = 0; j < invocation.Arguments.Count; j++)
				{
					if (!array2[j])
					{
						OdbcScalarExpression asScalar = this.Visit(invocation.Arguments[j]).AsScalar;
						if (!this.trace.ArgumentTypeKindIs(invocation, j, asScalar, ValueKind.Number))
						{
							throw this.trace.NewFoldingFailureException(null);
						}
						SqlExpression sqlExpression2;
						if (j != 3)
						{
							sqlExpression2 = new BinaryScalarOperation(this.Convert(this.BigIntType, asScalar).Expression, BinaryScalarOperator.Multiply, this.VisitConstantInt64(this.BigIntType, NumberValue.New(array[j])));
						}
						else
						{
							OdbcScalarExpression odbcScalarExpression = this.VisitConstant(NumberValue.New(array[j]));
							OdbcScalarExpression odbcScalarExpression2 = this.VisitNumberNumberBinaryScalarOperation(asScalar, BinaryOperator2.Multiply, odbcScalarExpression);
							sqlExpression2 = this.Convert(this.BigIntType, odbcScalarExpression2).Expression;
						}
						if (sqlExpression == null)
						{
							sqlExpression = sqlExpression2;
						}
						else
						{
							sqlExpression = new BinaryScalarOperation(sqlExpression, BinaryScalarOperator.Add, sqlExpression2);
						}
					}
				}
				SqlExpression sqlExpression3;
				if (sqlExpression == null)
				{
					sqlExpression3 = this.VisitConstantInt64(this.BigIntType, NumberValue.New(num));
				}
				else if (num != 0L)
				{
					sqlExpression3 = new BinaryScalarOperation(this.VisitConstantInt64(this.BigIntType, NumberValue.New(num)), BinaryScalarOperator.Add, sqlExpression);
				}
				else
				{
					sqlExpression3 = sqlExpression;
				}
				odbcSqlExpression = new OdbcScalarExpression(this.DurationType, sqlExpression3);
			}
			return odbcSqlExpression;
		}

		// Token: 0x0600322D RID: 12845 RVA: 0x0009D738 File Offset: 0x0009B938
		private OdbcSqlExpression VisitDurationFrom(InvocationQueryExpression invocation)
		{
			OdbcSqlExpression odbcSqlExpression;
			using (this.trace.NewScope("VisitDurationFrom"))
			{
				if (!this.trace.ArgumentCountEquals(invocation, 1))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				OdbcScalarExpression asScalar = this.Visit(invocation.Arguments[0]).AsScalar;
				if (!this.trace.ArgumentTypeKindIs(invocation, 0, asScalar, ValueKind.Number))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				OdbcScalarExpression odbcScalarExpression = this.VisitNumberNumberBinaryScalarOperation(asScalar, BinaryOperator2.Multiply, this.VisitConstant(NumberValue.New(864000000000L)));
				OdbcScalarExpression odbcScalarExpression2 = this.Convert(this.BigIntType, odbcScalarExpression);
				odbcSqlExpression = new OdbcScalarExpression(this.DurationType, odbcScalarExpression2.Expression);
			}
			return odbcSqlExpression;
		}

		// Token: 0x0600322E RID: 12846 RVA: 0x0009D804 File Offset: 0x0009BA04
		private OdbcSqlExpression VisitTimeSecond(InvocationQueryExpression invocation)
		{
			OdbcSqlExpression odbcSqlExpression;
			using (this.trace.NewScope("VisitTimeSecond"))
			{
				if (!this.trace.ArgumentCountEquals(invocation, 1))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				OdbcScalarExpression asScalar = this.Visit(invocation.Arguments[0]).AsScalar;
				if ((!this.trace.ArgumentTypeKindIs(invocation, 0, asScalar, ValueKind.DateTime) && !this.trace.ArgumentTypeKindIs(invocation, 0, asScalar, ValueKind.Time)) || !this.trace.WhenNot<FoldingWarnings.FoldingWarning<string>>(this.dataSource.Info.FractionalSecondsPerSecond != null, FoldingWarnings.SqlCapabilities("FractionalSecondsPerSecond")))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				SqlExpression sqlExpression2;
				if (asScalar.TypeValue.TypeKind == ValueKind.Time)
				{
					SqlExpression sqlExpression = this.VisitTimestampDiff(asScalar.Expression, this.VisitConstant(this.TimeType, TimeValue.New(OdbcQueryExpressionVisitor.startOfYearDateTime)), Odbc32.SQL_TSI.SQL_TSI_MINUTE);
					sqlExpression2 = this.VisitTimestampAdd(Odbc32.SQL_TSI.SQL_TSI_MINUTE, sqlExpression, this.VisitConstant(this.TimeType, TimeValue.New(OdbcQueryExpressionVisitor.startOfYearDateTime)));
				}
				else
				{
					SqlExpression sqlExpression = this.VisitTimestampDiff(asScalar.Expression, this.VisitConstant(this.TimestampType, DateTimeValue.New(OdbcQueryExpressionVisitor.startOfYearDateTime)), Odbc32.SQL_TSI.SQL_TSI_MINUTE);
					sqlExpression2 = this.VisitTimestampAdd(Odbc32.SQL_TSI.SQL_TSI_MINUTE, sqlExpression, this.VisitConstant(this.TimestampType, DateTimeValue.New(OdbcQueryExpressionVisitor.startOfYearDateTime)));
				}
				SqlExpression sqlExpression3 = this.VisitTimestampDiff(asScalar.Expression, sqlExpression2, Odbc32.SQL_TSI.SQL_TSI_FRAC_SECOND);
				SqlExpression sqlExpression4 = new BinaryScalarOperation(this.VisitConvert(this.IntegerType, this.DoubleType, sqlExpression3), BinaryScalarOperator.Divide, this.VisitConstantDouble(this.DoubleType, NumberValue.New(this.dataSource.Info.FractionalSecondsPerSecond.Value)));
				odbcSqlExpression = new OdbcScalarExpression(new OdbcDerivedColumnTypeInfo(this.DoubleType, asScalar.TypeInfo.IsNullable, this.DoubleType.ColumnSize, null), sqlExpression4);
			}
			return odbcSqlExpression;
		}

		// Token: 0x0600322F RID: 12847 RVA: 0x0009DA0C File Offset: 0x0009BC0C
		private Func<InvocationQueryExpression, OdbcSqlExpression> NewAggregateFunctionVisitor(Odbc32.SQL_AF capabilityFlag, ConstantSqlString functionName, bool convert, bool isNumeric)
		{
			return (InvocationQueryExpression e) => this.VisitAggregateInvocation(e, capabilityFlag, functionName, convert, isNumeric);
		}

		// Token: 0x06003230 RID: 12848 RVA: 0x0009DA42 File Offset: 0x0009BC42
		private Func<InvocationQueryExpression, OdbcSqlExpression> NewNumericFunctionVisitor(Odbc32.SQL_FUN_NUM capabilityFlag, ConstantSqlString functionName, params bool[] convertToDouble)
		{
			return (InvocationQueryExpression e) => this.VisitNumericFunction(e, capabilityFlag, functionName, convertToDouble);
		}

		// Token: 0x06003231 RID: 12849 RVA: 0x0009DA70 File Offset: 0x0009BC70
		private Func<InvocationQueryExpression, OdbcSqlExpression> NewRoundingFunctionVisitor(OdbcQueryExpressionVisitor.ExpressionVisitor visitor)
		{
			return delegate(InvocationQueryExpression e)
			{
				OdbcSqlExpression odbcSqlExpression;
				using (this.trace.NewScope("NewRoundingFunctionVisitor"))
				{
					if (!this.trace.ArgumentCountBetween(e, 1, 2))
					{
						throw this.trace.NewFoldingFailureException(null);
					}
					OdbcScalarExpression odbcScalarExpression = this.Visit(e.Arguments[0]).AsScalar;
					OdbcScalarExpression odbcScalarExpression2 = null;
					if (e.Arguments.Count == 2)
					{
						QueryExpression queryExpression = e.Arguments[1];
						odbcScalarExpression2 = this.VisitInvocation(Library.Number.Power, new QueryExpression[]
						{
							new ConstantQueryExpression(NumberValue.New(10.0)),
							queryExpression
						}).AsScalar;
						odbcScalarExpression = this.VisitBinaryScalarOperation(odbcScalarExpression, BinaryOperator2.Multiply, odbcScalarExpression2);
					}
					OdbcScalarExpression odbcScalarExpression3;
					if (!visitor(e, odbcScalarExpression, out odbcScalarExpression3))
					{
						throw this.trace.NewFoldingFailureException(null);
					}
					if (e.Arguments.Count == 2)
					{
						odbcScalarExpression3 = this.VisitBinaryScalarOperation(odbcScalarExpression3, BinaryOperator2.Divide, odbcScalarExpression2);
					}
					odbcSqlExpression = odbcScalarExpression3;
				}
				return odbcSqlExpression;
			};
		}

		// Token: 0x06003232 RID: 12850 RVA: 0x0009DA90 File Offset: 0x0009BC90
		private bool TryRound(InvocationQueryExpression expression, OdbcScalarExpression argument, out OdbcScalarExpression rounded)
		{
			return this.TryVisitRoundingFunction(expression, Odbc32.SQL_FUN_NUM.SQL_FN_NUM_ROUND, SqlLanguageStrings.RoundSqlString, out rounded, new OdbcScalarExpression[]
			{
				argument,
				this.VisitConstant(NumberValue.Zero)
			});
		}

		// Token: 0x06003233 RID: 12851 RVA: 0x0009DAC8 File Offset: 0x0009BCC8
		private bool TryRoundUp(InvocationQueryExpression expression, OdbcScalarExpression argument, out OdbcScalarExpression rounded)
		{
			return this.TryVisitRoundingFunction(expression, Odbc32.SQL_FUN_NUM.SQL_FN_NUM_CEILING, SqlLanguageStrings.CeilingSqlString, out rounded, new OdbcScalarExpression[] { argument });
		}

		// Token: 0x06003234 RID: 12852 RVA: 0x0009DAF0 File Offset: 0x0009BCF0
		private bool TryRoundDown(InvocationQueryExpression expression, OdbcScalarExpression argument, out OdbcScalarExpression rounded)
		{
			return this.TryVisitRoundingFunction(expression, Odbc32.SQL_FUN_NUM.SQL_FN_NUM_FLOOR, SqlLanguageStrings.FloorSqlString, out rounded, new OdbcScalarExpression[] { argument });
		}

		// Token: 0x06003235 RID: 12853 RVA: 0x0009DB1C File Offset: 0x0009BD1C
		private bool ArgumentsTypeKindAre(InvocationQueryExpression expression, OdbcScalarExpression[] args, ValueKind kind)
		{
			for (int i = 0; i < args.Length; i++)
			{
				if (!this.trace.ArgumentTypeKindIs(expression, i, args[i], kind))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003236 RID: 12854 RVA: 0x0009DB50 File Offset: 0x0009BD50
		private bool TryVisitRoundingFunction(InvocationQueryExpression expression, Odbc32.SQL_FUN_NUM odbcFunction, ConstantSqlString name, out OdbcScalarExpression result, params OdbcScalarExpression[] args)
		{
			OdbcScalarExpression odbcScalarExpression = args[0];
			if (odbcScalarExpression.TypeInfo.IsWholeNumber)
			{
				result = odbcScalarExpression;
				return true;
			}
			if (!this.trace.DataSourceInfo.Supports(odbcFunction) || !this.ArgumentsTypeKindAre(expression, args, ValueKind.Number))
			{
				result = null;
				return false;
			}
			OdbcDerivedColumnTypeInfo odbcDerivedColumnTypeInfo = odbcScalarExpression.TypeInfo;
			if (args.Any((OdbcScalarExpression p) => p.TypeInfo.IsNullable))
			{
				odbcDerivedColumnTypeInfo = odbcDerivedColumnTypeInfo.AsNullable;
			}
			result = new OdbcScalarExpression(odbcDerivedColumnTypeInfo, OdbcQueryExpressionVisitor.Call(new OdbcScalarFunctionReference(name), args.Select((OdbcScalarExpression a) => a.Expression).ToArray<SqlExpression>()));
			return true;
		}

		// Token: 0x06003237 RID: 12855 RVA: 0x0009DC10 File Offset: 0x0009BE10
		private Func<InvocationQueryExpression, OdbcSqlExpression> NewPrecisionNumericFunctionVisitor(BinaryScalarOperator operation)
		{
			return (InvocationQueryExpression e) => this.VisitPrecisionNumericFunction(e, operation);
		}

		// Token: 0x06003238 RID: 12856 RVA: 0x0009DC30 File Offset: 0x0009BE30
		private Func<InvocationQueryExpression, OdbcSqlExpression> NewDateAdditionFunctionVisitor(Odbc32.SQL_TSI intervalDimension)
		{
			return (InvocationQueryExpression e) => this.VisitDateAdditionFunction(e, intervalDimension);
		}

		// Token: 0x06003239 RID: 12857 RVA: 0x0009DC50 File Offset: 0x0009BE50
		private Func<InvocationQueryExpression, OdbcSqlExpression> NewDateStartOfFunctionVisitor(Odbc32.SQL_TSI intervalDimension, bool datetimeOnly = false)
		{
			return (InvocationQueryExpression e) => this.VisitDateStartOfFunction(e, intervalDimension, datetimeOnly);
		}

		// Token: 0x0600323A RID: 12858 RVA: 0x0009DC77 File Offset: 0x0009BE77
		private Func<InvocationQueryExpression, OdbcSqlExpression> NewDateEndOfFunctionVisitor(Odbc32.SQL_TSI intervalDimension, bool datetimeOnly = false)
		{
			return (InvocationQueryExpression e) => this.VisitDateEndOfFunction(e, intervalDimension, datetimeOnly);
		}

		// Token: 0x0600323B RID: 12859 RVA: 0x0009DCA0 File Offset: 0x0009BEA0
		private OdbcScalarExpression VisitDateEndOfFunction(InvocationQueryExpression expression, Odbc32.SQL_TSI intervalDimension, bool datetimeOnly)
		{
			OdbcScalarExpression odbcScalarExpression;
			using (this.trace.NewScope("VisitDateEndOfFunction"))
			{
				if (!this.trace.ArgumentCountEquals(expression, 1))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				OdbcScalarExpression asScalar = this.Visit(expression.Arguments[0]).AsScalar;
				Odbc32.SQL_TSI sql_TSI = ((asScalar.TypeInfo.TypeValue.TypeKind == ValueKind.Date) ? Odbc32.SQL_TSI.SQL_TSI_DAY : Odbc32.SQL_TSI.SQL_TSI_FRAC_SECOND);
				if (!this.trace.ArgumentTypeKindIs(expression, 0, asScalar, ValueKind.DateTime) && (datetimeOnly || !this.trace.ArgumentTypeKindIs(expression, 0, asScalar, ValueKind.Date)))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				SqlExpression sqlExpression = new BinaryScalarOperation(this.VisitTimestampDiff(asScalar.Expression, this.VisitConstant(this.TimestampType, DateTimeValue.New(OdbcQueryExpressionVisitor.startOfYearDateTime)), intervalDimension), BinaryScalarOperator.Add, SqlConstant.One);
				SqlExpression sqlExpression2 = this.VisitTimestampAdd(intervalDimension, sqlExpression, this.VisitConstant(this.TimestampType, DateTimeValue.New(OdbcQueryExpressionVisitor.startOfYearDateTime)));
				SqlExpression sqlExpression3 = this.VisitTimestampAdd(sql_TSI, SqlConstant.MinusOne, sqlExpression2);
				odbcScalarExpression = new OdbcScalarExpression(asScalar.TypeInfo, sqlExpression3);
			}
			return odbcScalarExpression;
		}

		// Token: 0x0600323C RID: 12860 RVA: 0x0009DDCC File Offset: 0x0009BFCC
		private OdbcScalarExpression VisitDateStartOfFunction(InvocationQueryExpression expression, Odbc32.SQL_TSI intervalDimension, bool datetimeOnly)
		{
			OdbcScalarExpression odbcScalarExpression;
			using (this.trace.NewScope("VisitDateStartOfFunction"))
			{
				if (!this.trace.ArgumentCountEquals(expression, 1))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				OdbcScalarExpression asScalar = this.Visit(expression.Arguments[0]).AsScalar;
				if (!this.trace.ArgumentTypeKindIs(expression, 0, asScalar, ValueKind.DateTime) && (datetimeOnly || !this.trace.ArgumentTypeKindIs(expression, 0, asScalar, ValueKind.Date)))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				SqlExpression sqlExpression = this.VisitTimestampDiff(asScalar.Expression, this.VisitConstant(this.TimestampType, DateTimeValue.New(OdbcQueryExpressionVisitor.startOfYearDateTime)), intervalDimension);
				SqlExpression sqlExpression2 = this.VisitTimestampAdd(intervalDimension, sqlExpression, this.VisitConstant(this.TimestampType, DateTimeValue.New(OdbcQueryExpressionVisitor.startOfYearDateTime)));
				odbcScalarExpression = new OdbcScalarExpression(asScalar.TypeInfo, sqlExpression2);
			}
			return odbcScalarExpression;
		}

		// Token: 0x0600323D RID: 12861 RVA: 0x0009DEC0 File Offset: 0x0009C0C0
		private OdbcScalarExpression VisitDateAdditionFunction(InvocationQueryExpression expression, Odbc32.SQL_TSI intervalDimension)
		{
			OdbcScalarExpression odbcScalarExpression;
			using (this.trace.NewScope("VisitDateAdditionFunction"))
			{
				if (!this.trace.ArgumentCountEquals(expression, 2))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				OdbcScalarExpression asScalar = this.Visit(expression.Arguments[0]).AsScalar;
				OdbcScalarExpression asScalar2 = this.Visit(expression.Arguments[1]).AsScalar;
				if ((!this.trace.ArgumentTypeKindIs(expression, 0, asScalar, ValueKind.Date) && !this.trace.ArgumentTypeKindIs(expression, 0, asScalar, ValueKind.DateTime)) || !this.trace.ArgumentSqlType(expression, 1, asScalar2, this.IntegerType.SqlType))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				odbcScalarExpression = new OdbcScalarExpression(asScalar.TypeInfo, this.VisitTimestampAdd(intervalDimension, asScalar2.Expression, asScalar.Expression));
			}
			return odbcScalarExpression;
		}

		// Token: 0x0600323E RID: 12862 RVA: 0x0009DFB0 File Offset: 0x0009C1B0
		private Func<InvocationQueryExpression, OdbcSqlExpression> NewDatePartFunctionVisitor(Odbc32.SQL_FN_TD capabilityFlag, ConstantSqlString functionName)
		{
			return (InvocationQueryExpression e) => this.VisitDatePartFunction(e, capabilityFlag, functionName);
		}

		// Token: 0x0600323F RID: 12863 RVA: 0x0009DFD7 File Offset: 0x0009C1D7
		private Func<InvocationQueryExpression, OdbcSqlExpression> NewTimePartFunctionVisitor(Odbc32.SQL_FN_TD capabilityFlag, ConstantSqlString functionName)
		{
			return (InvocationQueryExpression e) => this.VisitTimePartFunction(e, capabilityFlag, functionName);
		}

		// Token: 0x06003240 RID: 12864 RVA: 0x0009DFFE File Offset: 0x0009C1FE
		private Func<InvocationQueryExpression, OdbcSqlExpression> NewDurationPartFunctionVisitor(long intervalSize, int maxIntervals, bool integerResult)
		{
			return (InvocationQueryExpression e) => this.VisitDurationPartFunction(e, intervalSize, (long)maxIntervals, integerResult);
		}

		// Token: 0x06003241 RID: 12865 RVA: 0x0009E02C File Offset: 0x0009C22C
		private SqlExpression VisitMod(SqlExpression divided, SqlExpression divisor)
		{
			SqlExpression sqlExpression;
			using (this.trace.NewScope("VisitMod"))
			{
				if (!this.trace.DataSourceInfo.Supports(Odbc32.SQL_FUN_NUM.SQL_FN_NUM_MOD))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				sqlExpression = OdbcQueryExpressionVisitor.Call(new OdbcScalarFunctionReference(SqlLanguageStrings.ModSqlString), new SqlExpression[] { divided, divisor });
			}
			return sqlExpression;
		}

		// Token: 0x06003242 RID: 12866 RVA: 0x0009E0AC File Offset: 0x0009C2AC
		private OdbcScalarExpression VisitDurationPartFunction(InvocationQueryExpression expression, long intervalSize, long maxIntervals, bool integerResult)
		{
			OdbcScalarExpression odbcScalarExpression;
			using (this.trace.NewScope("VisitDurationPartFunction"))
			{
				if (!this.trace.ArgumentCountEquals(expression, 1))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				OdbcScalarExpression asScalar = this.Visit(expression.Arguments[0]).AsScalar;
				if (!this.trace.ArgumentTypeKindIs(expression, 0, asScalar, ValueKind.Duration))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				if (integerResult)
				{
					SqlExpression sqlExpression = new BinaryScalarOperation(asScalar.Expression, BinaryScalarOperator.Divide, this.VisitConstantInt64(this.BigIntType, NumberValue.New(intervalSize)));
					SqlExpression sqlExpression2 = null;
					if (maxIntervals != -1L)
					{
						sqlExpression2 = this.VisitMod(sqlExpression, this.VisitConstantInt64(this.BigIntType, NumberValue.New(maxIntervals)));
					}
					SqlExpression sqlExpression3 = this.VisitConvert(this.BigIntType, this.IntegerType, (maxIntervals == -1L) ? sqlExpression : sqlExpression2);
					odbcScalarExpression = new OdbcScalarExpression(this.NewColumnType(this.IntegerType.SqlType, asScalar.TypeInfo.IsNullable), sqlExpression3);
				}
				else
				{
					SqlExpression sqlExpression4 = null;
					if (maxIntervals != -1L)
					{
						sqlExpression4 = this.VisitMod(asScalar.Expression, this.VisitConstantInt64(this.BigIntType, NumberValue.New(maxIntervals * intervalSize)));
					}
					SqlExpression sqlExpression5 = new BinaryScalarOperation(this.VisitConvert(this.BigIntType, this.DoubleType, (maxIntervals != -1L) ? sqlExpression4 : asScalar.Expression), BinaryScalarOperator.Divide, this.VisitConstantDouble(this.DoubleType, NumberValue.New(intervalSize)));
					odbcScalarExpression = new OdbcScalarExpression(new OdbcDerivedColumnTypeInfo(this.DoubleType, asScalar.TypeInfo.IsNullable, this.DoubleType.ColumnSize, null), sqlExpression5);
				}
			}
			return odbcScalarExpression;
		}

		// Token: 0x06003243 RID: 12867 RVA: 0x0009E270 File Offset: 0x0009C470
		private OdbcScalarExpression VisitMod(InvocationQueryExpression expression)
		{
			OdbcScalarExpression odbcScalarExpression;
			using (this.trace.NewScope("VisitMod"))
			{
				if (!this.trace.ArgumentCountEquals(expression, 2))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				OdbcScalarExpression asScalar = this.Visit(expression.Arguments[0]).AsScalar;
				OdbcScalarExpression asScalar2 = this.Visit(expression.Arguments[1]).AsScalar;
				OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult compatibilityAdjustmentResult = this.AdjustNumberValuesForCompatibility(expression, 0, asScalar, 1, asScalar2, OdbcQueryExpressionVisitor.integerTypes);
				odbcScalarExpression = new OdbcScalarExpression(compatibilityAdjustmentResult.TypeInfo, this.VisitMod(compatibilityAdjustmentResult.Left.Expression, compatibilityAdjustmentResult.Right.Expression));
			}
			return odbcScalarExpression;
		}

		// Token: 0x06003244 RID: 12868 RVA: 0x0009E334 File Offset: 0x0009C534
		private OdbcScalarExpression VisitPrecisionNumericFunction(InvocationQueryExpression expression, BinaryScalarOperator operation)
		{
			OdbcDerivedColumnTypeInfo odbcDerivedColumnTypeInfo;
			SqlExpression[] array = this.VisitPrecisionNumericFunctionArguments(expression, out odbcDerivedColumnTypeInfo);
			return new OdbcScalarExpression(odbcDerivedColumnTypeInfo, new BinaryScalarOperation(array[0], operation, array[1]));
		}

		// Token: 0x06003245 RID: 12869 RVA: 0x0009E360 File Offset: 0x0009C560
		private OdbcScalarExpression VisitValueCompare(InvocationQueryExpression expression)
		{
			OdbcScalarExpression odbcScalarExpression2;
			using (this.trace.NewScope("VisitValueCompare"))
			{
				Precision precision;
				if (expression.Arguments.Count == 2)
				{
					precision = Precision.Double;
				}
				else
				{
					if (expression.Arguments.Count != 3)
					{
						throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<InvocationQueryExpression>, int, int>>(FoldingWarnings.InvalidArgumentsCount(expression, 2, 3));
					}
					precision = this.GetPrecision(expression, 2, expression.Arguments[2]);
				}
				OdbcScalarExpression asScalar = this.Visit(expression.Arguments[0]).AsScalar;
				OdbcScalarExpression asScalar2 = this.Visit(expression.Arguments[1]).AsScalar;
				OdbcScalarExpression odbcScalarExpression = this.VisitNumberNumberBinaryScalarOperation(precision, asScalar, BinaryOperator2.Subtract, asScalar2);
				odbcScalarExpression2 = this.VisitSign(odbcScalarExpression);
			}
			return odbcScalarExpression2;
		}

		// Token: 0x06003246 RID: 12870 RVA: 0x0009E434 File Offset: 0x0009C634
		private OdbcScalarExpression VisitSign(OdbcScalarExpression expression)
		{
			OdbcScalarExpression odbcScalarExpression;
			using (this.trace.NewScope("VisitSign"))
			{
				if (!this.trace.DataSourceInfo.Supports(Odbc32.SQL_FUN_NUM.SQL_FN_NUM_SIGN))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				odbcScalarExpression = new OdbcScalarExpression(expression.TypeInfo, OdbcQueryExpressionVisitor.Call(new OdbcScalarFunctionReference(SqlLanguageStrings.SignSqlString), new SqlExpression[] { expression.Expression }));
			}
			return odbcScalarExpression;
		}

		// Token: 0x06003247 RID: 12871 RVA: 0x0009E4C0 File Offset: 0x0009C6C0
		private OdbcConditionExpression VisitValueEquals(InvocationQueryExpression expression)
		{
			return this.VisitValueEqualsShared(expression, false);
		}

		// Token: 0x06003248 RID: 12872 RVA: 0x0009E4CA File Offset: 0x0009C6CA
		private OdbcConditionExpression VisitValueNullableEquals(InvocationQueryExpression expression)
		{
			return this.VisitValueEqualsShared(expression, true);
		}

		// Token: 0x06003249 RID: 12873 RVA: 0x0009E4D4 File Offset: 0x0009C6D4
		private OdbcConditionExpression VisitValueEqualsShared(InvocationQueryExpression expression, bool nullable)
		{
			OdbcConditionExpression odbcConditionExpression;
			using (this.trace.NewScope("VisitValueEqualsShared"))
			{
				Precision precision;
				if (expression.Arguments.Count == 2)
				{
					precision = Precision.Double;
				}
				else
				{
					if (expression.Arguments.Count != 3)
					{
						throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<InvocationQueryExpression>, int, int>>(FoldingWarnings.InvalidArgumentsCount(expression, 2, 3));
					}
					precision = this.GetPrecision(expression, 2, expression.Arguments[2]);
				}
				OdbcScalarExpression asScalar = this.Visit(expression.Arguments[0]).AsScalar;
				OdbcScalarExpression asScalar2 = this.Visit(expression.Arguments[1]).AsScalar;
				odbcConditionExpression = new OdbcConditionExpression(this.VisitEquals(asScalar, asScalar2, precision, nullable));
			}
			return odbcConditionExpression;
		}

		// Token: 0x0600324A RID: 12874 RVA: 0x0009E5A0 File Offset: 0x0009C7A0
		private bool TryGetNumberTypeInfo(TypeValue typeValue, out OdbcTypeInfo typeInfo)
		{
			if (typeValue == TypeValue.Int8)
			{
				return this.TryGetTypeInfo(Odbc32.SQL_TYPE.TINYINT, out typeInfo);
			}
			if (typeValue == TypeValue.Int16)
			{
				return this.TryGetTypeInfo(Odbc32.SQL_TYPE.SMALLINT, out typeInfo);
			}
			if (typeValue == TypeValue.Int32)
			{
				return this.TryGetTypeInfo(Odbc32.SQL_TYPE.INTEGER, out typeInfo);
			}
			if (typeValue == TypeValue.Int64)
			{
				return this.TryGetTypeInfo(Odbc32.SQL_TYPE.BIGINT, out typeInfo);
			}
			if (typeValue == TypeValue.Decimal)
			{
				return this.TryGetTypeInfo(Odbc32.SQL_TYPE.DECIMAL, out typeInfo);
			}
			if (typeValue == TypeValue.Single)
			{
				return this.TryGetTypeInfo(Odbc32.SQL_TYPE.FLOAT, out typeInfo);
			}
			if (typeValue == TypeValue.Double)
			{
				return this.TryGetDoubleType(out typeInfo);
			}
			typeInfo = null;
			return false;
		}

		// Token: 0x0600324B RID: 12875 RVA: 0x0009E629 File Offset: 0x0009C829
		private Func<InvocationQueryExpression, OdbcSqlExpression> NewNumberTypeFromVisitor(bool isInt)
		{
			return (InvocationQueryExpression e) => this.VisitNumberTypeFromFunction(e, isInt);
		}

		// Token: 0x0600324C RID: 12876 RVA: 0x0009E64C File Offset: 0x0009C84C
		private OdbcScalarExpression VisitNumberTypeFromFunction(InvocationQueryExpression expression, bool isInt)
		{
			OdbcScalarExpression odbcScalarExpression2;
			using (this.trace.NewScope("VisitNumberTypeFromFunction"))
			{
				if (!this.trace.ArgumentCountBetween(expression, 1, isInt ? 3 : 2))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				OdbcScalarExpression odbcScalarExpression;
				if (isInt && this.dataSource.Options.TryRecoverDateDiff && this.trace.DataSourceInfo.Supports(Odbc32.SQL_FN_TD.SQL_FN_TD_TIMESTAMPDIFF) && this.TryRecoverDateDiffFromInt(expression.Arguments[0], out odbcScalarExpression))
				{
					odbcScalarExpression2 = odbcScalarExpression;
				}
				else
				{
					OdbcScalarExpression asScalar = this.Visit(expression.Arguments[0]).AsScalar;
					Value value;
					if (expression.Arguments.Count > 1 && (!expression.Arguments[1].TryGetConstant(out value) || !value.IsNull))
					{
						throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<int, FoldingWarnings.StringFormatter<InvocationQueryExpression>, string>>(FoldingWarnings.InvalidArgumentValue(expression, 1, "culture being null"));
					}
					TypeValue nonNullable = ((ConstantQueryExpression)expression.Function).Value.Type.AsFunctionType.ReturnType.NonNullable;
					OdbcScalarExpression odbcScalarExpression3;
					switch (asScalar.TypeInfo.TypeValue.TypeKind)
					{
					case ValueKind.Date:
						odbcScalarExpression3 = this.VisitNumberFromDate(asScalar);
						goto IL_0186;
					case ValueKind.DateTime:
						odbcScalarExpression3 = this.VisitNumberFromDateTime(asScalar);
						goto IL_0186;
					case ValueKind.Duration:
						odbcScalarExpression3 = this.VisitNumberFromDuration(asScalar);
						goto IL_0186;
					case ValueKind.Number:
					case ValueKind.Logical:
						odbcScalarExpression3 = asScalar;
						goto IL_0186;
					case ValueKind.Text:
						odbcScalarExpression3 = this.SoftConvert(asScalar, Precision.Double);
						goto IL_0186;
					}
					throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<int, FoldingWarnings.StringFormatter<InvocationQueryExpression>, string>>(FoldingWarnings.InvalidArgumentValue(expression, 0, "Logical, Number, Text, Duration, Date, DateTime"));
					IL_0186:
					Value value2 = Value.Null;
					if (isInt)
					{
						value2 = Library.RoundingMode.TowardZero;
					}
					if (expression.Arguments.Count == 3 && !this.trace.ArgumentValueWhen(expression.Arguments[2].TryGetConstant(out value2), expression, 2, "being a constant"))
					{
						throw this.trace.NewFoldingFailureException(null);
					}
					OdbcScalarExpression odbcScalarExpression4 = odbcScalarExpression3;
					if (!value2.IsNull)
					{
						OdbcQueryExpressionVisitor.ExpressionVisitor expressionVisitor;
						if (value2.Equals(Library.RoundingMode.TowardZero))
						{
							expressionVisitor = new OdbcQueryExpressionVisitor.ExpressionVisitor(this.TryRound);
						}
						else if (value2.Equals(Library.RoundingMode.Down))
						{
							expressionVisitor = new OdbcQueryExpressionVisitor.ExpressionVisitor(this.TryRoundDown);
						}
						else
						{
							if (!value2.Equals(Library.RoundingMode.Up))
							{
								throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<int, FoldingWarnings.StringFormatter<InvocationQueryExpression>, string>>(FoldingWarnings.InvalidArgumentValue(expression, 2, "being one of the supported rounding modes: TowardZero, Down, Up"));
							}
							expressionVisitor = new OdbcQueryExpressionVisitor.ExpressionVisitor(this.TryRoundUp);
						}
						if (odbcScalarExpression3 == null || !expressionVisitor(expression, odbcScalarExpression3, out odbcScalarExpression4))
						{
							if (!this.softNumbers)
							{
								throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string>>(OdbcFoldingWarnings.OdbcOption("SoftNumbers"));
							}
							odbcScalarExpression4 = odbcScalarExpression3;
						}
					}
					OdbcTypeInfo odbcTypeInfo;
					OdbcScalarExpression odbcScalarExpression5;
					if (!this.TryGetNumberTypeInfo(nonNullable, out odbcTypeInfo) || !this.TryConvert(odbcTypeInfo, odbcScalarExpression4, out odbcScalarExpression5))
					{
						if (!this.softNumbers)
						{
							throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string>>(OdbcFoldingWarnings.OdbcOption("SoftNumbers"));
						}
						odbcScalarExpression5 = this.SoftConvert(odbcScalarExpression4, Precision.Double);
					}
					odbcScalarExpression2 = odbcScalarExpression5;
				}
			}
			return odbcScalarExpression2;
		}

		// Token: 0x0600324D RID: 12877 RVA: 0x0009E960 File Offset: 0x0009CB60
		private OdbcScalarExpression VisitNumberFromDuration(OdbcScalarExpression expression)
		{
			return this.VisitNumberNumberBinaryScalarOperation(expression, BinaryOperator2.Divide, this.VisitConstant(NumberValue.New(864000000000.0)));
		}

		// Token: 0x0600324E RID: 12878 RVA: 0x0009E980 File Offset: 0x0009CB80
		private OdbcScalarExpression VisitNumberFromDate(OdbcScalarExpression expression)
		{
			OdbcScalarExpression asScalar = this.Visit(new ConstantQueryExpression(DateValue.New(OdbcQueryExpressionVisitor.oleDBEpoch))).AsScalar;
			return this.VisitTimestampDiff(expression, asScalar, Odbc32.SQL_TSI.SQL_TSI_DAY);
		}

		// Token: 0x0600324F RID: 12879 RVA: 0x0009E9B4 File Offset: 0x0009CBB4
		private OdbcScalarExpression VisitNumberFromDateTime(OdbcScalarExpression expression)
		{
			OdbcScalarExpression odbcScalarExpression6;
			using (this.trace.NewScope("VisitNumberFromDateTime"))
			{
				if (this.dataSource.Info.FractionalSecondsScale == null)
				{
					throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string>>(FoldingWarnings.SqlCapabilities("FractionalSecondsScale"));
				}
				OdbcScalarExpression asScalar = this.Visit(new ConstantQueryExpression(DateValue.New(OdbcQueryExpressionVisitor.oleDBEpoch))).AsScalar;
				OdbcScalarExpression odbcScalarExpression = this.VisitTimestampDiff(expression, asScalar, Odbc32.SQL_TSI.SQL_TSI_DAY);
				OdbcScalarExpression odbcScalarExpression2 = this.VisitTimestampAdd(Odbc32.SQL_TSI.SQL_TSI_DAY, odbcScalarExpression, asScalar);
				OdbcScalarExpression odbcScalarExpression3 = this.VisitTimestampDiff(expression, odbcScalarExpression2, Odbc32.SQL_TSI.SQL_TSI_FRAC_SECOND);
				OdbcScalarExpression odbcScalarExpression4 = this.VisitConstant(NumberValue.New(86400L * (long)this.dataSource.Info.FractionalSecondsPerSecond.Value));
				OdbcScalarExpression odbcScalarExpression5 = this.VisitNumberNumberBinaryScalarOperation(odbcScalarExpression3, BinaryOperator2.Divide, odbcScalarExpression4);
				CaseFunction caseFunction = this.Case();
				caseFunction.WhenItems.Add(new WhenItem
				{
					When = this.VisitLogicalOperation(expression, BinaryOperator2.LessThan, this.Visit(new ConstantQueryExpression(DateTimeValue.New(OdbcQueryExpressionVisitor.oleDBEpoch))).AsScalar),
					Then = this.Visit(new ConstantQueryExpression(NumberValue.NegativeOne)).AsScalar.Expression
				});
				caseFunction.ElseExpression = this.Visit(OdbcQueryExpressionVisitor.oneExpression).AsScalar.Expression;
				odbcScalarExpression6 = this.VisitNumberNumberBinaryScalarOperation(odbcScalarExpression, BinaryOperator2.Add, this.VisitNumberNumberBinaryScalarOperation(odbcScalarExpression5, BinaryOperator2.Multiply, new OdbcScalarExpression(odbcScalarExpression5.TypeInfo, caseFunction)));
			}
			return odbcScalarExpression6;
		}

		// Token: 0x06003250 RID: 12880 RVA: 0x0009EB44 File Offset: 0x0009CD44
		private OdbcScalarExpression VisitTextFrom(InvocationQueryExpression expression)
		{
			OdbcScalarExpression odbcScalarExpression;
			using (this.trace.NewScope("VisitTextFrom"))
			{
				if (!this.trace.ArgumentCountEquals(expression, 1))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				OdbcScalarExpression asScalar = this.Visit(expression.Arguments[0]).AsScalar;
				if (this.trace.ArgumentTypeKindIs(expression, 0, asScalar, ValueKind.Text))
				{
					odbcScalarExpression = asScalar;
				}
				else if (this.trace.ArgumentTypeKindIs(expression, 0, asScalar, ValueKind.Number))
				{
					OdbcTypeInfo odbcTypeInfo = null;
					if (!this.TryGetTypeInfo(Odbc32.SQL_TYPE.VARCHAR, out odbcTypeInfo) && !this.TryGetTypeInfo(Odbc32.SQL_TYPE.WVARCHAR, out odbcTypeInfo))
					{
						throw this.trace.NewFoldingFailureException(null);
					}
					odbcScalarExpression = this.Convert(odbcTypeInfo, asScalar);
				}
				else if (this.trace.ArgumentTypeKindIs(expression, 0, asScalar, ValueKind.DateTime))
				{
					odbcScalarExpression = new OdbcScalarExpression(this.NewColumnType(OdbcTypeMap.VarChar.SqlType, asScalar.TypeInfo.IsNullable), this.CallConvertOrCast(asScalar.Expression, OdbcTypeMap.VarChar));
				}
				else
				{
					if (!this.trace.ArgumentTypeKindIs(expression, 0, asScalar, ValueKind.Logical))
					{
						throw this.trace.NewFoldingFailureException(null);
					}
					OdbcScalarExpression odbcScalarExpression2 = this.VisitConstant(TextValue.New(LogicalValue.True.ToString()));
					OdbcScalarExpression odbcScalarExpression3 = this.VisitConstant(TextValue.New(LogicalValue.False.ToString()));
					CaseFunction caseFunction = this.Case();
					caseFunction.WhenItems.Add(new WhenItem
					{
						When = this.VisitLogicalOperation(asScalar, BinaryOperator2.Equals, this.VisitConstant(LogicalValue.True)),
						Then = odbcScalarExpression2.Expression
					});
					caseFunction.ElseExpression = odbcScalarExpression3.Expression;
					odbcScalarExpression = new OdbcScalarExpression(odbcScalarExpression2.TypeInfo, caseFunction);
				}
			}
			return odbcScalarExpression;
		}

		// Token: 0x06003251 RID: 12881 RVA: 0x0009ED10 File Offset: 0x0009CF10
		private OdbcScalarExpression VisitLogicalFrom(InvocationQueryExpression expression)
		{
			OdbcScalarExpression odbcScalarExpression;
			using (this.trace.NewScope("VisitLogicalFrom"))
			{
				if (!this.trace.ArgumentCountEquals(expression, 1))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				OdbcScalarExpression asScalar = this.Visit(expression.Arguments[0]).AsScalar;
				if (this.trace.ArgumentTypeKindIs(expression, 0, asScalar, ValueKind.Logical))
				{
					odbcScalarExpression = asScalar;
				}
				else
				{
					Condition condition;
					if (this.trace.ArgumentTypeKindIs(expression, 0, asScalar, ValueKind.Number))
					{
						condition = this.VisitNotEquals(asScalar, this.VisitConstant(NumberValue.Zero), Precision.Double);
					}
					else
					{
						if (!this.trace.ArgumentTypeKindIs(expression, 0, asScalar, ValueKind.Text))
						{
							throw this.trace.NewFoldingFailureException(null);
						}
						condition = this.VisitEquals(asScalar, this.VisitConstant(TextValue.New(LogicalValue.True.ToString())), Precision.Double, false);
					}
					CaseFunction caseFunction = this.Case();
					OdbcScalarExpression odbcScalarExpression2 = this.VisitConstant(LogicalValue.True);
					caseFunction.WhenItems.Add(new WhenItem
					{
						When = condition,
						Then = odbcScalarExpression2.Expression
					});
					caseFunction.ElseExpression = this.VisitConstant(LogicalValue.False).Expression;
					OdbcScalarExpression odbcScalarExpression3 = new OdbcScalarExpression(odbcScalarExpression2.TypeInfo, caseFunction);
					odbcScalarExpression = this.NullLift(odbcScalarExpression3, new OdbcScalarExpression[] { asScalar });
				}
			}
			return odbcScalarExpression;
		}

		// Token: 0x06003252 RID: 12882 RVA: 0x0009EE84 File Offset: 0x0009D084
		private SqlExpression[] VisitPrecisionNumericFunctionArguments(InvocationQueryExpression expression, out OdbcDerivedColumnTypeInfo typeInfo)
		{
			SqlExpression[] array;
			using (this.trace.NewScope("VisitPrecisionNumericFunctionArguments"))
			{
				Precision precision;
				if (expression.Arguments.Count == 2)
				{
					precision = Precision.Double;
				}
				else
				{
					if (expression.Arguments.Count != 3)
					{
						throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<InvocationQueryExpression>, int, int>>(FoldingWarnings.InvalidArgumentsCount(expression, 2, 3));
					}
					precision = this.GetPrecision(expression, 2, expression.Arguments[2]);
				}
				OdbcScalarExpression asScalar = this.Visit(expression.Arguments[0]).AsScalar;
				OdbcScalarExpression asScalar2 = this.Visit(expression.Arguments[1]).AsScalar;
				if (!this.trace.ArgumentTypeKindIs(expression, 0, asScalar, ValueKind.Number) || !this.trace.ArgumentTypeKindIs(expression, 1, asScalar2, ValueKind.Number))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult compatibilityAdjustmentResult = this.AdjustNumberValuesToPreventOverflow(asScalar, asScalar2, precision);
				typeInfo = compatibilityAdjustmentResult.TypeInfo;
				array = new SqlExpression[]
				{
					compatibilityAdjustmentResult.Left.Expression,
					compatibilityAdjustmentResult.Right.Expression
				};
			}
			return array;
		}

		// Token: 0x06003253 RID: 12883 RVA: 0x0009EFA8 File Offset: 0x0009D1A8
		private OdbcScalarExpression VisitNumericFunction(InvocationQueryExpression expression, Odbc32.SQL_FUN_NUM capabilityFlag, ConstantSqlString functionName, bool[] convertToDouble)
		{
			int count = expression.Arguments.Count;
			OdbcScalarExpression[] array = new OdbcScalarExpression[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = this.Visit(expression.Arguments[i]).AsScalar;
			}
			return this.VisitNumericFunction(expression, array, capabilityFlag, functionName, convertToDouble);
		}

		// Token: 0x06003254 RID: 12884 RVA: 0x0009EFFC File Offset: 0x0009D1FC
		private OdbcScalarExpression VisitNumericFunction(InvocationQueryExpression expression, OdbcScalarExpression[] arguments, Odbc32.SQL_FUN_NUM capabilityFlag, ConstantSqlString functionName, bool[] convertToDouble)
		{
			OdbcScalarExpression odbcScalarExpression2;
			using (this.trace.NewScope("VisitNumericFunction"))
			{
				int num = arguments.Length;
				if (!this.trace.ArgumentCountEquals(expression, convertToDouble.Length) || !this.trace.DataSourceInfo.Supports(capabilityFlag))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				OdbcDerivedColumnTypeInfo odbcDerivedColumnTypeInfo = null;
				SqlExpression[] array = new SqlExpression[num];
				for (int i = 0; i < num; i++)
				{
					OdbcScalarExpression odbcScalarExpression = arguments[i];
					if (!this.trace.ArgumentTypeKindIs(expression, i, odbcScalarExpression, ValueKind.Number))
					{
						throw this.trace.NewFoldingFailureException(null);
					}
					if (convertToDouble[i])
					{
						odbcScalarExpression = this.SoftConvert(odbcScalarExpression, Precision.Double);
					}
					array[i] = odbcScalarExpression.Expression;
					if (i == 0)
					{
						odbcDerivedColumnTypeInfo = odbcScalarExpression.TypeInfo;
					}
					else if (odbcScalarExpression.TypeInfo.IsNullable && !odbcDerivedColumnTypeInfo.IsNullable)
					{
						odbcDerivedColumnTypeInfo = odbcDerivedColumnTypeInfo.AsNullable;
					}
				}
				odbcScalarExpression2 = new OdbcScalarExpression(odbcDerivedColumnTypeInfo, OdbcQueryExpressionVisitor.Call(new OdbcScalarFunctionReference(functionName), array));
			}
			return odbcScalarExpression2;
		}

		// Token: 0x06003255 RID: 12885 RVA: 0x0009F110 File Offset: 0x0009D310
		private OdbcSqlExpression VisitDatePartFunction(InvocationQueryExpression expression, Odbc32.SQL_FN_TD capabilityFlag, ConstantSqlString functionName)
		{
			OdbcSqlExpression odbcSqlExpression;
			using (this.trace.NewScope("VisitDatePartFunction"))
			{
				if (!this.trace.ArgumentCountEquals(expression, 1) || !this.trace.DataSourceInfo.Supports(capabilityFlag))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				OdbcScalarExpression asScalar = this.Visit(expression.Arguments[0]).AsScalar;
				if (!this.trace.ArgumentTypeKindIs(expression, 0, asScalar, ValueKind.Date) && !this.trace.ArgumentTypeKindIs(expression, 0, asScalar, ValueKind.DateTime))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				odbcSqlExpression = new OdbcScalarExpression(this.NewColumnType(this.IntegerType.SqlType, asScalar.TypeInfo.IsNullable), OdbcQueryExpressionVisitor.Call(new OdbcScalarFunctionReference(functionName), new SqlExpression[] { asScalar.Expression }));
			}
			return odbcSqlExpression;
		}

		// Token: 0x06003256 RID: 12886 RVA: 0x0009F200 File Offset: 0x0009D400
		private OdbcSqlExpression VisitTimePartFunction(InvocationQueryExpression expression, Odbc32.SQL_FN_TD capabilityFlag, ConstantSqlString functionName)
		{
			OdbcSqlExpression odbcSqlExpression;
			using (this.trace.NewScope("VisitTimePartFunction"))
			{
				if (!this.trace.ArgumentCountEquals(expression, 1) || !this.trace.DataSourceInfo.Supports(capabilityFlag))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				OdbcScalarExpression asScalar = this.Visit(expression.Arguments[0]).AsScalar;
				if (!this.trace.ArgumentTypeKindIs(expression, 0, asScalar, ValueKind.DateTime) && !this.trace.ArgumentTypeKindIs(expression, 0, asScalar, ValueKind.Time))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				odbcSqlExpression = new OdbcScalarExpression(this.NewColumnType(this.IntegerType.SqlType, asScalar.TypeInfo.IsNullable), OdbcQueryExpressionVisitor.Call(new OdbcScalarFunctionReference(functionName), new SqlExpression[] { asScalar.Expression }));
			}
			return odbcSqlExpression;
		}

		// Token: 0x06003257 RID: 12887 RVA: 0x0009F2F0 File Offset: 0x0009D4F0
		private OdbcScalarExpression VisitAggregateInvocation(InvocationQueryExpression expression, Odbc32.SQL_AF capabilityFlag, ConstantSqlString functionName, bool convert, bool isNumeric)
		{
			OdbcScalarExpression odbcScalarExpression;
			using (this.trace.NewScope("VisitAggregateInvocation"))
			{
				if (!this.trace.ArgumentCountBetween(expression, 1, 2) || !this.trace.ArgumentQueryExpressionKindIs(expression, 0, QueryExpressionKind.ColumnAccess) || this.trace.When(!this.allowAggregates, "Aggregates are not allowed.") || !this.trace.DataSourceInfo.Supports(capabilityFlag))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				odbcScalarExpression = this.VisitAggregateInvocation(expression, functionName, convert, isNumeric);
			}
			return odbcScalarExpression;
		}

		// Token: 0x06003258 RID: 12888 RVA: 0x0009F394 File Offset: 0x0009D594
		protected virtual OdbcScalarExpression VisitAggregateInvocation(InvocationQueryExpression expression, ConstantSqlString functionName, bool convert, bool isNumeric)
		{
			OdbcScalarExpression odbcScalarExpression;
			using (this.trace.NewScope("VisitAggregateInvocation"))
			{
				if (!this.trace.ArgumentCountBetween(expression, 1, 2))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				odbcScalarExpression = this.VisitAggregateInvocation(expression, 0, expression.Arguments[0], 1, (expression.Arguments.Count == 2) ? expression.Arguments[1] : null, functionName, convert, isNumeric);
			}
			return odbcScalarExpression;
		}

		// Token: 0x06003259 RID: 12889 RVA: 0x0009F424 File Offset: 0x0009D624
		private OdbcScalarExpression VisitAggregateInvocation(InvocationQueryExpression invocation, int argumentIndex, QueryExpression expressionToAggregate, int argumentPrecisionIndex, QueryExpression precisionExpression, ConstantSqlString functionName, bool convert, bool isNumeric)
		{
			OdbcScalarExpression odbcScalarExpression2;
			using (this.trace.NewScope("VisitAggregateInvocation"))
			{
				OdbcScalarExpression odbcScalarExpression = this.Visit(expressionToAggregate).AsScalar;
				if (isNumeric)
				{
					if (!this.trace.ArgumentTypeKindIs(invocation, argumentIndex, odbcScalarExpression, ValueKind.Number))
					{
						throw this.trace.NewFoldingFailureException(null);
					}
					Precision precision = ((precisionExpression == null) ? (convert ? Precision.Double : null) : this.GetPrecision(invocation, argumentPrecisionIndex, precisionExpression));
					if (precision != null)
					{
						odbcScalarExpression = this.SoftConvert(odbcScalarExpression, precision);
					}
				}
				odbcScalarExpression2 = new OdbcScalarExpression(odbcScalarExpression.TypeInfo, OdbcQueryExpressionVisitor.Call(new BuiltInFunctionReference(functionName), new SqlExpression[] { odbcScalarExpression.Expression }));
			}
			return odbcScalarExpression2;
		}

		// Token: 0x0600325A RID: 12890 RVA: 0x0009F4E0 File Offset: 0x0009D6E0
		private OdbcScalarExpression VisitMax(InvocationQueryExpression invocation, QueryExpression expressionToAggregate)
		{
			OdbcScalarExpression odbcScalarExpression;
			using (this.trace.NewScope("VisitMax"))
			{
				if (!this.trace.DataSourceInfo.Supports(Odbc32.SQL_AF.SQL_AF_MAX))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				odbcScalarExpression = this.VisitAggregateInvocation(invocation, -1, expressionToAggregate, -1, null, SqlLanguageStrings.MaxSqlString, false, false);
			}
			return odbcScalarExpression;
		}

		// Token: 0x0600325B RID: 12891 RVA: 0x0009F550 File Offset: 0x0009D750
		private OdbcScalarExpression SoftConvert(OdbcScalarExpression expression, Precision precision)
		{
			OdbcScalarExpression odbcScalarExpression;
			try
			{
				odbcScalarExpression = this.SoftConvertSeries(expression, precision).First<OdbcQueryExpressionVisitor.SoftConversionResult>().Expression;
			}
			catch (NotSupportedException)
			{
				if (!this.trace.WhenNot<FoldingWarnings.FoldingWarning<string>>(this.softNumbers, OdbcFoldingWarnings.OdbcOption("SoftNumbers")) || expression.TypeValue.TypeKind != ValueKind.Number)
				{
					throw;
				}
				odbcScalarExpression = expression;
			}
			return odbcScalarExpression;
		}

		// Token: 0x0600325C RID: 12892 RVA: 0x0009F5B8 File Offset: 0x0009D7B8
		private IEnumerable<OdbcQueryExpressionVisitor.SoftConversionResult> SoftConvertSeries(OdbcScalarExpression expression, Precision precision)
		{
			IDisposable disposable = this.trace.NewScope("SoftConvertSeries");
			OdbcTypeInfo odbcTypeInfo;
			Odbc32.SQL_TYPE[] fallbacks;
			if (precision.Equals(Precision.Double))
			{
				this.TryGetDoubleType(out odbcTypeInfo);
				fallbacks = OdbcQueryExpressionVisitor.softBase2Types;
			}
			else
			{
				if (!precision.Equals(Precision.Decimal))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				this.TryGetTypeInfo(Odbc32.SQL_TYPE.DECIMAL, out odbcTypeInfo);
				fallbacks = OdbcQueryExpressionVisitor.softBase10Types;
			}
			int rank = 0;
			OdbcScalarExpression odbcScalarExpression;
			if (odbcTypeInfo != null && this.TryConvert(odbcTypeInfo, expression, out odbcScalarExpression))
			{
				yield return new OdbcQueryExpressionVisitor.SoftConversionResult(odbcScalarExpression, rank);
			}
			int num = rank;
			rank = num + 1;
			if (!this.softNumbers)
			{
				throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string>>(OdbcFoldingWarnings.OdbcOption("SoftNumbers"));
			}
			foreach (Odbc32.SQL_TYPE type in fallbacks)
			{
				OdbcTypeInfo odbcTypeInfo2;
				if (this.TryGetTypeInfo(type, out odbcTypeInfo2) && this.TryConvert(odbcTypeInfo2, expression, out odbcScalarExpression))
				{
					yield return new OdbcQueryExpressionVisitor.SoftConversionResult(odbcScalarExpression, rank);
				}
				if (type == expression.TypeInfo.DataSourceType.SqlType)
				{
					break;
				}
				num = rank;
				rank = num + 1;
			}
			Odbc32.SQL_TYPE[] array = null;
			throw this.trace.NewFoldingFailureException(null);
			yield break;
			yield break;
		}

		// Token: 0x0600325D RID: 12893 RVA: 0x0009F5D6 File Offset: 0x0009D7D6
		private static string Escape(string value, string characterToEscape, string replaceCharacter)
		{
			return value.Replace(characterToEscape, replaceCharacter);
		}

		// Token: 0x0600325E RID: 12894 RVA: 0x0009F5E0 File Offset: 0x0009D7E0
		private Precision GetPrecision(InvocationQueryExpression invocation, int argumentIndex, QueryExpression expression)
		{
			Value value;
			if (!this.trace.ArgumentValueWhen(expression.TryGetConstant(out value), invocation, argumentIndex, "being a constant"))
			{
				throw this.trace.NewFoldingFailureException(null);
			}
			Precision value2 = Library.PrecisionEnum.Type.GetValue(value.AsNumber);
			if (value2 != Precision.Double && value2 != Precision.Decimal)
			{
				this.trace.Trace<FoldingWarnings.FoldingWarning<int, FoldingWarnings.StringFormatter<InvocationQueryExpression>, string>>(FoldingWarnings.InvalidArgumentValue(invocation, argumentIndex, "having precision double or decimal"));
			}
			return value2;
		}

		// Token: 0x0600325F RID: 12895 RVA: 0x0009F650 File Offset: 0x0009D850
		private OdbcConditionExpression VisitTextContains(InvocationQueryExpression expression, BinaryOperator2 op, NumberValue expectedLocation, string patternPrefix, string patternSuffix)
		{
			using (this.trace.NewScope("VisitTextContains"))
			{
				if (this.trace.ArgumentCountEquals(expression, 2))
				{
					OdbcScalarExpression asScalar = this.Visit(expression.Arguments[0]).AsScalar;
					OdbcScalarExpression asScalar2 = this.Visit(expression.Arguments[1]).AsScalar;
					OdbcConditionExpression odbcConditionExpression;
					if (this.TryVisitTextContainsLocate(expression, asScalar, asScalar2, op, expectedLocation, out odbcConditionExpression) || this.TryVisitTextContainsLike(expression, asScalar, asScalar2, patternPrefix, patternSuffix, out odbcConditionExpression))
					{
						return odbcConditionExpression;
					}
				}
				throw this.trace.NewFoldingFailureException(null);
			}
			OdbcConditionExpression odbcConditionExpression2;
			return odbcConditionExpression2;
		}

		// Token: 0x06003260 RID: 12896 RVA: 0x0009F6FC File Offset: 0x0009D8FC
		private bool TryVisitTextContainsLocate(InvocationQueryExpression expression, OdbcScalarExpression value, OdbcScalarExpression substring, BinaryOperator2 op, NumberValue expectedLocation, out OdbcConditionExpression result)
		{
			OdbcScalarExpression odbcScalarExpression;
			OdbcScalarExpression odbcScalarExpression2;
			if (this.TryLocate(expression, 1, substring, 0, value, out odbcScalarExpression) && this.TryVisitConstant(expectedLocation, out odbcScalarExpression2))
			{
				result = new OdbcConditionExpression(this.VisitLogicalOperation(odbcScalarExpression, op, odbcScalarExpression2));
				return true;
			}
			result = null;
			return false;
		}

		// Token: 0x06003261 RID: 12897 RVA: 0x0009F740 File Offset: 0x0009D940
		private bool TryVisitTextContainsLike(InvocationQueryExpression expression, OdbcScalarExpression value, OdbcScalarExpression substring, string patternPrefix, string patternSuffix, out OdbcConditionExpression result)
		{
			Value value2;
			if (this.TryGetValue(substring.Expression, out value2) && !value2.IsNull)
			{
				string text = this.LikeEscapeCharacter;
				string text2 = OdbcQueryExpressionVisitor.Escape(value2.AsString, text, text + text);
				text2 = OdbcSearchPattern.EscapeSearchCharacters(text, text2);
				string text3 = patternPrefix + text2 + patternSuffix;
				OdbcScalarExpression odbcScalarExpression = this.Constant(TextValue.New(text3));
				OdbcScalarExpression odbcScalarExpression2 = this.VisitConstant(TextValue.New(text));
				if (this.TryLike(expression, 0, value, odbcScalarExpression, odbcScalarExpression2, out result))
				{
					return true;
				}
			}
			result = null;
			return false;
		}

		// Token: 0x06003262 RID: 12898 RVA: 0x0009F7C8 File Offset: 0x0009D9C8
		private BinaryLogicalOperation BinaryLogicalOperation(SqlExpression left, BinaryLogicalOperator op, SqlExpression right)
		{
			BinaryLogicalOperation binaryLogicalOperation;
			using (this.trace.NewScope("BinaryLogicalOperation"))
			{
				if (!this.trace.DataSourceInfo.Supports(Odbc32.SQL_SP.SQL_SP_COMPARISON))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				binaryLogicalOperation = new BinaryLogicalOperation(left, op, right);
			}
			return binaryLogicalOperation;
		}

		// Token: 0x06003263 RID: 12899 RVA: 0x0009F830 File Offset: 0x0009DA30
		private OdbcScalarExpression Len(OdbcScalarExpression expression)
		{
			OdbcScalarExpression odbcScalarExpression;
			using (this.trace.NewScope("Len"))
			{
				ConstantAnnotationExpression constantAnnotationExpression = expression.Expression as ConstantAnnotationExpression;
				if (constantAnnotationExpression != null && !constantAnnotationExpression.Value.IsNull)
				{
					odbcScalarExpression = this.VisitConstant(NumberValue.New(constantAnnotationExpression.Value.AsString.Length));
				}
				else
				{
					if (!this.trace.DataSourceInfo.Supports(Odbc32.SQL_FN_STR.SQL_FN_STR_CHAR_LENGTH))
					{
						throw this.trace.NewFoldingFailureException(null);
					}
					odbcScalarExpression = new OdbcScalarExpression(this.NewColumnType(this.IntegerType.SqlType, expression.TypeInfo.IsNullable), OdbcQueryExpressionVisitor.Call(new OdbcScalarFunctionReference(SqlLanguageStrings.CharLengthSqlString), new SqlExpression[] { expression.Expression }));
				}
			}
			return odbcScalarExpression;
		}

		// Token: 0x06003264 RID: 12900 RVA: 0x0009F90C File Offset: 0x0009DB0C
		public Condition VisitEquals(OdbcSqlExpression leftExpression, OdbcSqlExpression rightExpression, Precision precision, bool nullable = false)
		{
			OdbcScalarExpression asScalar = leftExpression.AsScalar;
			OdbcScalarExpression asScalar2 = rightExpression.AsScalar;
			OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult comparisonResult = this.AdjustForCompatibility(asScalar, asScalar2, precision);
			Lazy<Condition> lazy = new Lazy<Condition>(() => this.BinaryLogicalOperation(comparisonResult.Left.Expression, BinaryLogicalOperator.Equals, comparisonResult.Right.Expression));
			if (nullable)
			{
				return lazy.Value;
			}
			if (asScalar.TypeInfo.TypeValue.TypeKind == ValueKind.Null)
			{
				return this.IsNull(asScalar2.Expression);
			}
			if (asScalar2.TypeInfo.TypeValue.TypeKind == ValueKind.Null)
			{
				return this.IsNull(asScalar.Expression);
			}
			if (asScalar.TypeInfo.IsNullable && asScalar2.TypeInfo.IsNullable)
			{
				if (comparisonResult.AreCompatible)
				{
					Condition condition = lazy.Value;
					if (!this.useBetterEquality)
					{
						condition = this.Condition(ConditionOperator.And, new Condition[]
						{
							condition,
							this.IsNotNull(asScalar.Expression),
							this.IsNotNull(asScalar2.Expression)
						});
					}
					return this.Condition(ConditionOperator.Or, new Condition[]
					{
						condition,
						this.Condition(ConditionOperator.And, new Condition[]
						{
							this.IsNull(asScalar.Expression),
							this.IsNull(asScalar2.Expression)
						})
					});
				}
				return this.Condition(ConditionOperator.And, new Condition[]
				{
					this.IsNull(asScalar.Expression),
					this.IsNull(asScalar2.Expression)
				});
			}
			else
			{
				if (!comparisonResult.AreCompatible)
				{
					return this.Condition(false);
				}
				if (asScalar.TypeInfo.IsNullable)
				{
					return this.Condition(ConditionOperator.And, new Condition[]
					{
						lazy.Value,
						this.IsNotNull(asScalar.Expression)
					});
				}
				if (asScalar2.TypeInfo.IsNullable)
				{
					return this.Condition(ConditionOperator.And, new Condition[]
					{
						lazy.Value,
						this.IsNotNull(asScalar2.Expression)
					});
				}
				return lazy.Value;
			}
		}

		// Token: 0x06003265 RID: 12901 RVA: 0x0009FB00 File Offset: 0x0009DD00
		private Condition VisitNotEquals(OdbcSqlExpression leftExpression, OdbcSqlExpression rightExpression, Precision precision)
		{
			OdbcScalarExpression asScalar = leftExpression.AsScalar;
			OdbcScalarExpression asScalar2 = rightExpression.AsScalar;
			if (asScalar.TypeInfo.TypeValue.TypeKind == ValueKind.Null)
			{
				return this.IsNotNull(asScalar2.Expression);
			}
			if (asScalar2.TypeInfo.TypeValue.TypeKind == ValueKind.Null)
			{
				return this.IsNotNull(asScalar.Expression);
			}
			OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult compatibilityAdjustmentResult = this.AdjustForCompatibility(asScalar, asScalar2, precision);
			Condition condition = this.BinaryLogicalOperation(compatibilityAdjustmentResult.Left.Expression, BinaryLogicalOperator.NotEqualTo, compatibilityAdjustmentResult.Right.Expression);
			if (asScalar.TypeInfo.IsNullable && asScalar2.TypeInfo.IsNullable)
			{
				if (!compatibilityAdjustmentResult.AreCompatible)
				{
					return this.Condition(ConditionOperator.Or, new Condition[]
					{
						this.Condition(ConditionOperator.And, new Condition[]
						{
							this.IsNotNull(asScalar.Expression),
							this.IsNull(asScalar2.Expression)
						}),
						this.Condition(ConditionOperator.And, new Condition[]
						{
							this.IsNull(asScalar.Expression),
							this.IsNotNull(asScalar2.Expression)
						})
					});
				}
				if (!this.useBetterEquality)
				{
					return this.Condition(ConditionOperator.And, new Condition[]
					{
						condition,
						this.Condition(ConditionOperator.Or, new Condition[]
						{
							this.IsNotNull(asScalar.Expression),
							this.IsNotNull(asScalar2.Expression)
						})
					});
				}
				return this.Condition(ConditionOperator.And, new Condition[]
				{
					this.Condition(ConditionOperator.Or, new Condition[]
					{
						this.Condition(ConditionOperator.Or, new Condition[]
						{
							condition,
							this.IsNull(asScalar.Expression)
						}),
						this.IsNull(asScalar2.Expression)
					}),
					this.Condition(ConditionOperator.Or, new Condition[]
					{
						this.IsNotNull(asScalar.Expression),
						this.IsNotNull(asScalar2.Expression)
					})
				});
			}
			else
			{
				if (!compatibilityAdjustmentResult.AreCompatible)
				{
					return this.Condition(true);
				}
				if (asScalar.TypeInfo.IsNullable)
				{
					return this.Condition(ConditionOperator.Or, new Condition[]
					{
						condition,
						this.IsNull(asScalar.Expression)
					});
				}
				if (asScalar2.TypeInfo.IsNullable)
				{
					return this.Condition(ConditionOperator.Or, new Condition[]
					{
						condition,
						this.IsNull(asScalar2.Expression)
					});
				}
				return condition;
			}
		}

		// Token: 0x06003266 RID: 12902 RVA: 0x0009FD4C File Offset: 0x0009DF4C
		private OdbcScalarExpression VisitColumnAccess(ColumnAccessQueryExpression expression)
		{
			OdbcScalarExpression odbcScalarExpression;
			using (this.trace.NewScope("VisitColumnAccess"))
			{
				OdbcDerivedColumnTypeInfo typeInfo = this.columns[expression.Column].TypeInfo;
				if (typeInfo.DataSourceType.Searchable == Odbc32.SQL_SEARCHABLE.UNSEARCHABLE || typeInfo.DataSourceType.Searchable == Odbc32.SQL_SEARCHABLE.LIKE_ONLY)
				{
					throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, Odbc32.SQL_SEARCHABLE>>(OdbcFoldingWarnings.DataTypeNotSearchable(this.columns[expression.Column]));
				}
				ColumnReference columnReference = this.selectItems[expression.Column].Expression as ColumnReference;
				Alias alias = ((columnReference != null) ? columnReference.Qualifier : null);
				odbcScalarExpression = new OdbcScalarExpression(typeInfo, new ColumnReference(alias, this.selectItems[expression.Column].Name));
			}
			return odbcScalarExpression;
		}

		// Token: 0x06003267 RID: 12903 RVA: 0x0009FE20 File Offset: 0x0009E020
		private OdbcScalarExpression VisitConstant(ConstantQueryExpression expression)
		{
			OdbcScalarExpression odbcScalarExpression;
			using (this.trace.NewScope("VisitConstant"))
			{
				if (!this.trace.DataSourceInfo.Supports(Odbc32.SQL_SC.SQL_SC_SQL92_ENTRY))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				odbcScalarExpression = this.VisitConstant(expression.Value);
			}
			return odbcScalarExpression;
		}

		// Token: 0x06003268 RID: 12904 RVA: 0x0009FE88 File Offset: 0x0009E088
		public OdbcScalarExpression VisitConstant(Value value)
		{
			OdbcScalarExpression odbcScalarExpression;
			using (this.trace.NewScope("VisitConstant"))
			{
				if (!value.IsNull)
				{
					switch (value.Type.TypeKind)
					{
					case ValueKind.Time:
						return this.VisitConstant(value.AsTime);
					case ValueKind.Date:
						return this.VisitConstant(value.AsDate);
					case ValueKind.DateTime:
						return this.VisitConstant(value.AsDateTime);
					case ValueKind.Duration:
						return this.VisitConstant(value.AsDuration);
					case ValueKind.Number:
						return this.VisitConstant(value.AsNumber);
					case ValueKind.Logical:
						return this.VisitConstant(value.AsLogical);
					case ValueKind.Text:
						return this.Constant(value.AsText);
					case ValueKind.Binary:
						return this.VisitConstant(value.AsBinary);
					}
					throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<Value>, string>>(FoldingWarnings.InvalidValue(value, "not supported constant type: Binary, Date, DateTime, Duration, Logical, Number, Text, Time"));
				}
				odbcScalarExpression = OdbcQueryExpressionVisitor.nullTypedExpression;
			}
			return odbcScalarExpression;
		}

		// Token: 0x06003269 RID: 12905 RVA: 0x0009FFA0 File Offset: 0x0009E1A0
		private OdbcScalarExpression VisitIf(IfQueryExpression expression)
		{
			OdbcScalarExpression odbcScalarExpression2;
			using (this.trace.NewScope("VisitIf"))
			{
				OdbcScalarExpression odbcScalarExpression;
				if (this.dataSource.Options.TryRecoverCoalesce && this.trace.DataSourceInfo.Supports(Odbc32.SQL_SVE.SQL_SVE_COALESCE) && this.TryRecoverCoalesce(expression, out odbcScalarExpression))
				{
					odbcScalarExpression2 = odbcScalarExpression;
				}
				else
				{
					OdbcScalarExpression asScalar = this.Visit(expression.TrueCase).AsScalar;
					OdbcScalarExpression asScalar2 = this.Visit(expression.FalseCase).AsScalar;
					OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult compatibilityAdjustmentResult = this.AdjustForCompatibility(asScalar, asScalar2, Precision.Double);
					if (compatibilityAdjustmentResult.TypeInfo == null)
					{
						throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, string>>(OdbcFoldingWarnings.IncompatibleExpressions(this.dataSource.SqlSettings, asScalar, asScalar2, "they failed on converting"));
					}
					OdbcSqlExpression odbcSqlExpression = this.Visit(expression.Condition);
					odbcSqlExpression = this.ConvertToCondition(odbcSqlExpression);
					CaseFunction caseFunction = this.Case();
					caseFunction.WhenItems.Add(new WhenItem
					{
						When = odbcSqlExpression.AsCondition.Expression,
						Then = compatibilityAdjustmentResult.Left.Expression
					});
					caseFunction.ElseExpression = compatibilityAdjustmentResult.Right.Expression;
					odbcScalarExpression2 = new OdbcScalarExpression(compatibilityAdjustmentResult.TypeInfo, caseFunction);
				}
			}
			return odbcScalarExpression2;
		}

		// Token: 0x0600326A RID: 12906 RVA: 0x000A00F8 File Offset: 0x0009E2F8
		private OdbcSqlExpression VisitInvocation(InvocationQueryExpression expression)
		{
			OdbcSqlExpression odbcSqlExpression;
			using (this.trace.NewScope("VisitInvocation"))
			{
				Value value;
				Func<InvocationQueryExpression, OdbcSqlExpression> func;
				if (!expression.Function.TryGetConstant(out value) || !value.IsFunction || !this.FunctionVisitors.TryGetValue(value.AsFunction, out func))
				{
					throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<InvocationQueryExpression>>>(FoldingWarnings.InvalidFunction(expression));
				}
				odbcSqlExpression = func(expression);
			}
			return odbcSqlExpression;
		}

		// Token: 0x0600326B RID: 12907 RVA: 0x000A017C File Offset: 0x0009E37C
		private OdbcSqlExpression VisitInvocation(FunctionValue function, params QueryExpression[] arguments)
		{
			InvocationQueryExpression invocationQueryExpression = new InvocationQueryExpression(new ConstantQueryExpression(function), arguments);
			return this.VisitInvocation(invocationQueryExpression);
		}

		// Token: 0x0600326C RID: 12908 RVA: 0x000A01A0 File Offset: 0x0009E3A0
		private OdbcScalarExpression VisitArgumentAccess(ArgumentAccessQueryExpression expression)
		{
			IDisposable disposable = this.trace.NewScope("VisitArgumentAccess");
			try
			{
				throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, string>>(FoldingWarnings.InvalidType(expression.Kind.ToString(), "aggregation functions"));
			}
			finally
			{
				if (disposable != null)
				{
					disposable.Dispose();
					goto IL_0044;
				}
				goto IL_0044;
				IL_0044:;
			}
		}

		// Token: 0x0600326D RID: 12909 RVA: 0x000A0210 File Offset: 0x0009E410
		private OdbcSqlExpression VisitUnary(UnaryQueryExpression expression)
		{
			OdbcSqlExpression odbcSqlExpression2;
			using (this.trace.NewScope("VisitUnary"))
			{
				OdbcSqlExpression odbcSqlExpression = this.Visit(expression.Expression);
				TypeValue typeValue = OperatorTypeflowModels.Unary(expression.Operator, odbcSqlExpression.TypeValue);
				TypeValue typeValue2;
				if (!this.TryCheckType(typeValue, out typeValue2))
				{
					throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<UnaryOperator2, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, string>>(OdbcFoldingWarnings.InvalidUnaryOperation(this.dataSource.SqlSettings, expression.Operator, odbcSqlExpression, "not a supported type"));
				}
				switch (expression.Operator)
				{
				case UnaryOperator2.Not:
					odbcSqlExpression2 = new OdbcConditionExpression(new ConditionOperation(ConditionOperator.Not, this.ConvertToCondition(odbcSqlExpression).Expression));
					break;
				case UnaryOperator2.Negative:
				{
					OdbcScalarExpression odbcScalarExpression = this.SoftConvert(odbcSqlExpression.AsScalar, Precision.Double);
					odbcSqlExpression2 = new OdbcScalarExpression(odbcScalarExpression.TypeInfo, new UnaryScalarOperation(UnaryScalarOperator.Negative, odbcScalarExpression.Expression));
					break;
				}
				case UnaryOperator2.Positive:
					odbcSqlExpression2 = odbcSqlExpression.AsScalar;
					break;
				default:
					throw this.trace.NewFoldingFailureException(null);
				}
			}
			return odbcSqlExpression2;
		}

		// Token: 0x0600326E RID: 12910 RVA: 0x000A031C File Offset: 0x0009E51C
		private OdbcScalarExpression VisitConstant(BinaryValue value)
		{
			OdbcScalarExpression odbcScalarExpression;
			using (this.trace.NewScope("VisitConstant"))
			{
				byte[] asBytes = value.AsBytes;
				OdbcTypeInfo odbcTypeInfo;
				if (!this.TryGetType(OdbcQueryExpressionVisitor.binaryTypes, asBytes.Length, out odbcTypeInfo, false))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				if (odbcTypeInfo.SqlType == Odbc32.SQL_TYPE.BINARY)
				{
					int num = asBytes.Length;
				}
				else
				{
					int value2 = odbcTypeInfo.ColumnSize.Value;
				}
				SqlExpression sqlExpression = this.VisitConstant(odbcTypeInfo, value);
				sqlExpression = new ConstantAnnotationExpression(value, sqlExpression);
				odbcScalarExpression = new OdbcScalarExpression(new OdbcDerivedColumnTypeInfo(odbcTypeInfo, false, new int?(asBytes.Length), null), sqlExpression);
			}
			return odbcScalarExpression;
		}

		// Token: 0x0600326F RID: 12911 RVA: 0x000A03D4 File Offset: 0x0009E5D4
		private OdbcScalarExpression Constant(TextValue value)
		{
			OdbcScalarExpression odbcScalarExpression;
			using (this.trace.NewScope("Constant"))
			{
				string asString = value.AsString;
				OdbcTypeInfo odbcTypeInfo;
				if ((!ScriptWriter.IsAnsiString(asString) || !this.TryGetType(OdbcQueryExpressionVisitor.charTypes, asString.Length, out odbcTypeInfo, false)) && !this.TryGetType(OdbcQueryExpressionVisitor.wcharTypes, asString.Length, out odbcTypeInfo, false))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				int num;
				if (odbcTypeInfo.SqlType == Odbc32.SQL_TYPE.CHAR || odbcTypeInfo.SqlType == Odbc32.SQL_TYPE.WCHAR)
				{
					num = asString.Length;
				}
				else
				{
					num = odbcTypeInfo.ColumnSize.Value;
				}
				SqlExpression sqlExpression = this.VisitConstant(odbcTypeInfo, value);
				sqlExpression = new ConstantAnnotationExpression(value, sqlExpression);
				odbcScalarExpression = new OdbcScalarExpression(new OdbcDerivedColumnTypeInfo(odbcTypeInfo, false, new int?(num), null), sqlExpression);
			}
			return odbcScalarExpression;
		}

		// Token: 0x06003270 RID: 12912 RVA: 0x000A04B8 File Offset: 0x0009E6B8
		private bool TryGetType(OdbcTypeMap[] candidates, int size, out OdbcTypeInfo typeInfo, bool fallbackToLargestType = false)
		{
			for (int i = 0; i < candidates.Length; i++)
			{
				OdbcTypeMatchCriteria odbcTypeMatchCriteria = new OdbcTypeMatchCriteria(candidates[i].SqlType, size);
				if (this.TryGetMatchingType(odbcTypeMatchCriteria, out typeInfo))
				{
					return true;
				}
			}
			if (fallbackToLargestType)
			{
				OdbcTypeInfo odbcTypeInfo = null;
				for (int i = 0; i < candidates.Length; i++)
				{
					OdbcTypeMatchCriteria odbcTypeMatchCriteria2 = new OdbcTypeMatchCriteria(candidates[i].SqlType, 0);
					if (this.TryGetMatchingType(odbcTypeMatchCriteria2, out typeInfo))
					{
						if (odbcTypeInfo != null)
						{
							int? columnSize = odbcTypeInfo.ColumnSize;
							int? columnSize2 = typeInfo.ColumnSize;
							if (!((columnSize.GetValueOrDefault() < columnSize2.GetValueOrDefault()) & ((columnSize != null) & (columnSize2 != null))))
							{
								goto IL_008B;
							}
						}
						odbcTypeInfo = typeInfo;
					}
					IL_008B:;
				}
				if (odbcTypeInfo != null)
				{
					typeInfo = odbcTypeInfo;
					return true;
				}
			}
			this.trace.Trace<FoldingWarnings.FoldingWarning<int, FoldingWarnings.StringFormatter<OdbcTypeMap[]>>>(OdbcFoldingWarnings.NoMatchedDataType(size, candidates));
			typeInfo = null;
			return false;
		}

		// Token: 0x06003271 RID: 12913 RVA: 0x000A0578 File Offset: 0x0009E778
		private OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult ConvertForSize(OdbcScalarExpression left, OdbcScalarExpression right, int size, bool fallbackToLargestType = false)
		{
			OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult compatibilityAdjustmentResult;
			using (this.trace.NewScope("ConvertForSize"))
			{
				OdbcDerivedColumnTypeInfo odbcDerivedColumnTypeInfo;
				if (this.TryGetImplicitConversion(left, right, size, out odbcDerivedColumnTypeInfo))
				{
					compatibilityAdjustmentResult = new OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult(left, right, odbcDerivedColumnTypeInfo);
				}
				else
				{
					OdbcTypeMap[] array;
					if (this.IsWideCharacterType(left.TypeInfo.DataSourceType.SqlType) || this.IsWideCharacterType(right.TypeInfo.DataSourceType.SqlType))
					{
						array = OdbcQueryExpressionVisitor.wVarCharTypes;
					}
					else
					{
						array = OdbcQueryExpressionVisitor.varCharTypes;
					}
					OdbcTypeInfo odbcTypeInfo;
					if (!this.TryGetType(array, size, out odbcTypeInfo, fallbackToLargestType))
					{
						throw this.trace.NewFoldingFailureException(null);
					}
					OdbcScalarExpression odbcScalarExpression = this.Convert(odbcTypeInfo, left);
					OdbcScalarExpression odbcScalarExpression2 = this.Convert(odbcTypeInfo, right);
					compatibilityAdjustmentResult = new OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult(odbcScalarExpression, odbcScalarExpression2, new OdbcDerivedColumnTypeInfo(odbcTypeInfo, left.TypeInfo.IsNullable || right.TypeInfo.IsNullable, new int?(size), null));
				}
			}
			return compatibilityAdjustmentResult;
		}

		// Token: 0x06003272 RID: 12914 RVA: 0x000A0678 File Offset: 0x0009E878
		private OdbcScalarExpression VisitConstant(DateValue value)
		{
			OdbcDerivedColumnTypeInfo odbcDerivedColumnTypeInfo = this.NewColumnType(Odbc32.SQL_TYPE.TYPE_DATE, false);
			SqlExpression sqlExpression = this.VisitConstant(odbcDerivedColumnTypeInfo.DataSourceType, value);
			sqlExpression = new ConstantAnnotationExpression(value, sqlExpression);
			return new OdbcScalarExpression(odbcDerivedColumnTypeInfo, sqlExpression);
		}

		// Token: 0x06003273 RID: 12915 RVA: 0x000A06AC File Offset: 0x0009E8AC
		private OdbcScalarExpression VisitConstant(DateTimeValue value)
		{
			OdbcDerivedColumnTypeInfo odbcDerivedColumnTypeInfo = this.NewColumnType(Odbc32.SQL_TYPE.TYPE_TIMESTAMP, false);
			SqlExpression sqlExpression = this.VisitConstant(odbcDerivedColumnTypeInfo.DataSourceType, value);
			sqlExpression = new ConstantAnnotationExpression(value, sqlExpression);
			return new OdbcScalarExpression(odbcDerivedColumnTypeInfo, sqlExpression);
		}

		// Token: 0x06003274 RID: 12916 RVA: 0x000A06E0 File Offset: 0x0009E8E0
		private OdbcScalarExpression VisitConstant(TimeValue value)
		{
			OdbcDerivedColumnTypeInfo odbcDerivedColumnTypeInfo = this.NewColumnType(Odbc32.SQL_TYPE.TYPE_TIME, false);
			SqlExpression sqlExpression = this.VisitConstant(odbcDerivedColumnTypeInfo.DataSourceType, value);
			sqlExpression = new ConstantAnnotationExpression(value, sqlExpression);
			return new OdbcScalarExpression(odbcDerivedColumnTypeInfo, sqlExpression);
		}

		// Token: 0x06003275 RID: 12917 RVA: 0x000A0714 File Offset: 0x0009E914
		private OdbcScalarExpression VisitConstant(DurationValue value)
		{
			OdbcDerivedColumnTypeInfo odbcDerivedColumnTypeInfo = this.DurationType;
			SqlExpression sqlExpression = this.VisitConstant(odbcDerivedColumnTypeInfo.DataSourceType, value);
			sqlExpression = new ConstantAnnotationExpression(value, sqlExpression);
			return new OdbcScalarExpression(odbcDerivedColumnTypeInfo, sqlExpression);
		}

		// Token: 0x06003276 RID: 12918 RVA: 0x000A0748 File Offset: 0x0009E948
		private OdbcScalarExpression VisitConstant(LogicalValue value)
		{
			OdbcDerivedColumnTypeInfo odbcDerivedColumnTypeInfo = this.NewColumnType(Odbc32.SQL_TYPE.BIT, false);
			SqlExpression sqlExpression = this.VisitConstant(odbcDerivedColumnTypeInfo.DataSourceType, value);
			sqlExpression = new ConstantAnnotationExpression(value, sqlExpression);
			return new OdbcScalarExpression(odbcDerivedColumnTypeInfo, sqlExpression);
		}

		// Token: 0x06003277 RID: 12919 RVA: 0x000A077C File Offset: 0x0009E97C
		private bool TryVisitConstant(NumberValue value, out OdbcScalarExpression expression)
		{
			return this.TryGetInt32Constant(value, out expression) || this.TryGetInt64Constant(value, out expression) || (value.NumberKind == Microsoft.Mashup.Engine1.Runtime.NumberKind.Decimal && this.TryGetDecimalConstant(value, out expression)) || this.TryConstant(value.AsDouble, out expression);
		}

		// Token: 0x06003278 RID: 12920 RVA: 0x000A07B4 File Offset: 0x0009E9B4
		private OdbcScalarExpression VisitConstant(NumberValue value)
		{
			OdbcScalarExpression odbcScalarExpression2;
			using (this.trace.NewScope("VisitConstant"))
			{
				OdbcScalarExpression odbcScalarExpression;
				if (!this.TryVisitConstant(value, out odbcScalarExpression))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				odbcScalarExpression2 = odbcScalarExpression;
			}
			return odbcScalarExpression2;
		}

		// Token: 0x06003279 RID: 12921 RVA: 0x000A080C File Offset: 0x0009EA0C
		private bool TryGetInt32Constant(NumberValue value, out OdbcScalarExpression expression)
		{
			return this.TryGetNumberConstant<int>(value, Odbc32.SQL_TYPE.INTEGER, new OdbcQueryExpressionVisitor.TryGet<int>(value.TryGetInt32), new Func<OdbcTypeInfo, NumberValue, SqlExpression>(this.VisitConstantInt32), out expression);
		}

		// Token: 0x0600327A RID: 12922 RVA: 0x000A0830 File Offset: 0x0009EA30
		private bool TryGetInt64Constant(NumberValue value, out OdbcScalarExpression expression)
		{
			return this.TryGetNumberConstant<long>(value, Odbc32.SQL_TYPE.BIGINT, new OdbcQueryExpressionVisitor.TryGet<long>(value.TryGetInt64), new Func<OdbcTypeInfo, NumberValue, SqlExpression>(this.VisitConstantInt64), out expression);
		}

		// Token: 0x0600327B RID: 12923 RVA: 0x000A0856 File Offset: 0x0009EA56
		private bool TryGetDecimalConstant(NumberValue value, out OdbcScalarExpression expression)
		{
			return this.TryGetNumberConstant<decimal>(value, Odbc32.SQL_TYPE.DECIMAL, new OdbcQueryExpressionVisitor.TryGet<decimal>(value.TryGetDecimal), new Func<OdbcTypeInfo, NumberValue, SqlExpression>(this.VisitConstantDecimal), out expression);
		}

		// Token: 0x0600327C RID: 12924 RVA: 0x000A087C File Offset: 0x0009EA7C
		private bool TryGetNumberConstant<T>(NumberValue value, Odbc32.SQL_TYPE sqlType, OdbcQueryExpressionVisitor.TryGet<T> tryGet, Func<OdbcTypeInfo, NumberValue, SqlExpression> visitor, out OdbcScalarExpression expression)
		{
			T t;
			OdbcDerivedColumnTypeInfo odbcDerivedColumnTypeInfo;
			if (this.trace.WhenNot<FoldingWarnings.FoldingWarning<NumberValue, string>>(tryGet(out t), FoldingWarnings.NumberTryGet(value, typeof(T))) && this.TryGetColumnType(sqlType, false, out odbcDerivedColumnTypeInfo))
			{
				SqlExpression sqlExpression = visitor(odbcDerivedColumnTypeInfo.DataSourceType, value);
				sqlExpression = new ConstantAnnotationExpression(value, sqlExpression);
				expression = new OdbcScalarExpression(odbcDerivedColumnTypeInfo, sqlExpression);
				return true;
			}
			expression = null;
			return false;
		}

		// Token: 0x0600327D RID: 12925 RVA: 0x000A08E4 File Offset: 0x0009EAE4
		private bool TryGetNumericConstant(NumberValue value, out OdbcScalarExpression expression)
		{
			OdbcDerivedColumnTypeInfo odbcDerivedColumnTypeInfo;
			if (this.TryGetColumnType(Odbc32.SQL_TYPE.NUMERIC, false, out odbcDerivedColumnTypeInfo))
			{
				SqlExpression sqlExpression = this.VisitConstantDecimal(odbcDerivedColumnTypeInfo.DataSourceType, value);
				sqlExpression = new ConstantAnnotationExpression(value, sqlExpression);
				expression = new OdbcScalarExpression(odbcDerivedColumnTypeInfo, sqlExpression);
				return true;
			}
			expression = null;
			return false;
		}

		// Token: 0x0600327E RID: 12926 RVA: 0x000A0924 File Offset: 0x0009EB24
		private OdbcScalarExpression Constant(decimal value)
		{
			OdbcDerivedColumnTypeInfo odbcDerivedColumnTypeInfo = this.NewColumnType(Odbc32.SQL_TYPE.DECIMAL, false);
			NumberValue numberValue = NumberValue.New(value);
			SqlExpression sqlExpression = this.VisitConstantDecimal(odbcDerivedColumnTypeInfo.DataSourceType, numberValue);
			sqlExpression = new ConstantAnnotationExpression(numberValue, sqlExpression);
			return new OdbcScalarExpression(odbcDerivedColumnTypeInfo, sqlExpression);
		}

		// Token: 0x0600327F RID: 12927 RVA: 0x000A0960 File Offset: 0x0009EB60
		private bool TryGetValue(SqlExpression expression, out Value value)
		{
			ConstantAnnotationExpression constantAnnotationExpression = expression as ConstantAnnotationExpression;
			if (constantAnnotationExpression != null)
			{
				value = constantAnnotationExpression.Value;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x06003280 RID: 12928 RVA: 0x000A0988 File Offset: 0x0009EB88
		private OdbcScalarExpression Constant(double value)
		{
			OdbcScalarExpression odbcScalarExpression2;
			using (this.trace.NewScope("Constant"))
			{
				OdbcScalarExpression odbcScalarExpression;
				if (!this.TryConstant(value, out odbcScalarExpression))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				odbcScalarExpression2 = odbcScalarExpression;
			}
			return odbcScalarExpression2;
		}

		// Token: 0x06003281 RID: 12929 RVA: 0x000A09E0 File Offset: 0x0009EBE0
		private bool TryConstant(double value, out OdbcScalarExpression expression)
		{
			OdbcTypeInfo odbcTypeInfo;
			if (this.TryGetDoubleType(out odbcTypeInfo))
			{
				OdbcDerivedColumnTypeInfo odbcDerivedColumnTypeInfo = new OdbcDerivedColumnTypeInfo(odbcTypeInfo, false, odbcTypeInfo.ColumnSize, null);
				NumberValue numberValue = NumberValue.New(value);
				SqlExpression sqlExpression = this.VisitConstantDouble(odbcTypeInfo, numberValue);
				sqlExpression = new ConstantAnnotationExpression(numberValue, sqlExpression);
				expression = new OdbcScalarExpression(odbcDerivedColumnTypeInfo, sqlExpression);
				return true;
			}
			expression = null;
			return false;
		}

		// Token: 0x06003282 RID: 12930 RVA: 0x000A0A38 File Offset: 0x0009EC38
		public OdbcDerivedColumnTypeInfo NewColumnType(Odbc32.SQL_TYPE sqlType, bool isNullable)
		{
			OdbcDerivedColumnTypeInfo odbcDerivedColumnTypeInfo2;
			using (this.trace.NewScope("NewColumnType"))
			{
				OdbcDerivedColumnTypeInfo odbcDerivedColumnTypeInfo;
				if (!this.TryGetColumnType(sqlType, isNullable, out odbcDerivedColumnTypeInfo))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				odbcDerivedColumnTypeInfo2 = odbcDerivedColumnTypeInfo;
			}
			return odbcDerivedColumnTypeInfo2;
		}

		// Token: 0x06003283 RID: 12931 RVA: 0x000A0A90 File Offset: 0x0009EC90
		protected bool TryGetColumnType(Odbc32.SQL_TYPE sqlType, bool isNullable, out OdbcDerivedColumnTypeInfo columnType)
		{
			OdbcTypeInfo odbcTypeInfo;
			if (this.TryGetTypeInfo(sqlType, out odbcTypeInfo))
			{
				columnType = new OdbcDerivedColumnTypeInfo(odbcTypeInfo, isNullable, odbcTypeInfo.ColumnSize, OdbcQueryExpressionVisitor.GetDecimalDigits(sqlType));
				return true;
			}
			columnType = null;
			return false;
		}

		// Token: 0x06003284 RID: 12932 RVA: 0x000A0AC4 File Offset: 0x0009ECC4
		private OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult AdjustForCompatibility(OdbcScalarExpression left, OdbcScalarExpression right, Precision precision)
		{
			OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult compatibilityAdjustmentResult;
			using (this.trace.NewScope("AdjustForCompatibility"))
			{
				if (left.TypeInfo.TypeValue.TypeKind == ValueKind.Any || right.TypeInfo.TypeValue.TypeKind == ValueKind.Any)
				{
					throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, string>>(OdbcFoldingWarnings.IncompatibleExpressions(this.dataSource.SqlSettings, left, right, "they don't have 'Any' data type"));
				}
				OdbcDerivedColumnTypeInfo odbcDerivedColumnTypeInfo;
				if (left.TypeInfo.DataSourceType.SqlType == Odbc32.SQL_TYPE.UNKNOWN)
				{
					compatibilityAdjustmentResult = new OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult(right.TypeInfo.IsNullable, left, right, right.TypeInfo.AsNullable);
				}
				else if (right.TypeInfo.DataSourceType.SqlType == Odbc32.SQL_TYPE.UNKNOWN)
				{
					compatibilityAdjustmentResult = new OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult(left.TypeInfo.IsNullable, left, right, left.TypeInfo.AsNullable);
				}
				else if (left.TypeInfo.TypeValue.TypeKind != right.TypeInfo.TypeValue.TypeKind)
				{
					compatibilityAdjustmentResult = new OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult(false, left, right, null);
				}
				else if (this.TryGetImplicitConversion(left, right, out odbcDerivedColumnTypeInfo))
				{
					compatibilityAdjustmentResult = new OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult(true, left, right, odbcDerivedColumnTypeInfo);
				}
				else
				{
					switch (left.TypeInfo.TypeValue.TypeKind)
					{
					case ValueKind.Number:
						return this.AdjustNumberValuesForCompatibility(left, right, precision);
					case ValueKind.Text:
						return this.AdjustTextValuesForCompatibility(left, right);
					case ValueKind.Binary:
						return this.AdjustBinaryValuesForCompatibility(left, right);
					}
					compatibilityAdjustmentResult = new OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult(left, right, left.TypeInfo);
				}
			}
			return compatibilityAdjustmentResult;
		}

		// Token: 0x06003285 RID: 12933 RVA: 0x000A0C60 File Offset: 0x0009EE60
		private OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult AdjustNumberValuesForCompatibility(OdbcScalarExpression left, OdbcScalarExpression right, Precision precision)
		{
			if (left.TypeInfo.IsComparable(right.TypeInfo))
			{
				return new OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult(left, right, left.TypeInfo);
			}
			OdbcScalarExpression odbcScalarExpression;
			OdbcScalarExpression odbcScalarExpression2;
			if (left.TypeInfo.DataSourceType.SqlType == right.TypeInfo.DataSourceType.SqlType && this.TryConvert(left.TypeInfo.DataSourceType.SqlType, left, out odbcScalarExpression) && this.TryConvert(right.TypeInfo.DataSourceType.SqlType, right, out odbcScalarExpression2))
			{
				return new OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult(odbcScalarExpression, odbcScalarExpression2, left.TypeInfo);
			}
			return this.AdjustNumberValuesToPreventOverflow(left, right, precision);
		}

		// Token: 0x06003286 RID: 12934 RVA: 0x000A0D00 File Offset: 0x0009EF00
		private OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult AdjustNumberValuesForCompatibility(InvocationQueryExpression expression, int leftArgumentIndex, OdbcScalarExpression left, int rightArgumentIndex, OdbcScalarExpression right, Odbc32.SQL_TYPE[] promotions)
		{
			OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult compatibilityAdjustmentResult;
			using (this.trace.NewScope("AdjustNumberValuesForCompatibility"))
			{
				int num = Array.IndexOf<Odbc32.SQL_TYPE>(promotions, left.TypeInfo.DataSourceType.SqlType);
				int num2 = Array.IndexOf<Odbc32.SQL_TYPE>(promotions, right.TypeInfo.DataSourceType.SqlType);
				if (!this.trace.ArgumentValueWhen(num >= 0, expression, leftArgumentIndex, "not being in required type") && !this.trace.ArgumentValueWhen(num2 >= 0, expression, rightArgumentIndex, "not being in required type"))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				if (!left.TypeInfo.IsComparable(right.TypeInfo))
				{
					for (int i = Math.Min(num, num2); i >= 0; i--)
					{
						Odbc32.SQL_TYPE sql_TYPE = promotions[i];
						OdbcScalarExpression odbcScalarExpression;
						OdbcScalarExpression odbcScalarExpression2;
						if (this.TryConvert(sql_TYPE, left, out odbcScalarExpression) && this.TryConvert(sql_TYPE, right, out odbcScalarExpression2))
						{
							return new OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult(odbcScalarExpression, odbcScalarExpression2, odbcScalarExpression.TypeInfo);
						}
					}
					throw this.trace.NewFoldingFailureException(null);
				}
				compatibilityAdjustmentResult = new OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult(left, right, left.TypeInfo);
			}
			return compatibilityAdjustmentResult;
		}

		// Token: 0x06003287 RID: 12935 RVA: 0x000A0E2C File Offset: 0x0009F02C
		private OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult AdjustNumberValuesToPreventOverflow(OdbcScalarExpression left, OdbcScalarExpression right, Precision precision)
		{
			OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult compatibilityAdjustmentResult;
			using (this.trace.NewScope("AdjustNumberValuesToPreventOverflow"))
			{
				try
				{
					using (IEnumerator<OdbcQueryExpressionVisitor.SoftConversionResult> enumerator = this.SoftConvertSeries(left, precision).GetEnumerator())
					{
						using (IEnumerator<OdbcQueryExpressionVisitor.SoftConversionResult> enumerator2 = this.SoftConvertSeries(right, precision).GetEnumerator())
						{
							enumerator.MoveNext();
							enumerator2.MoveNext();
							while (enumerator.Current.Expression.TypeInfo.DataSourceType.SqlType != enumerator2.Current.Expression.TypeInfo.DataSourceType.SqlType)
							{
								if (enumerator.Current.Rank < enumerator2.Current.Rank)
								{
									enumerator.MoveNext();
								}
								else
								{
									enumerator2.MoveNext();
								}
							}
							compatibilityAdjustmentResult = new OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult(enumerator.Current.Expression, enumerator2.Current.Expression, enumerator.Current.Expression.TypeInfo);
						}
					}
				}
				catch (NotSupportedException)
				{
					OdbcDerivedColumnTypeInfo odbcDerivedColumnTypeInfo;
					if (!this.trace.WhenNot<FoldingWarnings.FoldingWarning<string>>(this.softNumbers, OdbcFoldingWarnings.OdbcOption("SoftNumbers")) || !this.TryGetImplicitConversion(left, right, out odbcDerivedColumnTypeInfo))
					{
						throw;
					}
					compatibilityAdjustmentResult = new OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult(left, right, odbcDerivedColumnTypeInfo);
				}
			}
			return compatibilityAdjustmentResult;
		}

		// Token: 0x06003288 RID: 12936 RVA: 0x000A0FC0 File Offset: 0x0009F1C0
		private bool TryGetImplicitConversion(OdbcScalarExpression left, OdbcScalarExpression right, out OdbcDerivedColumnTypeInfo resultType)
		{
			if (left.TypeInfo.IsComparable(right.TypeInfo))
			{
				resultType = left.TypeInfo;
				return true;
			}
			OdbcTypeInfo odbcTypeInfo;
			if (left.TypeInfo.DataSourceType.TryGetImplicitConversion(right.TypeInfo.DataSourceType, out odbcTypeInfo))
			{
				resultType = new OdbcDerivedColumnTypeInfo(odbcTypeInfo, left.TypeInfo.IsNullable || right.TypeInfo.IsNullable, OdbcQueryExpressionVisitor.Max(left.TypeInfo.ColumnSize, right.TypeInfo.ColumnSize), OdbcQueryExpressionVisitor.Max(left.TypeInfo.DecimalDigits, right.TypeInfo.DecimalDigits));
				return true;
			}
			this.trace.Trace<FoldingWarnings.FoldingWarning<string, string, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>>>(OdbcFoldingWarnings.Conversion(this.dataSource.SqlSettings, left, right));
			resultType = null;
			return false;
		}

		// Token: 0x06003289 RID: 12937 RVA: 0x000A1088 File Offset: 0x0009F288
		private bool TryGetImplicitConversion(OdbcScalarExpression left, OdbcScalarExpression right, int size, out OdbcDerivedColumnTypeInfo resultType)
		{
			OdbcTypeInfo odbcTypeInfo;
			if (left.TypeInfo.DataSourceType.TryGetImplicitConversion(right.TypeInfo.DataSourceType, out odbcTypeInfo) && odbcTypeInfo.ColumnSize != null && odbcTypeInfo.ColumnSize.Value >= size)
			{
				resultType = new OdbcDerivedColumnTypeInfo(odbcTypeInfo, left.TypeInfo.IsNullable || right.TypeInfo.IsNullable, new int?(size), null);
				return true;
			}
			resultType = null;
			return false;
		}

		// Token: 0x0600328A RID: 12938 RVA: 0x000A1110 File Offset: 0x0009F310
		private OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult AdjustBinaryValuesForCompatibility(OdbcScalarExpression left, OdbcScalarExpression right)
		{
			OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult compatibilityAdjustmentResult;
			using (this.trace.NewScope("AdjustBinaryValuesForCompatibility"))
			{
				if (left.TypeInfo.ColumnSize == null || right.TypeInfo.ColumnSize == null)
				{
					throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, string>>(OdbcFoldingWarnings.IncompatibleExpressions(this.dataSource.SqlSettings, left, right, "their data types don't have column size"));
				}
				int num = Math.Max(left.TypeInfo.ColumnSize.Value, right.TypeInfo.ColumnSize.Value);
				OdbcTypeInfo odbcTypeInfo;
				if (!this.TryGetType(OdbcQueryExpressionVisitor.varBinaryTypes, num, out odbcTypeInfo, false))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				OdbcScalarExpression odbcScalarExpression = this.Convert(odbcTypeInfo, left);
				OdbcScalarExpression odbcScalarExpression2 = this.Convert(odbcTypeInfo, right);
				compatibilityAdjustmentResult = new OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult(odbcScalarExpression, odbcScalarExpression2, odbcScalarExpression.TypeInfo);
			}
			return compatibilityAdjustmentResult;
		}

		// Token: 0x0600328B RID: 12939 RVA: 0x000A120C File Offset: 0x0009F40C
		protected OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult AdjustTextValuesForCompatibility(OdbcScalarExpression left, OdbcScalarExpression right)
		{
			OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult compatibilityAdjustmentResult;
			using (this.trace.NewScope("AdjustTextValuesForCompatibility"))
			{
				if (left.TypeInfo.ColumnSize == null || right.TypeInfo.ColumnSize == null)
				{
					throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, string>>(OdbcFoldingWarnings.IncompatibleExpressions(this.dataSource.SqlSettings, left, right, "their data types don't have column size"));
				}
				int num = Math.Max(left.TypeInfo.ColumnSize.Value, right.TypeInfo.ColumnSize.Value);
				compatibilityAdjustmentResult = this.ConvertForSize(left, right, num, false);
			}
			return compatibilityAdjustmentResult;
		}

		// Token: 0x0600328C RID: 12940 RVA: 0x000A12CC File Offset: 0x0009F4CC
		private Condition Condition(bool value)
		{
			if (value)
			{
				return this.BinaryLogicalOperation(this.VisitConstant(NumberValue.One).Expression, BinaryLogicalOperator.Equals, this.VisitConstant(NumberValue.One).Expression);
			}
			return this.BinaryLogicalOperation(this.VisitConstant(NumberValue.Zero).Expression, BinaryLogicalOperator.Equals, this.VisitConstant(NumberValue.One).Expression);
		}

		// Token: 0x0600328D RID: 12941 RVA: 0x000A132C File Offset: 0x0009F52C
		private OdbcConditionExpression ConvertToCondition(OdbcSqlExpression expression)
		{
			OdbcConditionExpression odbcConditionExpression;
			using (this.trace.NewScope("ConvertToCondition"))
			{
				if (expression.Kind == OdbcSqlExpressionKind.Condition)
				{
					odbcConditionExpression = expression.AsCondition;
				}
				else
				{
					if (expression.TypeValue.TypeKind != ValueKind.Logical && expression.TypeValue.TypeKind != ValueKind.Null)
					{
						throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, string>>(OdbcFoldingWarnings.InvalidType(this.dataSource.SqlSettings, expression, "Logical or Null"));
					}
					odbcConditionExpression = new OdbcConditionExpression(this.VisitEquals(expression, this.VisitConstant(LogicalValue.True), Precision.Double, false));
				}
			}
			return odbcConditionExpression;
		}

		// Token: 0x0600328E RID: 12942 RVA: 0x000A13D4 File Offset: 0x0009F5D4
		private bool TryGetUnaryLogicalOperation(Odbc32.SQL_SP sp, UnaryLogicalOperator op, SqlExpression expression, out Condition condition, bool negated = false)
		{
			if (this.trace.DataSourceInfo.Supports(sp))
			{
				condition = new UnaryLogicalOperation(op, expression);
				if (negated)
				{
					condition = new ConditionOperation(ConditionOperator.Not, condition);
				}
				return true;
			}
			condition = null;
			return false;
		}

		// Token: 0x0600328F RID: 12943 RVA: 0x000A140C File Offset: 0x0009F60C
		protected virtual Condition IsNull(SqlExpression expression)
		{
			Condition condition2;
			using (this.trace.NewScope("IsNull"))
			{
				Condition condition;
				if (!this.TryGetUnaryLogicalOperation(Odbc32.SQL_SP.SQL_SP_ISNULL, UnaryLogicalOperator.IsNull, expression, out condition, false) && !this.TryGetUnaryLogicalOperation(Odbc32.SQL_SP.SQL_SP_ISNOTNULL, UnaryLogicalOperator.IsNotNull, expression, out condition, true))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				condition2 = condition;
			}
			return condition2;
		}

		// Token: 0x06003290 RID: 12944 RVA: 0x000A1474 File Offset: 0x0009F674
		protected virtual Condition IsNotNull(SqlExpression expression)
		{
			Condition condition2;
			using (this.trace.NewScope("IsNotNull"))
			{
				Condition condition;
				if (!this.TryGetUnaryLogicalOperation(Odbc32.SQL_SP.SQL_SP_ISNOTNULL, UnaryLogicalOperator.IsNotNull, expression, out condition, false) && !this.TryGetUnaryLogicalOperation(Odbc32.SQL_SP.SQL_SP_ISNULL, UnaryLogicalOperator.IsNull, expression, out condition, true))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				condition2 = condition;
			}
			return condition2;
		}

		// Token: 0x06003291 RID: 12945 RVA: 0x000A14DC File Offset: 0x0009F6DC
		private Condition In(SqlExpression item, SqlExpression sequence)
		{
			Condition condition;
			using (this.trace.NewScope("In"))
			{
				if (!this.trace.DataSourceInfo.Supports(Odbc32.SQL_SP.SQL_SP_IN))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				condition = new BinaryLogicalOperation(item, BinaryLogicalOperator.In, sequence);
			}
			return condition;
		}

		// Token: 0x06003292 RID: 12946 RVA: 0x000A1544 File Offset: 0x0009F744
		private Condition Condition(ConditionOperator op, params Condition[] conditions)
		{
			Condition condition = conditions[0];
			for (int i = 1; i < conditions.Length; i++)
			{
				condition = new ConditionOperation(condition, op, conditions[i]);
			}
			return condition;
		}

		// Token: 0x06003293 RID: 12947 RVA: 0x000A1570 File Offset: 0x0009F770
		protected SqlExpression Concat(SqlExpression left, SqlExpression right)
		{
			SqlExpression sqlExpression;
			using (this.trace.NewScope("Concat"))
			{
				if (!this.trace.DataSourceInfo.Supports(Odbc32.SQL_FN_STR.SQL_FN_STR_CONCAT))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				sqlExpression = OdbcQueryExpressionVisitor.Call(new OdbcScalarFunctionReference(SqlLanguageStrings.ConcatSqlString), new SqlExpression[] { left, right });
			}
			return sqlExpression;
		}

		// Token: 0x06003294 RID: 12948 RVA: 0x000A15EC File Offset: 0x0009F7EC
		private SqlExpression Coalesce(SqlExpression left, SqlExpression right)
		{
			if (!this.dataSource.Info.Supports(Odbc32.SQL_SVE.SQL_SVE_COALESCE))
			{
				throw new NotSupportedException();
			}
			return OdbcQueryExpressionVisitor.Call(new OdbcScalarFunctionReference(SqlLanguageStrings.CoalesceSqlString), new SqlExpression[] { left, right });
		}

		// Token: 0x06003295 RID: 12949 RVA: 0x000A1624 File Offset: 0x0009F824
		private CaseFunction Case()
		{
			CaseFunction caseFunction;
			using (this.trace.NewScope("Case"))
			{
				if (!this.trace.DataSourceInfo.Supports(Odbc32.SQL_SC.SQL_SC_SQL92_INTERMEDIATE))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				caseFunction = new CaseFunction();
			}
			return caseFunction;
		}

		// Token: 0x06003296 RID: 12950 RVA: 0x000A1688 File Offset: 0x0009F888
		private BinaryLogicalOperator GetBinaryLogicalOperator(OdbcScalarExpression left, BinaryOperator2 op, OdbcScalarExpression right)
		{
			BinaryLogicalOperator binaryLogicalOperator;
			using (this.trace.NewScope("GetBinaryLogicalOperator"))
			{
				switch (op)
				{
				case BinaryOperator2.GreaterThan:
					binaryLogicalOperator = BinaryLogicalOperator.GreaterThan;
					break;
				case BinaryOperator2.LessThan:
					binaryLogicalOperator = BinaryLogicalOperator.LessThan;
					break;
				case BinaryOperator2.GreaterThanOrEquals:
					binaryLogicalOperator = BinaryLogicalOperator.GreaterThanOrEqual;
					break;
				case BinaryOperator2.LessThanOrEquals:
					binaryLogicalOperator = BinaryLogicalOperator.LessThanOrEqual;
					break;
				case BinaryOperator2.Equals:
					binaryLogicalOperator = BinaryLogicalOperator.Equals;
					break;
				case BinaryOperator2.NotEquals:
					binaryLogicalOperator = BinaryLogicalOperator.NotEqualTo;
					break;
				default:
					throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<BinaryOperator2, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, string>>(OdbcFoldingWarnings.BinaryOperation(this.dataSource.SqlSettings, op, left, right, "GreaterThan, LessThan, GreaterThanOrEquals, LessThanOrEquals, Equals, NotEquals"));
				}
			}
			return binaryLogicalOperator;
		}

		// Token: 0x06003297 RID: 12951 RVA: 0x000A1720 File Offset: 0x0009F920
		private DynamicParameter Parameter(OdbcTypeInfo type, object value)
		{
			DynamicParameter dynamicParameter;
			using (this.trace.NewScope("Parameter"))
			{
				if (!this.SupportsParameters)
				{
					throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string>>(FoldingWarnings.UnsupportedFunction("SQLBINDPARAMETER"));
				}
				dynamicParameter = new DynamicParameter(new OdbcParameter(value, OdbcTypeMap.FromSqlType(type.SqlType)));
			}
			return dynamicParameter;
		}

		// Token: 0x06003298 RID: 12952 RVA: 0x000A1790 File Offset: 0x0009F990
		private bool TryGetDoubleType(out OdbcTypeInfo typeInfo)
		{
			if (this.TryGetTypeInfo(Odbc32.SQL_TYPE.DOUBLE, out typeInfo))
			{
				return true;
			}
			if (this.TryGetTypeInfo(Odbc32.SQL_TYPE.FLOAT, out typeInfo))
			{
				FoldingTracingService foldingTracingService = this.trace;
				OdbcNumberPrecisionRadix? numberPrecisionRadix = typeInfo.NumberPrecisionRadix;
				OdbcNumberPrecisionRadix odbcNumberPrecisionRadix = OdbcNumberPrecisionRadix.Bits;
				bool flag;
				if (((numberPrecisionRadix.GetValueOrDefault() == odbcNumberPrecisionRadix) & (numberPrecisionRadix != null)) && typeInfo.ColumnSize != null)
				{
					int? columnSize = typeInfo.ColumnSize;
					int num = 53;
					flag = (columnSize.GetValueOrDefault() >= num) & (columnSize != null);
				}
				else
				{
					flag = false;
				}
				if (foldingTracingService.WhenNot(flag, "This ODBC driver doesn't support SQLTYPE FLOAT with the same size as DOUBLE. You can override this using SQLGetTypeInfo."))
				{
					return true;
				}
			}
			typeInfo = null;
			return false;
		}

		// Token: 0x06003299 RID: 12953 RVA: 0x000A1820 File Offset: 0x0009FA20
		private OdbcTypeInfo GetTypeInfo(Odbc32.SQL_TYPE sqlType)
		{
			OdbcTypeInfo odbcTypeInfo2;
			using (this.trace.NewScope("GetTypeInfo"))
			{
				OdbcTypeInfo odbcTypeInfo;
				if (!this.TryGetTypeInfo(sqlType, out odbcTypeInfo))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				odbcTypeInfo2 = odbcTypeInfo;
			}
			return odbcTypeInfo2;
		}

		// Token: 0x0600329A RID: 12954 RVA: 0x000A1878 File Offset: 0x0009FA78
		private bool TryGetTypeInfo(Odbc32.SQL_TYPE sqlType, out OdbcTypeInfo typeInfo)
		{
			return this.trace.WhenNot<FoldingWarnings.FoldingWarning<Odbc32.SQL_TYPE>>(this.dataSource.Types.TryGetType(sqlType, out typeInfo), OdbcFoldingWarnings.DataTypeNotFound(sqlType));
		}

		// Token: 0x0600329B RID: 12955 RVA: 0x000A18A0 File Offset: 0x0009FAA0
		private OdbcTypeInfo GetTypeInfo(string nativeTypeName)
		{
			OdbcTypeInfo odbcTypeInfo;
			if (!this.dataSource.Types.TryGetType(nativeTypeName, out odbcTypeInfo))
			{
				throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string>>(FoldingWarnings.InvalidType(nativeTypeName));
			}
			return odbcTypeInfo;
		}

		// Token: 0x0600329C RID: 12956 RVA: 0x000A18D5 File Offset: 0x0009FAD5
		private bool TryCheckType(TypeValue type, out TypeValue result)
		{
			if (type.TypeKind != ValueKind.None)
			{
				result = type;
				return true;
			}
			result = null;
			return false;
		}

		// Token: 0x0600329D RID: 12957 RVA: 0x000A18EC File Offset: 0x0009FAEC
		private static QueryExpression MakeDateDiffPartialYearPattern(FunctionValue function, int multiplier)
		{
			int min = multiplier + 1;
			int max = 10000 * multiplier;
			return new BinaryQueryExpression(BinaryOperator2.Subtract, QueryExpressionMatcher.OneOf(new QueryExpression[]
			{
				QueryExpressionMatcher.Capture(0, delegate(QueryExpression expr)
				{
					int num;
					return expr.TryGetInt32Constant(out num) && num >= min && num < max;
				}),
				new BinaryQueryExpression(BinaryOperator2.Add, new BinaryQueryExpression(BinaryOperator2.Multiply, new InvocationQueryExpression(new ConstantQueryExpression(Library.Date.Year), new QueryExpression[] { QueryExpressionMatcher.Capture(0, null) }), new ConstantQueryExpression(NumberValue.New(multiplier))), new InvocationQueryExpression(new ConstantQueryExpression(function), new QueryExpression[] { QueryExpressionMatcher.Capture(0, null) }))
			}), QueryExpressionMatcher.OneOf(new QueryExpression[]
			{
				QueryExpressionMatcher.Capture(1, delegate(QueryExpression expr)
				{
					int num2;
					return expr.TryGetInt32Constant(out num2) && num2 >= min && num2 < max;
				}),
				new BinaryQueryExpression(BinaryOperator2.Add, new BinaryQueryExpression(BinaryOperator2.Multiply, new InvocationQueryExpression(new ConstantQueryExpression(Library.Date.Year), new QueryExpression[] { QueryExpressionMatcher.Capture(1, null) }), new ConstantQueryExpression(NumberValue.New(multiplier))), new InvocationQueryExpression(new ConstantQueryExpression(function), new QueryExpression[] { QueryExpressionMatcher.Capture(1, null) }))
			}));
		}

		// Token: 0x0600329E RID: 12958 RVA: 0x000A1A04 File Offset: 0x0009FC04
		private bool TryRecoverDateDiffFromBinary(BinaryQueryExpression binary, out OdbcSqlExpression result)
		{
			using (this.trace.NewScope("TryRecoverDateDiffFromBinary"))
			{
				Odbc32.SQL_TSI sql_TSI = Odbc32.SQL_TSI.None;
				if (QueryExpressionMatcher.Matches(OdbcQueryExpressionVisitor.dateDiffYearPattern, binary, this.captures))
				{
					this.captures[0] = OdbcQueryExpressionVisitor.MakeDateTimeFromYear(this.captures[0]);
					this.captures[1] = OdbcQueryExpressionVisitor.MakeDateTimeFromYear(this.captures[1]);
					sql_TSI = Odbc32.SQL_TSI.SQL_TSI_YEAR;
				}
				else if (QueryExpressionMatcher.Matches(OdbcQueryExpressionVisitor.dateDiffQuarterPattern, binary, this.captures))
				{
					this.captures[0] = OdbcQueryExpressionVisitor.MakeDateTimeFromYearAndPartialYear(this.captures[0], 4);
					this.captures[1] = OdbcQueryExpressionVisitor.MakeDateTimeFromYearAndPartialYear(this.captures[1], 4);
					sql_TSI = Odbc32.SQL_TSI.SQL_TSI_QUARTER;
				}
				else if (QueryExpressionMatcher.Matches(OdbcQueryExpressionVisitor.dateDiffMonthPattern, binary, this.captures))
				{
					this.captures[0] = OdbcQueryExpressionVisitor.MakeDateTimeFromYearAndPartialYear(this.captures[0], 12);
					this.captures[1] = OdbcQueryExpressionVisitor.MakeDateTimeFromYearAndPartialYear(this.captures[1], 12);
					sql_TSI = Odbc32.SQL_TSI.SQL_TSI_MONTH;
				}
				if (sql_TSI != Odbc32.SQL_TSI.None && this.dataSource.Info.SupportsTimedateDiffInterval(sql_TSI))
				{
					QueryExpression queryExpression = this.captures[0];
					QueryExpression queryExpression2 = this.captures[1];
					OdbcScalarExpression asScalar = this.Visit(queryExpression).AsScalar;
					OdbcScalarExpression asScalar2 = this.Visit(queryExpression2).AsScalar;
					result = this.VisitTimestampDiff(asScalar, asScalar2, sql_TSI);
					return true;
				}
			}
			result = null;
			return false;
		}

		// Token: 0x0600329F RID: 12959 RVA: 0x000A1B7C File Offset: 0x0009FD7C
		private bool TryRecoverDateDiffFromInt(QueryExpression expression, out OdbcScalarExpression result)
		{
			using (this.trace.NewScope("TryRecoverDateDiffFromInt"))
			{
				Odbc32.SQL_TSI sql_TSI = Odbc32.SQL_TSI.None;
				if (QueryExpressionMatcher.Matches(OdbcQueryExpressionVisitor.dateDiffDayPattern, expression, this.captures))
				{
					sql_TSI = Odbc32.SQL_TSI.SQL_TSI_DAY;
				}
				else if (QueryExpressionMatcher.Matches(OdbcQueryExpressionVisitor.dateDiffHourPattern, expression, this.captures))
				{
					sql_TSI = Odbc32.SQL_TSI.SQL_TSI_HOUR;
				}
				else if (QueryExpressionMatcher.Matches(OdbcQueryExpressionVisitor.dateDiffMinutePattern, expression, this.captures))
				{
					sql_TSI = Odbc32.SQL_TSI.SQL_TSI_MINUTE;
				}
				if (sql_TSI != Odbc32.SQL_TSI.None && this.dataSource.Info.SupportsTimedateDiffInterval(sql_TSI))
				{
					QueryExpression queryExpression = this.captures[0];
					QueryExpression queryExpression2 = this.captures[1];
					OdbcScalarExpression asScalar = this.Visit(queryExpression).AsScalar;
					OdbcScalarExpression asScalar2 = this.Visit(queryExpression2).AsScalar;
					result = this.VisitTimestampDiff(asScalar, asScalar2, sql_TSI);
					return true;
				}
			}
			result = null;
			return false;
		}

		// Token: 0x060032A0 RID: 12960 RVA: 0x000A1C5C File Offset: 0x0009FE5C
		private bool TryRecoverCoalesce(IfQueryExpression expression, out OdbcScalarExpression result)
		{
			if (expression.FalseCase.Kind == QueryExpressionKind.Constant && QueryExpressionMatcher.Matches(OdbcQueryExpressionVisitor.coalescePattern, expression, this.captures))
			{
				QueryExpression queryExpression = this.captures[0];
				QueryExpression queryExpression2 = this.captures[1];
				OdbcScalarExpression asScalar = this.Visit(queryExpression).AsScalar;
				OdbcScalarExpression asScalar2 = this.Visit(queryExpression2).AsScalar;
				OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult compatibilityAdjustmentResult = this.AdjustForCompatibility(asScalar, asScalar2, Precision.Double);
				if (compatibilityAdjustmentResult.TypeInfo != null)
				{
					BuiltInFunctionReference builtInFunctionReference = new BuiltInFunctionReference(SqlLanguageStrings.CoalesceSqlString);
					builtInFunctionReference.Parameters.Add(new FunctionParameterValue
					{
						Value = compatibilityAdjustmentResult.Left.AsScalar.Expression
					});
					builtInFunctionReference.Parameters.Add(new FunctionParameterValue
					{
						Value = compatibilityAdjustmentResult.Right.AsScalar.Expression
					});
					result = new OdbcScalarExpression(compatibilityAdjustmentResult.TypeInfo, builtInFunctionReference);
					return true;
				}
			}
			result = null;
			return false;
		}

		// Token: 0x060032A1 RID: 12961 RVA: 0x000A1D48 File Offset: 0x0009FF48
		private static QueryExpression MakeDateTimeFromYear(QueryExpression expr)
		{
			int num;
			if (expr.TryGetInt32Constant(out num))
			{
				return new ConstantQueryExpression(DateTimeValue.New(new DateTime(num, 1, 1)));
			}
			return expr;
		}

		// Token: 0x060032A2 RID: 12962 RVA: 0x000A1D74 File Offset: 0x0009FF74
		private static QueryExpression MakeDateTimeFromYearAndPartialYear(QueryExpression expr, int multiplier)
		{
			int num;
			if (expr.TryGetInt32Constant(out num))
			{
				return new ConstantQueryExpression(DateTimeValue.New(new DateTime(num / multiplier, num % multiplier, 1)));
			}
			return expr;
		}

		// Token: 0x060032A3 RID: 12963 RVA: 0x000A1DA4 File Offset: 0x0009FFA4
		protected virtual Dictionary<FunctionValue, Func<InvocationQueryExpression, OdbcSqlExpression>> GetFunctionVisitors()
		{
			Dictionary<FunctionValue, Func<InvocationQueryExpression, OdbcSqlExpression>> dictionary = new Dictionary<FunctionValue, Func<InvocationQueryExpression, OdbcSqlExpression>>();
			dictionary.Add(Library.Text.Contains, this.NewTextLocateCompareVisitor(BinaryOperator2.GreaterThanOrEquals, NumberValue.One, "%", "%"));
			dictionary.Add(Library.Text.StartsWith, this.NewTextLocateCompareVisitor(BinaryOperator2.Equals, NumberValue.One, string.Empty, "%"));
			dictionary.Add(Library.Text.EndsWith, new Func<InvocationQueryExpression, OdbcSqlExpression>(this.VisitTextEndsWith));
			dictionary.Add(Library.Text.PositionOf, new Func<InvocationQueryExpression, OdbcSqlExpression>(this.VisitTextPositionOf));
			dictionary.Add(Library.Text.Length, new Func<InvocationQueryExpression, OdbcSqlExpression>(this.VisitTextLength));
			dictionary.Add(Library.Text.Start, this.NewTextExtremityFunctionVisitor(Odbc32.SQL_FN_STR.SQL_FN_STR_LEFT, SqlLanguageStrings.LeftSqlString));
			dictionary.Add(Library.Text.End, this.NewTextExtremityFunctionVisitor(Odbc32.SQL_FN_STR.SQL_FN_STR_RIGHT, SqlLanguageStrings.RightSqlString));
			dictionary.Add(Library.Text.Middle, new Func<InvocationQueryExpression, OdbcSqlExpression>(this.VisitTextMiddle));
			dictionary.Add(Library.Text.Replace, new Func<InvocationQueryExpression, OdbcSqlExpression>(this.VisitTextReplace));
			dictionary.Add(Library.Text.RemoveRange, new Func<InvocationQueryExpression, OdbcSqlExpression>(this.VisitTextRemoveRange));
			dictionary.Add(Library.Text.Repeat, new Func<InvocationQueryExpression, OdbcSqlExpression>(this.VisitTextRepeat));
			dictionary.Add(CultureSpecificFunction.TextLower, this.NewTextCaseTransformVisitor(Odbc32.SQL_FN_STR.SQL_FN_STR_LCASE, SqlLanguageStrings.LCaseSqlString));
			dictionary.Add(CultureSpecificFunction.TextUpper, this.NewTextCaseTransformVisitor(Odbc32.SQL_FN_STR.SQL_FN_STR_UCASE, SqlLanguageStrings.UCaseSqlString));
			dictionary.Add(Library.Text.TrimStart, this.NewTextTrimFunctionVisitor(Odbc32.SQL_FN_STR.SQL_FN_STR_LTRIM, SqlLanguageStrings.LTrimSqlString));
			dictionary.Add(Library.Text.TrimEnd, this.NewTextTrimFunctionVisitor(Odbc32.SQL_FN_STR.SQL_FN_STR_RTRIM, SqlLanguageStrings.RTrimSqlString));
			dictionary.Add(Library.List.First, new Func<InvocationQueryExpression, OdbcSqlExpression>(this.VisitListFirst));
			dictionary.Add(LanguageLibrary.List.Count, new Func<InvocationQueryExpression, OdbcSqlExpression>(this.VisitListOrTableRowCount));
			dictionary.Add(TableModule.Table.RowCount, new Func<InvocationQueryExpression, OdbcSqlExpression>(this.VisitListOrTableRowCount));
			dictionary.Add(Library.List.Max, this.NewAggregateFunctionVisitor(Odbc32.SQL_AF.SQL_AF_MAX, SqlLanguageStrings.MaxSqlString, false, false));
			dictionary.Add(Library.List.Min, this.NewAggregateFunctionVisitor(Odbc32.SQL_AF.SQL_AF_MIN, SqlLanguageStrings.MinSqlString, false, false));
			dictionary.Add(Library.List.Sum, this.NewAggregateFunctionVisitor(Odbc32.SQL_AF.SQL_AF_SUM, SqlLanguageStrings.SumSqlString, true, true));
			dictionary.Add(Library.List.Average, this.NewAggregateFunctionVisitor(Odbc32.SQL_AF.SQL_AF_AVG, SqlLanguageStrings.AvgSqlString, false, true));
			dictionary.Add(Library.List.Contains, new Func<InvocationQueryExpression, OdbcSqlExpression>(this.VisitListContains));
			dictionary.Add(Library.Number.Abs, this.NewNumericFunctionVisitor(Odbc32.SQL_FUN_NUM.SQL_FN_NUM_ABS, SqlLanguageStrings.AbsSqlString, new bool[1]));
			dictionary.Add(Library.Number.Acos, this.NewNumericFunctionVisitor(Odbc32.SQL_FUN_NUM.SQL_FN_NUM_ACOS, SqlLanguageStrings.AcosSqlString, new bool[] { true }));
			dictionary.Add(Library.Number.Asin, this.NewNumericFunctionVisitor(Odbc32.SQL_FUN_NUM.SQL_FN_NUM_ASIN, SqlLanguageStrings.AsinSqlString, new bool[] { true }));
			dictionary.Add(Library.Number.Atan, this.NewNumericFunctionVisitor(Odbc32.SQL_FUN_NUM.SQL_FN_NUM_ATAN, SqlLanguageStrings.AtanSqlString, new bool[] { true }));
			dictionary.Add(Library.Number.Atan2, this.NewNumericFunctionVisitor(Odbc32.SQL_FUN_NUM.SQL_FN_NUM_ATAN2, SqlLanguageStrings.Atan2SqlString, new bool[] { true, true }));
			dictionary.Add(Library.Number.Cos, this.NewNumericFunctionVisitor(Odbc32.SQL_FUN_NUM.SQL_FN_NUM_COS, SqlLanguageStrings.CosSqlString, new bool[] { true }));
			dictionary.Add(Library.Number.Exp, this.NewNumericFunctionVisitor(Odbc32.SQL_FUN_NUM.SQL_FN_NUM_EXP, SqlLanguageStrings.ExpSqlString, new bool[] { true }));
			dictionary.Add(Library.Number.Log, new Func<InvocationQueryExpression, OdbcSqlExpression>(this.VisitNumberLog));
			dictionary.Add(Library.Number.Log10, this.NewNumericFunctionVisitor(Odbc32.SQL_FUN_NUM.SQL_FN_NUM_LOG10, SqlLanguageStrings.Log10SqlString, new bool[] { true }));
			dictionary.Add(Library.Number.Mod, new Func<InvocationQueryExpression, OdbcSqlExpression>(this.VisitMod));
			Dictionary<FunctionValue, Func<InvocationQueryExpression, OdbcSqlExpression>> dictionary2 = dictionary;
			FunctionValue power = Library.Number.Power;
			Odbc32.SQL_FUN_NUM sql_FUN_NUM = Odbc32.SQL_FUN_NUM.SQL_FN_NUM_POWER;
			ConstantSqlString powerSqlString = SqlLanguageStrings.PowerSqlString;
			bool[] array = new bool[2];
			array[0] = true;
			dictionary2.Add(power, this.NewNumericFunctionVisitor(sql_FUN_NUM, powerSqlString, array));
			dictionary.Add(Library.Number.Round, this.NewRoundingFunctionVisitor(new OdbcQueryExpressionVisitor.ExpressionVisitor(this.TryRound)));
			dictionary.Add(Library.Number.RoundUp, this.NewRoundingFunctionVisitor(new OdbcQueryExpressionVisitor.ExpressionVisitor(this.TryRoundUp)));
			dictionary.Add(Library.Number.RoundDown, this.NewRoundingFunctionVisitor(new OdbcQueryExpressionVisitor.ExpressionVisitor(this.TryRoundDown)));
			dictionary.Add(Library.Number.Sign, this.NewNumericFunctionVisitor(Odbc32.SQL_FUN_NUM.SQL_FN_NUM_SIGN, SqlLanguageStrings.SignSqlString, new bool[1]));
			dictionary.Add(Library.Number.Sin, this.NewNumericFunctionVisitor(Odbc32.SQL_FUN_NUM.SQL_FN_NUM_SIN, SqlLanguageStrings.SinSqlString, new bool[] { true }));
			dictionary.Add(Library.Number.Sqrt, this.NewNumericFunctionVisitor(Odbc32.SQL_FUN_NUM.SQL_FN_NUM_SQRT, SqlLanguageStrings.SqrtSqlString, new bool[] { true }));
			dictionary.Add(Library.Number.Tan, this.NewNumericFunctionVisitor(Odbc32.SQL_FUN_NUM.SQL_FN_NUM_TAN, SqlLanguageStrings.TanSqlString, new bool[] { true }));
			dictionary.Add(Library._Value.Add, this.NewPrecisionNumericFunctionVisitor(BinaryScalarOperator.Add));
			dictionary.Add(Library._Value.Subtract, this.NewPrecisionNumericFunctionVisitor(BinaryScalarOperator.Subtract));
			dictionary.Add(Library._Value.Multiply, this.NewPrecisionNumericFunctionVisitor(BinaryScalarOperator.Multiply));
			dictionary.Add(Library._Value.Divide, this.NewPrecisionNumericFunctionVisitor(BinaryScalarOperator.Divide));
			dictionary.Add(Library._Value.Equals, new Func<InvocationQueryExpression, OdbcSqlExpression>(this.VisitValueEquals));
			dictionary.Add(Library._Value.NullableEquals, new Func<InvocationQueryExpression, OdbcSqlExpression>(this.VisitValueNullableEquals));
			dictionary.Add(Library._Value.Compare, new Func<InvocationQueryExpression, OdbcSqlExpression>(this.VisitValueCompare));
			dictionary.Add(Library._Value.As, new Func<InvocationQueryExpression, OdbcSqlExpression>(this.VisitValueAs));
			dictionary.Add(Library._Value.ReplaceType, new Func<InvocationQueryExpression, OdbcSqlExpression>(this.VisitValueReplaceType));
			dictionary.Add(CultureSpecificFunction.DateFrom, new Func<InvocationQueryExpression, OdbcSqlExpression>(this.VisitDateFrom));
			dictionary.Add(CultureSpecificFunction.DateTimeFrom, new Func<InvocationQueryExpression, OdbcSqlExpression>(this.VisitDateTimeFrom));
			dictionary.Add(CultureSpecificFunction.ByteFrom, this.NewNumberTypeFromVisitor(true));
			dictionary.Add(CultureSpecificFunction.Int8From, this.NewNumberTypeFromVisitor(true));
			dictionary.Add(CultureSpecificFunction.Int16From, this.NewNumberTypeFromVisitor(true));
			dictionary.Add(CultureSpecificFunction.Int32From, this.NewNumberTypeFromVisitor(true));
			dictionary.Add(CultureSpecificFunction.Int64From, this.NewNumberTypeFromVisitor(true));
			dictionary.Add(CultureSpecificFunction.SingleFrom, this.NewNumberTypeFromVisitor(false));
			dictionary.Add(CultureSpecificFunction.DoubleFrom, this.NewNumberTypeFromVisitor(false));
			dictionary.Add(CultureSpecificFunction.DecimalFrom, this.NewNumberTypeFromVisitor(false));
			dictionary.Add(CultureSpecificFunction.TextFrom, new Func<InvocationQueryExpression, OdbcSqlExpression>(this.VisitTextFrom));
			dictionary.Add(Library.Logical.From, new Func<InvocationQueryExpression, OdbcSqlExpression>(this.VisitLogicalFrom));
			dictionary.Add(Library.Date.Year, this.NewDatePartFunctionVisitor(Odbc32.SQL_FN_TD.SQL_FN_TD_YEAR, SqlLanguageStrings.YearSqlString));
			dictionary.Add(Library.Date.Month, this.NewDatePartFunctionVisitor(Odbc32.SQL_FN_TD.SQL_FN_TD_MONTH, SqlLanguageStrings.MonthSqlString));
			dictionary.Add(Library.Date.QuarterOfYear, this.NewDatePartFunctionVisitor(Odbc32.SQL_FN_TD.SQL_FN_TD_QUARTER, SqlLanguageStrings.QuarterSqlString));
			dictionary.Add(CultureSpecificFunction.DateWeekOfYear, this.NewDatePartFunctionVisitor(Odbc32.SQL_FN_TD.SQL_FN_TD_WEEK, SqlLanguageStrings.WeekSqlString));
			dictionary.Add(Library.Date.DayOfYear, this.NewDatePartFunctionVisitor(Odbc32.SQL_FN_TD.SQL_FN_TD_DAYOFYEAR, SqlLanguageStrings.DayOfYearSqlString));
			dictionary.Add(Library.Date.Day, this.NewDatePartFunctionVisitor(Odbc32.SQL_FN_TD.SQL_FN_TD_DAYOFMONTH, SqlLanguageStrings.DayOfMonthSqlString));
			dictionary.Add(CultureSpecificFunction.DateDayOfWeek, this.NewDatePartFunctionVisitor(Odbc32.SQL_FN_TD.SQL_FN_TD_DAYOFWEEK, SqlLanguageStrings.DayOfWeekSqlString));
			dictionary.Add(Library.Date.AddDays, this.NewDateAdditionFunctionVisitor(Odbc32.SQL_TSI.SQL_TSI_DAY));
			dictionary.Add(Library.Date.AddWeeks, this.NewDateAdditionFunctionVisitor(Odbc32.SQL_TSI.SQL_TSI_WEEK));
			dictionary.Add(Library.Date.AddMonths, this.NewDateAdditionFunctionVisitor(Odbc32.SQL_TSI.SQL_TSI_MONTH));
			dictionary.Add(Library.Date.AddQuarters, this.NewDateAdditionFunctionVisitor(Odbc32.SQL_TSI.SQL_TSI_QUARTER));
			dictionary.Add(Library.Date.AddYears, this.NewDateAdditionFunctionVisitor(Odbc32.SQL_TSI.SQL_TSI_YEAR));
			dictionary.Add(Library.Date.StartOfDay, this.NewDateStartOfFunctionVisitor(Odbc32.SQL_TSI.SQL_TSI_DAY, false));
			dictionary.Add(CultureSpecificFunction.DateStartOfWeek, this.NewDateStartOfFunctionVisitor(Odbc32.SQL_TSI.SQL_TSI_WEEK, false));
			dictionary.Add(Library.Date.StartOfMonth, this.NewDateStartOfFunctionVisitor(Odbc32.SQL_TSI.SQL_TSI_MONTH, false));
			dictionary.Add(Library.Date.StartOfQuarter, this.NewDateStartOfFunctionVisitor(Odbc32.SQL_TSI.SQL_TSI_QUARTER, false));
			dictionary.Add(Library.Date.StartOfYear, this.NewDateStartOfFunctionVisitor(Odbc32.SQL_TSI.SQL_TSI_YEAR, false));
			dictionary.Add(Library.Date.EndOfDay, this.NewDateEndOfFunctionVisitor(Odbc32.SQL_TSI.SQL_TSI_DAY, false));
			dictionary.Add(CultureSpecificFunction.DateEndOfWeek, this.NewDateEndOfFunctionVisitor(Odbc32.SQL_TSI.SQL_TSI_WEEK, false));
			dictionary.Add(Library.Date.EndOfMonth, this.NewDateEndOfFunctionVisitor(Odbc32.SQL_TSI.SQL_TSI_MONTH, false));
			dictionary.Add(Library.Date.EndOfQuarter, this.NewDateEndOfFunctionVisitor(Odbc32.SQL_TSI.SQL_TSI_QUARTER, false));
			dictionary.Add(Library.Date.EndOfYear, this.NewDateEndOfFunctionVisitor(Odbc32.SQL_TSI.SQL_TSI_YEAR, false));
			dictionary.Add(Library.Time.Hour, this.NewTimePartFunctionVisitor(Odbc32.SQL_FN_TD.SQL_FN_TD_HOUR, SqlLanguageStrings.HourSqlString));
			dictionary.Add(Library.Time.Minute, this.NewTimePartFunctionVisitor(Odbc32.SQL_FN_TD.SQL_FN_TD_MINUTE, SqlLanguageStrings.MinuteSqlString));
			dictionary.Add(Library.Time.Second, new Func<InvocationQueryExpression, OdbcSqlExpression>(this.VisitTimeSecond));
			dictionary.Add(Library.Time.StartOfHour, this.NewDateStartOfFunctionVisitor(Odbc32.SQL_TSI.SQL_TSI_HOUR, true));
			dictionary.Add(Library.Time.EndOfHour, this.NewDateEndOfFunctionVisitor(Odbc32.SQL_TSI.SQL_TSI_HOUR, true));
			dictionary.Add(Library.Duration.duration, new Func<InvocationQueryExpression, OdbcSqlExpression>(this.VisitDurationConstructor));
			dictionary.Add(Library.Duration.Days, this.NewDurationPartFunctionVisitor(864000000000L, -1, true));
			dictionary.Add(Library.Duration.Hours, this.NewDurationPartFunctionVisitor(36000000000L, 24, true));
			dictionary.Add(Library.Duration.Minutes, this.NewDurationPartFunctionVisitor(600000000L, 60, true));
			dictionary.Add(Library.Duration.Seconds, this.NewDurationPartFunctionVisitor(10000000L, 60, false));
			dictionary.Add(Library.Duration.TotalDays, this.NewDurationPartFunctionVisitor(864000000000L, -1, false));
			dictionary.Add(Library.Duration.TotalHours, this.NewDurationPartFunctionVisitor(36000000000L, -1, false));
			dictionary.Add(Library.Duration.TotalMinutes, this.NewDurationPartFunctionVisitor(600000000L, -1, false));
			dictionary.Add(Library.Duration.TotalSeconds, this.NewDurationPartFunctionVisitor(10000000L, -1, false));
			dictionary.Add(Library.Duration.From, new Func<InvocationQueryExpression, OdbcSqlExpression>(this.VisitDurationFrom));
			dictionary.Add(Library.Character.FromNumber, new Func<InvocationQueryExpression, OdbcSqlExpression>(this.VisitCharacterFromNumber));
			dictionary.Add(Library.Character.ToNumber, new Func<InvocationQueryExpression, OdbcSqlExpression>(this.VisitCharacterToNumber));
			return dictionary;
		}

		// Token: 0x060032A4 RID: 12964 RVA: 0x000A2774 File Offset: 0x000A0974
		private void EnsureConformance(Odbc32.SQL_SC sqlLevel)
		{
			using (this.trace.NewScope("EnsureConformance"))
			{
				if (!this.trace.DataSourceInfo.Supports(sqlLevel))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x060032A5 RID: 12965 RVA: 0x000A27D0 File Offset: 0x000A09D0
		protected static BuiltInFunctionReference Call(BuiltInFunctionReference function, params SqlExpression[] args)
		{
			IList<FunctionParameterValue> parameters = function.Parameters;
			for (int i = 0; i < args.Length; i++)
			{
				FunctionParameterValue functionParameterValue = args[i] as FunctionParameterValue;
				ICollection<FunctionParameterValue> collection = parameters;
				FunctionParameterValue functionParameterValue2;
				if ((functionParameterValue2 = functionParameterValue) == null)
				{
					(functionParameterValue2 = new FunctionParameterValue()).Value = args[i];
				}
				collection.Add(functionParameterValue2);
			}
			return function;
		}

		// Token: 0x060032A6 RID: 12966 RVA: 0x000A2818 File Offset: 0x000A0A18
		private static int? Max(int? a, int? b)
		{
			if (a != null && b != null)
			{
				return new int?(Math.Max(a.Value, b.Value));
			}
			return null;
		}

		// Token: 0x060032A7 RID: 12967 RVA: 0x000A285C File Offset: 0x000A0A5C
		private static int? GetDecimalDigits(Odbc32.SQL_TYPE sqlType)
		{
			if (sqlType == Odbc32.SQL_TYPE.BIT || sqlType == Odbc32.SQL_TYPE.BIGINT || sqlType - Odbc32.SQL_TYPE.INTEGER <= 1)
			{
				return new int?(0);
			}
			return null;
		}

		// Token: 0x060032A8 RID: 12968 RVA: 0x000A2889 File Offset: 0x000A0A89
		private static SqlConstant GetTSIConstant(Odbc32.SQL_TSI tsi)
		{
			return new SqlConstant(ConstantType.Enum, Enum.GetName(typeof(Odbc32.SQL_TSI), tsi));
		}

		// Token: 0x04001628 RID: 5672
		private static readonly ExpressionPattern valueIsNotNullPattern = new ExpressionPattern(new string[] { "(__value) => __value <> null", "(__value) => null <> __value" });

		// Token: 0x04001629 RID: 5673
		private static readonly string[] likeEscapeCharacterCandidates = new string[]
		{
			"!", "#", "$", "*", "+", ".", "/", ":", ";", "<",
			"=", ">", "?", "@", "`", "~"
		};

		// Token: 0x0400162A RID: 5674
		private static readonly OdbcTypeMap[] binaryTypes = new OdbcTypeMap[]
		{
			OdbcTypeMap.Binary,
			OdbcTypeMap.VarBinary,
			OdbcTypeMap.LongVarBinary
		};

		// Token: 0x0400162B RID: 5675
		private static readonly OdbcTypeMap[] varBinaryTypes = new OdbcTypeMap[]
		{
			OdbcTypeMap.VarBinary,
			OdbcTypeMap.LongVarBinary
		};

		// Token: 0x0400162C RID: 5676
		private static readonly OdbcTypeMap[] charTypes = new OdbcTypeMap[]
		{
			OdbcTypeMap.Char,
			OdbcTypeMap.VarChar,
			OdbcTypeMap.LongVarChar
		};

		// Token: 0x0400162D RID: 5677
		private static readonly OdbcTypeMap[] wcharTypes = new OdbcTypeMap[]
		{
			OdbcTypeMap.WChar,
			OdbcTypeMap.WVarchar,
			OdbcTypeMap.WLongVarChar
		};

		// Token: 0x0400162E RID: 5678
		private static readonly OdbcTypeMap[] varCharTypes = new OdbcTypeMap[]
		{
			OdbcTypeMap.VarChar,
			OdbcTypeMap.LongVarChar
		};

		// Token: 0x0400162F RID: 5679
		private static readonly OdbcTypeMap[] wVarCharTypes = new OdbcTypeMap[]
		{
			OdbcTypeMap.WVarchar,
			OdbcTypeMap.WLongVarChar
		};

		// Token: 0x04001630 RID: 5680
		private static readonly OdbcScalarExpression nullTypedExpression = new OdbcScalarExpression(new OdbcDerivedColumnTypeInfo(OdbcTypeInfo.Null, true, null, null), SqlConstant.Null);

		// Token: 0x04001631 RID: 5681
		private static readonly DateTime startOfYearDateTime = new DateTime(2000, 1, 1, 0, 0, 0);

		// Token: 0x04001632 RID: 5682
		private static readonly DateTime oleDBEpoch = new DateTime(1899, 12, 30, 0, 0, 0);

		// Token: 0x04001633 RID: 5683
		private const long secondsPerDay = 86400L;

		// Token: 0x04001634 RID: 5684
		private const long millisecondsPerSecond = 1000L;

		// Token: 0x04001635 RID: 5685
		private const long ticksPerDay = 864000000000L;

		// Token: 0x04001636 RID: 5686
		private const long ticksPerHour = 36000000000L;

		// Token: 0x04001637 RID: 5687
		private const long ticksPerMinute = 600000000L;

		// Token: 0x04001638 RID: 5688
		private const long ticksPerSecond = 10000000L;

		// Token: 0x04001639 RID: 5689
		protected readonly OdbcFoldingTracingService trace;

		// Token: 0x0400163A RID: 5690
		private bool? supportsParameters;

		// Token: 0x0400163B RID: 5691
		private Dictionary<FunctionValue, Func<InvocationQueryExpression, OdbcSqlExpression>> functionVisitors;

		// Token: 0x0400163C RID: 5692
		private readonly OdbcDataSource dataSource;

		// Token: 0x0400163D RID: 5693
		private readonly OdbcQueryColumnInfo[] columns;

		// Token: 0x0400163E RID: 5694
		private readonly IList<SelectItem> selectItems;

		// Token: 0x0400163F RID: 5695
		private readonly bool allowAggregates;

		// Token: 0x04001640 RID: 5696
		private readonly bool softNumbers;

		// Token: 0x04001641 RID: 5697
		private readonly bool tolerateConcatOverflow;

		// Token: 0x04001642 RID: 5698
		private readonly int[] groupKey;

		// Token: 0x04001643 RID: 5699
		private readonly QueryExpression[] captures;

		// Token: 0x04001644 RID: 5700
		private readonly bool useBetterEquality;

		// Token: 0x04001645 RID: 5701
		private static readonly Odbc32.SQL_TYPE[] softBase2Types = new Odbc32.SQL_TYPE[]
		{
			Odbc32.SQL_TYPE.DOUBLE,
			Odbc32.SQL_TYPE.FLOAT,
			Odbc32.SQL_TYPE.REAL,
			Odbc32.SQL_TYPE.DECIMAL,
			Odbc32.SQL_TYPE.NUMERIC,
			Odbc32.SQL_TYPE.BIGINT,
			Odbc32.SQL_TYPE.INTEGER
		};

		// Token: 0x04001646 RID: 5702
		private static readonly Odbc32.SQL_TYPE[] softBase10Types = new Odbc32.SQL_TYPE[]
		{
			Odbc32.SQL_TYPE.DECIMAL,
			Odbc32.SQL_TYPE.NUMERIC,
			Odbc32.SQL_TYPE.DOUBLE,
			Odbc32.SQL_TYPE.FLOAT,
			Odbc32.SQL_TYPE.REAL,
			Odbc32.SQL_TYPE.DECIMAL,
			Odbc32.SQL_TYPE.BIGINT,
			Odbc32.SQL_TYPE.INTEGER
		};

		// Token: 0x04001647 RID: 5703
		private static readonly Odbc32.SQL_TYPE[] integerTypes = new Odbc32.SQL_TYPE[]
		{
			Odbc32.SQL_TYPE.BIGINT,
			Odbc32.SQL_TYPE.INTEGER,
			Odbc32.SQL_TYPE.SMALLINT,
			Odbc32.SQL_TYPE.TINYINT
		};

		// Token: 0x04001648 RID: 5704
		private static readonly ConstantQueryExpression zeroExpression = new ConstantQueryExpression(NumberValue.Zero);

		// Token: 0x04001649 RID: 5705
		private static readonly ConstantQueryExpression oneExpression = new ConstantQueryExpression(NumberValue.One);

		// Token: 0x0400164A RID: 5706
		private static readonly QueryExpression dateDiffYearPattern = new BinaryQueryExpression(BinaryOperator2.Subtract, QueryExpressionMatcher.OneOf(new QueryExpression[]
		{
			QueryExpressionMatcher.Capture(0, delegate(QueryExpression expr)
			{
				int num;
				return expr.TryGetInt32Constant(out num) && num >= 1 && num < 10000;
			}),
			new InvocationQueryExpression(new ConstantQueryExpression(Library.Date.Year), new QueryExpression[] { QueryExpressionMatcher.Capture(0, null) })
		}), QueryExpressionMatcher.OneOf(new QueryExpression[]
		{
			QueryExpressionMatcher.Capture(1, delegate(QueryExpression expr)
			{
				int num2;
				return expr.TryGetInt32Constant(out num2) && num2 >= 1 && num2 < 10000;
			}),
			new InvocationQueryExpression(new ConstantQueryExpression(Library.Date.Year), new QueryExpression[] { QueryExpressionMatcher.Capture(1, null) })
		}));

		// Token: 0x0400164B RID: 5707
		private static readonly QueryExpression dateDiffQuarterPattern = OdbcQueryExpressionVisitor.MakeDateDiffPartialYearPattern(Library.Date.QuarterOfYear, 4);

		// Token: 0x0400164C RID: 5708
		private static readonly QueryExpression dateDiffMonthPattern = OdbcQueryExpressionVisitor.MakeDateDiffPartialYearPattern(Library.Date.Month, 12);

		// Token: 0x0400164D RID: 5709
		private static readonly QueryExpression dateDiffDayPattern = new InvocationQueryExpression(new ConstantQueryExpression(Library.Duration.TotalDays), new QueryExpression[]
		{
			new BinaryQueryExpression(BinaryOperator2.Subtract, QueryExpressionMatcher.OneOf(new QueryExpression[]
			{
				QueryExpressionMatcher.CaptureConstant(0, ValueKind.DateTime, (Value dt) => dt.AsDateTime.AsClrDateTime.TimeOfDay.Ticks == 0L),
				new InvocationQueryExpression(new ConstantQueryExpression(Library.Date.StartOfDay), new QueryExpression[] { QueryExpressionMatcher.Capture(0, null) })
			}), QueryExpressionMatcher.OneOf(new QueryExpression[]
			{
				QueryExpressionMatcher.CaptureConstant(1, ValueKind.DateTime, (Value dt) => dt.AsDateTime.AsClrDateTime.TimeOfDay.Ticks == 0L),
				new InvocationQueryExpression(new ConstantQueryExpression(Library.Date.StartOfDay), new QueryExpression[] { QueryExpressionMatcher.Capture(1, null) })
			}))
		});

		// Token: 0x0400164E RID: 5710
		private static readonly QueryExpression dateDiffHourPattern = new InvocationQueryExpression(new ConstantQueryExpression(Library.Duration.TotalHours), new QueryExpression[]
		{
			new BinaryQueryExpression(BinaryOperator2.Subtract, QueryExpressionMatcher.OneOf(new QueryExpression[]
			{
				QueryExpressionMatcher.CaptureConstant(0, ValueKind.DateTime, (Value dt) => dt.AsDateTime.AsClrDateTime.TimeOfDay.Ticks % 36000000000L == 0L),
				new InvocationQueryExpression(new ConstantQueryExpression(Library.Time.StartOfHour), new QueryExpression[] { QueryExpressionMatcher.Capture(0, null) })
			}), QueryExpressionMatcher.OneOf(new QueryExpression[]
			{
				QueryExpressionMatcher.CaptureConstant(1, ValueKind.DateTime, (Value dt) => dt.AsDateTime.AsClrDateTime.TimeOfDay.Ticks % 36000000000L == 0L),
				new InvocationQueryExpression(new ConstantQueryExpression(Library.Time.StartOfHour), new QueryExpression[] { QueryExpressionMatcher.Capture(1, null) })
			}))
		});

		// Token: 0x0400164F RID: 5711
		private static readonly QueryExpression dateDiffMinutePattern = new InvocationQueryExpression(new ConstantQueryExpression(Library.Duration.TotalMinutes), new QueryExpression[]
		{
			new BinaryQueryExpression(BinaryOperator2.Subtract, QueryExpressionMatcher.OneOf(new QueryExpression[]
			{
				QueryExpressionMatcher.CaptureConstant(0, ValueKind.DateTime, (Value dt) => dt.AsDateTime.AsClrDateTime.Ticks % 600000000L == 0L),
				new BinaryQueryExpression(BinaryOperator2.Subtract, QueryExpressionMatcher.Capture(0, null), new InvocationQueryExpression(new ConstantQueryExpression(Library.Duration.duration), new QueryExpression[]
				{
					OdbcQueryExpressionVisitor.zeroExpression,
					OdbcQueryExpressionVisitor.zeroExpression,
					OdbcQueryExpressionVisitor.zeroExpression,
					new InvocationQueryExpression(new ConstantQueryExpression(Library.Time.Second), new QueryExpression[] { QueryExpressionMatcher.Capture(0, null) })
				}))
			}), QueryExpressionMatcher.OneOf(new QueryExpression[]
			{
				QueryExpressionMatcher.CaptureConstant(1, ValueKind.DateTime, (Value dt) => dt.AsDateTime.AsClrDateTime.Ticks % 600000000L == 0L),
				new BinaryQueryExpression(BinaryOperator2.Subtract, QueryExpressionMatcher.Capture(1, null), new InvocationQueryExpression(new ConstantQueryExpression(Library.Duration.duration), new QueryExpression[]
				{
					OdbcQueryExpressionVisitor.zeroExpression,
					OdbcQueryExpressionVisitor.zeroExpression,
					OdbcQueryExpressionVisitor.zeroExpression,
					new InvocationQueryExpression(new ConstantQueryExpression(Library.Time.Second), new QueryExpression[] { QueryExpressionMatcher.Capture(1, null) })
				}))
			}))
		});

		// Token: 0x04001650 RID: 5712
		private static readonly QueryExpression coalescePattern = new IfQueryExpression(new BinaryQueryExpression(BinaryOperator2.NotEquals, QueryExpressionMatcher.Capture(0, null), new ConstantQueryExpression(Value.Null)), QueryExpressionMatcher.Capture(0, null), QueryExpressionMatcher.Capture(1, null));

		// Token: 0x04001651 RID: 5713
		private string likeEscapeCharacter;

		// Token: 0x04001652 RID: 5714
		private OdbcTypeInfo bigIntType;

		// Token: 0x04001653 RID: 5715
		private OdbcTypeInfo integerType;

		// Token: 0x04001654 RID: 5716
		private OdbcTypeInfo doubleType;

		// Token: 0x04001655 RID: 5717
		private OdbcTypeInfo dateType;

		// Token: 0x04001656 RID: 5718
		private OdbcTypeInfo timestampType;

		// Token: 0x04001657 RID: 5719
		private OdbcTypeInfo timeType;

		// Token: 0x04001658 RID: 5720
		private OdbcDerivedColumnTypeInfo durationType;

		// Token: 0x04001659 RID: 5721
		private OdbcTypeInfo varcharType;

		// Token: 0x0400165A RID: 5722
		private OdbcTypeInfo logicalType;

		// Token: 0x02000629 RID: 1577
		// (Invoke) Token: 0x060032AC RID: 12972
		private delegate bool ExpressionVisitor(InvocationQueryExpression expression, OdbcScalarExpression argument, out OdbcScalarExpression visited);

		// Token: 0x0200062A RID: 1578
		// (Invoke) Token: 0x060032B0 RID: 12976
		private delegate bool TryGet<T>(out T value);

		// Token: 0x0200062B RID: 1579
		private class SoftConversionResult
		{
			// Token: 0x060032B3 RID: 12979 RVA: 0x000A2E62 File Offset: 0x000A1062
			public SoftConversionResult(OdbcScalarExpression expression, int rank)
			{
				this.expression = expression;
				this.rank = rank;
			}

			// Token: 0x17001256 RID: 4694
			// (get) Token: 0x060032B4 RID: 12980 RVA: 0x000A2E78 File Offset: 0x000A1078
			public OdbcScalarExpression Expression
			{
				get
				{
					return this.expression;
				}
			}

			// Token: 0x17001257 RID: 4695
			// (get) Token: 0x060032B5 RID: 12981 RVA: 0x000A2E80 File Offset: 0x000A1080
			public int Rank
			{
				get
				{
					return this.rank;
				}
			}

			// Token: 0x0400165B RID: 5723
			private readonly OdbcScalarExpression expression;

			// Token: 0x0400165C RID: 5724
			private readonly int rank;
		}

		// Token: 0x0200062C RID: 1580
		protected class CompatibilityAdjustmentResult
		{
			// Token: 0x060032B6 RID: 12982 RVA: 0x000A2E88 File Offset: 0x000A1088
			public CompatibilityAdjustmentResult(OdbcScalarExpression convertedLeft, OdbcScalarExpression convertedRight, OdbcDerivedColumnTypeInfo resultType)
				: this(true, convertedLeft, convertedRight, resultType)
			{
			}

			// Token: 0x060032B7 RID: 12983 RVA: 0x000A2E94 File Offset: 0x000A1094
			public CompatibilityAdjustmentResult(bool areCompatible, OdbcScalarExpression convertedLeft, OdbcScalarExpression convertedRight, OdbcDerivedColumnTypeInfo resultType)
			{
				this.areCompatible = areCompatible;
				this.convertedLeft = convertedLeft;
				this.convertedRight = convertedRight;
				this.resultType = resultType;
			}

			// Token: 0x17001258 RID: 4696
			// (get) Token: 0x060032B8 RID: 12984 RVA: 0x000A2EB9 File Offset: 0x000A10B9
			public bool AreCompatible
			{
				get
				{
					return this.areCompatible;
				}
			}

			// Token: 0x17001259 RID: 4697
			// (get) Token: 0x060032B9 RID: 12985 RVA: 0x000A2EC1 File Offset: 0x000A10C1
			public OdbcScalarExpression Left
			{
				get
				{
					return this.convertedLeft;
				}
			}

			// Token: 0x1700125A RID: 4698
			// (get) Token: 0x060032BA RID: 12986 RVA: 0x000A2EC9 File Offset: 0x000A10C9
			public OdbcScalarExpression Right
			{
				get
				{
					return this.convertedRight;
				}
			}

			// Token: 0x1700125B RID: 4699
			// (get) Token: 0x060032BB RID: 12987 RVA: 0x000A2ED1 File Offset: 0x000A10D1
			public OdbcDerivedColumnTypeInfo TypeInfo
			{
				get
				{
					return this.resultType;
				}
			}

			// Token: 0x0400165D RID: 5725
			private readonly bool areCompatible;

			// Token: 0x0400165E RID: 5726
			private readonly OdbcScalarExpression convertedLeft;

			// Token: 0x0400165F RID: 5727
			private readonly OdbcScalarExpression convertedRight;

			// Token: 0x04001660 RID: 5728
			private readonly OdbcDerivedColumnTypeInfo resultType;
		}

		// Token: 0x0200062D RID: 1581
		private class OdbcDateTimeLiteral : SqlExpression
		{
			// Token: 0x060032BC RID: 12988 RVA: 0x000A2ED9 File Offset: 0x000A10D9
			public OdbcDateTimeLiteral(ConstantSqlString type, string value)
			{
				this.type = type;
				this.value = new SqlConstant(ConstantType.AnsiString, value);
			}

			// Token: 0x1700125C RID: 4700
			// (get) Token: 0x060032BD RID: 12989 RVA: 0x00002105 File Offset: 0x00000305
			public override int Precedence
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x060032BE RID: 12990 RVA: 0x000A2EF5 File Offset: 0x000A10F5
			public override void WriteCreateScript(ScriptWriter writer)
			{
				writer.Write(SqlLanguageSymbols.OpenCurlyBraceSqlString);
				writer.Write(this.type);
				writer.WriteSpace();
				this.value.WriteCreateScript(writer);
				writer.Write(SqlLanguageSymbols.CloseCurlyBraceSqlString);
			}

			// Token: 0x04001661 RID: 5729
			public static readonly ConstantSqlString d = new ConstantSqlString("d");

			// Token: 0x04001662 RID: 5730
			public static readonly ConstantSqlString t = new ConstantSqlString("t");

			// Token: 0x04001663 RID: 5731
			public static readonly ConstantSqlString ts = new ConstantSqlString("ts");

			// Token: 0x04001664 RID: 5732
			private readonly ConstantSqlString type;

			// Token: 0x04001665 RID: 5733
			private readonly SqlConstant value;
		}

		// Token: 0x0200062E RID: 1582
		private enum CaptureIndex
		{
			// Token: 0x04001667 RID: 5735
			Left,
			// Token: 0x04001668 RID: 5736
			Right,
			// Token: 0x04001669 RID: 5737
			Count
		}
	}
}
