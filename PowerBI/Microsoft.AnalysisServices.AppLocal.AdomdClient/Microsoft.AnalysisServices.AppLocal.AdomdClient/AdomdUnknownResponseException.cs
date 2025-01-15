using System;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200006B RID: 107
	[Serializable]
	public sealed class AdomdUnknownResponseException : AdomdException
	{
		// Token: 0x06000703 RID: 1795 RVA: 0x00023B09 File Offset: 0x00021D09
		internal AdomdUnknownResponseException(Exception e)
			: base(XmlaSR.UnknownServerResponseFormat, e)
		{
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00023B17 File Offset: 0x00021D17
		private AdomdUnknownResponseException()
		{
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00023B1F File Offset: 0x00021D1F
		internal AdomdUnknownResponseException(string message, string debugMessage)
			: base(AdomdUnknownResponseException.GetExceptionMessage(message, debugMessage))
		{
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00023B2E File Offset: 0x00021D2E
		internal AdomdUnknownResponseException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00023B38 File Offset: 0x00021D38
		private AdomdUnknownResponseException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00023B42 File Offset: 0x00021D42
		private static string GetExceptionMessage(string message, string debugMessage)
		{
			return message;
		}
	}
}
