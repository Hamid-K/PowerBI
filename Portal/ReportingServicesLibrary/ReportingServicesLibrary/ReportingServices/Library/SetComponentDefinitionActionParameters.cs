using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000DF RID: 223
	internal sealed class SetComponentDefinitionActionParameters : UpdateItemActionParameters
	{
		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x00025D9E File Offset: 0x00023F9E
		// (set) Token: 0x060009B3 RID: 2483 RVA: 0x00025DA6 File Offset: 0x00023FA6
		public byte[] ComponentDefinition
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

		// Token: 0x060009B4 RID: 2484 RVA: 0x00025DAF File Offset: 0x00023FAF
		internal override void Validate()
		{
			if (base.ItemPath == null)
			{
				throw new MissingParameterException("ItemPath");
			}
			if (this.ComponentDefinition == null)
			{
				throw new MissingParameterException("Definition");
			}
		}

		// Token: 0x04000469 RID: 1129
		private byte[] m_definition;
	}
}
