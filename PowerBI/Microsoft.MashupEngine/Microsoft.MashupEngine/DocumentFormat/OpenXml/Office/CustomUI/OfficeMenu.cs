using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x02002295 RID: 8853
	[ChildElementInfo(typeof(UnsizedButton))]
	[ChildElementInfo(typeof(UnsizedGallery))]
	[ChildElementInfo(typeof(UnsizedToggleButton))]
	[ChildElementInfo(typeof(MenuSeparator))]
	[ChildElementInfo(typeof(SplitButtonWithTitle))]
	[ChildElementInfo(typeof(MenuWithTitle))]
	[ChildElementInfo(typeof(UnsizedDynamicMenu))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(UnsizedControlClone))]
	[ChildElementInfo(typeof(CheckBox))]
	internal class OfficeMenu : OpenXmlCompositeElement
	{
		// Token: 0x170040CA RID: 16586
		// (get) Token: 0x0600EFD7 RID: 61399 RVA: 0x002D03FB File Offset: 0x002CE5FB
		public override string LocalName
		{
			get
			{
				return "officeMenu";
			}
		}

		// Token: 0x170040CB RID: 16587
		// (get) Token: 0x0600EFD8 RID: 61400 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x170040CC RID: 16588
		// (get) Token: 0x0600EFD9 RID: 61401 RVA: 0x002D0402 File Offset: 0x002CE602
		internal override int ElementTypeId
		{
			get
			{
				return 12611;
			}
		}

		// Token: 0x0600EFDA RID: 61402 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600EFDB RID: 61403 RVA: 0x00293ECF File Offset: 0x002920CF
		public OfficeMenu()
		{
		}

		// Token: 0x0600EFDC RID: 61404 RVA: 0x00293ED7 File Offset: 0x002920D7
		public OfficeMenu(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EFDD RID: 61405 RVA: 0x00293EE0 File Offset: 0x002920E0
		public OfficeMenu(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EFDE RID: 61406 RVA: 0x00293EE9 File Offset: 0x002920E9
		public OfficeMenu(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600EFDF RID: 61407 RVA: 0x002D040C File Offset: 0x002CE60C
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
			if (34 == namespaceId && "checkBox" == name)
			{
				return new CheckBox();
			}
			if (34 == namespaceId && "gallery" == name)
			{
				return new UnsizedGallery();
			}
			if (34 == namespaceId && "toggleButton" == name)
			{
				return new UnsizedToggleButton();
			}
			if (34 == namespaceId && "menuSeparator" == name)
			{
				return new MenuSeparator();
			}
			if (34 == namespaceId && "splitButton" == name)
			{
				return new SplitButtonWithTitle();
			}
			if (34 == namespaceId && "menu" == name)
			{
				return new MenuWithTitle();
			}
			if (34 == namespaceId && "dynamicMenu" == name)
			{
				return new UnsizedDynamicMenu();
			}
			return null;
		}

		// Token: 0x0600EFE0 RID: 61408 RVA: 0x002D04F2 File Offset: 0x002CE6F2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OfficeMenu>(deep);
		}

		// Token: 0x04007041 RID: 28737
		private const string tagName = "officeMenu";

		// Token: 0x04007042 RID: 28738
		private const byte tagNsId = 34;

		// Token: 0x04007043 RID: 28739
		internal const int ElementTypeIdConst = 12611;
	}
}
