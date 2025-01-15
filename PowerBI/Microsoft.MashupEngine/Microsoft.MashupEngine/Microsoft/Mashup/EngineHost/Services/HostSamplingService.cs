using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019E9 RID: 6633
	internal class HostSamplingService : ISamplingService
	{
		// Token: 0x0600A7E0 RID: 42976 RVA: 0x0022B718 File Offset: 0x00229918
		public HostSamplingService(Action samplingRecordedCallback)
		{
			this.samplingRecordedCallback = samplingRecordedCallback;
		}

		// Token: 0x17002ABA RID: 10938
		// (get) Token: 0x0600A7E1 RID: 42977 RVA: 0x00002105 File Offset: 0x00000305
		public bool SamplingEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002ABB RID: 10939
		// (get) Token: 0x0600A7E2 RID: 42978 RVA: 0x0022B727 File Offset: 0x00229927
		public int SampleRowCount
		{
			get
			{
				return 100000;
			}
		}

		// Token: 0x17002ABC RID: 10940
		// (get) Token: 0x0600A7E3 RID: 42979 RVA: 0x0022B72E File Offset: 0x0022992E
		public bool Sampled
		{
			get
			{
				return this.sampled;
			}
		}

		// Token: 0x0600A7E4 RID: 42980 RVA: 0x0022B736 File Offset: 0x00229936
		public void RecordSampling()
		{
			if (!this.sampled)
			{
				this.sampled = true;
				this.samplingRecordedCallback();
			}
		}

		// Token: 0x0400576A RID: 22378
		private bool sampled;

		// Token: 0x0400576B RID: 22379
		private readonly Action samplingRecordedCallback;

		// Token: 0x0400576C RID: 22380
		private const int sampleRowCount = 100000;
	}
}
