using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FF8 RID: 12280
	[GeneratedCode("DomGen", "2.0")]
	internal class ColorSchemeMapping : OpenXmlLeafElement
	{
		// Token: 0x170095AA RID: 38314
		// (get) Token: 0x0601AC0A RID: 109578 RVA: 0x00367149 File Offset: 0x00365349
		public override string LocalName
		{
			get
			{
				return "clrSchemeMapping";
			}
		}

		// Token: 0x170095AB RID: 38315
		// (get) Token: 0x0601AC0B RID: 109579 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170095AC RID: 38316
		// (get) Token: 0x0601AC0C RID: 109580 RVA: 0x00367150 File Offset: 0x00365350
		internal override int ElementTypeId
		{
			get
			{
				return 12044;
			}
		}

		// Token: 0x0601AC0D RID: 109581 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170095AD RID: 38317
		// (get) Token: 0x0601AC0E RID: 109582 RVA: 0x00367157 File Offset: 0x00365357
		internal override string[] AttributeTagNames
		{
			get
			{
				return ColorSchemeMapping.attributeTagNames;
			}
		}

		// Token: 0x170095AE RID: 38318
		// (get) Token: 0x0601AC0F RID: 109583 RVA: 0x0036715E File Offset: 0x0036535E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ColorSchemeMapping.attributeNamespaceIds;
			}
		}

		// Token: 0x170095AF RID: 38319
		// (get) Token: 0x0601AC10 RID: 109584 RVA: 0x00367165 File Offset: 0x00365365
		// (set) Token: 0x0601AC11 RID: 109585 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "bg1")]
		public EnumValue<ColorSchemeIndexValues> Background1
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170095B0 RID: 38320
		// (get) Token: 0x0601AC12 RID: 109586 RVA: 0x00367174 File Offset: 0x00365374
		// (set) Token: 0x0601AC13 RID: 109587 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "t1")]
		public EnumValue<ColorSchemeIndexValues> Text1
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170095B1 RID: 38321
		// (get) Token: 0x0601AC14 RID: 109588 RVA: 0x00367183 File Offset: 0x00365383
		// (set) Token: 0x0601AC15 RID: 109589 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "bg2")]
		public EnumValue<ColorSchemeIndexValues> Background2
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170095B2 RID: 38322
		// (get) Token: 0x0601AC16 RID: 109590 RVA: 0x00367192 File Offset: 0x00365392
		// (set) Token: 0x0601AC17 RID: 109591 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "t2")]
		public EnumValue<ColorSchemeIndexValues> Text2
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x170095B3 RID: 38323
		// (get) Token: 0x0601AC18 RID: 109592 RVA: 0x003671A1 File Offset: 0x003653A1
		// (set) Token: 0x0601AC19 RID: 109593 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(23, "accent1")]
		public EnumValue<ColorSchemeIndexValues> Accent1
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x170095B4 RID: 38324
		// (get) Token: 0x0601AC1A RID: 109594 RVA: 0x003671B0 File Offset: 0x003653B0
		// (set) Token: 0x0601AC1B RID: 109595 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(23, "accent2")]
		public EnumValue<ColorSchemeIndexValues> Accent2
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x170095B5 RID: 38325
		// (get) Token: 0x0601AC1C RID: 109596 RVA: 0x003671BF File Offset: 0x003653BF
		// (set) Token: 0x0601AC1D RID: 109597 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(23, "accent3")]
		public EnumValue<ColorSchemeIndexValues> Accent3
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x170095B6 RID: 38326
		// (get) Token: 0x0601AC1E RID: 109598 RVA: 0x003671CE File Offset: 0x003653CE
		// (set) Token: 0x0601AC1F RID: 109599 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(23, "accent4")]
		public EnumValue<ColorSchemeIndexValues> Accent4
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x170095B7 RID: 38327
		// (get) Token: 0x0601AC20 RID: 109600 RVA: 0x003671DD File Offset: 0x003653DD
		// (set) Token: 0x0601AC21 RID: 109601 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(23, "accent5")]
		public EnumValue<ColorSchemeIndexValues> Accent5
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x170095B8 RID: 38328
		// (get) Token: 0x0601AC22 RID: 109602 RVA: 0x003671EC File Offset: 0x003653EC
		// (set) Token: 0x0601AC23 RID: 109603 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(23, "accent6")]
		public EnumValue<ColorSchemeIndexValues> Accent6
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x170095B9 RID: 38329
		// (get) Token: 0x0601AC24 RID: 109604 RVA: 0x003671FC File Offset: 0x003653FC
		// (set) Token: 0x0601AC25 RID: 109605 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(23, "hyperlink")]
		public EnumValue<ColorSchemeIndexValues> Hyperlink
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x170095BA RID: 38330
		// (get) Token: 0x0601AC26 RID: 109606 RVA: 0x0036720C File Offset: 0x0036540C
		// (set) Token: 0x0601AC27 RID: 109607 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(23, "followedHyperlink")]
		public EnumValue<ColorSchemeIndexValues> FollowedHyperlink
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x0601AC29 RID: 109609 RVA: 0x0036721C File Offset: 0x0036541C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "bg1" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			if (23 == namespaceId && "t1" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			if (23 == namespaceId && "bg2" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			if (23 == namespaceId && "t2" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			if (23 == namespaceId && "accent1" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			if (23 == namespaceId && "accent2" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			if (23 == namespaceId && "accent3" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			if (23 == namespaceId && "accent4" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			if (23 == namespaceId && "accent5" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			if (23 == namespaceId && "accent6" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			if (23 == namespaceId && "hyperlink" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			if (23 == namespaceId && "followedHyperlink" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AC2A RID: 109610 RVA: 0x00367351 File Offset: 0x00365551
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColorSchemeMapping>(deep);
		}

		// Token: 0x0400AE2E RID: 44590
		private const string tagName = "clrSchemeMapping";

		// Token: 0x0400AE2F RID: 44591
		private const byte tagNsId = 23;

		// Token: 0x0400AE30 RID: 44592
		internal const int ElementTypeIdConst = 12044;

		// Token: 0x0400AE31 RID: 44593
		private static string[] attributeTagNames = new string[]
		{
			"bg1", "t1", "bg2", "t2", "accent1", "accent2", "accent3", "accent4", "accent5", "accent6",
			"hyperlink", "followedHyperlink"
		};

		// Token: 0x0400AE32 RID: 44594
		private static byte[] attributeNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23
		};
	}
}
