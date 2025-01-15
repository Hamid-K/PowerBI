using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000161 RID: 353
	[Serializable]
	internal sealed class PhaseOneBatchException : Exception
	{
		// Token: 0x0600210E RID: 8462 RVA: 0x0015C598 File Offset: 0x0015A798
		public PhaseOneBatchException()
		{
		}

		// Token: 0x0600210F RID: 8463 RVA: 0x0015C5A0 File Offset: 0x0015A7A0
		private PhaseOneBatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
