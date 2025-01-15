using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020000AB RID: 171
	public class AddForeignKeyOperation : ForeignKeyOperation
	{
		// Token: 0x06000F06 RID: 3846 RVA: 0x0001FB7B File Offset: 0x0001DD7B
		public AddForeignKeyOperation(object anonymousArguments = null)
			: base(anonymousArguments)
		{
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x0001FB8F File Offset: 0x0001DD8F
		public IList<string> PrincipalColumns
		{
			get
			{
				return this._principalColumns;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x0001FB97 File Offset: 0x0001DD97
		// (set) Token: 0x06000F09 RID: 3849 RVA: 0x0001FB9F File Offset: 0x0001DD9F
		public bool CascadeDelete { get; set; }

		// Token: 0x06000F0A RID: 3850 RVA: 0x0001FBA8 File Offset: 0x0001DDA8
		public virtual CreateIndexOperation CreateCreateIndexOperation()
		{
			CreateIndexOperation createIndexOperation = new CreateIndexOperation(null)
			{
				Table = base.DependentTable
			};
			base.DependentColumns.Each(delegate(string c)
			{
				createIndexOperation.Columns.Add(c);
			});
			return createIndexOperation;
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000F0B RID: 3851 RVA: 0x0001FBF0 File Offset: 0x0001DDF0
		public override MigrationOperation Inverse
		{
			get
			{
				DropForeignKeyOperation dropForeignKeyOperation = new DropForeignKeyOperation(null)
				{
					Name = base.Name,
					PrincipalTable = base.PrincipalTable,
					DependentTable = base.DependentTable
				};
				base.DependentColumns.Each(delegate(string c)
				{
					dropForeignKeyOperation.DependentColumns.Add(c);
				});
				return dropForeignKeyOperation;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x0001FC50 File Offset: 0x0001DE50
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000844 RID: 2116
		private readonly List<string> _principalColumns = new List<string>();
	}
}
