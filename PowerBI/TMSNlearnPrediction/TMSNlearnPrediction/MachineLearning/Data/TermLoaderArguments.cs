using System;
using Microsoft.MachineLearning.CommandLine;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020003F9 RID: 1017
	public sealed class TermLoaderArguments
	{
		// Token: 0x04000D13 RID: 3347
		[Argument(0, HelpText = "Comma separated list of terms", SortOrder = 1)]
		public string terms;

		// Token: 0x04000D14 RID: 3348
		[Argument(0, IsInputFileName = true, HelpText = "Data file containing the terms", ShortName = "data", SortOrder = 2)]
		public string dataFile;

		// Token: 0x04000D15 RID: 3349
		[Argument(4, HelpText = "Data loader", NullName = "<Auto>", SortOrder = 3)]
		public SubComponent<IDataLoader, SignatureDataLoader> loader;

		// Token: 0x04000D16 RID: 3350
		[Argument(0, HelpText = "Name of the text column containing the terms", ShortName = "termCol", SortOrder = 4)]
		public string termsColumn;

		// Token: 0x04000D17 RID: 3351
		[Argument(0, HelpText = "How items should be ordered when vectorized. By default, they will be in the order encountered. If by value items are sorted according to their default comparison, e.g., text sorting will be case sensitive (e.g., 'A' then 'Z' then 'a').", SortOrder = 5)]
		public TermTransform.SortOrder sort;

		// Token: 0x04000D18 RID: 3352
		[Argument(0, HelpText = "Drop unknown terms instead of mapping them to NA term.", ShortName = "dropna", SortOrder = 6)]
		public bool dropUnknowns;
	}
}
