using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x0200243F RID: 9279
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(OlapSlicerCacheRange), FileFormatVersions.Office2010)]
	internal class OlapSlicerCacheRanges : OpenXmlCompositeElement
	{
		// Token: 0x17005068 RID: 20584
		// (get) Token: 0x060111C9 RID: 70089 RVA: 0x002EABD3 File Offset: 0x002E8DD3
		public override string LocalName
		{
			get
			{
				return "ranges";
			}
		}

		// Token: 0x17005069 RID: 20585
		// (get) Token: 0x060111CA RID: 70090 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x1700506A RID: 20586
		// (get) Token: 0x060111CB RID: 70091 RVA: 0x002EABDA File Offset: 0x002E8DDA
		internal override int ElementTypeId
		{
			get
			{
				return 13003;
			}
		}

		// Token: 0x060111CC RID: 70092 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060111CD RID: 70093 RVA: 0x00293ECF File Offset: 0x002920CF
		public OlapSlicerCacheRanges()
		{
		}

		// Token: 0x060111CE RID: 70094 RVA: 0x00293ED7 File Offset: 0x002920D7
		public OlapSlicerCacheRanges(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060111CF RID: 70095 RVA: 0x00293EE0 File Offset: 0x002920E0
		public OlapSlicerCacheRanges(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060111D0 RID: 70096 RVA: 0x00293EE9 File Offset: 0x002920E9
		public OlapSlicerCacheRanges(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060111D1 RID: 70097 RVA: 0x002EABE1 File Offset: 0x002E8DE1
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "range" == name)
			{
				return new OlapSlicerCacheRange();
			}
			return null;
		}

		// Token: 0x060111D2 RID: 70098 RVA: 0x002EABFC File Offset: 0x002E8DFC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OlapSlicerCacheRanges>(deep);
		}

		// Token: 0x040077B4 RID: 30644
		private const string tagName = "ranges";

		// Token: 0x040077B5 RID: 30645
		private const byte tagNsId = 53;

		// Token: 0x040077B6 RID: 30646
		internal const int ElementTypeIdConst = 13003;
	}
}
