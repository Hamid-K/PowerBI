using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace System.Web.Http.Owin.Properties
{
	// Token: 0x02000015 RID: 21
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class OwinResources
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x000029BB File Offset: 0x00000BBB
		internal OwinResources()
		{
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000382B File Offset: 0x00001A2B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (OwinResources.resourceMan == null)
				{
					OwinResources.resourceMan = new ResourceManager("System.Web.Http.Owin.Properties.OwinResources", typeof(OwinResources).Assembly);
				}
				return OwinResources.resourceMan;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00003857 File Offset: 0x00001A57
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x0000385E File Offset: 0x00001A5E
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return OwinResources.resourceCulture;
			}
			set
			{
				OwinResources.resourceCulture = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003866 File Offset: 0x00001A66
		internal static string HttpAuthenticationChallengeContext_RequestMustNotBeNull
		{
			get
			{
				return OwinResources.ResourceManager.GetString("HttpAuthenticationChallengeContext_RequestMustNotBeNull", OwinResources.resourceCulture);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000387C File Offset: 0x00001A7C
		internal static string HttpAuthenticationContext_RequestMustNotBeNull
		{
			get
			{
				return OwinResources.ResourceManager.GetString("HttpAuthenticationContext_RequestMustNotBeNull", OwinResources.resourceCulture);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00003892 File Offset: 0x00001A92
		internal static string IAuthenticationManagerNotAvailable
		{
			get
			{
				return OwinResources.ResourceManager.GetString("IAuthenticationManagerNotAvailable", OwinResources.resourceCulture);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000038A8 File Offset: 0x00001AA8
		internal static string OwinContext_NullRequest
		{
			get
			{
				return OwinResources.ResourceManager.GetString("OwinContext_NullRequest", OwinResources.resourceCulture);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x000038BE File Offset: 0x00001ABE
		internal static string OwinContext_NullResponse
		{
			get
			{
				return OwinResources.ResourceManager.GetString("OwinContext_NullResponse", OwinResources.resourceCulture);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000038D4 File Offset: 0x00001AD4
		internal static string Request_RequestContextMustNotBeNull
		{
			get
			{
				return OwinResources.ResourceManager.GetString("Request_RequestContextMustNotBeNull", OwinResources.resourceCulture);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000038EA File Offset: 0x00001AEA
		internal static string SendAsync_ReturnedNull
		{
			get
			{
				return OwinResources.ResourceManager.GetString("SendAsync_ReturnedNull", OwinResources.resourceCulture);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00003900 File Offset: 0x00001B00
		internal static string TypePropertyMustNotBeNull
		{
			get
			{
				return OwinResources.ResourceManager.GetString("TypePropertyMustNotBeNull", OwinResources.resourceCulture);
			}
		}

		// Token: 0x04000031 RID: 49
		private static ResourceManager resourceMan;

		// Token: 0x04000032 RID: 50
		private static CultureInfo resourceCulture;
	}
}
