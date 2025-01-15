using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Presentation;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002187 RID: 8583
	internal class NotesMasterPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D81E RID: 55326 RVA: 0x002A9FB0 File Offset: 0x002A81B0
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (NotesMasterPart._partConstraint == null)
			{
				NotesMasterPart._partConstraint = new Dictionary<string, PartConstraintRule>
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
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/theme",
						new PartConstraintRule("ThemePart", "application/vnd.openxmlformats-officedocument.theme+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/tags",
						new PartConstraintRule("UserDefinedTagsPart", "application/vnd.openxmlformats-officedocument.presentationml.tags+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/slide",
						new PartConstraintRule("SlidePart", "application/vnd.openxmlformats-officedocument.presentationml.slide+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					}
				};
			}
			return NotesMasterPart._partConstraint;
		}

		// Token: 0x0600D81F RID: 55327 RVA: 0x002AA178 File Offset: 0x002A8378
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (NotesMasterPart._dataPartReferenceConstraint == null)
			{
				NotesMasterPart._dataPartReferenceConstraint = new Dictionary<string, PartConstraintRule>
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
			return NotesMasterPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D820 RID: 55328 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal NotesMasterPart()
		{
		}

		// Token: 0x0600D821 RID: 55329 RVA: 0x002AA1D0 File Offset: 0x002A83D0
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
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/theme":
				return new ThemePart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/tags":
				return new UserDefinedTagsPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/slide":
				return new SlidePart();
			}
			throw new ArgumentOutOfRangeException("relationshipType");
		}

		// Token: 0x0600D822 RID: 55330 RVA: 0x002AA38C File Offset: 0x002A858C
		public CustomXmlPart AddCustomXmlPart(string contentType)
		{
			CustomXmlPart customXmlPart = new CustomXmlPart();
			base.InitPart<CustomXmlPart>(customXmlPart, contentType);
			return customXmlPart;
		}

		// Token: 0x0600D823 RID: 55331 RVA: 0x002AA3A8 File Offset: 0x002A85A8
		public CustomXmlPart AddCustomXmlPart(CustomXmlPartType partType)
		{
			string contentType = CustomXmlPartTypeInfo.GetContentType(partType);
			string targetExtension = CustomXmlPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddCustomXmlPart(contentType);
		}

		// Token: 0x0600D824 RID: 55332 RVA: 0x002AA3DC File Offset: 0x002A85DC
		public CustomXmlPart AddCustomXmlPart(string contentType, string id)
		{
			CustomXmlPart customXmlPart = new CustomXmlPart();
			this.InitPart<CustomXmlPart>(customXmlPart, contentType, id);
			return customXmlPart;
		}

		// Token: 0x0600D825 RID: 55333 RVA: 0x002AA3FC File Offset: 0x002A85FC
		public CustomXmlPart AddCustomXmlPart(CustomXmlPartType partType, string id)
		{
			string contentType = CustomXmlPartTypeInfo.GetContentType(partType);
			string targetExtension = CustomXmlPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddCustomXmlPart(contentType, id);
		}

		// Token: 0x0600D826 RID: 55334 RVA: 0x002A9DB5 File Offset: 0x002A7FB5
		public AudioReferenceRelationship AddAudioReferenceRelationship(MediaDataPart mediaDataPart)
		{
			return base.AddDataPartReferenceRelationship<AudioReferenceRelationship>(mediaDataPart);
		}

		// Token: 0x0600D827 RID: 55335 RVA: 0x002A9DBE File Offset: 0x002A7FBE
		public AudioReferenceRelationship AddAudioReferenceRelationship(MediaDataPart mediaDataPart, string id)
		{
			return base.AddDataPartReferenceRelationship<AudioReferenceRelationship>(mediaDataPart, id);
		}

		// Token: 0x0600D828 RID: 55336 RVA: 0x002AA434 File Offset: 0x002A8634
		public EmbeddedObjectPart AddEmbeddedObjectPart(string contentType)
		{
			EmbeddedObjectPart embeddedObjectPart = new EmbeddedObjectPart();
			base.InitPart<EmbeddedObjectPart>(embeddedObjectPart, contentType);
			return embeddedObjectPart;
		}

		// Token: 0x0600D829 RID: 55337 RVA: 0x002AA450 File Offset: 0x002A8650
		public EmbeddedPackagePart AddEmbeddedPackagePart(string contentType)
		{
			EmbeddedPackagePart embeddedPackagePart = new EmbeddedPackagePart();
			base.InitPart<EmbeddedPackagePart>(embeddedPackagePart, contentType);
			return embeddedPackagePart;
		}

		// Token: 0x0600D82A RID: 55338 RVA: 0x002AA46C File Offset: 0x002A866C
		public ImagePart AddImagePart(string contentType)
		{
			ImagePart imagePart = new ImagePart();
			base.InitPart<ImagePart>(imagePart, contentType);
			return imagePart;
		}

		// Token: 0x0600D82B RID: 55339 RVA: 0x002AA488 File Offset: 0x002A8688
		public ImagePart AddImagePart(ImagePartType partType)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType);
		}

		// Token: 0x0600D82C RID: 55340 RVA: 0x002AA4BC File Offset: 0x002A86BC
		public ImagePart AddImagePart(string contentType, string id)
		{
			ImagePart imagePart = new ImagePart();
			this.InitPart<ImagePart>(imagePart, contentType, id);
			return imagePart;
		}

		// Token: 0x0600D82D RID: 55341 RVA: 0x002AA4DC File Offset: 0x002A86DC
		public ImagePart AddImagePart(ImagePartType partType, string id)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType, id);
		}

		// Token: 0x0600D82E RID: 55342 RVA: 0x002A3EF9 File Offset: 0x002A20F9
		public VideoReferenceRelationship AddVideoReferenceRelationship(MediaDataPart mediaDataPart)
		{
			return base.AddDataPartReferenceRelationship<VideoReferenceRelationship>(mediaDataPart);
		}

		// Token: 0x0600D82F RID: 55343 RVA: 0x002A3F02 File Offset: 0x002A2102
		public VideoReferenceRelationship AddVideoReferenceRelationship(MediaDataPart mediaDataPart, string id)
		{
			return base.AddDataPartReferenceRelationship<VideoReferenceRelationship>(mediaDataPart, id);
		}

		// Token: 0x0600D830 RID: 55344 RVA: 0x002AA514 File Offset: 0x002A8714
		public EmbeddedControlPersistenceBinaryDataPart AddEmbeddedControlPersistenceBinaryDataPart(string contentType)
		{
			EmbeddedControlPersistenceBinaryDataPart embeddedControlPersistenceBinaryDataPart = new EmbeddedControlPersistenceBinaryDataPart();
			base.InitPart<EmbeddedControlPersistenceBinaryDataPart>(embeddedControlPersistenceBinaryDataPart, contentType);
			return embeddedControlPersistenceBinaryDataPart;
		}

		// Token: 0x0600D831 RID: 55345 RVA: 0x002AA530 File Offset: 0x002A8730
		public EmbeddedControlPersistenceBinaryDataPart AddEmbeddedControlPersistenceBinaryDataPart(EmbeddedControlPersistenceBinaryDataPartType partType)
		{
			string contentType = EmbeddedControlPersistenceBinaryDataPartTypeInfo.GetContentType(partType);
			string targetExtension = EmbeddedControlPersistenceBinaryDataPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddEmbeddedControlPersistenceBinaryDataPart(contentType);
		}

		// Token: 0x0600D832 RID: 55346 RVA: 0x002AA564 File Offset: 0x002A8764
		public EmbeddedControlPersistenceBinaryDataPart AddEmbeddedControlPersistenceBinaryDataPart(string contentType, string id)
		{
			EmbeddedControlPersistenceBinaryDataPart embeddedControlPersistenceBinaryDataPart = new EmbeddedControlPersistenceBinaryDataPart();
			this.InitPart<EmbeddedControlPersistenceBinaryDataPart>(embeddedControlPersistenceBinaryDataPart, contentType, id);
			return embeddedControlPersistenceBinaryDataPart;
		}

		// Token: 0x0600D833 RID: 55347 RVA: 0x002AA584 File Offset: 0x002A8784
		public EmbeddedControlPersistenceBinaryDataPart AddEmbeddedControlPersistenceBinaryDataPart(EmbeddedControlPersistenceBinaryDataPartType partType, string id)
		{
			string contentType = EmbeddedControlPersistenceBinaryDataPartTypeInfo.GetContentType(partType);
			string targetExtension = EmbeddedControlPersistenceBinaryDataPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddEmbeddedControlPersistenceBinaryDataPart(contentType, id);
		}

		// Token: 0x1700357C RID: 13692
		// (get) Token: 0x0600D834 RID: 55348 RVA: 0x002AA5B9 File Offset: 0x002A87B9
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/notesMaster";
			}
		}

		// Token: 0x1700357D RID: 13693
		// (get) Token: 0x0600D835 RID: 55349 RVA: 0x002AA5C0 File Offset: 0x002A87C0
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.presentationml.notesMaster+xml";
			}
		}

		// Token: 0x1700357E RID: 13694
		// (get) Token: 0x0600D836 RID: 55350 RVA: 0x002AA5C7 File Offset: 0x002A87C7
		internal sealed override string TargetPath
		{
			get
			{
				return "notesMasters";
			}
		}

		// Token: 0x1700357F RID: 13695
		// (get) Token: 0x0600D837 RID: 55351 RVA: 0x002AA5CE File Offset: 0x002A87CE
		internal sealed override string TargetName
		{
			get
			{
				return "notesMaster";
			}
		}

		// Token: 0x17003580 RID: 13696
		// (get) Token: 0x0600D838 RID: 55352 RVA: 0x002A3F21 File Offset: 0x002A2121
		public IEnumerable<CustomXmlPart> CustomXmlParts
		{
			get
			{
				return base.GetPartsOfType<CustomXmlPart>();
			}
		}

		// Token: 0x17003581 RID: 13697
		// (get) Token: 0x0600D839 RID: 55353 RVA: 0x002A3FB9 File Offset: 0x002A21B9
		public IEnumerable<ChartPart> ChartParts
		{
			get
			{
				return base.GetPartsOfType<ChartPart>();
			}
		}

		// Token: 0x17003582 RID: 13698
		// (get) Token: 0x0600D83A RID: 55354 RVA: 0x002A3FC1 File Offset: 0x002A21C1
		public IEnumerable<DiagramColorsPart> DiagramColorsParts
		{
			get
			{
				return base.GetPartsOfType<DiagramColorsPart>();
			}
		}

		// Token: 0x17003583 RID: 13699
		// (get) Token: 0x0600D83B RID: 55355 RVA: 0x002A3FC9 File Offset: 0x002A21C9
		public IEnumerable<DiagramDataPart> DiagramDataParts
		{
			get
			{
				return base.GetPartsOfType<DiagramDataPart>();
			}
		}

		// Token: 0x17003584 RID: 13700
		// (get) Token: 0x0600D83C RID: 55356 RVA: 0x002A3FD1 File Offset: 0x002A21D1
		public IEnumerable<DiagramPersistLayoutPart> DiagramPersistLayoutParts
		{
			get
			{
				return base.GetPartsOfType<DiagramPersistLayoutPart>();
			}
		}

		// Token: 0x17003585 RID: 13701
		// (get) Token: 0x0600D83D RID: 55357 RVA: 0x002A3FD9 File Offset: 0x002A21D9
		public IEnumerable<DiagramLayoutDefinitionPart> DiagramLayoutDefinitionParts
		{
			get
			{
				return base.GetPartsOfType<DiagramLayoutDefinitionPart>();
			}
		}

		// Token: 0x17003586 RID: 13702
		// (get) Token: 0x0600D83E RID: 55358 RVA: 0x002A3FE1 File Offset: 0x002A21E1
		public IEnumerable<DiagramStylePart> DiagramStyleParts
		{
			get
			{
				return base.GetPartsOfType<DiagramStylePart>();
			}
		}

		// Token: 0x17003587 RID: 13703
		// (get) Token: 0x0600D83F RID: 55359 RVA: 0x002A3FF1 File Offset: 0x002A21F1
		public IEnumerable<EmbeddedObjectPart> EmbeddedObjectParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedObjectPart>();
			}
		}

		// Token: 0x17003588 RID: 13704
		// (get) Token: 0x0600D840 RID: 55360 RVA: 0x002A3FF9 File Offset: 0x002A21F9
		public IEnumerable<EmbeddedPackagePart> EmbeddedPackageParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedPackagePart>();
			}
		}

		// Token: 0x17003589 RID: 13705
		// (get) Token: 0x0600D841 RID: 55361 RVA: 0x002A4001 File Offset: 0x002A2201
		public IEnumerable<ImagePart> ImageParts
		{
			get
			{
				return base.GetPartsOfType<ImagePart>();
			}
		}

		// Token: 0x1700358A RID: 13706
		// (get) Token: 0x0600D842 RID: 55362 RVA: 0x002A780D File Offset: 0x002A5A0D
		public IEnumerable<VmlDrawingPart> VmlDrawingParts
		{
			get
			{
				return base.GetPartsOfType<VmlDrawingPart>();
			}
		}

		// Token: 0x1700358B RID: 13707
		// (get) Token: 0x0600D843 RID: 55363 RVA: 0x002A9132 File Offset: 0x002A7332
		public IEnumerable<EmbeddedControlPersistenceBinaryDataPart> EmbeddedControlPersistenceBinaryDataParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedControlPersistenceBinaryDataPart>();
			}
		}

		// Token: 0x1700358C RID: 13708
		// (get) Token: 0x0600D844 RID: 55364 RVA: 0x002A3F31 File Offset: 0x002A2131
		public ThemePart ThemePart
		{
			get
			{
				return base.GetSubPartOfType<ThemePart>();
			}
		}

		// Token: 0x1700358D RID: 13709
		// (get) Token: 0x0600D845 RID: 55365 RVA: 0x002A9F69 File Offset: 0x002A8169
		public IEnumerable<UserDefinedTagsPart> UserDefinedTagsParts
		{
			get
			{
				return base.GetPartsOfType<UserDefinedTagsPart>();
			}
		}

		// Token: 0x1700358E RID: 13710
		// (get) Token: 0x0600D846 RID: 55366 RVA: 0x002A9F71 File Offset: 0x002A8171
		public SlidePart SlidePart
		{
			get
			{
				return base.GetSubPartOfType<SlidePart>();
			}
		}

		// Token: 0x1700358F RID: 13711
		// (get) Token: 0x0600D847 RID: 55367 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003590 RID: 13712
		// (get) Token: 0x0600D848 RID: 55368 RVA: 0x002AA5D5 File Offset: 0x002A87D5
		// (set) Token: 0x0600D849 RID: 55369 RVA: 0x002AA5DD File Offset: 0x002A87DD
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as NotesMaster;
			}
		}

		// Token: 0x17003591 RID: 13713
		// (get) Token: 0x0600D84A RID: 55370 RVA: 0x002AA5EB File Offset: 0x002A87EB
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.NotesMaster;
			}
		}

		// Token: 0x17003592 RID: 13714
		// (get) Token: 0x0600D84B RID: 55371 RVA: 0x002AA5F3 File Offset: 0x002A87F3
		// (set) Token: 0x0600D84C RID: 55372 RVA: 0x002A3296 File Offset: 0x002A1496
		public NotesMaster NotesMaster
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<NotesMaster>();
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

		// Token: 0x04006B1A RID: 27418
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/notesMaster";

		// Token: 0x04006B1B RID: 27419
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.presentationml.notesMaster+xml";

		// Token: 0x04006B1C RID: 27420
		internal const string TargetPathConstant = "notesMasters";

		// Token: 0x04006B1D RID: 27421
		internal const string TargetNameConstant = "notesMaster";

		// Token: 0x04006B1E RID: 27422
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006B1F RID: 27423
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006B20 RID: 27424
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private NotesMaster _rootEle;
	}
}
