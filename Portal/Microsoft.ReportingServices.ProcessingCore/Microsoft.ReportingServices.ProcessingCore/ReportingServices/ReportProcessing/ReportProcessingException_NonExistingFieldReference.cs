using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020005E2 RID: 1506
	[Serializable]
	internal sealed class ReportProcessingException_NonExistingFieldReference : Exception
	{
		// Token: 0x06005405 RID: 21509 RVA: 0x0016190E File Offset: 0x0015FB0E
		internal ReportProcessingException_NonExistingFieldReference(string fieldName)
			: base(string.Format(CultureInfo.CurrentCulture, RPResWrapper.rsNonExistingFieldReferenceByName(fieldName), Array.Empty<object>()))
		{
		}

		// Token: 0x06005406 RID: 21510 RVA: 0x0016192B File Offset: 0x0015FB2B
		internal ReportProcessingException_NonExistingFieldReference()
			: base(string.Format(CultureInfo.CurrentCulture, RPRes.rsNonExistingFieldReference, Array.Empty<object>()))
		{
		}

		// Token: 0x06005407 RID: 21511 RVA: 0x00161947 File Offset: 0x0015FB47
		private ReportProcessingException_NonExistingFieldReference(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
