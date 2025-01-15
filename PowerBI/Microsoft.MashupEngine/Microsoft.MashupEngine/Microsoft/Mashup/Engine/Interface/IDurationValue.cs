using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000F7 RID: 247
	public interface IDurationValue : IValue
	{
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060003C3 RID: 963
		TimeSpan AsTimeSpan { get; }
	}
}
