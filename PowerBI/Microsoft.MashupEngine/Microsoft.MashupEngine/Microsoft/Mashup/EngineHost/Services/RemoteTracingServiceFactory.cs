using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B20 RID: 6944
	internal class RemoteTracingServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600ADFB RID: 44539 RVA: 0x0023A9E8 File Offset: 0x00238BE8
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			ITracingOptions tracingOptions = engineHost.QueryService<ITracingOptions>();
			IEnumerable<string> enumerable = ((tracingOptions != null) ? tracingOptions.Keys : null) ?? Enumerable.Empty<string>();
			proxyInitArgs.WriteArray(enumerable.ToArray<string>(), new Action<BinaryWriter, string>(BinaryReaderWriterExtensions.WriteString));
			return EmptyStub.Instance;
		}

		// Token: 0x0600ADFC RID: 44540 RVA: 0x0023AA2E File Offset: 0x00238C2E
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			EvaluatorTracing.Options = proxyInitArgs.ReadArray(new Func<BinaryReader, string>(BinaryReaderWriterExtensions.ReadString));
			return new RemoteTracingServiceFactory.Proxy(EvaluatorTracing.Service);
		}

		// Token: 0x02001B21 RID: 6945
		private sealed class Proxy : IRemoteServiceProxy, IEngineHost, IDisposable
		{
			// Token: 0x0600ADFE RID: 44542 RVA: 0x0023AA51 File Offset: 0x00238C51
			public Proxy(ITracingService tracingService)
			{
				this.tracingService = tracingService;
			}

			// Token: 0x0600ADFF RID: 44543 RVA: 0x0023AA60 File Offset: 0x00238C60
			T IEngineHost.QueryService<T>()
			{
				if (typeof(T) == typeof(ITracingService))
				{
					return (T)((object)this.tracingService);
				}
				return default(T);
			}

			// Token: 0x0600AE00 RID: 44544 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}

			// Token: 0x040059C1 RID: 22977
			private ITracingService tracingService;
		}
	}
}
