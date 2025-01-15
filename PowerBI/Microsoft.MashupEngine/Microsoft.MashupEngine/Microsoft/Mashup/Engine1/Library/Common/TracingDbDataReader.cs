using System;
using System.Data.Common;
using System.Diagnostics;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine.Interface.Tracing;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001154 RID: 4436
	internal class TracingDbDataReader : DelegatingDbDataReaderWithTableSchema
	{
		// Token: 0x0600742E RID: 29742 RVA: 0x0018F34C File Offset: 0x0018D54C
		public TracingDbDataReader(Tracer tracer, DbDataReaderWithTableSchema reader)
			: base(reader)
		{
			this.trace = tracer.CreatePerformanceTrace("DbDataReader", TraceEventType.Information);
			this.rowCount = 0L;
		}

		// Token: 0x17002055 RID: 8277
		// (get) Token: 0x0600742F RID: 29743 RVA: 0x0018F36F File Offset: 0x0018D56F
		public IHostTrace Trace
		{
			get
			{
				return this.trace;
			}
		}

		// Token: 0x06007430 RID: 29744 RVA: 0x0018F377 File Offset: 0x0018D577
		public override bool Read()
		{
			this.rowCount += 1L;
			return base.Read();
		}

		// Token: 0x06007431 RID: 29745 RVA: 0x0018F390 File Offset: 0x0018D590
		protected override void Dispose(bool disposing)
		{
			try
			{
				base.Dispose(disposing);
			}
			catch (DbException ex)
			{
				this.trace.Add(ex, true);
			}
			finally
			{
				if (disposing && this.trace != null)
				{
					this.trace.Add("RowCount", this.rowCount - 1L, false);
					this.trace.Dispose();
					this.trace = null;
				}
			}
		}

		// Token: 0x04003FF0 RID: 16368
		private IHostTrace trace;

		// Token: 0x04003FF1 RID: 16369
		private long rowCount;
	}
}
