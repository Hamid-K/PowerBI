using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000076 RID: 118
	public sealed class DsvRelationCollection : DsvItemCollection<DsvRelation>
	{
		// Token: 0x06000501 RID: 1281 RVA: 0x0000F8D5 File Offset: 0x0000DAD5
		internal static DsvRelationCollection FromDataRelationCollection(DataRelationCollection relations)
		{
			return new DsvRelationCollection(new DsvRelationCollection.DsvRelationCollectionInfoDS(relations));
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0000F8E2 File Offset: 0x0000DAE2
		private DsvRelationCollection(DsvRelationCollection.IDsvRelationCollectionInfo relationsInfo)
		{
			this.m_relationsInfo = relationsInfo;
		}

		// Token: 0x1700010F RID: 271
		public override DsvRelation this[int index]
		{
			get
			{
				return this.m_relationsInfo[index];
			}
		}

		// Token: 0x17000110 RID: 272
		public DsvRelation this[string name]
		{
			get
			{
				return DsvItemCollection<DsvRelation>.CheckNameMatch(this.m_relationsInfo[name], name);
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x0000F913 File Offset: 0x0000DB13
		public override int Count
		{
			get
			{
				return this.m_relationsInfo.Count;
			}
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000F920 File Offset: 0x0000DB20
		internal static DsvRelationCollection FromBinary()
		{
			return new DsvRelationCollection(new DsvRelationCollection.DsvRelationCollectionInfoBinary());
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x0000F92C File Offset: 0x0000DB2C
		internal override IPersistable DataStorage
		{
			get
			{
				return this.m_relationsInfo;
			}
		}

		// Token: 0x04000299 RID: 665
		private readonly DsvRelationCollection.IDsvRelationCollectionInfo m_relationsInfo;

		// Token: 0x0200015E RID: 350
		private interface IDsvRelationCollectionInfo : IPersistable
		{
			// Token: 0x170003CD RID: 973
			DsvRelation this[int index] { get; }

			// Token: 0x170003CE RID: 974
			DsvRelation this[string name] { get; }

			// Token: 0x170003CF RID: 975
			// (get) Token: 0x06000F61 RID: 3937
			int Count { get; }
		}

		// Token: 0x0200015F RID: 351
		private sealed class DsvRelationCollectionInfoDS : DsvRelationCollection.IDsvRelationCollectionInfo, IPersistable
		{
			// Token: 0x06000F62 RID: 3938 RVA: 0x000309C6 File Offset: 0x0002EBC6
			internal DsvRelationCollectionInfoDS(DataRelationCollection relations)
			{
				if (relations == null)
				{
					throw new InternalModelingException("relations is null");
				}
				this.m_relations = relations;
			}

			// Token: 0x170003D0 RID: 976
			DsvRelation DsvRelationCollection.IDsvRelationCollectionInfo.this[int index]
			{
				get
				{
					return DsvRelation.FromDataRelation(this.m_relations[index]);
				}
			}

			// Token: 0x170003D1 RID: 977
			DsvRelation DsvRelationCollection.IDsvRelationCollectionInfo.this[string name]
			{
				get
				{
					return DsvRelation.FromDataRelation(this.m_relations[name]);
				}
			}

			// Token: 0x170003D2 RID: 978
			// (get) Token: 0x06000F65 RID: 3941 RVA: 0x00030A09 File Offset: 0x0002EC09
			int DsvRelationCollection.IDsvRelationCollectionInfo.Count
			{
				get
				{
					return this.m_relations.Count;
				}
			}

			// Token: 0x06000F66 RID: 3942 RVA: 0x00030A16 File Offset: 0x0002EC16
			void IPersistable.Serialize(IntermediateFormatWriter writer)
			{
				((IPersistable)new DsvRelationCollection.DsvRelationCollectionInfoBinary(this)).Serialize(writer);
			}

			// Token: 0x06000F67 RID: 3943 RVA: 0x00030A24 File Offset: 0x0002EC24
			void IPersistable.Deserialize(IntermediateFormatReader reader)
			{
				throw new InternalModelingException("Deserialize is not supported.");
			}

			// Token: 0x06000F68 RID: 3944 RVA: 0x00030A30 File Offset: 0x0002EC30
			void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
			{
				throw new InternalModelingException("ResolveReferences is not supported.");
			}

			// Token: 0x06000F69 RID: 3945 RVA: 0x00030A3C File Offset: 0x0002EC3C
			ObjectType IPersistable.GetObjectType()
			{
				return ObjectType.DsvRelationCollection;
			}

			// Token: 0x0400064E RID: 1614
			private readonly DataRelationCollection m_relations;
		}

		// Token: 0x02000160 RID: 352
		private sealed class DsvRelationCollectionInfoBinary : DsvRelationCollection.IDsvRelationCollectionInfo, IPersistable
		{
			// Token: 0x06000F6A RID: 3946 RVA: 0x00030A40 File Offset: 0x0002EC40
			internal DsvRelationCollectionInfoBinary()
			{
			}

			// Token: 0x06000F6B RID: 3947 RVA: 0x00030A48 File Offset: 0x0002EC48
			internal DsvRelationCollectionInfoBinary(DsvRelationCollection.IDsvRelationCollectionInfo relations)
				: this()
			{
				this.m_array = new DsvRelation[relations.Count];
				for (int i = 0; i < relations.Count; i++)
				{
					this.m_array[i] = relations[i];
				}
			}

			// Token: 0x170003D3 RID: 979
			DsvRelation DsvRelationCollection.IDsvRelationCollectionInfo.this[int index]
			{
				get
				{
					return this.m_array[index];
				}
			}

			// Token: 0x170003D4 RID: 980
			DsvRelation DsvRelationCollection.IDsvRelationCollectionInfo.this[string name]
			{
				get
				{
					return DsvItemCollection<DsvRelation>.FindDsvItemByName(this.m_array, this.m_dictionary, name);
				}
			}

			// Token: 0x170003D5 RID: 981
			// (get) Token: 0x06000F6E RID: 3950 RVA: 0x00030AAA File Offset: 0x0002ECAA
			int DsvRelationCollection.IDsvRelationCollectionInfo.Count
			{
				get
				{
					return this.m_array.Length;
				}
			}

			// Token: 0x06000F6F RID: 3951 RVA: 0x00030AB4 File Offset: 0x0002ECB4
			void IPersistable.Serialize(IntermediateFormatWriter writer)
			{
				writer.RegisterDeclaration(DsvRelationCollection.DsvRelationCollectionInfoBinary.Declaration);
				while (writer.NextMember())
				{
					if (writer.CurrentMember.MemberName != MemberName.Dictionary)
					{
						throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
					}
					PersistenceHelper.WriteDsvItemArray<DsvRelation>(ref writer, this.m_array);
				}
			}

			// Token: 0x06000F70 RID: 3952 RVA: 0x00030B24 File Offset: 0x0002ED24
			void IPersistable.Deserialize(IntermediateFormatReader reader)
			{
				reader.RegisterDeclaration(DsvRelationCollection.DsvRelationCollectionInfoBinary.Declaration);
				while (reader.NextMember())
				{
					if (reader.CurrentMember.MemberName != MemberName.Dictionary)
					{
						throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
					}
					this.m_array = PersistenceHelper.ReadDsvItemArray<DsvRelation>(ref reader);
					if (this.m_array.Length > 30)
					{
						this.m_dictionary = new Dictionary<string, DsvRelation>(StringComparer.Ordinal);
						for (int i = 0; i < this.m_array.Length; i++)
						{
							this.m_dictionary.Add(this.m_array[i].Name, this.m_array[i]);
						}
					}
					else
					{
						this.m_dictionary = null;
					}
				}
			}

			// Token: 0x06000F71 RID: 3953 RVA: 0x00030BEF File Offset: 0x0002EDEF
			void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
			{
				throw new InternalModelingException("ResolveReferences is not supported.");
			}

			// Token: 0x06000F72 RID: 3954 RVA: 0x00030BFB File Offset: 0x0002EDFB
			ObjectType IPersistable.GetObjectType()
			{
				return ObjectType.DsvRelationCollection;
			}

			// Token: 0x170003D6 RID: 982
			// (get) Token: 0x06000F73 RID: 3955 RVA: 0x00030BFF File Offset: 0x0002EDFF
			private static Declaration Declaration
			{
				get
				{
					return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref DsvRelationCollection.DsvRelationCollectionInfoBinary.__declaration, DsvRelationCollection.DsvRelationCollectionInfoBinary.__declarationLock, () => new Declaration(ObjectType.DsvRelationCollection, ObjectType.None, new List<MemberInfo>
					{
						new MemberInfo(MemberName.Dictionary, ObjectType.RIFObjectArray, ObjectType.DsvRelation)
					}));
				}
			}

			// Token: 0x0400064F RID: 1615
			private DsvRelation[] m_array;

			// Token: 0x04000650 RID: 1616
			private Dictionary<string, DsvRelation> m_dictionary;

			// Token: 0x04000651 RID: 1617
			private static Declaration __declaration;

			// Token: 0x04000652 RID: 1618
			private static readonly object __declarationLock = new object();
		}
	}
}
