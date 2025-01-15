using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020000B7 RID: 183
	internal class CsdlSemanticsRecordExpression : CsdlSemanticsExpression, IEdmRecordExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x0600031A RID: 794 RVA: 0x0000726D File Offset: 0x0000546D
		public CsdlSemanticsRecordExpression(CsdlRecordExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
			this.bindingContext = bindingContext;
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000729B File Offset: 0x0000549B
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600031C RID: 796 RVA: 0x000072A3 File Offset: 0x000054A3
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Record;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600031D RID: 797 RVA: 0x000072A7 File Offset: 0x000054A7
		public IEdmStructuredTypeReference DeclaredType
		{
			get
			{
				return this.declaredTypeCache.GetValue(this, CsdlSemanticsRecordExpression.ComputeDeclaredTypeFunc, null);
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600031E RID: 798 RVA: 0x000072BB File Offset: 0x000054BB
		public IEnumerable<IEdmPropertyConstructor> Properties
		{
			get
			{
				return this.propertiesCache.GetValue(this, CsdlSemanticsRecordExpression.ComputePropertiesFunc, null);
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600031F RID: 799 RVA: 0x000072CF File Offset: 0x000054CF
		public IEdmEntityType BindingContext
		{
			get
			{
				return this.bindingContext;
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x000072D8 File Offset: 0x000054D8
		private IEnumerable<IEdmPropertyConstructor> ComputeProperties()
		{
			List<IEdmPropertyConstructor> list = new List<IEdmPropertyConstructor>();
			foreach (CsdlPropertyValue csdlPropertyValue in this.expression.PropertyValues)
			{
				list.Add(new CsdlSemanticsPropertyConstructor(csdlPropertyValue, this));
			}
			return list;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00007338 File Offset: 0x00005538
		private IEdmStructuredTypeReference ComputeDeclaredType()
		{
			if (this.expression.Type == null)
			{
				return null;
			}
			return CsdlSemanticsModel.WrapTypeReference(base.Schema, this.expression.Type).AsStructured();
		}

		// Token: 0x0400014B RID: 331
		private readonly CsdlRecordExpression expression;

		// Token: 0x0400014C RID: 332
		private readonly IEdmEntityType bindingContext;

		// Token: 0x0400014D RID: 333
		private readonly Cache<CsdlSemanticsRecordExpression, IEdmStructuredTypeReference> declaredTypeCache = new Cache<CsdlSemanticsRecordExpression, IEdmStructuredTypeReference>();

		// Token: 0x0400014E RID: 334
		private static readonly Func<CsdlSemanticsRecordExpression, IEdmStructuredTypeReference> ComputeDeclaredTypeFunc = (CsdlSemanticsRecordExpression me) => me.ComputeDeclaredType();

		// Token: 0x0400014F RID: 335
		private readonly Cache<CsdlSemanticsRecordExpression, IEnumerable<IEdmPropertyConstructor>> propertiesCache = new Cache<CsdlSemanticsRecordExpression, IEnumerable<IEdmPropertyConstructor>>();

		// Token: 0x04000150 RID: 336
		private static readonly Func<CsdlSemanticsRecordExpression, IEnumerable<IEdmPropertyConstructor>> ComputePropertiesFunc = (CsdlSemanticsRecordExpression me) => me.ComputeProperties();
	}
}
