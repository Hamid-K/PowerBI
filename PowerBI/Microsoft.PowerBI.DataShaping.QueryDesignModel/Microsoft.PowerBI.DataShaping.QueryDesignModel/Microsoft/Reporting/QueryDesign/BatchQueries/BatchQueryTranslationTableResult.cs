using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x02000263 RID: 611
	internal sealed class BatchQueryTranslationTableResult
	{
		// Token: 0x06001A8A RID: 6794 RVA: 0x0004993C File Offset: 0x00047B3C
		internal BatchQueryTranslationTableResult(IEnumerable<QueryResultField> resultFields)
		{
			this._resultFields = resultFields.ToReadOnlyCollection<QueryResultField>();
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06001A8B RID: 6795 RVA: 0x00049950 File Offset: 0x00047B50
		public ReadOnlyCollection<QueryResultField> ResultFields
		{
			get
			{
				return this._resultFields;
			}
		}

		// Token: 0x04000EA9 RID: 3753
		private readonly ReadOnlyCollection<QueryResultField> _resultFields;
	}
}
