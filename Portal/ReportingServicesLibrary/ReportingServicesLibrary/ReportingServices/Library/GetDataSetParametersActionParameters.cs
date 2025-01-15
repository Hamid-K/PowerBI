using System;
using System.Collections.Specialized;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000101 RID: 257
	internal sealed class GetDataSetParametersActionParameters : RSSoapActionParameters
	{
		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x00027901 File Offset: 0x00025B01
		// (set) Token: 0x06000A5D RID: 2653 RVA: 0x00027909 File Offset: 0x00025B09
		public string ItemPath
		{
			get
			{
				return this.m_itemPath;
			}
			set
			{
				this.m_itemPath = value;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x00027912 File Offset: 0x00025B12
		// (set) Token: 0x06000A5F RID: 2655 RVA: 0x0002791A File Offset: 0x00025B1A
		public bool ForRendering
		{
			get
			{
				return this.m_forRendering;
			}
			set
			{
				this.m_forRendering = value;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x00027923 File Offset: 0x00025B23
		// (set) Token: 0x06000A61 RID: 2657 RVA: 0x0002792B File Offset: 0x00025B2B
		public NameValueCollection ParameterValidationValues
		{
			get
			{
				return this.m_parameterValidationValues;
			}
			set
			{
				this.m_parameterValidationValues = value;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x00027934 File Offset: 0x00025B34
		// (set) Token: 0x06000A63 RID: 2659 RVA: 0x0002793C File Offset: 0x00025B3C
		public ParameterInfoCollection Parameters
		{
			get
			{
				return this.m_parameters;
			}
			set
			{
				this.m_parameters = value;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x00027945 File Offset: 0x00025B45
		internal override string InputTrace
		{
			get
			{
				return this.ItemPath;
			}
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0002794D File Offset: 0x00025B4D
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException("ItemPath");
			}
		}

		// Token: 0x0400048A RID: 1162
		private string m_itemPath;

		// Token: 0x0400048B RID: 1163
		private bool m_forRendering;

		// Token: 0x0400048C RID: 1164
		private NameValueCollection m_parameterValidationValues;

		// Token: 0x0400048D RID: 1165
		private ParameterInfoCollection m_parameters;
	}
}
