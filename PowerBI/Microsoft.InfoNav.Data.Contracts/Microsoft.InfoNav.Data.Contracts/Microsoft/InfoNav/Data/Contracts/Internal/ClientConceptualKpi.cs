using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200015D RID: 349
	[DataContract(Name = "ConceptualKpi", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	internal sealed class ClientConceptualKpi
	{
		// Token: 0x060008DB RID: 2267 RVA: 0x000122C7 File Offset: 0x000104C7
		internal ClientConceptualKpi()
		{
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x000122CF File Offset: 0x000104CF
		internal ClientConceptualKpi(string statusGraphic, string status, string goal, string trend, string trendGraphic, string description)
		{
			this._statusGraphic = statusGraphic;
			this._status = status;
			this._goal = goal;
			this._trend = trend;
			this._trendGraphic = trendGraphic;
			this._description = description;
		}

		// Token: 0x04000460 RID: 1120
		[DataMember(Name = "StatusGraphic", IsRequired = false, EmitDefaultValue = false, Order = 0)]
		private string _statusGraphic;

		// Token: 0x04000461 RID: 1121
		[DataMember(Name = "Status", IsRequired = false, EmitDefaultValue = false, Order = 1)]
		private string _status;

		// Token: 0x04000462 RID: 1122
		[DataMember(Name = "Goal", IsRequired = false, EmitDefaultValue = false, Order = 2)]
		private string _goal;

		// Token: 0x04000463 RID: 1123
		[DataMember(Name = "Trend", IsRequired = false, EmitDefaultValue = false, Order = 3)]
		private string _trend;

		// Token: 0x04000464 RID: 1124
		[DataMember(Name = "TrendGraphic", IsRequired = false, EmitDefaultValue = false, Order = 4)]
		private string _trendGraphic;

		// Token: 0x04000465 RID: 1125
		[DataMember(Name = "Description", IsRequired = false, EmitDefaultValue = false, Order = 5)]
		private string _description;
	}
}
