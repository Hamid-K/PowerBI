using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.Mashup.Cdm;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cdm
{
	// Token: 0x02000012 RID: 18
	public sealed class MashupCdmProvider : ICdmProvider
	{
		// Token: 0x0600005C RID: 92 RVA: 0x000039DC File Offset: 0x00001BDC
		public MashupCdmProvider(IEngineHost host)
		{
			this.host = host;
			object obj = MashupCdmProvider.syncRoot;
			lock (obj)
			{
				if (MashupCdmProvider.provider == null)
				{
					MashupCdmProvider.Init();
				}
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003A30 File Offset: 0x00001C30
		public CdmManifest GetManifest(Uri baseFolderPath, string folioFileName, ICdmMashup cdmMashup)
		{
			CdmManifest manifest;
			try
			{
				manifest = MashupCdmProvider.provider.GetManifest(baseFolderPath, folioFileName, cdmMashup);
			}
			catch (Exception ex) when (SafeExceptions.IsSafeException(ex))
			{
				CdmException ex2 = ex.InnerException as CdmException;
				if (ex2 != null)
				{
					using (IHostTrace hostTrace = TracingService.CreateTrace(this.host, "Engine/Cdm/MashupCdmProvider/GetManifest", TraceEventType.Information, null))
					{
						hostTrace.Add("Message", ex2.Message, true);
						throw ValueException.NewDataSourceError<Message2>(Resources.Cdm_Sdk_FailedToGenerateManifest(baseFolderPath.AbsolutePath, folioFileName), Value.Null, ex2);
					}
				}
				string text = ex.Message;
				AggregateException ex3 = ex as AggregateException;
				if (ex3 != null)
				{
					AggregateException ex4 = ex3.Flatten();
					text = ((ex4.InnerExceptions.Count == 1) ? ex4.InnerExceptions[0].Message : ex.Message);
				}
				throw ValueException.NewDataSourceError(text, Value.Null, ex);
			}
			return manifest;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003B34 File Offset: 0x00001D34
		public string GetManifestName(Uri baseFolderPath, string folioFileName, ICdmMashup cdmMashup)
		{
			string manifestName;
			try
			{
				manifestName = MashupCdmProvider.provider.GetManifestName(baseFolderPath, folioFileName, cdmMashup);
			}
			catch (Exception ex) when (SafeExceptions.IsSafeException(ex))
			{
				CdmException ex2 = ex.InnerException as CdmException;
				if (ex2 != null)
				{
					using (IHostTrace hostTrace = TracingService.CreateTrace(this.host, "Engine/Cdm/MashupCdmProvider/GetManifestName", TraceEventType.Information, null))
					{
						hostTrace.Add("Message", ex2.Message, true);
						throw ValueException.NewDataSourceError<Message2>(Resources.Cdm_Sdk_FailedToGetManifestName(baseFolderPath.AbsolutePath, folioFileName), Value.Null, ex2);
					}
				}
				string text = ex.Message;
				AggregateException ex3 = ex as AggregateException;
				if (ex3 != null)
				{
					AggregateException ex4 = ex3.Flatten();
					text = ((ex4.InnerExceptions.Count == 1) ? ex4.InnerExceptions[0].Message : ex.Message);
				}
				throw ValueException.NewDataSourceError(text, Value.Null, ex);
			}
			return manifestName;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003C38 File Offset: 0x00001E38
		private static void Init()
		{
			string text = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Cdm");
			Assembly assembly = Assembly.Load(AssemblyName.GetAssemblyName(Path.Combine(text, "Microsoft.Mashup.Cdm.MashupCdmProvider.dll")));
			foreach (string text2 in MashupCdmProvider.dependencies)
			{
				Assembly.Load(AssemblyName.GetAssemblyName(Path.Combine(text, text2)));
			}
			try
			{
				MashupCdmProvider.provider = (ICdmProvider)Activator.CreateInstance(assembly.GetType("Microsoft.Mashup.Cdm.MashupCdmProvider"));
			}
			catch (Exception ex) when (SafeExceptions.IsSafeException(ex))
			{
				throw ValueException.NewDataSourceError(ex.Message, Value.Null, null);
			}
		}

		// Token: 0x04000047 RID: 71
		private static ICdmProvider provider;

		// Token: 0x04000048 RID: 72
		private static object syncRoot = new object();

		// Token: 0x04000049 RID: 73
		private static string[] dependencies = new string[] { "Microsoft.CommonDataModel.ObjectModel.dll", "Microsoft.Mashup.Cdm.MashupCdmProvider.dll", "Microsoft.Mashup.Cdm.ICdmProvider.dll", "Newtonsoft.Json.dll", "System.Net.Http.Extensions.dll", "System.Net.Http.Primitives.dll" };

		// Token: 0x0400004A RID: 74
		private const string CdmDllFolderPath = "Cdm";

		// Token: 0x0400004B RID: 75
		private const string CdmDllFileName = "Microsoft.Mashup.Cdm.MashupCdmProvider.dll";

		// Token: 0x0400004C RID: 76
		private const string CdmProviderTypeName = "Microsoft.Mashup.Cdm.MashupCdmProvider";

		// Token: 0x0400004D RID: 77
		private readonly IEngineHost host;
	}
}
