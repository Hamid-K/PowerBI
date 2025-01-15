using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.DataRefresh;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000053 RID: 83
	public abstract class DataSource : NamedMetadataObject, IMetadataObjectWithOverrides
	{
		// Token: 0x060003C9 RID: 969 RVA: 0x0001D91A File Offset: 0x0001BB1A
		private protected DataSource()
		{
			this.InitBodyAndCollections(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0001D92D File Offset: 0x0001BB2D
		private protected DataSource(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections(comparer);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0001D93C File Offset: 0x0001BB3C
		private void InitBodyAndCollections(IEqualityComparer<string> comparer)
		{
			this.body = new DataSource.ObjectBody(this);
			this.body.Name = string.Empty;
			this.body.Description = string.Empty;
			this.body.Type = DataSourceType.Provider;
			this.body.ConnectionString = string.Empty;
			this.body.ImpersonationMode = ImpersonationMode.Default;
			this.body.Account = string.Empty;
			this.body.Password = string.Empty;
			this.body.MaxConnections = 10;
			this.body.Isolation = DatasourceIsolation.ReadCommitted;
			this.body.Provider = string.Empty;
			this.body.ConnectionDetails = string.Empty;
			this.body.Options = string.Empty;
			this.body.Credential = string.Empty;
			this.body.ContextExpression = string.Empty;
			this._Annotations = new DataSourceAnnotationCollection(this, comparer);
			this._ExtendedProperties = new DataSourceExtendedPropertyCollection(this, comparer);
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0001DA40 File Offset: 0x0001BC40
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.DataSource;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0001DA43 File Offset: 0x0001BC43
		// (set) Token: 0x060003CE RID: 974 RVA: 0x0001DA55 File Offset: 0x0001BC55
		public override MetadataObject Parent
		{
			get
			{
				return this.body.ModelID.Object;
			}
			internal set
			{
				if (this.body.ModelID.Object != value)
				{
					MetadataObject.UpdateMetadataObjectParent<DataSource, Model>(this.body.ModelID, (Model)value, null, null);
				}
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0001DA82 File Offset: 0x0001BC82
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.ModelID.ObjectID;
			}
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0001DA94 File Offset: 0x0001BC94
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateChoiceScope())
			{
				DataSource.WriteMetadataSchemaForProviderDataSource(context, writer);
				if (CompatibilityRestrictions.StructuredDataSource.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					DataSource.WriteMetadataSchemaForStructuredDataSource(context, writer);
				}
			}
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0001DAEC File Offset: 0x0001BCEC
		private static void WriteMetadataSchemaForProviderDataSource(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.DataSource, "ProviderDataSource", "ProviderDataSource object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("type", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.DefaultProperty))
				{
					writer.WriteEnumProperty<DataSourceType>("type", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.DefaultProperty, PropertyHelper.GetDataSourceTypeCompatibleValues(context.CompatibilityMode, context.DbCompatibilityLevel));
				}
				if (writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
				{
					writer.WriteProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, typeof(string));
				}
				DataSource.WriteMetadataSchemaForCommonDataSourceRegularProperties(context, writer);
				if (writer.ShouldIncludeProperty("connectionString", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Restricted))
				{
					writer.WriteProperty("connectionString", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Restricted, typeof(string));
				}
				if (writer.ShouldIncludeProperty("impersonationMode", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<ImpersonationMode>("impersonationMode", MetadataPropertyNature.RegularProperty, null);
				}
				if (writer.ShouldIncludeProperty("account", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("account", MetadataPropertyNature.RegularProperty, typeof(string));
				}
				if (writer.ShouldIncludeProperty("password", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Restricted))
				{
					writer.WriteProperty("password", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Restricted, typeof(string));
				}
				if (writer.ShouldIncludeProperty("isolation", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<DatasourceIsolation>("isolation", MetadataPropertyNature.RegularProperty, null);
				}
				if (writer.ShouldIncludeProperty("timeout", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("timeout", MetadataPropertyNature.RegularProperty, typeof(int));
				}
				if (writer.ShouldIncludeProperty("provider", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("provider", MetadataPropertyNature.RegularProperty, typeof(string));
				}
				DataSource.WriteMetadataSchemaForCommonDataSourceChildCollectionsProperties(context, writer);
			}
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0001DCA0 File Offset: 0x0001BEA0
		private static void WriteMetadataSchemaForStructuredDataSource(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.DataSource, "StructuredDataSource", "StructuredDataSource object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("type", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.DefaultProperty))
				{
					writer.WriteEnumProperty<DataSourceType>("type", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.DefaultProperty, PropertyHelper.GetDataSourceTypeCompatibleValues(context.CompatibilityMode, context.DbCompatibilityLevel));
				}
				if (writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
				{
					writer.WriteProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, typeof(string));
				}
				DataSource.WriteMetadataSchemaForCommonDataSourceRegularProperties(context, writer);
				if (CompatibilityRestrictions.StructuredDataSource_ContextExpression.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("contextExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
				{
					writer.WriteProperty("contextExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, typeof(string));
				}
				if (CompatibilityRestrictions.StructuredDataSource_ConnectionDetails.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("connectionDetails", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.JsonString))
				{
					writer.WriteProperty("connectionDetails", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.JsonString, typeof(string));
				}
				if (CompatibilityRestrictions.StructuredDataSource_Options.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("options", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.JsonString))
				{
					writer.WriteProperty("options", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.JsonString, typeof(string));
				}
				if (CompatibilityRestrictions.StructuredDataSource_Credential.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("credential", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.Restricted | MetadataPropertyNature.JsonString))
				{
					writer.WriteProperty("credential", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.Restricted | MetadataPropertyNature.JsonString, typeof(string));
				}
				DataSource.WriteMetadataSchemaForCommonDataSourceChildCollectionsProperties(context, writer);
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0001DE6C File Offset: 0x0001C06C
		private static void WriteMetadataSchemaForCommonDataSourceRegularProperties(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			if (writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
			{
				writer.WriteProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, typeof(string));
			}
			if (writer.ShouldIncludeProperty("maxConnections", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteProperty("maxConnections", MetadataPropertyNature.RegularProperty, typeof(int));
			}
			if (writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0001DEF8 File Offset: 0x0001C0F8
		private static void WriteMetadataSchemaForCommonDataSourceChildCollectionsProperties(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			if (CompatibilityRestrictions.ExtendedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("extendedProperties", MetadataPropertyNature.ChildCollection))
			{
				writer.WriteChildCollection(context, "extendedProperties", MetadataPropertyNature.ChildCollection, ObjectType.ExtendedProperty);
			}
			if (writer.ShouldIncludeProperty("annotations", MetadataPropertyNature.ChildCollection))
			{
				writer.WriteChildCollection(context, "annotations", MetadataPropertyNature.ChildCollection, ObjectType.Annotation);
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0001DF58 File Offset: 0x0001C158
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			base.GetCompatibilityRequirementByMembers(mode, out requiredLevel, out requestingPath);
			if (requiredLevel == -2)
			{
				return;
			}
			if (this.body.Type != DataSourceType.Provider)
			{
				int num = PropertyHelper.GetDataSourceTypeCompatibilityRestrictions(this.body.Type)[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "Type");
					requiredLevel = num;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (!string.IsNullOrEmpty(this.body.ContextExpression))
			{
				int num2 = CompatibilityRestrictions.DataSource_ContextExpression[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num2, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ContextExpression");
					requiredLevel = num2;
					int num3 = requiredLevel;
					return;
				}
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x0001E010 File Offset: 0x0001C210
		// (set) Token: 0x060003D7 RID: 983 RVA: 0x0001E018 File Offset: 0x0001C218
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (DataSource.ObjectBody)value;
			}
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0001E026 File Offset: 0x0001C226
		internal override ITxObjectBody CreateBody()
		{
			return new DataSource.ObjectBody(this);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0001E02E File Offset: 0x0001C22E
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return ((Model)parent).DataSources;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0001E03C File Offset: 0x0001C23C
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			Model model = MetadataObject.ResolveMetadataObjectParentById<DataSource, Model>(this.body.ModelID, objectMap, throwIfCantResolve, null, null);
			if (model != null)
			{
				model.DataSources.Add(this);
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0001E06D File Offset: 0x0001C26D
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0001E06F File Offset: 0x0001C26F
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._Annotations;
			yield return this._ExtendedProperties;
			yield break;
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0001E07F File Offset: 0x0001C27F
		public DataSourceAnnotationCollection Annotations
		{
			get
			{
				return this._Annotations;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0001E087 File Offset: 0x0001C287
		[CompatibilityRequirement("1400")]
		public DataSourceExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this._ExtendedProperties;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0001E08F File Offset: 0x0001C28F
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x0001E09C File Offset: 0x0001C29C
		public override string Name
		{
			get
			{
				return this.body.Name;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Name, value))
				{
					string text;
					if (!Utils.IsSyntacticallyValidName(value, ObjectType.DataSource, out text))
					{
						throw new ArgumentException(text);
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "Name", typeof(string), this.body.Name, value);
					string name = this.body.Name;
					this.body.Name = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Name", typeof(string), name, value);
				}
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0001E11E File Offset: 0x0001C31E
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x0001E12C File Offset: 0x0001C32C
		public string Description
		{
			get
			{
				return this.body.Description;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Description, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Description", typeof(string), this.body.Description, value);
					string description = this.body.Description;
					this.body.Description = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Description", typeof(string), description, value);
				}
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0001E19C File Offset: 0x0001C39C
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x0001E1AC File Offset: 0x0001C3AC
		public DataSourceType Type
		{
			get
			{
				return this.body.Type;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Type, value))
				{
					CompatibilityRestrictionSet dataSourceTypeCompatibilityRestrictions = PropertyHelper.GetDataSourceTypeCompatibilityRestrictions(value);
					CompatibilityRestrictionSet dataSourceTypeCompatibilityRestrictions2 = PropertyHelper.GetDataSourceTypeCompatibilityRestrictions(this.body.Type);
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					RestrictionsComapreResult restrictionsComapreResult = dataSourceTypeCompatibilityRestrictions.Compare(dataSourceTypeCompatibilityRestrictions2);
					if (restrictionsComapreResult == RestrictionsComapreResult.MoreRestrictive || restrictionsComapreResult == RestrictionsComapreResult.Incomparable || (restrictionsComapreResult == RestrictionsComapreResult.Equal && value != DataSourceType.Provider))
					{
						array = base.ValidateCompatibilityRequirement(dataSourceTypeCompatibilityRestrictions, string.Format("[{0}]::[{1}]=[{2}]", this.GetFormattedObjectPath(), "Type", value));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "Type", typeof(DataSourceType), this.body.Type, value);
					switch (restrictionsComapreResult)
					{
					case RestrictionsComapreResult.Incomparable:
						base.ResetCompatibilityRequirement();
						base.SetCompatibilityRequirement(dataSourceTypeCompatibilityRestrictions, array);
						break;
					case RestrictionsComapreResult.LessRestrictive:
						base.ResetCompatibilityRequirement();
						break;
					case RestrictionsComapreResult.Equal:
						if (array != null)
						{
							base.SetCompatibilityRequirement(dataSourceTypeCompatibilityRestrictions, array);
						}
						break;
					case RestrictionsComapreResult.MoreRestrictive:
						base.SetCompatibilityRequirement(dataSourceTypeCompatibilityRestrictions, array);
						break;
					}
					DataSourceType type = this.body.Type;
					this.body.Type = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Type", typeof(DataSourceType), type, value);
				}
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0001E2CE File Offset: 0x0001C4CE
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x0001E2DC File Offset: 0x0001C4DC
		internal string ConnectionString
		{
			get
			{
				return this.body.ConnectionString;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.ConnectionString, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "ConnectionString", typeof(string), this.body.ConnectionString, value);
					string connectionString = this.body.ConnectionString;
					this.body.ConnectionString = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "ConnectionString", typeof(string), connectionString, value);
				}
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0001E34C File Offset: 0x0001C54C
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x0001E35C File Offset: 0x0001C55C
		internal ImpersonationMode ImpersonationMode
		{
			get
			{
				return this.body.ImpersonationMode;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.ImpersonationMode, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "ImpersonationMode", typeof(ImpersonationMode), this.body.ImpersonationMode, value);
					ImpersonationMode impersonationMode = this.body.ImpersonationMode;
					this.body.ImpersonationMode = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "ImpersonationMode", typeof(ImpersonationMode), impersonationMode, value);
				}
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0001E3E0 File Offset: 0x0001C5E0
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x0001E3F0 File Offset: 0x0001C5F0
		internal string Account
		{
			get
			{
				return this.body.Account;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Account, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Account", typeof(string), this.body.Account, value);
					string account = this.body.Account;
					this.body.Account = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Account", typeof(string), account, value);
				}
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0001E460 File Offset: 0x0001C660
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x0001E470 File Offset: 0x0001C670
		internal string Password
		{
			get
			{
				return this.body.Password;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Password, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Password", typeof(string), this.body.Password, value);
					string password = this.body.Password;
					this.body.Password = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Password", typeof(string), password, value);
				}
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0001E4E0 File Offset: 0x0001C6E0
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x0001E4F0 File Offset: 0x0001C6F0
		public int MaxConnections
		{
			get
			{
				return this.body.MaxConnections;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.MaxConnections, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "MaxConnections", typeof(int), this.body.MaxConnections, value);
					int maxConnections = this.body.MaxConnections;
					this.body.MaxConnections = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "MaxConnections", typeof(int), maxConnections, value);
				}
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0001E574 File Offset: 0x0001C774
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x0001E584 File Offset: 0x0001C784
		internal DatasourceIsolation Isolation
		{
			get
			{
				return this.body.Isolation;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Isolation, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Isolation", typeof(DatasourceIsolation), this.body.Isolation, value);
					DatasourceIsolation isolation = this.body.Isolation;
					this.body.Isolation = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Isolation", typeof(DatasourceIsolation), isolation, value);
				}
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0001E608 File Offset: 0x0001C808
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x0001E618 File Offset: 0x0001C818
		internal int Timeout
		{
			get
			{
				return this.body.Timeout;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Timeout, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Timeout", typeof(int), this.body.Timeout, value);
					int timeout = this.body.Timeout;
					this.body.Timeout = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Timeout", typeof(int), timeout, value);
				}
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0001E69C File Offset: 0x0001C89C
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x0001E6AC File Offset: 0x0001C8AC
		internal string Provider
		{
			get
			{
				return this.body.Provider;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Provider, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Provider", typeof(string), this.body.Provider, value);
					string provider = this.body.Provider;
					this.body.Provider = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Provider", typeof(string), provider, value);
				}
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0001E71C File Offset: 0x0001C91C
		// (set) Token: 0x060003F6 RID: 1014 RVA: 0x0001E72C File Offset: 0x0001C92C
		public DateTime ModifiedTime
		{
			get
			{
				return this.body.ModifiedTime;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.ModifiedTime, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "ModifiedTime", typeof(DateTime), this.body.ModifiedTime, value);
					DateTime modifiedTime = this.body.ModifiedTime;
					this.body.ModifiedTime = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "ModifiedTime", typeof(DateTime), modifiedTime, value);
				}
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0001E7B0 File Offset: 0x0001C9B0
		// (set) Token: 0x060003F8 RID: 1016 RVA: 0x0001E7C0 File Offset: 0x0001C9C0
		[CompatibilityRequirement("1400")]
		internal string ContextExpression
		{
			get
			{
				return this.body.ContextExpression;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.ContextExpression, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (!string.IsNullOrEmpty(value))
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.DataSource_ContextExpression, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ContextExpression"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "ContextExpression", typeof(string), this.body.ContextExpression, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.DataSource_ContextExpression, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					string contextExpression = this.body.ContextExpression;
					this.body.ContextExpression = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "ContextExpression", typeof(string), contextExpression, value);
				}
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0001E875 File Offset: 0x0001CA75
		// (set) Token: 0x060003FA RID: 1018 RVA: 0x0001E887 File Offset: 0x0001CA87
		internal ObjectId _ModelID
		{
			get
			{
				return this.body.ModelID.ObjectID;
			}
			set
			{
				this.body.ModelID.ObjectID = value;
			}
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0001E89C File Offset: 0x0001CA9C
		internal virtual void CopyFrom(DataSource other, CopyContext context)
		{
			base.CopyFrom(other, context);
			bool flag;
			if ((context.Flags & CopyFlags.IncludeCompatRestictions) == CopyFlags.IncludeCompatRestictions)
			{
				flag = true;
			}
			else if ((context.Flags & CopyFlags.MetadataSync) == CopyFlags.MetadataSync)
			{
				flag = this.body.ModifiedTime.CompareTo(other.body.ModifiedTime) != 0;
			}
			else
			{
				flag = !this.body.IsEqualTo(other.body, context);
			}
			if (flag)
			{
				ObjectChangeTracker.RegisterUpcomingPropertyChange(this);
				this.body.CopyFrom(other.body, context);
			}
			if ((context.Flags & CopyFlags.ShallowCopy) != CopyFlags.ShallowCopy)
			{
				this.Annotations.CopyFrom(other.Annotations, context);
				this.ExtendedProperties.CopyFrom(other.ExtendedProperties, context);
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0001E95E File Offset: 0x0001CB5E
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((DataSource)other, context);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0001E96D File Offset: 0x0001CB6D
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(DataSource other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0001E989 File Offset: 0x0001CB89
		public void CopyTo(DataSource other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0001E9A5 File Offset: 0x0001CBA5
		public DataSource Clone()
		{
			return base.CloneInternal<DataSource>();
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0001E9AD File Offset: 0x0001CBAD
		internal virtual void BeforeBodyCompareWith()
		{
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0001E9B0 File Offset: 0x0001CBB0
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (!string.IsNullOrEmpty(this.body.Name))
			{
				writer.WriteProperty<string>(options, "Name", this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.Description))
			{
				writer.WriteProperty<string>(options, "Description", this.body.Description);
			}
			if (this.body.Type != DataSourceType.Provider)
			{
				if (!PropertyHelper.IsDataSourceTypeValueCompatible(this.body.Type, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Type is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<DataSourceType>(options, "Type", this.body.Type);
			}
			if (!string.IsNullOrEmpty(this.body.ConnectionString))
			{
				writer.WriteProperty<string>(options, "ConnectionString", this.body.ConnectionString);
			}
			if (this.body.ImpersonationMode != ImpersonationMode.Default)
			{
				writer.WriteProperty<ImpersonationMode>(options, "ImpersonationMode", this.body.ImpersonationMode);
			}
			if (!string.IsNullOrEmpty(this.body.Account))
			{
				writer.WriteProperty<string>(options, "Account", this.body.Account);
			}
			if (!string.IsNullOrEmpty(this.body.Password))
			{
				writer.WriteProperty<string>(options, "Password", this.body.Password);
			}
			if (this.body.MaxConnections != 10)
			{
				writer.WriteProperty<int>(options, "MaxConnections", this.body.MaxConnections);
			}
			if (this.body.Isolation != DatasourceIsolation.ReadCommitted)
			{
				writer.WriteProperty<DatasourceIsolation>(options, "Isolation", this.body.Isolation);
			}
			if (this.body.Timeout != 0)
			{
				writer.WriteProperty<int>(options, "Timeout", this.body.Timeout);
			}
			if (!string.IsNullOrEmpty(this.body.Provider))
			{
				writer.WriteProperty<string>(options, "Provider", this.body.Provider);
			}
			if (!string.IsNullOrEmpty(this.body.ContextExpression))
			{
				if (!CompatibilityRestrictions.DataSource_ContextExpression.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ContextExpression is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<string>(options, "ContextExpression", this.body.ContextExpression);
			}
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0001EC0C File Offset: 0x0001CE0C
		void IMetadataObjectWithOverrides.WriteAllOverridenBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel, ReplacementPropertiesCollection newProperties)
		{
			this.BeforeBodyCompareWith();
			string text;
			if (newProperties.IsPropertyOverriden<string>("ConnectionString", out text) && !PropertyHelper.AreValuesIdentical(this.body.ConnectionString, text))
			{
				writer.WriteProperty<string>(options, "ConnectionString", text);
			}
			ImpersonationMode impersonationMode;
			if (newProperties.IsPropertyOverriden<ImpersonationMode>("ImpersonationMode", out impersonationMode) && !PropertyHelper.AreValuesIdentical(this.body.ImpersonationMode, impersonationMode))
			{
				writer.WriteProperty<ImpersonationMode>(options, "ImpersonationMode", impersonationMode);
			}
			string text2;
			if (newProperties.IsPropertyOverriden<string>("Account", out text2) && !PropertyHelper.AreValuesIdentical(this.body.Account, text2))
			{
				writer.WriteProperty<string>(options, "Account", text2);
			}
			string text3;
			if (newProperties.IsPropertyOverriden<string>("Password", out text3) && !PropertyHelper.AreValuesIdentical(this.body.Password, text3))
			{
				writer.WriteProperty<string>(options, "Password", text3);
			}
			int num;
			if (newProperties.IsPropertyOverriden<int>("MaxConnections", out num) && !PropertyHelper.AreValuesIdentical(this.body.MaxConnections, num))
			{
				writer.WriteProperty<int>(options, "MaxConnections", num);
			}
			DatasourceIsolation datasourceIsolation;
			if (newProperties.IsPropertyOverriden<DatasourceIsolation>("Isolation", out datasourceIsolation) && !PropertyHelper.AreValuesIdentical(this.body.Isolation, datasourceIsolation))
			{
				writer.WriteProperty<DatasourceIsolation>(options, "Isolation", datasourceIsolation);
			}
			int num2;
			if (newProperties.IsPropertyOverriden<int>("Timeout", out num2) && !PropertyHelper.AreValuesIdentical(this.body.Timeout, num2))
			{
				writer.WriteProperty<int>(options, "Timeout", num2);
			}
			string text4;
			if (newProperties.IsPropertyOverriden<string>("Provider", out text4) && !PropertyHelper.AreValuesIdentical(this.body.Provider, text4))
			{
				writer.WriteProperty<string>(options, "Provider", text4);
			}
			string text5;
			if (newProperties.IsPropertyOverriden<string>("ConnectionDetails", out text5) && !PropertyHelper.AreValuesIdentical(this.body.ConnectionDetails, text5))
			{
				if (!CompatibilityRestrictions.DataSource_ConnectionDetails.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ConnectionDetails is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<string>(options, "ConnectionDetails", text5);
			}
			string text6;
			if (newProperties.IsPropertyOverriden<string>("Options", out text6) && !PropertyHelper.AreValuesIdentical(this.body.Options, text6))
			{
				if (!CompatibilityRestrictions.DataSource_Options.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Options is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<string>(options, "Options", text6);
			}
			string text7;
			if (newProperties.IsPropertyOverriden<string>("Credential", out text7) && !PropertyHelper.AreValuesIdentical(this.body.Credential, text7))
			{
				if (!CompatibilityRestrictions.DataSource_Credential.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Credential is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<string>(options, "Credential", text7);
			}
			string text8;
			if (newProperties.IsPropertyOverriden<string>("ContextExpression", out text8) && !PropertyHelper.AreValuesIdentical(this.body.ContextExpression, text8))
			{
				if (!CompatibilityRestrictions.DataSource_ContextExpression.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ContextExpression is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<string>(options, "ContextExpression", text8);
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0001EF3C File Offset: 0x0001D13C
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("ModelID", out objectId))
			{
				this.body.ModelID.ObjectID = objectId;
			}
			string text;
			if (reader.TryReadProperty<string>("Name", out text))
			{
				this.body.Name = text;
			}
			string text2;
			if (reader.TryReadProperty<string>("Description", out text2))
			{
				this.body.Description = text2;
			}
			DataSourceType dataSourceType;
			if (reader.TryReadProperty<DataSourceType>("Type", out dataSourceType))
			{
				this.body.Type = dataSourceType;
			}
			string text3;
			if (reader.TryReadProperty<string>("ConnectionString", out text3))
			{
				this.body.ConnectionString = text3;
			}
			ImpersonationMode impersonationMode;
			if (reader.TryReadProperty<ImpersonationMode>("ImpersonationMode", out impersonationMode))
			{
				this.body.ImpersonationMode = impersonationMode;
			}
			string text4;
			if (reader.TryReadProperty<string>("Account", out text4))
			{
				this.body.Account = text4;
			}
			string text5;
			if (reader.TryReadProperty<string>("Password", out text5))
			{
				this.body.Password = text5;
			}
			int num;
			if (reader.TryReadProperty<int>("MaxConnections", out num))
			{
				this.body.MaxConnections = num;
			}
			DatasourceIsolation datasourceIsolation;
			if (reader.TryReadProperty<DatasourceIsolation>("Isolation", out datasourceIsolation))
			{
				this.body.Isolation = datasourceIsolation;
			}
			int num2;
			if (reader.TryReadProperty<int>("Timeout", out num2))
			{
				this.body.Timeout = num2;
			}
			string text6;
			if (reader.TryReadProperty<string>("Provider", out text6))
			{
				this.body.Provider = text6;
			}
			DateTime dateTime;
			if (reader.TryReadProperty<DateTime>("ModifiedTime", out dateTime))
			{
				this.body.ModifiedTime = dateTime;
			}
			string text7;
			if (CompatibilityRestrictions.DataSource_ContextExpression.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<string>("ContextExpression", out text7))
			{
				this.body.ContextExpression = text7;
			}
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0001F0EC File Offset: 0x0001D2EC
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataBodyProperties(context, writer);
			if (!string.IsNullOrEmpty(this.body.Name) && writer.ShouldIncludeProperty("Name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("Name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.body.Description);
			}
			if (this.body.Type != DataSourceType.Provider)
			{
				if (!PropertyHelper.IsDataSourceTypeValueCompatible(this.body.Type, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Type is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("Type", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.DefaultProperty))
				{
					writer.WriteEnumProperty<DataSourceType>("Type", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.DefaultProperty, this.body.Type);
				}
			}
			if (!string.IsNullOrEmpty(this.body.ConnectionString) && writer.ShouldIncludeProperty("ConnectionString", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Restricted))
			{
				writer.WriteStringProperty("ConnectionString", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Restricted, this.body.ConnectionString);
			}
			if (this.body.ImpersonationMode != ImpersonationMode.Default && writer.ShouldIncludeProperty("ImpersonationMode", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<ImpersonationMode>("ImpersonationMode", MetadataPropertyNature.RegularProperty, this.body.ImpersonationMode);
			}
			if (!string.IsNullOrEmpty(this.body.Account) && writer.ShouldIncludeProperty("Account", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("Account", MetadataPropertyNature.RegularProperty, this.body.Account);
			}
			if (!string.IsNullOrEmpty(this.body.Password) && writer.ShouldIncludeProperty("Password", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Restricted))
			{
				writer.WriteStringProperty("Password", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Restricted, this.body.Password);
			}
			if (this.body.MaxConnections != 10 && writer.ShouldIncludeProperty("MaxConnections", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteInt32Property("MaxConnections", MetadataPropertyNature.RegularProperty, this.body.MaxConnections);
			}
			if (this.body.Isolation != DatasourceIsolation.ReadCommitted && writer.ShouldIncludeProperty("Isolation", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<DatasourceIsolation>("Isolation", MetadataPropertyNature.RegularProperty, this.body.Isolation);
			}
			if (this.body.Timeout != 0 && writer.ShouldIncludeProperty("Timeout", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteInt32Property("Timeout", MetadataPropertyNature.RegularProperty, this.body.Timeout);
			}
			if (!string.IsNullOrEmpty(this.body.Provider) && writer.ShouldIncludeProperty("Provider", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("Provider", MetadataPropertyNature.RegularProperty, this.body.Provider);
			}
			if (!string.IsNullOrEmpty(this.body.ContextExpression))
			{
				if (!CompatibilityRestrictions.DataSource_ContextExpression.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ContextExpression is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("ContextExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
				{
					writer.WriteStringProperty("ContextExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.body.ContextExpression);
				}
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0001F444 File Offset: 0x0001D644
		private protected virtual void WriteRegularPropertiesToMetadataStream(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.body.Description);
			}
			if (this.body.MaxConnections != 10 && writer.ShouldIncludeProperty("maxConnections", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteInt32Property("maxConnections", MetadataPropertyNature.RegularProperty, this.body.MaxConnections);
			}
			if (this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.ModifiedTime);
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0001F508 File Offset: 0x0001D708
		private protected virtual void WriteDirectChildrenToMetadataStream(SerializationActivityContext context, IMetadataWriter writer)
		{
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0001F50C File Offset: 0x0001D70C
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataTree(context, writer);
			if (this.body.Type != DataSourceType.Provider || context.SerializationMode == MetadataSerializationMode.Tmdl)
			{
				if (!PropertyHelper.IsDataSourceTypeValueCompatible(this.body.Type, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Type is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("type", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.DefaultProperty))
				{
					writer.WriteEnumProperty<DataSourceType>("type", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.DefaultProperty, this.body.Type);
				}
			}
			if (!string.IsNullOrEmpty(this.body.Name) && writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
			this.WriteRegularPropertiesToMetadataStream(context, writer);
			this.WriteDirectChildrenToMetadataStream(context, writer);
			if (this.ExtendedProperties.Count > 0)
			{
				if (!CompatibilityRestrictions.ExtendedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("A child ExtendedProperty is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("extendedProperties", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "extendedProperties", MetadataPropertyNature.ChildCollection, this.ExtendedProperties);
				}
			}
			if (this.Annotations.Count > 0 && writer.ShouldIncludeProperty("annotations", MetadataPropertyNature.ChildCollection))
			{
				writer.WriteChildCollection(context, "annotations", MetadataPropertyNature.ChildCollection, this.Annotations);
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0001F6A4 File Offset: 0x0001D8A4
		private protected override bool TryReadNextMetadataProperty(SerializationActivityContext context, IMetadataReader reader, out UnexpectedPropertyClassification classification)
		{
			if (base.TryReadNextMetadataProperty(context, reader, out classification))
			{
				return true;
			}
			string propertyName = reader.PropertyName;
			if (propertyName != null)
			{
				switch (propertyName.Length)
				{
				case 4:
				{
					char c = propertyName[0];
					if (c <= 'T')
					{
						if (c != 'N')
						{
							if (c != 'T')
							{
								break;
							}
							if (!(propertyName == "Type"))
							{
								break;
							}
							goto IL_0356;
						}
						else if (!(propertyName == "Name"))
						{
							break;
						}
					}
					else if (c != 'n')
					{
						if (c != 't')
						{
							break;
						}
						if (!(propertyName == "type"))
						{
							break;
						}
						goto IL_0356;
					}
					else if (!(propertyName == "name"))
					{
						break;
					}
					this.body.Name = reader.ReadStringProperty();
					return true;
					IL_0356:
					DataSourceType dataSourceType = reader.ReadEnumProperty<DataSourceType>();
					if (!PropertyHelper.IsDataSourceTypeValueCompatible(dataSourceType, context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatiblePropertyValue;
						return false;
					}
					this.body.Type = dataSourceType;
					return true;
				}
				case 7:
				{
					char c = propertyName[0];
					if (c != 'A')
					{
						if (c != 'M')
						{
							if (c == 'T')
							{
								if (propertyName == "Timeout")
								{
									this.body.Timeout = reader.ReadInt32Property();
									return true;
								}
							}
						}
						else if (propertyName == "ModelID")
						{
							this.body.ModelID.ObjectID = reader.ReadObjectIdProperty();
							return true;
						}
					}
					else if (propertyName == "Account")
					{
						this.body.Account = reader.ReadStringProperty();
						return true;
					}
					break;
				}
				case 8:
				{
					char c = propertyName[1];
					if (c != 'a')
					{
						if (c == 'r')
						{
							if (propertyName == "Provider")
							{
								this.body.Provider = reader.ReadStringProperty();
								return true;
							}
						}
					}
					else if (propertyName == "Password")
					{
						this.body.Password = reader.ReadStringProperty();
						return true;
					}
					break;
				}
				case 9:
					if (propertyName == "Isolation")
					{
						this.body.Isolation = reader.ReadEnumProperty<DatasourceIsolation>();
						return true;
					}
					break;
				case 11:
				{
					char c = propertyName[0];
					if (c != 'D')
					{
						if (c != 'a')
						{
							if (c != 'd')
							{
								break;
							}
							if (!(propertyName == "description"))
							{
								break;
							}
						}
						else
						{
							if (!(propertyName == "annotations"))
							{
								break;
							}
							using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
							{
								foreach (Annotation annotation in reader.ReadChildCollectionProperty<Annotation>(context))
								{
									try
									{
										this.Annotations.Add(annotation);
									}
									catch (Exception ex)
									{
										throw reader.CreateInvalidChildException(context, annotation, TomSR.Exception_FailedAddDeserializedNamedObject("Annotation", (annotation != null) ? annotation.Name : null, ex.Message), ex);
									}
								}
							}
							return true;
						}
					}
					else if (!(propertyName == "Description"))
					{
						break;
					}
					this.body.Description = reader.ReadStringProperty();
					return true;
				}
				case 12:
				{
					char c = propertyName[0];
					if (c != 'M')
					{
						if (c != 'm')
						{
							break;
						}
						if (!(propertyName == "modifiedTime"))
						{
							break;
						}
					}
					else if (!(propertyName == "ModifiedTime"))
					{
						break;
					}
					this.body.ModifiedTime = reader.ReadDateTimeProperty();
					return true;
				}
				case 14:
				{
					char c = propertyName[0];
					if (c != 'M')
					{
						if (c != 'm')
						{
							break;
						}
						if (!(propertyName == "maxConnections"))
						{
							break;
						}
					}
					else if (!(propertyName == "MaxConnections"))
					{
						break;
					}
					this.body.MaxConnections = reader.ReadInt32Property();
					return true;
				}
				case 16:
					if (propertyName == "ConnectionString")
					{
						this.body.ConnectionString = reader.ReadStringProperty();
						return true;
					}
					break;
				case 17:
				{
					char c = propertyName[0];
					if (c != 'C')
					{
						if (c == 'I')
						{
							if (propertyName == "ImpersonationMode")
							{
								this.body.ImpersonationMode = reader.ReadEnumProperty<ImpersonationMode>();
								return true;
							}
						}
					}
					else if (propertyName == "ContextExpression")
					{
						if (!CompatibilityRestrictions.DataSource_ContextExpression.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							classification = UnexpectedPropertyClassification.IncompatibleProperty;
							return false;
						}
						this.body.ContextExpression = reader.ReadStringProperty();
						return true;
					}
					break;
				}
				case 18:
					if (propertyName == "extendedProperties")
					{
						if (!CompatibilityRestrictions.ExtendedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							classification = UnexpectedPropertyClassification.IncompatibleProperty;
							return false;
						}
						using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
						{
							foreach (ExtendedProperty extendedProperty in reader.ReadChildCollectionProperty<ExtendedProperty>(context))
							{
								try
								{
									this.ExtendedProperties.Add(extendedProperty);
								}
								catch (Exception ex2)
								{
									throw reader.CreateInvalidChildException(context, extendedProperty, TomSR.Exception_FailedAddDeserializedNamedObject("ExtendedProperty", (extendedProperty != null) ? extendedProperty.Name : null, ex2.Message), ex2);
								}
							}
						}
						return true;
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0001FC88 File Offset: 0x0001DE88
		[Obsolete("Deprecated. Use RequestRename method instead.", false)]
		public void Rename(string newName)
		{
			this.RequestRename(newName);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0001FC91 File Offset: 0x0001DE91
		public void RequestRename(string newName)
		{
			ObjectChangeTracker.RegisterObjectRenaming(this);
			this.Name = newName;
			this.body.RenameRequestedThroughAPI = true;
			ObjectChangeTracker.RegisterObjectRenamed(this);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0001FCB4 File Offset: 0x0001DEB4
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!string.IsNullOrEmpty(this.body.Name))
			{
				result["name", TomPropCategory.Name, 2, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Name, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.Description))
			{
				result["description", TomPropCategory.Regular, 3, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Description, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.Type != DataSourceType.Provider)
			{
				if (!PropertyHelper.IsDataSourceTypeValueCompatible(this.body.Type, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Type is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["type", TomPropCategory.Type, 4, false] = JsonPropertyHelper.ConvertEnumToJsonValue<DataSourceType>(this.body.Type);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.MaxConnections != 10)
			{
				result["maxConnections", TomPropCategory.Regular, 9, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<int>(this.body.MaxConnections);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["modifiedTime", TomPropCategory.Regular, 13, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.ModifiedTime);
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations && !options.IncludeTranslatablePropertiesOnly)
			{
				IEnumerable<ExtendedProperty> enumerable;
				if (!options.IgnoreInferredObjects)
				{
					IEnumerable<ExtendedProperty> extendedProperties = this.ExtendedProperties;
					enumerable = extendedProperties;
				}
				else
				{
					enumerable = this.ExtendedProperties.Where((ExtendedProperty o) => !ObjectTreeHelper.IsInferredObject(o));
				}
				IEnumerable<ExtendedProperty> enumerable2 = enumerable;
				if (enumerable2.Any<ExtendedProperty>())
				{
					if (!CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
					{
						throw TomInternalException.Create("A child ExtendedProperty is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
					}
					object[] array = enumerable2.Select((ExtendedProperty obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
					object[] array2 = array;
					result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array2;
				}
			}
			if (!options.IgnoreChildren && !options.IncludeTranslatablePropertiesOnly && this.Annotations.Any<Annotation>())
			{
				object[] array = this.Annotations.Select((Annotation obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array3;
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0001FFC0 File Offset: 0x0001E1C0
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name != null)
			{
				int length = name.Length;
				if (length != 4)
				{
					switch (length)
					{
					case 11:
					{
						char c = name[0];
						if (c != 'a')
						{
							if (c == 'd')
							{
								if (name == "description")
								{
									this.body.Description = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
									return true;
								}
							}
						}
						else if (name == "annotations")
						{
							JsonPropertyHelper.ReadObjectCollection(this.Annotations, jsonProp.Value, options, mode, dbCompatibilityLevel);
							return true;
						}
						break;
					}
					case 12:
						if (name == "modifiedTime")
						{
							this.body.ModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
							return true;
						}
						break;
					case 13:
						break;
					case 14:
						if (name == "maxConnections")
						{
							this.body.MaxConnections = JsonPropertyHelper.ConvertJsonValueToPrimitive<int>(jsonProp.Value);
							return true;
						}
						break;
					default:
						if (length == 18)
						{
							if (name == "extendedProperties")
							{
								if (!JsonPropertyHelper.IsEmptyObjectCollection(jsonProp.Value) && !CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
								{
									return false;
								}
								JsonPropertyHelper.ReadObjectCollection(this.ExtendedProperties, jsonProp.Value, options, mode, dbCompatibilityLevel);
								return true;
							}
						}
						break;
					}
				}
				else
				{
					char c = name[0];
					if (c != 'n')
					{
						if (c == 't')
						{
							if (name == "type")
							{
								DataSourceType dataSourceType = JsonPropertyHelper.ConvertJsonValueToEnum<DataSourceType>(jsonProp.Value);
								if (jsonProp.Value.Type != 10 && !PropertyHelper.IsDataSourceTypeValueCompatible(dataSourceType, mode, dbCompatibilityLevel))
								{
									return false;
								}
								this.body.Type = dataSourceType;
								return true;
							}
						}
					}
					else if (name == "name")
					{
						this.body.Name = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x000201AC File Offset: 0x0001E3AC
		internal static DataSource CreateFromMetadataStream(SerializationActivityContext context, IMetadataReader reader)
		{
			DataSourceType dataSourceType;
			if (reader.TryMoveToProperty("type"))
			{
				dataSourceType = reader.ReadEnumProperty<DataSourceType>();
			}
			else
			{
				dataSourceType = DataSourceType.Provider;
			}
			reader.Reset();
			if (dataSourceType == DataSourceType.Provider)
			{
				return new ProviderDataSource();
			}
			if (dataSourceType != DataSourceType.Structured)
			{
				throw reader.CreateInvalidDataException(context, TomSR.Exception_UnrecognizedValueOfType("DataSourceType", dataSourceType.ToString()), null);
			}
			return new StructuredDataSource();
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0002020C File Offset: 0x0001E40C
		internal override string GetFormattedObjectPath()
		{
			return TomSR.ObjectPath_DataSource_1Arg(this.Name);
		}

		// Token: 0x040000F0 RID: 240
		internal DataSource.ObjectBody body;

		// Token: 0x040000F1 RID: 241
		private DataSourceAnnotationCollection _Annotations;

		// Token: 0x040000F2 RID: 242
		private DataSourceExtendedPropertyCollection _ExtendedProperties;

		// Token: 0x040000F3 RID: 243
		internal static Func<DataSource, DataSource, bool> CompareDataSourceType = (DataSource dataSource1, DataSource dataSource2) => dataSource1.Type == dataSource2.Type;

		// Token: 0x0200025A RID: 602
		internal class ObjectBody : NamedMetadataObjectBody<DataSource>
		{
			// Token: 0x06001FEF RID: 8175 RVA: 0x000D226F File Offset: 0x000D046F
			public ObjectBody(DataSource owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.ModelID = new ParentLink<DataSource, Model>(owner, "Model");
			}

			// Token: 0x06001FF0 RID: 8176 RVA: 0x000D2294 File Offset: 0x000D0494
			public override string GetObjectName()
			{
				return this.Name;
			}

			// Token: 0x06001FF1 RID: 8177 RVA: 0x000D229C File Offset: 0x000D049C
			internal bool IsEqualTo(DataSource.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.Type, other.Type)) && PropertyHelper.AreValuesIdentical(this.ConnectionString, other.ConnectionString) && PropertyHelper.AreValuesIdentical(this.ImpersonationMode, other.ImpersonationMode) && PropertyHelper.AreValuesIdentical(this.Account, other.Account) && PropertyHelper.AreValuesIdentical(this.Password, other.Password) && PropertyHelper.AreValuesIdentical(this.MaxConnections, other.MaxConnections) && PropertyHelper.AreValuesIdentical(this.Isolation, other.Isolation) && PropertyHelper.AreValuesIdentical(this.Timeout, other.Timeout) && PropertyHelper.AreValuesIdentical(this.Provider, other.Provider) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && PropertyHelper.AreValuesIdentical(this.ConnectionDetails, other.ConnectionDetails) && PropertyHelper.AreValuesIdentical(this.Options, other.Options) && PropertyHelper.AreValuesIdentical(this.Credential, other.Credential) && PropertyHelper.AreValuesIdentical(this.ContextExpression, other.ContextExpression) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.ModelID.IsEqualTo(other.ModelID, context));
			}

			// Token: 0x06001FF2 RID: 8178 RVA: 0x000D244C File Offset: 0x000D064C
			internal void CopyFromImpl(DataSource.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				base.Owner.Name = other.Name;
				this.Description = other.Description;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.Type = other.Type;
				}
				if ((context.Flags & CopyFlags.Incremental) != CopyFlags.Incremental)
				{
					this.ConnectionString = other.ConnectionString;
				}
				this.ImpersonationMode = other.ImpersonationMode;
				this.Account = other.Account;
				if ((context.Flags & CopyFlags.Incremental) != CopyFlags.Incremental)
				{
					this.Password = other.Password;
				}
				this.MaxConnections = other.MaxConnections;
				this.Isolation = other.Isolation;
				this.Timeout = other.Timeout;
				this.Provider = other.Provider;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModifiedTime = other.ModifiedTime;
				}
				this.ConnectionDetails = other.ConnectionDetails;
				this.Options = other.Options;
				if ((context.Flags & CopyFlags.Incremental) != CopyFlags.Incremental)
				{
					this.Credential = other.Credential;
				}
				this.ContextExpression = other.ContextExpression;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModelID.CopyFrom(other.ModelID, context);
				}
			}

			// Token: 0x06001FF3 RID: 8179 RVA: 0x000D25A0 File Offset: 0x000D07A0
			internal void CopyFromImpl(DataSource.ObjectBody other)
			{
				this.Name = other.Name;
				this.Description = other.Description;
				this.Type = other.Type;
				this.ConnectionString = other.ConnectionString;
				this.ImpersonationMode = other.ImpersonationMode;
				this.Account = other.Account;
				this.Password = other.Password;
				this.MaxConnections = other.MaxConnections;
				this.Isolation = other.Isolation;
				this.Timeout = other.Timeout;
				this.Provider = other.Provider;
				this.ModifiedTime = other.ModifiedTime;
				this.ConnectionDetails = other.ConnectionDetails;
				this.Options = other.Options;
				this.Credential = other.Credential;
				this.ContextExpression = other.ContextExpression;
				this.ModelID.CopyFrom(other.ModelID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x06001FF4 RID: 8180 RVA: 0x000D2683 File Offset: 0x000D0883
			public override void CopyFrom(MetadataObjectBody<DataSource> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((DataSource.ObjectBody)other, context);
			}

			// Token: 0x06001FF5 RID: 8181 RVA: 0x000D269C File Offset: 0x000D089C
			internal bool IsEqualTo(DataSource.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.ConnectionString, other.ConnectionString) && PropertyHelper.AreValuesIdentical(this.ImpersonationMode, other.ImpersonationMode) && PropertyHelper.AreValuesIdentical(this.Account, other.Account) && PropertyHelper.AreValuesIdentical(this.Password, other.Password) && PropertyHelper.AreValuesIdentical(this.MaxConnections, other.MaxConnections) && PropertyHelper.AreValuesIdentical(this.Isolation, other.Isolation) && PropertyHelper.AreValuesIdentical(this.Timeout, other.Timeout) && PropertyHelper.AreValuesIdentical(this.Provider, other.Provider) && PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && PropertyHelper.AreValuesIdentical(this.ConnectionDetails, other.ConnectionDetails) && PropertyHelper.AreValuesIdentical(this.Options, other.Options) && PropertyHelper.AreValuesIdentical(this.Credential, other.Credential) && PropertyHelper.AreValuesIdentical(this.ContextExpression, other.ContextExpression) && this.ModelID.IsEqualTo(other.ModelID);
			}

			// Token: 0x06001FF6 RID: 8182 RVA: 0x000D27FA File Offset: 0x000D09FA
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				base.Owner.BeforeBodyCompareWith();
				return base.IsEqualTo(other) && this.IsEqualTo((DataSource.ObjectBody)other);
			}

			// Token: 0x06001FF7 RID: 8183 RVA: 0x000D2820 File Offset: 0x000D0A20
			internal void CompareWith(DataSource.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.Name, other.Name))
				{
					context.RegisterPropertyChange(base.Owner, "Name", typeof(string), base.RenameRequestedThroughAPI ? PropertyFlags.User : PropertyFlags.DdlAndUser, other.Name, this.Name);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Description, other.Description))
				{
					context.RegisterPropertyChange(base.Owner, "Description", typeof(string), PropertyFlags.DdlAndUser, other.Description, this.Description);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ConnectionString, other.ConnectionString))
				{
					context.RegisterPropertyChange(base.Owner, "ConnectionString", typeof(string), PropertyFlags.DdlAndUser, other.ConnectionString, this.ConnectionString);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ImpersonationMode, other.ImpersonationMode))
				{
					context.RegisterPropertyChange(base.Owner, "ImpersonationMode", typeof(ImpersonationMode), PropertyFlags.DdlAndUser, other.ImpersonationMode, this.ImpersonationMode);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Account, other.Account))
				{
					context.RegisterPropertyChange(base.Owner, "Account", typeof(string), PropertyFlags.DdlAndUser, other.Account, this.Account);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Password, other.Password))
				{
					context.RegisterPropertyChange(base.Owner, "Password", typeof(string), PropertyFlags.DdlAndUser, other.Password, this.Password);
				}
				if (!PropertyHelper.AreValuesIdentical(this.MaxConnections, other.MaxConnections))
				{
					context.RegisterPropertyChange(base.Owner, "MaxConnections", typeof(int), PropertyFlags.DdlAndUser, other.MaxConnections, this.MaxConnections);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Isolation, other.Isolation))
				{
					context.RegisterPropertyChange(base.Owner, "Isolation", typeof(DatasourceIsolation), PropertyFlags.DdlAndUser, other.Isolation, this.Isolation);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Timeout, other.Timeout))
				{
					context.RegisterPropertyChange(base.Owner, "Timeout", typeof(int), PropertyFlags.DdlAndUser, other.Timeout, this.Timeout);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Provider, other.Provider))
				{
					context.RegisterPropertyChange(base.Owner, "Provider", typeof(string), PropertyFlags.DdlAndUser, other.Provider, this.Provider);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ConnectionDetails, other.ConnectionDetails))
				{
					context.RegisterPropertyChange(base.Owner, "ConnectionDetails", typeof(string), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.Json, other.ConnectionDetails, this.ConnectionDetails);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Options, other.Options))
				{
					context.RegisterPropertyChange(base.Owner, "Options", typeof(string), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.Json, other.Options, this.Options);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Credential, other.Credential))
				{
					context.RegisterPropertyChange(base.Owner, "Credential", typeof(string), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.Json, other.Credential, this.Credential);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ContextExpression, other.ContextExpression))
				{
					context.RegisterPropertyChange(base.Owner, "ContextExpression", typeof(string), PropertyFlags.DdlAndUser, other.ContextExpression, this.ContextExpression);
				}
				this.ModelID.CompareWith(other.ModelID, "ModelID", "Model", PropertyFlags.ReadOnly, context);
			}

			// Token: 0x06001FF8 RID: 8184 RVA: 0x000D2BFF File Offset: 0x000D0DFF
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.Owner.BeforeBodyCompareWith();
				base.CompareWith(other, context);
				this.CompareWith((DataSource.ObjectBody)other, context);
			}

			// Token: 0x04000801 RID: 2049
			internal string Name;

			// Token: 0x04000802 RID: 2050
			internal string Description;

			// Token: 0x04000803 RID: 2051
			internal DataSourceType Type;

			// Token: 0x04000804 RID: 2052
			internal string ConnectionString;

			// Token: 0x04000805 RID: 2053
			internal ImpersonationMode ImpersonationMode;

			// Token: 0x04000806 RID: 2054
			internal string Account;

			// Token: 0x04000807 RID: 2055
			internal string Password;

			// Token: 0x04000808 RID: 2056
			internal int MaxConnections;

			// Token: 0x04000809 RID: 2057
			internal DatasourceIsolation Isolation;

			// Token: 0x0400080A RID: 2058
			internal int Timeout;

			// Token: 0x0400080B RID: 2059
			internal string Provider;

			// Token: 0x0400080C RID: 2060
			internal DateTime ModifiedTime;

			// Token: 0x0400080D RID: 2061
			internal string ConnectionDetails;

			// Token: 0x0400080E RID: 2062
			internal string Options;

			// Token: 0x0400080F RID: 2063
			internal string Credential;

			// Token: 0x04000810 RID: 2064
			internal string ContextExpression;

			// Token: 0x04000811 RID: 2065
			internal ParentLink<DataSource, Model> ModelID;
		}
	}
}
