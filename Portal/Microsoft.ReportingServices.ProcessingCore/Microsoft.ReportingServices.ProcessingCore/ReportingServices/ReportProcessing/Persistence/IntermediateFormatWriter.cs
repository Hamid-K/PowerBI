using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing.Persistence
{
	// Token: 0x020007A7 RID: 1959
	internal sealed class IntermediateFormatWriter
	{
		// Token: 0x06006C81 RID: 27777 RVA: 0x001B8626 File Offset: 0x001B6826
		internal IntermediateFormatWriter(Stream stream, bool[] firstPageDeclarationsToWrite, bool[] otherPageDeclarationsToWrite)
		{
			this.Initialize(stream, true, firstPageDeclarationsToWrite, otherPageDeclarationsToWrite);
		}

		// Token: 0x06006C82 RID: 27778 RVA: 0x001B863F File Offset: 0x001B683F
		internal IntermediateFormatWriter(Stream stream, bool writeDeclarations)
		{
			this.Initialize(stream, writeDeclarations, null, null);
		}

		// Token: 0x06006C83 RID: 27779 RVA: 0x001B8658 File Offset: 0x001B6858
		private void Initialize(Stream stream, bool writeDeclarations, bool[] firstPageDeclarationsToWrite, bool[] otherPageDeclarationsToWrite)
		{
			IntermediateFormatWriter.Assert(stream != null);
			this.m_writer = new IntermediateFormatWriter.ReportServerBinaryWriter(stream);
			this.m_writer.WriteBytes(VersionStamp.GetBytes());
			this.m_writeDeclarations = writeDeclarations;
			this.m_declarationsWritten = new bool[DeclarationList.Current.Count];
			this.WriteDeclarations(firstPageDeclarationsToWrite);
			this.WriteDeclarations(otherPageDeclarationsToWrite);
		}

		// Token: 0x06006C84 RID: 27780 RVA: 0x001B86B8 File Offset: 0x001B68B8
		private void WriteDeclarations(bool[] declarationsToWrite)
		{
			if (declarationsToWrite != null)
			{
				IntermediateFormatWriter.Assert(this.m_declarationsWritten.Length == declarationsToWrite.Length);
				for (int i = 0; i < declarationsToWrite.Length; i++)
				{
					if (declarationsToWrite[i])
					{
						this.DeclareType((ObjectType)i);
					}
				}
			}
		}

		// Token: 0x170025C5 RID: 9669
		// (get) Token: 0x06006C85 RID: 27781 RVA: 0x001B86F4 File Offset: 0x001B68F4
		internal bool[] DeclarationsToWrite
		{
			get
			{
				IntermediateFormatWriter.Assert(!this.m_writeDeclarations);
				return this.m_declarationsWritten;
			}
		}

		// Token: 0x06006C86 RID: 27782 RVA: 0x001B870C File Offset: 0x001B690C
		internal void WriteIntermediateFormatVersion(IntermediateFormatVersion version)
		{
			if (version == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.IntermediateFormatVersion;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteInt32(version.Major);
			this.m_writer.WriteInt32(version.Minor);
			this.m_writer.WriteInt32(version.Build);
			this.m_writer.EndObject();
		}

		// Token: 0x06006C87 RID: 27783 RVA: 0x001B8780 File Offset: 0x001B6980
		internal void WriteReport(Report report)
		{
			if (report == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.Report;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			report.IntermediateFormatVersion.SetCurrent();
			this.WriteIntermediateFormatVersion(report.IntermediateFormatVersion);
			this.m_writer.WriteGuid(report.ReportVersion);
			this.WriteReportItemBase(report);
			this.m_writer.WriteString(report.Author);
			this.m_writer.WriteInt32(report.AutoRefresh);
			this.WriteEmbeddedImageHashtable(report.EmbeddedImages);
			this.WritePageSection(report.PageHeader);
			this.WritePageSection(report.PageFooter);
			this.WriteReportItemCollection(report.ReportItems);
			this.WriteDataSourceList(report.DataSources);
			this.m_writer.WriteString(report.PageHeight);
			this.m_writer.WriteDouble(report.PageHeightValue);
			this.m_writer.WriteString(report.PageWidth);
			this.m_writer.WriteDouble(report.PageWidthValue);
			this.m_writer.WriteString(report.LeftMargin);
			this.m_writer.WriteDouble(report.LeftMarginValue);
			this.m_writer.WriteString(report.RightMargin);
			this.m_writer.WriteDouble(report.RightMarginValue);
			this.m_writer.WriteString(report.TopMargin);
			this.m_writer.WriteDouble(report.TopMarginValue);
			this.m_writer.WriteString(report.BottomMargin);
			this.m_writer.WriteDouble(report.BottomMarginValue);
			this.m_writer.WriteInt32(report.Columns);
			this.m_writer.WriteString(report.ColumnSpacing);
			this.m_writer.WriteDouble(report.ColumnSpacingValue);
			this.WriteDataAggregateInfoList(report.PageAggregates);
			this.m_writer.WriteBytes(report.CompiledCode);
			this.m_writer.WriteBoolean(report.MergeOnePass);
			this.m_writer.WriteBoolean(report.PageMergeOnePass);
			this.m_writer.WriteBoolean(report.SubReportMergeTransactions);
			this.m_writer.WriteBoolean(report.NeedPostGroupProcessing);
			this.m_writer.WriteBoolean(report.HasPostSortAggregates);
			this.m_writer.WriteBoolean(report.HasReportItemReferences);
			this.WriteShowHideTypes(report.ShowHideType);
			this.WriteImageStreamNames(report.ImageStreamNames);
			this.m_writer.WriteInt32(report.LastID);
			this.m_writer.WriteInt32(report.BodyID);
			this.WriteSubReportList(report.SubReports);
			this.m_writer.WriteBoolean(report.HasImageStreams);
			this.m_writer.WriteBoolean(report.HasLabels);
			this.m_writer.WriteBoolean(report.HasBookmarks);
			this.m_writer.WriteBoolean(report.ParametersNotUsedInQuery);
			this.WriteParameterDefList(report.Parameters);
			this.m_writer.WriteString(report.OneDataSetName);
			this.WriteStringList(report.CodeModules);
			this.WriteCodeClassList(report.CodeClasses);
			this.m_writer.WriteBoolean(report.HasSpecialRecursiveAggregates);
			this.WriteExpressionInfo(report.Language);
			this.m_writer.WriteString(report.DataTransform);
			this.m_writer.WriteString(report.DataSchema);
			this.m_writer.WriteBoolean(report.DataElementStyleAttribute);
			this.m_writer.WriteString(report.Code);
			this.m_writer.WriteBoolean(report.HasUserSortFilter);
			this.m_writer.WriteBoolean(report.CompiledCodeGeneratedWithRefusedPermissions);
			this.m_writer.WriteString(report.InteractiveHeight);
			this.m_writer.WriteDouble(report.InteractiveHeightValue);
			this.m_writer.WriteString(report.InteractiveWidth);
			this.m_writer.WriteDouble(report.InteractiveWidthValue);
			this.WriteInScopeSortFilterHashtable(report.NonDetailSortFiltersInScope);
			this.WriteInScopeSortFilterHashtable(report.DetailSortFiltersInScope);
			this.m_writer.EndObject();
		}

		// Token: 0x06006C88 RID: 27784 RVA: 0x001B8B6C File Offset: 0x001B6D6C
		internal void WriteReportSnapshot(ReportSnapshot reportSnapshot)
		{
			if (reportSnapshot == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ReportSnapshot;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteDateTime(reportSnapshot.ExecutionTime);
			this.WriteReport(reportSnapshot.Report);
			this.WriteParameterInfoCollection(reportSnapshot.Parameters);
			this.WriteReportInstance(reportSnapshot.ReportInstance);
			this.m_writer.WriteBoolean(reportSnapshot.HasDocumentMap);
			this.m_writer.WriteBoolean(reportSnapshot.HasShowHide);
			this.m_writer.WriteBoolean(reportSnapshot.HasBookmarks);
			this.m_writer.WriteBoolean(reportSnapshot.HasImageStreams);
			this.m_writer.WriteString(reportSnapshot.RequestUserName);
			this.m_writer.WriteString(reportSnapshot.ReportServerUrl);
			this.m_writer.WriteString(reportSnapshot.ReportFolder);
			this.m_writer.WriteString(reportSnapshot.Language);
			this.WriteProcessingMessageList(reportSnapshot.Warnings);
			this.WriteInt64List(reportSnapshot.PageSectionOffsets);
			this.m_writer.EndObject();
		}

		// Token: 0x06006C89 RID: 27785 RVA: 0x001B8C80 File Offset: 0x001B6E80
		internal void WriteDocumentMapNode(DocumentMapNode documentMapNode)
		{
			if (documentMapNode == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.DocumentMapNode;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteInstanceInfoBase(documentMapNode);
			this.m_writer.WriteString(documentMapNode.Id);
			this.m_writer.WriteString(documentMapNode.Label);
			this.m_writer.WriteInt32(documentMapNode.Page);
			this.WriteDocumentMapNodes(documentMapNode.Children);
			this.m_writer.EndObject();
		}

		// Token: 0x06006C8A RID: 27786 RVA: 0x001B8D04 File Offset: 0x001B6F04
		internal void WriteBookmarksHashtable(BookmarksHashtable bookmarks)
		{
			if (bookmarks == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.BookmarksHashtable;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(bookmarks.Count);
			int num = 0;
			IDictionaryEnumerator enumerator = bookmarks.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string text = (string)enumerator.Key;
				this.m_writer.WriteString(text);
				this.WriteBookmarkInformation((BookmarkInformation)enumerator.Value);
				num++;
			}
			IntermediateFormatWriter.Assert(num == bookmarks.Count);
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006C8B RID: 27787 RVA: 0x001B8DA8 File Offset: 0x001B6FA8
		internal void WriteDrillthroughInfo(ReportDrillthroughInfo reportDrillthroughInfo)
		{
			if (reportDrillthroughInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ReportDrillthroughInfo;
			this.m_writer.StartObject(objectType);
			this.WriteTokensHashtable(reportDrillthroughInfo.RewrittenCommands);
			this.WriteDrillthroughHashtable(reportDrillthroughInfo.DrillthroughInformation);
			this.m_writer.EndObject();
		}

		// Token: 0x06006C8C RID: 27788 RVA: 0x001B8DFC File Offset: 0x001B6FFC
		internal void WriteTokensHashtable(TokensHashtable tokens)
		{
			if (tokens == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.TokensHashtable;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(tokens.Count);
			int num = 0;
			IDictionaryEnumerator enumerator = tokens.GetEnumerator();
			while (enumerator.MoveNext())
			{
				int num2 = (int)enumerator.Key;
				this.m_writer.WriteInt32(num2);
				this.WriteVariant(enumerator.Value);
				num++;
			}
			IntermediateFormatWriter.Assert(num == tokens.Count);
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006C8D RID: 27789 RVA: 0x001B8E9C File Offset: 0x001B709C
		internal void WriteDrillthroughHashtable(DrillthroughHashtable drillthrough)
		{
			if (drillthrough == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.DrillthroughHashtable;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(drillthrough.Count);
			int num = 0;
			IDictionaryEnumerator enumerator = drillthrough.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string text = (string)enumerator.Key;
				this.m_writer.WriteString(text);
				this.WriteDrillthroughInformation((DrillthroughInformation)enumerator.Value);
				num++;
			}
			IntermediateFormatWriter.Assert(num == drillthrough.Count);
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006C8E RID: 27790 RVA: 0x001B8F40 File Offset: 0x001B7140
		internal void WriteSenderInformationHashtable(SenderInformationHashtable senders)
		{
			if (senders == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.SenderInformationHashtable;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(senders.Count);
			int num = 0;
			IDictionaryEnumerator enumerator = senders.GetEnumerator();
			while (enumerator.MoveNext())
			{
				int num2 = (int)enumerator.Key;
				this.m_writer.WriteInt32(num2);
				this.WriteSenderInformation((SenderInformation)enumerator.Value);
				num++;
			}
			IntermediateFormatWriter.Assert(num == senders.Count);
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006C8F RID: 27791 RVA: 0x001B8FE0 File Offset: 0x001B71E0
		internal void WriteReceiverInformationHashtable(ReceiverInformationHashtable receivers)
		{
			if (receivers == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ReceiverInformationHashtable;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(receivers.Count);
			int num = 0;
			IDictionaryEnumerator enumerator = receivers.GetEnumerator();
			while (enumerator.MoveNext())
			{
				int num2 = (int)enumerator.Key;
				this.m_writer.WriteInt32(num2);
				this.WriteReceiverInformation((ReceiverInformation)enumerator.Value);
				num++;
			}
			IntermediateFormatWriter.Assert(num == receivers.Count);
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006C90 RID: 27792 RVA: 0x001B9080 File Offset: 0x001B7280
		internal void WriteQuickFindHashtable(QuickFindHashtable quickFind)
		{
			if (quickFind == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.QuickFindHashtable;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(quickFind.Count);
			int num = 0;
			IDictionaryEnumerator enumerator = quickFind.GetEnumerator();
			while (enumerator.MoveNext())
			{
				int num2 = (int)enumerator.Key;
				this.m_writer.WriteInt32(num2);
				this.WriteReportItemInstanceReference((ReportItemInstance)enumerator.Value);
				num++;
			}
			IntermediateFormatWriter.Assert(num == quickFind.Count);
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006C91 RID: 27793 RVA: 0x001B9120 File Offset: 0x001B7320
		internal void WriteSortFilterEventInfoHashtable(SortFilterEventInfoHashtable sortFilterEventInfo)
		{
			if (sortFilterEventInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.SortFilterEventInfoHashtable;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(sortFilterEventInfo.Count);
			int num = 0;
			IDictionaryEnumerator enumerator = sortFilterEventInfo.GetEnumerator();
			while (enumerator.MoveNext())
			{
				int num2 = (int)enumerator.Key;
				this.m_writer.WriteInt32(num2);
				this.WriteSortFilterEventInfo((SortFilterEventInfo)enumerator.Value);
				num++;
			}
			IntermediateFormatWriter.Assert(num == sortFilterEventInfo.Count);
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006C92 RID: 27794 RVA: 0x001B91C4 File Offset: 0x001B73C4
		internal Int64List WritePageSections(Stream stream, List<PageSectionInstance> pageSections)
		{
			Int64List int64List = null;
			if (pageSections == null)
			{
				this.m_writer.WriteNull();
			}
			else
			{
				ObjectType objectType = ObjectType.PageSectionInstanceList;
				this.m_writer.StartObject(objectType);
				int count = pageSections.Count;
				IntermediateFormatWriter.Assert(count % 2 == 0);
				this.m_writer.StartArray(count);
				int64List = new Int64List(count / 2);
				for (int i = 0; i < count; i++)
				{
					if (i % 2 == 0)
					{
						int64List.Add(stream.Position);
					}
					this.WritePageSectionInstance(pageSections[i]);
				}
				this.m_writer.EndArray();
				this.m_writer.EndObject();
			}
			return int64List;
		}

		// Token: 0x06006C93 RID: 27795 RVA: 0x001B9264 File Offset: 0x001B7464
		internal void WriteSortFilterEventInfo(SortFilterEventInfo sortFilterEventInfo)
		{
			if (sortFilterEventInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.SortFilterEventInfo;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemReference(sortFilterEventInfo.EventSource);
			this.WriteVariantLists(sortFilterEventInfo.EventSourceScopeInfo, true);
			this.m_writer.EndObject();
		}

		// Token: 0x06006C94 RID: 27796 RVA: 0x001B92C0 File Offset: 0x001B74C0
		internal void WriteInstanceInfo(InstanceInfo instanceInfo)
		{
			if (instanceInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			if (instanceInfo is ReportItemInstanceInfo)
			{
				this.WriteReportItemInstanceInfo((ReportItemInstanceInfo)instanceInfo);
				return;
			}
			if (instanceInfo is SimpleTextBoxInstanceInfo)
			{
				this.WriteSimpleTextBoxInstanceInfo((SimpleTextBoxInstanceInfo)instanceInfo);
				return;
			}
			if (instanceInfo is ReportItemColInstanceInfo)
			{
				this.WriteReportItemColInstanceInfo((ReportItemColInstanceInfo)instanceInfo);
				return;
			}
			if (instanceInfo is ListContentInstanceInfo)
			{
				this.WriteListContentInstanceInfo((ListContentInstanceInfo)instanceInfo);
				return;
			}
			if (instanceInfo is TableRowInstanceInfo)
			{
				this.WriteTableRowInstanceInfo((TableRowInstanceInfo)instanceInfo);
				return;
			}
			if (instanceInfo is TableGroupInstanceInfo)
			{
				this.WriteTableGroupInstanceInfo((TableGroupInstanceInfo)instanceInfo);
				return;
			}
			if (instanceInfo is MatrixCellInstanceInfo)
			{
				this.WriteMatrixCellInstanceInfo((MatrixCellInstanceInfo)instanceInfo);
				return;
			}
			if (instanceInfo is MatrixSubtotalHeadingInstanceInfo)
			{
				this.WriteMatrixSubtotalHeadingInstanceInfo((MatrixSubtotalHeadingInstanceInfo)instanceInfo);
				return;
			}
			if (instanceInfo is TableDetailInstanceInfo)
			{
				this.WriteTableDetailInstanceInfo((TableDetailInstanceInfo)instanceInfo);
				return;
			}
			if (instanceInfo is ChartInstanceInfo)
			{
				this.WriteChartInstanceInfo((ChartInstanceInfo)instanceInfo);
				return;
			}
			if (instanceInfo is ChartHeadingInstanceInfo)
			{
				this.WriteChartHeadingInstanceInfo((ChartHeadingInstanceInfo)instanceInfo);
				return;
			}
			if (instanceInfo is ChartDataPointInstanceInfo)
			{
				this.WriteChartDataPointInstanceInfo((ChartDataPointInstanceInfo)instanceInfo);
				return;
			}
			IntermediateFormatWriter.Assert(instanceInfo is MatrixHeadingInstanceInfo);
			this.WriteMatrixHeadingInstanceInfo((MatrixHeadingInstanceInfo)instanceInfo);
		}

		// Token: 0x06006C95 RID: 27797 RVA: 0x001B93F4 File Offset: 0x001B75F4
		internal void WriteRecordSetInfo(RecordSetInfo recordSetInfo)
		{
			if (recordSetInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.RecordSetInfo;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteBoolean(recordSetInfo.ReaderExtensionsSupported);
			this.WriteRecordSetPropertyNamesList(recordSetInfo.FieldPropertyNames);
			this.m_writer.WriteEnum((int)recordSetInfo.CompareOptions);
			this.m_writer.EndObject();
		}

		// Token: 0x06006C96 RID: 27798 RVA: 0x001B9464 File Offset: 0x001B7664
		internal void WriteRecordSetPropertyNamesList(RecordSetPropertyNamesList list)
		{
			if (list == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.RecordSetPropertyNamesList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(list.Count);
			for (int i = 0; i < list.Count; i++)
			{
				this.WriteRecordSetPropertyNames(list[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006C97 RID: 27799 RVA: 0x001B94D8 File Offset: 0x001B76D8
		internal void WriteRecordSetPropertyNames(RecordSetPropertyNames field)
		{
			if (field == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.RecordSetPropertyNames;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteStringList(field.PropertyNames);
			this.m_writer.EndObject();
		}

		// Token: 0x06006C98 RID: 27800 RVA: 0x001B9524 File Offset: 0x001B7724
		internal bool WriteRecordRow(RecordRow recordRow, RecordSetPropertyNamesList aliasPropertyNames)
		{
			bool flag = true;
			if (recordRow == null)
			{
				this.m_writer.WriteNull();
			}
			else
			{
				ObjectType objectType = ObjectType.RecordRow;
				this.DeclareType(objectType);
				this.m_writer.StartObject(objectType);
				flag = this.WriteRecordFields(recordRow.RecordFields, aliasPropertyNames);
				this.m_writer.WriteBoolean(recordRow.IsAggregateRow);
				this.m_writer.WriteInt32(recordRow.AggregationFieldCount);
				this.m_writer.EndObject();
			}
			return flag;
		}

		// Token: 0x06006C99 RID: 27801 RVA: 0x001B9598 File Offset: 0x001B7798
		private static void Assert(bool condition)
		{
			Global.Tracer.Assert(condition);
		}

		// Token: 0x06006C9A RID: 27802 RVA: 0x001B95A5 File Offset: 0x001B77A5
		private void DeclareType(ObjectType objectType)
		{
			if (!this.m_declarationsWritten[(int)objectType])
			{
				if (this.m_writeDeclarations)
				{
					this.m_writer.DeclareType(objectType, DeclarationList.Current[objectType]);
				}
				this.m_declarationsWritten[(int)objectType] = true;
			}
		}

		// Token: 0x06006C9B RID: 27803 RVA: 0x001B95DC File Offset: 0x001B77DC
		private void WriteValidValueList(ValidValueList parameters)
		{
			if (parameters == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ValidValueList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(parameters.Count);
			for (int i = 0; i < parameters.Count; i++)
			{
				this.WriteValidValue(parameters[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006C9C RID: 27804 RVA: 0x001B9650 File Offset: 0x001B7850
		private void WriteParameterDefList(ParameterDefList parameters)
		{
			if (parameters == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ParameterDefList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(parameters.Count);
			for (int i = 0; i < parameters.Count; i++)
			{
				this.WriteParameterDef(parameters[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006C9D RID: 27805 RVA: 0x001B96C4 File Offset: 0x001B78C4
		private void WriteParameterDefRefList(ParameterDefList parameters)
		{
			if (parameters == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ParameterDefList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(parameters.Count);
			for (int i = 0; i < parameters.Count; i++)
			{
				this.m_writer.WriteString(parameters[i].Name);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006C9E RID: 27806 RVA: 0x001B9744 File Offset: 0x001B7944
		private void WriteParameterInfoCollection(ParameterInfoCollection parameters)
		{
			if (parameters == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ParameterInfoCollection;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(parameters.Count);
			for (int i = 0; i < parameters.Count; i++)
			{
				this.WriteParameterInfo(parameters[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006C9F RID: 27807 RVA: 0x001B97B4 File Offset: 0x001B79B4
		private void WriteParameterInfoRefCollection(ParameterInfoCollection parameters)
		{
			if (parameters == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ParameterInfoCollection;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(parameters.Count);
			for (int i = 0; i < parameters.Count; i++)
			{
				this.m_writer.WriteString(parameters[i].Name);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CA0 RID: 27808 RVA: 0x001B9830 File Offset: 0x001B7A30
		private void WriteFilterList(FilterList filters)
		{
			if (filters == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.FilterList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(filters.Count);
			for (int i = 0; i < filters.Count; i++)
			{
				this.WriteFilter(filters[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CA1 RID: 27809 RVA: 0x001B98A0 File Offset: 0x001B7AA0
		private void WriteDataSourceList(DataSourceList dataSources)
		{
			if (dataSources == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.DataSourceList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(dataSources.Count);
			for (int i = 0; i < dataSources.Count; i++)
			{
				this.WriteDataSource(dataSources[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CA2 RID: 27810 RVA: 0x001B9910 File Offset: 0x001B7B10
		private void WriteDataAggregateInfoList(DataAggregateInfoList aggregates)
		{
			if (aggregates == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.DataAggregateInfoList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(aggregates.Count);
			for (int i = 0; i < aggregates.Count; i++)
			{
				this.WriteDataAggregateInfo(aggregates[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CA3 RID: 27811 RVA: 0x001B9980 File Offset: 0x001B7B80
		private void WriteReportItemIDList(ReportItemList reportItems)
		{
			if (reportItems == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ReportItemList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(reportItems.Count);
			for (int i = 0; i < reportItems.Count; i++)
			{
				this.WriteReportItemID(reportItems[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CA4 RID: 27812 RVA: 0x001B99EF File Offset: 0x001B7BEF
		private void WriteReportItemID(ReportItem reportItem)
		{
			if (reportItem == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			IntermediateFormatWriter.Assert(reportItem is TextBox);
			this.m_writer.WriteReference(ObjectType.TextBox, reportItem.ID);
		}

		// Token: 0x06006CA5 RID: 27813 RVA: 0x001B9A24 File Offset: 0x001B7C24
		private void WriteReportItemList(ReportItemList reportItems)
		{
			if (reportItems == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ReportItemList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(reportItems.Count);
			for (int i = 0; i < reportItems.Count; i++)
			{
				this.WriteReportItem(reportItems[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CA6 RID: 27814 RVA: 0x001B9A94 File Offset: 0x001B7C94
		private void WriteReportItemIndexerList(ReportItemIndexerList reportItemIndexers)
		{
			if (reportItemIndexers == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ReportItemIndexerList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(reportItemIndexers.Count);
			for (int i = 0; i < reportItemIndexers.Count; i++)
			{
				this.WriteReportItemIndexer(reportItemIndexers[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CA7 RID: 27815 RVA: 0x001B9B04 File Offset: 0x001B7D04
		private void WriteRunningValueInfoList(RunningValueInfoList runningValues)
		{
			if (runningValues == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.RunningValueInfoList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(runningValues.Count);
			for (int i = 0; i < runningValues.Count; i++)
			{
				this.WriteRunningValueInfo(runningValues[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CA8 RID: 27816 RVA: 0x001B9B74 File Offset: 0x001B7D74
		private void WriteStyleAttributeHashtable(StyleAttributeHashtable styleAttributes)
		{
			if (styleAttributes == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.StyleAttributeHashtable;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(styleAttributes.Count);
			int num = 0;
			IDictionaryEnumerator enumerator = styleAttributes.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string text = (string)enumerator.Key;
				IntermediateFormatWriter.Assert(text != null);
				this.m_writer.WriteString(text);
				this.WriteAttributeInfo((AttributeInfo)enumerator.Value);
				num++;
			}
			IntermediateFormatWriter.Assert(num == styleAttributes.Count);
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CA9 RID: 27817 RVA: 0x001B9C20 File Offset: 0x001B7E20
		private void WriteImageInfo(ImageInfo imageInfo)
		{
			if (imageInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ImageInfo;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteString(imageInfo.StreamName);
			this.m_writer.WriteString(imageInfo.MimeType);
			this.m_writer.EndObject();
		}

		// Token: 0x06006CAA RID: 27818 RVA: 0x001B9C84 File Offset: 0x001B7E84
		private void WriteDrillthroughParameters(DrillthroughParameters parameters)
		{
			if (parameters == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.DrillthroughParameters;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(parameters.Count);
			for (int i = 0; i < parameters.Count; i++)
			{
				string key = parameters.GetKey(i);
				object value = parameters.GetValue(i);
				object[] array = null;
				IntermediateFormatWriter.Assert(key != null);
				this.m_writer.WriteString(key);
				if (value != null)
				{
					array = value as object[];
				}
				if (array != null)
				{
					this.WriteVariants(array, false);
				}
				else
				{
					this.WriteVariant(value);
				}
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CAB RID: 27819 RVA: 0x001B9D34 File Offset: 0x001B7F34
		private void WriteImageStreamNames(ImageStreamNames streamNames)
		{
			if (streamNames == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ImageStreamNames;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(streamNames.Count);
			int num = 0;
			IDictionaryEnumerator enumerator = streamNames.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string text = (string)enumerator.Key;
				IntermediateFormatWriter.Assert(text != null);
				this.m_writer.WriteString(text);
				this.WriteImageInfo((ImageInfo)enumerator.Value);
				num++;
			}
			IntermediateFormatWriter.Assert(num == streamNames.Count);
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CAC RID: 27820 RVA: 0x001B9DE0 File Offset: 0x001B7FE0
		private void WriteEmbeddedImageHashtable(EmbeddedImageHashtable embeddedImages)
		{
			if (embeddedImages == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.EmbeddedImageHashtable;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(embeddedImages.Count);
			int num = 0;
			IDictionaryEnumerator enumerator = embeddedImages.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string text = (string)enumerator.Key;
				IntermediateFormatWriter.Assert(text != null);
				this.m_writer.WriteString(text);
				this.WriteImageInfo((ImageInfo)enumerator.Value);
				num++;
			}
			IntermediateFormatWriter.Assert(num == embeddedImages.Count);
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CAD RID: 27821 RVA: 0x001B9E8C File Offset: 0x001B808C
		private void WriteExpressionInfoList(ExpressionInfoList expressions)
		{
			if (expressions == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ExpressionInfoList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(expressions.Count);
			for (int i = 0; i < expressions.Count; i++)
			{
				this.WriteExpressionInfo(expressions[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CAE RID: 27822 RVA: 0x001B9EFC File Offset: 0x001B80FC
		private void WriteDataSetList(DataSetList dataSets)
		{
			if (dataSets == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.DataSetList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(dataSets.Count);
			for (int i = 0; i < dataSets.Count; i++)
			{
				this.WriteDataSet(dataSets[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CAF RID: 27823 RVA: 0x001B9F6C File Offset: 0x001B816C
		private void WriteExpressionInfos(ExpressionInfo[] expressions)
		{
			if (expressions == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.StartArray(expressions.Length);
			for (int i = 0; i < expressions.Length; i++)
			{
				this.WriteExpressionInfo(expressions[i]);
			}
			this.m_writer.EndArray();
		}

		// Token: 0x06006CB0 RID: 27824 RVA: 0x001B9FB8 File Offset: 0x001B81B8
		private void WriteStringList(StringList strings)
		{
			if (strings == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.StringList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(strings.Count);
			for (int i = 0; i < strings.Count; i++)
			{
				this.m_writer.WriteString(strings[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CB1 RID: 27825 RVA: 0x001BA030 File Offset: 0x001B8230
		private void WriteDataFieldList(DataFieldList fields)
		{
			if (fields == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.DataFieldList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(fields.Count);
			for (int i = 0; i < fields.Count; i++)
			{
				this.WriteDataField(fields[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CB2 RID: 27826 RVA: 0x001BA0A0 File Offset: 0x001B82A0
		private void WriteDataRegionList(DataRegionList dataRegions)
		{
			if (dataRegions == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.DataRegionList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(dataRegions.Count);
			for (int i = 0; i < dataRegions.Count; i++)
			{
				this.WriteDataRegionReference(dataRegions[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CB3 RID: 27827 RVA: 0x001BA110 File Offset: 0x001B8310
		private void WriteParameterValueList(ParameterValueList parameters)
		{
			if (parameters == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ParameterValueList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(parameters.Count);
			for (int i = 0; i < parameters.Count; i++)
			{
				this.WriteParameterValue(parameters[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CB4 RID: 27828 RVA: 0x001BA180 File Offset: 0x001B8380
		private void WriteCodeClassList(CodeClassList classes)
		{
			if (classes == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.CodeClassList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(classes.Count);
			for (int i = 0; i < classes.Count; i++)
			{
				this.WriteCodeClass(classes[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CB5 RID: 27829 RVA: 0x001BA1F4 File Offset: 0x001B83F4
		private void WriteIntList(IntList ints)
		{
			if (ints == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.IntList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(ints.Count);
			for (int i = 0; i < ints.Count; i++)
			{
				this.m_writer.WriteInt32(ints[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CB6 RID: 27830 RVA: 0x001BA26C File Offset: 0x001B846C
		private void WriteInt64List(Int64List longs)
		{
			if (longs == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.Int64List;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(longs.Count);
			for (int i = 0; i < longs.Count; i++)
			{
				this.m_writer.WriteInt64(longs[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CB7 RID: 27831 RVA: 0x001BA2E4 File Offset: 0x001B84E4
		private void WriteBoolList(BoolList bools)
		{
			if (bools == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.BoolList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(bools.Count);
			for (int i = 0; i < bools.Count; i++)
			{
				this.m_writer.WriteBoolean(bools[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CB8 RID: 27832 RVA: 0x001BA35C File Offset: 0x001B855C
		private void WriteMatrixRowList(MatrixRowList rows)
		{
			if (rows == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.MatrixRowList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(rows.Count);
			for (int i = 0; i < rows.Count; i++)
			{
				this.WriteMatrixRow(rows[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CB9 RID: 27833 RVA: 0x001BA3CC File Offset: 0x001B85CC
		private void WriteMatrixColumnList(MatrixColumnList columns)
		{
			if (columns == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.MatrixColumnList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(columns.Count);
			for (int i = 0; i < columns.Count; i++)
			{
				this.WriteMatrixColumn(columns[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CBA RID: 27834 RVA: 0x001BA43C File Offset: 0x001B863C
		private void WriteTableColumnList(TableColumnList columns)
		{
			if (columns == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.TableColumnList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(columns.Count);
			for (int i = 0; i < columns.Count; i++)
			{
				this.WriteTableColumn(columns[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CBB RID: 27835 RVA: 0x001BA4AC File Offset: 0x001B86AC
		private void WriteTableRowList(TableRowList rows)
		{
			if (rows == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.TableRowList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(rows.Count);
			for (int i = 0; i < rows.Count; i++)
			{
				this.WriteTableRow(rows[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CBC RID: 27836 RVA: 0x001BA51C File Offset: 0x001B871C
		private void WriteChartColumnList(ChartColumnList columns)
		{
			if (columns == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ChartColumnList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(columns.Count);
			for (int i = 0; i < columns.Count; i++)
			{
				this.WriteChartColumn(columns[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CBD RID: 27837 RVA: 0x001BA58C File Offset: 0x001B878C
		private void WriteCustomReportItemHeadingList(CustomReportItemHeadingList headings)
		{
			if (headings == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.CustomReportItemHeadingList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(headings.Count);
			for (int i = 0; i < headings.Count; i++)
			{
				this.WriteCustomReportItemHeading(headings[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CBE RID: 27838 RVA: 0x001BA600 File Offset: 0x001B8800
		private void WriteDataCellsList(DataCellsList rows)
		{
			if (rows == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.DataCellsList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(rows.Count);
			for (int i = 0; i < rows.Count; i++)
			{
				this.WriteDataCellList(rows[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CBF RID: 27839 RVA: 0x001BA674 File Offset: 0x001B8874
		private void WriteDataCellList(DataCellList cells)
		{
			if (cells == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.DataCellList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(cells.Count);
			for (int i = 0; i < cells.Count; i++)
			{
				this.WriteDataValueCRIList(cells[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CC0 RID: 27840 RVA: 0x001BA6E8 File Offset: 0x001B88E8
		private void WriteDataValueCRIList(DataValueCRIList values)
		{
			if (values == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.DataValueCRIList;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteDataValueList(values);
			this.m_writer.WriteInt32(values.RDLRowIndex);
			this.m_writer.WriteInt32(values.RDLColumnIndex);
			this.m_writer.EndObject();
		}

		// Token: 0x06006CC1 RID: 27841 RVA: 0x001BA754 File Offset: 0x001B8954
		private void WriteDataValueList(DataValueList values)
		{
			if (values == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.DataValueList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(values.Count);
			for (int i = 0; i < values.Count; i++)
			{
				this.WriteDataValue(values[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CC2 RID: 27842 RVA: 0x001BA7C8 File Offset: 0x001B89C8
		private void WriteDataValueInstanceList(DataValueInstanceList instances)
		{
			if (instances == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.DataValueInstanceList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(instances.Count);
			for (int i = 0; i < instances.Count; i++)
			{
				this.WriteDataValueInstance(instances[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CC3 RID: 27843 RVA: 0x001BA83C File Offset: 0x001B8A3C
		private void WriteImageMapAreaInstanceList(ImageMapAreaInstanceList mapAreas)
		{
			if (mapAreas == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ImageMapAreaInstanceList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(mapAreas.Count);
			for (int i = 0; i < mapAreas.Count; i++)
			{
				this.WriteImageMapAreaInstance(mapAreas[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CC4 RID: 27844 RVA: 0x001BA8B0 File Offset: 0x001B8AB0
		private void WriteSubReportList(SubReportList subReports)
		{
			if (subReports == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.SubReportList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(subReports.Count);
			for (int i = 0; i < subReports.Count; i++)
			{
				this.WriteSubReportReference(subReports[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CC5 RID: 27845 RVA: 0x001BA923 File Offset: 0x001B8B23
		private void WriteSubReportReference(SubReport subReport)
		{
			if (subReport == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteReference(ObjectType.SubReport, subReport.ID);
		}

		// Token: 0x06006CC6 RID: 27846 RVA: 0x001BA948 File Offset: 0x001B8B48
		private void WriteNonComputedUniqueNamess(NonComputedUniqueNames[] names)
		{
			if (names == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.StartArray(names.Length);
			for (int i = 0; i < names.Length; i++)
			{
				this.WriteNonComputedUniqueNames(names[i]);
			}
			this.m_writer.EndArray();
		}

		// Token: 0x06006CC7 RID: 27847 RVA: 0x001BA994 File Offset: 0x001B8B94
		private void WriteReportItemInstanceList(ReportItemInstanceList reportItemInstances)
		{
			if (reportItemInstances == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ReportItemInstanceList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(reportItemInstances.Count);
			for (int i = 0; i < reportItemInstances.Count; i++)
			{
				this.WriteReportItemInstance(reportItemInstances[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CC8 RID: 27848 RVA: 0x001BAA04 File Offset: 0x001B8C04
		private void WriteRenderingPagesRangesList(RenderingPagesRangesList renderingPagesRanges)
		{
			if (renderingPagesRanges == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.RenderingPagesRangesList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(renderingPagesRanges.Count);
			for (int i = 0; i < renderingPagesRanges.Count; i++)
			{
				this.WriteRenderingPagesRanges(renderingPagesRanges[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CC9 RID: 27849 RVA: 0x001BAA78 File Offset: 0x001B8C78
		private void WriteListContentInstanceList(ListContentInstanceList listContents)
		{
			if (listContents == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ListContentInstanceList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(listContents.Count);
			for (int i = 0; i < listContents.Count; i++)
			{
				this.WriteListContentInstance(listContents[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CCA RID: 27850 RVA: 0x001BAAE8 File Offset: 0x001B8CE8
		private void WriteMatrixHeadingInstanceList(MatrixHeadingInstanceList matrixheadingInstances)
		{
			if (matrixheadingInstances == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.MatrixHeadingInstanceList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(matrixheadingInstances.Count);
			for (int i = 0; i < matrixheadingInstances.Count; i++)
			{
				this.WriteMatrixHeadingInstance(matrixheadingInstances[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CCB RID: 27851 RVA: 0x001BAB58 File Offset: 0x001B8D58
		private void WriteMatrixCellInstancesList(MatrixCellInstancesList matrixCellInstancesList)
		{
			if (matrixCellInstancesList == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.MatrixCellInstancesList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(matrixCellInstancesList.Count);
			for (int i = 0; i < matrixCellInstancesList.Count; i++)
			{
				this.WriteMatrixCellInstanceList(matrixCellInstancesList[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CCC RID: 27852 RVA: 0x001BABC8 File Offset: 0x001B8DC8
		private void WriteMatrixCellInstanceList(MatrixCellInstanceList matrixCellInstances)
		{
			if (matrixCellInstances == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.MatrixCellInstanceList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(matrixCellInstances.Count);
			for (int i = 0; i < matrixCellInstances.Count; i++)
			{
				MatrixSubtotalCellInstance matrixSubtotalCellInstance = matrixCellInstances[i] as MatrixSubtotalCellInstance;
				if (matrixSubtotalCellInstance != null)
				{
					this.WriteMatrixSubtotalCellInstance(matrixSubtotalCellInstance);
				}
				else
				{
					this.WriteMatrixCellInstance(matrixCellInstances[i]);
				}
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CCD RID: 27853 RVA: 0x001BAC54 File Offset: 0x001B8E54
		private void WriteMultiChartInstanceList(MultiChartInstanceList multichartInstances)
		{
			if (multichartInstances == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.MultiChartInstanceList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(multichartInstances.Count);
			for (int i = 0; i < multichartInstances.Count; i++)
			{
				this.WriteMultiChartInstance(multichartInstances[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CCE RID: 27854 RVA: 0x001BACC8 File Offset: 0x001B8EC8
		private void WriteChartHeadingInstanceList(ChartHeadingInstanceList chartheadingInstances)
		{
			if (chartheadingInstances == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ChartHeadingInstanceList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(chartheadingInstances.Count);
			for (int i = 0; i < chartheadingInstances.Count; i++)
			{
				this.WriteChartHeadingInstance(chartheadingInstances[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CCF RID: 27855 RVA: 0x001BAD3C File Offset: 0x001B8F3C
		private void WriteChartDataPointInstancesList(ChartDataPointInstancesList chartDataPointInstancesList)
		{
			if (chartDataPointInstancesList == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ChartDataPointInstancesList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(chartDataPointInstancesList.Count);
			for (int i = 0; i < chartDataPointInstancesList.Count; i++)
			{
				this.WriteChartDataPointInstanceList(chartDataPointInstancesList[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CD0 RID: 27856 RVA: 0x001BADB0 File Offset: 0x001B8FB0
		private void WriteChartDataPointInstanceList(ChartDataPointInstanceList chartDataPointInstanceList)
		{
			if (chartDataPointInstanceList == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ChartDataPointInstanceList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(chartDataPointInstanceList.Count);
			for (int i = 0; i < chartDataPointInstanceList.Count; i++)
			{
				this.WriteChartDataPointInstance(chartDataPointInstanceList[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CD1 RID: 27857 RVA: 0x001BAE24 File Offset: 0x001B9024
		private void WriteTableRowInstances(TableRowInstance[] tableRowInstances)
		{
			if (tableRowInstances == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.StartArray(tableRowInstances.Length);
			for (int i = 0; i < tableRowInstances.Length; i++)
			{
				this.WriteTableRowInstance(tableRowInstances[i]);
			}
			this.m_writer.EndArray();
		}

		// Token: 0x06006CD2 RID: 27858 RVA: 0x001BAE70 File Offset: 0x001B9070
		private void WriteTableDetailInstanceList(TableDetailInstanceList tableDetailInstanceList)
		{
			if (tableDetailInstanceList == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.TableDetailInstanceList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(tableDetailInstanceList.Count);
			for (int i = 0; i < tableDetailInstanceList.Count; i++)
			{
				this.WriteTableDetailInstance(tableDetailInstanceList[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CD3 RID: 27859 RVA: 0x001BAEE4 File Offset: 0x001B90E4
		private void WriteTableGroupInstanceList(TableGroupInstanceList tableGroupInstances)
		{
			if (tableGroupInstances == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.TableGroupInstanceList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(tableGroupInstances.Count);
			for (int i = 0; i < tableGroupInstances.Count; i++)
			{
				this.WriteTableGroupInstance(tableGroupInstances[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CD4 RID: 27860 RVA: 0x001BAF54 File Offset: 0x001B9154
		private void WriteTableColumnInstances(TableColumnInstance[] tableColumnInstances)
		{
			if (tableColumnInstances == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.StartArray(tableColumnInstances.Length);
			for (int i = 0; i < tableColumnInstances.Length; i++)
			{
				this.WriteTableColumnInstance(tableColumnInstances[i]);
			}
			this.m_writer.EndArray();
		}

		// Token: 0x06006CD5 RID: 27861 RVA: 0x001BAFA0 File Offset: 0x001B91A0
		private void WriteCustomReportItemHeadingInstanceList(CustomReportItemHeadingInstanceList headingInstances)
		{
			if (headingInstances == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.CustomReportItemHeadingInstanceList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(headingInstances.Count);
			for (int i = 0; i < headingInstances.Count; i++)
			{
				this.WriteCustomReportItemHeadingInstance(headingInstances[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CD6 RID: 27862 RVA: 0x001BB014 File Offset: 0x001B9214
		private void WriteCustomReportItemCellInstancesList(CustomReportItemCellInstancesList cellInstancesList)
		{
			if (cellInstancesList == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.CustomReportItemCellInstancesList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(cellInstancesList.Count);
			for (int i = 0; i < cellInstancesList.Count; i++)
			{
				this.WriteCustomReportItemCellInstanceList(cellInstancesList[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CD7 RID: 27863 RVA: 0x001BB088 File Offset: 0x001B9288
		private void WriteCustomReportItemCellInstanceList(CustomReportItemCellInstanceList cellInstances)
		{
			if (cellInstances == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.CustomReportItemCellInstanceList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(cellInstances.Count);
			for (int i = 0; i < cellInstances.Count; i++)
			{
				this.WriteCustomReportItemCellInstance(cellInstances[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CD8 RID: 27864 RVA: 0x001BB0FC File Offset: 0x001B92FC
		private void WriteDocumentMapNodes(DocumentMapNode[] nodes)
		{
			if (nodes == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.StartArray(nodes.Length);
			for (int i = 0; i < nodes.Length; i++)
			{
				this.WriteDocumentMapNode(nodes[i]);
			}
			this.m_writer.EndArray();
		}

		// Token: 0x06006CD9 RID: 27865 RVA: 0x001BB148 File Offset: 0x001B9348
		private void WriteStrings(string[] strings)
		{
			if (strings == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.StartArray(strings.Length);
			for (int i = 0; i < strings.Length; i++)
			{
				this.m_writer.WriteString(strings[i]);
			}
			this.m_writer.EndArray();
		}

		// Token: 0x06006CDA RID: 27866 RVA: 0x001BB199 File Offset: 0x001B9399
		private void WriteVariants(object[] variants)
		{
			this.WriteVariants(variants, false);
		}

		// Token: 0x06006CDB RID: 27867 RVA: 0x001BB1A4 File Offset: 0x001B93A4
		private void WriteVariants(object[] variants, bool isMultiValue)
		{
			if (variants == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.StartArray(variants.Length);
			for (int i = 0; i < variants.Length; i++)
			{
				if (isMultiValue && variants[i] is object[])
				{
					this.WriteVariants(variants[i] as object[], false);
				}
				else
				{
					this.WriteVariant(variants[i]);
				}
			}
			this.m_writer.EndArray();
		}

		// Token: 0x06006CDC RID: 27868 RVA: 0x001BB210 File Offset: 0x001B9410
		private void WriteVariantList(VariantList variants, bool convertDBNull)
		{
			if (variants == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.VariantList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(variants.Count);
			for (int i = 0; i < variants.Count; i++)
			{
				this.WriteVariant(variants[i], convertDBNull);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CDD RID: 27869 RVA: 0x001BB284 File Offset: 0x001B9484
		private void WriteVariantLists(VariantList[] variantLists, bool convertDBNull)
		{
			if (variantLists == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.StartArray(variantLists.Length);
			for (int i = 0; i < variantLists.Length; i++)
			{
				this.WriteVariantList(variantLists[i], convertDBNull);
			}
			this.m_writer.EndArray();
		}

		// Token: 0x06006CDE RID: 27870 RVA: 0x001BB2D4 File Offset: 0x001B94D4
		private void WriteProcessingMessageList(ProcessingMessageList messages)
		{
			if (messages == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ProcessingMessageList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(messages.Count);
			for (int i = 0; i < messages.Count; i++)
			{
				this.WriteProcessingMessage(messages[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CDF RID: 27871 RVA: 0x001BB348 File Offset: 0x001B9548
		private void WriteIDOwnerBase(IDOwner idOwner)
		{
			IntermediateFormatWriter.Assert(idOwner != null);
			ObjectType objectType = ObjectType.IDOwner;
			this.DeclareType(objectType);
			this.m_writer.WriteInt32(idOwner.ID);
		}

		// Token: 0x06006CE0 RID: 27872 RVA: 0x001BB378 File Offset: 0x001B9578
		private void WriteReportItem(ReportItem reportItem)
		{
			if (reportItem == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			if (reportItem is Line)
			{
				this.WriteLine((Line)reportItem);
				return;
			}
			if (reportItem is Rectangle)
			{
				this.WriteRectangle((Rectangle)reportItem);
				return;
			}
			if (reportItem is Image)
			{
				this.WriteImage((Image)reportItem);
				return;
			}
			if (reportItem is CheckBox)
			{
				this.WriteCheckBox((CheckBox)reportItem);
				return;
			}
			if (reportItem is TextBox)
			{
				this.WriteTextBox((TextBox)reportItem);
				return;
			}
			if (reportItem is SubReport)
			{
				this.WriteSubReport((SubReport)reportItem);
				return;
			}
			if (reportItem is ActiveXControl)
			{
				this.WriteActiveXControl((ActiveXControl)reportItem);
				return;
			}
			IntermediateFormatWriter.Assert(reportItem is DataRegion);
			this.WriteDataRegion((DataRegion)reportItem);
		}

		// Token: 0x06006CE1 RID: 27873 RVA: 0x001BB444 File Offset: 0x001B9644
		private void WriteReportItemBase(ReportItem reportItem)
		{
			IntermediateFormatWriter.Assert(reportItem != null);
			ObjectType objectType = ObjectType.ReportItem;
			this.DeclareType(objectType);
			this.WriteIDOwnerBase(reportItem);
			this.m_writer.WriteString(reportItem.Name);
			this.WriteStyle(reportItem.StyleClass);
			this.m_writer.WriteString(reportItem.Top);
			this.m_writer.WriteDouble(reportItem.TopValue);
			this.m_writer.WriteString(reportItem.Left);
			this.m_writer.WriteDouble(reportItem.LeftValue);
			this.m_writer.WriteString(reportItem.Height);
			this.m_writer.WriteDouble(reportItem.HeightValue);
			this.m_writer.WriteString(reportItem.Width);
			this.m_writer.WriteDouble(reportItem.WidthValue);
			this.m_writer.WriteInt32(reportItem.ZIndex);
			this.WriteVisibility(reportItem.Visibility);
			this.WriteExpressionInfo(reportItem.ToolTip);
			this.WriteExpressionInfo(reportItem.Label);
			this.WriteExpressionInfo(reportItem.Bookmark);
			this.m_writer.WriteString(reportItem.Custom);
			this.m_writer.WriteBoolean(reportItem.RepeatedSibling);
			this.m_writer.WriteBoolean(reportItem.IsFullSize);
			this.m_writer.WriteInt32(reportItem.ExprHostID);
			this.m_writer.WriteString(reportItem.DataElementName);
			this.WriteDataElementOutputType(reportItem.DataElementOutput);
			this.m_writer.WriteInt32(reportItem.DistanceFromReportTop);
			this.m_writer.WriteInt32(reportItem.DistanceBeforeTop);
			this.WriteIntList(reportItem.SiblingAboveMe);
			this.WriteDataValueList(reportItem.CustomProperties);
		}

		// Token: 0x06006CE2 RID: 27874 RVA: 0x001BB5EB File Offset: 0x001B97EB
		private void WriteReportItemReference(ReportItem reportItem)
		{
			if (reportItem == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteNoTypeReference(reportItem.ID);
		}

		// Token: 0x06006CE3 RID: 27875 RVA: 0x001BB610 File Offset: 0x001B9810
		private void WritePageSection(PageSection pageSection)
		{
			if (pageSection == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.PageSection;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemBase(pageSection);
			this.m_writer.WriteBoolean(pageSection.PrintOnFirstPage);
			this.m_writer.WriteBoolean(pageSection.PrintOnLastPage);
			this.WriteReportItemCollection(pageSection.ReportItems);
			this.m_writer.WriteBoolean(pageSection.PostProcessEvaluate);
			this.m_writer.EndObject();
		}

		// Token: 0x06006CE4 RID: 27876 RVA: 0x001BB694 File Offset: 0x001B9894
		private void WriteReportItemCollection(ReportItemCollection reportItems)
		{
			if (reportItems == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ReportItemCollection;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteIDOwnerBase(reportItems);
			this.WriteReportItemList(reportItems.NonComputedReportItems);
			this.WriteReportItemList(reportItems.ComputedReportItems);
			this.WriteReportItemIndexerList(reportItems.SortedReportItems);
			this.WriteRunningValueInfoList(reportItems.RunningValues);
			this.m_writer.EndObject();
		}

		// Token: 0x06006CE5 RID: 27877 RVA: 0x001BB708 File Offset: 0x001B9908
		private void WriteShowHideTypes(Report.ShowHideTypes showHideType)
		{
			this.m_writer.WriteEnum((int)showHideType);
		}

		// Token: 0x06006CE6 RID: 27878 RVA: 0x001BB716 File Offset: 0x001B9916
		private void WriteDataElementOutputType(DataElementOutputTypes element)
		{
			this.m_writer.WriteEnum((int)element);
		}

		// Token: 0x06006CE7 RID: 27879 RVA: 0x001BB724 File Offset: 0x001B9924
		private void WriteStyle(Style style)
		{
			if (style == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.Style;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteStyleAttributeHashtable(style.StyleAttributes);
			this.WriteExpressionInfoList(style.ExpressionList);
			this.m_writer.EndObject();
		}

		// Token: 0x06006CE8 RID: 27880 RVA: 0x001BB77C File Offset: 0x001B997C
		private void WriteVisibility(Visibility visibility)
		{
			if (visibility == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.Visibility;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteExpressionInfo(visibility.Hidden);
			this.m_writer.WriteString(visibility.Toggle);
			this.m_writer.WriteBoolean(visibility.RecursiveReceiver);
			this.m_writer.EndObject();
		}

		// Token: 0x06006CE9 RID: 27881 RVA: 0x001BB7E8 File Offset: 0x001B99E8
		private void WriteFilter(Filter filter)
		{
			if (filter == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.Filter;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteExpressionInfo(filter.Expression);
			this.WriteOperators(filter.Operator);
			this.WriteExpressionInfoList(filter.Values);
			this.m_writer.WriteInt32(filter.ExprHostID);
			this.m_writer.EndObject();
		}

		// Token: 0x06006CEA RID: 27882 RVA: 0x001BB85A File Offset: 0x001B9A5A
		private void WriteOperators(Filter.Operators operators)
		{
			this.m_writer.WriteEnum((int)operators);
		}

		// Token: 0x06006CEB RID: 27883 RVA: 0x001BB868 File Offset: 0x001B9A68
		private void WriteDataSource(DataSource dataSource)
		{
			if (dataSource == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.DataSource;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteString(dataSource.Name);
			this.m_writer.WriteBoolean(dataSource.Transaction);
			this.m_writer.WriteString(dataSource.Type);
			this.WriteExpressionInfo(dataSource.ConnectStringExpression);
			this.m_writer.WriteBoolean(dataSource.IntegratedSecurity);
			this.m_writer.WriteString(dataSource.Prompt);
			this.m_writer.WriteString(dataSource.DataSourceReference);
			this.WriteDataSetList(dataSource.DataSets);
			this.m_writer.WriteGuid(dataSource.ID);
			this.m_writer.WriteInt32(dataSource.ExprHostID);
			this.m_writer.WriteString(dataSource.SharedDataSourceReferencePath);
			this.m_writer.EndObject();
		}

		// Token: 0x06006CEC RID: 27884 RVA: 0x001BB958 File Offset: 0x001B9B58
		private void WriteDataAggregateInfo(DataAggregateInfo aggregate)
		{
			if (aggregate == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			if (aggregate is RunningValueInfo)
			{
				this.WriteRunningValueInfo((RunningValueInfo)aggregate);
				return;
			}
			ObjectType objectType = ObjectType.DataAggregateInfo;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteDataAggregateInfoBase(aggregate);
			this.m_writer.EndObject();
		}

		// Token: 0x06006CED RID: 27885 RVA: 0x001BB9B4 File Offset: 0x001B9BB4
		private void WriteDataAggregateInfoBase(DataAggregateInfo aggregate)
		{
			IntermediateFormatWriter.Assert(aggregate != null);
			ObjectType objectType = ObjectType.DataAggregateInfo;
			this.DeclareType(objectType);
			this.m_writer.WriteString(aggregate.Name);
			this.WriteAggregateTypes(aggregate.AggregateType);
			this.WriteExpressionInfos(aggregate.Expressions);
			this.WriteStringList(aggregate.DuplicateNames);
		}

		// Token: 0x06006CEE RID: 27886 RVA: 0x001BBA0C File Offset: 0x001B9C0C
		private void WriteExpressionInfo(ExpressionInfo expression)
		{
			if (expression == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ExpressionInfo;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteTypes(expression.Type);
			this.m_writer.WriteString(expression.Value);
			this.m_writer.WriteBoolean(expression.BoolValue);
			this.m_writer.WriteInt32(expression.IntValue);
			this.m_writer.WriteInt32(expression.ExprHostID);
			this.m_writer.WriteString(expression.OriginalText);
			this.m_writer.EndObject();
		}

		// Token: 0x06006CEF RID: 27887 RVA: 0x001BBAAA File Offset: 0x001B9CAA
		private void WriteAggregateTypes(DataAggregateInfo.AggregateTypes aggregateType)
		{
			this.m_writer.WriteEnum((int)aggregateType);
		}

		// Token: 0x06006CF0 RID: 27888 RVA: 0x001BBAB8 File Offset: 0x001B9CB8
		private void WriteTypes(ExpressionInfo.Types type)
		{
			this.m_writer.WriteEnum((int)type);
		}

		// Token: 0x06006CF1 RID: 27889 RVA: 0x001BBAC8 File Offset: 0x001B9CC8
		private void WriteReportItemIndexer(ReportItemIndexer reportItemIndexer)
		{
			ObjectType objectType = ObjectType.ReportItemIndexer;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteBoolean(reportItemIndexer.IsComputed);
			this.m_writer.WriteInt32(reportItemIndexer.Index);
			this.m_writer.EndObject();
		}

		// Token: 0x06006CF2 RID: 27890 RVA: 0x001BBB18 File Offset: 0x001B9D18
		private void WriteRenderingPagesRanges(RenderingPagesRanges renderingPagesRanges)
		{
			ObjectType objectType = ObjectType.RenderingPagesRanges;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteInt32(renderingPagesRanges.StartPage);
			this.m_writer.WriteInt32(renderingPagesRanges.EndPage);
			this.m_writer.EndObject();
		}

		// Token: 0x06006CF3 RID: 27891 RVA: 0x001BBB70 File Offset: 0x001B9D70
		private void WriteRunningValueInfo(RunningValueInfo runningValue)
		{
			if (runningValue == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.RunningValueInfo;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteDataAggregateInfoBase(runningValue);
			this.m_writer.WriteString(runningValue.Scope);
			this.m_writer.EndObject();
		}

		// Token: 0x06006CF4 RID: 27892 RVA: 0x001BBBC8 File Offset: 0x001B9DC8
		private void WriteAttributeInfo(AttributeInfo attributeInfo)
		{
			if (attributeInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.AttributeInfo;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteBoolean(attributeInfo.IsExpression);
			this.m_writer.WriteString(attributeInfo.Value);
			this.m_writer.WriteBoolean(attributeInfo.BoolValue);
			this.m_writer.WriteInt32(attributeInfo.IntValue);
			this.m_writer.EndObject();
		}

		// Token: 0x06006CF5 RID: 27893 RVA: 0x001BBC4C File Offset: 0x001B9E4C
		private void WriteDataSet(DataSet dataSet)
		{
			if (dataSet == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.DataSet;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			Global.Tracer.Assert(dataSet.ID > 0);
			this.WriteIDOwnerBase(dataSet);
			this.m_writer.WriteString(dataSet.Name);
			dataSet.PopulateReferencedFieldProperties();
			this.WriteDataFieldList(dataSet.Fields);
			this.WriteReportQuery(dataSet.Query);
			this.WriteSensitivity(dataSet.CaseSensitivity);
			this.m_writer.WriteString(dataSet.Collation);
			this.WriteSensitivity(dataSet.AccentSensitivity);
			this.WriteSensitivity(dataSet.KanatypeSensitivity);
			this.WriteSensitivity(dataSet.WidthSensitivity);
			this.WriteDataRegionList(dataSet.DataRegions);
			this.WriteDataAggregateInfoList(dataSet.Aggregates);
			this.WriteFilterList(dataSet.Filters);
			this.m_writer.WriteInt32(dataSet.RecordSetSize);
			this.m_writer.WriteBoolean(dataSet.UsedOnlyInParameters);
			this.m_writer.WriteInt32(dataSet.NonCalculatedFieldCount);
			this.m_writer.WriteInt32(dataSet.ExprHostID);
			this.WriteDataAggregateInfoList(dataSet.PostSortAggregates);
			this.m_writer.WriteInt32((int)dataSet.LCID);
			this.m_writer.WriteBoolean(dataSet.HasDetailUserSortFilter);
			this.WriteExpressionInfoList(dataSet.UserSortExpressions);
			this.m_writer.WriteBoolean(dataSet.DynamicFieldReferences);
			if (dataSet.InterpretSubtotalsAsDetailsIsAuto)
			{
				dataSet.InterpretSubtotalsAsDetails = true;
			}
			this.m_writer.WriteBoolean(dataSet.InterpretSubtotalsAsDetails);
			this.m_writer.EndObject();
		}

		// Token: 0x06006CF6 RID: 27894 RVA: 0x001BBDE8 File Offset: 0x001B9FE8
		private void WriteReportQuery(ReportQuery query)
		{
			if (query == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ReportQuery;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteCommandType(query.CommandType);
			this.WriteExpressionInfo(query.CommandText);
			this.WriteParameterValueList(query.Parameters);
			this.m_writer.WriteInt32(query.TimeOut);
			this.m_writer.WriteString(query.CommandTextValue);
			this.m_writer.WriteString(query.RewrittenCommandText);
			this.m_writer.EndObject();
		}

		// Token: 0x06006CF7 RID: 27895 RVA: 0x001BBE7C File Offset: 0x001BA07C
		private void WriteSensitivity(DataSet.Sensitivity sensitivity)
		{
			this.m_writer.WriteEnum((int)sensitivity);
		}

		// Token: 0x06006CF8 RID: 27896 RVA: 0x001BBE8A File Offset: 0x001BA08A
		private void WriteCommandType(CommandType commandType)
		{
			this.m_writer.WriteEnum((int)commandType);
		}

		// Token: 0x06006CF9 RID: 27897 RVA: 0x001BBE98 File Offset: 0x001BA098
		private void WriteDataField(Field field)
		{
			if (field == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.Field;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteString(field.Name);
			this.m_writer.WriteString(field.DataField);
			this.WriteExpressionInfo(field.Value);
			this.m_writer.WriteInt32(field.ExprHostID);
			this.m_writer.WriteBoolean(field.DynamicPropertyReferences);
			this.WriteFieldPropertyHashtable(field.ReferencedProperties);
			this.m_writer.EndObject();
		}

		// Token: 0x06006CFA RID: 27898 RVA: 0x001BBF34 File Offset: 0x001BA134
		internal void WriteFieldPropertyHashtable(FieldPropertyHashtable properties)
		{
			if (properties == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.FieldPropertyHashtable;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(properties.Count);
			int num = 0;
			IDictionaryEnumerator enumerator = properties.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string text = enumerator.Key as string;
				this.m_writer.WriteString(text);
				num++;
			}
			IntermediateFormatWriter.Assert(num == properties.Count);
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CFB RID: 27899 RVA: 0x001BBFC8 File Offset: 0x001BA1C8
		private void WriteParameterValue(ParameterValue parameter)
		{
			ObjectType objectType = ObjectType.ParameterValue;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteString(parameter.Name);
			this.WriteExpressionInfo(parameter.Value);
			this.m_writer.WriteInt32(parameter.ExprHostID);
			this.WriteExpressionInfo(parameter.Omit);
			this.m_writer.EndObject();
		}

		// Token: 0x06006CFC RID: 27900 RVA: 0x001BC030 File Offset: 0x001BA230
		private void WriteCodeClass(CodeClass codeClass)
		{
			ObjectType objectType = ObjectType.CodeClass;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteString(codeClass.ClassName);
			this.m_writer.WriteString(codeClass.InstanceName);
			this.m_writer.EndObject();
		}

		// Token: 0x06006CFD RID: 27901 RVA: 0x001BC084 File Offset: 0x001BA284
		private void WriteAction(Microsoft.ReportingServices.ReportProcessing.Action actionInfo)
		{
			if (actionInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.Action;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteActionItemList(actionInfo.ActionItems);
			this.WriteStyle(actionInfo.StyleClass);
			this.m_writer.WriteInt32(actionInfo.ComputedActionItemsCount);
			this.m_writer.EndObject();
		}

		// Token: 0x06006CFE RID: 27902 RVA: 0x001BC0F0 File Offset: 0x001BA2F0
		private void WriteActionItemList(ActionItemList actionItemList)
		{
			if (actionItemList == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ActionItemList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(actionItemList.Count);
			for (int i = 0; i < actionItemList.Count; i++)
			{
				this.WriteActionItem(actionItemList[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006CFF RID: 27903 RVA: 0x001BC164 File Offset: 0x001BA364
		private void WriteActionItem(ActionItem actionItem)
		{
			if (actionItem == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ActionItem;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteExpressionInfo(actionItem.HyperLinkURL);
			this.WriteExpressionInfo(actionItem.DrillthroughReportName);
			this.WriteParameterValueList(actionItem.DrillthroughParameters);
			this.WriteExpressionInfo(actionItem.DrillthroughBookmarkLink);
			this.WriteExpressionInfo(actionItem.BookmarkLink);
			this.WriteExpressionInfo(actionItem.Label);
			this.m_writer.WriteInt32(actionItem.ExprHostID);
			this.m_writer.WriteInt32(actionItem.ComputedIndex);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D00 RID: 27904 RVA: 0x001BC210 File Offset: 0x001BA410
		private void WriteLine(Line line)
		{
			if (line == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.Line;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemBase(line);
			this.m_writer.WriteBoolean(line.LineSlant);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D01 RID: 27905 RVA: 0x001BC264 File Offset: 0x001BA464
		private void WriteRectangle(Rectangle rectangle)
		{
			if (rectangle == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.Rectangle;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemBase(rectangle);
			this.WriteReportItemCollection(rectangle.ReportItems);
			this.m_writer.WriteBoolean(rectangle.PageBreakAtEnd);
			this.m_writer.WriteBoolean(rectangle.PageBreakAtStart);
			this.m_writer.WriteInt32(rectangle.LinkToChild);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D02 RID: 27906 RVA: 0x001BC2E8 File Offset: 0x001BA4E8
		private void WriteImage(Image image)
		{
			if (image == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.Image;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemBase(image);
			this.WriteAction(image.Action);
			this.WriteSourceType(image.Source);
			this.WriteExpressionInfo(image.Value);
			this.WriteExpressionInfo(image.MIMEType);
			this.WriteSizings(image.Sizing);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D03 RID: 27907 RVA: 0x001BC368 File Offset: 0x001BA568
		private void WriteImageMapAreaInstance(ImageMapAreaInstance mapArea)
		{
			if (mapArea == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ImageMapAreaInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteString(mapArea.ID);
			this.WriteImageMapAreaShape(mapArea.Shape);
			this.m_writer.WriteFloatArray(mapArea.Coordinates);
			this.WriteAction(mapArea.Action);
			this.WriteActionInstance(mapArea.ActionInstance);
			this.m_writer.WriteInt32(mapArea.UniqueName);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D04 RID: 27908 RVA: 0x001BC3FF File Offset: 0x001BA5FF
		private void WriteImageMapAreaShape(ImageMapArea.ImageMapAreaShape sourceType)
		{
			this.m_writer.WriteEnum((int)sourceType);
		}

		// Token: 0x06006D05 RID: 27909 RVA: 0x001BC40D File Offset: 0x001BA60D
		private void WriteSourceType(Image.SourceType sourceType)
		{
			this.m_writer.WriteEnum((int)sourceType);
		}

		// Token: 0x06006D06 RID: 27910 RVA: 0x001BC41B File Offset: 0x001BA61B
		private void WriteSizings(Image.Sizings sizing)
		{
			this.m_writer.WriteEnum((int)sizing);
		}

		// Token: 0x06006D07 RID: 27911 RVA: 0x001BC42C File Offset: 0x001BA62C
		private void WriteCheckBox(CheckBox checkBox)
		{
			if (checkBox == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.CheckBox;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemBase(checkBox);
			this.WriteExpressionInfo(checkBox.Value);
			this.m_writer.WriteString(checkBox.HideDuplicates);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D08 RID: 27912 RVA: 0x001BC490 File Offset: 0x001BA690
		private void WriteTextBox(TextBox textBox)
		{
			if (textBox == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.TextBox;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemBase(textBox);
			this.WriteExpressionInfo(textBox.Value);
			this.m_writer.WriteBoolean(textBox.CanGrow);
			this.m_writer.WriteBoolean(textBox.CanShrink);
			this.m_writer.WriteString(textBox.HideDuplicates);
			this.WriteAction(textBox.Action);
			this.m_writer.WriteBoolean(textBox.IsToggle);
			this.WriteExpressionInfo(textBox.InitialToggleState);
			this.WriteTypeCode(textBox.ValueType);
			this.m_writer.WriteString(textBox.Formula);
			this.m_writer.WriteBoolean(textBox.ValueReferenced);
			this.m_writer.WriteBoolean(textBox.RecursiveSender);
			this.m_writer.WriteBoolean(textBox.DataElementStyleAttribute);
			this.WriteGroupingReferenceList(textBox.ContainingScopes);
			this.WriteEndUserSort(textBox.UserSort);
			this.m_writer.WriteBoolean(textBox.IsMatrixCellScope);
			this.m_writer.WriteBoolean(textBox.IsSubReportTopLevelScope);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D09 RID: 27913 RVA: 0x001BC5C8 File Offset: 0x001BA7C8
		private void WriteEndUserSort(EndUserSort userSort)
		{
			if (userSort == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.EndUserSort;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteInt32(userSort.DataSetID);
			this.WriteSortFilterScopeReference(userSort.SortExpressionScope);
			this.WriteGroupingReferenceList(userSort.GroupsInSortTarget);
			this.WriteSortFilterScopeReference(userSort.SortTarget);
			this.m_writer.WriteInt32(userSort.SortExpressionIndex);
			this.WriteSubReportList(userSort.DetailScopeSubReports);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D0A RID: 27914 RVA: 0x001BC65A File Offset: 0x001BA85A
		private void WriteSortFilterScopeReference(ISortFilterScope sortFilterScope)
		{
			if (sortFilterScope == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteReference(ObjectType.ISortFilterScope, sortFilterScope.ID);
		}

		// Token: 0x06006D0B RID: 27915 RVA: 0x001BC684 File Offset: 0x001BA884
		private void WriteGroupingReferenceList(GroupingList groups)
		{
			if (groups == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.GroupingList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(groups.Count);
			for (int i = 0; i < groups.Count; i++)
			{
				if (groups[i] == null)
				{
					this.m_writer.WriteNull();
				}
				else
				{
					this.m_writer.WriteReference(ObjectType.Grouping, groups[i].Owner.ID);
				}
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006D0C RID: 27916 RVA: 0x001BC71E File Offset: 0x001BA91E
		private void WriteTypeCode(TypeCode typeCode)
		{
			this.m_writer.WriteEnum((int)typeCode);
		}

		// Token: 0x06006D0D RID: 27917 RVA: 0x001BC72C File Offset: 0x001BA92C
		private void WriteSubReport(SubReport subReport)
		{
			if (subReport == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.SubReport;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemBase(subReport);
			this.m_writer.WriteString(subReport.ReportPath);
			this.WriteParameterValueList(subReport.Parameters);
			this.WriteExpressionInfo(subReport.NoRows);
			this.m_writer.WriteBoolean(subReport.MergeTransactions);
			this.WriteGroupingReferenceList(subReport.ContainingScopes);
			this.m_writer.WriteBoolean(subReport.IsMatrixCellScope);
			this.WriteScopeLookupTable(subReport.DataSetUniqueNameMap);
			this.WriteStatus(subReport.RetrievalStatus);
			this.m_writer.WriteString(subReport.ReportName);
			this.m_writer.WriteString(subReport.Description);
			this.WriteReport(subReport.Report);
			this.m_writer.WriteString(subReport.StringUri);
			this.WriteParameterInfoCollection(subReport.ParametersFromCatalog);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D0E RID: 27918 RVA: 0x001BC82C File Offset: 0x001BAA2C
		private void WriteScopeLookupTable(ScopeLookupTable scopeTable)
		{
			if (scopeTable == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ScopeLookupTable;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteScopeTableValues(scopeTable.LookupTable);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D0F RID: 27919 RVA: 0x001BC878 File Offset: 0x001BAA78
		private void WriteScopeTableValues(object value)
		{
			if (value is int)
			{
				this.m_writer.WriteInt32((int)value);
				return;
			}
			Global.Tracer.Assert(value is Hashtable);
			Hashtable hashtable = (Hashtable)value;
			this.m_writer.StartArray(hashtable.Count);
			int num = 0;
			IDictionaryEnumerator enumerator = hashtable.GetEnumerator();
			while (enumerator.MoveNext())
			{
				this.WriteVariant(enumerator.Key, true);
				this.WriteScopeTableValues(enumerator.Value);
				num++;
			}
			IntermediateFormatWriter.Assert(num == hashtable.Count);
			this.m_writer.EndArray();
		}

		// Token: 0x06006D10 RID: 27920 RVA: 0x001BC913 File Offset: 0x001BAB13
		private void WriteStatus(SubReport.Status status)
		{
			this.m_writer.WriteEnum((int)status);
		}

		// Token: 0x06006D11 RID: 27921 RVA: 0x001BC924 File Offset: 0x001BAB24
		private void WriteActiveXControl(ActiveXControl control)
		{
			if (control == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ActiveXControl;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemBase(control);
			this.m_writer.WriteString(control.ClassID);
			this.m_writer.WriteString(control.CodeBase);
			this.WriteParameterValueList(control.Parameters);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D12 RID: 27922 RVA: 0x001BC998 File Offset: 0x001BAB98
		private void WriteParameterBase(ParameterBase parameter)
		{
			IntermediateFormatWriter.Assert(parameter != null);
			ObjectType objectType = ObjectType.ParameterBase;
			this.DeclareType(objectType);
			this.m_writer.WriteString(parameter.Name);
			this.WriteDataType(parameter.DataType);
			this.m_writer.WriteBoolean(parameter.Nullable);
			this.m_writer.WriteString(parameter.Prompt);
			this.m_writer.WriteBoolean(parameter.UsedInQuery);
			this.m_writer.WriteBoolean(parameter.AllowBlank);
			this.m_writer.WriteBoolean(parameter.MultiValue);
			this.WriteVariants(parameter.DefaultValues);
			this.m_writer.WriteBoolean(parameter.PromptUser);
		}

		// Token: 0x06006D13 RID: 27923 RVA: 0x001BCA4C File Offset: 0x001BAC4C
		private void WriteParameterDef(ParameterDef parameter)
		{
			if (parameter == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ParameterDef;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteParameterBase(parameter);
			this.WriteParameterDataSource(parameter.ValidValuesDataSource);
			this.WriteExpressionInfoList(parameter.ValidValuesValueExpressions);
			this.WriteExpressionInfoList(parameter.ValidValuesLabelExpressions);
			this.WriteParameterDataSource(parameter.DefaultDataSource);
			this.WriteExpressionInfoList(parameter.DefaultExpressions);
			this.WriteParameterDefRefList(parameter.DependencyList);
			this.m_writer.WriteInt32(parameter.ExprHostID);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D14 RID: 27924 RVA: 0x001BCAEC File Offset: 0x001BACEC
		private void WriteParameterDataSource(ParameterDataSource paramDataSource)
		{
			if (paramDataSource == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ParameterDataSource;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteInt32(paramDataSource.DataSourceIndex);
			this.m_writer.WriteInt32(paramDataSource.DataSetIndex);
			this.m_writer.WriteInt32(paramDataSource.ValueFieldIndex);
			this.m_writer.WriteInt32(paramDataSource.LabelFieldIndex);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D15 RID: 27925 RVA: 0x001BCB70 File Offset: 0x001BAD70
		private void WriteValidValue(ValidValue validValue)
		{
			if (validValue == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ValidValue;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteString(validValue.LabelRaw);
			this.WriteVariant(validValue.Value);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D16 RID: 27926 RVA: 0x001BCBD0 File Offset: 0x001BADD0
		private void WriteDataRegion(DataRegion dataRegion)
		{
			if (dataRegion == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			if (dataRegion is List)
			{
				this.WriteList((List)dataRegion);
				return;
			}
			if (dataRegion is Matrix)
			{
				this.WriteMatrix((Matrix)dataRegion);
				return;
			}
			if (dataRegion is Table)
			{
				this.WriteTable((Table)dataRegion);
				return;
			}
			if (dataRegion is Chart)
			{
				this.WriteChart((Chart)dataRegion);
				return;
			}
			if (dataRegion is CustomReportItem)
			{
				this.WriteCustomReportItem((CustomReportItem)dataRegion);
				return;
			}
			IntermediateFormatWriter.Assert(dataRegion is OWCChart);
			this.WriteOWCChart((OWCChart)dataRegion);
		}

		// Token: 0x06006D17 RID: 27927 RVA: 0x001BCC70 File Offset: 0x001BAE70
		private void WriteDataRegionBase(DataRegion dataRegion)
		{
			IntermediateFormatWriter.Assert(dataRegion != null);
			ObjectType objectType = ObjectType.DataRegion;
			this.DeclareType(objectType);
			this.WriteReportItemBase(dataRegion);
			this.m_writer.WriteString(dataRegion.DataSetName);
			this.WriteExpressionInfo(dataRegion.NoRows);
			this.m_writer.WriteBoolean(dataRegion.PageBreakAtEnd);
			this.m_writer.WriteBoolean(dataRegion.PageBreakAtStart);
			this.m_writer.WriteBoolean(dataRegion.KeepTogether);
			this.WriteIntList(dataRegion.RepeatSiblings);
			this.WriteFilterList(dataRegion.Filters);
			this.WriteDataAggregateInfoList(dataRegion.Aggregates);
			this.WriteDataAggregateInfoList(dataRegion.PostSortAggregates);
			this.WriteExpressionInfoList(dataRegion.UserSortExpressions);
			this.WriteInScopeSortFilterHashtable(dataRegion.DetailSortFiltersInScope);
		}

		// Token: 0x06006D18 RID: 27928 RVA: 0x001BCD30 File Offset: 0x001BAF30
		private void WriteDataRegionReference(DataRegion dataRegion)
		{
			if (dataRegion == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			if (dataRegion is List)
			{
				this.m_writer.WriteReference(ObjectType.List, dataRegion.ID);
				return;
			}
			if (dataRegion is Table)
			{
				this.m_writer.WriteReference(ObjectType.Table, dataRegion.ID);
				return;
			}
			if (dataRegion is Matrix)
			{
				this.m_writer.WriteReference(ObjectType.Matrix, dataRegion.ID);
				return;
			}
			if (dataRegion is Chart)
			{
				this.m_writer.WriteReference(ObjectType.Chart, dataRegion.ID);
				return;
			}
			if (dataRegion is CustomReportItem)
			{
				this.m_writer.WriteReference(ObjectType.CustomReportItem, dataRegion.ID);
				return;
			}
			IntermediateFormatWriter.Assert(dataRegion is OWCChart);
			this.m_writer.WriteReference(ObjectType.OWCChart, dataRegion.ID);
		}

		// Token: 0x06006D19 RID: 27929 RVA: 0x001BCE00 File Offset: 0x001BB000
		private void WriteReportHierarchyNode(ReportHierarchyNode node)
		{
			if (node == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			if (node is TableGroup)
			{
				this.WriteTableGroup((TableGroup)node);
				return;
			}
			if (node is MatrixHeading)
			{
				this.WriteMatrixHeading((MatrixHeading)node);
				return;
			}
			if (node is ChartHeading)
			{
				this.WriteChartHeading((ChartHeading)node);
				return;
			}
			if (node is MultiChart)
			{
				this.WriteMultiChart((MultiChart)node);
				return;
			}
			if (node is CustomReportItemHeading)
			{
				this.WriteCustomReportItemHeading((CustomReportItemHeading)node);
				return;
			}
			ObjectType objectType = ObjectType.ReportHierarchyNode;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportHierarchyNodeBase(node);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D1A RID: 27930 RVA: 0x001BCEB0 File Offset: 0x001BB0B0
		private void WriteReportHierarchyNodeBase(ReportHierarchyNode node)
		{
			IntermediateFormatWriter.Assert(node != null);
			ObjectType objectType = ObjectType.ReportHierarchyNode;
			this.DeclareType(objectType);
			this.WriteIDOwnerBase(node);
			this.WriteGrouping(node.Grouping);
			this.WriteSorting(node.Sorting);
			this.WriteReportHierarchyNode(node.InnerHierarchy);
			this.WriteDataRegionReference(node.DataRegionDef);
		}

		// Token: 0x06006D1B RID: 27931 RVA: 0x001BCF08 File Offset: 0x001BB108
		private void WriteGrouping(Grouping grouping)
		{
			if (grouping == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.Grouping;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteString(grouping.Name);
			this.WriteExpressionInfoList(grouping.GroupExpressions);
			this.WriteExpressionInfo(grouping.GroupLabel);
			this.WriteBoolList(grouping.SortDirections);
			this.m_writer.WriteBoolean(grouping.PageBreakAtEnd);
			this.m_writer.WriteBoolean(grouping.PageBreakAtStart);
			this.m_writer.WriteString(grouping.Custom);
			this.WriteDataAggregateInfoList(grouping.Aggregates);
			this.m_writer.WriteBoolean(grouping.GroupAndSort);
			this.WriteFilterList(grouping.Filters);
			this.WriteReportItemIDList(grouping.ReportItemsWithHideDuplicates);
			this.WriteExpressionInfoList(grouping.Parent);
			this.WriteDataAggregateInfoList(grouping.RecursiveAggregates);
			this.WriteDataAggregateInfoList(grouping.PostSortAggregates);
			this.m_writer.WriteString(grouping.DataElementName);
			this.m_writer.WriteString(grouping.DataCollectionName);
			this.WriteDataElementOutputType(grouping.DataElementOutput);
			this.WriteDataValueList(grouping.CustomProperties);
			this.m_writer.WriteBoolean(grouping.SaveGroupExprValues);
			this.WriteExpressionInfoList(grouping.UserSortExpressions);
			this.WriteInScopeSortFilterHashtable(grouping.NonDetailSortFiltersInScope);
			this.WriteInScopeSortFilterHashtable(grouping.DetailSortFiltersInScope);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D1C RID: 27932 RVA: 0x001BD078 File Offset: 0x001BB278
		private void WriteInScopeSortFilterHashtable(InScopeSortFilterHashtable inScopeSortFilters)
		{
			if (inScopeSortFilters == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.InScopeSortFilterHashtable;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(inScopeSortFilters.Count);
			int num = 0;
			IDictionaryEnumerator enumerator = inScopeSortFilters.GetEnumerator();
			while (enumerator.MoveNext())
			{
				int num2 = (int)enumerator.Key;
				this.m_writer.WriteInt32(num2);
				this.WriteIntList((IntList)enumerator.Value);
				num++;
			}
			IntermediateFormatWriter.Assert(num == inScopeSortFilters.Count);
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006D1D RID: 27933 RVA: 0x001BD11C File Offset: 0x001BB31C
		private void WriteSorting(Sorting sorting)
		{
			if (sorting == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.Sorting;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteExpressionInfoList(sorting.SortExpressions);
			this.WriteBoolList(sorting.SortDirections);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D1E RID: 27934 RVA: 0x001BD174 File Offset: 0x001BB374
		private void WriteTableGroup(TableGroup tableGroup)
		{
			if (tableGroup == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.TableGroup;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportHierarchyNodeBase(tableGroup);
			this.WriteTableRowList(tableGroup.HeaderRows);
			this.m_writer.WriteBoolean(tableGroup.HeaderRepeatOnNewPage);
			this.WriteTableRowList(tableGroup.FooterRows);
			this.m_writer.WriteBoolean(tableGroup.FooterRepeatOnNewPage);
			this.WriteVisibility(tableGroup.Visibility);
			this.m_writer.WriteBoolean(tableGroup.PropagatedPageBreakAtStart);
			this.m_writer.WriteBoolean(tableGroup.PropagatedPageBreakAtEnd);
			this.WriteRunningValueInfoList(tableGroup.RunningValues);
			this.m_writer.WriteBoolean(tableGroup.HasExprHost);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D1F RID: 27935 RVA: 0x001BD23D File Offset: 0x001BB43D
		private void WriteTableGroupReference(TableGroup tableGroup)
		{
			if (tableGroup == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteReference(ObjectType.TableGroup, tableGroup.ID);
		}

		// Token: 0x06006D20 RID: 27936 RVA: 0x001BD264 File Offset: 0x001BB464
		private void WriteTableDetail(TableDetail tableDetail)
		{
			if (tableDetail == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.TableDetail;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteIDOwnerBase(tableDetail);
			this.WriteTableRowList(tableDetail.DetailRows);
			this.WriteSorting(tableDetail.Sorting);
			this.WriteVisibility(tableDetail.Visibility);
			this.WriteRunningValueInfoList(tableDetail.RunningValues);
			this.m_writer.WriteBoolean(tableDetail.HasExprHost);
			this.m_writer.WriteBoolean(tableDetail.SimpleDetailRows);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D21 RID: 27937 RVA: 0x001BD300 File Offset: 0x001BB500
		private void WritePivotHeadingBase(PivotHeading pivotHeading)
		{
			if (pivotHeading == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.PivotHeading;
			this.DeclareType(objectType);
			this.WriteReportHierarchyNodeBase(pivotHeading);
			this.WriteVisibility(pivotHeading.Visibility);
			this.WriteSubtotal(pivotHeading.Subtotal);
			this.m_writer.WriteInt32(pivotHeading.Level);
			this.m_writer.WriteBoolean(pivotHeading.IsColumn);
			this.m_writer.WriteBoolean(pivotHeading.HasExprHost);
			this.m_writer.WriteInt32(pivotHeading.SubtotalSpan);
			this.WriteIntList(pivotHeading.IDs);
		}

		// Token: 0x06006D22 RID: 27938 RVA: 0x001BD398 File Offset: 0x001BB598
		private void WriteMatrixHeading(MatrixHeading matrixHeading)
		{
			if (matrixHeading == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.MatrixHeading;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WritePivotHeadingBase(matrixHeading);
			this.m_writer.WriteString(matrixHeading.Size);
			this.m_writer.WriteDouble(matrixHeading.SizeValue);
			this.WriteReportItemCollection(matrixHeading.ReportItems);
			this.m_writer.WriteBoolean(matrixHeading.OwcGroupExpression);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D23 RID: 27939 RVA: 0x001BD41B File Offset: 0x001BB61B
		private void WriteMatrixHeadingReference(MatrixHeading matrixHeading)
		{
			if (matrixHeading == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteReference(ObjectType.MatrixHeading, matrixHeading.ID);
		}

		// Token: 0x06006D24 RID: 27940 RVA: 0x001BD440 File Offset: 0x001BB640
		private void WriteTablixHeadingBase(TablixHeading tablixHeading)
		{
			if (tablixHeading == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.TablixHeading;
			this.DeclareType(objectType);
			this.WriteReportHierarchyNodeBase(tablixHeading);
			this.m_writer.WriteBoolean(tablixHeading.Subtotal);
			this.m_writer.WriteBoolean(tablixHeading.IsColumn);
			this.m_writer.WriteInt32(tablixHeading.Level);
			this.m_writer.WriteBoolean(tablixHeading.HasExprHost);
			this.m_writer.WriteInt32(tablixHeading.HeadingSpan);
		}

		// Token: 0x06006D25 RID: 27941 RVA: 0x001BD4C8 File Offset: 0x001BB6C8
		private void WriteCustomReportItemHeading(CustomReportItemHeading heading)
		{
			if (heading == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.CustomReportItemHeading;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteTablixHeadingBase(heading);
			this.m_writer.WriteBoolean(heading.Static);
			this.WriteCustomReportItemHeadingList(heading.InnerHeadings);
			this.WriteDataValueList(heading.CustomProperties);
			this.m_writer.WriteInt32(heading.ExprHostID);
			this.WriteRunningValueInfoList(heading.RunningValues);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D26 RID: 27942 RVA: 0x001BD555 File Offset: 0x001BB755
		private void WriteCustomReportItemHeadingReference(CustomReportItemHeading heading)
		{
			if (heading == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteReference(ObjectType.CustomReportItemHeading, heading.ID);
		}

		// Token: 0x06006D27 RID: 27943 RVA: 0x001BD57C File Offset: 0x001BB77C
		private void WriteTableRow(TableRow tableRow)
		{
			if (tableRow == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.TableRow;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteIDOwnerBase(tableRow);
			this.WriteReportItemCollection(tableRow.ReportItems);
			this.WriteIntList(tableRow.IDs);
			this.WriteIntList(tableRow.ColSpans);
			this.m_writer.WriteString(tableRow.Height);
			this.m_writer.WriteDouble(tableRow.HeightValue);
			this.WriteVisibility(tableRow.Visibility);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D28 RID: 27944 RVA: 0x001BD614 File Offset: 0x001BB814
		private void WriteSubtotal(Subtotal subtotal)
		{
			if (subtotal == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.Subtotal;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteIDOwnerBase(subtotal);
			this.m_writer.WriteBoolean(subtotal.AutoDerived);
			this.WriteReportItemCollection(subtotal.ReportItems);
			this.WriteStyle(subtotal.StyleClass);
			this.WritePositionType(subtotal.Position);
			this.m_writer.WriteString(subtotal.DataElementName);
			this.WriteDataElementOutputType(subtotal.DataElementOutput);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D29 RID: 27945 RVA: 0x001BD6AA File Offset: 0x001BB8AA
		private void WritePositionType(Subtotal.PositionType positionType)
		{
			this.m_writer.WriteEnum((int)positionType);
		}

		// Token: 0x06006D2A RID: 27946 RVA: 0x001BD6B8 File Offset: 0x001BB8B8
		private void WriteList(List list)
		{
			if (list == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.List;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteDataRegionBase(list);
			this.WriteReportHierarchyNode(list.HierarchyDef);
			this.WriteReportItemCollection(list.ReportItems);
			this.m_writer.WriteBoolean(list.FillPage);
			this.m_writer.WriteString(list.DataInstanceName);
			this.WriteDataElementOutputType(list.DataInstanceElementOutput);
			this.m_writer.WriteBoolean(list.IsListMostInner);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D2B RID: 27947 RVA: 0x001BD754 File Offset: 0x001BB954
		private void WritePivotBase(Pivot pivot)
		{
			IntermediateFormatWriter.Assert(pivot != null);
			ObjectType objectType = ObjectType.Pivot;
			this.DeclareType(objectType);
			this.WriteDataRegionBase(pivot);
			this.m_writer.WriteInt32(pivot.ColumnCount);
			this.m_writer.WriteInt32(pivot.RowCount);
			this.WriteDataAggregateInfoList(pivot.CellAggregates);
			this.WriteProcessingInnerGrouping(pivot.ProcessingInnerGrouping);
			this.WriteRunningValueInfoList(pivot.RunningValues);
			this.WriteDataAggregateInfoList(pivot.CellPostSortAggregates);
			this.WriteDataElementOutputType(pivot.CellDataElementOutput);
		}

		// Token: 0x06006D2C RID: 27948 RVA: 0x001BD7DC File Offset: 0x001BB9DC
		private void WriteTablixBase(Tablix tablix)
		{
			IntermediateFormatWriter.Assert(tablix != null);
			ObjectType objectType = ObjectType.Tablix;
			this.DeclareType(objectType);
			this.WriteDataRegionBase(tablix);
			this.m_writer.WriteInt32(tablix.ColumnCount);
			this.m_writer.WriteInt32(tablix.RowCount);
			this.WriteDataAggregateInfoList(tablix.CellAggregates);
			this.WriteProcessingInnerGrouping(tablix.ProcessingInnerGrouping);
			this.WriteRunningValueInfoList(tablix.RunningValues);
			this.WriteDataAggregateInfoList(tablix.CellPostSortAggregates);
		}

		// Token: 0x06006D2D RID: 27949 RVA: 0x001BD858 File Offset: 0x001BBA58
		private void WriteCustomReportItem(CustomReportItem custom)
		{
			if (custom == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.CustomReportItem;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteTablixBase(custom);
			this.m_writer.WriteString(custom.Type);
			this.WriteReportItemCollection(custom.AltReportItem);
			this.WriteCustomReportItemHeadingList(custom.Columns);
			this.WriteCustomReportItemHeadingList(custom.Rows);
			this.WriteDataCellsList(custom.DataRowCells);
			this.WriteRunningValueInfoList(custom.CellRunningValues);
			this.WriteIntList(custom.CellExprHostIDs);
			this.WriteReportItemCollection(custom.RenderReportItem);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D2E RID: 27950 RVA: 0x001BD904 File Offset: 0x001BBB04
		private void WriteChartDataPointList(ChartDataPointList datapoints)
		{
			if (datapoints == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ChartDataPointList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(datapoints.Count);
			for (int i = 0; i < datapoints.Count; i++)
			{
				this.WriteChartDataPoint(datapoints[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006D2F RID: 27951 RVA: 0x001BD978 File Offset: 0x001BBB78
		private void WriteChartDataPoint(ChartDataPoint datapoint)
		{
			if (datapoint == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ChartDataPoint;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteExpressionInfoList(datapoint.DataValues);
			this.WriteChartDataLabel(datapoint.DataLabel);
			this.WriteAction(datapoint.Action);
			this.WriteStyle(datapoint.StyleClass);
			this.m_writer.WriteEnum((int)datapoint.MarkerType);
			this.m_writer.WriteString(datapoint.MarkerSize);
			this.WriteStyle(datapoint.MarkerStyleClass);
			this.m_writer.WriteString(datapoint.DataElementName);
			this.WriteDataElementOutputType(datapoint.DataElementOutput);
			this.m_writer.WriteInt32(datapoint.ExprHostID);
			this.WriteDataValueList(datapoint.CustomProperties);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D30 RID: 27952 RVA: 0x001BDA50 File Offset: 0x001BBC50
		private void WriteChartDataLabel(ChartDataLabel label)
		{
			if (label == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ChartDataLabel;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteBoolean(label.Visible);
			this.WriteExpressionInfo(label.Value);
			this.WriteStyle(label.StyleClass);
			this.m_writer.WriteEnum((int)label.Position);
			this.m_writer.WriteInt32(label.Rotation);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D31 RID: 27953 RVA: 0x001BDADC File Offset: 0x001BBCDC
		private void WriteMultiChart(MultiChart multiChart)
		{
			if (multiChart == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.MultiChart;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportHierarchyNodeBase(multiChart);
			this.m_writer.WriteEnum((int)multiChart.Layout);
			this.m_writer.WriteInt32(multiChart.MaxCount);
			this.m_writer.WriteBoolean(multiChart.SyncScale);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D32 RID: 27954 RVA: 0x001BDB58 File Offset: 0x001BBD58
		private void WriteLegend(Legend legend)
		{
			if (legend == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.Legend;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteBoolean(legend.Visible);
			this.WriteStyle(legend.StyleClass);
			this.m_writer.WriteEnum((int)legend.Position);
			this.m_writer.WriteEnum((int)legend.Layout);
			this.m_writer.WriteBoolean(legend.InsidePlotArea);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D33 RID: 27955 RVA: 0x001BDBE8 File Offset: 0x001BBDE8
		private void WriteChartHeading(ChartHeading chartHeading)
		{
			if (chartHeading == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ChartHeading;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WritePivotHeadingBase(chartHeading);
			this.WriteExpressionInfoList(chartHeading.Labels);
			this.WriteRunningValueInfoList(chartHeading.RunningValues);
			this.m_writer.WriteBoolean(chartHeading.ChartGroupExpression);
			this.WriteBoolList(chartHeading.PlotTypesLine);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D34 RID: 27956 RVA: 0x001BDC64 File Offset: 0x001BBE64
		private void WriteChartHeadingReference(ChartHeading chartHeading)
		{
			if (chartHeading == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteReference(ObjectType.ChartHeading, chartHeading.ID);
		}

		// Token: 0x06006D35 RID: 27957 RVA: 0x001BDC8C File Offset: 0x001BBE8C
		private void WriteAxis(Axis axis)
		{
			if (axis == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.Axis;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteBoolean(axis.Visible);
			this.WriteStyle(axis.StyleClass);
			this.WriteChartTitle(axis.Title);
			this.m_writer.WriteBoolean(axis.Margin);
			this.m_writer.WriteEnum((int)axis.MajorTickMarks);
			this.m_writer.WriteEnum((int)axis.MinorTickMarks);
			this.WriteGridLines(axis.MajorGridLines);
			this.WriteGridLines(axis.MinorGridLines);
			this.WriteExpressionInfo(axis.MajorInterval);
			this.WriteExpressionInfo(axis.MinorInterval);
			this.m_writer.WriteBoolean(axis.Reverse);
			this.WriteExpressionInfo(axis.CrossAt);
			this.m_writer.WriteBoolean(axis.AutoCrossAt);
			this.m_writer.WriteBoolean(axis.Interlaced);
			this.m_writer.WriteBoolean(axis.Scalar);
			this.WriteExpressionInfo(axis.Min);
			this.WriteExpressionInfo(axis.Max);
			this.m_writer.WriteBoolean(axis.AutoScaleMin);
			this.m_writer.WriteBoolean(axis.AutoScaleMax);
			this.m_writer.WriteBoolean(axis.LogScale);
			this.WriteDataValueList(axis.CustomProperties);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D36 RID: 27958 RVA: 0x001BDE00 File Offset: 0x001BC000
		private void WriteGridLines(GridLines gridLines)
		{
			if (gridLines == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.GridLines;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteBoolean(gridLines.ShowGridLines);
			this.WriteStyle(gridLines.StyleClass);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D37 RID: 27959 RVA: 0x001BDE60 File Offset: 0x001BC060
		private void WriteChartTitle(ChartTitle chartTitle)
		{
			if (chartTitle == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ChartTitle;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteExpressionInfo(chartTitle.Caption);
			this.WriteStyle(chartTitle.StyleClass);
			this.m_writer.WriteEnum((int)chartTitle.Position);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D38 RID: 27960 RVA: 0x001BDECC File Offset: 0x001BC0CC
		private void WriteThreeDProperties(ThreeDProperties properties)
		{
			if (properties == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ThreeDProperties;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteBoolean(properties.Enabled);
			this.m_writer.WriteBoolean(properties.PerspectiveProjectionMode);
			this.m_writer.WriteInt32(properties.Rotation);
			this.m_writer.WriteInt32(properties.Inclination);
			this.m_writer.WriteInt32(properties.Perspective);
			this.m_writer.WriteInt32(properties.HeightRatio);
			this.m_writer.WriteInt32(properties.DepthRatio);
			this.m_writer.WriteEnum((int)properties.Shading);
			this.m_writer.WriteInt32(properties.GapDepth);
			this.m_writer.WriteInt32(properties.WallThickness);
			this.m_writer.WriteBoolean(properties.DrawingStyleCube);
			this.m_writer.WriteBoolean(properties.Clustered);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D39 RID: 27961 RVA: 0x001BDFD8 File Offset: 0x001BC1D8
		private void WritePlotArea(PlotArea plotArea)
		{
			if (plotArea == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.PlotArea;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteEnum((int)plotArea.Origin);
			this.WriteStyle(plotArea.StyleClass);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D3A RID: 27962 RVA: 0x001BE038 File Offset: 0x001BC238
		private void WriteChart(Chart chart)
		{
			if (chart == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.Chart;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WritePivotBase(chart);
			this.WriteChartHeading(chart.Columns);
			this.WriteChartHeading(chart.Rows);
			this.WriteChartDataPointList(chart.ChartDataPoints);
			this.WriteRunningValueInfoList(chart.CellRunningValues);
			this.WriteMultiChart(chart.MultiChart);
			this.WriteLegend(chart.Legend);
			this.WriteAxis(chart.CategoryAxis);
			this.WriteAxis(chart.ValueAxis);
			this.WriteChartHeadingReference(chart.StaticColumns);
			this.WriteChartHeadingReference(chart.StaticRows);
			this.m_writer.WriteEnum((int)chart.Type);
			this.m_writer.WriteEnum((int)chart.SubType);
			this.m_writer.WriteEnum((int)chart.Palette);
			this.WriteChartTitle(chart.Title);
			this.m_writer.WriteInt32(chart.PointWidth);
			this.WriteThreeDProperties(chart.ThreeDProperties);
			this.WritePlotArea(chart.PlotArea);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D3B RID: 27963 RVA: 0x001BE160 File Offset: 0x001BC360
		private void WriteMatrix(Matrix matrix)
		{
			if (matrix == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.Matrix;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WritePivotBase(matrix);
			this.WriteReportItemCollection(matrix.CornerReportItems);
			this.WriteMatrixHeading(matrix.Columns);
			this.WriteMatrixHeading(matrix.Rows);
			this.WriteReportItemCollection(matrix.CellReportItems);
			this.WriteIntList(matrix.CellIDs);
			this.m_writer.WriteBoolean(matrix.PropagatedPageBreakAtStart);
			this.m_writer.WriteBoolean(matrix.PropagatedPageBreakAtEnd);
			this.m_writer.WriteInt32(matrix.InnerRowLevelWithPageBreak);
			this.WriteMatrixRowList(matrix.MatrixRows);
			this.WriteMatrixColumnList(matrix.MatrixColumns);
			this.m_writer.WriteInt32(matrix.GroupsBeforeRowHeaders);
			this.m_writer.WriteBoolean(matrix.LayoutDirection);
			this.WriteMatrixHeadingReference(matrix.StaticColumns);
			this.WriteMatrixHeadingReference(matrix.StaticRows);
			this.m_writer.WriteBoolean(matrix.UseOWC);
			this.WriteStringList(matrix.OwcCellNames);
			this.m_writer.WriteString(matrix.CellDataElementName);
			this.m_writer.WriteBoolean(matrix.ColumnGroupingFixedHeader);
			this.m_writer.WriteBoolean(matrix.RowGroupingFixedHeader);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D3C RID: 27964 RVA: 0x001BE2B5 File Offset: 0x001BC4B5
		private void WriteProcessingInnerGrouping(Pivot.ProcessingInnerGroupings directions)
		{
			this.m_writer.WriteEnum((int)directions);
		}

		// Token: 0x06006D3D RID: 27965 RVA: 0x001BE2C4 File Offset: 0x001BC4C4
		private void WriteMatrixRow(MatrixRow row)
		{
			if (row == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.MatrixRow;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteString(row.Height);
			this.m_writer.WriteDouble(row.HeightValue);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D3E RID: 27966 RVA: 0x001BE324 File Offset: 0x001BC524
		private void WriteMatrixColumn(MatrixColumn column)
		{
			if (column == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.MatrixColumn;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteString(column.Width);
			this.m_writer.WriteDouble(column.WidthValue);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D3F RID: 27967 RVA: 0x001BE384 File Offset: 0x001BC584
		private void WriteTable(Table table)
		{
			if (table == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.Table;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteDataRegionBase(table);
			this.WriteTableColumnList(table.TableColumns);
			this.WriteTableRowList(table.HeaderRows);
			this.m_writer.WriteBoolean(table.HeaderRepeatOnNewPage);
			this.WriteTableGroup(table.TableGroups);
			this.WriteTableDetail(table.TableDetail);
			this.WriteTableGroupReference(table.DetailGroup);
			this.WriteTableRowList(table.FooterRows);
			this.m_writer.WriteBoolean(table.FooterRepeatOnNewPage);
			this.m_writer.WriteBoolean(table.PropagatedPageBreakAtStart);
			this.m_writer.WriteBoolean(table.GroupBreakAtStart);
			this.m_writer.WriteBoolean(table.PropagatedPageBreakAtEnd);
			this.m_writer.WriteBoolean(table.GroupBreakAtEnd);
			this.m_writer.WriteBoolean(table.FillPage);
			this.m_writer.WriteBoolean(table.UseOWC);
			this.m_writer.WriteBoolean(table.OWCNonSharedStyles);
			this.WriteRunningValueInfoList(table.RunningValues);
			this.m_writer.WriteString(table.DetailDataElementName);
			this.m_writer.WriteString(table.DetailDataCollectionName);
			this.WriteDataElementOutputType(table.DetailDataElementOutput);
			this.m_writer.WriteBoolean(table.FixedHeader);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D40 RID: 27968 RVA: 0x001BE4F4 File Offset: 0x001BC6F4
		private void WriteTableColumn(TableColumn column)
		{
			if (column == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.TableColumn;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteString(column.Width);
			this.m_writer.WriteDouble(column.WidthValue);
			this.WriteVisibility(column.Visibility);
			this.m_writer.WriteBoolean(column.FixedHeader);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D41 RID: 27969 RVA: 0x001BE570 File Offset: 0x001BC770
		private void WriteOWCChart(OWCChart chart)
		{
			if (chart == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.OWCChart;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteDataRegionBase(chart);
			this.WriteChartColumnList(chart.ChartData);
			this.m_writer.WriteString(chart.ChartDefinition);
			this.WriteRunningValueInfoList(chart.DetailRunningValues);
			this.WriteRunningValueInfoList(chart.RunningValues);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D42 RID: 27970 RVA: 0x001BE5EC File Offset: 0x001BC7EC
		private void WriteChartColumn(ChartColumn column)
		{
			if (column == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ChartColumn;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteString(column.Name);
			this.WriteExpressionInfo(column.Value);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D43 RID: 27971 RVA: 0x001BE648 File Offset: 0x001BC848
		private void WriteDataValue(DataValue value)
		{
			if (value == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.DataValue;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteExpressionInfo(value.Name);
			this.WriteExpressionInfo(value.Value);
			this.m_writer.WriteInt32(value.ExprHostID);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D44 RID: 27972 RVA: 0x001BE6B4 File Offset: 0x001BC8B4
		private void WriteParameterInfo(ParameterInfo parameter)
		{
			if (parameter == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ParameterInfo;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteParameterBase(parameter);
			this.m_writer.WriteBoolean(parameter.IsUserSupplied);
			this.WriteVariants(parameter.Values);
			this.m_writer.WriteBoolean(parameter.DynamicValidValues);
			this.m_writer.WriteBoolean(parameter.DynamicDefaultValue);
			this.WriteParameterInfoRefCollection(parameter.DependencyList);
			this.WriteValidValueList(parameter.ValidValues);
			this.WriteStrings(parameter.Labels);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D45 RID: 27973 RVA: 0x001BE75C File Offset: 0x001BC95C
		private void WriteProcessingMessage(ProcessingMessage message)
		{
			if (message == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ProcessingMessage;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteEnum((int)message.Code);
			this.m_writer.WriteEnum((int)message.Severity);
			this.m_writer.WriteEnum((int)message.ObjectType);
			this.m_writer.WriteString(message.ObjectName);
			this.m_writer.WriteString(message.PropertyName);
			this.m_writer.WriteString(message.Message);
			this.WriteProcessingMessageList(message.ProcessingMessages);
			this.m_writer.WriteEnum((int)message.CommonCode);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D46 RID: 27974 RVA: 0x001BE820 File Offset: 0x001BCA20
		private void WriteDataValueInstance(DataValueInstance instance)
		{
			if (instance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.DataValueInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteString(instance.Name);
			this.WriteVariant(instance.Value);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D47 RID: 27975 RVA: 0x001BE87D File Offset: 0x001BCA7D
		private void WriteDataType(DataType dataType)
		{
			this.m_writer.WriteEnum((int)dataType);
		}

		// Token: 0x06006D48 RID: 27976 RVA: 0x001BE88C File Offset: 0x001BCA8C
		private void WriteBookmarkInformation(BookmarkInformation bookmark)
		{
			if (bookmark == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.BookmarkInformation;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteString(bookmark.Id);
			this.m_writer.WriteInt32(bookmark.Page);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D49 RID: 27977 RVA: 0x001BE8F0 File Offset: 0x001BCAF0
		private void WriteDrillthroughInformation(DrillthroughInformation drillthroughInfo)
		{
			if (drillthroughInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.DrillthroughInformation;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteString(drillthroughInfo.ReportName);
			this.WriteDrillthroughParameters(drillthroughInfo.ReportParameters);
			this.WriteIntList(drillthroughInfo.DataSetTokenIDs);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D4A RID: 27978 RVA: 0x001BE95C File Offset: 0x001BCB5C
		private void WriteSenderInformation(SenderInformation sender)
		{
			if (sender == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.SenderInformation;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteBoolean(sender.StartHidden);
			this.WriteIntList(sender.ReceiverUniqueNames);
			this.m_writer.WriteInt32s(sender.ContainerUniqueNames);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D4B RID: 27979 RVA: 0x001BE9C8 File Offset: 0x001BCBC8
		private void WriteReceiverInformation(ReceiverInformation receiver)
		{
			if (receiver == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ReceiverInformation;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteBoolean(receiver.StartHidden);
			this.m_writer.WriteInt32(receiver.SenderUniqueName);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D4C RID: 27980 RVA: 0x001BEA28 File Offset: 0x001BCC28
		private void WriteInfoBaseBase(InfoBase infoBase)
		{
			IntermediateFormatWriter.Assert(infoBase != null);
			ObjectType objectType = ObjectType.InfoBase;
			this.DeclareType(objectType);
		}

		// Token: 0x06006D4D RID: 27981 RVA: 0x001BEA48 File Offset: 0x001BCC48
		private void WriteSimpleOffsetInfo(OffsetInfo offsetInfo)
		{
			this.m_writer.WriteInt64(offsetInfo.Offset);
		}

		// Token: 0x06006D4E RID: 27982 RVA: 0x001BEA5C File Offset: 0x001BCC5C
		private void WriteActionInstance(ActionInstance actionInstance)
		{
			if (actionInstance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ActionInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteActionItemInstanceList(actionInstance.ActionItemsValues);
			this.WriteVariants(actionInstance.StyleAttributeValues);
			this.m_writer.WriteInt32(actionInstance.UniqueName);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D4F RID: 27983 RVA: 0x001BEAC8 File Offset: 0x001BCCC8
		private void WriteActionItemInstanceList(ActionItemInstanceList actionItemInstanceList)
		{
			if (actionItemInstanceList == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ActionItemInstanceList;
			this.m_writer.StartObject(objectType);
			this.m_writer.StartArray(actionItemInstanceList.Count);
			for (int i = 0; i < actionItemInstanceList.Count; i++)
			{
				this.WriteActionItemInstance(actionItemInstanceList[i]);
			}
			this.m_writer.EndArray();
			this.m_writer.EndObject();
		}

		// Token: 0x06006D50 RID: 27984 RVA: 0x001BEB3C File Offset: 0x001BCD3C
		private void WriteActionItemInstance(ActionItemInstance actionItemInstance)
		{
			if (actionItemInstance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ActionItemInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteString(actionItemInstance.HyperLinkURL);
			this.m_writer.WriteString(actionItemInstance.BookmarkLink);
			this.m_writer.WriteString(actionItemInstance.DrillthroughReportName);
			this.WriteDrillthroughVariants(actionItemInstance.DrillthroughParametersValues, actionItemInstance.DataSetTokenIDs);
			this.WriteBoolList(actionItemInstance.DrillthroughParametersOmits);
			this.m_writer.WriteString(actionItemInstance.Label);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D51 RID: 27985 RVA: 0x001BEBE0 File Offset: 0x001BCDE0
		private void WriteDrillthroughVariants(object[] variants, IntList tokenIDs)
		{
			if (variants == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.StartArray(variants.Length);
			for (int i = 0; i < variants.Length; i++)
			{
				object obj = null;
				if (tokenIDs == null || tokenIDs[i] < 0)
				{
					obj = variants[i];
				}
				if (obj is object[])
				{
					this.WriteVariants(obj as object[], false);
				}
				else
				{
					this.WriteVariant(obj);
				}
			}
			this.m_writer.EndArray();
		}

		// Token: 0x06006D52 RID: 27986 RVA: 0x001BEC58 File Offset: 0x001BCE58
		private void WriteReportInstanceInfo(ReportInstanceInfo instanceInfo)
		{
			if (instanceInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ReportInstanceInfo;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemInstanceInfoBase(instanceInfo);
			this.WriteParameterInfoCollection(instanceInfo.Parameters);
			this.m_writer.WriteString(instanceInfo.ReportName);
			this.m_writer.WriteBoolean(instanceInfo.NoRows);
			this.m_writer.WriteInt32(instanceInfo.BodyUniqueName);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D53 RID: 27987 RVA: 0x001BECDC File Offset: 0x001BCEDC
		private void WriteReportItemColInstanceInfo(ReportItemColInstanceInfo instanceInfo)
		{
			if (instanceInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ReportItemColInstanceInfo;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteInstanceInfoBase(instanceInfo);
			this.WriteNonComputedUniqueNamess(instanceInfo.ChildrenNonComputedUniqueNames);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D54 RID: 27988 RVA: 0x001BED2C File Offset: 0x001BCF2C
		private void WriteReportItemInstanceInfo(ReportItemInstanceInfo reportItemInstanceInfo)
		{
			if (reportItemInstanceInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			if (reportItemInstanceInfo is LineInstanceInfo)
			{
				this.WriteLineInstanceInfo((LineInstanceInfo)reportItemInstanceInfo);
				return;
			}
			if (reportItemInstanceInfo is RectangleInstanceInfo)
			{
				this.WriteRectangleInstanceInfo((RectangleInstanceInfo)reportItemInstanceInfo);
				return;
			}
			if (reportItemInstanceInfo is ImageInstanceInfo)
			{
				this.WriteImageInstanceInfo((ImageInstanceInfo)reportItemInstanceInfo);
				return;
			}
			if (reportItemInstanceInfo is CheckBoxInstanceInfo)
			{
				this.WriteCheckBoxInstanceInfo((CheckBoxInstanceInfo)reportItemInstanceInfo);
				return;
			}
			if (reportItemInstanceInfo is TextBoxInstanceInfo)
			{
				this.WriteTextBoxInstanceInfo((TextBoxInstanceInfo)reportItemInstanceInfo);
				return;
			}
			if (reportItemInstanceInfo is SubReportInstanceInfo)
			{
				this.WriteSubReportInstanceInfo((SubReportInstanceInfo)reportItemInstanceInfo);
				return;
			}
			if (reportItemInstanceInfo is ActiveXControlInstanceInfo)
			{
				this.WriteActiveXControlInstanceInfo((ActiveXControlInstanceInfo)reportItemInstanceInfo);
				return;
			}
			if (reportItemInstanceInfo is ListInstanceInfo)
			{
				this.WriteListInstanceInfo((ListInstanceInfo)reportItemInstanceInfo);
				return;
			}
			if (reportItemInstanceInfo is MatrixInstanceInfo)
			{
				this.WriteMatrixInstanceInfo((MatrixInstanceInfo)reportItemInstanceInfo);
				return;
			}
			if (reportItemInstanceInfo is TableInstanceInfo)
			{
				this.WriteTableInstanceInfo((TableInstanceInfo)reportItemInstanceInfo);
				return;
			}
			if (reportItemInstanceInfo is OWCChartInstanceInfo)
			{
				this.WriteOWCChartInstanceInfo((OWCChartInstanceInfo)reportItemInstanceInfo);
				return;
			}
			if (reportItemInstanceInfo is ChartInstanceInfo)
			{
				this.WriteChartInstanceInfo((ChartInstanceInfo)reportItemInstanceInfo);
				return;
			}
			if (reportItemInstanceInfo is CustomReportItemInstanceInfo)
			{
				this.WriteCustomReportItemInstanceInfo((CustomReportItemInstanceInfo)reportItemInstanceInfo);
				return;
			}
			if (reportItemInstanceInfo is PageSectionInstanceInfo)
			{
				this.WritePageSectionInstanceInfo((PageSectionInstanceInfo)reportItemInstanceInfo);
				return;
			}
			IntermediateFormatWriter.Assert(reportItemInstanceInfo is ReportInstanceInfo);
			this.WriteReportInstanceInfo((ReportInstanceInfo)reportItemInstanceInfo);
		}

		// Token: 0x06006D55 RID: 27989 RVA: 0x001BEE88 File Offset: 0x001BD088
		private void WriteLineInstanceInfo(LineInstanceInfo instanceInfo)
		{
			if (instanceInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.LineInstanceInfo;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemInstanceInfoBase(instanceInfo);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D56 RID: 27990 RVA: 0x001BEECC File Offset: 0x001BD0CC
		private void WriteTextBoxInstanceInfo(TextBoxInstanceInfo instanceInfo)
		{
			if (instanceInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.TextBoxInstanceInfo;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemInstanceInfoBase(instanceInfo);
			this.m_writer.WriteString(instanceInfo.FormattedValue);
			this.WriteVariant(instanceInfo.OriginalValue);
			TextBox textBox = (TextBox)instanceInfo.ReportItemDef;
			if (textBox.HideDuplicates != null)
			{
				this.m_writer.WriteBoolean(instanceInfo.Duplicate);
			}
			if (textBox.Action != null)
			{
				this.WriteActionInstance(instanceInfo.Action);
			}
			if (textBox.InitialToggleState != null)
			{
				this.m_writer.WriteBoolean(instanceInfo.InitialToggleState);
			}
			this.m_writer.EndObject();
		}

		// Token: 0x06006D57 RID: 27991 RVA: 0x001BEF80 File Offset: 0x001BD180
		private void WriteSimpleTextBoxInstanceInfo(SimpleTextBoxInstanceInfo simpleTextBoxInstanceInfo)
		{
			if (simpleTextBoxInstanceInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.SimpleTextBoxInstanceInfo;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteInstanceInfoBase(simpleTextBoxInstanceInfo);
			this.m_writer.WriteString(simpleTextBoxInstanceInfo.FormattedValue);
			this.WriteVariant(simpleTextBoxInstanceInfo.OriginalValue);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D58 RID: 27992 RVA: 0x001BEFE4 File Offset: 0x001BD1E4
		private void WriteRectangleInstanceInfo(RectangleInstanceInfo instanceInfo)
		{
			if (instanceInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.RectangleInstanceInfo;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemInstanceInfoBase(instanceInfo);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D59 RID: 27993 RVA: 0x001BF028 File Offset: 0x001BD228
		private void WriteCheckBoxInstanceInfo(CheckBoxInstanceInfo instanceInfo)
		{
			if (instanceInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.CheckBoxInstanceInfo;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemInstanceInfoBase(instanceInfo);
			this.m_writer.WriteBoolean(instanceInfo.Value);
			this.m_writer.WriteBoolean(instanceInfo.Duplicate);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D5A RID: 27994 RVA: 0x001BF090 File Offset: 0x001BD290
		private void WriteImageInstanceInfo(ImageInstanceInfo instanceInfo)
		{
			if (instanceInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ImageInstanceInfo;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemInstanceInfoBase(instanceInfo);
			this.m_writer.WriteString(instanceInfo.ImageValue);
			this.WriteActionInstance(instanceInfo.Action);
			this.m_writer.WriteBoolean(instanceInfo.BrokenImage);
			this.WriteImageMapAreaInstanceList(instanceInfo.ImageMapAreas);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D5B RID: 27995 RVA: 0x001BF110 File Offset: 0x001BD310
		private void WriteSubReportInstanceInfo(SubReportInstanceInfo instanceInfo)
		{
			if (instanceInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.SubReportInstanceInfo;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemInstanceInfoBase(instanceInfo);
			this.m_writer.WriteString(instanceInfo.NoRows);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D5C RID: 27996 RVA: 0x001BF168 File Offset: 0x001BD368
		private void WriteActiveXControlInstanceInfo(ActiveXControlInstanceInfo instanceInfo)
		{
			if (instanceInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ActiveXControlInstanceInfo;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemInstanceInfoBase(instanceInfo);
			this.WriteVariants(instanceInfo.ParameterValues);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D5D RID: 27997 RVA: 0x001BF1B8 File Offset: 0x001BD3B8
		private void WriteListInstanceInfo(ListInstanceInfo instanceInfo)
		{
			if (instanceInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ListInstanceInfo;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemInstanceInfoBase(instanceInfo);
			this.m_writer.WriteString(instanceInfo.NoRows);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D5E RID: 27998 RVA: 0x001BF210 File Offset: 0x001BD410
		private void WriteListContentInstanceInfo(ListContentInstanceInfo instanceInfo)
		{
			if (instanceInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ListContentInstanceInfo;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteInstanceInfoBase(instanceInfo);
			this.m_writer.WriteBoolean(instanceInfo.StartHidden);
			this.m_writer.WriteString(instanceInfo.Label);
			this.WriteDataValueInstanceList(instanceInfo.CustomPropertyInstances);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D5F RID: 27999 RVA: 0x001BF284 File Offset: 0x001BD484
		private void WriteMatrixInstanceInfo(MatrixInstanceInfo instanceInfo)
		{
			if (instanceInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.MatrixInstanceInfo;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemInstanceInfoBase(instanceInfo);
			this.WriteNonComputedUniqueNames(instanceInfo.CornerNonComputedNames);
			this.m_writer.WriteString(instanceInfo.NoRows);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D60 RID: 28000 RVA: 0x001BF2E8 File Offset: 0x001BD4E8
		private void WriteMatrixHeadingInstanceInfo(MatrixHeadingInstanceInfo instanceInfo)
		{
			if (instanceInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.MatrixHeadingInstanceInfo;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteInstanceInfoBase(instanceInfo);
			this.WriteNonComputedUniqueNames(instanceInfo.ContentUniqueNames);
			this.m_writer.WriteBoolean(instanceInfo.StartHidden);
			this.m_writer.WriteInt32(instanceInfo.HeadingCellIndex);
			this.m_writer.WriteInt32(instanceInfo.HeadingSpan);
			this.WriteVariant(instanceInfo.GroupExpressionValue);
			this.m_writer.WriteString(instanceInfo.Label);
			this.WriteDataValueInstanceList(instanceInfo.CustomPropertyInstances);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D61 RID: 28001 RVA: 0x001BF394 File Offset: 0x001BD594
		private void WriteMatrixSubtotalHeadingInstanceInfo(MatrixSubtotalHeadingInstanceInfo instanceInfo)
		{
			if (instanceInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.MatrixSubtotalHeadingInstanceInfo;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteInstanceInfoBase(instanceInfo);
			this.WriteMatrixHeadingInstanceInfo(instanceInfo);
			this.WriteVariants(instanceInfo.StyleAttributeValues);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D62 RID: 28002 RVA: 0x001BF3F0 File Offset: 0x001BD5F0
		private void WriteMatrixCellInstanceInfo(MatrixCellInstanceInfo instanceInfo)
		{
			if (instanceInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.MatrixCellInstanceInfo;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteInstanceInfoBase(instanceInfo);
			this.WriteNonComputedUniqueNames(instanceInfo.ContentUniqueNames);
			this.m_writer.WriteInt32(instanceInfo.RowIndex);
			this.m_writer.WriteInt32(instanceInfo.ColumnIndex);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D63 RID: 28003 RVA: 0x001BF464 File Offset: 0x001BD664
		private void WriteChartInstanceInfo(ChartInstanceInfo instanceInfo)
		{
			if (instanceInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ChartInstanceInfo;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemInstanceInfoBase(instanceInfo);
			this.WriteAxisInstance(instanceInfo.CategoryAxis);
			this.WriteAxisInstance(instanceInfo.ValueAxis);
			this.WriteChartTitleInstance(instanceInfo.Title);
			this.WriteVariants(instanceInfo.PlotAreaStyleAttributeValues);
			this.WriteVariants(instanceInfo.LegendStyleAttributeValues);
			this.m_writer.WriteString(instanceInfo.CultureName);
			this.m_writer.WriteString(instanceInfo.NoRows);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D64 RID: 28004 RVA: 0x001BF508 File Offset: 0x001BD708
		private void WriteChartHeadingInstanceInfo(ChartHeadingInstanceInfo instanceInfo)
		{
			if (instanceInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ChartHeadingInstanceInfo;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteInstanceInfoBase(instanceInfo);
			this.WriteVariant(instanceInfo.HeadingLabel);
			this.m_writer.WriteInt32(instanceInfo.HeadingCellIndex);
			this.m_writer.WriteInt32(instanceInfo.HeadingSpan);
			this.WriteVariant(instanceInfo.GroupExpressionValue);
			this.m_writer.WriteInt32(instanceInfo.StaticGroupingIndex);
			this.WriteDataValueInstanceList(instanceInfo.CustomPropertyInstances);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D65 RID: 28005 RVA: 0x001BF5A8 File Offset: 0x001BD7A8
		private void WriteChartDataPointInstanceInfo(ChartDataPointInstanceInfo instanceInfo)
		{
			if (instanceInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ChartDataPointInstanceInfo;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteInstanceInfoBase(instanceInfo);
			this.m_writer.WriteInt32(instanceInfo.DataPointIndex);
			this.WriteVariants(instanceInfo.DataValues);
			this.m_writer.WriteString(instanceInfo.DataLabelValue);
			this.WriteVariants(instanceInfo.DataLabelStyleAttributeValues);
			this.WriteActionInstance(instanceInfo.Action);
			this.WriteVariants(instanceInfo.StyleAttributeValues);
			this.WriteVariants(instanceInfo.MarkerStyleAttributeValues);
			this.WriteDataValueInstanceList(instanceInfo.CustomPropertyInstances);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D66 RID: 28006 RVA: 0x001BF65C File Offset: 0x001BD85C
		private void WriteTableInstanceInfo(TableInstanceInfo instanceInfo)
		{
			if (instanceInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.TableInstanceInfo;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemInstanceInfoBase(instanceInfo);
			this.WriteTableColumnInstances(instanceInfo.ColumnInstances);
			this.m_writer.WriteString(instanceInfo.NoRows);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D67 RID: 28007 RVA: 0x001BF6C0 File Offset: 0x001BD8C0
		private void WriteTableGroupInstanceInfo(TableGroupInstanceInfo instanceInfo)
		{
			if (instanceInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.TableGroupInstanceInfo;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteInstanceInfoBase(instanceInfo);
			this.m_writer.WriteBoolean(instanceInfo.StartHidden);
			this.m_writer.WriteString(instanceInfo.Label);
			this.WriteDataValueInstanceList(instanceInfo.CustomPropertyInstances);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D68 RID: 28008 RVA: 0x001BF734 File Offset: 0x001BD934
		private void WriteTableDetailInstanceInfo(TableDetailInstanceInfo instanceInfo)
		{
			if (instanceInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.TableDetailInstanceInfo;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteInstanceInfoBase(instanceInfo);
			this.m_writer.WriteBoolean(instanceInfo.StartHidden);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D69 RID: 28009 RVA: 0x001BF78C File Offset: 0x001BD98C
		private void WriteTableRowInstanceInfo(TableRowInstanceInfo instanceInfo)
		{
			if (instanceInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.TableRowInstanceInfo;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteInstanceInfoBase(instanceInfo);
			this.m_writer.WriteBoolean(instanceInfo.StartHidden);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D6A RID: 28010 RVA: 0x001BF7E4 File Offset: 0x001BD9E4
		private void WriteOWCChartInstanceInfo(OWCChartInstanceInfo instanceInfo)
		{
			if (instanceInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.OWCChartInstanceInfo;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemInstanceInfoBase(instanceInfo);
			this.WriteVariantLists(instanceInfo.ChartData, false);
			this.m_writer.WriteString(instanceInfo.NoRows);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D6B RID: 28011 RVA: 0x001BF848 File Offset: 0x001BDA48
		private void WriteCustomReportItemInstanceInfo(CustomReportItemInstanceInfo instanceInfo)
		{
			if (instanceInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.CustomReportItemInstanceInfo;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemInstanceInfoBase(instanceInfo);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D6C RID: 28012 RVA: 0x001BF890 File Offset: 0x001BDA90
		private void WritePageSectionInstanceInfo(PageSectionInstanceInfo instanceInfo)
		{
			if (instanceInfo == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.PageSectionInstanceInfo;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemInstanceInfoBase(instanceInfo);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D6D RID: 28013 RVA: 0x001BF8D8 File Offset: 0x001BDAD8
		private void WriteInstanceInfoBase(InstanceInfo instanceInfo)
		{
			IntermediateFormatWriter.Assert(instanceInfo != null);
			ObjectType objectType = ObjectType.InstanceInfo;
			this.DeclareType(objectType);
			this.WriteInfoBaseBase(instanceInfo);
		}

		// Token: 0x06006D6E RID: 28014 RVA: 0x001BF900 File Offset: 0x001BDB00
		private void WriteReportItemInstanceInfoBase(ReportItemInstanceInfo instanceInfo)
		{
			IntermediateFormatWriter.Assert(instanceInfo != null);
			ObjectType objectType = ObjectType.ReportItemInstanceInfo;
			this.DeclareType(objectType);
			this.WriteInstanceInfoBase(instanceInfo);
			ReportItem reportItemDef = instanceInfo.ReportItemDef;
			if (reportItemDef.StyleClass != null && reportItemDef.StyleClass.ExpressionList != null)
			{
				this.WriteVariants(instanceInfo.StyleAttributeValues);
			}
			if (reportItemDef.Visibility != null)
			{
				this.m_writer.WriteBoolean(instanceInfo.StartHidden);
			}
			if (reportItemDef.Label != null)
			{
				this.m_writer.WriteString(instanceInfo.Label);
			}
			if (reportItemDef.Bookmark != null)
			{
				this.m_writer.WriteString(instanceInfo.Bookmark);
			}
			if (reportItemDef.ToolTip != null)
			{
				this.m_writer.WriteString(instanceInfo.ToolTip);
			}
			if (reportItemDef.CustomProperties != null)
			{
				this.WriteDataValueInstanceList(instanceInfo.CustomPropertyInstances);
			}
		}

		// Token: 0x06006D6F RID: 28015 RVA: 0x001BF9C8 File Offset: 0x001BDBC8
		private void WriteNonComputedUniqueNames(NonComputedUniqueNames names)
		{
			if (names == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.NonComputedUniqueNames;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteInt32(names.UniqueName);
			this.WriteNonComputedUniqueNamess(names.ChildrenUniqueNames);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D70 RID: 28016 RVA: 0x001BFA24 File Offset: 0x001BDC24
		private void WriteInstanceInfoOwnerBase(InstanceInfoOwner owner)
		{
			IntermediateFormatWriter.Assert(owner != null);
			ObjectType objectType = ObjectType.InstanceInfoOwner;
			this.DeclareType(objectType);
			this.WriteSimpleOffsetInfo(owner.OffsetInfo);
		}

		// Token: 0x06006D71 RID: 28017 RVA: 0x001BFA50 File Offset: 0x001BDC50
		private void WriteReportItemInstance(ReportItemInstance reportItemInstance)
		{
			if (reportItemInstance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			if (reportItemInstance is LineInstance)
			{
				this.WriteLineInstance((LineInstance)reportItemInstance);
				return;
			}
			if (reportItemInstance is RectangleInstance)
			{
				this.WriteRectangleInstance((RectangleInstance)reportItemInstance);
				return;
			}
			if (reportItemInstance is ImageInstance)
			{
				this.WriteImageInstance((ImageInstance)reportItemInstance);
				return;
			}
			if (reportItemInstance is CheckBoxInstance)
			{
				this.WriteCheckBoxInstance((CheckBoxInstance)reportItemInstance);
				return;
			}
			if (reportItemInstance is TextBoxInstance)
			{
				this.WriteTextBoxInstance((TextBoxInstance)reportItemInstance);
				return;
			}
			if (reportItemInstance is SubReportInstance)
			{
				this.WriteSubReportInstance((SubReportInstance)reportItemInstance);
				return;
			}
			if (reportItemInstance is ActiveXControlInstance)
			{
				this.WriteActiveXControlInstance((ActiveXControlInstance)reportItemInstance);
				return;
			}
			if (reportItemInstance is ListInstance)
			{
				this.WriteListInstance((ListInstance)reportItemInstance);
				return;
			}
			if (reportItemInstance is MatrixInstance)
			{
				this.WriteMatrixInstance((MatrixInstance)reportItemInstance);
				return;
			}
			if (reportItemInstance is TableInstance)
			{
				this.WriteTableInstance((TableInstance)reportItemInstance);
				return;
			}
			if (reportItemInstance is ChartInstance)
			{
				this.WriteChartInstance((ChartInstance)reportItemInstance);
				return;
			}
			if (reportItemInstance is CustomReportItemInstance)
			{
				this.WriteCustomReportItemInstance((CustomReportItemInstance)reportItemInstance);
				return;
			}
			IntermediateFormatWriter.Assert(reportItemInstance is OWCChartInstance);
			this.WriteOWCChartInstance((OWCChartInstance)reportItemInstance);
		}

		// Token: 0x06006D72 RID: 28018 RVA: 0x001BFB84 File Offset: 0x001BDD84
		private void WriteReportItemInstanceBase(ReportItemInstance reportItemInstance)
		{
			IntermediateFormatWriter.Assert(reportItemInstance != null);
			ObjectType objectType = ObjectType.ReportItemInstance;
			this.DeclareType(objectType);
			this.WriteInstanceInfoOwnerBase(reportItemInstance);
			if (this.m_writeUniqueName)
			{
				this.m_writer.WriteInt32(reportItemInstance.UniqueName);
			}
		}

		// Token: 0x06006D73 RID: 28019 RVA: 0x001BFBC4 File Offset: 0x001BDDC4
		private void WriteReportItemInstanceReference(ReportItemInstance reportItemInstance)
		{
			if (reportItemInstance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			IntermediateFormatWriter.Assert(reportItemInstance is OWCChartInstance || reportItemInstance is ChartInstance);
			ObjectType objectType = ((reportItemInstance is OWCChartInstance) ? ObjectType.OWCChartInstance : ObjectType.ChartInstance);
			this.m_writer.WriteReference(objectType, reportItemInstance.UniqueName);
		}

		// Token: 0x06006D74 RID: 28020 RVA: 0x001BFC20 File Offset: 0x001BDE20
		private void WriteReportInstance(ReportInstance reportInstance)
		{
			if (reportInstance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ReportInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemInstanceBase(reportInstance);
			this.WriteReportItemColInstance(reportInstance.ReportItemColInstance);
			this.m_writer.WriteString(reportInstance.Language);
			this.m_writer.WriteInt32(reportInstance.NumberOfPages);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D75 RID: 28021 RVA: 0x001BFC94 File Offset: 0x001BDE94
		private void WriteReportItemColInstance(ReportItemColInstance reportItemColInstance)
		{
			if (reportItemColInstance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ReportItemColInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteInstanceInfoOwnerBase(reportItemColInstance);
			this.WriteReportItemInstanceList(reportItemColInstance.ReportItemInstances);
			this.WriteRenderingPagesRangesList(reportItemColInstance.ChildrenStartAndEndPages);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D76 RID: 28022 RVA: 0x001BFCF0 File Offset: 0x001BDEF0
		private void WriteLineInstance(LineInstance lineInstance)
		{
			if (lineInstance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.LineInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemInstanceBase(lineInstance);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D77 RID: 28023 RVA: 0x001BFD34 File Offset: 0x001BDF34
		private void WriteTextBoxInstance(TextBoxInstance textBoxInstance)
		{
			if (textBoxInstance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.TextBoxInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemInstanceBase(textBoxInstance);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D78 RID: 28024 RVA: 0x001BFD78 File Offset: 0x001BDF78
		private void WriteRectangleInstance(RectangleInstance rectangleInstance)
		{
			if (rectangleInstance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.RectangleInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemInstanceBase(rectangleInstance);
			this.WriteReportItemColInstance(rectangleInstance.ReportItemColInstance);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D79 RID: 28025 RVA: 0x001BFDC8 File Offset: 0x001BDFC8
		private void WriteCheckBoxInstance(CheckBoxInstance checkBoxInstance)
		{
			if (checkBoxInstance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.CheckBoxInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemInstanceBase(checkBoxInstance);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D7A RID: 28026 RVA: 0x001BFE0C File Offset: 0x001BE00C
		private void WriteImageInstance(ImageInstance imageInstance)
		{
			if (imageInstance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ImageInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemInstanceBase(imageInstance);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D7B RID: 28027 RVA: 0x001BFE50 File Offset: 0x001BE050
		private void WriteSubReportInstance(SubReportInstance subReportInstance)
		{
			if (subReportInstance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.SubReportInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemInstanceBase(subReportInstance);
			this.WriteReportInstance(subReportInstance.ReportInstance);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D7C RID: 28028 RVA: 0x001BFEA0 File Offset: 0x001BE0A0
		private void WriteActiveXControlInstance(ActiveXControlInstance activeXControlInstance)
		{
			if (activeXControlInstance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ActiveXControlInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemInstanceBase(activeXControlInstance);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D7D RID: 28029 RVA: 0x001BFEE4 File Offset: 0x001BE0E4
		private void WriteListInstance(ListInstance listInstance)
		{
			if (listInstance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ListInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemInstanceBase(listInstance);
			this.WriteListContentInstanceList(listInstance.ListContents);
			this.WriteRenderingPagesRangesList(listInstance.ChildrenStartAndEndPages);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D7E RID: 28030 RVA: 0x001BFF40 File Offset: 0x001BE140
		private void WriteListContentInstance(ListContentInstance listContentInstance)
		{
			if (listContentInstance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ListContentInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteInstanceInfoOwnerBase(listContentInstance);
			this.m_writer.WriteInt32(listContentInstance.UniqueName);
			this.WriteReportItemColInstance(listContentInstance.ReportItemColInstance);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D7F RID: 28031 RVA: 0x001BFFA4 File Offset: 0x001BE1A4
		private void WriteMatrixInstance(MatrixInstance matrixInstance)
		{
			if (matrixInstance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.MatrixInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemInstanceBase(matrixInstance);
			this.WriteReportItemInstance(matrixInstance.CornerContent);
			this.WriteMatrixHeadingInstanceList(matrixInstance.ColumnInstances);
			this.WriteMatrixHeadingInstanceList(matrixInstance.RowInstances);
			this.WriteMatrixCellInstancesList(matrixInstance.Cells);
			this.m_writer.WriteInt32(matrixInstance.InstanceCountOfInnerRowWithPageBreak);
			this.WriteRenderingPagesRangesList(matrixInstance.ChildrenStartAndEndPages);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D80 RID: 28032 RVA: 0x001C0038 File Offset: 0x001BE238
		private void WriteMatrixHeadingInstance(MatrixHeadingInstance matrixHeadingInstance)
		{
			if (matrixHeadingInstance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.MatrixHeadingInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteInstanceInfoOwnerBase(matrixHeadingInstance);
			this.m_writer.WriteInt32(matrixHeadingInstance.UniqueName);
			this.WriteReportItemInstance(matrixHeadingInstance.Content);
			this.WriteMatrixHeadingInstanceList(matrixHeadingInstance.SubHeadingInstances);
			this.m_writer.WriteBoolean(matrixHeadingInstance.IsSubtotal);
			this.WriteRenderingPagesRangesList(matrixHeadingInstance.ChildrenStartAndEndPages);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D81 RID: 28033 RVA: 0x001C00C4 File Offset: 0x001BE2C4
		private void WriteMatrixCellInstance(MatrixCellInstance matrixCellInstance)
		{
			if (matrixCellInstance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.MatrixCellInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteInstanceInfoOwnerBase(matrixCellInstance);
			ReportItemInstance content = matrixCellInstance.Content;
			ReportItem reportItem = ((content == null) ? null : content.ReportItemDef);
			this.WriteReportItemReference(reportItem);
			this.WriteReportItemInstance(matrixCellInstance.Content);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D82 RID: 28034 RVA: 0x001C0130 File Offset: 0x001BE330
		private void WriteMatrixSubtotalCellInstance(MatrixSubtotalCellInstance matrixSubtotalCellInstance)
		{
			Global.Tracer.Assert(matrixSubtotalCellInstance != null);
			ObjectType objectType = ObjectType.MatrixSubtotalCellInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteInstanceInfoOwnerBase(matrixSubtotalCellInstance);
			Global.Tracer.Assert(matrixSubtotalCellInstance.SubtotalHeadingInstance != null, "(null != matrixSubtotalCellInstance.SubtotalHeadingInstance)");
			this.WriteMatrixCellInstance(matrixSubtotalCellInstance);
			this.m_writer.WriteInt32(matrixSubtotalCellInstance.SubtotalHeadingInstance.UniqueName);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D83 RID: 28035 RVA: 0x001C01AC File Offset: 0x001BE3AC
		private void WriteChartInstance(ChartInstance chartInstance)
		{
			if (chartInstance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ChartInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemInstanceBase(chartInstance);
			this.WriteMultiChartInstanceList(chartInstance.MultiCharts);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D84 RID: 28036 RVA: 0x001C0200 File Offset: 0x001BE400
		private void WriteMultiChartInstance(MultiChartInstance multiChartInstance)
		{
			if (multiChartInstance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.MultiChartInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteChartHeadingInstanceList(multiChartInstance.ColumnInstances);
			this.WriteChartHeadingInstanceList(multiChartInstance.RowInstances);
			this.WriteChartDataPointInstancesList(multiChartInstance.DataPoints);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D85 RID: 28037 RVA: 0x001C0264 File Offset: 0x001BE464
		private void WriteChartHeadingInstance(ChartHeadingInstance chartHeadingInstance)
		{
			if (chartHeadingInstance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ChartHeadingInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteInstanceInfoOwnerBase(chartHeadingInstance);
			this.m_writer.WriteInt32(chartHeadingInstance.UniqueName);
			this.WriteChartHeadingInstanceList(chartHeadingInstance.SubHeadingInstances);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D86 RID: 28038 RVA: 0x001C02C8 File Offset: 0x001BE4C8
		private void WriteChartDataPointInstance(ChartDataPointInstance dataPointInstance)
		{
			if (dataPointInstance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ChartDataPointInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteInstanceInfoOwnerBase(dataPointInstance);
			this.m_writer.WriteInt32(dataPointInstance.UniqueName);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D87 RID: 28039 RVA: 0x001C0320 File Offset: 0x001BE520
		private void WriteAxisInstance(AxisInstance axisInstance)
		{
			if (axisInstance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.AxisInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteInt32(axisInstance.UniqueName);
			this.WriteChartTitleInstance(axisInstance.Title);
			this.WriteVariants(axisInstance.StyleAttributeValues);
			this.WriteVariants(axisInstance.MajorGridLinesStyleAttributeValues);
			this.WriteVariants(axisInstance.MinorGridLinesStyleAttributeValues);
			this.WriteVariant(axisInstance.MinValue);
			this.WriteVariant(axisInstance.MaxValue);
			this.WriteVariant(axisInstance.CrossAtValue);
			this.WriteVariant(axisInstance.MajorIntervalValue);
			this.WriteVariant(axisInstance.MinorIntervalValue);
			this.WriteDataValueInstanceList(axisInstance.CustomPropertyInstances);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D88 RID: 28040 RVA: 0x001C03EC File Offset: 0x001BE5EC
		private void WriteChartTitleInstance(ChartTitleInstance chartTitleInstance)
		{
			if (chartTitleInstance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.ChartTitleInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteInt32(chartTitleInstance.UniqueName);
			this.m_writer.WriteString(chartTitleInstance.Caption);
			this.WriteVariants(chartTitleInstance.StyleAttributeValues);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D89 RID: 28041 RVA: 0x001C045C File Offset: 0x001BE65C
		private void WriteTableInstance(TableInstance tableInstance)
		{
			if (tableInstance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			Table table = (Table)tableInstance.ReportItemDef;
			ObjectType objectType = ObjectType.TableInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemInstanceBase(tableInstance);
			if (table.HeaderRows != null)
			{
				this.WriteTableRowInstances(tableInstance.HeaderRowInstances);
			}
			if (table.TableGroups != null)
			{
				this.WriteTableGroupInstanceList(tableInstance.TableGroupInstances);
			}
			else if (table.TableDetail != null)
			{
				if (table.TableDetail.SimpleDetailRows)
				{
					int num = -1;
					if (tableInstance.TableDetailInstances != null && 0 < tableInstance.TableDetailInstances.Count)
					{
						num = tableInstance.TableDetailInstances[0].UniqueName;
					}
					this.m_writer.WriteInt32(num);
				}
				this.WriteTableDetailInstanceList(tableInstance.TableDetailInstances);
			}
			if (table.FooterRows != null)
			{
				this.WriteTableRowInstances(tableInstance.FooterRowInstances);
			}
			this.WriteRenderingPagesRangesList(tableInstance.ChildrenStartAndEndPages);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D8A RID: 28042 RVA: 0x001C0550 File Offset: 0x001BE750
		private void WriteTableGroupInstance(TableGroupInstance tableGroupInstance)
		{
			if (tableGroupInstance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			TableGroup tableGroupDef = tableGroupInstance.TableGroupDef;
			Table table = (Table)tableGroupDef.DataRegionDef;
			ObjectType objectType = ObjectType.TableGroupInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteInstanceInfoOwnerBase(tableGroupInstance);
			this.m_writer.WriteInt32(tableGroupInstance.UniqueName);
			if (tableGroupDef.HeaderRows != null)
			{
				this.WriteTableRowInstances(tableGroupInstance.HeaderRowInstances);
			}
			if (tableGroupDef.FooterRows != null)
			{
				this.WriteTableRowInstances(tableGroupInstance.FooterRowInstances);
			}
			if (tableGroupDef.InnerHierarchy != null)
			{
				this.WriteTableGroupInstanceList(tableGroupInstance.SubGroupInstances);
			}
			else if (table.TableDetail != null)
			{
				if (table.TableDetail.SimpleDetailRows)
				{
					int num = -1;
					if (tableGroupInstance.TableDetailInstances != null && 0 < tableGroupInstance.TableDetailInstances.Count)
					{
						num = tableGroupInstance.TableDetailInstances[0].UniqueName;
					}
					this.m_writer.WriteInt32(num);
				}
				this.WriteTableDetailInstanceList(tableGroupInstance.TableDetailInstances);
			}
			this.WriteRenderingPagesRangesList(tableGroupInstance.ChildrenStartAndEndPages);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D8B RID: 28043 RVA: 0x001C065C File Offset: 0x001BE85C
		private void WriteTableDetailInstance(TableDetailInstance tableDetailInstance)
		{
			if (tableDetailInstance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.TableDetailInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteInstanceInfoOwnerBase(tableDetailInstance);
			bool simpleDetailRows = tableDetailInstance.TableDetailDef.SimpleDetailRows;
			if (simpleDetailRows)
			{
				this.m_writeUniqueName = false;
			}
			else
			{
				this.m_writer.WriteInt32(tableDetailInstance.UniqueName);
			}
			this.WriteTableRowInstances(tableDetailInstance.DetailRowInstances);
			if (simpleDetailRows)
			{
				this.m_writeUniqueName = true;
			}
			this.m_writer.EndObject();
		}

		// Token: 0x06006D8C RID: 28044 RVA: 0x001C06E0 File Offset: 0x001BE8E0
		private void WriteTableRowInstance(TableRowInstance tableRowInstance)
		{
			if (tableRowInstance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.TableRowInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteInstanceInfoOwnerBase(tableRowInstance);
			if (this.m_writeUniqueName)
			{
				this.m_writer.WriteInt32(tableRowInstance.UniqueName);
			}
			this.WriteReportItemColInstance(tableRowInstance.TableRowReportItemColInstance);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D8D RID: 28045 RVA: 0x001C074C File Offset: 0x001BE94C
		private void WriteTableColumnInstance(TableColumnInstance tableColumnInstance)
		{
			if (tableColumnInstance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.TableColumnInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteInt32(tableColumnInstance.UniqueName);
			this.m_writer.WriteBoolean(tableColumnInstance.StartHidden);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D8E RID: 28046 RVA: 0x001C07AC File Offset: 0x001BE9AC
		private void WriteOWCChartInstance(OWCChartInstance chartInstance)
		{
			if (chartInstance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.OWCChartInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemInstanceBase(chartInstance);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D8F RID: 28047 RVA: 0x001C07F0 File Offset: 0x001BE9F0
		private void WriteCustomReportItemInstance(CustomReportItemInstance instance)
		{
			if (instance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.CustomReportItemInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemInstanceBase(instance);
			this.WriteReportItemColInstance(instance.AltReportItemColInstance);
			this.WriteCustomReportItemHeadingInstanceList(instance.ColumnInstances);
			this.WriteCustomReportItemHeadingInstanceList(instance.RowInstances);
			this.WriteCustomReportItemCellInstancesList(instance.Cells);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D90 RID: 28048 RVA: 0x001C0868 File Offset: 0x001BEA68
		private void WriteCustomReportItemHeadingInstance(CustomReportItemHeadingInstance headingInstance)
		{
			if (headingInstance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.CustomReportItemHeadingInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteCustomReportItemHeadingInstanceList(headingInstance.SubHeadingInstances);
			this.WriteCustomReportItemHeadingReference(headingInstance.HeadingDefinition);
			this.m_writer.WriteInt32(headingInstance.HeadingCellIndex);
			this.m_writer.WriteInt32(headingInstance.HeadingSpan);
			this.WriteDataValueInstanceList(headingInstance.CustomPropertyInstances);
			this.m_writer.WriteString(headingInstance.Label);
			this.WriteVariantList(headingInstance.GroupExpressionValues, false);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D91 RID: 28049 RVA: 0x001C090C File Offset: 0x001BEB0C
		private void WriteCustomReportItemCellInstance(CustomReportItemCellInstance cellInstance)
		{
			if (cellInstance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.CustomReportItemCellInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.m_writer.WriteInt32(cellInstance.RowIndex);
			this.m_writer.WriteInt32(cellInstance.ColumnIndex);
			this.WriteDataValueInstanceList(cellInstance.DataValueInstances);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D92 RID: 28050 RVA: 0x001C097C File Offset: 0x001BEB7C
		private void WritePageSectionInstance(PageSectionInstance instance)
		{
			if (instance == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			ObjectType objectType = ObjectType.PageSectionInstance;
			this.DeclareType(objectType);
			this.m_writer.StartObject(objectType);
			this.WriteReportItemInstanceBase(instance);
			this.m_writer.WriteInt32(instance.PageNumber);
			this.WriteReportItemColInstance(instance.ReportItemColInstance);
			this.m_writer.EndObject();
		}

		// Token: 0x06006D93 RID: 28051 RVA: 0x001C09E0 File Offset: 0x001BEBE0
		private void WriteVariant(object variant)
		{
			this.WriteVariant(variant, false);
		}

		// Token: 0x06006D94 RID: 28052 RVA: 0x001C09EC File Offset: 0x001BEBEC
		private void WriteVariant(object variant, bool convertDBNull)
		{
			if (variant == null)
			{
				Global.Tracer.Assert(!convertDBNull);
				this.m_writer.WriteNull();
				return;
			}
			if (DBNull.Value == variant)
			{
				Global.Tracer.Assert(convertDBNull, "(convertDBNull)");
				this.m_writer.WriteNull();
				return;
			}
			if (variant is string)
			{
				this.m_writer.WriteString((string)variant);
				return;
			}
			if (variant is char)
			{
				this.m_writer.WriteChar((char)variant);
				return;
			}
			if (variant is bool)
			{
				this.m_writer.WriteBoolean((bool)variant);
				return;
			}
			if (variant is short)
			{
				this.m_writer.WriteInt16((short)variant);
				return;
			}
			if (variant is int)
			{
				this.m_writer.WriteInt32((int)variant);
				return;
			}
			if (variant is long)
			{
				this.m_writer.WriteInt64((long)variant);
				return;
			}
			if (variant is ushort)
			{
				this.m_writer.WriteUInt16((ushort)variant);
				return;
			}
			if (variant is uint)
			{
				this.m_writer.WriteUInt32((uint)variant);
				return;
			}
			if (variant is ulong)
			{
				this.m_writer.WriteUInt64((ulong)variant);
				return;
			}
			if (variant is byte)
			{
				this.m_writer.WriteByte((byte)variant);
				return;
			}
			if (variant is sbyte)
			{
				this.m_writer.WriteSByte((sbyte)variant);
				return;
			}
			if (variant is float)
			{
				this.m_writer.WriteSingle((float)variant);
				return;
			}
			if (variant is double)
			{
				this.m_writer.WriteDouble((double)variant);
				return;
			}
			if (variant is decimal)
			{
				this.m_writer.WriteDecimal((decimal)variant);
				return;
			}
			if (variant is DateTime)
			{
				this.m_writer.WriteDateTime((DateTime)variant);
				return;
			}
			IntermediateFormatWriter.Assert(variant is TimeSpan);
			this.m_writer.WriteTimeSpan((TimeSpan)variant);
		}

		// Token: 0x06006D95 RID: 28053 RVA: 0x001C0BE0 File Offset: 0x001BEDE0
		private bool WriteRecordFields(RecordField[] recordFields, RecordSetPropertyNamesList aliasPropertyNames)
		{
			bool flag = true;
			if (recordFields == null)
			{
				this.m_writer.WriteNull();
			}
			else
			{
				this.m_writer.StartArray(recordFields.Length);
				for (int i = 0; i < recordFields.Length; i++)
				{
					if (aliasPropertyNames != null && aliasPropertyNames[i] != null)
					{
						recordFields[i].PopulateFieldPropertyValues(aliasPropertyNames[i].PropertyNames);
					}
					if (!this.WriteRecordField(recordFields[i]))
					{
						flag = false;
					}
				}
				this.m_writer.EndArray();
			}
			return flag;
		}

		// Token: 0x06006D96 RID: 28054 RVA: 0x001C0C54 File Offset: 0x001BEE54
		private bool WriteRecordField(RecordField recordField)
		{
			bool flag = true;
			if (recordField == null)
			{
				this.m_writer.WriteNull();
			}
			else
			{
				ObjectType objectType = ObjectType.RecordField;
				this.DeclareType(objectType);
				this.m_writer.StartObject(objectType);
				if (recordField.IsOverflow)
				{
					this.m_writer.WriteDataFieldStatus(DataFieldStatus.Overflow);
				}
				else if (recordField.IsError)
				{
					this.m_writer.WriteDataFieldStatus(DataFieldStatus.IsError);
				}
				else if (recordField.IsUnSupportedDataType || !this.WriteFieldValue(recordField.FieldValue))
				{
					this.m_writer.WriteDataFieldStatus(DataFieldStatus.UnSupportedDataType);
					flag = false;
				}
				this.m_writer.WriteBoolean(recordField.IsAggregationField);
				this.WriteVariantList(recordField.FieldPropertyValues, false);
				this.m_writer.EndObject();
			}
			return flag;
		}

		// Token: 0x06006D97 RID: 28055 RVA: 0x001C0D0C File Offset: 0x001BEF0C
		private bool WriteFieldValue(object variant)
		{
			if (variant == null)
			{
				this.m_writer.WriteNull();
			}
			else if (variant is string)
			{
				this.m_writer.WriteString((string)variant);
			}
			else if (variant is char)
			{
				this.m_writer.WriteChar((char)variant);
			}
			else if (variant is char[])
			{
				this.m_writer.WriteChars((char[])variant);
			}
			else if (variant is bool)
			{
				this.m_writer.WriteBoolean((bool)variant);
			}
			else if (variant is short)
			{
				this.m_writer.WriteInt16((short)variant);
			}
			else if (variant is int)
			{
				this.m_writer.WriteInt32((int)variant);
			}
			else if (variant is long)
			{
				this.m_writer.WriteInt64((long)variant);
			}
			else if (variant is ushort)
			{
				this.m_writer.WriteUInt16((ushort)variant);
			}
			else if (variant is uint)
			{
				this.m_writer.WriteUInt32((uint)variant);
			}
			else if (variant is ulong)
			{
				this.m_writer.WriteUInt64((ulong)variant);
			}
			else if (variant is byte)
			{
				this.m_writer.WriteByte((byte)variant);
			}
			else if (variant is byte[])
			{
				this.m_writer.WriteBytes((byte[])variant);
			}
			else if (variant is sbyte)
			{
				this.m_writer.WriteSByte((sbyte)variant);
			}
			else if (variant is float)
			{
				this.m_writer.WriteSingle((float)variant);
			}
			else if (variant is double)
			{
				this.m_writer.WriteDouble((double)variant);
			}
			else if (variant is decimal)
			{
				this.m_writer.WriteDecimal((decimal)variant);
			}
			else if (variant is DateTime)
			{
				this.m_writer.WriteDateTime((DateTime)variant);
			}
			else if (variant is TimeSpan)
			{
				this.m_writer.WriteTimeSpan((TimeSpan)variant);
			}
			else if (variant is Guid)
			{
				this.m_writer.WriteGuid((Guid)variant);
			}
			else
			{
				if (!(variant is DBNull))
				{
					return false;
				}
				this.m_writer.WriteNull();
			}
			return true;
		}

		// Token: 0x0400396D RID: 14701
		private IntermediateFormatWriter.ReportServerBinaryWriter m_writer;

		// Token: 0x0400396E RID: 14702
		private bool m_writeDeclarations;

		// Token: 0x0400396F RID: 14703
		private bool[] m_declarationsWritten;

		// Token: 0x04003970 RID: 14704
		private bool m_writeUniqueName = true;

		// Token: 0x02000CEB RID: 3307
		private sealed class ReportServerBinaryWriter
		{
			// Token: 0x06008DB1 RID: 36273 RVA: 0x00243012 File Offset: 0x00241212
			internal ReportServerBinaryWriter(Stream stream)
			{
				this.m_binaryWriter = new IntermediateFormatWriter.ReportServerBinaryWriter.BinaryWriterWrapper(stream);
			}

			// Token: 0x06008DB2 RID: 36274 RVA: 0x00243028 File Offset: 0x00241228
			internal void WriteGuid(Guid guid)
			{
				byte[] array = guid.ToByteArray();
				IntermediateFormatWriter.Assert(array != null);
				IntermediateFormatWriter.Assert(16 == array.Length);
				this.m_binaryWriter.Write(239);
				this.m_binaryWriter.Write(array);
			}

			// Token: 0x06008DB3 RID: 36275 RVA: 0x0024306E File Offset: 0x0024126E
			internal void WriteString(string stringValue)
			{
				if (stringValue == null)
				{
					this.WriteNull();
					return;
				}
				this.m_binaryWriter.Write(240);
				this.m_binaryWriter.Write(stringValue);
			}

			// Token: 0x06008DB4 RID: 36276 RVA: 0x00243096 File Offset: 0x00241296
			internal void WriteChar(char charValue)
			{
				this.m_binaryWriter.Write(243);
				this.m_binaryWriter.Write(charValue);
			}

			// Token: 0x06008DB5 RID: 36277 RVA: 0x002430B4 File Offset: 0x002412B4
			internal void WriteBoolean(bool booleanValue)
			{
				this.m_binaryWriter.Write(244);
				this.m_binaryWriter.Write(booleanValue);
			}

			// Token: 0x06008DB6 RID: 36278 RVA: 0x002430D2 File Offset: 0x002412D2
			internal void WriteInt16(short int16Value)
			{
				this.m_binaryWriter.Write(245);
				this.m_binaryWriter.Write(int16Value);
			}

			// Token: 0x06008DB7 RID: 36279 RVA: 0x002430F0 File Offset: 0x002412F0
			internal void WriteInt32(int int32Value)
			{
				this.m_binaryWriter.Write(246);
				this.m_binaryWriter.Write(int32Value);
			}

			// Token: 0x06008DB8 RID: 36280 RVA: 0x0024310E File Offset: 0x0024130E
			internal void WriteInt64(long int64Value)
			{
				this.m_binaryWriter.Write(247);
				this.m_binaryWriter.Write(int64Value);
			}

			// Token: 0x06008DB9 RID: 36281 RVA: 0x0024312C File Offset: 0x0024132C
			internal void WriteUInt16(ushort uint16Value)
			{
				this.m_binaryWriter.Write(248);
				this.m_binaryWriter.Write(uint16Value);
			}

			// Token: 0x06008DBA RID: 36282 RVA: 0x0024314A File Offset: 0x0024134A
			internal void WriteUInt32(uint uint32Value)
			{
				this.m_binaryWriter.Write(249);
				this.m_binaryWriter.Write(uint32Value);
			}

			// Token: 0x06008DBB RID: 36283 RVA: 0x00243168 File Offset: 0x00241368
			internal void WriteUInt64(ulong uint64Value)
			{
				this.m_binaryWriter.Write(250);
				this.m_binaryWriter.Write(uint64Value);
			}

			// Token: 0x06008DBC RID: 36284 RVA: 0x00243186 File Offset: 0x00241386
			internal void WriteByte(byte byteValue)
			{
				this.m_binaryWriter.Write(251);
				this.m_binaryWriter.Write(byteValue);
			}

			// Token: 0x06008DBD RID: 36285 RVA: 0x002431A4 File Offset: 0x002413A4
			internal void WriteSByte(sbyte sbyteValue)
			{
				this.m_binaryWriter.Write(252);
				this.m_binaryWriter.Write(sbyteValue);
			}

			// Token: 0x06008DBE RID: 36286 RVA: 0x002431C2 File Offset: 0x002413C2
			internal void WriteSingle(float singleValue)
			{
				this.m_binaryWriter.Write(253);
				this.m_binaryWriter.Write(singleValue);
			}

			// Token: 0x06008DBF RID: 36287 RVA: 0x002431E0 File Offset: 0x002413E0
			internal void WriteDouble(double doubleValue)
			{
				this.m_binaryWriter.Write(254);
				this.m_binaryWriter.Write(doubleValue);
			}

			// Token: 0x06008DC0 RID: 36288 RVA: 0x002431FE File Offset: 0x002413FE
			internal void WriteDecimal(decimal decimalValue)
			{
				this.m_binaryWriter.Write(byte.MaxValue);
				this.m_binaryWriter.Write(decimalValue);
			}

			// Token: 0x06008DC1 RID: 36289 RVA: 0x0024321C File Offset: 0x0024141C
			internal void WriteDateTime(DateTime dateTimeValue)
			{
				this.m_binaryWriter.Write(241);
				this.m_binaryWriter.Write(dateTimeValue.Ticks);
			}

			// Token: 0x06008DC2 RID: 36290 RVA: 0x00243240 File Offset: 0x00241440
			internal void WriteTimeSpan(TimeSpan timeSpanValue)
			{
				this.m_binaryWriter.Write(242);
				this.m_binaryWriter.Write(timeSpanValue.Ticks);
			}

			// Token: 0x06008DC3 RID: 36291 RVA: 0x00243264 File Offset: 0x00241464
			internal void WriteBytes(byte[] bytesValue)
			{
				if (bytesValue == null)
				{
					this.WriteNull();
					return;
				}
				this.m_binaryWriter.Write(5);
				this.m_binaryWriter.Write(251);
				this.m_binaryWriter.Write7BitEncodedInt(bytesValue.Length);
				this.m_binaryWriter.Write(bytesValue);
			}

			// Token: 0x06008DC4 RID: 36292 RVA: 0x002432B4 File Offset: 0x002414B4
			internal void WriteInt32s(int[] int32Values)
			{
				if (int32Values == null)
				{
					this.WriteNull();
					return;
				}
				this.m_binaryWriter.Write(5);
				this.m_binaryWriter.Write(246);
				this.m_binaryWriter.Write7BitEncodedInt(int32Values.Length);
				for (int i = 0; i < int32Values.Length; i++)
				{
					this.m_binaryWriter.Write(int32Values[i]);
				}
			}

			// Token: 0x06008DC5 RID: 36293 RVA: 0x00243314 File Offset: 0x00241514
			internal void WriteFloatArray(float[] values)
			{
				if (values == null)
				{
					this.WriteNull();
					return;
				}
				this.m_binaryWriter.Write(5);
				this.m_binaryWriter.Write(253);
				this.m_binaryWriter.Write7BitEncodedInt(values.Length);
				for (int i = 0; i < values.Length; i++)
				{
					this.m_binaryWriter.Write(values[i]);
				}
			}

			// Token: 0x06008DC6 RID: 36294 RVA: 0x00243374 File Offset: 0x00241574
			internal void WriteChars(char[] charsValue)
			{
				if (charsValue == null)
				{
					this.WriteNull();
					return;
				}
				this.m_binaryWriter.Write(5);
				this.m_binaryWriter.Write(243);
				this.m_binaryWriter.Write7BitEncodedInt(charsValue.Length);
				this.m_binaryWriter.Write(charsValue);
			}

			// Token: 0x06008DC7 RID: 36295 RVA: 0x002433C1 File Offset: 0x002415C1
			internal void StartObject(ObjectType objectType)
			{
				this.m_binaryWriter.Write(1);
				this.m_binaryWriter.Write7BitEncodedInt((int)objectType);
			}

			// Token: 0x06008DC8 RID: 36296 RVA: 0x002433DB File Offset: 0x002415DB
			internal void EndObject()
			{
				this.m_binaryWriter.Write(2);
			}

			// Token: 0x06008DC9 RID: 36297 RVA: 0x002433E9 File Offset: 0x002415E9
			internal void WriteNull()
			{
				this.m_binaryWriter.Write(0);
			}

			// Token: 0x06008DCA RID: 36298 RVA: 0x002433F7 File Offset: 0x002415F7
			internal void WriteReference(ObjectType objectType, int referenceValue)
			{
				this.m_binaryWriter.Write(3);
				this.m_binaryWriter.Write7BitEncodedInt((int)objectType);
				this.m_binaryWriter.Write(referenceValue);
			}

			// Token: 0x06008DCB RID: 36299 RVA: 0x0024341D File Offset: 0x0024161D
			internal void WriteNoTypeReference(int referenceValue)
			{
				this.m_binaryWriter.Write(3);
				this.m_binaryWriter.Write(referenceValue);
			}

			// Token: 0x06008DCC RID: 36300 RVA: 0x00243437 File Offset: 0x00241637
			internal void WriteEnum(int enumValue)
			{
				this.m_binaryWriter.Write(4);
				this.m_binaryWriter.Write7BitEncodedInt(enumValue);
			}

			// Token: 0x06008DCD RID: 36301 RVA: 0x00243451 File Offset: 0x00241651
			internal void WriteDataFieldStatus(DataFieldStatus status)
			{
				this.m_binaryWriter.Write(8);
				this.m_binaryWriter.Write7BitEncodedInt((int)status);
			}

			// Token: 0x06008DCE RID: 36302 RVA: 0x0024346B File Offset: 0x0024166B
			internal void StartArray(int count)
			{
				this.m_binaryWriter.Write(6);
				this.m_binaryWriter.Write7BitEncodedInt(count);
			}

			// Token: 0x06008DCF RID: 36303 RVA: 0x00243485 File Offset: 0x00241685
			internal void EndArray()
			{
			}

			// Token: 0x06008DD0 RID: 36304 RVA: 0x00243488 File Offset: 0x00241688
			internal void DeclareType(ObjectType objectType, Declaration declaration)
			{
				IntermediateFormatWriter.Assert(declaration != null);
				IntermediateFormatWriter.Assert(declaration.Members != null);
				this.m_binaryWriter.Write(7);
				this.m_binaryWriter.Write7BitEncodedInt((int)objectType);
				this.m_binaryWriter.Write7BitEncodedInt((int)declaration.BaseType);
				this.m_binaryWriter.Write7BitEncodedInt(declaration.Members.Count);
				for (int i = 0; i < declaration.Members.Count; i++)
				{
					IntermediateFormatWriter.Assert(declaration.Members[i] != null);
					this.m_binaryWriter.Write7BitEncodedInt((int)declaration.Members[i].MemberName);
					this.m_binaryWriter.Write((byte)declaration.Members[i].Token);
					this.m_binaryWriter.Write7BitEncodedInt((int)declaration.Members[i].ObjectType);
				}
			}

			// Token: 0x04004F71 RID: 20337
			private IntermediateFormatWriter.ReportServerBinaryWriter.BinaryWriterWrapper m_binaryWriter;

			// Token: 0x02000D4A RID: 3402
			private sealed class BinaryWriterWrapper : BinaryWriter
			{
				// Token: 0x06008FD6 RID: 36822 RVA: 0x00247EB7 File Offset: 0x002460B7
				internal BinaryWriterWrapper(Stream stream)
					: base(stream, Encoding.Unicode)
				{
				}

				// Token: 0x06008FD7 RID: 36823 RVA: 0x00247EC5 File Offset: 0x002460C5
				internal new void Write7BitEncodedInt(int int32Value)
				{
					base.Write7BitEncodedInt(int32Value);
				}
			}
		}
	}
}
