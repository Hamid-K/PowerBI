using System;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001EA RID: 490
	[Serializable]
	internal abstract class MDHLeafNode : MDHNode
	{
		// Token: 0x06000FD1 RID: 4049 RVA: 0x00035F45 File Offset: 0x00034145
		internal MDHLeafNode(int hashCode)
		{
			this._hashCode = new int?(hashCode);
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x00035F59 File Offset: 0x00034159
		internal MDHLeafNode()
		{
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x00035F61 File Offset: 0x00034161
		internal MDHLeafNode(MDHLeafNode node)
		{
			this._hashCode = node._hashCode;
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x00035F75 File Offset: 0x00034175
		// (set) Token: 0x06000FD5 RID: 4053 RVA: 0x00035FA0 File Offset: 0x000341A0
		internal int HashCode
		{
			get
			{
				if (this._hashCode == null)
				{
					this._hashCode = new int?(this.GetHashCode());
				}
				return this._hashCode.Value;
			}
			set
			{
				this._hashCode = new int?(value);
			}
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x00035FAE File Offset: 0x000341AE
		internal bool CompareHashCode(int hashCode)
		{
			return this.HashCode == hashCode;
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x00035FBC File Offset: 0x000341BC
		public override int GetHashCode()
		{
			ReleaseAssert.Fail("Type {0} did not override GetHashCode and HashCode was not initialized", new object[] { base.GetType().Name });
			return 0;
		}

		// Token: 0x06000FD8 RID: 4056
		internal abstract object Get(object key, int hashKey);

		// Token: 0x06000FD9 RID: 4057
		internal abstract object GetData(FixedDepthEnumeratorState state, int level);

		// Token: 0x06000FDA RID: 4058
		internal abstract bool CompareKey(object key);

		// Token: 0x06000FDB RID: 4059 RVA: 0x00035FEA File Offset: 0x000341EA
		protected void Clean()
		{
			this._hashCode = null;
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x000036A9 File Offset: 0x000018A9
		protected void Init()
		{
		}

		// Token: 0x04000AA8 RID: 2728
		[NonSerialized]
		private int? _hashCode;
	}
}
