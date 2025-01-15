using System;

namespace NLog.Internal
{
	// Token: 0x02000121 RID: 289
	internal interface IRenderable
	{
		// Token: 0x06000ED1 RID: 3793
		string Render(LogEventInfo logEvent);
	}
}
