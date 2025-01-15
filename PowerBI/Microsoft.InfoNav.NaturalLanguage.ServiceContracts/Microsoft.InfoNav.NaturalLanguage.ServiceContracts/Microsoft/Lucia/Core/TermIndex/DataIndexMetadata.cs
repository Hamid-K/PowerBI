using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.InfoNav;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.TermIndex
{
	// Token: 0x02000157 RID: 343
	[ImmutableObject(true)]
	public sealed class DataIndexMetadata
	{
		// Token: 0x060006C2 RID: 1730 RVA: 0x0000B949 File Offset: 0x00009B49
		public DataIndexMetadata(IReadOnlyList<DataIndexElement> indexedElements, DataIndexBuilderSettings settings, DataIndexStatistics statistics, IReadOnlyDictionary<string, string> tags)
		{
			this.IndexedElements = indexedElements.EmptyIfNull<DataIndexElement>();
			this.Settings = settings;
			this.Statistics = statistics;
			this.Tags = tags;
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0000B973 File Offset: 0x00009B73
		[JsonConstructor]
		private DataIndexMetadata(List<DataIndexElement> indexedElements, DataIndexBuilderSettings settings, DataIndexStatistics statistics, Dictionary<string, string> tags)
			: this(indexedElements.AsReadOnlyList<DataIndexElement>(), settings, statistics, tags.AsReadOnlyDictionary<string, string>())
		{
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x0000B98A File Offset: 0x00009B8A
		public IReadOnlyList<DataIndexElement> IndexedElements { get; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0000B992 File Offset: 0x00009B92
		public DataIndexBuilderSettings Settings { get; }

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x0000B99A File Offset: 0x00009B9A
		public DataIndexStatistics Statistics { get; }

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0000B9A2 File Offset: 0x00009BA2
		public IReadOnlyDictionary<string, string> Tags { get; }

		// Token: 0x060006C8 RID: 1736 RVA: 0x0000B9AA File Offset: 0x00009BAA
		public bool IsSizeLimitReached()
		{
			return this.IndexedElements.Any((DataIndexElement e) => e.Status == DataIndexElementStatus.PartiallyIndexed || e.Status == DataIndexElementStatus.IndexLimitReached);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0000B9D6 File Offset: 0x00009BD6
		public bool IsStatisticsMissing()
		{
			return this.IndexedElements.Any((DataIndexElement e) => e.Status == DataIndexElementStatus.MissingStatistics);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0000BA02 File Offset: 0x00009C02
		public bool IsIndexingCancelled()
		{
			return this.IndexedElements.Any((DataIndexElement e) => e.Status == DataIndexElementStatus.Cancelled);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0000BA2E File Offset: 0x00009C2E
		public bool MissingAllStatistics()
		{
			if (this.IndexedElements.Count > 0)
			{
				return this.IndexedElements.All((DataIndexElement e) => e.Status == DataIndexElementStatus.MissingStatistics);
			}
			return false;
		}

		// Token: 0x04000687 RID: 1671
		public static readonly DataIndexMetadata Empty = new DataIndexMetadata(null, null, null, null);
	}
}
