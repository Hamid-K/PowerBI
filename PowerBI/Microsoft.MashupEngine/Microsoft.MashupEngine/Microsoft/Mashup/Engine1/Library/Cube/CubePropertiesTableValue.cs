using System;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D18 RID: 3352
	internal static class CubePropertiesTableValue
	{
		// Token: 0x0400329F RID: 12959
		public static readonly Keys Columns = Keys.New(new string[] { "DimensionAttributeId", "Id", "Name", "Kind", "DimensionId" });

		// Token: 0x040032A0 RID: 12960
		public static readonly TableTypeValue Type = TableTypeValue.New(RecordTypeValue.New(RecordValue.New(CubePropertiesTableValue.Columns, new Value[]
		{
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(TypeValue.Text, false)
		})), new TableKey[]
		{
			new TableKey(new int[] { 0, 1 }, true)
		});

		// Token: 0x040032A1 RID: 12961
		public static readonly TableValue Empty = ListValue.Empty.ToTable(CubePropertiesTableValue.Type);
	}
}
