using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.Lucia.Core.Packaging;

namespace Microsoft.Lucia.Core.TermIndex
{
	// Token: 0x02000151 RID: 337
	public abstract class DataIndex : TermIndex, IDataInstanceLookup, IEntityTermLookup<DataInstanceInfo>, IDisposable
	{
		// Token: 0x060006AB RID: 1707 RVA: 0x0000B7A6 File Offset: 0x000099A6
		internal DataIndex()
			: this(DataIndexMetadata.Empty)
		{
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0000B7B3 File Offset: 0x000099B3
		protected DataIndex(DataIndexMetadata metadata)
		{
			this.Metadata = metadata;
		}

		// Token: 0x060006AD RID: 1709
		public abstract IEnumerable<TokenSequenceMatch<DataInstanceInfo>> FindTokenSequenceMatchesForNonCompletion(IEnumerable<ITokenSequenceSearchDefinition> searchDefinitions, SearchSettings searchSettings, CancellationToken cancellationToken);

		// Token: 0x060006AE RID: 1710
		public abstract IEnumerable<TokenSequencePrefixMatch<DataInstanceInfo>> FindTokenSequenceMatchesForCompletion(ITokenSequenceSearchDefinition searchDefinition, SearchSettings searchSettings, CancellationToken cancellationToken, int targetMatchingInstances);

		// Token: 0x060006AF RID: 1711
		public abstract IEnumerable<string> GetSampleValues(EdmPropertyRef edmPropertyRef, int maxSamples);

		// Token: 0x060006B0 RID: 1712
		public abstract void WriteTo(DataIndexPackageWriter writer, CancellationToken cancellationToken);

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x0000B7C2 File Offset: 0x000099C2
		public DataIndexMetadata Metadata { get; }

		// Token: 0x04000672 RID: 1650
		public static readonly DataIndex Empty = new DataIndex.EmptyDataIndex();

		// Token: 0x02000209 RID: 521
		private sealed class EmptyDataIndex : DataIndex
		{
			// Token: 0x06000B37 RID: 2871 RVA: 0x00014D44 File Offset: 0x00012F44
			internal EmptyDataIndex()
				: base(DataIndexMetadata.Empty)
			{
			}

			// Token: 0x06000B38 RID: 2872 RVA: 0x00014D51 File Offset: 0x00012F51
			public override IEnumerable<TokenSequenceMatch<DataInstanceInfo>> FindTokenSequenceMatchesForNonCompletion(IEnumerable<ITokenSequenceSearchDefinition> searchDefinitions, SearchSettings searchSettings, CancellationToken cancellationToken)
			{
				return Enumerable.Empty<TokenSequenceMatch<DataInstanceInfo>>();
			}

			// Token: 0x06000B39 RID: 2873 RVA: 0x00014D58 File Offset: 0x00012F58
			public override IEnumerable<TokenSequencePrefixMatch<DataInstanceInfo>> FindTokenSequenceMatchesForCompletion(ITokenSequenceSearchDefinition searchDefinition, SearchSettings searchSettings, CancellationToken cancellationToken, int targetMatchingInstances)
			{
				return Enumerable.Empty<TokenSequencePrefixMatch<DataInstanceInfo>>();
			}

			// Token: 0x06000B3A RID: 2874 RVA: 0x00014D5F File Offset: 0x00012F5F
			public override IEnumerable<string> GetSampleValues(EdmPropertyRef edmPropertyRef, int maxSamples)
			{
				return Enumerable.Empty<string>();
			}

			// Token: 0x06000B3B RID: 2875 RVA: 0x00014D66 File Offset: 0x00012F66
			public override void WriteTo(DataIndexPackageWriter writer, CancellationToken cancellationToken)
			{
			}
		}
	}
}
