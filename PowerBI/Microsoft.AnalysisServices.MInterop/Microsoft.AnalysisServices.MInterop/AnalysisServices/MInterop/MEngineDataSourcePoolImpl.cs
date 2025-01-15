using System;
using Microsoft.Data.Mashup;
using Microsoft.Data.Mashup.Preview;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x0200001E RID: 30
	internal sealed class MEngineDataSourcePoolImpl : IDisposable
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00003F98 File Offset: 0x00002198
		internal MEngineDataSourcePoolImpl(string name)
		{
			this.handle = MashupDataSourcePoolHandle.FromDataSourcePool(name);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003FAC File Offset: 0x000021AC
		public void AddUsingResourcePath(string completeResourcePath, int maxConnections)
		{
			DataSource dataSource = DSRConversionHelper.ParseCompleteResourcePath(completeResourcePath);
			this.handle.SetMaxActiveConnections(dataSource, new int?(maxConnections));
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003FD4 File Offset: 0x000021D4
		public void AddUsingDSRJson(string dsrJson, int maxConnections)
		{
			DataSourceReference dataSourceReference = new DataSourceReference(dsrJson);
			this.handle.SetMaxActiveConnections(dataSourceReference.DataSource, new int?(maxConnections));
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003FFF File Offset: 0x000021FF
		public void Dispose()
		{
			if (this.handle != null)
			{
				this.handle.Dispose();
				this.handle = null;
			}
		}

		// Token: 0x040000B0 RID: 176
		private MashupDataSourcePoolHandle handle;
	}
}
