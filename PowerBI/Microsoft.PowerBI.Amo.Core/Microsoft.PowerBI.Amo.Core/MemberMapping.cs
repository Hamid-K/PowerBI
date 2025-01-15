using System;
using System.Xml.Serialization;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000011 RID: 17
	internal abstract class MemberMapping : Mapping
	{
		// Token: 0x0600007B RID: 123 RVA: 0x00005347 File Offset: 0x00003547
		public MemberMapping(Type type, string name, string ns, bool isReadOnly)
		{
			this.MemberName = name;
			this.MemberNamespace = ns;
			this.MemberType = type;
			this.IsReadOnly = isReadOnly;
		}

		// Token: 0x0600007C RID: 124
		public abstract void SetValue(object obj, object value);

		// Token: 0x0600007D RID: 125
		public abstract object GetValue(object obj);

		// Token: 0x0400006A RID: 106
		public bool IsReadOnly;

		// Token: 0x0400006B RID: 107
		public Type MemberType;

		// Token: 0x0400006C RID: 108
		public string MemberName;

		// Token: 0x0400006D RID: 109
		public string MemberNamespace;

		// Token: 0x0400006E RID: 110
		public XmlAttributes XmlAttributes;
	}
}
