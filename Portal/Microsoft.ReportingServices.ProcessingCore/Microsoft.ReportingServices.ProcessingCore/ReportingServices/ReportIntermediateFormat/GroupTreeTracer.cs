using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200052F RID: 1327
	internal static class GroupTreeTracer
	{
		// Token: 0x06004850 RID: 18512 RVA: 0x001312A8 File Offset: 0x0012F4A8
		internal static void TraceReportInstance(OnDemandMetadata odpMetadata, ReportInstance reportInstance, Report reportDefinition, int level)
		{
			if (!Global.Tracer.TraceVerbose)
			{
				return;
			}
			if (reportInstance == null)
			{
				return;
			}
			if (odpMetadata != null)
			{
				GroupTreeTracer.TraceMetadata(odpMetadata, level);
			}
			Global.Tracer.Trace("{0}Report: NoRows={1}, Language={2}, Variables={3}", new object[]
			{
				GroupTreeTracer.GetEmptyString(level),
				reportInstance.NoRows,
				reportInstance.Language,
				GroupTreeTracer.FlattenObjectArray(reportInstance.VariableValues)
			});
			if (odpMetadata == null && reportDefinition != null && reportDefinition.MappingDataSetIndexToDataSet.Count > 0)
			{
				Global.Tracer.Trace("{0}{1} DataSetInstances: ", new object[]
				{
					GroupTreeTracer.GetEmptyString(level),
					reportDefinition.MappingDataSetIndexToDataSet.Count
				});
				IEnumerator cachedDataSetInstances = reportInstance.GetCachedDataSetInstances();
				while (cachedDataSetInstances.MoveNext())
				{
					object obj = cachedDataSetInstances.Current;
					DataSetInstance dataSetInstance = (DataSetInstance)obj;
					if (dataSetInstance != null)
					{
						GroupTreeTracer.TraceDataSetInstance(dataSetInstance, level + 1);
					}
				}
			}
			GroupTreeTracer.TraceScopeInstance(reportInstance, level);
		}

		// Token: 0x06004851 RID: 18513 RVA: 0x00131388 File Offset: 0x0012F588
		private static string GetEmptyString(int level)
		{
			int count = GroupTreeTracer.m_strings.Count;
			if (level >= count)
			{
				for (int i = count; i <= level; i++)
				{
					GroupTreeTracer.m_strings.Add(new string(' ', level * 3));
				}
			}
			return GroupTreeTracer.m_strings[level];
		}

		// Token: 0x06004852 RID: 18514 RVA: 0x001313D0 File Offset: 0x0012F5D0
		private static void TraceMetadata(OnDemandMetadata odpMetadata, int level)
		{
			if (odpMetadata == null)
			{
				return;
			}
			if (odpMetadata.ReportSnapshot != null)
			{
				Global.Tracer.Trace("{0}ReportSnapshot: Time={1}, Language={2}, User={3}, Path={4}", new object[]
				{
					GroupTreeTracer.GetEmptyString(level),
					odpMetadata.ReportSnapshot.ExecutionTime,
					odpMetadata.ReportSnapshot.Language,
					odpMetadata.ReportSnapshot.RequestUserName,
					odpMetadata.ReportSnapshot.ReportServerUrl + odpMetadata.ReportSnapshot.ReportFolder
				});
				Global.Tracer.Trace("{0}Snapshot flags: Bookmarks={1}, DocumentMap={2}, ShowHide={3}, UserSort={4}", new object[]
				{
					GroupTreeTracer.GetEmptyString(level),
					odpMetadata.ReportSnapshot.HasBookmarks,
					odpMetadata.ReportSnapshot.HasDocumentMap,
					odpMetadata.ReportSnapshot.HasShowHide,
					odpMetadata.ReportSnapshot.HasUserSortFilter
				});
			}
			if (odpMetadata.DataChunkMap != null)
			{
				Global.Tracer.Trace("{0}Data chunk map: ", new object[] { GroupTreeTracer.GetEmptyString(level) });
				Dictionary<string, DataSetInstance> dataChunkMap = odpMetadata.DataChunkMap;
				lock (dataChunkMap)
				{
					IDictionaryEnumerator dictionaryEnumerator = odpMetadata.DataChunkMap.GetEnumerator();
					while (dictionaryEnumerator.MoveNext())
					{
						Global.Tracer.Trace("{0}Data chunk={1}", new object[]
						{
							GroupTreeTracer.GetEmptyString(level + 1),
							(string)dictionaryEnumerator.Key
						});
						GroupTreeTracer.TraceDataSetInstance(dictionaryEnumerator.Value as DataSetInstance, level + 2);
					}
				}
			}
		}

		// Token: 0x06004853 RID: 18515 RVA: 0x00131574 File Offset: 0x0012F774
		private static void TraceScopeInstance(ScopeInstance scopeInstance, int level)
		{
			Global.Tracer.Assert(scopeInstance != null, "(null != scopeInstance)");
			if (0L < scopeInstance.FirstRowOffset)
			{
				Global.Tracer.Trace("{0}FirstRowOffset={1}", new object[]
				{
					GroupTreeTracer.GetEmptyString(level),
					scopeInstance.FirstRowOffset
				});
			}
			if (scopeInstance.AggregateValues != null && scopeInstance.AggregateValues.Count != 0)
			{
				StringBuilder stringBuilder = new StringBuilder(GroupTreeTracer.GetEmptyString(level));
				stringBuilder.Append("Aggregates=");
				foreach (DataAggregateObjResult dataAggregateObjResult in scopeInstance.AggregateValues)
				{
					if (dataAggregateObjResult == null)
					{
						stringBuilder.Append("(NULL)");
					}
					else
					{
						stringBuilder.Append(dataAggregateObjResult.Value);
					}
					stringBuilder.Append("#");
				}
				Global.Tracer.Trace(stringBuilder.ToString());
			}
			if (scopeInstance.SubreportInstances != null && scopeInstance.SubreportInstances.Count != 0)
			{
				int count = scopeInstance.SubreportInstances.Count;
				Global.Tracer.Trace("{0}{1} SubReportInstances:", new object[]
				{
					GroupTreeTracer.GetEmptyString(level),
					count
				});
				for (int i = 0; i < count; i++)
				{
					if (scopeInstance.SubreportInstances[i] != null)
					{
						GroupTreeTracer.TraceSubReportInstance(scopeInstance.SubreportInstances[i].Value(), level + 1);
					}
				}
			}
			if (scopeInstance.DataRegionInstances != null && scopeInstance.DataRegionInstances.Count != 0)
			{
				int count2 = scopeInstance.DataRegionInstances.Count;
				Global.Tracer.Trace("{0}{1} DataRegionInstances:", new object[]
				{
					GroupTreeTracer.GetEmptyString(level),
					count2
				});
				for (int j = 0; j < count2; j++)
				{
					if (scopeInstance.DataRegionInstances[j] != null)
					{
						GroupTreeTracer.TraceDataRegionInstance(scopeInstance.DataRegionInstances[j].Value(), level + 1);
					}
				}
			}
		}

		// Token: 0x06004854 RID: 18516 RVA: 0x0013177C File Offset: 0x0012F97C
		private static void TraceDataSetInstance(DataSetInstance instance, int level)
		{
			Global.Tracer.Assert(instance != null, "(null != instance)");
			Global.Tracer.Trace("{0}DataSet={1}, NoRows={2}, RowCount={3}, Lcid={4}, CS={5}, AS={6}, KS={7}, WS={8}, CommandText={9}", new object[]
			{
				GroupTreeTracer.GetEmptyString(level),
				(instance.DataSetDef != null) ? instance.DataSetDef.Name : null,
				instance.NoRows,
				instance.RecordSetSize,
				instance.LCID,
				instance.CaseSensitivity.ToString(),
				instance.AccentSensitivity.ToString(),
				instance.KanatypeSensitivity.ToString(),
				instance.WidthSensitivity.ToString(),
				instance.RewrittenCommandText
			});
			GroupTreeTracer.TraceScopeInstance(instance, level);
		}

		// Token: 0x06004855 RID: 18517 RVA: 0x0013186C File Offset: 0x0012FA6C
		private static void TraceDataRegionInstance(DataRegionInstance instance, int level)
		{
			if (instance == null)
			{
				return;
			}
			Global.Tracer.Assert(instance != null, "(null != instance)");
			Global.Tracer.Trace("{0}DataRegion={1}, [id={2}], DataSetIndex={3}", new object[]
			{
				GroupTreeTracer.GetEmptyString(level),
				instance.DataRegionDef.Name,
				instance.DataRegionDef.ID,
				instance.DataSetIndexInCollection
			});
			GroupTreeTracer.TraceScopeInstance(instance, level);
			if (instance.TopLevelColumnMembers != null)
			{
				Global.Tracer.Trace("{0}{1} Top Level COLUMN Members:", new object[]
				{
					GroupTreeTracer.GetEmptyString(level),
					instance.TopLevelColumnMembers.Count
				});
				GroupTreeTracer.TraceDataRegionMemberInstances(instance.TopLevelColumnMembers, level);
			}
			if (instance.TopLevelRowMembers != null)
			{
				Global.Tracer.Trace("{0}{1} Top Level ROW Members:", new object[]
				{
					GroupTreeTracer.GetEmptyString(level),
					instance.TopLevelRowMembers.Count
				});
				GroupTreeTracer.TraceDataRegionMemberInstances(instance.TopLevelRowMembers, level);
			}
			if (instance.Cells != null && instance.Cells.Count > 0)
			{
				Global.Tracer.Trace("{0} DataRegion Cells:", new object[] { GroupTreeTracer.GetEmptyString(level) });
				GroupTreeTracer.TraceCellInstances(instance.Cells, level + 1);
			}
		}

		// Token: 0x06004856 RID: 18518 RVA: 0x001319B0 File Offset: 0x0012FBB0
		private static void TraceDataRegionMemberInstances(List<ScalableList<DataRegionMemberInstance>> members, int level)
		{
			if (members != null && members.Count != 0)
			{
				foreach (ScalableList<DataRegionMemberInstance> scalableList in members)
				{
					foreach (DataRegionMemberInstance dataRegionMemberInstance in scalableList)
					{
						GroupTreeTracer.TraceDataRegionMemberInstance(dataRegionMemberInstance, level + 1);
					}
				}
			}
		}

		// Token: 0x06004857 RID: 18519 RVA: 0x00131A38 File Offset: 0x0012FC38
		private static void TraceDataRegionMemberInstance(DataRegionMemberInstance instance, int level)
		{
			Global.Tracer.Assert(instance != null, "(null != instance)");
			string emptyString = GroupTreeTracer.GetEmptyString(level);
			Global.Tracer.Trace("{0}DataRegionMemberInstance={1}, [id={2}], Index={3}, RecursiveLevel={4}", new object[]
			{
				emptyString,
				instance.MemberDef.Grouping.Name,
				instance.MemberDef.ID,
				instance.MemberInstanceIndexWithinScopeLevel,
				instance.RecursiveLevel
			});
			if (instance.GroupVariables != null)
			{
				Global.Tracer.Trace("{0}Group Variables={1}", new object[]
				{
					emptyString,
					GroupTreeTracer.FlattenObjectArray(instance.GroupVariables)
				});
			}
			if (instance.GroupExprValues != null)
			{
				Global.Tracer.Trace("{0}Group Expr.Vals={1}", new object[]
				{
					emptyString,
					GroupTreeTracer.FlattenObjectArray(instance.GroupExprValues)
				});
			}
			GroupTreeTracer.TraceScopeInstance(instance, level + 1);
			GroupTreeTracer.TraceCellInstances(instance.Cells, level + 1);
			if (instance.Children != null && instance.Children.Count != 0)
			{
				GroupTreeTracer.TraceDataRegionMemberInstances(instance.Children, level + 1);
			}
		}

		// Token: 0x06004858 RID: 18520 RVA: 0x00131B54 File Offset: 0x0012FD54
		private static void TraceCellInstances(ScalableList<DataCellInstanceList> cells, int level)
		{
			if (cells != null && cells.Count != 0)
			{
				int num = 0;
				foreach (DataCellInstanceList dataCellInstanceList in cells)
				{
					Global.Tracer.Trace("{0}CellInstances for index={1}:", new object[]
					{
						GroupTreeTracer.GetEmptyString(level),
						num++
					});
					if (dataCellInstanceList != null)
					{
						foreach (DataCellInstance dataCellInstance in dataCellInstanceList)
						{
							GroupTreeTracer.TraceCellInstance(dataCellInstance, level + 1);
						}
					}
				}
			}
		}

		// Token: 0x06004859 RID: 18521 RVA: 0x00131C14 File Offset: 0x0012FE14
		private static void TraceCellInstance(DataCellInstance instance, int level)
		{
			if (instance == null)
			{
				return;
			}
			string text = "{0}DataCellInstance [id={1}]";
			string text2 = null;
			string text3 = null;
			if (instance.CellDef is TablixCell && ((TablixCell)instance.CellDef).CellContents != null)
			{
				text = "{0}DataCellInstance [id={1}, name={2}]";
				text2 = ((TablixCell)instance.CellDef).CellContents.Name;
				if (((TablixCell)instance.CellDef).AltCellContents != null)
				{
					text = "{0}DataCellInstance [id={1}, name={2}, altname={3}]";
					text3 = ((TablixCell)instance.CellDef).AltCellContents.Name;
				}
			}
			Global.Tracer.Trace(text, new object[]
			{
				GroupTreeTracer.GetEmptyString(level),
				instance.CellDef.ID,
				text2,
				text3
			});
			GroupTreeTracer.TraceScopeInstance(instance, level);
		}

		// Token: 0x0600485A RID: 18522 RVA: 0x00131CD4 File Offset: 0x0012FED4
		private static void TraceSubReportInstance(SubReportInstance instance, int level)
		{
			if (instance == null)
			{
				return;
			}
			Global.Tracer.Trace("{0}SubReport, Status={1}, Error={2}, NoRows={3}, InstanceUniqueName={4}, Culture={5}", new object[]
			{
				GroupTreeTracer.GetEmptyString(level),
				instance.RetrievalStatus,
				instance.ProcessedWithError,
				instance.ProcessedWithError || instance.NoRows,
				instance.InstanceUniqueName,
				instance.ThreadCulture
			});
			GroupTreeTracer.TraceParameters(instance.Parameters, level);
			if (instance.SubReportDef.Report != null)
			{
				int count = instance.SubReportDef.Report.MappingDataSetIndexToDataSet.Count;
			}
			GroupTreeTracer.TraceReportInstance(null, instance.ReportInstance.Value(), instance.SubReportDef.Report, level);
		}

		// Token: 0x0600485B RID: 18523 RVA: 0x00131D98 File Offset: 0x0012FF98
		private static void TraceParameters(ParametersImpl parameters, int level)
		{
			if (parameters == null)
			{
				return;
			}
			for (int i = 0; i < parameters.Count; i++)
			{
				Global.Tracer.Trace("{0}Parameter{1}: MV={2}, {3} values: [{4}] labels: [{5}]", new object[]
				{
					GroupTreeTracer.GetEmptyString(level),
					i,
					parameters.Collection[i].IsMultiValue,
					parameters.Collection[i].Count,
					GroupTreeTracer.FlattenParameterArray(parameters.Collection[i].Value),
					GroupTreeTracer.FlattenParameterArray(parameters.Collection[i].Label)
				});
			}
		}

		// Token: 0x0600485C RID: 18524 RVA: 0x00131E38 File Offset: 0x00130038
		private static string FlattenParameterArray(object parameterValueOrLabel)
		{
			if (parameterValueOrLabel == null)
			{
				return null;
			}
			ICollection collection = parameterValueOrLabel as ICollection;
			if (collection != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (object obj in collection)
				{
					stringBuilder.Append(GroupTreeTracer.GetObjectValue(obj));
					stringBuilder.Append("#");
				}
				return stringBuilder.ToString();
			}
			return GroupTreeTracer.GetObjectValue(parameterValueOrLabel);
		}

		// Token: 0x0600485D RID: 18525 RVA: 0x00131EC0 File Offset: 0x001300C0
		private static string FlattenObjectArray(object[] objects)
		{
			if (objects == null || objects.Length == 0)
			{
				return null;
			}
			if (objects.Length == 1)
			{
				return GroupTreeTracer.GetObjectValue(objects[0]);
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(GroupTreeTracer.GetObjectValue(objects[0]));
			for (int i = 1; i < objects.Length; i++)
			{
				stringBuilder.Append("#");
				stringBuilder.Append(GroupTreeTracer.GetObjectValue(objects[i]));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600485E RID: 18526 RVA: 0x00131F29 File Offset: 0x00130129
		private static string GetObjectValue(object obj)
		{
			if (obj == null)
			{
				return "";
			}
			return obj.ToString();
		}

		// Token: 0x04002042 RID: 8258
		private static List<string> m_strings = new List<string>();
	}
}
