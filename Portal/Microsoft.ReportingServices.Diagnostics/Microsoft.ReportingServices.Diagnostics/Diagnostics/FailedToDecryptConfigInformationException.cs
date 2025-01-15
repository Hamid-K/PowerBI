using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000049 RID: 73
	[Serializable]
	internal sealed class FailedToDecryptConfigInformationException : ReportCatalogException
	{
		// Token: 0x0600025C RID: 604 RVA: 0x0000B748 File Offset: 0x00009948
		public FailedToDecryptConfigInformationException(Exception innerException, string configElement)
			: base(ErrorCode.rsFailedToDecryptConfigInformation, ErrorStringsWrapper.rsFailedToDecryptConfigInformation(configElement), innerException, null, Array.Empty<object>())
		{
			RSEventLog.Current.WriteError(Event.FailedToDecryptDSN, new object[]
			{
				configElement,
				Globals.Configuration.ConfigFileName
			});
		}

		// Token: 0x0600025D RID: 605 RVA: 0x000022A7 File Offset: 0x000004A7
		private FailedToDecryptConfigInformationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
