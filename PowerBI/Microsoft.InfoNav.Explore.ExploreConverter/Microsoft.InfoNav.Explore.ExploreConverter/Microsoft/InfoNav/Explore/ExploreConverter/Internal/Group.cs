using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000068 RID: 104
	internal sealed class Group
	{
		// Token: 0x0600021C RID: 540 RVA: 0x0000B948 File Offset: 0x00009B48
		internal Group(List<GroupExpression> groupExpressions, string dataSetName)
		{
			Contract.Check(groupExpressions != null, "Expecting groupExpressions to not be null");
			this._groupExpressions = groupExpressions;
			this._dataSetName = dataSetName;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000B96C File Offset: 0x00009B6C
		internal List<GroupExpression> GroupExpressions
		{
			get
			{
				return this._groupExpressions;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000B974 File Offset: 0x00009B74
		internal string DataSetName
		{
			get
			{
				return this._dataSetName;
			}
		}

		// Token: 0x04000179 RID: 377
		private readonly List<GroupExpression> _groupExpressions;

		// Token: 0x0400017A RID: 378
		private readonly string _dataSetName;
	}
}
