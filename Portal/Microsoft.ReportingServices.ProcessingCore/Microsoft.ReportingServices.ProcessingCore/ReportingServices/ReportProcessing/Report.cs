using System;
using System.Globalization;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006D2 RID: 1746
	[Serializable]
	public sealed class Report : Microsoft.ReportingServices.ReportProcessing.ReportItem, IAggregateHolder
	{
		// Token: 0x06005DDB RID: 24027 RVA: 0x0017EFC0 File Offset: 0x0017D1C0
		internal Report(int id, int idForReportItems)
			: base(id, null)
		{
			this.m_intermediateFormatVersion = new IntermediateFormatVersion();
			this.m_reportVersion = Guid.NewGuid();
			this.m_height = "11in";
			this.m_width = "8.5in";
			this.m_dataSources = new DataSourceList();
			this.m_reportItems = new ReportItemCollection(idForReportItems, true);
			this.m_pageAggregates = new DataAggregateInfoList();
			this.m_exprCompiledCode = new byte[0];
		}

		// Token: 0x06005DDC RID: 24028 RVA: 0x0017F0C4 File Offset: 0x0017D2C4
		internal Report(Microsoft.ReportingServices.ReportProcessing.ReportItem parent, IntermediateFormatVersion version, Guid reportVersion)
			: base(parent)
		{
			this.m_intermediateFormatVersion = version;
			this.m_reportVersion = reportVersion;
			this.m_startPage = 0;
			this.m_endPage = 0;
		}

		// Token: 0x170020DD RID: 8413
		// (get) Token: 0x06005DDD RID: 24029 RVA: 0x0017F187 File Offset: 0x0017D387
		internal override ObjectType ObjectType
		{
			get
			{
				return ObjectType.Report;
			}
		}

		// Token: 0x170020DE RID: 8414
		// (get) Token: 0x06005DDE RID: 24030 RVA: 0x0017F18A File Offset: 0x0017D38A
		internal override string DataElementNameDefault
		{
			get
			{
				return "Report";
			}
		}

		// Token: 0x170020DF RID: 8415
		// (get) Token: 0x06005DDF RID: 24031 RVA: 0x0017F191 File Offset: 0x0017D391
		internal IntermediateFormatVersion IntermediateFormatVersion
		{
			get
			{
				return this.m_intermediateFormatVersion;
			}
		}

		// Token: 0x170020E0 RID: 8416
		// (get) Token: 0x06005DE0 RID: 24032 RVA: 0x0017F199 File Offset: 0x0017D399
		internal Guid ReportVersion
		{
			get
			{
				return this.m_reportVersion;
			}
		}

		// Token: 0x170020E1 RID: 8417
		// (get) Token: 0x06005DE1 RID: 24033 RVA: 0x0017F1A1 File Offset: 0x0017D3A1
		// (set) Token: 0x06005DE2 RID: 24034 RVA: 0x0017F1A9 File Offset: 0x0017D3A9
		internal string Author
		{
			get
			{
				return this.m_author;
			}
			set
			{
				this.m_author = value;
			}
		}

		// Token: 0x170020E2 RID: 8418
		// (get) Token: 0x06005DE3 RID: 24035 RVA: 0x0017F1B2 File Offset: 0x0017D3B2
		// (set) Token: 0x06005DE4 RID: 24036 RVA: 0x0017F1BA File Offset: 0x0017D3BA
		internal int AutoRefresh
		{
			get
			{
				return this.m_autoRefresh;
			}
			set
			{
				this.m_autoRefresh = value;
			}
		}

		// Token: 0x170020E3 RID: 8419
		// (get) Token: 0x06005DE5 RID: 24037 RVA: 0x0017F1C3 File Offset: 0x0017D3C3
		// (set) Token: 0x06005DE6 RID: 24038 RVA: 0x0017F1CB File Offset: 0x0017D3CB
		internal EmbeddedImageHashtable EmbeddedImages
		{
			get
			{
				return this.m_embeddedImages;
			}
			set
			{
				this.m_embeddedImages = value;
			}
		}

		// Token: 0x170020E4 RID: 8420
		// (get) Token: 0x06005DE7 RID: 24039 RVA: 0x0017F1D4 File Offset: 0x0017D3D4
		// (set) Token: 0x06005DE8 RID: 24040 RVA: 0x0017F1DC File Offset: 0x0017D3DC
		internal PageSection PageHeader
		{
			get
			{
				return this.m_pageHeader;
			}
			set
			{
				this.m_pageHeader = value;
			}
		}

		// Token: 0x170020E5 RID: 8421
		// (get) Token: 0x06005DE9 RID: 24041 RVA: 0x0017F1E5 File Offset: 0x0017D3E5
		internal bool PageHeaderEvaluation
		{
			get
			{
				return this.m_pageHeader != null && this.m_pageHeader.PostProcessEvaluate;
			}
		}

		// Token: 0x170020E6 RID: 8422
		// (get) Token: 0x06005DEA RID: 24042 RVA: 0x0017F1FC File Offset: 0x0017D3FC
		// (set) Token: 0x06005DEB RID: 24043 RVA: 0x0017F204 File Offset: 0x0017D404
		internal PageSection PageFooter
		{
			get
			{
				return this.m_pageFooter;
			}
			set
			{
				this.m_pageFooter = value;
			}
		}

		// Token: 0x170020E7 RID: 8423
		// (get) Token: 0x06005DEC RID: 24044 RVA: 0x0017F20D File Offset: 0x0017D40D
		internal bool PageFooterEvaluation
		{
			get
			{
				return this.m_pageFooter != null && this.m_pageFooter.PostProcessEvaluate;
			}
		}

		// Token: 0x170020E8 RID: 8424
		// (get) Token: 0x06005DED RID: 24045 RVA: 0x0017F224 File Offset: 0x0017D424
		internal double PageSectionWidth
		{
			get
			{
				double num = this.m_widthValue;
				if (this.m_columns > 1)
				{
					num += (double)(this.m_columns - 1) * (num + this.m_columnSpacingValue);
				}
				return num;
			}
		}

		// Token: 0x170020E9 RID: 8425
		// (get) Token: 0x06005DEE RID: 24046 RVA: 0x0017F257 File Offset: 0x0017D457
		// (set) Token: 0x06005DEF RID: 24047 RVA: 0x0017F25F File Offset: 0x0017D45F
		internal ReportItemCollection ReportItems
		{
			get
			{
				return this.m_reportItems;
			}
			set
			{
				this.m_reportItems = value;
			}
		}

		// Token: 0x170020EA RID: 8426
		// (get) Token: 0x06005DF0 RID: 24048 RVA: 0x0017F268 File Offset: 0x0017D468
		// (set) Token: 0x06005DF1 RID: 24049 RVA: 0x0017F270 File Offset: 0x0017D470
		internal DataSourceList DataSources
		{
			get
			{
				return this.m_dataSources;
			}
			set
			{
				this.m_dataSources = value;
			}
		}

		// Token: 0x170020EB RID: 8427
		// (get) Token: 0x06005DF2 RID: 24050 RVA: 0x0017F279 File Offset: 0x0017D479
		internal int DataSourceCount
		{
			get
			{
				if (this.m_dataSources != null)
				{
					return this.m_dataSources.Count;
				}
				return 0;
			}
		}

		// Token: 0x170020EC RID: 8428
		// (get) Token: 0x06005DF3 RID: 24051 RVA: 0x0017F290 File Offset: 0x0017D490
		// (set) Token: 0x06005DF4 RID: 24052 RVA: 0x0017F298 File Offset: 0x0017D498
		internal string PageHeight
		{
			get
			{
				return this.m_pageHeight;
			}
			set
			{
				this.m_pageHeight = value;
			}
		}

		// Token: 0x170020ED RID: 8429
		// (get) Token: 0x06005DF5 RID: 24053 RVA: 0x0017F2A1 File Offset: 0x0017D4A1
		// (set) Token: 0x06005DF6 RID: 24054 RVA: 0x0017F2A9 File Offset: 0x0017D4A9
		internal double PageHeightValue
		{
			get
			{
				return this.m_pageHeightValue;
			}
			set
			{
				this.m_pageHeightValue = value;
			}
		}

		// Token: 0x170020EE RID: 8430
		// (get) Token: 0x06005DF7 RID: 24055 RVA: 0x0017F2B2 File Offset: 0x0017D4B2
		// (set) Token: 0x06005DF8 RID: 24056 RVA: 0x0017F2BA File Offset: 0x0017D4BA
		internal string PageWidth
		{
			get
			{
				return this.m_pageWidth;
			}
			set
			{
				this.m_pageWidth = value;
			}
		}

		// Token: 0x170020EF RID: 8431
		// (get) Token: 0x06005DF9 RID: 24057 RVA: 0x0017F2C3 File Offset: 0x0017D4C3
		// (set) Token: 0x06005DFA RID: 24058 RVA: 0x0017F2CB File Offset: 0x0017D4CB
		internal double PageWidthValue
		{
			get
			{
				return this.m_pageWidthValue;
			}
			set
			{
				this.m_pageWidthValue = value;
			}
		}

		// Token: 0x170020F0 RID: 8432
		// (get) Token: 0x06005DFB RID: 24059 RVA: 0x0017F2D4 File Offset: 0x0017D4D4
		// (set) Token: 0x06005DFC RID: 24060 RVA: 0x0017F2DC File Offset: 0x0017D4DC
		internal string LeftMargin
		{
			get
			{
				return this.m_leftMargin;
			}
			set
			{
				this.m_leftMargin = value;
			}
		}

		// Token: 0x170020F1 RID: 8433
		// (get) Token: 0x06005DFD RID: 24061 RVA: 0x0017F2E5 File Offset: 0x0017D4E5
		// (set) Token: 0x06005DFE RID: 24062 RVA: 0x0017F2ED File Offset: 0x0017D4ED
		internal double LeftMarginValue
		{
			get
			{
				return this.m_leftMarginValue;
			}
			set
			{
				this.m_leftMarginValue = value;
			}
		}

		// Token: 0x170020F2 RID: 8434
		// (get) Token: 0x06005DFF RID: 24063 RVA: 0x0017F2F6 File Offset: 0x0017D4F6
		// (set) Token: 0x06005E00 RID: 24064 RVA: 0x0017F2FE File Offset: 0x0017D4FE
		internal string RightMargin
		{
			get
			{
				return this.m_rightMargin;
			}
			set
			{
				this.m_rightMargin = value;
			}
		}

		// Token: 0x170020F3 RID: 8435
		// (get) Token: 0x06005E01 RID: 24065 RVA: 0x0017F307 File Offset: 0x0017D507
		// (set) Token: 0x06005E02 RID: 24066 RVA: 0x0017F30F File Offset: 0x0017D50F
		internal double RightMarginValue
		{
			get
			{
				return this.m_rightMarginValue;
			}
			set
			{
				this.m_rightMarginValue = value;
			}
		}

		// Token: 0x170020F4 RID: 8436
		// (get) Token: 0x06005E03 RID: 24067 RVA: 0x0017F318 File Offset: 0x0017D518
		// (set) Token: 0x06005E04 RID: 24068 RVA: 0x0017F320 File Offset: 0x0017D520
		internal string TopMargin
		{
			get
			{
				return this.m_topMargin;
			}
			set
			{
				this.m_topMargin = value;
			}
		}

		// Token: 0x170020F5 RID: 8437
		// (get) Token: 0x06005E05 RID: 24069 RVA: 0x0017F329 File Offset: 0x0017D529
		// (set) Token: 0x06005E06 RID: 24070 RVA: 0x0017F331 File Offset: 0x0017D531
		internal double TopMarginValue
		{
			get
			{
				return this.m_topMarginValue;
			}
			set
			{
				this.m_topMarginValue = value;
			}
		}

		// Token: 0x170020F6 RID: 8438
		// (get) Token: 0x06005E07 RID: 24071 RVA: 0x0017F33A File Offset: 0x0017D53A
		// (set) Token: 0x06005E08 RID: 24072 RVA: 0x0017F342 File Offset: 0x0017D542
		internal string BottomMargin
		{
			get
			{
				return this.m_bottomMargin;
			}
			set
			{
				this.m_bottomMargin = value;
			}
		}

		// Token: 0x170020F7 RID: 8439
		// (get) Token: 0x06005E09 RID: 24073 RVA: 0x0017F34B File Offset: 0x0017D54B
		// (set) Token: 0x06005E0A RID: 24074 RVA: 0x0017F353 File Offset: 0x0017D553
		internal double BottomMarginValue
		{
			get
			{
				return this.m_bottomMarginValue;
			}
			set
			{
				this.m_bottomMarginValue = value;
			}
		}

		// Token: 0x170020F8 RID: 8440
		// (get) Token: 0x06005E0B RID: 24075 RVA: 0x0017F35C File Offset: 0x0017D55C
		// (set) Token: 0x06005E0C RID: 24076 RVA: 0x0017F364 File Offset: 0x0017D564
		internal int Columns
		{
			get
			{
				return this.m_columns;
			}
			set
			{
				this.m_columns = value;
			}
		}

		// Token: 0x170020F9 RID: 8441
		// (get) Token: 0x06005E0D RID: 24077 RVA: 0x0017F36D File Offset: 0x0017D56D
		// (set) Token: 0x06005E0E RID: 24078 RVA: 0x0017F375 File Offset: 0x0017D575
		internal string ColumnSpacing
		{
			get
			{
				return this.m_columnSpacing;
			}
			set
			{
				this.m_columnSpacing = value;
			}
		}

		// Token: 0x170020FA RID: 8442
		// (get) Token: 0x06005E0F RID: 24079 RVA: 0x0017F37E File Offset: 0x0017D57E
		// (set) Token: 0x06005E10 RID: 24080 RVA: 0x0017F386 File Offset: 0x0017D586
		internal double ColumnSpacingValue
		{
			get
			{
				return this.m_columnSpacingValue;
			}
			set
			{
				this.m_columnSpacingValue = value;
			}
		}

		// Token: 0x170020FB RID: 8443
		// (get) Token: 0x06005E11 RID: 24081 RVA: 0x0017F38F File Offset: 0x0017D58F
		// (set) Token: 0x06005E12 RID: 24082 RVA: 0x0017F397 File Offset: 0x0017D597
		internal DataAggregateInfoList PageAggregates
		{
			get
			{
				return this.m_pageAggregates;
			}
			set
			{
				this.m_pageAggregates = value;
			}
		}

		// Token: 0x170020FC RID: 8444
		// (get) Token: 0x06005E13 RID: 24083 RVA: 0x0017F3A0 File Offset: 0x0017D5A0
		// (set) Token: 0x06005E14 RID: 24084 RVA: 0x0017F3A8 File Offset: 0x0017D5A8
		internal byte[] CompiledCode
		{
			get
			{
				return this.m_exprCompiledCode;
			}
			set
			{
				this.m_exprCompiledCode = value;
			}
		}

		// Token: 0x170020FD RID: 8445
		// (get) Token: 0x06005E15 RID: 24085 RVA: 0x0017F3B1 File Offset: 0x0017D5B1
		// (set) Token: 0x06005E16 RID: 24086 RVA: 0x0017F3B9 File Offset: 0x0017D5B9
		internal bool CompiledCodeGeneratedWithRefusedPermissions
		{
			get
			{
				return this.m_exprCompiledCodeGeneratedWithRefusedPermissions;
			}
			set
			{
				this.m_exprCompiledCodeGeneratedWithRefusedPermissions = value;
			}
		}

		// Token: 0x170020FE RID: 8446
		// (get) Token: 0x06005E17 RID: 24087 RVA: 0x0017F3C2 File Offset: 0x0017D5C2
		// (set) Token: 0x06005E18 RID: 24088 RVA: 0x0017F3CA File Offset: 0x0017D5CA
		internal bool MergeOnePass
		{
			get
			{
				return this.m_mergeOnePass;
			}
			set
			{
				this.m_mergeOnePass = value;
			}
		}

		// Token: 0x170020FF RID: 8447
		// (get) Token: 0x06005E19 RID: 24089 RVA: 0x0017F3D3 File Offset: 0x0017D5D3
		// (set) Token: 0x06005E1A RID: 24090 RVA: 0x0017F3DB File Offset: 0x0017D5DB
		internal bool PageMergeOnePass
		{
			get
			{
				return this.m_pageMergeOnePass;
			}
			set
			{
				this.m_pageMergeOnePass = value;
			}
		}

		// Token: 0x17002100 RID: 8448
		// (get) Token: 0x06005E1B RID: 24091 RVA: 0x0017F3E4 File Offset: 0x0017D5E4
		// (set) Token: 0x06005E1C RID: 24092 RVA: 0x0017F3EC File Offset: 0x0017D5EC
		internal bool SubReportMergeTransactions
		{
			get
			{
				return this.m_subReportMergeTransactions;
			}
			set
			{
				this.m_subReportMergeTransactions = value;
			}
		}

		// Token: 0x17002101 RID: 8449
		// (get) Token: 0x06005E1D RID: 24093 RVA: 0x0017F3F5 File Offset: 0x0017D5F5
		// (set) Token: 0x06005E1E RID: 24094 RVA: 0x0017F3FD File Offset: 0x0017D5FD
		internal bool NeedPostGroupProcessing
		{
			get
			{
				return this.m_needPostGroupProcessing;
			}
			set
			{
				this.m_needPostGroupProcessing = value;
			}
		}

		// Token: 0x17002102 RID: 8450
		// (get) Token: 0x06005E1F RID: 24095 RVA: 0x0017F406 File Offset: 0x0017D606
		// (set) Token: 0x06005E20 RID: 24096 RVA: 0x0017F40E File Offset: 0x0017D60E
		internal bool HasPostSortAggregates
		{
			get
			{
				return this.m_hasPostSortAggregates;
			}
			set
			{
				this.m_hasPostSortAggregates = value;
			}
		}

		// Token: 0x17002103 RID: 8451
		// (get) Token: 0x06005E21 RID: 24097 RVA: 0x0017F417 File Offset: 0x0017D617
		// (set) Token: 0x06005E22 RID: 24098 RVA: 0x0017F41F File Offset: 0x0017D61F
		internal bool HasReportItemReferences
		{
			get
			{
				return this.m_hasReportItemReferences;
			}
			set
			{
				this.m_hasReportItemReferences = value;
			}
		}

		// Token: 0x17002104 RID: 8452
		// (get) Token: 0x06005E23 RID: 24099 RVA: 0x0017F428 File Offset: 0x0017D628
		// (set) Token: 0x06005E24 RID: 24100 RVA: 0x0017F430 File Offset: 0x0017D630
		internal Report.ShowHideTypes ShowHideType
		{
			get
			{
				return this.m_showHideType;
			}
			set
			{
				this.m_showHideType = value;
			}
		}

		// Token: 0x17002105 RID: 8453
		// (get) Token: 0x06005E25 RID: 24101 RVA: 0x0017F439 File Offset: 0x0017D639
		// (set) Token: 0x06005E26 RID: 24102 RVA: 0x0017F441 File Offset: 0x0017D641
		internal ImageStreamNames ImageStreamNames
		{
			get
			{
				return this.m_imageStreamNames;
			}
			set
			{
				this.m_imageStreamNames = value;
			}
		}

		// Token: 0x17002106 RID: 8454
		// (get) Token: 0x06005E27 RID: 24103 RVA: 0x0017F44A File Offset: 0x0017D64A
		// (set) Token: 0x06005E28 RID: 24104 RVA: 0x0017F452 File Offset: 0x0017D652
		internal bool ParametersNotUsedInQuery
		{
			get
			{
				return this.m_parametersNotUsedInQuery;
			}
			set
			{
				this.m_parametersNotUsedInQuery = value;
			}
		}

		// Token: 0x17002107 RID: 8455
		// (get) Token: 0x06005E29 RID: 24105 RVA: 0x0017F45B File Offset: 0x0017D65B
		// (set) Token: 0x06005E2A RID: 24106 RVA: 0x0017F463 File Offset: 0x0017D663
		internal int LastID
		{
			get
			{
				return this.m_lastID;
			}
			set
			{
				this.m_lastID = value;
			}
		}

		// Token: 0x17002108 RID: 8456
		// (get) Token: 0x06005E2B RID: 24107 RVA: 0x0017F46C File Offset: 0x0017D66C
		// (set) Token: 0x06005E2C RID: 24108 RVA: 0x0017F474 File Offset: 0x0017D674
		internal int BodyID
		{
			get
			{
				return this.m_bodyID;
			}
			set
			{
				this.m_bodyID = value;
			}
		}

		// Token: 0x17002109 RID: 8457
		// (get) Token: 0x06005E2D RID: 24109 RVA: 0x0017F47D File Offset: 0x0017D67D
		// (set) Token: 0x06005E2E RID: 24110 RVA: 0x0017F485 File Offset: 0x0017D685
		internal SubReportList SubReports
		{
			get
			{
				return this.m_subReports;
			}
			set
			{
				this.m_subReports = value;
			}
		}

		// Token: 0x1700210A RID: 8458
		// (get) Token: 0x06005E2F RID: 24111 RVA: 0x0017F48E File Offset: 0x0017D68E
		// (set) Token: 0x06005E30 RID: 24112 RVA: 0x0017F496 File Offset: 0x0017D696
		internal bool HasImageStreams
		{
			get
			{
				return this.m_hasImageStreams;
			}
			set
			{
				this.m_hasImageStreams = value;
			}
		}

		// Token: 0x1700210B RID: 8459
		// (get) Token: 0x06005E31 RID: 24113 RVA: 0x0017F49F File Offset: 0x0017D69F
		// (set) Token: 0x06005E32 RID: 24114 RVA: 0x0017F4A7 File Offset: 0x0017D6A7
		internal bool HasLabels
		{
			get
			{
				return this.m_hasLabels;
			}
			set
			{
				this.m_hasLabels = value;
			}
		}

		// Token: 0x1700210C RID: 8460
		// (get) Token: 0x06005E33 RID: 24115 RVA: 0x0017F4B0 File Offset: 0x0017D6B0
		// (set) Token: 0x06005E34 RID: 24116 RVA: 0x0017F4B8 File Offset: 0x0017D6B8
		internal bool HasBookmarks
		{
			get
			{
				return this.m_hasBookmarks;
			}
			set
			{
				this.m_hasBookmarks = value;
			}
		}

		// Token: 0x1700210D RID: 8461
		// (get) Token: 0x06005E35 RID: 24117 RVA: 0x0017F4C1 File Offset: 0x0017D6C1
		// (set) Token: 0x06005E36 RID: 24118 RVA: 0x0017F4C9 File Offset: 0x0017D6C9
		internal ReportSize PageHeightForRendering
		{
			get
			{
				return this.m_pageHeightForRendering;
			}
			set
			{
				this.m_pageHeightForRendering = value;
			}
		}

		// Token: 0x1700210E RID: 8462
		// (get) Token: 0x06005E37 RID: 24119 RVA: 0x0017F4D2 File Offset: 0x0017D6D2
		// (set) Token: 0x06005E38 RID: 24120 RVA: 0x0017F4DA File Offset: 0x0017D6DA
		internal ReportSize PageWidthForRendering
		{
			get
			{
				return this.m_pageWidthForRendering;
			}
			set
			{
				this.m_pageWidthForRendering = value;
			}
		}

		// Token: 0x1700210F RID: 8463
		// (get) Token: 0x06005E39 RID: 24121 RVA: 0x0017F4E3 File Offset: 0x0017D6E3
		// (set) Token: 0x06005E3A RID: 24122 RVA: 0x0017F4EB File Offset: 0x0017D6EB
		internal ReportSize LeftMarginForRendering
		{
			get
			{
				return this.m_leftMarginForRendering;
			}
			set
			{
				this.m_leftMarginForRendering = value;
			}
		}

		// Token: 0x17002110 RID: 8464
		// (get) Token: 0x06005E3B RID: 24123 RVA: 0x0017F4F4 File Offset: 0x0017D6F4
		// (set) Token: 0x06005E3C RID: 24124 RVA: 0x0017F4FC File Offset: 0x0017D6FC
		internal ReportSize RightMarginForRendering
		{
			get
			{
				return this.m_rightMarginForRendering;
			}
			set
			{
				this.m_rightMarginForRendering = value;
			}
		}

		// Token: 0x17002111 RID: 8465
		// (get) Token: 0x06005E3D RID: 24125 RVA: 0x0017F505 File Offset: 0x0017D705
		// (set) Token: 0x06005E3E RID: 24126 RVA: 0x0017F50D File Offset: 0x0017D70D
		internal ReportSize TopMarginForRendering
		{
			get
			{
				return this.m_topMarginForRendering;
			}
			set
			{
				this.m_topMarginForRendering = value;
			}
		}

		// Token: 0x17002112 RID: 8466
		// (get) Token: 0x06005E3F RID: 24127 RVA: 0x0017F516 File Offset: 0x0017D716
		// (set) Token: 0x06005E40 RID: 24128 RVA: 0x0017F51E File Offset: 0x0017D71E
		internal ReportSize BottomMarginForRendering
		{
			get
			{
				return this.m_bottomMarginForRendering;
			}
			set
			{
				this.m_bottomMarginForRendering = value;
			}
		}

		// Token: 0x17002113 RID: 8467
		// (get) Token: 0x06005E41 RID: 24129 RVA: 0x0017F527 File Offset: 0x0017D727
		// (set) Token: 0x06005E42 RID: 24130 RVA: 0x0017F52F File Offset: 0x0017D72F
		internal ReportSize ColumnSpacingForRendering
		{
			get
			{
				return this.m_columnSpacingForRendering;
			}
			set
			{
				this.m_columnSpacingForRendering = value;
			}
		}

		// Token: 0x17002114 RID: 8468
		// (get) Token: 0x06005E43 RID: 24131 RVA: 0x0017F538 File Offset: 0x0017D738
		// (set) Token: 0x06005E44 RID: 24132 RVA: 0x0017F540 File Offset: 0x0017D740
		internal ParameterDefList Parameters
		{
			get
			{
				return this.m_parameters;
			}
			set
			{
				this.m_parameters = value;
			}
		}

		// Token: 0x17002115 RID: 8469
		// (get) Token: 0x06005E45 RID: 24133 RVA: 0x0017F549 File Offset: 0x0017D749
		// (set) Token: 0x06005E46 RID: 24134 RVA: 0x0017F551 File Offset: 0x0017D751
		internal string OneDataSetName
		{
			get
			{
				return this.m_oneDataSetName;
			}
			set
			{
				this.m_oneDataSetName = value;
			}
		}

		// Token: 0x17002116 RID: 8470
		// (get) Token: 0x06005E47 RID: 24135 RVA: 0x0017F55A File Offset: 0x0017D75A
		// (set) Token: 0x06005E48 RID: 24136 RVA: 0x0017F562 File Offset: 0x0017D762
		internal StringList CodeModules
		{
			get
			{
				return this.m_codeModules;
			}
			set
			{
				this.m_codeModules = value;
			}
		}

		// Token: 0x17002117 RID: 8471
		// (get) Token: 0x06005E49 RID: 24137 RVA: 0x0017F56B File Offset: 0x0017D76B
		// (set) Token: 0x06005E4A RID: 24138 RVA: 0x0017F573 File Offset: 0x0017D773
		internal CodeClassList CodeClasses
		{
			get
			{
				return this.m_codeClasses;
			}
			set
			{
				this.m_codeClasses = value;
			}
		}

		// Token: 0x17002118 RID: 8472
		// (get) Token: 0x06005E4B RID: 24139 RVA: 0x0017F57C File Offset: 0x0017D77C
		// (set) Token: 0x06005E4C RID: 24140 RVA: 0x0017F584 File Offset: 0x0017D784
		internal bool HasSpecialRecursiveAggregates
		{
			get
			{
				return this.m_hasSpecialRecursiveAggregates;
			}
			set
			{
				this.m_hasSpecialRecursiveAggregates = value;
			}
		}

		// Token: 0x17002119 RID: 8473
		// (get) Token: 0x06005E4D RID: 24141 RVA: 0x0017F58D File Offset: 0x0017D78D
		// (set) Token: 0x06005E4E RID: 24142 RVA: 0x0017F595 File Offset: 0x0017D795
		internal ExpressionInfo Language
		{
			get
			{
				return this.m_language;
			}
			set
			{
				this.m_language = value;
			}
		}

		// Token: 0x1700211A RID: 8474
		// (get) Token: 0x06005E4F RID: 24143 RVA: 0x0017F59E File Offset: 0x0017D79E
		internal ReportExprHost ReportExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x1700211B RID: 8475
		// (get) Token: 0x06005E50 RID: 24144 RVA: 0x0017F5A6 File Offset: 0x0017D7A6
		// (set) Token: 0x06005E51 RID: 24145 RVA: 0x0017F5AE File Offset: 0x0017D7AE
		internal string DataTransform
		{
			get
			{
				return this.m_dataTransform;
			}
			set
			{
				this.m_dataTransform = value;
			}
		}

		// Token: 0x1700211C RID: 8476
		// (get) Token: 0x06005E52 RID: 24146 RVA: 0x0017F5B7 File Offset: 0x0017D7B7
		// (set) Token: 0x06005E53 RID: 24147 RVA: 0x0017F5BF File Offset: 0x0017D7BF
		internal string DataSchema
		{
			get
			{
				return this.m_dataSchema;
			}
			set
			{
				this.m_dataSchema = value;
			}
		}

		// Token: 0x1700211D RID: 8477
		// (get) Token: 0x06005E54 RID: 24148 RVA: 0x0017F5C8 File Offset: 0x0017D7C8
		// (set) Token: 0x06005E55 RID: 24149 RVA: 0x0017F5D0 File Offset: 0x0017D7D0
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

		// Token: 0x1700211E RID: 8478
		// (get) Token: 0x06005E56 RID: 24150 RVA: 0x0017F5D9 File Offset: 0x0017D7D9
		// (set) Token: 0x06005E57 RID: 24151 RVA: 0x0017F5E1 File Offset: 0x0017D7E1
		internal string Code
		{
			get
			{
				return this.m_code;
			}
			set
			{
				this.m_code = value;
			}
		}

		// Token: 0x1700211F RID: 8479
		// (get) Token: 0x06005E58 RID: 24152 RVA: 0x0017F5EA File Offset: 0x0017D7EA
		// (set) Token: 0x06005E59 RID: 24153 RVA: 0x0017F5F2 File Offset: 0x0017D7F2
		internal bool HasUserSortFilter
		{
			get
			{
				return this.m_hasUserSortFilter;
			}
			set
			{
				this.m_hasUserSortFilter = value;
			}
		}

		// Token: 0x17002120 RID: 8480
		// (get) Token: 0x06005E5A RID: 24154 RVA: 0x0017F5FB File Offset: 0x0017D7FB
		// (set) Token: 0x06005E5B RID: 24155 RVA: 0x0017F603 File Offset: 0x0017D803
		internal string InteractiveHeight
		{
			get
			{
				return this.m_interactiveHeight;
			}
			set
			{
				this.m_interactiveHeight = value;
			}
		}

		// Token: 0x17002121 RID: 8481
		// (get) Token: 0x06005E5C RID: 24156 RVA: 0x0017F60C File Offset: 0x0017D80C
		// (set) Token: 0x06005E5D RID: 24157 RVA: 0x0017F62C File Offset: 0x0017D82C
		internal double InteractiveHeightValue
		{
			get
			{
				if (this.m_interactiveHeightValue >= 0.0)
				{
					return this.m_interactiveHeightValue;
				}
				return this.m_pageHeightValue;
			}
			set
			{
				this.m_interactiveHeightValue = value;
			}
		}

		// Token: 0x17002122 RID: 8482
		// (get) Token: 0x06005E5E RID: 24158 RVA: 0x0017F635 File Offset: 0x0017D835
		// (set) Token: 0x06005E5F RID: 24159 RVA: 0x0017F63D File Offset: 0x0017D83D
		internal string InteractiveWidth
		{
			get
			{
				return this.m_interactiveWidth;
			}
			set
			{
				this.m_interactiveWidth = value;
			}
		}

		// Token: 0x17002123 RID: 8483
		// (get) Token: 0x06005E60 RID: 24160 RVA: 0x0017F646 File Offset: 0x0017D846
		// (set) Token: 0x06005E61 RID: 24161 RVA: 0x0017F666 File Offset: 0x0017D866
		internal double InteractiveWidthValue
		{
			get
			{
				if (this.m_interactiveWidthValue >= 0.0)
				{
					return this.m_interactiveWidthValue;
				}
				return this.m_pageWidthValue;
			}
			set
			{
				this.m_interactiveWidthValue = value;
			}
		}

		// Token: 0x17002124 RID: 8484
		// (get) Token: 0x06005E62 RID: 24162 RVA: 0x0017F66F File Offset: 0x0017D86F
		// (set) Token: 0x06005E63 RID: 24163 RVA: 0x0017F677 File Offset: 0x0017D877
		internal InScopeSortFilterHashtable NonDetailSortFiltersInScope
		{
			get
			{
				return this.m_nonDetailSortFiltersInScope;
			}
			set
			{
				this.m_nonDetailSortFiltersInScope = value;
			}
		}

		// Token: 0x17002125 RID: 8485
		// (get) Token: 0x06005E64 RID: 24164 RVA: 0x0017F680 File Offset: 0x0017D880
		// (set) Token: 0x06005E65 RID: 24165 RVA: 0x0017F688 File Offset: 0x0017D888
		internal InScopeSortFilterHashtable DetailSortFiltersInScope
		{
			get
			{
				return this.m_detailSortFiltersInScope;
			}
			set
			{
				this.m_detailSortFiltersInScope = value;
			}
		}

		// Token: 0x17002126 RID: 8486
		// (get) Token: 0x06005E66 RID: 24166 RVA: 0x0017F691 File Offset: 0x0017D891
		internal string ExprHostAssemblyName
		{
			get
			{
				return "expression_host_" + this.m_reportVersion.ToString().Replace("-", "");
			}
		}

		// Token: 0x17002127 RID: 8487
		// (get) Token: 0x06005E67 RID: 24167 RVA: 0x0017F6BD File Offset: 0x0017D8BD
		// (set) Token: 0x06005E68 RID: 24168 RVA: 0x0017F6C5 File Offset: 0x0017D8C5
		internal int LastAggregateID
		{
			get
			{
				return this.m_lastAggregateID;
			}
			set
			{
				this.m_lastAggregateID = value;
			}
		}

		// Token: 0x17002128 RID: 8488
		// (get) Token: 0x06005E69 RID: 24169 RVA: 0x0017F6CE File Offset: 0x0017D8CE
		// (set) Token: 0x06005E6A RID: 24170 RVA: 0x0017F6D6 File Offset: 0x0017D8D6
		internal long MainChunkSize
		{
			get
			{
				return this.m_mainChunkSize;
			}
			set
			{
				this.m_mainChunkSize = value;
			}
		}

		// Token: 0x06005E6B RID: 24171 RVA: 0x0017F6E0 File Offset: 0x0017D8E0
		internal override bool Initialize(InitializationContext context)
		{
			context.Location = LocationFlags.None;
			context.ObjectType = this.ObjectType;
			context.ObjectName = null;
			base.Initialize(context);
			if (this.m_language != null)
			{
				this.m_language.Initialize("Language", context);
				context.ExprHostBuilder.ReportLanguage(this.m_language);
			}
			context.ReportDataElementStyleAttribute = this.m_dataElementStyleAttribute;
			this.m_pageHeightValue = context.ValidateSize(ref this.m_pageHeight, "PageHeight");
			if (this.m_pageHeightValue <= 0.0)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsInvalidSize, Severity.Error, context.ObjectType, context.ObjectName, "PageHeight", new string[] { this.m_pageHeightValue.ToString(CultureInfo.InvariantCulture) });
			}
			this.m_pageWidthValue = context.ValidateSize(ref this.m_pageWidth, "PageWidth");
			if (this.m_pageWidthValue <= 0.0)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsInvalidSize, Severity.Error, context.ObjectType, context.ObjectName, "PageWidth", new string[] { this.m_pageWidthValue.ToString(CultureInfo.InvariantCulture) });
			}
			if (this.m_interactiveHeight != null)
			{
				this.m_interactiveHeightValue = context.ValidateSize(ref this.m_interactiveHeight, false, "InteractiveHeight");
				if (0.0 == this.m_interactiveHeightValue)
				{
					this.m_interactiveHeightValue = double.MaxValue;
				}
				else if (this.m_interactiveHeightValue < 0.0)
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsInvalidSize, Severity.Error, context.ObjectType, context.ObjectName, "InteractiveHeight", new string[] { this.m_interactiveHeightValue.ToString(CultureInfo.InvariantCulture) });
				}
			}
			if (this.m_interactiveWidth != null)
			{
				this.m_interactiveWidthValue = context.ValidateSize(ref this.m_interactiveWidth, false, "InteractiveWidth");
				if (0.0 == this.m_interactiveWidthValue)
				{
					this.m_interactiveWidthValue = double.MaxValue;
				}
				else if (this.m_interactiveWidthValue < 0.0)
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsInvalidSize, Severity.Error, context.ObjectType, context.ObjectName, "InteractiveWidth", new string[] { this.m_interactiveWidthValue.ToString(CultureInfo.InvariantCulture) });
				}
			}
			this.m_leftMarginValue = context.ValidateSize(ref this.m_leftMargin, "LeftMargin");
			this.m_rightMarginValue = context.ValidateSize(ref this.m_rightMargin, "RightMargin");
			this.m_topMarginValue = context.ValidateSize(ref this.m_topMargin, "TopMargin");
			this.m_bottomMarginValue = context.ValidateSize(ref this.m_bottomMargin, "BottomMargin");
			this.m_columnSpacingValue = context.ValidateSize(ref this.m_columnSpacing, "ColumnSpacing");
			if (this.m_dataSources != null)
			{
				for (int i = 0; i < this.m_dataSources.Count; i++)
				{
					Global.Tracer.Assert(this.m_dataSources[i] != null);
					this.m_dataSources[i].Initialize(context);
				}
			}
			this.BodyInitialize(context);
			this.PageHeaderFooterInitialize(context);
			if (context.ExprHostBuilder.CustomCode)
			{
				context.ExprHostBuilder.CustomCodeProxyStart();
				if (this.m_codeClasses != null && this.m_codeClasses.Count > 0)
				{
					for (int j = this.m_codeClasses.Count - 1; j >= 0; j--)
					{
						CodeClass codeClass = this.m_codeClasses[j];
						context.ExprHostBuilder.CustomCodeClassInstance(codeClass.ClassName, codeClass.InstanceName, j);
					}
				}
				if (this.m_code != null && this.m_code.Length > 0)
				{
					context.ExprHostBuilder.ReportCode(this.m_code);
				}
				context.ExprHostBuilder.CustomCodeProxyEnd();
			}
			return false;
		}

		// Token: 0x06005E6C RID: 24172 RVA: 0x0017FABC File Offset: 0x0017DCBC
		internal void BodyInitialize(InitializationContext context)
		{
			context.RegisterReportItems(this.m_reportItems);
			this.m_reportItems.Initialize(context, true);
			context.ValidateUserSortInnerScope("0_ReportScope");
			context.TextboxesWithDetailSortExpressionInitialize();
			context.CalculateSortFilterGroupingLists();
			context.UnRegisterReportItems(this.m_reportItems);
		}

		// Token: 0x06005E6D RID: 24173 RVA: 0x0017FB0B File Offset: 0x0017DD0B
		internal void PageHeaderFooterInitialize(InitializationContext context)
		{
			context.RegisterPageSectionScope(this.m_pageAggregates);
			if (this.m_pageHeader != null)
			{
				this.m_pageHeader.Initialize(context);
			}
			if (this.m_pageFooter != null)
			{
				this.m_pageFooter.Initialize(context);
			}
			context.UnRegisterPageSectionScope();
		}

		// Token: 0x06005E6E RID: 24174 RVA: 0x0017FB4B File Offset: 0x0017DD4B
		DataAggregateInfoList[] IAggregateHolder.GetAggregateLists()
		{
			return new DataAggregateInfoList[] { this.m_pageAggregates };
		}

		// Token: 0x06005E6F RID: 24175 RVA: 0x0017FB5C File Offset: 0x0017DD5C
		DataAggregateInfoList[] IAggregateHolder.GetPostSortAggregateLists()
		{
			return null;
		}

		// Token: 0x06005E70 RID: 24176 RVA: 0x0017FB5F File Offset: 0x0017DD5F
		void IAggregateHolder.ClearIfEmpty()
		{
			Global.Tracer.Assert(this.m_pageAggregates != null);
			if (this.m_pageAggregates.Count == 0)
			{
				this.m_pageAggregates = null;
			}
		}

		// Token: 0x06005E71 RID: 24177 RVA: 0x0017FB88 File Offset: 0x0017DD88
		internal override void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null, "(reportExprHost != null && reportObjectModel != null)");
			this.m_exprHost = reportExprHost;
			base.ReportItemSetExprHost(this.m_exprHost, reportObjectModel);
		}

		// Token: 0x06005E72 RID: 24178 RVA: 0x0017FBB8 File Offset: 0x0017DDB8
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItem, new MemberInfoList
			{
				new MemberInfo(MemberName.IntermediateFormatVersion, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.IntermediateFormatVersion),
				new MemberInfo(MemberName.ReportVersion, Token.Guid),
				new MemberInfo(MemberName.Author, Token.String),
				new MemberInfo(MemberName.AutoRefresh, Token.Int32),
				new MemberInfo(MemberName.EmbeddedImages, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.EmbeddedImageHashtable),
				new MemberInfo(MemberName.PageHeader, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.PageSection),
				new MemberInfo(MemberName.PageFooter, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.PageSection),
				new MemberInfo(MemberName.ReportItems, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItemCollection),
				new MemberInfo(MemberName.DataSources, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.DataSourceList),
				new MemberInfo(MemberName.PageHeight, Token.String),
				new MemberInfo(MemberName.PageHeightValue, Token.Double),
				new MemberInfo(MemberName.PageWidth, Token.String),
				new MemberInfo(MemberName.PageWidthValue, Token.Double),
				new MemberInfo(MemberName.LeftMargin, Token.String),
				new MemberInfo(MemberName.LeftMarginValue, Token.Double),
				new MemberInfo(MemberName.RightMargin, Token.String),
				new MemberInfo(MemberName.RightMarginValue, Token.Double),
				new MemberInfo(MemberName.TopMargin, Token.String),
				new MemberInfo(MemberName.TopMarginValue, Token.Double),
				new MemberInfo(MemberName.BottomMargin, Token.String),
				new MemberInfo(MemberName.BottomMarginValue, Token.Double),
				new MemberInfo(MemberName.Columns, Token.Int32),
				new MemberInfo(MemberName.ColumnSpacing, Token.String),
				new MemberInfo(MemberName.ColumnSpacingValue, Token.Double),
				new MemberInfo(MemberName.PageAggregates, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.DataAggregateInfoList),
				new MemberInfo(MemberName.CompiledCode, Token.TypedArray),
				new MemberInfo(MemberName.MergeOnePass, Token.Boolean),
				new MemberInfo(MemberName.PageMergeOnePass, Token.Boolean),
				new MemberInfo(MemberName.SubReportMergeTransactions, Token.Boolean),
				new MemberInfo(MemberName.NeedPostGroupProcessing, Token.Boolean),
				new MemberInfo(MemberName.HasPostSortAggregates, Token.Boolean),
				new MemberInfo(MemberName.HasReportItemReferences, Token.Boolean),
				new MemberInfo(MemberName.ShowHideType, Token.Enum),
				new MemberInfo(MemberName.Images, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ImageStreamNames),
				new MemberInfo(MemberName.LastID, Token.Int32),
				new MemberInfo(MemberName.BodyID, Token.Int32),
				new MemberInfo(MemberName.SubReports, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.SubReportList),
				new MemberInfo(MemberName.HasImageStreams, Token.Boolean),
				new MemberInfo(MemberName.HasLabels, Token.Boolean),
				new MemberInfo(MemberName.HasBookmarks, Token.Boolean),
				new MemberInfo(MemberName.SnapshotProcessingEnabled, Token.Boolean),
				new MemberInfo(MemberName.Parameters, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ParameterDefList),
				new MemberInfo(MemberName.DataSetName, Token.String),
				new MemberInfo(MemberName.CodeModules, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.StringList),
				new MemberInfo(MemberName.CodeClasses, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.CodeClassList),
				new MemberInfo(MemberName.HasSpecialRecursiveAggregates, Token.Boolean),
				new MemberInfo(MemberName.Language, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.DataTransform, Token.String),
				new MemberInfo(MemberName.DataSchema, Token.String),
				new MemberInfo(MemberName.DataElementStyleAttribute, Token.Boolean),
				new MemberInfo(MemberName.Code, Token.String),
				new MemberInfo(MemberName.HasUserSortFilter, Token.Boolean),
				new MemberInfo(MemberName.CompiledCodeGeneratedWithRefusedPermissions, Token.Boolean),
				new MemberInfo(MemberName.InteractiveHeight, Token.String),
				new MemberInfo(MemberName.InteractiveHeightValue, Token.Double),
				new MemberInfo(MemberName.InteractiveWidth, Token.String),
				new MemberInfo(MemberName.InteractiveWidthValue, Token.Double),
				new MemberInfo(MemberName.NonDetailSortFiltersInScope, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.InScopeSortFilterHashtable),
				new MemberInfo(MemberName.DetailSortFiltersInScope, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.InScopeSortFilterHashtable)
			});
		}

		// Token: 0x04002FEE RID: 12270
		private IntermediateFormatVersion m_intermediateFormatVersion;

		// Token: 0x04002FEF RID: 12271
		private Guid m_reportVersion = Guid.Empty;

		// Token: 0x04002FF0 RID: 12272
		private string m_author;

		// Token: 0x04002FF1 RID: 12273
		private int m_autoRefresh;

		// Token: 0x04002FF2 RID: 12274
		private EmbeddedImageHashtable m_embeddedImages;

		// Token: 0x04002FF3 RID: 12275
		private PageSection m_pageHeader;

		// Token: 0x04002FF4 RID: 12276
		private PageSection m_pageFooter;

		// Token: 0x04002FF5 RID: 12277
		private ReportItemCollection m_reportItems;

		// Token: 0x04002FF6 RID: 12278
		private DataSourceList m_dataSources;

		// Token: 0x04002FF7 RID: 12279
		private string m_pageHeight = "11in";

		// Token: 0x04002FF8 RID: 12280
		private double m_pageHeightValue;

		// Token: 0x04002FF9 RID: 12281
		private string m_pageWidth = "8.5in";

		// Token: 0x04002FFA RID: 12282
		private double m_pageWidthValue;

		// Token: 0x04002FFB RID: 12283
		private string m_leftMargin = "0in";

		// Token: 0x04002FFC RID: 12284
		private double m_leftMarginValue;

		// Token: 0x04002FFD RID: 12285
		private string m_rightMargin = "0in";

		// Token: 0x04002FFE RID: 12286
		private double m_rightMarginValue;

		// Token: 0x04002FFF RID: 12287
		private string m_topMargin = "0in";

		// Token: 0x04003000 RID: 12288
		private double m_topMarginValue;

		// Token: 0x04003001 RID: 12289
		private string m_bottomMargin = "0in";

		// Token: 0x04003002 RID: 12290
		private double m_bottomMarginValue;

		// Token: 0x04003003 RID: 12291
		private int m_columns = 1;

		// Token: 0x04003004 RID: 12292
		private string m_columnSpacing = "0.5in";

		// Token: 0x04003005 RID: 12293
		private double m_columnSpacingValue;

		// Token: 0x04003006 RID: 12294
		private DataAggregateInfoList m_pageAggregates;

		// Token: 0x04003007 RID: 12295
		private byte[] m_exprCompiledCode;

		// Token: 0x04003008 RID: 12296
		private bool m_exprCompiledCodeGeneratedWithRefusedPermissions;

		// Token: 0x04003009 RID: 12297
		private bool m_mergeOnePass;

		// Token: 0x0400300A RID: 12298
		private bool m_pageMergeOnePass;

		// Token: 0x0400300B RID: 12299
		private bool m_subReportMergeTransactions;

		// Token: 0x0400300C RID: 12300
		private bool m_needPostGroupProcessing;

		// Token: 0x0400300D RID: 12301
		private bool m_hasPostSortAggregates;

		// Token: 0x0400300E RID: 12302
		private bool m_hasReportItemReferences;

		// Token: 0x0400300F RID: 12303
		private Report.ShowHideTypes m_showHideType;

		// Token: 0x04003010 RID: 12304
		private ImageStreamNames m_imageStreamNames;

		// Token: 0x04003011 RID: 12305
		private int m_lastID;

		// Token: 0x04003012 RID: 12306
		private int m_bodyID;

		// Token: 0x04003013 RID: 12307
		private SubReportList m_subReports;

		// Token: 0x04003014 RID: 12308
		private bool m_hasImageStreams;

		// Token: 0x04003015 RID: 12309
		private bool m_hasLabels;

		// Token: 0x04003016 RID: 12310
		private bool m_hasBookmarks;

		// Token: 0x04003017 RID: 12311
		private bool m_parametersNotUsedInQuery;

		// Token: 0x04003018 RID: 12312
		private ParameterDefList m_parameters;

		// Token: 0x04003019 RID: 12313
		private string m_oneDataSetName;

		// Token: 0x0400301A RID: 12314
		private StringList m_codeModules;

		// Token: 0x0400301B RID: 12315
		private CodeClassList m_codeClasses;

		// Token: 0x0400301C RID: 12316
		private bool m_hasSpecialRecursiveAggregates;

		// Token: 0x0400301D RID: 12317
		private ExpressionInfo m_language;

		// Token: 0x0400301E RID: 12318
		private string m_dataTransform;

		// Token: 0x0400301F RID: 12319
		private string m_dataSchema;

		// Token: 0x04003020 RID: 12320
		private bool m_dataElementStyleAttribute = true;

		// Token: 0x04003021 RID: 12321
		private string m_code;

		// Token: 0x04003022 RID: 12322
		private bool m_hasUserSortFilter;

		// Token: 0x04003023 RID: 12323
		private string m_interactiveHeight;

		// Token: 0x04003024 RID: 12324
		private double m_interactiveHeightValue = -1.0;

		// Token: 0x04003025 RID: 12325
		private string m_interactiveWidth;

		// Token: 0x04003026 RID: 12326
		private double m_interactiveWidthValue = -1.0;

		// Token: 0x04003027 RID: 12327
		private InScopeSortFilterHashtable m_nonDetailSortFiltersInScope;

		// Token: 0x04003028 RID: 12328
		private InScopeSortFilterHashtable m_detailSortFiltersInScope;

		// Token: 0x04003029 RID: 12329
		[NonSerialized]
		private int m_lastAggregateID = -1;

		// Token: 0x0400302A RID: 12330
		[NonSerialized]
		private ReportExprHost m_exprHost;

		// Token: 0x0400302B RID: 12331
		[NonSerialized]
		private ReportSize m_pageHeightForRendering;

		// Token: 0x0400302C RID: 12332
		[NonSerialized]
		private ReportSize m_pageWidthForRendering;

		// Token: 0x0400302D RID: 12333
		[NonSerialized]
		private ReportSize m_leftMarginForRendering;

		// Token: 0x0400302E RID: 12334
		[NonSerialized]
		private ReportSize m_rightMarginForRendering;

		// Token: 0x0400302F RID: 12335
		[NonSerialized]
		private ReportSize m_topMarginForRendering;

		// Token: 0x04003030 RID: 12336
		[NonSerialized]
		private ReportSize m_bottomMarginForRendering;

		// Token: 0x04003031 RID: 12337
		[NonSerialized]
		private ReportSize m_columnSpacingForRendering;

		// Token: 0x04003032 RID: 12338
		[NonSerialized]
		private long m_mainChunkSize = -1L;

		// Token: 0x02000CBD RID: 3261
		internal enum ShowHideTypes
		{
			// Token: 0x04004E66 RID: 20070
			None,
			// Token: 0x04004E67 RID: 20071
			Static,
			// Token: 0x04004E68 RID: 20072
			Interactive
		}
	}
}
