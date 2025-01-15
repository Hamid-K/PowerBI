using System;
using System.CodeDom.Compiler;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.External
{
	// Token: 0x020000B9 RID: 185
	[GeneratedCode("gbc", "0.4.1.0")]
	internal class ExceptionData : Domain, ISerializableWithWriter
	{
		// Token: 0x060005D6 RID: 1494 RVA: 0x000167EC File Offset: 0x000149EC
		public ExceptionData DeepClone()
		{
			ExceptionData exceptionData = new ExceptionData();
			exceptionData.ver = this.ver;
			exceptionData.severityLevel = this.severityLevel;
			exceptionData.problemId = this.problemId;
			Utils.CopyDictionary<string>(this.properties, exceptionData.properties);
			Utils.CopyDictionary<double>(this.measurements, exceptionData.measurements);
			foreach (ExceptionDetails exceptionDetails in this.exceptions)
			{
				exceptionData.exceptions.Add(exceptionDetails);
			}
			return exceptionData;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0001688C File Offset: 0x00014A8C
		public void Serialize(ISerializationWriter serializationWriter)
		{
			serializationWriter.WriteProperty("ver", new int?(this.ver));
			serializationWriter.WriteProperty("problemId", this.problemId);
			serializationWriter.WriteProperty("exceptions", this.exceptions.ToList<ISerializableWithWriter>());
			serializationWriter.WriteProperty("severityLevel", (this.severityLevel.TranslateSeverityLevel() != null) ? this.severityLevel.TranslateSeverityLevel().Value.ToString() : null);
			serializationWriter.WriteProperty("properties", this.properties);
			serializationWriter.WriteProperty("measurements", this.measurements);
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x0001693C File Offset: 0x00014B3C
		// (set) Token: 0x060005D9 RID: 1497 RVA: 0x00016944 File Offset: 0x00014B44
		public int ver { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x0001694D File Offset: 0x00014B4D
		// (set) Token: 0x060005DB RID: 1499 RVA: 0x00016955 File Offset: 0x00014B55
		public IList<ExceptionDetails> exceptions { get; set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x0001695E File Offset: 0x00014B5E
		// (set) Token: 0x060005DD RID: 1501 RVA: 0x00016966 File Offset: 0x00014B66
		public SeverityLevel? severityLevel { get; set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x0001696F File Offset: 0x00014B6F
		// (set) Token: 0x060005DF RID: 1503 RVA: 0x00016977 File Offset: 0x00014B77
		public string problemId { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x00016980 File Offset: 0x00014B80
		// (set) Token: 0x060005E1 RID: 1505 RVA: 0x00016988 File Offset: 0x00014B88
		public IDictionary<string, string> properties { get; set; }

		// Token: 0x060005E2 RID: 1506 RVA: 0x00016991 File Offset: 0x00014B91
		public ExceptionData()
			: this("AI.ExceptionData", "ExceptionData")
		{
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x000169A3 File Offset: 0x00014BA3
		protected ExceptionData(string fullName, string name)
		{
			this.ver = 2;
			this.exceptions = new List<ExceptionDetails>();
			this.problemId = "";
			this.properties = new ConcurrentDictionary<string, string>();
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x000169D3 File Offset: 0x00014BD3
		// (set) Token: 0x060005E5 RID: 1509 RVA: 0x000169FF File Offset: 0x00014BFF
		public IDictionary<string, double> measurements
		{
			get
			{
				return LazyInitializer.EnsureInitialized<IDictionary<string, double>>(ref this.measurementsInternal, () => new ConcurrentDictionary<string, double>());
			}
			set
			{
				this.measurementsInternal = value;
			}
		}

		// Token: 0x0400025F RID: 607
		private IDictionary<string, double> measurementsInternal;
	}
}
