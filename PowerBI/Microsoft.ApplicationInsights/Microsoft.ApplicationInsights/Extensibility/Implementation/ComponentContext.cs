using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights.Extensibility.Implementation.External;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x02000067 RID: 103
	public sealed class ComponentContext
	{
		// Token: 0x06000316 RID: 790 RVA: 0x0000E810 File Offset: 0x0000CA10
		internal ComponentContext()
		{
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000E818 File Offset: 0x0000CA18
		// (set) Token: 0x06000318 RID: 792 RVA: 0x0000E82F File Offset: 0x0000CA2F
		public string Version
		{
			get
			{
				if (!string.IsNullOrEmpty(this.version))
				{
					return this.version;
				}
				return null;
			}
			set
			{
				this.version = value;
			}
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000E838 File Offset: 0x0000CA38
		internal void UpdateTags(IDictionary<string, string> tags)
		{
			tags.UpdateTagValue(ContextTagKeys.Keys.ApplicationVersion, this.Version);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000E850 File Offset: 0x0000CA50
		internal void CopyTo(ComponentContext target)
		{
			Tags.CopyTagValue(this.Version, ref target.version);
		}

		// Token: 0x04000154 RID: 340
		private string version;
	}
}
