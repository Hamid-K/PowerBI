using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D36 RID: 3382
	internal static class CubeObjectExtensions
	{
		// Token: 0x06005AE3 RID: 23267 RVA: 0x0013D700 File Offset: 0x0013B900
		public static bool TryCreateMeasure(this ICube cube, CubeExpression expression, out ICubeMeasure measure)
		{
			IList<ScopePath> list;
			CubeExpression unscoped = expression.GetUnscoped(out list);
			ICube2 cube2 = cube.GetUnscoped() as ICube2;
			if (list.Count == 1 && cube2 != null && cube2.TryCreateMeasure(unscoped, out measure))
			{
				return true;
			}
			measure = null;
			return false;
		}
	}
}
