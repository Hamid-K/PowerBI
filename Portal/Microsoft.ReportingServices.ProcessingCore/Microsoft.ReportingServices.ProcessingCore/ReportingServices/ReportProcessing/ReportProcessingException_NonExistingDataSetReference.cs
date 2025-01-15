using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020005E8 RID: 1512
	[Serializable]
	internal sealed class ReportProcessingException_NonExistingDataSetReference : Exception
	{
		// Token: 0x06005413 RID: 21523 RVA: 0x001619F0 File Offset: 0x0015FBF0
		internal ReportProcessingException_NonExistingDataSetReference(string dataSetName)
			: base(string.Format(CultureInfo.CurrentCulture, RPResWrapper.rsNonExistingDataSetReference(dataSetName), Array.Empty<object>()))
		{
		}

		// Token: 0x06005414 RID: 21524 RVA: 0x00161A0D File Offset: 0x0015FC0D
		private ReportProcessingException_NonExistingDataSetReference(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
