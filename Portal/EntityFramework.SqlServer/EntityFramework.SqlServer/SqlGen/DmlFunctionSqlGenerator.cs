using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x0200002D RID: 45
	internal class DmlFunctionSqlGenerator
	{
		// Token: 0x06000447 RID: 1095 RVA: 0x000106AE File Offset: 0x0000E8AE
		public DmlFunctionSqlGenerator(SqlGenerator sqlGenerator)
		{
			this._sqlGenerator = sqlGenerator;
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x000106C0 File Offset: 0x0000E8C0
		public string GenerateInsert(ICollection<DbInsertCommandTree> commandTrees)
		{
			StringBuilder stringBuilder = new StringBuilder();
			DbInsertCommandTree dbInsertCommandTree = commandTrees.First<DbInsertCommandTree>();
			List<SqlParameter> list;
			stringBuilder.Append(DmlSqlGenerator.GenerateInsertSql(dbInsertCommandTree, this._sqlGenerator, out list, false, true, false));
			stringBuilder.AppendLine();
			EntityType entityType = (EntityType)((DbScanExpression)dbInsertCommandTree.Target.Expression).Target.ElementType;
			stringBuilder.Append(this.IntroduceRequiredLocalVariables(entityType, dbInsertCommandTree));
			foreach (DbInsertCommandTree dbInsertCommandTree2 in commandTrees.Skip(1))
			{
				stringBuilder.Append(DmlSqlGenerator.GenerateInsertSql(dbInsertCommandTree2, this._sqlGenerator, out list, false, true, false));
				stringBuilder.AppendLine();
			}
			List<DbInsertCommandTree> list2 = commandTrees.Where((DbInsertCommandTree ct) => ct.Returning != null).ToList<DbInsertCommandTree>();
			if (list2.Any<DbInsertCommandTree>())
			{
				DmlFunctionSqlGenerator.ReturningSelectSqlGenerator returningSelectSqlGenerator = new DmlFunctionSqlGenerator.ReturningSelectSqlGenerator();
				foreach (DbInsertCommandTree dbInsertCommandTree3 in list2)
				{
					dbInsertCommandTree3.Target.Expression.Accept(returningSelectSqlGenerator);
					dbInsertCommandTree3.Returning.Accept(returningSelectSqlGenerator);
				}
				using (ReadOnlyMetadataCollection<EdmProperty>.Enumerator enumerator3 = entityType.KeyProperties.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						EdmProperty keyProperty = enumerator3.Current;
						DbExpression dbExpression = (from DbSetClause sc in dbInsertCommandTree.SetClauses
							where ((DbPropertyExpression)sc.Property).Property == keyProperty
							select sc.Value).SingleOrDefault<DbExpression>() ?? keyProperty.TypeUsage.Parameter(keyProperty.Name);
						dbInsertCommandTree.Target.Variable.Property(keyProperty).Equal(dbExpression).Accept(returningSelectSqlGenerator);
					}
				}
				stringBuilder.Append(returningSelectSqlGenerator.Sql);
			}
			return stringBuilder.ToString().TrimEnd(new char[0]);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00010918 File Offset: 0x0000EB18
		private string IntroduceRequiredLocalVariables(EntityType entityType, DbInsertCommandTree commandTree)
		{
			List<EdmProperty> list = entityType.KeyProperties.Where((EdmProperty p) => p.IsStoreGeneratedIdentity).ToList<EdmProperty>();
			SqlStringBuilder sqlStringBuilder = new SqlStringBuilder
			{
				UpperCaseKeywords = true
			};
			if (list.Any<EdmProperty>())
			{
				foreach (EdmProperty edmProperty in list)
				{
					sqlStringBuilder.Append((sqlStringBuilder.Length == 0) ? "DECLARE " : ", ");
					sqlStringBuilder.Append("@");
					sqlStringBuilder.Append(edmProperty.Name);
					sqlStringBuilder.Append(" ");
					sqlStringBuilder.Append(DmlSqlGenerator.GetVariableType(this._sqlGenerator, edmProperty));
				}
				sqlStringBuilder.AppendLine();
				DmlSqlGenerator.ExpressionTranslator expressionTranslator = new DmlSqlGenerator.ExpressionTranslator(sqlStringBuilder, commandTree, true, this._sqlGenerator, entityType.KeyProperties, true);
				DmlSqlGenerator.GenerateReturningSql(sqlStringBuilder, commandTree, entityType, expressionTranslator, commandTree.Returning, DmlSqlGenerator.UseGeneratedValuesVariable(commandTree, this._sqlGenerator.SqlVersion));
				sqlStringBuilder.AppendLine();
				sqlStringBuilder.AppendLine();
			}
			return sqlStringBuilder.ToString();
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00010A50 File Offset: 0x0000EC50
		public string GenerateUpdate(ICollection<DbUpdateCommandTree> commandTrees, string rowsAffectedParameter)
		{
			if (!commandTrees.Any<DbUpdateCommandTree>())
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			List<SqlParameter> list;
			stringBuilder.AppendLine(DmlSqlGenerator.GenerateUpdateSql(commandTrees.First<DbUpdateCommandTree>(), this._sqlGenerator, out list, false, true));
			foreach (DbUpdateCommandTree dbUpdateCommandTree in commandTrees.Skip(1))
			{
				stringBuilder.Append(DmlSqlGenerator.GenerateUpdateSql(dbUpdateCommandTree, this._sqlGenerator, out list, false, true));
				stringBuilder.AppendLine("AND @@ROWCOUNT > 0");
				stringBuilder.AppendLine();
			}
			List<DbUpdateCommandTree> list2 = commandTrees.Where((DbUpdateCommandTree ct) => ct.Returning != null).ToList<DbUpdateCommandTree>();
			if (list2.Any<DbUpdateCommandTree>())
			{
				DmlFunctionSqlGenerator.ReturningSelectSqlGenerator returningSelectSqlGenerator = new DmlFunctionSqlGenerator.ReturningSelectSqlGenerator();
				foreach (DbUpdateCommandTree dbUpdateCommandTree2 in list2)
				{
					dbUpdateCommandTree2.Target.Expression.Accept(returningSelectSqlGenerator);
					dbUpdateCommandTree2.Returning.Accept(returningSelectSqlGenerator);
					dbUpdateCommandTree2.Predicate.Accept(returningSelectSqlGenerator);
				}
				stringBuilder.AppendLine(returningSelectSqlGenerator.Sql);
				stringBuilder.AppendLine();
			}
			DmlFunctionSqlGenerator.AppendSetRowsAffected(stringBuilder, rowsAffectedParameter);
			return stringBuilder.ToString().TrimEnd(new char[0]);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00010BB8 File Offset: 0x0000EDB8
		public string GenerateDelete(ICollection<DbDeleteCommandTree> commandTrees, string rowsAffectedParameter)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<SqlParameter> list;
			stringBuilder.AppendLine(DmlSqlGenerator.GenerateDeleteSql(commandTrees.First<DbDeleteCommandTree>(), this._sqlGenerator, out list, true, true));
			stringBuilder.AppendLine();
			foreach (DbDeleteCommandTree dbDeleteCommandTree in commandTrees.Skip(1))
			{
				stringBuilder.AppendLine(DmlSqlGenerator.GenerateDeleteSql(dbDeleteCommandTree, this._sqlGenerator, out list, true, true));
				stringBuilder.AppendLine("AND @@ROWCOUNT > 0");
				stringBuilder.AppendLine();
			}
			DmlFunctionSqlGenerator.AppendSetRowsAffected(stringBuilder, rowsAffectedParameter);
			return stringBuilder.ToString().TrimEnd(new char[0]);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00010C6C File Offset: 0x0000EE6C
		private static void AppendSetRowsAffected(StringBuilder sql, string rowsAffectedParameter)
		{
			if (!string.IsNullOrWhiteSpace(rowsAffectedParameter))
			{
				sql.Append("SET @");
				sql.Append(rowsAffectedParameter);
				sql.AppendLine(" = @@ROWCOUNT");
				sql.AppendLine();
			}
		}

		// Token: 0x040000D9 RID: 217
		private readonly SqlGenerator _sqlGenerator;

		// Token: 0x02000082 RID: 130
		private sealed class ReturningSelectSqlGenerator : BasicExpressionVisitor
		{
			// Token: 0x170000F7 RID: 247
			// (get) Token: 0x060006D5 RID: 1749 RVA: 0x0001BFA0 File Offset: 0x0001A1A0
			public string Sql
			{
				get
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendLine(this._select.ToString());
					stringBuilder.AppendLine(this._from.ToString());
					stringBuilder.Append("WHERE @@ROWCOUNT > 0");
					stringBuilder.Append(this._where);
					return stringBuilder.ToString();
				}
			}

			// Token: 0x060006D6 RID: 1750 RVA: 0x0001BFF4 File Offset: 0x0001A1F4
			public override void Visit(DbNewInstanceExpression newInstanceExpression)
			{
				ReadOnlyMetadataCollection<EdmProperty> properties = ((RowType)newInstanceExpression.ResultType.EdmType).Properties;
				for (int i = 0; i < properties.Count; i++)
				{
					this._select.Append((this._select.Length == 0) ? "SELECT " : ", ");
					this._nextPropertyAlias = properties[i].Name;
					newInstanceExpression.Arguments[i].Accept(this);
				}
				this._nextPropertyAlias = null;
			}

			// Token: 0x060006D7 RID: 1751 RVA: 0x0001C078 File Offset: 0x0001A278
			public override void Visit(DbScanExpression scanExpression)
			{
				string targetTSql = SqlGenerator.GetTargetTSql(scanExpression.Target);
				string text = " AS ";
				string text2 = "t";
				int aliasCount = this._aliasCount;
				this._aliasCount = aliasCount + 1;
				string text3 = targetTSql + text + (this._currentTableAlias = text2 + aliasCount.ToString());
				EntityTypeBase elementType = scanExpression.Target.ElementType;
				if (this._from.Length == 0)
				{
					this._baseTable = (EntityType)elementType;
					this._from.Append("FROM ");
					this._from.Append(text3);
					return;
				}
				this._from.AppendLine();
				this._from.Append("JOIN ");
				this._from.Append(text3);
				this._from.Append(" ON ");
				for (int i = 0; i < elementType.KeyMembers.Count; i++)
				{
					if (i > 0)
					{
						this._from.Append(" AND ");
					}
					this._from.Append(this._currentTableAlias + ".");
					this._from.Append(SqlGenerator.QuoteIdentifier(elementType.KeyMembers[i].Name));
					this._from.Append(" = t0.");
					this._from.Append(SqlGenerator.QuoteIdentifier(this._baseTable.KeyMembers[i].Name));
				}
			}

			// Token: 0x060006D8 RID: 1752 RVA: 0x0001C1F4 File Offset: 0x0001A3F4
			public override void Visit(DbPropertyExpression propertyExpression)
			{
				this._select.Append(this._currentTableAlias);
				this._select.Append(".");
				this._select.Append(SqlGenerator.QuoteIdentifier(propertyExpression.Property.Name));
				if (!string.IsNullOrWhiteSpace(this._nextPropertyAlias) && !string.Equals(this._nextPropertyAlias, propertyExpression.Property.Name, StringComparison.Ordinal))
				{
					this._select.Append(" AS ");
					this._select.Append(this._nextPropertyAlias);
				}
			}

			// Token: 0x060006D9 RID: 1753 RVA: 0x0001C289 File Offset: 0x0001A489
			public override void Visit(DbParameterReferenceExpression expression)
			{
				this._where.Append("@" + expression.ParameterName);
			}

			// Token: 0x060006DA RID: 1754 RVA: 0x0001C2A7 File Offset: 0x0001A4A7
			public override void Visit(DbIsNullExpression expression)
			{
			}

			// Token: 0x060006DB RID: 1755 RVA: 0x0001C2AC File Offset: 0x0001A4AC
			public override void Visit(DbComparisonExpression comparisonExpression)
			{
				EdmMember property = ((DbPropertyExpression)comparisonExpression.Left).Property;
				if (this._baseTable.KeyMembers.Contains(property))
				{
					this._where.Append(" AND t0.");
					this._where.Append(SqlGenerator.QuoteIdentifier(property.Name));
					this._where.Append(" = ");
					comparisonExpression.Right.Accept(this);
				}
			}

			// Token: 0x04000207 RID: 519
			private readonly StringBuilder _select = new StringBuilder();

			// Token: 0x04000208 RID: 520
			private readonly StringBuilder _from = new StringBuilder();

			// Token: 0x04000209 RID: 521
			private readonly StringBuilder _where = new StringBuilder();

			// Token: 0x0400020A RID: 522
			private int _aliasCount;

			// Token: 0x0400020B RID: 523
			private string _currentTableAlias;

			// Token: 0x0400020C RID: 524
			private EntityType _baseTable;

			// Token: 0x0400020D RID: 525
			private string _nextPropertyAlias;
		}
	}
}
