using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Presentation;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002186 RID: 8582
	internal class HandoutMasterPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D7EF RID: 55279 RVA: 0x002A9934 File Offset: 0x002A7B34
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (HandoutMasterPart._partConstraint == null)
			{
				HandoutMasterPart._partConstraint = new Dictionary<string, PartConstraintRule>
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
			return HandoutMasterPart._partConstraint;
		}

		// Token: 0x0600D7F0 RID: 55280 RVA: 0x002A9AFC File Offset: 0x002A7CFC
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (HandoutMasterPart._dataPartReferenceConstraint == null)
			{
				HandoutMasterPart._dataPartReferenceConstraint = new Dictionary<string, PartConstraintRule>
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
			return HandoutMasterPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D7F1 RID: 55281 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal HandoutMasterPart()
		{
		}

		// Token: 0x0600D7F2 RID: 55282 RVA: 0x002A9B54 File Offset: 0x002A7D54
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

		// Token: 0x0600D7F3 RID: 55283 RVA: 0x002A9D10 File Offset: 0x002A7F10
		public CustomXmlPart AddCustomXmlPart(string contentType)
		{
			CustomXmlPart customXmlPart = new CustomXmlPart();
			base.InitPart<CustomXmlPart>(customXmlPart, contentType);
			return customXmlPart;
		}

		// Token: 0x0600D7F4 RID: 55284 RVA: 0x002A9D2C File Offset: 0x002A7F2C
		public CustomXmlPart AddCustomXmlPart(CustomXmlPartType partType)
		{
			string contentType = CustomXmlPartTypeInfo.GetContentType(partType);
			string targetExtension = CustomXmlPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddCustomXmlPart(contentType);
		}

		// Token: 0x0600D7F5 RID: 55285 RVA: 0x002A9D60 File Offset: 0x002A7F60
		public CustomXmlPart AddCustomXmlPart(string contentType, string id)
		{
			CustomXmlPart customXmlPart = new CustomXmlPart();
			this.InitPart<CustomXmlPart>(customXmlPart, contentType, id);
			return customXmlPart;
		}

		// Token: 0x0600D7F6 RID: 55286 RVA: 0x002A9D80 File Offset: 0x002A7F80
		public CustomXmlPart AddCustomXmlPart(CustomXmlPartType partType, string id)
		{
			string contentType = CustomXmlPartTypeInfo.GetContentType(partType);
			string targetExtension = CustomXmlPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddCustomXmlPart(contentType, id);
		}

		// Token: 0x0600D7F7 RID: 55287 RVA: 0x002A9DB5 File Offset: 0x002A7FB5
		public AudioReferenceRelationship AddAudioReferenceRelationship(MediaDataPart mediaDataPart)
		{
			return base.AddDataPartReferenceRelationship<AudioReferenceRelationship>(mediaDataPart);
		}

		// Token: 0x0600D7F8 RID: 55288 RVA: 0x002A9DBE File Offset: 0x002A7FBE
		public AudioReferenceRelationship AddAudioReferenceRelationship(MediaDataPart mediaDataPart, string id)
		{
			return base.AddDataPartReferenceRelationship<AudioReferenceRelationship>(mediaDataPart, id);
		}

		// Token: 0x0600D7F9 RID: 55289 RVA: 0x002A9DC8 File Offset: 0x002A7FC8
		public EmbeddedObjectPart AddEmbeddedObjectPart(string contentType)
		{
			EmbeddedObjectPart embeddedObjectPart = new EmbeddedObjectPart();
			base.InitPart<EmbeddedObjectPart>(embeddedObjectPart, contentType);
			return embeddedObjectPart;
		}

		// Token: 0x0600D7FA RID: 55290 RVA: 0x002A9DE4 File Offset: 0x002A7FE4
		public EmbeddedPackagePart AddEmbeddedPackagePart(string contentType)
		{
			EmbeddedPackagePart embeddedPackagePart = new EmbeddedPackagePart();
			base.InitPart<EmbeddedPackagePart>(embeddedPackagePart, contentType);
			return embeddedPackagePart;
		}

		// Token: 0x0600D7FB RID: 55291 RVA: 0x002A9E00 File Offset: 0x002A8000
		public ImagePart AddImagePart(string contentType)
		{
			ImagePart imagePart = new ImagePart();
			base.InitPart<ImagePart>(imagePart, contentType);
			return imagePart;
		}

		// Token: 0x0600D7FC RID: 55292 RVA: 0x002A9E1C File Offset: 0x002A801C
		public ImagePart AddImagePart(ImagePartType partType)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType);
		}

		// Token: 0x0600D7FD RID: 55293 RVA: 0x002A9E50 File Offset: 0x002A8050
		public ImagePart AddImagePart(string contentType, string id)
		{
			ImagePart imagePart = new ImagePart();
			this.InitPart<ImagePart>(imagePart, contentType, id);
			return imagePart;
		}

		// Token: 0x0600D7FE RID: 55294 RVA: 0x002A9E70 File Offset: 0x002A8070
		public ImagePart AddImagePart(ImagePartType partType, string id)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType, id);
		}

		// Token: 0x0600D7FF RID: 55295 RVA: 0x002A3EF9 File Offset: 0x002A20F9
		public VideoReferenceRelationship AddVideoReferenceRelationship(MediaDataPart mediaDataPart)
		{
			return base.AddDataPartReferenceRelationship<VideoReferenceRelationship>(mediaDataPart);
		}

		// Token: 0x0600D800 RID: 55296 RVA: 0x002A3F02 File Offset: 0x002A2102
		public VideoReferenceRelationship AddVideoReferenceRelationship(MediaDataPart mediaDataPart, string id)
		{
			return base.AddDataPartReferenceRelationship<VideoReferenceRelationship>(mediaDataPart, id);
		}

		// Token: 0x0600D801 RID: 55297 RVA: 0x002A9EA8 File Offset: 0x002A80A8
		public EmbeddedControlPersistenceBinaryDataPart AddEmbeddedControlPersistenceBinaryDataPart(string contentType)
		{
			EmbeddedControlPersistenceBinaryDataPart embeddedControlPersistenceBinaryDataPart = new EmbeddedControlPersistenceBinaryDataPart();
			base.InitPart<EmbeddedControlPersistenceBinaryDataPart>(embeddedControlPersistenceBinaryDataPart, contentType);
			return embeddedControlPersistenceBinaryDataPart;
		}

		// Token: 0x0600D802 RID: 55298 RVA: 0x002A9EC4 File Offset: 0x002A80C4
		public EmbeddedControlPersistenceBinaryDataPart AddEmbeddedControlPersistenceBinaryDataPart(EmbeddedControlPersistenceBinaryDataPartType partType)
		{
			string contentType = EmbeddedControlPersistenceBinaryDataPartTypeInfo.GetContentType(partType);
			string targetExtension = EmbeddedControlPersistenceBinaryDataPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddEmbeddedControlPersistenceBinaryDataPart(contentType);
		}

		// Token: 0x0600D803 RID: 55299 RVA: 0x002A9EF8 File Offset: 0x002A80F8
		public EmbeddedControlPersistenceBinaryDataPart AddEmbeddedControlPersistenceBinaryDataPart(string contentType, string id)
		{
			EmbeddedControlPersistenceBinaryDataPart embeddedControlPersistenceBinaryDataPart = new EmbeddedControlPersistenceBinaryDataPart();
			this.InitPart<EmbeddedControlPersistenceBinaryDataPart>(embeddedControlPersistenceBinaryDataPart, contentType, id);
			return embeddedControlPersistenceBinaryDataPart;
		}

		// Token: 0x0600D804 RID: 55300 RVA: 0x002A9F18 File Offset: 0x002A8118
		public EmbeddedControlPersistenceBinaryDataPart AddEmbeddedControlPersistenceBinaryDataPart(EmbeddedControlPersistenceBinaryDataPartType partType, string id)
		{
			string contentType = EmbeddedControlPersistenceBinaryDataPartTypeInfo.GetContentType(partType);
			string targetExtension = EmbeddedControlPersistenceBinaryDataPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddEmbeddedControlPersistenceBinaryDataPart(contentType, id);
		}

		// Token: 0x17003565 RID: 13669
		// (get) Token: 0x0600D805 RID: 55301 RVA: 0x002A9F4D File Offset: 0x002A814D
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/handoutMaster";
			}
		}

		// Token: 0x17003566 RID: 13670
		// (get) Token: 0x0600D806 RID: 55302 RVA: 0x002A9F54 File Offset: 0x002A8154
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.presentationml.handoutMaster+xml";
			}
		}

		// Token: 0x17003567 RID: 13671
		// (get) Token: 0x0600D807 RID: 55303 RVA: 0x002A9F5B File Offset: 0x002A815B
		internal sealed override string TargetPath
		{
			get
			{
				return "handoutMasters";
			}
		}

		// Token: 0x17003568 RID: 13672
		// (get) Token: 0x0600D808 RID: 55304 RVA: 0x002A9F62 File Offset: 0x002A8162
		internal sealed override string TargetName
		{
			get
			{
				return "handoutMaster";
			}
		}

		// Token: 0x17003569 RID: 13673
		// (get) Token: 0x0600D809 RID: 55305 RVA: 0x002A3F21 File Offset: 0x002A2121
		public IEnumerable<CustomXmlPart> CustomXmlParts
		{
			get
			{
				return base.GetPartsOfType<CustomXmlPart>();
			}
		}

		// Token: 0x1700356A RID: 13674
		// (get) Token: 0x0600D80A RID: 55306 RVA: 0x002A3FB9 File Offset: 0x002A21B9
		public IEnumerable<ChartPart> ChartParts
		{
			get
			{
				return base.GetPartsOfType<ChartPart>();
			}
		}

		// Token: 0x1700356B RID: 13675
		// (get) Token: 0x0600D80B RID: 55307 RVA: 0x002A3FC1 File Offset: 0x002A21C1
		public IEnumerable<DiagramColorsPart> DiagramColorsParts
		{
			get
			{
				return base.GetPartsOfType<DiagramColorsPart>();
			}
		}

		// Token: 0x1700356C RID: 13676
		// (get) Token: 0x0600D80C RID: 55308 RVA: 0x002A3FC9 File Offset: 0x002A21C9
		public IEnumerable<DiagramDataPart> DiagramDataParts
		{
			get
			{
				return base.GetPartsOfType<DiagramDataPart>();
			}
		}

		// Token: 0x1700356D RID: 13677
		// (get) Token: 0x0600D80D RID: 55309 RVA: 0x002A3FD1 File Offset: 0x002A21D1
		public IEnumerable<DiagramPersistLayoutPart> DiagramPersistLayoutParts
		{
			get
			{
				return base.GetPartsOfType<DiagramPersistLayoutPart>();
			}
		}

		// Token: 0x1700356E RID: 13678
		// (get) Token: 0x0600D80E RID: 55310 RVA: 0x002A3FD9 File Offset: 0x002A21D9
		public IEnumerable<DiagramLayoutDefinitionPart> DiagramLayoutDefinitionParts
		{
			get
			{
				return base.GetPartsOfType<DiagramLayoutDefinitionPart>();
			}
		}

		// Token: 0x1700356F RID: 13679
		// (get) Token: 0x0600D80F RID: 55311 RVA: 0x002A3FE1 File Offset: 0x002A21E1
		public IEnumerable<DiagramStylePart> DiagramStyleParts
		{
			get
			{
				return base.GetPartsOfType<DiagramStylePart>();
			}
		}

		// Token: 0x17003570 RID: 13680
		// (get) Token: 0x0600D810 RID: 55312 RVA: 0x002A3FF1 File Offset: 0x002A21F1
		public IEnumerable<EmbeddedObjectPart> EmbeddedObjectParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedObjectPart>();
			}
		}

		// Token: 0x17003571 RID: 13681
		// (get) Token: 0x0600D811 RID: 55313 RVA: 0x002A3FF9 File Offset: 0x002A21F9
		public IEnumerable<EmbeddedPackagePart> EmbeddedPackageParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedPackagePart>();
			}
		}

		// Token: 0x17003572 RID: 13682
		// (get) Token: 0x0600D812 RID: 55314 RVA: 0x002A4001 File Offset: 0x002A2201
		public IEnumerable<ImagePart> ImageParts
		{
			get
			{
				return base.GetPartsOfType<ImagePart>();
			}
		}

		// Token: 0x17003573 RID: 13683
		// (get) Token: 0x0600D813 RID: 55315 RVA: 0x002A780D File Offset: 0x002A5A0D
		public IEnumerable<VmlDrawingPart> VmlDrawingParts
		{
			get
			{
				return base.GetPartsOfType<VmlDrawingPart>();
			}
		}

		// Token: 0x17003574 RID: 13684
		// (get) Token: 0x0600D814 RID: 55316 RVA: 0x002A9132 File Offset: 0x002A7332
		public IEnumerable<EmbeddedControlPersistenceBinaryDataPart> EmbeddedControlPersistenceBinaryDataParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedControlPersistenceBinaryDataPart>();
			}
		}

		// Token: 0x17003575 RID: 13685
		// (get) Token: 0x0600D815 RID: 55317 RVA: 0x002A3F31 File Offset: 0x002A2131
		public ThemePart ThemePart
		{
			get
			{
				return base.GetSubPartOfType<ThemePart>();
			}
		}

		// Token: 0x17003576 RID: 13686
		// (get) Token: 0x0600D816 RID: 55318 RVA: 0x002A9F69 File Offset: 0x002A8169
		public IEnumerable<UserDefinedTagsPart> UserDefinedTagsParts
		{
			get
			{
				return base.GetPartsOfType<UserDefinedTagsPart>();
			}
		}

		// Token: 0x17003577 RID: 13687
		// (get) Token: 0x0600D817 RID: 55319 RVA: 0x002A9F71 File Offset: 0x002A8171
		public SlidePart SlidePart
		{
			get
			{
				return base.GetSubPartOfType<SlidePart>();
			}
		}

		// Token: 0x17003578 RID: 13688
		// (get) Token: 0x0600D818 RID: 55320 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003579 RID: 13689
		// (get) Token: 0x0600D819 RID: 55321 RVA: 0x002A9F79 File Offset: 0x002A8179
		// (set) Token: 0x0600D81A RID: 55322 RVA: 0x002A9F81 File Offset: 0x002A8181
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as HandoutMaster;
			}
		}

		// Token: 0x1700357A RID: 13690
		// (get) Token: 0x0600D81B RID: 55323 RVA: 0x002A9F8F File Offset: 0x002A818F
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.HandoutMaster;
			}
		}

		// Token: 0x1700357B RID: 13691
		// (get) Token: 0x0600D81C RID: 55324 RVA: 0x002A9F97 File Offset: 0x002A8197
		// (set) Token: 0x0600D81D RID: 55325 RVA: 0x002A3296 File Offset: 0x002A1496
		public HandoutMaster HandoutMaster
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<HandoutMaster>();
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

		// Token: 0x04006B13 RID: 27411
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/handoutMaster";

		// Token: 0x04006B14 RID: 27412
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.presentationml.handoutMaster+xml";

		// Token: 0x04006B15 RID: 27413
		internal const string TargetPathConstant = "handoutMasters";

		// Token: 0x04006B16 RID: 27414
		internal const string TargetNameConstant = "handoutMaster";

		// Token: 0x04006B17 RID: 27415
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006B18 RID: 27416
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006B19 RID: 27417
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private HandoutMaster _rootEle;
	}
}
