using System;
using AngleSharp.Services.Scripting;

namespace AngleSharp.Services
{
	// Token: 0x02000036 RID: 54
	public interface IScriptingProvider
	{
		// Token: 0x0600013B RID: 315
		IScriptEngine GetEngine(string mimeType);
	}
}
