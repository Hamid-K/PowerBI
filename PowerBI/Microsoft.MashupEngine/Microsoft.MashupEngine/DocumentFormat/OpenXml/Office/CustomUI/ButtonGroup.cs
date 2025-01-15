using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x02002285 RID: 8837
	[ChildElementInfo(typeof(UnsizedToggleButton))]
	[ChildElementInfo(typeof(UnsizedControlClone))]
	[ChildElementInfo(typeof(UnsizedGallery))]
	[ChildElementInfo(typeof(UnsizedMenu))]
	[ChildElementInfo(typeof(UnsizedDynamicMenu))]
	[ChildElementInfo(typeof(UnsizedSplitButton))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(UnsizedButton))]
	internal class ButtonGroup : OpenXmlCompositeElement
	{
		// Token: 0x17003FDF RID: 16351
		// (get) Token: 0x0600EDDC RID: 60892 RVA: 0x002CE751 File Offset: 0x002CC951
		public override string LocalName
		{
			get
			{
				return "buttonGroup";
			}
		}

		// Token: 0x17003FE0 RID: 16352
		// (get) Token: 0x0600EDDD RID: 60893 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x17003FE1 RID: 16353
		// (get) Token: 0x0600EDDE RID: 60894 RVA: 0x002CE758 File Offset: 0x002CC958
		internal override int ElementTypeId
		{
			get
			{
				return 12596;
			}
		}

		// Token: 0x0600EDDF RID: 60895 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003FE2 RID: 16354
		// (get) Token: 0x0600EDE0 RID: 60896 RVA: 0x002CE75F File Offset: 0x002CC95F
		internal override string[] AttributeTagNames
		{
			get
			{
				return ButtonGroup.attributeTagNames;
			}
		}

		// Token: 0x17003FE3 RID: 16355
		// (get) Token: 0x0600EDE1 RID: 60897 RVA: 0x002CE766 File Offset: 0x002CC966
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ButtonGroup.attributeNamespaceIds;
			}
		}

		// Token: 0x17003FE4 RID: 16356
		// (get) Token: 0x0600EDE2 RID: 60898 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600EDE3 RID: 60899 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17003FE5 RID: 16357
		// (get) Token: 0x0600EDE4 RID: 60900 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600EDE5 RID: 60901 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "idQ")]
		public StringValue IdQ
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

		// Token: 0x17003FE6 RID: 16358
		// (get) Token: 0x0600EDE6 RID: 60902 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x0600EDE7 RID: 60903 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
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

		// Token: 0x17003FE7 RID: 16359
		// (get) Token: 0x0600EDE8 RID: 60904 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600EDE9 RID: 60905 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x17003FE8 RID: 16360
		// (get) Token: 0x0600EDEA RID: 60906 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600EDEB RID: 60907 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
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

		// Token: 0x17003FE9 RID: 16361
		// (get) Token: 0x0600EDEC RID: 60908 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600EDED RID: 60909 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
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

		// Token: 0x17003FEA RID: 16362
		// (get) Token: 0x0600EDEE RID: 60910 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600EDEF RID: 60911 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQ
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

		// Token: 0x17003FEB RID: 16363
		// (get) Token: 0x0600EDF0 RID: 60912 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600EDF1 RID: 60913 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQ
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

		// Token: 0x0600EDF2 RID: 60914 RVA: 0x00293ECF File Offset: 0x002920CF
		public ButtonGroup()
		{
		}

		// Token: 0x0600EDF3 RID: 60915 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ButtonGroup(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EDF4 RID: 60916 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ButtonGroup(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EDF5 RID: 60917 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ButtonGroup(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600EDF6 RID: 60918 RVA: 0x002CE770 File Offset: 0x002CC970
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (34 == namespaceId && "control" == name)
			{
				return new UnsizedControlClone();
			}
			if (34 == namespaceId && "button" == name)
			{
				return new UnsizedButton();
			}
			if (34 == namespaceId && "toggleButton" == name)
			{
				return new UnsizedToggleButton();
			}
			if (34 == namespaceId && "gallery" == name)
			{
				return new UnsizedGallery();
			}
			if (34 == namespaceId && "menu" == name)
			{
				return new UnsizedMenu();
			}
			if (34 == namespaceId && "dynamicMenu" == name)
			{
				return new UnsizedDynamicMenu();
			}
			if (34 == namespaceId && "splitButton" == name)
			{
				return new UnsizedSplitButton();
			}
			return null;
		}

		// Token: 0x0600EDF7 RID: 60919 RVA: 0x002CE828 File Offset: 0x002CCA28
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
			if (namespaceId == 0 && "visible" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getVisible" == name)
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
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600EDF8 RID: 60920 RVA: 0x002CE8ED File Offset: 0x002CCAED
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ButtonGroup>(deep);
		}

		// Token: 0x0600EDF9 RID: 60921 RVA: 0x002CE8F8 File Offset: 0x002CCAF8
		// Note: this type is marked as 'beforefieldinit'.
		static ButtonGroup()
		{
			byte[] array = new byte[8];
			ButtonGroup.attributeNamespaceIds = array;
		}

		// Token: 0x04006FF8 RID: 28664
		private const string tagName = "buttonGroup";

		// Token: 0x04006FF9 RID: 28665
		private const byte tagNsId = 34;

		// Token: 0x04006FFA RID: 28666
		internal const int ElementTypeIdConst = 12596;

		// Token: 0x04006FFB RID: 28667
		private static string[] attributeTagNames = new string[] { "id", "idQ", "visible", "getVisible", "insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ" };

		// Token: 0x04006FFC RID: 28668
		private static byte[] attributeNamespaceIds;
	}
}
