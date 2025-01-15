using System;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x020001F3 RID: 499
	internal interface INotifyObjectNameChange
	{
		// Token: 0x06001CA4 RID: 7332
		void NotifyNameChanging(NamedMetadataObject obj, string newName);

		// Token: 0x06001CA5 RID: 7333
		void NotifyNameChanged(NamedMetadataObject obj, string oldName);
	}
}
