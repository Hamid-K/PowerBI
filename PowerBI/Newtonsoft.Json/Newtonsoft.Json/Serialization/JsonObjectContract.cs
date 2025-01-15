using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000091 RID: 145
	[NullableContext(2)]
	[Nullable(0)]
	public class JsonObjectContract : JsonContainerContract
	{
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x0001D4DE File Offset: 0x0001B6DE
		// (set) Token: 0x06000705 RID: 1797 RVA: 0x0001D4E6 File Offset: 0x0001B6E6
		public MemberSerialization MemberSerialization { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x0001D4EF File Offset: 0x0001B6EF
		// (set) Token: 0x06000707 RID: 1799 RVA: 0x0001D4F7 File Offset: 0x0001B6F7
		public MissingMemberHandling? MissingMemberHandling { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x0001D500 File Offset: 0x0001B700
		// (set) Token: 0x06000709 RID: 1801 RVA: 0x0001D508 File Offset: 0x0001B708
		public Required? ItemRequired { get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x0001D511 File Offset: 0x0001B711
		// (set) Token: 0x0600070B RID: 1803 RVA: 0x0001D519 File Offset: 0x0001B719
		public NullValueHandling? ItemNullValueHandling { get; set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x0001D522 File Offset: 0x0001B722
		[Nullable(1)]
		public JsonPropertyCollection Properties
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x0001D52A File Offset: 0x0001B72A
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
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x0001D54B File Offset: 0x0001B74B
		// (set) Token: 0x0600070F RID: 1807 RVA: 0x0001D553 File Offset: 0x0001B753
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
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x0001D55C File Offset: 0x0001B75C
		// (set) Token: 0x06000711 RID: 1809 RVA: 0x0001D564 File Offset: 0x0001B764
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
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x0001D56D File Offset: 0x0001B76D
		// (set) Token: 0x06000713 RID: 1811 RVA: 0x0001D575 File Offset: 0x0001B775
		public ExtensionDataSetter ExtensionDataSetter { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x0001D57E File Offset: 0x0001B77E
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x0001D586 File Offset: 0x0001B786
		public ExtensionDataGetter ExtensionDataGetter { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x0001D58F File Offset: 0x0001B78F
		// (set) Token: 0x06000717 RID: 1815 RVA: 0x0001D597 File Offset: 0x0001B797
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
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x0001D5C2 File Offset: 0x0001B7C2
		// (set) Token: 0x06000719 RID: 1817 RVA: 0x0001D5CA File Offset: 0x0001B7CA
		[Nullable(new byte[] { 2, 1, 1 })]
		public Func<string, string> ExtensionDataNameResolver
		{
			[return: Nullable(new byte[] { 2, 1, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1, 1 })]
			set;
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x0001D5D4 File Offset: 0x0001B7D4
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

		// Token: 0x0600071B RID: 1819 RVA: 0x0001D6C0 File Offset: 0x0001B8C0
		[NullableContext(1)]
		public JsonObjectContract(Type underlyingType)
			: base(underlyingType)
		{
			this.ContractType = JsonContractType.Object;
			this.Properties = new JsonPropertyCollection(base.UnderlyingType);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0001D6E1 File Offset: 0x0001B8E1
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

		// Token: 0x0400029B RID: 667
		internal bool ExtensionDataIsJToken;

		// Token: 0x0400029C RID: 668
		private bool? _hasRequiredOrDefaultValueProperties;

		// Token: 0x0400029D RID: 669
		[Nullable(new byte[] { 2, 1 })]
		private ObjectConstructor<object> _overrideCreator;

		// Token: 0x0400029E RID: 670
		[Nullable(new byte[] { 2, 1 })]
		private ObjectConstructor<object> _parameterizedCreator;

		// Token: 0x0400029F RID: 671
		private JsonPropertyCollection _creatorParameters;

		// Token: 0x040002A0 RID: 672
		private Type _extensionDataValueType;
	}
}
