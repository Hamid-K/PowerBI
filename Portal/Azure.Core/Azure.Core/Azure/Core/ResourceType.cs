using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x0200005D RID: 93
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct ResourceType : IEquatable<ResourceType>
	{
		// Token: 0x06000333 RID: 819 RVA: 0x000099F8 File Offset: 0x00007BF8
		public ResourceType(string resourceType)
		{
			Argument.AssertNotNullOrWhiteSpace(resourceType, "resourceType");
			int num = resourceType.IndexOf('/');
			if (num == -1 || resourceType.Length < 3)
			{
				throw new ArgumentOutOfRangeException("resourceType");
			}
			this._stringValue = resourceType;
			this.Namespace = resourceType.Substring(0, num);
			this.Type = resourceType.Substring(num + 1);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00009A55 File Offset: 0x00007C55
		internal ResourceType(string providerNamespace, string name)
		{
			this.Namespace = providerNamespace;
			this.Type = name;
			this._stringValue = this.Namespace + "/" + this.Type;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00009A81 File Offset: 0x00007C81
		private ResourceType(string providerNamespace, string name, string fullName)
		{
			this.Namespace = providerNamespace;
			this.Type = name;
			this._stringValue = fullName;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00009A98 File Offset: 0x00007C98
		internal ResourceType AppendChild(string childType)
		{
			return new ResourceType(this.Namespace, string.Format("{0}{1}{2}", this.Type, '/', childType));
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00009ABD File Offset: 0x00007CBD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string GetLastType()
		{
			return this.Type.Substring(this.Type.LastIndexOf('/') + 1);
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000338 RID: 824 RVA: 0x00009AD9 File Offset: 0x00007CD9
		public string Namespace { get; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00009AE1 File Offset: 0x00007CE1
		public string Type { get; }

		// Token: 0x0600033A RID: 826 RVA: 0x00009AE9 File Offset: 0x00007CE9
		public static implicit operator ResourceType(string resourceType)
		{
			return new ResourceType(resourceType);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00009AF1 File Offset: 0x00007CF1
		public static implicit operator string(ResourceType resourceType)
		{
			return resourceType._stringValue;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00009AF9 File Offset: 0x00007CF9
		public static bool operator ==(ResourceType left, ResourceType right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00009B03 File Offset: 0x00007D03
		public static bool operator !=(ResourceType left, ResourceType right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00009B10 File Offset: 0x00007D10
		public bool Equals(ResourceType other)
		{
			return string.Equals(this._stringValue, other._stringValue, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00009B24 File Offset: 0x00007D24
		public override string ToString()
		{
			return this._stringValue;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00009B2C File Offset: 0x00007D2C
		[NullableContext(2)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			if (other is ResourceType)
			{
				ResourceType resourceType = (ResourceType)other;
				return this.Equals(resourceType);
			}
			string text = other as string;
			return text != null && this.Equals(text);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00009B6D File Offset: 0x00007D6D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return StringComparer.OrdinalIgnoreCase.GetHashCode(this._stringValue);
		}

		// Token: 0x04000158 RID: 344
		internal static ResourceType Tenant = new ResourceType("Microsoft.Resources", "tenants", "Microsoft.Resources/tenants");

		// Token: 0x04000159 RID: 345
		internal static ResourceType Subscription = new ResourceType("Microsoft.Resources", "subscriptions", "Microsoft.Resources/subscriptions");

		// Token: 0x0400015A RID: 346
		internal static ResourceType ResourceGroup = new ResourceType("Microsoft.Resources", "resourceGroups", "Microsoft.Resources/resourceGroups");

		// Token: 0x0400015B RID: 347
		internal static ResourceType Provider = new ResourceType("Microsoft.Resources", "providers", "Microsoft.Resources/providers");

		// Token: 0x0400015C RID: 348
		internal const string ResourceNamespace = "Microsoft.Resources";

		// Token: 0x0400015D RID: 349
		private readonly string _stringValue;
	}
}
