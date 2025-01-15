using System;
using ParquetSharp;

namespace Microsoft.Mashup.Engine1.Library.Parquet.Interop
{
	// Token: 0x02001FEB RID: 8171
	internal interface IParquetColumnBuffer<T>
	{
		// Token: 0x17002D03 RID: 11523
		// (get) Token: 0x060110DE RID: 69854
		short MaxDefinitionLevel { get; }

		// Token: 0x17002D04 RID: 11524
		// (get) Token: 0x060110DF RID: 69855
		short MaxRepetitionLevel { get; }

		// Token: 0x17002D05 RID: 11525
		// (get) Token: 0x060110E0 RID: 69856
		T[] Values { get; }

		// Token: 0x17002D06 RID: 11526
		// (get) Token: 0x060110E1 RID: 69857
		// (set) Token: 0x060110E2 RID: 69858
		int ValuesOffset { get; set; }

		// Token: 0x17002D07 RID: 11527
		// (get) Token: 0x060110E3 RID: 69859
		// (set) Token: 0x060110E4 RID: 69860
		int ValuesCount { get; set; }

		// Token: 0x17002D08 RID: 11528
		// (get) Token: 0x060110E5 RID: 69861
		short[] DefinitionLevels { get; }

		// Token: 0x17002D09 RID: 11529
		// (get) Token: 0x060110E6 RID: 69862
		short[] RepetitionLevels { get; }

		// Token: 0x17002D0A RID: 11530
		// (get) Token: 0x060110E7 RID: 69863
		// (set) Token: 0x060110E8 RID: 69864
		int LevelsOffset { get; set; }

		// Token: 0x17002D0B RID: 11531
		// (get) Token: 0x060110E9 RID: 69865
		// (set) Token: 0x060110EA RID: 69866
		int LevelsCount { get; set; }

		// Token: 0x060110EB RID: 69867
		int Read(ColumnReader reader, int batchSize = 2147483647);

		// Token: 0x060110EC RID: 69868
		void Write(ColumnWriter writer);

		// Token: 0x060110ED RID: 69869
		void Clear();
	}
}
