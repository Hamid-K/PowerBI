using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008CB RID: 2251
	[PersistedWithinRequestOnly]
	internal sealed class RuntimeGroupingObjNaturalGroup : RuntimeGroupingObjLinkedList
	{
		// Token: 0x06007B4A RID: 31562 RVA: 0x001FB494 File Offset: 0x001F9694
		internal RuntimeGroupingObjNaturalGroup()
		{
		}

		// Token: 0x06007B4B RID: 31563 RVA: 0x001FB49C File Offset: 0x001F969C
		internal RuntimeGroupingObjNaturalGroup(RuntimeHierarchyObj owner, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType)
			: base(owner, objectType)
		{
		}

		// Token: 0x06007B4C RID: 31564 RVA: 0x001FB4A8 File Offset: 0x001F96A8
		internal override void NextRow(object keyValue, bool hasParent, object parentKey)
		{
			if (this.m_lastChild != null && this.m_owner.OdpContext.EqualityComparer.Equals(this.m_lastValue, keyValue))
			{
				using (this.m_lastChild.PinValue())
				{
					this.m_lastChild.Value().NextRow();
					return;
				}
			}
			this.m_lastValue = keyValue;
			this.m_lastChild = base.CreateHierarchyObjAndAddToParent();
		}

		// Token: 0x06007B4D RID: 31565 RVA: 0x001FB528 File Offset: 0x001F9728
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(RuntimeGroupingObjNaturalGroup.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.LastChild)
				{
					if (memberName == MemberName.LastValue)
					{
						writer.Write(this.m_lastValue);
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
				else
				{
					writer.Write(this.m_lastChild);
				}
			}
		}

		// Token: 0x06007B4E RID: 31566 RVA: 0x001FB59C File Offset: 0x001F979C
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(RuntimeGroupingObjNaturalGroup.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.LastChild)
				{
					if (memberName == MemberName.LastValue)
					{
						this.m_lastValue = reader.ReadVariant();
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
				else
				{
					this.m_lastChild = (IReference<RuntimeHierarchyObj>)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x06007B4F RID: 31567 RVA: 0x001FB612 File Offset: 0x001F9812
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06007B50 RID: 31568 RVA: 0x001FB614 File Offset: 0x001F9814
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObjNaturalGroup;
		}

		// Token: 0x06007B51 RID: 31569 RVA: 0x001FB61C File Offset: 0x001F981C
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeGroupingObjNaturalGroup.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObjNaturalGroup, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObjLinkedList, new List<MemberInfo>
				{
					new MemberInfo(MemberName.LastValue, Token.Object),
					new MemberInfo(MemberName.LastChild, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeHierarchyObjReference)
				});
			}
			return RuntimeGroupingObjNaturalGroup.m_declaration;
		}

		// Token: 0x1700287A RID: 10362
		// (get) Token: 0x06007B52 RID: 31570 RVA: 0x001FB66F File Offset: 0x001F986F
		public override int Size
		{
			get
			{
				return base.Size + ItemSizes.SizeOf(this.m_lastValue) + ItemSizes.SizeOf(this.m_lastChild);
			}
		}

		// Token: 0x04003D6B RID: 15723
		private object m_lastValue;

		// Token: 0x04003D6C RID: 15724
		private IReference<RuntimeHierarchyObj> m_lastChild;

		// Token: 0x04003D6D RID: 15725
		[NonSerialized]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeGroupingObjNaturalGroup.GetDeclaration();
	}
}
