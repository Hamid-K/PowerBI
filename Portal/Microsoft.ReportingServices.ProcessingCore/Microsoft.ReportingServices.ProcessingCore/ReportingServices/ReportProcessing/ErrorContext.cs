using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020005FD RID: 1533
	public abstract class ErrorContext
	{
		// Token: 0x17001F0D RID: 7949
		// (get) Token: 0x06005466 RID: 21606 RVA: 0x001624F8 File Offset: 0x001606F8
		// (set) Token: 0x06005467 RID: 21607 RVA: 0x00162500 File Offset: 0x00160700
		internal bool HasError
		{
			get
			{
				return this.m_hasError;
			}
			set
			{
				this.m_hasError = value;
			}
		}

		// Token: 0x17001F0E RID: 7950
		// (get) Token: 0x06005468 RID: 21608 RVA: 0x00162509 File Offset: 0x00160709
		// (set) Token: 0x06005469 RID: 21609 RVA: 0x00162511 File Offset: 0x00160711
		internal bool SuspendErrors
		{
			get
			{
				return this.m_suspendErrors;
			}
			set
			{
				this.m_suspendErrors = value;
			}
		}

		// Token: 0x17001F0F RID: 7951
		// (get) Token: 0x0600546A RID: 21610 RVA: 0x0016251A File Offset: 0x0016071A
		internal ProcessingMessageList Messages
		{
			get
			{
				return this.m_messages;
			}
		}

		// Token: 0x17001F10 RID: 7952
		// (get) Token: 0x0600546B RID: 21611 RVA: 0x00162522 File Offset: 0x00160722
		internal int MessageCount
		{
			get
			{
				if (this.m_messages != null)
				{
					return this.m_messages.Count;
				}
				return 0;
			}
		}

		// Token: 0x0600546C RID: 21612
		internal abstract ProcessingMessage Register(ProcessingErrorCode code, Severity severity, ObjectType objectType, string objectName, string propertyName, params string[] arguments);

		// Token: 0x0600546D RID: 21613
		internal abstract ProcessingMessage Register(ProcessingErrorCode code, Severity severity, ObjectType objectType, string objectName, string propertyName, ProcessingMessageList innerMessages, params string[] arguments);

		// Token: 0x0600546E RID: 21614
		internal abstract ProcessingMessage Register(string diagnosticDetails, ProcessingErrorCode code, Severity severity, ObjectType objectType, string objectName, string propertyName, ProcessingMessageList innerMessages, params string[] arguments);

		// Token: 0x0600546F RID: 21615 RVA: 0x0016253C File Offset: 0x0016073C
		internal virtual void Register(RSException rsException, ObjectType objectType)
		{
			if (this.m_suspendErrors)
			{
				return;
			}
			this.m_hasError = true;
			if (this.m_messages == null)
			{
				this.m_messages = new ProcessingMessageList();
			}
			ProcessingMessage processingMessage = this.CreateProcessingMessage(rsException.Code, objectType, rsException.Message);
			this.m_messages.Add(processingMessage);
			for (rsException = rsException.InnerException as RSException; rsException != null; rsException = rsException.InnerException as RSException)
			{
				ProcessingMessage processingMessage2 = this.CreateProcessingMessage(rsException.Code, objectType, rsException.Message);
				if (processingMessage.ProcessingMessages == null)
				{
					processingMessage.ProcessingMessages = new ProcessingMessageList(1);
				}
				processingMessage.ProcessingMessages.Add(processingMessage2);
				processingMessage = processingMessage2;
			}
		}

		// Token: 0x06005470 RID: 21616 RVA: 0x001625E4 File Offset: 0x001607E4
		internal static ProcessingMessage CreateProcessingMessage(ProcessingErrorCode code, Severity severity, ObjectType objectType, string objectName, string propertyName, string diagnosticDetails, ProcessingMessageList innerMessages, params string[] arguments)
		{
			object[] messageArgs = ErrorContext.GetMessageArgs(objectType, objectName, propertyName, arguments);
			string text = string.Format(CultureInfo.CurrentCulture, RPResWrapper.Keys.GetString(code.ToString()), messageArgs);
			return new ProcessingMessage(code, severity, objectType, objectName, propertyName, text, innerMessages, diagnosticDetails);
		}

		// Token: 0x06005471 RID: 21617 RVA: 0x0016262B File Offset: 0x0016082B
		protected ProcessingMessage CreateProcessingMessage(ErrorCode code, ObjectType objectType, string messageString)
		{
			return new ProcessingMessage(code, Severity.Error, objectType, null, null, messageString, null);
		}

		// Token: 0x06005472 RID: 21618 RVA: 0x0016263C File Offset: 0x0016083C
		private static object[] GetMessageArgs(ObjectType objectType, string objectName, string propertyName, params string[] arguments)
		{
			object[] array = new object[3 + ((arguments == null) ? 0 : arguments.Length)];
			array[0] = ErrorContext.GetLocalizedObjectTypeString(objectType);
			array[1] = ErrorContext.GetLocalizedObjectNameString(objectType, objectName);
			array[2] = ErrorContext.GetLocalizedPropertyNameString(propertyName);
			if (arguments != null)
			{
				for (int i = 0; i < arguments.Length; i++)
				{
					array[3 + i] = arguments[i];
				}
			}
			return array;
		}

		// Token: 0x06005473 RID: 21619 RVA: 0x00162690 File Offset: 0x00160890
		internal static string GetLocalizedObjectTypeString(ObjectType objectType)
		{
			switch (objectType)
			{
			case ObjectType.Report:
				return RPRes.rsObjectTypeReport;
			case ObjectType.PageHeader:
			case ObjectType.PageFooter:
				return RPRes.rsObjectTypePage;
			case ObjectType.Line:
				return RPRes.rsObjectTypeLine;
			case ObjectType.Rectangle:
				return RPRes.rsObjectTypeRectangle;
			case ObjectType.Checkbox:
				return RPRes.rsObjectTypeCheckbox;
			case ObjectType.Textbox:
				return RPRes.rsObjectTypeTextbox;
			case ObjectType.Image:
				return RPRes.rsObjectTypeImage;
			case ObjectType.Subreport:
				return RPRes.rsObjectTypeSubreport;
			case ObjectType.ActiveXControl:
				return RPRes.rsObjectTypeActiveXControl;
			case ObjectType.List:
				return RPRes.rsObjectTypeList;
			case ObjectType.Matrix:
				return RPRes.rsObjectTypeMatrix;
			case ObjectType.Table:
				return RPRes.rsObjectTypeTable;
			case ObjectType.OWCChart:
				return RPRes.rsObjectTypeOWCChart;
			case ObjectType.GaugePanel:
				return RPRes.rsObjectTypeGaugePanel;
			case ObjectType.GaugeCell:
				return RPRes.rsObjectTypeGaugeCell;
			case ObjectType.Chart:
				return RPRes.rsObjectTypeChart;
			case ObjectType.Grouping:
				return RPRes.rsObjectTypeGrouping;
			case ObjectType.ReportParameter:
				return RPRes.rsObjectTypeReportParameter;
			case ObjectType.DataSource:
				return RPRes.rsObjectTypeDataSource;
			case ObjectType.DataSet:
				return RPRes.rsObjectTypeDataSet;
			case ObjectType.Field:
				return RPRes.rsObjectTypeField;
			case ObjectType.Query:
				return RPRes.rsObjectTypeQuery;
			case ObjectType.QueryParameter:
				return RPRes.rsObjectTypeQueryParameter;
			case ObjectType.EmbeddedImage:
				return RPRes.rsObjectTypeEmbeddedImage;
			case ObjectType.ReportItem:
				return RPRes.rsObjectTypeReportItem;
			case ObjectType.Subtotal:
				return RPRes.rsObjectTypeSubtotal;
			case ObjectType.CodeClass:
				return RPRes.rsObjectTypeCodeClass;
			case ObjectType.CustomReportItem:
				return RPRes.rsObjectTypeCustomReportItem;
			case ObjectType.Tablix:
				return RPRes.rsObjectTypeTablix;
			case ObjectType.Page:
				return RPRes.rsObjectTypePage;
			case ObjectType.Paragraph:
				return RPRes.rsObjectTypeParagraph;
			case ObjectType.TextRun:
				return RPRes.rsObjectTypeTextRun;
			case ObjectType.ReportSection:
				return RPRes.rsObjectTypeReportSection;
			case ObjectType.Map:
				return RPRes.rsObjectTypeMap;
			case ObjectType.MapDataRegion:
				return RPRes.rsObjectTypeMapDataRegion;
			case ObjectType.MapCell:
				return RPRes.rsObjectTypeMapCell;
			case ObjectType.SharedDataSet:
				return RPRes.rsObjectTypeSharedDataSet;
			case ObjectType.TablixCell:
				return RPRes.rsObjectTypeTablixCell;
			case ObjectType.ChartDataPoint:
				return RPRes.rsObjectTypeChartDataPoint;
			case ObjectType.DataCell:
				return RPRes.rsObjectTypeDataCell;
			case ObjectType.DataShape:
				return RPRes.rsObjectTypeDataShape;
			case ObjectType.DataShapeMember:
				return RPRes.rsObjectTypeDataShapeMember;
			case ObjectType.DataShapeIntersection:
				return RPRes.rsObjectTypeDataShapeIntersection;
			case ObjectType.DataBinding:
				return RPRes.rsObjectTypeDataBinding;
			case ObjectType.Calculation:
				return RPRes.rsObjectTypeCalculation;
			case ObjectType.ParameterLayout:
				return RPRes.rsObjectTypeParameterLayout;
			}
			Global.Tracer.Assert(false);
			return null;
		}

		// Token: 0x06005474 RID: 21620 RVA: 0x0016288C File Offset: 0x00160A8C
		private static string GetLocalizedObjectNameString(ObjectType objectType, string objectName)
		{
			switch (objectType)
			{
			case ObjectType.Report:
				return RPRes.rsObjectNameBody;
			case ObjectType.PageHeader:
				return RPRes.rsObjectNameHeader;
			case ObjectType.PageFooter:
				return RPRes.rsObjectNameFooter;
			default:
				return objectName;
			}
		}

		// Token: 0x06005475 RID: 21621 RVA: 0x001628B5 File Offset: 0x00160AB5
		private static string GetLocalizedPropertyNameString(string propertyName)
		{
			return propertyName;
		}

		// Token: 0x04002CEF RID: 11503
		protected bool m_hasError;

		// Token: 0x04002CF0 RID: 11504
		protected bool m_suspendErrors;

		// Token: 0x04002CF1 RID: 11505
		protected ProcessingMessageList m_messages;
	}
}
