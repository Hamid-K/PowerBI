using System;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x0200004E RID: 78
	public sealed class ValidateLinguisticSchemaResult
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00004EA4 File Offset: 0x000030A4
		public bool Successful { get; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00004EAC File Offset: 0x000030AC
		public string LinguisticSchemaJson { get; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00004EB4 File Offset: 0x000030B4
		public string Message { get; }

		// Token: 0x060001B9 RID: 441 RVA: 0x00004EBC File Offset: 0x000030BC
		private ValidateLinguisticSchemaResult(bool sucessful, string linguisticSchemaJson, string message)
		{
			this.Successful = sucessful;
			this.LinguisticSchemaJson = linguisticSchemaJson;
			this.Message = message;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00004ED9 File Offset: 0x000030D9
		public static ValidateLinguisticSchemaResult Success(string linguisticSchemaJson, string warningMessage = null)
		{
			return new ValidateLinguisticSchemaResult(true, linguisticSchemaJson, warningMessage);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00004EE3 File Offset: 0x000030E3
		public static ValidateLinguisticSchemaResult Failure(string errorMessage)
		{
			return new ValidateLinguisticSchemaResult(false, null, errorMessage);
		}
	}
}
