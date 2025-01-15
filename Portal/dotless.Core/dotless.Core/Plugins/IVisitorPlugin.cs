using System;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Tree;

namespace dotless.Core.Plugins
{
	// Token: 0x0200001D RID: 29
	public interface IVisitorPlugin : IPlugin
	{
		// Token: 0x060000A9 RID: 169
		Root Apply(Root tree);

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000AA RID: 170
		VisitorPluginType AppliesTo { get; }

		// Token: 0x060000AB RID: 171
		void OnPreVisiting(Env env);

		// Token: 0x060000AC RID: 172
		void OnPostVisiting(Env env);
	}
}
