using System;
using System.Globalization;
using System.Security.Principal;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000062 RID: 98
	internal sealed class BackgroundRequestContext : RequestContext
	{
		// Token: 0x060002E8 RID: 744 RVA: 0x0000A8F6 File Offset: 0x00008AF6
		public BackgroundRequestContext(IServiceInstanceContext serviceInstanceContext)
		{
			this.m_serviceInstanceContext = serviceInstanceContext;
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000A905 File Offset: 0x00008B05
		public override string ApplicationPath
		{
			get
			{
				return ProcessingContext.Configuration.ConfigFilePath;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000A911 File Offset: 0x00008B11
		public override bool IsClientConnected
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000A914 File Offset: 0x00008B14
		public override IPrincipal User
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000A918 File Offset: 0x00008B18
		public override Uri ReportServerVirtualDirectoryUri
		{
			get
			{
				Uri uri;
				if (Uri.TryCreate(ProcessingContext.Configuration.UrlRootCalculated, UriKind.RelativeOrAbsolute, out uri))
				{
					return uri;
				}
				throw new ServerConfigurationErrorException(string.Format(CultureInfo.InvariantCulture, "UrlRoot is not a valid Uri: '{0}'", ProcessingContext.Configuration.UrlRootCalculated));
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000A959 File Offset: 0x00008B59
		public override bool IsClientLocal
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000A95C File Offset: 0x00008B5C
		public override string Namespace
		{
			get
			{
				return XmlConsts.DefaultNamespace;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000A963 File Offset: 0x00008B63
		public override bool IsMemberOfWindowsAdminGroup
		{
			get
			{
				return false;
			}
		}
	}
}
