using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001B4 RID: 436
	public abstract class PropertyDefinition
	{
		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06000E6D RID: 3693 RVA: 0x00023661 File Offset: 0x00021861
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x00023669 File Offset: 0x00021869
		protected PropertyDefinition(string name)
		{
			this.m_name = name;
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x00023678 File Offset: 0x00021878
		public static IPropertyDefinition Create(Type componentType, string propertyName)
		{
			PropertyInfo propertyInfo = componentType.GetProperty(propertyName, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
			if (propertyInfo == null)
			{
				propertyInfo = componentType.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);
			}
			Type type = propertyInfo.PropertyType;
			object obj = null;
			object obj2 = null;
			object obj3 = null;
			IList<int> list = null;
			foreach (object obj4 in propertyInfo.GetCustomAttributes(true))
			{
				if (obj4 is DefaultValueAttribute)
				{
					obj = ((DefaultValueAttribute)obj4).Value;
					if (obj is IExpression)
					{
						obj = ((IExpression)obj).Value;
					}
				}
				else if (obj4 is ValidValuesAttribute)
				{
					obj2 = ((ValidValuesAttribute)obj4).Minimum;
					obj3 = ((ValidValuesAttribute)obj4).Maximum;
				}
				else if (obj4 is ValidEnumValuesAttribute)
				{
					list = ((ValidEnumValuesAttribute)obj4).ValidValues;
				}
			}
			if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ReportExpression<>))
			{
				type = type.GetGenericArguments()[0];
			}
			if (type == typeof(int))
			{
				return new IntProperty(propertyName, (int?)obj, (int?)obj2, (int?)obj3);
			}
			if (type == typeof(double))
			{
				return new DoubleProperty(propertyName, (double?)obj, (double?)obj2, (double?)obj3);
			}
			if (type == typeof(string))
			{
				return new StringProperty(propertyName, (string)obj);
			}
			if (type == typeof(ReportSize))
			{
				return new SizeProperty(propertyName, (ReportSize?)obj, (ReportSize?)obj2, (ReportSize?)obj3);
			}
			if (type == typeof(ReportColor))
			{
				return new ColorProperty(propertyName, (ReportColor?)obj);
			}
			if (type.IsSubclassOf(typeof(Enum)))
			{
				return new EnumProperty(propertyName, type, obj, list);
			}
			return null;
		}

		// Token: 0x0400053C RID: 1340
		private readonly string m_name;
	}
}
