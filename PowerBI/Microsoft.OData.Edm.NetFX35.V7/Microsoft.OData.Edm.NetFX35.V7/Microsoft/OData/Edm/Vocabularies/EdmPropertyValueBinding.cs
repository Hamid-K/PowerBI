using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000E1 RID: 225
	public class EdmPropertyValueBinding : EdmElement, IEdmPropertyValueBinding, IEdmElement
	{
		// Token: 0x0600068A RID: 1674 RVA: 0x00011D44 File Offset: 0x0000FF44
		public EdmPropertyValueBinding(IEdmProperty boundProperty, IEdmExpression value)
		{
			EdmUtil.CheckArgumentNull<IEdmProperty>(boundProperty, "boundProperty");
			EdmUtil.CheckArgumentNull<IEdmExpression>(value, "value");
			this.boundProperty = boundProperty;
			this.value = value;
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x00011D72 File Offset: 0x0000FF72
		public IEdmProperty BoundProperty
		{
			get
			{
				return this.boundProperty;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x00011D7A File Offset: 0x0000FF7A
		public IEdmExpression Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x040003F1 RID: 1009
		private readonly IEdmProperty boundProperty;

		// Token: 0x040003F2 RID: 1010
		private readonly IEdmExpression value;
	}
}
