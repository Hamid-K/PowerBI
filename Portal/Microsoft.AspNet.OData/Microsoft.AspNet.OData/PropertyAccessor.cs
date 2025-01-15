using System;
using System.Reflection;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000053 RID: 83
	internal abstract class PropertyAccessor<TStructuralType> where TStructuralType : class
	{
		// Token: 0x06000241 RID: 577 RVA: 0x0000A830 File Offset: 0x00008A30
		protected PropertyAccessor(PropertyInfo property)
		{
			if (property == null)
			{
				throw Error.ArgumentNull("property");
			}
			this.Property = property;
			if (this.Property.GetGetMethod() == null || (!TypeHelper.IsCollection(property.PropertyType) && this.Property.GetSetMethod() == null))
			{
				throw Error.Argument("property", SRResources.PropertyMustHavePublicGetterAndSetter, new object[0]);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000A8A7 File Offset: 0x00008AA7
		// (set) Token: 0x06000243 RID: 579 RVA: 0x0000A8AF File Offset: 0x00008AAF
		public PropertyInfo Property { get; private set; }

		// Token: 0x06000244 RID: 580 RVA: 0x0000A8B8 File Offset: 0x00008AB8
		public void Copy(TStructuralType from, TStructuralType to)
		{
			if (from == null)
			{
				throw Error.ArgumentNull("from");
			}
			if (to == null)
			{
				throw Error.ArgumentNull("to");
			}
			this.SetValue(to, this.GetValue(from));
		}

		// Token: 0x06000245 RID: 581
		public abstract object GetValue(TStructuralType instance);

		// Token: 0x06000246 RID: 582
		public abstract void SetValue(TStructuralType instance, object value);
	}
}
