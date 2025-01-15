using System;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200002A RID: 42
	public interface ITransposeSchema : ISchema
	{
		// Token: 0x060000FA RID: 250
		VectorType GetSlotType(int col);
	}
}
