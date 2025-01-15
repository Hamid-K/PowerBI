using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025C4 RID: 9668
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PivotFormat))]
	internal class PivotFormats : OpenXmlCompositeElement
	{
		// Token: 0x17005797 RID: 22423
		// (get) Token: 0x060121E5 RID: 74213 RVA: 0x002F5BCC File Offset: 0x002F3DCC
		public override string LocalName
		{
			get
			{
				return "pivotFmts";
			}
		}

		// Token: 0x17005798 RID: 22424
		// (get) Token: 0x060121E6 RID: 74214 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005799 RID: 22425
		// (get) Token: 0x060121E7 RID: 74215 RVA: 0x002F5BD3 File Offset: 0x002F3DD3
		internal override int ElementTypeId
		{
			get
			{
				return 10495;
			}
		}

		// Token: 0x060121E8 RID: 74216 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060121E9 RID: 74217 RVA: 0x00293ECF File Offset: 0x002920CF
		public PivotFormats()
		{
		}

		// Token: 0x060121EA RID: 74218 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PivotFormats(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060121EB RID: 74219 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PivotFormats(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060121EC RID: 74220 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PivotFormats(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060121ED RID: 74221 RVA: 0x002F5BDA File Offset: 0x002F3DDA
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "pivotFmt" == name)
			{
				return new PivotFormat();
			}
			return null;
		}

		// Token: 0x060121EE RID: 74222 RVA: 0x002F5BF5 File Offset: 0x002F3DF5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotFormats>(deep);
		}

		// Token: 0x04007E51 RID: 32337
		private const string tagName = "pivotFmts";

		// Token: 0x04007E52 RID: 32338
		private const byte tagNsId = 11;

		// Token: 0x04007E53 RID: 32339
		internal const int ElementTypeIdConst = 10495;
	}
}
