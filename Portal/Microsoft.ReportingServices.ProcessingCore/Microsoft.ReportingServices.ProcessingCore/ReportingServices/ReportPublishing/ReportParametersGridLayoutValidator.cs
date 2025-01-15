using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x02000396 RID: 918
	internal sealed class ReportParametersGridLayoutValidator
	{
		// Token: 0x06002579 RID: 9593 RVA: 0x000B3E58 File Offset: 0x000B2058
		private ReportParametersGridLayoutValidator(List<Microsoft.ReportingServices.ReportIntermediateFormat.ParameterDef> parameters, ParametersGridLayout paramsLayout, PublishingErrorContext errorContext)
		{
			RSTrace.ProcessingTracer.Assert(paramsLayout != null, "paramsLayout should not be null");
			this.m_paramsLayout = paramsLayout;
			this.m_parameters = parameters;
			this.m_errorContext = errorContext;
			this.InitParameterNames();
		}

		// Token: 0x170013B4 RID: 5044
		// (get) Token: 0x0600257A RID: 9594 RVA: 0x000B3ED1 File Offset: 0x000B20D1
		private int NumberOfRows
		{
			get
			{
				return this.m_paramsLayout.NumberOfRows;
			}
		}

		// Token: 0x170013B5 RID: 5045
		// (get) Token: 0x0600257B RID: 9595 RVA: 0x000B3EDE File Offset: 0x000B20DE
		private int NumberOfColumns
		{
			get
			{
				return this.m_paramsLayout.NumberOfColumns;
			}
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x000B3EEB File Offset: 0x000B20EB
		public static bool Validate(List<Microsoft.ReportingServices.ReportIntermediateFormat.ParameterDef> parameters, ParametersGridLayout paramsLayout, PublishingErrorContext errorContext)
		{
			return new ReportParametersGridLayoutValidator(parameters, paramsLayout, errorContext).Validate();
		}

		// Token: 0x0600257D RID: 9597 RVA: 0x000B3EFC File Offset: 0x000B20FC
		private bool Validate()
		{
			if (this.m_parameterNames.Count == 0)
			{
				return this.ValidateNumberOfRows() && this.ValidateNumberOfColumns() && this.ValidateGridCells();
			}
			return this.ValidateNumberOfRows() && this.ValidateNumberOfColumns() && this.ValidateParametersCount() && this.ValidateGridCells() && this.ValidateConsecutiveEmptyRowCount();
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x000B3F58 File Offset: 0x000B2158
		private void InitParameterNames()
		{
			foreach (Microsoft.ReportingServices.ReportIntermediateFormat.ParameterDef parameterDef in this.m_parameters)
			{
				this.m_parameterNames.Add(parameterDef.Name);
			}
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x000B3FB8 File Offset: 0x000B21B8
		private static bool IsParamVisible(Microsoft.ReportingServices.ReportIntermediateFormat.ParameterDef param)
		{
			return param.Prompt.Length > 0;
		}

		// Token: 0x06002580 RID: 9600 RVA: 0x000B3FC8 File Offset: 0x000B21C8
		private bool ValidateNumberOfRows()
		{
			return this.ValidatePrerequisite(this.NumberOfRows > 0, ProcessingErrorCode.rsInvalidParameterLayoutZeroOrLessRowOrCol) && this.ValidatePrerequisite(this.NumberOfRows <= 10000, ProcessingErrorCode.rsInvalidParameterLayoutNumberOfRowsOrColumnsExceedingLimit, "", "NumberOfRows", new string[] { 10000.ToString() });
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x000B4028 File Offset: 0x000B2228
		private bool ValidateNumberOfColumns()
		{
			return this.ValidatePrerequisite(this.NumberOfColumns > 0, ProcessingErrorCode.rsInvalidParameterLayoutZeroOrLessRowOrCol) && this.ValidatePrerequisite(this.NumberOfColumns <= 8, ProcessingErrorCode.rsInvalidParameterLayoutNumberOfRowsOrColumnsExceedingLimit, "", "NumberOfColumns", new string[] { 8.ToString() });
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x000B4080 File Offset: 0x000B2280
		private bool ValidateParametersCount()
		{
			bool flag = this.ValidatePrerequisite(this.m_parameters.Count <= this.NumberOfRows * this.NumberOfColumns, ProcessingErrorCode.rsInvalidParameterLayoutParametersMissingFromPanel);
			if (this.m_paramsLayout.CellDefinitions != null)
			{
				flag &= this.ValidatePrerequisite(this.m_paramsLayout.CellDefinitions.Count == this.m_parameters.Count, ProcessingErrorCode.rsInvalidParameterLayoutCellDefNotEqualsParameterCount);
			}
			else
			{
				flag &= this.ValidatePrerequisite(this.m_parameters.Count == 0, ProcessingErrorCode.rsInvalidParameterLayoutCellDefNotEqualsParameterCount);
			}
			return flag;
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x000B4110 File Offset: 0x000B2310
		private bool ValidateGridCells()
		{
			if (this.m_parameterNames.Count == 0 && this.m_paramsLayout.CellDefinitions == null)
			{
				return true;
			}
			foreach (object obj in this.m_paramsLayout.CellDefinitions)
			{
				ParameterGridLayoutCellDefinition parameterGridLayoutCellDefinition = (ParameterGridLayoutCellDefinition)obj;
				if (!this.ValidateGridCell(parameterGridLayoutCellDefinition))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x000B4194 File Offset: 0x000B2394
		private bool ValidateGridCell(ParameterGridLayoutCellDefinition cell)
		{
			bool flag = true;
			string parameterName = cell.ParameterName;
			long num = (long)(cell.RowIndex * this.NumberOfColumns + cell.ColumnIndex);
			bool flag2 = flag & this.ValidatePrerequisite(!string.IsNullOrEmpty(parameterName), ProcessingErrorCode.rsInvalidParameterLayoutParameterNameMissing) & this.ValidatePrerequisite(!this.m_gridParameterNames.Contains(parameterName), ProcessingErrorCode.rsInvalidParameterLayoutParameterAppearsTwice, cell.ParameterName) & this.ValidatePrerequisite(this.m_parameterNames.Contains(parameterName), ProcessingErrorCode.rsInvalidParameterLayoutParameterNotVisible, parameterName) & this.ValidatePrerequisite(cell.RowIndex >= 0 && cell.RowIndex < this.NumberOfRows, ProcessingErrorCode.rsInvalidParameterLayoutRowColOutOfBounds) & this.ValidatePrerequisite(cell.ColumnIndex >= 0 && cell.ColumnIndex < this.NumberOfColumns, ProcessingErrorCode.rsInvalidParameterLayoutRowColOutOfBounds) & this.ValidatePrerequisite(!this.m_cellAddresses.Contains(num), ProcessingErrorCode.rsInvalidParameterLayoutCellCollition);
			if (flag2)
			{
				this.m_parameterRowIndexes[cell.RowIndex] = true;
				this.m_gridParameterNames.Add(parameterName);
				this.m_cellAddresses.Add(num);
			}
			return flag2;
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x000B42A4 File Offset: 0x000B24A4
		private bool ValidateConsecutiveEmptyRowCount()
		{
			return this.ValidatePrerequisite(!this.DoesNumberOfConsecutiveEmptyRowsExceedLimit(), ProcessingErrorCode.rsInvalidParameterLayoutNumberOfConsecutiveEmptyRowsExceedingLimit, "", "", new string[] { 20.ToString() });
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x000B42E2 File Offset: 0x000B24E2
		private bool ValidatePrerequisite(bool condition, ProcessingErrorCode errorCode)
		{
			return this.ValidatePrerequisite(condition, errorCode, "", "", this.NoArguments);
		}

		// Token: 0x06002587 RID: 9607 RVA: 0x000B42FC File Offset: 0x000B24FC
		private bool ValidatePrerequisite(bool condition, ProcessingErrorCode errorCode, string objectName)
		{
			return this.ValidatePrerequisite(condition, errorCode, objectName, "", this.NoArguments);
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x000B4312 File Offset: 0x000B2512
		private bool ValidatePrerequisite(bool condition, ProcessingErrorCode errorCode, string objectName, string propertyName, params string[] arguments)
		{
			if (!condition)
			{
				this.m_errorContext.Register(errorCode, Severity.Error, ObjectType.ParameterLayout, objectName, propertyName, arguments);
			}
			return condition;
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x000B4330 File Offset: 0x000B2530
		private bool DoesNumberOfConsecutiveEmptyRowsExceedLimit()
		{
			int num = 0;
			foreach (int num2 in this.m_parameterRowIndexes.Keys)
			{
				if (num2 - num - 1 > 20)
				{
					return true;
				}
				num = num2;
			}
			return this.NumberOfRows - 1 - num - 1 > 20;
		}

		// Token: 0x040015DD RID: 5597
		private const int MaxNumberOfRows = 10000;

		// Token: 0x040015DE RID: 5598
		private const int MaxNumberOfColumns = 8;

		// Token: 0x040015DF RID: 5599
		private const int MaxNumberOfConsecutiveEmptyRows = 20;

		// Token: 0x040015E0 RID: 5600
		private readonly string[] NoArguments = new string[0];

		// Token: 0x040015E1 RID: 5601
		private const string NoObjectName = "";

		// Token: 0x040015E2 RID: 5602
		private const string NoPropertyName = "";

		// Token: 0x040015E3 RID: 5603
		private List<Microsoft.ReportingServices.ReportIntermediateFormat.ParameterDef> m_parameters;

		// Token: 0x040015E4 RID: 5604
		private ParametersGridLayout m_paramsLayout;

		// Token: 0x040015E5 RID: 5605
		private PublishingErrorContext m_errorContext;

		// Token: 0x040015E6 RID: 5606
		private readonly HashSet<string> m_parameterNames = new HashSet<string>();

		// Token: 0x040015E7 RID: 5607
		private readonly HashSet<string> m_gridParameterNames = new HashSet<string>();

		// Token: 0x040015E8 RID: 5608
		private readonly HashSet<long> m_cellAddresses = new HashSet<long>();

		// Token: 0x040015E9 RID: 5609
		private readonly SortedList<int, bool> m_parameterRowIndexes = new SortedList<int, bool>();
	}
}
