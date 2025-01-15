using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000EE RID: 238
	public interface IRecordValue : IValue
	{
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000397 RID: 919
		IKeys Keys { get; }

		// Token: 0x1700016D RID: 365
		IValue this[int index] { get; }

		// Token: 0x1700016E RID: 366
		IValue this[string index] { get; }

		// Token: 0x0600039A RID: 922
		bool TryGetValue(string name, out IValue value);

		// Token: 0x0600039B RID: 923
		IValueReference2 GetReference(int index);
	}
}
