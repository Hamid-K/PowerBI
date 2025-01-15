using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000070 RID: 112
	public sealed class DsvColumnCollection : DsvItemCollection<DsvColumn>
	{
		// Token: 0x060004CB RID: 1227 RVA: 0x0000F52E File Offset: 0x0000D72E
		internal static DsvColumnCollection FromDataColumnCollection(DataColumnCollection columns)
		{
			return new DsvColumnCollection(new DsvColumnCollection.DsvColumnCollectionInfoDS(columns));
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0000F53B File Offset: 0x0000D73B
		private DsvColumnCollection(DsvColumnCollection.IDsvColumnCollectionInfo columnsInfo)
		{
			this.m_columnsInfo = columnsInfo;
		}

		// Token: 0x170000EA RID: 234
		public override DsvColumn this[int index]
		{
			get
			{
				return this.m_columnsInfo[index];
			}
		}

		// Token: 0x170000EB RID: 235
		public DsvColumn this[string name]
		{
			get
			{
				return DsvItemCollection<DsvColumn>.CheckNameMatch(this.m_columnsInfo[name], name);
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0000F56C File Offset: 0x0000D76C
		public override int Count
		{
			get
			{
				return this.m_columnsInfo.Count;
			}
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0000F579 File Offset: 0x0000D779
		internal static DsvColumnCollection FromBinary()
		{
			return new DsvColumnCollection(new DsvColumnCollection.DsvColumnCollectionInfoBinary());
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0000F585 File Offset: 0x0000D785
		internal override IPersistable DataStorage
		{
			get
			{
				return this.m_columnsInfo;
			}
		}

		// Token: 0x04000293 RID: 659
		private readonly DsvColumnCollection.IDsvColumnCollectionInfo m_columnsInfo;

		// Token: 0x02000149 RID: 329
		private interface IDsvColumnCollectionInfo : IPersistable
		{
			// Token: 0x17000376 RID: 886
			DsvColumn this[int index] { get; }

			// Token: 0x17000377 RID: 887
			DsvColumn this[string name] { get; }

			// Token: 0x17000378 RID: 888
			// (get) Token: 0x06000EC0 RID: 3776
			int Count { get; }
		}

		// Token: 0x0200014A RID: 330
		private sealed class DsvColumnCollectionInfoDS : DsvColumnCollection.IDsvColumnCollectionInfo, IPersistable
		{
			// Token: 0x06000EC1 RID: 3777 RVA: 0x0002F376 File Offset: 0x0002D576
			internal DsvColumnCollectionInfoDS(DataColumnCollection columns)
			{
				if (columns == null)
				{
					throw new InternalModelingException("columns is null");
				}
				this.m_columns = columns;
			}

			// Token: 0x17000379 RID: 889
			DsvColumn DsvColumnCollection.IDsvColumnCollectionInfo.this[int index]
			{
				get
				{
					return DsvColumn.FromDataColumn(this.m_columns[index]);
				}
			}

			// Token: 0x1700037A RID: 890
			DsvColumn DsvColumnCollection.IDsvColumnCollectionInfo.this[string name]
			{
				get
				{
					return DsvColumn.FromDataColumn(this.m_columns[name]);
				}
			}

			// Token: 0x1700037B RID: 891
			// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x0002F3B9 File Offset: 0x0002D5B9
			int DsvColumnCollection.IDsvColumnCollectionInfo.Count
			{
				get
				{
					return this.m_columns.Count;
				}
			}

			// Token: 0x06000EC5 RID: 3781 RVA: 0x0002F3C6 File Offset: 0x0002D5C6
			void IPersistable.Serialize(IntermediateFormatWriter writer)
			{
				((IPersistable)new DsvColumnCollection.DsvColumnCollectionInfoBinary(this)).Serialize(writer);
			}

			// Token: 0x06000EC6 RID: 3782 RVA: 0x0002F3D4 File Offset: 0x0002D5D4
			void IPersistable.Deserialize(IntermediateFormatReader reader)
			{
				throw new InternalModelingException("Deserialize is not supported.");
			}

			// Token: 0x06000EC7 RID: 3783 RVA: 0x0002F3E0 File Offset: 0x0002D5E0
			void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
			{
				throw new InternalModelingException("ResolveReferences is not supported.");
			}

			// Token: 0x06000EC8 RID: 3784 RVA: 0x0002F3EC File Offset: 0x0002D5EC
			ObjectType IPersistable.GetObjectType()
			{
				return ObjectType.DsvColumnCollection;
			}

			// Token: 0x04000620 RID: 1568
			private readonly DataColumnCollection m_columns;
		}

		// Token: 0x0200014B RID: 331
		private sealed class DsvColumnCollectionInfoBinary : DsvColumnCollection.IDsvColumnCollectionInfo, IPersistable
		{
			// Token: 0x06000EC9 RID: 3785 RVA: 0x0002F3F0 File Offset: 0x0002D5F0
			internal DsvColumnCollectionInfoBinary()
			{
			}

			// Token: 0x06000ECA RID: 3786 RVA: 0x0002F3F8 File Offset: 0x0002D5F8
			internal DsvColumnCollectionInfoBinary(DsvColumnCollection.IDsvColumnCollectionInfo columns)
				: this()
			{
				this.m_array = new DsvColumn[columns.Count];
				for (int i = 0; i < columns.Count; i++)
				{
					this.m_array[i] = columns[i];
				}
			}

			// Token: 0x1700037C RID: 892
			DsvColumn DsvColumnCollection.IDsvColumnCollectionInfo.this[int index]
			{
				get
				{
					return this.m_array[index];
				}
			}

			// Token: 0x1700037D RID: 893
			DsvColumn DsvColumnCollection.IDsvColumnCollectionInfo.this[string name]
			{
				get
				{
					return DsvItemCollection<DsvColumn>.FindDsvItemByName(this.m_array, this.m_dictionary, name);
				}
			}

			// Token: 0x1700037E RID: 894
			// (get) Token: 0x06000ECD RID: 3789 RVA: 0x0002F45A File Offset: 0x0002D65A
			int DsvColumnCollection.IDsvColumnCollectionInfo.Count
			{
				get
				{
					return this.m_array.Length;
				}
			}

			// Token: 0x06000ECE RID: 3790 RVA: 0x0002F464 File Offset: 0x0002D664
			void IPersistable.Serialize(IntermediateFormatWriter writer)
			{
				writer.RegisterDeclaration(DsvColumnCollection.DsvColumnCollectionInfoBinary.Declaration);
				while (writer.NextMember())
				{
					if (writer.CurrentMember.MemberName != MemberName.Dictionary)
					{
						throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
					}
					PersistenceHelper.WriteDsvItemArray<DsvColumn>(ref writer, this.m_array);
				}
			}

			// Token: 0x06000ECF RID: 3791 RVA: 0x0002F4D4 File Offset: 0x0002D6D4
			void IPersistable.Deserialize(IntermediateFormatReader reader)
			{
				reader.RegisterDeclaration(DsvColumnCollection.DsvColumnCollectionInfoBinary.Declaration);
				while (reader.NextMember())
				{
					if (reader.CurrentMember.MemberName != MemberName.Dictionary)
					{
						throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
					}
					this.m_array = PersistenceHelper.ReadDsvItemArray<DsvColumn>(ref reader);
					if (this.m_array.Length > 30)
					{
						this.m_dictionary = new Dictionary<string, DsvColumn>(StringComparer.Ordinal);
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

			// Token: 0x06000ED0 RID: 3792 RVA: 0x0002F59F File Offset: 0x0002D79F
			void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
			{
				throw new InternalModelingException("ResolveReferences is not supported.");
			}

			// Token: 0x06000ED1 RID: 3793 RVA: 0x0002F5AB File Offset: 0x0002D7AB
			ObjectType IPersistable.GetObjectType()
			{
				return ObjectType.DsvColumnCollection;
			}

			// Token: 0x1700037F RID: 895
			// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x0002F5AF File Offset: 0x0002D7AF
			private static Declaration Declaration
			{
				get
				{
					return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref DsvColumnCollection.DsvColumnCollectionInfoBinary.__declaration, DsvColumnCollection.DsvColumnCollectionInfoBinary.__declarationLock, () => new Declaration(ObjectType.DsvColumnCollection, ObjectType.None, new List<MemberInfo>
					{
						new MemberInfo(MemberName.Dictionary, ObjectType.RIFObjectArray, ObjectType.DsvColumn)
					}));
				}
			}

			// Token: 0x04000621 RID: 1569
			private DsvColumn[] m_array;

			// Token: 0x04000622 RID: 1570
			private Dictionary<string, DsvColumn> m_dictionary;

			// Token: 0x04000623 RID: 1571
			private static Declaration __declaration;

			// Token: 0x04000624 RID: 1572
			private static readonly object __declarationLock = new object();
		}
	}
}
