using System;
using System.Runtime.Serialization;

namespace System.Data.Entity.Migrations.Infrastructure
{
	// Token: 0x020000D3 RID: 211
	[Serializable]
	public class MigrationsException : Exception
	{
		// Token: 0x0600106E RID: 4206 RVA: 0x00025639 File Offset: 0x00023839
		public MigrationsException()
		{
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x00025641 File Offset: 0x00023841
		public MigrationsException(string message)
			: base(message)
		{
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x0002564A File Offset: 0x0002384A
		public MigrationsException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x00025654 File Offset: 0x00023854
		protected MigrationsException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
