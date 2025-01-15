using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002988 RID: 10632
	[ChildElementInfo(typeof(Literal))]
	[ChildElementInfo(typeof(Break))]
	[ChildElementInfo(typeof(Alignment))]
	[ChildElementInfo(typeof(Script))]
	[ChildElementInfo(typeof(Style))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NormalText))]
	internal class RunProperties : OpenXmlCompositeElement
	{
		// Token: 0x17006CA7 RID: 27815
		// (get) Token: 0x060151DC RID: 86492 RVA: 0x0030F747 File Offset: 0x0030D947
		public override string LocalName
		{
			get
			{
				return "rPr";
			}
		}

		// Token: 0x17006CA8 RID: 27816
		// (get) Token: 0x060151DD RID: 86493 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006CA9 RID: 27817
		// (get) Token: 0x060151DE RID: 86494 RVA: 0x0031B729 File Offset: 0x00319929
		internal override int ElementTypeId
		{
			get
			{
				return 10868;
			}
		}

		// Token: 0x060151DF RID: 86495 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060151E0 RID: 86496 RVA: 0x00293ECF File Offset: 0x002920CF
		public RunProperties()
		{
		}

		// Token: 0x060151E1 RID: 86497 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RunProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060151E2 RID: 86498 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RunProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060151E3 RID: 86499 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RunProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060151E4 RID: 86500 RVA: 0x0031B730 File Offset: 0x00319930
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "lit" == name)
			{
				return new Literal();
			}
			if (21 == namespaceId && "nor" == name)
			{
				return new NormalText();
			}
			if (21 == namespaceId && "scr" == name)
			{
				return new Script();
			}
			if (21 == namespaceId && "sty" == name)
			{
				return new Style();
			}
			if (21 == namespaceId && "brk" == name)
			{
				return new Break();
			}
			if (21 == namespaceId && "aln" == name)
			{
				return new Alignment();
			}
			return null;
		}

		// Token: 0x17006CAA RID: 27818
		// (get) Token: 0x060151E5 RID: 86501 RVA: 0x0031B7CE File Offset: 0x003199CE
		internal override string[] ElementTagNames
		{
			get
			{
				return RunProperties.eleTagNames;
			}
		}

		// Token: 0x17006CAB RID: 27819
		// (get) Token: 0x060151E6 RID: 86502 RVA: 0x0031B7D5 File Offset: 0x003199D5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return RunProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006CAC RID: 27820
		// (get) Token: 0x060151E7 RID: 86503 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006CAD RID: 27821
		// (get) Token: 0x060151E8 RID: 86504 RVA: 0x0031B7DC File Offset: 0x003199DC
		// (set) Token: 0x060151E9 RID: 86505 RVA: 0x0031B7E5 File Offset: 0x003199E5
		public Literal Literal
		{
			get
			{
				return base.GetElement<Literal>(0);
			}
			set
			{
				base.SetElement<Literal>(0, value);
			}
		}

		// Token: 0x060151EA RID: 86506 RVA: 0x0031B7EF File Offset: 0x003199EF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RunProperties>(deep);
		}

		// Token: 0x040091A6 RID: 37286
		private const string tagName = "rPr";

		// Token: 0x040091A7 RID: 37287
		private const byte tagNsId = 21;

		// Token: 0x040091A8 RID: 37288
		internal const int ElementTypeIdConst = 10868;

		// Token: 0x040091A9 RID: 37289
		private static readonly string[] eleTagNames = new string[] { "lit", "nor", "scr", "sty", "brk", "aln" };

		// Token: 0x040091AA RID: 37290
		private static readonly byte[] eleNamespaceIds = new byte[] { 21, 21, 21, 21, 21, 21 };
	}
}
