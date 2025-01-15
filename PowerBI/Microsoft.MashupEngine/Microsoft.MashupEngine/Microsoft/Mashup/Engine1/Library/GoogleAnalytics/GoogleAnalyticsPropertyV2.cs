using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.GoogleAnalytics
{
	// Token: 0x02000B26 RID: 2854
	internal sealed class GoogleAnalyticsPropertyV2 : GoogleAnalyticsProperty
	{
		// Token: 0x06004F00 RID: 20224 RVA: 0x00105D05 File Offset: 0x00103F05
		public GoogleAnalyticsPropertyV2(IGoogleAnalyticsService service, string accountId, string id, string name, DateTime created)
			: base(service, accountId, id, name)
		{
			this.created = created;
		}

		// Token: 0x06004F01 RID: 20225 RVA: 0x00105D1A File Offset: 0x00103F1A
		public override IGoogleAnalyticsQueryCompiler CreateCompiler(GoogleAnalyticsCube cube)
		{
			return new GoogleAnalyticsQueryCompilerV2(cube);
		}

		// Token: 0x06004F02 RID: 20226 RVA: 0x00105D24 File Offset: 0x00103F24
		protected override IList<GoogleAnalyticsCube> CreateViews()
		{
			return new GoogleAnalyticsCube[]
			{
				new GoogleAnalyticsCube(this.service, this, base.ID, base.Name, this.created, new Func<GoogleAnalyticsCube, GoogleAnalyticsQueryExpression, CubeExpression, Keys, IEnumerator<IValueReference>>(GoogleAnalyticsCubeContextProvider.CreateV2ResultEnumerator))
			};
		}

		// Token: 0x04002A86 RID: 10886
		private readonly DateTime created;
	}
}
