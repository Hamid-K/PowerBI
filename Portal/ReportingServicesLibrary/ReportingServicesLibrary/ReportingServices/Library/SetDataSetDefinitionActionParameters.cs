using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000F9 RID: 249
	internal sealed class SetDataSetDefinitionActionParameters : UpdateItemActionParameters
	{
		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x00027501 File Offset: 0x00025701
		// (set) Token: 0x06000A41 RID: 2625 RVA: 0x00027509 File Offset: 0x00025709
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

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x00027512 File Offset: 0x00025712
		// (set) Token: 0x06000A43 RID: 2627 RVA: 0x0002751A File Offset: 0x0002571A
		public byte[] DataSetDefinition
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

		// Token: 0x06000A44 RID: 2628 RVA: 0x00027523 File Offset: 0x00025723
		internal override void Validate()
		{
			if (base.ItemPath == null)
			{
				throw new MissingParameterException("ItemPath");
			}
			if (this.DataSetDefinition == null)
			{
				throw new MissingParameterException("Definition");
			}
		}

		// Token: 0x04000486 RID: 1158
		private Warning[] m_warnings;

		// Token: 0x04000487 RID: 1159
		private byte[] m_definition;
	}
}
