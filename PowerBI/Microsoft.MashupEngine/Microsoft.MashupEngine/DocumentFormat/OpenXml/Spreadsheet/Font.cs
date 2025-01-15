using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C0D RID: 11277
	[ChildElementInfo(typeof(VerticalTextAlignment))]
	[ChildElementInfo(typeof(Bold))]
	[ChildElementInfo(typeof(Italic))]
	[ChildElementInfo(typeof(Strike))]
	[ChildElementInfo(typeof(Condense))]
	[ChildElementInfo(typeof(Extend))]
	[ChildElementInfo(typeof(Outline))]
	[ChildElementInfo(typeof(Shadow))]
	[ChildElementInfo(typeof(Underline))]
	[ChildElementInfo(typeof(FontCharSet))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Color))]
	[ChildElementInfo(typeof(FontName))]
	[ChildElementInfo(typeof(FontFamilyNumbering))]
	[ChildElementInfo(typeof(FontSize))]
	[ChildElementInfo(typeof(FontScheme))]
	internal class Font : OpenXmlCompositeElement
	{
		// Token: 0x17007FBD RID: 32701
		// (get) Token: 0x06017C07 RID: 97287 RVA: 0x002AD88F File Offset: 0x002ABA8F
		public override string LocalName
		{
			get
			{
				return "font";
			}
		}

		// Token: 0x17007FBE RID: 32702
		// (get) Token: 0x06017C08 RID: 97288 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007FBF RID: 32703
		// (get) Token: 0x06017C09 RID: 97289 RVA: 0x0033AAF3 File Offset: 0x00338CF3
		internal override int ElementTypeId
		{
			get
			{
				return 11258;
			}
		}

		// Token: 0x06017C0A RID: 97290 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06017C0B RID: 97291 RVA: 0x00293ECF File Offset: 0x002920CF
		public Font()
		{
		}

		// Token: 0x06017C0C RID: 97292 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Font(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017C0D RID: 97293 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Font(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017C0E RID: 97294 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Font(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017C0F RID: 97295 RVA: 0x0033AAFC File Offset: 0x00338CFC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "b" == name)
			{
				return new Bold();
			}
			if (22 == namespaceId && "i" == name)
			{
				return new Italic();
			}
			if (22 == namespaceId && "strike" == name)
			{
				return new Strike();
			}
			if (22 == namespaceId && "condense" == name)
			{
				return new Condense();
			}
			if (22 == namespaceId && "extend" == name)
			{
				return new Extend();
			}
			if (22 == namespaceId && "outline" == name)
			{
				return new Outline();
			}
			if (22 == namespaceId && "shadow" == name)
			{
				return new Shadow();
			}
			if (22 == namespaceId && "u" == name)
			{
				return new Underline();
			}
			if (22 == namespaceId && "vertAlign" == name)
			{
				return new VerticalTextAlignment();
			}
			if (22 == namespaceId && "sz" == name)
			{
				return new FontSize();
			}
			if (22 == namespaceId && "color" == name)
			{
				return new Color();
			}
			if (22 == namespaceId && "name" == name)
			{
				return new FontName();
			}
			if (22 == namespaceId && "family" == name)
			{
				return new FontFamilyNumbering();
			}
			if (22 == namespaceId && "charset" == name)
			{
				return new FontCharSet();
			}
			if (22 == namespaceId && "scheme" == name)
			{
				return new FontScheme();
			}
			return null;
		}

		// Token: 0x17007FC0 RID: 32704
		// (get) Token: 0x06017C10 RID: 97296 RVA: 0x0033AC72 File Offset: 0x00338E72
		internal override string[] ElementTagNames
		{
			get
			{
				return Font.eleTagNames;
			}
		}

		// Token: 0x17007FC1 RID: 32705
		// (get) Token: 0x06017C11 RID: 97297 RVA: 0x0033AC79 File Offset: 0x00338E79
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Font.eleNamespaceIds;
			}
		}

		// Token: 0x17007FC2 RID: 32706
		// (get) Token: 0x06017C12 RID: 97298 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007FC3 RID: 32707
		// (get) Token: 0x06017C13 RID: 97299 RVA: 0x0033AC80 File Offset: 0x00338E80
		// (set) Token: 0x06017C14 RID: 97300 RVA: 0x0033AC89 File Offset: 0x00338E89
		public Bold Bold
		{
			get
			{
				return base.GetElement<Bold>(0);
			}
			set
			{
				base.SetElement<Bold>(0, value);
			}
		}

		// Token: 0x17007FC4 RID: 32708
		// (get) Token: 0x06017C15 RID: 97301 RVA: 0x0033AC93 File Offset: 0x00338E93
		// (set) Token: 0x06017C16 RID: 97302 RVA: 0x0033AC9C File Offset: 0x00338E9C
		public Italic Italic
		{
			get
			{
				return base.GetElement<Italic>(1);
			}
			set
			{
				base.SetElement<Italic>(1, value);
			}
		}

		// Token: 0x17007FC5 RID: 32709
		// (get) Token: 0x06017C17 RID: 97303 RVA: 0x0033ACA6 File Offset: 0x00338EA6
		// (set) Token: 0x06017C18 RID: 97304 RVA: 0x0033ACAF File Offset: 0x00338EAF
		public Strike Strike
		{
			get
			{
				return base.GetElement<Strike>(2);
			}
			set
			{
				base.SetElement<Strike>(2, value);
			}
		}

		// Token: 0x17007FC6 RID: 32710
		// (get) Token: 0x06017C19 RID: 97305 RVA: 0x0033ACB9 File Offset: 0x00338EB9
		// (set) Token: 0x06017C1A RID: 97306 RVA: 0x0033ACC2 File Offset: 0x00338EC2
		public Condense Condense
		{
			get
			{
				return base.GetElement<Condense>(3);
			}
			set
			{
				base.SetElement<Condense>(3, value);
			}
		}

		// Token: 0x17007FC7 RID: 32711
		// (get) Token: 0x06017C1B RID: 97307 RVA: 0x0033ACCC File Offset: 0x00338ECC
		// (set) Token: 0x06017C1C RID: 97308 RVA: 0x0033ACD5 File Offset: 0x00338ED5
		public Extend Extend
		{
			get
			{
				return base.GetElement<Extend>(4);
			}
			set
			{
				base.SetElement<Extend>(4, value);
			}
		}

		// Token: 0x17007FC8 RID: 32712
		// (get) Token: 0x06017C1D RID: 97309 RVA: 0x0033ACDF File Offset: 0x00338EDF
		// (set) Token: 0x06017C1E RID: 97310 RVA: 0x0033ACE8 File Offset: 0x00338EE8
		public Outline Outline
		{
			get
			{
				return base.GetElement<Outline>(5);
			}
			set
			{
				base.SetElement<Outline>(5, value);
			}
		}

		// Token: 0x17007FC9 RID: 32713
		// (get) Token: 0x06017C1F RID: 97311 RVA: 0x0033ACF2 File Offset: 0x00338EF2
		// (set) Token: 0x06017C20 RID: 97312 RVA: 0x0033ACFB File Offset: 0x00338EFB
		public Shadow Shadow
		{
			get
			{
				return base.GetElement<Shadow>(6);
			}
			set
			{
				base.SetElement<Shadow>(6, value);
			}
		}

		// Token: 0x17007FCA RID: 32714
		// (get) Token: 0x06017C21 RID: 97313 RVA: 0x0033AD05 File Offset: 0x00338F05
		// (set) Token: 0x06017C22 RID: 97314 RVA: 0x0033AD0E File Offset: 0x00338F0E
		public Underline Underline
		{
			get
			{
				return base.GetElement<Underline>(7);
			}
			set
			{
				base.SetElement<Underline>(7, value);
			}
		}

		// Token: 0x17007FCB RID: 32715
		// (get) Token: 0x06017C23 RID: 97315 RVA: 0x0033AD18 File Offset: 0x00338F18
		// (set) Token: 0x06017C24 RID: 97316 RVA: 0x0033AD21 File Offset: 0x00338F21
		public VerticalTextAlignment VerticalTextAlignment
		{
			get
			{
				return base.GetElement<VerticalTextAlignment>(8);
			}
			set
			{
				base.SetElement<VerticalTextAlignment>(8, value);
			}
		}

		// Token: 0x17007FCC RID: 32716
		// (get) Token: 0x06017C25 RID: 97317 RVA: 0x0033AD2B File Offset: 0x00338F2B
		// (set) Token: 0x06017C26 RID: 97318 RVA: 0x0033AD35 File Offset: 0x00338F35
		public FontSize FontSize
		{
			get
			{
				return base.GetElement<FontSize>(9);
			}
			set
			{
				base.SetElement<FontSize>(9, value);
			}
		}

		// Token: 0x17007FCD RID: 32717
		// (get) Token: 0x06017C27 RID: 97319 RVA: 0x0033AD40 File Offset: 0x00338F40
		// (set) Token: 0x06017C28 RID: 97320 RVA: 0x0033AD4A File Offset: 0x00338F4A
		public Color Color
		{
			get
			{
				return base.GetElement<Color>(10);
			}
			set
			{
				base.SetElement<Color>(10, value);
			}
		}

		// Token: 0x17007FCE RID: 32718
		// (get) Token: 0x06017C29 RID: 97321 RVA: 0x0033AD55 File Offset: 0x00338F55
		// (set) Token: 0x06017C2A RID: 97322 RVA: 0x0033AD5F File Offset: 0x00338F5F
		public FontName FontName
		{
			get
			{
				return base.GetElement<FontName>(11);
			}
			set
			{
				base.SetElement<FontName>(11, value);
			}
		}

		// Token: 0x17007FCF RID: 32719
		// (get) Token: 0x06017C2B RID: 97323 RVA: 0x0033AD6A File Offset: 0x00338F6A
		// (set) Token: 0x06017C2C RID: 97324 RVA: 0x0033AD74 File Offset: 0x00338F74
		public FontFamilyNumbering FontFamilyNumbering
		{
			get
			{
				return base.GetElement<FontFamilyNumbering>(12);
			}
			set
			{
				base.SetElement<FontFamilyNumbering>(12, value);
			}
		}

		// Token: 0x17007FD0 RID: 32720
		// (get) Token: 0x06017C2D RID: 97325 RVA: 0x0033AD7F File Offset: 0x00338F7F
		// (set) Token: 0x06017C2E RID: 97326 RVA: 0x0033AD89 File Offset: 0x00338F89
		public FontCharSet FontCharSet
		{
			get
			{
				return base.GetElement<FontCharSet>(13);
			}
			set
			{
				base.SetElement<FontCharSet>(13, value);
			}
		}

		// Token: 0x17007FD1 RID: 32721
		// (get) Token: 0x06017C2F RID: 97327 RVA: 0x0033AD94 File Offset: 0x00338F94
		// (set) Token: 0x06017C30 RID: 97328 RVA: 0x0033AD9E File Offset: 0x00338F9E
		public FontScheme FontScheme
		{
			get
			{
				return base.GetElement<FontScheme>(14);
			}
			set
			{
				base.SetElement<FontScheme>(14, value);
			}
		}

		// Token: 0x06017C31 RID: 97329 RVA: 0x0033ADA9 File Offset: 0x00338FA9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Font>(deep);
		}

		// Token: 0x04009D72 RID: 40306
		private const string tagName = "font";

		// Token: 0x04009D73 RID: 40307
		private const byte tagNsId = 22;

		// Token: 0x04009D74 RID: 40308
		internal const int ElementTypeIdConst = 11258;

		// Token: 0x04009D75 RID: 40309
		private static readonly string[] eleTagNames = new string[]
		{
			"b", "i", "strike", "condense", "extend", "outline", "shadow", "u", "vertAlign", "sz",
			"color", "name", "family", "charset", "scheme"
		};

		// Token: 0x04009D76 RID: 40310
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			22, 22, 22, 22, 22, 22, 22, 22, 22, 22,
			22, 22, 22, 22, 22
		};
	}
}
