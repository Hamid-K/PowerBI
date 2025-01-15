using System;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200002D RID: 45
	[Serializable]
	public sealed class ResponseFormatException : AmoException
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x0000596F File Offset: 0x00003B6F
		public ResponseFormatException(string message)
			: base(message)
		{
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00005978 File Offset: 0x00003B78
		public ResponseFormatException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00005982 File Offset: 0x00003B82
		internal ResponseFormatException(Exception e)
			: base(XmlaSR.UnknownServerResponseFormat, e)
		{
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00005990 File Offset: 0x00003B90
		internal ResponseFormatException(string message, string debugMessage)
			: base(ResponseFormatException.GetExceptionMessage(message, debugMessage))
		{
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000599F File Offset: 0x00003B9F
		private ResponseFormatException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000059A9 File Offset: 0x00003BA9
		private static string GetExceptionMessage(string message, string debugMessage)
		{
			return message;
		}
	}
}
