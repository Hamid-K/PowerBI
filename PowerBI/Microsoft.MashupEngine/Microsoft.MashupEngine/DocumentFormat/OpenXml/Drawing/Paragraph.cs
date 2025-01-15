using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Drawing;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002822 RID: 10274
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Field))]
	[ChildElementInfo(typeof(Break))]
	[ChildElementInfo(typeof(EndParagraphRunProperties))]
	[ChildElementInfo(typeof(ParagraphProperties))]
	[ChildElementInfo(typeof(Run))]
	[ChildElementInfo(typeof(TextMath), FileFormatVersions.Office2010)]
	internal class Paragraph : OpenXmlCompositeElement
	{
		// Token: 0x170065BB RID: 26043
		// (get) Token: 0x060141D3 RID: 82387 RVA: 0x002EA9F7 File Offset: 0x002E8BF7
		public override string LocalName
		{
			get
			{
				return "p";
			}
		}

		// Token: 0x170065BC RID: 26044
		// (get) Token: 0x060141D4 RID: 82388 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170065BD RID: 26045
		// (get) Token: 0x060141D5 RID: 82389 RVA: 0x0030F775 File Offset: 0x0030D975
		internal override int ElementTypeId
		{
			get
			{
				return 10306;
			}
		}

		// Token: 0x060141D6 RID: 82390 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060141D7 RID: 82391 RVA: 0x00293ECF File Offset: 0x002920CF
		public Paragraph()
		{
		}

		// Token: 0x060141D8 RID: 82392 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Paragraph(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060141D9 RID: 82393 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Paragraph(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060141DA RID: 82394 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Paragraph(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060141DB RID: 82395 RVA: 0x0030F77C File Offset: 0x0030D97C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "pPr" == name)
			{
				return new ParagraphProperties();
			}
			if (10 == namespaceId && "r" == name)
			{
				return new Run();
			}
			if (10 == namespaceId && "br" == name)
			{
				return new Break();
			}
			if (10 == namespaceId && "fld" == name)
			{
				return new Field();
			}
			if (48 == namespaceId && "m" == name)
			{
				return new TextMath();
			}
			if (10 == namespaceId && "endParaRPr" == name)
			{
				return new EndParagraphRunProperties();
			}
			return null;
		}

		// Token: 0x170065BE RID: 26046
		// (get) Token: 0x060141DC RID: 82396 RVA: 0x0030F81A File Offset: 0x0030DA1A
		internal override string[] ElementTagNames
		{
			get
			{
				return Paragraph.eleTagNames;
			}
		}

		// Token: 0x170065BF RID: 26047
		// (get) Token: 0x060141DD RID: 82397 RVA: 0x0030F821 File Offset: 0x0030DA21
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Paragraph.eleNamespaceIds;
			}
		}

		// Token: 0x170065C0 RID: 26048
		// (get) Token: 0x060141DE RID: 82398 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170065C1 RID: 26049
		// (get) Token: 0x060141DF RID: 82399 RVA: 0x0030F828 File Offset: 0x0030DA28
		// (set) Token: 0x060141E0 RID: 82400 RVA: 0x0030F831 File Offset: 0x0030DA31
		public ParagraphProperties ParagraphProperties
		{
			get
			{
				return base.GetElement<ParagraphProperties>(0);
			}
			set
			{
				base.SetElement<ParagraphProperties>(0, value);
			}
		}

		// Token: 0x060141E1 RID: 82401 RVA: 0x0030F83B File Offset: 0x0030DA3B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Paragraph>(deep);
		}

		// Token: 0x04008911 RID: 35089
		private const string tagName = "p";

		// Token: 0x04008912 RID: 35090
		private const byte tagNsId = 10;

		// Token: 0x04008913 RID: 35091
		internal const int ElementTypeIdConst = 10306;

		// Token: 0x04008914 RID: 35092
		private static readonly string[] eleTagNames = new string[] { "pPr", "r", "br", "fld", "m", "endParaRPr" };

		// Token: 0x04008915 RID: 35093
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10, 48, 10 };
	}
}
