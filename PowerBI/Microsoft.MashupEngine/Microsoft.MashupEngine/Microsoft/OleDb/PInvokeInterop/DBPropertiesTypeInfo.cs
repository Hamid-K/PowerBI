using System;

namespace Microsoft.OleDb.PInvokeInterop
{
	// Token: 0x02001F60 RID: 8032
	internal class DBPropertiesTypeInfo : InterfaceTypeInfo<IDBProperties>
	{
		// Token: 0x0600C463 RID: 50275 RVA: 0x00273E78 File Offset: 0x00272078
		private unsafe static int GetProperties(IntPtr objHandle, uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint countPropertySets, out DBPROPSET* nativePropertySets)
		{
			int num;
			try
			{
				num = InterfaceTypeInfo<IDBProperties>.ValidateHResult(InterfaceTypeInfo<IDBProperties>.FromIntPtr(objHandle).GetProperties(countPropertyIDSets, nativePropertyIDSets, out countPropertySets, out nativePropertySets), objHandle);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				countPropertySets = 0U;
				nativePropertySets = (IntPtr)((UIntPtr)0);
				num = InterfaceTypeInfo<IDBProperties>.ValidateException(ex, objHandle);
			}
			return num;
		}

		// Token: 0x0600C464 RID: 50276 RVA: 0x00273ECC File Offset: 0x002720CC
		private unsafe static int GetPropertyInfo(IntPtr objHandle, uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint countPropertyInfoSets, out DBPROPINFOSET* nativePropertyInfoSets, char** descriptions)
		{
			int num;
			try
			{
				num = InterfaceTypeInfo<IDBProperties>.ValidateHResult(InterfaceTypeInfo<IDBProperties>.FromIntPtr(objHandle).GetPropertyInfo(countPropertyIDSets, nativePropertyIDSets, out countPropertyInfoSets, out nativePropertyInfoSets, descriptions), objHandle);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				countPropertyInfoSets = 0U;
				nativePropertyInfoSets = (IntPtr)((UIntPtr)0);
				descriptions = null;
				num = InterfaceTypeInfo<IDBProperties>.ValidateException(ex, objHandle);
			}
			return num;
		}

		// Token: 0x0600C465 RID: 50277 RVA: 0x00273F24 File Offset: 0x00272124
		private unsafe static int SetProperties(IntPtr objHandle, uint countPropertySets, DBPROPSET* nativePropertySets)
		{
			return InterfaceTypeInfo<IDBProperties>.InvokeAndReturnHResult(() => InterfaceTypeInfo<IDBProperties>.FromIntPtr(objHandle).SetProperties(countPropertySets, nativePropertySets), objHandle);
		}

		// Token: 0x0600C466 RID: 50278 RVA: 0x00273F63 File Offset: 0x00272163
		protected override Delegate[] CreateDelegates()
		{
			return new Delegate[]
			{
				new DBPropertiesTypeInfo.GetPropertiesCallback(DBPropertiesTypeInfo.GetProperties),
				new DBPropertiesTypeInfo.GetPropertyInfoCallback(DBPropertiesTypeInfo.GetPropertyInfo),
				new DBPropertiesTypeInfo.SetPropertiesCallback(DBPropertiesTypeInfo.SetProperties)
			};
		}

		// Token: 0x02001F61 RID: 8033
		// (Invoke) Token: 0x0600C469 RID: 50281
		private unsafe delegate int GetPropertiesCallback(IntPtr objHandle, uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint countPropertySets, out DBPROPSET* nativePropertySets);

		// Token: 0x02001F62 RID: 8034
		// (Invoke) Token: 0x0600C46D RID: 50285
		private unsafe delegate int GetPropertyInfoCallback(IntPtr objHandle, uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint countPropertyInfoSets, out DBPROPINFOSET* nativePropertyInfoSets, char** descriptions);

		// Token: 0x02001F63 RID: 8035
		// (Invoke) Token: 0x0600C471 RID: 50289
		private unsafe delegate int SetPropertiesCallback(IntPtr objHandle, uint countPropertySets, DBPROPSET* nativePropertySets);
	}
}
