using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000DB RID: 219
	internal sealed class CreateComponentActionParameters : CreateItemActionParameters
	{
		// Token: 0x060009A0 RID: 2464 RVA: 0x00025C37 File Offset: 0x00023E37
		internal override void Validate()
		{
			if (base.ItemName == null)
			{
				throw new MissingParameterException("Component");
			}
			if (base.ParentPath == null)
			{
				throw new MissingParameterException("Parent");
			}
			if (this.ComponentDefinition == null)
			{
				throw new MissingParameterException("Definition");
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x00025C72 File Offset: 0x00023E72
		// (set) Token: 0x060009A2 RID: 2466 RVA: 0x00025C7A File Offset: 0x00023E7A
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

		// Token: 0x04000466 RID: 1126
		private byte[] m_definition;
	}
}
