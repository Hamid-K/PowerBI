using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Requester;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A08 RID: 2568
	internal class DrdaEmptyResultSet : DrdaResultSet
	{
		// Token: 0x060050DE RID: 20702 RVA: 0x00143D52 File Offset: 0x00141F52
		public DrdaEmptyResultSet()
			: base(null, null, null)
		{
		}

		// Token: 0x060050DF RID: 20703 RVA: 0x000189CC File Offset: 0x00016BCC
		public override DrdaColumnBinding GetColumn(int i)
		{
			return null;
		}

		// Token: 0x060050E0 RID: 20704 RVA: 0x00143D5D File Offset: 0x00141F5D
		public override Task<bool> ReadAsync(QueryScrollOrientation orientation, long number, bool isAsync, CancellationToken cancellationToken)
		{
			return Task.FromResult<bool>(false);
		}

		// Token: 0x060050E1 RID: 20705 RVA: 0x00006F04 File Offset: 0x00005104
		public override bool HasData()
		{
			return false;
		}

		// Token: 0x17001398 RID: 5016
		// (get) Token: 0x060050E2 RID: 20706 RVA: 0x00006F04 File Offset: 0x00005104
		public override bool HasRows
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060050E3 RID: 20707 RVA: 0x00006F04 File Offset: 0x00005104
		public override bool HasSchema()
		{
			return false;
		}

		// Token: 0x17001399 RID: 5017
		// (get) Token: 0x060050E4 RID: 20708 RVA: 0x00006F04 File Offset: 0x00005104
		public override int FieldCount
		{
			get
			{
				return 0;
			}
		}
	}
}
