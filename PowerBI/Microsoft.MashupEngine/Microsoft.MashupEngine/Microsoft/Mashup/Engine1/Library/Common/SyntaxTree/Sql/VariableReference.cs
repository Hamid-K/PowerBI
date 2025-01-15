using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x02001225 RID: 4645
	internal sealed class VariableReference : ScalarExpression
	{
		// Token: 0x06007ABB RID: 31419 RVA: 0x001A795D File Offset: 0x001A5B5D
		public VariableReference(Alias name)
		{
			this.name = name;
		}

		// Token: 0x1700219D RID: 8605
		// (get) Token: 0x06007ABC RID: 31420 RVA: 0x00002105 File Offset: 0x00000305
		public override int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06007ABD RID: 31421 RVA: 0x001A796C File Offset: 0x001A5B6C
		public override void WriteCreateScript(ScriptWriter writer)
		{
			writer.WriteVariable(this.name);
		}

		// Token: 0x04004404 RID: 17412
		private readonly Alias name;
	}
}
