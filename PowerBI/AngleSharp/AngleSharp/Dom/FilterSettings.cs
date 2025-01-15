using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x0200016D RID: 365
	[Flags]
	[DomName("NodeFilter")]
	public enum FilterSettings : ulong
	{
		// Token: 0x040009B4 RID: 2484
		[DomName("SHOW_ALL")]
		All = 4294967295UL,
		// Token: 0x040009B5 RID: 2485
		[DomName("SHOW_ELEMENT")]
		Element = 1UL,
		// Token: 0x040009B6 RID: 2486
		[DomName("SHOW_ATTRIBUTE")]
		[DomHistorical]
		Attribute = 2UL,
		// Token: 0x040009B7 RID: 2487
		[DomName("SHOW_TEXT")]
		Text = 4UL,
		// Token: 0x040009B8 RID: 2488
		[DomName("SHOW_CDATA_SECTION")]
		[DomHistorical]
		CharacterData = 8UL,
		// Token: 0x040009B9 RID: 2489
		[DomName("SHOW_ENTITY_REFERENCE")]
		[DomHistorical]
		EntityReference = 16UL,
		// Token: 0x040009BA RID: 2490
		[DomName("SHOW_ENTITY")]
		[DomHistorical]
		Entity = 32UL,
		// Token: 0x040009BB RID: 2491
		[DomName("SHOW_PROCESSING_INSTRUCTION")]
		ProcessingInstruction = 64UL,
		// Token: 0x040009BC RID: 2492
		[DomName("SHOW_COMMENT")]
		Comment = 128UL,
		// Token: 0x040009BD RID: 2493
		[DomName("SHOW_DOCUMENT")]
		Document = 256UL,
		// Token: 0x040009BE RID: 2494
		[DomName("SHOW_DOCUMENT_TYPE")]
		DocumentType = 512UL,
		// Token: 0x040009BF RID: 2495
		[DomName("SHOW_DOCUMENT_FRAGMENT")]
		DocumentFragment = 1024UL,
		// Token: 0x040009C0 RID: 2496
		[DomName("SHOW_NOTATION")]
		[DomHistorical]
		Notation = 2048UL
	}
}
