using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web.Cors.Properties;

namespace System.Web.Cors
{
	// Token: 0x02000002 RID: 2
	public class CorsPolicy
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public CorsPolicy()
		{
			this.ExposedHeaders = new List<string>();
			this.Headers = new List<string>();
			this.Methods = new List<string>();
			this.Origins = new List<string>();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002084 File Offset: 0x00000284
		// (set) Token: 0x06000003 RID: 3 RVA: 0x0000208C File Offset: 0x0000028C
		public bool AllowAnyHeader { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002095 File Offset: 0x00000295
		// (set) Token: 0x06000005 RID: 5 RVA: 0x0000209D File Offset: 0x0000029D
		public bool AllowAnyMethod { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020A6 File Offset: 0x000002A6
		// (set) Token: 0x06000007 RID: 7 RVA: 0x000020AE File Offset: 0x000002AE
		public bool AllowAnyOrigin { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020B7 File Offset: 0x000002B7
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000020BF File Offset: 0x000002BF
		public IList<string> ExposedHeaders { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020C8 File Offset: 0x000002C8
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000020D0 File Offset: 0x000002D0
		public IList<string> Headers { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020D9 File Offset: 0x000002D9
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000020E1 File Offset: 0x000002E1
		public IList<string> Methods { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000020EA File Offset: 0x000002EA
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000020F2 File Offset: 0x000002F2
		public IList<string> Origins { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000020FB File Offset: 0x000002FB
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00002104 File Offset: 0x00000304
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

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002141 File Offset: 0x00000341
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002149 File Offset: 0x00000349
		public bool SupportsCredentials { get; set; }

		// Token: 0x06000014 RID: 20 RVA: 0x00002154 File Offset: 0x00000354
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("AllowAnyHeader: ");
			stringBuilder.Append(this.AllowAnyHeader);
			stringBuilder.Append(", AllowAnyMethod: ");
			stringBuilder.Append(this.AllowAnyMethod);
			stringBuilder.Append(", AllowAnyOrigin: ");
			stringBuilder.Append(this.AllowAnyOrigin);
			stringBuilder.Append(", PreflightMaxAge: ");
			stringBuilder.Append((this.PreflightMaxAge != null) ? this.PreflightMaxAge.Value.ToString(CultureInfo.InvariantCulture) : "null");
			stringBuilder.Append(", SupportsCredentials: ");
			stringBuilder.Append(this.SupportsCredentials);
			stringBuilder.Append(", Origins: {");
			stringBuilder.Append(string.Join(",", this.Origins));
			stringBuilder.Append("}");
			stringBuilder.Append(", Methods: {");
			stringBuilder.Append(string.Join(",", this.Methods));
			stringBuilder.Append("}");
			stringBuilder.Append(", Headers: {");
			stringBuilder.Append(string.Join(",", this.Headers));
			stringBuilder.Append("}");
			stringBuilder.Append(", ExposedHeaders: {");
			stringBuilder.Append(string.Join(",", this.ExposedHeaders));
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x04000001 RID: 1
		private long? _preflightMaxAge;
	}
}
