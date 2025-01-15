using System;
using System.Reflection;

namespace Dapper
{
	// Token: 0x0200000C RID: 12
	internal sealed class SimpleMemberMap : SqlMapper.IMemberMap
	{
		// Token: 0x06000046 RID: 70 RVA: 0x000038E3 File Offset: 0x00001AE3
		public SimpleMemberMap(string columnName, PropertyInfo property)
		{
			if (columnName == null)
			{
				throw new ArgumentNullException("columnName");
			}
			this.ColumnName = columnName;
			if (property == null)
			{
				throw new ArgumentNullException("property");
			}
			this.Property = property;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003917 File Offset: 0x00001B17
		public SimpleMemberMap(string columnName, FieldInfo field)
		{
			if (columnName == null)
			{
				throw new ArgumentNullException("columnName");
			}
			this.ColumnName = columnName;
			if (field == null)
			{
				throw new ArgumentNullException("field");
			}
			this.Field = field;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000394B File Offset: 0x00001B4B
		public SimpleMemberMap(string columnName, ParameterInfo parameter)
		{
			if (columnName == null)
			{
				throw new ArgumentNullException("columnName");
			}
			this.ColumnName = columnName;
			if (parameter == null)
			{
				throw new ArgumentNullException("parameter");
			}
			this.Parameter = parameter;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000049 RID: 73 RVA: 0x0000397F File Offset: 0x00001B7F
		public string ColumnName { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00003987 File Offset: 0x00001B87
		public Type MemberType
		{
			get
			{
				FieldInfo field = this.Field;
				Type type;
				if ((type = ((field != null) ? field.FieldType : null)) == null)
				{
					PropertyInfo property = this.Property;
					if ((type = ((property != null) ? property.PropertyType : null)) == null)
					{
						ParameterInfo parameter = this.Parameter;
						if (parameter == null)
						{
							return null;
						}
						type = parameter.ParameterType;
					}
				}
				return type;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000039C6 File Offset: 0x00001BC6
		public PropertyInfo Property { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000039CE File Offset: 0x00001BCE
		public FieldInfo Field { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000039D6 File Offset: 0x00001BD6
		public ParameterInfo Parameter { get; }
	}
}
