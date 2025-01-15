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
	// Token: 0x0200005C RID: 92
	[EditorBrowsable(EditorBrowsableState.Never)]
	[CompatibilityRequirement("Internal")]
	public sealed class Function : NamedMetadataObject, IMetadataObjectWithLineage
	{
		// Token: 0x060004C9 RID: 1225 RVA: 0x0002462D File Offset: 0x0002282D
		public Function()
		{
			this.InitBodyAndCollections(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00024640 File Offset: 0x00022840
		internal Function(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections(comparer);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00024650 File Offset: 0x00022850
		private void InitBodyAndCollections(IEqualityComparer<string> comparer)
		{
			this.body = new Function.ObjectBody(this);
			this.body.Name = string.Empty;
			this.body.Description = string.Empty;
			this.body.Expression = string.Empty;
			this.body.State = ObjectState.Ready;
			this.body.ErrorMessage = string.Empty;
			this.body.LineageTag = string.Empty;
			this.body.SourceLineageTag = string.Empty;
			this._Annotations = new FunctionAnnotationCollection(this, comparer);
			this._ExtendedProperties = new FunctionExtendedPropertyCollection(this, comparer);
			this._ChangedProperties = new FunctionChangedPropertyCollection(this);
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x000246FB File Offset: 0x000228FB
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.Function;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x000246FF File Offset: 0x000228FF
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x00024711 File Offset: 0x00022911
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
					MetadataObject.UpdateMetadataObjectParent<Function, Model>(this.body.ModelID, (Model)value, null, null);
				}
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0002473E File Offset: 0x0002293E
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.ModelID.ObjectID;
			}
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00024750 File Offset: 0x00022950
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.Function, null, "Function object of Tabular Object Model (TOM)", new bool?(false)))
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
				if (writer.ShouldIncludeProperty("isHidden", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("isHidden", MetadataPropertyNature.RegularProperty, typeof(bool));
				}
				if (writer.ShouldIncludeProperty("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
				{
					writer.WriteEnumProperty<ObjectState>("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, PropertyHelper.GetObjectStateCompatibleValues(context.CompatibilityMode, context.DbCompatibilityLevel));
				}
				if (writer.ShouldIncludeProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
				{
					writer.WriteProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, typeof(string));
				}
				if (writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				if (writer.ShouldIncludeProperty("structureModifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("structureModifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				if (writer.ShouldIncludeProperty("lineageTag", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("lineageTag", MetadataPropertyNature.RegularProperty, typeof(string));
				}
				if (writer.ShouldIncludeProperty("sourceLineageTag", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("sourceLineageTag", MetadataPropertyNature.RegularProperty, typeof(string));
				}
				if (CompatibilityRestrictions.ExtendedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("extendedProperties", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "extendedProperties", MetadataPropertyNature.ChildCollection, ObjectType.ExtendedProperty);
				}
				if (CompatibilityRestrictions.ChangedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("changedProperties", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "changedProperties", MetadataPropertyNature.ChildCollection, ObjectType.ChangedProperty);
				}
				if (writer.ShouldIncludeProperty("annotations", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "annotations", MetadataPropertyNature.ChildCollection, ObjectType.Annotation);
				}
			}
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x000249C8 File Offset: 0x00022BC8
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			requiredLevel = CompatibilityRestrictions.Function[mode];
			requestingPath = ((!CompatibilityRestrictionSet.IsUnbound(requiredLevel)) ? string.Format("[{0}]", this.GetFormattedObjectPath()) : string.Empty);
			int num = requiredLevel;
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x000249FF File Offset: 0x00022BFF
		// (set) Token: 0x060004D3 RID: 1235 RVA: 0x00024A07 File Offset: 0x00022C07
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (Function.ObjectBody)value;
			}
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00024A15 File Offset: 0x00022C15
		internal override ITxObjectBody CreateBody()
		{
			return new Function.ObjectBody(this);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00024A1D File Offset: 0x00022C1D
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new Function();
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00024A24 File Offset: 0x00022C24
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return ((Model)parent).Functions;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00024A34 File Offset: 0x00022C34
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			Model model = MetadataObject.ResolveMetadataObjectParentById<Function, Model>(this.body.ModelID, objectMap, throwIfCantResolve, null, null);
			if (model != null)
			{
				model.Functions.Add(this);
			}
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00024A65 File Offset: 0x00022C65
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00024A67 File Offset: 0x00022C67
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._Annotations;
			yield return this._ExtendedProperties;
			yield return this._ChangedProperties;
			yield break;
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00024A77 File Offset: 0x00022C77
		public FunctionAnnotationCollection Annotations
		{
			get
			{
				return this._Annotations;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00024A7F File Offset: 0x00022C7F
		public FunctionExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this._ExtendedProperties;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x00024A87 File Offset: 0x00022C87
		public FunctionChangedPropertyCollection ChangedProperties
		{
			get
			{
				return this._ChangedProperties;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x00024A8F File Offset: 0x00022C8F
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x00024A9C File Offset: 0x00022C9C
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
					if (!Utils.IsSyntacticallyValidName(value, ObjectType.Function, out text))
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

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x00024B1F File Offset: 0x00022D1F
		// (set) Token: 0x060004E0 RID: 1248 RVA: 0x00024B2C File Offset: 0x00022D2C
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

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x00024B9C File Offset: 0x00022D9C
		// (set) Token: 0x060004E2 RID: 1250 RVA: 0x00024BAC File Offset: 0x00022DAC
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

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x00024C1C File Offset: 0x00022E1C
		// (set) Token: 0x060004E4 RID: 1252 RVA: 0x00024C2C File Offset: 0x00022E2C
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

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00024CB0 File Offset: 0x00022EB0
		// (set) Token: 0x060004E6 RID: 1254 RVA: 0x00024CC0 File Offset: 0x00022EC0
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

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x00024DE2 File Offset: 0x00022FE2
		// (set) Token: 0x060004E8 RID: 1256 RVA: 0x00024DF0 File Offset: 0x00022FF0
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

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x00024E60 File Offset: 0x00023060
		// (set) Token: 0x060004EA RID: 1258 RVA: 0x00024E70 File Offset: 0x00023070
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

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x00024EF4 File Offset: 0x000230F4
		// (set) Token: 0x060004EC RID: 1260 RVA: 0x00024F04 File Offset: 0x00023104
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

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x00024F88 File Offset: 0x00023188
		// (set) Token: 0x060004EE RID: 1262 RVA: 0x00024F98 File Offset: 0x00023198
		public string LineageTag
		{
			get
			{
				return this.body.LineageTag;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.LineageTag, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "LineageTag", typeof(string), this.body.LineageTag, value);
					string lineageTag = this.body.LineageTag;
					this.body.LineageTag = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "LineageTag", typeof(string), lineageTag, value);
				}
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x00025008 File Offset: 0x00023208
		// (set) Token: 0x060004F0 RID: 1264 RVA: 0x00025018 File Offset: 0x00023218
		public string SourceLineageTag
		{
			get
			{
				return this.body.SourceLineageTag;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.SourceLineageTag, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "SourceLineageTag", typeof(string), this.body.SourceLineageTag, value);
					string sourceLineageTag = this.body.SourceLineageTag;
					this.body.SourceLineageTag = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "SourceLineageTag", typeof(string), sourceLineageTag, value);
				}
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00025088 File Offset: 0x00023288
		// (set) Token: 0x060004F2 RID: 1266 RVA: 0x0002509A File Offset: 0x0002329A
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

		// Token: 0x060004F3 RID: 1267 RVA: 0x000250B0 File Offset: 0x000232B0
		internal void CopyFrom(Function other, CopyContext context)
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
				this.ChangedProperties.CopyFrom(other.ChangedProperties, context);
			}
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x000251A4 File Offset: 0x000233A4
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((Function)other, context);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x000251B3 File Offset: 0x000233B3
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(Function other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x000251CF File Offset: 0x000233CF
		public void CopyTo(Function other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x000251EB File Offset: 0x000233EB
		public Function Clone()
		{
			return base.CloneInternal<Function>();
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x000251F4 File Offset: 0x000233F4
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.Function.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object Function is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
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
			if (this.body.IsHidden)
			{
				writer.WriteProperty<bool>(options, "IsHidden", this.body.IsHidden);
			}
			if (!string.IsNullOrEmpty(this.body.LineageTag))
			{
				writer.WriteProperty<string>(options, "LineageTag", this.body.LineageTag);
			}
			if (!string.IsNullOrEmpty(this.body.SourceLineageTag))
			{
				writer.WriteProperty<string>(options, "SourceLineageTag", this.body.SourceLineageTag);
			}
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00025330 File Offset: 0x00023530
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
			string text3;
			if (reader.TryReadProperty<string>("Expression", out text3))
			{
				this.body.Expression = text3;
			}
			bool flag;
			if (reader.TryReadProperty<bool>("IsHidden", out flag))
			{
				this.body.IsHidden = flag;
			}
			ObjectState objectState;
			if (reader.TryReadProperty<ObjectState>("State", out objectState))
			{
				this.body.State = objectState;
			}
			string text4;
			if (reader.TryReadProperty<string>("ErrorMessage", out text4))
			{
				this.body.ErrorMessage = text4;
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
			string text5;
			if (reader.TryReadProperty<string>("LineageTag", out text5))
			{
				this.body.LineageTag = text5;
			}
			string text6;
			if (reader.TryReadProperty<string>("SourceLineageTag", out text6))
			{
				this.body.SourceLineageTag = text6;
			}
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0002547C File Offset: 0x0002367C
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.Function.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object Function is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataBodyProperties(context, writer);
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
			if (this.body.IsHidden && writer.ShouldIncludeProperty("IsHidden", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("IsHidden", MetadataPropertyNature.RegularProperty, this.body.IsHidden);
			}
			if (!string.IsNullOrEmpty(this.body.LineageTag) && writer.ShouldIncludeProperty("LineageTag", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("LineageTag", MetadataPropertyNature.RegularProperty, this.body.LineageTag);
			}
			if (!string.IsNullOrEmpty(this.body.SourceLineageTag) && writer.ShouldIncludeProperty("SourceLineageTag", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("SourceLineageTag", MetadataPropertyNature.RegularProperty, this.body.SourceLineageTag);
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00025634 File Offset: 0x00023834
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.Function.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object Function is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
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
			if (!string.IsNullOrEmpty(this.body.ErrorMessage) && writer.ShouldIncludeProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
			{
				writer.WriteStringProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, this.body.ErrorMessage);
			}
			if (this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.ModifiedTime);
			}
			if (this.body.StructureModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("structureModifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("structureModifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.StructureModifiedTime);
			}
			if (!string.IsNullOrEmpty(this.body.LineageTag) && writer.ShouldIncludeProperty("lineageTag", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("lineageTag", MetadataPropertyNature.RegularProperty, this.body.LineageTag);
			}
			if (!string.IsNullOrEmpty(this.body.SourceLineageTag) && writer.ShouldIncludeProperty("sourceLineageTag", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("sourceLineageTag", MetadataPropertyNature.RegularProperty, this.body.SourceLineageTag);
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
			if (this.ChangedProperties.Count > 0)
			{
				if (!CompatibilityRestrictions.ChangedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("A child ChangedProperty is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("changedProperties", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "changedProperties", MetadataPropertyNature.ChildCollection, this.ChangedProperties);
				}
			}
			if (this.Annotations.Count > 0 && writer.ShouldIncludeProperty("annotations", MetadataPropertyNature.ChildCollection))
			{
				writer.WriteChildCollection(context, "annotations", MetadataPropertyNature.ChildCollection, this.Annotations);
			}
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00025A50 File Offset: 0x00023C50
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
					if (propertyName == "ModelID")
					{
						this.body.ModelID.ObjectID = reader.ReadObjectIdProperty();
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
				case 10:
				{
					char c = propertyName[0];
					if (c <= 'L')
					{
						if (c != 'E')
						{
							if (c != 'L')
							{
								break;
							}
							if (!(propertyName == "LineageTag"))
							{
								break;
							}
							goto IL_045D;
						}
						else if (!(propertyName == "Expression"))
						{
							break;
						}
					}
					else if (c != 'e')
					{
						if (c != 'l')
						{
							break;
						}
						if (!(propertyName == "lineageTag"))
						{
							break;
						}
						goto IL_045D;
					}
					else if (!(propertyName == "expression"))
					{
						break;
					}
					this.body.Expression = reader.ReadStringProperty();
					return true;
					IL_045D:
					this.body.LineageTag = reader.ReadStringProperty();
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
							goto IL_0437;
						}
						else if (!(propertyName == "ErrorMessage"))
						{
							break;
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
						goto IL_0437;
					}
					else if (!(propertyName == "errorMessage"))
					{
						break;
					}
					this.body.ErrorMessage = reader.ReadStringProperty();
					return true;
					IL_0437:
					this.body.ModifiedTime = reader.ReadDateTimeProperty();
					return true;
				}
				case 16:
				{
					char c = propertyName[0];
					if (c != 'S')
					{
						if (c != 's')
						{
							break;
						}
						if (!(propertyName == "sourceLineageTag"))
						{
							break;
						}
					}
					else if (!(propertyName == "SourceLineageTag"))
					{
						break;
					}
					this.body.SourceLineageTag = reader.ReadStringProperty();
					return true;
				}
				case 17:
					if (propertyName == "changedProperties")
					{
						if (!CompatibilityRestrictions.ChangedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							classification = UnexpectedPropertyClassification.IncompatibleProperty;
							return false;
						}
						using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
						{
							foreach (ChangedProperty changedProperty in reader.ReadChildCollectionProperty<ChangedProperty>(context))
							{
								try
								{
									this.ChangedProperties.Add(changedProperty);
								}
								catch (Exception ex2)
								{
									throw reader.CreateInvalidChildException(context, changedProperty, TomSR.Exception_FailedAddDeserializedObject("ChangedProperty", ex2.Message), ex2);
								}
							}
						}
						return true;
					}
					break;
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
								catch (Exception ex3)
								{
									throw reader.CreateInvalidChildException(context, extendedProperty, TomSR.Exception_FailedAddDeserializedNamedObject("ExtendedProperty", (extendedProperty != null) ? extendedProperty.Name : null, ex3.Message), ex3);
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

		// Token: 0x060004FD RID: 1277 RVA: 0x00026118 File Offset: 0x00024318
		[Obsolete("Deprecated. Use RequestRename method instead.", false)]
		public void Rename(string newName)
		{
			this.RequestRename(newName);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00026121 File Offset: 0x00024321
		public void RequestRename(string newName)
		{
			ObjectChangeTracker.RegisterObjectRenaming(this);
			this.Name = newName;
			this.body.RenameRequestedThroughAPI = true;
			ObjectChangeTracker.RegisterObjectRenamed(this);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00026144 File Offset: 0x00024344
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.Function.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object Function is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
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
			if (!options.IncludeTranslatablePropertiesOnly && this.body.IsHidden)
			{
				result["isHidden", TomPropCategory.Regular, 5, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<bool>(this.body.IsHidden);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreInferredProperties && this.body.State != ObjectState.Ready)
			{
				if (!PropertyHelper.IsObjectStateValueCompatible(this.body.State, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member State is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["state", TomPropCategory.Regular, 6, true] = JsonPropertyHelper.ConvertEnumToJsonValue<ObjectState>(this.body.State);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreInferredProperties && !string.IsNullOrEmpty(this.body.ErrorMessage))
			{
				result["errorMessage", TomPropCategory.Regular, 7, true] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.ErrorMessage, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["modifiedTime", TomPropCategory.Regular, 8, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.ModifiedTime);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.StructureModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["structureModifiedTime", TomPropCategory.Regular, 9, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.StructureModifiedTime);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.LineageTag))
			{
				result["lineageTag", TomPropCategory.Regular, 10, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.LineageTag, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.SourceLineageTag))
			{
				result["sourceLineageTag", TomPropCategory.Regular, 11, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.SourceLineageTag, SplitMultilineOptions.None);
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations)
			{
				if (!options.IncludeTranslatablePropertiesOnly)
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
				if (!options.IncludeTranslatablePropertiesOnly)
				{
					IEnumerable<ChangedProperty> enumerable3;
					if (!options.IgnoreInferredObjects)
					{
						IEnumerable<ChangedProperty> changedProperties = this.ChangedProperties;
						enumerable3 = changedProperties;
					}
					else
					{
						enumerable3 = this.ChangedProperties.Where((ChangedProperty o) => !ObjectTreeHelper.IsInferredObject(o));
					}
					IEnumerable<ChangedProperty> enumerable4 = enumerable3;
					if (enumerable4.Any<ChangedProperty>())
					{
						if (!CompatibilityRestrictions.ChangedProperty.IsCompatible(mode, dbCompatibilityLevel))
						{
							throw TomInternalException.Create("A child ChangedProperty is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
						}
						object[] array = enumerable4.Select((ChangedProperty obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
						object[] array3 = array;
						result["changedProperties", TomPropCategory.ChildCollection, 52, false] = array3;
					}
				}
			}
			if (!options.IgnoreChildren && !options.IncludeTranslatablePropertiesOnly && this.Annotations.Any<Annotation>())
			{
				object[] array = this.Annotations.Select((Annotation obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
				object[] array4 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array4;
			}
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x000266DC File Offset: 0x000248DC
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
				case 10:
				{
					char c = name[0];
					if (c != 'e')
					{
						if (c == 'l')
						{
							if (name == "lineageTag")
							{
								this.body.LineageTag = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
								return true;
							}
						}
					}
					else if (name == "expression")
					{
						this.body.Expression = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				}
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
				case 16:
					if (name == "sourceLineageTag")
					{
						this.body.SourceLineageTag = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				case 17:
					if (name == "changedProperties")
					{
						if (!JsonPropertyHelper.IsEmptyObjectCollection(jsonProp.Value) && !CompatibilityRestrictions.ChangedProperty.IsCompatible(mode, dbCompatibilityLevel))
						{
							return false;
						}
						JsonPropertyHelper.ReadObjectCollection(this.ChangedProperties, jsonProp.Value, options, mode, dbCompatibilityLevel);
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

		// Token: 0x06000501 RID: 1281 RVA: 0x00026A3E File Offset: 0x00024C3E
		internal override string GetFormattedObjectPath()
		{
			return TomSR.ObjectPath_Function_1Arg(this.Name);
		}

		// Token: 0x040000FA RID: 250
		internal Function.ObjectBody body;

		// Token: 0x040000FB RID: 251
		private FunctionAnnotationCollection _Annotations;

		// Token: 0x040000FC RID: 252
		private FunctionExtendedPropertyCollection _ExtendedProperties;

		// Token: 0x040000FD RID: 253
		private FunctionChangedPropertyCollection _ChangedProperties;

		// Token: 0x02000265 RID: 613
		internal class ObjectBody : NamedMetadataObjectBody<Function>
		{
			// Token: 0x06002036 RID: 8246 RVA: 0x000D3C1F File Offset: 0x000D1E1F
			public ObjectBody(Function owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.StructureModifiedTime = DateTime.MinValue;
				this.ModelID = new ParentLink<Function, Model>(owner, "Model");
			}

			// Token: 0x06002037 RID: 8247 RVA: 0x000D3C4F File Offset: 0x000D1E4F
			public override string GetObjectName()
			{
				return this.Name;
			}

			// Token: 0x06002038 RID: 8248 RVA: 0x000D3C58 File Offset: 0x000D1E58
			internal bool IsEqualTo(Function.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.Expression, other.Expression) && PropertyHelper.AreValuesIdentical(this.IsHidden, other.IsHidden) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.State, other.State)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ErrorMessage, other.ErrorMessage)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.StructureModifiedTime, other.StructureModifiedTime)) && PropertyHelper.AreValuesIdentical(this.LineageTag, other.LineageTag) && PropertyHelper.AreValuesIdentical(this.SourceLineageTag, other.SourceLineageTag) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.ModelID.IsEqualTo(other.ModelID, context));
			}

			// Token: 0x06002039 RID: 8249 RVA: 0x000D3DB0 File Offset: 0x000D1FB0
			internal void CopyFromImpl(Function.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				base.Owner.Name = other.Name;
				this.Description = other.Description;
				this.Expression = other.Expression;
				this.IsHidden = other.IsHidden;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.State = other.State;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ErrorMessage = other.ErrorMessage;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModifiedTime = other.ModifiedTime;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.StructureModifiedTime = other.StructureModifiedTime;
				}
				base.Owner.LineageTag = other.LineageTag;
				this.SourceLineageTag = other.SourceLineageTag;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModelID.CopyFrom(other.ModelID, context);
				}
			}

			// Token: 0x0600203A RID: 8250 RVA: 0x000D3EC4 File Offset: 0x000D20C4
			internal void CopyFromImpl(Function.ObjectBody other)
			{
				this.Name = other.Name;
				this.Description = other.Description;
				this.Expression = other.Expression;
				this.IsHidden = other.IsHidden;
				this.State = other.State;
				this.ErrorMessage = other.ErrorMessage;
				this.ModifiedTime = other.ModifiedTime;
				this.StructureModifiedTime = other.StructureModifiedTime;
				this.LineageTag = other.LineageTag;
				this.SourceLineageTag = other.SourceLineageTag;
				this.ModelID.CopyFrom(other.ModelID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x0600203B RID: 8251 RVA: 0x000D3F5F File Offset: 0x000D215F
			public override void CopyFrom(MetadataObjectBody<Function> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((Function.ObjectBody)other, context);
			}

			// Token: 0x0600203C RID: 8252 RVA: 0x000D3F78 File Offset: 0x000D2178
			internal bool IsEqualTo(Function.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.Expression, other.Expression) && PropertyHelper.AreValuesIdentical(this.IsHidden, other.IsHidden) && PropertyHelper.AreValuesIdentical(this.State, other.State) && PropertyHelper.AreValuesIdentical(this.ErrorMessage, other.ErrorMessage) && PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && PropertyHelper.AreValuesIdentical(this.StructureModifiedTime, other.StructureModifiedTime) && PropertyHelper.AreValuesIdentical(this.LineageTag, other.LineageTag) && PropertyHelper.AreValuesIdentical(this.SourceLineageTag, other.SourceLineageTag) && this.ModelID.IsEqualTo(other.ModelID);
			}

			// Token: 0x0600203D RID: 8253 RVA: 0x000D406D File Offset: 0x000D226D
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((Function.ObjectBody)other);
			}

			// Token: 0x0600203E RID: 8254 RVA: 0x000D4088 File Offset: 0x000D2288
			internal void CompareWith(Function.ObjectBody other, CompareContext context)
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
				if (!PropertyHelper.AreValuesIdentical(this.IsHidden, other.IsHidden))
				{
					context.RegisterPropertyChange(base.Owner, "IsHidden", typeof(bool), PropertyFlags.DdlAndUser, other.IsHidden, this.IsHidden);
				}
				if (!PropertyHelper.AreValuesIdentical(this.State, other.State))
				{
					context.RegisterPropertyChange(base.Owner, "State", typeof(ObjectState), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.State, this.State);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ErrorMessage, other.ErrorMessage))
				{
					context.RegisterPropertyChange(base.Owner, "ErrorMessage", typeof(string), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ErrorMessage, this.ErrorMessage);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				if (!PropertyHelper.AreValuesIdentical(this.StructureModifiedTime, other.StructureModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "StructureModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.StructureModifiedTime, this.StructureModifiedTime);
				}
				if (!PropertyHelper.AreValuesIdentical(this.LineageTag, other.LineageTag))
				{
					context.RegisterPropertyChange(base.Owner, "LineageTag", typeof(string), PropertyFlags.DdlAndUser, other.LineageTag, this.LineageTag);
				}
				if (!PropertyHelper.AreValuesIdentical(this.SourceLineageTag, other.SourceLineageTag))
				{
					context.RegisterPropertyChange(base.Owner, "SourceLineageTag", typeof(string), PropertyFlags.DdlAndUser, other.SourceLineageTag, this.SourceLineageTag);
				}
				this.ModelID.CompareWith(other.ModelID, "ModelID", "Model", PropertyFlags.ReadOnly, context);
			}

			// Token: 0x0600203F RID: 8255 RVA: 0x000D4333 File Offset: 0x000D2533
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((Function.ObjectBody)other, context);
			}

			// Token: 0x04000832 RID: 2098
			internal string Name;

			// Token: 0x04000833 RID: 2099
			internal string Description;

			// Token: 0x04000834 RID: 2100
			internal string Expression;

			// Token: 0x04000835 RID: 2101
			internal bool IsHidden;

			// Token: 0x04000836 RID: 2102
			internal ObjectState State;

			// Token: 0x04000837 RID: 2103
			internal string ErrorMessage;

			// Token: 0x04000838 RID: 2104
			internal DateTime ModifiedTime;

			// Token: 0x04000839 RID: 2105
			internal DateTime StructureModifiedTime;

			// Token: 0x0400083A RID: 2106
			internal string LineageTag;

			// Token: 0x0400083B RID: 2107
			internal string SourceLineageTag;

			// Token: 0x0400083C RID: 2108
			internal ParentLink<Function, Model> ModelID;
		}
	}
}
