using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020005E9 RID: 1513
	[Serializable]
	internal sealed class ReportProcessingException_NonExistingDataSourceReference : Exception
	{
		// Token: 0x06005415 RID: 21525 RVA: 0x00161A17 File Offset: 0x0015FC17
		internal ReportProcessingException_NonExistingDataSourceReference(string dataSourceName)
			: base(string.Format(CultureInfo.CurrentCulture, RPResWrapper.rsNonExistingDataSourceReference(dataSourceName), Array.Empty<object>()))
		{
		}

		// Token: 0x06005416 RID: 21526 RVA: 0x00161A34 File Offset: 0x0015FC34
		private ReportProcessingException_NonExistingDataSourceReference(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
