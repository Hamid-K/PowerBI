using System;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000CF9 RID: 3321
	internal static class CubeMeasurePropertiesTableValue
	{
		// Token: 0x04003247 RID: 12871
		public static readonly Keys Columns = Keys.New("MeasurePropertyId", "Id", "Name", "MeasureId");

		// Token: 0x04003248 RID: 12872
		public static readonly TableTypeValue Type = TableTypeValue.New(RecordTypeValue.New(RecordValue.New(CubeMeasurePropertiesTableValue.Columns, new Value[]
		{
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(TypeValue.Text, false)
		})), new TableKey[]
		{
			new TableKey(new int[1], true)
		});

		// Token: 0x04003249 RID: 12873
		public static readonly TableValue Empty = ListValue.Empty.ToTable(CubeMeasurePropertiesTableValue.Type);
	}
}
