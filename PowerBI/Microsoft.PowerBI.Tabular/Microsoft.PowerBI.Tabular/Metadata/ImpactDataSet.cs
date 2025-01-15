using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Xml.Linq;
using Microsoft.AnalysisServices.Tabular.DDL;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x020001ED RID: 493
	internal class ImpactDataSet
	{
		// Token: 0x06001C84 RID: 7300 RVA: 0x000C605C File Offset: 0x000C425C
		public ImpactDataSet(DataSet data, Dictionary<XName, string> attributes)
		{
			this.data = data;
			this.BaseVersion = -1L;
			this.CurrentVersion = -1L;
			if (attributes.ContainsKey("name"))
			{
				try
				{
					this.DatabaseName = attributes["name"];
					this.BaseVersion = long.Parse(attributes["BaseVersion"], NumberStyles.Integer, CultureInfo.InvariantCulture);
					this.CurrentVersion = long.Parse(attributes["CurrentVersion"], NumberStyles.Integer, CultureInfo.InvariantCulture);
				}
				catch (FormatException ex)
				{
					throw TomInternalException.Create("FormatException while parsing versions. BaseVersion: {0} CurrentVersion: {1} Original Exception: '{2}'", new object[]
					{
						attributes["BaseVersion"],
						attributes["CurrentVersion"],
						ex.Message
					});
				}
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001C85 RID: 7301 RVA: 0x000C6148 File Offset: 0x000C4348
		// (set) Token: 0x06001C86 RID: 7302 RVA: 0x000C6150 File Offset: 0x000C4350
		public string DatabaseName { get; private set; }

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001C87 RID: 7303 RVA: 0x000C6159 File Offset: 0x000C4359
		// (set) Token: 0x06001C88 RID: 7304 RVA: 0x000C6161 File Offset: 0x000C4361
		public long BaseVersion { get; private set; }

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001C89 RID: 7305 RVA: 0x000C616A File Offset: 0x000C436A
		// (set) Token: 0x06001C8A RID: 7306 RVA: 0x000C6172 File Offset: 0x000C4372
		public long CurrentVersion { get; private set; }

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001C8B RID: 7307 RVA: 0x000C617B File Offset: 0x000C437B
		public bool IsEmpty
		{
			get
			{
				return string.IsNullOrEmpty(this.DatabaseName);
			}
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x000C6188 File Offset: 0x000C4388
		internal ServerImpact ConvertToServerImpact(Model target, IEqualityComparer<string> comparer)
		{
			ServerImpact impactFromSchema = DdlUtil.GetImpactFromSchema(target.Database.CompatibilityMode, target.Database.CompatibilityLevel, this.data, comparer);
			if (this.BaseVersion == 0L)
			{
				impactFromSchema.IsFullModel = true;
			}
			impactFromSchema.ModelVersion = this.CurrentVersion;
			return impactFromSchema;
		}

		// Token: 0x04000680 RID: 1664
		private readonly DataSet data;
	}
}
