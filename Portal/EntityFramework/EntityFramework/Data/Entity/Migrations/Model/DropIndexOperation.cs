using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020000B6 RID: 182
	public class DropIndexOperation : IndexOperation
	{
		// Token: 0x06000F53 RID: 3923 RVA: 0x0002065A File Offset: 0x0001E85A
		public DropIndexOperation(object anonymousArguments = null)
			: base(anonymousArguments)
		{
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x00020663 File Offset: 0x0001E863
		public DropIndexOperation(CreateIndexOperation inverse, object anonymousArguments = null)
			: base(anonymousArguments)
		{
			Check.NotNull<CreateIndexOperation>(inverse, "inverse");
			this._inverse = inverse;
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x0002067F File Offset: 0x0001E87F
		public override MigrationOperation Inverse
		{
			get
			{
				return this._inverse;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x00020687 File Offset: 0x0001E887
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400085F RID: 2143
		private readonly CreateIndexOperation _inverse;
	}
}
