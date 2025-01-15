using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000093 RID: 147
	[NullableContext(2)]
	[Nullable(0)]
	public class JsonProperty
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x0001D87C File Offset: 0x0001BA7C
		// (set) Token: 0x06000722 RID: 1826 RVA: 0x0001D884 File Offset: 0x0001BA84
		internal JsonContract PropertyContract { get; set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x0001D88D File Offset: 0x0001BA8D
		// (set) Token: 0x06000724 RID: 1828 RVA: 0x0001D895 File Offset: 0x0001BA95
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
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x0001D8B7 File Offset: 0x0001BAB7
		// (set) Token: 0x06000726 RID: 1830 RVA: 0x0001D8BF File Offset: 0x0001BABF
		public Type DeclaringType { get; set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x0001D8C8 File Offset: 0x0001BAC8
		// (set) Token: 0x06000728 RID: 1832 RVA: 0x0001D8D0 File Offset: 0x0001BAD0
		public int? Order { get; set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x0001D8D9 File Offset: 0x0001BAD9
		// (set) Token: 0x0600072A RID: 1834 RVA: 0x0001D8E1 File Offset: 0x0001BAE1
		public string UnderlyingName { get; set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x0001D8EA File Offset: 0x0001BAEA
		// (set) Token: 0x0600072C RID: 1836 RVA: 0x0001D8F2 File Offset: 0x0001BAF2
		public IValueProvider ValueProvider { get; set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x0001D8FB File Offset: 0x0001BAFB
		// (set) Token: 0x0600072E RID: 1838 RVA: 0x0001D903 File Offset: 0x0001BB03
		public IAttributeProvider AttributeProvider { get; set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x0001D90C File Offset: 0x0001BB0C
		// (set) Token: 0x06000730 RID: 1840 RVA: 0x0001D914 File Offset: 0x0001BB14
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
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x0001D932 File Offset: 0x0001BB32
		// (set) Token: 0x06000732 RID: 1842 RVA: 0x0001D93A File Offset: 0x0001BB3A
		public JsonConverter Converter { get; set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x0001D943 File Offset: 0x0001BB43
		// (set) Token: 0x06000734 RID: 1844 RVA: 0x0001D94B File Offset: 0x0001BB4B
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
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x0001D954 File Offset: 0x0001BB54
		// (set) Token: 0x06000736 RID: 1846 RVA: 0x0001D95C File Offset: 0x0001BB5C
		public bool Ignored { get; set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x0001D965 File Offset: 0x0001BB65
		// (set) Token: 0x06000738 RID: 1848 RVA: 0x0001D96D File Offset: 0x0001BB6D
		public bool Readable { get; set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x0001D976 File Offset: 0x0001BB76
		// (set) Token: 0x0600073A RID: 1850 RVA: 0x0001D97E File Offset: 0x0001BB7E
		public bool Writable { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x0001D987 File Offset: 0x0001BB87
		// (set) Token: 0x0600073C RID: 1852 RVA: 0x0001D98F File Offset: 0x0001BB8F
		public bool HasMemberAttribute { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x0001D998 File Offset: 0x0001BB98
		// (set) Token: 0x0600073E RID: 1854 RVA: 0x0001D9AA File Offset: 0x0001BBAA
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

		// Token: 0x0600073F RID: 1855 RVA: 0x0001D9BA File Offset: 0x0001BBBA
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
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x0001D9FA File Offset: 0x0001BBFA
		// (set) Token: 0x06000741 RID: 1857 RVA: 0x0001DA07 File Offset: 0x0001BC07
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
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x0001DA15 File Offset: 0x0001BC15
		public bool IsRequiredSpecified
		{
			get
			{
				return this._required != null;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x0001DA22 File Offset: 0x0001BC22
		// (set) Token: 0x06000744 RID: 1860 RVA: 0x0001DA2A File Offset: 0x0001BC2A
		public bool? IsReference { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x0001DA33 File Offset: 0x0001BC33
		// (set) Token: 0x06000746 RID: 1862 RVA: 0x0001DA3B File Offset: 0x0001BC3B
		public NullValueHandling? NullValueHandling { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x0001DA44 File Offset: 0x0001BC44
		// (set) Token: 0x06000748 RID: 1864 RVA: 0x0001DA4C File Offset: 0x0001BC4C
		public DefaultValueHandling? DefaultValueHandling { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x0001DA55 File Offset: 0x0001BC55
		// (set) Token: 0x0600074A RID: 1866 RVA: 0x0001DA5D File Offset: 0x0001BC5D
		public ReferenceLoopHandling? ReferenceLoopHandling { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x0001DA66 File Offset: 0x0001BC66
		// (set) Token: 0x0600074C RID: 1868 RVA: 0x0001DA6E File Offset: 0x0001BC6E
		public ObjectCreationHandling? ObjectCreationHandling { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x0001DA77 File Offset: 0x0001BC77
		// (set) Token: 0x0600074E RID: 1870 RVA: 0x0001DA7F File Offset: 0x0001BC7F
		public TypeNameHandling? TypeNameHandling { get; set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x0001DA88 File Offset: 0x0001BC88
		// (set) Token: 0x06000750 RID: 1872 RVA: 0x0001DA90 File Offset: 0x0001BC90
		[Nullable(new byte[] { 2, 1 })]
		public Predicate<object> ShouldSerialize
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x0001DA99 File Offset: 0x0001BC99
		// (set) Token: 0x06000752 RID: 1874 RVA: 0x0001DAA1 File Offset: 0x0001BCA1
		[Nullable(new byte[] { 2, 1 })]
		public Predicate<object> ShouldDeserialize
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x0001DAAA File Offset: 0x0001BCAA
		// (set) Token: 0x06000754 RID: 1876 RVA: 0x0001DAB2 File Offset: 0x0001BCB2
		[Nullable(new byte[] { 2, 1 })]
		public Predicate<object> GetIsSpecified
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x0001DABB File Offset: 0x0001BCBB
		// (set) Token: 0x06000756 RID: 1878 RVA: 0x0001DAC3 File Offset: 0x0001BCC3
		[Nullable(new byte[] { 2, 1, 2 })]
		public Action<object, object> SetIsSpecified
		{
			[return: Nullable(new byte[] { 2, 1, 2 })]
			get;
			[param: Nullable(new byte[] { 2, 1, 2 })]
			set;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0001DACC File Offset: 0x0001BCCC
		[NullableContext(1)]
		public override string ToString()
		{
			return this.PropertyName ?? string.Empty;
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x0001DADD File Offset: 0x0001BCDD
		// (set) Token: 0x06000759 RID: 1881 RVA: 0x0001DAE5 File Offset: 0x0001BCE5
		public JsonConverter ItemConverter { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x0001DAEE File Offset: 0x0001BCEE
		// (set) Token: 0x0600075B RID: 1883 RVA: 0x0001DAF6 File Offset: 0x0001BCF6
		public bool? ItemIsReference { get; set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x0001DAFF File Offset: 0x0001BCFF
		// (set) Token: 0x0600075D RID: 1885 RVA: 0x0001DB07 File Offset: 0x0001BD07
		public TypeNameHandling? ItemTypeNameHandling { get; set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x0001DB10 File Offset: 0x0001BD10
		// (set) Token: 0x0600075F RID: 1887 RVA: 0x0001DB18 File Offset: 0x0001BD18
		public ReferenceLoopHandling? ItemReferenceLoopHandling { get; set; }

		// Token: 0x06000760 RID: 1888 RVA: 0x0001DB24 File Offset: 0x0001BD24
		[NullableContext(1)]
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

		// Token: 0x040002A3 RID: 675
		internal Required? _required;

		// Token: 0x040002A4 RID: 676
		internal bool _hasExplicitDefaultValue;

		// Token: 0x040002A5 RID: 677
		private object _defaultValue;

		// Token: 0x040002A6 RID: 678
		private bool _hasGeneratedDefaultValue;

		// Token: 0x040002A7 RID: 679
		private string _propertyName;

		// Token: 0x040002A8 RID: 680
		internal bool _skipPropertyNameEscape;

		// Token: 0x040002A9 RID: 681
		private Type _propertyType;
	}
}
