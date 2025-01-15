using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Microsoft.Owin.Infrastructure
{
	// Token: 0x02000031 RID: 49
	public class ChunkingCookieManager : ICookieManager
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x00004FB8 File Offset: 0x000031B8
		public ChunkingCookieManager()
		{
			this.ChunkSize = new int?(4090);
			this.ThrowForPartialCookies = true;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00004FD7 File Offset: 0x000031D7
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x00004FDF File Offset: 0x000031DF
		public int? ChunkSize { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00004FE8 File Offset: 0x000031E8
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x00004FF0 File Offset: 0x000031F0
		public bool ThrowForPartialCookies { get; set; }

		// Token: 0x060001F5 RID: 501 RVA: 0x00004FFC File Offset: 0x000031FC
		private static int ParseChunksCount(string value)
		{
			if (value != null && value.StartsWith("chunks:", StringComparison.Ordinal))
			{
				string chunksCountString = value.Substring("chunks:".Length);
				int chunksCount;
				if (int.TryParse(chunksCountString, NumberStyles.None, CultureInfo.InvariantCulture, out chunksCount))
				{
					return chunksCount;
				}
			}
			return 0;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00005040 File Offset: 0x00003240
		public string GetRequestCookie(IOwinContext context, string key)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			RequestCookieCollection requestCookies = context.Request.Cookies;
			string value = requestCookies[key];
			int chunksCount = ChunkingCookieManager.ParseChunksCount(value);
			if (chunksCount > 0)
			{
				bool quoted = false;
				List<string> chunks = new List<string>(10);
				int chunkId = 1;
				while (chunkId <= chunksCount)
				{
					string chunk = requestCookies[key + "C" + chunkId.ToString(CultureInfo.InvariantCulture)];
					if (chunk == null)
					{
						if (this.ThrowForPartialCookies)
						{
							int totalSize = 0;
							for (int i = 0; i < chunkId - 1; i++)
							{
								totalSize += chunks[i].Length;
							}
							throw new FormatException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_ImcompleteChunkedCookie, new object[]
							{
								chunkId - 1,
								chunksCount,
								totalSize
							}));
						}
						return value;
					}
					else
					{
						if (ChunkingCookieManager.IsQuoted(chunk))
						{
							quoted = true;
							chunk = ChunkingCookieManager.RemoveQuotes(chunk);
						}
						chunks.Add(chunk);
						chunkId++;
					}
				}
				string merged = string.Join(string.Empty, chunks);
				if (quoted)
				{
					merged = ChunkingCookieManager.Quote(merged);
				}
				return merged;
			}
			return value;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000516C File Offset: 0x0000336C
		public void AppendResponseCookie(IOwinContext context, string key, string value, CookieOptions options)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			bool domainHasValue = !string.IsNullOrEmpty(options.Domain);
			bool pathHasValue = !string.IsNullOrEmpty(options.Path);
			bool expiresHasValue = options.Expires != null;
			bool sameSiteHasValue = options.SameSite != null;
			string escapedKey = Uri.EscapeDataString(key);
			string prefix = escapedKey + "=";
			string suffix = string.Concat(new string[]
			{
				(!domainHasValue) ? null : "; domain=",
				(!domainHasValue) ? null : options.Domain,
				(!pathHasValue) ? null : "; path=",
				(!pathHasValue) ? null : options.Path,
				(!expiresHasValue) ? null : "; expires=",
				(!expiresHasValue) ? null : options.Expires.Value.ToString("ddd, dd-MMM-yyyy HH:mm:ss \\G\\M\\T", CultureInfo.InvariantCulture),
				(!options.Secure) ? null : "; secure",
				(!options.HttpOnly) ? null : "; HttpOnly",
				(!sameSiteHasValue) ? null : "; SameSite=",
				(!sameSiteHasValue) ? null : ChunkingCookieManager.GetStringRepresentationOfSameSite(options.SameSite.Value)
			});
			value = value ?? string.Empty;
			bool quoted = false;
			if (ChunkingCookieManager.IsQuoted(value))
			{
				quoted = true;
				value = ChunkingCookieManager.RemoveQuotes(value);
			}
			string escapedValue = Uri.EscapeDataString(value);
			IHeaderDictionary responseHeaders = context.Response.Headers;
			if (this.ChunkSize == null || this.ChunkSize.Value > prefix.Length + escapedValue.Length + suffix.Length + (quoted ? 2 : 0))
			{
				string setCookieValue = prefix + (quoted ? ChunkingCookieManager.Quote(escapedValue) : escapedValue) + suffix;
				responseHeaders.AppendValues("Set-Cookie", new string[] { setCookieValue });
				return;
			}
			if (this.ChunkSize.Value < prefix.Length + suffix.Length + (quoted ? 2 : 0) + 10)
			{
				throw new InvalidOperationException(Resources.Exception_CookieLimitTooSmall);
			}
			int dataSizePerCookie = this.ChunkSize.Value - prefix.Length - suffix.Length - (quoted ? 2 : 0) - 3;
			int cookieChunkCount = (int)Math.Ceiling((double)escapedValue.Length * 1.0 / (double)dataSizePerCookie);
			responseHeaders.AppendValues("Set-Cookie", new string[] { prefix + "chunks:" + cookieChunkCount.ToString(CultureInfo.InvariantCulture) + suffix });
			string[] chunks = new string[cookieChunkCount];
			int offset = 0;
			for (int chunkId = 1; chunkId <= cookieChunkCount; chunkId++)
			{
				int remainingLength = escapedValue.Length - offset;
				int length = Math.Min(dataSizePerCookie, remainingLength);
				string segment = escapedValue.Substring(offset, length);
				offset += length;
				chunks[chunkId - 1] = string.Concat(new string[]
				{
					escapedKey,
					"C",
					chunkId.ToString(CultureInfo.InvariantCulture),
					"=",
					quoted ? "\"" : string.Empty,
					segment,
					quoted ? "\"" : string.Empty,
					suffix
				});
			}
			responseHeaders.AppendValues("Set-Cookie", chunks);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000054E4 File Offset: 0x000036E4
		public void DeleteCookie(IOwinContext context, string key, CookieOptions options)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			string escapedKey = Uri.EscapeDataString(key);
			List<string> keys = new List<string>();
			keys.Add(escapedKey + "=");
			RequestCookieCollection requestCookies = context.Request.Cookies;
			string requestCookie = requestCookies[key];
			long chunks = (long)ChunkingCookieManager.ParseChunksCount(requestCookie);
			if (chunks > 0L)
			{
				int i = 1;
				while ((long)i <= chunks + 1L)
				{
					string subkey = escapedKey + "C" + i.ToString(CultureInfo.InvariantCulture);
					if (string.IsNullOrEmpty(requestCookies[subkey]))
					{
						chunks = (long)(i - 1);
						break;
					}
					keys.Add(subkey + "=");
					i++;
				}
			}
			bool domainHasValue = !string.IsNullOrEmpty(options.Domain);
			bool pathHasValue = !string.IsNullOrEmpty(options.Path);
			Func<string, bool> predicate = (string value) => keys.Any((string k) => value.StartsWith(k, StringComparison.OrdinalIgnoreCase));
			Func<string, bool> rejectPredicate;
			if (domainHasValue)
			{
				rejectPredicate = (string value) => predicate(value) && value.IndexOf("domain=" + options.Domain, StringComparison.OrdinalIgnoreCase) != -1;
			}
			else if (pathHasValue)
			{
				rejectPredicate = (string value) => predicate(value) && value.IndexOf("path=" + options.Path, StringComparison.OrdinalIgnoreCase) != -1;
			}
			else
			{
				rejectPredicate = (string value) => predicate(value);
			}
			IHeaderDictionary responseHeaders = context.Response.Headers;
			IList<string> existingValues = responseHeaders.GetValues("Set-Cookie");
			if (existingValues != null)
			{
				responseHeaders.SetValues("Set-Cookie", existingValues.Where((string value) => !rejectPredicate(value)).ToArray<string>());
			}
			this.AppendResponseCookie(context, key, string.Empty, new CookieOptions
			{
				Path = options.Path,
				Domain = options.Domain,
				HttpOnly = options.HttpOnly,
				SameSite = options.SameSite,
				Secure = options.Secure,
				Expires = new DateTime?(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
			});
			int j = 1;
			while ((long)j <= chunks)
			{
				this.AppendResponseCookie(context, key + "C" + j.ToString(CultureInfo.InvariantCulture), string.Empty, new CookieOptions
				{
					Path = options.Path,
					Domain = options.Domain,
					HttpOnly = options.HttpOnly,
					SameSite = options.SameSite,
					Secure = options.Secure,
					Expires = new DateTime?(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
				});
				j++;
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x000057BA File Offset: 0x000039BA
		private static bool IsQuoted(string value)
		{
			return value.Length >= 2 && value[0] == '"' && value[value.Length - 1] == '"';
		}

		// Token: 0x060001FA RID: 506 RVA: 0x000057E4 File Offset: 0x000039E4
		private static string RemoveQuotes(string value)
		{
			return value.Substring(1, value.Length - 2);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x000057F5 File Offset: 0x000039F5
		private static string Quote(string value)
		{
			return "\"" + value + "\"";
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00005808 File Offset: 0x00003A08
		private static string GetStringRepresentationOfSameSite(SameSiteMode siteMode)
		{
			switch (siteMode)
			{
			case SameSiteMode.None:
				return "None";
			case SameSiteMode.Lax:
				return "Lax";
			case SameSiteMode.Strict:
				return "Strict";
			default:
				throw new ArgumentOutOfRangeException("siteMode", string.Format(CultureInfo.InvariantCulture, "Unexpected SameSiteMode value: {0}", new object[] { siteMode }));
			}
		}
	}
}
