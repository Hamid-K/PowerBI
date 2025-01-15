using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.EdmToClrConversion;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Evaluation
{
	// Token: 0x020000E9 RID: 233
	public class EdmToClrEvaluator : EdmExpressionEvaluator
	{
		// Token: 0x060004AE RID: 1198 RVA: 0x0000C5C2 File Offset: 0x0000A7C2
		public EdmToClrEvaluator(IDictionary<IEdmOperation, Func<IEdmValue[], IEdmValue>> builtInFunctions)
			: base(builtInFunctions)
		{
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0000C5D6 File Offset: 0x0000A7D6
		public EdmToClrEvaluator(IDictionary<IEdmOperation, Func<IEdmValue[], IEdmValue>> builtInFunctions, Func<string, IEdmValue[], IEdmValue> lastChanceOperationApplier)
			: base(builtInFunctions, lastChanceOperationApplier)
		{
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0000C5EB File Offset: 0x0000A7EB
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x0000C5F3 File Offset: 0x0000A7F3
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

		// Token: 0x060004B2 RID: 1202 RVA: 0x0000C608 File Offset: 0x0000A808
		public T EvaluateToClrValue<T>(IEdmExpression expression)
		{
			IEdmValue edmValue = base.Evaluate(expression);
			return this.edmToClrConverter.AsClrValue<T>(edmValue);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0000C62C File Offset: 0x0000A82C
		public T EvaluateToClrValue<T>(IEdmExpression expression, IEdmStructuredValue context)
		{
			IEdmValue edmValue = base.Evaluate(expression, context);
			return this.edmToClrConverter.AsClrValue<T>(edmValue);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0000C650 File Offset: 0x0000A850
		public T EvaluateToClrValue<T>(IEdmExpression expression, IEdmStructuredValue context, IEdmTypeReference targetType)
		{
			IEdmValue edmValue = base.Evaluate(expression, context, targetType);
			return this.edmToClrConverter.AsClrValue<T>(edmValue);
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0000C674 File Offset: 0x0000A874
		internal IEdmType ResolveEdmTypeFromName(string edmTypeName, IEdmModel edmModel)
		{
			string text = null;
			if (this.edmToClrConverter.TryGetClrTypeNameDelegate(edmModel, edmTypeName, out text))
			{
				return EdmExpressionEvaluator.FindEdmType(text, edmModel);
			}
			return null;
		}

		// Token: 0x040001C8 RID: 456
		private EdmToClrConverter edmToClrConverter = new EdmToClrConverter();
	}
}
