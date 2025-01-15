using System;
using DocumentFormat.OpenXml.Internal.SchemaValidation;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Validation
{
	// Token: 0x02003149 RID: 12617
	internal class ValidationContext
	{
		// Token: 0x14000011 RID: 17
		// (add) Token: 0x0601B5A3 RID: 112035 RVA: 0x00376A0E File Offset: 0x00374C0E
		// (remove) Token: 0x0601B5A4 RID: 112036 RVA: 0x00376A27 File Offset: 0x00374C27
		internal event EventHandler<ValidationErrorEventArgs> ValidationErrorEventHandler;

		// Token: 0x0601B5A5 RID: 112037 RVA: 0x00376A40 File Offset: 0x00374C40
		internal ValidationContext()
		{
			this.McContext = new MCContext(false);
			this.FileFormat = FileFormatVersions.Office2007;
		}

		// Token: 0x170099A6 RID: 39334
		// (get) Token: 0x0601B5A6 RID: 112038 RVA: 0x00376A5B File Offset: 0x00374C5B
		// (set) Token: 0x0601B5A7 RID: 112039 RVA: 0x00376A63 File Offset: 0x00374C63
		internal FileFormatVersions FileFormat { get; set; }

		// Token: 0x170099A7 RID: 39335
		// (get) Token: 0x0601B5A8 RID: 112040 RVA: 0x00376A6C File Offset: 0x00374C6C
		// (set) Token: 0x0601B5A9 RID: 112041 RVA: 0x00376A74 File Offset: 0x00374C74
		internal OpenXmlPackage Package { get; set; }

		// Token: 0x170099A8 RID: 39336
		// (get) Token: 0x0601B5AA RID: 112042 RVA: 0x00376A7D File Offset: 0x00374C7D
		// (set) Token: 0x0601B5AB RID: 112043 RVA: 0x00376A85 File Offset: 0x00374C85
		internal OpenXmlPart Part { get; set; }

		// Token: 0x170099A9 RID: 39337
		// (get) Token: 0x0601B5AC RID: 112044 RVA: 0x00376A8E File Offset: 0x00374C8E
		// (set) Token: 0x0601B5AD RID: 112045 RVA: 0x00376A96 File Offset: 0x00374C96
		internal OpenXmlElement Element { get; set; }

		// Token: 0x170099AA RID: 39338
		// (get) Token: 0x0601B5AE RID: 112046 RVA: 0x00376A9F File Offset: 0x00374C9F
		// (set) Token: 0x0601B5AF RID: 112047 RVA: 0x00376AA7 File Offset: 0x00374CA7
		internal MCContext McContext { get; set; }

		// Token: 0x170099AB RID: 39339
		// (get) Token: 0x0601B5B0 RID: 112048 RVA: 0x00376AB0 File Offset: 0x00374CB0
		// (set) Token: 0x0601B5B1 RID: 112049 RVA: 0x00376AB8 File Offset: 0x00374CB8
		internal bool CollectExpectedChildren { get; set; }

		// Token: 0x0601B5B2 RID: 112050 RVA: 0x00376AC1 File Offset: 0x00374CC1
		internal void EmitError(ValidationErrorInfo error)
		{
			this.OnValidationError(new ValidationErrorEventArgs(error));
		}

		// Token: 0x0601B5B3 RID: 112051 RVA: 0x00376AD0 File Offset: 0x00374CD0
		protected virtual void OnValidationError(ValidationErrorEventArgs errorEventArgs)
		{
			EventHandler<ValidationErrorEventArgs> validationErrorEventHandler = this.ValidationErrorEventHandler;
			if (validationErrorEventHandler != null)
			{
				validationErrorEventHandler(this, errorEventArgs);
			}
		}

		// Token: 0x0601B5B4 RID: 112052 RVA: 0x00376AEF File Offset: 0x00374CEF
		internal OpenXmlElement GetFirstChildMc()
		{
			return this.Element.GetFirstChildMc(this.McContext, this.FileFormat);
		}

		// Token: 0x0601B5B5 RID: 112053 RVA: 0x00376B08 File Offset: 0x00374D08
		internal OpenXmlElement GetNextChildMc(OpenXmlElement child)
		{
			return this.Element.GetNextChildMc(child, this.McContext, this.FileFormat);
		}
	}
}
