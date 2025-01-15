using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Hierarchy;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Sql;
using System.Data.Entity.Migrations.Utilities;
using System.Data.Entity.Spatial;
using System.Data.Entity.SqlServer.Resources;
using System.Data.Entity.SqlServer.SqlGen;
using System.Data.Entity.SqlServer.Utilities;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.CSharp.RuntimeBinder;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x02000012 RID: 18
	public class SqlServerMigrationSqlGenerator : MigrationSqlGenerator
	{
		// Token: 0x0600013C RID: 316 RVA: 0x00006554 File Offset: 0x00004754
		public override bool IsPermissionDeniedError(Exception exception)
		{
			SqlException ex = exception as SqlException;
			return ex != null && ex.Number == 229;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000657C File Offset: 0x0000477C
		public override IEnumerable<MigrationStatement> Generate(IEnumerable<MigrationOperation> migrationOperations, string providerManifestToken)
		{
			Check.NotNull<IEnumerable<MigrationOperation>>(migrationOperations, "migrationOperations");
			Check.NotNull<string>(providerManifestToken, "providerManifestToken");
			this._statements = new List<MigrationStatement>();
			this._generatedSchemas = new HashSet<string>();
			this.InitializeProviderServices(providerManifestToken);
			this.GenerateStatements(migrationOperations);
			return this._statements;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000065CB File Offset: 0x000047CB
		private void GenerateStatements(IEnumerable<MigrationOperation> migrationOperations)
		{
			Check.NotNull<IEnumerable<MigrationOperation>>(migrationOperations, "migrationOperations");
			SqlServerMigrationSqlGenerator.DetectHistoryRebuild(migrationOperations).Each(delegate(dynamic o)
			{
				if (SqlServerMigrationSqlGenerator.<>o__10.<>p__0 == null)
				{
					SqlServerMigrationSqlGenerator.<>o__10.<>p__0 = CallSite<Action<CallSite, SqlServerMigrationSqlGenerator, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName | CSharpBinderFlags.ResultDiscarded, "Generate", null, typeof(SqlServerMigrationSqlGenerator), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				SqlServerMigrationSqlGenerator.<>o__10.<>p__0.Target(SqlServerMigrationSqlGenerator.<>o__10.<>p__0, this, o);
			});
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000065F0 File Offset: 0x000047F0
		public override string GenerateProcedureBody(ICollection<DbModificationCommandTree> commandTrees, string rowsAffectedParameter, string providerManifestToken)
		{
			Check.NotNull<ICollection<DbModificationCommandTree>>(commandTrees, "commandTrees");
			Check.NotEmpty(providerManifestToken, "providerManifestToken");
			if (!commandTrees.Any<DbModificationCommandTree>())
			{
				return "RETURN";
			}
			this.InitializeProviderServices(providerManifestToken);
			return this.GenerateFunctionSql(commandTrees, rowsAffectedParameter);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006628 File Offset: 0x00004828
		private void InitializeProviderServices(string providerManifestToken)
		{
			Check.NotEmpty(providerManifestToken, "providerManifestToken");
			this._providerManifestToken = providerManifestToken;
			using (DbConnection dbConnection = this.CreateConnection())
			{
				base.ProviderManifest = DbProviderServices.GetProviderServices(dbConnection).GetProviderManifest(providerManifestToken);
				this._sqlGenerator = new SqlGenerator(SqlVersionUtils.GetSqlVersion(providerManifestToken));
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00006690 File Offset: 0x00004890
		private string GenerateFunctionSql(ICollection<DbModificationCommandTree> commandTrees, string rowsAffectedParameter)
		{
			DmlFunctionSqlGenerator dmlFunctionSqlGenerator = new DmlFunctionSqlGenerator(this._sqlGenerator);
			switch (commandTrees.First<DbModificationCommandTree>().CommandTreeKind)
			{
			case DbCommandTreeKind.Update:
				return dmlFunctionSqlGenerator.GenerateUpdate(commandTrees.Cast<DbUpdateCommandTree>().ToList<DbUpdateCommandTree>(), rowsAffectedParameter);
			case DbCommandTreeKind.Insert:
				return dmlFunctionSqlGenerator.GenerateInsert(commandTrees.Cast<DbInsertCommandTree>().ToList<DbInsertCommandTree>());
			case DbCommandTreeKind.Delete:
				return dmlFunctionSqlGenerator.GenerateDelete(commandTrees.Cast<DbDeleteCommandTree>().ToList<DbDeleteCommandTree>(), rowsAffectedParameter);
			default:
				return null;
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006704 File Offset: 0x00004904
		protected virtual void Generate(UpdateDatabaseOperation updateDatabaseOperation)
		{
			Check.NotNull<UpdateDatabaseOperation>(updateDatabaseOperation, "updateDatabaseOperation");
			if (!updateDatabaseOperation.Migrations.Any<UpdateDatabaseOperation.Migration>())
			{
				return;
			}
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				indentedTextWriter.WriteLine("DECLARE @CurrentMigration [nvarchar](max)");
				indentedTextWriter.WriteLine();
				int num;
				foreach (DbQueryCommandTree dbQueryCommandTree in updateDatabaseOperation.HistoryQueryTrees)
				{
					HashSet<string> hashSet;
					string text = this._sqlGenerator.GenerateSql(dbQueryCommandTree, out hashSet);
					indentedTextWriter.Write("IF object_id('");
					indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(this._sqlGenerator.Targets.Single<string>()));
					indentedTextWriter.WriteLine("') IS NOT NULL");
					IndentedTextWriter indentedTextWriter2 = indentedTextWriter;
					num = indentedTextWriter2.Indent;
					indentedTextWriter2.Indent = num + 1;
					indentedTextWriter.WriteLine("SELECT @CurrentMigration =");
					IndentedTextWriter indentedTextWriter3 = indentedTextWriter;
					num = indentedTextWriter3.Indent;
					indentedTextWriter3.Indent = num + 1;
					indentedTextWriter.Write("(");
					indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Indent(text, indentedTextWriter.CurrentIndentation()));
					indentedTextWriter.WriteLine(")");
					indentedTextWriter.Indent -= 2;
					indentedTextWriter.WriteLine();
				}
				indentedTextWriter.WriteLine("IF @CurrentMigration IS NULL");
				IndentedTextWriter indentedTextWriter4 = indentedTextWriter;
				num = indentedTextWriter4.Indent;
				indentedTextWriter4.Indent = num + 1;
				indentedTextWriter.WriteLine("SET @CurrentMigration = '0'");
				this.Statement(indentedTextWriter, null);
			}
			List<MigrationStatement> statements = this._statements;
			foreach (UpdateDatabaseOperation.Migration migration in updateDatabaseOperation.Migrations)
			{
				using (IndentedTextWriter indentedTextWriter5 = SqlServerMigrationSqlGenerator.Writer())
				{
					this._statements = new List<MigrationStatement>();
					this.GenerateStatements(migration.Operations);
					if (this._statements.Count > 0)
					{
						indentedTextWriter5.Write("IF @CurrentMigration < '");
						indentedTextWriter5.Write(SqlServerMigrationSqlGenerator.Escape(migration.MigrationId));
						indentedTextWriter5.WriteLine("'");
						indentedTextWriter5.Write("BEGIN");
						using (IndentedTextWriter blockWriter = SqlServerMigrationSqlGenerator.Writer())
						{
							blockWriter.WriteLine();
							IndentedTextWriter blockWriter4 = blockWriter;
							int num = blockWriter4.Indent;
							blockWriter4.Indent = num + 1;
							Action<string> <>9__0;
							foreach (MigrationStatement migrationStatement in this._statements)
							{
								if (string.IsNullOrWhiteSpace(migrationStatement.BatchTerminator))
								{
									migrationStatement.Sql.EachLine(new Action<string>(blockWriter.WriteLine));
								}
								else
								{
									blockWriter.WriteLine("EXECUTE('");
									IndentedTextWriter blockWriter2 = blockWriter;
									num = blockWriter2.Indent;
									blockWriter2.Indent = num + 1;
									string sql = migrationStatement.Sql;
									Action<string> action;
									if ((action = <>9__0) == null)
									{
										action = (<>9__0 = delegate(string l)
										{
											blockWriter.WriteLine(SqlServerMigrationSqlGenerator.Escape(l));
										});
									}
									sql.EachLine(action);
									IndentedTextWriter blockWriter3 = blockWriter;
									num = blockWriter3.Indent;
									blockWriter3.Indent = num - 1;
									blockWriter.WriteLine("')");
								}
							}
							indentedTextWriter5.WriteLine(blockWriter.InnerWriter.ToString().TrimEnd(new char[0]));
						}
						indentedTextWriter5.WriteLine("END");
						statements.Add(new MigrationStatement
						{
							Sql = indentedTextWriter5.InnerWriter.ToString()
						});
					}
				}
			}
			this._statements = statements;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00006B2C File Offset: 0x00004D2C
		protected virtual void Generate(MigrationOperation migrationOperation)
		{
			Check.NotNull<MigrationOperation>(migrationOperation, "migrationOperation");
			throw Error.SqlServerMigrationSqlGenerator_UnknownOperation(base.GetType().Name, migrationOperation.GetType().FullName);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00006B55 File Offset: 0x00004D55
		protected virtual DbConnection CreateConnection()
		{
			return DbConfiguration.DependencyResolver.GetService("System.Data.SqlClient").CreateConnection();
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006B6B File Offset: 0x00004D6B
		protected virtual void Generate(CreateProcedureOperation createProcedureOperation)
		{
			Check.NotNull<CreateProcedureOperation>(createProcedureOperation, "createProcedureOperation");
			this.Generate(createProcedureOperation, "CREATE");
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00006B85 File Offset: 0x00004D85
		protected virtual void Generate(AlterProcedureOperation alterProcedureOperation)
		{
			Check.NotNull<AlterProcedureOperation>(alterProcedureOperation, "alterProcedureOperation");
			this.Generate(alterProcedureOperation, "ALTER");
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00006BA0 File Offset: 0x00004DA0
		private void Generate(ProcedureOperation procedureOperation, string modifier)
		{
			using (IndentedTextWriter writer = SqlServerMigrationSqlGenerator.Writer())
			{
				writer.Write(modifier);
				writer.WriteLine(" PROCEDURE " + this.Name(procedureOperation.Name));
				IndentedTextWriter writer5 = writer;
				int num = writer5.Indent;
				writer5.Indent = num + 1;
				procedureOperation.Parameters.Each(delegate(ParameterModel p, int i)
				{
					this.Generate(p, writer);
					writer.WriteLine((i < procedureOperation.Parameters.Count - 1) ? "," : string.Empty);
				});
				IndentedTextWriter writer2 = writer;
				num = writer2.Indent;
				writer2.Indent = num - 1;
				writer.WriteLine("AS");
				writer.WriteLine("BEGIN");
				IndentedTextWriter writer3 = writer;
				num = writer3.Indent;
				writer3.Indent = num + 1;
				writer.WriteLine((!string.IsNullOrWhiteSpace(procedureOperation.BodySql)) ? SqlServerMigrationSqlGenerator.Indent(procedureOperation.BodySql, writer.CurrentIndentation()) : "RETURN");
				IndentedTextWriter writer4 = writer;
				num = writer4.Indent;
				writer4.Indent = num - 1;
				writer.Write("END");
				this.Statement(writer, "GO");
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00006D20 File Offset: 0x00004F20
		private void Generate(ParameterModel parameterModel, IndentedTextWriter writer)
		{
			writer.Write("@");
			writer.Write(parameterModel.Name);
			writer.Write(" ");
			writer.Write(this.BuildPropertyType(parameterModel));
			if (parameterModel.IsOutParameter)
			{
				writer.Write(" OUT");
			}
			if (parameterModel.DefaultValue != null)
			{
				writer.Write(" = ");
				if (SqlServerMigrationSqlGenerator.<>o__20.<>p__1 == null)
				{
					SqlServerMigrationSqlGenerator.<>o__20.<>p__1 = CallSite<Action<CallSite, IndentedTextWriter, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Write", null, typeof(SqlServerMigrationSqlGenerator), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Action<CallSite, IndentedTextWriter, object> target = SqlServerMigrationSqlGenerator.<>o__20.<>p__1.Target;
				CallSite <>p__ = SqlServerMigrationSqlGenerator.<>o__20.<>p__1;
				if (SqlServerMigrationSqlGenerator.<>o__20.<>p__0 == null)
				{
					SqlServerMigrationSqlGenerator.<>o__20.<>p__0 = CallSite<Func<CallSite, SqlServerMigrationSqlGenerator, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "Generate", null, typeof(SqlServerMigrationSqlGenerator), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				target(<>p__, writer, SqlServerMigrationSqlGenerator.<>o__20.<>p__0.Target(SqlServerMigrationSqlGenerator.<>o__20.<>p__0, this, parameterModel.DefaultValue));
				return;
			}
			if (!string.IsNullOrWhiteSpace(parameterModel.DefaultValueSql))
			{
				writer.Write(" = ");
				writer.Write(parameterModel.DefaultValueSql);
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00006E60 File Offset: 0x00005060
		protected virtual void Generate(DropProcedureOperation dropProcedureOperation)
		{
			Check.NotNull<DropProcedureOperation>(dropProcedureOperation, "dropProcedureOperation");
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				indentedTextWriter.Write("DROP PROCEDURE ");
				indentedTextWriter.Write(this.Name(dropProcedureOperation.Name));
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00006EC0 File Offset: 0x000050C0
		protected virtual void Generate(CreateTableOperation createTableOperation)
		{
			Check.NotNull<CreateTableOperation>(createTableOperation, "createTableOperation");
			DatabaseName databaseName = DatabaseName.Parse(createTableOperation.Name);
			if (!string.IsNullOrWhiteSpace(databaseName.Schema) && !databaseName.Schema.EqualsIgnoreCase("dbo") && !this._generatedSchemas.Contains(databaseName.Schema))
			{
				this.GenerateCreateSchema(databaseName.Schema);
				this._generatedSchemas.Add(databaseName.Schema);
			}
			this.WriteCreateTable(createTableOperation);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00006F3C File Offset: 0x0000513C
		protected virtual void WriteCreateTable(CreateTableOperation createTableOperation)
		{
			Check.NotNull<CreateTableOperation>(createTableOperation, "createTableOperation");
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				this.WriteCreateTable(createTableOperation, indentedTextWriter);
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00006F88 File Offset: 0x00005188
		protected virtual void WriteCreateTable(CreateTableOperation createTableOperation, IndentedTextWriter writer)
		{
			Check.NotNull<CreateTableOperation>(createTableOperation, "createTableOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.WriteLine("CREATE TABLE " + this.Name(createTableOperation.Name) + " (");
			IndentedTextWriter writer2 = writer;
			int num = writer2.Indent;
			writer2.Indent = num + 1;
			createTableOperation.Columns.Each(delegate(ColumnModel c, int i)
			{
				this.Generate(c, writer);
				if (i < createTableOperation.Columns.Count - 1)
				{
					writer.WriteLine(",");
				}
			});
			if (createTableOperation.PrimaryKey != null)
			{
				writer.WriteLine(",");
				writer.Write("CONSTRAINT ");
				writer.Write(this.Quote(createTableOperation.PrimaryKey.Name));
				writer.Write(" PRIMARY KEY ");
				if (!createTableOperation.PrimaryKey.IsClustered)
				{
					writer.Write("NONCLUSTERED ");
				}
				writer.Write("(");
				writer.Write(createTableOperation.PrimaryKey.Columns.Join(new Func<string, string>(this.Quote), ", "));
				writer.WriteLine(")");
			}
			else
			{
				writer.WriteLine();
			}
			IndentedTextWriter writer3 = writer;
			num = writer3.Indent;
			writer3.Indent = num - 1;
			writer.Write(")");
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00007135 File Offset: 0x00005335
		protected internal virtual void Generate(AlterTableOperation alterTableOperation)
		{
			Check.NotNull<AlterTableOperation>(alterTableOperation, "alterTableOperation");
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00007144 File Offset: 0x00005344
		protected virtual void GenerateMakeSystemTable(CreateTableOperation createTableOperation, IndentedTextWriter writer)
		{
			Check.NotNull<CreateTableOperation>(createTableOperation, "createTableOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.WriteLine("BEGIN TRY");
			int num = writer.Indent;
			writer.Indent = num + 1;
			writer.WriteLine("EXECUTE sp_MS_marksystemobject '" + SqlServerMigrationSqlGenerator.Escape(createTableOperation.Name) + "'");
			num = writer.Indent;
			writer.Indent = num - 1;
			writer.WriteLine("END TRY");
			writer.WriteLine("BEGIN CATCH");
			writer.Write("END CATCH");
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000071D8 File Offset: 0x000053D8
		protected virtual void GenerateCreateSchema(string schema)
		{
			Check.NotEmpty(schema, "schema");
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				indentedTextWriter.Write("IF schema_id('");
				indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(schema));
				indentedTextWriter.WriteLine("') IS NULL");
				IndentedTextWriter indentedTextWriter2 = indentedTextWriter;
				int indent = indentedTextWriter2.Indent;
				indentedTextWriter2.Indent = indent + 1;
				indentedTextWriter.Write("EXECUTE('CREATE SCHEMA ");
				indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(this.Quote(schema)));
				indentedTextWriter.Write("')");
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00007278 File Offset: 0x00005478
		protected virtual void Generate(AddForeignKeyOperation addForeignKeyOperation)
		{
			Check.NotNull<AddForeignKeyOperation>(addForeignKeyOperation, "addForeignKeyOperation");
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				indentedTextWriter.Write("ALTER TABLE ");
				indentedTextWriter.Write(this.Name(addForeignKeyOperation.DependentTable));
				indentedTextWriter.Write(" ADD CONSTRAINT ");
				indentedTextWriter.Write(this.Quote(addForeignKeyOperation.Name));
				indentedTextWriter.Write(" FOREIGN KEY (");
				indentedTextWriter.Write(addForeignKeyOperation.DependentColumns.Select(new Func<string, string>(this.Quote)).Join(null, ", "));
				indentedTextWriter.Write(") REFERENCES ");
				indentedTextWriter.Write(this.Name(addForeignKeyOperation.PrincipalTable));
				indentedTextWriter.Write(" (");
				indentedTextWriter.Write(addForeignKeyOperation.PrincipalColumns.Select(new Func<string, string>(this.Quote)).Join(null, ", "));
				indentedTextWriter.Write(")");
				if (addForeignKeyOperation.CascadeDelete)
				{
					indentedTextWriter.Write(" ON DELETE CASCADE");
				}
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00007398 File Offset: 0x00005598
		protected virtual void Generate(DropForeignKeyOperation dropForeignKeyOperation)
		{
			Check.NotNull<DropForeignKeyOperation>(dropForeignKeyOperation, "dropForeignKeyOperation");
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				indentedTextWriter.Write("IF object_id(N'");
				string schema = DatabaseName.Parse(dropForeignKeyOperation.DependentTable).Schema;
				if (schema != null)
				{
					indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(this.Quote(schema)));
					indentedTextWriter.Write(".");
				}
				indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(this.Quote(dropForeignKeyOperation.Name)));
				indentedTextWriter.WriteLine("', N'F') IS NOT NULL");
				IndentedTextWriter indentedTextWriter2 = indentedTextWriter;
				int num = indentedTextWriter2.Indent;
				indentedTextWriter2.Indent = num + 1;
				indentedTextWriter.Write("ALTER TABLE ");
				indentedTextWriter.Write(this.Name(dropForeignKeyOperation.DependentTable));
				indentedTextWriter.Write(" DROP CONSTRAINT ");
				indentedTextWriter.Write(this.Quote(dropForeignKeyOperation.Name));
				IndentedTextWriter indentedTextWriter3 = indentedTextWriter;
				num = indentedTextWriter3.Indent;
				indentedTextWriter3.Indent = num - 1;
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00007494 File Offset: 0x00005694
		protected virtual void Generate(CreateIndexOperation createIndexOperation)
		{
			Check.NotNull<CreateIndexOperation>(createIndexOperation, "createIndexOperation");
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				indentedTextWriter.Write("CREATE ");
				if (createIndexOperation.IsUnique)
				{
					indentedTextWriter.Write("UNIQUE ");
				}
				if (createIndexOperation.IsClustered)
				{
					indentedTextWriter.Write("CLUSTERED ");
				}
				indentedTextWriter.Write("INDEX ");
				indentedTextWriter.Write(this.Quote(createIndexOperation.Name));
				indentedTextWriter.Write(" ON ");
				indentedTextWriter.Write(this.Name(createIndexOperation.Table));
				indentedTextWriter.Write("(");
				indentedTextWriter.Write(createIndexOperation.Columns.Join(new Func<string, string>(this.Quote), ", "));
				indentedTextWriter.Write(")");
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000757C File Offset: 0x0000577C
		protected virtual void Generate(DropIndexOperation dropIndexOperation)
		{
			Check.NotNull<DropIndexOperation>(dropIndexOperation, "dropIndexOperation");
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				indentedTextWriter.Write("IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'");
				indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(dropIndexOperation.Name));
				indentedTextWriter.Write("' AND object_id = object_id(N'");
				indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(this.Name(dropIndexOperation.Table)));
				indentedTextWriter.WriteLine("', N'U'))");
				IndentedTextWriter indentedTextWriter2 = indentedTextWriter;
				int num = indentedTextWriter2.Indent;
				indentedTextWriter2.Indent = num + 1;
				indentedTextWriter.Write("DROP INDEX ");
				indentedTextWriter.Write(this.Quote(dropIndexOperation.Name));
				indentedTextWriter.Write(" ON ");
				indentedTextWriter.Write(this.Name(dropIndexOperation.Table));
				IndentedTextWriter indentedTextWriter3 = indentedTextWriter;
				num = indentedTextWriter3.Indent;
				indentedTextWriter3.Indent = num - 1;
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00007664 File Offset: 0x00005864
		protected virtual void Generate(AddPrimaryKeyOperation addPrimaryKeyOperation)
		{
			Check.NotNull<AddPrimaryKeyOperation>(addPrimaryKeyOperation, "addPrimaryKeyOperation");
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				indentedTextWriter.Write("ALTER TABLE ");
				indentedTextWriter.Write(this.Name(addPrimaryKeyOperation.Table));
				indentedTextWriter.Write(" ADD CONSTRAINT ");
				indentedTextWriter.Write(this.Quote(addPrimaryKeyOperation.Name));
				indentedTextWriter.Write(" PRIMARY KEY ");
				if (!addPrimaryKeyOperation.IsClustered)
				{
					indentedTextWriter.Write("NONCLUSTERED ");
				}
				indentedTextWriter.Write("(");
				indentedTextWriter.Write(addPrimaryKeyOperation.Columns.Select(new Func<string, string>(this.Quote)).Join(null, ", "));
				indentedTextWriter.Write(")");
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00007740 File Offset: 0x00005940
		protected virtual void Generate(DropPrimaryKeyOperation dropPrimaryKeyOperation)
		{
			Check.NotNull<DropPrimaryKeyOperation>(dropPrimaryKeyOperation, "dropPrimaryKeyOperation");
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				indentedTextWriter.Write("ALTER TABLE ");
				indentedTextWriter.Write(this.Name(dropPrimaryKeyOperation.Table));
				indentedTextWriter.Write(" DROP CONSTRAINT ");
				indentedTextWriter.Write(this.Quote(dropPrimaryKeyOperation.Name));
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000077C0 File Offset: 0x000059C0
		protected virtual void Generate(AddColumnOperation addColumnOperation)
		{
			Check.NotNull<AddColumnOperation>(addColumnOperation, "addColumnOperation");
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				indentedTextWriter.Write("ALTER TABLE ");
				indentedTextWriter.Write(this.Name(addColumnOperation.Table));
				indentedTextWriter.Write(" ADD ");
				ColumnModel column = addColumnOperation.Column;
				this.Generate(column, indentedTextWriter);
				if (column.IsNullable != null && !column.IsNullable.Value && column.DefaultValue == null && string.IsNullOrWhiteSpace(column.DefaultValueSql) && !column.IsIdentity && !column.IsTimestamp && !column.StoreType.EqualsIgnoreCase("rowversion") && !column.StoreType.EqualsIgnoreCase("timestamp"))
				{
					indentedTextWriter.Write(" DEFAULT ");
					if (column.Type == PrimitiveTypeKind.DateTime)
					{
						indentedTextWriter.Write(this.Generate(DateTime.Parse("1900-01-01 00:00:00", CultureInfo.InvariantCulture)));
					}
					else
					{
						if (SqlServerMigrationSqlGenerator.<>o__34.<>p__1 == null)
						{
							SqlServerMigrationSqlGenerator.<>o__34.<>p__1 = CallSite<Action<CallSite, IndentedTextWriter, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Write", null, typeof(SqlServerMigrationSqlGenerator), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
						}
						Action<CallSite, IndentedTextWriter, object> target = SqlServerMigrationSqlGenerator.<>o__34.<>p__1.Target;
						CallSite <>p__ = SqlServerMigrationSqlGenerator.<>o__34.<>p__1;
						IndentedTextWriter indentedTextWriter2 = indentedTextWriter;
						if (SqlServerMigrationSqlGenerator.<>o__34.<>p__0 == null)
						{
							SqlServerMigrationSqlGenerator.<>o__34.<>p__0 = CallSite<Func<CallSite, SqlServerMigrationSqlGenerator, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "Generate", null, typeof(SqlServerMigrationSqlGenerator), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
						}
						target(<>p__, indentedTextWriter2, SqlServerMigrationSqlGenerator.<>o__34.<>p__0.Target(SqlServerMigrationSqlGenerator.<>o__34.<>p__0, this, column.ClrDefaultValue));
					}
				}
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000079B4 File Offset: 0x00005BB4
		protected virtual void Generate(DropColumnOperation dropColumnOperation)
		{
			Check.NotNull<DropColumnOperation>(dropColumnOperation, "dropColumnOperation");
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				this.DropDefaultConstraint(dropColumnOperation.Table, dropColumnOperation.Name, indentedTextWriter);
				indentedTextWriter.Write("ALTER TABLE ");
				indentedTextWriter.Write(this.Name(dropColumnOperation.Table));
				indentedTextWriter.Write(" DROP COLUMN ");
				indentedTextWriter.Write(this.Quote(dropColumnOperation.Name));
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00007A44 File Offset: 0x00005C44
		protected virtual void Generate(AlterColumnOperation alterColumnOperation)
		{
			Check.NotNull<AlterColumnOperation>(alterColumnOperation, "alterColumnOperation");
			ColumnModel column = alterColumnOperation.Column;
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				this.DropDefaultConstraint(alterColumnOperation.Table, column.Name, indentedTextWriter);
				indentedTextWriter.Write("ALTER TABLE ");
				indentedTextWriter.Write(this.Name(alterColumnOperation.Table));
				indentedTextWriter.Write(" ALTER COLUMN ");
				indentedTextWriter.Write(this.Quote(column.Name));
				indentedTextWriter.Write(" ");
				indentedTextWriter.Write(this.BuildColumnType(column));
				if (column.IsNullable != null && !column.IsNullable.Value)
				{
					indentedTextWriter.Write(" NOT");
				}
				indentedTextWriter.Write(" NULL");
				if (column.DefaultValue != null || !string.IsNullOrWhiteSpace(column.DefaultValueSql))
				{
					indentedTextWriter.WriteLine();
					indentedTextWriter.Write("ALTER TABLE ");
					indentedTextWriter.Write(this.Name(alterColumnOperation.Table));
					indentedTextWriter.Write(" ADD CONSTRAINT ");
					indentedTextWriter.Write(this.Quote("DF_" + alterColumnOperation.Table + "_" + column.Name));
					indentedTextWriter.Write(" DEFAULT ");
					if (SqlServerMigrationSqlGenerator.<>o__36.<>p__1 == null)
					{
						SqlServerMigrationSqlGenerator.<>o__36.<>p__1 = CallSite<Action<CallSite, IndentedTextWriter, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Write", null, typeof(SqlServerMigrationSqlGenerator), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					Action<CallSite, IndentedTextWriter, object> target = SqlServerMigrationSqlGenerator.<>o__36.<>p__1.Target;
					CallSite <>p__ = SqlServerMigrationSqlGenerator.<>o__36.<>p__1;
					IndentedTextWriter indentedTextWriter2 = indentedTextWriter;
					object obj;
					if (column.DefaultValue == null)
					{
						obj = column.DefaultValueSql;
					}
					else
					{
						if (SqlServerMigrationSqlGenerator.<>o__36.<>p__0 == null)
						{
							SqlServerMigrationSqlGenerator.<>o__36.<>p__0 = CallSite<Func<CallSite, SqlServerMigrationSqlGenerator, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "Generate", null, typeof(SqlServerMigrationSqlGenerator), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
						}
						obj = SqlServerMigrationSqlGenerator.<>o__36.<>p__0.Target(SqlServerMigrationSqlGenerator.<>o__36.<>p__0, this, column.DefaultValue);
					}
					target(<>p__, indentedTextWriter2, obj);
					indentedTextWriter.Write(" FOR ");
					indentedTextWriter.Write(this.Quote(column.Name));
				}
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00007C90 File Offset: 0x00005E90
		protected internal virtual void DropDefaultConstraint(string table, string column, IndentedTextWriter writer)
		{
			Check.NotEmpty(table, "table");
			Check.NotEmpty(column, "column");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			string text = "@var";
			int num = this._variableCounter;
			this._variableCounter = num + 1;
			string text2 = text + num.ToString();
			writer.Write("DECLARE ");
			writer.Write(text2);
			writer.WriteLine(" nvarchar(128)");
			writer.Write("SELECT ");
			writer.Write(text2);
			writer.WriteLine(" = name");
			writer.WriteLine("FROM sys.default_constraints");
			writer.Write("WHERE parent_object_id = object_id(N'");
			writer.Write(table);
			writer.WriteLine("')");
			writer.Write("AND col_name(parent_object_id, parent_column_id) = '");
			writer.Write(column);
			writer.WriteLine("';");
			writer.Write("IF ");
			writer.Write(text2);
			writer.WriteLine(" IS NOT NULL");
			num = writer.Indent;
			writer.Indent = num + 1;
			writer.Write("EXECUTE('ALTER TABLE ");
			writer.Write(SqlServerMigrationSqlGenerator.Escape(this.Name(table)));
			writer.Write(" DROP CONSTRAINT [' + ");
			writer.Write(text2);
			writer.WriteLine(" + ']')");
			num = writer.Indent;
			writer.Indent = num - 1;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00007DDC File Offset: 0x00005FDC
		protected virtual void Generate(DropTableOperation dropTableOperation)
		{
			Check.NotNull<DropTableOperation>(dropTableOperation, "dropTableOperation");
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				indentedTextWriter.Write("DROP TABLE ");
				indentedTextWriter.Write(this.Name(dropTableOperation.Name));
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00007E3C File Offset: 0x0000603C
		protected virtual void Generate(SqlOperation sqlOperation)
		{
			Check.NotNull<SqlOperation>(sqlOperation, "sqlOperation");
			this.StatementBatch(sqlOperation.Sql, sqlOperation.SuppressTransaction);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00007E5C File Offset: 0x0000605C
		protected virtual void Generate(RenameColumnOperation renameColumnOperation)
		{
			Check.NotNull<RenameColumnOperation>(renameColumnOperation, "renameColumnOperation");
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				indentedTextWriter.Write("EXECUTE sp_rename @objname = N'");
				indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(renameColumnOperation.Table));
				indentedTextWriter.Write(".");
				indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(renameColumnOperation.Name));
				indentedTextWriter.Write("', @newname = N'");
				indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(renameColumnOperation.NewName));
				indentedTextWriter.Write("', @objtype = N'COLUMN'");
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00007F00 File Offset: 0x00006100
		protected virtual void Generate(RenameIndexOperation renameIndexOperation)
		{
			Check.NotNull<RenameIndexOperation>(renameIndexOperation, "renameIndexOperation");
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				indentedTextWriter.Write("EXECUTE sp_rename @objname = N'");
				indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(renameIndexOperation.Table));
				indentedTextWriter.Write(".");
				indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(renameIndexOperation.Name));
				indentedTextWriter.Write("', @newname = N'");
				indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(renameIndexOperation.NewName));
				indentedTextWriter.Write("', @objtype = N'INDEX'");
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00007FA4 File Offset: 0x000061A4
		protected virtual void Generate(RenameTableOperation renameTableOperation)
		{
			Check.NotNull<RenameTableOperation>(renameTableOperation, "renameTableOperation");
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				SqlServerMigrationSqlGenerator.WriteRenameTable(renameTableOperation, indentedTextWriter);
				string text = PrimaryKeyOperation.BuildDefaultName(renameTableOperation.Name);
				string text2 = PrimaryKeyOperation.BuildDefaultName(((RenameTableOperation)renameTableOperation.Inverse).Name);
				indentedTextWriter.WriteLine();
				indentedTextWriter.Write("IF object_id('");
				indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(this.Quote(text)));
				indentedTextWriter.WriteLine("') IS NOT NULL BEGIN");
				IndentedTextWriter indentedTextWriter2 = indentedTextWriter;
				int num = indentedTextWriter2.Indent;
				indentedTextWriter2.Indent = num + 1;
				indentedTextWriter.Write("EXECUTE sp_rename @objname = N'");
				indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(this.Quote(text)));
				indentedTextWriter.Write("', @newname = N'");
				indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(text2));
				indentedTextWriter.WriteLine("', @objtype = N'OBJECT'");
				IndentedTextWriter indentedTextWriter3 = indentedTextWriter;
				num = indentedTextWriter3.Indent;
				indentedTextWriter3.Indent = num - 1;
				indentedTextWriter.Write("END");
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000080A8 File Offset: 0x000062A8
		private static void WriteRenameTable(RenameTableOperation renameTableOperation, IndentedTextWriter writer)
		{
			writer.Write("EXECUTE sp_rename @objname = N'");
			writer.Write(SqlServerMigrationSqlGenerator.Escape(renameTableOperation.Name));
			writer.Write("', @newname = N'");
			writer.Write(SqlServerMigrationSqlGenerator.Escape(renameTableOperation.NewName));
			writer.Write("', @objtype = N'OBJECT'");
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000080F8 File Offset: 0x000062F8
		protected virtual void Generate(RenameProcedureOperation renameProcedureOperation)
		{
			Check.NotNull<RenameProcedureOperation>(renameProcedureOperation, "renameProcedureOperation");
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				indentedTextWriter.Write("EXECUTE sp_rename @objname = N'");
				indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(renameProcedureOperation.Name));
				indentedTextWriter.Write("', @newname = N'");
				indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(renameProcedureOperation.NewName));
				indentedTextWriter.Write("', @objtype = N'OBJECT'");
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00008180 File Offset: 0x00006380
		protected virtual void Generate(MoveProcedureOperation moveProcedureOperation)
		{
			Check.NotNull<MoveProcedureOperation>(moveProcedureOperation, "moveProcedureOperation");
			string text = moveProcedureOperation.NewSchema ?? "dbo";
			if (!text.EqualsIgnoreCase("dbo") && !this._generatedSchemas.Contains(text))
			{
				this.GenerateCreateSchema(text);
				this._generatedSchemas.Add(text);
			}
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				indentedTextWriter.Write("ALTER SCHEMA ");
				indentedTextWriter.Write(this.Quote(text));
				indentedTextWriter.Write(" TRANSFER ");
				indentedTextWriter.Write(this.Name(moveProcedureOperation.Name));
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00008238 File Offset: 0x00006438
		protected virtual void Generate(MoveTableOperation moveTableOperation)
		{
			Check.NotNull<MoveTableOperation>(moveTableOperation, "moveTableOperation");
			string text = moveTableOperation.NewSchema ?? "dbo";
			if (!text.EqualsIgnoreCase("dbo") && !this._generatedSchemas.Contains(text))
			{
				this.GenerateCreateSchema(text);
				this._generatedSchemas.Add(text);
			}
			if (!moveTableOperation.IsSystem)
			{
				using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
				{
					indentedTextWriter.Write("ALTER SCHEMA ");
					indentedTextWriter.Write(this.Quote(text));
					indentedTextWriter.Write(" TRANSFER ");
					indentedTextWriter.Write(this.Name(moveTableOperation.Name));
					this.Statement(indentedTextWriter, null);
					return;
				}
			}
			using (IndentedTextWriter indentedTextWriter2 = SqlServerMigrationSqlGenerator.Writer())
			{
				indentedTextWriter2.Write("IF object_id('");
				indentedTextWriter2.Write(moveTableOperation.CreateTableOperation.Name);
				indentedTextWriter2.WriteLine("') IS NULL BEGIN");
				IndentedTextWriter indentedTextWriter3 = indentedTextWriter2;
				int num = indentedTextWriter3.Indent;
				indentedTextWriter3.Indent = num + 1;
				this.WriteCreateTable(moveTableOperation.CreateTableOperation, indentedTextWriter2);
				indentedTextWriter2.WriteLine();
				IndentedTextWriter indentedTextWriter4 = indentedTextWriter2;
				num = indentedTextWriter4.Indent;
				indentedTextWriter4.Indent = num - 1;
				indentedTextWriter2.WriteLine("END");
				indentedTextWriter2.Write("INSERT INTO ");
				indentedTextWriter2.WriteLine(this.Name(moveTableOperation.CreateTableOperation.Name));
				indentedTextWriter2.Write("SELECT * FROM ");
				indentedTextWriter2.WriteLine(this.Name(moveTableOperation.Name));
				indentedTextWriter2.Write("WHERE [ContextKey] = ");
				indentedTextWriter2.WriteLine(this.Generate(moveTableOperation.ContextKey));
				indentedTextWriter2.Write("DELETE ");
				indentedTextWriter2.WriteLine(this.Name(moveTableOperation.Name));
				indentedTextWriter2.Write("WHERE [ContextKey] = ");
				indentedTextWriter2.WriteLine(this.Generate(moveTableOperation.ContextKey));
				indentedTextWriter2.Write("IF NOT EXISTS(SELECT * FROM ");
				indentedTextWriter2.Write(this.Name(moveTableOperation.Name));
				indentedTextWriter2.WriteLine(")");
				IndentedTextWriter indentedTextWriter5 = indentedTextWriter2;
				num = indentedTextWriter5.Indent;
				indentedTextWriter5.Indent = num + 1;
				indentedTextWriter2.Write("DROP TABLE ");
				indentedTextWriter2.Write(this.Name(moveTableOperation.Name));
				IndentedTextWriter indentedTextWriter6 = indentedTextWriter2;
				num = indentedTextWriter6.Indent;
				indentedTextWriter6.Indent = num - 1;
				this.Statement(indentedTextWriter2, null);
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00008498 File Offset: 0x00006698
		protected internal virtual void Generate(ColumnModel column, IndentedTextWriter writer)
		{
			Check.NotNull<ColumnModel>(column, "column");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write(this.Quote(column.Name));
			writer.Write(" ");
			writer.Write(this.BuildColumnType(column));
			if (column.IsNullable != null && !column.IsNullable.Value)
			{
				writer.Write(" NOT NULL");
			}
			if (column.DefaultValue != null)
			{
				writer.Write(" DEFAULT ");
				if (SqlServerMigrationSqlGenerator.<>o__47.<>p__1 == null)
				{
					SqlServerMigrationSqlGenerator.<>o__47.<>p__1 = CallSite<Action<CallSite, IndentedTextWriter, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Write", null, typeof(SqlServerMigrationSqlGenerator), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Action<CallSite, IndentedTextWriter, object> target = SqlServerMigrationSqlGenerator.<>o__47.<>p__1.Target;
				CallSite <>p__ = SqlServerMigrationSqlGenerator.<>o__47.<>p__1;
				if (SqlServerMigrationSqlGenerator.<>o__47.<>p__0 == null)
				{
					SqlServerMigrationSqlGenerator.<>o__47.<>p__0 = CallSite<Func<CallSite, SqlServerMigrationSqlGenerator, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "Generate", null, typeof(SqlServerMigrationSqlGenerator), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				target(<>p__, writer, SqlServerMigrationSqlGenerator.<>o__47.<>p__0.Target(SqlServerMigrationSqlGenerator.<>o__47.<>p__0, this, column.DefaultValue));
				return;
			}
			if (!string.IsNullOrWhiteSpace(column.DefaultValueSql))
			{
				writer.Write(" DEFAULT ");
				writer.Write(column.DefaultValueSql);
				return;
			}
			if (column.IsIdentity)
			{
				if (column.Type == PrimitiveTypeKind.Guid && column.DefaultValue == null)
				{
					writer.Write(" DEFAULT " + this.GuidColumnDefault);
					return;
				}
				writer.Write(" IDENTITY");
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000164 RID: 356 RVA: 0x0000863F File Offset: 0x0000683F
		protected virtual string GuidColumnDefault
		{
			get
			{
				if (!(this._providerManifestToken != "2012.Azure") || !(this._providerManifestToken != "2000"))
				{
					return "newid()";
				}
				return "newsequentialid()";
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00008670 File Offset: 0x00006870
		protected virtual void Generate(HistoryOperation historyOperation)
		{
			Check.NotNull<HistoryOperation>(historyOperation, "historyOperation");
			using (IndentedTextWriter writer = SqlServerMigrationSqlGenerator.Writer())
			{
				historyOperation.CommandTrees.Each(delegate(DbModificationCommandTree commandTree)
				{
					DbCommandTreeKind commandTreeKind = commandTree.CommandTreeKind;
					List<SqlParameter> list;
					if (commandTreeKind == DbCommandTreeKind.Insert)
					{
						writer.Write(DmlSqlGenerator.GenerateInsertSql((DbInsertCommandTree)commandTree, this._sqlGenerator, out list, false, true, false));
						return;
					}
					if (commandTreeKind != DbCommandTreeKind.Delete)
					{
						return;
					}
					writer.Write(DmlSqlGenerator.GenerateDeleteSql((DbDeleteCommandTree)commandTree, this._sqlGenerator, out list, true, false));
				});
				this.Statement(writer, null);
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000086EC File Offset: 0x000068EC
		protected virtual string Generate(byte[] defaultValue)
		{
			Check.NotNull<byte[]>(defaultValue, "defaultValue");
			return "0x" + defaultValue.ToHexString();
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000870A File Offset: 0x0000690A
		protected virtual string Generate(bool defaultValue)
		{
			if (!defaultValue)
			{
				return "0";
			}
			return "1";
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000871A File Offset: 0x0000691A
		protected virtual string Generate(DateTime defaultValue)
		{
			return "'" + defaultValue.ToString("yyyy-MM-ddTHH:mm:ss.fffK", CultureInfo.InvariantCulture) + "'";
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000873C File Offset: 0x0000693C
		protected virtual string Generate(DateTimeOffset defaultValue)
		{
			return "'" + defaultValue.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz", CultureInfo.InvariantCulture) + "'";
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00008760 File Offset: 0x00006960
		protected virtual string Generate(Guid defaultValue)
		{
			string text = "'";
			Guid guid = defaultValue;
			return text + guid.ToString() + "'";
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000878B File Offset: 0x0000698B
		protected virtual string Generate(string defaultValue)
		{
			Check.NotNull<string>(defaultValue, "defaultValue");
			return "'" + defaultValue + "'";
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000087AC File Offset: 0x000069AC
		protected virtual string Generate(TimeSpan defaultValue)
		{
			string text = "'";
			TimeSpan timeSpan = defaultValue;
			return text + timeSpan.ToString() + "'";
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000087D7 File Offset: 0x000069D7
		protected virtual string Generate(HierarchyId defaultValue)
		{
			return "cast('" + ((defaultValue != null) ? defaultValue.ToString() : null) + "' as hierarchyid)";
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000087F5 File Offset: 0x000069F5
		protected virtual string Generate(DbGeography defaultValue)
		{
			return "'" + ((defaultValue != null) ? defaultValue.ToString() : null) + "'";
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00008813 File Offset: 0x00006A13
		protected virtual string Generate(DbGeometry defaultValue)
		{
			return "'" + ((defaultValue != null) ? defaultValue.ToString() : null) + "'";
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00008831 File Offset: 0x00006A31
		protected virtual string Generate(object defaultValue)
		{
			Check.NotNull<object>(defaultValue, "defaultValue");
			return string.Format(CultureInfo.InvariantCulture, "{0}", new object[] { defaultValue });
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00008858 File Offset: 0x00006A58
		protected virtual string BuildColumnType(ColumnModel columnModel)
		{
			Check.NotNull<ColumnModel>(columnModel, "columnModel");
			if (columnModel.IsTimestamp)
			{
				return "rowversion";
			}
			return this.BuildPropertyType(columnModel);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000887C File Offset: 0x00006A7C
		private string BuildPropertyType(PropertyModel propertyModel)
		{
			string text = propertyModel.StoreType;
			TypeUsage typeUsage = base.ProviderManifest.GetStoreType(propertyModel.TypeUsage);
			if (string.IsNullOrWhiteSpace(text))
			{
				text = typeUsage.EdmType.Name;
			}
			else
			{
				typeUsage = this.BuildStoreTypeUsage(text, propertyModel) ?? typeUsage;
			}
			string text2 = text;
			if (text2.EndsWith("(max)", StringComparison.Ordinal))
			{
				text2 = this.Quote(text2.Substring(0, text2.Length - "(max)".Length)) + "(max)";
			}
			else
			{
				text2 = this.Quote(text2);
			}
			if (text != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1761125480U)
				{
					if (num <= 923440646U)
					{
						if (num != 520654156U)
						{
							if (num != 923440646U)
							{
								return text2;
							}
							if (!(text == "datetime2"))
							{
								return text2;
							}
							goto IL_0298;
						}
						else if (!(text == "decimal"))
						{
							return text2;
						}
					}
					else if (num != 1539863742U)
					{
						if (num != 1564253156U)
						{
							if (num != 1761125480U)
							{
								return text2;
							}
							if (!(text == "numeric"))
							{
								return text2;
							}
						}
						else
						{
							if (!(text == "time"))
							{
								return text2;
							}
							goto IL_0298;
						}
					}
					else
					{
						if (!(text == "nvarchar"))
						{
							return text2;
						}
						goto IL_02D4;
					}
					return string.Concat(new string[]
					{
						text2,
						"(",
						(propertyModel.Precision ?? typeUsage.GetPrecision()).ToString(),
						", ",
						(propertyModel.Scale ?? typeUsage.GetScale()).ToString(),
						")"
					});
				}
				if (num <= 3347933383U)
				{
					if (num != 2336348659U)
					{
						if (num != 2823553821U)
						{
							if (num != 3347933383U)
							{
								return text2;
							}
							if (!(text == "varbinary"))
							{
								return text2;
							}
							goto IL_02D4;
						}
						else
						{
							if (!(text == "char"))
							{
								return text2;
							}
							goto IL_02D4;
						}
					}
					else if (!(text == "datetimeoffset"))
					{
						return text2;
					}
				}
				else if (num != 3716508924U)
				{
					if (num != 3761451113U)
					{
						if (num != 4163743794U)
						{
							return text2;
						}
						if (!(text == "varchar"))
						{
							return text2;
						}
						goto IL_02D4;
					}
					else
					{
						if (!(text == "nchar"))
						{
							return text2;
						}
						goto IL_02D4;
					}
				}
				else
				{
					if (!(text == "binary"))
					{
						return text2;
					}
					goto IL_02D4;
				}
				IL_0298:
				return text2 + "(" + (propertyModel.Precision ?? typeUsage.GetPrecision()).ToString() + ")";
				IL_02D4:
				text2 = text2 + "(" + (propertyModel.MaxLength ?? typeUsage.GetMaxLength()).ToString() + ")";
			}
			return text2;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00008B98 File Offset: 0x00006D98
		protected virtual string Name(string name)
		{
			Check.NotEmpty(name, "name");
			DatabaseName databaseName = DatabaseName.Parse(name);
			return new string[] { databaseName.Schema, databaseName.Name }.Join(new Func<string, string>(this.Quote), ".");
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00008BE7 File Offset: 0x00006DE7
		protected virtual string Quote(string identifier)
		{
			Check.NotEmpty(identifier, "identifier");
			return SqlGenerator.QuoteIdentifier(identifier);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00008BFB File Offset: 0x00006DFB
		private static string Escape(string s)
		{
			return s.Replace("'", "''");
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00008C0D File Offset: 0x00006E0D
		private static string Indent(string s, string indentation)
		{
			return new Regex("\\r?\\n *").Replace(s, Environment.NewLine + indentation);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00008C2A File Offset: 0x00006E2A
		protected void Statement(string sql, bool suppressTransaction = false, string batchTerminator = null)
		{
			Check.NotEmpty(sql, "sql");
			this._statements.Add(new MigrationStatement
			{
				Sql = sql,
				SuppressTransaction = suppressTransaction,
				BatchTerminator = batchTerminator
			});
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00008C5D File Offset: 0x00006E5D
		protected static IndentedTextWriter Writer()
		{
			return new IndentedTextWriter(new StringWriter(CultureInfo.InvariantCulture));
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00008C6E File Offset: 0x00006E6E
		protected void Statement(IndentedTextWriter writer, string batchTerminator = null)
		{
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			this.Statement(writer.InnerWriter.ToString(), false, batchTerminator);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00008C90 File Offset: 0x00006E90
		protected void StatementBatch(string sqlBatch, bool suppressTransaction = false)
		{
			Check.NotNull<string>(sqlBatch, "sqlBatch");
			sqlBatch = Regex.Replace(sqlBatch, "\\\\(\\r\\n|\\r|\\n)", "");
			string[] array = Regex.Split(sqlBatch, string.Format(CultureInfo.InvariantCulture, "^\\s*({0}[ \\t]+[0-9]+|{0})(?:\\s+|$)", new object[] { "GO" }), RegexOptions.IgnoreCase | RegexOptions.Multiline);
			for (int i = 0; i < array.Length; i++)
			{
				if (!array[i].StartsWith("GO", StringComparison.OrdinalIgnoreCase) && (i != array.Length - 1 || !string.IsNullOrWhiteSpace(array[i])))
				{
					if (array.Length > i + 1 && array[i + 1].StartsWith("GO", StringComparison.OrdinalIgnoreCase))
					{
						int num = 1;
						if (!array[i + 1].EqualsIgnoreCase("GO"))
						{
							num = int.Parse(Regex.Match(array[i + 1], "([0-9]+)").Value, CultureInfo.InvariantCulture);
						}
						for (int j = 0; j < num; j++)
						{
							this.Statement(array[i], suppressTransaction, "GO");
						}
					}
					else
					{
						this.Statement(array[i], suppressTransaction, null);
					}
				}
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00008D8D File Offset: 0x00006F8D
		private static IEnumerable<MigrationOperation> DetectHistoryRebuild(IEnumerable<MigrationOperation> operations)
		{
			IEnumerator<MigrationOperation> enumerator = operations.GetEnumerator();
			while (enumerator.MoveNext())
			{
				SqlServerMigrationSqlGenerator.HistoryRebuildOperationSequence historyRebuildOperationSequence = SqlServerMigrationSqlGenerator.HistoryRebuildOperationSequence.Detect(enumerator);
				yield return historyRebuildOperationSequence ?? enumerator.Current;
			}
			yield break;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00008DA0 File Offset: 0x00006FA0
		private void Generate(SqlServerMigrationSqlGenerator.HistoryRebuildOperationSequence sequence)
		{
			CreateTableOperation createTableOperation = sequence.DropPrimaryKeyOperation.CreateTableOperation;
			CreateTableOperation createTableOperation2 = SqlServerMigrationSqlGenerator.ResolveNameConflicts(createTableOperation);
			RenameTableOperation renameTableOperation = new RenameTableOperation(createTableOperation2.Name, "__MigrationHistory", null);
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				this.WriteCreateTable(createTableOperation2, indentedTextWriter);
				indentedTextWriter.WriteLine();
				indentedTextWriter.Write("INSERT INTO ");
				indentedTextWriter.WriteLine(this.Name(createTableOperation2.Name));
				indentedTextWriter.Write("SELECT ");
				bool flag = true;
				foreach (ColumnModel columnModel in createTableOperation.Columns)
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						indentedTextWriter.Write(", ");
					}
					indentedTextWriter.Write((columnModel.Name == sequence.AddColumnOperation.Column.Name) ? this.Generate((string)sequence.AddColumnOperation.Column.DefaultValue) : ((columnModel.Type == PrimitiveTypeKind.String) ? string.Concat(new string[]
					{
						"LEFT(",
						this.Name(columnModel.Name),
						", ",
						columnModel.MaxLength.ToString(),
						")"
					}) : this.Name(columnModel.Name)));
				}
				indentedTextWriter.Write(" FROM ");
				indentedTextWriter.WriteLine(this.Name(createTableOperation.Name));
				indentedTextWriter.Write("DROP TABLE ");
				indentedTextWriter.WriteLine(this.Name(createTableOperation.Name));
				SqlServerMigrationSqlGenerator.WriteRenameTable(renameTableOperation, indentedTextWriter);
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00008F8C File Offset: 0x0000718C
		private static CreateTableOperation ResolveNameConflicts(CreateTableOperation source)
		{
			CreateTableOperation target = new CreateTableOperation(source.Name + "2", null)
			{
				PrimaryKey = new AddPrimaryKeyOperation(null)
				{
					IsClustered = source.PrimaryKey.IsClustered
				}
			};
			source.Columns.Each(delegate(ColumnModel c)
			{
				target.Columns.Add(c);
			});
			source.PrimaryKey.Columns.Each(delegate(string c)
			{
				target.PrimaryKey.Columns.Add(c);
			});
			return target;
		}

		// Token: 0x0400001E RID: 30
		private const string BatchTerminator = "GO";

		// Token: 0x0400001F RID: 31
		internal const string DateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fffK";

		// Token: 0x04000020 RID: 32
		internal const string DateTimeOffsetFormat = "yyyy-MM-ddTHH:mm:ss.fffzzz";

		// Token: 0x04000021 RID: 33
		private SqlGenerator _sqlGenerator;

		// Token: 0x04000022 RID: 34
		private List<MigrationStatement> _statements;

		// Token: 0x04000023 RID: 35
		private HashSet<string> _generatedSchemas;

		// Token: 0x04000024 RID: 36
		private string _providerManifestToken;

		// Token: 0x04000025 RID: 37
		private int _variableCounter;

		// Token: 0x02000057 RID: 87
		private class HistoryRebuildOperationSequence : MigrationOperation
		{
			// Token: 0x06000643 RID: 1603 RVA: 0x0001AF08 File Offset: 0x00019108
			private HistoryRebuildOperationSequence(AddColumnOperation addColumnOperation, DropPrimaryKeyOperation dropPrimaryKeyOperation)
				: base(null)
			{
				this.AddColumnOperation = addColumnOperation;
				this.DropPrimaryKeyOperation = dropPrimaryKeyOperation;
			}

			// Token: 0x170000EA RID: 234
			// (get) Token: 0x06000644 RID: 1604 RVA: 0x0001AF1F File Offset: 0x0001911F
			public override bool IsDestructiveChange
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000645 RID: 1605 RVA: 0x0001AF24 File Offset: 0x00019124
			public static SqlServerMigrationSqlGenerator.HistoryRebuildOperationSequence Detect(IEnumerator<MigrationOperation> enumerator)
			{
				AddColumnOperation addColumnOperation = enumerator.Current as AddColumnOperation;
				if (addColumnOperation == null || addColumnOperation.Table != "dbo.__MigrationHistory" || addColumnOperation.Column.Name != "ContextKey")
				{
					return null;
				}
				enumerator.MoveNext();
				DropPrimaryKeyOperation dropPrimaryKeyOperation = (DropPrimaryKeyOperation)enumerator.Current;
				enumerator.MoveNext();
				AlterColumnOperation alterColumnOperation = (AlterColumnOperation)enumerator.Current;
				enumerator.MoveNext();
				AddPrimaryKeyOperation addPrimaryKeyOperation = (AddPrimaryKeyOperation)enumerator.Current;
				return new SqlServerMigrationSqlGenerator.HistoryRebuildOperationSequence(addColumnOperation, dropPrimaryKeyOperation);
			}

			// Token: 0x04000191 RID: 401
			public readonly AddColumnOperation AddColumnOperation;

			// Token: 0x04000192 RID: 402
			public readonly DropPrimaryKeyOperation DropPrimaryKeyOperation;
		}
	}
}
