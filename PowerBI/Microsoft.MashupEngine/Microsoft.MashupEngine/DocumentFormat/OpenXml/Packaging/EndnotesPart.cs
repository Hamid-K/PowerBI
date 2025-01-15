using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200215E RID: 8542
	internal class EndnotesPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D4D2 RID: 54482 RVA: 0x002A49E8 File Offset: 0x002A2BE8
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (EndnotesPart._partConstraint == null)
			{
				EndnotesPart._partConstraint = new Dictionary<string, PartConstraintRule>
				{
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
			return EndnotesPart._partConstraint;
		}

		// Token: 0x0600D4D3 RID: 54483 RVA: 0x002A4B3C File Offset: 0x002A2D3C
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (EndnotesPart._dataPartReferenceConstraint == null)
			{
				EndnotesPart._dataPartReferenceConstraint = new Dictionary<string, PartConstraintRule> { 
				{
					"http://schemas.openxmlformats.org/officeDocument/2006/relationships/video",
					new PartConstraintRule("VideoReferenceRelationship", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
				} };
			}
			return EndnotesPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D4D4 RID: 54484 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal EndnotesPart()
		{
		}

		// Token: 0x0600D4D5 RID: 54485 RVA: 0x002A4B7C File Offset: 0x002A2D7C
		internal sealed override OpenXmlPart CreatePartCore(string relationshipType)
		{
			this.ThrowIfObjectDisposed();
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			switch (relationshipType)
			{
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

		// Token: 0x0600D4D6 RID: 54486 RVA: 0x002A4CD8 File Offset: 0x002A2ED8
		public AlternativeFormatImportPart AddAlternativeFormatImportPart(string contentType)
		{
			AlternativeFormatImportPart alternativeFormatImportPart = new AlternativeFormatImportPart();
			base.InitPart<AlternativeFormatImportPart>(alternativeFormatImportPart, contentType);
			return alternativeFormatImportPart;
		}

		// Token: 0x0600D4D7 RID: 54487 RVA: 0x002A4CF4 File Offset: 0x002A2EF4
		public AlternativeFormatImportPart AddAlternativeFormatImportPart(AlternativeFormatImportPartType partType)
		{
			string contentType = AlternativeFormatImportPartTypeInfo.GetContentType(partType);
			string targetExtension = AlternativeFormatImportPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddAlternativeFormatImportPart(contentType);
		}

		// Token: 0x0600D4D8 RID: 54488 RVA: 0x002A4D28 File Offset: 0x002A2F28
		public AlternativeFormatImportPart AddAlternativeFormatImportPart(string contentType, string id)
		{
			AlternativeFormatImportPart alternativeFormatImportPart = new AlternativeFormatImportPart();
			this.InitPart<AlternativeFormatImportPart>(alternativeFormatImportPart, contentType, id);
			return alternativeFormatImportPart;
		}

		// Token: 0x0600D4D9 RID: 54489 RVA: 0x002A4D48 File Offset: 0x002A2F48
		public AlternativeFormatImportPart AddAlternativeFormatImportPart(AlternativeFormatImportPartType partType, string id)
		{
			string contentType = AlternativeFormatImportPartTypeInfo.GetContentType(partType);
			string targetExtension = AlternativeFormatImportPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddAlternativeFormatImportPart(contentType, id);
		}

		// Token: 0x0600D4DA RID: 54490 RVA: 0x002A4D80 File Offset: 0x002A2F80
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(string contentType)
		{
			EmbeddedControlPersistencePart embeddedControlPersistencePart = new EmbeddedControlPersistencePart();
			base.InitPart<EmbeddedControlPersistencePart>(embeddedControlPersistencePart, contentType);
			return embeddedControlPersistencePart;
		}

		// Token: 0x0600D4DB RID: 54491 RVA: 0x002A4D9C File Offset: 0x002A2F9C
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(EmbeddedControlPersistencePartType partType)
		{
			string contentType = EmbeddedControlPersistencePartTypeInfo.GetContentType(partType);
			string targetExtension = EmbeddedControlPersistencePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddEmbeddedControlPersistencePart(contentType);
		}

		// Token: 0x0600D4DC RID: 54492 RVA: 0x002A4DD0 File Offset: 0x002A2FD0
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(string contentType, string id)
		{
			EmbeddedControlPersistencePart embeddedControlPersistencePart = new EmbeddedControlPersistencePart();
			this.InitPart<EmbeddedControlPersistencePart>(embeddedControlPersistencePart, contentType, id);
			return embeddedControlPersistencePart;
		}

		// Token: 0x0600D4DD RID: 54493 RVA: 0x002A4DF0 File Offset: 0x002A2FF0
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(EmbeddedControlPersistencePartType partType, string id)
		{
			string contentType = EmbeddedControlPersistencePartTypeInfo.GetContentType(partType);
			string targetExtension = EmbeddedControlPersistencePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddEmbeddedControlPersistencePart(contentType, id);
		}

		// Token: 0x0600D4DE RID: 54494 RVA: 0x002A4E28 File Offset: 0x002A3028
		public EmbeddedObjectPart AddEmbeddedObjectPart(string contentType)
		{
			EmbeddedObjectPart embeddedObjectPart = new EmbeddedObjectPart();
			base.InitPart<EmbeddedObjectPart>(embeddedObjectPart, contentType);
			return embeddedObjectPart;
		}

		// Token: 0x0600D4DF RID: 54495 RVA: 0x002A4E44 File Offset: 0x002A3044
		public EmbeddedPackagePart AddEmbeddedPackagePart(string contentType)
		{
			EmbeddedPackagePart embeddedPackagePart = new EmbeddedPackagePart();
			base.InitPart<EmbeddedPackagePart>(embeddedPackagePart, contentType);
			return embeddedPackagePart;
		}

		// Token: 0x0600D4E0 RID: 54496 RVA: 0x002A4E60 File Offset: 0x002A3060
		public ImagePart AddImagePart(string contentType)
		{
			ImagePart imagePart = new ImagePart();
			base.InitPart<ImagePart>(imagePart, contentType);
			return imagePart;
		}

		// Token: 0x0600D4E1 RID: 54497 RVA: 0x002A4E7C File Offset: 0x002A307C
		public ImagePart AddImagePart(ImagePartType partType)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType);
		}

		// Token: 0x0600D4E2 RID: 54498 RVA: 0x002A4EB0 File Offset: 0x002A30B0
		public ImagePart AddImagePart(string contentType, string id)
		{
			ImagePart imagePart = new ImagePart();
			this.InitPart<ImagePart>(imagePart, contentType, id);
			return imagePart;
		}

		// Token: 0x0600D4E3 RID: 54499 RVA: 0x002A4ED0 File Offset: 0x002A30D0
		public ImagePart AddImagePart(ImagePartType partType, string id)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType, id);
		}

		// Token: 0x0600D4E4 RID: 54500 RVA: 0x002A3EF9 File Offset: 0x002A20F9
		public VideoReferenceRelationship AddVideoReferenceRelationship(MediaDataPart mediaDataPart)
		{
			return base.AddDataPartReferenceRelationship<VideoReferenceRelationship>(mediaDataPart);
		}

		// Token: 0x0600D4E5 RID: 54501 RVA: 0x002A3F02 File Offset: 0x002A2102
		public VideoReferenceRelationship AddVideoReferenceRelationship(MediaDataPart mediaDataPart, string id)
		{
			return base.AddDataPartReferenceRelationship<VideoReferenceRelationship>(mediaDataPart, id);
		}

		// Token: 0x170033A2 RID: 13218
		// (get) Token: 0x0600D4E6 RID: 54502 RVA: 0x002A4F05 File Offset: 0x002A3105
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/endnotes";
			}
		}

		// Token: 0x170033A3 RID: 13219
		// (get) Token: 0x0600D4E7 RID: 54503 RVA: 0x002A4F0C File Offset: 0x002A310C
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.wordprocessingml.endnotes+xml";
			}
		}

		// Token: 0x170033A4 RID: 13220
		// (get) Token: 0x0600D4E8 RID: 54504 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x170033A5 RID: 13221
		// (get) Token: 0x0600D4E9 RID: 54505 RVA: 0x002A4F13 File Offset: 0x002A3113
		internal sealed override string TargetName
		{
			get
			{
				return "endnotes";
			}
		}

		// Token: 0x170033A6 RID: 13222
		// (get) Token: 0x0600D4EA RID: 54506 RVA: 0x002A3FB1 File Offset: 0x002A21B1
		public IEnumerable<AlternativeFormatImportPart> AlternativeFormatImportParts
		{
			get
			{
				return base.GetPartsOfType<AlternativeFormatImportPart>();
			}
		}

		// Token: 0x170033A7 RID: 13223
		// (get) Token: 0x0600D4EB RID: 54507 RVA: 0x002A3FB9 File Offset: 0x002A21B9
		public IEnumerable<ChartPart> ChartParts
		{
			get
			{
				return base.GetPartsOfType<ChartPart>();
			}
		}

		// Token: 0x170033A8 RID: 13224
		// (get) Token: 0x0600D4EC RID: 54508 RVA: 0x002A3FC1 File Offset: 0x002A21C1
		public IEnumerable<DiagramColorsPart> DiagramColorsParts
		{
			get
			{
				return base.GetPartsOfType<DiagramColorsPart>();
			}
		}

		// Token: 0x170033A9 RID: 13225
		// (get) Token: 0x0600D4ED RID: 54509 RVA: 0x002A3FC9 File Offset: 0x002A21C9
		public IEnumerable<DiagramDataPart> DiagramDataParts
		{
			get
			{
				return base.GetPartsOfType<DiagramDataPart>();
			}
		}

		// Token: 0x170033AA RID: 13226
		// (get) Token: 0x0600D4EE RID: 54510 RVA: 0x002A3FD1 File Offset: 0x002A21D1
		public IEnumerable<DiagramPersistLayoutPart> DiagramPersistLayoutParts
		{
			get
			{
				return base.GetPartsOfType<DiagramPersistLayoutPart>();
			}
		}

		// Token: 0x170033AB RID: 13227
		// (get) Token: 0x0600D4EF RID: 54511 RVA: 0x002A3FD9 File Offset: 0x002A21D9
		public IEnumerable<DiagramLayoutDefinitionPart> DiagramLayoutDefinitionParts
		{
			get
			{
				return base.GetPartsOfType<DiagramLayoutDefinitionPart>();
			}
		}

		// Token: 0x170033AC RID: 13228
		// (get) Token: 0x0600D4F0 RID: 54512 RVA: 0x002A3FE1 File Offset: 0x002A21E1
		public IEnumerable<DiagramStylePart> DiagramStyleParts
		{
			get
			{
				return base.GetPartsOfType<DiagramStylePart>();
			}
		}

		// Token: 0x170033AD RID: 13229
		// (get) Token: 0x0600D4F1 RID: 54513 RVA: 0x002A3FE9 File Offset: 0x002A21E9
		public IEnumerable<EmbeddedControlPersistencePart> EmbeddedControlPersistenceParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedControlPersistencePart>();
			}
		}

		// Token: 0x170033AE RID: 13230
		// (get) Token: 0x0600D4F2 RID: 54514 RVA: 0x002A3FF1 File Offset: 0x002A21F1
		public IEnumerable<EmbeddedObjectPart> EmbeddedObjectParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedObjectPart>();
			}
		}

		// Token: 0x170033AF RID: 13231
		// (get) Token: 0x0600D4F3 RID: 54515 RVA: 0x002A3FF9 File Offset: 0x002A21F9
		public IEnumerable<EmbeddedPackagePart> EmbeddedPackageParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedPackagePart>();
			}
		}

		// Token: 0x170033B0 RID: 13232
		// (get) Token: 0x0600D4F4 RID: 54516 RVA: 0x002A4001 File Offset: 0x002A2201
		public IEnumerable<ImagePart> ImageParts
		{
			get
			{
				return base.GetPartsOfType<ImagePart>();
			}
		}

		// Token: 0x170033B1 RID: 13233
		// (get) Token: 0x0600D4F5 RID: 54517 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170033B2 RID: 13234
		// (get) Token: 0x0600D4F6 RID: 54518 RVA: 0x002A4F1A File Offset: 0x002A311A
		// (set) Token: 0x0600D4F7 RID: 54519 RVA: 0x002A4F22 File Offset: 0x002A3122
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as Endnotes;
			}
		}

		// Token: 0x170033B3 RID: 13235
		// (get) Token: 0x0600D4F8 RID: 54520 RVA: 0x002A4F30 File Offset: 0x002A3130
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.Endnotes;
			}
		}

		// Token: 0x170033B4 RID: 13236
		// (get) Token: 0x0600D4F9 RID: 54521 RVA: 0x002A4F38 File Offset: 0x002A3138
		// (set) Token: 0x0600D4FA RID: 54522 RVA: 0x002A3296 File Offset: 0x002A1496
		public Endnotes Endnotes
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<Endnotes>();
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

		// Token: 0x04006A06 RID: 27142
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/endnotes";

		// Token: 0x04006A07 RID: 27143
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.wordprocessingml.endnotes+xml";

		// Token: 0x04006A08 RID: 27144
		internal const string TargetPathConstant = ".";

		// Token: 0x04006A09 RID: 27145
		internal const string TargetNameConstant = "endnotes";

		// Token: 0x04006A0A RID: 27146
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006A0B RID: 27147
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006A0C RID: 27148
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Endnotes _rootEle;
	}
}
