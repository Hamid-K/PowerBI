using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011D8 RID: 4568
	internal class BuiltInFunctionReference : FunctionReferenceBase
	{
		// Token: 0x0600788F RID: 30863 RVA: 0x001A1666 File Offset: 0x0019F866
		public BuiltInFunctionReference(ConstantSqlString name)
			: this(BuiltInFunctionReference.EmptyVerbatim, name)
		{
		}

		// Token: 0x06007890 RID: 30864 RVA: 0x001A1674 File Offset: 0x0019F874
		public BuiltInFunctionReference(ConstantSqlString verbatimPrefix, ConstantSqlString name)
		{
			this.verbatimPrefix = verbatimPrefix;
			this.name = name;
		}

		// Token: 0x170020FF RID: 8447
		// (get) Token: 0x06007891 RID: 30865 RVA: 0x001A168A File Offset: 0x0019F88A
		public ConstantSqlString Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17002100 RID: 8448
		// (get) Token: 0x06007892 RID: 30866 RVA: 0x001A1692 File Offset: 0x0019F892
		public ConstantSqlString VerbatimPrefix
		{
			get
			{
				return this.verbatimPrefix;
			}
		}

		// Token: 0x06007893 RID: 30867 RVA: 0x001A169C File Offset: 0x0019F89C
		public override void WriteCreateScript(ScriptWriter writer)
		{
			writer.Write(this.name);
			writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
			if (!this.verbatimPrefix.Equals(BuiltInFunctionReference.EmptyVerbatim))
			{
				writer.Write(this.verbatimPrefix);
				writer.WriteSpaceBeforeAndAfter(SqlLanguageStrings.FromSqlString);
			}
			base.WriteParameters(writer);
			writer.Write(SqlLanguageSymbols.CloseParenthesisSqlString);
		}

		// Token: 0x040041B9 RID: 16825
		private static readonly ConstantSqlString EmptyVerbatim = new ConstantSqlString(string.Empty);

		// Token: 0x040041BA RID: 16826
		private readonly ConstantSqlString name;

		// Token: 0x040041BB RID: 16827
		private readonly ConstantSqlString verbatimPrefix;
	}
}
