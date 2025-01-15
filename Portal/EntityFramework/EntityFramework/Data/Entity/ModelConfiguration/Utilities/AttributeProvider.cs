using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Utilities
{
	// Token: 0x020001BE RID: 446
	internal class AttributeProvider
	{
		// Token: 0x060017BF RID: 6079 RVA: 0x000406D8 File Offset: 0x0003E8D8
		public virtual IEnumerable<Attribute> GetAttributes(MemberInfo memberInfo)
		{
			Type type = memberInfo as Type;
			if (type != null)
			{
				return this.GetAttributes(type);
			}
			return this.GetAttributes((PropertyInfo)memberInfo);
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x0004070C File Offset: 0x0003E90C
		public virtual IEnumerable<Attribute> GetAttributes(Type type)
		{
			List<Attribute> attrs = new List<Attribute>(AttributeProvider.GetTypeDescriptor(type).GetAttributes().Cast<Attribute>());
			IEnumerable<Attribute> customAttributes = type.GetCustomAttributes(true);
			Func<Attribute, bool> <>9__0;
			Func<Attribute, bool> func;
			if ((func = <>9__0) == null)
			{
				func = (<>9__0 = (Attribute a) => a.GetType().FullName.Equals("System.Data.Services.Common.EntityPropertyMappingAttribute", StringComparison.Ordinal) && !attrs.Contains(a));
			}
			foreach (Attribute attribute in customAttributes.Where(func))
			{
				attrs.Add(attribute);
			}
			return attrs;
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x000407AC File Offset: 0x0003E9AC
		public virtual IEnumerable<Attribute> GetAttributes(PropertyInfo propertyInfo)
		{
			return this._discoveredAttributes.GetOrAdd(propertyInfo, delegate(PropertyInfo pi)
			{
				PropertyDescriptor propertyDescriptor = AttributeProvider.GetTypeDescriptor(pi.DeclaringType).GetProperties()[pi.Name];
				IEnumerable<Attribute> enumerable = ((propertyDescriptor != null) ? propertyDescriptor.Attributes.Cast<Attribute>() : pi.GetCustomAttributes(true));
				ICollection<Attribute> collection = (ICollection<Attribute>)this.GetAttributes(pi.PropertyType);
				if (collection.Count > 0)
				{
					enumerable = enumerable.Except(collection);
				}
				return enumerable.ToList<Attribute>();
			});
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x000407C6 File Offset: 0x0003E9C6
		private static ICustomTypeDescriptor GetTypeDescriptor(Type type)
		{
			return new AssociatedMetadataTypeTypeDescriptionProvider(type).GetTypeDescriptor(type);
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x000407D4 File Offset: 0x0003E9D4
		public virtual void ClearCache()
		{
			this._discoveredAttributes.Clear();
		}

		// Token: 0x04000A44 RID: 2628
		private readonly ConcurrentDictionary<PropertyInfo, IEnumerable<Attribute>> _discoveredAttributes = new ConcurrentDictionary<PropertyInfo, IEnumerable<Attribute>>();
	}
}
