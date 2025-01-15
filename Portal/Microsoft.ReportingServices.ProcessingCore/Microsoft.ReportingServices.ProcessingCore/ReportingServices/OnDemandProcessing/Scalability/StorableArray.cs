using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x020008A5 RID: 2213
	internal sealed class StorableArray : IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, ITransferable
	{
		// Token: 0x17002825 RID: 10277
		// (get) Token: 0x06007928 RID: 31016 RVA: 0x001F333E File Offset: 0x001F153E
		public int Size
		{
			get
			{
				return ItemSizes.SizeOf(this.Array);
			}
		}

		// Token: 0x17002826 RID: 10278
		// (get) Token: 0x06007929 RID: 31017 RVA: 0x001F334B File Offset: 0x001F154B
		public int EmptySize
		{
			get
			{
				return ItemSizes.NonNullIStorableOverhead + ItemSizes.SizeOfEmptyObjectArray(this.Array.Length);
			}
		}

		// Token: 0x0600792A RID: 31018 RVA: 0x001F3360 File Offset: 0x001F1560
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(StorableArray.m_declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.Array)
				{
					writer.WriteVariantOrPersistableArray(this.Array);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x0600792B RID: 31019 RVA: 0x001F33B4 File Offset: 0x001F15B4
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(StorableArray.m_declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.Array)
				{
					this.Array = reader.ReadVariantArray();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x0600792C RID: 31020 RVA: 0x001F3405 File Offset: 0x001F1605
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x0600792D RID: 31021 RVA: 0x001F3407 File Offset: 0x001F1607
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StorableArray;
		}

		// Token: 0x0600792E RID: 31022 RVA: 0x001F340C File Offset: 0x001F160C
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (StorableArray.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StorableArray, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Array, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveArray, Token.Object)
				});
			}
			return StorableArray.m_declaration;
		}

		// Token: 0x0600792F RID: 31023 RVA: 0x001F3448 File Offset: 0x001F1648
		public void TransferTo(IScalabilityCache scaleCache)
		{
			if (this.Array != null && this.Array.Length != 0)
			{
				IReference reference = this.Array[0] as IReference;
				if (reference != null)
				{
					this.Array[0] = reference.TransferTo(scaleCache);
					for (int i = 1; i < this.Array.Length; i++)
					{
						reference = this.Array[i] as IReference;
						if (reference != null)
						{
							this.Array[i] = reference.TransferTo(scaleCache);
						}
					}
					return;
				}
				ITransferable transferable = this.Array[0] as ITransferable;
				if (transferable != null)
				{
					transferable.TransferTo(scaleCache);
					for (int j = 1; j < this.Array.Length; j++)
					{
						transferable = this.Array[j] as ITransferable;
						if (transferable != null)
						{
							transferable.TransferTo(scaleCache);
						}
					}
				}
			}
		}

		// Token: 0x04003CD7 RID: 15575
		internal object[] Array;

		// Token: 0x04003CD8 RID: 15576
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = StorableArray.GetDeclaration();
	}
}
