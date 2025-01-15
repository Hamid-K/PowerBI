using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002420 RID: 9248
	[ChildElementInfo(typeof(PivotEdit), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class PivotEdits : OpenXmlCompositeElement
	{
		// Token: 0x17004F60 RID: 20320
		// (get) Token: 0x06010F76 RID: 69494 RVA: 0x002E92F3 File Offset: 0x002E74F3
		public override string LocalName
		{
			get
			{
				return "pivotEdits";
			}
		}

		// Token: 0x17004F61 RID: 20321
		// (get) Token: 0x06010F77 RID: 69495 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004F62 RID: 20322
		// (get) Token: 0x06010F78 RID: 69496 RVA: 0x002E92FA File Offset: 0x002E74FA
		internal override int ElementTypeId
		{
			get
			{
				return 12972;
			}
		}

		// Token: 0x06010F79 RID: 69497 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010F7A RID: 69498 RVA: 0x00293ECF File Offset: 0x002920CF
		public PivotEdits()
		{
		}

		// Token: 0x06010F7B RID: 69499 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PivotEdits(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010F7C RID: 69500 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PivotEdits(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010F7D RID: 69501 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PivotEdits(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010F7E RID: 69502 RVA: 0x002E9301 File Offset: 0x002E7501
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "pivotEdit" == name)
			{
				return new PivotEdit();
			}
			return null;
		}

		// Token: 0x06010F7F RID: 69503 RVA: 0x002E931C File Offset: 0x002E751C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotEdits>(deep);
		}

		// Token: 0x04007715 RID: 30485
		private const string tagName = "pivotEdits";

		// Token: 0x04007716 RID: 30486
		private const byte tagNsId = 53;

		// Token: 0x04007717 RID: 30487
		internal const int ElementTypeIdConst = 12972;
	}
}
