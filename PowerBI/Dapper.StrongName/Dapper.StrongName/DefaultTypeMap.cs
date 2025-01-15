using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Dapper
{
	// Token: 0x02000008 RID: 8
	public sealed class DefaultTypeMap : SqlMapper.ITypeMap
	{
		// Token: 0x06000026 RID: 38 RVA: 0x0000257E File Offset: 0x0000077E
		public DefaultTypeMap(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this._fields = DefaultTypeMap.GetSettableFields(type);
			this.Properties = DefaultTypeMap.GetSettableProps(type);
			this._type = type;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000025BC File Offset: 0x000007BC
		internal static MethodInfo GetPropertySetter(PropertyInfo propertyInfo, Type type)
		{
			if (propertyInfo.DeclaringType == type)
			{
				return propertyInfo.GetSetMethod(true);
			}
			return propertyInfo.DeclaringType.GetProperty(propertyInfo.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, Type.DefaultBinder, propertyInfo.PropertyType, (from p in propertyInfo.GetIndexParameters()
				select p.ParameterType).ToArray<Type>(), null).GetSetMethod(true);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002634 File Offset: 0x00000834
		internal static List<PropertyInfo> GetSettableProps(Type t)
		{
			return (from p in t.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
				where DefaultTypeMap.GetPropertySetter(p, t) != null
				select p).ToList<PropertyInfo>();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002671 File Offset: 0x00000871
		internal static List<FieldInfo> GetSettableFields(Type t)
		{
			return t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).ToList<FieldInfo>();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002680 File Offset: 0x00000880
		public ConstructorInfo FindConstructor(string[] names, Type[] types)
		{
			ConstructorInfo[] constructors = this._type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			foreach (ConstructorInfo ctor in constructors.OrderBy(delegate(ConstructorInfo c)
			{
				if (c.IsPublic)
				{
					return 0;
				}
				if (!c.IsPrivate)
				{
					return 1;
				}
				return 2;
			}).ThenBy((ConstructorInfo c) => c.GetParameters().Length))
			{
				ParameterInfo[] ctorParameters = ctor.GetParameters();
				if (ctorParameters.Length == 0)
				{
					return ctor;
				}
				if (ctorParameters.Length == types.Length)
				{
					int i = 0;
					while (i < ctorParameters.Length && string.Equals(ctorParameters[i].Name, names[i], StringComparison.OrdinalIgnoreCase))
					{
						if (!(types[i] == typeof(byte[])) || !(ctorParameters[i].ParameterType.FullName == "System.Data.Linq.Binary"))
						{
							Type unboxedType = Nullable.GetUnderlyingType(ctorParameters[i].ParameterType) ?? ctorParameters[i].ParameterType;
							if (unboxedType != types[i] && !SqlMapper.HasTypeHandler(unboxedType) && (!unboxedType.IsEnum() || !(Enum.GetUnderlyingType(unboxedType) == types[i])) && (!(unboxedType == typeof(char)) || !(types[i] == typeof(string))) && (!unboxedType.IsEnum() || !(types[i] == typeof(string))))
							{
								break;
							}
						}
						i++;
					}
					if (i == ctorParameters.Length)
					{
						return ctor;
					}
				}
			}
			return null;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002850 File Offset: 0x00000A50
		public ConstructorInfo FindExplicitConstructor()
		{
			ConstructorInfo[] constructors = this._type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			List<ConstructorInfo> withAttr = constructors.Where((ConstructorInfo c) => c.GetCustomAttributes(typeof(ExplicitConstructorAttribute), true).Length != 0).ToList<ConstructorInfo>();
			if (withAttr.Count == 1)
			{
				return withAttr[0];
			}
			return null;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000028A8 File Offset: 0x00000AA8
		public SqlMapper.IMemberMap GetConstructorParameter(ConstructorInfo constructor, string columnName)
		{
			ParameterInfo[] parameters = constructor.GetParameters();
			return new SimpleMemberMap(columnName, parameters.FirstOrDefault((ParameterInfo p) => string.Equals(p.Name, columnName, StringComparison.OrdinalIgnoreCase)));
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000028E8 File Offset: 0x00000AE8
		public SqlMapper.IMemberMap GetMember(string columnName)
		{
			PropertyInfo property = this.Properties.Find((PropertyInfo p) => string.Equals(p.Name, columnName, StringComparison.Ordinal)) ?? this.Properties.Find((PropertyInfo p) => string.Equals(p.Name, columnName, StringComparison.OrdinalIgnoreCase));
			if (property == null && DefaultTypeMap.MatchNamesWithUnderscores)
			{
				property = this.Properties.Find((PropertyInfo p) => string.Equals(p.Name, columnName.Replace("_", ""), StringComparison.Ordinal)) ?? this.Properties.Find((PropertyInfo p) => string.Equals(p.Name, columnName.Replace("_", ""), StringComparison.OrdinalIgnoreCase));
			}
			if (property != null)
			{
				return new SimpleMemberMap(columnName, property);
			}
			string backingFieldName = "<" + columnName + ">k__BackingField";
			FieldInfo fieldInfo;
			if ((fieldInfo = this._fields.Find((FieldInfo p) => string.Equals(p.Name, columnName, StringComparison.Ordinal))) == null && (fieldInfo = this._fields.Find((FieldInfo p) => string.Equals(p.Name, backingFieldName, StringComparison.Ordinal))) == null)
			{
				fieldInfo = this._fields.Find((FieldInfo p) => string.Equals(p.Name, columnName, StringComparison.OrdinalIgnoreCase)) ?? this._fields.Find((FieldInfo p) => string.Equals(p.Name, backingFieldName, StringComparison.OrdinalIgnoreCase));
			}
			FieldInfo field = fieldInfo;
			if (field == null && DefaultTypeMap.MatchNamesWithUnderscores)
			{
				string effectiveColumnName = columnName.Replace("_", "");
				backingFieldName = "<" + effectiveColumnName + ">k__BackingField";
				FieldInfo fieldInfo2;
				if ((fieldInfo2 = this._fields.Find((FieldInfo p) => string.Equals(p.Name, effectiveColumnName, StringComparison.Ordinal))) == null && (fieldInfo2 = this._fields.Find((FieldInfo p) => string.Equals(p.Name, backingFieldName, StringComparison.Ordinal))) == null)
				{
					fieldInfo2 = this._fields.Find((FieldInfo p) => string.Equals(p.Name, effectiveColumnName, StringComparison.OrdinalIgnoreCase)) ?? this._fields.Find((FieldInfo p) => string.Equals(p.Name, backingFieldName, StringComparison.OrdinalIgnoreCase));
				}
				field = fieldInfo2;
			}
			if (field != null)
			{
				return new SimpleMemberMap(columnName, field);
			}
			return null;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002AE4 File Offset: 0x00000CE4
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002AEB File Offset: 0x00000CEB
		public static bool MatchNamesWithUnderscores { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002AF3 File Offset: 0x00000CF3
		public List<PropertyInfo> Properties { get; }

		// Token: 0x0400001F RID: 31
		private readonly List<FieldInfo> _fields;

		// Token: 0x04000020 RID: 32
		private readonly Type _type;
	}
}
