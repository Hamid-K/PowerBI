using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Microsoft.DataShaping.Common.DaxComparer;
using Microsoft.DataShaping.Processing.DataShapeResultGeneration;
using Microsoft.DataShaping.Processing.Pipeline;
using Microsoft.DataShaping.Processing.QueryExecution;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;
using Microsoft.DataShaping.Processing.Utils;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.DataPreparation
{
	// Token: 0x02000083 RID: 131
	internal sealed class DataPreparer
	{
		// Token: 0x06000353 RID: 851 RVA: 0x0000B008 File Offset: 0x00009208
		internal static IRowSourceManager CreateRowSourceManager(IList<IRowSource> rowSources, IList<DataSet> dataSets, IList<DataTransform> dataTransforms, IList<ResultTableLookupInfo> resultTableInfos, Microsoft.DataShaping.ServiceContracts.ITracer tracer, Microsoft.DataShaping.ServiceContracts.ITelemetryService telemetryService, IDataTransformPluginFactory transformFactory, ProcessingCompareInfo compareInfo, CancellationToken cancelToken)
		{
			ReadOnlyCollection<bool[]> readOnlyCollection = dataSets.Select((DataSet d) => d.ResultTableIndexesToCache).ToReadOnlyCollection<bool[]>();
			IDataTransformManager dataTransformManager = null;
			if (dataTransforms != null)
			{
				StringKeyGenerator stringKeyGenerator = new StringKeyGenerator(compareInfo.CompareInfo, compareInfo.CompareOptions, compareInfo.NullAsBlank, compareInfo.UseOrdinalStringKeyGeneration);
				ExpressionEvaluatorSingleRow expressionEvaluatorSingleRow = new ExpressionEvaluatorSingleRow(new DaxDataComparer(compareInfo.CompareInfo, compareInfo.CompareOptions, compareInfo.NullAsBlank), stringKeyGenerator);
				dataTransformManager = new DataTransformManager(tracer, telemetryService, transformFactory, dataTransforms.AsReadOnlyCollection<DataTransform>(), cancelToken, expressionEvaluatorSingleRow);
			}
			return new RowSourceManager(rowSources.AsReadOnlyCollection<IRowSource>(), resultTableInfos.AsReadOnlyCollection<ResultTableLookupInfo>(), readOnlyCollection, tracer, dataTransformManager);
		}
	}
}
