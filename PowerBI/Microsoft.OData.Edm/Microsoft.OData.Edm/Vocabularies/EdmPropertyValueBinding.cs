using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000DB RID: 219
	public class EdmPropertyValueBinding : EdmElement, IEdmPropertyValueBinding, IEdmElement
	{
		// Token: 0x060006BA RID: 1722 RVA: 0x0000FF94 File Offset: 0x0000E194
		public EdmPropertyValueBinding(IEdmProperty boundProperty, IEdmExpression value)
		{
			EdmUtil.CheckArgumentNull<IEdmProperty>(boundProperty, "boundProperty");
			EdmUtil.CheckArgumentNull<IEdmExpression>(value, "value");
			this.boundProperty = boundProperty;
			this.value = value;
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x0000FFC2 File Offset: 0x0000E1C2
		public IEdmProperty BoundProperty
		{
			get
			{
				return this.boundProperty;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x0000FFCA File Offset: 0x0000E1CA
		public IEdmExpression Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x040002E5 RID: 741
		private readonly IEdmProperty boundProperty;

		// Token: 0x040002E6 RID: 742
		private readonly IEdmExpression value;
	}
}
