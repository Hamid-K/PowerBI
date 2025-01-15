using System;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D15 RID: 3349
	internal static class CubeParametersTableValue
	{
		// Token: 0x04003298 RID: 12952
		public const string DataColumnName = "Data";

		// Token: 0x04003299 RID: 12953
		public static readonly TextValue IsOptional = TextValue.New("IsOptional");

		// Token: 0x0400329A RID: 12954
		public static readonly Keys Columns = Keys.New("Id", "Name", CubeParametersTableValue.IsOptional.AsString, "Data");

		// Token: 0x0400329B RID: 12955
		public static readonly TableTypeValue Type = TableTypeValue.New(RecordTypeValue.New(RecordValue.New(CubeParametersTableValue.Columns, new Value[]
		{
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(TypeValue.Logical, false),
			RecordTypeAlgebra.NewField(TypeValue.Function, false)
		})), new TableKey[]
		{
			new TableKey(new int[1], true)
		});

		// Token: 0x0400329C RID: 12956
		public static readonly TableValue Empty = ListValue.Empty.ToTable(CubeParametersTableValue.Type);
	}
}
