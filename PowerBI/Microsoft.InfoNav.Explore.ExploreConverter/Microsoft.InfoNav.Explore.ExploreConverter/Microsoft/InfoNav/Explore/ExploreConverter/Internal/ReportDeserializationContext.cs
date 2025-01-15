using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000089 RID: 137
	internal sealed class ReportDeserializationContext : IReportDeserializationContext
	{
		// Token: 0x060002AE RID: 686 RVA: 0x0000C8D7 File Offset: 0x0000AAD7
		internal ReportDeserializationContext(PVDocumentRoot document, RdmReport report, Dictionary<PVVisual, Tuple<ReportItem, IRdlReportItemConverter>> visualMap)
		{
			this._document = document;
			this._report = report;
			this._dataSetScopes = new Stack<DataSet>();
			this._visuals = visualMap;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000C900 File Offset: 0x0000AB00
		public DataSet GetDataSetByName(string dataSetName)
		{
			DataSet dataSet = null;
			if (!string.IsNullOrEmpty(dataSetName))
			{
				dataSet = this._report.FindDataSet(dataSetName);
			}
			Contract.Check(dataSet != null, "Expect the DataSet to exist");
			return dataSet;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000C933 File Offset: 0x0000AB33
		public void PushDataScope(DataRegion dataRegion)
		{
			this.PushDataScope(dataRegion.DataSetName);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000C944 File Offset: 0x0000AB44
		public void PushDataScope(Group group)
		{
			string text = string.Empty;
			if (group != null)
			{
				text = group.DataSetName;
			}
			this.PushDataScope(text);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000C968 File Offset: 0x0000AB68
		public void PushDataScope(LabelData labelData)
		{
			this.PushDataScope(labelData.DataSetName);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000C978 File Offset: 0x0000AB78
		public void PushDataScope(string dataSetName)
		{
			if (!string.IsNullOrEmpty(dataSetName))
			{
				DataSet dataSet = this._report.FindDataSet(dataSetName);
				Contract.Check(dataSet != null, "Expect the DataSet to exist");
				this._dataSetScopes.Push(dataSet);
				return;
			}
			Contract.Check(this._dataSetScopes.Count != 0, "Expect the DataSetName to exist");
			this._dataSetScopes.Push(this._dataSetScopes.First<DataSet>());
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000C9E3 File Offset: 0x0000ABE3
		public void PopDataScope()
		{
			this._dataSetScopes.Pop();
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000C9F1 File Offset: 0x0000ABF1
		public void RegisterReportSectionState(ReportSectionState reportSectionState)
		{
			Contract.Check(this._currentReportSectionState == null, "Expect the ReportSectionState to be unregistered before it is registered again.");
			this._currentReportSectionState = reportSectionState;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000CA0D File Offset: 0x0000AC0D
		public void UnregisterReportSectionState()
		{
			Contract.Check(this._currentReportSectionState != null, "Expect the ReportSectionState to be registered before it is unregistered.");
			this._currentReportSectionState = null;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000CA29 File Offset: 0x0000AC29
		public ReportSectionState GetCurrentReportSectionState()
		{
			return this._currentReportSectionState;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000CA31 File Offset: 0x0000AC31
		public DataSet GetCurrentDataSet()
		{
			return this._dataSetScopes.First<DataSet>();
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000CA3E File Offset: 0x0000AC3E
		public void Register(PVVisual visual, Tuple<ReportItem, IRdlReportItemConverter> context)
		{
			this._visuals.Add(visual, context);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000CA4D File Offset: 0x0000AC4D
		public Tuple<ReportItem, IRdlReportItemConverter> GetCreationContext(PVVisual visual)
		{
			return this._visuals[visual];
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000CA5C File Offset: 0x0000AC5C
		public Field FindDataSetField(string fieldName)
		{
			Field field = null;
			foreach (DataSet dataSet in this._dataSetScopes)
			{
				field = dataSet.FindField(fieldName);
				if (field != null && field.Expression != null)
				{
					break;
				}
			}
			return field;
		}

		// Token: 0x040001B9 RID: 441
		private readonly PVDocumentRoot _document;

		// Token: 0x040001BA RID: 442
		private readonly RdmReport _report;

		// Token: 0x040001BB RID: 443
		private readonly Dictionary<PVVisual, Tuple<ReportItem, IRdlReportItemConverter>> _visuals;

		// Token: 0x040001BC RID: 444
		private readonly Stack<DataSet> _dataSetScopes;

		// Token: 0x040001BD RID: 445
		private ReportSectionState _currentReportSectionState;
	}
}
