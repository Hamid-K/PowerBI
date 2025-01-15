using System;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020000C1 RID: 193
	public class NotSupportedOperation : MigrationOperation
	{
		// Token: 0x06000F94 RID: 3988 RVA: 0x00020B6D File Offset: 0x0001ED6D
		private NotSupportedOperation()
			: base(null)
		{
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x00020B76 File Offset: 0x0001ED76
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000876 RID: 2166
		internal static readonly NotSupportedOperation Instance = new NotSupportedOperation();
	}
}
