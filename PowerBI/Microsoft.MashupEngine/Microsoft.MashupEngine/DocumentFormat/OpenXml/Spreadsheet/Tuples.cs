using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B5B RID: 11099
	[GeneratedCode("DomGen", "2.0")]
	internal class Tuples : TuplesType
	{
		// Token: 0x170078C1 RID: 30913
		// (get) Token: 0x06016D16 RID: 93462 RVA: 0x0032F733 File Offset: 0x0032D933
		public override string LocalName
		{
			get
			{
				return "tpls";
			}
		}

		// Token: 0x170078C2 RID: 30914
		// (get) Token: 0x06016D17 RID: 93463 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170078C3 RID: 30915
		// (get) Token: 0x06016D18 RID: 93464 RVA: 0x0032F73A File Offset: 0x0032D93A
		internal override int ElementTypeId
		{
			get
			{
				return 11081;
			}
		}

		// Token: 0x06016D19 RID: 93465 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016D1A RID: 93466 RVA: 0x0032F741 File Offset: 0x0032D941
		public Tuples()
		{
		}

		// Token: 0x06016D1B RID: 93467 RVA: 0x0032F749 File Offset: 0x0032D949
		public Tuples(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016D1C RID: 93468 RVA: 0x0032F752 File Offset: 0x0032D952
		public Tuples(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016D1D RID: 93469 RVA: 0x0032F75B File Offset: 0x0032D95B
		public Tuples(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016D1E RID: 93470 RVA: 0x0032F764 File Offset: 0x0032D964
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Tuples>(deep);
		}

		// Token: 0x04009A04 RID: 39428
		private const string tagName = "tpls";

		// Token: 0x04009A05 RID: 39429
		private const byte tagNsId = 22;

		// Token: 0x04009A06 RID: 39430
		internal const int ElementTypeIdConst = 11081;
	}
}
