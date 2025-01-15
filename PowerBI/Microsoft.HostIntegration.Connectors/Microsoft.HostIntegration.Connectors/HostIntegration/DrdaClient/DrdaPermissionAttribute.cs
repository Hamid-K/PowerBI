using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A03 RID: 2563
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class DrdaPermissionAttribute : DBDataPermissionAttribute
	{
		// Token: 0x060050A8 RID: 20648 RVA: 0x00142D1B File Offset: 0x00140F1B
		public DrdaPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		// Token: 0x060050A9 RID: 20649 RVA: 0x00142D24 File Offset: 0x00140F24
		public override IPermission CreatePermission()
		{
			return new DrdaPermission(this);
		}
	}
}
