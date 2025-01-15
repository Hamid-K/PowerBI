using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000070 RID: 112
	[Serializable]
	internal sealed class RoleNotFoundException : ReportCatalogException
	{
		// Token: 0x06000216 RID: 534 RVA: 0x00004A03 File Offset: 0x00002C03
		public RoleNotFoundException(string roleName)
			: base(ErrorCode.rsRoleNotFound, ErrorStringsWrapper.rsRoleNotFound(roleName), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00004A1A File Offset: 0x00002C1A
		private RoleNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
