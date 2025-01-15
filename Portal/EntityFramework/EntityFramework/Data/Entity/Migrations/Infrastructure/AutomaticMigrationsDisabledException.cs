using System;
using System.Runtime.Serialization;

namespace System.Data.Entity.Migrations.Infrastructure
{
	// Token: 0x020000CD RID: 205
	[Serializable]
	public sealed class AutomaticMigrationsDisabledException : MigrationsException
	{
		// Token: 0x06000FEA RID: 4074 RVA: 0x0002133F File Offset: 0x0001F53F
		public AutomaticMigrationsDisabledException()
		{
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x00021347 File Offset: 0x0001F547
		public AutomaticMigrationsDisabledException(string message)
			: base(message)
		{
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x00021350 File Offset: 0x0001F550
		public AutomaticMigrationsDisabledException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x0002135A File Offset: 0x0001F55A
		private AutomaticMigrationsDisabledException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
