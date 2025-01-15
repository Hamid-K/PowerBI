using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.Mashup.Security.Cryptography.Properties
{
	// Token: 0x02002005 RID: 8197
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x0600C7B2 RID: 51122 RVA: 0x000020FD File Offset: 0x000002FD
		internal Resources()
		{
		}

		// Token: 0x1700305A RID: 12378
		// (get) Token: 0x0600C7B3 RID: 51123 RVA: 0x0027BD34 File Offset: 0x00279F34
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("Security.Cryptography.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x1700305B RID: 12379
		// (get) Token: 0x0600C7B4 RID: 51124 RVA: 0x0027BD60 File Offset: 0x00279F60
		// (set) Token: 0x0600C7B5 RID: 51125 RVA: 0x0027BD67 File Offset: 0x00279F67
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x1700305C RID: 12380
		// (get) Token: 0x0600C7B6 RID: 51126 RVA: 0x0027BD6F File Offset: 0x00279F6F
		internal static string AlreadyTransformedFinalBlock
		{
			get
			{
				return Resources.ResourceManager.GetString("AlreadyTransformedFinalBlock", Resources.resourceCulture);
			}
		}

		// Token: 0x1700305D RID: 12381
		// (get) Token: 0x0600C7B7 RID: 51127 RVA: 0x0027BD85 File Offset: 0x00279F85
		internal static string CannotDecryptPartialBlock
		{
			get
			{
				return Resources.ResourceManager.GetString("CannotDecryptPartialBlock", Resources.resourceCulture);
			}
		}

		// Token: 0x1700305E RID: 12382
		// (get) Token: 0x0600C7B8 RID: 51128 RVA: 0x0027BD9B File Offset: 0x00279F9B
		internal static string DuplicateCryptoConfigAlias
		{
			get
			{
				return Resources.ResourceManager.GetString("DuplicateCryptoConfigAlias", Resources.resourceCulture);
			}
		}

		// Token: 0x1700305F RID: 12383
		// (get) Token: 0x0600C7B9 RID: 51129 RVA: 0x0027BDB1 File Offset: 0x00279FB1
		internal static string EmptyCryptoConfigAlias
		{
			get
			{
				return Resources.ResourceManager.GetString("EmptyCryptoConfigAlias", Resources.resourceCulture);
			}
		}

		// Token: 0x17003060 RID: 12384
		// (get) Token: 0x0600C7BA RID: 51130 RVA: 0x0027BDC7 File Offset: 0x00279FC7
		internal static string InvalidChainingModeName
		{
			get
			{
				return Resources.ResourceManager.GetString("InvalidChainingModeName", Resources.resourceCulture);
			}
		}

		// Token: 0x17003061 RID: 12385
		// (get) Token: 0x0600C7BB RID: 51131 RVA: 0x0027BDDD File Offset: 0x00279FDD
		internal static string InvalidIVSize
		{
			get
			{
				return Resources.ResourceManager.GetString("InvalidIVSize", Resources.resourceCulture);
			}
		}

		// Token: 0x17003062 RID: 12386
		// (get) Token: 0x0600C7BC RID: 51132 RVA: 0x0027BDF3 File Offset: 0x00279FF3
		internal static string InvalidPadding
		{
			get
			{
				return Resources.ResourceManager.GetString("InvalidPadding", Resources.resourceCulture);
			}
		}

		// Token: 0x17003063 RID: 12387
		// (get) Token: 0x0600C7BD RID: 51133 RVA: 0x0027BE09 File Offset: 0x0027A009
		internal static string InvalidRsaParameters
		{
			get
			{
				return Resources.ResourceManager.GetString("InvalidRsaParameters", Resources.resourceCulture);
			}
		}

		// Token: 0x17003064 RID: 12388
		// (get) Token: 0x0600C7BE RID: 51134 RVA: 0x0027BE1F File Offset: 0x0027A01F
		internal static string InvalidSignatureHashAlgorithm
		{
			get
			{
				return Resources.ResourceManager.GetString("InvalidSignatureHashAlgorithm", Resources.resourceCulture);
			}
		}

		// Token: 0x17003065 RID: 12389
		// (get) Token: 0x0600C7BF RID: 51135 RVA: 0x0027BE35 File Offset: 0x0027A035
		internal static string InvalidTagSize
		{
			get
			{
				return Resources.ResourceManager.GetString("InvalidTagSize", Resources.resourceCulture);
			}
		}

		// Token: 0x17003066 RID: 12390
		// (get) Token: 0x0600C7C0 RID: 51136 RVA: 0x0027BE4B File Offset: 0x0027A04B
		internal static string KeyMustBeRsa
		{
			get
			{
				return Resources.ResourceManager.GetString("KeyMustBeRsa", Resources.resourceCulture);
			}
		}

		// Token: 0x17003067 RID: 12391
		// (get) Token: 0x0600C7C1 RID: 51137 RVA: 0x0027BE61 File Offset: 0x0027A061
		internal static string MissingIV
		{
			get
			{
				return Resources.ResourceManager.GetString("MissingIV", Resources.resourceCulture);
			}
		}

		// Token: 0x17003068 RID: 12392
		// (get) Token: 0x0600C7C2 RID: 51138 RVA: 0x0027BE77 File Offset: 0x0027A077
		internal static string TagIsOnlyGeneratedAfterFinalBlock
		{
			get
			{
				return Resources.ResourceManager.GetString("TagIsOnlyGeneratedAfterFinalBlock", Resources.resourceCulture);
			}
		}

		// Token: 0x17003069 RID: 12393
		// (get) Token: 0x0600C7C3 RID: 51139 RVA: 0x0027BE8D File Offset: 0x0027A08D
		internal static string TagIsOnlyGeneratedDuringEncryption
		{
			get
			{
				return Resources.ResourceManager.GetString("TagIsOnlyGeneratedDuringEncryption", Resources.resourceCulture);
			}
		}

		// Token: 0x1700306A RID: 12394
		// (get) Token: 0x0600C7C4 RID: 51140 RVA: 0x0027BEA3 File Offset: 0x0027A0A3
		internal static string UnsupportedCipherMode
		{
			get
			{
				return Resources.ResourceManager.GetString("UnsupportedCipherMode", Resources.resourceCulture);
			}
		}

		// Token: 0x1700306B RID: 12395
		// (get) Token: 0x0600C7C5 RID: 51141 RVA: 0x0027BEB9 File Offset: 0x0027A0B9
		internal static string UnsupportedPaddingMode
		{
			get
			{
				return Resources.ResourceManager.GetString("UnsupportedPaddingMode", Resources.resourceCulture);
			}
		}

		// Token: 0x040065F8 RID: 26104
		private static ResourceManager resourceMan;

		// Token: 0x040065F9 RID: 26105
		private static CultureInfo resourceCulture;
	}
}
