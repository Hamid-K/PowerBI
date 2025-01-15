using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000039 RID: 57
	public interface ITimeZoneService
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000131 RID: 305
		ITimeZone DefaultTimeZone { get; }

		// Token: 0x06000132 RID: 306
		bool TryGetTimeZone(string name, out ITimeZone timeZone);
	}
}
