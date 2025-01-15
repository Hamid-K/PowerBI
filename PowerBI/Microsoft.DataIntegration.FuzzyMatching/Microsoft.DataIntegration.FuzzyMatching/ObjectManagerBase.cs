using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data.Sql;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000D8 RID: 216
	public abstract class ObjectManagerBase : IObjectManager
	{
		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x00028B5A File Offset: 0x00026D5A
		// (set) Token: 0x06000890 RID: 2192 RVA: 0x00028B62 File Offset: 0x00026D62
		public bool EnableCollectionTimer { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x00028B6B File Offset: 0x00026D6B
		// (set) Token: 0x06000892 RID: 2194 RVA: 0x00028B73 File Offset: 0x00026D73
		public int CollectionFrequency { get; set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x00028B7C File Offset: 0x00026D7C
		// (set) Token: 0x06000894 RID: 2196 RVA: 0x00028B84 File Offset: 0x00026D84
		public int DefaultTimeout { get; set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x00028B8D File Offset: 0x00026D8D
		// (set) Token: 0x06000896 RID: 2198 RVA: 0x00028B95 File Offset: 0x00026D95
		public string SchemaName { get; set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x00028B9E File Offset: 0x00026D9E
		public bool CollectionTimerActive
		{
			get
			{
				return this.m_collectionTimer != null;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x00028BA9 File Offset: 0x00026DA9
		public DateTime LastCollectionTime
		{
			get
			{
				return this.m_lastCollectTime;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x00028BB1 File Offset: 0x00026DB1
		public int CollectionCount
		{
			get
			{
				return this.m_collectionCount;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x00028BB9 File Offset: 0x00026DB9
		public int ObjectHandleCount
		{
			get
			{
				return this.m_objectHandlesById.Count;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x00028BC6 File Offset: 0x00026DC6
		public int ObjectReferenceCount
		{
			get
			{
				return this.m_referencesByName.Count;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x00028BD3 File Offset: 0x00026DD3
		// (set) Token: 0x0600089D RID: 2205 RVA: 0x00028BDB File Offset: 0x00026DDB
		public int SignatureGeneratorPoolSize { get; set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x00028BE4 File Offset: 0x00026DE4
		// (set) Token: 0x0600089F RID: 2207 RVA: 0x00028BEC File Offset: 0x00026DEC
		public int ComparerPoolSize { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x00028BF5 File Offset: 0x00026DF5
		// (set) Token: 0x060008A1 RID: 2209 RVA: 0x00028BFD File Offset: 0x00026DFD
		public int RecordContextBuilderPoolSize { get; set; }

		// Token: 0x060008A2 RID: 2210 RVA: 0x00028C08 File Offset: 0x00026E08
		public ObjectManagerBase()
		{
			this.EnableCollectionTimer = false;
			this.CollectionFrequency = 10000;
			this.DefaultTimeout = 60000;
			this.SignatureGeneratorPoolSize = GlobalConfiguration.SignatureGeneratorPoolSize;
			this.ComparerPoolSize = GlobalConfiguration.ComparerPoolSize;
			this.RecordContextBuilderPoolSize = GlobalConfiguration.RecordContextBuilderPoolSize;
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00028C83 File Offset: 0x00026E83
		private void OnTimer(object arg)
		{
			this.Collect();
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00028C8B File Offset: 0x00026E8B
		public IEnumerable<KeyValuePair<string, ObjectReference>> Objects()
		{
			return this.m_referencesByName;
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00028C94 File Offset: 0x00026E94
		public void Release(int handle)
		{
			ObjectManagerBase.ValidateHandle(handle);
			lock (this)
			{
				TemporalHandle temporalHandle;
				if (this.m_objectHandlesById.TryGetValue(handle, ref temporalHandle))
				{
					this.m_objectHandlesById.Remove(handle);
					temporalHandle.Reference.Handles.Remove(temporalHandle);
					if (temporalHandle.Reference.Handles.Count == 0)
					{
						temporalHandle.Reference.Weaken();
					}
				}
			}
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00028D14 File Offset: 0x00026F14
		private static void ValidateHandle(int handle)
		{
			byte b = (byte)AppDomain.CurrentDomain.Id;
			byte b2 = (byte)(handle >> 24);
			if (b != b2)
			{
				throw new Exception("Handle was created in another AppDomain and is no longer valid.");
			}
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00028D40 File Offset: 0x00026F40
		protected int NextObjectHandle()
		{
			int num3;
			lock (this)
			{
				byte b = (byte)AppDomain.CurrentDomain.Id;
				int num = 0;
				for (;;)
				{
					int num2 = (int)b << 24;
					int nextObjectHandle = this.m_nextObjectHandle;
					this.m_nextObjectHandle = nextObjectHandle + 1;
					num3 = num2 | nextObjectHandle;
					if (this.m_nextObjectHandle >= 16777216)
					{
						this.m_nextObjectHandle = 1;
					}
					if (num++ == 16777216)
					{
						break;
					}
					if (!this.m_objectHandlesById.ContainsKey(num3))
					{
						goto Block_5;
					}
				}
				throw new Exception("Out of object handles!");
				Block_5:;
			}
			return num3;
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00028DD0 File Offset: 0x00026FD0
		public virtual string GetQualifiedObjectName(string schemaName, string objectName)
		{
			return new SqlName(schemaName, objectName).QualifiedName;
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00028DDE File Offset: 0x00026FDE
		public virtual string GetQualifiedObjectName(string objectName)
		{
			return this.GetQualifiedObjectName(this.SchemaName, objectName);
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00028DED File Offset: 0x00026FED
		public bool Contains(string objectName)
		{
			return this.m_referencesByName.ContainsKey(this.GetQualifiedObjectName(objectName));
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x00028E04 File Offset: 0x00027004
		public int CreateReference(object obj)
		{
			return this.CreateReference(Guid.NewGuid().ToString(), obj);
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00028E2B File Offset: 0x0002702B
		public int CreateReference(string objectName, object obj)
		{
			return this.CreateReference(objectName, obj, this.DefaultTimeout);
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00028E3C File Offset: 0x0002703C
		public int CreateReference(string objectName, object obj, int timeout)
		{
			int id;
			lock (this)
			{
				if (string.IsNullOrEmpty(objectName))
				{
					throw new ArgumentException("objectName may not be null or empty.");
				}
				string qualifiedObjectName = this.GetQualifiedObjectName(objectName);
				if (this.m_referencesByName.ContainsKey(qualifiedObjectName))
				{
					throw new ArgumentException(string.Format("An object with name {0} already exists.", qualifiedObjectName));
				}
				DateTime now = DateTime.Now;
				ObjectReference objectReference = new ObjectReference
				{
					SqlSchemaName = this.SchemaName,
					Name = objectName,
					Type = obj.GetType(),
					StrongReference = obj,
					LastAccessed = now
				};
				TemporalHandle temporalHandle = new TemporalHandle
				{
					Reference = objectReference,
					Id = this.NextObjectHandle(),
					LastAccessed = now,
					Timeout = timeout
				};
				objectReference.Handles.Add(temporalHandle);
				this.m_referencesByName.Add(qualifiedObjectName, objectReference);
				this.m_objectHandlesById.Add(temporalHandle.Id, temporalHandle);
				if (this.m_collectionTimer == null && this.EnableCollectionTimer)
				{
					this.m_collectionTimer = new Timer(new TimerCallback(this.OnTimer), null, 0, this.CollectionFrequency);
				}
				id = temporalHandle.Id;
			}
			return id;
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x00028F7C File Offset: 0x0002717C
		protected ObjectReference GetReference(string objectName)
		{
			return this.m_referencesByName[this.GetQualifiedObjectName(objectName)];
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00028F90 File Offset: 0x00027190
		public int GetHandle(string objectName)
		{
			return this.GetObjectHandle(objectName).Id;
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00028F9E File Offset: 0x0002719E
		public int GetHandle(string objectName, int timeout)
		{
			return this.GetObjectHandle(objectName, timeout).Id;
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00028FAD File Offset: 0x000271AD
		public TemporalHandle GetObjectHandle(string objectName)
		{
			return this.GetObjectHandle(objectName, this.DefaultTimeout);
		}

		// Token: 0x060008B2 RID: 2226
		public abstract TemporalHandle GetObjectHandle(string objectName, int timeout);

		// Token: 0x060008B3 RID: 2227 RVA: 0x00028FBC File Offset: 0x000271BC
		public object GetInstanceState(int handle)
		{
			return this.GetObjectHandle(handle).Reference.InstanceState;
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00028FCF File Offset: 0x000271CF
		public void SetInstanceState(int handle, object instanceState)
		{
			this.GetObjectHandle(handle).Reference.InstanceState = instanceState;
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00028FE4 File Offset: 0x000271E4
		public TemporalHandle GetObjectHandle(int handle)
		{
			ObjectManagerBase.ValidateHandle(handle);
			TemporalHandle temporalHandle;
			lock (this)
			{
				if (!this.m_objectHandlesById.TryGetValue(handle, ref temporalHandle))
				{
					throw new InvalidOperationException("The specified handle is no longer valid.  Consider increasing the handle timeout.");
				}
			}
			temporalHandle.LastAccessed = DateTime.Now;
			return temporalHandle;
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00029040 File Offset: 0x00027240
		public string GetObjectName(int handle)
		{
			return this.GetObjectHandle(handle).Reference.Name;
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00029053 File Offset: 0x00027253
		public object GetObject(int handle)
		{
			return this.GetObject(this.GetObjectHandle(handle));
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00029062 File Offset: 0x00027262
		public object GetObject(string objectName)
		{
			return this.GetObject(this.GetObjectHandle(objectName));
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00029074 File Offset: 0x00027274
		public object GetObject(string objectName, out int handle)
		{
			TemporalHandle objectHandle = this.GetObjectHandle(objectName);
			handle = objectHandle.Id;
			return this.GetObject(objectHandle);
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00029098 File Offset: 0x00027298
		private object GetObject(TemporalHandle objectHandle)
		{
			object obj = objectHandle.Reference.TryGetStrongReference();
			if (obj == null)
			{
				throw new InvalidOperationException("The specified handle is no longer valid.  Consider increasing the handle timeout.");
			}
			return obj;
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x000290C0 File Offset: 0x000272C0
		public void Collect()
		{
			this.Collect(false, false);
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x000290CC File Offset: 0x000272CC
		public void Collect(ObjectReference reference, DateTime collectTime)
		{
			lock (this)
			{
				for (int i = 0; i < reference.Handles.Count; i++)
				{
					TemporalHandle temporalHandle = reference.Handles[i];
					if ((collectTime - temporalHandle.LastAccessed).TotalMilliseconds > (double)temporalHandle.Timeout)
					{
						this.m_objectHandlesById.Remove(temporalHandle.Id);
						reference.Handles.RemoveAt(i--);
					}
				}
			}
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0002915C File Offset: 0x0002735C
		public void Collect(bool forceCollection, bool releaseAllHandles)
		{
			if (forceCollection || ((DateTime.Now - this.m_lastCollectTime).TotalMilliseconds > (double)this.CollectionFrequency && GC.CollectionCount(0) > this.m_lastGcCollectCount))
			{
				this.m_lastGcCollectCount = GC.CollectionCount(0);
				this.m_lastCollectTime = DateTime.Now;
				DateTime dateTime = (releaseAllHandles ? DateTime.MaxValue : this.m_lastCollectTime);
				lock (this)
				{
					this.m_collectionCount++;
					int num = 0;
					List<ObjectReference> list = null;
					foreach (ObjectReference objectReference in this.m_referencesByName.Values)
					{
						this.Collect(objectReference, dateTime);
						if (objectReference.Handles.Count == 0)
						{
							objectReference.Weaken();
						}
						if (!objectReference.InMemory && objectReference.PersistedSize < 0L)
						{
							if (list == null)
							{
								list = new List<ObjectReference>();
							}
							list.Add(objectReference);
						}
						if (objectReference.PersistedSize >= 0L)
						{
							num++;
						}
					}
					if (list != null)
					{
						foreach (ObjectReference objectReference2 in list)
						{
							this.m_referencesByName.Remove(this.GetQualifiedObjectName(objectReference2.SqlSchemaName, objectReference2.Name));
						}
					}
					if (this.m_objectHandlesById.Count == 0 && this.m_referencesByName.Count == num && this.m_collectionTimer != null)
					{
						this.m_collectionTimer.Dispose();
						this.m_collectionTimer = null;
					}
				}
			}
		}

		// Token: 0x04000366 RID: 870
		internal Timer m_collectionTimer;

		// Token: 0x04000367 RID: 871
		internal DateTime m_lastCollectTime = new DateTime(0L);

		// Token: 0x04000368 RID: 872
		private int m_collectionCount;

		// Token: 0x04000369 RID: 873
		private int m_lastGcCollectCount;

		// Token: 0x0400036A RID: 874
		protected int m_nextObjectHandle = 1;

		// Token: 0x0400036B RID: 875
		protected Dictionary<int, TemporalHandle> m_objectHandlesById = new Dictionary<int, TemporalHandle>();

		// Token: 0x0400036C RID: 876
		protected Dictionary<string, ObjectReference> m_referencesByName = new Dictionary<string, ObjectReference>();
	}
}
