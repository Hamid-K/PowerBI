using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200282A RID: 10282
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(TabStop))]
	internal class TabStopList : OpenXmlCompositeElement
	{
		// Token: 0x170065E3 RID: 26083
		// (get) Token: 0x06014231 RID: 82481 RVA: 0x0030FAF9 File Offset: 0x0030DCF9
		public override string LocalName
		{
			get
			{
				return "tabLst";
			}
		}

		// Token: 0x170065E4 RID: 26084
		// (get) Token: 0x06014232 RID: 82482 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170065E5 RID: 26085
		// (get) Token: 0x06014233 RID: 82483 RVA: 0x0030FB00 File Offset: 0x0030DD00
		internal override int ElementTypeId
		{
			get
			{
				return 10314;
			}
		}

		// Token: 0x06014234 RID: 82484 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014235 RID: 82485 RVA: 0x00293ECF File Offset: 0x002920CF
		public TabStopList()
		{
		}

		// Token: 0x06014236 RID: 82486 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TabStopList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014237 RID: 82487 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TabStopList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014238 RID: 82488 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TabStopList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014239 RID: 82489 RVA: 0x0030FB07 File Offset: 0x0030DD07
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "tab" == name)
			{
				return new TabStop();
			}
			return null;
		}

		// Token: 0x0601423A RID: 82490 RVA: 0x0030FB22 File Offset: 0x0030DD22
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TabStopList>(deep);
		}

		// Token: 0x04008930 RID: 35120
		private const string tagName = "tabLst";

		// Token: 0x04008931 RID: 35121
		private const byte tagNsId = 10;

		// Token: 0x04008932 RID: 35122
		internal const int ElementTypeIdConst = 10314;
	}
}
