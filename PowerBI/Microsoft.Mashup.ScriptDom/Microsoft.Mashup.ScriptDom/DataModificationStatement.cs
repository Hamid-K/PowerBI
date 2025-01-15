using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000236 RID: 566
	[Serializable]
	internal abstract class DataModificationStatement : StatementWithCtesAndXmlNamespaces
	{
		// Token: 0x06002555 RID: 9557 RVA: 0x00162C72 File Offset: 0x00160E72
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
