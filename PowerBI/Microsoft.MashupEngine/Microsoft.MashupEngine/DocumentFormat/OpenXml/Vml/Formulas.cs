using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Vml
{
	// Token: 0x02002243 RID: 8771
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Formula))]
	internal class Formulas : OpenXmlCompositeElement
	{
		// Token: 0x1700398F RID: 14735
		// (get) Token: 0x0600E0CE RID: 57550 RVA: 0x002C0214 File Offset: 0x002BE414
		public override string LocalName
		{
			get
			{
				return "formulas";
			}
		}

		// Token: 0x17003990 RID: 14736
		// (get) Token: 0x0600E0CF RID: 57551 RVA: 0x00243C87 File Offset: 0x00241E87
		internal override byte NamespaceId
		{
			get
			{
				return 26;
			}
		}

		// Token: 0x17003991 RID: 14737
		// (get) Token: 0x0600E0D0 RID: 57552 RVA: 0x002C021B File Offset: 0x002BE41B
		internal override int ElementTypeId
		{
			get
			{
				return 12507;
			}
		}

		// Token: 0x0600E0D1 RID: 57553 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600E0D2 RID: 57554 RVA: 0x00293ECF File Offset: 0x002920CF
		public Formulas()
		{
		}

		// Token: 0x0600E0D3 RID: 57555 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Formulas(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E0D4 RID: 57556 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Formulas(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E0D5 RID: 57557 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Formulas(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600E0D6 RID: 57558 RVA: 0x002C0222 File Offset: 0x002BE422
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (26 == namespaceId && "f" == name)
			{
				return new Formula();
			}
			return null;
		}

		// Token: 0x0600E0D7 RID: 57559 RVA: 0x002C023D File Offset: 0x002BE43D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Formulas>(deep);
		}

		// Token: 0x04006E95 RID: 28309
		private const string tagName = "formulas";

		// Token: 0x04006E96 RID: 28310
		private const byte tagNsId = 26;

		// Token: 0x04006E97 RID: 28311
		internal const int ElementTypeIdConst = 12507;
	}
}
