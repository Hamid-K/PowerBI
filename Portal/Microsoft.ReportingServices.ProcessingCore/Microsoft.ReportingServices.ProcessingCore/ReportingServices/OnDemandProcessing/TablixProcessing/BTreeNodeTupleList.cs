using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008D2 RID: 2258
	[PersistedWithinRequestOnly]
	internal sealed class BTreeNodeTupleList : IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007B9C RID: 31644 RVA: 0x001FC3BB File Offset: 0x001FA5BB
		internal BTreeNodeTupleList()
		{
		}

		// Token: 0x06007B9D RID: 31645 RVA: 0x001FC3C3 File Offset: 0x001FA5C3
		internal BTreeNodeTupleList(int capacity)
		{
			this.m_list = new List<BTreeNodeTuple>(capacity);
			this.m_capacity = capacity;
		}

		// Token: 0x17002887 RID: 10375
		internal BTreeNodeTuple this[int index]
		{
			get
			{
				return this.m_list[index];
			}
		}

		// Token: 0x17002888 RID: 10376
		// (get) Token: 0x06007B9F RID: 31647 RVA: 0x001FC3EC File Offset: 0x001FA5EC
		internal int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		// Token: 0x06007BA0 RID: 31648 RVA: 0x001FC3FC File Offset: 0x001FA5FC
		internal void Add(BTreeNodeTuple tuple, ScalableList<BTreeNode> nodes)
		{
			if (this.m_list.Count == this.m_capacity)
			{
				throw new InvalidOperationException();
			}
			this.m_list.Add(tuple);
			if (-1 != tuple.ChildIndex)
			{
				BTreeNode btreeNode;
				using (nodes.GetAndPin(tuple.ChildIndex, out btreeNode))
				{
					btreeNode.IndexInParent = this.m_list.Count - 1;
				}
			}
		}

		// Token: 0x06007BA1 RID: 31649 RVA: 0x001FC478 File Offset: 0x001FA678
		internal void Insert(int index, BTreeNodeTuple tuple, ScalableList<BTreeNode> nodes)
		{
			if (this.m_list.Count == this.m_capacity)
			{
				throw new InvalidOperationException();
			}
			this.m_list.Insert(index, tuple);
			for (int i = index; i < this.m_list.Count; i++)
			{
				int childIndex = this.m_list[i].ChildIndex;
				if (childIndex != -1)
				{
					BTreeNode btreeNode;
					using (nodes.GetAndPin(childIndex, out btreeNode))
					{
						btreeNode.IndexInParent = i;
					}
				}
			}
		}

		// Token: 0x06007BA2 RID: 31650 RVA: 0x001FC504 File Offset: 0x001FA704
		internal void RemoveAtEnd()
		{
			this.m_list.RemoveAt(this.m_list.Count - 1);
		}

		// Token: 0x06007BA3 RID: 31651 RVA: 0x001FC520 File Offset: 0x001FA720
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(BTreeNodeTupleList.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.List)
				{
					if (memberName != MemberName.Capacity)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_capacity);
					}
				}
				else
				{
					writer.Write<BTreeNodeTuple>(this.m_list);
				}
			}
		}

		// Token: 0x06007BA4 RID: 31652 RVA: 0x001FC588 File Offset: 0x001FA788
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(BTreeNodeTupleList.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.List)
				{
					if (memberName != MemberName.Capacity)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_capacity = reader.ReadInt32();
					}
				}
				else
				{
					this.m_list = reader.ReadListOfRIFObjects<List<BTreeNodeTuple>>();
				}
			}
		}

		// Token: 0x06007BA5 RID: 31653 RVA: 0x001FC5EE File Offset: 0x001FA7EE
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06007BA6 RID: 31654 RVA: 0x001FC5F0 File Offset: 0x001FA7F0
		Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BTreeNodeTupleList;
		}

		// Token: 0x06007BA7 RID: 31655 RVA: 0x001FC5F4 File Offset: 0x001FA7F4
		public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (BTreeNodeTupleList.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BTreeNodeTupleList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.List, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BTreeNodeTuple),
					new MemberInfo(MemberName.Capacity, Token.Int32)
				});
			}
			return BTreeNodeTupleList.m_declaration;
		}

		// Token: 0x17002889 RID: 10377
		// (get) Token: 0x06007BA8 RID: 31656 RVA: 0x001FC63F File Offset: 0x001FA83F
		public int Size
		{
			get
			{
				return ItemSizes.SizeOf<BTreeNodeTuple>(this.m_list) + 4;
			}
		}

		// Token: 0x04003D7C RID: 15740
		private List<BTreeNodeTuple> m_list;

		// Token: 0x04003D7D RID: 15741
		private int m_capacity;

		// Token: 0x04003D7E RID: 15742
		[NonSerialized]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = BTreeNodeTupleList.GetDeclaration();
	}
}
