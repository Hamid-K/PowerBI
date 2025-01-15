using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations
{
	// Token: 0x0200024B RID: 587
	internal sealed class WriteableDataShapeSharedAnnotation
	{
		// Token: 0x06001425 RID: 5157 RVA: 0x0004E1FB File Offset: 0x0004C3FB
		internal WriteableDataShapeSharedAnnotation()
		{
			this.ExistsFilter = null;
			this.ValueFilters = new List<Filter>();
			this.DataShapeValueFilters = new List<Filter>();
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x0004E220 File Offset: 0x0004C420
		// (set) Token: 0x06001427 RID: 5159 RVA: 0x0004E228 File Offset: 0x0004C428
		public ExistsFilterCondition ExistsFilter { get; set; }

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x0004E231 File Offset: 0x0004C431
		public List<Filter> ValueFilters { get; }

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x0004E239 File Offset: 0x0004C439
		public List<Filter> DataShapeValueFilters { get; }

		// Token: 0x0600142A RID: 5162 RVA: 0x0004E241 File Offset: 0x0004C441
		public DataShapeSharedAnnotation ToReadOnly()
		{
			return new DataShapeSharedAnnotation(this.ExistsFilter, this.ValueFilters, this.DataShapeValueFilters);
		}
	}
}
