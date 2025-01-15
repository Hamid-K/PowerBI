using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Presentation;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200218A RID: 8586
	internal class SlidePart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D88A RID: 55434 RVA: 0x002AAD38 File Offset: 0x002A8F38
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (SlidePart._partConstraint == null)
			{
				SlidePart._partConstraint = new Dictionary<string, PartConstraintRule>
				{
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/customXml",
						new PartConstraintRule("CustomXmlPart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
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
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/vmlDrawing",
						new PartConstraintRule("VmlDrawingPart", "application/vnd.openxmlformats-officedocument.vmlDrawing", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.microsoft.com/office/2006/relationships/activeXControlBinary",
						new PartConstraintRule("EmbeddedControlPersistenceBinaryDataPart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/comments",
						new PartConstraintRule("SlideCommentsPart", "application/vnd.openxmlformats-officedocument.presentationml.comments+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/notesSlide",
						new PartConstraintRule("NotesSlidePart", "application/vnd.openxmlformats-officedocument.presentationml.notesSlide+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/themeOverride",
						new PartConstraintRule("ThemeOverridePart", "application/vnd.openxmlformats-officedocument.themeOverride+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/slideLayout",
						new PartConstraintRule("SlideLayoutPart", "application/vnd.openxmlformats-officedocument.presentationml.slideLayout+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/slideUpdateInfo",
						new PartConstraintRule("SlideSyncDataPart", "application/vnd.openxmlformats-officedocument.presentationml.slideUpdateInfo+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/tags",
						new PartConstraintRule("UserDefinedTagsPart", "application/vnd.openxmlformats-officedocument.presentationml.tags+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/slide",
						new PartConstraintRule("SlidePart", "application/vnd.openxmlformats-officedocument.presentationml.slide+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/control",
						new PartConstraintRule("EmbeddedControlPersistencePart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					}
				};
			}
			return SlidePart._partConstraint;
		}

		// Token: 0x0600D88B RID: 55435 RVA: 0x002AAF8C File Offset: 0x002A918C
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (SlidePart._dataPartReferenceConstraint == null)
			{
				SlidePart._dataPartReferenceConstraint = new Dictionary<string, PartConstraintRule>
				{
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/audio",
						new PartConstraintRule("AudioReferenceRelationship", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/video",
						new PartConstraintRule("VideoReferenceRelationship", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.microsoft.com/office/2007/relationships/media",
						new PartConstraintRule("MediaReferenceRelationship", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					}
				};
			}
			return SlidePart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D88C RID: 55436 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal SlidePart()
		{
		}

		// Token: 0x0600D88D RID: 55437 RVA: 0x002AAFFC File Offset: 0x002A91FC
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
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/oleObject":
				return new EmbeddedObjectPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/package":
				return new EmbeddedPackagePart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image":
				return new ImagePart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/vmlDrawing":
				return new VmlDrawingPart();
			case "http://schemas.microsoft.com/office/2006/relationships/activeXControlBinary":
				return new EmbeddedControlPersistenceBinaryDataPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/comments":
				return new SlideCommentsPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/notesSlide":
				return new NotesSlidePart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/themeOverride":
				return new ThemeOverridePart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/slideLayout":
				return new SlideLayoutPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/slideUpdateInfo":
				return new SlideSyncDataPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/tags":
				return new UserDefinedTagsPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/slide":
				return new SlidePart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/control":
				return new EmbeddedControlPersistencePart();
			}
			throw new ArgumentOutOfRangeException("relationshipType");
		}

		// Token: 0x0600D88E RID: 55438 RVA: 0x002AB228 File Offset: 0x002A9428
		public CustomXmlPart AddCustomXmlPart(string contentType)
		{
			CustomXmlPart customXmlPart = new CustomXmlPart();
			base.InitPart<CustomXmlPart>(customXmlPart, contentType);
			return customXmlPart;
		}

		// Token: 0x0600D88F RID: 55439 RVA: 0x002AB244 File Offset: 0x002A9444
		public CustomXmlPart AddCustomXmlPart(CustomXmlPartType partType)
		{
			string contentType = CustomXmlPartTypeInfo.GetContentType(partType);
			string targetExtension = CustomXmlPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddCustomXmlPart(contentType);
		}

		// Token: 0x0600D890 RID: 55440 RVA: 0x002AB278 File Offset: 0x002A9478
		public CustomXmlPart AddCustomXmlPart(string contentType, string id)
		{
			CustomXmlPart customXmlPart = new CustomXmlPart();
			this.InitPart<CustomXmlPart>(customXmlPart, contentType, id);
			return customXmlPart;
		}

		// Token: 0x0600D891 RID: 55441 RVA: 0x002AB298 File Offset: 0x002A9498
		public CustomXmlPart AddCustomXmlPart(CustomXmlPartType partType, string id)
		{
			string contentType = CustomXmlPartTypeInfo.GetContentType(partType);
			string targetExtension = CustomXmlPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddCustomXmlPart(contentType, id);
		}

		// Token: 0x0600D892 RID: 55442 RVA: 0x002A9DB5 File Offset: 0x002A7FB5
		public AudioReferenceRelationship AddAudioReferenceRelationship(MediaDataPart mediaDataPart)
		{
			return base.AddDataPartReferenceRelationship<AudioReferenceRelationship>(mediaDataPart);
		}

		// Token: 0x0600D893 RID: 55443 RVA: 0x002A9DBE File Offset: 0x002A7FBE
		public AudioReferenceRelationship AddAudioReferenceRelationship(MediaDataPart mediaDataPart, string id)
		{
			return base.AddDataPartReferenceRelationship<AudioReferenceRelationship>(mediaDataPart, id);
		}

		// Token: 0x0600D894 RID: 55444 RVA: 0x002AB2D0 File Offset: 0x002A94D0
		public EmbeddedObjectPart AddEmbeddedObjectPart(string contentType)
		{
			EmbeddedObjectPart embeddedObjectPart = new EmbeddedObjectPart();
			base.InitPart<EmbeddedObjectPart>(embeddedObjectPart, contentType);
			return embeddedObjectPart;
		}

		// Token: 0x0600D895 RID: 55445 RVA: 0x002AB2EC File Offset: 0x002A94EC
		public EmbeddedPackagePart AddEmbeddedPackagePart(string contentType)
		{
			EmbeddedPackagePart embeddedPackagePart = new EmbeddedPackagePart();
			base.InitPart<EmbeddedPackagePart>(embeddedPackagePart, contentType);
			return embeddedPackagePart;
		}

		// Token: 0x0600D896 RID: 55446 RVA: 0x002AB308 File Offset: 0x002A9508
		public ImagePart AddImagePart(string contentType)
		{
			ImagePart imagePart = new ImagePart();
			base.InitPart<ImagePart>(imagePart, contentType);
			return imagePart;
		}

		// Token: 0x0600D897 RID: 55447 RVA: 0x002AB324 File Offset: 0x002A9524
		public ImagePart AddImagePart(ImagePartType partType)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType);
		}

		// Token: 0x0600D898 RID: 55448 RVA: 0x002AB358 File Offset: 0x002A9558
		public ImagePart AddImagePart(string contentType, string id)
		{
			ImagePart imagePart = new ImagePart();
			this.InitPart<ImagePart>(imagePart, contentType, id);
			return imagePart;
		}

		// Token: 0x0600D899 RID: 55449 RVA: 0x002AB378 File Offset: 0x002A9578
		public ImagePart AddImagePart(ImagePartType partType, string id)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType, id);
		}

		// Token: 0x0600D89A RID: 55450 RVA: 0x002A3EF9 File Offset: 0x002A20F9
		public VideoReferenceRelationship AddVideoReferenceRelationship(MediaDataPart mediaDataPart)
		{
			return base.AddDataPartReferenceRelationship<VideoReferenceRelationship>(mediaDataPart);
		}

		// Token: 0x0600D89B RID: 55451 RVA: 0x002A3F02 File Offset: 0x002A2102
		public VideoReferenceRelationship AddVideoReferenceRelationship(MediaDataPart mediaDataPart, string id)
		{
			return base.AddDataPartReferenceRelationship<VideoReferenceRelationship>(mediaDataPart, id);
		}

		// Token: 0x0600D89C RID: 55452 RVA: 0x002AB3B0 File Offset: 0x002A95B0
		public EmbeddedControlPersistenceBinaryDataPart AddEmbeddedControlPersistenceBinaryDataPart(string contentType)
		{
			EmbeddedControlPersistenceBinaryDataPart embeddedControlPersistenceBinaryDataPart = new EmbeddedControlPersistenceBinaryDataPart();
			base.InitPart<EmbeddedControlPersistenceBinaryDataPart>(embeddedControlPersistenceBinaryDataPart, contentType);
			return embeddedControlPersistenceBinaryDataPart;
		}

		// Token: 0x0600D89D RID: 55453 RVA: 0x002AB3CC File Offset: 0x002A95CC
		public EmbeddedControlPersistenceBinaryDataPart AddEmbeddedControlPersistenceBinaryDataPart(EmbeddedControlPersistenceBinaryDataPartType partType)
		{
			string contentType = EmbeddedControlPersistenceBinaryDataPartTypeInfo.GetContentType(partType);
			string targetExtension = EmbeddedControlPersistenceBinaryDataPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddEmbeddedControlPersistenceBinaryDataPart(contentType);
		}

		// Token: 0x0600D89E RID: 55454 RVA: 0x002AB400 File Offset: 0x002A9600
		public EmbeddedControlPersistenceBinaryDataPart AddEmbeddedControlPersistenceBinaryDataPart(string contentType, string id)
		{
			EmbeddedControlPersistenceBinaryDataPart embeddedControlPersistenceBinaryDataPart = new EmbeddedControlPersistenceBinaryDataPart();
			this.InitPart<EmbeddedControlPersistenceBinaryDataPart>(embeddedControlPersistenceBinaryDataPart, contentType, id);
			return embeddedControlPersistenceBinaryDataPart;
		}

		// Token: 0x0600D89F RID: 55455 RVA: 0x002AB420 File Offset: 0x002A9620
		public EmbeddedControlPersistenceBinaryDataPart AddEmbeddedControlPersistenceBinaryDataPart(EmbeddedControlPersistenceBinaryDataPartType partType, string id)
		{
			string contentType = EmbeddedControlPersistenceBinaryDataPartTypeInfo.GetContentType(partType);
			string targetExtension = EmbeddedControlPersistenceBinaryDataPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddEmbeddedControlPersistenceBinaryDataPart(contentType, id);
		}

		// Token: 0x0600D8A0 RID: 55456 RVA: 0x002AB458 File Offset: 0x002A9658
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(string contentType)
		{
			EmbeddedControlPersistencePart embeddedControlPersistencePart = new EmbeddedControlPersistencePart();
			base.InitPart<EmbeddedControlPersistencePart>(embeddedControlPersistencePart, contentType);
			return embeddedControlPersistencePart;
		}

		// Token: 0x0600D8A1 RID: 55457 RVA: 0x002AB474 File Offset: 0x002A9674
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(EmbeddedControlPersistencePartType partType)
		{
			string contentType = EmbeddedControlPersistencePartTypeInfo.GetContentType(partType);
			string targetExtension = EmbeddedControlPersistencePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddEmbeddedControlPersistencePart(contentType);
		}

		// Token: 0x0600D8A2 RID: 55458 RVA: 0x002AB4A8 File Offset: 0x002A96A8
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(string contentType, string id)
		{
			EmbeddedControlPersistencePart embeddedControlPersistencePart = new EmbeddedControlPersistencePart();
			this.InitPart<EmbeddedControlPersistencePart>(embeddedControlPersistencePart, contentType, id);
			return embeddedControlPersistencePart;
		}

		// Token: 0x0600D8A3 RID: 55459 RVA: 0x002AB4C8 File Offset: 0x002A96C8
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(EmbeddedControlPersistencePartType partType, string id)
		{
			string contentType = EmbeddedControlPersistencePartTypeInfo.GetContentType(partType);
			string targetExtension = EmbeddedControlPersistencePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddEmbeddedControlPersistencePart(contentType, id);
		}

		// Token: 0x0600D8A4 RID: 55460 RVA: 0x002AB4FD File Offset: 0x002A96FD
		public MediaReferenceRelationship AddMediaReferenceRelationship(MediaDataPart mediaDataPart)
		{
			return base.AddDataPartReferenceRelationship<MediaReferenceRelationship>(mediaDataPart);
		}

		// Token: 0x0600D8A5 RID: 55461 RVA: 0x002AB506 File Offset: 0x002A9706
		public MediaReferenceRelationship AddMediaReferenceRelationship(MediaDataPart mediaDataPart, string id)
		{
			return base.AddDataPartReferenceRelationship<MediaReferenceRelationship>(mediaDataPart, id);
		}

		// Token: 0x170035B3 RID: 13747
		// (get) Token: 0x0600D8A6 RID: 55462 RVA: 0x002AB510 File Offset: 0x002A9710
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/slide";
			}
		}

		// Token: 0x170035B4 RID: 13748
		// (get) Token: 0x0600D8A7 RID: 55463 RVA: 0x002AB517 File Offset: 0x002A9717
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.presentationml.slide+xml";
			}
		}

		// Token: 0x170035B5 RID: 13749
		// (get) Token: 0x0600D8A8 RID: 55464 RVA: 0x002AB51E File Offset: 0x002A971E
		internal sealed override string TargetPath
		{
			get
			{
				return "slides";
			}
		}

		// Token: 0x170035B6 RID: 13750
		// (get) Token: 0x0600D8A9 RID: 55465 RVA: 0x002AB525 File Offset: 0x002A9725
		internal sealed override string TargetName
		{
			get
			{
				return "slide";
			}
		}

		// Token: 0x170035B7 RID: 13751
		// (get) Token: 0x0600D8AA RID: 55466 RVA: 0x002A3F21 File Offset: 0x002A2121
		public IEnumerable<CustomXmlPart> CustomXmlParts
		{
			get
			{
				return base.GetPartsOfType<CustomXmlPart>();
			}
		}

		// Token: 0x170035B8 RID: 13752
		// (get) Token: 0x0600D8AB RID: 55467 RVA: 0x002A3FB9 File Offset: 0x002A21B9
		public IEnumerable<ChartPart> ChartParts
		{
			get
			{
				return base.GetPartsOfType<ChartPart>();
			}
		}

		// Token: 0x170035B9 RID: 13753
		// (get) Token: 0x0600D8AC RID: 55468 RVA: 0x002A3FC1 File Offset: 0x002A21C1
		public IEnumerable<DiagramColorsPart> DiagramColorsParts
		{
			get
			{
				return base.GetPartsOfType<DiagramColorsPart>();
			}
		}

		// Token: 0x170035BA RID: 13754
		// (get) Token: 0x0600D8AD RID: 55469 RVA: 0x002A3FC9 File Offset: 0x002A21C9
		public IEnumerable<DiagramDataPart> DiagramDataParts
		{
			get
			{
				return base.GetPartsOfType<DiagramDataPart>();
			}
		}

		// Token: 0x170035BB RID: 13755
		// (get) Token: 0x0600D8AE RID: 55470 RVA: 0x002A3FD1 File Offset: 0x002A21D1
		public IEnumerable<DiagramPersistLayoutPart> DiagramPersistLayoutParts
		{
			get
			{
				return base.GetPartsOfType<DiagramPersistLayoutPart>();
			}
		}

		// Token: 0x170035BC RID: 13756
		// (get) Token: 0x0600D8AF RID: 55471 RVA: 0x002A3FD9 File Offset: 0x002A21D9
		public IEnumerable<DiagramLayoutDefinitionPart> DiagramLayoutDefinitionParts
		{
			get
			{
				return base.GetPartsOfType<DiagramLayoutDefinitionPart>();
			}
		}

		// Token: 0x170035BD RID: 13757
		// (get) Token: 0x0600D8B0 RID: 55472 RVA: 0x002A3FE1 File Offset: 0x002A21E1
		public IEnumerable<DiagramStylePart> DiagramStyleParts
		{
			get
			{
				return base.GetPartsOfType<DiagramStylePart>();
			}
		}

		// Token: 0x170035BE RID: 13758
		// (get) Token: 0x0600D8B1 RID: 55473 RVA: 0x002A3FF1 File Offset: 0x002A21F1
		public IEnumerable<EmbeddedObjectPart> EmbeddedObjectParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedObjectPart>();
			}
		}

		// Token: 0x170035BF RID: 13759
		// (get) Token: 0x0600D8B2 RID: 55474 RVA: 0x002A3FF9 File Offset: 0x002A21F9
		public IEnumerable<EmbeddedPackagePart> EmbeddedPackageParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedPackagePart>();
			}
		}

		// Token: 0x170035C0 RID: 13760
		// (get) Token: 0x0600D8B3 RID: 55475 RVA: 0x002A4001 File Offset: 0x002A2201
		public IEnumerable<ImagePart> ImageParts
		{
			get
			{
				return base.GetPartsOfType<ImagePart>();
			}
		}

		// Token: 0x170035C1 RID: 13761
		// (get) Token: 0x0600D8B4 RID: 55476 RVA: 0x002A780D File Offset: 0x002A5A0D
		public IEnumerable<VmlDrawingPart> VmlDrawingParts
		{
			get
			{
				return base.GetPartsOfType<VmlDrawingPart>();
			}
		}

		// Token: 0x170035C2 RID: 13762
		// (get) Token: 0x0600D8B5 RID: 55477 RVA: 0x002A9132 File Offset: 0x002A7332
		public IEnumerable<EmbeddedControlPersistenceBinaryDataPart> EmbeddedControlPersistenceBinaryDataParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedControlPersistenceBinaryDataPart>();
			}
		}

		// Token: 0x170035C3 RID: 13763
		// (get) Token: 0x0600D8B6 RID: 55478 RVA: 0x002AB52C File Offset: 0x002A972C
		public SlideCommentsPart SlideCommentsPart
		{
			get
			{
				return base.GetSubPartOfType<SlideCommentsPart>();
			}
		}

		// Token: 0x170035C4 RID: 13764
		// (get) Token: 0x0600D8B7 RID: 55479 RVA: 0x002AB534 File Offset: 0x002A9734
		public NotesSlidePart NotesSlidePart
		{
			get
			{
				return base.GetSubPartOfType<NotesSlidePart>();
			}
		}

		// Token: 0x170035C5 RID: 13765
		// (get) Token: 0x0600D8B8 RID: 55480 RVA: 0x002AAC61 File Offset: 0x002A8E61
		public ThemeOverridePart ThemeOverridePart
		{
			get
			{
				return base.GetSubPartOfType<ThemeOverridePart>();
			}
		}

		// Token: 0x170035C6 RID: 13766
		// (get) Token: 0x0600D8B9 RID: 55481 RVA: 0x002AB53C File Offset: 0x002A973C
		public SlideLayoutPart SlideLayoutPart
		{
			get
			{
				return base.GetSubPartOfType<SlideLayoutPart>();
			}
		}

		// Token: 0x170035C7 RID: 13767
		// (get) Token: 0x0600D8BA RID: 55482 RVA: 0x002AB544 File Offset: 0x002A9744
		public SlideSyncDataPart SlideSyncDataPart
		{
			get
			{
				return base.GetSubPartOfType<SlideSyncDataPart>();
			}
		}

		// Token: 0x170035C8 RID: 13768
		// (get) Token: 0x0600D8BB RID: 55483 RVA: 0x002A9F69 File Offset: 0x002A8169
		public IEnumerable<UserDefinedTagsPart> UserDefinedTagsParts
		{
			get
			{
				return base.GetPartsOfType<UserDefinedTagsPart>();
			}
		}

		// Token: 0x170035C9 RID: 13769
		// (get) Token: 0x0600D8BC RID: 55484 RVA: 0x002A9797 File Offset: 0x002A7997
		public IEnumerable<SlidePart> SlideParts
		{
			get
			{
				return base.GetPartsOfType<SlidePart>();
			}
		}

		// Token: 0x170035CA RID: 13770
		// (get) Token: 0x0600D8BD RID: 55485 RVA: 0x002A3FE9 File Offset: 0x002A21E9
		public IEnumerable<EmbeddedControlPersistencePart> EmbeddedControlPersistenceParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedControlPersistencePart>();
			}
		}

		// Token: 0x170035CB RID: 13771
		// (get) Token: 0x0600D8BE RID: 55486 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170035CC RID: 13772
		// (get) Token: 0x0600D8BF RID: 55487 RVA: 0x002AB54C File Offset: 0x002A974C
		// (set) Token: 0x0600D8C0 RID: 55488 RVA: 0x002AB554 File Offset: 0x002A9754
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as Slide;
			}
		}

		// Token: 0x170035CD RID: 13773
		// (get) Token: 0x0600D8C1 RID: 55489 RVA: 0x002AB562 File Offset: 0x002A9762
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.Slide;
			}
		}

		// Token: 0x170035CE RID: 13774
		// (get) Token: 0x0600D8C2 RID: 55490 RVA: 0x002AB56A File Offset: 0x002A976A
		// (set) Token: 0x0600D8C3 RID: 55491 RVA: 0x002A3296 File Offset: 0x002A1496
		public Slide Slide
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<Slide>();
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

		// Token: 0x04006B2F RID: 27439
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/slide";

		// Token: 0x04006B30 RID: 27440
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.presentationml.slide+xml";

		// Token: 0x04006B31 RID: 27441
		internal const string TargetPathConstant = "slides";

		// Token: 0x04006B32 RID: 27442
		internal const string TargetNameConstant = "slide";

		// Token: 0x04006B33 RID: 27443
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006B34 RID: 27444
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006B35 RID: 27445
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Slide _rootEle;
	}
}
