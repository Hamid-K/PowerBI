using System;
using System.Data.Entity.Resources;
using System.Runtime.Serialization;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000268 RID: 616
	[Serializable]
	public class UnintentionalCodeFirstException : InvalidOperationException
	{
		// Token: 0x06001F53 RID: 8019 RVA: 0x00056C95 File Offset: 0x00054E95
		public UnintentionalCodeFirstException()
			: base(Strings.UnintentionalCodeFirstException_Message)
		{
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x00056CA2 File Offset: 0x00054EA2
		protected UnintentionalCodeFirstException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x00056CAC File Offset: 0x00054EAC
		public UnintentionalCodeFirstException(string message)
			: base(message)
		{
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x00056CB5 File Offset: 0x00054EB5
		public UnintentionalCodeFirstException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
