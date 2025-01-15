using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000B7 RID: 183
	[CompatibilityRequirement(Pbi = "1400")]
	public sealed class Set : NamedMetadataObject
	{
		// Token: 0x06000B43 RID: 2883 RVA: 0x0005C49F File Offset: 0x0005A69F
		public Set()
		{
			this.InitBodyAndCollections(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0005C4B2 File Offset: 0x0005A6B2
		internal Set(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections(comparer);
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0005C4C4 File Offset: 0x0005A6C4
		private void InitBodyAndCollections(IEqualityComparer<string> comparer)
		{
			this.body = new Set.ObjectBody(this);
			this.body.Name = string.Empty;
			this.body.Description = string.Empty;
			this.body.Expression = string.Empty;
			this.body.State = ObjectState.Ready;
			this.body.ErrorMessage = string.Empty;
			this.body.DisplayFolder = string.Empty;
			this._Annotations = new SetAnnotationCollection(this, comparer);
			this._ExtendedProperties = new SetExtendedPropertyCollection(this, comparer);
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x0005C553 File Offset: 0x0005A753
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.Set;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x0005C557 File Offset: 0x0005A757
		// (set) Token: 0x06000B48 RID: 2888 RVA: 0x0005C569 File Offset: 0x0005A769
		public override MetadataObject Parent
		{
			get
			{
				return this.body.TableID.Object;
			}
			internal set
			{
				if (this.body.TableID.Object != value)
				{
					MetadataObject.UpdateMetadataObjectParent<Set, Table>(this.body.TableID, (Table)value, null, null);
				}
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x0005C596 File Offset: 0x0005A796
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.TableID.ObjectID;
			}
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0005C5A8 File Offset: 0x0005A7A8
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.Set, null, "Set object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
				{
					writer.WriteProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, typeof(string));
				}
				if (writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
				{
					writer.WriteProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, typeof(string));
				}
				if (writer.ShouldIncludeProperty("expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
				{
					writer.WriteProperty("expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, typeof(string));
				}
				if (writer.ShouldIncludeProperty("isDynamic", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("isDynamic", MetadataPropertyNature.RegularProperty, typeof(bool));
				}
				if (writer.ShouldIncludeProperty("isHidden", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("isHidden", MetadataPropertyNature.RegularProperty, typeof(bool));
				}
				if (writer.ShouldIncludeProperty("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
				{
					writer.WriteEnumProperty<ObjectState>("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, PropertyHelper.GetObjectStateCompatibleValues(context.CompatibilityMode, context.DbCompatibilityLevel));
				}
				if (writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				if (writer.ShouldIncludeProperty("structureModifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("structureModifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				if (writer.ShouldIncludeProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
				{
					writer.WriteProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, typeof(string));
				}
				if (writer.ShouldIncludeProperty("displayFolder", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
				{
					writer.WriteProperty("displayFolder", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, typeof(string));
				}
				if (CompatibilityRestrictions.ExtendedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("extendedProperties", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "extendedProperties", MetadataPropertyNature.ChildCollection, ObjectType.ExtendedProperty);
				}
				if (writer.ShouldIncludeProperty("annotations", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "annotations", MetadataPropertyNature.ChildCollection, ObjectType.Annotation);
				}
			}
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0005C7F4 File Offset: 0x0005A9F4
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			requiredLevel = CompatibilityRestrictions.Set[mode];
			requestingPath = ((!CompatibilityRestrictionSet.IsUnbound(requiredLevel)) ? string.Format("[{0}]", this.GetFormattedObjectPath()) : string.Empty);
			int num = requiredLevel;
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x0005C82B File Offset: 0x0005AA2B
		// (set) Token: 0x06000B4D RID: 2893 RVA: 0x0005C833 File Offset: 0x0005AA33
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (Set.ObjectBody)value;
			}
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0005C841 File Offset: 0x0005AA41
		internal override ITxObjectBody CreateBody()
		{
			return new Set.ObjectBody(this);
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0005C849 File Offset: 0x0005AA49
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new Set();
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0005C850 File Offset: 0x0005AA50
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return ((Table)parent).Sets;
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0005C860 File Offset: 0x0005AA60
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			Table table = MetadataObject.ResolveMetadataObjectParentById<Set, Table>(this.body.TableID, objectMap, throwIfCantResolve, null, null);
			if (table != null)
			{
				table.Sets.Add(this);
			}
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0005C891 File Offset: 0x0005AA91
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0005C893 File Offset: 0x0005AA93
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._Annotations;
			yield return this._ExtendedProperties;
			yield break;
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x0005C8A3 File Offset: 0x0005AAA3
		public SetAnnotationCollection Annotations
		{
			get
			{
				return this._Annotations;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x0005C8AB File Offset: 0x0005AAAB
		public SetExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this._ExtendedProperties;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x0005C8B3 File Offset: 0x0005AAB3
		// (set) Token: 0x06000B57 RID: 2903 RVA: 0x0005C8C0 File Offset: 0x0005AAC0
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
					if (!Utils.IsSyntacticallyValidName(value, ObjectType.Set, out text))
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

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x0005C943 File Offset: 0x0005AB43
		// (set) Token: 0x06000B59 RID: 2905 RVA: 0x0005C950 File Offset: 0x0005AB50
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

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x0005C9C0 File Offset: 0x0005ABC0
		// (set) Token: 0x06000B5B RID: 2907 RVA: 0x0005C9D0 File Offset: 0x0005ABD0
		public string Expression
		{
			get
			{
				return this.body.Expression;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Expression, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Expression", typeof(string), this.body.Expression, value);
					string expression = this.body.Expression;
					this.body.Expression = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Expression", typeof(string), expression, value);
				}
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x0005CA40 File Offset: 0x0005AC40
		// (set) Token: 0x06000B5D RID: 2909 RVA: 0x0005CA50 File Offset: 0x0005AC50
		public bool IsDynamic
		{
			get
			{
				return this.body.IsDynamic;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.IsDynamic, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "IsDynamic", typeof(bool), this.body.IsDynamic, value);
					bool isDynamic = this.body.IsDynamic;
					this.body.IsDynamic = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "IsDynamic", typeof(bool), isDynamic, value);
				}
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x0005CAD4 File Offset: 0x0005ACD4
		// (set) Token: 0x06000B5F RID: 2911 RVA: 0x0005CAE4 File Offset: 0x0005ACE4
		public bool IsHidden
		{
			get
			{
				return this.body.IsHidden;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.IsHidden, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "IsHidden", typeof(bool), this.body.IsHidden, value);
					bool isHidden = this.body.IsHidden;
					this.body.IsHidden = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "IsHidden", typeof(bool), isHidden, value);
				}
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x0005CB68 File Offset: 0x0005AD68
		// (set) Token: 0x06000B61 RID: 2913 RVA: 0x0005CB78 File Offset: 0x0005AD78
		public ObjectState State
		{
			get
			{
				return this.body.State;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.State, value))
				{
					CompatibilityRestrictionSet objectStateCompatibilityRestrictions = PropertyHelper.GetObjectStateCompatibilityRestrictions(value);
					CompatibilityRestrictionSet objectStateCompatibilityRestrictions2 = PropertyHelper.GetObjectStateCompatibilityRestrictions(this.body.State);
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					RestrictionsComapreResult restrictionsComapreResult = objectStateCompatibilityRestrictions.Compare(objectStateCompatibilityRestrictions2);
					if (restrictionsComapreResult == RestrictionsComapreResult.MoreRestrictive || restrictionsComapreResult == RestrictionsComapreResult.Incomparable || (restrictionsComapreResult == RestrictionsComapreResult.Equal && value != ObjectState.Ready))
					{
						array = base.ValidateCompatibilityRequirement(objectStateCompatibilityRestrictions, string.Format("[{0}]::[{1}]=[{2}]", this.GetFormattedObjectPath(), "State", value));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "State", typeof(ObjectState), this.body.State, value);
					switch (restrictionsComapreResult)
					{
					case RestrictionsComapreResult.Incomparable:
						base.ResetCompatibilityRequirement();
						base.SetCompatibilityRequirement(objectStateCompatibilityRestrictions, array);
						break;
					case RestrictionsComapreResult.LessRestrictive:
						base.ResetCompatibilityRequirement();
						break;
					case RestrictionsComapreResult.Equal:
						if (array != null)
						{
							base.SetCompatibilityRequirement(objectStateCompatibilityRestrictions, array);
						}
						break;
					case RestrictionsComapreResult.MoreRestrictive:
						base.SetCompatibilityRequirement(objectStateCompatibilityRestrictions, array);
						break;
					}
					ObjectState state = this.body.State;
					this.body.State = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "State", typeof(ObjectState), state, value);
				}
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x0005CC9A File Offset: 0x0005AE9A
		// (set) Token: 0x06000B63 RID: 2915 RVA: 0x0005CCA8 File Offset: 0x0005AEA8
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

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x0005CD2C File Offset: 0x0005AF2C
		// (set) Token: 0x06000B65 RID: 2917 RVA: 0x0005CD3C File Offset: 0x0005AF3C
		public DateTime StructureModifiedTime
		{
			get
			{
				return this.body.StructureModifiedTime;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.StructureModifiedTime, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "StructureModifiedTime", typeof(DateTime), this.body.StructureModifiedTime, value);
					DateTime structureModifiedTime = this.body.StructureModifiedTime;
					this.body.StructureModifiedTime = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "StructureModifiedTime", typeof(DateTime), structureModifiedTime, value);
				}
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x0005CDC0 File Offset: 0x0005AFC0
		// (set) Token: 0x06000B67 RID: 2919 RVA: 0x0005CDD0 File Offset: 0x0005AFD0
		public string ErrorMessage
		{
			get
			{
				return this.body.ErrorMessage;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.ErrorMessage, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "ErrorMessage", typeof(string), this.body.ErrorMessage, value);
					string errorMessage = this.body.ErrorMessage;
					this.body.ErrorMessage = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "ErrorMessage", typeof(string), errorMessage, value);
				}
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x0005CE40 File Offset: 0x0005B040
		// (set) Token: 0x06000B69 RID: 2921 RVA: 0x0005CE50 File Offset: 0x0005B050
		public string DisplayFolder
		{
			get
			{
				return this.body.DisplayFolder;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.DisplayFolder, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "DisplayFolder", typeof(string), this.body.DisplayFolder, value);
					string displayFolder = this.body.DisplayFolder;
					this.body.DisplayFolder = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "DisplayFolder", typeof(string), displayFolder, value);
				}
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x0005CEC0 File Offset: 0x0005B0C0
		// (set) Token: 0x06000B6B RID: 2923 RVA: 0x0005CED4 File Offset: 0x0005B0D4
		public Table Table
		{
			get
			{
				return this.body.TableID.Object;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.TableID.Object, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Table", typeof(Table), this.body.TableID.Object, value);
					Table @object = this.body.TableID.Object;
					this.body.TableID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Table", typeof(Table), @object, value);
				}
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x0005CF58 File Offset: 0x0005B158
		// (set) Token: 0x06000B6D RID: 2925 RVA: 0x0005CF6A File Offset: 0x0005B16A
		internal ObjectId _TableID
		{
			get
			{
				return this.body.TableID.ObjectID;
			}
			set
			{
				this.body.TableID.ObjectID = value;
			}
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0005CF80 File Offset: 0x0005B180
		internal void CopyFrom(Set other, CopyContext context)
		{
			base.CopyFrom(other, context);
			bool flag;
			if ((context.Flags & CopyFlags.IncludeCompatRestictions) == CopyFlags.IncludeCompatRestictions)
			{
				flag = true;
			}
			else if ((context.Flags & CopyFlags.MetadataSync) == CopyFlags.MetadataSync)
			{
				flag = this.body.ModifiedTime.CompareTo(other.body.ModifiedTime) != 0 || this.body.StructureModifiedTime.CompareTo(other.body.StructureModifiedTime) != 0;
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

		// Token: 0x06000B6F RID: 2927 RVA: 0x0005D062 File Offset: 0x0005B262
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((Set)other, context);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0005D071 File Offset: 0x0005B271
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(Set other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0005D08D File Offset: 0x0005B28D
		public void CopyTo(Set other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0005D0A9 File Offset: 0x0005B2A9
		public Set Clone()
		{
			return base.CloneInternal<Set>();
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0005D0B4 File Offset: 0x0005B2B4
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.Set.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object Set is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (this.body.TableID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "TableID", this.body.TableID.Object);
			}
			if (!string.IsNullOrEmpty(this.body.Name))
			{
				writer.WriteProperty<string>(options, "Name", this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.Description))
			{
				writer.WriteProperty<string>(options, "Description", this.body.Description);
			}
			if (!string.IsNullOrEmpty(this.body.Expression))
			{
				writer.WriteProperty<string>(options, "Expression", this.body.Expression);
			}
			if (this.body.IsDynamic)
			{
				writer.WriteProperty<bool>(options, "IsDynamic", this.body.IsDynamic);
			}
			if (this.body.IsHidden)
			{
				writer.WriteProperty<bool>(options, "IsHidden", this.body.IsHidden);
			}
			if (!string.IsNullOrEmpty(this.body.DisplayFolder))
			{
				writer.WriteProperty<string>(options, "DisplayFolder", this.body.DisplayFolder);
			}
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0005D21C File Offset: 0x0005B41C
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("TableID", out objectId))
			{
				this.body.TableID.ObjectID = objectId;
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
			string text3;
			if (reader.TryReadProperty<string>("Expression", out text3))
			{
				this.body.Expression = text3;
			}
			bool flag;
			if (reader.TryReadProperty<bool>("IsDynamic", out flag))
			{
				this.body.IsDynamic = flag;
			}
			bool flag2;
			if (reader.TryReadProperty<bool>("IsHidden", out flag2))
			{
				this.body.IsHidden = flag2;
			}
			ObjectState objectState;
			if (reader.TryReadProperty<ObjectState>("State", out objectState))
			{
				this.body.State = objectState;
			}
			DateTime dateTime;
			if (reader.TryReadProperty<DateTime>("ModifiedTime", out dateTime))
			{
				this.body.ModifiedTime = dateTime;
			}
			DateTime dateTime2;
			if (reader.TryReadProperty<DateTime>("StructureModifiedTime", out dateTime2))
			{
				this.body.StructureModifiedTime = dateTime2;
			}
			string text4;
			if (reader.TryReadProperty<string>("ErrorMessage", out text4))
			{
				this.body.ErrorMessage = text4;
			}
			string text5;
			if (reader.TryReadProperty<string>("DisplayFolder", out text5))
			{
				this.body.DisplayFolder = text5;
			}
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0005D368 File Offset: 0x0005B568
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.Set.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object Set is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataBodyProperties(context, writer);
			if (this.body.TableID.Object != null && writer.ShouldIncludeProperty("TableID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
			{
				writer.WriteObjectIdProperty("TableID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly, this.body.TableID.Object);
			}
			if (!string.IsNullOrEmpty(this.body.Name) && writer.ShouldIncludeProperty("Name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("Name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, this.body.Description);
			}
			if (!string.IsNullOrEmpty(this.body.Expression) && writer.ShouldIncludeProperty("Expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("Expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.body.Expression);
			}
			if (this.body.IsDynamic && writer.ShouldIncludeProperty("IsDynamic", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("IsDynamic", MetadataPropertyNature.RegularProperty, this.body.IsDynamic);
			}
			if (this.body.IsHidden && writer.ShouldIncludeProperty("IsHidden", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("IsHidden", MetadataPropertyNature.RegularProperty, this.body.IsHidden);
			}
			if (!string.IsNullOrEmpty(this.body.DisplayFolder) && writer.ShouldIncludeProperty("DisplayFolder", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("DisplayFolder", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.DisplayFolder);
			}
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0005D568 File Offset: 0x0005B768
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.Set.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object Set is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataTree(context, writer);
			if (!string.IsNullOrEmpty(this.body.Name) && writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, this.body.Description);
			}
			if (!string.IsNullOrEmpty(this.body.Expression) && writer.ShouldIncludeProperty("expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.body.Expression);
			}
			if (this.body.IsDynamic && writer.ShouldIncludeProperty("isDynamic", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("isDynamic", MetadataPropertyNature.RegularProperty, this.body.IsDynamic);
			}
			if (this.body.IsHidden && writer.ShouldIncludeProperty("isHidden", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("isHidden", MetadataPropertyNature.RegularProperty, this.body.IsHidden);
			}
			if (this.body.State != ObjectState.Ready)
			{
				if (!PropertyHelper.IsObjectStateValueCompatible(this.body.State, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member State is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
				{
					writer.WriteEnumProperty<ObjectState>("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, this.body.State);
				}
			}
			if (this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.ModifiedTime);
			}
			if (this.body.StructureModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("structureModifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("structureModifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.StructureModifiedTime);
			}
			if (!string.IsNullOrEmpty(this.body.ErrorMessage) && writer.ShouldIncludeProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
			{
				writer.WriteStringProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, this.body.ErrorMessage);
			}
			if (!string.IsNullOrEmpty(this.body.DisplayFolder) && writer.ShouldIncludeProperty("displayFolder", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("displayFolder", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.DisplayFolder);
			}
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

		// Token: 0x06000B77 RID: 2935 RVA: 0x0005D914 File Offset: 0x0005BB14
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
					if (c != 'N')
					{
						if (c != 'n')
						{
							break;
						}
						if (!(propertyName == "name"))
						{
							break;
						}
					}
					else if (!(propertyName == "Name"))
					{
						break;
					}
					this.body.Name = reader.ReadStringProperty();
					return true;
				}
				case 5:
				{
					char c = propertyName[0];
					if (c != 'S')
					{
						if (c != 's')
						{
							break;
						}
						if (!(propertyName == "state"))
						{
							break;
						}
					}
					else if (!(propertyName == "State"))
					{
						break;
					}
					ObjectState objectState = reader.ReadEnumProperty<ObjectState>();
					if (!PropertyHelper.IsObjectStateValueCompatible(objectState, context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatiblePropertyValue;
						return false;
					}
					this.body.State = objectState;
					return true;
				}
				case 7:
					if (propertyName == "TableID")
					{
						this.body.TableID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				case 8:
				{
					char c = propertyName[0];
					if (c != 'I')
					{
						if (c != 'i')
						{
							break;
						}
						if (!(propertyName == "isHidden"))
						{
							break;
						}
					}
					else if (!(propertyName == "IsHidden"))
					{
						break;
					}
					this.body.IsHidden = reader.ReadBooleanProperty();
					return true;
				}
				case 9:
				{
					char c = propertyName[0];
					if (c != 'I')
					{
						if (c != 'i')
						{
							break;
						}
						if (!(propertyName == "isDynamic"))
						{
							break;
						}
					}
					else if (!(propertyName == "IsDynamic"))
					{
						break;
					}
					this.body.IsDynamic = reader.ReadBooleanProperty();
					return true;
				}
				case 10:
				{
					char c = propertyName[0];
					if (c != 'E')
					{
						if (c != 'e')
						{
							break;
						}
						if (!(propertyName == "expression"))
						{
							break;
						}
					}
					else if (!(propertyName == "Expression"))
					{
						break;
					}
					this.body.Expression = reader.ReadStringProperty();
					return true;
				}
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
					if (c <= 'M')
					{
						if (c != 'E')
						{
							if (c != 'M')
							{
								break;
							}
							if (!(propertyName == "ModifiedTime"))
							{
								break;
							}
						}
						else
						{
							if (!(propertyName == "ErrorMessage"))
							{
								break;
							}
							goto IL_044B;
						}
					}
					else if (c != 'e')
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
					else
					{
						if (!(propertyName == "errorMessage"))
						{
							break;
						}
						goto IL_044B;
					}
					this.body.ModifiedTime = reader.ReadDateTimeProperty();
					return true;
					IL_044B:
					this.body.ErrorMessage = reader.ReadStringProperty();
					return true;
				}
				case 13:
				{
					char c = propertyName[0];
					if (c != 'D')
					{
						if (c != 'd')
						{
							break;
						}
						if (!(propertyName == "displayFolder"))
						{
							break;
						}
					}
					else if (!(propertyName == "DisplayFolder"))
					{
						break;
					}
					this.body.DisplayFolder = reader.ReadStringProperty();
					return true;
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
				case 21:
				{
					char c = propertyName[0];
					if (c != 'S')
					{
						if (c != 's')
						{
							break;
						}
						if (!(propertyName == "structureModifiedTime"))
						{
							break;
						}
					}
					else if (!(propertyName == "StructureModifiedTime"))
					{
						break;
					}
					this.body.StructureModifiedTime = reader.ReadDateTimeProperty();
					return true;
				}
				}
			}
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0005DF10 File Offset: 0x0005C110
		[Obsolete("Deprecated. Use RequestRename method instead.", false)]
		public void Rename(string newName)
		{
			this.RequestRename(newName);
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0005DF19 File Offset: 0x0005C119
		public void RequestRename(string newName)
		{
			ObjectChangeTracker.RegisterObjectRenaming(this);
			this.Name = newName;
			this.body.RenameRequestedThroughAPI = true;
			ObjectChangeTracker.RegisterObjectRenamed(this);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0005DF3C File Offset: 0x0005C13C
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.Set.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object Set is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			if (!string.IsNullOrEmpty(this.body.Name))
			{
				result["name", TomPropCategory.Name, 2, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Name, SplitMultilineOptions.None);
			}
			if (!string.IsNullOrEmpty(this.body.Description))
			{
				result["description", TomPropCategory.Regular, 3, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Description, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.Expression))
			{
				result["expression", TomPropCategory.Regular, 4, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Expression, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.IsDynamic)
			{
				result["isDynamic", TomPropCategory.Regular, 5, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<bool>(this.body.IsDynamic);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.IsHidden)
			{
				result["isHidden", TomPropCategory.Regular, 6, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<bool>(this.body.IsHidden);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreInferredProperties && this.body.State != ObjectState.Ready)
			{
				if (!PropertyHelper.IsObjectStateValueCompatible(this.body.State, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member State is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["state", TomPropCategory.Regular, 7, true] = JsonPropertyHelper.ConvertEnumToJsonValue<ObjectState>(this.body.State);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["modifiedTime", TomPropCategory.Regular, 8, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.ModifiedTime);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.StructureModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["structureModifiedTime", TomPropCategory.Regular, 9, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.StructureModifiedTime);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreInferredProperties && !string.IsNullOrEmpty(this.body.ErrorMessage))
			{
				result["errorMessage", TomPropCategory.Regular, 10, true] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.ErrorMessage, SplitMultilineOptions.None);
			}
			if (!string.IsNullOrEmpty(this.body.DisplayFolder))
			{
				result["displayFolder", TomPropCategory.Regular, 11, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.DisplayFolder, SplitMultilineOptions.None);
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

		// Token: 0x06000B7B RID: 2939 RVA: 0x0005E3F0 File Offset: 0x0005C5F0
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name != null)
			{
				switch (name.Length)
				{
				case 4:
					if (name == "name")
					{
						this.body.Name = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				case 5:
					if (name == "state")
					{
						ObjectState objectState = JsonPropertyHelper.ConvertJsonValueToEnum<ObjectState>(jsonProp.Value);
						if (jsonProp.Value.Type != 10 && !PropertyHelper.IsObjectStateValueCompatible(objectState, mode, dbCompatibilityLevel))
						{
							return false;
						}
						this.body.State = objectState;
						return true;
					}
					break;
				case 8:
					if (name == "isHidden")
					{
						this.body.IsHidden = JsonPropertyHelper.ConvertJsonValueToPrimitive<bool>(jsonProp.Value);
						return true;
					}
					break;
				case 9:
					if (name == "isDynamic")
					{
						this.body.IsDynamic = JsonPropertyHelper.ConvertJsonValueToPrimitive<bool>(jsonProp.Value);
						return true;
					}
					break;
				case 10:
					if (name == "expression")
					{
						this.body.Expression = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
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
				{
					char c = name[0];
					if (c != 'e')
					{
						if (c == 'm')
						{
							if (name == "modifiedTime")
							{
								this.body.ModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
								return true;
							}
						}
					}
					else if (name == "errorMessage")
					{
						this.body.ErrorMessage = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				}
				case 13:
					if (name == "displayFolder")
					{
						this.body.DisplayFolder = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				case 18:
					if (name == "extendedProperties")
					{
						if (!JsonPropertyHelper.IsEmptyObjectCollection(jsonProp.Value) && !CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
						{
							return false;
						}
						JsonPropertyHelper.ReadObjectCollection(this.ExtendedProperties, jsonProp.Value, options, mode, dbCompatibilityLevel);
						return true;
					}
					break;
				case 21:
					if (name == "structureModifiedTime")
					{
						this.body.StructureModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
						return true;
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0005E6F1 File Offset: 0x0005C8F1
		internal override IEnumerable<MetadataObject> GetNameLinkedObjects(string objectName = null)
		{
			if (objectName == null)
			{
				objectName = this.Name;
			}
			if (this.Table == null || base.Model == null)
			{
				yield break;
			}
			foreach (Perspective perspective in base.Model.Perspectives)
			{
				PerspectiveTable perspectiveTable = perspective.PerspectiveTables.Find(this.Table.Name);
				if (perspectiveTable != null)
				{
					PerspectiveSet perspectiveSet = perspectiveTable.PerspectiveSets.Find(objectName);
					if (perspectiveSet != null)
					{
						yield return perspectiveSet;
					}
				}
			}
			IEnumerator<Perspective> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0005E708 File Offset: 0x0005C908
		internal override string GetFormattedObjectPath()
		{
			if (this.Table != null)
			{
				return TomSR.ObjectPath_Set_2Args(this.Table.Name, this.Name);
			}
			return TomSR.ObjectPath_Set_1Arg(this.Name);
		}

		// Token: 0x0400016F RID: 367
		internal Set.ObjectBody body;

		// Token: 0x04000170 RID: 368
		private SetAnnotationCollection _Annotations;

		// Token: 0x04000171 RID: 369
		private SetExtendedPropertyCollection _ExtendedProperties;

		// Token: 0x020002CA RID: 714
		internal class ObjectBody : NamedMetadataObjectBody<Set>
		{
			// Token: 0x060022EE RID: 8942 RVA: 0x000DF6DB File Offset: 0x000DD8DB
			public ObjectBody(Set owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.StructureModifiedTime = DateTime.MinValue;
				this.TableID = new ParentLink<Set, Table>(owner, "Table");
			}

			// Token: 0x060022EF RID: 8943 RVA: 0x000DF70B File Offset: 0x000DD90B
			public override string GetObjectName()
			{
				return this.Name;
			}

			// Token: 0x060022F0 RID: 8944 RVA: 0x000DF714 File Offset: 0x000DD914
			internal bool IsEqualTo(Set.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.Expression, other.Expression) && PropertyHelper.AreValuesIdentical(this.IsDynamic, other.IsDynamic) && PropertyHelper.AreValuesIdentical(this.IsHidden, other.IsHidden) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.State, other.State)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.StructureModifiedTime, other.StructureModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ErrorMessage, other.ErrorMessage)) && PropertyHelper.AreValuesIdentical(this.DisplayFolder, other.DisplayFolder) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.TableID.IsEqualTo(other.TableID, context));
			}

			// Token: 0x060022F1 RID: 8945 RVA: 0x000DF86C File Offset: 0x000DDA6C
			internal void CopyFromImpl(Set.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				base.Owner.Name = other.Name;
				this.Description = other.Description;
				this.Expression = other.Expression;
				this.IsDynamic = other.IsDynamic;
				this.IsHidden = other.IsHidden;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.State = other.State;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModifiedTime = other.ModifiedTime;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.StructureModifiedTime = other.StructureModifiedTime;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ErrorMessage = other.ErrorMessage;
				}
				this.DisplayFolder = other.DisplayFolder;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.TableID.CopyFrom(other.TableID, context);
				}
			}

			// Token: 0x060022F2 RID: 8946 RVA: 0x000DF97C File Offset: 0x000DDB7C
			internal void CopyFromImpl(Set.ObjectBody other)
			{
				this.Name = other.Name;
				this.Description = other.Description;
				this.Expression = other.Expression;
				this.IsDynamic = other.IsDynamic;
				this.IsHidden = other.IsHidden;
				this.State = other.State;
				this.ModifiedTime = other.ModifiedTime;
				this.StructureModifiedTime = other.StructureModifiedTime;
				this.ErrorMessage = other.ErrorMessage;
				this.DisplayFolder = other.DisplayFolder;
				this.TableID.CopyFrom(other.TableID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x060022F3 RID: 8947 RVA: 0x000DFA17 File Offset: 0x000DDC17
			public override void CopyFrom(MetadataObjectBody<Set> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((Set.ObjectBody)other, context);
			}

			// Token: 0x060022F4 RID: 8948 RVA: 0x000DFA30 File Offset: 0x000DDC30
			internal bool IsEqualTo(Set.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.Expression, other.Expression) && PropertyHelper.AreValuesIdentical(this.IsDynamic, other.IsDynamic) && PropertyHelper.AreValuesIdentical(this.IsHidden, other.IsHidden) && PropertyHelper.AreValuesIdentical(this.State, other.State) && PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && PropertyHelper.AreValuesIdentical(this.StructureModifiedTime, other.StructureModifiedTime) && PropertyHelper.AreValuesIdentical(this.ErrorMessage, other.ErrorMessage) && PropertyHelper.AreValuesIdentical(this.DisplayFolder, other.DisplayFolder) && this.TableID.IsEqualTo(other.TableID);
			}

			// Token: 0x060022F5 RID: 8949 RVA: 0x000DFB25 File Offset: 0x000DDD25
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((Set.ObjectBody)other);
			}

			// Token: 0x060022F6 RID: 8950 RVA: 0x000DFB40 File Offset: 0x000DDD40
			internal void CompareWith(Set.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.Name, other.Name))
				{
					context.RegisterPropertyChange(base.Owner, "Name", typeof(string), base.RenameRequestedThroughAPI ? PropertyFlags.User : PropertyFlags.DdlAndUser, other.Name, this.Name);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Description, other.Description))
				{
					context.RegisterPropertyChange(base.Owner, "Description", typeof(string), PropertyFlags.DdlAndUser, other.Description, this.Description);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Expression, other.Expression))
				{
					context.RegisterPropertyChange(base.Owner, "Expression", typeof(string), PropertyFlags.DdlAndUser, other.Expression, this.Expression);
				}
				if (!PropertyHelper.AreValuesIdentical(this.IsDynamic, other.IsDynamic))
				{
					context.RegisterPropertyChange(base.Owner, "IsDynamic", typeof(bool), PropertyFlags.DdlAndUser, other.IsDynamic, this.IsDynamic);
				}
				if (!PropertyHelper.AreValuesIdentical(this.IsHidden, other.IsHidden))
				{
					context.RegisterPropertyChange(base.Owner, "IsHidden", typeof(bool), PropertyFlags.DdlAndUser, other.IsHidden, this.IsHidden);
				}
				if (!PropertyHelper.AreValuesIdentical(this.State, other.State))
				{
					context.RegisterPropertyChange(base.Owner, "State", typeof(ObjectState), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.State, this.State);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				if (!PropertyHelper.AreValuesIdentical(this.StructureModifiedTime, other.StructureModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "StructureModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.StructureModifiedTime, this.StructureModifiedTime);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ErrorMessage, other.ErrorMessage))
				{
					context.RegisterPropertyChange(base.Owner, "ErrorMessage", typeof(string), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ErrorMessage, this.ErrorMessage);
				}
				if (!PropertyHelper.AreValuesIdentical(this.DisplayFolder, other.DisplayFolder))
				{
					context.RegisterPropertyChange(base.Owner, "DisplayFolder", typeof(string), PropertyFlags.DdlAndUser, other.DisplayFolder, this.DisplayFolder);
				}
				this.TableID.CompareWith(other.TableID, "TableID", "Table", PropertyFlags.ReadOnly, context);
			}

			// Token: 0x060022F7 RID: 8951 RVA: 0x000DFDF5 File Offset: 0x000DDFF5
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((Set.ObjectBody)other, context);
			}

			// Token: 0x04000A28 RID: 2600
			internal string Name;

			// Token: 0x04000A29 RID: 2601
			internal string Description;

			// Token: 0x04000A2A RID: 2602
			internal string Expression;

			// Token: 0x04000A2B RID: 2603
			internal bool IsDynamic;

			// Token: 0x04000A2C RID: 2604
			internal bool IsHidden;

			// Token: 0x04000A2D RID: 2605
			internal ObjectState State;

			// Token: 0x04000A2E RID: 2606
			internal DateTime ModifiedTime;

			// Token: 0x04000A2F RID: 2607
			internal DateTime StructureModifiedTime;

			// Token: 0x04000A30 RID: 2608
			internal string ErrorMessage;

			// Token: 0x04000A31 RID: 2609
			internal string DisplayFolder;

			// Token: 0x04000A32 RID: 2610
			internal ParentLink<Set, Table> TableID;
		}
	}
}
