using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.DataExtensions
{
	// Token: 0x020005BB RID: 1467
	internal sealed class MappingDataReader : IDisposable
	{
		// Token: 0x06005314 RID: 21268 RVA: 0x0015D710 File Offset: 0x0015B910
		public MappingDataReader(string dataSetName, IDataReader sourceReader, string[] aliases, string[] fieldNames, DataSourceErrorInspector errorInspector)
		{
			if (sourceReader == null)
			{
				if (Global.Tracer.TraceError)
				{
					Global.Tracer.Trace(TraceLevel.Error, "The source data reader is null. Cannot read results.");
				}
				throw new ReportProcessingException(ErrorCode.rsErrorCreatingDataReader, new object[] { dataSetName });
			}
			Global.Tracer.Assert(aliases != null, "(null != aliases)");
			this.m_dataSetName = dataSetName;
			this.m_dataReader = sourceReader;
			this.m_fieldNames = fieldNames;
			this.m_errorInspector = errorInspector;
			this.m_dataReaderExtension = sourceReader as IDataReaderExtension;
			this.m_dataFieldProperties = sourceReader as IDataReaderFieldProperties;
			if (fieldNames == null)
			{
				if (Global.Tracer.TraceInfo)
				{
					Global.Tracer.Trace(TraceLevel.Info, "The array of field names is null. Aliases will map positionally to the data reader fields.");
				}
				for (int i = 0; i < aliases.Length; i++)
				{
					this.m_aliasIndexToFieldIndex[i] = i;
				}
				return;
			}
			Global.Tracer.Assert(aliases.Length == fieldNames.Length, " (aliases.Length == fieldNames.Length)");
			this.m_aliasIndexToFieldIndex = new int[aliases.Length];
			if (fieldNames == null)
			{
				if (Global.Tracer.TraceWarning)
				{
					Global.Tracer.Trace(TraceLevel.Warning, "The data reader does not have any fields.");
				}
				for (int j = 0; j < aliases.Length; j++)
				{
					this.m_aliasIndexToFieldIndex[j] = -1;
				}
				return;
			}
			for (int k = 0; k < aliases.Length; k++)
			{
				string text = fieldNames[k];
				if (text != null)
				{
					int num;
					try
					{
						num = this.m_dataReader.GetOrdinal(text);
					}
					catch (RSException)
					{
						throw;
					}
					catch (Exception ex)
					{
						if (AsynchronousExceptionDetection.IsStoppingException(ex))
						{
							throw;
						}
						Global.Tracer.Trace(TraceLevel.Warning, "An exception occurred while trying to map a data set field.  Field: '{0}' DataField: '{1}' Details: {2}", new object[]
						{
							aliases[k],
							fieldNames[k],
							ex.Message
						});
						num = -1;
					}
					this.m_aliasIndexToFieldIndex[k] = num;
				}
				else
				{
					this.m_aliasIndexToFieldIndex[k] = -1;
					this.m_fieldNames[k] = aliases[k];
				}
			}
			if (Global.Tracer.TraceVerbose)
			{
				Global.Tracer.Trace(TraceLevel.Verbose, "Mapping data reader successfully initialized.");
			}
		}

		// Token: 0x17001EDF RID: 7903
		// (get) Token: 0x06005315 RID: 21269 RVA: 0x0015D8FC File Offset: 0x0015BAFC
		public string[] FieldNames
		{
			get
			{
				return this.m_fieldNames;
			}
		}

		// Token: 0x06005316 RID: 21270 RVA: 0x0015D904 File Offset: 0x0015BB04
		public bool GetNextRow()
		{
			bool flag;
			try
			{
				flag = this.m_dataReader.Read();
			}
			catch (Exception ex)
			{
				ErrorCode errorCode;
				if (this.m_errorInspector != null && this.m_errorInspector.TryInterpretProviderErrorCode(ex, out errorCode))
				{
					string text = string.Format(CultureInfo.CurrentCulture, RPRes.rsErrorReadingNextDataRow, this.m_dataSetName);
					throw new ReportProcessingQueryException(errorCode, ex, new object[] { text });
				}
				throw new ReportProcessingException(ErrorCode.rsErrorReadingNextDataRow, ex, new object[] { this.m_dataSetName });
			}
			return flag;
		}

		// Token: 0x06005317 RID: 21271 RVA: 0x0015D98C File Offset: 0x0015BB8C
		private void GenerateFieldErrorException(Exception e)
		{
			if (e == null)
			{
				throw new ReportProcessingException_FieldError(DataFieldStatus.IsError, null);
			}
			if (e is ReportProcessingException)
			{
				throw new ReportProcessingException_FieldError(DataFieldStatus.IsMissing, e.Message);
			}
			if (e is OverflowException)
			{
				throw new ReportProcessingException_FieldError(DataFieldStatus.Overflow, e.ToString());
			}
			if (e is NotSupportedException)
			{
				throw new ReportProcessingException_FieldError(DataFieldStatus.UnSupportedDataType, null);
			}
			throw new ReportProcessingException_FieldError(DataFieldStatus.IsError, e.ToString());
		}

		// Token: 0x06005318 RID: 21272 RVA: 0x0015D9EC File Offset: 0x0015BBEC
		public object GetFieldValue(int aliasIndex)
		{
			try
			{
				return this.m_dataReader.GetValue(this.GetFieldIndex(aliasIndex));
			}
			catch (Exception ex)
			{
				this.GenerateFieldErrorException(ex);
			}
			return null;
		}

		// Token: 0x06005319 RID: 21273 RVA: 0x0015DA2C File Offset: 0x0015BC2C
		public bool IsAggregationField(int aliasIndex)
		{
			bool flag;
			try
			{
				Global.Tracer.Assert(this.m_dataReaderExtension != null, "(null != m_dataReaderExtension)");
				flag = this.m_dataReaderExtension.IsAggregationField(this.GetFieldIndex(aliasIndex));
			}
			catch (Exception ex)
			{
				throw new ReportProcessingException(ErrorCode.rsErrorReadingDataAggregationField, ex, new object[] { this.m_dataSetName });
			}
			return flag;
		}

		// Token: 0x17001EE0 RID: 7904
		// (get) Token: 0x0600531A RID: 21274 RVA: 0x0015DA94 File Offset: 0x0015BC94
		public bool ReaderExtensionsSupported
		{
			get
			{
				return this.m_dataReaderExtension != null;
			}
		}

		// Token: 0x17001EE1 RID: 7905
		// (get) Token: 0x0600531B RID: 21275 RVA: 0x0015DA9F File Offset: 0x0015BC9F
		public bool IsAggregateRow
		{
			get
			{
				return this.m_dataReaderExtension.IsAggregateRow;
			}
		}

		// Token: 0x17001EE2 RID: 7906
		// (get) Token: 0x0600531C RID: 21276 RVA: 0x0015DAAC File Offset: 0x0015BCAC
		public int AggregationFieldCount
		{
			get
			{
				return this.m_dataReaderExtension.AggregationFieldCount;
			}
		}

		// Token: 0x17001EE3 RID: 7907
		// (get) Token: 0x0600531D RID: 21277 RVA: 0x0015DAB9 File Offset: 0x0015BCB9
		public bool ReaderFieldProperties
		{
			get
			{
				return this.m_dataFieldProperties != null;
			}
		}

		// Token: 0x0600531E RID: 21278 RVA: 0x0015DAC4 File Offset: 0x0015BCC4
		public int GetPropertyCount(int aliasIndex)
		{
			try
			{
				return this.m_dataFieldProperties.GetPropertyCount(this.GetFieldIndex(aliasIndex));
			}
			catch (Exception ex)
			{
				this.GenerateFieldErrorException(ex);
			}
			return 0;
		}

		// Token: 0x0600531F RID: 21279 RVA: 0x0015DB04 File Offset: 0x0015BD04
		public string GetPropertyName(int aliasIndex, int propertyIndex)
		{
			try
			{
				return this.m_dataFieldProperties.GetPropertyName(this.GetFieldIndex(aliasIndex), propertyIndex);
			}
			catch (Exception ex)
			{
				this.GenerateFieldErrorException(ex);
			}
			return null;
		}

		// Token: 0x06005320 RID: 21280 RVA: 0x0015DB44 File Offset: 0x0015BD44
		public object GetPropertyValue(int aliasIndex, int propertyIndex)
		{
			try
			{
				return this.m_dataFieldProperties.GetPropertyValue(this.GetFieldIndex(aliasIndex), propertyIndex);
			}
			catch (Exception ex)
			{
				this.GenerateFieldErrorException(ex);
			}
			return null;
		}

		// Token: 0x06005321 RID: 21281 RVA: 0x0015DB84 File Offset: 0x0015BD84
		void IDisposable.Dispose()
		{
			if (Global.Tracer.TraceVerbose)
			{
				Global.Tracer.Trace(TraceLevel.Verbose, "Closing the data reader in Dispose().");
			}
			if (this.m_dataReader != null)
			{
				this.m_dataReader.Dispose();
			}
			this.m_dataReader = null;
			this.m_aliasIndexToFieldIndex = null;
		}

		// Token: 0x06005322 RID: 21282 RVA: 0x0015DBC4 File Offset: 0x0015BDC4
		private int GetFieldIndex(int aliasIndex)
		{
			int num = -1;
			try
			{
				num = this.m_aliasIndexToFieldIndex[aliasIndex];
			}
			catch (IndexOutOfRangeException)
			{
			}
			if (num < 0)
			{
				throw new ReportProcessingException(ErrorCode.rsNoFieldDataAtIndex, new object[] { aliasIndex + 1 });
			}
			return num;
		}

		// Token: 0x040029DD RID: 10717
		private string m_dataSetName;

		// Token: 0x040029DE RID: 10718
		private IDataReader m_dataReader;

		// Token: 0x040029DF RID: 10719
		private IDataReaderExtension m_dataReaderExtension;

		// Token: 0x040029E0 RID: 10720
		private IDataReaderFieldProperties m_dataFieldProperties;

		// Token: 0x040029E1 RID: 10721
		private int[] m_aliasIndexToFieldIndex;

		// Token: 0x040029E2 RID: 10722
		private string[] m_fieldNames;

		// Token: 0x040029E3 RID: 10723
		private readonly DataSourceErrorInspector m_errorInspector;
	}
}
