using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder.Spatial;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Hierarchy;
using System.Data.Entity.Spatial;
using System.Data.Entity.SqlServer.Resources;
using System.Data.Entity.SqlServer.Utilities;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x02000037 RID: 55
	internal class SqlGenerator : DbExpressionVisitor<ISqlFragment>
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x000150D7 File Offset: 0x000132D7
		private SqlSelectStatement CurrentSelectStatement
		{
			get
			{
				return this.selectStatementStack.Peek();
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x000150E4 File Offset: 0x000132E4
		private bool IsParentAJoin
		{
			get
			{
				return this.isParentAJoinStack.Count != 0 && this.isParentAJoinStack.Peek();
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x00015100 File Offset: 0x00013300
		internal Dictionary<string, int> AllExtentNames
		{
			get
			{
				return this.allExtentNames;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x00015108 File Offset: 0x00013308
		internal Dictionary<string, int> AllColumnNames
		{
			get
			{
				return this.allColumnNames;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x00015110 File Offset: 0x00013310
		public List<string> Targets
		{
			get
			{
				return this._targets;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x00015118 File Offset: 0x00013318
		internal SqlVersion SqlVersion
		{
			get
			{
				return this._sqlVersion;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x00015120 File Offset: 0x00013320
		internal bool IsPreKatmai
		{
			get
			{
				return SqlVersionUtils.IsPreKatmai(this.SqlVersion);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x00015130 File Offset: 0x00013330
		internal TypeUsage IntegerType
		{
			get
			{
				TypeUsage typeUsage;
				if ((typeUsage = this._integerType) == null)
				{
					typeUsage = (this._integerType = TypeUsage.CreateDefaultTypeUsage(this.StoreItemCollection.GetPrimitiveTypes().First((PrimitiveType t) => t.PrimitiveTypeKind == PrimitiveTypeKind.Int64)));
				}
				return typeUsage;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x00015184 File Offset: 0x00013384
		internal virtual StoreItemCollection StoreItemCollection
		{
			get
			{
				return this._storeItemCollection;
			}
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0001518C File Offset: 0x0001338C
		internal SqlGenerator()
		{
			this._sqlVersion = SqlVersion.Sql11;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x000151BD File Offset: 0x000133BD
		internal SqlGenerator(SqlVersion sqlVersion)
		{
			this._sqlVersion = sqlVersion;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x000151F0 File Offset: 0x000133F0
		internal static string GenerateSql(DbCommandTree tree, SqlVersion sqlVersion, out List<SqlParameter> parameters, out CommandType commandType, out HashSet<string> paramsToForceNonUnicode)
		{
			commandType = CommandType.Text;
			parameters = null;
			paramsToForceNonUnicode = null;
			SqlGenerator sqlGenerator = new SqlGenerator(sqlVersion);
			switch (tree.CommandTreeKind)
			{
			case DbCommandTreeKind.Query:
				return sqlGenerator.GenerateSql((DbQueryCommandTree)tree, out paramsToForceNonUnicode);
			case DbCommandTreeKind.Update:
				return DmlSqlGenerator.GenerateUpdateSql((DbUpdateCommandTree)tree, sqlGenerator, out parameters, true, true);
			case DbCommandTreeKind.Insert:
				return DmlSqlGenerator.GenerateInsertSql((DbInsertCommandTree)tree, sqlGenerator, out parameters, true, true, true);
			case DbCommandTreeKind.Delete:
				return DmlSqlGenerator.GenerateDeleteSql((DbDeleteCommandTree)tree, sqlGenerator, out parameters, true, true);
			case DbCommandTreeKind.Function:
				return SqlGenerator.GenerateFunctionSql((DbFunctionCommandTree)tree, out commandType);
			default:
				return null;
			}
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00015280 File Offset: 0x00013480
		private static string GenerateFunctionSql(DbFunctionCommandTree tree, out CommandType commandType)
		{
			EdmFunction edmFunction = tree.EdmFunction;
			if (string.IsNullOrEmpty(edmFunction.CommandTextAttribute))
			{
				commandType = CommandType.StoredProcedure;
				string text = (string.IsNullOrEmpty(edmFunction.Schema) ? edmFunction.NamespaceName : edmFunction.Schema);
				string text2 = (string.IsNullOrEmpty(edmFunction.StoreFunctionNameAttribute) ? edmFunction.Name : edmFunction.StoreFunctionNameAttribute);
				string text3 = SqlGenerator.QuoteIdentifier(text);
				string text4 = SqlGenerator.QuoteIdentifier(text2);
				return text3 + "." + text4;
			}
			commandType = CommandType.Text;
			return edmFunction.CommandTextAttribute;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00015300 File Offset: 0x00013500
		internal string GenerateSql(DbQueryCommandTree tree, out HashSet<string> paramsToForceNonUnicode)
		{
			this._targets = new List<string>();
			DbQueryCommandTree dbQueryCommandTree = tree;
			if (this.SqlVersion == SqlVersion.Sql8 && Sql8ConformanceChecker.NeedsRewrite(tree.Query))
			{
				dbQueryCommandTree = Sql8ExpressionRewriter.Rewrite(tree);
			}
			this._storeItemCollection = (StoreItemCollection)dbQueryCommandTree.MetadataWorkspace.GetItemCollection(DataSpace.SSpace);
			this.selectStatementStack = new Stack<SqlSelectStatement>();
			this.isParentAJoinStack = new Stack<bool>();
			this.allExtentNames = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
			this.allColumnNames = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
			ISqlFragment sqlFragment;
			if (BuiltInTypeKind.CollectionType == dbQueryCommandTree.Query.ResultType.EdmType.BuiltInTypeKind)
			{
				SqlSelectStatement sqlSelectStatement = this.VisitExpressionEnsureSqlStatement(dbQueryCommandTree.Query);
				sqlSelectStatement.IsTopMost = true;
				sqlFragment = sqlSelectStatement;
			}
			else
			{
				SqlBuilder sqlBuilder = new SqlBuilder();
				sqlBuilder.Append("SELECT ");
				sqlBuilder.Append(dbQueryCommandTree.Query.Accept<ISqlFragment>(this));
				sqlFragment = sqlBuilder;
			}
			if (this.isVarRefSingle)
			{
				throw new NotSupportedException();
			}
			paramsToForceNonUnicode = new HashSet<string>(from p in this._candidateParametersToForceNonUnicode
				where p.Value
				select p into q
				select q.Key);
			StringBuilder stringBuilder = new StringBuilder(1024);
			using (SqlWriter sqlWriter = new SqlWriter(stringBuilder))
			{
				this.WriteSql(sqlWriter, sqlFragment);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00015478 File Offset: 0x00013678
		internal SqlWriter WriteSql(SqlWriter writer, ISqlFragment sqlStatement)
		{
			sqlStatement.WriteSql(writer, this);
			return writer;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00015483 File Offset: 0x00013683
		public override ISqlFragment Visit(DbAndExpression e)
		{
			Check.NotNull<DbAndExpression>(e, "e");
			return this.VisitBinaryExpression(" AND ", DbExpressionKind.And, e.Left, e.Right);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x000154AC File Offset: 0x000136AC
		public override ISqlFragment Visit(DbApplyExpression e)
		{
			Check.NotNull<DbApplyExpression>(e, "e");
			List<DbExpressionBinding> list = new List<DbExpressionBinding>();
			list.Add(e.Input);
			list.Add(e.Apply);
			DbExpressionKind expressionKind = e.ExpressionKind;
			string text;
			if (expressionKind != DbExpressionKind.CrossApply)
			{
				if (expressionKind != DbExpressionKind.OuterApply)
				{
					throw new InvalidOperationException(string.Empty);
				}
				text = "OUTER APPLY";
			}
			else
			{
				text = "CROSS APPLY";
			}
			return this.VisitJoinExpression(list, DbExpressionKind.CrossJoin, text, null);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0001551C File Offset: 0x0001371C
		public override ISqlFragment Visit(DbArithmeticExpression e)
		{
			Check.NotNull<DbArithmeticExpression>(e, "e");
			DbExpressionKind expressionKind = e.ExpressionKind;
			if (expressionKind <= DbExpressionKind.Multiply)
			{
				if (expressionKind == DbExpressionKind.Divide)
				{
					return this.VisitBinaryExpression(" / ", e.ExpressionKind, e.Arguments[0], e.Arguments[1]);
				}
				switch (expressionKind)
				{
				case DbExpressionKind.Minus:
					return this.VisitBinaryExpression(" - ", e.ExpressionKind, e.Arguments[0], e.Arguments[1]);
				case DbExpressionKind.Modulo:
					return this.VisitBinaryExpression(" % ", e.ExpressionKind, e.Arguments[0], e.Arguments[1]);
				case DbExpressionKind.Multiply:
					return this.VisitBinaryExpression(" * ", e.ExpressionKind, e.Arguments[0], e.Arguments[1]);
				}
			}
			else
			{
				if (expressionKind == DbExpressionKind.Plus)
				{
					return this.VisitBinaryExpression(" + ", e.ExpressionKind, e.Arguments[0], e.Arguments[1]);
				}
				if (expressionKind == DbExpressionKind.UnaryMinus)
				{
					SqlBuilder sqlBuilder = new SqlBuilder();
					sqlBuilder.Append(" -(");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(this));
					sqlBuilder.Append(")");
					return sqlBuilder;
				}
			}
			throw new InvalidOperationException(string.Empty);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0001569C File Offset: 0x0001389C
		public override ISqlFragment Visit(DbCaseExpression e)
		{
			Check.NotNull<DbCaseExpression>(e, "e");
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append("CASE");
			for (int i = 0; i < e.When.Count; i++)
			{
				sqlBuilder.Append(" WHEN (");
				sqlBuilder.Append(e.When[i].Accept<ISqlFragment>(this));
				sqlBuilder.Append(") THEN ");
				sqlBuilder.Append(e.Then[i].Accept<ISqlFragment>(this));
			}
			if (e.Else != null && !(e.Else is DbNullExpression))
			{
				sqlBuilder.Append(" ELSE ");
				sqlBuilder.Append(e.Else.Accept<ISqlFragment>(this));
			}
			sqlBuilder.Append(" END");
			return sqlBuilder;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00015760 File Offset: 0x00013960
		public override ISqlFragment Visit(DbCastExpression e)
		{
			Check.NotNull<DbCastExpression>(e, "e");
			if (e.ResultType.IsSpatialType())
			{
				return e.Argument.Accept<ISqlFragment>(this);
			}
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append(" CAST( ");
			sqlBuilder.Append(e.Argument.Accept<ISqlFragment>(this));
			sqlBuilder.Append(" AS ");
			sqlBuilder.Append(this.GetSqlPrimitiveType(e.ResultType));
			sqlBuilder.Append(")");
			return sqlBuilder;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x000157E0 File Offset: 0x000139E0
		public override ISqlFragment Visit(DbComparisonExpression e)
		{
			Check.NotNull<DbComparisonExpression>(e, "e");
			if (e.Left.ResultType.IsPrimitiveType(PrimitiveTypeKind.String))
			{
				this._forceNonUnicode = this.CheckIfForceNonUnicodeRequired(e);
			}
			DbExpressionKind expressionKind = e.ExpressionKind;
			SqlBuilder sqlBuilder;
			if (expressionKind <= DbExpressionKind.GreaterThanOrEquals)
			{
				if (expressionKind == DbExpressionKind.Equals)
				{
					sqlBuilder = this.VisitComparisonExpression(" = ", e.Left, e.Right);
					goto IL_0111;
				}
				if (expressionKind == DbExpressionKind.GreaterThan)
				{
					sqlBuilder = this.VisitComparisonExpression(" > ", e.Left, e.Right);
					goto IL_0111;
				}
				if (expressionKind == DbExpressionKind.GreaterThanOrEquals)
				{
					sqlBuilder = this.VisitComparisonExpression(" >= ", e.Left, e.Right);
					goto IL_0111;
				}
			}
			else
			{
				if (expressionKind == DbExpressionKind.LessThan)
				{
					sqlBuilder = this.VisitComparisonExpression(" < ", e.Left, e.Right);
					goto IL_0111;
				}
				if (expressionKind == DbExpressionKind.LessThanOrEquals)
				{
					sqlBuilder = this.VisitComparisonExpression(" <= ", e.Left, e.Right);
					goto IL_0111;
				}
				if (expressionKind == DbExpressionKind.NotEquals)
				{
					sqlBuilder = this.VisitComparisonExpression(" <> ", e.Left, e.Right);
					goto IL_0111;
				}
			}
			throw new InvalidOperationException(string.Empty);
			IL_0111:
			this._forceNonUnicode = false;
			return sqlBuilder;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00015906 File Offset: 0x00013B06
		private bool CheckIfForceNonUnicodeRequired(DbExpression e)
		{
			if (this._forceNonUnicode)
			{
				throw new NotSupportedException();
			}
			return this.MatchPatternForForcingNonUnicode(e);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00015920 File Offset: 0x00013B20
		private bool MatchPatternForForcingNonUnicode(DbExpression e)
		{
			DbExpressionKind expressionKind = e.ExpressionKind;
			if (expressionKind == DbExpressionKind.Like)
			{
				DbLikeExpression dbLikeExpression = (DbLikeExpression)e;
				return SqlGenerator.MatchSourcePatternForForcingNonUnicode(dbLikeExpression.Argument) && this.MatchTargetPatternForForcingNonUnicode(dbLikeExpression.Pattern) && this.MatchTargetPatternForForcingNonUnicode(dbLikeExpression.Escape);
			}
			if (expressionKind != DbExpressionKind.In)
			{
				DbComparisonExpression dbComparisonExpression = (DbComparisonExpression)e;
				DbExpression left = dbComparisonExpression.Left;
				DbExpression right = dbComparisonExpression.Right;
				return (SqlGenerator.MatchSourcePatternForForcingNonUnicode(left) && this.MatchTargetPatternForForcingNonUnicode(right)) || (SqlGenerator.MatchSourcePatternForForcingNonUnicode(right) && this.MatchTargetPatternForForcingNonUnicode(left));
			}
			return SqlGenerator.MatchSourcePatternForForcingNonUnicode(((DbInExpression)e).Item);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x000159BC File Offset: 0x00013BBC
		internal bool MatchTargetPatternForForcingNonUnicode(DbExpression expr)
		{
			if (SqlGenerator.IsConstParamOrNullExpressionUnicodeNotSpecified(expr))
			{
				return true;
			}
			if (expr.ExpressionKind == DbExpressionKind.Function)
			{
				DbFunctionExpression dbFunctionExpression = (DbFunctionExpression)expr;
				EdmFunction function = dbFunctionExpression.Function;
				if (!function.IsCanonicalFunction() && !SqlFunctionCallHandler.IsStoreFunction(function))
				{
					return false;
				}
				string fullName = function.FullName;
				if (SqlGenerator._canonicalAndStoreStringFunctionsOneArg.Contains(fullName))
				{
					return this.MatchTargetPatternForForcingNonUnicode(dbFunctionExpression.Arguments[0]);
				}
				if ("Edm.Concat".Equals(fullName, StringComparison.Ordinal))
				{
					return this.MatchTargetPatternForForcingNonUnicode(dbFunctionExpression.Arguments[0]) && this.MatchTargetPatternForForcingNonUnicode(dbFunctionExpression.Arguments[1]);
				}
				if ("Edm.Replace".Equals(fullName, StringComparison.Ordinal) || "SqlServer.REPLACE".Equals(fullName, StringComparison.Ordinal))
				{
					return this.MatchTargetPatternForForcingNonUnicode(dbFunctionExpression.Arguments[0]) && this.MatchTargetPatternForForcingNonUnicode(dbFunctionExpression.Arguments[1]) && this.MatchTargetPatternForForcingNonUnicode(dbFunctionExpression.Arguments[2]);
				}
			}
			return false;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00015AB8 File Offset: 0x00013CB8
		private static bool MatchSourcePatternForForcingNonUnicode(DbExpression argument)
		{
			bool flag;
			return argument.ExpressionKind == DbExpressionKind.Property && argument.ResultType.TryGetIsUnicode(out flag) && !flag;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00015AE4 File Offset: 0x00013CE4
		internal static bool IsConstParamOrNullExpressionUnicodeNotSpecified(DbExpression argument)
		{
			DbExpressionKind expressionKind = argument.ExpressionKind;
			TypeUsage resultType = argument.ResultType;
			bool flag;
			return resultType.IsPrimitiveType(PrimitiveTypeKind.String) && (expressionKind == DbExpressionKind.Constant || expressionKind == DbExpressionKind.ParameterReference || expressionKind == DbExpressionKind.Null) && !resultType.TryGetFacetValue("Unicode", out flag);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00015B2C File Offset: 0x00013D2C
		private ISqlFragment VisitConstant(DbConstantExpression e, bool isCastOptional)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			TypeUsage resultType = e.ResultType;
			if (resultType.IsPrimitiveType())
			{
				PrimitiveTypeKind primitiveTypeKind = resultType.GetPrimitiveTypeKind();
				switch (primitiveTypeKind)
				{
				case PrimitiveTypeKind.Binary:
					sqlBuilder.Append(" 0x");
					sqlBuilder.Append(SqlGenerator.ByteArrayToBinaryString((byte[])e.Value));
					sqlBuilder.Append(" ");
					return sqlBuilder;
				case PrimitiveTypeKind.Boolean:
					SqlGenerator.WrapWithCastIfNeeded(!isCastOptional, ((bool)e.Value) ? "1" : "0", "bit", sqlBuilder);
					return sqlBuilder;
				case PrimitiveTypeKind.Byte:
					SqlGenerator.WrapWithCastIfNeeded(!isCastOptional, e.Value.ToString(), "tinyint", sqlBuilder);
					return sqlBuilder;
				case PrimitiveTypeKind.DateTime:
					sqlBuilder.Append("convert(");
					sqlBuilder.Append(this.IsPreKatmai ? "datetime" : "datetime2");
					sqlBuilder.Append(", ");
					sqlBuilder.Append(SqlGenerator.EscapeSingleQuote(((DateTime)e.Value).ToString(this.IsPreKatmai ? "yyyy-MM-dd HH:mm:ss.fff" : "yyyy-MM-dd HH:mm:ss.fffffff", CultureInfo.InvariantCulture), false));
					sqlBuilder.Append(", 121)");
					return sqlBuilder;
				case PrimitiveTypeKind.Decimal:
				{
					string text = ((decimal)e.Value).ToString(CultureInfo.InvariantCulture);
					bool flag = -1 == text.IndexOf('.') && text.TrimStart(new char[] { '-' }).Length < 20;
					string text2 = "decimal(" + Math.Max((byte)text.Length, 18).ToString(CultureInfo.InvariantCulture) + ")";
					SqlGenerator.WrapWithCastIfNeeded(flag, text, text2, sqlBuilder);
					return sqlBuilder;
				}
				case PrimitiveTypeKind.Double:
				{
					double num = (double)e.Value;
					SqlGenerator.AssertValidDouble(num);
					SqlGenerator.WrapWithCastIfNeeded(true, num.ToString("R", CultureInfo.InvariantCulture), "float(53)", sqlBuilder);
					return sqlBuilder;
				}
				case PrimitiveTypeKind.Guid:
					SqlGenerator.WrapWithCastIfNeeded(true, SqlGenerator.EscapeSingleQuote(e.Value.ToString(), false), "uniqueidentifier", sqlBuilder);
					return sqlBuilder;
				case PrimitiveTypeKind.Single:
				{
					float num2 = (float)e.Value;
					SqlGenerator.AssertValidSingle(num2);
					SqlGenerator.WrapWithCastIfNeeded(true, num2.ToString("R", CultureInfo.InvariantCulture), "real", sqlBuilder);
					return sqlBuilder;
				}
				case PrimitiveTypeKind.Int16:
					SqlGenerator.WrapWithCastIfNeeded(!isCastOptional, e.Value.ToString(), "smallint", sqlBuilder);
					return sqlBuilder;
				case PrimitiveTypeKind.Int32:
					sqlBuilder.Append(e.Value.ToString());
					return sqlBuilder;
				case PrimitiveTypeKind.Int64:
					SqlGenerator.WrapWithCastIfNeeded(!isCastOptional, e.Value.ToString(), "bigint", sqlBuilder);
					return sqlBuilder;
				case PrimitiveTypeKind.String:
				{
					bool flag2;
					if (!e.ResultType.TryGetIsUnicode(out flag2))
					{
						flag2 = !this._forceNonUnicode;
					}
					sqlBuilder.Append(SqlGenerator.EscapeSingleQuote(e.Value as string, flag2));
					return sqlBuilder;
				}
				case PrimitiveTypeKind.Time:
					this.AssertKatmaiOrNewer(primitiveTypeKind);
					sqlBuilder.Append("convert(");
					sqlBuilder.Append(e.ResultType.EdmType.Name);
					sqlBuilder.Append(", ");
					sqlBuilder.Append(SqlGenerator.EscapeSingleQuote(e.Value.ToString(), false));
					sqlBuilder.Append(", 121)");
					return sqlBuilder;
				case PrimitiveTypeKind.DateTimeOffset:
					this.AssertKatmaiOrNewer(primitiveTypeKind);
					sqlBuilder.Append("convert(");
					sqlBuilder.Append(e.ResultType.EdmType.Name);
					sqlBuilder.Append(", ");
					sqlBuilder.Append(SqlGenerator.EscapeSingleQuote(((DateTimeOffset)e.Value).ToString("yyyy-MM-dd HH:mm:ss.fffffff zzz", CultureInfo.InvariantCulture), false));
					sqlBuilder.Append(", 121)");
					return sqlBuilder;
				case PrimitiveTypeKind.Geometry:
					this.AppendSpatialConstant(sqlBuilder, ((DbGeometry)e.Value).AsSpatialValue());
					return sqlBuilder;
				case PrimitiveTypeKind.Geography:
					this.AppendSpatialConstant(sqlBuilder, ((DbGeography)e.Value).AsSpatialValue());
					return sqlBuilder;
				case PrimitiveTypeKind.HierarchyId:
					SqlGenerator.AppendHierarchyConstant(sqlBuilder, (HierarchyId)e.Value);
					return sqlBuilder;
				}
				throw new NotSupportedException(Strings.NoStoreTypeForEdmType(resultType.EdmType.Name, ((PrimitiveType)resultType.EdmType).PrimitiveTypeKind));
			}
			throw new NotSupportedException();
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00015FB0 File Offset: 0x000141B0
		private static void AppendHierarchyConstant(SqlBuilder result, HierarchyId hierarchyId)
		{
			result.Append("cast(");
			result.Append(SqlGenerator.EscapeSingleQuote(hierarchyId.ToString(), false));
			result.Append(" as hierarchyid)");
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00015FDC File Offset: 0x000141DC
		private void AppendSpatialConstant(SqlBuilder result, IDbSpatialValue spatialValue)
		{
			DbFunctionExpression dbFunctionExpression = null;
			int? coordinateSystemId = spatialValue.CoordinateSystemId;
			if (coordinateSystemId != null)
			{
				string wellKnownText = spatialValue.WellKnownText;
				if (wellKnownText != null)
				{
					dbFunctionExpression = (spatialValue.IsGeography ? SpatialEdmFunctions.GeographyFromText(wellKnownText, new int?(coordinateSystemId.Value)) : SpatialEdmFunctions.GeometryFromText(wellKnownText, new int?(coordinateSystemId.Value)));
				}
				else
				{
					byte[] wellKnownBinary = spatialValue.WellKnownBinary;
					if (wellKnownBinary != null)
					{
						dbFunctionExpression = (spatialValue.IsGeography ? SpatialEdmFunctions.GeographyFromBinary(wellKnownBinary, new int?(coordinateSystemId.Value)) : SpatialEdmFunctions.GeometryFromBinary(wellKnownBinary, new int?(coordinateSystemId.Value)));
					}
					else
					{
						string gmlString = spatialValue.GmlString;
						if (gmlString != null)
						{
							dbFunctionExpression = (spatialValue.IsGeography ? SpatialEdmFunctions.GeographyFromGml(gmlString, new int?(coordinateSystemId.Value)) : SpatialEdmFunctions.GeometryFromGml(gmlString, new int?(coordinateSystemId.Value)));
						}
					}
				}
			}
			if (dbFunctionExpression != null)
			{
				result.Append(SqlFunctionCallHandler.GenerateFunctionCallSql(this, dbFunctionExpression));
				return;
			}
			throw spatialValue.NotSqlCompatible();
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00016108 File Offset: 0x00014308
		private static void AssertValidDouble(double value)
		{
			if (double.IsNaN(value))
			{
				throw new NotSupportedException(Strings.SqlGen_TypedNaNNotSupported(Enum.GetName(typeof(PrimitiveTypeKind), PrimitiveTypeKind.Double)));
			}
			if (double.IsPositiveInfinity(value))
			{
				throw new NotSupportedException(Strings.SqlGen_TypedPositiveInfinityNotSupported(Enum.GetName(typeof(PrimitiveTypeKind), PrimitiveTypeKind.Double), typeof(double).Name));
			}
			if (double.IsNegativeInfinity(value))
			{
				throw new NotSupportedException(Strings.SqlGen_TypedNegativeInfinityNotSupported(Enum.GetName(typeof(PrimitiveTypeKind), PrimitiveTypeKind.Double), typeof(double).Name));
			}
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x000161AC File Offset: 0x000143AC
		private static void AssertValidSingle(float value)
		{
			if (float.IsNaN(value))
			{
				throw new NotSupportedException(Strings.SqlGen_TypedNaNNotSupported(Enum.GetName(typeof(PrimitiveTypeKind), PrimitiveTypeKind.Single)));
			}
			if (float.IsPositiveInfinity(value))
			{
				throw new NotSupportedException(Strings.SqlGen_TypedPositiveInfinityNotSupported(Enum.GetName(typeof(PrimitiveTypeKind), PrimitiveTypeKind.Single), typeof(float).Name));
			}
			if (float.IsNegativeInfinity(value))
			{
				throw new NotSupportedException(Strings.SqlGen_TypedNegativeInfinityNotSupported(Enum.GetName(typeof(PrimitiveTypeKind), PrimitiveTypeKind.Single), typeof(float).Name));
			}
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0001624F File Offset: 0x0001444F
		private static void WrapWithCastIfNeeded(bool cast, string value, string typeName, SqlBuilder result)
		{
			if (!cast)
			{
				result.Append(value);
				return;
			}
			result.Append("cast(");
			result.Append(value);
			result.Append(" as ");
			result.Append(typeName);
			result.Append(")");
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0001628B File Offset: 0x0001448B
		public override ISqlFragment Visit(DbConstantExpression e)
		{
			Check.NotNull<DbConstantExpression>(e, "e");
			return this.VisitConstant(e, false);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x000162A1 File Offset: 0x000144A1
		public override ISqlFragment Visit(DbDerefExpression e)
		{
			Check.NotNull<DbDerefExpression>(e, "e");
			throw new NotSupportedException();
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x000162B4 File Offset: 0x000144B4
		public override ISqlFragment Visit(DbDistinctExpression e)
		{
			Check.NotNull<DbDistinctExpression>(e, "e");
			SqlSelectStatement sqlSelectStatement = this.VisitExpressionEnsureSqlStatement(e.Argument);
			if (!SqlGenerator.IsCompatible(sqlSelectStatement, e.ExpressionKind))
			{
				TypeUsage elementTypeUsage = e.Argument.ResultType.GetElementTypeUsage();
				Symbol symbol;
				sqlSelectStatement = this.CreateNewSelectStatement(sqlSelectStatement, "distinct", elementTypeUsage, out symbol);
				this.AddFromSymbol(sqlSelectStatement, "distinct", symbol, false);
			}
			sqlSelectStatement.Select.IsDistinct = true;
			return sqlSelectStatement;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00016324 File Offset: 0x00014524
		public override ISqlFragment Visit(DbElementExpression e)
		{
			Check.NotNull<DbElementExpression>(e, "e");
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append("(");
			sqlBuilder.Append(this.VisitExpressionEnsureSqlStatement(e.Argument));
			sqlBuilder.Append(")");
			return sqlBuilder;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0001635F File Offset: 0x0001455F
		public override ISqlFragment Visit(DbExceptExpression e)
		{
			Check.NotNull<DbExceptExpression>(e, "e");
			return this.VisitSetOpExpression(e, "EXCEPT");
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00016379 File Offset: 0x00014579
		public override ISqlFragment Visit(DbExpression e)
		{
			Check.NotNull<DbExpression>(e, "e");
			throw new InvalidOperationException(string.Empty);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00016394 File Offset: 0x00014594
		public override ISqlFragment Visit(DbScanExpression e)
		{
			Check.NotNull<DbScanExpression>(e, "e");
			string targetTSql = SqlGenerator.GetTargetTSql(e.Target);
			if (this._targets != null)
			{
				this._targets.Add(targetTSql);
			}
			if (this.IsParentAJoin)
			{
				SqlBuilder sqlBuilder = new SqlBuilder();
				sqlBuilder.Append(targetTSql);
				return sqlBuilder;
			}
			SqlSelectStatement sqlSelectStatement = new SqlSelectStatement();
			sqlSelectStatement.From.Append(targetTSql);
			return sqlSelectStatement;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x000163F4 File Offset: 0x000145F4
		internal static string GetTargetTSql(EntitySetBase entitySetBase)
		{
			string metadataPropertyValue = entitySetBase.GetMetadataPropertyValue("DefiningQuery");
			if (metadataPropertyValue != null)
			{
				return "(" + metadataPropertyValue + ")";
			}
			StringBuilder stringBuilder = new StringBuilder(50);
			string metadataPropertyValue2 = entitySetBase.GetMetadataPropertyValue("Schema");
			if (!string.IsNullOrEmpty(metadataPropertyValue2))
			{
				stringBuilder.Append(SqlGenerator.QuoteIdentifier(metadataPropertyValue2));
				stringBuilder.Append(".");
			}
			else
			{
				stringBuilder.Append(SqlGenerator.QuoteIdentifier(entitySetBase.EntityContainer.Name));
				stringBuilder.Append(".");
			}
			string metadataPropertyValue3 = entitySetBase.GetMetadataPropertyValue("Table");
			stringBuilder.Append(string.IsNullOrEmpty(metadataPropertyValue3) ? SqlGenerator.QuoteIdentifier(entitySetBase.Name) : SqlGenerator.QuoteIdentifier(metadataPropertyValue3));
			return stringBuilder.ToString();
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x000164AF File Offset: 0x000146AF
		public override ISqlFragment Visit(DbFilterExpression e)
		{
			Check.NotNull<DbFilterExpression>(e, "e");
			return this.VisitFilterExpression(e.Input, e.Predicate, false);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x000164D0 File Offset: 0x000146D0
		public override ISqlFragment Visit(DbFunctionExpression e)
		{
			Check.NotNull<DbFunctionExpression>(e, "e");
			return SqlFunctionCallHandler.GenerateFunctionCallSql(this, e);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x000164E5 File Offset: 0x000146E5
		public override ISqlFragment Visit(DbLambdaExpression expression)
		{
			Check.NotNull<DbLambdaExpression>(expression, "expression");
			throw new NotSupportedException();
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x000164F8 File Offset: 0x000146F8
		public override ISqlFragment Visit(DbEntityRefExpression e)
		{
			Check.NotNull<DbEntityRefExpression>(e, "e");
			throw new NotSupportedException();
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0001650B File Offset: 0x0001470B
		public override ISqlFragment Visit(DbRefKeyExpression e)
		{
			Check.NotNull<DbRefKeyExpression>(e, "e");
			throw new NotSupportedException();
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00016520 File Offset: 0x00014720
		public override ISqlFragment Visit(DbGroupByExpression e)
		{
			Check.NotNull<DbGroupByExpression>(e, "e");
			Symbol symbol;
			SqlSelectStatement sqlSelectStatement = this.VisitInputExpression(e.Input.Expression, e.Input.VariableName, e.Input.VariableType, out symbol);
			if (!SqlGenerator.IsCompatible(sqlSelectStatement, e.ExpressionKind))
			{
				sqlSelectStatement = this.CreateNewSelectStatement(sqlSelectStatement, e.Input.VariableName, e.Input.VariableType, out symbol);
			}
			this.selectStatementStack.Push(sqlSelectStatement);
			this.symbolTable.EnterScope();
			this.AddFromSymbol(sqlSelectStatement, e.Input.VariableName, symbol);
			this.symbolTable.Add(e.Input.GroupVariableName, symbol);
			RowType rowType = (RowType)((CollectionType)e.ResultType.EdmType).TypeUsage.EdmType;
			bool flag = SqlGenerator.GroupByAggregatesNeedInnerQuery(e.Aggregates, e.Input.GroupVariableName) || SqlGenerator.GroupByKeysNeedInnerQuery(e.Keys, e.Input.VariableName);
			SqlSelectStatement sqlSelectStatement2;
			if (flag)
			{
				sqlSelectStatement2 = this.CreateNewSelectStatement(sqlSelectStatement, e.Input.VariableName, e.Input.VariableType, false, out symbol);
				this.AddFromSymbol(sqlSelectStatement2, e.Input.VariableName, symbol, false);
			}
			else
			{
				sqlSelectStatement2 = sqlSelectStatement;
			}
			using (IEnumerator<EdmProperty> enumerator = rowType.Properties.GetEnumerator())
			{
				enumerator.MoveNext();
				string text = "";
				foreach (DbExpression dbExpression in e.Keys)
				{
					string text2 = SqlGenerator.QuoteIdentifier(enumerator.Current.Name);
					sqlSelectStatement2.GroupBy.Append(text);
					ISqlFragment sqlFragment = dbExpression.Accept<ISqlFragment>(this);
					if (!flag)
					{
						sqlSelectStatement2.Select.Append(text);
						sqlSelectStatement2.Select.AppendLine();
						sqlSelectStatement2.Select.Append(sqlFragment);
						sqlSelectStatement2.Select.Append(" AS ");
						sqlSelectStatement2.Select.Append(text2);
						sqlSelectStatement2.GroupBy.Append(sqlFragment);
					}
					else
					{
						sqlSelectStatement.Select.Append(text);
						sqlSelectStatement.Select.AppendLine();
						sqlSelectStatement.Select.Append(sqlFragment);
						sqlSelectStatement.Select.Append(" AS ");
						sqlSelectStatement.Select.Append(text2);
						sqlSelectStatement2.Select.Append(text);
						sqlSelectStatement2.Select.AppendLine();
						sqlSelectStatement2.Select.Append(symbol);
						sqlSelectStatement2.Select.Append(".");
						sqlSelectStatement2.Select.Append(text2);
						sqlSelectStatement2.Select.Append(" AS ");
						sqlSelectStatement2.Select.Append(text2);
						sqlSelectStatement2.GroupBy.Append(text2);
					}
					text = ", ";
					enumerator.MoveNext();
				}
				foreach (DbAggregate dbAggregate in e.Aggregates)
				{
					EdmProperty edmProperty = enumerator.Current;
					string text3 = SqlGenerator.QuoteIdentifier(edmProperty.Name);
					List<object> list = new List<object>();
					for (int i = 0; i < dbAggregate.Arguments.Count; i++)
					{
						ISqlFragment sqlFragment2 = dbAggregate.Arguments[i].Accept<ISqlFragment>(this);
						object obj;
						if (flag)
						{
							string text4 = SqlGenerator.QuoteIdentifier(edmProperty.Name + "_" + i.ToString());
							SqlBuilder sqlBuilder = new SqlBuilder();
							sqlBuilder.Append(symbol);
							sqlBuilder.Append(".");
							sqlBuilder.Append(text4);
							obj = sqlBuilder;
							sqlSelectStatement.Select.Append(text);
							sqlSelectStatement.Select.AppendLine();
							sqlSelectStatement.Select.Append(sqlFragment2);
							sqlSelectStatement.Select.Append(" AS ");
							sqlSelectStatement.Select.Append(text4);
						}
						else
						{
							obj = sqlFragment2;
						}
						list.Add(obj);
					}
					ISqlFragment sqlFragment3 = SqlGenerator.VisitAggregate(dbAggregate, list);
					sqlSelectStatement2.Select.Append(text);
					sqlSelectStatement2.Select.AppendLine();
					sqlSelectStatement2.Select.Append(sqlFragment3);
					sqlSelectStatement2.Select.Append(" AS ");
					sqlSelectStatement2.Select.Append(text3);
					text = ", ";
					enumerator.MoveNext();
				}
			}
			this.symbolTable.ExitScope();
			this.selectStatementStack.Pop();
			return sqlSelectStatement2;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x000169DC File Offset: 0x00014BDC
		public override ISqlFragment Visit(DbIntersectExpression e)
		{
			Check.NotNull<DbIntersectExpression>(e, "e");
			return this.VisitSetOpExpression(e, "INTERSECT");
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x000169F6 File Offset: 0x00014BF6
		public override ISqlFragment Visit(DbIsEmptyExpression e)
		{
			Check.NotNull<DbIsEmptyExpression>(e, "e");
			return this.VisitIsEmptyExpression(e, false);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00016A0C File Offset: 0x00014C0C
		public override ISqlFragment Visit(DbIsNullExpression e)
		{
			Check.NotNull<DbIsNullExpression>(e, "e");
			return this.VisitIsNullExpression(e, false);
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00016A22 File Offset: 0x00014C22
		public override ISqlFragment Visit(DbIsOfExpression e)
		{
			Check.NotNull<DbIsOfExpression>(e, "e");
			throw new NotSupportedException();
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00016A35 File Offset: 0x00014C35
		public override ISqlFragment Visit(DbCrossJoinExpression e)
		{
			Check.NotNull<DbCrossJoinExpression>(e, "e");
			return this.VisitJoinExpression(e.Inputs, e.ExpressionKind, "CROSS JOIN", null);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00016A5C File Offset: 0x00014C5C
		public override ISqlFragment Visit(DbJoinExpression e)
		{
			Check.NotNull<DbJoinExpression>(e, "e");
			DbExpressionKind expressionKind = e.ExpressionKind;
			string text;
			if (expressionKind != DbExpressionKind.FullOuterJoin)
			{
				if (expressionKind != DbExpressionKind.InnerJoin)
				{
					if (expressionKind != DbExpressionKind.LeftOuterJoin)
					{
						text = null;
					}
					else
					{
						text = "LEFT OUTER JOIN";
					}
				}
				else
				{
					text = "INNER JOIN";
				}
			}
			else
			{
				text = "FULL OUTER JOIN";
			}
			return this.VisitJoinExpression(new List<DbExpressionBinding>(2) { e.Left, e.Right }, e.ExpressionKind, text, e.JoinCondition);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00016ADC File Offset: 0x00014CDC
		public override ISqlFragment Visit(DbLikeExpression e)
		{
			Check.NotNull<DbLikeExpression>(e, "e");
			this._forceNonUnicode = this.CheckIfForceNonUnicodeRequired(e);
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append(e.Argument.Accept<ISqlFragment>(this));
			sqlBuilder.Append(" LIKE ");
			sqlBuilder.Append(e.Pattern.Accept<ISqlFragment>(this));
			if (e.Escape.ExpressionKind != DbExpressionKind.Null)
			{
				sqlBuilder.Append(" ESCAPE ");
				sqlBuilder.Append(e.Escape.Accept<ISqlFragment>(this));
			}
			this._forceNonUnicode = false;
			return sqlBuilder;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00016B6C File Offset: 0x00014D6C
		public override ISqlFragment Visit(DbLimitExpression e)
		{
			Check.NotNull<DbLimitExpression>(e, "e");
			SqlSelectStatement sqlSelectStatement = this.VisitExpressionEnsureSqlStatement(e.Argument, false, false);
			if (!SqlGenerator.IsCompatible(sqlSelectStatement, e.ExpressionKind))
			{
				TypeUsage elementTypeUsage = e.Argument.ResultType.GetElementTypeUsage();
				Symbol symbol;
				sqlSelectStatement = this.CreateNewSelectStatement(sqlSelectStatement, "top", elementTypeUsage, out symbol);
				this.AddFromSymbol(sqlSelectStatement, "top", symbol, false);
			}
			ISqlFragment sqlFragment = this.HandleCountExpression(e.Limit);
			sqlSelectStatement.Select.Top = new TopClause(sqlFragment, e.WithTies);
			return sqlSelectStatement;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00016BF6 File Offset: 0x00014DF6
		public override ISqlFragment Visit(DbNewInstanceExpression e)
		{
			Check.NotNull<DbNewInstanceExpression>(e, "e");
			if (BuiltInTypeKind.CollectionType == e.ResultType.EdmType.BuiltInTypeKind)
			{
				return this.VisitCollectionConstructor(e);
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00016C24 File Offset: 0x00014E24
		public override ISqlFragment Visit(DbNotExpression e)
		{
			Check.NotNull<DbNotExpression>(e, "e");
			DbNotExpression dbNotExpression = e.Argument as DbNotExpression;
			if (dbNotExpression != null)
			{
				return dbNotExpression.Argument.Accept<ISqlFragment>(this);
			}
			DbIsEmptyExpression dbIsEmptyExpression = e.Argument as DbIsEmptyExpression;
			if (dbIsEmptyExpression != null)
			{
				return this.VisitIsEmptyExpression(dbIsEmptyExpression, true);
			}
			DbIsNullExpression dbIsNullExpression = e.Argument as DbIsNullExpression;
			if (dbIsNullExpression != null)
			{
				return this.VisitIsNullExpression(dbIsNullExpression, true);
			}
			DbComparisonExpression dbComparisonExpression = e.Argument as DbComparisonExpression;
			if (dbComparisonExpression != null && dbComparisonExpression.ExpressionKind == DbExpressionKind.Equals)
			{
				bool forceNonUnicode = this._forceNonUnicode;
				if (dbComparisonExpression.Left.ResultType.IsPrimitiveType(PrimitiveTypeKind.String))
				{
					this._forceNonUnicode = this.CheckIfForceNonUnicodeRequired(dbComparisonExpression);
				}
				ISqlFragment sqlFragment = this.VisitComparisonExpression(" <> ", dbComparisonExpression.Left, dbComparisonExpression.Right);
				this._forceNonUnicode = forceNonUnicode;
				return sqlFragment;
			}
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append(" NOT (");
			sqlBuilder.Append(e.Argument.Accept<ISqlFragment>(this));
			sqlBuilder.Append(")");
			return sqlBuilder;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00016D18 File Offset: 0x00014F18
		public override ISqlFragment Visit(DbNullExpression e)
		{
			Check.NotNull<DbNullExpression>(e, "e");
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append("CAST(NULL AS ");
			TypeUsage resultType = e.ResultType;
			PrimitiveTypeKind primitiveTypeKind = (resultType.EdmType as PrimitiveType).PrimitiveTypeKind;
			if (primitiveTypeKind != PrimitiveTypeKind.Binary)
			{
				if (primitiveTypeKind == PrimitiveTypeKind.String)
				{
					sqlBuilder.Append("varchar(1)");
				}
				else
				{
					sqlBuilder.Append(this.GetSqlPrimitiveType(resultType));
				}
			}
			else
			{
				sqlBuilder.Append("varbinary(1)");
			}
			sqlBuilder.Append(")");
			return sqlBuilder;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00016D95 File Offset: 0x00014F95
		public override ISqlFragment Visit(DbOfTypeExpression e)
		{
			Check.NotNull<DbOfTypeExpression>(e, "e");
			throw new NotSupportedException();
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00016DA8 File Offset: 0x00014FA8
		public override ISqlFragment Visit(DbOrExpression e)
		{
			Check.NotNull<DbOrExpression>(e, "e");
			ISqlFragment sqlFragment = null;
			if (this.TryTranslateIntoIn(e, out sqlFragment))
			{
				return sqlFragment;
			}
			return this.VisitBinaryExpression(" OR ", e.ExpressionKind, e.Left, e.Right);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00016DF0 File Offset: 0x00014FF0
		public override ISqlFragment Visit(DbInExpression e)
		{
			Check.NotNull<DbInExpression>(e, "e");
			if (e.List.Count == 0)
			{
				return this.Visit(DbExpressionBuilder.False);
			}
			SqlBuilder sqlBuilder = new SqlBuilder();
			if (e.Item.ResultType.IsPrimitiveType(PrimitiveTypeKind.String))
			{
				this._forceNonUnicode = this.CheckIfForceNonUnicodeRequired(e);
			}
			sqlBuilder.Append(e.Item.Accept<ISqlFragment>(this));
			sqlBuilder.Append(" IN (");
			bool flag = true;
			foreach (DbExpression dbExpression in e.List)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					sqlBuilder.Append(", ");
				}
				sqlBuilder.Append(dbExpression.Accept<ISqlFragment>(this));
			}
			sqlBuilder.Append(")");
			this._forceNonUnicode = false;
			return sqlBuilder;
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00016ED4 File Offset: 0x000150D4
		internal static IDictionary<DbExpression, IList<DbExpression>> HasBuiltMapForIn(DbOrExpression expression)
		{
			Dictionary<DbExpression, IList<DbExpression>> dictionary = new Dictionary<DbExpression, IList<DbExpression>>(new SqlGenerator.KeyFieldExpressionComparer());
			if (!SqlGenerator.HasBuiltMapForIn(expression, dictionary))
			{
				return null;
			}
			return dictionary;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00016EF8 File Offset: 0x000150F8
		private bool TryTranslateIntoIn(DbOrExpression e, out ISqlFragment sqlFragment)
		{
			IDictionary<DbExpression, IList<DbExpression>> dictionary = SqlGenerator.HasBuiltMapForIn(e);
			if (dictionary == null)
			{
				sqlFragment = null;
				return false;
			}
			SqlBuilder sqlBuilder = new SqlBuilder();
			bool flag = true;
			foreach (DbExpression dbExpression in dictionary.Keys)
			{
				IList<DbExpression> list = dictionary[dbExpression];
				if (!flag)
				{
					sqlBuilder.Append(" OR ");
				}
				else
				{
					flag = false;
				}
				IEnumerable<DbExpression> enumerable = list.Where((DbExpression v) => v.ExpressionKind != DbExpressionKind.IsNull);
				int num = enumerable.Count<DbExpression>();
				bool flag2 = false;
				bool flag3 = false;
				if (dbExpression.ResultType.IsPrimitiveType(PrimitiveTypeKind.String))
				{
					flag2 = SqlGenerator.MatchSourcePatternForForcingNonUnicode(dbExpression);
					flag3 = !flag2 && this.MatchTargetPatternForForcingNonUnicode(dbExpression) && enumerable.All(new Func<DbExpression, bool>(SqlGenerator.MatchSourcePatternForForcingNonUnicode));
				}
				if (num == 1)
				{
					this.HandleInKey(sqlBuilder, dbExpression, flag3);
					sqlBuilder.Append(" = ");
					DbExpression dbExpression2 = enumerable.First<DbExpression>();
					this.HandleInValue(sqlBuilder, dbExpression2, dbExpression.ResultType.EdmType == dbExpression2.ResultType.EdmType, flag2);
				}
				if (num > 1)
				{
					this.HandleInKey(sqlBuilder, dbExpression, flag3);
					sqlBuilder.Append(" IN (");
					bool flag4 = true;
					foreach (DbExpression dbExpression3 in enumerable)
					{
						if (!flag4)
						{
							sqlBuilder.Append(",");
						}
						else
						{
							flag4 = false;
						}
						this.HandleInValue(sqlBuilder, dbExpression3, dbExpression.ResultType.EdmType == dbExpression3.ResultType.EdmType, flag2);
					}
					sqlBuilder.Append(")");
				}
				DbIsNullExpression dbIsNullExpression = list.FirstOrDefault((DbExpression v) => v.ExpressionKind == DbExpressionKind.IsNull) as DbIsNullExpression;
				if (dbIsNullExpression != null)
				{
					if (num > 0)
					{
						sqlBuilder.Append(" OR ");
					}
					sqlBuilder.Append(this.VisitIsNullExpression(dbIsNullExpression, false));
				}
			}
			sqlFragment = sqlBuilder;
			return true;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00017140 File Offset: 0x00015340
		private void HandleInValue(SqlBuilder sqlBuilder, DbExpression value, bool isSameEdmType, bool forceNonUnicodeOnQualifyingValues)
		{
			this.ForcingNonUnicode(delegate
			{
				this.ParenthesizeExpressionWithoutRedundantConstantCasts(value, sqlBuilder, isSameEdmType);
			}, forceNonUnicodeOnQualifyingValues && this.MatchTargetPatternForForcingNonUnicode(value));
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00017194 File Offset: 0x00015394
		private void HandleInKey(SqlBuilder sqlBuilder, DbExpression key, bool forceNonUnicodeOnKey)
		{
			this.ForcingNonUnicode(delegate
			{
				this.ParenthesizeExpressionIfNeeded(key, sqlBuilder);
			}, forceNonUnicodeOnKey);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x000171D0 File Offset: 0x000153D0
		private void ForcingNonUnicode(Action action, bool forceNonUnicode)
		{
			bool flag = false;
			if (forceNonUnicode && !this._forceNonUnicode)
			{
				this._forceNonUnicode = true;
				flag = true;
			}
			action();
			if (flag)
			{
				this._forceNonUnicode = false;
			}
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00017203 File Offset: 0x00015403
		private void ParenthesizeExpressionWithoutRedundantConstantCasts(DbExpression value, SqlBuilder sqlBuilder, bool isSameEdmType)
		{
			if (value.ExpressionKind == DbExpressionKind.Constant)
			{
				sqlBuilder.Append(this.VisitConstant((DbConstantExpression)value, isSameEdmType));
				return;
			}
			this.ParenthesizeExpressionIfNeeded(value, sqlBuilder);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0001722A File Offset: 0x0001542A
		internal static bool IsKeyForIn(DbExpression e)
		{
			return e.ExpressionKind == DbExpressionKind.Property || e.ExpressionKind == DbExpressionKind.VariableReference || e.ExpressionKind == DbExpressionKind.ParameterReference;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0001724C File Offset: 0x0001544C
		internal static bool TryAddExpressionForIn(DbBinaryExpression e, IDictionary<DbExpression, IList<DbExpression>> values)
		{
			if (SqlGenerator.IsKeyForIn(e.Left))
			{
				values.Add(e.Left, e.Right);
				return true;
			}
			if (SqlGenerator.IsKeyForIn(e.Right))
			{
				values.Add(e.Right, e.Left);
				return true;
			}
			return false;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0001729C File Offset: 0x0001549C
		internal static bool HasBuiltMapForIn(DbExpression e, IDictionary<DbExpression, IList<DbExpression>> values)
		{
			DbExpressionKind expressionKind = e.ExpressionKind;
			if (expressionKind == DbExpressionKind.Equals)
			{
				return SqlGenerator.TryAddExpressionForIn((DbBinaryExpression)e, values);
			}
			if (expressionKind != DbExpressionKind.IsNull)
			{
				if (expressionKind != DbExpressionKind.Or)
				{
					return false;
				}
				DbBinaryExpression dbBinaryExpression = (DbBinaryExpression)e;
				return SqlGenerator.HasBuiltMapForIn(dbBinaryExpression.Left, values) && SqlGenerator.HasBuiltMapForIn(dbBinaryExpression.Right, values);
			}
			else
			{
				DbExpression argument = ((DbIsNullExpression)e).Argument;
				if (SqlGenerator.IsKeyForIn(argument))
				{
					values.Add(argument, e);
					return true;
				}
				return false;
			}
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00017314 File Offset: 0x00015514
		public override ISqlFragment Visit(DbParameterReferenceExpression e)
		{
			Check.NotNull<DbParameterReferenceExpression>(e, "e");
			if (!this._ignoreForceNonUnicodeFlag)
			{
				if (!this._forceNonUnicode)
				{
					this._candidateParametersToForceNonUnicode[e.ParameterName] = false;
				}
				else if (!this._candidateParametersToForceNonUnicode.ContainsKey(e.ParameterName))
				{
					this._candidateParametersToForceNonUnicode[e.ParameterName] = true;
				}
			}
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append("@" + e.ParameterName);
			return sqlBuilder;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00017394 File Offset: 0x00015594
		public override ISqlFragment Visit(DbProjectExpression e)
		{
			Check.NotNull<DbProjectExpression>(e, "e");
			Symbol symbol;
			SqlSelectStatement sqlSelectStatement = this.VisitInputExpression(e.Input.Expression, e.Input.VariableName, e.Input.VariableType, out symbol);
			bool flag = false;
			if (!SqlGenerator.IsCompatible(sqlSelectStatement, e.ExpressionKind))
			{
				sqlSelectStatement = this.CreateNewSelectStatement(sqlSelectStatement, e.Input.VariableName, e.Input.VariableType, out symbol);
			}
			else if (this.SqlVersion == SqlVersion.Sql8 && !sqlSelectStatement.OrderBy.IsEmpty)
			{
				flag = true;
			}
			this.selectStatementStack.Push(sqlSelectStatement);
			this.symbolTable.EnterScope();
			this.AddFromSymbol(sqlSelectStatement, e.Input.VariableName, symbol);
			DbNewInstanceExpression dbNewInstanceExpression = e.Projection as DbNewInstanceExpression;
			if (dbNewInstanceExpression != null)
			{
				Dictionary<string, Symbol> dictionary;
				sqlSelectStatement.Select.Append(this.VisitNewInstanceExpression(dbNewInstanceExpression, flag, out dictionary));
				if (flag)
				{
					sqlSelectStatement.OutputColumnsRenamed = true;
				}
				sqlSelectStatement.OutputColumns = dictionary;
			}
			else
			{
				sqlSelectStatement.Select.Append(e.Projection.Accept<ISqlFragment>(this));
			}
			this.symbolTable.ExitScope();
			this.selectStatementStack.Pop();
			return sqlSelectStatement;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x000174B4 File Offset: 0x000156B4
		public override ISqlFragment Visit(DbPropertyExpression e)
		{
			Check.NotNull<DbPropertyExpression>(e, "e");
			ISqlFragment sqlFragment = e.Instance.Accept<ISqlFragment>(this);
			if (e.Instance is DbVariableReferenceExpression)
			{
				this.isVarRefSingle = false;
			}
			JoinSymbol joinSymbol = sqlFragment as JoinSymbol;
			if (joinSymbol == null)
			{
				SymbolPair symbolPair = sqlFragment as SymbolPair;
				SqlBuilder sqlBuilder;
				if (symbolPair != null)
				{
					JoinSymbol joinSymbol2 = symbolPair.Column as JoinSymbol;
					if (joinSymbol2 != null)
					{
						symbolPair.Column = joinSymbol2.NameToExtent[e.Property.Name];
						return symbolPair;
					}
					if (symbolPair.Column.Columns.ContainsKey(e.Property.Name))
					{
						sqlBuilder = new SqlBuilder();
						sqlBuilder.Append(symbolPair.Source);
						sqlBuilder.Append(".");
						Symbol symbol = symbolPair.Column.Columns[e.Property.Name];
						this.optionalColumnUsageManager.MarkAsUsed(symbol);
						sqlBuilder.Append(symbol);
						return sqlBuilder;
					}
				}
				sqlBuilder = new SqlBuilder();
				sqlBuilder.Append(sqlFragment);
				sqlBuilder.Append(".");
				Symbol symbol2 = sqlFragment as Symbol;
				Symbol symbol3;
				if (symbol2 != null && symbol2.OutputColumns.TryGetValue(e.Property.Name, out symbol3))
				{
					this.optionalColumnUsageManager.MarkAsUsed(symbol3);
					if (symbol2.OutputColumnsRenamed)
					{
						sqlBuilder.Append(symbol3);
					}
					else
					{
						sqlBuilder.Append(SqlGenerator.QuoteIdentifier(e.Property.Name));
					}
				}
				else
				{
					sqlBuilder.Append(SqlGenerator.QuoteIdentifier(e.Property.Name));
				}
				return sqlBuilder;
			}
			if (joinSymbol.IsNestedJoin)
			{
				return new SymbolPair(joinSymbol, joinSymbol.NameToExtent[e.Property.Name]);
			}
			return joinSymbol.NameToExtent[e.Property.Name];
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00017670 File Offset: 0x00015870
		public override ISqlFragment Visit(DbQuantifierExpression e)
		{
			Check.NotNull<DbQuantifierExpression>(e, "e");
			SqlBuilder sqlBuilder = new SqlBuilder();
			bool flag = e.ExpressionKind == DbExpressionKind.All;
			if (e.ExpressionKind == DbExpressionKind.Any)
			{
				sqlBuilder.Append("EXISTS (");
			}
			else
			{
				sqlBuilder.Append("NOT EXISTS (");
			}
			SqlSelectStatement sqlSelectStatement = this.VisitFilterExpression(e.Input, e.Predicate, flag);
			if (sqlSelectStatement.Select.IsEmpty)
			{
				this.AddDefaultColumns(sqlSelectStatement);
			}
			sqlBuilder.Append(sqlSelectStatement);
			sqlBuilder.Append(")");
			return sqlBuilder;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x000176F6 File Offset: 0x000158F6
		public override ISqlFragment Visit(DbRefExpression e)
		{
			Check.NotNull<DbRefExpression>(e, "e");
			throw new NotSupportedException();
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00017709 File Offset: 0x00015909
		public override ISqlFragment Visit(DbRelationshipNavigationExpression e)
		{
			Check.NotNull<DbRelationshipNavigationExpression>(e, "e");
			throw new NotSupportedException();
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0001771C File Offset: 0x0001591C
		public override ISqlFragment Visit(DbSkipExpression e)
		{
			Check.NotNull<DbSkipExpression>(e, "e");
			Symbol symbol;
			SqlSelectStatement sqlSelectStatement = this.VisitInputExpression(e.Input.Expression, e.Input.VariableName, e.Input.VariableType, out symbol);
			if (!SqlGenerator.IsCompatible(sqlSelectStatement, e.ExpressionKind))
			{
				sqlSelectStatement = this.CreateNewSelectStatement(sqlSelectStatement, e.Input.VariableName, e.Input.VariableType, out symbol);
			}
			this.selectStatementStack.Push(sqlSelectStatement);
			this.symbolTable.EnterScope();
			this.AddFromSymbol(sqlSelectStatement, e.Input.VariableName, symbol);
			if (this.SqlVersion >= SqlVersion.Sql11)
			{
				sqlSelectStatement.Select.Skip = new SkipClause(this.HandleCountExpression(e.Count));
				if (SqlProviderServices.UseRowNumberOrderingInOffsetQueries)
				{
					sqlSelectStatement.OrderBy.Append("row_number() OVER (ORDER BY ");
					this.AddSortKeys(sqlSelectStatement.OrderBy, e.SortOrder);
					sqlSelectStatement.OrderBy.Append(")");
				}
				else
				{
					this.AddSortKeys(sqlSelectStatement.OrderBy, e.SortOrder);
				}
				this.symbolTable.ExitScope();
				this.selectStatementStack.Pop();
				return sqlSelectStatement;
			}
			List<Symbol> list = this.AddDefaultColumns(sqlSelectStatement);
			sqlSelectStatement.Select.Append("row_number() OVER (ORDER BY ");
			this.AddSortKeys(sqlSelectStatement.Select, e.SortOrder);
			sqlSelectStatement.Select.Append(") AS ");
			string row_numberName = "row_number";
			Symbol symbol2 = new Symbol(row_numberName, this.IntegerType);
			if (list.Any((Symbol c) => string.Equals(c.Name, row_numberName, StringComparison.OrdinalIgnoreCase)))
			{
				symbol2.NeedsRenaming = true;
			}
			sqlSelectStatement.Select.Append(symbol2);
			this.symbolTable.ExitScope();
			this.selectStatementStack.Pop();
			SqlSelectStatement sqlSelectStatement2 = new SqlSelectStatement();
			sqlSelectStatement2.From.Append("( ");
			sqlSelectStatement2.From.Append(sqlSelectStatement);
			sqlSelectStatement2.From.AppendLine();
			sqlSelectStatement2.From.Append(") ");
			Symbol symbol3 = null;
			if (sqlSelectStatement.FromExtents.Count == 1)
			{
				JoinSymbol joinSymbol = sqlSelectStatement.FromExtents[0] as JoinSymbol;
				if (joinSymbol != null)
				{
					symbol3 = new JoinSymbol(e.Input.VariableName, e.Input.VariableType, joinSymbol.ExtentList)
					{
						IsNestedJoin = true,
						ColumnList = list,
						FlattenedExtentList = joinSymbol.FlattenedExtentList
					};
				}
			}
			if (symbol3 == null)
			{
				symbol3 = new Symbol(e.Input.VariableName, e.Input.VariableType, sqlSelectStatement.OutputColumns, false);
			}
			this.selectStatementStack.Push(sqlSelectStatement2);
			this.symbolTable.EnterScope();
			this.AddFromSymbol(sqlSelectStatement2, e.Input.VariableName, symbol3);
			sqlSelectStatement2.Where.Append(symbol3);
			sqlSelectStatement2.Where.Append(".");
			sqlSelectStatement2.Where.Append(symbol2);
			sqlSelectStatement2.Where.Append(" > ");
			sqlSelectStatement2.Where.Append(this.HandleCountExpression(e.Count));
			this.AddSortKeys(sqlSelectStatement2.OrderBy, e.SortOrder);
			this.symbolTable.ExitScope();
			this.selectStatementStack.Pop();
			return sqlSelectStatement2;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00017A64 File Offset: 0x00015C64
		public override ISqlFragment Visit(DbSortExpression e)
		{
			Check.NotNull<DbSortExpression>(e, "e");
			Symbol symbol;
			SqlSelectStatement sqlSelectStatement = this.VisitInputExpression(e.Input.Expression, e.Input.VariableName, e.Input.VariableType, out symbol);
			if (!SqlGenerator.IsCompatible(sqlSelectStatement, e.ExpressionKind))
			{
				sqlSelectStatement = this.CreateNewSelectStatement(sqlSelectStatement, e.Input.VariableName, e.Input.VariableType, out symbol);
			}
			this.selectStatementStack.Push(sqlSelectStatement);
			this.symbolTable.EnterScope();
			this.AddFromSymbol(sqlSelectStatement, e.Input.VariableName, symbol);
			this.AddSortKeys(sqlSelectStatement.OrderBy, e.SortOrder);
			this.symbolTable.ExitScope();
			this.selectStatementStack.Pop();
			return sqlSelectStatement;
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00017B29 File Offset: 0x00015D29
		public override ISqlFragment Visit(DbTreatExpression e)
		{
			Check.NotNull<DbTreatExpression>(e, "e");
			throw new NotSupportedException();
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00017B3C File Offset: 0x00015D3C
		public override ISqlFragment Visit(DbUnionAllExpression e)
		{
			Check.NotNull<DbUnionAllExpression>(e, "e");
			return this.VisitSetOpExpression(e, "UNION ALL");
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00017B58 File Offset: 0x00015D58
		public override ISqlFragment Visit(DbVariableReferenceExpression e)
		{
			Check.NotNull<DbVariableReferenceExpression>(e, "e");
			if (this.isVarRefSingle)
			{
				throw new NotSupportedException();
			}
			this.isVarRefSingle = true;
			Symbol symbol = this.symbolTable.Lookup(e.VariableName);
			this.optionalColumnUsageManager.MarkAsUsed(symbol);
			if (!this.CurrentSelectStatement.FromExtents.Contains(symbol))
			{
				this.CurrentSelectStatement.OuterExtents[symbol] = true;
			}
			return symbol;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00017BCC File Offset: 0x00015DCC
		private static SqlBuilder VisitAggregate(DbAggregate aggregate, IList<object> aggregateArguments)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			DbFunctionAggregate dbFunctionAggregate = aggregate as DbFunctionAggregate;
			if (dbFunctionAggregate == null)
			{
				throw new NotSupportedException();
			}
			if (dbFunctionAggregate.Function.IsCanonicalFunction() && string.Equals(dbFunctionAggregate.Function.Name, "BigCount", StringComparison.Ordinal))
			{
				sqlBuilder.Append("COUNT_BIG");
			}
			else
			{
				SqlFunctionCallHandler.WriteFunctionName(sqlBuilder, dbFunctionAggregate.Function);
			}
			sqlBuilder.Append("(");
			DbFunctionAggregate dbFunctionAggregate2 = dbFunctionAggregate;
			if (dbFunctionAggregate2 != null && dbFunctionAggregate2.Distinct)
			{
				sqlBuilder.Append("DISTINCT ");
			}
			string text = string.Empty;
			foreach (object obj in aggregateArguments)
			{
				sqlBuilder.Append(text);
				sqlBuilder.Append(obj);
				text = ", ";
			}
			sqlBuilder.Append(")");
			return sqlBuilder;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00017CB4 File Offset: 0x00015EB4
		internal void ParenthesizeExpressionIfNeeded(DbExpression e, SqlBuilder result)
		{
			if (SqlGenerator.IsComplexExpression(e))
			{
				result.Append("(");
				result.Append(e.Accept<ISqlFragment>(this));
				result.Append(")");
				return;
			}
			result.Append(e.Accept<ISqlFragment>(this));
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00017CF0 File Offset: 0x00015EF0
		private SqlBuilder VisitBinaryExpression(string op, DbExpressionKind expressionKind, DbExpression left, DbExpression right)
		{
			SqlGenerator.RemoveUnnecessaryCasts(ref left, ref right);
			SqlBuilder sqlBuilder = new SqlBuilder();
			bool flag = true;
			foreach (DbExpression dbExpression in SqlGenerator.FlattenAssociativeExpression(expressionKind, left, right))
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					sqlBuilder.Append(op);
				}
				this.ParenthesizeExpressionIfNeeded(dbExpression, sqlBuilder);
			}
			return sqlBuilder;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00017D64 File Offset: 0x00015F64
		private static IEnumerable<DbExpression> FlattenAssociativeExpression(DbExpressionKind kind, DbExpression left, DbExpression right)
		{
			if (kind != DbExpressionKind.Or && kind != DbExpressionKind.And && kind != DbExpressionKind.Plus && kind != DbExpressionKind.Multiply)
			{
				return new DbExpression[] { left, right };
			}
			List<DbExpression> list = new List<DbExpression>();
			SqlGenerator.ExtractAssociativeArguments(kind, list, left);
			SqlGenerator.ExtractAssociativeArguments(kind, list, right);
			return list;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00017DAC File Offset: 0x00015FAC
		private static void ExtractAssociativeArguments(DbExpressionKind expressionKind, List<DbExpression> argumentList, DbExpression expression)
		{
			IEnumerable<DbExpression> leafNodes = expression.GetLeafNodes(expressionKind, delegate(DbExpression exp)
			{
				DbBinaryExpression dbBinaryExpression = exp as DbBinaryExpression;
				if (dbBinaryExpression != null)
				{
					return new DbExpression[] { dbBinaryExpression.Left, dbBinaryExpression.Right };
				}
				return ((DbArithmeticExpression)exp).Arguments;
			});
			argumentList.AddRange(leafNodes);
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00017DE8 File Offset: 0x00015FE8
		private SqlBuilder VisitComparisonExpression(string op, DbExpression left, DbExpression right)
		{
			SqlGenerator.RemoveUnnecessaryCasts(ref left, ref right);
			SqlBuilder sqlBuilder = new SqlBuilder();
			bool flag = left.ResultType.EdmType == right.ResultType.EdmType;
			if (left.ExpressionKind == DbExpressionKind.Constant)
			{
				sqlBuilder.Append(this.VisitConstant((DbConstantExpression)left, flag));
			}
			else
			{
				this.ParenthesizeExpressionIfNeeded(left, sqlBuilder);
			}
			sqlBuilder.Append(op);
			if (right.ExpressionKind == DbExpressionKind.Constant)
			{
				sqlBuilder.Append(this.VisitConstant((DbConstantExpression)right, flag));
			}
			else
			{
				this.ParenthesizeExpressionIfNeeded(right, sqlBuilder);
			}
			return sqlBuilder;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00017E74 File Offset: 0x00016074
		private static void RemoveUnnecessaryCasts(ref DbExpression left, ref DbExpression right)
		{
			if (left.ResultType.EdmType != right.ResultType.EdmType)
			{
				return;
			}
			DbCastExpression dbCastExpression = left as DbCastExpression;
			if (dbCastExpression != null && dbCastExpression.Argument.ResultType.EdmType == left.ResultType.EdmType)
			{
				left = dbCastExpression.Argument;
			}
			DbCastExpression dbCastExpression2 = right as DbCastExpression;
			if (dbCastExpression2 != null && dbCastExpression2.Argument.ResultType.EdmType == left.ResultType.EdmType)
			{
				right = dbCastExpression2.Argument;
			}
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00017F00 File Offset: 0x00016100
		private SqlSelectStatement VisitInputExpression(DbExpression inputExpression, string inputVarName, TypeUsage inputVarType, out Symbol fromSymbol)
		{
			ISqlFragment sqlFragment = inputExpression.Accept<ISqlFragment>(this);
			SqlSelectStatement sqlSelectStatement = sqlFragment as SqlSelectStatement;
			if (sqlSelectStatement == null)
			{
				sqlSelectStatement = new SqlSelectStatement();
				SqlGenerator.WrapNonQueryExtent(sqlSelectStatement, sqlFragment, inputExpression.ExpressionKind);
			}
			if (sqlSelectStatement.FromExtents.Count == 0)
			{
				fromSymbol = new Symbol(inputVarName, inputVarType);
			}
			else if (sqlSelectStatement.FromExtents.Count == 1)
			{
				fromSymbol = sqlSelectStatement.FromExtents[0];
			}
			else
			{
				fromSymbol = new JoinSymbol(inputVarName, inputVarType, sqlSelectStatement.FromExtents)
				{
					FlattenedExtentList = sqlSelectStatement.AllJoinExtents
				};
				sqlSelectStatement.FromExtents.Clear();
				sqlSelectStatement.FromExtents.Add(fromSymbol);
			}
			return sqlSelectStatement;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00017FA4 File Offset: 0x000161A4
		private SqlBuilder VisitIsEmptyExpression(DbIsEmptyExpression e, bool negate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			if (!negate)
			{
				sqlBuilder.Append(" NOT");
			}
			sqlBuilder.Append(" EXISTS (");
			sqlBuilder.Append(this.VisitExpressionEnsureSqlStatement(e.Argument));
			sqlBuilder.AppendLine();
			sqlBuilder.Append(")");
			return sqlBuilder;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00017FF4 File Offset: 0x000161F4
		private ISqlFragment VisitCollectionConstructor(DbNewInstanceExpression e)
		{
			if (e.Arguments.Count == 1 && e.Arguments[0].ExpressionKind == DbExpressionKind.Element)
			{
				DbElementExpression dbElementExpression = e.Arguments[0] as DbElementExpression;
				SqlSelectStatement sqlSelectStatement = this.VisitExpressionEnsureSqlStatement(dbElementExpression.Argument);
				if (!SqlGenerator.IsCompatible(sqlSelectStatement, DbExpressionKind.Element))
				{
					TypeUsage elementTypeUsage = dbElementExpression.Argument.ResultType.GetElementTypeUsage();
					Symbol symbol;
					sqlSelectStatement = this.CreateNewSelectStatement(sqlSelectStatement, "element", elementTypeUsage, out symbol);
					this.AddFromSymbol(sqlSelectStatement, "element", symbol, false);
				}
				sqlSelectStatement.Select.Top = new TopClause(1, false);
				return sqlSelectStatement;
			}
			CollectionType collectionType = (CollectionType)e.ResultType.EdmType;
			bool flag = BuiltInTypeKind.PrimitiveType == collectionType.TypeUsage.EdmType.BuiltInTypeKind;
			SqlBuilder sqlBuilder = new SqlBuilder();
			string text = "";
			if (e.Arguments.Count == 0)
			{
				sqlBuilder.Append(" SELECT CAST(null as ");
				sqlBuilder.Append(this.GetSqlPrimitiveType(collectionType.TypeUsage));
				sqlBuilder.Append(") AS X FROM (SELECT 1) AS Y WHERE 1=0");
			}
			foreach (DbExpression dbExpression in e.Arguments)
			{
				sqlBuilder.Append(text);
				sqlBuilder.Append(" SELECT ");
				sqlBuilder.Append(dbExpression.Accept<ISqlFragment>(this));
				if (flag)
				{
					sqlBuilder.Append(" AS X ");
				}
				text = " UNION ALL ";
			}
			return sqlBuilder;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0001817C File Offset: 0x0001637C
		private SqlBuilder VisitIsNullExpression(DbIsNullExpression e, bool negate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			if (e.Argument.ExpressionKind == DbExpressionKind.ParameterReference)
			{
				this._ignoreForceNonUnicodeFlag = true;
			}
			sqlBuilder.Append(e.Argument.Accept<ISqlFragment>(this));
			this._ignoreForceNonUnicodeFlag = false;
			if (!negate)
			{
				sqlBuilder.Append(" IS NULL");
			}
			else
			{
				sqlBuilder.Append(" IS NOT NULL");
			}
			return sqlBuilder;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x000181DC File Offset: 0x000163DC
		private ISqlFragment VisitJoinExpression(IList<DbExpressionBinding> inputs, DbExpressionKind joinKind, string joinString, DbExpression joinCondition)
		{
			SqlSelectStatement sqlSelectStatement;
			if (!this.IsParentAJoin)
			{
				sqlSelectStatement = new SqlSelectStatement();
				sqlSelectStatement.AllJoinExtents = new List<Symbol>();
				this.selectStatementStack.Push(sqlSelectStatement);
			}
			else
			{
				sqlSelectStatement = this.CurrentSelectStatement;
			}
			this.symbolTable.EnterScope();
			string text = "";
			bool flag = true;
			int count = inputs.Count;
			for (int i = 0; i < count; i++)
			{
				DbExpressionBinding dbExpressionBinding = inputs[i];
				if (text.Length != 0)
				{
					sqlSelectStatement.From.AppendLine();
				}
				sqlSelectStatement.From.Append(text + " ");
				bool flag2 = dbExpressionBinding.Expression.ExpressionKind == DbExpressionKind.Scan || (flag && (SqlGenerator.IsJoinExpression(dbExpressionBinding.Expression) || SqlGenerator.IsApplyExpression(dbExpressionBinding.Expression)));
				this.isParentAJoinStack.Push(flag2);
				int count2 = sqlSelectStatement.FromExtents.Count;
				ISqlFragment sqlFragment = dbExpressionBinding.Expression.Accept<ISqlFragment>(this);
				this.isParentAJoinStack.Pop();
				this.ProcessJoinInputResult(sqlFragment, sqlSelectStatement, dbExpressionBinding, count2);
				text = joinString;
				flag = false;
			}
			if (joinKind == DbExpressionKind.FullOuterJoin || joinKind == DbExpressionKind.InnerJoin || joinKind == DbExpressionKind.LeftOuterJoin)
			{
				sqlSelectStatement.From.Append(" ON ");
				this.isParentAJoinStack.Push(false);
				sqlSelectStatement.From.Append(joinCondition.Accept<ISqlFragment>(this));
				this.isParentAJoinStack.Pop();
			}
			this.symbolTable.ExitScope();
			if (!this.IsParentAJoin)
			{
				this.selectStatementStack.Pop();
			}
			return sqlSelectStatement;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00018368 File Offset: 0x00016568
		private void ProcessJoinInputResult(ISqlFragment fromExtentFragment, SqlSelectStatement result, DbExpressionBinding input, int fromSymbolStart)
		{
			Symbol symbol = null;
			if (result != fromExtentFragment)
			{
				SqlSelectStatement sqlSelectStatement = fromExtentFragment as SqlSelectStatement;
				if (sqlSelectStatement != null)
				{
					if (sqlSelectStatement.Select.IsEmpty)
					{
						List<Symbol> list = this.AddDefaultColumns(sqlSelectStatement);
						if (SqlGenerator.IsJoinExpression(input.Expression) || SqlGenerator.IsApplyExpression(input.Expression))
						{
							List<Symbol> fromExtents = sqlSelectStatement.FromExtents;
							symbol = new JoinSymbol(input.VariableName, input.VariableType, fromExtents)
							{
								IsNestedJoin = true,
								ColumnList = list
							};
						}
						else
						{
							JoinSymbol joinSymbol = sqlSelectStatement.FromExtents[0] as JoinSymbol;
							if (joinSymbol != null)
							{
								symbol = new JoinSymbol(input.VariableName, input.VariableType, joinSymbol.ExtentList)
								{
									IsNestedJoin = true,
									ColumnList = list,
									FlattenedExtentList = joinSymbol.FlattenedExtentList
								};
							}
							else
							{
								symbol = new Symbol(input.VariableName, input.VariableType, sqlSelectStatement.OutputColumns, sqlSelectStatement.OutputColumnsRenamed);
							}
						}
					}
					else
					{
						symbol = new Symbol(input.VariableName, input.VariableType, sqlSelectStatement.OutputColumns, sqlSelectStatement.OutputColumnsRenamed);
					}
					result.From.Append(" (");
					result.From.Append(sqlSelectStatement);
					result.From.Append(" )");
				}
				else if (input.Expression is DbScanExpression)
				{
					result.From.Append(fromExtentFragment);
				}
				else
				{
					SqlGenerator.WrapNonQueryExtent(result, fromExtentFragment, input.Expression.ExpressionKind);
				}
				if (symbol == null)
				{
					symbol = new Symbol(input.VariableName, input.VariableType);
				}
				this.AddFromSymbol(result, input.VariableName, symbol);
				result.AllJoinExtents.Add(symbol);
				return;
			}
			List<Symbol> list2 = new List<Symbol>();
			for (int i = fromSymbolStart; i < result.FromExtents.Count; i++)
			{
				list2.Add(result.FromExtents[i]);
			}
			result.FromExtents.RemoveRange(fromSymbolStart, result.FromExtents.Count - fromSymbolStart);
			symbol = new JoinSymbol(input.VariableName, input.VariableType, list2);
			result.FromExtents.Add(symbol);
			this.symbolTable.Add(input.VariableName, symbol);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00018584 File Offset: 0x00016784
		private ISqlFragment VisitNewInstanceExpression(DbNewInstanceExpression e, bool aliasesNeedRenaming, out Dictionary<string, Symbol> newColumns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			RowType rowType = e.ResultType.EdmType as RowType;
			if (rowType != null)
			{
				newColumns = new Dictionary<string, Symbol>(e.Arguments.Count);
				ReadOnlyMetadataCollection<EdmProperty> properties = rowType.Properties;
				string text = "";
				for (int i = 0; i < e.Arguments.Count; i++)
				{
					DbExpression dbExpression = e.Arguments[i];
					if (BuiltInTypeKind.RowType == dbExpression.ResultType.EdmType.BuiltInTypeKind)
					{
						throw new NotSupportedException();
					}
					EdmProperty edmProperty = properties[i];
					sqlBuilder.Append(text);
					sqlBuilder.AppendLine();
					sqlBuilder.Append(dbExpression.Accept<ISqlFragment>(this));
					sqlBuilder.Append(" AS ");
					if (aliasesNeedRenaming)
					{
						Symbol symbol = new Symbol(edmProperty.Name, edmProperty.TypeUsage);
						symbol.NeedsRenaming = true;
						symbol.NewName = "Internal_" + edmProperty.Name;
						sqlBuilder.Append(symbol);
						newColumns.Add(edmProperty.Name, symbol);
					}
					else
					{
						sqlBuilder.Append(SqlGenerator.QuoteIdentifier(edmProperty.Name));
					}
					text = ", ";
				}
				return sqlBuilder;
			}
			throw new NotSupportedException();
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x000186BC File Offset: 0x000168BC
		private ISqlFragment VisitSetOpExpression(DbBinaryExpression setOpExpression, string separator)
		{
			List<SqlSelectStatement> list = new List<SqlSelectStatement>();
			this.VisitAndGatherSetOpLeafExpressions(setOpExpression.ExpressionKind, setOpExpression.Left, list);
			this.VisitAndGatherSetOpLeafExpressions(setOpExpression.ExpressionKind, setOpExpression.Right, list);
			SqlBuilder sqlBuilder = new SqlBuilder();
			for (int i = 0; i < list.Count; i++)
			{
				if (i > 0)
				{
					sqlBuilder.AppendLine();
					sqlBuilder.Append(separator);
					sqlBuilder.AppendLine();
				}
				sqlBuilder.Append(list[i]);
			}
			if (!list[0].OutputColumnsRenamed)
			{
				return sqlBuilder;
			}
			SqlSelectStatement sqlSelectStatement = new SqlSelectStatement();
			sqlSelectStatement.From.Append("( ");
			sqlSelectStatement.From.Append(sqlBuilder);
			sqlSelectStatement.From.AppendLine();
			sqlSelectStatement.From.Append(") ");
			Symbol symbol = new Symbol("X", setOpExpression.Left.ResultType.GetElementTypeUsage(), list[0].OutputColumns, true);
			this.AddFromSymbol(sqlSelectStatement, null, symbol, false);
			return sqlSelectStatement;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x000187B4 File Offset: 0x000169B4
		private void VisitAndGatherSetOpLeafExpressions(DbExpressionKind kind, DbExpression expression, List<SqlSelectStatement> leafSelectStatements)
		{
			if (this.SqlVersion > SqlVersion.Sql8 && (kind == DbExpressionKind.UnionAll || kind == DbExpressionKind.Intersect) && expression.ExpressionKind == kind)
			{
				DbBinaryExpression dbBinaryExpression = (DbBinaryExpression)expression;
				this.VisitAndGatherSetOpLeafExpressions(kind, dbBinaryExpression.Left, leafSelectStatements);
				this.VisitAndGatherSetOpLeafExpressions(kind, dbBinaryExpression.Right, leafSelectStatements);
				return;
			}
			leafSelectStatements.Add(this.VisitExpressionEnsureSqlStatement(expression, true, true));
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00018814 File Offset: 0x00016A14
		private void AddColumns(SqlSelectStatement selectStatement, Symbol symbol, List<Symbol> columnList, Dictionary<string, Symbol> columnDictionary)
		{
			JoinSymbol joinSymbol = symbol as JoinSymbol;
			if (joinSymbol != null)
			{
				if (!joinSymbol.IsNestedJoin)
				{
					using (List<Symbol>.Enumerator enumerator = joinSymbol.ExtentList.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Symbol symbol2 = enumerator.Current;
							if (symbol2.Type != null && BuiltInTypeKind.PrimitiveType != symbol2.Type.EdmType.BuiltInTypeKind)
							{
								this.AddColumns(selectStatement, symbol2, columnList, columnDictionary);
							}
						}
						return;
					}
				}
				using (List<Symbol>.Enumerator enumerator = joinSymbol.ColumnList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Symbol symbol3 = enumerator.Current;
						OptionalColumn optionalColumn = this.CreateOptionalColumn(null, symbol3);
						optionalColumn.Append(symbol);
						optionalColumn.Append(".");
						optionalColumn.Append(symbol3);
						selectStatement.Select.AddOptionalColumn(optionalColumn);
						if (columnDictionary.ContainsKey(symbol3.Name))
						{
							columnDictionary[symbol3.Name].NeedsRenaming = true;
							symbol3.NeedsRenaming = true;
						}
						else
						{
							columnDictionary[symbol3.Name] = symbol3;
						}
						columnList.Add(symbol3);
					}
					return;
				}
			}
			if (symbol.OutputColumnsRenamed)
			{
				selectStatement.OutputColumnsRenamed = true;
			}
			if (selectStatement.OutputColumns == null)
			{
				selectStatement.OutputColumns = new Dictionary<string, Symbol>();
			}
			if (symbol.Type == null || BuiltInTypeKind.PrimitiveType == symbol.Type.EdmType.BuiltInTypeKind)
			{
				this.AddColumn(selectStatement, symbol, columnList, columnDictionary, "X");
				return;
			}
			foreach (EdmProperty edmProperty in symbol.Type.GetProperties())
			{
				this.AddColumn(selectStatement, symbol, columnList, columnDictionary, edmProperty.Name);
			}
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x000189F4 File Offset: 0x00016BF4
		private OptionalColumn CreateOptionalColumn(Symbol inputColumnSymbol, Symbol column)
		{
			if (!this.optionalColumnUsageManager.ContainsKey(column))
			{
				this.optionalColumnUsageManager.Add(inputColumnSymbol, column);
			}
			return new OptionalColumn(this.optionalColumnUsageManager, column);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00018A20 File Offset: 0x00016C20
		private void AddColumn(SqlSelectStatement selectStatement, Symbol symbol, List<Symbol> columnList, Dictionary<string, Symbol> columnDictionary, string columnName)
		{
			this.allColumnNames[columnName] = 0;
			Symbol symbol2 = null;
			symbol.OutputColumns.TryGetValue(columnName, out symbol2);
			Symbol symbol3;
			if (!symbol.Columns.TryGetValue(columnName, out symbol3))
			{
				symbol3 = ((symbol2 != null && symbol.OutputColumnsRenamed) ? symbol2 : new Symbol(columnName, null));
				symbol.Columns.Add(columnName, symbol3);
			}
			OptionalColumn optionalColumn = this.CreateOptionalColumn(symbol2, symbol3);
			optionalColumn.Append(symbol);
			optionalColumn.Append(".");
			if (symbol.OutputColumnsRenamed)
			{
				optionalColumn.Append(symbol2);
			}
			else
			{
				optionalColumn.Append(SqlGenerator.QuoteIdentifier(columnName));
			}
			optionalColumn.Append(" AS ");
			optionalColumn.Append(symbol3);
			selectStatement.Select.AddOptionalColumn(optionalColumn);
			if (!selectStatement.OutputColumns.ContainsKey(columnName))
			{
				selectStatement.OutputColumns.Add(columnName, symbol3);
			}
			if (columnDictionary.ContainsKey(columnName))
			{
				columnDictionary[columnName].NeedsRenaming = true;
				symbol3.NeedsRenaming = true;
			}
			else
			{
				columnDictionary[columnName] = symbol.Columns[columnName];
			}
			columnList.Add(symbol3);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00018B38 File Offset: 0x00016D38
		private List<Symbol> AddDefaultColumns(SqlSelectStatement selectStatement)
		{
			List<Symbol> list = new List<Symbol>();
			Dictionary<string, Symbol> dictionary = new Dictionary<string, Symbol>(StringComparer.OrdinalIgnoreCase);
			foreach (Symbol symbol in selectStatement.FromExtents)
			{
				this.AddColumns(selectStatement, symbol, list, dictionary);
			}
			return list;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00018BA0 File Offset: 0x00016DA0
		private void AddFromSymbol(SqlSelectStatement selectStatement, string inputVarName, Symbol fromSymbol)
		{
			this.AddFromSymbol(selectStatement, inputVarName, fromSymbol, true);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00018BAC File Offset: 0x00016DAC
		private void AddFromSymbol(SqlSelectStatement selectStatement, string inputVarName, Symbol fromSymbol, bool addToSymbolTable)
		{
			if (selectStatement.FromExtents.Count == 0 || fromSymbol != selectStatement.FromExtents[0])
			{
				selectStatement.FromExtents.Add(fromSymbol);
				selectStatement.From.Append(" AS ");
				selectStatement.From.Append(fromSymbol);
				this.allExtentNames[fromSymbol.Name] = 0;
			}
			if (addToSymbolTable)
			{
				this.symbolTable.Add(inputVarName, fromSymbol);
			}
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00018C20 File Offset: 0x00016E20
		private void AddSortKeys(SqlBuilder orderByClause, IList<DbSortClause> sortKeys)
		{
			string text = "";
			foreach (DbSortClause dbSortClause in sortKeys)
			{
				orderByClause.Append(text);
				orderByClause.Append(dbSortClause.Expression.Accept<ISqlFragment>(this));
				if (!string.IsNullOrEmpty(dbSortClause.Collation))
				{
					orderByClause.Append(" COLLATE ");
					orderByClause.Append(dbSortClause.Collation);
				}
				orderByClause.Append(dbSortClause.Ascending ? " ASC" : " DESC");
				text = ", ";
			}
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00018CC4 File Offset: 0x00016EC4
		private SqlSelectStatement CreateNewSelectStatement(SqlSelectStatement oldStatement, string inputVarName, TypeUsage inputVarType, out Symbol fromSymbol)
		{
			return this.CreateNewSelectStatement(oldStatement, inputVarName, inputVarType, true, out fromSymbol);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00018CD4 File Offset: 0x00016ED4
		private SqlSelectStatement CreateNewSelectStatement(SqlSelectStatement oldStatement, string inputVarName, TypeUsage inputVarType, bool finalizeOldStatement, out Symbol fromSymbol)
		{
			fromSymbol = null;
			if (finalizeOldStatement && oldStatement.Select.IsEmpty)
			{
				List<Symbol> list = this.AddDefaultColumns(oldStatement);
				JoinSymbol joinSymbol = oldStatement.FromExtents[0] as JoinSymbol;
				if (joinSymbol != null)
				{
					fromSymbol = new JoinSymbol(inputVarName, inputVarType, joinSymbol.ExtentList)
					{
						IsNestedJoin = true,
						ColumnList = list,
						FlattenedExtentList = joinSymbol.FlattenedExtentList
					};
				}
			}
			if (fromSymbol == null)
			{
				fromSymbol = new Symbol(inputVarName, inputVarType, oldStatement.OutputColumns, oldStatement.OutputColumnsRenamed);
			}
			SqlSelectStatement sqlSelectStatement = new SqlSelectStatement();
			sqlSelectStatement.From.Append("( ");
			sqlSelectStatement.From.Append(oldStatement);
			sqlSelectStatement.From.AppendLine();
			sqlSelectStatement.From.Append(") ");
			return sqlSelectStatement;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00018D96 File Offset: 0x00016F96
		private static string EscapeSingleQuote(string s, bool isUnicode)
		{
			return (isUnicode ? "N'" : "'") + s.Replace("'", "''") + "'";
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00018DC4 File Offset: 0x00016FC4
		private string GetSqlPrimitiveType(TypeUsage type)
		{
			TypeUsage storeType = this._storeItemCollection.ProviderManifest.GetStoreType(type);
			return SqlGenerator.GenerateSqlForStoreType(this._sqlVersion, storeType);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00018DF0 File Offset: 0x00016FF0
		internal static string GenerateSqlForStoreType(SqlVersion sqlVersion, TypeUsage storeTypeUsage)
		{
			string text = storeTypeUsage.EdmType.Name;
			int num = 0;
			byte b = 0;
			byte b2 = 0;
			PrimitiveTypeKind primitiveTypeKind = ((PrimitiveType)storeTypeUsage.EdmType).PrimitiveTypeKind;
			switch (primitiveTypeKind)
			{
			case PrimitiveTypeKind.Binary:
				if (!storeTypeUsage.MustFacetBeConstant("MaxLength"))
				{
					storeTypeUsage.TryGetMaxLength(out num);
					text = text + "(" + num.ToString(CultureInfo.InvariantCulture) + ")";
				}
				break;
			case PrimitiveTypeKind.Boolean:
			case PrimitiveTypeKind.Byte:
				break;
			case PrimitiveTypeKind.DateTime:
				text = (SqlVersionUtils.IsPreKatmai(sqlVersion) ? "datetime" : "datetime2");
				break;
			case PrimitiveTypeKind.Decimal:
				if (!storeTypeUsage.MustFacetBeConstant("Precision"))
				{
					storeTypeUsage.TryGetPrecision(out b);
					storeTypeUsage.TryGetScale(out b2);
					text = string.Concat(new string[]
					{
						text,
						"(",
						b.ToString(),
						",",
						b2.ToString(),
						")"
					});
				}
				break;
			default:
				switch (primitiveTypeKind)
				{
				case PrimitiveTypeKind.String:
					if (!storeTypeUsage.MustFacetBeConstant("MaxLength"))
					{
						storeTypeUsage.TryGetMaxLength(out num);
						text = text + "(" + num.ToString(CultureInfo.InvariantCulture) + ")";
					}
					break;
				case PrimitiveTypeKind.Time:
					SqlGenerator.AssertKatmaiOrNewer(sqlVersion, primitiveTypeKind);
					text = "time";
					break;
				case PrimitiveTypeKind.DateTimeOffset:
					SqlGenerator.AssertKatmaiOrNewer(sqlVersion, primitiveTypeKind);
					text = "datetimeoffset";
					break;
				}
				break;
			}
			return text;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00018F60 File Offset: 0x00017160
		private ISqlFragment HandleCountExpression(DbExpression e)
		{
			ISqlFragment sqlFragment;
			if (e.ExpressionKind == DbExpressionKind.Constant)
			{
				SqlBuilder sqlBuilder = new SqlBuilder();
				sqlBuilder.Append(((DbConstantExpression)e).Value.ToString());
				sqlFragment = sqlBuilder;
			}
			else
			{
				sqlFragment = e.Accept<ISqlFragment>(this);
			}
			return sqlFragment;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00018F9D File Offset: 0x0001719D
		private static bool IsApplyExpression(DbExpression e)
		{
			return DbExpressionKind.CrossApply == e.ExpressionKind || DbExpressionKind.OuterApply == e.ExpressionKind;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00018FB4 File Offset: 0x000171B4
		private static bool IsJoinExpression(DbExpression e)
		{
			return DbExpressionKind.CrossJoin == e.ExpressionKind || DbExpressionKind.FullOuterJoin == e.ExpressionKind || DbExpressionKind.InnerJoin == e.ExpressionKind || DbExpressionKind.LeftOuterJoin == e.ExpressionKind;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00018FE0 File Offset: 0x000171E0
		private static bool IsComplexExpression(DbExpression e)
		{
			DbExpressionKind expressionKind = e.ExpressionKind;
			return expressionKind - DbExpressionKind.Cast > 1 && expressionKind != DbExpressionKind.ParameterReference && expressionKind != DbExpressionKind.Property;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00019008 File Offset: 0x00017208
		private static bool IsCompatible(SqlSelectStatement result, DbExpressionKind expressionKind)
		{
			if (expressionKind <= DbExpressionKind.GroupBy)
			{
				if (expressionKind <= DbExpressionKind.Element)
				{
					if (expressionKind == DbExpressionKind.Distinct)
					{
						return result.Select.Top == null && result.Select.Skip == null && result.OrderBy.IsEmpty;
					}
					if (expressionKind != DbExpressionKind.Element)
					{
						goto IL_01D3;
					}
				}
				else
				{
					if (expressionKind == DbExpressionKind.Filter)
					{
						return result.Select.IsEmpty && result.Where.IsEmpty && result.GroupBy.IsEmpty && result.Select.Top == null && result.Select.Skip == null;
					}
					if (expressionKind != DbExpressionKind.GroupBy)
					{
						goto IL_01D3;
					}
					return result.Select.IsEmpty && result.GroupBy.IsEmpty && result.OrderBy.IsEmpty && result.Select.Top == null && result.Select.Skip == null && !result.Select.IsDistinct;
				}
			}
			else if (expressionKind <= DbExpressionKind.Project)
			{
				if (expressionKind != DbExpressionKind.Limit)
				{
					if (expressionKind != DbExpressionKind.Project)
					{
						goto IL_01D3;
					}
					return result.Select.IsEmpty && result.GroupBy.IsEmpty && !result.Select.IsDistinct;
				}
			}
			else
			{
				if (expressionKind == DbExpressionKind.Skip)
				{
					return result.Select.IsEmpty && result.Select.Skip == null && result.GroupBy.IsEmpty && result.OrderBy.IsEmpty && !result.Select.IsDistinct;
				}
				if (expressionKind != DbExpressionKind.Sort)
				{
					goto IL_01D3;
				}
				return result.Select.IsEmpty && result.GroupBy.IsEmpty && result.OrderBy.IsEmpty && !result.Select.IsDistinct;
			}
			return result.Select.Top == null;
			IL_01D3:
			throw new InvalidOperationException(string.Empty);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x000191F2 File Offset: 0x000173F2
		internal static string QuoteIdentifier(string name)
		{
			return "[" + name.Replace("]", "]]") + "]";
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00019213 File Offset: 0x00017413
		private SqlSelectStatement VisitExpressionEnsureSqlStatement(DbExpression e)
		{
			return this.VisitExpressionEnsureSqlStatement(e, true, false);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00019220 File Offset: 0x00017420
		private SqlSelectStatement VisitExpressionEnsureSqlStatement(DbExpression e, bool addDefaultColumns, bool markAllDefaultColumnsAsUsed)
		{
			DbExpressionKind expressionKind = e.ExpressionKind;
			if (expressionKind <= DbExpressionKind.GroupBy)
			{
				if (expressionKind != DbExpressionKind.Filter && expressionKind != DbExpressionKind.GroupBy)
				{
					goto IL_003A;
				}
			}
			else if (expressionKind != DbExpressionKind.Project && expressionKind != DbExpressionKind.Sort)
			{
				goto IL_003A;
			}
			SqlSelectStatement sqlSelectStatement = e.Accept<ISqlFragment>(this) as SqlSelectStatement;
			goto IL_00C6;
			IL_003A:
			string text = "c";
			this.symbolTable.EnterScope();
			DbExpressionKind expressionKind2 = e.ExpressionKind;
			if (expressionKind2 <= DbExpressionKind.InnerJoin)
			{
				if (expressionKind2 - DbExpressionKind.CrossApply > 1 && expressionKind2 != DbExpressionKind.FullOuterJoin && expressionKind2 != DbExpressionKind.InnerJoin)
				{
					goto IL_0090;
				}
			}
			else if (expressionKind2 != DbExpressionKind.LeftOuterJoin && expressionKind2 != DbExpressionKind.OuterApply && expressionKind2 != DbExpressionKind.Scan)
			{
				goto IL_0090;
			}
			TypeUsage typeUsage = e.ResultType.GetElementTypeUsage();
			goto IL_00A6;
			IL_0090:
			typeUsage = ((CollectionType)e.ResultType.EdmType).TypeUsage;
			IL_00A6:
			Symbol symbol;
			sqlSelectStatement = this.VisitInputExpression(e, text, typeUsage, out symbol);
			this.AddFromSymbol(sqlSelectStatement, text, symbol);
			this.symbolTable.ExitScope();
			IL_00C6:
			if (addDefaultColumns && sqlSelectStatement.Select.IsEmpty)
			{
				List<Symbol> list = this.AddDefaultColumns(sqlSelectStatement);
				if (markAllDefaultColumnsAsUsed)
				{
					foreach (Symbol symbol2 in list)
					{
						this.optionalColumnUsageManager.MarkAsUsed(symbol2);
					}
				}
			}
			return sqlSelectStatement;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0001935C File Offset: 0x0001755C
		private SqlSelectStatement VisitFilterExpression(DbExpressionBinding input, DbExpression predicate, bool negatePredicate)
		{
			Symbol symbol;
			SqlSelectStatement sqlSelectStatement = this.VisitInputExpression(input.Expression, input.VariableName, input.VariableType, out symbol);
			if (!SqlGenerator.IsCompatible(sqlSelectStatement, DbExpressionKind.Filter))
			{
				sqlSelectStatement = this.CreateNewSelectStatement(sqlSelectStatement, input.VariableName, input.VariableType, out symbol);
			}
			this.selectStatementStack.Push(sqlSelectStatement);
			this.symbolTable.EnterScope();
			this.AddFromSymbol(sqlSelectStatement, input.VariableName, symbol);
			if (negatePredicate)
			{
				sqlSelectStatement.Where.Append("NOT (");
			}
			sqlSelectStatement.Where.Append(predicate.Accept<ISqlFragment>(this));
			if (negatePredicate)
			{
				sqlSelectStatement.Where.Append(")");
			}
			this.symbolTable.ExitScope();
			this.selectStatementStack.Pop();
			return sqlSelectStatement;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00019419 File Offset: 0x00017619
		private static void WrapNonQueryExtent(SqlSelectStatement result, ISqlFragment sqlFragment, DbExpressionKind expressionKind)
		{
			if (expressionKind == DbExpressionKind.Function)
			{
				result.From.Append(sqlFragment);
				return;
			}
			result.From.Append(" (");
			result.From.Append(sqlFragment);
			result.From.Append(")");
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0001945C File Offset: 0x0001765C
		private static string ByteArrayToBinaryString(byte[] binaryArray)
		{
			StringBuilder stringBuilder = new StringBuilder(binaryArray.Length * 2);
			for (int i = 0; i < binaryArray.Length; i++)
			{
				stringBuilder.Append(SqlGenerator._hexDigits[(binaryArray[i] & 240) >> 4]).Append(SqlGenerator._hexDigits[(int)(binaryArray[i] & 15)]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x000194B4 File Offset: 0x000176B4
		private static bool GroupByAggregatesNeedInnerQuery(IList<DbAggregate> aggregates, string inputVarRefName)
		{
			using (IEnumerator<DbAggregate> enumerator = aggregates.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (SqlGenerator.GroupByAggregateNeedsInnerQuery(enumerator.Current.Arguments[0], inputVarRefName))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00019510 File Offset: 0x00017710
		private static bool GroupByAggregateNeedsInnerQuery(DbExpression expression, string inputVarRefName)
		{
			return SqlGenerator.GroupByExpressionNeedsInnerQuery(expression, inputVarRefName, true);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0001951C File Offset: 0x0001771C
		private static bool GroupByKeysNeedInnerQuery(IList<DbExpression> keys, string inputVarRefName)
		{
			using (IEnumerator<DbExpression> enumerator = keys.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (SqlGenerator.GroupByKeyNeedsInnerQuery(enumerator.Current, inputVarRefName))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0001956C File Offset: 0x0001776C
		private static bool GroupByKeyNeedsInnerQuery(DbExpression expression, string inputVarRefName)
		{
			return SqlGenerator.GroupByExpressionNeedsInnerQuery(expression, inputVarRefName, false);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00019578 File Offset: 0x00017778
		private static bool GroupByExpressionNeedsInnerQuery(DbExpression expression, string inputVarRefName, bool allowConstants)
		{
			if (allowConstants && expression.ExpressionKind == DbExpressionKind.Constant)
			{
				return false;
			}
			if (expression.ExpressionKind == DbExpressionKind.Cast)
			{
				return SqlGenerator.GroupByExpressionNeedsInnerQuery(((DbCastExpression)expression).Argument, inputVarRefName, allowConstants);
			}
			if (expression.ExpressionKind == DbExpressionKind.Property)
			{
				return SqlGenerator.GroupByExpressionNeedsInnerQuery(((DbPropertyExpression)expression).Instance, inputVarRefName, allowConstants);
			}
			return expression.ExpressionKind != DbExpressionKind.VariableReference || !(expression as DbVariableReferenceExpression).VariableName.Equals(inputVarRefName);
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x000195EC File Offset: 0x000177EC
		private void AssertKatmaiOrNewer(PrimitiveTypeKind primitiveTypeKind)
		{
			SqlGenerator.AssertKatmaiOrNewer(this._sqlVersion, primitiveTypeKind);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x000195FA File Offset: 0x000177FA
		private static void AssertKatmaiOrNewer(SqlVersion sqlVersion, PrimitiveTypeKind primitiveTypeKind)
		{
			if (SqlVersionUtils.IsPreKatmai(sqlVersion))
			{
				throw new NotSupportedException(Strings.SqlGen_PrimitiveTypeNotSupportedPriorSql10(primitiveTypeKind));
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00019615 File Offset: 0x00017815
		internal void AssertKatmaiOrNewer(DbFunctionExpression e)
		{
			if (this.IsPreKatmai)
			{
				throw new NotSupportedException(Strings.SqlGen_CanonicalFunctionNotSupportedPriorSql10(e.Function.Name));
			}
		}

		// Token: 0x040000FA RID: 250
		private Stack<SqlSelectStatement> selectStatementStack;

		// Token: 0x040000FB RID: 251
		private Stack<bool> isParentAJoinStack;

		// Token: 0x040000FC RID: 252
		private Dictionary<string, int> allExtentNames;

		// Token: 0x040000FD RID: 253
		private Dictionary<string, int> allColumnNames;

		// Token: 0x040000FE RID: 254
		private readonly SymbolTable symbolTable = new SymbolTable();

		// Token: 0x040000FF RID: 255
		private bool isVarRefSingle;

		// Token: 0x04000100 RID: 256
		private readonly SymbolUsageManager optionalColumnUsageManager = new SymbolUsageManager();

		// Token: 0x04000101 RID: 257
		private readonly Dictionary<string, bool> _candidateParametersToForceNonUnicode = new Dictionary<string, bool>();

		// Token: 0x04000102 RID: 258
		private bool _forceNonUnicode;

		// Token: 0x04000103 RID: 259
		private bool _ignoreForceNonUnicodeFlag;

		// Token: 0x04000104 RID: 260
		private const byte DefaultDecimalPrecision = 18;

		// Token: 0x04000105 RID: 261
		private static readonly char[] _hexDigits = new char[]
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'A', 'B', 'C', 'D', 'E', 'F'
		};

		// Token: 0x04000106 RID: 262
		private List<string> _targets;

		// Token: 0x04000107 RID: 263
		private static readonly ISet<string> _canonicalAndStoreStringFunctionsOneArg = new HashSet<string>(StringComparer.Ordinal)
		{
			"Edm.Trim", "Edm.RTrim", "Edm.LTrim", "Edm.Left", "Edm.Right", "Edm.Substring", "Edm.ToLower", "Edm.ToUpper", "Edm.Reverse", "SqlServer.RTRIM",
			"SqlServer.LTRIM", "SqlServer.LEFT", "SqlServer.RIGHT", "SqlServer.SUBSTRING", "SqlServer.LOWER", "SqlServer.UPPER", "SqlServer.REVERSE"
		};

		// Token: 0x04000108 RID: 264
		private readonly SqlVersion _sqlVersion;

		// Token: 0x04000109 RID: 265
		private TypeUsage _integerType;

		// Token: 0x0400010A RID: 266
		private StoreItemCollection _storeItemCollection;

		// Token: 0x0200008E RID: 142
		internal class KeyFieldExpressionComparer : IEqualityComparer<DbExpression>
		{
			// Token: 0x0600073B RID: 1851 RVA: 0x0001CC7C File Offset: 0x0001AE7C
			public bool Equals(DbExpression x, DbExpression y)
			{
				if (x.ExpressionKind != y.ExpressionKind)
				{
					return false;
				}
				DbExpressionKind expressionKind = x.ExpressionKind;
				if (expressionKind <= DbExpressionKind.ParameterReference)
				{
					if (expressionKind == DbExpressionKind.Cast)
					{
						DbCastExpression dbCastExpression = (DbCastExpression)x;
						DbCastExpression dbCastExpression2 = (DbCastExpression)y;
						return dbCastExpression.ResultType == dbCastExpression2.ResultType && this.Equals(dbCastExpression.Argument, dbCastExpression2.Argument);
					}
					if (expressionKind == DbExpressionKind.ParameterReference)
					{
						return ((DbParameterReferenceExpression)x).ParameterName == ((DbParameterReferenceExpression)y).ParameterName;
					}
				}
				else
				{
					if (expressionKind == DbExpressionKind.Property)
					{
						DbPropertyExpression dbPropertyExpression = (DbPropertyExpression)x;
						DbPropertyExpression dbPropertyExpression2 = (DbPropertyExpression)y;
						return dbPropertyExpression.Property == dbPropertyExpression2.Property && this.Equals(dbPropertyExpression.Instance, dbPropertyExpression2.Instance);
					}
					if (expressionKind == DbExpressionKind.VariableReference)
					{
						return x == y;
					}
				}
				return false;
			}

			// Token: 0x0600073C RID: 1852 RVA: 0x0001CD4C File Offset: 0x0001AF4C
			public int GetHashCode(DbExpression obj)
			{
				DbExpressionKind expressionKind = obj.ExpressionKind;
				if (expressionKind <= DbExpressionKind.ParameterReference)
				{
					if (expressionKind == DbExpressionKind.Cast)
					{
						return this.GetHashCode(((DbCastExpression)obj).Argument);
					}
					if (expressionKind == DbExpressionKind.ParameterReference)
					{
						return ((DbParameterReferenceExpression)obj).ParameterName.GetHashCode() ^ int.MaxValue;
					}
				}
				else
				{
					if (expressionKind == DbExpressionKind.Property)
					{
						return ((DbPropertyExpression)obj).Property.GetHashCode();
					}
					if (expressionKind == DbExpressionKind.VariableReference)
					{
						return ((DbVariableReferenceExpression)obj).VariableName.GetHashCode();
					}
				}
				return obj.GetHashCode();
			}
		}
	}
}
