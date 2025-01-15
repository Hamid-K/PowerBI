using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AA0 RID: 10912
	[ChildElementInfo(typeof(Control))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ControlList : OpenXmlCompositeElement
	{
		// Token: 0x17007436 RID: 29750
		// (get) Token: 0x060162B0 RID: 90800 RVA: 0x00327339 File Offset: 0x00325539
		public override string LocalName
		{
			get
			{
				return "controls";
			}
		}

		// Token: 0x17007437 RID: 29751
		// (get) Token: 0x060162B1 RID: 90801 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007438 RID: 29752
		// (get) Token: 0x060162B2 RID: 90802 RVA: 0x00327340 File Offset: 0x00325540
		internal override int ElementTypeId
		{
			get
			{
				return 12325;
			}
		}

		// Token: 0x060162B3 RID: 90803 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060162B4 RID: 90804 RVA: 0x00293ECF File Offset: 0x002920CF
		public ControlList()
		{
		}

		// Token: 0x060162B5 RID: 90805 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ControlList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060162B6 RID: 90806 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ControlList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060162B7 RID: 90807 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ControlList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060162B8 RID: 90808 RVA: 0x00327347 File Offset: 0x00325547
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "control" == name)
			{
				return new Control();
			}
			return null;
		}

		// Token: 0x060162B9 RID: 90809 RVA: 0x00327362 File Offset: 0x00325562
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ControlList>(deep);
		}

		// Token: 0x04009686 RID: 38534
		private const string tagName = "controls";

		// Token: 0x04009687 RID: 38535
		private const byte tagNsId = 24;

		// Token: 0x04009688 RID: 38536
		internal const int ElementTypeIdConst = 12325;
	}
}
