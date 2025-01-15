using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200017B RID: 379
	internal class CsdlSemanticsPropertyConstructor : CsdlSemanticsElement, IEdmPropertyConstructor, IEdmElement
	{
		// Token: 0x06000A09 RID: 2569 RVA: 0x0001B36F File Offset: 0x0001956F
		public CsdlSemanticsPropertyConstructor(CsdlPropertyValue property, CsdlSemanticsRecordExpression context)
			: base(property)
		{
			this.property = property;
			this.context = context;
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x0001B391 File Offset: 0x00019591
		public string Name
		{
			get
			{
				return this.property.Property;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0001B39E File Offset: 0x0001959E
		public IEdmExpression Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsPropertyConstructor.ComputeValueFunc, null);
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x0001B3B2 File Offset: 0x000195B2
		public override CsdlElement Element
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x0001B3BA File Offset: 0x000195BA
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.context.Model;
			}
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0001B3C7 File Offset: 0x000195C7
		private IEdmExpression ComputeValue()
		{
			return CsdlSemanticsModel.WrapExpression(this.property.Expression, this.context.BindingContext, this.context.Schema);
		}

		// Token: 0x040005F7 RID: 1527
		private readonly CsdlPropertyValue property;

		// Token: 0x040005F8 RID: 1528
		private readonly CsdlSemanticsRecordExpression context;

		// Token: 0x040005F9 RID: 1529
		private readonly Cache<CsdlSemanticsPropertyConstructor, IEdmExpression> valueCache = new Cache<CsdlSemanticsPropertyConstructor, IEdmExpression>();

		// Token: 0x040005FA RID: 1530
		private static readonly Func<CsdlSemanticsPropertyConstructor, IEdmExpression> ComputeValueFunc = (CsdlSemanticsPropertyConstructor me) => me.ComputeValue();
	}
}
