using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Diagnostics
{
	// Token: 0x0200004A RID: 74
	public interface IStatistics
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000252 RID: 594
		// (set) Token: 0x06000253 RID: 595
		bool EnableTimers { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000254 RID: 596
		IEnumerable<string> Properties { get; }

		// Token: 0x1700005B RID: 91
		double this[string propertyName] { get; }

		// Token: 0x06000256 RID: 598
		string ToString();

		// Token: 0x06000257 RID: 599
		void Reset();
	}
}
