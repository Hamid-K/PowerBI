using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002162 RID: 8546
	internal class GlossaryDocumentPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D560 RID: 54624 RVA: 0x002A5BC4 File Offset: 0x002A3DC4
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (GlossaryDocumentPart._partConstraint == null)
			{
				GlossaryDocumentPart._partConstraint = new Dictionary<string, PartConstraintRule>
				{
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/comments",
						new PartConstraintRule("WordprocessingCommentsPart", "application/vnd.openxmlformats-officedocument.wordprocessingml.comments+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/settings",
						new PartConstraintRule("DocumentSettingsPart", "application/vnd.openxmlformats-officedocument.wordprocessingml.settings+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/endnotes",
						new PartConstraintRule("EndnotesPart", "application/vnd.openxmlformats-officedocument.wordprocessingml.endnotes+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/fontTable",
						new PartConstraintRule("FontTablePart", "application/vnd.openxmlformats-officedocument.wordprocessingml.fontTable+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/footnotes",
						new PartConstraintRule("FootnotesPart", "application/vnd.openxmlformats-officedocument.wordprocessingml.footnotes+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/numbering",
						new PartConstraintRule("NumberingDefinitionsPart", "application/vnd.openxmlformats-officedocument.wordprocessingml.numbering+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles",
						new PartConstraintRule("StyleDefinitionsPart", "application/vnd.openxmlformats-officedocument.wordprocessingml.styles+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.microsoft.com/office/2007/relationships/stylesWithEffects",
						new PartConstraintRule("StylesWithEffectsPart", "application/vnd.ms-word.stylesWithEffects+xml", false, false, FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/webSettings",
						new PartConstraintRule("WebSettingsPart", "application/vnd.openxmlformats-officedocument.wordprocessingml.webSettings+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/footer",
						new PartConstraintRule("FooterPart", "application/vnd.openxmlformats-officedocument.wordprocessingml.footer+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/header",
						new PartConstraintRule("HeaderPart", "application/vnd.openxmlformats-officedocument.wordprocessingml.header+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/printerSettings",
						new PartConstraintRule("WordprocessingPrinterSettingsPart", "application/vnd.openxmlformats-officedocument.wordprocessingml.printerSettings", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.microsoft.com/office/2006/relationships/keyMapCustomizations",
						new PartConstraintRule("CustomizationPart", "application/vnd.ms-word.keyMapCustomizations+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.microsoft.com/office/2006/relationships/vbaProject",
						new PartConstraintRule("VbaProjectPart", "application/vnd.ms-office.vbaProject", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/aFChunk",
						new PartConstraintRule("AlternativeFormatImportPart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/chart",
						new PartConstraintRule("ChartPart", "application/vnd.openxmlformats-officedocument.drawingml.chart+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramColors",
						new PartConstraintRule("DiagramColorsPart", "application/vnd.openxmlformats-officedocument.drawingml.diagramColors+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramData",
						new PartConstraintRule("DiagramDataPart", "application/vnd.openxmlformats-officedocument.drawingml.diagramData+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.microsoft.com/office/2007/relationships/diagramDrawing",
						new PartConstraintRule("DiagramPersistLayoutPart", "application/vnd.ms-office.drawingml.diagramDrawing+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramLayout",
						new PartConstraintRule("DiagramLayoutDefinitionPart", "application/vnd.openxmlformats-officedocument.drawingml.diagramLayout+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramQuickStyle",
						new PartConstraintRule("DiagramStylePart", "application/vnd.openxmlformats-officedocument.drawingml.diagramStyle+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/control",
						new PartConstraintRule("EmbeddedControlPersistencePart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/oleObject",
						new PartConstraintRule("EmbeddedObjectPart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/package",
						new PartConstraintRule("EmbeddedPackagePart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/image",
						new PartConstraintRule("ImagePart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					}
				};
			}
			return GlossaryDocumentPart._partConstraint;
		}

		// Token: 0x0600D561 RID: 54625 RVA: 0x002A5EB0 File Offset: 0x002A40B0
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (GlossaryDocumentPart._dataPartReferenceConstraint == null)
			{
				GlossaryDocumentPart._dataPartReferenceConstraint = new Dictionary<string, PartConstraintRule> { 
				{
					"http://schemas.openxmlformats.org/officeDocument/2006/relationships/video",
					new PartConstraintRule("VideoReferenceRelationship", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
				} };
			}
			return GlossaryDocumentPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D562 RID: 54626 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal GlossaryDocumentPart()
		{
		}

		// Token: 0x0600D563 RID: 54627 RVA: 0x002A5EF0 File Offset: 0x002A40F0
		internal sealed override OpenXmlPart CreatePartCore(string relationshipType)
		{
			this.ThrowIfObjectDisposed();
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			switch (relationshipType)
			{
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/comments":
				return new WordprocessingCommentsPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/settings":
				return new DocumentSettingsPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/endnotes":
				return new EndnotesPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/fontTable":
				return new FontTablePart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/footnotes":
				return new FootnotesPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/numbering":
				return new NumberingDefinitionsPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles":
				return new StyleDefinitionsPart();
			case "http://schemas.microsoft.com/office/2007/relationships/stylesWithEffects":
				return new StylesWithEffectsPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/webSettings":
				return new WebSettingsPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/footer":
				return new FooterPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/header":
				return new HeaderPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/printerSettings":
				return new WordprocessingPrinterSettingsPart();
			case "http://schemas.microsoft.com/office/2006/relationships/keyMapCustomizations":
				return new CustomizationPart();
			case "http://schemas.microsoft.com/office/2006/relationships/vbaProject":
				return new VbaProjectPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/aFChunk":
				return new AlternativeFormatImportPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/chart":
				return new ChartPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramColors":
				return new DiagramColorsPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramData":
				return new DiagramDataPart();
			case "http://schemas.microsoft.com/office/2007/relationships/diagramDrawing":
				return new DiagramPersistLayoutPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramLayout":
				return new DiagramLayoutDefinitionPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramQuickStyle":
				return new DiagramStylePart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/control":
				return new EmbeddedControlPersistencePart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/oleObject":
				return new EmbeddedObjectPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/package":
				return new EmbeddedPackagePart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image":
				return new ImagePart();
			}
			throw new ArgumentOutOfRangeException("relationshipType");
		}

		// Token: 0x0600D564 RID: 54628 RVA: 0x002A6194 File Offset: 0x002A4394
		public AlternativeFormatImportPart AddAlternativeFormatImportPart(string contentType)
		{
			AlternativeFormatImportPart alternativeFormatImportPart = new AlternativeFormatImportPart();
			base.InitPart<AlternativeFormatImportPart>(alternativeFormatImportPart, contentType);
			return alternativeFormatImportPart;
		}

		// Token: 0x0600D565 RID: 54629 RVA: 0x002A61B0 File Offset: 0x002A43B0
		public AlternativeFormatImportPart AddAlternativeFormatImportPart(AlternativeFormatImportPartType partType)
		{
			string contentType = AlternativeFormatImportPartTypeInfo.GetContentType(partType);
			string targetExtension = AlternativeFormatImportPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddAlternativeFormatImportPart(contentType);
		}

		// Token: 0x0600D566 RID: 54630 RVA: 0x002A61E4 File Offset: 0x002A43E4
		public AlternativeFormatImportPart AddAlternativeFormatImportPart(string contentType, string id)
		{
			AlternativeFormatImportPart alternativeFormatImportPart = new AlternativeFormatImportPart();
			this.InitPart<AlternativeFormatImportPart>(alternativeFormatImportPart, contentType, id);
			return alternativeFormatImportPart;
		}

		// Token: 0x0600D567 RID: 54631 RVA: 0x002A6204 File Offset: 0x002A4404
		public AlternativeFormatImportPart AddAlternativeFormatImportPart(AlternativeFormatImportPartType partType, string id)
		{
			string contentType = AlternativeFormatImportPartTypeInfo.GetContentType(partType);
			string targetExtension = AlternativeFormatImportPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddAlternativeFormatImportPart(contentType, id);
		}

		// Token: 0x0600D568 RID: 54632 RVA: 0x002A623C File Offset: 0x002A443C
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(string contentType)
		{
			EmbeddedControlPersistencePart embeddedControlPersistencePart = new EmbeddedControlPersistencePart();
			base.InitPart<EmbeddedControlPersistencePart>(embeddedControlPersistencePart, contentType);
			return embeddedControlPersistencePart;
		}

		// Token: 0x0600D569 RID: 54633 RVA: 0x002A6258 File Offset: 0x002A4458
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(EmbeddedControlPersistencePartType partType)
		{
			string contentType = EmbeddedControlPersistencePartTypeInfo.GetContentType(partType);
			string targetExtension = EmbeddedControlPersistencePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddEmbeddedControlPersistencePart(contentType);
		}

		// Token: 0x0600D56A RID: 54634 RVA: 0x002A628C File Offset: 0x002A448C
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(string contentType, string id)
		{
			EmbeddedControlPersistencePart embeddedControlPersistencePart = new EmbeddedControlPersistencePart();
			this.InitPart<EmbeddedControlPersistencePart>(embeddedControlPersistencePart, contentType, id);
			return embeddedControlPersistencePart;
		}

		// Token: 0x0600D56B RID: 54635 RVA: 0x002A62AC File Offset: 0x002A44AC
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(EmbeddedControlPersistencePartType partType, string id)
		{
			string contentType = EmbeddedControlPersistencePartTypeInfo.GetContentType(partType);
			string targetExtension = EmbeddedControlPersistencePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddEmbeddedControlPersistencePart(contentType, id);
		}

		// Token: 0x0600D56C RID: 54636 RVA: 0x002A62E4 File Offset: 0x002A44E4
		public EmbeddedObjectPart AddEmbeddedObjectPart(string contentType)
		{
			EmbeddedObjectPart embeddedObjectPart = new EmbeddedObjectPart();
			base.InitPart<EmbeddedObjectPart>(embeddedObjectPart, contentType);
			return embeddedObjectPart;
		}

		// Token: 0x0600D56D RID: 54637 RVA: 0x002A6300 File Offset: 0x002A4500
		public EmbeddedPackagePart AddEmbeddedPackagePart(string contentType)
		{
			EmbeddedPackagePart embeddedPackagePart = new EmbeddedPackagePart();
			base.InitPart<EmbeddedPackagePart>(embeddedPackagePart, contentType);
			return embeddedPackagePart;
		}

		// Token: 0x0600D56E RID: 54638 RVA: 0x002A631C File Offset: 0x002A451C
		public ImagePart AddImagePart(string contentType)
		{
			ImagePart imagePart = new ImagePart();
			base.InitPart<ImagePart>(imagePart, contentType);
			return imagePart;
		}

		// Token: 0x0600D56F RID: 54639 RVA: 0x002A6338 File Offset: 0x002A4538
		public ImagePart AddImagePart(ImagePartType partType)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType);
		}

		// Token: 0x0600D570 RID: 54640 RVA: 0x002A636C File Offset: 0x002A456C
		public ImagePart AddImagePart(string contentType, string id)
		{
			ImagePart imagePart = new ImagePart();
			this.InitPart<ImagePart>(imagePart, contentType, id);
			return imagePart;
		}

		// Token: 0x0600D571 RID: 54641 RVA: 0x002A638C File Offset: 0x002A458C
		public ImagePart AddImagePart(ImagePartType partType, string id)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType, id);
		}

		// Token: 0x0600D572 RID: 54642 RVA: 0x002A3EF9 File Offset: 0x002A20F9
		public VideoReferenceRelationship AddVideoReferenceRelationship(MediaDataPart mediaDataPart)
		{
			return base.AddDataPartReferenceRelationship<VideoReferenceRelationship>(mediaDataPart);
		}

		// Token: 0x0600D573 RID: 54643 RVA: 0x002A3F02 File Offset: 0x002A2102
		public VideoReferenceRelationship AddVideoReferenceRelationship(MediaDataPart mediaDataPart, string id)
		{
			return base.AddDataPartReferenceRelationship<VideoReferenceRelationship>(mediaDataPart, id);
		}

		// Token: 0x170033E4 RID: 13284
		// (get) Token: 0x0600D574 RID: 54644 RVA: 0x002A63C1 File Offset: 0x002A45C1
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/glossaryDocument";
			}
		}

		// Token: 0x170033E5 RID: 13285
		// (get) Token: 0x0600D575 RID: 54645 RVA: 0x002A63C8 File Offset: 0x002A45C8
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.wordprocessingml.document.glossary+xml";
			}
		}

		// Token: 0x170033E6 RID: 13286
		// (get) Token: 0x0600D576 RID: 54646 RVA: 0x002A63CF File Offset: 0x002A45CF
		internal sealed override string TargetPath
		{
			get
			{
				return "glossary";
			}
		}

		// Token: 0x170033E7 RID: 13287
		// (get) Token: 0x0600D577 RID: 54647 RVA: 0x002A3F1A File Offset: 0x002A211A
		internal sealed override string TargetName
		{
			get
			{
				return "document";
			}
		}

		// Token: 0x170033E8 RID: 13288
		// (get) Token: 0x0600D578 RID: 54648 RVA: 0x002A3F41 File Offset: 0x002A2141
		public WordprocessingCommentsPart WordprocessingCommentsPart
		{
			get
			{
				return base.GetSubPartOfType<WordprocessingCommentsPart>();
			}
		}

		// Token: 0x170033E9 RID: 13289
		// (get) Token: 0x0600D579 RID: 54649 RVA: 0x002A3F49 File Offset: 0x002A2149
		public DocumentSettingsPart DocumentSettingsPart
		{
			get
			{
				return base.GetSubPartOfType<DocumentSettingsPart>();
			}
		}

		// Token: 0x170033EA RID: 13290
		// (get) Token: 0x0600D57A RID: 54650 RVA: 0x002A3F51 File Offset: 0x002A2151
		public EndnotesPart EndnotesPart
		{
			get
			{
				return base.GetSubPartOfType<EndnotesPart>();
			}
		}

		// Token: 0x170033EB RID: 13291
		// (get) Token: 0x0600D57B RID: 54651 RVA: 0x002A3F59 File Offset: 0x002A2159
		public FontTablePart FontTablePart
		{
			get
			{
				return base.GetSubPartOfType<FontTablePart>();
			}
		}

		// Token: 0x170033EC RID: 13292
		// (get) Token: 0x0600D57C RID: 54652 RVA: 0x002A3F61 File Offset: 0x002A2161
		public FootnotesPart FootnotesPart
		{
			get
			{
				return base.GetSubPartOfType<FootnotesPart>();
			}
		}

		// Token: 0x170033ED RID: 13293
		// (get) Token: 0x0600D57D RID: 54653 RVA: 0x002A3F69 File Offset: 0x002A2169
		public NumberingDefinitionsPart NumberingDefinitionsPart
		{
			get
			{
				return base.GetSubPartOfType<NumberingDefinitionsPart>();
			}
		}

		// Token: 0x170033EE RID: 13294
		// (get) Token: 0x0600D57E RID: 54654 RVA: 0x002A3F71 File Offset: 0x002A2171
		public StyleDefinitionsPart StyleDefinitionsPart
		{
			get
			{
				return base.GetSubPartOfType<StyleDefinitionsPart>();
			}
		}

		// Token: 0x170033EF RID: 13295
		// (get) Token: 0x0600D57F RID: 54655 RVA: 0x002A3F79 File Offset: 0x002A2179
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public StylesWithEffectsPart StylesWithEffectsPart
		{
			get
			{
				return base.GetSubPartOfType<StylesWithEffectsPart>();
			}
		}

		// Token: 0x170033F0 RID: 13296
		// (get) Token: 0x0600D580 RID: 54656 RVA: 0x002A3F81 File Offset: 0x002A2181
		public WebSettingsPart WebSettingsPart
		{
			get
			{
				return base.GetSubPartOfType<WebSettingsPart>();
			}
		}

		// Token: 0x170033F1 RID: 13297
		// (get) Token: 0x0600D581 RID: 54657 RVA: 0x002A3F89 File Offset: 0x002A2189
		public IEnumerable<FooterPart> FooterParts
		{
			get
			{
				return base.GetPartsOfType<FooterPart>();
			}
		}

		// Token: 0x170033F2 RID: 13298
		// (get) Token: 0x0600D582 RID: 54658 RVA: 0x002A3F91 File Offset: 0x002A2191
		public IEnumerable<HeaderPart> HeaderParts
		{
			get
			{
				return base.GetPartsOfType<HeaderPart>();
			}
		}

		// Token: 0x170033F3 RID: 13299
		// (get) Token: 0x0600D583 RID: 54659 RVA: 0x002A3F99 File Offset: 0x002A2199
		public IEnumerable<WordprocessingPrinterSettingsPart> WordprocessingPrinterSettingsParts
		{
			get
			{
				return base.GetPartsOfType<WordprocessingPrinterSettingsPart>();
			}
		}

		// Token: 0x170033F4 RID: 13300
		// (get) Token: 0x0600D584 RID: 54660 RVA: 0x002A3FA1 File Offset: 0x002A21A1
		public CustomizationPart CustomizationPart
		{
			get
			{
				return base.GetSubPartOfType<CustomizationPart>();
			}
		}

		// Token: 0x170033F5 RID: 13301
		// (get) Token: 0x0600D585 RID: 54661 RVA: 0x002A3FA9 File Offset: 0x002A21A9
		public VbaProjectPart VbaProjectPart
		{
			get
			{
				return base.GetSubPartOfType<VbaProjectPart>();
			}
		}

		// Token: 0x170033F6 RID: 13302
		// (get) Token: 0x0600D586 RID: 54662 RVA: 0x002A3FB1 File Offset: 0x002A21B1
		public IEnumerable<AlternativeFormatImportPart> AlternativeFormatImportParts
		{
			get
			{
				return base.GetPartsOfType<AlternativeFormatImportPart>();
			}
		}

		// Token: 0x170033F7 RID: 13303
		// (get) Token: 0x0600D587 RID: 54663 RVA: 0x002A3FB9 File Offset: 0x002A21B9
		public IEnumerable<ChartPart> ChartParts
		{
			get
			{
				return base.GetPartsOfType<ChartPart>();
			}
		}

		// Token: 0x170033F8 RID: 13304
		// (get) Token: 0x0600D588 RID: 54664 RVA: 0x002A3FC1 File Offset: 0x002A21C1
		public IEnumerable<DiagramColorsPart> DiagramColorsParts
		{
			get
			{
				return base.GetPartsOfType<DiagramColorsPart>();
			}
		}

		// Token: 0x170033F9 RID: 13305
		// (get) Token: 0x0600D589 RID: 54665 RVA: 0x002A3FC9 File Offset: 0x002A21C9
		public IEnumerable<DiagramDataPart> DiagramDataParts
		{
			get
			{
				return base.GetPartsOfType<DiagramDataPart>();
			}
		}

		// Token: 0x170033FA RID: 13306
		// (get) Token: 0x0600D58A RID: 54666 RVA: 0x002A3FD1 File Offset: 0x002A21D1
		public IEnumerable<DiagramPersistLayoutPart> DiagramPersistLayoutParts
		{
			get
			{
				return base.GetPartsOfType<DiagramPersistLayoutPart>();
			}
		}

		// Token: 0x170033FB RID: 13307
		// (get) Token: 0x0600D58B RID: 54667 RVA: 0x002A3FD9 File Offset: 0x002A21D9
		public IEnumerable<DiagramLayoutDefinitionPart> DiagramLayoutDefinitionParts
		{
			get
			{
				return base.GetPartsOfType<DiagramLayoutDefinitionPart>();
			}
		}

		// Token: 0x170033FC RID: 13308
		// (get) Token: 0x0600D58C RID: 54668 RVA: 0x002A3FE1 File Offset: 0x002A21E1
		public IEnumerable<DiagramStylePart> DiagramStyleParts
		{
			get
			{
				return base.GetPartsOfType<DiagramStylePart>();
			}
		}

		// Token: 0x170033FD RID: 13309
		// (get) Token: 0x0600D58D RID: 54669 RVA: 0x002A3FE9 File Offset: 0x002A21E9
		public IEnumerable<EmbeddedControlPersistencePart> EmbeddedControlPersistenceParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedControlPersistencePart>();
			}
		}

		// Token: 0x170033FE RID: 13310
		// (get) Token: 0x0600D58E RID: 54670 RVA: 0x002A3FF1 File Offset: 0x002A21F1
		public IEnumerable<EmbeddedObjectPart> EmbeddedObjectParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedObjectPart>();
			}
		}

		// Token: 0x170033FF RID: 13311
		// (get) Token: 0x0600D58F RID: 54671 RVA: 0x002A3FF9 File Offset: 0x002A21F9
		public IEnumerable<EmbeddedPackagePart> EmbeddedPackageParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedPackagePart>();
			}
		}

		// Token: 0x17003400 RID: 13312
		// (get) Token: 0x0600D590 RID: 54672 RVA: 0x002A4001 File Offset: 0x002A2201
		public IEnumerable<ImagePart> ImageParts
		{
			get
			{
				return base.GetPartsOfType<ImagePart>();
			}
		}

		// Token: 0x17003401 RID: 13313
		// (get) Token: 0x0600D591 RID: 54673 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003402 RID: 13314
		// (get) Token: 0x0600D592 RID: 54674 RVA: 0x002A63D6 File Offset: 0x002A45D6
		// (set) Token: 0x0600D593 RID: 54675 RVA: 0x002A63DE File Offset: 0x002A45DE
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as GlossaryDocument;
			}
		}

		// Token: 0x17003403 RID: 13315
		// (get) Token: 0x0600D594 RID: 54676 RVA: 0x002A63EC File Offset: 0x002A45EC
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.GlossaryDocument;
			}
		}

		// Token: 0x17003404 RID: 13316
		// (get) Token: 0x0600D595 RID: 54677 RVA: 0x002A63F4 File Offset: 0x002A45F4
		// (set) Token: 0x0600D596 RID: 54678 RVA: 0x002A3296 File Offset: 0x002A1496
		public GlossaryDocument GlossaryDocument
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<GlossaryDocument>();
				}
				return this._rootEle;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.SetDomTree(value);
			}
		}

		// Token: 0x04006A22 RID: 27170
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/glossaryDocument";

		// Token: 0x04006A23 RID: 27171
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.wordprocessingml.document.glossary+xml";

		// Token: 0x04006A24 RID: 27172
		internal const string TargetPathConstant = "glossary";

		// Token: 0x04006A25 RID: 27173
		internal const string TargetNameConstant = "document";

		// Token: 0x04006A26 RID: 27174
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006A27 RID: 27175
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006A28 RID: 27176
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private GlossaryDocument _rootEle;
	}
}
