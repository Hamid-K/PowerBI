using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000880 RID: 2176
	internal class StorageItem : ItemHolder, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060077D7 RID: 30679 RVA: 0x001EED3B File Offset: 0x001ECF3B
		public StorageItem()
		{
		}

		// Token: 0x060077D8 RID: 30680 RVA: 0x001EED4C File Offset: 0x001ECF4C
		public StorageItem(ReferenceID id, int priority, IStorable item, int initialSize)
		{
			this.Priority = priority;
			this.Item = item;
			this.Offset = -1L;
			this.Id = id;
			this.PersistedSize = -1L;
			this.m_size = this.CalculateSize(initialSize);
		}

		// Token: 0x060077D9 RID: 30681 RVA: 0x001EED9A File Offset: 0x001ECF9A
		public void AddReference(BaseReference newReference)
		{
			if (this.Reference == null)
			{
				this.Reference = newReference;
				return;
			}
			if (this.m_otherReferences == null)
			{
				this.m_otherReferences = new LinkedBucketedQueue<BaseReference>(5);
			}
			this.m_otherReferences.Enqueue(newReference);
		}

		// Token: 0x170027E2 RID: 10210
		// (get) Token: 0x060077DA RID: 30682 RVA: 0x001EEDD2 File Offset: 0x001ECFD2
		public int Size
		{
			get
			{
				return this.m_size;
			}
		}

		// Token: 0x170027E3 RID: 10211
		// (get) Token: 0x060077DB RID: 30683 RVA: 0x001EEDDC File Offset: 0x001ECFDC
		internal int ActiveReferenceCount
		{
			get
			{
				int num = 0;
				if (this.Reference != null)
				{
					num++;
				}
				if (this.m_otherReferences != null)
				{
					num += this.m_otherReferences.Count;
				}
				return num;
			}
		}

		// Token: 0x060077DC RID: 30684 RVA: 0x001EEE14 File Offset: 0x001ED014
		public int UpdateSize()
		{
			int size = this.m_size;
			this.m_size = this.CalculateSize(ItemSizes.SizeOf(this.Item));
			return this.m_size - size;
		}

		// Token: 0x060077DD RID: 30685 RVA: 0x001EEE47 File Offset: 0x001ED047
		private int CalculateSize(int itemSize)
		{
			return base.BaseSize() + itemSize + 8 + 8 + 4 + 4 + 1 + 8 + ItemSizes.ReferenceSize;
		}

		// Token: 0x060077DE RID: 30686 RVA: 0x001EEE63 File Offset: 0x001ED063
		internal override int ComputeSizeForReference()
		{
			return 0;
		}

		// Token: 0x060077DF RID: 30687 RVA: 0x001EEE66 File Offset: 0x001ED066
		public void UpdateSize(int sizeDeltaBytes)
		{
			this.m_size += sizeDeltaBytes;
		}

		// Token: 0x060077E0 RID: 30688 RVA: 0x001EEE78 File Offset: 0x001ED078
		public void Flush(IStorage storage, IIndexStrategy indexStrategy)
		{
			bool isTemporary = this.Id.IsTemporary;
			if (isTemporary)
			{
				this.Id = indexStrategy.GenerateId(this.Id);
			}
			this.UnlinkReferences(isTemporary);
			if (this.Offset >= 0L)
			{
				long num = storage.Update(this, this.Offset, this.PersistedSize);
				if (num != this.Offset)
				{
					this.Offset = num;
					indexStrategy.Update(this.Id, this.Offset);
					return;
				}
			}
			else
			{
				this.Offset = storage.Allocate(this);
				indexStrategy.Update(this.Id, this.Offset);
			}
		}

		// Token: 0x060077E1 RID: 30689 RVA: 0x001EEF10 File Offset: 0x001ED110
		internal void UnlinkReferences(bool updateId)
		{
			if (this.Reference != null)
			{
				if (updateId)
				{
					this.Reference.Id = this.Id;
				}
				this.Reference.Item = null;
			}
			if (this.m_otherReferences != null)
			{
				while (this.m_otherReferences.Count > 0)
				{
					BaseReference baseReference = this.m_otherReferences.Dequeue();
					baseReference.Item = null;
					if (updateId)
					{
						baseReference.Id = this.Id;
					}
				}
			}
			this.Reference = null;
			this.m_otherReferences = null;
		}

		// Token: 0x060077E2 RID: 30690 RVA: 0x001EEF94 File Offset: 0x001ED194
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(StorageItem.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.ID)
				{
					if (memberName != MemberName.Item)
					{
						if (memberName != MemberName.Priority)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							writer.Write(this.Priority);
						}
					}
					else
					{
						writer.Write(this.Item);
					}
				}
				else
				{
					writer.Write(this.Id.Value);
				}
			}
		}

		// Token: 0x060077E3 RID: 30691 RVA: 0x001EF018 File Offset: 0x001ED218
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(StorageItem.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.ID)
				{
					if (memberName != MemberName.Item)
					{
						if (memberName != MemberName.Priority)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							this.Priority = reader.ReadInt32();
						}
					}
					else
					{
						this.Item = (IStorable)reader.ReadRIFObject();
					}
				}
				else
				{
					this.Id = new ReferenceID(reader.ReadInt64());
				}
			}
		}

		// Token: 0x060077E4 RID: 30692 RVA: 0x001EF0A0 File Offset: 0x001ED2A0
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x060077E5 RID: 30693 RVA: 0x001EF0A2 File Offset: 0x001ED2A2
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StorageItem;
		}

		// Token: 0x060077E6 RID: 30694 RVA: 0x001EF0A8 File Offset: 0x001ED2A8
		[SkipMemberStaticValidation(MemberName.Item)]
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StorageItem, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Priority, Token.Int32),
				new MemberInfo(MemberName.Item, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObject),
				new MemberInfo(MemberName.ID, Token.Int64)
			});
		}

		// Token: 0x04003C73 RID: 15475
		internal int Priority;

		// Token: 0x04003C74 RID: 15476
		internal ReferenceID Id;

		// Token: 0x04003C75 RID: 15477
		[NonSerialized]
		internal long PersistedSize;

		// Token: 0x04003C76 RID: 15478
		[NonSerialized]
		private int m_size;

		// Token: 0x04003C77 RID: 15479
		[NonSerialized]
		internal int PinCount;

		// Token: 0x04003C78 RID: 15480
		[NonSerialized]
		internal bool HasBeenUnPinned;

		// Token: 0x04003C79 RID: 15481
		[NonSerialized]
		internal long Offset = -1L;

		// Token: 0x04003C7A RID: 15482
		[NonSerialized]
		private LinkedBucketedQueue<BaseReference> m_otherReferences;

		// Token: 0x04003C7B RID: 15483
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = StorageItem.GetDeclaration();
	}
}
