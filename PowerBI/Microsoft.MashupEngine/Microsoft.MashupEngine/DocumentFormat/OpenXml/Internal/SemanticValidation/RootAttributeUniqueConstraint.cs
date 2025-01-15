using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SemanticValidation
{
	// Token: 0x020030F5 RID: 12533
	internal class RootAttributeUniqueConstraint : SemanticConstraint
	{
		// Token: 0x0601B318 RID: 111384 RVA: 0x00371778 File Offset: 0x0036F978
		public RootAttributeUniqueConstraint(byte attribute, bool caseSensitive)
			: base(SemanticValidationLevel.PackageOnly)
		{
			this._attribute = attribute;
			this._caseSensitive = caseSensitive;
		}

		// Token: 0x0601B319 RID: 111385 RVA: 0x0037179C File Offset: 0x0036F99C
		public override ValidationErrorInfo Validate(ValidationContext context)
		{
			OpenXmlSimpleType attributeValue = context.Element.Attributes[(int)this._attribute];
			if (attributeValue == null || string.IsNullOrEmpty(attributeValue.InnerText))
			{
				return null;
			}
			bool flag = false;
			if (this._values.Where((string v) => string.Compare(v, attributeValue.InnerText, !this._caseSensitive, CultureInfo.InvariantCulture) == 0).Count<string>() > 0)
			{
				flag = true;
			}
			else
			{
				this._values.Add(attributeValue.InnerText);
			}
			if (flag)
			{
				string text = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Sem_AttributeValueUniqueInDocument, new object[]
				{
					SemanticConstraint.GetAttributeQualifiedName(context.Element, this._attribute),
					attributeValue
				});
				return new ValidationErrorInfo
				{
					Id = "Sem_AttributeValueUniqueInDocument",
					ErrorType = ValidationErrorType.Semantic,
					Node = context.Element,
					Description = text
				};
			}
			return null;
		}

		// Token: 0x0601B31A RID: 111386 RVA: 0x0037188F File Offset: 0x0036FA8F
		public override void ClearState(ValidationContext context)
		{
			this._values.Clear();
		}

		// Token: 0x0400B457 RID: 46167
		private byte _attribute;

		// Token: 0x0400B458 RID: 46168
		private bool _caseSensitive;

		// Token: 0x0400B459 RID: 46169
		private List<string> _values = new List<string>();
	}
}
