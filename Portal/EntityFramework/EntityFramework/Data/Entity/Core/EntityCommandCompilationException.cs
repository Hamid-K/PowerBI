using System;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core
{
	// Token: 0x020002CD RID: 717
	[Serializable]
	public sealed class EntityCommandCompilationException : EntityException
	{
		// Token: 0x060022AE RID: 8878 RVA: 0x00061FA6 File Offset: 0x000601A6
		public EntityCommandCompilationException()
		{
			base.HResult = -2146232005;
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x00061FB9 File Offset: 0x000601B9
		public EntityCommandCompilationException(string message)
			: base(message)
		{
			base.HResult = -2146232005;
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x00061FCD File Offset: 0x000601CD
		public EntityCommandCompilationException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146232005;
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x00061FE2 File Offset: 0x000601E2
		private EntityCommandCompilationException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
			base.HResult = -2146232005;
		}

		// Token: 0x04000BF5 RID: 3061
		private const int HResultCommandCompilation = -2146232005;
	}
}
