using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B12 RID: 6930
	internal class RemoteSamplingServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600ADC5 RID: 44485 RVA: 0x0023A51C File Offset: 0x0023871C
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			ISamplingService samplingService = engineHost.QueryService<ISamplingService>();
			proxyInitArgs.WriteBool(samplingService.SamplingEnabled);
			if (samplingService.SamplingEnabled)
			{
				proxyInitArgs.WriteInt32(samplingService.SampleRowCount);
			}
			return EmptyStub.Instance;
		}

		// Token: 0x0600ADC6 RID: 44486 RVA: 0x0023A558 File Offset: 0x00238758
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			bool flag = proxyInitArgs.ReadBool();
			int num = -1;
			if (flag)
			{
				num = proxyInitArgs.ReadInt32();
			}
			return new RemoteSamplingServiceFactory.Proxy(flag, num);
		}

		// Token: 0x02001B13 RID: 6931
		private sealed class Proxy : IRemoteServiceProxy, IEngineHost, IDisposable, ISamplingService
		{
			// Token: 0x0600ADC8 RID: 44488 RVA: 0x0023A57D File Offset: 0x0023877D
			public Proxy(bool samplingEnabled, int sampleRowCount)
			{
				this.samplingEnabled = samplingEnabled;
				this.sampleRowCount = sampleRowCount;
			}

			// Token: 0x17002BAA RID: 11178
			// (get) Token: 0x0600ADC9 RID: 44489 RVA: 0x0023A593 File Offset: 0x00238793
			public bool SamplingEnabled
			{
				get
				{
					return this.samplingEnabled;
				}
			}

			// Token: 0x17002BAB RID: 11179
			// (get) Token: 0x0600ADCA RID: 44490 RVA: 0x0023A59B File Offset: 0x0023879B
			public int SampleRowCount
			{
				get
				{
					return this.sampleRowCount;
				}
			}

			// Token: 0x17002BAC RID: 11180
			// (get) Token: 0x0600ADCB RID: 44491 RVA: 0x0023A5A3 File Offset: 0x002387A3
			public bool Sampled
			{
				get
				{
					return this.sampled;
				}
			}

			// Token: 0x0600ADCC RID: 44492 RVA: 0x0023A5AB File Offset: 0x002387AB
			public void RecordSampling()
			{
				this.sampled = true;
			}

			// Token: 0x0600ADCD RID: 44493 RVA: 0x0023A5B4 File Offset: 0x002387B4
			T IEngineHost.QueryService<T>()
			{
				if (typeof(T) == typeof(ISamplingService))
				{
					return (T)((object)this);
				}
				return default(T);
			}

			// Token: 0x0600ADCE RID: 44494 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}

			// Token: 0x040059B1 RID: 22961
			private readonly bool samplingEnabled;

			// Token: 0x040059B2 RID: 22962
			private readonly int sampleRowCount;

			// Token: 0x040059B3 RID: 22963
			private bool sampled;
		}
	}
}
