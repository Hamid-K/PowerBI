using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000069 RID: 105
	[Serializable]
	internal sealed class ReservedRoleException : ReportCatalogException
	{
		// Token: 0x06000207 RID: 519 RVA: 0x00004906 File Offset: 0x00002B06
		public ReservedRoleException(string roleName)
			: base(ErrorCode.rsReservedRole, ErrorStringsWrapper.rsReservedRole(roleName), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000491D File Offset: 0x00002B1D
		private ReservedRoleException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
