using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.JsonLight;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.Evaluation
{
	// Token: 0x0200022C RID: 556
	internal sealed class ODataMissingOperationGenerator
	{
		// Token: 0x060016A6 RID: 5798 RVA: 0x000457EC File Offset: 0x000439EC
		internal ODataMissingOperationGenerator(IODataResourceMetadataContext resourceMetadataContext, IODataMetadataContext metadataContext)
		{
			this.resourceMetadataContext = resourceMetadataContext;
			this.metadataContext = metadataContext;
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x00045802 File Offset: 0x00043A02
		internal IEnumerable<ODataAction> GetComputedActions()
		{
			this.ComputeMissingOperationsToResource();
			return this.computedActions;
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x00045810 File Offset: 0x00043A10
		internal IEnumerable<ODataFunction> GetComputedFunctions()
		{
			this.ComputeMissingOperationsToResource();
			return this.computedFunctions;
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x00045820 File Offset: 0x00043A20
		private static HashSet<IEdmOperation> GetOperationsInEntry(ODataResource resource, IEdmModel model, Uri metadataDocumentUri)
		{
			HashSet<IEdmOperation> hashSet = new HashSet<IEdmOperation>(EqualityComparer<IEdmOperation>.Default);
			IEnumerable<ODataOperation> enumerable = ODataUtilsInternal.ConcatEnumerables<ODataOperation>((IEnumerable<ODataOperation>)resource.NonComputedActions, (IEnumerable<ODataOperation>)resource.NonComputedFunctions);
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

		// Token: 0x060016AA RID: 5802 RVA: 0x000458F4 File Offset: 0x00043AF4
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
						if (Enumerable.Any<IEdmOperationParameter>(edmOperation.Parameters) && this.resourceMetadataContext.ActualResourceTypeName != Enumerable.First<IEdmOperationParameter>(edmOperation.Parameters).Type.FullName())
						{
							odataOperation.BindingParameterTypeName = Enumerable.First<IEdmOperationParameter>(edmOperation.Parameters).Type.FullName();
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

		// Token: 0x04000A71 RID: 2673
		private readonly IODataMetadataContext metadataContext;

		// Token: 0x04000A72 RID: 2674
		private readonly IODataResourceMetadataContext resourceMetadataContext;

		// Token: 0x04000A73 RID: 2675
		private List<ODataAction> computedActions;

		// Token: 0x04000A74 RID: 2676
		private List<ODataFunction> computedFunctions;
	}
}
