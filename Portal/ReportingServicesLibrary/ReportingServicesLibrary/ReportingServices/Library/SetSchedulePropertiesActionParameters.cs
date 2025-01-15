using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001DA RID: 474
	internal sealed class SetSchedulePropertiesActionParameters : RSSoapActionParameters
	{
		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x00039CAD File Offset: 0x00037EAD
		// (set) Token: 0x0600106F RID: 4207 RVA: 0x00039CB5 File Offset: 0x00037EB5
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

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x00039CCE File Offset: 0x00037ECE
		// (set) Token: 0x06001071 RID: 4209 RVA: 0x00039CD6 File Offset: 0x00037ED6
		public string ScheduleID
		{
			get
			{
				return this.m_scheduleID;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					value = value.Trim();
				}
				this.m_scheduleID = value;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x00039CEF File Offset: 0x00037EEF
		// (set) Token: 0x06001073 RID: 4211 RVA: 0x00039CF7 File Offset: 0x00037EF7
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

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x00039D00 File Offset: 0x00037F00
		internal override string InputTrace
		{
			get
			{
				string text = string.Empty;
				if (RSTrace.CatalogTrace.TraceVerbose)
				{
					text = string.Format(CultureInfo.InvariantCulture, "ID ({0}), Schedule ({1})", this.m_scheduleID, ScheduleDefinition.DefinitionToXml(this.m_scheduledefinition));
				}
				else
				{
					text = string.Format(CultureInfo.InvariantCulture, "ID ({0})", this.m_scheduleID);
				}
				return text;
			}
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x00039D5C File Offset: 0x00037F5C
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
			if (string.IsNullOrEmpty(this.ScheduleID))
			{
				throw new MissingParameterException("ScheduleID");
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

		// Token: 0x0400065D RID: 1629
		private string m_name;

		// Token: 0x0400065E RID: 1630
		private string m_scheduleID = Guid.Empty.ToString();

		// Token: 0x0400065F RID: 1631
		private ScheduleDefinition m_scheduledefinition;
	}
}
