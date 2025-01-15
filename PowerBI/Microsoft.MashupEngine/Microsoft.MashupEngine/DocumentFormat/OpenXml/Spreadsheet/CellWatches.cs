using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C9F RID: 11423
	[ChildElementInfo(typeof(CellWatch))]
	[GeneratedCode("DomGen", "2.0")]
	internal class CellWatches : OpenXmlCompositeElement
	{
		// Token: 0x17008436 RID: 33846
		// (get) Token: 0x06018659 RID: 99929 RVA: 0x0034140F File Offset: 0x0033F60F
		public override string LocalName
		{
			get
			{
				return "cellWatches";
			}
		}

		// Token: 0x17008437 RID: 33847
		// (get) Token: 0x0601865A RID: 99930 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008438 RID: 33848
		// (get) Token: 0x0601865B RID: 99931 RVA: 0x00341416 File Offset: 0x0033F616
		internal override int ElementTypeId
		{
			get
			{
				return 11403;
			}
		}

		// Token: 0x0601865C RID: 99932 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601865D RID: 99933 RVA: 0x00293ECF File Offset: 0x002920CF
		public CellWatches()
		{
		}

		// Token: 0x0601865E RID: 99934 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CellWatches(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601865F RID: 99935 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CellWatches(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018660 RID: 99936 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CellWatches(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018661 RID: 99937 RVA: 0x0034141D File Offset: 0x0033F61D
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "cellWatch" == name)
			{
				return new CellWatch();
			}
			return null;
		}

		// Token: 0x06018662 RID: 99938 RVA: 0x00341438 File Offset: 0x0033F638
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CellWatches>(deep);
		}

		// Token: 0x0400A012 RID: 40978
		private const string tagName = "cellWatches";

		// Token: 0x0400A013 RID: 40979
		private const byte tagNsId = 22;

		// Token: 0x0400A014 RID: 40980
		internal const int ElementTypeIdConst = 11403;
	}
}
