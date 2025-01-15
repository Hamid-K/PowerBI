using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020000B1 RID: 177
	public class CreateIndexOperation : IndexOperation
	{
		// Token: 0x06000F31 RID: 3889 RVA: 0x0002030E File Offset: 0x0001E50E
		public CreateIndexOperation(object anonymousArguments = null)
			: base(anonymousArguments)
		{
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000F32 RID: 3890 RVA: 0x00020317 File Offset: 0x0001E517
		// (set) Token: 0x06000F33 RID: 3891 RVA: 0x0002031F File Offset: 0x0001E51F
		public bool IsUnique { get; set; }

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000F34 RID: 3892 RVA: 0x00020328 File Offset: 0x0001E528
		public override MigrationOperation Inverse
		{
			get
			{
				DropIndexOperation dropIndexOperation = new DropIndexOperation(this, null)
				{
					Name = base.Name,
					Table = base.Table
				};
				base.Columns.Each(delegate(string c)
				{
					dropIndexOperation.Columns.Add(c);
				});
				return dropIndexOperation;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000F35 RID: 3893 RVA: 0x0002037D File Offset: 0x0001E57D
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x00020380 File Offset: 0x0001E580
		// (set) Token: 0x06000F37 RID: 3895 RVA: 0x00020388 File Offset: 0x0001E588
		public bool IsClustered { get; set; }
	}
}
