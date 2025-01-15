using System;
using System.Reflection;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000012 RID: 18
	internal sealed class ReflectionFieldMapping : MemberMapping
	{
		// Token: 0x0600007E RID: 126 RVA: 0x0000536C File Offset: 0x0000356C
		public ReflectionFieldMapping(string name, string ns, FieldInfo field)
			: base(field.FieldType, name, ns, false)
		{
			this.field = field;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00005384 File Offset: 0x00003584
		public ReflectionFieldMapping(string name, string ns, PropertyInfo field)
			: base(field.PropertyType, name, ns, !field.CanWrite)
		{
			this.field = field;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000053A4 File Offset: 0x000035A4
		public override void SetValue(object obj, object value)
		{
			if (this.field is FieldInfo)
			{
				((FieldInfo)this.field).SetValue(obj, value);
				return;
			}
			((PropertyInfo)this.field).SetValue(obj, value, new object[0]);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000053DE File Offset: 0x000035DE
		public override object GetValue(object obj)
		{
			if (this.field is FieldInfo)
			{
				return ((FieldInfo)this.field).GetValue(obj);
			}
			return ((PropertyInfo)this.field).GetValue(obj, new object[0]);
		}

		// Token: 0x0400006F RID: 111
		private MemberInfo field;
	}
}
