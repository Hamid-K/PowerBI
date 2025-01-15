using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000069 RID: 105
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class DsvItem : IPersistable
	{
		// Token: 0x06000454 RID: 1108 RVA: 0x0000E996 File Offset: 0x0000CB96
		internal DsvItem()
		{
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000455 RID: 1109
		public abstract string Name { get; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000456 RID: 1110
		public abstract bool IsReadOnly { get; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000457 RID: 1111
		protected abstract IDictionary Properties { get; }

		// Token: 0x06000458 RID: 1112 RVA: 0x0000E9A0 File Offset: 0x0000CBA0
		public string GetPropertyValue(string propertyName)
		{
			PropertyInfo property = base.GetType().GetProperty(propertyName);
			if (property != null)
			{
				return Convert.ToString(property.GetValue(this, null), CultureInfo.InvariantCulture);
			}
			return this.GetString(propertyName);
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0000E9DD File Offset: 0x0000CBDD
		protected string GetString(string propertyName)
		{
			return this.Properties[propertyName] as string;
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000E9F0 File Offset: 0x0000CBF0
		protected void SetString(string propertyName, string value)
		{
			this.CheckWriteable();
			DataSourceView.SetExtendedProperty(this.Properties, propertyName, value);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000EA08 File Offset: 0x0000CC08
		protected bool GetBoolean(string propertyName)
		{
			bool? nullableBoolean = this.GetNullableBoolean(propertyName);
			return nullableBoolean != null && nullableBoolean.Value;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000EA2F File Offset: 0x0000CC2F
		protected void SetBoolean(string propertyName, bool value)
		{
			this.SetNullableBoolean(propertyName, new bool?(value));
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000EA40 File Offset: 0x0000CC40
		protected bool? GetNullableBoolean(string propertyName)
		{
			string @string = this.GetString(propertyName);
			if (@string == null)
			{
				return null;
			}
			return new bool?(Convert.ToBoolean(@string, CultureInfo.InvariantCulture));
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000EA74 File Offset: 0x0000CC74
		protected void SetNullableBoolean(string propertyName, bool? value)
		{
			this.SetString(propertyName, (value != null) ? value.Value.ToString(CultureInfo.InvariantCulture) : null);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0000EAA8 File Offset: 0x0000CCA8
		protected int? GetNullableInt32(string propertyName)
		{
			string @string = this.GetString(propertyName);
			if (@string == null)
			{
				return null;
			}
			return new int?(Convert.ToInt32(@string, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000EADC File Offset: 0x0000CCDC
		protected void SetNullableInt32(string propertyName, int? value)
		{
			this.SetString(propertyName, (value != null) ? value.Value.ToString(CultureInfo.InvariantCulture) : null);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000EB10 File Offset: 0x0000CD10
		protected long? GetNullableInt64(string propertyName)
		{
			string @string = this.GetString(propertyName);
			if (@string == null)
			{
				return null;
			}
			return new long?(Convert.ToInt64(@string, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000EB44 File Offset: 0x0000CD44
		protected void SetNullableInt64(string propertyName, long? value)
		{
			this.SetString(propertyName, (value != null) ? value.Value.ToString(CultureInfo.InvariantCulture) : null);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000EB78 File Offset: 0x0000CD78
		protected float? GetNullableSingle(string propertyName)
		{
			string @string = this.GetString(propertyName);
			if (@string == null)
			{
				return null;
			}
			return new float?(Convert.ToSingle(@string, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000EBAC File Offset: 0x0000CDAC
		protected void SetNullableSingle(string propertyName, float? value)
		{
			this.SetString(propertyName, (value != null) ? value.Value.ToString(CultureInfo.InvariantCulture) : null);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000EBE0 File Offset: 0x0000CDE0
		protected T GetEnum<T>(string propertyName)
		{
			string @string = this.GetString(propertyName);
			if (@string == null)
			{
				return default(T);
			}
			return (T)((object)Enum.Parse(typeof(T), @string));
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000EC17 File Offset: 0x0000CE17
		protected void SetEnum(string propertyName, Enum value)
		{
			this.SetString(propertyName, value.ToString());
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000EC26 File Offset: 0x0000CE26
		internal void CheckWriteable()
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(DevExceptionMessages.ReadOnly);
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000EC3C File Offset: 0x0000CE3C
		internal static bool IsDataSetReadonly(DataSet dataSet)
		{
			object obj = dataSet.ExtendedProperties["_ReadOnly"];
			return obj != null && Convert.ToBoolean(obj, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000EC6A File Offset: 0x0000CE6A
		internal static void CleanProperties(IDictionary properties)
		{
			properties.Remove("_DsvItem");
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000EC78 File Offset: 0x0000CE78
		internal static T GetDsvItem<T>(IDictionary properties, CreatorGetter<T> createItem) where T : DsvItem
		{
			T t = properties["_DsvItem"] as T;
			if (t == null)
			{
				object syncRoot = properties.SyncRoot;
				lock (syncRoot)
				{
					t = properties["_DsvItem"] as T;
					if (t == null)
					{
						t = createItem();
						properties["_DsvItem"] = t;
					}
				}
			}
			return t;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000ED08 File Offset: 0x0000CF08
		internal static ReadOnlyCollection<DsvColumn> CreateDataColumnArrayWrapper(DataColumn[] columns)
		{
			DsvColumn[] array = new DsvColumn[columns.Length];
			for (int i = 0; i < columns.Length; i++)
			{
				array[i] = DsvColumn.FromDataColumn(columns[i]);
			}
			return new ReadOnlyCollection<DsvColumn>(array);
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600046C RID: 1132
		internal abstract IPersistable DataStorage { get; }

		// Token: 0x0600046D RID: 1133 RVA: 0x0000ED3D File Offset: 0x0000CF3D
		internal static bool AllowExtendedPropertyForBinaryDeserialization(string name)
		{
			return string.CompareOrdinal(name, "IsLogical") == 0;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000ED4D File Offset: 0x0000CF4D
		void IPersistable.Serialize(IntermediateFormatWriter writer)
		{
			this.DataStorage.Serialize(writer);
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000ED5B File Offset: 0x0000CF5B
		void IPersistable.Deserialize(IntermediateFormatReader reader)
		{
			this.DataStorage.Deserialize(reader);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000ED69 File Offset: 0x0000CF69
		void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			this.DataStorage.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000ED78 File Offset: 0x0000CF78
		ObjectType IPersistable.GetObjectType()
		{
			return this.DataStorage.GetObjectType();
		}

		// Token: 0x04000268 RID: 616
		private const string DsvItemExtProperty = "_DsvItem";

		// Token: 0x04000269 RID: 617
		internal const string DataSetReadOnlyExtProperty = "_ReadOnly";

		// Token: 0x0400026A RID: 618
		internal const string DescriptionExtProperty = "Description";

		// Token: 0x0400026B RID: 619
		public const string FriendlyNameExtProperty = "FriendlyName";

		// Token: 0x0400026C RID: 620
		internal const string IsLogicalExtProperty = "IsLogical";

		// Token: 0x0400026D RID: 621
		internal const string NameObjProperty = "Name";

		// Token: 0x0400026E RID: 622
		protected const string StatisticsExtPropertyPrefix = "stats_";
	}
}
