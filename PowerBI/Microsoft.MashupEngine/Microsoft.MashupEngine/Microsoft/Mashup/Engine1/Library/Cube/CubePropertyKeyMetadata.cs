using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D19 RID: 3353
	internal static class CubePropertyKeyMetadata
	{
		// Token: 0x06005A70 RID: 23152 RVA: 0x0013BF9F File Offset: 0x0013A19F
		public static TypeValue AddColumnMetadata(this TypeValue columnType)
		{
			return columnType.NewMeta(CubePropertyKeyMetadata.hasPropertyKeysMeta).AsType;
		}

		// Token: 0x040032A2 RID: 12962
		private static readonly RecordValue hasPropertyKeysMeta = RecordValue.New(Keys.New("Cube.HasPropertyKeys"), new Value[] { LogicalValue.True });
	}
}
