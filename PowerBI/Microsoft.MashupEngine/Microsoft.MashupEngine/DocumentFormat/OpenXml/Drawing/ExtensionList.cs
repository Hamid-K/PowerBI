using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200276C RID: 10092
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Extension))]
	internal class ExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x17006137 RID: 24887
		// (get) Token: 0x0601375E RID: 79710 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x17006138 RID: 24888
		// (get) Token: 0x0601375F RID: 79711 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006139 RID: 24889
		// (get) Token: 0x06013760 RID: 79712 RVA: 0x0030756F File Offset: 0x0030576F
		internal override int ElementTypeId
		{
			get
			{
				return 10127;
			}
		}

		// Token: 0x06013761 RID: 79713 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013762 RID: 79714 RVA: 0x00293ECF File Offset: 0x002920CF
		public ExtensionList()
		{
		}

		// Token: 0x06013763 RID: 79715 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013764 RID: 79716 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013765 RID: 79717 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013766 RID: 79718 RVA: 0x002DF2C5 File Offset: 0x002DD4C5
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "ext" == name)
			{
				return new Extension();
			}
			return null;
		}

		// Token: 0x06013767 RID: 79719 RVA: 0x00307576 File Offset: 0x00305776
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ExtensionList>(deep);
		}

		// Token: 0x0400864F RID: 34383
		private const string tagName = "extLst";

		// Token: 0x04008650 RID: 34384
		private const byte tagNsId = 10;

		// Token: 0x04008651 RID: 34385
		internal const int ElementTypeIdConst = 10127;
	}
}
