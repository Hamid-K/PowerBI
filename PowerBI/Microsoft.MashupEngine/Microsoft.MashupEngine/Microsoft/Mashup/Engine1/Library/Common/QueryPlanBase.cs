using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200110E RID: 4366
	internal abstract class QueryPlanBase
	{
		// Token: 0x0600723C RID: 29244 RVA: 0x001893D8 File Offset: 0x001875D8
		protected QueryPlanBase(TypeValue expressionType)
		{
			this.expressionType = expressionType;
		}

		// Token: 0x17001FF5 RID: 8181
		// (get) Token: 0x0600723D RID: 29245 RVA: 0x001893E7 File Offset: 0x001875E7
		public TypeValue Type
		{
			get
			{
				return this.expressionType;
			}
		}

		// Token: 0x17001FF6 RID: 8182
		// (get) Token: 0x0600723E RID: 29246 RVA: 0x001893F0 File Offset: 0x001875F0
		public bool ReturnsScalar
		{
			get
			{
				ValueKind typeKind = this.expressionType.TypeKind;
				return typeKind != ValueKind.List && typeKind != ValueKind.Table;
			}
		}

		// Token: 0x04003F07 RID: 16135
		private TypeValue expressionType;
	}
}
