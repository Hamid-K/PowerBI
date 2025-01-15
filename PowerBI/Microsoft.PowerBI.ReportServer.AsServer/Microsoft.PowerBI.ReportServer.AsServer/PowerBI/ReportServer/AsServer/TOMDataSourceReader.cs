using System;
using Microsoft.AnalysisServices;
using Microsoft.AnalysisServices.Azure.Common;
using Microsoft.AnalysisServices.Tabular;

namespace Microsoft.PowerBI.ReportServer.AsServer
{
	// Token: 0x02000014 RID: 20
	internal class TOMDataSourceReader : IDataSourceReader, IDisposable
	{
		// Token: 0x06000076 RID: 118 RVA: 0x000040DF File Offset: 0x000022DF
		public TOMDataSourceReader(Server server)
		{
			this._asServer = server;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000040EE File Offset: 0x000022EE
		public virtual void Connect(string connectionString)
		{
			this._asServer.Connect(connectionString);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000040FC File Offset: 0x000022FC
		public virtual IDataResultReader ExecuteReader(string DiscoverDataSourceText)
		{
			XmlaResultCollection xmlaResultCollection;
			AmoDataReader amoDataReader = this._asServer.ExecuteReader(DiscoverDataSourceText, out xmlaResultCollection, null, true);
			if (xmlaResultCollection != null && xmlaResultCollection.ContainsErrors)
			{
				throw new OperationException(xmlaResultCollection);
			}
			if (amoDataReader == null)
			{
				throw new DataSourceDiscoveryException("AMODataReader returned by discovery execution is null.");
			}
			return new AMODataResultReader(amoDataReader);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000413E File Offset: 0x0000233E
		public void Dispose()
		{
			this._asServer.Dispose();
		}

		// Token: 0x0400004C RID: 76
		protected readonly Server _asServer;
	}
}
