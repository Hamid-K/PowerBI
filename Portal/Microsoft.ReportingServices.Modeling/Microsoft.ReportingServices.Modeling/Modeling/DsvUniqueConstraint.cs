using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000072 RID: 114
	public sealed class DsvUniqueConstraint : DsvConstraint
	{
		// Token: 0x060004DB RID: 1243 RVA: 0x0000F648 File Offset: 0x0000D848
		internal static DsvUniqueConstraint FromUniqueConstraint(UniqueConstraint constraint)
		{
			if (constraint == null)
			{
				return null;
			}
			return DsvItem.GetDsvItem<DsvUniqueConstraint>(constraint.ExtendedProperties, () => new DsvUniqueConstraint(new DsvUniqueConstraint.DsvUniqueConstraintInfoDS(constraint)));
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0000F688 File Offset: 0x0000D888
		private DsvUniqueConstraint(DsvUniqueConstraint.IDsvUniqueConstraintInfo constraintInfo)
		{
			this.m_constraintInfo = constraintInfo;
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x0000F697 File Offset: 0x0000D897
		public bool IsPrimaryKey
		{
			get
			{
				return this.m_constraintInfo.IsPrimaryKey;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x0000F6A4 File Offset: 0x0000D8A4
		public ReadOnlyCollection<DsvColumn> Columns
		{
			get
			{
				return this.m_constraintInfo.Columns;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x0000F6B1 File Offset: 0x0000D8B1
		public bool IsLogical
		{
			get
			{
				return base.GetBoolean("IsLogical");
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x0000F6BE File Offset: 0x0000D8BE
		internal override DsvConstraint.IDsvConstraintInfo DsvConstraintInfo
		{
			get
			{
				return this.m_constraintInfo;
			}
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000F6C6 File Offset: 0x0000D8C6
		internal static DsvUniqueConstraint FromBinary()
		{
			return new DsvUniqueConstraint(new DsvUniqueConstraint.DsvUniqueConstraintInfoBinary());
		}

		// Token: 0x04000295 RID: 661
		private readonly DsvUniqueConstraint.IDsvUniqueConstraintInfo m_constraintInfo;

		// Token: 0x0200014F RID: 335
		private interface IDsvUniqueConstraintInfo : DsvConstraint.IDsvConstraintInfo, IPersistable
		{
			// Token: 0x1700038D RID: 909
			// (get) Token: 0x06000EED RID: 3821
			bool IsPrimaryKey { get; }

			// Token: 0x1700038E RID: 910
			// (get) Token: 0x06000EEE RID: 3822
			ReadOnlyCollection<DsvColumn> Columns { get; }
		}

		// Token: 0x02000150 RID: 336
		private sealed class DsvUniqueConstraintInfoDS : DsvConstraint.DsvConstraintInfoDS, DsvUniqueConstraint.IDsvUniqueConstraintInfo, DsvConstraint.IDsvConstraintInfo, IPersistable
		{
			// Token: 0x06000EEF RID: 3823 RVA: 0x0002F8FC File Offset: 0x0002DAFC
			internal DsvUniqueConstraintInfoDS(UniqueConstraint constraint)
				: base(constraint)
			{
				this.m_columns = DsvItem.CreateDataColumnArrayWrapper(constraint.Columns);
				if (this.m_columns.Count == 0)
				{
					throw new InternalModelingException("DsvUniqueConstraints columns is empty");
				}
			}

			// Token: 0x1700038F RID: 911
			// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x0002F92E File Offset: 0x0002DB2E
			bool DsvUniqueConstraint.IDsvUniqueConstraintInfo.IsPrimaryKey
			{
				get
				{
					return ((UniqueConstraint)this.m_constraint).IsPrimaryKey;
				}
			}

			// Token: 0x17000390 RID: 912
			// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x0002F940 File Offset: 0x0002DB40
			ReadOnlyCollection<DsvColumn> DsvUniqueConstraint.IDsvUniqueConstraintInfo.Columns
			{
				get
				{
					return this.m_columns;
				}
			}

			// Token: 0x06000EF2 RID: 3826 RVA: 0x0002F948 File Offset: 0x0002DB48
			public override void Serialize(IntermediateFormatWriter writer)
			{
				((IPersistable)new DsvUniqueConstraint.DsvUniqueConstraintInfoBinary(this)).Serialize(writer);
			}

			// Token: 0x06000EF3 RID: 3827 RVA: 0x0002F956 File Offset: 0x0002DB56
			public override ObjectType GetObjectType()
			{
				return ObjectType.DsvUniqueConstraint;
			}

			// Token: 0x0400062B RID: 1579
			private readonly ReadOnlyCollection<DsvColumn> m_columns;
		}

		// Token: 0x02000151 RID: 337
		private sealed class DsvUniqueConstraintInfoBinary : DsvConstraint.DsvConstraintInfoBinary, DsvUniqueConstraint.IDsvUniqueConstraintInfo, DsvConstraint.IDsvConstraintInfo, IPersistable
		{
			// Token: 0x06000EF4 RID: 3828 RVA: 0x0002F95A File Offset: 0x0002DB5A
			internal DsvUniqueConstraintInfoBinary()
			{
			}

			// Token: 0x06000EF5 RID: 3829 RVA: 0x0002F962 File Offset: 0x0002DB62
			internal DsvUniqueConstraintInfoBinary(DsvUniqueConstraint.IDsvUniqueConstraintInfo constraintInfo)
				: base(constraintInfo)
			{
				this.m_columns = constraintInfo.Columns;
				this.m_flags = DsvUniqueConstraint.DsvUniqueConstraintInfoBinary.Flags.None;
				if (constraintInfo.IsPrimaryKey)
				{
					this.m_flags |= DsvUniqueConstraint.DsvUniqueConstraintInfoBinary.Flags.PrimaryKey;
				}
			}

			// Token: 0x17000391 RID: 913
			// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x0002F994 File Offset: 0x0002DB94
			bool DsvUniqueConstraint.IDsvUniqueConstraintInfo.IsPrimaryKey
			{
				get
				{
					return (this.m_flags & DsvUniqueConstraint.DsvUniqueConstraintInfoBinary.Flags.PrimaryKey) == DsvUniqueConstraint.DsvUniqueConstraintInfoBinary.Flags.PrimaryKey;
				}
			}

			// Token: 0x17000392 RID: 914
			// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x0002F9A1 File Offset: 0x0002DBA1
			ReadOnlyCollection<DsvColumn> DsvUniqueConstraint.IDsvUniqueConstraintInfo.Columns
			{
				get
				{
					return this.m_columns;
				}
			}

			// Token: 0x06000EF8 RID: 3832 RVA: 0x0002F9AC File Offset: 0x0002DBAC
			public override void Serialize(IntermediateFormatWriter writer)
			{
				base.Serialize(writer);
				writer.RegisterDeclaration(DsvUniqueConstraint.DsvUniqueConstraintInfoBinary.Declaration);
				while (writer.NextMember())
				{
					MemberName memberName = writer.CurrentMember.MemberName;
					if (memberName != MemberName.Flags)
					{
						if (memberName != MemberName.Columns)
						{
							throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
						}
						PersistenceHelper.WriteDsvItemReferences<DsvColumn>(ref writer, this.m_columns);
					}
					else
					{
						writer.Write((byte)this.m_flags);
					}
				}
			}

			// Token: 0x06000EF9 RID: 3833 RVA: 0x0002FA38 File Offset: 0x0002DC38
			public override void Deserialize(IntermediateFormatReader reader)
			{
				base.Deserialize(reader);
				reader.RegisterDeclaration(DsvUniqueConstraint.DsvUniqueConstraintInfoBinary.Declaration);
				while (reader.NextMember())
				{
					MemberName memberName = reader.CurrentMember.MemberName;
					if (memberName != MemberName.Flags)
					{
						if (memberName != MemberName.Columns)
						{
							throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
						}
						reader.ReadListOfReferencesNoResolution(this);
					}
					else
					{
						this.m_flags = (DsvUniqueConstraint.DsvUniqueConstraintInfoBinary.Flags)reader.ReadByte();
					}
				}
			}

			// Token: 0x06000EFA RID: 3834 RVA: 0x0002FAC0 File Offset: 0x0002DCC0
			public override void ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
			{
				base.ResolveReferences(memberReferencesCollection, referenceableItems);
				List<DsvColumn> list = new List<DsvColumn>();
				List<MemberReference> list2;
				if (memberReferencesCollection.TryGetValue(DsvUniqueConstraint.DsvUniqueConstraintInfoBinary.Declaration.ObjectType, out list2))
				{
					foreach (MemberReference memberReference in list2)
					{
						if (memberReference.MemberName != MemberName.Columns)
						{
							throw new InternalModelingException("Unexpected member: " + memberReference.MemberName.ToString());
						}
						list.Add(PersistenceHelper.ResolveDsvItemReference<DsvColumn>(referenceableItems[memberReference.RefID]));
					}
				}
				this.m_columns = new ReadOnlyCollection<DsvColumn>(ArrayUtil.ToArray<DsvColumn>(list));
			}

			// Token: 0x06000EFB RID: 3835 RVA: 0x0002FB84 File Offset: 0x0002DD84
			public override ObjectType GetObjectType()
			{
				return ObjectType.DsvUniqueConstraint;
			}

			// Token: 0x17000393 RID: 915
			// (get) Token: 0x06000EFC RID: 3836 RVA: 0x0002FB88 File Offset: 0x0002DD88
			private static Declaration Declaration
			{
				get
				{
					return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref DsvUniqueConstraint.DsvUniqueConstraintInfoBinary.__declaration, DsvUniqueConstraint.DsvUniqueConstraintInfoBinary.__declarationLock, () => new Declaration(ObjectType.DsvUniqueConstraint, ObjectType.DsvConstraint, new List<MemberInfo>
					{
						new MemberInfo(MemberName.Columns, ObjectType.RIFObjectList, Token.Reference, ObjectType.DsvColumn),
						new MemberInfo(MemberName.Flags, Token.Byte)
					}));
				}
			}

			// Token: 0x0400062C RID: 1580
			private ReadOnlyCollection<DsvColumn> m_columns;

			// Token: 0x0400062D RID: 1581
			private DsvUniqueConstraint.DsvUniqueConstraintInfoBinary.Flags m_flags;

			// Token: 0x0400062E RID: 1582
			private static Declaration __declaration;

			// Token: 0x0400062F RID: 1583
			private static readonly object __declarationLock = new object();

			// Token: 0x020001EC RID: 492
			private enum Flags : byte
			{
				// Token: 0x0400084C RID: 2124
				None,
				// Token: 0x0400084D RID: 2125
				PrimaryKey
			}
		}
	}
}
