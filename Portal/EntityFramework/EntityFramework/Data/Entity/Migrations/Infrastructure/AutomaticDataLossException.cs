using System;
using System.Data.Entity.Utilities;
using System.Runtime.Serialization;

namespace System.Data.Entity.Migrations.Infrastructure
{
	// Token: 0x020000CC RID: 204
	[Serializable]
	public sealed class AutomaticDataLossException : MigrationsException
	{
		// Token: 0x06000FE6 RID: 4070 RVA: 0x0002130E File Offset: 0x0001F50E
		public AutomaticDataLossException()
		{
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x00021316 File Offset: 0x0001F516
		public AutomaticDataLossException(string message)
			: base(message)
		{
			Check.NotEmpty(message, "message");
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x0002132B File Offset: 0x0001F52B
		public AutomaticDataLossException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x00021335 File Offset: 0x0001F535
		private AutomaticDataLossException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
