using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000266 RID: 614
	internal sealed class NoOpQueryDefinitionNameRegistrar : IQueryDefinitionNameRegistrar
	{
		// Token: 0x06001260 RID: 4704 RVA: 0x00020322 File Offset: 0x0001E522
		private NoOpQueryDefinitionNameRegistrar()
		{
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x0002032A File Offset: 0x0001E52A
		public void PushName(string name, bool isUnique)
		{
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x0002032C File Offset: 0x0001E52C
		public void PopName(string name)
		{
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x0002032E File Offset: 0x0001E52E
		public string GetNextName()
		{
			return null;
		}

		// Token: 0x040007C3 RID: 1987
		internal static readonly NoOpQueryDefinitionNameRegistrar Instance = new NoOpQueryDefinitionNameRegistrar();
	}
}
