using System;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001D3 RID: 467
	public interface IPhrasingVisitor
	{
		// Token: 0x06000A0B RID: 2571
		void Visit(AttributePhrasingProperties phrasing);

		// Token: 0x06000A0C RID: 2572
		void Visit(NamePhrasingProperties phrasing);

		// Token: 0x06000A0D RID: 2573
		void Visit(AdjectivePhrasingProperties phrasing);

		// Token: 0x06000A0E RID: 2574
		void Visit(DynamicAdjectivePhrasingProperties phrasing);

		// Token: 0x06000A0F RID: 2575
		void Visit(NounPhrasingProperties phrasing);

		// Token: 0x06000A10 RID: 2576
		void Visit(DynamicNounPhrasingProperties phrasing);

		// Token: 0x06000A11 RID: 2577
		void Visit(PrepositionPhrasingProperties phrasing);

		// Token: 0x06000A12 RID: 2578
		void Visit(VerbPhrasingProperties phrasing);
	}
}
