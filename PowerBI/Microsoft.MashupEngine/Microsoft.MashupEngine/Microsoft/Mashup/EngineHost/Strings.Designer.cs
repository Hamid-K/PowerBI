using System;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x02001988 RID: 6536
	internal class Strings
	{
		// Token: 0x17002A52 RID: 10834
		// (get) Token: 0x0600A5BD RID: 42429 RVA: 0x00224AD4 File Offset: 0x00222CD4
		public static ResourceManager ResourceManager
		{
			get
			{
				return Strings.ResourceLoader.Resources;
			}
		}

		// Token: 0x17002A53 RID: 10835
		// (get) Token: 0x0600A5BE RID: 42430 RVA: 0x00224ADB File Offset: 0x00222CDB
		public static string Cache_EntryTooLarge
		{
			get
			{
				return Strings.ResourceLoader.GetString("Cache_EntryTooLarge");
			}
		}

		// Token: 0x17002A54 RID: 10836
		// (get) Token: 0x0600A5BF RID: 42431 RVA: 0x00224AE7 File Offset: 0x00222CE7
		public static string ClientApplicationRequired
		{
			get
			{
				return Strings.ResourceLoader.GetString("ClientApplicationRequired");
			}
		}

		// Token: 0x0600A5C0 RID: 42432 RVA: 0x00224AF3 File Offset: 0x00222CF3
		public static string Firewall_Buffering_Failed(object p0, object p1)
		{
			return Strings.ResourceLoader.GetString("Firewall_Buffering_Failed", new object[] { p0, p1 });
		}

		// Token: 0x17002A55 RID: 10837
		// (get) Token: 0x0600A5C1 RID: 42433 RVA: 0x00224B0D File Offset: 0x00222D0D
		public static string Firewall_CouldNotSerializeException
		{
			get
			{
				return Strings.ResourceLoader.GetString("Firewall_CouldNotSerializeException");
			}
		}

		// Token: 0x17002A56 RID: 10838
		// (get) Token: 0x0600A5C2 RID: 42434 RVA: 0x00224B19 File Offset: 0x00222D19
		public static string OutOfTempStorage
		{
			get
			{
				return Strings.ResourceLoader.GetString("OutOfTempStorage");
			}
		}

		// Token: 0x0600A5C3 RID: 42435 RVA: 0x00224B25 File Offset: 0x00222D25
		public static string Section_Exists(object p0)
		{
			return Strings.ResourceLoader.GetString("Section_Exists", new object[] { p0 });
		}

		// Token: 0x0600A5C4 RID: 42436 RVA: 0x00224B3B File Offset: 0x00222D3B
		public static string Section_In_Error(object p0)
		{
			return Strings.ResourceLoader.GetString("Section_In_Error", new object[] { p0 });
		}

		// Token: 0x0600A5C5 RID: 42437 RVA: 0x00224B51 File Offset: 0x00222D51
		public static string Section_Name_In_Section_Not_Recognized(object p0, object p1)
		{
			return Strings.ResourceLoader.GetString("Section_Name_In_Section_Not_Recognized", new object[] { p0, p1 });
		}

		// Token: 0x0600A5C6 RID: 42438 RVA: 0x00224B6B File Offset: 0x00222D6B
		public static string Section_Name_Not_Recognized(object p0)
		{
			return Strings.ResourceLoader.GetString("Section_Name_Not_Recognized", new object[] { p0 });
		}

		// Token: 0x0600A5C7 RID: 42439 RVA: 0x00224B81 File Offset: 0x00222D81
		public static string Section_Not_Recognized(object p0)
		{
			return Strings.ResourceLoader.GetString("Section_Not_Recognized", new object[] { p0 });
		}

		// Token: 0x0600A5C8 RID: 42440 RVA: 0x00224B97 File Offset: 0x00222D97
		public static string ModuleMultipleSourceFiles(object p0, object p1)
		{
			return Strings.ResourceLoader.GetString("ModuleMultipleSourceFiles", new object[] { p0, p1 });
		}

		// Token: 0x17002A57 RID: 10839
		// (get) Token: 0x0600A5C9 RID: 42441 RVA: 0x00224BB1 File Offset: 0x00222DB1
		public static string ModuleNoSourceFile
		{
			get
			{
				return Strings.ResourceLoader.GetString("ModuleNoSourceFile");
			}
		}

		// Token: 0x0600A5CA RID: 42442 RVA: 0x00224BBD File Offset: 0x00222DBD
		public static string LibraryValidationFailed(object p0)
		{
			return Strings.ResourceLoader.GetString("LibraryValidationFailed", new object[] { p0 });
		}

		// Token: 0x0600A5CB RID: 42443 RVA: 0x00224BD3 File Offset: 0x00222DD3
		public static string CannotCreateDirectory(object p0, object p1, object p2)
		{
			return Strings.ResourceLoader.GetString("CannotCreateDirectory", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600A5CC RID: 42444 RVA: 0x00224BF1 File Offset: 0x00222DF1
		public static string Cache_Certificate_Not_Found(object p0)
		{
			return Strings.ResourceLoader.GetString("Cache_Certificate_Not_Found", new object[] { p0 });
		}

		// Token: 0x0600A5CD RID: 42445 RVA: 0x00224C07 File Offset: 0x00222E07
		public static string Cache_Certificate_Additional_Cert(object p0)
		{
			return Strings.ResourceLoader.GetString("Cache_Certificate_Additional_Cert", new object[] { p0 });
		}

		// Token: 0x0600A5CE RID: 42446 RVA: 0x00224C1D File Offset: 0x00222E1D
		public static string Cache_Certificate_Cryptographic_Exception(object p0, object p1)
		{
			return Strings.ResourceLoader.GetString("Cache_Certificate_Cryptographic_Exception", new object[] { p0, p1 });
		}

		// Token: 0x0600A5CF RID: 42447 RVA: 0x00224C37 File Offset: 0x00222E37
		public static string Cache_Certificate_NotRSA(object p0)
		{
			return Strings.ResourceLoader.GetString("Cache_Certificate_NotRSA", new object[] { p0 });
		}

		// Token: 0x0600A5D0 RID: 42448 RVA: 0x00224C4D File Offset: 0x00222E4D
		public static string Cache_Certificate_No_PrivateKey(object p0)
		{
			return Strings.ResourceLoader.GetString("Cache_Certificate_No_PrivateKey", new object[] { p0 });
		}

		// Token: 0x0600A5D1 RID: 42449 RVA: 0x00224C63 File Offset: 0x00222E63
		public static string Cache_Certificate_Verification_Failed(object p0)
		{
			return Strings.ResourceLoader.GetString("Cache_Certificate_Verification_Failed", new object[] { p0 });
		}

		// Token: 0x17002A58 RID: 10840
		// (get) Token: 0x0600A5D2 RID: 42450 RVA: 0x00224C79 File Offset: 0x00222E79
		public static string ConnectionGovernance_DeadlockAborted
		{
			get
			{
				return Strings.ResourceLoader.GetString("ConnectionGovernance_DeadlockAborted");
			}
		}

		// Token: 0x17002A59 RID: 10841
		// (get) Token: 0x0600A5D3 RID: 42451 RVA: 0x00224C85 File Offset: 0x00222E85
		public static string NoWindowsAuth
		{
			get
			{
				return Strings.ResourceLoader.GetString("NoWindowsAuth");
			}
		}

		// Token: 0x17002A5A RID: 10842
		// (get) Token: 0x0600A5D4 RID: 42452 RVA: 0x00224C91 File Offset: 0x00222E91
		public static string ShadowedLibrary
		{
			get
			{
				return Strings.ResourceLoader.GetString("ShadowedLibrary");
			}
		}

		// Token: 0x17002A5B RID: 10843
		// (get) Token: 0x0600A5D5 RID: 42453 RVA: 0x00224C9D File Offset: 0x00222E9D
		public static string TraitFirewallRuleRequired
		{
			get
			{
				return Strings.ResourceLoader.GetString("TraitFirewallRuleRequired");
			}
		}

		// Token: 0x17002A5C RID: 10844
		// (get) Token: 0x0600A5D6 RID: 42454 RVA: 0x00224CA9 File Offset: 0x00222EA9
		public static string InvalidCacheConfiguration
		{
			get
			{
				return Strings.ResourceLoader.GetString("InvalidCacheConfiguration");
			}
		}

		// Token: 0x02001989 RID: 6537
		private class ResourceLoader
		{
			// Token: 0x0600A5D8 RID: 42456 RVA: 0x00224CB5 File Offset: 0x00222EB5
			internal ResourceLoader()
			{
				this.resources = new ResourceManager("Microsoft.Mashup.EngineHost.Strings", base.GetType().Assembly);
			}

			// Token: 0x0600A5D9 RID: 42457 RVA: 0x00224CD8 File Offset: 0x00222ED8
			private static Strings.ResourceLoader GetLoader()
			{
				if (Strings.ResourceLoader.instance == null)
				{
					Strings.ResourceLoader resourceLoader = new Strings.ResourceLoader();
					Interlocked.CompareExchange<Strings.ResourceLoader>(ref Strings.ResourceLoader.instance, resourceLoader, null);
				}
				return Strings.ResourceLoader.instance;
			}

			// Token: 0x17002A5D RID: 10845
			// (get) Token: 0x0600A5DA RID: 42458 RVA: 0x000020FA File Offset: 0x000002FA
			public static CultureInfo Culture
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17002A5E RID: 10846
			// (get) Token: 0x0600A5DB RID: 42459 RVA: 0x00224D04 File Offset: 0x00222F04
			public static ResourceManager Resources
			{
				get
				{
					return Strings.ResourceLoader.GetLoader().resources;
				}
			}

			// Token: 0x0600A5DC RID: 42460 RVA: 0x00224D10 File Offset: 0x00222F10
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

			// Token: 0x0600A5DD RID: 42461 RVA: 0x00224D50 File Offset: 0x00222F50
			public static string GetString(string name)
			{
				Strings.ResourceLoader loader = Strings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.resources.GetString(name, Strings.ResourceLoader.Culture);
			}

			// Token: 0x0600A5DE RID: 42462 RVA: 0x00224D7C File Offset: 0x00222F7C
			public static object GetObject(string name)
			{
				Strings.ResourceLoader loader = Strings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.resources.GetObject(name, Strings.ResourceLoader.Culture);
			}

			// Token: 0x0600A5DF RID: 42463 RVA: 0x00224DA8 File Offset: 0x00222FA8
			public static T GetObject<T>(string name) where T : class
			{
				Strings.ResourceLoader loader = Strings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return default(T);
				}
				return (T)((object)loader.resources.GetObject(name, Strings.ResourceLoader.Culture));
			}

			// Token: 0x0400564F RID: 22095
			private static Strings.ResourceLoader instance;

			// Token: 0x04005650 RID: 22096
			private ResourceManager resources;
		}
	}
}
