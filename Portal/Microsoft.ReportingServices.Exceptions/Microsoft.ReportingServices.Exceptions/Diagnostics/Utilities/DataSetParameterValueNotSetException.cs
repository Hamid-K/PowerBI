using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200003B RID: 59
	[Serializable]
	internal sealed class DataSetParameterValueNotSetException : ReportCatalogException
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x00004250 File Offset: 0x00002450
		public DataSetParameterValueNotSetException(string parameterName)
			: base(ErrorCode.rsDataSetParameterValueNotSet, ErrorStringsWrapper.rsDataSetParameterValueNotSet(parameterName), null, parameterName, Array.Empty<object>())
		{
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000426A File Offset: 0x0000246A
		private DataSetParameterValueNotSetException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
