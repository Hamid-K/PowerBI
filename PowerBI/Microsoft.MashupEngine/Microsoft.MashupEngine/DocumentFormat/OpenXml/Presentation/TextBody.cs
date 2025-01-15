using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A63 RID: 10851
	[ChildElementInfo(typeof(ListStyle))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(BodyProperties))]
	[ChildElementInfo(typeof(Paragraph))]
	internal class TextBody : OpenXmlCompositeElement
	{
		// Token: 0x17007271 RID: 29297
		// (get) Token: 0x06015EA9 RID: 89769 RVA: 0x002DF074 File Offset: 0x002DD274
		public override string LocalName
		{
			get
			{
				return "txBody";
			}
		}

		// Token: 0x17007272 RID: 29298
		// (get) Token: 0x06015EAA RID: 89770 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007273 RID: 29299
		// (get) Token: 0x06015EAB RID: 89771 RVA: 0x00324860 File Offset: 0x00322A60
		internal override int ElementTypeId
		{
			get
			{
				return 12269;
			}
		}

		// Token: 0x06015EAC RID: 89772 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015EAD RID: 89773 RVA: 0x00293ECF File Offset: 0x002920CF
		public TextBody()
		{
		}

		// Token: 0x06015EAE RID: 89774 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TextBody(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015EAF RID: 89775 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TextBody(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015EB0 RID: 89776 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TextBody(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015EB1 RID: 89777 RVA: 0x00324868 File Offset: 0x00322A68
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

		// Token: 0x17007274 RID: 29300
		// (get) Token: 0x06015EB2 RID: 89778 RVA: 0x003248BE File Offset: 0x00322ABE
		internal override string[] ElementTagNames
		{
			get
			{
				return TextBody.eleTagNames;
			}
		}

		// Token: 0x17007275 RID: 29301
		// (get) Token: 0x06015EB3 RID: 89779 RVA: 0x003248C5 File Offset: 0x00322AC5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TextBody.eleNamespaceIds;
			}
		}

		// Token: 0x17007276 RID: 29302
		// (get) Token: 0x06015EB4 RID: 89780 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007277 RID: 29303
		// (get) Token: 0x06015EB5 RID: 89781 RVA: 0x002DF0E8 File Offset: 0x002DD2E8
		// (set) Token: 0x06015EB6 RID: 89782 RVA: 0x002DF0F1 File Offset: 0x002DD2F1
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

		// Token: 0x17007278 RID: 29304
		// (get) Token: 0x06015EB7 RID: 89783 RVA: 0x002DF0FB File Offset: 0x002DD2FB
		// (set) Token: 0x06015EB8 RID: 89784 RVA: 0x002DF104 File Offset: 0x002DD304
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

		// Token: 0x06015EB9 RID: 89785 RVA: 0x003248CC File Offset: 0x00322ACC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TextBody>(deep);
		}

		// Token: 0x04009567 RID: 38247
		private const string tagName = "txBody";

		// Token: 0x04009568 RID: 38248
		private const byte tagNsId = 24;

		// Token: 0x04009569 RID: 38249
		internal const int ElementTypeIdConst = 12269;

		// Token: 0x0400956A RID: 38250
		private static readonly string[] eleTagNames = new string[] { "bodyPr", "lstStyle", "p" };

		// Token: 0x0400956B RID: 38251
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10 };
	}
}
