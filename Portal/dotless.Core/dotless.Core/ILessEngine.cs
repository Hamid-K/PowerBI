using System;
using System.Collections.Generic;

namespace dotless.Core
{
	// Token: 0x02000006 RID: 6
	public interface ILessEngine
	{
		// Token: 0x06000024 RID: 36
		string TransformToCss(string source, string fileName);

		// Token: 0x06000025 RID: 37
		void ResetImports();

		// Token: 0x06000026 RID: 38
		IEnumerable<string> GetImports();

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000027 RID: 39
		bool LastTransformationSuccessful { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000028 RID: 40
		// (set) Token: 0x06000029 RID: 41
		string CurrentDirectory { get; set; }
	}
}
