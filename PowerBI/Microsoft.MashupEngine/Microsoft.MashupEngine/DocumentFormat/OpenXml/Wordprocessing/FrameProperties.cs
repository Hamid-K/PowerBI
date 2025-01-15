using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E24 RID: 11812
	[GeneratedCode("DomGen", "2.0")]
	internal class FrameProperties : OpenXmlLeafElement
	{
		// Token: 0x17008908 RID: 35080
		// (get) Token: 0x060190D0 RID: 102608 RVA: 0x00345A49 File Offset: 0x00343C49
		public override string LocalName
		{
			get
			{
				return "framePr";
			}
		}

		// Token: 0x17008909 RID: 35081
		// (get) Token: 0x060190D1 RID: 102609 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700890A RID: 35082
		// (get) Token: 0x060190D2 RID: 102610 RVA: 0x00345A50 File Offset: 0x00343C50
		internal override int ElementTypeId
		{
			get
			{
				return 11496;
			}
		}

		// Token: 0x060190D3 RID: 102611 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700890B RID: 35083
		// (get) Token: 0x060190D4 RID: 102612 RVA: 0x00345A57 File Offset: 0x00343C57
		internal override string[] AttributeTagNames
		{
			get
			{
				return FrameProperties.attributeTagNames;
			}
		}

		// Token: 0x1700890C RID: 35084
		// (get) Token: 0x060190D5 RID: 102613 RVA: 0x00345A5E File Offset: 0x00343C5E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FrameProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x1700890D RID: 35085
		// (get) Token: 0x060190D6 RID: 102614 RVA: 0x00345A65 File Offset: 0x00343C65
		// (set) Token: 0x060190D7 RID: 102615 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "dropCap")]
		public EnumValue<DropCapLocationValues> DropCap
		{
			get
			{
				return (EnumValue<DropCapLocationValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700890E RID: 35086
		// (get) Token: 0x060190D8 RID: 102616 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x060190D9 RID: 102617 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "lines")]
		public Int32Value Lines
		{
			get
			{
				return (Int32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x1700890F RID: 35087
		// (get) Token: 0x060190DA RID: 102618 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x060190DB RID: 102619 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "w")]
		public StringValue Width
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

		// Token: 0x17008910 RID: 35088
		// (get) Token: 0x060190DC RID: 102620 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x060190DD RID: 102621 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "h")]
		public UInt32Value Height
		{
			get
			{
				return (UInt32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17008911 RID: 35089
		// (get) Token: 0x060190DE RID: 102622 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x060190DF RID: 102623 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(23, "vSpace")]
		public StringValue VerticalSpace
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

		// Token: 0x17008912 RID: 35090
		// (get) Token: 0x060190E0 RID: 102624 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x060190E1 RID: 102625 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(23, "hSpace")]
		public StringValue HorizontalSpace
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

		// Token: 0x17008913 RID: 35091
		// (get) Token: 0x060190E2 RID: 102626 RVA: 0x00345A74 File Offset: 0x00343C74
		// (set) Token: 0x060190E3 RID: 102627 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(23, "wrap")]
		public EnumValue<TextWrappingValues> Wrap
		{
			get
			{
				return (EnumValue<TextWrappingValues>)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17008914 RID: 35092
		// (get) Token: 0x060190E4 RID: 102628 RVA: 0x00345A83 File Offset: 0x00343C83
		// (set) Token: 0x060190E5 RID: 102629 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(23, "hAnchor")]
		public EnumValue<HorizontalAnchorValues> HorizontalPosition
		{
			get
			{
				return (EnumValue<HorizontalAnchorValues>)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17008915 RID: 35093
		// (get) Token: 0x060190E6 RID: 102630 RVA: 0x00345A92 File Offset: 0x00343C92
		// (set) Token: 0x060190E7 RID: 102631 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(23, "vAnchor")]
		public EnumValue<VerticalAnchorValues> VerticalPosition
		{
			get
			{
				return (EnumValue<VerticalAnchorValues>)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17008916 RID: 35094
		// (get) Token: 0x060190E8 RID: 102632 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x060190E9 RID: 102633 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(23, "x")]
		public StringValue X
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

		// Token: 0x17008917 RID: 35095
		// (get) Token: 0x060190EA RID: 102634 RVA: 0x00345AA1 File Offset: 0x00343CA1
		// (set) Token: 0x060190EB RID: 102635 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(23, "xAlign")]
		public EnumValue<HorizontalAlignmentValues> XAlign
		{
			get
			{
				return (EnumValue<HorizontalAlignmentValues>)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17008918 RID: 35096
		// (get) Token: 0x060190EC RID: 102636 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x060190ED RID: 102637 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(23, "y")]
		public StringValue Y
		{
			get
			{
				return (StringValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x17008919 RID: 35097
		// (get) Token: 0x060190EE RID: 102638 RVA: 0x00345AB1 File Offset: 0x00343CB1
		// (set) Token: 0x060190EF RID: 102639 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(23, "yAlign")]
		public EnumValue<VerticalAlignmentValues> YAlign
		{
			get
			{
				return (EnumValue<VerticalAlignmentValues>)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x1700891A RID: 35098
		// (get) Token: 0x060190F0 RID: 102640 RVA: 0x00345AC1 File Offset: 0x00343CC1
		// (set) Token: 0x060190F1 RID: 102641 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(23, "hRule")]
		public EnumValue<HeightRuleValues> HeightType
		{
			get
			{
				return (EnumValue<HeightRuleValues>)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x1700891B RID: 35099
		// (get) Token: 0x060190F2 RID: 102642 RVA: 0x00345AD1 File Offset: 0x00343CD1
		// (set) Token: 0x060190F3 RID: 102643 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(23, "anchorLock")]
		public OnOffValue AnchorLock
		{
			get
			{
				return (OnOffValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x060190F5 RID: 102645 RVA: 0x00345AE4 File Offset: 0x00343CE4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "dropCap" == name)
			{
				return new EnumValue<DropCapLocationValues>();
			}
			if (23 == namespaceId && "lines" == name)
			{
				return new Int32Value();
			}
			if (23 == namespaceId && "w" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "h" == name)
			{
				return new UInt32Value();
			}
			if (23 == namespaceId && "vSpace" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "hSpace" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "wrap" == name)
			{
				return new EnumValue<TextWrappingValues>();
			}
			if (23 == namespaceId && "hAnchor" == name)
			{
				return new EnumValue<HorizontalAnchorValues>();
			}
			if (23 == namespaceId && "vAnchor" == name)
			{
				return new EnumValue<VerticalAnchorValues>();
			}
			if (23 == namespaceId && "x" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "xAlign" == name)
			{
				return new EnumValue<HorizontalAlignmentValues>();
			}
			if (23 == namespaceId && "y" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "yAlign" == name)
			{
				return new EnumValue<VerticalAlignmentValues>();
			}
			if (23 == namespaceId && "hRule" == name)
			{
				return new EnumValue<HeightRuleValues>();
			}
			if (23 == namespaceId && "anchorLock" == name)
			{
				return new OnOffValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060190F6 RID: 102646 RVA: 0x00345C61 File Offset: 0x00343E61
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FrameProperties>(deep);
		}

		// Token: 0x0400A6D7 RID: 42711
		private const string tagName = "framePr";

		// Token: 0x0400A6D8 RID: 42712
		private const byte tagNsId = 23;

		// Token: 0x0400A6D9 RID: 42713
		internal const int ElementTypeIdConst = 11496;

		// Token: 0x0400A6DA RID: 42714
		private static string[] attributeTagNames = new string[]
		{
			"dropCap", "lines", "w", "h", "vSpace", "hSpace", "wrap", "hAnchor", "vAnchor", "x",
			"xAlign", "y", "yAlign", "hRule", "anchorLock"
		};

		// Token: 0x0400A6DB RID: 42715
		private static byte[] attributeNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23
		};
	}
}
