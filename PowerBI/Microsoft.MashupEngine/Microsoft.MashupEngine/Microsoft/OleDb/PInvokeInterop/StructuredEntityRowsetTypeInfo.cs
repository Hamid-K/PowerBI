using System;

namespace Microsoft.OleDb.PInvokeInterop
{
	// Token: 0x02001F91 RID: 8081
	internal class StructuredEntityRowsetTypeInfo : InterfaceTypeInfo<IStructuredEntityRowset>
	{
		// Token: 0x0600C522 RID: 50466 RVA: 0x00274DAC File Offset: 0x00272FAC
		private unsafe static int GetEntityColumnInfo(IntPtr objHandle, DBORDINAL cOrdinals, DBORDINAL* nativeOrdinals, out DBORDINAL countColumnInfos, out EntityDbcolumninfo* nativeColumnInfos, out char* nativeStrings)
		{
			try
			{
				InterfaceTypeInfo<IStructuredEntityRowset>.FromIntPtr(objHandle).GetEntityColumnInfo(cOrdinals, nativeOrdinals, out countColumnInfos, out nativeColumnInfos, out nativeStrings);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				countColumnInfos = default(DBORDINAL);
				nativeColumnInfos = (IntPtr)((UIntPtr)0);
				nativeStrings = (IntPtr)((UIntPtr)0);
				return InterfaceTypeInfo<IStructuredEntityRowset>.ValidateException(ex, objHandle);
			}
			return 0;
		}

		// Token: 0x0600C523 RID: 50467 RVA: 0x00274E04 File Offset: 0x00273004
		private static int BindAccessor(IntPtr objHandle, HACCESSOR hAccessor, DBORDINAL dbOrdinal, HACCESSOR hAccessorChild)
		{
			return InterfaceTypeInfo<IStructuredEntityRowset>.InvokeAndReturnHResult(delegate
			{
				InterfaceTypeInfo<IStructuredEntityRowset>.FromIntPtr(objHandle).BindAccessor(hAccessor, dbOrdinal, hAccessorChild);
			}, objHandle);
		}

		// Token: 0x0600C524 RID: 50468 RVA: 0x00274E4A File Offset: 0x0027304A
		protected override Delegate[] CreateDelegates()
		{
			return new Delegate[]
			{
				new StructuredEntityRowsetTypeInfo.GetEntityColumnInfoCallback(StructuredEntityRowsetTypeInfo.GetEntityColumnInfo),
				new StructuredEntityRowsetTypeInfo.BindAccessorCallback(StructuredEntityRowsetTypeInfo.BindAccessor)
			};
		}

		// Token: 0x02001F92 RID: 8082
		// (Invoke) Token: 0x0600C527 RID: 50471
		private unsafe delegate int GetEntityColumnInfoCallback(IntPtr objHandle, DBORDINAL cOrdinals, DBORDINAL* nativeOrdinals, out DBORDINAL countColumnInfos, out EntityDbcolumninfo* nativeColumnInfos, out char* nativeStrings);

		// Token: 0x02001F93 RID: 8083
		// (Invoke) Token: 0x0600C52B RID: 50475
		private delegate int BindAccessorCallback(IntPtr objHandle, HACCESSOR hAccessor, DBORDINAL dbOrdinal, HACCESSOR hAccessorChild);
	}
}
