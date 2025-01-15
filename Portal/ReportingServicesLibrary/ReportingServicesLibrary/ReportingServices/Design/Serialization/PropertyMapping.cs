using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.Design.Serialization
{
	// Token: 0x020003A1 RID: 929
	internal class PropertyMapping : MemberMapping
	{
		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06001E89 RID: 7817 RVA: 0x0007D56F File Offset: 0x0007B76F
		public PropertyDescriptor Property
		{
			get
			{
				return this.field;
			}
		}

		// Token: 0x06001E8A RID: 7818 RVA: 0x0007D577 File Offset: 0x0007B777
		public PropertyMapping(string name, string ns, PropertyDescriptor field)
			: base(field.PropertyType, name, ns, field.IsReadOnly)
		{
			this.field = field;
			if (field.IsReadOnly)
			{
				field.Attributes.Contains(DesignerSerializationVisibilityAttribute.Content);
			}
		}

		// Token: 0x06001E8B RID: 7819 RVA: 0x0007D5AD File Offset: 0x0007B7AD
		public override void SetValue(object obj, object value)
		{
			this.field.SetValue(obj, value);
		}

		// Token: 0x06001E8C RID: 7820 RVA: 0x0007D5BC File Offset: 0x0007B7BC
		public override object GetValue(object obj)
		{
			return this.field.GetValue(obj);
		}

		// Token: 0x04000CEE RID: 3310
		private PropertyDescriptor field;
	}
}
