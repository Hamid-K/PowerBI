using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Query.Validators;
using Microsoft.OData;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000D6 RID: 214
	public class SkipQueryOption
	{
		// Token: 0x06000733 RID: 1843 RVA: 0x0001871C File Offset: 0x0001691C
		public SkipQueryOption(string rawValue, ODataQueryContext context, ODataQueryOptionParser queryOptionParser)
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
			this.Validator = SkipQueryValidator.GetSkipQueryValidator(context);
			this._queryOptionParser = queryOptionParser;
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00018780 File Offset: 0x00016980
		internal SkipQueryOption(string rawValue, ODataQueryContext context)
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
			this.Validator = SkipQueryValidator.GetSkipQueryValidator(context);
			this._queryOptionParser = new ODataQueryOptionParser(context.Model, context.ElementType, context.NavigationSource, new Dictionary<string, string> { { "$skip", rawValue } });
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x000187FC File Offset: 0x000169FC
		// (set) Token: 0x06000736 RID: 1846 RVA: 0x00018804 File Offset: 0x00016A04
		public ODataQueryContext Context { get; private set; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x0001880D File Offset: 0x00016A0D
		// (set) Token: 0x06000738 RID: 1848 RVA: 0x00018815 File Offset: 0x00016A15
		public string RawValue { get; private set; }

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x00018820 File Offset: 0x00016A20
		public int Value
		{
			get
			{
				if (this._value == null)
				{
					long? num = this._queryOptionParser.ParseSkip();
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
								AllowedQueryOptions.Skip,
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

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x000188D9 File Offset: 0x00016AD9
		// (set) Token: 0x0600073B RID: 1851 RVA: 0x000188E1 File Offset: 0x00016AE1
		public SkipQueryValidator Validator { get; set; }

		// Token: 0x0600073C RID: 1852 RVA: 0x000188EA File Offset: 0x00016AEA
		public IQueryable<T> ApplyTo<T>(IQueryable<T> query, ODataQuerySettings querySettings)
		{
			return this.ApplyToCore(query, querySettings) as IOrderedQueryable<T>;
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x000188F9 File Offset: 0x00016AF9
		public IQueryable ApplyTo(IQueryable query, ODataQuerySettings querySettings)
		{
			return this.ApplyToCore(query, querySettings);
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00018903 File Offset: 0x00016B03
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

		// Token: 0x0600073F RID: 1855 RVA: 0x00018928 File Offset: 0x00016B28
		private IQueryable ApplyToCore(IQueryable query, ODataQuerySettings querySettings)
		{
			if (this.Context.ElementClrType == null)
			{
				throw Error.NotSupported(SRResources.ApplyToOnUntypedQueryOption, new object[] { "ApplyTo" });
			}
			return ExpressionHelpers.Skip(query, this.Value, query.ElementType, querySettings.EnableConstantParameterization);
		}

		// Token: 0x04000225 RID: 549
		private int? _value;

		// Token: 0x04000226 RID: 550
		private ODataQueryOptionParser _queryOptionParser;
	}
}
