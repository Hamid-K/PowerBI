using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel.RdlUpgrade;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel2010.Upgrade
{
	// Token: 0x0200006C RID: 108
	internal class UpgradeImpl2010 : UpgraderBase
	{
		// Token: 0x060003F4 RID: 1012 RVA: 0x0001650D File Offset: 0x0001470D
		internal UpgradeImpl2010()
		{
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00016515 File Offset: 0x00014715
		internal override Type GetReportType()
		{
			return typeof(Report2010);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00016521 File Offset: 0x00014721
		protected override void InitUpgrade()
		{
			this.m_upgradeable = new List<IUpgradeable2010>();
			base.InitUpgrade();
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00016534 File Offset: 0x00014734
		protected override void Upgrade(Report report)
		{
			foreach (IUpgradeable2010 upgradeable in this.m_upgradeable)
			{
				upgradeable.Upgrade(this);
			}
			base.Upgrade(report);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0001658C File Offset: 0x0001478C
		protected override RdlSerializerSettings CreateReaderSettings()
		{
			return UpgradeSerializerSettings2010.CreateReaderSettings();
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00016593 File Offset: 0x00014793
		protected override RdlSerializerSettings CreateWriterSettings()
		{
			return UpgradeSerializerSettings2010.CreateWriterSettings();
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0001659A File Offset: 0x0001479A
		protected override void SetupReaderSettings(RdlSerializerSettings settings)
		{
			((SerializerHost2010)settings.Host).Upgradeable2010 = this.m_upgradeable;
			base.SetupReaderSettings(settings);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x000165BC File Offset: 0x000147BC
		internal void UpgradeReport(Report2010 report)
		{
			ReportParametersLayout reportParametersLayout = new ReportParametersLayout();
			GridLayoutDefinition gridLayoutDefinition = new GridLayoutDefinition();
			int num = 2;
			int num2 = 4;
			if (report.ReportParameters != null && report.ReportParameters.Count > 0)
			{
				IList<ReportParameter> reportParameters = report.ReportParameters;
				gridLayoutDefinition.CellDefinitions = this.CreatedArrangedCellDefinitions(reportParameters, out num, out num2);
				gridLayoutDefinition.NumberOfColumns = num2;
				gridLayoutDefinition.NumberOfRows = num;
				reportParametersLayout.GridLayoutDefinition = gridLayoutDefinition;
				report.ReportParametersLayout = reportParametersLayout;
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00016625 File Offset: 0x00014825
		private CellDefinition CreateCellDefinition(ReportParameter parameters, int rowIndex, int columnIndex)
		{
			return new CellDefinition
			{
				ParameterName = parameters.Name,
				ColumnIndex = columnIndex,
				RowIndex = rowIndex
			};
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00016648 File Offset: 0x00014848
		private IList<CellDefinition> CreatedArrangedCellDefinitions(IList<ReportParameter> parameters, out int numberOfRows, out int numberOfColumns)
		{
			int num = 0;
			int num2 = 0;
			numberOfRows = 1;
			numberOfColumns = 1;
			List<CellDefinition> list = new List<CellDefinition>();
			List<UpgradeImpl2010.CellInfo> list2 = new List<UpgradeImpl2010.CellInfo>();
			int num3 = 0;
			int num4 = 1;
			for (int i = 0; i < parameters.Count; i++)
			{
				ReportParameter reportParameter = parameters[i];
				bool flag = i < parameters.Count - 1 && !UpgradeImpl2010.IsParameterHidden(parameters[i + 1]);
				numberOfColumns = Math.Max(num2 + 1, numberOfColumns);
				numberOfRows = num + 1;
				bool flag2 = false;
				bool flag3 = false;
				if (!UpgradeImpl2010.IsParameterHidden(parameters[i]))
				{
					num3++;
					flag2 = num3 % 2 == 0;
					if (flag2)
					{
						num4 = Math.Max(num2, num4);
					}
					flag3 = flag2;
				}
				else if (flag)
				{
					flag3 = num3 % 2 == 0;
				}
				list2.Add(new UpgradeImpl2010.CellInfo
				{
					RowIndex = num,
					ColumnIndex = num2,
					Parameter = reportParameter,
					IsSecondVisibleColumn = flag2
				});
				if (num2 == 7 || flag3)
				{
					num++;
					num2 = 0;
				}
				else
				{
					num2++;
				}
			}
			foreach (UpgradeImpl2010.CellInfo cellInfo in list2)
			{
				CellDefinition cellDefinition = this.CreateCellDefinition(cellInfo.Parameter, cellInfo.RowIndex, cellInfo.IsSecondVisibleColumn ? num4 : cellInfo.ColumnIndex);
				list.Add(cellDefinition);
			}
			return list;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x000167BC File Offset: 0x000149BC
		private static bool IsParameterHidden(ReportParameter parameterNode)
		{
			return parameterNode.Hidden || parameterNode.Prompt == null || string.IsNullOrEmpty(parameterNode.Prompt.Value);
		}

		// Token: 0x04000104 RID: 260
		private List<IUpgradeable2010> m_upgradeable;

		// Token: 0x0200032C RID: 812
		private class CellInfo
		{
			// Token: 0x04000747 RID: 1863
			public int RowIndex;

			// Token: 0x04000748 RID: 1864
			public int ColumnIndex;

			// Token: 0x04000749 RID: 1865
			public ReportParameter Parameter;

			// Token: 0x0400074A RID: 1866
			public bool IsSecondVisibleColumn;
		}
	}
}
