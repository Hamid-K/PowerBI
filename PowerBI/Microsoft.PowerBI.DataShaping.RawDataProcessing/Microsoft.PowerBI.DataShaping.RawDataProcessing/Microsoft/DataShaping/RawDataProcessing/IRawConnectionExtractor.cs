using System;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase;

namespace Microsoft.DataShaping.RawDataProcessing
{
	// Token: 0x02000006 RID: 6
	internal interface IRawConnectionExtractor
	{
		// Token: 0x06000004 RID: 4
		bool TryExtractRawConnection(IDbConnection connection, out IDBCreateSession rawConnection);
	}
}
