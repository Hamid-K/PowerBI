using System;

namespace Microsoft.OleDb.PInvokeInterop
{
	// Token: 0x02001F87 RID: 8071
	internal class RowsetTypeInfo : InterfaceTypeInfo<IRowset>
	{
		// Token: 0x0600C4FF RID: 50431 RVA: 0x00274B34 File Offset: 0x00272D34
		private unsafe static int AddRefRows(IntPtr objHandle, DBCOUNTITEM cRows, HROW* nativeRows, uint* nativeRefCounts, DBROWSTATUS* nativeRowStatus)
		{
			return InterfaceTypeInfo<IRowset>.InvokeAndReturnHResult(delegate
			{
				InterfaceTypeInfo<IRowset>.FromIntPtr(objHandle).AddRefRows(cRows, nativeRows, nativeRefCounts, nativeRowStatus);
			}, objHandle);
		}

		// Token: 0x0600C500 RID: 50432 RVA: 0x00274B84 File Offset: 0x00272D84
		private unsafe static int GetData(IntPtr objHandle, HROW hRow, HACCESSOR hAccessor, byte* pData)
		{
			return InterfaceTypeInfo<IRowset>.InvokeAndReturnHResult(delegate
			{
				InterfaceTypeInfo<IRowset>.FromIntPtr(objHandle).GetData(hRow, hAccessor, pData);
			}, objHandle);
		}

		// Token: 0x0600C501 RID: 50433 RVA: 0x00274BCC File Offset: 0x00272DCC
		private unsafe static int GetNextRows(IntPtr objHandle, HCHAPTER hReserved, DBROWOFFSET lRowsOffset, DBROWCOUNT cRows, out DBCOUNTITEM countRowsObtained, HROW** pRows)
		{
			int num;
			try
			{
				num = InterfaceTypeInfo<IRowset>.ValidateHResult(InterfaceTypeInfo<IRowset>.FromIntPtr(objHandle).GetNextRows(hReserved, lRowsOffset, cRows, out countRowsObtained, pRows), objHandle);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				countRowsObtained = default(DBCOUNTITEM);
				num = InterfaceTypeInfo<IRowset>.ValidateException(ex, objHandle);
			}
			return num;
		}

		// Token: 0x0600C502 RID: 50434 RVA: 0x00274C20 File Offset: 0x00272E20
		private unsafe static int ReleaseRows(IntPtr objHandle, DBCOUNTITEM cRows, HROW* nativeRows, int* nativeRowOptions, uint* nativeRefCounts, DBROWSTATUS* nativeRowStatus)
		{
			return InterfaceTypeInfo<IRowset>.InvokeAndReturnHResult(delegate
			{
				InterfaceTypeInfo<IRowset>.FromIntPtr(objHandle).ReleaseRows(cRows, nativeRows, (void*)nativeRowOptions, nativeRefCounts, nativeRowStatus);
			}, objHandle);
		}

		// Token: 0x0600C503 RID: 50435 RVA: 0x00274C78 File Offset: 0x00272E78
		private static int RestartPosition(IntPtr objHandle, HCHAPTER hReserved)
		{
			return InterfaceTypeInfo<IRowset>.InvokeAndReturnHResult(delegate
			{
				InterfaceTypeInfo<IRowset>.FromIntPtr(objHandle).RestartPosition(hReserved);
			}, objHandle);
		}

		// Token: 0x0600C504 RID: 50436 RVA: 0x00274CB0 File Offset: 0x00272EB0
		protected override Delegate[] CreateDelegates()
		{
			return new Delegate[]
			{
				new RowsetTypeInfo.AddRefRowsCallback(RowsetTypeInfo.AddRefRows),
				new RowsetTypeInfo.GetDataCallback(RowsetTypeInfo.GetData),
				new RowsetTypeInfo.GetNextRowsCallback(RowsetTypeInfo.GetNextRows),
				new RowsetTypeInfo.ReleaseRowsCallback(RowsetTypeInfo.ReleaseRows),
				new RowsetTypeInfo.RestartPositionCallback(RowsetTypeInfo.RestartPosition)
			};
		}

		// Token: 0x02001F88 RID: 8072
		// (Invoke) Token: 0x0600C507 RID: 50439
		private unsafe delegate int AddRefRowsCallback(IntPtr objHandle, DBCOUNTITEM cRows, HROW* nativeRows, uint* nativeRefCounts, DBROWSTATUS* nativeRowStatus);

		// Token: 0x02001F89 RID: 8073
		// (Invoke) Token: 0x0600C50B RID: 50443
		private unsafe delegate int GetDataCallback(IntPtr objHandle, HROW hRow, HACCESSOR hAccessor, byte* pData);

		// Token: 0x02001F8A RID: 8074
		// (Invoke) Token: 0x0600C50F RID: 50447
		private unsafe delegate int GetNextRowsCallback(IntPtr objHandle, HCHAPTER hReserved, DBROWOFFSET lRowsOffset, DBROWCOUNT cRows, out DBCOUNTITEM countRowsObtained, HROW** pRows);

		// Token: 0x02001F8B RID: 8075
		// (Invoke) Token: 0x0600C513 RID: 50451
		private unsafe delegate int ReleaseRowsCallback(IntPtr objHandle, DBCOUNTITEM cRows, HROW* nativeRows, int* nativeRowOptions, uint* nativeRefCounts, DBROWSTATUS* nativeRowStatus);

		// Token: 0x02001F8C RID: 8076
		// (Invoke) Token: 0x0600C517 RID: 50455
		private delegate int RestartPositionCallback(IntPtr objHandle, HCHAPTER hReserved);
	}
}
