using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B4F RID: 6991
	internal sealed class ValueBufferService : IValueBufferingService
	{
		// Token: 0x0600AEEA RID: 44778 RVA: 0x0023CF3F File Offset: 0x0023B13F
		public ValueBufferService(IEngineHost engineHost, IEngine engine)
		{
			this.engineHost = engineHost;
			this.engine = engine;
		}

		// Token: 0x0600AEEB RID: 44779 RVA: 0x0023CF55 File Offset: 0x0023B155
		public IValueBuffer CreateBuffer()
		{
			return new ValueBufferService.ValueBuffer(this.engineHost, this.engine, new TempFileManager(this.engineHost));
		}

		// Token: 0x04005A2E RID: 23086
		private readonly IEngineHost engineHost;

		// Token: 0x04005A2F RID: 23087
		private readonly IEngine engine;

		// Token: 0x02001B50 RID: 6992
		private sealed class ValueBuffer : IValueBuffer, IDisposable
		{
			// Token: 0x0600AEEC RID: 44780 RVA: 0x0023CF73 File Offset: 0x0023B173
			public ValueBuffer(IEngineHost engineHost, IEngine engine, TempFileManager tempFileManager)
			{
				this.engineHost = engineHost;
				this.engine = engine;
				this.tempFileManager = tempFileManager;
				this.buffers = new Dictionary<string, FirewallBuffer>(StringComparer.Ordinal);
			}

			// Token: 0x0600AEED RID: 44781 RVA: 0x0023CFA0 File Offset: 0x0023B1A0
			public IDataReaderSource GetDataReaderSource(string key)
			{
				Dictionary<string, FirewallBuffer> dictionary = this.buffers;
				IDataReaderSource dataReaderSource;
				lock (dictionary)
				{
					dataReaderSource = this.buffers[key];
				}
				return dataReaderSource;
			}

			// Token: 0x0600AEEE RID: 44782 RVA: 0x0023CFE8 File Offset: 0x0023B1E8
			public void SetDataReaderSource(string key, IDataReaderSource dataReaderSource, Action<int> writeCallback)
			{
				FirewallBuffer firewallBuffer = new FirewallBuffer(this.engineHost, this.engine, this.tempFileManager, key);
				firewallBuffer.SetDataReaderSource(dataReaderSource, writeCallback);
				Dictionary<string, FirewallBuffer> dictionary = this.buffers;
				lock (dictionary)
				{
					this.buffers.Add(key, firewallBuffer);
				}
			}

			// Token: 0x0600AEEF RID: 44783 RVA: 0x0023D050 File Offset: 0x0023B250
			public void SetException(string key, ValueException2 exception, Action<int> writeCallback)
			{
				FirewallBuffer firewallBuffer = new FirewallBuffer(this.engineHost, this.engine, this.tempFileManager, key);
				firewallBuffer.SetException(exception, writeCallback);
				Dictionary<string, FirewallBuffer> dictionary = this.buffers;
				lock (dictionary)
				{
					this.buffers.Add(key, firewallBuffer);
				}
			}

			// Token: 0x0600AEF0 RID: 44784 RVA: 0x0023D0B8 File Offset: 0x0023B2B8
			public void Dispose()
			{
				this.tempFileManager.Dispose();
			}

			// Token: 0x04005A30 RID: 23088
			private readonly IEngineHost engineHost;

			// Token: 0x04005A31 RID: 23089
			private readonly IEngine engine;

			// Token: 0x04005A32 RID: 23090
			private readonly TempFileManager tempFileManager;

			// Token: 0x04005A33 RID: 23091
			private readonly Dictionary<string, FirewallBuffer> buffers;
		}
	}
}
