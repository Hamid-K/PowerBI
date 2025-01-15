using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x0200001E RID: 30
	public sealed class DatabaseRequestActivity : SingletonActivityType<DatabaseRequestActivity>
	{
		// Token: 0x0600009F RID: 159 RVA: 0x00003DAC File Offset: 0x00001FAC
		public DatabaseRequestActivity()
			: base("TSQL")
		{
		}
	}
}
