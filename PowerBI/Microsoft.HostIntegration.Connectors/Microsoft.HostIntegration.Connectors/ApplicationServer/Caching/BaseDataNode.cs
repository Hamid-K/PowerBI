using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001FD RID: 509
	internal sealed class BaseDataNode : MDHLeafNode, IBaseDataNode
	{
		// Token: 0x06001091 RID: 4241 RVA: 0x000374B6 File Offset: 0x000356B6
		internal BaseDataNode(object key, object data)
			: this(key.GetHashCode(), key, data)
		{
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x000374C6 File Offset: 0x000356C6
		internal BaseDataNode(int hashCode, object key, object data)
			: base(hashCode)
		{
			this._key = key;
			this._data = data;
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x000374DD File Offset: 0x000356DD
		internal override object Get(object searchKey, int hashCode)
		{
			if (this.CompareKey(hashCode, searchKey))
			{
				return this._data;
			}
			return null;
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001094 RID: 4244 RVA: 0x00002B16 File Offset: 0x00000D16
		internal override int Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x000374F1 File Offset: 0x000356F1
		internal override bool GetBatch(IScanner info, BaseEnumeratorState state)
		{
			return info.Scan(this._data);
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001096 RID: 4246 RVA: 0x00036AA6 File Offset: 0x00034CA6
		internal override MDHNodeType NodeType
		{
			get
			{
				return MDHNodeType.BaseDataNode;
			}
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x00002B16 File Offset: 0x00000D16
		internal override bool CanNodeBeMoved()
		{
			return true;
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x000036A9 File Offset: 0x000018A9
		internal override void VerifyState()
		{
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x000374FF File Offset: 0x000356FF
		internal override object GetData(FixedDepthEnumeratorState state, int level)
		{
			return this._data;
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x00037507 File Offset: 0x00035707
		internal bool CompareKey(int hashCode, object searchKey)
		{
			return base.HashCode == hashCode && searchKey.Equals(this._key);
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x00037523 File Offset: 0x00035723
		internal override bool CompareKey(object searchKey)
		{
			return this._key.Equals(searchKey);
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x0600109C RID: 4252 RVA: 0x00037531 File Offset: 0x00035731
		public object Key
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x0600109D RID: 4253 RVA: 0x000374FF File Offset: 0x000356FF
		public object Data
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x04000ACD RID: 2765
		private readonly object _data;

		// Token: 0x04000ACE RID: 2766
		private readonly object _key;
	}
}
