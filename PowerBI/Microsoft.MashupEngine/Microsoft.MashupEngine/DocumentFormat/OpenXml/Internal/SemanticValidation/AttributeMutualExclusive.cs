using System;
using System.Globalization;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SemanticValidation
{
	// Token: 0x020030E9 RID: 12521
	internal class AttributeMutualExclusive : SemanticConstraint
	{
		// Token: 0x0601B2F6 RID: 111350 RVA: 0x00370616 File Offset: 0x0036E816
		public AttributeMutualExclusive(params byte[] attributes)
			: base(SemanticValidationLevel.Element)
		{
			this._attributes = attributes;
		}

		// Token: 0x0601B2F7 RID: 111351 RVA: 0x00370628 File Offset: 0x0036E828
		public override ValidationErrorInfo Validate(ValidationContext context)
		{
			string text = string.Empty;
			string text2 = string.Empty;
			string text3 = string.Empty;
			foreach (byte b in this._attributes)
			{
				text = text + "," + SemanticConstraint.GetAttributeQualifiedName(context.Element, b);
				if (context.Element.Attributes[(int)b] != null)
				{
					if (!string.IsNullOrEmpty(text3))
					{
						text2 = text2 + "," + text3;
					}
					text3 = SemanticConstraint.GetAttributeQualifiedName(context.Element, b).ToString();
				}
			}
			if (string.IsNullOrEmpty(text2))
			{
				return null;
			}
			return new ValidationErrorInfo
			{
				Id = "Sem_AttributeMutualExclusive",
				ErrorType = ValidationErrorType.Semantic,
				Node = context.Element,
				Description = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Sem_AttributeMutualExclusive, new object[]
				{
					text2.Substring(1),
					text3,
					text.Substring(1)
				})
			};
		}

		// Token: 0x0400B42D RID: 46125
		private byte[] _attributes;
	}
}
