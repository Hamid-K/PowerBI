using System;
using System.Data.Entity.Core;
using System.Data.Entity.Internal;
using System.Runtime.Serialization;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000239 RID: 569
	[Serializable]
	public class DbUpdateConcurrencyException : DbUpdateException
	{
		// Token: 0x06001E1A RID: 7706 RVA: 0x0005421B File Offset: 0x0005241B
		internal DbUpdateConcurrencyException(InternalContext context, OptimisticConcurrencyException innerException)
			: base(context, innerException, false)
		{
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x00054226 File Offset: 0x00052426
		public DbUpdateConcurrencyException()
		{
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x0005422E File Offset: 0x0005242E
		public DbUpdateConcurrencyException(string message)
			: base(message)
		{
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x00054237 File Offset: 0x00052437
		public DbUpdateConcurrencyException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x00054241 File Offset: 0x00052441
		protected DbUpdateConcurrencyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
