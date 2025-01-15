using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000FA RID: 250
	public interface IDateTimeZone : IValue
	{
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060003C6 RID: 966
		DateTimeOffset AsClrDateTimeOffset { get; }
	}
}
