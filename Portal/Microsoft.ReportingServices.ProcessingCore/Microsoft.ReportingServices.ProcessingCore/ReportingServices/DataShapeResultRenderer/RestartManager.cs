using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.DataShapeResultRenderer
{
	// Token: 0x02000582 RID: 1410
	internal sealed class RestartManager
	{
		// Token: 0x0600516D RID: 20845 RVA: 0x001599FC File Offset: 0x00157BFC
		internal RestartFlag GetRestartFlag(ScopeID scopeId, IList<object> startPositions, IEqualityComparer comparer)
		{
			if (!this.m_allPreviousFlagsAreMerge)
			{
				return RestartFlag.Append;
			}
			RestartFlag restartFlag = this.ComputeRestartFlag(scopeId, startPositions, comparer);
			if (restartFlag == RestartFlag.Append)
			{
				this.m_allPreviousFlagsAreMerge = false;
			}
			return restartFlag;
		}

		// Token: 0x0600516E RID: 20846 RVA: 0x00159A1C File Offset: 0x00157C1C
		private RestartFlag ComputeRestartFlag(ScopeID scopeId, IList<object> startPositions, IEqualityComparer comparer)
		{
			RestartFlag restartFlag = RestartFlag.Merge;
			for (int i = 0; i < scopeId.ScopeValueCount; i++)
			{
				if (!comparer.Equals(scopeId.GetScopeValue(i).Value, startPositions[i]))
				{
					restartFlag = RestartFlag.Append;
					break;
				}
			}
			return restartFlag;
		}

		// Token: 0x04002912 RID: 10514
		private bool m_allPreviousFlagsAreMerge = true;
	}
}
