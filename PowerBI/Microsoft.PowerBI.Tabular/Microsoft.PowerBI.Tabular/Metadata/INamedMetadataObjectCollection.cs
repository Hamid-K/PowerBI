using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x020001EF RID: 495
	internal interface INamedMetadataObjectCollection : IMetadataObjectCollection, INotifyObjectIdChange, ITxObject, INotifyObjectNameChange
	{
		// Token: 0x06001C90 RID: 7312
		MetadataObject Find(string name);

		// Token: 0x06001C91 RID: 7313
		void Remove(string name);

		// Token: 0x06001C92 RID: 7314
		IEqualityComparer<string> GetNamesComparer();

		// Token: 0x06001C93 RID: 7315
		bool CanUpdateCultureInfo(IEqualityComparer<string> comparer);

		// Token: 0x06001C94 RID: 7316
		void UpdateCultureInfo(IEqualityComparer<string> comparer);
	}
}
