using System;

namespace Microsoft.OleDb.PInvokeInterop
{
	// Token: 0x02001F3D RID: 7997
	internal class ColumnsInfoTypeInfo : InterfaceTypeInfo<IColumnsInfo>
	{
		// Token: 0x0600C3E4 RID: 50148 RVA: 0x002736B0 File Offset: 0x002718B0
		private unsafe static int GetColumnInfo(IntPtr objHandle, out DBORDINAL countColumnInfos, out DBCOLUMNINFO* nativeColumnInfos, out char* nativeStrings)
		{
			try
			{
				InterfaceTypeInfo<IColumnsInfo>.FromIntPtr(objHandle).GetColumnInfo(out countColumnInfos, out nativeColumnInfos, out nativeStrings);
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
				return InterfaceTypeInfo<IColumnsInfo>.ValidateException(ex, objHandle);
			}
			return 0;
		}

		// Token: 0x0600C3E5 RID: 50149 RVA: 0x00273704 File Offset: 0x00271904
		private unsafe static int MapColumnIDs(IntPtr objHandle, DBORDINAL cColumnIDs, DBID* rgColumnIDs, DBORDINAL* rgColumns)
		{
			return InterfaceTypeInfo<IColumnsInfo>.InvokeAndReturnHResult(delegate
			{
				InterfaceTypeInfo<IColumnsInfo>.FromIntPtr(objHandle).MapColumnIDs(cColumnIDs, rgColumnIDs, rgColumns);
			}, objHandle);
		}

		// Token: 0x0600C3E6 RID: 50150 RVA: 0x0027374A File Offset: 0x0027194A
		protected override Delegate[] CreateDelegates()
		{
			return new Delegate[]
			{
				new ColumnsInfoTypeInfo.GetColumnInfoCallback(ColumnsInfoTypeInfo.GetColumnInfo),
				new ColumnsInfoTypeInfo.MapColumnIDsCallback(ColumnsInfoTypeInfo.MapColumnIDs)
			};
		}

		// Token: 0x02001F3E RID: 7998
		// (Invoke) Token: 0x0600C3E9 RID: 50153
		private unsafe delegate int GetColumnInfoCallback(IntPtr objHandle, out DBORDINAL countColumnInfos, out DBCOLUMNINFO* nativeColumnInfos, out char* nativeStrings);

		// Token: 0x02001F3F RID: 7999
		// (Invoke) Token: 0x0600C3ED RID: 50157
		private unsafe delegate int MapColumnIDsCallback(IntPtr objHandle, DBORDINAL cColumnIDs, DBID* rgColumnIDs, DBORDINAL* rgColumns);
	}
}
