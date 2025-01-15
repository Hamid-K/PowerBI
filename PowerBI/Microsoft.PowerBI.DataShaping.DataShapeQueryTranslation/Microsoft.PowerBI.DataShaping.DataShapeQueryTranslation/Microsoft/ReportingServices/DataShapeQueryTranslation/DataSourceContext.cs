using System;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.Edm.Internal;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation
{
	// Token: 0x02000056 RID: 86
	internal sealed class DataSourceContext
	{
		// Token: 0x060003F7 RID: 1015 RVA: 0x0000D584 File Offset: 0x0000B784
		internal DataSourceContext(string dataSourceName, EntityDataModel model, IFederatedConceptualSchema federatedConceptualSchema)
		{
			this.DataSourceName = dataSourceName;
			this.Model = model;
			this.FederatedConceptualSchema = federatedConceptualSchema;
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000D5A1 File Offset: 0x0000B7A1
		public string DataSourceName { get; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0000D5A9 File Offset: 0x0000B7A9
		public EntityDataModel Model { get; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0000D5B1 File Offset: 0x0000B7B1
		public IFederatedConceptualSchema FederatedConceptualSchema { get; }
	}
}
