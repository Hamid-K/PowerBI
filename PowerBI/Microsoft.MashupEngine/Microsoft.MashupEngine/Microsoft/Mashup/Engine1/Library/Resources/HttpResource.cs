using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.Resources
{
	// Token: 0x0200050C RID: 1292
	public class HttpResource
	{
		// Token: 0x060029ED RID: 10733 RVA: 0x0007D66C File Offset: 0x0007B86C
		protected HttpResource(IResource resource, bool newUrlIsNewResource = true)
		{
			this.resource = resource;
			this.newUrlIsNewResource = newUrlIsNewResource;
		}

		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x060029EE RID: 10734 RVA: 0x0007D682 File Offset: 0x0007B882
		public IResource Resource
		{
			get
			{
				return this.resource;
			}
		}

		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x060029EF RID: 10735 RVA: 0x0007D68A File Offset: 0x0007B88A
		public string Kind
		{
			get
			{
				return this.resource.Kind;
			}
		}

		// Token: 0x1700100F RID: 4111
		// (get) Token: 0x060029F0 RID: 10736 RVA: 0x0007D697 File Offset: 0x0007B897
		public string Path
		{
			get
			{
				return this.resource.Path;
			}
		}

		// Token: 0x060029F1 RID: 10737 RVA: 0x0007D6A4 File Offset: 0x0007B8A4
		public virtual HttpResource NewUrl(string newUrl)
		{
			if (this.newUrlIsNewResource)
			{
				return new HttpResource(Microsoft.Mashup.Engine1.Library.Resource.New(this.Kind, newUrl), true);
			}
			return this;
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x0007D6C4 File Offset: 0x0007B8C4
		public bool IsCompatibleWith(IResource resource)
		{
			if (this.Kind != resource.Kind)
			{
				return false;
			}
			if (this.Path == resource.Path)
			{
				return true;
			}
			Uri uri = new Uri(this.Path);
			Uri uri2 = new Uri(resource.Path);
			return uri.Scheme.Equals(uri2.Scheme, StringComparison.OrdinalIgnoreCase) && uri.Host.Equals(uri2.Host, StringComparison.OrdinalIgnoreCase) && uri.Port == uri2.Port;
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x0007D74B File Offset: 0x0007B94B
		public static HttpResource New(string kind, string path, bool newUrlIsNewResource)
		{
			return new HttpResource(Microsoft.Mashup.Engine1.Library.Resource.New(kind, path), newUrlIsNewResource);
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x0007D75A File Offset: 0x0007B95A
		public static HttpResource New(IResource resource, bool newUrlIsNewResource)
		{
			return new HttpResource(resource, newUrlIsNewResource);
		}

		// Token: 0x04001239 RID: 4665
		private readonly IResource resource;

		// Token: 0x0400123A RID: 4666
		private readonly bool newUrlIsNewResource;
	}
}
