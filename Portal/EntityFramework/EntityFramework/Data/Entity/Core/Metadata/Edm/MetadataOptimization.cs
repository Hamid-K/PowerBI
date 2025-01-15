using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004DB RID: 1243
	internal class MetadataOptimization
	{
		// Token: 0x06003DC2 RID: 15810 RVA: 0x000CCFEA File Offset: 0x000CB1EA
		internal MetadataOptimization(MetadataWorkspace workspace)
		{
			this._workspace = workspace;
		}

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x06003DC3 RID: 15811 RVA: 0x000CD00F File Offset: 0x000CB20F
		internal IDictionary<Type, EntitySetTypePair> EntitySetMappingCache
		{
			get
			{
				return this._entitySetMappingsCache;
			}
		}

		// Token: 0x06003DC4 RID: 15812 RVA: 0x000CD018 File Offset: 0x000CB218
		private void UpdateEntitySetMappings()
		{
			ObjectItemCollection objectItemCollection = (ObjectItemCollection)this._workspace.GetItemCollection(DataSpace.OSpace);
			ReadOnlyCollection<EntityType> items = this._workspace.GetItems<EntityType>(DataSpace.OSpace);
			Stack<EntityType> stack = new Stack<EntityType>();
			foreach (EntityType entityType in items)
			{
				stack.Clear();
				EntityType cspaceType = (EntityType)this._workspace.GetEdmSpaceType(entityType);
				do
				{
					stack.Push(cspaceType);
					cspaceType = (EntityType)cspaceType.BaseType;
				}
				while (cspaceType != null);
				EntitySet entitySet = null;
				Func<EntitySetBase, bool> <>9__0;
				while (entitySet == null && stack.Count > 0)
				{
					cspaceType = stack.Pop();
					foreach (EntityContainer entityContainer in this._workspace.GetItems<EntityContainer>(DataSpace.CSpace))
					{
						IEnumerable<EntitySetBase> baseEntitySets = entityContainer.BaseEntitySets;
						Func<EntitySetBase, bool> func;
						if ((func = <>9__0) == null)
						{
							func = (<>9__0 = (EntitySetBase s) => s.ElementType == cspaceType);
						}
						List<EntitySetBase> list = baseEntitySets.Where(func).ToList<EntitySetBase>();
						int count = list.Count;
						if (count > 1 || (count == 1 && entitySet != null))
						{
							throw Error.DbContext_MESTNotSupported();
						}
						if (count == 1)
						{
							entitySet = (EntitySet)list[0];
						}
					}
				}
				if (entitySet != null)
				{
					EntityType entityType2 = (EntityType)this._workspace.GetObjectSpaceType(cspaceType);
					Type clrType = objectItemCollection.GetClrType(entityType);
					Type clrType2 = objectItemCollection.GetClrType(entityType2);
					this._entitySetMappingsCache[clrType] = new EntitySetTypePair(entitySet, clrType2);
				}
			}
		}

		// Token: 0x06003DC5 RID: 15813 RVA: 0x000CD200 File Offset: 0x000CB400
		internal bool TryUpdateEntitySetMappingsForType(Type entityType)
		{
			if (this._entitySetMappingsCache.ContainsKey(entityType))
			{
				return true;
			}
			Type type = entityType;
			do
			{
				this._workspace.LoadFromAssembly(type.Assembly());
				type = type.BaseType();
			}
			while (type != null && type != typeof(object));
			object entitySetMappingsUpdateLock = this._entitySetMappingsUpdateLock;
			lock (entitySetMappingsUpdateLock)
			{
				if (this._entitySetMappingsCache.ContainsKey(entityType))
				{
					return true;
				}
				this.UpdateEntitySetMappings();
			}
			return this._entitySetMappingsCache.ContainsKey(entityType);
		}

		// Token: 0x06003DC6 RID: 15814 RVA: 0x000CD2A8 File Offset: 0x000CB4A8
		internal AssociationType GetCSpaceAssociationType(AssociationType osAssociationType)
		{
			return this._csAssociationTypes[osAssociationType.Index];
		}

		// Token: 0x06003DC7 RID: 15815 RVA: 0x000CD2BC File Offset: 0x000CB4BC
		internal AssociationSet FindCSpaceAssociationSet(AssociationType associationType, string endName, EntitySet endEntitySet)
		{
			object[] cspaceAssociationTypeToSetsMap = this.GetCSpaceAssociationTypeToSetsMap();
			int index = associationType.Index;
			object obj = cspaceAssociationTypeToSetsMap[index];
			if (obj == null)
			{
				return null;
			}
			AssociationSet associationSet = obj as AssociationSet;
			if (associationSet == null)
			{
				foreach (AssociationSet associationSet in (AssociationSet[])obj)
				{
					if (associationSet.AssociationSetEnds[endName].EntitySet == endEntitySet)
					{
						return associationSet;
					}
				}
				return null;
			}
			if (associationSet.AssociationSetEnds[endName].EntitySet != endEntitySet)
			{
				return null;
			}
			return associationSet;
		}

		// Token: 0x06003DC8 RID: 15816 RVA: 0x000CD338 File Offset: 0x000CB538
		internal AssociationSet FindCSpaceAssociationSet(AssociationType associationType, string endName, string entitySetName, string entityContainerName, out EntitySet endEntitySet)
		{
			object[] cspaceAssociationTypeToSetsMap = this.GetCSpaceAssociationTypeToSetsMap();
			int index = associationType.Index;
			object obj = cspaceAssociationTypeToSetsMap[index];
			if (obj == null)
			{
				endEntitySet = null;
				return null;
			}
			AssociationSet associationSet = obj as AssociationSet;
			if (associationSet == null)
			{
				foreach (AssociationSet associationSet in (AssociationSet[])obj)
				{
					EntitySet entitySet = associationSet.AssociationSetEnds[endName].EntitySet;
					if (entitySet.Name == entitySetName && entitySet.EntityContainer.Name == entityContainerName)
					{
						endEntitySet = entitySet;
						return associationSet;
					}
				}
				endEntitySet = null;
				return null;
			}
			EntitySet entitySet2 = associationSet.AssociationSetEnds[endName].EntitySet;
			if (entitySet2.Name == entitySetName && entitySet2.EntityContainer.Name == entityContainerName)
			{
				endEntitySet = entitySet2;
				return associationSet;
			}
			endEntitySet = null;
			return null;
		}

		// Token: 0x06003DC9 RID: 15817 RVA: 0x000CD40F File Offset: 0x000CB60F
		internal AssociationType[] GetCSpaceAssociationTypes()
		{
			if (this._csAssociationTypes == null)
			{
				this._csAssociationTypes = MetadataOptimization.IndexCSpaceAssociationTypes(this._workspace.GetItemCollection(DataSpace.CSpace));
			}
			return this._csAssociationTypes;
		}

		// Token: 0x06003DCA RID: 15818 RVA: 0x000CD43C File Offset: 0x000CB63C
		private static AssociationType[] IndexCSpaceAssociationTypes(ItemCollection itemCollection)
		{
			List<AssociationType> list = new List<AssociationType>();
			int num = 0;
			foreach (AssociationType associationType in itemCollection.GetItems<AssociationType>())
			{
				list.Add(associationType);
				associationType.Index = num++;
			}
			return list.ToArray();
		}

		// Token: 0x06003DCB RID: 15819 RVA: 0x000CD4A4 File Offset: 0x000CB6A4
		internal object[] GetCSpaceAssociationTypeToSetsMap()
		{
			if (this._csAssociationTypeToSets == null)
			{
				this._csAssociationTypeToSets = MetadataOptimization.MapCSpaceAssociationTypeToSets(this._workspace.GetItemCollection(DataSpace.CSpace), this.GetCSpaceAssociationTypes().Length);
			}
			return this._csAssociationTypeToSets;
		}

		// Token: 0x06003DCC RID: 15820 RVA: 0x000CD4DC File Offset: 0x000CB6DC
		private static object[] MapCSpaceAssociationTypeToSets(ItemCollection itemCollection, int associationTypeCount)
		{
			object[] array = new object[associationTypeCount];
			foreach (EntityContainer entityContainer in itemCollection.GetItems<EntityContainer>())
			{
				foreach (EntitySetBase entitySetBase in entityContainer.BaseEntitySets)
				{
					AssociationSet associationSet = entitySetBase as AssociationSet;
					if (associationSet != null)
					{
						int index = associationSet.ElementType.Index;
						MetadataOptimization.AddItemAtIndex<AssociationSet>(array, index, associationSet);
					}
				}
			}
			return array;
		}

		// Token: 0x06003DCD RID: 15821 RVA: 0x000CD584 File Offset: 0x000CB784
		internal AssociationType GetOSpaceAssociationType(AssociationType cSpaceAssociationType, Func<AssociationType> initializer)
		{
			AssociationType[] ospaceAssociationTypes = this.GetOSpaceAssociationTypes();
			int index = cSpaceAssociationType.Index;
			Thread.MemoryBarrier();
			AssociationType associationType = ospaceAssociationTypes[index];
			if (associationType == null)
			{
				associationType = initializer();
				associationType.Index = index;
				ospaceAssociationTypes[index] = associationType;
				Thread.MemoryBarrier();
			}
			return associationType;
		}

		// Token: 0x06003DCE RID: 15822 RVA: 0x000CD5C7 File Offset: 0x000CB7C7
		internal AssociationType[] GetOSpaceAssociationTypes()
		{
			if (this._osAssociationTypes == null)
			{
				this._osAssociationTypes = new AssociationType[this.GetCSpaceAssociationTypes().Length];
			}
			return this._osAssociationTypes;
		}

		// Token: 0x06003DCF RID: 15823 RVA: 0x000CD5F0 File Offset: 0x000CB7F0
		private static void AddItemAtIndex<T>(object[] array, int index, T newItem) where T : class
		{
			object obj = array[index];
			if (obj == null)
			{
				array[index] = newItem;
				return;
			}
			T t = obj as T;
			if (t != null)
			{
				array[index] = new T[] { t, newItem };
				return;
			}
			T[] array2 = (T[])obj;
			int num = array2.Length;
			Array.Resize<T>(ref array2, num + 1);
			array2[num] = newItem;
			array[index] = array2;
		}

		// Token: 0x04001509 RID: 5385
		private readonly MetadataWorkspace _workspace;

		// Token: 0x0400150A RID: 5386
		private readonly IDictionary<Type, EntitySetTypePair> _entitySetMappingsCache = new Dictionary<Type, EntitySetTypePair>();

		// Token: 0x0400150B RID: 5387
		private object _entitySetMappingsUpdateLock = new object();

		// Token: 0x0400150C RID: 5388
		private volatile AssociationType[] _csAssociationTypes;

		// Token: 0x0400150D RID: 5389
		private volatile AssociationType[] _osAssociationTypes;

		// Token: 0x0400150E RID: 5390
		private volatile object[] _csAssociationTypeToSets;
	}
}
