using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000126 RID: 294
	[Serializable]
	internal class RestoreStatementKindsHelper : OptionsHelper<RestoreStatementKind>
	{
		// Token: 0x060014B8 RID: 5304 RVA: 0x00090B08 File Offset: 0x0008ED08
		private RestoreStatementKindsHelper()
		{
			base.AddOptionMapping(RestoreStatementKind.FileListOnly, "FILELISTONLY");
			base.AddOptionMapping(RestoreStatementKind.VerifyOnly, "VERIFYONLY");
			base.AddOptionMapping(RestoreStatementKind.LabelOnly, "LABELONLY");
			base.AddOptionMapping(RestoreStatementKind.RewindOnly, "REWINDONLY");
			base.AddOptionMapping(RestoreStatementKind.HeaderOnly, "HEADERONLY");
		}

		// Token: 0x0400113E RID: 4414
		internal static readonly RestoreStatementKindsHelper Instance = new RestoreStatementKindsHelper();
	}
}
