using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Reflection;
using System.Threading;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x02000097 RID: 151
	public abstract class ItemCollection : ReadOnlyMetadataCollection<GlobalItem>
	{
		// Token: 0x06000A88 RID: 2696 RVA: 0x0001907E File Offset: 0x0001727E
		internal ItemCollection(DataSpace dataspace)
			: this(dataspace, new MetadataCollection<GlobalItem>())
		{
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0001908C File Offset: 0x0001728C
		internal ItemCollection(DataSpace dataspace, MetadataCollection<GlobalItem> items)
			: base(items)
		{
			this._space = dataspace;
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000A8A RID: 2698 RVA: 0x0001909C File Offset: 0x0001729C
		public DataSpace DataSpace
		{
			get
			{
				return this._space;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x000190A4 File Offset: 0x000172A4
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

		// Token: 0x06000A8C RID: 2700 RVA: 0x000190D4 File Offset: 0x000172D4
		internal void AddInternal(GlobalItem item)
		{
			base.Source.Add(item);
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x000190E2 File Offset: 0x000172E2
		internal bool AtomicAddRange(List<GlobalItem> items)
		{
			return base.Source.AtomicAddRange(items);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x000190F5 File Offset: 0x000172F5
		public T GetItem<T>(string identity) where T : GlobalItem
		{
			return this.GetItem<T>(identity, false);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x000190FF File Offset: 0x000172FF
		public bool TryGetItem<T>(string identity, out T item) where T : GlobalItem
		{
			return this.TryGetItem<T>(identity, false, out item);
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0001910C File Offset: 0x0001730C
		public bool TryGetItem<T>(string identity, bool ignoreCase, out T item) where T : GlobalItem
		{
			GlobalItem globalItem = null;
			this.TryGetValue(identity, ignoreCase, out globalItem);
			item = globalItem as T;
			return item != null;
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00019148 File Offset: 0x00017348
		public T GetItem<T>(string identity, bool ignoreCase) where T : GlobalItem
		{
			T t;
			if (this.TryGetItem<T>(identity, ignoreCase, out t))
			{
				return t;
			}
			throw EntityUtil.ItemInvalidIdentity(identity, "identity");
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x00019170 File Offset: 0x00017370
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

		// Token: 0x06000A93 RID: 2707 RVA: 0x000191E1 File Offset: 0x000173E1
		internal ICollection InternalGetItems(Type type)
		{
			return typeof(ItemCollection).GetMethod("GenericGetItems", BindingFlags.Static | BindingFlags.NonPublic).MakeGenericMethod(new Type[] { type }).Invoke(null, new object[] { this }) as ICollection;
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00019220 File Offset: 0x00017420
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
			return list.AsReadOnly();
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0001928C File Offset: 0x0001748C
		public EdmType GetType(string name, string namespaceName)
		{
			return this.GetType(name, namespaceName, false);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00019297 File Offset: 0x00017497
		public bool TryGetType(string name, string namespaceName, out EdmType type)
		{
			return this.TryGetType(name, namespaceName, false, out type);
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x000192A3 File Offset: 0x000174A3
		public EdmType GetType(string name, string namespaceName, bool ignoreCase)
		{
			EntityUtil.GenericCheckArgumentNull<string>(name, "name");
			EntityUtil.GenericCheckArgumentNull<string>(namespaceName, "namespaceName");
			return this.GetItem<EdmType>(EdmType.CreateEdmTypeIdentity(namespaceName, name), ignoreCase);
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x000192CC File Offset: 0x000174CC
		public bool TryGetType(string name, string namespaceName, bool ignoreCase, out EdmType type)
		{
			EntityUtil.GenericCheckArgumentNull<string>(name, "name");
			EntityUtil.GenericCheckArgumentNull<string>(namespaceName, "namespaceName");
			GlobalItem globalItem = null;
			this.TryGetValue(EdmType.CreateEdmTypeIdentity(namespaceName, name), ignoreCase, out globalItem);
			type = globalItem as EdmType;
			return type != null;
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x00019313 File Offset: 0x00017513
		public ReadOnlyCollection<EdmFunction> GetFunctions(string functionName)
		{
			return this.GetFunctions(functionName, false);
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0001931D File Offset: 0x0001751D
		public ReadOnlyCollection<EdmFunction> GetFunctions(string functionName, bool ignoreCase)
		{
			return ItemCollection.GetFunctions(this.FunctionLookUpTable, functionName, ignoreCase);
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0001932C File Offset: 0x0001752C
		protected static ReadOnlyCollection<EdmFunction> GetFunctions(Dictionary<string, ReadOnlyCollection<EdmFunction>> functionCollection, string functionName, bool ignoreCase)
		{
			ReadOnlyCollection<EdmFunction> readOnlyCollection;
			if (!functionCollection.TryGetValue(functionName, out readOnlyCollection))
			{
				return ItemCollection.EmptyEdmFunctionReadOnlyCollection;
			}
			if (ignoreCase)
			{
				return readOnlyCollection;
			}
			return ItemCollection.GetCaseSensitiveFunctions(readOnlyCollection, functionName);
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00019358 File Offset: 0x00017558
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
				functionOverloads = list.AsReadOnly();
			}
			return functionOverloads;
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x000193BC File Offset: 0x000175BC
		internal bool TryGetFunction(string functionName, TypeUsage[] parameterTypes, bool ignoreCase, out EdmFunction function)
		{
			EntityUtil.GenericCheckArgumentNull<string>(functionName, "functionName");
			EntityUtil.GenericCheckArgumentNull<TypeUsage[]>(parameterTypes, "parameterTypes");
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

		// Token: 0x06000A9E RID: 2718 RVA: 0x0001940F File Offset: 0x0001760F
		public EntityContainer GetEntityContainer(string name)
		{
			EntityUtil.GenericCheckArgumentNull<string>(name, "name");
			return this.GetEntityContainer(name, false);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00019425 File Offset: 0x00017625
		public bool TryGetEntityContainer(string name, out EntityContainer entityContainer)
		{
			EntityUtil.GenericCheckArgumentNull<string>(name, "name");
			return this.TryGetEntityContainer(name, false, out entityContainer);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0001943C File Offset: 0x0001763C
		public EntityContainer GetEntityContainer(string name, bool ignoreCase)
		{
			EntityContainer entityContainer = this.GetValue(name, ignoreCase) as EntityContainer;
			if (entityContainer != null)
			{
				return entityContainer;
			}
			throw EntityUtil.ItemInvalidIdentity(name, "name");
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00019468 File Offset: 0x00017668
		public bool TryGetEntityContainer(string name, bool ignoreCase, out EntityContainer entityContainer)
		{
			EntityUtil.GenericCheckArgumentNull<string>(name, "name");
			GlobalItem globalItem = null;
			if (this.TryGetValue(name, ignoreCase, out globalItem) && Helper.IsEntityContainer(globalItem))
			{
				entityContainer = (EntityContainer)globalItem;
				return true;
			}
			entityContainer = null;
			return false;
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x000194A5 File Offset: 0x000176A5
		internal virtual PrimitiveType GetMappedPrimitiveType(PrimitiveTypeKind primitiveTypeKind)
		{
			throw Error.NotSupported();
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x000194AC File Offset: 0x000176AC
		internal virtual bool MetadataEquals(ItemCollection other)
		{
			return this == other;
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x000194B4 File Offset: 0x000176B4
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

		// Token: 0x0400084F RID: 2127
		internal static readonly ReadOnlyCollection<EdmFunction> EmptyEdmFunctionReadOnlyCollection = new ReadOnlyCollection<EdmFunction>(new EdmFunction[0]);

		// Token: 0x04000850 RID: 2128
		private readonly DataSpace _space;

		// Token: 0x04000851 RID: 2129
		private Dictionary<string, ReadOnlyCollection<EdmFunction>> _functionLookUpTable;

		// Token: 0x04000852 RID: 2130
		private Memoizer<Type, ICollection> _itemsCache;

		// Token: 0x04000853 RID: 2131
		private int _itemCount;
	}
}
