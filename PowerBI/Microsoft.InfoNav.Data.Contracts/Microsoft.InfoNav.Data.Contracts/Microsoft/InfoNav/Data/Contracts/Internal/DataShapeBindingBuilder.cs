using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200019D RID: 413
	public class DataShapeBindingBuilder : DataShapeBindingBuilder<SemanticQueryDataShapeCommandBuilder>
	{
		// Token: 0x06000B32 RID: 2866 RVA: 0x000160D1 File Offset: 0x000142D1
		public DataShapeBindingBuilder(int? version = null)
			: this(null, version)
		{
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x000160DB File Offset: 0x000142DB
		public DataShapeBindingBuilder(SemanticQueryDataShapeCommandBuilder parent, int? version = null)
			: base(parent, version)
		{
		}
	}
}
