using System;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel.Serialization
{
	// Token: 0x020002E2 RID: 738
	public abstract class MemberMapping : TypeMapping
	{
		// Token: 0x060016B9 RID: 5817 RVA: 0x00035BF3 File Offset: 0x00033DF3
		public MemberMapping(Type type, string name, string ns, bool isReadOnly)
			: base(type)
		{
			this.Name = name;
			this.Namespace = ns;
			this.IsReadOnly = isReadOnly;
		}

		// Token: 0x060016BA RID: 5818
		public abstract void SetValue(object obj, object value);

		// Token: 0x060016BB RID: 5819
		public abstract object GetValue(object obj);

		// Token: 0x060016BC RID: 5820
		public abstract bool HasValue(object obj);

		// Token: 0x04000706 RID: 1798
		public bool IsReadOnly;

		// Token: 0x04000707 RID: 1799
		public XmlAttributes XmlAttributes;
	}
}
