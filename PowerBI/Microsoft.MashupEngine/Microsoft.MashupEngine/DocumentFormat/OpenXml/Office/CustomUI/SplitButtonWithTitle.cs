using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x02002277 RID: 8823
	[ChildElementInfo(typeof(VisibleToggleButton))]
	[ChildElementInfo(typeof(MenuWithTitle))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(VisibleButton))]
	internal class SplitButtonWithTitle : OpenXmlCompositeElement
	{
		// Token: 0x17003DFD RID: 15869
		// (get) Token: 0x0600E9F8 RID: 59896 RVA: 0x002C9F5F File Offset: 0x002C815F
		public override string LocalName
		{
			get
			{
				return "splitButton";
			}
		}

		// Token: 0x17003DFE RID: 15870
		// (get) Token: 0x0600E9F9 RID: 59897 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x17003DFF RID: 15871
		// (get) Token: 0x0600E9FA RID: 59898 RVA: 0x002CAB57 File Offset: 0x002C8D57
		internal override int ElementTypeId
		{
			get
			{
				return 12582;
			}
		}

		// Token: 0x0600E9FB RID: 59899 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003E00 RID: 15872
		// (get) Token: 0x0600E9FC RID: 59900 RVA: 0x002CAB5E File Offset: 0x002C8D5E
		internal override string[] AttributeTagNames
		{
			get
			{
				return SplitButtonWithTitle.attributeTagNames;
			}
		}

		// Token: 0x17003E01 RID: 15873
		// (get) Token: 0x0600E9FD RID: 59901 RVA: 0x002CAB65 File Offset: 0x002C8D65
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SplitButtonWithTitle.attributeNamespaceIds;
			}
		}

		// Token: 0x17003E02 RID: 15874
		// (get) Token: 0x0600E9FE RID: 59902 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x0600E9FF RID: 59903 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17003E03 RID: 15875
		// (get) Token: 0x0600EA00 RID: 59904 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600EA01 RID: 59905 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17003E04 RID: 15876
		// (get) Token: 0x0600EA02 RID: 59906 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600EA03 RID: 59907 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17003E05 RID: 15877
		// (get) Token: 0x0600EA04 RID: 59908 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600EA05 RID: 59909 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17003E06 RID: 15878
		// (get) Token: 0x0600EA06 RID: 59910 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600EA07 RID: 59911 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x17003E07 RID: 15879
		// (get) Token: 0x0600EA08 RID: 59912 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600EA09 RID: 59913 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x17003E08 RID: 15880
		// (get) Token: 0x0600EA0A RID: 59914 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600EA0B RID: 59915 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x17003E09 RID: 15881
		// (get) Token: 0x0600EA0C RID: 59916 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600EA0D RID: 59917 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x17003E0A RID: 15882
		// (get) Token: 0x0600EA0E RID: 59918 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600EA0F RID: 59919 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x17003E0B RID: 15883
		// (get) Token: 0x0600EA10 RID: 59920 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600EA11 RID: 59921 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x17003E0C RID: 15884
		// (get) Token: 0x0600EA12 RID: 59922 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x0600EA13 RID: 59923 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x17003E0D RID: 15885
		// (get) Token: 0x0600EA14 RID: 59924 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600EA15 RID: 59925 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x17003E0E RID: 15886
		// (get) Token: 0x0600EA16 RID: 59926 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600EA17 RID: 59927 RVA: 0x002BDB7D File Offset: 0x002BBD7D
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

		// Token: 0x17003E0F RID: 15887
		// (get) Token: 0x0600EA18 RID: 59928 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600EA19 RID: 59929 RVA: 0x002BE209 File Offset: 0x002BC409
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

		// Token: 0x17003E10 RID: 15888
		// (get) Token: 0x0600EA1A RID: 59930 RVA: 0x002C9F8A File Offset: 0x002C818A
		// (set) Token: 0x0600EA1B RID: 59931 RVA: 0x002BE225 File Offset: 0x002BC425
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

		// Token: 0x17003E11 RID: 15889
		// (get) Token: 0x0600EA1C RID: 59932 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600EA1D RID: 59933 RVA: 0x002BE241 File Offset: 0x002BC441
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

		// Token: 0x0600EA1E RID: 59934 RVA: 0x00293ECF File Offset: 0x002920CF
		public SplitButtonWithTitle()
		{
		}

		// Token: 0x0600EA1F RID: 59935 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SplitButtonWithTitle(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EA20 RID: 59936 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SplitButtonWithTitle(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EA21 RID: 59937 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SplitButtonWithTitle(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600EA22 RID: 59938 RVA: 0x002CAB6C File Offset: 0x002C8D6C
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
				return new MenuWithTitle();
			}
			return null;
		}

		// Token: 0x0600EA23 RID: 59939 RVA: 0x002CABC4 File Offset: 0x002C8DC4
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

		// Token: 0x0600EA24 RID: 59940 RVA: 0x002CAD39 File Offset: 0x002C8F39
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SplitButtonWithTitle>(deep);
		}

		// Token: 0x0600EA25 RID: 59941 RVA: 0x002CAD44 File Offset: 0x002C8F44
		// Note: this type is marked as 'beforefieldinit'.
		static SplitButtonWithTitle()
		{
			byte[] array = new byte[16];
			SplitButtonWithTitle.attributeNamespaceIds = array;
		}

		// Token: 0x04006FB2 RID: 28594
		private const string tagName = "splitButton";

		// Token: 0x04006FB3 RID: 28595
		private const byte tagNsId = 34;

		// Token: 0x04006FB4 RID: 28596
		internal const int ElementTypeIdConst = 12582;

		// Token: 0x04006FB5 RID: 28597
		private static string[] attributeTagNames = new string[]
		{
			"enabled", "getEnabled", "id", "idQ", "idMso", "tag", "insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ",
			"visible", "getVisible", "keytip", "getKeytip", "showLabel", "getShowLabel"
		};

		// Token: 0x04006FB6 RID: 28598
		private static byte[] attributeNamespaceIds;
	}
}
