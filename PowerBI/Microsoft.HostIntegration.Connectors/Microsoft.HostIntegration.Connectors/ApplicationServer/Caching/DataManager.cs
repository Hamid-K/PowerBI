using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200020E RID: 526
	internal sealed class DataManager : IDataManager
	{
		// Token: 0x06001108 RID: 4360 RVA: 0x000381FB File Offset: 0x000363FB
		internal DataManager(IDirectoryNodeFactory directoryNodeFactory)
		{
			this._directoryNodeFactory = directoryNodeFactory;
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x0003820A File Offset: 0x0003640A
		public IDMContainer CreateContainer()
		{
			return new DMHashContainer(this._directoryNodeFactory);
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x00038217 File Offset: 0x00036417
		public IDMContainer CreateContainer(IContainerSchema schema)
		{
			return new DMHashContainer(schema, this._directoryNodeFactory);
		}

		// Token: 0x04000AE2 RID: 2786
		private IDirectoryNodeFactory _directoryNodeFactory;
	}
}
