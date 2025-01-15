using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020030D0 RID: 12496
	internal class WordprocessingDocument : OpenXmlPackage
	{
		// Token: 0x0601B1F0 RID: 111088 RVA: 0x0036C98C File Offset: 0x0036AB8C
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (WordprocessingDocument._partConstraint == null)
			{
				WordprocessingDocument._partConstraint = new Dictionary<string, PartConstraintRule>
				{
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument",
						new PartConstraintRule("MainDocumentPart", null, true, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
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
			return WordprocessingDocument._partConstraint;
		}

		// Token: 0x0601B1F1 RID: 111089 RVA: 0x0036CAB4 File Offset: 0x0036ACB4
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (WordprocessingDocument._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				WordprocessingDocument._dataPartReferenceConstraint = dictionary;
			}
			return WordprocessingDocument._dataPartReferenceConstraint;
		}

		// Token: 0x17009840 RID: 38976
		// (get) Token: 0x0601B1F2 RID: 111090 RVA: 0x002A3F0C File Offset: 0x002A210C
		internal sealed override string MainPartRelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument";
			}
		}

		// Token: 0x17009841 RID: 38977
		// (get) Token: 0x0601B1F3 RID: 111091 RVA: 0x0036CADC File Offset: 0x0036ACDC
		private static Dictionary<WordprocessingDocumentType, string> MainPartContentTypes
		{
			get
			{
				if (WordprocessingDocument._validMainPartContentType == null)
				{
					WordprocessingDocument._validMainPartContentType = new Dictionary<WordprocessingDocumentType, string>
					{
						{
							WordprocessingDocumentType.Document,
							"application/vnd.openxmlformats-officedocument.wordprocessingml.document.main+xml"
						},
						{
							WordprocessingDocumentType.Template,
							"application/vnd.openxmlformats-officedocument.wordprocessingml.template.main+xml"
						},
						{
							WordprocessingDocumentType.MacroEnabledDocument,
							"application/vnd.ms-word.document.macroEnabled.main+xml"
						},
						{
							WordprocessingDocumentType.MacroEnabledTemplate,
							"application/vnd.ms-word.template.macroEnabledTemplate.main+xml"
						}
					};
				}
				return WordprocessingDocument._validMainPartContentType;
			}
		}

		// Token: 0x17009842 RID: 38978
		// (get) Token: 0x0601B1F4 RID: 111092 RVA: 0x0036CB31 File Offset: 0x0036AD31
		internal sealed override ICollection<string> ValidMainPartContentTypes
		{
			get
			{
				return WordprocessingDocument.MainPartContentTypes.Values;
			}
		}

		// Token: 0x0601B1F5 RID: 111093 RVA: 0x0036CB3D File Offset: 0x0036AD3D
		protected WordprocessingDocument()
		{
		}

		// Token: 0x17009843 RID: 38979
		// (get) Token: 0x0601B1F6 RID: 111094 RVA: 0x0036CB45 File Offset: 0x0036AD45
		// (set) Token: 0x0601B1F7 RID: 111095 RVA: 0x0036CB53 File Offset: 0x0036AD53
		public WordprocessingDocumentType DocumentType
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

		// Token: 0x0601B1F8 RID: 111096 RVA: 0x0036CB64 File Offset: 0x0036AD64
		private void UpdateDocumentTypeFromContentType()
		{
			if (base.MainPartContentType == null)
			{
				throw new InvalidOperationException();
			}
			foreach (KeyValuePair<WordprocessingDocumentType, string> keyValuePair in WordprocessingDocument.MainPartContentTypes)
			{
				if (keyValuePair.Value == base.MainPartContentType)
				{
					this.DocumentType = keyValuePair.Key;
				}
			}
		}

		// Token: 0x0601B1F9 RID: 111097 RVA: 0x0036CBE0 File Offset: 0x0036ADE0
		public static WordprocessingDocument Create(string path, WordprocessingDocumentType type)
		{
			return WordprocessingDocument.Create(path, type, true);
		}

		// Token: 0x0601B1FA RID: 111098 RVA: 0x0036CBEA File Offset: 0x0036ADEA
		public static WordprocessingDocument Create(Stream stream, WordprocessingDocumentType type)
		{
			return WordprocessingDocument.Create(stream, type, true);
		}

		// Token: 0x0601B1FB RID: 111099 RVA: 0x0036CBF4 File Offset: 0x0036ADF4
		public static WordprocessingDocument Create(Package package, WordprocessingDocumentType type)
		{
			return WordprocessingDocument.Create(package, type, true);
		}

		// Token: 0x0601B1FC RID: 111100 RVA: 0x0036CC00 File Offset: 0x0036AE00
		public static WordprocessingDocument Create(string path, WordprocessingDocumentType type, bool autoSave)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			WordprocessingDocument wordprocessingDocument = new WordprocessingDocument();
			wordprocessingDocument.DocumentType = type;
			wordprocessingDocument.OpenSettings = new OpenSettings();
			wordprocessingDocument.OpenSettings.AutoSave = autoSave;
			wordprocessingDocument.MainPartContentType = WordprocessingDocument.MainPartContentTypes[type];
			wordprocessingDocument.CreateCore(path);
			return wordprocessingDocument;
		}

		// Token: 0x0601B1FD RID: 111101 RVA: 0x0036CC60 File Offset: 0x0036AE60
		public static WordprocessingDocument Create(Stream stream, WordprocessingDocumentType type, bool autoSave)
		{
			WordprocessingDocument wordprocessingDocument = new WordprocessingDocument();
			wordprocessingDocument.DocumentType = type;
			wordprocessingDocument.OpenSettings = new OpenSettings();
			wordprocessingDocument.OpenSettings.AutoSave = autoSave;
			wordprocessingDocument.MainPartContentType = WordprocessingDocument.MainPartContentTypes[type];
			wordprocessingDocument.CreateCore(stream);
			return wordprocessingDocument;
		}

		// Token: 0x0601B1FE RID: 111102 RVA: 0x0036CCAC File Offset: 0x0036AEAC
		public static WordprocessingDocument Create(Package package, WordprocessingDocumentType type, bool autoSave)
		{
			WordprocessingDocument wordprocessingDocument = new WordprocessingDocument();
			wordprocessingDocument.DocumentType = type;
			wordprocessingDocument.OpenSettings = new OpenSettings();
			wordprocessingDocument.OpenSettings.AutoSave = autoSave;
			wordprocessingDocument.MainPartContentType = WordprocessingDocument.MainPartContentTypes[type];
			wordprocessingDocument.CreateCore(package);
			return wordprocessingDocument;
		}

		// Token: 0x0601B1FF RID: 111103 RVA: 0x0036CCF6 File Offset: 0x0036AEF6
		public static WordprocessingDocument Open(string path, bool isEditable)
		{
			return WordprocessingDocument.Open(path, isEditable, new OpenSettings());
		}

		// Token: 0x0601B200 RID: 111104 RVA: 0x0036CD04 File Offset: 0x0036AF04
		public static WordprocessingDocument Open(Stream stream, bool isEditable)
		{
			return WordprocessingDocument.Open(stream, isEditable, new OpenSettings());
		}

		// Token: 0x0601B201 RID: 111105 RVA: 0x0036CD14 File Offset: 0x0036AF14
		public static WordprocessingDocument Open(string path, bool isEditable, OpenSettings openSettings)
		{
			if (openSettings.MarkupCompatibilityProcessSettings.ProcessMode != MarkupCompatibilityProcessMode.NoProcess && openSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions != FileFormatVersions.Office2007 && openSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions != FileFormatVersions.Office2010)
			{
				throw new ArgumentException(ExceptionMessages.InvalidMCMode);
			}
			WordprocessingDocument wordprocessingDocument = new WordprocessingDocument();
			wordprocessingDocument.OpenSettings = new OpenSettings();
			wordprocessingDocument.OpenSettings.AutoSave = openSettings.AutoSave;
			wordprocessingDocument.OpenSettings.MarkupCompatibilityProcessSettings.ProcessMode = openSettings.MarkupCompatibilityProcessSettings.ProcessMode;
			wordprocessingDocument.OpenSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions = openSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions;
			wordprocessingDocument.MaxCharactersInPart = openSettings.MaxCharactersInPart;
			wordprocessingDocument.OpenCore(path, isEditable);
			if (WordprocessingDocument.MainPartContentTypes[wordprocessingDocument.DocumentType] != wordprocessingDocument.MainPartContentType)
			{
				wordprocessingDocument.UpdateDocumentTypeFromContentType();
			}
			return wordprocessingDocument;
		}

		// Token: 0x0601B202 RID: 111106 RVA: 0x0036CDE8 File Offset: 0x0036AFE8
		public static WordprocessingDocument Open(Stream stream, bool isEditable, OpenSettings openSettings)
		{
			if (openSettings.MarkupCompatibilityProcessSettings.ProcessMode != MarkupCompatibilityProcessMode.NoProcess && openSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions != FileFormatVersions.Office2007 && openSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions != FileFormatVersions.Office2010)
			{
				throw new ArgumentException(ExceptionMessages.InvalidMCMode);
			}
			WordprocessingDocument wordprocessingDocument = new WordprocessingDocument();
			wordprocessingDocument.OpenSettings = new OpenSettings();
			wordprocessingDocument.OpenSettings.AutoSave = openSettings.AutoSave;
			wordprocessingDocument.OpenSettings.MarkupCompatibilityProcessSettings.ProcessMode = openSettings.MarkupCompatibilityProcessSettings.ProcessMode;
			wordprocessingDocument.OpenSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions = openSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions;
			wordprocessingDocument.MaxCharactersInPart = openSettings.MaxCharactersInPart;
			wordprocessingDocument.OpenCore(stream, isEditable);
			if (WordprocessingDocument.MainPartContentTypes[wordprocessingDocument.DocumentType] != wordprocessingDocument.MainPartContentType)
			{
				wordprocessingDocument.UpdateDocumentTypeFromContentType();
			}
			return wordprocessingDocument;
		}

		// Token: 0x0601B203 RID: 111107 RVA: 0x0036CEBC File Offset: 0x0036B0BC
		public static WordprocessingDocument Open(Package package, OpenSettings openSettings)
		{
			if (openSettings.MarkupCompatibilityProcessSettings.ProcessMode != MarkupCompatibilityProcessMode.NoProcess && openSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions != FileFormatVersions.Office2007 && openSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions != FileFormatVersions.Office2010)
			{
				throw new ArgumentException(ExceptionMessages.InvalidMCMode);
			}
			WordprocessingDocument wordprocessingDocument = new WordprocessingDocument();
			wordprocessingDocument.OpenSettings = new OpenSettings();
			wordprocessingDocument.OpenSettings.AutoSave = openSettings.AutoSave;
			wordprocessingDocument.OpenSettings.MarkupCompatibilityProcessSettings.ProcessMode = openSettings.MarkupCompatibilityProcessSettings.ProcessMode;
			wordprocessingDocument.OpenSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions = openSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions;
			wordprocessingDocument.MaxCharactersInPart = openSettings.MaxCharactersInPart;
			wordprocessingDocument.OpenCore(package);
			if (WordprocessingDocument.MainPartContentTypes[wordprocessingDocument.DocumentType] != wordprocessingDocument.MainPartContentType)
			{
				wordprocessingDocument.UpdateDocumentTypeFromContentType();
			}
			return wordprocessingDocument;
		}

		// Token: 0x0601B204 RID: 111108 RVA: 0x0036CF8C File Offset: 0x0036B18C
		public static WordprocessingDocument Open(Package package)
		{
			return WordprocessingDocument.Open(package, new OpenSettings());
		}

		// Token: 0x0601B205 RID: 111109 RVA: 0x0036CF9C File Offset: 0x0036B19C
		public void ChangeDocumentType(WordprocessingDocumentType newType)
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
			WordprocessingDocumentType documentType = this.DocumentType;
			this.DocumentType = newType;
			base.MainPartContentType = WordprocessingDocument.MainPartContentTypes[newType];
			if (this.MainDocumentPart == null)
			{
				return;
			}
			try
			{
				base.ChangeDocumentTypeInternal<MainDocumentPart>();
			}
			catch (OpenXmlPackageException ex)
			{
				if (ex.Message == ExceptionMessages.CannotChangeDocumentType)
				{
					this.DocumentType = documentType;
					base.MainPartContentType = WordprocessingDocument.MainPartContentTypes[documentType];
				}
				throw;
			}
		}

		// Token: 0x0601B206 RID: 111110 RVA: 0x0036D03C File Offset: 0x0036B23C
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
				return new MainDocumentPart();
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
			}
			throw new ArgumentOutOfRangeException("relationshipType");
		}

		// Token: 0x0601B207 RID: 111111 RVA: 0x0036D150 File Offset: 0x0036B350
		public override T AddNewPart<T>(string contentType, string id)
		{
			if (contentType == null)
			{
				throw new ArgumentNullException("contentType");
			}
			if (typeof(MainDocumentPart).IsAssignableFrom(typeof(T)) && contentType != WordprocessingDocument.MainPartContentTypes[this._documentType])
			{
				throw new OpenXmlPackageException(ExceptionMessages.ErrorContentType);
			}
			return base.AddNewPart<T>(contentType, id);
		}

		// Token: 0x0601B208 RID: 111112 RVA: 0x0036D1B4 File Offset: 0x0036B3B4
		public MainDocumentPart AddMainDocumentPart()
		{
			MainDocumentPart mainDocumentPart = new MainDocumentPart();
			base.InitPart<MainDocumentPart>(mainDocumentPart, base.MainPartContentType);
			return mainDocumentPart;
		}

		// Token: 0x0601B209 RID: 111113 RVA: 0x0036D1D8 File Offset: 0x0036B3D8
		public CoreFilePropertiesPart AddCoreFilePropertiesPart()
		{
			CoreFilePropertiesPart coreFilePropertiesPart = new CoreFilePropertiesPart();
			base.InitPart<CoreFilePropertiesPart>(coreFilePropertiesPart, "application/vnd.openxmlformats-package.core-properties+xml");
			return coreFilePropertiesPart;
		}

		// Token: 0x0601B20A RID: 111114 RVA: 0x0036D1F8 File Offset: 0x0036B3F8
		public ExtendedFilePropertiesPart AddExtendedFilePropertiesPart()
		{
			ExtendedFilePropertiesPart extendedFilePropertiesPart = new ExtendedFilePropertiesPart();
			base.InitPart<ExtendedFilePropertiesPart>(extendedFilePropertiesPart, "application/vnd.openxmlformats-officedocument.extended-properties+xml");
			return extendedFilePropertiesPart;
		}

		// Token: 0x0601B20B RID: 111115 RVA: 0x0036D218 File Offset: 0x0036B418
		public CustomFilePropertiesPart AddCustomFilePropertiesPart()
		{
			CustomFilePropertiesPart customFilePropertiesPart = new CustomFilePropertiesPart();
			base.InitPart<CustomFilePropertiesPart>(customFilePropertiesPart, "application/vnd.openxmlformats-officedocument.custom-properties+xml");
			return customFilePropertiesPart;
		}

		// Token: 0x0601B20C RID: 111116 RVA: 0x0036D238 File Offset: 0x0036B438
		public DigitalSignatureOriginPart AddDigitalSignatureOriginPart()
		{
			DigitalSignatureOriginPart digitalSignatureOriginPart = new DigitalSignatureOriginPart();
			base.InitPart<DigitalSignatureOriginPart>(digitalSignatureOriginPart, "application/vnd.openxmlformats-package.digital-signature-origin");
			return digitalSignatureOriginPart;
		}

		// Token: 0x0601B20D RID: 111117 RVA: 0x0036D258 File Offset: 0x0036B458
		public ThumbnailPart AddThumbnailPart(string contentType)
		{
			ThumbnailPart thumbnailPart = new ThumbnailPart();
			base.InitPart<ThumbnailPart>(thumbnailPart, contentType);
			return thumbnailPart;
		}

		// Token: 0x0601B20E RID: 111118 RVA: 0x0036D274 File Offset: 0x0036B474
		public ThumbnailPart AddThumbnailPart(ThumbnailPartType partType)
		{
			string contentType = ThumbnailPartTypeInfo.GetContentType(partType);
			string targetExtension = ThumbnailPartTypeInfo.GetTargetExtension(partType);
			base.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddThumbnailPart(contentType);
		}

		// Token: 0x0601B20F RID: 111119 RVA: 0x0036D2A4 File Offset: 0x0036B4A4
		public QuickAccessToolbarCustomizationsPart AddQuickAccessToolbarCustomizationsPart()
		{
			QuickAccessToolbarCustomizationsPart quickAccessToolbarCustomizationsPart = new QuickAccessToolbarCustomizationsPart();
			base.InitPart<QuickAccessToolbarCustomizationsPart>(quickAccessToolbarCustomizationsPart, "application/xml");
			return quickAccessToolbarCustomizationsPart;
		}

		// Token: 0x0601B210 RID: 111120 RVA: 0x0036D2C4 File Offset: 0x0036B4C4
		public RibbonExtensibilityPart AddRibbonExtensibilityPart()
		{
			RibbonExtensibilityPart ribbonExtensibilityPart = new RibbonExtensibilityPart();
			base.InitPart<RibbonExtensibilityPart>(ribbonExtensibilityPart, "application/xml");
			return ribbonExtensibilityPart;
		}

		// Token: 0x0601B211 RID: 111121 RVA: 0x0036D2E4 File Offset: 0x0036B4E4
		public RibbonAndBackstageCustomizationsPart AddRibbonAndBackstageCustomizationsPart()
		{
			RibbonAndBackstageCustomizationsPart ribbonAndBackstageCustomizationsPart = new RibbonAndBackstageCustomizationsPart();
			base.InitPart<RibbonAndBackstageCustomizationsPart>(ribbonAndBackstageCustomizationsPart, "application/xml");
			return ribbonAndBackstageCustomizationsPart;
		}

		// Token: 0x17009844 RID: 38980
		// (get) Token: 0x0601B212 RID: 111122 RVA: 0x0036D304 File Offset: 0x0036B504
		public MainDocumentPart MainDocumentPart
		{
			get
			{
				return base.GetSubPartOfType<MainDocumentPart>();
			}
		}

		// Token: 0x17009845 RID: 38981
		// (get) Token: 0x0601B213 RID: 111123 RVA: 0x0036D30C File Offset: 0x0036B50C
		public CoreFilePropertiesPart CoreFilePropertiesPart
		{
			get
			{
				return base.GetSubPartOfType<CoreFilePropertiesPart>();
			}
		}

		// Token: 0x17009846 RID: 38982
		// (get) Token: 0x0601B214 RID: 111124 RVA: 0x0036D314 File Offset: 0x0036B514
		public ExtendedFilePropertiesPart ExtendedFilePropertiesPart
		{
			get
			{
				return base.GetSubPartOfType<ExtendedFilePropertiesPart>();
			}
		}

		// Token: 0x17009847 RID: 38983
		// (get) Token: 0x0601B215 RID: 111125 RVA: 0x0036D31C File Offset: 0x0036B51C
		public CustomFilePropertiesPart CustomFilePropertiesPart
		{
			get
			{
				return base.GetSubPartOfType<CustomFilePropertiesPart>();
			}
		}

		// Token: 0x17009848 RID: 38984
		// (get) Token: 0x0601B216 RID: 111126 RVA: 0x002A3F39 File Offset: 0x002A2139
		public ThumbnailPart ThumbnailPart
		{
			get
			{
				return base.GetSubPartOfType<ThumbnailPart>();
			}
		}

		// Token: 0x17009849 RID: 38985
		// (get) Token: 0x0601B217 RID: 111127 RVA: 0x0036D324 File Offset: 0x0036B524
		public DigitalSignatureOriginPart DigitalSignatureOriginPart
		{
			get
			{
				return base.GetSubPartOfType<DigitalSignatureOriginPart>();
			}
		}

		// Token: 0x1700984A RID: 38986
		// (get) Token: 0x0601B218 RID: 111128 RVA: 0x0036D32C File Offset: 0x0036B52C
		public RibbonExtensibilityPart RibbonExtensibilityPart
		{
			get
			{
				return base.GetSubPartOfType<RibbonExtensibilityPart>();
			}
		}

		// Token: 0x1700984B RID: 38987
		// (get) Token: 0x0601B219 RID: 111129 RVA: 0x0036D334 File Offset: 0x0036B534
		public QuickAccessToolbarCustomizationsPart QuickAccessToolbarCustomizationsPart
		{
			get
			{
				return base.GetSubPartOfType<QuickAccessToolbarCustomizationsPart>();
			}
		}

		// Token: 0x1700984C RID: 38988
		// (get) Token: 0x0601B21A RID: 111130 RVA: 0x0036D33C File Offset: 0x0036B53C
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public RibbonAndBackstageCustomizationsPart RibbonAndBackstageCustomizationsPart
		{
			get
			{
				return base.GetSubPartOfType<RibbonAndBackstageCustomizationsPart>();
			}
		}

		// Token: 0x0400B3DD RID: 46045
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x0400B3DE RID: 46046
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x0400B3DF RID: 46047
		private static Dictionary<WordprocessingDocumentType, string> _validMainPartContentType;

		// Token: 0x0400B3E0 RID: 46048
		private WordprocessingDocumentType _documentType;
	}
}
