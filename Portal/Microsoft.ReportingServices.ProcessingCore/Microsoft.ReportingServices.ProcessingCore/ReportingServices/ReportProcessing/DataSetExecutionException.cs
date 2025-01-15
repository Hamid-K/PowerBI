using System;
using System.Globalization;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200065A RID: 1626
	[Serializable]
	internal sealed class DataSetExecutionException : RSException
	{
		// Token: 0x06005A6C RID: 23148 RVA: 0x001729C5 File Offset: 0x00170BC5
		internal DataSetExecutionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06005A6D RID: 23149 RVA: 0x001729CF File Offset: 0x00170BCF
		internal DataSetExecutionException(ErrorCode code)
			: base(code, RPResWrapper.Keys.GetString(code.ToString()), null, Global.Tracer, null, Array.Empty<object>())
		{
		}

		// Token: 0x06005A6E RID: 23150 RVA: 0x001729F6 File Offset: 0x00170BF6
		internal DataSetExecutionException(string dataSetName, Exception innerException)
			: base(ErrorCode.rsDataSetExecutionError, string.Format(CultureInfo.CurrentCulture, ErrorStringsWrapper.rsDataSetExecutionError(dataSetName), Array.Empty<object>()), innerException, Global.Tracer, null, Array.Empty<object>())
		{
		}
	}
}
