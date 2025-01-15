using System;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000102 RID: 258
	public class OperationLinkBuilder
	{
		// Token: 0x060008FF RID: 2303 RVA: 0x00025D38 File Offset: 0x00023F38
		public OperationLinkBuilder(Func<ResourceContext, Uri> linkFactory, bool followsConventions)
		{
			if (linkFactory == null)
			{
				throw Error.ArgumentNull("linkFactory");
			}
			this._linkFactory = linkFactory;
			this.FollowsConventions = followsConventions;
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00025D5C File Offset: 0x00023F5C
		public OperationLinkBuilder(Func<ResourceSetContext, Uri> linkFactory, bool followsConventions)
		{
			if (linkFactory == null)
			{
				throw Error.ArgumentNull("linkFactory");
			}
			this._feedLinkFactory = linkFactory;
			this.FollowsConventions = followsConventions;
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x00025D80 File Offset: 0x00023F80
		internal Func<ResourceContext, Uri> LinkFactory
		{
			get
			{
				return this._linkFactory;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x00025D88 File Offset: 0x00023F88
		internal Func<ResourceSetContext, Uri> FeedLinkFactory
		{
			get
			{
				return this._feedLinkFactory;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x00025D90 File Offset: 0x00023F90
		// (set) Token: 0x06000904 RID: 2308 RVA: 0x00025D98 File Offset: 0x00023F98
		public bool FollowsConventions { get; private set; }

		// Token: 0x06000905 RID: 2309 RVA: 0x00025DA1 File Offset: 0x00023FA1
		public virtual Uri BuildLink(ResourceContext context)
		{
			if (this._linkFactory == null)
			{
				return null;
			}
			return this._linkFactory(context);
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00025DB9 File Offset: 0x00023FB9
		public virtual Uri BuildLink(ResourceSetContext context)
		{
			if (this._feedLinkFactory == null)
			{
				return null;
			}
			return this._feedLinkFactory(context);
		}

		// Token: 0x040002D0 RID: 720
		private Func<ResourceContext, Uri> _linkFactory;

		// Token: 0x040002D1 RID: 721
		private readonly Func<ResourceSetContext, Uri> _feedLinkFactory;
	}
}
