using System;
using System.Globalization;
using Microsoft.Cloud.Platform.Configuration;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.ConfigurationClasses.Tracing
{
	// Token: 0x0200043C RID: 1084
	[Serializable]
	public sealed class TraceSourceConfig : ConfigurationClass
	{
		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060021A1 RID: 8609 RVA: 0x0007D148 File Offset: 0x0007B348
		// (set) Token: 0x060021A2 RID: 8610 RVA: 0x0007D150 File Offset: 0x0007B350
		[ConfigurationProperty]
		public TraceVerbosity Verbosity { get; set; }

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060021A3 RID: 8611 RVA: 0x0007D159 File Offset: 0x0007B359
		// (set) Token: 0x060021A4 RID: 8612 RVA: 0x0007D161 File Offset: 0x0007B361
		[ConfigurationProperty]
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				base.ValidateStringNotNullEmptyOrWhiteSpace(value, "Name");
				this.m_name = value;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060021A5 RID: 8613 RVA: 0x0007D176 File Offset: 0x0007B376
		[NonConfigurationProperty]
		public TraceSourceIdentifier ID
		{
			get
			{
				return new TraceSourceIdentifier(this.Name);
			}
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x0007D183 File Offset: 0x0007B383
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "ID: {0}. Verbosity: {1}.", new object[] { this.ID, this.Verbosity });
		}

		// Token: 0x04000B94 RID: 2964
		private string m_name;
	}
}
