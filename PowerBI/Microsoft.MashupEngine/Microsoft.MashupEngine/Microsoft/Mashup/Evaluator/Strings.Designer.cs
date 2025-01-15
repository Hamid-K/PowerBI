using System;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001D7D RID: 7549
	internal class Strings
	{
		// Token: 0x17002E49 RID: 11849
		// (get) Token: 0x0600BB7B RID: 47995 RVA: 0x0025F5F2 File Offset: 0x0025D7F2
		public static ResourceManager ResourceManager
		{
			get
			{
				return Strings.ResourceLoader.Resources;
			}
		}

		// Token: 0x0600BB7C RID: 47996 RVA: 0x0025F5F9 File Offset: 0x0025D7F9
		public static string Firewall_Buffering_Failed(object p0, object p1)
		{
			return Strings.ResourceLoader.GetString("Firewall_Buffering_Failed", new object[] { p0, p1 });
		}

		// Token: 0x17002E4A RID: 11850
		// (get) Token: 0x0600BB7D RID: 47997 RVA: 0x0025F613 File Offset: 0x0025D813
		public static string FirewallRuleRequired
		{
			get
			{
				return Strings.ResourceLoader.GetString("FirewallRuleRequired");
			}
		}

		// Token: 0x17002E4B RID: 11851
		// (get) Token: 0x0600BB7E RID: 47998 RVA: 0x0025F61F File Offset: 0x0025D81F
		public static string FirewallFlow_Cyclic_Reference
		{
			get
			{
				return Strings.ResourceLoader.GetString("FirewallFlow_Cyclic_Reference");
			}
		}

		// Token: 0x0600BB7F RID: 47999 RVA: 0x0025F62B File Offset: 0x0025D82B
		public static string FirewallFlow_DeviationFromGroup(object p0, object p1, object p2)
		{
			return Strings.ResourceLoader.GetString("FirewallFlow_DeviationFromGroup", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600BB80 RID: 48000 RVA: 0x0025F649 File Offset: 0x0025D849
		public static string FirewallFlow_NoGroupsAllowed(object p0)
		{
			return Strings.ResourceLoader.GetString("FirewallFlow_NoGroupsAllowed", new object[] { p0 });
		}

		// Token: 0x0600BB81 RID: 48001 RVA: 0x0025F65F File Offset: 0x0025D85F
		public static string FirewallFlow_IllegalReference(object p0, object p1)
		{
			return Strings.ResourceLoader.GetString("FirewallFlow_IllegalReference", new object[] { p0, p1 });
		}

		// Token: 0x0600BB82 RID: 48002 RVA: 0x0025F679 File Offset: 0x0025D879
		public static string FirewallFlow_CantCreateGroup(object p0)
		{
			return Strings.ResourceLoader.GetString("FirewallFlow_CantCreateGroup", new object[] { p0 });
		}

		// Token: 0x17002E4C RID: 11852
		// (get) Token: 0x0600BB83 RID: 48003 RVA: 0x0025F68F File Offset: 0x0025D88F
		public static string ValueSerializer_TypeTooComplex
		{
			get
			{
				return Strings.ResourceLoader.GetString("ValueSerializer_TypeTooComplex");
			}
		}

		// Token: 0x17002E4D RID: 11853
		// (get) Token: 0x0600BB84 RID: 48004 RVA: 0x0025F69B File Offset: 0x0025D89B
		public static string ValueSerializer_ValueTooComplex
		{
			get
			{
				return Strings.ResourceLoader.GetString("ValueSerializer_ValueTooComplex");
			}
		}

		// Token: 0x17002E4E RID: 11854
		// (get) Token: 0x0600BB85 RID: 48005 RVA: 0x0025F6A7 File Offset: 0x0025D8A7
		public static string Evaluation_Result_Deserialization_Error
		{
			get
			{
				return Strings.ResourceLoader.GetString("Evaluation_Result_Deserialization_Error");
			}
		}

		// Token: 0x17002E4F RID: 11855
		// (get) Token: 0x0600BB86 RID: 48006 RVA: 0x0025F6B3 File Offset: 0x0025D8B3
		public static string Unexpected_Error
		{
			get
			{
				return Strings.ResourceLoader.GetString("Unexpected_Error");
			}
		}

		// Token: 0x0600BB87 RID: 48007 RVA: 0x0025F6BF File Offset: 0x0025D8BF
		public static string Evaluation_TerminatedUnexpectedly(object p0, object p1)
		{
			return Strings.ResourceLoader.GetString("Evaluation_TerminatedUnexpectedly", new object[] { p0, p1 });
		}

		// Token: 0x0600BB88 RID: 48008 RVA: 0x0025F6D9 File Offset: 0x0025D8D9
		public static string Evaluation_ContainerExitedUnexpectedly(object p0, object p1)
		{
			return Strings.ResourceLoader.GetString("Evaluation_ContainerExitedUnexpectedly", new object[] { p0, p1 });
		}

		// Token: 0x0600BB89 RID: 48009 RVA: 0x0025F6F3 File Offset: 0x0025D8F3
		public static string Evaluation_UsedFeatures(object p0, object p1)
		{
			return Strings.ResourceLoader.GetString("Evaluation_UsedFeatures", new object[] { p0, p1 });
		}

		// Token: 0x17002E50 RID: 11856
		// (get) Token: 0x0600BB8A RID: 48010 RVA: 0x0025F70D File Offset: 0x0025D90D
		public static string Evaluation_NoFeatures
		{
			get
			{
				return Strings.ResourceLoader.GetString("Evaluation_NoFeatures");
			}
		}

		// Token: 0x17002E51 RID: 11857
		// (get) Token: 0x0600BB8B RID: 48011 RVA: 0x0025F719 File Offset: 0x0025D919
		public static string Evaluation_OutOfMemory
		{
			get
			{
				return Strings.ResourceLoader.GetString("Evaluation_OutOfMemory");
			}
		}

		// Token: 0x17002E52 RID: 11858
		// (get) Token: 0x0600BB8C RID: 48012 RVA: 0x0025F725 File Offset: 0x0025D925
		public static string Evaluation_StackOverflow
		{
			get
			{
				return Strings.ResourceLoader.GetString("Evaluation_StackOverflow");
			}
		}

		// Token: 0x17002E53 RID: 11859
		// (get) Token: 0x0600BB8D RID: 48013 RVA: 0x0025F731 File Offset: 0x0025D931
		public static string Evaluation_Canceled
		{
			get
			{
				return Strings.ResourceLoader.GetString("Evaluation_Canceled");
			}
		}

		// Token: 0x17002E54 RID: 11860
		// (get) Token: 0x0600BB8E RID: 48014 RVA: 0x0025F73D File Offset: 0x0025D93D
		public static string Container_Terminated
		{
			get
			{
				return Strings.ResourceLoader.GetString("Container_Terminated");
			}
		}

		// Token: 0x0600BB8F RID: 48015 RVA: 0x0025F749 File Offset: 0x0025D949
		public static string Messenger_NoHandler(object p0)
		{
			return Strings.ResourceLoader.GetString("Messenger_NoHandler", new object[] { p0 });
		}

		// Token: 0x0600BB90 RID: 48016 RVA: 0x0025F75F File Offset: 0x0025D95F
		public static string FirewallFlow_NoMatch(object p0)
		{
			return Strings.ResourceLoader.GetString("FirewallFlow_NoMatch", new object[] { p0 });
		}

		// Token: 0x0600BB91 RID: 48017 RVA: 0x0025F775 File Offset: 0x0025D975
		public static string Parameter_TypeNotSupported(object p0)
		{
			return Strings.ResourceLoader.GetString("Parameter_TypeNotSupported", new object[] { p0 });
		}

		// Token: 0x0600BB92 RID: 48018 RVA: 0x0025F78B File Offset: 0x0025D98B
		public static string Invalid_Section_Attribute(object p0, object p1)
		{
			return Strings.ResourceLoader.GetString("Invalid_Section_Attribute", new object[] { p0, p1 });
		}

		// Token: 0x0600BB93 RID: 48019 RVA: 0x0025F7A5 File Offset: 0x0025D9A5
		public static string Invalid_Section_Version(object p0, object p1)
		{
			return Strings.ResourceLoader.GetString("Invalid_Section_Version", new object[] { p0, p1 });
		}

		// Token: 0x0600BB94 RID: 48020 RVA: 0x0025F7BF File Offset: 0x0025D9BF
		public static string Invalid_Section_Dependency(object p0, object p1)
		{
			return Strings.ResourceLoader.GetString("Invalid_Section_Dependency", new object[] { p0, p1 });
		}

		// Token: 0x0600BB95 RID: 48021 RVA: 0x0025F7D9 File Offset: 0x0025D9D9
		public static string Recursive_Section_Dependency(object p0, object p1)
		{
			return Strings.ResourceLoader.GetString("Recursive_Section_Dependency", new object[] { p0, p1 });
		}

		// Token: 0x0600BB96 RID: 48022 RVA: 0x0025F7F3 File Offset: 0x0025D9F3
		public static string Missing_Section_Dependency(object p0, object p1)
		{
			return Strings.ResourceLoader.GetString("Missing_Section_Dependency", new object[] { p0, p1 });
		}

		// Token: 0x0600BB97 RID: 48023 RVA: 0x0025F80D File Offset: 0x0025DA0D
		public static string Section_Name_Mismatch(object p0, object p1)
		{
			return Strings.ResourceLoader.GetString("Section_Name_Mismatch", new object[] { p0, p1 });
		}

		// Token: 0x0600BB98 RID: 48024 RVA: 0x0025F827 File Offset: 0x0025DA27
		public static string Illegal_Module_Reference(object p0)
		{
			return Strings.ResourceLoader.GetString("Illegal_Module_Reference", new object[] { p0 });
		}

		// Token: 0x0600BB99 RID: 48025 RVA: 0x0025F83D File Offset: 0x0025DA3D
		public static string Invalid_Dynamic_Module(object p0)
		{
			return Strings.ResourceLoader.GetString("Invalid_Dynamic_Module", new object[] { p0 });
		}

		// Token: 0x0600BB9A RID: 48026 RVA: 0x0025F853 File Offset: 0x0025DA53
		public static string Invalid_Dynamic_Generator(object p0, object p1)
		{
			return Strings.ResourceLoader.GetString("Invalid_Dynamic_Generator", new object[] { p0, p1 });
		}

		// Token: 0x17002E55 RID: 11861
		// (get) Token: 0x0600BB9B RID: 48027 RVA: 0x0025F86D File Offset: 0x0025DA6D
		public static string Duplicate_Member_Definition
		{
			get
			{
				return Strings.ResourceLoader.GetString("Duplicate_Member_Definition");
			}
		}

		// Token: 0x17002E56 RID: 11862
		// (get) Token: 0x0600BB9C RID: 48028 RVA: 0x0025F879 File Offset: 0x0025DA79
		public static string LineageWasLost
		{
			get
			{
				return Strings.ResourceLoader.GetString("LineageWasLost");
			}
		}

		// Token: 0x0600BB9D RID: 48029 RVA: 0x0025F885 File Offset: 0x0025DA85
		public static string InvalidTimeZone(object p0)
		{
			return Strings.ResourceLoader.GetString("InvalidTimeZone", new object[] { p0 });
		}

		// Token: 0x02001D7E RID: 7550
		private class ResourceLoader
		{
			// Token: 0x0600BB9F RID: 48031 RVA: 0x0025F89B File Offset: 0x0025DA9B
			internal ResourceLoader()
			{
				this.resources = new ResourceManager("Microsoft.Mashup.Evaluator.Strings", base.GetType().Assembly);
			}

			// Token: 0x0600BBA0 RID: 48032 RVA: 0x0025F8C0 File Offset: 0x0025DAC0
			private static Strings.ResourceLoader GetLoader()
			{
				if (Strings.ResourceLoader.instance == null)
				{
					Strings.ResourceLoader resourceLoader = new Strings.ResourceLoader();
					Interlocked.CompareExchange<Strings.ResourceLoader>(ref Strings.ResourceLoader.instance, resourceLoader, null);
				}
				return Strings.ResourceLoader.instance;
			}

			// Token: 0x17002E57 RID: 11863
			// (get) Token: 0x0600BBA1 RID: 48033 RVA: 0x000020FA File Offset: 0x000002FA
			public static CultureInfo Culture
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17002E58 RID: 11864
			// (get) Token: 0x0600BBA2 RID: 48034 RVA: 0x0025F8EC File Offset: 0x0025DAEC
			public static ResourceManager Resources
			{
				get
				{
					return Strings.ResourceLoader.GetLoader().resources;
				}
			}

			// Token: 0x0600BBA3 RID: 48035 RVA: 0x0025F8F8 File Offset: 0x0025DAF8
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

			// Token: 0x0600BBA4 RID: 48036 RVA: 0x0025F938 File Offset: 0x0025DB38
			public static string GetString(string name)
			{
				Strings.ResourceLoader loader = Strings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.resources.GetString(name, Strings.ResourceLoader.Culture);
			}

			// Token: 0x0600BBA5 RID: 48037 RVA: 0x0025F964 File Offset: 0x0025DB64
			public static object GetObject(string name)
			{
				Strings.ResourceLoader loader = Strings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.resources.GetObject(name, Strings.ResourceLoader.Culture);
			}

			// Token: 0x0600BBA6 RID: 48038 RVA: 0x0025F990 File Offset: 0x0025DB90
			public static T GetObject<T>(string name) where T : class
			{
				Strings.ResourceLoader loader = Strings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return default(T);
				}
				return (T)((object)loader.resources.GetObject(name, Strings.ResourceLoader.Culture));
			}

			// Token: 0x04005F75 RID: 24437
			private static Strings.ResourceLoader instance;

			// Token: 0x04005F76 RID: 24438
			private ResourceManager resources;
		}
	}
}
