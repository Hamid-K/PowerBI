using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using DocumentFormat.OpenXml.Internal.SchemaValidation;
using DocumentFormat.OpenXml.Internal.SemanticValidation;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Validation
{
	// Token: 0x020030FD RID: 12541
	internal class OpenXmlValidator
	{
		// Token: 0x17009893 RID: 39059
		// (get) Token: 0x0601B33C RID: 111420 RVA: 0x00371DE1 File Offset: 0x0036FFE1
		private SchemaValidator SchemaValidator
		{
			get
			{
				if (this._schemaValidator == null)
				{
					this._schemaValidator = new SchemaValidator(this._settings.FileFormat);
				}
				return this._schemaValidator;
			}
		}

		// Token: 0x17009894 RID: 39060
		// (get) Token: 0x0601B33D RID: 111421 RVA: 0x00371E07 File Offset: 0x00370007
		private SemanticValidator DocSmenaticValidator
		{
			get
			{
				if (this._docSmenaticValidator == null)
				{
					this._docSmenaticValidator = new SemanticValidator(this._settings.FileFormat, ApplicationType.Word);
				}
				return this._docSmenaticValidator;
			}
		}

		// Token: 0x17009895 RID: 39061
		// (get) Token: 0x0601B33E RID: 111422 RVA: 0x00371E2E File Offset: 0x0037002E
		private SemanticValidator XlsSemanticValidator
		{
			get
			{
				if (this._xlsSemanticValidator == null)
				{
					this._xlsSemanticValidator = new SemanticValidator(this._settings.FileFormat, ApplicationType.Excel);
				}
				return this._xlsSemanticValidator;
			}
		}

		// Token: 0x17009896 RID: 39062
		// (get) Token: 0x0601B33F RID: 111423 RVA: 0x00371E55 File Offset: 0x00370055
		private SemanticValidator PptSemanticValidator
		{
			get
			{
				if (this._pptSemanticValidator == null)
				{
					this._pptSemanticValidator = new SemanticValidator(this._settings.FileFormat, ApplicationType.PowerPoint);
				}
				return this._pptSemanticValidator;
			}
		}

		// Token: 0x17009897 RID: 39063
		// (get) Token: 0x0601B340 RID: 111424 RVA: 0x00371E7C File Offset: 0x0037007C
		private SemanticValidator FullSemanticValidator
		{
			get
			{
				if (this._fullSemanticValidator == null)
				{
					this._fullSemanticValidator = new SemanticValidator(this._settings.FileFormat, ApplicationType.All);
				}
				return this._fullSemanticValidator;
			}
		}

		// Token: 0x17009898 RID: 39064
		// (get) Token: 0x0601B341 RID: 111425 RVA: 0x00371EA3 File Offset: 0x003700A3
		private SpreadsheetDocumentValidator SpreadsheetDocumentValidator
		{
			get
			{
				if (this._spreadsheetDocumentValidator == null)
				{
					this._spreadsheetDocumentValidator = new SpreadsheetDocumentValidator(this._settings, this.SchemaValidator, this.XlsSemanticValidator);
				}
				return this._spreadsheetDocumentValidator;
			}
		}

		// Token: 0x17009899 RID: 39065
		// (get) Token: 0x0601B342 RID: 111426 RVA: 0x00371ED0 File Offset: 0x003700D0
		private WordprocessingDocumentValidator WordprocessingDocumentValidator
		{
			get
			{
				if (this._wordprocessingDocumentValidator == null)
				{
					this._wordprocessingDocumentValidator = new WordprocessingDocumentValidator(this._settings, this.SchemaValidator, this.DocSmenaticValidator);
				}
				return this._wordprocessingDocumentValidator;
			}
		}

		// Token: 0x1700989A RID: 39066
		// (get) Token: 0x0601B343 RID: 111427 RVA: 0x00371EFD File Offset: 0x003700FD
		private PresentationDocumentValidator PresentationDocumentValidator
		{
			get
			{
				if (this._presentationDocumentValidator == null)
				{
					this._presentationDocumentValidator = new PresentationDocumentValidator(this._settings, this.SchemaValidator, this.PptSemanticValidator);
				}
				return this._presentationDocumentValidator;
			}
		}

		// Token: 0x0601B344 RID: 111428 RVA: 0x00371F2A File Offset: 0x0037012A
		public OpenXmlValidator()
			: this(FileFormatVersions.Office2007)
		{
		}

		// Token: 0x0601B345 RID: 111429 RVA: 0x00371F33 File Offset: 0x00370133
		public OpenXmlValidator(FileFormatVersions fileFormat)
		{
			fileFormat.ThrowExceptionIfNot2007Or2010("fileFormat");
			this._settings = new ValidationSettings(fileFormat);
		}

		// Token: 0x1700989B RID: 39067
		// (get) Token: 0x0601B346 RID: 111430 RVA: 0x00371F52 File Offset: 0x00370152
		public FileFormatVersions FileFormat
		{
			get
			{
				return this._settings.FileFormat;
			}
		}

		// Token: 0x1700989C RID: 39068
		// (get) Token: 0x0601B347 RID: 111431 RVA: 0x00371F5F File Offset: 0x0037015F
		// (set) Token: 0x0601B348 RID: 111432 RVA: 0x00371F6C File Offset: 0x0037016C
		public int MaxNumberOfErrors
		{
			get
			{
				return this._settings.MaxNumberOfErrors;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._settings.MaxNumberOfErrors = value;
			}
		}

		// Token: 0x0601B349 RID: 111433 RVA: 0x00371F8C File Offset: 0x0037018C
		public IEnumerable<ValidationErrorInfo> Validate(OpenXmlPackage openXmlPackage)
		{
			if (openXmlPackage == null)
			{
				throw new ArgumentNullException("openXmlPackage");
			}
			if (openXmlPackage.OpenSettings.MarkupCompatibilityProcessSettings.ProcessMode != MarkupCompatibilityProcessMode.NoProcess && openXmlPackage.OpenSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions != this.FileFormat)
			{
				string text = string.Format(CultureInfo.CurrentUICulture, ExceptionMessages.DocumentFileFormatVersionMismatch, new object[]
				{
					openXmlPackage.OpenSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions,
					this.FileFormat
				});
				throw new InvalidOperationException(text);
			}
			ValidationResult validationResult;
			switch (DocumentTypeDetector.GetDocumentType(openXmlPackage))
			{
			case OpenXmlDocumentType.Wordprocessing:
				validationResult = this.WordprocessingDocumentValidator.Validate(openXmlPackage as WordprocessingDocument);
				break;
			case OpenXmlDocumentType.Spreadsheet:
				validationResult = this.SpreadsheetDocumentValidator.Validate(openXmlPackage as SpreadsheetDocument);
				break;
			case OpenXmlDocumentType.Presentation:
				validationResult = this.PresentationDocumentValidator.Validate(openXmlPackage as PresentationDocument);
				break;
			default:
				throw new InvalidDataException(ExceptionMessages.UnknownPackage);
			}
			return this.YieldResult(validationResult);
		}

		// Token: 0x0601B34A RID: 111434 RVA: 0x00372084 File Offset: 0x00370284
		public IEnumerable<ValidationErrorInfo> Validate(OpenXmlPart openXmlPart)
		{
			if (openXmlPart == null)
			{
				throw new ArgumentNullException("openXmlPart");
			}
			OpenXmlPackage openXmlPackage = openXmlPart.OpenXmlPackage;
			if (openXmlPackage.OpenSettings.MarkupCompatibilityProcessSettings.ProcessMode != MarkupCompatibilityProcessMode.NoProcess && openXmlPackage.OpenSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions != this.FileFormat)
			{
				string text = string.Format(CultureInfo.CurrentUICulture, ExceptionMessages.DocumentFileFormatVersionMismatch, new object[]
				{
					openXmlPackage.OpenSettings.MarkupCompatibilityProcessSettings.TargetFileFormatVersions,
					this.FileFormat
				});
				throw new InvalidOperationException(text);
			}
			if (openXmlPart.IsInVersion(this.FileFormat))
			{
				OpenXmlPackage openXmlPackage2 = openXmlPart.OpenXmlPackage;
				ValidationResult validationResult;
				switch (DocumentTypeDetector.GetDocumentType(openXmlPackage2))
				{
				case OpenXmlDocumentType.Wordprocessing:
					validationResult = this.WordprocessingDocumentValidator.Validate(openXmlPart);
					break;
				case OpenXmlDocumentType.Spreadsheet:
					validationResult = this.SpreadsheetDocumentValidator.Validate(openXmlPart);
					break;
				case OpenXmlDocumentType.Presentation:
					validationResult = this.PresentationDocumentValidator.Validate(openXmlPart);
					break;
				default:
					throw new InvalidDataException(ExceptionMessages.UnknownPackage);
				}
				return this.YieldResult(validationResult);
			}
			if (openXmlPart is ExtendedPart)
			{
				throw new InvalidOperationException(ExceptionMessages.PartNotInVersion);
			}
			throw new InvalidOperationException(ExceptionMessages.PartIsNotInOffice2007);
		}

		// Token: 0x0601B34B RID: 111435 RVA: 0x003721AC File Offset: 0x003703AC
		public IEnumerable<ValidationErrorInfo> Validate(OpenXmlElement openXmlElement)
		{
			if (openXmlElement == null)
			{
				throw new ArgumentNullException("openXmlElement");
			}
			if (openXmlElement is OpenXmlUnknownElement)
			{
				throw new ArgumentOutOfRangeException("openXmlElement", ExceptionMessages.CannotValidateUnknownElement);
			}
			if (openXmlElement is OpenXmlMiscNode)
			{
				throw new ArgumentOutOfRangeException("openXmlElement", ExceptionMessages.CannotValidateMiscNode);
			}
			if (openXmlElement is AlternateContent || openXmlElement is AlternateContentChoice || openXmlElement is AlternateContentFallback)
			{
				throw new ArgumentOutOfRangeException("openXmlElement", ExceptionMessages.CannotValidateAcbElement);
			}
			if (!openXmlElement.IsInVersion(this.FileFormat))
			{
				switch (this.FileFormat)
				{
				case FileFormatVersions.Office2007:
					throw new InvalidOperationException(ExceptionMessages.ElementIsNotInOffice2007);
				case FileFormatVersions.Office2010:
					throw new InvalidOperationException(ExceptionMessages.ElementIsNotInOffice2010);
				}
			}
			ValidationResult validationResult = new ValidationResult();
			validationResult.Valid = true;
			validationResult.MaxNumberOfErrors = this._settings.MaxNumberOfErrors;
			validationResult.MaxNumberOfErrorsEventHandler += this.SchemaValidator.OnCancel;
			ValidationContext validationContext = new ValidationContext();
			validationContext.FileFormat = this.FileFormat;
			validationContext.ValidationErrorEventHandler += validationResult.OnValidationError;
			validationContext.Element = openXmlElement;
			this.SchemaValidator.Validate(validationContext);
			validationContext.Element = openXmlElement;
			this.FullSemanticValidator.Validate(validationContext);
			return this.YieldResult(validationResult);
		}

		// Token: 0x0601B34C RID: 111436 RVA: 0x003722E4 File Offset: 0x003704E4
		private IEnumerable<ValidationErrorInfo> YieldResult(ValidationResult validationResult)
		{
			if (validationResult != null && !validationResult.Valid)
			{
				foreach (ValidationErrorInfo error in validationResult.Errors)
				{
					yield return error;
				}
			}
			yield break;
		}

		// Token: 0x0400B471 RID: 46193
		private ValidationSettings _settings;

		// Token: 0x0400B472 RID: 46194
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private SchemaValidator _schemaValidator;

		// Token: 0x0400B473 RID: 46195
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private SemanticValidator _docSmenaticValidator;

		// Token: 0x0400B474 RID: 46196
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private SemanticValidator _xlsSemanticValidator;

		// Token: 0x0400B475 RID: 46197
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private SemanticValidator _pptSemanticValidator;

		// Token: 0x0400B476 RID: 46198
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private SemanticValidator _fullSemanticValidator;

		// Token: 0x0400B477 RID: 46199
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private SpreadsheetDocumentValidator _spreadsheetDocumentValidator;

		// Token: 0x0400B478 RID: 46200
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private WordprocessingDocumentValidator _wordprocessingDocumentValidator;

		// Token: 0x0400B479 RID: 46201
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private PresentationDocumentValidator _presentationDocumentValidator;
	}
}
