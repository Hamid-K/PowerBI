using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x0200242B RID: 9259
	[ChildElementInfo(typeof(SlicerStyleElement), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class SlicerStyleElements : OpenXmlCompositeElement
	{
		// Token: 0x17004FB3 RID: 20403
		// (get) Token: 0x0601103A RID: 69690 RVA: 0x002E9B14 File Offset: 0x002E7D14
		public override string LocalName
		{
			get
			{
				return "slicerStyleElements";
			}
		}

		// Token: 0x17004FB4 RID: 20404
		// (get) Token: 0x0601103B RID: 69691 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004FB5 RID: 20405
		// (get) Token: 0x0601103C RID: 69692 RVA: 0x002E9B1B File Offset: 0x002E7D1B
		internal override int ElementTypeId
		{
			get
			{
				return 12983;
			}
		}

		// Token: 0x0601103D RID: 69693 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0601103E RID: 69694 RVA: 0x00293ECF File Offset: 0x002920CF
		public SlicerStyleElements()
		{
		}

		// Token: 0x0601103F RID: 69695 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SlicerStyleElements(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011040 RID: 69696 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SlicerStyleElements(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011041 RID: 69697 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SlicerStyleElements(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011042 RID: 69698 RVA: 0x002E9B22 File Offset: 0x002E7D22
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "slicerStyleElement" == name)
			{
				return new SlicerStyleElement();
			}
			return null;
		}

		// Token: 0x06011043 RID: 69699 RVA: 0x002E9B3D File Offset: 0x002E7D3D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SlicerStyleElements>(deep);
		}

		// Token: 0x04007748 RID: 30536
		private const string tagName = "slicerStyleElements";

		// Token: 0x04007749 RID: 30537
		private const byte tagNsId = 53;

		// Token: 0x0400774A RID: 30538
		internal const int ElementTypeIdConst = 12983;
	}
}
