using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing.Persistence
{
	// Token: 0x020007A8 RID: 1960
	internal sealed class IntermediateFormatReader
	{
		// Token: 0x06006D98 RID: 28056 RVA: 0x001C0F72 File Offset: 0x001BF172
		internal IntermediateFormatReader(Stream stream)
		{
			this.Initialize(stream);
			this.m_definitionObjects = null;
			this.m_instanceObjects = null;
			this.m_matrixHeadingInstanceObjects = null;
			this.m_state = new IntermediateFormatReader.State();
			this.m_expectDeclarations = true;
		}

		// Token: 0x06006D99 RID: 28057 RVA: 0x001C0FAF File Offset: 0x001BF1AF
		internal IntermediateFormatReader(Stream stream, Hashtable instanceObjects)
		{
			this.Initialize(stream);
			this.m_definitionObjects = null;
			this.m_instanceObjects = instanceObjects;
			this.m_matrixHeadingInstanceObjects = null;
			this.m_state = new IntermediateFormatReader.State();
			this.m_expectDeclarations = true;
		}

		// Token: 0x06006D9A RID: 28058 RVA: 0x001C0FEC File Offset: 0x001BF1EC
		internal IntermediateFormatReader(Stream stream, Hashtable instanceObjects, Hashtable definitionObjects, IntermediateFormatVersion intermediateFormatVersion)
		{
			this.Initialize(stream);
			this.m_intermediateFormatVersion = intermediateFormatVersion;
			this.m_definitionObjects = definitionObjects;
			this.m_instanceObjects = instanceObjects;
			this.m_matrixHeadingInstanceObjects = null;
			this.m_state = new IntermediateFormatReader.State();
			this.m_expectDeclarations = true;
		}

		// Token: 0x06006D9B RID: 28059 RVA: 0x001C103C File Offset: 0x001BF23C
		internal IntermediateFormatReader(Stream stream, Hashtable instanceObjects, IntermediateFormatVersion intermediateFormatVersion)
		{
			this.Initialize(stream);
			this.m_definitionObjects = null;
			this.m_instanceObjects = instanceObjects;
			this.m_intermediateFormatVersion = intermediateFormatVersion;
			this.m_matrixHeadingInstanceObjects = null;
			this.m_state = new IntermediateFormatReader.State();
			this.m_expectDeclarations = true;
		}

		// Token: 0x06006D9C RID: 28060 RVA: 0x001C108C File Offset: 0x001BF28C
		internal IntermediateFormatReader(Stream stream, IntermediateFormatVersion intermediateFormatVersion)
		{
			this.Initialize(stream);
			this.m_definitionObjects = null;
			this.m_instanceObjects = null;
			this.m_intermediateFormatVersion = intermediateFormatVersion;
			this.m_matrixHeadingInstanceObjects = null;
			this.m_state = new IntermediateFormatReader.State();
			this.m_expectDeclarations = true;
		}

		// Token: 0x06006D9D RID: 28061 RVA: 0x001C10DC File Offset: 0x001BF2DC
		internal IntermediateFormatReader(Stream stream, IntermediateFormatReader.State state, Hashtable definitionObjects, IntermediateFormatVersion intermediateFormatVersion)
		{
			this.Initialize(stream);
			this.m_definitionObjects = definitionObjects;
			this.m_instanceObjects = null;
			this.m_intermediateFormatVersion = intermediateFormatVersion;
			this.m_matrixHeadingInstanceObjects = null;
			if (state == null)
			{
				this.m_state = IntermediateFormatReader.State.Current;
			}
			else
			{
				this.m_state = state;
			}
			this.m_expectDeclarations = false;
		}

		// Token: 0x06006D9E RID: 28062 RVA: 0x001C1138 File Offset: 0x001BF338
		internal IntermediateFormatReader(Stream stream, IntermediateFormatReader.State state, IntermediateFormatVersion intermediateFormatVersion)
		{
			this.Initialize(stream);
			this.m_definitionObjects = null;
			this.m_instanceObjects = null;
			this.m_intermediateFormatVersion = intermediateFormatVersion;
			this.m_matrixHeadingInstanceObjects = null;
			if (state == null)
			{
				this.m_state = IntermediateFormatReader.State.Current;
			}
			else
			{
				this.m_state = state;
			}
			this.m_expectDeclarations = false;
		}

		// Token: 0x06006D9F RID: 28063 RVA: 0x001C1193 File Offset: 0x001BF393
		private void Initialize(Stream stream)
		{
			IntermediateFormatReader.Assert(stream != null);
			this.m_reader = new IntermediateFormatReader.ReportServerBinaryReader(stream, new IntermediateFormatReader.ReportServerBinaryReader.DeclarationCallback(this.DeclarationCallback));
			IntermediateFormatReader.Assert(VersionStamp.Validate(this.m_reader.ReadBytes()));
		}

		// Token: 0x170025C6 RID: 9670
		// (get) Token: 0x06006DA0 RID: 28064 RVA: 0x001C11CB File Offset: 0x001BF3CB
		internal IntermediateFormatVersion IntermediateFormatVersion
		{
			get
			{
				return this.m_intermediateFormatVersion;
			}
		}

		// Token: 0x170025C7 RID: 9671
		// (get) Token: 0x06006DA1 RID: 28065 RVA: 0x001C11D3 File Offset: 0x001BF3D3
		internal Hashtable DefinitionObjects
		{
			get
			{
				return this.m_definitionObjects;
			}
		}

		// Token: 0x170025C8 RID: 9672
		// (get) Token: 0x06006DA2 RID: 28066 RVA: 0x001C11DB File Offset: 0x001BF3DB
		internal Hashtable InstanceObjects
		{
			get
			{
				return this.m_instanceObjects;
			}
		}

		// Token: 0x170025C9 RID: 9673
		// (get) Token: 0x06006DA3 RID: 28067 RVA: 0x001C11E3 File Offset: 0x001BF3E3
		internal Hashtable MatrixHeadingInstanceObjects
		{
			get
			{
				return this.m_matrixHeadingInstanceObjects;
			}
		}

		// Token: 0x170025CA RID: 9674
		// (get) Token: 0x06006DA4 RID: 28068 RVA: 0x001C11EB File Offset: 0x001BF3EB
		internal IntermediateFormatReader.State ReaderState
		{
			get
			{
				return this.m_state;
			}
		}

		// Token: 0x06006DA5 RID: 28069 RVA: 0x001C11F4 File Offset: 0x001BF3F4
		internal IntermediateFormatVersion ReadIntermediateFormatVersion()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.IntermediateFormatVersion;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			IntermediateFormatVersion intermediateFormatVersion = new IntermediateFormatVersion();
			if (this.PreRead(objectType, indexes))
			{
				intermediateFormatVersion.Major = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				intermediateFormatVersion.Minor = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				intermediateFormatVersion.Build = this.m_reader.ReadInt32();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return intermediateFormatVersion;
		}

		// Token: 0x06006DA6 RID: 28070 RVA: 0x001C12C0 File Offset: 0x001BF4C0
		internal Report ReadReport(ReportItem parent)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.Report;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			if (this.m_groupingsWithHideDuplicatesStack == null)
			{
				this.m_groupingsWithHideDuplicatesStack = new Stack<GroupingList>();
			}
			this.m_groupingsWithHideDuplicatesStack.Push(new GroupingList());
			if (this.m_textboxesWithUserSort == null)
			{
				this.m_textboxesWithUserSort = new ArrayList();
			}
			this.m_textboxesWithUserSort.Add(new TextBoxList());
			if (this.PreRead(objectType, indexes))
			{
				this.m_intermediateFormatVersion = this.ReadIntermediateFormatVersion();
			}
			if (this.m_intermediateFormatVersion == null)
			{
				this.m_intermediateFormatVersion = new IntermediateFormatVersion(8, 0, 673);
			}
			Guid guid = Guid.Empty;
			if (this.PreRead(objectType, indexes))
			{
				guid = this.m_reader.ReadGuid();
			}
			if (guid == Guid.Empty)
			{
				guid = Guid.NewGuid();
			}
			Report report = new Report(parent, this.m_intermediateFormatVersion, guid);
			this.ReadReportItemBase(report);
			if (this.PreRead(objectType, indexes))
			{
				report.Author = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.AutoRefresh = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.EmbeddedImages = this.ReadEmbeddedImageHashtable();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.PageHeader = this.ReadPageSection(true, report);
			}
			if (this.PreRead(objectType, indexes))
			{
				report.PageFooter = this.ReadPageSection(false, report);
			}
			if (this.PreRead(objectType, indexes))
			{
				report.ReportItems = this.ReadReportItemCollection(report);
			}
			if (this.PreRead(objectType, indexes))
			{
				report.DataSources = this.ReadDataSourceList();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.PageHeight = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.PageHeightValue = this.m_reader.ReadDouble();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.PageWidth = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.PageWidthValue = this.m_reader.ReadDouble();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.LeftMargin = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.LeftMarginValue = this.m_reader.ReadDouble();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.RightMargin = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.RightMarginValue = this.m_reader.ReadDouble();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.TopMargin = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.TopMarginValue = this.m_reader.ReadDouble();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.BottomMargin = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.BottomMarginValue = this.m_reader.ReadDouble();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.Columns = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.ColumnSpacing = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.ColumnSpacingValue = this.m_reader.ReadDouble();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.PageAggregates = this.ReadDataAggregateInfoList();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.CompiledCode = this.m_reader.ReadBytes();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.MergeOnePass = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.PageMergeOnePass = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.SubReportMergeTransactions = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.NeedPostGroupProcessing = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.HasPostSortAggregates = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.HasReportItemReferences = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.ShowHideType = this.ReadShowHideTypes();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.ImageStreamNames = this.ReadImageStreamNames();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.LastID = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.BodyID = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.SubReports = this.ReadSubReportList();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.HasImageStreams = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.HasLabels = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.HasBookmarks = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.ParametersNotUsedInQuery = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.Parameters = this.ReadParameterDefList();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.OneDataSetName = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.CodeModules = this.ReadStringList();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.CodeClasses = this.ReadCodeClassList();
			}
			if (this.PreRead(objectType, indexes) && this.m_intermediateFormatVersion.IsRS2000_WithSpecialRecursiveAggregates)
			{
				report.HasSpecialRecursiveAggregates = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.Language = this.ReadExpressionInfo();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.DataTransform = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.DataSchema = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.DataElementStyleAttribute = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.Code = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.HasUserSortFilter = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.CompiledCodeGeneratedWithRefusedPermissions = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.InteractiveHeight = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.InteractiveHeightValue = this.m_reader.ReadDouble();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.InteractiveWidth = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.InteractiveWidthValue = this.m_reader.ReadDouble();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.NonDetailSortFiltersInScope = this.ReadInScopeSortFilterTable();
			}
			if (this.PreRead(objectType, indexes))
			{
				report.DetailSortFiltersInScope = this.ReadInScopeSortFilterTable();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			this.ResolveReportItemReferenceForGroupings(this.m_groupingsWithHideDuplicatesStack.Pop());
			this.ResolveUserSortReferenceForTextBoxes();
			return report;
		}

		// Token: 0x06006DA7 RID: 28071 RVA: 0x001C19CC File Offset: 0x001BFBCC
		private void ResolveReportItemReferenceForGroupings(GroupingList groupingsWithHideDuplicates)
		{
			if (groupingsWithHideDuplicates == null)
			{
				return;
			}
			for (int i = 0; i < groupingsWithHideDuplicates.Count; i++)
			{
				Grouping grouping = groupingsWithHideDuplicates[i];
				IntList hideDuplicatesReportItemIDs = grouping.HideDuplicatesReportItemIDs;
				Global.Tracer.Assert(hideDuplicatesReportItemIDs != null, "(null != reportItemIDs)");
				for (int j = 0; j < hideDuplicatesReportItemIDs.Count; j++)
				{
					IDOwner definitionObject = this.GetDefinitionObject(hideDuplicatesReportItemIDs[j]);
					IntermediateFormatReader.Assert(definitionObject is ReportItem);
					grouping.AddReportItemWithHideDuplicates((ReportItem)definitionObject);
				}
				grouping.HideDuplicatesReportItemIDs = null;
			}
		}

		// Token: 0x06006DA8 RID: 28072 RVA: 0x001C1A54 File Offset: 0x001BFC54
		private void ResolveUserSortReferenceForTextBoxes()
		{
			Global.Tracer.Assert(this.m_textboxesWithUserSort != null && 0 < this.m_textboxesWithUserSort.Count && this.m_textboxesWithUserSort[this.m_textboxesWithUserSort.Count - 1] != null);
			TextBoxList textBoxList = (TextBoxList)this.m_textboxesWithUserSort[this.m_textboxesWithUserSort.Count - 1];
			for (int i = 0; i < textBoxList.Count; i++)
			{
				TextBox textBox = textBoxList[i];
				if (-1 != textBox.UserSort.SortExpressionScopeID)
				{
					IDOwner definitionObject = this.GetDefinitionObject(textBox.UserSort.SortExpressionScopeID);
					ISortFilterScope sortFilterScope = definitionObject as ISortFilterScope;
					if (sortFilterScope == null)
					{
						IntermediateFormatReader.Assert(definitionObject is ReportHierarchyNode);
						sortFilterScope = ((ReportHierarchyNode)definitionObject).Grouping;
					}
					textBox.UserSort.SortExpressionScope = sortFilterScope;
					textBox.UserSort.SortExpressionScopeID = -1;
				}
				IntList groupInSortTargetIDs = textBox.UserSort.GroupInSortTargetIDs;
				if (groupInSortTargetIDs != null)
				{
					textBox.UserSort.GroupsInSortTarget = new GroupingList(groupInSortTargetIDs.Count);
					for (int j = 0; j < groupInSortTargetIDs.Count; j++)
					{
						IDOwner definitionObject2 = this.GetDefinitionObject(groupInSortTargetIDs[j]);
						IntermediateFormatReader.Assert(definitionObject2 is ReportHierarchyNode);
						textBox.UserSort.GroupsInSortTarget.Add(((ReportHierarchyNode)definitionObject2).Grouping);
					}
					textBox.UserSort.GroupInSortTargetIDs = null;
				}
				if (-1 != textBox.UserSort.SortTargetID)
				{
					IDOwner definitionObject3 = this.GetDefinitionObject(textBox.UserSort.SortTargetID);
					ISortFilterScope sortFilterScope = definitionObject3 as ISortFilterScope;
					if (sortFilterScope == null)
					{
						IntermediateFormatReader.Assert(definitionObject3 is ReportHierarchyNode);
						sortFilterScope = ((ReportHierarchyNode)definitionObject3).Grouping;
					}
					textBox.UserSort.SortTarget = sortFilterScope;
					textBox.UserSort.SortTargetID = -1;
				}
			}
			this.m_textboxesWithUserSort.RemoveAt(this.m_textboxesWithUserSort.Count - 1);
		}

		// Token: 0x06006DA9 RID: 28073 RVA: 0x001C1C40 File Offset: 0x001BFE40
		internal ReportSnapshot ReadReportSnapshot()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.ReportSnapshot;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ReportSnapshot reportSnapshot = new ReportSnapshot();
			if (this.PreRead(objectType, indexes))
			{
				reportSnapshot.ExecutionTime = this.m_reader.ReadDateTime();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportSnapshot.Report = this.ReadReport(null);
			}
			if (this.PreRead(objectType, indexes))
			{
				reportSnapshot.Parameters = this.ReadParameterInfoCollection();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportSnapshot.ReportInstance = this.ReadReportInstance(reportSnapshot.Report);
			}
			if (this.PreRead(objectType, indexes))
			{
				reportSnapshot.HasDocumentMap = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportSnapshot.HasShowHide = this.m_reader.ReadBoolean();
			}
			if (this.m_intermediateFormatVersion.IsRS2005_WithSpecialChunkSplit)
			{
				if (this.PreRead(objectType, indexes))
				{
					reportSnapshot.HasBookmarks = this.m_reader.ReadBoolean();
				}
			}
			else
			{
				OffsetInfo offsetInfo = this.ReadOffsetInfo();
				if (offsetInfo != null)
				{
					reportSnapshot.DocumentMapOffset = offsetInfo;
					reportSnapshot.HasDocumentMap = true;
				}
				offsetInfo = this.ReadOffsetInfo();
				if (offsetInfo != null)
				{
					reportSnapshot.ShowHideSenderInfoOffset = offsetInfo;
					reportSnapshot.HasShowHide = true;
				}
				reportSnapshot.ShowHideReceiverInfoOffset = this.ReadOffsetInfo();
				reportSnapshot.QuickFindOffset = this.ReadOffsetInfo();
				indexes.CurrentIndex++;
				reportSnapshot.HasBookmarks = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportSnapshot.HasImageStreams = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportSnapshot.RequestUserName = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportSnapshot.ReportServerUrl = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportSnapshot.ReportFolder = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportSnapshot.Language = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportSnapshot.Warnings = this.ReadProcessingMessageList();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportSnapshot.PageSectionOffsets = this.ReadInt64List();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return reportSnapshot;
		}

		// Token: 0x06006DAA RID: 28074 RVA: 0x001C1E94 File Offset: 0x001C0094
		internal Report ReadReportFromSnapshot(out DateTime executionTime)
		{
			executionTime = DateTime.Now;
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.ReportSnapshot;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			Report report = null;
			if (this.PreRead(objectType, indexes))
			{
				executionTime = this.m_reader.ReadDateTime();
			}
			if (this.PreRead(objectType, indexes))
			{
				report = this.ReadReport(null);
			}
			return report;
		}

		// Token: 0x06006DAB RID: 28075 RVA: 0x001C1F2C File Offset: 0x001C012C
		internal ParameterInfoCollection ReadSnapshotParameters()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.ReportSnapshot;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ParameterInfoCollection parameterInfoCollection = null;
			if (this.PreRead(objectType, indexes))
			{
				this.m_reader.ReadDateTime();
			}
			if (this.PreRead(objectType, indexes))
			{
				this.ReadReport(null);
			}
			if (this.PreRead(objectType, indexes))
			{
				parameterInfoCollection = this.ReadParameterInfoCollection();
			}
			return parameterInfoCollection;
		}

		// Token: 0x06006DAC RID: 28076 RVA: 0x001C1FC4 File Offset: 0x001C01C4
		internal DocumentMapNode ReadDocumentMapNode()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.DocumentMapNode;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			DocumentMapNode documentMapNode = new DocumentMapNode();
			this.ReadInstanceInfoBase(documentMapNode);
			if (this.PreRead(objectType, indexes))
			{
				documentMapNode.Id = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				documentMapNode.Label = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				documentMapNode.Page = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				documentMapNode.Children = this.ReadDocumentMapNodes();
			}
			if (this.m_intermediateFormatVersion != null && !this.m_intermediateFormatVersion.IsRS2005_WithSpecialChunkSplit)
			{
				documentMapNode.Page = this.m_reader.ReadInt32();
				indexes.CurrentIndex++;
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return documentMapNode;
		}

		// Token: 0x06006DAD RID: 28077 RVA: 0x001C20DC File Offset: 0x001C02DC
		internal DocumentMapNodeInfo ReadDocumentMapNodeInfo()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.DocumentMapNode;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			DocumentMapNode documentMapNode = new DocumentMapNode();
			this.ReadInstanceInfoBase(documentMapNode);
			DocumentMapNodeInfo[] array = null;
			if (this.PreRead(objectType, indexes))
			{
				documentMapNode.Id = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				documentMapNode.Label = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				array = this.ReadDocumentMapNodesInfo();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return new DocumentMapNodeInfo(documentMapNode, array);
		}

		// Token: 0x06006DAE RID: 28078 RVA: 0x001C21C0 File Offset: 0x001C03C0
		internal bool FindDocumentMapNodePage(string documentMapId, ref int page)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return false;
			}
			ObjectType objectType = ObjectType.DocumentMapNode;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			DocumentMapNode documentMapNode = new DocumentMapNode();
			this.ReadInstanceInfoBase(documentMapNode);
			if (this.PreRead(objectType, indexes))
			{
				documentMapNode.Id = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				documentMapNode.Page = this.m_reader.ReadInt32();
			}
			if (documentMapId.Equals(documentMapNode.Id, StringComparison.Ordinal))
			{
				page = documentMapNode.Page + 1;
				return true;
			}
			bool flag = false;
			if (this.PreRead(objectType, indexes))
			{
				flag = this.FindDocumentMapNodesPage(documentMapId, ref page);
			}
			if (!flag)
			{
				this.PostRead(objectType, indexes);
				this.m_reader.ReadEndObject();
			}
			return flag;
		}

		// Token: 0x06006DAF RID: 28079 RVA: 0x001C22C0 File Offset: 0x001C04C0
		internal TokensHashtable ReadTokensHashtable()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.TokensHashtable == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			TokensHashtable tokensHashtable = new TokensHashtable(num);
			for (int i = 0; i < num; i++)
			{
				int num2 = this.m_reader.ReadInt32();
				object obj = this.ReadVariant();
				tokensHashtable.Add(num2, obj);
			}
			this.m_reader.ReadEndObject();
			return tokensHashtable;
		}

		// Token: 0x06006DB0 RID: 28080 RVA: 0x001C2360 File Offset: 0x001C0560
		internal BookmarksHashtable ReadBookmarksHashtable()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.BookmarksHashtable == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			BookmarksHashtable bookmarksHashtable = new BookmarksHashtable(num);
			for (int i = 0; i < num; i++)
			{
				string text = this.m_reader.ReadString();
				BookmarkInformation bookmarkInformation = this.ReadBookmarkInformation();
				bookmarksHashtable.Add(text, bookmarkInformation);
			}
			this.m_reader.ReadEndObject();
			return bookmarksHashtable;
		}

		// Token: 0x06006DB1 RID: 28081 RVA: 0x001C2400 File Offset: 0x001C0600
		internal BookmarkInformation FindBookmarkIdInfo(string bookmarkId)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.BookmarksHashtable == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			for (int i = 0; i < num; i++)
			{
				string text = this.m_reader.ReadString();
				BookmarkInformation bookmarkInformation = this.ReadBookmarkInformation();
				if (bookmarkId.Equals(text, StringComparison.Ordinal))
				{
					return bookmarkInformation;
				}
			}
			return null;
		}

		// Token: 0x06006DB2 RID: 28082 RVA: 0x001C2490 File Offset: 0x001C0690
		internal DrillthroughInformation FindDrillthroughIdInfo(string drillthroughId)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			bool flag = false;
			TokensHashtable tokensHashtable = null;
			if (ObjectType.ReportDrillthroughInfo == this.m_reader.ObjectType)
			{
				flag = true;
				tokensHashtable = this.ReadTokensHashtable();
				IntermediateFormatReader.Assert(this.m_reader.Read());
				if (this.m_reader.Token == Token.Null)
				{
					return null;
				}
			}
			IntermediateFormatReader.Assert(ObjectType.DrillthroughHashtable == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			for (int i = 0; i < num; i++)
			{
				string text = this.m_reader.ReadString();
				DrillthroughInformation drillthroughInformation = this.ReadDrillthroughInformation(flag);
				if (drillthroughId.Equals(text, StringComparison.Ordinal))
				{
					if (flag)
					{
						drillthroughInformation.ResolveDataSetTokenIDs(tokensHashtable);
					}
					return drillthroughInformation;
				}
			}
			return null;
		}

		// Token: 0x06006DB3 RID: 28083 RVA: 0x001C256C File Offset: 0x001C076C
		internal SenderInformationHashtable ReadSenderInformationHashtable()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.SenderInformationHashtable == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			SenderInformationHashtable senderInformationHashtable = new SenderInformationHashtable(num);
			for (int i = 0; i < num; i++)
			{
				int num2 = this.m_reader.ReadInt32();
				SenderInformation senderInformation = this.ReadSenderInformation();
				senderInformationHashtable.Add(num2, senderInformation);
			}
			this.m_reader.ReadEndObject();
			return senderInformationHashtable;
		}

		// Token: 0x06006DB4 RID: 28084 RVA: 0x001C2608 File Offset: 0x001C0808
		internal ReceiverInformationHashtable ReadReceiverInformationHashtable()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.ReceiverInformationHashtable == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			ReceiverInformationHashtable receiverInformationHashtable = new ReceiverInformationHashtable(num);
			for (int i = 0; i < num; i++)
			{
				int num2 = this.m_reader.ReadInt32();
				ReceiverInformation receiverInformation = this.ReadReceiverInformation();
				receiverInformationHashtable.Add(num2, receiverInformation);
			}
			this.m_reader.ReadEndObject();
			return receiverInformationHashtable;
		}

		// Token: 0x06006DB5 RID: 28085 RVA: 0x001C26A4 File Offset: 0x001C08A4
		internal QuickFindHashtable ReadQuickFindHashtable()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.QuickFindHashtable == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			QuickFindHashtable quickFindHashtable = new QuickFindHashtable(num);
			for (int i = 0; i < num; i++)
			{
				int num2 = this.m_reader.ReadInt32();
				ReportItemInstance reportItemInstance = this.ReadReportItemInstanceReference();
				quickFindHashtable.Add(num2, reportItemInstance);
			}
			this.m_reader.ReadEndObject();
			return quickFindHashtable;
		}

		// Token: 0x06006DB6 RID: 28086 RVA: 0x001C2740 File Offset: 0x001C0940
		internal SortFilterEventInfoHashtable ReadSortFilterEventInfoHashtable()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.SortFilterEventInfoHashtable == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			SortFilterEventInfoHashtable sortFilterEventInfoHashtable = new SortFilterEventInfoHashtable(num);
			for (int i = 0; i < num; i++)
			{
				int num2 = this.m_reader.ReadInt32();
				SortFilterEventInfo sortFilterEventInfo = this.ReadSortFilterEventInfo(true);
				sortFilterEventInfoHashtable.Add(num2, sortFilterEventInfo);
			}
			this.m_reader.ReadEndObject();
			return sortFilterEventInfoHashtable;
		}

		// Token: 0x06006DB7 RID: 28087 RVA: 0x001C27E0 File Offset: 0x001C09E0
		internal List<PageSectionInstance> ReadPageSections(int requestedPageNumber, int startPage, PageSection headerDef, PageSection footerDef)
		{
			IntermediateFormatReader.Assert(startPage >= 0);
			int num = (requestedPageNumber + 1) * 2;
			int num2 = 2;
			if (startPage == 0)
			{
				IntermediateFormatReader.Assert(this.m_reader.Read());
				if (this.m_reader.Token == Token.Null)
				{
					return null;
				}
				IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
				IntermediateFormatReader.Assert(ObjectType.PageSectionInstanceList == this.m_reader.ObjectType);
				num2 = this.m_reader.ReadArray();
				if (requestedPageNumber < 0)
				{
					num = num2;
				}
				IntermediateFormatReader.Assert(num2 % 2 == 0);
			}
			List<PageSectionInstance> list = new List<PageSectionInstance>((requestedPageNumber < 0) ? num2 : 2);
			for (int i = startPage * 2; i < num; i++)
			{
				PageSection pageSection = ((i % 2 == 0) ? headerDef : footerDef);
				PageSectionInstance pageSectionInstance = this.ReadPageSectionInstance(pageSection);
				if (requestedPageNumber < 0)
				{
					list.Add(pageSectionInstance);
				}
				else if (requestedPageNumber == i >> 1)
				{
					list.Add(pageSectionInstance);
				}
			}
			return list;
		}

		// Token: 0x06006DB8 RID: 28088 RVA: 0x001C28BC File Offset: 0x001C0ABC
		internal ActionInstance ReadActionInstance(Microsoft.ReportingServices.ReportProcessing.Action action)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.ActionInstance;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ActionInstance actionInstance = new ActionInstance();
			if (this.PreRead(objectType, indexes))
			{
				ActionItemList actionItemList = null;
				if (action != null)
				{
					actionItemList = action.ActionItems;
				}
				actionInstance.ActionItemsValues = this.ReadActionItemInstanceList(actionItemList);
			}
			if (this.PreRead(objectType, indexes))
			{
				actionInstance.StyleAttributeValues = this.ReadVariants();
			}
			if (this.PreRead(objectType, indexes))
			{
				actionInstance.UniqueName = this.m_reader.ReadInt32();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return actionInstance;
		}

		// Token: 0x06006DB9 RID: 28089 RVA: 0x001C2988 File Offset: 0x001C0B88
		private ActionItemInstanceList ReadActionItemInstanceList(ActionItemList actionItemList)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.ActionItemInstanceList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			ActionItemInstanceList actionItemInstanceList = new ActionItemInstanceList(num);
			for (int i = 0; i < num; i++)
			{
				ActionItem actionItem = null;
				if (actionItemList != null)
				{
					actionItem = actionItemList[i];
				}
				actionItemInstanceList.Add(this.ReadActionItemInstance(actionItem));
			}
			this.m_reader.ReadEndObject();
			return actionItemInstanceList;
		}

		// Token: 0x06006DBA RID: 28090 RVA: 0x001C2A28 File Offset: 0x001C0C28
		internal ActionItemInstance ReadActionItemInstance(ActionItem actionItemDef)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ActionItemInstance actionItemInstance = new ActionItemInstance();
			if (this.m_intermediateFormatVersion.IsRS2005_WithMultipleActions)
			{
				ObjectType objectType = ObjectType.ActionItemInstance;
				IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
				IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
				IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
				if (this.PreRead(objectType, indexes))
				{
					actionItemInstance.HyperLinkURL = this.m_reader.ReadString();
				}
				if (this.PreRead(objectType, indexes))
				{
					actionItemInstance.BookmarkLink = this.m_reader.ReadString();
				}
				if (this.PreRead(objectType, indexes))
				{
					actionItemInstance.DrillthroughReportName = this.m_reader.ReadString();
				}
				if (this.PreRead(objectType, indexes))
				{
					actionItemInstance.DrillthroughParametersValues = this.ReadVariants(true, true);
				}
				if (this.PreRead(objectType, indexes))
				{
					actionItemInstance.DrillthroughParametersOmits = this.ReadBoolList();
				}
				if (this.PreRead(objectType, indexes))
				{
					actionItemInstance.Label = this.m_reader.ReadString();
				}
				if (actionItemDef != null && this.m_intermediateFormatVersion.IsRS2005_WithSharedDrillthroughParams)
				{
					ParameterValueList drillthroughParameters = actionItemDef.DrillthroughParameters;
					if (drillthroughParameters != null && drillthroughParameters.Count > 0)
					{
						for (int i = 0; i < drillthroughParameters.Count; i++)
						{
							ExpressionInfo value = drillthroughParameters[i].Value;
							if (value != null && value.Type == ExpressionInfo.Types.Token)
							{
								DataSet dataSet = this.m_definitionObjects[value.IntValue] as DataSet;
								if (dataSet != null && dataSet.Query != null)
								{
									actionItemInstance.DrillthroughParametersValues[i] = dataSet.Query.RewrittenCommandText;
								}
							}
						}
					}
				}
				this.PostRead(objectType, indexes);
			}
			else
			{
				ObjectType objectType2 = ObjectType.ActionInstance;
				IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
				IntermediateFormatReader.Assert(objectType2 == this.m_reader.ObjectType);
				actionItemInstance.HyperLinkURL = this.m_reader.ReadString();
				actionItemInstance.BookmarkLink = this.m_reader.ReadString();
				actionItemInstance.DrillthroughReportName = this.m_reader.ReadString();
				actionItemInstance.DrillthroughParametersValues = this.ReadVariants(true, true);
				actionItemInstance.DrillthroughParametersOmits = this.ReadBoolList();
			}
			this.m_reader.ReadEndObject();
			return actionItemInstance;
		}

		// Token: 0x06006DBB RID: 28091 RVA: 0x001C2C68 File Offset: 0x001C0E68
		internal ReportItemColInstanceInfo ReadReportItemColInstanceInfo()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.ReportItemColInstanceInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ReportItemColInstanceInfo reportItemColInstanceInfo = new ReportItemColInstanceInfo();
			this.ReadInstanceInfoBase(reportItemColInstanceInfo);
			if (this.PreRead(objectType, indexes))
			{
				reportItemColInstanceInfo.ChildrenNonComputedUniqueNames = this.ReadNonComputedUniqueNamess();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return reportItemColInstanceInfo;
		}

		// Token: 0x06006DBC RID: 28092 RVA: 0x001C2CFC File Offset: 0x001C0EFC
		internal ListContentInstanceInfo ReadListContentInstanceInfo()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.ListContentInstanceInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ListContentInstanceInfo listContentInstanceInfo = new ListContentInstanceInfo();
			this.ReadInstanceInfoBase(listContentInstanceInfo);
			if (this.PreRead(objectType, indexes))
			{
				listContentInstanceInfo.StartHidden = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				listContentInstanceInfo.Label = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				listContentInstanceInfo.CustomPropertyInstances = this.ReadDataValueInstanceList();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return listContentInstanceInfo;
		}

		// Token: 0x06006DBD RID: 28093 RVA: 0x001C2DC4 File Offset: 0x001C0FC4
		internal MatrixHeadingInstanceInfo ReadMatrixHeadingInstanceInfoBase()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			if (ObjectType.MatrixHeadingInstanceInfo == this.m_reader.ObjectType)
			{
				MatrixHeadingInstanceInfo matrixHeadingInstanceInfo = new MatrixHeadingInstanceInfo();
				this.ReadMatrixHeadingInstanceInfo(matrixHeadingInstanceInfo);
				return matrixHeadingInstanceInfo;
			}
			IntermediateFormatReader.Assert(ObjectType.MatrixSubtotalHeadingInstanceInfo == this.m_reader.ObjectType);
			MatrixSubtotalHeadingInstanceInfo matrixSubtotalHeadingInstanceInfo = new MatrixSubtotalHeadingInstanceInfo();
			this.ReadMatrixSubtotalHeadingInstanceInfo(matrixSubtotalHeadingInstanceInfo);
			return matrixSubtotalHeadingInstanceInfo;
		}

		// Token: 0x06006DBE RID: 28094 RVA: 0x001C2E48 File Offset: 0x001C1048
		internal void ReadMatrixHeadingInstanceInfo(MatrixHeadingInstanceInfo instanceInfo)
		{
			IntermediateFormatReader.Assert(this.m_reader.Token > Token.Null);
			ObjectType objectType = ObjectType.MatrixHeadingInstanceInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			this.ReadInstanceInfoBase(instanceInfo);
			if (this.PreRead(objectType, indexes))
			{
				instanceInfo.ContentUniqueNames = this.ReadNonComputedUniqueNames();
			}
			if (this.PreRead(objectType, indexes))
			{
				instanceInfo.StartHidden = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				instanceInfo.HeadingCellIndex = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				instanceInfo.HeadingSpan = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				instanceInfo.GroupExpressionValue = this.ReadVariant();
			}
			if (this.PreRead(objectType, indexes))
			{
				instanceInfo.Label = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				instanceInfo.CustomPropertyInstances = this.ReadDataValueInstanceList();
			}
			if (this.m_intermediateFormatVersion.IsRS2000_RTM_orNewer && this.m_intermediateFormatVersion.IsRS2005_IDW9_orOlder)
			{
				indexes.CurrentIndex++;
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
		}

		// Token: 0x06006DBF RID: 28095 RVA: 0x001C2F88 File Offset: 0x001C1188
		internal void ReadMatrixSubtotalHeadingInstanceInfo(MatrixSubtotalHeadingInstanceInfo instanceInfo)
		{
			IntermediateFormatReader.Assert(this.m_reader.Token > Token.Null);
			ObjectType objectType = ObjectType.MatrixSubtotalHeadingInstanceInfo;
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			this.ReadInstanceInfoBase(instanceInfo);
			IntermediateFormatReader.Assert(this.m_reader.Read());
			this.ReadMatrixHeadingInstanceInfo(instanceInfo);
			instanceInfo.StyleAttributeValues = this.ReadVariants();
			this.m_reader.ReadEndObject();
		}

		// Token: 0x06006DC0 RID: 28096 RVA: 0x001C3008 File Offset: 0x001C1208
		internal MatrixCellInstanceInfo ReadMatrixCellInstanceInfo()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.MatrixCellInstanceInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			MatrixCellInstanceInfo matrixCellInstanceInfo = new MatrixCellInstanceInfo();
			this.ReadInstanceInfoBase(matrixCellInstanceInfo);
			if (this.PreRead(objectType, indexes))
			{
				matrixCellInstanceInfo.ContentUniqueNames = this.ReadNonComputedUniqueNames();
			}
			if (this.PreRead(objectType, indexes))
			{
				matrixCellInstanceInfo.RowIndex = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				matrixCellInstanceInfo.ColumnIndex = this.m_reader.ReadInt32();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return matrixCellInstanceInfo;
		}

		// Token: 0x06006DC1 RID: 28097 RVA: 0x001C30D0 File Offset: 0x001C12D0
		internal ChartHeadingInstanceInfo ReadChartHeadingInstanceInfo()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.ChartHeadingInstanceInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ChartHeadingInstanceInfo chartHeadingInstanceInfo = new ChartHeadingInstanceInfo();
			this.ReadInstanceInfoBase(chartHeadingInstanceInfo);
			if (this.PreRead(objectType, indexes))
			{
				chartHeadingInstanceInfo.HeadingLabel = this.ReadVariant();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartHeadingInstanceInfo.HeadingCellIndex = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartHeadingInstanceInfo.HeadingSpan = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartHeadingInstanceInfo.GroupExpressionValue = this.ReadVariant();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartHeadingInstanceInfo.StaticGroupingIndex = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartHeadingInstanceInfo.CustomPropertyInstances = this.ReadDataValueInstanceList();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return chartHeadingInstanceInfo;
		}

		// Token: 0x06006DC2 RID: 28098 RVA: 0x001C31E4 File Offset: 0x001C13E4
		internal ChartDataPointInstanceInfo ReadChartDataPointInstanceInfo(ChartDataPointList chartDataPoints)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.ChartDataPointInstanceInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ChartDataPointInstanceInfo chartDataPointInstanceInfo = new ChartDataPointInstanceInfo();
			this.ReadInstanceInfoBase(chartDataPointInstanceInfo);
			if (this.PreRead(objectType, indexes))
			{
				chartDataPointInstanceInfo.DataPointIndex = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartDataPointInstanceInfo.DataValues = this.ReadVariants();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartDataPointInstanceInfo.DataLabelValue = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartDataPointInstanceInfo.DataLabelStyleAttributeValues = this.ReadVariants();
			}
			if (this.PreRead(objectType, indexes))
			{
				if (this.m_intermediateFormatVersion.IsRS2005_WithMultipleActions)
				{
					ChartDataPoint chartDataPoint = chartDataPoints[chartDataPointInstanceInfo.DataPointIndex];
					chartDataPointInstanceInfo.Action = this.ReadActionInstance(chartDataPoint.Action);
				}
				else
				{
					ActionItemInstance actionItemInstance = this.ReadActionItemInstance(null);
					if (actionItemInstance != null)
					{
						chartDataPointInstanceInfo.Action = new ActionInstance(actionItemInstance);
					}
				}
			}
			if (this.PreRead(objectType, indexes))
			{
				chartDataPointInstanceInfo.StyleAttributeValues = this.ReadVariants();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartDataPointInstanceInfo.MarkerStyleAttributeValues = this.ReadVariants();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartDataPointInstanceInfo.CustomPropertyInstances = this.ReadDataValueInstanceList();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return chartDataPointInstanceInfo;
		}

		// Token: 0x06006DC3 RID: 28099 RVA: 0x001C335C File Offset: 0x001C155C
		internal TableGroupInstanceInfo ReadTableGroupInstanceInfo()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.TableGroupInstanceInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			TableGroupInstanceInfo tableGroupInstanceInfo = new TableGroupInstanceInfo();
			this.ReadInstanceInfoBase(tableGroupInstanceInfo);
			if (this.PreRead(objectType, indexes))
			{
				tableGroupInstanceInfo.StartHidden = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				tableGroupInstanceInfo.Label = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				tableGroupInstanceInfo.CustomPropertyInstances = this.ReadDataValueInstanceList();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return tableGroupInstanceInfo;
		}

		// Token: 0x06006DC4 RID: 28100 RVA: 0x001C3424 File Offset: 0x001C1624
		internal TableRowInstanceInfo ReadTableRowInstanceInfo()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.TableRowInstanceInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			TableRowInstanceInfo tableRowInstanceInfo = new TableRowInstanceInfo();
			this.ReadInstanceInfoBase(tableRowInstanceInfo);
			if (this.PreRead(objectType, indexes))
			{
				tableRowInstanceInfo.StartHidden = this.m_reader.ReadBoolean();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return tableRowInstanceInfo;
		}

		// Token: 0x06006DC5 RID: 28101 RVA: 0x001C34BC File Offset: 0x001C16BC
		internal LineInstanceInfo ReadLineInstanceInfo(Line line)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.LineInstanceInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			LineInstanceInfo lineInstanceInfo = new LineInstanceInfo(line);
			this.ReadReportItemInstanceInfoBase(lineInstanceInfo);
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return lineInstanceInfo;
		}

		// Token: 0x06006DC6 RID: 28102 RVA: 0x001C353C File Offset: 0x001C173C
		internal TextBoxInstanceInfo ReadTextBoxInstanceInfo(TextBox textBox)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.TextBoxInstanceInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			TextBoxInstanceInfo textBoxInstanceInfo = new TextBoxInstanceInfo(textBox);
			this.ReadReportItemInstanceInfoBase(textBoxInstanceInfo);
			if (this.PreRead(objectType, indexes))
			{
				textBoxInstanceInfo.FormattedValue = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				textBoxInstanceInfo.OriginalValue = this.ReadVariant();
			}
			bool flag = false;
			if (this.m_intermediateFormatVersion.IsRS2000_WithUnusedFieldsOptimization)
			{
				flag = true;
			}
			if ((!flag || textBox.HideDuplicates != null) && this.PreRead(objectType, indexes))
			{
				textBoxInstanceInfo.Duplicate = this.m_reader.ReadBoolean();
			}
			if ((!flag || textBox.Action != null) && this.PreRead(objectType, indexes))
			{
				if (this.m_intermediateFormatVersion.IsRS2005_WithMultipleActions)
				{
					textBoxInstanceInfo.Action = this.ReadActionInstance(textBox.Action);
				}
				else
				{
					ActionItemInstance actionItemInstance = this.ReadActionItemInstance(null);
					if (actionItemInstance != null)
					{
						textBoxInstanceInfo.Action = new ActionInstance(actionItemInstance);
					}
				}
			}
			if ((!flag || textBox.InitialToggleState != null) && this.PreRead(objectType, indexes))
			{
				textBoxInstanceInfo.InitialToggleState = this.m_reader.ReadBoolean();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return textBoxInstanceInfo;
		}

		// Token: 0x06006DC7 RID: 28103 RVA: 0x001C3698 File Offset: 0x001C1898
		internal SimpleTextBoxInstanceInfo ReadSimpleTextBoxInstanceInfo(TextBox textBox)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.SimpleTextBoxInstanceInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			SimpleTextBoxInstanceInfo simpleTextBoxInstanceInfo = new SimpleTextBoxInstanceInfo(textBox);
			this.ReadInstanceInfoBase(simpleTextBoxInstanceInfo);
			if (this.PreRead(objectType, indexes))
			{
				simpleTextBoxInstanceInfo.FormattedValue = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				simpleTextBoxInstanceInfo.OriginalValue = this.ReadVariant();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return simpleTextBoxInstanceInfo;
		}

		// Token: 0x06006DC8 RID: 28104 RVA: 0x001C374C File Offset: 0x001C194C
		internal RectangleInstanceInfo ReadRectangleInstanceInfo(Rectangle rectangle)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.RectangleInstanceInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			RectangleInstanceInfo rectangleInstanceInfo = new RectangleInstanceInfo(rectangle);
			this.ReadReportItemInstanceInfoBase(rectangleInstanceInfo);
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return rectangleInstanceInfo;
		}

		// Token: 0x06006DC9 RID: 28105 RVA: 0x001C37CC File Offset: 0x001C19CC
		internal CheckBoxInstanceInfo ReadCheckBoxInstanceInfo(CheckBox checkBox)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.CheckBoxInstanceInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			CheckBoxInstanceInfo checkBoxInstanceInfo = new CheckBoxInstanceInfo(checkBox);
			this.ReadReportItemInstanceInfoBase(checkBoxInstanceInfo);
			if (this.PreRead(objectType, indexes))
			{
				checkBoxInstanceInfo.Value = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				checkBoxInstanceInfo.Duplicate = this.m_reader.ReadBoolean();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return checkBoxInstanceInfo;
		}

		// Token: 0x06006DCA RID: 28106 RVA: 0x001C3880 File Offset: 0x001C1A80
		internal ImageInstanceInfo ReadImageInstanceInfo(Image image)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.ImageInstanceInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ImageInstanceInfo imageInstanceInfo = new ImageInstanceInfo(image);
			this.ReadReportItemInstanceInfoBase(imageInstanceInfo);
			if (this.PreRead(objectType, indexes))
			{
				imageInstanceInfo.ImageValue = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				if (this.m_intermediateFormatVersion.IsRS2005_WithMultipleActions)
				{
					imageInstanceInfo.Action = this.ReadActionInstance(image.Action);
				}
				else
				{
					ActionItemInstance actionItemInstance = this.ReadActionItemInstance(null);
					if (actionItemInstance != null)
					{
						imageInstanceInfo.Action = new ActionInstance(actionItemInstance);
					}
				}
			}
			if (this.PreRead(objectType, indexes))
			{
				imageInstanceInfo.BrokenImage = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				imageInstanceInfo.ImageMapAreas = this.ReadImageMapAreaInstanceList();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return imageInstanceInfo;
		}

		// Token: 0x06006DCB RID: 28107 RVA: 0x001C398C File Offset: 0x001C1B8C
		internal SubReportInstanceInfo ReadSubReportInstanceInfo(SubReport subReport)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.SubReportInstanceInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			SubReportInstanceInfo subReportInstanceInfo = new SubReportInstanceInfo(subReport);
			this.ReadReportItemInstanceInfoBase(subReportInstanceInfo);
			if (this.PreRead(objectType, indexes))
			{
				subReportInstanceInfo.NoRows = this.m_reader.ReadString();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return subReportInstanceInfo;
		}

		// Token: 0x06006DCC RID: 28108 RVA: 0x001C3A24 File Offset: 0x001C1C24
		internal ActiveXControlInstanceInfo ReadActiveXControlInstanceInfo(ActiveXControl activeXControl)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.ActiveXControlInstanceInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ActiveXControlInstanceInfo activeXControlInstanceInfo = new ActiveXControlInstanceInfo(activeXControl);
			this.ReadReportItemInstanceInfoBase(activeXControlInstanceInfo);
			if (this.PreRead(objectType, indexes))
			{
				activeXControlInstanceInfo.ParameterValues = this.ReadVariants();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return activeXControlInstanceInfo;
		}

		// Token: 0x06006DCD RID: 28109 RVA: 0x001C3AB8 File Offset: 0x001C1CB8
		internal ListInstanceInfo ReadListInstanceInfo(List list)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.ListInstanceInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ListInstanceInfo listInstanceInfo = new ListInstanceInfo(list);
			this.ReadReportItemInstanceInfoBase(listInstanceInfo);
			if (this.PreRead(objectType, indexes))
			{
				listInstanceInfo.NoRows = this.m_reader.ReadString();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return listInstanceInfo;
		}

		// Token: 0x06006DCE RID: 28110 RVA: 0x001C3B50 File Offset: 0x001C1D50
		internal MatrixInstanceInfo ReadMatrixInstanceInfo(Matrix matrix)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.MatrixInstanceInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			MatrixInstanceInfo matrixInstanceInfo = new MatrixInstanceInfo(matrix);
			this.ReadReportItemInstanceInfoBase(matrixInstanceInfo);
			if (this.PreRead(objectType, indexes))
			{
				matrixInstanceInfo.CornerNonComputedNames = this.ReadNonComputedUniqueNames();
			}
			if (this.PreRead(objectType, indexes))
			{
				matrixInstanceInfo.NoRows = this.m_reader.ReadString();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return matrixInstanceInfo;
		}

		// Token: 0x06006DCF RID: 28111 RVA: 0x001C3C00 File Offset: 0x001C1E00
		internal TableInstanceInfo ReadTableInstanceInfo(Table table)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.TableInstanceInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			TableInstanceInfo tableInstanceInfo = new TableInstanceInfo(table);
			this.ReadReportItemInstanceInfoBase(tableInstanceInfo);
			if (this.PreRead(objectType, indexes))
			{
				tableInstanceInfo.ColumnInstances = this.ReadTableColumnInstances();
			}
			if (this.PreRead(objectType, indexes))
			{
				tableInstanceInfo.NoRows = this.m_reader.ReadString();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return tableInstanceInfo;
		}

		// Token: 0x06006DD0 RID: 28112 RVA: 0x001C3CB0 File Offset: 0x001C1EB0
		internal OWCChartInstanceInfo ReadOWCChartInstanceInfo(OWCChart chart)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.OWCChartInstanceInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			OWCChartInstanceInfo owcchartInstanceInfo = new OWCChartInstanceInfo(chart);
			this.ReadReportItemInstanceInfoBase(owcchartInstanceInfo);
			if (this.PreRead(objectType, indexes))
			{
				owcchartInstanceInfo.ChartData = this.ReadVariantLists(false);
			}
			if (this.PreRead(objectType, indexes))
			{
				owcchartInstanceInfo.NoRows = this.m_reader.ReadString();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return owcchartInstanceInfo;
		}

		// Token: 0x06006DD1 RID: 28113 RVA: 0x001C3D60 File Offset: 0x001C1F60
		internal ChartInstanceInfo ReadChartInstanceInfo(Chart chart)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.ChartInstanceInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ChartInstanceInfo chartInstanceInfo = new ChartInstanceInfo(chart);
			this.ReadReportItemInstanceInfoBase(chartInstanceInfo);
			if (this.PreRead(objectType, indexes))
			{
				chartInstanceInfo.CategoryAxis = this.ReadAxisInstance();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartInstanceInfo.ValueAxis = this.ReadAxisInstance();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartInstanceInfo.Title = this.ReadChartTitleInstance();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartInstanceInfo.PlotAreaStyleAttributeValues = this.ReadVariants();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartInstanceInfo.LegendStyleAttributeValues = this.ReadVariants();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartInstanceInfo.CultureName = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartInstanceInfo.NoRows = this.m_reader.ReadString();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return chartInstanceInfo;
		}

		// Token: 0x06006DD2 RID: 28114 RVA: 0x001C3E84 File Offset: 0x001C2084
		internal CustomReportItemInstanceInfo ReadCustomReportItemInstanceInfo(CustomReportItem cri)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.CustomReportItemInstanceInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			CustomReportItemInstanceInfo customReportItemInstanceInfo = new CustomReportItemInstanceInfo(cri);
			this.ReadReportItemInstanceInfoBase(customReportItemInstanceInfo);
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return customReportItemInstanceInfo;
		}

		// Token: 0x06006DD3 RID: 28115 RVA: 0x001C3F04 File Offset: 0x001C2104
		internal PageSectionInstanceInfo ReadPageSectionInstanceInfo(PageSection pageSectionDef)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.PageSectionInstanceInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			PageSectionInstanceInfo pageSectionInstanceInfo = new PageSectionInstanceInfo(pageSectionDef);
			this.ReadReportItemInstanceInfoBase(pageSectionInstanceInfo);
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return pageSectionInstanceInfo;
		}

		// Token: 0x06006DD4 RID: 28116 RVA: 0x001C3F84 File Offset: 0x001C2184
		internal ReportInstanceInfo ReadReportInstanceInfo(Report report)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.ReportInstanceInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ReportInstanceInfo reportInstanceInfo = new ReportInstanceInfo(report);
			this.ReadReportItemInstanceInfoBase(reportInstanceInfo);
			if (this.PreRead(objectType, indexes))
			{
				reportInstanceInfo.Parameters = this.ReadParameterInfoCollection();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportInstanceInfo.ReportName = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportInstanceInfo.NoRows = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportInstanceInfo.BodyUniqueName = this.m_reader.ReadInt32();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return reportInstanceInfo;
		}

		// Token: 0x06006DD5 RID: 28117 RVA: 0x001C4068 File Offset: 0x001C2268
		internal RecordSetInfo ReadRecordSetInfo()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.RecordSetInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			RecordSetInfo recordSetInfo = new RecordSetInfo();
			if (this.PreRead(objectType, indexes))
			{
				recordSetInfo.ReaderExtensionsSupported = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				recordSetInfo.FieldPropertyNames = this.ReadRecordSetPropertyNamesList();
			}
			if (this.PreRead(objectType, indexes))
			{
				recordSetInfo.CompareOptions = this.ReadCompareOptions();
				recordSetInfo.ValidCompareOptions = true;
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return recordSetInfo;
		}

		// Token: 0x06006DD6 RID: 28118 RVA: 0x001C412E File Offset: 0x001C232E
		private CompareOptions ReadCompareOptions()
		{
			return (CompareOptions)this.m_reader.ReadEnum();
		}

		// Token: 0x06006DD7 RID: 28119 RVA: 0x001C413C File Offset: 0x001C233C
		private RecordSetPropertyNamesList ReadRecordSetPropertyNamesList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.RecordSetPropertyNamesList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			RecordSetPropertyNamesList recordSetPropertyNamesList = new RecordSetPropertyNamesList(num);
			for (int i = 0; i < num; i++)
			{
				recordSetPropertyNamesList.Add(this.ReadRecordSetPropertyNames());
			}
			this.m_reader.ReadEndObject();
			return recordSetPropertyNamesList;
		}

		// Token: 0x06006DD8 RID: 28120 RVA: 0x001C41CC File Offset: 0x001C23CC
		internal RecordSetPropertyNames ReadRecordSetPropertyNames()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.RecordSetPropertyNames;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			RecordSetPropertyNames recordSetPropertyNames = new RecordSetPropertyNames();
			if (this.PreRead(objectType, indexes))
			{
				recordSetPropertyNames.PropertyNames = this.ReadStringList();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return recordSetPropertyNames;
		}

		// Token: 0x06006DD9 RID: 28121 RVA: 0x001C425C File Offset: 0x001C245C
		internal RecordRow ReadRecordRow()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.RecordRow;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			RecordRow recordRow = new RecordRow();
			if (this.PreRead(objectType, indexes))
			{
				recordRow.RecordFields = this.ReadRecordFields();
			}
			if (this.PreRead(objectType, indexes))
			{
				recordRow.IsAggregateRow = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				recordRow.AggregationFieldCount = this.m_reader.ReadInt32();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return recordRow;
		}

		// Token: 0x06006DDA RID: 28122 RVA: 0x001C4320 File Offset: 0x001C2520
		private static void Assert(bool condition)
		{
			if (!condition)
			{
				Global.Tracer.Assert(false);
				throw new ReportProcessingException(ErrorCode.rsUnexpectedError);
			}
		}

		// Token: 0x06006DDB RID: 28123 RVA: 0x001C433C File Offset: 0x001C253C
		private void RegisterParameterDef(ParameterDef paramDef)
		{
			IntermediateFormatReader.Assert(paramDef != null);
			if (this.m_parametersDef == null)
			{
				this.m_parametersDef = new Hashtable();
			}
			else
			{
				IntermediateFormatReader.Assert(!this.m_parametersDef.ContainsKey(paramDef.Name));
			}
			this.m_parametersDef.Add(paramDef.Name, paramDef);
		}

		// Token: 0x06006DDC RID: 28124 RVA: 0x001C4394 File Offset: 0x001C2594
		private ParameterDef GetParameterDefObject(string name)
		{
			IntermediateFormatReader.Assert(this.m_parametersDef != null);
			ParameterDef parameterDef = (ParameterDef)this.m_parametersDef[name];
			IntermediateFormatReader.Assert(parameterDef != null);
			return parameterDef;
		}

		// Token: 0x06006DDD RID: 28125 RVA: 0x001C43CC File Offset: 0x001C25CC
		private void RegisterParameterInfo(ParameterInfo paramInfo)
		{
			IntermediateFormatReader.Assert(paramInfo != null);
			if (this.m_parametersInfo == null)
			{
				this.m_parametersInfo = new Hashtable();
			}
			else
			{
				IntermediateFormatReader.Assert(!this.m_parametersInfo.ContainsKey(paramInfo.Name));
			}
			this.m_parametersInfo.Add(paramInfo.Name, paramInfo);
		}

		// Token: 0x06006DDE RID: 28126 RVA: 0x001C4424 File Offset: 0x001C2624
		private ParameterInfo GetParameterInfoObject(string name)
		{
			IntermediateFormatReader.Assert(this.m_parametersInfo != null);
			ParameterInfo parameterInfo = (ParameterInfo)this.m_parametersInfo[name];
			IntermediateFormatReader.Assert(parameterInfo != null);
			return parameterInfo;
		}

		// Token: 0x06006DDF RID: 28127 RVA: 0x001C445C File Offset: 0x001C265C
		private void RegisterDefinitionObject(IDOwner idOwner)
		{
			IntermediateFormatReader.Assert(idOwner != null);
			if (this.m_definitionObjects == null)
			{
				this.m_definitionObjects = new Hashtable();
			}
			else
			{
				IntermediateFormatReader.Assert(!this.m_definitionObjects.ContainsKey(idOwner.ID));
			}
			this.m_definitionObjects.Add(idOwner.ID, idOwner);
		}

		// Token: 0x06006DE0 RID: 28128 RVA: 0x001C44BC File Offset: 0x001C26BC
		private IDOwner GetDefinitionObject(int id)
		{
			IntermediateFormatReader.Assert(this.m_definitionObjects != null);
			IDOwner idowner = (IDOwner)this.m_definitionObjects[id];
			IntermediateFormatReader.Assert(idowner != null);
			return idowner;
		}

		// Token: 0x06006DE1 RID: 28129 RVA: 0x001C44F8 File Offset: 0x001C26F8
		private void RegisterInstanceObject(ReportItemInstance reportItemInstance)
		{
			IntermediateFormatReader.Assert(reportItemInstance != null);
			if (this.m_instanceObjects == null)
			{
				this.m_instanceObjects = new Hashtable();
			}
			else
			{
				IntermediateFormatReader.Assert(!this.m_instanceObjects.ContainsKey(reportItemInstance.UniqueName));
			}
			this.m_instanceObjects.Add(reportItemInstance.UniqueName, reportItemInstance);
		}

		// Token: 0x06006DE2 RID: 28130 RVA: 0x001C4558 File Offset: 0x001C2758
		private ReportItemInstance GetInstanceObject(int uniqueName)
		{
			IntermediateFormatReader.Assert(this.m_instanceObjects != null);
			ReportItemInstance reportItemInstance = (ReportItemInstance)this.m_instanceObjects[uniqueName];
			IntermediateFormatReader.Assert(reportItemInstance != null);
			return reportItemInstance;
		}

		// Token: 0x06006DE3 RID: 28131 RVA: 0x001C4594 File Offset: 0x001C2794
		private void RegisterMatrixHeadingInstanceObject(MatrixHeadingInstance matrixHeadingInstance)
		{
			IntermediateFormatReader.Assert(matrixHeadingInstance != null);
			if (this.m_matrixHeadingInstanceObjects == null)
			{
				this.m_matrixHeadingInstanceObjects = new Hashtable();
			}
			else
			{
				IntermediateFormatReader.Assert(!this.m_matrixHeadingInstanceObjects.ContainsKey(matrixHeadingInstance.UniqueName));
			}
			this.m_matrixHeadingInstanceObjects.Add(matrixHeadingInstance.UniqueName, matrixHeadingInstance);
		}

		// Token: 0x06006DE4 RID: 28132 RVA: 0x001C45F4 File Offset: 0x001C27F4
		private MatrixHeadingInstance GetMatrixHeadingInstanceObject(int uniqueName)
		{
			IntermediateFormatReader.Assert(this.m_matrixHeadingInstanceObjects != null);
			MatrixHeadingInstance matrixHeadingInstance = (MatrixHeadingInstance)this.m_matrixHeadingInstanceObjects[uniqueName];
			IntermediateFormatReader.Assert(matrixHeadingInstance != null);
			return matrixHeadingInstance;
		}

		// Token: 0x06006DE5 RID: 28133 RVA: 0x001C4630 File Offset: 0x001C2830
		private void DeclarationCallback(ObjectType objectType, Declaration declaration)
		{
			IntermediateFormatReader.Assert(this.m_expectDeclarations);
			IntermediateFormatReader.Assert(declaration != null);
			IntermediateFormatReader.Assert(declaration.Members != null);
			bool flag = false;
			if (this.m_intermediateFormatVersion != null && !this.m_intermediateFormatVersion.IsRS2005_WithTableOptimizations && ObjectType.TableGroupInstance == objectType)
			{
				flag = true;
			}
			Hashtable hashtable = new Hashtable();
			for (int i = 0; i < declaration.Members.Count; i++)
			{
				MemberInfo memberInfo = declaration.Members[i];
				if (flag && memberInfo.MemberName == MemberName.DetailRowInstances)
				{
					memberInfo.MemberName = MemberName.TableDetailInstances;
				}
				hashtable[memberInfo.MemberName] = i;
			}
			Declaration declaration2 = DeclarationList.Current[objectType];
			IntermediateFormatReader.Assert(declaration2 != null);
			IntermediateFormatReader.Assert(declaration2.Members != null);
			int num = -1;
			bool[] array = new bool[declaration2.Members.Count];
			IntList[] array2 = new IntList[declaration2.Members.Count + 1];
			for (int j = 0; j < declaration2.Members.Count; j++)
			{
				if (hashtable.ContainsKey(declaration2.Members[j].MemberName))
				{
					int num2 = (int)hashtable[declaration2.Members[j].MemberName];
					bool flag2 = MemberInfo.Equals(declaration2.Members[j], declaration.Members[num2]);
					if (!flag2)
					{
						if (declaration2.Members[j].ObjectType == ObjectType.ExpressionInfo)
						{
							flag2 = declaration.Members[num2].ObjectType == ObjectType.None && (declaration.Members[num2].Token == Token.String || declaration.Members[num2].Token == Token.Boolean || declaration.Members[num2].Token == Token.Int32);
						}
						if (!flag2 && objectType == ObjectType.Axis && declaration2.Members[j].ObjectType == ObjectType.ExpressionInfo)
						{
							flag2 = declaration.Members[num2].ObjectType == ObjectType.Variant;
						}
					}
					if (flag2)
					{
						array[j] = true;
						array2[j] = this.CreateOldIndexesToSkip(num2, num);
						num = num2;
					}
				}
			}
			array2[declaration2.Members.Count] = this.CreateOldIndexesToSkip(declaration.Members.Count, num);
			IntermediateFormatReader.Assert(this.m_state.OldDeclarations[objectType] == null);
			IntermediateFormatReader.Assert(this.m_state.IsInOldDeclaration[(int)objectType] == null);
			IntermediateFormatReader.Assert(this.m_state.OldIndexesToSkip[(int)objectType] == null);
			this.m_state.OldDeclarations[objectType] = declaration;
			this.m_state.IsInOldDeclaration[(int)objectType] = array;
			this.m_state.OldIndexesToSkip[(int)objectType] = array2;
		}

		// Token: 0x06006DE6 RID: 28134 RVA: 0x001C4920 File Offset: 0x001C2B20
		private IntList CreateOldIndexesToSkip(int index, int lastIndex)
		{
			IntermediateFormatReader.Assert(index > lastIndex);
			IntList intList = null;
			if (index - lastIndex > 1)
			{
				intList = new IntList();
				for (int i = lastIndex + 1; i < index; i++)
				{
					intList.Add(i);
				}
			}
			return intList;
		}

		// Token: 0x06006DE7 RID: 28135 RVA: 0x001C4960 File Offset: 0x001C2B60
		private bool PreRead(ObjectType objectType, IntermediateFormatReader.Indexes indexes)
		{
			this.PostRead(objectType, indexes);
			bool flag = this.IsInOldDeclaration(objectType, indexes);
			indexes.CurrentIndex++;
			return flag;
		}

		// Token: 0x06006DE8 RID: 28136 RVA: 0x001C4980 File Offset: 0x001C2B80
		private void PostRead(ObjectType objectType, IntermediateFormatReader.Indexes indexes)
		{
			while (!this.m_state.OldDeclarations.ContainsKey(objectType))
			{
				this.m_reader.ReadDeclaration();
			}
			this.Skip(objectType, indexes);
		}

		// Token: 0x06006DE9 RID: 28137 RVA: 0x001C49AC File Offset: 0x001C2BAC
		private void Skip(ObjectType objectType, IntermediateFormatReader.Indexes indexes)
		{
			IntList[] array = this.m_state.OldIndexesToSkip[(int)objectType];
			if (array != null && array.Length > indexes.CurrentIndex)
			{
				IntList intList = array[indexes.CurrentIndex];
				if (intList != null)
				{
					for (int i = 0; i < intList.Count; i++)
					{
						this.ReadRemovedItemType(this.m_state.OldDeclarations[objectType].Members[intList[i]].Token, this.m_state.OldDeclarations[objectType].Members[intList[i]].ObjectType);
					}
				}
			}
		}

		// Token: 0x06006DEA RID: 28138 RVA: 0x001C4A48 File Offset: 0x001C2C48
		private void ReadRemovedItemType(Token token, ObjectType objectType)
		{
			if (token <= Token.Reference)
			{
				if (token != Token.Object)
				{
					if (token == Token.Reference)
					{
						if (objectType <= ObjectType.MatrixHeading)
						{
							if (objectType == ObjectType.ReportItem)
							{
								this.ReadReportItemReference(false);
								return;
							}
							if (objectType != ObjectType.List && objectType != ObjectType.MatrixHeading)
							{
								goto IL_00CC;
							}
						}
						else if (objectType <= ObjectType.ReportItemCollection)
						{
							if (objectType - ObjectType.TableGroup > 1 && objectType != ObjectType.ReportItemCollection)
							{
								goto IL_00CC;
							}
						}
						else if (objectType != ObjectType.TableDetail)
						{
							if (objectType != ObjectType.ChartHeading)
							{
								goto IL_00CC;
							}
							if (this.m_intermediateFormatVersion.IsRS2000_RTM_orOlder)
							{
								this.ReadRemovedReference();
							}
							return;
						}
						this.ReadRemovedReference();
						return;
					}
				}
				else
				{
					if (objectType == ObjectType.TableGroup)
					{
						Global.Tracer.Assert(!this.m_intermediateFormatVersion.IsRS2005_WithTableDetailFix);
						return;
					}
					if (objectType == ObjectType.DataAggregateInfoList)
					{
						this.ReadDataAggregateInfoList();
						return;
					}
				}
			}
			else
			{
				if (token == Token.String)
				{
					this.m_reader.ReadString();
					return;
				}
				if (token == Token.Int32)
				{
					this.m_reader.ReadInt32();
					return;
				}
			}
			IL_00CC:
			IntermediateFormatReader.Assert(false);
		}

		// Token: 0x06006DEB RID: 28139 RVA: 0x001C4B27 File Offset: 0x001C2D27
		private void ReadRemovedReference()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
		}

		// Token: 0x06006DEC RID: 28140 RVA: 0x001C4B3C File Offset: 0x001C2D3C
		private bool IsInOldDeclaration(ObjectType objectType, IntermediateFormatReader.Indexes indexes)
		{
			bool[] array = this.m_state.IsInOldDeclaration[(int)objectType];
			return array == null || array[indexes.CurrentIndex];
		}

		// Token: 0x06006DED RID: 28141 RVA: 0x001C4B64 File Offset: 0x001C2D64
		private ValidValueList ReadValidValueList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.ValidValueList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			ValidValueList validValueList = new ValidValueList(num);
			for (int i = 0; i < num; i++)
			{
				validValueList.Add(this.ReadValidValue());
			}
			this.m_reader.ReadEndObject();
			return validValueList;
		}

		// Token: 0x06006DEE RID: 28142 RVA: 0x001C4BF4 File Offset: 0x001C2DF4
		private ParameterDefList ReadParameterDefList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.ParameterDefList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			ParameterDefList parameterDefList = new ParameterDefList(num);
			this.m_parametersDef = null;
			for (int i = 0; i < num; i++)
			{
				parameterDefList.Add(this.ReadParameterDef());
			}
			this.m_reader.ReadEndObject();
			return parameterDefList;
		}

		// Token: 0x06006DEF RID: 28143 RVA: 0x001C4C8C File Offset: 0x001C2E8C
		private ParameterDefList ReadParameterDefRefList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.ParameterDefList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			ParameterDefList parameterDefList = new ParameterDefList(num);
			for (int i = 0; i < num; i++)
			{
				parameterDefList.Add(this.GetParameterDefObject(this.m_reader.ReadString()));
			}
			this.m_reader.ReadEndObject();
			return parameterDefList;
		}

		// Token: 0x06006DF0 RID: 28144 RVA: 0x001C4D28 File Offset: 0x001C2F28
		private ParameterInfoCollection ReadParameterInfoCollection()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.ParameterInfoCollection == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			ParameterInfoCollection parameterInfoCollection = new ParameterInfoCollection(num);
			this.m_parametersInfo = null;
			for (int i = 0; i < num; i++)
			{
				parameterInfoCollection.Add(this.ReadParameterInfo());
			}
			this.m_reader.ReadEndObject();
			return parameterInfoCollection;
		}

		// Token: 0x06006DF1 RID: 28145 RVA: 0x001C4DBC File Offset: 0x001C2FBC
		private ParameterInfoCollection ReadParameterInfoRefCollection()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.ParameterInfoCollection == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			ParameterInfoCollection parameterInfoCollection = new ParameterInfoCollection(num);
			for (int i = 0; i < num; i++)
			{
				parameterInfoCollection.Add(this.GetParameterInfoObject(this.m_reader.ReadString()));
			}
			this.m_reader.ReadEndObject();
			return parameterInfoCollection;
		}

		// Token: 0x06006DF2 RID: 28146 RVA: 0x001C4E54 File Offset: 0x001C3054
		private FilterList ReadFilterList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.FilterList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			FilterList filterList = new FilterList(num);
			for (int i = 0; i < num; i++)
			{
				filterList.Add(this.ReadFilter());
			}
			this.m_reader.ReadEndObject();
			return filterList;
		}

		// Token: 0x06006DF3 RID: 28147 RVA: 0x001C4EE0 File Offset: 0x001C30E0
		private DataSourceList ReadDataSourceList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.DataSourceList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			DataSourceList dataSourceList = new DataSourceList(num);
			for (int i = 0; i < num; i++)
			{
				dataSourceList.Add(this.ReadDataSource());
			}
			this.m_reader.ReadEndObject();
			return dataSourceList;
		}

		// Token: 0x06006DF4 RID: 28148 RVA: 0x001C4F6C File Offset: 0x001C316C
		private DataAggregateInfoList ReadDataAggregateInfoList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.DataAggregateInfoList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			DataAggregateInfoList dataAggregateInfoList = new DataAggregateInfoList(num);
			for (int i = 0; i < num; i++)
			{
				dataAggregateInfoList.Add(this.ReadDataAggregateInfo());
			}
			this.m_reader.ReadEndObject();
			return dataAggregateInfoList;
		}

		// Token: 0x06006DF5 RID: 28149 RVA: 0x001C4FF8 File Offset: 0x001C31F8
		private ReportItemList ReadReportItemList(ReportItem parent)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.ReportItemList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			ReportItemList reportItemList = new ReportItemList(num);
			for (int i = 0; i < num; i++)
			{
				reportItemList.Add(this.ReadReportItem(parent));
			}
			this.m_reader.ReadEndObject();
			return reportItemList;
		}

		// Token: 0x06006DF6 RID: 28150 RVA: 0x001C5084 File Offset: 0x001C3284
		private ReportItemIndexerList ReadReportItemIndexerList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.ReportItemIndexerList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			ReportItemIndexerList reportItemIndexerList = new ReportItemIndexerList(num);
			for (int i = 0; i < num; i++)
			{
				reportItemIndexerList.Add(this.ReadReportItemIndexer());
			}
			this.m_reader.ReadEndObject();
			return reportItemIndexerList;
		}

		// Token: 0x06006DF7 RID: 28151 RVA: 0x001C5114 File Offset: 0x001C3314
		private RunningValueInfoList ReadRunningValueInfoList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.RunningValueInfoList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			RunningValueInfoList runningValueInfoList = new RunningValueInfoList(num);
			for (int i = 0; i < num; i++)
			{
				runningValueInfoList.Add(this.ReadRunningValueInfo());
			}
			this.m_reader.ReadEndObject();
			return runningValueInfoList;
		}

		// Token: 0x06006DF8 RID: 28152 RVA: 0x001C51A0 File Offset: 0x001C33A0
		private StyleAttributeHashtable ReadStyleAttributeHashtable()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.StyleAttributeHashtable == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			StyleAttributeHashtable styleAttributeHashtable = new StyleAttributeHashtable(num);
			for (int i = 0; i < num; i++)
			{
				string text = this.m_reader.ReadString();
				AttributeInfo attributeInfo = this.ReadAttributeInfo();
				styleAttributeHashtable.Add(text, attributeInfo);
			}
			this.m_reader.ReadEndObject();
			return styleAttributeHashtable;
		}

		// Token: 0x06006DF9 RID: 28153 RVA: 0x001C523C File Offset: 0x001C343C
		private ImageInfo ReadImageInfo()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.ImageInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ImageInfo imageInfo = new ImageInfo();
			if (this.PreRead(objectType, indexes))
			{
				imageInfo.StreamName = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				imageInfo.MimeType = this.m_reader.ReadString();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return imageInfo;
		}

		// Token: 0x06006DFA RID: 28154 RVA: 0x001C52EC File Offset: 0x001C34EC
		private DrillthroughParameters ReadDrillthroughParameters()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.DrillthroughParameters == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			DrillthroughParameters drillthroughParameters = new DrillthroughParameters(num);
			for (int i = 0; i < num; i++)
			{
				string text = this.m_reader.ReadString();
				object obj = this.ReadMultiValue();
				drillthroughParameters.Add(text, obj);
			}
			this.m_reader.ReadEndObject();
			return drillthroughParameters;
		}

		// Token: 0x06006DFB RID: 28155 RVA: 0x001C538C File Offset: 0x001C358C
		private ImageStreamNames ReadImageStreamNames()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.ImageStreamNames == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			ImageStreamNames imageStreamNames = new ImageStreamNames(num);
			for (int i = 0; i < num; i++)
			{
				string text = this.m_reader.ReadString();
				if (this.m_intermediateFormatVersion.IsRS2000_WithImageInfo)
				{
					ImageInfo imageInfo = this.ReadImageInfo();
					imageStreamNames.Add(text, imageInfo);
				}
				else
				{
					string text2 = this.m_reader.ReadString();
					imageStreamNames.Add(text, new ImageInfo(text2, null));
				}
			}
			this.m_reader.ReadEndObject();
			return imageStreamNames;
		}

		// Token: 0x06006DFC RID: 28156 RVA: 0x001C5454 File Offset: 0x001C3654
		private EmbeddedImageHashtable ReadEmbeddedImageHashtable()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.EmbeddedImageHashtable == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			EmbeddedImageHashtable embeddedImageHashtable = new EmbeddedImageHashtable(num);
			for (int i = 0; i < num; i++)
			{
				string text = this.m_reader.ReadString();
				if (this.m_intermediateFormatVersion.IsRS2000_WithImageInfo)
				{
					ImageInfo imageInfo = this.ReadImageInfo();
					embeddedImageHashtable.Add(text, imageInfo);
				}
				else
				{
					string text2 = this.m_reader.ReadString();
					embeddedImageHashtable.Add(text, new ImageInfo(text2, null));
				}
			}
			this.m_reader.ReadEndObject();
			return embeddedImageHashtable;
		}

		// Token: 0x06006DFD RID: 28157 RVA: 0x001C551C File Offset: 0x001C371C
		private ExpressionInfoList ReadExpressionInfoList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.ExpressionInfoList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			ExpressionInfoList expressionInfoList = new ExpressionInfoList(num);
			for (int i = 0; i < num; i++)
			{
				expressionInfoList.Add(this.ReadExpressionInfo());
			}
			this.m_reader.ReadEndObject();
			return expressionInfoList;
		}

		// Token: 0x06006DFE RID: 28158 RVA: 0x001C55A8 File Offset: 0x001C37A8
		private DataSetList ReadDataSetList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.DataSetList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			DataSetList dataSetList = new DataSetList(num);
			for (int i = 0; i < num; i++)
			{
				dataSetList.Add(this.ReadDataSet());
			}
			this.m_reader.ReadEndObject();
			return dataSetList;
		}

		// Token: 0x06006DFF RID: 28159 RVA: 0x001C5634 File Offset: 0x001C3834
		private ExpressionInfo[] ReadExpressionInfos()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Array == this.m_reader.Token);
			int arrayLength = this.m_reader.ArrayLength;
			ExpressionInfo[] array = new ExpressionInfo[arrayLength];
			for (int i = 0; i < arrayLength; i++)
			{
				array[i] = this.ReadExpressionInfo();
			}
			return array;
		}

		// Token: 0x06006E00 RID: 28160 RVA: 0x001C569C File Offset: 0x001C389C
		private StringList ReadStringList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.StringList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			StringList stringList = new StringList(num);
			for (int i = 0; i < num; i++)
			{
				stringList.Add(this.m_reader.ReadString());
			}
			this.m_reader.ReadEndObject();
			return stringList;
		}

		// Token: 0x06006E01 RID: 28161 RVA: 0x001C572C File Offset: 0x001C392C
		private DataFieldList ReadDataFieldList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.DataFieldList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			DataFieldList dataFieldList = new DataFieldList(num);
			for (int i = 0; i < num; i++)
			{
				dataFieldList.Add(this.ReadDataField());
			}
			this.m_reader.ReadEndObject();
			return dataFieldList;
		}

		// Token: 0x06006E02 RID: 28162 RVA: 0x001C57B8 File Offset: 0x001C39B8
		private DataRegionList ReadDataRegionList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.DataRegionList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			DataRegionList dataRegionList = new DataRegionList(num);
			for (int i = 0; i < num; i++)
			{
				dataRegionList.Add(this.ReadDataRegionReference());
			}
			this.m_reader.ReadEndObject();
			return dataRegionList;
		}

		// Token: 0x06006E03 RID: 28163 RVA: 0x001C5844 File Offset: 0x001C3A44
		private ParameterValueList ReadParameterValueList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.ParameterValueList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			ParameterValueList parameterValueList = new ParameterValueList(num);
			for (int i = 0; i < num; i++)
			{
				parameterValueList.Add(this.ReadParameterValue());
			}
			this.m_reader.ReadEndObject();
			return parameterValueList;
		}

		// Token: 0x06006E04 RID: 28164 RVA: 0x001C58D0 File Offset: 0x001C3AD0
		private CodeClassList ReadCodeClassList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.CodeClassList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			CodeClassList codeClassList = new CodeClassList(num);
			for (int i = 0; i < num; i++)
			{
				codeClassList.Add(this.ReadCodeClass());
			}
			this.m_reader.ReadEndObject();
			return codeClassList;
		}

		// Token: 0x06006E05 RID: 28165 RVA: 0x001C5964 File Offset: 0x001C3B64
		private IntList ReadIntList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.IntList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			IntList intList = new IntList(num);
			for (int i = 0; i < num; i++)
			{
				intList.Add(this.m_reader.ReadInt32());
			}
			this.m_reader.ReadEndObject();
			return intList;
		}

		// Token: 0x06006E06 RID: 28166 RVA: 0x001C59FC File Offset: 0x001C3BFC
		private Int64List ReadInt64List()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.Int64List == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			Int64List int64List = new Int64List(num);
			for (int i = 0; i < num; i++)
			{
				int64List.Add(this.m_reader.ReadInt64());
			}
			this.m_reader.ReadEndObject();
			return int64List;
		}

		// Token: 0x06006E07 RID: 28167 RVA: 0x001C5A94 File Offset: 0x001C3C94
		private BoolList ReadBoolList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.BoolList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			BoolList boolList = new BoolList(num);
			for (int i = 0; i < num; i++)
			{
				boolList.Add(this.m_reader.ReadBoolean());
			}
			this.m_reader.ReadEndObject();
			return boolList;
		}

		// Token: 0x06006E08 RID: 28168 RVA: 0x001C5B2C File Offset: 0x001C3D2C
		private MatrixRowList ReadMatrixRowList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.MatrixRowList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			MatrixRowList matrixRowList = new MatrixRowList(num);
			for (int i = 0; i < num; i++)
			{
				matrixRowList.Add(this.ReadMatrixRow());
			}
			this.m_reader.ReadEndObject();
			return matrixRowList;
		}

		// Token: 0x06006E09 RID: 28169 RVA: 0x001C5BB8 File Offset: 0x001C3DB8
		private MatrixColumnList ReadMatrixColumnList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.MatrixColumnList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			MatrixColumnList matrixColumnList = new MatrixColumnList(num);
			for (int i = 0; i < num; i++)
			{
				matrixColumnList.Add(this.ReadMatrixColumn());
			}
			this.m_reader.ReadEndObject();
			return matrixColumnList;
		}

		// Token: 0x06006E0A RID: 28170 RVA: 0x001C5C44 File Offset: 0x001C3E44
		private TableColumnList ReadTableColumnList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.TableColumnList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			TableColumnList tableColumnList = new TableColumnList(num);
			for (int i = 0; i < num; i++)
			{
				tableColumnList.Add(this.ReadTableColumn());
			}
			this.m_reader.ReadEndObject();
			return tableColumnList;
		}

		// Token: 0x06006E0B RID: 28171 RVA: 0x001C5CD0 File Offset: 0x001C3ED0
		private TableRowList ReadTableRowList(ReportItem parent)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.TableRowList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			TableRowList tableRowList = new TableRowList(num);
			for (int i = 0; i < num; i++)
			{
				tableRowList.Add(this.ReadTableRow(parent));
			}
			this.m_reader.ReadEndObject();
			return tableRowList;
		}

		// Token: 0x06006E0C RID: 28172 RVA: 0x001C5D5C File Offset: 0x001C3F5C
		private ChartColumnList ReadChartColumnList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.ChartColumnList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			ChartColumnList chartColumnList = new ChartColumnList(num);
			for (int i = 0; i < num; i++)
			{
				chartColumnList.Add(this.ReadChartColumn());
			}
			this.m_reader.ReadEndObject();
			return chartColumnList;
		}

		// Token: 0x06006E0D RID: 28173 RVA: 0x001C5DE8 File Offset: 0x001C3FE8
		private SubReportList ReadSubReportList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.SubReportList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			SubReportList subReportList = new SubReportList(num);
			for (int i = 0; i < num; i++)
			{
				subReportList.Add(this.ReadSubReportReference());
			}
			this.m_reader.ReadEndObject();
			return subReportList;
		}

		// Token: 0x06006E0E RID: 28174 RVA: 0x001C5E78 File Offset: 0x001C4078
		private NonComputedUniqueNames[] ReadNonComputedUniqueNamess()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Array == this.m_reader.Token);
			int arrayLength = this.m_reader.ArrayLength;
			NonComputedUniqueNames[] array = new NonComputedUniqueNames[arrayLength];
			for (int i = 0; i < arrayLength; i++)
			{
				array[i] = this.ReadNonComputedUniqueNames();
			}
			return array;
		}

		// Token: 0x06006E0F RID: 28175 RVA: 0x001C5EE0 File Offset: 0x001C40E0
		private ReportItemInstanceList ReadReportItemInstanceList(ReportItemCollection reportItemsDef)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.ReportItemInstanceList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			ReportItemInstanceList reportItemInstanceList = new ReportItemInstanceList(num);
			for (int i = 0; i < num; i++)
			{
				reportItemInstanceList.Add(this.ReadReportItemInstance(reportItemsDef.ComputedReportItems[i]));
			}
			this.m_reader.ReadEndObject();
			return reportItemInstanceList;
		}

		// Token: 0x06006E10 RID: 28176 RVA: 0x001C5F78 File Offset: 0x001C4178
		private RenderingPagesRangesList ReadRenderingPagesRangesList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.RenderingPagesRangesList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			RenderingPagesRangesList renderingPagesRangesList = new RenderingPagesRangesList(num);
			for (int i = 0; i < num; i++)
			{
				renderingPagesRangesList.Add(this.ReadRenderingPagesRanges());
			}
			this.m_reader.ReadEndObject();
			return renderingPagesRangesList;
		}

		// Token: 0x06006E11 RID: 28177 RVA: 0x001C600C File Offset: 0x001C420C
		private ListContentInstanceList ReadListContentInstanceList(List listDef)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.ListContentInstanceList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			ListContentInstanceList listContentInstanceList = new ListContentInstanceList(num);
			for (int i = 0; i < num; i++)
			{
				listContentInstanceList.Add(this.ReadListContentInstance(listDef));
			}
			this.m_reader.ReadEndObject();
			return listContentInstanceList;
		}

		// Token: 0x06006E12 RID: 28178 RVA: 0x001C6098 File Offset: 0x001C4298
		private MatrixHeadingInstanceList ReadMatrixHeadingInstanceList(MatrixHeading headingDef)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.MatrixHeadingInstanceList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			MatrixHeadingInstanceList matrixHeadingInstanceList = new MatrixHeadingInstanceList(num);
			for (int i = 0; i < num; i++)
			{
				matrixHeadingInstanceList.Add(this.ReadMatrixHeadingInstance(headingDef, i, num));
			}
			this.m_reader.ReadEndObject();
			return matrixHeadingInstanceList;
		}

		// Token: 0x06006E13 RID: 28179 RVA: 0x001C6128 File Offset: 0x001C4328
		private MatrixCellInstancesList ReadMatrixCellInstancesList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.MatrixCellInstancesList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			MatrixCellInstancesList matrixCellInstancesList = new MatrixCellInstancesList(num);
			for (int i = 0; i < num; i++)
			{
				matrixCellInstancesList.Add(this.ReadMatrixCellInstanceList());
			}
			this.m_reader.ReadEndObject();
			return matrixCellInstancesList;
		}

		// Token: 0x06006E14 RID: 28180 RVA: 0x001C61B4 File Offset: 0x001C43B4
		private MatrixCellInstanceList ReadMatrixCellInstanceList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.MatrixCellInstanceList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			MatrixCellInstanceList matrixCellInstanceList = new MatrixCellInstanceList(num);
			for (int i = 0; i < num; i++)
			{
				matrixCellInstanceList.Add(this.ReadMatrixCellInstanceBase());
			}
			this.m_reader.ReadEndObject();
			return matrixCellInstanceList;
		}

		// Token: 0x06006E15 RID: 28181 RVA: 0x001C6240 File Offset: 0x001C4440
		private MultiChartInstanceList ReadMultiChartInstanceList(Chart chartDef)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.MultiChartInstanceList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			MultiChartInstanceList multiChartInstanceList = new MultiChartInstanceList(num);
			for (int i = 0; i < num; i++)
			{
				multiChartInstanceList.Add(this.ReadMultiChartInstance(chartDef));
			}
			this.m_reader.ReadEndObject();
			return multiChartInstanceList;
		}

		// Token: 0x06006E16 RID: 28182 RVA: 0x001C62D0 File Offset: 0x001C44D0
		private ChartHeadingInstanceList ReadChartHeadingInstanceList(ChartHeading headingDef)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.ChartHeadingInstanceList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			ChartHeadingInstanceList chartHeadingInstanceList = new ChartHeadingInstanceList(num);
			for (int i = 0; i < num; i++)
			{
				chartHeadingInstanceList.Add(this.ReadChartHeadingInstance(headingDef));
			}
			this.m_reader.ReadEndObject();
			return chartHeadingInstanceList;
		}

		// Token: 0x06006E17 RID: 28183 RVA: 0x001C6360 File Offset: 0x001C4560
		private ChartDataPointInstancesList ReadChartDataPointInstancesList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.ChartDataPointInstancesList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			ChartDataPointInstancesList chartDataPointInstancesList = new ChartDataPointInstancesList(num);
			for (int i = 0; i < num; i++)
			{
				chartDataPointInstancesList.Add(this.ReadChartDataPointInstanceList());
			}
			this.m_reader.ReadEndObject();
			return chartDataPointInstancesList;
		}

		// Token: 0x06006E18 RID: 28184 RVA: 0x001C63F0 File Offset: 0x001C45F0
		private ChartDataPointInstanceList ReadChartDataPointInstanceList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.ChartDataPointInstanceList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			ChartDataPointInstanceList chartDataPointInstanceList = new ChartDataPointInstanceList(num);
			for (int i = 0; i < num; i++)
			{
				chartDataPointInstanceList.Add(this.ReadChartDataPointInstance());
			}
			this.m_reader.ReadEndObject();
			return chartDataPointInstanceList;
		}

		// Token: 0x06006E19 RID: 28185 RVA: 0x001C6480 File Offset: 0x001C4680
		private TableRowInstance[] ReadTableRowInstances(TableRowList rowDefs, int rowStartUniqueName)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Array == this.m_reader.Token);
			int arrayLength = this.m_reader.ArrayLength;
			TableRowInstance[] array = new TableRowInstance[arrayLength];
			for (int i = 0; i < arrayLength; i++)
			{
				array[i] = this.ReadTableRowInstance(rowDefs, i, rowStartUniqueName);
				if (-1 != rowStartUniqueName)
				{
					rowStartUniqueName++;
					Global.Tracer.Assert(rowDefs != null, "(null != rowDefs)");
					if (rowDefs[i] != null)
					{
						ReportItemCollection reportItems = rowDefs[i].ReportItems;
						if (reportItems != null && reportItems.NonComputedReportItems != null)
						{
							rowStartUniqueName += reportItems.NonComputedReportItems.Count;
						}
					}
				}
			}
			return array;
		}

		// Token: 0x06006E1A RID: 28186 RVA: 0x001C6538 File Offset: 0x001C4738
		private TableDetailInstanceList ReadTableDetailInstanceList(TableDetail detailDef)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.TableDetailInstanceList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			TableDetailInstanceList tableDetailInstanceList = new TableDetailInstanceList(num);
			for (int i = 0; i < num; i++)
			{
				tableDetailInstanceList.Add(this.ReadTableDetailInstance(detailDef));
			}
			this.m_reader.ReadEndObject();
			return tableDetailInstanceList;
		}

		// Token: 0x06006E1B RID: 28187 RVA: 0x001C65C8 File Offset: 0x001C47C8
		private TableGroupInstanceList ReadTableGroupInstanceList(TableGroup groupDef)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.TableGroupInstanceList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			TableGroupInstanceList tableGroupInstanceList = new TableGroupInstanceList(num);
			for (int i = 0; i < num; i++)
			{
				tableGroupInstanceList.Add(this.ReadTableGroupInstance(groupDef));
			}
			this.m_reader.ReadEndObject();
			return tableGroupInstanceList;
		}

		// Token: 0x06006E1C RID: 28188 RVA: 0x001C6654 File Offset: 0x001C4854
		private TableColumnInstance[] ReadTableColumnInstances()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Array == this.m_reader.Token);
			int arrayLength = this.m_reader.ArrayLength;
			TableColumnInstance[] array = new TableColumnInstance[arrayLength];
			for (int i = 0; i < arrayLength; i++)
			{
				array[i] = this.ReadTableColumnInstance();
			}
			return array;
		}

		// Token: 0x06006E1D RID: 28189 RVA: 0x001C66BC File Offset: 0x001C48BC
		private CustomReportItemHeadingList ReadCustomReportItemHeadingList(ReportItem parent)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.CustomReportItemHeadingList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			CustomReportItemHeadingList customReportItemHeadingList = new CustomReportItemHeadingList(num);
			for (int i = 0; i < num; i++)
			{
				customReportItemHeadingList.Add(this.ReadCustomReportItemHeading(parent));
			}
			this.m_reader.ReadEndObject();
			return customReportItemHeadingList;
		}

		// Token: 0x06006E1E RID: 28190 RVA: 0x001C674C File Offset: 0x001C494C
		private CustomReportItemHeadingInstanceList ReadCustomReportItemHeadingInstanceList(CustomReportItemHeadingList headingDefinitions)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.CustomReportItemHeadingInstanceList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			CustomReportItemHeadingInstanceList customReportItemHeadingInstanceList = new CustomReportItemHeadingInstanceList(num);
			for (int i = 0; i < num; i++)
			{
				customReportItemHeadingInstanceList.Add(this.ReadCustomReportItemHeadingInstance(headingDefinitions, i, num));
			}
			this.m_reader.ReadEndObject();
			return customReportItemHeadingInstanceList;
		}

		// Token: 0x06006E1F RID: 28191 RVA: 0x001C67E0 File Offset: 0x001C49E0
		private CustomReportItemCellInstancesList ReadCustomReportItemCellInstancesList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.CustomReportItemCellInstancesList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			CustomReportItemCellInstancesList customReportItemCellInstancesList = new CustomReportItemCellInstancesList(num);
			for (int i = 0; i < num; i++)
			{
				customReportItemCellInstancesList.Add(this.ReadCustomReportItemCellInstanceList());
			}
			this.m_reader.ReadEndObject();
			return customReportItemCellInstancesList;
		}

		// Token: 0x06006E20 RID: 28192 RVA: 0x001C6870 File Offset: 0x001C4A70
		private CustomReportItemCellInstanceList ReadCustomReportItemCellInstanceList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.CustomReportItemCellInstanceList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			CustomReportItemCellInstanceList customReportItemCellInstanceList = new CustomReportItemCellInstanceList(num);
			for (int i = 0; i < num; i++)
			{
				customReportItemCellInstanceList.Add(this.ReadCustomReportItemCellInstance());
			}
			this.m_reader.ReadEndObject();
			return customReportItemCellInstanceList;
		}

		// Token: 0x06006E21 RID: 28193 RVA: 0x001C6900 File Offset: 0x001C4B00
		private DocumentMapNode[] ReadDocumentMapNodes()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Array == this.m_reader.Token);
			int arrayLength = this.m_reader.ArrayLength;
			DocumentMapNode[] array = new DocumentMapNode[arrayLength];
			for (int i = 0; i < arrayLength; i++)
			{
				array[i] = this.ReadDocumentMapNode();
			}
			return array;
		}

		// Token: 0x06006E22 RID: 28194 RVA: 0x001C6968 File Offset: 0x001C4B68
		private DocumentMapNodeInfo[] ReadDocumentMapNodesInfo()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Array == this.m_reader.Token);
			int arrayLength = this.m_reader.ArrayLength;
			DocumentMapNodeInfo[] array = new DocumentMapNodeInfo[arrayLength];
			for (int i = 0; i < arrayLength; i++)
			{
				array[i] = this.ReadDocumentMapNodeInfo();
			}
			return array;
		}

		// Token: 0x06006E23 RID: 28195 RVA: 0x001C69D0 File Offset: 0x001C4BD0
		private bool FindDocumentMapNodesPage(string documentMapId, ref int page)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return false;
			}
			IntermediateFormatReader.Assert(Token.Array == this.m_reader.Token);
			int arrayLength = this.m_reader.ArrayLength;
			for (int i = 0; i < arrayLength; i++)
			{
				if (this.FindDocumentMapNodePage(documentMapId, ref page))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006E24 RID: 28196 RVA: 0x001C6A34 File Offset: 0x001C4C34
		private object[] ReadVariants()
		{
			return this.ReadVariants(false, true);
		}

		// Token: 0x06006E25 RID: 28197 RVA: 0x001C6A40 File Offset: 0x001C4C40
		private object[] ReadVariants(bool isMultiValue, bool readNextToken)
		{
			if (readNextToken)
			{
				IntermediateFormatReader.Assert(this.m_reader.Read());
			}
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Array == this.m_reader.Token);
			int arrayLength = this.m_reader.ArrayLength;
			object[] array = new object[arrayLength];
			for (int i = 0; i < arrayLength; i++)
			{
				if (isMultiValue)
				{
					this.ReadMultiValue(array, i);
				}
				else
				{
					array[i] = this.ReadVariant();
				}
			}
			return array;
		}

		// Token: 0x06006E26 RID: 28198 RVA: 0x001C6AB8 File Offset: 0x001C4CB8
		private void ReadMultiValue(object[] parentArray, int index)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return;
			}
			if (Token.Array != this.m_reader.Token)
			{
				IntermediateFormatReader.Assert(parentArray != null);
				parentArray[index] = this.ReadVariant(false);
				return;
			}
			IntermediateFormatReader.Assert(parentArray != null);
			parentArray[index] = this.ReadVariants(false, false);
		}

		// Token: 0x06006E27 RID: 28199 RVA: 0x001C6B19 File Offset: 0x001C4D19
		private object ReadMultiValue()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			if (Token.Array != this.m_reader.Token)
			{
				return this.ReadVariant(false);
			}
			return this.ReadVariants(false, false);
		}

		// Token: 0x06006E28 RID: 28200 RVA: 0x001C6B58 File Offset: 0x001C4D58
		private string[] ReadStrings()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Array == this.m_reader.Token);
			int arrayLength = this.m_reader.ArrayLength;
			string[] array = new string[arrayLength];
			for (int i = 0; i < arrayLength; i++)
			{
				array[i] = this.m_reader.ReadString();
			}
			return array;
		}

		// Token: 0x06006E29 RID: 28201 RVA: 0x001C6BC8 File Offset: 0x001C4DC8
		private VariantList ReadVariantList(bool convertDBNull)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.VariantList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			VariantList variantList = new VariantList(num);
			for (int i = 0; i < num; i++)
			{
				variantList.Add(this.ReadVariant(true, convertDBNull));
			}
			this.m_reader.ReadEndObject();
			return variantList;
		}

		// Token: 0x06006E2A RID: 28202 RVA: 0x001C6C58 File Offset: 0x001C4E58
		private VariantList[] ReadVariantLists(bool convertDBNull)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Array == this.m_reader.Token);
			int arrayLength = this.m_reader.ArrayLength;
			VariantList[] array = new VariantList[arrayLength];
			for (int i = 0; i < arrayLength; i++)
			{
				array[i] = this.ReadVariantList(convertDBNull);
			}
			return array;
		}

		// Token: 0x06006E2B RID: 28203 RVA: 0x001C6CC4 File Offset: 0x001C4EC4
		private ProcessingMessageList ReadProcessingMessageList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.ProcessingMessageList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			ProcessingMessageList processingMessageList = new ProcessingMessageList(num);
			for (int i = 0; i < num; i++)
			{
				processingMessageList.Add(this.ReadProcessingMessage());
			}
			this.m_reader.ReadEndObject();
			return processingMessageList;
		}

		// Token: 0x06006E2C RID: 28204 RVA: 0x001C6D54 File Offset: 0x001C4F54
		private DataCellsList ReadDataCellsList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.DataCellsList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			DataCellsList dataCellsList = new DataCellsList(num);
			for (int i = 0; i < num; i++)
			{
				dataCellsList.Add(this.ReadDataCellList());
			}
			this.m_reader.ReadEndObject();
			return dataCellsList;
		}

		// Token: 0x06006E2D RID: 28205 RVA: 0x001C6DE4 File Offset: 0x001C4FE4
		private DataCellList ReadDataCellList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.DataCellList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			DataCellList dataCellList = new DataCellList(num);
			for (int i = 0; i < num; i++)
			{
				dataCellList.Add(this.ReadDataValueCRIList());
			}
			this.m_reader.ReadEndObject();
			return dataCellList;
		}

		// Token: 0x06006E2E RID: 28206 RVA: 0x001C6E74 File Offset: 0x001C5074
		private DataValueCRIList ReadDataValueCRIList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = this.m_reader.ObjectType;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.DataValueCRIList == objectType);
			DataValueCRIList dataValueCRIList = new DataValueCRIList();
			this.ReadDataValueList(dataValueCRIList);
			if (this.PreRead(objectType, indexes))
			{
				dataValueCRIList.RDLRowIndex = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataValueCRIList.RDLColumnIndex = this.m_reader.ReadInt32();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return dataValueCRIList;
		}

		// Token: 0x06006E2F RID: 28207 RVA: 0x001C6F2C File Offset: 0x001C512C
		private DataValueList ReadDataValueList()
		{
			DataValueList dataValueList = new DataValueList();
			return this.ReadDataValueList(dataValueList);
		}

		// Token: 0x06006E30 RID: 28208 RVA: 0x001C6F48 File Offset: 0x001C5148
		private DataValueList ReadDataValueList(DataValueList values)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.DataValueList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			values.Capacity = num;
			for (int i = 0; i < num; i++)
			{
				values.Add(this.ReadDataValue());
			}
			this.m_reader.ReadEndObject();
			return values;
		}

		// Token: 0x06006E31 RID: 28209 RVA: 0x001C6FD8 File Offset: 0x001C51D8
		private DataValueInstanceList ReadDataValueInstanceList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.DataValueInstanceList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			DataValueInstanceList dataValueInstanceList = new DataValueInstanceList(num);
			for (int i = 0; i < num; i++)
			{
				dataValueInstanceList.Add(this.ReadDataValueInstance());
			}
			this.m_reader.ReadEndObject();
			return dataValueInstanceList;
		}

		// Token: 0x06006E32 RID: 28210 RVA: 0x001C7068 File Offset: 0x001C5268
		private ImageMapAreaInstanceList ReadImageMapAreaInstanceList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.ImageMapAreaInstanceList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			ImageMapAreaInstanceList imageMapAreaInstanceList = new ImageMapAreaInstanceList(num);
			for (int i = 0; i < num; i++)
			{
				imageMapAreaInstanceList.Add(this.ReadImageMapAreaInstance());
			}
			this.m_reader.ReadEndObject();
			return imageMapAreaInstanceList;
		}

		// Token: 0x06006E33 RID: 28211 RVA: 0x001C70F8 File Offset: 0x001C52F8
		private void ReadIDOwnerBase(IDOwner idOwner)
		{
			IntermediateFormatReader.Assert(idOwner != null);
			ObjectType objectType = ObjectType.IDOwner;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			if (this.PreRead(objectType, indexes))
			{
				idOwner.ID = this.m_reader.ReadInt32();
			}
			this.PostRead(objectType, indexes);
		}

		// Token: 0x06006E34 RID: 28212 RVA: 0x001C713C File Offset: 0x001C533C
		private ReportItem ReadReportItem(ReportItem parent)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			if (ObjectType.Line == this.m_reader.ObjectType)
			{
				return this.ReadLineInternals(parent);
			}
			if (ObjectType.Rectangle == this.m_reader.ObjectType)
			{
				return this.ReadRectangleInternals(parent);
			}
			if (ObjectType.Image == this.m_reader.ObjectType)
			{
				return this.ReadImageInternals(parent);
			}
			if (ObjectType.CheckBox == this.m_reader.ObjectType)
			{
				return this.ReadCheckBoxInternals(parent);
			}
			if (ObjectType.TextBox == this.m_reader.ObjectType)
			{
				return this.ReadTextBoxInternals(parent);
			}
			if (ObjectType.SubReport == this.m_reader.ObjectType)
			{
				return this.ReadSubReportInternals(parent);
			}
			if (ObjectType.ActiveXControl == this.m_reader.ObjectType)
			{
				return this.ReadActiveXControlInternals(parent);
			}
			return this.ReadDataRegionInternals(parent);
		}

		// Token: 0x06006E35 RID: 28213 RVA: 0x001C7220 File Offset: 0x001C5420
		private void ReadReportItemBase(ReportItem reportItem)
		{
			IntermediateFormatReader.Assert(reportItem != null);
			ObjectType objectType = ObjectType.ReportItem;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			this.ReadIDOwnerBase(reportItem);
			this.RegisterDefinitionObject(reportItem);
			if (this.PreRead(objectType, indexes))
			{
				reportItem.Name = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportItem.StyleClass = this.ReadStyle();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportItem.Top = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportItem.TopValue = this.m_reader.ReadDouble();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportItem.Left = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportItem.LeftValue = this.m_reader.ReadDouble();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportItem.Height = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportItem.HeightValue = this.m_reader.ReadDouble();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportItem.Width = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportItem.WidthValue = this.m_reader.ReadDouble();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportItem.ZIndex = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportItem.Visibility = this.ReadVisibility();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportItem.ToolTip = this.ReadExpressionInfo();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportItem.Label = this.ReadExpressionInfo();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportItem.Bookmark = this.ReadExpressionInfo();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportItem.Custom = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportItem.RepeatedSibling = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportItem.IsFullSize = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportItem.ExprHostID = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportItem.DataElementName = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportItem.DataElementOutput = this.ReadDataElementOutputType(reportItem.Visibility);
			}
			if (this.PreRead(objectType, indexes))
			{
				reportItem.DistanceFromReportTop = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportItem.DistanceBeforeTop = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportItem.SiblingAboveMe = this.ReadIntList();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportItem.CustomProperties = this.ReadDataValueList();
			}
			this.PostRead(objectType, indexes);
		}

		// Token: 0x06006E36 RID: 28214 RVA: 0x001C74D8 File Offset: 0x001C56D8
		private ReportItem ReadReportItemReference(bool getDefinition)
		{
			if (this.m_intermediateFormatVersion.IsRS2000_WithOtherPageChunkSplit)
			{
				IntermediateFormatReader.Assert(this.m_reader.ReadNoTypeReference());
			}
			else
			{
				IntermediateFormatReader.Assert(this.m_reader.Read());
			}
			if (this.m_reader.Token == Token.Null || !getDefinition)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Reference == this.m_reader.Token);
			IDOwner definitionObject = this.GetDefinitionObject(this.m_reader.ReferenceValue);
			IntermediateFormatReader.Assert(definitionObject is ReportItem);
			return (ReportItem)definitionObject;
		}

		// Token: 0x06006E37 RID: 28215 RVA: 0x001C7560 File Offset: 0x001C5760
		private SubReport ReadSubReportReference()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Reference == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.SubReport == this.m_reader.ObjectType);
			IDOwner definitionObject = this.GetDefinitionObject(this.m_reader.ReferenceValue);
			IntermediateFormatReader.Assert(definitionObject is SubReport);
			return (SubReport)definitionObject;
		}

		// Token: 0x06006E38 RID: 28216 RVA: 0x001C75D8 File Offset: 0x001C57D8
		private PageSection ReadPageSection(bool isHeader, ReportItem parent)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.PageSection;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			PageSection pageSection = new PageSection(isHeader, parent);
			this.ReadReportItemBase(pageSection);
			if (this.PreRead(objectType, indexes))
			{
				pageSection.PrintOnFirstPage = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				pageSection.PrintOnLastPage = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				pageSection.ReportItems = this.ReadReportItemCollection(pageSection);
			}
			if (this.PreRead(objectType, indexes))
			{
				pageSection.PostProcessEvaluate = this.m_reader.ReadBoolean();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return pageSection;
		}

		// Token: 0x06006E39 RID: 28217 RVA: 0x001C76C0 File Offset: 0x001C58C0
		private ReportItemCollection ReadReportItemCollection(ReportItem parent)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.ReportItemCollection;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ReportItemCollection reportItemCollection = new ReportItemCollection();
			this.ReadIDOwnerBase(reportItemCollection);
			this.RegisterDefinitionObject(reportItemCollection);
			if (this.PreRead(objectType, indexes))
			{
				reportItemCollection.NonComputedReportItems = this.ReadReportItemList(parent);
			}
			if (this.PreRead(objectType, indexes))
			{
				reportItemCollection.ComputedReportItems = this.ReadReportItemList(parent);
			}
			if (this.PreRead(objectType, indexes))
			{
				reportItemCollection.SortedReportItems = this.ReadReportItemIndexerList();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportItemCollection.RunningValues = this.ReadRunningValueInfoList();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return reportItemCollection;
		}

		// Token: 0x06006E3A RID: 28218 RVA: 0x001C77A0 File Offset: 0x001C59A0
		private Report.ShowHideTypes ReadShowHideTypes()
		{
			int num = this.m_reader.ReadEnum();
			IntermediateFormatReader.Assert(Enum.IsDefined(typeof(Report.ShowHideTypes), num));
			return (Report.ShowHideTypes)num;
		}

		// Token: 0x06006E3B RID: 28219 RVA: 0x001C77D4 File Offset: 0x001C59D4
		private DataElementOutputTypes ReadDataElementOutputType(Visibility visibility)
		{
			int num = this.m_reader.ReadEnum();
			IntermediateFormatReader.Assert(Enum.IsDefined(typeof(DataElementOutputTypes), num));
			DataElementOutputTypes dataElementOutputTypes = (DataElementOutputTypes)num;
			if (dataElementOutputTypes == DataElementOutputTypes.Output && (this.m_intermediateFormatVersion == null || !this.m_intermediateFormatVersion.IsRS2005_WithXmlDataElementOutputChange) && visibility != null && visibility.Hidden != null && ExpressionInfo.Types.Constant == visibility.Hidden.Type && visibility.Hidden.BoolValue && visibility.Toggle == null)
			{
				dataElementOutputTypes = DataElementOutputTypes.NoOutput;
			}
			return dataElementOutputTypes;
		}

		// Token: 0x06006E3C RID: 28220 RVA: 0x001C7854 File Offset: 0x001C5A54
		private Style ReadStyle()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.Style;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			Style style = new Style(ConstructionPhase.Deserializing);
			if (this.PreRead(objectType, indexes))
			{
				style.StyleAttributes = this.ReadStyleAttributeHashtable();
			}
			if (this.PreRead(objectType, indexes))
			{
				style.ExpressionList = this.ReadExpressionInfoList();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return style;
		}

		// Token: 0x06006E3D RID: 28221 RVA: 0x001C78F8 File Offset: 0x001C5AF8
		private Visibility ReadVisibility()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.Visibility;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			Visibility visibility = new Visibility();
			if (this.PreRead(objectType, indexes))
			{
				visibility.Hidden = this.ReadExpressionInfo();
			}
			if (this.PreRead(objectType, indexes))
			{
				visibility.Toggle = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				visibility.RecursiveReceiver = this.m_reader.ReadBoolean();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return visibility;
		}

		// Token: 0x06006E3E RID: 28222 RVA: 0x001C79BC File Offset: 0x001C5BBC
		private Filter ReadFilter()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.Filter;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			Filter filter = new Filter();
			if (this.PreRead(objectType, indexes))
			{
				filter.Expression = this.ReadExpressionInfo();
			}
			if (this.PreRead(objectType, indexes))
			{
				filter.Operator = this.ReadOperators();
			}
			if (this.PreRead(objectType, indexes))
			{
				filter.Values = this.ReadExpressionInfoList();
			}
			if (this.PreRead(objectType, indexes))
			{
				filter.ExprHostID = this.m_reader.ReadInt32();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return filter;
		}

		// Token: 0x06006E3F RID: 28223 RVA: 0x001C7A90 File Offset: 0x001C5C90
		private Filter.Operators ReadOperators()
		{
			int num = this.m_reader.ReadEnum();
			IntermediateFormatReader.Assert(Enum.IsDefined(typeof(Filter.Operators), num));
			return (Filter.Operators)num;
		}

		// Token: 0x06006E40 RID: 28224 RVA: 0x001C7AC4 File Offset: 0x001C5CC4
		private DataSource ReadDataSource()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.DataSource;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			DataSource dataSource = new DataSource();
			if (this.PreRead(objectType, indexes))
			{
				dataSource.Name = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataSource.Transaction = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataSource.Type = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataSource.ConnectStringExpression = this.ReadExpressionInfo();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataSource.IntegratedSecurity = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataSource.Prompt = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataSource.DataSourceReference = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataSource.DataSets = this.ReadDataSetList();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataSource.ID = this.m_reader.ReadGuid();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataSource.ExprHostID = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataSource.SharedDataSourceReferencePath = this.m_reader.ReadString();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return dataSource;
		}

		// Token: 0x06006E41 RID: 28225 RVA: 0x001C7C58 File Offset: 0x001C5E58
		private DataAggregateInfo ReadDataAggregateInfo()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			if (ObjectType.RunningValueInfo == this.m_reader.ObjectType)
			{
				return this.ReadRunningValueInfoInternals();
			}
			IntermediateFormatReader.Assert(ObjectType.DataAggregateInfo == this.m_reader.ObjectType);
			DataAggregateInfo dataAggregateInfo = new DataAggregateInfo();
			this.ReadDataAggregateInfoBase(dataAggregateInfo);
			this.m_reader.ReadEndObject();
			return dataAggregateInfo;
		}

		// Token: 0x06006E42 RID: 28226 RVA: 0x001C7CDC File Offset: 0x001C5EDC
		private void ReadDataAggregateInfoBase(DataAggregateInfo aggregate)
		{
			IntermediateFormatReader.Assert(aggregate != null);
			ObjectType objectType = ObjectType.DataAggregateInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			if (this.PreRead(objectType, indexes))
			{
				aggregate.Name = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				aggregate.AggregateType = this.ReadAggregateTypes();
			}
			if (this.PreRead(objectType, indexes))
			{
				aggregate.Expressions = this.ReadExpressionInfos();
			}
			if (this.PreRead(objectType, indexes))
			{
				aggregate.DuplicateNames = this.ReadStringList();
			}
			this.PostRead(objectType, indexes);
		}

		// Token: 0x06006E43 RID: 28227 RVA: 0x001C7D60 File Offset: 0x001C5F60
		private ExpressionInfo ReadExpressionInfo()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.ExpressionInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token || Token.String == this.m_reader.Token || Token.Boolean == this.m_reader.Token || Token.Int32 == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType || this.m_reader.ObjectType == ObjectType.None);
			ExpressionInfo expressionInfo = new ExpressionInfo();
			if (this.m_reader.ObjectType == ObjectType.None)
			{
				expressionInfo.Type = ExpressionInfo.Types.Constant;
				Token token = this.m_reader.Token;
				if (token != Token.String)
				{
					if (token != Token.Boolean)
					{
						if (token != Token.Int32)
						{
							IntermediateFormatReader.Assert(false);
						}
						else
						{
							expressionInfo.IntValue = this.m_reader.Int32Value;
						}
					}
					else
					{
						expressionInfo.BoolValue = this.m_reader.BooleanValue;
					}
				}
				else
				{
					expressionInfo.Value = this.m_reader.StringValue;
				}
			}
			else
			{
				if (this.PreRead(objectType, indexes))
				{
					expressionInfo.Type = this.ReadTypes();
				}
				if (this.PreRead(objectType, indexes))
				{
					expressionInfo.Value = this.m_reader.ReadString();
				}
				if (this.PreRead(objectType, indexes))
				{
					expressionInfo.BoolValue = this.m_reader.ReadBoolean();
				}
				if (this.PreRead(objectType, indexes))
				{
					expressionInfo.IntValue = this.m_reader.ReadInt32();
				}
				if (this.PreRead(objectType, indexes))
				{
					expressionInfo.ExprHostID = this.m_reader.ReadInt32();
				}
				if (this.PreRead(objectType, indexes))
				{
					expressionInfo.OriginalText = this.m_reader.ReadString();
				}
				this.PostRead(objectType, indexes);
				this.m_reader.ReadEndObject();
			}
			return expressionInfo;
		}

		// Token: 0x06006E44 RID: 28228 RVA: 0x001C7F44 File Offset: 0x001C6144
		private DataAggregateInfo.AggregateTypes ReadAggregateTypes()
		{
			int num = this.m_reader.ReadEnum();
			IntermediateFormatReader.Assert(Enum.IsDefined(typeof(DataAggregateInfo.AggregateTypes), num));
			return (DataAggregateInfo.AggregateTypes)num;
		}

		// Token: 0x06006E45 RID: 28229 RVA: 0x001C7F78 File Offset: 0x001C6178
		private ExpressionInfo.Types ReadTypes()
		{
			int num = this.m_reader.ReadEnum();
			IntermediateFormatReader.Assert(Enum.IsDefined(typeof(ExpressionInfo.Types), num));
			return (ExpressionInfo.Types)num;
		}

		// Token: 0x06006E46 RID: 28230 RVA: 0x001C7FAC File Offset: 0x001C61AC
		private ReportItemIndexer ReadReportItemIndexer()
		{
			ObjectType objectType = ObjectType.ReportItemIndexer;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(objectType == this.m_reader.ReadObject());
			ReportItemIndexer reportItemIndexer = default(ReportItemIndexer);
			if (this.PreRead(objectType, indexes))
			{
				reportItemIndexer.IsComputed = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportItemIndexer.Index = this.m_reader.ReadInt32();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return reportItemIndexer;
		}

		// Token: 0x06006E47 RID: 28231 RVA: 0x001C802C File Offset: 0x001C622C
		private RenderingPagesRanges ReadRenderingPagesRanges()
		{
			ObjectType objectType = ObjectType.RenderingPagesRanges;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(objectType == this.m_reader.ReadObject());
			RenderingPagesRanges renderingPagesRanges = default(RenderingPagesRanges);
			if (this.PreRead(objectType, indexes))
			{
				renderingPagesRanges.StartPage = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				renderingPagesRanges.EndPage = this.m_reader.ReadInt32();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return renderingPagesRanges;
		}

		// Token: 0x06006E48 RID: 28232 RVA: 0x001C80AC File Offset: 0x001C62AC
		private RunningValueInfo ReadRunningValueInfo()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			return this.ReadRunningValueInfoInternals();
		}

		// Token: 0x06006E49 RID: 28233 RVA: 0x001C80D4 File Offset: 0x001C62D4
		private RunningValueInfo ReadRunningValueInfoInternals()
		{
			ObjectType objectType = ObjectType.RunningValueInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			RunningValueInfo runningValueInfo = new RunningValueInfo();
			this.ReadDataAggregateInfoBase(runningValueInfo);
			if (this.PreRead(objectType, indexes))
			{
				runningValueInfo.Scope = this.m_reader.ReadString();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return runningValueInfo;
		}

		// Token: 0x06006E4A RID: 28234 RVA: 0x001C814C File Offset: 0x001C634C
		private AttributeInfo ReadAttributeInfo()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.AttributeInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			AttributeInfo attributeInfo = new AttributeInfo();
			if (this.PreRead(objectType, indexes))
			{
				attributeInfo.IsExpression = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				attributeInfo.Value = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				attributeInfo.BoolValue = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				attributeInfo.IntValue = this.m_reader.ReadInt32();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return attributeInfo;
		}

		// Token: 0x06006E4B RID: 28235 RVA: 0x001C8230 File Offset: 0x001C6430
		private DataSet ReadDataSet()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.DataSet;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			DataSet dataSet = new DataSet();
			if (this.m_intermediateFormatVersion.Is_WithUserSort)
			{
				this.ReadIDOwnerBase(dataSet);
				this.RegisterDefinitionObject(dataSet);
			}
			if (this.PreRead(objectType, indexes))
			{
				dataSet.Name = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataSet.Fields = this.ReadDataFieldList();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataSet.Query = this.ReadReportQuery();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataSet.CaseSensitivity = this.ReadSensitivity();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataSet.Collation = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataSet.AccentSensitivity = this.ReadSensitivity();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataSet.KanatypeSensitivity = this.ReadSensitivity();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataSet.WidthSensitivity = this.ReadSensitivity();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataSet.DataRegions = this.ReadDataRegionList();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataSet.Aggregates = this.ReadDataAggregateInfoList();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataSet.Filters = this.ReadFilterList();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataSet.RecordSetSize = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataSet.UsedOnlyInParameters = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataSet.NonCalculatedFieldCount = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataSet.ExprHostID = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataSet.PostSortAggregates = this.ReadDataAggregateInfoList();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataSet.LCID = (uint)this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataSet.HasDetailUserSortFilter = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataSet.UserSortExpressions = this.ReadExpressionInfoList();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataSet.DynamicFieldReferences = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataSet.InterpretSubtotalsAsDetails = this.m_reader.ReadBoolean();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return dataSet;
		}

		// Token: 0x06006E4C RID: 28236 RVA: 0x001C84C0 File Offset: 0x001C66C0
		private ReportQuery ReadReportQuery()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.ReportQuery;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ReportQuery reportQuery = new ReportQuery();
			if (this.PreRead(objectType, indexes))
			{
				reportQuery.CommandType = this.ReadCommandType();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportQuery.CommandText = this.ReadExpressionInfo();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportQuery.Parameters = this.ReadParameterValueList();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportQuery.TimeOut = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportQuery.CommandTextValue = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportQuery.RewrittenCommandText = this.m_reader.ReadString();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return reportQuery;
		}

		// Token: 0x06006E4D RID: 28237 RVA: 0x001C85C8 File Offset: 0x001C67C8
		private DataSet.Sensitivity ReadSensitivity()
		{
			int num = this.m_reader.ReadEnum();
			IntermediateFormatReader.Assert(Enum.IsDefined(typeof(DataSet.Sensitivity), num));
			return (DataSet.Sensitivity)num;
		}

		// Token: 0x06006E4E RID: 28238 RVA: 0x001C85FC File Offset: 0x001C67FC
		private CommandType ReadCommandType()
		{
			int num = this.m_reader.ReadEnum();
			IntermediateFormatReader.Assert(Enum.IsDefined(typeof(CommandType), num));
			return (CommandType)num;
		}

		// Token: 0x06006E4F RID: 28239 RVA: 0x001C8630 File Offset: 0x001C6830
		private Field ReadDataField()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.Field;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			Field field = new Field();
			if (this.PreRead(objectType, indexes))
			{
				field.Name = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				field.DataField = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				field.Value = this.ReadExpressionInfo();
			}
			if (this.PreRead(objectType, indexes))
			{
				field.ExprHostID = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				field.DynamicPropertyReferences = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				field.ReferencedProperties = this.ReadFieldPropertyHashtable();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return field;
		}

		// Token: 0x06006E50 RID: 28240 RVA: 0x001C8740 File Offset: 0x001C6940
		internal FieldPropertyHashtable ReadFieldPropertyHashtable()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.FieldPropertyHashtable == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			FieldPropertyHashtable fieldPropertyHashtable = new FieldPropertyHashtable(num);
			for (int i = 0; i < num; i++)
			{
				string text = this.m_reader.ReadString();
				fieldPropertyHashtable.Add(text);
			}
			this.m_reader.ReadEndObject();
			return fieldPropertyHashtable;
		}

		// Token: 0x06006E51 RID: 28241 RVA: 0x001C87D4 File Offset: 0x001C69D4
		private ParameterValue ReadParameterValue()
		{
			ObjectType objectType = ObjectType.ParameterValue;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(objectType == this.m_reader.ReadObject());
			ParameterValue parameterValue = new ParameterValue();
			if (this.PreRead(objectType, indexes))
			{
				parameterValue.Name = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				parameterValue.Value = this.ReadExpressionInfo();
			}
			if (this.PreRead(objectType, indexes))
			{
				parameterValue.ExprHostID = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				parameterValue.Omit = this.ReadExpressionInfo();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return parameterValue;
		}

		// Token: 0x06006E52 RID: 28242 RVA: 0x001C887C File Offset: 0x001C6A7C
		private CodeClass ReadCodeClass()
		{
			ObjectType objectType = ObjectType.CodeClass;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(objectType == this.m_reader.ReadObject());
			CodeClass codeClass = default(CodeClass);
			if (this.PreRead(objectType, indexes))
			{
				codeClass.ClassName = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				codeClass.InstanceName = this.m_reader.ReadString();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return codeClass;
		}

		// Token: 0x06006E53 RID: 28243 RVA: 0x001C88FC File Offset: 0x001C6AFC
		private Microsoft.ReportingServices.ReportProcessing.Action ReadAction()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.Action;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			Microsoft.ReportingServices.ReportProcessing.Action action = new Microsoft.ReportingServices.ReportProcessing.Action();
			if (this.PreRead(objectType, indexes))
			{
				action.ActionItems = this.ReadActionItemList();
			}
			if (this.PreRead(objectType, indexes))
			{
				action.StyleClass = this.ReadStyle();
			}
			if (this.PreRead(objectType, indexes))
			{
				action.ComputedActionItemsCount = this.m_reader.ReadInt32();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return action;
		}

		// Token: 0x06006E54 RID: 28244 RVA: 0x001C89BC File Offset: 0x001C6BBC
		private ActionItemList ReadActionItemList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.ActionItemList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			ActionItemList actionItemList = new ActionItemList(num);
			for (int i = 0; i < num; i++)
			{
				actionItemList.Add(this.ReadActionItem());
			}
			this.m_reader.ReadEndObject();
			return actionItemList;
		}

		// Token: 0x06006E55 RID: 28245 RVA: 0x001C8A4C File Offset: 0x001C6C4C
		private ActionItem ReadActionItem()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ActionItem actionItem = new ActionItem();
			if (this.m_intermediateFormatVersion.IsRS2005_WithMultipleActions)
			{
				ObjectType objectType = ObjectType.ActionItem;
				IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
				IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
				IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
				if (this.PreRead(objectType, indexes))
				{
					actionItem.HyperLinkURL = this.ReadExpressionInfo();
				}
				if (this.PreRead(objectType, indexes))
				{
					actionItem.DrillthroughReportName = this.ReadExpressionInfo();
				}
				if (this.PreRead(objectType, indexes))
				{
					actionItem.DrillthroughParameters = this.ReadParameterValueList();
				}
				if (this.PreRead(objectType, indexes))
				{
					actionItem.DrillthroughBookmarkLink = this.ReadExpressionInfo();
				}
				if (this.PreRead(objectType, indexes))
				{
					actionItem.BookmarkLink = this.ReadExpressionInfo();
				}
				if (this.PreRead(objectType, indexes))
				{
					actionItem.Label = this.ReadExpressionInfo();
				}
				if (this.PreRead(objectType, indexes))
				{
					actionItem.ExprHostID = this.m_reader.ReadInt32();
				}
				if (this.PreRead(objectType, indexes))
				{
					actionItem.ComputedIndex = this.m_reader.ReadInt32();
				}
				this.PostRead(objectType, indexes);
			}
			else
			{
				ObjectType objectType2 = ObjectType.Action;
				IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
				IntermediateFormatReader.Assert(objectType2 == this.m_reader.ObjectType);
				actionItem.ComputedIndex = 0;
				actionItem.HyperLinkURL = this.ReadExpressionInfo();
				actionItem.DrillthroughReportName = this.ReadExpressionInfo();
				actionItem.DrillthroughParameters = this.ReadParameterValueList();
				actionItem.DrillthroughBookmarkLink = this.ReadExpressionInfo();
				actionItem.BookmarkLink = this.ReadExpressionInfo();
			}
			this.m_reader.ReadEndObject();
			return actionItem;
		}

		// Token: 0x06006E56 RID: 28246 RVA: 0x001C8C00 File Offset: 0x001C6E00
		private Line ReadLineInternals(ReportItem parent)
		{
			ObjectType objectType = ObjectType.Line;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			Line line = new Line(parent);
			this.ReadReportItemBase(line);
			if (this.PreRead(objectType, indexes))
			{
				line.LineSlant = this.m_reader.ReadBoolean();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return line;
		}

		// Token: 0x06006E57 RID: 28247 RVA: 0x001C8C78 File Offset: 0x001C6E78
		private Rectangle ReadRectangleInternals(ReportItem parent)
		{
			ObjectType objectType = ObjectType.Rectangle;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			Rectangle rectangle = new Rectangle(parent);
			this.ReadReportItemBase(rectangle);
			if (this.PreRead(objectType, indexes))
			{
				rectangle.ReportItems = this.ReadReportItemCollection(rectangle);
			}
			if (this.PreRead(objectType, indexes))
			{
				rectangle.PageBreakAtEnd = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				rectangle.PageBreakAtStart = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				rectangle.LinkToChild = this.m_reader.ReadInt32();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return rectangle;
		}

		// Token: 0x06006E58 RID: 28248 RVA: 0x001C8D40 File Offset: 0x001C6F40
		private Image ReadImageInternals(ReportItem parent)
		{
			ObjectType objectType = ObjectType.Image;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			Image image = new Image(parent);
			this.ReadReportItemBase(image);
			if (this.PreRead(objectType, indexes))
			{
				if (this.m_intermediateFormatVersion.IsRS2005_WithMultipleActions)
				{
					image.Action = this.ReadAction();
				}
				else
				{
					ActionItem actionItem = this.ReadActionItem();
					if (actionItem != null)
					{
						image.Action = new Microsoft.ReportingServices.ReportProcessing.Action(actionItem, true);
					}
				}
			}
			if (this.PreRead(objectType, indexes))
			{
				image.Source = this.ReadSourceType();
			}
			if (this.PreRead(objectType, indexes))
			{
				image.Value = this.ReadExpressionInfo();
			}
			if (this.PreRead(objectType, indexes))
			{
				image.MIMEType = this.ReadExpressionInfo();
			}
			if (this.PreRead(objectType, indexes))
			{
				image.Sizing = this.ReadSizings();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return image;
		}

		// Token: 0x06006E59 RID: 28249 RVA: 0x001C8E34 File Offset: 0x001C7034
		private ImageMapAreaInstance ReadImageMapAreaInstance()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ImageMapAreaInstance imageMapAreaInstance = new ImageMapAreaInstance();
			ObjectType objectType = ObjectType.ImageMapAreaInstance;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			if (this.PreRead(objectType, indexes))
			{
				imageMapAreaInstance.ID = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				imageMapAreaInstance.Shape = this.ReadImageMapAreaShape();
			}
			if (this.PreRead(objectType, indexes))
			{
				imageMapAreaInstance.Coordinates = this.m_reader.ReadFloatArray();
			}
			if (this.PreRead(objectType, indexes))
			{
				imageMapAreaInstance.Action = this.ReadAction();
			}
			if (this.PreRead(objectType, indexes))
			{
				imageMapAreaInstance.ActionInstance = this.ReadActionInstance(null);
			}
			if (this.PreRead(objectType, indexes))
			{
				imageMapAreaInstance.UniqueName = this.m_reader.ReadInt32();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return imageMapAreaInstance;
		}

		// Token: 0x06006E5A RID: 28250 RVA: 0x001C8F40 File Offset: 0x001C7140
		private Image.SourceType ReadSourceType()
		{
			int num = this.m_reader.ReadEnum();
			IntermediateFormatReader.Assert(Enum.IsDefined(typeof(Image.SourceType), num));
			return (Image.SourceType)num;
		}

		// Token: 0x06006E5B RID: 28251 RVA: 0x001C8F74 File Offset: 0x001C7174
		private Image.Sizings ReadSizings()
		{
			int num = this.m_reader.ReadEnum();
			IntermediateFormatReader.Assert(Enum.IsDefined(typeof(Image.Sizings), num));
			return (Image.Sizings)num;
		}

		// Token: 0x06006E5C RID: 28252 RVA: 0x001C8FA8 File Offset: 0x001C71A8
		private ImageMapArea.ImageMapAreaShape ReadImageMapAreaShape()
		{
			int num = this.m_reader.ReadEnum();
			IntermediateFormatReader.Assert(Enum.IsDefined(typeof(ImageMapArea.ImageMapAreaShape), num));
			return (ImageMapArea.ImageMapAreaShape)num;
		}

		// Token: 0x06006E5D RID: 28253 RVA: 0x001C8FDC File Offset: 0x001C71DC
		private CheckBox ReadCheckBoxInternals(ReportItem parent)
		{
			ObjectType objectType = ObjectType.CheckBox;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			CheckBox checkBox = new CheckBox(parent);
			this.ReadReportItemBase(checkBox);
			if (this.PreRead(objectType, indexes))
			{
				checkBox.Value = this.ReadExpressionInfo();
			}
			if (this.PreRead(objectType, indexes))
			{
				checkBox.HideDuplicates = this.m_reader.ReadString();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return checkBox;
		}

		// Token: 0x06006E5E RID: 28254 RVA: 0x001C906C File Offset: 0x001C726C
		private TextBox ReadTextBoxInternals(ReportItem parent)
		{
			ObjectType objectType = ObjectType.TextBox;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			TextBox textBox = new TextBox(parent);
			this.ReadReportItemBase(textBox);
			if (this.PreRead(objectType, indexes))
			{
				textBox.Value = this.ReadExpressionInfo();
			}
			if (this.PreRead(objectType, indexes))
			{
				textBox.CanGrow = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				textBox.CanShrink = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				textBox.HideDuplicates = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				if (this.m_intermediateFormatVersion.IsRS2005_WithMultipleActions)
				{
					textBox.Action = this.ReadAction();
				}
				else
				{
					ActionItem actionItem = this.ReadActionItem();
					if (actionItem != null)
					{
						textBox.Action = new Microsoft.ReportingServices.ReportProcessing.Action(actionItem, true);
					}
				}
			}
			if (this.PreRead(objectType, indexes))
			{
				textBox.IsToggle = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				textBox.InitialToggleState = this.ReadExpressionInfo();
			}
			if (this.PreRead(objectType, indexes))
			{
				textBox.ValueType = this.ReadTypeCode();
			}
			if (this.PreRead(objectType, indexes))
			{
				textBox.Formula = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				textBox.ValueReferenced = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				textBox.RecursiveSender = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				textBox.DataElementStyleAttribute = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				textBox.ContainingScopes = this.ReadGroupingReferenceList();
			}
			if (this.PreRead(objectType, indexes))
			{
				textBox.UserSort = this.ReadEndUserSort(textBox);
			}
			if (this.PreRead(objectType, indexes))
			{
				textBox.IsMatrixCellScope = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				textBox.IsSubReportTopLevelScope = this.m_reader.ReadBoolean();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return textBox;
		}

		// Token: 0x06006E5F RID: 28255 RVA: 0x001C9284 File Offset: 0x001C7484
		private EndUserSort ReadEndUserSort(TextBox eventSource)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.EndUserSort;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			EndUserSort endUserSort = new EndUserSort();
			if (this.PreRead(objectType, indexes))
			{
				endUserSort.DataSetID = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				endUserSort.SortExpressionScopeID = this.ReadIDOwnerID(ObjectType.ISortFilterScope);
			}
			if (this.PreRead(objectType, indexes))
			{
				endUserSort.GroupInSortTargetIDs = this.ReadGroupingIDList();
			}
			if (this.PreRead(objectType, indexes))
			{
				endUserSort.SortTargetID = this.ReadIDOwnerID(ObjectType.ISortFilterScope);
			}
			if (this.PreRead(objectType, indexes))
			{
				endUserSort.SortExpressionIndex = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				endUserSort.DetailScopeSubReports = this.ReadSubReportList();
			}
			if (-1 != endUserSort.SortExpressionScopeID || endUserSort.GroupInSortTargetIDs != null || -1 != endUserSort.SortTargetID)
			{
				Global.Tracer.Assert(this.m_textboxesWithUserSort != null && 0 < this.m_textboxesWithUserSort.Count);
				TextBoxList textBoxList = (TextBoxList)this.m_textboxesWithUserSort[this.m_textboxesWithUserSort.Count - 1];
				Global.Tracer.Assert(textBoxList != null, "(null != textboxesWithUserSort)");
				textBoxList.Add(eventSource);
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return endUserSort;
		}

		// Token: 0x06006E60 RID: 28256 RVA: 0x001C940C File Offset: 0x001C760C
		private IntList ReadGroupingIDList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.GroupingList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			IntList intList = new IntList(num);
			for (int i = 0; i < num; i++)
			{
				intList.Add(this.ReadIDOwnerID(ObjectType.Grouping));
			}
			this.m_reader.ReadEndObject();
			return intList;
		}

		// Token: 0x06006E61 RID: 28257 RVA: 0x001C94A4 File Offset: 0x001C76A4
		private GroupingList ReadGroupingReferenceList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.GroupingList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			GroupingList groupingList = new GroupingList(num);
			for (int i = 0; i < num; i++)
			{
				groupingList.Add(this.ReadGroupingReference());
			}
			this.m_reader.ReadEndObject();
			return groupingList;
		}

		// Token: 0x06006E62 RID: 28258 RVA: 0x001C9534 File Offset: 0x001C7734
		private Grouping ReadGroupingReference()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Reference == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.Grouping == this.m_reader.ObjectType);
			IDOwner definitionObject = this.GetDefinitionObject(this.m_reader.ReferenceValue);
			IntermediateFormatReader.Assert(definitionObject is ReportHierarchyNode);
			return ((ReportHierarchyNode)definitionObject).Grouping;
		}

		// Token: 0x06006E63 RID: 28259 RVA: 0x001C95B0 File Offset: 0x001C77B0
		private TypeCode ReadTypeCode()
		{
			int num = this.m_reader.ReadEnum();
			IntermediateFormatReader.Assert(Enum.IsDefined(typeof(TypeCode), num));
			return (TypeCode)num;
		}

		// Token: 0x06006E64 RID: 28260 RVA: 0x001C95E4 File Offset: 0x001C77E4
		private SubReport ReadSubReportInternals(ReportItem parent)
		{
			ObjectType objectType = ObjectType.SubReport;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			SubReport subReport = new SubReport(parent);
			this.ReadReportItemBase(subReport);
			if (this.PreRead(objectType, indexes))
			{
				subReport.ReportPath = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				subReport.Parameters = this.ReadParameterValueList();
			}
			if (this.PreRead(objectType, indexes))
			{
				subReport.NoRows = this.ReadExpressionInfo();
			}
			if (this.PreRead(objectType, indexes))
			{
				subReport.MergeTransactions = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				subReport.ContainingScopes = this.ReadGroupingReferenceList();
			}
			if (this.PreRead(objectType, indexes))
			{
				subReport.IsMatrixCellScope = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				subReport.DataSetUniqueNameMap = this.ReadScopeLookupTable();
			}
			if (this.PreRead(objectType, indexes))
			{
				subReport.RetrievalStatus = this.ReadStatus();
			}
			if (this.PreRead(objectType, indexes))
			{
				subReport.ReportName = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				subReport.Description = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				subReport.Report = this.ReadReport(subReport);
			}
			if (this.PreRead(objectType, indexes))
			{
				subReport.StringUri = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				subReport.ParametersFromCatalog = this.ReadParameterInfoCollection();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return subReport;
		}

		// Token: 0x06006E65 RID: 28261 RVA: 0x001C9780 File Offset: 0x001C7980
		private ScopeLookupTable ReadScopeLookupTable()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.ScopeLookupTable;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ScopeLookupTable scopeLookupTable = new ScopeLookupTable();
			if (this.PreRead(objectType, indexes))
			{
				scopeLookupTable.LookupTable = this.ReadScopeTableValues();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return scopeLookupTable;
		}

		// Token: 0x06006E66 RID: 28262 RVA: 0x001C9810 File Offset: 0x001C7A10
		private object ReadScopeTableValues()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (Token.Int32 == this.m_reader.Token)
			{
				return this.m_reader.Int32Value;
			}
			IntermediateFormatReader.Assert(Token.Array == this.m_reader.Token);
			int arrayLength = this.m_reader.ArrayLength;
			Hashtable hashtable = new Hashtable(arrayLength);
			for (int i = 0; i < arrayLength; i++)
			{
				object obj = this.ReadVariant(true, true);
				object obj2 = this.ReadScopeTableValues();
				hashtable.Add(obj, obj2);
			}
			return hashtable;
		}

		// Token: 0x06006E67 RID: 28263 RVA: 0x001C98A0 File Offset: 0x001C7AA0
		private SubReport.Status ReadStatus()
		{
			int num = this.m_reader.ReadEnum();
			IntermediateFormatReader.Assert(Enum.IsDefined(typeof(SubReport.Status), num));
			return (SubReport.Status)num;
		}

		// Token: 0x06006E68 RID: 28264 RVA: 0x001C98D4 File Offset: 0x001C7AD4
		private ActiveXControl ReadActiveXControlInternals(ReportItem parent)
		{
			ObjectType objectType = ObjectType.ActiveXControl;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ActiveXControl activeXControl = new ActiveXControl(parent);
			this.ReadReportItemBase(activeXControl);
			if (this.PreRead(objectType, indexes))
			{
				activeXControl.ClassID = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				activeXControl.CodeBase = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				activeXControl.Parameters = this.ReadParameterValueList();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return activeXControl;
		}

		// Token: 0x06006E69 RID: 28265 RVA: 0x001C9980 File Offset: 0x001C7B80
		private ParameterBase ReadParameterBase(ParameterBase parameter)
		{
			IntermediateFormatReader.Assert(parameter != null);
			ObjectType objectType = ObjectType.ParameterBase;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			if (this.PreRead(objectType, indexes))
			{
				parameter.Name = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				parameter.DataType = this.ReadDataType();
			}
			if (this.PreRead(objectType, indexes))
			{
				parameter.Nullable = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				parameter.Prompt = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				parameter.UsedInQuery = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				parameter.AllowBlank = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				parameter.MultiValue = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				parameter.DefaultValues = this.ReadVariants();
			}
			if (this.PreRead(objectType, indexes))
			{
				parameter.PromptUser = this.m_reader.ReadBoolean();
			}
			this.PostRead(objectType, indexes);
			return parameter;
		}

		// Token: 0x06006E6A RID: 28266 RVA: 0x001C9A94 File Offset: 0x001C7C94
		private ParameterDef ReadParameterDef()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.ParameterDef;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ParameterDef parameterDef = new ParameterDef();
			this.ReadParameterBase(parameterDef);
			this.RegisterParameterDef(parameterDef);
			if (this.PreRead(objectType, indexes))
			{
				parameterDef.ValidValuesDataSource = this.ReadParameterDataSource();
			}
			if (this.PreRead(objectType, indexes))
			{
				parameterDef.ValidValuesValueExpressions = this.ReadExpressionInfoList();
			}
			if (this.PreRead(objectType, indexes))
			{
				parameterDef.ValidValuesLabelExpressions = this.ReadExpressionInfoList();
			}
			if (this.PreRead(objectType, indexes))
			{
				parameterDef.DefaultDataSource = this.ReadParameterDataSource();
			}
			if (this.PreRead(objectType, indexes))
			{
				parameterDef.DefaultExpressions = this.ReadExpressionInfoList();
			}
			if (this.PreRead(objectType, indexes))
			{
				parameterDef.DependencyList = this.ReadParameterDefRefList();
			}
			if (this.PreRead(objectType, indexes))
			{
				parameterDef.ExprHostID = this.m_reader.ReadInt32();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return parameterDef;
		}

		// Token: 0x06006E6B RID: 28267 RVA: 0x001C9BBC File Offset: 0x001C7DBC
		private ParameterDataSource ReadParameterDataSource()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.ParameterDataSource;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ParameterDataSource parameterDataSource = new ParameterDataSource();
			if (this.PreRead(objectType, indexes))
			{
				parameterDataSource.DataSourceIndex = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				parameterDataSource.DataSetIndex = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				parameterDataSource.ValueFieldIndex = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				parameterDataSource.LabelFieldIndex = this.m_reader.ReadInt32();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return parameterDataSource;
		}

		// Token: 0x06006E6C RID: 28268 RVA: 0x001C9CA0 File Offset: 0x001C7EA0
		private ValidValue ReadValidValue()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.ValidValue;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ValidValue validValue = new ValidValue();
			if (this.PreRead(objectType, indexes))
			{
				validValue.Label = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				validValue.Value = this.ReadVariant();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return validValue;
		}

		// Token: 0x06006E6D RID: 28269 RVA: 0x001C9D4C File Offset: 0x001C7F4C
		private DataRegion ReadDataRegionInternals(ReportItem parent)
		{
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			if (ObjectType.List == this.m_reader.ObjectType)
			{
				return this.ReadListInternals(parent);
			}
			if (ObjectType.Matrix == this.m_reader.ObjectType)
			{
				return this.ReadMatrixInternals(parent);
			}
			if (ObjectType.Table == this.m_reader.ObjectType)
			{
				return this.ReadTableInternals(parent);
			}
			if (ObjectType.Chart == this.m_reader.ObjectType)
			{
				return this.ReadChartInternals(parent);
			}
			if (ObjectType.CustomReportItem == this.m_reader.ObjectType)
			{
				return this.ReadCustomReportItemInternals(parent);
			}
			IntermediateFormatReader.Assert(ObjectType.OWCChart == this.m_reader.ObjectType);
			return this.ReadOWCChartInternals(parent);
		}

		// Token: 0x06006E6E RID: 28270 RVA: 0x001C9E00 File Offset: 0x001C8000
		private void ReadDataRegionBase(DataRegion dataRegion)
		{
			IntermediateFormatReader.Assert(dataRegion != null);
			ObjectType objectType = ObjectType.DataRegion;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			this.ReadReportItemBase(dataRegion);
			if (this.PreRead(objectType, indexes))
			{
				dataRegion.DataSetName = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataRegion.NoRows = this.ReadExpressionInfo();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataRegion.PageBreakAtEnd = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataRegion.PageBreakAtStart = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataRegion.KeepTogether = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataRegion.RepeatSiblings = this.ReadIntList();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataRegion.Filters = this.ReadFilterList();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataRegion.Aggregates = this.ReadDataAggregateInfoList();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataRegion.PostSortAggregates = this.ReadDataAggregateInfoList();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataRegion.UserSortExpressions = this.ReadExpressionInfoList();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataRegion.DetailSortFiltersInScope = this.ReadInScopeSortFilterTable();
			}
			this.PostRead(objectType, indexes);
		}

		// Token: 0x06006E6F RID: 28271 RVA: 0x001C9F34 File Offset: 0x001C8134
		private DataRegion ReadDataRegionReference()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Reference == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.List == this.m_reader.ObjectType || ObjectType.Table == this.m_reader.ObjectType || ObjectType.Matrix == this.m_reader.ObjectType || ObjectType.Chart == this.m_reader.ObjectType || ObjectType.CustomReportItem == this.m_reader.ObjectType || ObjectType.OWCChart == this.m_reader.ObjectType);
			IDOwner definitionObject = this.GetDefinitionObject(this.m_reader.ReferenceValue);
			IntermediateFormatReader.Assert(definitionObject is DataRegion);
			return (DataRegion)definitionObject;
		}

		// Token: 0x06006E70 RID: 28272 RVA: 0x001CA000 File Offset: 0x001C8200
		private ReportHierarchyNode ReadReportHierarchyNode(ReportItem parent)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			if (ObjectType.TableGroup == this.m_reader.ObjectType)
			{
				return this.ReadTableGroupInternals(parent);
			}
			if (ObjectType.MatrixHeading == this.m_reader.ObjectType)
			{
				return this.ReadMatrixHeadingInternals(parent);
			}
			if (ObjectType.MultiChart == this.m_reader.ObjectType)
			{
				return this.ReadMultiChartInternals(parent);
			}
			if (ObjectType.ChartHeading == this.m_reader.ObjectType)
			{
				return this.ReadChartHeadingInternals(parent);
			}
			if (ObjectType.CustomReportItemHeading == this.m_reader.ObjectType)
			{
				return this.ReadCustomReportItemHeadingInternals(parent);
			}
			IntermediateFormatReader.Assert(ObjectType.ReportHierarchyNode == this.m_reader.ObjectType);
			ReportHierarchyNode reportHierarchyNode = new ReportHierarchyNode();
			this.ReadReportHierarchyNodeBase(reportHierarchyNode, parent);
			this.m_reader.ReadEndObject();
			return reportHierarchyNode;
		}

		// Token: 0x06006E71 RID: 28273 RVA: 0x001CA0EC File Offset: 0x001C82EC
		private void ReadReportHierarchyNodeBase(ReportHierarchyNode node, ReportItem parent)
		{
			IntermediateFormatReader.Assert(node != null);
			this.ReadIDOwnerBase(node);
			this.RegisterDefinitionObject(node);
			ObjectType objectType = ObjectType.ReportHierarchyNode;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			if (this.PreRead(objectType, indexes))
			{
				node.Grouping = this.ReadGrouping();
			}
			if (this.PreRead(objectType, indexes))
			{
				node.Sorting = this.ReadSorting();
			}
			if (this.PreRead(objectType, indexes))
			{
				node.InnerHierarchy = this.ReadReportHierarchyNode(parent);
			}
			if (this.PreRead(objectType, indexes))
			{
				node.DataRegionDef = this.ReadDataRegionReference();
			}
			this.PostRead(objectType, indexes);
		}

		// Token: 0x06006E72 RID: 28274 RVA: 0x001CA17C File Offset: 0x001C837C
		private Grouping ReadGrouping()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.Grouping;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			Grouping grouping = new Grouping(ConstructionPhase.Deserializing);
			if (this.PreRead(objectType, indexes))
			{
				grouping.Name = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				grouping.GroupExpressions = this.ReadExpressionInfoList();
			}
			if (this.PreRead(objectType, indexes))
			{
				grouping.GroupLabel = this.ReadExpressionInfo();
			}
			if (this.PreRead(objectType, indexes))
			{
				grouping.SortDirections = this.ReadBoolList();
			}
			if (this.PreRead(objectType, indexes))
			{
				grouping.PageBreakAtEnd = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				grouping.PageBreakAtStart = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				grouping.Custom = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				grouping.Aggregates = this.ReadDataAggregateInfoList();
			}
			if (this.PreRead(objectType, indexes))
			{
				grouping.GroupAndSort = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				grouping.Filters = this.ReadFilterList();
			}
			if (this.PreRead(objectType, indexes))
			{
				grouping.HideDuplicatesReportItemIDs = this.ReadReportItemIDList();
				if (grouping.HideDuplicatesReportItemIDs != null)
				{
					this.m_groupingsWithHideDuplicatesStack.Peek().Add(grouping);
				}
			}
			if (this.PreRead(objectType, indexes))
			{
				grouping.Parent = this.ReadExpressionInfoList();
			}
			if (this.PreRead(objectType, indexes))
			{
				grouping.RecursiveAggregates = this.ReadDataAggregateInfoList();
			}
			if (this.PreRead(objectType, indexes))
			{
				grouping.PostSortAggregates = this.ReadDataAggregateInfoList();
			}
			if (this.PreRead(objectType, indexes))
			{
				grouping.DataElementName = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				grouping.DataCollectionName = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				grouping.DataElementOutput = this.ReadDataElementOutputType(null);
			}
			if (this.PreRead(objectType, indexes))
			{
				grouping.CustomProperties = this.ReadDataValueList();
			}
			if (this.PreRead(objectType, indexes))
			{
				grouping.SaveGroupExprValues = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				grouping.UserSortExpressions = this.ReadExpressionInfoList();
			}
			if (this.PreRead(objectType, indexes))
			{
				grouping.NonDetailSortFiltersInScope = this.ReadInScopeSortFilterTable();
			}
			if (this.PreRead(objectType, indexes))
			{
				grouping.DetailSortFiltersInScope = this.ReadInScopeSortFilterTable();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return grouping;
		}

		// Token: 0x06006E73 RID: 28275 RVA: 0x001CA41C File Offset: 0x001C861C
		private InScopeSortFilterHashtable ReadInScopeSortFilterTable()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.InScopeSortFilterHashtable == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			InScopeSortFilterHashtable inScopeSortFilterHashtable = new InScopeSortFilterHashtable(num);
			for (int i = 0; i < num; i++)
			{
				int num2 = this.m_reader.ReadInt32();
				IntList intList = this.ReadIntList();
				inScopeSortFilterHashtable.Add(num2, intList);
			}
			this.m_reader.ReadEndObject();
			return inScopeSortFilterHashtable;
		}

		// Token: 0x06006E74 RID: 28276 RVA: 0x001CA4C0 File Offset: 0x001C86C0
		private IntList ReadReportItemIDList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.ReportItemList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			IntList intList = new IntList(num);
			for (int i = 0; i < num; i++)
			{
				intList.Add(this.ReadIDOwnerID(ObjectType.TextBox));
			}
			this.m_reader.ReadEndObject();
			return intList;
		}

		// Token: 0x06006E75 RID: 28277 RVA: 0x001CA554 File Offset: 0x001C8754
		private int ReadIDOwnerID(ObjectType objectType)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return -1;
			}
			IntermediateFormatReader.Assert(Token.Reference == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			return this.m_reader.ReferenceValue;
		}

		// Token: 0x06006E76 RID: 28278 RVA: 0x001CA5B4 File Offset: 0x001C87B4
		private Sorting ReadSorting()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.Sorting;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			Sorting sorting = new Sorting(ConstructionPhase.Deserializing);
			if (this.PreRead(objectType, indexes))
			{
				sorting.SortExpressions = this.ReadExpressionInfoList();
			}
			if (this.PreRead(objectType, indexes))
			{
				sorting.SortDirections = this.ReadBoolList();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return sorting;
		}

		// Token: 0x06006E77 RID: 28279 RVA: 0x001CA656 File Offset: 0x001C8856
		private TableGroup ReadTableGroup(ReportItem parent)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			return this.ReadTableGroupInternals(parent);
		}

		// Token: 0x06006E78 RID: 28280 RVA: 0x001CA680 File Offset: 0x001C8880
		private TableGroup ReadTableGroupInternals(ReportItem parent)
		{
			ObjectType objectType = ObjectType.TableGroup;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			TableGroup tableGroup = new TableGroup();
			this.ReadReportHierarchyNodeBase(tableGroup, parent);
			if (this.PreRead(objectType, indexes))
			{
				tableGroup.HeaderRows = this.ReadTableRowList(parent);
			}
			if (this.PreRead(objectType, indexes))
			{
				tableGroup.HeaderRepeatOnNewPage = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				tableGroup.FooterRows = this.ReadTableRowList(parent);
			}
			if (this.PreRead(objectType, indexes))
			{
				tableGroup.FooterRepeatOnNewPage = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				tableGroup.Visibility = this.ReadVisibility();
			}
			if (this.PreRead(objectType, indexes))
			{
				tableGroup.PropagatedPageBreakAtStart = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				tableGroup.PropagatedPageBreakAtEnd = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				tableGroup.RunningValues = this.ReadRunningValueInfoList();
			}
			if (this.PreRead(objectType, indexes))
			{
				tableGroup.HasExprHost = this.m_reader.ReadBoolean();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return tableGroup;
		}

		// Token: 0x06006E79 RID: 28281 RVA: 0x001CA7C0 File Offset: 0x001C89C0
		private TableGroup ReadTableGroupReference()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Reference == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.TableGroup == this.m_reader.ObjectType);
			IDOwner definitionObject = this.GetDefinitionObject(this.m_reader.ReferenceValue);
			IntermediateFormatReader.Assert(definitionObject is TableGroup);
			return (TableGroup)definitionObject;
		}

		// Token: 0x06006E7A RID: 28282 RVA: 0x001CA837 File Offset: 0x001C8A37
		private TableDetail ReadTableDetail(ReportItem parent)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			return this.ReadTableDetailInternals(parent);
		}

		// Token: 0x06006E7B RID: 28283 RVA: 0x001CA860 File Offset: 0x001C8A60
		private TableDetail ReadTableDetailInternals(ReportItem parent)
		{
			ObjectType objectType = ObjectType.TableDetail;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			TableDetail tableDetail = new TableDetail();
			this.ReadIDOwnerBase(tableDetail);
			this.RegisterDefinitionObject(tableDetail);
			if (this.PreRead(objectType, indexes))
			{
				tableDetail.DetailRows = this.ReadTableRowList(parent);
			}
			if (this.PreRead(objectType, indexes))
			{
				tableDetail.Sorting = this.ReadSorting();
			}
			if (this.PreRead(objectType, indexes))
			{
				tableDetail.Visibility = this.ReadVisibility();
			}
			if (this.PreRead(objectType, indexes))
			{
				tableDetail.RunningValues = this.ReadRunningValueInfoList();
			}
			if (this.PreRead(objectType, indexes))
			{
				tableDetail.HasExprHost = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				tableDetail.SimpleDetailRows = this.m_reader.ReadBoolean();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return tableDetail;
		}

		// Token: 0x06006E7C RID: 28284 RVA: 0x001CA958 File Offset: 0x001C8B58
		private void ReadPivotHeadingBase(PivotHeading pivotHeading, ReportItem parent)
		{
			IntermediateFormatReader.Assert(pivotHeading != null);
			ObjectType objectType = ObjectType.PivotHeading;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			this.ReadReportHierarchyNodeBase(pivotHeading, parent);
			if (this.PreRead(objectType, indexes))
			{
				pivotHeading.Visibility = this.ReadVisibility();
			}
			if (this.PreRead(objectType, indexes))
			{
				pivotHeading.Subtotal = this.ReadSubtotal(parent);
			}
			if (this.PreRead(objectType, indexes))
			{
				pivotHeading.Level = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				pivotHeading.IsColumn = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				pivotHeading.HasExprHost = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				pivotHeading.SubtotalSpan = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				pivotHeading.IDs = this.ReadIntList();
			}
			this.PostRead(objectType, indexes);
		}

		// Token: 0x06006E7D RID: 28285 RVA: 0x001CAA36 File Offset: 0x001C8C36
		private MatrixHeading ReadMatrixHeading(ReportItem parent)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			return this.ReadMatrixHeadingInternals(parent);
		}

		// Token: 0x06006E7E RID: 28286 RVA: 0x001CAA60 File Offset: 0x001C8C60
		private MatrixHeading ReadMatrixHeadingInternals(ReportItem parent)
		{
			ObjectType objectType = ObjectType.MatrixHeading;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			MatrixHeading matrixHeading = new MatrixHeading();
			this.ReadPivotHeadingBase(matrixHeading, parent);
			if (this.PreRead(objectType, indexes))
			{
				matrixHeading.Size = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				matrixHeading.SizeValue = this.m_reader.ReadDouble();
			}
			if (this.PreRead(objectType, indexes))
			{
				matrixHeading.ReportItems = this.ReadReportItemCollection(parent);
			}
			if (this.PreRead(objectType, indexes))
			{
				matrixHeading.OwcGroupExpression = this.m_reader.ReadBoolean();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return matrixHeading;
		}

		// Token: 0x06006E7F RID: 28287 RVA: 0x001CAB28 File Offset: 0x001C8D28
		private MatrixHeading ReadMatrixHeadingReference()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Reference == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.MatrixHeading == this.m_reader.ObjectType);
			IDOwner definitionObject = this.GetDefinitionObject(this.m_reader.ReferenceValue);
			IntermediateFormatReader.Assert(definitionObject is MatrixHeading);
			return (MatrixHeading)definitionObject;
		}

		// Token: 0x06006E80 RID: 28288 RVA: 0x001CABA0 File Offset: 0x001C8DA0
		private void ReadTablixHeadingBase(TablixHeading tablixHeading, ReportItem parent)
		{
			IntermediateFormatReader.Assert(tablixHeading != null);
			ObjectType objectType = ObjectType.TablixHeading;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			this.ReadReportHierarchyNodeBase(tablixHeading, null);
			if (this.PreRead(objectType, indexes))
			{
				tablixHeading.Subtotal = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				tablixHeading.IsColumn = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				tablixHeading.Level = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				tablixHeading.HasExprHost = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				tablixHeading.HeadingSpan = this.m_reader.ReadInt32();
			}
			this.PostRead(objectType, indexes);
		}

		// Token: 0x06006E81 RID: 28289 RVA: 0x001CAC59 File Offset: 0x001C8E59
		private CustomReportItemHeading ReadCustomReportItemHeading(ReportItem parent)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			return this.ReadCustomReportItemHeadingInternals(parent);
		}

		// Token: 0x06006E82 RID: 28290 RVA: 0x001CAC84 File Offset: 0x001C8E84
		private CustomReportItemHeading ReadCustomReportItemHeadingInternals(ReportItem parent)
		{
			ObjectType objectType = ObjectType.CustomReportItemHeading;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			CustomReportItemHeading customReportItemHeading = new CustomReportItemHeading();
			this.ReadTablixHeadingBase(customReportItemHeading, parent);
			if (this.PreRead(objectType, indexes))
			{
				customReportItemHeading.Static = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				customReportItemHeading.InnerHeadings = this.ReadCustomReportItemHeadingList(parent);
			}
			if (this.PreRead(objectType, indexes))
			{
				customReportItemHeading.CustomProperties = this.ReadDataValueList();
			}
			if (this.PreRead(objectType, indexes))
			{
				customReportItemHeading.ExprHostID = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				customReportItemHeading.RunningValues = this.ReadRunningValueInfoList();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return customReportItemHeading;
		}

		// Token: 0x06006E83 RID: 28291 RVA: 0x001CAD60 File Offset: 0x001C8F60
		private CustomReportItemHeading ReadCustomReportItemHeadingReference()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Reference == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.CustomReportItemHeading == this.m_reader.ObjectType);
			IDOwner definitionObject = this.GetDefinitionObject(this.m_reader.ReferenceValue);
			IntermediateFormatReader.Assert(definitionObject is CustomReportItemHeading);
			return (CustomReportItemHeading)definitionObject;
		}

		// Token: 0x06006E84 RID: 28292 RVA: 0x001CADDC File Offset: 0x001C8FDC
		private TableRow ReadTableRow(ReportItem parent)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.TableRow;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			TableRow tableRow = new TableRow();
			this.ReadIDOwnerBase(tableRow);
			this.RegisterDefinitionObject(tableRow);
			if (this.PreRead(objectType, indexes))
			{
				tableRow.ReportItems = this.ReadReportItemCollection(parent);
			}
			if (this.PreRead(objectType, indexes))
			{
				tableRow.IDs = this.ReadIntList();
			}
			if (this.PreRead(objectType, indexes))
			{
				tableRow.ColSpans = this.ReadIntList();
			}
			if (this.PreRead(objectType, indexes))
			{
				tableRow.Height = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				tableRow.HeightValue = this.m_reader.ReadDouble();
			}
			if (this.PreRead(objectType, indexes))
			{
				tableRow.Visibility = this.ReadVisibility();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return tableRow;
		}

		// Token: 0x06006E85 RID: 28293 RVA: 0x001CAEF0 File Offset: 0x001C90F0
		private Subtotal ReadSubtotal(ReportItem parent)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.Subtotal;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			Subtotal subtotal = new Subtotal();
			this.ReadIDOwnerBase(subtotal);
			if (this.PreRead(objectType, indexes))
			{
				subtotal.AutoDerived = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				subtotal.ReportItems = this.ReadReportItemCollection(parent);
			}
			if (this.PreRead(objectType, indexes))
			{
				subtotal.StyleClass = this.ReadStyle();
			}
			if (this.PreRead(objectType, indexes))
			{
				subtotal.Position = this.ReadPositionType();
			}
			if (this.PreRead(objectType, indexes))
			{
				subtotal.DataElementName = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				subtotal.DataElementOutput = this.ReadDataElementOutputType(null);
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return subtotal;
		}

		// Token: 0x06006E86 RID: 28294 RVA: 0x001CAFFC File Offset: 0x001C91FC
		private Subtotal.PositionType ReadPositionType()
		{
			int num = this.m_reader.ReadEnum();
			IntermediateFormatReader.Assert(Enum.IsDefined(typeof(Subtotal.PositionType), num));
			return (Subtotal.PositionType)num;
		}

		// Token: 0x06006E87 RID: 28295 RVA: 0x001CB030 File Offset: 0x001C9230
		private List ReadListInternals(ReportItem parent)
		{
			ObjectType objectType = ObjectType.List;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			List list = new List(parent);
			this.ReadDataRegionBase(list);
			if (this.PreRead(objectType, indexes))
			{
				list.HierarchyDef = this.ReadReportHierarchyNode(list);
			}
			if (this.PreRead(objectType, indexes))
			{
				list.ReportItems = this.ReadReportItemCollection(list);
			}
			if (this.PreRead(objectType, indexes))
			{
				list.FillPage = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				list.DataInstanceName = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				list.DataInstanceElementOutput = this.ReadDataElementOutputType(null);
			}
			if (this.PreRead(objectType, indexes))
			{
				list.IsListMostInner = this.m_reader.ReadBoolean();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return list;
		}

		// Token: 0x06006E88 RID: 28296 RVA: 0x001CB124 File Offset: 0x001C9324
		private void ReadPivotBase(Pivot pivot)
		{
			IntermediateFormatReader.Assert(pivot != null);
			ObjectType objectType = ObjectType.Pivot;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			this.ReadDataRegionBase(pivot);
			if (this.PreRead(objectType, indexes))
			{
				pivot.ColumnCount = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				pivot.RowCount = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				pivot.CellAggregates = this.ReadDataAggregateInfoList();
			}
			if (this.PreRead(objectType, indexes))
			{
				pivot.ProcessingInnerGrouping = this.ReadProcessingInnerGrouping();
			}
			if (this.PreRead(objectType, indexes))
			{
				pivot.RunningValues = this.ReadRunningValueInfoList();
			}
			if (this.PreRead(objectType, indexes))
			{
				pivot.CellPostSortAggregates = this.ReadDataAggregateInfoList();
			}
			if (this.PreRead(objectType, indexes))
			{
				pivot.CellDataElementOutput = this.ReadDataElementOutputType(null);
			}
			this.PostRead(objectType, indexes);
		}

		// Token: 0x06006E89 RID: 28297 RVA: 0x001CB1F8 File Offset: 0x001C93F8
		private void ReadTablixBase(Tablix tablix)
		{
			IntermediateFormatReader.Assert(tablix != null);
			ObjectType objectType = ObjectType.Tablix;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			this.ReadDataRegionBase(tablix);
			if (this.PreRead(objectType, indexes))
			{
				tablix.ColumnCount = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				tablix.RowCount = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				tablix.CellAggregates = this.ReadDataAggregateInfoList();
			}
			if (this.PreRead(objectType, indexes))
			{
				tablix.ProcessingInnerGrouping = this.ReadProcessingInnerGrouping();
			}
			if (this.PreRead(objectType, indexes))
			{
				tablix.RunningValues = this.ReadRunningValueInfoList();
			}
			if (this.PreRead(objectType, indexes))
			{
				tablix.CellPostSortAggregates = this.ReadDataAggregateInfoList();
			}
			this.PostRead(objectType, indexes);
		}

		// Token: 0x06006E8A RID: 28298 RVA: 0x001CB2B8 File Offset: 0x001C94B8
		private CustomReportItem ReadCustomReportItemInternals(ReportItem parent)
		{
			ObjectType objectType = ObjectType.CustomReportItem;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			CustomReportItem customReportItem = new CustomReportItem(parent);
			this.ReadTablixBase(customReportItem);
			if (this.PreRead(objectType, indexes))
			{
				customReportItem.Type = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				customReportItem.AltReportItem = this.ReadReportItemCollection(parent);
			}
			if (this.PreRead(objectType, indexes))
			{
				customReportItem.Columns = this.ReadCustomReportItemHeadingList(customReportItem);
			}
			if (this.PreRead(objectType, indexes))
			{
				customReportItem.Rows = this.ReadCustomReportItemHeadingList(customReportItem);
			}
			if (this.PreRead(objectType, indexes))
			{
				customReportItem.DataRowCells = this.ReadDataCellsList();
			}
			if (this.PreRead(objectType, indexes))
			{
				customReportItem.CellRunningValues = this.ReadRunningValueInfoList();
			}
			if (this.PreRead(objectType, indexes))
			{
				customReportItem.CellExprHostIDs = this.ReadIntList();
			}
			if (this.PreRead(objectType, indexes))
			{
				customReportItem.RenderReportItem = this.ReadReportItemCollection(parent);
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return customReportItem;
		}

		// Token: 0x06006E8B RID: 28299 RVA: 0x001CB3D2 File Offset: 0x001C95D2
		private ChartHeading ReadChartHeading(ReportItem parent)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			return this.ReadChartHeadingInternals(parent);
		}

		// Token: 0x06006E8C RID: 28300 RVA: 0x001CB3FC File Offset: 0x001C95FC
		private ChartHeading ReadChartHeadingInternals(ReportItem parent)
		{
			ObjectType objectType = ObjectType.ChartHeading;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ChartHeading chartHeading = new ChartHeading();
			this.ReadPivotHeadingBase(chartHeading, parent);
			if (this.PreRead(objectType, indexes))
			{
				chartHeading.Labels = this.ReadExpressionInfoList();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartHeading.RunningValues = this.ReadRunningValueInfoList();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartHeading.ChartGroupExpression = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartHeading.PlotTypesLine = this.ReadBoolList();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return chartHeading;
		}

		// Token: 0x06006E8D RID: 28301 RVA: 0x001CB4BC File Offset: 0x001C96BC
		private ChartHeading ReadChartHeadingReference()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Reference == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.ChartHeading == this.m_reader.ObjectType);
			IDOwner definitionObject = this.GetDefinitionObject(this.m_reader.ReferenceValue);
			IntermediateFormatReader.Assert(definitionObject is ChartHeading);
			return (ChartHeading)definitionObject;
		}

		// Token: 0x06006E8E RID: 28302 RVA: 0x001CB538 File Offset: 0x001C9738
		private ChartDataPointList ReadChartDataPointList()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.ChartDataPointList == this.m_reader.ObjectType);
			int num = this.m_reader.ReadArray();
			ChartDataPointList chartDataPointList = new ChartDataPointList(num);
			for (int i = 0; i < num; i++)
			{
				chartDataPointList.Add(this.ReadChartDataPoint());
			}
			this.m_reader.ReadEndObject();
			return chartDataPointList;
		}

		// Token: 0x06006E8F RID: 28303 RVA: 0x001CB5C8 File Offset: 0x001C97C8
		private ChartDataPoint ReadChartDataPoint()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.ChartDataPoint;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ChartDataPoint chartDataPoint = new ChartDataPoint();
			if (this.PreRead(objectType, indexes))
			{
				chartDataPoint.DataValues = this.ReadExpressionInfoList();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartDataPoint.DataLabel = this.ReadChartDataLabel();
			}
			if (this.PreRead(objectType, indexes))
			{
				if (this.m_intermediateFormatVersion.IsRS2005_WithMultipleActions)
				{
					chartDataPoint.Action = this.ReadAction();
				}
				else
				{
					ActionItem actionItem = this.ReadActionItem();
					if (actionItem != null)
					{
						chartDataPoint.Action = new Microsoft.ReportingServices.ReportProcessing.Action(actionItem, true);
					}
				}
			}
			if (this.PreRead(objectType, indexes))
			{
				chartDataPoint.StyleClass = this.ReadStyle();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartDataPoint.MarkerType = this.ReadMarkerType();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartDataPoint.MarkerSize = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartDataPoint.MarkerStyleClass = this.ReadStyle();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartDataPoint.DataElementName = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartDataPoint.DataElementOutput = this.ReadDataElementOutputType(null);
			}
			if (this.PreRead(objectType, indexes))
			{
				chartDataPoint.ExprHostID = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartDataPoint.CustomProperties = this.ReadDataValueList();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return chartDataPoint;
		}

		// Token: 0x06006E90 RID: 28304 RVA: 0x001CB768 File Offset: 0x001C9968
		private ChartDataLabel ReadChartDataLabel()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.ChartDataLabel;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ChartDataLabel chartDataLabel = new ChartDataLabel();
			if (this.PreRead(objectType, indexes))
			{
				chartDataLabel.Visible = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartDataLabel.Value = this.ReadExpressionInfo();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartDataLabel.StyleClass = this.ReadStyle();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartDataLabel.Position = this.ReadDataLabelPosition();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartDataLabel.Rotation = this.m_reader.ReadInt32();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return chartDataLabel;
		}

		// Token: 0x06006E91 RID: 28305 RVA: 0x001CB858 File Offset: 0x001C9A58
		private MultiChart ReadMultiChart(ReportItem parent)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			return this.ReadMultiChartInternals(parent);
		}

		// Token: 0x06006E92 RID: 28306 RVA: 0x001CB880 File Offset: 0x001C9A80
		private MultiChart ReadMultiChartInternals(ReportItem parent)
		{
			ObjectType objectType = ObjectType.MultiChart;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			MultiChart multiChart = new MultiChart();
			this.ReadReportHierarchyNodeBase(multiChart, parent);
			if (this.PreRead(objectType, indexes))
			{
				multiChart.Layout = this.ReadMultiChartLayout();
			}
			if (this.PreRead(objectType, indexes))
			{
				multiChart.MaxCount = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				multiChart.SyncScale = this.m_reader.ReadBoolean();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return multiChart;
		}

		// Token: 0x06006E93 RID: 28307 RVA: 0x001CB930 File Offset: 0x001C9B30
		private Axis ReadAxis()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.Axis;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			Axis axis = new Axis();
			if (this.PreRead(objectType, indexes))
			{
				axis.Visible = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				axis.StyleClass = this.ReadStyle();
			}
			if (this.PreRead(objectType, indexes))
			{
				axis.Title = this.ReadChartTitle();
			}
			if (this.PreRead(objectType, indexes))
			{
				axis.Margin = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				axis.MajorTickMarks = this.ReadTickMark();
			}
			if (this.PreRead(objectType, indexes))
			{
				axis.MinorTickMarks = this.ReadTickMark();
			}
			if (this.PreRead(objectType, indexes))
			{
				axis.MajorGridLines = this.ReadGridLines();
			}
			if (this.PreRead(objectType, indexes))
			{
				axis.MinorGridLines = this.ReadGridLines();
			}
			if (this.PreRead(objectType, indexes))
			{
				axis.MajorInterval = this.ReadExpressionInfo();
			}
			if (this.PreRead(objectType, indexes))
			{
				axis.MinorInterval = this.ReadExpressionInfo();
			}
			if (this.PreRead(objectType, indexes))
			{
				axis.Reverse = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				axis.CrossAt = this.ReadExpressionInfo();
			}
			if (this.PreRead(objectType, indexes))
			{
				axis.AutoCrossAt = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				axis.Interlaced = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				axis.Scalar = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				axis.Min = this.ReadExpressionInfo();
			}
			if (this.PreRead(objectType, indexes))
			{
				axis.Max = this.ReadExpressionInfo();
			}
			if (this.PreRead(objectType, indexes))
			{
				axis.AutoScaleMin = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				axis.AutoScaleMax = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				axis.LogScale = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				axis.CustomProperties = this.ReadDataValueList();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return axis;
		}

		// Token: 0x06006E94 RID: 28308 RVA: 0x001CBBA4 File Offset: 0x001C9DA4
		private ChartTitle ReadChartTitle()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.ChartTitle;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ChartTitle chartTitle = new ChartTitle();
			if (this.PreRead(objectType, indexes))
			{
				chartTitle.Caption = this.ReadExpressionInfo();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartTitle.StyleClass = this.ReadStyle();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartTitle.Position = this.ReadChartTitlePosition();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return chartTitle;
		}

		// Token: 0x06006E95 RID: 28309 RVA: 0x001CBC60 File Offset: 0x001C9E60
		private Legend ReadLegend()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.Legend;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			Legend legend = new Legend();
			if (this.PreRead(objectType, indexes))
			{
				legend.Visible = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				legend.StyleClass = this.ReadStyle();
			}
			if (this.PreRead(objectType, indexes))
			{
				legend.Position = this.ReadLegendPosition();
			}
			if (this.PreRead(objectType, indexes))
			{
				legend.Layout = this.ReadLegendLayout();
			}
			if (this.PreRead(objectType, indexes))
			{
				legend.InsidePlotArea = this.m_reader.ReadBoolean();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return legend;
		}

		// Token: 0x06006E96 RID: 28310 RVA: 0x001CBD50 File Offset: 0x001C9F50
		private GridLines ReadGridLines()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.GridLines;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			GridLines gridLines = new GridLines();
			if (this.PreRead(objectType, indexes))
			{
				gridLines.ShowGridLines = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				gridLines.StyleClass = this.ReadStyle();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return gridLines;
		}

		// Token: 0x06006E97 RID: 28311 RVA: 0x001CBDFC File Offset: 0x001C9FFC
		private ThreeDProperties ReadThreeDProperties()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.ThreeDProperties;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ThreeDProperties threeDProperties = new ThreeDProperties();
			if (this.PreRead(objectType, indexes))
			{
				threeDProperties.Enabled = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				threeDProperties.PerspectiveProjectionMode = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				threeDProperties.Rotation = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				threeDProperties.Inclination = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				threeDProperties.Perspective = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				threeDProperties.HeightRatio = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				threeDProperties.DepthRatio = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				threeDProperties.Shading = this.ReadShading();
			}
			if (this.PreRead(objectType, indexes))
			{
				threeDProperties.GapDepth = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				threeDProperties.WallThickness = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				threeDProperties.DrawingStyleCube = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				threeDProperties.Clustered = this.m_reader.ReadBoolean();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return threeDProperties;
		}

		// Token: 0x06006E98 RID: 28312 RVA: 0x001CBFB4 File Offset: 0x001CA1B4
		private PlotArea ReadPlotArea()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.PlotArea;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			PlotArea plotArea = new PlotArea();
			if (this.PreRead(objectType, indexes))
			{
				plotArea.Origin = this.ReadPlotAreaOrigin();
			}
			if (this.PreRead(objectType, indexes))
			{
				plotArea.StyleClass = this.ReadStyle();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return plotArea;
		}

		// Token: 0x06006E99 RID: 28313 RVA: 0x001CC058 File Offset: 0x001CA258
		private Chart ReadChartInternals(ReportItem parent)
		{
			ObjectType objectType = ObjectType.Chart;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			Chart chart = new Chart(parent);
			this.ReadPivotBase(chart);
			if (this.PreRead(objectType, indexes))
			{
				chart.Columns = this.ReadChartHeading(chart);
			}
			if (this.PreRead(objectType, indexes))
			{
				chart.Rows = this.ReadChartHeading(chart);
			}
			if (this.PreRead(objectType, indexes))
			{
				chart.ChartDataPoints = this.ReadChartDataPointList();
			}
			if (this.PreRead(objectType, indexes))
			{
				chart.CellRunningValues = this.ReadRunningValueInfoList();
			}
			if (this.PreRead(objectType, indexes))
			{
				chart.MultiChart = this.ReadMultiChart(chart);
			}
			if (this.PreRead(objectType, indexes))
			{
				chart.Legend = this.ReadLegend();
			}
			if (this.PreRead(objectType, indexes))
			{
				chart.CategoryAxis = this.ReadAxis();
			}
			if (this.PreRead(objectType, indexes))
			{
				chart.ValueAxis = this.ReadAxis();
			}
			if (this.PreRead(objectType, indexes))
			{
				chart.StaticColumns = this.ReadChartHeadingReference();
			}
			if (this.PreRead(objectType, indexes))
			{
				chart.StaticRows = this.ReadChartHeadingReference();
			}
			if (this.PreRead(objectType, indexes))
			{
				chart.Type = this.ReadChartType();
			}
			if (this.PreRead(objectType, indexes))
			{
				chart.SubType = this.ReadChartSubType();
			}
			if (this.PreRead(objectType, indexes))
			{
				chart.Palette = this.ReadChartPalette();
			}
			if (this.PreRead(objectType, indexes))
			{
				chart.Title = this.ReadChartTitle();
			}
			if (this.PreRead(objectType, indexes))
			{
				chart.PointWidth = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				chart.ThreeDProperties = this.ReadThreeDProperties();
			}
			if (this.PreRead(objectType, indexes))
			{
				chart.PlotArea = this.ReadPlotArea();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return chart;
		}

		// Token: 0x06006E9A RID: 28314 RVA: 0x001CC238 File Offset: 0x001CA438
		private ChartDataLabel.Positions ReadDataLabelPosition()
		{
			int num = this.m_reader.ReadEnum();
			IntermediateFormatReader.Assert(Enum.IsDefined(typeof(ChartDataLabel.Positions), num));
			return (ChartDataLabel.Positions)num;
		}

		// Token: 0x06006E9B RID: 28315 RVA: 0x001CC26C File Offset: 0x001CA46C
		private ChartDataPoint.MarkerTypes ReadMarkerType()
		{
			int num = this.m_reader.ReadEnum();
			IntermediateFormatReader.Assert(Enum.IsDefined(typeof(ChartDataPoint.MarkerTypes), num));
			return (ChartDataPoint.MarkerTypes)num;
		}

		// Token: 0x06006E9C RID: 28316 RVA: 0x001CC2A0 File Offset: 0x001CA4A0
		private MultiChart.Layouts ReadMultiChartLayout()
		{
			int num = this.m_reader.ReadEnum();
			IntermediateFormatReader.Assert(Enum.IsDefined(typeof(MultiChart.Layouts), num));
			return (MultiChart.Layouts)num;
		}

		// Token: 0x06006E9D RID: 28317 RVA: 0x001CC2D4 File Offset: 0x001CA4D4
		private Axis.TickMarks ReadTickMark()
		{
			int num = this.m_reader.ReadEnum();
			IntermediateFormatReader.Assert(Enum.IsDefined(typeof(Axis.TickMarks), num));
			return (Axis.TickMarks)num;
		}

		// Token: 0x06006E9E RID: 28318 RVA: 0x001CC308 File Offset: 0x001CA508
		private ThreeDProperties.ShadingTypes ReadShading()
		{
			int num = this.m_reader.ReadEnum();
			IntermediateFormatReader.Assert(Enum.IsDefined(typeof(ThreeDProperties.ShadingTypes), num));
			return (ThreeDProperties.ShadingTypes)num;
		}

		// Token: 0x06006E9F RID: 28319 RVA: 0x001CC33C File Offset: 0x001CA53C
		private PlotArea.Origins ReadPlotAreaOrigin()
		{
			int num = this.m_reader.ReadEnum();
			IntermediateFormatReader.Assert(Enum.IsDefined(typeof(PlotArea.Origins), num));
			return (PlotArea.Origins)num;
		}

		// Token: 0x06006EA0 RID: 28320 RVA: 0x001CC370 File Offset: 0x001CA570
		private Legend.LegendLayout ReadLegendLayout()
		{
			int num = this.m_reader.ReadEnum();
			IntermediateFormatReader.Assert(Enum.IsDefined(typeof(Legend.LegendLayout), num));
			return (Legend.LegendLayout)num;
		}

		// Token: 0x06006EA1 RID: 28321 RVA: 0x001CC3A4 File Offset: 0x001CA5A4
		private Legend.Positions ReadLegendPosition()
		{
			int num = this.m_reader.ReadEnum();
			IntermediateFormatReader.Assert(Enum.IsDefined(typeof(Legend.Positions), num));
			return (Legend.Positions)num;
		}

		// Token: 0x06006EA2 RID: 28322 RVA: 0x001CC3D8 File Offset: 0x001CA5D8
		private ChartTitle.Positions ReadChartTitlePosition()
		{
			int num = this.m_reader.ReadEnum();
			IntermediateFormatReader.Assert(Enum.IsDefined(typeof(ChartTitle.Positions), num));
			return (ChartTitle.Positions)num;
		}

		// Token: 0x06006EA3 RID: 28323 RVA: 0x001CC40C File Offset: 0x001CA60C
		private Chart.ChartTypes ReadChartType()
		{
			int num = this.m_reader.ReadEnum();
			IntermediateFormatReader.Assert(Enum.IsDefined(typeof(Chart.ChartTypes), num));
			return (Chart.ChartTypes)num;
		}

		// Token: 0x06006EA4 RID: 28324 RVA: 0x001CC440 File Offset: 0x001CA640
		private Chart.ChartSubTypes ReadChartSubType()
		{
			int num = this.m_reader.ReadEnum();
			IntermediateFormatReader.Assert(Enum.IsDefined(typeof(Chart.ChartSubTypes), num));
			return (Chart.ChartSubTypes)num;
		}

		// Token: 0x06006EA5 RID: 28325 RVA: 0x001CC474 File Offset: 0x001CA674
		private Chart.ChartPalette ReadChartPalette()
		{
			int num = this.m_reader.ReadEnum();
			IntermediateFormatReader.Assert(Enum.IsDefined(typeof(Chart.ChartPalette), num));
			return (Chart.ChartPalette)num;
		}

		// Token: 0x06006EA6 RID: 28326 RVA: 0x001CC4A8 File Offset: 0x001CA6A8
		private Matrix ReadMatrixInternals(ReportItem parent)
		{
			ObjectType objectType = ObjectType.Matrix;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			Matrix matrix = new Matrix(parent);
			this.ReadPivotBase(matrix);
			if (this.PreRead(objectType, indexes))
			{
				matrix.CornerReportItems = this.ReadReportItemCollection(matrix);
			}
			if (this.PreRead(objectType, indexes))
			{
				matrix.Columns = this.ReadMatrixHeading(matrix);
			}
			if (this.PreRead(objectType, indexes))
			{
				matrix.Rows = this.ReadMatrixHeading(matrix);
			}
			if (this.PreRead(objectType, indexes))
			{
				matrix.CellReportItems = this.ReadReportItemCollection(matrix);
			}
			if (this.PreRead(objectType, indexes))
			{
				matrix.CellIDs = this.ReadIntList();
			}
			if (this.PreRead(objectType, indexes))
			{
				matrix.PropagatedPageBreakAtStart = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				matrix.PropagatedPageBreakAtEnd = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				matrix.InnerRowLevelWithPageBreak = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				matrix.MatrixRows = this.ReadMatrixRowList();
			}
			if (this.PreRead(objectType, indexes))
			{
				matrix.MatrixColumns = this.ReadMatrixColumnList();
			}
			if (this.PreRead(objectType, indexes))
			{
				matrix.GroupsBeforeRowHeaders = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				matrix.LayoutDirection = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				matrix.StaticColumns = this.ReadMatrixHeadingReference();
			}
			if (this.PreRead(objectType, indexes))
			{
				matrix.StaticRows = this.ReadMatrixHeadingReference();
			}
			if (this.PreRead(objectType, indexes))
			{
				matrix.UseOWC = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				matrix.OwcCellNames = this.ReadStringList();
			}
			if (this.PreRead(objectType, indexes))
			{
				matrix.CellDataElementName = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				matrix.ColumnGroupingFixedHeader = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				matrix.RowGroupingFixedHeader = this.m_reader.ReadBoolean();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return matrix;
		}

		// Token: 0x06006EA7 RID: 28327 RVA: 0x001CC6DC File Offset: 0x001CA8DC
		private Pivot.ProcessingInnerGroupings ReadProcessingInnerGrouping()
		{
			int num = this.m_reader.ReadEnum();
			IntermediateFormatReader.Assert(Enum.IsDefined(typeof(Pivot.ProcessingInnerGroupings), num));
			return (Pivot.ProcessingInnerGroupings)num;
		}

		// Token: 0x06006EA8 RID: 28328 RVA: 0x001CC710 File Offset: 0x001CA910
		private MatrixRow ReadMatrixRow()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.MatrixRow;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			MatrixRow matrixRow = new MatrixRow();
			if (this.PreRead(objectType, indexes))
			{
				matrixRow.Height = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				matrixRow.HeightValue = this.m_reader.ReadDouble();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return matrixRow;
		}

		// Token: 0x06006EA9 RID: 28329 RVA: 0x001CC7BC File Offset: 0x001CA9BC
		private MatrixColumn ReadMatrixColumn()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.MatrixColumn;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			MatrixColumn matrixColumn = new MatrixColumn();
			if (this.PreRead(objectType, indexes))
			{
				matrixColumn.Width = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				matrixColumn.WidthValue = this.m_reader.ReadDouble();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return matrixColumn;
		}

		// Token: 0x06006EAA RID: 28330 RVA: 0x001CC868 File Offset: 0x001CAA68
		private Table ReadTableInternals(ReportItem parent)
		{
			ObjectType objectType = ObjectType.Table;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			Table table = new Table(parent);
			this.ReadDataRegionBase(table);
			if (this.PreRead(objectType, indexes))
			{
				table.TableColumns = this.ReadTableColumnList();
			}
			if (this.PreRead(objectType, indexes))
			{
				table.HeaderRows = this.ReadTableRowList(table);
			}
			if (this.PreRead(objectType, indexes))
			{
				table.HeaderRepeatOnNewPage = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				table.TableGroups = this.ReadTableGroup(table);
			}
			if (this.PreRead(objectType, indexes))
			{
				table.TableDetail = this.ReadTableDetail(table);
			}
			if (this.PreRead(objectType, indexes) && this.m_intermediateFormatVersion.IsRS2005_WithTableDetailFix)
			{
				table.DetailGroup = this.ReadTableGroupReference();
			}
			if (this.PreRead(objectType, indexes))
			{
				table.FooterRows = this.ReadTableRowList(table);
			}
			if (this.PreRead(objectType, indexes))
			{
				table.FooterRepeatOnNewPage = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				table.PropagatedPageBreakAtStart = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				table.GroupBreakAtStart = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				table.PropagatedPageBreakAtEnd = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				table.GroupBreakAtEnd = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				table.FillPage = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				table.UseOWC = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				table.OWCNonSharedStyles = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				table.RunningValues = this.ReadRunningValueInfoList();
			}
			if (this.PreRead(objectType, indexes))
			{
				table.DetailDataElementName = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				table.DetailDataCollectionName = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				table.DetailDataElementOutput = this.ReadDataElementOutputType(null);
			}
			if (this.PreRead(objectType, indexes))
			{
				table.FixedHeader = this.m_reader.ReadBoolean();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return table;
		}

		// Token: 0x06006EAB RID: 28331 RVA: 0x001CCACC File Offset: 0x001CACCC
		private TableColumn ReadTableColumn()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.TableColumn;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			TableColumn tableColumn = new TableColumn();
			if (this.PreRead(objectType, indexes))
			{
				tableColumn.Width = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				tableColumn.WidthValue = this.m_reader.ReadDouble();
			}
			if (this.PreRead(objectType, indexes))
			{
				tableColumn.Visibility = this.ReadVisibility();
			}
			if (this.PreRead(objectType, indexes))
			{
				tableColumn.FixedHeader = this.m_reader.ReadBoolean();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return tableColumn;
		}

		// Token: 0x06006EAC RID: 28332 RVA: 0x001CCBA8 File Offset: 0x001CADA8
		private OWCChart ReadOWCChartInternals(ReportItem parent)
		{
			ObjectType objectType = ObjectType.OWCChart;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			OWCChart owcchart = new OWCChart(parent);
			this.ReadDataRegionBase(owcchart);
			if (this.PreRead(objectType, indexes))
			{
				owcchart.ChartData = this.ReadChartColumnList();
			}
			if (this.PreRead(objectType, indexes))
			{
				owcchart.ChartDefinition = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				owcchart.DetailRunningValues = this.ReadRunningValueInfoList();
			}
			if (this.PreRead(objectType, indexes))
			{
				owcchart.RunningValues = this.ReadRunningValueInfoList();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return owcchart;
		}

		// Token: 0x06006EAD RID: 28333 RVA: 0x001CCC64 File Offset: 0x001CAE64
		private ChartColumn ReadChartColumn()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.ChartColumn;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ChartColumn chartColumn = new ChartColumn();
			if (this.PreRead(objectType, indexes))
			{
				chartColumn.Name = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartColumn.Value = this.ReadExpressionInfo();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return chartColumn;
		}

		// Token: 0x06006EAE RID: 28334 RVA: 0x001CCD0C File Offset: 0x001CAF0C
		private DataValue ReadDataValue()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.DataValue;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			DataValue dataValue = new DataValue();
			if (this.PreRead(objectType, indexes))
			{
				dataValue.Name = this.ReadExpressionInfo();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataValue.Value = this.ReadExpressionInfo();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataValue.ExprHostID = this.m_reader.ReadInt32();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return dataValue;
		}

		// Token: 0x06006EAF RID: 28335 RVA: 0x001CCDCC File Offset: 0x001CAFCC
		private ParameterInfo ReadParameterInfo()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.ParameterInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ParameterInfo parameterInfo = new ParameterInfo();
			this.ReadParameterBase(parameterInfo);
			this.RegisterParameterInfo(parameterInfo);
			if (this.PreRead(objectType, indexes))
			{
				parameterInfo.IsUserSupplied = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				parameterInfo.Values = this.ReadVariants();
			}
			if (this.PreRead(objectType, indexes))
			{
				parameterInfo.DynamicValidValues = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				parameterInfo.DynamicDefaultValue = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				parameterInfo.DependencyList = this.ReadParameterInfoRefCollection();
			}
			if (this.PreRead(objectType, indexes))
			{
				parameterInfo.ValidValues = this.ReadValidValueList();
			}
			if (this.PreRead(objectType, indexes))
			{
				parameterInfo.Labels = this.ReadStrings();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return parameterInfo;
		}

		// Token: 0x06006EB0 RID: 28336 RVA: 0x001CCEFC File Offset: 0x001CB0FC
		private ProcessingMessage ReadProcessingMessage()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.ProcessingMessage;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ProcessingMessage processingMessage = new ProcessingMessage();
			if (this.PreRead(objectType, indexes))
			{
				processingMessage.Code = this.ReadProcessingErrorCode();
			}
			if (this.PreRead(objectType, indexes))
			{
				processingMessage.Severity = this.ReadProcessingErrorSeverity();
			}
			if (this.PreRead(objectType, indexes))
			{
				processingMessage.ObjectType = this.ReadProcessingErrorObjectType();
			}
			if (this.PreRead(objectType, indexes))
			{
				processingMessage.ObjectName = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				processingMessage.PropertyName = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				processingMessage.Message = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				processingMessage.ProcessingMessages = this.ReadProcessingMessageList();
			}
			if (this.PreRead(objectType, indexes))
			{
				processingMessage.CommonCode = this.ReadCommonErrorCode();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return processingMessage;
		}

		// Token: 0x06006EB1 RID: 28337 RVA: 0x001CD034 File Offset: 0x001CB234
		private DataValueInstance ReadDataValueInstance()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.DataValueInstance;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			DataValueInstance dataValueInstance = new DataValueInstance();
			if (this.PreRead(objectType, indexes))
			{
				dataValueInstance.Name = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				dataValueInstance.Value = this.ReadVariant();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return dataValueInstance;
		}

		// Token: 0x06006EB2 RID: 28338 RVA: 0x001CD0E0 File Offset: 0x001CB2E0
		private ProcessingErrorCode ReadProcessingErrorCode()
		{
			int num = this.m_reader.ReadEnum();
			IntermediateFormatReader.Assert(Enum.IsDefined(typeof(ProcessingErrorCode), num));
			return (ProcessingErrorCode)num;
		}

		// Token: 0x06006EB3 RID: 28339 RVA: 0x001CD114 File Offset: 0x001CB314
		private ErrorCode ReadCommonErrorCode()
		{
			int num = this.m_reader.ReadEnum();
			IntermediateFormatReader.Assert(Enum.IsDefined(typeof(ErrorCode), num));
			return (ErrorCode)num;
		}

		// Token: 0x06006EB4 RID: 28340 RVA: 0x001CD148 File Offset: 0x001CB348
		private Severity ReadProcessingErrorSeverity()
		{
			int num = this.m_reader.ReadEnum();
			IntermediateFormatReader.Assert(Enum.IsDefined(typeof(Severity), num));
			return (Severity)num;
		}

		// Token: 0x06006EB5 RID: 28341 RVA: 0x001CD17C File Offset: 0x001CB37C
		private ObjectType ReadProcessingErrorObjectType()
		{
			int num = this.m_reader.ReadEnum();
			IntermediateFormatReader.Assert(Enum.IsDefined(typeof(ObjectType), num));
			return (ObjectType)num;
		}

		// Token: 0x06006EB6 RID: 28342 RVA: 0x001CD1B0 File Offset: 0x001CB3B0
		private DataType ReadDataType()
		{
			int num = this.m_reader.ReadEnum();
			IntermediateFormatReader.Assert(Enum.IsDefined(typeof(DataType), num));
			return (DataType)num;
		}

		// Token: 0x06006EB7 RID: 28343 RVA: 0x001CD1E4 File Offset: 0x001CB3E4
		private BookmarkInformation ReadBookmarkInformation()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.BookmarkInformation;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			BookmarkInformation bookmarkInformation = new BookmarkInformation();
			if (this.PreRead(objectType, indexes))
			{
				bookmarkInformation.Id = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				bookmarkInformation.Page = this.m_reader.ReadInt32();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return bookmarkInformation;
		}

		// Token: 0x06006EB8 RID: 28344 RVA: 0x001CD294 File Offset: 0x001CB494
		private DrillthroughInformation ReadDrillthroughInformation(bool hasTokensIDs)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.DrillthroughInformation;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			DrillthroughInformation drillthroughInformation = new DrillthroughInformation();
			if (this.PreRead(objectType, indexes))
			{
				drillthroughInformation.ReportName = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				drillthroughInformation.ReportParameters = this.ReadDrillthroughParameters();
			}
			if (hasTokensIDs && this.PreRead(objectType, indexes))
			{
				drillthroughInformation.DataSetTokenIDs = this.ReadIntList();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return drillthroughInformation;
		}

		// Token: 0x06006EB9 RID: 28345 RVA: 0x001CD358 File Offset: 0x001CB558
		private SenderInformation ReadSenderInformation()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.SenderInformation;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			SenderInformation senderInformation = new SenderInformation();
			if (this.PreRead(objectType, indexes))
			{
				senderInformation.StartHidden = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				senderInformation.ReceiverUniqueNames = this.ReadIntList();
			}
			if (this.PreRead(objectType, indexes))
			{
				senderInformation.ContainerUniqueNames = this.m_reader.ReadInt32s();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return senderInformation;
		}

		// Token: 0x06006EBA RID: 28346 RVA: 0x001CD41C File Offset: 0x001CB61C
		private ReceiverInformation ReadReceiverInformation()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.ReceiverInformation;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ReceiverInformation receiverInformation = new ReceiverInformation();
			if (this.PreRead(objectType, indexes))
			{
				receiverInformation.StartHidden = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				receiverInformation.SenderUniqueName = this.m_reader.ReadInt32();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return receiverInformation;
		}

		// Token: 0x06006EBB RID: 28347 RVA: 0x001CD4C8 File Offset: 0x001CB6C8
		private SortFilterEventInfo ReadSortFilterEventInfo(bool getDefinition)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.SortFilterEventInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			SortFilterEventInfo sortFilterEventInfo = new SortFilterEventInfo();
			if (this.PreRead(objectType, indexes))
			{
				sortFilterEventInfo.EventSource = (TextBox)this.ReadReportItemReference(getDefinition);
			}
			if (this.PreRead(objectType, indexes))
			{
				sortFilterEventInfo.EventSourceScopeInfo = this.ReadVariantLists(true);
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return sortFilterEventInfo;
		}

		// Token: 0x06006EBC RID: 28348 RVA: 0x001CD574 File Offset: 0x001CB774
		private void ReadInfoBaseBase(InfoBase infoBase)
		{
			IntermediateFormatReader.Assert(infoBase != null);
			ObjectType objectType = ObjectType.InfoBase;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			this.PostRead(objectType, indexes);
		}

		// Token: 0x06006EBD RID: 28349 RVA: 0x001CD59B File Offset: 0x001CB79B
		private OffsetInfo ReadSimpleOffsetInfo()
		{
			return new OffsetInfo
			{
				Offset = this.m_reader.ReadInt64()
			};
		}

		// Token: 0x06006EBE RID: 28350 RVA: 0x001CD5B4 File Offset: 0x001CB7B4
		private OffsetInfo ReadOffsetInfo()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.OffsetInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			OffsetInfo offsetInfo = new OffsetInfo();
			this.ReadInfoBaseBase(offsetInfo);
			if (this.PreRead(objectType, indexes))
			{
				offsetInfo.Offset = this.m_reader.ReadInt64();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return offsetInfo;
		}

		// Token: 0x06006EBF RID: 28351 RVA: 0x001CD64C File Offset: 0x001CB84C
		private void ReadInstanceInfoBase(InstanceInfo instanceInfo)
		{
			IntermediateFormatReader.Assert(instanceInfo != null);
			ObjectType objectType = ObjectType.InstanceInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			this.ReadInfoBaseBase(instanceInfo);
			this.PostRead(objectType, indexes);
		}

		// Token: 0x06006EC0 RID: 28352 RVA: 0x001CD67C File Offset: 0x001CB87C
		private void ReadReportItemInstanceInfoBase(ReportItemInstanceInfo instanceInfo)
		{
			IntermediateFormatReader.Assert(instanceInfo != null);
			ObjectType objectType = ObjectType.ReportItemInstanceInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			this.ReadInstanceInfoBase(instanceInfo);
			bool flag = false;
			ReportItem reportItemDef = instanceInfo.ReportItemDef;
			if (this.m_intermediateFormatVersion.IsRS2000_WithUnusedFieldsOptimization)
			{
				flag = true;
			}
			if ((!flag || (reportItemDef.StyleClass != null && reportItemDef.StyleClass.ExpressionList != null)) && this.PreRead(objectType, indexes))
			{
				instanceInfo.StyleAttributeValues = this.ReadVariants();
			}
			if ((!flag || reportItemDef.Visibility != null) && this.PreRead(objectType, indexes))
			{
				instanceInfo.StartHidden = this.m_reader.ReadBoolean();
			}
			if ((!flag || reportItemDef.Label != null) && this.PreRead(objectType, indexes))
			{
				instanceInfo.Label = this.m_reader.ReadString();
			}
			if ((!flag || reportItemDef.Bookmark != null) && this.PreRead(objectType, indexes))
			{
				instanceInfo.Bookmark = this.m_reader.ReadString();
			}
			if ((!flag || reportItemDef.ToolTip != null) && this.PreRead(objectType, indexes))
			{
				instanceInfo.ToolTip = this.m_reader.ReadString();
			}
			if ((!flag || reportItemDef.CustomProperties != null) && this.PreRead(objectType, indexes))
			{
				instanceInfo.CustomPropertyInstances = this.ReadDataValueInstanceList();
			}
			this.PostRead(objectType, indexes);
		}

		// Token: 0x06006EC1 RID: 28353 RVA: 0x001CD7AC File Offset: 0x001CB9AC
		private NonComputedUniqueNames ReadNonComputedUniqueNames()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.NonComputedUniqueNames;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			NonComputedUniqueNames nonComputedUniqueNames = new NonComputedUniqueNames();
			if (this.PreRead(objectType, indexes))
			{
				nonComputedUniqueNames.UniqueName = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				nonComputedUniqueNames.ChildrenUniqueNames = this.ReadNonComputedUniqueNamess();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return nonComputedUniqueNames;
		}

		// Token: 0x06006EC2 RID: 28354 RVA: 0x001CD854 File Offset: 0x001CBA54
		private void ReadInstanceInfoOwnerBase(InstanceInfoOwner owner)
		{
			IntermediateFormatReader.Assert(owner != null);
			ObjectType objectType = ObjectType.InstanceInfoOwner;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			if (this.PreRead(objectType, indexes))
			{
				if (this.m_intermediateFormatVersion.IsRS2000_WithOtherPageChunkSplit)
				{
					owner.OffsetInfo = this.ReadSimpleOffsetInfo();
				}
				else
				{
					owner.OffsetInfo = this.ReadOffsetInfo();
				}
			}
			this.PostRead(objectType, indexes);
		}

		// Token: 0x06006EC3 RID: 28355 RVA: 0x001CD8AC File Offset: 0x001CBAAC
		private ReportItemInstance ReadReportItemInstance(ReportItem reportItemDef)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			if (ObjectType.LineInstance == this.m_reader.ObjectType)
			{
				return this.ReadLineInstanceInternals(reportItemDef);
			}
			if (ObjectType.RectangleInstance == this.m_reader.ObjectType)
			{
				return this.ReadRectangleInstanceInternals(reportItemDef);
			}
			if (ObjectType.ImageInstance == this.m_reader.ObjectType)
			{
				return this.ReadImageInstanceInternals(reportItemDef);
			}
			if (ObjectType.CheckBoxInstance == this.m_reader.ObjectType)
			{
				return this.ReadCheckBoxInstanceInternals(reportItemDef);
			}
			if (ObjectType.TextBoxInstance == this.m_reader.ObjectType)
			{
				return this.ReadTextBoxInstanceInternals(reportItemDef);
			}
			if (ObjectType.SubReportInstance == this.m_reader.ObjectType)
			{
				return this.ReadSubReportInstanceInternals(reportItemDef);
			}
			if (ObjectType.ActiveXControlInstance == this.m_reader.ObjectType)
			{
				return this.ReadActiveXControlInstanceInternals(reportItemDef);
			}
			if (ObjectType.ListInstance == this.m_reader.ObjectType)
			{
				return this.ReadListInstanceInternals(reportItemDef);
			}
			if (ObjectType.MatrixInstance == this.m_reader.ObjectType)
			{
				return this.ReadMatrixInstanceInternals(reportItemDef);
			}
			if (ObjectType.TableInstance == this.m_reader.ObjectType)
			{
				return this.ReadTableInstanceInternals(reportItemDef);
			}
			if (ObjectType.ChartInstance == this.m_reader.ObjectType)
			{
				return this.ReadChartInstanceInternals(reportItemDef);
			}
			if (ObjectType.CustomReportItemInstance == this.m_reader.ObjectType)
			{
				IntermediateFormatReader.Assert(reportItemDef is CustomReportItem);
				return this.ReadCustomReportItemInstanceInternals(reportItemDef as CustomReportItem);
			}
			IntermediateFormatReader.Assert(ObjectType.OWCChartInstance == this.m_reader.ObjectType);
			return this.ReadOWCChartInstanceInternals(reportItemDef);
		}

		// Token: 0x06006EC4 RID: 28356 RVA: 0x001CDA33 File Offset: 0x001CBC33
		private void ReadReportItemInstanceBase(ReportItemInstance reportItemInstance, ReportItem reportItemDef)
		{
			Global.Tracer.Assert(reportItemDef != null, "(null != reportItemDef)");
			this.ReadReportItemInstanceBase(reportItemInstance, ref reportItemDef);
		}

		// Token: 0x06006EC5 RID: 28357 RVA: 0x001CDA54 File Offset: 0x001CBC54
		private void ReadReportItemInstanceBase(ReportItemInstance reportItemInstance, ref ReportItem reportItemDef)
		{
			IntermediateFormatReader.Assert(reportItemInstance != null);
			ObjectType objectType = ObjectType.ReportItemInstance;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			this.ReadInstanceInfoOwnerBase(reportItemInstance);
			if (this.PreRead(objectType, indexes))
			{
				if (-1 == this.m_currentUniqueName)
				{
					reportItemInstance.UniqueName = this.m_reader.ReadInt32();
				}
				else
				{
					Global.Tracer.Assert(this.m_intermediateFormatVersion.IsRS2005_WithTableOptimizations);
					int currentUniqueName = this.m_currentUniqueName;
					this.m_currentUniqueName = currentUniqueName + 1;
					reportItemInstance.UniqueName = currentUniqueName;
				}
			}
			if (reportItemDef == null)
			{
				reportItemDef = this.ReadReportItemReference(true);
				indexes.CurrentIndex++;
			}
			Global.Tracer.Assert(reportItemDef != null, "(null != reportItemDef)");
			reportItemInstance.ReportItemDef = reportItemDef;
			this.PostRead(objectType, indexes);
		}

		// Token: 0x06006EC6 RID: 28358 RVA: 0x001CDB0C File Offset: 0x001CBD0C
		private ReportItemInstance ReadReportItemInstanceReference()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Reference == this.m_reader.Token);
			IntermediateFormatReader.Assert(ObjectType.OWCChartInstance == this.m_reader.ObjectType || ObjectType.ChartInstance == this.m_reader.ObjectType);
			ReportItemInstance instanceObject = this.GetInstanceObject(this.m_reader.ReferenceValue);
			IntermediateFormatReader.Assert(instanceObject is OWCChartInstance || instanceObject is ChartInstance);
			return instanceObject;
		}

		// Token: 0x06006EC7 RID: 28359 RVA: 0x001CDBA0 File Offset: 0x001CBDA0
		private ReportInstance ReadReportInstance(Report reportDef)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.ReportInstance;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ReportInstance reportInstance = new ReportInstance();
			this.ReadReportItemInstanceBase(reportInstance, reportDef);
			if (this.PreRead(objectType, indexes))
			{
				reportInstance.ReportItemColInstance = this.ReadReportItemColInstance(reportDef.ReportItems);
			}
			if (this.PreRead(objectType, indexes))
			{
				reportInstance.Language = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				reportInstance.NumberOfPages = this.m_reader.ReadInt32();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return reportInstance;
		}

		// Token: 0x06006EC8 RID: 28360 RVA: 0x001CDC70 File Offset: 0x001CBE70
		private ReportItemColInstance ReadReportItemColInstance(ReportItemCollection reportItemsDef)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.ReportItemColInstance;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ReportItemColInstance reportItemColInstance = new ReportItemColInstance();
			this.ReadInstanceInfoOwnerBase(reportItemColInstance);
			if (this.PreRead(objectType, indexes))
			{
				reportItemColInstance.ReportItemInstances = this.ReadReportItemInstanceList(reportItemsDef);
			}
			reportItemColInstance.ReportItemColDef = reportItemsDef;
			if (this.PreRead(objectType, indexes))
			{
				reportItemColInstance.ChildrenStartAndEndPages = this.ReadRenderingPagesRangesList();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return reportItemColInstance;
		}

		// Token: 0x06006EC9 RID: 28361 RVA: 0x001CDD20 File Offset: 0x001CBF20
		private LineInstance ReadLineInstanceInternals(ReportItem reportItemDef)
		{
			ObjectType objectType = ObjectType.LineInstance;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			LineInstance lineInstance = new LineInstance();
			this.ReadReportItemInstanceBase(lineInstance, ref reportItemDef);
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return lineInstance;
		}

		// Token: 0x06006ECA RID: 28362 RVA: 0x001CDD80 File Offset: 0x001CBF80
		private void UpdateUniqueNameForAction(Microsoft.ReportingServices.ReportProcessing.Action actionDef)
		{
			if (-1 != this.m_currentUniqueName && actionDef != null)
			{
				Global.Tracer.Assert(this.m_intermediateFormatVersion.IsRS2005_WithTableOptimizations);
				if ((actionDef.StyleClass != null && actionDef.StyleClass.ExpressionList != null && 0 < actionDef.StyleClass.ExpressionList.Count) || actionDef.ComputedActionItemsCount > 0)
				{
					this.m_currentUniqueName++;
				}
			}
		}

		// Token: 0x06006ECB RID: 28363 RVA: 0x001CDDF0 File Offset: 0x001CBFF0
		private TextBoxInstance ReadTextBoxInstanceInternals(ReportItem reportItemDef)
		{
			ObjectType objectType = ObjectType.TextBoxInstance;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			TextBoxInstance textBoxInstance = new TextBoxInstance();
			this.ReadReportItemInstanceBase(textBoxInstance, ref reportItemDef);
			this.UpdateUniqueNameForAction(((TextBox)reportItemDef).Action);
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return textBoxInstance;
		}

		// Token: 0x06006ECC RID: 28364 RVA: 0x001CDE60 File Offset: 0x001CC060
		private RectangleInstance ReadRectangleInstanceInternals(ReportItem reportItemDef)
		{
			ObjectType objectType = ObjectType.RectangleInstance;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			RectangleInstance rectangleInstance = new RectangleInstance();
			this.ReadReportItemInstanceBase(rectangleInstance, ref reportItemDef);
			if (this.PreRead(objectType, indexes))
			{
				rectangleInstance.ReportItemColInstance = this.ReadReportItemColInstance(((Rectangle)reportItemDef).ReportItems);
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return rectangleInstance;
		}

		// Token: 0x06006ECD RID: 28365 RVA: 0x001CDEE0 File Offset: 0x001CC0E0
		private CheckBoxInstance ReadCheckBoxInstanceInternals(ReportItem reportItemDef)
		{
			ObjectType objectType = ObjectType.CheckBoxInstance;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			CheckBoxInstance checkBoxInstance = new CheckBoxInstance();
			this.ReadReportItemInstanceBase(checkBoxInstance, ref reportItemDef);
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return checkBoxInstance;
		}

		// Token: 0x06006ECE RID: 28366 RVA: 0x001CDF40 File Offset: 0x001CC140
		private ImageInstance ReadImageInstanceInternals(ReportItem reportItemDef)
		{
			ObjectType objectType = ObjectType.ImageInstance;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ImageInstance imageInstance = new ImageInstance();
			this.ReadReportItemInstanceBase(imageInstance, ref reportItemDef);
			this.UpdateUniqueNameForAction(((Image)reportItemDef).Action);
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return imageInstance;
		}

		// Token: 0x06006ECF RID: 28367 RVA: 0x001CDFB0 File Offset: 0x001CC1B0
		private SubReportInstance ReadSubReportInstanceInternals(ReportItem reportItemDef)
		{
			ObjectType objectType = ObjectType.SubReportInstance;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			SubReportInstance subReportInstance = new SubReportInstance();
			this.ReadReportItemInstanceBase(subReportInstance, ref reportItemDef);
			if (this.PreRead(objectType, indexes))
			{
				subReportInstance.ReportInstance = this.ReadReportInstance(((SubReport)reportItemDef).Report);
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return subReportInstance;
		}

		// Token: 0x06006ED0 RID: 28368 RVA: 0x001CE030 File Offset: 0x001CC230
		private ActiveXControlInstance ReadActiveXControlInstanceInternals(ReportItem reportItemDef)
		{
			ObjectType objectType = ObjectType.ActiveXControlInstance;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ActiveXControlInstance activeXControlInstance = new ActiveXControlInstance();
			this.ReadReportItemInstanceBase(activeXControlInstance, ref reportItemDef);
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return activeXControlInstance;
		}

		// Token: 0x06006ED1 RID: 28369 RVA: 0x001CE090 File Offset: 0x001CC290
		private ListInstance ReadListInstanceInternals(ReportItem reportItemDef)
		{
			ObjectType objectType = ObjectType.ListInstance;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ListInstance listInstance = new ListInstance();
			this.ReadReportItemInstanceBase(listInstance, ref reportItemDef);
			if (this.PreRead(objectType, indexes))
			{
				listInstance.ListContents = this.ReadListContentInstanceList((List)reportItemDef);
			}
			if (this.PreRead(objectType, indexes))
			{
				listInstance.ChildrenStartAndEndPages = this.ReadRenderingPagesRangesList();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return listInstance;
		}

		// Token: 0x06006ED2 RID: 28370 RVA: 0x001CE124 File Offset: 0x001CC324
		private ListContentInstance ReadListContentInstance(List listDef)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.ListContentInstance;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ListContentInstance listContentInstance = new ListContentInstance();
			this.ReadInstanceInfoOwnerBase(listContentInstance);
			if (this.PreRead(objectType, indexes))
			{
				listContentInstance.UniqueName = this.m_reader.ReadInt32();
			}
			listContentInstance.ListDef = listDef;
			if (this.PreRead(objectType, indexes))
			{
				listContentInstance.ReportItemColInstance = this.ReadReportItemColInstance(listDef.ReportItems);
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return listContentInstance;
		}

		// Token: 0x06006ED3 RID: 28371 RVA: 0x001CE1E0 File Offset: 0x001CC3E0
		private MatrixInstance ReadMatrixInstanceInternals(ReportItem reportItemDef)
		{
			ObjectType objectType = ObjectType.MatrixInstance;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			MatrixInstance matrixInstance = new MatrixInstance();
			Matrix matrix = (Matrix)reportItemDef;
			this.ReadReportItemInstanceBase(matrixInstance, ref reportItemDef);
			if (this.PreRead(objectType, indexes))
			{
				matrixInstance.CornerContent = this.ReadReportItemInstance(matrix.CornerReportItem);
			}
			if (this.PreRead(objectType, indexes))
			{
				matrixInstance.ColumnInstances = this.ReadMatrixHeadingInstanceList(matrix.Columns);
			}
			if (this.PreRead(objectType, indexes))
			{
				matrixInstance.RowInstances = this.ReadMatrixHeadingInstanceList(matrix.Rows);
			}
			if (this.PreRead(objectType, indexes))
			{
				matrixInstance.Cells = this.ReadMatrixCellInstancesList();
			}
			if (this.PreRead(objectType, indexes))
			{
				matrixInstance.InstanceCountOfInnerRowWithPageBreak = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				matrixInstance.ChildrenStartAndEndPages = this.ReadRenderingPagesRangesList();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return matrixInstance;
		}

		// Token: 0x06006ED4 RID: 28372 RVA: 0x001CE2E4 File Offset: 0x001CC4E4
		private MatrixHeadingInstance ReadMatrixHeadingInstance(MatrixHeading headingDef, int index, int totalCount)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.MatrixHeadingInstance;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			MatrixHeadingInstance matrixHeadingInstance = new MatrixHeadingInstance();
			bool flag = false;
			if (headingDef.Grouping != null && headingDef.Subtotal != null && ((index == 0 && Subtotal.PositionType.Before == headingDef.Subtotal.Position) || (totalCount - 1 == index && headingDef.Subtotal.Position == Subtotal.PositionType.After)))
			{
				flag = true;
			}
			this.ReadInstanceInfoOwnerBase(matrixHeadingInstance);
			if (this.PreRead(objectType, indexes))
			{
				matrixHeadingInstance.UniqueName = this.m_reader.ReadInt32();
			}
			matrixHeadingInstance.MatrixHeadingDef = headingDef;
			if (this.PreRead(objectType, indexes))
			{
				ReportItem reportItem;
				if (headingDef.Grouping == null)
				{
					Global.Tracer.Assert(headingDef.ReportItems != null, "(null != headingDef.ReportItems)");
					reportItem = headingDef.ReportItems[index];
				}
				else if (flag)
				{
					reportItem = headingDef.Subtotal.ReportItem;
				}
				else
				{
					reportItem = headingDef.ReportItem;
				}
				matrixHeadingInstance.Content = this.ReadReportItemInstance(reportItem);
			}
			if (this.PreRead(objectType, indexes))
			{
				MatrixHeading matrixHeading = headingDef.SubHeading;
				if (flag)
				{
					while (matrixHeading != null && matrixHeading.Grouping != null)
					{
						matrixHeading = matrixHeading.SubHeading;
					}
				}
				matrixHeadingInstance.SubHeadingInstances = this.ReadMatrixHeadingInstanceList(matrixHeading);
			}
			if (this.PreRead(objectType, indexes))
			{
				matrixHeadingInstance.IsSubtotal = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				matrixHeadingInstance.ChildrenStartAndEndPages = this.ReadRenderingPagesRangesList();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			if (matrixHeadingInstance.IsSubtotal && matrixHeadingInstance.MatrixHeadingDef.Subtotal.StyleClass != null)
			{
				this.RegisterMatrixHeadingInstanceObject(matrixHeadingInstance);
			}
			return matrixHeadingInstance;
		}

		// Token: 0x06006ED5 RID: 28373 RVA: 0x001CE4AC File Offset: 0x001CC6AC
		internal MatrixCellInstance ReadMatrixCellInstanceBase()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			if (ObjectType.MatrixCellInstance == this.m_reader.ObjectType)
			{
				MatrixCellInstance matrixCellInstance = new MatrixCellInstance();
				this.ReadMatrixCellInstance(matrixCellInstance);
				return matrixCellInstance;
			}
			IntermediateFormatReader.Assert(ObjectType.MatrixSubtotalCellInstance == this.m_reader.ObjectType);
			MatrixSubtotalCellInstance matrixSubtotalCellInstance = new MatrixSubtotalCellInstance();
			this.ReadMatrixSubtotalCellInstance(matrixSubtotalCellInstance);
			return matrixSubtotalCellInstance;
		}

		// Token: 0x06006ED6 RID: 28374 RVA: 0x001CE530 File Offset: 0x001CC730
		private void ReadMatrixCellInstance(MatrixCellInstance instance)
		{
			ObjectType objectType = ObjectType.MatrixCellInstance;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			this.ReadInstanceInfoOwnerBase(instance);
			ReportItem reportItem = null;
			if (this.PreRead(objectType, indexes))
			{
				reportItem = this.ReadReportItemReference(true);
			}
			if (this.PreRead(objectType, indexes))
			{
				instance.Content = this.ReadReportItemInstance(reportItem);
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
		}

		// Token: 0x06006ED7 RID: 28375 RVA: 0x001CE5B4 File Offset: 0x001CC7B4
		internal void ReadMatrixSubtotalCellInstance(MatrixSubtotalCellInstance instance)
		{
			ObjectType objectType = ObjectType.MatrixSubtotalCellInstance;
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			this.ReadInstanceInfoOwnerBase(instance);
			IntermediateFormatReader.Assert(this.m_reader.Read());
			this.ReadMatrixCellInstance(instance);
			int num = this.m_reader.ReadInt32();
			instance.SubtotalHeadingInstance = this.GetMatrixHeadingInstanceObject(num);
			this.m_reader.ReadEndObject();
		}

		// Token: 0x06006ED8 RID: 28376 RVA: 0x001CE630 File Offset: 0x001CC830
		private MultiChartInstance ReadMultiChartInstance(Chart chartDef)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			ObjectType objectType = ObjectType.MultiChartInstance;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			MultiChartInstance multiChartInstance = new MultiChartInstance();
			if (this.PreRead(objectType, indexes))
			{
				multiChartInstance.ColumnInstances = this.ReadChartHeadingInstanceList(chartDef.Columns);
			}
			if (this.PreRead(objectType, indexes))
			{
				multiChartInstance.RowInstances = this.ReadChartHeadingInstanceList(chartDef.Rows);
			}
			if (this.PreRead(objectType, indexes))
			{
				multiChartInstance.DataPoints = this.ReadChartDataPointInstancesList();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return multiChartInstance;
		}

		// Token: 0x06006ED9 RID: 28377 RVA: 0x001CE6E8 File Offset: 0x001CC8E8
		private ChartInstance ReadChartInstanceInternals(ReportItem reportItemDef)
		{
			ObjectType objectType = ObjectType.ChartInstance;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ChartInstance chartInstance = new ChartInstance();
			this.ReadReportItemInstanceBase(chartInstance, ref reportItemDef);
			if (this.PreRead(objectType, indexes))
			{
				chartInstance.MultiCharts = this.ReadMultiChartInstanceList((Chart)reportItemDef);
			}
			this.RegisterInstanceObject(chartInstance);
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return chartInstance;
		}

		// Token: 0x06006EDA RID: 28378 RVA: 0x001CE770 File Offset: 0x001CC970
		private ChartHeadingInstance ReadChartHeadingInstance(ChartHeading headingDef)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.ChartHeadingInstance;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ChartHeadingInstance chartHeadingInstance = new ChartHeadingInstance();
			this.ReadInstanceInfoOwnerBase(chartHeadingInstance);
			if (this.PreRead(objectType, indexes))
			{
				chartHeadingInstance.UniqueName = this.m_reader.ReadInt32();
			}
			chartHeadingInstance.ChartHeadingDef = headingDef;
			if (this.PreRead(objectType, indexes))
			{
				chartHeadingInstance.SubHeadingInstances = this.ReadChartHeadingInstanceList(headingDef.SubHeading);
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return chartHeadingInstance;
		}

		// Token: 0x06006EDB RID: 28379 RVA: 0x001CE830 File Offset: 0x001CCA30
		private ChartDataPointInstance ReadChartDataPointInstance()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			ObjectType objectType = ObjectType.ChartDataPointInstance;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ChartDataPointInstance chartDataPointInstance = new ChartDataPointInstance();
			this.ReadInstanceInfoOwnerBase(chartDataPointInstance);
			if (this.PreRead(objectType, indexes))
			{
				chartDataPointInstance.UniqueName = this.m_reader.ReadInt32();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return chartDataPointInstance;
		}

		// Token: 0x06006EDC RID: 28380 RVA: 0x001CE8BC File Offset: 0x001CCABC
		private AxisInstance ReadAxisInstance()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.AxisInstance;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			AxisInstance axisInstance = new AxisInstance();
			if (this.PreRead(objectType, indexes))
			{
				axisInstance.UniqueName = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				axisInstance.Title = this.ReadChartTitleInstance();
			}
			if (this.PreRead(objectType, indexes))
			{
				axisInstance.StyleAttributeValues = this.ReadVariants();
			}
			if (this.PreRead(objectType, indexes))
			{
				axisInstance.MajorGridLinesStyleAttributeValues = this.ReadVariants();
			}
			if (this.PreRead(objectType, indexes))
			{
				axisInstance.MinorGridLinesStyleAttributeValues = this.ReadVariants();
			}
			if (this.PreRead(objectType, indexes))
			{
				axisInstance.MinValue = this.ReadVariant();
			}
			if (this.PreRead(objectType, indexes))
			{
				axisInstance.MaxValue = this.ReadVariant();
			}
			if (this.PreRead(objectType, indexes))
			{
				axisInstance.CrossAtValue = this.ReadVariant();
			}
			if (this.PreRead(objectType, indexes))
			{
				axisInstance.MajorIntervalValue = this.ReadVariant();
			}
			if (this.PreRead(objectType, indexes))
			{
				axisInstance.MinorIntervalValue = this.ReadVariant();
			}
			if (this.PreRead(objectType, indexes))
			{
				axisInstance.CustomPropertyInstances = this.ReadDataValueInstanceList();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return axisInstance;
		}

		// Token: 0x06006EDD RID: 28381 RVA: 0x001CEA2C File Offset: 0x001CCC2C
		private ChartTitleInstance ReadChartTitleInstance()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.ChartTitleInstance;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			ChartTitleInstance chartTitleInstance = new ChartTitleInstance();
			if (this.PreRead(objectType, indexes))
			{
				chartTitleInstance.UniqueName = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartTitleInstance.Caption = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				chartTitleInstance.StyleAttributeValues = this.ReadVariants();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return chartTitleInstance;
		}

		// Token: 0x06006EDE RID: 28382 RVA: 0x001CEAF0 File Offset: 0x001CCCF0
		private TableInstance ReadTableInstanceInternals(ReportItem reportItemDef)
		{
			ObjectType objectType = ObjectType.TableInstance;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			TableInstance tableInstance = new TableInstance();
			Table table = (Table)reportItemDef;
			this.ReadReportItemInstanceBase(tableInstance, ref reportItemDef);
			if (this.PreRead(objectType, indexes) && (!this.m_intermediateFormatVersion.IsRS2005_WithTableOptimizations || table.HeaderRows != null))
			{
				tableInstance.HeaderRowInstances = this.ReadTableRowInstances(table.HeaderRows, -1);
			}
			if (this.PreRead(objectType, indexes) && (!this.m_intermediateFormatVersion.IsRS2005_WithTableOptimizations || table.TableGroups != null))
			{
				tableInstance.TableGroupInstances = this.ReadTableGroupInstanceList(table.TableGroups);
			}
			tableInstance.TableDetailInstances = this.ReadTableDetailInstances(table, table.TableGroups, objectType, indexes);
			if (this.PreRead(objectType, indexes) && (!this.m_intermediateFormatVersion.IsRS2005_WithTableOptimizations || table.FooterRows != null))
			{
				tableInstance.FooterRowInstances = this.ReadTableRowInstances(table.FooterRows, -1);
			}
			if (this.PreRead(objectType, indexes))
			{
				tableInstance.ChildrenStartAndEndPages = this.ReadRenderingPagesRangesList();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return tableInstance;
		}

		// Token: 0x06006EDF RID: 28383 RVA: 0x001CEC18 File Offset: 0x001CCE18
		private TableDetailInstanceList ReadTableDetailInstances(Table tableDef, TableGroup tableGroup, ObjectType objectType, IntermediateFormatReader.Indexes indexes)
		{
			TableDetailInstanceList tableDetailInstanceList = null;
			bool flag = false;
			if (this.PreRead(objectType, indexes) && tableGroup == null && tableDef.TableDetail != null && tableDef.TableDetail.SimpleDetailRows)
			{
				Global.Tracer.Assert(this.m_intermediateFormatVersion.IsRS2005_WithTableOptimizations);
				flag = true;
				this.m_currentUniqueName = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes) && (!this.m_intermediateFormatVersion.IsRS2005_WithTableOptimizations || (tableGroup == null && tableDef.TableDetail != null)))
			{
				tableDetailInstanceList = this.ReadTableDetailInstanceList(tableDef.TableDetail);
			}
			if (flag)
			{
				this.m_currentUniqueName = -1;
			}
			return tableDetailInstanceList;
		}

		// Token: 0x06006EE0 RID: 28384 RVA: 0x001CECB0 File Offset: 0x001CCEB0
		private TableGroupInstance ReadTableGroupInstance(TableGroup groupDef)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.TableGroupInstance;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			TableGroupInstance tableGroupInstance = new TableGroupInstance();
			this.ReadInstanceInfoOwnerBase(tableGroupInstance);
			if (this.PreRead(objectType, indexes))
			{
				tableGroupInstance.UniqueName = this.m_reader.ReadInt32();
			}
			tableGroupInstance.TableGroupDef = groupDef;
			if (this.PreRead(objectType, indexes) && (!this.m_intermediateFormatVersion.IsRS2005_WithTableOptimizations || groupDef.HeaderRows != null))
			{
				tableGroupInstance.HeaderRowInstances = this.ReadTableRowInstances(groupDef.HeaderRows, -1);
			}
			if (this.PreRead(objectType, indexes) && (!this.m_intermediateFormatVersion.IsRS2005_WithTableOptimizations || groupDef.FooterRows != null))
			{
				tableGroupInstance.FooterRowInstances = this.ReadTableRowInstances(groupDef.FooterRows, -1);
			}
			if (this.PreRead(objectType, indexes) && (!this.m_intermediateFormatVersion.IsRS2005_WithTableOptimizations || groupDef.SubGroup != null))
			{
				tableGroupInstance.SubGroupInstances = this.ReadTableGroupInstanceList(groupDef.SubGroup);
			}
			tableGroupInstance.TableDetailInstances = this.ReadTableDetailInstances((Table)groupDef.DataRegionDef, groupDef.SubGroup, objectType, indexes);
			if (this.PreRead(objectType, indexes))
			{
				tableGroupInstance.ChildrenStartAndEndPages = this.ReadRenderingPagesRangesList();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return tableGroupInstance;
		}

		// Token: 0x06006EE1 RID: 28385 RVA: 0x001CEE18 File Offset: 0x001CD018
		private TableDetailInstance ReadTableDetailInstance(TableDetail detailDef)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.TableDetailInstance;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			TableDetailInstance tableDetailInstance = new TableDetailInstance();
			this.ReadInstanceInfoOwnerBase(tableDetailInstance);
			if (this.PreRead(objectType, indexes))
			{
				if (-1 == this.m_currentUniqueName)
				{
					tableDetailInstance.UniqueName = this.m_reader.ReadInt32();
				}
				else
				{
					TableDetailInstance tableDetailInstance2 = tableDetailInstance;
					int currentUniqueName = this.m_currentUniqueName;
					this.m_currentUniqueName = currentUniqueName + 1;
					tableDetailInstance2.UniqueName = currentUniqueName;
				}
			}
			tableDetailInstance.TableDetailDef = detailDef;
			int currentUniqueName2 = this.m_currentUniqueName;
			if (-1 != this.m_currentUniqueName && detailDef.DetailRows != null)
			{
				for (int i = 0; i < detailDef.DetailRows.Count; i++)
				{
					this.m_currentUniqueName++;
					if (detailDef.DetailRows[i] != null)
					{
						ReportItemCollection reportItems = detailDef.DetailRows[i].ReportItems;
						if (reportItems.NonComputedReportItems != null)
						{
							this.m_currentUniqueName += reportItems.NonComputedReportItems.Count;
						}
					}
				}
			}
			if (this.PreRead(objectType, indexes))
			{
				tableDetailInstance.DetailRowInstances = this.ReadTableRowInstances(detailDef.DetailRows, currentUniqueName2);
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return tableDetailInstance;
		}

		// Token: 0x06006EE2 RID: 28386 RVA: 0x001CEF80 File Offset: 0x001CD180
		internal TableDetailInstanceInfo ReadTableDetailInstanceInfo()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.TableDetailInstanceInfo;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			TableDetailInstanceInfo tableDetailInstanceInfo = new TableDetailInstanceInfo();
			this.ReadInstanceInfoBase(tableDetailInstanceInfo);
			if (this.PreRead(objectType, indexes))
			{
				tableDetailInstanceInfo.StartHidden = this.m_reader.ReadBoolean();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return tableDetailInstanceInfo;
		}

		// Token: 0x06006EE3 RID: 28387 RVA: 0x001CF01C File Offset: 0x001CD21C
		private TableRowInstance ReadTableRowInstance(TableRowList rowDefs, int index, int rowUniqueName)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.TableRowInstance;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			TableRowInstance tableRowInstance = new TableRowInstance();
			this.ReadInstanceInfoOwnerBase(tableRowInstance);
			if (this.PreRead(objectType, indexes))
			{
				if (-1 == rowUniqueName)
				{
					tableRowInstance.UniqueName = this.m_reader.ReadInt32();
				}
				else
				{
					tableRowInstance.UniqueName = rowUniqueName;
				}
			}
			tableRowInstance.TableRowDef = rowDefs[index];
			if (this.PreRead(objectType, indexes))
			{
				tableRowInstance.TableRowReportItemColInstance = this.ReadReportItemColInstance(tableRowInstance.TableRowDef.ReportItems);
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return tableRowInstance;
		}

		// Token: 0x06006EE4 RID: 28388 RVA: 0x001CF0F0 File Offset: 0x001CD2F0
		private TableColumnInstance ReadTableColumnInstance()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.TableColumnInstance;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			TableColumnInstance tableColumnInstance = new TableColumnInstance();
			if (this.PreRead(objectType, indexes))
			{
				tableColumnInstance.UniqueName = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				tableColumnInstance.StartHidden = this.m_reader.ReadBoolean();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return tableColumnInstance;
		}

		// Token: 0x06006EE5 RID: 28389 RVA: 0x001CF19C File Offset: 0x001CD39C
		private OWCChartInstance ReadOWCChartInstanceInternals(ReportItem reportItemDef)
		{
			ObjectType objectType = ObjectType.OWCChartInstance;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			OWCChartInstance owcchartInstance = new OWCChartInstance();
			this.ReadReportItemInstanceBase(owcchartInstance, ref reportItemDef);
			this.RegisterInstanceObject(owcchartInstance);
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return owcchartInstance;
		}

		// Token: 0x06006EE6 RID: 28390 RVA: 0x001CF204 File Offset: 0x001CD404
		private CustomReportItemInstance ReadCustomReportItemInstanceInternals(CustomReportItem reportItemDef)
		{
			ObjectType objectType = ObjectType.CustomReportItemInstance;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			CustomReportItemInstance customReportItemInstance = new CustomReportItemInstance();
			this.ReadReportItemInstanceBase(customReportItemInstance, reportItemDef);
			if (this.PreRead(objectType, indexes))
			{
				if (reportItemDef.RenderReportItem != null && 1 == reportItemDef.RenderReportItem.Count)
				{
					customReportItemInstance.AltReportItemColInstance = this.ReadReportItemColInstance(reportItemDef.RenderReportItem);
				}
				else
				{
					customReportItemInstance.AltReportItemColInstance = this.ReadReportItemColInstance(reportItemDef.AltReportItem);
				}
			}
			if (this.PreRead(objectType, indexes))
			{
				customReportItemInstance.ColumnInstances = this.ReadCustomReportItemHeadingInstanceList(reportItemDef.Columns);
			}
			if (this.PreRead(objectType, indexes))
			{
				customReportItemInstance.RowInstances = this.ReadCustomReportItemHeadingInstanceList(reportItemDef.Rows);
			}
			if (this.PreRead(objectType, indexes))
			{
				customReportItemInstance.Cells = this.ReadCustomReportItemCellInstancesList();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return customReportItemInstance;
		}

		// Token: 0x06006EE7 RID: 28391 RVA: 0x001CF2FC File Offset: 0x001CD4FC
		private CustomReportItemHeadingInstance ReadCustomReportItemHeadingInstance(CustomReportItemHeadingList headingDef, int index, int totalCount)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.CustomReportItemHeadingInstance;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			CustomReportItemHeadingInstance customReportItemHeadingInstance = new CustomReportItemHeadingInstance();
			if (this.PreRead(objectType, indexes))
			{
				customReportItemHeadingInstance.SubHeadingInstances = this.ReadCustomReportItemHeadingInstanceList(headingDef);
			}
			if (this.PreRead(objectType, indexes))
			{
				customReportItemHeadingInstance.HeadingDefinition = this.ReadCustomReportItemHeadingReference();
			}
			if (this.PreRead(objectType, indexes))
			{
				customReportItemHeadingInstance.HeadingCellIndex = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				customReportItemHeadingInstance.HeadingSpan = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				customReportItemHeadingInstance.CustomPropertyInstances = this.ReadDataValueInstanceList();
			}
			if (this.PreRead(objectType, indexes))
			{
				customReportItemHeadingInstance.Label = this.m_reader.ReadString();
			}
			if (this.PreRead(objectType, indexes))
			{
				customReportItemHeadingInstance.GroupExpressionValues = this.ReadVariantList(false);
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return customReportItemHeadingInstance;
		}

		// Token: 0x06006EE8 RID: 28392 RVA: 0x001CF420 File Offset: 0x001CD620
		internal CustomReportItemCellInstance ReadCustomReportItemCellInstance()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.CustomReportItemCellInstance;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			CustomReportItemCellInstance customReportItemCellInstance = new CustomReportItemCellInstance();
			if (this.PreRead(objectType, indexes))
			{
				customReportItemCellInstance.RowIndex = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				customReportItemCellInstance.ColumnIndex = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				customReportItemCellInstance.DataValueInstances = this.ReadDataValueInstanceList();
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return customReportItemCellInstance;
		}

		// Token: 0x06006EE9 RID: 28393 RVA: 0x001CF4E4 File Offset: 0x001CD6E4
		private PageSectionInstance ReadPageSectionInstance(PageSection pageSectionDef)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.PageSectionInstance;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			PageSectionInstance pageSectionInstance = new PageSectionInstance();
			this.ReadReportItemInstanceBase(pageSectionInstance, pageSectionDef);
			if (this.PreRead(objectType, indexes))
			{
				pageSectionInstance.PageNumber = this.m_reader.ReadInt32();
			}
			if (this.PreRead(objectType, indexes))
			{
				pageSectionInstance.ReportItemColInstance = this.ReadReportItemColInstance(pageSectionDef.ReportItems);
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return pageSectionInstance;
		}

		// Token: 0x06006EEA RID: 28394 RVA: 0x001CF59B File Offset: 0x001CD79B
		private object ReadVariant()
		{
			return this.ReadVariant(true);
		}

		// Token: 0x06006EEB RID: 28395 RVA: 0x001CF5A4 File Offset: 0x001CD7A4
		private object ReadVariant(bool readNextToken)
		{
			return this.ReadVariant(readNextToken, false);
		}

		// Token: 0x06006EEC RID: 28396 RVA: 0x001CF5B0 File Offset: 0x001CD7B0
		private object ReadVariant(bool readNextToken, bool convertDBNull)
		{
			if (readNextToken)
			{
				IntermediateFormatReader.Assert(this.m_reader.Read());
			}
			Token token = this.m_reader.Token;
			if (token != Token.Null)
			{
				switch (token)
				{
				case Token.String:
					return this.m_reader.StringValue;
				case Token.DateTime:
					return this.m_reader.DateTimeValue;
				case Token.Char:
					return this.m_reader.CharValue;
				case Token.Boolean:
					return this.m_reader.BooleanValue;
				case Token.Int16:
					return this.m_reader.Int16Value;
				case Token.Int32:
					return this.m_reader.Int32Value;
				case Token.Int64:
					return this.m_reader.Int64Value;
				case Token.UInt16:
					return this.m_reader.UInt16Value;
				case Token.UInt32:
					return this.m_reader.UInt32Value;
				case Token.UInt64:
					return this.m_reader.UInt64Value;
				case Token.Byte:
					return this.m_reader.ByteValue;
				case Token.SByte:
					return this.m_reader.SByteValue;
				case Token.Single:
					return this.m_reader.SingleValue;
				case Token.Double:
					return this.m_reader.DoubleValue;
				case Token.Decimal:
					return this.m_reader.DecimalValue;
				}
				IntermediateFormatReader.Assert(Token.TimeSpan == this.m_reader.Token);
				return this.m_reader.TimeSpanValue;
			}
			if (convertDBNull)
			{
				return DBNull.Value;
			}
			return null;
		}

		// Token: 0x06006EED RID: 28397 RVA: 0x001CF75C File Offset: 0x001CD95C
		private RecordField[] ReadRecordFields()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			IntermediateFormatReader.Assert(Token.Array == this.m_reader.Token);
			int arrayLength = this.m_reader.ArrayLength;
			RecordField[] array = new RecordField[arrayLength];
			for (int i = 0; i < arrayLength; i++)
			{
				array[i] = this.ReadRecordField();
			}
			return array;
		}

		// Token: 0x06006EEE RID: 28398 RVA: 0x001CF7C4 File Offset: 0x001CD9C4
		private RecordField ReadRecordField()
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			if (this.m_reader.Token == Token.Null)
			{
				return null;
			}
			ObjectType objectType = ObjectType.RecordField;
			IntermediateFormatReader.Indexes indexes = new IntermediateFormatReader.Indexes();
			IntermediateFormatReader.Assert(Token.Object == this.m_reader.Token);
			IntermediateFormatReader.Assert(objectType == this.m_reader.ObjectType);
			RecordField recordField = new RecordField();
			if (this.PreRead(objectType, indexes))
			{
				DataFieldStatus dataFieldStatus;
				recordField.FieldValue = this.ReadFieldValue(out dataFieldStatus);
				recordField.FieldStatus = dataFieldStatus;
			}
			if (this.PreRead(objectType, indexes))
			{
				recordField.IsAggregationField = this.m_reader.ReadBoolean();
			}
			if (this.PreRead(objectType, indexes))
			{
				recordField.FieldPropertyValues = this.ReadVariantList(false);
			}
			this.PostRead(objectType, indexes);
			this.m_reader.ReadEndObject();
			return recordField;
		}

		// Token: 0x06006EEF RID: 28399 RVA: 0x001CF890 File Offset: 0x001CDA90
		private object ReadFieldValue(out DataFieldStatus fieldStatus)
		{
			IntermediateFormatReader.Assert(this.m_reader.Read());
			fieldStatus = DataFieldStatus.None;
			Token token = this.m_reader.Token;
			if (token == Token.Null)
			{
				return DBNull.Value;
			}
			if (token == Token.DataFieldInfo)
			{
				fieldStatus = this.m_reader.DataFieldInfo;
				return null;
			}
			switch (token)
			{
			case Token.Guid:
				return this.m_reader.GuidValue;
			case Token.String:
				return this.m_reader.StringValue;
			case Token.DateTime:
				return this.m_reader.DateTimeValue;
			case Token.TimeSpan:
				return this.m_reader.TimeSpanValue;
			case Token.Char:
				return this.m_reader.CharValue;
			case Token.Boolean:
				return this.m_reader.BooleanValue;
			case Token.Int16:
				return this.m_reader.Int16Value;
			case Token.Int32:
				return this.m_reader.Int32Value;
			case Token.Int64:
				return this.m_reader.Int64Value;
			case Token.UInt16:
				return this.m_reader.UInt16Value;
			case Token.UInt32:
				return this.m_reader.UInt32Value;
			case Token.UInt64:
				return this.m_reader.UInt64Value;
			case Token.Byte:
				return this.m_reader.ByteValue;
			case Token.SByte:
				return this.m_reader.SByteValue;
			case Token.Single:
				return this.m_reader.SingleValue;
			case Token.Double:
				return this.m_reader.DoubleValue;
			case Token.Decimal:
				return this.m_reader.DecimalValue;
			default:
				IntermediateFormatReader.Assert(Token.TypedArray == this.m_reader.Token);
				if (Token.Byte == this.m_reader.ArrayType)
				{
					return this.m_reader.BytesValue;
				}
				if (Token.Int32 == this.m_reader.ArrayType)
				{
					return this.m_reader.Int32sValue;
				}
				IntermediateFormatReader.Assert(Token.Char == this.m_reader.ArrayType);
				return this.m_reader.CharsValue;
			}
		}

		// Token: 0x04003971 RID: 14705
		private IntermediateFormatReader.ReportServerBinaryReader m_reader;

		// Token: 0x04003972 RID: 14706
		private Hashtable m_definitionObjects;

		// Token: 0x04003973 RID: 14707
		private Hashtable m_instanceObjects;

		// Token: 0x04003974 RID: 14708
		private Hashtable m_parametersDef;

		// Token: 0x04003975 RID: 14709
		private Hashtable m_parametersInfo;

		// Token: 0x04003976 RID: 14710
		private Hashtable m_matrixHeadingInstanceObjects;

		// Token: 0x04003977 RID: 14711
		private IntermediateFormatReader.State m_state;

		// Token: 0x04003978 RID: 14712
		private bool m_expectDeclarations;

		// Token: 0x04003979 RID: 14713
		private Stack<GroupingList> m_groupingsWithHideDuplicatesStack;

		// Token: 0x0400397A RID: 14714
		private IntermediateFormatVersion m_intermediateFormatVersion;

		// Token: 0x0400397B RID: 14715
		private ArrayList m_textboxesWithUserSort;

		// Token: 0x0400397C RID: 14716
		private int m_currentUniqueName = -1;

		// Token: 0x02000CEC RID: 3308
		internal sealed class State
		{
			// Token: 0x06008DD1 RID: 36305 RVA: 0x00243569 File Offset: 0x00241769
			internal State()
			{
				this.m_oldDeclarations = new DeclarationList();
				this.Initialize();
			}

			// Token: 0x06008DD2 RID: 36306 RVA: 0x00243582 File Offset: 0x00241782
			private State(DeclarationList declarations)
			{
				this.m_oldDeclarations = declarations;
				this.Initialize();
			}

			// Token: 0x06008DD3 RID: 36307 RVA: 0x00243597 File Offset: 0x00241797
			private void Initialize()
			{
				this.m_oldIndexesToSkip = new IntList[DeclarationList.Current.Count][];
				this.m_isInOldDeclaration = new bool[DeclarationList.Current.Count][];
			}

			// Token: 0x17002B66 RID: 11110
			// (get) Token: 0x06008DD4 RID: 36308 RVA: 0x002435C3 File Offset: 0x002417C3
			internal DeclarationList OldDeclarations
			{
				get
				{
					return this.m_oldDeclarations;
				}
			}

			// Token: 0x17002B67 RID: 11111
			// (get) Token: 0x06008DD5 RID: 36309 RVA: 0x002435CB File Offset: 0x002417CB
			internal IntList[][] OldIndexesToSkip
			{
				get
				{
					return this.m_oldIndexesToSkip;
				}
			}

			// Token: 0x17002B68 RID: 11112
			// (get) Token: 0x06008DD6 RID: 36310 RVA: 0x002435D3 File Offset: 0x002417D3
			internal bool[][] IsInOldDeclaration
			{
				get
				{
					return this.m_isInOldDeclaration;
				}
			}

			// Token: 0x04004F72 RID: 20338
			private DeclarationList m_oldDeclarations;

			// Token: 0x04004F73 RID: 20339
			private IntList[][] m_oldIndexesToSkip;

			// Token: 0x04004F74 RID: 20340
			private bool[][] m_isInOldDeclaration;

			// Token: 0x04004F75 RID: 20341
			internal static readonly IntermediateFormatReader.State Current = new IntermediateFormatReader.State(DeclarationList.Current);
		}

		// Token: 0x02000CED RID: 3309
		private sealed class Indexes
		{
			// Token: 0x04004F76 RID: 20342
			internal int CurrentIndex;
		}

		// Token: 0x02000CEE RID: 3310
		private sealed class ReportServerBinaryReader
		{
			// Token: 0x06008DD9 RID: 36313 RVA: 0x002435F4 File Offset: 0x002417F4
			internal ReportServerBinaryReader(Stream stream, IntermediateFormatReader.ReportServerBinaryReader.DeclarationCallback declarationCallback)
			{
				IntermediateFormatReader.Assert(declarationCallback != null);
				this.m_binaryReader = new IntermediateFormatReader.ReportServerBinaryReader.BinaryReaderWrapper(stream);
				this.m_declarationCallback = declarationCallback;
			}

			// Token: 0x17002B69 RID: 11113
			// (get) Token: 0x06008DDA RID: 36314 RVA: 0x00243741 File Offset: 0x00241941
			internal Token Token
			{
				get
				{
					return this.m_token;
				}
			}

			// Token: 0x17002B6A RID: 11114
			// (get) Token: 0x06008DDB RID: 36315 RVA: 0x00243749 File Offset: 0x00241949
			internal ObjectType ObjectType
			{
				get
				{
					return this.m_objectType;
				}
			}

			// Token: 0x17002B6B RID: 11115
			// (get) Token: 0x06008DDC RID: 36316 RVA: 0x00243751 File Offset: 0x00241951
			internal Token ArrayType
			{
				get
				{
					return this.m_arrayType;
				}
			}

			// Token: 0x17002B6C RID: 11116
			// (get) Token: 0x06008DDD RID: 36317 RVA: 0x00243759 File Offset: 0x00241959
			internal int ArrayLength
			{
				get
				{
					return this.m_arrayLength;
				}
			}

			// Token: 0x17002B6D RID: 11117
			// (get) Token: 0x06008DDE RID: 36318 RVA: 0x00243761 File Offset: 0x00241961
			internal int ReferenceValue
			{
				get
				{
					return this.m_referenceValue;
				}
			}

			// Token: 0x17002B6E RID: 11118
			// (get) Token: 0x06008DDF RID: 36319 RVA: 0x00243769 File Offset: 0x00241969
			internal string StringValue
			{
				get
				{
					return this.m_stringValue;
				}
			}

			// Token: 0x17002B6F RID: 11119
			// (get) Token: 0x06008DE0 RID: 36320 RVA: 0x00243771 File Offset: 0x00241971
			internal char CharValue
			{
				get
				{
					return this.m_charValue;
				}
			}

			// Token: 0x17002B70 RID: 11120
			// (get) Token: 0x06008DE1 RID: 36321 RVA: 0x00243779 File Offset: 0x00241979
			internal char[] CharsValue
			{
				get
				{
					return this.m_charsValue;
				}
			}

			// Token: 0x17002B71 RID: 11121
			// (get) Token: 0x06008DE2 RID: 36322 RVA: 0x00243781 File Offset: 0x00241981
			internal bool BooleanValue
			{
				get
				{
					return this.m_booleanValue;
				}
			}

			// Token: 0x17002B72 RID: 11122
			// (get) Token: 0x06008DE3 RID: 36323 RVA: 0x00243789 File Offset: 0x00241989
			internal short Int16Value
			{
				get
				{
					return this.m_int16Value;
				}
			}

			// Token: 0x17002B73 RID: 11123
			// (get) Token: 0x06008DE4 RID: 36324 RVA: 0x00243791 File Offset: 0x00241991
			internal int Int32Value
			{
				get
				{
					return this.m_int32Value;
				}
			}

			// Token: 0x17002B74 RID: 11124
			// (get) Token: 0x06008DE5 RID: 36325 RVA: 0x00243799 File Offset: 0x00241999
			internal int[] Int32sValue
			{
				get
				{
					return this.m_int32sValue;
				}
			}

			// Token: 0x17002B75 RID: 11125
			// (get) Token: 0x06008DE6 RID: 36326 RVA: 0x002437A1 File Offset: 0x002419A1
			internal long Int64Value
			{
				get
				{
					return this.m_int64Value;
				}
			}

			// Token: 0x17002B76 RID: 11126
			// (get) Token: 0x06008DE7 RID: 36327 RVA: 0x002437A9 File Offset: 0x002419A9
			internal ushort UInt16Value
			{
				get
				{
					return this.m_uint16Value;
				}
			}

			// Token: 0x17002B77 RID: 11127
			// (get) Token: 0x06008DE8 RID: 36328 RVA: 0x002437B1 File Offset: 0x002419B1
			internal uint UInt32Value
			{
				get
				{
					return this.m_uint32Value;
				}
			}

			// Token: 0x17002B78 RID: 11128
			// (get) Token: 0x06008DE9 RID: 36329 RVA: 0x002437B9 File Offset: 0x002419B9
			internal ulong UInt64Value
			{
				get
				{
					return this.m_uint64Value;
				}
			}

			// Token: 0x17002B79 RID: 11129
			// (get) Token: 0x06008DEA RID: 36330 RVA: 0x002437C1 File Offset: 0x002419C1
			internal byte ByteValue
			{
				get
				{
					return this.m_byteValue;
				}
			}

			// Token: 0x17002B7A RID: 11130
			// (get) Token: 0x06008DEB RID: 36331 RVA: 0x002437C9 File Offset: 0x002419C9
			internal byte[] BytesValue
			{
				get
				{
					return this.m_bytesValue;
				}
			}

			// Token: 0x17002B7B RID: 11131
			// (get) Token: 0x06008DEC RID: 36332 RVA: 0x002437D1 File Offset: 0x002419D1
			internal sbyte SByteValue
			{
				get
				{
					return this.m_sbyteValue;
				}
			}

			// Token: 0x17002B7C RID: 11132
			// (get) Token: 0x06008DED RID: 36333 RVA: 0x002437D9 File Offset: 0x002419D9
			internal float SingleValue
			{
				get
				{
					return this.m_singleValue;
				}
			}

			// Token: 0x17002B7D RID: 11133
			// (get) Token: 0x06008DEE RID: 36334 RVA: 0x002437E1 File Offset: 0x002419E1
			internal double DoubleValue
			{
				get
				{
					return this.m_doubleValue;
				}
			}

			// Token: 0x17002B7E RID: 11134
			// (get) Token: 0x06008DEF RID: 36335 RVA: 0x002437E9 File Offset: 0x002419E9
			internal decimal DecimalValue
			{
				get
				{
					return this.m_decimalValue;
				}
			}

			// Token: 0x17002B7F RID: 11135
			// (get) Token: 0x06008DF0 RID: 36336 RVA: 0x002437F1 File Offset: 0x002419F1
			internal DateTime DateTimeValue
			{
				get
				{
					return this.m_dateTimeValue;
				}
			}

			// Token: 0x17002B80 RID: 11136
			// (get) Token: 0x06008DF1 RID: 36337 RVA: 0x002437F9 File Offset: 0x002419F9
			internal TimeSpan TimeSpanValue
			{
				get
				{
					return this.m_timeSpanValue;
				}
			}

			// Token: 0x17002B81 RID: 11137
			// (get) Token: 0x06008DF2 RID: 36338 RVA: 0x00243801 File Offset: 0x00241A01
			internal Guid GuidValue
			{
				get
				{
					return this.m_guidValue;
				}
			}

			// Token: 0x17002B82 RID: 11138
			// (get) Token: 0x06008DF3 RID: 36339 RVA: 0x00243809 File Offset: 0x00241A09
			internal DataFieldStatus DataFieldInfo
			{
				get
				{
					return (DataFieldStatus)this.m_enumValue;
				}
			}

			// Token: 0x06008DF4 RID: 36340 RVA: 0x00243814 File Offset: 0x00241A14
			internal bool Read()
			{
				bool flag;
				for (flag = this.Advance(); flag && Token.Declaration == this.m_token; flag = this.Advance())
				{
				}
				return flag;
			}

			// Token: 0x06008DF5 RID: 36341 RVA: 0x00243840 File Offset: 0x00241A40
			internal bool ReadNoTypeReference()
			{
				bool flag;
				for (flag = this.ReadNoTypeReferenceAdvance(); flag && Token.Declaration == this.m_token; flag = this.ReadNoTypeReferenceAdvance())
				{
				}
				return flag;
			}

			// Token: 0x06008DF6 RID: 36342 RVA: 0x0024386A File Offset: 0x00241A6A
			internal void ReadDeclaration()
			{
				IntermediateFormatReader.Assert(this.Advance());
				IntermediateFormatReader.Assert(Token.Declaration == this.m_token);
			}

			// Token: 0x06008DF7 RID: 36343 RVA: 0x00243888 File Offset: 0x00241A88
			private bool Advance()
			{
				bool flag;
				try
				{
					this.m_objectType = IntermediateFormatReader.ReportServerBinaryReader.ObjectTypeDefault;
					this.m_token = this.UnsafeReadToken();
					Token token = this.m_token;
					switch (token)
					{
					case Token.Null:
					case Token.EndObject:
						break;
					case Token.Object:
						this.m_objectType = this.UnsafeReadObjectType();
						break;
					case Token.Reference:
						this.m_objectType = this.UnsafeReadObjectType();
						this.m_referenceValue = this.m_binaryReader.ReadInt32();
						break;
					case Token.Enum:
						this.m_enumValue = this.m_binaryReader.Read7BitEncodedInt();
						break;
					case Token.TypedArray:
					{
						this.m_arrayType = this.ReadToken();
						int num = this.m_binaryReader.Read7BitEncodedInt();
						if (Token.Byte == this.m_arrayType)
						{
							this.m_bytesValue = this.m_binaryReader.ReadBytes(num);
						}
						else if (Token.Int32 == this.m_arrayType)
						{
							this.m_int32sValue = new int[num];
							for (int i = 0; i < num; i++)
							{
								this.m_int32sValue[i] = this.m_binaryReader.ReadInt32();
							}
						}
						else if (Token.Single == this.m_arrayType)
						{
							this.m_floatsValue = new float[num];
							for (int j = 0; j < num; j++)
							{
								this.m_floatsValue[j] = this.m_binaryReader.ReadSingle();
							}
						}
						else
						{
							IntermediateFormatReader.Assert(Token.Char == this.m_arrayType);
							this.m_charsValue = this.m_binaryReader.ReadChars(num);
						}
						break;
					}
					case Token.Array:
						this.m_arrayLength = this.m_binaryReader.Read7BitEncodedInt();
						break;
					case Token.Declaration:
					{
						ObjectType objectType = this.ReadObjectType();
						ObjectType objectType2 = this.ReadObjectType();
						int num2 = this.m_binaryReader.Read7BitEncodedInt();
						MemberInfoList memberInfoList = new MemberInfoList(num2);
						for (int k = 0; k < num2; k++)
						{
							memberInfoList.Add(new MemberInfo(this.ReadMemberName(), this.ReadToken(), this.ReadObjectType()));
						}
						Declaration declaration = new Declaration(objectType2, memberInfoList);
						this.m_declarationCallback(objectType, declaration);
						break;
					}
					case Token.DataFieldInfo:
						this.m_enumValue = this.m_binaryReader.Read7BitEncodedInt();
						break;
					default:
						switch (token)
						{
						case Token.Guid:
						{
							byte[] array = this.m_binaryReader.ReadBytes(16);
							IntermediateFormatReader.Assert(array != null);
							IntermediateFormatReader.Assert(16 == array.Length);
							this.m_guidValue = new Guid(array);
							break;
						}
						case Token.String:
							this.m_stringValue = this.m_binaryReader.ReadString();
							break;
						case Token.DateTime:
							this.m_dateTimeValue = new DateTime(this.m_binaryReader.ReadInt64());
							break;
						case Token.TimeSpan:
							this.m_timeSpanValue = new TimeSpan(this.m_binaryReader.ReadInt64());
							break;
						case Token.Char:
							this.m_charValue = this.m_binaryReader.ReadChar();
							break;
						case Token.Boolean:
							this.m_booleanValue = this.m_binaryReader.ReadBoolean();
							break;
						case Token.Int16:
							this.m_int16Value = this.m_binaryReader.ReadInt16();
							break;
						case Token.Int32:
							this.m_int32Value = this.m_binaryReader.ReadInt32();
							break;
						case Token.Int64:
							this.m_int64Value = this.m_binaryReader.ReadInt64();
							break;
						case Token.UInt16:
							this.m_uint16Value = this.m_binaryReader.ReadUInt16();
							break;
						case Token.UInt32:
							this.m_uint32Value = this.m_binaryReader.ReadUInt32();
							break;
						case Token.UInt64:
							this.m_uint64Value = this.m_binaryReader.ReadUInt64();
							break;
						case Token.Byte:
							this.m_byteValue = this.m_binaryReader.ReadByte();
							break;
						case Token.SByte:
							this.m_sbyteValue = this.m_binaryReader.ReadSByte();
							break;
						case Token.Single:
							this.m_singleValue = this.m_binaryReader.ReadSingle();
							break;
						case Token.Double:
							this.m_doubleValue = this.m_binaryReader.ReadDouble();
							break;
						case Token.Decimal:
							this.m_decimalValue = this.m_binaryReader.ReadDecimal();
							break;
						default:
							IntermediateFormatReader.Assert(false);
							return false;
						}
						break;
					}
					flag = true;
				}
				catch (IOException)
				{
					flag = false;
				}
				return flag;
			}

			// Token: 0x06008DF8 RID: 36344 RVA: 0x00243CC4 File Offset: 0x00241EC4
			internal bool ReadNoTypeReferenceAdvance()
			{
				bool flag;
				try
				{
					this.m_token = this.UnsafeReadToken();
					IntermediateFormatReader.Assert(Token.Reference == this.m_token || this.m_token == Token.Null);
					if (Token.Reference == this.m_token)
					{
						this.m_referenceValue = this.m_binaryReader.ReadInt32();
					}
					flag = true;
				}
				catch (IOException)
				{
					flag = false;
				}
				return flag;
			}

			// Token: 0x06008DF9 RID: 36345 RVA: 0x00243D2C File Offset: 0x00241F2C
			internal byte[] ReadBytes()
			{
				IntermediateFormatReader.Assert(this.Read());
				if (this.m_token == Token.Null)
				{
					return null;
				}
				IntermediateFormatReader.Assert(Token.TypedArray == this.m_token);
				IntermediateFormatReader.Assert(Token.Byte == this.m_arrayType);
				return this.m_bytesValue;
			}

			// Token: 0x06008DFA RID: 36346 RVA: 0x00243D69 File Offset: 0x00241F69
			internal int[] ReadInt32s()
			{
				IntermediateFormatReader.Assert(this.Read());
				if (this.m_token == Token.Null)
				{
					return null;
				}
				IntermediateFormatReader.Assert(Token.TypedArray == this.m_token);
				IntermediateFormatReader.Assert(Token.Int32 == this.m_arrayType);
				return this.m_int32sValue;
			}

			// Token: 0x06008DFB RID: 36347 RVA: 0x00243DA6 File Offset: 0x00241FA6
			internal float[] ReadFloatArray()
			{
				IntermediateFormatReader.Assert(this.Read());
				if (this.m_token == Token.Null)
				{
					return null;
				}
				IntermediateFormatReader.Assert(Token.TypedArray == this.m_token);
				IntermediateFormatReader.Assert(Token.Single == this.m_arrayType);
				return this.m_floatsValue;
			}

			// Token: 0x06008DFC RID: 36348 RVA: 0x00243DE3 File Offset: 0x00241FE3
			internal Guid ReadGuid()
			{
				IntermediateFormatReader.Assert(this.Read());
				IntermediateFormatReader.Assert(Token.Guid == this.m_token);
				return this.m_guidValue;
			}

			// Token: 0x06008DFD RID: 36349 RVA: 0x00243E08 File Offset: 0x00242008
			internal string ReadString()
			{
				IntermediateFormatReader.Assert(this.Read());
				if (this.m_token == Token.Null)
				{
					return null;
				}
				IntermediateFormatReader.Assert(Token.String == this.m_token);
				return this.m_stringValue;
			}

			// Token: 0x06008DFE RID: 36350 RVA: 0x00243E37 File Offset: 0x00242037
			internal int ReadInt32()
			{
				IntermediateFormatReader.Assert(this.Read());
				IntermediateFormatReader.Assert(Token.Int32 == this.m_token);
				return this.m_int32Value;
			}

			// Token: 0x06008DFF RID: 36351 RVA: 0x00243E5C File Offset: 0x0024205C
			internal long ReadInt64()
			{
				IntermediateFormatReader.Assert(this.Read());
				IntermediateFormatReader.Assert(Token.Int64 == this.m_token);
				return this.m_int64Value;
			}

			// Token: 0x06008E00 RID: 36352 RVA: 0x00243E81 File Offset: 0x00242081
			internal double ReadDouble()
			{
				IntermediateFormatReader.Assert(this.Read());
				IntermediateFormatReader.Assert(Token.Double == this.m_token);
				return this.m_doubleValue;
			}

			// Token: 0x06008E01 RID: 36353 RVA: 0x00243EA6 File Offset: 0x002420A6
			internal bool ReadBoolean()
			{
				IntermediateFormatReader.Assert(this.Read());
				IntermediateFormatReader.Assert(Token.Boolean == this.m_token);
				return this.m_booleanValue;
			}

			// Token: 0x06008E02 RID: 36354 RVA: 0x00243ECB File Offset: 0x002420CB
			internal DateTime ReadDateTime()
			{
				IntermediateFormatReader.Assert(this.Read());
				IntermediateFormatReader.Assert(Token.DateTime == this.m_token);
				return this.m_dateTimeValue;
			}

			// Token: 0x06008E03 RID: 36355 RVA: 0x00243EF0 File Offset: 0x002420F0
			internal int ReadEnum()
			{
				IntermediateFormatReader.Assert(this.Read());
				IntermediateFormatReader.Assert(Token.Enum == this.m_token);
				return this.m_enumValue;
			}

			// Token: 0x06008E04 RID: 36356 RVA: 0x00243F11 File Offset: 0x00242111
			internal ObjectType ReadObject()
			{
				IntermediateFormatReader.Assert(this.Read());
				IntermediateFormatReader.Assert(Token.Object == this.m_token);
				return this.m_objectType;
			}

			// Token: 0x06008E05 RID: 36357 RVA: 0x00243F32 File Offset: 0x00242132
			internal void ReadEndObject()
			{
				IntermediateFormatReader.Assert(this.Read());
				IntermediateFormatReader.Assert(Token.EndObject == this.m_token);
			}

			// Token: 0x06008E06 RID: 36358 RVA: 0x00243F4D File Offset: 0x0024214D
			internal int ReadArray()
			{
				IntermediateFormatReader.Assert(this.Read());
				IntermediateFormatReader.Assert(Token.Array == this.m_token);
				return this.m_arrayLength;
			}

			// Token: 0x06008E07 RID: 36359 RVA: 0x00243F6E File Offset: 0x0024216E
			private Token UnsafeReadToken()
			{
				return (Token)this.m_binaryReader.ReadByte();
			}

			// Token: 0x06008E08 RID: 36360 RVA: 0x00243F7B File Offset: 0x0024217B
			private Token ReadToken()
			{
				return (Token)this.m_binaryReader.ReadByte();
			}

			// Token: 0x06008E09 RID: 36361 RVA: 0x00243F88 File Offset: 0x00242188
			private ObjectType UnsafeReadObjectType()
			{
				return (ObjectType)this.m_binaryReader.Read7BitEncodedInt();
			}

			// Token: 0x06008E0A RID: 36362 RVA: 0x00243F95 File Offset: 0x00242195
			private ObjectType ReadObjectType()
			{
				return (ObjectType)this.m_binaryReader.Read7BitEncodedInt();
			}

			// Token: 0x06008E0B RID: 36363 RVA: 0x00243FA2 File Offset: 0x002421A2
			private MemberName ReadMemberName()
			{
				return (MemberName)this.m_binaryReader.Read7BitEncodedInt();
			}

			// Token: 0x04004F77 RID: 20343
			private IntermediateFormatReader.ReportServerBinaryReader.BinaryReaderWrapper m_binaryReader;

			// Token: 0x04004F78 RID: 20344
			private IntermediateFormatReader.ReportServerBinaryReader.DeclarationCallback m_declarationCallback;

			// Token: 0x04004F79 RID: 20345
			private Token m_token;

			// Token: 0x04004F7A RID: 20346
			private ObjectType m_objectType = IntermediateFormatReader.ReportServerBinaryReader.ObjectTypeDefault;

			// Token: 0x04004F7B RID: 20347
			private int m_referenceValue = IntermediateFormatReader.ReportServerBinaryReader.ReferenceDefault;

			// Token: 0x04004F7C RID: 20348
			private int m_enumValue = IntermediateFormatReader.ReportServerBinaryReader.EnumDefault;

			// Token: 0x04004F7D RID: 20349
			private int m_arrayLength = IntermediateFormatReader.ReportServerBinaryReader.ArrayLengthDefault;

			// Token: 0x04004F7E RID: 20350
			private Guid m_guidValue = IntermediateFormatReader.ReportServerBinaryReader.GuidDefault;

			// Token: 0x04004F7F RID: 20351
			private string m_stringValue = IntermediateFormatReader.ReportServerBinaryReader.StringDefault;

			// Token: 0x04004F80 RID: 20352
			private DateTime m_dateTimeValue = IntermediateFormatReader.ReportServerBinaryReader.DateTimeDefault;

			// Token: 0x04004F81 RID: 20353
			private TimeSpan m_timeSpanValue = IntermediateFormatReader.ReportServerBinaryReader.TimeSpanDefault;

			// Token: 0x04004F82 RID: 20354
			private char m_charValue = IntermediateFormatReader.ReportServerBinaryReader.CharDefault;

			// Token: 0x04004F83 RID: 20355
			private bool m_booleanValue = IntermediateFormatReader.ReportServerBinaryReader.BooleanDefault;

			// Token: 0x04004F84 RID: 20356
			private short m_int16Value = IntermediateFormatReader.ReportServerBinaryReader.Int16Default;

			// Token: 0x04004F85 RID: 20357
			private int m_int32Value = IntermediateFormatReader.ReportServerBinaryReader.Int32Default;

			// Token: 0x04004F86 RID: 20358
			private long m_int64Value = IntermediateFormatReader.ReportServerBinaryReader.Int64Default;

			// Token: 0x04004F87 RID: 20359
			private ushort m_uint16Value = IntermediateFormatReader.ReportServerBinaryReader.UInt16Default;

			// Token: 0x04004F88 RID: 20360
			private uint m_uint32Value = IntermediateFormatReader.ReportServerBinaryReader.UInt32Default;

			// Token: 0x04004F89 RID: 20361
			private ulong m_uint64Value = IntermediateFormatReader.ReportServerBinaryReader.UInt64Default;

			// Token: 0x04004F8A RID: 20362
			private byte m_byteValue = IntermediateFormatReader.ReportServerBinaryReader.ByteDefault;

			// Token: 0x04004F8B RID: 20363
			private sbyte m_sbyteValue = IntermediateFormatReader.ReportServerBinaryReader.SByteDefault;

			// Token: 0x04004F8C RID: 20364
			private float m_singleValue = IntermediateFormatReader.ReportServerBinaryReader.SingleDefault;

			// Token: 0x04004F8D RID: 20365
			private double m_doubleValue = IntermediateFormatReader.ReportServerBinaryReader.DoubleDefault;

			// Token: 0x04004F8E RID: 20366
			private decimal m_decimalValue = IntermediateFormatReader.ReportServerBinaryReader.DecimalDefault;

			// Token: 0x04004F8F RID: 20367
			private Token m_arrayType = IntermediateFormatReader.ReportServerBinaryReader.ArrayTypeDefault;

			// Token: 0x04004F90 RID: 20368
			private byte[] m_bytesValue = IntermediateFormatReader.ReportServerBinaryReader.BytesDefault;

			// Token: 0x04004F91 RID: 20369
			private int[] m_int32sValue = IntermediateFormatReader.ReportServerBinaryReader.Int32sDefault;

			// Token: 0x04004F92 RID: 20370
			private char[] m_charsValue = IntermediateFormatReader.ReportServerBinaryReader.CharsDefault;

			// Token: 0x04004F93 RID: 20371
			private float[] m_floatsValue = IntermediateFormatReader.ReportServerBinaryReader.FloatsDefault;

			// Token: 0x04004F94 RID: 20372
			private static readonly ObjectType ObjectTypeDefault = ObjectType.None;

			// Token: 0x04004F95 RID: 20373
			private static readonly int ReferenceDefault = 0;

			// Token: 0x04004F96 RID: 20374
			private static readonly int EnumDefault = 0;

			// Token: 0x04004F97 RID: 20375
			private static readonly int ArrayLengthDefault = 0;

			// Token: 0x04004F98 RID: 20376
			private static readonly Guid GuidDefault = Guid.Empty;

			// Token: 0x04004F99 RID: 20377
			private static readonly string StringDefault = null;

			// Token: 0x04004F9A RID: 20378
			private static readonly DateTime DateTimeDefault = new DateTime(0L);

			// Token: 0x04004F9B RID: 20379
			private static readonly TimeSpan TimeSpanDefault = new TimeSpan(0L);

			// Token: 0x04004F9C RID: 20380
			private static readonly char CharDefault = '\0';

			// Token: 0x04004F9D RID: 20381
			private static readonly bool BooleanDefault = false;

			// Token: 0x04004F9E RID: 20382
			private static readonly short Int16Default = 0;

			// Token: 0x04004F9F RID: 20383
			private static readonly int Int32Default = 0;

			// Token: 0x04004FA0 RID: 20384
			private static readonly long Int64Default = 0L;

			// Token: 0x04004FA1 RID: 20385
			private static readonly ushort UInt16Default = 0;

			// Token: 0x04004FA2 RID: 20386
			private static readonly uint UInt32Default = 0U;

			// Token: 0x04004FA3 RID: 20387
			private static readonly ulong UInt64Default = 0UL;

			// Token: 0x04004FA4 RID: 20388
			private static readonly byte ByteDefault = 0;

			// Token: 0x04004FA5 RID: 20389
			private static readonly sbyte SByteDefault = 0;

			// Token: 0x04004FA6 RID: 20390
			private static readonly float SingleDefault = 0f;

			// Token: 0x04004FA7 RID: 20391
			private static readonly double DoubleDefault = 0.0;

			// Token: 0x04004FA8 RID: 20392
			private static readonly decimal DecimalDefault = 0m;

			// Token: 0x04004FA9 RID: 20393
			private static readonly Token ArrayTypeDefault = Token.Byte;

			// Token: 0x04004FAA RID: 20394
			private static readonly byte[] BytesDefault = null;

			// Token: 0x04004FAB RID: 20395
			private static readonly int[] Int32sDefault = null;

			// Token: 0x04004FAC RID: 20396
			private static readonly char[] CharsDefault = null;

			// Token: 0x04004FAD RID: 20397
			private static readonly float[] FloatsDefault = null;

			// Token: 0x02000D4B RID: 3403
			private sealed class BinaryReaderWrapper : BinaryReader
			{
				// Token: 0x06008FD8 RID: 36824 RVA: 0x00247ECE File Offset: 0x002460CE
				internal BinaryReaderWrapper(Stream stream)
					: base(stream, Encoding.Unicode)
				{
				}

				// Token: 0x06008FD9 RID: 36825 RVA: 0x00247EDC File Offset: 0x002460DC
				internal new int Read7BitEncodedInt()
				{
					return base.Read7BitEncodedInt();
				}

				// Token: 0x06008FDA RID: 36826 RVA: 0x00247EE4 File Offset: 0x002460E4
				public override byte ReadByte()
				{
					return (byte)this.BaseStream.ReadByte();
				}
			}

			// Token: 0x02000D4C RID: 3404
			// (Invoke) Token: 0x06008FDC RID: 36828
			internal delegate void DeclarationCallback(ObjectType objectType, Declaration declaration);
		}
	}
}
