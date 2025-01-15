using System;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder.Conventions
{
	// Token: 0x0200014A RID: 330
	internal class ActionLinkGenerationConvention : IOperationConvention, IConvention
	{
		// Token: 0x06000C3D RID: 3133 RVA: 0x0002FC2C File Offset: 0x0002DE2C
		public void Apply(OperationConfiguration configuration, ODataModelBuilder model)
		{
			ActionConfiguration action = configuration as ActionConfiguration;
			if (action == null || !action.IsBindable)
			{
				return;
			}
			if (action.BindingParameter.TypeConfiguration.Kind == EdmTypeKind.Entity && action.GetActionLink() == null)
			{
				if (action.BindingParameter.TypeConfiguration.Kind == EdmTypeKind.Entity && action.GetActionLink() == null)
				{
					string bindingParameterType2 = action.BindingParameter.TypeConfiguration.FullName;
					action.HasActionLink((ResourceContext entityContext) => entityContext.GenerateActionLink(bindingParameterType2, action.FullyQualifiedName), true);
					return;
				}
			}
			else if (action.BindingParameter.TypeConfiguration.Kind == EdmTypeKind.Collection && action.GetFeedActionLink() == null && ((CollectionTypeConfiguration)action.BindingParameter.TypeConfiguration).ElementType.Kind == EdmTypeKind.Entity)
			{
				string bindingParameterType = action.BindingParameter.TypeConfiguration.FullName;
				action.HasFeedActionLink((ResourceSetContext feedContext) => feedContext.GenerateActionLink(bindingParameterType, action.FullyQualifiedName), true);
			}
		}
	}
}
