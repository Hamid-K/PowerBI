using System;
using Microsoft.AnalysisServices.Core;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular.Scripting
{
	// Token: 0x020001A2 RID: 418
	internal sealed class CreateOrReplaceJsonCommand : AlterBaseJsonCommand
	{
		// Token: 0x060019C4 RID: 6596 RVA: 0x000AB021 File Offset: 0x000A9221
		public CreateOrReplaceJsonCommand(Database db)
			: base(JsonCommandType.CreateOrReplace, "createOrReplace", "CreateOrReplace", true, CopyFlags.UserCopy, UpdateOptions.ExpandFull, db)
		{
			this.isCommandValid = true;
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x000AB043 File Offset: 0x000A9243
		public CreateOrReplaceJsonCommand(NamedMetadataObject obj)
			: base(JsonCommandType.CreateOrReplace, "createOrReplace", "CreateOrReplace", true, CopyFlags.UserCopy, UpdateOptions.ExpandFull, obj)
		{
			this.isCommandValid = true;
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x000AB065 File Offset: 0x000A9265
		internal CreateOrReplaceJsonCommand()
			: base(JsonCommandType.CreateOrReplace, "createOrReplace", "CreateOrReplace", true, CopyFlags.UserCopy, UpdateOptions.ExpandFull)
		{
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x000AB07F File Offset: 0x000A927F
		private protected override SerializeOptions GetJsonSerializeOptions()
		{
			return JsonCommand.JsonSerializationOptions.ObjectWithDescendants;
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x000AB086 File Offset: 0x000A9286
		private protected override DeserializeOptions GetJsonDeserializeOptions()
		{
			return JsonCommand.JsonDeserializationOptions.Default;
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x000AB08D File Offset: 0x000A928D
		private protected override bool CanAffectedDatabaseBeMissing()
		{
			return base.ObjectType == ObjectType.Database;
		}
	}
}
