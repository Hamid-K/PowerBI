using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x0200172C RID: 5932
	internal class ExpressionBuilder
	{
		// Token: 0x060096D6 RID: 38614 RVA: 0x001F46E5 File Offset: 0x001F28E5
		public ExpressionBuilder(IDocument document, RecordValue environment, CompileOptions options)
		{
			this.document = document;
			this.environment = environment;
			this.compiler = new Compiler(options);
		}

		// Token: 0x060096D7 RID: 38615 RVA: 0x001F4708 File Offset: 0x001F2908
		public Module Build()
		{
			DocumentKind kind = this.document.Kind;
			if (kind == DocumentKind.Section)
			{
				return this.compiler.Compile((ISectionDocument)this.document, this.environment);
			}
			if (kind == DocumentKind.Expression)
			{
				return this.compiler.Compile((IExpressionDocument)this.document, this.environment);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x04005026 RID: 20518
		private IDocument document;

		// Token: 0x04005027 RID: 20519
		private RecordValue environment;

		// Token: 0x04005028 RID: 20520
		private Compiler compiler;
	}
}
