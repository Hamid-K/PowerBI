using System;
using System.Reflection;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter.Deserialization;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000049 RID: 73
	internal class FastPropertyAccessor<TStructuralType> : PropertyAccessor<TStructuralType> where TStructuralType : class
	{
		// Token: 0x060001B6 RID: 438 RVA: 0x00007C3C File Offset: 0x00005E3C
		public FastPropertyAccessor(PropertyInfo property)
			: base(property)
		{
			this._property = property;
			this._isCollection = TypeHelper.IsCollection(property.PropertyType);
			if (!this._isCollection)
			{
				this._setter = PropertyHelper.MakeFastPropertySetter<TStructuralType>(property);
			}
			this._getter = PropertyHelper.MakeFastPropertyGetter(property);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00007C88 File Offset: 0x00005E88
		public override object GetValue(TStructuralType instance)
		{
			if (instance == null)
			{
				throw Error.ArgumentNull("instance");
			}
			return this._getter(instance);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00007CB0 File Offset: 0x00005EB0
		public override void SetValue(TStructuralType instance, object value)
		{
			if (instance == null)
			{
				throw Error.ArgumentNull("instance");
			}
			if (this._isCollection)
			{
				DeserializationHelpers.SetCollectionProperty(instance, this._property.Name, null, value, true);
				return;
			}
			this._setter(instance, value);
		}

		// Token: 0x04000075 RID: 117
		private bool _isCollection;

		// Token: 0x04000076 RID: 118
		private PropertyInfo _property;

		// Token: 0x04000077 RID: 119
		private Action<TStructuralType, object> _setter;

		// Token: 0x04000078 RID: 120
		private Func<object, object> _getter;
	}
}
