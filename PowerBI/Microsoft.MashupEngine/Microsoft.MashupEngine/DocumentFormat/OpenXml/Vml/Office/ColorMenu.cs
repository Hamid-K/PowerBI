using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Office
{
	// Token: 0x02002220 RID: 8736
	[GeneratedCode("DomGen", "2.0")]
	internal class ColorMenu : OpenXmlLeafElement
	{
		// Token: 0x1700394B RID: 14667
		// (get) Token: 0x0600E045 RID: 57413 RVA: 0x002BFBE0 File Offset: 0x002BDDE0
		public override string LocalName
		{
			get
			{
				return "colormenu";
			}
		}

		// Token: 0x1700394C RID: 14668
		// (get) Token: 0x0600E046 RID: 57414 RVA: 0x0012AF09 File Offset: 0x00129109
		internal override byte NamespaceId
		{
			get
			{
				return 27;
			}
		}

		// Token: 0x1700394D RID: 14669
		// (get) Token: 0x0600E047 RID: 57415 RVA: 0x002BFBE7 File Offset: 0x002BDDE7
		internal override int ElementTypeId
		{
			get
			{
				return 12429;
			}
		}

		// Token: 0x0600E048 RID: 57416 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700394E RID: 14670
		// (get) Token: 0x0600E049 RID: 57417 RVA: 0x002BFBEE File Offset: 0x002BDDEE
		internal override string[] AttributeTagNames
		{
			get
			{
				return ColorMenu.attributeTagNames;
			}
		}

		// Token: 0x1700394F RID: 14671
		// (get) Token: 0x0600E04A RID: 57418 RVA: 0x002BFBF5 File Offset: 0x002BDDF5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ColorMenu.attributeNamespaceIds;
			}
		}

		// Token: 0x17003950 RID: 14672
		// (get) Token: 0x0600E04B RID: 57419 RVA: 0x002BD45C File Offset: 0x002BB65C
		// (set) Token: 0x0600E04C RID: 57420 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(26, "ext")]
		public EnumValue<ExtensionHandlingBehaviorValues> Extension
		{
			get
			{
				return (EnumValue<ExtensionHandlingBehaviorValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17003951 RID: 14673
		// (get) Token: 0x0600E04D RID: 57421 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600E04E RID: 57422 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "strokecolor")]
		public StringValue StrokeColor
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17003952 RID: 14674
		// (get) Token: 0x0600E04F RID: 57423 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600E050 RID: 57424 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "fillcolor")]
		public StringValue FillColor
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17003953 RID: 14675
		// (get) Token: 0x0600E051 RID: 57425 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600E052 RID: 57426 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "shadowcolor")]
		public StringValue ShadowColor
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17003954 RID: 14676
		// (get) Token: 0x0600E053 RID: 57427 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600E054 RID: 57428 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "extrusioncolor")]
		public StringValue ExtrusionColor
		{
			get
			{
				return (StringValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x0600E056 RID: 57430 RVA: 0x002BFBFC File Offset: 0x002BDDFC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (26 == namespaceId && "ext" == name)
			{
				return new EnumValue<ExtensionHandlingBehaviorValues>();
			}
			if (namespaceId == 0 && "strokecolor" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "fillcolor" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "shadowcolor" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "extrusioncolor" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600E057 RID: 57431 RVA: 0x002BFC81 File Offset: 0x002BDE81
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColorMenu>(deep);
		}

		// Token: 0x0600E058 RID: 57432 RVA: 0x002BFC8C File Offset: 0x002BDE8C
		// Note: this type is marked as 'beforefieldinit'.
		static ColorMenu()
		{
			byte[] array = new byte[5];
			array[0] = 26;
			ColorMenu.attributeNamespaceIds = array;
		}

		// Token: 0x04006DDE RID: 28126
		private const string tagName = "colormenu";

		// Token: 0x04006DDF RID: 28127
		private const byte tagNsId = 27;

		// Token: 0x04006DE0 RID: 28128
		internal const int ElementTypeIdConst = 12429;

		// Token: 0x04006DE1 RID: 28129
		private static string[] attributeTagNames = new string[] { "ext", "strokecolor", "fillcolor", "shadowcolor", "extrusioncolor" };

		// Token: 0x04006DE2 RID: 28130
		private static byte[] attributeNamespaceIds;
	}
}
