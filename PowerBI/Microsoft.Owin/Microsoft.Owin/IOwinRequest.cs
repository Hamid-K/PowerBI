using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Owin
{
	// Token: 0x0200000D RID: 13
	public interface IOwinRequest
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000051 RID: 81
		IDictionary<string, object> Environment { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000052 RID: 82
		IOwinContext Context { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000053 RID: 83
		// (set) Token: 0x06000054 RID: 84
		string Method { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000055 RID: 85
		// (set) Token: 0x06000056 RID: 86
		string Scheme { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000057 RID: 87
		bool IsSecure { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000058 RID: 88
		// (set) Token: 0x06000059 RID: 89
		HostString Host { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600005A RID: 90
		// (set) Token: 0x0600005B RID: 91
		PathString PathBase { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600005C RID: 92
		// (set) Token: 0x0600005D RID: 93
		PathString Path { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600005E RID: 94
		// (set) Token: 0x0600005F RID: 95
		QueryString QueryString { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000060 RID: 96
		IReadableStringCollection Query { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000061 RID: 97
		Uri Uri { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000062 RID: 98
		// (set) Token: 0x06000063 RID: 99
		string Protocol { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000064 RID: 100
		IHeaderDictionary Headers { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000065 RID: 101
		RequestCookieCollection Cookies { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000066 RID: 102
		// (set) Token: 0x06000067 RID: 103
		string ContentType { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000068 RID: 104
		// (set) Token: 0x06000069 RID: 105
		string CacheControl { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600006A RID: 106
		// (set) Token: 0x0600006B RID: 107
		string MediaType { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600006C RID: 108
		// (set) Token: 0x0600006D RID: 109
		string Accept { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600006E RID: 110
		// (set) Token: 0x0600006F RID: 111
		Stream Body { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000070 RID: 112
		// (set) Token: 0x06000071 RID: 113
		CancellationToken CallCancelled { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000072 RID: 114
		// (set) Token: 0x06000073 RID: 115
		string LocalIpAddress { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000074 RID: 116
		// (set) Token: 0x06000075 RID: 117
		int? LocalPort { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000076 RID: 118
		// (set) Token: 0x06000077 RID: 119
		string RemoteIpAddress { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000078 RID: 120
		// (set) Token: 0x06000079 RID: 121
		int? RemotePort { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600007A RID: 122
		// (set) Token: 0x0600007B RID: 123
		IPrincipal User { get; set; }

		// Token: 0x0600007C RID: 124
		Task<IFormCollection> ReadFormAsync();

		// Token: 0x0600007D RID: 125
		T Get<T>(string key);

		// Token: 0x0600007E RID: 126
		IOwinRequest Set<T>(string key, T value);
	}
}
