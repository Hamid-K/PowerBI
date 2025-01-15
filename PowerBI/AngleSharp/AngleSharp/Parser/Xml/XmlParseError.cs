using System;

namespace AngleSharp.Parser.Xml
{
	// Token: 0x02000058 RID: 88
	internal enum XmlParseError : ushort
	{
		// Token: 0x040001E1 RID: 481
		EOF,
		// Token: 0x040001E2 RID: 482
		UndefinedMarkupDeclaration = 30,
		// Token: 0x040001E3 RID: 483
		CharacterReferenceInvalidNumber = 56,
		// Token: 0x040001E4 RID: 484
		CharacterReferenceInvalidCode,
		// Token: 0x040001E5 RID: 485
		CharacterReferenceNotTerminated,
		// Token: 0x040001E6 RID: 486
		DoctypeInvalid = 69,
		// Token: 0x040001E7 RID: 487
		TagClosingMismatch = 118,
		// Token: 0x040001E8 RID: 488
		XmlMissingRoot = 512,
		// Token: 0x040001E9 RID: 489
		XmlDoctypeAfterContent,
		// Token: 0x040001EA RID: 490
		XmlDeclarationInvalid,
		// Token: 0x040001EB RID: 491
		XmlDeclarationMisplaced,
		// Token: 0x040001EC RID: 492
		XmlDeclarationVersionUnsupported,
		// Token: 0x040001ED RID: 493
		XmlInvalidStartTag,
		// Token: 0x040001EE RID: 494
		XmlInvalidEndTag,
		// Token: 0x040001EF RID: 495
		XmlLtInAttributeValue,
		// Token: 0x040001F0 RID: 496
		XmlUniqueAttribute,
		// Token: 0x040001F1 RID: 497
		XmlInvalidPI,
		// Token: 0x040001F2 RID: 498
		XmlValidationFailed = 528,
		// Token: 0x040001F3 RID: 499
		XmlInvalidCharData,
		// Token: 0x040001F4 RID: 500
		XmlInvalidName,
		// Token: 0x040001F5 RID: 501
		XmlInvalidPubId,
		// Token: 0x040001F6 RID: 502
		XmlInvalidAttribute,
		// Token: 0x040001F7 RID: 503
		XmlInvalidComment,
		// Token: 0x040001F8 RID: 504
		DtdInvalid = 768,
		// Token: 0x040001F9 RID: 505
		DtdPEReferenceInvalid,
		// Token: 0x040001FA RID: 506
		DtdNameInvalid,
		// Token: 0x040001FB RID: 507
		DtdDeclInvalid,
		// Token: 0x040001FC RID: 508
		DtdTypeInvalid,
		// Token: 0x040001FD RID: 509
		DtdEntityInvalid,
		// Token: 0x040001FE RID: 510
		DtdAttListInvalid,
		// Token: 0x040001FF RID: 511
		DtdTypeContent,
		// Token: 0x04000200 RID: 512
		DtdUniqueElementViolated,
		// Token: 0x04000201 RID: 513
		DtdConditionInvalid,
		// Token: 0x04000202 RID: 514
		DtdTextDeclInvalid = 784,
		// Token: 0x04000203 RID: 515
		DtdNotationInvalid,
		// Token: 0x04000204 RID: 516
		DtdPEReferenceRecursion
	}
}
