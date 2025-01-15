using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020005EA RID: 1514
	[Serializable]
	internal sealed class ReportProcessingException_NonExistingVariableReference : Exception
	{
		// Token: 0x06005417 RID: 21527 RVA: 0x00161A3E File Offset: 0x0015FC3E
		internal ReportProcessingException_NonExistingVariableReference(string varName)
			: base(string.Format(CultureInfo.CurrentCulture, RPResWrapper.rsNonExistingVariableReference(varName), Array.Empty<object>()))
		{
		}

		// Token: 0x06005418 RID: 21528 RVA: 0x00161A5B File Offset: 0x0015FC5B
		private ReportProcessingException_NonExistingVariableReference(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
