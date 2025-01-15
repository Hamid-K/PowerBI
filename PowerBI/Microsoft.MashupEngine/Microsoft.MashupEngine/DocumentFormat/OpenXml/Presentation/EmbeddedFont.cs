using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A5A RID: 10842
	[ChildElementInfo(typeof(Font))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(BoldItalicFont))]
	[ChildElementInfo(typeof(RegularFont))]
	[ChildElementInfo(typeof(BoldFont))]
	[ChildElementInfo(typeof(ItalicFont))]
	internal class EmbeddedFont : OpenXmlCompositeElement
	{
		// Token: 0x17007214 RID: 29204
		// (get) Token: 0x06015DE0 RID: 89568 RVA: 0x00323D94 File Offset: 0x00321F94
		public override string LocalName
		{
			get
			{
				return "embeddedFont";
			}
		}

		// Token: 0x17007215 RID: 29205
		// (get) Token: 0x06015DE1 RID: 89569 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007216 RID: 29206
		// (get) Token: 0x06015DE2 RID: 89570 RVA: 0x00323D9B File Offset: 0x00321F9B
		internal override int ElementTypeId
		{
			get
			{
				return 12260;
			}
		}

		// Token: 0x06015DE3 RID: 89571 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015DE4 RID: 89572 RVA: 0x00293ECF File Offset: 0x002920CF
		public EmbeddedFont()
		{
		}

		// Token: 0x06015DE5 RID: 89573 RVA: 0x00293ED7 File Offset: 0x002920D7
		public EmbeddedFont(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015DE6 RID: 89574 RVA: 0x00293EE0 File Offset: 0x002920E0
		public EmbeddedFont(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015DE7 RID: 89575 RVA: 0x00293EE9 File Offset: 0x002920E9
		public EmbeddedFont(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015DE8 RID: 89576 RVA: 0x00323DA4 File Offset: 0x00321FA4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "font" == name)
			{
				return new Font();
			}
			if (24 == namespaceId && "regular" == name)
			{
				return new RegularFont();
			}
			if (24 == namespaceId && "bold" == name)
			{
				return new BoldFont();
			}
			if (24 == namespaceId && "italic" == name)
			{
				return new ItalicFont();
			}
			if (24 == namespaceId && "boldItalic" == name)
			{
				return new BoldItalicFont();
			}
			return null;
		}

		// Token: 0x17007217 RID: 29207
		// (get) Token: 0x06015DE9 RID: 89577 RVA: 0x00323E2A File Offset: 0x0032202A
		internal override string[] ElementTagNames
		{
			get
			{
				return EmbeddedFont.eleTagNames;
			}
		}

		// Token: 0x17007218 RID: 29208
		// (get) Token: 0x06015DEA RID: 89578 RVA: 0x00323E31 File Offset: 0x00322031
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return EmbeddedFont.eleNamespaceIds;
			}
		}

		// Token: 0x17007219 RID: 29209
		// (get) Token: 0x06015DEB RID: 89579 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700721A RID: 29210
		// (get) Token: 0x06015DEC RID: 89580 RVA: 0x00323E38 File Offset: 0x00322038
		// (set) Token: 0x06015DED RID: 89581 RVA: 0x00323E41 File Offset: 0x00322041
		public Font Font
		{
			get
			{
				return base.GetElement<Font>(0);
			}
			set
			{
				base.SetElement<Font>(0, value);
			}
		}

		// Token: 0x1700721B RID: 29211
		// (get) Token: 0x06015DEE RID: 89582 RVA: 0x00323E4B File Offset: 0x0032204B
		// (set) Token: 0x06015DEF RID: 89583 RVA: 0x00323E54 File Offset: 0x00322054
		public RegularFont RegularFont
		{
			get
			{
				return base.GetElement<RegularFont>(1);
			}
			set
			{
				base.SetElement<RegularFont>(1, value);
			}
		}

		// Token: 0x1700721C RID: 29212
		// (get) Token: 0x06015DF0 RID: 89584 RVA: 0x00323E5E File Offset: 0x0032205E
		// (set) Token: 0x06015DF1 RID: 89585 RVA: 0x00323E67 File Offset: 0x00322067
		public BoldFont BoldFont
		{
			get
			{
				return base.GetElement<BoldFont>(2);
			}
			set
			{
				base.SetElement<BoldFont>(2, value);
			}
		}

		// Token: 0x1700721D RID: 29213
		// (get) Token: 0x06015DF2 RID: 89586 RVA: 0x00323E71 File Offset: 0x00322071
		// (set) Token: 0x06015DF3 RID: 89587 RVA: 0x00323E7A File Offset: 0x0032207A
		public ItalicFont ItalicFont
		{
			get
			{
				return base.GetElement<ItalicFont>(3);
			}
			set
			{
				base.SetElement<ItalicFont>(3, value);
			}
		}

		// Token: 0x1700721E RID: 29214
		// (get) Token: 0x06015DF4 RID: 89588 RVA: 0x00323E84 File Offset: 0x00322084
		// (set) Token: 0x06015DF5 RID: 89589 RVA: 0x00323E8D File Offset: 0x0032208D
		public BoldItalicFont BoldItalicFont
		{
			get
			{
				return base.GetElement<BoldItalicFont>(4);
			}
			set
			{
				base.SetElement<BoldItalicFont>(4, value);
			}
		}

		// Token: 0x06015DF6 RID: 89590 RVA: 0x00323E97 File Offset: 0x00322097
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EmbeddedFont>(deep);
		}

		// Token: 0x04009532 RID: 38194
		private const string tagName = "embeddedFont";

		// Token: 0x04009533 RID: 38195
		private const byte tagNsId = 24;

		// Token: 0x04009534 RID: 38196
		internal const int ElementTypeIdConst = 12260;

		// Token: 0x04009535 RID: 38197
		private static readonly string[] eleTagNames = new string[] { "font", "regular", "bold", "italic", "boldItalic" };

		// Token: 0x04009536 RID: 38198
		private static readonly byte[] eleNamespaceIds = new byte[] { 24, 24, 24, 24, 24 };
	}
}
