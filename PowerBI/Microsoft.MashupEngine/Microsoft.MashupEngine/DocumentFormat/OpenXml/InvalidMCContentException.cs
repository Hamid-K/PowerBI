using System;
using System.Runtime.Serialization;

namespace DocumentFormat.OpenXml
{
	// Token: 0x020020F0 RID: 8432
	[Serializable]
	internal sealed class InvalidMCContentException : Exception
	{
		// Token: 0x0600CF7F RID: 53119 RVA: 0x00005F33 File Offset: 0x00004133
		public InvalidMCContentException()
		{
		}

		// Token: 0x0600CF80 RID: 53120 RVA: 0x00002FDF File Offset: 0x000011DF
		public InvalidMCContentException(string message)
			: base(message)
		{
		}

		// Token: 0x0600CF81 RID: 53121 RVA: 0x00005F45 File Offset: 0x00004145
		private InvalidMCContentException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0600CF82 RID: 53122 RVA: 0x00005F3B File Offset: 0x0000413B
		public InvalidMCContentException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
