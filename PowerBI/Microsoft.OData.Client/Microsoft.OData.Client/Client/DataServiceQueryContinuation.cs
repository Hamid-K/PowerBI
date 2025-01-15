using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Microsoft.OData.Client
{
	// Token: 0x02000049 RID: 73
	[DebuggerDisplay("{NextLinkUri}")]
	public abstract class DataServiceQueryContinuation
	{
		// Token: 0x06000242 RID: 578 RVA: 0x00009442 File Offset: 0x00007642
		internal DataServiceQueryContinuation(Uri nextLinkUri, ProjectionPlan plan)
		{
			this.nextLinkUri = nextLinkUri;
			this.plan = plan;
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00009458 File Offset: 0x00007658
		// (set) Token: 0x06000244 RID: 580 RVA: 0x00009460 File Offset: 0x00007660
		public Uri NextLinkUri
		{
			get
			{
				return this.nextLinkUri;
			}
			internal set
			{
				this.nextLinkUri = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000245 RID: 581
		internal abstract Type ElementType { get; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00009469 File Offset: 0x00007669
		// (set) Token: 0x06000247 RID: 583 RVA: 0x00009471 File Offset: 0x00007671
		internal ProjectionPlan Plan
		{
			get
			{
				return this.plan;
			}
			set
			{
				this.plan = value;
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000947A File Offset: 0x0000767A
		public override string ToString()
		{
			return this.NextLinkUri.ToString();
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00009488 File Offset: 0x00007688
		internal static DataServiceQueryContinuation Create(Uri nextLinkUri, ProjectionPlan plan)
		{
			if (nextLinkUri == null)
			{
				return null;
			}
			IEnumerable<ConstructorInfo> instanceConstructors = typeof(DataServiceQueryContinuation<>).MakeGenericType(new Type[] { plan.ProjectedType }).GetInstanceConstructors(false);
			object obj = Util.ConstructorInvoke(instanceConstructors.Single<ConstructorInfo>(), new object[] { nextLinkUri, plan });
			return (DataServiceQueryContinuation)obj;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x000094E8 File Offset: 0x000076E8
		internal QueryComponents CreateQueryComponents()
		{
			return new QueryComponents(this.NextLinkUri, Util.ODataVersionEmpty, this.Plan.LastSegmentType, null, null);
		}

		// Token: 0x040000C6 RID: 198
		private Uri nextLinkUri;

		// Token: 0x040000C7 RID: 199
		private ProjectionPlan plan;
	}
}
