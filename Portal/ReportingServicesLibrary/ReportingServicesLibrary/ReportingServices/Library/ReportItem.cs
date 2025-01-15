using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200027F RID: 639
	internal class ReportItem
	{
		// Token: 0x060016CA RID: 5834 RVA: 0x0005CDE8 File Offset: 0x0005AFE8
		internal ReportItem(ExternalItemPath itemPath)
		{
			this.m_creationDate = DateTime.MinValue;
			this.m_itemPath = itemPath;
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x060016CB RID: 5835 RVA: 0x0005CE0D File Offset: 0x0005B00D
		// (set) Token: 0x060016CC RID: 5836 RVA: 0x0005CE15 File Offset: 0x0005B015
		internal string UserParams
		{
			get
			{
				return this.m_userParams;
			}
			set
			{
				this.m_userParams = value;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x0005CE1E File Offset: 0x0005B01E
		// (set) Token: 0x060016CE RID: 5838 RVA: 0x0005CE26 File Offset: 0x0005B026
		internal ParameterInfoCollection ParametersOnSnapshot
		{
			get
			{
				return this.m_parametersOnSnapshot;
			}
			set
			{
				this.m_parametersOnSnapshot = value;
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x0005CE2F File Offset: 0x0005B02F
		// (set) Token: 0x060016D0 RID: 5840 RVA: 0x0005CE37 File Offset: 0x0005B037
		internal ParameterInfoCollection EffectiveParams
		{
			get
			{
				return this.m_effectiveParams;
			}
			set
			{
				this.m_effectiveParams = value;
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x060016D1 RID: 5841 RVA: 0x0005CE40 File Offset: 0x0005B040
		// (set) Token: 0x060016D2 RID: 5842 RVA: 0x0005CE48 File Offset: 0x0005B048
		internal ParametersGridLayout ParametersLayout
		{
			get
			{
				return this.m_parametersLayout;
			}
			set
			{
				this.m_parametersLayout = value;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x060016D3 RID: 5843 RVA: 0x0005CE51 File Offset: 0x0005B051
		internal string EffectiveParamsXml
		{
			get
			{
				if (this.m_effectiveParams == null)
				{
					return null;
				}
				return this.m_effectiveParams.ToXml(false);
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x060016D4 RID: 5844 RVA: 0x0005CE69 File Offset: 0x0005B069
		// (set) Token: 0x060016D5 RID: 5845 RVA: 0x0005CE71 File Offset: 0x0005B071
		internal string Description
		{
			get
			{
				return this.m_description;
			}
			set
			{
				this.m_description = value;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x060016D6 RID: 5846 RVA: 0x0005CE7A File Offset: 0x0005B07A
		// (set) Token: 0x060016D7 RID: 5847 RVA: 0x0005CE82 File Offset: 0x0005B082
		internal DateTime CreationDate
		{
			get
			{
				return this.m_creationDate;
			}
			set
			{
				this.m_creationDate = value;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x060016D8 RID: 5848 RVA: 0x0005CE8B File Offset: 0x0005B08B
		// (set) Token: 0x060016D9 RID: 5849 RVA: 0x0005CE93 File Offset: 0x0005B093
		internal ReportSnapshot SnapshotData
		{
			get
			{
				return this.m_snapshotData;
			}
			set
			{
				this.m_snapshotData = value;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x060016DA RID: 5850 RVA: 0x0005CE9C File Offset: 0x0005B09C
		// (set) Token: 0x060016DB RID: 5851 RVA: 0x0005CEA4 File Offset: 0x0005B0A4
		internal DateTime HistoryDate
		{
			get
			{
				return this.m_historyDate;
			}
			set
			{
				this.m_historyDate = value;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x060016DC RID: 5852 RVA: 0x0005CEAD File Offset: 0x0005B0AD
		// (set) Token: 0x060016DD RID: 5853 RVA: 0x0005CEB5 File Offset: 0x0005B0B5
		internal ReportSnapshot CompiledDefinition
		{
			get
			{
				return this.m_compiledDefinition;
			}
			set
			{
				this.m_compiledDefinition = value;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x060016DE RID: 5854 RVA: 0x0005CEBE File Offset: 0x0005B0BE
		internal ExternalItemPath ItemPath
		{
			get
			{
				return this.m_itemPath;
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x060016DF RID: 5855 RVA: 0x0005CEC6 File Offset: 0x0005B0C6
		// (set) Token: 0x060016E0 RID: 5856 RVA: 0x0005CEDD File Offset: 0x0005B0DD
		internal ExternalItemPath ReportDefinitionPath
		{
			[DebuggerStepThrough]
			get
			{
				if (this.m_reportDefinitionPath == null)
				{
					return this.m_itemPath;
				}
				return this.m_reportDefinitionPath;
			}
			set
			{
				this.m_reportDefinitionPath = value;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x060016E1 RID: 5857 RVA: 0x0005CEE6 File Offset: 0x0005B0E6
		internal bool SnapshotParametersHaveChanged
		{
			get
			{
				if (!this.m_parameterChangeChecked)
				{
					this.CheckIfParametersChanged();
				}
				return this.m_snapshotParametersHaveChanged;
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x060016E2 RID: 5858 RVA: 0x0005CEFC File Offset: 0x0005B0FC
		internal bool QueryParametersHaveChanged
		{
			get
			{
				if (!this.m_parameterChangeChecked)
				{
					this.CheckIfParametersChanged();
				}
				return this.m_queryParametersHaveChanged;
			}
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x0005CF14 File Offset: 0x0005B114
		private void CheckIfParametersChanged()
		{
			if (this.m_snapshotData != null)
			{
				bool flag;
				bool flag2;
				this.m_parametersOnSnapshot.SameParameters(this.m_effectiveParams, out flag, out flag2);
				this.m_snapshotParametersHaveChanged = !flag2;
				this.m_queryParametersHaveChanged = !flag;
			}
			else
			{
				this.m_snapshotParametersHaveChanged = false;
				this.m_queryParametersHaveChanged = false;
			}
			this.m_parameterChangeChecked = true;
		}

		// Token: 0x04000844 RID: 2116
		private ReportSnapshot m_snapshotData;

		// Token: 0x04000845 RID: 2117
		private ReportSnapshot m_compiledDefinition;

		// Token: 0x04000846 RID: 2118
		private string m_userParams;

		// Token: 0x04000847 RID: 2119
		private ParameterInfoCollection m_effectiveParams;

		// Token: 0x04000848 RID: 2120
		private ParameterInfoCollection m_parametersOnSnapshot;

		// Token: 0x04000849 RID: 2121
		private ParametersGridLayout m_parametersLayout;

		// Token: 0x0400084A RID: 2122
		private string m_description;

		// Token: 0x0400084B RID: 2123
		private DateTime m_creationDate;

		// Token: 0x0400084C RID: 2124
		private DateTime m_historyDate = DateTime.MinValue;

		// Token: 0x0400084D RID: 2125
		private ExternalItemPath m_itemPath;

		// Token: 0x0400084E RID: 2126
		private ExternalItemPath m_reportDefinitionPath;

		// Token: 0x0400084F RID: 2127
		private bool m_parameterChangeChecked;

		// Token: 0x04000850 RID: 2128
		private bool m_snapshotParametersHaveChanged;

		// Token: 0x04000851 RID: 2129
		private bool m_queryParametersHaveChanged;
	}
}
