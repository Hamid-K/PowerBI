using System;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace Microsoft.OleDb
{
	// Token: 0x02001F34 RID: 7988
	internal class Strings
	{
		// Token: 0x17002FCC RID: 12236
		// (get) Token: 0x0600C3BD RID: 50109 RVA: 0x00273392 File Offset: 0x00271592
		public static ResourceManager ResourceManager
		{
			get
			{
				return Strings.ResourceLoader.Resources;
			}
		}

		// Token: 0x0600C3BE RID: 50110 RVA: 0x00273399 File Offset: 0x00271599
		public static string DefaultOleDbExceptionMessage(object p0)
		{
			return Strings.ResourceLoader.GetString("DefaultOleDbExceptionMessage", new object[] { p0 });
		}

		// Token: 0x17002FCD RID: 12237
		// (get) Token: 0x0600C3BF RID: 50111 RVA: 0x002733AF File Offset: 0x002715AF
		public static string NotLegalOADate
		{
			get
			{
				return Strings.ResourceLoader.GetString("NotLegalOADate");
			}
		}

		// Token: 0x17002FCE RID: 12238
		// (get) Token: 0x0600C3C0 RID: 50112 RVA: 0x002733BB File Offset: 0x002715BB
		public static string BigInteger_Error
		{
			get
			{
				return Strings.ResourceLoader.GetString("BigInteger_Error");
			}
		}

		// Token: 0x02001F35 RID: 7989
		private class ResourceLoader
		{
			// Token: 0x0600C3C2 RID: 50114 RVA: 0x002733C7 File Offset: 0x002715C7
			internal ResourceLoader()
			{
				this.resources = new ResourceManager("Microsoft.OleDb.Strings", base.GetType().Assembly);
			}

			// Token: 0x0600C3C3 RID: 50115 RVA: 0x002733EC File Offset: 0x002715EC
			private static Strings.ResourceLoader GetLoader()
			{
				if (Strings.ResourceLoader.instance == null)
				{
					Strings.ResourceLoader resourceLoader = new Strings.ResourceLoader();
					Interlocked.CompareExchange<Strings.ResourceLoader>(ref Strings.ResourceLoader.instance, resourceLoader, null);
				}
				return Strings.ResourceLoader.instance;
			}

			// Token: 0x17002FCF RID: 12239
			// (get) Token: 0x0600C3C4 RID: 50116 RVA: 0x000020FA File Offset: 0x000002FA
			public static CultureInfo Culture
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17002FD0 RID: 12240
			// (get) Token: 0x0600C3C5 RID: 50117 RVA: 0x00273418 File Offset: 0x00271618
			public static ResourceManager Resources
			{
				get
				{
					return Strings.ResourceLoader.GetLoader().resources;
				}
			}

			// Token: 0x0600C3C6 RID: 50118 RVA: 0x00273424 File Offset: 0x00271624
			public static string GetString(string name, params object[] args)
			{
				Strings.ResourceLoader loader = Strings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				string @string = loader.resources.GetString(name, Strings.ResourceLoader.Culture);
				if (args != null && args.Length != 0)
				{
					return string.Format(CultureInfo.CurrentCulture, @string, args);
				}
				return @string;
			}

			// Token: 0x0600C3C7 RID: 50119 RVA: 0x00273464 File Offset: 0x00271664
			public static string GetString(string name)
			{
				Strings.ResourceLoader loader = Strings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.resources.GetString(name, Strings.ResourceLoader.Culture);
			}

			// Token: 0x0600C3C8 RID: 50120 RVA: 0x00273490 File Offset: 0x00271690
			public static object GetObject(string name)
			{
				Strings.ResourceLoader loader = Strings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.resources.GetObject(name, Strings.ResourceLoader.Culture);
			}

			// Token: 0x0600C3C9 RID: 50121 RVA: 0x002734BC File Offset: 0x002716BC
			public static T GetObject<T>(string name) where T : class
			{
				Strings.ResourceLoader loader = Strings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return default(T);
				}
				return (T)((object)loader.resources.GetObject(name, Strings.ResourceLoader.Culture));
			}

			// Token: 0x040064A4 RID: 25764
			private static Strings.ResourceLoader instance;

			// Token: 0x040064A5 RID: 25765
			private ResourceManager resources;
		}
	}
}
