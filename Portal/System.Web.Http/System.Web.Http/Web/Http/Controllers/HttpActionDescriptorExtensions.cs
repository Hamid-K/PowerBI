using System;

namespace System.Web.Http.Controllers
{
	// Token: 0x020000F0 RID: 240
	internal static class HttpActionDescriptorExtensions
	{
		// Token: 0x0600062E RID: 1582 RVA: 0x0000FD78 File Offset: 0x0000DF78
		public static bool IsAttributeRouted(this HttpActionDescriptor actionDescriptor)
		{
			if (actionDescriptor == null)
			{
				throw new ArgumentNullException("actionDescriptor");
			}
			object obj;
			actionDescriptor.Properties.TryGetValue("MS_IsAttributeRouted", out obj);
			return (obj as bool?) ?? false;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0000FDC4 File Offset: 0x0000DFC4
		public static void SetIsAttributeRouted(this HttpActionDescriptor actionDescriptor, bool value)
		{
			if (actionDescriptor == null)
			{
				throw new ArgumentNullException("actionDescriptor");
			}
			actionDescriptor.Properties["MS_IsAttributeRouted"] = value;
		}

		// Token: 0x0400017C RID: 380
		private const string AttributeRoutedPropertyKey = "MS_IsAttributeRouted";
	}
}
