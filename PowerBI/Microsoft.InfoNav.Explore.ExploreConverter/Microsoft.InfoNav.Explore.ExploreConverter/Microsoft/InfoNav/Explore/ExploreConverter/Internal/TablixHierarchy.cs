using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x020000A5 RID: 165
	internal sealed class TablixHierarchy
	{
		// Token: 0x06000335 RID: 821 RVA: 0x0000D289 File Offset: 0x0000B489
		internal TablixHierarchy(List<TablixMember> members, bool isSubtotals, bool enableDrilling)
		{
			this._members = members;
			this._isSubtotals = isSubtotals;
			this._enableDrilling = enableDrilling;
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000D2A6 File Offset: 0x0000B4A6
		public List<TablixMember> Members
		{
			get
			{
				return this._members;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000D2AE File Offset: 0x0000B4AE
		public bool IsSubtotals
		{
			get
			{
				return this._isSubtotals;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000D2B6 File Offset: 0x0000B4B6
		public bool EnableDrilling
		{
			get
			{
				return this._enableDrilling;
			}
		}

		// Token: 0x04000221 RID: 545
		private readonly List<TablixMember> _members;

		// Token: 0x04000222 RID: 546
		private readonly bool _isSubtotals;

		// Token: 0x04000223 RID: 547
		private readonly bool _enableDrilling;
	}
}
