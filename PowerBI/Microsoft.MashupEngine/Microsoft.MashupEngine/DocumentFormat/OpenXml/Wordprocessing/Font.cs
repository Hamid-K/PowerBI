using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FBB RID: 12219
	[ChildElementInfo(typeof(FontFamily))]
	[ChildElementInfo(typeof(Pitch))]
	[ChildElementInfo(typeof(FontSignature))]
	[ChildElementInfo(typeof(AltName))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(FontCharSet))]
	[ChildElementInfo(typeof(NotTrueType))]
	[ChildElementInfo(typeof(EmbedRegularFont))]
	[ChildElementInfo(typeof(Panose1Number))]
	[ChildElementInfo(typeof(EmbedBoldFont))]
	[ChildElementInfo(typeof(EmbedItalicFont))]
	[ChildElementInfo(typeof(EmbedBoldItalicFont))]
	internal class Font : OpenXmlCompositeElement
	{
		// Token: 0x170093B7 RID: 37815
		// (get) Token: 0x0601A7D3 RID: 108499 RVA: 0x002AD88F File Offset: 0x002ABA8F
		public override string LocalName
		{
			get
			{
				return "font";
			}
		}

		// Token: 0x170093B8 RID: 37816
		// (get) Token: 0x0601A7D4 RID: 108500 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170093B9 RID: 37817
		// (get) Token: 0x0601A7D5 RID: 108501 RVA: 0x00362F50 File Offset: 0x00361150
		internal override int ElementTypeId
		{
			get
			{
				return 11926;
			}
		}

		// Token: 0x0601A7D6 RID: 108502 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170093BA RID: 37818
		// (get) Token: 0x0601A7D7 RID: 108503 RVA: 0x00362F57 File Offset: 0x00361157
		internal override string[] AttributeTagNames
		{
			get
			{
				return Font.attributeTagNames;
			}
		}

		// Token: 0x170093BB RID: 37819
		// (get) Token: 0x0601A7D8 RID: 108504 RVA: 0x00362F5E File Offset: 0x0036115E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Font.attributeNamespaceIds;
			}
		}

		// Token: 0x170093BC RID: 37820
		// (get) Token: 0x0601A7D9 RID: 108505 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601A7DA RID: 108506 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "name")]
		public StringValue Name
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601A7DB RID: 108507 RVA: 0x00293ECF File Offset: 0x002920CF
		public Font()
		{
		}

		// Token: 0x0601A7DC RID: 108508 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Font(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A7DD RID: 108509 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Font(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A7DE RID: 108510 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Font(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A7DF RID: 108511 RVA: 0x00362F68 File Offset: 0x00361168
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "altName" == name)
			{
				return new AltName();
			}
			if (23 == namespaceId && "panose1" == name)
			{
				return new Panose1Number();
			}
			if (23 == namespaceId && "charset" == name)
			{
				return new FontCharSet();
			}
			if (23 == namespaceId && "family" == name)
			{
				return new FontFamily();
			}
			if (23 == namespaceId && "notTrueType" == name)
			{
				return new NotTrueType();
			}
			if (23 == namespaceId && "pitch" == name)
			{
				return new Pitch();
			}
			if (23 == namespaceId && "sig" == name)
			{
				return new FontSignature();
			}
			if (23 == namespaceId && "embedRegular" == name)
			{
				return new EmbedRegularFont();
			}
			if (23 == namespaceId && "embedBold" == name)
			{
				return new EmbedBoldFont();
			}
			if (23 == namespaceId && "embedItalic" == name)
			{
				return new EmbedItalicFont();
			}
			if (23 == namespaceId && "embedBoldItalic" == name)
			{
				return new EmbedBoldItalicFont();
			}
			return null;
		}

		// Token: 0x170093BD RID: 37821
		// (get) Token: 0x0601A7E0 RID: 108512 RVA: 0x0036307E File Offset: 0x0036127E
		internal override string[] ElementTagNames
		{
			get
			{
				return Font.eleTagNames;
			}
		}

		// Token: 0x170093BE RID: 37822
		// (get) Token: 0x0601A7E1 RID: 108513 RVA: 0x00363085 File Offset: 0x00361285
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Font.eleNamespaceIds;
			}
		}

		// Token: 0x170093BF RID: 37823
		// (get) Token: 0x0601A7E2 RID: 108514 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170093C0 RID: 37824
		// (get) Token: 0x0601A7E3 RID: 108515 RVA: 0x0036308C File Offset: 0x0036128C
		// (set) Token: 0x0601A7E4 RID: 108516 RVA: 0x00363095 File Offset: 0x00361295
		public AltName AltName
		{
			get
			{
				return base.GetElement<AltName>(0);
			}
			set
			{
				base.SetElement<AltName>(0, value);
			}
		}

		// Token: 0x170093C1 RID: 37825
		// (get) Token: 0x0601A7E5 RID: 108517 RVA: 0x0036309F File Offset: 0x0036129F
		// (set) Token: 0x0601A7E6 RID: 108518 RVA: 0x003630A8 File Offset: 0x003612A8
		public Panose1Number Panose1Number
		{
			get
			{
				return base.GetElement<Panose1Number>(1);
			}
			set
			{
				base.SetElement<Panose1Number>(1, value);
			}
		}

		// Token: 0x170093C2 RID: 37826
		// (get) Token: 0x0601A7E7 RID: 108519 RVA: 0x003630B2 File Offset: 0x003612B2
		// (set) Token: 0x0601A7E8 RID: 108520 RVA: 0x003630BB File Offset: 0x003612BB
		public FontCharSet FontCharSet
		{
			get
			{
				return base.GetElement<FontCharSet>(2);
			}
			set
			{
				base.SetElement<FontCharSet>(2, value);
			}
		}

		// Token: 0x170093C3 RID: 37827
		// (get) Token: 0x0601A7E9 RID: 108521 RVA: 0x003630C5 File Offset: 0x003612C5
		// (set) Token: 0x0601A7EA RID: 108522 RVA: 0x003630CE File Offset: 0x003612CE
		public FontFamily FontFamily
		{
			get
			{
				return base.GetElement<FontFamily>(3);
			}
			set
			{
				base.SetElement<FontFamily>(3, value);
			}
		}

		// Token: 0x170093C4 RID: 37828
		// (get) Token: 0x0601A7EB RID: 108523 RVA: 0x003630D8 File Offset: 0x003612D8
		// (set) Token: 0x0601A7EC RID: 108524 RVA: 0x003630E1 File Offset: 0x003612E1
		public NotTrueType NotTrueType
		{
			get
			{
				return base.GetElement<NotTrueType>(4);
			}
			set
			{
				base.SetElement<NotTrueType>(4, value);
			}
		}

		// Token: 0x170093C5 RID: 37829
		// (get) Token: 0x0601A7ED RID: 108525 RVA: 0x003630EB File Offset: 0x003612EB
		// (set) Token: 0x0601A7EE RID: 108526 RVA: 0x003630F4 File Offset: 0x003612F4
		public Pitch Pitch
		{
			get
			{
				return base.GetElement<Pitch>(5);
			}
			set
			{
				base.SetElement<Pitch>(5, value);
			}
		}

		// Token: 0x170093C6 RID: 37830
		// (get) Token: 0x0601A7EF RID: 108527 RVA: 0x003630FE File Offset: 0x003612FE
		// (set) Token: 0x0601A7F0 RID: 108528 RVA: 0x00363107 File Offset: 0x00361307
		public FontSignature FontSignature
		{
			get
			{
				return base.GetElement<FontSignature>(6);
			}
			set
			{
				base.SetElement<FontSignature>(6, value);
			}
		}

		// Token: 0x170093C7 RID: 37831
		// (get) Token: 0x0601A7F1 RID: 108529 RVA: 0x00363111 File Offset: 0x00361311
		// (set) Token: 0x0601A7F2 RID: 108530 RVA: 0x0036311A File Offset: 0x0036131A
		public EmbedRegularFont EmbedRegularFont
		{
			get
			{
				return base.GetElement<EmbedRegularFont>(7);
			}
			set
			{
				base.SetElement<EmbedRegularFont>(7, value);
			}
		}

		// Token: 0x170093C8 RID: 37832
		// (get) Token: 0x0601A7F3 RID: 108531 RVA: 0x00363124 File Offset: 0x00361324
		// (set) Token: 0x0601A7F4 RID: 108532 RVA: 0x0036312D File Offset: 0x0036132D
		public EmbedBoldFont EmbedBoldFont
		{
			get
			{
				return base.GetElement<EmbedBoldFont>(8);
			}
			set
			{
				base.SetElement<EmbedBoldFont>(8, value);
			}
		}

		// Token: 0x170093C9 RID: 37833
		// (get) Token: 0x0601A7F5 RID: 108533 RVA: 0x00363137 File Offset: 0x00361337
		// (set) Token: 0x0601A7F6 RID: 108534 RVA: 0x00363141 File Offset: 0x00361341
		public EmbedItalicFont EmbedItalicFont
		{
			get
			{
				return base.GetElement<EmbedItalicFont>(9);
			}
			set
			{
				base.SetElement<EmbedItalicFont>(9, value);
			}
		}

		// Token: 0x170093CA RID: 37834
		// (get) Token: 0x0601A7F7 RID: 108535 RVA: 0x0036314C File Offset: 0x0036134C
		// (set) Token: 0x0601A7F8 RID: 108536 RVA: 0x00363156 File Offset: 0x00361356
		public EmbedBoldItalicFont EmbedBoldItalicFont
		{
			get
			{
				return base.GetElement<EmbedBoldItalicFont>(10);
			}
			set
			{
				base.SetElement<EmbedBoldItalicFont>(10, value);
			}
		}

		// Token: 0x0601A7F9 RID: 108537 RVA: 0x00363161 File Offset: 0x00361361
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "name" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A7FA RID: 108538 RVA: 0x00363183 File Offset: 0x00361383
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Font>(deep);
		}

		// Token: 0x0400AD31 RID: 44337
		private const string tagName = "font";

		// Token: 0x0400AD32 RID: 44338
		private const byte tagNsId = 23;

		// Token: 0x0400AD33 RID: 44339
		internal const int ElementTypeIdConst = 11926;

		// Token: 0x0400AD34 RID: 44340
		private static string[] attributeTagNames = new string[] { "name" };

		// Token: 0x0400AD35 RID: 44341
		private static byte[] attributeNamespaceIds = new byte[] { 23 };

		// Token: 0x0400AD36 RID: 44342
		private static readonly string[] eleTagNames = new string[]
		{
			"altName", "panose1", "charset", "family", "notTrueType", "pitch", "sig", "embedRegular", "embedBold", "embedItalic",
			"embedBoldItalic"
		};

		// Token: 0x0400AD37 RID: 44343
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23
		};
	}
}
