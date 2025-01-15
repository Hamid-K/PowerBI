using System;
using System.Runtime.Serialization;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000048 RID: 72
	[Serializable]
	public class MashupVersionException : MashupException
	{
		// Token: 0x0600037F RID: 895 RVA: 0x0000D8A1 File Offset: 0x0000BAA1
		public MashupVersionException(string message)
			: base(message)
		{
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000D8AA File Offset: 0x0000BAAA
		public MashupVersionException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000D8B4 File Offset: 0x0000BAB4
		protected MashupVersionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
