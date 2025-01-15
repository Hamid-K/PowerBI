using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000176 RID: 374
	internal sealed class GetUserOofSettingsResponse : ServiceResponse
	{
		// Token: 0x060010E4 RID: 4324 RVA: 0x000318EF File Offset: 0x000308EF
		internal GetUserOofSettingsResponse()
		{
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x060010E5 RID: 4325 RVA: 0x000318F7 File Offset: 0x000308F7
		// (set) Token: 0x060010E6 RID: 4326 RVA: 0x000318FF File Offset: 0x000308FF
		public OofSettings OofSettings
		{
			get
			{
				return this.oofSettings;
			}
			internal set
			{
				this.oofSettings = value;
			}
		}

		// Token: 0x040009D0 RID: 2512
		private OofSettings oofSettings;
	}
}
