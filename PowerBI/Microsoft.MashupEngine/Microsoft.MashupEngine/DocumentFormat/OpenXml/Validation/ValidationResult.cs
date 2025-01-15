using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Validation
{
	// Token: 0x0200314D RID: 12621
	internal class ValidationResult
	{
		// Token: 0x0601B5CB RID: 112075 RVA: 0x00376BEF File Offset: 0x00374DEF
		internal ValidationResult()
		{
			this.Valid = true;
			this.Errors = new List<ValidationErrorInfo>();
		}

		// Token: 0x170099B5 RID: 39349
		// (get) Token: 0x0601B5CC RID: 112076 RVA: 0x00376C09 File Offset: 0x00374E09
		// (set) Token: 0x0601B5CD RID: 112077 RVA: 0x00376C11 File Offset: 0x00374E11
		internal bool Canceled { get; set; }

		// Token: 0x170099B6 RID: 39350
		// (get) Token: 0x0601B5CE RID: 112078 RVA: 0x00376C1A File Offset: 0x00374E1A
		// (set) Token: 0x0601B5CF RID: 112079 RVA: 0x00376C22 File Offset: 0x00374E22
		internal bool Valid { get; set; }

		// Token: 0x170099B7 RID: 39351
		// (get) Token: 0x0601B5D0 RID: 112080 RVA: 0x00376C2B File Offset: 0x00374E2B
		// (set) Token: 0x0601B5D1 RID: 112081 RVA: 0x00376C33 File Offset: 0x00374E33
		internal List<ValidationErrorInfo> Errors { get; private set; }

		// Token: 0x170099B8 RID: 39352
		// (get) Token: 0x0601B5D2 RID: 112082 RVA: 0x00376C3C File Offset: 0x00374E3C
		// (set) Token: 0x0601B5D3 RID: 112083 RVA: 0x00376C44 File Offset: 0x00374E44
		internal int MaxNumberOfErrors { get; set; }

		// Token: 0x0601B5D4 RID: 112084 RVA: 0x00376C4D File Offset: 0x00374E4D
		internal void Clear()
		{
			this.Valid = true;
			this.Canceled = false;
			this.Errors.Clear();
		}

		// Token: 0x0601B5D5 RID: 112085 RVA: 0x00376C68 File Offset: 0x00374E68
		internal void AddError(ValidationErrorInfo error)
		{
			this.Valid = false;
			this.Errors.Add(error);
		}

		// Token: 0x0601B5D6 RID: 112086 RVA: 0x00376C80 File Offset: 0x00374E80
		internal void OnValidationError(object sender, ValidationErrorEventArgs errorEventArgs)
		{
			if (errorEventArgs.ValidationErrorInfo != null)
			{
				if (this.MaxNumberOfErrors > 0 && this.Errors.Count >= this.MaxNumberOfErrors)
				{
					EventHandler<EventArgs> maxNumberOfErrorsEventHandler = this.MaxNumberOfErrorsEventHandler;
					if (maxNumberOfErrorsEventHandler != null)
					{
						maxNumberOfErrorsEventHandler(this, null);
						return;
					}
				}
				else
				{
					this.Valid = false;
					this.Errors.Add(errorEventArgs.ValidationErrorInfo);
				}
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x0601B5D7 RID: 112087 RVA: 0x00376CDC File Offset: 0x00374EDC
		// (remove) Token: 0x0601B5D8 RID: 112088 RVA: 0x00376CF5 File Offset: 0x00374EF5
		internal event EventHandler<EventArgs> MaxNumberOfErrorsEventHandler;
	}
}
