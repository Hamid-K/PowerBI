using System;
using System.Xml;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000159 RID: 345
	internal abstract class UpdateModelDefinitionAction<P> : UpdateItemAction<P, ModelCatalogItem> where P : UpdateModelDefinitionActionParameters, new()
	{
		// Token: 0x06000D20 RID: 3360 RVA: 0x00030394 File Offset: 0x0002E594
		public UpdateModelDefinitionAction(string actionName, RSService service)
			: base(actionName, service)
		{
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000D21 RID: 3361
		protected abstract bool ShouldLoadExistingModelDefinition { get; }

		// Token: 0x06000D22 RID: 3362 RVA: 0x00005BF2 File Offset: 0x00003DF2
		internal virtual void EnsureModelItem(ModelCatalogItem item)
		{
		}

		// Token: 0x06000D23 RID: 3363
		internal abstract byte[] GetNewModelDefinition(ModelCatalogItem existingModel);

		// Token: 0x06000D24 RID: 3364
		internal abstract void VerifyIDAndAdjustDatasource(ModelCatalogItem existingModel, SemanticModel newModel);

		// Token: 0x06000D25 RID: 3365
		internal abstract void VerifyProperties(ModelCatalogItem model);

		// Token: 0x06000D26 RID: 3366
		internal abstract void AdjustProperties(ItemProperties properties);

		// Token: 0x06000D27 RID: 3367 RVA: 0x000303A0 File Offset: 0x0002E5A0
		internal override void Update(ModelCatalogItem existingModel)
		{
			this.EnsureModelItem(existingModel);
			existingModel.LoadModel(this.ShouldLoadExistingModelDefinition);
			existingModel.ThrowIfNoAccess(ModelOperation.UpdateContent);
			existingModel.ModelItemPolicies.CacheInherited = false;
			this.VerifyProperties(existingModel);
			byte[] newModelDefinition = this.GetNewModelDefinition(existingModel);
			ValidationMessageCollection validationMessageCollection;
			SemanticModel semanticModel = ModelCatalogItem.CompileModelDefinition(newModelDefinition, true, existingModel.ItemContext.ItemPath.Value, out validationMessageCollection);
			base.ActionParameters.Warnings = Warning.ModelingMessagesToWarningArray(validationMessageCollection);
			SnapshotBase snapshotBase = CreateModelAction.CreateBinarySnapshot(semanticModel, base.Service);
			base.Service.Storage.SetObjectContent(existingModel.ItemContext.CatalogItemPath, ItemType.Model, newModelDefinition, snapshotBase.SnapshotDataID, null, Guid.Empty, null);
			existingModel.RemoveFromCache();
			this.VerifyIDAndAdjustDatasource(existingModel, semanticModel);
			this.AdjustProperties(existingModel.Properties);
			UpdateModelDefinitionAction<P>.PropagateSemanticModelProperties(semanticModel, existingModel.Properties, true);
			this.MergeModelItemSecurity(existingModel, semanticModel);
			this.UpdateModelPerspectives(existingModel, semanticModel);
			existingModel.Content = newModelDefinition;
			existingModel.Save(false);
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00030490 File Offset: 0x0002E690
		internal static void PropagateSemanticModelProperties(SemanticModel model, ItemProperties properties, bool overrideExistingValues)
		{
			if (overrideExistingValues || properties["MustUsePerspective"] == null)
			{
				QName qname = new QName("MustUsePerspective", "http://schemas.microsoft.com/sqlserver/2004/11/semanticquerydesign");
				CustomProperty customProperty = model.CustomProperties[qname];
				if (customProperty != null && customProperty.Value is bool)
				{
					properties["MustUsePerspective"] = XmlConvert.ToString((bool)customProperty.Value);
					return;
				}
				properties.Remove("MustUsePerspective");
			}
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x00030504 File Offset: 0x0002E704
		private void MergeModelItemSecurity(ModelCatalogItem existingModel, SemanticModel newModel)
		{
			foreach (object obj in existingModel.ModelItemPolicies.GetAllPolicies())
			{
				ModelItemPolicy modelItemPolicy = (ModelItemPolicy)obj;
				ModelItem modelItem = newModel.LookupItemByID(ModelItem.StringToID(modelItemPolicy.ModelItemID));
				if (modelItem == null || modelItem is Perspective)
				{
					string text;
					if (modelItem == null)
					{
						text = existingModel.Model.LookupItemByID(ModelItem.StringToID(modelItemPolicy.ModelItemID)).Name;
					}
					else
					{
						text = modelItem.Name;
					}
					base.Service.SecMgr.DeleteModelItemPolicy(existingModel.CatalogItemID, modelItemPolicy.ModelItemID, text);
				}
			}
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x000305C0 File Offset: 0x0002E7C0
		private void UpdateModelPerspectives(ModelCatalogItem existingModel, SemanticModel newModel)
		{
			ModelCatalogItem.ModelStorage modelStorage = new ModelCatalogItem.ModelStorage(base.Service);
			modelStorage.DeleteModelPerspectives(existingModel.CatalogItemID);
			modelStorage.AddModelPerspectives(existingModel.CatalogItemID, newModel);
		}

		// Token: 0x0400053B RID: 1339
		private const string MUST_USE_PERSPECTIVE_SCHEMA = "http://schemas.microsoft.com/sqlserver/2004/11/semanticquerydesign";
	}
}
