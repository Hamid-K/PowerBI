using System;
using Microsoft.Mashup.Engine1.Library.Cube;

namespace Microsoft.Mashup.Engine1.Library.GoogleAnalytics
{
	// Token: 0x02000B32 RID: 2866
	internal interface IGoogleAnalyticsQueryCompiler
	{
		// Token: 0x06004F7E RID: 20350
		bool CanCompile(QueryCubeExpression expression);

		// Token: 0x06004F7F RID: 20351
		GoogleAnalyticsExpression Compile(QueryCubeExpression expression);
	}
}
