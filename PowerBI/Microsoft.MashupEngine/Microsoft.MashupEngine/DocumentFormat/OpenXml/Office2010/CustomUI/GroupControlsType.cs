using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022FE RID: 8958
	[ChildElementInfo(typeof(BackstageEditBox), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BackstageGroupButton), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BackstageDropDown), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(RadioGroup), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BackstageCheckBox), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Hyperlink), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BackstageLabelControl), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(GroupBox), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(LayoutContainer), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ImageControl), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(BackstageComboBox), FileFormatVersions.Office2010)]
	internal abstract class GroupControlsType : OpenXmlCompositeElement
	{
		// Token: 0x0600FDCF RID: 64975 RVA: 0x002DC994 File Offset: 0x002DAB94
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "button" == name)
			{
				return new BackstageGroupButton();
			}
			if (57 == namespaceId && "checkBox" == name)
			{
				return new BackstageCheckBox();
			}
			if (57 == namespaceId && "editBox" == name)
			{
				return new BackstageEditBox();
			}
			if (57 == namespaceId && "dropDown" == name)
			{
				return new BackstageDropDown();
			}
			if (57 == namespaceId && "radioGroup" == name)
			{
				return new RadioGroup();
			}
			if (57 == namespaceId && "comboBox" == name)
			{
				return new BackstageComboBox();
			}
			if (57 == namespaceId && "hyperlink" == name)
			{
				return new Hyperlink();
			}
			if (57 == namespaceId && "labelControl" == name)
			{
				return new BackstageLabelControl();
			}
			if (57 == namespaceId && "groupBox" == name)
			{
				return new GroupBox();
			}
			if (57 == namespaceId && "layoutContainer" == name)
			{
				return new LayoutContainer();
			}
			if (57 == namespaceId && "imageControl" == name)
			{
				return new ImageControl();
			}
			return null;
		}

		// Token: 0x0600FDD0 RID: 64976 RVA: 0x00293ECF File Offset: 0x002920CF
		protected GroupControlsType()
		{
		}

		// Token: 0x0600FDD1 RID: 64977 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected GroupControlsType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FDD2 RID: 64978 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected GroupControlsType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FDD3 RID: 64979 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected GroupControlsType(string outerXml)
			: base(outerXml)
		{
		}
	}
}
