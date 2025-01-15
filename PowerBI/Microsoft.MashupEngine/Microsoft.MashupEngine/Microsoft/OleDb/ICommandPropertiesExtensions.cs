using System;
using System.Runtime.InteropServices;
using Microsoft.OleDb.Marshallers;

namespace Microsoft.OleDb
{
	// Token: 0x02001E9D RID: 7837
	public static class ICommandPropertiesExtensions
	{
		// Token: 0x0600C1BC RID: 49596 RVA: 0x0026F584 File Offset: 0x0026D784
		public unsafe static bool TryGetValue(this ICommandProperties commandProperties, Guid propertyGroup, DBPROPID propertyID, out object value)
		{
			DBPROPIDSET dbpropidset;
			dbpropidset.guidPropertySet = propertyGroup;
			dbpropidset.cPropertyIDs = 1U;
			dbpropidset.rgPropertyIDs = &propertyID;
			uint num;
			DBPROPSET* ptr;
			int properties = commandProperties.GetProperties(1U, &dbpropidset, out num, out ptr);
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

		// Token: 0x0600C1BD RID: 49597 RVA: 0x0026F62C File Offset: 0x0026D82C
		public unsafe static bool TrySetValue(this ICommandProperties commandProperties, Guid propertyGroup, DBPROPID propertyID, object value)
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
				flag = commandProperties.SetProperties(1U, &dbpropset) == 0;
			}
			finally
			{
				VariantMarshaller.Instance.Cleanup((IntPtr)((void*)(&variant)));
			}
			return flag;
		}

		// Token: 0x0600C1BE RID: 49598 RVA: 0x0026F6AC File Offset: 0x0026D8AC
		public static bool TryGetValue(this ICommandProperties commandProperties, Guid propertyGroup, DBPROPID propertyID, out int value)
		{
			object obj;
			if (commandProperties.TryGetValue(propertyGroup, propertyID, out obj))
			{
				value = (int)obj;
				return true;
			}
			value = 0;
			return false;
		}

		// Token: 0x0600C1BF RID: 49599 RVA: 0x0026F6D4 File Offset: 0x0026D8D4
		public static bool TryGetValue(this ICommandProperties commandProperties, Guid propertyGroup, DBPROPID propertyID, out bool value)
		{
			object obj;
			if (commandProperties.TryGetValue(propertyGroup, propertyID, out obj))
			{
				value = (bool)obj;
				return true;
			}
			value = false;
			return false;
		}

		// Token: 0x0600C1C0 RID: 49600 RVA: 0x0026F6FC File Offset: 0x0026D8FC
		public static bool TryGetValue(this ICommandProperties commandProperties, Guid propertyGroup, DBPROPID propertyID, out string value)
		{
			object obj;
			if (commandProperties.TryGetValue(propertyGroup, propertyID, out obj))
			{
				value = (string)obj;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600C1C1 RID: 49601 RVA: 0x0026F724 File Offset: 0x0026D924
		public static object GetValue(this ICommandProperties commandProperties, Guid propertyGroup, DBPROPID propertyID)
		{
			object obj;
			if (!commandProperties.TryGetValue(propertyGroup, propertyID, out obj))
			{
				throw new InvalidOperationException("Could not get property: " + propertyID.ToString());
			}
			return obj;
		}

		// Token: 0x0600C1C2 RID: 49602 RVA: 0x0026F75B File Offset: 0x0026D95B
		public static void SetValue(this ICommandProperties commandProperties, Guid propertyGroup, DBPROPID propertyID, object value)
		{
			if (!commandProperties.TrySetValue(propertyGroup, propertyID, value))
			{
				throw new InvalidOperationException("Could not set property: " + propertyID.ToString());
			}
		}
	}
}
