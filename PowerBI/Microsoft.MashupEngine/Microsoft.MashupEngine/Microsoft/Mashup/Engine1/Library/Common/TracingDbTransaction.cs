using System;
using System.Data.Common;
using Microsoft.Mashup.Engine.Interface.Tracing;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001157 RID: 4439
	internal sealed class TracingDbTransaction : DelegatingDbTransaction
	{
		// Token: 0x06007439 RID: 29753 RVA: 0x0018F4DE File Offset: 0x0018D6DE
		public TracingDbTransaction(Tracer tracer, DbTransaction transaction)
			: base(transaction)
		{
			this.tracer = tracer;
		}

		// Token: 0x0600743A RID: 29754 RVA: 0x0018F4F0 File Offset: 0x0018D6F0
		public static DbTransaction RemoveTracing(DbTransaction transaction)
		{
			TracingDbTransaction tracingDbTransaction = transaction as TracingDbTransaction;
			if (tracingDbTransaction != null)
			{
				return tracingDbTransaction.InnerTransaction;
			}
			return transaction;
		}

		// Token: 0x0600743B RID: 29755 RVA: 0x0018F50F File Offset: 0x0018D70F
		public override void Commit()
		{
			this.tracer.Trace("Transaction/Commit", delegate(IHostTrace trace)
			{
				base.Commit();
			});
		}

		// Token: 0x0600743C RID: 29756 RVA: 0x0018F52D File Offset: 0x0018D72D
		public override void Rollback()
		{
			this.tracer.Trace("Transaction/Rollback", delegate(IHostTrace trace)
			{
				base.Rollback();
			});
		}

		// Token: 0x04003FF6 RID: 16374
		private readonly Tracer tracer;
	}
}
