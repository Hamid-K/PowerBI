using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000144 RID: 324
	internal sealed class GetModelDefinitionActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x0002F169 File Offset: 0x0002D369
		// (set) Token: 0x06000CA3 RID: 3235 RVA: 0x0002F171 File Offset: 0x0002D371
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

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x0002F17A File Offset: 0x0002D37A
		// (set) Token: 0x06000CA5 RID: 3237 RVA: 0x0002F182 File Offset: 0x0002D382
		public byte[] ModelDefinition
		{
			get
			{
				return this.m_modelDefinition;
			}
			set
			{
				this.m_modelDefinition = value;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x0002F18B File Offset: 0x0002D38B
		internal override string InputTrace
		{
			get
			{
				return this.ItemPath;
			}
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0002F193 File Offset: 0x0002D393
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException("Model");
			}
		}

		// Token: 0x0400051D RID: 1309
		private string m_itemPath;

		// Token: 0x0400051E RID: 1310
		private byte[] m_modelDefinition;
	}
}
