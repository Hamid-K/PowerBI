using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001CD RID: 461
	[Serializable]
	internal class XmlNamespacesDefaultElement : XmlNamespacesElement
	{
		// Token: 0x060022D5 RID: 8917 RVA: 0x0015FE02 File Offset: 0x0015E002
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060022D6 RID: 8918 RVA: 0x0015FE0E File Offset: 0x0015E00E
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
