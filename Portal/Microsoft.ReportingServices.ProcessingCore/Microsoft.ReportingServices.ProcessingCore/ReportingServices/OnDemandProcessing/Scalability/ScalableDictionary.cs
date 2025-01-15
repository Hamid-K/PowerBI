using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000892 RID: 2194
	internal sealed class ScalableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007829 RID: 30761 RVA: 0x001EF189 File Offset: 0x001ED389
		public ScalableDictionary()
		{
		}

		// Token: 0x0600782A RID: 30762 RVA: 0x001EF191 File Offset: 0x001ED391
		internal ScalableDictionary(int priority, IScalabilityCache cache)
			: this(priority, cache, 23, 5, null)
		{
		}

		// Token: 0x0600782B RID: 30763 RVA: 0x001EF19F File Offset: 0x001ED39F
		internal ScalableDictionary(int priority, IScalabilityCache cache, int nodeCapacity, int entryCapacity)
			: this(priority, cache, nodeCapacity, entryCapacity, null)
		{
		}

		// Token: 0x0600782C RID: 30764 RVA: 0x001EF1AD File Offset: 0x001ED3AD
		internal ScalableDictionary(int priority, IScalabilityCache cache, int nodeCapacity, int entryCapacity, IEqualityComparer<TKey> comparer)
			: this(priority, cache, nodeCapacity, entryCapacity, comparer, false)
		{
		}

		// Token: 0x0600782D RID: 30765 RVA: 0x001EF1C0 File Offset: 0x001ED3C0
		internal ScalableDictionary(int priority, IScalabilityCache cache, int nodeCapacity, int entryCapacity, IEqualityComparer<TKey> comparer, bool useFixedReferences)
		{
			this.m_priority = priority;
			this.m_scalabilityCache = cache;
			this.m_nodeCapacity = nodeCapacity;
			this.m_valuesCapacity = entryCapacity;
			this.m_comparer = comparer;
			this.m_version = 0;
			this.m_count = 0;
			this.m_useFixedReferences = useFixedReferences;
			if (this.m_comparer == null)
			{
				this.m_comparer = EqualityComparer<TKey>.Default;
			}
			this.m_root = this.BuildNode(0, this.m_nodeCapacity);
		}

		// Token: 0x0600782E RID: 30766 RVA: 0x001EF234 File Offset: 0x001ED434
		public void Add(TKey key, TValue value)
		{
			this.AddAndPin(key, value).Dispose();
		}

		// Token: 0x0600782F RID: 30767 RVA: 0x001EF244 File Offset: 0x001ED444
		public IDisposable AddAndPin(TKey key, TValue value)
		{
			IDisposable disposable;
			if (this.Insert(this.m_root, this.GetHashCode(key), key, value, true, 0, true, out disposable))
			{
				this.m_count++;
			}
			this.m_version++;
			return disposable;
		}

		// Token: 0x06007830 RID: 30768 RVA: 0x001EF28C File Offset: 0x001ED48C
		public bool ContainsKey(TKey key)
		{
			TValue tvalue;
			return this.TryGetValue(key, out tvalue);
		}

		// Token: 0x170027F5 RID: 10229
		// (get) Token: 0x06007831 RID: 30769 RVA: 0x001EF2A2 File Offset: 0x001ED4A2
		public ICollection<TKey> Keys
		{
			get
			{
				if (this.m_keysCollection == null)
				{
					this.m_keysCollection = new ScalableDictionary<TKey, TValue>.ScalableDictionaryKeysCollection(this);
				}
				return this.m_keysCollection;
			}
		}

		// Token: 0x170027F6 RID: 10230
		// (get) Token: 0x06007832 RID: 30770 RVA: 0x001EF2BE File Offset: 0x001ED4BE
		public ICollection<TValue> Values
		{
			get
			{
				if (this.m_valuesCollection == null)
				{
					this.m_valuesCollection = new ScalableDictionary<TKey, TValue>.ScalableDictionaryValuesCollection(this);
				}
				return this.m_valuesCollection;
			}
		}

		// Token: 0x06007833 RID: 30771 RVA: 0x001EF2DC File Offset: 0x001ED4DC
		public bool Remove(TKey key)
		{
			int num;
			bool flag = this.Remove(this.m_root, this.GetHashCode(key), key, 0, out num);
			if (flag)
			{
				this.m_count--;
				this.m_version++;
			}
			return flag;
		}

		// Token: 0x06007834 RID: 30772 RVA: 0x001EF320 File Offset: 0x001ED520
		public bool TryGetValue(TKey key, out TValue value)
		{
			IDisposable disposable;
			bool flag = this.TryGetAndPin(key, out value, out disposable);
			if (flag)
			{
				disposable.Dispose();
			}
			return flag;
		}

		// Token: 0x170027F7 RID: 10231
		public TValue this[TKey key]
		{
			get
			{
				TValue tvalue;
				if (!this.TryGetValue(key, out tvalue))
				{
					Global.Tracer.Assert(false, "Given key is not present in the dictionary");
				}
				return tvalue;
			}
			set
			{
				IDisposable disposable;
				if (this.Insert(this.m_root, this.GetHashCode(key), key, value, false, 0, true, out disposable))
				{
					this.m_count++;
				}
				this.m_version++;
				disposable.Dispose();
			}
		}

		// Token: 0x170027F8 RID: 10232
		// (get) Token: 0x06007837 RID: 30775 RVA: 0x001EF3B7 File Offset: 0x001ED5B7
		public IEqualityComparer<TKey> Comparer
		{
			get
			{
				return this.m_comparer;
			}
		}

		// Token: 0x06007838 RID: 30776 RVA: 0x001EF3C0 File Offset: 0x001ED5C0
		public bool ContainsValue(TValue value)
		{
			IEqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
			{
				if (@default.Equals(keyValuePair.Value, value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06007839 RID: 30777 RVA: 0x001EF420 File Offset: 0x001ED620
		public bool TryGetAndPin(TKey key, out TValue value, out IDisposable reference)
		{
			if (key == null)
			{
				Global.Tracer.Assert(false, "ScalableDictionary: Key cannot be null");
			}
			return this.Find(this.m_root, this.GetHashCode(key), key, 0, out value, out reference);
		}

		// Token: 0x0600783A RID: 30778 RVA: 0x001EF454 File Offset: 0x001ED654
		public IDisposable GetAndPin(TKey key, out TValue value)
		{
			IDisposable disposable;
			bool flag = this.TryGetAndPin(key, out value, out disposable);
			Global.Tracer.Assert(flag, "Missing expected dictionary item with key");
			return disposable;
		}

		// Token: 0x0600783B RID: 30779 RVA: 0x001EF47D File Offset: 0x001ED67D
		public void TransferTo(IScalabilityCache scaleCache)
		{
			this.m_root = (ScalableDictionaryNodeReference)this.m_root.TransferTo(scaleCache);
			this.m_scalabilityCache = scaleCache;
		}

		// Token: 0x0600783C RID: 30780 RVA: 0x001EF49D File Offset: 0x001ED69D
		public void UpdateComparer(IEqualityComparer<TKey> comparer)
		{
			Global.Tracer.Assert(this.m_comparer == null, "Cannot update equality comparer in the middle of a table computation.");
			this.m_comparer = comparer;
		}

		// Token: 0x0600783D RID: 30781 RVA: 0x001EF4BE File Offset: 0x001ED6BE
		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
		{
			this.Add(item.Key, item.Value);
		}

		// Token: 0x0600783E RID: 30782 RVA: 0x001EF4D4 File Offset: 0x001ED6D4
		public void DisableStorageUpdates()
		{
			this.m_scalabilityCache.DisableStorageUpdates();
		}

		// Token: 0x0600783F RID: 30783 RVA: 0x001EF4E1 File Offset: 0x001ED6E1
		public void Clear()
		{
			this.FreeChildren(this.m_root);
			this.m_count = 0;
			this.m_version++;
		}

		// Token: 0x06007840 RID: 30784 RVA: 0x001EF504 File Offset: 0x001ED704
		public void Dispose()
		{
			if (this.m_root != null)
			{
				this.Clear();
				this.m_root.Free();
			}
		}

		// Token: 0x06007841 RID: 30785 RVA: 0x001EF528 File Offset: 0x001ED728
		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
		{
			IEqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			TValue tvalue;
			return this.TryGetValue(item.Key, out tvalue) && @default.Equals(item.Value, tvalue);
		}

		// Token: 0x06007842 RID: 30786 RVA: 0x001EF55C File Offset: 0x001ED75C
		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			if (arrayIndex < 0)
			{
				Global.Tracer.Assert(false, "ScalableDictionary.CopyTo: Index must be greater than 0");
			}
			if (array == null)
			{
				Global.Tracer.Assert(false, "ScalableDictionary.CopyTo: Specified array must not be null");
			}
			if (array.Rank > 1)
			{
				Global.Tracer.Assert(false, "ScalableDictionary.CopyTo: Specified array must be 1 dimensional", new object[] { "array" });
			}
			if (arrayIndex + this.Count > array.Length)
			{
				Global.Tracer.Assert(false, "ScalableDictionary.CopyTo: Insufficent space in destination array");
			}
			foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
			{
				array[arrayIndex] = keyValuePair;
				arrayIndex++;
			}
		}

		// Token: 0x170027F9 RID: 10233
		// (get) Token: 0x06007843 RID: 30787 RVA: 0x001EF618 File Offset: 0x001ED818
		public int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x170027FA RID: 10234
		// (get) Token: 0x06007844 RID: 30788 RVA: 0x001EF620 File Offset: 0x001ED820
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007845 RID: 30789 RVA: 0x001EF623 File Offset: 0x001ED823
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			return ((ICollection<KeyValuePair<TKey, TValue>>)this).Contains(item) && this.Remove(item.Key);
		}

		// Token: 0x06007846 RID: 30790 RVA: 0x001EF63D File Offset: 0x001ED83D
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return new ScalableDictionary<TKey, TValue>.ScalableDictionaryEnumerator(this);
		}

		// Token: 0x06007847 RID: 30791 RVA: 0x001EF64A File Offset: 0x001ED84A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06007848 RID: 30792 RVA: 0x001EF652 File Offset: 0x001ED852
		private int GetHashCode(TKey key)
		{
			if (key == null || key is DBNull)
			{
				return DBNull.Value.GetHashCode();
			}
			return this.m_comparer.GetHashCode(key);
		}

		// Token: 0x06007849 RID: 30793 RVA: 0x001EF680 File Offset: 0x001ED880
		private void FreeChildren(ScalableDictionaryNodeReference nodeRef)
		{
			using (nodeRef.PinValue())
			{
				ScalableDictionaryNode scalableDictionaryNode = nodeRef.Value();
				for (int i = 0; i < scalableDictionaryNode.Entries.Length; i++)
				{
					IScalableDictionaryEntry scalableDictionaryEntry = scalableDictionaryNode.Entries[i];
					if (scalableDictionaryEntry != null)
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType objectType = scalableDictionaryEntry.GetObjectType();
						if (objectType != Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionaryNodeReference)
						{
							if (objectType != Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionaryValues)
							{
								Global.Tracer.Assert(false, "Unknown ObjectType");
							}
						}
						else
						{
							ScalableDictionaryNodeReference scalableDictionaryNodeReference = scalableDictionaryEntry as ScalableDictionaryNodeReference;
							this.FreeChildren(scalableDictionaryNodeReference);
							scalableDictionaryNodeReference.Free();
						}
					}
					scalableDictionaryNode.Entries[i] = null;
				}
				scalableDictionaryNode.Count = 0;
			}
		}

		// Token: 0x0600784A RID: 30794 RVA: 0x001EF728 File Offset: 0x001ED928
		private ScalableDictionaryNodeReference BuildNode(int level, int capacity)
		{
			ScalableDictionaryNode scalableDictionaryNode = new ScalableDictionaryNode(capacity);
			ScalableDictionaryNodeReference scalableDictionaryNodeReference;
			if (this.m_useFixedReferences)
			{
				scalableDictionaryNodeReference = (ScalableDictionaryNodeReference)this.m_scalabilityCache.GenerateFixedReference<ScalableDictionaryNode>(scalableDictionaryNode);
			}
			else
			{
				scalableDictionaryNodeReference = (ScalableDictionaryNodeReference)this.m_scalabilityCache.Allocate<ScalableDictionaryNode>(scalableDictionaryNode, this.m_priority, scalableDictionaryNode.EmptySize);
			}
			return scalableDictionaryNodeReference;
		}

		// Token: 0x0600784B RID: 30795 RVA: 0x001EF778 File Offset: 0x001ED978
		private bool Insert(ScalableDictionaryNodeReference nodeRef, int hashCode, TKey key, TValue value, bool add, int level, bool updateSize, out IDisposable cleanupRef)
		{
			IDisposable disposable = nodeRef.PinValue();
			ScalableDictionaryNode scalableDictionaryNode = nodeRef.Value();
			bool flag = false;
			int num = this.HashToSlot(scalableDictionaryNode, hashCode, level);
			IScalableDictionaryEntry scalableDictionaryEntry = scalableDictionaryNode.Entries[num];
			if (scalableDictionaryEntry == null)
			{
				ScalableDictionaryValues scalableDictionaryValues = new ScalableDictionaryValues(this.m_valuesCapacity);
				scalableDictionaryValues.Keys[0] = key;
				scalableDictionaryValues.Values[0] = value;
				ScalableDictionaryValues scalableDictionaryValues2 = scalableDictionaryValues;
				int num2 = scalableDictionaryValues2.Count;
				scalableDictionaryValues2.Count = num2 + 1;
				scalableDictionaryNode.Entries[num] = scalableDictionaryValues;
				flag = true;
				cleanupRef = disposable;
				if (!this.m_useFixedReferences && updateSize)
				{
					int num3 = ItemSizes.SizeOfInObjectArray(key) + ItemSizes.SizeOfInObjectArray(value) + scalableDictionaryValues.EmptySize;
					nodeRef.UpdateSize(num3);
				}
			}
			else
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType objectType = scalableDictionaryEntry.GetObjectType();
				if (objectType != Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionaryNodeReference)
				{
					if (objectType == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionaryValues)
					{
						ScalableDictionaryValues scalableDictionaryValues3 = scalableDictionaryEntry as ScalableDictionaryValues;
						bool flag2 = false;
						cleanupRef = null;
						for (int i = 0; i < scalableDictionaryValues3.Count; i++)
						{
							if (this.m_comparer.Equals(key, (TKey)((object)scalableDictionaryValues3.Keys[i])))
							{
								if (add)
								{
									Global.Tracer.Assert(false, "ScalableDictionary: An element with the same key already exists within the Dictionary");
								}
								scalableDictionaryValues3.Values[i] = value;
								flag2 = true;
								flag = false;
								cleanupRef = disposable;
								break;
							}
						}
						if (flag2)
						{
							goto IL_02E0;
						}
						if (scalableDictionaryValues3.Count < scalableDictionaryValues3.Capacity)
						{
							int count = scalableDictionaryValues3.Count;
							scalableDictionaryValues3.Keys[count] = key;
							scalableDictionaryValues3.Values[count] = value;
							ScalableDictionaryValues scalableDictionaryValues4 = scalableDictionaryValues3;
							int num2 = scalableDictionaryValues4.Count;
							scalableDictionaryValues4.Count = num2 + 1;
							flag = true;
							cleanupRef = disposable;
							if (!this.m_useFixedReferences && updateSize)
							{
								nodeRef.UpdateSize(ItemSizes.SizeOfInObjectArray(key));
								nodeRef.UpdateSize(ItemSizes.SizeOfInObjectArray(value));
								goto IL_02E0;
							}
							goto IL_02E0;
						}
						else
						{
							ScalableDictionaryNodeReference scalableDictionaryNodeReference = this.BuildNode(level + 1, this.m_nodeCapacity);
							scalableDictionaryNode.Entries[num] = scalableDictionaryNodeReference;
							using (scalableDictionaryNodeReference.PinValue())
							{
								if (!this.m_useFixedReferences && updateSize)
								{
									int num4 = ItemSizes.SizeOfInObjectArray(scalableDictionaryValues3);
									nodeRef.UpdateSize(num4 * -1);
									scalableDictionaryNodeReference.UpdateSize(num4);
								}
								for (int j = 0; j < scalableDictionaryValues3.Count; j++)
								{
									TKey tkey = (TKey)((object)scalableDictionaryValues3.Keys[j]);
									IDisposable disposable3;
									this.Insert(scalableDictionaryNodeReference, this.GetHashCode(tkey), tkey, (TValue)((object)scalableDictionaryValues3.Values[j]), false, level + 1, false, out disposable3);
									disposable3.Dispose();
								}
								flag = this.Insert(scalableDictionaryNodeReference, hashCode, key, value, add, level + 1, updateSize, out cleanupRef);
								goto IL_02E0;
							}
						}
					}
					Global.Tracer.Assert(false, "Unknown ObjectType");
					cleanupRef = null;
				}
				else
				{
					ScalableDictionaryNodeReference scalableDictionaryNodeReference2 = scalableDictionaryEntry as ScalableDictionaryNodeReference;
					flag = this.Insert(scalableDictionaryNodeReference2, hashCode, key, value, add, level + 1, updateSize, out cleanupRef);
				}
			}
			IL_02E0:
			if (flag)
			{
				scalableDictionaryNode.Count++;
			}
			if (disposable != cleanupRef)
			{
				disposable.Dispose();
			}
			return flag;
		}

		// Token: 0x0600784C RID: 30796 RVA: 0x001EFA94 File Offset: 0x001EDC94
		private int HashToSlot(ScalableDictionaryNode node, int hashCode, int level)
		{
			int prime = PrimeHelper.GetPrime(level);
			int hashInputA = PrimeHelper.GetHashInputA(level);
			int hashInputB = PrimeHelper.GetHashInputB(level);
			return Math.Abs(hashInputA * hashCode + hashInputB) % prime % node.Entries.Length;
		}

		// Token: 0x0600784D RID: 30797 RVA: 0x001EFACC File Offset: 0x001EDCCC
		private bool Find(ScalableDictionaryNodeReference nodeRef, int hashCode, TKey key, int level, out TValue value, out IDisposable containingNodeRef)
		{
			containingNodeRef = null;
			IDisposable disposable = nodeRef.PinValue();
			ScalableDictionaryNode scalableDictionaryNode = nodeRef.Value();
			value = default(TValue);
			bool flag = false;
			int num = this.HashToSlot(scalableDictionaryNode, hashCode, level);
			IScalableDictionaryEntry scalableDictionaryEntry = scalableDictionaryNode.Entries[num];
			if (scalableDictionaryEntry != null)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType objectType = scalableDictionaryEntry.GetObjectType();
				if (objectType != Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionaryNodeReference)
				{
					if (objectType != Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionaryValues)
					{
						Global.Tracer.Assert(false, "Unknown ObjectType");
					}
					else
					{
						ScalableDictionaryValues scalableDictionaryValues = scalableDictionaryEntry as ScalableDictionaryValues;
						for (int i = 0; i < scalableDictionaryValues.Count; i++)
						{
							if (this.m_comparer.Equals(key, (TKey)((object)scalableDictionaryValues.Keys[i])))
							{
								value = (TValue)((object)scalableDictionaryValues.Values[i]);
								containingNodeRef = disposable;
								return true;
							}
						}
					}
				}
				else
				{
					ScalableDictionaryNodeReference scalableDictionaryNodeReference = scalableDictionaryEntry as ScalableDictionaryNodeReference;
					flag = this.Find(scalableDictionaryNodeReference, hashCode, key, level + 1, out value, out containingNodeRef);
				}
			}
			disposable.Dispose();
			return flag;
		}

		// Token: 0x0600784E RID: 30798 RVA: 0x001EFBB8 File Offset: 0x001EDDB8
		private bool Remove(ScalableDictionaryNodeReference nodeRef, int hashCode, TKey key, int level, out int newCount)
		{
			bool flag2;
			using (nodeRef.PinValue())
			{
				ScalableDictionaryNode scalableDictionaryNode = nodeRef.Value();
				bool flag = false;
				int num = this.HashToSlot(scalableDictionaryNode, hashCode, level);
				IScalableDictionaryEntry scalableDictionaryEntry = scalableDictionaryNode.Entries[num];
				if (scalableDictionaryEntry == null)
				{
					flag = false;
				}
				else
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType objectType = scalableDictionaryEntry.GetObjectType();
					if (objectType != Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionaryNodeReference)
					{
						if (objectType != Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionaryValues)
						{
							Global.Tracer.Assert(false, "Unknown ObjectType");
						}
						else
						{
							ScalableDictionaryValues scalableDictionaryValues = scalableDictionaryEntry as ScalableDictionaryValues;
							for (int i = 0; i < scalableDictionaryValues.Count; i++)
							{
								if (this.m_comparer.Equals(key, (TKey)((object)scalableDictionaryValues.Keys[i])))
								{
									if (scalableDictionaryValues.Count == 1)
									{
										scalableDictionaryNode.Entries[num] = null;
									}
									else
									{
										scalableDictionaryValues.Keys[i] = null;
										scalableDictionaryValues.Values[i] = null;
										ScalableDictionaryValues scalableDictionaryValues2 = scalableDictionaryValues;
										int count = scalableDictionaryValues2.Count;
										scalableDictionaryValues2.Count = count - 1;
										int num2 = scalableDictionaryValues.Count - i;
										if (num2 > 0)
										{
											Array.Copy(scalableDictionaryValues.Keys, i + 1, scalableDictionaryValues.Keys, i, num2);
											Array.Copy(scalableDictionaryValues.Values, i + 1, scalableDictionaryValues.Values, i, num2);
										}
									}
									flag = true;
									break;
								}
							}
						}
					}
					else
					{
						ScalableDictionaryNodeReference scalableDictionaryNodeReference = scalableDictionaryEntry as ScalableDictionaryNodeReference;
						int num3;
						flag = this.Remove(scalableDictionaryNodeReference, hashCode, key, level + 1, out num3);
						if (flag && num3 == 0)
						{
							scalableDictionaryNode.Entries[num] = null;
							scalableDictionaryNodeReference.Free();
						}
					}
				}
				if (flag)
				{
					scalableDictionaryNode.Count--;
				}
				newCount = scalableDictionaryNode.Count;
				flag2 = flag;
			}
			return flag2;
		}

		// Token: 0x170027FB RID: 10235
		// (get) Token: 0x0600784F RID: 30799 RVA: 0x001EFD74 File Offset: 0x001EDF74
		public int Size
		{
			get
			{
				return 8 + ItemSizes.ReferenceSize + 4 + 4 + ItemSizes.SizeOf(this.m_root) + ItemSizes.ReferenceSize + ItemSizes.ReferenceSize + ItemSizes.ReferenceSize + ItemSizes.ReferenceSize + 4 + 1;
			}
		}

		// Token: 0x06007850 RID: 30800 RVA: 0x001EFDAC File Offset: 0x001EDFAC
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ScalableDictionary<TKey, TValue>.m_declaration);
			IScalabilityCache scalabilityCache = writer.PersistenceHelper as IScalabilityCache;
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Count)
				{
					if (memberName != MemberName.Priority)
					{
						switch (memberName)
						{
						case MemberName.Version:
							writer.Write(this.m_version);
							continue;
						case MemberName.NodeCapacity:
							writer.Write(this.m_nodeCapacity);
							continue;
						case MemberName.ValuesCapacity:
							writer.Write(this.m_valuesCapacity);
							continue;
						case MemberName.Comparer:
						{
							int num = int.MinValue;
							if (scalabilityCache.CacheType == ScalabilityCacheType.Standard)
							{
								num = scalabilityCache.StoreStaticReference(this.m_comparer);
							}
							writer.Write(num);
							continue;
						}
						case MemberName.Root:
							writer.Write(this.m_root);
							continue;
						case MemberName.UseFixedReferences:
							writer.Write(this.m_useFixedReferences);
							continue;
						}
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_priority);
					}
				}
				else
				{
					writer.Write(this.m_count);
				}
			}
		}

		// Token: 0x06007851 RID: 30801 RVA: 0x001EFEDC File Offset: 0x001EE0DC
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ScalableDictionary<TKey, TValue>.m_declaration);
			IScalabilityCache scalabilityCache = reader.PersistenceHelper as IScalabilityCache;
			this.m_scalabilityCache = scalabilityCache;
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Count)
				{
					if (memberName != MemberName.Priority)
					{
						switch (memberName)
						{
						case MemberName.Version:
							this.m_version = reader.ReadInt32();
							continue;
						case MemberName.NodeCapacity:
							this.m_nodeCapacity = reader.ReadInt32();
							continue;
						case MemberName.ValuesCapacity:
							this.m_valuesCapacity = reader.ReadInt32();
							continue;
						case MemberName.Comparer:
						{
							int num = reader.ReadInt32();
							if (scalabilityCache.CacheType == ScalabilityCacheType.Standard)
							{
								this.m_comparer = (IEqualityComparer<TKey>)scalabilityCache.FetchStaticReference(num);
								continue;
							}
							continue;
						}
						case MemberName.Root:
							this.m_root = (ScalableDictionaryNodeReference)reader.ReadRIFObject();
							continue;
						case MemberName.UseFixedReferences:
							this.m_useFixedReferences = reader.ReadBoolean();
							continue;
						}
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_priority = reader.ReadInt32();
					}
				}
				else
				{
					this.m_count = reader.ReadInt32();
				}
			}
		}

		// Token: 0x06007852 RID: 30802 RVA: 0x001F0016 File Offset: 0x001EE216
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06007853 RID: 30803 RVA: 0x001F0018 File Offset: 0x001EE218
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionary;
		}

		// Token: 0x06007854 RID: 30804 RVA: 0x001F001C File Offset: 0x001EE21C
		public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (ScalableDictionary<TKey, TValue>.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionary, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.NodeCapacity, Token.Int32),
					new MemberInfo(MemberName.ValuesCapacity, Token.Int32),
					new MemberInfo(MemberName.Comparer, Token.Int32),
					new MemberInfo(MemberName.Count, Token.Int32),
					new MemberInfo(MemberName.Version, Token.Int32),
					new MemberInfo(MemberName.Root, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionaryNodeReference),
					new MemberInfo(MemberName.UseFixedReferences, Token.Boolean),
					new MemberInfo(MemberName.Priority, Token.Int32)
				});
			}
			return ScalableDictionary<TKey, TValue>.m_declaration;
		}

		// Token: 0x04003C86 RID: 15494
		private int m_nodeCapacity;

		// Token: 0x04003C87 RID: 15495
		private int m_valuesCapacity;

		// Token: 0x04003C88 RID: 15496
		[StaticReference]
		private IEqualityComparer<TKey> m_comparer;

		// Token: 0x04003C89 RID: 15497
		private int m_count;

		// Token: 0x04003C8A RID: 15498
		private int m_version;

		// Token: 0x04003C8B RID: 15499
		private ScalableDictionaryNodeReference m_root;

		// Token: 0x04003C8C RID: 15500
		private bool m_useFixedReferences;

		// Token: 0x04003C8D RID: 15501
		private int m_priority;

		// Token: 0x04003C8E RID: 15502
		[NonSerialized]
		private IScalabilityCache m_scalabilityCache;

		// Token: 0x04003C8F RID: 15503
		[NonSerialized]
		private ScalableDictionary<TKey, TValue>.ScalableDictionaryKeysCollection m_keysCollection;

		// Token: 0x04003C90 RID: 15504
		[NonSerialized]
		private ScalableDictionary<TKey, TValue>.ScalableDictionaryValuesCollection m_valuesCollection;

		// Token: 0x04003C91 RID: 15505
		[NonSerialized]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = ScalableDictionary<TKey, TValue>.GetDeclaration();

		// Token: 0x02000D0A RID: 3338
		internal struct ScalableDictionaryEnumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator
		{
			// Token: 0x06008E91 RID: 36497 RVA: 0x00245529 File Offset: 0x00243729
			internal ScalableDictionaryEnumerator(ScalableDictionary<TKey, TValue> dictionary)
			{
				this.m_dictionary = dictionary;
				this.m_version = dictionary.m_version;
				this.m_currentValueIndex = -1;
				this.m_currentPair = default(KeyValuePair<TKey, TValue>);
				this.m_context = null;
				this.Reset();
			}

			// Token: 0x17002BBB RID: 11195
			// (get) Token: 0x06008E92 RID: 36498 RVA: 0x00245560 File Offset: 0x00243760
			public KeyValuePair<TKey, TValue> Current
			{
				get
				{
					if (this.m_dictionary.m_version != this.m_version)
					{
						Global.Tracer.Assert(false, "ScalableDictionaryEnumerator: Cannot use enumerator after modifying the underlying collection");
					}
					if (this.m_context.Count < 1)
					{
						Global.Tracer.Assert(false, "ScalableDictionaryEnumerator: Enumerator beyond the bounds of the underlying collection");
					}
					return this.m_currentPair;
				}
			}

			// Token: 0x06008E93 RID: 36499 RVA: 0x002455B4 File Offset: 0x002437B4
			public void Dispose()
			{
				this.m_context = null;
				this.m_dictionary = null;
			}

			// Token: 0x17002BBC RID: 11196
			// (get) Token: 0x06008E94 RID: 36500 RVA: 0x002455C4 File Offset: 0x002437C4
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06008E95 RID: 36501 RVA: 0x002455D4 File Offset: 0x002437D4
			public bool MoveNext()
			{
				if (this.m_dictionary.m_version != this.m_version)
				{
					Global.Tracer.Assert(false, "ScalableDictionaryEnumerator: Cannot use enumerator after modifying the underlying collection");
				}
				if (this.m_context.Count < 1 && this.m_currentValueIndex != -1)
				{
					return false;
				}
				if (this.m_context.Count == 0)
				{
					ScalableDictionary<TKey, TValue>.ScalableDictionaryEnumerator.ContextItem<int, ScalableDictionaryNodeReference> contextItem = new ScalableDictionary<TKey, TValue>.ScalableDictionaryEnumerator.ContextItem<int, ScalableDictionaryNodeReference>(0, this.m_dictionary.m_root);
					this.m_currentValueIndex = 0;
					this.m_context.Push(contextItem);
				}
				return this.FindNext();
			}

			// Token: 0x06008E96 RID: 36502 RVA: 0x00245658 File Offset: 0x00243858
			private bool FindNext()
			{
				bool flag = false;
				while (this.m_context.Count > 0 && !flag)
				{
					ScalableDictionary<TKey, TValue>.ScalableDictionaryEnumerator.ContextItem<int, ScalableDictionaryNodeReference> contextItem = this.m_context.Peek();
					ScalableDictionaryNodeReference value = contextItem.Value;
					using (value.PinValue())
					{
						flag = this.FindNext(value.Value(), contextItem);
					}
				}
				return flag;
			}

			// Token: 0x06008E97 RID: 36503 RVA: 0x002456C0 File Offset: 0x002438C0
			private bool FindNext(ScalableDictionaryNode node, ScalableDictionary<TKey, TValue>.ScalableDictionaryEnumerator.ContextItem<int, ScalableDictionaryNodeReference> curContext)
			{
				bool flag = false;
				while (!flag && curContext.Key < node.Entries.Length)
				{
					IScalableDictionaryEntry scalableDictionaryEntry = node.Entries[curContext.Key];
					if (scalableDictionaryEntry != null)
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType objectType = scalableDictionaryEntry.GetObjectType();
						if (objectType != Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionaryNodeReference)
						{
							if (objectType != Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionaryValues)
							{
								Global.Tracer.Assert(false, "Unknown ObjectType");
							}
							else
							{
								ScalableDictionaryValues scalableDictionaryValues = scalableDictionaryEntry as ScalableDictionaryValues;
								if (this.m_currentValueIndex < scalableDictionaryValues.Count)
								{
									this.m_currentPair = new KeyValuePair<TKey, TValue>((TKey)((object)scalableDictionaryValues.Keys[this.m_currentValueIndex]), (TValue)((object)scalableDictionaryValues.Values[this.m_currentValueIndex]));
									this.m_currentValueIndex++;
									return true;
								}
								this.m_currentValueIndex = 0;
							}
						}
						else
						{
							ScalableDictionaryNodeReference scalableDictionaryNodeReference = scalableDictionaryEntry as ScalableDictionaryNodeReference;
							this.m_context.Push(new ScalableDictionary<TKey, TValue>.ScalableDictionaryEnumerator.ContextItem<int, ScalableDictionaryNodeReference>(0, scalableDictionaryNodeReference));
							flag = this.FindNext();
						}
					}
					curContext.Key++;
				}
				if (!flag)
				{
					this.m_currentValueIndex = 0;
					this.m_context.Pop();
				}
				return flag;
			}

			// Token: 0x06008E98 RID: 36504 RVA: 0x002457C8 File Offset: 0x002439C8
			public void Reset()
			{
				this.m_currentValueIndex = -1;
				this.m_context = new Stack<ScalableDictionary<TKey, TValue>.ScalableDictionaryEnumerator.ContextItem<int, ScalableDictionaryNodeReference>>();
				this.m_version = this.m_dictionary.m_version;
			}

			// Token: 0x0400502E RID: 20526
			private int m_currentValueIndex;

			// Token: 0x0400502F RID: 20527
			private KeyValuePair<TKey, TValue> m_currentPair;

			// Token: 0x04005030 RID: 20528
			private Stack<ScalableDictionary<TKey, TValue>.ScalableDictionaryEnumerator.ContextItem<int, ScalableDictionaryNodeReference>> m_context;

			// Token: 0x04005031 RID: 20529
			private int m_version;

			// Token: 0x04005032 RID: 20530
			private ScalableDictionary<TKey, TValue> m_dictionary;

			// Token: 0x02000D4E RID: 3406
			private class ContextItem<KeyType, ValueType>
			{
				// Token: 0x06008FE4 RID: 36836 RVA: 0x00247F1C File Offset: 0x0024611C
				public ContextItem(KeyType key, ValueType value)
				{
					this.Key = key;
					this.Value = value;
				}

				// Token: 0x04005100 RID: 20736
				public KeyType Key;

				// Token: 0x04005101 RID: 20737
				public ValueType Value;
			}
		}

		// Token: 0x02000D0B RID: 3339
		internal struct ScalableDictionaryKeysEnumerator : IEnumerator<TKey>, IDisposable, IEnumerator
		{
			// Token: 0x06008E99 RID: 36505 RVA: 0x002457ED File Offset: 0x002439ED
			internal ScalableDictionaryKeysEnumerator(ScalableDictionary<TKey, TValue> dictionary)
			{
				this.m_dictionary = dictionary;
				this.m_enumerator = dictionary.GetEnumerator();
			}

			// Token: 0x17002BBD RID: 11197
			// (get) Token: 0x06008E9A RID: 36506 RVA: 0x00245804 File Offset: 0x00243A04
			public TKey Current
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.m_enumerator.Current;
					return keyValuePair.Key;
				}
			}

			// Token: 0x06008E9B RID: 36507 RVA: 0x00245824 File Offset: 0x00243A24
			public void Dispose()
			{
			}

			// Token: 0x17002BBE RID: 11198
			// (get) Token: 0x06008E9C RID: 36508 RVA: 0x00245826 File Offset: 0x00243A26
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06008E9D RID: 36509 RVA: 0x00245833 File Offset: 0x00243A33
			public bool MoveNext()
			{
				return this.m_enumerator.MoveNext();
			}

			// Token: 0x06008E9E RID: 36510 RVA: 0x00245840 File Offset: 0x00243A40
			public void Reset()
			{
				this.m_enumerator.Reset();
			}

			// Token: 0x04005033 RID: 20531
			private ScalableDictionary<TKey, TValue> m_dictionary;

			// Token: 0x04005034 RID: 20532
			private IEnumerator<KeyValuePair<TKey, TValue>> m_enumerator;
		}

		// Token: 0x02000D0C RID: 3340
		internal struct ScalableDictionaryValuesEnumerator : IEnumerator<TValue>, IDisposable, IEnumerator
		{
			// Token: 0x06008E9F RID: 36511 RVA: 0x0024584D File Offset: 0x00243A4D
			internal ScalableDictionaryValuesEnumerator(ScalableDictionary<TKey, TValue> dictionary)
			{
				this.m_dictionary = dictionary;
				this.m_enumerator = dictionary.GetEnumerator();
			}

			// Token: 0x17002BBF RID: 11199
			// (get) Token: 0x06008EA0 RID: 36512 RVA: 0x00245864 File Offset: 0x00243A64
			public TValue Current
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.m_enumerator.Current;
					return keyValuePair.Value;
				}
			}

			// Token: 0x06008EA1 RID: 36513 RVA: 0x00245884 File Offset: 0x00243A84
			public void Dispose()
			{
			}

			// Token: 0x17002BC0 RID: 11200
			// (get) Token: 0x06008EA2 RID: 36514 RVA: 0x00245886 File Offset: 0x00243A86
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06008EA3 RID: 36515 RVA: 0x00245893 File Offset: 0x00243A93
			public bool MoveNext()
			{
				return this.m_enumerator.MoveNext();
			}

			// Token: 0x06008EA4 RID: 36516 RVA: 0x002458A0 File Offset: 0x00243AA0
			public void Reset()
			{
				this.m_enumerator.Reset();
			}

			// Token: 0x04005035 RID: 20533
			private ScalableDictionary<TKey, TValue> m_dictionary;

			// Token: 0x04005036 RID: 20534
			private IEnumerator<KeyValuePair<TKey, TValue>> m_enumerator;
		}

		// Token: 0x02000D0D RID: 3341
		internal sealed class ScalableDictionaryKeysCollection : ICollection<TKey>, IEnumerable<TKey>, IEnumerable
		{
			// Token: 0x06008EA5 RID: 36517 RVA: 0x002458AD File Offset: 0x00243AAD
			internal ScalableDictionaryKeysCollection(ScalableDictionary<TKey, TValue> dictionary)
			{
				this.m_dictionary = dictionary;
			}

			// Token: 0x06008EA6 RID: 36518 RVA: 0x002458BC File Offset: 0x00243ABC
			public void Add(TKey item)
			{
				Global.Tracer.Assert(false, "ScalableDictionaryKeysCollection: Dictionary keys collection is read only");
			}

			// Token: 0x06008EA7 RID: 36519 RVA: 0x002458CE File Offset: 0x00243ACE
			public void Clear()
			{
				Global.Tracer.Assert(false, "ScalableDictionaryKeysCollection: Dictionary keys collection is read only");
			}

			// Token: 0x06008EA8 RID: 36520 RVA: 0x002458E0 File Offset: 0x00243AE0
			public bool Contains(TKey item)
			{
				return this.m_dictionary.ContainsKey(item);
			}

			// Token: 0x06008EA9 RID: 36521 RVA: 0x002458F0 File Offset: 0x00243AF0
			public void CopyTo(TKey[] array, int arrayIndex)
			{
				if (arrayIndex < 0)
				{
					Global.Tracer.Assert(false, "ScalableDictionaryKeysCollection.CopyTo: Index must be greater than 0");
				}
				if (array == null)
				{
					Global.Tracer.Assert(false, "ScalableDictionaryKeysCollection.CopyTo: Specified array must not be null");
				}
				if (array.Rank > 1)
				{
					Global.Tracer.Assert(false, "ScalableDictionaryKeysCollection.CopyTo: Specified array must be 1 dimensional");
				}
				if (arrayIndex + this.Count > array.Length)
				{
					Global.Tracer.Assert(false, "ScalableDictionaryKeysCollection.CopyTo: Insufficent space in destination array");
				}
				foreach (TKey tkey in this)
				{
					array[arrayIndex] = tkey;
					arrayIndex++;
				}
			}

			// Token: 0x17002BC1 RID: 11201
			// (get) Token: 0x06008EAA RID: 36522 RVA: 0x0024599C File Offset: 0x00243B9C
			public int Count
			{
				get
				{
					return this.m_dictionary.Count;
				}
			}

			// Token: 0x17002BC2 RID: 11202
			// (get) Token: 0x06008EAB RID: 36523 RVA: 0x002459A9 File Offset: 0x00243BA9
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06008EAC RID: 36524 RVA: 0x002459AC File Offset: 0x00243BAC
			public bool Remove(TKey item)
			{
				Global.Tracer.Assert(false, "ScalableDictionaryKeysCollection.Remove: Dictionary keys collection is read only");
				return false;
			}

			// Token: 0x06008EAD RID: 36525 RVA: 0x002459BF File Offset: 0x00243BBF
			public IEnumerator<TKey> GetEnumerator()
			{
				return new ScalableDictionary<TKey, TValue>.ScalableDictionaryKeysEnumerator(this.m_dictionary);
			}

			// Token: 0x06008EAE RID: 36526 RVA: 0x002459D1 File Offset: 0x00243BD1
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04005037 RID: 20535
			private ScalableDictionary<TKey, TValue> m_dictionary;
		}

		// Token: 0x02000D0E RID: 3342
		internal sealed class ScalableDictionaryValuesCollection : ICollection<TValue>, IEnumerable<TValue>, IEnumerable
		{
			// Token: 0x06008EAF RID: 36527 RVA: 0x002459D9 File Offset: 0x00243BD9
			internal ScalableDictionaryValuesCollection(ScalableDictionary<TKey, TValue> dictionary)
			{
				this.m_dictionary = dictionary;
			}

			// Token: 0x06008EB0 RID: 36528 RVA: 0x002459E8 File Offset: 0x00243BE8
			public void Add(TValue item)
			{
				Global.Tracer.Assert(false, "ScalableDictionaryValuesCollection.Add: Dictionary values collection is read only");
			}

			// Token: 0x06008EB1 RID: 36529 RVA: 0x002459FA File Offset: 0x00243BFA
			public void Clear()
			{
				Global.Tracer.Assert(false, "ScalableDictionaryValuesCollection.Clear: Dictionary values collection is read only");
			}

			// Token: 0x06008EB2 RID: 36530 RVA: 0x00245A0C File Offset: 0x00243C0C
			public bool Contains(TValue item)
			{
				return this.m_dictionary.ContainsValue(item);
			}

			// Token: 0x06008EB3 RID: 36531 RVA: 0x00245A1C File Offset: 0x00243C1C
			public void CopyTo(TValue[] array, int arrayIndex)
			{
				if (arrayIndex < 0)
				{
					Global.Tracer.Assert(false, "ScalableDictionaryValuesCollection.CopyTo: Index must be greater than 0");
				}
				if (array == null)
				{
					Global.Tracer.Assert(false, "ScalableDictionaryValuesCollection.CopyTo: Specified array must not be null");
				}
				if (array.Rank > 1)
				{
					Global.Tracer.Assert(false, "ScalableDictionaryValuesCollection.CopyTo: Specified array must be 1 dimensional");
				}
				if (arrayIndex + this.Count > array.Length)
				{
					Global.Tracer.Assert(false, "ScalableDictionaryValuesCollection.CopyTo: Insufficent space in destination array");
				}
				foreach (TValue tvalue in this)
				{
					array[arrayIndex] = tvalue;
					arrayIndex++;
				}
			}

			// Token: 0x17002BC3 RID: 11203
			// (get) Token: 0x06008EB4 RID: 36532 RVA: 0x00245AC8 File Offset: 0x00243CC8
			public int Count
			{
				get
				{
					return this.m_dictionary.Count;
				}
			}

			// Token: 0x17002BC4 RID: 11204
			// (get) Token: 0x06008EB5 RID: 36533 RVA: 0x00245AD5 File Offset: 0x00243CD5
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06008EB6 RID: 36534 RVA: 0x00245AD8 File Offset: 0x00243CD8
			public bool Remove(TValue item)
			{
				Global.Tracer.Assert(false, "ScalableDictionaryValuesCollection.Remove: Dictionary values collection is read only");
				return false;
			}

			// Token: 0x06008EB7 RID: 36535 RVA: 0x00245AEB File Offset: 0x00243CEB
			public IEnumerator<TValue> GetEnumerator()
			{
				return new ScalableDictionary<TKey, TValue>.ScalableDictionaryValuesEnumerator(this.m_dictionary);
			}

			// Token: 0x06008EB8 RID: 36536 RVA: 0x00245AFD File Offset: 0x00243CFD
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04005038 RID: 20536
			private ScalableDictionary<TKey, TValue> m_dictionary;
		}
	}
}
