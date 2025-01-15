using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200016F RID: 367
	public sealed class QueryableState
	{
		// Token: 0x0600098A RID: 2442 RVA: 0x00013624 File Offset: 0x00011824
		internal QueryableState(ClientConceptualQueryableState state, string errorMessage = null)
		{
			this.Queryable = state;
			this.ErrorMessage = errorMessage;
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x0001363A File Offset: 0x0001183A
		// (set) Token: 0x0600098C RID: 2444 RVA: 0x00013642 File Offset: 0x00011842
		internal ClientConceptualQueryableState Queryable { get; private set; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x0001364B File Offset: 0x0001184B
		// (set) Token: 0x0600098E RID: 2446 RVA: 0x00013653 File Offset: 0x00011853
		internal string ErrorMessage { get; private set; }
	}
}
