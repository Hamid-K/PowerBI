using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001C9 RID: 457
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class RDLUpgradeStrings
	{
		// Token: 0x06000EDB RID: 3803 RVA: 0x00024489 File Offset: 0x00022689
		internal RDLUpgradeStrings()
		{
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06000EDC RID: 3804 RVA: 0x00024491 File Offset: 0x00022691
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (RDLUpgradeStrings.resourceMan == null)
				{
					RDLUpgradeStrings.resourceMan = new ResourceManager("Microsoft.ReportingServices.RdlObjectModel.RDLUpgradeStrings", typeof(RDLUpgradeStrings).Assembly);
				}
				return RDLUpgradeStrings.resourceMan;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x000244BD File Offset: 0x000226BD
		// (set) Token: 0x06000EDE RID: 3806 RVA: 0x000244C4 File Offset: 0x000226C4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return RDLUpgradeStrings.resourceCulture;
			}
			set
			{
				RDLUpgradeStrings.resourceCulture = value;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x000244CC File Offset: 0x000226CC
		internal static string rdlInvalidTargetNamespace
		{
			get
			{
				return RDLUpgradeStrings.ResourceManager.GetString("rdlInvalidTargetNamespace", RDLUpgradeStrings.resourceCulture);
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x000244E2 File Offset: 0x000226E2
		internal static string rdlInvalidXmlContents
		{
			get
			{
				return RDLUpgradeStrings.ResourceManager.GetString("rdlInvalidXmlContents", RDLUpgradeStrings.resourceCulture);
			}
		}

		// Token: 0x04000555 RID: 1365
		private static ResourceManager resourceMan;

		// Token: 0x04000556 RID: 1366
		private static CultureInfo resourceCulture;
	}
}
