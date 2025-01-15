using System;
using System.ComponentModel;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Migrations.Builders
{
	// Token: 0x020000EB RID: 235
	public class TableBuilder<TColumns>
	{
		// Token: 0x060011E2 RID: 4578 RVA: 0x0002E509 File Offset: 0x0002C709
		public TableBuilder(CreateTableOperation createTableOperation, DbMigration migration)
		{
			Check.NotNull<CreateTableOperation>(createTableOperation, "createTableOperation");
			this._createTableOperation = createTableOperation;
			this._migration = migration;
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x0002E52C File Offset: 0x0002C72C
		public TableBuilder<TColumns> PrimaryKey(Expression<Func<TColumns, object>> keyExpression, string name = null, bool clustered = true, object anonymousArguments = null)
		{
			Check.NotNull<Expression<Func<TColumns, object>>>(keyExpression, "keyExpression");
			AddPrimaryKeyOperation addPrimaryKeyOperation = new AddPrimaryKeyOperation(anonymousArguments)
			{
				Name = name,
				IsClustered = clustered
			};
			(from p in keyExpression.GetSimplePropertyAccessList()
				select this._createTableOperation.Columns.Single((ColumnModel c) => c.ApiPropertyInfo == p.Single<PropertyInfo>())).Each(delegate(ColumnModel c)
			{
				addPrimaryKeyOperation.Columns.Add(c.Name);
			});
			this._createTableOperation.PrimaryKey = addPrimaryKeyOperation;
			return this;
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x0002E5A8 File Offset: 0x0002C7A8
		public TableBuilder<TColumns> Index(Expression<Func<TColumns, object>> indexExpression, string name = null, bool unique = false, bool clustered = false, object anonymousArguments = null)
		{
			Check.NotNull<Expression<Func<TColumns, object>>>(indexExpression, "indexExpression");
			CreateIndexOperation createIndexOperation = new CreateIndexOperation(anonymousArguments)
			{
				Name = name,
				Table = this._createTableOperation.Name,
				IsUnique = unique,
				IsClustered = clustered
			};
			(from p in indexExpression.GetSimplePropertyAccessList()
				select this._createTableOperation.Columns.Single((ColumnModel c) => c.ApiPropertyInfo == p.Single<PropertyInfo>())).Each(delegate(ColumnModel c)
			{
				createIndexOperation.Columns.Add(c.Name);
			});
			this._migration.AddOperation(createIndexOperation);
			return this;
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x0002E63C File Offset: 0x0002C83C
		public TableBuilder<TColumns> ForeignKey(string principalTable, Expression<Func<TColumns, object>> dependentKeyExpression, bool cascadeDelete = false, string name = null, object anonymousArguments = null)
		{
			Check.NotEmpty(principalTable, "principalTable");
			Check.NotNull<Expression<Func<TColumns, object>>>(dependentKeyExpression, "dependentKeyExpression");
			AddForeignKeyOperation addForeignKeyOperation = new AddForeignKeyOperation(anonymousArguments)
			{
				Name = name,
				PrincipalTable = principalTable,
				DependentTable = this._createTableOperation.Name,
				CascadeDelete = cascadeDelete
			};
			(from p in dependentKeyExpression.GetSimplePropertyAccessList()
				select this._createTableOperation.Columns.Single((ColumnModel c) => c.ApiPropertyInfo == p.Single<PropertyInfo>())).Each(delegate(ColumnModel c)
			{
				addForeignKeyOperation.DependentColumns.Add(c.Name);
			});
			this._migration.AddOperation(addForeignKeyOperation);
			return this;
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x0002E6DC File Offset: 0x0002C8DC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x0002E6E4 File Offset: 0x0002C8E4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x0002E6ED File Offset: 0x0002C8ED
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x0002E6F5 File Offset: 0x0002C8F5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x0002E6FD File Offset: 0x0002C8FD
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected new object MemberwiseClone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x040008EC RID: 2284
		private readonly CreateTableOperation _createTableOperation;

		// Token: 0x040008ED RID: 2285
		private readonly DbMigration _migration;
	}
}
