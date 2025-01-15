using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap2005;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200013A RID: 314
	internal sealed class ModelAndPerspectiveListBuilder
	{
		// Token: 0x06000C62 RID: 3170 RVA: 0x0002E577 File Offset: 0x0002C777
		internal ModelAndPerspectiveListBuilder(Security securityManager)
		{
			this.m_securityCheckCache = new SecurityCheckCache(securityManager, CommonOperation.ReadProperties);
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0002E5B4 File Offset: 0x0002C7B4
		internal void AddModelPerspective(Guid modelPolicyID, byte[] modelSecDesc, Guid modelCatalogItemID, ExternalItemPath modelPath, string modelDescription, string perspectiveID, string perspectiveName, string perspectiveDescription)
		{
			if (modelCatalogItemID == Guid.Empty)
			{
				throw new InternalCatalogException("PerspectiveListBuilder.AddModelPerspective: Model catalog item is an empty guid!");
			}
			if (modelCatalogItemID == this.m_currentModelCatalogItemID)
			{
				if (!this.m_userHasAccessToCurrentModel)
				{
					return;
				}
				this.AddPerspectiveToCurrentModel(perspectiveID, perspectiveName, perspectiveDescription);
				return;
			}
			else
			{
				this.CompleteCurrentModel();
				this.m_currentModelCatalogItemID = modelCatalogItemID;
				if (this.m_securityCheckCache.CheckAccess(ItemType.Model, modelPolicyID, modelSecDesc, modelPath) != SecurityCheckCache.CheckResult.AccessGranted)
				{
					this.m_userHasAccessToCurrentModel = false;
					return;
				}
				this.m_userHasAccessToCurrentModel = true;
				this.StartNewModel(modelPath, modelDescription);
				this.AddPerspectiveToCurrentModel(perspectiveID, perspectiveName, perspectiveDescription);
				return;
			}
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0002E642 File Offset: 0x0002C842
		private void StartNewModel(ExternalItemPath modelPath, string modelDescription)
		{
			this.m_currentModel = new ModelCatalogItem();
			this.m_currentModel.Model = modelPath.Value;
			this.m_currentModel.Description = modelDescription;
			this.m_currentPerspectives.Clear();
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0002E677 File Offset: 0x0002C877
		private void CompleteCurrentModel()
		{
			if (this.m_currentModel != null)
			{
				this.m_currentModel.Perspectives = this.m_currentPerspectives.ToArray();
				this.m_modelList.Add(this.m_currentModel);
				this.m_currentModel = null;
			}
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x0002E6B0 File Offset: 0x0002C8B0
		private void AddPerspectiveToCurrentModel(string perspectiveID, string perspectiveName, string perspectiveDescription)
		{
			if (perspectiveID != null)
			{
				ModelPerspective modelPerspective = new ModelPerspective();
				modelPerspective.ID = perspectiveID;
				modelPerspective.Name = perspectiveName;
				modelPerspective.Description = perspectiveDescription;
				this.m_currentPerspectives.Add(modelPerspective);
			}
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x0002E6E7 File Offset: 0x0002C8E7
		internal ModelCatalogItem[] GetModelAndPerspectiveList()
		{
			this.CompleteCurrentModel();
			return this.m_modelList.ToArray();
		}

		// Token: 0x04000510 RID: 1296
		private List<ModelPerspective> m_currentPerspectives = new List<ModelPerspective>();

		// Token: 0x04000511 RID: 1297
		private ModelCatalogItem m_currentModel;

		// Token: 0x04000512 RID: 1298
		private Guid m_currentModelCatalogItemID = Guid.Empty;

		// Token: 0x04000513 RID: 1299
		private bool m_userHasAccessToCurrentModel = true;

		// Token: 0x04000514 RID: 1300
		private List<ModelCatalogItem> m_modelList = new List<ModelCatalogItem>();

		// Token: 0x04000515 RID: 1301
		private SecurityCheckCache m_securityCheckCache;
	}
}
