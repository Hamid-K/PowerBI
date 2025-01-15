using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002964 RID: 10596
	[ChildElementInfo(typeof(SuperscriptProperties))]
	[ChildElementInfo(typeof(Base))]
	[ChildElementInfo(typeof(SuperArgument))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Superscript : OpenXmlCompositeElement
	{
		// Token: 0x17006C1E RID: 27678
		// (get) Token: 0x060150BF RID: 86207 RVA: 0x0031A4D4 File Offset: 0x003186D4
		public override string LocalName
		{
			get
			{
				return "sSup";
			}
		}

		// Token: 0x17006C1F RID: 27679
		// (get) Token: 0x060150C0 RID: 86208 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C20 RID: 27680
		// (get) Token: 0x060150C1 RID: 86209 RVA: 0x0031A4DB File Offset: 0x003186DB
		internal override int ElementTypeId
		{
			get
			{
				return 10860;
			}
		}

		// Token: 0x060150C2 RID: 86210 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060150C3 RID: 86211 RVA: 0x00293ECF File Offset: 0x002920CF
		public Superscript()
		{
		}

		// Token: 0x060150C4 RID: 86212 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Superscript(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060150C5 RID: 86213 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Superscript(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060150C6 RID: 86214 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Superscript(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060150C7 RID: 86215 RVA: 0x0031A4E4 File Offset: 0x003186E4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "sSupPr" == name)
			{
				return new SuperscriptProperties();
			}
			if (21 == namespaceId && "e" == name)
			{
				return new Base();
			}
			if (21 == namespaceId && "sup" == name)
			{
				return new SuperArgument();
			}
			return null;
		}

		// Token: 0x17006C21 RID: 27681
		// (get) Token: 0x060150C8 RID: 86216 RVA: 0x0031A53A File Offset: 0x0031873A
		internal override string[] ElementTagNames
		{
			get
			{
				return Superscript.eleTagNames;
			}
		}

		// Token: 0x17006C22 RID: 27682
		// (get) Token: 0x060150C9 RID: 86217 RVA: 0x0031A541 File Offset: 0x00318741
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Superscript.eleNamespaceIds;
			}
		}

		// Token: 0x17006C23 RID: 27683
		// (get) Token: 0x060150CA RID: 86218 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006C24 RID: 27684
		// (get) Token: 0x060150CB RID: 86219 RVA: 0x0031A548 File Offset: 0x00318748
		// (set) Token: 0x060150CC RID: 86220 RVA: 0x0031A551 File Offset: 0x00318751
		public SuperscriptProperties SuperscriptProperties
		{
			get
			{
				return base.GetElement<SuperscriptProperties>(0);
			}
			set
			{
				base.SetElement<SuperscriptProperties>(0, value);
			}
		}

		// Token: 0x17006C25 RID: 27685
		// (get) Token: 0x060150CD RID: 86221 RVA: 0x00319656 File Offset: 0x00317856
		// (set) Token: 0x060150CE RID: 86222 RVA: 0x0031965F File Offset: 0x0031785F
		public Base Base
		{
			get
			{
				return base.GetElement<Base>(1);
			}
			set
			{
				base.SetElement<Base>(1, value);
			}
		}

		// Token: 0x17006C26 RID: 27686
		// (get) Token: 0x060150CF RID: 86223 RVA: 0x00319FCA File Offset: 0x003181CA
		// (set) Token: 0x060150D0 RID: 86224 RVA: 0x00319FD3 File Offset: 0x003181D3
		public SuperArgument SuperArgument
		{
			get
			{
				return base.GetElement<SuperArgument>(2);
			}
			set
			{
				base.SetElement<SuperArgument>(2, value);
			}
		}

		// Token: 0x060150D1 RID: 86225 RVA: 0x0031A55B File Offset: 0x0031875B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Superscript>(deep);
		}

		// Token: 0x04009133 RID: 37171
		private const string tagName = "sSup";

		// Token: 0x04009134 RID: 37172
		private const byte tagNsId = 21;

		// Token: 0x04009135 RID: 37173
		internal const int ElementTypeIdConst = 10860;

		// Token: 0x04009136 RID: 37174
		private static readonly string[] eleTagNames = new string[] { "sSupPr", "e", "sup" };

		// Token: 0x04009137 RID: 37175
		private static readonly byte[] eleNamespaceIds = new byte[] { 21, 21, 21 };
	}
}
