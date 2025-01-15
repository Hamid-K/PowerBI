using System;
using System.Runtime.Serialization;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000029 RID: 41
	[Serializable]
	public class MashupDeadlockException : MashupException
	{
		// Token: 0x06000257 RID: 599 RVA: 0x0000A259 File Offset: 0x00008459
		public MashupDeadlockException(string message)
			: base(message)
		{
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000A262 File Offset: 0x00008462
		protected MashupDeadlockException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
