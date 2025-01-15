using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Mashup.Engine1.Library.ActiveDirectory
{
	// Token: 0x02000FF5 RID: 4085
	internal struct LdapPath
	{
		// Token: 0x06006B28 RID: 27432 RVA: 0x001712B8 File Offset: 0x0016F4B8
		public LdapPath(string hostName, string distinguishedName)
		{
			this.hostName = hostName;
			this.distinguishedName = distinguishedName;
		}

		// Token: 0x17001EA9 RID: 7849
		// (get) Token: 0x06006B29 RID: 27433 RVA: 0x001712C8 File Offset: 0x0016F4C8
		public string Host
		{
			get
			{
				return this.hostName;
			}
		}

		// Token: 0x17001EAA RID: 7850
		// (get) Token: 0x06006B2A RID: 27434 RVA: 0x001712D0 File Offset: 0x0016F4D0
		public string DistinguishedName
		{
			get
			{
				return this.distinguishedName;
			}
		}

		// Token: 0x06006B2B RID: 27435 RVA: 0x001712D8 File Offset: 0x0016F4D8
		public override string ToString()
		{
			return "LDAP://" + this.hostName + "/" + this.distinguishedName.Replace("/", "\\/");
		}

		// Token: 0x06006B2C RID: 27436 RVA: 0x00171304 File Offset: 0x0016F504
		public static LdapPath GetDomainRoot(string domain)
		{
			return new LdapPath(domain, LdapPath.GetDomainDistinguishedName(domain));
		}

		// Token: 0x06006B2D RID: 27437 RVA: 0x00171312 File Offset: 0x0016F512
		public static LdapPath FromDistinguishedName(string distinguishedName)
		{
			return new LdapPath(LdapPath.GetDomainName(distinguishedName), distinguishedName);
		}

		// Token: 0x06006B2E RID: 27438 RVA: 0x00171320 File Offset: 0x0016F520
		public LdapPath AddPartToDistinguishedName(string part)
		{
			return new LdapPath(this.hostName, part + "," + this.distinguishedName);
		}

		// Token: 0x06006B2F RID: 27439 RVA: 0x00171340 File Offset: 0x0016F540
		public static string GetDomainName(string distinguishedName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			IEnumerable<string> enumerable = from p in LdapPath.GetDistinguishedNameParts(distinguishedName)
				where p.StartsWith("DC=", StringComparison.OrdinalIgnoreCase)
				select p.Replace("DC=", "");
			string text = "";
			foreach (string text2 in enumerable)
			{
				stringBuilder.Append(text);
				stringBuilder.Append(text2);
				text = ".";
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006B30 RID: 27440 RVA: 0x001713F8 File Offset: 0x0016F5F8
		private static string GetDomainDistinguishedName(string domainName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = "";
			foreach (string text2 in domainName.Split(new char[] { '.' }))
			{
				stringBuilder.Append(text);
				text = ",";
				stringBuilder.Append("DC=");
				stringBuilder.Append(text2);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006B31 RID: 27441 RVA: 0x00171460 File Offset: 0x0016F660
		private static List<string> GetDistinguishedNameParts(string distinguishedName)
		{
			List<string> list = new List<string>();
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			foreach (char c in distinguishedName)
			{
				if (flag)
				{
					flag = false;
					stringBuilder.Append(c);
				}
				else if (c == '\\')
				{
					flag = true;
					stringBuilder.Append(c);
				}
				else if (c == ',')
				{
					list.Add(stringBuilder.ToString());
					stringBuilder = new StringBuilder();
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			list.Add(stringBuilder.ToString().Trim());
			stringBuilder = new StringBuilder();
			return list;
		}

		// Token: 0x04003B97 RID: 15255
		public const string LdapScheme = "LDAP://";

		// Token: 0x04003B98 RID: 15256
		private readonly string hostName;

		// Token: 0x04003B99 RID: 15257
		private readonly string distinguishedName;
	}
}
