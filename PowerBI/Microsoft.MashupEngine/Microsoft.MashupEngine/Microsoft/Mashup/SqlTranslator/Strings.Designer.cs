using System;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace Microsoft.Mashup.SqlTranslator
{
	// Token: 0x02002040 RID: 8256
	internal class Strings
	{
		// Token: 0x170030A4 RID: 12452
		// (get) Token: 0x0600CA1C RID: 51740 RVA: 0x002869C5 File Offset: 0x00284BC5
		public static ResourceManager ResourceManager
		{
			get
			{
				return Strings.ResourceLoader.Resources;
			}
		}

		// Token: 0x170030A5 RID: 12453
		// (get) Token: 0x0600CA1D RID: 51741 RVA: 0x002869CC File Offset: 0x00284BCC
		public static string ValueException_CyclicReference
		{
			get
			{
				return Strings.ResourceLoader.GetString("ValueException_CyclicReference");
			}
		}

		// Token: 0x02002041 RID: 8257
		private class ResourceLoader
		{
			// Token: 0x0600CA1F RID: 51743 RVA: 0x002869D8 File Offset: 0x00284BD8
			internal ResourceLoader()
			{
				this.resources = new ResourceManager("Microsoft.Mashup.SqlTranslator.Strings", base.GetType().Assembly);
			}

			// Token: 0x0600CA20 RID: 51744 RVA: 0x002869FC File Offset: 0x00284BFC
			private static Strings.ResourceLoader GetLoader()
			{
				if (Strings.ResourceLoader.instance == null)
				{
					Strings.ResourceLoader resourceLoader = new Strings.ResourceLoader();
					Interlocked.CompareExchange<Strings.ResourceLoader>(ref Strings.ResourceLoader.instance, resourceLoader, null);
				}
				return Strings.ResourceLoader.instance;
			}

			// Token: 0x170030A6 RID: 12454
			// (get) Token: 0x0600CA21 RID: 51745 RVA: 0x000020FA File Offset: 0x000002FA
			public static CultureInfo Culture
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170030A7 RID: 12455
			// (get) Token: 0x0600CA22 RID: 51746 RVA: 0x00286A28 File Offset: 0x00284C28
			public static ResourceManager Resources
			{
				get
				{
					return Strings.ResourceLoader.GetLoader().resources;
				}
			}

			// Token: 0x0600CA23 RID: 51747 RVA: 0x00286A34 File Offset: 0x00284C34
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

			// Token: 0x0600CA24 RID: 51748 RVA: 0x00286A74 File Offset: 0x00284C74
			public static string GetString(string name)
			{
				Strings.ResourceLoader loader = Strings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.resources.GetString(name, Strings.ResourceLoader.Culture);
			}

			// Token: 0x0600CA25 RID: 51749 RVA: 0x00286AA0 File Offset: 0x00284CA0
			public static object GetObject(string name)
			{
				Strings.ResourceLoader loader = Strings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.resources.GetObject(name, Strings.ResourceLoader.Culture);
			}

			// Token: 0x0600CA26 RID: 51750 RVA: 0x00286ACC File Offset: 0x00284CCC
			public static T GetObject<T>(string name) where T : class
			{
				Strings.ResourceLoader loader = Strings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return default(T);
				}
				return (T)((object)loader.resources.GetObject(name, Strings.ResourceLoader.Culture));
			}

			// Token: 0x040066CF RID: 26319
			private static Strings.ResourceLoader instance;

			// Token: 0x040066D0 RID: 26320
			private ResourceManager resources;
		}
	}
}
