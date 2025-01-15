using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200004B RID: 75
	[Serializable]
	internal sealed class InvalidDataSetReferenceException : ReportCatalogException
	{
		// Token: 0x060001C0 RID: 448 RVA: 0x0000446A File Offset: 0x0000266A
		public InvalidDataSetReferenceException(string dataSetName)
			: base(ErrorCode.rsInvalidDataSetReference, ErrorStringsWrapper.rsInvalidDataSetReference(dataSetName), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00004484 File Offset: 0x00002684
		private InvalidDataSetReferenceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
