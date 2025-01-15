using System;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Library.Annotations
{
	// Token: 0x020001B3 RID: 435
	public class EdmPropertyValueBinding : EdmElement, IEdmPropertyValueBinding, IEdmElement
	{
		// Token: 0x06000932 RID: 2354 RVA: 0x00019167 File Offset: 0x00017367
		public EdmPropertyValueBinding(IEdmProperty boundProperty, IEdmExpression value)
		{
			EdmUtil.CheckArgumentNull<IEdmProperty>(boundProperty, "boundProperty");
			EdmUtil.CheckArgumentNull<IEdmExpression>(value, "value");
			this.boundProperty = boundProperty;
			this.value = value;
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x00019195 File Offset: 0x00017395
		public IEdmProperty BoundProperty
		{
			get
			{
				return this.boundProperty;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x0001919D File Offset: 0x0001739D
		public IEdmExpression Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x04000488 RID: 1160
		private readonly IEdmProperty boundProperty;

		// Token: 0x04000489 RID: 1161
		private readonly IEdmExpression value;
	}
}
