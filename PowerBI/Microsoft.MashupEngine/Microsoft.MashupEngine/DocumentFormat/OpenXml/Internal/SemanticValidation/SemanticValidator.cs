using System;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SemanticValidation
{
	// Token: 0x020030FA RID: 12538
	internal class SemanticValidator : ICancelable
	{
		// Token: 0x1700988D RID: 39053
		// (get) Token: 0x0601B324 RID: 111396 RVA: 0x00371ABC File Offset: 0x0036FCBC
		// (set) Token: 0x0601B325 RID: 111397 RVA: 0x00371AC4 File Offset: 0x0036FCC4
		public FileFormatVersions FileFormat { get; private set; }

		// Token: 0x1700988E RID: 39054
		// (get) Token: 0x0601B326 RID: 111398 RVA: 0x00371ACD File Offset: 0x0036FCCD
		// (set) Token: 0x0601B327 RID: 111399 RVA: 0x00371AD5 File Offset: 0x0036FCD5
		public ApplicationType AppType { get; private set; }

		// Token: 0x0601B328 RID: 111400 RVA: 0x00371ADE File Offset: 0x0036FCDE
		public SemanticValidator(FileFormatVersions format, ApplicationType app)
		{
			this.FileFormat = format;
			this.AppType = app;
			this._curReg = new SemanticConstraintRegistry(format, app);
		}

		// Token: 0x0601B329 RID: 111401 RVA: 0x00371B01 File Offset: 0x0036FD01
		public void Validate(ValidationContext validationContext)
		{
			this._stopValidating = false;
			ValidationTraverser.ValidatingTraverse(validationContext, new ValidationTraverser.ValidationAction(this.ValidateElement), new ValidationTraverser.ValidationAction(this.OnContextValidationFinished), new ValidationTraverser.GetStopSignal(this.StopSignal));
		}

		// Token: 0x0601B32A RID: 111402 RVA: 0x00371B34 File Offset: 0x0036FD34
		public void ClearConstraintState(SemanticValidationLevel level)
		{
			this._curReg.ClearConstraintState(level);
		}

		// Token: 0x0601B32B RID: 111403 RVA: 0x00371B42 File Offset: 0x0036FD42
		private void OnContextValidationFinished(ValidationContext context)
		{
			this._curReg.ActCallBack(context.Element.ElementTypeId);
		}

		// Token: 0x0601B32C RID: 111404 RVA: 0x00371B5C File Offset: 0x0036FD5C
		private void ValidateElement(ValidationContext context)
		{
			if (this._curReg != null)
			{
				foreach (ValidationErrorInfo validationErrorInfo in this._curReg.CheckConstraints(context))
				{
					context.EmitError(validationErrorInfo);
				}
			}
		}

		// Token: 0x0601B32D RID: 111405 RVA: 0x00371BB8 File Offset: 0x0036FDB8
		private bool StopSignal(ValidationContext validationContext)
		{
			return this._stopValidating;
		}

		// Token: 0x0601B32E RID: 111406 RVA: 0x00371BC0 File Offset: 0x0036FDC0
		public void OnCancel(object sender, EventArgs eventArgs)
		{
			this._stopValidating = true;
		}

		// Token: 0x0400B464 RID: 46180
		private SemanticConstraintRegistry _curReg;

		// Token: 0x0400B465 RID: 46181
		private bool _stopValidating;
	}
}
