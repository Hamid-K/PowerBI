using System;

namespace Microsoft.ReportingServices.Portal.Interfaces.Exceptions
{
	// Token: 0x020000AF RID: 175
	public sealed class FailedToSetDataModelParameterException : Exception
	{
		// Token: 0x06000578 RID: 1400 RVA: 0x00004B5A File Offset: 0x00002D5A
		public FailedToSetDataModelParameterException(string message)
			: base(message)
		{
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00004B63 File Offset: 0x00002D63
		public FailedToSetDataModelParameterException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
