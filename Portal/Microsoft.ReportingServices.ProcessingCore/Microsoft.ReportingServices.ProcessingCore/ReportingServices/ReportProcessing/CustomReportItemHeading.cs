using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000764 RID: 1892
	[Serializable]
	internal sealed class CustomReportItemHeading : TablixHeading, IRunningValueHolder
	{
		// Token: 0x060068F4 RID: 26868 RVA: 0x00198848 File Offset: 0x00196A48
		internal CustomReportItemHeading()
		{
		}

		// Token: 0x060068F5 RID: 26869 RVA: 0x00198857 File Offset: 0x00196A57
		internal CustomReportItemHeading(int id, CustomReportItem crItem)
			: base(id, crItem)
		{
			this.m_runningValues = new RunningValueInfoList();
		}

		// Token: 0x17002511 RID: 9489
		// (get) Token: 0x060068F6 RID: 26870 RVA: 0x00198873 File Offset: 0x00196A73
		// (set) Token: 0x060068F7 RID: 26871 RVA: 0x0019887B File Offset: 0x00196A7B
		internal bool Static
		{
			get
			{
				return this.m_static;
			}
			set
			{
				this.m_static = value;
			}
		}

		// Token: 0x17002512 RID: 9490
		// (get) Token: 0x060068F8 RID: 26872 RVA: 0x00198884 File Offset: 0x00196A84
		// (set) Token: 0x060068F9 RID: 26873 RVA: 0x0019888C File Offset: 0x00196A8C
		internal CustomReportItemHeadingList InnerHeadings
		{
			get
			{
				return this.m_innerHeadings;
			}
			set
			{
				this.m_innerHeadings = value;
			}
		}

		// Token: 0x17002513 RID: 9491
		// (get) Token: 0x060068FA RID: 26874 RVA: 0x00198895 File Offset: 0x00196A95
		// (set) Token: 0x060068FB RID: 26875 RVA: 0x0019889D File Offset: 0x00196A9D
		internal DataValueList CustomProperties
		{
			get
			{
				return this.m_customProperties;
			}
			set
			{
				this.m_customProperties = value;
			}
		}

		// Token: 0x17002514 RID: 9492
		// (get) Token: 0x060068FC RID: 26876 RVA: 0x001988A6 File Offset: 0x00196AA6
		// (set) Token: 0x060068FD RID: 26877 RVA: 0x001988AE File Offset: 0x00196AAE
		internal int ExprHostID
		{
			get
			{
				return this.m_exprHostID;
			}
			set
			{
				this.m_exprHostID = value;
			}
		}

		// Token: 0x17002515 RID: 9493
		// (get) Token: 0x060068FE RID: 26878 RVA: 0x001988B7 File Offset: 0x00196AB7
		// (set) Token: 0x060068FF RID: 26879 RVA: 0x001988BF File Offset: 0x00196ABF
		internal RunningValueInfoList RunningValues
		{
			get
			{
				return this.m_runningValues;
			}
			set
			{
				this.m_runningValues = value;
			}
		}

		// Token: 0x17002516 RID: 9494
		// (get) Token: 0x06006900 RID: 26880 RVA: 0x001988C8 File Offset: 0x00196AC8
		internal DataGroupingExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x06006901 RID: 26881 RVA: 0x001988D0 File Offset: 0x00196AD0
		RunningValueInfoList IRunningValueHolder.GetRunningValueList()
		{
			return this.m_runningValues;
		}

		// Token: 0x06006902 RID: 26882 RVA: 0x001988D8 File Offset: 0x00196AD8
		void IRunningValueHolder.ClearIfEmpty()
		{
			Global.Tracer.Assert(this.m_runningValues != null);
			if (this.m_runningValues.Count == 0)
			{
				this.m_runningValues = null;
			}
		}

		// Token: 0x06006903 RID: 26883 RVA: 0x00198904 File Offset: 0x00196B04
		internal bool Initialize(int level, CustomReportItemHeadingList peerHeadings, int headingIndex, DataCellsList dataRowCells, ref int currentIndex, ref int maxLevel, InitializationContext context)
		{
			this.m_level = level;
			if (level > maxLevel)
			{
				maxLevel = level;
			}
			context.ExprHostBuilder.DataGroupingStart(this.m_isColumn);
			if (this.m_static)
			{
				Global.Tracer.Assert(!this.m_subtotal);
				if (this.m_grouping != null)
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsInvalidStaticDataGrouping, Severity.Error, context.ObjectType, context.ObjectName, "DataGrouping", Array.Empty<string>());
					this.m_grouping = null;
				}
				else
				{
					this.m_sorting = null;
					this.CommonInitialize(level, dataRowCells, ref currentIndex, ref maxLevel, context);
				}
			}
			else
			{
				if ((context.Location & LocationFlags.InDetail) != (LocationFlags)0)
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsInvalidDetailDataGrouping, Severity.Error, context.ObjectType, context.ObjectName, "DataGrouping", Array.Empty<string>());
					return false;
				}
				if (this.m_grouping != null && this.m_grouping.CustomProperties != null)
				{
					if (this.m_customProperties == null)
					{
						this.m_customProperties = new DataValueList(this.m_grouping.CustomProperties.Count);
					}
					this.m_customProperties.AddRange(this.m_grouping.CustomProperties);
					this.m_grouping.CustomProperties = null;
				}
				if (this.m_subtotal)
				{
					if (this.m_grouping != null)
					{
						context.AggregateRewriteScopes = new Hashtable();
						context.AggregateRewriteScopes.Add(this.m_grouping.Name, null);
					}
					Global.Tracer.Assert(peerHeadings[headingIndex] != null);
					int num = currentIndex;
					CustomReportItemHeading customReportItemHeading = CustomReportItemHeading.HeadingClone(this, dataRowCells, ref num, this.m_headingSpan, context);
					customReportItemHeading.m_innerHeadings = CustomReportItemHeading.HeadingListClone(this.m_innerHeadings, dataRowCells, ref num, this.m_headingSpan, context);
					Global.Tracer.Assert(currentIndex + this.m_headingSpan == num);
					Global.Tracer.Assert(!customReportItemHeading.m_subtotal && this.m_subtotal);
					Global.Tracer.Assert(headingIndex < peerHeadings.Count);
					peerHeadings.Insert(headingIndex + 1, customReportItemHeading);
					context.AggregateRewriteScopes = null;
					context.AggregateRewriteMap = null;
				}
				if (this.m_grouping != null)
				{
					context.Location |= LocationFlags.InGrouping;
					context.RegisterGroupingScope(this.m_grouping.Name, this.m_grouping.SimpleGroupExpressions, this.m_grouping.Aggregates, this.m_grouping.PostSortAggregates, this.m_grouping.RecursiveAggregates, this.m_grouping);
					ObjectType objectType = context.ObjectType;
					string objectName = context.ObjectName;
					context.ObjectType = ObjectType.Grouping;
					context.ObjectName = this.m_grouping.Name;
					this.CommonInitialize(level, dataRowCells, ref currentIndex, ref maxLevel, context);
					context.ObjectType = objectType;
					context.ObjectName = objectName;
					context.UnRegisterGroupingScope(this.m_grouping.Name);
				}
				else
				{
					context.Location |= LocationFlags.InDetail;
					this.CommonInitialize(level, dataRowCells, ref currentIndex, ref maxLevel, context);
				}
			}
			this.m_exprHostID = context.ExprHostBuilder.DataGroupingEnd(this.m_isColumn);
			this.m_hasExprHost |= this.m_exprHostID >= 0;
			return this.m_subtotal;
		}

		// Token: 0x06006904 RID: 26884 RVA: 0x00198C2C File Offset: 0x00196E2C
		private void CommonInitialize(int level, DataCellsList dataRowCells, ref int currentIndex, ref int maxLevel, InitializationContext context)
		{
			base.Initialize(context);
			if (this.m_customProperties != null)
			{
				context.RegisterRunningValues(this.m_runningValues);
				this.m_customProperties.Initialize(null, true, context);
				context.UnRegisterRunningValues(this.m_runningValues);
			}
			if (this.m_innerHeadings != null)
			{
				Global.Tracer.Assert(context.AggregateEscalateScopes != null);
				if (this.m_grouping != null)
				{
					context.AggregateEscalateScopes.Add(this.m_grouping.Name);
				}
				this.m_headingSpan += this.m_innerHeadings.Initialize(level + 1, dataRowCells, ref currentIndex, ref maxLevel, context);
				if (this.m_grouping != null)
				{
					context.AggregateEscalateScopes.RemoveAt(context.AggregateEscalateScopes.Count - 1);
					return;
				}
			}
			else
			{
				currentIndex++;
			}
		}

		// Token: 0x06006905 RID: 26885 RVA: 0x00198CF8 File Offset: 0x00196EF8
		private static CustomReportItemHeading HeadingClone(CustomReportItemHeading heading, DataCellsList dataRowCells, ref int currentIndex, int headingSpan, InitializationContext context)
		{
			Global.Tracer.Assert(heading != null);
			CustomReportItemHeading customReportItemHeading = new CustomReportItemHeading(context.GenerateSubtotalID(), (CustomReportItem)heading.DataRegionDef);
			customReportItemHeading.m_isColumn = heading.m_isColumn;
			customReportItemHeading.m_level = heading.m_level;
			customReportItemHeading.m_static = true;
			customReportItemHeading.m_subtotal = false;
			customReportItemHeading.m_headingSpan = heading.m_headingSpan;
			if (heading.m_customProperties != null)
			{
				customReportItemHeading.m_customProperties = heading.m_customProperties.DeepClone(context);
			}
			if (heading.m_innerHeadings == null)
			{
				if (heading.m_isColumn)
				{
					int count = dataRowCells.Count;
					for (int i = 0; i < count; i++)
					{
						DataCellList dataCellList = dataRowCells[i];
						Global.Tracer.Assert(currentIndex + headingSpan <= dataCellList.Count);
						dataCellList.Insert(currentIndex + headingSpan, dataCellList[currentIndex].DeepClone(context));
					}
				}
				else
				{
					Global.Tracer.Assert(currentIndex + headingSpan <= dataRowCells.Count);
					DataCellList dataCellList2 = dataRowCells[currentIndex];
					int count2 = dataCellList2.Count;
					DataCellList dataCellList3 = new DataCellList(count2);
					dataRowCells.Insert(currentIndex + headingSpan, dataCellList3);
					for (int j = 0; j < count2; j++)
					{
						dataCellList3.Add(dataCellList2[j].DeepClone(context));
					}
				}
				currentIndex++;
			}
			return customReportItemHeading;
		}

		// Token: 0x06006906 RID: 26886 RVA: 0x00198E4C File Offset: 0x0019704C
		private static CustomReportItemHeadingList HeadingListClone(CustomReportItemHeadingList headings, DataCellsList dataRowCells, ref int currentIndex, int headingSpan, InitializationContext context)
		{
			if (headings == null)
			{
				return null;
			}
			int count = headings.Count;
			Global.Tracer.Assert(1 <= count);
			CustomReportItemHeadingList customReportItemHeadingList = new CustomReportItemHeadingList(count);
			for (int i = 0; i < count; i++)
			{
				CustomReportItemHeading customReportItemHeading = headings[i];
				if (customReportItemHeading.m_grouping != null)
				{
					context.AggregateRewriteScopes.Add(customReportItemHeading.m_grouping.Name, null);
				}
				CustomReportItemHeading customReportItemHeading2 = CustomReportItemHeading.HeadingClone(customReportItemHeading, dataRowCells, ref currentIndex, headingSpan, context);
				if (customReportItemHeading.m_innerHeadings != null)
				{
					customReportItemHeading2.m_innerHeadings = CustomReportItemHeading.HeadingListClone(customReportItemHeading.m_innerHeadings, dataRowCells, ref currentIndex, headingSpan, context);
				}
				if (customReportItemHeading.m_grouping != null)
				{
					context.AggregateRewriteScopes.Remove(customReportItemHeading.m_grouping.Name);
				}
				customReportItemHeadingList.Add(customReportItemHeading2);
			}
			return customReportItemHeadingList;
		}

		// Token: 0x06006907 RID: 26887 RVA: 0x00198F0C File Offset: 0x0019710C
		internal static bool ValidateProcessingRestrictions(CustomReportItemHeadingList headings, bool isColumn, bool hasStatic, InitializationContext context)
		{
			bool flag = true;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			string text = (isColumn ? "column" : "row");
			if (headings != null)
			{
				for (int i = 0; i < headings.Count; i++)
				{
					CustomReportItemHeading customReportItemHeading = headings[i];
					if (!customReportItemHeading.Static && customReportItemHeading.Grouping == null)
					{
						context.ErrorContext.Register(ProcessingErrorCode.rsInvalidGrouping, Severity.Error, context.ObjectType, context.ObjectName, text, Array.Empty<string>());
						flag = false;
					}
					if (customReportItemHeading.Subtotal)
					{
						context.ErrorContext.Register(ProcessingErrorCode.rsCRISubtotalNotSupported, Severity.Error, context.ObjectType, context.ObjectName, text, Array.Empty<string>());
						flag = false;
					}
					if (customReportItemHeading.Static && hasStatic)
					{
						flag3 = true;
					}
					if (customReportItemHeading.Static && customReportItemHeading.InnerHeadings != null)
					{
						flag4 = true;
					}
					if (!customReportItemHeading.Static && headings.Count > 1)
					{
						flag2 = true;
					}
					if (flag && !flag2 && !flag3 && !flag4 && customReportItemHeading.InnerHeadings != null && !CustomReportItemHeading.ValidateProcessingRestrictions(customReportItemHeading.InnerHeadings, isColumn, customReportItemHeading.Static, context))
					{
						flag = false;
					}
				}
			}
			if (flag3)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsCRIMultiStaticColumnsOrRows, Severity.Error, context.ObjectType, context.ObjectName, text, Array.Empty<string>());
				flag = false;
			}
			if (flag4)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsCRIStaticWithSubgroups, Severity.Error, context.ObjectType, context.ObjectName, text, Array.Empty<string>());
				flag = false;
			}
			if (flag2)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsCRIMultiNonStaticGroups, Severity.Error, context.ObjectType, context.ObjectName, text, Array.Empty<string>());
				flag = false;
			}
			return flag;
		}

		// Token: 0x06006908 RID: 26888 RVA: 0x001990B8 File Offset: 0x001972B8
		internal void CopySubHeadingAggregates()
		{
			if (this.m_innerHeadings != null)
			{
				int count = this.m_innerHeadings.Count;
				for (int i = 0; i < count; i++)
				{
					CustomReportItemHeading customReportItemHeading = this.m_innerHeadings[i];
					customReportItemHeading.CopySubHeadingAggregates();
					Tablix.CopyAggregates(customReportItemHeading.Aggregates, this.m_aggregates);
					Tablix.CopyAggregates(customReportItemHeading.PostSortAggregates, this.m_postSortAggregates);
					Tablix.CopyAggregates(customReportItemHeading.RecursiveAggregates, this.m_aggregates);
				}
			}
		}

		// Token: 0x06006909 RID: 26889 RVA: 0x0019912C File Offset: 0x0019732C
		internal void TransferHeadingAggregates()
		{
			if (this.m_innerHeadings != null)
			{
				this.m_innerHeadings.TransferHeadingAggregates();
			}
			if (this.m_grouping != null)
			{
				for (int i = 0; i < this.m_aggregates.Count; i++)
				{
					this.m_grouping.Aggregates.Add(this.m_aggregates[i]);
				}
			}
			this.m_aggregates = null;
			if (this.m_grouping != null)
			{
				for (int j = 0; j < this.m_postSortAggregates.Count; j++)
				{
					this.m_grouping.PostSortAggregates.Add(this.m_postSortAggregates[j]);
				}
			}
			this.m_postSortAggregates = null;
			if (this.m_grouping != null)
			{
				for (int k = 0; k < this.m_recursiveAggregates.Count; k++)
				{
					this.m_grouping.RecursiveAggregates.Add(this.m_recursiveAggregates[k]);
				}
			}
			this.m_recursiveAggregates = null;
		}

		// Token: 0x0600690A RID: 26890 RVA: 0x00199214 File Offset: 0x00197414
		internal void SetExprHost(IList<DataGroupingExprHost> dataGroupingHosts, ObjectModelImpl reportObjectModel)
		{
			if (this.m_exprHostID >= 0)
			{
				Global.Tracer.Assert(dataGroupingHosts != null && dataGroupingHosts.Count > this.m_exprHostID && reportObjectModel != null);
				this.m_exprHost = dataGroupingHosts[this.m_exprHostID];
				this.m_exprHost.SetReportObjectModel(reportObjectModel);
				if (this.m_exprHost.GroupingHost != null)
				{
					Global.Tracer.Assert(this.m_grouping != null);
					this.m_grouping.SetExprHost(this.m_exprHost.GroupingHost, reportObjectModel);
				}
				if (this.m_exprHost.SortingHost != null)
				{
					Global.Tracer.Assert(this.m_sorting != null);
					this.m_sorting.SetExprHost(this.m_exprHost.SortingHost, reportObjectModel);
				}
				if (this.m_customProperties != null)
				{
					Global.Tracer.Assert(this.m_customProperties != null);
					this.m_customProperties.SetExprHost(this.m_exprHost.CustomPropertyHostsRemotable, reportObjectModel);
				}
			}
		}

		// Token: 0x0600690B RID: 26891 RVA: 0x00199310 File Offset: 0x00197510
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.TablixHeading, new MemberInfoList
			{
				new MemberInfo(MemberName.Static, Token.Boolean),
				new MemberInfo(MemberName.InnerHeadings, ObjectType.CustomReportItemHeadingList),
				new MemberInfo(MemberName.CustomProperties, ObjectType.DataValueList),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.RunningValues, ObjectType.RunningValueInfoList)
			});
		}

		// Token: 0x040033BF RID: 13247
		private bool m_static;

		// Token: 0x040033C0 RID: 13248
		private CustomReportItemHeadingList m_innerHeadings;

		// Token: 0x040033C1 RID: 13249
		private DataValueList m_customProperties;

		// Token: 0x040033C2 RID: 13250
		private int m_exprHostID = -1;

		// Token: 0x040033C3 RID: 13251
		private RunningValueInfoList m_runningValues;

		// Token: 0x040033C4 RID: 13252
		[NonSerialized]
		private DataGroupingExprHost m_exprHost;
	}
}
