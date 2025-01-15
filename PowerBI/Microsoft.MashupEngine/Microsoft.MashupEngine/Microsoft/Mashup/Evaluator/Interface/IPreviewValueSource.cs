using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DF1 RID: 7665
	public interface IPreviewValueSource : IDisposable
	{
		// Token: 0x17002EA6 RID: 11942
		// (get) Token: 0x0600BDA3 RID: 48547
		bool IsComplete { get; }

		// Token: 0x17002EA7 RID: 11943
		// (get) Token: 0x0600BDA4 RID: 48548
		ITableSource TableSource { get; }

		// Token: 0x17002EA8 RID: 11944
		// (get) Token: 0x0600BDA5 RID: 48549
		string SmallValue { get; }

		// Token: 0x17002EA9 RID: 11945
		// (get) Token: 0x0600BDA6 RID: 48550
		string Value { get; }
	}
}
