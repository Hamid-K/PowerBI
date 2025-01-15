using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;
using Microsoft.IdentityModel.Json.Linq;
using Microsoft.IdentityModel.Json.Utilities;

namespace Microsoft.IdentityModel.Json.Serialization
{
	// Token: 0x02000092 RID: 146
	[NullableContext(2)]
	[Nullable(0)]
	internal class JsonObjectContract : JsonContainerContract
	{
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x0001D526 File Offset: 0x0001B726
		// (set) Token: 0x06000706 RID: 1798 RVA: 0x0001D52E File Offset: 0x0001B72E
		public MemberSerialization MemberSerialization { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x0001D537 File Offset: 0x0001B737
		// (set) Token: 0x06000708 RID: 1800 RVA: 0x0001D53F File Offset: 0x0001B73F
		public MissingMemberHandling? MissingMemberHandling { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x0001D548 File Offset: 0x0001B748
		// (set) Token: 0x0600070A RID: 1802 RVA: 0x0001D550 File Offset: 0x0001B750
		public Required? ItemRequired { get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x0001D559 File Offset: 0x0001B759
		// (set) Token: 0x0600070C RID: 1804 RVA: 0x0001D561 File Offset: 0x0001B761
		public NullValueHandling? ItemNullValueHandling { get; set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x0001D56A File Offset: 0x0001B76A
		[Nullable(1)]
		public JsonPropertyCollection Properties
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x0001D572 File Offset: 0x0001B772
		[Nullable(1)]
		public JsonPropertyCollection CreatorParameters
		{
			[NullableContext(1)]
			get
			{
				if (this._creatorParameters == null)
				{
					this._creatorParameters = new JsonPropertyCollection(base.UnderlyingType);
				}
				return this._creatorParameters;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x0001D593 File Offset: 0x0001B793
		// (set) Token: 0x06000710 RID: 1808 RVA: 0x0001D59B File Offset: 0x0001B79B
		[Nullable(new byte[] { 2, 1 })]
		public ObjectConstructor<object> OverrideCreator
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return this._overrideCreator;
			}
			[param: Nullable(new byte[] { 2, 1 })]
			set
			{
				this._overrideCreator = value;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x0001D5A4 File Offset: 0x0001B7A4
		// (set) Token: 0x06000712 RID: 1810 RVA: 0x0001D5AC File Offset: 0x0001B7AC
		[Nullable(new byte[] { 2, 1 })]
		internal ObjectConstructor<object> ParameterizedCreator
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return this._parameterizedCreator;
			}
			[param: Nullable(new byte[] { 2, 1 })]
			set
			{
				this._parameterizedCreator = value;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x0001D5B5 File Offset: 0x0001B7B5
		// (set) Token: 0x06000714 RID: 1812 RVA: 0x0001D5BD File Offset: 0x0001B7BD
		public ExtensionDataSetter ExtensionDataSetter { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x0001D5C6 File Offset: 0x0001B7C6
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x0001D5CE File Offset: 0x0001B7CE
		public ExtensionDataGetter ExtensionDataGetter { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0001D5D7 File Offset: 0x0001B7D7
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x0001D5DF File Offset: 0x0001B7DF
		public Type ExtensionDataValueType
		{
			get
			{
				return this._extensionDataValueType;
			}
			set
			{
				this._extensionDataValueType = value;
				this.ExtensionDataIsJToken = value != null && typeof(JToken).IsAssignableFrom(value);
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x0001D60A File Offset: 0x0001B80A
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x0001D612 File Offset: 0x0001B812
		[Nullable(new byte[] { 2, 1, 1 })]
		public Func<string, string> ExtensionDataNameResolver
		{
			[return: Nullable(new byte[] { 2, 1, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1, 1 })]
			set;
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x0001D61C File Offset: 0x0001B81C
		internal bool HasRequiredOrDefaultValueProperties
		{
			get
			{
				if (this._hasRequiredOrDefaultValueProperties == null)
				{
					this._hasRequiredOrDefaultValueProperties = new bool?(false);
					if (this.ItemRequired.GetValueOrDefault(Required.Default) != Required.Default)
					{
						this._hasRequiredOrDefaultValueProperties = new bool?(true);
					}
					else
					{
						foreach (JsonProperty jsonProperty in this.Properties)
						{
							if (jsonProperty.Required == Required.Default)
							{
								DefaultValueHandling? defaultValueHandling = jsonProperty.DefaultValueHandling & DefaultValueHandling.Populate;
								DefaultValueHandling defaultValueHandling2 = DefaultValueHandling.Populate;
								if (!((defaultValueHandling.GetValueOrDefault() == defaultValueHandling2) & (defaultValueHandling != null)))
								{
									continue;
								}
							}
							this._hasRequiredOrDefaultValueProperties = new bool?(true);
							break;
						}
					}
				}
				return this._hasRequiredOrDefaultValueProperties.GetValueOrDefault();
			}
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0001D708 File Offset: 0x0001B908
		[NullableContext(1)]
		public JsonObjectContract(Type underlyingType)
			: base(underlyingType)
		{
			this.ContractType = JsonContractType.Object;
			this.Properties = new JsonPropertyCollection(base.UnderlyingType);
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0001D729 File Offset: 0x0001B929
		[NullableContext(1)]
		[SecuritySafeCritical]
		internal object GetUninitializedObject()
		{
			if (!JsonTypeReflector.FullyTrusted)
			{
				throw new JsonException("Insufficient permissions. Creating an uninitialized '{0}' type requires full trust.".FormatWith(CultureInfo.InvariantCulture, this.NonNullableUnderlyingType));
			}
			return FormatterServices.GetUninitializedObject(this.NonNullableUnderlyingType);
		}

		// Token: 0x0400029C RID: 668
		internal bool ExtensionDataIsJToken;

		// Token: 0x0400029D RID: 669
		private bool? _hasRequiredOrDefaultValueProperties;

		// Token: 0x0400029E RID: 670
		[Nullable(new byte[] { 2, 1 })]
		private ObjectConstructor<object> _overrideCreator;

		// Token: 0x0400029F RID: 671
		[Nullable(new byte[] { 2, 1 })]
		private ObjectConstructor<object> _parameterizedCreator;

		// Token: 0x040002A0 RID: 672
		private JsonPropertyCollection _creatorParameters;

		// Token: 0x040002A1 RID: 673
		private Type _extensionDataValueType;
	}
}
