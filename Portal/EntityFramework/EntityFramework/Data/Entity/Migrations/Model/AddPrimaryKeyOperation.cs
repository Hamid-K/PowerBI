using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020000AC RID: 172
	public class AddPrimaryKeyOperation : PrimaryKeyOperation
	{
		// Token: 0x06000F0D RID: 3853 RVA: 0x0001FC53 File Offset: 0x0001DE53
		public AddPrimaryKeyOperation(object anonymousArguments = null)
			: base(anonymousArguments)
		{
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x0001FC5C File Offset: 0x0001DE5C
		public override MigrationOperation Inverse
		{
			get
			{
				DropPrimaryKeyOperation dropPrimaryKeyOperation = new DropPrimaryKeyOperation(null)
				{
					Name = base.Name,
					Table = base.Table,
					IsClustered = base.IsClustered
				};
				base.Columns.Each(delegate(string c)
				{
					dropPrimaryKeyOperation.Columns.Add(c);
				});
				return dropPrimaryKeyOperation;
			}
		}
	}
}
