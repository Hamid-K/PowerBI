using System;
using System.ComponentModel;
using System.Reflection;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x0200085A RID: 2138
	public class ReflectionUtils
	{
		// Token: 0x06004423 RID: 17443 RVA: 0x000E54B0 File Offset: 0x000E36B0
		public static bool SetProperty(object theObj, string propertyName, string val)
		{
			PropertyInfo property = theObj.GetType().GetProperty(propertyName);
			bool flag = property != null;
			if (!flag)
			{
				return flag;
			}
			TypeConverter converter = TypeDescriptor.GetConverter(property.PropertyType);
			if (converter != null && converter.CanConvertFrom(typeof(string)))
			{
				object obj = converter.ConvertFromString(val);
				property.SetValue(theObj, obj, null);
				return flag;
			}
			throw new Exception(string.Format("Cannot set the property {0} with value {1} on class {2}. Ensure that the value is correct", propertyName, val, theObj.GetType().FullName));
		}
	}
}
