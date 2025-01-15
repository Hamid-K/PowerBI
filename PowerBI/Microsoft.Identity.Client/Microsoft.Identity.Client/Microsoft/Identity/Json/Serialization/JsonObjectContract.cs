using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;
using Microsoft.Identity.Json.Linq;
using Microsoft.Identity.Json.Utilities;

namespace Microsoft.Identity.Json.Serialization
{
	// Token: 0x02000091 RID: 145
	[NullableContext(2)]
	[Nullable(0)]
	internal class JsonObjectContract : JsonContainerContract
	{
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x0001CF52 File Offset: 0x0001B152
		// (set) Token: 0x060006FC RID: 1788 RVA: 0x0001CF5A File Offset: 0x0001B15A
		public MemberSerialization MemberSerialization { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x0001CF63 File Offset: 0x0001B163
		// (set) Token: 0x060006FE RID: 1790 RVA: 0x0001CF6B File Offset: 0x0001B16B
		public MissingMemberHandling? MissingMemberHandling { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x0001CF74 File Offset: 0x0001B174
		// (set) Token: 0x06000700 RID: 1792 RVA: 0x0001CF7C File Offset: 0x0001B17C
		public Required? ItemRequired { get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x0001CF85 File Offset: 0x0001B185
		// (set) Token: 0x06000702 RID: 1794 RVA: 0x0001CF8D File Offset: 0x0001B18D
		public NullValueHandling? ItemNullValueHandling { get; set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x0001CF96 File Offset: 0x0001B196
		[Nullable(0)]
		public JsonPropertyCollection Properties
		{
			[NullableContext(0)]
			get;
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x0001CF9E File Offset: 0x0001B19E
		[Nullable(0)]
		public JsonPropertyCollection CreatorParameters
		{
			[NullableContext(0)]
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
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x0001CFBF File Offset: 0x0001B1BF
		// (set) Token: 0x06000706 RID: 1798 RVA: 0x0001CFC7 File Offset: 0x0001B1C7
		[Nullable(new byte[] { 2, 0 })]
		public ObjectConstructor<object> OverrideCreator
		{
			[return: Nullable(new byte[] { 2, 0 })]
			get
			{
				return this._overrideCreator;
			}
			[param: Nullable(new byte[] { 2, 0 })]
			set
			{
				this._overrideCreator = value;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x0001CFD0 File Offset: 0x0001B1D0
		// (set) Token: 0x06000708 RID: 1800 RVA: 0x0001CFD8 File Offset: 0x0001B1D8
		[Nullable(new byte[] { 2, 0 })]
		internal ObjectConstructor<object> ParameterizedCreator
		{
			[return: Nullable(new byte[] { 2, 0 })]
			get
			{
				return this._parameterizedCreator;
			}
			[param: Nullable(new byte[] { 2, 0 })]
			set
			{
				this._parameterizedCreator = value;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x0001CFE1 File Offset: 0x0001B1E1
		// (set) Token: 0x0600070A RID: 1802 RVA: 0x0001CFE9 File Offset: 0x0001B1E9
		public ExtensionDataSetter ExtensionDataSetter { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x0001CFF2 File Offset: 0x0001B1F2
		// (set) Token: 0x0600070C RID: 1804 RVA: 0x0001CFFA File Offset: 0x0001B1FA
		public ExtensionDataGetter ExtensionDataGetter { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x0001D003 File Offset: 0x0001B203
		// (set) Token: 0x0600070E RID: 1806 RVA: 0x0001D00B File Offset: 0x0001B20B
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
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x0001D036 File Offset: 0x0001B236
		// (set) Token: 0x06000710 RID: 1808 RVA: 0x0001D03E File Offset: 0x0001B23E
		[Nullable(new byte[] { 2, 0, 0 })]
		public Func<string, string> ExtensionDataNameResolver
		{
			[return: Nullable(new byte[] { 2, 0, 0 })]
			get;
			[param: Nullable(new byte[] { 2, 0, 0 })]
			set;
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x0001D048 File Offset: 0x0001B248
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

		// Token: 0x06000712 RID: 1810 RVA: 0x0001D134 File Offset: 0x0001B334
		[NullableContext(0)]
		public JsonObjectContract(Type underlyingType)
			: base(underlyingType)
		{
			this.ContractType = JsonContractType.Object;
			this.Properties = new JsonPropertyCollection(base.UnderlyingType);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0001D155 File Offset: 0x0001B355
		[NullableContext(0)]
		[SecuritySafeCritical]
		internal object GetUninitializedObject()
		{
			if (!JsonTypeReflector.FullyTrusted)
			{
				throw new JsonException("Insufficient permissions. Creating an uninitialized '{0}' type requires full trust.".FormatWith(CultureInfo.InvariantCulture, this.NonNullableUnderlyingType));
			}
			return FormatterServices.GetUninitializedObject(this.NonNullableUnderlyingType);
		}

		// Token: 0x04000281 RID: 641
		internal bool ExtensionDataIsJToken;

		// Token: 0x04000282 RID: 642
		private bool? _hasRequiredOrDefaultValueProperties;

		// Token: 0x04000283 RID: 643
		[Nullable(new byte[] { 2, 0 })]
		private ObjectConstructor<object> _overrideCreator;

		// Token: 0x04000284 RID: 644
		[Nullable(new byte[] { 2, 0 })]
		private ObjectConstructor<object> _parameterizedCreator;

		// Token: 0x04000285 RID: 645
		private JsonPropertyCollection _creatorParameters;

		// Token: 0x04000286 RID: 646
		private Type _extensionDataValueType;
	}
}
