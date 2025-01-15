using System;

namespace NLog.Targets
{
	// Token: 0x02000040 RID: 64
	[Obsolete("Use NLog.IJsonConverter class instead. Marked obsolete on NLog 4.5")]
	public interface IJsonSerializer
	{
		// Token: 0x06000696 RID: 1686
		string SerializeObject(object value);
	}
}
