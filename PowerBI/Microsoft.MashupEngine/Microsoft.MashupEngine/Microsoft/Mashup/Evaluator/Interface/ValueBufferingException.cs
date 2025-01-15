using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E35 RID: 7733
	[Serializable]
	public class ValueBufferingException : Exception
	{
		// Token: 0x0600BE4C RID: 48716 RVA: 0x00002FDF File Offset: 0x000011DF
		public ValueBufferingException(string message)
			: base(message)
		{
		}

		// Token: 0x0600BE4D RID: 48717 RVA: 0x00005F3B File Offset: 0x0000413B
		public ValueBufferingException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600BE4E RID: 48718 RVA: 0x00005F45 File Offset: 0x00004145
		protected ValueBufferingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
