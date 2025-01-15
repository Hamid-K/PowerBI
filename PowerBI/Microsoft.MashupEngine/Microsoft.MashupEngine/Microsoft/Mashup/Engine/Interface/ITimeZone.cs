using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200003A RID: 58
	public interface ITimeZone
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000133 RID: 307
		string Name { get; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000134 RID: 308
		TimeZoneInfo Value { get; }
	}
}
