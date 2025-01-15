using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Http.Internal
{
	// Token: 0x02000185 RID: 389
	internal static class TypeDescriptorHelper
	{
		// Token: 0x06000A0C RID: 2572 RVA: 0x00019F35 File Offset: 0x00018135
		internal static ICustomTypeDescriptor Get(Type type)
		{
			return new AssociatedMetadataTypeTypeDescriptionProvider(type).GetTypeDescriptor(type);
		}
	}
}
