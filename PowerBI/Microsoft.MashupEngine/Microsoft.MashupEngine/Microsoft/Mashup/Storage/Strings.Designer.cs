using System;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x0200209B RID: 8347
	internal class Strings
	{
		// Token: 0x17003126 RID: 12582
		// (get) Token: 0x0600CC55 RID: 52309 RVA: 0x0028A7E3 File Offset: 0x002889E3
		public static ResourceManager ResourceManager
		{
			get
			{
				return Strings.ResourceLoader.Resources;
			}
		}

		// Token: 0x17003127 RID: 12583
		// (get) Token: 0x0600CC56 RID: 52310 RVA: 0x0028A7EA File Offset: 0x002889EA
		public static string PackagePartStorage_UnableToAddPart
		{
			get
			{
				return Strings.ResourceLoader.GetString("PackagePartStorage_UnableToAddPart");
			}
		}

		// Token: 0x17003128 RID: 12584
		// (get) Token: 0x0600CC57 RID: 52311 RVA: 0x0028A7F6 File Offset: 0x002889F6
		public static string PackagePartStorage_UnableToRemovePart
		{
			get
			{
				return Strings.ResourceLoader.GetString("PackagePartStorage_UnableToRemovePart");
			}
		}

		// Token: 0x17003129 RID: 12585
		// (get) Token: 0x0600CC58 RID: 52312 RVA: 0x0028A802 File Offset: 0x00288A02
		public static string PackagePartStorage_UnableToAccessPart
		{
			get
			{
				return Strings.ResourceLoader.GetString("PackagePartStorage_UnableToAccessPart");
			}
		}

		// Token: 0x1700312A RID: 12586
		// (get) Token: 0x0600CC59 RID: 52313 RVA: 0x0028A80E File Offset: 0x00288A0E
		public static string PackagePartStorage_UnableToAccessPartNames
		{
			get
			{
				return Strings.ResourceLoader.GetString("PackagePartStorage_UnableToAccessPartNames");
			}
		}

		// Token: 0x1700312B RID: 12587
		// (get) Token: 0x0600CC5A RID: 52314 RVA: 0x0028A81A File Offset: 0x00288A1A
		public static string PackagePartStorage_UnableToAccessPartContentType
		{
			get
			{
				return Strings.ResourceLoader.GetString("PackagePartStorage_UnableToAccessPartContentType");
			}
		}

		// Token: 0x1700312C RID: 12588
		// (get) Token: 0x0600CC5B RID: 52315 RVA: 0x0028A826 File Offset: 0x00288A26
		public static string PackagePartStorage_UnableToAccessPartContents
		{
			get
			{
				return Strings.ResourceLoader.GetString("PackagePartStorage_UnableToAccessPartContents");
			}
		}

		// Token: 0x1700312D RID: 12589
		// (get) Token: 0x0600CC5C RID: 52316 RVA: 0x0028A832 File Offset: 0x00288A32
		public static string PackagePartStorage_UnableToSavePartContents
		{
			get
			{
				return Strings.ResourceLoader.GetString("PackagePartStorage_UnableToSavePartContents");
			}
		}

		// Token: 0x0600CC5D RID: 52317 RVA: 0x0028A83E File Offset: 0x00288A3E
		public static string PackagePartStorage_EmbeddedPartTooLarge(object p0)
		{
			return Strings.ResourceLoader.GetString("PackagePartStorage_EmbeddedPartTooLarge", new object[] { p0 });
		}

		// Token: 0x1700312E RID: 12590
		// (get) Token: 0x0600CC5E RID: 52318 RVA: 0x0028A854 File Offset: 0x00288A54
		public static string Package_Unable_To_Load
		{
			get
			{
				return Strings.ResourceLoader.GetString("Package_Unable_To_Load");
			}
		}

		// Token: 0x0600CC5F RID: 52319 RVA: 0x0028A860 File Offset: 0x00288A60
		public static string Package_Unable_To_Load_Too_Large(object p0)
		{
			return Strings.ResourceLoader.GetString("Package_Unable_To_Load_Too_Large", new object[] { p0 });
		}

		// Token: 0x1700312F RID: 12591
		// (get) Token: 0x0600CC60 RID: 52320 RVA: 0x0028A876 File Offset: 0x00288A76
		public static string Local_Storage_Unable_To_File_Storage
		{
			get
			{
				return Strings.ResourceLoader.GetString("Local_Storage_Unable_To_File_Storage");
			}
		}

		// Token: 0x17003130 RID: 12592
		// (get) Token: 0x0600CC61 RID: 52321 RVA: 0x0028A882 File Offset: 0x00288A82
		public static string Local_Storage_Unable_To_Remove_File
		{
			get
			{
				return Strings.ResourceLoader.GetString("Local_Storage_Unable_To_Remove_File");
			}
		}

		// Token: 0x17003131 RID: 12593
		// (get) Token: 0x0600CC62 RID: 52322 RVA: 0x0028A88E File Offset: 0x00288A8E
		public static string Local_Storage_Unable_To_Save_File
		{
			get
			{
				return Strings.ResourceLoader.GetString("Local_Storage_Unable_To_Save_File");
			}
		}

		// Token: 0x17003132 RID: 12594
		// (get) Token: 0x0600CC63 RID: 52323 RVA: 0x0028A89A File Offset: 0x00288A9A
		public static string Content_Storage_ContentMissing
		{
			get
			{
				return Strings.ResourceLoader.GetString("Content_Storage_ContentMissing");
			}
		}

		// Token: 0x17003133 RID: 12595
		// (get) Token: 0x0600CC64 RID: 52324 RVA: 0x0028A8A6 File Offset: 0x00288AA6
		public static string Package_Unable_To_Access
		{
			get
			{
				return Strings.ResourceLoader.GetString("Package_Unable_To_Access");
			}
		}

		// Token: 0x17003134 RID: 12596
		// (get) Token: 0x0600CC65 RID: 52325 RVA: 0x0028A8B2 File Offset: 0x00288AB2
		public static string Package_Unrecognized_File_Format
		{
			get
			{
				return Strings.ResourceLoader.GetString("Package_Unrecognized_File_Format");
			}
		}

		// Token: 0x17003135 RID: 12597
		// (get) Token: 0x0600CC66 RID: 52326 RVA: 0x0028A8BE File Offset: 0x00288ABE
		public static string User_Name_Not_Specified
		{
			get
			{
				return Strings.ResourceLoader.GetString("User_Name_Not_Specified");
			}
		}

		// Token: 0x17003136 RID: 12598
		// (get) Token: 0x0600CC67 RID: 52327 RVA: 0x0028A8CA File Offset: 0x00288ACA
		public static string IdentitySource_Not_Valid
		{
			get
			{
				return Strings.ResourceLoader.GetString("IdentitySource_Not_Valid");
			}
		}

		// Token: 0x17003137 RID: 12599
		// (get) Token: 0x0600CC68 RID: 52328 RVA: 0x0028A8D6 File Offset: 0x00288AD6
		public static string User_Name_Not_Valid
		{
			get
			{
				return Strings.ResourceLoader.GetString("User_Name_Not_Valid");
			}
		}

		// Token: 0x17003138 RID: 12600
		// (get) Token: 0x0600CC69 RID: 52329 RVA: 0x0028A8E2 File Offset: 0x00288AE2
		public static string EmailAddress_Not_Specified
		{
			get
			{
				return Strings.ResourceLoader.GetString("EmailAddress_Not_Specified");
			}
		}

		// Token: 0x17003139 RID: 12601
		// (get) Token: 0x0600CC6A RID: 52330 RVA: 0x0028A8EE File Offset: 0x00288AEE
		public static string EmailAddress_Not_Valid
		{
			get
			{
				return Strings.ResourceLoader.GetString("EmailAddress_Not_Valid");
			}
		}

		// Token: 0x1700313A RID: 12602
		// (get) Token: 0x0600CC6B RID: 52331 RVA: 0x0028A8FA File Offset: 0x00288AFA
		public static string API_Key_Not_Specified
		{
			get
			{
				return Strings.ResourceLoader.GetString("API_Key_Not_Specified");
			}
		}

		// Token: 0x1700313B RID: 12603
		// (get) Token: 0x0600CC6C RID: 52332 RVA: 0x0028A906 File Offset: 0x00288B06
		public static string Feed_Key_Not_Specified
		{
			get
			{
				return Strings.ResourceLoader.GetString("Feed_Key_Not_Specified");
			}
		}

		// Token: 0x1700313C RID: 12604
		// (get) Token: 0x0600CC6D RID: 52333 RVA: 0x0028A912 File Offset: 0x00288B12
		public static string User_Not_Signed_In
		{
			get
			{
				return Strings.ResourceLoader.GetString("User_Not_Signed_In");
			}
		}

		// Token: 0x1700313D RID: 12605
		// (get) Token: 0x0600CC6E RID: 52334 RVA: 0x0028A91E File Offset: 0x00288B1E
		public static string FirewallFlow_BadEmbedding
		{
			get
			{
				return Strings.ResourceLoader.GetString("FirewallFlow_BadEmbedding");
			}
		}

		// Token: 0x0600CC6F RID: 52335 RVA: 0x0028A92A File Offset: 0x00288B2A
		public static string MashupTooLarge(object p0)
		{
			return Strings.ResourceLoader.GetString("MashupTooLarge", new object[] { p0 });
		}

		// Token: 0x1700313E RID: 12606
		// (get) Token: 0x0600CC70 RID: 52336 RVA: 0x0028A940 File Offset: 0x00288B40
		public static string Thread_Not_Yet_Supported
		{
			get
			{
				return Strings.ResourceLoader.GetString("Thread_Not_Yet_Supported");
			}
		}

		// Token: 0x0200209C RID: 8348
		private class ResourceLoader
		{
			// Token: 0x0600CC72 RID: 52338 RVA: 0x0028A94C File Offset: 0x00288B4C
			internal ResourceLoader()
			{
				this.resources = new ResourceManager("Microsoft.Mashup.Storage.Strings", base.GetType().Assembly);
			}

			// Token: 0x0600CC73 RID: 52339 RVA: 0x0028A970 File Offset: 0x00288B70
			private static Strings.ResourceLoader GetLoader()
			{
				if (Strings.ResourceLoader.instance == null)
				{
					Strings.ResourceLoader resourceLoader = new Strings.ResourceLoader();
					Interlocked.CompareExchange<Strings.ResourceLoader>(ref Strings.ResourceLoader.instance, resourceLoader, null);
				}
				return Strings.ResourceLoader.instance;
			}

			// Token: 0x1700313F RID: 12607
			// (get) Token: 0x0600CC74 RID: 52340 RVA: 0x000020FA File Offset: 0x000002FA
			public static CultureInfo Culture
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17003140 RID: 12608
			// (get) Token: 0x0600CC75 RID: 52341 RVA: 0x0028A99C File Offset: 0x00288B9C
			public static ResourceManager Resources
			{
				get
				{
					return Strings.ResourceLoader.GetLoader().resources;
				}
			}

			// Token: 0x0600CC76 RID: 52342 RVA: 0x0028A9A8 File Offset: 0x00288BA8
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

			// Token: 0x0600CC77 RID: 52343 RVA: 0x0028A9E8 File Offset: 0x00288BE8
			public static string GetString(string name)
			{
				Strings.ResourceLoader loader = Strings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.resources.GetString(name, Strings.ResourceLoader.Culture);
			}

			// Token: 0x0600CC78 RID: 52344 RVA: 0x0028AA14 File Offset: 0x00288C14
			public static object GetObject(string name)
			{
				Strings.ResourceLoader loader = Strings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.resources.GetObject(name, Strings.ResourceLoader.Culture);
			}

			// Token: 0x0600CC79 RID: 52345 RVA: 0x0028AA40 File Offset: 0x00288C40
			public static T GetObject<T>(string name) where T : class
			{
				Strings.ResourceLoader loader = Strings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return default(T);
				}
				return (T)((object)loader.resources.GetObject(name, Strings.ResourceLoader.Culture));
			}

			// Token: 0x04006790 RID: 26512
			private static Strings.ResourceLoader instance;

			// Token: 0x04006791 RID: 26513
			private ResourceManager resources;
		}
	}
}
