using System;
using Microsoft.MachineLearning.CommandLine;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200007D RID: 125
	public abstract class ScorerArgumentsBase
	{
		// Token: 0x040000CB RID: 203
		[Argument(0, HelpText = "Output column: The suffix to append to the default column names", ShortName = "ex")]
		public string suffix;
	}
}
