using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000049 RID: 73
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public static class IDbPropertiesExtensions
	{
		// Token: 0x0600026D RID: 621 RVA: 0x000080A0 File Offset: 0x000062A0
		public unsafe static bool TryGetValue(this IDBProperties dbProperties, Guid propertyGroup, DBPROPID propertyID, out object value)
		{
			DBPROPIDSET dbpropidset;
			dbpropidset.PropertySet = propertyGroup;
			dbpropidset.PropertyIDCount = 1U;
			dbpropidset.PropertyIDs = &propertyID;
			uint num;
			DBPROPSET* ptr;
			int properties = dbProperties.GetProperties(1U, &dbpropidset, out num, out ptr);
			bool flag = false;
			try
			{
				value = null;
				if (properties >= 0 && num == 1U && ptr->PropertyCount == 1U && ptr->Properties->Status == DBPROPSTATUS.OK)
				{
					VARIANT* ptr2 = &ptr->Properties->Variant;
					value = Variant.GetObject(ptr2);
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

		// Token: 0x0600026E RID: 622 RVA: 0x0000813C File Offset: 0x0000633C
		public unsafe static bool TrySetValue(this IDBProperties dbProperties, Guid propertyGroup, DBPROPID propertyID, object value)
		{
			VARIANT variant;
			Marshal.GetNativeVariantForObject(value, new IntPtr((void*)(&variant)));
			bool flag;
			try
			{
				DBPROP dbprop;
				dbprop.PropertyID = propertyID;
				dbprop.Variant = variant;
				DBPROPSET dbpropset;
				dbpropset.PropertySet = propertyGroup;
				dbpropset.PropertyCount = 1U;
				dbpropset.Properties = &dbprop;
				flag = dbProperties.SetProperties(1U, &dbpropset) == 0;
			}
			finally
			{
				Variant.Clear(&variant);
			}
			return flag;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000081AC File Offset: 0x000063AC
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

		// Token: 0x06000270 RID: 624 RVA: 0x000081D4 File Offset: 0x000063D4
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

		// Token: 0x06000271 RID: 625 RVA: 0x000081FC File Offset: 0x000063FC
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

		// Token: 0x06000272 RID: 626 RVA: 0x00008224 File Offset: 0x00006424
		public static object GetValue(this IDBProperties dbProperties, Guid propertyGroup, DBPROPID propertyID)
		{
			object obj;
			if (!dbProperties.TryGetValue(propertyGroup, propertyID, out obj))
			{
				throw new InvalidOperationException("Could not get property: " + propertyID.ToString());
			}
			return obj;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000825B File Offset: 0x0000645B
		public static void SetValue(this IDBProperties dbProperties, Guid propertyGroup, DBPROPID propertyID, object value)
		{
			if (!dbProperties.TrySetValue(propertyGroup, propertyID, value))
			{
				throw new InvalidOperationException("Could not set property: " + propertyID.ToString());
			}
		}
	}
}
