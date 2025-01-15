using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000075 RID: 117
	public sealed class DsvRelation : DsvItem
	{
		// Token: 0x060004F0 RID: 1264 RVA: 0x0000F7C0 File Offset: 0x0000D9C0
		internal static DsvRelation FromDataRelation(DataRelation relation)
		{
			if (relation == null)
			{
				return null;
			}
			return DsvItem.GetDsvItem<DsvRelation>(relation.ExtendedProperties, () => new DsvRelation(new DsvRelation.DsvRelationInfoDS(relation)));
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0000F800 File Offset: 0x0000DA00
		private DsvRelation(DsvRelation.IDsvRelationInfo relationInfo)
		{
			this.m_relationInfo = relationInfo;
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x0000F80F File Offset: 0x0000DA0F
		public override string Name
		{
			get
			{
				return this.m_relationInfo.Name;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0000F81C File Offset: 0x0000DA1C
		public override bool IsReadOnly
		{
			get
			{
				return this.m_relationInfo.IsReadOnly;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0000F829 File Offset: 0x0000DA29
		public DsvTable SourceTable
		{
			get
			{
				return this.m_relationInfo.SourceTable;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x0000F836 File Offset: 0x0000DA36
		public DsvTable TargetTable
		{
			get
			{
				return this.m_relationInfo.TargetTable;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x0000F843 File Offset: 0x0000DA43
		public ReadOnlyCollection<DsvColumn> SourceColumns
		{
			get
			{
				return this.m_relationInfo.SourceColumns;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x0000F850 File Offset: 0x0000DA50
		public ReadOnlyCollection<DsvColumn> TargetColumns
		{
			get
			{
				return this.m_relationInfo.TargetColumns;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x0000F85D File Offset: 0x0000DA5D
		public DsvForeignKeyConstraint SourceConstraint
		{
			get
			{
				return this.m_relationInfo.SourceConstraint;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x0000F86A File Offset: 0x0000DA6A
		public DsvUniqueConstraint TargetConstraint
		{
			get
			{
				return this.m_relationInfo.TargetConstraint;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x0000F877 File Offset: 0x0000DA77
		public string Description
		{
			get
			{
				return base.GetString("Description") ?? string.Empty;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x0000F88D File Offset: 0x0000DA8D
		public bool OneToOne
		{
			get
			{
				return this.m_relationInfo.OneToOne;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x0000F89A File Offset: 0x0000DA9A
		public bool OptionalSource
		{
			get
			{
				return this.m_relationInfo.OptionalSource;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0000F8A7 File Offset: 0x0000DAA7
		public bool OptionalTarget
		{
			get
			{
				return this.m_relationInfo.OptionalTarget;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x0000F8B4 File Offset: 0x0000DAB4
		protected override IDictionary Properties
		{
			get
			{
				return this.m_relationInfo.ExtendedProperties;
			}
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0000F8C1 File Offset: 0x0000DAC1
		internal static DsvRelation FromBinary()
		{
			return new DsvRelation(new DsvRelation.DsvRelationInfoBinary());
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x0000F8CD File Offset: 0x0000DACD
		internal override IPersistable DataStorage
		{
			get
			{
				return this.m_relationInfo;
			}
		}

		// Token: 0x04000298 RID: 664
		private readonly DsvRelation.IDsvRelationInfo m_relationInfo;

		// Token: 0x0200015A RID: 346
		private interface IDsvRelationInfo : IPersistable
		{
			// Token: 0x170003A8 RID: 936
			// (get) Token: 0x06000F2C RID: 3884
			string Name { get; }

			// Token: 0x170003A9 RID: 937
			// (get) Token: 0x06000F2D RID: 3885
			bool IsReadOnly { get; }

			// Token: 0x170003AA RID: 938
			// (get) Token: 0x06000F2E RID: 3886
			DsvTable SourceTable { get; }

			// Token: 0x170003AB RID: 939
			// (get) Token: 0x06000F2F RID: 3887
			DsvTable TargetTable { get; }

			// Token: 0x170003AC RID: 940
			// (get) Token: 0x06000F30 RID: 3888
			ReadOnlyCollection<DsvColumn> SourceColumns { get; }

			// Token: 0x170003AD RID: 941
			// (get) Token: 0x06000F31 RID: 3889
			ReadOnlyCollection<DsvColumn> TargetColumns { get; }

			// Token: 0x170003AE RID: 942
			// (get) Token: 0x06000F32 RID: 3890
			DsvForeignKeyConstraint SourceConstraint { get; }

			// Token: 0x170003AF RID: 943
			// (get) Token: 0x06000F33 RID: 3891
			DsvUniqueConstraint TargetConstraint { get; }

			// Token: 0x170003B0 RID: 944
			// (get) Token: 0x06000F34 RID: 3892
			bool OneToOne { get; }

			// Token: 0x170003B1 RID: 945
			// (get) Token: 0x06000F35 RID: 3893
			bool OptionalSource { get; }

			// Token: 0x170003B2 RID: 946
			// (get) Token: 0x06000F36 RID: 3894
			bool OptionalTarget { get; }

			// Token: 0x170003B3 RID: 947
			// (get) Token: 0x06000F37 RID: 3895
			IDictionary ExtendedProperties { get; }
		}

		// Token: 0x0200015B RID: 347
		private sealed class DsvRelationInfoDS : DsvRelation.IDsvRelationInfo, IPersistable
		{
			// Token: 0x06000F38 RID: 3896 RVA: 0x00030218 File Offset: 0x0002E418
			internal DsvRelationInfoDS(DataRelation relation)
			{
				if (relation == null)
				{
					throw new InternalModelingException("relation is null");
				}
				this.m_relation = relation;
				this.m_sourceColumns = DsvItem.CreateDataColumnArrayWrapper(relation.ChildColumns);
				this.m_targetColumns = DsvItem.CreateDataColumnArrayWrapper(relation.ParentColumns);
				if (this.m_sourceColumns.Count == 0)
				{
					throw new InternalModelingException("DsvRelation sourceColumns is empty");
				}
				if (this.m_targetColumns.Count == 0)
				{
					throw new InternalModelingException("DsvRelation targetColumns is empty");
				}
				DsvRelation.DsvRelationInfoDS.Flags flags = DsvRelation.DsvRelationInfoDS.Flags.None;
				if (((DsvRelation.IDsvRelationInfo)this).SourceTable.AreColumnsUnique(this.m_sourceColumns))
				{
					flags |= DsvRelation.DsvRelationInfoDS.Flags.OneToOne;
				}
				flags |= DsvRelation.DsvRelationInfoDS.Flags.OptionalSource;
				if (((DsvRelation.IDsvRelationInfo)this).SourceConstraint == null)
				{
					flags |= DsvRelation.DsvRelationInfoDS.Flags.OptionalTarget;
				}
				else
				{
					flags &= (DsvRelation.DsvRelationInfoDS.Flags)251;
					using (IEnumerator<DsvColumn> enumerator = this.m_sourceColumns.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.Nullable)
							{
								flags |= DsvRelation.DsvRelationInfoDS.Flags.OptionalTarget;
								break;
							}
						}
					}
				}
				this.m_flags = flags;
			}

			// Token: 0x170003B4 RID: 948
			// (get) Token: 0x06000F39 RID: 3897 RVA: 0x00030314 File Offset: 0x0002E514
			string DsvRelation.IDsvRelationInfo.Name
			{
				get
				{
					return this.m_relation.RelationName;
				}
			}

			// Token: 0x170003B5 RID: 949
			// (get) Token: 0x06000F3A RID: 3898 RVA: 0x00030321 File Offset: 0x0002E521
			bool DsvRelation.IDsvRelationInfo.IsReadOnly
			{
				get
				{
					return DsvItem.IsDataSetReadonly(this.m_relation.DataSet);
				}
			}

			// Token: 0x170003B6 RID: 950
			// (get) Token: 0x06000F3B RID: 3899 RVA: 0x00030333 File Offset: 0x0002E533
			DsvTable DsvRelation.IDsvRelationInfo.SourceTable
			{
				get
				{
					return DsvTable.FromDataTable(this.m_relation.ChildTable);
				}
			}

			// Token: 0x170003B7 RID: 951
			// (get) Token: 0x06000F3C RID: 3900 RVA: 0x00030345 File Offset: 0x0002E545
			DsvTable DsvRelation.IDsvRelationInfo.TargetTable
			{
				get
				{
					return DsvTable.FromDataTable(this.m_relation.ParentTable);
				}
			}

			// Token: 0x170003B8 RID: 952
			// (get) Token: 0x06000F3D RID: 3901 RVA: 0x00030357 File Offset: 0x0002E557
			ReadOnlyCollection<DsvColumn> DsvRelation.IDsvRelationInfo.SourceColumns
			{
				get
				{
					return this.m_sourceColumns;
				}
			}

			// Token: 0x170003B9 RID: 953
			// (get) Token: 0x06000F3E RID: 3902 RVA: 0x0003035F File Offset: 0x0002E55F
			ReadOnlyCollection<DsvColumn> DsvRelation.IDsvRelationInfo.TargetColumns
			{
				get
				{
					return this.m_targetColumns;
				}
			}

			// Token: 0x170003BA RID: 954
			// (get) Token: 0x06000F3F RID: 3903 RVA: 0x00030367 File Offset: 0x0002E567
			DsvForeignKeyConstraint DsvRelation.IDsvRelationInfo.SourceConstraint
			{
				get
				{
					return DsvForeignKeyConstraint.FromForeignKeyConstraint(this.m_relation.ChildKeyConstraint);
				}
			}

			// Token: 0x170003BB RID: 955
			// (get) Token: 0x06000F40 RID: 3904 RVA: 0x00030379 File Offset: 0x0002E579
			DsvUniqueConstraint DsvRelation.IDsvRelationInfo.TargetConstraint
			{
				get
				{
					return DsvUniqueConstraint.FromUniqueConstraint(this.m_relation.ParentKeyConstraint);
				}
			}

			// Token: 0x170003BC RID: 956
			// (get) Token: 0x06000F41 RID: 3905 RVA: 0x0003038B File Offset: 0x0002E58B
			bool DsvRelation.IDsvRelationInfo.OneToOne
			{
				get
				{
					return (this.m_flags & DsvRelation.DsvRelationInfoDS.Flags.OneToOne) == DsvRelation.DsvRelationInfoDS.Flags.OneToOne;
				}
			}

			// Token: 0x170003BD RID: 957
			// (get) Token: 0x06000F42 RID: 3906 RVA: 0x00030398 File Offset: 0x0002E598
			bool DsvRelation.IDsvRelationInfo.OptionalSource
			{
				get
				{
					return (this.m_flags & DsvRelation.DsvRelationInfoDS.Flags.OptionalSource) == DsvRelation.DsvRelationInfoDS.Flags.OptionalSource;
				}
			}

			// Token: 0x170003BE RID: 958
			// (get) Token: 0x06000F43 RID: 3907 RVA: 0x000303A5 File Offset: 0x0002E5A5
			bool DsvRelation.IDsvRelationInfo.OptionalTarget
			{
				get
				{
					return (this.m_flags & DsvRelation.DsvRelationInfoDS.Flags.OptionalTarget) == DsvRelation.DsvRelationInfoDS.Flags.OptionalTarget;
				}
			}

			// Token: 0x170003BF RID: 959
			// (get) Token: 0x06000F44 RID: 3908 RVA: 0x000303B2 File Offset: 0x0002E5B2
			IDictionary DsvRelation.IDsvRelationInfo.ExtendedProperties
			{
				get
				{
					return this.m_relation.ExtendedProperties;
				}
			}

			// Token: 0x06000F45 RID: 3909 RVA: 0x000303BF File Offset: 0x0002E5BF
			void IPersistable.Serialize(IntermediateFormatWriter writer)
			{
				((IPersistable)new DsvRelation.DsvRelationInfoBinary(this)).Serialize(writer);
			}

			// Token: 0x06000F46 RID: 3910 RVA: 0x000303CD File Offset: 0x0002E5CD
			void IPersistable.Deserialize(IntermediateFormatReader reader)
			{
				throw new InternalModelingException("Deserialize is not supported.");
			}

			// Token: 0x06000F47 RID: 3911 RVA: 0x000303D9 File Offset: 0x0002E5D9
			void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
			{
				throw new InternalModelingException("ResolveReferences is not supported.");
			}

			// Token: 0x06000F48 RID: 3912 RVA: 0x000303E5 File Offset: 0x0002E5E5
			ObjectType IPersistable.GetObjectType()
			{
				return ObjectType.DsvRelation;
			}

			// Token: 0x0400063E RID: 1598
			private readonly DataRelation m_relation;

			// Token: 0x0400063F RID: 1599
			private readonly ReadOnlyCollection<DsvColumn> m_sourceColumns;

			// Token: 0x04000640 RID: 1600
			private readonly ReadOnlyCollection<DsvColumn> m_targetColumns;

			// Token: 0x04000641 RID: 1601
			private readonly DsvRelation.DsvRelationInfoDS.Flags m_flags;

			// Token: 0x020001F0 RID: 496
			private enum Flags : byte
			{
				// Token: 0x04000855 RID: 2133
				None,
				// Token: 0x04000856 RID: 2134
				OneToOne,
				// Token: 0x04000857 RID: 2135
				OptionalSource,
				// Token: 0x04000858 RID: 2136
				OptionalTarget = 4
			}
		}

		// Token: 0x0200015C RID: 348
		private sealed class DsvRelationInfoBinary : DsvRelation.IDsvRelationInfo, IPersistable
		{
			// Token: 0x06000F49 RID: 3913 RVA: 0x000303E9 File Offset: 0x0002E5E9
			internal DsvRelationInfoBinary()
			{
			}

			// Token: 0x06000F4A RID: 3914 RVA: 0x000303F4 File Offset: 0x0002E5F4
			internal DsvRelationInfoBinary(DsvRelation.IDsvRelationInfo relationInfo)
				: this()
			{
				this.m_name = relationInfo.Name;
				this.m_sourceTable = relationInfo.SourceTable;
				this.m_targetTable = relationInfo.TargetTable;
				this.m_sourceColumns = relationInfo.SourceColumns;
				this.m_targetColumns = relationInfo.TargetColumns;
				this.m_sourceConstraint = relationInfo.SourceConstraint;
				this.m_targetConstraint = relationInfo.TargetConstraint;
				this.m_extendedProperties = relationInfo.ExtendedProperties;
				this.m_flags = DsvRelation.DsvRelationInfoBinary.Flags.None;
				if (relationInfo.OneToOne)
				{
					this.m_flags |= DsvRelation.DsvRelationInfoBinary.Flags.OneToOne;
				}
				if (relationInfo.OptionalSource)
				{
					this.m_flags |= DsvRelation.DsvRelationInfoBinary.Flags.OptionalSource;
				}
				if (relationInfo.OptionalTarget)
				{
					this.m_flags |= DsvRelation.DsvRelationInfoBinary.Flags.OptionalTarget;
				}
			}

			// Token: 0x170003C0 RID: 960
			// (get) Token: 0x06000F4B RID: 3915 RVA: 0x000304B0 File Offset: 0x0002E6B0
			string DsvRelation.IDsvRelationInfo.Name
			{
				get
				{
					return this.m_name;
				}
			}

			// Token: 0x170003C1 RID: 961
			// (get) Token: 0x06000F4C RID: 3916 RVA: 0x000304B8 File Offset: 0x0002E6B8
			bool DsvRelation.IDsvRelationInfo.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170003C2 RID: 962
			// (get) Token: 0x06000F4D RID: 3917 RVA: 0x000304BB File Offset: 0x0002E6BB
			DsvTable DsvRelation.IDsvRelationInfo.SourceTable
			{
				get
				{
					return this.m_sourceTable;
				}
			}

			// Token: 0x170003C3 RID: 963
			// (get) Token: 0x06000F4E RID: 3918 RVA: 0x000304C3 File Offset: 0x0002E6C3
			DsvTable DsvRelation.IDsvRelationInfo.TargetTable
			{
				get
				{
					return this.m_targetTable;
				}
			}

			// Token: 0x170003C4 RID: 964
			// (get) Token: 0x06000F4F RID: 3919 RVA: 0x000304CB File Offset: 0x0002E6CB
			ReadOnlyCollection<DsvColumn> DsvRelation.IDsvRelationInfo.SourceColumns
			{
				get
				{
					return this.m_sourceColumns;
				}
			}

			// Token: 0x170003C5 RID: 965
			// (get) Token: 0x06000F50 RID: 3920 RVA: 0x000304D3 File Offset: 0x0002E6D3
			ReadOnlyCollection<DsvColumn> DsvRelation.IDsvRelationInfo.TargetColumns
			{
				get
				{
					return this.m_targetColumns;
				}
			}

			// Token: 0x170003C6 RID: 966
			// (get) Token: 0x06000F51 RID: 3921 RVA: 0x000304DB File Offset: 0x0002E6DB
			DsvForeignKeyConstraint DsvRelation.IDsvRelationInfo.SourceConstraint
			{
				get
				{
					return this.m_sourceConstraint;
				}
			}

			// Token: 0x170003C7 RID: 967
			// (get) Token: 0x06000F52 RID: 3922 RVA: 0x000304E3 File Offset: 0x0002E6E3
			DsvUniqueConstraint DsvRelation.IDsvRelationInfo.TargetConstraint
			{
				get
				{
					return this.m_targetConstraint;
				}
			}

			// Token: 0x170003C8 RID: 968
			// (get) Token: 0x06000F53 RID: 3923 RVA: 0x000304EB File Offset: 0x0002E6EB
			bool DsvRelation.IDsvRelationInfo.OneToOne
			{
				get
				{
					return (this.m_flags & DsvRelation.DsvRelationInfoBinary.Flags.OneToOne) == DsvRelation.DsvRelationInfoBinary.Flags.OneToOne;
				}
			}

			// Token: 0x170003C9 RID: 969
			// (get) Token: 0x06000F54 RID: 3924 RVA: 0x000304F8 File Offset: 0x0002E6F8
			bool DsvRelation.IDsvRelationInfo.OptionalSource
			{
				get
				{
					return (this.m_flags & DsvRelation.DsvRelationInfoBinary.Flags.OptionalSource) == DsvRelation.DsvRelationInfoBinary.Flags.OptionalSource;
				}
			}

			// Token: 0x170003CA RID: 970
			// (get) Token: 0x06000F55 RID: 3925 RVA: 0x00030505 File Offset: 0x0002E705
			bool DsvRelation.IDsvRelationInfo.OptionalTarget
			{
				get
				{
					return (this.m_flags & DsvRelation.DsvRelationInfoBinary.Flags.OptionalTarget) == DsvRelation.DsvRelationInfoBinary.Flags.OptionalTarget;
				}
			}

			// Token: 0x170003CB RID: 971
			// (get) Token: 0x06000F56 RID: 3926 RVA: 0x00030512 File Offset: 0x0002E712
			IDictionary DsvRelation.IDsvRelationInfo.ExtendedProperties
			{
				get
				{
					return this.m_extendedProperties;
				}
			}

			// Token: 0x06000F57 RID: 3927 RVA: 0x0003051C File Offset: 0x0002E71C
			void IPersistable.Serialize(IntermediateFormatWriter writer)
			{
				writer.RegisterDeclaration(DsvRelation.DsvRelationInfoBinary.Declaration);
				while (writer.NextMember())
				{
					switch (writer.CurrentMember.MemberName)
					{
					case MemberName.Name:
						writer.Write(this.m_name);
						continue;
					case MemberName.ExtendedProperties:
						PersistenceHelper.WriteProperyCollection(ref writer, this.m_extendedProperties, new Action<IDictionary>(DsvItem.CleanProperties));
						continue;
					case MemberName.Flags:
						writer.Write((byte)this.m_flags);
						continue;
					case MemberName.SourceTable:
						PersistenceHelper.WriteDsvItemReference(ref writer, this.m_sourceTable);
						continue;
					case MemberName.TargetTable:
						PersistenceHelper.WriteDsvItemReference(ref writer, this.m_targetTable);
						continue;
					case MemberName.SourceColumns:
						PersistenceHelper.WriteDsvItemReferences<DsvColumn>(ref writer, this.m_sourceColumns);
						continue;
					case MemberName.TargetColumns:
						PersistenceHelper.WriteDsvItemReferences<DsvColumn>(ref writer, this.m_targetColumns);
						continue;
					case MemberName.SourceConstraint:
						PersistenceHelper.WriteDsvItemReference(ref writer, this.m_sourceConstraint);
						continue;
					case MemberName.TargetConstraint:
						PersistenceHelper.WriteDsvItemReference(ref writer, this.m_targetConstraint);
						continue;
					}
					throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
				}
			}

			// Token: 0x06000F58 RID: 3928 RVA: 0x00030670 File Offset: 0x0002E870
			void IPersistable.Deserialize(IntermediateFormatReader reader)
			{
				reader.RegisterDeclaration(DsvRelation.DsvRelationInfoBinary.Declaration);
				while (reader.NextMember())
				{
					switch (reader.CurrentMember.MemberName)
					{
					case MemberName.Name:
						this.m_name = reader.ReadString();
						continue;
					case MemberName.ExtendedProperties:
						this.m_extendedProperties = PersistenceHelper.ReadPropertyCollection(ref reader, (string name) => DsvItem.AllowExtendedPropertyForBinaryDeserialization(name));
						continue;
					case MemberName.Flags:
						this.m_flags = (DsvRelation.DsvRelationInfoBinary.Flags)reader.ReadByte();
						continue;
					case MemberName.SourceTable:
						this.m_sourceTable = PersistenceHelper.ReadDsvItemReference<DsvTable>(ref reader, this);
						continue;
					case MemberName.TargetTable:
						this.m_targetTable = PersistenceHelper.ReadDsvItemReference<DsvTable>(ref reader, this);
						continue;
					case MemberName.SourceColumns:
						reader.ReadListOfReferencesNoResolution(this);
						continue;
					case MemberName.TargetColumns:
						reader.ReadListOfReferencesNoResolution(this);
						continue;
					case MemberName.SourceConstraint:
						this.m_sourceConstraint = PersistenceHelper.ReadDsvItemReference<DsvForeignKeyConstraint>(ref reader, this);
						continue;
					case MemberName.TargetConstraint:
						this.m_targetConstraint = PersistenceHelper.ReadDsvItemReference<DsvUniqueConstraint>(ref reader, this);
						continue;
					}
					throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
				}
			}

			// Token: 0x06000F59 RID: 3929 RVA: 0x000307D4 File Offset: 0x0002E9D4
			void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
			{
				List<DsvColumn> list = new List<DsvColumn>();
				List<DsvColumn> list2 = new List<DsvColumn>();
				List<MemberReference> list3;
				if (memberReferencesCollection.TryGetValue(DsvRelation.DsvRelationInfoBinary.Declaration.ObjectType, out list3))
				{
					foreach (MemberReference memberReference in list3)
					{
						switch (memberReference.MemberName)
						{
						case MemberName.SourceTable:
							this.m_sourceTable = PersistenceHelper.ResolveDsvItemReference<DsvTable>(referenceableItems[memberReference.RefID]);
							break;
						case MemberName.TargetTable:
							this.m_targetTable = PersistenceHelper.ResolveDsvItemReference<DsvTable>(referenceableItems[memberReference.RefID]);
							break;
						case MemberName.SourceColumns:
							list.Add(PersistenceHelper.ResolveDsvItemReference<DsvColumn>(referenceableItems[memberReference.RefID]));
							break;
						case MemberName.TargetColumns:
							list2.Add(PersistenceHelper.ResolveDsvItemReference<DsvColumn>(referenceableItems[memberReference.RefID]));
							break;
						case MemberName.SourceConstraint:
							this.m_sourceConstraint = PersistenceHelper.ResolveDsvItemReference<DsvForeignKeyConstraint>(referenceableItems[memberReference.RefID]);
							break;
						case MemberName.TargetConstraint:
							this.m_targetConstraint = PersistenceHelper.ResolveDsvItemReference<DsvUniqueConstraint>(referenceableItems[memberReference.RefID]);
							break;
						default:
							throw new InternalModelingException("Unexpected member: " + memberReference.MemberName.ToString());
						}
					}
				}
				this.m_sourceColumns = new ReadOnlyCollection<DsvColumn>(ArrayUtil.ToArray<DsvColumn>(list));
				this.m_targetColumns = new ReadOnlyCollection<DsvColumn>(ArrayUtil.ToArray<DsvColumn>(list2));
			}

			// Token: 0x06000F5A RID: 3930 RVA: 0x0003096C File Offset: 0x0002EB6C
			ObjectType IPersistable.GetObjectType()
			{
				return ObjectType.DsvRelation;
			}

			// Token: 0x170003CC RID: 972
			// (get) Token: 0x06000F5B RID: 3931 RVA: 0x00030970 File Offset: 0x0002EB70
			private static Declaration Declaration
			{
				get
				{
					return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref DsvRelation.DsvRelationInfoBinary.__declaration, DsvRelation.DsvRelationInfoBinary.__declarationLock, () => new Declaration(ObjectType.DsvRelation, ObjectType.RefHelper, new List<MemberInfo>
					{
						new MemberInfo(MemberName.Name, Token.String),
						new MemberInfo(MemberName.SourceTable, ObjectType.DsvTable, Token.Reference),
						new MemberInfo(MemberName.TargetTable, ObjectType.DsvTable, Token.Reference),
						new MemberInfo(MemberName.SourceColumns, ObjectType.RIFObjectList, Token.Reference, ObjectType.DsvColumn),
						new MemberInfo(MemberName.TargetColumns, ObjectType.RIFObjectList, Token.Reference, ObjectType.DsvColumn),
						new MemberInfo(MemberName.SourceConstraint, ObjectType.DsvForeignKeyConstraint, Token.Reference),
						new MemberInfo(MemberName.TargetConstraint, ObjectType.DsvUniqueConstraint, Token.Reference),
						new MemberInfo(MemberName.ExtendedProperties, ObjectType.StringObjectHashtable, Token.String),
						new MemberInfo(MemberName.Flags, Token.Byte)
					}));
				}
			}

			// Token: 0x04000642 RID: 1602
			private string m_name;

			// Token: 0x04000643 RID: 1603
			private DsvTable m_sourceTable;

			// Token: 0x04000644 RID: 1604
			private DsvTable m_targetTable;

			// Token: 0x04000645 RID: 1605
			private ReadOnlyCollection<DsvColumn> m_sourceColumns;

			// Token: 0x04000646 RID: 1606
			private ReadOnlyCollection<DsvColumn> m_targetColumns;

			// Token: 0x04000647 RID: 1607
			private DsvForeignKeyConstraint m_sourceConstraint;

			// Token: 0x04000648 RID: 1608
			private DsvUniqueConstraint m_targetConstraint;

			// Token: 0x04000649 RID: 1609
			private IDictionary m_extendedProperties;

			// Token: 0x0400064A RID: 1610
			private DsvRelation.DsvRelationInfoBinary.Flags m_flags;

			// Token: 0x0400064B RID: 1611
			private static Declaration __declaration;

			// Token: 0x0400064C RID: 1612
			private static readonly object __declarationLock = new object();

			// Token: 0x020001F1 RID: 497
			private enum Flags : byte
			{
				// Token: 0x0400085A RID: 2138
				None,
				// Token: 0x0400085B RID: 2139
				OneToOne,
				// Token: 0x0400085C RID: 2140
				OptionalSource,
				// Token: 0x0400085D RID: 2141
				OptionalTarget = 4
			}
		}
	}
}
