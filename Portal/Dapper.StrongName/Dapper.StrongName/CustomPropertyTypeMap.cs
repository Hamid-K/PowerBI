using System;
using System.Reflection;

namespace Dapper
{
	// Token: 0x02000005 RID: 5
	public sealed class CustomPropertyTypeMap : SqlMapper.ITypeMap
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002385 File Offset: 0x00000585
		public CustomPropertyTypeMap(Type type, Func<Type, string, PropertyInfo> propertySelector)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this._type = type;
			if (propertySelector == null)
			{
				throw new ArgumentNullException("propertySelector");
			}
			this._propertySelector = propertySelector;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023B9 File Offset: 0x000005B9
		public ConstructorInfo FindConstructor(string[] names, Type[] types)
		{
			return this._type.GetConstructor(new Type[0]);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023CC File Offset: 0x000005CC
		public ConstructorInfo FindExplicitConstructor()
		{
			return null;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023CF File Offset: 0x000005CF
		public SqlMapper.IMemberMap GetConstructorParameter(ConstructorInfo constructor, string columnName)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023D8 File Offset: 0x000005D8
		public SqlMapper.IMemberMap GetMember(string columnName)
		{
			PropertyInfo prop = this._propertySelector(this._type, columnName);
			if (!(prop != null))
			{
				return null;
			}
			return new SimpleMemberMap(columnName, prop);
		}

		// Token: 0x04000017 RID: 23
		private readonly Type _type;

		// Token: 0x04000018 RID: 24
		private readonly Func<Type, string, PropertyInfo> _propertySelector;
	}
}
