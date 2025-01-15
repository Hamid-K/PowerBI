using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Edm
{
	// Token: 0x02000024 RID: 36
	[ImmutableObject(true)]
	internal sealed class EdmDistributiveBy
	{
		// Token: 0x06000131 RID: 305 RVA: 0x00006929 File Offset: 0x00004B29
		internal EdmDistributiveBy(string aggregationKind, IReadOnlyList<string> entityRef)
		{
			this._aggregationKind = aggregationKind;
			this._entityRef = entityRef;
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000132 RID: 306 RVA: 0x0000693F File Offset: 0x00004B3F
		internal string AggregationKind
		{
			get
			{
				return this._aggregationKind;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00006947 File Offset: 0x00004B47
		internal IReadOnlyList<string> EntityRef
		{
			get
			{
				return this._entityRef;
			}
		}

		// Token: 0x0400015F RID: 351
		private readonly string _aggregationKind;

		// Token: 0x04000160 RID: 352
		private readonly IReadOnlyList<string> _entityRef;
	}
}
