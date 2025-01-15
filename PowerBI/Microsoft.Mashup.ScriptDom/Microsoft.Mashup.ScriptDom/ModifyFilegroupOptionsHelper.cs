using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200015B RID: 347
	internal class ModifyFilegroupOptionsHelper : OptionsHelper<ModifyFileGroupOption>
	{
		// Token: 0x06002106 RID: 8454 RVA: 0x0015C4FF File Offset: 0x0015A6FF
		private ModifyFilegroupOptionsHelper()
		{
			base.AddOptionMapping(ModifyFileGroupOption.ReadOnly, "READ_ONLY");
			base.AddOptionMapping(ModifyFileGroupOption.ReadOnlyOld, "READONLY");
			base.AddOptionMapping(ModifyFileGroupOption.ReadWrite, "READ_WRITE");
			base.AddOptionMapping(ModifyFileGroupOption.ReadWriteOld, "READWRITE");
		}

		// Token: 0x04001892 RID: 6290
		internal static readonly ModifyFilegroupOptionsHelper Instance = new ModifyFilegroupOptionsHelper();
	}
}
