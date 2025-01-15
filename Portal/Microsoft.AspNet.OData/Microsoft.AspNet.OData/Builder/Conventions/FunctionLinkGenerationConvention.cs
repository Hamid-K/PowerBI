using System;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder.Conventions
{
	// Token: 0x02000149 RID: 329
	internal class FunctionLinkGenerationConvention : IOperationConvention, IConvention
	{
		// Token: 0x06000C3B RID: 3131 RVA: 0x0002FAF4 File Offset: 0x0002DCF4
		public void Apply(OperationConfiguration configuration, ODataModelBuilder model)
		{
			FunctionConfiguration function = configuration as FunctionConfiguration;
			if (function == null || !function.IsBindable)
			{
				return;
			}
			if (function.BindingParameter.TypeConfiguration.Kind == EdmTypeKind.Entity && function.GetFunctionLink() == null)
			{
				string bindingParamterType2 = function.BindingParameter.TypeConfiguration.FullName;
				function.HasFunctionLink((ResourceContext entityContext) => entityContext.GenerateFunctionLink(bindingParamterType2, function.FullyQualifiedName, function.Parameters.Select((ParameterConfiguration p) => p.Name)), true);
				return;
			}
			if (function.BindingParameter.TypeConfiguration.Kind == EdmTypeKind.Collection && function.GetFeedFunctionLink() == null && ((CollectionTypeConfiguration)function.BindingParameter.TypeConfiguration).ElementType.Kind == EdmTypeKind.Entity)
			{
				string bindingParamterType = function.BindingParameter.TypeConfiguration.FullName;
				function.HasFeedFunctionLink((ResourceSetContext feedContext) => feedContext.GenerateFunctionLink(bindingParamterType, function.FullyQualifiedName, function.Parameters.Select((ParameterConfiguration p) => p.Name)), true);
			}
		}
	}
}
