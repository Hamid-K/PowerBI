using System;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001D5 RID: 469
	public interface IPhrasingVisitor<T, TArg>
	{
		// Token: 0x06000A1B RID: 2587
		T Visit(AttributePhrasingProperties phrasing, TArg arg);

		// Token: 0x06000A1C RID: 2588
		T Visit(NamePhrasingProperties phrasing, TArg arg);

		// Token: 0x06000A1D RID: 2589
		T Visit(AdjectivePhrasingProperties phrasing, TArg arg);

		// Token: 0x06000A1E RID: 2590
		T Visit(DynamicAdjectivePhrasingProperties phrasing, TArg arg);

		// Token: 0x06000A1F RID: 2591
		T Visit(NounPhrasingProperties phrasing, TArg arg);

		// Token: 0x06000A20 RID: 2592
		T Visit(DynamicNounPhrasingProperties phrasing, TArg arg);

		// Token: 0x06000A21 RID: 2593
		T Visit(PrepositionPhrasingProperties phrasing, TArg arg);

		// Token: 0x06000A22 RID: 2594
		T Visit(VerbPhrasingProperties phrasing, TArg arg);
	}
}
