using System;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Dracula
{
	// Token: 0x02000427 RID: 1063
	public interface IMultiCountTable : ICanSaveModel
	{
		// Token: 0x06001618 RID: 5656
		ICountTable GetCountTable(int iCol, int iSlot);
	}
}
