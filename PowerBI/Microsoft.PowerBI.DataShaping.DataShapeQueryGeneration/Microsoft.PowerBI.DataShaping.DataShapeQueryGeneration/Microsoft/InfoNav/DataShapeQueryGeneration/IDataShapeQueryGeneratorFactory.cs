using System;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200008F RID: 143
	internal interface IDataShapeQueryGeneratorFactory
	{
		// Token: 0x0600058E RID: 1422
		IDataShapeGenerator CreateDataShapeGenerator(DataShapeGenerationContext context);
	}
}
