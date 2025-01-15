using System;
using System.Runtime.CompilerServices;
using Microsoft.Identity.Json.Utilities;

namespace Microsoft.Identity.Json.Serialization
{
	// Token: 0x02000093 RID: 147
	[NullableContext(2)]
	[Nullable(0)]
	internal class JsonProperty
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x0001D2F0 File Offset: 0x0001B4F0
		// (set) Token: 0x06000719 RID: 1817 RVA: 0x0001D2F8 File Offset: 0x0001B4F8
		internal JsonContract PropertyContract { get; set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x0001D301 File Offset: 0x0001B501
		// (set) Token: 0x0600071B RID: 1819 RVA: 0x0001D309 File Offset: 0x0001B509
		public string PropertyName
		{
			get
			{
				return this._propertyName;
			}
			set
			{
				this._propertyName = value;
				this._skipPropertyNameEscape = !JavaScriptUtils.ShouldEscapeJavaScriptString(this._propertyName, JavaScriptUtils.HtmlCharEscapeFlags);
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x0001D32B File Offset: 0x0001B52B
		// (set) Token: 0x0600071D RID: 1821 RVA: 0x0001D333 File Offset: 0x0001B533
		public Type DeclaringType { get; set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x0001D33C File Offset: 0x0001B53C
		// (set) Token: 0x0600071F RID: 1823 RVA: 0x0001D344 File Offset: 0x0001B544
		public int? Order { get; set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x0001D34D File Offset: 0x0001B54D
		// (set) Token: 0x06000721 RID: 1825 RVA: 0x0001D355 File Offset: 0x0001B555
		public string UnderlyingName { get; set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x0001D35E File Offset: 0x0001B55E
		// (set) Token: 0x06000723 RID: 1827 RVA: 0x0001D366 File Offset: 0x0001B566
		public IValueProvider ValueProvider { get; set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x0001D36F File Offset: 0x0001B56F
		// (set) Token: 0x06000725 RID: 1829 RVA: 0x0001D377 File Offset: 0x0001B577
		public IAttributeProvider AttributeProvider { get; set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x0001D380 File Offset: 0x0001B580
		// (set) Token: 0x06000727 RID: 1831 RVA: 0x0001D388 File Offset: 0x0001B588
		public Type PropertyType
		{
			get
			{
				return this._propertyType;
			}
			set
			{
				if (this._propertyType != value)
				{
					this._propertyType = value;
					this._hasGeneratedDefaultValue = false;
				}
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x0001D3A6 File Offset: 0x0001B5A6
		// (set) Token: 0x06000729 RID: 1833 RVA: 0x0001D3AE File Offset: 0x0001B5AE
		public JsonConverter Converter { get; set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x0001D3B7 File Offset: 0x0001B5B7
		// (set) Token: 0x0600072B RID: 1835 RVA: 0x0001D3BF File Offset: 0x0001B5BF
		[Obsolete("MemberConverter is obsolete. Use Converter instead.")]
		public JsonConverter MemberConverter
		{
			get
			{
				return this.Converter;
			}
			set
			{
				this.Converter = value;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x0001D3C8 File Offset: 0x0001B5C8
		// (set) Token: 0x0600072D RID: 1837 RVA: 0x0001D3D0 File Offset: 0x0001B5D0
		public bool Ignored { get; set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x0001D3D9 File Offset: 0x0001B5D9
		// (set) Token: 0x0600072F RID: 1839 RVA: 0x0001D3E1 File Offset: 0x0001B5E1
		public bool Readable { get; set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x0001D3EA File Offset: 0x0001B5EA
		// (set) Token: 0x06000731 RID: 1841 RVA: 0x0001D3F2 File Offset: 0x0001B5F2
		public bool Writable { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x0001D3FB File Offset: 0x0001B5FB
		// (set) Token: 0x06000733 RID: 1843 RVA: 0x0001D403 File Offset: 0x0001B603
		public bool HasMemberAttribute { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x0001D40C File Offset: 0x0001B60C
		// (set) Token: 0x06000735 RID: 1845 RVA: 0x0001D41E File Offset: 0x0001B61E
		public object DefaultValue
		{
			get
			{
				if (!this._hasExplicitDefaultValue)
				{
					return null;
				}
				return this._defaultValue;
			}
			set
			{
				this._hasExplicitDefaultValue = true;
				this._defaultValue = value;
			}
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0001D42E File Offset: 0x0001B62E
		internal object GetResolvedDefaultValue()
		{
			if (this._propertyType == null)
			{
				return null;
			}
			if (!this._hasExplicitDefaultValue && !this._hasGeneratedDefaultValue)
			{
				this._defaultValue = ReflectionUtils.GetDefaultValue(this._propertyType);
				this._hasGeneratedDefaultValue = true;
			}
			return this._defaultValue;
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x0001D46E File Offset: 0x0001B66E
		// (set) Token: 0x06000738 RID: 1848 RVA: 0x0001D47B File Offset: 0x0001B67B
		public Required Required
		{
			get
			{
				return this._required.GetValueOrDefault();
			}
			set
			{
				this._required = new Required?(value);
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x0001D489 File Offset: 0x0001B689
		public bool IsRequiredSpecified
		{
			get
			{
				return this._required != null;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x0001D496 File Offset: 0x0001B696
		// (set) Token: 0x0600073B RID: 1851 RVA: 0x0001D49E File Offset: 0x0001B69E
		public bool? IsReference { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x0001D4A7 File Offset: 0x0001B6A7
		// (set) Token: 0x0600073D RID: 1853 RVA: 0x0001D4AF File Offset: 0x0001B6AF
		public NullValueHandling? NullValueHandling { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x0001D4B8 File Offset: 0x0001B6B8
		// (set) Token: 0x0600073F RID: 1855 RVA: 0x0001D4C0 File Offset: 0x0001B6C0
		public DefaultValueHandling? DefaultValueHandling { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x0001D4C9 File Offset: 0x0001B6C9
		// (set) Token: 0x06000741 RID: 1857 RVA: 0x0001D4D1 File Offset: 0x0001B6D1
		public ReferenceLoopHandling? ReferenceLoopHandling { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x0001D4DA File Offset: 0x0001B6DA
		// (set) Token: 0x06000743 RID: 1859 RVA: 0x0001D4E2 File Offset: 0x0001B6E2
		public ObjectCreationHandling? ObjectCreationHandling { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x0001D4EB File Offset: 0x0001B6EB
		// (set) Token: 0x06000745 RID: 1861 RVA: 0x0001D4F3 File Offset: 0x0001B6F3
		public TypeNameHandling? TypeNameHandling { get; set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x0001D4FC File Offset: 0x0001B6FC
		// (set) Token: 0x06000747 RID: 1863 RVA: 0x0001D504 File Offset: 0x0001B704
		[Nullable(new byte[] { 2, 0 })]
		public Predicate<object> ShouldSerialize
		{
			[return: Nullable(new byte[] { 2, 0 })]
			get;
			[param: Nullable(new byte[] { 2, 0 })]
			set;
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x0001D50D File Offset: 0x0001B70D
		// (set) Token: 0x06000749 RID: 1865 RVA: 0x0001D515 File Offset: 0x0001B715
		[Nullable(new byte[] { 2, 0 })]
		public Predicate<object> ShouldDeserialize
		{
			[return: Nullable(new byte[] { 2, 0 })]
			get;
			[param: Nullable(new byte[] { 2, 0 })]
			set;
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x0001D51E File Offset: 0x0001B71E
		// (set) Token: 0x0600074B RID: 1867 RVA: 0x0001D526 File Offset: 0x0001B726
		[Nullable(new byte[] { 2, 0 })]
		public Predicate<object> GetIsSpecified
		{
			[return: Nullable(new byte[] { 2, 0 })]
			get;
			[param: Nullable(new byte[] { 2, 0 })]
			set;
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x0001D52F File Offset: 0x0001B72F
		// (set) Token: 0x0600074D RID: 1869 RVA: 0x0001D537 File Offset: 0x0001B737
		[Nullable(new byte[] { 2, 0, 2 })]
		public Action<object, object> SetIsSpecified
		{
			[return: Nullable(new byte[] { 2, 0, 2 })]
			get;
			[param: Nullable(new byte[] { 2, 0, 2 })]
			set;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0001D540 File Offset: 0x0001B740
		[NullableContext(0)]
		public override string ToString()
		{
			return this.PropertyName ?? string.Empty;
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x0001D551 File Offset: 0x0001B751
		// (set) Token: 0x06000750 RID: 1872 RVA: 0x0001D559 File Offset: 0x0001B759
		public JsonConverter ItemConverter { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x0001D562 File Offset: 0x0001B762
		// (set) Token: 0x06000752 RID: 1874 RVA: 0x0001D56A File Offset: 0x0001B76A
		public bool? ItemIsReference { get; set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x0001D573 File Offset: 0x0001B773
		// (set) Token: 0x06000754 RID: 1876 RVA: 0x0001D57B File Offset: 0x0001B77B
		public TypeNameHandling? ItemTypeNameHandling { get; set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x0001D584 File Offset: 0x0001B784
		// (set) Token: 0x06000756 RID: 1878 RVA: 0x0001D58C File Offset: 0x0001B78C
		public ReferenceLoopHandling? ItemReferenceLoopHandling { get; set; }

		// Token: 0x06000757 RID: 1879 RVA: 0x0001D598 File Offset: 0x0001B798
		[NullableContext(0)]
		internal void WritePropertyName(JsonWriter writer)
		{
			string propertyName = this.PropertyName;
			if (this._skipPropertyNameEscape)
			{
				writer.WritePropertyName(propertyName, false);
				return;
			}
			writer.WritePropertyName(propertyName);
		}

		// Token: 0x04000289 RID: 649
		internal Required? _required;

		// Token: 0x0400028A RID: 650
		internal bool _hasExplicitDefaultValue;

		// Token: 0x0400028B RID: 651
		private object _defaultValue;

		// Token: 0x0400028C RID: 652
		private bool _hasGeneratedDefaultValue;

		// Token: 0x0400028D RID: 653
		private string _propertyName;

		// Token: 0x0400028E RID: 654
		internal bool _skipPropertyNameEscape;

		// Token: 0x0400028F RID: 655
		private Type _propertyType;
	}
}
