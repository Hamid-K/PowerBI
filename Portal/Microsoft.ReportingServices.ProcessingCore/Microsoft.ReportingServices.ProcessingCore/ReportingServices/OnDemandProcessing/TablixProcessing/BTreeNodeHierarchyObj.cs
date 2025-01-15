using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008D1 RID: 2257
	[PersistedWithinRequestOnly]
	internal sealed class BTreeNodeHierarchyObj : BTreeNodeValue
	{
		// Token: 0x06007B90 RID: 31632 RVA: 0x001FC211 File Offset: 0x001FA411
		internal BTreeNodeHierarchyObj()
		{
		}

		// Token: 0x06007B91 RID: 31633 RVA: 0x001FC219 File Offset: 0x001FA419
		internal BTreeNodeHierarchyObj(object key, IHierarchyObj owner)
		{
			this.m_key = key;
			if (key != null)
			{
				this.m_hierarchyNode = owner.CreateHierarchyObjForSortTree();
				this.m_hierarchyNode.NextRow(owner);
			}
		}

		// Token: 0x17002885 RID: 10373
		// (get) Token: 0x06007B92 RID: 31634 RVA: 0x001FC243 File Offset: 0x001FA443
		protected override object Key
		{
			get
			{
				return this.m_key;
			}
		}

		// Token: 0x06007B93 RID: 31635 RVA: 0x001FC24B File Offset: 0x001FA44B
		internal override void AddRow(IHierarchyObj owner)
		{
			this.m_hierarchyNode.NextRow(owner);
		}

		// Token: 0x06007B94 RID: 31636 RVA: 0x001FC259 File Offset: 0x001FA459
		internal override void Traverse(ProcessingStages operation, ITraversalContext traversalContext)
		{
			if (this.m_hierarchyNode != null)
			{
				this.m_hierarchyNode.Traverse(operation, traversalContext);
			}
		}

		// Token: 0x06007B95 RID: 31637 RVA: 0x001FC270 File Offset: 0x001FA470
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(BTreeNodeHierarchyObj.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Key)
				{
					if (memberName != MemberName.HierarchyNode)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_hierarchyNode);
					}
				}
				else
				{
					writer.Write(this.m_key);
				}
			}
		}

		// Token: 0x06007B96 RID: 31638 RVA: 0x001FC2D8 File Offset: 0x001FA4D8
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(BTreeNodeHierarchyObj.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Key)
				{
					if (memberName != MemberName.HierarchyNode)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_hierarchyNode = (IHierarchyObj)reader.ReadRIFObject();
					}
				}
				else
				{
					this.m_key = reader.ReadVariant();
				}
			}
		}

		// Token: 0x06007B97 RID: 31639 RVA: 0x001FC343 File Offset: 0x001FA543
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06007B98 RID: 31640 RVA: 0x001FC345 File Offset: 0x001FA545
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BTreeNodeHierarchyObj;
		}

		// Token: 0x06007B99 RID: 31641 RVA: 0x001FC34C File Offset: 0x001FA54C
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (BTreeNodeHierarchyObj.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BTreeNodeHierarchyObj, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BTreeNodeValue, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Key, Token.Object),
					new MemberInfo(MemberName.HierarchyNode, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IHierarchyObj)
				});
			}
			return BTreeNodeHierarchyObj.m_declaration;
		}

		// Token: 0x17002886 RID: 10374
		// (get) Token: 0x06007B9A RID: 31642 RVA: 0x001FC396 File Offset: 0x001FA596
		public override int Size
		{
			get
			{
				return ItemSizes.SizeOf(this.m_key) + ItemSizes.SizeOf(this.m_hierarchyNode);
			}
		}

		// Token: 0x04003D79 RID: 15737
		private object m_key;

		// Token: 0x04003D7A RID: 15738
		private IHierarchyObj m_hierarchyNode;

		// Token: 0x04003D7B RID: 15739
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = BTreeNodeHierarchyObj.GetDeclaration();
	}
}
