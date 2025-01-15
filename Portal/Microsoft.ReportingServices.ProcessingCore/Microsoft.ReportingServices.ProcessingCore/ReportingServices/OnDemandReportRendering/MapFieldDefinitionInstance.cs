using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001B3 RID: 435
	public sealed class MapFieldDefinitionInstance : BaseInstance
	{
		// Token: 0x0600114D RID: 4429 RVA: 0x000485F7 File Offset: 0x000467F7
		internal MapFieldDefinitionInstance(MapFieldDefinition defObject)
			: base(defObject.MapDef.ReportScope)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x00048611 File Offset: 0x00046811
		protected override void ResetInstanceCache()
		{
		}

		// Token: 0x0400082B RID: 2091
		private MapFieldDefinition m_defObject;
	}
}
