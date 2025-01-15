using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.Common.DaxComparer;
using Microsoft.DataShaping.InternalContracts.DataShapeResultWriter;
using Microsoft.DataShaping.Processing.Correlation;
using Microsoft.DataShaping.Processing.Pipeline;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;
using Microsoft.DataShaping.Processing.Utils;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.DataShapeResult;
using Microsoft.InfoNav.Utils;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.DataShapeResultGeneration
{
	// Token: 0x02000073 RID: 115
	internal sealed class DataShapeResultGenerator
	{
		// Token: 0x060002C5 RID: 709 RVA: 0x000083A0 File Offset: 0x000065A0
		internal DataShapeResultGenerator(Microsoft.DataShaping.ServiceContracts.ITelemetryService telemetryService, Microsoft.DataShaping.ServiceContracts.ITracer tracer, DataShapeDefinition dsd, IStreamingStructureEncodedWriter writer, ProcessingCompareInfo compareInfo, ProcessingTelemetry processingStats, IRowSourceManager rowSourceManager, DsrWriterOptions options)
		{
			this._telemetryService = telemetryService;
			this._dsd = dsd;
			this._options = options;
			this._writer = StreamingDsrWriterWrapperBase.CreateWriter<DataShapeResultWriter>(writer, options.Names);
			this._keyGenerator = new StringKeyGenerator(compareInfo.CompareInfo, compareInfo.CompareOptions, compareInfo.NullAsBlank, compareInfo.UseOrdinalStringKeyGeneration);
			this._comparer = new DaxDataComparer(compareInfo.CompareInfo, compareInfo.CompareOptions, compareInfo.NullAsBlank);
			this._evaluator = new ExpressionEvaluator(this._comparer, this._keyGenerator, this._dsd.ResultTableInfos.Count);
			this._restartKindGenerator = new RestartKindGenerator(this._evaluator);
			this._typeEvaluator = new ExpressionTypeEvaluator(this._dsd.ResultTableInfos.Count);
			this._dictEncodingCoordinator = this.CreateDictionaryEncodingCoordinator(options, dsd.EncodingHints);
			this._calcGenerator = CalculationGenerator.Create(options, this._evaluator, this._typeEvaluator, this._dictEncodingCoordinator);
			this._tableMetadata = new ResultTableMetadataManager(this._dsd.ResultTableMetadata);
			this._data = new DataPipeline(rowSourceManager, this._comparer, this._keyGenerator, this._dsd.ResultTableInfos.Count, this._tableMetadata);
			this._processingStats = processingStats;
			this._rootDataShapeId = this._dsd.DataShape.Id;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00008520 File Offset: 0x00006720
		internal DataShapeResultGenerator(IRowSourceManager rowSourceManager, ExpressionEvaluator evaluator, RestartKindGenerator restartKindGenerator, ExpressionTypeEvaluator typeEvaluator, ProcessingCompareInfo compareInfo, IKeyGenerator keyGenerator, RestartManager restartManager, int resultSetCount, IList<int> restartIndicesWithStartPositions, ResultTableMetadata tableMetadata, ResultEncodingHints encodingHints, DsrWriterOptions options, string rootDataShapeId)
		{
			this._keyGenerator = keyGenerator;
			this._comparer = new DaxDataComparer(compareInfo.CompareInfo, compareInfo.CompareOptions, compareInfo.NullAsBlank);
			this._segmentationManager = new SegmentationManager(this._comparer, restartIndicesWithStartPositions);
			this._evaluator = evaluator;
			this._restartKindGenerator = restartKindGenerator;
			this._typeEvaluator = typeEvaluator;
			this._options = options;
			this._dictEncodingCoordinator = this.CreateDictionaryEncodingCoordinator(options, encodingHints);
			this._calcGenerator = CalculationGenerator.Create(options, this._evaluator, this._typeEvaluator, this._dictEncodingCoordinator);
			this._tableMetadata = new ResultTableMetadataManager(tableMetadata);
			this._data = new DataPipeline(rowSourceManager, this._comparer, this._keyGenerator, resultSetCount, this._tableMetadata);
			this._restartManager = restartManager;
			this._processingStats = new ProcessingTelemetry();
			this._rootDataShapeId = rootDataShapeId;
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00008613 File Offset: 0x00006813
		internal DataPipeline Data
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000861B File Offset: 0x0000681B
		internal SegmentationManager SegmentationManager
		{
			get
			{
				return this._segmentationManager;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x00008623 File Offset: 0x00006823
		internal ExpressionEvaluator Evaluator
		{
			get
			{
				return this._evaluator;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000862B File Offset: 0x0000682B
		internal ExpressionTypeEvaluator TypeEvaluator
		{
			get
			{
				return this._typeEvaluator;
			}
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00008633 File Offset: 0x00006833
		internal void Generate()
		{
			this._telemetryService.RunInActivity(ActivityKind.DataShapeResultGeneration, delegate
			{
				this.GenerateImpl();
			});
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00008650 File Offset: 0x00006850
		private void GenerateImpl()
		{
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataShape dataShape = this._dsd.DataShape;
			if (dataShape.RestartDefinitions == null)
			{
				DataWindows dataWindows = dataShape.DataWindows;
				bool flag;
				if (dataWindows == null)
				{
					flag = false;
				}
				else
				{
					flag = dataWindows.Windows.Any((Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataWindow dw) => !dw.RestartDefinitions.IsNullOrEmpty<IList<ExpressionNode>>());
				}
				if (!flag)
				{
					goto IL_0073;
				}
			}
			this._restartManager = new RestartManager(this._comparer, this._keyGenerator, this._dsd.ResultTableInfos.Count);
			IL_0073:
			this._segmentationManager = new SegmentationManager(this._comparer, dataShape.RestartIndicesWithStartPosition);
			this._writer.Begin();
			if (this._options.Version != DsrVersion.V1)
			{
				this._writer.WriteVersion(this._options.Version);
				this._writer.WriteMinorVersion(DsrMinorVersion.MV1);
			}
			CollectionWriter<DataShapeWriter> collectionWriter = this._writer.BeginDataShapes();
			global::System.ValueTuple<LimitsTelemetryInfo, DataWindowsTelemetryInfo> valueTuple = this.ProcessDataShape(dataShape, collectionWriter.BeginItem());
			LimitsTelemetryInfo item = valueTuple.Item1;
			DataWindowsTelemetryInfo item2 = valueTuple.Item2;
			this.ReportTelemetry(item, item2);
			collectionWriter.End();
			this._writer.End();
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00008764 File Offset: 0x00006964
		private void ReportTelemetry(LimitsTelemetryInfo limitsTelemetry, DataWindowsTelemetryInfo windowsTelemetry)
		{
			this._processingStats.LimitsTelemetry = limitsTelemetry;
			this._processingStats.WindowsTelemetry = windowsTelemetry;
			long totalCachedRowsCount = this._data.RowSourceManager.TotalCachedRowsCount;
			if (totalCachedRowsCount != 0L)
			{
				this._processingStats.CachedRowCount = totalCachedRowsCount;
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x000087AC File Offset: 0x000069AC
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "limitTelemetry", "dataWindowsTelemetry" })]
		internal global::System.ValueTuple<LimitsTelemetryInfo, DataWindowsTelemetryInfo> ProcessDataShape(Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataShape dataShape, DataShapeWriter writer)
		{
			this._data.EnterDataShape(dataShape);
			DataLimits dataLimits = dataShape.DataLimits;
			if (((dataLimits != null) ? dataLimits.Binding : null) != null)
			{
				this._data.SetupContextAndNextRow(this._evaluator, this._typeEvaluator, dataShape.DataLimits.Binding.TableIndex);
				DataWindows dataWindows = dataShape.DataWindows;
				if (((dataWindows != null) ? dataWindows.Binding : null) != null)
				{
					Contract.RetailAssert(dataShape.DataLimits.Binding.TableIndex == dataShape.DataWindows.Binding.TableIndex, "DataLimits and DataWindows must be on the same table.");
				}
			}
			else
			{
				DataWindows dataWindows2 = dataShape.DataWindows;
				if (((dataWindows2 != null) ? dataWindows2.Binding : null) != null)
				{
					this._data.SetupContextAndNextRow(this._evaluator, this._typeEvaluator, dataShape.DataWindows.Binding.TableIndex);
				}
			}
			writer.WriteId(dataShape.Id);
			if (dataShape.DataShapes != null)
			{
				this.ProcessDataShapes(dataShape.DataShapes, writer.BeginDataShapes());
			}
			if (dataShape.DataBinding != null)
			{
				this._data.SetupContextAndNextRow(this._evaluator, this._typeEvaluator, dataShape.DataBinding.TableIndex);
			}
			DataWindowsTelemetryInfo dataWindowsTelemetryInfo = this.SetupDataWindow(dataShape.DataWindow, dataShape.DataWindows, dataShape.PrimaryHierarchy);
			LimitsTelemetryInfo limitsTelemetryInfo = this.SetupDataLimits(dataShape.DataLimits);
			if (dataShape.Calculations != null)
			{
				this.ProcessCalculations(dataShape.Id, dataShape.Calculations, writer);
			}
			if (dataShape.SecondaryHierarchy != null)
			{
				this._data.CorrelationGovernor.StartInSecondaryHierarchy();
				DataShapeResultGenerator.ProcessCollection<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember, DataMemberWriter>(dataShape.SecondaryHierarchy, writer.BeginSecondaryHierarchy(), new Action<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember, CollectionWriter<DataMemberWriter>>(this.ProcessDataMember));
				this._data.CorrelationGovernor.StopInSecondaryHierarchy();
				this.SetupCorrelation(dataShape.CorrelationExpression);
			}
			if (dataShape.PrimaryHierarchy != null)
			{
				DataShapeResultGenerator.ProcessCollection<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember, DataMemberWriter>(dataShape.PrimaryHierarchy, writer.BeginPrimaryHierarchy(), new Action<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember, CollectionWriter<DataMemberWriter>>(this.ProcessDataMember));
			}
			bool flag = false;
			if (dataShape.DataLimits != null)
			{
				this.ProcessLimits(writer, out flag);
				this.UpdateLimitTelemetry(dataShape.DataLimits, limitsTelemetryInfo);
			}
			bool flag2 = false;
			if (dataShape.DataWindows != null)
			{
				this.ProcessDataWindows(dataShape, writer, out flag2);
				this.UpdateWindowTelemetry(dataShape.DataWindows, dataWindowsTelemetryInfo);
			}
			else
			{
				this.ProcessLegacyDataWindow(dataShape, writer, out flag2);
			}
			this.WriteHasAllData(dataShape, writer, flag2, flag);
			if (!dataShape.Messages.IsNullOrEmpty<Message>() || !this._data.RowSourceManager.Messages.IsNullOrEmpty<Message>())
			{
				this.ProcessMessages(dataShape, this._data.RowSourceManager.Messages, writer);
			}
			this.WriteValueDictionaries(this._dictEncodingCoordinator, dataShape.Id, writer, this._calcGenerator.Telemetry);
			this._processingStats.CalculationTelemetry = this._calcGenerator.Telemetry;
			writer.End();
			this.UpdateDataShapeTelemetry(dataShape, this._data.DataShapeTelemetry);
			this._data.ExitDataShape();
			return new global::System.ValueTuple<LimitsTelemetryInfo, DataWindowsTelemetryInfo>(limitsTelemetryInfo, dataWindowsTelemetryInfo);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00008A78 File Offset: 0x00006C78
		private void ProcessLegacyDataWindow(Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataShape dataShape, DataShapeWriter writer, out bool isComplete)
		{
			int? num = null;
			if (dataShape.DataWindow != null)
			{
				num = new int?(0);
			}
			isComplete = this.ComputeIsComplete(dataShape, num);
			writer.WriteIsComplete(isComplete);
			IList<IList<ExpressionNode>> restartDefinitions = dataShape.RestartDefinitions;
			if (!isComplete && restartDefinitions != null)
			{
				this._restartManager.WriteRestartTokens(restartDefinitions, writer.BeginRestartTokens());
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00008ACF File Offset: 0x00006CCF
		private void UpdateDataShapeTelemetry(Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataShape dataShape, ProcessingDataShapeTelemetry dsStats)
		{
			this._processingStats.UpdateDataShapeTelemetry(dataShape.Id, dsStats);
			bool skipNullIntersectionAndCalculations = this._options.SkipNullIntersectionAndCalculations;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00008AF0 File Offset: 0x00006CF0
		private void ProcessMessages(Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataShape dataShape, IReadOnlyList<Message> additionalMessages, DataShapeWriter writer)
		{
			CollectionWriter<MessageWriter> collectionWriter = writer.BeginMessages();
			if (!dataShape.Messages.IsNullOrEmpty<Message>())
			{
				DataShapeResultGenerator.ProcessCollectionInstance<Message, MessageWriter>(dataShape.Messages, collectionWriter, new Action<Message, MessageWriter>(this.ProcessMessage));
			}
			if (!additionalMessages.IsNullOrEmpty<Message>())
			{
				DataShapeResultGenerator.ProcessCollectionInstance<Message, MessageWriter>(additionalMessages, collectionWriter, new Action<Message, MessageWriter>(this.ProcessMessage));
			}
			collectionWriter.End();
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00008B4C File Offset: 0x00006D4C
		private void ProcessDataWindows(Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataShape dataShape, DataShapeWriter writer, out bool areComplete)
		{
			DataWindows dataWindows = dataShape.DataWindows;
			CollectionWriter<DataWindowWriter> collectionWriter = writer.BeginDataWindows();
			areComplete = true;
			for (int i = 0; i < dataWindows.Windows.Count; i++)
			{
				Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataWindow dataWindow = dataWindows.Windows[i];
				bool flag;
				this.ProcessDataWindow(dataShape, dataWindow, collectionWriter.BeginItem(), i, out flag);
				areComplete = areComplete && flag;
			}
			collectionWriter.End();
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00008BAB File Offset: 0x00006DAB
		private void WriteHasAllData(Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataShape dataShape, DataShapeWriter writer, bool windowsAreComplete, bool hasAnyLimitExceeded)
		{
			if (this._options.Version != DsrVersion.V1 && (!this._restartKindGenerator.HasRestartKind && !hasAnyLimitExceeded && windowsAreComplete) && this.HasNoStartPositions(dataShape))
			{
				writer.WriteHasAllData();
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00008BE4 File Offset: 0x00006DE4
		private bool HasNoStartPositions(Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataShape dataShape)
		{
			bool flag = true;
			if (dataShape.PrimaryHierarchy != null)
			{
				flag = dataShape.PrimaryHierarchy.All((Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember m) => !m.HasStartPosition && !m.HasAnyDescendantWithStartPosition);
			}
			bool flag2 = true;
			if (dataShape.SecondaryHierarchy != null)
			{
				flag2 = dataShape.SecondaryHierarchy.All((Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember m) => !m.HasStartPosition && !m.HasAnyDescendantWithStartPosition);
			}
			return flag && flag2;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00008C60 File Offset: 0x00006E60
		internal bool EnterDataMember(Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember dataMember, out DataContextToken pipelineToken, out DataWindowToken dataWindowToken)
		{
			WindowConstraintMode? windowConstraintMode = null;
			if (!dataMember.IsDynamic && !dataMember.IsCountedForDataWindow && !this._data.WindowGovernor.HasCapacityForMember(dataMember))
			{
				windowConstraintMode = new WindowConstraintMode?(WindowConstraintMode.Relaxed);
			}
			dataWindowToken = this._data.WindowGovernor.Update(dataMember, windowConstraintMode);
			bool flag = this._data.SetupDataContext(dataMember.DataBinding, this._evaluator, this._typeEvaluator, out pipelineToken);
			if (dataMember.MatchCondition != null)
			{
				this._data.MatchConditions.PushCondition(dataMember.MatchCondition.Field.FieldIndex, dataMember.MatchCondition.Value);
			}
			if (dataMember.DiscardCondition != null)
			{
				this._data.MatchConditions.PushDiscardCondition(dataMember.DiscardCondition.Field.FieldIndex, dataMember.DiscardCondition.Value, dataMember.DiscardCondition.Operator);
			}
			bool flag2 = flag;
			if (flag)
			{
				flag2 = this._data.SetupNextRow(this._evaluator);
			}
			return flag2;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00008D68 File Offset: 0x00006F68
		internal void ExitDataMember(Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember dataMember, in DataContextToken token, bool isNextRowReady, DataWindowToken dataWindowToken)
		{
			if (dataMember.MatchCondition != null)
			{
				this._data.MatchConditions.PopCondition();
				if (isNextRowReady)
				{
					this._data.ClearActiveRow();
				}
			}
			if (dataMember.DiscardCondition != null)
			{
				this._data.MatchConditions.PopDiscardCondition();
			}
			this._data.RestoreDataContext(token, this._evaluator);
			if (!this._data.CorrelationGovernor.IsInSecondaryHierarchy)
			{
				this._segmentationManager.ExitMember(dataMember);
			}
			this._data.WindowGovernor.Restore(in dataWindowToken);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00008DFC File Offset: 0x00006FFC
		private bool ShouldSkipDataMember(Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember dataMember, bool isNextRowReady)
		{
			return this._data.WindowGovernor.HasExplicitlyExceededCapacity() || (!isNextRowReady && !this._data.WindowGovernor.HasCapacity) || (!this._data.CorrelationGovernor.IsInSecondaryHierarchy && !this._segmentationManager.ShouldOutput(dataMember, isNextRowReady)) || (!dataMember.IsDynamic && this._data.LimitGovernor.IsConstrainedByLimitWithoutCapacity(dataMember));
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00008E78 File Offset: 0x00007078
		internal void ProcessDataMember(Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember dataMember, CollectionWriter<DataMemberWriter> collectionWriter)
		{
			DataContextToken dataContextToken;
			DataWindowToken dataWindowToken;
			bool isNextRowReady = this.EnterDataMember(dataMember, out dataContextToken, out dataWindowToken);
			if (this.ShouldSkipDataMember(dataMember, isNextRowReady))
			{
				if (isNextRowReady && dataMember.Intersections != null)
				{
					do
					{
						this._data.ClearActiveRow();
					}
					while (this._data.SetupNextRow(this._evaluator));
				}
				this.ExitDataMember(dataMember, in dataContextToken, isNextRowReady, dataWindowToken);
				return;
			}
			DataMemberWriter dataMemberWriter = collectionWriter.BeginItem();
			CollectionWriter<DataMemberInstanceWriter> instancesWriter;
			if (this._options.DataMemberIdAsPropertyName)
			{
				instancesWriter = dataMemberWriter.BeginInstances(dataMember.Id);
			}
			else
			{
				dataMemberWriter.WriteId(dataMember.Id);
				instancesWriter = dataMemberWriter.BeginInstances();
			}
			if (dataMember.IsDynamic)
			{
				if (isNextRowReady)
				{
					new RuntimeGrouping(dataMember).Process(this._data, this._evaluator, delegate
					{
						this.ProcessDataMemberInstance(dataMember, instancesWriter.BeginItem(), false, isNextRowReady);
						DataShapeResultGenerator.UpdateDataWindowAndLimits(this._data, dataMember, this._evaluator, this._restartManager, this._tableMetadata);
					});
				}
			}
			else if (isNextRowReady)
			{
				Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember activeMember = this._data.LimitGovernor.ActiveMember;
				this._data.LimitGovernor.ActiveMember = dataMember;
				this.ProcessDataMemberInstance(dataMember, instancesWriter.BeginItem(), false, isNextRowReady);
				DataShapeResultGenerator.UpdateDataWindowAndLimits(this._data, dataMember, this._evaluator, this._restartManager, this._tableMetadata);
				this._data.LimitGovernor.ActiveMember = activeMember;
			}
			else
			{
				this.ProcessEmptyDataMemberInstance(dataMember, instancesWriter.BeginItem());
			}
			instancesWriter.End();
			dataMemberWriter.End();
			this.ExitDataMember(dataMember, in dataContextToken, isNextRowReady, dataWindowToken);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00009060 File Offset: 0x00007260
		internal void ProcessDataMemberInstance(Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember dataMember, DataMemberInstanceWriter writer, bool allowEmptyRow, bool isRowReady)
		{
			this._data.CorrelationGovernor.EnterMemberInstance(dataMember, this.GetRowIndex());
			if (this._restartManager != null && !this._data.CorrelationGovernor.IsInSecondaryHierarchy)
			{
				writer.WriteRestartFlag(this._segmentationManager.GetRestartFlag(dataMember, this._evaluator));
			}
			Microsoft.DataShaping.InternalContracts.DataShapeResultWriter.RestartKind restartKind = this._restartKindGenerator.GetRestartKind(dataMember);
			if (restartKind != Microsoft.DataShaping.InternalContracts.DataShapeResultWriter.RestartKind.None)
			{
				writer.WriteRestartKind(restartKind);
			}
			if (dataMember.Group != null && dataMember.Group.ScopeIdDefinition != null)
			{
				this.ProcessGroup(dataMember.Group, writer.BeginGroup());
			}
			if (dataMember.Calculations != null)
			{
				this.ProcessCalculations(dataMember.Id, dataMember.Calculations, writer);
			}
			if (dataMember.DataMembers != null)
			{
				DataShapeResultGenerator.ProcessCollection<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember, DataMemberWriter>(dataMember.DataMembers, writer.BeginDataMembers(), new Action<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember, CollectionWriter<DataMemberWriter>>(this.ProcessDataMember));
			}
			if (dataMember.Intersections != null)
			{
				bool flag = !dataMember.IsDynamic && dataMember.HasAllEmptyIntersections && this._data.MatchConditions.ConditionCount == 0 && this._dsd.DataShape.HasTopLevelPrimaryHierarchyGroup;
				this.ProcessRow(dataMember.Intersections, writer.BeginIntersections(), flag, allowEmptyRow, isRowReady);
			}
			writer.End();
			this._data.CorrelationGovernor.ExitMemberInstance(dataMember);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x000091A0 File Offset: 0x000073A0
		private void ProcessEmptyDataMemberInstance(Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember dataMember, DataMemberInstanceWriter writer)
		{
			IDataRow activeRow = this._evaluator.ActiveRow;
			int activeTableIndex = this._evaluator.ActiveTableIndex;
			this._evaluator.SetActiveRow(null, activeTableIndex);
			this.ProcessDataMemberInstance(dataMember, writer, true, false);
			this._evaluator.SetActiveRow(activeRow, activeTableIndex);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000091EC File Offset: 0x000073EC
		internal void ProcessGroup(Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Group group, GroupWriter writer)
		{
			ScopeIdWriter scopeIdWriter = writer.BeginScopeId();
			DataShapeResultGenerator.ProcessCollection<ScopeKey, ScopeValueWriter>(group.ScopeIdDefinition.ScopeKeys, scopeIdWriter.BeginScopeValues(), new Action<ScopeKey, ScopeValueWriter>(this.ProcessScopeKey));
			scopeIdWriter.End();
			writer.End();
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00009230 File Offset: 0x00007430
		private void ProcessScopeKey(ScopeKey scopeKey, ScopeValueWriter writer)
		{
			object obj = this._evaluator.Evaluate(scopeKey.Value);
			writer.WriteValue(obj);
			string key = this._keyGenerator.GetKey(obj);
			writer.WriteKey(key);
			writer.End();
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00009270 File Offset: 0x00007470
		private void ProcessRow(IList<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataIntersection> collection, CollectionWriter<DataIntersectionWriter> writer, bool writeEmptyRow, bool allowEmptyRow, bool isRowReady)
		{
			if (this._data.CorrelationGovernor.IsCorrelationEnabled)
			{
				Action<IndexRange, int, bool> action = delegate(IndexRange range, int currentColumnIndex, bool hadGap)
				{
					DataShapeResultGenerator.ProcessCollectionInstance<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataIntersection, DataIntersectionWriter>(collection, range, currentColumnIndex, hadGap, writer, new Action<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataIntersection, DataIntersectionWriter, int, bool>(this.ProcessDataIntersection));
				};
				if (writeEmptyRow)
				{
					if (!this._options.SkipNullIntersectionAndCalculations)
					{
						RuntimeRow.ProcessEmptyRow(this._data, this._evaluator, action);
					}
				}
				else
				{
					if (this._restartManager != null)
					{
						this._restartManager.SetLastRowRead(this._evaluator.ActiveRow, this._data.ActiveContextIndex, allowEmptyRow);
					}
					RuntimeRow.Process(this._data, this._evaluator, action, collection, this._options.SkipNullIntersectionAndCalculations, this._visitedIntersectionIds, isRowReady);
				}
			}
			else
			{
				for (int i = 0; i < collection.Count; i++)
				{
					this.ProcessDataIntersection(collection[i], writer.BeginItem(), i, false);
				}
			}
			writer.End();
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000937C File Offset: 0x0000757C
		internal void ProcessDataIntersection(Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataIntersection dataIntersection, DataIntersectionWriter writer, int currentColumnIndex, bool hadGap)
		{
			this._data.DataShapeTelemetry.IncrementIntersectionCount();
			if (!this._options.SkipIntersectionIds)
			{
				writer.WriteId(dataIntersection.Id);
			}
			if (hadGap && this._options.SkipNullIntersectionAndCalculations)
			{
				writer.WriteIndex(currentColumnIndex);
			}
			IList<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Calculation> calculations = dataIntersection.Calculations;
			if (calculations != null)
			{
				this.ProcessCalculations(dataIntersection.Id, calculations, writer);
			}
			if (dataIntersection.DataShapes != null)
			{
				int num = 0;
				CorrelationGovernor correlationGovernor = this._data.CorrelationGovernor;
				if (correlationGovernor.IsCorrelationEnabled)
				{
					num = correlationGovernor.PushCorrelationMatchConditions(this._data.MatchConditions, dataIntersection.DataBinding, currentColumnIndex);
				}
				this.ProcessDataShapes(dataIntersection.DataShapes, writer.BeginDataShapes());
				for (int i = 0; i < num; i++)
				{
					this._data.MatchConditions.PopCondition();
				}
			}
			writer.End();
			if (this._evaluator.HasActiveRow)
			{
				this._data.LimitGovernor.ExitDataIntersectionInstance(dataIntersection);
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000946C File Offset: 0x0000766C
		internal void ProcessDataShapes(IList<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataShape> dataShapes, CollectionWriter<DataShapeWriter> writer)
		{
			int activeContextIndex = this._data.ActiveContextIndex;
			DataShapeResultGenerator.ProcessCollection<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataShape, DataShapeWriter>(dataShapes, writer, new Action<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataShape, DataShapeWriter>(this.ProcessNestedDataShape));
			if (this._data.IsValidContextIndex(activeContextIndex))
			{
				this._data.RestoreContext(activeContextIndex, this._evaluator);
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x000094B9 File Offset: 0x000076B9
		internal void ProcessNestedDataShape(Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataShape dataShape, DataShapeWriter writer)
		{
			this.ProcessDataShape(dataShape, writer);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000094C4 File Offset: 0x000076C4
		internal void ProcessCalculations(string parentId, IList<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Calculation> calculations, ICalculationContainerWriter writer)
		{
			this._calcGenerator.Process(parentId, calculations, writer);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x000094D4 File Offset: 0x000076D4
		internal static void UpdateDataWindowAndLimits(DataPipeline dataPipeline, Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember dataMember, ExpressionEvaluator evaluator, RestartManager restartManager, ResultTableMetadataManager tableMetadata)
		{
			if (!tableMetadata.CountsForLimiting(evaluator.ActiveRow, dataPipeline.ActiveContextIndex))
			{
				return;
			}
			dataPipeline.LimitGovernor.ExitInstance(dataMember);
			if (!dataPipeline.CorrelationGovernor.IsInSecondaryHierarchy)
			{
				dataPipeline.WindowGovernor.ExitInstance(dataMember);
				if (restartManager != null)
				{
					restartManager.SetLastRowRead(evaluator.ActiveRow, dataPipeline.ActiveContextIndex, true);
				}
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00009538 File Offset: 0x00007738
		private void ProcessLimits(DataShapeWriter writer, out bool hasAnyLimitExceeded)
		{
			hasAnyLimitExceeded = false;
			CollectionWriter<DataLimitWriter> collectionWriter = null;
			List<DataPipelineLimit> limits = this._data.LimitGovernor.Limits;
			for (int i = 0; i < limits.Count; i++)
			{
				DataPipelineLimit dataPipelineLimit = limits[i];
				if (dataPipelineLimit.IsExceededByAnyInstance)
				{
					hasAnyLimitExceeded = true;
					if (collectionWriter == null)
					{
						collectionWriter = writer.BeginDataLimits();
					}
					DataLimitWriter dataLimitWriter = collectionWriter.BeginItem();
					dataLimitWriter.WriteId(dataPipelineLimit.Id);
					dataLimitWriter.End();
				}
			}
			if (collectionWriter != null)
			{
				collectionWriter.End();
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x000095AC File Offset: 0x000077AC
		private void UpdateLimitTelemetry(DataLimits limitDefinitions, LimitsTelemetryInfo limitsTelemetry)
		{
			IList<DataLimit> limits = limitDefinitions.Limits;
			DataLimitGovernor limitGovernor = this._data.LimitGovernor;
			for (int i = 0; i < limits.Count; i++)
			{
				DataLimit dataLimit = limits[i];
				DataPipelineLimit limit = limitGovernor.GetLimit(dataLimit.Id);
				LimitTelemetryInfo limitTelemetryInfo = limitsTelemetry.LimitTelemetry[i];
				limitTelemetryInfo.Populate(dataLimit);
				limitTelemetryInfo.Populate(limit);
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00009610 File Offset: 0x00007810
		private void UpdateWindowTelemetry(DataWindows dataWindows, DataWindowsTelemetryInfo windowsTelemetry)
		{
			IReadOnlyList<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataWindow> windows = dataWindows.Windows;
			DataWindowGovernor windowGovernor = this._data.WindowGovernor;
			for (int i = 0; i < windows.Count; i++)
			{
				Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataWindow dataWindow = windows[i];
				DataPipelineWindow window = windowGovernor.GetWindow(i);
				DataWindowTelemetryInfo dataWindowTelemetryInfo = windowsTelemetry.WindowTelemetry[i];
				dataWindowTelemetryInfo.Populate(dataWindow);
				dataWindowTelemetryInfo.Populate(window);
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000966C File Offset: 0x0000786C
		internal void ProcessMessage(Message message, MessageWriter writer)
		{
			writer.WriteCode(message.Code);
			writer.WriteSeverity(message.Severity);
			writer.WriteMessage(message.MessageText.RemovePrivateAndInternalMarkup());
			writer.WriteObjectType(message.ObjectType);
			writer.WriteObjectName(message.ObjectName.RemovePrivateAndInternalMarkup());
			writer.WritePropertyName(message.PropertyName.RemovePrivateAndInternalMarkup());
			writer.End();
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x000096D8 File Offset: 0x000078D8
		internal void ProcessDataWindow(Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataShape dataShape, Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataWindow window, DataWindowWriter windowWriter, int index, out bool isComplete)
		{
			windowWriter.WriteId(window.Id);
			isComplete = this.ComputeIsComplete(dataShape, new int?(index));
			windowWriter.WriteIsComplete(isComplete);
			if (!isComplete && window.RestartDefinitions != null)
			{
				this._restartManager.WriteRestartTokens(window.RestartDefinitions, windowWriter.BeginRestartTokens());
			}
			windowWriter.End();
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00009738 File Offset: 0x00007938
		internal DataWindowsTelemetryInfo SetupDataWindow(Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataWindow legacyWindow, DataWindows windows, IList<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember> primaryHierarchy)
		{
			if (legacyWindow != null || windows != null)
			{
				if (legacyWindow == null)
				{
					List<DataWindowTelemetryInfo> list = new List<DataWindowTelemetryInfo>();
					foreach (Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataWindow dataWindow in windows.Windows)
					{
						DataWindowTelemetryInfo dataWindowTelemetryInfo = new DataWindowTelemetryInfo();
						list.Add(dataWindowTelemetryInfo);
						int num = Convert.ToInt32(this._evaluator.Evaluate(dataWindow.Count), CultureInfo.InvariantCulture);
						int? num2 = null;
						if (dataWindow.IsExceededDbCount != null)
						{
							num2 = new int?(Convert.ToInt32(this._evaluator.Evaluate(dataWindow.IsExceededDbCount), CultureInfo.InvariantCulture));
						}
						if (dataWindow.DbCount != null)
						{
							int num3 = Convert.ToInt32(this._evaluator.Evaluate(dataWindow.DbCount), CultureInfo.InvariantCulture);
							dataWindowTelemetryInfo.DbCount = num3;
						}
						this._data.WindowGovernor.AddWindow(dataWindow, num, num2);
					}
					return new DataWindowsTelemetryInfo
					{
						WindowTelemetry = list
					};
				}
				int num4 = Convert.ToInt32(this._evaluator.Evaluate(legacyWindow.Count), CultureInfo.InvariantCulture);
				this._data.WindowGovernor.AddWindow(legacyWindow, num4, null);
			}
			return null;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00009888 File Offset: 0x00007A88
		internal LimitsTelemetryInfo SetupDataLimits(DataLimits limits)
		{
			if (limits == null)
			{
				return null;
			}
			List<LimitTelemetryInfo> list = new List<LimitTelemetryInfo>(limits.Limits.Count);
			for (int i = 0; i < limits.Limits.Count; i++)
			{
				DataLimit dataLimit = limits.Limits[i];
				LimitTelemetryInfo limitTelemetryInfo = new LimitTelemetryInfo();
				list.Add(limitTelemetryInfo);
				DataLimitOperator limitOperator = dataLimit.LimitOperator;
				int num = Convert.ToInt32(this._evaluator.Evaluate(limitOperator.Count), CultureInfo.InvariantCulture);
				int? num2 = null;
				if (limitOperator.DbCount != null)
				{
					num2 = new int?(Convert.ToInt32(this._evaluator.Evaluate(limitOperator.DbCount), CultureInfo.InvariantCulture));
					limitTelemetryInfo.DbCount = num2.Value;
				}
				int? num3 = null;
				if (limitOperator.IsExceededDbCount != null)
				{
					num3 = new int?(Convert.ToInt32(this._evaluator.Evaluate(limitOperator.IsExceededDbCount), CultureInfo.InvariantCulture));
				}
				int? num4 = null;
				if (limitOperator.WarningCount != null)
				{
					num4 = new int?(Convert.ToInt32(this._evaluator.Evaluate(limitOperator.WarningCount), CultureInfo.InvariantCulture));
					limitTelemetryInfo.WarningCount = num4.Value;
				}
				this._data.LimitGovernor.AddLimit(dataLimit, num, num3, num4);
			}
			Dictionary<string, object> dictionary = null;
			if (limits.TelemetryItems != null)
			{
				dictionary = limits.TelemetryItems.Evaluate(this._evaluator);
			}
			return new LimitsTelemetryInfo
			{
				LimitTelemetry = list,
				AdditionalInfo = dictionary
			};
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00009A08 File Offset: 0x00007C08
		private void SetupCorrelation(FieldValueExpressionNode correlationExpression)
		{
			int num = ((correlationExpression != null) ? correlationExpression.FieldIndex : (-1));
			this._data.SetCorrelationInfo(num);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00009A2E File Offset: 0x00007C2E
		private long GetRowIndex()
		{
			return this._tableMetadata.GetRowIndex(this._evaluator.ActiveRow, this._data.ActiveContextIndex);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00009A51 File Offset: 0x00007C51
		private bool ComputeIsComplete(Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataShape dataShape, int? windowIndex)
		{
			return (windowIndex == null || this._data.WindowGovernor.GetWindow(windowIndex.Value).IsComplete) && this._data.AreResultSetsComplete(dataShape.SegmentationTableIndices);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00009A90 File Offset: 0x00007C90
		private void WriteValueDictionaries(DictionaryEncodingCoordinator dictEncoding, string dataShapeId, DataShapeWriter writer, CalculationGenerationTelemetry calcTelemetry)
		{
			if (dictEncoding == null || dictEncoding.DictionaryEncodings.Count == 0)
			{
				return;
			}
			if (dataShapeId != this._rootDataShapeId)
			{
				return;
			}
			DictionaryValuesWriter dictionaryValuesWriter = writer.BeginValueDictionaries();
			foreach (DsrValuesDictionaryBuilder dsrValuesDictionaryBuilder in dictEncoding.DictionaryEncodings)
			{
				dictionaryValuesWriter.WriteValues(dsrValuesDictionaryBuilder.Id, dsrValuesDictionaryBuilder.IsVariant, dsrValuesDictionaryBuilder.Values);
				if (calcTelemetry != null)
				{
					calcTelemetry.AddDictionaryInfo(dsrValuesDictionaryBuilder.Id, dsrValuesDictionaryBuilder.Values.Count, dsrValuesDictionaryBuilder.HitCount, dsrValuesDictionaryBuilder.MissCount);
				}
			}
			dictionaryValuesWriter.End();
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00009B44 File Offset: 0x00007D44
		private DictionaryEncodingCoordinator CreateDictionaryEncodingCoordinator(DsrWriterOptions options, ResultEncodingHints encodingHints)
		{
			if (!options.WriteCalcsDictionaryEncoded || encodingHints.DisableDictionaryEncoding)
			{
				return null;
			}
			return new DictionaryEncodingCoordinator(options.Names.DictionaryIdPrefix, options.DictionaryEncodingCapacity, encodingHints.DictionaryEncodingExcludeList, encodingHints.CalculationsWithSharedValues);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00009B7A File Offset: 0x00007D7A
		private static void ProcessCollection<TItem, TWriter>(IList<TItem> collection, CollectionWriter<TWriter> writer, Action<TItem, TWriter> action) where TWriter : StreamingDsrWriterWrapperBase, new()
		{
			DataShapeResultGenerator.ProcessCollectionInstance<TItem, TWriter>(collection, writer, action);
			writer.End();
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00009B8C File Offset: 0x00007D8C
		private static void ProcessCollectionInstance<TItem, TWriter>(IReadOnlyList<TItem> collection, CollectionWriter<TWriter> writer, Action<TItem, TWriter> action) where TWriter : StreamingDsrWriterWrapperBase, new()
		{
			for (int i = 0; i < collection.Count; i++)
			{
				TItem titem = collection[i];
				action(titem, writer.BeginItem());
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00009BC0 File Offset: 0x00007DC0
		private static void ProcessCollectionInstance<TItem, TWriter>(IList<TItem> collection, CollectionWriter<TWriter> writer, Action<TItem, TWriter> action) where TWriter : StreamingDsrWriterWrapperBase, new()
		{
			for (int i = 0; i < collection.Count; i++)
			{
				TItem titem = collection[i];
				action(titem, writer.BeginItem());
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00009BF4 File Offset: 0x00007DF4
		private static void ProcessCollectionInstance<TItem, TWriter>(IList<TItem> collection, CollectionWriter<TWriter> writer, Action<TItem, CollectionWriter<TWriter>> action) where TWriter : StreamingDsrWriterWrapperBase, new()
		{
			for (int i = 0; i < collection.Count; i++)
			{
				TItem titem = collection[i];
				action(titem, writer);
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00009C22 File Offset: 0x00007E22
		private static void ProcessCollection<TItem, TWriter>(IList<TItem> collection, CollectionWriter<TWriter> writer, Action<TItem, CollectionWriter<TWriter>> action) where TWriter : StreamingDsrWriterWrapperBase, new()
		{
			DataShapeResultGenerator.ProcessCollectionInstance<TItem, TWriter>(collection, writer, action);
			writer.End();
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00009C34 File Offset: 0x00007E34
		private static void ProcessCollectionInstance<TItem, TWriter>(IList<TItem> collection, IndexRange indexRange, int currentColumnIndex, bool hadGap, CollectionWriter<TWriter> writer, Action<TItem, TWriter, int, bool> action) where TWriter : StreamingDsrWriterWrapperBase, new()
		{
			int start = indexRange.Start;
			int end = indexRange.End;
			for (int i = start; i <= end; i++)
			{
				action(collection[i], writer.BeginItem(), currentColumnIndex, hadGap);
			}
		}

		// Token: 0x040001A2 RID: 418
		private readonly Microsoft.DataShaping.ServiceContracts.ITelemetryService _telemetryService;

		// Token: 0x040001A3 RID: 419
		private readonly DataShapeDefinition _dsd;

		// Token: 0x040001A4 RID: 420
		private readonly DataShapeResultWriter _writer;

		// Token: 0x040001A5 RID: 421
		private readonly IKeyGenerator _keyGenerator;

		// Token: 0x040001A6 RID: 422
		private readonly IDataComparer _comparer;

		// Token: 0x040001A7 RID: 423
		private readonly ExpressionEvaluator _evaluator;

		// Token: 0x040001A8 RID: 424
		private readonly RestartKindGenerator _restartKindGenerator;

		// Token: 0x040001A9 RID: 425
		private readonly ExpressionTypeEvaluator _typeEvaluator;

		// Token: 0x040001AA RID: 426
		private readonly ProcessingTelemetry _processingStats;

		// Token: 0x040001AB RID: 427
		private readonly ResultTableMetadataManager _tableMetadata;

		// Token: 0x040001AC RID: 428
		private readonly CalculationGenerator _calcGenerator;

		// Token: 0x040001AD RID: 429
		private readonly DsrWriterOptions _options;

		// Token: 0x040001AE RID: 430
		private readonly DictionaryEncodingCoordinator _dictEncodingCoordinator;

		// Token: 0x040001AF RID: 431
		private readonly string _rootDataShapeId;

		// Token: 0x040001B0 RID: 432
		private readonly ISet<string> _visitedIntersectionIds = new HashSet<string>(StringComparer.Ordinal);

		// Token: 0x040001B1 RID: 433
		private SegmentationManager _segmentationManager;

		// Token: 0x040001B2 RID: 434
		private RestartManager _restartManager;

		// Token: 0x040001B3 RID: 435
		private DataPipeline _data;
	}
}
