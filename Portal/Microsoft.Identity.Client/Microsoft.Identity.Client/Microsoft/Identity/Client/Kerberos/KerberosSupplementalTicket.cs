using System;
using Microsoft.Identity.Json;

namespace Microsoft.Identity.Client.Kerberos
{
	// Token: 0x02000228 RID: 552
	public class KerberosSupplementalTicket
	{
		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x0600169F RID: 5791 RVA: 0x0004B2FB File Offset: 0x000494FB
		// (set) Token: 0x060016A0 RID: 5792 RVA: 0x0004B303 File Offset: 0x00049503
		[JsonProperty("clientKey")]
		public string ClientKey { get; set; }

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x0004B30C File Offset: 0x0004950C
		// (set) Token: 0x060016A2 RID: 5794 RVA: 0x0004B314 File Offset: 0x00049514
		[JsonProperty("keyType")]
		public KerberosKeyTypes KeyType { get; set; }

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060016A3 RID: 5795 RVA: 0x0004B31D File Offset: 0x0004951D
		// (set) Token: 0x060016A4 RID: 5796 RVA: 0x0004B325 File Offset: 0x00049525
		[JsonProperty("messageBuffer")]
		public string KerberosMessageBuffer { get; set; }

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060016A5 RID: 5797 RVA: 0x0004B32E File Offset: 0x0004952E
		// (set) Token: 0x060016A6 RID: 5798 RVA: 0x0004B336 File Offset: 0x00049536
		[JsonProperty("error")]
		public string ErrorMessage { get; set; }

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060016A7 RID: 5799 RVA: 0x0004B33F File Offset: 0x0004953F
		// (set) Token: 0x060016A8 RID: 5800 RVA: 0x0004B347 File Offset: 0x00049547
		[JsonProperty("realm")]
		public string Realm { get; set; }

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060016A9 RID: 5801 RVA: 0x0004B350 File Offset: 0x00049550
		// (set) Token: 0x060016AA RID: 5802 RVA: 0x0004B358 File Offset: 0x00049558
		[JsonProperty("sn")]
		public string ServicePrincipalName { get; set; }

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060016AB RID: 5803 RVA: 0x0004B361 File Offset: 0x00049561
		// (set) Token: 0x060016AC RID: 5804 RVA: 0x0004B369 File Offset: 0x00049569
		[JsonProperty("cn")]
		public string ClientName { get; set; }

		// Token: 0x060016AD RID: 5805 RVA: 0x0004B372 File Offset: 0x00049572
		public KerberosSupplementalTicket()
		{
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x0004B37A File Offset: 0x0004957A
		public KerberosSupplementalTicket(string errorMessage)
		{
			this.ErrorMessage = errorMessage;
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x0004B389 File Offset: 0x00049589
		public override string ToString()
		{
			return string.Format("[ Realm: {0}, sp: {1}, cn: {2}, KeyType: {3} ]", new object[] { this.Realm, this.ServicePrincipalName, this.ClientName, this.KeyType });
		}
	}
}
