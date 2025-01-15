using System;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200006B RID: 107
	[Serializable]
	public sealed class AdomdUnknownResponseException : AdomdException
	{
		// Token: 0x060006F6 RID: 1782 RVA: 0x000237D9 File Offset: 0x000219D9
		internal AdomdUnknownResponseException(Exception e)
			: base(XmlaSR.UnknownServerResponseFormat, e)
		{
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x000237E7 File Offset: 0x000219E7
		private AdomdUnknownResponseException()
		{
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x000237EF File Offset: 0x000219EF
		internal AdomdUnknownResponseException(string message, string debugMessage)
			: base(AdomdUnknownResponseException.GetExceptionMessage(message, debugMessage))
		{
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x000237FE File Offset: 0x000219FE
		internal AdomdUnknownResponseException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00023808 File Offset: 0x00021A08
		private AdomdUnknownResponseException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00023812 File Offset: 0x00021A12
		private static string GetExceptionMessage(string message, string debugMessage)
		{
			return message;
		}
	}
}
