using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Pdf
{
	// Token: 0x02002033 RID: 8243
	internal class Resources
	{
		// Token: 0x17002DAF RID: 11695
		// (get) Token: 0x060112C7 RID: 70343 RVA: 0x003B24BB File Offset: 0x003B06BB
		public static ResourceManager ResourceManager
		{
			get
			{
				return Resources.ResourceLoader.Resources;
			}
		}

		// Token: 0x17002DB0 RID: 11696
		// (get) Token: 0x060112C8 RID: 70344 RVA: 0x003B24C2 File Offset: 0x003B06C2
		public static Message0 Pdf_Tables_Category
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Pdf_Tables_Category");
			}
		}

		// Token: 0x17002DB1 RID: 11697
		// (get) Token: 0x060112C9 RID: 70345 RVA: 0x003B24CE File Offset: 0x003B06CE
		public static Message0 Pdf_Tables
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Pdf_Tables");
			}
		}

		// Token: 0x060112CA RID: 70346 RVA: 0x003B24DA File Offset: 0x003B06DA
		public static Message2 Pdf_Tables_Description(object p0, object p1)
		{
			return Resources.ResourceLoader.GetMessage("Pdf_Tables_Description", p0, p1);
		}

		// Token: 0x17002DB2 RID: 11698
		// (get) Token: 0x060112CB RID: 70347 RVA: 0x003B24E8 File Offset: 0x003B06E8
		public static Message0 Option_Implementation_Caption
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Option_Implementation_Caption");
			}
		}

		// Token: 0x17002DB3 RID: 11699
		// (get) Token: 0x060112CC RID: 70348 RVA: 0x003B24F4 File Offset: 0x003B06F4
		public static Message0 Option_Implementation_Description
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Option_Implementation_Description");
			}
		}

		// Token: 0x17002DB4 RID: 11700
		// (get) Token: 0x060112CD RID: 70349 RVA: 0x003B2500 File Offset: 0x003B0700
		public static Message0 Option_StartPage_Description
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Option_StartPage_Description");
			}
		}

		// Token: 0x17002DB5 RID: 11701
		// (get) Token: 0x060112CE RID: 70350 RVA: 0x003B250C File Offset: 0x003B070C
		public static Message0 Option_StartPage_Caption
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Option_StartPage_Caption");
			}
		}

		// Token: 0x17002DB6 RID: 11702
		// (get) Token: 0x060112CF RID: 70351 RVA: 0x003B2518 File Offset: 0x003B0718
		public static Message0 Option_EndPage_Description
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Option_EndPage_Description");
			}
		}

		// Token: 0x17002DB7 RID: 11703
		// (get) Token: 0x060112D0 RID: 70352 RVA: 0x003B2524 File Offset: 0x003B0724
		public static Message0 Option_EndPage_Caption
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Option_EndPage_Caption");
			}
		}

		// Token: 0x17002DB8 RID: 11704
		// (get) Token: 0x060112D1 RID: 70353 RVA: 0x003B2530 File Offset: 0x003B0730
		public static Message0 Option_MultiPageTables_Description
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Option_MultiPageTables_Description");
			}
		}

		// Token: 0x17002DB9 RID: 11705
		// (get) Token: 0x060112D2 RID: 70354 RVA: 0x003B253C File Offset: 0x003B073C
		public static Message0 Option_MultiPageTables_Caption
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Option_MultiPageTables_Caption");
			}
		}

		// Token: 0x17002DBA RID: 11706
		// (get) Token: 0x060112D3 RID: 70355 RVA: 0x003B2548 File Offset: 0x003B0748
		public static Message0 Option_EnforceBorderLines_Description
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Option_EnforceBorderLines_Description");
			}
		}

		// Token: 0x17002DBB RID: 11707
		// (get) Token: 0x060112D4 RID: 70356 RVA: 0x003B2554 File Offset: 0x003B0754
		public static Message0 Option_EnforceBorderLines_Caption
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Option_EnforceBorderLines_Caption");
			}
		}

		// Token: 0x17002DBC RID: 11708
		// (get) Token: 0x060112D5 RID: 70357 RVA: 0x003B2560 File Offset: 0x003B0760
		public static Message0 Pdf_Tables_Example1
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Pdf_Tables_Example1");
			}
		}

		// Token: 0x17002DBD RID: 11709
		// (get) Token: 0x060112D6 RID: 70358 RVA: 0x003B256C File Offset: 0x003B076C
		public static Message0 Pdf_Tables_Example1_Code
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Pdf_Tables_Example1_Code");
			}
		}

		// Token: 0x17002DBE RID: 11710
		// (get) Token: 0x060112D7 RID: 70359 RVA: 0x003B2578 File Offset: 0x003B0778
		public static Message0 Pdf_Tables_Example1_Result
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Pdf_Tables_Example1_Result");
			}
		}

		// Token: 0x17002DBF RID: 11711
		// (get) Token: 0x060112D8 RID: 70360 RVA: 0x003B2584 File Offset: 0x003B0784
		public static Message0 InvalidPageRange
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("InvalidPageRange");
			}
		}

		// Token: 0x17002DC0 RID: 11712
		// (get) Token: 0x060112D9 RID: 70361 RVA: 0x003B2590 File Offset: 0x003B0790
		public static Message0 Pdf_Tables_ButtonHelp
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Pdf_Tables_ButtonHelp");
			}
		}

		// Token: 0x17002DC1 RID: 11713
		// (get) Token: 0x060112DA RID: 70362 RVA: 0x003B259C File Offset: 0x003B079C
		public static Message0 Pdf_Tables_ButtonTitle
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Pdf_Tables_ButtonTitle");
			}
		}

		// Token: 0x02002034 RID: 8244
		private class ResourceLoader
		{
			// Token: 0x060112DC RID: 70364 RVA: 0x003B25A8 File Offset: 0x003B07A8
			internal ResourceLoader()
			{
				this.resources = new ResourceManager("Microsoft.Mashup.Pdf.Resources", base.GetType().Assembly);
			}

			// Token: 0x060112DD RID: 70365 RVA: 0x003B25CC File Offset: 0x003B07CC
			private static Resources.ResourceLoader GetLoader()
			{
				if (Microsoft.Mashup.Pdf.Resources.ResourceLoader.instance == null)
				{
					Resources.ResourceLoader resourceLoader = new Resources.ResourceLoader();
					Interlocked.CompareExchange<Resources.ResourceLoader>(ref Microsoft.Mashup.Pdf.Resources.ResourceLoader.instance, resourceLoader, null);
				}
				return Microsoft.Mashup.Pdf.Resources.ResourceLoader.instance;
			}

			// Token: 0x17002DC2 RID: 11714
			// (get) Token: 0x060112DE RID: 70366 RVA: 0x00002188 File Offset: 0x00000388
			public static CultureInfo Culture
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17002DC3 RID: 11715
			// (get) Token: 0x060112DF RID: 70367 RVA: 0x003B25F8 File Offset: 0x003B07F8
			public static ResourceManager Resources
			{
				get
				{
					return Microsoft.Mashup.Pdf.Resources.ResourceLoader.GetLoader().resources;
				}
			}

			// Token: 0x060112E0 RID: 70368 RVA: 0x003B2604 File Offset: 0x003B0804
			public static Message0 GetMessage(string name)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.Pdf.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message0(null);
				}
				return new Message0(loader.resources.GetString(name, Microsoft.Mashup.Pdf.Resources.ResourceLoader.Culture));
			}

			// Token: 0x060112E1 RID: 70369 RVA: 0x003B2638 File Offset: 0x003B0838
			public static Message1 GetMessage(string name, object arg0)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.Pdf.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message1(null, null);
				}
				return new Message1(loader.resources.GetString(name, Microsoft.Mashup.Pdf.Resources.ResourceLoader.Culture), arg0);
			}

			// Token: 0x060112E2 RID: 70370 RVA: 0x003B2670 File Offset: 0x003B0870
			public static Message2 GetMessage(string name, object arg0, object arg1)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.Pdf.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message2(null, null, null);
				}
				return new Message2(loader.resources.GetString(name, Microsoft.Mashup.Pdf.Resources.ResourceLoader.Culture), arg0, arg1);
			}

			// Token: 0x060112E3 RID: 70371 RVA: 0x003B26A8 File Offset: 0x003B08A8
			public static Message3 GetMessage(string name, object arg0, object arg1, object arg2)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.Pdf.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message3(null, null, null, null);
				}
				return new Message3(loader.resources.GetString(name, Microsoft.Mashup.Pdf.Resources.ResourceLoader.Culture), arg0, arg1, arg2);
			}

			// Token: 0x060112E4 RID: 70372 RVA: 0x003B26E4 File Offset: 0x003B08E4
			public static Message4 GetMessage(string name, params object[] args)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.Pdf.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message4(null, null);
				}
				return new Message4(loader.resources.GetString(name, Microsoft.Mashup.Pdf.Resources.ResourceLoader.Culture), args);
			}

			// Token: 0x060112E5 RID: 70373 RVA: 0x003B271C File Offset: 0x003B091C
			public static string GetString(string name, params object[] args)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.Pdf.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				string @string = loader.resources.GetString(name, Microsoft.Mashup.Pdf.Resources.ResourceLoader.Culture);
				if (args != null && args.Length != 0)
				{
					return string.Format(CultureInfo.CurrentCulture, @string, args);
				}
				return @string;
			}

			// Token: 0x060112E6 RID: 70374 RVA: 0x003B275C File Offset: 0x003B095C
			public static string GetString(string name)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.Pdf.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.resources.GetString(name, Microsoft.Mashup.Pdf.Resources.ResourceLoader.Culture);
			}

			// Token: 0x060112E7 RID: 70375 RVA: 0x003B2788 File Offset: 0x003B0988
			public static object GetObject(string name)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.Pdf.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.resources.GetObject(name, Microsoft.Mashup.Pdf.Resources.ResourceLoader.Culture);
			}

			// Token: 0x060112E8 RID: 70376 RVA: 0x003B27B4 File Offset: 0x003B09B4
			public static T GetObject<T>(string name) where T : class
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.Pdf.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return default(T);
				}
				return (T)((object)loader.resources.GetObject(name, Microsoft.Mashup.Pdf.Resources.ResourceLoader.Culture));
			}

			// Token: 0x0400682D RID: 26669
			private static Resources.ResourceLoader instance;

			// Token: 0x0400682E RID: 26670
			private ResourceManager resources;
		}
	}
}
