using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002435 RID: 9269
	[ChildElementInfo(typeof(ExtensionList), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ListItem), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ListItems : OpenXmlCompositeElement
	{
		// Token: 0x17005011 RID: 20497
		// (get) Token: 0x06011106 RID: 69894 RVA: 0x002EA39B File Offset: 0x002E859B
		public override string LocalName
		{
			get
			{
				return "itemLst";
			}
		}

		// Token: 0x17005012 RID: 20498
		// (get) Token: 0x06011107 RID: 69895 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17005013 RID: 20499
		// (get) Token: 0x06011108 RID: 69896 RVA: 0x002EA3A2 File Offset: 0x002E85A2
		internal override int ElementTypeId
		{
			get
			{
				return 12993;
			}
		}

		// Token: 0x06011109 RID: 69897 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0601110A RID: 69898 RVA: 0x00293ECF File Offset: 0x002920CF
		public ListItems()
		{
		}

		// Token: 0x0601110B RID: 69899 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ListItems(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601110C RID: 69900 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ListItems(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601110D RID: 69901 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ListItems(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601110E RID: 69902 RVA: 0x002EA3A9 File Offset: 0x002E85A9
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "item" == name)
			{
				return new ListItem();
			}
			if (53 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x0601110F RID: 69903 RVA: 0x002EA3DC File Offset: 0x002E85DC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ListItems>(deep);
		}

		// Token: 0x04007780 RID: 30592
		private const string tagName = "itemLst";

		// Token: 0x04007781 RID: 30593
		private const byte tagNsId = 53;

		// Token: 0x04007782 RID: 30594
		internal const int ElementTypeIdConst = 12993;
	}
}
