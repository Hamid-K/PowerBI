using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x02002274 RID: 8820
	[ChildElementInfo(typeof(VisibleToggleButton))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(UnsizedMenu))]
	[ChildElementInfo(typeof(VisibleButton))]
	internal class UnsizedSplitButton : OpenXmlCompositeElement
	{
		// Token: 0x17003DA1 RID: 15777
		// (get) Token: 0x0600E938 RID: 59704 RVA: 0x002C9F5F File Offset: 0x002C815F
		public override string LocalName
		{
			get
			{
				return "splitButton";
			}
		}

		// Token: 0x17003DA2 RID: 15778
		// (get) Token: 0x0600E939 RID: 59705 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x17003DA3 RID: 15779
		// (get) Token: 0x0600E93A RID: 59706 RVA: 0x002C9F66 File Offset: 0x002C8166
		internal override int ElementTypeId
		{
			get
			{
				return 12579;
			}
		}

		// Token: 0x0600E93B RID: 59707 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003DA4 RID: 15780
		// (get) Token: 0x0600E93C RID: 59708 RVA: 0x002C9F6D File Offset: 0x002C816D
		internal override string[] AttributeTagNames
		{
			get
			{
				return UnsizedSplitButton.attributeTagNames;
			}
		}

		// Token: 0x17003DA5 RID: 15781
		// (get) Token: 0x0600E93D RID: 59709 RVA: 0x002C9F74 File Offset: 0x002C8174
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return UnsizedSplitButton.attributeNamespaceIds;
			}
		}

		// Token: 0x17003DA6 RID: 15782
		// (get) Token: 0x0600E93E RID: 59710 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x0600E93F RID: 59711 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17003DA7 RID: 15783
		// (get) Token: 0x0600E940 RID: 59712 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600E941 RID: 59713 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x17003DA8 RID: 15784
		// (get) Token: 0x0600E942 RID: 59714 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600E943 RID: 59715 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x17003DA9 RID: 15785
		// (get) Token: 0x0600E944 RID: 59716 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600E945 RID: 59717 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "idQ")]
		public StringValue IdQ
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

		// Token: 0x17003DAA RID: 15786
		// (get) Token: 0x0600E946 RID: 59718 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600E947 RID: 59719 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x17003DAB RID: 15787
		// (get) Token: 0x0600E948 RID: 59720 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600E949 RID: 59721 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x17003DAC RID: 15788
		// (get) Token: 0x0600E94A RID: 59722 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600E94B RID: 59723 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
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

		// Token: 0x17003DAD RID: 15789
		// (get) Token: 0x0600E94C RID: 59724 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600E94D RID: 59725 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
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

		// Token: 0x17003DAE RID: 15790
		// (get) Token: 0x0600E94E RID: 59726 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600E94F RID: 59727 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQ
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

		// Token: 0x17003DAF RID: 15791
		// (get) Token: 0x0600E950 RID: 59728 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600E951 RID: 59729 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQ
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

		// Token: 0x17003DB0 RID: 15792
		// (get) Token: 0x0600E952 RID: 59730 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x0600E953 RID: 59731 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
		{
			get
			{
				return (BooleanValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17003DB1 RID: 15793
		// (get) Token: 0x0600E954 RID: 59732 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600E955 RID: 59733 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x17003DB2 RID: 15794
		// (get) Token: 0x0600E956 RID: 59734 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600E957 RID: 59735 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
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

		// Token: 0x17003DB3 RID: 15795
		// (get) Token: 0x0600E958 RID: 59736 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600E959 RID: 59737 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
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

		// Token: 0x17003DB4 RID: 15796
		// (get) Token: 0x0600E95A RID: 59738 RVA: 0x002C9F8A File Offset: 0x002C818A
		// (set) Token: 0x0600E95B RID: 59739 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "showLabel")]
		public BooleanValue ShowLabel
		{
			get
			{
				return (BooleanValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x17003DB5 RID: 15797
		// (get) Token: 0x0600E95C RID: 59740 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600E95D RID: 59741 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "getShowLabel")]
		public StringValue GetShowLabel
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

		// Token: 0x0600E95E RID: 59742 RVA: 0x00293ECF File Offset: 0x002920CF
		public UnsizedSplitButton()
		{
		}

		// Token: 0x0600E95F RID: 59743 RVA: 0x00293ED7 File Offset: 0x002920D7
		public UnsizedSplitButton(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E960 RID: 59744 RVA: 0x00293EE0 File Offset: 0x002920E0
		public UnsizedSplitButton(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E961 RID: 59745 RVA: 0x00293EE9 File Offset: 0x002920E9
		public UnsizedSplitButton(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600E962 RID: 59746 RVA: 0x002C9F9C File Offset: 0x002C819C
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

		// Token: 0x0600E963 RID: 59747 RVA: 0x002C9FF4 File Offset: 0x002C81F4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
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

		// Token: 0x0600E964 RID: 59748 RVA: 0x002CA169 File Offset: 0x002C8369
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UnsizedSplitButton>(deep);
		}

		// Token: 0x0600E965 RID: 59749 RVA: 0x002CA174 File Offset: 0x002C8374
		// Note: this type is marked as 'beforefieldinit'.
		static UnsizedSplitButton()
		{
			byte[] array = new byte[16];
			UnsizedSplitButton.attributeNamespaceIds = array;
		}

		// Token: 0x04006FA3 RID: 28579
		private const string tagName = "splitButton";

		// Token: 0x04006FA4 RID: 28580
		private const byte tagNsId = 34;

		// Token: 0x04006FA5 RID: 28581
		internal const int ElementTypeIdConst = 12579;

		// Token: 0x04006FA6 RID: 28582
		private static string[] attributeTagNames = new string[]
		{
			"enabled", "getEnabled", "id", "idQ", "idMso", "tag", "insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ",
			"visible", "getVisible", "keytip", "getKeytip", "showLabel", "getShowLabel"
		};

		// Token: 0x04006FA7 RID: 28583
		private static byte[] attributeNamespaceIds;
	}
}
