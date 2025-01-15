using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x020001EB RID: 491
	internal interface IMetadataObjectCollection<T> : ICollection<T>, IEnumerable<T>, IEnumerable, IMetadataObjectCollection, INotifyObjectIdChange, ITxObject where T : MetadataObject
	{
		// Token: 0x06001C82 RID: 7298
		T FindById(ObjectId id);
	}
}
