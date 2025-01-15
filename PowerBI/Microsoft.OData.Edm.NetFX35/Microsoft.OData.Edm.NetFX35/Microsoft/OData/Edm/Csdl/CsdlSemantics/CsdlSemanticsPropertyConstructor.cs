using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020000B2 RID: 178
	internal class CsdlSemanticsPropertyConstructor : CsdlSemanticsElement, IEdmPropertyConstructor, IEdmElement
	{
		// Token: 0x06000301 RID: 769 RVA: 0x0000706F File Offset: 0x0000526F
		public CsdlSemanticsPropertyConstructor(CsdlPropertyValue property, CsdlSemanticsRecordExpression context)
			: base(property)
		{
			this.property = property;
			this.context = context;
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00007091 File Offset: 0x00005291
		public string Name
		{
			get
			{
				return this.property.Property;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000709E File Offset: 0x0000529E
		public IEdmExpression Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsPropertyConstructor.ComputeValueFunc, null);
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000304 RID: 772 RVA: 0x000070B2 File Offset: 0x000052B2
		public override CsdlElement Element
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000305 RID: 773 RVA: 0x000070BA File Offset: 0x000052BA
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.context.Model;
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x000070C7 File Offset: 0x000052C7
		private IEdmExpression ComputeValue()
		{
			return CsdlSemanticsModel.WrapExpression(this.property.Expression, this.context.BindingContext, this.context.Schema);
		}

		// Token: 0x0400013E RID: 318
		private readonly CsdlPropertyValue property;

		// Token: 0x0400013F RID: 319
		private readonly CsdlSemanticsRecordExpression context;

		// Token: 0x04000140 RID: 320
		private readonly Cache<CsdlSemanticsPropertyConstructor, IEdmExpression> valueCache = new Cache<CsdlSemanticsPropertyConstructor, IEdmExpression>();

		// Token: 0x04000141 RID: 321
		private static readonly Func<CsdlSemanticsPropertyConstructor, IEdmExpression> ComputeValueFunc = (CsdlSemanticsPropertyConstructor me) => me.ComputeValue();
	}
}
