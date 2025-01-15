using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000C4 RID: 196
	[Serializable]
	internal sealed class RSAddinVersionMismatchException : ReportCatalogException
	{
		// Token: 0x060002D2 RID: 722 RVA: 0x000059F2 File Offset: 0x00003BF2
		public RSAddinVersionMismatchException()
			: base(ErrorCode.rsVersionMismatch, ErrorStringsWrapper.rsVersionMismatch, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00005A0B File Offset: 0x00003C0B
		private RSAddinVersionMismatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
