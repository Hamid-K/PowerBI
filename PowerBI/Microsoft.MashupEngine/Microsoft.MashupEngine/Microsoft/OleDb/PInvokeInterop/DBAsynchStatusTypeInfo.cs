using System;

namespace Microsoft.OleDb.PInvokeInterop
{
	// Token: 0x02001F7F RID: 8063
	internal class DBAsynchStatusTypeInfo : InterfaceTypeInfo<IDBAsynchStatus>
	{
		// Token: 0x0600C4E0 RID: 50400 RVA: 0x0027492C File Offset: 0x00272B2C
		private static int Abort(IntPtr objHandle, HCHAPTER hChapter, DBASYNCHOP eOperation)
		{
			return InterfaceTypeInfo<IDBAsynchStatus>.InvokeAndReturnHResult(delegate
			{
				InterfaceTypeInfo<IDBAsynchStatus>.FromIntPtr(objHandle).Abort(hChapter, eOperation);
			}, objHandle);
		}

		// Token: 0x0600C4E1 RID: 50401 RVA: 0x0027496C File Offset: 0x00272B6C
		private unsafe static int GetStatus(IntPtr objHandle, HCHAPTER hChapter, DBASYNCHOP eOperation, DBCOUNTITEM* pulProgress, DBCOUNTITEM* pulProgressMax, out DBASYNCHPHASE peAsynchPhase, char** ppwszStatusText)
		{
			try
			{
				InterfaceTypeInfo<IDBAsynchStatus>.FromIntPtr(objHandle).GetStatus(hChapter, eOperation, pulProgress, pulProgressMax, out peAsynchPhase, ppwszStatusText);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				peAsynchPhase = DBASYNCHPHASE.INITIALIZATION;
				return InterfaceTypeInfo<IDBAsynchStatus>.ValidateException(ex, objHandle);
			}
			return 0;
		}

		// Token: 0x0600C4E2 RID: 50402 RVA: 0x002749BC File Offset: 0x00272BBC
		protected override Delegate[] CreateDelegates()
		{
			return new Delegate[]
			{
				new DBAsynchStatusTypeInfo.AbortCallback(DBAsynchStatusTypeInfo.Abort),
				new DBAsynchStatusTypeInfo.GetStatusCallback(DBAsynchStatusTypeInfo.GetStatus)
			};
		}

		// Token: 0x02001F80 RID: 8064
		// (Invoke) Token: 0x0600C4E5 RID: 50405
		private delegate int AbortCallback(IntPtr objHandle, HCHAPTER hChapter, DBASYNCHOP eOperation);

		// Token: 0x02001F81 RID: 8065
		// (Invoke) Token: 0x0600C4E9 RID: 50409
		private unsafe delegate int GetStatusCallback(IntPtr objHandle, HCHAPTER hChapter, DBASYNCHOP eOperation, DBCOUNTITEM* pulProgress, DBCOUNTITEM* pulProgressMax, out DBASYNCHPHASE peAsynchPhase, char** ppwszStatusText);
	}
}
