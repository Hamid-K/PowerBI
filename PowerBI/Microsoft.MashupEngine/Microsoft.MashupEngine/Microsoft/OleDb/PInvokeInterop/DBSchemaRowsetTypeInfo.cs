using System;

namespace Microsoft.OleDb.PInvokeInterop
{
	// Token: 0x02001F97 RID: 8087
	internal class DBSchemaRowsetTypeInfo : InterfaceTypeInfo<IDBSchemaRowset>
	{
		// Token: 0x0600C537 RID: 50487 RVA: 0x00274F0C File Offset: 0x0027310C
		private unsafe static int GetRowset(IntPtr objHandle, IntPtr punkOuter, ref Guid guidSchema, uint cRestrictions, VARIANT* rgRestrictions, ref Guid iid, uint cPropertySets, DBPROPSET* rgPropertySets, out IntPtr rowset)
		{
			int num;
			try
			{
				num = InterfaceTypeInfo<IDBSchemaRowset>.ValidateHResult(InterfaceTypeInfo<IDBSchemaRowset>.FromIntPtr(objHandle).GetRowset(punkOuter, ref guidSchema, cRestrictions, rgRestrictions, ref iid, cPropertySets, rgPropertySets, out rowset), objHandle);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				rowset = IntPtr.Zero;
				num = InterfaceTypeInfo<IDBSchemaRowset>.ValidateException(ex, objHandle);
			}
			return num;
		}

		// Token: 0x0600C538 RID: 50488 RVA: 0x00274F68 File Offset: 0x00273168
		private unsafe static int GetSchemas(IntPtr objHandle, out uint cSchemas, out Guid* rgSchemas, out uint* rgRestrictionSupport)
		{
			try
			{
				InterfaceTypeInfo<IDBSchemaRowset>.FromIntPtr(objHandle).GetSchemas(out cSchemas, out rgSchemas, out rgRestrictionSupport);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				cSchemas = 0U;
				rgSchemas = (IntPtr)((UIntPtr)0);
				rgRestrictionSupport = (IntPtr)((UIntPtr)0);
				return InterfaceTypeInfo<IDBSchemaRowset>.ValidateException(ex, objHandle);
			}
			return 0;
		}

		// Token: 0x0600C539 RID: 50489 RVA: 0x00274FB8 File Offset: 0x002731B8
		protected override Delegate[] CreateDelegates()
		{
			return new Delegate[]
			{
				new DBSchemaRowsetTypeInfo.GetRowsetCallback(DBSchemaRowsetTypeInfo.GetRowset),
				new DBSchemaRowsetTypeInfo.GetSchemasCallback(DBSchemaRowsetTypeInfo.GetSchemas)
			};
		}

		// Token: 0x02001F98 RID: 8088
		// (Invoke) Token: 0x0600C53C RID: 50492
		private unsafe delegate int GetRowsetCallback(IntPtr objHandle, IntPtr punkOuter, ref Guid guidSchema, uint cRestrictions, VARIANT* rgRestrictions, ref Guid iid, uint cPropertySets, DBPROPSET* rgPropertySets, out IntPtr rowset);

		// Token: 0x02001F99 RID: 8089
		// (Invoke) Token: 0x0600C540 RID: 50496
		private unsafe delegate int GetSchemasCallback(IntPtr objHandle, out uint cSchemas, out Guid* rgSchemas, out uint* rgRestrictionSupport);
	}
}
