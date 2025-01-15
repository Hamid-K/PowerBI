using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics
{
	// Token: 0x02001C9C RID: 7324
	internal class IndexableRowWrapper : IRow, IEquatable<IRow>
	{
		// Token: 0x0600F7A0 RID: 63392 RVA: 0x0034C039 File Offset: 0x0034A239
		public IndexableRowWrapper(IIndexableRow indexableRow)
		{
			this.IndexableRow = indexableRow;
		}

		// Token: 0x17002961 RID: 10593
		// (get) Token: 0x0600F7A1 RID: 63393 RVA: 0x0034C048 File Offset: 0x0034A248
		public IIndexableRow IndexableRow { get; }

		// Token: 0x17002962 RID: 10594
		// (get) Token: 0x0600F7A2 RID: 63394 RVA: 0x000170F6 File Offset: 0x000152F6
		public IEnumerable<string> ColumnNames
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600F7A3 RID: 63395 RVA: 0x000170F6 File Offset: 0x000152F6
		public bool TryGetValue(string columnName, out object value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600F7A4 RID: 63396 RVA: 0x000170F6 File Offset: 0x000152F6
		public bool Equals(IRow other)
		{
			throw new NotImplementedException();
		}
	}
}
