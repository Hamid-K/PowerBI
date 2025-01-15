using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003B3 RID: 947
	internal class KeyVec
	{
		// Token: 0x06002D9C RID: 11676 RVA: 0x0009200C File Offset: 0x0009020C
		internal KeyVec(Command itree)
		{
			this.m_keys = itree.CreateVarVec();
			this.m_noKeys = true;
		}

		// Token: 0x06002D9D RID: 11677 RVA: 0x00092027 File Offset: 0x00090227
		internal void InitFrom(KeyVec keyset)
		{
			this.m_keys.InitFrom(keyset.m_keys);
			this.m_noKeys = keyset.m_noKeys;
		}

		// Token: 0x06002D9E RID: 11678 RVA: 0x00092046 File Offset: 0x00090246
		internal void InitFrom(IEnumerable<Var> varSet)
		{
			this.InitFrom(varSet, false);
		}

		// Token: 0x06002D9F RID: 11679 RVA: 0x00092050 File Offset: 0x00090250
		internal void InitFrom(IEnumerable<Var> varSet, bool ignoreParameters)
		{
			this.m_keys.InitFrom(varSet, ignoreParameters);
			this.m_noKeys = false;
		}

		// Token: 0x06002DA0 RID: 11680 RVA: 0x00092068 File Offset: 0x00090268
		internal void InitFrom(KeyVec left, KeyVec right)
		{
			if (left.m_noKeys || right.m_noKeys)
			{
				this.m_noKeys = true;
				return;
			}
			this.m_noKeys = false;
			this.m_keys.InitFrom(left.m_keys);
			this.m_keys.Or(right.m_keys);
		}

		// Token: 0x06002DA1 RID: 11681 RVA: 0x000920B8 File Offset: 0x000902B8
		internal void InitFrom(List<KeyVec> keyVecList)
		{
			this.m_noKeys = false;
			this.m_keys.Clear();
			foreach (KeyVec keyVec in keyVecList)
			{
				if (keyVec.m_noKeys)
				{
					this.m_noKeys = true;
					break;
				}
				this.m_keys.Or(keyVec.m_keys);
			}
		}

		// Token: 0x06002DA2 RID: 11682 RVA: 0x00092134 File Offset: 0x00090334
		internal void Clear()
		{
			this.m_noKeys = true;
			this.m_keys.Clear();
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06002DA3 RID: 11683 RVA: 0x00092148 File Offset: 0x00090348
		internal VarVec KeyVars
		{
			get
			{
				return this.m_keys;
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06002DA4 RID: 11684 RVA: 0x00092150 File Offset: 0x00090350
		// (set) Token: 0x06002DA5 RID: 11685 RVA: 0x00092158 File Offset: 0x00090358
		internal bool NoKeys
		{
			get
			{
				return this.m_noKeys;
			}
			set
			{
				this.m_noKeys = value;
			}
		}

		// Token: 0x04000F41 RID: 3905
		private readonly VarVec m_keys;

		// Token: 0x04000F42 RID: 3906
		private bool m_noKeys;
	}
}
