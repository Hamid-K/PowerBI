using System;
using System.Runtime.Serialization;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000021 RID: 33
	[Serializable]
	public class InternalMashupException : MashupException
	{
		// Token: 0x06000131 RID: 305 RVA: 0x00006A58 File Offset: 0x00004C58
		public InternalMashupException(string message)
			: base(message)
		{
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00006A61 File Offset: 0x00004C61
		public InternalMashupException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00006A6B File Offset: 0x00004C6B
		protected InternalMashupException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
