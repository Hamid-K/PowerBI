using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001D2 RID: 466
	internal sealed class CreateScheduleActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x0600103E RID: 4158 RVA: 0x0003974F File Offset: 0x0003794F
		// (set) Token: 0x0600103F RID: 4159 RVA: 0x00039757 File Offset: 0x00037957
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					value = value.Trim();
				}
				this.m_name = value;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001040 RID: 4160 RVA: 0x00039770 File Offset: 0x00037970
		// (set) Token: 0x06001041 RID: 4161 RVA: 0x00039778 File Offset: 0x00037978
		public ScheduleDefinition ScheduleDefinition
		{
			get
			{
				return this.m_scheduledefinition;
			}
			set
			{
				this.m_scheduledefinition = value;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001042 RID: 4162 RVA: 0x00039781 File Offset: 0x00037981
		// (set) Token: 0x06001043 RID: 4163 RVA: 0x00039789 File Offset: 0x00037989
		public string Site
		{
			get
			{
				return this.m_site;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					value = value.Trim();
				}
				this.m_site = value;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001044 RID: 4164 RVA: 0x000397A2 File Offset: 0x000379A2
		// (set) Token: 0x06001045 RID: 4165 RVA: 0x000397AA File Offset: 0x000379AA
		public string ScheduleID
		{
			get
			{
				return this.m_scheduleID;
			}
			set
			{
				this.m_scheduleID = value;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001046 RID: 4166 RVA: 0x000397B3 File Offset: 0x000379B3
		internal override string OutputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}", this.m_scheduleID);
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001047 RID: 4167 RVA: 0x000397CC File Offset: 0x000379CC
		internal override string InputTrace
		{
			get
			{
				string text = string.Empty;
				if (RSTrace.CatalogTrace.TraceVerbose)
				{
					text = string.Format(CultureInfo.InvariantCulture, "Schedule ({0})", ScheduleDefinition.DefinitionToXml(this.m_scheduledefinition));
				}
				return text;
			}
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x00039808 File Offset: 0x00037A08
		internal override void Validate()
		{
			if (string.IsNullOrEmpty(this.Name))
			{
				throw new MissingParameterException("Name");
			}
			if (this.Name.Length > CatalogItemNameUtility.MaxItemNameLength)
			{
				throw new InvalidItemNameException(this.Name, CatalogItemNameUtility.MaxItemNameLength);
			}
			if (this.ScheduleDefinition == null)
			{
				throw new MissingParameterException("ScheduleDefinition");
			}
			if (!this.ScheduleDefinition.IsValid())
			{
				throw new InvalidParameterException("ScheduleDefinition");
			}
		}

		// Token: 0x04000654 RID: 1620
		private string m_name;

		// Token: 0x04000655 RID: 1621
		private ScheduleDefinition m_scheduledefinition;

		// Token: 0x04000656 RID: 1622
		private string m_site;

		// Token: 0x04000657 RID: 1623
		private string m_scheduleID = Guid.Empty.ToString();
	}
}
