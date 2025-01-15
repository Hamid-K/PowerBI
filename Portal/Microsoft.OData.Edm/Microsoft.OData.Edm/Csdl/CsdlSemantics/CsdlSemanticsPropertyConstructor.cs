using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200018A RID: 394
	internal class CsdlSemanticsPropertyConstructor : CsdlSemanticsElement, IEdmPropertyConstructor, IEdmElement
	{
		// Token: 0x06000AC5 RID: 2757 RVA: 0x0001D474 File Offset: 0x0001B674
		public CsdlSemanticsPropertyConstructor(CsdlPropertyValue property, CsdlSemanticsRecordExpression context)
			: base(property)
		{
			this.property = property;
			this.context = context;
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x0001D496 File Offset: 0x0001B696
		public string Name
		{
			get
			{
				return this.property.Property;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0001D4A3 File Offset: 0x0001B6A3
		public IEdmExpression Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsPropertyConstructor.ComputeValueFunc, null);
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x0001D4B7 File Offset: 0x0001B6B7
		public override CsdlElement Element
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x0001D4BF File Offset: 0x0001B6BF
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.context.Model;
			}
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0001D4CC File Offset: 0x0001B6CC
		private IEdmExpression ComputeValue()
		{
			return CsdlSemanticsModel.WrapExpression(this.property.Expression, this.context.BindingContext, this.context.Schema);
		}

		// Token: 0x04000673 RID: 1651
		private readonly CsdlPropertyValue property;

		// Token: 0x04000674 RID: 1652
		private readonly CsdlSemanticsRecordExpression context;

		// Token: 0x04000675 RID: 1653
		private readonly Cache<CsdlSemanticsPropertyConstructor, IEdmExpression> valueCache = new Cache<CsdlSemanticsPropertyConstructor, IEdmExpression>();

		// Token: 0x04000676 RID: 1654
		private static readonly Func<CsdlSemanticsPropertyConstructor, IEdmExpression> ComputeValueFunc = (CsdlSemanticsPropertyConstructor me) => me.ComputeValue();
	}
}
