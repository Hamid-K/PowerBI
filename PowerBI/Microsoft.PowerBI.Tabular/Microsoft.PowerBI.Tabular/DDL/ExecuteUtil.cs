using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Xml;
using Microsoft.AnalysisServices.Core;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular.DDL
{
	// Token: 0x02000122 RID: 290
	internal static class ExecuteUtil
	{
		// Token: 0x0600146C RID: 5228 RVA: 0x0008AB6C File Offset: 0x00088D6C
		internal static bool TryExecuteXmla(Model model, string xmlaRequest, out XmlaResultCollection xmlaResults)
		{
			long num = (model.Server.IsInTransactionInternal() ? (-1L) : model.Version);
			ImpactDataSet impactDataSet;
			xmlaResults = ExecuteUtil.RunCommand(xmlaRequest, !model.Server.CaptureXml, num, model.Server, out impactDataSet);
			if (!model.Server.CaptureXml)
			{
				if (xmlaResults.ContainsErrors)
				{
					return false;
				}
				model.ApplyImpact(impactDataSet);
			}
			return true;
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x0008ABD0 File Offset: 0x00088DD0
		internal static XmlaResultCollection RunCommand(string commandText, bool requestImpact, long impactSinceVersion, Server server, out ImpactDataSet impactDataSet)
		{
			XmlaResultCollection xmlaResultCollection;
			if (requestImpact)
			{
				using (AmoDataReader amoDataReader = server.ExecuteReader(commandText, out xmlaResultCollection, new Dictionary<string, string> { 
				{
					"ReturnAffectedObjects",
					XmlConvert.ToString(impactSinceVersion)
				} }, true))
				{
					if (amoDataReader != null)
					{
						DataAdapter dataAdapter = new AmoDataAdapter(amoDataReader);
						DataSet dataSet = new DataSet();
						dataAdapter.Fill(dataSet);
						impactDataSet = new ImpactDataSet(dataSet, amoDataReader.TopLevelAttributes);
						return amoDataReader.Results;
					}
					impactDataSet = null;
					return xmlaResultCollection;
				}
			}
			xmlaResultCollection = server.Execute(commandText);
			impactDataSet = null;
			return xmlaResultCollection;
		}
	}
}
