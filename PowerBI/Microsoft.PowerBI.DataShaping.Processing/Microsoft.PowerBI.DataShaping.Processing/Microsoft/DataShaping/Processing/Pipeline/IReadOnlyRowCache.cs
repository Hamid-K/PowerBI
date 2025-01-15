using System;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.Pipeline
{
	// Token: 0x0200009C RID: 156
	internal interface IReadOnlyRowCache
	{
		// Token: 0x17000167 RID: 359
		IDataRow this[int index] { get; }
	}
}
