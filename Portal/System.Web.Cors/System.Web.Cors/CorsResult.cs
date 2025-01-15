using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Cors.Properties;

namespace System.Web.Cors
{
	// Token: 0x02000007 RID: 7
	public class CorsResult
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00002867 File Offset: 0x00000A67
		public CorsResult()
		{
			this.AllowedMethods = new List<string>();
			this.AllowedHeaders = new List<string>();
			this.AllowedExposedHeaders = new List<string>();
			this.ErrorMessages = new List<string>();
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000289B File Offset: 0x00000A9B
		public bool IsValid
		{
			get
			{
				return this.ErrorMessages.Count == 0;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000028AB File Offset: 0x00000AAB
		// (set) Token: 0x06000031 RID: 49 RVA: 0x000028B3 File Offset: 0x00000AB3
		public IList<string> ErrorMessages { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000028BC File Offset: 0x00000ABC
		// (set) Token: 0x06000033 RID: 51 RVA: 0x000028C4 File Offset: 0x00000AC4
		public string AllowedOrigin { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000028CD File Offset: 0x00000ACD
		// (set) Token: 0x06000035 RID: 53 RVA: 0x000028D5 File Offset: 0x00000AD5
		public bool SupportsCredentials { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000028DE File Offset: 0x00000ADE
		// (set) Token: 0x06000037 RID: 55 RVA: 0x000028E6 File Offset: 0x00000AE6
		public IList<string> AllowedMethods { get; private set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000028EF File Offset: 0x00000AEF
		// (set) Token: 0x06000039 RID: 57 RVA: 0x000028F7 File Offset: 0x00000AF7
		public IList<string> AllowedHeaders { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002900 File Offset: 0x00000B00
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002908 File Offset: 0x00000B08
		public IList<string> AllowedExposedHeaders { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002911 File Offset: 0x00000B11
		// (set) Token: 0x0600003D RID: 61 RVA: 0x0000291C File Offset: 0x00000B1C
		public long? PreflightMaxAge
		{
			get
			{
				return this._preflightMaxAge;
			}
			set
			{
				long? num = value;
				long num2 = 0L;
				if ((num.GetValueOrDefault() < num2) & (num != null))
				{
					throw new ArgumentOutOfRangeException("value", SRResources.PreflightMaxAgeOutOfRange);
				}
				this._preflightMaxAge = value;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000295C File Offset: 0x00000B5C
		public virtual IDictionary<string, string> ToResponseHeaders()
		{
			IDictionary<string, string> dictionary = new Dictionary<string, string>();
			if (this.AllowedOrigin != null)
			{
				dictionary.Add(CorsConstants.AccessControlAllowOrigin, this.AllowedOrigin);
			}
			if (this.SupportsCredentials)
			{
				dictionary.Add(CorsConstants.AccessControlAllowCredentials, "true");
			}
			if (this.AllowedMethods.Count > 0)
			{
				IEnumerable<string> enumerable = this.AllowedMethods.Where((string m) => !CorsConstants.SimpleMethods.Contains(m, StringComparer.OrdinalIgnoreCase));
				CorsResult.AddHeader(dictionary, CorsConstants.AccessControlAllowMethods, enumerable);
			}
			if (this.AllowedHeaders.Count > 0)
			{
				IEnumerable<string> enumerable2 = this.AllowedHeaders.Where((string header) => !CorsConstants.SimpleRequestHeaders.Contains(header, StringComparer.OrdinalIgnoreCase));
				CorsResult.AddHeader(dictionary, CorsConstants.AccessControlAllowHeaders, enumerable2);
			}
			if (this.AllowedExposedHeaders.Count > 0)
			{
				IEnumerable<string> enumerable3 = this.AllowedExposedHeaders.Where((string header) => !CorsConstants.SimpleResponseHeaders.Contains(header, StringComparer.OrdinalIgnoreCase));
				CorsResult.AddHeader(dictionary, CorsConstants.AccessControlExposeHeaders, enumerable3);
			}
			if (this.PreflightMaxAge != null)
			{
				dictionary.Add(CorsConstants.AccessControlMaxAge, this.PreflightMaxAge.ToString());
			}
			return dictionary;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002AA4 File Offset: 0x00000CA4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("IsValid: ");
			stringBuilder.Append(this.IsValid);
			stringBuilder.Append(", AllowCredentials: ");
			stringBuilder.Append(this.SupportsCredentials);
			stringBuilder.Append(", PreflightMaxAge: ");
			stringBuilder.Append((this.PreflightMaxAge != null) ? this.PreflightMaxAge.Value.ToString(CultureInfo.InvariantCulture) : "null");
			stringBuilder.Append(", AllowOrigin: ");
			stringBuilder.Append(this.AllowedOrigin);
			stringBuilder.Append(", AllowExposedHeaders: {");
			stringBuilder.Append(string.Join(",", this.AllowedExposedHeaders));
			stringBuilder.Append("}");
			stringBuilder.Append(", AllowHeaders: {");
			stringBuilder.Append(string.Join(",", this.AllowedHeaders));
			stringBuilder.Append("}");
			stringBuilder.Append(", AllowMethods: {");
			stringBuilder.Append(string.Join(",", this.AllowedMethods));
			stringBuilder.Append("}");
			stringBuilder.Append(", ErrorMessages: {");
			stringBuilder.Append(string.Join(",", this.ErrorMessages));
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002C08 File Offset: 0x00000E08
		private static void AddHeader(IDictionary<string, string> headers, string headerName, IEnumerable<string> headerValues)
		{
			string text = string.Join(",", headerValues);
			if (!string.IsNullOrEmpty(text))
			{
				headers.Add(headerName, text);
			}
		}

		// Token: 0x0400001F RID: 31
		private long? _preflightMaxAge;
	}
}
