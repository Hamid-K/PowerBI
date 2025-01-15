using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.QueryDesignModel.QueryDesign.BatchQueries
{
	// Token: 0x020000C2 RID: 194
	internal interface IGroupBuilder<TTGroupingKey> : IGroupBuilder
	{
		// Token: 0x06000C68 RID: 3176
		void RemoveDuplicateKeys(HashSet<TTGroupingKey> existingKeys);
	}
}
