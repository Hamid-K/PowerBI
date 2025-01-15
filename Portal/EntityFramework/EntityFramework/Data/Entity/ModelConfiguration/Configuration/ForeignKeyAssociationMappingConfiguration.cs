using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001ED RID: 493
	public sealed class ForeignKeyAssociationMappingConfiguration : AssociationMappingConfiguration
	{
		// Token: 0x060019E4 RID: 6628 RVA: 0x00045F21 File Offset: 0x00044121
		internal ForeignKeyAssociationMappingConfiguration()
		{
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x00045F40 File Offset: 0x00044140
		private ForeignKeyAssociationMappingConfiguration(ForeignKeyAssociationMappingConfiguration source)
		{
			this._keyColumnNames.AddRange(source._keyColumnNames);
			this._tableName = source._tableName;
			foreach (KeyValuePair<Tuple<string, string>, object> keyValuePair in source._annotations)
			{
				this._annotations.Add(keyValuePair);
			}
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x00045FCC File Offset: 0x000441CC
		internal override AssociationMappingConfiguration Clone()
		{
			return new ForeignKeyAssociationMappingConfiguration(this);
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x00045FD4 File Offset: 0x000441D4
		public ForeignKeyAssociationMappingConfiguration MapKey(params string[] keyColumnNames)
		{
			Check.NotNull<string[]>(keyColumnNames, "keyColumnNames");
			this._keyColumnNames.Clear();
			this._keyColumnNames.AddRange(keyColumnNames);
			return this;
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x00045FFA File Offset: 0x000441FA
		public ForeignKeyAssociationMappingConfiguration HasColumnAnnotation(string keyColumnName, string annotationName, object value)
		{
			Check.NotEmpty(keyColumnName, "keyColumnName");
			Check.NotEmpty(annotationName, "annotationName");
			this._annotations[Tuple.Create<string, string>(keyColumnName, annotationName)] = value;
			return this;
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x00046028 File Offset: 0x00044228
		public ForeignKeyAssociationMappingConfiguration ToTable(string tableName)
		{
			Check.NotEmpty(tableName, "tableName");
			return this.ToTable(tableName, null);
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x0004603E File Offset: 0x0004423E
		public ForeignKeyAssociationMappingConfiguration ToTable(string tableName, string schemaName)
		{
			Check.NotEmpty(tableName, "tableName");
			this._tableName = new DatabaseName(tableName, schemaName);
			return this;
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x0004605C File Offset: 0x0004425C
		internal override void Configure(AssociationSetMapping associationSetMapping, EdmModel database, PropertyInfo navigationProperty)
		{
			List<ScalarPropertyMapping> propertyMappings = associationSetMapping.SourceEndMapping.PropertyMappings.ToList<ScalarPropertyMapping>();
			if (this._tableName != null)
			{
				ForeignKeyAssociationMappingConfiguration.<>c__DisplayClass10_1 CS$<>8__locals2 = new ForeignKeyAssociationMappingConfiguration.<>c__DisplayClass10_1();
				ForeignKeyAssociationMappingConfiguration.<>c__DisplayClass10_1 CS$<>8__locals3 = CS$<>8__locals2;
				EntityType entityType;
				if ((entityType = (from t in database.EntityTypes
					let n = t.GetTableName()
					where n != null && n.Equals(this._tableName)
					select t).SingleOrDefault<EntityType>()) == null)
				{
					entityType = (from es in database.GetEntitySets()
						where string.Equals(es.Table, this._tableName.Name, StringComparison.Ordinal)
						select es.ElementType).SingleOrDefault<EntityType>();
				}
				CS$<>8__locals3.targetTable = entityType;
				if (CS$<>8__locals2.targetTable == null)
				{
					throw Error.TableNotFound(this._tableName);
				}
				CS$<>8__locals2.sourceTable = associationSetMapping.Table;
				if (CS$<>8__locals2.sourceTable != CS$<>8__locals2.targetTable)
				{
					ForeignKeyBuilder foreignKeyBuilder = CS$<>8__locals2.sourceTable.ForeignKeyBuilders.Single((ForeignKeyBuilder fk) => fk.DependentColumns.SequenceEqual(propertyMappings.Select((ScalarPropertyMapping pm) => pm.Column)));
					CS$<>8__locals2.sourceTable.RemoveForeignKey(foreignKeyBuilder);
					CS$<>8__locals2.targetTable.AddForeignKey(foreignKeyBuilder);
					foreignKeyBuilder.DependentColumns.Each(delegate(EdmProperty c)
					{
						bool isPrimaryKeyColumn = c.IsPrimaryKeyColumn;
						CS$<>8__locals2.sourceTable.RemoveMember(c);
						CS$<>8__locals2.targetTable.AddMember(c);
						if (isPrimaryKeyColumn)
						{
							CS$<>8__locals2.targetTable.AddKeyMember(c);
						}
					});
					associationSetMapping.StoreEntitySet = database.GetEntitySet(CS$<>8__locals2.targetTable);
				}
			}
			if (this._keyColumnNames.Count > 0 && this._keyColumnNames.Count != propertyMappings.Count<ScalarPropertyMapping>())
			{
				throw Error.IncorrectColumnCount(string.Join(", ", this._keyColumnNames));
			}
			this._keyColumnNames.Each(delegate(string n, int i)
			{
				propertyMappings[i].Column.Name = n;
			});
			foreach (KeyValuePair<Tuple<string, string>, object> keyValuePair in this._annotations)
			{
				int num = this._keyColumnNames.IndexOf(keyValuePair.Key.Item1);
				if (num == -1)
				{
					throw new InvalidOperationException(Strings.BadKeyNameForAnnotation(keyValuePair.Key.Item1, keyValuePair.Key.Item2));
				}
				propertyMappings[num].Column.AddAnnotation("http://schemas.microsoft.com/ado/2013/11/edm/customannotation:" + keyValuePair.Key.Item2, keyValuePair.Value);
			}
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x000462DC File Offset: 0x000444DC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x000462E4 File Offset: 0x000444E4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Equals(ForeignKeyAssociationMappingConfiguration other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			if (object.Equals(other._tableName, this._tableName) && other._keyColumnNames.SequenceEqual(this._keyColumnNames))
			{
				return other._annotations.OrderBy((KeyValuePair<Tuple<string, string>, object> a) => a.Key).SequenceEqual(this._annotations.OrderBy((KeyValuePair<Tuple<string, string>, object> a) => a.Key));
			}
			return false;
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x0004637D File Offset: 0x0004457D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != typeof(ForeignKeyAssociationMappingConfiguration)) && this.Equals((ForeignKeyAssociationMappingConfiguration)obj)));
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x000463B0 File Offset: 0x000445B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			int num = ((this._tableName != null) ? this._tableName.GetHashCode() : 0) * 397;
			num = this._keyColumnNames.Aggregate(num, (int h, string v) => (h * 397) ^ v.GetHashCode());
			return this._annotations.OrderBy((KeyValuePair<Tuple<string, string>, object> a) => a.Key).Aggregate(num, (int h, KeyValuePair<Tuple<string, string>, object> v) => (h * 397) ^ v.GetHashCode());
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x00046455 File Offset: 0x00044655
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000A87 RID: 2695
		private readonly List<string> _keyColumnNames = new List<string>();

		// Token: 0x04000A88 RID: 2696
		private readonly IDictionary<Tuple<string, string>, object> _annotations = new Dictionary<Tuple<string, string>, object>();

		// Token: 0x04000A89 RID: 2697
		private DatabaseName _tableName;
	}
}
