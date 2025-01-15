using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006E6 RID: 1766
	[Serializable]
	internal sealed class TextBox : Microsoft.ReportingServices.ReportProcessing.ReportItem, IActionOwner
	{
		// Token: 0x0600603F RID: 24639 RVA: 0x0018436D File Offset: 0x0018256D
		internal TextBox(Microsoft.ReportingServices.ReportProcessing.ReportItem parent)
			: base(parent)
		{
		}

		// Token: 0x06006040 RID: 24640 RVA: 0x0018438C File Offset: 0x0018258C
		internal TextBox(int id, Microsoft.ReportingServices.ReportProcessing.ReportItem parent)
			: base(id, parent)
		{
		}

		// Token: 0x170021D9 RID: 8665
		// (get) Token: 0x06006041 RID: 24641 RVA: 0x001843AC File Offset: 0x001825AC
		internal override ObjectType ObjectType
		{
			get
			{
				return ObjectType.Textbox;
			}
		}

		// Token: 0x170021DA RID: 8666
		// (get) Token: 0x06006042 RID: 24642 RVA: 0x001843AF File Offset: 0x001825AF
		// (set) Token: 0x06006043 RID: 24643 RVA: 0x001843B7 File Offset: 0x001825B7
		internal ExpressionInfo Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x170021DB RID: 8667
		// (get) Token: 0x06006044 RID: 24644 RVA: 0x001843C0 File Offset: 0x001825C0
		// (set) Token: 0x06006045 RID: 24645 RVA: 0x001843C8 File Offset: 0x001825C8
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

		// Token: 0x170021DC RID: 8668
		// (get) Token: 0x06006046 RID: 24646 RVA: 0x001843D1 File Offset: 0x001825D1
		// (set) Token: 0x06006047 RID: 24647 RVA: 0x001843D9 File Offset: 0x001825D9
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

		// Token: 0x170021DD RID: 8669
		// (get) Token: 0x06006048 RID: 24648 RVA: 0x001843E2 File Offset: 0x001825E2
		// (set) Token: 0x06006049 RID: 24649 RVA: 0x001843EA File Offset: 0x001825EA
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

		// Token: 0x170021DE RID: 8670
		// (get) Token: 0x0600604A RID: 24650 RVA: 0x001843F3 File Offset: 0x001825F3
		// (set) Token: 0x0600604B RID: 24651 RVA: 0x001843FB File Offset: 0x001825FB
		internal Microsoft.ReportingServices.ReportProcessing.Action Action
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

		// Token: 0x170021DF RID: 8671
		// (get) Token: 0x0600604C RID: 24652 RVA: 0x00184404 File Offset: 0x00182604
		// (set) Token: 0x0600604D RID: 24653 RVA: 0x0018440C File Offset: 0x0018260C
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

		// Token: 0x170021E0 RID: 8672
		// (get) Token: 0x0600604E RID: 24654 RVA: 0x00184415 File Offset: 0x00182615
		// (set) Token: 0x0600604F RID: 24655 RVA: 0x0018441D File Offset: 0x0018261D
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

		// Token: 0x170021E1 RID: 8673
		// (get) Token: 0x06006050 RID: 24656 RVA: 0x00184426 File Offset: 0x00182626
		// (set) Token: 0x06006051 RID: 24657 RVA: 0x0018442E File Offset: 0x0018262E
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

		// Token: 0x170021E2 RID: 8674
		// (get) Token: 0x06006052 RID: 24658 RVA: 0x00184437 File Offset: 0x00182637
		// (set) Token: 0x06006053 RID: 24659 RVA: 0x0018443F File Offset: 0x0018263F
		internal TypeCode ValueType
		{
			get
			{
				return this.m_valueType;
			}
			set
			{
				this.m_valueType = value;
			}
		}

		// Token: 0x170021E3 RID: 8675
		// (get) Token: 0x06006054 RID: 24660 RVA: 0x00184448 File Offset: 0x00182648
		// (set) Token: 0x06006055 RID: 24661 RVA: 0x00184450 File Offset: 0x00182650
		internal VariantResult OldResult
		{
			get
			{
				return this.m_oldResult;
			}
			set
			{
				this.m_oldResult = value;
				this.m_hasOldResult = true;
			}
		}

		// Token: 0x170021E4 RID: 8676
		// (get) Token: 0x06006056 RID: 24662 RVA: 0x00184460 File Offset: 0x00182660
		// (set) Token: 0x06006057 RID: 24663 RVA: 0x00184468 File Offset: 0x00182668
		internal bool IsSubReportTopLevelScope
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

		// Token: 0x170021E5 RID: 8677
		// (get) Token: 0x06006058 RID: 24664 RVA: 0x00184471 File Offset: 0x00182671
		// (set) Token: 0x06006059 RID: 24665 RVA: 0x00184479 File Offset: 0x00182679
		internal bool HasOldResult
		{
			get
			{
				return this.m_hasOldResult;
			}
			set
			{
				this.m_hasOldResult = value;
			}
		}

		// Token: 0x170021E6 RID: 8678
		// (get) Token: 0x0600605A RID: 24666 RVA: 0x00184482 File Offset: 0x00182682
		// (set) Token: 0x0600605B RID: 24667 RVA: 0x0018448A File Offset: 0x0018268A
		internal bool SharedFormatSettings
		{
			get
			{
				return this.m_sharedFormatSettings;
			}
			set
			{
				this.m_sharedFormatSettings = value;
			}
		}

		// Token: 0x170021E7 RID: 8679
		// (get) Token: 0x0600605C RID: 24668 RVA: 0x00184493 File Offset: 0x00182693
		// (set) Token: 0x0600605D RID: 24669 RVA: 0x0018449B File Offset: 0x0018269B
		internal string FormattedValue
		{
			get
			{
				return this.m_formattedValue;
			}
			set
			{
				this.m_formattedValue = value;
			}
		}

		// Token: 0x170021E8 RID: 8680
		// (get) Token: 0x0600605E RID: 24670 RVA: 0x001844A4 File Offset: 0x001826A4
		// (set) Token: 0x0600605F RID: 24671 RVA: 0x001844AC File Offset: 0x001826AC
		internal string Formula
		{
			get
			{
				return this.m_formula;
			}
			set
			{
				this.m_formula = value;
			}
		}

		// Token: 0x170021E9 RID: 8681
		// (get) Token: 0x06006060 RID: 24672 RVA: 0x001844B5 File Offset: 0x001826B5
		// (set) Token: 0x06006061 RID: 24673 RVA: 0x001844BD File Offset: 0x001826BD
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

		// Token: 0x170021EA RID: 8682
		// (get) Token: 0x06006062 RID: 24674 RVA: 0x001844C6 File Offset: 0x001826C6
		// (set) Token: 0x06006063 RID: 24675 RVA: 0x001844CE File Offset: 0x001826CE
		internal bool CalendarValidated
		{
			get
			{
				return this.m_calendarValidated;
			}
			set
			{
				this.m_calendarValidated = value;
			}
		}

		// Token: 0x170021EB RID: 8683
		// (get) Token: 0x06006064 RID: 24676 RVA: 0x001844D7 File Offset: 0x001826D7
		// (set) Token: 0x06006065 RID: 24677 RVA: 0x001844DF File Offset: 0x001826DF
		internal Calendar Calendar
		{
			get
			{
				return this.m_calendar;
			}
			set
			{
				this.m_calendar = value;
			}
		}

		// Token: 0x170021EC RID: 8684
		// (get) Token: 0x06006066 RID: 24678 RVA: 0x001844E8 File Offset: 0x001826E8
		// (set) Token: 0x06006067 RID: 24679 RVA: 0x001844F0 File Offset: 0x001826F0
		internal uint LanguageInstanceId
		{
			get
			{
				return this.m_languageInstanceId;
			}
			set
			{
				this.m_languageInstanceId = value;
			}
		}

		// Token: 0x170021ED RID: 8685
		// (get) Token: 0x06006068 RID: 24680 RVA: 0x001844F9 File Offset: 0x001826F9
		internal TextBoxExprHost TextBoxExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x170021EE RID: 8686
		// (get) Token: 0x06006069 RID: 24681 RVA: 0x00184501 File Offset: 0x00182701
		// (set) Token: 0x0600606A RID: 24682 RVA: 0x00184509 File Offset: 0x00182709
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

		// Token: 0x170021EF RID: 8687
		// (get) Token: 0x0600606B RID: 24683 RVA: 0x00184512 File Offset: 0x00182712
		// (set) Token: 0x0600606C RID: 24684 RVA: 0x0018451A File Offset: 0x0018271A
		internal GroupingList ContainingScopes
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

		// Token: 0x170021F0 RID: 8688
		// (get) Token: 0x0600606D RID: 24685 RVA: 0x00184523 File Offset: 0x00182723
		// (set) Token: 0x0600606E RID: 24686 RVA: 0x0018452B File Offset: 0x0018272B
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

		// Token: 0x170021F1 RID: 8689
		// (get) Token: 0x0600606F RID: 24687 RVA: 0x00184534 File Offset: 0x00182734
		// (set) Token: 0x06006070 RID: 24688 RVA: 0x0018453C File Offset: 0x0018273C
		internal bool IsMatrixCellScope
		{
			get
			{
				return this.m_isMatrixCellScope;
			}
			set
			{
				this.m_isMatrixCellScope = value;
			}
		}

		// Token: 0x170021F2 RID: 8690
		// (get) Token: 0x06006071 RID: 24689 RVA: 0x00184545 File Offset: 0x00182745
		// (set) Token: 0x06006072 RID: 24690 RVA: 0x0018454D File Offset: 0x0018274D
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

		// Token: 0x170021F3 RID: 8691
		// (get) Token: 0x06006073 RID: 24691 RVA: 0x00184556 File Offset: 0x00182756
		internal override DataElementOutputTypes DataElementOutputDefault
		{
			get
			{
				if (Microsoft.ReportingServices.ReportProcessing.ReportItem.DataElementOutputTypesRDL.Auto == this.m_dataElementOutputRDL && this.m_value != null && ExpressionInfo.Types.Constant == this.m_value.Type)
				{
					return DataElementOutputTypes.NoOutput;
				}
				return DataElementOutputTypes.Output;
			}
		}

		// Token: 0x170021F4 RID: 8692
		// (get) Token: 0x06006074 RID: 24692 RVA: 0x0018457A File Offset: 0x0018277A
		// (set) Token: 0x06006075 RID: 24693 RVA: 0x00184582 File Offset: 0x00182782
		internal string TextBoxScope
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

		// Token: 0x170021F5 RID: 8693
		// (get) Token: 0x06006076 RID: 24694 RVA: 0x0018458B File Offset: 0x0018278B
		// (set) Token: 0x06006077 RID: 24695 RVA: 0x00184593 File Offset: 0x00182793
		internal bool IsDetailScope
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

		// Token: 0x170021F6 RID: 8694
		// (get) Token: 0x06006078 RID: 24696 RVA: 0x0018459C File Offset: 0x0018279C
		// (set) Token: 0x06006079 RID: 24697 RVA: 0x001845A4 File Offset: 0x001827A4
		internal int TableColumnPosition
		{
			get
			{
				return this.m_tableColumnPosition;
			}
			set
			{
				this.m_tableColumnPosition = value;
			}
		}

		// Token: 0x170021F7 RID: 8695
		// (get) Token: 0x0600607A RID: 24698 RVA: 0x001845AD File Offset: 0x001827AD
		Microsoft.ReportingServices.ReportProcessing.Action IActionOwner.Action
		{
			get
			{
				return this.m_action;
			}
		}

		// Token: 0x170021F8 RID: 8696
		// (get) Token: 0x0600607B RID: 24699 RVA: 0x001845B5 File Offset: 0x001827B5
		// (set) Token: 0x0600607C RID: 24700 RVA: 0x001845BD File Offset: 0x001827BD
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

		// Token: 0x0600607D RID: 24701 RVA: 0x001845C8 File Offset: 0x001827C8
		internal override bool Initialize(InitializationContext context)
		{
			context.ObjectType = this.ObjectType;
			context.ObjectName = this.m_name;
			context.ExprHostBuilder.TextBoxStart(this.m_name);
			base.Initialize(context);
			if (this.m_visibility != null)
			{
				this.m_visibility.Initialize(context, false, false);
			}
			if (this.m_value != null)
			{
				this.m_value.Initialize("Value", context);
				context.ExprHostBuilder.GenericValue(this.m_value);
			}
			if (this.m_action != null)
			{
				this.m_action.Initialize(context);
			}
			if (this.m_initialToggleState != null)
			{
				this.m_initialToggleState.Initialize("InitialState", context);
				context.ExprHostBuilder.TextBoxToggleImageInitialState(this.m_initialToggleState);
			}
			if (this.m_hideDuplicates != null)
			{
				context.ValidateHideDuplicateScope(this.m_hideDuplicates, this);
			}
			context.RegisterSender(this);
			if (this.m_userSort != null)
			{
				context.RegisterSortFilterTextbox(this);
				this.m_textboxScope = context.GetCurrentScope();
				if ((LocationFlags)0 < (context.Location & LocationFlags.InMatrixCellTopLevelItem))
				{
					this.m_isMatrixCellScope = true;
				}
				if ((LocationFlags)0 < (context.Location & LocationFlags.InDetail))
				{
					this.m_isDetailScope = true;
					context.SetDataSetDetailUserSortFilter();
				}
				string sortExpressionScopeString = this.m_userSort.SortExpressionScopeString;
				if (sortExpressionScopeString == null)
				{
					context.TextboxWithDetailSortExpressionAdd(this);
				}
				else if (context.IsScope(sortExpressionScopeString))
				{
					if (context.IsCurrentScope(sortExpressionScopeString) && !this.m_isMatrixCellScope)
					{
						this.m_userSort.SortExpressionScope = context.GetSortFilterScope(sortExpressionScopeString);
						this.InitializeSortExpression(context, false);
					}
					else if (context.IsAncestorScope(sortExpressionScopeString, (LocationFlags)0 < (context.Location & LocationFlags.InMatrixGroupHeader), this.m_isMatrixCellScope))
					{
						context.ErrorContext.Register(ProcessingErrorCode.rsInvalidExpressionScope, Severity.Error, context.ObjectType, context.ObjectName, "SortExpressionScope", new string[] { sortExpressionScopeString });
					}
					else
					{
						context.RegisterUserSortInnerScope(this);
					}
				}
				else
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsNonExistingScope, Severity.Error, context.ObjectType, context.ObjectName, "SortExpressionScope", new string[] { sortExpressionScopeString });
				}
				string sortTargetString = this.m_userSort.SortTargetString;
				if (sortTargetString != null)
				{
					if (context.IsScope(sortTargetString))
					{
						if (!context.IsCurrentScope(sortTargetString) && !context.IsAncestorScope(sortTargetString, (LocationFlags)0 < (context.Location & LocationFlags.InMatrixGroupHeader), false) && !context.IsPeerScope(sortTargetString))
						{
							context.ErrorContext.Register(ProcessingErrorCode.rsInvalidTargetScope, Severity.Error, context.ObjectType, context.ObjectName, "SortTarget", new string[] { sortTargetString });
						}
					}
					else
					{
						context.ErrorContext.Register(ProcessingErrorCode.rsNonExistingScope, Severity.Error, context.ObjectType, context.ObjectName, "SortTarget", new string[] { sortTargetString });
					}
				}
				else if (context.IsReportTopLevelScope())
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsInvalidOmittedTargetScope, Severity.Error, context.ObjectType, context.ObjectName, "SortTarget", Array.Empty<string>());
				}
			}
			base.ExprHostID = context.ExprHostBuilder.TextBoxEnd();
			return true;
		}

		// Token: 0x0600607E RID: 24702 RVA: 0x001848DC File Offset: 0x00182ADC
		internal void InitializeSortExpression(InitializationContext context, bool needsExplicitAggregateScope)
		{
			if (this.m_userSort != null && this.m_userSort.SortExpression != null)
			{
				bool flag = true;
				if (needsExplicitAggregateScope && this.m_userSort.SortExpression.Aggregates != null)
				{
					int count = this.m_userSort.SortExpression.Aggregates.Count;
					for (int i = 0; i < count; i++)
					{
						string text;
						if (!this.m_userSort.SortExpression.Aggregates[i].GetScope(out text))
						{
							flag = false;
							context.ErrorContext.Register(ProcessingErrorCode.rsInvalidOmittedExpressionScope, Severity.Error, ObjectType.Textbox, this.m_name, "SortExpression", new string[] { "SortExpressionScope" });
						}
					}
				}
				if (flag)
				{
					this.m_userSort.SortExpression.Initialize("SortExpression", context);
				}
			}
		}

		// Token: 0x0600607F RID: 24703 RVA: 0x001849A8 File Offset: 0x00182BA8
		internal void AddToScopeSortFilterList()
		{
			IntList peerSortFilters = this.GetPeerSortFilters(true);
			Global.Tracer.Assert(peerSortFilters != null);
			peerSortFilters.Add(this.m_ID);
		}

		// Token: 0x06006080 RID: 24704 RVA: 0x001849E0 File Offset: 0x00182BE0
		internal IntList GetPeerSortFilters(bool create)
		{
			if (this.m_userSort == null)
			{
				return null;
			}
			IntList intList = null;
			InScopeSortFilterHashtable inScopeSortFilterHashtable;
			if (this.m_containingScopes == null || this.m_containingScopes.Count == 0 || this.m_isSubReportTopLevelScope)
			{
				inScopeSortFilterHashtable = this.GetSortFiltersInScope(create, false);
			}
			else
			{
				Grouping lastEntry = this.m_containingScopes.LastEntry;
				if (lastEntry == null)
				{
					inScopeSortFilterHashtable = this.GetSortFiltersInScope(create, true);
				}
				else if (this.m_userSort.SortExpressionScope == null)
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
				if (this.m_userSort.SortExpressionScope != null)
				{
					num = this.m_userSort.SortExpressionScope.ID;
				}
				else
				{
					num = this.m_userSort.SortTarget.ID;
				}
				intList = inScopeSortFilterHashtable[num];
				if (intList == null && create)
				{
					intList = new IntList();
					inScopeSortFilterHashtable.Add(num, intList);
				}
			}
			return intList;
		}

		// Token: 0x06006081 RID: 24705 RVA: 0x00184AE4 File Offset: 0x00182CE4
		private InScopeSortFilterHashtable GetSortFiltersInScope(bool create, bool inDetail)
		{
			Microsoft.ReportingServices.ReportProcessing.ReportItem reportItem = this.m_parent;
			if (inDetail)
			{
				while (reportItem != null)
				{
					if (reportItem is DataRegion)
					{
						break;
					}
					reportItem = reportItem.Parent;
				}
			}
			else
			{
				while (reportItem != null && !(reportItem is Report))
				{
					reportItem = reportItem.Parent;
				}
			}
			Global.Tracer.Assert(reportItem is DataRegion || reportItem is Report);
			InScopeSortFilterHashtable inScopeSortFilterHashtable;
			if (reportItem is Report)
			{
				Report report = (Report)reportItem;
				if (this.m_userSort.SortExpressionScope == null)
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
				Global.Tracer.Assert(this.m_userSort.SortExpressionScope == null);
				DataRegion dataRegion = (DataRegion)reportItem;
				if (dataRegion.DetailSortFiltersInScope == null && create)
				{
					dataRegion.DetailSortFiltersInScope = new InScopeSortFilterHashtable();
				}
				inScopeSortFilterHashtable = dataRegion.DetailSortFiltersInScope;
			}
			return inScopeSortFilterHashtable;
		}

		// Token: 0x06006082 RID: 24706 RVA: 0x00184BE0 File Offset: 0x00182DE0
		protected override void DataRendererInitialize(InitializationContext context)
		{
			base.DataRendererInitialize(context);
			if (!this.m_overrideReportDataElementStyle)
			{
				this.m_dataElementStyleAttribute = context.ReportDataElementStyleAttribute;
			}
		}

		// Token: 0x06006083 RID: 24707 RVA: 0x00184C00 File Offset: 0x00182E00
		internal void SetValueType(object textBoxValue)
		{
			if (textBoxValue == null || DBNull.Value == textBoxValue)
			{
				return;
			}
			if (this.m_valueTypeSet && TypeCode.Object == this.m_valueType)
			{
				return;
			}
			TypeCode typeCode = Type.GetTypeCode(textBoxValue.GetType());
			if (this.m_valueTypeSet)
			{
				if (this.m_valueType != typeCode)
				{
					this.m_valueType = TypeCode.Object;
				}
			}
			else
			{
				this.m_valueType = typeCode;
			}
			this.m_valueTypeSet = true;
		}

		// Token: 0x06006084 RID: 24708 RVA: 0x00184C60 File Offset: 0x00182E60
		internal override void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			if (base.ExprHostID >= 0)
			{
				Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null);
				this.m_exprHost = reportExprHost.TextBoxHostsRemotable[base.ExprHostID];
				base.ReportItemSetExprHost(this.m_exprHost, reportObjectModel);
				if (this.m_action != null)
				{
					if (this.m_exprHost.ActionInfoHost != null)
					{
						this.m_action.SetExprHost(this.m_exprHost.ActionInfoHost, reportObjectModel);
						return;
					}
					if (this.m_exprHost.ActionHost != null)
					{
						this.m_action.SetExprHost(this.m_exprHost.ActionHost, reportObjectModel);
					}
				}
			}
		}

		// Token: 0x06006085 RID: 24709 RVA: 0x00184D02 File Offset: 0x00182F02
		internal override void ProcessDrillthroughAction(ReportProcessing.ProcessingContext processingContext, NonComputedUniqueNames nonCompNames)
		{
			if (this.m_action == null || nonCompNames == null)
			{
				return;
			}
			this.m_action.ProcessDrillthroughAction(processingContext, nonCompNames.UniqueName);
		}

		// Token: 0x06006086 RID: 24710 RVA: 0x00184D24 File Offset: 0x00182F24
		internal bool IsSimpleTextBox()
		{
			return (this.m_styleClass == null || this.m_styleClass.ExpressionList == null || 0 >= this.m_styleClass.ExpressionList.Count) && this.m_initialToggleState == null && !this.m_isToggle && this.m_visibility == null && this.m_label == null && this.m_bookmark == null && this.m_action == null && (this.m_toolTip == null || ExpressionInfo.Types.Constant == this.m_toolTip.Type) && this.m_customProperties == null && this.m_hideDuplicates == null && this.m_userSort == null;
		}

		// Token: 0x06006087 RID: 24711 RVA: 0x00184DC4 File Offset: 0x00182FC4
		internal bool IsSimpleTextBox(IntermediateFormatVersion intermediateFormatVersion)
		{
			Global.Tracer.Assert(intermediateFormatVersion != null);
			return intermediateFormatVersion.IsRS2005_WithSimpleTextBoxOptimizations && this.IsSimpleTextBox();
		}

		// Token: 0x06006088 RID: 24712 RVA: 0x00184DE4 File Offset: 0x00182FE4
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItem, new MemberInfoList
			{
				new MemberInfo(MemberName.Value, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.CanGrow, Token.Boolean),
				new MemberInfo(MemberName.CanShrink, Token.Boolean),
				new MemberInfo(MemberName.HideDuplicates, Token.String),
				new MemberInfo(MemberName.Action, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.Action),
				new MemberInfo(MemberName.IsToggle, Token.Boolean),
				new MemberInfo(MemberName.InitialToggleState, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ValueType, Token.Enum),
				new MemberInfo(MemberName.Formula, Token.String),
				new MemberInfo(MemberName.ValueReferenced, Token.Boolean),
				new MemberInfo(MemberName.RecursiveSender, Token.Boolean),
				new MemberInfo(MemberName.DataElementStyleAttribute, Token.Boolean),
				new MemberInfo(MemberName.ContainingScopes, Token.Reference, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.GroupingList),
				new MemberInfo(MemberName.UserSort, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.EndUserSort),
				new MemberInfo(MemberName.IsMatrixCellScope, Token.Boolean),
				new MemberInfo(MemberName.IsSubReportTopLevelScope, Token.Boolean)
			});
		}

		// Token: 0x040030DE RID: 12510
		private ExpressionInfo m_value;

		// Token: 0x040030DF RID: 12511
		private string m_formula;

		// Token: 0x040030E0 RID: 12512
		private bool m_canGrow;

		// Token: 0x040030E1 RID: 12513
		private bool m_canShrink;

		// Token: 0x040030E2 RID: 12514
		private string m_hideDuplicates;

		// Token: 0x040030E3 RID: 12515
		private Microsoft.ReportingServices.ReportProcessing.Action m_action;

		// Token: 0x040030E4 RID: 12516
		private bool m_isToggle;

		// Token: 0x040030E5 RID: 12517
		private ExpressionInfo m_initialToggleState;

		// Token: 0x040030E6 RID: 12518
		private bool m_valueReferenced;

		// Token: 0x040030E7 RID: 12519
		private bool m_recursiveSender;

		// Token: 0x040030E8 RID: 12520
		private bool m_dataElementStyleAttribute = true;

		// Token: 0x040030E9 RID: 12521
		[Reference]
		private GroupingList m_containingScopes;

		// Token: 0x040030EA RID: 12522
		private EndUserSort m_userSort;

		// Token: 0x040030EB RID: 12523
		private bool m_isMatrixCellScope;

		// Token: 0x040030EC RID: 12524
		private TypeCode m_valueType = TypeCode.String;

		// Token: 0x040030ED RID: 12525
		private bool m_isSubReportTopLevelScope;

		// Token: 0x040030EE RID: 12526
		[NonSerialized]
		private bool m_overrideReportDataElementStyle;

		// Token: 0x040030EF RID: 12527
		[NonSerialized]
		private string m_textboxScope;

		// Token: 0x040030F0 RID: 12528
		[NonSerialized]
		private bool m_isDetailScope;

		// Token: 0x040030F1 RID: 12529
		[NonSerialized]
		private bool m_valueTypeSet;

		// Token: 0x040030F2 RID: 12530
		[NonSerialized]
		private VariantResult m_oldResult;

		// Token: 0x040030F3 RID: 12531
		[NonSerialized]
		private bool m_hasOldResult;

		// Token: 0x040030F4 RID: 12532
		[NonSerialized]
		private string m_formattedValue;

		// Token: 0x040030F5 RID: 12533
		[NonSerialized]
		private TextBoxExprHost m_exprHost;

		// Token: 0x040030F6 RID: 12534
		[NonSerialized]
		private bool m_sharedFormatSettings;

		// Token: 0x040030F7 RID: 12535
		[NonSerialized]
		private bool m_calendarValidated;

		// Token: 0x040030F8 RID: 12536
		[NonSerialized]
		private Calendar m_calendar;

		// Token: 0x040030F9 RID: 12537
		[NonSerialized]
		private uint m_languageInstanceId;

		// Token: 0x040030FA RID: 12538
		[NonSerialized]
		private int m_tableColumnPosition = -1;

		// Token: 0x040030FB RID: 12539
		[NonSerialized]
		private List<string> m_fieldsUsedInValueExpression;
	}
}
