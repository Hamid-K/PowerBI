using System;

namespace System.Web.Cors
{
	// Token: 0x02000003 RID: 3
	public interface ICorsEngine
	{
		// Token: 0x06000015 RID: 21
		CorsResult EvaluatePolicy(CorsRequestContext requestContext, CorsPolicy policy);
	}
}
