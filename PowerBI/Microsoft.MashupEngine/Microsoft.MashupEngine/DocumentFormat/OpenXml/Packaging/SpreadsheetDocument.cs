using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020030D1 RID: 12497
	internal class SpreadsheetDocument : OpenXmlPackage
	{
		// Token: 0x0601B21B RID: 111131 RVA: 0x0036D344 File Offset: 0x0036B544
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (SpreadsheetDocument._partConstraint == null)
			{
				SpreadsheetDocument._partConstraint = new Dictionary<string, PartConstraintRule>
				{
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument",
						new PartConstraintRule("WorkbookPart", null, true, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
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
			return SpreadsheetDocument._partConstraint;
		}

		// Token: 0x0601B21C RID: 111132 RVA: 0x0036D46C File Offset: 0x0036B66C
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (SpreadsheetDocument._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				SpreadsheetDocument._dataPartReferenceConstraint = dictionary;
			}
			return SpreadsheetDocument._dataPartReferenceConstraint;
		}

		// Token: 0x1700984D RID: 38989
		// (get) Token: 0x0601B21D RID: 111133 RVA: 0x002A3F0C File Offset: 0x002A210C
		internal sealed override string MainPartRelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument";
			}
		}

		// Token: 0x1700984E RID: 38990
		// (get) Token: 0x0601B21E RID: 111134 RVA: 0x0036D494 File Offset: 0x0036B694
		private static Dictionary<SpreadsheetDocumentType, string> MainPartContentTypes
		{
			get
			{
				if (SpreadsheetDocument._validMainPartContentType == null)
				{
					SpreadsheetDocument._validMainPartContentType = new Dictionary<SpreadsheetDocumentType, string>
					{
						{
							SpreadsheetDocumentType.Workbook,
							"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml"
						},
						{
							SpreadsheetDocumentType.Template,
							"application/vnd.openxmlformats-officedocument.spreadsheetml.template.main+xml"
						},
						{
							SpreadsheetDocumentType.MacroEnabledWorkbook,
							"application/vnd.ms-excel.sheet.macroEnabled.main+xml"
						},
						{
							SpreadsheetDocumentType.MacroEnabledTemplate,
							"application/vnd.ms-excel.template.macroEnabled.main+xml"
						},
						{
							SpreadsheetDocumentType.AddIn,
							"application/vnd.ms-excel.addin.macroEnabled.main+xml"
						}
					};
				}
				return SpreadsheetDocument._validMainPartContentType;
			}
		}

		// Token: 0x1700984F RID: 38991
		// (get) Token: 0x0601B21F RID: 111135 RVA: 0x0036D4F5 File Offset: 0x0036B6F5
		internal sealed override ICollection<string> ValidMainPartContentTypes
		{
			get
			{
				return SpreadsheetDocument.MainPartContentTypes.Values;
			}
		}

		// Token: 0x0601B220 RID: 111136 RVA: 0x0036CB3D File Offset: 0x0036AD3D
		protected SpreadsheetDocument()
		{
		}

		// Token: 0x17009850 RID: 38992
		// (get) Token: 0x0601B221 RID: 111137 RVA: 0x0036D501 File Offset: 0x0036B701
		// (set) Token: 0x0601B222 RID: 111138 RVA: 0x0036D50F File Offset: 0x0036B70F
		public SpreadsheetDocumentType DocumentType
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

		// Token: 0x0601B223 RID: 111139 RVA: 0x0036D520 File Offset: 0x0036B720
		private void UpdateDocumentTypeFromContentType()
		{
			if (base.MainPartContentType == null)
			{
				throw new InvalidOperationException();
			}
			foreach (KeyValuePair<SpreadsheetDocumentType, string> keyValuePair in SpreadsheetDocument.MainPartContentTypes)
			{
				if (keyValuePair.Value == base.MainPartContentType)
				{
					this.DocumentType = keyValuePair.Key;
				}
			}
		}

		// Token: 0x0601B224 RID: 111140 RVA: 0x0036D59C File Offset: 0x0036B79C
		public static SpreadsheetDocument Create(string path, SpreadsheetDocumentType type)
		{
			return SpreadsheetDocument.Create(path, type, true);
		}

		// Token: 0x0601B225 RID: 111141 RVA: 0x0036D5A6 File Offset: 0x0036B7A6
		public static SpreadsheetDocument Create(Stream stream, SpreadsheetDocumentType type)
		{
			return SpreadsheetDocument.Create(stream, type, true);
		}

		// Token: 0x0601B226 RID: 111142 RVA: 0x0036D5B0 File Offset: 0x0036B7B0
		public static SpreadsheetDocument Create(Package package, SpreadsheetDocumentType type)
		{
			return SpreadsheetDocument.Create(package, type, true);
		}

		// Token: 0x0601B227 RID: 111143 RVA: 0x0036D5BC File Offset: 0x0036B7BC
		public static SpreadsheetDocument Create(string path, SpreadsheetDocumentType type, bool autoSave)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			SpreadsheetDocument spreadsheetDocument = new SpreadsheetDocument();
			spreadsheetDocument.DocumentType = type;
			spreadsheetDocument.OpenSettings = new OpenSettings();
			spreadsheetDocument.OpenSettings.AutoSave = autoSave;
			spreadsheetDocument.MainPartContentType = SpreadsheetDocument.MainPartContentTypes[type];
			spreadsheetDocument.CreateCore(path);
			return spreadsheetDocument;
		}

		// Token: 0x0601B228 RID: 111144 RVA: 0x0036D61C File Offset: 0x0036B81C
		public static SpreadsheetDocument Create(Stream stream, SpreadsheetDocumentType type, bool autoSave)
		{
			SpreadsheetDocument spreadsheetDocument = new SpreadsheetDocument();
			spreadsheetDocument.DocumentType = type;
			spreadsheetDocument.OpenSettings = new OpenSettings();
			spreadsheetDocument.OpenSettings.AutoSave = autoSave;
			spreadsheetDocument.MainPartContentType = SpreadsheetDocument.MainPartContentTypes[type];
			spreadsheetDocument.CreateCore(stream);
			return spreadsheetDocument;
		}

		// Token: 0x0601B229 RID: 111145 RVA: 0x0036D668 File Offset: 0x0036B868
		public static SpreadsheetDocument Create(Package package, SpreadsheetDocumentType type, bool autoSave)
		{
			SpreadsheetDocument spreadsheetDocument = new SpreadsheetDocument();
			spreadsheetDocument.DocumentType = type;
			spreadsheetDocument.OpenSettings = new OpenSettings();
			spreadsheetDocument.OpenSettings.AutoSave = autoSave;
			spreadsheetDocument.MainPartContentType = SpreadsheetDocument.MainPartContentTypes[type];
			spreadsheetDocument.CreateCore(package);
			return spreadsheetDocument;
		}

		// Token: 0x0601B22A RID: 111146 RVA: 0x0036D6B4 File Offset: 0x0036B8B4
		public static SpreadsheetDocument Open(string path, bool isEditable, OpenSettings openSettings)
		{
			if (openSettings.MarkupCompatibilityProcessSettings.ProcessMode != MarkupCompatibilityProcessMode.NoProcess && openSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions != FileFormatVersions.Office2007 && openSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions != FileFormatVersions.Office2010)
			{
				throw new ArgumentException(ExceptionMessages.InvalidMCMode);
			}
			SpreadsheetDocument spreadsheetDocument = new SpreadsheetDocument();
			spreadsheetDocument.OpenSettings = new OpenSettings();
			spreadsheetDocument.OpenSettings.AutoSave = openSettings.AutoSave;
			spreadsheetDocument.OpenSettings.MarkupCompatibilityProcessSettings.ProcessMode = openSettings.MarkupCompatibilityProcessSettings.ProcessMode;
			spreadsheetDocument.OpenSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions = openSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions;
			spreadsheetDocument.MaxCharactersInPart = openSettings.MaxCharactersInPart;
			spreadsheetDocument.OpenCore(path, isEditable);
			if (SpreadsheetDocument.MainPartContentTypes[spreadsheetDocument.DocumentType] != spreadsheetDocument.MainPartContentType)
			{
				spreadsheetDocument.UpdateDocumentTypeFromContentType();
			}
			return spreadsheetDocument;
		}

		// Token: 0x0601B22B RID: 111147 RVA: 0x0036D788 File Offset: 0x0036B988
		public static SpreadsheetDocument Open(Stream stream, bool isEditable, OpenSettings openSettings)
		{
			if (openSettings.MarkupCompatibilityProcessSettings.ProcessMode != MarkupCompatibilityProcessMode.NoProcess && openSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions != FileFormatVersions.Office2007 && openSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions != FileFormatVersions.Office2010)
			{
				throw new ArgumentException(ExceptionMessages.InvalidMCMode);
			}
			SpreadsheetDocument spreadsheetDocument = new SpreadsheetDocument();
			spreadsheetDocument.OpenSettings = new OpenSettings();
			spreadsheetDocument.OpenSettings.AutoSave = openSettings.AutoSave;
			spreadsheetDocument.OpenSettings.MarkupCompatibilityProcessSettings.ProcessMode = openSettings.MarkupCompatibilityProcessSettings.ProcessMode;
			spreadsheetDocument.OpenSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions = openSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions;
			spreadsheetDocument.MaxCharactersInPart = openSettings.MaxCharactersInPart;
			spreadsheetDocument.OpenCore(stream, isEditable);
			if (SpreadsheetDocument.MainPartContentTypes[spreadsheetDocument.DocumentType] != spreadsheetDocument.MainPartContentType)
			{
				spreadsheetDocument.UpdateDocumentTypeFromContentType();
			}
			return spreadsheetDocument;
		}

		// Token: 0x0601B22C RID: 111148 RVA: 0x0036D85C File Offset: 0x0036BA5C
		public static SpreadsheetDocument Open(Package package, OpenSettings openSettings)
		{
			if (openSettings.MarkupCompatibilityProcessSettings.ProcessMode != MarkupCompatibilityProcessMode.NoProcess && openSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions != FileFormatVersions.Office2007 && openSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions != FileFormatVersions.Office2010)
			{
				throw new ArgumentException(ExceptionMessages.InvalidMCMode);
			}
			SpreadsheetDocument spreadsheetDocument = new SpreadsheetDocument();
			spreadsheetDocument.OpenSettings = new OpenSettings();
			spreadsheetDocument.OpenSettings.AutoSave = openSettings.AutoSave;
			spreadsheetDocument.OpenSettings.MarkupCompatibilityProcessSettings.ProcessMode = openSettings.MarkupCompatibilityProcessSettings.ProcessMode;
			spreadsheetDocument.OpenSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions = openSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions;
			spreadsheetDocument.MaxCharactersInPart = openSettings.MaxCharactersInPart;
			spreadsheetDocument.OpenCore(package);
			if (SpreadsheetDocument.MainPartContentTypes[spreadsheetDocument.DocumentType] != spreadsheetDocument.MainPartContentType)
			{
				spreadsheetDocument.UpdateDocumentTypeFromContentType();
			}
			return spreadsheetDocument;
		}

		// Token: 0x0601B22D RID: 111149 RVA: 0x0036D92C File Offset: 0x0036BB2C
		public static SpreadsheetDocument Open(string path, bool isEditable)
		{
			return SpreadsheetDocument.Open(path, isEditable, new OpenSettings());
		}

		// Token: 0x0601B22E RID: 111150 RVA: 0x0036D93A File Offset: 0x0036BB3A
		public static SpreadsheetDocument Open(Stream stream, bool isEditable)
		{
			return SpreadsheetDocument.Open(stream, isEditable, new OpenSettings());
		}

		// Token: 0x0601B22F RID: 111151 RVA: 0x0036D948 File Offset: 0x0036BB48
		public static SpreadsheetDocument Open(Package package)
		{
			return SpreadsheetDocument.Open(package, new OpenSettings());
		}

		// Token: 0x0601B230 RID: 111152 RVA: 0x0036D958 File Offset: 0x0036BB58
		public void ChangeDocumentType(SpreadsheetDocumentType newType)
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
			SpreadsheetDocumentType documentType = this.DocumentType;
			this.DocumentType = newType;
			base.MainPartContentType = SpreadsheetDocument.MainPartContentTypes[newType];
			if (this.WorkbookPart == null)
			{
				return;
			}
			try
			{
				base.ChangeDocumentTypeInternal<WorkbookPart>();
			}
			catch (OpenXmlPackageException ex)
			{
				if (ex.Message == ExceptionMessages.CannotChangeDocumentType)
				{
					this.DocumentType = documentType;
					base.MainPartContentType = SpreadsheetDocument.MainPartContentTypes[documentType];
				}
				throw;
			}
		}

		// Token: 0x0601B231 RID: 111153 RVA: 0x0036D9F8 File Offset: 0x0036BBF8
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
				return new WorkbookPart();
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

		// Token: 0x0601B232 RID: 111154 RVA: 0x0036DB24 File Offset: 0x0036BD24
		public override T AddNewPart<T>(string contentType, string id)
		{
			if (contentType == null)
			{
				throw new ArgumentNullException("contentType");
			}
			if (typeof(WorkbookPart).IsAssignableFrom(typeof(T)) && contentType != SpreadsheetDocument.MainPartContentTypes[this._documentType])
			{
				throw new OpenXmlPackageException(ExceptionMessages.ErrorContentType);
			}
			return base.AddNewPart<T>(contentType, id);
		}

		// Token: 0x0601B233 RID: 111155 RVA: 0x0036DB88 File Offset: 0x0036BD88
		public WorkbookPart AddWorkbookPart()
		{
			WorkbookPart workbookPart = new WorkbookPart();
			base.InitPart<WorkbookPart>(workbookPart, base.MainPartContentType);
			return workbookPart;
		}

		// Token: 0x0601B234 RID: 111156 RVA: 0x0036DBAC File Offset: 0x0036BDAC
		public CoreFilePropertiesPart AddCoreFilePropertiesPart()
		{
			CoreFilePropertiesPart coreFilePropertiesPart = new CoreFilePropertiesPart();
			base.InitPart<CoreFilePropertiesPart>(coreFilePropertiesPart, "application/vnd.openxmlformats-package.core-properties+xml");
			return coreFilePropertiesPart;
		}

		// Token: 0x0601B235 RID: 111157 RVA: 0x0036DBCC File Offset: 0x0036BDCC
		public ExtendedFilePropertiesPart AddExtendedFilePropertiesPart()
		{
			ExtendedFilePropertiesPart extendedFilePropertiesPart = new ExtendedFilePropertiesPart();
			base.InitPart<ExtendedFilePropertiesPart>(extendedFilePropertiesPart, "application/vnd.openxmlformats-officedocument.extended-properties+xml");
			return extendedFilePropertiesPart;
		}

		// Token: 0x0601B236 RID: 111158 RVA: 0x0036DBEC File Offset: 0x0036BDEC
		public CustomFilePropertiesPart AddCustomFilePropertiesPart()
		{
			CustomFilePropertiesPart customFilePropertiesPart = new CustomFilePropertiesPart();
			base.InitPart<CustomFilePropertiesPart>(customFilePropertiesPart, "application/vnd.openxmlformats-officedocument.custom-properties+xml");
			return customFilePropertiesPart;
		}

		// Token: 0x0601B237 RID: 111159 RVA: 0x0036DC0C File Offset: 0x0036BE0C
		public DigitalSignatureOriginPart AddDigitalSignatureOriginPart()
		{
			DigitalSignatureOriginPart digitalSignatureOriginPart = new DigitalSignatureOriginPart();
			base.InitPart<DigitalSignatureOriginPart>(digitalSignatureOriginPart, "application/vnd.openxmlformats-package.digital-signature-origin");
			return digitalSignatureOriginPart;
		}

		// Token: 0x0601B238 RID: 111160 RVA: 0x0036DC2C File Offset: 0x0036BE2C
		public ThumbnailPart AddThumbnailPart(string contentType)
		{
			ThumbnailPart thumbnailPart = new ThumbnailPart();
			base.InitPart<ThumbnailPart>(thumbnailPart, contentType);
			return thumbnailPart;
		}

		// Token: 0x0601B239 RID: 111161 RVA: 0x0036DC48 File Offset: 0x0036BE48
		public ThumbnailPart AddThumbnailPart(ThumbnailPartType partType)
		{
			string contentType = ThumbnailPartTypeInfo.GetContentType(partType);
			string targetExtension = ThumbnailPartTypeInfo.GetTargetExtension(partType);
			base.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddThumbnailPart(contentType);
		}

		// Token: 0x0601B23A RID: 111162 RVA: 0x0036DC78 File Offset: 0x0036BE78
		public QuickAccessToolbarCustomizationsPart AddQuickAccessToolbarCustomizationsPart()
		{
			QuickAccessToolbarCustomizationsPart quickAccessToolbarCustomizationsPart = new QuickAccessToolbarCustomizationsPart();
			base.InitPart<QuickAccessToolbarCustomizationsPart>(quickAccessToolbarCustomizationsPart, "application/xml");
			return quickAccessToolbarCustomizationsPart;
		}

		// Token: 0x0601B23B RID: 111163 RVA: 0x0036DC98 File Offset: 0x0036BE98
		public RibbonExtensibilityPart AddRibbonExtensibilityPart()
		{
			RibbonExtensibilityPart ribbonExtensibilityPart = new RibbonExtensibilityPart();
			base.InitPart<RibbonExtensibilityPart>(ribbonExtensibilityPart, "application/xml");
			return ribbonExtensibilityPart;
		}

		// Token: 0x0601B23C RID: 111164 RVA: 0x0036DCB8 File Offset: 0x0036BEB8
		public RibbonAndBackstageCustomizationsPart AddRibbonAndBackstageCustomizationsPart()
		{
			RibbonAndBackstageCustomizationsPart ribbonAndBackstageCustomizationsPart = new RibbonAndBackstageCustomizationsPart();
			base.InitPart<RibbonAndBackstageCustomizationsPart>(ribbonAndBackstageCustomizationsPart, "application/xml");
			return ribbonAndBackstageCustomizationsPart;
		}

		// Token: 0x17009851 RID: 38993
		// (get) Token: 0x0601B23D RID: 111165 RVA: 0x0036DCD8 File Offset: 0x0036BED8
		public WorkbookPart WorkbookPart
		{
			get
			{
				return base.GetSubPartOfType<WorkbookPart>();
			}
		}

		// Token: 0x17009852 RID: 38994
		// (get) Token: 0x0601B23E RID: 111166 RVA: 0x0036D30C File Offset: 0x0036B50C
		public CoreFilePropertiesPart CoreFilePropertiesPart
		{
			get
			{
				return base.GetSubPartOfType<CoreFilePropertiesPart>();
			}
		}

		// Token: 0x17009853 RID: 38995
		// (get) Token: 0x0601B23F RID: 111167 RVA: 0x0036D314 File Offset: 0x0036B514
		public ExtendedFilePropertiesPart ExtendedFilePropertiesPart
		{
			get
			{
				return base.GetSubPartOfType<ExtendedFilePropertiesPart>();
			}
		}

		// Token: 0x17009854 RID: 38996
		// (get) Token: 0x0601B240 RID: 111168 RVA: 0x0036D31C File Offset: 0x0036B51C
		public CustomFilePropertiesPart CustomFilePropertiesPart
		{
			get
			{
				return base.GetSubPartOfType<CustomFilePropertiesPart>();
			}
		}

		// Token: 0x17009855 RID: 38997
		// (get) Token: 0x0601B241 RID: 111169 RVA: 0x002A3F39 File Offset: 0x002A2139
		public ThumbnailPart ThumbnailPart
		{
			get
			{
				return base.GetSubPartOfType<ThumbnailPart>();
			}
		}

		// Token: 0x17009856 RID: 38998
		// (get) Token: 0x0601B242 RID: 111170 RVA: 0x0036D32C File Offset: 0x0036B52C
		public RibbonExtensibilityPart RibbonExtensibilityPart
		{
			get
			{
				return base.GetSubPartOfType<RibbonExtensibilityPart>();
			}
		}

		// Token: 0x17009857 RID: 38999
		// (get) Token: 0x0601B243 RID: 111171 RVA: 0x0036D334 File Offset: 0x0036B534
		public QuickAccessToolbarCustomizationsPart QuickAccessToolbarCustomizationsPart
		{
			get
			{
				return base.GetSubPartOfType<QuickAccessToolbarCustomizationsPart>();
			}
		}

		// Token: 0x17009858 RID: 39000
		// (get) Token: 0x0601B244 RID: 111172 RVA: 0x0036D324 File Offset: 0x0036B524
		public DigitalSignatureOriginPart DigitalSignatureOriginPart
		{
			get
			{
				return base.GetSubPartOfType<DigitalSignatureOriginPart>();
			}
		}

		// Token: 0x17009859 RID: 39001
		// (get) Token: 0x0601B245 RID: 111173 RVA: 0x0036D33C File Offset: 0x0036B53C
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public RibbonAndBackstageCustomizationsPart RibbonAndBackstageCustomizationsPart
		{
			get
			{
				return base.GetSubPartOfType<RibbonAndBackstageCustomizationsPart>();
			}
		}

		// Token: 0x0400B3E1 RID: 46049
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x0400B3E2 RID: 46050
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x0400B3E3 RID: 46051
		private static Dictionary<SpreadsheetDocumentType, string> _validMainPartContentType;

		// Token: 0x0400B3E4 RID: 46052
		private SpreadsheetDocumentType _documentType;
	}
}
