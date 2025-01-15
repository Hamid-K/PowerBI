using System;

namespace Microsoft.OData.Client
{
	// Token: 0x020000CF RID: 207
	public sealed class DataServiceRequest<TElement> : DataServiceRequest
	{
		// Token: 0x060006BB RID: 1723 RVA: 0x0001C8B0 File Offset: 0x0001AAB0
		public DataServiceRequest(Uri requestUri)
		{
			Util.CheckArgumentNull<Uri>(requestUri, "requestUri");
			this.requestUri = requestUri;
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0001C8CB File Offset: 0x0001AACB
		internal DataServiceRequest(Uri requestUri, QueryComponents queryComponents, ProjectionPlan plan)
			: this(requestUri)
		{
			this.queryComponents = queryComponents;
			this.plan = plan;
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x0001C8E2 File Offset: 0x0001AAE2
		public override Type ElementType
		{
			get
			{
				return typeof(TElement);
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x0001C8EE File Offset: 0x0001AAEE
		// (set) Token: 0x060006BF RID: 1727 RVA: 0x0001C8F6 File Offset: 0x0001AAF6
		public override Uri RequestUri
		{
			get
			{
				return this.requestUri;
			}
			internal set
			{
				this.requestUri = value;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0001C8FF File Offset: 0x0001AAFF
		internal override ProjectionPlan Plan
		{
			get
			{
				return this.plan;
			}
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0001C907 File Offset: 0x0001AB07
		public override string ToString()
		{
			return this.requestUri.ToString();
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0001C914 File Offset: 0x0001AB14
		internal override QueryComponents QueryComponents(ClientEdmModel model)
		{
			if (this.queryComponents == null)
			{
				Type type = typeof(TElement);
				type = ((PrimitiveType.IsKnownType(type) || WebUtil.IsCLRTypeCollection(type, model)) ? type : TypeSystem.GetElementType(type));
				this.queryComponents = new QueryComponents(this.requestUri, Util.ODataVersionEmpty, type, null, null);
			}
			return this.queryComponents;
		}

		// Token: 0x040002ED RID: 749
		private readonly ProjectionPlan plan;

		// Token: 0x040002EE RID: 750
		private Uri requestUri;

		// Token: 0x040002EF RID: 751
		private QueryComponents queryComponents;
	}
}
