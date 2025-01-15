using System;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.Edm.Internal;

namespace Microsoft.DataShaping.Engine
{
	// Token: 0x0200000A RID: 10
	public sealed class EngineDataModel
	{
		// Token: 0x0600002B RID: 43 RVA: 0x000029DD File Offset: 0x00000BDD
		private EngineDataModel()
		{
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000029E5 File Offset: 0x00000BE5
		public EngineDataModel(EntityDataModel model, IConceptualSchema schema)
		{
			this.Model = model;
			this.Schema = schema;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000029FB File Offset: 0x00000BFB
		public EngineDataModel(IConceptualSchema schema)
		{
			this.Schema = schema;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002A0A File Offset: 0x00000C0A
		public EntityDataModel Model { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002A12 File Offset: 0x00000C12
		public IConceptualSchema Schema { get; }

		// Token: 0x04000030 RID: 48
		public static readonly EngineDataModel EmptyInstance = new EngineDataModel();
	}
}
