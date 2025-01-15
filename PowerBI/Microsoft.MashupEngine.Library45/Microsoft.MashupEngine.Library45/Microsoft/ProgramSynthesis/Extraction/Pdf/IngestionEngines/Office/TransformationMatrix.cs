using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities;
using Microsoft.ProgramSynthesis.Utils.Caching;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.IngestionEngines.Office
{
	// Token: 0x02000DBB RID: 3515
	[NullableContext(1)]
	[Nullable(0)]
	internal struct TransformationMatrix
	{
		// Token: 0x06005956 RID: 22870 RVA: 0x0011C16B File Offset: 0x0011A36B
		internal static void ClearCache()
		{
			TransformationMatrix.InterningCache.Clear();
		}

		// Token: 0x06005957 RID: 22871 RVA: 0x0011C178 File Offset: 0x0011A378
		public TransformationMatrix AsTransformationMatrix(double featureScale)
		{
			return TransformationMatrix.InterningCache.GetOrAdd(Tuple.Create<float, float, float, float, float, float>(this.m00, this.m01, this.m10, this.m11, this.m20, this.m21), (Tuple<float, float, float, float, float, float> t) => TransformationMatrix.Create((float)(featureScale * (double)t.Item1), (float)(featureScale * (double)t.Item2), (float)(featureScale * (double)t.Item3), (float)(featureScale * (double)t.Item4), (float)(featureScale * (double)t.Item5), (float)(featureScale * (double)t.Item6)));
		}

		// Token: 0x040029AC RID: 10668
		public readonly float m00;

		// Token: 0x040029AD RID: 10669
		public readonly float m01;

		// Token: 0x040029AE RID: 10670
		public readonly float m10;

		// Token: 0x040029AF RID: 10671
		public readonly float m11;

		// Token: 0x040029B0 RID: 10672
		public readonly float m20;

		// Token: 0x040029B1 RID: 10673
		public readonly float m21;

		// Token: 0x040029B2 RID: 10674
		private static readonly ConcurrentLruCache<Tuple<float, float, float, float, float, float>, TransformationMatrix> InterningCache = new ConcurrentLruCache<Tuple<float, float, float, float, float, float>, TransformationMatrix>(4096, EqualityComparer<Tuple<float, float, float, float, float, float>>.Default, null, null);
	}
}
