using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeResultWriter
{
	// Token: 0x02000047 RID: 71
	internal abstract class DsrCalculationContainerWriterBase : DsrObjectWriterBase, ICalculationContainerWriter
	{
		// Token: 0x06000183 RID: 387 RVA: 0x00004C38 File Offset: 0x00002E38
		public CollectionWriter<CalculationAsObjectWriter> BeginCalculationsAsObjects()
		{
			return this.BeginCalculations<CalculationAsObjectWriter>(ref this._calcsAsObjectsWriter, false);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00004C47 File Offset: 0x00002E47
		public CollectionWriter<CalculationAsPropertyWriter> BeginCalculationsAsProperties()
		{
			base.CreateAndBeginCollectionWriter<CalculationAsPropertyWriter>(ref this._calcsAsPropertiesWriter, true);
			return this._calcsAsPropertiesWriter;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00004C5D File Offset: 0x00002E5D
		public CollectionWriter<CalculationAsValueWriter> BeginCalculationsAsValuesArray()
		{
			return this.BeginCalculations<CalculationAsValueWriter>(ref this._calcsAsValuesWriter, false);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00004C6C File Offset: 0x00002E6C
		public CollectionWriter<CalculationMetadataWriter> BeginCalculationsSchema()
		{
			base.Writer.BeginProperty(base.DsrNames.CalcSchema);
			return base.CreateAndBeginChild<CalculationMetadataWriter>(ref this._calcsSchemaWriter);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00004C90 File Offset: 0x00002E90
		public FlagSequenceWriter BeginFlagSequence()
		{
			return base.CreateAndBeginChild<FlagSequenceWriter>(ref this._flagSequenceWriter);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00004C9E File Offset: 0x00002E9E
		private CollectionWriter<T> BeginCalculations<T>(ref CollectionWriter<T> calcsWriter, bool inlineCalcsInCalcContainer) where T : StreamingDsrWriterWrapperBase, ICalculationWriter, new()
		{
			base.Writer.BeginProperty(base.DsrNames.Calculations);
			base.CreateAndBeginCollectionWriter<T>(ref calcsWriter, inlineCalcsInCalcContainer);
			return calcsWriter;
		}

		// Token: 0x040000AF RID: 175
		private CollectionWriter<CalculationAsObjectWriter> _calcsAsObjectsWriter;

		// Token: 0x040000B0 RID: 176
		private CollectionWriter<CalculationAsPropertyWriter> _calcsAsPropertiesWriter;

		// Token: 0x040000B1 RID: 177
		private CollectionWriter<CalculationAsValueWriter> _calcsAsValuesWriter;

		// Token: 0x040000B2 RID: 178
		private CollectionWriter<CalculationMetadataWriter> _calcsSchemaWriter;

		// Token: 0x040000B3 RID: 179
		private FlagSequenceWriter _flagSequenceWriter;
	}
}
