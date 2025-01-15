using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Owin.Security
{
	// Token: 0x0200001E RID: 30
	public class AuthenticationDescription
	{
		// Token: 0x06000166 RID: 358 RVA: 0x00003DC9 File Offset: 0x00001FC9
		public AuthenticationDescription()
		{
			this.Properties = new Dictionary<string, object>(StringComparer.Ordinal);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00003DE1 File Offset: 0x00001FE1
		public AuthenticationDescription(IDictionary<string, object> properties)
		{
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			this.Properties = properties;
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00003DFE File Offset: 0x00001FFE
		// (set) Token: 0x06000169 RID: 361 RVA: 0x00003E06 File Offset: 0x00002006
		public IDictionary<string, object> Properties { get; private set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00003E0F File Offset: 0x0000200F
		// (set) Token: 0x0600016B RID: 363 RVA: 0x00003E1C File Offset: 0x0000201C
		public string AuthenticationType
		{
			get
			{
				return this.GetString("AuthenticationType");
			}
			set
			{
				this.Properties["AuthenticationType"] = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00003E2F File Offset: 0x0000202F
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00003E3C File Offset: 0x0000203C
		public string Caption
		{
			get
			{
				return this.GetString("Caption");
			}
			set
			{
				this.Properties["Caption"] = value;
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00003E50 File Offset: 0x00002050
		private string GetString(string name)
		{
			object value;
			if (this.Properties.TryGetValue(name, out value))
			{
				return Convert.ToString(value, CultureInfo.InvariantCulture);
			}
			return null;
		}

		// Token: 0x0400003D RID: 61
		private const string CaptionPropertyKey = "Caption";

		// Token: 0x0400003E RID: 62
		private const string AuthenticationTypePropertyKey = "AuthenticationType";
	}
}
