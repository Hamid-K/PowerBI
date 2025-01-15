using System;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020003FC RID: 1020
	internal class NgramHashExtractorFactory : INgramExtractorFactory
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06001569 RID: 5481 RVA: 0x0007CECE File Offset: 0x0007B0CE
		public bool UseHashingTrick
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x0007CED1 File Offset: 0x0007B0D1
		public NgramHashExtractorFactory(NgramHashExtractorTransform.NgramHashExtractorArguments extractorArgs, TermLoaderArguments customTermsArgs = null)
		{
			Contracts.CheckValue<NgramHashExtractorTransform.NgramHashExtractorArguments>(extractorArgs, "extractorArgs");
			this._extractorArgs = extractorArgs;
			this._termLoaderArgs = customTermsArgs;
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x0007CEF2 File Offset: 0x0007B0F2
		public IDataTransform Create(IHostEnvironment env, IDataView input, ExtractorColumn[] cols)
		{
			return NgramHashExtractorTransform.Create(this._extractorArgs, env, input, cols, this._termLoaderArgs);
		}

		// Token: 0x04000D1B RID: 3355
		private readonly NgramHashExtractorTransform.NgramHashExtractorArguments _extractorArgs;

		// Token: 0x04000D1C RID: 3356
		private readonly TermLoaderArguments _termLoaderArgs;
	}
}
