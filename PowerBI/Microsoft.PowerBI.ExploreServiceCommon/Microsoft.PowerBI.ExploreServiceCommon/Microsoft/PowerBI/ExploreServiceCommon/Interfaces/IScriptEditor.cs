using System;
using System.Data;

namespace Microsoft.PowerBI.ExploreServiceCommon.Interfaces
{
	// Token: 0x02000025 RID: 37
	public interface IScriptEditor
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600014D RID: 333
		string Name { get; }

		// Token: 0x0600014E RID: 334
		void OpenEditor(string script, IDataReader reader);
	}
}
