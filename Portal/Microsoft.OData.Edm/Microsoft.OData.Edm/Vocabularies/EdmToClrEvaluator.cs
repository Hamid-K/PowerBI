using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000E7 RID: 231
	public class EdmToClrEvaluator : EdmExpressionEvaluator
	{
		// Token: 0x06000718 RID: 1816 RVA: 0x00011D1E File Offset: 0x0000FF1E
		public EdmToClrEvaluator(IDictionary<IEdmOperation, Func<IEdmValue[], IEdmValue>> builtInFunctions)
			: base(builtInFunctions)
		{
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00011D32 File Offset: 0x0000FF32
		public EdmToClrEvaluator(IDictionary<IEdmOperation, Func<IEdmValue[], IEdmValue>> builtInFunctions, Func<string, IEdmValue[], IEdmValue> lastChanceOperationApplier)
			: base(builtInFunctions, lastChanceOperationApplier)
		{
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00011D47 File Offset: 0x0000FF47
		public EdmToClrEvaluator(IDictionary<IEdmOperation, Func<IEdmValue[], IEdmValue>> builtInFunctions, Func<string, IEdmValue[], IEdmValue> lastChanceOperationApplier, Func<IEdmModel, IEdmType, string, string, IEdmExpression> getAnnotationExpressionForType, Func<IEdmModel, IEdmType, string, string, string, IEdmExpression> getAnnotationExpressionForProperty, IEdmModel edmModel)
			: base(builtInFunctions, lastChanceOperationApplier, getAnnotationExpressionForType, getAnnotationExpressionForProperty, edmModel)
		{
			base.ResolveTypeFromName = new Func<string, IEdmModel, IEdmType>(this.ResolveEdmTypeFromName);
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x00011D73 File Offset: 0x0000FF73
		// (set) Token: 0x0600071C RID: 1820 RVA: 0x00011D7B File Offset: 0x0000FF7B
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

		// Token: 0x0600071D RID: 1821 RVA: 0x00011D90 File Offset: 0x0000FF90
		public T EvaluateToClrValue<T>(IEdmExpression expression)
		{
			IEdmValue edmValue = base.Evaluate(expression);
			return this.edmToClrConverter.AsClrValue<T>(edmValue);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00011DB4 File Offset: 0x0000FFB4
		public T EvaluateToClrValue<T>(IEdmExpression expression, IEdmStructuredValue context)
		{
			IEdmValue edmValue = base.Evaluate(expression, context);
			return this.edmToClrConverter.AsClrValue<T>(edmValue);
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00011DD8 File Offset: 0x0000FFD8
		public T EvaluateToClrValue<T>(IEdmExpression expression, IEdmStructuredValue context, IEdmTypeReference targetType)
		{
			IEdmValue edmValue = base.Evaluate(expression, context, targetType);
			return this.edmToClrConverter.AsClrValue<T>(edmValue);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00011DFC File Offset: 0x0000FFFC
		internal IEdmType ResolveEdmTypeFromName(string edmTypeName, IEdmModel edmModel)
		{
			string text = null;
			if (this.edmToClrConverter.TryGetClrTypeNameDelegate(edmModel, edmTypeName, out text))
			{
				return EdmExpressionEvaluator.FindEdmType(text, edmModel);
			}
			return null;
		}

		// Token: 0x04000302 RID: 770
		private EdmToClrConverter edmToClrConverter = new EdmToClrConverter();
	}
}
