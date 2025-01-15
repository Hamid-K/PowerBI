using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.ReportProcessing.ReportObjectModel
{
	// Token: 0x02000797 RID: 1943
	internal sealed class AggregatesImpl : Aggregates
	{
		// Token: 0x06006C4B RID: 27723 RVA: 0x001B7313 File Offset: 0x001B5513
		internal AggregatesImpl(IErrorContext iErrorContext)
			: this(false, iErrorContext)
		{
		}

		// Token: 0x06006C4C RID: 27724 RVA: 0x001B731D File Offset: 0x001B551D
		internal AggregatesImpl(bool lockAdd, IErrorContext iErrorContext)
		{
			this.m_lockAdd = lockAdd;
			this.m_collection = new Hashtable();
			this.m_duplicateNames = null;
			this.m_iErrorContext = iErrorContext;
		}

		// Token: 0x170025B3 RID: 9651
		public override object this[string key]
		{
			get
			{
				DataAggregateObj dataAggregateObj = this.GetAggregateObj(key);
				if (dataAggregateObj == null && this.m_duplicateNames != null)
				{
					string text = (string)this.m_duplicateNames[key];
					if (text != null)
					{
						dataAggregateObj = this.GetAggregateObj(text);
					}
				}
				if (dataAggregateObj == null)
				{
					return null;
				}
				dataAggregateObj.UsedInExpression = true;
				DataAggregateObjResult dataAggregateObjResult = dataAggregateObj.AggregateResult();
				if (dataAggregateObjResult.HasCode)
				{
					if (dataAggregateObjResult.FieldStatus == DataFieldStatus.None && dataAggregateObjResult.Code != ProcessingErrorCode.rsNone)
					{
						this.m_iErrorContext.Register(dataAggregateObjResult.Code, dataAggregateObjResult.Severity, dataAggregateObjResult.Arguments);
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
		}

		// Token: 0x170025B4 RID: 9652
		// (get) Token: 0x06006C4E RID: 27726 RVA: 0x001B73F4 File Offset: 0x001B55F4
		internal ICollection Objects
		{
			get
			{
				return this.m_collection.Values;
			}
		}

		// Token: 0x06006C4F RID: 27727 RVA: 0x001B7404 File Offset: 0x001B5604
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

		// Token: 0x06006C50 RID: 27728 RVA: 0x001B7488 File Offset: 0x001B5688
		internal void Set(string name, DataAggregateInfo aggregateDef, StringList duplicateNames, DataAggregateObjResult aggregateResult)
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

		// Token: 0x06006C51 RID: 27729 RVA: 0x001B7500 File Offset: 0x001B5700
		internal DataAggregateObj GetAggregateObj(string name)
		{
			return (DataAggregateObj)this.m_collection[name];
		}

		// Token: 0x06006C52 RID: 27730 RVA: 0x001B7514 File Offset: 0x001B5714
		private void PopulateDuplicateNames(string name, StringList duplicateNames)
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

		// Token: 0x06006C53 RID: 27731 RVA: 0x001B7564 File Offset: 0x001B5764
		internal void ResetUsedInExpression()
		{
			foreach (object obj in this.m_collection.Values)
			{
				((DataAggregateObj)obj).UsedInExpression = false;
			}
		}

		// Token: 0x06006C54 RID: 27732 RVA: 0x001B75C0 File Offset: 0x001B57C0
		internal void AddFieldsUsedInExpression(List<string> fieldsUsedInValueExpression)
		{
			foreach (object obj in this.m_collection.Values)
			{
				DataAggregateObj dataAggregateObj = (DataAggregateObj)obj;
				if (dataAggregateObj.UsedInExpression && dataAggregateObj.AggregateDef != null && dataAggregateObj.AggregateDef.FieldsUsedInValueExpression != null)
				{
					fieldsUsedInValueExpression.AddRange(dataAggregateObj.AggregateDef.FieldsUsedInValueExpression);
				}
			}
		}

		// Token: 0x0400366C RID: 13932
		private bool m_lockAdd;

		// Token: 0x0400366D RID: 13933
		private Hashtable m_collection;

		// Token: 0x0400366E RID: 13934
		private Hashtable m_duplicateNames;

		// Token: 0x0400366F RID: 13935
		private IErrorContext m_iErrorContext;

		// Token: 0x04003670 RID: 13936
		internal const string Name = "Aggregates";

		// Token: 0x04003671 RID: 13937
		internal const string FullName = "Microsoft.ReportingServices.ReportProcessing.ReportObjectModel.Aggregates";
	}
}
