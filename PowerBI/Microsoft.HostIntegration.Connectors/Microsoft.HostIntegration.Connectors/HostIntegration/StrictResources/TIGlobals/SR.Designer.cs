using System;
using System.Globalization;
using System.Resources;

namespace Microsoft.HostIntegration.StrictResources.TIGlobals
{
	// Token: 0x0200070B RID: 1803
	internal class SR
	{
		// Token: 0x0600391F RID: 14623 RVA: 0x00002061 File Offset: 0x00000261
		private SR()
		{
		}

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x06003920 RID: 14624 RVA: 0x000BF3BD File Offset: 0x000BD5BD
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (SR.resourceManager == null)
				{
					SR.resourceManager = new ResourceManager("Microsoft.HostIntegration.StrictResources.TIGlobals.SR", typeof(SR).Assembly);
				}
				return SR.resourceManager;
			}
		}

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x06003921 RID: 14625 RVA: 0x000BF3E9 File Offset: 0x000BD5E9
		// (set) Token: 0x06003922 RID: 14626 RVA: 0x000BF3F0 File Offset: 0x000BD5F0
		internal static CultureInfo Culture
		{
			get
			{
				return SR.resourceCulture;
			}
			set
			{
				SR.resourceCulture = value;
			}
		}

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x06003923 RID: 14627 RVA: 0x000BF3F8 File Offset: 0x000BD5F8
		internal static string UnknownTransport
		{
			get
			{
				return SR.ResourceManager.GetString("UnknownTransport", SR.Culture);
			}
		}

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x06003924 RID: 14628 RVA: 0x000BF40E File Offset: 0x000BD60E
		internal static string BufferPositionTooBig
		{
			get
			{
				return SR.ResourceManager.GetString("BufferPositionTooBig", SR.Culture);
			}
		}

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x06003925 RID: 14629 RVA: 0x000BF424 File Offset: 0x000BD624
		internal static string BufferDataPositionTooBig
		{
			get
			{
				return SR.ResourceManager.GetString("BufferDataPositionTooBig", SR.Culture);
			}
		}

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x06003926 RID: 14630 RVA: 0x000BF43A File Offset: 0x000BD63A
		internal static string BufferDataPositionTooSmall
		{
			get
			{
				return SR.ResourceManager.GetString("BufferDataPositionTooSmall", SR.Culture);
			}
		}

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x06003927 RID: 14631 RVA: 0x000BF450 File Offset: 0x000BD650
		internal static string HeaderSizeTooBig
		{
			get
			{
				return SR.ResourceManager.GetString("HeaderSizeTooBig", SR.Culture);
			}
		}

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x06003928 RID: 14632 RVA: 0x000BF466 File Offset: 0x000BD666
		internal static string TrailerSizeTooBig
		{
			get
			{
				return SR.ResourceManager.GetString("TrailerSizeTooBig", SR.Culture);
			}
		}

		// Token: 0x06003929 RID: 14633 RVA: 0x000BF47C File Offset: 0x000BD67C
		internal static string PersistentConnectionTimeout(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("PersistentConnectionTimeout", SR.Culture), param0);
		}

		// Token: 0x0600392A RID: 14634 RVA: 0x000BF49D File Offset: 0x000BD69D
		internal static string UnknownPrimitiveConverterClass(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("UnknownPrimitiveConverterClass", SR.Culture), param0);
		}

		// Token: 0x0600392B RID: 14635 RVA: 0x000BF4BE File Offset: 0x000BD6BE
		internal static string UnknownRemoteEnvironmentClassId(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("UnknownRemoteEnvironmentClassId", SR.Culture), param0);
		}

		// Token: 0x0600392C RID: 14636 RVA: 0x000BF4DF File Offset: 0x000BD6DF
		internal static string UnknownRemoteEnvironmentType(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("UnknownRemoteEnvironmentType", SR.Culture), param0);
		}

		// Token: 0x0600392D RID: 14637 RVA: 0x000BF500 File Offset: 0x000BD700
		internal static string UnableToCreateRECFactory(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("UnableToCreateRECFactory", SR.Culture), param0);
		}

		// Token: 0x04002141 RID: 8513
		private static ResourceManager resourceManager;

		// Token: 0x04002142 RID: 8514
		private static CultureInfo resourceCulture;
	}
}
