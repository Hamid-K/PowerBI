using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Owin.Host.HttpListener.RequestProcessing
{
	// Token: 0x0200000D RID: 13
	[GeneratedCode("TextTemplatingFileGenerator", "")]
	internal sealed class CallEnvironment : IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002F5B File Offset: 0x0000115B
		internal CallEnvironment(CallEnvironment.IPropertySource propertySource)
		{
			this._propertySource = propertySource;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002F8B File Offset: 0x0000118B
		internal IDictionary<string, object> Extra
		{
			get
			{
				return this._extra;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002F93 File Offset: 0x00001193
		private IDictionary<string, object> StrongExtra
		{
			get
			{
				if (this._extra == CallEnvironment.WeakNilEnvironment)
				{
					this._extra = new Dictionary<string, object>();
				}
				return this._extra;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002FB3 File Offset: 0x000011B3
		internal bool IsExtraDictionaryCreated
		{
			get
			{
				return this._extra != CallEnvironment.WeakNilEnvironment;
			}
		}

		// Token: 0x1700000D RID: 13
		public object this[string key]
		{
			get
			{
				object value;
				if (!this.PropertiesTryGetValue(key, out value))
				{
					return this.Extra[key];
				}
				return value;
			}
			set
			{
				if (!this.PropertiesTrySetValue(key, value))
				{
					this.StrongExtra[key] = value;
				}
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003007 File Offset: 0x00001207
		public void Add(string key, object value)
		{
			if (!this.PropertiesTrySetValue(key, value))
			{
				this.StrongExtra.Add(key, value);
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003020 File Offset: 0x00001220
		public bool ContainsKey(string key)
		{
			return this.PropertiesContainsKey(key) || this.Extra.ContainsKey(key);
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00003039 File Offset: 0x00001239
		public ICollection<string> Keys
		{
			get
			{
				return this.PropertiesKeys().Concat(this.Extra.Keys).ToArray<string>();
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003056 File Offset: 0x00001256
		public bool Remove(string key)
		{
			return this.PropertiesTryRemove(key) || this.Extra.Remove(key);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000306F File Offset: 0x0000126F
		public bool TryGetValue(string key, out object value)
		{
			return this.PropertiesTryGetValue(key, out value) || this.Extra.TryGetValue(key, out value);
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000044 RID: 68 RVA: 0x0000308A File Offset: 0x0000128A
		public ICollection<object> Values
		{
			get
			{
				return this.PropertiesValues().Concat(this.Extra.Values).ToArray<object>();
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000030A7 File Offset: 0x000012A7
		public void Add(KeyValuePair<string, object> item)
		{
			((IDictionary<string, object>)this).Add(item.Key, item.Value);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000030C0 File Offset: 0x000012C0
		public void Clear()
		{
			foreach (string key in this.PropertiesKeys())
			{
				this.PropertiesTryRemove(key);
			}
			this.Extra.Clear();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000311C File Offset: 0x0000131C
		public bool Contains(KeyValuePair<string, object> item)
		{
			object value;
			return ((IDictionary<string, object>)this).TryGetValue(item.Key, out value) && object.Equals(value, item.Value);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003149 File Offset: 0x00001349
		public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
		{
			this.PropertiesEnumerable().Concat(this.Extra).ToArray<KeyValuePair<string, object>>()
				.CopyTo(array, arrayIndex);
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00003168 File Offset: 0x00001368
		public int Count
		{
			get
			{
				return this.PropertiesKeys().Count<string>() + this.Extra.Count;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00003181 File Offset: 0x00001381
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003184 File Offset: 0x00001384
		public bool Remove(KeyValuePair<string, object> item)
		{
			return ((ICollection<KeyValuePair<string, object>>)this).Contains(item) && ((IDictionary<string, object>)this).Remove(item.Key);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000319E File Offset: 0x0000139E
		IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
		{
			return this.PropertiesEnumerable().Concat(this.Extra).GetEnumerator();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000031B6 File Offset: 0x000013B6
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<string, object>>)this).GetEnumerator();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000031C0 File Offset: 0x000013C0
		private bool InitPropertyClientCert()
		{
			if (!this._propertySource.TryGetClientCert(ref this._ClientCert))
			{
				this._flag0 &= 4160749567U;
				this._initFlag0 &= 4160749567U;
				return false;
			}
			this._initFlag0 &= 4160749567U;
			return true;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000321C File Offset: 0x0000141C
		private bool InitPropertyClientCertErrors()
		{
			if (!this._propertySource.TryGetClientCertErrors(ref this._ClientCertErrors))
			{
				this._flag0 &= 4026531839U;
				this._initFlag0 &= 4026531839U;
				return false;
			}
			this._initFlag0 &= 4026531839U;
			return true;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003278 File Offset: 0x00001478
		private bool InitPropertyWebSocketAccept()
		{
			if (!this._propertySource.TryGetWebSocketAccept(ref this._WebSocketAccept))
			{
				this._flag0 &= 3221225471U;
				this._initFlag0 &= 3221225471U;
				return false;
			}
			this._initFlag0 &= 3221225471U;
			return true;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000032D1 File Offset: 0x000014D1
		internal bool ClientCertNeedsInit
		{
			get
			{
				return (this._initFlag0 & 134217728U) > 0U;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000032E2 File Offset: 0x000014E2
		internal bool ClientCertErrorsNeedsInit
		{
			get
			{
				return (this._initFlag0 & 268435456U) > 0U;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000032F3 File Offset: 0x000014F3
		internal bool WebSocketAcceptNeedsInit
		{
			get
			{
				return (this._initFlag0 & 1073741824U) > 0U;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00003304 File Offset: 0x00001504
		// (set) Token: 0x06000055 RID: 85 RVA: 0x0000330C File Offset: 0x0000150C
		internal string RequestPath
		{
			get
			{
				return this._RequestPath;
			}
			set
			{
				this._flag0 |= 1U;
				this._RequestPath = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00003323 File Offset: 0x00001523
		// (set) Token: 0x06000057 RID: 87 RVA: 0x0000332B File Offset: 0x0000152B
		internal IDictionary<string, string[]> ResponseHeaders
		{
			get
			{
				return this._ResponseHeaders;
			}
			set
			{
				this._flag0 |= 2U;
				this._ResponseHeaders = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003342 File Offset: 0x00001542
		// (set) Token: 0x06000059 RID: 89 RVA: 0x0000334A File Offset: 0x0000154A
		internal IDictionary<string, string[]> RequestHeaders
		{
			get
			{
				return this._RequestHeaders;
			}
			set
			{
				this._flag0 |= 4U;
				this._RequestHeaders = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003361 File Offset: 0x00001561
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00003369 File Offset: 0x00001569
		internal Stream ResponseBody
		{
			get
			{
				return this._ResponseBody;
			}
			set
			{
				this._flag0 |= 8U;
				this._ResponseBody = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003380 File Offset: 0x00001580
		// (set) Token: 0x0600005D RID: 93 RVA: 0x000033B3 File Offset: 0x000015B3
		internal Stream RequestBody
		{
			get
			{
				if ((this._initFlag0 & 16U) != 0U)
				{
					this._RequestBody = this._propertySource.GetRequestBody();
					this._initFlag0 &= 4294967279U;
				}
				return this._RequestBody;
			}
			set
			{
				this._initFlag0 &= 4294967279U;
				this._flag0 |= 16U;
				this._RequestBody = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000033DA File Offset: 0x000015DA
		// (set) Token: 0x0600005F RID: 95 RVA: 0x000033E2 File Offset: 0x000015E2
		internal string RequestId
		{
			get
			{
				return this._RequestId;
			}
			set
			{
				this._flag0 |= 32U;
				this._RequestId = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000033FA File Offset: 0x000015FA
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00003402 File Offset: 0x00001602
		internal int ResponseStatusCode
		{
			get
			{
				return this._ResponseStatusCode;
			}
			set
			{
				this._flag0 |= 64U;
				this._ResponseStatusCode = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000062 RID: 98 RVA: 0x0000341A File Offset: 0x0000161A
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00003422 File Offset: 0x00001622
		internal string ResponseReasonPhrase
		{
			get
			{
				return this._ResponseReasonPhrase;
			}
			set
			{
				this._flag0 |= 128U;
				this._ResponseReasonPhrase = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000064 RID: 100 RVA: 0x0000343D File Offset: 0x0000163D
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00003445 File Offset: 0x00001645
		internal string RequestQueryString
		{
			get
			{
				return this._RequestQueryString;
			}
			set
			{
				this._flag0 |= 256U;
				this._RequestQueryString = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003460 File Offset: 0x00001660
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00003499 File Offset: 0x00001699
		internal CancellationToken CallCancelled
		{
			get
			{
				if ((this._initFlag0 & 512U) != 0U)
				{
					this._CallCancelled = this._propertySource.GetCallCancelled();
					this._initFlag0 &= 4294966783U;
				}
				return this._CallCancelled;
			}
			set
			{
				this._initFlag0 &= 4294966783U;
				this._flag0 |= 512U;
				this._CallCancelled = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000034C6 File Offset: 0x000016C6
		// (set) Token: 0x06000069 RID: 105 RVA: 0x000034CE File Offset: 0x000016CE
		internal string RequestMethod
		{
			get
			{
				return this._RequestMethod;
			}
			set
			{
				this._flag0 |= 1024U;
				this._RequestMethod = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000034E9 File Offset: 0x000016E9
		// (set) Token: 0x0600006B RID: 107 RVA: 0x000034F1 File Offset: 0x000016F1
		internal string RequestScheme
		{
			get
			{
				return this._RequestScheme;
			}
			set
			{
				this._flag0 |= 2048U;
				this._RequestScheme = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600006C RID: 108 RVA: 0x0000350C File Offset: 0x0000170C
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00003514 File Offset: 0x00001714
		internal string RequestPathBase
		{
			get
			{
				return this._RequestPathBase;
			}
			set
			{
				this._flag0 |= 4096U;
				this._RequestPathBase = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600006E RID: 110 RVA: 0x0000352F File Offset: 0x0000172F
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00003537 File Offset: 0x00001737
		internal string RequestProtocol
		{
			get
			{
				return this._RequestProtocol;
			}
			set
			{
				this._flag0 |= 8192U;
				this._RequestProtocol = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003552 File Offset: 0x00001752
		// (set) Token: 0x06000071 RID: 113 RVA: 0x0000355A File Offset: 0x0000175A
		internal string OwinVersion
		{
			get
			{
				return this._OwinVersion;
			}
			set
			{
				this._flag0 |= 16384U;
				this._OwinVersion = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003575 File Offset: 0x00001775
		// (set) Token: 0x06000073 RID: 115 RVA: 0x0000357D File Offset: 0x0000177D
		internal TextWriter HostTraceOutput
		{
			get
			{
				return this._HostTraceOutput;
			}
			set
			{
				this._flag0 |= 32768U;
				this._HostTraceOutput = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003598 File Offset: 0x00001798
		// (set) Token: 0x06000075 RID: 117 RVA: 0x000035A0 File Offset: 0x000017A0
		internal string HostAppName
		{
			get
			{
				return this._HostAppName;
			}
			set
			{
				this._flag0 |= 65536U;
				this._HostAppName = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000035BB File Offset: 0x000017BB
		// (set) Token: 0x06000077 RID: 119 RVA: 0x000035C3 File Offset: 0x000017C3
		internal string HostAppMode
		{
			get
			{
				return this._HostAppMode;
			}
			set
			{
				this._flag0 |= 131072U;
				this._HostAppMode = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000035DE File Offset: 0x000017DE
		// (set) Token: 0x06000079 RID: 121 RVA: 0x000035E6 File Offset: 0x000017E6
		internal CancellationToken OnAppDisposing
		{
			get
			{
				return this._OnAppDisposing;
			}
			set
			{
				this._flag0 |= 262144U;
				this._OnAppDisposing = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003601 File Offset: 0x00001801
		// (set) Token: 0x0600007B RID: 123 RVA: 0x0000360E File Offset: 0x0000180E
		internal IPrincipal ServerUser
		{
			get
			{
				return this._propertySource.GetServerUser();
			}
			set
			{
				this._propertySource.SetServerUser(value);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600007C RID: 124 RVA: 0x0000361C File Offset: 0x0000181C
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00003624 File Offset: 0x00001824
		internal Action<Action<object>, object> OnSendingHeaders
		{
			get
			{
				return this._OnSendingHeaders;
			}
			set
			{
				this._flag0 |= 1048576U;
				this._OnSendingHeaders = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600007E RID: 126 RVA: 0x0000363F File Offset: 0x0000183F
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00003647 File Offset: 0x00001847
		internal IDictionary<string, object> ServerCapabilities
		{
			get
			{
				return this._ServerCapabilities;
			}
			set
			{
				this._flag0 |= 2097152U;
				this._ServerCapabilities = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003662 File Offset: 0x00001862
		// (set) Token: 0x06000081 RID: 129 RVA: 0x0000369B File Offset: 0x0000189B
		internal string ServerRemoteIpAddress
		{
			get
			{
				if ((this._initFlag0 & 4194304U) != 0U)
				{
					this._ServerRemoteIpAddress = this._propertySource.GetServerRemoteIpAddress();
					this._initFlag0 &= 4290772991U;
				}
				return this._ServerRemoteIpAddress;
			}
			set
			{
				this._initFlag0 &= 4290772991U;
				this._flag0 |= 4194304U;
				this._ServerRemoteIpAddress = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000036C8 File Offset: 0x000018C8
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00003701 File Offset: 0x00001901
		internal string ServerRemotePort
		{
			get
			{
				if ((this._initFlag0 & 8388608U) != 0U)
				{
					this._ServerRemotePort = this._propertySource.GetServerRemotePort();
					this._initFlag0 &= 4286578687U;
				}
				return this._ServerRemotePort;
			}
			set
			{
				this._initFlag0 &= 4286578687U;
				this._flag0 |= 8388608U;
				this._ServerRemotePort = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000084 RID: 132 RVA: 0x0000372E File Offset: 0x0000192E
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00003767 File Offset: 0x00001967
		internal string ServerLocalIpAddress
		{
			get
			{
				if ((this._initFlag0 & 16777216U) != 0U)
				{
					this._ServerLocalIpAddress = this._propertySource.GetServerLocalIpAddress();
					this._initFlag0 &= 4278190079U;
				}
				return this._ServerLocalIpAddress;
			}
			set
			{
				this._initFlag0 &= 4278190079U;
				this._flag0 |= 16777216U;
				this._ServerLocalIpAddress = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003794 File Offset: 0x00001994
		// (set) Token: 0x06000087 RID: 135 RVA: 0x000037CD File Offset: 0x000019CD
		internal string ServerLocalPort
		{
			get
			{
				if ((this._initFlag0 & 33554432U) != 0U)
				{
					this._ServerLocalPort = this._propertySource.GetServerLocalPort();
					this._initFlag0 &= 4261412863U;
				}
				return this._ServerLocalPort;
			}
			set
			{
				this._initFlag0 &= 4261412863U;
				this._flag0 |= 33554432U;
				this._ServerLocalPort = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000037FA File Offset: 0x000019FA
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00003833 File Offset: 0x00001A33
		internal bool ServerIsLocal
		{
			get
			{
				if ((this._initFlag0 & 67108864U) != 0U)
				{
					this._ServerIsLocal = this._propertySource.GetServerIsLocal();
					this._initFlag0 &= 4227858431U;
				}
				return this._ServerIsLocal;
			}
			set
			{
				this._initFlag0 &= 4227858431U;
				this._flag0 |= 67108864U;
				this._ServerIsLocal = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00003860 File Offset: 0x00001A60
		// (set) Token: 0x0600008B RID: 139 RVA: 0x0000387D File Offset: 0x00001A7D
		internal X509Certificate ClientCert
		{
			get
			{
				if ((this._initFlag0 & 134217728U) != 0U)
				{
					this.InitPropertyClientCert();
				}
				return this._ClientCert;
			}
			set
			{
				this._initFlag0 &= 4160749567U;
				this._flag0 |= 134217728U;
				this._ClientCert = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000038AA File Offset: 0x00001AAA
		// (set) Token: 0x0600008D RID: 141 RVA: 0x000038C7 File Offset: 0x00001AC7
		internal Exception ClientCertErrors
		{
			get
			{
				if ((this._initFlag0 & 268435456U) != 0U)
				{
					this.InitPropertyClientCertErrors();
				}
				return this._ClientCertErrors;
			}
			set
			{
				this._initFlag0 &= 4026531839U;
				this._flag0 |= 268435456U;
				this._ClientCertErrors = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000038F4 File Offset: 0x00001AF4
		// (set) Token: 0x0600008F RID: 143 RVA: 0x000038FC File Offset: 0x00001AFC
		internal Func<Task> LoadClientCert
		{
			get
			{
				return this._LoadClientCert;
			}
			set
			{
				this._flag0 |= 536870912U;
				this._LoadClientCert = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003917 File Offset: 0x00001B17
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00003934 File Offset: 0x00001B34
		internal Action<IDictionary<string, object>, Func<IDictionary<string, object>, Task>> WebSocketAccept
		{
			get
			{
				if ((this._initFlag0 & 1073741824U) != 0U)
				{
					this.InitPropertyWebSocketAccept();
				}
				return this._WebSocketAccept;
			}
			set
			{
				this._initFlag0 &= 3221225471U;
				this._flag0 |= 1073741824U;
				this._WebSocketAccept = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003961 File Offset: 0x00001B61
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00003969 File Offset: 0x00001B69
		internal Func<string, long, long?, CancellationToken, Task> SendFileAsync
		{
			get
			{
				return this._SendFileAsync;
			}
			set
			{
				this._flag0 |= 2147483648U;
				this._SendFileAsync = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003984 File Offset: 0x00001B84
		// (set) Token: 0x06000095 RID: 149 RVA: 0x0000398C File Offset: 0x00001B8C
		internal HttpListenerContext RequestContext
		{
			get
			{
				return this._RequestContext;
			}
			set
			{
				this._flag1 |= 1U;
				this._RequestContext = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000039A3 File Offset: 0x00001BA3
		// (set) Token: 0x06000097 RID: 151 RVA: 0x000039AB File Offset: 0x00001BAB
		internal HttpListener Listener
		{
			get
			{
				return this._Listener;
			}
			set
			{
				this._flag1 |= 2U;
				this._Listener = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000039C2 File Offset: 0x00001BC2
		// (set) Token: 0x06000099 RID: 153 RVA: 0x000039CA File Offset: 0x00001BCA
		internal OwinHttpListener OwinHttpListener
		{
			get
			{
				return this._OwinHttpListener;
			}
			set
			{
				this._flag1 |= 4U;
				this._OwinHttpListener = value;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000039E4 File Offset: 0x00001BE4
		private bool PropertiesContainsKey(string key)
		{
			int length = key.Length;
			switch (length)
			{
			case 11:
				if ((this._flag0 & 524288U) != 0U && string.Equals(key, "server.User", StringComparison.Ordinal))
				{
					return true;
				}
				break;
			case 12:
				if ((this._flag0 & 16384U) != 0U && string.Equals(key, "owin.Version", StringComparison.Ordinal))
				{
					return true;
				}
				if ((this._flag0 & 65536U) != 0U && string.Equals(key, "host.AppName", StringComparison.Ordinal))
				{
					return true;
				}
				if ((this._flag0 & 131072U) != 0U && string.Equals(key, "host.AppMode", StringComparison.Ordinal))
				{
					return true;
				}
				break;
			case 13:
			case 15:
			case 24:
			case 26:
			case 28:
			case 29:
				break;
			case 14:
				if ((this._flag0 & 32U) != 0U && string.Equals(key, "owin.RequestId", StringComparison.Ordinal))
				{
					return true;
				}
				if ((this._flag0 & 67108864U) != 0U && string.Equals(key, "server.IsLocal", StringComparison.Ordinal))
				{
					return true;
				}
				break;
			case 16:
				if ((this._flag0 & 1U) != 0U && string.Equals(key, "owin.RequestPath", StringComparison.Ordinal))
				{
					return true;
				}
				if ((this._flag0 & 16U) != 0U && string.Equals(key, "owin.RequestBody", StringComparison.Ordinal))
				{
					return true;
				}
				if ((this._flag0 & 32768U) != 0U && string.Equals(key, "host.TraceOutput", StringComparison.Ordinal))
				{
					return true;
				}
				if ((this._flag0 & 33554432U) != 0U && string.Equals(key, "server.LocalPort", StringComparison.Ordinal))
				{
					return true;
				}
				if ((this._flag0 & 1073741824U) != 0U && string.Equals(key, "websocket.Accept", StringComparison.Ordinal) && ((this._initFlag0 & 1073741824U) == 0U || this.InitPropertyWebSocketAccept()))
				{
					return true;
				}
				break;
			case 17:
				if ((this._flag0 & 8U) != 0U && string.Equals(key, "owin.ResponseBody", StringComparison.Ordinal))
				{
					return true;
				}
				if ((this._flag0 & 8388608U) != 0U && string.Equals(key, "server.RemotePort", StringComparison.Ordinal))
				{
					return true;
				}
				break;
			case 18:
				if ((this._flag0 & 512U) != 0U && string.Equals(key, "owin.CallCancelled", StringComparison.Ordinal))
				{
					return true;
				}
				if ((this._flag0 & 1024U) != 0U && string.Equals(key, "owin.RequestMethod", StringComparison.Ordinal))
				{
					return true;
				}
				if ((this._flag0 & 2048U) != 0U && string.Equals(key, "owin.RequestScheme", StringComparison.Ordinal))
				{
					return true;
				}
				if ((this._flag0 & 2147483648U) != 0U && string.Equals(key, "sendfile.SendAsync", StringComparison.Ordinal))
				{
					return true;
				}
				break;
			case 19:
				if ((this._flag0 & 4U) != 0U && string.Equals(key, "owin.RequestHeaders", StringComparison.Ordinal))
				{
					return true;
				}
				if ((this._flag0 & 262144U) != 0U && string.Equals(key, "host.OnAppDisposing", StringComparison.Ordinal))
				{
					return true;
				}
				if ((this._flag0 & 2097152U) != 0U && string.Equals(key, "server.Capabilities", StringComparison.Ordinal))
				{
					return true;
				}
				break;
			case 20:
				if ((this._flag0 & 2U) != 0U && string.Equals(key, "owin.ResponseHeaders", StringComparison.Ordinal))
				{
					return true;
				}
				if ((this._flag0 & 4096U) != 0U && string.Equals(key, "owin.RequestPathBase", StringComparison.Ordinal))
				{
					return true;
				}
				if ((this._flag0 & 8192U) != 0U && string.Equals(key, "owin.RequestProtocol", StringComparison.Ordinal))
				{
					return true;
				}
				break;
			case 21:
				if ((this._flag0 & 16777216U) != 0U && string.Equals(key, "server.LocalIpAddress", StringComparison.Ordinal))
				{
					return true;
				}
				if ((this._flag0 & 134217728U) != 0U && string.Equals(key, "ssl.ClientCertificate", StringComparison.Ordinal) && ((this._initFlag0 & 134217728U) == 0U || this.InitPropertyClientCert()))
				{
					return true;
				}
				break;
			case 22:
				if ((this._flag0 & 4194304U) != 0U && string.Equals(key, "server.RemoteIpAddress", StringComparison.Ordinal))
				{
					return true;
				}
				break;
			case 23:
				if ((this._flag0 & 64U) != 0U && string.Equals(key, "owin.ResponseStatusCode", StringComparison.Ordinal))
				{
					return true;
				}
				if ((this._flag0 & 256U) != 0U && string.Equals(key, "owin.RequestQueryString", StringComparison.Ordinal))
				{
					return true;
				}
				if ((this._flag0 & 1048576U) != 0U && string.Equals(key, "server.OnSendingHeaders", StringComparison.Ordinal))
				{
					return true;
				}
				if ((this._flag0 & 536870912U) != 0U && string.Equals(key, "ssl.LoadClientCertAsync", StringComparison.Ordinal))
				{
					return true;
				}
				if ((this._flag1 & 2U) != 0U && string.Equals(key, "System.Net.HttpListener", StringComparison.Ordinal))
				{
					return true;
				}
				break;
			case 25:
				if ((this._flag0 & 128U) != 0U && string.Equals(key, "owin.ResponseReasonPhrase", StringComparison.Ordinal))
				{
					return true;
				}
				break;
			case 27:
				if ((this._flag0 & 268435456U) != 0U && string.Equals(key, "ssl.ClientCertificateErrors", StringComparison.Ordinal) && ((this._initFlag0 & 268435456U) == 0U || this.InitPropertyClientCertErrors()))
				{
					return true;
				}
				break;
			case 30:
				if ((this._flag1 & 1U) != 0U && string.Equals(key, "System.Net.HttpListenerContext", StringComparison.Ordinal))
				{
					return true;
				}
				break;
			default:
				if (length == 49)
				{
					if ((this._flag1 & 4U) != 0U && string.Equals(key, "Microsoft.Owin.Host.HttpListener.OwinHttpListener", StringComparison.Ordinal))
					{
						return true;
					}
				}
				break;
			}
			return false;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003EE0 File Offset: 0x000020E0
		private bool PropertiesTryGetValue(string key, out object value)
		{
			int length = key.Length;
			switch (length)
			{
			case 11:
				if ((this._flag0 & 524288U) != 0U && string.Equals(key, "server.User", StringComparison.Ordinal))
				{
					value = this.ServerUser;
					return true;
				}
				break;
			case 12:
				if ((this._flag0 & 16384U) != 0U && string.Equals(key, "owin.Version", StringComparison.Ordinal))
				{
					value = this.OwinVersion;
					return true;
				}
				if ((this._flag0 & 65536U) != 0U && string.Equals(key, "host.AppName", StringComparison.Ordinal))
				{
					value = this.HostAppName;
					return true;
				}
				if ((this._flag0 & 131072U) != 0U && string.Equals(key, "host.AppMode", StringComparison.Ordinal))
				{
					value = this.HostAppMode;
					return true;
				}
				break;
			case 13:
			case 15:
			case 24:
			case 26:
			case 28:
			case 29:
				break;
			case 14:
				if ((this._flag0 & 32U) != 0U && string.Equals(key, "owin.RequestId", StringComparison.Ordinal))
				{
					value = this.RequestId;
					return true;
				}
				if ((this._flag0 & 67108864U) != 0U && string.Equals(key, "server.IsLocal", StringComparison.Ordinal))
				{
					value = this.ServerIsLocal;
					return true;
				}
				break;
			case 16:
				if ((this._flag0 & 1U) != 0U && string.Equals(key, "owin.RequestPath", StringComparison.Ordinal))
				{
					value = this.RequestPath;
					return true;
				}
				if ((this._flag0 & 16U) != 0U && string.Equals(key, "owin.RequestBody", StringComparison.Ordinal))
				{
					value = this.RequestBody;
					return true;
				}
				if ((this._flag0 & 32768U) != 0U && string.Equals(key, "host.TraceOutput", StringComparison.Ordinal))
				{
					value = this.HostTraceOutput;
					return true;
				}
				if ((this._flag0 & 33554432U) != 0U && string.Equals(key, "server.LocalPort", StringComparison.Ordinal))
				{
					value = this.ServerLocalPort;
					return true;
				}
				if ((this._flag0 & 1073741824U) != 0U && string.Equals(key, "websocket.Accept", StringComparison.Ordinal))
				{
					value = this.WebSocketAccept;
					if ((this._flag0 & 1073741824U) == 0U)
					{
						value = null;
						return false;
					}
					return true;
				}
				break;
			case 17:
				if ((this._flag0 & 8U) != 0U && string.Equals(key, "owin.ResponseBody", StringComparison.Ordinal))
				{
					value = this.ResponseBody;
					return true;
				}
				if ((this._flag0 & 8388608U) != 0U && string.Equals(key, "server.RemotePort", StringComparison.Ordinal))
				{
					value = this.ServerRemotePort;
					return true;
				}
				break;
			case 18:
				if ((this._flag0 & 512U) != 0U && string.Equals(key, "owin.CallCancelled", StringComparison.Ordinal))
				{
					value = this.CallCancelled;
					return true;
				}
				if ((this._flag0 & 1024U) != 0U && string.Equals(key, "owin.RequestMethod", StringComparison.Ordinal))
				{
					value = this.RequestMethod;
					return true;
				}
				if ((this._flag0 & 2048U) != 0U && string.Equals(key, "owin.RequestScheme", StringComparison.Ordinal))
				{
					value = this.RequestScheme;
					return true;
				}
				if ((this._flag0 & 2147483648U) != 0U && string.Equals(key, "sendfile.SendAsync", StringComparison.Ordinal))
				{
					value = this.SendFileAsync;
					return true;
				}
				break;
			case 19:
				if ((this._flag0 & 4U) != 0U && string.Equals(key, "owin.RequestHeaders", StringComparison.Ordinal))
				{
					value = this.RequestHeaders;
					return true;
				}
				if ((this._flag0 & 262144U) != 0U && string.Equals(key, "host.OnAppDisposing", StringComparison.Ordinal))
				{
					value = this.OnAppDisposing;
					return true;
				}
				if ((this._flag0 & 2097152U) != 0U && string.Equals(key, "server.Capabilities", StringComparison.Ordinal))
				{
					value = this.ServerCapabilities;
					return true;
				}
				break;
			case 20:
				if ((this._flag0 & 2U) != 0U && string.Equals(key, "owin.ResponseHeaders", StringComparison.Ordinal))
				{
					value = this.ResponseHeaders;
					return true;
				}
				if ((this._flag0 & 4096U) != 0U && string.Equals(key, "owin.RequestPathBase", StringComparison.Ordinal))
				{
					value = this.RequestPathBase;
					return true;
				}
				if ((this._flag0 & 8192U) != 0U && string.Equals(key, "owin.RequestProtocol", StringComparison.Ordinal))
				{
					value = this.RequestProtocol;
					return true;
				}
				break;
			case 21:
				if ((this._flag0 & 16777216U) != 0U && string.Equals(key, "server.LocalIpAddress", StringComparison.Ordinal))
				{
					value = this.ServerLocalIpAddress;
					return true;
				}
				if ((this._flag0 & 134217728U) != 0U && string.Equals(key, "ssl.ClientCertificate", StringComparison.Ordinal))
				{
					value = this.ClientCert;
					if ((this._flag0 & 134217728U) == 0U)
					{
						value = null;
						return false;
					}
					return true;
				}
				break;
			case 22:
				if ((this._flag0 & 4194304U) != 0U && string.Equals(key, "server.RemoteIpAddress", StringComparison.Ordinal))
				{
					value = this.ServerRemoteIpAddress;
					return true;
				}
				break;
			case 23:
				if ((this._flag0 & 64U) != 0U && string.Equals(key, "owin.ResponseStatusCode", StringComparison.Ordinal))
				{
					value = this.ResponseStatusCode;
					return true;
				}
				if ((this._flag0 & 256U) != 0U && string.Equals(key, "owin.RequestQueryString", StringComparison.Ordinal))
				{
					value = this.RequestQueryString;
					return true;
				}
				if ((this._flag0 & 1048576U) != 0U && string.Equals(key, "server.OnSendingHeaders", StringComparison.Ordinal))
				{
					value = this.OnSendingHeaders;
					return true;
				}
				if ((this._flag0 & 536870912U) != 0U && string.Equals(key, "ssl.LoadClientCertAsync", StringComparison.Ordinal))
				{
					value = this.LoadClientCert;
					return true;
				}
				if ((this._flag1 & 2U) != 0U && string.Equals(key, "System.Net.HttpListener", StringComparison.Ordinal))
				{
					value = this.Listener;
					return true;
				}
				break;
			case 25:
				if ((this._flag0 & 128U) != 0U && string.Equals(key, "owin.ResponseReasonPhrase", StringComparison.Ordinal))
				{
					value = this.ResponseReasonPhrase;
					return true;
				}
				break;
			case 27:
				if ((this._flag0 & 268435456U) != 0U && string.Equals(key, "ssl.ClientCertificateErrors", StringComparison.Ordinal))
				{
					value = this.ClientCertErrors;
					if ((this._flag0 & 268435456U) == 0U)
					{
						value = null;
						return false;
					}
					return true;
				}
				break;
			case 30:
				if ((this._flag1 & 1U) != 0U && string.Equals(key, "System.Net.HttpListenerContext", StringComparison.Ordinal))
				{
					value = this.RequestContext;
					return true;
				}
				break;
			default:
				if (length == 49)
				{
					if ((this._flag1 & 4U) != 0U && string.Equals(key, "Microsoft.Owin.Host.HttpListener.OwinHttpListener", StringComparison.Ordinal))
					{
						value = this.OwinHttpListener;
						return true;
					}
				}
				break;
			}
			value = null;
			return false;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004500 File Offset: 0x00002700
		private bool PropertiesTrySetValue(string key, object value)
		{
			int length = key.Length;
			switch (length)
			{
			case 11:
				if (string.Equals(key, "server.User", StringComparison.Ordinal))
				{
					this.ServerUser = (IPrincipal)value;
					return true;
				}
				break;
			case 12:
				if (string.Equals(key, "owin.Version", StringComparison.Ordinal))
				{
					this.OwinVersion = (string)value;
					return true;
				}
				if (string.Equals(key, "host.AppName", StringComparison.Ordinal))
				{
					this.HostAppName = (string)value;
					return true;
				}
				if (string.Equals(key, "host.AppMode", StringComparison.Ordinal))
				{
					this.HostAppMode = (string)value;
					return true;
				}
				break;
			case 13:
			case 15:
			case 24:
			case 26:
			case 28:
			case 29:
				break;
			case 14:
				if (string.Equals(key, "owin.RequestId", StringComparison.Ordinal))
				{
					this.RequestId = (string)value;
					return true;
				}
				if (string.Equals(key, "server.IsLocal", StringComparison.Ordinal))
				{
					this.ServerIsLocal = (bool)value;
					return true;
				}
				break;
			case 16:
				if (string.Equals(key, "owin.RequestPath", StringComparison.Ordinal))
				{
					this.RequestPath = (string)value;
					return true;
				}
				if (string.Equals(key, "owin.RequestBody", StringComparison.Ordinal))
				{
					this.RequestBody = (Stream)value;
					return true;
				}
				if (string.Equals(key, "host.TraceOutput", StringComparison.Ordinal))
				{
					this.HostTraceOutput = (TextWriter)value;
					return true;
				}
				if (string.Equals(key, "server.LocalPort", StringComparison.Ordinal))
				{
					this.ServerLocalPort = (string)value;
					return true;
				}
				if (string.Equals(key, "websocket.Accept", StringComparison.Ordinal))
				{
					this.WebSocketAccept = (Action<IDictionary<string, object>, Func<IDictionary<string, object>, Task>>)value;
					return true;
				}
				break;
			case 17:
				if (string.Equals(key, "owin.ResponseBody", StringComparison.Ordinal))
				{
					this.ResponseBody = (Stream)value;
					return true;
				}
				if (string.Equals(key, "server.RemotePort", StringComparison.Ordinal))
				{
					this.ServerRemotePort = (string)value;
					return true;
				}
				break;
			case 18:
				if (string.Equals(key, "owin.CallCancelled", StringComparison.Ordinal))
				{
					this.CallCancelled = (CancellationToken)value;
					return true;
				}
				if (string.Equals(key, "owin.RequestMethod", StringComparison.Ordinal))
				{
					this.RequestMethod = (string)value;
					return true;
				}
				if (string.Equals(key, "owin.RequestScheme", StringComparison.Ordinal))
				{
					this.RequestScheme = (string)value;
					return true;
				}
				if (string.Equals(key, "sendfile.SendAsync", StringComparison.Ordinal))
				{
					this.SendFileAsync = (Func<string, long, long?, CancellationToken, Task>)value;
					return true;
				}
				break;
			case 19:
				if (string.Equals(key, "owin.RequestHeaders", StringComparison.Ordinal))
				{
					this.RequestHeaders = (IDictionary<string, string[]>)value;
					return true;
				}
				if (string.Equals(key, "host.OnAppDisposing", StringComparison.Ordinal))
				{
					this.OnAppDisposing = (CancellationToken)value;
					return true;
				}
				if (string.Equals(key, "server.Capabilities", StringComparison.Ordinal))
				{
					this.ServerCapabilities = (IDictionary<string, object>)value;
					return true;
				}
				break;
			case 20:
				if (string.Equals(key, "owin.ResponseHeaders", StringComparison.Ordinal))
				{
					this.ResponseHeaders = (IDictionary<string, string[]>)value;
					return true;
				}
				if (string.Equals(key, "owin.RequestPathBase", StringComparison.Ordinal))
				{
					this.RequestPathBase = (string)value;
					return true;
				}
				if (string.Equals(key, "owin.RequestProtocol", StringComparison.Ordinal))
				{
					this.RequestProtocol = (string)value;
					return true;
				}
				break;
			case 21:
				if (string.Equals(key, "server.LocalIpAddress", StringComparison.Ordinal))
				{
					this.ServerLocalIpAddress = (string)value;
					return true;
				}
				if (string.Equals(key, "ssl.ClientCertificate", StringComparison.Ordinal))
				{
					this.ClientCert = (X509Certificate)value;
					return true;
				}
				break;
			case 22:
				if (string.Equals(key, "server.RemoteIpAddress", StringComparison.Ordinal))
				{
					this.ServerRemoteIpAddress = (string)value;
					return true;
				}
				break;
			case 23:
				if (string.Equals(key, "owin.ResponseStatusCode", StringComparison.Ordinal))
				{
					this.ResponseStatusCode = (int)value;
					return true;
				}
				if (string.Equals(key, "owin.RequestQueryString", StringComparison.Ordinal))
				{
					this.RequestQueryString = (string)value;
					return true;
				}
				if (string.Equals(key, "server.OnSendingHeaders", StringComparison.Ordinal))
				{
					this.OnSendingHeaders = (Action<Action<object>, object>)value;
					return true;
				}
				if (string.Equals(key, "ssl.LoadClientCertAsync", StringComparison.Ordinal))
				{
					this.LoadClientCert = (Func<Task>)value;
					return true;
				}
				if (string.Equals(key, "System.Net.HttpListener", StringComparison.Ordinal))
				{
					this.Listener = (HttpListener)value;
					return true;
				}
				break;
			case 25:
				if (string.Equals(key, "owin.ResponseReasonPhrase", StringComparison.Ordinal))
				{
					this.ResponseReasonPhrase = (string)value;
					return true;
				}
				break;
			case 27:
				if (string.Equals(key, "ssl.ClientCertificateErrors", StringComparison.Ordinal))
				{
					this.ClientCertErrors = (Exception)value;
					return true;
				}
				break;
			case 30:
				if (string.Equals(key, "System.Net.HttpListenerContext", StringComparison.Ordinal))
				{
					this.RequestContext = (HttpListenerContext)value;
					return true;
				}
				break;
			default:
				if (length == 49)
				{
					if (string.Equals(key, "Microsoft.Owin.Host.HttpListener.OwinHttpListener", StringComparison.Ordinal))
					{
						this.OwinHttpListener = (OwinHttpListener)value;
						return true;
					}
				}
				break;
			}
			return false;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004970 File Offset: 0x00002B70
		private bool PropertiesTryRemove(string key)
		{
			int length = key.Length;
			switch (length)
			{
			case 11:
				if ((this._flag0 & 524288U) != 0U && string.Equals(key, "server.User", StringComparison.Ordinal))
				{
					return true;
				}
				break;
			case 12:
				if ((this._flag0 & 16384U) != 0U && string.Equals(key, "owin.Version", StringComparison.Ordinal))
				{
					this._flag0 &= 4294950911U;
					this._OwinVersion = null;
					return true;
				}
				if ((this._flag0 & 65536U) != 0U && string.Equals(key, "host.AppName", StringComparison.Ordinal))
				{
					this._flag0 &= 4294901759U;
					this._HostAppName = null;
					return true;
				}
				if ((this._flag0 & 131072U) != 0U && string.Equals(key, "host.AppMode", StringComparison.Ordinal))
				{
					this._flag0 &= 4294836223U;
					this._HostAppMode = null;
					return true;
				}
				break;
			case 13:
			case 15:
			case 24:
			case 26:
			case 28:
			case 29:
				break;
			case 14:
				if ((this._flag0 & 32U) != 0U && string.Equals(key, "owin.RequestId", StringComparison.Ordinal))
				{
					this._flag0 &= 4294967263U;
					this._RequestId = null;
					return true;
				}
				if ((this._flag0 & 67108864U) != 0U && string.Equals(key, "server.IsLocal", StringComparison.Ordinal))
				{
					this._initFlag0 &= 4227858431U;
					this._flag0 &= 4227858431U;
					this._ServerIsLocal = false;
					return true;
				}
				break;
			case 16:
				if ((this._flag0 & 1U) != 0U && string.Equals(key, "owin.RequestPath", StringComparison.Ordinal))
				{
					this._flag0 &= 4294967294U;
					this._RequestPath = null;
					return true;
				}
				if ((this._flag0 & 16U) != 0U && string.Equals(key, "owin.RequestBody", StringComparison.Ordinal))
				{
					this._initFlag0 &= 4294967279U;
					this._flag0 &= 4294967279U;
					this._RequestBody = null;
					return true;
				}
				if ((this._flag0 & 32768U) != 0U && string.Equals(key, "host.TraceOutput", StringComparison.Ordinal))
				{
					this._flag0 &= 4294934527U;
					this._HostTraceOutput = null;
					return true;
				}
				if ((this._flag0 & 33554432U) != 0U && string.Equals(key, "server.LocalPort", StringComparison.Ordinal))
				{
					this._initFlag0 &= 4261412863U;
					this._flag0 &= 4261412863U;
					this._ServerLocalPort = null;
					return true;
				}
				if ((this._flag0 & 1073741824U) != 0U && string.Equals(key, "websocket.Accept", StringComparison.Ordinal))
				{
					this._initFlag0 &= 3221225471U;
					this._flag0 &= 3221225471U;
					this._WebSocketAccept = null;
					return true;
				}
				break;
			case 17:
				if ((this._flag0 & 8U) != 0U && string.Equals(key, "owin.ResponseBody", StringComparison.Ordinal))
				{
					this._flag0 &= 4294967287U;
					this._ResponseBody = null;
					return true;
				}
				if ((this._flag0 & 8388608U) != 0U && string.Equals(key, "server.RemotePort", StringComparison.Ordinal))
				{
					this._initFlag0 &= 4286578687U;
					this._flag0 &= 4286578687U;
					this._ServerRemotePort = null;
					return true;
				}
				break;
			case 18:
				if ((this._flag0 & 512U) != 0U && string.Equals(key, "owin.CallCancelled", StringComparison.Ordinal))
				{
					this._initFlag0 &= 4294966783U;
					this._flag0 &= 4294966783U;
					this._CallCancelled = default(CancellationToken);
					return true;
				}
				if ((this._flag0 & 1024U) != 0U && string.Equals(key, "owin.RequestMethod", StringComparison.Ordinal))
				{
					this._flag0 &= 4294966271U;
					this._RequestMethod = null;
					return true;
				}
				if ((this._flag0 & 2048U) != 0U && string.Equals(key, "owin.RequestScheme", StringComparison.Ordinal))
				{
					this._flag0 &= 4294965247U;
					this._RequestScheme = null;
					return true;
				}
				if ((this._flag0 & 2147483648U) != 0U && string.Equals(key, "sendfile.SendAsync", StringComparison.Ordinal))
				{
					this._flag0 &= 2147483647U;
					this._SendFileAsync = null;
					return true;
				}
				break;
			case 19:
				if ((this._flag0 & 4U) != 0U && string.Equals(key, "owin.RequestHeaders", StringComparison.Ordinal))
				{
					this._flag0 &= 4294967291U;
					this._RequestHeaders = null;
					return true;
				}
				if ((this._flag0 & 262144U) != 0U && string.Equals(key, "host.OnAppDisposing", StringComparison.Ordinal))
				{
					this._flag0 &= 4294705151U;
					this._OnAppDisposing = default(CancellationToken);
					return true;
				}
				if ((this._flag0 & 2097152U) != 0U && string.Equals(key, "server.Capabilities", StringComparison.Ordinal))
				{
					this._flag0 &= 4292870143U;
					this._ServerCapabilities = null;
					return true;
				}
				break;
			case 20:
				if ((this._flag0 & 2U) != 0U && string.Equals(key, "owin.ResponseHeaders", StringComparison.Ordinal))
				{
					this._flag0 &= 4294967293U;
					this._ResponseHeaders = null;
					return true;
				}
				if ((this._flag0 & 4096U) != 0U && string.Equals(key, "owin.RequestPathBase", StringComparison.Ordinal))
				{
					this._flag0 &= 4294963199U;
					this._RequestPathBase = null;
					return true;
				}
				if ((this._flag0 & 8192U) != 0U && string.Equals(key, "owin.RequestProtocol", StringComparison.Ordinal))
				{
					this._flag0 &= 4294959103U;
					this._RequestProtocol = null;
					return true;
				}
				break;
			case 21:
				if ((this._flag0 & 16777216U) != 0U && string.Equals(key, "server.LocalIpAddress", StringComparison.Ordinal))
				{
					this._initFlag0 &= 4278190079U;
					this._flag0 &= 4278190079U;
					this._ServerLocalIpAddress = null;
					return true;
				}
				if ((this._flag0 & 134217728U) != 0U && string.Equals(key, "ssl.ClientCertificate", StringComparison.Ordinal))
				{
					this._initFlag0 &= 4160749567U;
					this._flag0 &= 4160749567U;
					this._ClientCert = null;
					return true;
				}
				break;
			case 22:
				if ((this._flag0 & 4194304U) != 0U && string.Equals(key, "server.RemoteIpAddress", StringComparison.Ordinal))
				{
					this._initFlag0 &= 4290772991U;
					this._flag0 &= 4290772991U;
					this._ServerRemoteIpAddress = null;
					return true;
				}
				break;
			case 23:
				if ((this._flag0 & 64U) != 0U && string.Equals(key, "owin.ResponseStatusCode", StringComparison.Ordinal))
				{
					this._flag0 &= 4294967231U;
					this._ResponseStatusCode = 0;
					return true;
				}
				if ((this._flag0 & 256U) != 0U && string.Equals(key, "owin.RequestQueryString", StringComparison.Ordinal))
				{
					this._flag0 &= 4294967039U;
					this._RequestQueryString = null;
					return true;
				}
				if ((this._flag0 & 1048576U) != 0U && string.Equals(key, "server.OnSendingHeaders", StringComparison.Ordinal))
				{
					this._flag0 &= 4293918719U;
					this._OnSendingHeaders = null;
					return true;
				}
				if ((this._flag0 & 536870912U) != 0U && string.Equals(key, "ssl.LoadClientCertAsync", StringComparison.Ordinal))
				{
					this._flag0 &= 3758096383U;
					this._LoadClientCert = null;
					return true;
				}
				if ((this._flag1 & 2U) != 0U && string.Equals(key, "System.Net.HttpListener", StringComparison.Ordinal))
				{
					this._flag1 &= 4294967293U;
					this._Listener = null;
					return true;
				}
				break;
			case 25:
				if ((this._flag0 & 128U) != 0U && string.Equals(key, "owin.ResponseReasonPhrase", StringComparison.Ordinal))
				{
					this._flag0 &= 4294967167U;
					this._ResponseReasonPhrase = null;
					return true;
				}
				break;
			case 27:
				if ((this._flag0 & 268435456U) != 0U && string.Equals(key, "ssl.ClientCertificateErrors", StringComparison.Ordinal))
				{
					this._initFlag0 &= 4026531839U;
					this._flag0 &= 4026531839U;
					this._ClientCertErrors = null;
					return true;
				}
				break;
			case 30:
				if ((this._flag1 & 1U) != 0U && string.Equals(key, "System.Net.HttpListenerContext", StringComparison.Ordinal))
				{
					this._flag1 &= 4294967294U;
					this._RequestContext = null;
					return true;
				}
				break;
			default:
				if (length == 49)
				{
					if ((this._flag1 & 4U) != 0U && string.Equals(key, "Microsoft.Owin.Host.HttpListener.OwinHttpListener", StringComparison.Ordinal))
					{
						this._flag1 &= 4294967291U;
						this._OwinHttpListener = null;
						return true;
					}
				}
				break;
			}
			return false;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000521D File Offset: 0x0000341D
		private IEnumerable<string> PropertiesKeys()
		{
			if ((this._flag0 & 1U) != 0U)
			{
				yield return "owin.RequestPath";
			}
			if ((this._flag0 & 2U) != 0U)
			{
				yield return "owin.ResponseHeaders";
			}
			if ((this._flag0 & 4U) != 0U)
			{
				yield return "owin.RequestHeaders";
			}
			if ((this._flag0 & 8U) != 0U)
			{
				yield return "owin.ResponseBody";
			}
			if ((this._flag0 & 16U) != 0U)
			{
				yield return "owin.RequestBody";
			}
			if ((this._flag0 & 32U) != 0U)
			{
				yield return "owin.RequestId";
			}
			if ((this._flag0 & 64U) != 0U)
			{
				yield return "owin.ResponseStatusCode";
			}
			if ((this._flag0 & 128U) != 0U)
			{
				yield return "owin.ResponseReasonPhrase";
			}
			if ((this._flag0 & 256U) != 0U)
			{
				yield return "owin.RequestQueryString";
			}
			if ((this._flag0 & 512U) != 0U)
			{
				yield return "owin.CallCancelled";
			}
			if ((this._flag0 & 1024U) != 0U)
			{
				yield return "owin.RequestMethod";
			}
			if ((this._flag0 & 2048U) != 0U)
			{
				yield return "owin.RequestScheme";
			}
			if ((this._flag0 & 4096U) != 0U)
			{
				yield return "owin.RequestPathBase";
			}
			if ((this._flag0 & 8192U) != 0U)
			{
				yield return "owin.RequestProtocol";
			}
			if ((this._flag0 & 16384U) != 0U)
			{
				yield return "owin.Version";
			}
			if ((this._flag0 & 32768U) != 0U)
			{
				yield return "host.TraceOutput";
			}
			if ((this._flag0 & 65536U) != 0U)
			{
				yield return "host.AppName";
			}
			if ((this._flag0 & 131072U) != 0U)
			{
				yield return "host.AppMode";
			}
			if ((this._flag0 & 262144U) != 0U)
			{
				yield return "host.OnAppDisposing";
			}
			if ((this._flag0 & 524288U) != 0U)
			{
				yield return "server.User";
			}
			if ((this._flag0 & 1048576U) != 0U)
			{
				yield return "server.OnSendingHeaders";
			}
			if ((this._flag0 & 2097152U) != 0U)
			{
				yield return "server.Capabilities";
			}
			if ((this._flag0 & 4194304U) != 0U)
			{
				yield return "server.RemoteIpAddress";
			}
			if ((this._flag0 & 8388608U) != 0U)
			{
				yield return "server.RemotePort";
			}
			if ((this._flag0 & 16777216U) != 0U)
			{
				yield return "server.LocalIpAddress";
			}
			if ((this._flag0 & 33554432U) != 0U)
			{
				yield return "server.LocalPort";
			}
			if ((this._flag0 & 67108864U) != 0U)
			{
				yield return "server.IsLocal";
			}
			if ((this._flag0 & 134217728U) != 0U && ((this._initFlag0 & 134217728U) == 0U || this.InitPropertyClientCert()))
			{
				yield return "ssl.ClientCertificate";
			}
			if ((this._flag0 & 268435456U) != 0U && ((this._initFlag0 & 268435456U) == 0U || this.InitPropertyClientCertErrors()))
			{
				yield return "ssl.ClientCertificateErrors";
			}
			if ((this._flag0 & 536870912U) != 0U)
			{
				yield return "ssl.LoadClientCertAsync";
			}
			if ((this._flag0 & 1073741824U) != 0U && ((this._initFlag0 & 1073741824U) == 0U || this.InitPropertyWebSocketAccept()))
			{
				yield return "websocket.Accept";
			}
			if ((this._flag0 & 2147483648U) != 0U)
			{
				yield return "sendfile.SendAsync";
			}
			if ((this._flag1 & 1U) != 0U)
			{
				yield return "System.Net.HttpListenerContext";
			}
			if ((this._flag1 & 2U) != 0U)
			{
				yield return "System.Net.HttpListener";
			}
			if ((this._flag1 & 4U) != 0U)
			{
				yield return "Microsoft.Owin.Host.HttpListener.OwinHttpListener";
			}
			yield break;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000522D File Offset: 0x0000342D
		private IEnumerable<object> PropertiesValues()
		{
			if ((this._flag0 & 1U) != 0U)
			{
				yield return this.RequestPath;
			}
			if ((this._flag0 & 2U) != 0U)
			{
				yield return this.ResponseHeaders;
			}
			if ((this._flag0 & 4U) != 0U)
			{
				yield return this.RequestHeaders;
			}
			if ((this._flag0 & 8U) != 0U)
			{
				yield return this.ResponseBody;
			}
			if ((this._flag0 & 16U) != 0U)
			{
				yield return this.RequestBody;
			}
			if ((this._flag0 & 32U) != 0U)
			{
				yield return this.RequestId;
			}
			if ((this._flag0 & 64U) != 0U)
			{
				yield return this.ResponseStatusCode;
			}
			if ((this._flag0 & 128U) != 0U)
			{
				yield return this.ResponseReasonPhrase;
			}
			if ((this._flag0 & 256U) != 0U)
			{
				yield return this.RequestQueryString;
			}
			if ((this._flag0 & 512U) != 0U)
			{
				yield return this.CallCancelled;
			}
			if ((this._flag0 & 1024U) != 0U)
			{
				yield return this.RequestMethod;
			}
			if ((this._flag0 & 2048U) != 0U)
			{
				yield return this.RequestScheme;
			}
			if ((this._flag0 & 4096U) != 0U)
			{
				yield return this.RequestPathBase;
			}
			if ((this._flag0 & 8192U) != 0U)
			{
				yield return this.RequestProtocol;
			}
			if ((this._flag0 & 16384U) != 0U)
			{
				yield return this.OwinVersion;
			}
			if ((this._flag0 & 32768U) != 0U)
			{
				yield return this.HostTraceOutput;
			}
			if ((this._flag0 & 65536U) != 0U)
			{
				yield return this.HostAppName;
			}
			if ((this._flag0 & 131072U) != 0U)
			{
				yield return this.HostAppMode;
			}
			if ((this._flag0 & 262144U) != 0U)
			{
				yield return this.OnAppDisposing;
			}
			if ((this._flag0 & 524288U) != 0U)
			{
				yield return this.ServerUser;
			}
			if ((this._flag0 & 1048576U) != 0U)
			{
				yield return this.OnSendingHeaders;
			}
			if ((this._flag0 & 2097152U) != 0U)
			{
				yield return this.ServerCapabilities;
			}
			if ((this._flag0 & 4194304U) != 0U)
			{
				yield return this.ServerRemoteIpAddress;
			}
			if ((this._flag0 & 8388608U) != 0U)
			{
				yield return this.ServerRemotePort;
			}
			if ((this._flag0 & 16777216U) != 0U)
			{
				yield return this.ServerLocalIpAddress;
			}
			if ((this._flag0 & 33554432U) != 0U)
			{
				yield return this.ServerLocalPort;
			}
			if ((this._flag0 & 67108864U) != 0U)
			{
				yield return this.ServerIsLocal;
			}
			if ((this._flag0 & 134217728U) != 0U && ((this._initFlag0 & 134217728U) == 0U || this.InitPropertyClientCert()))
			{
				yield return this.ClientCert;
			}
			if ((this._flag0 & 268435456U) != 0U && ((this._initFlag0 & 268435456U) == 0U || this.InitPropertyClientCertErrors()))
			{
				yield return this.ClientCertErrors;
			}
			if ((this._flag0 & 536870912U) != 0U)
			{
				yield return this.LoadClientCert;
			}
			if ((this._flag0 & 1073741824U) != 0U && ((this._initFlag0 & 1073741824U) == 0U || this.InitPropertyWebSocketAccept()))
			{
				yield return this.WebSocketAccept;
			}
			if ((this._flag0 & 2147483648U) != 0U)
			{
				yield return this.SendFileAsync;
			}
			if ((this._flag1 & 1U) != 0U)
			{
				yield return this.RequestContext;
			}
			if ((this._flag1 & 2U) != 0U)
			{
				yield return this.Listener;
			}
			if ((this._flag1 & 4U) != 0U)
			{
				yield return this.OwinHttpListener;
			}
			yield break;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000523D File Offset: 0x0000343D
		private IEnumerable<KeyValuePair<string, object>> PropertiesEnumerable()
		{
			if ((this._flag0 & 1U) != 0U)
			{
				yield return new KeyValuePair<string, object>("owin.RequestPath", this.RequestPath);
			}
			if ((this._flag0 & 2U) != 0U)
			{
				yield return new KeyValuePair<string, object>("owin.ResponseHeaders", this.ResponseHeaders);
			}
			if ((this._flag0 & 4U) != 0U)
			{
				yield return new KeyValuePair<string, object>("owin.RequestHeaders", this.RequestHeaders);
			}
			if ((this._flag0 & 8U) != 0U)
			{
				yield return new KeyValuePair<string, object>("owin.ResponseBody", this.ResponseBody);
			}
			if ((this._flag0 & 16U) != 0U)
			{
				yield return new KeyValuePair<string, object>("owin.RequestBody", this.RequestBody);
			}
			if ((this._flag0 & 32U) != 0U)
			{
				yield return new KeyValuePair<string, object>("owin.RequestId", this.RequestId);
			}
			if ((this._flag0 & 64U) != 0U)
			{
				yield return new KeyValuePair<string, object>("owin.ResponseStatusCode", this.ResponseStatusCode);
			}
			if ((this._flag0 & 128U) != 0U)
			{
				yield return new KeyValuePair<string, object>("owin.ResponseReasonPhrase", this.ResponseReasonPhrase);
			}
			if ((this._flag0 & 256U) != 0U)
			{
				yield return new KeyValuePair<string, object>("owin.RequestQueryString", this.RequestQueryString);
			}
			if ((this._flag0 & 512U) != 0U)
			{
				yield return new KeyValuePair<string, object>("owin.CallCancelled", this.CallCancelled);
			}
			if ((this._flag0 & 1024U) != 0U)
			{
				yield return new KeyValuePair<string, object>("owin.RequestMethod", this.RequestMethod);
			}
			if ((this._flag0 & 2048U) != 0U)
			{
				yield return new KeyValuePair<string, object>("owin.RequestScheme", this.RequestScheme);
			}
			if ((this._flag0 & 4096U) != 0U)
			{
				yield return new KeyValuePair<string, object>("owin.RequestPathBase", this.RequestPathBase);
			}
			if ((this._flag0 & 8192U) != 0U)
			{
				yield return new KeyValuePair<string, object>("owin.RequestProtocol", this.RequestProtocol);
			}
			if ((this._flag0 & 16384U) != 0U)
			{
				yield return new KeyValuePair<string, object>("owin.Version", this.OwinVersion);
			}
			if ((this._flag0 & 32768U) != 0U)
			{
				yield return new KeyValuePair<string, object>("host.TraceOutput", this.HostTraceOutput);
			}
			if ((this._flag0 & 65536U) != 0U)
			{
				yield return new KeyValuePair<string, object>("host.AppName", this.HostAppName);
			}
			if ((this._flag0 & 131072U) != 0U)
			{
				yield return new KeyValuePair<string, object>("host.AppMode", this.HostAppMode);
			}
			if ((this._flag0 & 262144U) != 0U)
			{
				yield return new KeyValuePair<string, object>("host.OnAppDisposing", this.OnAppDisposing);
			}
			if ((this._flag0 & 524288U) != 0U)
			{
				yield return new KeyValuePair<string, object>("server.User", this.ServerUser);
			}
			if ((this._flag0 & 1048576U) != 0U)
			{
				yield return new KeyValuePair<string, object>("server.OnSendingHeaders", this.OnSendingHeaders);
			}
			if ((this._flag0 & 2097152U) != 0U)
			{
				yield return new KeyValuePair<string, object>("server.Capabilities", this.ServerCapabilities);
			}
			if ((this._flag0 & 4194304U) != 0U)
			{
				yield return new KeyValuePair<string, object>("server.RemoteIpAddress", this.ServerRemoteIpAddress);
			}
			if ((this._flag0 & 8388608U) != 0U)
			{
				yield return new KeyValuePair<string, object>("server.RemotePort", this.ServerRemotePort);
			}
			if ((this._flag0 & 16777216U) != 0U)
			{
				yield return new KeyValuePair<string, object>("server.LocalIpAddress", this.ServerLocalIpAddress);
			}
			if ((this._flag0 & 33554432U) != 0U)
			{
				yield return new KeyValuePair<string, object>("server.LocalPort", this.ServerLocalPort);
			}
			if ((this._flag0 & 67108864U) != 0U)
			{
				yield return new KeyValuePair<string, object>("server.IsLocal", this.ServerIsLocal);
			}
			if ((this._flag0 & 134217728U) != 0U && ((this._initFlag0 & 134217728U) == 0U || this.InitPropertyClientCert()))
			{
				yield return new KeyValuePair<string, object>("ssl.ClientCertificate", this.ClientCert);
			}
			if ((this._flag0 & 268435456U) != 0U && ((this._initFlag0 & 268435456U) == 0U || this.InitPropertyClientCertErrors()))
			{
				yield return new KeyValuePair<string, object>("ssl.ClientCertificateErrors", this.ClientCertErrors);
			}
			if ((this._flag0 & 536870912U) != 0U)
			{
				yield return new KeyValuePair<string, object>("ssl.LoadClientCertAsync", this.LoadClientCert);
			}
			if ((this._flag0 & 1073741824U) != 0U && ((this._initFlag0 & 1073741824U) == 0U || this.InitPropertyWebSocketAccept()))
			{
				yield return new KeyValuePair<string, object>("websocket.Accept", this.WebSocketAccept);
			}
			if ((this._flag0 & 2147483648U) != 0U)
			{
				yield return new KeyValuePair<string, object>("sendfile.SendAsync", this.SendFileAsync);
			}
			if ((this._flag1 & 1U) != 0U)
			{
				yield return new KeyValuePair<string, object>("System.Net.HttpListenerContext", this.RequestContext);
			}
			if ((this._flag1 & 2U) != 0U)
			{
				yield return new KeyValuePair<string, object>("System.Net.HttpListener", this.Listener);
			}
			if ((this._flag1 & 4U) != 0U)
			{
				yield return new KeyValuePair<string, object>("Microsoft.Owin.Host.HttpListener.OwinHttpListener", this.OwinHttpListener);
			}
			yield break;
		}

		// Token: 0x04000059 RID: 89
		private static readonly IDictionary<string, object> WeakNilEnvironment = new NilDictionary();

		// Token: 0x0400005A RID: 90
		private readonly CallEnvironment.IPropertySource _propertySource;

		// Token: 0x0400005B RID: 91
		private IDictionary<string, object> _extra = CallEnvironment.WeakNilEnvironment;

		// Token: 0x0400005C RID: 92
		private uint _flag0 = 1606943248U;

		// Token: 0x0400005D RID: 93
		private uint _flag1;

		// Token: 0x0400005E RID: 94
		private uint _initFlag0 = 1606943248U;

		// Token: 0x0400005F RID: 95
		private string _RequestPath;

		// Token: 0x04000060 RID: 96
		private IDictionary<string, string[]> _ResponseHeaders;

		// Token: 0x04000061 RID: 97
		private IDictionary<string, string[]> _RequestHeaders;

		// Token: 0x04000062 RID: 98
		private Stream _ResponseBody;

		// Token: 0x04000063 RID: 99
		private Stream _RequestBody;

		// Token: 0x04000064 RID: 100
		private string _RequestId;

		// Token: 0x04000065 RID: 101
		private int _ResponseStatusCode;

		// Token: 0x04000066 RID: 102
		private string _ResponseReasonPhrase;

		// Token: 0x04000067 RID: 103
		private string _RequestQueryString;

		// Token: 0x04000068 RID: 104
		private CancellationToken _CallCancelled;

		// Token: 0x04000069 RID: 105
		private string _RequestMethod;

		// Token: 0x0400006A RID: 106
		private string _RequestScheme;

		// Token: 0x0400006B RID: 107
		private string _RequestPathBase;

		// Token: 0x0400006C RID: 108
		private string _RequestProtocol;

		// Token: 0x0400006D RID: 109
		private string _OwinVersion;

		// Token: 0x0400006E RID: 110
		private TextWriter _HostTraceOutput;

		// Token: 0x0400006F RID: 111
		private string _HostAppName;

		// Token: 0x04000070 RID: 112
		private string _HostAppMode;

		// Token: 0x04000071 RID: 113
		private CancellationToken _OnAppDisposing;

		// Token: 0x04000072 RID: 114
		private Action<Action<object>, object> _OnSendingHeaders;

		// Token: 0x04000073 RID: 115
		private IDictionary<string, object> _ServerCapabilities;

		// Token: 0x04000074 RID: 116
		private string _ServerRemoteIpAddress;

		// Token: 0x04000075 RID: 117
		private string _ServerRemotePort;

		// Token: 0x04000076 RID: 118
		private string _ServerLocalIpAddress;

		// Token: 0x04000077 RID: 119
		private string _ServerLocalPort;

		// Token: 0x04000078 RID: 120
		private bool _ServerIsLocal;

		// Token: 0x04000079 RID: 121
		private X509Certificate _ClientCert;

		// Token: 0x0400007A RID: 122
		private Exception _ClientCertErrors;

		// Token: 0x0400007B RID: 123
		private Func<Task> _LoadClientCert;

		// Token: 0x0400007C RID: 124
		private Action<IDictionary<string, object>, Func<IDictionary<string, object>, Task>> _WebSocketAccept;

		// Token: 0x0400007D RID: 125
		private Func<string, long, long?, CancellationToken, Task> _SendFileAsync;

		// Token: 0x0400007E RID: 126
		private HttpListenerContext _RequestContext;

		// Token: 0x0400007F RID: 127
		private HttpListener _Listener;

		// Token: 0x04000080 RID: 128
		private OwinHttpListener _OwinHttpListener;

		// Token: 0x02000020 RID: 32
		internal interface IPropertySource
		{
			// Token: 0x06000147 RID: 327
			Stream GetRequestBody();

			// Token: 0x06000148 RID: 328
			CancellationToken GetCallCancelled();

			// Token: 0x06000149 RID: 329
			IPrincipal GetServerUser();

			// Token: 0x0600014A RID: 330
			void SetServerUser(IPrincipal value);

			// Token: 0x0600014B RID: 331
			string GetServerRemoteIpAddress();

			// Token: 0x0600014C RID: 332
			string GetServerRemotePort();

			// Token: 0x0600014D RID: 333
			string GetServerLocalIpAddress();

			// Token: 0x0600014E RID: 334
			string GetServerLocalPort();

			// Token: 0x0600014F RID: 335
			bool GetServerIsLocal();

			// Token: 0x06000150 RID: 336
			bool TryGetClientCert(ref X509Certificate value);

			// Token: 0x06000151 RID: 337
			bool TryGetClientCertErrors(ref Exception value);

			// Token: 0x06000152 RID: 338
			bool TryGetWebSocketAccept(ref Action<IDictionary<string, object>, Func<IDictionary<string, object>, Task>> value);
		}
	}
}
