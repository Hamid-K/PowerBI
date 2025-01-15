using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;

namespace Microsoft.Mashup.Engine1.Library.Excel
{
	// Token: 0x02000C0D RID: 3085
	public class AceMutex : IDisposable
	{
		// Token: 0x0600542A RID: 21546 RVA: 0x00120B18 File Offset: 0x0011ED18
		public AceMutex(string mutexName, IEngineHost host)
		{
			this.mutex = new Mutex(false, mutexName);
			try
			{
				this.mutex.WaitOne();
			}
			catch (AbandonedMutexException ex)
			{
				using (IHostTrace hostTrace = TracingService.CreateTrace(host, "Engine/IO/Excel/AceMutex", TraceEventType.Information, null))
				{
					hostTrace.Add(ex, TraceEventType.Warning, true);
				}
			}
		}

		// Token: 0x0600542B RID: 21547 RVA: 0x00120B88 File Offset: 0x0011ED88
		public static string GetMutexName(string filePath)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BAD75C71-8C90-4F83-9EAB-F40795F43875");
			if (filePath.Length + "BAD75C71-8C90-4F83-9EAB-F40795F43875".Length < 260)
			{
				stringBuilder.Append(filePath);
			}
			else
			{
				using (HashAlgorithm hashAlgorithm = CryptoAlgorithmFactory.CreateSHA256Algorithm())
				{
					stringBuilder.Append(Convert.ToBase64String(hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(filePath))));
				}
				stringBuilder.Append(filePath.Substring(filePath.Length - 260 + stringBuilder.Length));
			}
			return stringBuilder.ToString().Replace('\\', '-');
		}

		// Token: 0x0600542C RID: 21548 RVA: 0x00120C38 File Offset: 0x0011EE38
		public void Dispose()
		{
			this.mutex.ReleaseMutex();
			this.mutex.Close();
		}

		// Token: 0x04002E81 RID: 11905
		private const string MutexNamePrefix = "BAD75C71-8C90-4F83-9EAB-F40795F43875";

		// Token: 0x04002E82 RID: 11906
		private const int MaxMutexNameLength = 260;

		// Token: 0x04002E83 RID: 11907
		private readonly Mutex mutex;
	}
}
