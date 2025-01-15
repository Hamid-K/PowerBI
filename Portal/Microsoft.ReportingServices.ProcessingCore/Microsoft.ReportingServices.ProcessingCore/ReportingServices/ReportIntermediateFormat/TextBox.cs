using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000501 RID: 1281
	[Serializable]
	public sealed class TextBox : Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem, IActionOwner, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IInScopeEventSource, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IGloballyReferenceable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IGlobalIDOwner
	{
		// Token: 0x06004275 RID: 17013 RVA: 0x00117906 File Offset: 0x00115B06
		internal TextBox(Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem parent)
			: base(parent)
		{
		}

		// Token: 0x06004276 RID: 17014 RVA: 0x0011791D File Offset: 0x00115B1D
		internal TextBox(int id, Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem parent)
			: base(id, parent)
		{
			this.m_paragraphs = new List<Microsoft.ReportingServices.ReportIntermediateFormat.Paragraph>();
		}

		// Token: 0x17001BED RID: 7149
		// (get) Token: 0x06004277 RID: 17015 RVA: 0x00117940 File Offset: 0x00115B40
		internal override Microsoft.ReportingServices.ReportProcessing.ObjectType ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.Textbox;
			}
		}

		// Token: 0x17001BEE RID: 7150
		// (get) Token: 0x06004278 RID: 17016 RVA: 0x00117943 File Offset: 0x00115B43
		// (set) Token: 0x06004279 RID: 17017 RVA: 0x0011794B File Offset: 0x00115B4B
		internal List<Microsoft.ReportingServices.ReportIntermediateFormat.Paragraph> Paragraphs
		{
			get
			{
				return this.m_paragraphs;
			}
			set
			{
				this.m_paragraphs = value;
			}
		}

		// Token: 0x17001BEF RID: 7151
		// (get) Token: 0x0600427A RID: 17018 RVA: 0x00117954 File Offset: 0x00115B54
		// (set) Token: 0x0600427B RID: 17019 RVA: 0x0011795C File Offset: 0x00115B5C
		internal bool CanScrollVertically
		{
			get
			{
				return this.m_canScrollVertically;
			}
			set
			{
				this.m_canScrollVertically = value;
			}
		}

		// Token: 0x17001BF0 RID: 7152
		// (get) Token: 0x0600427C RID: 17020 RVA: 0x00117965 File Offset: 0x00115B65
		// (set) Token: 0x0600427D RID: 17021 RVA: 0x0011796D File Offset: 0x00115B6D
		internal bool CanGrow
		{
			get
			{
				return this.m_canGrow;
			}
			set
			{
				this.m_canGrow = value;
			}
		}

		// Token: 0x17001BF1 RID: 7153
		// (get) Token: 0x0600427E RID: 17022 RVA: 0x00117976 File Offset: 0x00115B76
		// (set) Token: 0x0600427F RID: 17023 RVA: 0x0011797E File Offset: 0x00115B7E
		internal bool CanShrink
		{
			get
			{
				return this.m_canShrink;
			}
			set
			{
				this.m_canShrink = value;
			}
		}

		// Token: 0x17001BF2 RID: 7154
		// (get) Token: 0x06004280 RID: 17024 RVA: 0x00117987 File Offset: 0x00115B87
		// (set) Token: 0x06004281 RID: 17025 RVA: 0x0011798F File Offset: 0x00115B8F
		internal string HideDuplicates
		{
			get
			{
				return this.m_hideDuplicates;
			}
			set
			{
				this.m_hideDuplicates = value;
			}
		}

		// Token: 0x17001BF3 RID: 7155
		// (get) Token: 0x06004282 RID: 17026 RVA: 0x00117998 File Offset: 0x00115B98
		// (set) Token: 0x06004283 RID: 17027 RVA: 0x001179A0 File Offset: 0x00115BA0
		internal StructureTypeOverwriteType StructureTypeOverwrite
		{
			get
			{
				return this.m_structureTypeOverwrite;
			}
			set
			{
				this.m_structureTypeOverwrite = value;
			}
		}

		// Token: 0x17001BF4 RID: 7156
		// (get) Token: 0x06004284 RID: 17028 RVA: 0x001179A9 File Offset: 0x00115BA9
		// (set) Token: 0x06004285 RID: 17029 RVA: 0x001179B1 File Offset: 0x00115BB1
		internal bool IsToggle
		{
			get
			{
				return this.m_isToggle;
			}
			set
			{
				this.m_isToggle = value;
			}
		}

		// Token: 0x17001BF5 RID: 7157
		// (get) Token: 0x06004286 RID: 17030 RVA: 0x001179BA File Offset: 0x00115BBA
		// (set) Token: 0x06004287 RID: 17031 RVA: 0x001179C2 File Offset: 0x00115BC2
		internal ExpressionInfo InitialToggleState
		{
			get
			{
				return this.m_initialToggleState;
			}
			set
			{
				this.m_initialToggleState = value;
			}
		}

		// Token: 0x17001BF6 RID: 7158
		// (get) Token: 0x06004288 RID: 17032 RVA: 0x001179CB File Offset: 0x00115BCB
		// (set) Token: 0x06004289 RID: 17033 RVA: 0x001179D3 File Offset: 0x00115BD3
		internal bool RecursiveSender
		{
			get
			{
				return this.m_recursiveSender;
			}
			set
			{
				this.m_recursiveSender = value;
			}
		}

		// Token: 0x17001BF7 RID: 7159
		// (get) Token: 0x0600428A RID: 17034 RVA: 0x001179DC File Offset: 0x00115BDC
		// (set) Token: 0x0600428B RID: 17035 RVA: 0x001179E4 File Offset: 0x00115BE4
		internal bool HasNonRecursiveSender
		{
			get
			{
				return this.m_hasNonRecursiveSender;
			}
			set
			{
				this.m_hasNonRecursiveSender = value;
			}
		}

		// Token: 0x17001BF8 RID: 7160
		// (get) Token: 0x0600428C RID: 17036 RVA: 0x001179ED File Offset: 0x00115BED
		// (set) Token: 0x0600428D RID: 17037 RVA: 0x001179F5 File Offset: 0x00115BF5
		internal TablixMember RecursiveMember
		{
			get
			{
				return this.m_recursiveMember;
			}
			set
			{
				this.m_recursiveMember = value;
			}
		}

		// Token: 0x17001BF9 RID: 7161
		// (get) Token: 0x0600428E RID: 17038 RVA: 0x001179FE File Offset: 0x00115BFE
		// (set) Token: 0x0600428F RID: 17039 RVA: 0x00117A06 File Offset: 0x00115C06
		internal bool ValueReferenced
		{
			get
			{
				return this.m_valueReferenced;
			}
			set
			{
				this.m_valueReferenced = value;
			}
		}

		// Token: 0x17001BFA RID: 7162
		// (get) Token: 0x06004290 RID: 17040 RVA: 0x00117A0F File Offset: 0x00115C0F
		internal TextBoxExprHost TextBoxExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17001BFB RID: 7163
		// (get) Token: 0x06004291 RID: 17041 RVA: 0x00117A17 File Offset: 0x00115C17
		// (set) Token: 0x06004292 RID: 17042 RVA: 0x00117A1F File Offset: 0x00115C1F
		internal bool DataElementStyleAttribute
		{
			get
			{
				return this.m_dataElementStyleAttribute;
			}
			set
			{
				this.m_dataElementStyleAttribute = value;
			}
		}

		// Token: 0x17001BFC RID: 7164
		// (get) Token: 0x06004293 RID: 17043 RVA: 0x00117A28 File Offset: 0x00115C28
		// (set) Token: 0x06004294 RID: 17044 RVA: 0x00117A30 File Offset: 0x00115C30
		internal EndUserSort UserSort
		{
			get
			{
				return this.m_userSort;
			}
			set
			{
				this.m_userSort = value;
			}
		}

		// Token: 0x17001BFD RID: 7165
		// (get) Token: 0x06004295 RID: 17045 RVA: 0x00117A39 File Offset: 0x00115C39
		// (set) Token: 0x06004296 RID: 17046 RVA: 0x00117A41 File Offset: 0x00115C41
		internal bool OverrideReportDataElementStyle
		{
			get
			{
				return this.m_overrideReportDataElementStyle;
			}
			set
			{
				this.m_overrideReportDataElementStyle = value;
			}
		}

		// Token: 0x17001BFE RID: 7166
		// (get) Token: 0x06004297 RID: 17047 RVA: 0x00117A4A File Offset: 0x00115C4A
		// (set) Token: 0x06004298 RID: 17048 RVA: 0x00117A52 File Offset: 0x00115C52
		internal bool KeepTogether
		{
			get
			{
				return this.m_keepTogether;
			}
			set
			{
				this.m_keepTogether = value;
			}
		}

		// Token: 0x17001BFF RID: 7167
		// (get) Token: 0x06004299 RID: 17049 RVA: 0x00117A5B File Offset: 0x00115C5B
		internal bool HasExpressionBasedValue
		{
			get
			{
				return this.m_hasExpressionBasedValue;
			}
		}

		// Token: 0x17001C00 RID: 7168
		// (get) Token: 0x0600429A RID: 17050 RVA: 0x00117A63 File Offset: 0x00115C63
		internal bool HasValue
		{
			get
			{
				return this.m_hasValue;
			}
		}

		// Token: 0x17001C01 RID: 7169
		// (get) Token: 0x0600429B RID: 17051 RVA: 0x00117A6B File Offset: 0x00115C6B
		internal bool IsSimple
		{
			get
			{
				return this.m_isSimple;
			}
		}

		// Token: 0x17001C02 RID: 7170
		// (get) Token: 0x0600429C RID: 17052 RVA: 0x00117A73 File Offset: 0x00115C73
		internal override DataElementOutputTypes DataElementOutputDefault
		{
			get
			{
				if (DataElementOutputTypes.Auto == this.m_dataElementOutput && this.HasExpressionBasedValue)
				{
					return DataElementOutputTypes.Output;
				}
				return DataElementOutputTypes.NoOutput;
			}
		}

		// Token: 0x17001C03 RID: 7171
		// (get) Token: 0x0600429D RID: 17053 RVA: 0x00117A89 File Offset: 0x00115C89
		// (set) Token: 0x0600429E RID: 17054 RVA: 0x00117A91 File Offset: 0x00115C91
		internal Microsoft.ReportingServices.ReportIntermediateFormat.Action Action
		{
			get
			{
				return this.m_action;
			}
			set
			{
				this.m_action = value;
			}
		}

		// Token: 0x17001C04 RID: 7172
		// (get) Token: 0x0600429F RID: 17055 RVA: 0x00117A9A File Offset: 0x00115C9A
		Microsoft.ReportingServices.ReportIntermediateFormat.Action IActionOwner.Action
		{
			get
			{
				return this.m_action;
			}
		}

		// Token: 0x17001C05 RID: 7173
		// (get) Token: 0x060042A0 RID: 17056 RVA: 0x00117AA2 File Offset: 0x00115CA2
		// (set) Token: 0x060042A1 RID: 17057 RVA: 0x00117AAA File Offset: 0x00115CAA
		List<string> IActionOwner.FieldsUsedInValueExpression
		{
			get
			{
				return this.m_fieldsUsedInValueExpression;
			}
			set
			{
				this.m_fieldsUsedInValueExpression = value;
			}
		}

		// Token: 0x17001C06 RID: 7174
		// (get) Token: 0x060042A2 RID: 17058 RVA: 0x00117AB3 File Offset: 0x00115CB3
		// (set) Token: 0x060042A3 RID: 17059 RVA: 0x00117ABB File Offset: 0x00115CBB
		internal bool TextRunValueReferenced
		{
			get
			{
				return this.m_textRunValueReferenced;
			}
			set
			{
				this.m_textRunValueReferenced = value;
			}
		}

		// Token: 0x17001C07 RID: 7175
		// (get) Token: 0x060042A4 RID: 17060 RVA: 0x00117AC4 File Offset: 0x00115CC4
		// (set) Token: 0x060042A5 RID: 17061 RVA: 0x00117ACC File Offset: 0x00115CCC
		internal int SequenceID
		{
			get
			{
				return this.m_sequenceID;
			}
			set
			{
				this.m_sequenceID = value;
			}
		}

		// Token: 0x060042A6 RID: 17062 RVA: 0x00117AD8 File Offset: 0x00115CD8
		internal override bool Initialize(InitializationContext context)
		{
			context.ObjectType = this.ObjectType;
			context.ObjectName = this.m_name;
			context.ExprHostBuilder.TextBoxStart(this.m_name);
			if (this.m_visibility != null)
			{
				this.m_visibility.Initialize(context);
			}
			bool flag = context.RegisterVisibility(this.m_visibility, this);
			context.RegisterTextBoxInScope(this);
			if (this.m_paragraphs != null)
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Paragraph paragraph in this.m_paragraphs)
				{
					bool flag2;
					this.m_hasValue |= paragraph.Initialize(context, out flag2);
					this.m_hasExpressionBasedValue = this.m_hasExpressionBasedValue || flag2;
				}
			}
			if (this.m_paragraphs.Count == 1)
			{
				this.m_isSimple = this.m_paragraphs[0].DetermineSimplicity();
			}
			base.Initialize(context);
			if (this.m_action != null)
			{
				this.m_action.Initialize(context);
			}
			if (this.m_initialToggleState != null)
			{
				this.m_initialToggleState.Initialize("InitialState", context);
				context.ExprHostBuilder.TextBoxToggleImageInitialState(this.m_initialToggleState);
			}
			if (this.m_userSort != null)
			{
				context.RegisterSortEventSource(this);
			}
			if (this.m_hideDuplicates != null)
			{
				context.ValidateHideDuplicateScope(this.m_hideDuplicates, this);
			}
			context.RegisterToggleItem(this);
			if (flag)
			{
				context.UnRegisterVisibility(this.m_visibility, this);
			}
			base.ExprHostID = context.ExprHostBuilder.TextBoxEnd();
			return true;
		}

		// Token: 0x17001C08 RID: 7176
		// (get) Token: 0x060042A7 RID: 17063 RVA: 0x00117C68 File Offset: 0x00115E68
		Microsoft.ReportingServices.ReportProcessing.ObjectType IInScopeEventSource.ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.Textbox;
			}
		}

		// Token: 0x17001C09 RID: 7177
		// (get) Token: 0x060042A8 RID: 17064 RVA: 0x00117C6B File Offset: 0x00115E6B
		string IInScopeEventSource.Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17001C0A RID: 7178
		// (get) Token: 0x060042A9 RID: 17065 RVA: 0x00117C73 File Offset: 0x00115E73
		Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem IInScopeEventSource.Parent
		{
			get
			{
				return this.m_parent;
			}
		}

		// Token: 0x17001C0B RID: 7179
		// (get) Token: 0x060042AA RID: 17066 RVA: 0x00117C7B File Offset: 0x00115E7B
		EndUserSort IInScopeEventSource.UserSort
		{
			get
			{
				return this.m_userSort;
			}
		}

		// Token: 0x17001C0C RID: 7180
		// (get) Token: 0x060042AB RID: 17067 RVA: 0x00117C83 File Offset: 0x00115E83
		// (set) Token: 0x060042AC RID: 17068 RVA: 0x00117C8B File Offset: 0x00115E8B
		GroupingList IInScopeEventSource.ContainingScopes
		{
			get
			{
				return this.m_containingScopes;
			}
			set
			{
				this.m_containingScopes = value;
			}
		}

		// Token: 0x17001C0D RID: 7181
		// (get) Token: 0x060042AD RID: 17069 RVA: 0x00117C94 File Offset: 0x00115E94
		internal GroupingList ContainingScopes
		{
			get
			{
				return this.m_containingScopes;
			}
		}

		// Token: 0x17001C0E RID: 7182
		// (get) Token: 0x060042AE RID: 17070 RVA: 0x00117C9C File Offset: 0x00115E9C
		// (set) Token: 0x060042AF RID: 17071 RVA: 0x00117CA4 File Offset: 0x00115EA4
		string IInScopeEventSource.Scope
		{
			get
			{
				return this.m_textboxScope;
			}
			set
			{
				this.m_textboxScope = value;
			}
		}

		// Token: 0x17001C0F RID: 7183
		// (get) Token: 0x060042B0 RID: 17072 RVA: 0x00117CAD File Offset: 0x00115EAD
		// (set) Token: 0x060042B1 RID: 17073 RVA: 0x00117CB5 File Offset: 0x00115EB5
		bool IInScopeEventSource.IsTablixCellScope
		{
			get
			{
				return this.m_isTablixCellScope;
			}
			set
			{
				this.m_isTablixCellScope = value;
			}
		}

		// Token: 0x17001C10 RID: 7184
		// (get) Token: 0x060042B2 RID: 17074 RVA: 0x00117CBE File Offset: 0x00115EBE
		// (set) Token: 0x060042B3 RID: 17075 RVA: 0x00117CC6 File Offset: 0x00115EC6
		bool IInScopeEventSource.IsDetailScope
		{
			get
			{
				return this.m_isDetailScope;
			}
			set
			{
				this.m_isDetailScope = value;
			}
		}

		// Token: 0x17001C11 RID: 7185
		// (get) Token: 0x060042B4 RID: 17076 RVA: 0x00117CCF File Offset: 0x00115ECF
		// (set) Token: 0x060042B5 RID: 17077 RVA: 0x00117CD7 File Offset: 0x00115ED7
		bool IInScopeEventSource.IsSubReportTopLevelScope
		{
			get
			{
				return this.m_isSubReportTopLevelScope;
			}
			set
			{
				this.m_isSubReportTopLevelScope = value;
			}
		}

		// Token: 0x17001C12 RID: 7186
		// (get) Token: 0x060042B6 RID: 17078 RVA: 0x00117CE0 File Offset: 0x00115EE0
		// (set) Token: 0x060042B7 RID: 17079 RVA: 0x00117CE8 File Offset: 0x00115EE8
		InitializationContext.ScopeChainInfo IInScopeEventSource.ScopeChainInfo
		{
			get
			{
				return this.m_scopeChainInfo;
			}
			set
			{
				this.m_scopeChainInfo = value;
			}
		}

		// Token: 0x060042B8 RID: 17080 RVA: 0x00117CF4 File Offset: 0x00115EF4
		InScopeSortFilterHashtable IInScopeEventSource.GetSortFiltersInScope(bool create, bool inDetail)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem reportItem = ((IInScopeEventSource)this).Parent;
			if (inDetail)
			{
				while (reportItem != null)
				{
					if (reportItem.IsDataRegion)
					{
						break;
					}
					reportItem = reportItem.Parent;
				}
			}
			else
			{
				while (reportItem != null && !(reportItem is Microsoft.ReportingServices.ReportIntermediateFormat.Report))
				{
					reportItem = reportItem.Parent;
				}
			}
			Global.Tracer.Assert(reportItem.IsDataRegion || reportItem is Microsoft.ReportingServices.ReportIntermediateFormat.Report, "(parent.IsDataRegion || parent is Report)");
			InScopeSortFilterHashtable inScopeSortFilterHashtable;
			if (reportItem is Microsoft.ReportingServices.ReportIntermediateFormat.Report)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.Report report = (Microsoft.ReportingServices.ReportIntermediateFormat.Report)reportItem;
				if (((IInScopeEventSource)this).UserSort.SortExpressionScope == null)
				{
					if (report.DetailSortFiltersInScope == null && create)
					{
						report.DetailSortFiltersInScope = new InScopeSortFilterHashtable();
					}
					inScopeSortFilterHashtable = report.DetailSortFiltersInScope;
				}
				else
				{
					if (report.NonDetailSortFiltersInScope == null && create)
					{
						report.NonDetailSortFiltersInScope = new InScopeSortFilterHashtable();
					}
					inScopeSortFilterHashtable = report.NonDetailSortFiltersInScope;
				}
			}
			else
			{
				Global.Tracer.Assert(((IInScopeEventSource)this).UserSort.SortExpressionScope == null, "(null == eventSource.UserSort.SortExpressionScope)");
				Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegion = (Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion)reportItem;
				if (dataRegion.DetailSortFiltersInScope == null && create)
				{
					dataRegion.DetailSortFiltersInScope = new InScopeSortFilterHashtable();
				}
				inScopeSortFilterHashtable = dataRegion.DetailSortFiltersInScope;
			}
			return inScopeSortFilterHashtable;
		}

		// Token: 0x060042B9 RID: 17081 RVA: 0x00117E00 File Offset: 0x00116000
		List<int> IInScopeEventSource.GetPeerSortFilters(bool create)
		{
			EndUserSort userSort = ((IInScopeEventSource)this).UserSort;
			if (userSort == null)
			{
				return null;
			}
			List<int> list = null;
			InScopeSortFilterHashtable inScopeSortFilterHashtable;
			if (((IInScopeEventSource)this).ContainingScopes == null || ((IInScopeEventSource)this).ContainingScopes.Count == 0 || ((IInScopeEventSource)this).IsSubReportTopLevelScope)
			{
				inScopeSortFilterHashtable = ((IInScopeEventSource)this).GetSortFiltersInScope(create, false);
			}
			else
			{
				Grouping lastEntry = ((IInScopeEventSource)this).ContainingScopes.LastEntry;
				if (lastEntry == null)
				{
					inScopeSortFilterHashtable = ((IInScopeEventSource)this).GetSortFiltersInScope(create, true);
				}
				else if (userSort.SortExpressionScope == null)
				{
					if (lastEntry.DetailSortFiltersInScope == null && create)
					{
						lastEntry.DetailSortFiltersInScope = new InScopeSortFilterHashtable();
					}
					inScopeSortFilterHashtable = lastEntry.DetailSortFiltersInScope;
				}
				else
				{
					if (lastEntry.NonDetailSortFiltersInScope == null && create)
					{
						lastEntry.NonDetailSortFiltersInScope = new InScopeSortFilterHashtable();
					}
					inScopeSortFilterHashtable = lastEntry.NonDetailSortFiltersInScope;
				}
			}
			if (inScopeSortFilterHashtable != null)
			{
				int num;
				if (userSort.SortExpressionScope != null)
				{
					num = userSort.SortExpressionScope.ID;
				}
				else
				{
					num = userSort.SortTarget.ID;
				}
				list = inScopeSortFilterHashtable[num];
				if (list == null && create)
				{
					list = new List<int>();
					inScopeSortFilterHashtable.Add(num, list);
				}
			}
			return list;
		}

		// Token: 0x060042BA RID: 17082 RVA: 0x00117EFE File Offset: 0x001160FE
		internal string GetRecursiveUniqueName(int parentInstanceIndex)
		{
			return InstancePathItem.GenerateUniqueNameString(base.ID, this.InstancePath, parentInstanceIndex);
		}

		// Token: 0x060042BB RID: 17083 RVA: 0x00117F14 File Offset: 0x00116114
		internal bool EvaluateIsToggle(IReportScopeInstance romInstance, OnDemandProcessingContext context)
		{
			bool flag = this.IsToggle;
			if (flag && this.RecursiveSender && !this.HasNonRecursiveSender)
			{
				if (this.m_recursiveMember != null)
				{
					context.SetupContext(this, romInstance);
					return this.m_recursiveMember.InstanceHasRecursiveChildren;
				}
				flag = true;
			}
			return flag;
		}

		// Token: 0x060042BC RID: 17084 RVA: 0x00117F5C File Offset: 0x0011615C
		protected override void DataRendererInitialize(InitializationContext context)
		{
			base.DataRendererInitialize(context);
			if (!this.m_overrideReportDataElementStyle)
			{
				this.m_dataElementStyleAttribute = context.ReportDataElementStyleAttribute;
			}
		}

		// Token: 0x060042BD RID: 17085 RVA: 0x00117F7A File Offset: 0x0011617A
		internal override void InitializeRVDirectionDependentItems(InitializationContext context)
		{
			if (this.m_userSort != null)
			{
				context.ProcessSortEventSource(this);
			}
		}

		// Token: 0x060042BE RID: 17086 RVA: 0x00117F8C File Offset: 0x0011618C
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.TextBox textBox = (Microsoft.ReportingServices.ReportIntermediateFormat.TextBox)base.PublishClone(context);
			textBox.m_sequenceID = context.GenerateTextboxSequenceID();
			if (this.m_paragraphs != null)
			{
				textBox.m_paragraphs = new List<Microsoft.ReportingServices.ReportIntermediateFormat.Paragraph>(this.m_paragraphs.Count);
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Paragraph paragraph in this.m_paragraphs)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.Paragraph paragraph2 = (Microsoft.ReportingServices.ReportIntermediateFormat.Paragraph)paragraph.PublishClone(context);
					paragraph2.TextBox = textBox;
					textBox.m_paragraphs.Add(paragraph2);
				}
			}
			if (this.m_hideDuplicates != null)
			{
				textBox.m_hideDuplicates = context.GetNewScopeName(this.m_hideDuplicates);
			}
			if (this.m_action != null)
			{
				textBox.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)this.m_action.PublishClone(context);
			}
			if (this.m_initialToggleState != null)
			{
				textBox.m_initialToggleState = (ExpressionInfo)this.m_initialToggleState.PublishClone(context);
			}
			if (this.m_userSort != null)
			{
				textBox.m_userSort = (EndUserSort)this.m_userSort.PublishClone(context);
			}
			return textBox;
		}

		// Token: 0x060042BF RID: 17087 RVA: 0x001180A8 File Offset: 0x001162A8
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TextBox, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportItem, new List<MemberInfo>
			{
				new ReadOnlyMemberInfo(MemberName.Value, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new ReadOnlyMemberInfo(MemberName.DataType, Token.Enum),
				new MemberInfo(MemberName.Paragraphs, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Paragraph),
				new MemberInfo(MemberName.CanGrow, Token.Boolean),
				new MemberInfo(MemberName.CanShrink, Token.Boolean),
				new MemberInfo(MemberName.HideDuplicates, Token.String),
				new MemberInfo(MemberName.Action, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Action),
				new MemberInfo(MemberName.IsToggle, Token.Boolean),
				new MemberInfo(MemberName.InitialToggleState, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ValueReferenced, Token.Boolean),
				new MemberInfo(MemberName.RecursiveSender, Token.Boolean),
				new MemberInfo(MemberName.DataElementStyleAttribute, Token.Boolean),
				new MemberInfo(MemberName.ContainingScopes, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Token.Reference, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Grouping),
				new MemberInfo(MemberName.UserSort, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.EndUserSort),
				new MemberInfo(MemberName.IsTablixCellScope, Token.Boolean),
				new MemberInfo(MemberName.IsSubReportTopLevelScope, Token.Boolean),
				new MemberInfo(MemberName.KeepTogether, Token.Boolean),
				new MemberInfo(MemberName.SequenceID, Token.Int32),
				new MemberInfo(MemberName.RecursiveMember, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TablixMember, Token.Reference),
				new MemberInfo(MemberName.HasExpressionBasedValue, Token.Boolean),
				new MemberInfo(MemberName.HasValue, Token.Boolean),
				new MemberInfo(MemberName.IsSimple, Token.Boolean),
				new MemberInfo(MemberName.TextRunValueReferenced, Token.Boolean),
				new MemberInfo(MemberName.HasNonRecursiveSender, Token.Boolean),
				new MemberInfo(MemberName.CanScrollVertically, Token.Boolean),
				new MemberInfo(MemberName.StructureTypeOverwrite, Token.Enum)
			});
		}

		// Token: 0x060042C0 RID: 17088 RVA: 0x001182E4 File Offset: 0x001164E4
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.TextBox.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.IsTablixCellScope)
				{
					if (memberName <= MemberName.RecursiveSender)
					{
						if (memberName <= MemberName.KeepTogether)
						{
							switch (memberName)
							{
							case MemberName.HideDuplicates:
								writer.Write(this.m_hideDuplicates);
								continue;
							case MemberName.CanGrow:
								writer.Write(this.m_canGrow);
								continue;
							case MemberName.CanShrink:
								writer.Write(this.m_canShrink);
								continue;
							case MemberName.IsToggle:
								writer.Write(this.m_isToggle);
								continue;
							case MemberName.InitialToggleState:
								writer.Write(this.m_initialToggleState);
								continue;
							default:
								if (memberName == MemberName.KeepTogether)
								{
									writer.Write(this.m_keepTogether);
									continue;
								}
								break;
							}
						}
						else
						{
							if (memberName == MemberName.ValueReferenced)
							{
								writer.Write(this.m_valueReferenced);
								continue;
							}
							if (memberName == MemberName.RecursiveSender)
							{
								writer.Write(this.m_recursiveSender);
								continue;
							}
						}
					}
					else if (memberName <= MemberName.DataElementStyleAttribute)
					{
						if (memberName == MemberName.Action)
						{
							writer.Write(this.m_action);
							continue;
						}
						if (memberName == MemberName.DataElementStyleAttribute)
						{
							writer.Write(this.m_dataElementStyleAttribute);
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.ContainingScopes)
						{
							writer.WriteListOfReferences(this.m_containingScopes);
							continue;
						}
						if (memberName == MemberName.UserSort)
						{
							writer.Write(this.m_userSort);
							continue;
						}
						if (memberName == MemberName.IsTablixCellScope)
						{
							writer.Write(this.m_isTablixCellScope);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.RecursiveMember)
				{
					if (memberName <= MemberName.IsSimple)
					{
						if (memberName == MemberName.IsSubReportTopLevelScope)
						{
							writer.Write(this.m_isSubReportTopLevelScope);
							continue;
						}
						if (memberName == MemberName.IsSimple)
						{
							writer.Write(this.m_isSimple);
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.SequenceID)
						{
							writer.Write(this.m_sequenceID);
							continue;
						}
						if (memberName == MemberName.RecursiveMember)
						{
							writer.WriteReference(this.m_recursiveMember);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.TextRunValueReferenced)
				{
					if (memberName == MemberName.Paragraphs)
					{
						writer.Write<Microsoft.ReportingServices.ReportIntermediateFormat.Paragraph>(this.m_paragraphs);
						continue;
					}
					switch (memberName)
					{
					case MemberName.HasExpressionBasedValue:
						writer.Write(this.m_hasExpressionBasedValue);
						continue;
					case MemberName.HasValue:
						writer.Write(this.m_hasValue);
						continue;
					case MemberName.TextRunValueReferenced:
						writer.Write(this.m_textRunValueReferenced);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.HasNonRecursiveSender)
					{
						writer.Write(this.m_hasNonRecursiveSender);
						continue;
					}
					if (memberName == MemberName.CanScrollVertically)
					{
						writer.Write(this.m_canScrollVertically);
						continue;
					}
					if (memberName == MemberName.StructureTypeOverwrite)
					{
						writer.WriteEnum((int)this.m_structureTypeOverwrite);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060042C1 RID: 17089 RVA: 0x00118608 File Offset: 0x00116808
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.TextBox.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.UserSort)
				{
					if (memberName <= MemberName.ValueReferenced)
					{
						if (memberName <= MemberName.DataType)
						{
							if (memberName != MemberName.Value)
							{
								if (memberName == MemberName.DataType)
								{
									this.GetOrCreateSingleTextRun(reader).DataType = (DataType)reader.ReadEnum();
									continue;
								}
							}
							else
							{
								Microsoft.ReportingServices.ReportIntermediateFormat.TextRun orCreateSingleTextRun = this.GetOrCreateSingleTextRun(reader);
								ExpressionInfo expressionInfo = (ExpressionInfo)reader.ReadRIFObject();
								this.m_hasValue = true;
								this.m_hasExpressionBasedValue = expressionInfo.IsExpression;
								orCreateSingleTextRun.Value = expressionInfo;
								if (this.m_styleClass != null)
								{
									orCreateSingleTextRun.Paragraph.StyleClass = new ParagraphFilteredStyle(this.m_styleClass);
									orCreateSingleTextRun.StyleClass = new TextRunFilteredStyle(this.m_styleClass);
									this.m_styleClass = new TextBoxFilteredStyle(this.m_styleClass);
									continue;
								}
								continue;
							}
						}
						else
						{
							switch (memberName)
							{
							case MemberName.HideDuplicates:
								this.m_hideDuplicates = reader.ReadString();
								continue;
							case MemberName.CanGrow:
								this.m_canGrow = reader.ReadBoolean();
								continue;
							case MemberName.CanShrink:
								this.m_canShrink = reader.ReadBoolean();
								continue;
							case MemberName.IsToggle:
								this.m_isToggle = reader.ReadBoolean();
								continue;
							case MemberName.InitialToggleState:
								this.m_initialToggleState = (ExpressionInfo)reader.ReadRIFObject();
								continue;
							default:
								if (memberName == MemberName.KeepTogether)
								{
									this.m_keepTogether = reader.ReadBoolean();
									continue;
								}
								if (memberName == MemberName.ValueReferenced)
								{
									this.m_valueReferenced = reader.ReadBoolean();
									continue;
								}
								break;
							}
						}
					}
					else if (memberName <= MemberName.Action)
					{
						if (memberName == MemberName.RecursiveSender)
						{
							this.m_recursiveSender = reader.ReadBoolean();
							continue;
						}
						if (memberName == MemberName.Action)
						{
							this.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)reader.ReadRIFObject();
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.DataElementStyleAttribute)
						{
							this.m_dataElementStyleAttribute = reader.ReadBoolean();
							continue;
						}
						if (memberName != MemberName.ContainingScopes)
						{
							if (memberName == MemberName.UserSort)
							{
								this.m_userSort = (EndUserSort)reader.ReadRIFObject();
								continue;
							}
						}
						else
						{
							if (reader.ReadListOfReferencesNoResolution(this) == 0)
							{
								this.m_containingScopes = new GroupingList();
								continue;
							}
							continue;
						}
					}
				}
				else if (memberName <= MemberName.RecursiveMember)
				{
					if (memberName <= MemberName.IsSubReportTopLevelScope)
					{
						if (memberName == MemberName.IsTablixCellScope)
						{
							this.m_isTablixCellScope = reader.ReadBoolean();
							continue;
						}
						if (memberName == MemberName.IsSubReportTopLevelScope)
						{
							this.m_isSubReportTopLevelScope = reader.ReadBoolean();
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.IsSimple)
						{
							this.m_isSimple = reader.ReadBoolean();
							continue;
						}
						if (memberName == MemberName.SequenceID)
						{
							this.m_sequenceID = reader.ReadInt32();
							continue;
						}
						if (memberName == MemberName.RecursiveMember)
						{
							this.m_recursiveMember = reader.ReadReference<TablixMember>(this);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.TextRunValueReferenced)
				{
					if (memberName == MemberName.Paragraphs)
					{
						this.m_paragraphs = reader.ReadGenericListOfRIFObjects<Microsoft.ReportingServices.ReportIntermediateFormat.Paragraph>();
						continue;
					}
					switch (memberName)
					{
					case MemberName.HasExpressionBasedValue:
						this.m_hasExpressionBasedValue = reader.ReadBoolean();
						continue;
					case MemberName.HasValue:
						this.m_hasValue = reader.ReadBoolean();
						continue;
					case MemberName.TextRunValueReferenced:
						this.m_textRunValueReferenced = reader.ReadBoolean();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.HasNonRecursiveSender)
					{
						this.m_hasNonRecursiveSender = reader.ReadBoolean();
						continue;
					}
					if (memberName == MemberName.CanScrollVertically)
					{
						this.m_canScrollVertically = reader.ReadBoolean();
						continue;
					}
					if (memberName == MemberName.StructureTypeOverwrite)
					{
						this.m_structureTypeOverwrite = (StructureTypeOverwriteType)reader.ReadEnum();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060042C2 RID: 17090 RVA: 0x001189E8 File Offset: 0x00116BE8
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(Microsoft.ReportingServices.ReportIntermediateFormat.TextBox.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					MemberName memberName = memberReference.MemberName;
					if (memberName != MemberName.ContainingScopes)
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable referenceable;
						if (memberName != MemberName.RecursiveMember)
						{
							Global.Tracer.Assert(false);
						}
						else if (referenceableItems.TryGetValue(memberReference.RefID, out referenceable))
						{
							this.m_recursiveMember = referenceable as TablixMember;
						}
					}
					else
					{
						if (this.m_containingScopes == null)
						{
							this.m_containingScopes = new GroupingList();
						}
						if (memberReference.RefID != -2)
						{
							Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
							Global.Tracer.Assert(referenceableItems[memberReference.RefID] is Grouping);
							Global.Tracer.Assert(!this.m_containingScopes.Contains((Grouping)referenceableItems[memberReference.RefID]));
							this.m_containingScopes.Add((Grouping)referenceableItems[memberReference.RefID]);
						}
						else
						{
							this.m_containingScopes.Add(null);
						}
					}
				}
			}
		}

		// Token: 0x060042C3 RID: 17091 RVA: 0x00118B54 File Offset: 0x00116D54
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TextBox;
		}

		// Token: 0x060042C4 RID: 17092 RVA: 0x00118B5C File Offset: 0x00116D5C
		private Microsoft.ReportingServices.ReportIntermediateFormat.TextRun GetOrCreateSingleTextRun(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			if (this.m_paragraphs == null)
			{
				this.m_isSimple = true;
				this.m_paragraphs = new List<Microsoft.ReportingServices.ReportIntermediateFormat.Paragraph>(1);
				Microsoft.ReportingServices.ReportIntermediateFormat.Paragraph paragraph = new Microsoft.ReportingServices.ReportIntermediateFormat.Paragraph(this, 0, -1);
				paragraph.GlobalID = -1;
				this.m_paragraphs.Add(paragraph);
				List<Microsoft.ReportingServices.ReportIntermediateFormat.TextRun> list = new List<Microsoft.ReportingServices.ReportIntermediateFormat.TextRun>(1);
				Microsoft.ReportingServices.ReportIntermediateFormat.TextRun textRun = new Microsoft.ReportingServices.ReportIntermediateFormat.TextRun(paragraph, 0, -1);
				textRun.GlobalID = -1;
				list.Add(textRun);
				paragraph.TextRuns = list;
				return textRun;
			}
			return this.m_paragraphs[0].TextRuns[0];
		}

		// Token: 0x060042C5 RID: 17093 RVA: 0x00118BE0 File Offset: 0x00116DE0
		internal override void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			if (base.ExprHostID >= 0)
			{
				Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null, "(reportExprHost != null && reportObjectModel != null)");
				this.m_exprHost = reportExprHost.TextBoxHostsRemotable[base.ExprHostID];
				base.ReportItemSetExprHost(this.m_exprHost, reportObjectModel);
				if (this.m_action != null && this.m_exprHost.ActionInfoHost != null)
				{
					this.m_action.SetExprHost(this.m_exprHost.ActionInfoHost, reportObjectModel);
				}
				if (this.m_paragraphs != null)
				{
					foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Paragraph paragraph in this.m_paragraphs)
					{
						paragraph.SetExprHost(this.m_exprHost, reportObjectModel);
					}
				}
			}
		}

		// Token: 0x060042C6 RID: 17094 RVA: 0x00118CB4 File Offset: 0x00116EB4
		internal bool EvaluateInitialToggleState(IReportScopeInstance romInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, romInstance);
			return context.ReportRuntime.EvaluateTextBoxInitialToggleStateExpression(this);
		}

		// Token: 0x060042C7 RID: 17095 RVA: 0x00118CCA File Offset: 0x00116ECA
		internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluateValue(IReportScopeInstance romInstance, OnDemandProcessingContext context)
		{
			return this.GetTextBoxImpl(context).GetResult(romInstance, false);
		}

		// Token: 0x060042C8 RID: 17096 RVA: 0x00118CDA File Offset: 0x00116EDA
		internal List<string> GetFieldsUsedInValueExpression(IReportScopeInstance romInstance, OnDemandProcessingContext context)
		{
			return this.GetTextBoxImpl(context).GetFieldsUsedInValueExpression(romInstance);
		}

		// Token: 0x060042C9 RID: 17097 RVA: 0x00118CEC File Offset: 0x00116EEC
		internal TextBoxImpl GetTextBoxImpl(OnDemandProcessingContext context)
		{
			if (this.m_textBoxImpl == null)
			{
				Microsoft.ReportingServices.RdlExpressions.ReportRuntime reportRuntime = context.ReportRuntime;
				ReportItemsImpl reportItemsImpl = context.ReportObjectModel.ReportItemsImpl;
				this.m_textBoxImpl = (TextBoxImpl)reportItemsImpl.GetReportItem(this.m_name);
				Global.Tracer.Assert(this.m_textBoxImpl != null, "(m_textBoxImpl != null)");
			}
			return this.m_textBoxImpl;
		}

		// Token: 0x060042CA RID: 17098 RVA: 0x00118D49 File Offset: 0x00116F49
		internal void ResetTextBoxImpl(OnDemandProcessingContext context)
		{
			this.GetTextBoxImpl(context).Reset();
		}

		// Token: 0x060042CB RID: 17099 RVA: 0x00118D57 File Offset: 0x00116F57
		internal void ResetDuplicates()
		{
			this.m_hasOldResult = false;
		}

		// Token: 0x060042CC RID: 17100 RVA: 0x00118D60 File Offset: 0x00116F60
		internal bool CalculateDuplicates(Microsoft.ReportingServices.RdlExpressions.VariantResult currentResult, OnDemandProcessingContext context)
		{
			bool flag = false;
			if (this.m_hideDuplicates != null)
			{
				if (this.m_hasOldResult)
				{
					if (currentResult.ErrorOccurred && this.m_oldResult.ErrorOccurred)
					{
						flag = true;
					}
					else if (currentResult.ErrorOccurred)
					{
						flag = false;
					}
					else if (this.m_oldResult.ErrorOccurred)
					{
						flag = false;
					}
					else if (currentResult.Value == null && this.m_oldResult.Value == null)
					{
						flag = true;
					}
					else if (currentResult.Value == null)
					{
						flag = false;
					}
					else
					{
						bool flag2;
						flag = ReportProcessing.CompareTo(currentResult.Value, this.m_oldResult.Value, (context.CurrentOdpDataSetInstance != null) ? context.CurrentOdpDataSetInstance.DataSetDef.NullsAsBlanks : context.NullsAsBlanks, (context.CurrentOdpDataSetInstance != null) ? context.CurrentOdpDataSetInstance.CompareInfo : context.CompareInfo, (context.CurrentOdpDataSetInstance != null) ? context.CurrentOdpDataSetInstance.ClrCompareOptions : context.ClrCompareOptions, false, false, out flag2) == 0;
						if (!flag2)
						{
							flag = false;
						}
					}
				}
				if (!flag)
				{
					this.m_hasOldResult = true;
					this.m_oldResult = currentResult;
				}
			}
			return flag;
		}

		// Token: 0x04001E61 RID: 7777
		private List<Microsoft.ReportingServices.ReportIntermediateFormat.Paragraph> m_paragraphs;

		// Token: 0x04001E62 RID: 7778
		private Microsoft.ReportingServices.ReportIntermediateFormat.Action m_action;

		// Token: 0x04001E63 RID: 7779
		private bool m_canGrow;

		// Token: 0x04001E64 RID: 7780
		private bool m_canShrink;

		// Token: 0x04001E65 RID: 7781
		private string m_hideDuplicates;

		// Token: 0x04001E66 RID: 7782
		private StructureTypeOverwriteType m_structureTypeOverwrite;

		// Token: 0x04001E67 RID: 7783
		private bool m_isToggle;

		// Token: 0x04001E68 RID: 7784
		private ExpressionInfo m_initialToggleState;

		// Token: 0x04001E69 RID: 7785
		private bool m_valueReferenced;

		// Token: 0x04001E6A RID: 7786
		private bool m_textRunValueReferenced;

		// Token: 0x04001E6B RID: 7787
		private bool m_recursiveSender;

		// Token: 0x04001E6C RID: 7788
		private bool m_hasNonRecursiveSender;

		// Token: 0x04001E6D RID: 7789
		[Reference]
		private TablixMember m_recursiveMember;

		// Token: 0x04001E6E RID: 7790
		private bool m_dataElementStyleAttribute = true;

		// Token: 0x04001E6F RID: 7791
		private bool m_hasValue;

		// Token: 0x04001E70 RID: 7792
		private bool m_hasExpressionBasedValue;

		// Token: 0x04001E71 RID: 7793
		private bool m_keepTogether;

		// Token: 0x04001E72 RID: 7794
		private bool m_canScrollVertically;

		// Token: 0x04001E73 RID: 7795
		[Reference]
		private GroupingList m_containingScopes;

		// Token: 0x04001E74 RID: 7796
		private EndUserSort m_userSort;

		// Token: 0x04001E75 RID: 7797
		private bool m_isTablixCellScope;

		// Token: 0x04001E76 RID: 7798
		private int m_sequenceID = -1;

		// Token: 0x04001E77 RID: 7799
		private bool m_isSimple;

		// Token: 0x04001E78 RID: 7800
		[NonSerialized]
		private InitializationContext.ScopeChainInfo m_scopeChainInfo;

		// Token: 0x04001E79 RID: 7801
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Microsoft.ReportingServices.ReportIntermediateFormat.TextBox.GetDeclaration();

		// Token: 0x04001E7A RID: 7802
		private bool m_isSubReportTopLevelScope;

		// Token: 0x04001E7B RID: 7803
		[NonSerialized]
		private bool m_overrideReportDataElementStyle;

		// Token: 0x04001E7C RID: 7804
		[NonSerialized]
		private string m_textboxScope;

		// Token: 0x04001E7D RID: 7805
		[NonSerialized]
		private bool m_isDetailScope;

		// Token: 0x04001E7E RID: 7806
		[NonSerialized]
		private Microsoft.ReportingServices.RdlExpressions.VariantResult m_oldResult;

		// Token: 0x04001E7F RID: 7807
		[NonSerialized]
		private bool m_hasOldResult;

		// Token: 0x04001E80 RID: 7808
		[NonSerialized]
		private TextBoxExprHost m_exprHost;

		// Token: 0x04001E81 RID: 7809
		[NonSerialized]
		private List<string> m_fieldsUsedInValueExpression;

		// Token: 0x04001E82 RID: 7810
		[NonSerialized]
		private TextBoxImpl m_textBoxImpl;
	}
}
