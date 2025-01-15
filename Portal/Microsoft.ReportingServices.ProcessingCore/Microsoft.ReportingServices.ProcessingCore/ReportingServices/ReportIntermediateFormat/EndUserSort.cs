using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200050F RID: 1295
	[Serializable]
	public sealed class EndUserSort : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x17001CF0 RID: 7408
		// (get) Token: 0x060044E6 RID: 17638 RVA: 0x0011FB75 File Offset: 0x0011DD75
		// (set) Token: 0x060044E7 RID: 17639 RVA: 0x0011FB7D File Offset: 0x0011DD7D
		internal DataSet DataSet
		{
			get
			{
				return this.m_dataSet;
			}
			set
			{
				this.m_dataSet = value;
			}
		}

		// Token: 0x17001CF1 RID: 7409
		// (get) Token: 0x060044E8 RID: 17640 RVA: 0x0011FB86 File Offset: 0x0011DD86
		// (set) Token: 0x060044E9 RID: 17641 RVA: 0x0011FB8E File Offset: 0x0011DD8E
		internal ISortFilterScope SortExpressionScope
		{
			get
			{
				return this.m_sortExpressionScope;
			}
			set
			{
				this.m_sortExpressionScope = value;
			}
		}

		// Token: 0x17001CF2 RID: 7410
		// (get) Token: 0x060044EA RID: 17642 RVA: 0x0011FB97 File Offset: 0x0011DD97
		// (set) Token: 0x060044EB RID: 17643 RVA: 0x0011FB9F File Offset: 0x0011DD9F
		internal GroupingList GroupsInSortTarget
		{
			get
			{
				return this.m_groupsInSortTarget;
			}
			set
			{
				this.m_groupsInSortTarget = value;
			}
		}

		// Token: 0x17001CF3 RID: 7411
		// (get) Token: 0x060044EC RID: 17644 RVA: 0x0011FBA8 File Offset: 0x0011DDA8
		// (set) Token: 0x060044ED RID: 17645 RVA: 0x0011FBB0 File Offset: 0x0011DDB0
		internal ISortFilterScope SortTarget
		{
			get
			{
				return this.m_sortTarget;
			}
			set
			{
				this.m_sortTarget = value;
			}
		}

		// Token: 0x17001CF4 RID: 7412
		// (get) Token: 0x060044EE RID: 17646 RVA: 0x0011FBB9 File Offset: 0x0011DDB9
		// (set) Token: 0x060044EF RID: 17647 RVA: 0x0011FBC1 File Offset: 0x0011DDC1
		internal int SortExpressionIndex
		{
			get
			{
				return this.m_sortExpressionIndex;
			}
			set
			{
				this.m_sortExpressionIndex = value;
			}
		}

		// Token: 0x17001CF5 RID: 7413
		// (get) Token: 0x060044F0 RID: 17648 RVA: 0x0011FBCA File Offset: 0x0011DDCA
		// (set) Token: 0x060044F1 RID: 17649 RVA: 0x0011FBD2 File Offset: 0x0011DDD2
		internal List<SubReport> DetailScopeSubReports
		{
			get
			{
				return this.m_detailScopeSubReports;
			}
			set
			{
				this.m_detailScopeSubReports = value;
			}
		}

		// Token: 0x17001CF6 RID: 7414
		// (get) Token: 0x060044F2 RID: 17650 RVA: 0x0011FBDB File Offset: 0x0011DDDB
		// (set) Token: 0x060044F3 RID: 17651 RVA: 0x0011FBE3 File Offset: 0x0011DDE3
		internal int SubReportDataSetGlobalId
		{
			get
			{
				return this.m_subReportDataSetGlobalId;
			}
			set
			{
				this.m_subReportDataSetGlobalId = value;
			}
		}

		// Token: 0x17001CF7 RID: 7415
		// (get) Token: 0x060044F4 RID: 17652 RVA: 0x0011FBEC File Offset: 0x0011DDEC
		// (set) Token: 0x060044F5 RID: 17653 RVA: 0x0011FBF4 File Offset: 0x0011DDF4
		internal ExpressionInfo SortExpression
		{
			get
			{
				return this.m_sortExpression;
			}
			set
			{
				this.m_sortExpression = value;
			}
		}

		// Token: 0x17001CF8 RID: 7416
		// (get) Token: 0x060044F6 RID: 17654 RVA: 0x0011FBFD File Offset: 0x0011DDFD
		// (set) Token: 0x060044F7 RID: 17655 RVA: 0x0011FC05 File Offset: 0x0011DE05
		internal string SortExpressionScopeString
		{
			get
			{
				return this.m_sortExpressionScopeString;
			}
			set
			{
				this.m_sortExpressionScopeString = value;
			}
		}

		// Token: 0x17001CF9 RID: 7417
		// (get) Token: 0x060044F8 RID: 17656 RVA: 0x0011FC0E File Offset: 0x0011DE0E
		// (set) Token: 0x060044F9 RID: 17657 RVA: 0x0011FC16 File Offset: 0x0011DE16
		internal string SortTargetString
		{
			get
			{
				return this.m_sortTargetString;
			}
			set
			{
				this.m_sortTargetString = value;
			}
		}

		// Token: 0x060044FA RID: 17658 RVA: 0x0011FC20 File Offset: 0x0011DE20
		internal void SetSortTarget(ISortFilterScope target)
		{
			Global.Tracer.Assert(target != null);
			this.m_sortTarget = target;
			if (target.UserSortExpressions == null)
			{
				target.UserSortExpressions = new List<ExpressionInfo>();
			}
			this.m_sortExpressionIndex = target.UserSortExpressions.Count;
			target.UserSortExpressions.Add(this.m_sortExpression);
		}

		// Token: 0x060044FB RID: 17659 RVA: 0x0011FC77 File Offset: 0x0011DE77
		internal void SetDefaultSortTarget(ISortFilterScope target)
		{
			this.SetSortTarget(target);
			this.m_sortTargetString = target.ScopeName;
		}

		// Token: 0x060044FC RID: 17660 RVA: 0x0011FC8C File Offset: 0x0011DE8C
		public object PublishClone(AutomaticSubtotalContext context)
		{
			EndUserSort endUserSort = (EndUserSort)base.MemberwiseClone();
			if (this.m_sortExpression != null)
			{
				endUserSort.m_sortExpression = (ExpressionInfo)this.m_sortExpression.PublishClone(context);
			}
			if (this.m_sortExpressionScopeString != null)
			{
				endUserSort.m_sortExpressionScopeString = (string)this.m_sortExpressionScopeString.Clone();
			}
			if (this.m_sortTargetString != null)
			{
				endUserSort.m_sortTargetString = (string)this.m_sortTargetString.Clone();
			}
			if (this.m_sortTargetString != null || this.m_sortExpressionScopeString != null)
			{
				context.AddEndUserSort(endUserSort);
			}
			return endUserSort;
		}

		// Token: 0x060044FD RID: 17661 RVA: 0x0011FD1C File Offset: 0x0011DF1C
		internal void UpdateSortScopeAndTargetReference(AutomaticSubtotalContext context)
		{
			if (this.m_sortExpressionScopeString != null)
			{
				this.m_sortExpressionScopeString = context.GetNewScopeName(this.m_sortExpressionScopeString);
			}
			if (this.m_sortTargetString != null)
			{
				this.m_sortTargetString = context.GetNewScopeName(this.m_sortTargetString);
				if (this.m_sortTarget != null)
				{
					ISortFilterScope sortFilterScope = null;
					if (context.TryGetNewSortTarget(this.m_sortTargetString, out sortFilterScope))
					{
						this.SetSortTarget(sortFilterScope);
					}
				}
			}
		}

		// Token: 0x060044FE RID: 17662 RVA: 0x0011FD84 File Offset: 0x0011DF84
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.EndUserSort, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.DataSet, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataSet, Token.Reference),
				new MemberInfo(MemberName.SortExpressionScope, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ISortFilterScope, Token.Reference),
				new MemberInfo(MemberName.GroupsInSortTarget, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Token.Reference, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Grouping),
				new MemberInfo(MemberName.SortTarget, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ISortFilterScope, Token.Reference),
				new MemberInfo(MemberName.SortExpressionIndex, Token.Int32),
				new MemberInfo(MemberName.DetailScopeSubReports, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Token.Reference, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SubReport)
			});
		}

		// Token: 0x060044FF RID: 17663 RVA: 0x0011FE28 File Offset: 0x0011E028
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(EndUserSort.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.SortExpressionIndex)
				{
					switch (memberName)
					{
					case MemberName.SortExpressionScope:
						writer.WriteReference(this.m_sortExpressionScope);
						continue;
					case MemberName.GroupsInSortTarget:
						writer.WriteListOfReferences(this.m_groupsInSortTarget);
						continue;
					case MemberName.SortTarget:
						writer.WriteReference(this.m_sortTarget);
						continue;
					default:
						if (memberName == MemberName.SortExpressionIndex)
						{
							writer.Write(this.m_sortExpressionIndex);
							continue;
						}
						break;
					}
				}
				else
				{
					if (memberName == MemberName.DetailScopeSubReports)
					{
						writer.WriteListOfReferences(this.m_detailScopeSubReports);
						continue;
					}
					if (memberName == MemberName.DataSet)
					{
						writer.WriteReference(this.m_dataSet);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06004500 RID: 17664 RVA: 0x0011FF00 File Offset: 0x0011E100
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(EndUserSort.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.SortExpressionIndex)
				{
					switch (memberName)
					{
					case MemberName.SortExpressionScope:
						this.m_sortExpressionScope = reader.ReadReference<ISortFilterScope>(this);
						continue;
					case MemberName.GroupsInSortTarget:
						this.m_groupsInSortTarget = reader.ReadListOfReferences<GroupingList, Grouping>(this);
						continue;
					case MemberName.SortTarget:
						this.m_sortTarget = reader.ReadReference<ISortFilterScope>(this);
						continue;
					default:
						if (memberName == MemberName.SortExpressionIndex)
						{
							this.m_sortExpressionIndex = reader.ReadInt32();
							continue;
						}
						break;
					}
				}
				else
				{
					if (memberName == MemberName.DetailScopeSubReports)
					{
						this.m_detailScopeSubReports = reader.ReadGenericListOfReferences<SubReport>(this);
						continue;
					}
					if (memberName == MemberName.DataSet)
					{
						this.m_dataSet = reader.ReadReference<DataSet>(this);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06004501 RID: 17665 RVA: 0x0011FFDC File Offset: 0x0011E1DC
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(EndUserSort.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					MemberName memberName = memberReference.MemberName;
					switch (memberName)
					{
					case MemberName.SortExpressionScope:
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						Global.Tracer.Assert(referenceableItems[memberReference.RefID] is ISortFilterScope);
						Global.Tracer.Assert(this.m_sortExpressionScope != (ISortFilterScope)referenceableItems[memberReference.RefID]);
						this.m_sortExpressionScope = (ISortFilterScope)referenceableItems[memberReference.RefID];
						break;
					case MemberName.GroupsInSortTarget:
						if (this.m_groupsInSortTarget == null)
						{
							this.m_groupsInSortTarget = new GroupingList();
						}
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						Global.Tracer.Assert(referenceableItems[memberReference.RefID] is Grouping);
						Global.Tracer.Assert(!this.m_groupsInSortTarget.Contains((Grouping)referenceableItems[memberReference.RefID]));
						this.m_groupsInSortTarget.Add((Grouping)referenceableItems[memberReference.RefID]);
						break;
					case MemberName.SortTarget:
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						Global.Tracer.Assert(referenceableItems[memberReference.RefID] is ISortFilterScope);
						Global.Tracer.Assert(this.m_sortTarget != (ISortFilterScope)referenceableItems[memberReference.RefID]);
						this.m_sortTarget = (ISortFilterScope)referenceableItems[memberReference.RefID];
						break;
					default:
						if (memberName != MemberName.DetailScopeSubReports)
						{
							if (memberName == MemberName.DataSet)
							{
								Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
								Global.Tracer.Assert(referenceableItems[memberReference.RefID] is DataSet);
								Global.Tracer.Assert(this.m_dataSet != (DataSet)referenceableItems[memberReference.RefID]);
								this.m_dataSet = (DataSet)referenceableItems[memberReference.RefID];
							}
							else
							{
								Global.Tracer.Assert(false);
							}
						}
						else
						{
							if (this.m_detailScopeSubReports == null)
							{
								this.m_detailScopeSubReports = new List<SubReport>();
							}
							Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
							Global.Tracer.Assert(referenceableItems[memberReference.RefID] is SubReport);
							Global.Tracer.Assert(!this.m_detailScopeSubReports.Contains((SubReport)referenceableItems[memberReference.RefID]));
							this.m_detailScopeSubReports.Add((SubReport)referenceableItems[memberReference.RefID]);
						}
						break;
					}
				}
			}
		}

		// Token: 0x06004502 RID: 17666 RVA: 0x00120310 File Offset: 0x0011E510
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.EndUserSort;
		}

		// Token: 0x04001F24 RID: 7972
		[Reference]
		private DataSet m_dataSet;

		// Token: 0x04001F25 RID: 7973
		[Reference]
		private ISortFilterScope m_sortExpressionScope;

		// Token: 0x04001F26 RID: 7974
		[Reference]
		private GroupingList m_groupsInSortTarget;

		// Token: 0x04001F27 RID: 7975
		[Reference]
		private ISortFilterScope m_sortTarget;

		// Token: 0x04001F28 RID: 7976
		private int m_sortExpressionIndex = -1;

		// Token: 0x04001F29 RID: 7977
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = EndUserSort.GetDeclaration();

		// Token: 0x04001F2A RID: 7978
		[NonSerialized]
		private ExpressionInfo m_sortExpression;

		// Token: 0x04001F2B RID: 7979
		[NonSerialized]
		private string m_sortExpressionScopeString;

		// Token: 0x04001F2C RID: 7980
		[NonSerialized]
		private string m_sortTargetString;

		// Token: 0x04001F2D RID: 7981
		private List<SubReport> m_detailScopeSubReports;

		// Token: 0x04001F2E RID: 7982
		[NonSerialized]
		private int m_subReportDataSetGlobalId = -1;
	}
}
