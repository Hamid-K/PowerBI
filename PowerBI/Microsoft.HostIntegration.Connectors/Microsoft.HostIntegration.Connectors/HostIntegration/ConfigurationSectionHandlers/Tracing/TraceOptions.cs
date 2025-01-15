using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Tracing
{
	// Token: 0x02000524 RID: 1316
	public class TraceOptions : ConfigurationElement
	{
		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06002C7A RID: 11386 RVA: 0x000977BC File Offset: 0x000959BC
		// (set) Token: 0x06002C7B RID: 11387 RVA: 0x000977CE File Offset: 0x000959CE
		[Description("Name of the file which contains Tracing Definitions")]
		[Category("General")]
		[ConfigurationProperty("traceDefinitionFile", IsRequired = true)]
		public string TraceDefinitionFile
		{
			get
			{
				return (string)base["traceDefinitionFile"];
			}
			set
			{
				base["traceDefinitionFile"] = value;
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06002C7C RID: 11388 RVA: 0x000977DC File Offset: 0x000959DC
		// (set) Token: 0x06002C7D RID: 11389 RVA: 0x000977EE File Offset: 0x000959EE
		[Description("Enable tracing to file with the appropriate settings.")]
		[Category("General")]
		[ConfigurationProperty("writeTraceFile", IsRequired = false, DefaultValue = false)]
		public bool WriteTraceFile
		{
			get
			{
				return (bool)base["writeTraceFile"];
			}
			set
			{
				base["writeTraceFile"] = value;
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06002C7E RID: 11390 RVA: 0x00097801 File Offset: 0x00095A01
		// (set) Token: 0x06002C7F RID: 11391 RVA: 0x00097813 File Offset: 0x00095A13
		[Description("Name of the Folder where trace output should be written.")]
		[Category("General")]
		[ConfigurationProperty("traceFileFolder", IsRequired = false)]
		public string TraceFileFolder
		{
			get
			{
				return (string)base["traceFileFolder"];
			}
			set
			{
				base["traceFileFolder"] = value;
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06002C80 RID: 11392 RVA: 0x00097821 File Offset: 0x00095A21
		// (set) Token: 0x06002C81 RID: 11393 RVA: 0x00097833 File Offset: 0x00095A33
		[Description("Preamble used to distinguish trace files.")]
		[Category("General")]
		[ConfigurationProperty("fileNamePreamble", IsRequired = false, DefaultValue = "HisTracing")]
		public string FileNamePreamble
		{
			get
			{
				return (string)base["fileNamePreamble"];
			}
			set
			{
				base["fileNamePreamble"] = value;
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06002C82 RID: 11394 RVA: 0x00097841 File Offset: 0x00095A41
		// (set) Token: 0x06002C83 RID: 11395 RVA: 0x00097853 File Offset: 0x00095A53
		[Description("Number of trace lines written to each file.")]
		[Category("General")]
		[ConfigurationProperty("maxTraceEntries", IsRequired = false, DefaultValue = 1000000)]
		public int MaxTraceEntries
		{
			get
			{
				return (int)base["maxTraceEntries"];
			}
			set
			{
				base["maxTraceEntries"] = value;
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06002C84 RID: 11396 RVA: 0x00097866 File Offset: 0x00095A66
		// (set) Token: 0x06002C85 RID: 11397 RVA: 0x00097878 File Offset: 0x00095A78
		[Description("Flush trace file after each record.")]
		[Category("General")]
		[ConfigurationProperty("autoFlush", IsRequired = false, DefaultValue = false)]
		public bool AutoFlush
		{
			get
			{
				return (bool)base["autoFlush"];
			}
			set
			{
				base["autoFlush"] = value;
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06002C86 RID: 11398 RVA: 0x0009788B File Offset: 0x00095A8B
		// (set) Token: 0x06002C87 RID: 11399 RVA: 0x0009789D File Offset: 0x00095A9D
		[Description("Open trace file when any Trace statement produces output.")]
		[Category("General")]
		[ConfigurationProperty("allowNonHisTracingToCreateFile", IsRequired = false, DefaultValue = false)]
		public bool AllowNonHisTracingToCreateFile
		{
			get
			{
				return (bool)base["allowNonHisTracingToCreateFile"];
			}
			set
			{
				base["allowNonHisTracingToCreateFile"] = value;
			}
		}
	}
}
