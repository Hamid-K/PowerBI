using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.GoogleAnalytics
{
	// Token: 0x02000B0F RID: 2831
	internal abstract class GoogleAnalyticsQueryExpression : GoogleAnalyticsExpression
	{
		// Token: 0x06004E6C RID: 20076 RVA: 0x00103FA0 File Offset: 0x001021A0
		public GoogleAnalyticsQueryExpression(IGoogleAnalyticsCube cube, IList<GoogleAnalyticsExpression> measures, IList<GoogleAnalyticsExpression> dimensions, bool shouldFilterLocally, CubeExpression filterExpression, IList<GoogleAnalyticsExpression> sorts, RowRange range, DateTime startDate, DateTime endDate, bool isFalse)
		{
			this.cube = cube;
			this.measures = measures;
			this.dimensions = dimensions;
			this.shouldFilterLocally = shouldFilterLocally;
			this.filterExpression = filterExpression;
			this.sorts = sorts;
			this.range = range;
			this.startDate = startDate;
			this.isFalse = isFalse;
			if (endDate < startDate)
			{
				this.endDate = startDate;
			}
			else
			{
				this.endDate = endDate;
			}
			this.uri = new UriBuilder
			{
				Path = "/v4/reports:batchGet"
			}.Uri;
		}

		// Token: 0x1700189A RID: 6298
		// (get) Token: 0x06004E6D RID: 20077 RVA: 0x00104032 File Offset: 0x00102232
		public RecordValue Body
		{
			get
			{
				if (this.body == null)
				{
					this.body = this.CreateBody();
				}
				return this.body;
			}
		}

		// Token: 0x1700189B RID: 6299
		// (get) Token: 0x06004E6E RID: 20078 RVA: 0x0010404E File Offset: 0x0010224E
		public CubeExpression Filter
		{
			get
			{
				return this.filterExpression;
			}
		}

		// Token: 0x1700189C RID: 6300
		// (get) Token: 0x06004E6F RID: 20079 RVA: 0x00104056 File Offset: 0x00102256
		public bool IsFalse
		{
			get
			{
				return this.isFalse;
			}
		}

		// Token: 0x1700189D RID: 6301
		// (get) Token: 0x06004E70 RID: 20080 RVA: 0x0010405E File Offset: 0x0010225E
		public IList<GoogleAnalyticsExpression> Measures
		{
			get
			{
				return this.measures;
			}
		}

		// Token: 0x1700189E RID: 6302
		// (get) Token: 0x06004E71 RID: 20081 RVA: 0x00104066 File Offset: 0x00102266
		public string PropertyName
		{
			get
			{
				return this.cube.ViewId;
			}
		}

		// Token: 0x1700189F RID: 6303
		// (get) Token: 0x06004E72 RID: 20082 RVA: 0x0000240C File Offset: 0x0000060C
		public override GoogleAnalyticsExpressionKind Kind
		{
			get
			{
				return GoogleAnalyticsExpressionKind.Query;
			}
		}

		// Token: 0x170018A0 RID: 6304
		// (get) Token: 0x06004E73 RID: 20083 RVA: 0x000091AE File Offset: 0x000073AE
		public override string QueryString
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170018A1 RID: 6305
		// (get) Token: 0x06004E74 RID: 20084 RVA: 0x00104073 File Offset: 0x00102273
		public bool ShouldFilterLocally
		{
			get
			{
				return this.shouldFilterLocally;
			}
		}

		// Token: 0x170018A2 RID: 6306
		// (get) Token: 0x06004E75 RID: 20085 RVA: 0x0010407B File Offset: 0x0010227B
		public Uri Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x06004E76 RID: 20086
		protected abstract RecordValue CreateBody();

		// Token: 0x04002A2E RID: 10798
		public readonly DateTime endDate;

		// Token: 0x04002A2F RID: 10799
		protected readonly IGoogleAnalyticsCube cube;

		// Token: 0x04002A30 RID: 10800
		protected readonly RowRange range;

		// Token: 0x04002A31 RID: 10801
		protected readonly IList<GoogleAnalyticsExpression> dimensions;

		// Token: 0x04002A32 RID: 10802
		protected readonly IList<GoogleAnalyticsExpression> measures;

		// Token: 0x04002A33 RID: 10803
		protected readonly IList<GoogleAnalyticsExpression> sorts;

		// Token: 0x04002A34 RID: 10804
		private RecordValue body;

		// Token: 0x04002A35 RID: 10805
		private readonly CubeExpression filterExpression;

		// Token: 0x04002A36 RID: 10806
		private readonly bool isFalse;

		// Token: 0x04002A37 RID: 10807
		private readonly bool shouldFilterLocally;

		// Token: 0x04002A38 RID: 10808
		public readonly DateTime startDate;

		// Token: 0x04002A39 RID: 10809
		private readonly Uri uri;
	}
}
