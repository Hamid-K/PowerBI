using System;
using System.Runtime.Serialization;

namespace antlr
{
	// Token: 0x02000003 RID: 3
	[Serializable]
	internal class ANTLRException : Exception
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000029B3 File Offset: 0x00000BB3
		public ANTLRException()
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000029BB File Offset: 0x00000BBB
		public ANTLRException(string s)
			: base(s)
		{
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000029C4 File Offset: 0x00000BC4
		public ANTLRException(string s, Exception inner)
			: base(s, inner)
		{
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000029CE File Offset: 0x00000BCE
		protected ANTLRException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
