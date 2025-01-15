using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F8A RID: 12170
	[GeneratedCode("DomGen", "2.0")]
	internal class ScrollbarVisibility : OpenXmlLeafElement
	{
		// Token: 0x170091B3 RID: 37299
		// (get) Token: 0x0601A3A0 RID: 107424 RVA: 0x0035F472 File Offset: 0x0035D672
		public override string LocalName
		{
			get
			{
				return "scrollbar";
			}
		}

		// Token: 0x170091B4 RID: 37300
		// (get) Token: 0x0601A3A1 RID: 107425 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170091B5 RID: 37301
		// (get) Token: 0x0601A3A2 RID: 107426 RVA: 0x0035F479 File Offset: 0x0035D679
		internal override int ElementTypeId
		{
			get
			{
				return 11853;
			}
		}

		// Token: 0x0601A3A3 RID: 107427 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170091B6 RID: 37302
		// (get) Token: 0x0601A3A4 RID: 107428 RVA: 0x0035F480 File Offset: 0x0035D680
		internal override string[] AttributeTagNames
		{
			get
			{
				return ScrollbarVisibility.attributeTagNames;
			}
		}

		// Token: 0x170091B7 RID: 37303
		// (get) Token: 0x0601A3A5 RID: 107429 RVA: 0x0035F487 File Offset: 0x0035D687
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ScrollbarVisibility.attributeNamespaceIds;
			}
		}

		// Token: 0x170091B8 RID: 37304
		// (get) Token: 0x0601A3A6 RID: 107430 RVA: 0x0035F48E File Offset: 0x0035D68E
		// (set) Token: 0x0601A3A7 RID: 107431 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<FrameScrollbarVisibilityValues> Val
		{
			get
			{
				return (EnumValue<FrameScrollbarVisibilityValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601A3A9 RID: 107433 RVA: 0x0035F49D File Offset: 0x0035D69D
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<FrameScrollbarVisibilityValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A3AA RID: 107434 RVA: 0x0035F4BF File Offset: 0x0035D6BF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ScrollbarVisibility>(deep);
		}

		// Token: 0x0400AC4C RID: 44108
		private const string tagName = "scrollbar";

		// Token: 0x0400AC4D RID: 44109
		private const byte tagNsId = 23;

		// Token: 0x0400AC4E RID: 44110
		internal const int ElementTypeIdConst = 11853;

		// Token: 0x0400AC4F RID: 44111
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AC50 RID: 44112
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
