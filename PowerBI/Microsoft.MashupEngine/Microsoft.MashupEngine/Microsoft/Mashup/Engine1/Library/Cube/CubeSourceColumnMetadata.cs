using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D1A RID: 3354
	internal static class CubeSourceColumnMetadata
	{
		// Token: 0x06005A72 RID: 23154 RVA: 0x0013BFD8 File Offset: 0x0013A1D8
		public static TypeValue AddColumnMetadata(TypeValue columnType, string columnName)
		{
			RecordValue recordValue = RecordValue.New(CubeSourceColumnMetadata.sourceColumnKeys, new Value[] { TextValue.New(columnName) });
			return BinaryOperator.AddMeta.Invoke(columnType, recordValue).AsType;
		}

		// Token: 0x040032A3 RID: 12963
		private static readonly Keys sourceColumnKeys = Keys.New("Cube.SourceColumn");
	}
}
