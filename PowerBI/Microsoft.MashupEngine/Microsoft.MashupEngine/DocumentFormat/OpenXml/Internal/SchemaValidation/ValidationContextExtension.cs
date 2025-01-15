using System;
using System.Globalization;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003121 RID: 12577
	internal static class ValidationContextExtension
	{
		// Token: 0x0601B485 RID: 111749 RVA: 0x00375161 File Offset: 0x00373361
		internal static ValidationErrorInfo ComposeSchemaValidationError(this ValidationContext validationContext, OpenXmlElement element, OpenXmlElement child, string messageId, params string[] args)
		{
			return validationContext.ComposeValidationError(ValidationErrorType.Schema, element, child, messageId, args);
		}

		// Token: 0x0601B486 RID: 111750 RVA: 0x0037516F File Offset: 0x0037336F
		internal static ValidationErrorInfo ComposeMcValidationError(this ValidationContext validationContext, OpenXmlElement element, string messageId, params string[] args)
		{
			return validationContext.ComposeValidationError(ValidationErrorType.MarkupCompatibility, element, null, messageId, args);
		}

		// Token: 0x0601B487 RID: 111751 RVA: 0x0037517C File Offset: 0x0037337C
		internal static ValidationErrorInfo ComposeValidationError(this ValidationContext validationContext, ValidationErrorType errorType, OpenXmlElement element, OpenXmlElement child, string messageId, params string[] args)
		{
			return new ValidationErrorInfo
			{
				ErrorType = errorType,
				Part = validationContext.Part,
				Node = element,
				Id = messageId,
				RelatedNode = child,
				Description = string.Format(CultureInfo.CurrentUICulture, ValidationResources.ResourceManager.GetString(messageId), args)
			};
		}
	}
}
