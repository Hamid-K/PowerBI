using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001CB RID: 459
	[Serializable]
	internal class XmlNamespaces : TSqlFragment
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060022CD RID: 8909 RVA: 0x0015FD5F File Offset: 0x0015DF5F
		public IList<XmlNamespacesElement> XmlNamespacesElements
		{
			get
			{
				return this._xmlNamespacesElements;
			}
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x0015FD67 File Offset: 0x0015DF67
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x0015FD74 File Offset: 0x0015DF74
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.XmlNamespacesElements.Count;
			while (i < count)
			{
				this.XmlNamespacesElements[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A43 RID: 6723
		private List<XmlNamespacesElement> _xmlNamespacesElements = new List<XmlNamespacesElement>();
	}
}
