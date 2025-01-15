using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Mashup.Cdm;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cdm
{
	// Token: 0x02000011 RID: 17
	public class CdmMashup : ICdmMashup
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00003908 File Offset: 0x00001B08
		public CdmMashup(IEngineHost host, TableValue table, bool throwsException)
		{
			this.host = host;
			this.table = table;
			this.throwsException = throwsException;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003928 File Offset: 0x00001B28
		public string GetFileContents(string fileName)
		{
			string text;
			using (StreamReader streamReader = CdmHelper.GetBinaryFileContent(this.table, fileName).OpenText(Value.Null))
			{
				text = streamReader.ReadToEnd();
			}
			return text;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003970 File Offset: 0x00001B70
		public IEnumerable<string> GetFileNames(string folderName)
		{
			return CdmHelper.FetchAllFiles(this.table, folderName);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003980 File Offset: 0x00001B80
		public void ReportStatus(TraceEventType eventType, string message)
		{
			using (IHostTrace hostTrace = TracingService.CreateTrace(this.host, "Engine/Cdm/ReportStatus", TraceEventType.Information, null))
			{
				hostTrace.Add("Message", message, true);
				if (this.throwsException && eventType == TraceEventType.Error)
				{
					throw new CdmException(message);
				}
			}
		}

		// Token: 0x04000044 RID: 68
		private readonly IEngineHost host;

		// Token: 0x04000045 RID: 69
		private readonly TableValue table;

		// Token: 0x04000046 RID: 70
		private readonly bool throwsException;
	}
}
