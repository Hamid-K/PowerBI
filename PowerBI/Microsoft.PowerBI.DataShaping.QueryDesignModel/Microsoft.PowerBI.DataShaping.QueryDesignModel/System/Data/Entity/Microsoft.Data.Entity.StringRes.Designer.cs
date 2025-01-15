using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace System.Data.Entity
{
	// Token: 0x02000017 RID: 23
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class StringRes
	{
		// Token: 0x060005E4 RID: 1508 RVA: 0x000099C0 File Offset: 0x00007BC0
		internal StringRes()
		{
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x000099C8 File Offset: 0x00007BC8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (StringRes.resourceMan == null)
				{
					StringRes.resourceMan = new ResourceManager("Microsoft.Data.Entity.StringRes", typeof(StringRes).Assembly);
				}
				return StringRes.resourceMan;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x000099F4 File Offset: 0x00007BF4
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x000099FB File Offset: 0x00007BFB
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return StringRes.resourceCulture;
			}
			set
			{
				StringRes.resourceCulture = value;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x00009A03 File Offset: 0x00007C03
		internal static string ReturnTypeDeclaredAsAttributeAndElement
		{
			get
			{
				return StringRes.ResourceManager.GetString("ReturnTypeDeclaredAsAttributeAndElement", StringRes.resourceCulture);
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x00009A19 File Offset: 0x00007C19
		internal static string ReturnTypeMustBeDeclared
		{
			get
			{
				return StringRes.ResourceManager.GetString("ReturnTypeMustBeDeclared", StringRes.resourceCulture);
			}
		}

		// Token: 0x0400059E RID: 1438
		private static ResourceManager resourceMan;

		// Token: 0x0400059F RID: 1439
		private static CultureInfo resourceCulture;
	}
}
