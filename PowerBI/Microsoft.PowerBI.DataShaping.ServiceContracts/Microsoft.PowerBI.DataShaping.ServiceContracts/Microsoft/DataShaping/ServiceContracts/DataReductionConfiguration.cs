using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Microsoft.DataShaping.ServiceContracts
{
	// Token: 0x02000007 RID: 7
	[ImmutableObject(true)]
	public sealed class DataReductionConfiguration
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000022AC File Offset: 0x000004AC
		internal DataReductionConfiguration(int defaultDataVolume, IList<DataVolumeLevel> levels)
		{
			this._defaultDataVolume = defaultDataVolume;
			this._levels = levels.AsReadOnlyCollection<DataVolumeLevel>();
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000022C7 File Offset: 0x000004C7
		public static DataReductionConfiguration Default
		{
			get
			{
				return DataReductionConfiguration._default;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000022CE File Offset: 0x000004CE
		public static DataReductionConfiguration DefaultForLegacyLimits
		{
			get
			{
				return DataReductionConfiguration._defaultForLegacyLimits;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000022D5 File Offset: 0x000004D5
		public static DataReductionConfiguration DefaultForCompositeDataQuery
		{
			get
			{
				return DataReductionConfiguration._defaultForCompositeDataQuery;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000022DC File Offset: 0x000004DC
		internal int DefaultDataVolume
		{
			get
			{
				return this._defaultDataVolume;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000022E4 File Offset: 0x000004E4
		internal int MaxDataVolume
		{
			get
			{
				return this._levels.Count;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000022F1 File Offset: 0x000004F1
		internal DataVolumeLevel MaxLevel
		{
			get
			{
				return this.GetLevel(this.MaxDataVolume);
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022FF File Offset: 0x000004FF
		internal bool TryGetLevel(int dataVolume, out DataVolumeLevel level)
		{
			if (dataVolume > 0 && dataVolume <= this._levels.Count)
			{
				level = this.GetLevel(dataVolume);
				return true;
			}
			level = null;
			return false;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002322 File Offset: 0x00000522
		private DataVolumeLevel GetLevel(int dataVolume)
		{
			return this._levels[dataVolume - 1];
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002334 File Offset: 0x00000534
		private static DataReductionConfiguration CreateDefault()
		{
			DataVolumeLevel[] array = new DataVolumeLevel[]
			{
				new DataVolumeLevel(60, 300, 50, 6, 12, 5, 60, 6, 1800, 6, 5, 60),
				new DataVolumeLevel(300, 1500, 100, 30, 50, 6, 100, 30, 9000, 30, 5, 300),
				new DataVolumeLevel(1000, 5000, 500, 60, 100, 10, 100, 100, 30000, 60, 5, 1000),
				new DataVolumeLevel(3500, 17500, 800, 60, 233, 15, 200, 350, 105000, 60, 5, 3500),
				new DataVolumeLevel(10000, 50000, 1000, 60, 500, 20, 200, 1000, 300000, 60, 10, 3500),
				new DataVolumeLevel(30000, 150000, 1000, 60, 1000, 30, 500, 3000, 900000, 60, 20, 3500)
			};
			foreach (DataVolumeLevel dataVolumeLevel in array)
			{
			}
			return new DataReductionConfiguration(2, array);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002484 File Offset: 0x00000684
		private static DataReductionConfiguration CreateDefaultForLegacyLimits()
		{
			DataVolumeLevel[] array = new DataVolumeLevel[]
			{
				new DataVolumeLevel(18000, 18000, 300, 60, 300, 60, 100, 1800, 18000, 60, 10, 3500)
			};
			return new DataReductionConfiguration(1, array);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024D4 File Offset: 0x000006D4
		private static DataReductionConfiguration CreateDefaultForCompositeDataQuery()
		{
			DataVolumeLevel[] array = new DataVolumeLevel[]
			{
				new DataVolumeLevel(150000, 150000, 1000, 60, 500, 20, 200, 15000, 150000, 60, 20, 3500)
			};
			return new DataReductionConfiguration(1, array);
		}

		// Token: 0x0400004D RID: 77
		private static DataReductionConfiguration _default = DataReductionConfiguration.CreateDefault();

		// Token: 0x0400004E RID: 78
		private static DataReductionConfiguration _defaultForLegacyLimits = DataReductionConfiguration.CreateDefaultForLegacyLimits();

		// Token: 0x0400004F RID: 79
		private static DataReductionConfiguration _defaultForCompositeDataQuery = DataReductionConfiguration.CreateDefaultForCompositeDataQuery();

		// Token: 0x04000050 RID: 80
		private readonly int _defaultDataVolume;

		// Token: 0x04000051 RID: 81
		private readonly ReadOnlyCollection<DataVolumeLevel> _levels;
	}
}
