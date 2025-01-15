using System;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020003FB RID: 1019
	internal class NgramExtractorFactory : INgramExtractorFactory
	{
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06001566 RID: 5478 RVA: 0x0007CE94 File Offset: 0x0007B094
		public bool UseHashingTrick
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x0007CE97 File Offset: 0x0007B097
		public NgramExtractorFactory(NgramExtractorTransform.NgramExtractorArguments extractorArgs, TermLoaderArguments termLoaderArgs)
		{
			Contracts.CheckValue<NgramExtractorTransform.NgramExtractorArguments>(extractorArgs, "extractorArgs");
			this._extractorArgs = extractorArgs;
			this._termLoaderArgs = termLoaderArgs;
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x0007CEB8 File Offset: 0x0007B0B8
		public IDataTransform Create(IHostEnvironment env, IDataView input, ExtractorColumn[] cols)
		{
			return NgramExtractorTransform.Create(this._extractorArgs, env, input, cols, this._termLoaderArgs);
		}

		// Token: 0x04000D19 RID: 3353
		private readonly NgramExtractorTransform.NgramExtractorArguments _extractorArgs;

		// Token: 0x04000D1A RID: 3354
		private readonly TermLoaderArguments _termLoaderArgs;
	}
}
