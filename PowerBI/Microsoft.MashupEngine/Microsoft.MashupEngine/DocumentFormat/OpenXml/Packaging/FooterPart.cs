using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002160 RID: 8544
	internal class FooterPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D50E RID: 54542 RVA: 0x002A50F4 File Offset: 0x002A32F4
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (FooterPart._partConstraint == null)
			{
				FooterPart._partConstraint = new Dictionary<string, PartConstraintRule>
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
			return FooterPart._partConstraint;
		}

		// Token: 0x0600D50F RID: 54543 RVA: 0x002A5248 File Offset: 0x002A3448
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (FooterPart._dataPartReferenceConstraint == null)
			{
				FooterPart._dataPartReferenceConstraint = new Dictionary<string, PartConstraintRule> { 
				{
					"http://schemas.openxmlformats.org/officeDocument/2006/relationships/video",
					new PartConstraintRule("VideoReferenceRelationship", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
				} };
			}
			return FooterPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D510 RID: 54544 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal FooterPart()
		{
		}

		// Token: 0x0600D511 RID: 54545 RVA: 0x002A5288 File Offset: 0x002A3488
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

		// Token: 0x0600D512 RID: 54546 RVA: 0x002A53E4 File Offset: 0x002A35E4
		public AlternativeFormatImportPart AddAlternativeFormatImportPart(string contentType)
		{
			AlternativeFormatImportPart alternativeFormatImportPart = new AlternativeFormatImportPart();
			base.InitPart<AlternativeFormatImportPart>(alternativeFormatImportPart, contentType);
			return alternativeFormatImportPart;
		}

		// Token: 0x0600D513 RID: 54547 RVA: 0x002A5400 File Offset: 0x002A3600
		public AlternativeFormatImportPart AddAlternativeFormatImportPart(AlternativeFormatImportPartType partType)
		{
			string contentType = AlternativeFormatImportPartTypeInfo.GetContentType(partType);
			string targetExtension = AlternativeFormatImportPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddAlternativeFormatImportPart(contentType);
		}

		// Token: 0x0600D514 RID: 54548 RVA: 0x002A5434 File Offset: 0x002A3634
		public AlternativeFormatImportPart AddAlternativeFormatImportPart(string contentType, string id)
		{
			AlternativeFormatImportPart alternativeFormatImportPart = new AlternativeFormatImportPart();
			this.InitPart<AlternativeFormatImportPart>(alternativeFormatImportPart, contentType, id);
			return alternativeFormatImportPart;
		}

		// Token: 0x0600D515 RID: 54549 RVA: 0x002A5454 File Offset: 0x002A3654
		public AlternativeFormatImportPart AddAlternativeFormatImportPart(AlternativeFormatImportPartType partType, string id)
		{
			string contentType = AlternativeFormatImportPartTypeInfo.GetContentType(partType);
			string targetExtension = AlternativeFormatImportPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddAlternativeFormatImportPart(contentType, id);
		}

		// Token: 0x0600D516 RID: 54550 RVA: 0x002A548C File Offset: 0x002A368C
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(string contentType)
		{
			EmbeddedControlPersistencePart embeddedControlPersistencePart = new EmbeddedControlPersistencePart();
			base.InitPart<EmbeddedControlPersistencePart>(embeddedControlPersistencePart, contentType);
			return embeddedControlPersistencePart;
		}

		// Token: 0x0600D517 RID: 54551 RVA: 0x002A54A8 File Offset: 0x002A36A8
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(EmbeddedControlPersistencePartType partType)
		{
			string contentType = EmbeddedControlPersistencePartTypeInfo.GetContentType(partType);
			string targetExtension = EmbeddedControlPersistencePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddEmbeddedControlPersistencePart(contentType);
		}

		// Token: 0x0600D518 RID: 54552 RVA: 0x002A54DC File Offset: 0x002A36DC
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(string contentType, string id)
		{
			EmbeddedControlPersistencePart embeddedControlPersistencePart = new EmbeddedControlPersistencePart();
			this.InitPart<EmbeddedControlPersistencePart>(embeddedControlPersistencePart, contentType, id);
			return embeddedControlPersistencePart;
		}

		// Token: 0x0600D519 RID: 54553 RVA: 0x002A54FC File Offset: 0x002A36FC
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(EmbeddedControlPersistencePartType partType, string id)
		{
			string contentType = EmbeddedControlPersistencePartTypeInfo.GetContentType(partType);
			string targetExtension = EmbeddedControlPersistencePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddEmbeddedControlPersistencePart(contentType, id);
		}

		// Token: 0x0600D51A RID: 54554 RVA: 0x002A5534 File Offset: 0x002A3734
		public EmbeddedObjectPart AddEmbeddedObjectPart(string contentType)
		{
			EmbeddedObjectPart embeddedObjectPart = new EmbeddedObjectPart();
			base.InitPart<EmbeddedObjectPart>(embeddedObjectPart, contentType);
			return embeddedObjectPart;
		}

		// Token: 0x0600D51B RID: 54555 RVA: 0x002A5550 File Offset: 0x002A3750
		public EmbeddedPackagePart AddEmbeddedPackagePart(string contentType)
		{
			EmbeddedPackagePart embeddedPackagePart = new EmbeddedPackagePart();
			base.InitPart<EmbeddedPackagePart>(embeddedPackagePart, contentType);
			return embeddedPackagePart;
		}

		// Token: 0x0600D51C RID: 54556 RVA: 0x002A556C File Offset: 0x002A376C
		public ImagePart AddImagePart(string contentType)
		{
			ImagePart imagePart = new ImagePart();
			base.InitPart<ImagePart>(imagePart, contentType);
			return imagePart;
		}

		// Token: 0x0600D51D RID: 54557 RVA: 0x002A5588 File Offset: 0x002A3788
		public ImagePart AddImagePart(ImagePartType partType)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType);
		}

		// Token: 0x0600D51E RID: 54558 RVA: 0x002A55BC File Offset: 0x002A37BC
		public ImagePart AddImagePart(string contentType, string id)
		{
			ImagePart imagePart = new ImagePart();
			this.InitPart<ImagePart>(imagePart, contentType, id);
			return imagePart;
		}

		// Token: 0x0600D51F RID: 54559 RVA: 0x002A55DC File Offset: 0x002A37DC
		public ImagePart AddImagePart(ImagePartType partType, string id)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType, id);
		}

		// Token: 0x0600D520 RID: 54560 RVA: 0x002A3EF9 File Offset: 0x002A20F9
		public VideoReferenceRelationship AddVideoReferenceRelationship(MediaDataPart mediaDataPart)
		{
			return base.AddDataPartReferenceRelationship<VideoReferenceRelationship>(mediaDataPart);
		}

		// Token: 0x0600D521 RID: 54561 RVA: 0x002A3F02 File Offset: 0x002A2102
		public VideoReferenceRelationship AddVideoReferenceRelationship(MediaDataPart mediaDataPart, string id)
		{
			return base.AddDataPartReferenceRelationship<VideoReferenceRelationship>(mediaDataPart, id);
		}

		// Token: 0x170033BE RID: 13246
		// (get) Token: 0x0600D522 RID: 54562 RVA: 0x002A5611 File Offset: 0x002A3811
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/footer";
			}
		}

		// Token: 0x170033BF RID: 13247
		// (get) Token: 0x0600D523 RID: 54563 RVA: 0x002A5618 File Offset: 0x002A3818
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.wordprocessingml.footer+xml";
			}
		}

		// Token: 0x170033C0 RID: 13248
		// (get) Token: 0x0600D524 RID: 54564 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x170033C1 RID: 13249
		// (get) Token: 0x0600D525 RID: 54565 RVA: 0x002A561F File Offset: 0x002A381F
		internal sealed override string TargetName
		{
			get
			{
				return "footer";
			}
		}

		// Token: 0x170033C2 RID: 13250
		// (get) Token: 0x0600D526 RID: 54566 RVA: 0x002A3FB1 File Offset: 0x002A21B1
		public IEnumerable<AlternativeFormatImportPart> AlternativeFormatImportParts
		{
			get
			{
				return base.GetPartsOfType<AlternativeFormatImportPart>();
			}
		}

		// Token: 0x170033C3 RID: 13251
		// (get) Token: 0x0600D527 RID: 54567 RVA: 0x002A3FB9 File Offset: 0x002A21B9
		public IEnumerable<ChartPart> ChartParts
		{
			get
			{
				return base.GetPartsOfType<ChartPart>();
			}
		}

		// Token: 0x170033C4 RID: 13252
		// (get) Token: 0x0600D528 RID: 54568 RVA: 0x002A3FC1 File Offset: 0x002A21C1
		public IEnumerable<DiagramColorsPart> DiagramColorsParts
		{
			get
			{
				return base.GetPartsOfType<DiagramColorsPart>();
			}
		}

		// Token: 0x170033C5 RID: 13253
		// (get) Token: 0x0600D529 RID: 54569 RVA: 0x002A3FC9 File Offset: 0x002A21C9
		public IEnumerable<DiagramDataPart> DiagramDataParts
		{
			get
			{
				return base.GetPartsOfType<DiagramDataPart>();
			}
		}

		// Token: 0x170033C6 RID: 13254
		// (get) Token: 0x0600D52A RID: 54570 RVA: 0x002A3FD1 File Offset: 0x002A21D1
		public IEnumerable<DiagramPersistLayoutPart> DiagramPersistLayoutParts
		{
			get
			{
				return base.GetPartsOfType<DiagramPersistLayoutPart>();
			}
		}

		// Token: 0x170033C7 RID: 13255
		// (get) Token: 0x0600D52B RID: 54571 RVA: 0x002A3FD9 File Offset: 0x002A21D9
		public IEnumerable<DiagramLayoutDefinitionPart> DiagramLayoutDefinitionParts
		{
			get
			{
				return base.GetPartsOfType<DiagramLayoutDefinitionPart>();
			}
		}

		// Token: 0x170033C8 RID: 13256
		// (get) Token: 0x0600D52C RID: 54572 RVA: 0x002A3FE1 File Offset: 0x002A21E1
		public IEnumerable<DiagramStylePart> DiagramStyleParts
		{
			get
			{
				return base.GetPartsOfType<DiagramStylePart>();
			}
		}

		// Token: 0x170033C9 RID: 13257
		// (get) Token: 0x0600D52D RID: 54573 RVA: 0x002A3FE9 File Offset: 0x002A21E9
		public IEnumerable<EmbeddedControlPersistencePart> EmbeddedControlPersistenceParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedControlPersistencePart>();
			}
		}

		// Token: 0x170033CA RID: 13258
		// (get) Token: 0x0600D52E RID: 54574 RVA: 0x002A3FF1 File Offset: 0x002A21F1
		public IEnumerable<EmbeddedObjectPart> EmbeddedObjectParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedObjectPart>();
			}
		}

		// Token: 0x170033CB RID: 13259
		// (get) Token: 0x0600D52F RID: 54575 RVA: 0x002A3FF9 File Offset: 0x002A21F9
		public IEnumerable<EmbeddedPackagePart> EmbeddedPackageParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedPackagePart>();
			}
		}

		// Token: 0x170033CC RID: 13260
		// (get) Token: 0x0600D530 RID: 54576 RVA: 0x002A4001 File Offset: 0x002A2201
		public IEnumerable<ImagePart> ImageParts
		{
			get
			{
				return base.GetPartsOfType<ImagePart>();
			}
		}

		// Token: 0x170033CD RID: 13261
		// (get) Token: 0x0600D531 RID: 54577 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170033CE RID: 13262
		// (get) Token: 0x0600D532 RID: 54578 RVA: 0x002A5626 File Offset: 0x002A3826
		// (set) Token: 0x0600D533 RID: 54579 RVA: 0x002A562E File Offset: 0x002A382E
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as Footer;
			}
		}

		// Token: 0x170033CF RID: 13263
		// (get) Token: 0x0600D534 RID: 54580 RVA: 0x002A563C File Offset: 0x002A383C
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.Footer;
			}
		}

		// Token: 0x170033D0 RID: 13264
		// (get) Token: 0x0600D535 RID: 54581 RVA: 0x002A5644 File Offset: 0x002A3844
		// (set) Token: 0x0600D536 RID: 54582 RVA: 0x002A3296 File Offset: 0x002A1496
		public Footer Footer
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<Footer>();
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

		// Token: 0x04006A14 RID: 27156
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/footer";

		// Token: 0x04006A15 RID: 27157
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.wordprocessingml.footer+xml";

		// Token: 0x04006A16 RID: 27158
		internal const string TargetPathConstant = ".";

		// Token: 0x04006A17 RID: 27159
		internal const string TargetNameConstant = "footer";

		// Token: 0x04006A18 RID: 27160
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006A19 RID: 27161
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006A1A RID: 27162
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Footer _rootEle;
	}
}
