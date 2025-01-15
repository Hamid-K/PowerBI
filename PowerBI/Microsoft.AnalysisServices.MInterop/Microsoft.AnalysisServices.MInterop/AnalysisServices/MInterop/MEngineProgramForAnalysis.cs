using System;
using System.Collections.Concurrent;
using Microsoft.AnalysisServices.PlatformHost;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x0200001F RID: 31
	internal class MEngineProgramForAnalysis
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000077 RID: 119 RVA: 0x0000401B File Offset: 0x0000221B
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00004023 File Offset: 0x00002223
		internal string MProgram { get; private set; }

		// Token: 0x06000079 RID: 121 RVA: 0x0000402C File Offset: 0x0000222C
		public MEngineProgramForAnalysis(string MProgram, MStaticAnalysisMode batchMode, string batchKey)
		{
			this.batchMode = batchMode;
			this.batchKey = batchKey;
			this.MProgram = MProgram;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000404C File Offset: 0x0000224C
		public MEngineProgramForAnalysis(string MProgram, string MQuery, MStaticAnalysisMode batchMode, string batchKey)
		{
			this.batchMode = batchMode;
			this.batchKey = batchKey;
			if (batchMode == MStaticAnalysisMode.Simple)
			{
				string text;
				MInteropHelperImpl.GetMinimizedPartitionMProgram(MProgram, MQuery, out text);
				this.MProgram = text;
				return;
			}
			this.MProgram = MProgram;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000408C File Offset: 0x0000228C
		public IMEngineDataSourceDiscovery GetDataSourceDiscovery(MEngineDiscoveryOptions discoveryOptions)
		{
			MStaticAnalysisMode mstaticAnalysisMode = this.batchMode;
			if (mstaticAnalysisMode == MStaticAnalysisMode.Simple)
			{
				return new MEngineDataSourceDiscovery(this.MProgram, discoveryOptions);
			}
			if (mstaticAnalysisMode != MStaticAnalysisMode.Batch)
			{
				throw MInteropHelperImpl.InternalError("D:\\dbs\\sh\\uikp\\0709_010605\\cmd\\13\\Sql\\Picasso\\Engine\\src\\om\\MInterop\\MEngineProgramForAnalysis.cs", "GetDataSourceDiscovery", 77);
			}
			string batchKeyWithOptions = MEngineProgramForAnalysis.GetBatchKeyWithOptions(this.batchKey, discoveryOptions);
			IMEngineDataSourceDiscovery imengineDataSourceDiscovery;
			if (MEngineProgramForAnalysis.batchDiscovery.TryGetValue(batchKeyWithOptions, out imengineDataSourceDiscovery))
			{
				return imengineDataSourceDiscovery;
			}
			IEngineTracer engineTracer = MInteropHelperImpl.EngineTracer;
			if (engineTracer != null)
			{
				engineTracer.LogMessage(string.Format("Adding new MEngineBatchModeDataSourceDiscovery with key: {0}", batchKeyWithOptions));
			}
			IMEngineDataSourceDiscovery imengineDataSourceDiscovery2 = new MEngineBatchModeDataSourceDiscovery(this.MProgram, discoveryOptions);
			if (MEngineProgramForAnalysis.batchDiscovery.TryAdd(batchKeyWithOptions, imengineDataSourceDiscovery2))
			{
				return imengineDataSourceDiscovery2;
			}
			throw MInteropHelperImpl.InternalError("D:\\dbs\\sh\\uikp\\0709_010605\\cmd\\13\\Sql\\Picasso\\Engine\\src\\om\\MInterop\\MEngineProgramForAnalysis.cs", "GetDataSourceDiscovery", 75);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004130 File Offset: 0x00002330
		public static void ClearBatch(string batchKey, MEngineDiscoveryOptions discoveryOptions)
		{
			IMEngineDataSourceDiscovery imengineDataSourceDiscovery;
			MEngineProgramForAnalysis.batchDiscovery.TryRemove(MEngineProgramForAnalysis.GetBatchKeyWithOptions(batchKey, discoveryOptions), out imengineDataSourceDiscovery);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004151 File Offset: 0x00002351
		private static string GetBatchKeyWithOptions(string batchKey, MEngineDiscoveryOptions discoveryOptions)
		{
			return string.Format(string.Format("{0}.{1}", batchKey, discoveryOptions), Array.Empty<object>());
		}

		// Token: 0x040000B1 RID: 177
		internal static ConcurrentDictionary<string, IMEngineDataSourceDiscovery> batchDiscovery = new ConcurrentDictionary<string, IMEngineDataSourceDiscovery>();

		// Token: 0x040000B2 RID: 178
		internal readonly MStaticAnalysisMode batchMode;

		// Token: 0x040000B3 RID: 179
		private readonly string batchKey;
	}
}
