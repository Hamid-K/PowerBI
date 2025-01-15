using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.HostIntegration.MqClient.StrictResources.ClassLibrary;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B4A RID: 2890
	public class Rf2hFolderCollection : IEnumerable<Rf2hFolder>, IEnumerable
	{
		// Token: 0x1700160E RID: 5646
		// (get) Token: 0x06005B4E RID: 23374 RVA: 0x00177A97 File Offset: 0x00175C97
		// (set) Token: 0x06005B4F RID: 23375 RVA: 0x00177A9F File Offset: 0x00175C9F
		internal Dictionary<Rf2hFolderType, List<Rf2hFolder>> TypesToFolder { get; private set; }

		// Token: 0x1700160F RID: 5647
		// (get) Token: 0x06005B50 RID: 23376 RVA: 0x00177AA8 File Offset: 0x00175CA8
		public int Count
		{
			get
			{
				object obj = this.lockObject;
				int num;
				lock (obj)
				{
					num = this.count;
				}
				return num;
			}
		}

		// Token: 0x06005B51 RID: 23377 RVA: 0x00177AEC File Offset: 0x00175CEC
		internal Rf2hFolderCollection(RulesAndFormattingVersion2Header parentHeader)
		{
			this.parent = parentHeader;
			this.TypesToFolder = new Dictionary<Rf2hFolderType, List<Rf2hFolder>>();
		}

		// Token: 0x06005B52 RID: 23378 RVA: 0x00177B1C File Offset: 0x00175D1C
		public IEnumerator<Rf2hFolder> GetEnumerator()
		{
			Rf2hFolderCollectionEnumerator rf2hFolderCollectionEnumerator = new Rf2hFolderCollectionEnumerator(this);
			object obj = this.lockObject;
			lock (obj)
			{
				this.enumerators.Add(rf2hFolderCollectionEnumerator);
			}
			return rf2hFolderCollectionEnumerator;
		}

		// Token: 0x06005B53 RID: 23379 RVA: 0x00177B6C File Offset: 0x00175D6C
		private IEnumerator GetEnumerator1()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06005B54 RID: 23380 RVA: 0x00177B74 File Offset: 0x00175D74
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator1();
		}

		// Token: 0x06005B55 RID: 23381 RVA: 0x00177B7C File Offset: 0x00175D7C
		public void Add(Rf2hFolder newFolder)
		{
			if (newFolder == null)
			{
				throw new ArgumentNullException("newFolder");
			}
			if (newFolder.Parent == null)
			{
				this.parent.ChangesMade(false);
				object obj = this.lockObject;
				lock (obj)
				{
					this.Invalidate();
					List<Rf2hFolder> list = null;
					if (!this.TryGetValue(newFolder.FolderType, out list))
					{
						list = new List<Rf2hFolder>();
						this.TypesToFolder.Add(newFolder.FolderType, list);
					}
					list.Add(newFolder);
					newFolder.Parent = this;
					this.count++;
				}
				return;
			}
			if (newFolder.Parent == this)
			{
				throw new CustomMqClientException(SR.FolderAlreadyInHeader);
			}
			throw new CustomMqClientException(SR.FolderOneHeader);
		}

		// Token: 0x06005B56 RID: 23382 RVA: 0x00177C44 File Offset: 0x00175E44
		public bool Remove(Rf2hFolder oldFolder)
		{
			if (oldFolder == null)
			{
				throw new ArgumentNullException("oldFolder");
			}
			if (oldFolder.Parent == null)
			{
				throw new CustomMqClientException(SR.FolderNoHeader);
			}
			if (oldFolder.Parent != this)
			{
				throw new CustomMqClientException(SR.FolderNotInHeader);
			}
			object obj = this.lockObject;
			bool flag2;
			lock (obj)
			{
				List<Rf2hFolder> list;
				if (!this.TypesToFolder.TryGetValue(oldFolder.FolderType, out list))
				{
					throw new InvalidOperationException("BUGBUG: couldn't find folder corresponding to the type to remove");
				}
				this.Invalidate();
				if (!list.Remove(oldFolder))
				{
					throw new InvalidOperationException("BUGBUG: couldn't find folder to remove");
				}
				oldFolder.Parent = null;
				this.count--;
				if (list.Count == 0)
				{
					this.TypesToFolder.Remove(oldFolder.FolderType);
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06005B57 RID: 23383 RVA: 0x00177D20 File Offset: 0x00175F20
		public void Clear()
		{
			object obj = this.lockObject;
			lock (obj)
			{
				if (this.TypesToFolder.Count != 0)
				{
					this.Invalidate();
					foreach (KeyValuePair<Rf2hFolderType, List<Rf2hFolder>> keyValuePair in this.TypesToFolder)
					{
						foreach (Rf2hFolder rf2hFolder in keyValuePair.Value)
						{
							rf2hFolder.Parent = null;
						}
					}
					this.TypesToFolder.Clear();
				}
				this.count = 0;
			}
		}

		// Token: 0x17001610 RID: 5648
		public List<Rf2hFolder> this[Rf2hFolderType folderType]
		{
			get
			{
				object obj = this.lockObject;
				List<Rf2hFolder> list2;
				lock (obj)
				{
					List<Rf2hFolder> list;
					if (!this.TypesToFolder.TryGetValue(folderType, out list))
					{
						throw new ArgumentException("folderType");
					}
					list2 = list;
				}
				return list2;
			}
		}

		// Token: 0x06005B59 RID: 23385 RVA: 0x00177E54 File Offset: 0x00176054
		public bool TryGetValue(Rf2hFolderType folderType, out List<Rf2hFolder> folders)
		{
			return this.TypesToFolder.TryGetValue(folderType, out folders);
		}

		// Token: 0x06005B5A RID: 23386 RVA: 0x00177E64 File Offset: 0x00176064
		internal void EnumeratorDisposed(Rf2hFolderCollectionEnumerator enumerator)
		{
			object obj = this.lockObject;
			lock (obj)
			{
				this.enumerators.Remove(enumerator);
			}
		}

		// Token: 0x06005B5B RID: 23387 RVA: 0x00177EAC File Offset: 0x001760AC
		private void Invalidate()
		{
			foreach (Rf2hFolderCollectionEnumerator rf2hFolderCollectionEnumerator in this.enumerators)
			{
				rf2hFolderCollectionEnumerator.Invalidate();
			}
		}

		// Token: 0x06005B5C RID: 23388 RVA: 0x00177EFC File Offset: 0x001760FC
		internal Rf2hFolderWithFieldsAndProperties GetPropertiesFolder(Rf2hFolderType folderType, string folderTag, bool createIfNeeded)
		{
			bool flag = false;
			object obj = this.lockObject;
			Rf2hFolderWithFieldsAndProperties rf2hFolderWithFieldsAndProperties3;
			lock (obj)
			{
				List<Rf2hFolder> list;
				if (!this.TryGetValue(folderType, out list))
				{
					if (!createIfNeeded)
					{
						return null;
					}
					list = new List<Rf2hFolder>();
					this.TypesToFolder.Add(folderType, list);
				}
				if (list.Count == 0)
				{
					if (!createIfNeeded)
					{
						return null;
					}
					switch (folderType)
					{
					case Rf2hFolderType.Mq_Usr:
						list.Add(new Rf2hMqUsrFolder());
						flag = true;
						break;
					case Rf2hFolderType.Usr:
						list.Add(new Rf2hUsrFolder());
						flag = true;
						break;
					case Rf2hFolderType.Properties:
						break;
					default:
						throw new Exception("BUG BUG: insert into folder type wrong");
					}
				}
				Rf2hFolderWithFieldsAndProperties rf2hFolderWithFieldsAndProperties = null;
				if (folderType != Rf2hFolderType.Properties)
				{
					rf2hFolderWithFieldsAndProperties = list[0] as Rf2hFolderWithFieldsAndProperties;
				}
				else
				{
					foreach (Rf2hFolder rf2hFolder in list)
					{
						Rf2hFolderWithFieldsAndProperties rf2hFolderWithFieldsAndProperties2 = rf2hFolder as Rf2hFolderWithFieldsAndProperties;
						if (rf2hFolderWithFieldsAndProperties2.FolderTag == folderTag)
						{
							rf2hFolderWithFieldsAndProperties = rf2hFolderWithFieldsAndProperties2;
							break;
						}
					}
					if (rf2hFolderWithFieldsAndProperties == null)
					{
						if (!createIfNeeded)
						{
							return null;
						}
						rf2hFolderWithFieldsAndProperties = new Rf2hPropertiesFolder(folderTag, null);
						list.Add(rf2hFolderWithFieldsAndProperties);
						flag = true;
					}
				}
				if (flag)
				{
					this.count++;
					this.Invalidate();
				}
				rf2hFolderWithFieldsAndProperties3 = rf2hFolderWithFieldsAndProperties;
			}
			return rf2hFolderWithFieldsAndProperties3;
		}

		// Token: 0x040047E6 RID: 18406
		private object lockObject = new object();

		// Token: 0x040047E7 RID: 18407
		private List<Rf2hFolderCollectionEnumerator> enumerators = new List<Rf2hFolderCollectionEnumerator>();

		// Token: 0x040047E9 RID: 18409
		private int count;

		// Token: 0x040047EA RID: 18410
		private RulesAndFormattingVersion2Header parent;
	}
}
