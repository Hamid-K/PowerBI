using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008CD RID: 2253
	[PersistedWithinRequestOnly]
	public sealed class BTree : IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IDisposable
	{
		// Token: 0x06007B5C RID: 31580 RVA: 0x001FB77B File Offset: 0x001F997B
		internal BTree()
		{
		}

		// Token: 0x06007B5D RID: 31581 RVA: 0x001FB783 File Offset: 0x001F9983
		internal BTree(IHierarchyObj owner, OnDemandProcessingContext odpContext, int level)
		{
			this.m_nodes = new ScalableList<BTreeNode>(level, odpContext.TablixProcessingScalabilityCache);
			this.m_root = new BTreeNode(owner);
		}

		// Token: 0x06007B5E RID: 31582 RVA: 0x001FB7AC File Offset: 0x001F99AC
		internal void NextRow(object keyValue, IHierarchyObj owner)
		{
			try
			{
				BTreeNodeValue btreeNodeValue;
				int num;
				int num2;
				if (!this.m_root.SearchAndInsert(keyValue, this.m_nodes, owner, out btreeNodeValue, out num, out num2))
				{
					int count = this.m_nodes.Count;
					this.m_nodes.Add(this.m_root);
					this.m_root = new BTreeNode(owner);
					this.m_root.SetFirstChild(this.m_nodes, count);
					this.m_root.Tuples.Add(new BTreeNodeTuple(btreeNodeValue, num2), this.m_nodes);
				}
			}
			catch (ReportProcessingException_SpatialTypeComparisonError)
			{
				throw new ReportProcessingException(owner.RegisterComparisonError("SortExpression"));
			}
			catch (ReportProcessingException_ComparisonError)
			{
				throw new ReportProcessingException(owner.RegisterComparisonError("SortExpression"));
			}
		}

		// Token: 0x06007B5F RID: 31583 RVA: 0x001FB870 File Offset: 0x001F9A70
		internal void Traverse(ProcessingStages operation, bool ascending, ITraversalContext traversalContext)
		{
			this.m_root.Traverse(operation, ascending, this.m_nodes, traversalContext);
		}

		// Token: 0x06007B60 RID: 31584 RVA: 0x001FB886 File Offset: 0x001F9A86
		public void Dispose()
		{
			this.m_nodes.Dispose();
			this.m_nodes = null;
		}

		// Token: 0x06007B61 RID: 31585 RVA: 0x001FB89C File Offset: 0x001F9A9C
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(BTree.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Root)
				{
					if (memberName != MemberName.SortTreeNodes)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_nodes);
					}
				}
				else
				{
					writer.Write(this.m_root);
				}
			}
		}

		// Token: 0x06007B62 RID: 31586 RVA: 0x001FB908 File Offset: 0x001F9B08
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(BTree.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Root)
				{
					if (memberName != MemberName.SortTreeNodes)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_nodes = reader.ReadRIFObject<ScalableList<BTreeNode>>();
					}
				}
				else
				{
					this.m_root = (BTreeNode)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x06007B63 RID: 31587 RVA: 0x001FB979 File Offset: 0x001F9B79
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06007B64 RID: 31588 RVA: 0x001FB97B File Offset: 0x001F9B7B
		Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BTree;
		}

		// Token: 0x06007B65 RID: 31589 RVA: 0x001FB980 File Offset: 0x001F9B80
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (BTree.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BTree, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Root, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BTreeNode),
					new MemberInfo(MemberName.SortTreeNodes, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BTreeNode)
				});
			}
			return BTree.m_declaration;
		}

		// Token: 0x1700287C RID: 10364
		// (get) Token: 0x06007B66 RID: 31590 RVA: 0x001FB9CF File Offset: 0x001F9BCF
		public int Size
		{
			get
			{
				return ItemSizes.SizeOf(this.m_root) + ItemSizes.SizeOf<BTreeNode>(this.m_nodes);
			}
		}

		// Token: 0x04003D6F RID: 15727
		private BTreeNode m_root;

		// Token: 0x04003D70 RID: 15728
		private ScalableList<BTreeNode> m_nodes;

		// Token: 0x04003D71 RID: 15729
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = BTree.GetDeclaration();
	}
}
