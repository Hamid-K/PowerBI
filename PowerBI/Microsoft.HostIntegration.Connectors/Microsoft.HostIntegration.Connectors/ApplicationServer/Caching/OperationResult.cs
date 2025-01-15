using System;
using System.Text;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002B5 RID: 693
	internal class OperationResult
	{
		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001953 RID: 6483 RVA: 0x0004B4F5 File Offset: 0x000496F5
		// (set) Token: 0x06001954 RID: 6484 RVA: 0x0004B4FD File Offset: 0x000496FD
		public object ResultContext { get; set; }

		// Token: 0x06001955 RID: 6485 RVA: 0x0004B506 File Offset: 0x00049706
		internal OperationResult(OperationStatus status)
		{
			this._status = status;
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x0004B515 File Offset: 0x00049715
		internal OperationResult(OperationStatus status, object resultContext)
			: this(status)
		{
			this.ResultContext = resultContext;
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x0004B525 File Offset: 0x00049725
		internal OperationResult(OperationStatus status, Exception exception)
			: this(status)
		{
			this._exception = exception;
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001958 RID: 6488 RVA: 0x0004B535 File Offset: 0x00049735
		internal bool IsSuccess
		{
			get
			{
				return this._status == OperationStatus.Success;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001959 RID: 6489 RVA: 0x0004B540 File Offset: 0x00049740
		internal bool HasVerificationFailed
		{
			get
			{
				return this._status == OperationStatus.VerificationFailed;
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x0600195A RID: 6490 RVA: 0x0004B54B File Offset: 0x0004974B
		internal bool HasAuthorizationFailed
		{
			get
			{
				return this._status == OperationStatus.AuthorizationFailed;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x0600195B RID: 6491 RVA: 0x0004B557 File Offset: 0x00049757
		internal bool HasChannelAuthenticationFailed
		{
			get
			{
				return this._status == OperationStatus.ChannelAuthFailure;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x0600195C RID: 6492 RVA: 0x0004B563 File Offset: 0x00049763
		internal bool HasCertificateRevocationCheckFailed
		{
			get
			{
				return this._status == OperationStatus.ChannelAuthOfflineRevocationFailure;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x0600195D RID: 6493 RVA: 0x0004B56F File Offset: 0x0004976F
		internal bool HasConnectionFailed
		{
			get
			{
				return this._status == OperationStatus.ChannelOpenFailed || this._status == OperationStatus.SendFailed || this._status == OperationStatus.ChannelCreationFailed;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x0600195E RID: 6494 RVA: 0x0004B58E File Offset: 0x0004978E
		internal bool IsConnectionFailedOrOpening
		{
			get
			{
				return this.HasConnectionFailed || this._status == OperationStatus.ChannelOpening;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x0600195F RID: 6495 RVA: 0x0004B5A3 File Offset: 0x000497A3
		internal bool IsRetryable
		{
			get
			{
				return this._status == OperationStatus.SendFailed;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001960 RID: 6496 RVA: 0x0004B5AE File Offset: 0x000497AE
		internal bool IsAsyncFailure
		{
			get
			{
				return this._status == OperationStatus.AsyncFailureReceived;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001961 RID: 6497 RVA: 0x0004B5B9 File Offset: 0x000497B9
		internal Exception Fault
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001962 RID: 6498 RVA: 0x0004B5C1 File Offset: 0x000497C1
		internal OperationStatus Status
		{
			get
			{
				return this._status;
			}
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x0004B5CC File Offset: 0x000497CC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("Status=");
			stringBuilder.Append(this._status);
			if (this._exception != null)
			{
				stringBuilder.Append('[');
				stringBuilder.Append(this._exception.ToString());
				stringBuilder.Append(']');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x0004B628 File Offset: 0x00049828
		public bool IsMatchingContext(object context)
		{
			return this.ResultContext != null && this.ResultContext.Equals(context);
		}

		// Token: 0x04000DB9 RID: 3513
		internal static OperationResult Success = new OperationResult(OperationStatus.Success);

		// Token: 0x04000DBA RID: 3514
		internal static OperationResult InstanceClosed = new OperationResult(OperationStatus.InstanceClosed);

		// Token: 0x04000DBB RID: 3515
		internal static OperationResult VerificationFailed = new OperationResult(OperationStatus.VerificationFailed);

		// Token: 0x04000DBC RID: 3516
		internal static OperationResult ChannelOpening = new OperationResult(OperationStatus.ChannelOpening);

		// Token: 0x04000DBD RID: 3517
		private OperationStatus _status;

		// Token: 0x04000DBE RID: 3518
		private Exception _exception;
	}
}
