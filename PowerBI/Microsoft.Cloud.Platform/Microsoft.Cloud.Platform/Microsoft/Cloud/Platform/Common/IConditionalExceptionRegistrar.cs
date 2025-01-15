using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x02000514 RID: 1300
	public interface IConditionalExceptionRegistrar
	{
		// Token: 0x0600285B RID: 10331
		void RegisterConditionalException(Exception exception, TriggeringMethod step, Activity activity, EntryPointIdentifier identifier, ConditionalExceptionRegistrationOptions options);
	}
}
