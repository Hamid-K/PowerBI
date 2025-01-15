using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004CC RID: 1228
	public abstract class ItemCollection : ReadOnlyMetadataCollection<GlobalItem>
	{
		// Token: 0x06003CC7 RID: 15559 RVA: 0x000C9424 File Offset: 0x000C7624
		internal ItemCollection()
		{
		}

		// Token: 0x06003CC8 RID: 15560 RVA: 0x000C942C File Offset: 0x000C762C
		internal ItemCollection(DataSpace dataspace)
			: base(new MetadataCollection<GlobalItem>())
		{
			this._space = dataspace;
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06003CC9 RID: 15561 RVA: 0x000C9440 File Offset: 0x000C7640
		public DataSpace DataSpace
		{
			get
			{
				return this._space;
			}
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06003CCA RID: 15562 RVA: 0x000C9448 File Offset: 0x000C7648
		internal Dictionary<string, ReadOnlyCollection<EdmFunction>> FunctionLookUpTable
		{
			get
			{
				if (this._functionLookUpTable == null)
				{
					Dictionary<string, ReadOnlyCollection<EdmFunction>> dictionary = ItemCollection.PopulateFunctionLookUpTable(this);
					Interlocked.CompareExchange<Dictionary<string, ReadOnlyCollection<EdmFunction>>>(ref this._functionLookUpTable, dictionary, null);
				}
				return this._functionLookUpTable;
			}
		}

		// Token: 0x06003CCB RID: 15563 RVA: 0x000C9478 File Offset: 0x000C7678
		internal void AddInternal(GlobalItem item)
		{
			base.Source.Add(item);
		}

		// Token: 0x06003CCC RID: 15564 RVA: 0x000C9486 File Offset: 0x000C7686
		internal void AddRange(List<GlobalItem> items)
		{
			base.Source.AddRange(items);
		}

		// Token: 0x06003CCD RID: 15565 RVA: 0x000C9494 File Offset: 0x000C7694
		public T GetItem<T>(string identity) where T : GlobalItem
		{
			return this.GetItem<T>(identity, false);
		}

		// Token: 0x06003CCE RID: 15566 RVA: 0x000C949E File Offset: 0x000C769E
		public bool TryGetItem<T>(string identity, out T item) where T : GlobalItem
		{
			return this.TryGetItem<T>(identity, false, out item);
		}

		// Token: 0x06003CCF RID: 15567 RVA: 0x000C94AC File Offset: 0x000C76AC
		public bool TryGetItem<T>(string identity, bool ignoreCase, out T item) where T : GlobalItem
		{
			GlobalItem globalItem = null;
			this.TryGetValue(identity, ignoreCase, out globalItem);
			item = globalItem as T;
			return item != null;
		}

		// Token: 0x06003CD0 RID: 15568 RVA: 0x000C94E8 File Offset: 0x000C76E8
		public T GetItem<T>(string identity, bool ignoreCase) where T : GlobalItem
		{
			T t;
			if (this.TryGetItem<T>(identity, ignoreCase, out t))
			{
				return t;
			}
			throw new ArgumentException(Strings.ItemInvalidIdentity(identity), "identity");
		}

		// Token: 0x06003CD1 RID: 15569 RVA: 0x000C9514 File Offset: 0x000C7714
		public virtual ReadOnlyCollection<T> GetItems<T>() where T : GlobalItem
		{
			Memoizer<Type, ICollection> itemsCache = this._itemsCache;
			if (this._itemsCache == null || this._itemCount != base.Count)
			{
				Memoizer<Type, ICollection> memoizer = new Memoizer<Type, ICollection>(new Func<Type, ICollection>(this.InternalGetItems), null);
				Interlocked.CompareExchange<Memoizer<Type, ICollection>>(ref this._itemsCache, memoizer, itemsCache);
				this._itemCount = base.Count;
			}
			return this._itemsCache.Evaluate(typeof(T)) as ReadOnlyCollection<T>;
		}

		// Token: 0x06003CD2 RID: 15570 RVA: 0x000C9585 File Offset: 0x000C7785
		internal ICollection InternalGetItems(Type type)
		{
			return typeof(ItemCollection).GetOnlyDeclaredMethod("GenericGetItems").MakeGenericMethod(new Type[] { type }).Invoke(null, new object[] { this }) as ICollection;
		}

		// Token: 0x06003CD3 RID: 15571 RVA: 0x000C95C0 File Offset: 0x000C77C0
		private static ReadOnlyCollection<TItem> GenericGetItems<TItem>(ItemCollection collection) where TItem : GlobalItem
		{
			List<TItem> list = new List<TItem>();
			foreach (GlobalItem globalItem in collection)
			{
				TItem titem = globalItem as TItem;
				if (titem != null)
				{
					list.Add(titem);
				}
			}
			return new ReadOnlyCollection<TItem>(list);
		}

		// Token: 0x06003CD4 RID: 15572 RVA: 0x000C962C File Offset: 0x000C782C
		public EdmType GetType(string name, string namespaceName)
		{
			return this.GetType(name, namespaceName, false);
		}

		// Token: 0x06003CD5 RID: 15573 RVA: 0x000C9637 File Offset: 0x000C7837
		public bool TryGetType(string name, string namespaceName, out EdmType type)
		{
			return this.TryGetType(name, namespaceName, false, out type);
		}

		// Token: 0x06003CD6 RID: 15574 RVA: 0x000C9643 File Offset: 0x000C7843
		public EdmType GetType(string name, string namespaceName, bool ignoreCase)
		{
			Check.NotNull<string>(name, "name");
			Check.NotNull<string>(namespaceName, "namespaceName");
			return this.GetItem<EdmType>(EdmType.CreateEdmTypeIdentity(namespaceName, name), ignoreCase);
		}

		// Token: 0x06003CD7 RID: 15575 RVA: 0x000C966C File Offset: 0x000C786C
		public bool TryGetType(string name, string namespaceName, bool ignoreCase, out EdmType type)
		{
			Check.NotNull<string>(name, "name");
			Check.NotNull<string>(namespaceName, "namespaceName");
			GlobalItem globalItem = null;
			this.TryGetValue(EdmType.CreateEdmTypeIdentity(namespaceName, name), ignoreCase, out globalItem);
			type = globalItem as EdmType;
			return type != null;
		}

		// Token: 0x06003CD8 RID: 15576 RVA: 0x000C96B3 File Offset: 0x000C78B3
		public ReadOnlyCollection<EdmFunction> GetFunctions(string functionName)
		{
			return this.GetFunctions(functionName, false);
		}

		// Token: 0x06003CD9 RID: 15577 RVA: 0x000C96BD File Offset: 0x000C78BD
		public ReadOnlyCollection<EdmFunction> GetFunctions(string functionName, bool ignoreCase)
		{
			return ItemCollection.GetFunctions(this.FunctionLookUpTable, functionName, ignoreCase);
		}

		// Token: 0x06003CDA RID: 15578 RVA: 0x000C96CC File Offset: 0x000C78CC
		protected static ReadOnlyCollection<EdmFunction> GetFunctions(Dictionary<string, ReadOnlyCollection<EdmFunction>> functionCollection, string functionName, bool ignoreCase)
		{
			ReadOnlyCollection<EdmFunction> readOnlyCollection;
			if (!functionCollection.TryGetValue(functionName, out readOnlyCollection))
			{
				return Helper.EmptyEdmFunctionReadOnlyCollection;
			}
			if (ignoreCase)
			{
				return readOnlyCollection;
			}
			return ItemCollection.GetCaseSensitiveFunctions(readOnlyCollection, functionName);
		}

		// Token: 0x06003CDB RID: 15579 RVA: 0x000C96F8 File Offset: 0x000C78F8
		internal static ReadOnlyCollection<EdmFunction> GetCaseSensitiveFunctions(ReadOnlyCollection<EdmFunction> functionOverloads, string functionName)
		{
			List<EdmFunction> list = new List<EdmFunction>(functionOverloads.Count);
			for (int i = 0; i < functionOverloads.Count; i++)
			{
				if (functionOverloads[i].FullName == functionName)
				{
					list.Add(functionOverloads[i]);
				}
			}
			if (list.Count != functionOverloads.Count)
			{
				functionOverloads = new ReadOnlyCollection<EdmFunction>(list);
			}
			return functionOverloads;
		}

		// Token: 0x06003CDC RID: 15580 RVA: 0x000C975C File Offset: 0x000C795C
		internal bool TryGetFunction(string functionName, TypeUsage[] parameterTypes, bool ignoreCase, out EdmFunction function)
		{
			Check.NotNull<string>(functionName, "functionName");
			Check.NotNull<TypeUsage[]>(parameterTypes, "parameterTypes");
			string text = EdmFunction.BuildIdentity(functionName, parameterTypes);
			GlobalItem globalItem = null;
			function = null;
			if (this.TryGetValue(text, ignoreCase, out globalItem) && Helper.IsEdmFunction(globalItem))
			{
				function = (EdmFunction)globalItem;
				return true;
			}
			return false;
		}

		// Token: 0x06003CDD RID: 15581 RVA: 0x000C97AF File Offset: 0x000C79AF
		public EntityContainer GetEntityContainer(string name)
		{
			Check.NotNull<string>(name, "name");
			return this.GetEntityContainer(name, false);
		}

		// Token: 0x06003CDE RID: 15582 RVA: 0x000C97C5 File Offset: 0x000C79C5
		public bool TryGetEntityContainer(string name, out EntityContainer entityContainer)
		{
			Check.NotNull<string>(name, "name");
			return this.TryGetEntityContainer(name, false, out entityContainer);
		}

		// Token: 0x06003CDF RID: 15583 RVA: 0x000C97DC File Offset: 0x000C79DC
		public EntityContainer GetEntityContainer(string name, bool ignoreCase)
		{
			EntityContainer entityContainer = this.GetValue(name, ignoreCase) as EntityContainer;
			if (entityContainer != null)
			{
				return entityContainer;
			}
			throw new ArgumentException(Strings.ItemInvalidIdentity(name), "name");
		}

		// Token: 0x06003CE0 RID: 15584 RVA: 0x000C980C File Offset: 0x000C7A0C
		public bool TryGetEntityContainer(string name, bool ignoreCase, out EntityContainer entityContainer)
		{
			Check.NotNull<string>(name, "name");
			GlobalItem globalItem = null;
			if (this.TryGetValue(name, ignoreCase, out globalItem) && Helper.IsEntityContainer(globalItem))
			{
				entityContainer = (EntityContainer)globalItem;
				return true;
			}
			entityContainer = null;
			return false;
		}

		// Token: 0x06003CE1 RID: 15585 RVA: 0x000C9849 File Offset: 0x000C7A49
		internal virtual PrimitiveType GetMappedPrimitiveType(PrimitiveTypeKind primitiveTypeKind)
		{
			throw Error.NotSupported();
		}

		// Token: 0x06003CE2 RID: 15586 RVA: 0x000C9850 File Offset: 0x000C7A50
		internal virtual bool MetadataEquals(ItemCollection other)
		{
			return this == other;
		}

		// Token: 0x06003CE3 RID: 15587 RVA: 0x000C9858 File Offset: 0x000C7A58
		private static Dictionary<string, ReadOnlyCollection<EdmFunction>> PopulateFunctionLookUpTable(ItemCollection itemCollection)
		{
			Dictionary<string, List<EdmFunction>> dictionary = new Dictionary<string, List<EdmFunction>>(StringComparer.OrdinalIgnoreCase);
			foreach (EdmFunction edmFunction in itemCollection.GetItems<EdmFunction>())
			{
				List<EdmFunction> list;
				if (!dictionary.TryGetValue(edmFunction.FullName, out list))
				{
					list = new List<EdmFunction>();
					dictionary[edmFunction.FullName] = list;
				}
				list.Add(edmFunction);
			}
			Dictionary<string, ReadOnlyCollection<EdmFunction>> dictionary2 = new Dictionary<string, ReadOnlyCollection<EdmFunction>>(StringComparer.OrdinalIgnoreCase);
			foreach (List<EdmFunction> list2 in dictionary.Values)
			{
				dictionary2.Add(list2[0].FullName, new ReadOnlyCollection<EdmFunction>(list2.ToArray()));
			}
			return dictionary2;
		}

		// Token: 0x040014DC RID: 5340
		private readonly DataSpace _space;

		// Token: 0x040014DD RID: 5341
		private Dictionary<string, ReadOnlyCollection<EdmFunction>> _functionLookUpTable;

		// Token: 0x040014DE RID: 5342
		private Memoizer<Type, ICollection> _itemsCache;

		// Token: 0x040014DF RID: 5343
		private int _itemCount;
	}
}
