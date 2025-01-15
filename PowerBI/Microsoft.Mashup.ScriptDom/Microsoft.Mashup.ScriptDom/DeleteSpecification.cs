using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200023A RID: 570
	[Serializable]
	internal class DeleteSpecification : UpdateDeleteSpecificationBase
	{
		// Token: 0x0600256C RID: 9580 RVA: 0x00162E01 File Offset: 0x00161001
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x00162E0D File Offset: 0x0016100D
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
