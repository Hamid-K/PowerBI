using System;

namespace Microsoft.OleDb.PInvokeInterop
{
	// Token: 0x02001F36 RID: 7990
	internal class AccessorTypeInfo : InterfaceTypeInfo<IAccessor>
	{
		// Token: 0x0600C3CA RID: 50122 RVA: 0x002734F4 File Offset: 0x002716F4
		private unsafe static int AddRefAccessor(IntPtr objHandle, HACCESSOR hAccessor, uint* pcRefCount)
		{
			return InterfaceTypeInfo<IAccessor>.InvokeAndReturnHResult(delegate
			{
				InterfaceTypeInfo<IAccessor>.FromIntPtr(objHandle).AddRefAccessor(hAccessor, pcRefCount);
			}, objHandle);
		}

		// Token: 0x0600C3CB RID: 50123 RVA: 0x00273534 File Offset: 0x00271734
		private unsafe static int CreateAccessor(IntPtr objHandle, DBACCESSORFLAGS dwAccessorFlags, DBCOUNTITEM cBindings, DBBINDING* rgBindings, DBLENGTH cbRowSize, out HACCESSOR hAccessor, DBBINDSTATUS* rgStatus)
		{
			try
			{
				InterfaceTypeInfo<IAccessor>.FromIntPtr(objHandle).CreateAccessor(dwAccessorFlags, cBindings, rgBindings, cbRowSize, out hAccessor, rgStatus);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				hAccessor = default(HACCESSOR);
				return InterfaceTypeInfo<IAccessor>.ValidateException(ex, objHandle);
			}
			return 0;
		}

		// Token: 0x0600C3CC RID: 50124 RVA: 0x00273588 File Offset: 0x00271788
		private unsafe static int GetBindings(IntPtr objHandle, HACCESSOR hAccessor, out DBACCESSORFLAGS dwAccessorFlags, out DBCOUNTITEM pcBindings, out DBBINDING* rgBindings)
		{
			try
			{
				InterfaceTypeInfo<IAccessor>.FromIntPtr(objHandle).GetBindings(hAccessor, out dwAccessorFlags, out pcBindings, out rgBindings);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				dwAccessorFlags = DBACCESSORFLAGS.INVALID;
				pcBindings = default(DBCOUNTITEM);
				rgBindings = (IntPtr)((UIntPtr)0);
				return InterfaceTypeInfo<IAccessor>.ValidateException(ex, objHandle);
			}
			return 0;
		}

		// Token: 0x0600C3CD RID: 50125 RVA: 0x002735DC File Offset: 0x002717DC
		private unsafe static int ReleaseAccessor(IntPtr objHandle, HACCESSOR hAccessor, uint* pcRefCount)
		{
			return InterfaceTypeInfo<IAccessor>.InvokeAndReturnHResult(delegate
			{
				InterfaceTypeInfo<IAccessor>.FromIntPtr(objHandle).ReleaseAccessor(hAccessor, pcRefCount);
			}, objHandle);
		}

		// Token: 0x0600C3CE RID: 50126 RVA: 0x0027361C File Offset: 0x0027181C
		protected override Delegate[] CreateDelegates()
		{
			return new Delegate[]
			{
				new AccessorTypeInfo.AddRefAccessorCallback(AccessorTypeInfo.AddRefAccessor),
				new AccessorTypeInfo.CreateAccessorCallback(AccessorTypeInfo.CreateAccessor),
				new AccessorTypeInfo.GetBindingsCallback(AccessorTypeInfo.GetBindings),
				new AccessorTypeInfo.ReleaseAccessorCallback(AccessorTypeInfo.ReleaseAccessor)
			};
		}

		// Token: 0x02001F37 RID: 7991
		// (Invoke) Token: 0x0600C3D1 RID: 50129
		private unsafe delegate int AddRefAccessorCallback(IntPtr objHandle, HACCESSOR hAccessor, uint* pcRefCount);

		// Token: 0x02001F38 RID: 7992
		// (Invoke) Token: 0x0600C3D5 RID: 50133
		private unsafe delegate int CreateAccessorCallback(IntPtr objHandle, DBACCESSORFLAGS dwAccessorFlags, DBCOUNTITEM cBindings, DBBINDING* rgBindings, DBLENGTH cbRowSize, out HACCESSOR hAccessor, DBBINDSTATUS* rgStatus);

		// Token: 0x02001F39 RID: 7993
		// (Invoke) Token: 0x0600C3D9 RID: 50137
		private unsafe delegate int GetBindingsCallback(IntPtr objHandle, HACCESSOR hAccessor, out DBACCESSORFLAGS dwAccessorFlags, out DBCOUNTITEM pcBindings, out DBBINDING* rgBindings);

		// Token: 0x02001F3A RID: 7994
		// (Invoke) Token: 0x0600C3DD RID: 50141
		private unsafe delegate int ReleaseAccessorCallback(IntPtr objHandle, HACCESSOR hAccessor, uint* pcRefCount);
	}
}
