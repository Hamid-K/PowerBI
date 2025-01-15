using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.JsonLight;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.Evaluation
{
	// Token: 0x02000267 RID: 615
	internal sealed class ODataMissingOperationGenerator
	{
		// Token: 0x06001BD0 RID: 7120 RVA: 0x000557B0 File Offset: 0x000539B0
		internal ODataMissingOperationGenerator(IODataResourceMetadataContext resourceMetadataContext, IODataMetadataContext metadataContext)
		{
			this.resourceMetadataContext = resourceMetadataContext;
			this.metadataContext = metadataContext;
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x000557C6 File Offset: 0x000539C6
		internal IEnumerable<ODataAction> GetComputedActions()
		{
			this.ComputeMissingOperationsToResource();
			return this.computedActions;
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x000557D4 File Offset: 0x000539D4
		internal IEnumerable<ODataFunction> GetComputedFunctions()
		{
			this.ComputeMissingOperationsToResource();
			return this.computedFunctions;
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x000557E4 File Offset: 0x000539E4
		private static HashSet<IEdmOperation> GetOperationsInEntry(ODataResourceBase resource, IEdmModel model, Uri metadataDocumentUri)
		{
			HashSet<IEdmOperation> hashSet = new HashSet<IEdmOperation>(EqualityComparer<IEdmOperation>.Default);
			IEnumerable<ODataOperation> enumerable = ODataUtilsInternal.ConcatEnumerables<ODataOperation>(resource.NonComputedActions, resource.NonComputedFunctions);
			if (enumerable != null)
			{
				foreach (ODataOperation odataOperation in enumerable)
				{
					string text = UriUtils.UriToString(odataOperation.Metadata);
					string uriFragmentFromMetadataReferencePropertyName = ODataJsonLightUtils.GetUriFragmentFromMetadataReferencePropertyName(metadataDocumentUri, text);
					IEnumerable<IEdmOperation> enumerable2 = model.ResolveOperations(uriFragmentFromMetadataReferencePropertyName);
					if (enumerable2 != null)
					{
						foreach (IEdmOperation edmOperation in enumerable2)
						{
							hashSet.Add(edmOperation);
						}
					}
				}
			}
			return hashSet;
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x000558AC File Offset: 0x00053AAC
		private void ComputeMissingOperationsToResource()
		{
			if (this.computedActions == null)
			{
				this.computedActions = new List<ODataAction>();
				this.computedFunctions = new List<ODataFunction>();
				HashSet<IEdmOperation> operationsInEntry = ODataMissingOperationGenerator.GetOperationsInEntry(this.resourceMetadataContext.Resource, this.metadataContext.Model, this.metadataContext.MetadataDocumentUri);
				foreach (IEdmOperation edmOperation in this.resourceMetadataContext.SelectedBindableOperations)
				{
					if (!operationsInEntry.Contains(edmOperation))
					{
						string text = "#" + ODataJsonLightUtils.GetMetadataReferenceName(this.metadataContext.Model, edmOperation);
						bool flag;
						ODataOperation odataOperation = ODataJsonLightUtils.CreateODataOperation(this.metadataContext.MetadataDocumentUri, text, edmOperation, out flag);
						if (edmOperation.Parameters.Any<IEdmOperationParameter>() && this.resourceMetadataContext.ActualResourceTypeName != edmOperation.Parameters.First<IEdmOperationParameter>().Type.FullName())
						{
							odataOperation.BindingParameterTypeName = edmOperation.Parameters.First<IEdmOperationParameter>().Type.FullName();
						}
						odataOperation.SetMetadataBuilder(this.resourceMetadataContext.Resource.MetadataBuilder, this.metadataContext.MetadataDocumentUri);
						if (flag)
						{
							this.computedActions.Add((ODataAction)odataOperation);
						}
						else
						{
							this.computedFunctions.Add((ODataFunction)odataOperation);
						}
					}
				}
			}
		}

		// Token: 0x04000B9C RID: 2972
		private readonly IODataMetadataContext metadataContext;

		// Token: 0x04000B9D RID: 2973
		private readonly IODataResourceMetadataContext resourceMetadataContext;

		// Token: 0x04000B9E RID: 2974
		private List<ODataAction> computedActions;

		// Token: 0x04000B9F RID: 2975
		private List<ODataFunction> computedFunctions;
	}
}
