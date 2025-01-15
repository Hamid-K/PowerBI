using System;

namespace Microsoft.OData.Client
{
	// Token: 0x0200003D RID: 61
	internal static class ODataWriterHelper
	{
		// Token: 0x060001DA RID: 474 RVA: 0x00008604 File Offset: 0x00006804
		public static void WriteResourceSet(ODataWriter writer, ODataResourceSetWrapper resourceSetWrapper)
		{
			writer.WriteStart(resourceSetWrapper.ResourceSet);
			if (resourceSetWrapper.Resources != null)
			{
				foreach (ODataResourceWrapper odataResourceWrapper in resourceSetWrapper.Resources)
				{
					ODataWriterHelper.WriteResource(writer, odataResourceWrapper);
				}
			}
			writer.WriteEnd();
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000866C File Offset: 0x0000686C
		public static void WriteResource(ODataWriter writer, ODataResourceWrapper resourceWrapper)
		{
			writer.WriteStart(resourceWrapper.Resource);
			if (resourceWrapper.NestedResourceInfoWrappers != null)
			{
				foreach (ODataNestedResourceInfoWrapper odataNestedResourceInfoWrapper in resourceWrapper.NestedResourceInfoWrappers)
				{
					ODataWriterHelper.WriteNestedResourceInfo(writer, odataNestedResourceInfoWrapper);
				}
			}
			writer.WriteEnd();
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000086D4 File Offset: 0x000068D4
		public static void WriteNestedResourceInfo(ODataWriter writer, ODataNestedResourceInfoWrapper nestedResourceInfo)
		{
			writer.WriteStart(nestedResourceInfo.NestedResourceInfo);
			if (nestedResourceInfo.NestedResourceOrResourceSet != null)
			{
				ODataWriterHelper.WriteItem(writer, nestedResourceInfo.NestedResourceOrResourceSet);
			}
			writer.WriteEnd();
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000086FC File Offset: 0x000068FC
		private static void WriteItem(ODataWriter writer, ODataItemWrapper odataItemWrapper)
		{
			ODataResourceWrapper odataResourceWrapper = odataItemWrapper as ODataResourceWrapper;
			if (odataResourceWrapper != null)
			{
				ODataWriterHelper.WriteResource(writer, odataResourceWrapper);
			}
			ODataResourceSetWrapper odataResourceSetWrapper = odataItemWrapper as ODataResourceSetWrapper;
			if (odataResourceSetWrapper != null)
			{
				ODataWriterHelper.WriteResourceSet(writer, odataResourceSetWrapper);
			}
		}
	}
}
