using System;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Autocompletion
{
	// Token: 0x0200026F RID: 623
	public struct CompletionResultWithIndex
	{
		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x00027625 File Offset: 0x00025825
		public readonly string Key { get; }

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000D73 RID: 3443 RVA: 0x0002762D File Offset: 0x0002582D
		public readonly CompletionInfo Value { get; }

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x00027635 File Offset: 0x00025835
		public readonly int Index { get; }

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x0002763D File Offset: 0x0002583D
		public readonly string FullPrefix { get; }

		// Token: 0x06000D76 RID: 3446 RVA: 0x00027645 File Offset: 0x00025845
		public CompletionResultWithIndex(string key, CompletionInfo value, int index, string fullPrefix)
		{
			this.Key = key;
			this.Value = value;
			this.Index = index;
			this.FullPrefix = fullPrefix;
		}
	}
}
