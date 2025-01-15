using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000074 RID: 116
	public sealed class DsvConstraintCollection : DsvItemCollection<DsvConstraint>
	{
		// Token: 0x060004E9 RID: 1257 RVA: 0x0000F75E File Offset: 0x0000D95E
		internal static DsvConstraintCollection FromConstraintCollection(ConstraintCollection constraints)
		{
			return new DsvConstraintCollection(new DsvConstraintCollection.DsvConstraintCollectionInfoDS(constraints));
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0000F76B File Offset: 0x0000D96B
		private DsvConstraintCollection(DsvConstraintCollection.IDsvConstraintCollectionInfo constraintsInfo)
		{
			this.m_constraintsInfo = constraintsInfo;
		}

		// Token: 0x170000FD RID: 253
		public override DsvConstraint this[int index]
		{
			get
			{
				return this.m_constraintsInfo[index];
			}
		}

		// Token: 0x170000FE RID: 254
		public DsvConstraint this[string name]
		{
			get
			{
				return DsvItemCollection<DsvConstraint>.CheckNameMatch(this.m_constraintsInfo[name], name);
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x0000F79C File Offset: 0x0000D99C
		public override int Count
		{
			get
			{
				return this.m_constraintsInfo.Count;
			}
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0000F7A9 File Offset: 0x0000D9A9
		internal static DsvConstraintCollection FromBinary()
		{
			return new DsvConstraintCollection(new DsvConstraintCollection.DsvConstraintCollectionInfoBinary());
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x0000F7B5 File Offset: 0x0000D9B5
		internal override IPersistable DataStorage
		{
			get
			{
				return this.m_constraintsInfo;
			}
		}

		// Token: 0x04000297 RID: 663
		private readonly DsvConstraintCollection.IDsvConstraintCollectionInfo m_constraintsInfo;

		// Token: 0x02000157 RID: 343
		private interface IDsvConstraintCollectionInfo : IPersistable
		{
			// Token: 0x1700039E RID: 926
			DsvConstraint this[int index] { get; }

			// Token: 0x1700039F RID: 927
			DsvConstraint this[string name] { get; }

			// Token: 0x170003A0 RID: 928
			// (get) Token: 0x06000F18 RID: 3864
			int Count { get; }
		}

		// Token: 0x02000158 RID: 344
		private sealed class DsvConstraintCollectionInfoDS : DsvConstraintCollection.IDsvConstraintCollectionInfo, IPersistable
		{
			// Token: 0x06000F19 RID: 3865 RVA: 0x0002FFA2 File Offset: 0x0002E1A2
			internal DsvConstraintCollectionInfoDS(ConstraintCollection constraints)
			{
				if (constraints == null)
				{
					throw new InternalModelingException("constraints is null");
				}
				this.m_constraints = constraints;
			}

			// Token: 0x170003A1 RID: 929
			DsvConstraint DsvConstraintCollection.IDsvConstraintCollectionInfo.this[int index]
			{
				get
				{
					return DsvConstraint.FromConstraint(this.m_constraints[index]);
				}
			}

			// Token: 0x170003A2 RID: 930
			DsvConstraint DsvConstraintCollection.IDsvConstraintCollectionInfo.this[string name]
			{
				get
				{
					return DsvConstraint.FromConstraint(this.m_constraints[name]);
				}
			}

			// Token: 0x170003A3 RID: 931
			// (get) Token: 0x06000F1C RID: 3868 RVA: 0x0002FFE5 File Offset: 0x0002E1E5
			int DsvConstraintCollection.IDsvConstraintCollectionInfo.Count
			{
				get
				{
					return this.m_constraints.Count;
				}
			}

			// Token: 0x06000F1D RID: 3869 RVA: 0x0002FFF2 File Offset: 0x0002E1F2
			void IPersistable.Serialize(IntermediateFormatWriter writer)
			{
				((IPersistable)new DsvConstraintCollection.DsvConstraintCollectionInfoBinary(this)).Serialize(writer);
			}

			// Token: 0x06000F1E RID: 3870 RVA: 0x00030000 File Offset: 0x0002E200
			void IPersistable.Deserialize(IntermediateFormatReader reader)
			{
				throw new InternalModelingException("Deserialize is not supported.");
			}

			// Token: 0x06000F1F RID: 3871 RVA: 0x0003000C File Offset: 0x0002E20C
			void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
			{
				throw new InternalModelingException("ResolveReferences is not supported.");
			}

			// Token: 0x06000F20 RID: 3872 RVA: 0x00030018 File Offset: 0x0002E218
			ObjectType IPersistable.GetObjectType()
			{
				return ObjectType.DsvConstraintCollection;
			}

			// Token: 0x04000639 RID: 1593
			private readonly ConstraintCollection m_constraints;
		}

		// Token: 0x02000159 RID: 345
		private sealed class DsvConstraintCollectionInfoBinary : DsvConstraintCollection.IDsvConstraintCollectionInfo, IPersistable
		{
			// Token: 0x06000F21 RID: 3873 RVA: 0x0003001C File Offset: 0x0002E21C
			internal DsvConstraintCollectionInfoBinary()
			{
			}

			// Token: 0x06000F22 RID: 3874 RVA: 0x00030024 File Offset: 0x0002E224
			internal DsvConstraintCollectionInfoBinary(DsvConstraintCollection.IDsvConstraintCollectionInfo constraints)
				: this()
			{
				this.m_array = new DsvConstraint[constraints.Count];
				for (int i = 0; i < constraints.Count; i++)
				{
					this.m_array[i] = constraints[i];
				}
			}

			// Token: 0x170003A4 RID: 932
			DsvConstraint DsvConstraintCollection.IDsvConstraintCollectionInfo.this[int index]
			{
				get
				{
					return this.m_array[index];
				}
			}

			// Token: 0x170003A5 RID: 933
			DsvConstraint DsvConstraintCollection.IDsvConstraintCollectionInfo.this[string name]
			{
				get
				{
					return DsvItemCollection<DsvConstraint>.FindDsvItemByName(this.m_array, this.m_dictionary, name);
				}
			}

			// Token: 0x170003A6 RID: 934
			// (get) Token: 0x06000F25 RID: 3877 RVA: 0x00030086 File Offset: 0x0002E286
			int DsvConstraintCollection.IDsvConstraintCollectionInfo.Count
			{
				get
				{
					return this.m_array.Length;
				}
			}

			// Token: 0x06000F26 RID: 3878 RVA: 0x00030090 File Offset: 0x0002E290
			void IPersistable.Serialize(IntermediateFormatWriter writer)
			{
				writer.RegisterDeclaration(DsvConstraintCollection.DsvConstraintCollectionInfoBinary.Declaration);
				while (writer.NextMember())
				{
					if (writer.CurrentMember.MemberName != MemberName.Dictionary)
					{
						throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
					}
					PersistenceHelper.WriteDsvItemArray<DsvConstraint>(ref writer, this.m_array);
				}
			}

			// Token: 0x06000F27 RID: 3879 RVA: 0x00030100 File Offset: 0x0002E300
			void IPersistable.Deserialize(IntermediateFormatReader reader)
			{
				reader.RegisterDeclaration(DsvConstraintCollection.DsvConstraintCollectionInfoBinary.Declaration);
				while (reader.NextMember())
				{
					if (reader.CurrentMember.MemberName != MemberName.Dictionary)
					{
						throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
					}
					this.m_array = PersistenceHelper.ReadDsvItemArray<DsvConstraint>(ref reader);
					if (this.m_array.Length > 30)
					{
						this.m_dictionary = new Dictionary<string, DsvConstraint>(StringComparer.Ordinal);
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

			// Token: 0x06000F28 RID: 3880 RVA: 0x000301CB File Offset: 0x0002E3CB
			void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
			{
				throw new InternalModelingException("ResolveReferences is not supported.");
			}

			// Token: 0x06000F29 RID: 3881 RVA: 0x000301D7 File Offset: 0x0002E3D7
			ObjectType IPersistable.GetObjectType()
			{
				return ObjectType.DsvConstraintCollection;
			}

			// Token: 0x170003A7 RID: 935
			// (get) Token: 0x06000F2A RID: 3882 RVA: 0x000301DB File Offset: 0x0002E3DB
			private static Declaration Declaration
			{
				get
				{
					return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref DsvConstraintCollection.DsvConstraintCollectionInfoBinary.__declaration, DsvConstraintCollection.DsvConstraintCollectionInfoBinary.__declarationLock, () => new Declaration(ObjectType.DsvConstraintCollection, ObjectType.None, new List<MemberInfo>
					{
						new MemberInfo(MemberName.Dictionary, ObjectType.RIFObjectArray, ObjectType.DsvConstraint)
					}));
				}
			}

			// Token: 0x0400063A RID: 1594
			private DsvConstraint[] m_array;

			// Token: 0x0400063B RID: 1595
			private Dictionary<string, DsvConstraint> m_dictionary;

			// Token: 0x0400063C RID: 1596
			private static Declaration __declaration;

			// Token: 0x0400063D RID: 1597
			private static readonly object __declarationLock = new object();
		}
	}
}
