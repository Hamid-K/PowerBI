using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200018C RID: 396
	internal class CsdlSemanticsRecordExpression : CsdlSemanticsExpression, IEdmRecordExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x06000ACE RID: 2766 RVA: 0x0001D50B File Offset: 0x0001B70B
		public CsdlSemanticsRecordExpression(CsdlRecordExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
			this.bindingContext = bindingContext;
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x0001D539 File Offset: 0x0001B739
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x0001212F File Offset: 0x0001032F
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Record;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x0001D541 File Offset: 0x0001B741
		public IEdmStructuredTypeReference DeclaredType
		{
			get
			{
				return this.declaredTypeCache.GetValue(this, CsdlSemanticsRecordExpression.ComputeDeclaredTypeFunc, null);
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x0001D555 File Offset: 0x0001B755
		public IEnumerable<IEdmPropertyConstructor> Properties
		{
			get
			{
				return this.propertiesCache.GetValue(this, CsdlSemanticsRecordExpression.ComputePropertiesFunc, null);
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x0001D569 File Offset: 0x0001B769
		public IEdmEntityType BindingContext
		{
			get
			{
				return this.bindingContext;
			}
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0001D574 File Offset: 0x0001B774
		private IEnumerable<IEdmPropertyConstructor> ComputeProperties()
		{
			List<IEdmPropertyConstructor> list = new List<IEdmPropertyConstructor>();
			foreach (CsdlPropertyValue csdlPropertyValue in this.expression.PropertyValues)
			{
				list.Add(new CsdlSemanticsPropertyConstructor(csdlPropertyValue, this));
			}
			return list;
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0001D5D4 File Offset: 0x0001B7D4
		private IEdmStructuredTypeReference ComputeDeclaredType()
		{
			if (this.expression.Type == null)
			{
				return null;
			}
			return CsdlSemanticsModel.WrapTypeReference(base.Schema, this.expression.Type).AsStructured();
		}

		// Token: 0x04000677 RID: 1655
		private readonly CsdlRecordExpression expression;

		// Token: 0x04000678 RID: 1656
		private readonly IEdmEntityType bindingContext;

		// Token: 0x04000679 RID: 1657
		private readonly Cache<CsdlSemanticsRecordExpression, IEdmStructuredTypeReference> declaredTypeCache = new Cache<CsdlSemanticsRecordExpression, IEdmStructuredTypeReference>();

		// Token: 0x0400067A RID: 1658
		private static readonly Func<CsdlSemanticsRecordExpression, IEdmStructuredTypeReference> ComputeDeclaredTypeFunc = (CsdlSemanticsRecordExpression me) => me.ComputeDeclaredType();

		// Token: 0x0400067B RID: 1659
		private readonly Cache<CsdlSemanticsRecordExpression, IEnumerable<IEdmPropertyConstructor>> propertiesCache = new Cache<CsdlSemanticsRecordExpression, IEnumerable<IEdmPropertyConstructor>>();

		// Token: 0x0400067C RID: 1660
		private static readonly Func<CsdlSemanticsRecordExpression, IEnumerable<IEdmPropertyConstructor>> ComputePropertiesFunc = (CsdlSemanticsRecordExpression me) => me.ComputeProperties();
	}
}
