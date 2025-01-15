using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200014B RID: 331
	internal sealed class GetUserModelAction : RSSoapAction<GetUserModelActionParameters>
	{
		// Token: 0x06000CCE RID: 3278 RVA: 0x0002F542 File Offset: 0x0002D742
		internal GetUserModelAction(RSService service)
			: base("GetUserModelAction", service)
		{
			Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.ReportBuilder);
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x0002F564 File Offset: 0x0002D764
		internal override void PerformActionNow()
		{
			if (RSTrace.CatalogTrace.TraceVerbose)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "Call to GetUserModelAction( '{0}' )", new object[] { base.ActionParameters.ItemPath });
			}
			this.m_userModel = null;
			this.m_userModelWR = null;
			this.m_userModelDefinitionWR = null;
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ItemPath, "Model");
			ModelCatalogItem modelCatalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, ItemType.Model, true) as ModelCatalogItem;
			this.m_userModel = modelCatalogItem.LoadUserModel(base.ActionParameters.PerspectiveID);
			modelCatalogItem.ThrowIfNoAccess(ModelOperation.ReadProperties);
			if (RSTrace.CatalogTrace.TraceVerbose)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "Call to GetUserModelAction completed");
			}
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x0002F628 File Offset: 0x0002D828
		internal SemanticModel GetUserModel()
		{
			if (this.m_userModel == null && this.m_userModelWR != null && this.m_userModelWR.IsAlive)
			{
				this.m_userModel = (SemanticModel)this.m_userModelWR.Target;
			}
			if (this.m_userModel == null)
			{
				base.Execute();
			}
			RSTrace.CatalogTrace.Assert(this.m_userModel != null, "m_userModel is null after call to GetUserModelAction.Execute()");
			SemanticModel userModel = this.m_userModel;
			this.m_userModelWR = new WeakReference(this.m_userModelWR);
			this.m_userModel = null;
			return userModel;
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x0002F6AC File Offset: 0x0002D8AC
		public byte[] GetUserModelDefinition()
		{
			byte[] array = null;
			if (this.m_userModelDefinitionWR != null && this.m_userModelDefinitionWR.IsAlive)
			{
				array = (byte[])this.m_userModelWR.Target;
			}
			if (array == null)
			{
				SemanticModel userModel = this.GetUserModel();
				MemoryStream memoryStream = new MemoryStream(1024);
				XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
				userModel.WriteTo(xmlTextWriter, ModelingSerializationOptions.OmitBindings);
				array = memoryStream.ToArray();
				this.m_userModelDefinitionWR = new WeakReference(array);
			}
			RSTrace.CatalogTrace.Assert(array != null, "userModelDefinition != null");
			return array;
		}

		// Token: 0x04000528 RID: 1320
		private WeakReference m_userModelWR;

		// Token: 0x04000529 RID: 1321
		private SemanticModel m_userModel;

		// Token: 0x0400052A RID: 1322
		private WeakReference m_userModelDefinitionWR;
	}
}
