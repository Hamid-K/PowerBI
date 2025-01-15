using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200023D RID: 573
	internal class MDHEnumerator : IHashtableEnumerator, IEnumerator<ADMCacheItem>, IDisposable, IEnumerator
	{
		// Token: 0x06001303 RID: 4867 RVA: 0x0003AF44 File Offset: 0x00039144
		private IMDHEnumerationState GetEnumerationContextAtStackTop()
		{
			if (this._enumerationContext.Count > 0)
			{
				return this._enumerationContext.Peek();
			}
			return null;
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x0003AF64 File Offset: 0x00039164
		private static IMDHEnumerationState CreateContext(MDHNode node)
		{
			MDHDirectoryNode mdhdirectoryNode = null;
			if (node != null && node.NodeType == MDHNodeType.MDHDirectoryNode)
			{
				mdhdirectoryNode = (MDHDirectoryNode)node;
			}
			if (mdhdirectoryNode != null)
			{
				return new MDHDirectoryEnumerationState(mdhdirectoryNode);
			}
			return new MDHConflictingHashNodeEnumerationState((MDHConflictingHashNode)node);
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x0003AFA5 File Offset: 0x000391A5
		private bool UpdateCurrent(AMDHObjectNode node)
		{
			if (node == null)
			{
				this._current = null;
				return false;
			}
			this._current = node.GetCacheItem();
			return true;
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x0003AFC0 File Offset: 0x000391C0
		private AMDHObjectNode GetNextValidObjectNode()
		{
			AMDHObjectNode amdhobjectNode = this.GetNextObjectNode();
			while (amdhobjectNode != null && amdhobjectNode.GetCacheItem() == null)
			{
				amdhobjectNode = this.GetNextObjectNode();
			}
			return amdhobjectNode;
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x0003AFEC File Offset: 0x000391EC
		private void reset()
		{
			MDHDirectoryEnumerationState mdhdirectoryEnumerationState = new MDHDirectoryEnumerationState(this._startDirectory);
			this._enumerationContext.Clear();
			this._enumerationContext.Push(mdhdirectoryEnumerationState);
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x0003B022 File Offset: 0x00039222
		internal MDHEnumerator(MDHDirectoryNode root, MDHEnumerator.EnumerationScope scope)
		{
			this._startDirectory = root;
			this._scope = scope;
			this.reset();
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x0003B049 File Offset: 0x00039249
		internal MDHEnumerator(MDHDirectoryNode root)
		{
			this._startDirectory = root;
			this._scope = MDHEnumerator.EnumerationScope.ENTIRE_SUBTREE;
			this.reset();
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x0003B070 File Offset: 0x00039270
		internal AMDHObjectNode GetNextObjectNode()
		{
			for (IMDHEnumerationState imdhenumerationState = this.GetEnumerationContextAtStackTop(); imdhenumerationState != null; imdhenumerationState = this.GetEnumerationContextAtStackTop())
			{
				MDHNode mdhnode = imdhenumerationState.getNextNode();
				AMDHObjectNode amdhobjectNode = null;
				if (mdhnode != null && mdhnode.NodeType == MDHNodeType.MDHObjectNode)
				{
					amdhobjectNode = (AMDHObjectNode)mdhnode;
				}
				while (mdhnode != null && amdhobjectNode == null)
				{
					if (this._scope == MDHEnumerator.EnumerationScope.ENTIRE_SUBTREE || mdhnode.NodeType == MDHNodeType.MDHConflictingHashNode)
					{
						imdhenumerationState = MDHEnumerator.CreateContext(mdhnode);
						this._enumerationContext.Push(imdhenumerationState);
					}
					mdhnode = imdhenumerationState.getNextNode();
					amdhobjectNode = null;
					if (mdhnode != null && mdhnode.NodeType == MDHNodeType.MDHObjectNode)
					{
						amdhobjectNode = (AMDHObjectNode)mdhnode;
					}
				}
				if (mdhnode != null)
				{
					return amdhobjectNode;
				}
				this._enumerationContext.Pop();
			}
			return null;
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x0003B108 File Offset: 0x00039308
		internal MDHDirectoryNode GetNextDirectoryNode()
		{
			IMDHEnumerationState imdhenumerationState = this.GetEnumerationContextAtStackTop();
			while (imdhenumerationState != null)
			{
				MDHNode mdhnode = imdhenumerationState.getNextNode();
				while (mdhnode != null && mdhnode.NodeType != MDHNodeType.MDHDirectoryNode)
				{
					mdhnode = imdhenumerationState.getNextNode();
				}
				if (mdhnode == null)
				{
					return (MDHDirectoryNode)((MDHDirectoryEnumerationState)this._enumerationContext.Pop()).getContainer();
				}
				if (this._scope == MDHEnumerator.EnumerationScope.ENTIRE_SUBTREE)
				{
					imdhenumerationState = MDHEnumerator.CreateContext(mdhnode);
					this._enumerationContext.Push(imdhenumerationState);
				}
			}
			return null;
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x0003B17C File Offset: 0x0003937C
		public ADMCacheItem Current
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x0003B184 File Offset: 0x00039384
		public void Reset()
		{
			this.reset();
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x0003B18C File Offset: 0x0003938C
		public bool MoveNext()
		{
			AMDHObjectNode nextValidObjectNode = this.GetNextValidObjectNode();
			return this.UpdateCurrent(nextValidObjectNode);
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x0003B1A7 File Offset: 0x000393A7
		public ADMCacheItem GetNext()
		{
			if (this.MoveNext())
			{
				return this.Current;
			}
			return null;
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x0003B1B9 File Offset: 0x000393B9
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			throw new NotImplementedException();
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001311 RID: 4881 RVA: 0x0003B17C File Offset: 0x0003937C
		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x04000B6F RID: 2927
		private readonly MDHEnumerator.EnumerationScope _scope;

		// Token: 0x04000B70 RID: 2928
		private readonly MDHDirectoryNode _startDirectory;

		// Token: 0x04000B71 RID: 2929
		private Stack<IMDHEnumerationState> _enumerationContext = new Stack<IMDHEnumerationState>();

		// Token: 0x04000B72 RID: 2930
		private ADMCacheItem _current;

		// Token: 0x0200023E RID: 574
		internal enum EnumerationScope
		{
			// Token: 0x04000B74 RID: 2932
			ENTIRE_SUBTREE,
			// Token: 0x04000B75 RID: 2933
			DIRECTORY
		}
	}
}
