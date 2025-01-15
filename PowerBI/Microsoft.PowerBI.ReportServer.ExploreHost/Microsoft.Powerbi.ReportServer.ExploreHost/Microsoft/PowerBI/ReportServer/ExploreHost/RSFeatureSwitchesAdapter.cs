using System;
using System.Collections.Generic;
using Microsoft.BusinessIntelligence;
using Microsoft.InfoNav.Explore.ServiceContracts.Internal;

namespace Microsoft.PowerBI.ReportServer.ExploreHost
{
	// Token: 0x02000019 RID: 25
	public class RSFeatureSwitchesAdapter : Microsoft.InfoNav.Explore.ServiceContracts.Internal.IFeatureSwitchesProxy, Microsoft.BusinessIntelligence.IFeatureSwitchesProxy
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00003353 File Offset: 0x00001553
		public RSFeatureSwitchesAdapter(IReadOnlyDictionary<string, bool> rsFeatureSwitches)
		{
			this._rsFeatureSwitches = rsFeatureSwitches;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003362 File Offset: 0x00001562
		public bool GetSwitchValue(string featureSwitchName)
		{
			return this._rsFeatureSwitches.ContainsKey(featureSwitchName) && this._rsFeatureSwitches[featureSwitchName];
		}

		// Token: 0x04000058 RID: 88
		private readonly IReadOnlyDictionary<string, bool> _rsFeatureSwitches;
	}
}
