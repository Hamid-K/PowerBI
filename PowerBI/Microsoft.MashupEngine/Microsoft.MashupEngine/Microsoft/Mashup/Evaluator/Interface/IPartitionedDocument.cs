using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E22 RID: 7714
	public interface IPartitionedDocument
	{
		// Token: 0x17002ECA RID: 11978
		// (get) Token: 0x0600BE13 RID: 48659
		IPackage Package { get; }

		// Token: 0x17002ECB RID: 11979
		// (get) Token: 0x0600BE14 RID: 48660
		PartitioningScheme PartitioningScheme { get; }

		// Token: 0x17002ECC RID: 11980
		// (get) Token: 0x0600BE15 RID: 48661
		IEnumerable<IPartitionKey> PartitionKeys { get; }

		// Token: 0x0600BE16 RID: 48662
		IEnumerable<IPartitionKey> GetPartitionInputs(IPartitionKey partitionKey);

		// Token: 0x0600BE17 RID: 48663
		bool IsPartitionInError(IPartitionKey partitionKey);

		// Token: 0x0600BE18 RID: 48664
		SegmentedString GetPartition(IPartitionKey partitionKey);

		// Token: 0x0600BE19 RID: 48665
		string GetPartitionSection(IPartitionKey partitionKey);

		// Token: 0x0600BE1A RID: 48666
		string GetPartitionSectionOffsetAndLength(IPartitionKey partitionKey, out int offset, out int length);

		// Token: 0x0600BE1B RID: 48667
		bool TryGetOffsetAndLength(string sectionName, TextRange range, out int offset, out int length);

		// Token: 0x0600BE1C RID: 48668
		IPartitionKey GetPartitionKeyAndOffset(string sectionName, int offset, int length, out int partitionOffset);

		// Token: 0x0600BE1D RID: 48669
		IEnumerable<PackageEdit> ReplacePartition(IPartitionKey partitionKey, SegmentedString expression);

		// Token: 0x0600BE1E RID: 48670
		IEnumerable<PackageEdit> ReferencePartition(IPartitionKey partitionKey, out string referencingExpression);

		// Token: 0x0600BE1F RID: 48671
		IPartitionedDocument ApplyEdits(IEnumerable<PackageEdit> edits);
	}
}
