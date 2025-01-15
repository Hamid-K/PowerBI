using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.PowerBI.ExploreHost.Utils
{
	// Token: 0x02000035 RID: 53
	internal static class ExportDataMetadataBuilder
	{
		// Token: 0x060001A4 RID: 420 RVA: 0x00005170 File Offset: 0x00003370
		public static ExportDataMetadata Build(DataQueryRequest queryRequest, QueryBindingDescriptor queryBindingDescriptor)
		{
			if (queryBindingDescriptor == null)
			{
				return null;
			}
			IList<Tuple<string, string>> list = ExportDataMetadataBuilder.CreatePrimarySelectsMap(queryBindingDescriptor, DataQuery.GetExportDataQueryCommand(queryRequest.Query).Columns, DataQuery.GetSemanticQueryDataShapeCommand(queryRequest.Query));
			ExportDataMetadata exportDataMetadata = new ExportDataMetadata();
			exportDataMetadata.PrimarySelectsMap = list;
			exportDataMetadata.ColumnsFormatting = queryBindingDescriptor.Select.Select((SelectBinding sb) => sb.Format).ToList<string>();
			return exportDataMetadata;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000051E8 File Offset: 0x000033E8
		private static IList<Tuple<string, string>> CreatePrimarySelectsMap(QueryBindingDescriptor queryBindingDescriptor, IList<ExportDataColumn> columns, SemanticQueryDataShapeCommand command)
		{
			return columns.Select(delegate(ExportDataColumn dataQueryColumn)
			{
				int num = command.Query.Select.FindIndex((QueryExpressionContainer select) => select.Name == dataQueryColumn.QueryName);
				return Tuple.Create<string, string>(queryBindingDescriptor.Select[num].Value, dataQueryColumn.Name);
			}).ToList<Tuple<string, string>>();
		}
	}
}
