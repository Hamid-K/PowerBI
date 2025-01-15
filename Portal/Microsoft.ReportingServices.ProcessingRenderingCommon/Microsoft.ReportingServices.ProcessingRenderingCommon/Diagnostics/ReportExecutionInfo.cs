using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Diagnostics.Internal;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200007F RID: 127
	public sealed class ReportExecutionInfo
	{
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000B1D3 File Offset: 0x000093D3
		// (set) Token: 0x0600036E RID: 878 RVA: 0x0000B1DB File Offset: 0x000093DB
		public ExternalItemPath ItemPath
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_itemPath;
			}
			[DebuggerStepThrough]
			set
			{
				this.m_itemPath = value;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000B1E4 File Offset: 0x000093E4
		// (set) Token: 0x06000370 RID: 880 RVA: 0x0000B1EC File Offset: 0x000093EC
		public string Format
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_format;
			}
			[DebuggerStepThrough]
			set
			{
				this.m_format = value;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000B1F5 File Offset: 0x000093F5
		// (set) Token: 0x06000372 RID: 882 RVA: 0x0000B1FD File Offset: 0x000093FD
		public string Parameters
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_parameters;
			}
			[DebuggerStepThrough]
			set
			{
				this.m_parameters = value;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000B206 File Offset: 0x00009406
		// (set) Token: 0x06000374 RID: 884 RVA: 0x0000B20E File Offset: 0x0000940E
		public ExecutionLogExecType Source
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_source;
			}
			[DebuggerStepThrough]
			set
			{
				this.m_source = value;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000B217 File Offset: 0x00009417
		// (set) Token: 0x06000376 RID: 886 RVA: 0x0000B21F File Offset: 0x0000941F
		public ExecutionLogLevel ExecutionLogLevel
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_execLogLevel;
			}
			[DebuggerStepThrough]
			set
			{
				this.m_execLogLevel = value;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000B228 File Offset: 0x00009428
		// (set) Token: 0x06000378 RID: 888 RVA: 0x0000B230 File Offset: 0x00009430
		public ErrorCode Status
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_status;
			}
			[DebuggerStepThrough]
			set
			{
				this.m_status = value;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000B239 File Offset: 0x00009439
		// (set) Token: 0x0600037A RID: 890 RVA: 0x0000B241 File Offset: 0x00009441
		public long ByteCount
		{
			get
			{
				return this.m_byteCount;
			}
			set
			{
				this.m_byteCount = value;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000B24A File Offset: 0x0000944A
		// (set) Token: 0x0600037C RID: 892 RVA: 0x0000B252 File Offset: 0x00009452
		public long RowCount
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_rowCount;
			}
			[DebuggerStepThrough]
			set
			{
				this.m_rowCount = value;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000B25B File Offset: 0x0000945B
		// (set) Token: 0x0600037E RID: 894 RVA: 0x0000B263 File Offset: 0x00009463
		public TimeSpan ProcessingTime
		{
			get
			{
				return this.m_processingTime;
			}
			set
			{
				this.m_processingTime = value;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000B26C File Offset: 0x0000946C
		// (set) Token: 0x06000380 RID: 896 RVA: 0x0000B274 File Offset: 0x00009474
		public TimeSpan RenderingTime
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_renderingTime;
			}
			[DebuggerStepThrough]
			set
			{
				this.m_renderingTime = value;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000B27D File Offset: 0x0000947D
		// (set) Token: 0x06000382 RID: 898 RVA: 0x0000B285 File Offset: 0x00009485
		public TimeSpan DataRetrievalTime
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_dataRetrievalTime;
			}
			[DebuggerStepThrough]
			set
			{
				this.m_dataRetrievalTime = value;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000B28E File Offset: 0x0000948E
		// (set) Token: 0x06000384 RID: 900 RVA: 0x0000B296 File Offset: 0x00009496
		public string ExecutionId
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_executionId;
			}
			[DebuggerStepThrough]
			set
			{
				this.m_executionId = value;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000B29F File Offset: 0x0000949F
		// (set) Token: 0x06000386 RID: 902 RVA: 0x0000B2A7 File Offset: 0x000094A7
		public ReportEventType EventType
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_eventType;
			}
			[DebuggerStepThrough]
			set
			{
				this.m_eventType = value;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000B2B0 File Offset: 0x000094B0
		public AdditionalInfo AdditionalInfo
		{
			get
			{
				if (this.m_additionalInfo == null)
				{
					AdditionalInfo additionalInfo = new AdditionalInfo();
					Interlocked.CompareExchange<AdditionalInfo>(ref this.m_additionalInfo, additionalInfo, null);
				}
				return this.m_additionalInfo;
			}
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000B2E4 File Offset: 0x000094E4
		public void AggregateDataShapeMetrics()
		{
			if (this.m_additionalInfo == null || this.m_additionalInfo.DataShapes == null)
			{
				return;
			}
			long? num = null;
			long? num2 = null;
			long? num3 = null;
			long? num4 = null;
			long? num5 = null;
			long? num6 = null;
			long? num7 = null;
			long? num8 = null;
			long? num9 = null;
			long? num10 = null;
			long? num11 = null;
			foreach (DataShape dataShape in this.m_additionalInfo.DataShapes)
			{
				this.AggregateValue(dataShape.TimeQueryTranslation, ref num);
				this.AggregateValue(dataShape.TimeQueryTranslation, ref num2);
				this.AggregateValue(dataShape.TimeProcessing, ref num2);
				this.AggregateValue(dataShape.TimeRendering, ref num3);
				this.AggregateValue(dataShape.TimeDataRetrieval, ref num4);
				if (dataShape.EstimatedMemoryUsageKB != null)
				{
					this.AggregateValue(dataShape.EstimatedMemoryUsageKB.Pagination, ref num6);
					this.AggregateValue(dataShape.EstimatedMemoryUsageKB.Processing, ref num7);
					this.AggregateValue(dataShape.EstimatedMemoryUsageKB.Rendering, ref num8);
				}
				if (dataShape.ScalabilityTime != null)
				{
					this.AggregateValue(dataShape.ScalabilityTime.Pagination, ref num9);
					this.AggregateValue(dataShape.ScalabilityTime.Processing, ref num10);
					this.AggregateValue(dataShape.ScalabilityTime.Rendering, ref num11);
				}
				if (this.ExecutionLogLevel != ExecutionLogLevel.Verbose)
				{
					dataShape.EstimatedMemoryUsageKB = null;
					dataShape.ScalabilityTime = null;
				}
				if (dataShape.Connections != null)
				{
					foreach (Connection connection in dataShape.Connections)
					{
						if (connection.DataSets != null)
						{
							foreach (DataSet dataSet in connection.DataSets)
							{
								this.AggregateValue(dataSet.RowsRead, ref num5);
							}
						}
						if (connection.ModelMetadata != null)
						{
							this.AggregateValue(connection.ModelMetadata.TotalTimeDataRetrieval, ref num4);
						}
					}
				}
			}
			if (num != null)
			{
				this.m_additionalInfo.TimeQueryTranslation = num;
			}
			if (num2 != null)
			{
				this.ProcessingTime = TimeSpan.FromMilliseconds((double)num2.Value);
			}
			if (num3 != null)
			{
				this.RenderingTime = TimeSpan.FromMilliseconds((double)num3.Value);
			}
			if (num4 != null)
			{
				this.DataRetrievalTime = TimeSpan.FromMilliseconds((double)num4.Value);
			}
			if (num5 != null)
			{
				this.RowCount = num5.Value;
			}
			if (num6 != null || num7 != null || num8 != null)
			{
				this.m_additionalInfo.EstimatedMemoryUsageKB = new EstimatedMemoryUsageKBCategory
				{
					Pagination = num6,
					Processing = num7,
					Rendering = num8
				};
			}
			if (num9 != null || num10 != null || num11 != null)
			{
				this.m_additionalInfo.ScalabilityTime = new ScaleTimeCategory
				{
					Pagination = num9,
					Processing = num10,
					Rendering = num11
				};
			}
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000B680 File Offset: 0x00009880
		private void AggregateValue(long? from, ref long? to)
		{
			if (from != null)
			{
				if (to == null)
				{
					to = new long?(0L);
				}
				to += from.Value;
			}
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000B6E4 File Offset: 0x000098E4
		public string GetAdditionalInfoXml()
		{
			if (this.m_additionalInfo == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, ReportExecutionInfo.m_additionalInfoSettings))
			{
				ReportExecutionInfo.s_xmlSerializer.Serialize(xmlWriter, this.m_additionalInfo, ReportExecutionInfo.m_additionalInfoNamespace);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000B748 File Offset: 0x00009948
		public Exception SetStatusFromException(Exception e)
		{
			RSException ex = e as RSException;
			if (ex != null)
			{
				this.m_status = ex.Code;
				return new RSException(ex);
			}
			if (e is ThreadAbortException)
			{
				ThreadAbortException ex2 = e as ThreadAbortException;
				if (ex2.ExceptionState != null && ex2.ExceptionState is ReportServerAbortInfo)
				{
					ReportServerAbortInfo reportServerAbortInfo = ex2.ExceptionState as ReportServerAbortInfo;
					ProcessingContext.JobsTracer.Trace(TraceLevel.Info, "CancelableJobExecution.Execute caught our thread abort exception");
					Thread.ResetAbort();
					ReportServerAbortInfo.AbortReason reason = reportServerAbortInfo.Reason;
					if (reason == ReportServerAbortInfo.AbortReason.TimeoutExpired)
					{
						this.m_status = ErrorCode.rsReportTimeoutExpired;
						return new ReportTimeoutExpiredException(e);
					}
					if (reason - ReportServerAbortInfo.AbortReason.JobCanceled <= 1)
					{
						this.m_status = ErrorCode.rsJobWasCanceled;
						return new JobCanceledException(e);
					}
				}
				else
				{
					ProcessingContext.JobsTracer.Trace(TraceLevel.Info, "CancelableJobExecution.Execute caught some other thread abort exception");
				}
				this.m_status = ErrorCode.rsInternalError;
				return null;
			}
			this.m_status = ErrorCode.rsInternalError;
			return new InternalCatalogException(e, "Internal error");
		}

		// Token: 0x040001DE RID: 478
		private ExternalItemPath m_itemPath;

		// Token: 0x040001DF RID: 479
		private string m_format;

		// Token: 0x040001E0 RID: 480
		private string m_parameters;

		// Token: 0x040001E1 RID: 481
		private ExecutionLogExecType m_source = ExecutionLogExecType.Live;

		// Token: 0x040001E2 RID: 482
		private ExecutionLogLevel m_execLogLevel;

		// Token: 0x040001E3 RID: 483
		private ErrorCode m_status;

		// Token: 0x040001E4 RID: 484
		private long m_byteCount;

		// Token: 0x040001E5 RID: 485
		private long m_rowCount;

		// Token: 0x040001E6 RID: 486
		private TimeSpan m_processingTime;

		// Token: 0x040001E7 RID: 487
		private TimeSpan m_renderingTime;

		// Token: 0x040001E8 RID: 488
		private TimeSpan m_dataRetrievalTime;

		// Token: 0x040001E9 RID: 489
		private string m_executionId;

		// Token: 0x040001EA RID: 490
		private ReportEventType m_eventType = ReportEventType.Render;

		// Token: 0x040001EB RID: 491
		private AdditionalInfo m_additionalInfo;

		// Token: 0x040001EC RID: 492
		private readonly object m_additionalInfoSync = new object();

		// Token: 0x040001ED RID: 493
		private static readonly XmlSerializer s_xmlSerializer = new XmlSerializer(typeof(AdditionalInfo));

		// Token: 0x040001EE RID: 494
		private static readonly XmlWriterSettings m_additionalInfoSettings = new XmlWriterSettings
		{
			OmitXmlDeclaration = true,
			Indent = false,
			CheckCharacters = false
		};

		// Token: 0x040001EF RID: 495
		private static readonly XmlSerializerNamespaces m_additionalInfoNamespace = new XmlSerializerNamespaces(new XmlQualifiedName[]
		{
			new XmlQualifiedName(string.Empty, string.Empty)
		});
	}
}
