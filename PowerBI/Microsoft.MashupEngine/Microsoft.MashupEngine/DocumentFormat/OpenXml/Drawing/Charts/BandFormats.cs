using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002551 RID: 9553
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(BandFormat))]
	internal class BandFormats : OpenXmlCompositeElement
	{
		// Token: 0x17005555 RID: 21845
		// (get) Token: 0x06011CD5 RID: 72917 RVA: 0x002F2922 File Offset: 0x002F0B22
		public override string LocalName
		{
			get
			{
				return "bandFmts";
			}
		}

		// Token: 0x17005556 RID: 21846
		// (get) Token: 0x06011CD6 RID: 72918 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005557 RID: 21847
		// (get) Token: 0x06011CD7 RID: 72919 RVA: 0x002F2929 File Offset: 0x002F0B29
		internal override int ElementTypeId
		{
			get
			{
				return 10372;
			}
		}

		// Token: 0x06011CD8 RID: 72920 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011CD9 RID: 72921 RVA: 0x00293ECF File Offset: 0x002920CF
		public BandFormats()
		{
		}

		// Token: 0x06011CDA RID: 72922 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BandFormats(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011CDB RID: 72923 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BandFormats(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011CDC RID: 72924 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BandFormats(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011CDD RID: 72925 RVA: 0x002F2930 File Offset: 0x002F0B30
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "bandFmt" == name)
			{
				return new BandFormat();
			}
			return null;
		}

		// Token: 0x06011CDE RID: 72926 RVA: 0x002F294B File Offset: 0x002F0B4B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BandFormats>(deep);
		}

		// Token: 0x04007C9B RID: 31899
		private const string tagName = "bandFmts";

		// Token: 0x04007C9C RID: 31900
		private const byte tagNsId = 11;

		// Token: 0x04007C9D RID: 31901
		internal const int ElementTypeIdConst = 10372;
	}
}
