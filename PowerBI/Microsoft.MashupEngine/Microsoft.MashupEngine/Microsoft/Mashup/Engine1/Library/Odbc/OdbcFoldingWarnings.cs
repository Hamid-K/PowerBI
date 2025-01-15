using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005F8 RID: 1528
	internal static class OdbcFoldingWarnings
	{
		// Token: 0x06003030 RID: 12336 RVA: 0x00091E04 File Offset: 0x00090004
		private static string OdbcSqlExpressionToString(OdbcSqlExpression expression, SqlSettings sqlSettings)
		{
			if (expression == null)
			{
				return "null";
			}
			switch (expression.Kind)
			{
			case OdbcSqlExpressionKind.Condition:
				return OdbcFoldingWarnings.IScriptableToString(expression.AsCondition.Expression, sqlSettings);
			case OdbcSqlExpressionKind.Scalar:
				return OdbcFoldingWarnings.IScriptableToString(expression.AsScalar.Expression, sqlSettings);
			case OdbcSqlExpressionKind.Statement:
				return OdbcFoldingWarnings.IScriptableToString(expression.AsStatement.Statement, sqlSettings);
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06003031 RID: 12337 RVA: 0x00091E70 File Offset: 0x00090070
		public static string IScriptableToString(IScriptable expression, SqlSettings sqlSettings)
		{
			string text;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				ScriptWriter scriptWriter = new ScriptWriter(stringWriter, sqlSettings);
				expression.WriteCreateScript(scriptWriter);
				text = stringWriter.ToString();
			}
			return text;
		}

		// Token: 0x06003032 RID: 12338 RVA: 0x00091EBC File Offset: 0x000900BC
		private static string OdbcTypeMapsToString(OdbcTypeMap[] maps)
		{
			return string.Join(",", maps.Select((OdbcTypeMap typeMap) => typeMap.Token).ToArray<string>());
		}

		// Token: 0x06003033 RID: 12339 RVA: 0x00091EF2 File Offset: 0x000900F2
		public static FoldingWarnings.FoldingWarning<string> NonOdbcQueryAtJoin(string querySide)
		{
			return new FoldingWarnings.FoldingWarning<string>("The {0} query is not an ODBC query.", querySide);
		}

		// Token: 0x06003034 RID: 12340 RVA: 0x00091EFF File Offset: 0x000900FF
		public static FoldingWarnings.FoldingWarning<string> OdbcOption(string property)
		{
			return new FoldingWarnings.FoldingWarning<string>("The {0} feature has not been set. You can override it by using the option parameter in Odbc.DataSource/Odbc.Query.", property);
		}

		// Token: 0x06003035 RID: 12341 RVA: 0x00091F0C File Offset: 0x0009010C
		public static FoldingWarnings.FoldingWarning<BinaryOperator2, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, string> BinaryOperation(SqlSettings sqlSettings, BinaryOperator2 op, OdbcScalarExpression left, OdbcScalarExpression right, string supportedOperators)
		{
			return new FoldingWarnings.FoldingWarning<BinaryOperator2, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, string>("Failed for the {0} Operator with {1} and {2}. Supported binary operators for this scenario {3}", op, new FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>(left, sqlSettings, new Func<OdbcSqlExpression, SqlSettings, string>(OdbcFoldingWarnings.OdbcSqlExpressionToString)), new FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>(right, sqlSettings, new Func<OdbcSqlExpression, SqlSettings, string>(OdbcFoldingWarnings.OdbcSqlExpressionToString)), supportedOperators);
		}

		// Token: 0x06003036 RID: 12342 RVA: 0x00091F41 File Offset: 0x00090141
		public static FoldingWarnings.FoldingWarning<string, string, FoldingWarnings.StringFormatter<IScriptable, SqlSettings>, Odbc32.SQL_TYPE, Odbc32.SQL_TYPE> Conversion(SqlSettings sqlSettings, OdbcTypeInfo fromType, OdbcTypeInfo toType, SqlExpression expression)
		{
			return new FoldingWarnings.FoldingWarning<string, string, FoldingWarnings.StringFormatter<IScriptable, SqlSettings>, Odbc32.SQL_TYPE, Odbc32.SQL_TYPE>("Failed to convert from {0} to {1} for expression {2}. You can override this by using SqlGetInfo for converting from {3} to {4}.", fromType.Name, toType.Name, new FoldingWarnings.StringFormatter<IScriptable, SqlSettings>(expression, sqlSettings, new Func<IScriptable, SqlSettings, string>(OdbcFoldingWarnings.IScriptableToString)), fromType.SqlType, toType.SqlType);
		}

		// Token: 0x06003037 RID: 12343 RVA: 0x00091F78 File Offset: 0x00090178
		public static FoldingWarnings.FoldingWarning<string, string, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>> Conversion(SqlSettings sqlSettings, OdbcScalarExpression left, OdbcScalarExpression right)
		{
			return new FoldingWarnings.FoldingWarning<string, string, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>>("Failed to convert type {0} to {1}, expression {2} to {3}.", left.TypeInfo.DataSourceType.Name, right.TypeInfo.DataSourceType.Name, new FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>(left, sqlSettings, new Func<OdbcSqlExpression, SqlSettings, string>(OdbcFoldingWarnings.OdbcSqlExpressionToString)), new FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>(right, sqlSettings, new Func<OdbcSqlExpression, SqlSettings, string>(OdbcFoldingWarnings.OdbcSqlExpressionToString)));
		}

		// Token: 0x06003038 RID: 12344 RVA: 0x00091FD5 File Offset: 0x000901D5
		public static FoldingWarnings.FoldingWarning<string, string> DataTypeNotCompatible(string dataTypeName, string reason)
		{
			return new FoldingWarnings.FoldingWarning<string, string>("ODBC data type {0} doesn't meet the requirement because {1}. You can override this by using SQLGetTypeInfo.", dataTypeName, reason);
		}

		// Token: 0x06003039 RID: 12345 RVA: 0x00091FE3 File Offset: 0x000901E3
		public static FoldingWarnings.FoldingWarning<Odbc32.SQL_TYPE> DataTypeNotFound(Odbc32.SQL_TYPE sqlType)
		{
			return new FoldingWarnings.FoldingWarning<Odbc32.SQL_TYPE>("ODBC SQL Type {0} was not found in the SQLGetTypeInfo data from the ODBC driver. You can override the type information using SQLGetTypeInfo.", sqlType);
		}

		// Token: 0x0600303A RID: 12346 RVA: 0x00091FF0 File Offset: 0x000901F0
		public static FoldingWarnings.FoldingWarning<string, Odbc32.SQL_SEARCHABLE> DataTypeNotSearchable(OdbcQueryColumnInfo columnInfo)
		{
			if (!string.IsNullOrEmpty(columnInfo.TypeInfo.DataSourceType.Name))
			{
				return new FoldingWarnings.FoldingWarning<string, Odbc32.SQL_SEARCHABLE>("Data Type {0} with searchable property {1} should be SEARCHABLE or ALL_EXCEPT_LIKE. You can override the supported data types from ODBC driver using SQLGetTypeInfo.", columnInfo.TypeInfo.DataSourceType.Name, columnInfo.TypeInfo.DataSourceType.Searchable);
			}
			return new FoldingWarnings.FoldingWarning<string, Odbc32.SQL_SEARCHABLE>("Data Type of column {0} with searchable property {1} should be SEARCHABLE or ALL_EXCEPT_LIKE. You can override the supported data types from ODBC driver using SQLGetTypeInfo.", columnInfo.LocalName, columnInfo.TypeInfo.DataSourceType.Searchable);
		}

		// Token: 0x0600303B RID: 12347 RVA: 0x0009205F File Offset: 0x0009025F
		public static FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, BinaryOperator2, ValueKind> DurationOperation(SqlSettings sqlSettings, OdbcScalarExpression left, OdbcScalarExpression right, BinaryOperator2 op, ValueKind dateKind)
		{
			return new FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, BinaryOperator2, ValueKind>("Failed for the {0} Operator with {1} and {2}, make sure they are types {3} and Duration.", new FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>(left, sqlSettings, new Func<OdbcSqlExpression, SqlSettings, string>(OdbcFoldingWarnings.OdbcSqlExpressionToString)), new FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>(right, sqlSettings, new Func<OdbcSqlExpression, SqlSettings, string>(OdbcFoldingWarnings.OdbcSqlExpressionToString)), op, dateKind);
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x00092094 File Offset: 0x00090294
		public static FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, string> IncompatibleExpressions(SqlSettings sqlSettings, OdbcScalarExpression left, OdbcScalarExpression right, string reason)
		{
			return new FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, string>("Expression {0} and expression {1} are not compatible because {2}.", new FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>(left, sqlSettings, new Func<OdbcSqlExpression, SqlSettings, string>(OdbcFoldingWarnings.OdbcSqlExpressionToString)), new FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>(right, sqlSettings, new Func<OdbcSqlExpression, SqlSettings, string>(OdbcFoldingWarnings.OdbcSqlExpressionToString)), reason);
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x000920C7 File Offset: 0x000902C7
		public static FoldingWarnings.FoldingWarning<BinaryOperator2, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>> InvalidBinaryOperator(SqlSettings sqlSettings, BinaryOperator2 binaryOperator, OdbcSqlExpression left, OdbcSqlExpression right)
		{
			return new FoldingWarnings.FoldingWarning<BinaryOperator2, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>>("Binary operator {0} is not supported for expressions {1} and {2}.", binaryOperator, new FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>(left, sqlSettings, new Func<OdbcSqlExpression, SqlSettings, string>(OdbcFoldingWarnings.OdbcSqlExpressionToString)), new FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>(right, sqlSettings, new Func<OdbcSqlExpression, SqlSettings, string>(OdbcFoldingWarnings.OdbcSqlExpressionToString)));
		}

		// Token: 0x0600303E RID: 12350 RVA: 0x000920FA File Offset: 0x000902FA
		public static FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, string> InvalidType(SqlSettings sqlSettings, OdbcSqlExpression expression, string reason)
		{
			return new FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, string>("The type of expression {0} should be {1}.", new FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>(expression, sqlSettings, new Func<OdbcSqlExpression, SqlSettings, string>(OdbcFoldingWarnings.OdbcSqlExpressionToString)), reason);
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x0009211A File Offset: 0x0009031A
		public static FoldingWarnings.FoldingWarning<UnaryOperator2, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, string> InvalidUnaryOperation(SqlSettings sqlSettings, UnaryOperator2 unaryOperator, OdbcSqlExpression expression, string reason)
		{
			return new FoldingWarnings.FoldingWarning<UnaryOperator2, FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>, string>("Operator {0} with expression {1} is not supported: {2}.", unaryOperator, new FoldingWarnings.StringFormatter<OdbcSqlExpression, SqlSettings>(expression, sqlSettings, new Func<OdbcSqlExpression, SqlSettings, string>(OdbcFoldingWarnings.OdbcSqlExpressionToString)), reason);
		}

		// Token: 0x06003040 RID: 12352 RVA: 0x0009213B File Offset: 0x0009033B
		public static FoldingWarnings.FoldingWarning<int, FoldingWarnings.StringFormatter<OdbcTypeMap[]>> NoMatchedDataType(int size, OdbcTypeMap[] maps)
		{
			return new FoldingWarnings.FoldingWarning<int, FoldingWarnings.StringFormatter<OdbcTypeMap[]>>("Unable to find data type with size {0} from data types {1}.", size, new FoldingWarnings.StringFormatter<OdbcTypeMap[]>(maps, new Func<OdbcTypeMap[], string>(OdbcFoldingWarnings.OdbcTypeMapsToString)));
		}

		// Token: 0x06003041 RID: 12353 RVA: 0x0009215A File Offset: 0x0009035A
		public static FoldingWarnings.FoldingWarning<T, Odbc32.SQL_INFO> SqlGetInfo<T>(Odbc32.SQL_INFO sqlInfo, T property)
		{
			return new FoldingWarnings.FoldingWarning<T, Odbc32.SQL_INFO>("This ODBC driver doesn't support {0}. You can override this by using SqlGetInfo for {1}.", property, sqlInfo);
		}

		// Token: 0x06003042 RID: 12354 RVA: 0x00092168 File Offset: 0x00090368
		public static FoldingWarnings.FoldingWarning<string, FoldingWarnings.StringFormatter<SqlSettings>> InvalidCharacter(string identifier, SqlSettings sqlSettings)
		{
			return new FoldingWarnings.FoldingWarning<string, FoldingWarnings.StringFormatter<SqlSettings>>("Identifier {0} contains an invalid character specified by the driver's invalid characters list {1}. You can override it using SqlGetInfo with SQL_SPECIAL_CHARACTERS.", identifier, new FoldingWarnings.StringFormatter<SqlSettings>(sqlSettings, new Func<SqlSettings, string>(OdbcFoldingWarnings.SqlSettingsInvalidCharactersToString)));
		}

		// Token: 0x06003043 RID: 12355 RVA: 0x00092187 File Offset: 0x00090387
		public static string SqlSettingsInvalidCharactersToString(SqlSettings sqlSettings)
		{
			return string.Join(",", sqlSettings.InvalidIdentifierCharacters.Select((char character) => character.ToString()).ToArray<string>());
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x000921C2 File Offset: 0x000903C2
		public static FoldingWarnings.FoldingWarning<string, string> QualifierAtOrderByColumn(ColumnReference columnReference)
		{
			return new FoldingWarnings.FoldingWarning<string, string>("Order by column {0} should not have qualifier {1}.", columnReference.Name.Name, columnReference.Qualifier.OriginalName);
		}

		// Token: 0x04001532 RID: 5426
		public const string DataTypeInvalidFloatSize = "This ODBC driver doesn't support SQLTYPE FLOAT with the same size as DOUBLE. You can override this using SQLGetTypeInfo.";

		// Token: 0x04001533 RID: 5427
		public const string NoInvalidCharacterSetting = "This ODBC driver doesn't provide an invalid character list.";

		// Token: 0x04001534 RID: 5428
		public const string InvalidCharacterFormat = "Identifier {0} contains an invalid character specified by the driver's invalid characters list {1}. You can override it using SqlGetInfo with SQL_SPECIAL_CHARACTERS.";

		// Token: 0x04001535 RID: 5429
		public const string QualifierAtOrderByColumnFormat = "Order by column {0} should not have qualifier {1}.";
	}
}
