using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.AnalysisServices.Tabular.Tmdl.Schema
{
	// Token: 0x0200015C RID: 348
	internal sealed class TmdlSchema : TmdlSchemaElement, ITmdlObjectContainer
	{
		// Token: 0x060015EB RID: 5611 RVA: 0x00092229 File Offset: 0x00090429
		public TmdlSchema()
			: this(new List<TmdlObjectInfo>(), null)
		{
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x00092237 File Offset: 0x00090437
		private TmdlSchema(List<TmdlObjectInfo> rootObjects, IReadOnlyDictionary<ObjectType, TmdlObjectInfo> metadataObjects)
		{
			this.rootObjects = rootObjects;
			this.metadataObjects = metadataObjects;
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060015ED RID: 5613 RVA: 0x0009224D File Offset: 0x0009044D
		public IReadOnlyCollection<TmdlObjectInfo> RootObjects
		{
			get
			{
				return this.rootObjects;
			}
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x00092258 File Offset: 0x00090458
		public bool TryGetObjectInfo(string keyword, out TmdlObjectInfo info)
		{
			TmdlObjectInfo.AdjustDeprecatedObjectKeywordIfNeeded(ref keyword);
			info = this.rootObjects.FirstOrDefault((TmdlObjectInfo x) => keyword.Equals(x.Keyword, StringComparison.InvariantCultureIgnoreCase));
			return info != null;
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x0009229C File Offset: 0x0009049C
		public TmdlObjectInfo FindObjectInfo(ObjectType objectType)
		{
			this.EnsureMetadataObjectDictionary();
			TmdlObjectInfo tmdlObjectInfo;
			if (!this.metadataObjects.TryGetValue(objectType, out tmdlObjectInfo))
			{
				return null;
			}
			return tmdlObjectInfo;
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x000922C4 File Offset: 0x000904C4
		internal static TmdlSchema CreateStandardReadOnlySchema(IReadOnlyDictionary<ObjectType, TmdlObjectInfo> metadataObjects, bool includeDatabaseInfo)
		{
			TmdlObjectInfo tmdlObjectInfo;
			TmdlObjectInfo tmdlObjectInfo2;
			if (includeDatabaseInfo)
			{
				Utils.Verify(metadataObjects.TryGetValue(ObjectType.Database, out tmdlObjectInfo));
				Utils.Verify(tmdlObjectInfo.TryGetObjectInfo("model", out tmdlObjectInfo2));
			}
			else
			{
				tmdlObjectInfo = null;
				Utils.Verify(metadataObjects.TryGetValue(ObjectType.Model, out tmdlObjectInfo2));
				if (metadataObjects.ContainsKey(ObjectType.Database))
				{
					Dictionary<ObjectType, TmdlObjectInfo> dictionary = new Dictionary<ObjectType, TmdlObjectInfo>(metadataObjects.Count - 1);
					foreach (KeyValuePair<ObjectType, TmdlObjectInfo> keyValuePair in metadataObjects.Where((KeyValuePair<ObjectType, TmdlObjectInfo> kvp) => kvp.Key != ObjectType.Database))
					{
						dictionary.Add(keyValuePair.Key, keyValuePair.Value);
					}
					metadataObjects = dictionary;
				}
			}
			List<TmdlObjectInfo> list = new List<TmdlObjectInfo>(((tmdlObjectInfo != null) ? 1 : 0) + tmdlObjectInfo2.ChildObjects.Count + 1);
			if (tmdlObjectInfo != null)
			{
				list.Add(tmdlObjectInfo);
			}
			list.Add(tmdlObjectInfo2);
			list.AddRange(tmdlObjectInfo2.ChildObjects.Where((TmdlObjectInfo o) => o.ObjectType > ObjectType.Null));
			TmdlSchema tmdlSchema = new TmdlSchema(list, metadataObjects);
			tmdlSchema.MakeReadOnly();
			return tmdlSchema;
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x00092404 File Offset: 0x00090604
		internal void AddRootObject(TmdlObjectInfo @object)
		{
			base.EnsureNotReadOnly();
			this.rootObjects.Add(@object);
			this.metadataObjects = null;
			this.orderedMetadataObjects = null;
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x00092426 File Offset: 0x00090626
		internal bool RemoveRootObject(TmdlObjectInfo @object)
		{
			base.EnsureNotReadOnly();
			if (this.rootObjects.Remove(@object))
			{
				this.metadataObjects = null;
				this.orderedMetadataObjects = null;
				return true;
			}
			return false;
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x0009244D File Offset: 0x0009064D
		internal void ClearRootObjects()
		{
			base.EnsureNotReadOnly();
			this.rootObjects.Clear();
			if (this.metadataObjects != null)
			{
				this.metadataObjects = null;
			}
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x00092470 File Offset: 0x00090670
		internal IEnumerable<TmdlObjectInfo> GetAllMetadataObjects()
		{
			this.EnsureMetadataObjectDictionary();
			if (this.orderedMetadataObjects == null)
			{
				List<TmdlObjectInfo> list = new List<TmdlObjectInfo>(this.metadataObjects.Values);
				list.Sort(TmdlSchema.metadataObjectOrder);
				this.orderedMetadataObjects = list;
			}
			return this.orderedMetadataObjects;
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x000924B4 File Offset: 0x000906B4
		internal TmdlSchema Clone(bool makeReadOnly = false)
		{
			TmdlSchema tmdlSchema;
			if (base.IsReadOnly && makeReadOnly)
			{
				tmdlSchema = new TmdlSchema(this.rootObjects, this.metadataObjects);
			}
			else if (this.rootObjects.Count > 0)
			{
				List<TmdlObjectInfo> list = new List<TmdlObjectInfo>(this.rootObjects.Count);
				for (int i = 0; i < this.rootObjects.Count; i++)
				{
					if (!makeReadOnly || !this.rootObjects[i].IsReadOnly)
					{
						list.Add(this.rootObjects[i].Clone(null, makeReadOnly));
					}
					else
					{
						list.Add(this.rootObjects[i]);
					}
				}
				tmdlSchema = new TmdlSchema(list, null);
			}
			else
			{
				tmdlSchema = new TmdlSchema();
			}
			if (makeReadOnly)
			{
				tmdlSchema.MakeReadOnly();
			}
			return tmdlSchema;
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x00092574 File Offset: 0x00090774
		private protected override void MakeReadOnlyImpl()
		{
			foreach (TmdlObjectInfo tmdlObjectInfo in this.rootObjects.Where((TmdlObjectInfo o) => !o.IsReadOnly))
			{
				tmdlObjectInfo.MakeReadOnly();
			}
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x000925E4 File Offset: 0x000907E4
		private static IEnumerable<TmdlObjectInfo> GetAllMetadataObjectsImpl(IList<TmdlObjectInfo> rootObjects)
		{
			HashSet<ObjectType> objectTypes = new HashSet<ObjectType>();
			Dictionary<ObjectType, TmdlObjectInfo> deferredSingletons = new Dictionary<ObjectType, TmdlObjectInfo>();
			Queue<TmdlObjectInfo> objectsToEnumerate = new Queue<TmdlObjectInfo>();
			using (IEnumerator<TmdlObjectInfo> enumerator = rootObjects.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TmdlObjectInfo tmdlObjectInfo = enumerator.Current;
					objectsToEnumerate.Enqueue(tmdlObjectInfo);
				}
				goto IL_0294;
			}
			IL_0080:
			TmdlObjectInfo tmdlObjectInfo2 = objectsToEnumerate.Dequeue();
			if (tmdlObjectInfo2.IsSingleton && string.Compare(tmdlObjectInfo2.ObjectType.ToString("G"), tmdlObjectInfo2.PropertyName, StringComparison.InvariantCultureIgnoreCase) != 0)
			{
				if (!objectTypes.Contains(tmdlObjectInfo2.ObjectType) && !deferredSingletons.ContainsKey(tmdlObjectInfo2.ObjectType))
				{
					deferredSingletons.Add(tmdlObjectInfo2.ObjectType, tmdlObjectInfo2);
				}
			}
			else if (objectTypes.Add(tmdlObjectInfo2.ObjectType))
			{
				if (tmdlObjectInfo2.IsSingleton)
				{
					deferredSingletons.Remove(tmdlObjectInfo2.ObjectType);
				}
				if (tmdlObjectInfo2.HasVariants)
				{
					using (IEnumerator<TmdlObjectInfo> enumerator = (from v in tmdlObjectInfo2.Variants
						select v.Value into v
						where v.HasAnyChild(false)
						select v).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							TmdlObjectInfo tmdlObjectInfo3 = enumerator.Current;
							foreach (TmdlObjectInfo tmdlObjectInfo4 in tmdlObjectInfo3.ChildObjects.Where((TmdlObjectInfo c) => c.ObjectType > ObjectType.Null))
							{
								objectsToEnumerate.Enqueue(tmdlObjectInfo4);
							}
						}
						goto IL_0277;
					}
				}
				if (tmdlObjectInfo2.HasAnyChild(false))
				{
					foreach (TmdlObjectInfo tmdlObjectInfo5 in tmdlObjectInfo2.ChildObjects.Where((TmdlObjectInfo c) => c.ObjectType > ObjectType.Null))
					{
						objectsToEnumerate.Enqueue(tmdlObjectInfo5);
					}
				}
				IL_0277:
				yield return tmdlObjectInfo2;
			}
			IL_0294:
			if (objectsToEnumerate.Count <= 0)
			{
				if (deferredSingletons.Count > 0)
				{
					foreach (TmdlObjectInfo tmdlObjectInfo6 in deferredSingletons.Values)
					{
						yield return tmdlObjectInfo6;
					}
					Dictionary<ObjectType, TmdlObjectInfo>.ValueCollection.Enumerator enumerator3 = default(Dictionary<ObjectType, TmdlObjectInfo>.ValueCollection.Enumerator);
				}
				yield break;
			}
			goto IL_0080;
			yield break;
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x000925F4 File Offset: 0x000907F4
		private void EnsureMetadataObjectDictionary()
		{
			if (this.metadataObjects == null)
			{
				Dictionary<ObjectType, TmdlObjectInfo> dictionary = new Dictionary<ObjectType, TmdlObjectInfo>();
				foreach (TmdlObjectInfo tmdlObjectInfo in TmdlSchema.GetAllMetadataObjectsImpl(this.rootObjects))
				{
					dictionary.Add(tmdlObjectInfo.ObjectType, tmdlObjectInfo);
				}
				this.metadataObjects = dictionary;
			}
		}

		// Token: 0x04000405 RID: 1029
		private static readonly Comparison<TmdlObjectInfo> metadataObjectOrder = delegate(TmdlObjectInfo info1, TmdlObjectInfo info2)
		{
			if (info1.ObjectType == ObjectType.Database)
			{
				if (info2.ObjectType != ObjectType.Database)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (info2.ObjectType == ObjectType.Database)
				{
					return 1;
				}
				return info1.ObjectType - info2.ObjectType;
			}
		};

		// Token: 0x04000406 RID: 1030
		private readonly List<TmdlObjectInfo> rootObjects;

		// Token: 0x04000407 RID: 1031
		private IReadOnlyDictionary<ObjectType, TmdlObjectInfo> metadataObjects;

		// Token: 0x04000408 RID: 1032
		private IReadOnlyList<TmdlObjectInfo> orderedMetadataObjects;
	}
}
