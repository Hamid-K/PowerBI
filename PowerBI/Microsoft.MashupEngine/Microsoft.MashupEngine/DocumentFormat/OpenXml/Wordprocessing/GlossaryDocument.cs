using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F1E RID: 12062
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DocumentBackground))]
	[ChildElementInfo(typeof(DocParts))]
	internal class GlossaryDocument : OpenXmlPartRootElement
	{
		// Token: 0x17008E9E RID: 36510
		// (get) Token: 0x06019CE9 RID: 105705 RVA: 0x00356B3D File Offset: 0x00354D3D
		public override string LocalName
		{
			get
			{
				return "glossaryDocument";
			}
		}

		// Token: 0x17008E9F RID: 36511
		// (get) Token: 0x06019CEA RID: 105706 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008EA0 RID: 36512
		// (get) Token: 0x06019CEB RID: 105707 RVA: 0x00356B44 File Offset: 0x00354D44
		internal override int ElementTypeId
		{
			get
			{
				return 11703;
			}
		}

		// Token: 0x06019CEC RID: 105708 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019CED RID: 105709 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal GlossaryDocument(GlossaryDocumentPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06019CEE RID: 105710 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(GlossaryDocumentPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17008EA1 RID: 36513
		// (get) Token: 0x06019CEF RID: 105711 RVA: 0x00356B4B File Offset: 0x00354D4B
		// (set) Token: 0x06019CF0 RID: 105712 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public GlossaryDocumentPart GlossaryDocumentPart
		{
			get
			{
				return base.OpenXmlPart as GlossaryDocumentPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06019CF1 RID: 105713 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public GlossaryDocument(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019CF2 RID: 105714 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public GlossaryDocument(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019CF3 RID: 105715 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public GlossaryDocument(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019CF4 RID: 105716 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public GlossaryDocument()
		{
		}

		// Token: 0x06019CF5 RID: 105717 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(GlossaryDocumentPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06019CF6 RID: 105718 RVA: 0x00356B58 File Offset: 0x00354D58
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "background" == name)
			{
				return new DocumentBackground();
			}
			if (23 == namespaceId && "docParts" == name)
			{
				return new DocParts();
			}
			return null;
		}

		// Token: 0x17008EA2 RID: 36514
		// (get) Token: 0x06019CF7 RID: 105719 RVA: 0x00356B8B File Offset: 0x00354D8B
		internal override string[] ElementTagNames
		{
			get
			{
				return GlossaryDocument.eleTagNames;
			}
		}

		// Token: 0x17008EA3 RID: 36515
		// (get) Token: 0x06019CF8 RID: 105720 RVA: 0x00356B92 File Offset: 0x00354D92
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return GlossaryDocument.eleNamespaceIds;
			}
		}

		// Token: 0x17008EA4 RID: 36516
		// (get) Token: 0x06019CF9 RID: 105721 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008EA5 RID: 36517
		// (get) Token: 0x06019CFA RID: 105722 RVA: 0x00356ACD File Offset: 0x00354CCD
		// (set) Token: 0x06019CFB RID: 105723 RVA: 0x00356AD6 File Offset: 0x00354CD6
		public DocumentBackground DocumentBackground
		{
			get
			{
				return base.GetElement<DocumentBackground>(0);
			}
			set
			{
				base.SetElement<DocumentBackground>(0, value);
			}
		}

		// Token: 0x17008EA6 RID: 36518
		// (get) Token: 0x06019CFC RID: 105724 RVA: 0x00356B99 File Offset: 0x00354D99
		// (set) Token: 0x06019CFD RID: 105725 RVA: 0x00356BA2 File Offset: 0x00354DA2
		public DocParts DocParts
		{
			get
			{
				return base.GetElement<DocParts>(1);
			}
			set
			{
				base.SetElement<DocParts>(1, value);
			}
		}

		// Token: 0x06019CFE RID: 105726 RVA: 0x00356BAC File Offset: 0x00354DAC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GlossaryDocument>(deep);
		}

		// Token: 0x0400AA8B RID: 43659
		private const string tagName = "glossaryDocument";

		// Token: 0x0400AA8C RID: 43660
		private const byte tagNsId = 23;

		// Token: 0x0400AA8D RID: 43661
		internal const int ElementTypeIdConst = 11703;

		// Token: 0x0400AA8E RID: 43662
		private static readonly string[] eleTagNames = new string[] { "background", "docParts" };

		// Token: 0x0400AA8F RID: 43663
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23 };
	}
}
