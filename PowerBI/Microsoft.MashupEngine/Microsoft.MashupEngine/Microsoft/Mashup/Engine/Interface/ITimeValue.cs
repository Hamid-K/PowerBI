using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000FB RID: 251
	public interface ITimeValue : IValue
	{
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060003C7 RID: 967
		TimeSpan AsClrTimeSpan { get; }
	}
}
