using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020004A6 RID: 1190
	[CompilerGenerated]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	internal class SqlScriptGeneratorResource
	{
		// Token: 0x060033EC RID: 13292 RVA: 0x00171A9F File Offset: 0x0016FC9F
		internal SqlScriptGeneratorResource()
		{
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x060033ED RID: 13293 RVA: 0x00171AA8 File Offset: 0x0016FCA8
		[EditorBrowsable(2)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(SqlScriptGeneratorResource.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("Microsoft.Mashup.ScriptDom.SqlScriptGeneratorResource", typeof(SqlScriptGeneratorResource).Assembly);
					SqlScriptGeneratorResource.resourceMan = resourceManager;
				}
				return SqlScriptGeneratorResource.resourceMan;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x060033EE RID: 13294 RVA: 0x00171AE7 File Offset: 0x0016FCE7
		// (set) Token: 0x060033EF RID: 13295 RVA: 0x00171AEE File Offset: 0x0016FCEE
		[EditorBrowsable(2)]
		internal static CultureInfo Culture
		{
			get
			{
				return SqlScriptGeneratorResource.resourceCulture;
			}
			set
			{
				SqlScriptGeneratorResource.resourceCulture = value;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x060033F0 RID: 13296 RVA: 0x00171AF6 File Offset: 0x0016FCF6
		internal static string ScriptDomTreeTypeNotSupported
		{
			get
			{
				return SqlScriptGeneratorResource.ResourceManager.GetString("ScriptDomTreeTypeNotSupported", SqlScriptGeneratorResource.resourceCulture);
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x060033F1 RID: 13297 RVA: 0x00171B0C File Offset: 0x0016FD0C
		internal static string TokenTypeDoesNotHaveStringRepresentation
		{
			get
			{
				return SqlScriptGeneratorResource.ResourceManager.GetString("TokenTypeDoesNotHaveStringRepresentation", SqlScriptGeneratorResource.resourceCulture);
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x060033F2 RID: 13298 RVA: 0x00171B22 File Offset: 0x0016FD22
		internal static string UnknownEnumValue
		{
			get
			{
				return SqlScriptGeneratorResource.ResourceManager.GetString("UnknownEnumValue", SqlScriptGeneratorResource.resourceCulture);
			}
		}

		// Token: 0x04001F18 RID: 7960
		private static ResourceManager resourceMan;

		// Token: 0x04001F19 RID: 7961
		private static CultureInfo resourceCulture;
	}
}
