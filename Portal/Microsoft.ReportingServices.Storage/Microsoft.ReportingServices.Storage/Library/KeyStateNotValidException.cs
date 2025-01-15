using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Exceptions;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000022 RID: 34
	[Serializable]
	internal sealed class KeyStateNotValidException : ReportCatalogException
	{
		// Token: 0x0600010F RID: 271 RVA: 0x00007DC8 File Offset: 0x00005FC8
		public KeyStateNotValidException(Exception innerException)
			: base(ErrorCode.rsKeyStateNotValid, ErrorStrings.rsKeyStateNotValid, innerException, null, Array.Empty<object>())
		{
			RSEventLog.Current.WriteError(Event.IsDisabled, Array.Empty<object>());
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00007D2D File Offset: 0x00005F2D
		private KeyStateNotValidException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
