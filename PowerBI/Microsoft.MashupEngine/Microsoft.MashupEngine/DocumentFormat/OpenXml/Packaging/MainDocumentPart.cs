using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002158 RID: 8536
	internal class MainDocumentPart : OpenXmlPart
	{
		// Token: 0x0600D42F RID: 54319 RVA: 0x002A34E4 File Offset: 0x002A16E4
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (MainDocumentPart._partConstraint == null)
			{
				MainDocumentPart._partConstraint = new Dictionary<string, PartConstraintRule>
				{
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/customXml",
						new PartConstraintRule("CustomXmlPart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/glossaryDocument",
						new PartConstraintRule("GlossaryDocumentPart", "application/vnd.openxmlformats-officedocument.wordprocessingml.document.glossary+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/theme",
						new PartConstraintRule("ThemePart", "application/vnd.openxmlformats-officedocument.theme+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/package/2006/relationships/metadata/thumbnail",
						new PartConstraintRule("ThumbnailPart", null, false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
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
			return MainDocumentPart._partConstraint;
		}

		// Token: 0x0600D430 RID: 54320 RVA: 0x002A383C File Offset: 0x002A1A3C
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (MainDocumentPart._dataPartReferenceConstraint == null)
			{
				MainDocumentPart._dataPartReferenceConstraint = new Dictionary<string, PartConstraintRule> { 
				{
					"http://schemas.openxmlformats.org/officeDocument/2006/relationships/video",
					new PartConstraintRule("VideoReferenceRelationship", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
				} };
			}
			return MainDocumentPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D431 RID: 54321 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal MainDocumentPart()
		{
		}

		// Token: 0x0600D432 RID: 54322 RVA: 0x002A387C File Offset: 0x002A1A7C
		internal sealed override OpenXmlPart CreatePartCore(string relationshipType)
		{
			this.ThrowIfObjectDisposed();
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			switch (relationshipType)
			{
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/customXml":
				return new CustomXmlPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/glossaryDocument":
				return new GlossaryDocumentPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/theme":
				return new ThemePart();
			case "http://schemas.openxmlformats.org/package/2006/relationships/metadata/thumbnail":
				return new ThumbnailPart();
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

		// Token: 0x0600D433 RID: 54323 RVA: 0x002A3B7C File Offset: 0x002A1D7C
		public CustomXmlPart AddCustomXmlPart(string contentType)
		{
			CustomXmlPart customXmlPart = new CustomXmlPart();
			base.InitPart<CustomXmlPart>(customXmlPart, contentType);
			return customXmlPart;
		}

		// Token: 0x0600D434 RID: 54324 RVA: 0x002A3B98 File Offset: 0x002A1D98
		public CustomXmlPart AddCustomXmlPart(CustomXmlPartType partType)
		{
			string contentType = CustomXmlPartTypeInfo.GetContentType(partType);
			string targetExtension = CustomXmlPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddCustomXmlPart(contentType);
		}

		// Token: 0x0600D435 RID: 54325 RVA: 0x002A3BCC File Offset: 0x002A1DCC
		public CustomXmlPart AddCustomXmlPart(string contentType, string id)
		{
			CustomXmlPart customXmlPart = new CustomXmlPart();
			this.InitPart<CustomXmlPart>(customXmlPart, contentType, id);
			return customXmlPart;
		}

		// Token: 0x0600D436 RID: 54326 RVA: 0x002A3BEC File Offset: 0x002A1DEC
		public CustomXmlPart AddCustomXmlPart(CustomXmlPartType partType, string id)
		{
			string contentType = CustomXmlPartTypeInfo.GetContentType(partType);
			string targetExtension = CustomXmlPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddCustomXmlPart(contentType, id);
		}

		// Token: 0x0600D437 RID: 54327 RVA: 0x002A3C24 File Offset: 0x002A1E24
		public ThumbnailPart AddThumbnailPart(string contentType)
		{
			ThumbnailPart thumbnailPart = new ThumbnailPart();
			base.InitPart<ThumbnailPart>(thumbnailPart, contentType);
			return thumbnailPart;
		}

		// Token: 0x0600D438 RID: 54328 RVA: 0x002A3C40 File Offset: 0x002A1E40
		public ThumbnailPart AddThumbnailPart(ThumbnailPartType partType)
		{
			string contentType = ThumbnailPartTypeInfo.GetContentType(partType);
			string targetExtension = ThumbnailPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddThumbnailPart(contentType);
		}

		// Token: 0x0600D439 RID: 54329 RVA: 0x002A3C74 File Offset: 0x002A1E74
		public ThumbnailPart AddThumbnailPart(string contentType, string id)
		{
			ThumbnailPart thumbnailPart = new ThumbnailPart();
			this.InitPart<ThumbnailPart>(thumbnailPart, contentType, id);
			return thumbnailPart;
		}

		// Token: 0x0600D43A RID: 54330 RVA: 0x002A3C94 File Offset: 0x002A1E94
		public ThumbnailPart AddThumbnailPart(ThumbnailPartType partType, string id)
		{
			string contentType = ThumbnailPartTypeInfo.GetContentType(partType);
			string targetExtension = ThumbnailPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddThumbnailPart(contentType, id);
		}

		// Token: 0x0600D43B RID: 54331 RVA: 0x002A3CCC File Offset: 0x002A1ECC
		public AlternativeFormatImportPart AddAlternativeFormatImportPart(string contentType)
		{
			AlternativeFormatImportPart alternativeFormatImportPart = new AlternativeFormatImportPart();
			base.InitPart<AlternativeFormatImportPart>(alternativeFormatImportPart, contentType);
			return alternativeFormatImportPart;
		}

		// Token: 0x0600D43C RID: 54332 RVA: 0x002A3CE8 File Offset: 0x002A1EE8
		public AlternativeFormatImportPart AddAlternativeFormatImportPart(AlternativeFormatImportPartType partType)
		{
			string contentType = AlternativeFormatImportPartTypeInfo.GetContentType(partType);
			string targetExtension = AlternativeFormatImportPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddAlternativeFormatImportPart(contentType);
		}

		// Token: 0x0600D43D RID: 54333 RVA: 0x002A3D1C File Offset: 0x002A1F1C
		public AlternativeFormatImportPart AddAlternativeFormatImportPart(string contentType, string id)
		{
			AlternativeFormatImportPart alternativeFormatImportPart = new AlternativeFormatImportPart();
			this.InitPart<AlternativeFormatImportPart>(alternativeFormatImportPart, contentType, id);
			return alternativeFormatImportPart;
		}

		// Token: 0x0600D43E RID: 54334 RVA: 0x002A3D3C File Offset: 0x002A1F3C
		public AlternativeFormatImportPart AddAlternativeFormatImportPart(AlternativeFormatImportPartType partType, string id)
		{
			string contentType = AlternativeFormatImportPartTypeInfo.GetContentType(partType);
			string targetExtension = AlternativeFormatImportPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddAlternativeFormatImportPart(contentType, id);
		}

		// Token: 0x0600D43F RID: 54335 RVA: 0x002A3D74 File Offset: 0x002A1F74
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(string contentType)
		{
			EmbeddedControlPersistencePart embeddedControlPersistencePart = new EmbeddedControlPersistencePart();
			base.InitPart<EmbeddedControlPersistencePart>(embeddedControlPersistencePart, contentType);
			return embeddedControlPersistencePart;
		}

		// Token: 0x0600D440 RID: 54336 RVA: 0x002A3D90 File Offset: 0x002A1F90
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(EmbeddedControlPersistencePartType partType)
		{
			string contentType = EmbeddedControlPersistencePartTypeInfo.GetContentType(partType);
			string targetExtension = EmbeddedControlPersistencePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddEmbeddedControlPersistencePart(contentType);
		}

		// Token: 0x0600D441 RID: 54337 RVA: 0x002A3DC4 File Offset: 0x002A1FC4
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(string contentType, string id)
		{
			EmbeddedControlPersistencePart embeddedControlPersistencePart = new EmbeddedControlPersistencePart();
			this.InitPart<EmbeddedControlPersistencePart>(embeddedControlPersistencePart, contentType, id);
			return embeddedControlPersistencePart;
		}

		// Token: 0x0600D442 RID: 54338 RVA: 0x002A3DE4 File Offset: 0x002A1FE4
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(EmbeddedControlPersistencePartType partType, string id)
		{
			string contentType = EmbeddedControlPersistencePartTypeInfo.GetContentType(partType);
			string targetExtension = EmbeddedControlPersistencePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddEmbeddedControlPersistencePart(contentType, id);
		}

		// Token: 0x0600D443 RID: 54339 RVA: 0x002A3E1C File Offset: 0x002A201C
		public EmbeddedObjectPart AddEmbeddedObjectPart(string contentType)
		{
			EmbeddedObjectPart embeddedObjectPart = new EmbeddedObjectPart();
			base.InitPart<EmbeddedObjectPart>(embeddedObjectPart, contentType);
			return embeddedObjectPart;
		}

		// Token: 0x0600D444 RID: 54340 RVA: 0x002A3E38 File Offset: 0x002A2038
		public EmbeddedPackagePart AddEmbeddedPackagePart(string contentType)
		{
			EmbeddedPackagePart embeddedPackagePart = new EmbeddedPackagePart();
			base.InitPart<EmbeddedPackagePart>(embeddedPackagePart, contentType);
			return embeddedPackagePart;
		}

		// Token: 0x0600D445 RID: 54341 RVA: 0x002A3E54 File Offset: 0x002A2054
		public ImagePart AddImagePart(string contentType)
		{
			ImagePart imagePart = new ImagePart();
			base.InitPart<ImagePart>(imagePart, contentType);
			return imagePart;
		}

		// Token: 0x0600D446 RID: 54342 RVA: 0x002A3E70 File Offset: 0x002A2070
		public ImagePart AddImagePart(ImagePartType partType)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType);
		}

		// Token: 0x0600D447 RID: 54343 RVA: 0x002A3EA4 File Offset: 0x002A20A4
		public ImagePart AddImagePart(string contentType, string id)
		{
			ImagePart imagePart = new ImagePart();
			this.InitPart<ImagePart>(imagePart, contentType, id);
			return imagePart;
		}

		// Token: 0x0600D448 RID: 54344 RVA: 0x002A3EC4 File Offset: 0x002A20C4
		public ImagePart AddImagePart(ImagePartType partType, string id)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType, id);
		}

		// Token: 0x0600D449 RID: 54345 RVA: 0x002A3EF9 File Offset: 0x002A20F9
		public VideoReferenceRelationship AddVideoReferenceRelationship(MediaDataPart mediaDataPart)
		{
			return base.AddDataPartReferenceRelationship<VideoReferenceRelationship>(mediaDataPart);
		}

		// Token: 0x0600D44A RID: 54346 RVA: 0x002A3F02 File Offset: 0x002A2102
		public VideoReferenceRelationship AddVideoReferenceRelationship(MediaDataPart mediaDataPart, string id)
		{
			return base.AddDataPartReferenceRelationship<VideoReferenceRelationship>(mediaDataPart, id);
		}

		// Token: 0x1700334D RID: 13133
		// (get) Token: 0x0600D44B RID: 54347 RVA: 0x002A3F0C File Offset: 0x002A210C
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument";
			}
		}

		// Token: 0x1700334E RID: 13134
		// (get) Token: 0x0600D44C RID: 54348 RVA: 0x002A3F13 File Offset: 0x002A2113
		internal sealed override string TargetPath
		{
			get
			{
				return "word";
			}
		}

		// Token: 0x1700334F RID: 13135
		// (get) Token: 0x0600D44D RID: 54349 RVA: 0x002A3F1A File Offset: 0x002A211A
		internal sealed override string TargetName
		{
			get
			{
				return "document";
			}
		}

		// Token: 0x17003350 RID: 13136
		// (get) Token: 0x0600D44E RID: 54350 RVA: 0x002A3F21 File Offset: 0x002A2121
		public IEnumerable<CustomXmlPart> CustomXmlParts
		{
			get
			{
				return base.GetPartsOfType<CustomXmlPart>();
			}
		}

		// Token: 0x17003351 RID: 13137
		// (get) Token: 0x0600D44F RID: 54351 RVA: 0x002A3F29 File Offset: 0x002A2129
		public GlossaryDocumentPart GlossaryDocumentPart
		{
			get
			{
				return base.GetSubPartOfType<GlossaryDocumentPart>();
			}
		}

		// Token: 0x17003352 RID: 13138
		// (get) Token: 0x0600D450 RID: 54352 RVA: 0x002A3F31 File Offset: 0x002A2131
		public ThemePart ThemePart
		{
			get
			{
				return base.GetSubPartOfType<ThemePart>();
			}
		}

		// Token: 0x17003353 RID: 13139
		// (get) Token: 0x0600D451 RID: 54353 RVA: 0x002A3F39 File Offset: 0x002A2139
		public ThumbnailPart ThumbnailPart
		{
			get
			{
				return base.GetSubPartOfType<ThumbnailPart>();
			}
		}

		// Token: 0x17003354 RID: 13140
		// (get) Token: 0x0600D452 RID: 54354 RVA: 0x002A3F41 File Offset: 0x002A2141
		public WordprocessingCommentsPart WordprocessingCommentsPart
		{
			get
			{
				return base.GetSubPartOfType<WordprocessingCommentsPart>();
			}
		}

		// Token: 0x17003355 RID: 13141
		// (get) Token: 0x0600D453 RID: 54355 RVA: 0x002A3F49 File Offset: 0x002A2149
		public DocumentSettingsPart DocumentSettingsPart
		{
			get
			{
				return base.GetSubPartOfType<DocumentSettingsPart>();
			}
		}

		// Token: 0x17003356 RID: 13142
		// (get) Token: 0x0600D454 RID: 54356 RVA: 0x002A3F51 File Offset: 0x002A2151
		public EndnotesPart EndnotesPart
		{
			get
			{
				return base.GetSubPartOfType<EndnotesPart>();
			}
		}

		// Token: 0x17003357 RID: 13143
		// (get) Token: 0x0600D455 RID: 54357 RVA: 0x002A3F59 File Offset: 0x002A2159
		public FontTablePart FontTablePart
		{
			get
			{
				return base.GetSubPartOfType<FontTablePart>();
			}
		}

		// Token: 0x17003358 RID: 13144
		// (get) Token: 0x0600D456 RID: 54358 RVA: 0x002A3F61 File Offset: 0x002A2161
		public FootnotesPart FootnotesPart
		{
			get
			{
				return base.GetSubPartOfType<FootnotesPart>();
			}
		}

		// Token: 0x17003359 RID: 13145
		// (get) Token: 0x0600D457 RID: 54359 RVA: 0x002A3F69 File Offset: 0x002A2169
		public NumberingDefinitionsPart NumberingDefinitionsPart
		{
			get
			{
				return base.GetSubPartOfType<NumberingDefinitionsPart>();
			}
		}

		// Token: 0x1700335A RID: 13146
		// (get) Token: 0x0600D458 RID: 54360 RVA: 0x002A3F71 File Offset: 0x002A2171
		public StyleDefinitionsPart StyleDefinitionsPart
		{
			get
			{
				return base.GetSubPartOfType<StyleDefinitionsPart>();
			}
		}

		// Token: 0x1700335B RID: 13147
		// (get) Token: 0x0600D459 RID: 54361 RVA: 0x002A3F79 File Offset: 0x002A2179
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public StylesWithEffectsPart StylesWithEffectsPart
		{
			get
			{
				return base.GetSubPartOfType<StylesWithEffectsPart>();
			}
		}

		// Token: 0x1700335C RID: 13148
		// (get) Token: 0x0600D45A RID: 54362 RVA: 0x002A3F81 File Offset: 0x002A2181
		public WebSettingsPart WebSettingsPart
		{
			get
			{
				return base.GetSubPartOfType<WebSettingsPart>();
			}
		}

		// Token: 0x1700335D RID: 13149
		// (get) Token: 0x0600D45B RID: 54363 RVA: 0x002A3F89 File Offset: 0x002A2189
		public IEnumerable<FooterPart> FooterParts
		{
			get
			{
				return base.GetPartsOfType<FooterPart>();
			}
		}

		// Token: 0x1700335E RID: 13150
		// (get) Token: 0x0600D45C RID: 54364 RVA: 0x002A3F91 File Offset: 0x002A2191
		public IEnumerable<HeaderPart> HeaderParts
		{
			get
			{
				return base.GetPartsOfType<HeaderPart>();
			}
		}

		// Token: 0x1700335F RID: 13151
		// (get) Token: 0x0600D45D RID: 54365 RVA: 0x002A3F99 File Offset: 0x002A2199
		public IEnumerable<WordprocessingPrinterSettingsPart> WordprocessingPrinterSettingsParts
		{
			get
			{
				return base.GetPartsOfType<WordprocessingPrinterSettingsPart>();
			}
		}

		// Token: 0x17003360 RID: 13152
		// (get) Token: 0x0600D45E RID: 54366 RVA: 0x002A3FA1 File Offset: 0x002A21A1
		public CustomizationPart CustomizationPart
		{
			get
			{
				return base.GetSubPartOfType<CustomizationPart>();
			}
		}

		// Token: 0x17003361 RID: 13153
		// (get) Token: 0x0600D45F RID: 54367 RVA: 0x002A3FA9 File Offset: 0x002A21A9
		public VbaProjectPart VbaProjectPart
		{
			get
			{
				return base.GetSubPartOfType<VbaProjectPart>();
			}
		}

		// Token: 0x17003362 RID: 13154
		// (get) Token: 0x0600D460 RID: 54368 RVA: 0x002A3FB1 File Offset: 0x002A21B1
		public IEnumerable<AlternativeFormatImportPart> AlternativeFormatImportParts
		{
			get
			{
				return base.GetPartsOfType<AlternativeFormatImportPart>();
			}
		}

		// Token: 0x17003363 RID: 13155
		// (get) Token: 0x0600D461 RID: 54369 RVA: 0x002A3FB9 File Offset: 0x002A21B9
		public IEnumerable<ChartPart> ChartParts
		{
			get
			{
				return base.GetPartsOfType<ChartPart>();
			}
		}

		// Token: 0x17003364 RID: 13156
		// (get) Token: 0x0600D462 RID: 54370 RVA: 0x002A3FC1 File Offset: 0x002A21C1
		public IEnumerable<DiagramColorsPart> DiagramColorsParts
		{
			get
			{
				return base.GetPartsOfType<DiagramColorsPart>();
			}
		}

		// Token: 0x17003365 RID: 13157
		// (get) Token: 0x0600D463 RID: 54371 RVA: 0x002A3FC9 File Offset: 0x002A21C9
		public IEnumerable<DiagramDataPart> DiagramDataParts
		{
			get
			{
				return base.GetPartsOfType<DiagramDataPart>();
			}
		}

		// Token: 0x17003366 RID: 13158
		// (get) Token: 0x0600D464 RID: 54372 RVA: 0x002A3FD1 File Offset: 0x002A21D1
		public IEnumerable<DiagramPersistLayoutPart> DiagramPersistLayoutParts
		{
			get
			{
				return base.GetPartsOfType<DiagramPersistLayoutPart>();
			}
		}

		// Token: 0x17003367 RID: 13159
		// (get) Token: 0x0600D465 RID: 54373 RVA: 0x002A3FD9 File Offset: 0x002A21D9
		public IEnumerable<DiagramLayoutDefinitionPart> DiagramLayoutDefinitionParts
		{
			get
			{
				return base.GetPartsOfType<DiagramLayoutDefinitionPart>();
			}
		}

		// Token: 0x17003368 RID: 13160
		// (get) Token: 0x0600D466 RID: 54374 RVA: 0x002A3FE1 File Offset: 0x002A21E1
		public IEnumerable<DiagramStylePart> DiagramStyleParts
		{
			get
			{
				return base.GetPartsOfType<DiagramStylePart>();
			}
		}

		// Token: 0x17003369 RID: 13161
		// (get) Token: 0x0600D467 RID: 54375 RVA: 0x002A3FE9 File Offset: 0x002A21E9
		public IEnumerable<EmbeddedControlPersistencePart> EmbeddedControlPersistenceParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedControlPersistencePart>();
			}
		}

		// Token: 0x1700336A RID: 13162
		// (get) Token: 0x0600D468 RID: 54376 RVA: 0x002A3FF1 File Offset: 0x002A21F1
		public IEnumerable<EmbeddedObjectPart> EmbeddedObjectParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedObjectPart>();
			}
		}

		// Token: 0x1700336B RID: 13163
		// (get) Token: 0x0600D469 RID: 54377 RVA: 0x002A3FF9 File Offset: 0x002A21F9
		public IEnumerable<EmbeddedPackagePart> EmbeddedPackageParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedPackagePart>();
			}
		}

		// Token: 0x1700336C RID: 13164
		// (get) Token: 0x0600D46A RID: 54378 RVA: 0x002A4001 File Offset: 0x002A2201
		public IEnumerable<ImagePart> ImageParts
		{
			get
			{
				return base.GetPartsOfType<ImagePart>();
			}
		}

		// Token: 0x1700336D RID: 13165
		// (get) Token: 0x0600D46B RID: 54379 RVA: 0x00002105 File Offset: 0x00000305
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700336E RID: 13166
		// (get) Token: 0x0600D46C RID: 54380 RVA: 0x002A4009 File Offset: 0x002A2209
		// (set) Token: 0x0600D46D RID: 54381 RVA: 0x002A4011 File Offset: 0x002A2211
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as Document;
			}
		}

		// Token: 0x1700336F RID: 13167
		// (get) Token: 0x0600D46E RID: 54382 RVA: 0x002A401F File Offset: 0x002A221F
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.Document;
			}
		}

		// Token: 0x17003370 RID: 13168
		// (get) Token: 0x0600D46F RID: 54383 RVA: 0x002A4027 File Offset: 0x002A2227
		// (set) Token: 0x0600D470 RID: 54384 RVA: 0x002A3296 File Offset: 0x002A1496
		public Document Document
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<Document>();
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

		// Token: 0x040069DE RID: 27102
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument";

		// Token: 0x040069DF RID: 27103
		internal const string TargetPathConstant = "word";

		// Token: 0x040069E0 RID: 27104
		internal const string TargetNameConstant = "document";

		// Token: 0x040069E1 RID: 27105
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x040069E2 RID: 27106
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x040069E3 RID: 27107
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Document _rootEle;
	}
}
