using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001D0 RID: 464
	internal sealed class SetResourceContentsActionParameters : UpdateItemActionParameters
	{
		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001033 RID: 4147 RVA: 0x00039510 File Offset: 0x00037710
		// (set) Token: 0x06001034 RID: 4148 RVA: 0x00039518 File Offset: 0x00037718
		public byte[] Definition
		{
			get
			{
				return this.m_definition;
			}
			set
			{
				this.m_definition = value;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001035 RID: 4149 RVA: 0x00039521 File Offset: 0x00037721
		// (set) Token: 0x06001036 RID: 4150 RVA: 0x00039529 File Offset: 0x00037729
		public string MimeType
		{
			get
			{
				return this.m_mimeType;
			}
			set
			{
				this.m_mimeType = value;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x00039532 File Offset: 0x00037732
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}", base.ItemPath, this.MimeType);
			}
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x00039550 File Offset: 0x00037750
		internal override void Validate()
		{
			if (base.ItemPath == null)
			{
				throw new MissingParameterException(CallingEndpoint.Is2010Endpoint ? "ItemPath" : "Resource");
			}
			if (this.Definition == null)
			{
				throw new MissingParameterException(CallingEndpoint.Is2010Endpoint ? "Definition" : "Contents");
			}
			if (this.MimeType == null)
			{
				throw new MissingParameterException("MimeType");
			}
		}

		// Token: 0x04000652 RID: 1618
		private byte[] m_definition;

		// Token: 0x04000653 RID: 1619
		private string m_mimeType;
	}
}
