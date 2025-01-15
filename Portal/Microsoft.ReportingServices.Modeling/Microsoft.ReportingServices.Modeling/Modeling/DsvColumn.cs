using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x0200006F RID: 111
	public sealed class DsvColumn : DsvItem, IHasFriendlyName
	{
		// Token: 0x0600049D RID: 1181 RVA: 0x0000F228 File Offset: 0x0000D428
		internal static DsvColumn FromDataColumn(DataColumn column)
		{
			if (column == null)
			{
				return null;
			}
			return DsvItem.GetDsvItem<DsvColumn>(column.ExtendedProperties, () => new DsvColumn(new DsvColumn.DsvColumnInfoDS(column)));
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0000F268 File Offset: 0x0000D468
		private DsvColumn(DsvColumn.IDsvColumnInfo columnInfo)
		{
			this.m_columnInfo = columnInfo;
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x0000F277 File Offset: 0x0000D477
		public override string Name
		{
			get
			{
				return this.m_columnInfo.Name;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0000F284 File Offset: 0x0000D484
		public override bool IsReadOnly
		{
			get
			{
				return this.m_columnInfo.IsReadOnly;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x0000F291 File Offset: 0x0000D491
		public Type DataType
		{
			get
			{
				return this.m_columnInfo.DataType;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0000F29E File Offset: 0x0000D49E
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x0000F2B4 File Offset: 0x0000D4B4
		public string DbDataType
		{
			get
			{
				return base.GetString("DbDataType") ?? string.Empty;
			}
			set
			{
				base.SetString("DbDataType", value);
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0000F2C2 File Offset: 0x0000D4C2
		public int MaxLength
		{
			get
			{
				return this.m_columnInfo.MaxLength;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x0000F2CF File Offset: 0x0000D4CF
		public bool Nullable
		{
			get
			{
				return this.m_columnInfo.AllowDBNull || base.GetBoolean("NullableKey");
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x0000F2EB File Offset: 0x0000D4EB
		public int Ordinal
		{
			get
			{
				return this.m_columnInfo.Ordinal;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0000F2F8 File Offset: 0x0000D4F8
		public bool Unique
		{
			get
			{
				return this.m_columnInfo.Unique;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0000F305 File Offset: 0x0000D505
		public bool AutoIncrement
		{
			get
			{
				return this.m_columnInfo.AutoIncrement;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x0000F312 File Offset: 0x0000D512
		public DsvTable Table
		{
			get
			{
				return this.m_columnInfo.Table;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x0000F31F File Offset: 0x0000D51F
		public string Description
		{
			get
			{
				return base.GetString("Description") ?? string.Empty;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x0000F335 File Offset: 0x0000D535
		public string DbColumnName
		{
			get
			{
				return base.GetString("DbColumnName") ?? string.Empty;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x0000F34B File Offset: 0x0000D54B
		public string FriendlyName
		{
			get
			{
				return base.GetString("FriendlyName") ?? string.Empty;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x0000F361 File Offset: 0x0000D561
		public bool IsLogical
		{
			get
			{
				return base.GetBoolean("IsLogical");
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x0000F36E File Offset: 0x0000D56E
		public bool IsPrimaryKey
		{
			get
			{
				return this.m_columnInfo.IsPrimaryKey;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x0000F37B File Offset: 0x0000D57B
		public bool IsForeignKey
		{
			get
			{
				return this.m_columnInfo.IsForeignKey;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0000F388 File Offset: 0x0000D588
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x0000F395 File Offset: 0x0000D595
		public bool? IsBlob
		{
			get
			{
				return base.GetNullableBoolean("IsBlob");
			}
			set
			{
				base.SetNullableBoolean("IsBlob", value);
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0000F3A3 File Offset: 0x0000D5A3
		public string ComputedColumnExpression
		{
			get
			{
				return base.GetString("ComputedColumnExpression") ?? string.Empty;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x0000F3B9 File Offset: 0x0000D5B9
		public DataType? ModelingDataType
		{
			get
			{
				return DataTypeMapper.TranslateClrType(this.DataType);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x0000F3C6 File Offset: 0x0000D5C6
		// (set) Token: 0x060004B5 RID: 1205 RVA: 0x0000F3D3 File Offset: 0x0000D5D3
		public long? UniqueValueCount
		{
			get
			{
				return base.GetNullableInt64("stats_UniqueValueCount");
			}
			set
			{
				base.SetNullableInt64("stats_UniqueValueCount", value);
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x0000F3E1 File Offset: 0x0000D5E1
		// (set) Token: 0x060004B7 RID: 1207 RVA: 0x0000F3EE File Offset: 0x0000D5EE
		public int? UniqueValuePercent
		{
			get
			{
				return base.GetNullableInt32("stats_UniqueValuePercent");
			}
			set
			{
				base.SetNullableInt32("stats_UniqueValuePercent", value);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x0000F3FC File Offset: 0x0000D5FC
		public DsvColumn.ColumnGroupCollection ColumnGroups
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<DsvColumn.ColumnGroupCollection>(ref this.__columnGroups, DsvColumn.__columnGroupsLock, () => new DsvColumn.ColumnGroupCollection(this));
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x0000F41A File Offset: 0x0000D61A
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x0000F427 File Offset: 0x0000D627
		public float? AvgScale
		{
			get
			{
				return base.GetNullableSingle("stats_AvgScale");
			}
			set
			{
				base.SetNullableSingle("stats_AvgScale", value);
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x0000F435 File Offset: 0x0000D635
		// (set) Token: 0x060004BC RID: 1212 RVA: 0x0000F442 File Offset: 0x0000D642
		public float? StDevScale
		{
			get
			{
				return base.GetNullableSingle("stats_StDevScale");
			}
			set
			{
				base.SetNullableSingle("stats_StDevScale", value);
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x0000F450 File Offset: 0x0000D650
		// (set) Token: 0x060004BE RID: 1214 RVA: 0x0000F45D File Offset: 0x0000D65D
		public int? MaxScale
		{
			get
			{
				return base.GetNullableInt32("stats_MaxScale");
			}
			set
			{
				base.SetNullableInt32("stats_MaxScale", value);
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0000F46B File Offset: 0x0000D66B
		// (set) Token: 0x060004C0 RID: 1216 RVA: 0x0000F478 File Offset: 0x0000D678
		public float? AvgWidth
		{
			get
			{
				return base.GetNullableSingle("stats_AvgWidth");
			}
			set
			{
				base.SetNullableSingle("stats_AvgWidth", value);
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x0000F486 File Offset: 0x0000D686
		// (set) Token: 0x060004C2 RID: 1218 RVA: 0x0000F493 File Offset: 0x0000D693
		public float? StDevWidth
		{
			get
			{
				return base.GetNullableSingle("stats_StDevWidth");
			}
			set
			{
				base.SetNullableSingle("stats_StDevWidth", value);
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x0000F4A1 File Offset: 0x0000D6A1
		// (set) Token: 0x060004C4 RID: 1220 RVA: 0x0000F4AE File Offset: 0x0000D6AE
		public int? MaxWidth
		{
			get
			{
				return base.GetNullableInt32("stats_MaxWidth");
			}
			set
			{
				base.SetNullableInt32("stats_MaxWidth", value);
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0000F4BC File Offset: 0x0000D6BC
		protected override IDictionary Properties
		{
			get
			{
				return this.m_columnInfo.ExtendedProperties;
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0000F4CC File Offset: 0x0000D6CC
		internal new static void CleanProperties(IDictionary properties)
		{
			DsvColumn.ColumnGroupCollection columnGroupCollection = properties["stats_ColumnGroups"] as DsvColumn.ColumnGroupCollection;
			if (columnGroupCollection != null && columnGroupCollection.Count == 0)
			{
				properties.Remove("stats_ColumnGroups");
			}
			DsvItem.CleanProperties(properties);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0000F506 File Offset: 0x0000D706
		internal static DsvColumn FromBinary()
		{
			return new DsvColumn(new DsvColumn.DsvColumnInfoBinary());
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x0000F512 File Offset: 0x0000D712
		internal override IPersistable DataStorage
		{
			get
			{
				return this.m_columnInfo;
			}
		}

		// Token: 0x04000281 RID: 641
		public const string DbDataTypeExtProperty = "DbDataType";

		// Token: 0x04000282 RID: 642
		public const string DbColumnNameExtProperty = "DbColumnName";

		// Token: 0x04000283 RID: 643
		private const string ComputedColumnExpressionExtProperty = "ComputedColumnExpression";

		// Token: 0x04000284 RID: 644
		private const string NullableKeyExtProperty = "NullableKey";

		// Token: 0x04000285 RID: 645
		private const string IsBlobExtProperty = "IsBlob";

		// Token: 0x04000286 RID: 646
		public const string DataSizeExtProperty = "DataSize";

		// Token: 0x04000287 RID: 647
		private const string UniqueValueCountExtProperty = "stats_UniqueValueCount";

		// Token: 0x04000288 RID: 648
		private const string UniqueValuePercentExtProperty = "stats_UniqueValuePercent";

		// Token: 0x04000289 RID: 649
		private const string ColumnGroupsExtProperty = "stats_ColumnGroups";

		// Token: 0x0400028A RID: 650
		private const string AvgScaleExtProperty = "stats_AvgScale";

		// Token: 0x0400028B RID: 651
		private const string StDevScaleExtProperty = "stats_StDevScale";

		// Token: 0x0400028C RID: 652
		private const string MaxScaleExtProperty = "stats_MaxScale";

		// Token: 0x0400028D RID: 653
		private const string AvgWidthExtProperty = "stats_AvgWidth";

		// Token: 0x0400028E RID: 654
		private const string StDevWidthExtProperty = "stats_StDevWidth";

		// Token: 0x0400028F RID: 655
		private const string MaxWidthExtProperty = "stats_MaxWidth";

		// Token: 0x04000290 RID: 656
		private readonly DsvColumn.IDsvColumnInfo m_columnInfo;

		// Token: 0x04000291 RID: 657
		private DsvColumn.ColumnGroupCollection __columnGroups;

		// Token: 0x04000292 RID: 658
		private static readonly object __columnGroupsLock = new object();

		// Token: 0x02000144 RID: 324
		private interface IDsvColumnInfo : IPersistable
		{
			// Token: 0x17000350 RID: 848
			// (get) Token: 0x06000E85 RID: 3717
			string Name { get; }

			// Token: 0x17000351 RID: 849
			// (get) Token: 0x06000E86 RID: 3718
			bool IsReadOnly { get; }

			// Token: 0x17000352 RID: 850
			// (get) Token: 0x06000E87 RID: 3719
			Type DataType { get; }

			// Token: 0x17000353 RID: 851
			// (get) Token: 0x06000E88 RID: 3720
			int MaxLength { get; }

			// Token: 0x17000354 RID: 852
			// (get) Token: 0x06000E89 RID: 3721
			bool AllowDBNull { get; }

			// Token: 0x17000355 RID: 853
			// (get) Token: 0x06000E8A RID: 3722
			int Ordinal { get; }

			// Token: 0x17000356 RID: 854
			// (get) Token: 0x06000E8B RID: 3723
			bool Unique { get; }

			// Token: 0x17000357 RID: 855
			// (get) Token: 0x06000E8C RID: 3724
			bool AutoIncrement { get; }

			// Token: 0x17000358 RID: 856
			// (get) Token: 0x06000E8D RID: 3725
			DsvTable Table { get; }

			// Token: 0x17000359 RID: 857
			// (get) Token: 0x06000E8E RID: 3726
			bool IsPrimaryKey { get; }

			// Token: 0x1700035A RID: 858
			// (get) Token: 0x06000E8F RID: 3727
			bool IsForeignKey { get; }

			// Token: 0x1700035B RID: 859
			// (get) Token: 0x06000E90 RID: 3728
			IDictionary ExtendedProperties { get; }
		}

		// Token: 0x02000145 RID: 325
		private sealed class DsvColumnInfoDS : DsvColumn.IDsvColumnInfo, IPersistable
		{
			// Token: 0x06000E91 RID: 3729 RVA: 0x0002EBE8 File Offset: 0x0002CDE8
			internal DsvColumnInfoDS(DataColumn column)
			{
				if (column == null)
				{
					throw new InternalModelingException("column is null");
				}
				this.m_column = column;
				DsvColumn.DsvColumnInfoDS.Flags flags = DsvColumn.DsvColumnInfoDS.Flags.None;
				if (ArrayUtil.Contains<DataColumn>(column.Table.PrimaryKey, column))
				{
					flags |= DsvColumn.DsvColumnInfoDS.Flags.PrimaryKey;
				}
				foreach (object obj in column.Table.ParentRelations)
				{
					DataRelation dataRelation = (DataRelation)obj;
					flags &= ~DsvColumn.DsvColumnInfoDS.Flags.ForeignKey;
					if (ArrayUtil.Contains<DataColumn>(dataRelation.ChildColumns, column))
					{
						flags |= DsvColumn.DsvColumnInfoDS.Flags.ForeignKey;
						break;
					}
				}
				this.m_flags = flags;
			}

			// Token: 0x1700035C RID: 860
			// (get) Token: 0x06000E92 RID: 3730 RVA: 0x0002EC94 File Offset: 0x0002CE94
			string DsvColumn.IDsvColumnInfo.Name
			{
				get
				{
					return this.m_column.ColumnName;
				}
			}

			// Token: 0x1700035D RID: 861
			// (get) Token: 0x06000E93 RID: 3731 RVA: 0x0002ECA1 File Offset: 0x0002CEA1
			bool DsvColumn.IDsvColumnInfo.IsReadOnly
			{
				get
				{
					return DsvItem.IsDataSetReadonly(this.m_column.Table.DataSet);
				}
			}

			// Token: 0x1700035E RID: 862
			// (get) Token: 0x06000E94 RID: 3732 RVA: 0x0002ECB8 File Offset: 0x0002CEB8
			Type DsvColumn.IDsvColumnInfo.DataType
			{
				get
				{
					return this.m_column.DataType;
				}
			}

			// Token: 0x1700035F RID: 863
			// (get) Token: 0x06000E95 RID: 3733 RVA: 0x0002ECC5 File Offset: 0x0002CEC5
			int DsvColumn.IDsvColumnInfo.MaxLength
			{
				get
				{
					return this.m_column.MaxLength;
				}
			}

			// Token: 0x17000360 RID: 864
			// (get) Token: 0x06000E96 RID: 3734 RVA: 0x0002ECD2 File Offset: 0x0002CED2
			bool DsvColumn.IDsvColumnInfo.AllowDBNull
			{
				get
				{
					return this.m_column.AllowDBNull;
				}
			}

			// Token: 0x17000361 RID: 865
			// (get) Token: 0x06000E97 RID: 3735 RVA: 0x0002ECDF File Offset: 0x0002CEDF
			int DsvColumn.IDsvColumnInfo.Ordinal
			{
				get
				{
					return this.m_column.Ordinal;
				}
			}

			// Token: 0x17000362 RID: 866
			// (get) Token: 0x06000E98 RID: 3736 RVA: 0x0002ECEC File Offset: 0x0002CEEC
			bool DsvColumn.IDsvColumnInfo.Unique
			{
				get
				{
					return this.m_column.Unique;
				}
			}

			// Token: 0x17000363 RID: 867
			// (get) Token: 0x06000E99 RID: 3737 RVA: 0x0002ECF9 File Offset: 0x0002CEF9
			bool DsvColumn.IDsvColumnInfo.AutoIncrement
			{
				get
				{
					return this.m_column.AutoIncrement;
				}
			}

			// Token: 0x17000364 RID: 868
			// (get) Token: 0x06000E9A RID: 3738 RVA: 0x0002ED06 File Offset: 0x0002CF06
			DsvTable DsvColumn.IDsvColumnInfo.Table
			{
				get
				{
					return DsvTable.FromDataTable(this.m_column.Table);
				}
			}

			// Token: 0x17000365 RID: 869
			// (get) Token: 0x06000E9B RID: 3739 RVA: 0x0002ED18 File Offset: 0x0002CF18
			bool DsvColumn.IDsvColumnInfo.IsPrimaryKey
			{
				get
				{
					return (this.m_flags & DsvColumn.DsvColumnInfoDS.Flags.PrimaryKey) == DsvColumn.DsvColumnInfoDS.Flags.PrimaryKey;
				}
			}

			// Token: 0x17000366 RID: 870
			// (get) Token: 0x06000E9C RID: 3740 RVA: 0x0002ED25 File Offset: 0x0002CF25
			bool DsvColumn.IDsvColumnInfo.IsForeignKey
			{
				get
				{
					return (this.m_flags & DsvColumn.DsvColumnInfoDS.Flags.ForeignKey) == DsvColumn.DsvColumnInfoDS.Flags.ForeignKey;
				}
			}

			// Token: 0x17000367 RID: 871
			// (get) Token: 0x06000E9D RID: 3741 RVA: 0x0002ED32 File Offset: 0x0002CF32
			IDictionary DsvColumn.IDsvColumnInfo.ExtendedProperties
			{
				get
				{
					return this.m_column.ExtendedProperties;
				}
			}

			// Token: 0x06000E9E RID: 3742 RVA: 0x0002ED3F File Offset: 0x0002CF3F
			void IPersistable.Serialize(IntermediateFormatWriter writer)
			{
				((IPersistable)new DsvColumn.DsvColumnInfoBinary(this)).Serialize(writer);
			}

			// Token: 0x06000E9F RID: 3743 RVA: 0x0002ED4D File Offset: 0x0002CF4D
			void IPersistable.Deserialize(IntermediateFormatReader reader)
			{
				throw new InternalModelingException("Deserialize is not supported.");
			}

			// Token: 0x06000EA0 RID: 3744 RVA: 0x0002ED59 File Offset: 0x0002CF59
			void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
			{
				throw new InternalModelingException("ResolveReferences is not supported.");
			}

			// Token: 0x06000EA1 RID: 3745 RVA: 0x0002ED65 File Offset: 0x0002CF65
			ObjectType IPersistable.GetObjectType()
			{
				return ObjectType.DsvColumn;
			}

			// Token: 0x04000611 RID: 1553
			private readonly DataColumn m_column;

			// Token: 0x04000612 RID: 1554
			private readonly DsvColumn.DsvColumnInfoDS.Flags m_flags;

			// Token: 0x020001E7 RID: 487
			[Flags]
			private enum Flags : byte
			{
				// Token: 0x04000839 RID: 2105
				None = 0,
				// Token: 0x0400083A RID: 2106
				PrimaryKey = 1,
				// Token: 0x0400083B RID: 2107
				ForeignKey = 2
			}
		}

		// Token: 0x02000146 RID: 326
		public sealed class ColumnGroupCollection : CheckedCollection<string>
		{
			// Token: 0x06000EA2 RID: 3746 RVA: 0x0002ED6C File Offset: 0x0002CF6C
			internal ColumnGroupCollection(DsvColumn owner)
			{
				this.m_owner = owner;
				if (owner.Properties["stats_ColumnGroups"] != null)
				{
					try
					{
						this.LoadXml(owner.Properties["stats_ColumnGroups"].ToString());
					}
					catch (XmlException)
					{
						base.Items.Clear();
					}
				}
				owner.Properties["stats_ColumnGroups"] = this;
			}

			// Token: 0x17000368 RID: 872
			// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x0002EDE4 File Offset: 0x0002CFE4
			public override bool IsReadOnly
			{
				get
				{
					return this.m_owner.IsReadOnly;
				}
			}

			// Token: 0x06000EA4 RID: 3748 RVA: 0x0002EDF1 File Offset: 0x0002CFF1
			public new void Add(string columnGroupName)
			{
				if (!base.Items.Contains(columnGroupName))
				{
					base.Add(columnGroupName);
				}
			}

			// Token: 0x06000EA5 RID: 3749 RVA: 0x0002EE08 File Offset: 0x0002D008
			public override string ToString()
			{
				return XmlFragmentUtil.ToXmlString(new Action<XmlWriter>(this.WriteXml));
			}

			// Token: 0x06000EA6 RID: 3750 RVA: 0x0002EE1C File Offset: 0x0002D01C
			private void LoadXml(string xml)
			{
				using (XmlReader xmlReader = XmlFragmentUtil.ReadXmlString(xml))
				{
					XmlUtil.CheckElement(xmlReader, "ColumnGroups", string.Empty);
					if (xmlReader.ReadToDescendant("Group"))
					{
						do
						{
							base.Items.Add(xmlReader.ReadElementContentAsString());
						}
						while (xmlReader.IsStartElement("Group") || xmlReader.ReadToNextSibling("Group"));
					}
				}
			}

			// Token: 0x06000EA7 RID: 3751 RVA: 0x0002EE94 File Offset: 0x0002D094
			private void WriteXml(XmlWriter xw)
			{
				xw.WriteStartElement("ColumnGroups");
				foreach (string text in this)
				{
					xw.WriteElementString("Group", text);
				}
				xw.WriteEndElement();
			}

			// Token: 0x04000613 RID: 1555
			private const string ColumnGroupsElem = "ColumnGroups";

			// Token: 0x04000614 RID: 1556
			private const string GroupElem = "Group";

			// Token: 0x04000615 RID: 1557
			private readonly DsvColumn m_owner;
		}

		// Token: 0x02000147 RID: 327
		private sealed class DsvColumnInfoBinary : DsvColumn.IDsvColumnInfo, IPersistable
		{
			// Token: 0x06000EA8 RID: 3752 RVA: 0x0002EEF8 File Offset: 0x0002D0F8
			internal DsvColumnInfoBinary()
			{
			}

			// Token: 0x06000EA9 RID: 3753 RVA: 0x0002EF00 File Offset: 0x0002D100
			internal DsvColumnInfoBinary(DsvColumn.IDsvColumnInfo columnInfo)
				: this()
			{
				this.m_name = columnInfo.Name;
				this.m_dataType = columnInfo.DataType;
				this.m_maxLength = columnInfo.MaxLength;
				this.m_ordinal = (short)columnInfo.Ordinal;
				this.m_table = columnInfo.Table;
				this.m_extendedProperties = columnInfo.ExtendedProperties;
				this.m_flags = DsvColumn.DsvColumnInfoBinary.Flags.None;
				if (columnInfo.IsPrimaryKey)
				{
					this.m_flags |= DsvColumn.DsvColumnInfoBinary.Flags.PrimaryKey;
				}
				if (columnInfo.IsForeignKey)
				{
					this.m_flags |= DsvColumn.DsvColumnInfoBinary.Flags.ForeignKey;
				}
				if (columnInfo.AllowDBNull)
				{
					this.m_flags |= DsvColumn.DsvColumnInfoBinary.Flags.AllowDBNull;
				}
				if (columnInfo.Unique)
				{
					this.m_flags |= DsvColumn.DsvColumnInfoBinary.Flags.Unique;
				}
				if (columnInfo.AutoIncrement)
				{
					this.m_flags |= DsvColumn.DsvColumnInfoBinary.Flags.AutoIncrement;
				}
			}

			// Token: 0x17000369 RID: 873
			// (get) Token: 0x06000EAA RID: 3754 RVA: 0x0002EFD2 File Offset: 0x0002D1D2
			string DsvColumn.IDsvColumnInfo.Name
			{
				get
				{
					return this.m_name;
				}
			}

			// Token: 0x1700036A RID: 874
			// (get) Token: 0x06000EAB RID: 3755 RVA: 0x0002EFDA File Offset: 0x0002D1DA
			bool DsvColumn.IDsvColumnInfo.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700036B RID: 875
			// (get) Token: 0x06000EAC RID: 3756 RVA: 0x0002EFDD File Offset: 0x0002D1DD
			Type DsvColumn.IDsvColumnInfo.DataType
			{
				get
				{
					return this.m_dataType;
				}
			}

			// Token: 0x1700036C RID: 876
			// (get) Token: 0x06000EAD RID: 3757 RVA: 0x0002EFE5 File Offset: 0x0002D1E5
			int DsvColumn.IDsvColumnInfo.MaxLength
			{
				get
				{
					return this.m_maxLength;
				}
			}

			// Token: 0x1700036D RID: 877
			// (get) Token: 0x06000EAE RID: 3758 RVA: 0x0002EFED File Offset: 0x0002D1ED
			bool DsvColumn.IDsvColumnInfo.AllowDBNull
			{
				get
				{
					return (this.m_flags & DsvColumn.DsvColumnInfoBinary.Flags.AllowDBNull) == DsvColumn.DsvColumnInfoBinary.Flags.AllowDBNull;
				}
			}

			// Token: 0x1700036E RID: 878
			// (get) Token: 0x06000EAF RID: 3759 RVA: 0x0002EFFA File Offset: 0x0002D1FA
			int DsvColumn.IDsvColumnInfo.Ordinal
			{
				get
				{
					return (int)this.m_ordinal;
				}
			}

			// Token: 0x1700036F RID: 879
			// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x0002F002 File Offset: 0x0002D202
			bool DsvColumn.IDsvColumnInfo.Unique
			{
				get
				{
					return (this.m_flags & DsvColumn.DsvColumnInfoBinary.Flags.Unique) == DsvColumn.DsvColumnInfoBinary.Flags.Unique;
				}
			}

			// Token: 0x17000370 RID: 880
			// (get) Token: 0x06000EB1 RID: 3761 RVA: 0x0002F00F File Offset: 0x0002D20F
			bool DsvColumn.IDsvColumnInfo.AutoIncrement
			{
				get
				{
					return (this.m_flags & DsvColumn.DsvColumnInfoBinary.Flags.AutoIncrement) == DsvColumn.DsvColumnInfoBinary.Flags.AutoIncrement;
				}
			}

			// Token: 0x17000371 RID: 881
			// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x0002F01E File Offset: 0x0002D21E
			DsvTable DsvColumn.IDsvColumnInfo.Table
			{
				get
				{
					return this.m_table;
				}
			}

			// Token: 0x17000372 RID: 882
			// (get) Token: 0x06000EB3 RID: 3763 RVA: 0x0002F026 File Offset: 0x0002D226
			bool DsvColumn.IDsvColumnInfo.IsPrimaryKey
			{
				get
				{
					return (this.m_flags & DsvColumn.DsvColumnInfoBinary.Flags.PrimaryKey) == DsvColumn.DsvColumnInfoBinary.Flags.PrimaryKey;
				}
			}

			// Token: 0x17000373 RID: 883
			// (get) Token: 0x06000EB4 RID: 3764 RVA: 0x0002F033 File Offset: 0x0002D233
			bool DsvColumn.IDsvColumnInfo.IsForeignKey
			{
				get
				{
					return (this.m_flags & DsvColumn.DsvColumnInfoBinary.Flags.ForeignKey) == DsvColumn.DsvColumnInfoBinary.Flags.ForeignKey;
				}
			}

			// Token: 0x17000374 RID: 884
			// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x0002F040 File Offset: 0x0002D240
			IDictionary DsvColumn.IDsvColumnInfo.ExtendedProperties
			{
				get
				{
					return this.m_extendedProperties;
				}
			}

			// Token: 0x06000EB6 RID: 3766 RVA: 0x0002F048 File Offset: 0x0002D248
			void IPersistable.Serialize(IntermediateFormatWriter writer)
			{
				writer.RegisterDeclaration(DsvColumn.DsvColumnInfoBinary.Declaration);
				while (writer.NextMember())
				{
					switch (writer.CurrentMember.MemberName)
					{
					case MemberName.Name:
						writer.Write(this.m_name);
						break;
					case MemberName.DataType:
						writer.Write(this.m_dataType.FullName);
						break;
					case MemberName.MaxLength:
						writer.Write(this.m_maxLength);
						break;
					case MemberName.Ordinal:
						writer.Write(this.m_ordinal);
						break;
					case MemberName.Table:
						PersistenceHelper.WriteDsvItemReference(ref writer, this.m_table);
						break;
					case MemberName.ExtendedProperties:
						PersistenceHelper.WriteProperyCollection(ref writer, this.m_extendedProperties, new Action<IDictionary>(DsvColumn.CleanProperties));
						break;
					case MemberName.Flags:
						writer.Write((byte)this.m_flags);
						break;
					default:
						throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
					}
				}
			}

			// Token: 0x06000EB7 RID: 3767 RVA: 0x0002F154 File Offset: 0x0002D354
			void IPersistable.Deserialize(IntermediateFormatReader reader)
			{
				reader.RegisterDeclaration(DsvColumn.DsvColumnInfoBinary.Declaration);
				while (reader.NextMember())
				{
					switch (reader.CurrentMember.MemberName)
					{
					case MemberName.Name:
						this.m_name = reader.ReadString();
						break;
					case MemberName.DataType:
					{
						string text = reader.ReadString();
						this.m_dataType = Type.GetType(text);
						break;
					}
					case MemberName.MaxLength:
						this.m_maxLength = reader.ReadInt32();
						break;
					case MemberName.Ordinal:
						this.m_ordinal = reader.ReadInt16();
						break;
					case MemberName.Table:
						this.m_table = PersistenceHelper.ReadDsvItemReference<DsvTable>(ref reader, this);
						break;
					case MemberName.ExtendedProperties:
						this.m_extendedProperties = PersistenceHelper.ReadPropertyCollection(ref reader, (string name) => string.CompareOrdinal(name, "DbColumnName") == 0 || string.CompareOrdinal(name, "DbDataType") == 0 || string.CompareOrdinal(name, "ComputedColumnExpression") == 0 || string.CompareOrdinal(name, "NullableKey") == 0 || string.CompareOrdinal(name, "IsBlob") == 0 || string.CompareOrdinal(name, "DataSize") == 0 || DsvItem.AllowExtendedPropertyForBinaryDeserialization(name));
						break;
					case MemberName.Flags:
						this.m_flags = (DsvColumn.DsvColumnInfoBinary.Flags)reader.ReadByte();
						break;
					default:
						throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
					}
				}
			}

			// Token: 0x06000EB8 RID: 3768 RVA: 0x0002F278 File Offset: 0x0002D478
			void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
			{
				List<MemberReference> list;
				if (memberReferencesCollection.TryGetValue(DsvColumn.DsvColumnInfoBinary.Declaration.ObjectType, out list))
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

			// Token: 0x06000EB9 RID: 3769 RVA: 0x0002F31C File Offset: 0x0002D51C
			ObjectType IPersistable.GetObjectType()
			{
				return ObjectType.DsvColumn;
			}

			// Token: 0x17000375 RID: 885
			// (get) Token: 0x06000EBA RID: 3770 RVA: 0x0002F320 File Offset: 0x0002D520
			private static Declaration Declaration
			{
				get
				{
					return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref DsvColumn.DsvColumnInfoBinary.__declaration, DsvColumn.DsvColumnInfoBinary.__declarationLock, () => new Declaration(ObjectType.DsvColumn, ObjectType.RefHelper, new List<MemberInfo>
					{
						new MemberInfo(MemberName.Name, Token.String),
						new MemberInfo(MemberName.DataType, Token.String),
						new MemberInfo(MemberName.MaxLength, Token.Int32),
						new MemberInfo(MemberName.Ordinal, Token.Int16),
						new MemberInfo(MemberName.Table, ObjectType.DsvTable, Token.Reference),
						new MemberInfo(MemberName.ExtendedProperties, ObjectType.StringObjectHashtable, Token.String),
						new MemberInfo(MemberName.Flags, Token.Byte)
					}));
				}
			}

			// Token: 0x04000616 RID: 1558
			private string m_name;

			// Token: 0x04000617 RID: 1559
			private Type m_dataType;

			// Token: 0x04000618 RID: 1560
			private int m_maxLength;

			// Token: 0x04000619 RID: 1561
			private short m_ordinal;

			// Token: 0x0400061A RID: 1562
			private DsvTable m_table;

			// Token: 0x0400061B RID: 1563
			private IDictionary m_extendedProperties;

			// Token: 0x0400061C RID: 1564
			private DsvColumn.DsvColumnInfoBinary.Flags m_flags;

			// Token: 0x0400061D RID: 1565
			private static Declaration __declaration;

			// Token: 0x0400061E RID: 1566
			private static readonly object __declarationLock = new object();

			// Token: 0x020001E8 RID: 488
			[Flags]
			private enum Flags : byte
			{
				// Token: 0x0400083D RID: 2109
				None = 0,
				// Token: 0x0400083E RID: 2110
				PrimaryKey = 1,
				// Token: 0x0400083F RID: 2111
				ForeignKey = 2,
				// Token: 0x04000840 RID: 2112
				AllowDBNull = 4,
				// Token: 0x04000841 RID: 2113
				Unique = 8,
				// Token: 0x04000842 RID: 2114
				AutoIncrement = 16
			}
		}
	}
}
