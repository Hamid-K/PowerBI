using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008C7 RID: 2247
	[PersistedWithinRequestOnly]
	internal sealed class RuntimeGroupingObjTree : RuntimeGroupingObj
	{
		// Token: 0x06007B17 RID: 31511 RVA: 0x001FAF2D File Offset: 0x001F912D
		internal RuntimeGroupingObjTree()
		{
		}

		// Token: 0x06007B18 RID: 31512 RVA: 0x001FAF38 File Offset: 0x001F9138
		internal RuntimeGroupingObjTree(RuntimeHierarchyObj owner, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType)
			: base(owner, objectType)
		{
			OnDemandProcessingContext odpContext = this.m_owner.OdpContext;
			this.m_tree = new BTree(owner, odpContext, owner.Depth + 1);
		}

		// Token: 0x17002875 RID: 10357
		// (get) Token: 0x06007B19 RID: 31513 RVA: 0x001FAF6E File Offset: 0x001F916E
		internal override BTree Tree
		{
			get
			{
				return this.m_tree;
			}
		}

		// Token: 0x06007B1A RID: 31514 RVA: 0x001FAF76 File Offset: 0x001F9176
		internal override void Cleanup()
		{
			if (this.m_tree != null)
			{
				this.m_tree.Dispose();
				this.m_tree = null;
			}
		}

		// Token: 0x06007B1B RID: 31515 RVA: 0x001FAF92 File Offset: 0x001F9192
		internal override void NextRow(object keyValue, bool hasParent, object parentKey)
		{
			this.m_tree.NextRow(keyValue, this.m_owner);
		}

		// Token: 0x06007B1C RID: 31516 RVA: 0x001FAFA6 File Offset: 0x001F91A6
		internal override void Traverse(ProcessingStages operation, bool ascending, ITraversalContext traversalContext)
		{
			this.m_tree.Traverse(operation, ascending, traversalContext);
		}

		// Token: 0x06007B1D RID: 31517 RVA: 0x001FAFB6 File Offset: 0x001F91B6
		internal override void CopyDomainScopeGroupInstances(RuntimeGroupRootObj destination)
		{
			Global.Tracer.Assert(false, "Domain Scope should only be applied to Hash groups");
		}

		// Token: 0x06007B1E RID: 31518 RVA: 0x001FAFC8 File Offset: 0x001F91C8
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(RuntimeGroupingObjTree.m_declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.Tree)
				{
					writer.Write(this.m_tree);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06007B1F RID: 31519 RVA: 0x001FB020 File Offset: 0x001F9220
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(RuntimeGroupingObjTree.m_declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.Tree)
				{
					this.m_tree = (BTree)reader.ReadRIFObject();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06007B20 RID: 31520 RVA: 0x001FB07D File Offset: 0x001F927D
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06007B21 RID: 31521 RVA: 0x001FB07F File Offset: 0x001F927F
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObjTree;
		}

		// Token: 0x06007B22 RID: 31522 RVA: 0x001FB088 File Offset: 0x001F9288
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeGroupingObjTree.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObjTree, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObj, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Tree, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BTree)
				});
			}
			return RuntimeGroupingObjTree.m_declaration;
		}

		// Token: 0x17002876 RID: 10358
		// (get) Token: 0x06007B23 RID: 31523 RVA: 0x001FB0C7 File Offset: 0x001F92C7
		public override int Size
		{
			get
			{
				return base.Size + ItemSizes.SizeOf(this.m_tree);
			}
		}

		// Token: 0x04003D66 RID: 15718
		private BTree m_tree;

		// Token: 0x04003D67 RID: 15719
		[NonSerialized]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeGroupingObjTree.GetDeclaration();
	}
}
