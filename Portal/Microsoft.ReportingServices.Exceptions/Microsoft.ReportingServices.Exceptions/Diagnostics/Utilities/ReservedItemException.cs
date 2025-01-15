using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000028 RID: 40
	[Serializable]
	internal sealed class ReservedItemException : ReportCatalogException
	{
		// Token: 0x0600017A RID: 378 RVA: 0x00003FDE File Offset: 0x000021DE
		public ReservedItemException(string itemPath)
			: base(ErrorCode.rsReservedItem, ErrorStringsWrapper.rsReservedItem(itemPath), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00003FF5 File Offset: 0x000021F5
		private ReservedItemException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
