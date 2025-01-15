using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Presentation;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002188 RID: 8584
	internal class NotesSlidePart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D84D RID: 55373 RVA: 0x002AA60C File Offset: 0x002A880C
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (NotesSlidePart._partConstraint == null)
			{
				NotesSlidePart._partConstraint = new Dictionary<string, PartConstraintRule>
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
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/notesMaster",
						new PartConstraintRule("NotesMasterPart", "application/vnd.openxmlformats-officedocument.presentationml.notesMaster+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/themeOverride",
						new PartConstraintRule("ThemeOverridePart", "application/vnd.openxmlformats-officedocument.themeOverride+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/slide",
						new PartConstraintRule("SlidePart", "application/vnd.openxmlformats-officedocument.presentationml.slide+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/tags",
						new PartConstraintRule("UserDefinedTagsPart", "application/vnd.openxmlformats-officedocument.presentationml.tags+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					}
				};
			}
			return NotesSlidePart._partConstraint;
		}

		// Token: 0x0600D84E RID: 55374 RVA: 0x002AA7F0 File Offset: 0x002A89F0
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (NotesSlidePart._dataPartReferenceConstraint == null)
			{
				NotesSlidePart._dataPartReferenceConstraint = new Dictionary<string, PartConstraintRule>
				{
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/audio",
						new PartConstraintRule("AudioReferenceRelationship", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/video",
						new PartConstraintRule("VideoReferenceRelationship", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					}
				};
			}
			return NotesSlidePart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D84F RID: 55375 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal NotesSlidePart()
		{
		}

		// Token: 0x0600D850 RID: 55376 RVA: 0x002AA848 File Offset: 0x002A8A48
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
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/notesMaster":
				return new NotesMasterPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/themeOverride":
				return new ThemeOverridePart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/slide":
				return new SlidePart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/tags":
				return new UserDefinedTagsPart();
			}
			throw new ArgumentOutOfRangeException("relationshipType");
		}

		// Token: 0x0600D851 RID: 55377 RVA: 0x002AAA18 File Offset: 0x002A8C18
		public CustomXmlPart AddCustomXmlPart(string contentType)
		{
			CustomXmlPart customXmlPart = new CustomXmlPart();
			base.InitPart<CustomXmlPart>(customXmlPart, contentType);
			return customXmlPart;
		}

		// Token: 0x0600D852 RID: 55378 RVA: 0x002AAA34 File Offset: 0x002A8C34
		public CustomXmlPart AddCustomXmlPart(CustomXmlPartType partType)
		{
			string contentType = CustomXmlPartTypeInfo.GetContentType(partType);
			string targetExtension = CustomXmlPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddCustomXmlPart(contentType);
		}

		// Token: 0x0600D853 RID: 55379 RVA: 0x002AAA68 File Offset: 0x002A8C68
		public CustomXmlPart AddCustomXmlPart(string contentType, string id)
		{
			CustomXmlPart customXmlPart = new CustomXmlPart();
			this.InitPart<CustomXmlPart>(customXmlPart, contentType, id);
			return customXmlPart;
		}

		// Token: 0x0600D854 RID: 55380 RVA: 0x002AAA88 File Offset: 0x002A8C88
		public CustomXmlPart AddCustomXmlPart(CustomXmlPartType partType, string id)
		{
			string contentType = CustomXmlPartTypeInfo.GetContentType(partType);
			string targetExtension = CustomXmlPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddCustomXmlPart(contentType, id);
		}

		// Token: 0x0600D855 RID: 55381 RVA: 0x002A9DB5 File Offset: 0x002A7FB5
		public AudioReferenceRelationship AddAudioReferenceRelationship(MediaDataPart mediaDataPart)
		{
			return base.AddDataPartReferenceRelationship<AudioReferenceRelationship>(mediaDataPart);
		}

		// Token: 0x0600D856 RID: 55382 RVA: 0x002A9DBE File Offset: 0x002A7FBE
		public AudioReferenceRelationship AddAudioReferenceRelationship(MediaDataPart mediaDataPart, string id)
		{
			return base.AddDataPartReferenceRelationship<AudioReferenceRelationship>(mediaDataPart, id);
		}

		// Token: 0x0600D857 RID: 55383 RVA: 0x002AAAC0 File Offset: 0x002A8CC0
		public EmbeddedObjectPart AddEmbeddedObjectPart(string contentType)
		{
			EmbeddedObjectPart embeddedObjectPart = new EmbeddedObjectPart();
			base.InitPart<EmbeddedObjectPart>(embeddedObjectPart, contentType);
			return embeddedObjectPart;
		}

		// Token: 0x0600D858 RID: 55384 RVA: 0x002AAADC File Offset: 0x002A8CDC
		public EmbeddedPackagePart AddEmbeddedPackagePart(string contentType)
		{
			EmbeddedPackagePart embeddedPackagePart = new EmbeddedPackagePart();
			base.InitPart<EmbeddedPackagePart>(embeddedPackagePart, contentType);
			return embeddedPackagePart;
		}

		// Token: 0x0600D859 RID: 55385 RVA: 0x002AAAF8 File Offset: 0x002A8CF8
		public ImagePart AddImagePart(string contentType)
		{
			ImagePart imagePart = new ImagePart();
			base.InitPart<ImagePart>(imagePart, contentType);
			return imagePart;
		}

		// Token: 0x0600D85A RID: 55386 RVA: 0x002AAB14 File Offset: 0x002A8D14
		public ImagePart AddImagePart(ImagePartType partType)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType);
		}

		// Token: 0x0600D85B RID: 55387 RVA: 0x002AAB48 File Offset: 0x002A8D48
		public ImagePart AddImagePart(string contentType, string id)
		{
			ImagePart imagePart = new ImagePart();
			this.InitPart<ImagePart>(imagePart, contentType, id);
			return imagePart;
		}

		// Token: 0x0600D85C RID: 55388 RVA: 0x002AAB68 File Offset: 0x002A8D68
		public ImagePart AddImagePart(ImagePartType partType, string id)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType, id);
		}

		// Token: 0x0600D85D RID: 55389 RVA: 0x002A3EF9 File Offset: 0x002A20F9
		public VideoReferenceRelationship AddVideoReferenceRelationship(MediaDataPart mediaDataPart)
		{
			return base.AddDataPartReferenceRelationship<VideoReferenceRelationship>(mediaDataPart);
		}

		// Token: 0x0600D85E RID: 55390 RVA: 0x002A3F02 File Offset: 0x002A2102
		public VideoReferenceRelationship AddVideoReferenceRelationship(MediaDataPart mediaDataPart, string id)
		{
			return base.AddDataPartReferenceRelationship<VideoReferenceRelationship>(mediaDataPart, id);
		}

		// Token: 0x0600D85F RID: 55391 RVA: 0x002AABA0 File Offset: 0x002A8DA0
		public EmbeddedControlPersistenceBinaryDataPart AddEmbeddedControlPersistenceBinaryDataPart(string contentType)
		{
			EmbeddedControlPersistenceBinaryDataPart embeddedControlPersistenceBinaryDataPart = new EmbeddedControlPersistenceBinaryDataPart();
			base.InitPart<EmbeddedControlPersistenceBinaryDataPart>(embeddedControlPersistenceBinaryDataPart, contentType);
			return embeddedControlPersistenceBinaryDataPart;
		}

		// Token: 0x0600D860 RID: 55392 RVA: 0x002AABBC File Offset: 0x002A8DBC
		public EmbeddedControlPersistenceBinaryDataPart AddEmbeddedControlPersistenceBinaryDataPart(EmbeddedControlPersistenceBinaryDataPartType partType)
		{
			string contentType = EmbeddedControlPersistenceBinaryDataPartTypeInfo.GetContentType(partType);
			string targetExtension = EmbeddedControlPersistenceBinaryDataPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddEmbeddedControlPersistenceBinaryDataPart(contentType);
		}

		// Token: 0x0600D861 RID: 55393 RVA: 0x002AABF0 File Offset: 0x002A8DF0
		public EmbeddedControlPersistenceBinaryDataPart AddEmbeddedControlPersistenceBinaryDataPart(string contentType, string id)
		{
			EmbeddedControlPersistenceBinaryDataPart embeddedControlPersistenceBinaryDataPart = new EmbeddedControlPersistenceBinaryDataPart();
			this.InitPart<EmbeddedControlPersistenceBinaryDataPart>(embeddedControlPersistenceBinaryDataPart, contentType, id);
			return embeddedControlPersistenceBinaryDataPart;
		}

		// Token: 0x0600D862 RID: 55394 RVA: 0x002AAC10 File Offset: 0x002A8E10
		public EmbeddedControlPersistenceBinaryDataPart AddEmbeddedControlPersistenceBinaryDataPart(EmbeddedControlPersistenceBinaryDataPartType partType, string id)
		{
			string contentType = EmbeddedControlPersistenceBinaryDataPartTypeInfo.GetContentType(partType);
			string targetExtension = EmbeddedControlPersistenceBinaryDataPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddEmbeddedControlPersistenceBinaryDataPart(contentType, id);
		}

		// Token: 0x17003593 RID: 13715
		// (get) Token: 0x0600D863 RID: 55395 RVA: 0x002AAC45 File Offset: 0x002A8E45
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/notesSlide";
			}
		}

		// Token: 0x17003594 RID: 13716
		// (get) Token: 0x0600D864 RID: 55396 RVA: 0x002AAC4C File Offset: 0x002A8E4C
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.presentationml.notesSlide+xml";
			}
		}

		// Token: 0x17003595 RID: 13717
		// (get) Token: 0x0600D865 RID: 55397 RVA: 0x002AAC53 File Offset: 0x002A8E53
		internal sealed override string TargetPath
		{
			get
			{
				return "../notesSlides";
			}
		}

		// Token: 0x17003596 RID: 13718
		// (get) Token: 0x0600D866 RID: 55398 RVA: 0x002AAC5A File Offset: 0x002A8E5A
		internal sealed override string TargetName
		{
			get
			{
				return "notesSlide";
			}
		}

		// Token: 0x17003597 RID: 13719
		// (get) Token: 0x0600D867 RID: 55399 RVA: 0x002A3F21 File Offset: 0x002A2121
		public IEnumerable<CustomXmlPart> CustomXmlParts
		{
			get
			{
				return base.GetPartsOfType<CustomXmlPart>();
			}
		}

		// Token: 0x17003598 RID: 13720
		// (get) Token: 0x0600D868 RID: 55400 RVA: 0x002A3FB9 File Offset: 0x002A21B9
		public IEnumerable<ChartPart> ChartParts
		{
			get
			{
				return base.GetPartsOfType<ChartPart>();
			}
		}

		// Token: 0x17003599 RID: 13721
		// (get) Token: 0x0600D869 RID: 55401 RVA: 0x002A3FC1 File Offset: 0x002A21C1
		public IEnumerable<DiagramColorsPart> DiagramColorsParts
		{
			get
			{
				return base.GetPartsOfType<DiagramColorsPart>();
			}
		}

		// Token: 0x1700359A RID: 13722
		// (get) Token: 0x0600D86A RID: 55402 RVA: 0x002A3FC9 File Offset: 0x002A21C9
		public IEnumerable<DiagramDataPart> DiagramDataParts
		{
			get
			{
				return base.GetPartsOfType<DiagramDataPart>();
			}
		}

		// Token: 0x1700359B RID: 13723
		// (get) Token: 0x0600D86B RID: 55403 RVA: 0x002A3FD1 File Offset: 0x002A21D1
		public IEnumerable<DiagramPersistLayoutPart> DiagramPersistLayoutParts
		{
			get
			{
				return base.GetPartsOfType<DiagramPersistLayoutPart>();
			}
		}

		// Token: 0x1700359C RID: 13724
		// (get) Token: 0x0600D86C RID: 55404 RVA: 0x002A3FD9 File Offset: 0x002A21D9
		public IEnumerable<DiagramLayoutDefinitionPart> DiagramLayoutDefinitionParts
		{
			get
			{
				return base.GetPartsOfType<DiagramLayoutDefinitionPart>();
			}
		}

		// Token: 0x1700359D RID: 13725
		// (get) Token: 0x0600D86D RID: 55405 RVA: 0x002A3FE1 File Offset: 0x002A21E1
		public IEnumerable<DiagramStylePart> DiagramStyleParts
		{
			get
			{
				return base.GetPartsOfType<DiagramStylePart>();
			}
		}

		// Token: 0x1700359E RID: 13726
		// (get) Token: 0x0600D86E RID: 55406 RVA: 0x002A3FF1 File Offset: 0x002A21F1
		public IEnumerable<EmbeddedObjectPart> EmbeddedObjectParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedObjectPart>();
			}
		}

		// Token: 0x1700359F RID: 13727
		// (get) Token: 0x0600D86F RID: 55407 RVA: 0x002A3FF9 File Offset: 0x002A21F9
		public IEnumerable<EmbeddedPackagePart> EmbeddedPackageParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedPackagePart>();
			}
		}

		// Token: 0x170035A0 RID: 13728
		// (get) Token: 0x0600D870 RID: 55408 RVA: 0x002A4001 File Offset: 0x002A2201
		public IEnumerable<ImagePart> ImageParts
		{
			get
			{
				return base.GetPartsOfType<ImagePart>();
			}
		}

		// Token: 0x170035A1 RID: 13729
		// (get) Token: 0x0600D871 RID: 55409 RVA: 0x002A780D File Offset: 0x002A5A0D
		public IEnumerable<VmlDrawingPart> VmlDrawingParts
		{
			get
			{
				return base.GetPartsOfType<VmlDrawingPart>();
			}
		}

		// Token: 0x170035A2 RID: 13730
		// (get) Token: 0x0600D872 RID: 55410 RVA: 0x002A9132 File Offset: 0x002A7332
		public IEnumerable<EmbeddedControlPersistenceBinaryDataPart> EmbeddedControlPersistenceBinaryDataParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedControlPersistenceBinaryDataPart>();
			}
		}

		// Token: 0x170035A3 RID: 13731
		// (get) Token: 0x0600D873 RID: 55411 RVA: 0x002A978F File Offset: 0x002A798F
		public NotesMasterPart NotesMasterPart
		{
			get
			{
				return base.GetSubPartOfType<NotesMasterPart>();
			}
		}

		// Token: 0x170035A4 RID: 13732
		// (get) Token: 0x0600D874 RID: 55412 RVA: 0x002AAC61 File Offset: 0x002A8E61
		public ThemeOverridePart ThemeOverridePart
		{
			get
			{
				return base.GetSubPartOfType<ThemeOverridePart>();
			}
		}

		// Token: 0x170035A5 RID: 13733
		// (get) Token: 0x0600D875 RID: 55413 RVA: 0x002A9F71 File Offset: 0x002A8171
		public SlidePart SlidePart
		{
			get
			{
				return base.GetSubPartOfType<SlidePart>();
			}
		}

		// Token: 0x170035A6 RID: 13734
		// (get) Token: 0x0600D876 RID: 55414 RVA: 0x002A9F69 File Offset: 0x002A8169
		public IEnumerable<UserDefinedTagsPart> UserDefinedTagsParts
		{
			get
			{
				return base.GetPartsOfType<UserDefinedTagsPart>();
			}
		}

		// Token: 0x170035A7 RID: 13735
		// (get) Token: 0x0600D877 RID: 55415 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170035A8 RID: 13736
		// (get) Token: 0x0600D878 RID: 55416 RVA: 0x002AAC69 File Offset: 0x002A8E69
		// (set) Token: 0x0600D879 RID: 55417 RVA: 0x002AAC71 File Offset: 0x002A8E71
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as NotesSlide;
			}
		}

		// Token: 0x170035A9 RID: 13737
		// (get) Token: 0x0600D87A RID: 55418 RVA: 0x002AAC7F File Offset: 0x002A8E7F
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.NotesSlide;
			}
		}

		// Token: 0x170035AA RID: 13738
		// (get) Token: 0x0600D87B RID: 55419 RVA: 0x002AAC87 File Offset: 0x002A8E87
		// (set) Token: 0x0600D87C RID: 55420 RVA: 0x002A3296 File Offset: 0x002A1496
		public NotesSlide NotesSlide
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<NotesSlide>();
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

		// Token: 0x04006B21 RID: 27425
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/notesSlide";

		// Token: 0x04006B22 RID: 27426
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.presentationml.notesSlide+xml";

		// Token: 0x04006B23 RID: 27427
		internal const string TargetPathConstant = "../notesSlides";

		// Token: 0x04006B24 RID: 27428
		internal const string TargetNameConstant = "notesSlide";

		// Token: 0x04006B25 RID: 27429
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006B26 RID: 27430
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006B27 RID: 27431
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private NotesSlide _rootEle;
	}
}
