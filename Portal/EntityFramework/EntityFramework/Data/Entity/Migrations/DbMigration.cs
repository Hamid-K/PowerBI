using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Migrations.Builders;
using System.Data.Entity.Migrations.Edm;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.IO;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Migrations
{
	// Token: 0x0200009D RID: 157
	public abstract class DbMigration : IDbMigration
	{
		// Token: 0x06000E26 RID: 3622
		public abstract void Up();

		// Token: 0x06000E27 RID: 3623 RVA: 0x0001C3E1 File Offset: 0x0001A5E1
		public virtual void Down()
		{
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x0001C3E3 File Offset: 0x0001A5E3
		public void CreateStoredProcedure(string name, string body, object anonymousArguments = null)
		{
			Check.NotEmpty(name, "name");
			this.CreateStoredProcedure<object>(name, (ParameterBuilder _) => new { }, body, anonymousArguments);
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x0001C41C File Offset: 0x0001A61C
		public void CreateStoredProcedure<TParameters>(string name, Func<ParameterBuilder, TParameters> parametersAction, string body, object anonymousArguments = null)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<Func<ParameterBuilder, TParameters>>(parametersAction, "parametersAction");
			CreateProcedureOperation createProcedureOperation = new CreateProcedureOperation(name, body, anonymousArguments);
			this.AddOperation(createProcedureOperation);
			TParameters parameters = parametersAction(new ParameterBuilder());
			parameters.GetType().GetNonIndexerProperties().Each(delegate(PropertyInfo p, int i)
			{
				ParameterModel parameterModel = p.GetValue(parameters, null) as ParameterModel;
				if (parameterModel != null)
				{
					if (string.IsNullOrWhiteSpace(parameterModel.Name))
					{
						parameterModel.Name = p.Name;
					}
					createProcedureOperation.Parameters.Add(parameterModel);
				}
			});
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x0001C49A File Offset: 0x0001A69A
		public void AlterStoredProcedure(string name, string body, object anonymousArguments = null)
		{
			Check.NotEmpty(name, "name");
			this.AlterStoredProcedure<object>(name, (ParameterBuilder _) => new { }, body, anonymousArguments);
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0001C4D0 File Offset: 0x0001A6D0
		public void AlterStoredProcedure<TParameters>(string name, Func<ParameterBuilder, TParameters> parametersAction, string body, object anonymousArguments = null)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<Func<ParameterBuilder, TParameters>>(parametersAction, "parametersAction");
			AlterProcedureOperation alterProcedureOperation = new AlterProcedureOperation(name, body, anonymousArguments);
			this.AddOperation(alterProcedureOperation);
			TParameters parameters = parametersAction(new ParameterBuilder());
			parameters.GetType().GetNonIndexerProperties().Each(delegate(PropertyInfo p, int i)
			{
				ParameterModel parameterModel = p.GetValue(parameters, null) as ParameterModel;
				if (parameterModel != null)
				{
					if (string.IsNullOrWhiteSpace(parameterModel.Name))
					{
						parameterModel.Name = p.Name;
					}
					alterProcedureOperation.Parameters.Add(parameterModel);
				}
			});
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x0001C54E File Offset: 0x0001A74E
		public void DropStoredProcedure(string name, object anonymousArguments = null)
		{
			Check.NotEmpty(name, "name");
			this.AddOperation(new DropProcedureOperation(name, anonymousArguments));
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x0001C569 File Offset: 0x0001A769
		protected internal TableBuilder<TColumns> CreateTable<TColumns>(string name, Func<ColumnBuilder, TColumns> columnsAction, object anonymousArguments = null)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<Func<ColumnBuilder, TColumns>>(columnsAction, "columnsAction");
			return this.CreateTable<TColumns>(name, columnsAction, null, anonymousArguments);
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0001C590 File Offset: 0x0001A790
		protected internal TableBuilder<TColumns> CreateTable<TColumns>(string name, Func<ColumnBuilder, TColumns> columnsAction, IDictionary<string, object> annotations, object anonymousArguments = null)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<Func<ColumnBuilder, TColumns>>(columnsAction, "columnsAction");
			CreateTableOperation createTableOperation = new CreateTableOperation(name, annotations, anonymousArguments);
			this.AddOperation(createTableOperation);
			DbMigration.AddColumns<TColumns>(columnsAction(new ColumnBuilder()), createTableOperation.Columns);
			return new TableBuilder<TColumns>(createTableOperation, this);
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0001C5E4 File Offset: 0x0001A7E4
		protected internal void AlterTableAnnotations<TColumns>(string name, Func<ColumnBuilder, TColumns> columnsAction, IDictionary<string, AnnotationValues> annotations, object anonymousArguments = null)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<Func<ColumnBuilder, TColumns>>(columnsAction, "columnsAction");
			AlterTableOperation alterTableOperation = new AlterTableOperation(name, annotations, anonymousArguments);
			DbMigration.AddColumns<TColumns>(columnsAction(new ColumnBuilder()), alterTableOperation.Columns);
			this.AddOperation(alterTableOperation);
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x0001C630 File Offset: 0x0001A830
		private static void AddColumns<TColumns>(TColumns columns, ICollection<ColumnModel> columnModels)
		{
			columns.GetType().GetNonIndexerProperties().Each(delegate(PropertyInfo p, int i)
			{
				ColumnModel columnModel = p.GetValue(columns, null) as ColumnModel;
				if (columnModel != null)
				{
					columnModel.ApiPropertyInfo = p;
					if (string.IsNullOrWhiteSpace(columnModel.Name))
					{
						columnModel.Name = p.Name;
					}
					columnModels.Add(columnModel);
				}
			});
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x0001C678 File Offset: 0x0001A878
		protected internal void AddForeignKey(string dependentTable, string dependentColumn, string principalTable, string principalColumn = null, bool cascadeDelete = false, string name = null, object anonymousArguments = null)
		{
			Check.NotEmpty(dependentTable, "dependentTable");
			Check.NotEmpty(dependentColumn, "dependentColumn");
			Check.NotEmpty(principalTable, "principalTable");
			string[] array = new string[] { dependentColumn };
			object obj;
			if (principalColumn == null)
			{
				obj = null;
			}
			else
			{
				(obj = new string[1])[0] = principalColumn;
			}
			this.AddForeignKey(dependentTable, array, principalTable, obj, cascadeDelete, name, anonymousArguments);
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x0001C6D4 File Offset: 0x0001A8D4
		protected internal void AddForeignKey(string dependentTable, string[] dependentColumns, string principalTable, string[] principalColumns = null, bool cascadeDelete = false, string name = null, object anonymousArguments = null)
		{
			Check.NotEmpty(dependentTable, "dependentTable");
			Check.NotNull<string[]>(dependentColumns, "dependentColumns");
			Check.NotEmpty(principalTable, "principalTable");
			if (!dependentColumns.Any<string>())
			{
				throw new ArgumentException(Strings.CollectionEmpty("dependentColumns", "AddForeignKey"));
			}
			AddForeignKeyOperation addForeignKeyOperation = new AddForeignKeyOperation(anonymousArguments)
			{
				DependentTable = dependentTable,
				PrincipalTable = principalTable,
				CascadeDelete = cascadeDelete,
				Name = name
			};
			dependentColumns.Each(delegate(string c)
			{
				addForeignKeyOperation.DependentColumns.Add(c);
			});
			if (principalColumns != null)
			{
				principalColumns.Each(delegate(string c)
				{
					addForeignKeyOperation.PrincipalColumns.Add(c);
				});
			}
			this.AddOperation(addForeignKeyOperation);
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0001C788 File Offset: 0x0001A988
		protected internal void DropForeignKey(string dependentTable, string name, object anonymousArguments = null)
		{
			Check.NotEmpty(dependentTable, "dependentTable");
			Check.NotEmpty(name, "name");
			DropForeignKeyOperation dropForeignKeyOperation = new DropForeignKeyOperation(anonymousArguments)
			{
				DependentTable = dependentTable,
				Name = name
			};
			this.AddOperation(dropForeignKeyOperation);
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x0001C7C9 File Offset: 0x0001A9C9
		protected internal void DropForeignKey(string dependentTable, string dependentColumn, string principalTable, object anonymousArguments = null)
		{
			Check.NotEmpty(dependentTable, "dependentTable");
			Check.NotEmpty(dependentColumn, "dependentColumn");
			Check.NotEmpty(principalTable, "principalTable");
			this.DropForeignKey(dependentTable, new string[] { dependentColumn }, principalTable, anonymousArguments);
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0001C803 File Offset: 0x0001AA03
		[Obsolete("The principalColumn parameter is no longer required and can be removed.")]
		protected internal void DropForeignKey(string dependentTable, string dependentColumn, string principalTable, string principalColumn, object anonymousArguments = null)
		{
			Check.NotEmpty(dependentTable, "dependentTable");
			Check.NotEmpty(dependentColumn, "dependentColumn");
			Check.NotEmpty(principalTable, "principalTable");
			this.DropForeignKey(dependentTable, new string[] { dependentColumn }, principalTable, anonymousArguments);
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0001C840 File Offset: 0x0001AA40
		protected internal void DropForeignKey(string dependentTable, string[] dependentColumns, string principalTable, object anonymousArguments = null)
		{
			Check.NotEmpty(dependentTable, "dependentTable");
			Check.NotNull<string[]>(dependentColumns, "dependentColumns");
			Check.NotEmpty(principalTable, "principalTable");
			if (!dependentColumns.Any<string>())
			{
				throw new ArgumentException(Strings.CollectionEmpty("dependentColumns", "DropForeignKey"));
			}
			DropForeignKeyOperation dropForeignKeyOperation = new DropForeignKeyOperation(anonymousArguments)
			{
				DependentTable = dependentTable,
				PrincipalTable = principalTable
			};
			dependentColumns.Each(delegate(string c)
			{
				dropForeignKeyOperation.DependentColumns.Add(c);
			});
			this.AddOperation(dropForeignKeyOperation);
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0001C8CD File Offset: 0x0001AACD
		protected internal void DropTable(string name, object anonymousArguments = null)
		{
			Check.NotEmpty(name, "name");
			this.DropTable(name, null, null, anonymousArguments);
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0001C8E5 File Offset: 0x0001AAE5
		protected internal void DropTable(string name, IDictionary<string, IDictionary<string, object>> removedColumnAnnotations, object anonymousArguments = null)
		{
			Check.NotEmpty(name, "name");
			this.DropTable(name, null, removedColumnAnnotations, anonymousArguments);
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x0001C8FD File Offset: 0x0001AAFD
		protected internal void DropTable(string name, IDictionary<string, object> removedAnnotations, object anonymousArguments = null)
		{
			Check.NotEmpty(name, "name");
			this.DropTable(name, removedAnnotations, null, anonymousArguments);
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0001C915 File Offset: 0x0001AB15
		protected internal void DropTable(string name, IDictionary<string, object> removedAnnotations, IDictionary<string, IDictionary<string, object>> removedColumnAnnotations, object anonymousArguments = null)
		{
			Check.NotEmpty(name, "name");
			this.AddOperation(new DropTableOperation(name, removedAnnotations, removedColumnAnnotations, anonymousArguments));
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x0001C933 File Offset: 0x0001AB33
		protected internal void MoveTable(string name, string newSchema, object anonymousArguments = null)
		{
			Check.NotEmpty(name, "name");
			this.AddOperation(new MoveTableOperation(name, newSchema, anonymousArguments));
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0001C94F File Offset: 0x0001AB4F
		protected internal void MoveStoredProcedure(string name, string newSchema, object anonymousArguments = null)
		{
			Check.NotEmpty(name, "name");
			this.AddOperation(new MoveProcedureOperation(name, newSchema, anonymousArguments));
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0001C96B File Offset: 0x0001AB6B
		protected internal void RenameTable(string name, string newName, object anonymousArguments = null)
		{
			Check.NotEmpty(name, "name");
			Check.NotEmpty(newName, "newName");
			this.AddOperation(new RenameTableOperation(name, newName, anonymousArguments));
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x0001C993 File Offset: 0x0001AB93
		protected internal void RenameStoredProcedure(string name, string newName, object anonymousArguments = null)
		{
			Check.NotEmpty(name, "name");
			Check.NotEmpty(newName, "newName");
			this.AddOperation(new RenameProcedureOperation(name, newName, anonymousArguments));
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x0001C9BB File Offset: 0x0001ABBB
		protected internal void RenameColumn(string table, string name, string newName, object anonymousArguments = null)
		{
			Check.NotEmpty(table, "table");
			Check.NotEmpty(name, "name");
			Check.NotEmpty(newName, "newName");
			this.AddOperation(new RenameColumnOperation(table, name, newName, anonymousArguments));
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x0001C9F4 File Offset: 0x0001ABF4
		protected internal void AddColumn(string table, string name, Func<ColumnBuilder, ColumnModel> columnAction, object anonymousArguments = null)
		{
			Check.NotEmpty(table, "table");
			Check.NotEmpty(name, "name");
			Check.NotNull<Func<ColumnBuilder, ColumnModel>>(columnAction, "columnAction");
			ColumnModel columnModel = columnAction(new ColumnBuilder());
			columnModel.Name = name;
			this.AddOperation(new AddColumnOperation(table, columnModel, anonymousArguments));
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x0001CA47 File Offset: 0x0001AC47
		protected internal void DropColumn(string table, string name, object anonymousArguments = null)
		{
			Check.NotEmpty(table, "table");
			Check.NotEmpty(name, "name");
			this.DropColumn(table, name, null, anonymousArguments);
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x0001CA6B File Offset: 0x0001AC6B
		protected internal void DropColumn(string table, string name, IDictionary<string, object> removedAnnotations, object anonymousArguments = null)
		{
			Check.NotEmpty(table, "table");
			Check.NotEmpty(name, "name");
			this.AddOperation(new DropColumnOperation(table, name, removedAnnotations, anonymousArguments));
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x0001CA98 File Offset: 0x0001AC98
		protected internal void AlterColumn(string table, string name, Func<ColumnBuilder, ColumnModel> columnAction, object anonymousArguments = null)
		{
			Check.NotEmpty(table, "table");
			Check.NotEmpty(name, "name");
			Check.NotNull<Func<ColumnBuilder, ColumnModel>>(columnAction, "columnAction");
			ColumnModel columnModel = columnAction(new ColumnBuilder());
			columnModel.Name = name;
			this.AddOperation(new AlterColumnOperation(table, columnModel, false, anonymousArguments));
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x0001CAEC File Offset: 0x0001ACEC
		protected internal void AddPrimaryKey(string table, string column, string name = null, bool clustered = true, object anonymousArguments = null)
		{
			Check.NotEmpty(table, "table");
			Check.NotEmpty(column, "column");
			this.AddPrimaryKey(table, new string[] { column }, name, clustered, anonymousArguments);
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x0001CB1C File Offset: 0x0001AD1C
		protected internal void AddPrimaryKey(string table, string[] columns, string name = null, bool clustered = true, object anonymousArguments = null)
		{
			Check.NotEmpty(table, "table");
			Check.NotNull<string[]>(columns, "columns");
			if (!columns.Any<string>())
			{
				throw new ArgumentException(Strings.CollectionEmpty("columns", "AddPrimaryKey"));
			}
			AddPrimaryKeyOperation addPrimaryKeyOperation = new AddPrimaryKeyOperation(anonymousArguments)
			{
				Table = table,
				Name = name,
				IsClustered = clustered
			};
			columns.Each(delegate(string c)
			{
				addPrimaryKeyOperation.Columns.Add(c);
			});
			this.AddOperation(addPrimaryKeyOperation);
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x0001CBA8 File Offset: 0x0001ADA8
		protected internal void DropPrimaryKey(string table, string name, object anonymousArguments = null)
		{
			Check.NotEmpty(table, "table");
			Check.NotEmpty(name, "name");
			DropPrimaryKeyOperation dropPrimaryKeyOperation = new DropPrimaryKeyOperation(anonymousArguments)
			{
				Table = table,
				Name = name
			};
			this.AddOperation(dropPrimaryKeyOperation);
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x0001CBEC File Offset: 0x0001ADEC
		protected internal void DropPrimaryKey(string table, object anonymousArguments = null)
		{
			Check.NotEmpty(table, "table");
			DropPrimaryKeyOperation dropPrimaryKeyOperation = new DropPrimaryKeyOperation(anonymousArguments)
			{
				Table = table
			};
			this.AddOperation(dropPrimaryKeyOperation);
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x0001CC1A File Offset: 0x0001AE1A
		protected internal void CreateIndex(string table, string column, bool unique = false, string name = null, bool clustered = false, object anonymousArguments = null)
		{
			Check.NotEmpty(table, "table");
			Check.NotEmpty(column, "column");
			this.CreateIndex(table, new string[] { column }, unique, name, clustered, anonymousArguments);
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x0001CC4C File Offset: 0x0001AE4C
		protected internal void CreateIndex(string table, string[] columns, bool unique = false, string name = null, bool clustered = false, object anonymousArguments = null)
		{
			Check.NotEmpty(table, "table");
			Check.NotNull<string[]>(columns, "columns");
			if (!columns.Any<string>())
			{
				throw new ArgumentException(Strings.CollectionEmpty("columns", "CreateIndex"));
			}
			CreateIndexOperation createIndexOperation = new CreateIndexOperation(anonymousArguments)
			{
				Table = table,
				IsUnique = unique,
				Name = name,
				IsClustered = clustered
			};
			columns.Each(delegate(string c)
			{
				createIndexOperation.Columns.Add(c);
			});
			this.AddOperation(createIndexOperation);
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x0001CCE0 File Offset: 0x0001AEE0
		protected internal void DropIndex(string table, string name, object anonymousArguments = null)
		{
			Check.NotEmpty(table, "table");
			Check.NotEmpty(name, "name");
			DropIndexOperation dropIndexOperation = new DropIndexOperation(anonymousArguments)
			{
				Table = table,
				Name = name
			};
			this.AddOperation(dropIndexOperation);
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x0001CD24 File Offset: 0x0001AF24
		protected internal void DropIndex(string table, string[] columns, object anonymousArguments = null)
		{
			Check.NotEmpty(table, "table");
			Check.NotNull<string[]>(columns, "columns");
			if (!columns.Any<string>())
			{
				throw new ArgumentException(Strings.CollectionEmpty("columns", "DropIndex"));
			}
			DropIndexOperation dropIndexOperation = new DropIndexOperation(anonymousArguments)
			{
				Table = table
			};
			columns.Each(delegate(string c)
			{
				dropIndexOperation.Columns.Add(c);
			});
			this.AddOperation(dropIndexOperation);
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x0001CD9D File Offset: 0x0001AF9D
		protected internal void RenameIndex(string table, string name, string newName, object anonymousArguments = null)
		{
			Check.NotEmpty(table, "table");
			Check.NotEmpty(name, "name");
			Check.NotEmpty(newName, "newName");
			this.AddOperation(new RenameIndexOperation(table, name, newName, anonymousArguments));
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0001CDD3 File Offset: 0x0001AFD3
		protected internal void Sql(string sql, bool suppressTransaction = false, object anonymousArguments = null)
		{
			Check.NotEmpty(sql, "sql");
			this.AddOperation(new SqlOperation(sql, anonymousArguments)
			{
				SuppressTransaction = suppressTransaction
			});
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x0001CDF8 File Offset: 0x0001AFF8
		protected internal void SqlFile(string sqlFile, bool suppressTransaction = false, object anonymousArguments = null)
		{
			Check.NotEmpty(sqlFile, "sqlFile");
			if (!Path.IsPathRooted(sqlFile))
			{
				sqlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, sqlFile);
			}
			this.AddOperation(new SqlOperation(File.ReadAllText(sqlFile), anonymousArguments)
			{
				SuppressTransaction = suppressTransaction
			});
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0001CE44 File Offset: 0x0001B044
		protected internal void SqlResource(string sqlResource, Assembly resourceAssembly = null, bool suppressTransaction = false, object anonymousArguments = null)
		{
			Check.NotEmpty(sqlResource, "sqlResource");
			resourceAssembly = resourceAssembly ?? Assembly.GetCallingAssembly();
			if (!resourceAssembly.GetManifestResourceNames().Contains(sqlResource))
			{
				throw new ArgumentException(Strings.UnableToLoadEmbeddedResource(resourceAssembly.FullName, sqlResource));
			}
			using (StreamReader streamReader = new StreamReader(resourceAssembly.GetManifestResourceStream(sqlResource)))
			{
				this.AddOperation(new SqlOperation(streamReader.ReadToEnd(), anonymousArguments)
				{
					SuppressTransaction = suppressTransaction
				});
			}
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x0001CECC File Offset: 0x0001B0CC
		void IDbMigration.AddOperation(MigrationOperation migrationOperation)
		{
			this.AddOperation(migrationOperation);
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x0001CED5 File Offset: 0x0001B0D5
		internal void AddOperation(MigrationOperation migrationOperation)
		{
			Check.NotNull<MigrationOperation>(migrationOperation, "migrationOperation");
			this._operations.Add(migrationOperation);
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000E52 RID: 3666 RVA: 0x0001CEEF File Offset: 0x0001B0EF
		internal IEnumerable<MigrationOperation> Operations
		{
			get
			{
				return this._operations;
			}
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x0001CEF7 File Offset: 0x0001B0F7
		internal void Reset()
		{
			this._operations.Clear();
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x0001CF04 File Offset: 0x0001B104
		internal VersionedModel GetSourceModel()
		{
			return this.GetModel((IMigrationMetadata mm) => mm.Source);
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x0001CF2B File Offset: 0x0001B12B
		internal VersionedModel GetTargetModel()
		{
			return this.GetModel((IMigrationMetadata mm) => mm.Target);
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x0001CF54 File Offset: 0x0001B154
		private VersionedModel GetModel(Func<IMigrationMetadata, string> modelAccessor)
		{
			IMigrationMetadata migrationMetadata = (IMigrationMetadata)this;
			string text = modelAccessor(migrationMetadata);
			if (string.IsNullOrWhiteSpace(text))
			{
				return null;
			}
			GeneratedCodeAttribute generatedCodeAttribute = this.GetType().GetCustomAttributes(false).SingleOrDefault<GeneratedCodeAttribute>();
			string text2 = ((generatedCodeAttribute != null && !string.IsNullOrWhiteSpace(generatedCodeAttribute.Version)) ? generatedCodeAttribute.Version : typeof(DbMigration).Assembly().GetInformationalVersion());
			return new VersionedModel(new ModelCompressor().Decompress(Convert.FromBase64String(text)), text2);
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x0001CFCF File Offset: 0x0001B1CF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x0001CFD7 File Offset: 0x0001B1D7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x0001CFE0 File Offset: 0x0001B1E0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x0001CFE8 File Offset: 0x0001B1E8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x0001CFF0 File Offset: 0x0001B1F0
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected new object MemberwiseClone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x0400080A RID: 2058
		private readonly List<MigrationOperation> _operations = new List<MigrationOperation>();
	}
}
