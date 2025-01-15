using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002413 RID: 9235
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(TupleSetHeader), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class TupleSetHeaders : OpenXmlCompositeElement
	{
		// Token: 0x17004F02 RID: 20226
		// (get) Token: 0x06010E9B RID: 69275 RVA: 0x002E897B File Offset: 0x002E6B7B
		public override string LocalName
		{
			get
			{
				return "headers";
			}
		}

		// Token: 0x17004F03 RID: 20227
		// (get) Token: 0x06010E9C RID: 69276 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004F04 RID: 20228
		// (get) Token: 0x06010E9D RID: 69277 RVA: 0x002E8982 File Offset: 0x002E6B82
		internal override int ElementTypeId
		{
			get
			{
				return 12953;
			}
		}

		// Token: 0x06010E9E RID: 69278 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010E9F RID: 69279 RVA: 0x00293ECF File Offset: 0x002920CF
		public TupleSetHeaders()
		{
		}

		// Token: 0x06010EA0 RID: 69280 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TupleSetHeaders(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010EA1 RID: 69281 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TupleSetHeaders(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010EA2 RID: 69282 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TupleSetHeaders(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010EA3 RID: 69283 RVA: 0x002E8989 File Offset: 0x002E6B89
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "header" == name)
			{
				return new TupleSetHeader();
			}
			return null;
		}

		// Token: 0x06010EA4 RID: 69284 RVA: 0x002E89A4 File Offset: 0x002E6BA4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TupleSetHeaders>(deep);
		}

		// Token: 0x040076DA RID: 30426
		private const string tagName = "headers";

		// Token: 0x040076DB RID: 30427
		private const byte tagNsId = 53;

		// Token: 0x040076DC RID: 30428
		internal const int ElementTypeIdConst = 12953;
	}
}
