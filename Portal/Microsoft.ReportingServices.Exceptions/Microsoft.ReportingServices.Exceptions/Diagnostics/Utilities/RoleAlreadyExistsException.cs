using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200006F RID: 111
	[Serializable]
	internal sealed class RoleAlreadyExistsException : ReportCatalogException
	{
		// Token: 0x06000214 RID: 532 RVA: 0x000049E2 File Offset: 0x00002BE2
		public RoleAlreadyExistsException(string roleName)
			: base(ErrorCode.rsRoleAlreadyExists, ErrorStringsWrapper.rsRoleAlreadyExists(roleName), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000049F9 File Offset: 0x00002BF9
		private RoleAlreadyExistsException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
