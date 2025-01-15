using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using DocumentFormat.OpenXml.Internal.SchemaValidation;
using DocumentFormat.OpenXml.Internal.SemanticValidation;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Validation
{
	// Token: 0x020030D5 RID: 12501
	internal abstract class DocumentValidator
	{
		// Token: 0x1700986B RID: 39019
		// (get) Token: 0x0601B278 RID: 111224 RVA: 0x0036E706 File Offset: 0x0036C906
		// (set) Token: 0x0601B279 RID: 111225 RVA: 0x0036E70E File Offset: 0x0036C90E
		internal SchemaValidator SchemaValidator { get; private set; }

		// Token: 0x1700986C RID: 39020
		// (get) Token: 0x0601B27A RID: 111226 RVA: 0x0036E717 File Offset: 0x0036C917
		// (set) Token: 0x0601B27B RID: 111227 RVA: 0x0036E71F File Offset: 0x0036C91F
		internal SemanticValidator SemanticValidator { get; private set; }

		// Token: 0x1700986D RID: 39021
		// (get) Token: 0x0601B27C RID: 111228 RVA: 0x0036E728 File Offset: 0x0036C928
		// (set) Token: 0x0601B27D RID: 111229 RVA: 0x0036E730 File Offset: 0x0036C930
		private protected ValidationSettings ValidationSettings { protected get; private set; }

		// Token: 0x0601B27E RID: 111230 RVA: 0x0036E739 File Offset: 0x0036C939
		internal DocumentValidator(ValidationSettings settings, SchemaValidator schemaValidator, SemanticValidator semanticValidator)
		{
			this.SchemaValidator = schemaValidator;
			this.SemanticValidator = semanticValidator;
			this.ValidationSettings = settings;
		}

		// Token: 0x0601B27F RID: 111231 RVA: 0x0036E758 File Offset: 0x0036C958
		internal ValidationResult Validate(OpenXmlPackage document)
		{
			this.TargetDocument = document;
			this.PrepareValidation();
			this.ValidationContext.Package = document;
			this.ValidatePackageStructure(document);
			foreach (OpenXmlPart openXmlPart in this.PartsToBeValidated)
			{
				this.ValidatePart(openXmlPart);
			}
			this.FinishValidation();
			return this.ValidationResult;
		}

		// Token: 0x0601B280 RID: 111232 RVA: 0x0036E7D4 File Offset: 0x0036C9D4
		internal ValidationResult Validate(OpenXmlPart part)
		{
			this.PrepareValidation();
			this.ValidatePart(part);
			return this.ValidationResult;
		}

		// Token: 0x0601B281 RID: 111233 RVA: 0x0036E7EC File Offset: 0x0036C9EC
		internal void ValidatePart(OpenXmlPart part)
		{
			if (!part.IsInVersion(this.ValidationSettings.FileFormat))
			{
				return;
			}
			try
			{
				bool isRootElementLoaded = part.IsRootElementLoaded;
				this.ValidationContext.Part = part;
				this.ValidationContext.Element = part.PartRootElement;
				int count = this.ValidationResult.Errors.Count;
				if (part.PartRootElement != null)
				{
					this.SchemaValidator.Validate(this.ValidationContext);
					this.ValidationContext.Element = part.PartRootElement;
					this.SemanticValidator.ClearConstraintState(SemanticValidationLevel.PartOnly);
					this.SemanticValidator.Validate(this.ValidationContext);
				}
				if (!isRootElementLoaded && this.ValidationResult.Errors.Count == count)
				{
					part.SetPartRootElementToNull();
				}
			}
			catch (XmlException ex)
			{
				ValidationErrorInfo validationErrorInfo = new ValidationErrorInfo();
				validationErrorInfo.ErrorType = ValidationErrorType.Schema;
				validationErrorInfo.Id = "ExceptionError";
				validationErrorInfo.Part = part;
				validationErrorInfo.Path = new XmlPath(part);
				validationErrorInfo.Description = string.Format(CultureInfo.CurrentUICulture, ValidationResources.ExceptionError, new object[] { ex.Message });
				this.ValidationResult.AddError(validationErrorInfo);
			}
		}

		// Token: 0x1700986E RID: 39022
		// (get) Token: 0x0601B282 RID: 111234 RVA: 0x0036E91C File Offset: 0x0036CB1C
		// (set) Token: 0x0601B283 RID: 111235 RVA: 0x0036E924 File Offset: 0x0036CB24
		protected ValidationContext ValidationContext { get; set; }

		// Token: 0x1700986F RID: 39023
		// (get) Token: 0x0601B284 RID: 111236 RVA: 0x0036E92D File Offset: 0x0036CB2D
		// (set) Token: 0x0601B285 RID: 111237 RVA: 0x0036E935 File Offset: 0x0036CB35
		protected ValidationResult ValidationResult { get; set; }

		// Token: 0x17009870 RID: 39024
		// (get) Token: 0x0601B286 RID: 111238
		// (set) Token: 0x0601B287 RID: 111239
		protected abstract OpenXmlPackage TargetDocument { get; set; }

		// Token: 0x17009871 RID: 39025
		// (get) Token: 0x0601B288 RID: 111240
		protected abstract IEnumerable<OpenXmlPart> PartsToBeValidated { get; }

		// Token: 0x0601B289 RID: 111241 RVA: 0x0036E93E File Offset: 0x0036CB3E
		protected virtual bool PrepareValidation()
		{
			this.InitValidationContext();
			this.SemanticValidator.ClearConstraintState(SemanticValidationLevel.PackageOnly);
			return true;
		}

		// Token: 0x0601B28A RID: 111242 RVA: 0x0036E954 File Offset: 0x0036CB54
		protected void InitValidationContext()
		{
			this.ValidationResult = new ValidationResult();
			this.ValidationResult.Valid = true;
			this.ValidationResult.MaxNumberOfErrors = this.ValidationSettings.MaxNumberOfErrors;
			this.ValidationResult.MaxNumberOfErrorsEventHandler += this.SchemaValidator.OnCancel;
			this.ValidationResult.MaxNumberOfErrorsEventHandler += this.SemanticValidator.OnCancel;
			this.ValidationContext = new ValidationContext();
			this.ValidationContext.FileFormat = this.ValidationSettings.FileFormat;
			this.ValidationContext.ValidationErrorEventHandler += this.ValidationResult.OnValidationError;
		}

		// Token: 0x0601B28B RID: 111243 RVA: 0x00002139 File Offset: 0x00000339
		protected virtual bool FinishValidation()
		{
			return true;
		}

		// Token: 0x0601B28C RID: 111244 RVA: 0x0036EA04 File Offset: 0x0036CC04
		private void ValidatePackageStructure(OpenXmlPackage document)
		{
			OpenXmlPackageValidationSettings openXmlPackageValidationSettings = new OpenXmlPackageValidationSettings();
			openXmlPackageValidationSettings.EventHandler += this.OnPackageValidationError;
			document.Validate(openXmlPackageValidationSettings, this.ValidationSettings.FileFormat);
		}

		// Token: 0x0601B28D RID: 111245 RVA: 0x0036EA3C File Offset: 0x0036CC3C
		private void OnPackageValidationError(object sender, OpenXmlPackageValidationEventArgs e)
		{
			ValidationErrorInfo validationErrorInfo = new ValidationErrorInfo();
			validationErrorInfo.ErrorType = ValidationErrorType.Package;
			validationErrorInfo.Id = "Pkg_" + e.MessageId;
			string id;
			switch (id = validationErrorInfo.Id)
			{
			case "Pkg_PartIsNotAllowed":
			{
				string text = ((e.Part != null) ? DocumentValidator.GetPartNameAndUri(e.Part) : DocumentValidator.GetDocumentName(this.TargetDocument));
				validationErrorInfo.Description = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Pkg_PartIsNotAllowed, new object[]
				{
					text,
					DocumentValidator.GetPartNameAndUri(e.SubPart)
				});
				break;
			}
			case "Pkg_RequiredPartDoNotExist":
				validationErrorInfo.Description = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Pkg_RequiredPartDoNotExist, new object[] { e.PartClassName });
				break;
			case "Pkg_OnlyOnePartAllowed":
			{
				string text = ((e.Part != null) ? DocumentValidator.GetPartNameAndUri(e.Part) : DocumentValidator.GetDocumentName(this.TargetDocument));
				validationErrorInfo.Description = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Pkg_OnlyOnePartAllowed, new object[] { text, e.PartClassName });
				break;
			}
			case "Pkg_ExtendedPartIsOpenXmlPart":
				validationErrorInfo.Description = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Pkg_ExtendedPartIsOpenXmlPart, new object[] { DocumentValidator.GetPartUri(e.SubPart) });
				break;
			case "Pkg_DataPartReferenceIsNotAllowed":
			{
				string text = ((e.Part != null) ? DocumentValidator.GetPartNameAndUri(e.Part) : DocumentValidator.GetDocumentName(this.TargetDocument));
				validationErrorInfo.Description = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Pkg_PartIsNotAllowed, new object[]
				{
					text,
					e.DataPartReferenceRelationship.Uri
				});
				break;
			}
			}
			if (e.Part != null)
			{
				validationErrorInfo.Part = e.Part;
				validationErrorInfo.Path = new XmlPath(e.Part);
			}
			validationErrorInfo.RelatedPart = e.SubPart;
			this.ValidationResult.AddError(validationErrorInfo);
		}

		// Token: 0x0601B28E RID: 111246 RVA: 0x0036ECAC File Offset: 0x0036CEAC
		private static string GetPartNameAndUri(OpenXmlPart part)
		{
			string name = part.GetType().Name;
			return string.Format(CultureInfo.CurrentUICulture, "{0}{1}{2}{3}", new object[] { name, '{', part.Uri, '}' });
		}

		// Token: 0x0601B28F RID: 111247 RVA: 0x0036ED00 File Offset: 0x0036CF00
		private static string GetPartUri(OpenXmlPart part)
		{
			return string.Format(CultureInfo.CurrentUICulture, "{0}{1}{2}", new object[] { '{', part.Uri, '}' });
		}

		// Token: 0x0601B290 RID: 111248 RVA: 0x0036ED41 File Offset: 0x0036CF41
		private static string GetDocumentName(OpenXmlPackage document)
		{
			return document.GetType().Name;
		}
	}
}
