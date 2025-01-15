using System;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200050C RID: 1292
	[Serializable]
	internal sealed class ProcessingIntermediateFormatVersion
	{
		// Token: 0x06004492 RID: 17554 RVA: 0x0011EDA0 File Offset: 0x0011CFA0
		internal ProcessingIntermediateFormatVersion(IntermediateFormatVersion version)
		{
			this.m_version = version;
		}

		// Token: 0x17001CC7 RID: 7367
		// (get) Token: 0x06004493 RID: 17555 RVA: 0x0011EDAF File Offset: 0x0011CFAF
		// (set) Token: 0x06004494 RID: 17556 RVA: 0x0011EDBC File Offset: 0x0011CFBC
		internal int Major
		{
			get
			{
				return this.m_version.Major;
			}
			set
			{
				this.m_version.Major = value;
			}
		}

		// Token: 0x17001CC8 RID: 7368
		// (get) Token: 0x06004495 RID: 17557 RVA: 0x0011EDCA File Offset: 0x0011CFCA
		// (set) Token: 0x06004496 RID: 17558 RVA: 0x0011EDD7 File Offset: 0x0011CFD7
		internal int Minor
		{
			get
			{
				return this.m_version.Minor;
			}
			set
			{
				this.m_version.Minor = value;
			}
		}

		// Token: 0x17001CC9 RID: 7369
		// (get) Token: 0x06004497 RID: 17559 RVA: 0x0011EDE5 File Offset: 0x0011CFE5
		// (set) Token: 0x06004498 RID: 17560 RVA: 0x0011EDF2 File Offset: 0x0011CFF2
		internal int Build
		{
			get
			{
				return this.m_version.Build;
			}
			set
			{
				this.m_version.Build = value;
			}
		}

		// Token: 0x17001CCA RID: 7370
		// (get) Token: 0x06004499 RID: 17561 RVA: 0x0011EE00 File Offset: 0x0011D000
		internal bool IsOldVersion
		{
			get
			{
				return this.m_version.IsOldVersion;
			}
		}

		// Token: 0x17001CCB RID: 7371
		// (get) Token: 0x0600449A RID: 17562 RVA: 0x0011EE0D File Offset: 0x0011D00D
		internal bool IsRIF11_orOlder
		{
			get
			{
				return this.m_version.CompareTo(11, 0, 0) <= 0;
			}
		}

		// Token: 0x17001CCC RID: 7372
		// (get) Token: 0x0600449B RID: 17563 RVA: 0x0011EE24 File Offset: 0x0011D024
		internal bool IsRIF11_orNewer
		{
			get
			{
				return this.m_version.CompareTo(11, 0, 0) >= 0;
			}
		}

		// Token: 0x17001CCD RID: 7373
		// (get) Token: 0x0600449C RID: 17564 RVA: 0x0011EE3B File Offset: 0x0011D03B
		internal bool IsRS2000_Beta2_orOlder
		{
			get
			{
				return this.m_version.CompareTo(8, 0, 673) <= 0;
			}
		}

		// Token: 0x17001CCE RID: 7374
		// (get) Token: 0x0600449D RID: 17565 RVA: 0x0011EE55 File Offset: 0x0011D055
		internal bool IsRS2000_WithSpecialRecursiveAggregates
		{
			get
			{
				return this.m_version.CompareTo(8, 0, 700) >= 0;
			}
		}

		// Token: 0x17001CCF RID: 7375
		// (get) Token: 0x0600449E RID: 17566 RVA: 0x0011EE6F File Offset: 0x0011D06F
		internal bool IsRS2000_WithNewChartYAxis
		{
			get
			{
				return this.m_version.CompareTo(8, 0, 713) >= 0;
			}
		}

		// Token: 0x17001CD0 RID: 7376
		// (get) Token: 0x0600449F RID: 17567 RVA: 0x0011EE89 File Offset: 0x0011D089
		internal bool IsRS2000_WithOtherPageChunkSplit
		{
			get
			{
				return this.m_version.CompareTo(8, 0, 716) >= 0;
			}
		}

		// Token: 0x17001CD1 RID: 7377
		// (get) Token: 0x060044A0 RID: 17568 RVA: 0x0011EEA3 File Offset: 0x0011D0A3
		internal bool IsRS2000_RTM_orOlder
		{
			get
			{
				return this.m_version.CompareTo(8, 0, 743) <= 0;
			}
		}

		// Token: 0x17001CD2 RID: 7378
		// (get) Token: 0x060044A1 RID: 17569 RVA: 0x0011EEBD File Offset: 0x0011D0BD
		internal bool IsRS2000_RTM_orNewer
		{
			get
			{
				return this.m_version.CompareTo(8, 0, 743) >= 0;
			}
		}

		// Token: 0x17001CD3 RID: 7379
		// (get) Token: 0x060044A2 RID: 17570 RVA: 0x0011EED7 File Offset: 0x0011D0D7
		internal bool IsRS2000_WithUnusedFieldsOptimization
		{
			get
			{
				return this.m_version.CompareTo(8, 0, 801) >= 0;
			}
		}

		// Token: 0x17001CD4 RID: 7380
		// (get) Token: 0x060044A3 RID: 17571 RVA: 0x0011EEF1 File Offset: 0x0011D0F1
		internal bool IsRS2000_WithImageInfo
		{
			get
			{
				return this.m_version.CompareTo(8, 0, 843) >= 0;
			}
		}

		// Token: 0x17001CD5 RID: 7381
		// (get) Token: 0x060044A4 RID: 17572 RVA: 0x0011EF0B File Offset: 0x0011D10B
		internal bool IsRS2005_Beta2_orOlder
		{
			get
			{
				return this.m_version.CompareTo(9, 0, 852) <= 0;
			}
		}

		// Token: 0x17001CD6 RID: 7382
		// (get) Token: 0x060044A5 RID: 17573 RVA: 0x0011EF26 File Offset: 0x0011D126
		internal bool IsRS2005_WithMultipleActions
		{
			get
			{
				return this.m_version.CompareTo(9, 0, 937) >= 0;
			}
		}

		// Token: 0x17001CD7 RID: 7383
		// (get) Token: 0x060044A6 RID: 17574 RVA: 0x0011EF41 File Offset: 0x0011D141
		internal bool IsRS2005_WithSpecialChunkSplit
		{
			get
			{
				return this.m_version.CompareTo(9, 0, 937) >= 0;
			}
		}

		// Token: 0x17001CD8 RID: 7384
		// (get) Token: 0x060044A7 RID: 17575 RVA: 0x0011EF5C File Offset: 0x0011D15C
		internal bool IsRS2005_IDW9_orOlder
		{
			get
			{
				return this.m_version.CompareTo(9, 0, 951) <= 0;
			}
		}

		// Token: 0x17001CD9 RID: 7385
		// (get) Token: 0x060044A8 RID: 17576 RVA: 0x0011EF77 File Offset: 0x0011D177
		internal bool IsRS2005_WithTableDetailFix
		{
			get
			{
				return this.m_version.CompareTo(10, 2, 0) >= 0;
			}
		}

		// Token: 0x17001CDA RID: 7386
		// (get) Token: 0x060044A9 RID: 17577 RVA: 0x0011EF8E File Offset: 0x0011D18E
		internal bool IsRS2005_WithPHFChunks
		{
			get
			{
				return this.m_version.CompareTo(10, 3, 0) >= 0;
			}
		}

		// Token: 0x17001CDB RID: 7387
		// (get) Token: 0x060044AA RID: 17578 RVA: 0x0011EFA5 File Offset: 0x0011D1A5
		internal bool IsRS2005_WithTableOptimizations
		{
			get
			{
				return this.m_version.CompareTo(10, 4, 0) >= 0;
			}
		}

		// Token: 0x17001CDC RID: 7388
		// (get) Token: 0x060044AB RID: 17579 RVA: 0x0011EFBC File Offset: 0x0011D1BC
		internal bool IsRS2005_WithSharedDrillthroughParams
		{
			get
			{
				return this.m_version.CompareTo(10, 8, 0) >= 0;
			}
		}

		// Token: 0x17001CDD RID: 7389
		// (get) Token: 0x060044AC RID: 17580 RVA: 0x0011EFD3 File Offset: 0x0011D1D3
		internal bool IsRS2005_WithSimpleTextBoxOptimizations
		{
			get
			{
				return this.m_version.CompareTo(10, 5, 0) >= 0;
			}
		}

		// Token: 0x17001CDE RID: 7390
		// (get) Token: 0x060044AD RID: 17581 RVA: 0x0011EFEA File Offset: 0x0011D1EA
		internal bool IsRS2005_WithChartHeadingInstanceFix
		{
			get
			{
				return this.m_version.CompareTo(10, 6, 0) >= 0;
			}
		}

		// Token: 0x17001CDF RID: 7391
		// (get) Token: 0x060044AE RID: 17582 RVA: 0x0011F001 File Offset: 0x0011D201
		internal bool IsRS2005_WithXmlDataElementOutputChange
		{
			get
			{
				return this.m_version.CompareTo(10, 7, 0) >= 0;
			}
		}

		// Token: 0x17001CE0 RID: 7392
		// (get) Token: 0x060044AF RID: 17583 RVA: 0x0011F018 File Offset: 0x0011D218
		internal bool Is_WithUserSort
		{
			get
			{
				return this.m_version.CompareTo(9, 0, 970) >= 0;
			}
		}

		// Token: 0x060044B0 RID: 17584 RVA: 0x0011F033 File Offset: 0x0011D233
		public override string ToString()
		{
			return this.m_version.ToString();
		}

		// Token: 0x04001F16 RID: 7958
		private IntermediateFormatVersion m_version;
	}
}
