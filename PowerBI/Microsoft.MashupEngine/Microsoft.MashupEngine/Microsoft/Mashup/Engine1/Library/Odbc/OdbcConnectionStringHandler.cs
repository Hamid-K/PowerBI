using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005D6 RID: 1494
	internal class OdbcConnectionStringHandler : ConnectionStringHandler
	{
		// Token: 0x17001107 RID: 4359
		// (get) Token: 0x06002E91 RID: 11921 RVA: 0x0008DEDD File Offset: 0x0008C0DD
		public static ConnectionStringHandler Windows
		{
			get
			{
				return OdbcConnectionStringHandler.windows.Value;
			}
		}

		// Token: 0x06002E92 RID: 11922 RVA: 0x0008DEEC File Offset: 0x0008C0EC
		public OdbcConnectionStringHandler(IOdbcService odbc)
			: base(true, true, "UID", "PWD", "Trusted_Connection", new string[] { "UIDDBMS", "PWDDBMS", "User Id", "Password" }, new string[] { "Driver", "DSN" }, new string[] { "FileDsn", "SaveFile" })
		{
			this.odbc = odbc;
		}

		// Token: 0x17001108 RID: 4360
		// (get) Token: 0x06002E93 RID: 11923 RVA: 0x0008DF69 File Offset: 0x0008C169
		protected override IEnumerable<string> HostNameKeys
		{
			get
			{
				return new string[] { "Hostname", "Host", "Server", "ServerName", "ServerNode" };
			}
		}

		// Token: 0x06002E94 RID: 11924 RVA: 0x0008DF9C File Offset: 0x0008C19C
		public override void ValidateSourcePropertyWithPermission(string key, object value, IResource resource)
		{
			base.ValidateSourcePropertyWithPermission(key, value, resource);
			if (key.Equals("Driver", StringComparison.OrdinalIgnoreCase))
			{
				string text = value as string;
				if (text != null)
				{
					text = text.Trim().TrimStart(new char[] { '{' }).TrimEnd(new char[] { '}' })
						.Trim();
					if (!this.odbc.GetInstalledDrivers().Contains(text))
					{
						throw DataSourceException.NewMissingClientLibraryError<Message1>(null, Strings.Odbc_InvalidDriverName(value), resource, text, null, null);
					}
				}
			}
		}

		// Token: 0x06002E95 RID: 11925 RVA: 0x0008E01C File Offset: 0x0008C21C
		protected override string EscapeInboundValue(string value)
		{
			if (value.StartsWith(" ", StringComparison.Ordinal) || (value.StartsWith("{", StringComparison.Ordinal) && value.EndsWith("}", StringComparison.Ordinal)) || value == string.Empty)
			{
				value = "{" + value.Replace("}", "}}") + "}";
			}
			return value;
		}

		// Token: 0x06002E96 RID: 11926 RVA: 0x0008E084 File Offset: 0x0008C284
		protected override string EscapeOutboundValue(string value)
		{
			value = value.Trim();
			if (value.StartsWith("{", StringComparison.Ordinal))
			{
				if (!value.EndsWith("}", StringComparison.Ordinal))
				{
					throw new ArgumentException(value, "value");
				}
				value = value.Substring(1, value.Length - 2).Replace("}}", "}");
			}
			return value;
		}

		// Token: 0x04001493 RID: 5267
		private static readonly Lazy<ConnectionStringHandler> windows = new Lazy<ConnectionStringHandler>(() => new OdbcConnectionStringHandler(OdbcService.WindowsInstance));

		// Token: 0x04001494 RID: 5268
		private readonly IOdbcService odbc;
	}
}
