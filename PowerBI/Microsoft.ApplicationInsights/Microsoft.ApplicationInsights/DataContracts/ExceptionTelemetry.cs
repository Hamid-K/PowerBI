using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.ApplicationInsights.Extensibility.Implementation.External;

namespace Microsoft.ApplicationInsights.DataContracts
{
	// Token: 0x020000CF RID: 207
	public sealed class ExceptionTelemetry : ITelemetry, ISupportProperties, ISupportSampling, ISupportMetrics, IAiSerializableTelemetry
	{
		// Token: 0x0600070A RID: 1802 RVA: 0x00018995 File Offset: 0x00016B95
		public ExceptionTelemetry()
		{
			this.Data = new ExceptionInfo(new ExceptionData());
			this.context = new TelemetryContext(this.Data.Properties);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x000189C3 File Offset: 0x00016BC3
		public ExceptionTelemetry(Exception exception)
			: this()
		{
			if (exception == null)
			{
				exception = new Exception(Utils.PopulateRequiredStringValue(null, "message", typeof(ExceptionTelemetry).FullName));
			}
			this.Exception = exception;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x000189F8 File Offset: 0x00016BF8
		public ExceptionTelemetry(IEnumerable<ExceptionDetailsInfo> exceptionDetailsInfoList, SeverityLevel? severityLevel, string problemId, IDictionary<string, string> properties, IDictionary<string, double> measurements)
		{
			this.isCreatedFromExceptionInfo = true;
			ExceptionInfo exceptionInfo = new ExceptionInfo(exceptionDetailsInfoList, severityLevel, problemId, properties, measurements);
			this.Data = exceptionInfo;
			this.context = new TelemetryContext(this.Data.Properties);
			this.UpdateData(exceptionInfo);
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00018A44 File Offset: 0x00016C44
		private ExceptionTelemetry(ExceptionTelemetry source)
		{
			this.isCreatedFromExceptionInfo = source.isCreatedFromExceptionInfo;
			this.Data = source.Data.DeepClone();
			this.context = source.context.DeepClone(this.Data.Properties);
			this.Sequence = source.Sequence;
			this.Timestamp = source.Timestamp;
			this.samplingPercentage = source.samplingPercentage;
			if (!this.isCreatedFromExceptionInfo)
			{
				this.exception = source.Exception;
			}
			IExtension extension = source.extension;
			this.extension = ((extension != null) ? extension.DeepClone() : null);
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x00018AE0 File Offset: 0x00016CE0
		string IAiSerializableTelemetry.TelemetryName
		{
			get
			{
				return "Exception";
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x00018AE7 File Offset: 0x00016CE7
		string IAiSerializableTelemetry.BaseType
		{
			get
			{
				return "ExceptionData";
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x00018AEE File Offset: 0x00016CEE
		// (set) Token: 0x06000711 RID: 1809 RVA: 0x00018AF6 File Offset: 0x00016CF6
		public DateTimeOffset Timestamp { get; set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x00018AFF File Offset: 0x00016CFF
		// (set) Token: 0x06000713 RID: 1811 RVA: 0x00018B07 File Offset: 0x00016D07
		public string Sequence { get; set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x00018B10 File Offset: 0x00016D10
		public TelemetryContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x00018B18 File Offset: 0x00016D18
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x00018B20 File Offset: 0x00016D20
		public IExtension Extension
		{
			get
			{
				return this.extension;
			}
			set
			{
				this.extension = value;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x00018B29 File Offset: 0x00016D29
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x00018B36 File Offset: 0x00016D36
		public string ProblemId
		{
			get
			{
				return this.Data.ProblemId;
			}
			set
			{
				this.Data.ProblemId = value;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x00018B44 File Offset: 0x00016D44
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x00018B7F File Offset: 0x00016D7F
		[Obsolete("Use custom properties to report exception handling layer")]
		public ExceptionHandledAt HandledAt
		{
			get
			{
				ExceptionHandledAt exceptionHandledAt;
				if (this.Properties.ContainsKey("handledAt") && Enum.TryParse<ExceptionHandledAt>(this.Properties["handledAt"], out exceptionHandledAt))
				{
					return exceptionHandledAt;
				}
				return ExceptionHandledAt.Unhandled;
			}
			set
			{
				this.Properties["handledAt"] = value.ToString();
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x00018B9E File Offset: 0x00016D9E
		// (set) Token: 0x0600071C RID: 1820 RVA: 0x00018BCE File Offset: 0x00016DCE
		public Exception Exception
		{
			get
			{
				if (!this.isCreatedFromExceptionInfo)
				{
					return this.exception;
				}
				return this.ConstructExceptionFromDetailsInfo(this.Data.ExceptionDetailsInfoList ?? new List<ExceptionDetailsInfo>().AsReadOnly());
			}
			set
			{
				if (this.isCreatedFromExceptionInfo)
				{
					throw new InvalidOperationException("The property is unavailable to be set on an instance created with the ExceptionDetailsInfo-based constructor");
				}
				this.exception = value;
				this.UpdateData(value);
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x00018BF4 File Offset: 0x00016DF4
		// (set) Token: 0x0600071E RID: 1822 RVA: 0x00018C5C File Offset: 0x00016E5C
		public string Message
		{
			get
			{
				if (!this.isCreatedFromExceptionInfo)
				{
					return this.message;
				}
				if (this.Data.ExceptionDetailsInfoList == null)
				{
					return string.Empty;
				}
				return string.Join(" <--- ", this.Data.ExceptionDetailsInfoList.Select((ExceptionDetailsInfo info) => info.Message));
			}
			set
			{
				if (this.isCreatedFromExceptionInfo)
				{
					throw new InvalidOperationException("The property is unavailable to be set on an instance created with the ExceptionDetailsInfo-based constructor");
				}
				this.message = value;
				if (this.Data.ExceptionDetailsInfoList != null && this.Data.ExceptionDetailsInfoList.Count > 0)
				{
					this.Data.ExceptionDetailsInfoList[0].Message = value;
					return;
				}
				this.UpdateData(this.Exception);
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x00018CC7 File Offset: 0x00016EC7
		public IDictionary<string, double> Metrics
		{
			get
			{
				return this.Data.Measurements;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x00018CD4 File Offset: 0x00016ED4
		public IReadOnlyList<ExceptionDetailsInfo> ExceptionDetailsInfoList
		{
			get
			{
				return this.Data.ExceptionDetailsInfoList;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x00018CE1 File Offset: 0x00016EE1
		public IDictionary<string, string> Properties
		{
			get
			{
				return this.Data.Properties;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x00018CEE File Offset: 0x00016EEE
		// (set) Token: 0x06000723 RID: 1827 RVA: 0x00018CFB File Offset: 0x00016EFB
		public SeverityLevel? SeverityLevel
		{
			get
			{
				return this.Data.SeverityLevel;
			}
			set
			{
				this.Data.SeverityLevel = value;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x00018D09 File Offset: 0x00016F09
		// (set) Token: 0x06000725 RID: 1829 RVA: 0x00018D11 File Offset: 0x00016F11
		double? ISupportSampling.SamplingPercentage
		{
			get
			{
				return this.samplingPercentage;
			}
			set
			{
				this.samplingPercentage = value;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x00018D1A File Offset: 0x00016F1A
		internal IList<ExceptionDetails> Exceptions
		{
			get
			{
				return this.Data.Data.exceptions;
			}
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00018D2C File Offset: 0x00016F2C
		public ITelemetry DeepClone()
		{
			return new ExceptionTelemetry(this);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00018D34 File Offset: 0x00016F34
		public void SerializeData(ISerializationWriter serializationWriter)
		{
			serializationWriter.WriteProperty(this.Data.Data);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00018D48 File Offset: 0x00016F48
		public void SetParsedStack(global::System.Diagnostics.StackFrame[] frames)
		{
			if (this.Exceptions != null && this.Exceptions.Count > 0 && frames != null && frames.Length != 0)
			{
				int num = 0;
				this.Exceptions[0].parsedStack = new List<Microsoft.ApplicationInsights.Extensibility.Implementation.External.StackFrame>();
				this.Exceptions[0].hasFullStack = true;
				for (int i = 0; i < frames.Length; i++)
				{
					Microsoft.ApplicationInsights.Extensibility.Implementation.External.StackFrame stackFrame = ExceptionConverter.GetStackFrame(frames[i], i);
					num += ExceptionConverter.GetStackFrameLength(stackFrame);
					if (num > 32768)
					{
						this.Exceptions[0].hasFullStack = false;
						return;
					}
					this.Exceptions[0].parsedStack.Add(stackFrame);
				}
			}
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00018DF8 File Offset: 0x00016FF8
		void ITelemetry.Sanitize()
		{
			this.Properties.SanitizeProperties();
			this.Metrics.SanitizeMeasurements();
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00018E10 File Offset: 0x00017010
		private void ConvertExceptionTree(Exception exception, ExceptionDetails parentExceptionDetails, List<ExceptionDetails> exceptions)
		{
			if (exception == null)
			{
				exception = new Exception(Utils.PopulateRequiredStringValue(null, "message", typeof(ExceptionTelemetry).FullName));
			}
			ExceptionDetails exceptionDetails = ExceptionConverter.ConvertToExceptionDetails(exception, parentExceptionDetails);
			if (parentExceptionDetails == null && !string.IsNullOrWhiteSpace(this.Message))
			{
				exceptionDetails.message = this.Message;
			}
			exceptions.Add(exceptionDetails);
			AggregateException ex = exception as AggregateException;
			if (ex != null)
			{
				using (IEnumerator<Exception> enumerator = ex.InnerExceptions.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Exception ex2 = enumerator.Current;
						this.ConvertExceptionTree(ex2, exceptionDetails, exceptions);
					}
					return;
				}
			}
			if (exception.InnerException != null)
			{
				this.ConvertExceptionTree(exception.InnerException, exceptionDetails, exceptions);
			}
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00018ED0 File Offset: 0x000170D0
		private void UpdateData(Exception exception)
		{
			if (this.isCreatedFromExceptionInfo)
			{
				throw new InvalidOperationException("Operation is not supported given the state of the object.");
			}
			List<ExceptionDetails> list = new List<ExceptionDetails>();
			this.ConvertExceptionTree(exception, null, list);
			if (list.Count > 10)
			{
				InnerExceptionCountExceededException ex2 = new InnerExceptionCountExceededException(string.Format(CultureInfo.InvariantCulture, "The number of inner exceptions was {0} which is larger than {1}, the maximum number allowed during transmission. All but the first {1} have been dropped.", new object[] { list.Count, 10 }));
				list.RemoveRange(10, list.Count - 10);
				list.Add(ExceptionConverter.ConvertToExceptionDetails(ex2, list[0]));
			}
			this.Data = new ExceptionInfo(list.Select((ExceptionDetails ex) => new ExceptionDetailsInfo(ex)), this.SeverityLevel, this.ProblemId, this.Properties, this.Metrics);
			this.context = new TelemetryContext(this.Data.Properties);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00018FC0 File Offset: 0x000171C0
		private void UpdateData(ExceptionInfo exceptionInfo)
		{
			if (!this.isCreatedFromExceptionInfo)
			{
				throw new InvalidOperationException("Operation is not supported given the state of the object.");
			}
			if (exceptionInfo == null)
			{
				throw new ArgumentNullException("exceptionInfo");
			}
			this.Data = exceptionInfo;
			this.context = new TelemetryContext(this.Data.Properties);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0001900C File Offset: 0x0001720C
		private Exception ConstructExceptionFromDetailsInfo(IReadOnlyList<ExceptionDetailsInfo> exceptionInfos)
		{
			if (!this.isCreatedFromExceptionInfo)
			{
				throw new InvalidOperationException("Operation is not supported given the state of the object.");
			}
			if (!exceptionInfos.Any<ExceptionDetailsInfo>())
			{
				return new Exception(string.Empty);
			}
			return new Exception(exceptionInfos[0].Message, this.ConstructInnerException(exceptionInfos, 0));
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00019058 File Offset: 0x00017258
		private Exception ConstructInnerException(IReadOnlyList<ExceptionDetailsInfo> exceptionInfos, int parentExceptionIndex)
		{
			int num = parentExceptionIndex + 1;
			if (num < exceptionInfos.Count)
			{
				return new Exception(exceptionInfos[num].Message, this.ConstructInnerException(exceptionInfos, num));
			}
			return null;
		}

		// Token: 0x040002CA RID: 714
		internal const string TelemetryName = "Exception";

		// Token: 0x040002CB RID: 715
		internal ExceptionInfo Data;

		// Token: 0x040002CC RID: 716
		private readonly bool isCreatedFromExceptionInfo;

		// Token: 0x040002CD RID: 717
		private TelemetryContext context;

		// Token: 0x040002CE RID: 718
		private IExtension extension;

		// Token: 0x040002CF RID: 719
		private Exception exception;

		// Token: 0x040002D0 RID: 720
		private string message;

		// Token: 0x040002D1 RID: 721
		private double? samplingPercentage;
	}
}
