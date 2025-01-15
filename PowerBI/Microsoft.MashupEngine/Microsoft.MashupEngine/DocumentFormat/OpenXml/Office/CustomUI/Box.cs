using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x02002284 RID: 8836
	[ChildElementInfo(typeof(CheckBox))]
	[ChildElementInfo(typeof(Menu))]
	[ChildElementInfo(typeof(DynamicMenu))]
	[ChildElementInfo(typeof(SplitButton))]
	[ChildElementInfo(typeof(Box))]
	[ChildElementInfo(typeof(ButtonGroup))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(EditBox))]
	[ChildElementInfo(typeof(ComboBox))]
	[ChildElementInfo(typeof(DropDown))]
	[ChildElementInfo(typeof(Gallery))]
	[ChildElementInfo(typeof(ControlClone))]
	[ChildElementInfo(typeof(TextLabel))]
	[ChildElementInfo(typeof(Button))]
	[ChildElementInfo(typeof(ToggleButton))]
	internal class Box : OpenXmlCompositeElement
	{
		// Token: 0x17003FD1 RID: 16337
		// (get) Token: 0x0600EDBC RID: 60860 RVA: 0x002CE46E File Offset: 0x002CC66E
		public override string LocalName
		{
			get
			{
				return "box";
			}
		}

		// Token: 0x17003FD2 RID: 16338
		// (get) Token: 0x0600EDBD RID: 60861 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x17003FD3 RID: 16339
		// (get) Token: 0x0600EDBE RID: 60862 RVA: 0x002CE475 File Offset: 0x002CC675
		internal override int ElementTypeId
		{
			get
			{
				return 12595;
			}
		}

		// Token: 0x0600EDBF RID: 60863 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003FD4 RID: 16340
		// (get) Token: 0x0600EDC0 RID: 60864 RVA: 0x002CE47C File Offset: 0x002CC67C
		internal override string[] AttributeTagNames
		{
			get
			{
				return Box.attributeTagNames;
			}
		}

		// Token: 0x17003FD5 RID: 16341
		// (get) Token: 0x0600EDC1 RID: 60865 RVA: 0x002CE483 File Offset: 0x002CC683
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Box.attributeNamespaceIds;
			}
		}

		// Token: 0x17003FD6 RID: 16342
		// (get) Token: 0x0600EDC2 RID: 60866 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600EDC3 RID: 60867 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17003FD7 RID: 16343
		// (get) Token: 0x0600EDC4 RID: 60868 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600EDC5 RID: 60869 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17003FD8 RID: 16344
		// (get) Token: 0x0600EDC6 RID: 60870 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x0600EDC7 RID: 60871 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17003FD9 RID: 16345
		// (get) Token: 0x0600EDC8 RID: 60872 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600EDC9 RID: 60873 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17003FDA RID: 16346
		// (get) Token: 0x0600EDCA RID: 60874 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600EDCB RID: 60875 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x17003FDB RID: 16347
		// (get) Token: 0x0600EDCC RID: 60876 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600EDCD RID: 60877 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x17003FDC RID: 16348
		// (get) Token: 0x0600EDCE RID: 60878 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600EDCF RID: 60879 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x17003FDD RID: 16349
		// (get) Token: 0x0600EDD0 RID: 60880 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600EDD1 RID: 60881 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x17003FDE RID: 16350
		// (get) Token: 0x0600EDD2 RID: 60882 RVA: 0x002CE48A File Offset: 0x002CC68A
		// (set) Token: 0x0600EDD3 RID: 60883 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "boxStyle")]
		public EnumValue<BoxStyleValues> BoxStyle
		{
			get
			{
				return (EnumValue<BoxStyleValues>)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x0600EDD4 RID: 60884 RVA: 0x00293ECF File Offset: 0x002920CF
		public Box()
		{
		}

		// Token: 0x0600EDD5 RID: 60885 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Box(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EDD6 RID: 60886 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Box(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EDD7 RID: 60887 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Box(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600EDD8 RID: 60888 RVA: 0x002CE49C File Offset: 0x002CC69C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (34 == namespaceId && "control" == name)
			{
				return new ControlClone();
			}
			if (34 == namespaceId && "labelControl" == name)
			{
				return new TextLabel();
			}
			if (34 == namespaceId && "button" == name)
			{
				return new Button();
			}
			if (34 == namespaceId && "toggleButton" == name)
			{
				return new ToggleButton();
			}
			if (34 == namespaceId && "checkBox" == name)
			{
				return new CheckBox();
			}
			if (34 == namespaceId && "editBox" == name)
			{
				return new EditBox();
			}
			if (34 == namespaceId && "comboBox" == name)
			{
				return new ComboBox();
			}
			if (34 == namespaceId && "dropDown" == name)
			{
				return new DropDown();
			}
			if (34 == namespaceId && "gallery" == name)
			{
				return new Gallery();
			}
			if (34 == namespaceId && "menu" == name)
			{
				return new Menu();
			}
			if (34 == namespaceId && "dynamicMenu" == name)
			{
				return new DynamicMenu();
			}
			if (34 == namespaceId && "splitButton" == name)
			{
				return new SplitButton();
			}
			if (34 == namespaceId && "box" == name)
			{
				return new Box();
			}
			if (34 == namespaceId && "buttonGroup" == name)
			{
				return new ButtonGroup();
			}
			return null;
		}

		// Token: 0x0600EDD9 RID: 60889 RVA: 0x002CE5FC File Offset: 0x002CC7FC
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
			if (namespaceId == 0 && "boxStyle" == name)
			{
				return new EnumValue<BoxStyleValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600EDDA RID: 60890 RVA: 0x002CE6D7 File Offset: 0x002CC8D7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Box>(deep);
		}

		// Token: 0x0600EDDB RID: 60891 RVA: 0x002CE6E0 File Offset: 0x002CC8E0
		// Note: this type is marked as 'beforefieldinit'.
		static Box()
		{
			byte[] array = new byte[9];
			Box.attributeNamespaceIds = array;
		}

		// Token: 0x04006FF3 RID: 28659
		private const string tagName = "box";

		// Token: 0x04006FF4 RID: 28660
		private const byte tagNsId = 34;

		// Token: 0x04006FF5 RID: 28661
		internal const int ElementTypeIdConst = 12595;

		// Token: 0x04006FF6 RID: 28662
		private static string[] attributeTagNames = new string[] { "id", "idQ", "visible", "getVisible", "insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ", "boxStyle" };

		// Token: 0x04006FF7 RID: 28663
		private static byte[] attributeNamespaceIds;
	}
}
