using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeResultWriter
{
	// Token: 0x02000052 RID: 82
	internal sealed class GroupWriter : DsrObjectWriterBase
	{
		// Token: 0x060001AA RID: 426 RVA: 0x00005386 File Offset: 0x00003586
		internal ScopeIdWriter BeginScopeId()
		{
			base.Writer.BeginProperty(base.DsrNames.ScopeId);
			base.CreateAndBeginChild<ScopeIdWriter>(ref this._scopeIdWriter);
			return this._scopeIdWriter;
		}

		// Token: 0x040000D9 RID: 217
		private ScopeIdWriter _scopeIdWriter;
	}
}
