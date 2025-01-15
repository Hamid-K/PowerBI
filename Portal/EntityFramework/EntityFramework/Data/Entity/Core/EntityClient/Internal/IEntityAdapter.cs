using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.EntityClient.Internal
{
	// Token: 0x020005E7 RID: 1511
	internal interface IEntityAdapter
	{
		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x060049D0 RID: 18896
		// (set) Token: 0x060049D1 RID: 18897
		DbConnection Connection { get; set; }

		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x060049D2 RID: 18898
		// (set) Token: 0x060049D3 RID: 18899
		bool AcceptChangesDuringUpdate { get; set; }

		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x060049D4 RID: 18900
		// (set) Token: 0x060049D5 RID: 18901
		int? CommandTimeout { get; set; }

		// Token: 0x060049D6 RID: 18902
		int Update();

		// Token: 0x060049D7 RID: 18903
		Task<int> UpdateAsync(CancellationToken cancellationToken);
	}
}
