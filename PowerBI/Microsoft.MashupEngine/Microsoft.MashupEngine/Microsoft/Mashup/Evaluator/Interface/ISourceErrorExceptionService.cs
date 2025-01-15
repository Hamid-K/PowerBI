using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DF3 RID: 7667
	public interface ISourceErrorExceptionService
	{
		// Token: 0x0600BDA8 RID: 48552
		bool TryGetSourceErrorException(IPartitionKey partitionKey, IError error, out ValueException2 exception);
	}
}
