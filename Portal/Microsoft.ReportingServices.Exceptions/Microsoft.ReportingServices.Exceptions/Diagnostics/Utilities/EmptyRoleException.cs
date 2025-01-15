using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200006C RID: 108
	[Serializable]
	internal sealed class EmptyRoleException : ReportCatalogException
	{
		// Token: 0x0600020D RID: 525 RVA: 0x00004968 File Offset: 0x00002B68
		public EmptyRoleException()
			: base(ErrorCode.rsEmptyRole, ErrorStringsWrapper.rsEmptyRole, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000497E File Offset: 0x00002B7E
		private EmptyRoleException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
