using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.Http.Routing
{
	// Token: 0x02000149 RID: 329
	internal static class RoutePrecedence
	{
		// Token: 0x060008F8 RID: 2296 RVA: 0x0001671C File Offset: 0x0001491C
		internal static int ComputeDigit(PathContentSegment segment, IDictionary<string, object> constraints)
		{
			if (segment.Subsegments.Count > 1)
			{
				return 2;
			}
			PathSubsegment pathSubsegment = segment.Subsegments[0];
			if (pathSubsegment is PathLiteralSubsegment)
			{
				return 1;
			}
			PathParameterSubsegment pathParameterSubsegment = pathSubsegment as PathParameterSubsegment;
			int num = (pathParameterSubsegment.IsCatchAll ? 5 : 3);
			if (constraints != null && constraints.ContainsKey(pathParameterSubsegment.ParameterName))
			{
				num--;
			}
			return num;
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0001677C File Offset: 0x0001497C
		public static decimal Compute(HttpParsedRoute parsedRoute, IDictionary<string, object> constraints)
		{
			IList<PathContentSegment> list = parsedRoute.PathSegments.OfType<PathContentSegment>().ToArray<PathContentSegment>();
			decimal num = 0m;
			uint num2 = 1U;
			for (int i = 0; i < list.Count; i++)
			{
				int num3 = RoutePrecedence.ComputeDigit(list[i], constraints);
				num += decimal.Divide(num3, num2);
				num2 *= 10U;
			}
			return num;
		}
	}
}
