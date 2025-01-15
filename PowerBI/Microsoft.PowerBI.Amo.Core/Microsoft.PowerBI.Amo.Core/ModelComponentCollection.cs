using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.AnalysisServices.Core;
using Microsoft.AnalysisServices.Hosting;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000A2 RID: 162
	[Guid("74BB3312-550A-4f1f-BECF-80422AC163C6")]
	public abstract class ModelComponentCollection : IModelComponentCollection, ICollection, IEnumerable, IList, IOnDemandLoadableCollection
	{
		// Token: 0x170001C9 RID: 457
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x000264E9 File Offset: 0x000246E9
		int IList.Add(object value)
		{
			return this.Add(value as ModelComponent);
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x000264F7 File Offset: 0x000246F7
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x000264FF File Offset: 0x000246FF
		bool IList.Contains(object value)
		{
			return this.Contains(value as IModelComponent);
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0002650D File Offset: 0x0002470D
		int IList.IndexOf(object value)
		{
			return this.IndexOf(value as IModelComponent);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0002651B File Offset: 0x0002471B
		void IList.Insert(int index, object value)
		{
			this.Insert(index, value as ModelComponent);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0002652A File Offset: 0x0002472A
		void IList.Remove(object value)
		{
			this.Remove(value as IModelComponent);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00026538 File Offset: 0x00024738
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00026541 File Offset: 0x00024741
		protected ModelComponentCollection(IModelComponent parent)
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			this.parent = parent;
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x00026574 File Offset: 0x00024774
		public IModelComponent Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x0002657C File Offset: 0x0002477C
		public int Count
		{
			get
			{
				this.EnsureLoaded();
				return this.keys.Count;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060007CE RID: 1998
		protected abstract Type ItemsType { get; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x0002658F File Offset: 0x0002478F
		public bool IsSynchronized
		{
			get
			{
				this.EnsureLoaded();
				return this.keys.IsSynchronized && this.values.IsSynchronized;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x000265B1 File Offset: 0x000247B1
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x000265B4 File Offset: 0x000247B4
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x000265B7 File Offset: 0x000247B7
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060007D3 RID: 2003 RVA: 0x000265BC File Offset: 0x000247BC
		// (remove) Token: 0x060007D4 RID: 2004 RVA: 0x000265F4 File Offset: 0x000247F4
		public event CollectionChangeEventHandler CollectionChanging;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060007D5 RID: 2005 RVA: 0x0002662C File Offset: 0x0002482C
		// (remove) Token: 0x060007D6 RID: 2006 RVA: 0x00026664 File Offset: 0x00024864
		public event CollectionChangeEventHandler CollectionChanged;

		// Token: 0x060007D7 RID: 2007 RVA: 0x00026699 File Offset: 0x00024899
		internal virtual void OnItemAdding(IModelComponent item)
		{
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0002669B File Offset: 0x0002489B
		internal virtual void OnItemAdded(IModelComponent item)
		{
		}

		// Token: 0x170001D1 RID: 465
		protected IModelComponent this[int index]
		{
			get
			{
				this.EnsureLoaded();
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index", index, SR.Collections_IndexOutOfRangeException);
				}
				object obj = this.values[this.keys[index]];
				IModelComponent modelComponent = this.ResolvePotentialLink(obj);
				if (obj != modelComponent)
				{
					object obj2 = this.keys[index];
					this.values[obj2] = modelComponent;
				}
				return modelComponent;
			}
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00026718 File Offset: 0x00024918
		protected IModelComponent GetItem(string key, bool throwIfNotFound, string keyPropertyName)
		{
			if (key == null)
			{
				if (throwIfNotFound)
				{
					throw Utils.CreateItemNotFoundException(key, keyPropertyName, this.ItemsType.Name);
				}
				return null;
			}
			else
			{
				this.EnsureLoaded();
				string text = key.ToLower(CultureInfo.InvariantCulture);
				object obj = this.values[text];
				if (obj != null)
				{
					IModelComponent modelComponent = this.ResolvePotentialLink(obj);
					if (obj != modelComponent)
					{
						this.keys.IndexOf(text);
						this.values[text] = modelComponent;
					}
					return modelComponent;
				}
				if (throwIfNotFound)
				{
					throw Utils.CreateItemNotFoundException(key, keyPropertyName, this.ItemsType.Name);
				}
				return null;
			}
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x000267A4 File Offset: 0x000249A4
		public virtual bool CanAdd(ModelComponent item, out string error)
		{
			if (item == null)
			{
				error = SR.Collections_CannotAddANullItem;
				return false;
			}
			if (!this.ItemsType.IsAssignableFrom(item.GetType()))
			{
				error = SR.Collections_AddingObjectOfInvalidType(item.GetType().Name);
				return false;
			}
			if (this.Contains(item))
			{
				error = SR.Collections_ItemAlreadyExists;
				return false;
			}
			if (item.KeyForCollection == null)
			{
				error = SR.Collections_KeyPropertyNotDefined;
				return false;
			}
			if (this.Contains(item.KeyForCollection))
			{
				error = SR.Collections_KeyIsNotUniqueException(item.KeyForCollection, this.ItemsType.Name);
				return false;
			}
			error = null;
			return true;
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00026834 File Offset: 0x00024A34
		[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736AD6E5F9586BAC2D531EABC3ACC666C2F8EC879FA94F8F7B0327D2FF2ED523448F83C3D5C5DD2DFC7BC99C5286B2C125117BF5CBE242B9D41750732B2BDFFE649C6EFB8E5526D526FDD130095ECDB7BF210809C6CDAD8824FAA9AC0310AC3CBA2AA0523567B2DFA7FE250B30FACBD62D4EC99B94AC47C7D3B28F1F6E4C8")]
		protected IModelComponent AddNew(string name, string key, Type type)
		{
			if (this.Contains(key))
			{
				throw new InvalidOperationException(SR.Collections_KeyIsNotUniqueException(key, this.ItemsType.Name));
			}
			IModelComponent modelComponent = (IModelComponent)Activator.CreateInstance(type, new object[] { name, key });
			this.Add(modelComponent as ModelComponent);
			return modelComponent;
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00026889 File Offset: 0x00024A89
		[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736AD6E5F9586BAC2D531EABC3ACC666C2F8EC879FA94F8F7B0327D2FF2ED523448F83C3D5C5DD2DFC7BC99C5286B2C125117BF5CBE242B9D41750732B2BDFFE649C6EFB8E5526D526FDD130095ECDB7BF210809C6CDAD8824FAA9AC0310AC3CBA2AA0523567B2DFA7FE250B30FACBD62D4EC99B94AC47C7D3B28F1F6E4C8")]
		protected IModelComponent AddNew(string key, Type type)
		{
			return this.AddNew(key, key, type);
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00026894 File Offset: 0x00024A94
		protected internal virtual int Add(ModelComponent item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			return this.Add(item.KeyForCollection, item);
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x000268B1 File Offset: 0x00024AB1
		protected internal virtual int Add(ModelComponent item, bool updateDependents)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			return this.Add(item.KeyForCollection, item, updateDependents);
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x000268CF File Offset: 0x00024ACF
		protected int Add(string key, IModelComponent item, bool updateDependents)
		{
			this.Insert(item, key, this.Count, updateDependents);
			return this.Count - 1;
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x000268E8 File Offset: 0x00024AE8
		protected int Add(string key, IModelComponent item)
		{
			this.Insert(item, key, this.Count);
			return this.Count - 1;
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00026900 File Offset: 0x00024B00
		protected internal virtual void Insert(int index, ModelComponent item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			this.Insert(item, item.KeyForCollection, index);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0002691E File Offset: 0x00024B1E
		protected void Insert(IModelComponent item, string key, int index)
		{
			this.Insert(item, key, index, true);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0002692C File Offset: 0x00024B2C
		protected void Insert(IModelComponent item, string key, int index, bool updateDependents)
		{
			this.EnsureLoaded();
			if (index < 0 || index > this.Count)
			{
				throw new ArgumentOutOfRangeException("index", index, SR.Collections_IndexOutOfRangeException);
			}
			string text = null;
			if (!this.CanAdd((ModelComponent)item, out text))
			{
				throw new InvalidOperationException(text);
			}
			int count = this.Count;
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			string text2 = key.ToLower(CultureInfo.InvariantCulture);
			if (this.Contains(text2))
			{
				throw new InvalidOperationException(SR.Collections_KeyIsNotUniqueException(key, this.ItemsType.Name));
			}
			this.OnItemAdding(item);
			if (this.CollectionChanging != null)
			{
				this.CollectionChanging(this, new AddEventArgs(item, index));
			}
			IModelComponentCollection owningCollection = item.OwningCollection;
			if (item.OwningCollection != null)
			{
				item.OwningCollection.Remove(item);
			}
			item.OwningCollection = this;
			this.values.Add(text2, item);
			this.keys.Insert(index, text2);
			bool flag = true;
			IHostMaterializationService hostMaterializationService = (IHostMaterializationService)this.GetHostService(typeof(IHostMaterializationService));
			if (hostMaterializationService != null)
			{
				try
				{
					hostMaterializationService.MaterializeComponent(item, this);
					flag = !hostMaterializationService.SitingBlocked;
				}
				catch
				{
					this.values.Remove(text2);
					this.keys.RemoveAt(index);
					item.OwningCollection = null;
					if (owningCollection != null && owningCollection is ModelComponentCollection && item is ModelComponent)
					{
						((ModelComponentCollection)owningCollection).Add((ModelComponent)item);
					}
					throw;
				}
			}
			IModelComponent modelComponent = item.Parent;
			if (flag && item.Site == null && modelComponent != null)
			{
				ISite site = modelComponent.Site;
				if (site != null && site.Container != null)
				{
					try
					{
						site.Container.Add(item);
					}
					catch
					{
						this.values.Remove(text2);
						this.keys.RemoveAt(index);
						item.OwningCollection = null;
						if (owningCollection != null && owningCollection is ModelComponentCollection && item is ModelComponent)
						{
							((ModelComponentCollection)owningCollection).Add((ModelComponent)item);
						}
						throw;
					}
				}
			}
			if (updateDependents)
			{
				ModelComponent modelComponent2 = item as ModelComponent;
				if (modelComponent2 != null)
				{
					modelComponent2.AfterInsert(index);
				}
			}
			if (item.Site != null)
			{
				((ModelComponent)item).AddToContainer(item.Site.Container);
			}
			this.OnItemAdded(item);
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged(this, new AddEventArgs(item, index));
			}
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00026B9C File Offset: 0x00024D9C
		private IModelComponent ResolvePotentialLink(object obj)
		{
			return (IModelComponent)obj;
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00026BA4 File Offset: 0x00024DA4
		internal IModelComponent Move(int fromIndex, int toIndex)
		{
			this.EnsureLoaded();
			int count = this.Count;
			if (fromIndex < 0 || fromIndex >= count)
			{
				throw new ArgumentOutOfRangeException("fromIndex");
			}
			if (toIndex < 0 || toIndex >= count)
			{
				throw new ArgumentOutOfRangeException("toIndex");
			}
			object obj = this.values[this.keys[fromIndex]];
			if (fromIndex == toIndex)
			{
				return (IModelComponent)obj;
			}
			if (this.CollectionChanging != null)
			{
				this.CollectionChanging(this, new MoveEventArgs(obj, fromIndex, toIndex));
			}
			string text = this.keys[fromIndex];
			if (fromIndex < toIndex)
			{
				for (int i = fromIndex + 1; i <= toIndex; i++)
				{
					this.keys[i - 1] = this.keys[i];
				}
			}
			else
			{
				for (int j = fromIndex - 1; j >= toIndex; j--)
				{
					this.keys[j + 1] = this.keys[j];
				}
			}
			this.keys[toIndex] = text;
			ModelComponent modelComponent = obj as ModelComponent;
			if (modelComponent != null)
			{
				modelComponent.AfterMove(fromIndex, toIndex);
			}
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged(this, new MoveEventArgs(obj, fromIndex, toIndex));
			}
			return (IModelComponent)obj;
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00026CD3 File Offset: 0x00024ED3
		internal IModelComponent Move(string key, int toIndex)
		{
			this.EnsureLoaded();
			return this.Move(this.keys.IndexOf(key.ToLower(CultureInfo.InvariantCulture)), toIndex);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00026CF8 File Offset: 0x00024EF8
		internal void Move(IModelComponent item, int toIndex)
		{
			this.EnsureLoaded();
			this.Move(this.IndexOf(item), toIndex);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00026D0F File Offset: 0x00024F0F
		public void RemoveAt(int index)
		{
			this.RemoveAt(index, true);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00026D1C File Offset: 0x00024F1C
		public void RemoveAt(int index, bool cleanUp)
		{
			this.EnsureLoaded();
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			string text = this.keys[index];
			object obj = this.values[text];
			if (this.CollectionChanging != null)
			{
				this.CollectionChanging(this, new RemoveEventArgs(obj, index));
			}
			ModelComponent modelComponent = obj as ModelComponent;
			if (modelComponent != null)
			{
				modelComponent.BeforeRemove(cleanUp);
			}
			if (obj is IModelComponent)
			{
				ISite site = ((IModelComponent)obj).Site;
				IContainer container = ((site != null) ? site.Container : null);
				ISite site2 = ((this.parent != null) ? this.parent.Site : null);
				IContainer container2 = ((site2 != null) ? site2.Container : null);
				if (container != null && container == container2)
				{
					container.Remove((IComponent)obj);
				}
			}
			this.values.Remove(text);
			if (obj is IModelComponent)
			{
				((IModelComponent)obj).OwningCollection = null;
			}
			IHostMaterializationService hostMaterializationService = (IHostMaterializationService)this.GetHostService(typeof(IHostMaterializationService));
			if (hostMaterializationService != null)
			{
				hostMaterializationService.DematerializeComponent((IComponent)obj, this);
			}
			this.keys.RemoveAt(index);
			if (cleanUp && modelComponent != null)
			{
				modelComponent.AfterRemove(this);
			}
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged(this, new RemoveEventArgs(obj, index));
			}
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00026E6C File Offset: 0x0002506C
		internal void Remove(string key, bool cleanUp)
		{
			string text = key.ToLower(CultureInfo.InvariantCulture);
			this.EnsureLoaded();
			int num = this.keys.IndexOf(text);
			if (num == -1)
			{
				return;
			}
			this.RemoveAt(num, cleanUp);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00026EA5 File Offset: 0x000250A5
		internal void Remove(string key, bool cleanUp, bool throwIfNotFound)
		{
			this.Remove(key, cleanUp);
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00026EB0 File Offset: 0x000250B0
		internal void Remove(IModelComponent item, bool cleanUp)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			this.EnsureLoaded();
			int num = this.IndexOf(item);
			if (num == -1)
			{
				return;
			}
			this.RemoveAt(num, cleanUp);
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00026EE6 File Offset: 0x000250E6
		protected internal void Remove(IModelComponent item)
		{
			this.Remove(item, true);
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00026EF0 File Offset: 0x000250F0
		void IModelComponentCollection.Remove(IModelComponent item)
		{
			this.Remove(item);
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00026EF9 File Offset: 0x000250F9
		void IModelComponentCollection.Remove(IModelComponent item, bool cleanUp)
		{
			this.Remove(item, cleanUp);
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00026F03 File Offset: 0x00025103
		public void Clear()
		{
			this.Clear(true);
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00026F0C File Offset: 0x0002510C
		internal void Clear(bool cleanUp)
		{
			this.EnsureLoaded();
			for (int i = this.Count - 1; i >= 0; i--)
			{
				this.RemoveAt(i, cleanUp);
			}
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00026F3A File Offset: 0x0002513A
		protected int IndexOf(string key)
		{
			this.EnsureLoaded();
			return this.keys.IndexOf(key.ToLower(CultureInfo.InvariantCulture));
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00026F58 File Offset: 0x00025158
		protected int IndexOf(IModelComponent item)
		{
			this.EnsureLoaded();
			foreach (object obj in this.values)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (dictionaryEntry.Value == item)
				{
					return this.keys.IndexOf((string)dictionaryEntry.Key);
				}
			}
			return -1;
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00026FD8 File Offset: 0x000251D8
		protected bool Contains(string key)
		{
			this.EnsureLoaded();
			return key != null && this.values.Contains(key.ToLower(CultureInfo.InvariantCulture));
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00026FFB File Offset: 0x000251FB
		protected bool Contains(IModelComponent item)
		{
			this.EnsureLoaded();
			return this.values.ContainsValue(item);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0002700F File Offset: 0x0002520F
		bool IModelComponentCollection.Contains(IModelComponent item)
		{
			return this.Contains(item);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00027018 File Offset: 0x00025218
		public IEnumerator GetEnumerator()
		{
			this.EnsureLoaded();
			return new ModelComponentCollection.ModelComponentEnumerator(this.keys, this.values);
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00027034 File Offset: 0x00025234
		protected internal void ChangeKey(string oldKey, string newKey)
		{
			this.EnsureLoaded();
			string text = oldKey.ToLower(CultureInfo.InvariantCulture);
			string text2 = newKey.ToLower(CultureInfo.InvariantCulture);
			int num = this.keys.IndexOf(text);
			object obj = this.values[text];
			object item = this.GetItem(newKey, false, null);
			if (num == -1 || obj == null)
			{
				throw new ArgumentOutOfRangeException("oldKey");
			}
			if (item != null && item != obj)
			{
				throw new InvalidOperationException(SR.Collections_KeyIsNotUniqueException(newKey, this.ItemsType.Name));
			}
			this.keys[num] = text2;
			this.values.Remove(text);
			this.values.Add(text2, obj);
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x000270DC File Offset: 0x000252DC
		internal object GetHostService(Type serviceType)
		{
			object obj = null;
			for (IModelComponent modelComponent = this.Parent; modelComponent != null; modelComponent = modelComponent.Parent)
			{
				IHostableComponent hostableComponent = modelComponent as IHostableComponent;
				if (modelComponent != null)
				{
					IServiceProvider host = hostableComponent.Host;
					if (host != null)
					{
						obj = host.GetService(serviceType);
						if (obj != null)
						{
							break;
						}
					}
				}
			}
			if (obj == null)
			{
				return null;
			}
			return obj;
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00027124 File Offset: 0x00025324
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (array.Rank != 1 || this.Count > array.Length - index)
			{
				throw new ArgumentException(SR.Collections_InvalidIndex);
			}
			this.EnsureLoaded();
			int i = 0;
			int count = this.Count;
			while (i < count)
			{
				array.SetValue(this[i], index);
				i++;
				index++;
			}
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x000271A0 File Offset: 0x000253A0
		internal void CopyTo(ModelComponentCollection dest)
		{
			this.EnsureLoaded();
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			if (dest == this)
			{
				return;
			}
			if (dest.GetType() != base.GetType())
			{
				throw new InvalidOperationException();
			}
			dest.Clear(false);
			int i = 0;
			int count = this.Count;
			while (i < count)
			{
				ICloneable cloneable = this[i] as ICloneable;
				if (cloneable != null)
				{
					dest.Add(cloneable.Clone() as ModelComponent);
				}
				i++;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060007FD RID: 2045 RVA: 0x0002721B File Offset: 0x0002541B
		protected IHostOnDemandLoader DemandLoadingService
		{
			get
			{
				return this.GetHostService(typeof(IHostOnDemandLoader)) as IHostOnDemandLoader;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x00027232 File Offset: 0x00025432
		// (set) Token: 0x060007FF RID: 2047 RVA: 0x00027244 File Offset: 0x00025444
		bool IOnDemandLoadableCollection.Loaded
		{
			get
			{
				return !this.Preloadable || this.loaded;
			}
			set
			{
				if (this.Preloadable)
				{
					this.loaded = value;
					return;
				}
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0002725C File Offset: 0x0002545C
		int IOnDemandLoadableCollection.BlockOnDemandLoad(bool block)
		{
			int num;
			if (!block)
			{
				num = this.blockCount - 1;
				this.blockCount = num;
				return num;
			}
			num = this.blockCount + 1;
			this.blockCount = num;
			return num;
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00027290 File Offset: 0x00025490
		protected void EnsureLoaded()
		{
			if (((IOnDemandLoadableCollection)this).Loaded || this.blockCount != 0)
			{
				return;
			}
			IHostOnDemandLoader demandLoadingService = this.DemandLoadingService;
			if (demandLoadingService != null)
			{
				demandLoadingService.LoadCollectionOnDemand(this, this.Parent);
				return;
			}
			this.loaded = true;
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x000272CE File Offset: 0x000254CE
		protected virtual bool Preloadable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000499 RID: 1177
		private IModelComponent parent;

		// Token: 0x0400049A RID: 1178
		private StringCollection keys = new StringCollection();

		// Token: 0x0400049B RID: 1179
		private Hashtable values = new Hashtable();

		// Token: 0x0400049C RID: 1180
		private bool loaded;

		// Token: 0x0400049D RID: 1181
		private int blockCount;

		// Token: 0x0200019A RID: 410
		private sealed class ModelComponentEnumerator : IEnumerator
		{
			// Token: 0x06001303 RID: 4867 RVA: 0x00043113 File Offset: 0x00041313
			internal ModelComponentEnumerator(StringCollection keys, Hashtable values)
			{
				this.keys = keys.GetEnumerator();
				this.values = values;
			}

			// Token: 0x17000629 RID: 1577
			// (get) Token: 0x06001304 RID: 4868 RVA: 0x0004312E File Offset: 0x0004132E
			public object Current
			{
				get
				{
					return this.values[this.keys.Current];
				}
			}

			// Token: 0x06001305 RID: 4869 RVA: 0x00043146 File Offset: 0x00041346
			public bool MoveNext()
			{
				return this.keys.MoveNext();
			}

			// Token: 0x06001306 RID: 4870 RVA: 0x00043153 File Offset: 0x00041353
			public void Reset()
			{
				this.keys.Reset();
			}

			// Token: 0x04000C43 RID: 3139
			private StringEnumerator keys;

			// Token: 0x04000C44 RID: 3140
			private Hashtable values;
		}
	}
}
