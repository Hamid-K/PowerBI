using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Xml;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x0200006C RID: 108
	public sealed class DsvTable : DsvItem, IHasFriendlyName
	{
		// Token: 0x0600047B RID: 1147 RVA: 0x0000EE4C File Offset: 0x0000D04C
		internal static DsvTable FromDataTable(DataTable table)
		{
			if (table == null)
			{
				return null;
			}
			return DsvItem.GetDsvItem<DsvTable>(table.ExtendedProperties, () => new DsvTable(new DsvTable.DsvTableInfoDS(table)));
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000EE8C File Offset: 0x0000D08C
		private DsvTable(DsvTable.IDsvTableInfo tableInfo)
		{
			this.m_tableInfo = tableInfo;
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0000EE9B File Offset: 0x0000D09B
		public override string Name
		{
			get
			{
				return this.m_tableInfo.Name;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x0000EEA8 File Offset: 0x0000D0A8
		public override bool IsReadOnly
		{
			get
			{
				return this.m_tableInfo.IsReadOnly;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0000EEB5 File Offset: 0x0000D0B5
		public string Description
		{
			get
			{
				return base.GetString("Description") ?? string.Empty;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0000EECC File Offset: 0x0000D0CC
		public string DataSourceID
		{
			get
			{
				string @string = base.GetString("DataSourceID");
				if (!string.IsNullOrEmpty(@string))
				{
					return @string;
				}
				return null;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0000EEF0 File Offset: 0x0000D0F0
		public string DbSchemaName
		{
			get
			{
				return base.GetString("DbSchemaName") ?? string.Empty;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x0000EF06 File Offset: 0x0000D106
		public string DbTableName
		{
			get
			{
				return base.GetString("DbTableName") ?? string.Empty;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0000EF1C File Offset: 0x0000D11C
		public string FriendlyName
		{
			get
			{
				return base.GetString("FriendlyName") ?? string.Empty;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x0000EF32 File Offset: 0x0000D132
		public DsvTableType TableType
		{
			get
			{
				return base.GetEnum<DsvTableType>("TableType");
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0000EF3F File Offset: 0x0000D13F
		public bool IsLogical
		{
			get
			{
				return base.GetBoolean("IsLogical");
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x0000EF4C File Offset: 0x0000D14C
		public string QueryDefinition
		{
			get
			{
				return base.GetString("QueryDefinition") ?? string.Empty;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0000EF62 File Offset: 0x0000D162
		public DsvColumnCollection Columns
		{
			get
			{
				return this.m_tableInfo.Columns;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x0000EF6F File Offset: 0x0000D16F
		public DsvConstraintCollection Constraints
		{
			get
			{
				return this.m_tableInfo.Constraints;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x0000EF7C File Offset: 0x0000D17C
		public IEnumerable<DsvUniqueConstraint> UniqueConstraints
		{
			get
			{
				int num;
				for (int i = 0; i < this.Constraints.Count; i = num + 1)
				{
					if (this.Constraints[i] is DsvUniqueConstraint)
					{
						yield return (DsvUniqueConstraint)this.Constraints[i];
					}
					num = i;
				}
				yield break;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x0000EF8C File Offset: 0x0000D18C
		public ReadOnlyCollection<DsvColumn> PrimaryKey
		{
			get
			{
				return this.m_tableInfo.PrimaryKey;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0000EF99 File Offset: 0x0000D199
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x0000EFA6 File Offset: 0x0000D1A6
		public long? RowCount
		{
			get
			{
				return base.GetNullableInt64("stats_RowCount");
			}
			set
			{
				base.SetNullableInt64("stats_RowCount", value);
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x0000EFB4 File Offset: 0x0000D1B4
		public DsvTable.ColumnGroupUniqueRowsCollection ColumnGroupUniqueRows
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<DsvTable.ColumnGroupUniqueRowsCollection>(ref this.__columnGroupUniqueRows, DsvTable.__columnGroupUniqueRowsLock, () => new DsvTable.ColumnGroupUniqueRowsCollection(this));
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000EFD4 File Offset: 0x0000D1D4
		public bool AreColumnsUnique(ICollection<DsvColumn> columns)
		{
			foreach (DsvUniqueConstraint dsvUniqueConstraint in this.UniqueConstraints)
			{
				if (dsvUniqueConstraint.Columns.Count <= columns.Count)
				{
					bool flag = true;
					foreach (DsvColumn dsvColumn in dsvUniqueConstraint.Columns)
					{
						flag = flag && columns.Contains(dsvColumn);
					}
					if (flag)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000F080 File Offset: 0x0000D280
		internal Dictionary<string, List<DsvColumn>> GetColumnGroupsDictionary()
		{
			Dictionary<string, List<DsvColumn>> dictionary = new Dictionary<string, List<DsvColumn>>();
			foreach (DsvColumn dsvColumn in this.Columns)
			{
				foreach (string text in dsvColumn.ColumnGroups)
				{
					List<DsvColumn> list;
					if (!dictionary.TryGetValue(text, out list))
					{
						list = new List<DsvColumn>();
						dictionary.Add(text, list);
					}
					list.Add(dsvColumn);
				}
			}
			return dictionary;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x0000F134 File Offset: 0x0000D334
		protected override IDictionary Properties
		{
			get
			{
				return this.m_tableInfo.ExtendedProperties;
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000F144 File Offset: 0x0000D344
		internal new static void CleanProperties(IDictionary properties)
		{
			DsvTable.ColumnGroupUniqueRowsCollection columnGroupUniqueRowsCollection = properties["stats_ColumnGroupUniqueRows"] as DsvTable.ColumnGroupUniqueRowsCollection;
			if (columnGroupUniqueRowsCollection != null && columnGroupUniqueRowsCollection.Count == 0)
			{
				properties.Remove("stats_ColumnGroupUniqueRows");
			}
			DsvItem.CleanProperties(properties);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0000F17E File Offset: 0x0000D37E
		internal static DsvTable FromBinary()
		{
			return new DsvTable(new DsvTable.DsvTableInfoBinary());
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0000F18A File Offset: 0x0000D38A
		internal override IPersistable DataStorage
		{
			get
			{
				return this.m_tableInfo;
			}
		}

		// Token: 0x0400026F RID: 623
		internal const string PrimaryKeyObjProperty = "PrimaryKey";

		// Token: 0x04000270 RID: 624
		private const string DataSourceIdExtProperty = "DataSourceID";

		// Token: 0x04000271 RID: 625
		public const string DbSchemaNameExtProperty = "DbSchemaName";

		// Token: 0x04000272 RID: 626
		public const string DbTableNameExtProperty = "DbTableName";

		// Token: 0x04000273 RID: 627
		public const string TableTypeExtProperty = "TableType";

		// Token: 0x04000274 RID: 628
		public static readonly string TableTypeExtPropertyValue = DsvTableType.Table.ToString();

		// Token: 0x04000275 RID: 629
		private const string QueryDefinitionExtProperty = "QueryDefinition";

		// Token: 0x04000276 RID: 630
		private const string RowCountExtProperty = "stats_RowCount";

		// Token: 0x04000277 RID: 631
		private const string ColumnGroupUniqueRowsExtProperty = "stats_ColumnGroupUniqueRows";

		// Token: 0x04000278 RID: 632
		private readonly DsvTable.IDsvTableInfo m_tableInfo;

		// Token: 0x04000279 RID: 633
		private DsvTable.ColumnGroupUniqueRowsCollection __columnGroupUniqueRows;

		// Token: 0x0400027A RID: 634
		private static readonly object __columnGroupUniqueRowsLock = new object();

		// Token: 0x0200013B RID: 315
		private interface IDsvTableInfo : IPersistable
		{
			// Token: 0x1700032F RID: 815
			// (get) Token: 0x06000E39 RID: 3641
			string Name { get; }

			// Token: 0x17000330 RID: 816
			// (get) Token: 0x06000E3A RID: 3642
			bool IsReadOnly { get; }

			// Token: 0x17000331 RID: 817
			// (get) Token: 0x06000E3B RID: 3643
			DsvColumnCollection Columns { get; }

			// Token: 0x17000332 RID: 818
			// (get) Token: 0x06000E3C RID: 3644
			DsvConstraintCollection Constraints { get; }

			// Token: 0x17000333 RID: 819
			// (get) Token: 0x06000E3D RID: 3645
			ReadOnlyCollection<DsvColumn> PrimaryKey { get; }

			// Token: 0x17000334 RID: 820
			// (get) Token: 0x06000E3E RID: 3646
			IDictionary ExtendedProperties { get; }
		}

		// Token: 0x0200013C RID: 316
		private sealed class DsvTableInfoDS : DsvTable.IDsvTableInfo, IPersistable
		{
			// Token: 0x06000E3F RID: 3647 RVA: 0x0002E148 File Offset: 0x0002C348
			internal DsvTableInfoDS(DataTable table)
			{
				if (table == null)
				{
					throw new InternalModelingException("table is null");
				}
				this.m_table = table;
				this.m_columns = DsvColumnCollection.FromDataColumnCollection(table.Columns);
				this.m_constraints = DsvConstraintCollection.FromConstraintCollection(table.Constraints);
				this.m_primaryKey = DsvItem.CreateDataColumnArrayWrapper(table.PrimaryKey);
			}

			// Token: 0x17000335 RID: 821
			// (get) Token: 0x06000E40 RID: 3648 RVA: 0x0002E1A3 File Offset: 0x0002C3A3
			string DsvTable.IDsvTableInfo.Name
			{
				get
				{
					return this.m_table.TableName;
				}
			}

			// Token: 0x17000336 RID: 822
			// (get) Token: 0x06000E41 RID: 3649 RVA: 0x0002E1B0 File Offset: 0x0002C3B0
			bool DsvTable.IDsvTableInfo.IsReadOnly
			{
				get
				{
					return DsvItem.IsDataSetReadonly(this.m_table.DataSet);
				}
			}

			// Token: 0x17000337 RID: 823
			// (get) Token: 0x06000E42 RID: 3650 RVA: 0x0002E1C2 File Offset: 0x0002C3C2
			DsvColumnCollection DsvTable.IDsvTableInfo.Columns
			{
				get
				{
					return this.m_columns;
				}
			}

			// Token: 0x17000338 RID: 824
			// (get) Token: 0x06000E43 RID: 3651 RVA: 0x0002E1CA File Offset: 0x0002C3CA
			DsvConstraintCollection DsvTable.IDsvTableInfo.Constraints
			{
				get
				{
					return this.m_constraints;
				}
			}

			// Token: 0x17000339 RID: 825
			// (get) Token: 0x06000E44 RID: 3652 RVA: 0x0002E1D2 File Offset: 0x0002C3D2
			ReadOnlyCollection<DsvColumn> DsvTable.IDsvTableInfo.PrimaryKey
			{
				get
				{
					return this.m_primaryKey;
				}
			}

			// Token: 0x1700033A RID: 826
			// (get) Token: 0x06000E45 RID: 3653 RVA: 0x0002E1DA File Offset: 0x0002C3DA
			IDictionary DsvTable.IDsvTableInfo.ExtendedProperties
			{
				get
				{
					return this.m_table.ExtendedProperties;
				}
			}

			// Token: 0x06000E46 RID: 3654 RVA: 0x0002E1E7 File Offset: 0x0002C3E7
			void IPersistable.Serialize(IntermediateFormatWriter writer)
			{
				((IPersistable)new DsvTable.DsvTableInfoBinary(this)).Serialize(writer);
			}

			// Token: 0x06000E47 RID: 3655 RVA: 0x0002E1F5 File Offset: 0x0002C3F5
			void IPersistable.Deserialize(IntermediateFormatReader reader)
			{
				throw new InternalModelingException("Deserialize is not supported.");
			}

			// Token: 0x06000E48 RID: 3656 RVA: 0x0002E201 File Offset: 0x0002C401
			void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
			{
				throw new InternalModelingException("ResolveReferences is not supported.");
			}

			// Token: 0x06000E49 RID: 3657 RVA: 0x0002E20D File Offset: 0x0002C40D
			ObjectType IPersistable.GetObjectType()
			{
				return ObjectType.DsvTable;
			}

			// Token: 0x040005F5 RID: 1525
			private readonly DataTable m_table;

			// Token: 0x040005F6 RID: 1526
			private readonly DsvColumnCollection m_columns;

			// Token: 0x040005F7 RID: 1527
			private readonly DsvConstraintCollection m_constraints;

			// Token: 0x040005F8 RID: 1528
			private readonly ReadOnlyCollection<DsvColumn> m_primaryKey;
		}

		// Token: 0x0200013D RID: 317
		public sealed class ColumnGroupUniqueRowsCollection : IEnumerable<KeyValuePair<string, long>>, IEnumerable
		{
			// Token: 0x06000E4A RID: 3658 RVA: 0x0002E214 File Offset: 0x0002C414
			internal ColumnGroupUniqueRowsCollection(DsvTable owner)
			{
				this.m_owner = owner;
				if (owner.Properties["stats_ColumnGroupUniqueRows"] != null)
				{
					try
					{
						this.LoadXml(owner.Properties["stats_ColumnGroupUniqueRows"].ToString());
					}
					catch (XmlException)
					{
						this.m_dict.Clear();
					}
				}
				owner.Properties["stats_ColumnGroupUniqueRows"] = this;
			}

			// Token: 0x1700033B RID: 827
			public long? this[string columnGroup]
			{
				get
				{
					long num;
					if (this.m_dict.TryGetValue(columnGroup, out num))
					{
						return new long?(num);
					}
					return null;
				}
				set
				{
					this.m_owner.CheckWriteable();
					if (value == null)
					{
						this.m_dict.Remove(columnGroup);
						return;
					}
					this.m_dict[columnGroup] = value.Value;
				}
			}

			// Token: 0x1700033C RID: 828
			// (get) Token: 0x06000E4D RID: 3661 RVA: 0x0002E2FC File Offset: 0x0002C4FC
			public int Count
			{
				get
				{
					return this.m_dict.Count;
				}
			}

			// Token: 0x06000E4E RID: 3662 RVA: 0x0002E309 File Offset: 0x0002C509
			public bool Contains(string columnGroup)
			{
				return this.m_dict.ContainsKey(columnGroup);
			}

			// Token: 0x06000E4F RID: 3663 RVA: 0x0002E317 File Offset: 0x0002C517
			public IEnumerator<KeyValuePair<string, long>> GetEnumerator()
			{
				return this.m_dict.GetEnumerator();
			}

			// Token: 0x06000E50 RID: 3664 RVA: 0x0002E329 File Offset: 0x0002C529
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.m_dict.GetEnumerator();
			}

			// Token: 0x06000E51 RID: 3665 RVA: 0x0002E33B File Offset: 0x0002C53B
			public void Add(string columnGroup, long uniqueRows)
			{
				this.m_owner.CheckWriteable();
				this.m_dict.Add(columnGroup, uniqueRows);
			}

			// Token: 0x06000E52 RID: 3666 RVA: 0x0002E355 File Offset: 0x0002C555
			public void Clear()
			{
				this.m_owner.CheckWriteable();
				this.m_dict.Clear();
			}

			// Token: 0x06000E53 RID: 3667 RVA: 0x0002E36D File Offset: 0x0002C56D
			public bool Remove(string columnGroup)
			{
				this.m_owner.CheckWriteable();
				return this.m_dict.Remove(columnGroup);
			}

			// Token: 0x06000E54 RID: 3668 RVA: 0x0002E386 File Offset: 0x0002C586
			public override string ToString()
			{
				return XmlFragmentUtil.ToXmlString(new Action<XmlWriter>(this.WriteXml));
			}

			// Token: 0x06000E55 RID: 3669 RVA: 0x0002E39C File Offset: 0x0002C59C
			private void LoadXml(string xml)
			{
				using (XmlReader xmlReader = XmlFragmentUtil.ReadXmlString(xml))
				{
					XmlUtil.CheckElement(xmlReader, "ColumnGroupUniqueRows", string.Empty);
					if (xmlReader.ReadToDescendant("GroupRows"))
					{
						for (;;)
						{
							string text = null;
							long num = 0L;
							try
							{
								text = xmlReader.GetAttribute("Key");
								num = XmlConvert.ToInt64(xmlReader.GetAttribute("Value"));
							}
							catch (FormatException)
							{
								goto IL_0060;
							}
							goto IL_004B;
							IL_0060:
							if (!xmlReader.ReadToNextSibling("GroupRows"))
							{
								break;
							}
							continue;
							IL_004B:
							if (!string.IsNullOrEmpty(text))
							{
								this.m_dict[text] = num;
								goto IL_0060;
							}
							goto IL_0060;
						}
					}
				}
			}

			// Token: 0x06000E56 RID: 3670 RVA: 0x0002E440 File Offset: 0x0002C640
			private void WriteXml(XmlWriter xw)
			{
				xw.WriteStartElement("ColumnGroupUniqueRows");
				foreach (KeyValuePair<string, long> keyValuePair in this.m_dict)
				{
					xw.WriteStartElement("GroupRows");
					xw.WriteAttributeString("Key", keyValuePair.Key);
					xw.WriteAttributeString("Value", XmlConvert.ToString(keyValuePair.Value));
					xw.WriteEndElement();
				}
				xw.WriteEndElement();
			}

			// Token: 0x040005F9 RID: 1529
			private const string ColumnGroupUniqueRowsElem = "ColumnGroupUniqueRows";

			// Token: 0x040005FA RID: 1530
			private const string GroupRowsElem = "GroupRows";

			// Token: 0x040005FB RID: 1531
			private const string KeyAttr = "Key";

			// Token: 0x040005FC RID: 1532
			private const string ValueAttr = "Value";

			// Token: 0x040005FD RID: 1533
			private readonly DsvTable m_owner;

			// Token: 0x040005FE RID: 1534
			private readonly Dictionary<string, long> m_dict = new Dictionary<string, long>();
		}

		// Token: 0x0200013E RID: 318
		private sealed class DsvTableInfoBinary : DsvTable.IDsvTableInfo, IPersistable
		{
			// Token: 0x06000E57 RID: 3671 RVA: 0x0002E4D8 File Offset: 0x0002C6D8
			internal DsvTableInfoBinary()
			{
			}

			// Token: 0x06000E58 RID: 3672 RVA: 0x0002E4E0 File Offset: 0x0002C6E0
			internal DsvTableInfoBinary(DsvTable.IDsvTableInfo tableInfo)
				: this()
			{
				this.m_name = tableInfo.Name;
				this.m_columns = tableInfo.Columns;
				this.m_constraints = tableInfo.Constraints;
				this.m_primaryKey = tableInfo.PrimaryKey;
				this.m_extendedProperties = tableInfo.ExtendedProperties;
			}

			// Token: 0x1700033D RID: 829
			// (get) Token: 0x06000E59 RID: 3673 RVA: 0x0002E52F File Offset: 0x0002C72F
			string DsvTable.IDsvTableInfo.Name
			{
				get
				{
					return this.m_name;
				}
			}

			// Token: 0x1700033E RID: 830
			// (get) Token: 0x06000E5A RID: 3674 RVA: 0x0002E537 File Offset: 0x0002C737
			bool DsvTable.IDsvTableInfo.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700033F RID: 831
			// (get) Token: 0x06000E5B RID: 3675 RVA: 0x0002E53A File Offset: 0x0002C73A
			DsvColumnCollection DsvTable.IDsvTableInfo.Columns
			{
				get
				{
					return this.m_columns;
				}
			}

			// Token: 0x17000340 RID: 832
			// (get) Token: 0x06000E5C RID: 3676 RVA: 0x0002E542 File Offset: 0x0002C742
			DsvConstraintCollection DsvTable.IDsvTableInfo.Constraints
			{
				get
				{
					return this.m_constraints;
				}
			}

			// Token: 0x17000341 RID: 833
			// (get) Token: 0x06000E5D RID: 3677 RVA: 0x0002E54A File Offset: 0x0002C74A
			ReadOnlyCollection<DsvColumn> DsvTable.IDsvTableInfo.PrimaryKey
			{
				get
				{
					return this.m_primaryKey;
				}
			}

			// Token: 0x17000342 RID: 834
			// (get) Token: 0x06000E5E RID: 3678 RVA: 0x0002E552 File Offset: 0x0002C752
			IDictionary DsvTable.IDsvTableInfo.ExtendedProperties
			{
				get
				{
					return this.m_extendedProperties;
				}
			}

			// Token: 0x06000E5F RID: 3679 RVA: 0x0002E55C File Offset: 0x0002C75C
			void IPersistable.Serialize(IntermediateFormatWriter writer)
			{
				writer.RegisterDeclaration(DsvTable.DsvTableInfoBinary.Declaration);
				while (writer.NextMember())
				{
					MemberName memberName = writer.CurrentMember.MemberName;
					if (memberName != MemberName.Name)
					{
						switch (memberName)
						{
						case MemberName.ExtendedProperties:
							PersistenceHelper.WriteProperyCollection(ref writer, this.m_extendedProperties, new Action<IDictionary>(DsvTable.CleanProperties));
							continue;
						case MemberName.Columns:
							writer.Write(this.m_columns);
							continue;
						case MemberName.Constraints:
							writer.Write(this.m_constraints);
							continue;
						case MemberName.PrimaryKey:
							PersistenceHelper.WriteDsvItemReferences<DsvColumn>(ref writer, this.m_primaryKey);
							continue;
						}
						throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
					}
					writer.Write(this.m_name);
				}
			}

			// Token: 0x06000E60 RID: 3680 RVA: 0x0002E63C File Offset: 0x0002C83C
			void IPersistable.Deserialize(IntermediateFormatReader reader)
			{
				reader.RegisterDeclaration(DsvTable.DsvTableInfoBinary.Declaration);
				while (reader.NextMember())
				{
					MemberName memberName = reader.CurrentMember.MemberName;
					if (memberName != MemberName.Name)
					{
						switch (memberName)
						{
						case MemberName.ExtendedProperties:
							this.m_extendedProperties = PersistenceHelper.ReadPropertyCollection(ref reader, (string name) => string.CompareOrdinal(name, "DbTableName") == 0 || string.CompareOrdinal(name, "DbSchemaName") == 0 || string.CompareOrdinal(name, "TableType") == 0 || string.CompareOrdinal(name, "QueryDefinition") == 0 || string.CompareOrdinal(name, "DataSourceID") == 0 || string.CompareOrdinal(name, "DataSize") == 0 || DsvItem.AllowExtendedPropertyForBinaryDeserialization(name));
							continue;
						case MemberName.Columns:
							this.m_columns = (DsvColumnCollection)reader.ReadRIFObject();
							continue;
						case MemberName.Constraints:
							this.m_constraints = (DsvConstraintCollection)reader.ReadRIFObject();
							continue;
						case MemberName.PrimaryKey:
							reader.ReadListOfReferencesNoResolution(this);
							continue;
						}
						throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
					}
					this.m_name = reader.ReadString();
				}
			}

			// Token: 0x06000E61 RID: 3681 RVA: 0x0002E738 File Offset: 0x0002C938
			void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
			{
				List<DsvColumn> list = new List<DsvColumn>();
				List<MemberReference> list2;
				if (memberReferencesCollection.TryGetValue(DsvTable.DsvTableInfoBinary.Declaration.ObjectType, out list2))
				{
					foreach (MemberReference memberReference in list2)
					{
						if (memberReference.MemberName != MemberName.PrimaryKey)
						{
							throw new InternalModelingException("Unexpected member: " + memberReference.MemberName.ToString());
						}
						list.Add(PersistenceHelper.ResolveDsvItemReference<DsvColumn>(referenceableItems[memberReference.RefID]));
					}
					this.m_primaryKey = new ReadOnlyCollection<DsvColumn>(ArrayUtil.ToArray<DsvColumn>(list));
				}
			}

			// Token: 0x06000E62 RID: 3682 RVA: 0x0002E7F8 File Offset: 0x0002C9F8
			ObjectType IPersistable.GetObjectType()
			{
				return ObjectType.DsvTable;
			}

			// Token: 0x17000343 RID: 835
			// (get) Token: 0x06000E63 RID: 3683 RVA: 0x0002E7FC File Offset: 0x0002C9FC
			private static Declaration Declaration
			{
				get
				{
					return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref DsvTable.DsvTableInfoBinary.__declaration, DsvTable.DsvTableInfoBinary.__declarationLock, () => new Declaration(ObjectType.DsvTable, ObjectType.RefHelper, new List<MemberInfo>
					{
						new MemberInfo(MemberName.Name, Token.String),
						new MemberInfo(MemberName.Columns, ObjectType.DsvColumnCollection),
						new MemberInfo(MemberName.Constraints, ObjectType.DsvConstraintCollection),
						new MemberInfo(MemberName.PrimaryKey, ObjectType.RIFObjectList, Token.Reference, ObjectType.DsvColumn),
						new MemberInfo(MemberName.ExtendedProperties, ObjectType.StringObjectHashtable, Token.String)
					}));
				}
			}

			// Token: 0x040005FF RID: 1535
			private string m_name;

			// Token: 0x04000600 RID: 1536
			private DsvColumnCollection m_columns;

			// Token: 0x04000601 RID: 1537
			private DsvConstraintCollection m_constraints;

			// Token: 0x04000602 RID: 1538
			private ReadOnlyCollection<DsvColumn> m_primaryKey;

			// Token: 0x04000603 RID: 1539
			private IDictionary m_extendedProperties;

			// Token: 0x04000604 RID: 1540
			private static Declaration __declaration;

			// Token: 0x04000605 RID: 1541
			private static readonly object __declarationLock = new object();
		}
	}
}
