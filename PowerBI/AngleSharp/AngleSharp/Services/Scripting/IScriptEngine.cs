using System;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Network;

namespace AngleSharp.Services.Scripting
{
	// Token: 0x0200003D RID: 61
	public interface IScriptEngine
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000150 RID: 336
		string Type { get; }

		// Token: 0x06000151 RID: 337
		Task EvaluateScriptAsync(IResponse response, ScriptOptions options, CancellationToken cancel);
	}
}
