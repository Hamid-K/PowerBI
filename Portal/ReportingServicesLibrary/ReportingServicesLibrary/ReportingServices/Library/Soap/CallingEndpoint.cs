using System;
using System.Diagnostics;
using System.Web;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x02000319 RID: 793
	internal static class CallingEndpoint
	{
		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06001B3C RID: 6972 RVA: 0x0006EB78 File Offset: 0x0006CD78
		internal static SoapEndpoint SoapEndpoint
		{
			[DebuggerStepThrough]
			get
			{
				if (HttpContext.Current != null && HttpContext.Current.Items.Contains("RSEndpointKey"))
				{
					object obj = HttpContext.Current.Items["RSEndpointKey"];
					if (obj != null)
					{
						try
						{
							return (SoapEndpoint)obj;
						}
						catch (OverflowException)
						{
						}
						catch (InvalidCastException)
						{
						}
						return SoapEndpoint.Endpoint2010;
					}
				}
				return SoapEndpoint.Endpoint2010;
			}
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06001B3D RID: 6973 RVA: 0x0006EBE8 File Offset: 0x0006CDE8
		internal static bool Is2005Endpoint
		{
			[DebuggerStepThrough]
			get
			{
				return SoapEndpoint.Endpoint2005 == CallingEndpoint.SoapEndpoint;
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06001B3E RID: 6974 RVA: 0x0006EBF2 File Offset: 0x0006CDF2
		internal static bool Is2006Endpoint
		{
			[DebuggerStepThrough]
			get
			{
				return SoapEndpoint.Endpoint2006 == CallingEndpoint.SoapEndpoint;
			}
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06001B3F RID: 6975 RVA: 0x0006EBFC File Offset: 0x0006CDFC
		internal static bool Is2010Endpoint
		{
			[DebuggerStepThrough]
			get
			{
				return SoapEndpoint.Endpoint2010 == CallingEndpoint.SoapEndpoint;
			}
		}

		// Token: 0x04000AAC RID: 2732
		internal const string RSEndpointKey = "RSEndpointKey";
	}
}
