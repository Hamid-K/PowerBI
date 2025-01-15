using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F03 RID: 12035
	[GeneratedCode("DomGen", "2.0")]
	internal class TablePositionProperties : OpenXmlLeafElement
	{
		// Token: 0x17008DD3 RID: 36307
		// (get) Token: 0x06019AF8 RID: 105208 RVA: 0x00353DD8 File Offset: 0x00351FD8
		public override string LocalName
		{
			get
			{
				return "tblpPr";
			}
		}

		// Token: 0x17008DD4 RID: 36308
		// (get) Token: 0x06019AF9 RID: 105209 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008DD5 RID: 36309
		// (get) Token: 0x06019AFA RID: 105210 RVA: 0x00353DDF File Offset: 0x00351FDF
		internal override int ElementTypeId
		{
			get
			{
				return 11672;
			}
		}

		// Token: 0x06019AFB RID: 105211 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008DD6 RID: 36310
		// (get) Token: 0x06019AFC RID: 105212 RVA: 0x00353DE6 File Offset: 0x00351FE6
		internal override string[] AttributeTagNames
		{
			get
			{
				return TablePositionProperties.attributeTagNames;
			}
		}

		// Token: 0x17008DD7 RID: 36311
		// (get) Token: 0x06019AFD RID: 105213 RVA: 0x00353DED File Offset: 0x00351FED
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TablePositionProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17008DD8 RID: 36312
		// (get) Token: 0x06019AFE RID: 105214 RVA: 0x0034726F File Offset: 0x0034546F
		// (set) Token: 0x06019AFF RID: 105215 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "leftFromText")]
		public Int16Value LeftFromText
		{
			get
			{
				return (Int16Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008DD9 RID: 36313
		// (get) Token: 0x06019B00 RID: 105216 RVA: 0x0034727E File Offset: 0x0034547E
		// (set) Token: 0x06019B01 RID: 105217 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "rightFromText")]
		public Int16Value RightFromText
		{
			get
			{
				return (Int16Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17008DDA RID: 36314
		// (get) Token: 0x06019B02 RID: 105218 RVA: 0x0034749D File Offset: 0x0034569D
		// (set) Token: 0x06019B03 RID: 105219 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "topFromText")]
		public Int16Value TopFromText
		{
			get
			{
				return (Int16Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17008DDB RID: 36315
		// (get) Token: 0x06019B04 RID: 105220 RVA: 0x00353DF4 File Offset: 0x00351FF4
		// (set) Token: 0x06019B05 RID: 105221 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "bottomFromText")]
		public Int16Value BottomFromText
		{
			get
			{
				return (Int16Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17008DDC RID: 36316
		// (get) Token: 0x06019B06 RID: 105222 RVA: 0x00353E03 File Offset: 0x00352003
		// (set) Token: 0x06019B07 RID: 105223 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(23, "vertAnchor")]
		public EnumValue<VerticalAnchorValues> VerticalAnchor
		{
			get
			{
				return (EnumValue<VerticalAnchorValues>)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17008DDD RID: 36317
		// (get) Token: 0x06019B08 RID: 105224 RVA: 0x00353E12 File Offset: 0x00352012
		// (set) Token: 0x06019B09 RID: 105225 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(23, "horzAnchor")]
		public EnumValue<HorizontalAnchorValues> HorizontalAnchor
		{
			get
			{
				return (EnumValue<HorizontalAnchorValues>)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17008DDE RID: 36318
		// (get) Token: 0x06019B0A RID: 105226 RVA: 0x00353E21 File Offset: 0x00352021
		// (set) Token: 0x06019B0B RID: 105227 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(23, "tblpXSpec")]
		public EnumValue<HorizontalAlignmentValues> TablePositionXAlignment
		{
			get
			{
				return (EnumValue<HorizontalAlignmentValues>)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17008DDF RID: 36319
		// (get) Token: 0x06019B0C RID: 105228 RVA: 0x002D14EB File Offset: 0x002CF6EB
		// (set) Token: 0x06019B0D RID: 105229 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(23, "tblpX")]
		public Int32Value TablePositionX
		{
			get
			{
				return (Int32Value)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17008DE0 RID: 36320
		// (get) Token: 0x06019B0E RID: 105230 RVA: 0x00353E30 File Offset: 0x00352030
		// (set) Token: 0x06019B0F RID: 105231 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(23, "tblpYSpec")]
		public EnumValue<VerticalAlignmentValues> TablePositionYAlignment
		{
			get
			{
				return (EnumValue<VerticalAlignmentValues>)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17008DE1 RID: 36321
		// (get) Token: 0x06019B10 RID: 105232 RVA: 0x002D14FA File Offset: 0x002CF6FA
		// (set) Token: 0x06019B11 RID: 105233 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(23, "tblpY")]
		public Int32Value TablePositionY
		{
			get
			{
				return (Int32Value)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x06019B13 RID: 105235 RVA: 0x00353E40 File Offset: 0x00352040
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "leftFromText" == name)
			{
				return new Int16Value();
			}
			if (23 == namespaceId && "rightFromText" == name)
			{
				return new Int16Value();
			}
			if (23 == namespaceId && "topFromText" == name)
			{
				return new Int16Value();
			}
			if (23 == namespaceId && "bottomFromText" == name)
			{
				return new Int16Value();
			}
			if (23 == namespaceId && "vertAnchor" == name)
			{
				return new EnumValue<VerticalAnchorValues>();
			}
			if (23 == namespaceId && "horzAnchor" == name)
			{
				return new EnumValue<HorizontalAnchorValues>();
			}
			if (23 == namespaceId && "tblpXSpec" == name)
			{
				return new EnumValue<HorizontalAlignmentValues>();
			}
			if (23 == namespaceId && "tblpX" == name)
			{
				return new Int32Value();
			}
			if (23 == namespaceId && "tblpYSpec" == name)
			{
				return new EnumValue<VerticalAlignmentValues>();
			}
			if (23 == namespaceId && "tblpY" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019B14 RID: 105236 RVA: 0x00353F45 File Offset: 0x00352145
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TablePositionProperties>(deep);
		}

		// Token: 0x0400AA20 RID: 43552
		private const string tagName = "tblpPr";

		// Token: 0x0400AA21 RID: 43553
		private const byte tagNsId = 23;

		// Token: 0x0400AA22 RID: 43554
		internal const int ElementTypeIdConst = 11672;

		// Token: 0x0400AA23 RID: 43555
		private static string[] attributeTagNames = new string[] { "leftFromText", "rightFromText", "topFromText", "bottomFromText", "vertAnchor", "horzAnchor", "tblpXSpec", "tblpX", "tblpYSpec", "tblpY" };

		// Token: 0x0400AA24 RID: 43556
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23, 23, 23, 23, 23, 23, 23 };
	}
}
