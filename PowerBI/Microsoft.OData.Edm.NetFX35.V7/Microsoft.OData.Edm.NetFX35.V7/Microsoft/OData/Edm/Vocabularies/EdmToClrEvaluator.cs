using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000EE RID: 238
	public class EdmToClrEvaluator : EdmExpressionEvaluator
	{
		// Token: 0x060006EC RID: 1772 RVA: 0x00013866 File Offset: 0x00011A66
		public EdmToClrEvaluator(IDictionary<IEdmOperation, Func<IEdmValue[], IEdmValue>> builtInFunctions)
			: base(builtInFunctions)
		{
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0001387A File Offset: 0x00011A7A
		public EdmToClrEvaluator(IDictionary<IEdmOperation, Func<IEdmValue[], IEdmValue>> builtInFunctions, Func<string, IEdmValue[], IEdmValue> lastChanceOperationApplier)
			: base(builtInFunctions, lastChanceOperationApplier)
		{
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x0001388F File Offset: 0x00011A8F
		// (set) Token: 0x060006EF RID: 1775 RVA: 0x00013897 File Offset: 0x00011A97
		public EdmToClrConverter EdmToClrConverter
		{
			get
			{
				return this.edmToClrConverter;
			}
			set
			{
				EdmUtil.CheckArgumentNull<EdmToClrConverter>(value, "value");
				this.edmToClrConverter = value;
			}
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x000138AC File Offset: 0x00011AAC
		public T EvaluateToClrValue<T>(IEdmExpression expression)
		{
			IEdmValue edmValue = base.Evaluate(expression);
			return this.edmToClrConverter.AsClrValue<T>(edmValue);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x000138D0 File Offset: 0x00011AD0
		public T EvaluateToClrValue<T>(IEdmExpression expression, IEdmStructuredValue context)
		{
			IEdmValue edmValue = base.Evaluate(expression, context);
			return this.edmToClrConverter.AsClrValue<T>(edmValue);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x000138F4 File Offset: 0x00011AF4
		public T EvaluateToClrValue<T>(IEdmExpression expression, IEdmStructuredValue context, IEdmTypeReference targetType)
		{
			IEdmValue edmValue = base.Evaluate(expression, context, targetType);
			return this.edmToClrConverter.AsClrValue<T>(edmValue);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00013918 File Offset: 0x00011B18
		internal IEdmType ResolveEdmTypeFromName(string edmTypeName, IEdmModel edmModel)
		{
			string text = null;
			if (this.edmToClrConverter.TryGetClrTypeNameDelegate(edmModel, edmTypeName, out text))
			{
				return EdmExpressionEvaluator.FindEdmType(text, edmModel);
			}
			return null;
		}

		// Token: 0x0400040E RID: 1038
		private EdmToClrConverter edmToClrConverter = new EdmToClrConverter();
	}
}
