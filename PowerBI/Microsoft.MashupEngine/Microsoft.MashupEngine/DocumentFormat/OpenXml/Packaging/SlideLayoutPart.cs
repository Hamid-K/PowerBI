using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Presentation;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200218B RID: 8587
	internal class SlideLayoutPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D8C4 RID: 55492 RVA: 0x002AB580 File Offset: 0x002A9780
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (SlideLayoutPart._partConstraint == null)
			{
				SlideLayoutPart._partConstraint = new Dictionary<string, PartConstraintRule>
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
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/slide",
						new PartConstraintRule("SlidePart", "application/vnd.openxmlformats-officedocument.presentationml.slide+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/slideMaster",
						new PartConstraintRule("SlideMasterPart", "application/vnd.openxmlformats-officedocument.presentationml.slideMaster+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/themeOverride",
						new PartConstraintRule("ThemeOverridePart", "application/vnd.openxmlformats-officedocument.themeOverride+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/tags",
						new PartConstraintRule("UserDefinedTagsPart", "application/vnd.openxmlformats-officedocument.presentationml.tags+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/control",
						new PartConstraintRule("EmbeddedControlPersistencePart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					}
				};
			}
			return SlideLayoutPart._partConstraint;
		}

		// Token: 0x0600D8C5 RID: 55493 RVA: 0x002AB780 File Offset: 0x002A9980
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (SlideLayoutPart._dataPartReferenceConstraint == null)
			{
				SlideLayoutPart._dataPartReferenceConstraint = new Dictionary<string, PartConstraintRule>
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
			return SlideLayoutPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D8C6 RID: 55494 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal SlideLayoutPart()
		{
		}

		// Token: 0x0600D8C7 RID: 55495 RVA: 0x002AB7F0 File Offset: 0x002A99F0
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
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/slide":
				return new SlidePart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/slideMaster":
				return new SlideMasterPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/themeOverride":
				return new ThemeOverridePart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/tags":
				return new UserDefinedTagsPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/control":
				return new EmbeddedControlPersistencePart();
			}
			throw new ArgumentOutOfRangeException("relationshipType");
		}

		// Token: 0x0600D8C8 RID: 55496 RVA: 0x002AB9D8 File Offset: 0x002A9BD8
		public CustomXmlPart AddCustomXmlPart(string contentType)
		{
			CustomXmlPart customXmlPart = new CustomXmlPart();
			base.InitPart<CustomXmlPart>(customXmlPart, contentType);
			return customXmlPart;
		}

		// Token: 0x0600D8C9 RID: 55497 RVA: 0x002AB9F4 File Offset: 0x002A9BF4
		public CustomXmlPart AddCustomXmlPart(CustomXmlPartType partType)
		{
			string contentType = CustomXmlPartTypeInfo.GetContentType(partType);
			string targetExtension = CustomXmlPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddCustomXmlPart(contentType);
		}

		// Token: 0x0600D8CA RID: 55498 RVA: 0x002ABA28 File Offset: 0x002A9C28
		public CustomXmlPart AddCustomXmlPart(string contentType, string id)
		{
			CustomXmlPart customXmlPart = new CustomXmlPart();
			this.InitPart<CustomXmlPart>(customXmlPart, contentType, id);
			return customXmlPart;
		}

		// Token: 0x0600D8CB RID: 55499 RVA: 0x002ABA48 File Offset: 0x002A9C48
		public CustomXmlPart AddCustomXmlPart(CustomXmlPartType partType, string id)
		{
			string contentType = CustomXmlPartTypeInfo.GetContentType(partType);
			string targetExtension = CustomXmlPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddCustomXmlPart(contentType, id);
		}

		// Token: 0x0600D8CC RID: 55500 RVA: 0x002A9DB5 File Offset: 0x002A7FB5
		public AudioReferenceRelationship AddAudioReferenceRelationship(MediaDataPart mediaDataPart)
		{
			return base.AddDataPartReferenceRelationship<AudioReferenceRelationship>(mediaDataPart);
		}

		// Token: 0x0600D8CD RID: 55501 RVA: 0x002A9DBE File Offset: 0x002A7FBE
		public AudioReferenceRelationship AddAudioReferenceRelationship(MediaDataPart mediaDataPart, string id)
		{
			return base.AddDataPartReferenceRelationship<AudioReferenceRelationship>(mediaDataPart, id);
		}

		// Token: 0x0600D8CE RID: 55502 RVA: 0x002ABA80 File Offset: 0x002A9C80
		public EmbeddedObjectPart AddEmbeddedObjectPart(string contentType)
		{
			EmbeddedObjectPart embeddedObjectPart = new EmbeddedObjectPart();
			base.InitPart<EmbeddedObjectPart>(embeddedObjectPart, contentType);
			return embeddedObjectPart;
		}

		// Token: 0x0600D8CF RID: 55503 RVA: 0x002ABA9C File Offset: 0x002A9C9C
		public EmbeddedPackagePart AddEmbeddedPackagePart(string contentType)
		{
			EmbeddedPackagePart embeddedPackagePart = new EmbeddedPackagePart();
			base.InitPart<EmbeddedPackagePart>(embeddedPackagePart, contentType);
			return embeddedPackagePart;
		}

		// Token: 0x0600D8D0 RID: 55504 RVA: 0x002ABAB8 File Offset: 0x002A9CB8
		public ImagePart AddImagePart(string contentType)
		{
			ImagePart imagePart = new ImagePart();
			base.InitPart<ImagePart>(imagePart, contentType);
			return imagePart;
		}

		// Token: 0x0600D8D1 RID: 55505 RVA: 0x002ABAD4 File Offset: 0x002A9CD4
		public ImagePart AddImagePart(ImagePartType partType)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType);
		}

		// Token: 0x0600D8D2 RID: 55506 RVA: 0x002ABB08 File Offset: 0x002A9D08
		public ImagePart AddImagePart(string contentType, string id)
		{
			ImagePart imagePart = new ImagePart();
			this.InitPart<ImagePart>(imagePart, contentType, id);
			return imagePart;
		}

		// Token: 0x0600D8D3 RID: 55507 RVA: 0x002ABB28 File Offset: 0x002A9D28
		public ImagePart AddImagePart(ImagePartType partType, string id)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType, id);
		}

		// Token: 0x0600D8D4 RID: 55508 RVA: 0x002A3EF9 File Offset: 0x002A20F9
		public VideoReferenceRelationship AddVideoReferenceRelationship(MediaDataPart mediaDataPart)
		{
			return base.AddDataPartReferenceRelationship<VideoReferenceRelationship>(mediaDataPart);
		}

		// Token: 0x0600D8D5 RID: 55509 RVA: 0x002A3F02 File Offset: 0x002A2102
		public VideoReferenceRelationship AddVideoReferenceRelationship(MediaDataPart mediaDataPart, string id)
		{
			return base.AddDataPartReferenceRelationship<VideoReferenceRelationship>(mediaDataPart, id);
		}

		// Token: 0x0600D8D6 RID: 55510 RVA: 0x002ABB60 File Offset: 0x002A9D60
		public EmbeddedControlPersistenceBinaryDataPart AddEmbeddedControlPersistenceBinaryDataPart(string contentType)
		{
			EmbeddedControlPersistenceBinaryDataPart embeddedControlPersistenceBinaryDataPart = new EmbeddedControlPersistenceBinaryDataPart();
			base.InitPart<EmbeddedControlPersistenceBinaryDataPart>(embeddedControlPersistenceBinaryDataPart, contentType);
			return embeddedControlPersistenceBinaryDataPart;
		}

		// Token: 0x0600D8D7 RID: 55511 RVA: 0x002ABB7C File Offset: 0x002A9D7C
		public EmbeddedControlPersistenceBinaryDataPart AddEmbeddedControlPersistenceBinaryDataPart(EmbeddedControlPersistenceBinaryDataPartType partType)
		{
			string contentType = EmbeddedControlPersistenceBinaryDataPartTypeInfo.GetContentType(partType);
			string targetExtension = EmbeddedControlPersistenceBinaryDataPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddEmbeddedControlPersistenceBinaryDataPart(contentType);
		}

		// Token: 0x0600D8D8 RID: 55512 RVA: 0x002ABBB0 File Offset: 0x002A9DB0
		public EmbeddedControlPersistenceBinaryDataPart AddEmbeddedControlPersistenceBinaryDataPart(string contentType, string id)
		{
			EmbeddedControlPersistenceBinaryDataPart embeddedControlPersistenceBinaryDataPart = new EmbeddedControlPersistenceBinaryDataPart();
			this.InitPart<EmbeddedControlPersistenceBinaryDataPart>(embeddedControlPersistenceBinaryDataPart, contentType, id);
			return embeddedControlPersistenceBinaryDataPart;
		}

		// Token: 0x0600D8D9 RID: 55513 RVA: 0x002ABBD0 File Offset: 0x002A9DD0
		public EmbeddedControlPersistenceBinaryDataPart AddEmbeddedControlPersistenceBinaryDataPart(EmbeddedControlPersistenceBinaryDataPartType partType, string id)
		{
			string contentType = EmbeddedControlPersistenceBinaryDataPartTypeInfo.GetContentType(partType);
			string targetExtension = EmbeddedControlPersistenceBinaryDataPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddEmbeddedControlPersistenceBinaryDataPart(contentType, id);
		}

		// Token: 0x0600D8DA RID: 55514 RVA: 0x002ABC08 File Offset: 0x002A9E08
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(string contentType)
		{
			EmbeddedControlPersistencePart embeddedControlPersistencePart = new EmbeddedControlPersistencePart();
			base.InitPart<EmbeddedControlPersistencePart>(embeddedControlPersistencePart, contentType);
			return embeddedControlPersistencePart;
		}

		// Token: 0x0600D8DB RID: 55515 RVA: 0x002ABC24 File Offset: 0x002A9E24
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(EmbeddedControlPersistencePartType partType)
		{
			string contentType = EmbeddedControlPersistencePartTypeInfo.GetContentType(partType);
			string targetExtension = EmbeddedControlPersistencePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddEmbeddedControlPersistencePart(contentType);
		}

		// Token: 0x0600D8DC RID: 55516 RVA: 0x002ABC58 File Offset: 0x002A9E58
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(string contentType, string id)
		{
			EmbeddedControlPersistencePart embeddedControlPersistencePart = new EmbeddedControlPersistencePart();
			this.InitPart<EmbeddedControlPersistencePart>(embeddedControlPersistencePart, contentType, id);
			return embeddedControlPersistencePart;
		}

		// Token: 0x0600D8DD RID: 55517 RVA: 0x002ABC78 File Offset: 0x002A9E78
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(EmbeddedControlPersistencePartType partType, string id)
		{
			string contentType = EmbeddedControlPersistencePartTypeInfo.GetContentType(partType);
			string targetExtension = EmbeddedControlPersistencePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddEmbeddedControlPersistencePart(contentType, id);
		}

		// Token: 0x0600D8DE RID: 55518 RVA: 0x002AB4FD File Offset: 0x002A96FD
		public MediaReferenceRelationship AddMediaReferenceRelationship(MediaDataPart mediaDataPart)
		{
			return base.AddDataPartReferenceRelationship<MediaReferenceRelationship>(mediaDataPart);
		}

		// Token: 0x0600D8DF RID: 55519 RVA: 0x002AB506 File Offset: 0x002A9706
		public MediaReferenceRelationship AddMediaReferenceRelationship(MediaDataPart mediaDataPart, string id)
		{
			return base.AddDataPartReferenceRelationship<MediaReferenceRelationship>(mediaDataPart, id);
		}

		// Token: 0x170035CF RID: 13775
		// (get) Token: 0x0600D8E0 RID: 55520 RVA: 0x002ABCAD File Offset: 0x002A9EAD
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/slideLayout";
			}
		}

		// Token: 0x170035D0 RID: 13776
		// (get) Token: 0x0600D8E1 RID: 55521 RVA: 0x002ABCB4 File Offset: 0x002A9EB4
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.presentationml.slideLayout+xml";
			}
		}

		// Token: 0x170035D1 RID: 13777
		// (get) Token: 0x0600D8E2 RID: 55522 RVA: 0x002ABCBB File Offset: 0x002A9EBB
		internal sealed override string TargetPath
		{
			get
			{
				return "../slideLayouts";
			}
		}

		// Token: 0x170035D2 RID: 13778
		// (get) Token: 0x0600D8E3 RID: 55523 RVA: 0x002ABCC2 File Offset: 0x002A9EC2
		internal sealed override string TargetName
		{
			get
			{
				return "slideLayout";
			}
		}

		// Token: 0x170035D3 RID: 13779
		// (get) Token: 0x0600D8E4 RID: 55524 RVA: 0x002A3F21 File Offset: 0x002A2121
		public IEnumerable<CustomXmlPart> CustomXmlParts
		{
			get
			{
				return base.GetPartsOfType<CustomXmlPart>();
			}
		}

		// Token: 0x170035D4 RID: 13780
		// (get) Token: 0x0600D8E5 RID: 55525 RVA: 0x002A3FB9 File Offset: 0x002A21B9
		public IEnumerable<ChartPart> ChartParts
		{
			get
			{
				return base.GetPartsOfType<ChartPart>();
			}
		}

		// Token: 0x170035D5 RID: 13781
		// (get) Token: 0x0600D8E6 RID: 55526 RVA: 0x002A3FC1 File Offset: 0x002A21C1
		public IEnumerable<DiagramColorsPart> DiagramColorsParts
		{
			get
			{
				return base.GetPartsOfType<DiagramColorsPart>();
			}
		}

		// Token: 0x170035D6 RID: 13782
		// (get) Token: 0x0600D8E7 RID: 55527 RVA: 0x002A3FC9 File Offset: 0x002A21C9
		public IEnumerable<DiagramDataPart> DiagramDataParts
		{
			get
			{
				return base.GetPartsOfType<DiagramDataPart>();
			}
		}

		// Token: 0x170035D7 RID: 13783
		// (get) Token: 0x0600D8E8 RID: 55528 RVA: 0x002A3FD1 File Offset: 0x002A21D1
		public IEnumerable<DiagramPersistLayoutPart> DiagramPersistLayoutParts
		{
			get
			{
				return base.GetPartsOfType<DiagramPersistLayoutPart>();
			}
		}

		// Token: 0x170035D8 RID: 13784
		// (get) Token: 0x0600D8E9 RID: 55529 RVA: 0x002A3FD9 File Offset: 0x002A21D9
		public IEnumerable<DiagramLayoutDefinitionPart> DiagramLayoutDefinitionParts
		{
			get
			{
				return base.GetPartsOfType<DiagramLayoutDefinitionPart>();
			}
		}

		// Token: 0x170035D9 RID: 13785
		// (get) Token: 0x0600D8EA RID: 55530 RVA: 0x002A3FE1 File Offset: 0x002A21E1
		public IEnumerable<DiagramStylePart> DiagramStyleParts
		{
			get
			{
				return base.GetPartsOfType<DiagramStylePart>();
			}
		}

		// Token: 0x170035DA RID: 13786
		// (get) Token: 0x0600D8EB RID: 55531 RVA: 0x002A3FF1 File Offset: 0x002A21F1
		public IEnumerable<EmbeddedObjectPart> EmbeddedObjectParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedObjectPart>();
			}
		}

		// Token: 0x170035DB RID: 13787
		// (get) Token: 0x0600D8EC RID: 55532 RVA: 0x002A3FF9 File Offset: 0x002A21F9
		public IEnumerable<EmbeddedPackagePart> EmbeddedPackageParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedPackagePart>();
			}
		}

		// Token: 0x170035DC RID: 13788
		// (get) Token: 0x0600D8ED RID: 55533 RVA: 0x002A4001 File Offset: 0x002A2201
		public IEnumerable<ImagePart> ImageParts
		{
			get
			{
				return base.GetPartsOfType<ImagePart>();
			}
		}

		// Token: 0x170035DD RID: 13789
		// (get) Token: 0x0600D8EE RID: 55534 RVA: 0x002A780D File Offset: 0x002A5A0D
		public IEnumerable<VmlDrawingPart> VmlDrawingParts
		{
			get
			{
				return base.GetPartsOfType<VmlDrawingPart>();
			}
		}

		// Token: 0x170035DE RID: 13790
		// (get) Token: 0x0600D8EF RID: 55535 RVA: 0x002A9132 File Offset: 0x002A7332
		public IEnumerable<EmbeddedControlPersistenceBinaryDataPart> EmbeddedControlPersistenceBinaryDataParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedControlPersistenceBinaryDataPart>();
			}
		}

		// Token: 0x170035DF RID: 13791
		// (get) Token: 0x0600D8F0 RID: 55536 RVA: 0x002A9797 File Offset: 0x002A7997
		public IEnumerable<SlidePart> SlideParts
		{
			get
			{
				return base.GetPartsOfType<SlidePart>();
			}
		}

		// Token: 0x170035E0 RID: 13792
		// (get) Token: 0x0600D8F1 RID: 55537 RVA: 0x002ABCC9 File Offset: 0x002A9EC9
		public SlideMasterPart SlideMasterPart
		{
			get
			{
				return base.GetSubPartOfType<SlideMasterPart>();
			}
		}

		// Token: 0x170035E1 RID: 13793
		// (get) Token: 0x0600D8F2 RID: 55538 RVA: 0x002AAC61 File Offset: 0x002A8E61
		public ThemeOverridePart ThemeOverridePart
		{
			get
			{
				return base.GetSubPartOfType<ThemeOverridePart>();
			}
		}

		// Token: 0x170035E2 RID: 13794
		// (get) Token: 0x0600D8F3 RID: 55539 RVA: 0x002A9F69 File Offset: 0x002A8169
		public IEnumerable<UserDefinedTagsPart> UserDefinedTagsParts
		{
			get
			{
				return base.GetPartsOfType<UserDefinedTagsPart>();
			}
		}

		// Token: 0x170035E3 RID: 13795
		// (get) Token: 0x0600D8F4 RID: 55540 RVA: 0x002A3FE9 File Offset: 0x002A21E9
		public IEnumerable<EmbeddedControlPersistencePart> EmbeddedControlPersistenceParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedControlPersistencePart>();
			}
		}

		// Token: 0x170035E4 RID: 13796
		// (get) Token: 0x0600D8F5 RID: 55541 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170035E5 RID: 13797
		// (get) Token: 0x0600D8F6 RID: 55542 RVA: 0x002ABCD1 File Offset: 0x002A9ED1
		// (set) Token: 0x0600D8F7 RID: 55543 RVA: 0x002ABCD9 File Offset: 0x002A9ED9
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as SlideLayout;
			}
		}

		// Token: 0x170035E6 RID: 13798
		// (get) Token: 0x0600D8F8 RID: 55544 RVA: 0x002ABCE7 File Offset: 0x002A9EE7
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.SlideLayout;
			}
		}

		// Token: 0x170035E7 RID: 13799
		// (get) Token: 0x0600D8F9 RID: 55545 RVA: 0x002ABCEF File Offset: 0x002A9EEF
		// (set) Token: 0x0600D8FA RID: 55546 RVA: 0x002A3296 File Offset: 0x002A1496
		public SlideLayout SlideLayout
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<SlideLayout>();
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

		// Token: 0x04006B36 RID: 27446
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/slideLayout";

		// Token: 0x04006B37 RID: 27447
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.presentationml.slideLayout+xml";

		// Token: 0x04006B38 RID: 27448
		internal const string TargetPathConstant = "../slideLayouts";

		// Token: 0x04006B39 RID: 27449
		internal const string TargetNameConstant = "slideLayout";

		// Token: 0x04006B3A RID: 27450
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006B3B RID: 27451
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006B3C RID: 27452
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private SlideLayout _rootEle;
	}
}
