using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200215C RID: 8540
	internal class WordprocessingCommentsPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D491 RID: 54417 RVA: 0x002A4208 File Offset: 0x002A2408
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (WordprocessingCommentsPart._partConstraint == null)
			{
				WordprocessingCommentsPart._partConstraint = new Dictionary<string, PartConstraintRule>
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
			return WordprocessingCommentsPart._partConstraint;
		}

		// Token: 0x0600D492 RID: 54418 RVA: 0x002A435C File Offset: 0x002A255C
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (WordprocessingCommentsPart._dataPartReferenceConstraint == null)
			{
				WordprocessingCommentsPart._dataPartReferenceConstraint = new Dictionary<string, PartConstraintRule> { 
				{
					"http://schemas.openxmlformats.org/officeDocument/2006/relationships/video",
					new PartConstraintRule("VideoReferenceRelationship", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
				} };
			}
			return WordprocessingCommentsPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D493 RID: 54419 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal WordprocessingCommentsPart()
		{
		}

		// Token: 0x0600D494 RID: 54420 RVA: 0x002A439C File Offset: 0x002A259C
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

		// Token: 0x0600D495 RID: 54421 RVA: 0x002A44F8 File Offset: 0x002A26F8
		public AlternativeFormatImportPart AddAlternativeFormatImportPart(string contentType)
		{
			AlternativeFormatImportPart alternativeFormatImportPart = new AlternativeFormatImportPart();
			base.InitPart<AlternativeFormatImportPart>(alternativeFormatImportPart, contentType);
			return alternativeFormatImportPart;
		}

		// Token: 0x0600D496 RID: 54422 RVA: 0x002A4514 File Offset: 0x002A2714
		public AlternativeFormatImportPart AddAlternativeFormatImportPart(AlternativeFormatImportPartType partType)
		{
			string contentType = AlternativeFormatImportPartTypeInfo.GetContentType(partType);
			string targetExtension = AlternativeFormatImportPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddAlternativeFormatImportPart(contentType);
		}

		// Token: 0x0600D497 RID: 54423 RVA: 0x002A4548 File Offset: 0x002A2748
		public AlternativeFormatImportPart AddAlternativeFormatImportPart(string contentType, string id)
		{
			AlternativeFormatImportPart alternativeFormatImportPart = new AlternativeFormatImportPart();
			this.InitPart<AlternativeFormatImportPart>(alternativeFormatImportPart, contentType, id);
			return alternativeFormatImportPart;
		}

		// Token: 0x0600D498 RID: 54424 RVA: 0x002A4568 File Offset: 0x002A2768
		public AlternativeFormatImportPart AddAlternativeFormatImportPart(AlternativeFormatImportPartType partType, string id)
		{
			string contentType = AlternativeFormatImportPartTypeInfo.GetContentType(partType);
			string targetExtension = AlternativeFormatImportPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddAlternativeFormatImportPart(contentType, id);
		}

		// Token: 0x0600D499 RID: 54425 RVA: 0x002A45A0 File Offset: 0x002A27A0
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(string contentType)
		{
			EmbeddedControlPersistencePart embeddedControlPersistencePart = new EmbeddedControlPersistencePart();
			base.InitPart<EmbeddedControlPersistencePart>(embeddedControlPersistencePart, contentType);
			return embeddedControlPersistencePart;
		}

		// Token: 0x0600D49A RID: 54426 RVA: 0x002A45BC File Offset: 0x002A27BC
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(EmbeddedControlPersistencePartType partType)
		{
			string contentType = EmbeddedControlPersistencePartTypeInfo.GetContentType(partType);
			string targetExtension = EmbeddedControlPersistencePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddEmbeddedControlPersistencePart(contentType);
		}

		// Token: 0x0600D49B RID: 54427 RVA: 0x002A45F0 File Offset: 0x002A27F0
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(string contentType, string id)
		{
			EmbeddedControlPersistencePart embeddedControlPersistencePart = new EmbeddedControlPersistencePart();
			this.InitPart<EmbeddedControlPersistencePart>(embeddedControlPersistencePart, contentType, id);
			return embeddedControlPersistencePart;
		}

		// Token: 0x0600D49C RID: 54428 RVA: 0x002A4610 File Offset: 0x002A2810
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(EmbeddedControlPersistencePartType partType, string id)
		{
			string contentType = EmbeddedControlPersistencePartTypeInfo.GetContentType(partType);
			string targetExtension = EmbeddedControlPersistencePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddEmbeddedControlPersistencePart(contentType, id);
		}

		// Token: 0x0600D49D RID: 54429 RVA: 0x002A4648 File Offset: 0x002A2848
		public EmbeddedObjectPart AddEmbeddedObjectPart(string contentType)
		{
			EmbeddedObjectPart embeddedObjectPart = new EmbeddedObjectPart();
			base.InitPart<EmbeddedObjectPart>(embeddedObjectPart, contentType);
			return embeddedObjectPart;
		}

		// Token: 0x0600D49E RID: 54430 RVA: 0x002A4664 File Offset: 0x002A2864
		public EmbeddedPackagePart AddEmbeddedPackagePart(string contentType)
		{
			EmbeddedPackagePart embeddedPackagePart = new EmbeddedPackagePart();
			base.InitPart<EmbeddedPackagePart>(embeddedPackagePart, contentType);
			return embeddedPackagePart;
		}

		// Token: 0x0600D49F RID: 54431 RVA: 0x002A4680 File Offset: 0x002A2880
		public ImagePart AddImagePart(string contentType)
		{
			ImagePart imagePart = new ImagePart();
			base.InitPart<ImagePart>(imagePart, contentType);
			return imagePart;
		}

		// Token: 0x0600D4A0 RID: 54432 RVA: 0x002A469C File Offset: 0x002A289C
		public ImagePart AddImagePart(ImagePartType partType)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType);
		}

		// Token: 0x0600D4A1 RID: 54433 RVA: 0x002A46D0 File Offset: 0x002A28D0
		public ImagePart AddImagePart(string contentType, string id)
		{
			ImagePart imagePart = new ImagePart();
			this.InitPart<ImagePart>(imagePart, contentType, id);
			return imagePart;
		}

		// Token: 0x0600D4A2 RID: 54434 RVA: 0x002A46F0 File Offset: 0x002A28F0
		public ImagePart AddImagePart(ImagePartType partType, string id)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType, id);
		}

		// Token: 0x0600D4A3 RID: 54435 RVA: 0x002A3EF9 File Offset: 0x002A20F9
		public VideoReferenceRelationship AddVideoReferenceRelationship(MediaDataPart mediaDataPart)
		{
			return base.AddDataPartReferenceRelationship<VideoReferenceRelationship>(mediaDataPart);
		}

		// Token: 0x0600D4A4 RID: 54436 RVA: 0x002A3F02 File Offset: 0x002A2102
		public VideoReferenceRelationship AddVideoReferenceRelationship(MediaDataPart mediaDataPart, string id)
		{
			return base.AddDataPartReferenceRelationship<VideoReferenceRelationship>(mediaDataPart, id);
		}

		// Token: 0x17003385 RID: 13189
		// (get) Token: 0x0600D4A5 RID: 54437 RVA: 0x002A4725 File Offset: 0x002A2925
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/comments";
			}
		}

		// Token: 0x17003386 RID: 13190
		// (get) Token: 0x0600D4A6 RID: 54438 RVA: 0x002A472C File Offset: 0x002A292C
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.wordprocessingml.comments+xml";
			}
		}

		// Token: 0x17003387 RID: 13191
		// (get) Token: 0x0600D4A7 RID: 54439 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x17003388 RID: 13192
		// (get) Token: 0x0600D4A8 RID: 54440 RVA: 0x002A4733 File Offset: 0x002A2933
		internal sealed override string TargetName
		{
			get
			{
				return "comments";
			}
		}

		// Token: 0x17003389 RID: 13193
		// (get) Token: 0x0600D4A9 RID: 54441 RVA: 0x002A3FB1 File Offset: 0x002A21B1
		public IEnumerable<AlternativeFormatImportPart> AlternativeFormatImportParts
		{
			get
			{
				return base.GetPartsOfType<AlternativeFormatImportPart>();
			}
		}

		// Token: 0x1700338A RID: 13194
		// (get) Token: 0x0600D4AA RID: 54442 RVA: 0x002A3FB9 File Offset: 0x002A21B9
		public IEnumerable<ChartPart> ChartParts
		{
			get
			{
				return base.GetPartsOfType<ChartPart>();
			}
		}

		// Token: 0x1700338B RID: 13195
		// (get) Token: 0x0600D4AB RID: 54443 RVA: 0x002A3FC1 File Offset: 0x002A21C1
		public IEnumerable<DiagramColorsPart> DiagramColorsParts
		{
			get
			{
				return base.GetPartsOfType<DiagramColorsPart>();
			}
		}

		// Token: 0x1700338C RID: 13196
		// (get) Token: 0x0600D4AC RID: 54444 RVA: 0x002A3FC9 File Offset: 0x002A21C9
		public IEnumerable<DiagramDataPart> DiagramDataParts
		{
			get
			{
				return base.GetPartsOfType<DiagramDataPart>();
			}
		}

		// Token: 0x1700338D RID: 13197
		// (get) Token: 0x0600D4AD RID: 54445 RVA: 0x002A3FD1 File Offset: 0x002A21D1
		public IEnumerable<DiagramPersistLayoutPart> DiagramPersistLayoutParts
		{
			get
			{
				return base.GetPartsOfType<DiagramPersistLayoutPart>();
			}
		}

		// Token: 0x1700338E RID: 13198
		// (get) Token: 0x0600D4AE RID: 54446 RVA: 0x002A3FD9 File Offset: 0x002A21D9
		public IEnumerable<DiagramLayoutDefinitionPart> DiagramLayoutDefinitionParts
		{
			get
			{
				return base.GetPartsOfType<DiagramLayoutDefinitionPart>();
			}
		}

		// Token: 0x1700338F RID: 13199
		// (get) Token: 0x0600D4AF RID: 54447 RVA: 0x002A3FE1 File Offset: 0x002A21E1
		public IEnumerable<DiagramStylePart> DiagramStyleParts
		{
			get
			{
				return base.GetPartsOfType<DiagramStylePart>();
			}
		}

		// Token: 0x17003390 RID: 13200
		// (get) Token: 0x0600D4B0 RID: 54448 RVA: 0x002A3FE9 File Offset: 0x002A21E9
		public IEnumerable<EmbeddedControlPersistencePart> EmbeddedControlPersistenceParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedControlPersistencePart>();
			}
		}

		// Token: 0x17003391 RID: 13201
		// (get) Token: 0x0600D4B1 RID: 54449 RVA: 0x002A3FF1 File Offset: 0x002A21F1
		public IEnumerable<EmbeddedObjectPart> EmbeddedObjectParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedObjectPart>();
			}
		}

		// Token: 0x17003392 RID: 13202
		// (get) Token: 0x0600D4B2 RID: 54450 RVA: 0x002A3FF9 File Offset: 0x002A21F9
		public IEnumerable<EmbeddedPackagePart> EmbeddedPackageParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedPackagePart>();
			}
		}

		// Token: 0x17003393 RID: 13203
		// (get) Token: 0x0600D4B3 RID: 54451 RVA: 0x002A4001 File Offset: 0x002A2201
		public IEnumerable<ImagePart> ImageParts
		{
			get
			{
				return base.GetPartsOfType<ImagePart>();
			}
		}

		// Token: 0x17003394 RID: 13204
		// (get) Token: 0x0600D4B4 RID: 54452 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003395 RID: 13205
		// (get) Token: 0x0600D4B5 RID: 54453 RVA: 0x002A473A File Offset: 0x002A293A
		// (set) Token: 0x0600D4B6 RID: 54454 RVA: 0x002A4742 File Offset: 0x002A2942
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as Comments;
			}
		}

		// Token: 0x17003396 RID: 13206
		// (get) Token: 0x0600D4B7 RID: 54455 RVA: 0x002A4750 File Offset: 0x002A2950
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.Comments;
			}
		}

		// Token: 0x17003397 RID: 13207
		// (get) Token: 0x0600D4B8 RID: 54456 RVA: 0x002A4758 File Offset: 0x002A2958
		// (set) Token: 0x0600D4B9 RID: 54457 RVA: 0x002A3296 File Offset: 0x002A1496
		public Comments Comments
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<Comments>();
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

		// Token: 0x040069F8 RID: 27128
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/comments";

		// Token: 0x040069F9 RID: 27129
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.wordprocessingml.comments+xml";

		// Token: 0x040069FA RID: 27130
		internal const string TargetPathConstant = ".";

		// Token: 0x040069FB RID: 27131
		internal const string TargetNameConstant = "comments";

		// Token: 0x040069FC RID: 27132
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x040069FD RID: 27133
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x040069FE RID: 27134
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Comments _rootEle;
	}
}
