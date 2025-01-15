using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.IdentityModel.Tokens.Configuration
{
	// Token: 0x02000197 RID: 407
	public class LKGConfigurationCacheOptions
	{
		// Token: 0x17000387 RID: 903
		// (get) Token: 0x0600124B RID: 4683 RVA: 0x0004450D File Offset: 0x0004270D
		// (set) Token: 0x0600124C RID: 4684 RVA: 0x00044515 File Offset: 0x00042715
		public IEqualityComparer<BaseConfiguration> BaseConfigurationComparer
		{
			get
			{
				return this._baseConfigurationComparer;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._baseConfigurationComparer = value;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x0600124D RID: 4685 RVA: 0x0004452D File Offset: 0x0004272D
		// (set) Token: 0x0600124E RID: 4686 RVA: 0x00044535 File Offset: 0x00042735
		public int LastKnownGoodConfigurationSizeLimit
		{
			get
			{
				return this._lastKnownGoodConfigurationSizeLimit;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._lastKnownGoodConfigurationSizeLimit = value;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x0600124F RID: 4687 RVA: 0x0004454D File Offset: 0x0004274D
		// (set) Token: 0x06001250 RID: 4688 RVA: 0x00044555 File Offset: 0x00042755
		public TaskCreationOptions TaskCreationOptions { get; set; }

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06001251 RID: 4689 RVA: 0x0004455E File Offset: 0x0004275E
		// (set) Token: 0x06001252 RID: 4690 RVA: 0x00044566 File Offset: 0x00042766
		public bool RemoveExpiredValues { get; set; } = true;

		// Token: 0x040006EC RID: 1772
		private IEqualityComparer<BaseConfiguration> _baseConfigurationComparer = new BaseConfigurationComparer();

		// Token: 0x040006ED RID: 1773
		private int _lastKnownGoodConfigurationSizeLimit = LKGConfigurationCacheOptions.DefaultLKGConfigurationSizeLimit;

		// Token: 0x040006EE RID: 1774
		public static readonly int DefaultLKGConfigurationSizeLimit = 10;
	}
}
