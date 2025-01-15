using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200084D RID: 2125
	public abstract class BaseReference : IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IDisposable
	{
		// Token: 0x06007696 RID: 30358 RVA: 0x001EB61D File Offset: 0x001E981D
		internal void Init(BaseScalabilityCache storageManager)
		{
			this.SetScalabilityCache(storageManager);
		}

		// Token: 0x06007697 RID: 30359 RVA: 0x001EB626 File Offset: 0x001E9826
		internal void Init(BaseScalabilityCache storageManager, ReferenceID id)
		{
			this.SetScalabilityCache(storageManager);
			this.m_id = id;
		}

		// Token: 0x170027B9 RID: 10169
		// (get) Token: 0x06007698 RID: 30360 RVA: 0x001EB636 File Offset: 0x001E9836
		// (set) Token: 0x06007699 RID: 30361 RVA: 0x001EB63E File Offset: 0x001E983E
		public ReferenceID Id
		{
			get
			{
				return this.m_id;
			}
			set
			{
				this.m_id = value;
			}
		}

		// Token: 0x170027BA RID: 10170
		// (get) Token: 0x0600769A RID: 30362 RVA: 0x001EB647 File Offset: 0x001E9847
		// (set) Token: 0x0600769B RID: 30363 RVA: 0x001EB64F File Offset: 0x001E984F
		internal int PinCount
		{
			get
			{
				return this.m_pinCount;
			}
			set
			{
				this.m_pinCount = value;
			}
		}

		// Token: 0x170027BB RID: 10171
		// (get) Token: 0x0600769C RID: 30364 RVA: 0x001EB658 File Offset: 0x001E9858
		internal BaseScalabilityCache ScalabilityCache
		{
			get
			{
				return this.m_scalabilityCache;
			}
		}

		// Token: 0x170027BC RID: 10172
		// (get) Token: 0x0600769D RID: 30365 RVA: 0x001EB660 File Offset: 0x001E9860
		// (set) Token: 0x0600769E RID: 30366 RVA: 0x001EB677 File Offset: 0x001E9877
		internal InQueueState InQueue
		{
			get
			{
				if (this.Item != null)
				{
					return this.Item.InQueue;
				}
				return InQueueState.None;
			}
			set
			{
				this.Item.InQueue = value;
			}
		}

		// Token: 0x0600769F RID: 30367 RVA: 0x001EB685 File Offset: 0x001E9885
		public IReference TransferTo(IScalabilityCache scaleCache)
		{
			return ((BaseScalabilityCache)scaleCache).TransferTo(this);
		}

		// Token: 0x060076A0 RID: 30368 RVA: 0x001EB693 File Offset: 0x001E9893
		public IDisposable PinValue()
		{
			this.m_pinCount++;
			this.m_scalabilityCache.Pin(this);
			return this;
		}

		// Token: 0x060076A1 RID: 30369 RVA: 0x001EB6B0 File Offset: 0x001E98B0
		public void UnPinValue()
		{
			this.m_pinCount--;
			this.m_scalabilityCache.UnPin(this);
		}

		// Token: 0x060076A2 RID: 30370 RVA: 0x001EB6CC File Offset: 0x001E98CC
		public void Free()
		{
			this.m_scalabilityCache.Free(this);
		}

		// Token: 0x060076A3 RID: 30371 RVA: 0x001EB6DA File Offset: 0x001E98DA
		public void UpdateSize(int sizeBytesDelta)
		{
			this.m_scalabilityCache.UpdateTargetSize(this, sizeBytesDelta);
		}

		// Token: 0x060076A4 RID: 30372 RVA: 0x001EB6EC File Offset: 0x001E98EC
		[DebuggerStepThrough]
		internal IStorable InternalValue()
		{
			IStorable storable;
			if (this.Item != null)
			{
				storable = this.Item.Item;
				this.m_scalabilityCache.ReferenceValueCallback(this);
			}
			else
			{
				storable = this.m_scalabilityCache.Retrieve(this);
			}
			return storable;
		}

		// Token: 0x060076A5 RID: 30373 RVA: 0x001EB729 File Offset: 0x001E9929
		private void SetScalabilityCache(BaseScalabilityCache cache)
		{
			this.m_scalabilityCache = cache;
		}

		// Token: 0x060076A6 RID: 30374 RVA: 0x001EB734 File Offset: 0x001E9934
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			this.m_scalabilityCache.ReferenceSerializeCallback(this);
			long num = this.m_id.Value;
			if (writer.PersistenceHelper != this.m_scalabilityCache)
			{
				BaseScalabilityCache baseScalabilityCache = writer.PersistenceHelper as BaseScalabilityCache;
				PairObj<ReferenceID, BaseScalabilityCache> pairObj = new PairObj<ReferenceID, BaseScalabilityCache>(this.m_id, this.m_scalabilityCache);
				num = (long)baseScalabilityCache.StoreStaticReference(pairObj);
			}
			writer.RegisterDeclaration(BaseReference.m_declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.ID)
				{
					writer.Write(num);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x060076A7 RID: 30375 RVA: 0x001EB7C8 File Offset: 0x001E99C8
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(BaseReference.m_declaration);
			long num = 0L;
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.ID)
				{
					num = reader.ReadInt64();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
			BaseScalabilityCache baseScalabilityCache = reader.PersistenceHelper as BaseScalabilityCache;
			ScalabilityCacheType cacheType = baseScalabilityCache.CacheType;
			if (num < 0L && cacheType != ScalabilityCacheType.GroupTree && cacheType != ScalabilityCacheType.Lookup)
			{
				PairObj<ReferenceID, BaseScalabilityCache> pairObj = (PairObj<ReferenceID, BaseScalabilityCache>)baseScalabilityCache.FetchStaticReference((int)num);
				this.m_id = pairObj.First;
				baseScalabilityCache = pairObj.Second;
			}
			else
			{
				this.m_id = new ReferenceID(num);
			}
			this.SetScalabilityCache(baseScalabilityCache);
		}

		// Token: 0x060076A8 RID: 30376 RVA: 0x001EB869 File Offset: 0x001E9A69
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x060076A9 RID: 30377
		public abstract Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType();

		// Token: 0x060076AA RID: 30378 RVA: 0x001EB878 File Offset: 0x001E9A78
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Reference, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Reference, new List<MemberInfo>
			{
				new MemberInfo(MemberName.ID, Token.Int64)
			});
		}

		// Token: 0x060076AB RID: 30379 RVA: 0x001EB8A6 File Offset: 0x001E9AA6
		void IDisposable.Dispose()
		{
			this.UnPinValue();
		}

		// Token: 0x170027BD RID: 10173
		// (get) Token: 0x060076AC RID: 30380 RVA: 0x001EB8B0 File Offset: 0x001E9AB0
		public int Size
		{
			get
			{
				int num = 16 + ItemSizes.ReferenceSize + 4 + ItemSizes.ReferenceSize;
				if (this.Item != null)
				{
					num += this.Item.ComputeSizeForReference();
				}
				return num;
			}
		}

		// Token: 0x060076AD RID: 30381 RVA: 0x001EB8E5 File Offset: 0x001E9AE5
		public static bool operator ==(BaseReference reference, object obj)
		{
			return reference == obj || (reference != null && reference.Equals(obj));
		}

		// Token: 0x060076AE RID: 30382 RVA: 0x001EB8F9 File Offset: 0x001E9AF9
		public static bool operator !=(BaseReference reference, object obj)
		{
			return !(reference == obj);
		}

		// Token: 0x060076AF RID: 30383 RVA: 0x001EB908 File Offset: 0x001E9B08
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			BaseReference baseReference = obj as BaseReference;
			return !(baseReference == null) && this.m_id == baseReference.m_id;
		}

		// Token: 0x060076B0 RID: 30384 RVA: 0x001EB93D File Offset: 0x001E9B3D
		public override int GetHashCode()
		{
			return (int)this.m_id.Value;
		}

		// Token: 0x04003C0D RID: 15373
		private ReferenceID m_id;

		// Token: 0x04003C0E RID: 15374
		[NonSerialized]
		protected BaseScalabilityCache m_scalabilityCache;

		// Token: 0x04003C0F RID: 15375
		[NonSerialized]
		internal ItemHolder Item;

		// Token: 0x04003C10 RID: 15376
		[NonSerialized]
		private int m_pinCount;

		// Token: 0x04003C11 RID: 15377
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = BaseReference.GetDeclaration();
	}
}
