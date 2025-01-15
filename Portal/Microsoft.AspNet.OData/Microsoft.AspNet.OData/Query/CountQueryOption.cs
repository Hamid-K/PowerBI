using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Query.Validators;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000C6 RID: 198
	public class CountQueryOption
	{
		// Token: 0x060006AB RID: 1707 RVA: 0x000172D4 File Offset: 0x000154D4
		public CountQueryOption(string rawValue, ODataQueryContext context, ODataQueryOptionParser queryOptionParser)
		{
			if (string.IsNullOrEmpty(rawValue))
			{
				throw Error.ArgumentNullOrEmpty("rawValue");
			}
			if (context == null)
			{
				throw Error.ArgumentNull("context");
			}
			if (queryOptionParser == null)
			{
				throw Error.ArgumentNull("queryOptionParser");
			}
			this.Context = context;
			this.RawValue = rawValue;
			this.Validator = CountQueryValidator.GetCountQueryValidator(context);
			this._queryOptionParser = queryOptionParser;
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00017338 File Offset: 0x00015538
		internal CountQueryOption(string rawValue, ODataQueryContext context)
		{
			if (string.IsNullOrEmpty(rawValue))
			{
				throw Error.ArgumentNullOrEmpty("rawValue");
			}
			if (context == null)
			{
				throw Error.ArgumentNull("context");
			}
			this.Context = context;
			this.RawValue = rawValue;
			this.Validator = CountQueryValidator.GetCountQueryValidator(context);
			this._queryOptionParser = new ODataQueryOptionParser(context.Model, context.ElementType, context.NavigationSource, new Dictionary<string, string> { { "$count", rawValue } }, context.RequestContainer);
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x000173BA File Offset: 0x000155BA
		// (set) Token: 0x060006AE RID: 1710 RVA: 0x000173C2 File Offset: 0x000155C2
		public ODataQueryContext Context { get; private set; }

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x000173CB File Offset: 0x000155CB
		// (set) Token: 0x060006B0 RID: 1712 RVA: 0x000173D3 File Offset: 0x000155D3
		public string RawValue { get; private set; }

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x000173DC File Offset: 0x000155DC
		public bool Value
		{
			get
			{
				if (this._value == null)
				{
					this._value = this._queryOptionParser.ParseCount();
				}
				return this._value.Value;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x00017407 File Offset: 0x00015607
		// (set) Token: 0x060006B3 RID: 1715 RVA: 0x0001740F File Offset: 0x0001560F
		public CountQueryValidator Validator { get; set; }

		// Token: 0x060006B4 RID: 1716 RVA: 0x00017418 File Offset: 0x00015618
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

		// Token: 0x060006B5 RID: 1717 RVA: 0x00017440 File Offset: 0x00015640
		public long? GetEntityCount(IQueryable query)
		{
			if (this.Context.ElementClrType == null)
			{
				throw Error.NotSupported(SRResources.ApplyToOnUntypedQueryOption, new object[] { "GetEntityCount" });
			}
			if (this.Value)
			{
				return new long?(ExpressionHelpers.Count(query, this.Context.ElementClrType)());
			}
			return null;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x000174A8 File Offset: 0x000156A8
		internal Func<long> GetEntityCountFunc(IQueryable query)
		{
			if (this.Context.ElementClrType == null)
			{
				throw Error.NotSupported(SRResources.ApplyToOnUntypedQueryOption, new object[] { "GetEntityCount" });
			}
			if (this.Value)
			{
				return ExpressionHelpers.Count(query, this.Context.ElementClrType);
			}
			return null;
		}

		// Token: 0x0400019A RID: 410
		private bool? _value;

		// Token: 0x0400019B RID: 411
		private ODataQueryOptionParser _queryOptionParser;
	}
}
