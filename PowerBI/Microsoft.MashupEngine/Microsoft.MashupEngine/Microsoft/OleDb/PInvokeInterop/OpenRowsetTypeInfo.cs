using System;

namespace Microsoft.OleDb.PInvokeInterop
{
	// Token: 0x02001F9C RID: 8092
	internal class OpenRowsetTypeInfo : InterfaceTypeInfo<IOpenRowset>
	{
		// Token: 0x0600C54A RID: 50506 RVA: 0x00275058 File Offset: 0x00273258
		private unsafe static int OpenRowset(IntPtr objHandle, IntPtr pUnkOuter, DBID* pTableID, DBID* pIndexID, ref Guid iid, uint cPropertySets, DBPROPSET* rgPropertySets, out IntPtr ppRowset)
		{
			int num;
			try
			{
				num = InterfaceTypeInfo<IOpenRowset>.ValidateHResult(InterfaceTypeInfo<IOpenRowset>.FromIntPtr(objHandle).OpenRowset(pUnkOuter, pTableID, pIndexID, ref iid, cPropertySets, rgPropertySets, out ppRowset), objHandle);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				ppRowset = IntPtr.Zero;
				num = InterfaceTypeInfo<IOpenRowset>.ValidateException(ex, objHandle);
			}
			return num;
		}

		// Token: 0x0600C54B RID: 50507 RVA: 0x002750B0 File Offset: 0x002732B0
		protected override Delegate[] CreateDelegates()
		{
			return new Delegate[]
			{
				new OpenRowsetTypeInfo.OpenRowsetCallback(OpenRowsetTypeInfo.OpenRowset)
			};
		}

		// Token: 0x02001F9D RID: 8093
		// (Invoke) Token: 0x0600C54E RID: 50510
		private unsafe delegate int OpenRowsetCallback(IntPtr objHandle, IntPtr pUnkOuter, DBID* pTableID, DBID* pIndexID, ref Guid iid, uint cPropertySets, DBPROPSET* rgPropertySets, out IntPtr ppRowset);
	}
}
