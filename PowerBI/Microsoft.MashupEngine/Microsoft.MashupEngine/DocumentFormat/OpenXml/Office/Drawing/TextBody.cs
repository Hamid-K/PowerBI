using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office.Drawing
{
	// Token: 0x0200232B RID: 9003
	[ChildElementInfo(typeof(BodyProperties))]
	[ChildElementInfo(typeof(ListStyle))]
	[ChildElementInfo(typeof(Paragraph))]
	[GeneratedCode("DomGen", "2.0")]
	internal class TextBody : OpenXmlCompositeElement
	{
		// Token: 0x170048B1 RID: 18609
		// (get) Token: 0x060100BE RID: 65726 RVA: 0x002DF074 File Offset: 0x002DD274
		public override string LocalName
		{
			get
			{
				return "txBody";
			}
		}

		// Token: 0x170048B2 RID: 18610
		// (get) Token: 0x060100BF RID: 65727 RVA: 0x002DE7F3 File Offset: 0x002DC9F3
		internal override byte NamespaceId
		{
			get
			{
				return 56;
			}
		}

		// Token: 0x170048B3 RID: 18611
		// (get) Token: 0x060100C0 RID: 65728 RVA: 0x002DF07B File Offset: 0x002DD27B
		internal override int ElementTypeId
		{
			get
			{
				return 13026;
			}
		}

		// Token: 0x060100C1 RID: 65729 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060100C2 RID: 65730 RVA: 0x00293ECF File Offset: 0x002920CF
		public TextBody()
		{
		}

		// Token: 0x060100C3 RID: 65731 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TextBody(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060100C4 RID: 65732 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TextBody(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060100C5 RID: 65733 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TextBody(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060100C6 RID: 65734 RVA: 0x002DF084 File Offset: 0x002DD284
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "bodyPr" == name)
			{
				return new BodyProperties();
			}
			if (10 == namespaceId && "lstStyle" == name)
			{
				return new ListStyle();
			}
			if (10 == namespaceId && "p" == name)
			{
				return new Paragraph();
			}
			return null;
		}

		// Token: 0x170048B4 RID: 18612
		// (get) Token: 0x060100C7 RID: 65735 RVA: 0x002DF0DA File Offset: 0x002DD2DA
		internal override string[] ElementTagNames
		{
			get
			{
				return TextBody.eleTagNames;
			}
		}

		// Token: 0x170048B5 RID: 18613
		// (get) Token: 0x060100C8 RID: 65736 RVA: 0x002DF0E1 File Offset: 0x002DD2E1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TextBody.eleNamespaceIds;
			}
		}

		// Token: 0x170048B6 RID: 18614
		// (get) Token: 0x060100C9 RID: 65737 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170048B7 RID: 18615
		// (get) Token: 0x060100CA RID: 65738 RVA: 0x002DF0E8 File Offset: 0x002DD2E8
		// (set) Token: 0x060100CB RID: 65739 RVA: 0x002DF0F1 File Offset: 0x002DD2F1
		public BodyProperties BodyProperties
		{
			get
			{
				return base.GetElement<BodyProperties>(0);
			}
			set
			{
				base.SetElement<BodyProperties>(0, value);
			}
		}

		// Token: 0x170048B8 RID: 18616
		// (get) Token: 0x060100CC RID: 65740 RVA: 0x002DF0FB File Offset: 0x002DD2FB
		// (set) Token: 0x060100CD RID: 65741 RVA: 0x002DF104 File Offset: 0x002DD304
		public ListStyle ListStyle
		{
			get
			{
				return base.GetElement<ListStyle>(1);
			}
			set
			{
				base.SetElement<ListStyle>(1, value);
			}
		}

		// Token: 0x060100CE RID: 65742 RVA: 0x002DF10E File Offset: 0x002DD30E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TextBody>(deep);
		}

		// Token: 0x040072D9 RID: 29401
		private const string tagName = "txBody";

		// Token: 0x040072DA RID: 29402
		private const byte tagNsId = 56;

		// Token: 0x040072DB RID: 29403
		internal const int ElementTypeIdConst = 13026;

		// Token: 0x040072DC RID: 29404
		private static readonly string[] eleTagNames = new string[] { "bodyPr", "lstStyle", "p" };

		// Token: 0x040072DD RID: 29405
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10 };
	}
}
