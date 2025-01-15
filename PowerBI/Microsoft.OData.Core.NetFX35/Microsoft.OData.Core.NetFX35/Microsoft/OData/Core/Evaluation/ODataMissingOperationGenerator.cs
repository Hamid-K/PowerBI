using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Core.JsonLight;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.Evaluation
{
	// Token: 0x0200008D RID: 141
	internal sealed class ODataMissingOperationGenerator
	{
		// Token: 0x0600059A RID: 1434 RVA: 0x000147A4 File Offset: 0x000129A4
		internal ODataMissingOperationGenerator(IODataEntryMetadataContext entryMetadataContext, IODataMetadataContext metadataContext)
		{
			this.entryMetadataContext = entryMetadataContext;
			this.metadataContext = metadataContext;
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x000147BA File Offset: 0x000129BA
		internal IEnumerable<ODataAction> GetComputedActions()
		{
			this.ComputeMissingOperationsToEntry();
			return this.computedActions;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x000147C8 File Offset: 0x000129C8
		internal IEnumerable<ODataFunction> GetComputedFunctions()
		{
			this.ComputeMissingOperationsToEntry();
			return this.computedFunctions;
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x000147D8 File Offset: 0x000129D8
		private static HashSet<IEdmOperation> GetOperationsInEntry(ODataEntry entry, IEdmModel model, Uri metadataDocumentUri)
		{
			HashSet<IEdmOperation> hashSet = new HashSet<IEdmOperation>(EqualityComparer<IEdmOperation>.Default);
			IEnumerable<ODataOperation> enumerable = ODataUtilsInternal.ConcatEnumerables<ODataOperation>((IEnumerable<ODataOperation>)entry.NonComputedActions, (IEnumerable<ODataOperation>)entry.NonComputedFunctions);
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

		// Token: 0x0600059E RID: 1438 RVA: 0x000148B0 File Offset: 0x00012AB0
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "Parameter type is needed to get binding type.")]
		private void ComputeMissingOperationsToEntry()
		{
			if (this.computedActions == null)
			{
				this.computedActions = new List<ODataAction>();
				this.computedFunctions = new List<ODataFunction>();
				HashSet<IEdmOperation> operationsInEntry = ODataMissingOperationGenerator.GetOperationsInEntry(this.entryMetadataContext.Entry, this.metadataContext.Model, this.metadataContext.MetadataDocumentUri);
				foreach (IEdmOperation edmOperation in this.entryMetadataContext.SelectedBindableOperations)
				{
					if (!operationsInEntry.Contains(edmOperation))
					{
						string text = '#' + ODataJsonLightUtils.GetMetadataReferenceName(this.metadataContext.Model, edmOperation);
						bool flag;
						ODataOperation odataOperation = ODataJsonLightUtils.CreateODataOperation(this.metadataContext.MetadataDocumentUri, text, edmOperation, out flag);
						if (Enumerable.Any<IEdmOperationParameter>(edmOperation.Parameters) && this.entryMetadataContext.ActualEntityTypeName != Enumerable.First<IEdmOperationParameter>(edmOperation.Parameters).Type.FullName())
						{
							odataOperation.BindingParameterTypeName = Enumerable.First<IEdmOperationParameter>(edmOperation.Parameters).Type.FullName();
						}
						odataOperation.SetMetadataBuilder(this.entryMetadataContext.Entry.MetadataBuilder, this.metadataContext.MetadataDocumentUri);
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

		// Token: 0x0400025E RID: 606
		private readonly IODataMetadataContext metadataContext;

		// Token: 0x0400025F RID: 607
		private readonly IODataEntryMetadataContext entryMetadataContext;

		// Token: 0x04000260 RID: 608
		private List<ODataAction> computedActions;

		// Token: 0x04000261 RID: 609
		private List<ODataFunction> computedFunctions;
	}
}
