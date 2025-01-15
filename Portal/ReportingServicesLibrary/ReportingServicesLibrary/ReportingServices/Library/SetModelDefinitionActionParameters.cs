using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000156 RID: 342
	internal sealed class SetModelDefinitionActionParameters : UpdateModelDefinitionActionParameters
	{
		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x0003022F File Offset: 0x0002E42F
		// (set) Token: 0x06000D12 RID: 3346 RVA: 0x00030237 File Offset: 0x0002E437
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

		// Token: 0x06000D13 RID: 3347 RVA: 0x00030240 File Offset: 0x0002E440
		internal override void Validate()
		{
			if (base.ItemPath == null)
			{
				throw new MissingParameterException(CallingEndpoint.Is2010Endpoint ? "ItemPath" : "Model");
			}
			if (this.ModelDefinition == null)
			{
				throw new MissingParameterException("Definition");
			}
		}

		// Token: 0x04000539 RID: 1337
		private byte[] m_modelDefinition;
	}
}
