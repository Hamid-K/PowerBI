using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200008D RID: 141
	internal class PartnerDbOptionsHelper : OptionsHelper<PartnerDatabaseOptionKind>
	{
		// Token: 0x060002A5 RID: 677 RVA: 0x0000B764 File Offset: 0x00009964
		private PartnerDbOptionsHelper()
		{
			base.AddOptionMapping(PartnerDatabaseOptionKind.Failover, "FAILOVER");
			base.AddOptionMapping(PartnerDatabaseOptionKind.ForceServiceAllowDataLoss, "FORCE_SERVICE_ALLOW_DATA_LOSS");
			base.AddOptionMapping(PartnerDatabaseOptionKind.Off, TSqlTokenType.Off);
			base.AddOptionMapping(PartnerDatabaseOptionKind.Resume, "RESUME");
			base.AddOptionMapping(PartnerDatabaseOptionKind.Suspend, "SUSPEND");
			base.AddOptionMapping(PartnerDatabaseOptionKind.Timeout, "TIMEOUT");
		}

		// Token: 0x04000385 RID: 901
		public static readonly PartnerDbOptionsHelper Instance = new PartnerDbOptionsHelper();
	}
}
