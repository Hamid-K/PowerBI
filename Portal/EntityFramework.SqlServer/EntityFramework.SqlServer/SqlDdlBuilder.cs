using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.SqlServer.Utilities;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x0200000C RID: 12
	internal sealed class SqlDdlBuilder
	{
		// Token: 0x0600003F RID: 63 RVA: 0x000026D0 File Offset: 0x000008D0
		internal static string CreateObjectsScript(StoreItemCollection itemCollection, bool createSchemas)
		{
			SqlDdlBuilder sqlDdlBuilder = new SqlDdlBuilder();
			foreach (EntityContainer entityContainer in itemCollection.GetItems<EntityContainer>())
			{
				IOrderedEnumerable<EntitySet> orderedEnumerable = from s in entityContainer.BaseEntitySets.OfType<EntitySet>()
					orderby s.Name
					select s;
				if (createSchemas)
				{
					foreach (string text in new HashSet<string>(orderedEnumerable.Select((EntitySet s) => SqlDdlBuilder.GetSchemaName(s))).OrderBy((string s) => s))
					{
						if (text != "dbo")
						{
							sqlDdlBuilder.AppendCreateSchema(text);
						}
					}
				}
				foreach (EntitySet entitySet in from s in entityContainer.BaseEntitySets.OfType<EntitySet>()
					orderby s.Name
					select s)
				{
					sqlDdlBuilder.AppendCreateTable(entitySet);
				}
				foreach (AssociationSet associationSet in from s in entityContainer.BaseEntitySets.OfType<AssociationSet>()
					orderby s.Name
					select s)
				{
					sqlDdlBuilder.AppendCreateForeignKeys(associationSet);
				}
			}
			return sqlDdlBuilder.GetCommandText();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002900 File Offset: 0x00000B00
		internal static string CreateDatabaseScript(string databaseName, string dataFileName, string logFileName)
		{
			SqlDdlBuilder sqlDdlBuilder = new SqlDdlBuilder();
			sqlDdlBuilder.AppendSql("create database ");
			sqlDdlBuilder.AppendIdentifier(databaseName);
			if (dataFileName != null)
			{
				sqlDdlBuilder.AppendSql(" on primary ");
				sqlDdlBuilder.AppendFileName(dataFileName);
				sqlDdlBuilder.AppendSql(" log on ");
				sqlDdlBuilder.AppendFileName(logFileName);
			}
			return sqlDdlBuilder.unencodedStringBuilder.ToString();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002957 File Offset: 0x00000B57
		internal static string SetDatabaseOptionsScript(SqlVersion sqlVersion, string databaseName)
		{
			if (sqlVersion < SqlVersion.Sql9)
			{
				return string.Empty;
			}
			SqlDdlBuilder sqlDdlBuilder = new SqlDdlBuilder();
			sqlDdlBuilder.AppendSql("if serverproperty('EngineEdition') <> 5 execute sp_executesql ");
			sqlDdlBuilder.AppendStringLiteral(SqlDdlBuilder.SetReadCommittedSnapshotScript(databaseName));
			return sqlDdlBuilder.unencodedStringBuilder.ToString();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000298A File Offset: 0x00000B8A
		private static string SetReadCommittedSnapshotScript(string databaseName)
		{
			SqlDdlBuilder sqlDdlBuilder = new SqlDdlBuilder();
			sqlDdlBuilder.AppendSql("alter database ");
			sqlDdlBuilder.AppendIdentifier(databaseName);
			sqlDdlBuilder.AppendSql(" set read_committed_snapshot on");
			return sqlDdlBuilder.unencodedStringBuilder.ToString();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000029B8 File Offset: 0x00000BB8
		internal static string CreateDatabaseExistsScript(string databaseName)
		{
			SqlDdlBuilder sqlDdlBuilder = new SqlDdlBuilder();
			sqlDdlBuilder.AppendSql("IF db_id(");
			sqlDdlBuilder.AppendStringLiteral(databaseName);
			sqlDdlBuilder.AppendSql(") IS NOT NULL SELECT 1 ELSE SELECT Count(*) FROM sys.databases WHERE [name]=");
			sqlDdlBuilder.AppendStringLiteral(databaseName);
			return sqlDdlBuilder.unencodedStringBuilder.ToString();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000029ED File Offset: 0x00000BED
		private static void AppendSysDatabases(SqlDdlBuilder builder, bool useDeprecatedSystemTable)
		{
			if (useDeprecatedSystemTable)
			{
				builder.AppendSql("sysdatabases");
				return;
			}
			builder.AppendSql("sys.databases");
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002A0C File Offset: 0x00000C0C
		internal static string CreateGetDatabaseNamesBasedOnFileNameScript(string databaseFileName, bool useDeprecatedSystemTable)
		{
			SqlDdlBuilder sqlDdlBuilder = new SqlDdlBuilder();
			sqlDdlBuilder.AppendSql("SELECT [d].[name] FROM ");
			SqlDdlBuilder.AppendSysDatabases(sqlDdlBuilder, useDeprecatedSystemTable);
			sqlDdlBuilder.AppendSql(" AS [d] ");
			if (!useDeprecatedSystemTable)
			{
				sqlDdlBuilder.AppendSql("INNER JOIN sys.master_files AS [f] ON [f].[database_id] = [d].[database_id]");
			}
			sqlDdlBuilder.AppendSql(" WHERE [");
			if (useDeprecatedSystemTable)
			{
				sqlDdlBuilder.AppendSql("filename");
			}
			else
			{
				sqlDdlBuilder.AppendSql("f].[physical_name");
			}
			sqlDdlBuilder.AppendSql("]=");
			sqlDdlBuilder.AppendStringLiteral(databaseFileName);
			return sqlDdlBuilder.unencodedStringBuilder.ToString();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002A90 File Offset: 0x00000C90
		internal static string CreateCountDatabasesBasedOnFileNameScript(string databaseFileName, bool useDeprecatedSystemTable)
		{
			SqlDdlBuilder sqlDdlBuilder = new SqlDdlBuilder();
			sqlDdlBuilder.AppendSql("SELECT Count(*) FROM ");
			if (useDeprecatedSystemTable)
			{
				sqlDdlBuilder.AppendSql("sysdatabases");
			}
			if (!useDeprecatedSystemTable)
			{
				sqlDdlBuilder.AppendSql("sys.master_files");
			}
			sqlDdlBuilder.AppendSql(" WHERE [");
			if (useDeprecatedSystemTable)
			{
				sqlDdlBuilder.AppendSql("filename");
			}
			else
			{
				sqlDdlBuilder.AppendSql("physical_name");
			}
			sqlDdlBuilder.AppendSql("]=");
			sqlDdlBuilder.AppendStringLiteral(databaseFileName);
			return sqlDdlBuilder.unencodedStringBuilder.ToString();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B0D File Offset: 0x00000D0D
		internal static string DropDatabaseScript(string databaseName)
		{
			SqlDdlBuilder sqlDdlBuilder = new SqlDdlBuilder();
			sqlDdlBuilder.AppendSql("drop database ");
			sqlDdlBuilder.AppendIdentifier(databaseName);
			return sqlDdlBuilder.unencodedStringBuilder.ToString();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002B30 File Offset: 0x00000D30
		internal string GetCommandText()
		{
			return this.unencodedStringBuilder.ToString();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002B3D File Offset: 0x00000D3D
		internal static string GetSchemaName(EntitySet entitySet)
		{
			return entitySet.GetMetadataPropertyValue("Schema") ?? entitySet.EntityContainer.Name;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002B59 File Offset: 0x00000D59
		internal static string GetTableName(EntitySet entitySet)
		{
			return entitySet.GetMetadataPropertyValue("Table") ?? entitySet.Name;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002B70 File Offset: 0x00000D70
		private void AppendCreateForeignKeys(AssociationSet associationSet)
		{
			ReferentialConstraint referentialConstraint = associationSet.ElementType.ReferentialConstraints.Single<ReferentialConstraint>();
			AssociationSetEnd associationSetEnd = associationSet.AssociationSetEnds[referentialConstraint.FromRole.Name];
			AssociationSetEnd associationSetEnd2 = associationSet.AssociationSetEnds[referentialConstraint.ToRole.Name];
			if (this.ignoredEntitySets.Contains(associationSetEnd.EntitySet) || this.ignoredEntitySets.Contains(associationSetEnd2.EntitySet))
			{
				this.AppendSql("-- Ignoring association set with participating entity set with defining query: ");
				this.AppendIdentifierEscapeNewLine(associationSet.Name);
			}
			else
			{
				this.AppendSql("alter table ");
				this.AppendIdentifier(associationSetEnd2.EntitySet);
				this.AppendSql(" add constraint ");
				this.AppendIdentifier(associationSet.Name);
				this.AppendSql(" foreign key (");
				this.AppendIdentifiers(referentialConstraint.ToProperties);
				this.AppendSql(") references ");
				this.AppendIdentifier(associationSetEnd.EntitySet);
				this.AppendSql("(");
				this.AppendIdentifiers(referentialConstraint.FromProperties);
				this.AppendSql(")");
				if (associationSetEnd.CorrespondingAssociationEndMember.DeleteBehavior == OperationAction.Cascade)
				{
					this.AppendSql(" on delete cascade");
				}
				this.AppendSql(";");
			}
			this.AppendNewLine();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002CA8 File Offset: 0x00000EA8
		private void AppendCreateTable(EntitySet entitySet)
		{
			if (entitySet.GetMetadataPropertyValue("DefiningQuery") != null)
			{
				this.AppendSql("-- Ignoring entity set with defining query: ");
				this.AppendIdentifier(entitySet, new Action<string>(this.AppendIdentifierEscapeNewLine));
				this.ignoredEntitySets.Add(entitySet);
			}
			else
			{
				this.AppendSql("create table ");
				this.AppendIdentifier(entitySet);
				this.AppendSql(" (");
				this.AppendNewLine();
				foreach (EdmProperty edmProperty in entitySet.ElementType.Properties)
				{
					this.AppendSql("    ");
					this.AppendIdentifier(edmProperty.Name);
					this.AppendSql(" ");
					this.AppendType(edmProperty);
					this.AppendSql(",");
					this.AppendNewLine();
				}
				this.AppendSql("    primary key (");
				this.AppendJoin<EdmMember>(entitySet.ElementType.KeyMembers, delegate(EdmMember k)
				{
					this.AppendIdentifier(k.Name);
				}, ", ");
				this.AppendSql(")");
				this.AppendNewLine();
				this.AppendSql(");");
			}
			this.AppendNewLine();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002DE4 File Offset: 0x00000FE4
		private void AppendCreateSchema(string schema)
		{
			this.AppendSql("if (schema_id(");
			this.AppendStringLiteral(schema);
			this.AppendSql(") is null) exec(");
			SqlDdlBuilder sqlDdlBuilder = new SqlDdlBuilder();
			sqlDdlBuilder.AppendSql("create schema ");
			sqlDdlBuilder.AppendIdentifier(schema);
			this.AppendStringLiteral(sqlDdlBuilder.unencodedStringBuilder.ToString());
			this.AppendSql(");");
			this.AppendNewLine();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002E48 File Offset: 0x00001048
		private void AppendIdentifier(EntitySet table)
		{
			this.AppendIdentifier(table, new Action<string>(this.AppendIdentifier));
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002E60 File Offset: 0x00001060
		private void AppendIdentifier(EntitySet table, Action<string> AppendIdentifierEscape)
		{
			string schemaName = SqlDdlBuilder.GetSchemaName(table);
			string tableName = SqlDdlBuilder.GetTableName(table);
			if (schemaName != null)
			{
				AppendIdentifierEscape(schemaName);
				this.AppendSql(".");
			}
			AppendIdentifierEscape(tableName);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002E97 File Offset: 0x00001097
		private void AppendStringLiteral(string literalValue)
		{
			this.AppendSql("N'" + literalValue.Replace("'", "''") + "'");
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002EBE File Offset: 0x000010BE
		private void AppendIdentifiers(IEnumerable<EdmProperty> properties)
		{
			this.AppendJoin<EdmProperty>(properties, delegate(EdmProperty p)
			{
				this.AppendIdentifier(p.Name);
			}, ", ");
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002ED8 File Offset: 0x000010D8
		private void AppendIdentifier(string identifier)
		{
			this.AppendSql("[" + identifier.Replace("]", "]]") + "]");
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002EFF File Offset: 0x000010FF
		private void AppendIdentifierEscapeNewLine(string identifier)
		{
			this.AppendIdentifier(identifier.Replace("\r", "\r--").Replace("\n", "\n--"));
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002F26 File Offset: 0x00001126
		private void AppendFileName(string path)
		{
			this.AppendSql("(name=");
			this.AppendStringLiteral(Path.GetFileName(path));
			this.AppendSql(", filename=");
			this.AppendStringLiteral(path);
			this.AppendSql(")");
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002F5C File Offset: 0x0000115C
		private void AppendJoin<T>(IEnumerable<T> elements, Action<T> appendElement, string unencodedSeparator)
		{
			bool flag = true;
			foreach (T t in elements)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					this.AppendSql(unencodedSeparator);
				}
				appendElement(t);
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002FB4 File Offset: 0x000011B4
		private void AppendType(EdmProperty column)
		{
			TypeUsage typeUsage = column.TypeUsage;
			bool flag = false;
			Facet facet;
			if (typeUsage.EdmType.Name == "binary" && 8 == typeUsage.GetMaxLength() && column.TypeUsage.Facets.TryGetValue("StoreGeneratedPattern", false, out facet) && facet.Value != null && StoreGeneratedPattern.Computed == (StoreGeneratedPattern)facet.Value)
			{
				flag = true;
				this.AppendIdentifier("rowversion");
			}
			else
			{
				string name = typeUsage.EdmType.Name;
				if (typeUsage.EdmType.BuiltInTypeKind == BuiltInTypeKind.PrimitiveType && name.EndsWith("(max)", StringComparison.Ordinal))
				{
					this.AppendIdentifier(name.Substring(0, name.Length - "(max)".Length));
					this.AppendSql("(max)");
				}
				else
				{
					this.AppendIdentifier(name);
				}
				string name2 = typeUsage.EdmType.Name;
				if (name2 != null)
				{
					uint num = <PrivateImplementationDetails>.ComputeStringHash(name2);
					if (num <= 1761125480U)
					{
						if (num <= 923440646U)
						{
							if (num != 520654156U)
							{
								if (num != 923440646U)
								{
									goto IL_02E5;
								}
								if (!(name2 == "datetime2"))
								{
									goto IL_02E5;
								}
								goto IL_02A5;
							}
							else if (!(name2 == "decimal"))
							{
								goto IL_02E5;
							}
						}
						else if (num != 1539863742U)
						{
							if (num != 1564253156U)
							{
								if (num != 1761125480U)
								{
									goto IL_02E5;
								}
								if (!(name2 == "numeric"))
								{
									goto IL_02E5;
								}
							}
							else
							{
								if (!(name2 == "time"))
								{
									goto IL_02E5;
								}
								goto IL_02A5;
							}
						}
						else
						{
							if (!(name2 == "nvarchar"))
							{
								goto IL_02E5;
							}
							goto IL_02C6;
						}
						this.AppendSqlInvariantFormat("({0}, {1})", new object[]
						{
							typeUsage.GetPrecision(),
							typeUsage.GetScale()
						});
						goto IL_02E5;
					}
					if (num <= 3347933383U)
					{
						if (num != 2336348659U)
						{
							if (num != 2823553821U)
							{
								if (num != 3347933383U)
								{
									goto IL_02E5;
								}
								if (!(name2 == "varbinary"))
								{
									goto IL_02E5;
								}
								goto IL_02C6;
							}
							else
							{
								if (!(name2 == "char"))
								{
									goto IL_02E5;
								}
								goto IL_02C6;
							}
						}
						else if (!(name2 == "datetimeoffset"))
						{
							goto IL_02E5;
						}
					}
					else if (num != 3716508924U)
					{
						if (num != 3761451113U)
						{
							if (num != 4163743794U)
							{
								goto IL_02E5;
							}
							if (!(name2 == "varchar"))
							{
								goto IL_02E5;
							}
							goto IL_02C6;
						}
						else
						{
							if (!(name2 == "nchar"))
							{
								goto IL_02E5;
							}
							goto IL_02C6;
						}
					}
					else
					{
						if (!(name2 == "binary"))
						{
							goto IL_02E5;
						}
						goto IL_02C6;
					}
					IL_02A5:
					this.AppendSqlInvariantFormat("({0})", new object[] { typeUsage.GetPrecision() });
					goto IL_02E5;
					IL_02C6:
					this.AppendSqlInvariantFormat("({0})", new object[] { typeUsage.GetMaxLength() });
				}
			}
			IL_02E5:
			this.AppendSql(column.Nullable ? " null" : " not null");
			if (!flag && column.TypeUsage.Facets.TryGetValue("StoreGeneratedPattern", false, out facet) && facet.Value != null && (StoreGeneratedPattern)facet.Value == StoreGeneratedPattern.Identity)
			{
				if (typeUsage.EdmType.Name == "uniqueidentifier")
				{
					this.AppendSql(" default newid()");
					return;
				}
				this.AppendSql(" identity");
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003321 File Offset: 0x00001521
		private void AppendSql(string text)
		{
			this.unencodedStringBuilder.Append(text);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003330 File Offset: 0x00001530
		private void AppendNewLine()
		{
			this.unencodedStringBuilder.Append("\r\n");
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003343 File Offset: 0x00001543
		private void AppendSqlInvariantFormat(string format, params object[] args)
		{
			this.unencodedStringBuilder.AppendFormat(CultureInfo.InvariantCulture, format, args);
		}

		// Token: 0x04000008 RID: 8
		private readonly StringBuilder unencodedStringBuilder = new StringBuilder();

		// Token: 0x04000009 RID: 9
		private readonly HashSet<EntitySet> ignoredEntitySets = new HashSet<EntitySet>();
	}
}
