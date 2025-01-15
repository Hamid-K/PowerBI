using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200013C RID: 316
	internal sealed class CreateModelActionParameters : CreateItemActionParameters
	{
		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x0002E7B2 File Offset: 0x0002C9B2
		// (set) Token: 0x06000C73 RID: 3187 RVA: 0x0002E7BA File Offset: 0x0002C9BA
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

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x0002E7C3 File Offset: 0x0002C9C3
		// (set) Token: 0x06000C75 RID: 3189 RVA: 0x0002E7CB File Offset: 0x0002C9CB
		public Warning[] Warnings
		{
			get
			{
				return this.m_warnings;
			}
			set
			{
				this.m_warnings = value;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x00010225 File Offset: 0x0000E425
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}", base.ItemName, base.ParentPath);
			}
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x0002E7D4 File Offset: 0x0002C9D4
		internal override void Validate()
		{
			if (base.ItemName == null)
			{
				throw new MissingParameterException(CallingEndpoint.Is2010Endpoint ? "Name" : "Model");
			}
			if (base.ParentPath == null)
			{
				throw new MissingParameterException("Parent");
			}
			if (this.ModelDefinition == null)
			{
				throw new MissingParameterException("Definition");
			}
		}

		// Token: 0x04000516 RID: 1302
		private byte[] m_modelDefinition;

		// Token: 0x04000517 RID: 1303
		private Warning[] m_warnings;
	}
}
