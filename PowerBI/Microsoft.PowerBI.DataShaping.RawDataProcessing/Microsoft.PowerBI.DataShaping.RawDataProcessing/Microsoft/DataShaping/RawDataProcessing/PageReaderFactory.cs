using System;
using System.Collections.Generic;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;
using Microsoft.PowerBI.DataMovement.Pipeline.PowerBIPipeline;

namespace Microsoft.DataShaping.RawDataProcessing
{
	// Token: 0x02000008 RID: 8
	internal class PageReaderFactory : IPageReaderFactory
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002067 File Offset: 0x00000267
		public IPageReader CreatePageReader(Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.IRowset rowSet, IReadOnlyDictionary<string, string> columnMapping)
		{
			return new PageReaderView(new RowsetPageReader(rowSet), columnMapping);
		}
	}
}
