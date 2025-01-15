using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Presentation;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002183 RID: 8579
	internal class PresentationPart : OpenXmlPart
	{
		// Token: 0x0600D7B1 RID: 55217 RVA: 0x002A9264 File Offset: 0x002A7464
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (PresentationPart._partConstraint == null)
			{
				PresentationPart._partConstraint = new Dictionary<string, PartConstraintRule>
				{
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/customXml",
						new PartConstraintRule("CustomXmlPart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/font",
						new PartConstraintRule("FontPart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/presProps",
						new PartConstraintRule("PresentationPropertiesPart", "application/vnd.openxmlformats-officedocument.presentationml.presProps+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/tableStyles",
						new PartConstraintRule("TableStylesPart", "application/vnd.openxmlformats-officedocument.presentationml.tableStyles+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/theme",
						new PartConstraintRule("ThemePart", "application/vnd.openxmlformats-officedocument.theme+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/viewProps",
						new PartConstraintRule("ViewPropertiesPart", "application/vnd.openxmlformats-officedocument.presentationml.viewProps+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/notesMaster",
						new PartConstraintRule("NotesMasterPart", "application/vnd.openxmlformats-officedocument.presentationml.notesMaster+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/slide",
						new PartConstraintRule("SlidePart", "application/vnd.openxmlformats-officedocument.presentationml.slide+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/slideMaster",
						new PartConstraintRule("SlideMasterPart", "application/vnd.openxmlformats-officedocument.presentationml.slideMaster+xml", true, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/tags",
						new PartConstraintRule("UserDefinedTagsPart", "application/vnd.openxmlformats-officedocument.presentationml.tags+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/commentAuthors",
						new PartConstraintRule("CommentAuthorsPart", "application/vnd.openxmlformats-officedocument.presentationml.commentAuthors+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/handoutMaster",
						new PartConstraintRule("HandoutMasterPart", "application/vnd.openxmlformats-officedocument.presentationml.handoutMaster+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.microsoft.com/office/2006/relationships/legacyDocTextInfo",
						new PartConstraintRule("LegacyDiagramTextInfoPart", "application/vnd.ms-office.legacyDocTextInfo", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.microsoft.com/office/2006/relationships/smartTags",
						new PartConstraintRule("SmartTagsPart", "application/vnd.ms-powerpoint.smartTags", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.microsoft.com/office/2006/relationships/vbaProject",
						new PartConstraintRule("VbaProjectPart", "application/vnd.ms-office.vbaProject", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					}
				};
			}
			return PresentationPart._partConstraint;
		}

		// Token: 0x0600D7B2 RID: 55218 RVA: 0x002A9438 File Offset: 0x002A7638
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (PresentationPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				PresentationPart._dataPartReferenceConstraint = dictionary;
			}
			return PresentationPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D7B3 RID: 55219 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal PresentationPart()
		{
		}

		// Token: 0x0600D7B4 RID: 55220 RVA: 0x002A9460 File Offset: 0x002A7660
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
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/font":
				return new FontPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/presProps":
				return new PresentationPropertiesPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/tableStyles":
				return new TableStylesPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/theme":
				return new ThemePart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/viewProps":
				return new ViewPropertiesPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/notesMaster":
				return new NotesMasterPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/slide":
				return new SlidePart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/slideMaster":
				return new SlideMasterPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/tags":
				return new UserDefinedTagsPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/commentAuthors":
				return new CommentAuthorsPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/handoutMaster":
				return new HandoutMasterPart();
			case "http://schemas.microsoft.com/office/2006/relationships/legacyDocTextInfo":
				return new LegacyDiagramTextInfoPart();
			case "http://schemas.microsoft.com/office/2006/relationships/smartTags":
				return new SmartTagsPart();
			case "http://schemas.microsoft.com/office/2006/relationships/vbaProject":
				return new VbaProjectPart();
			}
			throw new ArgumentOutOfRangeException("relationshipType");
		}

		// Token: 0x0600D7B5 RID: 55221 RVA: 0x002A961C File Offset: 0x002A781C
		public CustomXmlPart AddCustomXmlPart(string contentType)
		{
			CustomXmlPart customXmlPart = new CustomXmlPart();
			base.InitPart<CustomXmlPart>(customXmlPart, contentType);
			return customXmlPart;
		}

		// Token: 0x0600D7B6 RID: 55222 RVA: 0x002A9638 File Offset: 0x002A7838
		public CustomXmlPart AddCustomXmlPart(CustomXmlPartType partType)
		{
			string contentType = CustomXmlPartTypeInfo.GetContentType(partType);
			string targetExtension = CustomXmlPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddCustomXmlPart(contentType);
		}

		// Token: 0x0600D7B7 RID: 55223 RVA: 0x002A966C File Offset: 0x002A786C
		public CustomXmlPart AddCustomXmlPart(string contentType, string id)
		{
			CustomXmlPart customXmlPart = new CustomXmlPart();
			this.InitPart<CustomXmlPart>(customXmlPart, contentType, id);
			return customXmlPart;
		}

		// Token: 0x0600D7B8 RID: 55224 RVA: 0x002A968C File Offset: 0x002A788C
		public CustomXmlPart AddCustomXmlPart(CustomXmlPartType partType, string id)
		{
			string contentType = CustomXmlPartTypeInfo.GetContentType(partType);
			string targetExtension = CustomXmlPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddCustomXmlPart(contentType, id);
		}

		// Token: 0x0600D7B9 RID: 55225 RVA: 0x002A96C4 File Offset: 0x002A78C4
		public FontPart AddFontPart(string contentType)
		{
			FontPart fontPart = new FontPart();
			base.InitPart<FontPart>(fontPart, contentType);
			return fontPart;
		}

		// Token: 0x0600D7BA RID: 55226 RVA: 0x002A96E0 File Offset: 0x002A78E0
		public FontPart AddFontPart(FontPartType partType)
		{
			string contentType = FontPartTypeInfo.GetContentType(partType);
			string targetExtension = FontPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddFontPart(contentType);
		}

		// Token: 0x0600D7BB RID: 55227 RVA: 0x002A9714 File Offset: 0x002A7914
		public FontPart AddFontPart(string contentType, string id)
		{
			FontPart fontPart = new FontPart();
			this.InitPart<FontPart>(fontPart, contentType, id);
			return fontPart;
		}

		// Token: 0x0600D7BC RID: 55228 RVA: 0x002A9734 File Offset: 0x002A7934
		public FontPart AddFontPart(FontPartType partType, string id)
		{
			string contentType = FontPartTypeInfo.GetContentType(partType);
			string targetExtension = FontPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddFontPart(contentType, id);
		}

		// Token: 0x1700353F RID: 13631
		// (get) Token: 0x0600D7BD RID: 55229 RVA: 0x002A3F0C File Offset: 0x002A210C
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument";
			}
		}

		// Token: 0x17003540 RID: 13632
		// (get) Token: 0x0600D7BE RID: 55230 RVA: 0x002A9769 File Offset: 0x002A7969
		internal sealed override string TargetPath
		{
			get
			{
				return "ppt";
			}
		}

		// Token: 0x17003541 RID: 13633
		// (get) Token: 0x0600D7BF RID: 55231 RVA: 0x002A9770 File Offset: 0x002A7970
		internal sealed override string TargetName
		{
			get
			{
				return "presentation";
			}
		}

		// Token: 0x17003542 RID: 13634
		// (get) Token: 0x0600D7C0 RID: 55232 RVA: 0x002A3F21 File Offset: 0x002A2121
		public IEnumerable<CustomXmlPart> CustomXmlParts
		{
			get
			{
				return base.GetPartsOfType<CustomXmlPart>();
			}
		}

		// Token: 0x17003543 RID: 13635
		// (get) Token: 0x0600D7C1 RID: 55233 RVA: 0x002A50B6 File Offset: 0x002A32B6
		public IEnumerable<FontPart> FontParts
		{
			get
			{
				return base.GetPartsOfType<FontPart>();
			}
		}

		// Token: 0x17003544 RID: 13636
		// (get) Token: 0x0600D7C2 RID: 55234 RVA: 0x002A9777 File Offset: 0x002A7977
		public PresentationPropertiesPart PresentationPropertiesPart
		{
			get
			{
				return base.GetSubPartOfType<PresentationPropertiesPart>();
			}
		}

		// Token: 0x17003545 RID: 13637
		// (get) Token: 0x0600D7C3 RID: 55235 RVA: 0x002A977F File Offset: 0x002A797F
		public TableStylesPart TableStylesPart
		{
			get
			{
				return base.GetSubPartOfType<TableStylesPart>();
			}
		}

		// Token: 0x17003546 RID: 13638
		// (get) Token: 0x0600D7C4 RID: 55236 RVA: 0x002A3F31 File Offset: 0x002A2131
		public ThemePart ThemePart
		{
			get
			{
				return base.GetSubPartOfType<ThemePart>();
			}
		}

		// Token: 0x17003547 RID: 13639
		// (get) Token: 0x0600D7C5 RID: 55237 RVA: 0x002A9787 File Offset: 0x002A7987
		public ViewPropertiesPart ViewPropertiesPart
		{
			get
			{
				return base.GetSubPartOfType<ViewPropertiesPart>();
			}
		}

		// Token: 0x17003548 RID: 13640
		// (get) Token: 0x0600D7C6 RID: 55238 RVA: 0x002A978F File Offset: 0x002A798F
		public NotesMasterPart NotesMasterPart
		{
			get
			{
				return base.GetSubPartOfType<NotesMasterPart>();
			}
		}

		// Token: 0x17003549 RID: 13641
		// (get) Token: 0x0600D7C7 RID: 55239 RVA: 0x002A9797 File Offset: 0x002A7997
		public IEnumerable<SlidePart> SlideParts
		{
			get
			{
				return base.GetPartsOfType<SlidePart>();
			}
		}

		// Token: 0x1700354A RID: 13642
		// (get) Token: 0x0600D7C8 RID: 55240 RVA: 0x002A979F File Offset: 0x002A799F
		public IEnumerable<SlideMasterPart> SlideMasterParts
		{
			get
			{
				return base.GetPartsOfType<SlideMasterPart>();
			}
		}

		// Token: 0x1700354B RID: 13643
		// (get) Token: 0x0600D7C9 RID: 55241 RVA: 0x002A97A7 File Offset: 0x002A79A7
		public UserDefinedTagsPart UserDefinedTagsPart
		{
			get
			{
				return base.GetSubPartOfType<UserDefinedTagsPart>();
			}
		}

		// Token: 0x1700354C RID: 13644
		// (get) Token: 0x0600D7CA RID: 55242 RVA: 0x002A97AF File Offset: 0x002A79AF
		public CommentAuthorsPart CommentAuthorsPart
		{
			get
			{
				return base.GetSubPartOfType<CommentAuthorsPart>();
			}
		}

		// Token: 0x1700354D RID: 13645
		// (get) Token: 0x0600D7CB RID: 55243 RVA: 0x002A97B7 File Offset: 0x002A79B7
		public HandoutMasterPart HandoutMasterPart
		{
			get
			{
				return base.GetSubPartOfType<HandoutMasterPart>();
			}
		}

		// Token: 0x1700354E RID: 13646
		// (get) Token: 0x0600D7CC RID: 55244 RVA: 0x002A97BF File Offset: 0x002A79BF
		public LegacyDiagramTextInfoPart LegacyDiagramTextInfoPart
		{
			get
			{
				return base.GetSubPartOfType<LegacyDiagramTextInfoPart>();
			}
		}

		// Token: 0x1700354F RID: 13647
		// (get) Token: 0x0600D7CD RID: 55245 RVA: 0x002A97C7 File Offset: 0x002A79C7
		public SmartTagsPart SmartTagsPart
		{
			get
			{
				return base.GetSubPartOfType<SmartTagsPart>();
			}
		}

		// Token: 0x17003550 RID: 13648
		// (get) Token: 0x0600D7CE RID: 55246 RVA: 0x002A3FA9 File Offset: 0x002A21A9
		public VbaProjectPart VbaProjectPart
		{
			get
			{
				return base.GetSubPartOfType<VbaProjectPart>();
			}
		}

		// Token: 0x17003551 RID: 13649
		// (get) Token: 0x0600D7CF RID: 55247 RVA: 0x00002105 File Offset: 0x00000305
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17003552 RID: 13650
		// (get) Token: 0x0600D7D0 RID: 55248 RVA: 0x002A97CF File Offset: 0x002A79CF
		// (set) Token: 0x0600D7D1 RID: 55249 RVA: 0x002A97D7 File Offset: 0x002A79D7
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as Presentation;
			}
		}

		// Token: 0x17003553 RID: 13651
		// (get) Token: 0x0600D7D2 RID: 55250 RVA: 0x002A97E5 File Offset: 0x002A79E5
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.Presentation;
			}
		}

		// Token: 0x17003554 RID: 13652
		// (get) Token: 0x0600D7D3 RID: 55251 RVA: 0x002A97ED File Offset: 0x002A79ED
		// (set) Token: 0x0600D7D4 RID: 55252 RVA: 0x002A3296 File Offset: 0x002A1496
		public Presentation Presentation
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<Presentation>();
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

		// Token: 0x04006AFF RID: 27391
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument";

		// Token: 0x04006B00 RID: 27392
		internal const string TargetPathConstant = "ppt";

		// Token: 0x04006B01 RID: 27393
		internal const string TargetNameConstant = "presentation";

		// Token: 0x04006B02 RID: 27394
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006B03 RID: 27395
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006B04 RID: 27396
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Presentation _rootEle;
	}
}
