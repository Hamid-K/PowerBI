using System;
using System.Data;
using System.Diagnostics;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.DataExtensions
{
	// Token: 0x0200001E RID: 30
	internal class TransactionWrapper : BaseDataWrapper, Microsoft.ReportingServices.DataProcessing.IDbTransaction, IDisposable
	{
		// Token: 0x060000DD RID: 221 RVA: 0x00004F95 File Offset: 0x00003195
		protected internal TransactionWrapper(global::System.Data.IDbTransaction underlyingTransaction)
			: base(underlyingTransaction)
		{
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004F9E File Offset: 0x0000319E
		public virtual void Commit()
		{
			this.UnderlyingTransaction.Commit();
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004FAC File Offset: 0x000031AC
		public virtual void Rollback()
		{
			if (this.UnderlyingTransaction.Connection != null && this.UnderlyingTransaction.Connection.State != ConnectionState.Closed && this.UnderlyingTransaction.Connection.State != ConnectionState.Broken)
			{
				this.UnderlyingTransaction.Rollback();
				return;
			}
			if (RSTrace.DataExtensionTracer.TraceWarning)
			{
				RSTrace.DataExtensionTracer.Trace(TraceLevel.Warning, "TransactionWrapper.Rollback not called, connection is not valid");
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00005014 File Offset: 0x00003214
		protected internal global::System.Data.IDbTransaction UnderlyingTransaction
		{
			get
			{
				return (global::System.Data.IDbTransaction)base.UnderlyingObject;
			}
		}
	}
}
