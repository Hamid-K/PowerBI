using System;
using Microsoft.AnalysisServices.Core;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular.Scripting
{
	// Token: 0x0200019D RID: 413
	internal sealed class AlterJsonCommand : AlterBaseJsonCommand
	{
		// Token: 0x06001988 RID: 6536 RVA: 0x000A9A60 File Offset: 0x000A7C60
		public AlterJsonCommand(Database db)
			: base(JsonCommandType.Alter, "alter", "Alter", false, CopyFlags.ShallowCopy | CopyFlags.IgnoreIdsForChildLinks | CopyFlags.IgnoreInferredProperties | CopyFlags.IgnoreInferredObjects, UpdateOptions.Default, db)
		{
			this.isCommandValid = true;
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x000A9A82 File Offset: 0x000A7C82
		public AlterJsonCommand(NamedMetadataObject obj)
			: base(JsonCommandType.Alter, "alter", "Alter", false, CopyFlags.ShallowCopy | CopyFlags.IgnoreIdsForChildLinks | CopyFlags.IgnoreInferredProperties | CopyFlags.IgnoreInferredObjects, UpdateOptions.Default, obj)
		{
			this.isCommandValid = true;
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x000A9AA4 File Offset: 0x000A7CA4
		internal AlterJsonCommand()
			: base(JsonCommandType.Alter, "alter", "Alter", false, CopyFlags.ShallowCopy | CopyFlags.IgnoreIdsForChildLinks | CopyFlags.IgnoreInferredProperties | CopyFlags.IgnoreInferredObjects, UpdateOptions.Default)
		{
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x000A9ABE File Offset: 0x000A7CBE
		private protected override SerializeOptions GetJsonSerializeOptions()
		{
			return JsonCommand.JsonSerializationOptions.TopLevelObjectOnly;
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x000A9AC5 File Offset: 0x000A7CC5
		private protected override DeserializeOptions GetJsonDeserializeOptions()
		{
			return JsonCommand.JsonDeserializationOptions.Default;
		}
	}
}
