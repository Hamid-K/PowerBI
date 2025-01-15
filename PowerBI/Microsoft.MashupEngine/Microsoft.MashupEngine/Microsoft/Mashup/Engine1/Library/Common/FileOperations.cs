using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010C8 RID: 4296
	internal static class FileOperations
	{
		// Token: 0x06007098 RID: 28824 RVA: 0x00183388 File Offset: 0x00181588
		public static bool TryDelete(IEngineHost host, string path, IResource resource = null)
		{
			bool result2;
			using (IHostTrace hostTrace = TracingService.CreateTrace(host, "FileHelper/TryDelete", TraceEventType.Information, resource))
			{
				hostTrace.Add("Path", path, true);
				bool result = false;
				SafeExceptions.IgnoreSafeExceptions(host, hostTrace, delegate
				{
					File.Delete(path);
					result = true;
				});
				hostTrace.Add("Result", result, false);
				result2 = result;
			}
			return result2;
		}
	}
}
