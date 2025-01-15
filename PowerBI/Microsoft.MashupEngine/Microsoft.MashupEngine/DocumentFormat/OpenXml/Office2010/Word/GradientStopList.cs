using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024C5 RID: 9413
	[ChildElementInfo(typeof(GradientStop), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class GradientStopList : OpenXmlCompositeElement
	{
		// Token: 0x170052D1 RID: 21201
		// (get) Token: 0x06011747 RID: 71495 RVA: 0x002EE9D0 File Offset: 0x002ECBD0
		public override string LocalName
		{
			get
			{
				return "gsLst";
			}
		}

		// Token: 0x170052D2 RID: 21202
		// (get) Token: 0x06011748 RID: 71496 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x170052D3 RID: 21203
		// (get) Token: 0x06011749 RID: 71497 RVA: 0x002EE9D7 File Offset: 0x002ECBD7
		internal override int ElementTypeId
		{
			get
			{
				return 12885;
			}
		}

		// Token: 0x0601174A RID: 71498 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0601174B RID: 71499 RVA: 0x00293ECF File Offset: 0x002920CF
		public GradientStopList()
		{
		}

		// Token: 0x0601174C RID: 71500 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GradientStopList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601174D RID: 71501 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GradientStopList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601174E RID: 71502 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GradientStopList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601174F RID: 71503 RVA: 0x002EE9DE File Offset: 0x002ECBDE
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "gs" == name)
			{
				return new GradientStop();
			}
			return null;
		}

		// Token: 0x06011750 RID: 71504 RVA: 0x002EE9F9 File Offset: 0x002ECBF9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GradientStopList>(deep);
		}

		// Token: 0x040079E7 RID: 31207
		private const string tagName = "gsLst";

		// Token: 0x040079E8 RID: 31208
		private const byte tagNsId = 52;

		// Token: 0x040079E9 RID: 31209
		internal const int ElementTypeIdConst = 12885;
	}
}
