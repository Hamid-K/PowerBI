using System;
using Microsoft.Data.Metadata.Edm;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x02000217 RID: 535
	public abstract class EdmType : EdmItem, IEquatable<EdmType>
	{
		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x060018C6 RID: 6342 RVA: 0x00043A27 File Offset: 0x00041C27
		public string Name
		{
			get
			{
				return this.InternalEdmType.Name;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x060018C7 RID: 6343 RVA: 0x00043A34 File Offset: 0x00041C34
		public string NamespaceName
		{
			get
			{
				return this.InternalEdmType.NamespaceName;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x060018C8 RID: 6344 RVA: 0x00043A41 File Offset: 0x00041C41
		public string FullName
		{
			get
			{
				return this.InternalEdmType.FullName;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x060018C9 RID: 6345
		internal abstract EdmType InternalEdmType { get; }

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x060018CA RID: 6346 RVA: 0x00043A4E File Offset: 0x00041C4E
		internal sealed override MetadataItem InternalEdmItem
		{
			get
			{
				return this.InternalEdmType;
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x060018CB RID: 6347 RVA: 0x00043A56 File Offset: 0x00041C56
		internal string Identity
		{
			get
			{
				return this.InternalEdmType.Identity;
			}
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x00043A63 File Offset: 0x00041C63
		public override bool Equals(object obj)
		{
			return this.Equals(obj as EdmType);
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x00043A71 File Offset: 0x00041C71
		public override int GetHashCode()
		{
			return this.Identity.GetHashCode();
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x00043A7E File Offset: 0x00041C7E
		public bool Equals(EdmType other)
		{
			return this == other || (other != null && base.GetType() == other.GetType() && string.Equals(this.Identity, other.Identity, EdmItem.IdentityComparison));
		}
	}
}
