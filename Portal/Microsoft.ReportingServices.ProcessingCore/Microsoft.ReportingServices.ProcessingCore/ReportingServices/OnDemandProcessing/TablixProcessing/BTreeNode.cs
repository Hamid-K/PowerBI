using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008CE RID: 2254
	[PersistedWithinRequestOnly]
	internal sealed class BTreeNode : IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007B68 RID: 31592 RVA: 0x001FB9F4 File Offset: 0x001F9BF4
		internal BTreeNode()
		{
		}

		// Token: 0x06007B69 RID: 31593 RVA: 0x001FB9FC File Offset: 0x001F9BFC
		internal BTreeNode(IHierarchyObj owner)
		{
			this.m_tuples = new BTreeNodeTupleList(3);
			BTreeNodeTuple btreeNodeTuple = new BTreeNodeTuple(this.CreateBTreeNode(null, owner), -1);
			this.m_tuples.Add(btreeNodeTuple, null);
		}

		// Token: 0x1700287D RID: 10365
		// (set) Token: 0x06007B6A RID: 31594 RVA: 0x001FBA37 File Offset: 0x001F9C37
		internal int IndexInParent
		{
			set
			{
				this.m_indexInParent = value;
			}
		}

		// Token: 0x1700287E RID: 10366
		// (get) Token: 0x06007B6B RID: 31595 RVA: 0x001FBA40 File Offset: 0x001F9C40
		internal BTreeNodeTupleList Tuples
		{
			get
			{
				return this.m_tuples;
			}
		}

		// Token: 0x06007B6C RID: 31596 RVA: 0x001FBA48 File Offset: 0x001F9C48
		internal void Traverse(ProcessingStages operation, bool ascending, ScalableList<BTreeNode> nodes, ITraversalContext traversalContext)
		{
			if (ascending)
			{
				for (int i = 0; i < this.m_tuples.Count; i++)
				{
					this.m_tuples[i].Traverse(operation, ascending, nodes, traversalContext);
				}
				return;
			}
			for (int j = this.m_tuples.Count - 1; j >= 0; j--)
			{
				this.m_tuples[j].Traverse(operation, ascending, nodes, traversalContext);
			}
		}

		// Token: 0x06007B6D RID: 31597 RVA: 0x001FBAB4 File Offset: 0x001F9CB4
		internal void SetFirstChild(ScalableList<BTreeNode> nodes, int childIndex)
		{
			Global.Tracer.Assert(1 <= this.m_tuples.Count, "(1 <= m_tuples.Count)");
			this.m_tuples[0].ChildIndex = childIndex;
			if (childIndex != -1)
			{
				BTreeNode btreeNode;
				using (nodes.GetAndPin(childIndex, out btreeNode))
				{
					btreeNode.IndexInParent = 0;
				}
			}
		}

		// Token: 0x06007B6E RID: 31598 RVA: 0x001FBB24 File Offset: 0x001F9D24
		private BTreeNodeValue CreateBTreeNode(object key, IHierarchyObj owner)
		{
			return new BTreeNodeHierarchyObj(key, owner);
		}

		// Token: 0x06007B6F RID: 31599 RVA: 0x001FBB30 File Offset: 0x001F9D30
		internal bool SearchAndInsert(object keyValue, ScalableList<BTreeNode> nodes, IHierarchyObj owner, out BTreeNodeValue newSiblingValue, out int newSiblingIndex, out int globalNewSiblingIndex)
		{
			int num = -1;
			int i;
			for (i = 1; i < this.m_tuples.Count; i++)
			{
				num = this.m_tuples[i].Value.CompareTo(keyValue, owner.OdpContext);
				if (num >= 0)
				{
					break;
				}
			}
			if (num == 0)
			{
				this.m_tuples[i].Value.AddRow(owner);
			}
			else
			{
				int childIndex = this.m_tuples[i - 1].ChildIndex;
				if (childIndex == -1)
				{
					return this.InsertBTreeNode(nodes, this.CreateBTreeNode(keyValue, owner), i, -1, owner, out newSiblingValue, out newSiblingIndex, out globalNewSiblingIndex);
				}
				BTreeNode btreeNode;
				using (nodes.GetAndPin(childIndex, out btreeNode))
				{
					BTreeNodeValue btreeNodeValue;
					int num2;
					int num3;
					if (!btreeNode.SearchAndInsert(keyValue, nodes, owner, out btreeNodeValue, out num2, out num3))
					{
						return this.InsertBTreeNode(nodes, btreeNodeValue, num2, num3, owner, out newSiblingValue, out newSiblingIndex, out globalNewSiblingIndex);
					}
				}
			}
			newSiblingValue = null;
			newSiblingIndex = -1;
			globalNewSiblingIndex = -1;
			return true;
		}

		// Token: 0x06007B70 RID: 31600 RVA: 0x001FBC28 File Offset: 0x001F9E28
		private bool InsertBTreeNode(ScalableList<BTreeNode> nodes, BTreeNodeValue nodeValueToInsert, int nodeIndexToInsert, int globalNodeIndexToInsert, IHierarchyObj owner, out BTreeNodeValue newSiblingValue, out int newSiblingIndex, out int globalNewSibingIndex)
		{
			if (3 > this.m_tuples.Count)
			{
				this.m_tuples.Insert(nodeIndexToInsert, new BTreeNodeTuple(nodeValueToInsert, globalNodeIndexToInsert), nodes);
				newSiblingValue = null;
				newSiblingIndex = -1;
				globalNewSibingIndex = -1;
				return true;
			}
			int num = 2;
			BTreeNode btreeNode = new BTreeNode(owner);
			BTreeNodeValue btreeNodeValue;
			if (num < nodeIndexToInsert)
			{
				btreeNodeValue = this.m_tuples[num].Value;
				btreeNode.SetFirstChild(nodes, this.m_tuples[num].ChildIndex);
				for (int i = num + 1; i < ((this.m_tuples.Count <= nodeIndexToInsert) ? this.m_tuples.Count : nodeIndexToInsert); i++)
				{
					btreeNode.m_tuples.Add(this.m_tuples[i], nodes);
				}
				btreeNode.m_tuples.Add(new BTreeNodeTuple(nodeValueToInsert, globalNodeIndexToInsert), nodes);
				for (int j = nodeIndexToInsert; j < this.m_tuples.Count; j++)
				{
					btreeNode.m_tuples.Add(this.m_tuples[j], nodes);
				}
				int count = this.m_tuples.Count;
				for (int k = num; k < count; k++)
				{
					this.m_tuples.RemoveAtEnd();
				}
			}
			else if (num > nodeIndexToInsert)
			{
				btreeNodeValue = this.m_tuples[num - 1].Value;
				btreeNode.SetFirstChild(nodes, this.m_tuples[num - 1].ChildIndex);
				for (int l = num; l < this.m_tuples.Count; l++)
				{
					btreeNode.m_tuples.Add(this.m_tuples[l], nodes);
				}
				int count2 = this.m_tuples.Count;
				for (int m = num - 1; m < count2; m++)
				{
					this.m_tuples.RemoveAtEnd();
				}
				this.m_tuples.Insert(nodeIndexToInsert, new BTreeNodeTuple(nodeValueToInsert, globalNodeIndexToInsert), nodes);
			}
			else
			{
				btreeNodeValue = nodeValueToInsert;
				btreeNode.SetFirstChild(nodes, globalNodeIndexToInsert);
				for (int n = num; n < this.m_tuples.Count; n++)
				{
					btreeNode.m_tuples.Add(this.m_tuples[n], nodes);
				}
				int count3 = this.m_tuples.Count;
				for (int num2 = num; num2 < count3; num2++)
				{
					this.m_tuples.RemoveAtEnd();
				}
			}
			newSiblingValue = btreeNodeValue;
			newSiblingIndex = this.m_indexInParent + 1;
			globalNewSibingIndex = nodes.Count;
			nodes.Add(btreeNode);
			return false;
		}

		// Token: 0x06007B71 RID: 31601 RVA: 0x001FBE8C File Offset: 0x001FA08C
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(BTreeNode.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Tuples)
				{
					if (memberName != MemberName.IndexInParent)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_indexInParent);
					}
				}
				else
				{
					writer.Write(this.m_tuples);
				}
			}
		}

		// Token: 0x06007B72 RID: 31602 RVA: 0x001FBEF4 File Offset: 0x001FA0F4
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(BTreeNode.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Tuples)
				{
					if (memberName != MemberName.IndexInParent)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_indexInParent = reader.ReadInt32();
					}
				}
				else
				{
					this.m_tuples = (BTreeNodeTupleList)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x06007B73 RID: 31603 RVA: 0x001FBF5F File Offset: 0x001FA15F
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06007B74 RID: 31604 RVA: 0x001FBF61 File Offset: 0x001FA161
		Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BTreeNode;
		}

		// Token: 0x06007B75 RID: 31605 RVA: 0x001FBF68 File Offset: 0x001FA168
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (BTreeNode.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BTreeNode, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Tuples, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BTreeNodeTupleList),
					new MemberInfo(MemberName.IndexInParent, Token.Int32)
				});
			}
			return BTreeNode.m_declaration;
		}

		// Token: 0x1700287F RID: 10367
		// (get) Token: 0x06007B76 RID: 31606 RVA: 0x001FBFB2 File Offset: 0x001FA1B2
		public int Size
		{
			get
			{
				return ItemSizes.SizeOf(this.m_tuples) + 4;
			}
		}

		// Token: 0x04003D72 RID: 15730
		private const int BTreeOrder = 3;

		// Token: 0x04003D73 RID: 15731
		private BTreeNodeTupleList m_tuples;

		// Token: 0x04003D74 RID: 15732
		private int m_indexInParent;

		// Token: 0x04003D75 RID: 15733
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = BTreeNode.GetDeclaration();
	}
}
