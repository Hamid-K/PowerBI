using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml
{
	// Token: 0x02002247 RID: 8775
	[GeneratedCode("DomGen", "2.0")]
	internal class Shadow : OpenXmlLeafElement
	{
		// Token: 0x170039E1 RID: 14817
		// (get) Token: 0x0600E17C RID: 57724 RVA: 0x002C0C98 File Offset: 0x002BEE98
		public override string LocalName
		{
			get
			{
				return "shadow";
			}
		}

		// Token: 0x170039E2 RID: 14818
		// (get) Token: 0x0600E17D RID: 57725 RVA: 0x00243C87 File Offset: 0x00241E87
		internal override byte NamespaceId
		{
			get
			{
				return 26;
			}
		}

		// Token: 0x170039E3 RID: 14819
		// (get) Token: 0x0600E17E RID: 57726 RVA: 0x002C0C9F File Offset: 0x002BEE9F
		internal override int ElementTypeId
		{
			get
			{
				return 12511;
			}
		}

		// Token: 0x0600E17F RID: 57727 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170039E4 RID: 14820
		// (get) Token: 0x0600E180 RID: 57728 RVA: 0x002C0CA6 File Offset: 0x002BEEA6
		internal override string[] AttributeTagNames
		{
			get
			{
				return Shadow.attributeTagNames;
			}
		}

		// Token: 0x170039E5 RID: 14821
		// (get) Token: 0x0600E181 RID: 57729 RVA: 0x002C0CAD File Offset: 0x002BEEAD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Shadow.attributeNamespaceIds;
			}
		}

		// Token: 0x170039E6 RID: 14822
		// (get) Token: 0x0600E182 RID: 57730 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600E183 RID: 57731 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public StringValue Id
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170039E7 RID: 14823
		// (get) Token: 0x0600E184 RID: 57732 RVA: 0x002BDACB File Offset: 0x002BBCCB
		// (set) Token: 0x0600E185 RID: 57733 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "on")]
		public TrueFalseValue On
		{
			get
			{
				return (TrueFalseValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170039E8 RID: 14824
		// (get) Token: 0x0600E186 RID: 57734 RVA: 0x002C0CB4 File Offset: 0x002BEEB4
		// (set) Token: 0x0600E187 RID: 57735 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "type")]
		public EnumValue<ShadowValues> Type
		{
			get
			{
				return (EnumValue<ShadowValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170039E9 RID: 14825
		// (get) Token: 0x0600E188 RID: 57736 RVA: 0x002BD49F File Offset: 0x002BB69F
		// (set) Token: 0x0600E189 RID: 57737 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "obscured")]
		public TrueFalseValue Obscured
		{
			get
			{
				return (TrueFalseValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x170039EA RID: 14826
		// (get) Token: 0x0600E18A RID: 57738 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600E18B RID: 57739 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "color")]
		public StringValue Color
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

		// Token: 0x170039EB RID: 14827
		// (get) Token: 0x0600E18C RID: 57740 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600E18D RID: 57741 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "opacity")]
		public StringValue Opacity
		{
			get
			{
				return (StringValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x170039EC RID: 14828
		// (get) Token: 0x0600E18E RID: 57742 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600E18F RID: 57743 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "offset")]
		public StringValue Offset
		{
			get
			{
				return (StringValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x170039ED RID: 14829
		// (get) Token: 0x0600E190 RID: 57744 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600E191 RID: 57745 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "color2")]
		public StringValue Color2
		{
			get
			{
				return (StringValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x170039EE RID: 14830
		// (get) Token: 0x0600E192 RID: 57746 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600E193 RID: 57747 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "offset2")]
		public StringValue Offset2
		{
			get
			{
				return (StringValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x170039EF RID: 14831
		// (get) Token: 0x0600E194 RID: 57748 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600E195 RID: 57749 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "origin")]
		public StringValue Origin
		{
			get
			{
				return (StringValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x170039F0 RID: 14832
		// (get) Token: 0x0600E196 RID: 57750 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600E197 RID: 57751 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "matrix")]
		public StringValue Matrix
		{
			get
			{
				return (StringValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x0600E199 RID: 57753 RVA: 0x002C0CC4 File Offset: 0x002BEEC4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "on" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<ShadowValues>();
			}
			if (namespaceId == 0 && "obscured" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "color" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "opacity" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "offset" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "color2" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "offset2" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "origin" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "matrix" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600E19A RID: 57754 RVA: 0x002C0DCB File Offset: 0x002BEFCB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Shadow>(deep);
		}

		// Token: 0x0600E19B RID: 57755 RVA: 0x002C0DD4 File Offset: 0x002BEFD4
		// Note: this type is marked as 'beforefieldinit'.
		static Shadow()
		{
			byte[] array = new byte[11];
			Shadow.attributeNamespaceIds = array;
		}

		// Token: 0x04006EA9 RID: 28329
		private const string tagName = "shadow";

		// Token: 0x04006EAA RID: 28330
		private const byte tagNsId = 26;

		// Token: 0x04006EAB RID: 28331
		internal const int ElementTypeIdConst = 12511;

		// Token: 0x04006EAC RID: 28332
		private static string[] attributeTagNames = new string[]
		{
			"id", "on", "type", "obscured", "color", "opacity", "offset", "color2", "offset2", "origin",
			"matrix"
		};

		// Token: 0x04006EAD RID: 28333
		private static byte[] attributeNamespaceIds;
	}
}
