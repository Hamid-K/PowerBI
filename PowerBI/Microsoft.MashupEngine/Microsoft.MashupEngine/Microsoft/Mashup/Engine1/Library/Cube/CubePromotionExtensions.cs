using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D16 RID: 3350
	internal static class CubePromotionExtensions
	{
		// Token: 0x06005A6A RID: 23146 RVA: 0x0013BE67 File Offset: 0x0013A067
		public static TableValue PromoteIfCube(this TableValue table)
		{
			if (table.IsCube)
			{
				return table.AsCube.AddCubeMetadata();
			}
			return table;
		}

		// Token: 0x06005A6B RID: 23147 RVA: 0x0013BE7E File Offset: 0x0013A07E
		public static CubeValue AddCubeMetadata(this CubeValue cube)
		{
			return cube.NewMeta(RecordValue.New(CubeContextCubeValue.CubeMetadataKeys, delegate(int index)
			{
				if (index == 0)
				{
					return LogicalValue.True;
				}
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			})).AsTable.AsCube;
		}
	}
}
