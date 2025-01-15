using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000062 RID: 98
	public interface IRelationship
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600019D RID: 413
		int KeyColumnCount { get; }

		// Token: 0x0600019E RID: 414
		int KeyColumn(int index);

		// Token: 0x0600019F RID: 415
		IColumnIdentity OtherKeyColumn(int index);
	}
}
