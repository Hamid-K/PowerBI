using System;
using System.Globalization;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003120 RID: 12576
	internal class SchemaValidator : ICancelable
	{
		// Token: 0x0601B47F RID: 111743 RVA: 0x00375094 File Offset: 0x00373294
		internal SchemaValidator()
			: this(FileFormatVersions.Office2007)
		{
		}

		// Token: 0x0601B480 RID: 111744 RVA: 0x003750A0 File Offset: 0x003732A0
		internal SchemaValidator(FileFormatVersions fileFormat)
		{
			if (fileFormat == FileFormatVersions.Office2007)
			{
				this._sdbSchemaDatas = SdbSchemaDatas.GetOffice2007SchemaDatas();
			}
			else
			{
				if (fileFormat != FileFormatVersions.Office2010)
				{
					string text = string.Format(CultureInfo.CurrentUICulture, ExceptionMessages.FileFormatShouldBe2007Or2010, new object[] { fileFormat });
					throw new ArgumentOutOfRangeException("fileFormat", text);
				}
				this._sdbSchemaDatas = SdbSchemaDatas.GetOffice2010SchemaDatas();
			}
			this._schemaTypeValidator = new SchemaTypeValidator(this._sdbSchemaDatas);
		}

		// Token: 0x0601B481 RID: 111745 RVA: 0x00375113 File Offset: 0x00373313
		internal void Validate(ValidationContext validationContext)
		{
			this._stopValidating = false;
			OpenXmlElement element = validationContext.Element;
			ValidationTraverser.ValidatingTraverse(validationContext, new ValidationTraverser.ValidationAction(this.ValidateElement), null, new ValidationTraverser.GetStopSignal(this.StopSignal));
		}

		// Token: 0x0601B482 RID: 111746 RVA: 0x00375142 File Offset: 0x00373342
		private void ValidateElement(ValidationContext validationContext)
		{
			this._schemaTypeValidator.Validate(validationContext);
		}

		// Token: 0x0601B483 RID: 111747 RVA: 0x00375150 File Offset: 0x00373350
		private bool StopSignal(ValidationContext validationContext)
		{
			return this._stopValidating;
		}

		// Token: 0x0601B484 RID: 111748 RVA: 0x00375158 File Offset: 0x00373358
		public void OnCancel(object sender, EventArgs eventArgs)
		{
			this._stopValidating = true;
		}

		// Token: 0x0400B4CD RID: 46285
		private SdbSchemaDatas _sdbSchemaDatas;

		// Token: 0x0400B4CE RID: 46286
		private SchemaTypeValidator _schemaTypeValidator;

		// Token: 0x0400B4CF RID: 46287
		private bool _stopValidating;
	}
}
