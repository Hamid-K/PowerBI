using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000079 RID: 121
	public interface IRowsetManager
	{
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060004D9 RID: 1241
		ConnectionCollection Connections { get; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060004DA RID: 1242
		RowsetCollection Rowsets { get; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060004DB RID: 1243
		RecordBindingCollection RecordBindings { get; }

		// Token: 0x060004DC RID: 1244
		bool TryGetRecordBinding(string rowsetName, ConnectionManager connectionManager, out RecordBinding recordBinding);
	}
}
