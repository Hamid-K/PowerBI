using System;
using System.Collections.Generic;
using Microsoft.PowerBI.ReportServer.AsServer.DataAccessObject;

namespace Microsoft.PowerBI.ReportServer.AsServer
{
	// Token: 0x0200001F RID: 31
	public interface IModelDataAccessor
	{
		// Token: 0x060000B0 RID: 176
		IList<ModelInfoEntity> GetModelInfo();

		// Token: 0x060000B1 RID: 177
		void RefreshLastQueried(string databaseName);
	}
}
