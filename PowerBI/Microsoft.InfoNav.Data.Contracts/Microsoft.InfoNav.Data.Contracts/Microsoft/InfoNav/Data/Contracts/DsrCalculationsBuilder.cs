using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts
{
	// Token: 0x02000087 RID: 135
	internal class DsrCalculationsBuilder
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000325 RID: 805 RVA: 0x000091DE File Offset: 0x000073DE
		// (set) Token: 0x06000326 RID: 806 RVA: 0x000091E6 File Offset: 0x000073E6
		public IReadOnlyList<CalculationMetadata> Schema { get; private set; }

		// Token: 0x06000327 RID: 807 RVA: 0x000091EF File Offset: 0x000073EF
		public virtual void SetCalcMetadata(IReadOnlyList<CalculationMetadata> metadata)
		{
			this.Schema = metadata;
		}
	}
}
