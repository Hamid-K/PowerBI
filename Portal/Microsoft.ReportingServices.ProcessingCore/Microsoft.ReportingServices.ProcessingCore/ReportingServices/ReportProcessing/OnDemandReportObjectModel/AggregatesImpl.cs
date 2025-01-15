using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel
{
	// Token: 0x020007BB RID: 1979
	public sealed class AggregatesImpl : Aggregates, IStaticReferenceable
	{
		// Token: 0x0600703F RID: 28735 RVA: 0x001D3C7E File Offset: 0x001D1E7E
		internal AggregatesImpl()
		{
		}

		// Token: 0x06007040 RID: 28736 RVA: 0x001D3C91 File Offset: 0x001D1E91
		internal AggregatesImpl(OnDemandProcessingContext odpContext)
			: this(false, odpContext)
		{
		}

		// Token: 0x06007041 RID: 28737 RVA: 0x001D3C9B File Offset: 0x001D1E9B
		internal AggregatesImpl(bool lockAdd, OnDemandProcessingContext odpContext)
		{
			this.m_lockAdd = lockAdd;
			this.m_odpContext = odpContext;
			this.ClearAll();
		}

		// Token: 0x17002642 RID: 9794
		public override object this[string key]
		{
			get
			{
				DataAggregateObj dataAggregateObj = this.GetAggregateObj(key);
				if (dataAggregateObj == null)
				{
					if (this.m_odpContext.IsTablixProcessingMode)
					{
						this.m_odpContext.ReportRuntime.UnfulfilledDependency = true;
					}
					if (!this.m_odpContext.CalculateAggregate(key))
					{
						return null;
					}
					dataAggregateObj = this.GetAggregateObj(key);
					Global.Tracer.Assert(dataAggregateObj != null, "(null != aggregateObj)");
				}
				object obj = this.GetAggregateValue(key, dataAggregateObj);
				if (obj == null && dataAggregateObj.AggregateDef.AggregateType == DataAggregateInfo.AggregateTypes.Aggregate && this.m_odpContext.StreamingMode && this.m_odpContext.StateManager.CheckForPrematureServerAggregate(key))
				{
					obj = this.GetAggregateValue(key, this.GetAggregateObj(key));
				}
				return obj;
			}
		}

		// Token: 0x06007043 RID: 28739 RVA: 0x001D3D74 File Offset: 0x001D1F74
		private object GetAggregateValue(string key, DataAggregateObj aggregateObj)
		{
			aggregateObj.UsedInExpression = true;
			DataAggregateObjResult dataAggregateObjResult = aggregateObj.AggregateResult();
			if (dataAggregateObjResult == null)
			{
				Global.Tracer.Assert(this.m_odpContext.IsTablixProcessingMode, "Missing aggregate result outside of tablix processing");
				throw new ReportProcessingException_MissingAggregateDependency();
			}
			if (dataAggregateObjResult.HasCode)
			{
				if ((dataAggregateObjResult.FieldStatus == DataFieldStatus.None || dataAggregateObjResult.FieldStatus == DataFieldStatus.IsError) && dataAggregateObjResult.Code != ProcessingErrorCode.rsNone)
				{
					this.ErrorContext.Register(dataAggregateObjResult.Code, dataAggregateObjResult.Severity, dataAggregateObjResult.Arguments);
				}
				else if (dataAggregateObjResult.FieldStatus == DataFieldStatus.UnSupportedDataType)
				{
					this.ErrorContext.Register(ProcessingErrorCode.rsAggregateOfInvalidExpressionDataType, Severity.Warning, dataAggregateObjResult.Arguments);
				}
				if (dataAggregateObjResult.ErrorOccurred)
				{
					throw new ReportProcessingException_InvalidOperationException();
				}
			}
			if (dataAggregateObjResult.ErrorOccurred)
			{
				throw new ReportProcessingException(ErrorCode.rsInvalidOperation);
			}
			return dataAggregateObjResult.Value;
		}

		// Token: 0x17002643 RID: 9795
		// (get) Token: 0x06007044 RID: 28740 RVA: 0x001D3E39 File Offset: 0x001D2039
		private IErrorContext ErrorContext
		{
			get
			{
				return this.m_odpContext.ReportRuntime;
			}
		}

		// Token: 0x17002644 RID: 9796
		// (get) Token: 0x06007045 RID: 28741 RVA: 0x001D3E46 File Offset: 0x001D2046
		internal ICollection Objects
		{
			get
			{
				return this.m_collection.Values;
			}
		}

		// Token: 0x06007046 RID: 28742 RVA: 0x001D3E53 File Offset: 0x001D2053
		internal void ClearAll()
		{
			if (this.m_collection != null)
			{
				this.m_collection.Clear();
			}
			else
			{
				this.m_collection = new Hashtable();
			}
			this.m_duplicateNames = null;
		}

		// Token: 0x06007047 RID: 28743 RVA: 0x001D3E7C File Offset: 0x001D207C
		internal void ResetAll()
		{
			foreach (object obj in this.m_collection.Values)
			{
				((DataAggregateObj)obj).Init();
			}
		}

		// Token: 0x06007048 RID: 28744 RVA: 0x001D3ED8 File Offset: 0x001D20D8
		internal void ResetAll<T>(IEnumerable<T> aggregateDefs) where T : DataAggregateInfo
		{
			if (aggregateDefs == null)
			{
				return;
			}
			foreach (T t in aggregateDefs)
			{
				DataAggregateInfo dataAggregateInfo = t;
				this.Reset(dataAggregateInfo);
			}
		}

		// Token: 0x06007049 RID: 28745 RVA: 0x001D3F2C File Offset: 0x001D212C
		internal void Reset(DataAggregateInfo aggregateDef)
		{
			if (this.m_collection != null)
			{
				DataAggregateObj aggregateObj = this.GetAggregateObj(aggregateDef.Name);
				if (aggregateObj != null)
				{
					aggregateObj.ResetForNoRows();
				}
			}
		}

		// Token: 0x0600704A RID: 28746 RVA: 0x001D3F58 File Offset: 0x001D2158
		internal void Add(DataAggregateObj newObject)
		{
			Global.Tracer.Assert(!newObject.NonAggregateMode, "( !newObject.NonAggregateMode )");
			try
			{
				if (this.m_lockAdd)
				{
					Monitor.Enter(this.m_collection);
				}
				this.m_collection.Add(newObject.Name, newObject);
				this.PopulateDuplicateNames(newObject.Name, newObject.DuplicateNames);
			}
			finally
			{
				if (this.m_lockAdd)
				{
					Monitor.Exit(this.m_collection);
				}
			}
		}

		// Token: 0x0600704B RID: 28747 RVA: 0x001D3FDC File Offset: 0x001D21DC
		internal void Remove(DataAggregateInfo aggDef)
		{
			try
			{
				if (this.m_lockAdd)
				{
					Monitor.Enter(this.m_collection);
				}
				if (this.m_collection != null)
				{
					this.m_collection.Remove(aggDef.Name);
					List<string> duplicateNames = aggDef.DuplicateNames;
					if (this.m_duplicateNames != null && duplicateNames != null)
					{
						for (int i = 0; i < duplicateNames.Count; i++)
						{
							this.m_duplicateNames.Remove(duplicateNames[i]);
						}
					}
				}
			}
			finally
			{
				if (this.m_lockAdd)
				{
					Monitor.Exit(this.m_collection);
				}
			}
		}

		// Token: 0x0600704C RID: 28748 RVA: 0x001D4070 File Offset: 0x001D2270
		internal void Set(string name, DataAggregateInfo aggregateDef, List<string> duplicateNames, DataAggregateObjResult aggregateResult)
		{
			DataAggregateObj dataAggregateObj = this.GetAggregateObj(name);
			if (dataAggregateObj == null)
			{
				try
				{
					if (this.m_lockAdd)
					{
						Monitor.Enter(this.m_collection);
					}
					dataAggregateObj = new DataAggregateObj(aggregateDef, aggregateResult);
					this.m_collection.Add(name, dataAggregateObj);
					this.PopulateDuplicateNames(name, duplicateNames);
					return;
				}
				finally
				{
					if (this.m_lockAdd)
					{
						Monitor.Exit(this.m_collection);
					}
				}
			}
			dataAggregateObj.Set(aggregateResult);
		}

		// Token: 0x0600704D RID: 28749 RVA: 0x001D40E8 File Offset: 0x001D22E8
		internal DataAggregateObj GetAggregateObj(string name)
		{
			DataAggregateObj dataAggregateObj = (DataAggregateObj)this.m_collection[name];
			if (dataAggregateObj == null && this.m_duplicateNames != null)
			{
				string text = (string)this.m_duplicateNames[name];
				if (text != null)
				{
					dataAggregateObj = (DataAggregateObj)this.m_collection[text];
				}
			}
			return dataAggregateObj;
		}

		// Token: 0x0600704E RID: 28750 RVA: 0x001D413C File Offset: 0x001D233C
		private void PopulateDuplicateNames(string name, List<string> duplicateNames)
		{
			if (duplicateNames != null && 0 < duplicateNames.Count)
			{
				if (this.m_duplicateNames == null)
				{
					this.m_duplicateNames = new Hashtable();
				}
				for (int i = 0; i < duplicateNames.Count; i++)
				{
					this.m_duplicateNames[duplicateNames[i]] = name;
				}
			}
		}

		// Token: 0x0600704F RID: 28751 RVA: 0x001D418C File Offset: 0x001D238C
		internal void ResetFieldsUsedInExpression()
		{
			foreach (object obj in this.m_collection.Values)
			{
				((DataAggregateObj)obj).UsedInExpression = false;
			}
		}

		// Token: 0x06007050 RID: 28752 RVA: 0x001D41E8 File Offset: 0x001D23E8
		internal void AddFieldsUsedInExpression(OnDemandProcessingContext odpContext, List<string> fieldsUsedInValueExpression)
		{
			Dictionary<string, List<string>> aggregateFieldReferences = odpContext.OdpMetadata.ReportSnapshot.AggregateFieldReferences;
			foreach (object obj in this.m_collection.Values)
			{
				DataAggregateObj dataAggregateObj = (DataAggregateObj)obj;
				List<string> list;
				if (dataAggregateObj.UsedInExpression && dataAggregateObj.AggregateDef != null && aggregateFieldReferences.TryGetValue(dataAggregateObj.AggregateDef.Name, out list))
				{
					fieldsUsedInValueExpression.AddRange(list);
				}
			}
		}

		// Token: 0x17002645 RID: 9797
		// (get) Token: 0x06007051 RID: 28753 RVA: 0x001D4280 File Offset: 0x001D2480
		public int ID
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x06007052 RID: 28754 RVA: 0x001D4288 File Offset: 0x001D2488
		public void SetID(int id)
		{
			this.m_id = id;
		}

		// Token: 0x06007053 RID: 28755 RVA: 0x001D4291 File Offset: 0x001D2491
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.AggregatesImpl;
		}

		// Token: 0x040039FF RID: 14847
		private bool m_lockAdd;

		// Token: 0x04003A00 RID: 14848
		private Hashtable m_collection;

		// Token: 0x04003A01 RID: 14849
		private Hashtable m_duplicateNames;

		// Token: 0x04003A02 RID: 14850
		private OnDemandProcessingContext m_odpContext;

		// Token: 0x04003A03 RID: 14851
		private int m_id = int.MinValue;
	}
}
