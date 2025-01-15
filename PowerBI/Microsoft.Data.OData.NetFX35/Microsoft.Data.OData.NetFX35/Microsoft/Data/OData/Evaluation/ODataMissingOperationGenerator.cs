using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.JsonLight;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Evaluation
{
	// Token: 0x0200010A RID: 266
	internal sealed class ODataMissingOperationGenerator
	{
		// Token: 0x06000715 RID: 1813 RVA: 0x00018478 File Offset: 0x00016678
		internal ODataMissingOperationGenerator(IODataEntryMetadataContext entryMetadataContext, IODataMetadataContext metadataContext)
		{
			this.entryMetadataContext = entryMetadataContext;
			this.metadataContext = metadataContext;
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0001848E File Offset: 0x0001668E
		internal IEnumerable<ODataAction> GetComputedActions()
		{
			this.ComputeMissingOperationsToEntry();
			return this.computedActions;
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0001849C File Offset: 0x0001669C
		internal IEnumerable<ODataFunction> GetComputedFunctions()
		{
			this.ComputeMissingOperationsToEntry();
			return this.computedFunctions;
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x000184AC File Offset: 0x000166AC
		private static HashSet<IEdmFunctionImport> GetFunctionImportsInEntry(ODataEntry entry, IEdmModel model, Uri metadataDocumentUri)
		{
			HashSet<IEdmFunctionImport> hashSet = new HashSet<IEdmFunctionImport>(EqualityComparer<IEdmFunctionImport>.Default);
			IEnumerable<ODataOperation> enumerable = ODataUtilsInternal.ConcatEnumerables<ODataOperation>((IEnumerable<ODataOperation>)entry.NonComputedActions, (IEnumerable<ODataOperation>)entry.NonComputedFunctions);
			if (enumerable != null)
			{
				foreach (ODataOperation odataOperation in enumerable)
				{
					string text = UriUtilsCommon.UriToString(odataOperation.Metadata);
					string uriFragmentFromMetadataReferencePropertyName = ODataJsonLightUtils.GetUriFragmentFromMetadataReferencePropertyName(metadataDocumentUri, text);
					IEnumerable<IEdmFunctionImport> enumerable2 = model.ResolveFunctionImports(uriFragmentFromMetadataReferencePropertyName);
					if (enumerable2 != null)
					{
						foreach (IEdmFunctionImport edmFunctionImport in enumerable2)
						{
							hashSet.Add(edmFunctionImport);
						}
					}
				}
			}
			return hashSet;
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00018584 File Offset: 0x00016784
		private void ComputeMissingOperationsToEntry()
		{
			if (this.computedActions == null)
			{
				this.computedActions = new List<ODataAction>();
				this.computedFunctions = new List<ODataFunction>();
				HashSet<IEdmFunctionImport> functionImportsInEntry = ODataMissingOperationGenerator.GetFunctionImportsInEntry(this.entryMetadataContext.Entry, this.metadataContext.Model, this.metadataContext.MetadataDocumentUri);
				foreach (IEdmFunctionImport edmFunctionImport in this.entryMetadataContext.SelectedAlwaysBindableOperations)
				{
					if (!functionImportsInEntry.Contains(edmFunctionImport))
					{
						string text = '#' + ODataJsonLightUtils.GetMetadataReferenceName(edmFunctionImport);
						bool flag;
						ODataOperation odataOperation = ODataJsonLightUtils.CreateODataOperation(this.metadataContext.MetadataDocumentUri, text, edmFunctionImport, out flag);
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

		// Token: 0x040002BE RID: 702
		private readonly IODataMetadataContext metadataContext;

		// Token: 0x040002BF RID: 703
		private readonly IODataEntryMetadataContext entryMetadataContext;

		// Token: 0x040002C0 RID: 704
		private List<ODataAction> computedActions;

		// Token: 0x040002C1 RID: 705
		private List<ODataFunction> computedFunctions;
	}
}
