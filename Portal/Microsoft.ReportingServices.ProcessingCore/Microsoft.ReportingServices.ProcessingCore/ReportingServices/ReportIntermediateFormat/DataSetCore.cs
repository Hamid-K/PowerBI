using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004B9 RID: 1209
	[Serializable]
	public sealed class DataSetCore : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IExpressionHostAssemblyHolder
	{
		// Token: 0x06003C74 RID: 15476 RVA: 0x0010486A File Offset: 0x00102A6A
		internal DataSetCore()
		{
		}

		// Token: 0x170019D8 RID: 6616
		// (get) Token: 0x06003C75 RID: 15477 RVA: 0x001048A1 File Offset: 0x00102AA1
		// (set) Token: 0x06003C76 RID: 15478 RVA: 0x001048A9 File Offset: 0x00102AA9
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

		// Token: 0x170019D9 RID: 6617
		// (get) Token: 0x06003C77 RID: 15479 RVA: 0x001048B2 File Offset: 0x00102AB2
		// (set) Token: 0x06003C78 RID: 15480 RVA: 0x001048BA File Offset: 0x00102ABA
		internal List<Field> Fields
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

		// Token: 0x170019DA RID: 6618
		// (get) Token: 0x06003C79 RID: 15481 RVA: 0x001048C4 File Offset: 0x00102AC4
		internal bool HasAggregateIndicatorFields
		{
			get
			{
				if (this.m_cachedUsesAggregateIndicatorFields == null)
				{
					this.m_cachedUsesAggregateIndicatorFields = new bool?(false);
					if (this.m_fields != null)
					{
						this.m_cachedUsesAggregateIndicatorFields = new bool?(this.m_fields.Any((Field field) => field.HasAggregateIndicatorField));
					}
				}
				return this.m_cachedUsesAggregateIndicatorFields.Value;
			}
		}

		// Token: 0x170019DB RID: 6619
		// (get) Token: 0x06003C7A RID: 15482 RVA: 0x00104932 File Offset: 0x00102B32
		// (set) Token: 0x06003C7B RID: 15483 RVA: 0x0010493A File Offset: 0x00102B3A
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

		// Token: 0x170019DC RID: 6620
		// (get) Token: 0x06003C7C RID: 15484 RVA: 0x00104943 File Offset: 0x00102B43
		// (set) Token: 0x06003C7D RID: 15485 RVA: 0x0010494B File Offset: 0x00102B4B
		internal SharedDataSetQuery SharedDataSetQuery
		{
			get
			{
				return this.m_sharedDataSetQuery;
			}
			set
			{
				this.m_sharedDataSetQuery = value;
			}
		}

		// Token: 0x170019DD RID: 6621
		// (get) Token: 0x06003C7E RID: 15486 RVA: 0x00104954 File Offset: 0x00102B54
		// (set) Token: 0x06003C7F RID: 15487 RVA: 0x0010495C File Offset: 0x00102B5C
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

		// Token: 0x170019DE RID: 6622
		// (get) Token: 0x06003C80 RID: 15488 RVA: 0x00104965 File Offset: 0x00102B65
		// (set) Token: 0x06003C81 RID: 15489 RVA: 0x0010496D File Offset: 0x00102B6D
		internal string CollationCulture
		{
			get
			{
				return this.m_collationCulture;
			}
			set
			{
				this.m_collationCulture = value;
			}
		}

		// Token: 0x170019DF RID: 6623
		// (get) Token: 0x06003C82 RID: 15490 RVA: 0x00104976 File Offset: 0x00102B76
		// (set) Token: 0x06003C83 RID: 15491 RVA: 0x0010497E File Offset: 0x00102B7E
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

		// Token: 0x170019E0 RID: 6624
		// (get) Token: 0x06003C84 RID: 15492 RVA: 0x00104987 File Offset: 0x00102B87
		// (set) Token: 0x06003C85 RID: 15493 RVA: 0x0010498F File Offset: 0x00102B8F
		internal DataSet.TriState CaseSensitivity
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

		// Token: 0x170019E1 RID: 6625
		// (get) Token: 0x06003C86 RID: 15494 RVA: 0x00104998 File Offset: 0x00102B98
		// (set) Token: 0x06003C87 RID: 15495 RVA: 0x001049A0 File Offset: 0x00102BA0
		internal DataSet.TriState AccentSensitivity
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

		// Token: 0x170019E2 RID: 6626
		// (get) Token: 0x06003C88 RID: 15496 RVA: 0x001049A9 File Offset: 0x00102BA9
		// (set) Token: 0x06003C89 RID: 15497 RVA: 0x001049B1 File Offset: 0x00102BB1
		internal DataSet.TriState KanatypeSensitivity
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

		// Token: 0x170019E3 RID: 6627
		// (get) Token: 0x06003C8A RID: 15498 RVA: 0x001049BA File Offset: 0x00102BBA
		// (set) Token: 0x06003C8B RID: 15499 RVA: 0x001049C2 File Offset: 0x00102BC2
		internal DataSet.TriState WidthSensitivity
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

		// Token: 0x170019E4 RID: 6628
		// (get) Token: 0x06003C8C RID: 15500 RVA: 0x001049CB File Offset: 0x00102BCB
		// (set) Token: 0x06003C8D RID: 15501 RVA: 0x001049D3 File Offset: 0x00102BD3
		internal bool NullsAsBlanks
		{
			get
			{
				return this.m_nullsAsBlanks;
			}
			set
			{
				this.m_nullsAsBlanks = value;
			}
		}

		// Token: 0x170019E5 RID: 6629
		// (get) Token: 0x06003C8E RID: 15502 RVA: 0x001049DC File Offset: 0x00102BDC
		// (set) Token: 0x06003C8F RID: 15503 RVA: 0x001049E4 File Offset: 0x00102BE4
		internal bool UseOrdinalStringKeyGeneration
		{
			get
			{
				return this.m_useOrdinalStringKeyGeneration;
			}
			set
			{
				this.m_useOrdinalStringKeyGeneration = value;
			}
		}

		// Token: 0x170019E6 RID: 6630
		// (get) Token: 0x06003C90 RID: 15504 RVA: 0x001049ED File Offset: 0x00102BED
		// (set) Token: 0x06003C91 RID: 15505 RVA: 0x001049F5 File Offset: 0x00102BF5
		internal List<Filter> Filters
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

		// Token: 0x170019E7 RID: 6631
		// (get) Token: 0x06003C92 RID: 15506 RVA: 0x001049FE File Offset: 0x00102BFE
		// (set) Token: 0x06003C93 RID: 15507 RVA: 0x00104A06 File Offset: 0x00102C06
		internal DataSet.TriState InterpretSubtotalsAsDetails
		{
			get
			{
				return this.m_interpretSubtotalsAsDetails;
			}
			set
			{
				this.m_interpretSubtotalsAsDetails = value;
			}
		}

		// Token: 0x170019E8 RID: 6632
		// (get) Token: 0x06003C94 RID: 15508 RVA: 0x00104A10 File Offset: 0x00102C10
		// (set) Token: 0x06003C95 RID: 15509 RVA: 0x00104A64 File Offset: 0x00102C64
		internal int NonCalculatedFieldCount
		{
			get
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
				return this.m_nonCalculatedFieldCount;
			}
			set
			{
				this.m_nonCalculatedFieldCount = value;
			}
		}

		// Token: 0x170019E9 RID: 6633
		// (get) Token: 0x06003C96 RID: 15510 RVA: 0x00104A6D File Offset: 0x00102C6D
		internal Guid CatalogID
		{
			get
			{
				return this.m_catalogID;
			}
		}

		// Token: 0x170019EA RID: 6634
		// (get) Token: 0x06003C97 RID: 15511 RVA: 0x00104A75 File Offset: 0x00102C75
		// (set) Token: 0x06003C98 RID: 15512 RVA: 0x00104A7D File Offset: 0x00102C7D
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

		// Token: 0x170019EB RID: 6635
		// (get) Token: 0x06003C99 RID: 15513 RVA: 0x00104A86 File Offset: 0x00102C86
		// (set) Token: 0x06003C9A RID: 15514 RVA: 0x00104A8E File Offset: 0x00102C8E
		internal DataSetExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
			set
			{
				this.m_exprHost = value;
			}
		}

		// Token: 0x170019EC RID: 6636
		// (get) Token: 0x06003C9B RID: 15515 RVA: 0x00104A97 File Offset: 0x00102C97
		// (set) Token: 0x06003C9C RID: 15516 RVA: 0x00104A9F File Offset: 0x00102C9F
		internal FieldsContext FieldsContext
		{
			get
			{
				return this.m_fieldsContext;
			}
			set
			{
				this.m_fieldsContext = value;
			}
		}

		// Token: 0x170019ED RID: 6637
		// (get) Token: 0x06003C9D RID: 15517 RVA: 0x00104AA8 File Offset: 0x00102CA8
		Microsoft.ReportingServices.ReportProcessing.ObjectType IExpressionHostAssemblyHolder.ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.SharedDataSet;
			}
		}

		// Token: 0x170019EE RID: 6638
		// (get) Token: 0x06003C9E RID: 15518 RVA: 0x00104AAC File Offset: 0x00102CAC
		string IExpressionHostAssemblyHolder.ExprHostAssemblyName
		{
			get
			{
				if (this.m_exprHostAssemblyId == Guid.Empty)
				{
					this.m_exprHostAssemblyId = Guid.NewGuid();
					this.m_exprHostID = 0;
				}
				return "expression_host_RSD_" + this.m_exprHostAssemblyId.ToString().Replace("-", "");
			}
		}

		// Token: 0x170019EF RID: 6639
		// (get) Token: 0x06003C9F RID: 15519 RVA: 0x00104B07 File Offset: 0x00102D07
		// (set) Token: 0x06003CA0 RID: 15520 RVA: 0x00104B0F File Offset: 0x00102D0F
		byte[] IExpressionHostAssemblyHolder.CompiledCode
		{
			get
			{
				return this.m_compiledCode;
			}
			set
			{
				this.m_compiledCode = value;
			}
		}

		// Token: 0x170019F0 RID: 6640
		// (get) Token: 0x06003CA1 RID: 15521 RVA: 0x00104B18 File Offset: 0x00102D18
		// (set) Token: 0x06003CA2 RID: 15522 RVA: 0x00104B20 File Offset: 0x00102D20
		bool IExpressionHostAssemblyHolder.CompiledCodeGeneratedWithRefusedPermissions
		{
			get
			{
				return this.m_compiledCodeGeneratedWithRefusedPermissions;
			}
			set
			{
				this.m_compiledCodeGeneratedWithRefusedPermissions = value;
			}
		}

		// Token: 0x170019F1 RID: 6641
		// (get) Token: 0x06003CA3 RID: 15523 RVA: 0x00104B29 File Offset: 0x00102D29
		// (set) Token: 0x06003CA4 RID: 15524 RVA: 0x00104B2C File Offset: 0x00102D2C
		List<string> IExpressionHostAssemblyHolder.CodeModules
		{
			get
			{
				return null;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170019F2 RID: 6642
		// (get) Token: 0x06003CA5 RID: 15525 RVA: 0x00104B33 File Offset: 0x00102D33
		// (set) Token: 0x06003CA6 RID: 15526 RVA: 0x00104B36 File Offset: 0x00102D36
		List<CodeClass> IExpressionHostAssemblyHolder.CodeClasses
		{
			get
			{
				return null;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06003CA7 RID: 15527 RVA: 0x00104B3D File Offset: 0x00102D3D
		internal CultureInfo CreateCultureInfoFromLcid()
		{
			return new CultureInfo((int)this.LCID, false);
		}

		// Token: 0x06003CA8 RID: 15528 RVA: 0x00104B4B File Offset: 0x00102D4B
		internal void SetCatalogID(Guid id)
		{
			if (this.m_catalogID == Guid.Empty)
			{
				this.m_catalogID = id;
			}
		}

		// Token: 0x06003CA9 RID: 15529 RVA: 0x00104B68 File Offset: 0x00102D68
		internal void Initialize(InitializationContext context)
		{
			if (this.m_query != null)
			{
				this.m_query.Initialize(context);
			}
			else if (this.m_sharedDataSetQuery != null)
			{
				this.m_sharedDataSetQuery.Initialize(context);
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
		}

		// Token: 0x06003CAA RID: 15530 RVA: 0x00104C04 File Offset: 0x00102E04
		internal CompareOptions GetCLRCompareOptions()
		{
			CompareOptions compareOptions = CompareOptions.None;
			if (DataSet.TriState.True != this.m_caseSensitivity)
			{
				compareOptions |= CompareOptions.IgnoreCase;
			}
			if (DataSet.TriState.True != this.m_accentSensitivity)
			{
				compareOptions |= CompareOptions.IgnoreNonSpace;
			}
			if (DataSet.TriState.True != this.m_kanatypeSensitivity)
			{
				compareOptions |= CompareOptions.IgnoreKanaType;
			}
			if (DataSet.TriState.True != this.m_widthSensitivity)
			{
				compareOptions |= CompareOptions.IgnoreWidth;
			}
			return compareOptions;
		}

		// Token: 0x06003CAB RID: 15531 RVA: 0x00104C49 File Offset: 0x00102E49
		internal bool NeedAutoDetectCollation()
		{
			return DataSetValidator.LOCALE_SYSTEM_DEFAULT == this.m_lcid || this.m_accentSensitivity == DataSet.TriState.Auto || this.m_caseSensitivity == DataSet.TriState.Auto || this.m_kanatypeSensitivity == DataSet.TriState.Auto || this.m_widthSensitivity == DataSet.TriState.Auto;
		}

		// Token: 0x06003CAC RID: 15532 RVA: 0x00104C7C File Offset: 0x00102E7C
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
					if (errorContext != null)
					{
						errorContext.Register(ProcessingErrorCode.rsInvalidCollationCultureName, Severity.Warning, Microsoft.ReportingServices.ReportProcessing.ObjectType.DataSet, this.m_name, dataSourceType, new string[] { cultureName });
					}
				}
			}
			if (DataSetValidator.LOCALE_SYSTEM_DEFAULT == this.m_lcid)
			{
				this.m_lcid = num;
			}
			this.m_accentSensitivity = this.MergeSensitivity(this.m_accentSensitivity, accentSensitive);
			this.m_caseSensitivity = this.MergeSensitivity(this.m_caseSensitivity, caseSensitive);
			this.m_kanatypeSensitivity = this.MergeSensitivity(this.m_kanatypeSensitivity, kanatypeSensitive);
			this.m_widthSensitivity = this.MergeSensitivity(this.m_widthSensitivity, widthSensitive);
		}

		// Token: 0x06003CAD RID: 15533 RVA: 0x00104D44 File Offset: 0x00102F44
		private DataSet.TriState MergeSensitivity(DataSet.TriState current, bool detectedValue)
		{
			if (current != DataSet.TriState.Auto)
			{
				return current;
			}
			if (detectedValue)
			{
				return DataSet.TriState.True;
			}
			return DataSet.TriState.False;
		}

		// Token: 0x06003CAE RID: 15534 RVA: 0x00104D51 File Offset: 0x00102F51
		internal bool HasCalculatedFields()
		{
			return this.m_fields != null && this.NonCalculatedFieldCount != this.m_fields.Count;
		}

		// Token: 0x06003CAF RID: 15535 RVA: 0x00104D74 File Offset: 0x00102F74
		internal void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			if (this.m_exprHostID >= 0)
			{
				Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null, "(reportExprHost != null && reportObjectModel != null)");
				this.m_exprHost = reportExprHost.DataSetHostsRemotable[this.m_exprHostID];
				this.m_exprHost.SetReportObjectModel(reportObjectModel);
				if (this.m_exprHost.QueryParametersHost != null)
				{
					if (this.m_query != null)
					{
						this.m_query.SetExprHost(this.m_exprHost.QueryParametersHost, reportObjectModel);
					}
					else
					{
						this.m_sharedDataSetQuery.SetExprHost(this.m_exprHost.QueryParametersHost, reportObjectModel);
					}
				}
				if (this.m_exprHost.UserSortExpressionsHost != null)
				{
					this.m_exprHost.UserSortExpressionsHost.SetReportObjectModel(reportObjectModel);
				}
			}
		}

		// Token: 0x06003CB0 RID: 15536 RVA: 0x00104E2C File Offset: 0x0010302C
		internal void SetFilterExprHost(ObjectModelImpl reportObjectModel)
		{
			if (this.m_filters != null && this.m_exprHost != null)
			{
				for (int i = 0; i < this.m_filters.Count; i++)
				{
					this.m_filters[i].SetExprHost(this.m_exprHost.FilterHostsRemotable, reportObjectModel);
				}
			}
		}

		// Token: 0x06003CB1 RID: 15537 RVA: 0x00104E7C File Offset: 0x0010307C
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataSetCore, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.Fields, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Field),
				new MemberInfo(MemberName.Query, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportQuery),
				new MemberInfo(MemberName.SharedDataSetQuery, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SharedDataSetQuery),
				new MemberInfo(MemberName.Collation, Token.String),
				new MemberInfo(MemberName.LCID, Token.UInt32),
				new MemberInfo(MemberName.CaseSensitivity, Token.Enum),
				new MemberInfo(MemberName.AccentSensitivity, Token.Enum),
				new MemberInfo(MemberName.KanatypeSensitivity, Token.Enum),
				new MemberInfo(MemberName.WidthSensitivity, Token.Enum),
				new MemberInfo(MemberName.Filters, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Filter),
				new MemberInfo(MemberName.InterpretSubtotalsAsDetails, Token.Enum),
				new MemberInfo(MemberName.CatalogID, Token.Guid),
				new MemberInfo(MemberName.NonCalculatedFieldCount, Token.Int32),
				new MemberInfo(MemberName.CompiledCode, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveTypedArray, Token.Byte),
				new MemberInfo(MemberName.CompiledCodeGeneratedWithRefusedPermissions, Token.Boolean),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.ExprHostAssemblyID, Token.Guid),
				new MemberInfo(MemberName.NullsAsBlanks, Token.Boolean),
				new MemberInfo(MemberName.CollationCulture, Token.String)
			});
		}

		// Token: 0x06003CB2 RID: 15538 RVA: 0x00105018 File Offset: 0x00103218
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(DataSetCore.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.NonCalculatedFieldCount)
				{
					if (memberName <= MemberName.CompiledCode)
					{
						switch (memberName)
						{
						case MemberName.Fields:
							writer.Write<Field>(this.m_fields);
							continue;
						case MemberName.CaseSensitivity:
							writer.WriteEnum((int)this.m_caseSensitivity);
							continue;
						case MemberName.Collation:
							writer.Write(this.m_collation);
							continue;
						case MemberName.AccentSensitivity:
							writer.WriteEnum((int)this.m_accentSensitivity);
							continue;
						case MemberName.KanatypeSensitivity:
							writer.WriteEnum((int)this.m_kanatypeSensitivity);
							continue;
						case MemberName.WidthSensitivity:
							writer.WriteEnum((int)this.m_widthSensitivity);
							continue;
						case MemberName.LCID:
							writer.Write(this.m_lcid);
							continue;
						default:
							if (memberName == MemberName.Name)
							{
								writer.Write(this.m_name);
								continue;
							}
							if (memberName == MemberName.CompiledCode)
							{
								writer.Write(this.m_compiledCode);
								continue;
							}
							break;
						}
					}
					else
					{
						if (memberName == MemberName.Query)
						{
							writer.Write(this.m_query);
							continue;
						}
						if (memberName == MemberName.Filters)
						{
							writer.Write<Filter>(this.m_filters);
							continue;
						}
						if (memberName == MemberName.NonCalculatedFieldCount)
						{
							writer.Write(this.m_nonCalculatedFieldCount);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.InterpretSubtotalsAsDetails)
				{
					if (memberName == MemberName.ExprHostID)
					{
						writer.Write(this.m_exprHostID);
						continue;
					}
					if (memberName == MemberName.CompiledCodeGeneratedWithRefusedPermissions)
					{
						writer.Write(this.m_compiledCodeGeneratedWithRefusedPermissions);
						continue;
					}
					if (memberName == MemberName.InterpretSubtotalsAsDetails)
					{
						writer.WriteEnum((int)this.m_interpretSubtotalsAsDetails);
						continue;
					}
				}
				else
				{
					switch (memberName)
					{
					case MemberName.SharedDataSetQuery:
						writer.Write(this.m_sharedDataSetQuery);
						continue;
					case MemberName.OriginalSharedDataSetReference:
					case MemberName.DataSetCore:
						break;
					case MemberName.CatalogID:
						writer.Write(this.m_catalogID);
						continue;
					case MemberName.ExprHostAssemblyID:
						writer.Write(this.m_exprHostAssemblyId);
						continue;
					default:
						if (memberName == MemberName.NullsAsBlanks)
						{
							writer.Write(this.m_nullsAsBlanks);
							continue;
						}
						if (memberName == MemberName.CollationCulture)
						{
							writer.Write(this.m_collationCulture);
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false, string.Empty);
			}
		}

		// Token: 0x06003CB3 RID: 15539 RVA: 0x00105288 File Offset: 0x00103488
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(DataSetCore.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.NonCalculatedFieldCount)
				{
					if (memberName <= MemberName.CompiledCode)
					{
						switch (memberName)
						{
						case MemberName.Fields:
							this.m_fields = reader.ReadGenericListOfRIFObjects<Field>();
							continue;
						case MemberName.CaseSensitivity:
							this.m_caseSensitivity = (DataSet.TriState)reader.ReadEnum();
							continue;
						case MemberName.Collation:
							this.m_collation = reader.ReadString();
							continue;
						case MemberName.AccentSensitivity:
							this.m_accentSensitivity = (DataSet.TriState)reader.ReadEnum();
							continue;
						case MemberName.KanatypeSensitivity:
							this.m_kanatypeSensitivity = (DataSet.TriState)reader.ReadEnum();
							continue;
						case MemberName.WidthSensitivity:
							this.m_widthSensitivity = (DataSet.TriState)reader.ReadEnum();
							continue;
						case MemberName.LCID:
							this.m_lcid = reader.ReadUInt32();
							continue;
						default:
							if (memberName == MemberName.Name)
							{
								this.m_name = reader.ReadString();
								continue;
							}
							if (memberName == MemberName.CompiledCode)
							{
								this.m_compiledCode = reader.ReadByteArray();
								continue;
							}
							break;
						}
					}
					else
					{
						if (memberName == MemberName.Query)
						{
							this.m_query = (ReportQuery)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.Filters)
						{
							this.m_filters = reader.ReadGenericListOfRIFObjects<Filter>();
							continue;
						}
						if (memberName == MemberName.NonCalculatedFieldCount)
						{
							this.m_nonCalculatedFieldCount = reader.ReadInt32();
							continue;
						}
					}
				}
				else if (memberName <= MemberName.InterpretSubtotalsAsDetails)
				{
					if (memberName == MemberName.ExprHostID)
					{
						this.m_exprHostID = reader.ReadInt32();
						continue;
					}
					if (memberName == MemberName.CompiledCodeGeneratedWithRefusedPermissions)
					{
						this.m_compiledCodeGeneratedWithRefusedPermissions = reader.ReadBoolean();
						continue;
					}
					if (memberName == MemberName.InterpretSubtotalsAsDetails)
					{
						this.m_interpretSubtotalsAsDetails = (DataSet.TriState)reader.ReadEnum();
						continue;
					}
				}
				else
				{
					switch (memberName)
					{
					case MemberName.SharedDataSetQuery:
						this.m_sharedDataSetQuery = (SharedDataSetQuery)reader.ReadRIFObject();
						continue;
					case MemberName.OriginalSharedDataSetReference:
					case MemberName.DataSetCore:
						break;
					case MemberName.CatalogID:
						this.m_catalogID = reader.ReadGuid();
						continue;
					case MemberName.ExprHostAssemblyID:
						this.m_exprHostAssemblyId = reader.ReadGuid();
						continue;
					default:
						if (memberName == MemberName.NullsAsBlanks)
						{
							this.m_nullsAsBlanks = reader.ReadBoolean();
							continue;
						}
						if (memberName == MemberName.CollationCulture)
						{
							this.m_collationCulture = reader.ReadString();
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false, string.Empty);
			}
		}

		// Token: 0x06003CB4 RID: 15540 RVA: 0x00105502 File Offset: 0x00103702
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false, string.Empty);
		}

		// Token: 0x06003CB5 RID: 15541 RVA: 0x00105514 File Offset: 0x00103714
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataSetCore;
		}

		// Token: 0x04001C69 RID: 7273
		private string m_name;

		// Token: 0x04001C6A RID: 7274
		private List<Field> m_fields;

		// Token: 0x04001C6B RID: 7275
		private ReportQuery m_query;

		// Token: 0x04001C6C RID: 7276
		private SharedDataSetQuery m_sharedDataSetQuery;

		// Token: 0x04001C6D RID: 7277
		private string m_collation;

		// Token: 0x04001C6E RID: 7278
		private string m_collationCulture;

		// Token: 0x04001C6F RID: 7279
		private uint m_lcid = DataSetValidator.LOCALE_SYSTEM_DEFAULT;

		// Token: 0x04001C70 RID: 7280
		private DataSet.TriState m_caseSensitivity;

		// Token: 0x04001C71 RID: 7281
		private DataSet.TriState m_accentSensitivity;

		// Token: 0x04001C72 RID: 7282
		private DataSet.TriState m_kanatypeSensitivity;

		// Token: 0x04001C73 RID: 7283
		private DataSet.TriState m_widthSensitivity;

		// Token: 0x04001C74 RID: 7284
		private bool m_nullsAsBlanks;

		// Token: 0x04001C75 RID: 7285
		[NonSerialized]
		private bool m_useOrdinalStringKeyGeneration;

		// Token: 0x04001C76 RID: 7286
		private List<Filter> m_filters;

		// Token: 0x04001C77 RID: 7287
		private DataSet.TriState m_interpretSubtotalsAsDetails;

		// Token: 0x04001C78 RID: 7288
		private Guid m_catalogID = Guid.Empty;

		// Token: 0x04001C79 RID: 7289
		private int m_nonCalculatedFieldCount = -1;

		// Token: 0x04001C7A RID: 7290
		private byte[] m_compiledCode;

		// Token: 0x04001C7B RID: 7291
		private bool m_compiledCodeGeneratedWithRefusedPermissions;

		// Token: 0x04001C7C RID: 7292
		private int m_exprHostID = -1;

		// Token: 0x04001C7D RID: 7293
		private Guid m_exprHostAssemblyId = Guid.Empty;

		// Token: 0x04001C7E RID: 7294
		[NonSerialized]
		private bool? m_cachedUsesAggregateIndicatorFields;

		// Token: 0x04001C7F RID: 7295
		[NonSerialized]
		private DataSetExprHost m_exprHost;

		// Token: 0x04001C80 RID: 7296
		[NonSerialized]
		private FieldsContext m_fieldsContext;

		// Token: 0x04001C81 RID: 7297
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = DataSetCore.GetDeclaration();
	}
}
