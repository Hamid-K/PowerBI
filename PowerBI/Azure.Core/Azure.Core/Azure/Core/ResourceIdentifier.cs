using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Azure.Core
{
	// Token: 0x0200005C RID: 92
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ResourceIdentifier : IEquatable<ResourceIdentifier>, IComparable<ResourceIdentifier>
	{
		// Token: 0x0600030C RID: 780 RVA: 0x00009088 File Offset: 0x00007288
		public ResourceIdentifier(string resourceId)
		{
			Argument.AssertNotNullOrEmpty(resourceId, "resourceId");
			this._stringValue = resourceId;
			if (resourceId.Length == 1 && resourceId[0] == '/')
			{
				this.Init(null, ResourceType.Tenant, string.Empty, false, ResourceIdentifier.SpecialType.None);
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x000090D4 File Offset: 0x000072D4
		private ResourceIdentifier([Nullable(2)] ResourceIdentifier parent, ResourceType resourceType, string resourceName, bool isProviderResource, ResourceIdentifier.SpecialType specialType)
		{
			this.Init(parent, resourceType, resourceName, isProviderResource, specialType);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x000090EC File Offset: 0x000072EC
		private void Init([Nullable(2)] ResourceIdentifier parent, ResourceType resourceType, string resourceName, bool isProviderResource, ResourceIdentifier.SpecialType specialType)
		{
			if (parent != null)
			{
				this._provider = parent.Provider;
				this._subscriptionId = parent.SubscriptionId;
				this._location = parent.Location;
				this._resourceGroupName = parent.ResourceGroupName;
			}
			switch (specialType)
			{
			case ResourceIdentifier.SpecialType.Subscription:
				this._subscriptionId = resourceName;
				break;
			case ResourceIdentifier.SpecialType.ResourceGroup:
				this._resourceGroupName = resourceName;
				break;
			case ResourceIdentifier.SpecialType.Location:
				this._location = new AzureLocation?(resourceName);
				break;
			case ResourceIdentifier.SpecialType.Provider:
				this._provider = resourceName;
				break;
			}
			this._resourceType = resourceType;
			this._name = resourceName;
			this._isProviderResource = isProviderResource;
			this._parent = parent;
			if (parent == null)
			{
				this._stringValue = "/";
			}
			this._initialized = true;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x000091A8 File Offset: 0x000073A8
		[NullableContext(2)]
		private unsafe string Parse()
		{
			ReadOnlySpan<char> readOnlySpan = MemoryExtensions.AsSpan(this._stringValue);
			if (!MemoryExtensions.StartsWith(readOnlySpan, MemoryExtensions.AsSpan("/subscriptions/"), StringComparison.OrdinalIgnoreCase) && !MemoryExtensions.StartsWith(readOnlySpan, MemoryExtensions.AsSpan("/providers/"), StringComparison.OrdinalIgnoreCase))
			{
				return "The ResourceIdentifier must start with /subscriptions/ or /providers/.";
			}
			readOnlySpan = ((*readOnlySpan[readOnlySpan.Length - 1] == 47) ? readOnlySpan.Slice(1, readOnlySpan.Length - 2) : readOnlySpan.Slice(1));
			ReadOnlySpan<char> readOnlySpan2 = ResourceIdentifier.PopNextWord(ref readOnlySpan);
			ResourceIdentifier.ResourceIdentifierParts? resourceIdentifierParts;
			string text = ResourceIdentifier.GetNextParts(ResourceIdentifier.Root, ref readOnlySpan, ref readOnlySpan2, out resourceIdentifierParts);
			if (text != null)
			{
				return text;
			}
			ResourceIdentifier.ResourceIdentifierParts resourceIdentifierParts2 = resourceIdentifierParts.Value;
			while (!readOnlySpan2.IsEmpty)
			{
				ResourceIdentifier resourceIdentifier = new ResourceIdentifier(resourceIdentifierParts2.Parent, resourceIdentifierParts2.ResourceType, resourceIdentifierParts2.ResourceName, resourceIdentifierParts2.IsProviderResource, resourceIdentifierParts2.SpecialType);
				if (resourceIdentifierParts2.SpecialType == ResourceIdentifier.SpecialType.Subscription)
				{
					text = resourceIdentifier.CheckSubscriptionFormat();
					if (text != null)
					{
						return text;
					}
				}
				text = ResourceIdentifier.GetNextParts(resourceIdentifier, ref readOnlySpan, ref readOnlySpan2, out resourceIdentifierParts);
				if (text != null)
				{
					return text;
				}
				resourceIdentifierParts2 = resourceIdentifierParts.Value;
			}
			this.Init(resourceIdentifierParts2.Parent, resourceIdentifierParts2.ResourceType, resourceIdentifierParts2.ResourceName, resourceIdentifierParts2.IsProviderResource, resourceIdentifierParts2.SpecialType);
			if (resourceIdentifierParts2.SpecialType != ResourceIdentifier.SpecialType.Subscription)
			{
				return null;
			}
			return this.CheckSubscriptionFormat();
		}

		// Token: 0x06000310 RID: 784 RVA: 0x000092E8 File Offset: 0x000074E8
		[NullableContext(2)]
		private string CheckSubscriptionFormat()
		{
			Guid guid;
			if (this._subscriptionId != null && !Guid.TryParse(this._subscriptionId, out guid))
			{
				return "The GUID for subscription is invalid " + this._subscriptionId + ".";
			}
			return null;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00009324 File Offset: 0x00007524
		private T GetValue<[Nullable(2)] T>(ref T value)
		{
			if (!this._initialized)
			{
				string text = this.Parse();
				if (text != null)
				{
					throw new FormatException(text);
				}
			}
			return value;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00009350 File Offset: 0x00007550
		[NullableContext(0)]
		private static ResourceType ChooseResourceType(ReadOnlySpan<char> resourceTypeName, [Nullable(1)] ResourceIdentifier parent, out ResourceIdentifier.SpecialType specialType)
		{
			if (MemoryExtensions.Equals(resourceTypeName, MemoryExtensions.AsSpan("resourcegroups"), StringComparison.OrdinalIgnoreCase))
			{
				specialType = ResourceIdentifier.SpecialType.ResourceGroup;
				if (parent.ResourceType == ResourceType.Subscription)
				{
					return ResourceType.ResourceGroup;
				}
			}
			else
			{
				if (MemoryExtensions.Equals(resourceTypeName, MemoryExtensions.AsSpan("subscriptions"), StringComparison.OrdinalIgnoreCase) && parent.ResourceType == ResourceType.Tenant)
				{
					specialType = ResourceIdentifier.SpecialType.Subscription;
					return ResourceType.Subscription;
				}
				specialType = (MemoryExtensions.Equals(resourceTypeName, MemoryExtensions.AsSpan("locations"), StringComparison.OrdinalIgnoreCase) ? ResourceIdentifier.SpecialType.Location : ResourceIdentifier.SpecialType.None);
			}
			return parent.ResourceType.AppendChild(resourceTypeName.ToString());
		}

		// Token: 0x06000313 RID: 787 RVA: 0x000093F0 File Offset: 0x000075F0
		[NullableContext(0)]
		[return: Nullable(2)]
		private static string GetNextParts([Nullable(1)] ResourceIdentifier parent, ref ReadOnlySpan<char> remaining, ref ReadOnlySpan<char> nextWord, out ResourceIdentifier.ResourceIdentifierParts? parts)
		{
			parts = null;
			ReadOnlySpan<char> readOnlySpan = nextWord;
			ReadOnlySpan<char> readOnlySpan2 = ResourceIdentifier.PopNextWord(ref remaining);
			if (readOnlySpan2.IsEmpty)
			{
				if (MemoryExtensions.Equals(readOnlySpan, MemoryExtensions.AsSpan("subscriptions"), StringComparison.OrdinalIgnoreCase) || MemoryExtensions.Equals(readOnlySpan, MemoryExtensions.AsSpan("resourcegroups"), StringComparison.OrdinalIgnoreCase))
				{
					return "The ResourceIdentifier is missing the key for " + readOnlySpan.ToString() + ".";
				}
				if (parent.ResourceType == ResourceType.ResourceGroup)
				{
					return "Expected providers path segment after resourcegroups.";
				}
				nextWord = readOnlySpan2;
				ResourceIdentifier.SpecialType specialType = (MemoryExtensions.Equals(readOnlySpan, MemoryExtensions.AsSpan("locations"), StringComparison.OrdinalIgnoreCase) ? ResourceIdentifier.SpecialType.Location : ResourceIdentifier.SpecialType.None);
				ResourceType resourceType = parent.ResourceType.AppendChild(readOnlySpan.ToString());
				parts = new ResourceIdentifier.ResourceIdentifierParts?(new ResourceIdentifier.ResourceIdentifierParts(parent, new ResourceType(resourceType), string.Empty, false, specialType));
				return null;
			}
			else
			{
				ReadOnlySpan<char> readOnlySpan3 = ResourceIdentifier.PopNextWord(ref remaining);
				if (!MemoryExtensions.Equals(readOnlySpan, MemoryExtensions.AsSpan("providers"), StringComparison.OrdinalIgnoreCase))
				{
					nextWord = readOnlySpan3;
					ResourceIdentifier.SpecialType specialType2;
					parts = new ResourceIdentifier.ResourceIdentifierParts?(new ResourceIdentifier.ResourceIdentifierParts(parent, ResourceIdentifier.ChooseResourceType(readOnlySpan, parent, out specialType2), readOnlySpan2.ToString(), false, specialType2));
					return null;
				}
				if (readOnlySpan3.IsEmpty || MemoryExtensions.Equals(readOnlySpan3, MemoryExtensions.AsSpan("providers"), StringComparison.OrdinalIgnoreCase))
				{
					if (parent.ResourceType == ResourceType.Subscription || parent.ResourceType == ResourceType.Tenant)
					{
						nextWord = readOnlySpan3;
						parts = new ResourceIdentifier.ResourceIdentifierParts?(new ResourceIdentifier.ResourceIdentifierParts(parent, ResourceType.Provider, readOnlySpan2.ToString(), true, ResourceIdentifier.SpecialType.Provider));
						return null;
					}
					return "Provider resource can only come after the root or subscriptions.";
				}
				else
				{
					ReadOnlySpan<char> readOnlySpan4 = ResourceIdentifier.PopNextWord(ref remaining);
					if (!readOnlySpan4.IsEmpty)
					{
						nextWord = ResourceIdentifier.PopNextWord(ref remaining);
						ResourceIdentifier.SpecialType specialType3 = (MemoryExtensions.Equals(readOnlySpan3, MemoryExtensions.AsSpan("locations"), StringComparison.OrdinalIgnoreCase) ? ResourceIdentifier.SpecialType.Location : ResourceIdentifier.SpecialType.None);
						parts = new ResourceIdentifier.ResourceIdentifierParts?(new ResourceIdentifier.ResourceIdentifierParts(parent, new ResourceType(readOnlySpan2.ToString(), readOnlySpan3.ToString()), readOnlySpan4.ToString(), true, specialType3));
						return null;
					}
					return "Invalid resource id.";
				}
			}
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00009620 File Offset: 0x00007820
		[NullableContext(0)]
		private static ReadOnlySpan<char> PopNextWord([ScopedRef] ref ReadOnlySpan<char> remaining)
		{
			int num = MemoryExtensions.IndexOf<char>(remaining, '/');
			if (num < 0)
			{
				ReadOnlySpan<char> readOnlySpan = remaining.Slice(0);
				remaining = ReadOnlySpan<char>.Empty;
				return readOnlySpan;
			}
			ReadOnlySpan<char> readOnlySpan2 = remaining.Slice(0, num);
			remaining = remaining.Slice(num + 1);
			return readOnlySpan2;
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000966C File Offset: 0x0000786C
		private string StringValue
		{
			get
			{
				string text;
				if ((text = this._stringValue) == null)
				{
					text = (this._stringValue = this.ToResourceString());
				}
				return text;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000316 RID: 790 RVA: 0x00009692 File Offset: 0x00007892
		public ResourceType ResourceType
		{
			get
			{
				return this.GetValue<ResourceType>(ref this._resourceType);
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000317 RID: 791 RVA: 0x000096A0 File Offset: 0x000078A0
		public string Name
		{
			get
			{
				return this.GetValue<string>(ref this._name);
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000318 RID: 792 RVA: 0x000096AE File Offset: 0x000078AE
		[Nullable(2)]
		public ResourceIdentifier Parent
		{
			[NullableContext(2)]
			get
			{
				return this.GetValue<ResourceIdentifier>(ref this._parent);
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000319 RID: 793 RVA: 0x000096BC File Offset: 0x000078BC
		internal bool IsProviderResource
		{
			get
			{
				return this.GetValue<bool>(ref this._isProviderResource);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600031A RID: 794 RVA: 0x000096CA File Offset: 0x000078CA
		[Nullable(2)]
		public string SubscriptionId
		{
			[NullableContext(2)]
			get
			{
				return this.GetValue<string>(ref this._subscriptionId);
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600031B RID: 795 RVA: 0x000096D8 File Offset: 0x000078D8
		[Nullable(2)]
		public string Provider
		{
			[NullableContext(2)]
			get
			{
				return this.GetValue<string>(ref this._provider);
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600031C RID: 796 RVA: 0x000096E6 File Offset: 0x000078E6
		public AzureLocation? Location
		{
			get
			{
				return this.GetValue<AzureLocation?>(ref this._location);
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600031D RID: 797 RVA: 0x000096F4 File Offset: 0x000078F4
		[Nullable(2)]
		public string ResourceGroupName
		{
			[NullableContext(2)]
			get
			{
				return this.GetValue<string>(ref this._resourceGroupName);
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00009704 File Offset: 0x00007904
		private string ToResourceString()
		{
			if (this.Parent == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder((this.Parent == ResourceIdentifier.Root) ? string.Empty : this.Parent.StringValue);
			if (!this.IsProviderResource)
			{
				stringBuilder.Append('/').Append(this.ResourceType.GetLastType());
				if (!string.IsNullOrWhiteSpace(this.Name))
				{
					stringBuilder.Append('/').Append(this.Name);
				}
			}
			else
			{
				stringBuilder.Append("/providers/").Append(this.ResourceType).Append('/')
					.Append(this.Name);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600031F RID: 799 RVA: 0x000097C4 File Offset: 0x000079C4
		public override string ToString()
		{
			return this.StringValue;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x000097CC File Offset: 0x000079CC
		[NullableContext(2)]
		public bool Equals(ResourceIdentifier other)
		{
			return other != null && this.StringValue.Equals(other.StringValue, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x000097E5 File Offset: 0x000079E5
		[NullableContext(2)]
		public int CompareTo(ResourceIdentifier other)
		{
			if (other == null)
			{
				return 1;
			}
			return string.Compare(this.StringValue, other.StringValue, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00009800 File Offset: 0x00007A00
		[NullableContext(2)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			ResourceIdentifier resourceIdentifier = obj as ResourceIdentifier;
			if (resourceIdentifier != null)
			{
				return resourceIdentifier.Equals(this);
			}
			string text = obj as string;
			return text != null && this.Equals(new ResourceIdentifier(text));
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00009837 File Offset: 0x00007A37
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return StringComparer.OrdinalIgnoreCase.GetHashCode(this.StringValue);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00009849 File Offset: 0x00007A49
		[return: Nullable(2)]
		public static implicit operator string(ResourceIdentifier id)
		{
			if (id == null)
			{
				return null;
			}
			return id.StringValue;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00009856 File Offset: 0x00007A56
		public static bool operator ==(ResourceIdentifier left, ResourceIdentifier right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00009867 File Offset: 0x00007A67
		public static bool operator !=(ResourceIdentifier left, ResourceIdentifier right)
		{
			if (left == null)
			{
				return right != null;
			}
			return !left.Equals(right);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000987B File Offset: 0x00007A7B
		public static bool operator <(ResourceIdentifier left, ResourceIdentifier right)
		{
			if (left != null)
			{
				return left.CompareTo(right) < 0;
			}
			return right != null;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000988F File Offset: 0x00007A8F
		public static bool operator <=(ResourceIdentifier left, ResourceIdentifier right)
		{
			return left == null || left.CompareTo(right) <= 0;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x000098A3 File Offset: 0x00007AA3
		public static bool operator >(ResourceIdentifier left, ResourceIdentifier right)
		{
			return left != null && left.CompareTo(right) > 0;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x000098B4 File Offset: 0x00007AB4
		public static bool operator >=(ResourceIdentifier left, ResourceIdentifier right)
		{
			if (left != null)
			{
				return left.CompareTo(right) >= 0;
			}
			return right == null;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x000098CC File Offset: 0x00007ACC
		public static ResourceIdentifier Parse(string input)
		{
			ResourceIdentifier resourceIdentifier = new ResourceIdentifier(input);
			string text = resourceIdentifier.Parse();
			if (text != null)
			{
				throw new FormatException(text);
			}
			return resourceIdentifier;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x000098F0 File Offset: 0x00007AF0
		[NullableContext(2)]
		public static bool TryParse(string input, out ResourceIdentifier result)
		{
			result = null;
			if (string.IsNullOrEmpty(input))
			{
				return false;
			}
			result = new ResourceIdentifier(input);
			if (result.Parse() == null)
			{
				return true;
			}
			result = null;
			return false;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00009918 File Offset: 0x00007B18
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ResourceIdentifier AppendProviderResource(string providerNamespace, string resourceType, string resourceName)
		{
			ResourceIdentifier.ValidateProviderResourceParameters(providerNamespace, resourceType, resourceName);
			ResourceIdentifier.SpecialType specialType = (resourceType.Equals("locations", StringComparison.OrdinalIgnoreCase) ? ResourceIdentifier.SpecialType.Location : ResourceIdentifier.SpecialType.None);
			return new ResourceIdentifier(this, new ResourceType(providerNamespace, resourceType), resourceName, true, specialType);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00009950 File Offset: 0x00007B50
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ResourceIdentifier AppendChildResource(string childResourceType, string childResourceName)
		{
			ResourceIdentifier.ValidateChildResourceParameters(childResourceType, childResourceName);
			ResourceIdentifier.SpecialType specialType;
			return new ResourceIdentifier(this, ResourceIdentifier.ChooseResourceType(MemoryExtensions.AsSpan(childResourceType), this, out specialType), childResourceName, false, specialType);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000997B File Offset: 0x00007B7B
		private static void ValidateProviderResourceParameters(string providerNamespace, string resourceType, string resourceName)
		{
			ResourceIdentifier.ValidatePathSegment(providerNamespace, "providerNamespace");
			ResourceIdentifier.ValidatePathSegment(resourceType, "resourceType");
			ResourceIdentifier.ValidatePathSegment(resourceName, "resourceName");
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000999E File Offset: 0x00007B9E
		private static void ValidateChildResourceParameters(string childResourceType, string childResourceName)
		{
			ResourceIdentifier.ValidatePathSegment(childResourceType, "childResourceType");
			ResourceIdentifier.ValidatePathSegment(childResourceName, "childResourceName");
		}

		// Token: 0x06000331 RID: 817 RVA: 0x000099B6 File Offset: 0x00007BB6
		private static void ValidatePathSegment(string segment, string parameterName)
		{
			Argument.AssertNotNullOrWhiteSpace(segment, "segment");
			if (segment.Contains('/'))
			{
				throw new ArgumentOutOfRangeException(parameterName, parameterName + " must be a single path segment");
			}
		}

		// Token: 0x04000145 RID: 325
		internal const char Separator = '/';

		// Token: 0x04000146 RID: 326
		private const string RootStringValue = "/";

		// Token: 0x04000147 RID: 327
		private const string ProvidersKey = "providers";

		// Token: 0x04000148 RID: 328
		private const string SubscriptionsKey = "subscriptions";

		// Token: 0x04000149 RID: 329
		private const string LocationsKey = "locations";

		// Token: 0x0400014A RID: 330
		private const string ResourceGroupKey = "resourcegroups";

		// Token: 0x0400014B RID: 331
		private const string SubscriptionStart = "/subscriptions/";

		// Token: 0x0400014C RID: 332
		private const string ProviderStart = "/providers/";

		// Token: 0x0400014D RID: 333
		private bool _initialized;

		// Token: 0x0400014E RID: 334
		[Nullable(2)]
		private string _stringValue;

		// Token: 0x0400014F RID: 335
		private ResourceType _resourceType;

		// Token: 0x04000150 RID: 336
		private string _name;

		// Token: 0x04000151 RID: 337
		[Nullable(2)]
		private ResourceIdentifier _parent;

		// Token: 0x04000152 RID: 338
		private bool _isProviderResource;

		// Token: 0x04000153 RID: 339
		[Nullable(2)]
		private string _subscriptionId;

		// Token: 0x04000154 RID: 340
		[Nullable(2)]
		private string _provider;

		// Token: 0x04000155 RID: 341
		private AzureLocation? _location;

		// Token: 0x04000156 RID: 342
		[Nullable(2)]
		private string _resourceGroupName;

		// Token: 0x04000157 RID: 343
		public static readonly ResourceIdentifier Root = new ResourceIdentifier(null, ResourceType.Tenant, string.Empty, false, ResourceIdentifier.SpecialType.None);

		// Token: 0x020000F6 RID: 246
		[NullableContext(0)]
		private enum SpecialType
		{
			// Token: 0x04000370 RID: 880
			None,
			// Token: 0x04000371 RID: 881
			Subscription,
			// Token: 0x04000372 RID: 882
			ResourceGroup,
			// Token: 0x04000373 RID: 883
			Location,
			// Token: 0x04000374 RID: 884
			Provider
		}

		// Token: 0x020000F7 RID: 247
		[Nullable(0)]
		private readonly struct ResourceIdentifierParts
		{
			// Token: 0x06000768 RID: 1896 RVA: 0x0001A113 File Offset: 0x00018313
			public ResourceIdentifierParts(ResourceIdentifier parent, ResourceType resourceType, string resourceName, bool isProviderResource, ResourceIdentifier.SpecialType specialType)
			{
				this.Parent = parent;
				this.ResourceType = resourceType;
				this.ResourceName = resourceName;
				this.IsProviderResource = isProviderResource;
				this.SpecialType = specialType;
			}

			// Token: 0x170001C6 RID: 454
			// (get) Token: 0x06000769 RID: 1897 RVA: 0x0001A13A File Offset: 0x0001833A
			public ResourceIdentifier Parent { get; }

			// Token: 0x170001C7 RID: 455
			// (get) Token: 0x0600076A RID: 1898 RVA: 0x0001A142 File Offset: 0x00018342
			public ResourceType ResourceType { get; }

			// Token: 0x170001C8 RID: 456
			// (get) Token: 0x0600076B RID: 1899 RVA: 0x0001A14A File Offset: 0x0001834A
			public string ResourceName { get; }

			// Token: 0x170001C9 RID: 457
			// (get) Token: 0x0600076C RID: 1900 RVA: 0x0001A152 File Offset: 0x00018352
			public bool IsProviderResource { get; }

			// Token: 0x170001CA RID: 458
			// (get) Token: 0x0600076D RID: 1901 RVA: 0x0001A15A File Offset: 0x0001835A
			public ResourceIdentifier.SpecialType SpecialType { get; }
		}
	}
}
