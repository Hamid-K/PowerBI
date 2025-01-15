using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E01 RID: 7681
	public interface IGetStackFrameExtendedInfo
	{
		// Token: 0x0600BDB9 RID: 48569
		IRecordValue GetStackFrameExtendedInfo(IEngine engine, IValue frameLocation, IValue sectionName);
	}
}
