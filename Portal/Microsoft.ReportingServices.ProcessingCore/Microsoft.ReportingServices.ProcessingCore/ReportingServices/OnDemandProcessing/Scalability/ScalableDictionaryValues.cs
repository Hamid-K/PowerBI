using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000896 RID: 2198
	internal sealed class ScalableDictionaryValues : IScalableDictionaryEntry, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, ITransferable
	{
		// Token: 0x06007864 RID: 30820 RVA: 0x001F0306 File Offset: 0x001EE506
		internal ScalableDictionaryValues()
		{
		}

		// Token: 0x06007865 RID: 30821 RVA: 0x001F030E File Offset: 0x001EE50E
		public ScalableDictionaryValues(int capacity)
		{
			this.m_count = 0;
			this.m_keys = new object[capacity];
			this.m_values = new object[capacity];
		}

		// Token: 0x170027FE RID: 10238
		// (get) Token: 0x06007866 RID: 30822 RVA: 0x001F0335 File Offset: 0x001EE535
		public object[] Keys
		{
			get
			{
				return this.m_keys;
			}
		}

		// Token: 0x170027FF RID: 10239
		// (get) Token: 0x06007867 RID: 30823 RVA: 0x001F033D File Offset: 0x001EE53D
		public object[] Values
		{
			get
			{
				return this.m_values;
			}
		}

		// Token: 0x17002800 RID: 10240
		// (get) Token: 0x06007868 RID: 30824 RVA: 0x001F0345 File Offset: 0x001EE545
		// (set) Token: 0x06007869 RID: 30825 RVA: 0x001F034D File Offset: 0x001EE54D
		public int Count
		{
			get
			{
				return this.m_count;
			}
			set
			{
				this.m_count = value;
			}
		}

		// Token: 0x17002801 RID: 10241
		// (get) Token: 0x0600786A RID: 30826 RVA: 0x001F0356 File Offset: 0x001EE556
		public int Capacity
		{
			get
			{
				return this.m_keys.Length;
			}
		}

		// Token: 0x0600786B RID: 30827 RVA: 0x001F0360 File Offset: 0x001EE560
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ScalableDictionaryValues.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Values)
				{
					if (memberName != MemberName.Count)
					{
						if (memberName == MemberName.Keys)
						{
							writer.WriteVariantOrPersistableArray(this.m_keys);
						}
						else
						{
							Global.Tracer.Assert(false);
						}
					}
					else
					{
						writer.Write(this.m_count);
					}
				}
				else
				{
					writer.WriteVariantOrPersistableArray(this.m_values);
				}
			}
		}

		// Token: 0x0600786C RID: 30828 RVA: 0x001F03E4 File Offset: 0x001EE5E4
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ScalableDictionaryValues.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Values)
				{
					if (memberName != MemberName.Count)
					{
						if (memberName == MemberName.Keys)
						{
							this.m_keys = reader.ReadVariantArray();
						}
						else
						{
							Global.Tracer.Assert(false);
						}
					}
					else
					{
						this.m_count = reader.ReadInt32();
					}
				}
				else
				{
					this.m_values = reader.ReadVariantArray();
				}
			}
		}

		// Token: 0x0600786D RID: 30829 RVA: 0x001F0465 File Offset: 0x001EE665
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x0600786E RID: 30830 RVA: 0x001F0467 File Offset: 0x001EE667
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionaryValues;
		}

		// Token: 0x0600786F RID: 30831 RVA: 0x001F046C File Offset: 0x001EE66C
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (ScalableDictionaryValues.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionaryValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Keys, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveArray, Token.Object),
					new MemberInfo(MemberName.Values, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveArray, Token.Object),
					new MemberInfo(MemberName.Count, Token.Int32)
				});
			}
			return ScalableDictionaryValues.m_declaration;
		}

		// Token: 0x17002802 RID: 10242
		// (get) Token: 0x06007870 RID: 30832 RVA: 0x001F04CE File Offset: 0x001EE6CE
		public int Size
		{
			get
			{
				return 4 + ItemSizes.SizeOf(this.m_keys) + ItemSizes.SizeOf(this.m_values);
			}
		}

		// Token: 0x17002803 RID: 10243
		// (get) Token: 0x06007871 RID: 30833 RVA: 0x001F04E9 File Offset: 0x001EE6E9
		public int EmptySize
		{
			get
			{
				return ItemSizes.NonNullIStorableOverhead + 4 + ItemSizes.SizeOfEmptyObjectArray(this.m_keys.Length) * 2;
			}
		}

		// Token: 0x06007872 RID: 30834 RVA: 0x001F0504 File Offset: 0x001EE704
		public void TransferTo(IScalabilityCache scaleCache)
		{
			for (int i = 0; i < this.m_count; i++)
			{
				ITransferable transferable = this.m_values[i] as ITransferable;
				if (transferable != null)
				{
					transferable.TransferTo(scaleCache);
				}
			}
		}

		// Token: 0x04003C95 RID: 15509
		private object[] m_keys;

		// Token: 0x04003C96 RID: 15510
		private object[] m_values;

		// Token: 0x04003C97 RID: 15511
		private int m_count;

		// Token: 0x04003C98 RID: 15512
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = ScalableDictionaryValues.GetDeclaration();
	}
}
