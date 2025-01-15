using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000DD RID: 221
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class SqlClientPermissionAttribute : DBDataPermissionAttribute
	{
		// Token: 0x06000F79 RID: 3961 RVA: 0x00033AC0 File Offset: 0x00031CC0
		public SqlClientPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x00033AC9 File Offset: 0x00031CC9
		public override IPermission CreatePermission()
		{
			return new SqlClientPermission(this);
		}
	}
}
