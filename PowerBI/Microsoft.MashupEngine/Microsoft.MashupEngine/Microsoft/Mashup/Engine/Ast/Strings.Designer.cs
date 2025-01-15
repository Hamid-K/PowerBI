using System;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BCF RID: 7119
	internal class Strings
	{
		// Token: 0x17002CA9 RID: 11433
		// (get) Token: 0x0600B1A8 RID: 45480 RVA: 0x00243DF6 File Offset: 0x00241FF6
		public static ResourceManager ResourceManager
		{
			get
			{
				return Strings.ResourceLoader.Resources;
			}
		}

		// Token: 0x17002CAA RID: 11434
		// (get) Token: 0x0600B1A9 RID: 45481 RVA: 0x00243DFD File Offset: 0x00241FFD
		public static string DocumentReader_ParseDepth
		{
			get
			{
				return Strings.ResourceLoader.GetString("DocumentReader_ParseDepth");
			}
		}

		// Token: 0x17002CAB RID: 11435
		// (get) Token: 0x0600B1AA RID: 45482 RVA: 0x00243E09 File Offset: 0x00242009
		public static string CyclicReference
		{
			get
			{
				return Strings.ResourceLoader.GetString("CyclicReference");
			}
		}

		// Token: 0x02001BD0 RID: 7120
		private class ResourceLoader
		{
			// Token: 0x0600B1AC RID: 45484 RVA: 0x00243E15 File Offset: 0x00242015
			internal ResourceLoader()
			{
				this.resources = new ResourceManager("Microsoft.Mashup.Engine.Ast.Strings", base.GetType().Assembly);
			}

			// Token: 0x0600B1AD RID: 45485 RVA: 0x00243E38 File Offset: 0x00242038
			private static Strings.ResourceLoader GetLoader()
			{
				if (Strings.ResourceLoader.instance == null)
				{
					Strings.ResourceLoader resourceLoader = new Strings.ResourceLoader();
					Interlocked.CompareExchange<Strings.ResourceLoader>(ref Strings.ResourceLoader.instance, resourceLoader, null);
				}
				return Strings.ResourceLoader.instance;
			}

			// Token: 0x17002CAC RID: 11436
			// (get) Token: 0x0600B1AE RID: 45486 RVA: 0x000020FA File Offset: 0x000002FA
			public static CultureInfo Culture
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17002CAD RID: 11437
			// (get) Token: 0x0600B1AF RID: 45487 RVA: 0x00243E64 File Offset: 0x00242064
			public static ResourceManager Resources
			{
				get
				{
					return Strings.ResourceLoader.GetLoader().resources;
				}
			}

			// Token: 0x0600B1B0 RID: 45488 RVA: 0x00243E70 File Offset: 0x00242070
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

			// Token: 0x0600B1B1 RID: 45489 RVA: 0x00243EB0 File Offset: 0x002420B0
			public static string GetString(string name)
			{
				Strings.ResourceLoader loader = Strings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.resources.GetString(name, Strings.ResourceLoader.Culture);
			}

			// Token: 0x0600B1B2 RID: 45490 RVA: 0x00243EDC File Offset: 0x002420DC
			public static object GetObject(string name)
			{
				Strings.ResourceLoader loader = Strings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.resources.GetObject(name, Strings.ResourceLoader.Culture);
			}

			// Token: 0x0600B1B3 RID: 45491 RVA: 0x00243F08 File Offset: 0x00242108
			public static T GetObject<T>(string name) where T : class
			{
				Strings.ResourceLoader loader = Strings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return default(T);
				}
				return (T)((object)loader.resources.GetObject(name, Strings.ResourceLoader.Culture));
			}

			// Token: 0x04005B12 RID: 23314
			private static Strings.ResourceLoader instance;

			// Token: 0x04005B13 RID: 23315
			private ResourceManager resources;
		}
	}
}
