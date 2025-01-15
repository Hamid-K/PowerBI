using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000123 RID: 291
	internal class ExecuteAsOptionHelper : OptionsHelper<ExecuteAsOption>
	{
		// Token: 0x060014B6 RID: 5302 RVA: 0x00090ACF File Offset: 0x0008ECCF
		private ExecuteAsOptionHelper()
		{
			base.AddOptionMapping(ExecuteAsOption.Caller, "CALLER");
			base.AddOptionMapping(ExecuteAsOption.Self, "SELF");
			base.AddOptionMapping(ExecuteAsOption.Owner, "OWNER");
		}

		// Token: 0x0400112D RID: 4397
		internal static readonly ExecuteAsOptionHelper Instance = new ExecuteAsOptionHelper();
	}
}
