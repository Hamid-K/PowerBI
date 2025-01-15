using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x0200016F RID: 367
	[DomName("Document")]
	public enum NodeType : byte
	{
		// Token: 0x040009C7 RID: 2503
		[DomName("ELEMENT_NODE")]
		Element = 1,
		// Token: 0x040009C8 RID: 2504
		[DomName("ATTRIBUTE_NODE")]
		[DomHistorical]
		Attribute,
		// Token: 0x040009C9 RID: 2505
		[DomName("TEXT_NODE")]
		Text,
		// Token: 0x040009CA RID: 2506
		[DomName("CDATA_SECTION_NODE")]
		[DomHistorical]
		CharacterData,
		// Token: 0x040009CB RID: 2507
		[DomName("ENTITY_REFERENCE_NODE")]
		[DomHistorical]
		EntityReference,
		// Token: 0x040009CC RID: 2508
		[DomName("ENTITY_NODE")]
		[DomHistorical]
		Entity,
		// Token: 0x040009CD RID: 2509
		[DomName("PROCESSING_INSTRUCTION_NODE")]
		[DomHistorical]
		ProcessingInstruction,
		// Token: 0x040009CE RID: 2510
		[DomName("COMMENT_NODE")]
		Comment,
		// Token: 0x040009CF RID: 2511
		[DomName("DOCUMENT_NODE")]
		Document,
		// Token: 0x040009D0 RID: 2512
		[DomName("DOCUMENT_TYPE_NODE")]
		DocumentType,
		// Token: 0x040009D1 RID: 2513
		[DomName("DOCUMENT_FRAGMENT_NODE")]
		DocumentFragment,
		// Token: 0x040009D2 RID: 2514
		[DomName("NOTATION_NODE")]
		[DomHistorical]
		Notation
	}
}
