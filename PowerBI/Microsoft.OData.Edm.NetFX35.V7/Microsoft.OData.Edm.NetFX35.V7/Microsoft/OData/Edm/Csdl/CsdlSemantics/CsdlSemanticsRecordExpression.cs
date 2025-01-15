using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200017D RID: 381
	internal class CsdlSemanticsRecordExpression : CsdlSemanticsExpression, IEdmRecordExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x06000A12 RID: 2578 RVA: 0x0001B406 File Offset: 0x00019606
		public CsdlSemanticsRecordExpression(CsdlRecordExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
			this.bindingContext = bindingContext;
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x0001B434 File Offset: 0x00019634
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x00013C4B File Offset: 0x00011E4B
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Record;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x0001B43C File Offset: 0x0001963C
		public IEdmStructuredTypeReference DeclaredType
		{
			get
			{
				return this.declaredTypeCache.GetValue(this, CsdlSemanticsRecordExpression.ComputeDeclaredTypeFunc, null);
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x0001B450 File Offset: 0x00019650
		public IEnumerable<IEdmPropertyConstructor> Properties
		{
			get
			{
				return this.propertiesCache.GetValue(this, CsdlSemanticsRecordExpression.ComputePropertiesFunc, null);
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x0001B464 File Offset: 0x00019664
		public IEdmEntityType BindingContext
		{
			get
			{
				return this.bindingContext;
			}
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0001B46C File Offset: 0x0001966C
		private IEnumerable<IEdmPropertyConstructor> ComputeProperties()
		{
			List<IEdmPropertyConstructor> list = new List<IEdmPropertyConstructor>();
			foreach (CsdlPropertyValue csdlPropertyValue in this.expression.PropertyValues)
			{
				list.Add(new CsdlSemanticsPropertyConstructor(csdlPropertyValue, this));
			}
			return list;
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0001B4CC File Offset: 0x000196CC
		private IEdmStructuredTypeReference ComputeDeclaredType()
		{
			if (this.expression.Type == null)
			{
				return null;
			}
			return CsdlSemanticsModel.WrapTypeReference(base.Schema, this.expression.Type).AsStructured();
		}

		// Token: 0x040005FB RID: 1531
		private readonly CsdlRecordExpression expression;

		// Token: 0x040005FC RID: 1532
		private readonly IEdmEntityType bindingContext;

		// Token: 0x040005FD RID: 1533
		private readonly Cache<CsdlSemanticsRecordExpression, IEdmStructuredTypeReference> declaredTypeCache = new Cache<CsdlSemanticsRecordExpression, IEdmStructuredTypeReference>();

		// Token: 0x040005FE RID: 1534
		private static readonly Func<CsdlSemanticsRecordExpression, IEdmStructuredTypeReference> ComputeDeclaredTypeFunc = (CsdlSemanticsRecordExpression me) => me.ComputeDeclaredType();

		// Token: 0x040005FF RID: 1535
		private readonly Cache<CsdlSemanticsRecordExpression, IEnumerable<IEdmPropertyConstructor>> propertiesCache = new Cache<CsdlSemanticsRecordExpression, IEnumerable<IEdmPropertyConstructor>>();

		// Token: 0x04000600 RID: 1536
		private static readonly Func<CsdlSemanticsRecordExpression, IEnumerable<IEdmPropertyConstructor>> ComputePropertiesFunc = (CsdlSemanticsRecordExpression me) => me.ComputeProperties();
	}
}
