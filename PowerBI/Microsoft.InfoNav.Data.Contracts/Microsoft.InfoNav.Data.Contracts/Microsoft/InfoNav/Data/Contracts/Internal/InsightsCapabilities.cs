using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200016D RID: 365
	[DataContract(Name = "Insights", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public class InsightsCapabilities
	{
		// Token: 0x06000971 RID: 2417 RVA: 0x000134A9 File Offset: 0x000116A9
		public InsightsCapabilities()
		{
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x000134B4 File Offset: 0x000116B4
		public InsightsCapabilities(bool supportsDistributionFactors = false, bool supportsExplainChange = false, bool supportsExplore = false, bool supportsKeyDrivers = false, bool supportsRelatedInsights = false, bool supportsDecomposition = false, bool supportsExplainAnomalies = false, bool supportsCompatibleFields = false, bool supportsQuerySuggestions = false, bool supportsExplainVarianceToTarget = false, bool supportsGenerateSummaries = false)
		{
			this.SupportsDistributionFactors = supportsDistributionFactors;
			this.SupportsExplainChange = supportsExplainChange;
			this.SupportsExplore = supportsExplore;
			this.SupportsKeyDrivers = supportsKeyDrivers;
			this.SupportsRelatedInsights = supportsRelatedInsights;
			this.SupportsDecomposition = supportsDecomposition;
			this.SupportsExplainAnomalies = supportsExplainAnomalies;
			this.SupportsCompatibleFields = supportsCompatibleFields;
			this.SupportsQuerySuggestions = supportsQuerySuggestions;
			this.SupportsExplainVarianceToTarget = supportsExplainVarianceToTarget;
			this.SupportsGenerateSummaries = supportsGenerateSummaries;
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x0001351C File Offset: 0x0001171C
		// (set) Token: 0x06000974 RID: 2420 RVA: 0x00013524 File Offset: 0x00011724
		[DataMember(Name = "SupportsDistributionFactors", IsRequired = false, EmitDefaultValue = false, Order = 0)]
		public bool SupportsDistributionFactors { get; private set; }

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x0001352D File Offset: 0x0001172D
		// (set) Token: 0x06000976 RID: 2422 RVA: 0x00013535 File Offset: 0x00011735
		[DataMember(Name = "SupportsExplainChange", IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public bool SupportsExplainChange { get; private set; }

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x0001353E File Offset: 0x0001173E
		// (set) Token: 0x06000978 RID: 2424 RVA: 0x00013546 File Offset: 0x00011746
		[DataMember(Name = "SupportsExplore", IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public bool SupportsExplore { get; private set; }

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x0001354F File Offset: 0x0001174F
		// (set) Token: 0x0600097A RID: 2426 RVA: 0x00013557 File Offset: 0x00011757
		[DataMember(Name = "SupportsKeyDrivers", IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public bool SupportsKeyDrivers { get; private set; }

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x00013560 File Offset: 0x00011760
		// (set) Token: 0x0600097C RID: 2428 RVA: 0x00013568 File Offset: 0x00011768
		[DataMember(Name = "SupportsRelatedInsights", IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public bool SupportsRelatedInsights { get; private set; }

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x00013571 File Offset: 0x00011771
		// (set) Token: 0x0600097E RID: 2430 RVA: 0x00013579 File Offset: 0x00011779
		[DataMember(Name = "SupportsDecomposition", IsRequired = false, EmitDefaultValue = false, Order = 50)]
		public bool SupportsDecomposition { get; private set; }

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x00013582 File Offset: 0x00011782
		// (set) Token: 0x06000980 RID: 2432 RVA: 0x0001358A File Offset: 0x0001178A
		[DataMember(Name = "SupportsExplainAnomalies", IsRequired = false, EmitDefaultValue = false, Order = 60)]
		public bool SupportsExplainAnomalies { get; private set; }

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x00013593 File Offset: 0x00011793
		// (set) Token: 0x06000982 RID: 2434 RVA: 0x0001359B File Offset: 0x0001179B
		[DataMember(Name = "SupportsCompatibleFields", IsRequired = false, EmitDefaultValue = false, Order = 70)]
		public bool SupportsCompatibleFields { get; private set; }

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x000135A4 File Offset: 0x000117A4
		// (set) Token: 0x06000984 RID: 2436 RVA: 0x000135AC File Offset: 0x000117AC
		[DataMember(Name = "SupportsQuerySuggestions", IsRequired = false, EmitDefaultValue = false, Order = 80)]
		public bool SupportsQuerySuggestions { get; private set; }

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x000135B5 File Offset: 0x000117B5
		// (set) Token: 0x06000986 RID: 2438 RVA: 0x000135BD File Offset: 0x000117BD
		[DataMember(Name = "SupportsExplainVarianceToTarget", IsRequired = false, EmitDefaultValue = false, Order = 90)]
		public bool SupportsExplainVarianceToTarget { get; private set; }

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x000135C6 File Offset: 0x000117C6
		// (set) Token: 0x06000988 RID: 2440 RVA: 0x000135CE File Offset: 0x000117CE
		[DataMember(Name = "SupportsGenerateSummaries", IsRequired = false, EmitDefaultValue = false, Order = 100)]
		public bool SupportsGenerateSummaries { get; private set; }

		// Token: 0x04000543 RID: 1347
		public static readonly InsightsCapabilities SupportsAll = new InsightsCapabilities(true, true, true, true, true, true, true, true, true, true, true);

		// Token: 0x04000544 RID: 1348
		public static readonly InsightsCapabilities SupportsAnalysisVisuals = new InsightsCapabilities(false, false, false, true, false, true, true, false, false, true, true);

		// Token: 0x04000545 RID: 1349
		public static readonly InsightsCapabilities SupportsAllButMining = new InsightsCapabilities(false, false, false, true, false, true, true, true, true, true, true);
	}
}
