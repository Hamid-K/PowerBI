using System;
using System.Collections.Specialized;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200019C RID: 412
	internal sealed class GetReportParametersActionParameters : RSSoapActionParameters
	{
		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x00036859 File Offset: 0x00034A59
		// (set) Token: 0x06000F0B RID: 3851 RVA: 0x00036861 File Offset: 0x00034A61
		public string ItemPath
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_itemPath;
			}
			set
			{
				this.m_itemPath = value;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x0003686A File Offset: 0x00034A6A
		// (set) Token: 0x06000F0D RID: 3853 RVA: 0x00036872 File Offset: 0x00034A72
		public string HistoryID
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_historyID;
			}
			set
			{
				this.m_historyID = value;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x0003687B File Offset: 0x00034A7B
		// (set) Token: 0x06000F0F RID: 3855 RVA: 0x00036883 File Offset: 0x00034A83
		public bool ForRendering
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_forRendering;
			}
			set
			{
				this.m_forRendering = value;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x0003688C File Offset: 0x00034A8C
		// (set) Token: 0x06000F11 RID: 3857 RVA: 0x00036894 File Offset: 0x00034A94
		public NameValueCollection ParameterValidationValues
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_parameterValidationValues;
			}
			set
			{
				this.m_parameterValidationValues = value;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x0003689D File Offset: 0x00034A9D
		// (set) Token: 0x06000F13 RID: 3859 RVA: 0x000368A5 File Offset: 0x00034AA5
		public DatasourceCredentialsCollection DatasourceCredentials
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_datasourceCredentials;
			}
			set
			{
				this.m_datasourceCredentials = value;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x000368AE File Offset: 0x00034AAE
		// (set) Token: 0x06000F15 RID: 3861 RVA: 0x000368B6 File Offset: 0x00034AB6
		public ParameterInfoCollection Parameters
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_parameters;
			}
			set
			{
				this.m_parameters = value;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x000368BF File Offset: 0x00034ABF
		internal override string InputTrace
		{
			[DebuggerStepThrough]
			get
			{
				return this.ItemPath;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06000F17 RID: 3863 RVA: 0x000368C7 File Offset: 0x00034AC7
		// (set) Token: 0x06000F18 RID: 3864 RVA: 0x000368CF File Offset: 0x00034ACF
		internal bool Use2006FallbackBehavior
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_use2006FalllbackBehavior;
			}
			set
			{
				this.m_use2006FalllbackBehavior = value;
			}
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x000368D8 File Offset: 0x00034AD8
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException(CallingEndpoint.Is2010Endpoint ? "ItemPath" : "Report");
			}
			if (this.HistoryID != null)
			{
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.History);
			}
		}

		// Token: 0x04000616 RID: 1558
		private string m_itemPath;

		// Token: 0x04000617 RID: 1559
		private string m_historyID;

		// Token: 0x04000618 RID: 1560
		private bool m_forRendering;

		// Token: 0x04000619 RID: 1561
		private NameValueCollection m_parameterValidationValues;

		// Token: 0x0400061A RID: 1562
		private DatasourceCredentialsCollection m_datasourceCredentials;

		// Token: 0x0400061B RID: 1563
		private ParameterInfoCollection m_parameters;

		// Token: 0x0400061C RID: 1564
		private bool m_use2006FalllbackBehavior;
	}
}
