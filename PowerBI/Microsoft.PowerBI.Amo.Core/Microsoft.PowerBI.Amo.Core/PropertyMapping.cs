using System;
using System.ComponentModel;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000013 RID: 19
	internal sealed class PropertyMapping : MemberMapping
	{
		// Token: 0x06000082 RID: 130 RVA: 0x00005416 File Offset: 0x00003616
		public PropertyMapping(string name, string ns, PropertyDescriptor field)
			: base(field.PropertyType, name, ns, field.IsReadOnly)
		{
			this.field = field;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00005433 File Offset: 0x00003633
		public override void SetValue(object obj, object value)
		{
			this.field.SetValue(obj, value);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00005442 File Offset: 0x00003642
		public override object GetValue(object obj)
		{
			return this.field.GetValue(obj);
		}

		// Token: 0x04000070 RID: 112
		private PropertyDescriptor field;
	}
}
