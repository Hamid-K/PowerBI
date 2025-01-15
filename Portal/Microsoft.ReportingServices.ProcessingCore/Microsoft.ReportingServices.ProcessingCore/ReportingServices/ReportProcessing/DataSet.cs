using System;
using System.Collections;
using System.Globalization;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006D7 RID: 1751
	[Serializable]
	internal sealed class DataSet : IDOwner, IAggregateHolder, ISortFilterScope
	{
		// Token: 0x06005EB8 RID: 24248 RVA: 0x00180BBC File Offset: 0x0017EDBC
		internal DataSet(int id)
			: base(id)
		{
			this.m_fields = new DataFieldList();
			this.m_dataRegions = new DataRegionList();
			this.m_aggregates = new DataAggregateInfoList();
			this.m_postSortAggregates = new DataAggregateInfoList();
		}

		// Token: 0x06005EB9 RID: 24249 RVA: 0x00180C1C File Offset: 0x0017EE1C
		internal DataSet()
		{
		}

		// Token: 0x17002144 RID: 8516
		// (get) Token: 0x06005EBA RID: 24250 RVA: 0x00180C44 File Offset: 0x0017EE44
		internal ObjectType ObjectType
		{
			get
			{
				return ObjectType.DataSet;
			}
		}

		// Token: 0x17002145 RID: 8517
		// (get) Token: 0x06005EBB RID: 24251 RVA: 0x00180C48 File Offset: 0x0017EE48
		// (set) Token: 0x06005EBC RID: 24252 RVA: 0x00180C50 File Offset: 0x0017EE50
		internal string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x17002146 RID: 8518
		// (get) Token: 0x06005EBD RID: 24253 RVA: 0x00180C59 File Offset: 0x0017EE59
		// (set) Token: 0x06005EBE RID: 24254 RVA: 0x00180C61 File Offset: 0x0017EE61
		internal DataFieldList Fields
		{
			get
			{
				return this.m_fields;
			}
			set
			{
				this.m_fields = value;
			}
		}

		// Token: 0x17002147 RID: 8519
		// (get) Token: 0x06005EBF RID: 24255 RVA: 0x00180C6A File Offset: 0x0017EE6A
		// (set) Token: 0x06005EC0 RID: 24256 RVA: 0x00180C72 File Offset: 0x0017EE72
		internal ReportQuery Query
		{
			get
			{
				return this.m_query;
			}
			set
			{
				this.m_query = value;
			}
		}

		// Token: 0x17002148 RID: 8520
		// (get) Token: 0x06005EC1 RID: 24257 RVA: 0x00180C7B File Offset: 0x0017EE7B
		// (set) Token: 0x06005EC2 RID: 24258 RVA: 0x00180C83 File Offset: 0x0017EE83
		internal DataSet.Sensitivity CaseSensitivity
		{
			get
			{
				return this.m_caseSensitivity;
			}
			set
			{
				this.m_caseSensitivity = value;
			}
		}

		// Token: 0x17002149 RID: 8521
		// (get) Token: 0x06005EC3 RID: 24259 RVA: 0x00180C8C File Offset: 0x0017EE8C
		// (set) Token: 0x06005EC4 RID: 24260 RVA: 0x00180C94 File Offset: 0x0017EE94
		internal string Collation
		{
			get
			{
				return this.m_collation;
			}
			set
			{
				this.m_collation = value;
			}
		}

		// Token: 0x1700214A RID: 8522
		// (get) Token: 0x06005EC5 RID: 24261 RVA: 0x00180C9D File Offset: 0x0017EE9D
		// (set) Token: 0x06005EC6 RID: 24262 RVA: 0x00180CA5 File Offset: 0x0017EEA5
		internal DataSet.Sensitivity AccentSensitivity
		{
			get
			{
				return this.m_accentSensitivity;
			}
			set
			{
				this.m_accentSensitivity = value;
			}
		}

		// Token: 0x1700214B RID: 8523
		// (get) Token: 0x06005EC7 RID: 24263 RVA: 0x00180CAE File Offset: 0x0017EEAE
		// (set) Token: 0x06005EC8 RID: 24264 RVA: 0x00180CB6 File Offset: 0x0017EEB6
		internal DataSet.Sensitivity KanatypeSensitivity
		{
			get
			{
				return this.m_kanatypeSensitivity;
			}
			set
			{
				this.m_kanatypeSensitivity = value;
			}
		}

		// Token: 0x1700214C RID: 8524
		// (get) Token: 0x06005EC9 RID: 24265 RVA: 0x00180CBF File Offset: 0x0017EEBF
		// (set) Token: 0x06005ECA RID: 24266 RVA: 0x00180CC7 File Offset: 0x0017EEC7
		internal DataSet.Sensitivity WidthSensitivity
		{
			get
			{
				return this.m_widthSensitivity;
			}
			set
			{
				this.m_widthSensitivity = value;
			}
		}

		// Token: 0x1700214D RID: 8525
		// (get) Token: 0x06005ECB RID: 24267 RVA: 0x00180CD0 File Offset: 0x0017EED0
		// (set) Token: 0x06005ECC RID: 24268 RVA: 0x00180CD8 File Offset: 0x0017EED8
		internal DataRegionList DataRegions
		{
			get
			{
				return this.m_dataRegions;
			}
			set
			{
				this.m_dataRegions = value;
			}
		}

		// Token: 0x1700214E RID: 8526
		// (get) Token: 0x06005ECD RID: 24269 RVA: 0x00180CE1 File Offset: 0x0017EEE1
		// (set) Token: 0x06005ECE RID: 24270 RVA: 0x00180CE9 File Offset: 0x0017EEE9
		internal DataAggregateInfoList Aggregates
		{
			get
			{
				return this.m_aggregates;
			}
			set
			{
				this.m_aggregates = value;
			}
		}

		// Token: 0x1700214F RID: 8527
		// (get) Token: 0x06005ECF RID: 24271 RVA: 0x00180CF2 File Offset: 0x0017EEF2
		// (set) Token: 0x06005ED0 RID: 24272 RVA: 0x00180CFA File Offset: 0x0017EEFA
		internal FilterList Filters
		{
			get
			{
				return this.m_filters;
			}
			set
			{
				this.m_filters = value;
			}
		}

		// Token: 0x17002150 RID: 8528
		// (get) Token: 0x06005ED1 RID: 24273 RVA: 0x00180D03 File Offset: 0x0017EF03
		// (set) Token: 0x06005ED2 RID: 24274 RVA: 0x00180D0B File Offset: 0x0017EF0B
		internal bool UsedOnlyInParameters
		{
			get
			{
				return this.m_usedOnlyInParameters;
			}
			set
			{
				this.m_usedOnlyInParameters = value;
			}
		}

		// Token: 0x17002151 RID: 8529
		// (get) Token: 0x06005ED3 RID: 24275 RVA: 0x00180D14 File Offset: 0x0017EF14
		// (set) Token: 0x06005ED4 RID: 24276 RVA: 0x00180D1C File Offset: 0x0017EF1C
		internal int NonCalculatedFieldCount
		{
			get
			{
				return this.m_nonCalculatedFieldCount;
			}
			set
			{
				this.m_nonCalculatedFieldCount = value;
			}
		}

		// Token: 0x17002152 RID: 8530
		// (get) Token: 0x06005ED5 RID: 24277 RVA: 0x00180D25 File Offset: 0x0017EF25
		// (set) Token: 0x06005ED6 RID: 24278 RVA: 0x00180D2D File Offset: 0x0017EF2D
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

		// Token: 0x17002153 RID: 8531
		// (get) Token: 0x06005ED7 RID: 24279 RVA: 0x00180D36 File Offset: 0x0017EF36
		// (set) Token: 0x06005ED8 RID: 24280 RVA: 0x00180D3E File Offset: 0x0017EF3E
		internal DataAggregateInfoList PostSortAggregates
		{
			get
			{
				return this.m_postSortAggregates;
			}
			set
			{
				this.m_postSortAggregates = value;
			}
		}

		// Token: 0x17002154 RID: 8532
		// (get) Token: 0x06005ED9 RID: 24281 RVA: 0x00180D47 File Offset: 0x0017EF47
		// (set) Token: 0x06005EDA RID: 24282 RVA: 0x00180D4F File Offset: 0x0017EF4F
		internal int RecordSetSize
		{
			get
			{
				return this.m_recordSetSize;
			}
			set
			{
				this.m_recordSetSize = value;
			}
		}

		// Token: 0x17002155 RID: 8533
		// (get) Token: 0x06005EDB RID: 24283 RVA: 0x00180D58 File Offset: 0x0017EF58
		// (set) Token: 0x06005EDC RID: 24284 RVA: 0x00180D60 File Offset: 0x0017EF60
		internal uint LCID
		{
			get
			{
				return this.m_lcid;
			}
			set
			{
				this.m_lcid = value;
			}
		}

		// Token: 0x17002156 RID: 8534
		// (get) Token: 0x06005EDD RID: 24285 RVA: 0x00180D69 File Offset: 0x0017EF69
		// (set) Token: 0x06005EDE RID: 24286 RVA: 0x00180D71 File Offset: 0x0017EF71
		internal bool HasDetailUserSortFilter
		{
			get
			{
				return this.m_hasDetailUserSortFilter;
			}
			set
			{
				this.m_hasDetailUserSortFilter = value;
			}
		}

		// Token: 0x17002157 RID: 8535
		// (get) Token: 0x06005EDF RID: 24287 RVA: 0x00180D7A File Offset: 0x0017EF7A
		// (set) Token: 0x06005EE0 RID: 24288 RVA: 0x00180D82 File Offset: 0x0017EF82
		internal ExpressionInfoList UserSortExpressions
		{
			get
			{
				return this.m_userSortExpressions;
			}
			set
			{
				this.m_userSortExpressions = value;
			}
		}

		// Token: 0x17002158 RID: 8536
		// (get) Token: 0x06005EE1 RID: 24289 RVA: 0x00180D8B File Offset: 0x0017EF8B
		internal DataSetExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17002159 RID: 8537
		// (get) Token: 0x06005EE2 RID: 24290 RVA: 0x00180D93 File Offset: 0x0017EF93
		// (set) Token: 0x06005EE3 RID: 24291 RVA: 0x00180D9B File Offset: 0x0017EF9B
		internal string AutoDetectedCollation
		{
			get
			{
				return this.m_autoDetectedCollation;
			}
			set
			{
				this.m_autoDetectedCollation = value;
			}
		}

		// Token: 0x1700215A RID: 8538
		// (get) Token: 0x06005EE4 RID: 24292 RVA: 0x00180DA4 File Offset: 0x0017EFA4
		// (set) Token: 0x06005EE5 RID: 24293 RVA: 0x00180DAC File Offset: 0x0017EFAC
		internal bool[] IsSortFilterTarget
		{
			get
			{
				return this.m_isSortFilterTarget;
			}
			set
			{
				this.m_isSortFilterTarget = value;
			}
		}

		// Token: 0x1700215B RID: 8539
		// (get) Token: 0x06005EE6 RID: 24294 RVA: 0x00180DB5 File Offset: 0x0017EFB5
		int ISortFilterScope.ID
		{
			get
			{
				return this.m_ID;
			}
		}

		// Token: 0x1700215C RID: 8540
		// (get) Token: 0x06005EE7 RID: 24295 RVA: 0x00180DBD File Offset: 0x0017EFBD
		string ISortFilterScope.ScopeName
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x1700215D RID: 8541
		// (get) Token: 0x06005EE8 RID: 24296 RVA: 0x00180DC5 File Offset: 0x0017EFC5
		// (set) Token: 0x06005EE9 RID: 24297 RVA: 0x00180DCD File Offset: 0x0017EFCD
		bool[] ISortFilterScope.IsSortFilterTarget
		{
			get
			{
				return this.m_isSortFilterTarget;
			}
			set
			{
				this.m_isSortFilterTarget = value;
			}
		}

		// Token: 0x1700215E RID: 8542
		// (get) Token: 0x06005EEA RID: 24298 RVA: 0x00180DD6 File Offset: 0x0017EFD6
		// (set) Token: 0x06005EEB RID: 24299 RVA: 0x00180DD9 File Offset: 0x0017EFD9
		bool[] ISortFilterScope.IsSortFilterExpressionScope
		{
			get
			{
				return null;
			}
			set
			{
				Global.Tracer.Assert(false, string.Empty);
			}
		}

		// Token: 0x1700215F RID: 8543
		// (get) Token: 0x06005EEC RID: 24300 RVA: 0x00180DEB File Offset: 0x0017EFEB
		// (set) Token: 0x06005EED RID: 24301 RVA: 0x00180DF3 File Offset: 0x0017EFF3
		ExpressionInfoList ISortFilterScope.UserSortExpressions
		{
			get
			{
				return this.m_userSortExpressions;
			}
			set
			{
				this.m_userSortExpressions = value;
			}
		}

		// Token: 0x17002160 RID: 8544
		// (get) Token: 0x06005EEE RID: 24302 RVA: 0x00180DFC File Offset: 0x0017EFFC
		IndexedExprHost ISortFilterScope.UserSortExpressionsHost
		{
			get
			{
				if (this.m_exprHost == null)
				{
					return null;
				}
				return this.m_exprHost.UserSortExpressionsHost;
			}
		}

		// Token: 0x17002161 RID: 8545
		// (get) Token: 0x06005EEF RID: 24303 RVA: 0x00180E13 File Offset: 0x0017F013
		// (set) Token: 0x06005EF0 RID: 24304 RVA: 0x00180E1B File Offset: 0x0017F01B
		internal bool DynamicFieldReferences
		{
			get
			{
				return this.m_dynamicFieldReferences;
			}
			set
			{
				this.m_dynamicFieldReferences = value;
			}
		}

		// Token: 0x17002162 RID: 8546
		// (get) Token: 0x06005EF1 RID: 24305 RVA: 0x00180E24 File Offset: 0x0017F024
		// (set) Token: 0x06005EF2 RID: 24306 RVA: 0x00180E2C File Offset: 0x0017F02C
		internal bool UsedInAggregates
		{
			get
			{
				return this.m_usedInAggregates;
			}
			set
			{
				this.m_usedInAggregates = value;
			}
		}

		// Token: 0x17002163 RID: 8547
		// (get) Token: 0x06005EF3 RID: 24307 RVA: 0x00180E35 File Offset: 0x0017F035
		internal bool InterpretSubtotalsAsDetailsIsAuto
		{
			get
			{
				return this.m_interpretSubtotalsAsDetails == null;
			}
		}

		// Token: 0x17002164 RID: 8548
		// (get) Token: 0x06005EF4 RID: 24308 RVA: 0x00180E45 File Offset: 0x0017F045
		// (set) Token: 0x06005EF5 RID: 24309 RVA: 0x00180E61 File Offset: 0x0017F061
		internal bool InterpretSubtotalsAsDetails
		{
			get
			{
				return this.m_interpretSubtotalsAsDetails != null && this.m_interpretSubtotalsAsDetails.Value;
			}
			set
			{
				this.m_interpretSubtotalsAsDetails = new bool?(value);
			}
		}

		// Token: 0x06005EF6 RID: 24310 RVA: 0x00180E6F File Offset: 0x0017F06F
		internal void Initialize(InitializationContext context)
		{
			context.ObjectType = this.ObjectType;
			context.ObjectName = this.m_name;
			context.RegisterDataSet(this);
			this.InternalInitialize(context);
			context.UnRegisterDataSet(this);
		}

		// Token: 0x06005EF7 RID: 24311 RVA: 0x00180EA4 File Offset: 0x0017F0A4
		private void InternalInitialize(InitializationContext context)
		{
			context.ExprHostBuilder.DataSetStart(this.m_name);
			context.Location |= LocationFlags.InDataSet;
			if (this.m_query != null)
			{
				this.m_query.Initialize(context);
			}
			if (this.m_fields != null)
			{
				int count = this.m_fields.Count;
				for (int i = 0; i < count; i++)
				{
					this.m_fields[i].Initialize(context);
				}
			}
			if (this.m_filters != null)
			{
				for (int j = 0; j < this.m_filters.Count; j++)
				{
					this.m_filters[j].Initialize(context);
				}
			}
			if (this.m_userSortExpressions != null)
			{
				context.ExprHostBuilder.UserSortExpressionsStart();
				for (int k = 0; k < this.m_userSortExpressions.Count; k++)
				{
					ExpressionInfo expressionInfo = this.m_userSortExpressions[k];
					context.ExprHostBuilder.UserSortExpression(expressionInfo);
				}
				context.ExprHostBuilder.UserSortExpressionsEnd();
			}
			this.m_exprHostID = context.ExprHostBuilder.DataSetEnd();
		}

		// Token: 0x06005EF8 RID: 24312 RVA: 0x00180FAD File Offset: 0x0017F1AD
		DataAggregateInfoList[] IAggregateHolder.GetAggregateLists()
		{
			return new DataAggregateInfoList[] { this.m_aggregates };
		}

		// Token: 0x06005EF9 RID: 24313 RVA: 0x00180FBE File Offset: 0x0017F1BE
		DataAggregateInfoList[] IAggregateHolder.GetPostSortAggregateLists()
		{
			return new DataAggregateInfoList[] { this.m_postSortAggregates };
		}

		// Token: 0x06005EFA RID: 24314 RVA: 0x00180FD0 File Offset: 0x0017F1D0
		void IAggregateHolder.ClearIfEmpty()
		{
			Global.Tracer.Assert(this.m_aggregates != null);
			if (this.m_aggregates.Count == 0)
			{
				this.m_aggregates = null;
			}
			Global.Tracer.Assert(this.m_postSortAggregates != null);
			if (this.m_postSortAggregates.Count == 0)
			{
				this.m_postSortAggregates = null;
			}
		}

		// Token: 0x06005EFB RID: 24315 RVA: 0x0018102C File Offset: 0x0017F22C
		internal void CheckNonCalculatedFieldCount()
		{
			if (this.m_nonCalculatedFieldCount < 0 && this.m_fields != null)
			{
				int num = 0;
				while (num < this.m_fields.Count && !this.m_fields[num].IsCalculatedField)
				{
					num++;
				}
				this.m_nonCalculatedFieldCount = num;
			}
		}

		// Token: 0x06005EFC RID: 24316 RVA: 0x0018107C File Offset: 0x0017F27C
		internal void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			if (this.ExprHostID >= 0)
			{
				Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null);
				this.m_exprHost = reportExprHost.DataSetHostsRemotable[this.ExprHostID];
				this.m_exprHost.SetReportObjectModel(reportObjectModel);
				if (this.m_exprHost.QueryParametersHost != null)
				{
					this.m_query.SetExprHost(this.m_exprHost.QueryParametersHost, reportObjectModel);
				}
				if (this.m_exprHost.UserSortExpressionsHost != null)
				{
					this.m_exprHost.UserSortExpressionsHost.SetReportObjectModel(reportObjectModel);
				}
			}
		}

		// Token: 0x06005EFD RID: 24317 RVA: 0x0018110B File Offset: 0x0017F30B
		internal bool NeedAutoDetectCollation()
		{
			return DataSetValidator.LOCALE_SYSTEM_DEFAULT == this.m_lcid || this.m_accentSensitivity == DataSet.Sensitivity.Auto || this.m_caseSensitivity == DataSet.Sensitivity.Auto || this.m_kanatypeSensitivity == DataSet.Sensitivity.Auto || this.m_widthSensitivity == DataSet.Sensitivity.Auto;
		}

		// Token: 0x06005EFE RID: 24318 RVA: 0x00181140 File Offset: 0x0017F340
		internal void MergeCollationSettings(ErrorContext errorContext, string dataSourceType, string cultureName, bool caseSensitive, bool accentSensitive, bool kanatypeSensitive, bool widthSensitive)
		{
			if (!this.NeedAutoDetectCollation())
			{
				return;
			}
			uint num = DataSetValidator.LOCALE_SYSTEM_DEFAULT;
			if (cultureName != null)
			{
				try
				{
					num = (uint)CultureInfo.GetCultureInfo(cultureName).LCID;
				}
				catch (Exception)
				{
					errorContext.Register(ProcessingErrorCode.rsInvalidCollationCultureName, Severity.Warning, ObjectType.DataSet, this.m_name, dataSourceType, new string[] { cultureName });
				}
			}
			if (DataSetValidator.LOCALE_SYSTEM_DEFAULT == this.m_lcid)
			{
				this.m_lcid = num;
			}
			this.MergeSensitivity(ref this.m_accentSensitivity, accentSensitive);
			this.MergeSensitivity(ref this.m_caseSensitivity, caseSensitive);
			this.MergeSensitivity(ref this.m_kanatypeSensitivity, kanatypeSensitive);
			this.MergeSensitivity(ref this.m_widthSensitivity, widthSensitive);
		}

		// Token: 0x06005EFF RID: 24319 RVA: 0x001811EC File Offset: 0x0017F3EC
		private void MergeSensitivity(ref DataSet.Sensitivity current, bool detectedValue)
		{
			if (current != DataSet.Sensitivity.Auto)
			{
				return;
			}
			if (detectedValue)
			{
				current = DataSet.Sensitivity.True;
				return;
			}
			current = DataSet.Sensitivity.False;
		}

		// Token: 0x06005F00 RID: 24320 RVA: 0x001811FD File Offset: 0x0017F3FD
		internal uint GetSQLSortCompareFlags()
		{
			return DataSetValidator.GetSQLSortCompareMask(DataSet.Sensitivity.True == this.m_caseSensitivity, DataSet.Sensitivity.True == this.m_accentSensitivity, DataSet.Sensitivity.True == this.m_kanatypeSensitivity, DataSet.Sensitivity.True == this.m_widthSensitivity);
		}

		// Token: 0x06005F01 RID: 24321 RVA: 0x00181228 File Offset: 0x0017F428
		internal CompareOptions GetCLRCompareOptions()
		{
			CompareOptions compareOptions = CompareOptions.None;
			if (DataSet.Sensitivity.True != this.m_caseSensitivity)
			{
				compareOptions |= CompareOptions.IgnoreCase;
			}
			if (DataSet.Sensitivity.True != this.m_accentSensitivity)
			{
				compareOptions |= CompareOptions.IgnoreNonSpace;
			}
			if (DataSet.Sensitivity.True != this.m_kanatypeSensitivity)
			{
				compareOptions |= CompareOptions.IgnoreKanaType;
			}
			if (DataSet.Sensitivity.True != this.m_widthSensitivity)
			{
				compareOptions |= CompareOptions.IgnoreWidth;
			}
			return compareOptions;
		}

		// Token: 0x06005F02 RID: 24322 RVA: 0x00181270 File Offset: 0x0017F470
		internal void MergeFieldProperties(ExpressionInfo expressionInfo)
		{
			if (this.m_dynamicFieldReferences)
			{
				return;
			}
			Global.Tracer.Assert(expressionInfo != null);
			if (expressionInfo.DynamicFieldReferences)
			{
				this.m_dynamicFieldReferences = true;
				this.m_referencedFieldProperties = null;
				return;
			}
			if (expressionInfo.ReferencedFieldProperties != null && expressionInfo.ReferencedFieldProperties.Count != 0)
			{
				if (this.m_referencedFieldProperties == null)
				{
					this.m_referencedFieldProperties = new Hashtable();
				}
				IDictionaryEnumerator enumerator = expressionInfo.ReferencedFieldProperties.GetEnumerator();
				while (enumerator.MoveNext())
				{
					string text = enumerator.Entry.Key as string;
					bool flag = this.m_referencedFieldProperties.ContainsKey(text);
					Hashtable hashtable = this.m_referencedFieldProperties[text] as Hashtable;
					if (!flag || hashtable != null)
					{
						Hashtable hashtable2 = enumerator.Entry.Value as Hashtable;
						if (!flag)
						{
							this.m_referencedFieldProperties.Add(text, hashtable2);
						}
						else if (hashtable2 == null)
						{
							this.m_referencedFieldProperties[text] = null;
						}
						else
						{
							Global.Tracer.Assert(hashtable != null && hashtable2 != null);
							foreach (object obj in hashtable2.Keys)
							{
								string text2 = obj as string;
								if (!hashtable.ContainsKey(text2))
								{
									hashtable.Add(text2, null);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06005F03 RID: 24323 RVA: 0x001813C0 File Offset: 0x0017F5C0
		internal void PopulateReferencedFieldProperties()
		{
			if (this.m_dynamicFieldReferences || this.m_fields == null || this.m_referencedFieldProperties == null)
			{
				return;
			}
			for (int i = 0; i < this.m_fields.Count; i++)
			{
				Field field = this.m_fields[i];
				if (this.m_referencedFieldProperties.ContainsKey(field.Name))
				{
					Hashtable hashtable = this.m_referencedFieldProperties[field.Name] as Hashtable;
					if (hashtable == null)
					{
						field.DynamicPropertyReferences = true;
					}
					else
					{
						IEnumerator enumerator = hashtable.Keys.GetEnumerator();
						FieldPropertyHashtable fieldPropertyHashtable = new FieldPropertyHashtable(hashtable.Count);
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							fieldPropertyHashtable.Add(obj as string);
						}
						field.ReferencedProperties = fieldPropertyHashtable;
					}
				}
			}
		}

		// Token: 0x06005F04 RID: 24324 RVA: 0x00181484 File Offset: 0x0017F684
		internal bool IsShareable()
		{
			if (this.m_query == null || this.m_query.CommandText == null)
			{
				return true;
			}
			if (ExpressionInfo.Types.Constant != this.m_query.CommandText.Type)
			{
				return false;
			}
			if (this.m_query.Parameters == null)
			{
				return true;
			}
			int count = this.m_query.Parameters.Count;
			for (int i = 0; i < count; i++)
			{
				ExpressionInfo value = this.m_query.Parameters[i].Value;
				if (value != null && ExpressionInfo.Types.Constant != value.Type)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005F05 RID: 24325 RVA: 0x00181510 File Offset: 0x0017F710
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.IDOwner, new MemberInfoList
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.Fields, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.DataFieldList),
				new MemberInfo(MemberName.Query, Token.String),
				new MemberInfo(MemberName.CaseSensitivity, Token.Enum),
				new MemberInfo(MemberName.Collation, Token.String),
				new MemberInfo(MemberName.AccentSensitivity, Token.Enum),
				new MemberInfo(MemberName.KanatypeSensitivity, Token.Enum),
				new MemberInfo(MemberName.WidthSensitivity, Token.Enum),
				new MemberInfo(MemberName.DataRegions, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.DataRegionList),
				new MemberInfo(MemberName.Aggregates, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.DataAggregateInfoList),
				new MemberInfo(MemberName.Filters, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.FilterList),
				new MemberInfo(MemberName.RecordSetSize, Token.Int32),
				new MemberInfo(MemberName.UsedOnlyInParameters, Token.Boolean),
				new MemberInfo(MemberName.NonCalculatedFieldCount, Token.Int32),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.PostSortAggregates, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.DataAggregateInfoList),
				new MemberInfo(MemberName.LCID, Token.UInt32),
				new MemberInfo(MemberName.HasDetailUserSortFilter, Token.Boolean),
				new MemberInfo(MemberName.UserSortExpressions, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ExpressionInfoList),
				new MemberInfo(MemberName.DynamicFieldReferences, Token.Boolean),
				new MemberInfo(MemberName.InterpretSubtotalsAsDetails, Token.Boolean)
			});
		}

		// Token: 0x0400304E RID: 12366
		internal const uint CompareFlag_Default = 0U;

		// Token: 0x0400304F RID: 12367
		internal const uint CompareFlag_IgnoreCase = 1U;

		// Token: 0x04003050 RID: 12368
		internal const uint CompareFlag_IgnoreNonSpace = 2U;

		// Token: 0x04003051 RID: 12369
		internal const uint CompareFlag_IgnoreKanatype = 65536U;

		// Token: 0x04003052 RID: 12370
		internal const uint CompareFlag_IgnoreWidth = 131072U;

		// Token: 0x04003053 RID: 12371
		private string m_name;

		// Token: 0x04003054 RID: 12372
		private DataFieldList m_fields;

		// Token: 0x04003055 RID: 12373
		private ReportQuery m_query;

		// Token: 0x04003056 RID: 12374
		private DataSet.Sensitivity m_caseSensitivity;

		// Token: 0x04003057 RID: 12375
		private string m_collation;

		// Token: 0x04003058 RID: 12376
		private DataSet.Sensitivity m_accentSensitivity;

		// Token: 0x04003059 RID: 12377
		private DataSet.Sensitivity m_kanatypeSensitivity;

		// Token: 0x0400305A RID: 12378
		private DataSet.Sensitivity m_widthSensitivity;

		// Token: 0x0400305B RID: 12379
		private DataRegionList m_dataRegions;

		// Token: 0x0400305C RID: 12380
		private DataAggregateInfoList m_aggregates;

		// Token: 0x0400305D RID: 12381
		private FilterList m_filters;

		// Token: 0x0400305E RID: 12382
		private bool m_usedOnlyInParameters;

		// Token: 0x0400305F RID: 12383
		private int m_nonCalculatedFieldCount = -1;

		// Token: 0x04003060 RID: 12384
		private int m_exprHostID = -1;

		// Token: 0x04003061 RID: 12385
		private DataAggregateInfoList m_postSortAggregates;

		// Token: 0x04003062 RID: 12386
		private bool m_hasDetailUserSortFilter;

		// Token: 0x04003063 RID: 12387
		private ExpressionInfoList m_userSortExpressions;

		// Token: 0x04003064 RID: 12388
		private bool m_dynamicFieldReferences;

		// Token: 0x04003065 RID: 12389
		private bool? m_interpretSubtotalsAsDetails;

		// Token: 0x04003066 RID: 12390
		private int m_recordSetSize = -1;

		// Token: 0x04003067 RID: 12391
		private uint m_lcid = DataSetValidator.LOCALE_SYSTEM_DEFAULT;

		// Token: 0x04003068 RID: 12392
		[NonSerialized]
		private DataSetExprHost m_exprHost;

		// Token: 0x04003069 RID: 12393
		[NonSerialized]
		private string m_autoDetectedCollation;

		// Token: 0x0400306A RID: 12394
		[NonSerialized]
		private bool[] m_isSortFilterTarget;

		// Token: 0x0400306B RID: 12395
		[NonSerialized]
		private Hashtable m_referencedFieldProperties;

		// Token: 0x0400306C RID: 12396
		[NonSerialized]
		private bool m_usedInAggregates;

		// Token: 0x02000CBF RID: 3263
		internal enum Sensitivity
		{
			// Token: 0x04004E78 RID: 20088
			Auto,
			// Token: 0x04004E79 RID: 20089
			True,
			// Token: 0x04004E7A RID: 20090
			False
		}
	}
}
