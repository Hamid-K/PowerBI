using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200008B RID: 139
	public sealed class FeatureSwitchProvider : IFeatureSwitchProvider
	{
		// Token: 0x06000275 RID: 629 RVA: 0x00005D19 File Offset: 0x00003F19
		private FeatureSwitchProvider(ImmutableHashSet<FeatureSwitch> featureSwitches)
		{
			this._featureSwitches = featureSwitches;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00005D28 File Offset: 0x00003F28
		public static FeatureSwitchProvider Create(IEnumerable<FeatureSwitch> featureSwitches)
		{
			ImmutableHashSet<FeatureSwitch> immutableHashSet = ((featureSwitches != null) ? featureSwitches.ToImmutableHashSet<FeatureSwitch>() : null);
			if (immutableHashSet.IsNullOrEmpty<FeatureSwitch>())
			{
				return FeatureSwitchProvider.Empty;
			}
			return new FeatureSwitchProvider(immutableHashSet);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00005D56 File Offset: 0x00003F56
		public IList<FeatureSwitch> GetFeatureSwitches()
		{
			return this._featureSwitches.AsList<FeatureSwitch>();
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00005D63 File Offset: 0x00003F63
		public bool IsFeatureSwitchEnabled(FeatureSwitch featureSwitch)
		{
			return this._featureSwitches.Contains(featureSwitch);
		}

		// Token: 0x040002EF RID: 751
		public static readonly FeatureSwitchProvider Empty = new FeatureSwitchProvider(ImmutableHashSet<FeatureSwitch>.Empty);

		// Token: 0x040002F0 RID: 752
		private readonly ImmutableHashSet<FeatureSwitch> _featureSwitches;
	}
}
