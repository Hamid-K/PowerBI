using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020000B5 RID: 181
	public class DropForeignKeyOperation : ForeignKeyOperation
	{
		// Token: 0x06000F4E RID: 3918 RVA: 0x000205D7 File Offset: 0x0001E7D7
		public DropForeignKeyOperation(object anonymousArguments = null)
			: base(anonymousArguments)
		{
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x000205E0 File Offset: 0x0001E7E0
		public DropForeignKeyOperation(AddForeignKeyOperation inverse, object anonymousArguments = null)
			: base(anonymousArguments)
		{
			Check.NotNull<AddForeignKeyOperation>(inverse, "inverse");
			this._inverse = inverse;
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x000205FC File Offset: 0x0001E7FC
		public virtual DropIndexOperation CreateDropIndexOperation()
		{
			DropIndexOperation dropIndexOperation = new DropIndexOperation(this._inverse.CreateCreateIndexOperation(), null)
			{
				Table = base.DependentTable
			};
			base.DependentColumns.Each(delegate(string c)
			{
				dropIndexOperation.Columns.Add(c);
			});
			return dropIndexOperation;
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000F51 RID: 3921 RVA: 0x0002064F File Offset: 0x0001E84F
		public override MigrationOperation Inverse
		{
			get
			{
				return this._inverse;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000F52 RID: 3922 RVA: 0x00020657 File Offset: 0x0001E857
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400085E RID: 2142
		private readonly AddForeignKeyOperation _inverse;
	}
}
