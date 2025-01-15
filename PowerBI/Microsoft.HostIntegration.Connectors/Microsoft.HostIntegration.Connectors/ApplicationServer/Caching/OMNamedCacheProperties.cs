using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000285 RID: 645
	internal class OMNamedCacheProperties
	{
		// Token: 0x060016D2 RID: 5842 RVA: 0x00045A97 File Offset: 0x00043C97
		public OMNamedCacheProperties()
		{
			this.InitializeNamedCacheDefaults(null);
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x00045AAD File Offset: 0x00043CAD
		public OMNamedCacheProperties(INamedCacheConfiguration nc)
		{
			this.InitializeNamedCacheDefaults(nc);
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x00045AC4 File Offset: 0x00043CC4
		private void InitializeNamedCacheDefaults(INamedCacheConfiguration nc)
		{
			nc = nc ?? ServiceConfigurationManager.GetNamedCacheDefault();
			if (!nc.IsExpirable || nc.DefaultTTL > 2147483647L)
			{
				this._defaultTTL = TimeSpan.MaxValue;
			}
			else
			{
				int num = (int)nc.DefaultTTL;
				this._defaultTTL = new TimeSpan(0, 0, num, 0);
			}
			this._consistencyType = nc.Consistency;
			this._expirationType = nc.ExpirationType;
			this._isExpirable = nc.IsExpirable;
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x00045B3C File Offset: 0x00043D3C
		public OMNamedCacheProperties(OMNamedCacheProperties props)
		{
			this._oType = props._oType;
			this._maxSize = props._maxSize;
			this._consistencyType = props._consistencyType;
			this._defaultTTL = props.DefaultTTL;
			this._expirationType = props.ExpirationType;
			this._isExpirable = props.IsExpirable;
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x060016D6 RID: 5846 RVA: 0x00045B9E File Offset: 0x00043D9E
		// (set) Token: 0x060016D7 RID: 5847 RVA: 0x00045BA6 File Offset: 0x00043DA6
		public ObjectType OType
		{
			get
			{
				return this._oType;
			}
			set
			{
				this._oType = value;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x060016D8 RID: 5848 RVA: 0x00045BAF File Offset: 0x00043DAF
		// (set) Token: 0x060016D9 RID: 5849 RVA: 0x00045BB7 File Offset: 0x00043DB7
		public ConsistencyType ConsistencyType
		{
			get
			{
				return this._consistencyType;
			}
			set
			{
				this._consistencyType = value;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x060016DA RID: 5850 RVA: 0x00045BC0 File Offset: 0x00043DC0
		// (set) Token: 0x060016DB RID: 5851 RVA: 0x00045BC8 File Offset: 0x00043DC8
		public long MaxSize
		{
			get
			{
				return this._maxSize;
			}
			set
			{
				this._maxSize = value;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x060016DC RID: 5852 RVA: 0x00045BD1 File Offset: 0x00043DD1
		public TimeSpan DefaultTTL
		{
			get
			{
				return this._defaultTTL;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x060016DD RID: 5853 RVA: 0x00045BD9 File Offset: 0x00043DD9
		public bool IsExpirable
		{
			get
			{
				return this._isExpirable;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x060016DE RID: 5854 RVA: 0x00045BE1 File Offset: 0x00043DE1
		// (set) Token: 0x060016DF RID: 5855 RVA: 0x00045BE9 File Offset: 0x00043DE9
		public ExpirationType ExpirationType
		{
			get
			{
				return this._expirationType;
			}
			set
			{
				this._expirationType = value;
			}
		}

		// Token: 0x04000CC7 RID: 3271
		private ObjectType _oType = ObjectType.SerializedBufferRefType;

		// Token: 0x04000CC8 RID: 3272
		private ConsistencyType _consistencyType;

		// Token: 0x04000CC9 RID: 3273
		private long _maxSize;

		// Token: 0x04000CCA RID: 3274
		private TimeSpan _defaultTTL;

		// Token: 0x04000CCB RID: 3275
		private bool _isExpirable;

		// Token: 0x04000CCC RID: 3276
		private ExpirationType _expirationType;
	}
}
