using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200010E RID: 270
	internal class CertificateOptionKindsHelper : OptionsHelper<CertificateOptionKinds>
	{
		// Token: 0x0600149E RID: 5278 RVA: 0x000907A9 File Offset: 0x0008E9A9
		private CertificateOptionKindsHelper()
		{
			base.AddOptionMapping(CertificateOptionKinds.Subject, "SUBJECT");
			base.AddOptionMapping(CertificateOptionKinds.StartDate, "START_DATE");
			base.AddOptionMapping(CertificateOptionKinds.ExpiryDate, "EXPIRY_DATE");
		}

		// Token: 0x04000B82 RID: 2946
		internal static readonly CertificateOptionKindsHelper Instance = new CertificateOptionKindsHelper();
	}
}
