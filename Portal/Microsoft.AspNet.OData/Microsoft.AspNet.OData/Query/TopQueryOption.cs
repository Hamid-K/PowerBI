using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Query.Validators;
using Microsoft.OData;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000D2 RID: 210
	public class TopQueryOption
	{
		// Token: 0x06000701 RID: 1793 RVA: 0x00017C2C File Offset: 0x00015E2C
		public TopQueryOption(string rawValue, ODataQueryContext context, ODataQueryOptionParser queryOptionParser)
		{
			if (context == null)
			{
				throw Error.ArgumentNull("context");
			}
			if (string.IsNullOrEmpty(rawValue))
			{
				throw Error.ArgumentNullOrEmpty("rawValue");
			}
			if (queryOptionParser == null)
			{
				throw Error.ArgumentNull("queryOptionParser");
			}
			this.Context = context;
			this.RawValue = rawValue;
			this.Validator = TopQueryValidator.GetTopQueryValidator(context);
			this._queryOptionParser = queryOptionParser;
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00017C90 File Offset: 0x00015E90
		internal TopQueryOption(string rawValue, ODataQueryContext context)
		{
			if (context == null)
			{
				throw Error.ArgumentNull("context");
			}
			if (string.IsNullOrEmpty(rawValue))
			{
				throw Error.ArgumentNullOrEmpty("rawValue");
			}
			this.Context = context;
			this.RawValue = rawValue;
			this.Validator = TopQueryValidator.GetTopQueryValidator(context);
			this._queryOptionParser = new ODataQueryOptionParser(context.Model, context.ElementType, context.NavigationSource, new Dictionary<string, string> { { "$top", rawValue } });
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x00017D0C File Offset: 0x00015F0C
		// (set) Token: 0x06000704 RID: 1796 RVA: 0x00017D14 File Offset: 0x00015F14
		public ODataQueryContext Context { get; private set; }

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x00017D1D File Offset: 0x00015F1D
		// (set) Token: 0x06000706 RID: 1798 RVA: 0x00017D25 File Offset: 0x00015F25
		public string RawValue { get; private set; }

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x00017D30 File Offset: 0x00015F30
		public int Value
		{
			get
			{
				if (this._value == null)
				{
					long? num = this._queryOptionParser.ParseTop();
					long? num2;
					if (num != null)
					{
						num2 = num;
						long num3 = 2147483647L;
						if ((num2.GetValueOrDefault() > num3) & (num2 != null))
						{
							throw new ODataException(Error.Format(SRResources.SkipTopLimitExceeded, new object[]
							{
								int.MaxValue,
								AllowedQueryOptions.Top,
								this.RawValue
							}));
						}
					}
					num2 = num;
					this._value = ((num2 != null) ? new int?((int)num2.GetValueOrDefault()) : null);
				}
				return this._value.Value;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x00017DE9 File Offset: 0x00015FE9
		// (set) Token: 0x06000709 RID: 1801 RVA: 0x00017DF1 File Offset: 0x00015FF1
		public TopQueryValidator Validator { get; set; }

		// Token: 0x0600070A RID: 1802 RVA: 0x00017DFA File Offset: 0x00015FFA
		public IOrderedQueryable<T> ApplyTo<T>(IQueryable<T> query, ODataQuerySettings querySettings)
		{
			return this.ApplyToCore(query, querySettings) as IOrderedQueryable<T>;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00017E09 File Offset: 0x00016009
		public IQueryable ApplyTo(IQueryable query, ODataQuerySettings querySettings)
		{
			return this.ApplyToCore(query, querySettings);
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00017E13 File Offset: 0x00016013
		public void Validate(ODataValidationSettings validationSettings)
		{
			if (validationSettings == null)
			{
				throw Error.ArgumentNull("validationSettings");
			}
			if (this.Validator != null)
			{
				this.Validator.Validate(this, validationSettings);
			}
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00017E38 File Offset: 0x00016038
		private IQueryable ApplyToCore(IQueryable query, ODataQuerySettings querySettings)
		{
			if (this.Context.ElementClrType == null)
			{
				throw Error.NotSupported(SRResources.ApplyToOnUntypedQueryOption, new object[] { "ApplyTo" });
			}
			return ExpressionHelpers.Take(query, this.Value, query.ElementType, querySettings.EnableConstantParameterization);
		}

		// Token: 0x04000213 RID: 531
		private int? _value;

		// Token: 0x04000214 RID: 532
		private ODataQueryOptionParser _queryOptionParser;
	}
}
