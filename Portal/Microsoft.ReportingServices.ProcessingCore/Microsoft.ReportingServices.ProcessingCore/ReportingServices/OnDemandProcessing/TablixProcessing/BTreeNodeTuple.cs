using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008CF RID: 2255
	[PersistedWithinRequestOnly]
	internal sealed class BTreeNodeTuple : IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007B78 RID: 31608 RVA: 0x001FBFCD File Offset: 0x001FA1CD
		internal BTreeNodeTuple()
		{
		}

		// Token: 0x06007B79 RID: 31609 RVA: 0x001FBFDC File Offset: 0x001FA1DC
		internal BTreeNodeTuple(BTreeNodeValue value, int childIndex)
		{
			this.m_value = value;
			this.m_childIndex = childIndex;
		}

		// Token: 0x17002880 RID: 10368
		// (get) Token: 0x06007B7A RID: 31610 RVA: 0x001FBFF9 File Offset: 0x001FA1F9
		internal BTreeNodeValue Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x17002881 RID: 10369
		// (get) Token: 0x06007B7B RID: 31611 RVA: 0x001FC001 File Offset: 0x001FA201
		// (set) Token: 0x06007B7C RID: 31612 RVA: 0x001FC009 File Offset: 0x001FA209
		internal int ChildIndex
		{
			get
			{
				return this.m_childIndex;
			}
			set
			{
				this.m_childIndex = value;
			}
		}

		// Token: 0x06007B7D RID: 31613 RVA: 0x001FC014 File Offset: 0x001FA214
		internal void Traverse(ProcessingStages operation, bool ascending, ScalableList<BTreeNode> nodeList, ITraversalContext traversalContext)
		{
			if (ascending)
			{
				if (this.m_value != null)
				{
					this.m_value.Traverse(operation, traversalContext);
				}
				this.VisitChild(operation, ascending, nodeList, traversalContext);
				return;
			}
			this.VisitChild(operation, ascending, nodeList, traversalContext);
			if (this.m_value != null)
			{
				this.m_value.Traverse(operation, traversalContext);
			}
		}

		// Token: 0x06007B7E RID: 31614 RVA: 0x001FC068 File Offset: 0x001FA268
		internal void VisitChild(ProcessingStages operation, bool ascending, ScalableList<BTreeNode> nodeList, ITraversalContext traversalContext)
		{
			if (-1 != this.m_childIndex)
			{
				BTreeNode btreeNode;
				using (nodeList.GetAndPin(this.m_childIndex, out btreeNode))
				{
					btreeNode.Traverse(operation, ascending, nodeList, traversalContext);
				}
			}
		}

		// Token: 0x06007B7F RID: 31615 RVA: 0x001FC0B4 File Offset: 0x001FA2B4
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(BTreeNodeTuple.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Value)
				{
					if (memberName != MemberName.Child)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_childIndex);
					}
				}
				else
				{
					writer.Write(this.m_value);
				}
			}
		}

		// Token: 0x06007B80 RID: 31616 RVA: 0x001FC11C File Offset: 0x001FA31C
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(BTreeNodeTuple.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Value)
				{
					if (memberName != MemberName.Child)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_childIndex = reader.ReadInt32();
					}
				}
				else
				{
					this.m_value = (BTreeNodeValue)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x06007B81 RID: 31617 RVA: 0x001FC187 File Offset: 0x001FA387
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06007B82 RID: 31618 RVA: 0x001FC189 File Offset: 0x001FA389
		Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BTreeNodeTuple;
		}

		// Token: 0x06007B83 RID: 31619 RVA: 0x001FC190 File Offset: 0x001FA390
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (BTreeNodeTuple.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BTreeNodeTuple, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Value, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BTreeNodeValue),
					new MemberInfo(MemberName.Child, Token.Int32)
				});
			}
			return BTreeNodeTuple.m_declaration;
		}

		// Token: 0x17002882 RID: 10370
		// (get) Token: 0x06007B84 RID: 31620 RVA: 0x001FC1DA File Offset: 0x001FA3DA
		public int Size
		{
			get
			{
				return ItemSizes.SizeOf(this.m_value) + 4;
			}
		}

		// Token: 0x04003D76 RID: 15734
		private BTreeNodeValue m_value;

		// Token: 0x04003D77 RID: 15735
		private int m_childIndex = -1;

		// Token: 0x04003D78 RID: 15736
		[NonSerialized]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = BTreeNodeTuple.GetDeclaration();
	}
}
