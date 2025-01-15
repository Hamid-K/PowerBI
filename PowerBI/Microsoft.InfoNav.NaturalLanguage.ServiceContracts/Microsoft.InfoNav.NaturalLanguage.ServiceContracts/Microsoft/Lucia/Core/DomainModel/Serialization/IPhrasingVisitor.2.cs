using System;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001D4 RID: 468
	public interface IPhrasingVisitor<T>
	{
		// Token: 0x06000A13 RID: 2579
		T Visit(AttributePhrasingProperties phrasing);

		// Token: 0x06000A14 RID: 2580
		T Visit(NamePhrasingProperties phrasing);

		// Token: 0x06000A15 RID: 2581
		T Visit(AdjectivePhrasingProperties phrasing);

		// Token: 0x06000A16 RID: 2582
		T Visit(DynamicAdjectivePhrasingProperties phrasing);

		// Token: 0x06000A17 RID: 2583
		T Visit(NounPhrasingProperties phrasing);

		// Token: 0x06000A18 RID: 2584
		T Visit(DynamicNounPhrasingProperties phrasing);

		// Token: 0x06000A19 RID: 2585
		T Visit(PrepositionPhrasingProperties phrasing);

		// Token: 0x06000A1A RID: 2586
		T Visit(VerbPhrasingProperties phrasing);
	}
}
