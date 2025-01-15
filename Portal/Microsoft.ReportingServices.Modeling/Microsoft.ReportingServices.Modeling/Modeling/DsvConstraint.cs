using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Security.Permissions;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000071 RID: 113
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class DsvConstraint : DsvItem
	{
		// Token: 0x060004D2 RID: 1234 RVA: 0x0000F590 File Offset: 0x0000D790
		internal static DsvConstraint FromConstraint(Constraint constraint)
		{
			if (constraint is UniqueConstraint)
			{
				return DsvUniqueConstraint.FromUniqueConstraint((UniqueConstraint)constraint);
			}
			if (constraint is ForeignKeyConstraint)
			{
				return DsvForeignKeyConstraint.FromForeignKeyConstraint((ForeignKeyConstraint)constraint);
			}
			if (constraint == null)
			{
				return null;
			}
			throw new InternalModelingException("Unknown Constraint '" + ((constraint != null) ? constraint.ToString() : null) + "'");
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0000F5EB File Offset: 0x0000D7EB
		internal DsvConstraint()
		{
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x0000F5F3 File Offset: 0x0000D7F3
		public override string Name
		{
			get
			{
				return this.DsvConstraintInfo.Name;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x0000F600 File Offset: 0x0000D800
		public override bool IsReadOnly
		{
			get
			{
				return this.DsvConstraintInfo.IsReadOnly;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x0000F60D File Offset: 0x0000D80D
		public DsvTable Table
		{
			get
			{
				return this.DsvConstraintInfo.Table;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x0000F61A File Offset: 0x0000D81A
		public string DbConstraintName
		{
			get
			{
				return base.GetString("DbConstraintName") ?? string.Empty;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x0000F630 File Offset: 0x0000D830
		protected override IDictionary Properties
		{
			get
			{
				return this.DsvConstraintInfo.ExtendedProperties;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060004D9 RID: 1241
		internal abstract DsvConstraint.IDsvConstraintInfo DsvConstraintInfo { get; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x0000F63D File Offset: 0x0000D83D
		internal override IPersistable DataStorage
		{
			get
			{
				return this.DsvConstraintInfo;
			}
		}

		// Token: 0x04000294 RID: 660
		private const string DbConstraintNameExtProperty = "DbConstraintName";

		// Token: 0x0200014C RID: 332
		internal interface IDsvConstraintInfo : IPersistable
		{
			// Token: 0x17000380 RID: 896
			// (get) Token: 0x06000ED4 RID: 3796
			string Name { get; }

			// Token: 0x17000381 RID: 897
			// (get) Token: 0x06000ED5 RID: 3797
			bool IsReadOnly { get; }

			// Token: 0x17000382 RID: 898
			// (get) Token: 0x06000ED6 RID: 3798
			DsvTable Table { get; }

			// Token: 0x17000383 RID: 899
			// (get) Token: 0x06000ED7 RID: 3799
			IDictionary ExtendedProperties { get; }
		}

		// Token: 0x0200014D RID: 333
		internal abstract class DsvConstraintInfoDS : DsvConstraint.IDsvConstraintInfo, IPersistable
		{
			// Token: 0x06000ED8 RID: 3800 RVA: 0x0002F5EB File Offset: 0x0002D7EB
			protected DsvConstraintInfoDS(Constraint constraint)
			{
				if (constraint == null)
				{
					throw new InternalModelingException("constraint is null");
				}
				this.m_constraint = constraint;
			}

			// Token: 0x17000384 RID: 900
			// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x0002F608 File Offset: 0x0002D808
			string DsvConstraint.IDsvConstraintInfo.Name
			{
				get
				{
					return this.m_constraint.ConstraintName;
				}
			}

			// Token: 0x17000385 RID: 901
			// (get) Token: 0x06000EDA RID: 3802 RVA: 0x0002F615 File Offset: 0x0002D815
			bool DsvConstraint.IDsvConstraintInfo.IsReadOnly
			{
				get
				{
					return DsvItem.IsDataSetReadonly(this.m_constraint.Table.DataSet);
				}
			}

			// Token: 0x17000386 RID: 902
			// (get) Token: 0x06000EDB RID: 3803 RVA: 0x0002F62C File Offset: 0x0002D82C
			DsvTable DsvConstraint.IDsvConstraintInfo.Table
			{
				get
				{
					return DsvTable.FromDataTable(this.m_constraint.Table);
				}
			}

			// Token: 0x17000387 RID: 903
			// (get) Token: 0x06000EDC RID: 3804 RVA: 0x0002F63E File Offset: 0x0002D83E
			IDictionary DsvConstraint.IDsvConstraintInfo.ExtendedProperties
			{
				get
				{
					return this.m_constraint.ExtendedProperties;
				}
			}

			// Token: 0x06000EDD RID: 3805
			public abstract void Serialize(IntermediateFormatWriter writer);

			// Token: 0x06000EDE RID: 3806 RVA: 0x0002F64B File Offset: 0x0002D84B
			void IPersistable.Deserialize(IntermediateFormatReader reader)
			{
				throw new InternalModelingException("Deserialize is not supported.");
			}

			// Token: 0x06000EDF RID: 3807 RVA: 0x0002F657 File Offset: 0x0002D857
			void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
			{
				throw new InternalModelingException("ResolveReferences is not supported.");
			}

			// Token: 0x06000EE0 RID: 3808
			public abstract ObjectType GetObjectType();

			// Token: 0x04000625 RID: 1573
			protected readonly Constraint m_constraint;
		}

		// Token: 0x0200014E RID: 334
		internal abstract class DsvConstraintInfoBinary : DsvConstraint.IDsvConstraintInfo, IPersistable
		{
			// Token: 0x06000EE1 RID: 3809 RVA: 0x0002F663 File Offset: 0x0002D863
			protected DsvConstraintInfoBinary()
			{
			}

			// Token: 0x06000EE2 RID: 3810 RVA: 0x0002F66B File Offset: 0x0002D86B
			protected DsvConstraintInfoBinary(DsvConstraint.IDsvConstraintInfo constraintInfo)
				: this()
			{
				this.m_name = constraintInfo.Name;
				this.m_table = constraintInfo.Table;
				this.m_extendedProperties = constraintInfo.ExtendedProperties;
			}

			// Token: 0x17000388 RID: 904
			// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x0002F697 File Offset: 0x0002D897
			string DsvConstraint.IDsvConstraintInfo.Name
			{
				get
				{
					return this.m_name;
				}
			}

			// Token: 0x17000389 RID: 905
			// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x0002F69F File Offset: 0x0002D89F
			bool DsvConstraint.IDsvConstraintInfo.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700038A RID: 906
			// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x0002F6A2 File Offset: 0x0002D8A2
			DsvTable DsvConstraint.IDsvConstraintInfo.Table
			{
				get
				{
					return this.m_table;
				}
			}

			// Token: 0x1700038B RID: 907
			// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x0002F6AA File Offset: 0x0002D8AA
			IDictionary DsvConstraint.IDsvConstraintInfo.ExtendedProperties
			{
				get
				{
					return this.m_extendedProperties;
				}
			}

			// Token: 0x06000EE7 RID: 3815 RVA: 0x0002F6B4 File Offset: 0x0002D8B4
			public virtual void Serialize(IntermediateFormatWriter writer)
			{
				writer.RegisterDeclaration(DsvConstraint.DsvConstraintInfoBinary.Declaration);
				while (writer.NextMember())
				{
					MemberName memberName = writer.CurrentMember.MemberName;
					if (memberName != MemberName.Name)
					{
						if (memberName != MemberName.Table)
						{
							if (memberName != MemberName.ExtendedProperties)
							{
								throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
							}
							PersistenceHelper.WriteProperyCollection(ref writer, this.m_extendedProperties, new Action<IDictionary>(DsvItem.CleanProperties));
						}
						else
						{
							PersistenceHelper.WriteDsvItemReference(ref writer, this.m_table);
						}
					}
					else
					{
						writer.Write(this.m_name);
					}
				}
			}

			// Token: 0x06000EE8 RID: 3816 RVA: 0x0002F75C File Offset: 0x0002D95C
			public virtual void Deserialize(IntermediateFormatReader reader)
			{
				reader.RegisterDeclaration(DsvConstraint.DsvConstraintInfoBinary.Declaration);
				while (reader.NextMember())
				{
					MemberName memberName = reader.CurrentMember.MemberName;
					if (memberName != MemberName.Name)
					{
						if (memberName != MemberName.Table)
						{
							if (memberName != MemberName.ExtendedProperties)
							{
								throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
							}
							this.m_extendedProperties = PersistenceHelper.ReadPropertyCollection(ref reader, (string name) => string.CompareOrdinal(name, "DbConstraintName") == 0 || DsvItem.AllowExtendedPropertyForBinaryDeserialization(name));
						}
						else
						{
							this.m_table = PersistenceHelper.ReadDsvItemReference<DsvTable>(ref reader, this);
						}
					}
					else
					{
						this.m_name = reader.ReadString();
					}
				}
			}

			// Token: 0x06000EE9 RID: 3817 RVA: 0x0002F81C File Offset: 0x0002DA1C
			public virtual void ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
			{
				List<MemberReference> list;
				if (memberReferencesCollection.TryGetValue(DsvConstraint.DsvConstraintInfoBinary.Declaration.ObjectType, out list))
				{
					foreach (MemberReference memberReference in list)
					{
						if (memberReference.MemberName != MemberName.Table)
						{
							throw new InternalModelingException("Unexpected member: " + memberReference.MemberName.ToString());
						}
						this.m_table = PersistenceHelper.ResolveDsvItemReference<DsvTable>(referenceableItems[memberReference.RefID]);
					}
				}
			}

			// Token: 0x06000EEA RID: 3818
			public abstract ObjectType GetObjectType();

			// Token: 0x1700038C RID: 908
			// (get) Token: 0x06000EEB RID: 3819 RVA: 0x0002F8C0 File Offset: 0x0002DAC0
			private static Declaration Declaration
			{
				get
				{
					return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref DsvConstraint.DsvConstraintInfoBinary.__declaration, DsvConstraint.DsvConstraintInfoBinary.__declarationLock, () => new Declaration(ObjectType.DsvConstraint, ObjectType.RefHelper, new List<MemberInfo>
					{
						new MemberInfo(MemberName.Name, Token.String),
						new MemberInfo(MemberName.Table, ObjectType.DsvTable, Token.Reference),
						new MemberInfo(MemberName.ExtendedProperties, ObjectType.StringObjectHashtable, Token.String)
					}));
				}
			}

			// Token: 0x04000626 RID: 1574
			private string m_name;

			// Token: 0x04000627 RID: 1575
			private DsvTable m_table;

			// Token: 0x04000628 RID: 1576
			private IDictionary m_extendedProperties;

			// Token: 0x04000629 RID: 1577
			private static Declaration __declaration;

			// Token: 0x0400062A RID: 1578
			private static readonly object __declarationLock = new object();
		}
	}
}
