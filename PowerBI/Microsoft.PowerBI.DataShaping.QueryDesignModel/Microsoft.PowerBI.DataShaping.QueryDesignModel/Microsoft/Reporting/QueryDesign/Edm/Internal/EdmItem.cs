using System;
using Microsoft.Data.Metadata.Edm;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x02000208 RID: 520
	public abstract class EdmItem
	{
		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x0600185E RID: 6238 RVA: 0x00042FC0 File Offset: 0x000411C0
		internal static StringComparer IdentityComparer
		{
			get
			{
				return StringComparer.Ordinal;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x0600185F RID: 6239 RVA: 0x00042FC7 File Offset: 0x000411C7
		internal static StringComparison IdentityComparison
		{
			get
			{
				return StringComparison.Ordinal;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001860 RID: 6240 RVA: 0x00042FCA File Offset: 0x000411CA
		internal static StringComparer ReferenceNameComparer
		{
			get
			{
				return StringComparer.Ordinal;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001862 RID: 6242 RVA: 0x00042FD9 File Offset: 0x000411D9
		public string Description
		{
			get
			{
				if (this.InternalEdmItem.Documentation == null)
				{
					return null;
				}
				return this.InternalEdmItem.Documentation.Summary;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001863 RID: 6243
		internal abstract MetadataItem InternalEdmItem { get; }

		// Token: 0x06001864 RID: 6244 RVA: 0x00042FFA File Offset: 0x000411FA
		public override string ToString()
		{
			return this.InternalEdmItem.ToString();
		}
	}
}
