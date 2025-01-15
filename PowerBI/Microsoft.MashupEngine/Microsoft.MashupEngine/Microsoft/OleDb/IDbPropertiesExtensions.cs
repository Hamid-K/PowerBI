using System;
using System.Runtime.InteropServices;
using Microsoft.OleDb.Marshallers;

namespace Microsoft.OleDb
{
	// Token: 0x02001EA3 RID: 7843
	public static class IDbPropertiesExtensions
	{
		// Token: 0x0600C1DF RID: 49631 RVA: 0x0026FC4C File Offset: 0x0026DE4C
		public unsafe static bool TryGetValue(this IDBProperties dbProperties, Guid propertyGroup, DBPROPID propertyID, out object value)
		{
			DBPROPIDSET dbpropidset;
			dbpropidset.guidPropertySet = propertyGroup;
			dbpropidset.cPropertyIDs = 1U;
			dbpropidset.rgPropertyIDs = &propertyID;
			uint num;
			DBPROPSET* ptr;
			int properties = dbProperties.GetProperties(1U, &dbpropidset, out num, out ptr);
			bool flag = false;
			try
			{
				value = null;
				if (properties >= 0 && num == 1U && ptr->cProperties == 1U && ptr->rgProperties->dwStatus == DBPROPSTATUS.OK)
				{
					VARIANT* ptr2 = &ptr->rgProperties->variant;
					value = VariantMarshaller.Instance.GetManaged((IntPtr)((void*)ptr2));
					Variant.Clear(ptr2);
					flag = true;
				}
			}
			finally
			{
				Marshal.FreeCoTaskMem(new IntPtr((void*)ptr));
			}
			return flag;
		}

		// Token: 0x0600C1E0 RID: 49632 RVA: 0x0026FCF4 File Offset: 0x0026DEF4
		public unsafe static bool TrySetValue(this IDBProperties dbProperties, Guid propertyGroup, DBPROPID propertyID, object value)
		{
			VARIANT variant;
			VariantMarshaller.Instance.GetNative(value, (IntPtr)((void*)(&variant)));
			bool flag;
			try
			{
				DBPROP dbprop;
				dbprop.dwPropertyID = propertyID;
				dbprop.variant = variant;
				DBPROPSET dbpropset;
				dbpropset.guidPropertySet = propertyGroup;
				dbpropset.cProperties = 1U;
				dbpropset.rgProperties = &dbprop;
				flag = dbProperties.SetProperties(1U, &dbpropset) == 0;
			}
			finally
			{
				VariantMarshaller.Instance.Cleanup((IntPtr)((void*)(&variant)));
			}
			return flag;
		}

		// Token: 0x0600C1E1 RID: 49633 RVA: 0x0026FD74 File Offset: 0x0026DF74
		public static bool TryGetValue(this IDBProperties dbProperties, Guid propertyGroup, DBPROPID propertyID, out int value)
		{
			object obj;
			if (dbProperties.TryGetValue(propertyGroup, propertyID, out obj))
			{
				value = (int)obj;
				return true;
			}
			value = 0;
			return false;
		}

		// Token: 0x0600C1E2 RID: 49634 RVA: 0x0026FD9C File Offset: 0x0026DF9C
		public static bool TryGetValue(this IDBProperties dbProperties, Guid propertyGroup, DBPROPID propertyID, out bool value)
		{
			object obj;
			if (dbProperties.TryGetValue(propertyGroup, propertyID, out obj))
			{
				value = (bool)obj;
				return true;
			}
			value = false;
			return false;
		}

		// Token: 0x0600C1E3 RID: 49635 RVA: 0x0026FDC4 File Offset: 0x0026DFC4
		public static bool TryGetValue(this IDBProperties dbProperties, Guid propertyGroup, DBPROPID propertyID, out string value)
		{
			object obj;
			if (dbProperties.TryGetValue(propertyGroup, propertyID, out obj))
			{
				value = (string)obj;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600C1E4 RID: 49636 RVA: 0x0026FDEC File Offset: 0x0026DFEC
		public static object GetValue(this IDBProperties dbProperties, Guid propertyGroup, DBPROPID propertyID)
		{
			object obj;
			if (!dbProperties.TryGetValue(propertyGroup, propertyID, out obj))
			{
				throw new InvalidOperationException("Could not get property: " + propertyID.ToString());
			}
			return obj;
		}

		// Token: 0x0600C1E5 RID: 49637 RVA: 0x0026FE23 File Offset: 0x0026E023
		public static void SetValue(this IDBProperties dbProperties, Guid propertyGroup, DBPROPID propertyID, object value)
		{
			if (!dbProperties.TrySetValue(propertyGroup, propertyID, value))
			{
				throw new InvalidOperationException("Could not set property: " + propertyID.ToString());
			}
		}
	}
}
