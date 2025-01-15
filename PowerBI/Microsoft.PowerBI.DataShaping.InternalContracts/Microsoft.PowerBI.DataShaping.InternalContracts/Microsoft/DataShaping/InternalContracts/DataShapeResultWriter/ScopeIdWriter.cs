using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeResultWriter
{
	// Token: 0x02000059 RID: 89
	internal sealed class ScopeIdWriter : DsrObjectWriterBase
	{
		// Token: 0x060001C1 RID: 449 RVA: 0x000054DD File Offset: 0x000036DD
		internal CollectionWriter<ScopeValueWriter> BeginScopeValues()
		{
			base.Writer.BeginProperty(base.DsrNames.ScopeValues);
			base.CreateAndBeginChild<ScopeValueWriter>(ref this._scopesWriter);
			return this._scopesWriter;
		}

		// Token: 0x040000E2 RID: 226
		private CollectionWriter<ScopeValueWriter> _scopesWriter;
	}
}
