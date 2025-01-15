using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003A5 RID: 933
	internal class ExtendedNodeInfo : NodeInfo
	{
		// Token: 0x06002D46 RID: 11590 RVA: 0x00091B74 File Offset: 0x0008FD74
		internal ExtendedNodeInfo(Command cmd)
			: base(cmd)
		{
			this.m_localDefinitions = cmd.CreateVarVec();
			this.m_definitions = cmd.CreateVarVec();
			this.m_nonNullableDefinitions = cmd.CreateVarVec();
			this.m_nonNullableVisibleDefinitions = cmd.CreateVarVec();
			this.m_keys = new KeyVec(cmd);
			this.m_minRows = RowCount.Zero;
			this.m_maxRows = RowCount.Unbounded;
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x00091BD4 File Offset: 0x0008FDD4
		internal override void Clear()
		{
			base.Clear();
			this.m_definitions.Clear();
			this.m_localDefinitions.Clear();
			this.m_nonNullableDefinitions.Clear();
			this.m_nonNullableVisibleDefinitions.Clear();
			this.m_keys.Clear();
			this.m_minRows = RowCount.Zero;
			this.m_maxRows = RowCount.Unbounded;
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x00091C2C File Offset: 0x0008FE2C
		internal override void ComputeHashValue(Command cmd, Node n)
		{
			base.ComputeHashValue(cmd, n);
			this.m_hashValue = (this.m_hashValue << 4) ^ NodeInfo.GetHashValue(this.Definitions);
			this.m_hashValue = (this.m_hashValue << 4) ^ NodeInfo.GetHashValue(this.Keys.KeyVars);
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06002D49 RID: 11593 RVA: 0x00091C7A File Offset: 0x0008FE7A
		internal VarVec LocalDefinitions
		{
			get
			{
				return this.m_localDefinitions;
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06002D4A RID: 11594 RVA: 0x00091C82 File Offset: 0x0008FE82
		internal VarVec Definitions
		{
			get
			{
				return this.m_definitions;
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06002D4B RID: 11595 RVA: 0x00091C8A File Offset: 0x0008FE8A
		internal KeyVec Keys
		{
			get
			{
				return this.m_keys;
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06002D4C RID: 11596 RVA: 0x00091C92 File Offset: 0x0008FE92
		internal VarVec NonNullableDefinitions
		{
			get
			{
				return this.m_nonNullableDefinitions;
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06002D4D RID: 11597 RVA: 0x00091C9A File Offset: 0x0008FE9A
		internal VarVec NonNullableVisibleDefinitions
		{
			get
			{
				return this.m_nonNullableVisibleDefinitions;
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06002D4E RID: 11598 RVA: 0x00091CA2 File Offset: 0x0008FEA2
		// (set) Token: 0x06002D4F RID: 11599 RVA: 0x00091CAA File Offset: 0x0008FEAA
		internal RowCount MinRows
		{
			get
			{
				return this.m_minRows;
			}
			set
			{
				this.m_minRows = value;
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06002D50 RID: 11600 RVA: 0x00091CB3 File Offset: 0x0008FEB3
		// (set) Token: 0x06002D51 RID: 11601 RVA: 0x00091CBB File Offset: 0x0008FEBB
		internal RowCount MaxRows
		{
			get
			{
				return this.m_maxRows;
			}
			set
			{
				this.m_maxRows = value;
			}
		}

		// Token: 0x06002D52 RID: 11602 RVA: 0x00091CC4 File Offset: 0x0008FEC4
		internal void SetRowCount(RowCount minRows, RowCount maxRows)
		{
			this.m_minRows = minRows;
			this.m_maxRows = maxRows;
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x00091CD4 File Offset: 0x0008FED4
		internal void InitRowCountFrom(ExtendedNodeInfo source)
		{
			this.m_minRows = source.m_minRows;
			this.m_maxRows = source.m_maxRows;
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x00091CEE File Offset: 0x0008FEEE
		[Conditional("DEBUG")]
		private void ValidateRowCount()
		{
		}

		// Token: 0x04000F26 RID: 3878
		private readonly VarVec m_localDefinitions;

		// Token: 0x04000F27 RID: 3879
		private readonly VarVec m_definitions;

		// Token: 0x04000F28 RID: 3880
		private readonly KeyVec m_keys;

		// Token: 0x04000F29 RID: 3881
		private readonly VarVec m_nonNullableDefinitions;

		// Token: 0x04000F2A RID: 3882
		private readonly VarVec m_nonNullableVisibleDefinitions;

		// Token: 0x04000F2B RID: 3883
		private RowCount m_minRows;

		// Token: 0x04000F2C RID: 3884
		private RowCount m_maxRows;
	}
}
