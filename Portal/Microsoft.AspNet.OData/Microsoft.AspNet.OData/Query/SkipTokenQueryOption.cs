using System;
using System.Linq;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Query.Validators;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000AA RID: 170
	public class SkipTokenQueryOption
	{
		// Token: 0x060005E0 RID: 1504 RVA: 0x0001517C File Offset: 0x0001337C
		public SkipTokenQueryOption(string rawValue, ODataQueryContext context, ODataQueryOptionParser queryOptionParser)
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
			this.RawValue = rawValue;
			this.Validator = context.GetSkipTokenQueryValidator();
			this.skipTokenHandler = context.GetSkipTokenHandler();
			this.Context = context;
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x000151E4 File Offset: 0x000133E4
		// (set) Token: 0x060005E2 RID: 1506 RVA: 0x000151EC File Offset: 0x000133EC
		public string RawValue { get; private set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x000151F5 File Offset: 0x000133F5
		// (set) Token: 0x060005E4 RID: 1508 RVA: 0x000151FD File Offset: 0x000133FD
		public ODataQueryContext Context { get; private set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x00015206 File Offset: 0x00013406
		public SkipTokenQueryValidator Validator { get; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x0001520E File Offset: 0x0001340E
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x00015216 File Offset: 0x00013416
		public ODataQuerySettings QuerySettings { get; private set; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x0001521F File Offset: 0x0001341F
		// (set) Token: 0x060005E9 RID: 1513 RVA: 0x00015227 File Offset: 0x00013427
		public ODataQueryOptions QueryOptions { get; private set; }

		// Token: 0x060005EA RID: 1514 RVA: 0x00015230 File Offset: 0x00013430
		public virtual IQueryable<T> ApplyTo<T>(IQueryable<T> query, ODataQuerySettings querySettings, ODataQueryOptions queryOptions)
		{
			this.QuerySettings = querySettings;
			this.QueryOptions = queryOptions;
			return this.skipTokenHandler.ApplyTo<T>(query, this) as IOrderedQueryable<T>;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00015252 File Offset: 0x00013452
		public virtual IQueryable ApplyTo(IQueryable query, ODataQuerySettings querySettings, ODataQueryOptions queryOptions)
		{
			this.QuerySettings = querySettings;
			this.QueryOptions = queryOptions;
			return this.skipTokenHandler.ApplyTo(query, this);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0001526F File Offset: 0x0001346F
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

		// Token: 0x04000152 RID: 338
		private SkipTokenHandler skipTokenHandler;
	}
}
