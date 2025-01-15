using System;

namespace Microsoft.Mashup.ScriptDom.ScriptGenerator
{
	// Token: 0x02000190 RID: 400
	internal class EmptyGenerator : TokenGenerator
	{
		// Token: 0x06002168 RID: 8552 RVA: 0x0015DDF7 File Offset: 0x0015BFF7
		public EmptyGenerator()
			: base(false)
		{
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x0015DE00 File Offset: 0x0015C000
		public override void Generate(ScriptWriter writer)
		{
		}
	}
}
