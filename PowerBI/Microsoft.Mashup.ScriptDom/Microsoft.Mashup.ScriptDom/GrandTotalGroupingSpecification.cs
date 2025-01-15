using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003BA RID: 954
	[Serializable]
	internal class GrandTotalGroupingSpecification : GroupingSpecification
	{
		// Token: 0x06002EA9 RID: 11945 RVA: 0x0016C85D File Offset: 0x0016AA5D
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002EAA RID: 11946 RVA: 0x0016C869 File Offset: 0x0016AA69
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
