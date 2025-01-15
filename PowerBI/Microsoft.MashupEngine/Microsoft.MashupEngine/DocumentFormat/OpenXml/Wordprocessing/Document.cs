using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F1D RID: 12061
	[ChildElementInfo(typeof(Body))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DocumentBackground))]
	internal class Document : OpenXmlPartRootElement
	{
		// Token: 0x17008E95 RID: 36501
		// (get) Token: 0x06019CD2 RID: 105682 RVA: 0x002A3F1A File Offset: 0x002A211A
		public override string LocalName
		{
			get
			{
				return "document";
			}
		}

		// Token: 0x17008E96 RID: 36502
		// (get) Token: 0x06019CD3 RID: 105683 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008E97 RID: 36503
		// (get) Token: 0x06019CD4 RID: 105684 RVA: 0x00356A78 File Offset: 0x00354C78
		internal override int ElementTypeId
		{
			get
			{
				return 11702;
			}
		}

		// Token: 0x06019CD5 RID: 105685 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019CD6 RID: 105686 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal Document(MainDocumentPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06019CD7 RID: 105687 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(MainDocumentPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17008E98 RID: 36504
		// (get) Token: 0x06019CD8 RID: 105688 RVA: 0x00356A7F File Offset: 0x00354C7F
		// (set) Token: 0x06019CD9 RID: 105689 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public MainDocumentPart MainDocumentPart
		{
			get
			{
				return base.OpenXmlPart as MainDocumentPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06019CDA RID: 105690 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public Document(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019CDB RID: 105691 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public Document(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019CDC RID: 105692 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public Document(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019CDD RID: 105693 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public Document()
		{
		}

		// Token: 0x06019CDE RID: 105694 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(MainDocumentPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06019CDF RID: 105695 RVA: 0x00356A8C File Offset: 0x00354C8C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "background" == name)
			{
				return new DocumentBackground();
			}
			if (23 == namespaceId && "body" == name)
			{
				return new Body();
			}
			return null;
		}

		// Token: 0x17008E99 RID: 36505
		// (get) Token: 0x06019CE0 RID: 105696 RVA: 0x00356ABF File Offset: 0x00354CBF
		internal override string[] ElementTagNames
		{
			get
			{
				return Document.eleTagNames;
			}
		}

		// Token: 0x17008E9A RID: 36506
		// (get) Token: 0x06019CE1 RID: 105697 RVA: 0x00356AC6 File Offset: 0x00354CC6
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Document.eleNamespaceIds;
			}
		}

		// Token: 0x17008E9B RID: 36507
		// (get) Token: 0x06019CE2 RID: 105698 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008E9C RID: 36508
		// (get) Token: 0x06019CE3 RID: 105699 RVA: 0x00356ACD File Offset: 0x00354CCD
		// (set) Token: 0x06019CE4 RID: 105700 RVA: 0x00356AD6 File Offset: 0x00354CD6
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

		// Token: 0x17008E9D RID: 36509
		// (get) Token: 0x06019CE5 RID: 105701 RVA: 0x00356AE0 File Offset: 0x00354CE0
		// (set) Token: 0x06019CE6 RID: 105702 RVA: 0x00356AE9 File Offset: 0x00354CE9
		public Body Body
		{
			get
			{
				return base.GetElement<Body>(1);
			}
			set
			{
				base.SetElement<Body>(1, value);
			}
		}

		// Token: 0x06019CE7 RID: 105703 RVA: 0x00356AF3 File Offset: 0x00354CF3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Document>(deep);
		}

		// Token: 0x0400AA86 RID: 43654
		private const string tagName = "document";

		// Token: 0x0400AA87 RID: 43655
		private const byte tagNsId = 23;

		// Token: 0x0400AA88 RID: 43656
		internal const int ElementTypeIdConst = 11702;

		// Token: 0x0400AA89 RID: 43657
		private static readonly string[] eleTagNames = new string[] { "background", "body" };

		// Token: 0x0400AA8A RID: 43658
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23 };
	}
}
