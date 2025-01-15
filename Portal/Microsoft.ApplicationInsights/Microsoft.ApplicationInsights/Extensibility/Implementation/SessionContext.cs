using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights.Extensibility.Implementation.External;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x0200007A RID: 122
	public sealed class SessionContext
	{
		// Token: 0x060003E3 RID: 995 RVA: 0x000118E5 File Offset: 0x0000FAE5
		internal SessionContext()
		{
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x000118ED File Offset: 0x0000FAED
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x00011904 File Offset: 0x0000FB04
		public string Id
		{
			get
			{
				if (!string.IsNullOrEmpty(this.id))
				{
					return this.id;
				}
				return null;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x0001190D File Offset: 0x0000FB0D
		// (set) Token: 0x060003E7 RID: 999 RVA: 0x00011915 File Offset: 0x0000FB15
		public bool? IsFirst
		{
			get
			{
				return this.isFirst;
			}
			set
			{
				this.isFirst = value;
			}
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0001191E File Offset: 0x0000FB1E
		internal void UpdateTags(IDictionary<string, string> tags)
		{
			tags.UpdateTagValue(ContextTagKeys.Keys.SessionId, this.Id);
			tags.UpdateTagValue(ContextTagKeys.Keys.SessionIsFirst, this.IsFirst);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0001194C File Offset: 0x0000FB4C
		internal void CopyTo(SessionContext target)
		{
			Tags.CopyTagValue(this.Id, ref target.id);
			Tags.CopyTagValue(this.IsFirst, ref target.isFirst);
		}

		// Token: 0x04000195 RID: 405
		private string id;

		// Token: 0x04000196 RID: 406
		private bool? isFirst;
	}
}
