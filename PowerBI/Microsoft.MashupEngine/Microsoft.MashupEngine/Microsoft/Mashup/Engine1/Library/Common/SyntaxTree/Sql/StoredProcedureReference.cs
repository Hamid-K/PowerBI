using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011D7 RID: 4567
	internal sealed class StoredProcedureReference : FunctionReferenceBase
	{
		// Token: 0x0600788C RID: 30860 RVA: 0x001A161B File Offset: 0x0019F81B
		public StoredProcedureReference(Alias schema, Alias name)
		{
			this.schema = schema;
			this.name = name;
		}

		// Token: 0x0600788D RID: 30861 RVA: 0x001A1631 File Offset: 0x0019F831
		public override void WriteCreateScript(ScriptWriter writer)
		{
			writer.WriteIdentifier(this.schema, this.name);
			writer.WriteSpace();
			base.WriteParameters(writer);
		}

		// Token: 0x0600788E RID: 30862 RVA: 0x001A1652 File Offset: 0x0019F852
		public void WriteIdentifier(ScriptWriter writer)
		{
			writer.WriteIdentifier(this.schema, this.name);
		}

		// Token: 0x040041B7 RID: 16823
		private Alias name;

		// Token: 0x040041B8 RID: 16824
		private Alias schema;
	}
}
