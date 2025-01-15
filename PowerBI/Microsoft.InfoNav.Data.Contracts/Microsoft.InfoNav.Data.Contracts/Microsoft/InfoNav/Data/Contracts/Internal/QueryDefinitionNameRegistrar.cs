using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000267 RID: 615
	internal sealed class QueryDefinitionNameRegistrar : IQueryDefinitionNameRegistrar
	{
		// Token: 0x06001265 RID: 4709 RVA: 0x0002033D File Offset: 0x0001E53D
		internal QueryDefinitionNameRegistrar()
		{
			this._availableNames = new Stack<QueryDefinitionNameRegistrar.NameInfo>();
			this._usedQueryDefinitionNames = new HashSet<string>(QueryNameComparer.Instance);
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x00020360 File Offset: 0x0001E560
		public void PushName(string name, bool isUnique)
		{
			this._availableNames.Push(new QueryDefinitionNameRegistrar.NameInfo(name, isUnique));
			if (isUnique)
			{
				this._usedQueryDefinitionNames.Add(name);
			}
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x00020384 File Offset: 0x0001E584
		public void PopName(string name)
		{
			this._availableNames.Pop();
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x00020394 File Offset: 0x0001E594
		public string GetNextName()
		{
			QueryDefinitionNameRegistrar.NameInfo nameInfo = this._availableNames.Peek();
			string text = nameInfo.CandidateName;
			if (!nameInfo.IsUnique)
			{
				text = StringUtil.MakeUniqueName(text, this._usedQueryDefinitionNames);
				this._usedQueryDefinitionNames.Add(text);
			}
			return text;
		}

		// Token: 0x040007C4 RID: 1988
		private readonly Stack<QueryDefinitionNameRegistrar.NameInfo> _availableNames;

		// Token: 0x040007C5 RID: 1989
		private readonly HashSet<string> _usedQueryDefinitionNames;

		// Token: 0x0200033B RID: 827
		private readonly struct NameInfo
		{
			// Token: 0x06001A14 RID: 6676 RVA: 0x0002F094 File Offset: 0x0002D294
			internal NameInfo(string candidateName, bool isUnique)
			{
				this.CandidateName = candidateName;
				this.IsUnique = isUnique;
			}

			// Token: 0x17000550 RID: 1360
			// (get) Token: 0x06001A15 RID: 6677 RVA: 0x0002F0A4 File Offset: 0x0002D2A4
			internal string CandidateName { get; }

			// Token: 0x17000551 RID: 1361
			// (get) Token: 0x06001A16 RID: 6678 RVA: 0x0002F0AC File Offset: 0x0002D2AC
			internal bool IsUnique { get; }
		}
	}
}
