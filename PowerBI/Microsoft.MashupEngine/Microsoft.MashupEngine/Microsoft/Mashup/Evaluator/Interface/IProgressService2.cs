using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E25 RID: 7717
	public interface IProgressService2 : IProgressService
	{
		// Token: 0x0600BE27 RID: 48679
		void RecordRowCount(long rowCount, long errorRowCount);
	}
}
