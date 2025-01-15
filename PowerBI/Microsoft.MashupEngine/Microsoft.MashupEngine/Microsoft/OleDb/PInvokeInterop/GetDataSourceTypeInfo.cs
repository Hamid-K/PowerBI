using System;

namespace Microsoft.OleDb.PInvokeInterop
{
	// Token: 0x02001F9A RID: 8090
	internal class GetDataSourceTypeInfo : InterfaceTypeInfo<IGetDataSource>
	{
		// Token: 0x0600C543 RID: 50499 RVA: 0x00274FE8 File Offset: 0x002731E8
		private static int GetDataSource(IntPtr objHandle, ref Guid iid, out IntPtr dataSource)
		{
			int num;
			try
			{
				num = InterfaceTypeInfo<IGetDataSource>.ValidateHResult(InterfaceTypeInfo<IGetDataSource>.FromIntPtr(objHandle).GetDataSource(ref iid, out dataSource), objHandle);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				dataSource = IntPtr.Zero;
				num = InterfaceTypeInfo<IGetDataSource>.ValidateException(ex, objHandle);
			}
			return num;
		}

		// Token: 0x0600C544 RID: 50500 RVA: 0x00275038 File Offset: 0x00273238
		protected override Delegate[] CreateDelegates()
		{
			return new Delegate[]
			{
				new GetDataSourceTypeInfo.GetDataSourceCallback(GetDataSourceTypeInfo.GetDataSource)
			};
		}

		// Token: 0x02001F9B RID: 8091
		// (Invoke) Token: 0x0600C547 RID: 50503
		private delegate int GetDataSourceCallback(IntPtr objHandle, ref Guid iid, out IntPtr dataSource);
	}
}
