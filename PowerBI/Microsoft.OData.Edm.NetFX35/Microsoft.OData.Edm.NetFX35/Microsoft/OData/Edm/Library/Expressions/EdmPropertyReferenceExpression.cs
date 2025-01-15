using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Library.Expressions
{
	// Token: 0x020001CC RID: 460
	public class EdmPropertyReferenceExpression : EdmElement, IEdmPropertyReferenceExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x060009A4 RID: 2468 RVA: 0x0001980A File Offset: 0x00017A0A
		public EdmPropertyReferenceExpression(IEdmExpression baseExpression, IEdmProperty referencedProperty)
		{
			EdmUtil.CheckArgumentNull<IEdmExpression>(baseExpression, "baseExpression");
			EdmUtil.CheckArgumentNull<IEdmProperty>(referencedProperty, "referencedPropert");
			this.baseExpression = baseExpression;
			this.referencedProperty = referencedProperty;
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x00019838 File Offset: 0x00017A38
		public IEdmExpression Base
		{
			get
			{
				return this.baseExpression;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x00019840 File Offset: 0x00017A40
		public IEdmProperty ReferencedProperty
		{
			get
			{
				return this.referencedProperty;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x00019848 File Offset: 0x00017A48
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.PropertyReference;
			}
		}

		// Token: 0x040004B3 RID: 1203
		private readonly IEdmExpression baseExpression;

		// Token: 0x040004B4 RID: 1204
		private readonly IEdmProperty referencedProperty;
	}
}
