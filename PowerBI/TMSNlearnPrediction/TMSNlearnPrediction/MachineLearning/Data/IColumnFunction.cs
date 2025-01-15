using System;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200033F RID: 831
	public interface IColumnFunction : ICanSaveModel
	{
		// Token: 0x06001251 RID: 4689
		Delegate GetGetter(IRow input, int icol);
	}
}
