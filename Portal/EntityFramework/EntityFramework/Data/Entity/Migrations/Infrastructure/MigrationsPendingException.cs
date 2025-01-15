using System;
using System.Runtime.Serialization;

namespace System.Data.Entity.Migrations.Infrastructure
{
	// Token: 0x020000D5 RID: 213
	[Serializable]
	public sealed class MigrationsPendingException : MigrationsException
	{
		// Token: 0x06001076 RID: 4214 RVA: 0x00025666 File Offset: 0x00023866
		public MigrationsPendingException()
		{
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x0002566E File Offset: 0x0002386E
		public MigrationsPendingException(string message)
			: base(message)
		{
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x00025677 File Offset: 0x00023877
		public MigrationsPendingException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x00025681 File Offset: 0x00023881
		private MigrationsPendingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
