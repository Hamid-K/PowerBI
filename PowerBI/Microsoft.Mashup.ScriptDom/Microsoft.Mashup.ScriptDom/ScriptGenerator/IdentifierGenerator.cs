using System;

namespace Microsoft.Mashup.ScriptDom.ScriptGenerator
{
	// Token: 0x02000197 RID: 407
	internal sealed class IdentifierGenerator : TokenGenerator
	{
		// Token: 0x06002192 RID: 8594 RVA: 0x0015E59C File Offset: 0x0015C79C
		public IdentifierGenerator(string identifier)
			: this(identifier, false)
		{
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x0015E5A6 File Offset: 0x0015C7A6
		public IdentifierGenerator(string identifier, bool appendSpace)
			: base(appendSpace)
		{
			this._identifier = identifier;
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x0015E5B6 File Offset: 0x0015C7B6
		public override void Generate(ScriptWriter writer)
		{
			writer.AddIdentifierWithCasing(this._identifier);
			base.AppendSpaceIfRequired(writer);
		}

		// Token: 0x040019B8 RID: 6584
		private string _identifier;
	}
}
