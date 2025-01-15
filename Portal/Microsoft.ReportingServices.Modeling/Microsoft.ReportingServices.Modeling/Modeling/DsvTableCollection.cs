using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x0200006E RID: 110
	public sealed class DsvTableCollection : DsvItemCollection<DsvTable>
	{
		// Token: 0x06000496 RID: 1174 RVA: 0x0000F1C7 File Offset: 0x0000D3C7
		internal static DsvTableCollection FromDataRelationCollection(DataTableCollection tables)
		{
			return new DsvTableCollection(new DsvTableCollection.DsvTableCollectionInfoDS(tables));
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000F1D4 File Offset: 0x0000D3D4
		private DsvTableCollection(DsvTableCollection.IDsvTableCollectionInfo tablesInfo)
		{
			this.m_tablesInfo = tablesInfo;
		}

		// Token: 0x170000C8 RID: 200
		public override DsvTable this[int index]
		{
			get
			{
				return this.m_tablesInfo[index];
			}
		}

		// Token: 0x170000C9 RID: 201
		public DsvTable this[string name]
		{
			get
			{
				return DsvItemCollection<DsvTable>.CheckNameMatch(this.m_tablesInfo[name], name);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x0000F205 File Offset: 0x0000D405
		public override int Count
		{
			get
			{
				return this.m_tablesInfo.Count;
			}
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0000F212 File Offset: 0x0000D412
		internal static DsvTableCollection FromBinary()
		{
			return new DsvTableCollection(new DsvTableCollection.DsvTableCollectionInfoBinary());
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x0000F21E File Offset: 0x0000D41E
		internal override IPersistable DataStorage
		{
			get
			{
				return this.m_tablesInfo;
			}
		}

		// Token: 0x04000280 RID: 640
		private readonly DsvTableCollection.IDsvTableCollectionInfo m_tablesInfo;

		// Token: 0x02000141 RID: 321
		private interface IDsvTableCollectionInfo : IPersistable
		{
			// Token: 0x17000346 RID: 838
			DsvTable this[int index] { get; }

			// Token: 0x17000347 RID: 839
			DsvTable this[string name] { get; }

			// Token: 0x17000348 RID: 840
			// (get) Token: 0x06000E71 RID: 3697
			int Count { get; }
		}

		// Token: 0x02000142 RID: 322
		private sealed class DsvTableCollectionInfoDS : DsvTableCollection.IDsvTableCollectionInfo, IPersistable
		{
			// Token: 0x06000E72 RID: 3698 RVA: 0x0002E96F File Offset: 0x0002CB6F
			internal DsvTableCollectionInfoDS(DataTableCollection tables)
			{
				if (tables == null)
				{
					throw new InternalModelingException("tables is null");
				}
				this.m_tables = tables;
			}

			// Token: 0x17000349 RID: 841
			DsvTable DsvTableCollection.IDsvTableCollectionInfo.this[int index]
			{
				get
				{
					return DsvTable.FromDataTable(this.m_tables[index]);
				}
			}

			// Token: 0x1700034A RID: 842
			DsvTable DsvTableCollection.IDsvTableCollectionInfo.this[string name]
			{
				get
				{
					return DsvTable.FromDataTable(this.m_tables[name]);
				}
			}

			// Token: 0x1700034B RID: 843
			// (get) Token: 0x06000E75 RID: 3701 RVA: 0x0002E9B2 File Offset: 0x0002CBB2
			int DsvTableCollection.IDsvTableCollectionInfo.Count
			{
				get
				{
					return this.m_tables.Count;
				}
			}

			// Token: 0x06000E76 RID: 3702 RVA: 0x0002E9BF File Offset: 0x0002CBBF
			void IPersistable.Serialize(IntermediateFormatWriter writer)
			{
				((IPersistable)new DsvTableCollection.DsvTableCollectionInfoBinary(this)).Serialize(writer);
			}

			// Token: 0x06000E77 RID: 3703 RVA: 0x0002E9CD File Offset: 0x0002CBCD
			void IPersistable.Deserialize(IntermediateFormatReader reader)
			{
				throw new InternalModelingException("Deserialize is not supported.");
			}

			// Token: 0x06000E78 RID: 3704 RVA: 0x0002E9D9 File Offset: 0x0002CBD9
			void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
			{
				throw new InternalModelingException("ResolveReferences is not supported.");
			}

			// Token: 0x06000E79 RID: 3705 RVA: 0x0002E9E5 File Offset: 0x0002CBE5
			ObjectType IPersistable.GetObjectType()
			{
				return ObjectType.DsvTableCollection;
			}

			// Token: 0x0400060C RID: 1548
			private readonly DataTableCollection m_tables;
		}

		// Token: 0x02000143 RID: 323
		private sealed class DsvTableCollectionInfoBinary : DsvTableCollection.IDsvTableCollectionInfo, IPersistable
		{
			// Token: 0x06000E7A RID: 3706 RVA: 0x0002E9E9 File Offset: 0x0002CBE9
			internal DsvTableCollectionInfoBinary()
			{
			}

			// Token: 0x06000E7B RID: 3707 RVA: 0x0002E9F4 File Offset: 0x0002CBF4
			internal DsvTableCollectionInfoBinary(DsvTableCollection.IDsvTableCollectionInfo tables)
				: this()
			{
				this.m_array = new DsvTable[tables.Count];
				for (int i = 0; i < tables.Count; i++)
				{
					this.m_array[i] = tables[i];
				}
			}

			// Token: 0x1700034C RID: 844
			DsvTable DsvTableCollection.IDsvTableCollectionInfo.this[int index]
			{
				get
				{
					return this.m_array[index];
				}
			}

			// Token: 0x1700034D RID: 845
			DsvTable DsvTableCollection.IDsvTableCollectionInfo.this[string name]
			{
				get
				{
					return DsvItemCollection<DsvTable>.FindDsvItemByName(this.m_array, this.m_dictionary, name);
				}
			}

			// Token: 0x1700034E RID: 846
			// (get) Token: 0x06000E7E RID: 3710 RVA: 0x0002EA56 File Offset: 0x0002CC56
			int DsvTableCollection.IDsvTableCollectionInfo.Count
			{
				get
				{
					return this.m_array.Length;
				}
			}

			// Token: 0x06000E7F RID: 3711 RVA: 0x0002EA60 File Offset: 0x0002CC60
			void IPersistable.Serialize(IntermediateFormatWriter writer)
			{
				writer.RegisterDeclaration(DsvTableCollection.DsvTableCollectionInfoBinary.Declaration);
				while (writer.NextMember())
				{
					if (writer.CurrentMember.MemberName != MemberName.Dictionary)
					{
						throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
					}
					PersistenceHelper.WriteDsvItemArray<DsvTable>(ref writer, this.m_array);
				}
			}

			// Token: 0x06000E80 RID: 3712 RVA: 0x0002EAD0 File Offset: 0x0002CCD0
			void IPersistable.Deserialize(IntermediateFormatReader reader)
			{
				reader.RegisterDeclaration(DsvTableCollection.DsvTableCollectionInfoBinary.Declaration);
				while (reader.NextMember())
				{
					if (reader.CurrentMember.MemberName != MemberName.Dictionary)
					{
						throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
					}
					this.m_array = PersistenceHelper.ReadDsvItemArray<DsvTable>(ref reader);
					if (this.m_array.Length > 30)
					{
						this.m_dictionary = new Dictionary<string, DsvTable>(StringComparer.Ordinal);
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

			// Token: 0x06000E81 RID: 3713 RVA: 0x0002EB9B File Offset: 0x0002CD9B
			void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
			{
				throw new InternalModelingException("ResolveReferences is not supported.");
			}

			// Token: 0x06000E82 RID: 3714 RVA: 0x0002EBA7 File Offset: 0x0002CDA7
			ObjectType IPersistable.GetObjectType()
			{
				return ObjectType.DsvTableCollection;
			}

			// Token: 0x1700034F RID: 847
			// (get) Token: 0x06000E83 RID: 3715 RVA: 0x0002EBAB File Offset: 0x0002CDAB
			private static Declaration Declaration
			{
				get
				{
					return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref DsvTableCollection.DsvTableCollectionInfoBinary.__declaration, DsvTableCollection.DsvTableCollectionInfoBinary.__declarationLock, () => new Declaration(ObjectType.DsvTableCollection, ObjectType.None, new List<MemberInfo>
					{
						new MemberInfo(MemberName.Dictionary, ObjectType.RIFObjectArray, ObjectType.DsvTable)
					}));
				}
			}

			// Token: 0x0400060D RID: 1549
			private DsvTable[] m_array;

			// Token: 0x0400060E RID: 1550
			private Dictionary<string, DsvTable> m_dictionary;

			// Token: 0x0400060F RID: 1551
			private static Declaration __declaration;

			// Token: 0x04000610 RID: 1552
			private static readonly object __declarationLock = new object();
		}
	}
}
