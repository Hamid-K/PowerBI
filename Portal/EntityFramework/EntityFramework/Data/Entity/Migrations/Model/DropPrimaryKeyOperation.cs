using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020000B7 RID: 183
	public class DropPrimaryKeyOperation : PrimaryKeyOperation
	{
		// Token: 0x06000F57 RID: 3927 RVA: 0x0002068A File Offset: 0x0001E88A
		public DropPrimaryKeyOperation(object anonymousArguments = null)
			: base(anonymousArguments)
		{
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x00020694 File Offset: 0x0001E894
		public override MigrationOperation Inverse
		{
			get
			{
				AddPrimaryKeyOperation addPrimaryKeyOperation = new AddPrimaryKeyOperation(null)
				{
					Name = base.Name,
					Table = base.Table,
					IsClustered = base.IsClustered
				};
				base.Columns.Each(delegate(string c)
				{
					addPrimaryKeyOperation.Columns.Add(c);
				});
				return addPrimaryKeyOperation;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000F59 RID: 3929 RVA: 0x000206F4 File Offset: 0x0001E8F4
		// (set) Token: 0x06000F5A RID: 3930 RVA: 0x000206FC File Offset: 0x0001E8FC
		public CreateTableOperation CreateTableOperation { get; internal set; }
	}
}
