using System;
using System.Threading.Tasks;
using Microsoft.Lucia.Core;
using Microsoft.PowerBI.Lucia.Interpret;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x02000067 RID: 103
	internal interface IInterpretHandler
	{
		// Token: 0x060002CE RID: 718
		Task<InterpretResponse<DesktopResultContext>> InterpretAsync(InterpretRequest<DesktopRequestContext> interpretRequest, IDatabaseContext databaseContext, IDataIndexContainer dataIndexContainer);
	}
}
