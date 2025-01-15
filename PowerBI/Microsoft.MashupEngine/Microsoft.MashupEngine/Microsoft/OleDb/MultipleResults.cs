using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001EAF RID: 7855
	public abstract class MultipleResults : IMultipleResults, IDBAsynchStatus, ISupportErrorInfo, IEvaluationResultSource
	{
		// Token: 0x17002F66 RID: 12134
		// (get) Token: 0x0600C22B RID: 49707
		public abstract IMultipleResults _MultipleResults { get; }

		// Token: 0x17002F67 RID: 12135
		// (get) Token: 0x0600C22C RID: 49708
		public abstract IDBAsynchStatus DbAsyncStatus { get; }

		// Token: 0x17002F68 RID: 12136
		// (get) Token: 0x0600C22D RID: 49709 RVA: 0x000020FA File Offset: 0x000002FA
		public virtual IEvaluationResultSource EvaluationResultSource
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600C22E RID: 49710 RVA: 0x002700AE File Offset: 0x0026E2AE
		unsafe int IMultipleResults.GetResult(IntPtr pUnkOuter, IntPtr lResultFlag, ref Guid riid, DBROWCOUNT* cRowsAffected, out IntPtr ppRowset)
		{
			return this._MultipleResults.GetResult(pUnkOuter, lResultFlag, ref riid, cRowsAffected, out ppRowset);
		}

		// Token: 0x0600C22F RID: 49711 RVA: 0x002700C2 File Offset: 0x0026E2C2
		public void Abort(HCHAPTER hChapter, DBASYNCHOP eOperation)
		{
			this.DbAsyncStatus.Abort(hChapter, eOperation);
		}

		// Token: 0x0600C230 RID: 49712 RVA: 0x002700D1 File Offset: 0x0026E2D1
		public unsafe void GetStatus(HCHAPTER hChapter, DBASYNCHOP eOperation, DBCOUNTITEM* pulProgress, DBCOUNTITEM* pulProgressMax, out DBASYNCHPHASE peAsynchPhase, char** ppwszStatusText)
		{
			this.DbAsyncStatus.GetStatus(hChapter, eOperation, pulProgress, pulProgressMax, out peAsynchPhase, ppwszStatusText);
		}

		// Token: 0x0600C231 RID: 49713 RVA: 0x00002139 File Offset: 0x00000339
		int ISupportErrorInfo.InterfaceSupportsErrorInfo(ref Guid iid)
		{
			return 1;
		}

		// Token: 0x0600C232 RID: 49714 RVA: 0x002700E7 File Offset: 0x0026E2E7
		void IEvaluationResultSource.WaitForResults()
		{
			IEvaluationResultSource evaluationResultSource = this.EvaluationResultSource;
			if (evaluationResultSource == null)
			{
				return;
			}
			evaluationResultSource.WaitForResults();
		}
	}
}
