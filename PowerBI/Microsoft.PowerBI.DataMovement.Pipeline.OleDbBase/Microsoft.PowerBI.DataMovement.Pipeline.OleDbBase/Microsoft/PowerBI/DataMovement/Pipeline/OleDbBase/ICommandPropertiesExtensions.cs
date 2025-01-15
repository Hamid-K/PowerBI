using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000044 RID: 68
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public static class ICommandPropertiesExtensions
	{
		// Token: 0x0600025F RID: 607 RVA: 0x00007D1C File Offset: 0x00005F1C
		public unsafe static bool TryGetValue(this ICommandProperties dbProperties, Guid propertyGroup, DBPROPID propertyID, out object value)
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

		// Token: 0x06000260 RID: 608 RVA: 0x00007DB8 File Offset: 0x00005FB8
		public unsafe static bool TrySetValue(this ICommandProperties dbProperties, Guid propertyGroup, DBPROPID propertyID, object value)
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

		// Token: 0x06000261 RID: 609 RVA: 0x00007E28 File Offset: 0x00006028
		public static bool TryGetValue(this ICommandProperties dbProperties, Guid propertyGroup, DBPROPID propertyID, out int value)
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

		// Token: 0x06000262 RID: 610 RVA: 0x00007E50 File Offset: 0x00006050
		public static bool TryGetValue(this ICommandProperties dbProperties, Guid propertyGroup, DBPROPID propertyID, out bool value)
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

		// Token: 0x06000263 RID: 611 RVA: 0x00007E78 File Offset: 0x00006078
		public static bool TryGetValue(this ICommandProperties dbProperties, Guid propertyGroup, DBPROPID propertyID, out string value)
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

		// Token: 0x06000264 RID: 612 RVA: 0x00007EA0 File Offset: 0x000060A0
		public static object GetValue(this ICommandProperties dbProperties, Guid propertyGroup, DBPROPID propertyID)
		{
			object obj;
			if (!dbProperties.TryGetValue(propertyGroup, propertyID, out obj))
			{
				throw new InvalidOperationException("Could not get property: " + propertyID.ToString());
			}
			return obj;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00007ED7 File Offset: 0x000060D7
		public static void SetValue(this ICommandProperties dbProperties, Guid propertyGroup, DBPROPID propertyID, object value)
		{
			if (!dbProperties.TrySetValue(propertyGroup, propertyID, value))
			{
				throw new InvalidOperationException("Could not set property: " + propertyID.ToString());
			}
		}
	}
}
