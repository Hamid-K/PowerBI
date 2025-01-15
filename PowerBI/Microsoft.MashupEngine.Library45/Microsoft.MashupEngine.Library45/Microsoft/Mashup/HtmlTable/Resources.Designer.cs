using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.HtmlTable
{
	// Token: 0x0200002B RID: 43
	internal class Resources
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00005F47 File Offset: 0x00004147
		public static ResourceManager ResourceManager
		{
			get
			{
				return Resources.ResourceLoader.Resources;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00005F4E File Offset: 0x0000414E
		public static Message0 Html_Table_Category
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Html_Table_Category");
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00005F5A File Offset: 0x0000415A
		public static Message0 Html_Table
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Html_Table");
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005F66 File Offset: 0x00004166
		public static Message3 Html_Table_Description(object p0, object p1, object p2)
		{
			return Resources.ResourceLoader.GetMessage("Html_Table_Description", p0, p1, p2);
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00005F75 File Offset: 0x00004175
		public static Message0 Html_Table_Example1
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Html_Table_Example1");
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00005F81 File Offset: 0x00004181
		public static Message0 Html_Table_Example1_Code
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Html_Table_Example1_Code");
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00005F8D File Offset: 0x0000418D
		public static Message0 Html_Table_Example1_Result
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Html_Table_Example1_Result");
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00005F99 File Offset: 0x00004199
		public static Message0 Html_Table_Example2
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Html_Table_Example2");
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00005FA5 File Offset: 0x000041A5
		public static Message0 Html_Table_Example2_Code
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Html_Table_Example2_Code");
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00005FB1 File Offset: 0x000041B1
		public static Message0 Html_Table_Example2_Result
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Html_Table_Example2_Result");
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00005FBD File Offset: 0x000041BD
		public static Message0 RowSelectorRequired
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("RowSelectorRequired");
			}
		}

		// Token: 0x0200002C RID: 44
		private class ResourceLoader
		{
			// Token: 0x060000F5 RID: 245 RVA: 0x00005FC9 File Offset: 0x000041C9
			internal ResourceLoader()
			{
				this.resources = new ResourceManager("Microsoft.Mashup.HtmlTable.Resources", base.GetType().Assembly);
			}

			// Token: 0x060000F6 RID: 246 RVA: 0x00005FEC File Offset: 0x000041EC
			private static Resources.ResourceLoader GetLoader()
			{
				if (Microsoft.Mashup.HtmlTable.Resources.ResourceLoader.instance == null)
				{
					Resources.ResourceLoader resourceLoader = new Resources.ResourceLoader();
					Interlocked.CompareExchange<Resources.ResourceLoader>(ref Microsoft.Mashup.HtmlTable.Resources.ResourceLoader.instance, resourceLoader, null);
				}
				return Microsoft.Mashup.HtmlTable.Resources.ResourceLoader.instance;
			}

			// Token: 0x1700006A RID: 106
			// (get) Token: 0x060000F7 RID: 247 RVA: 0x00002188 File Offset: 0x00000388
			public static CultureInfo Culture
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700006B RID: 107
			// (get) Token: 0x060000F8 RID: 248 RVA: 0x00006018 File Offset: 0x00004218
			public static ResourceManager Resources
			{
				get
				{
					return Microsoft.Mashup.HtmlTable.Resources.ResourceLoader.GetLoader().resources;
				}
			}

			// Token: 0x060000F9 RID: 249 RVA: 0x00006024 File Offset: 0x00004224
			public static Message0 GetMessage(string name)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.HtmlTable.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message0(null);
				}
				return new Message0(loader.resources.GetString(name, Microsoft.Mashup.HtmlTable.Resources.ResourceLoader.Culture));
			}

			// Token: 0x060000FA RID: 250 RVA: 0x00006058 File Offset: 0x00004258
			public static Message1 GetMessage(string name, object arg0)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.HtmlTable.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message1(null, null);
				}
				return new Message1(loader.resources.GetString(name, Microsoft.Mashup.HtmlTable.Resources.ResourceLoader.Culture), arg0);
			}

			// Token: 0x060000FB RID: 251 RVA: 0x00006090 File Offset: 0x00004290
			public static Message2 GetMessage(string name, object arg0, object arg1)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.HtmlTable.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message2(null, null, null);
				}
				return new Message2(loader.resources.GetString(name, Microsoft.Mashup.HtmlTable.Resources.ResourceLoader.Culture), arg0, arg1);
			}

			// Token: 0x060000FC RID: 252 RVA: 0x000060C8 File Offset: 0x000042C8
			public static Message3 GetMessage(string name, object arg0, object arg1, object arg2)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.HtmlTable.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message3(null, null, null, null);
				}
				return new Message3(loader.resources.GetString(name, Microsoft.Mashup.HtmlTable.Resources.ResourceLoader.Culture), arg0, arg1, arg2);
			}

			// Token: 0x060000FD RID: 253 RVA: 0x00006104 File Offset: 0x00004304
			public static Message4 GetMessage(string name, params object[] args)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.HtmlTable.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message4(null, null);
				}
				return new Message4(loader.resources.GetString(name, Microsoft.Mashup.HtmlTable.Resources.ResourceLoader.Culture), args);
			}

			// Token: 0x060000FE RID: 254 RVA: 0x0000613C File Offset: 0x0000433C
			public static string GetString(string name, params object[] args)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.HtmlTable.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				string @string = loader.resources.GetString(name, Microsoft.Mashup.HtmlTable.Resources.ResourceLoader.Culture);
				if (args != null && args.Length != 0)
				{
					return string.Format(CultureInfo.CurrentCulture, @string, args);
				}
				return @string;
			}

			// Token: 0x060000FF RID: 255 RVA: 0x0000617C File Offset: 0x0000437C
			public static string GetString(string name)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.HtmlTable.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.resources.GetString(name, Microsoft.Mashup.HtmlTable.Resources.ResourceLoader.Culture);
			}

			// Token: 0x06000100 RID: 256 RVA: 0x000061A8 File Offset: 0x000043A8
			public static object GetObject(string name)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.HtmlTable.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.resources.GetObject(name, Microsoft.Mashup.HtmlTable.Resources.ResourceLoader.Culture);
			}

			// Token: 0x06000101 RID: 257 RVA: 0x000061D4 File Offset: 0x000043D4
			public static T GetObject<T>(string name) where T : class
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.HtmlTable.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return default(T);
				}
				return (T)((object)loader.resources.GetObject(name, Microsoft.Mashup.HtmlTable.Resources.ResourceLoader.Culture));
			}

			// Token: 0x040000B2 RID: 178
			private static Resources.ResourceLoader instance;

			// Token: 0x040000B3 RID: 179
			private ResourceManager resources;
		}
	}
}
