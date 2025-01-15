using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B4B RID: 2891
	public class Rf2hFolderCollectionEnumerator : IEnumerator<Rf2hFolder>, IDisposable, IEnumerator
	{
		// Token: 0x06005B5D RID: 23389 RVA: 0x00178070 File Offset: 0x00176270
		internal Rf2hFolderCollectionEnumerator(Rf2hFolderCollection parent)
		{
			this.parent = parent;
			this.folderTypes = new Rf2hFolderType[parent.TypesToFolder.Keys.Count];
			parent.TypesToFolder.Keys.CopyTo(this.folderTypes, 0);
		}

		// Token: 0x17001611 RID: 5649
		// (get) Token: 0x06005B5E RID: 23390 RVA: 0x001780D8 File Offset: 0x001762D8
		public Rf2hFolder Current
		{
			get
			{
				object obj = this.lockObject;
				Rf2hFolder rf2hFolder;
				lock (obj)
				{
					if (this.current == null)
					{
						throw new InvalidOperationException();
					}
					rf2hFolder = this.current;
				}
				return rf2hFolder;
			}
		}

		// Token: 0x17001612 RID: 5650
		// (get) Token: 0x06005B5F RID: 23391 RVA: 0x00178128 File Offset: 0x00176328
		private object Current1
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x17001613 RID: 5651
		// (get) Token: 0x06005B60 RID: 23392 RVA: 0x00178130 File Offset: 0x00176330
		object IEnumerator.Current
		{
			get
			{
				return this.Current1;
			}
		}

		// Token: 0x06005B61 RID: 23393 RVA: 0x00178138 File Offset: 0x00176338
		public bool MoveNext()
		{
			object obj = this.lockObject;
			bool flag2;
			lock (obj)
			{
				if (this.moveNextMustReturnFalse)
				{
					flag2 = false;
				}
				else
				{
					if (this.collectionInvalidated)
					{
						throw new InvalidOperationException();
					}
					bool flag3 = this.folderTypeIndex == -1;
					if (!flag3)
					{
						this.folderInstanceIndex++;
						if (this.folderInstanceIndex >= this.parent[this.folderTypes[this.folderTypeIndex]].Count)
						{
							flag3 = true;
						}
					}
					if (flag3)
					{
						this.folderTypeIndex++;
						this.folderInstanceIndex = 0;
					}
					if (this.folderTypeIndex >= this.folderTypes.Length)
					{
						this.moveNextMustReturnFalse = true;
						flag2 = false;
					}
					else
					{
						this.current = this.parent[this.folderTypes[this.folderTypeIndex]][this.folderInstanceIndex];
						flag2 = true;
					}
				}
			}
			return flag2;
		}

		// Token: 0x06005B62 RID: 23394 RVA: 0x00003CAB File Offset: 0x00001EAB
		public void Reset()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005B63 RID: 23395 RVA: 0x00178230 File Offset: 0x00176430
		internal void Invalidate()
		{
			object obj = this.lockObject;
			lock (obj)
			{
				this.collectionInvalidated = true;
			}
		}

		// Token: 0x06005B64 RID: 23396 RVA: 0x00178274 File Offset: 0x00176474
		public void Dispose()
		{
			this.parent.EnumeratorDisposed(this);
		}

		// Token: 0x040047EB RID: 18411
		private Rf2hFolderCollection parent;

		// Token: 0x040047EC RID: 18412
		private Rf2hFolderType[] folderTypes;

		// Token: 0x040047ED RID: 18413
		private object lockObject = new object();

		// Token: 0x040047EE RID: 18414
		private bool collectionInvalidated;

		// Token: 0x040047EF RID: 18415
		private bool moveNextMustReturnFalse;

		// Token: 0x040047F0 RID: 18416
		private Rf2hFolder current;

		// Token: 0x040047F1 RID: 18417
		private int folderTypeIndex = -1;

		// Token: 0x040047F2 RID: 18418
		private int folderInstanceIndex = -1;
	}
}
