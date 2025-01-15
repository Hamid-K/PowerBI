using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000DB RID: 219
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class StringResources
	{
		// Token: 0x060008E3 RID: 2275 RVA: 0x0002A3A7 File Offset: 0x000285A7
		internal StringResources()
		{
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x0002A3AF File Offset: 0x000285AF
		[EditorBrowsable(2)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (StringResources.resourceMan == null)
				{
					StringResources.resourceMan = new ResourceManager("Microsoft.DataIntegration.FuzzyMatching.StringResources", typeof(StringResources).Assembly);
				}
				return StringResources.resourceMan;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x0002A3DB File Offset: 0x000285DB
		// (set) Token: 0x060008E6 RID: 2278 RVA: 0x0002A3E2 File Offset: 0x000285E2
		[EditorBrowsable(2)]
		internal static CultureInfo Culture
		{
			get
			{
				return StringResources.resourceCulture;
			}
			set
			{
				StringResources.resourceCulture = value;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x0002A3EA File Offset: 0x000285EA
		internal static string AlreadyInLookupMode
		{
			get
			{
				return StringResources.ResourceManager.GetString("AlreadyInLookupMode", StringResources.resourceCulture);
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x0002A400 File Offset: 0x00028600
		internal static string AlreadyInUpdateMode
		{
			get
			{
				return StringResources.ResourceManager.GetString("AlreadyInUpdateMode", StringResources.resourceCulture);
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x0002A416 File Offset: 0x00028616
		internal static string ColumnNotFoundInSchemaTable
		{
			get
			{
				return StringResources.ResourceManager.GetString("ColumnNotFoundInSchemaTable", StringResources.resourceCulture);
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060008EA RID: 2282 RVA: 0x0002A42C File Offset: 0x0002862C
		internal static string DomainAlreadyExists
		{
			get
			{
				return StringResources.ResourceManager.GetString("DomainAlreadyExists", StringResources.resourceCulture);
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x0002A442 File Offset: 0x00028642
		internal static string DomainCapacityExceeded
		{
			get
			{
				return StringResources.ResourceManager.GetString("DomainCapacityExceeded", StringResources.resourceCulture);
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060008EC RID: 2284 RVA: 0x0002A458 File Offset: 0x00028658
		internal static string DomainNotFound
		{
			get
			{
				return StringResources.ResourceManager.GetString("DomainNotFound", StringResources.resourceCulture);
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x0002A46E File Offset: 0x0002866E
		internal static string EpsilonRulesNotAllowed
		{
			get
			{
				return StringResources.ResourceManager.GetString("EpsilonRulesNotAllowed", StringResources.resourceCulture);
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x0002A484 File Offset: 0x00028684
		internal static string InvalidMode
		{
			get
			{
				return StringResources.ResourceManager.GetString("InvalidMode", StringResources.resourceCulture);
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x0002A49A File Offset: 0x0002869A
		internal static string MustCallEndLookup
		{
			get
			{
				return StringResources.ResourceManager.GetString("MustCallEndLookup", StringResources.resourceCulture);
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060008F0 RID: 2288 RVA: 0x0002A4B0 File Offset: 0x000286B0
		internal static string MustCallEndUpdate
		{
			get
			{
				return StringResources.ResourceManager.GetString("MustCallEndUpdate", StringResources.resourceCulture);
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x0002A4C6 File Offset: 0x000286C6
		internal static string OutputSchemaName
		{
			get
			{
				return StringResources.ResourceManager.GetString("OutputSchemaName", StringResources.resourceCulture);
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x0002A4DC File Offset: 0x000286DC
		internal static string SimilarityColumnName
		{
			get
			{
				return StringResources.ResourceManager.GetString("SimilarityColumnName", StringResources.resourceCulture);
			}
		}

		// Token: 0x0400037C RID: 892
		private static ResourceManager resourceMan;

		// Token: 0x0400037D RID: 893
		private static CultureInfo resourceCulture;
	}
}
