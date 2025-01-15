using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Cdm
{
	// Token: 0x02000004 RID: 4
	internal class Resources
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020B3 File Offset: 0x000002B3
		public static ResourceManager ResourceManager
		{
			get
			{
				return Resources.ResourceLoader.Resources;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020BA File Offset: 0x000002BA
		public static Message0 Cdm_Contents
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Cdm_Contents");
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020C6 File Offset: 0x000002C6
		public static Message0 Cdm_Contents_Category
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Cdm_Contents_Category");
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020D2 File Offset: 0x000002D2
		public static Message0 Cdm_Contents_Description
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Cdm_Contents_Description");
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020DE File Offset: 0x000002DE
		public static Message1 Cdm_FileNotFound(object p0)
		{
			return Resources.ResourceLoader.GetMessage("Cdm_FileNotFound", p0);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020EB File Offset: 0x000002EB
		public static Message1 Cdm_InvalidFileName(object p0)
		{
			return Resources.ResourceLoader.GetMessage("Cdm_InvalidFileName", p0);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020F8 File Offset: 0x000002F8
		public static Message1 Cdm_InvalidTableParameter(object p0)
		{
			return Resources.ResourceLoader.GetMessage("Cdm_InvalidTableParameter", p0);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002105 File Offset: 0x00000305
		public static Message3 Cdm_InvalidEntityPartitionParameter(object p0, object p1, object p2)
		{
			return Resources.ResourceLoader.GetMessage("Cdm_InvalidEntityPartitionParameter", p0, p1, p2);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002114 File Offset: 0x00000314
		public static Message2 Cdm_Sdk_FailedToGetManifestName(object p0, object p1)
		{
			return Resources.ResourceLoader.GetMessage("Cdm_Sdk_FailedToGetManifestName", p0, p1);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002122 File Offset: 0x00000322
		public static Message2 Cdm_Sdk_FailedToGenerateManifest(object p0, object p1)
		{
			return Resources.ResourceLoader.GetMessage("Cdm_Sdk_FailedToGenerateManifest", p0, p1);
		}

		// Token: 0x02000005 RID: 5
		private class ResourceLoader
		{
			// Token: 0x0600000E RID: 14 RVA: 0x00002138 File Offset: 0x00000338
			internal ResourceLoader()
			{
				this.resources = new ResourceManager("Microsoft.Mashup.Cdm.Resources", base.GetType().Assembly);
			}

			// Token: 0x0600000F RID: 15 RVA: 0x0000215C File Offset: 0x0000035C
			private static Resources.ResourceLoader GetLoader()
			{
				if (Microsoft.Mashup.Cdm.Resources.ResourceLoader.instance == null)
				{
					Resources.ResourceLoader resourceLoader = new Resources.ResourceLoader();
					Interlocked.CompareExchange<Resources.ResourceLoader>(ref Microsoft.Mashup.Cdm.Resources.ResourceLoader.instance, resourceLoader, null);
				}
				return Microsoft.Mashup.Cdm.Resources.ResourceLoader.instance;
			}

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000010 RID: 16 RVA: 0x00002188 File Offset: 0x00000388
			public static CultureInfo Culture
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x06000011 RID: 17 RVA: 0x0000218B File Offset: 0x0000038B
			public static ResourceManager Resources
			{
				get
				{
					return Microsoft.Mashup.Cdm.Resources.ResourceLoader.GetLoader().resources;
				}
			}

			// Token: 0x06000012 RID: 18 RVA: 0x00002198 File Offset: 0x00000398
			public static Message0 GetMessage(string name)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.Cdm.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message0(null);
				}
				return new Message0(loader.resources.GetString(name, Microsoft.Mashup.Cdm.Resources.ResourceLoader.Culture));
			}

			// Token: 0x06000013 RID: 19 RVA: 0x000021CC File Offset: 0x000003CC
			public static Message1 GetMessage(string name, object arg0)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.Cdm.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message1(null, null);
				}
				return new Message1(loader.resources.GetString(name, Microsoft.Mashup.Cdm.Resources.ResourceLoader.Culture), arg0);
			}

			// Token: 0x06000014 RID: 20 RVA: 0x00002204 File Offset: 0x00000404
			public static Message2 GetMessage(string name, object arg0, object arg1)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.Cdm.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message2(null, null, null);
				}
				return new Message2(loader.resources.GetString(name, Microsoft.Mashup.Cdm.Resources.ResourceLoader.Culture), arg0, arg1);
			}

			// Token: 0x06000015 RID: 21 RVA: 0x0000223C File Offset: 0x0000043C
			public static Message3 GetMessage(string name, object arg0, object arg1, object arg2)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.Cdm.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message3(null, null, null, null);
				}
				return new Message3(loader.resources.GetString(name, Microsoft.Mashup.Cdm.Resources.ResourceLoader.Culture), arg0, arg1, arg2);
			}

			// Token: 0x06000016 RID: 22 RVA: 0x00002278 File Offset: 0x00000478
			public static Message4 GetMessage(string name, params object[] args)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.Cdm.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message4(null, null);
				}
				return new Message4(loader.resources.GetString(name, Microsoft.Mashup.Cdm.Resources.ResourceLoader.Culture), args);
			}

			// Token: 0x06000017 RID: 23 RVA: 0x000022B0 File Offset: 0x000004B0
			public static string GetString(string name, params object[] args)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.Cdm.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				string @string = loader.resources.GetString(name, Microsoft.Mashup.Cdm.Resources.ResourceLoader.Culture);
				if (args != null && args.Length != 0)
				{
					return string.Format(CultureInfo.CurrentCulture, @string, args);
				}
				return @string;
			}

			// Token: 0x06000018 RID: 24 RVA: 0x000022F0 File Offset: 0x000004F0
			public static string GetString(string name)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.Cdm.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.resources.GetString(name, Microsoft.Mashup.Cdm.Resources.ResourceLoader.Culture);
			}

			// Token: 0x06000019 RID: 25 RVA: 0x0000231C File Offset: 0x0000051C
			public static object GetObject(string name)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.Cdm.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.resources.GetObject(name, Microsoft.Mashup.Cdm.Resources.ResourceLoader.Culture);
			}

			// Token: 0x0600001A RID: 26 RVA: 0x00002348 File Offset: 0x00000548
			public static T GetObject<T>(string name) where T : class
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.Cdm.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return default(T);
				}
				return (T)((object)loader.resources.GetObject(name, Microsoft.Mashup.Cdm.Resources.ResourceLoader.Culture));
			}

			// Token: 0x04000003 RID: 3
			private static Resources.ResourceLoader instance;

			// Token: 0x04000004 RID: 4
			private ResourceManager resources;
		}
	}
}
