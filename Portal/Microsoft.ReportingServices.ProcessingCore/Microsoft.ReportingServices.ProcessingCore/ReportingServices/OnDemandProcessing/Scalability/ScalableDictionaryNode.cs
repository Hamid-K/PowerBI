using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000895 RID: 2197
	internal sealed class ScalableDictionaryNode : IScalableDictionaryEntry, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, ITransferable
	{
		// Token: 0x06007859 RID: 30809 RVA: 0x001F010D File Offset: 0x001EE30D
		internal ScalableDictionaryNode()
		{
		}

		// Token: 0x0600785A RID: 30810 RVA: 0x001F0115 File Offset: 0x001EE315
		internal ScalableDictionaryNode(int capacity)
		{
			this.Entries = new IScalableDictionaryEntry[capacity];
		}

		// Token: 0x170027FC RID: 10236
		// (get) Token: 0x0600785B RID: 30811 RVA: 0x001F0129 File Offset: 0x001EE329
		public int Size
		{
			get
			{
				return 4 + ItemSizes.SizeOf<IScalableDictionaryEntry>(this.Entries);
			}
		}

		// Token: 0x170027FD RID: 10237
		// (get) Token: 0x0600785C RID: 30812 RVA: 0x001F0138 File Offset: 0x001EE338
		public int EmptySize
		{
			get
			{
				return ItemSizes.NonNullIStorableOverhead + 4 + ItemSizes.SizeOfEmptyObjectArray(this.Entries.Length);
			}
		}

		// Token: 0x0600785D RID: 30813 RVA: 0x001F0150 File Offset: 0x001EE350
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ScalableDictionaryNode.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Count)
				{
					if (memberName == MemberName.Entries)
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable[] entries = this.Entries;
						writer.Write(entries);
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
				else
				{
					writer.Write(this.Count);
				}
			}
		}

		// Token: 0x0600785E RID: 30814 RVA: 0x001F01BC File Offset: 0x001EE3BC
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ScalableDictionaryNode.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Count)
				{
					if (memberName == MemberName.Entries)
					{
						this.Entries = reader.ReadArrayOfRIFObjects<IScalableDictionaryEntry>();
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
				else
				{
					this.Count = reader.ReadInt32();
				}
			}
		}

		// Token: 0x0600785F RID: 30815 RVA: 0x001F0226 File Offset: 0x001EE426
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06007860 RID: 30816 RVA: 0x001F0228 File Offset: 0x001EE428
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionaryNode;
		}

		// Token: 0x06007861 RID: 30817 RVA: 0x001F022C File Offset: 0x001EE42C
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (ScalableDictionaryNode.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionaryNode, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Entries, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectArray, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IScalableDictionaryEntry),
					new MemberInfo(MemberName.Count, Token.Int32)
				});
			}
			return ScalableDictionaryNode.m_declaration;
		}

		// Token: 0x06007862 RID: 30818 RVA: 0x001F0280 File Offset: 0x001EE480
		public void TransferTo(IScalabilityCache scaleCache)
		{
			for (int i = 0; i < this.Entries.Length; i++)
			{
				IScalableDictionaryEntry scalableDictionaryEntry = this.Entries[i];
				if (scalableDictionaryEntry != null)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType objectType = scalableDictionaryEntry.GetObjectType();
					if (objectType != Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionaryNodeReference)
					{
						if (objectType != Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionaryValues)
						{
							Global.Tracer.Assert(false, "Unknown ObjectType");
						}
						else
						{
							(scalableDictionaryEntry as ScalableDictionaryValues).TransferTo(scaleCache);
						}
					}
					else
					{
						ScalableDictionaryNodeReference scalableDictionaryNodeReference = scalableDictionaryEntry as ScalableDictionaryNodeReference;
						this.Entries[i] = (ScalableDictionaryNodeReference)scalableDictionaryNodeReference.TransferTo(scaleCache);
					}
				}
			}
		}

		// Token: 0x04003C92 RID: 15506
		internal IScalableDictionaryEntry[] Entries;

		// Token: 0x04003C93 RID: 15507
		internal int Count;

		// Token: 0x04003C94 RID: 15508
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = ScalableDictionaryNode.GetDeclaration();
	}
}
