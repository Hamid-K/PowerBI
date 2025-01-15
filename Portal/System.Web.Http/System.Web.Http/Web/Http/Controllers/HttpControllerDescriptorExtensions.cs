using System;

namespace System.Web.Http.Controllers
{
	// Token: 0x020000F1 RID: 241
	internal static class HttpControllerDescriptorExtensions
	{
		// Token: 0x06000630 RID: 1584 RVA: 0x0000FDEC File Offset: 0x0000DFEC
		public static bool IsAttributeRouted(this HttpControllerDescriptor controllerDescriptor)
		{
			if (controllerDescriptor == null)
			{
				throw new ArgumentNullException("controllerDescriptor");
			}
			object obj;
			controllerDescriptor.Properties.TryGetValue("MS_IsAttributeRouted", out obj);
			return (obj as bool?) ?? false;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0000FE38 File Offset: 0x0000E038
		public static void SetIsAttributeRouted(this HttpControllerDescriptor controllerDescriptor, bool value)
		{
			if (controllerDescriptor == null)
			{
				throw new ArgumentNullException("controllerDescriptor");
			}
			controllerDescriptor.Properties["MS_IsAttributeRouted"] = value;
		}

		// Token: 0x0400017D RID: 381
		private const string AttributeRoutedPropertyKey = "MS_IsAttributeRouted";
	}
}
