using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SemanticValidation
{
	// Token: 0x020030F7 RID: 12535
	internal class UniqueAttributeValueConstraint : SemanticConstraint
	{
		// Token: 0x0601B31D RID: 111389 RVA: 0x003718C5 File Offset: 0x0036FAC5
		public UniqueAttributeValueConstraint(byte attribute, bool caseSensitive, bool partLevel, SemanticConstraintRegistry reg)
			: base(SemanticValidationLevel.Part)
		{
			this._attribute = attribute;
			this._reg = reg;
			this._caseSensitive = caseSensitive;
			this._partLevel = partLevel;
		}

		// Token: 0x0601B31E RID: 111390 RVA: 0x00371904 File Offset: 0x0036FB04
		public override ValidationErrorInfo Validate(ValidationContext context)
		{
			if (this._values == null)
			{
				return null;
			}
			OpenXmlSimpleType attributeValue = context.Element.Attributes[(int)this._attribute];
			if (attributeValue == null || string.IsNullOrEmpty(attributeValue.InnerText))
			{
				return null;
			}
			if (this._values.Where((string v) => string.Compare(v, attributeValue.InnerText, !this._caseSensitive, CultureInfo.InvariantCulture) == 0).Count<string>() == 0)
			{
				this._values.Add(attributeValue.InnerText);
				return null;
			}
			return new ValidationErrorInfo
			{
				Id = "Sem_UniqueAttributeValue",
				ErrorType = ValidationErrorType.Semantic,
				Node = context.Element,
				Description = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Sem_UniqueAttributeValue, new object[]
				{
					SemanticConstraint.GetAttributeQualifiedName(context.Element, this._attribute),
					attributeValue.InnerText
				})
			};
		}

		// Token: 0x0601B31F RID: 111391 RVA: 0x003719F6 File Offset: 0x0036FBF6
		public void AdjustState()
		{
			if (this._stateStack.Count<List<string>>() != 0)
			{
				this._values = this._stateStack.Pop();
				return;
			}
			this._values = null;
		}

		// Token: 0x0601B320 RID: 111392 RVA: 0x00371A20 File Offset: 0x0036FC20
		public override void ClearState(ValidationContext context)
		{
			if (context == null)
			{
				this._stateStack.Clear();
				this._values = (this._partLevel ? new List<string>() : null);
				return;
			}
			if (this._values != null)
			{
				this._stateStack.Push(this._values);
			}
			this._reg.AddCallBackMethod(context.Element, new CallBackMethod(this.AdjustState));
			this._values = new List<string>();
		}

		// Token: 0x0400B45C RID: 46172
		private byte _attribute;

		// Token: 0x0400B45D RID: 46173
		private Stack<List<string>> _stateStack = new Stack<List<string>>();

		// Token: 0x0400B45E RID: 46174
		private List<string> _values = new List<string>();

		// Token: 0x0400B45F RID: 46175
		private SemanticConstraintRegistry _reg;

		// Token: 0x0400B460 RID: 46176
		private bool _caseSensitive;

		// Token: 0x0400B461 RID: 46177
		private bool _partLevel;
	}
}
