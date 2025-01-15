using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B5C RID: 11100
	[GeneratedCode("DomGen", "2.0")]
	internal class SortByTuple : TuplesType
	{
		// Token: 0x170078C4 RID: 30916
		// (get) Token: 0x06016D1F RID: 93471 RVA: 0x0032F76D File Offset: 0x0032D96D
		public override string LocalName
		{
			get
			{
				return "sortByTuple";
			}
		}

		// Token: 0x170078C5 RID: 30917
		// (get) Token: 0x06016D20 RID: 93472 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170078C6 RID: 30918
		// (get) Token: 0x06016D21 RID: 93473 RVA: 0x0032F774 File Offset: 0x0032D974
		internal override int ElementTypeId
		{
			get
			{
				return 11098;
			}
		}

		// Token: 0x06016D22 RID: 93474 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016D23 RID: 93475 RVA: 0x0032F741 File Offset: 0x0032D941
		public SortByTuple()
		{
		}

		// Token: 0x06016D24 RID: 93476 RVA: 0x0032F749 File Offset: 0x0032D949
		public SortByTuple(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016D25 RID: 93477 RVA: 0x0032F752 File Offset: 0x0032D952
		public SortByTuple(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016D26 RID: 93478 RVA: 0x0032F75B File Offset: 0x0032D95B
		public SortByTuple(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016D27 RID: 93479 RVA: 0x0032F77B File Offset: 0x0032D97B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SortByTuple>(deep);
		}

		// Token: 0x04009A07 RID: 39431
		private const string tagName = "sortByTuple";

		// Token: 0x04009A08 RID: 39432
		private const byte tagNsId = 22;

		// Token: 0x04009A09 RID: 39433
		internal const int ElementTypeIdConst = 11098;
	}
}
