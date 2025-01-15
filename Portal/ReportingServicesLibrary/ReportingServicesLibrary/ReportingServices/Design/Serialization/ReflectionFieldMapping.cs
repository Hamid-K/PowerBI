using System;
using System.Reflection;

namespace Microsoft.ReportingServices.Design.Serialization
{
	// Token: 0x020003A0 RID: 928
	internal class ReflectionFieldMapping : MemberMapping
	{
		// Token: 0x06001E85 RID: 7813 RVA: 0x0007D4C5 File Offset: 0x0007B6C5
		public ReflectionFieldMapping(string name, string ns, FieldInfo field)
			: base(field.FieldType, name, ns, false)
		{
			this.field = field;
		}

		// Token: 0x06001E86 RID: 7814 RVA: 0x0007D4DD File Offset: 0x0007B6DD
		public ReflectionFieldMapping(string name, string ns, PropertyInfo field)
			: base(field.PropertyType, name, ns, !field.CanWrite)
		{
			this.field = field;
		}

		// Token: 0x06001E87 RID: 7815 RVA: 0x0007D4FD File Offset: 0x0007B6FD
		public override void SetValue(object obj, object value)
		{
			if (this.field is FieldInfo)
			{
				((FieldInfo)this.field).SetValue(obj, value);
				return;
			}
			((PropertyInfo)this.field).SetValue(obj, value, new object[0]);
		}

		// Token: 0x06001E88 RID: 7816 RVA: 0x0007D537 File Offset: 0x0007B737
		public override object GetValue(object obj)
		{
			if (this.field is FieldInfo)
			{
				return ((FieldInfo)this.field).GetValue(obj);
			}
			return ((PropertyInfo)this.field).GetValue(obj, new object[0]);
		}

		// Token: 0x04000CED RID: 3309
		private MemberInfo field;
	}
}
