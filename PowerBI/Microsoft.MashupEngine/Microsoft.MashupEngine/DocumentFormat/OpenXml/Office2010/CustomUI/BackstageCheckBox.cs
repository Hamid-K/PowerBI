using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022D8 RID: 8920
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class BackstageCheckBox : OpenXmlLeafElement
	{
		// Token: 0x1700451C RID: 17692
		// (get) Token: 0x0600F8F9 RID: 63737 RVA: 0x002C8F4A File Offset: 0x002C714A
		public override string LocalName
		{
			get
			{
				return "checkBox";
			}
		}

		// Token: 0x1700451D RID: 17693
		// (get) Token: 0x0600F8FA RID: 63738 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x1700451E RID: 17694
		// (get) Token: 0x0600F8FB RID: 63739 RVA: 0x002D8572 File Offset: 0x002D6772
		internal override int ElementTypeId
		{
			get
			{
				return 13065;
			}
		}

		// Token: 0x0600F8FC RID: 63740 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700451F RID: 17695
		// (get) Token: 0x0600F8FD RID: 63741 RVA: 0x002D8579 File Offset: 0x002D6779
		internal override string[] AttributeTagNames
		{
			get
			{
				return BackstageCheckBox.attributeTagNames;
			}
		}

		// Token: 0x17004520 RID: 17696
		// (get) Token: 0x0600F8FE RID: 63742 RVA: 0x002D8580 File Offset: 0x002D6780
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BackstageCheckBox.attributeNamespaceIds;
			}
		}

		// Token: 0x17004521 RID: 17697
		// (get) Token: 0x0600F8FF RID: 63743 RVA: 0x002D8267 File Offset: 0x002D6467
		// (set) Token: 0x0600F900 RID: 63744 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "expand")]
		public EnumValue<ExpandValues> Expand
		{
			get
			{
				return (EnumValue<ExpandValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004522 RID: 17698
		// (get) Token: 0x0600F901 RID: 63745 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F902 RID: 63746 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "description")]
		public StringValue Description
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

		// Token: 0x17004523 RID: 17699
		// (get) Token: 0x0600F903 RID: 63747 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600F904 RID: 63748 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "getDescription")]
		public StringValue GetDescription
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

		// Token: 0x17004524 RID: 17700
		// (get) Token: 0x0600F905 RID: 63749 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600F906 RID: 63750 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "screentip")]
		public StringValue Screentip
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

		// Token: 0x17004525 RID: 17701
		// (get) Token: 0x0600F907 RID: 63751 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600F908 RID: 63752 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "getScreentip")]
		public StringValue GetScreentip
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

		// Token: 0x17004526 RID: 17702
		// (get) Token: 0x0600F909 RID: 63753 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600F90A RID: 63754 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "supertip")]
		public StringValue Supertip
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

		// Token: 0x17004527 RID: 17703
		// (get) Token: 0x0600F90B RID: 63755 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600F90C RID: 63756 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "getSupertip")]
		public StringValue GetSupertip
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

		// Token: 0x17004528 RID: 17704
		// (get) Token: 0x0600F90D RID: 63757 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600F90E RID: 63758 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x17004529 RID: 17705
		// (get) Token: 0x0600F90F RID: 63759 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600F910 RID: 63760 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
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

		// Token: 0x1700452A RID: 17706
		// (get) Token: 0x0600F911 RID: 63761 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600F912 RID: 63762 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x1700452B RID: 17707
		// (get) Token: 0x0600F913 RID: 63763 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600F914 RID: 63764 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "onAction")]
		public StringValue OnAction
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

		// Token: 0x1700452C RID: 17708
		// (get) Token: 0x0600F915 RID: 63765 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600F916 RID: 63766 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "getPressed")]
		public StringValue GetPressed
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

		// Token: 0x1700452D RID: 17709
		// (get) Token: 0x0600F917 RID: 63767 RVA: 0x002CE196 File Offset: 0x002CC396
		// (set) Token: 0x0600F918 RID: 63768 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
		{
			get
			{
				return (BooleanValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x1700452E RID: 17710
		// (get) Token: 0x0600F919 RID: 63769 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600F91A RID: 63770 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x1700452F RID: 17711
		// (get) Token: 0x0600F91B RID: 63771 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600F91C RID: 63772 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x17004530 RID: 17712
		// (get) Token: 0x0600F91D RID: 63773 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600F91E RID: 63774 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
		{
			get
			{
				return (StringValue)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x17004531 RID: 17713
		// (get) Token: 0x0600F91F RID: 63775 RVA: 0x002C930A File Offset: 0x002C750A
		// (set) Token: 0x0600F920 RID: 63776 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
		{
			get
			{
				return (BooleanValue)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x17004532 RID: 17714
		// (get) Token: 0x0600F921 RID: 63777 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600F922 RID: 63778 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
		{
			get
			{
				return (StringValue)base.Attributes[17];
			}
			set
			{
				base.Attributes[17] = value;
			}
		}

		// Token: 0x17004533 RID: 17715
		// (get) Token: 0x0600F923 RID: 63779 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600F924 RID: 63780 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
		{
			get
			{
				return (StringValue)base.Attributes[18];
			}
			set
			{
				base.Attributes[18] = value;
			}
		}

		// Token: 0x17004534 RID: 17716
		// (get) Token: 0x0600F925 RID: 63781 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600F926 RID: 63782 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
		{
			get
			{
				return (StringValue)base.Attributes[19];
			}
			set
			{
				base.Attributes[19] = value;
			}
		}

		// Token: 0x0600F928 RID: 63784 RVA: 0x002D8588 File Offset: 0x002D6788
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "expand" == name)
			{
				return new EnumValue<ExpandValues>();
			}
			if (namespaceId == 0 && "description" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getDescription" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "screentip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getScreentip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "supertip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getSupertip" == name)
			{
				return new StringValue();
			}
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
			if (namespaceId == 0 && "onAction" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getPressed" == name)
			{
				return new StringValue();
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
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600F929 RID: 63785 RVA: 0x002D8755 File Offset: 0x002D6955
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BackstageCheckBox>(deep);
		}

		// Token: 0x0600F92A RID: 63786 RVA: 0x002D8760 File Offset: 0x002D6960
		// Note: this type is marked as 'beforefieldinit'.
		static BackstageCheckBox()
		{
			byte[] array = new byte[20];
			BackstageCheckBox.attributeNamespaceIds = array;
		}

		// Token: 0x04007165 RID: 29029
		private const string tagName = "checkBox";

		// Token: 0x04007166 RID: 29030
		private const byte tagNsId = 57;

		// Token: 0x04007167 RID: 29031
		internal const int ElementTypeIdConst = 13065;

		// Token: 0x04007168 RID: 29032
		private static string[] attributeTagNames = new string[]
		{
			"expand", "description", "getDescription", "screentip", "getScreentip", "supertip", "getSupertip", "id", "idQ", "tag",
			"onAction", "getPressed", "enabled", "getEnabled", "label", "getLabel", "visible", "getVisible", "keytip", "getKeytip"
		};

		// Token: 0x04007169 RID: 29033
		private static byte[] attributeNamespaceIds;
	}
}
