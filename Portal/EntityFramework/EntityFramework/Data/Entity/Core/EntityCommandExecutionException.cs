using System;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core
{
	// Token: 0x020002CE RID: 718
	[Serializable]
	public sealed class EntityCommandExecutionException : EntityException
	{
		// Token: 0x060022B2 RID: 8882 RVA: 0x00061FF7 File Offset: 0x000601F7
		public EntityCommandExecutionException()
		{
			base.HResult = -2146232004;
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x0006200A File Offset: 0x0006020A
		public EntityCommandExecutionException(string message)
			: base(message)
		{
			base.HResult = -2146232004;
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x0006201E File Offset: 0x0006021E
		public EntityCommandExecutionException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146232004;
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x00062033 File Offset: 0x00060233
		private EntityCommandExecutionException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
			base.HResult = -2146232004;
		}

		// Token: 0x04000BF6 RID: 3062
		private const int HResultCommandExecution = -2146232004;
	}
}
