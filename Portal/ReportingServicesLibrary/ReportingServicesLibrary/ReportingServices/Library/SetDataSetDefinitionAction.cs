using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000FA RID: 250
	internal sealed class SetDataSetDefinitionAction : UpdateItemAction<SetDataSetDefinitionActionParameters, DataSetCatalogItem>
	{
		// Token: 0x06000A46 RID: 2630 RVA: 0x0002754B File Offset: 0x0002574B
		public SetDataSetDefinitionAction(RSService service)
			: base("SetDataSetDefinition", service)
		{
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0002755C File Offset: 0x0002575C
		internal override void Update(DataSetCatalogItem dataSet)
		{
			dataSet.Content = base.ActionParameters.DataSetDefinition;
			dataSet.LoadProperties();
			dataSet.ThrowIfNoAccess(ReportOperation.UpdateReportDefinition);
			ReportSnapshot reportSnapshot;
			ParameterInfoCollection parameterInfoCollection;
			Warning[] array;
			DataSourceInfoCollection dataSourceInfoCollection;
			base.Service.CreateDataSetAction.ConvertToIntermediate(dataSet.Content, dataSet.Properties, dataSet.ItemContext, dataSet.ModificationDate, out reportSnapshot, out parameterInfoCollection, out array, out dataSourceInfoCollection);
			dataSet.Parameters = parameterInfoCollection;
			dataSet.CompiledDefinition = reportSnapshot;
			dataSet.DataSources = dataSet.DataSources.CombineOnSetDefinition(dataSourceInfoCollection);
			dataSet.Save(false);
			base.ActionParameters.Warnings = array;
		}
	}
}
