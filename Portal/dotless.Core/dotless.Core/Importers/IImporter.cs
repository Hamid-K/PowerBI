using System;
using System.Collections.Generic;
using dotless.Core.Parser;
using dotless.Core.Parser.Tree;

namespace dotless.Core.Importers
{
	// Token: 0x020000B8 RID: 184
	public interface IImporter
	{
		// Token: 0x06000534 RID: 1332
		List<string> GetCurrentPathsClone();

		// Token: 0x06000535 RID: 1333
		ImportAction Import(Import import);

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000536 RID: 1334
		// (set) Token: 0x06000537 RID: 1335
		Func<Parser> Parser { get; set; }

		// Token: 0x06000538 RID: 1336
		string AlterUrl(string url, List<string> pathList);

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000539 RID: 1337
		// (set) Token: 0x0600053A RID: 1338
		string CurrentDirectory { get; set; }

		// Token: 0x0600053B RID: 1339
		IDisposable BeginScope(Import parent);

		// Token: 0x0600053C RID: 1340
		void ResetImports();

		// Token: 0x0600053D RID: 1341
		IEnumerable<string> GetImports();
	}
}
