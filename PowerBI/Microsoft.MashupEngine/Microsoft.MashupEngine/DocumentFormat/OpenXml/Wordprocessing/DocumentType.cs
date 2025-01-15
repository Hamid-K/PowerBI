using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FE0 RID: 12256
	[GeneratedCode("DomGen", "2.0")]
	internal class DocumentType : OpenXmlLeafElement
	{
		// Token: 0x170094C8 RID: 38088
		// (get) Token: 0x0601AA2B RID: 109099 RVA: 0x0036536C File Offset: 0x0036356C
		public override string LocalName
		{
			get
			{
				return "documentType";
			}
		}

		// Token: 0x170094C9 RID: 38089
		// (get) Token: 0x0601AA2C RID: 109100 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170094CA RID: 38090
		// (get) Token: 0x0601AA2D RID: 109101 RVA: 0x00365373 File Offset: 0x00363573
		internal override int ElementTypeId
		{
			get
			{
				return 11986;
			}
		}

		// Token: 0x0601AA2E RID: 109102 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170094CB RID: 38091
		// (get) Token: 0x0601AA2F RID: 109103 RVA: 0x0036537A File Offset: 0x0036357A
		internal override string[] AttributeTagNames
		{
			get
			{
				return DocumentType.attributeTagNames;
			}
		}

		// Token: 0x170094CC RID: 38092
		// (get) Token: 0x0601AA30 RID: 109104 RVA: 0x00365381 File Offset: 0x00363581
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DocumentType.attributeNamespaceIds;
			}
		}

		// Token: 0x170094CD RID: 38093
		// (get) Token: 0x0601AA31 RID: 109105 RVA: 0x00365388 File Offset: 0x00363588
		// (set) Token: 0x0601AA32 RID: 109106 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<DocumentTypeValues> Val
		{
			get
			{
				return (EnumValue<DocumentTypeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601AA34 RID: 109108 RVA: 0x00365397 File Offset: 0x00363597
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<DocumentTypeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AA35 RID: 109109 RVA: 0x003653B9 File Offset: 0x003635B9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DocumentType>(deep);
		}

		// Token: 0x0400ADCF RID: 44495
		private const string tagName = "documentType";

		// Token: 0x0400ADD0 RID: 44496
		private const byte tagNsId = 23;

		// Token: 0x0400ADD1 RID: 44497
		internal const int ElementTypeIdConst = 11986;

		// Token: 0x0400ADD2 RID: 44498
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400ADD3 RID: 44499
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
