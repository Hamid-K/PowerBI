using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000295 RID: 661
	internal sealed class RdlChunkMapper
	{
		// Token: 0x06001823 RID: 6179 RVA: 0x000621F0 File Offset: 0x000603F0
		internal string GetRdlChunkName(CatalogItemContext reportContext, bool isMainReport, out bool isNew)
		{
			RSTrace.CatalogTrace.Assert(reportContext != null, "reportContext");
			RSTrace.CatalogTrace.Assert(!this.m_chunkNamesPersisted, "chunk names already persisted");
			string value = reportContext.ItemPath.Value;
			Dictionary<string, RdlChunkMapper.ChunkData> rdlChunkNames = this.m_rdlChunkNames;
			string text;
			lock (rdlChunkNames)
			{
				RSTrace.CatalogTrace.Assert(!isMainReport || !this.m_mainReportStored, "main report already defined");
				isNew = !this.m_rdlChunkNames.ContainsKey(value);
				if (isNew)
				{
					text = Guid.NewGuid().ToString();
					this.m_rdlChunkNames.Add(value, new RdlChunkMapper.ChunkData(text, isMainReport));
				}
				else
				{
					RdlChunkMapper.ChunkData chunkData = this.m_rdlChunkNames[value];
					chunkData.IsMainReport = isMainReport;
					text = chunkData.Name;
				}
				this.m_mainReportStored = this.m_mainReportStored || isMainReport;
			}
			RSTrace.CatalogTrace.Assert(!string.IsNullOrEmpty(text), "chunkName");
			return text;
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x00062308 File Offset: 0x00060508
		private void SerializeRdlMapping(Stream s)
		{
			RSTrace.CatalogTrace.Assert(s != null && s.CanWrite, "stream is invalid");
			RSTrace.CatalogTrace.Assert(this.m_rdlChunkNames.Count > 0, "m_rdlChunkNames.Count");
			BinaryWriter binaryWriter = new BinaryWriter(s);
			Dictionary<string, RdlChunkMapper.ChunkData> rdlChunkNames = this.m_rdlChunkNames;
			lock (rdlChunkNames)
			{
				binaryWriter.Write(1);
				binaryWriter.Write(this.m_rdlChunkNames.Count);
				foreach (KeyValuePair<string, RdlChunkMapper.ChunkData> keyValuePair in this.m_rdlChunkNames)
				{
					string key = keyValuePair.Key;
					string name = keyValuePair.Value.Name;
					bool isMainReport = keyValuePair.Value.IsMainReport;
					binaryWriter.Write(key);
					binaryWriter.Write(isMainReport);
					binaryWriter.Write(name);
				}
				binaryWriter.Flush();
			}
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x00062418 File Offset: 0x00060618
		internal void CreateChunkAndSerializeRdlMapping(ReportSnapshot snapshot)
		{
			Stream stream = snapshot.CreateChunk("RdlChunkMapping", ReportProcessing.ReportChunkTypes.ServerRdlMapping, null);
			this.SerializeRdlMapping(stream);
			this.m_chunkNamesPersisted = true;
		}

		// Token: 0x040008B4 RID: 2228
		private readonly Dictionary<string, RdlChunkMapper.ChunkData> m_rdlChunkNames = new Dictionary<string, RdlChunkMapper.ChunkData>(Localization.CatalogStringComparer);

		// Token: 0x040008B5 RID: 2229
		private bool m_chunkNamesPersisted;

		// Token: 0x040008B6 RID: 2230
		private bool m_mainReportStored;

		// Token: 0x020004D0 RID: 1232
		private sealed class ChunkData
		{
			// Token: 0x06002463 RID: 9315 RVA: 0x00086119 File Offset: 0x00084319
			public ChunkData(string name, bool isMainReport)
			{
				this.Name = name;
				this.IsMainReport = isMainReport;
			}

			// Token: 0x04001118 RID: 4376
			public readonly string Name;

			// Token: 0x04001119 RID: 4377
			public bool IsMainReport;
		}
	}
}
