using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.ApplicationInsights.Extensibility.Implementation.External;

namespace Microsoft.ApplicationInsights.DataContracts
{
	// Token: 0x020000CE RID: 206
	internal sealed class ExceptionInfo
	{
		// Token: 0x060006FD RID: 1789 RVA: 0x00018840 File Offset: 0x00016A40
		public ExceptionInfo(IEnumerable<ExceptionDetailsInfo> exceptionDetailsInfoList, SeverityLevel? severityLevel, string problemId, IDictionary<string, string> properties, IDictionary<string, double> measurements)
		{
			ExceptionData exceptionData = new ExceptionData();
			exceptionData.exceptions = exceptionDetailsInfoList.Select((ExceptionDetailsInfo edi) => edi.ExceptionDetails).ToList<ExceptionDetails>();
			exceptionData.severityLevel = severityLevel.TranslateSeverityLevel();
			exceptionData.problemId = problemId;
			exceptionData.properties = new ConcurrentDictionary<string, string>(properties);
			exceptionData.measurements = new ConcurrentDictionary<string, double>(measurements);
			this.data = exceptionData;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x000188BB File Offset: 0x00016ABB
		internal ExceptionInfo(ExceptionData data)
		{
			this.data = data;
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x000188CA File Offset: 0x00016ACA
		public IReadOnlyList<ExceptionDetailsInfo> ExceptionDetailsInfoList
		{
			get
			{
				return this.data.exceptions.Select((ExceptionDetails ed) => new ExceptionDetailsInfo(ed)).ToList<ExceptionDetailsInfo>().AsReadOnly();
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x00018905 File Offset: 0x00016B05
		// (set) Token: 0x06000701 RID: 1793 RVA: 0x00018917 File Offset: 0x00016B17
		public SeverityLevel? SeverityLevel
		{
			get
			{
				return this.data.severityLevel.TranslateSeverityLevel();
			}
			set
			{
				this.data.severityLevel = value.TranslateSeverityLevel();
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x0001892A File Offset: 0x00016B2A
		// (set) Token: 0x06000703 RID: 1795 RVA: 0x00018937 File Offset: 0x00016B37
		public string ProblemId
		{
			get
			{
				return this.data.problemId;
			}
			set
			{
				this.data.problemId = value;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x00018945 File Offset: 0x00016B45
		// (set) Token: 0x06000705 RID: 1797 RVA: 0x00018952 File Offset: 0x00016B52
		public IDictionary<string, string> Properties
		{
			get
			{
				return this.data.properties;
			}
			set
			{
				this.data.properties = value;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x00018960 File Offset: 0x00016B60
		// (set) Token: 0x06000707 RID: 1799 RVA: 0x0001896D File Offset: 0x00016B6D
		public IDictionary<string, double> Measurements
		{
			get
			{
				return this.data.measurements;
			}
			set
			{
				this.data.measurements = value;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x0001897B File Offset: 0x00016B7B
		internal ExceptionData Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00018983 File Offset: 0x00016B83
		internal ExceptionInfo DeepClone()
		{
			return new ExceptionInfo(this.data.DeepClone());
		}

		// Token: 0x040002C9 RID: 713
		private readonly ExceptionData data;
	}
}
