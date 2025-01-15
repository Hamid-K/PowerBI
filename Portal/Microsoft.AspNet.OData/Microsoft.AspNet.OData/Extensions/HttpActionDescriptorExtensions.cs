using System;
using System.ComponentModel;
using System.Web.Http;
using System.Web.Http.Controllers;
using Microsoft.AspNet.OData.Adapters;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Extensions
{
	// Token: 0x020001C8 RID: 456
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal static class HttpActionDescriptorExtensions
	{
		// Token: 0x06000F20 RID: 3872 RVA: 0x0003E370 File Offset: 0x0003C570
		internal static IEdmModel GetEdmModel(this HttpActionDescriptor actionDescriptor, Type entityClrType)
		{
			if (actionDescriptor == null)
			{
				throw Error.ArgumentNull("actionDescriptor");
			}
			if (entityClrType == null)
			{
				throw Error.ArgumentNull("entityClrType");
			}
			return actionDescriptor.Properties.GetOrAdd("Microsoft.AspNet.OData.Model+" + entityClrType.FullName, delegate(object _)
			{
				ODataConventionModelBuilder odataConventionModelBuilder = new ODataConventionModelBuilder(new WebApiAssembliesResolver(ServicesExtensions.GetAssembliesResolver(actionDescriptor.Configuration.Services)), true);
				EntityTypeConfiguration entityTypeConfiguration = odataConventionModelBuilder.AddEntityType(entityClrType);
				odataConventionModelBuilder.AddEntitySet(entityClrType.Name, entityTypeConfiguration);
				return odataConventionModelBuilder.GetEdmModel();
			}) as IEdmModel;
		}

		// Token: 0x04000431 RID: 1073
		private const string ModelKeyPrefix = "Microsoft.AspNet.OData.Model+";
	}
}
