using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.WebBrowserContents
{
	// Token: 0x02002040 RID: 8256
	internal class Resources
	{
		// Token: 0x17002DD4 RID: 11732
		// (get) Token: 0x0601131A RID: 70426 RVA: 0x003B35AF File Offset: 0x003B17AF
		public static ResourceManager ResourceManager
		{
			get
			{
				return Resources.ResourceLoader.Resources;
			}
		}

		// Token: 0x17002DD5 RID: 11733
		// (get) Token: 0x0601131B RID: 70427 RVA: 0x003B35B6 File Offset: 0x003B17B6
		public static Message0 WebBrowserContentsChallengeTitle
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("WebBrowserContentsChallengeTitle");
			}
		}

		// Token: 0x17002DD6 RID: 11734
		// (get) Token: 0x0601131C RID: 70428 RVA: 0x003B35C2 File Offset: 0x003B17C2
		public static Message0 Web_BrowserContents_Category
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Web_BrowserContents_Category");
			}
		}

		// Token: 0x17002DD7 RID: 11735
		// (get) Token: 0x0601131D RID: 70429 RVA: 0x003B35CE File Offset: 0x003B17CE
		public static Message0 Web_BrowserContents
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Web_BrowserContents");
			}
		}

		// Token: 0x0601131E RID: 70430 RVA: 0x003B35DA File Offset: 0x003B17DA
		public static Message2 Web_BrowserContents_Description(object p0, object p1)
		{
			return Resources.ResourceLoader.GetMessage("Web_BrowserContents_Description", p0, p1);
		}

		// Token: 0x17002DD8 RID: 11736
		// (get) Token: 0x0601131F RID: 70431 RVA: 0x003B35E8 File Offset: 0x003B17E8
		public static Message0 Web_BrowserContents_Example1
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Web_BrowserContents_Example1");
			}
		}

		// Token: 0x17002DD9 RID: 11737
		// (get) Token: 0x06011320 RID: 70432 RVA: 0x003B35F4 File Offset: 0x003B17F4
		public static Message0 Web_BrowserContents_Example1_Code
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Web_BrowserContents_Example1_Code");
			}
		}

		// Token: 0x17002DDA RID: 11738
		// (get) Token: 0x06011321 RID: 70433 RVA: 0x003B3600 File Offset: 0x003B1800
		public static Message0 Web_BrowserContents_Example1_Result
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Web_BrowserContents_Example1_Result");
			}
		}

		// Token: 0x17002DDB RID: 11739
		// (get) Token: 0x06011322 RID: 70434 RVA: 0x003B360C File Offset: 0x003B180C
		public static Message0 Web_BrowserContents_Example2
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Web_BrowserContents_Example2");
			}
		}

		// Token: 0x17002DDC RID: 11740
		// (get) Token: 0x06011323 RID: 70435 RVA: 0x003B3618 File Offset: 0x003B1818
		public static Message0 Web_BrowserContents_Example2_Code
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Web_BrowserContents_Example2_Code");
			}
		}

		// Token: 0x17002DDD RID: 11741
		// (get) Token: 0x06011324 RID: 70436 RVA: 0x003B3624 File Offset: 0x003B1824
		public static Message0 Web_BrowserContents_Example2_Result
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Web_BrowserContents_Example2_Result");
			}
		}

		// Token: 0x17002DDE RID: 11742
		// (get) Token: 0x06011325 RID: 70437 RVA: 0x003B3630 File Offset: 0x003B1830
		public static Message0 Web_BrowserContents_Example3
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Web_BrowserContents_Example3");
			}
		}

		// Token: 0x17002DDF RID: 11743
		// (get) Token: 0x06011326 RID: 70438 RVA: 0x003B363C File Offset: 0x003B183C
		public static Message0 Web_BrowserContents_Example3_Code
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Web_BrowserContents_Example3_Code");
			}
		}

		// Token: 0x17002DE0 RID: 11744
		// (get) Token: 0x06011327 RID: 70439 RVA: 0x003B3648 File Offset: 0x003B1848
		public static Message0 Web_BrowserContents_Example3_Result
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Web_BrowserContents_Example3_Result");
			}
		}

		// Token: 0x17002DE1 RID: 11745
		// (get) Token: 0x06011328 RID: 70440 RVA: 0x003B3654 File Offset: 0x003B1854
		public static Message0 Web_BrowserContents_Example4
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Web_BrowserContents_Example4");
			}
		}

		// Token: 0x17002DE2 RID: 11746
		// (get) Token: 0x06011329 RID: 70441 RVA: 0x003B3660 File Offset: 0x003B1860
		public static Message0 Web_BrowserContents_Example4_Code
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Web_BrowserContents_Example4_Code");
			}
		}

		// Token: 0x17002DE3 RID: 11747
		// (get) Token: 0x0601132A RID: 70442 RVA: 0x003B366C File Offset: 0x003B186C
		public static Message0 Web_BrowserContents_Example4_Result
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Web_BrowserContents_Example4_Result");
			}
		}

		// Token: 0x17002DE4 RID: 11748
		// (get) Token: 0x0601132B RID: 70443 RVA: 0x003B3678 File Offset: 0x003B1878
		public static Message0 InitializationError
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("InitializationError");
			}
		}

		// Token: 0x17002DE5 RID: 11749
		// (get) Token: 0x0601132C RID: 70444 RVA: 0x003B3684 File Offset: 0x003B1884
		public static Message0 ProcessFailed
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("ProcessFailed");
			}
		}

		// Token: 0x17002DE6 RID: 11750
		// (get) Token: 0x0601132D RID: 70445 RVA: 0x003B3690 File Offset: 0x003B1890
		public static Message0 UnableToRetrieveTheContentsOfTheWebPage
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("UnableToRetrieveTheContentsOfTheWebPage");
			}
		}

		// Token: 0x17002DE7 RID: 11751
		// (get) Token: 0x0601132E RID: 70446 RVA: 0x003B369C File Offset: 0x003B189C
		public static Message0 InvalidURL
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("InvalidURL");
			}
		}

		// Token: 0x17002DE8 RID: 11752
		// (get) Token: 0x0601132F RID: 70447 RVA: 0x003B36A8 File Offset: 0x003B18A8
		public static Message0 InvalidSelector
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("InvalidSelector");
			}
		}

		// Token: 0x17002DE9 RID: 11753
		// (get) Token: 0x06011330 RID: 70448 RVA: 0x003B36B4 File Offset: 0x003B18B4
		public static Message0 TimedOutWaitingForInitialization
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("TimedOutWaitingForInitialization");
			}
		}

		// Token: 0x17002DEA RID: 11754
		// (get) Token: 0x06011331 RID: 70449 RVA: 0x003B36C0 File Offset: 0x003B18C0
		public static Message0 TimedOutWaitingForPageLoad
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("TimedOutWaitingForPageLoad");
			}
		}

		// Token: 0x17002DEB RID: 11755
		// (get) Token: 0x06011332 RID: 70450 RVA: 0x003B36CC File Offset: 0x003B18CC
		public static Message0 TimedOutWaitingForJavascript
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("TimedOutWaitingForJavascript");
			}
		}

		// Token: 0x17002DEC RID: 11756
		// (get) Token: 0x06011333 RID: 70451 RVA: 0x003B36D8 File Offset: 0x003B18D8
		public static Message0 TimedOutWaitingForSelector
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("TimedOutWaitingForSelector");
			}
		}

		// Token: 0x17002DED RID: 11757
		// (get) Token: 0x06011334 RID: 70452 RVA: 0x003B36E4 File Offset: 0x003B18E4
		public static Message0 UnableToUseSelectorOnPage
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("UnableToUseSelectorOnPage");
			}
		}

		// Token: 0x06011335 RID: 70453 RVA: 0x003B36F0 File Offset: 0x003B18F0
		public static Message1 OnlyAnonymousAuthenticationSupported(object p0)
		{
			return Resources.ResourceLoader.GetMessage("OnlyAnonymousAuthenticationSupported", p0);
		}

		// Token: 0x06011336 RID: 70454 RVA: 0x003B36FD File Offset: 0x003B18FD
		public static Message1 OnlyAnonymousWindowsWebApiBasicAuthenticationSupported(object p0)
		{
			return Resources.ResourceLoader.GetMessage("OnlyAnonymousWindowsWebApiBasicAuthenticationSupported", p0);
		}

		// Token: 0x06011337 RID: 70455 RVA: 0x003B370A File Offset: 0x003B190A
		public static Message3 ExitCodeOutputAndError(object p0, object p1, object p2)
		{
			return Resources.ResourceLoader.GetMessage("ExitCodeOutputAndError", p0, p1, p2);
		}

		// Token: 0x17002DEE RID: 11758
		// (get) Token: 0x06011338 RID: 70456 RVA: 0x003B3719 File Offset: 0x003B1919
		public static Message0 RuntimeMissing
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("RuntimeMissing");
			}
		}

		// Token: 0x06011339 RID: 70457 RVA: 0x003B3725 File Offset: 0x003B1925
		public static Message1 TooManyRedirects(object p0)
		{
			return Resources.ResourceLoader.GetMessage("TooManyRedirects", p0);
		}

		// Token: 0x02002041 RID: 8257
		private class ResourceLoader
		{
			// Token: 0x0601133B RID: 70459 RVA: 0x003B3732 File Offset: 0x003B1932
			internal ResourceLoader()
			{
				this.resources = new ResourceManager("Microsoft.Mashup.WebBrowserContents.Resources", base.GetType().Assembly);
			}

			// Token: 0x0601133C RID: 70460 RVA: 0x003B3758 File Offset: 0x003B1958
			private static Resources.ResourceLoader GetLoader()
			{
				if (Microsoft.Mashup.WebBrowserContents.Resources.ResourceLoader.instance == null)
				{
					Resources.ResourceLoader resourceLoader = new Resources.ResourceLoader();
					Interlocked.CompareExchange<Resources.ResourceLoader>(ref Microsoft.Mashup.WebBrowserContents.Resources.ResourceLoader.instance, resourceLoader, null);
				}
				return Microsoft.Mashup.WebBrowserContents.Resources.ResourceLoader.instance;
			}

			// Token: 0x17002DEF RID: 11759
			// (get) Token: 0x0601133D RID: 70461 RVA: 0x00002188 File Offset: 0x00000388
			public static CultureInfo Culture
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17002DF0 RID: 11760
			// (get) Token: 0x0601133E RID: 70462 RVA: 0x003B3784 File Offset: 0x003B1984
			public static ResourceManager Resources
			{
				get
				{
					return Microsoft.Mashup.WebBrowserContents.Resources.ResourceLoader.GetLoader().resources;
				}
			}

			// Token: 0x0601133F RID: 70463 RVA: 0x003B3790 File Offset: 0x003B1990
			public static Message0 GetMessage(string name)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.WebBrowserContents.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message0(null);
				}
				return new Message0(loader.resources.GetString(name, Microsoft.Mashup.WebBrowserContents.Resources.ResourceLoader.Culture));
			}

			// Token: 0x06011340 RID: 70464 RVA: 0x003B37C4 File Offset: 0x003B19C4
			public static Message1 GetMessage(string name, object arg0)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.WebBrowserContents.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message1(null, null);
				}
				return new Message1(loader.resources.GetString(name, Microsoft.Mashup.WebBrowserContents.Resources.ResourceLoader.Culture), arg0);
			}

			// Token: 0x06011341 RID: 70465 RVA: 0x003B37FC File Offset: 0x003B19FC
			public static Message2 GetMessage(string name, object arg0, object arg1)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.WebBrowserContents.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message2(null, null, null);
				}
				return new Message2(loader.resources.GetString(name, Microsoft.Mashup.WebBrowserContents.Resources.ResourceLoader.Culture), arg0, arg1);
			}

			// Token: 0x06011342 RID: 70466 RVA: 0x003B3834 File Offset: 0x003B1A34
			public static Message3 GetMessage(string name, object arg0, object arg1, object arg2)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.WebBrowserContents.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message3(null, null, null, null);
				}
				return new Message3(loader.resources.GetString(name, Microsoft.Mashup.WebBrowserContents.Resources.ResourceLoader.Culture), arg0, arg1, arg2);
			}

			// Token: 0x06011343 RID: 70467 RVA: 0x003B3870 File Offset: 0x003B1A70
			public static Message4 GetMessage(string name, params object[] args)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.WebBrowserContents.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message4(null, null);
				}
				return new Message4(loader.resources.GetString(name, Microsoft.Mashup.WebBrowserContents.Resources.ResourceLoader.Culture), args);
			}

			// Token: 0x06011344 RID: 70468 RVA: 0x003B38A8 File Offset: 0x003B1AA8
			public static string GetString(string name, params object[] args)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.WebBrowserContents.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				string @string = loader.resources.GetString(name, Microsoft.Mashup.WebBrowserContents.Resources.ResourceLoader.Culture);
				if (args != null && args.Length != 0)
				{
					return string.Format(CultureInfo.CurrentCulture, @string, args);
				}
				return @string;
			}

			// Token: 0x06011345 RID: 70469 RVA: 0x003B38E8 File Offset: 0x003B1AE8
			public static string GetString(string name)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.WebBrowserContents.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.resources.GetString(name, Microsoft.Mashup.WebBrowserContents.Resources.ResourceLoader.Culture);
			}

			// Token: 0x06011346 RID: 70470 RVA: 0x003B3914 File Offset: 0x003B1B14
			public static object GetObject(string name)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.WebBrowserContents.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.resources.GetObject(name, Microsoft.Mashup.WebBrowserContents.Resources.ResourceLoader.Culture);
			}

			// Token: 0x06011347 RID: 70471 RVA: 0x003B3940 File Offset: 0x003B1B40
			public static T GetObject<T>(string name) where T : class
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.WebBrowserContents.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return default(T);
				}
				return (T)((object)loader.resources.GetObject(name, Microsoft.Mashup.WebBrowserContents.Resources.ResourceLoader.Culture));
			}

			// Token: 0x04006851 RID: 26705
			private static Resources.ResourceLoader instance;

			// Token: 0x04006852 RID: 26706
			private ResourceManager resources;
		}
	}
}
