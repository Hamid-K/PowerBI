using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeResultWriter
{
	// Token: 0x02000053 RID: 83
	internal interface ICalculationContainerWriter
	{
		// Token: 0x060001AC RID: 428
		CollectionWriter<CalculationAsObjectWriter> BeginCalculationsAsObjects();

		// Token: 0x060001AD RID: 429
		CollectionWriter<CalculationAsPropertyWriter> BeginCalculationsAsProperties();

		// Token: 0x060001AE RID: 430
		CollectionWriter<CalculationAsValueWriter> BeginCalculationsAsValuesArray();

		// Token: 0x060001AF RID: 431
		CollectionWriter<CalculationMetadataWriter> BeginCalculationsSchema();

		// Token: 0x060001B0 RID: 432
		FlagSequenceWriter BeginFlagSequence();
	}
}
