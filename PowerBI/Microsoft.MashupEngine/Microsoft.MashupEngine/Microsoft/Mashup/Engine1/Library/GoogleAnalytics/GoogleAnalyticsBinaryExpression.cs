using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.GoogleAnalytics
{
	// Token: 0x02000B0E RID: 2830
	internal sealed class GoogleAnalyticsBinaryExpression : GoogleAnalyticsFilterExpression
	{
		// Token: 0x06004E5D RID: 20061 RVA: 0x00103C45 File Offset: 0x00101E45
		public GoogleAnalyticsBinaryExpression(GoogleAnalyticsBinaryOperator op, GoogleAnalyticsFilterExpression left, GoogleAnalyticsFilterExpression right, GoogleAnalyticsCubeObjectKind columnKind)
		{
			this.op = op;
			this.left = left;
			this.right = right;
			this.columnKind = columnKind;
		}

		// Token: 0x17001894 RID: 6292
		// (get) Token: 0x06004E5E RID: 20062 RVA: 0x00002105 File Offset: 0x00000305
		public override GoogleAnalyticsExpressionKind Kind
		{
			get
			{
				return GoogleAnalyticsExpressionKind.Binary;
			}
		}

		// Token: 0x17001895 RID: 6293
		// (get) Token: 0x06004E5F RID: 20063 RVA: 0x00103C6A File Offset: 0x00101E6A
		public override string QueryString
		{
			get
			{
				return this.left.QueryString + this.OperatorString(this.op) + this.right.QueryString;
			}
		}

		// Token: 0x17001896 RID: 6294
		// (get) Token: 0x06004E60 RID: 20064 RVA: 0x00103C93 File Offset: 0x00101E93
		public GoogleAnalyticsBinaryOperator Operator
		{
			get
			{
				return this.op;
			}
		}

		// Token: 0x17001897 RID: 6295
		// (get) Token: 0x06004E61 RID: 20065 RVA: 0x00103C9B File Offset: 0x00101E9B
		// (set) Token: 0x06004E62 RID: 20066 RVA: 0x00103CA3 File Offset: 0x00101EA3
		public GoogleAnalyticsFilterExpression Left
		{
			get
			{
				return this.left;
			}
			set
			{
				this.left = value;
			}
		}

		// Token: 0x17001898 RID: 6296
		// (get) Token: 0x06004E63 RID: 20067 RVA: 0x00103CAC File Offset: 0x00101EAC
		// (set) Token: 0x06004E64 RID: 20068 RVA: 0x00103CB4 File Offset: 0x00101EB4
		public GoogleAnalyticsFilterExpression Right
		{
			get
			{
				return this.right;
			}
			set
			{
				this.right = value;
			}
		}

		// Token: 0x17001899 RID: 6297
		// (get) Token: 0x06004E65 RID: 20069 RVA: 0x00103CBD File Offset: 0x00101EBD
		public override GoogleAnalyticsCubeObjectKind ColumnKind
		{
			get
			{
				return this.columnKind;
			}
		}

		// Token: 0x06004E66 RID: 20070 RVA: 0x00103CC8 File Offset: 0x00101EC8
		private string OperatorString(GoogleAnalyticsBinaryOperator op)
		{
			switch (op)
			{
			case GoogleAnalyticsBinaryOperator.Or:
				return ",";
			case GoogleAnalyticsBinaryOperator.GreaterThan:
				return ">";
			case GoogleAnalyticsBinaryOperator.GreaterThanOrEqual:
				return ">=";
			case GoogleAnalyticsBinaryOperator.LessThan:
				return "<";
			case GoogleAnalyticsBinaryOperator.LessThanOrEqual:
				return "<=";
			case GoogleAnalyticsBinaryOperator.Equal:
				return "==";
			case GoogleAnalyticsBinaryOperator.NotEqual:
				return "!=";
			case GoogleAnalyticsBinaryOperator.RegexMatch:
				return "=~";
			default:
				throw new NotImplementedException();
			}
		}

		// Token: 0x06004E67 RID: 20071 RVA: 0x00103D34 File Offset: 0x00101F34
		protected override Value GenerateV2Filter()
		{
			switch (this.op)
			{
			case GoogleAnalyticsBinaryOperator.Or:
				return RecordValue.New(Keys.New("orGroup"), new Value[] { RecordValue.New(Keys.New("expressions"), new Value[] { ListValue.New(new Value[]
				{
					this.Left.V2Filter,
					this.right.V2Filter
				}) }) });
			case GoogleAnalyticsBinaryOperator.GreaterThan:
				return this.WrapNumericFilter("GREATER_THAN");
			case GoogleAnalyticsBinaryOperator.GreaterThanOrEqual:
				return this.WrapNumericFilter("GREATER_THAN_OR_EQUAL");
			case GoogleAnalyticsBinaryOperator.LessThan:
				return this.WrapNumericFilter("LESS_THAN");
			case GoogleAnalyticsBinaryOperator.LessThanOrEqual:
				return this.WrapNumericFilter("LESS_THAN_OR_EQUAL");
			case GoogleAnalyticsBinaryOperator.Equal:
				return this.WrapEqualFilter();
			case GoogleAnalyticsBinaryOperator.NotEqual:
				return RecordValue.New(Keys.New("notExpression"), new Value[] { this.WrapEqualFilter() });
			case GoogleAnalyticsBinaryOperator.RegexMatch:
				return this.WrapStringFilter("FULL_REGEXP");
			default:
				throw new NotImplementedException();
			}
		}

		// Token: 0x06004E68 RID: 20072 RVA: 0x00103E32 File Offset: 0x00102032
		private RecordValue WrapFilter(RecordValue filter)
		{
			return RecordValue.New(Keys.New("filter"), new Value[] { filter });
		}

		// Token: 0x06004E69 RID: 20073 RVA: 0x00103E50 File Offset: 0x00102050
		private RecordValue WrapNumericFilter(string operation)
		{
			return this.WrapFilter(RecordValue.New(Keys.New("fieldName", "numericFilter"), new Value[]
			{
				this.left.V2Filter,
				RecordValue.New(Keys.New("operation", "value"), new Value[]
				{
					TextValue.New(operation),
					this.right.V2Filter
				})
			}));
		}

		// Token: 0x06004E6A RID: 20074 RVA: 0x00103EC0 File Offset: 0x001020C0
		private RecordValue WrapStringFilter(string matchType)
		{
			return this.WrapFilter(RecordValue.New(Keys.New("fieldName", "stringFilter"), new Value[]
			{
				this.left.V2Filter,
				RecordValue.New(Keys.New("matchType", "value", "caseSensitive"), new Value[]
				{
					TextValue.New(matchType),
					this.right.V2Filter,
					LogicalValue.True
				})
			}));
		}

		// Token: 0x06004E6B RID: 20075 RVA: 0x00103F3C File Offset: 0x0010213C
		private RecordValue WrapEqualFilter()
		{
			GoogleAnalyticsIdentifierExpression googleAnalyticsIdentifierExpression = this.left as GoogleAnalyticsIdentifierExpression;
			if (googleAnalyticsIdentifierExpression == null)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.SemanticsBuilder_InvalidIdentifier, null, null);
			}
			GoogleAnalyticsDataType dataType = googleAnalyticsIdentifierExpression.DataType;
			switch (dataType)
			{
			case GoogleAnalyticsDataType.Currency:
			case GoogleAnalyticsDataType.Float:
			case GoogleAnalyticsDataType.Integer:
				break;
			case GoogleAnalyticsDataType.Date:
			case GoogleAnalyticsDataType.Hours:
				goto IL_004B;
			default:
				if (dataType != GoogleAnalyticsDataType.Percent)
				{
					goto IL_004B;
				}
				break;
			}
			return this.WrapNumericFilter("EQUAL");
			IL_004B:
			return this.WrapStringFilter("EXACT");
		}

		// Token: 0x04002A2A RID: 10794
		private readonly GoogleAnalyticsBinaryOperator op;

		// Token: 0x04002A2B RID: 10795
		private GoogleAnalyticsFilterExpression left;

		// Token: 0x04002A2C RID: 10796
		private GoogleAnalyticsFilterExpression right;

		// Token: 0x04002A2D RID: 10797
		private GoogleAnalyticsCubeObjectKind columnKind;
	}
}
