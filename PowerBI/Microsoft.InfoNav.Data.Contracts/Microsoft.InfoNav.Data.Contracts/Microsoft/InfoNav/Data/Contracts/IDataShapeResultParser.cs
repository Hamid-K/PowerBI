using System;
using System.IO;
using Microsoft.InfoNav.Data.Contracts.DataShapeResult;

namespace Microsoft.InfoNav.Data.Contracts
{
	// Token: 0x02000081 RID: 129
	public interface IDataShapeResultParser
	{
		// Token: 0x060002F2 RID: 754
		DataShapeResult Parse(Stream result);
	}
}
