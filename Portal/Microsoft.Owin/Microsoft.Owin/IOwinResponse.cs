using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Owin
{
	// Token: 0x0200000E RID: 14
	public interface IOwinResponse
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600007F RID: 127
		IDictionary<string, object> Environment { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000080 RID: 128
		IOwinContext Context { get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000081 RID: 129
		// (set) Token: 0x06000082 RID: 130
		int StatusCode { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000083 RID: 131
		// (set) Token: 0x06000084 RID: 132
		string ReasonPhrase { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000085 RID: 133
		// (set) Token: 0x06000086 RID: 134
		string Protocol { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000087 RID: 135
		IHeaderDictionary Headers { get; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000088 RID: 136
		ResponseCookieCollection Cookies { get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000089 RID: 137
		// (set) Token: 0x0600008A RID: 138
		long? ContentLength { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600008B RID: 139
		// (set) Token: 0x0600008C RID: 140
		string ContentType { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600008D RID: 141
		// (set) Token: 0x0600008E RID: 142
		DateTimeOffset? Expires { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600008F RID: 143
		// (set) Token: 0x06000090 RID: 144
		string ETag { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000091 RID: 145
		// (set) Token: 0x06000092 RID: 146
		Stream Body { get; set; }

		// Token: 0x06000093 RID: 147
		void OnSendingHeaders(Action<object> callback, object state);

		// Token: 0x06000094 RID: 148
		void Redirect(string location);

		// Token: 0x06000095 RID: 149
		void Write(string text);

		// Token: 0x06000096 RID: 150
		void Write(byte[] data);

		// Token: 0x06000097 RID: 151
		void Write(byte[] data, int offset, int count);

		// Token: 0x06000098 RID: 152
		Task WriteAsync(string text);

		// Token: 0x06000099 RID: 153
		Task WriteAsync(string text, CancellationToken token);

		// Token: 0x0600009A RID: 154
		Task WriteAsync(byte[] data);

		// Token: 0x0600009B RID: 155
		Task WriteAsync(byte[] data, CancellationToken token);

		// Token: 0x0600009C RID: 156
		Task WriteAsync(byte[] data, int offset, int count, CancellationToken token);

		// Token: 0x0600009D RID: 157
		T Get<T>(string key);

		// Token: 0x0600009E RID: 158
		IOwinResponse Set<T>(string key, T value);
	}
}
