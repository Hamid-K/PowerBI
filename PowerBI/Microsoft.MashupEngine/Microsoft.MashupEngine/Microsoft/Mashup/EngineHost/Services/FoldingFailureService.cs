using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019E7 RID: 6631
	public class FoldingFailureService : IFoldingFailureService
	{
		// Token: 0x0600A7DB RID: 42971 RVA: 0x0022B66A File Offset: 0x0022986A
		public FoldingFailureService(bool throwOnFoldingFailure, bool throwOnVolatileFunctions)
		{
			this.throwOnFoldingFailure = throwOnFoldingFailure;
			this.throwOnVolatileFunctions = throwOnVolatileFunctions;
		}

		// Token: 0x17002AB8 RID: 10936
		// (get) Token: 0x0600A7DC RID: 42972 RVA: 0x0022B680 File Offset: 0x00229880
		public bool ThrowOnFoldingFailure
		{
			get
			{
				return this.throwOnFoldingFailure;
			}
		}

		// Token: 0x17002AB9 RID: 10937
		// (get) Token: 0x0600A7DD RID: 42973 RVA: 0x0022B688 File Offset: 0x00229888
		public bool ThrowOnVolatileFunctions
		{
			get
			{
				return this.throwOnVolatileFunctions;
			}
		}

		// Token: 0x04005767 RID: 22375
		private readonly bool throwOnFoldingFailure;

		// Token: 0x04005768 RID: 22376
		private readonly bool throwOnVolatileFunctions;
	}
}
