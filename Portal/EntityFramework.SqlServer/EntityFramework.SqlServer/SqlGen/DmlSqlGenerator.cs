using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.SqlServer.Resources;
using System.Data.Entity.SqlServer.Utilities;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x0200002E RID: 46
	internal static class DmlSqlGenerator
	{
		// Token: 0x0600044D RID: 1101 RVA: 0x00010CA0 File Offset: 0x0000EEA0
		internal static string GenerateUpdateSql(DbUpdateCommandTree tree, SqlGenerator sqlGenerator, out List<SqlParameter> parameters, bool generateReturningSql = true, bool upperCaseKeywords = true)
		{
			SqlStringBuilder sqlStringBuilder = new SqlStringBuilder(256)
			{
				UpperCaseKeywords = upperCaseKeywords
			};
			DmlSqlGenerator.ExpressionTranslator expressionTranslator = new DmlSqlGenerator.ExpressionTranslator(sqlStringBuilder, tree, tree.Returning != null, sqlGenerator, null, true);
			if (tree.SetClauses.Count == 0)
			{
				sqlStringBuilder.AppendKeyword("declare ");
				sqlStringBuilder.AppendLine("@p int");
			}
			sqlStringBuilder.AppendKeyword("update ");
			tree.Target.Expression.Accept(expressionTranslator);
			sqlStringBuilder.AppendLine();
			bool flag = true;
			sqlStringBuilder.AppendKeyword("set ");
			foreach (DbModificationClause dbModificationClause in tree.SetClauses)
			{
				DbSetClause dbSetClause = (DbSetClause)dbModificationClause;
				if (flag)
				{
					flag = false;
				}
				else
				{
					sqlStringBuilder.Append(", ");
				}
				dbSetClause.Property.Accept(expressionTranslator);
				sqlStringBuilder.Append(" = ");
				dbSetClause.Value.Accept(expressionTranslator);
			}
			if (flag)
			{
				sqlStringBuilder.Append("@p = 0");
			}
			sqlStringBuilder.AppendLine();
			sqlStringBuilder.AppendKeyword("where ");
			tree.Predicate.Accept(expressionTranslator);
			sqlStringBuilder.AppendLine();
			if (generateReturningSql)
			{
				DmlSqlGenerator.GenerateReturningSql(sqlStringBuilder, tree, null, expressionTranslator, tree.Returning, false);
			}
			parameters = expressionTranslator.Parameters;
			return sqlStringBuilder.ToString();
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00010DF4 File Offset: 0x0000EFF4
		internal static string GenerateDeleteSql(DbDeleteCommandTree tree, SqlGenerator sqlGenerator, out List<SqlParameter> parameters, bool upperCaseKeywords = true, bool createParameters = true)
		{
			SqlStringBuilder sqlStringBuilder = new SqlStringBuilder(256);
			sqlStringBuilder.UpperCaseKeywords = upperCaseKeywords;
			DmlSqlGenerator.ExpressionTranslator expressionTranslator = new DmlSqlGenerator.ExpressionTranslator(sqlStringBuilder, tree, false, sqlGenerator, null, createParameters);
			sqlStringBuilder.AppendKeyword("delete ");
			tree.Target.Expression.Accept(expressionTranslator);
			sqlStringBuilder.AppendLine();
			sqlStringBuilder.AppendKeyword("where ");
			tree.Predicate.Accept(expressionTranslator);
			parameters = expressionTranslator.Parameters;
			return sqlStringBuilder.ToString();
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00010E68 File Offset: 0x0000F068
		internal static string GenerateInsertSql(DbInsertCommandTree tree, SqlGenerator sqlGenerator, out List<SqlParameter> parameters, bool generateReturningSql = true, bool upperCaseKeywords = true, bool createParameters = true)
		{
			SqlStringBuilder sqlStringBuilder = new SqlStringBuilder(256)
			{
				UpperCaseKeywords = upperCaseKeywords
			};
			DmlSqlGenerator.ExpressionTranslator expressionTranslator = new DmlSqlGenerator.ExpressionTranslator(sqlStringBuilder, tree, tree.Returning != null, sqlGenerator, null, createParameters);
			bool flag = DmlSqlGenerator.UseGeneratedValuesVariable(tree, sqlGenerator.SqlVersion);
			EntityType entityType = (EntityType)((DbScanExpression)tree.Target.Expression).Target.ElementType;
			if (flag)
			{
				sqlStringBuilder.AppendKeyword("declare ").Append("@generated_keys").Append(" table(");
				bool flag2 = true;
				foreach (EdmMember edmMember in entityType.KeyMembers)
				{
					if (flag2)
					{
						flag2 = false;
					}
					else
					{
						sqlStringBuilder.Append(", ");
					}
					sqlStringBuilder.Append(DmlSqlGenerator.GenerateMemberTSql(edmMember)).Append(" ").Append(DmlSqlGenerator.GetVariableType(sqlGenerator, edmMember));
					Facet facet;
					if (edmMember.TypeUsage.Facets.TryGetValue("Collation", false, out facet))
					{
						string text = facet.Value as string;
						if (!string.IsNullOrEmpty(text))
						{
							sqlStringBuilder.AppendKeyword(" collate ").Append(text);
						}
					}
				}
				sqlStringBuilder.AppendLine(")");
			}
			sqlStringBuilder.AppendKeyword("insert ");
			tree.Target.Expression.Accept(expressionTranslator);
			if (0 < tree.SetClauses.Count)
			{
				sqlStringBuilder.Append("(");
				bool flag3 = true;
				foreach (DbModificationClause dbModificationClause in tree.SetClauses)
				{
					DbSetClause dbSetClause = (DbSetClause)dbModificationClause;
					if (flag3)
					{
						flag3 = false;
					}
					else
					{
						sqlStringBuilder.Append(", ");
					}
					dbSetClause.Property.Accept(expressionTranslator);
				}
				sqlStringBuilder.AppendLine(")");
			}
			else
			{
				sqlStringBuilder.AppendLine();
			}
			if (flag)
			{
				sqlStringBuilder.AppendKeyword("output ");
				bool flag4 = true;
				foreach (EdmMember edmMember2 in entityType.KeyMembers)
				{
					if (flag4)
					{
						flag4 = false;
					}
					else
					{
						sqlStringBuilder.Append(", ");
					}
					sqlStringBuilder.Append("inserted.");
					sqlStringBuilder.Append(DmlSqlGenerator.GenerateMemberTSql(edmMember2));
				}
				sqlStringBuilder.AppendKeyword(" into ").AppendLine("@generated_keys");
			}
			if (0 < tree.SetClauses.Count)
			{
				bool flag5 = true;
				sqlStringBuilder.AppendKeyword("values (");
				foreach (DbModificationClause dbModificationClause2 in tree.SetClauses)
				{
					DbSetClause dbSetClause2 = (DbSetClause)dbModificationClause2;
					if (flag5)
					{
						flag5 = false;
					}
					else
					{
						sqlStringBuilder.Append(", ");
					}
					dbSetClause2.Value.Accept(expressionTranslator);
					expressionTranslator.RegisterMemberValue(dbSetClause2.Property, dbSetClause2.Value);
				}
				sqlStringBuilder.AppendLine(")");
			}
			else
			{
				sqlStringBuilder.AppendKeyword("default values");
				sqlStringBuilder.AppendLine();
			}
			if (generateReturningSql)
			{
				DmlSqlGenerator.GenerateReturningSql(sqlStringBuilder, tree, entityType, expressionTranslator, tree.Returning, flag);
			}
			parameters = expressionTranslator.Parameters;
			return sqlStringBuilder.ToString();
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x000111E4 File Offset: 0x0000F3E4
		internal static string GetVariableType(SqlGenerator sqlGenerator, EdmMember column)
		{
			string text = SqlGenerator.GenerateSqlForStoreType(sqlGenerator.SqlVersion, column.TypeUsage);
			if (text == "rowversion" || text == "timestamp")
			{
				text = "binary(8)";
			}
			return text;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00011224 File Offset: 0x0000F424
		internal static bool UseGeneratedValuesVariable(DbInsertCommandTree tree, SqlVersion sqlVersion)
		{
			bool flag = false;
			if (sqlVersion > SqlVersion.Sql8 && tree.Returning != null)
			{
				HashSet<EdmMember> hashSet = new HashSet<EdmMember>(from DbSetClause s in tree.SetClauses
					select ((DbPropertyExpression)s.Property).Property);
				bool flag2 = false;
				foreach (EdmMember edmMember in ((DbScanExpression)tree.Target.Expression).Target.ElementType.KeyMembers)
				{
					if (!hashSet.Contains(edmMember))
					{
						if (flag2)
						{
							flag = true;
							break;
						}
						flag2 = true;
						if (!DmlSqlGenerator.IsValidScopeIdentityColumnType(edmMember.TypeUsage))
						{
							flag = true;
							break;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x000112FC File Offset: 0x0000F4FC
		internal static string GenerateMemberTSql(EdmMember member)
		{
			return SqlGenerator.QuoteIdentifier(member.Name);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0001130C File Offset: 0x0000F50C
		internal static void GenerateReturningSql(SqlStringBuilder commandText, DbModificationCommandTree tree, EntityType tableType, DmlSqlGenerator.ExpressionTranslator translator, DbExpression returning, bool useGeneratedValuesVariable)
		{
			if (returning == null)
			{
				return;
			}
			commandText.AppendKeyword("select ");
			if (useGeneratedValuesVariable)
			{
				translator.PropertyAlias = "t";
			}
			returning.Accept(translator);
			if (useGeneratedValuesVariable)
			{
				translator.PropertyAlias = null;
			}
			commandText.AppendLine();
			if (useGeneratedValuesVariable)
			{
				commandText.AppendKeyword("from ");
				commandText.Append("@generated_keys");
				commandText.AppendKeyword(" as ");
				commandText.Append("g");
				commandText.AppendKeyword(" join ");
				tree.Target.Expression.Accept(translator);
				commandText.AppendKeyword(" as ");
				commandText.Append("t");
				commandText.AppendKeyword(" on ");
				string text = string.Empty;
				foreach (EdmMember edmMember in tableType.KeyMembers)
				{
					commandText.AppendKeyword(text);
					text = " and ";
					commandText.Append("g.");
					string text2 = DmlSqlGenerator.GenerateMemberTSql(edmMember);
					commandText.Append(text2);
					commandText.Append(" = t.");
					commandText.Append(text2);
				}
				commandText.AppendLine();
				commandText.AppendKeyword("where @@ROWCOUNT > 0");
				return;
			}
			commandText.AppendKeyword("from ");
			tree.Target.Expression.Accept(translator);
			commandText.AppendLine();
			commandText.AppendKeyword("where @@ROWCOUNT > 0");
			EntitySetBase target = ((DbScanExpression)tree.Target.Expression).Target;
			bool flag = false;
			foreach (EdmMember edmMember2 in target.ElementType.KeyMembers)
			{
				commandText.AppendKeyword(" and ");
				commandText.Append(DmlSqlGenerator.GenerateMemberTSql(edmMember2));
				commandText.Append(" = ");
				SqlParameter sqlParameter;
				if (translator.MemberValues.TryGetValue(edmMember2, out sqlParameter))
				{
					commandText.Append(sqlParameter.ParameterName);
				}
				else
				{
					if (flag)
					{
						throw new NotSupportedException(Strings.Update_NotSupportedServerGenKey(target.Name));
					}
					if (!DmlSqlGenerator.IsValidScopeIdentityColumnType(edmMember2.TypeUsage))
					{
						throw new InvalidOperationException(Strings.Update_NotSupportedIdentityType(edmMember2.Name, edmMember2.TypeUsage.ToString()));
					}
					commandText.Append("scope_identity()");
					flag = true;
				}
			}
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00011588 File Offset: 0x0000F788
		private static bool IsValidScopeIdentityColumnType(TypeUsage typeUsage)
		{
			if (!SqlProviderServices.UseScopeIdentity)
			{
				return false;
			}
			if (typeUsage.EdmType.BuiltInTypeKind != BuiltInTypeKind.PrimitiveType)
			{
				return false;
			}
			string name = typeUsage.EdmType.Name;
			Facet facet;
			return name == "tinyint" || name == "smallint" || name == "int" || name == "bigint" || ((name == "decimal" || name == "numeric") && typeUsage.Facets.TryGetValue("Scale", false, out facet) && Convert.ToInt32(facet.Value, CultureInfo.InvariantCulture) == 0);
		}

		// Token: 0x040000DA RID: 218
		private const int CommandTextBuilderInitialCapacity = 256;

		// Token: 0x040000DB RID: 219
		private const string GeneratedValuesVariableName = "@generated_keys";

		// Token: 0x02000085 RID: 133
		internal class ExpressionTranslator : BasicExpressionVisitor
		{
			// Token: 0x060006E5 RID: 1765 RVA: 0x0001C3A8 File Offset: 0x0001A5A8
			internal ExpressionTranslator(SqlStringBuilder commandText, DbModificationCommandTree commandTree, bool preserveMemberValues, SqlGenerator sqlGenerator, ICollection<EdmProperty> localVariableBindings = null, bool createParameters = true)
			{
				this._commandText = commandText;
				this._commandTree = commandTree;
				this._sqlGenerator = sqlGenerator;
				this._localVariableBindings = localVariableBindings;
				this._parameters = new List<SqlParameter>();
				this._memberValues = (preserveMemberValues ? new Dictionary<EdmMember, SqlParameter>() : null);
				this._createParameters = createParameters;
			}

			// Token: 0x170000F8 RID: 248
			// (get) Token: 0x060006E6 RID: 1766 RVA: 0x0001C3FD File Offset: 0x0001A5FD
			internal List<SqlParameter> Parameters
			{
				get
				{
					return this._parameters;
				}
			}

			// Token: 0x170000F9 RID: 249
			// (get) Token: 0x060006E7 RID: 1767 RVA: 0x0001C405 File Offset: 0x0001A605
			internal Dictionary<EdmMember, SqlParameter> MemberValues
			{
				get
				{
					return this._memberValues;
				}
			}

			// Token: 0x170000FA RID: 250
			// (get) Token: 0x060006E8 RID: 1768 RVA: 0x0001C40D File Offset: 0x0001A60D
			// (set) Token: 0x060006E9 RID: 1769 RVA: 0x0001C415 File Offset: 0x0001A615
			internal string PropertyAlias { get; set; }

			// Token: 0x060006EA RID: 1770 RVA: 0x0001C420 File Offset: 0x0001A620
			internal SqlParameter CreateParameter(object value, TypeUsage type, string name = null)
			{
				SqlParameter sqlParameter = SqlProviderServices.CreateSqlParameter(name ?? DmlSqlGenerator.ExpressionTranslator.GetParameterName(this._parameters.Count), type, ParameterMode.In, value, true, this._sqlGenerator.SqlVersion);
				this._parameters.Add(sqlParameter);
				return sqlParameter;
			}

			// Token: 0x060006EB RID: 1771 RVA: 0x0001C464 File Offset: 0x0001A664
			internal static string GetParameterName(int index)
			{
				return "@" + index.ToString(CultureInfo.InvariantCulture);
			}

			// Token: 0x060006EC RID: 1772 RVA: 0x0001C47C File Offset: 0x0001A67C
			public override void Visit(DbAndExpression expression)
			{
				Check.NotNull<DbAndExpression>(expression, "expression");
				this.VisitBinary(expression, " and ");
			}

			// Token: 0x060006ED RID: 1773 RVA: 0x0001C496 File Offset: 0x0001A696
			public override void Visit(DbOrExpression expression)
			{
				Check.NotNull<DbOrExpression>(expression, "expression");
				this.VisitBinary(expression, " or ");
			}

			// Token: 0x060006EE RID: 1774 RVA: 0x0001C4B0 File Offset: 0x0001A6B0
			public override void Visit(DbComparisonExpression expression)
			{
				Check.NotNull<DbComparisonExpression>(expression, "expression");
				this.VisitBinary(expression, " = ");
				this.RegisterMemberValue(expression.Left, expression.Right);
			}

			// Token: 0x060006EF RID: 1775 RVA: 0x0001C4DC File Offset: 0x0001A6DC
			internal void RegisterMemberValue(DbExpression propertyExpression, DbExpression value)
			{
				if (this._memberValues != null)
				{
					EdmMember property = ((DbPropertyExpression)propertyExpression).Property;
					if (value.ExpressionKind != DbExpressionKind.Null)
					{
						this._memberValues[property] = this._parameters[this._parameters.Count - 1];
					}
				}
			}

			// Token: 0x060006F0 RID: 1776 RVA: 0x0001C52B File Offset: 0x0001A72B
			public override void Visit(DbIsNullExpression expression)
			{
				Check.NotNull<DbIsNullExpression>(expression, "expression");
				expression.Argument.Accept(this);
				this._commandText.AppendKeyword(" is null");
			}

			// Token: 0x060006F1 RID: 1777 RVA: 0x0001C556 File Offset: 0x0001A756
			public override void Visit(DbNotExpression expression)
			{
				Check.NotNull<DbNotExpression>(expression, "expression");
				this._commandText.AppendKeyword("not (");
				expression.Argument.Accept(this);
				this._commandText.Append(")");
			}

			// Token: 0x060006F2 RID: 1778 RVA: 0x0001C594 File Offset: 0x0001A794
			public override void Visit(DbConstantExpression expression)
			{
				Check.NotNull<DbConstantExpression>(expression, "expression");
				SqlParameter sqlParameter = this.CreateParameter(expression.Value, expression.ResultType, null);
				if (this._createParameters)
				{
					this._commandText.Append(sqlParameter.ParameterName);
					return;
				}
				using (SqlWriter sqlWriter = new SqlWriter(this._commandText.InnerBuilder))
				{
					this._sqlGenerator.WriteSql(sqlWriter, expression.Accept<ISqlFragment>(this._sqlGenerator));
				}
			}

			// Token: 0x060006F3 RID: 1779 RVA: 0x0001C624 File Offset: 0x0001A824
			public override void Visit(DbParameterReferenceExpression expression)
			{
				Check.NotNull<DbParameterReferenceExpression>(expression, "expression");
				SqlParameter sqlParameter = this.CreateParameter(DBNull.Value, expression.ResultType, "@" + expression.ParameterName);
				this._commandText.Append(sqlParameter.ParameterName);
			}

			// Token: 0x060006F4 RID: 1780 RVA: 0x0001C674 File Offset: 0x0001A874
			public override void Visit(DbScanExpression expression)
			{
				Check.NotNull<DbScanExpression>(expression, "expression");
				if (expression.Target.GetMetadataPropertyValue("DefiningQuery") != null)
				{
					string text;
					if (this._commandTree is DbDeleteCommandTree)
					{
						text = "DeleteFunction";
					}
					else if (this._commandTree is DbInsertCommandTree)
					{
						text = "InsertFunction";
					}
					else
					{
						text = "UpdateFunction";
					}
					throw new UpdateException(Strings.Update_SqlEntitySetWithoutDmlFunctions(expression.Target.Name, text, "ModificationFunctionMapping"));
				}
				this._commandText.Append(SqlGenerator.GetTargetTSql(expression.Target));
			}

			// Token: 0x060006F5 RID: 1781 RVA: 0x0001C704 File Offset: 0x0001A904
			public override void Visit(DbPropertyExpression expression)
			{
				Check.NotNull<DbPropertyExpression>(expression, "expression");
				if (!string.IsNullOrEmpty(this.PropertyAlias))
				{
					this._commandText.Append(this.PropertyAlias);
					this._commandText.Append(".");
				}
				this._commandText.Append(DmlSqlGenerator.GenerateMemberTSql(expression.Property));
			}

			// Token: 0x060006F6 RID: 1782 RVA: 0x0001C764 File Offset: 0x0001A964
			public override void Visit(DbNullExpression expression)
			{
				Check.NotNull<DbNullExpression>(expression, "expression");
				this._commandText.AppendKeyword("null");
			}

			// Token: 0x060006F7 RID: 1783 RVA: 0x0001C784 File Offset: 0x0001A984
			public override void Visit(DbNewInstanceExpression expression)
			{
				Check.NotNull<DbNewInstanceExpression>(expression, "expression");
				bool flag = true;
				foreach (DbExpression dbExpression in expression.Arguments)
				{
					EdmMember property = ((DbPropertyExpression)dbExpression).Property;
					string text = ((this._localVariableBindings != null) ? (this._localVariableBindings.Contains(property) ? ("@" + property.Name + " = ") : null) : string.Empty);
					if (text != null)
					{
						if (flag)
						{
							flag = false;
						}
						else
						{
							this._commandText.Append(", ");
						}
						this._commandText.Append(text);
						dbExpression.Accept(this);
					}
				}
			}

			// Token: 0x060006F8 RID: 1784 RVA: 0x0001C850 File Offset: 0x0001AA50
			private void VisitBinary(DbBinaryExpression expression, string separator)
			{
				this._commandText.Append("(");
				expression.Left.Accept(this);
				this._commandText.AppendKeyword(separator);
				expression.Right.Accept(this);
				this._commandText.Append(")");
			}

			// Token: 0x04000214 RID: 532
			private readonly SqlStringBuilder _commandText;

			// Token: 0x04000215 RID: 533
			private readonly DbModificationCommandTree _commandTree;

			// Token: 0x04000216 RID: 534
			private readonly List<SqlParameter> _parameters;

			// Token: 0x04000217 RID: 535
			private readonly Dictionary<EdmMember, SqlParameter> _memberValues;

			// Token: 0x04000218 RID: 536
			private readonly SqlGenerator _sqlGenerator;

			// Token: 0x04000219 RID: 537
			private readonly ICollection<EdmProperty> _localVariableBindings;

			// Token: 0x0400021A RID: 538
			private readonly bool _createParameters;
		}
	}
}
