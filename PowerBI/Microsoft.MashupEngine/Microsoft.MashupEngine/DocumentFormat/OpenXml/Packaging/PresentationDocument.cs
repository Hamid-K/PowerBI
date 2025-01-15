using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020030D2 RID: 12498
	internal class PresentationDocument : OpenXmlPackage
	{
		// Token: 0x0601B246 RID: 111174 RVA: 0x0036DCE0 File Offset: 0x0036BEE0
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (PresentationDocument._partConstraint == null)
			{
				PresentationDocument._partConstraint = new Dictionary<string, PartConstraintRule>
				{
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument",
						new PartConstraintRule("PresentationPart", null, true, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/package/2006/relationships/metadata/core-properties",
						new PartConstraintRule("CoreFilePropertiesPart", "application/vnd.openxmlformats-package.core-properties+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/extended-properties",
						new PartConstraintRule("ExtendedFilePropertiesPart", "application/vnd.openxmlformats-officedocument.extended-properties+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/custom-properties",
						new PartConstraintRule("CustomFilePropertiesPart", "application/vnd.openxmlformats-officedocument.custom-properties+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/package/2006/relationships/metadata/thumbnail",
						new PartConstraintRule("ThumbnailPart", null, false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/package/2006/relationships/digital-signature/origin",
						new PartConstraintRule("DigitalSignatureOriginPart", "application/vnd.openxmlformats-package.digital-signature-origin", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.microsoft.com/office/2006/relationships/ui/userCustomization",
						new PartConstraintRule("QuickAccessToolbarCustomizationsPart", "application/xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.microsoft.com/office/2006/relationships/ui/extensibility",
						new PartConstraintRule("RibbonExtensibilityPart", "application/xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.microsoft.com/office/2007/relationships/ui/extensibility",
						new PartConstraintRule("RibbonAndBackstageCustomizationsPart", "application/xml", false, false, FileFormatVersions.Office2010)
					}
				};
			}
			return PresentationDocument._partConstraint;
		}

		// Token: 0x0601B247 RID: 111175 RVA: 0x0036DE08 File Offset: 0x0036C008
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (PresentationDocument._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				PresentationDocument._dataPartReferenceConstraint = dictionary;
			}
			return PresentationDocument._dataPartReferenceConstraint;
		}

		// Token: 0x1700985A RID: 39002
		// (get) Token: 0x0601B248 RID: 111176 RVA: 0x002A3F0C File Offset: 0x002A210C
		internal sealed override string MainPartRelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument";
			}
		}

		// Token: 0x1700985B RID: 39003
		// (get) Token: 0x0601B249 RID: 111177 RVA: 0x0036DE30 File Offset: 0x0036C030
		private static Dictionary<PresentationDocumentType, string> MainPartContentTypes
		{
			get
			{
				if (PresentationDocument._validMainPartContentType == null)
				{
					PresentationDocument._validMainPartContentType = new Dictionary<PresentationDocumentType, string>
					{
						{
							PresentationDocumentType.Presentation,
							"application/vnd.openxmlformats-officedocument.presentationml.presentation.main+xml"
						},
						{
							PresentationDocumentType.Template,
							"application/vnd.openxmlformats-officedocument.presentationml.template.main+xml"
						},
						{
							PresentationDocumentType.Slideshow,
							"application/vnd.openxmlformats-officedocument.presentationml.slideshow.main+xml"
						},
						{
							PresentationDocumentType.MacroEnabledPresentation,
							"application/vnd.ms-powerpoint.presentation.macroEnabled.main+xml"
						},
						{
							PresentationDocumentType.MacroEnabledTemplate,
							"application/vnd.ms-powerpoint.template.macroEnabled.main+xml"
						},
						{
							PresentationDocumentType.MacroEnabledSlideshow,
							"application/vnd.ms-powerpoint.slideshow.macroEnabled.main+xml"
						},
						{
							PresentationDocumentType.AddIn,
							"application/vnd.ms-powerpoint.addin.macroEnabled.main+xml"
						}
					};
				}
				return PresentationDocument._validMainPartContentType;
			}
		}

		// Token: 0x1700985C RID: 39004
		// (get) Token: 0x0601B24A RID: 111178 RVA: 0x0036DEA9 File Offset: 0x0036C0A9
		internal sealed override ICollection<string> ValidMainPartContentTypes
		{
			get
			{
				return PresentationDocument.MainPartContentTypes.Values;
			}
		}

		// Token: 0x0601B24B RID: 111179 RVA: 0x0036CB3D File Offset: 0x0036AD3D
		protected PresentationDocument()
		{
		}

		// Token: 0x1700985D RID: 39005
		// (get) Token: 0x0601B24C RID: 111180 RVA: 0x0036DEB5 File Offset: 0x0036C0B5
		// (set) Token: 0x0601B24D RID: 111181 RVA: 0x0036DEC3 File Offset: 0x0036C0C3
		public PresentationDocumentType DocumentType
		{
			get
			{
				this.ThrowIfObjectDisposed();
				return this._documentType;
			}
			private set
			{
				this.ThrowIfObjectDisposed();
				this._documentType = value;
			}
		}

		// Token: 0x0601B24E RID: 111182 RVA: 0x0036DED4 File Offset: 0x0036C0D4
		private void UpdateDocumentTypeFromContentType()
		{
			if (base.MainPartContentType == null)
			{
				throw new InvalidOperationException();
			}
			foreach (KeyValuePair<PresentationDocumentType, string> keyValuePair in PresentationDocument.MainPartContentTypes)
			{
				if (keyValuePair.Value == base.MainPartContentType)
				{
					this.DocumentType = keyValuePair.Key;
				}
			}
		}

		// Token: 0x0601B24F RID: 111183 RVA: 0x0036DF50 File Offset: 0x0036C150
		public static PresentationDocument Create(string path, PresentationDocumentType type)
		{
			return PresentationDocument.Create(path, type, true);
		}

		// Token: 0x0601B250 RID: 111184 RVA: 0x0036DF5A File Offset: 0x0036C15A
		public static PresentationDocument Create(Stream stream, PresentationDocumentType type)
		{
			return PresentationDocument.Create(stream, type, true);
		}

		// Token: 0x0601B251 RID: 111185 RVA: 0x0036DF64 File Offset: 0x0036C164
		public static PresentationDocument Create(Package package, PresentationDocumentType type)
		{
			return PresentationDocument.Create(package, type, true);
		}

		// Token: 0x0601B252 RID: 111186 RVA: 0x0036DF70 File Offset: 0x0036C170
		public static PresentationDocument Create(string path, PresentationDocumentType type, bool autoSave)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException(path);
			}
			PresentationDocument presentationDocument = new PresentationDocument();
			presentationDocument.DocumentType = type;
			presentationDocument.OpenSettings = new OpenSettings();
			presentationDocument.OpenSettings.AutoSave = autoSave;
			presentationDocument.MainPartContentType = PresentationDocument.MainPartContentTypes[type];
			presentationDocument.CreateCore(path);
			return presentationDocument;
		}

		// Token: 0x0601B253 RID: 111187 RVA: 0x0036DFCC File Offset: 0x0036C1CC
		public static PresentationDocument Create(Stream stream, PresentationDocumentType type, bool autoSave)
		{
			PresentationDocument presentationDocument = new PresentationDocument();
			presentationDocument.DocumentType = type;
			presentationDocument.OpenSettings = new OpenSettings();
			presentationDocument.OpenSettings.AutoSave = autoSave;
			presentationDocument.MainPartContentType = PresentationDocument.MainPartContentTypes[type];
			presentationDocument.CreateCore(stream);
			return presentationDocument;
		}

		// Token: 0x0601B254 RID: 111188 RVA: 0x0036E018 File Offset: 0x0036C218
		public static PresentationDocument Create(Package package, PresentationDocumentType type, bool autoSave)
		{
			PresentationDocument presentationDocument = new PresentationDocument();
			presentationDocument.DocumentType = type;
			presentationDocument.OpenSettings = new OpenSettings();
			presentationDocument.OpenSettings.AutoSave = autoSave;
			presentationDocument.MainPartContentType = PresentationDocument.MainPartContentTypes[type];
			presentationDocument.CreateCore(package);
			return presentationDocument;
		}

		// Token: 0x0601B255 RID: 111189 RVA: 0x0036E062 File Offset: 0x0036C262
		public static PresentationDocument Open(string path, bool isEditable)
		{
			return PresentationDocument.Open(path, isEditable, new OpenSettings());
		}

		// Token: 0x0601B256 RID: 111190 RVA: 0x0036E070 File Offset: 0x0036C270
		public static PresentationDocument Open(Stream stream, bool isEditable)
		{
			return PresentationDocument.Open(stream, isEditable, new OpenSettings());
		}

		// Token: 0x0601B257 RID: 111191 RVA: 0x0036E07E File Offset: 0x0036C27E
		public static PresentationDocument Open(Package package)
		{
			return PresentationDocument.Open(package, new OpenSettings());
		}

		// Token: 0x0601B258 RID: 111192 RVA: 0x0036E08C File Offset: 0x0036C28C
		public static PresentationDocument Open(string path, bool isEditable, OpenSettings openSettings)
		{
			if (openSettings.MarkupCompatibilityProcessSettings.ProcessMode != MarkupCompatibilityProcessMode.NoProcess && openSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions != FileFormatVersions.Office2007 && openSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions != FileFormatVersions.Office2010)
			{
				throw new ArgumentException(ExceptionMessages.InvalidMCMode);
			}
			PresentationDocument presentationDocument = new PresentationDocument();
			presentationDocument.OpenSettings = new OpenSettings();
			presentationDocument.OpenSettings.AutoSave = openSettings.AutoSave;
			presentationDocument.OpenSettings.MarkupCompatibilityProcessSettings.ProcessMode = openSettings.MarkupCompatibilityProcessSettings.ProcessMode;
			presentationDocument.OpenSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions = openSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions;
			presentationDocument.MaxCharactersInPart = openSettings.MaxCharactersInPart;
			presentationDocument.OpenCore(path, isEditable);
			if (PresentationDocument.MainPartContentTypes[presentationDocument.DocumentType] != presentationDocument.MainPartContentType)
			{
				presentationDocument.UpdateDocumentTypeFromContentType();
			}
			return presentationDocument;
		}

		// Token: 0x0601B259 RID: 111193 RVA: 0x0036E160 File Offset: 0x0036C360
		public static PresentationDocument Open(Stream stream, bool isEditable, OpenSettings openSettings)
		{
			if (openSettings.MarkupCompatibilityProcessSettings.ProcessMode != MarkupCompatibilityProcessMode.NoProcess && openSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions != FileFormatVersions.Office2007 && openSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions != FileFormatVersions.Office2010)
			{
				throw new ArgumentException(ExceptionMessages.InvalidMCMode);
			}
			PresentationDocument presentationDocument = new PresentationDocument();
			presentationDocument.OpenSettings = new OpenSettings();
			presentationDocument.OpenSettings.AutoSave = openSettings.AutoSave;
			presentationDocument.OpenSettings.MarkupCompatibilityProcessSettings.ProcessMode = openSettings.MarkupCompatibilityProcessSettings.ProcessMode;
			presentationDocument.OpenSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions = openSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions;
			presentationDocument.MaxCharactersInPart = openSettings.MaxCharactersInPart;
			presentationDocument.OpenCore(stream, isEditable);
			if (PresentationDocument.MainPartContentTypes[presentationDocument.DocumentType] != presentationDocument.MainPartContentType)
			{
				presentationDocument.UpdateDocumentTypeFromContentType();
			}
			return presentationDocument;
		}

		// Token: 0x0601B25A RID: 111194 RVA: 0x0036E234 File Offset: 0x0036C434
		public static PresentationDocument Open(Package package, OpenSettings openSettings)
		{
			if (openSettings.MarkupCompatibilityProcessSettings.ProcessMode != MarkupCompatibilityProcessMode.NoProcess && openSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions != FileFormatVersions.Office2007 && openSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions != FileFormatVersions.Office2010)
			{
				throw new ArgumentException(ExceptionMessages.InvalidMCMode);
			}
			PresentationDocument presentationDocument = new PresentationDocument();
			presentationDocument.OpenSettings = new OpenSettings();
			presentationDocument.OpenSettings.AutoSave = openSettings.AutoSave;
			presentationDocument.OpenSettings.MarkupCompatibilityProcessSettings.ProcessMode = openSettings.MarkupCompatibilityProcessSettings.ProcessMode;
			presentationDocument.OpenSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions = openSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions;
			presentationDocument.MaxCharactersInPart = openSettings.MaxCharactersInPart;
			presentationDocument.OpenCore(package);
			if (PresentationDocument.MainPartContentTypes[presentationDocument.DocumentType] != presentationDocument.MainPartContentType)
			{
				presentationDocument.UpdateDocumentTypeFromContentType();
			}
			return presentationDocument;
		}

		// Token: 0x0601B25B RID: 111195 RVA: 0x0036E304 File Offset: 0x0036C504
		public void ChangeDocumentType(PresentationDocumentType newType)
		{
			this.ThrowIfObjectDisposed();
			if (newType == this.DocumentType)
			{
				return;
			}
			if (base.FileOpenAccess == FileAccess.Read)
			{
				throw new IOException(ExceptionMessages.PackageAccessModeIsReadonly);
			}
			PresentationDocumentType documentType = this.DocumentType;
			this.DocumentType = newType;
			base.MainPartContentType = PresentationDocument.MainPartContentTypes[newType];
			if (this.PresentationPart == null)
			{
				return;
			}
			try
			{
				base.ChangeDocumentTypeInternal<PresentationPart>();
			}
			catch (OpenXmlPackageException ex)
			{
				if (ex.Message == ExceptionMessages.CannotChangeDocumentType)
				{
					this.DocumentType = documentType;
					base.MainPartContentType = PresentationDocument.MainPartContentTypes[documentType];
				}
				throw;
			}
		}

		// Token: 0x0601B25C RID: 111196 RVA: 0x0036E3A4 File Offset: 0x0036C5A4
		internal sealed override OpenXmlPart CreatePartCore(string relationshipType)
		{
			this.ThrowIfObjectDisposed();
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			switch (relationshipType)
			{
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument":
				return new PresentationPart();
			case "http://schemas.openxmlformats.org/package/2006/relationships/metadata/core-properties":
				return new CoreFilePropertiesPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/extended-properties":
				return new ExtendedFilePropertiesPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/custom-properties":
				return new CustomFilePropertiesPart();
			case "http://schemas.openxmlformats.org/package/2006/relationships/metadata/thumbnail":
				return new ThumbnailPart();
			case "http://schemas.openxmlformats.org/package/2006/relationships/digital-signature/origin":
				return new DigitalSignatureOriginPart();
			case "http://schemas.microsoft.com/office/2006/relationships/ui/userCustomization":
				return new QuickAccessToolbarCustomizationsPart();
			case "http://schemas.microsoft.com/office/2006/relationships/ui/extensibility":
				return new RibbonExtensibilityPart();
			case "http://schemas.microsoft.com/office/2007/relationships/ui/extensibility":
				return new RibbonAndBackstageCustomizationsPart();
			}
			throw new ArgumentOutOfRangeException("relationshipType");
		}

		// Token: 0x0601B25D RID: 111197 RVA: 0x0036E4D0 File Offset: 0x0036C6D0
		public PresentationPart AddPresentationPart()
		{
			PresentationPart presentationPart = new PresentationPart();
			base.InitPart<PresentationPart>(presentationPart, base.MainPartContentType);
			return presentationPart;
		}

		// Token: 0x0601B25E RID: 111198 RVA: 0x0036E4F4 File Offset: 0x0036C6F4
		public CoreFilePropertiesPart AddCoreFilePropertiesPart()
		{
			CoreFilePropertiesPart coreFilePropertiesPart = new CoreFilePropertiesPart();
			base.InitPart<CoreFilePropertiesPart>(coreFilePropertiesPart, "application/vnd.openxmlformats-package.core-properties+xml");
			return coreFilePropertiesPart;
		}

		// Token: 0x0601B25F RID: 111199 RVA: 0x0036E514 File Offset: 0x0036C714
		public ExtendedFilePropertiesPart AddExtendedFilePropertiesPart()
		{
			ExtendedFilePropertiesPart extendedFilePropertiesPart = new ExtendedFilePropertiesPart();
			base.InitPart<ExtendedFilePropertiesPart>(extendedFilePropertiesPart, "application/vnd.openxmlformats-officedocument.extended-properties+xml");
			return extendedFilePropertiesPart;
		}

		// Token: 0x0601B260 RID: 111200 RVA: 0x0036E534 File Offset: 0x0036C734
		public CustomFilePropertiesPart AddCustomFilePropertiesPart()
		{
			CustomFilePropertiesPart customFilePropertiesPart = new CustomFilePropertiesPart();
			base.InitPart<CustomFilePropertiesPart>(customFilePropertiesPart, "application/vnd.openxmlformats-officedocument.custom-properties+xml");
			return customFilePropertiesPart;
		}

		// Token: 0x0601B261 RID: 111201 RVA: 0x0036E554 File Offset: 0x0036C754
		public DigitalSignatureOriginPart AddDigitalSignatureOriginPart()
		{
			DigitalSignatureOriginPart digitalSignatureOriginPart = new DigitalSignatureOriginPart();
			base.InitPart<DigitalSignatureOriginPart>(digitalSignatureOriginPart, "application/vnd.openxmlformats-package.digital-signature-origin");
			return digitalSignatureOriginPart;
		}

		// Token: 0x0601B262 RID: 111202 RVA: 0x0036E574 File Offset: 0x0036C774
		public ThumbnailPart AddThumbnailPart(string contentType)
		{
			ThumbnailPart thumbnailPart = new ThumbnailPart();
			base.InitPart<ThumbnailPart>(thumbnailPart, contentType);
			return thumbnailPart;
		}

		// Token: 0x0601B263 RID: 111203 RVA: 0x0036E590 File Offset: 0x0036C790
		public ThumbnailPart AddThumbnailPart(ThumbnailPartType partType)
		{
			string contentType = ThumbnailPartTypeInfo.GetContentType(partType);
			string targetExtension = ThumbnailPartTypeInfo.GetTargetExtension(partType);
			base.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddThumbnailPart(contentType);
		}

		// Token: 0x0601B264 RID: 111204 RVA: 0x0036E5C0 File Offset: 0x0036C7C0
		public override T AddNewPart<T>(string contentType, string id)
		{
			if (contentType == null)
			{
				throw new ArgumentNullException("contentType");
			}
			if (typeof(PresentationPart).IsAssignableFrom(typeof(T)) && contentType != PresentationDocument.MainPartContentTypes[this._documentType])
			{
				throw new OpenXmlPackageException(ExceptionMessages.ErrorContentType);
			}
			return base.AddNewPart<T>(contentType, id);
		}

		// Token: 0x0601B265 RID: 111205 RVA: 0x0036E624 File Offset: 0x0036C824
		public QuickAccessToolbarCustomizationsPart AddQuickAccessToolbarCustomizationsPart()
		{
			QuickAccessToolbarCustomizationsPart quickAccessToolbarCustomizationsPart = new QuickAccessToolbarCustomizationsPart();
			base.InitPart<QuickAccessToolbarCustomizationsPart>(quickAccessToolbarCustomizationsPart, "application/xml");
			return quickAccessToolbarCustomizationsPart;
		}

		// Token: 0x0601B266 RID: 111206 RVA: 0x0036E644 File Offset: 0x0036C844
		public RibbonExtensibilityPart AddRibbonExtensibilityPart()
		{
			RibbonExtensibilityPart ribbonExtensibilityPart = new RibbonExtensibilityPart();
			base.InitPart<RibbonExtensibilityPart>(ribbonExtensibilityPart, "application/xml");
			return ribbonExtensibilityPart;
		}

		// Token: 0x0601B267 RID: 111207 RVA: 0x0036E664 File Offset: 0x0036C864
		public RibbonAndBackstageCustomizationsPart AddRibbonAndBackstageCustomizationsPart()
		{
			RibbonAndBackstageCustomizationsPart ribbonAndBackstageCustomizationsPart = new RibbonAndBackstageCustomizationsPart();
			base.InitPart<RibbonAndBackstageCustomizationsPart>(ribbonAndBackstageCustomizationsPart, "application/xml");
			return ribbonAndBackstageCustomizationsPart;
		}

		// Token: 0x1700985E RID: 39006
		// (get) Token: 0x0601B268 RID: 111208 RVA: 0x0036E684 File Offset: 0x0036C884
		public PresentationPart PresentationPart
		{
			get
			{
				return base.GetSubPartOfType<PresentationPart>();
			}
		}

		// Token: 0x1700985F RID: 39007
		// (get) Token: 0x0601B269 RID: 111209 RVA: 0x0036D30C File Offset: 0x0036B50C
		public CoreFilePropertiesPart CoreFilePropertiesPart
		{
			get
			{
				return base.GetSubPartOfType<CoreFilePropertiesPart>();
			}
		}

		// Token: 0x17009860 RID: 39008
		// (get) Token: 0x0601B26A RID: 111210 RVA: 0x0036D314 File Offset: 0x0036B514
		public ExtendedFilePropertiesPart ExtendedFilePropertiesPart
		{
			get
			{
				return base.GetSubPartOfType<ExtendedFilePropertiesPart>();
			}
		}

		// Token: 0x17009861 RID: 39009
		// (get) Token: 0x0601B26B RID: 111211 RVA: 0x0036D31C File Offset: 0x0036B51C
		public CustomFilePropertiesPart CustomFilePropertiesPart
		{
			get
			{
				return base.GetSubPartOfType<CustomFilePropertiesPart>();
			}
		}

		// Token: 0x17009862 RID: 39010
		// (get) Token: 0x0601B26C RID: 111212 RVA: 0x002A3F39 File Offset: 0x002A2139
		public ThumbnailPart ThumbnailPart
		{
			get
			{
				return base.GetSubPartOfType<ThumbnailPart>();
			}
		}

		// Token: 0x17009863 RID: 39011
		// (get) Token: 0x0601B26D RID: 111213 RVA: 0x0036D324 File Offset: 0x0036B524
		public DigitalSignatureOriginPart DigitalSignatureOriginPart
		{
			get
			{
				return base.GetSubPartOfType<DigitalSignatureOriginPart>();
			}
		}

		// Token: 0x17009864 RID: 39012
		// (get) Token: 0x0601B26E RID: 111214 RVA: 0x0036D32C File Offset: 0x0036B52C
		public RibbonExtensibilityPart RibbonExtensibilityPart
		{
			get
			{
				return base.GetSubPartOfType<RibbonExtensibilityPart>();
			}
		}

		// Token: 0x17009865 RID: 39013
		// (get) Token: 0x0601B26F RID: 111215 RVA: 0x0036D334 File Offset: 0x0036B534
		public QuickAccessToolbarCustomizationsPart QuickAccessToolbarCustomizationsPart
		{
			get
			{
				return base.GetSubPartOfType<QuickAccessToolbarCustomizationsPart>();
			}
		}

		// Token: 0x17009866 RID: 39014
		// (get) Token: 0x0601B270 RID: 111216 RVA: 0x0036D33C File Offset: 0x0036B53C
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public RibbonAndBackstageCustomizationsPart RibbonAndBackstageCustomizationsPart
		{
			get
			{
				return base.GetSubPartOfType<RibbonAndBackstageCustomizationsPart>();
			}
		}

		// Token: 0x0400B3E5 RID: 46053
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x0400B3E6 RID: 46054
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x0400B3E7 RID: 46055
		private static Dictionary<PresentationDocumentType, string> _validMainPartContentType;

		// Token: 0x0400B3E8 RID: 46056
		private PresentationDocumentType _documentType;
	}
}
