using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.OData.Client
{
	// Token: 0x020000D2 RID: 210
	[SuppressMessage("Microsoft.Naming", "CA1710", Justification = "required for this feature")]
	public sealed class QueryOperationResponse<T> : QueryOperationResponse, IEnumerable<T>, IEnumerable
	{
		// Token: 0x060006D0 RID: 1744 RVA: 0x0001CA14 File Offset: 0x0001AC14
		internal QueryOperationResponse(HeaderCollection headers, DataServiceRequest query, MaterializeAtom results)
			: base(headers, query, results)
		{
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x0001CA1F File Offset: 0x0001AC1F
		public override long TotalCount
		{
			get
			{
				if (base.Results != null && base.Results.IsCountable)
				{
					return base.Results.CountValue();
				}
				throw new InvalidOperationException(Strings.MaterializeFromAtom_CountNotPresent);
			}
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001CA4C File Offset: 0x0001AC4C
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "required for this feature")]
		public new DataServiceQueryContinuation<T> GetContinuation()
		{
			return (DataServiceQueryContinuation<T>)base.GetContinuation();
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001CA59 File Offset: 0x0001AC59
		public new IEnumerator<T> GetEnumerator()
		{
			return base.GetEnumeratorHelper<IEnumerator<T>>(() => base.Results.Cast<T>().GetEnumerator());
		}
	}
}
