using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002163 RID: 8547
	internal class HeaderPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D597 RID: 54679 RVA: 0x002A640C File Offset: 0x002A460C
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (HeaderPart._partConstraint == null)
			{
				HeaderPart._partConstraint = new Dictionary<string, PartConstraintRule>
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
			return HeaderPart._partConstraint;
		}

		// Token: 0x0600D598 RID: 54680 RVA: 0x002A6560 File Offset: 0x002A4760
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (HeaderPart._dataPartReferenceConstraint == null)
			{
				HeaderPart._dataPartReferenceConstraint = new Dictionary<string, PartConstraintRule> { 
				{
					"http://schemas.openxmlformats.org/officeDocument/2006/relationships/video",
					new PartConstraintRule("VideoReferenceRelationship", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
				} };
			}
			return HeaderPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D599 RID: 54681 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal HeaderPart()
		{
		}

		// Token: 0x0600D59A RID: 54682 RVA: 0x002A65A0 File Offset: 0x002A47A0
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

		// Token: 0x0600D59B RID: 54683 RVA: 0x002A66FC File Offset: 0x002A48FC
		public AlternativeFormatImportPart AddAlternativeFormatImportPart(string contentType)
		{
			AlternativeFormatImportPart alternativeFormatImportPart = new AlternativeFormatImportPart();
			base.InitPart<AlternativeFormatImportPart>(alternativeFormatImportPart, contentType);
			return alternativeFormatImportPart;
		}

		// Token: 0x0600D59C RID: 54684 RVA: 0x002A6718 File Offset: 0x002A4918
		public AlternativeFormatImportPart AddAlternativeFormatImportPart(AlternativeFormatImportPartType partType)
		{
			string contentType = AlternativeFormatImportPartTypeInfo.GetContentType(partType);
			string targetExtension = AlternativeFormatImportPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddAlternativeFormatImportPart(contentType);
		}

		// Token: 0x0600D59D RID: 54685 RVA: 0x002A674C File Offset: 0x002A494C
		public AlternativeFormatImportPart AddAlternativeFormatImportPart(string contentType, string id)
		{
			AlternativeFormatImportPart alternativeFormatImportPart = new AlternativeFormatImportPart();
			this.InitPart<AlternativeFormatImportPart>(alternativeFormatImportPart, contentType, id);
			return alternativeFormatImportPart;
		}

		// Token: 0x0600D59E RID: 54686 RVA: 0x002A676C File Offset: 0x002A496C
		public AlternativeFormatImportPart AddAlternativeFormatImportPart(AlternativeFormatImportPartType partType, string id)
		{
			string contentType = AlternativeFormatImportPartTypeInfo.GetContentType(partType);
			string targetExtension = AlternativeFormatImportPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddAlternativeFormatImportPart(contentType, id);
		}

		// Token: 0x0600D59F RID: 54687 RVA: 0x002A67A4 File Offset: 0x002A49A4
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(string contentType)
		{
			EmbeddedControlPersistencePart embeddedControlPersistencePart = new EmbeddedControlPersistencePart();
			base.InitPart<EmbeddedControlPersistencePart>(embeddedControlPersistencePart, contentType);
			return embeddedControlPersistencePart;
		}

		// Token: 0x0600D5A0 RID: 54688 RVA: 0x002A67C0 File Offset: 0x002A49C0
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(EmbeddedControlPersistencePartType partType)
		{
			string contentType = EmbeddedControlPersistencePartTypeInfo.GetContentType(partType);
			string targetExtension = EmbeddedControlPersistencePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddEmbeddedControlPersistencePart(contentType);
		}

		// Token: 0x0600D5A1 RID: 54689 RVA: 0x002A67F4 File Offset: 0x002A49F4
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(string contentType, string id)
		{
			EmbeddedControlPersistencePart embeddedControlPersistencePart = new EmbeddedControlPersistencePart();
			this.InitPart<EmbeddedControlPersistencePart>(embeddedControlPersistencePart, contentType, id);
			return embeddedControlPersistencePart;
		}

		// Token: 0x0600D5A2 RID: 54690 RVA: 0x002A6814 File Offset: 0x002A4A14
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(EmbeddedControlPersistencePartType partType, string id)
		{
			string contentType = EmbeddedControlPersistencePartTypeInfo.GetContentType(partType);
			string targetExtension = EmbeddedControlPersistencePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddEmbeddedControlPersistencePart(contentType, id);
		}

		// Token: 0x0600D5A3 RID: 54691 RVA: 0x002A684C File Offset: 0x002A4A4C
		public EmbeddedObjectPart AddEmbeddedObjectPart(string contentType)
		{
			EmbeddedObjectPart embeddedObjectPart = new EmbeddedObjectPart();
			base.InitPart<EmbeddedObjectPart>(embeddedObjectPart, contentType);
			return embeddedObjectPart;
		}

		// Token: 0x0600D5A4 RID: 54692 RVA: 0x002A6868 File Offset: 0x002A4A68
		public EmbeddedPackagePart AddEmbeddedPackagePart(string contentType)
		{
			EmbeddedPackagePart embeddedPackagePart = new EmbeddedPackagePart();
			base.InitPart<EmbeddedPackagePart>(embeddedPackagePart, contentType);
			return embeddedPackagePart;
		}

		// Token: 0x0600D5A5 RID: 54693 RVA: 0x002A6884 File Offset: 0x002A4A84
		public ImagePart AddImagePart(string contentType)
		{
			ImagePart imagePart = new ImagePart();
			base.InitPart<ImagePart>(imagePart, contentType);
			return imagePart;
		}

		// Token: 0x0600D5A6 RID: 54694 RVA: 0x002A68A0 File Offset: 0x002A4AA0
		public ImagePart AddImagePart(ImagePartType partType)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType);
		}

		// Token: 0x0600D5A7 RID: 54695 RVA: 0x002A68D4 File Offset: 0x002A4AD4
		public ImagePart AddImagePart(string contentType, string id)
		{
			ImagePart imagePart = new ImagePart();
			this.InitPart<ImagePart>(imagePart, contentType, id);
			return imagePart;
		}

		// Token: 0x0600D5A8 RID: 54696 RVA: 0x002A68F4 File Offset: 0x002A4AF4
		public ImagePart AddImagePart(ImagePartType partType, string id)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType, id);
		}

		// Token: 0x0600D5A9 RID: 54697 RVA: 0x002A3EF9 File Offset: 0x002A20F9
		public VideoReferenceRelationship AddVideoReferenceRelationship(MediaDataPart mediaDataPart)
		{
			return base.AddDataPartReferenceRelationship<VideoReferenceRelationship>(mediaDataPart);
		}

		// Token: 0x0600D5AA RID: 54698 RVA: 0x002A3F02 File Offset: 0x002A2102
		public VideoReferenceRelationship AddVideoReferenceRelationship(MediaDataPart mediaDataPart, string id)
		{
			return base.AddDataPartReferenceRelationship<VideoReferenceRelationship>(mediaDataPart, id);
		}

		// Token: 0x17003405 RID: 13317
		// (get) Token: 0x0600D5AB RID: 54699 RVA: 0x002A6929 File Offset: 0x002A4B29
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/header";
			}
		}

		// Token: 0x17003406 RID: 13318
		// (get) Token: 0x0600D5AC RID: 54700 RVA: 0x002A6930 File Offset: 0x002A4B30
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.wordprocessingml.header+xml";
			}
		}

		// Token: 0x17003407 RID: 13319
		// (get) Token: 0x0600D5AD RID: 54701 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x17003408 RID: 13320
		// (get) Token: 0x0600D5AE RID: 54702 RVA: 0x002A6937 File Offset: 0x002A4B37
		internal sealed override string TargetName
		{
			get
			{
				return "header";
			}
		}

		// Token: 0x17003409 RID: 13321
		// (get) Token: 0x0600D5AF RID: 54703 RVA: 0x002A3FB1 File Offset: 0x002A21B1
		public IEnumerable<AlternativeFormatImportPart> AlternativeFormatImportParts
		{
			get
			{
				return base.GetPartsOfType<AlternativeFormatImportPart>();
			}
		}

		// Token: 0x1700340A RID: 13322
		// (get) Token: 0x0600D5B0 RID: 54704 RVA: 0x002A3FB9 File Offset: 0x002A21B9
		public IEnumerable<ChartPart> ChartParts
		{
			get
			{
				return base.GetPartsOfType<ChartPart>();
			}
		}

		// Token: 0x1700340B RID: 13323
		// (get) Token: 0x0600D5B1 RID: 54705 RVA: 0x002A3FC1 File Offset: 0x002A21C1
		public IEnumerable<DiagramColorsPart> DiagramColorsParts
		{
			get
			{
				return base.GetPartsOfType<DiagramColorsPart>();
			}
		}

		// Token: 0x1700340C RID: 13324
		// (get) Token: 0x0600D5B2 RID: 54706 RVA: 0x002A3FC9 File Offset: 0x002A21C9
		public IEnumerable<DiagramDataPart> DiagramDataParts
		{
			get
			{
				return base.GetPartsOfType<DiagramDataPart>();
			}
		}

		// Token: 0x1700340D RID: 13325
		// (get) Token: 0x0600D5B3 RID: 54707 RVA: 0x002A3FD1 File Offset: 0x002A21D1
		public IEnumerable<DiagramPersistLayoutPart> DiagramPersistLayoutParts
		{
			get
			{
				return base.GetPartsOfType<DiagramPersistLayoutPart>();
			}
		}

		// Token: 0x1700340E RID: 13326
		// (get) Token: 0x0600D5B4 RID: 54708 RVA: 0x002A3FD9 File Offset: 0x002A21D9
		public IEnumerable<DiagramLayoutDefinitionPart> DiagramLayoutDefinitionParts
		{
			get
			{
				return base.GetPartsOfType<DiagramLayoutDefinitionPart>();
			}
		}

		// Token: 0x1700340F RID: 13327
		// (get) Token: 0x0600D5B5 RID: 54709 RVA: 0x002A3FE1 File Offset: 0x002A21E1
		public IEnumerable<DiagramStylePart> DiagramStyleParts
		{
			get
			{
				return base.GetPartsOfType<DiagramStylePart>();
			}
		}

		// Token: 0x17003410 RID: 13328
		// (get) Token: 0x0600D5B6 RID: 54710 RVA: 0x002A3FE9 File Offset: 0x002A21E9
		public IEnumerable<EmbeddedControlPersistencePart> EmbeddedControlPersistenceParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedControlPersistencePart>();
			}
		}

		// Token: 0x17003411 RID: 13329
		// (get) Token: 0x0600D5B7 RID: 54711 RVA: 0x002A3FF1 File Offset: 0x002A21F1
		public IEnumerable<EmbeddedObjectPart> EmbeddedObjectParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedObjectPart>();
			}
		}

		// Token: 0x17003412 RID: 13330
		// (get) Token: 0x0600D5B8 RID: 54712 RVA: 0x002A3FF9 File Offset: 0x002A21F9
		public IEnumerable<EmbeddedPackagePart> EmbeddedPackageParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedPackagePart>();
			}
		}

		// Token: 0x17003413 RID: 13331
		// (get) Token: 0x0600D5B9 RID: 54713 RVA: 0x002A4001 File Offset: 0x002A2201
		public IEnumerable<ImagePart> ImageParts
		{
			get
			{
				return base.GetPartsOfType<ImagePart>();
			}
		}

		// Token: 0x17003414 RID: 13332
		// (get) Token: 0x0600D5BA RID: 54714 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003415 RID: 13333
		// (get) Token: 0x0600D5BB RID: 54715 RVA: 0x002A693E File Offset: 0x002A4B3E
		// (set) Token: 0x0600D5BC RID: 54716 RVA: 0x002A6946 File Offset: 0x002A4B46
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as Header;
			}
		}

		// Token: 0x17003416 RID: 13334
		// (get) Token: 0x0600D5BD RID: 54717 RVA: 0x002A6954 File Offset: 0x002A4B54
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.Header;
			}
		}

		// Token: 0x17003417 RID: 13335
		// (get) Token: 0x0600D5BE RID: 54718 RVA: 0x002A695C File Offset: 0x002A4B5C
		// (set) Token: 0x0600D5BF RID: 54719 RVA: 0x002A3296 File Offset: 0x002A1496
		public Header Header
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<Header>();
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

		// Token: 0x04006A29 RID: 27177
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/header";

		// Token: 0x04006A2A RID: 27178
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.wordprocessingml.header+xml";

		// Token: 0x04006A2B RID: 27179
		internal const string TargetPathConstant = ".";

		// Token: 0x04006A2C RID: 27180
		internal const string TargetNameConstant = "header";

		// Token: 0x04006A2D RID: 27181
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006A2E RID: 27182
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006A2F RID: 27183
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Header _rootEle;
	}
}
