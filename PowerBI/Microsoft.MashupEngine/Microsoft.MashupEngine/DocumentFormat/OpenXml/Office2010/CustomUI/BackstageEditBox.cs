using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022D9 RID: 8921
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class BackstageEditBox : OpenXmlLeafElement
	{
		// Token: 0x17004535 RID: 17717
		// (get) Token: 0x0600F92B RID: 63787 RVA: 0x002CC299 File Offset: 0x002CA499
		public override string LocalName
		{
			get
			{
				return "editBox";
			}
		}

		// Token: 0x17004536 RID: 17718
		// (get) Token: 0x0600F92C RID: 63788 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x17004537 RID: 17719
		// (get) Token: 0x0600F92D RID: 63789 RVA: 0x002D8834 File Offset: 0x002D6A34
		internal override int ElementTypeId
		{
			get
			{
				return 13066;
			}
		}

		// Token: 0x0600F92E RID: 63790 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004538 RID: 17720
		// (get) Token: 0x0600F92F RID: 63791 RVA: 0x002D883B File Offset: 0x002D6A3B
		internal override string[] AttributeTagNames
		{
			get
			{
				return BackstageEditBox.attributeTagNames;
			}
		}

		// Token: 0x17004539 RID: 17721
		// (get) Token: 0x0600F930 RID: 63792 RVA: 0x002D8842 File Offset: 0x002D6A42
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BackstageEditBox.attributeNamespaceIds;
			}
		}

		// Token: 0x1700453A RID: 17722
		// (get) Token: 0x0600F931 RID: 63793 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600F932 RID: 63794 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x1700453B RID: 17723
		// (get) Token: 0x0600F933 RID: 63795 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F934 RID: 63796 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
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

		// Token: 0x1700453C RID: 17724
		// (get) Token: 0x0600F935 RID: 63797 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600F936 RID: 63798 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x1700453D RID: 17725
		// (get) Token: 0x0600F937 RID: 63799 RVA: 0x002D8849 File Offset: 0x002D6A49
		// (set) Token: 0x0600F938 RID: 63800 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "alignLabel")]
		public EnumValue<ExpandValues> AlignLabel
		{
			get
			{
				return (EnumValue<ExpandValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x1700453E RID: 17726
		// (get) Token: 0x0600F939 RID: 63801 RVA: 0x002D8858 File Offset: 0x002D6A58
		// (set) Token: 0x0600F93A RID: 63802 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "expand")]
		public EnumValue<ExpandValues> Expand
		{
			get
			{
				return (EnumValue<ExpandValues>)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x1700453F RID: 17727
		// (get) Token: 0x0600F93B RID: 63803 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x0600F93C RID: 63804 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
		{
			get
			{
				return (BooleanValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17004540 RID: 17728
		// (get) Token: 0x0600F93D RID: 63805 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600F93E RID: 63806 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x17004541 RID: 17729
		// (get) Token: 0x0600F93F RID: 63807 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600F940 RID: 63808 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x17004542 RID: 17730
		// (get) Token: 0x0600F941 RID: 63809 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600F942 RID: 63810 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x17004543 RID: 17731
		// (get) Token: 0x0600F943 RID: 63811 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x0600F944 RID: 63812 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
		{
			get
			{
				return (BooleanValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17004544 RID: 17732
		// (get) Token: 0x0600F945 RID: 63813 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600F946 RID: 63814 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x17004545 RID: 17733
		// (get) Token: 0x0600F947 RID: 63815 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600F948 RID: 63816 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
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

		// Token: 0x17004546 RID: 17734
		// (get) Token: 0x0600F949 RID: 63817 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600F94A RID: 63818 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
		{
			get
			{
				return (StringValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x17004547 RID: 17735
		// (get) Token: 0x0600F94B RID: 63819 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600F94C RID: 63820 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "getText")]
		public StringValue GetText
		{
			get
			{
				return (StringValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x17004548 RID: 17736
		// (get) Token: 0x0600F94D RID: 63821 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600F94E RID: 63822 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "onChange")]
		public StringValue OnChange
		{
			get
			{
				return (StringValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x17004549 RID: 17737
		// (get) Token: 0x0600F94F RID: 63823 RVA: 0x002D8867 File Offset: 0x002D6A67
		// (set) Token: 0x0600F950 RID: 63824 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "maxLength")]
		public IntegerValue MaxLength
		{
			get
			{
				return (IntegerValue)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x1700454A RID: 17738
		// (get) Token: 0x0600F951 RID: 63825 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600F952 RID: 63826 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "sizeString")]
		public StringValue SizeString
		{
			get
			{
				return (StringValue)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x0600F954 RID: 63828 RVA: 0x002D8878 File Offset: 0x002D6A78
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "idQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "tag" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "alignLabel" == name)
			{
				return new EnumValue<ExpandValues>();
			}
			if (namespaceId == 0 && "expand" == name)
			{
				return new EnumValue<ExpandValues>();
			}
			if (namespaceId == 0 && "enabled" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getEnabled" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "label" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getLabel" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "visible" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getVisible" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "keytip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getKeytip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getText" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "onChange" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "maxLength" == name)
			{
				return new IntegerValue();
			}
			if (namespaceId == 0 && "sizeString" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600F955 RID: 63829 RVA: 0x002D8A03 File Offset: 0x002D6C03
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BackstageEditBox>(deep);
		}

		// Token: 0x0600F956 RID: 63830 RVA: 0x002D8A0C File Offset: 0x002D6C0C
		// Note: this type is marked as 'beforefieldinit'.
		static BackstageEditBox()
		{
			byte[] array = new byte[17];
			BackstageEditBox.attributeNamespaceIds = array;
		}

		// Token: 0x0400716A RID: 29034
		private const string tagName = "editBox";

		// Token: 0x0400716B RID: 29035
		private const byte tagNsId = 57;

		// Token: 0x0400716C RID: 29036
		internal const int ElementTypeIdConst = 13066;

		// Token: 0x0400716D RID: 29037
		private static string[] attributeTagNames = new string[]
		{
			"id", "idQ", "tag", "alignLabel", "expand", "enabled", "getEnabled", "label", "getLabel", "visible",
			"getVisible", "keytip", "getKeytip", "getText", "onChange", "maxLength", "sizeString"
		};

		// Token: 0x0400716E RID: 29038
		private static byte[] attributeNamespaceIds;
	}
}
