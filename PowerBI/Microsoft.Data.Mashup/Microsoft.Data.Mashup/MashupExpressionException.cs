using System;
using System.Runtime.Serialization;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000030 RID: 48
	[Serializable]
	public class MashupExpressionException : MashupException
	{
		// Token: 0x06000287 RID: 647 RVA: 0x0000AE61 File Offset: 0x00009061
		public MashupExpressionException(string message)
			: base(message)
		{
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000AE6A File Offset: 0x0000906A
		public MashupExpressionException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000AE74 File Offset: 0x00009074
		protected MashupExpressionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
