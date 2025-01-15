using System;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000047 RID: 71
	public sealed class ExportLinguisticSchemaResult
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00004DAB File Offset: 0x00002FAB
		public bool Successful { get; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00004DB3 File Offset: 0x00002FB3
		public string LinguisticSchemaYaml { get; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00004DBB File Offset: 0x00002FBB
		public string Message { get; }

		// Token: 0x06000192 RID: 402 RVA: 0x00004DC3 File Offset: 0x00002FC3
		private ExportLinguisticSchemaResult(bool sucessful, string linguisticSchemaYaml, string message)
		{
			this.Successful = sucessful;
			this.LinguisticSchemaYaml = linguisticSchemaYaml;
			this.Message = message;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00004DE0 File Offset: 0x00002FE0
		public static ExportLinguisticSchemaResult Success(string linguisticSchemaYaml, string warningMessage)
		{
			return new ExportLinguisticSchemaResult(true, linguisticSchemaYaml, warningMessage);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00004DEA File Offset: 0x00002FEA
		public static ExportLinguisticSchemaResult Failure(string errorMessage)
		{
			return new ExportLinguisticSchemaResult(false, null, errorMessage);
		}
	}
}
