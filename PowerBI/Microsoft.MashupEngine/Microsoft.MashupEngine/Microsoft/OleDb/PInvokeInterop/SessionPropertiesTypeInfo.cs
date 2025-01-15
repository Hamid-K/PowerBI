using System;

namespace Microsoft.OleDb.PInvokeInterop
{
	// Token: 0x02001F9E RID: 8094
	internal class SessionPropertiesTypeInfo : InterfaceTypeInfo<ISessionProperties>
	{
		// Token: 0x0600C551 RID: 50513 RVA: 0x002750D0 File Offset: 0x002732D0
		private unsafe static int GetProperties(IntPtr objHandle, uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint countPropertySets, out DBPROPSET* nativePropertySets)
		{
			int num;
			try
			{
				num = InterfaceTypeInfo<ISessionProperties>.ValidateHResult(InterfaceTypeInfo<ISessionProperties>.FromIntPtr(objHandle).GetProperties(countPropertyIDSets, nativePropertyIDSets, out countPropertySets, out nativePropertySets), objHandle);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				countPropertySets = 0U;
				nativePropertySets = (IntPtr)((UIntPtr)0);
				num = InterfaceTypeInfo<ISessionProperties>.ValidateException(ex, objHandle);
			}
			return num;
		}

		// Token: 0x0600C552 RID: 50514 RVA: 0x00275124 File Offset: 0x00273324
		private unsafe static int SetProperties(IntPtr objHandle, uint countPropertySets, DBPROPSET* nativePropertySets)
		{
			return InterfaceTypeInfo<ISessionProperties>.InvokeAndReturnHResult(() => InterfaceTypeInfo<ISessionProperties>.FromIntPtr(objHandle).SetProperties(countPropertySets, nativePropertySets), objHandle);
		}

		// Token: 0x0600C553 RID: 50515 RVA: 0x00275163 File Offset: 0x00273363
		protected override Delegate[] CreateDelegates()
		{
			return new Delegate[]
			{
				new SessionPropertiesTypeInfo.GetPropertiesCallback(SessionPropertiesTypeInfo.GetProperties),
				new SessionPropertiesTypeInfo.SetPropertiesCallback(SessionPropertiesTypeInfo.SetProperties)
			};
		}

		// Token: 0x02001F9F RID: 8095
		// (Invoke) Token: 0x0600C556 RID: 50518
		private unsafe delegate int GetPropertiesCallback(IntPtr objHandle, uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint countPropertySets, out DBPROPSET* nativePropertySets);

		// Token: 0x02001FA0 RID: 8096
		// (Invoke) Token: 0x0600C55A RID: 50522
		private unsafe delegate int SetPropertiesCallback(IntPtr objHandle, uint countPropertySets, DBPROPSET* nativePropertySets);
	}
}
