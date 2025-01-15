using System;
using System.ComponentModel;
using System.Reflection;

namespace System.Web.Http.Internal
{
	// Token: 0x02000184 RID: 388
	internal static class ParameterInfoExtensions
	{
		// Token: 0x06000A0B RID: 2571 RVA: 0x00019ED8 File Offset: 0x000180D8
		public static bool TryGetDefaultValue(this ParameterInfo parameterInfo, out object value)
		{
			if (parameterInfo == null)
			{
				throw Error.ArgumentNull("parameterInfo");
			}
			object defaultValue = parameterInfo.DefaultValue;
			if (defaultValue != DBNull.Value)
			{
				value = defaultValue;
				return true;
			}
			DefaultValueAttribute[] array = (DefaultValueAttribute[])parameterInfo.GetCustomAttributes(typeof(DefaultValueAttribute), false);
			if (array == null || array.Length == 0)
			{
				value = null;
				return false;
			}
			value = array[0].Value;
			return true;
		}
	}
}
