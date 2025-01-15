using System;
using System.Data;
using Microsoft.AnalysisServices;

namespace Microsoft.PowerBI.ReportServer.AsServer
{
	// Token: 0x0200000A RID: 10
	public class AMODataResultReader : IDisposable, IDataResultReader
	{
		// Token: 0x0600004A RID: 74 RVA: 0x00003B1F File Offset: 0x00001D1F
		public AMODataResultReader(AmoDataReader reader)
		{
			this.amoDataReader = reader;
			this.amoDataAdapter = new AmoDataAdapter(this.amoDataReader);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003B40 File Offset: 0x00001D40
		public DataSet Evaluate()
		{
			DataSet dataSet = new DataSet();
			this.amoDataAdapter.Fill(dataSet);
			XmlaResultCollection results = this.amoDataReader.Results;
			if (results != null && results.ContainsErrors)
			{
				throw new OperationException(this.amoDataReader.Results);
			}
			return dataSet;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003B8B File Offset: 0x00001D8B
		public void Dispose()
		{
			AmoDataAdapter amoDataAdapter = this.amoDataAdapter;
			if (amoDataAdapter != null)
			{
				amoDataAdapter.Dispose();
			}
			AmoDataReader amoDataReader = this.amoDataReader;
			if (amoDataReader == null)
			{
				return;
			}
			amoDataReader.Dispose();
		}

		// Token: 0x04000044 RID: 68
		private AmoDataReader amoDataReader;

		// Token: 0x04000045 RID: 69
		private AmoDataAdapter amoDataAdapter;
	}
}
