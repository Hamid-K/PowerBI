using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x02002283 RID: 8835
	[ChildElementInfo(typeof(VisibleToggleButton))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(VisibleButton))]
	[ChildElementInfo(typeof(UnsizedMenu))]
	internal class SplitButton : OpenXmlCompositeElement
	{
		// Token: 0x17003FBA RID: 16314
		// (get) Token: 0x0600ED8A RID: 60810 RVA: 0x002C9F5F File Offset: 0x002C815F
		public override string LocalName
		{
			get
			{
				return "splitButton";
			}
		}

		// Token: 0x17003FBB RID: 16315
		// (get) Token: 0x0600ED8B RID: 60811 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x17003FBC RID: 16316
		// (get) Token: 0x0600ED8C RID: 60812 RVA: 0x002CE181 File Offset: 0x002CC381
		internal override int ElementTypeId
		{
			get
			{
				return 12594;
			}
		}

		// Token: 0x0600ED8D RID: 60813 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003FBD RID: 16317
		// (get) Token: 0x0600ED8E RID: 60814 RVA: 0x002CE188 File Offset: 0x002CC388
		internal override string[] AttributeTagNames
		{
			get
			{
				return SplitButton.attributeTagNames;
			}
		}

		// Token: 0x17003FBE RID: 16318
		// (get) Token: 0x0600ED8F RID: 60815 RVA: 0x002CE18F File Offset: 0x002CC38F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SplitButton.attributeNamespaceIds;
			}
		}

		// Token: 0x17003FBF RID: 16319
		// (get) Token: 0x0600ED90 RID: 60816 RVA: 0x002CB2F7 File Offset: 0x002C94F7
		// (set) Token: 0x0600ED91 RID: 60817 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "size")]
		public EnumValue<SizeValues> Size
		{
			get
			{
				return (EnumValue<SizeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17003FC0 RID: 16320
		// (get) Token: 0x0600ED92 RID: 60818 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600ED93 RID: 60819 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "getSize")]
		public StringValue GetSize
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

		// Token: 0x17003FC1 RID: 16321
		// (get) Token: 0x0600ED94 RID: 60820 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x0600ED95 RID: 60821 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17003FC2 RID: 16322
		// (get) Token: 0x0600ED96 RID: 60822 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600ED97 RID: 60823 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x17003FC3 RID: 16323
		// (get) Token: 0x0600ED98 RID: 60824 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600ED99 RID: 60825 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x17003FC4 RID: 16324
		// (get) Token: 0x0600ED9A RID: 60826 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600ED9B RID: 60827 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "idQ")]
		public StringValue IdQ
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

		// Token: 0x17003FC5 RID: 16325
		// (get) Token: 0x0600ED9C RID: 60828 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600ED9D RID: 60829 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x17003FC6 RID: 16326
		// (get) Token: 0x0600ED9E RID: 60830 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600ED9F RID: 60831 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x17003FC7 RID: 16327
		// (get) Token: 0x0600EDA0 RID: 60832 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600EDA1 RID: 60833 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
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

		// Token: 0x17003FC8 RID: 16328
		// (get) Token: 0x0600EDA2 RID: 60834 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600EDA3 RID: 60835 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
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

		// Token: 0x17003FC9 RID: 16329
		// (get) Token: 0x0600EDA4 RID: 60836 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600EDA5 RID: 60837 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQ
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

		// Token: 0x17003FCA RID: 16330
		// (get) Token: 0x0600EDA6 RID: 60838 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600EDA7 RID: 60839 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQ
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

		// Token: 0x17003FCB RID: 16331
		// (get) Token: 0x0600EDA8 RID: 60840 RVA: 0x002CE196 File Offset: 0x002CC396
		// (set) Token: 0x0600EDA9 RID: 60841 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
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

		// Token: 0x17003FCC RID: 16332
		// (get) Token: 0x0600EDAA RID: 60842 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600EDAB RID: 60843 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x17003FCD RID: 16333
		// (get) Token: 0x0600EDAC RID: 60844 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600EDAD RID: 60845 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
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

		// Token: 0x17003FCE RID: 16334
		// (get) Token: 0x0600EDAE RID: 60846 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600EDAF RID: 60847 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
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

		// Token: 0x17003FCF RID: 16335
		// (get) Token: 0x0600EDB0 RID: 60848 RVA: 0x002C930A File Offset: 0x002C750A
		// (set) Token: 0x0600EDB1 RID: 60849 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "showLabel")]
		public BooleanValue ShowLabel
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

		// Token: 0x17003FD0 RID: 16336
		// (get) Token: 0x0600EDB2 RID: 60850 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600EDB3 RID: 60851 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "getShowLabel")]
		public StringValue GetShowLabel
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

		// Token: 0x0600EDB4 RID: 60852 RVA: 0x00293ECF File Offset: 0x002920CF
		public SplitButton()
		{
		}

		// Token: 0x0600EDB5 RID: 60853 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SplitButton(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EDB6 RID: 60854 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SplitButton(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EDB7 RID: 60855 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SplitButton(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600EDB8 RID: 60856 RVA: 0x002CE1A8 File Offset: 0x002CC3A8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (34 == namespaceId && "button" == name)
			{
				return new VisibleButton();
			}
			if (34 == namespaceId && "toggleButton" == name)
			{
				return new VisibleToggleButton();
			}
			if (34 == namespaceId && "menu" == name)
			{
				return new UnsizedMenu();
			}
			return null;
		}

		// Token: 0x0600EDB9 RID: 60857 RVA: 0x002CE200 File Offset: 0x002CC400
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "size" == name)
			{
				return new EnumValue<SizeValues>();
			}
			if (namespaceId == 0 && "getSize" == name)
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
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "idQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "idMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "tag" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertAfterMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertBeforeMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertAfterQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertBeforeQ" == name)
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
			if (namespaceId == 0 && "showLabel" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getShowLabel" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600EDBA RID: 60858 RVA: 0x002CE3A1 File Offset: 0x002CC5A1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SplitButton>(deep);
		}

		// Token: 0x0600EDBB RID: 60859 RVA: 0x002CE3AC File Offset: 0x002CC5AC
		// Note: this type is marked as 'beforefieldinit'.
		static SplitButton()
		{
			byte[] array = new byte[18];
			SplitButton.attributeNamespaceIds = array;
		}

		// Token: 0x04006FEE RID: 28654
		private const string tagName = "splitButton";

		// Token: 0x04006FEF RID: 28655
		private const byte tagNsId = 34;

		// Token: 0x04006FF0 RID: 28656
		internal const int ElementTypeIdConst = 12594;

		// Token: 0x04006FF1 RID: 28657
		private static string[] attributeTagNames = new string[]
		{
			"size", "getSize", "enabled", "getEnabled", "id", "idQ", "idMso", "tag", "insertAfterMso", "insertBeforeMso",
			"insertAfterQ", "insertBeforeQ", "visible", "getVisible", "keytip", "getKeytip", "showLabel", "getShowLabel"
		};

		// Token: 0x04006FF2 RID: 28658
		private static byte[] attributeNamespaceIds;
	}
}
