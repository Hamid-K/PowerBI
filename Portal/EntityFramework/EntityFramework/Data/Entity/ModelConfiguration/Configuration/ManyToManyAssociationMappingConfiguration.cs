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
	// Token: 0x020001EF RID: 495
	public sealed class ManyToManyAssociationMappingConfiguration : AssociationMappingConfiguration
	{
		// Token: 0x060019F7 RID: 6647 RVA: 0x000464CC File Offset: 0x000446CC
		internal ManyToManyAssociationMappingConfiguration()
		{
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x000464F8 File Offset: 0x000446F8
		private ManyToManyAssociationMappingConfiguration(ManyToManyAssociationMappingConfiguration source)
		{
			this._leftKeyColumnNames.AddRange(source._leftKeyColumnNames);
			this._rightKeyColumnNames.AddRange(source._rightKeyColumnNames);
			this._tableName = source._tableName;
			foreach (KeyValuePair<string, object> keyValuePair in source._annotations)
			{
				this._annotations.Add(keyValuePair);
			}
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x000465A0 File Offset: 0x000447A0
		internal override AssociationMappingConfiguration Clone()
		{
			return new ManyToManyAssociationMappingConfiguration(this);
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x000465A8 File Offset: 0x000447A8
		public ManyToManyAssociationMappingConfiguration ToTable(string tableName)
		{
			Check.NotEmpty(tableName, "tableName");
			return this.ToTable(tableName, null);
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x000465BE File Offset: 0x000447BE
		public ManyToManyAssociationMappingConfiguration ToTable(string tableName, string schemaName)
		{
			Check.NotEmpty(tableName, "tableName");
			this._tableName = new DatabaseName(tableName, schemaName);
			return this;
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x000465DA File Offset: 0x000447DA
		public ManyToManyAssociationMappingConfiguration HasTableAnnotation(string name, object value)
		{
			Check.NotEmpty(name, "name");
			if (!name.IsValidUndottedName())
			{
				throw new ArgumentException(Strings.BadAnnotationName(name));
			}
			this._annotations[name] = value;
			return this;
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x0004660A File Offset: 0x0004480A
		public ManyToManyAssociationMappingConfiguration MapLeftKey(params string[] keyColumnNames)
		{
			Check.NotNull<string[]>(keyColumnNames, "keyColumnNames");
			this._leftKeyColumnNames.Clear();
			this._leftKeyColumnNames.AddRange(keyColumnNames);
			return this;
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x00046630 File Offset: 0x00044830
		public ManyToManyAssociationMappingConfiguration MapRightKey(params string[] keyColumnNames)
		{
			Check.NotNull<string[]>(keyColumnNames, "keyColumnNames");
			this._rightKeyColumnNames.Clear();
			this._rightKeyColumnNames.AddRange(keyColumnNames);
			return this;
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x00046658 File Offset: 0x00044858
		internal override void Configure(AssociationSetMapping associationSetMapping, EdmModel database, PropertyInfo navigationProperty)
		{
			EntityType table = associationSetMapping.Table;
			if (this._tableName != null)
			{
				table.SetTableName(this._tableName);
				table.SetConfiguration(this);
			}
			bool flag = navigationProperty.IsSameAs(associationSetMapping.SourceEndMapping.AssociationEnd.GetClrPropertyInfo());
			ManyToManyAssociationMappingConfiguration.ConfigureColumnNames(flag ? this._leftKeyColumnNames : this._rightKeyColumnNames, associationSetMapping.SourceEndMapping.PropertyMappings.ToList<ScalarPropertyMapping>());
			ManyToManyAssociationMappingConfiguration.ConfigureColumnNames(flag ? this._rightKeyColumnNames : this._leftKeyColumnNames, associationSetMapping.TargetEndMapping.PropertyMappings.ToList<ScalarPropertyMapping>());
			foreach (KeyValuePair<string, object> keyValuePair in this._annotations)
			{
				table.AddAnnotation("http://schemas.microsoft.com/ado/2013/11/edm/customannotation:" + keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x00046740 File Offset: 0x00044940
		private static void ConfigureColumnNames(ICollection<string> keyColumnNames, IList<ScalarPropertyMapping> propertyMappings)
		{
			if (keyColumnNames.Count > 0 && keyColumnNames.Count != propertyMappings.Count)
			{
				throw Error.IncorrectColumnCount(string.Join(", ", keyColumnNames));
			}
			keyColumnNames.Each(delegate(string n, int i)
			{
				propertyMappings[i].Column.Name = n;
			});
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x00046799 File Offset: 0x00044999
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x000467A4 File Offset: 0x000449A4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Equals(ManyToManyAssociationMappingConfiguration other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			if (!object.Equals(other._tableName, this._tableName))
			{
				return false;
			}
			if (object.Equals(other._tableName, this._tableName) && ((this._leftKeyColumnNames.SequenceEqual(other._leftKeyColumnNames) && this._rightKeyColumnNames.SequenceEqual(other._rightKeyColumnNames)) || (this._leftKeyColumnNames.SequenceEqual(other._rightKeyColumnNames) && this._rightKeyColumnNames.SequenceEqual(other._leftKeyColumnNames))))
			{
				return this._annotations.OrderBy((KeyValuePair<string, object> a) => a.Key).SequenceEqual(other._annotations.OrderBy((KeyValuePair<string, object> a) => a.Key));
			}
			return false;
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x0004688E File Offset: 0x00044A8E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != typeof(ManyToManyAssociationMappingConfiguration)) && this.Equals((ManyToManyAssociationMappingConfiguration)obj)));
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x000468C0 File Offset: 0x00044AC0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			int num = ((this._tableName != null) ? this._tableName.GetHashCode() : 0) * 397;
			num = this._leftKeyColumnNames.Aggregate(num, (int h, string v) => (h * 397) ^ v.GetHashCode());
			num = this._rightKeyColumnNames.Aggregate(num, (int h, string v) => (h * 397) ^ v.GetHashCode());
			return this._annotations.OrderBy((KeyValuePair<string, object> a) => a.Key).Aggregate(num, (int h, KeyValuePair<string, object> v) => (h * 397) ^ v.GetHashCode());
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x00046991 File Offset: 0x00044B91
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000A8A RID: 2698
		private readonly List<string> _leftKeyColumnNames = new List<string>();

		// Token: 0x04000A8B RID: 2699
		private readonly List<string> _rightKeyColumnNames = new List<string>();

		// Token: 0x04000A8C RID: 2700
		private DatabaseName _tableName;

		// Token: 0x04000A8D RID: 2701
		private readonly IDictionary<string, object> _annotations = new Dictionary<string, object>();
	}
}
