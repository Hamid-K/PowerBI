using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A02 RID: 2562
	[Serializable]
	public sealed class DrdaPermission : DBDataPermission
	{
		// Token: 0x060050A4 RID: 20644 RVA: 0x00142CF8 File Offset: 0x00140EF8
		public DrdaPermission(PermissionState state)
			: base(state)
		{
		}

		// Token: 0x060050A5 RID: 20645 RVA: 0x00142D01 File Offset: 0x00140F01
		private DrdaPermission(DrdaPermission permission)
			: base(permission)
		{
		}

		// Token: 0x060050A6 RID: 20646 RVA: 0x00142D0A File Offset: 0x00140F0A
		internal DrdaPermission(DrdaPermissionAttribute permissionAttribute)
			: base(permissionAttribute)
		{
		}

		// Token: 0x060050A7 RID: 20647 RVA: 0x00142D13 File Offset: 0x00140F13
		public override IPermission Copy()
		{
			return new DrdaPermission(this);
		}
	}
}
