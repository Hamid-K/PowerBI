using System;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003105 RID: 12549
	internal class AttributeConstraint
	{
		// Token: 0x0601B37F RID: 111487 RVA: 0x00372FDF File Offset: 0x003711DF
		internal AttributeConstraint(XsdAttributeUse xsdAttributeUse, SimpleTypeRestriction simpleTypeConstraint)
		{
			this.XsdAttributeUse = xsdAttributeUse;
			this.SimpleTypeConstraint = simpleTypeConstraint;
		}

		// Token: 0x0601B380 RID: 111488 RVA: 0x00372FF5 File Offset: 0x003711F5
		internal AttributeConstraint(XsdAttributeUse xsdAttributeUse, SimpleTypeRestriction simpleTypeConstraint, FileFormatVersions supportedVersion)
		{
			this.XsdAttributeUse = xsdAttributeUse;
			this.SimpleTypeConstraint = simpleTypeConstraint;
			this.SupportedVersion = supportedVersion;
		}

		// Token: 0x170098A2 RID: 39074
		// (get) Token: 0x0601B381 RID: 111489 RVA: 0x00373012 File Offset: 0x00371212
		// (set) Token: 0x0601B382 RID: 111490 RVA: 0x0037301A File Offset: 0x0037121A
		internal XsdAttributeUse XsdAttributeUse { get; private set; }

		// Token: 0x170098A3 RID: 39075
		// (get) Token: 0x0601B383 RID: 111491 RVA: 0x00373023 File Offset: 0x00371223
		// (set) Token: 0x0601B384 RID: 111492 RVA: 0x0037302B File Offset: 0x0037122B
		internal SimpleTypeRestriction SimpleTypeConstraint { get; private set; }

		// Token: 0x170098A4 RID: 39076
		// (get) Token: 0x0601B385 RID: 111493 RVA: 0x00373034 File Offset: 0x00371234
		// (set) Token: 0x0601B386 RID: 111494 RVA: 0x0037303C File Offset: 0x0037123C
		internal FileFormatVersions SupportedVersion { get; private set; }
	}
}
