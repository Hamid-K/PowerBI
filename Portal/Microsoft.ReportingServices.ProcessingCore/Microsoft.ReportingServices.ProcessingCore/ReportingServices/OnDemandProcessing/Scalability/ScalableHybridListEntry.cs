using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000899 RID: 2201
	internal sealed class ScalableHybridListEntry : IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x17002808 RID: 10248
		// (get) Token: 0x06007888 RID: 30856 RVA: 0x001F08C6 File Offset: 0x001EEAC6
		public int Size
		{
			get
			{
				return ItemSizes.SizeOf(this.Item) + 4 + 4;
			}
		}

		// Token: 0x06007889 RID: 30857 RVA: 0x001F08D8 File Offset: 0x001EEAD8
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ScalableHybridListEntry.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Item)
				{
					if (memberName != MemberName.NextLeaf)
					{
						if (memberName != MemberName.PrevLeaf)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							writer.Write(this.Previous);
						}
					}
					else
					{
						writer.Write(this.Next);
					}
				}
				else
				{
					writer.WriteVariantOrPersistable(this.Item);
				}
			}
		}

		// Token: 0x0600788A RID: 30858 RVA: 0x001F095C File Offset: 0x001EEB5C
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ScalableHybridListEntry.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Item)
				{
					if (memberName != MemberName.NextLeaf)
					{
						if (memberName != MemberName.PrevLeaf)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							this.Previous = reader.ReadInt32();
						}
					}
					else
					{
						this.Next = reader.ReadInt32();
					}
				}
				else
				{
					this.Item = reader.ReadVariant();
				}
			}
		}

		// Token: 0x0600788B RID: 30859 RVA: 0x001F09DF File Offset: 0x001EEBDF
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false, "No references to resolve");
		}

		// Token: 0x0600788C RID: 30860 RVA: 0x001F09F1 File Offset: 0x001EEBF1
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableHybridListEntry;
		}

		// Token: 0x0600788D RID: 30861 RVA: 0x001F09F8 File Offset: 0x001EEBF8
		public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (ScalableHybridListEntry.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableHybridListEntry, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Item, Token.Object),
					new MemberInfo(MemberName.NextLeaf, Token.Int32),
					new MemberInfo(MemberName.PrevLeaf, Token.Int32)
				});
			}
			return ScalableHybridListEntry.m_declaration;
		}

		// Token: 0x04003CA2 RID: 15522
		internal object Item;

		// Token: 0x04003CA3 RID: 15523
		internal int Next;

		// Token: 0x04003CA4 RID: 15524
		internal int Previous;

		// Token: 0x04003CA5 RID: 15525
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = ScalableHybridListEntry.GetDeclaration();
	}
}
