using System;

namespace Microsoft.Lucia.Core.TermIndex
{
	// Token: 0x0200015D RID: 349
	public static class DataIndexMetadataExtensions
	{
		// Token: 0x060006ED RID: 1773 RVA: 0x0000BDA0 File Offset: 0x00009FA0
		public static InterpretWarnings ExtractInterpretWarnings(this DataIndexMetadata dataIndexMetadata)
		{
			InterpretWarnings interpretWarnings = InterpretWarnings.None;
			if (dataIndexMetadata.IsSizeLimitReached())
			{
				interpretWarnings |= InterpretWarnings.DataIndexSizeLimitReached;
			}
			if (dataIndexMetadata.MissingAllStatistics())
			{
				interpretWarnings |= (InterpretWarnings)((ulong)int.MinValue);
			}
			else if (dataIndexMetadata.IsStatisticsMissing())
			{
				interpretWarnings |= InterpretWarnings.DataIndexMissingStatistics;
			}
			if (dataIndexMetadata.IsIndexingCancelled())
			{
				interpretWarnings |= InterpretWarnings.DataIndexBuildingCancelled;
			}
			return interpretWarnings;
		}
	}
}
