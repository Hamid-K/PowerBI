using System;
using System.Collections.Generic;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;

namespace Microsoft.DataShaping.RawDataProcessing
{
	// Token: 0x02000005 RID: 5
	internal interface IPageReaderFactory
	{
		// Token: 0x06000003 RID: 3
		IPageReader CreatePageReader(Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.IRowset rowSet, IReadOnlyDictionary<string, string> columnMapping);
	}
}
