using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.DocumentHost
{
	// Token: 0x0200194A RID: 6474
	public class ResourcesAccessed : IEnumerable<IResource>, IEnumerable
	{
		// Token: 0x0600A43D RID: 42045 RVA: 0x002201A9 File Offset: 0x0021E3A9
		public ResourcesAccessed(IResourcePathService resourcePathService)
		{
			this.resourcePathService = resourcePathService;
			this.resources = new List<IResource>();
		}

		// Token: 0x0600A43E RID: 42046 RVA: 0x002201C3 File Offset: 0x0021E3C3
		public IEnumerator<IResource> GetEnumerator()
		{
			return this.resources.GetEnumerator();
		}

		// Token: 0x0600A43F RID: 42047 RVA: 0x002201D5 File Offset: 0x0021E3D5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600A440 RID: 42048 RVA: 0x002201E0 File Offset: 0x0021E3E0
		public void Add(IEnumerable<IResource> resources)
		{
			foreach (IResource resource in resources)
			{
				this.Add(resource);
			}
		}

		// Token: 0x0600A441 RID: 42049 RVA: 0x00220228 File Offset: 0x0021E428
		public void Add(IResource newResource)
		{
			foreach (IResource resource in this.resources)
			{
				if (this.resourcePathService.StartsWith(resource, newResource))
				{
					return;
				}
			}
			for (int i = this.resources.Count - 1; i >= 0; i--)
			{
				IResource resource2 = this.resources[i];
				if (this.resourcePathService.StartsWith(newResource, resource2))
				{
					this.resources.RemoveAt(i);
				}
			}
			this.resources.Add(newResource);
		}

		// Token: 0x04005587 RID: 21895
		private readonly IResourcePathService resourcePathService;

		// Token: 0x04005588 RID: 21896
		private readonly List<IResource> resources;
	}
}
