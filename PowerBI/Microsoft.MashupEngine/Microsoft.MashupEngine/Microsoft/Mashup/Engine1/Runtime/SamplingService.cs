using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200160A RID: 5642
	public static class SamplingService
	{
		// Token: 0x06008DE5 RID: 36325 RVA: 0x001DA750 File Offset: 0x001D8950
		public static ISamplingService GetSamplingService(IEngineHost host)
		{
			ISamplingService samplingService = host.QueryService<ISamplingService>();
			if (samplingService != null)
			{
				return samplingService;
			}
			return SamplingService.noSamplingService;
		}

		// Token: 0x04004D32 RID: 19762
		private static readonly SamplingService.NoSamplingService noSamplingService = new SamplingService.NoSamplingService();

		// Token: 0x0200160B RID: 5643
		private class NoSamplingService : ISamplingService
		{
			// Token: 0x17002542 RID: 9538
			// (get) Token: 0x06008DE7 RID: 36327 RVA: 0x00002105 File Offset: 0x00000305
			public bool SamplingEnabled
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002543 RID: 9539
			// (get) Token: 0x06008DE8 RID: 36328 RVA: 0x0000EE09 File Offset: 0x0000D009
			public int SampleRowCount
			{
				get
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x17002544 RID: 9540
			// (get) Token: 0x06008DE9 RID: 36329 RVA: 0x00002105 File Offset: 0x00000305
			public bool Sampled
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06008DEA RID: 36330 RVA: 0x0000EE09 File Offset: 0x0000D009
			public void RecordSampling()
			{
				throw new InvalidOperationException();
			}
		}
	}
}
