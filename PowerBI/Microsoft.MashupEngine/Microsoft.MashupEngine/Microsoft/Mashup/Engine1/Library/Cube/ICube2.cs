using System;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D2E RID: 3374
	internal interface ICube2 : ICube
	{
		// Token: 0x06005AD6 RID: 23254
		bool IsIndependent(ICubeLevel level);

		// Token: 0x06005AD7 RID: 23255
		bool AreRelated(ICubeLevel level1, ICubeLevel level2);

		// Token: 0x06005AD8 RID: 23256
		bool TryCreateMeasure(CubeExpression expression, out ICubeMeasure measure);
	}
}
