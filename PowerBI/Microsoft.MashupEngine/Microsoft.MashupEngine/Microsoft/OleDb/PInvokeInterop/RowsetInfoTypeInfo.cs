using System;

namespace Microsoft.OleDb.PInvokeInterop
{
	// Token: 0x02001F83 RID: 8067
	internal class RowsetInfoTypeInfo : InterfaceTypeInfo<IRowsetInfo>
	{
		// Token: 0x0600C4EE RID: 50414 RVA: 0x00274A08 File Offset: 0x00272C08
		private unsafe static int GetProperties(IntPtr objHandle, uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint countPropertySets, out DBPROPSET* nativePropertySets)
		{
			int num;
			try
			{
				num = InterfaceTypeInfo<IRowsetInfo>.ValidateHResult(InterfaceTypeInfo<IRowsetInfo>.FromIntPtr(objHandle).GetProperties(countPropertyIDSets, nativePropertyIDSets, out countPropertySets, out nativePropertySets), objHandle);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				countPropertySets = 0U;
				nativePropertySets = (IntPtr)((UIntPtr)0);
				num = InterfaceTypeInfo<IRowsetInfo>.ValidateException(ex, objHandle);
			}
			return num;
		}

		// Token: 0x0600C4EF RID: 50415 RVA: 0x00274A5C File Offset: 0x00272C5C
		private static int GetReferencedRowset(IntPtr objHandle, DBORDINAL iOrdinal, ref Guid iid, out IntPtr referencedRowset)
		{
			try
			{
				InterfaceTypeInfo<IRowsetInfo>.FromIntPtr(objHandle).GetReferencedRowset(iOrdinal, ref iid, out referencedRowset);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				referencedRowset = IntPtr.Zero;
				return InterfaceTypeInfo<IRowsetInfo>.ValidateException(ex, objHandle);
			}
			return 0;
		}

		// Token: 0x0600C4F0 RID: 50416 RVA: 0x00274AA8 File Offset: 0x00272CA8
		private static int GetSpecification(IntPtr objHandle, ref Guid iid, out IntPtr specification)
		{
			try
			{
				InterfaceTypeInfo<IRowsetInfo>.FromIntPtr(objHandle).GetSpecification(ref iid, out specification);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				specification = IntPtr.Zero;
				return InterfaceTypeInfo<IRowsetInfo>.ValidateException(ex, objHandle);
			}
			return 0;
		}

		// Token: 0x0600C4F1 RID: 50417 RVA: 0x00274AF4 File Offset: 0x00272CF4
		protected override Delegate[] CreateDelegates()
		{
			return new Delegate[]
			{
				new RowsetInfoTypeInfo.GetPropertiesCallback(RowsetInfoTypeInfo.GetProperties),
				new RowsetInfoTypeInfo.GetReferencedRowsetCallback(RowsetInfoTypeInfo.GetReferencedRowset),
				new RowsetInfoTypeInfo.GetSpecificationCallback(RowsetInfoTypeInfo.GetSpecification)
			};
		}

		// Token: 0x02001F84 RID: 8068
		// (Invoke) Token: 0x0600C4F4 RID: 50420
		private unsafe delegate int GetPropertiesCallback(IntPtr objHandle, uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint countPropertySets, out DBPROPSET* nativePropertySets);

		// Token: 0x02001F85 RID: 8069
		// (Invoke) Token: 0x0600C4F8 RID: 50424
		private delegate int GetReferencedRowsetCallback(IntPtr objHandle, DBORDINAL iOrdinal, ref Guid iid, out IntPtr referencedRowset);

		// Token: 0x02001F86 RID: 8070
		// (Invoke) Token: 0x0600C4FC RID: 50428
		private delegate int GetSpecificationCallback(IntPtr objHandle, ref Guid iid, out IntPtr specification);
	}
}
