using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000073 RID: 115
	public sealed class DsvForeignKeyConstraint : DsvConstraint
	{
		// Token: 0x060004E2 RID: 1250 RVA: 0x0000F6D4 File Offset: 0x0000D8D4
		internal static DsvForeignKeyConstraint FromForeignKeyConstraint(ForeignKeyConstraint constraint)
		{
			if (constraint == null)
			{
				return null;
			}
			return DsvItem.GetDsvItem<DsvForeignKeyConstraint>(constraint.ExtendedProperties, () => new DsvForeignKeyConstraint(new DsvForeignKeyConstraint.DsvForeignKeyConstraintInfoDS(constraint)));
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0000F714 File Offset: 0x0000D914
		private DsvForeignKeyConstraint(DsvForeignKeyConstraint.IDsvForeignKeyConstraintInfo constraintInfo)
		{
			this.m_constraintInfo = constraintInfo;
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x0000F723 File Offset: 0x0000D923
		public ReadOnlyCollection<DsvColumn> Columns
		{
			get
			{
				return this.m_constraintInfo.Columns;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x0000F730 File Offset: 0x0000D930
		public ReadOnlyCollection<DsvColumn> TargetColumns
		{
			get
			{
				return this.m_constraintInfo.TargetColumns;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x0000F73D File Offset: 0x0000D93D
		public DsvTable TargetTable
		{
			get
			{
				return this.m_constraintInfo.TargetTable;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x0000F74A File Offset: 0x0000D94A
		internal override DsvConstraint.IDsvConstraintInfo DsvConstraintInfo
		{
			get
			{
				return this.m_constraintInfo;
			}
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0000F752 File Offset: 0x0000D952
		internal static DsvForeignKeyConstraint FromBinary()
		{
			return new DsvForeignKeyConstraint(new DsvForeignKeyConstraint.DsvForeignKeyConstraintInfoBinary());
		}

		// Token: 0x04000296 RID: 662
		private readonly DsvForeignKeyConstraint.IDsvForeignKeyConstraintInfo m_constraintInfo;

		// Token: 0x02000153 RID: 339
		private interface IDsvForeignKeyConstraintInfo : DsvConstraint.IDsvConstraintInfo, IPersistable
		{
			// Token: 0x17000394 RID: 916
			// (get) Token: 0x06000F00 RID: 3840
			ReadOnlyCollection<DsvColumn> Columns { get; }

			// Token: 0x17000395 RID: 917
			// (get) Token: 0x06000F01 RID: 3841
			ReadOnlyCollection<DsvColumn> TargetColumns { get; }

			// Token: 0x17000396 RID: 918
			// (get) Token: 0x06000F02 RID: 3842
			DsvTable TargetTable { get; }
		}

		// Token: 0x02000154 RID: 340
		private sealed class DsvForeignKeyConstraintInfoDS : DsvConstraint.DsvConstraintInfoDS, DsvForeignKeyConstraint.IDsvForeignKeyConstraintInfo, DsvConstraint.IDsvConstraintInfo, IPersistable
		{
			// Token: 0x06000F03 RID: 3843 RVA: 0x0002FBE0 File Offset: 0x0002DDE0
			internal DsvForeignKeyConstraintInfoDS(ForeignKeyConstraint constraint)
				: base(constraint)
			{
				this.m_sourceColumns = DsvItem.CreateDataColumnArrayWrapper(constraint.Columns);
				this.m_targetColumns = DsvItem.CreateDataColumnArrayWrapper(constraint.RelatedColumns);
				if (this.m_sourceColumns.Count == 0)
				{
					throw new InternalModelingException("DsvForeignKeyConstraint sourceColumns is empty");
				}
				if (this.m_targetColumns.Count == 0)
				{
					throw new InternalModelingException("DsvForeignKeyConstraint targetColumns is empty");
				}
			}

			// Token: 0x17000397 RID: 919
			// (get) Token: 0x06000F04 RID: 3844 RVA: 0x0002FC46 File Offset: 0x0002DE46
			ReadOnlyCollection<DsvColumn> DsvForeignKeyConstraint.IDsvForeignKeyConstraintInfo.Columns
			{
				get
				{
					return this.m_sourceColumns;
				}
			}

			// Token: 0x17000398 RID: 920
			// (get) Token: 0x06000F05 RID: 3845 RVA: 0x0002FC4E File Offset: 0x0002DE4E
			ReadOnlyCollection<DsvColumn> DsvForeignKeyConstraint.IDsvForeignKeyConstraintInfo.TargetColumns
			{
				get
				{
					return this.m_targetColumns;
				}
			}

			// Token: 0x17000399 RID: 921
			// (get) Token: 0x06000F06 RID: 3846 RVA: 0x0002FC56 File Offset: 0x0002DE56
			DsvTable DsvForeignKeyConstraint.IDsvForeignKeyConstraintInfo.TargetTable
			{
				get
				{
					return DsvTable.FromDataTable(((ForeignKeyConstraint)this.m_constraint).RelatedTable);
				}
			}

			// Token: 0x06000F07 RID: 3847 RVA: 0x0002FC6D File Offset: 0x0002DE6D
			public override void Serialize(IntermediateFormatWriter writer)
			{
				((IPersistable)new DsvForeignKeyConstraint.DsvForeignKeyConstraintInfoBinary(this)).Serialize(writer);
			}

			// Token: 0x06000F08 RID: 3848 RVA: 0x0002FC7B File Offset: 0x0002DE7B
			public override ObjectType GetObjectType()
			{
				return ObjectType.DsvForeignKeyConstraint;
			}

			// Token: 0x04000631 RID: 1585
			private readonly ReadOnlyCollection<DsvColumn> m_sourceColumns;

			// Token: 0x04000632 RID: 1586
			private readonly ReadOnlyCollection<DsvColumn> m_targetColumns;
		}

		// Token: 0x02000155 RID: 341
		private sealed class DsvForeignKeyConstraintInfoBinary : DsvConstraint.DsvConstraintInfoBinary, DsvForeignKeyConstraint.IDsvForeignKeyConstraintInfo, DsvConstraint.IDsvConstraintInfo, IPersistable
		{
			// Token: 0x06000F09 RID: 3849 RVA: 0x0002FC7F File Offset: 0x0002DE7F
			internal DsvForeignKeyConstraintInfoBinary()
			{
			}

			// Token: 0x06000F0A RID: 3850 RVA: 0x0002FC87 File Offset: 0x0002DE87
			internal DsvForeignKeyConstraintInfoBinary(DsvForeignKeyConstraint.IDsvForeignKeyConstraintInfo constraintInfo)
				: base(constraintInfo)
			{
				this.m_sourceColumns = constraintInfo.Columns;
				this.m_targetColumns = constraintInfo.TargetColumns;
				this.m_targetTable = constraintInfo.TargetTable;
			}

			// Token: 0x1700039A RID: 922
			// (get) Token: 0x06000F0B RID: 3851 RVA: 0x0002FCB4 File Offset: 0x0002DEB4
			ReadOnlyCollection<DsvColumn> DsvForeignKeyConstraint.IDsvForeignKeyConstraintInfo.Columns
			{
				get
				{
					return this.m_sourceColumns;
				}
			}

			// Token: 0x1700039B RID: 923
			// (get) Token: 0x06000F0C RID: 3852 RVA: 0x0002FCBC File Offset: 0x0002DEBC
			ReadOnlyCollection<DsvColumn> DsvForeignKeyConstraint.IDsvForeignKeyConstraintInfo.TargetColumns
			{
				get
				{
					return this.m_targetColumns;
				}
			}

			// Token: 0x1700039C RID: 924
			// (get) Token: 0x06000F0D RID: 3853 RVA: 0x0002FCC4 File Offset: 0x0002DEC4
			DsvTable DsvForeignKeyConstraint.IDsvForeignKeyConstraintInfo.TargetTable
			{
				get
				{
					return this.m_targetTable;
				}
			}

			// Token: 0x06000F0E RID: 3854 RVA: 0x0002FCCC File Offset: 0x0002DECC
			public override void Serialize(IntermediateFormatWriter writer)
			{
				base.Serialize(writer);
				writer.RegisterDeclaration(DsvForeignKeyConstraint.DsvForeignKeyConstraintInfoBinary.Declaration);
				while (writer.NextMember())
				{
					switch (writer.CurrentMember.MemberName)
					{
					case MemberName.TargetTable:
						PersistenceHelper.WriteDsvItemReference(ref writer, this.m_targetTable);
						break;
					case MemberName.SourceColumns:
						PersistenceHelper.WriteDsvItemReferences<DsvColumn>(ref writer, this.m_sourceColumns);
						break;
					case MemberName.TargetColumns:
						PersistenceHelper.WriteDsvItemReferences<DsvColumn>(ref writer, this.m_targetColumns);
						break;
					default:
						throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
					}
				}
			}

			// Token: 0x06000F0F RID: 3855 RVA: 0x0002FD78 File Offset: 0x0002DF78
			public override void Deserialize(IntermediateFormatReader reader)
			{
				base.Deserialize(reader);
				reader.RegisterDeclaration(DsvForeignKeyConstraint.DsvForeignKeyConstraintInfoBinary.Declaration);
				while (reader.NextMember())
				{
					switch (reader.CurrentMember.MemberName)
					{
					case MemberName.TargetTable:
						this.m_targetTable = PersistenceHelper.ReadDsvItemReference<DsvTable>(ref reader, this);
						break;
					case MemberName.SourceColumns:
						reader.ReadListOfReferencesNoResolution(this);
						break;
					case MemberName.TargetColumns:
						reader.ReadListOfReferencesNoResolution(this);
						break;
					default:
						throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
					}
				}
			}

			// Token: 0x06000F10 RID: 3856 RVA: 0x0002FE18 File Offset: 0x0002E018
			public override void ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
			{
				base.ResolveReferences(memberReferencesCollection, referenceableItems);
				List<DsvColumn> list = new List<DsvColumn>();
				List<DsvColumn> list2 = new List<DsvColumn>();
				List<MemberReference> list3;
				if (memberReferencesCollection.TryGetValue(DsvForeignKeyConstraint.DsvForeignKeyConstraintInfoBinary.Declaration.ObjectType, out list3))
				{
					foreach (MemberReference memberReference in list3)
					{
						switch (memberReference.MemberName)
						{
						case MemberName.TargetTable:
							this.m_targetTable = PersistenceHelper.ResolveDsvItemReference<DsvTable>(referenceableItems[memberReference.RefID]);
							break;
						case MemberName.SourceColumns:
							list.Add(PersistenceHelper.ResolveDsvItemReference<DsvColumn>(referenceableItems[memberReference.RefID]));
							break;
						case MemberName.TargetColumns:
							list2.Add(PersistenceHelper.ResolveDsvItemReference<DsvColumn>(referenceableItems[memberReference.RefID]));
							break;
						default:
							throw new InternalModelingException("Unexpected member: " + memberReference.MemberName.ToString());
						}
					}
				}
				this.m_sourceColumns = new ReadOnlyCollection<DsvColumn>(ArrayUtil.ToArray<DsvColumn>(list));
				this.m_targetColumns = new ReadOnlyCollection<DsvColumn>(ArrayUtil.ToArray<DsvColumn>(list2));
			}

			// Token: 0x06000F11 RID: 3857 RVA: 0x0002FF48 File Offset: 0x0002E148
			public override ObjectType GetObjectType()
			{
				return ObjectType.DsvForeignKeyConstraint;
			}

			// Token: 0x1700039D RID: 925
			// (get) Token: 0x06000F12 RID: 3858 RVA: 0x0002FF4C File Offset: 0x0002E14C
			private static Declaration Declaration
			{
				get
				{
					return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref DsvForeignKeyConstraint.DsvForeignKeyConstraintInfoBinary.__declaration, DsvForeignKeyConstraint.DsvForeignKeyConstraintInfoBinary.__declarationLock, () => new Declaration(ObjectType.DsvForeignKeyConstraint, ObjectType.DsvConstraint, new List<MemberInfo>
					{
						new MemberInfo(MemberName.SourceColumns, ObjectType.RIFObjectList, Token.Reference, ObjectType.DsvColumn),
						new MemberInfo(MemberName.TargetColumns, ObjectType.RIFObjectList, Token.Reference, ObjectType.DsvColumn),
						new MemberInfo(MemberName.TargetTable, ObjectType.DsvTable, Token.Reference)
					}));
				}
			}

			// Token: 0x04000633 RID: 1587
			private ReadOnlyCollection<DsvColumn> m_sourceColumns;

			// Token: 0x04000634 RID: 1588
			private ReadOnlyCollection<DsvColumn> m_targetColumns;

			// Token: 0x04000635 RID: 1589
			private DsvTable m_targetTable;

			// Token: 0x04000636 RID: 1590
			private static Declaration __declaration;

			// Token: 0x04000637 RID: 1591
			private static readonly object __declarationLock = new object();
		}
	}
}
