using System;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000060 RID: 96
	public sealed class SchemaOptions
	{
		// Token: 0x0600022E RID: 558 RVA: 0x000062FF File Offset: 0x000044FF
		public SchemaOptions(TranslationsBehavior translationsBehavior)
		{
			this.TranslationsBehavior = translationsBehavior;
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000630E File Offset: 0x0000450E
		// (set) Token: 0x06000230 RID: 560 RVA: 0x00006316 File Offset: 0x00004516
		public TranslationsBehavior TranslationsBehavior { get; set; }
	}
}
