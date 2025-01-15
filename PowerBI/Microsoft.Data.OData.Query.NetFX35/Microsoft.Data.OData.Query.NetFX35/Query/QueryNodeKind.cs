using System;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x0200004E RID: 78
	public enum QueryNodeKind
	{
		// Token: 0x040001D4 RID: 468
		Extension,
		// Token: 0x040001D5 RID: 469
		QueryDescriptor,
		// Token: 0x040001D6 RID: 470
		EntitySet,
		// Token: 0x040001D7 RID: 471
		KeyLookup,
		// Token: 0x040001D8 RID: 472
		Constant,
		// Token: 0x040001D9 RID: 473
		Convert,
		// Token: 0x040001DA RID: 474
		CollectionServiceOperation,
		// Token: 0x040001DB RID: 475
		SingleValueServiceOperation,
		// Token: 0x040001DC RID: 476
		UncomposableServiceOperation,
		// Token: 0x040001DD RID: 477
		Filter,
		// Token: 0x040001DE RID: 478
		Parameter,
		// Token: 0x040001DF RID: 479
		Skip,
		// Token: 0x040001E0 RID: 480
		Top,
		// Token: 0x040001E1 RID: 481
		BinaryOperator,
		// Token: 0x040001E2 RID: 482
		UnaryOperator,
		// Token: 0x040001E3 RID: 483
		PropertyAccess,
		// Token: 0x040001E4 RID: 484
		OrderBy,
		// Token: 0x040001E5 RID: 485
		SingleValueFunctionCall,
		// Token: 0x040001E6 RID: 486
		CustomQueryOption
	}
}
