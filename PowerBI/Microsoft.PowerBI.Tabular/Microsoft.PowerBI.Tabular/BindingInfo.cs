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
	// Token: 0x02000031 RID: 49
	[EditorBrowsable(EditorBrowsableState.Never)]
	[CompatibilityRequirement("Preview")]
	public abstract class BindingInfo : NamedMetadataObject
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x000066BB File Offset: 0x000048BB
		private protected BindingInfo()
		{
			this.InitBodyAndCollections(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000066CE File Offset: 0x000048CE
		private protected BindingInfo(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections(comparer);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000066E0 File Offset: 0x000048E0
		private void InitBodyAndCollections(IEqualityComparer<string> comparer)
		{
			this.body = new BindingInfo.ObjectBody(this);
			this.body.Name = string.Empty;
			this.body.Description = string.Empty;
			this.body.Type = BindingInfoType.Unknown;
			this.body.ConnectionId = string.Empty;
			this._Annotations = new BindingInfoAnnotationCollection(this, comparer);
			this._ExtendedProperties = new BindingInfoExtendedPropertyCollection(this, comparer);
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x0000674F File Offset: 0x0000494F
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.BindingInfo;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00006753 File Offset: 0x00004953
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00006765 File Offset: 0x00004965
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
					MetadataObject.UpdateMetadataObjectParent<BindingInfo, Model>(this.body.ModelID, (Model)value, null, null);
				}
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00006792 File Offset: 0x00004992
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.ModelID.ObjectID;
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000067A4 File Offset: 0x000049A4
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateChoiceScope())
			{
				BindingInfo.WriteMetadataSchemaForDataBindingHint(context, writer);
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000067DC File Offset: 0x000049DC
		private static void WriteMetadataSchemaForDataBindingHint(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.BindingInfo, "DataBindingHint", "DataBindingHint object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("type", MetadataPropertyNature.TypeProperty))
				{
					writer.WriteEnumProperty<BindingInfoType>("type", MetadataPropertyNature.TypeProperty, null);
				}
				if (writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
				{
					writer.WriteProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, typeof(string));
				}
				if (writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
				{
					writer.WriteProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, typeof(string));
				}
				if (writer.ShouldIncludeProperty("connectionId", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("connectionId", MetadataPropertyNature.RegularProperty, typeof(string));
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

		// Token: 0x060000C9 RID: 201 RVA: 0x00006908 File Offset: 0x00004B08
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			requiredLevel = CompatibilityRestrictions.BindingInfo[mode];
			requestingPath = ((!CompatibilityRestrictionSet.IsUnbound(requiredLevel)) ? string.Format("[{0}]", this.GetFormattedObjectPath()) : string.Empty);
			if (requiredLevel == -2)
			{
				return;
			}
			if (this.body.Type != BindingInfoType.Unknown)
			{
				int num = PropertyHelper.GetBindingInfoTypeCompatibilityRestrictions(this.body.Type)[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "Type");
					requiredLevel = num;
					int num2 = requiredLevel;
					return;
				}
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000CA RID: 202 RVA: 0x0000699B File Offset: 0x00004B9B
		// (set) Token: 0x060000CB RID: 203 RVA: 0x000069A3 File Offset: 0x00004BA3
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (BindingInfo.ObjectBody)value;
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000069B1 File Offset: 0x00004BB1
		internal override ITxObjectBody CreateBody()
		{
			return new BindingInfo.ObjectBody(this);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000069B9 File Offset: 0x00004BB9
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return ((Model)parent).BindingInfoCollection;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000069C8 File Offset: 0x00004BC8
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			Model model = MetadataObject.ResolveMetadataObjectParentById<BindingInfo, Model>(this.body.ModelID, objectMap, throwIfCantResolve, null, null);
			if (model != null)
			{
				model.BindingInfoCollection.Add(this);
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000069F9 File Offset: 0x00004BF9
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000069FB File Offset: 0x00004BFB
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._Annotations;
			yield return this._ExtendedProperties;
			yield break;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00006A0B File Offset: 0x00004C0B
		public BindingInfoAnnotationCollection Annotations
		{
			get
			{
				return this._Annotations;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00006A13 File Offset: 0x00004C13
		public BindingInfoExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this._ExtendedProperties;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00006A1B File Offset: 0x00004C1B
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00006A28 File Offset: 0x00004C28
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
					if (!Utils.IsSyntacticallyValidName(value, ObjectType.BindingInfo, out text))
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

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00006AAB File Offset: 0x00004CAB
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00006AB8 File Offset: 0x00004CB8
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

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00006B28 File Offset: 0x00004D28
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00006B38 File Offset: 0x00004D38
		public BindingInfoType Type
		{
			get
			{
				return this.body.Type;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Type, value))
				{
					CompatibilityRestrictionSet bindingInfoTypeCompatibilityRestrictions = PropertyHelper.GetBindingInfoTypeCompatibilityRestrictions(value);
					CompatibilityRestrictionSet bindingInfoTypeCompatibilityRestrictions2 = PropertyHelper.GetBindingInfoTypeCompatibilityRestrictions(this.body.Type);
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					RestrictionsComapreResult restrictionsComapreResult = bindingInfoTypeCompatibilityRestrictions.Compare(bindingInfoTypeCompatibilityRestrictions2);
					if (restrictionsComapreResult == RestrictionsComapreResult.MoreRestrictive || restrictionsComapreResult == RestrictionsComapreResult.Incomparable || (restrictionsComapreResult == RestrictionsComapreResult.Equal && value != BindingInfoType.Unknown))
					{
						array = base.ValidateCompatibilityRequirement(bindingInfoTypeCompatibilityRestrictions, string.Format("[{0}]::[{1}]=[{2}]", this.GetFormattedObjectPath(), "Type", value));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "Type", typeof(BindingInfoType), this.body.Type, value);
					switch (restrictionsComapreResult)
					{
					case RestrictionsComapreResult.Incomparable:
						base.ResetCompatibilityRequirement();
						base.SetCompatibilityRequirement(bindingInfoTypeCompatibilityRestrictions, array);
						break;
					case RestrictionsComapreResult.LessRestrictive:
						base.ResetCompatibilityRequirement();
						break;
					case RestrictionsComapreResult.Equal:
						if (array != null)
						{
							base.SetCompatibilityRequirement(bindingInfoTypeCompatibilityRestrictions, array);
						}
						break;
					case RestrictionsComapreResult.MoreRestrictive:
						base.SetCompatibilityRequirement(bindingInfoTypeCompatibilityRestrictions, array);
						break;
					}
					BindingInfoType type = this.body.Type;
					this.body.Type = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Type", typeof(BindingInfoType), type, value);
				}
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00006C59 File Offset: 0x00004E59
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00006C68 File Offset: 0x00004E68
		internal string ConnectionId
		{
			get
			{
				return this.body.ConnectionId;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.ConnectionId, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "ConnectionId", typeof(string), this.body.ConnectionId, value);
					string connectionId = this.body.ConnectionId;
					this.body.ConnectionId = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "ConnectionId", typeof(string), connectionId, value);
				}
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00006CD8 File Offset: 0x00004ED8
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00006CEA File Offset: 0x00004EEA
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

		// Token: 0x060000DD RID: 221 RVA: 0x00006D00 File Offset: 0x00004F00
		internal void CopyFrom(BindingInfo other, CopyContext context)
		{
			base.CopyFrom(other, context);
			if ((context.Flags & CopyFlags.IncludeCompatRestictions) == CopyFlags.IncludeCompatRestictions || !this.body.IsEqualTo(other.body, context))
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

		// Token: 0x060000DE RID: 222 RVA: 0x00006D85 File Offset: 0x00004F85
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((BindingInfo)other, context);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00006D94 File Offset: 0x00004F94
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(BindingInfo other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00006DB0 File Offset: 0x00004FB0
		public void CopyTo(BindingInfo other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00006DCC File Offset: 0x00004FCC
		public BindingInfo Clone()
		{
			return base.CloneInternal<BindingInfo>();
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00006DD4 File Offset: 0x00004FD4
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.BindingInfo.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object BindingInfo is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
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
			if (this.body.Type != BindingInfoType.Unknown)
			{
				if (!PropertyHelper.IsBindingInfoTypeValueCompatible(this.body.Type, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Type is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<BindingInfoType>(options, "Type", this.body.Type);
			}
			if (!string.IsNullOrEmpty(this.body.ConnectionId))
			{
				writer.WriteProperty<string>(options, "ConnectionId", this.body.ConnectionId);
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00006EF8 File Offset: 0x000050F8
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
			BindingInfoType bindingInfoType;
			if (reader.TryReadProperty<BindingInfoType>("Type", out bindingInfoType))
			{
				this.body.Type = bindingInfoType;
			}
			string text3;
			if (reader.TryReadProperty<string>("ConnectionId", out text3))
			{
				this.body.ConnectionId = text3;
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00006F9C File Offset: 0x0000519C
		private protected sealed override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.BindingInfo.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object BindingInfo is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
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
			if (this.body.Type != BindingInfoType.Unknown)
			{
				if (!PropertyHelper.IsBindingInfoTypeValueCompatible(this.body.Type, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Type is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("Type", MetadataPropertyNature.TypeProperty))
				{
					writer.WriteEnumProperty<BindingInfoType>("Type", MetadataPropertyNature.TypeProperty, this.body.Type);
				}
			}
			if (!string.IsNullOrEmpty(this.body.ConnectionId) && writer.ShouldIncludeProperty("ConnectionId", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("ConnectionId", MetadataPropertyNature.RegularProperty, this.body.ConnectionId);
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00007128 File Offset: 0x00005328
		private protected virtual void WriteRegularPropertiesToMetadataStream(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, this.body.Description);
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00007174 File Offset: 0x00005374
		private protected sealed override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.BindingInfo.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object BindingInfo is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataTree(context, writer);
			if (this.body.Type != BindingInfoType.Unknown)
			{
				if (!PropertyHelper.IsBindingInfoTypeValueCompatible(this.body.Type, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Type is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("type", MetadataPropertyNature.TypeProperty))
				{
					writer.WriteEnumProperty<BindingInfoType>("type", MetadataPropertyNature.TypeProperty, this.body.Type);
				}
			}
			if (!string.IsNullOrEmpty(this.body.Name) && writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
			this.WriteRegularPropertiesToMetadataStream(context, writer);
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

		// Token: 0x060000E7 RID: 231 RVA: 0x00007338 File Offset: 0x00005538
		private protected override bool TryReadNextMetadataProperty(SerializationActivityContext context, IMetadataReader reader, out UnexpectedPropertyClassification classification)
		{
			if (base.TryReadNextMetadataProperty(context, reader, out classification))
			{
				return true;
			}
			string propertyName = reader.PropertyName;
			if (propertyName != null)
			{
				int length = propertyName.Length;
				if (length <= 7)
				{
					if (length != 4)
					{
						if (length == 7)
						{
							if (propertyName == "ModelID")
							{
								this.body.ModelID.ObjectID = reader.ReadObjectIdProperty();
								return true;
							}
						}
					}
					else
					{
						char c = propertyName[0];
						if (c <= 'T')
						{
							if (c != 'N')
							{
								if (c != 'T')
								{
									return false;
								}
								if (!(propertyName == "Type"))
								{
									return false;
								}
								goto IL_01A6;
							}
							else if (!(propertyName == "Name"))
							{
								return false;
							}
						}
						else if (c != 'n')
						{
							if (c != 't')
							{
								return false;
							}
							if (!(propertyName == "type"))
							{
								return false;
							}
							goto IL_01A6;
						}
						else if (!(propertyName == "name"))
						{
							return false;
						}
						this.body.Name = reader.ReadStringProperty();
						return true;
						IL_01A6:
						if (!CompatibilityRestrictions.BindingInfoType.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							classification = UnexpectedPropertyClassification.IncompatibleProperty;
							return false;
						}
						this.body.Type = reader.ReadEnumProperty<BindingInfoType>();
						return true;
					}
				}
				else
				{
					if (length == 11)
					{
						char c = propertyName[0];
						if (c != 'D')
						{
							if (c != 'a')
							{
								if (c != 'd')
								{
									return false;
								}
								if (!(propertyName == "description"))
								{
									return false;
								}
							}
							else
							{
								if (!(propertyName == "annotations"))
								{
									return false;
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
							return false;
						}
						this.body.Description = reader.ReadStringProperty();
						return true;
					}
					if (length != 12)
					{
						if (length == 18)
						{
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
						}
					}
					else if (propertyName == "ConnectionId")
					{
						this.body.ConnectionId = reader.ReadStringProperty();
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000076A0 File Offset: 0x000058A0
		[Obsolete("Deprecated. Use RequestRename method instead.", false)]
		public void Rename(string newName)
		{
			this.RequestRename(newName);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000076A9 File Offset: 0x000058A9
		public void RequestRename(string newName)
		{
			ObjectChangeTracker.RegisterObjectRenaming(this);
			this.Name = newName;
			this.body.RenameRequestedThroughAPI = true;
			ObjectChangeTracker.RegisterObjectRenamed(this);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000076CC File Offset: 0x000058CC
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.BindingInfo.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object BindingInfo is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			if (!string.IsNullOrEmpty(this.body.Name))
			{
				result["name", TomPropCategory.Name, 2, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Name, SplitMultilineOptions.None);
			}
			if (!string.IsNullOrEmpty(this.body.Description))
			{
				result["description", TomPropCategory.Regular, 3, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Description, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.Type != BindingInfoType.Unknown)
			{
				if (!PropertyHelper.IsBindingInfoTypeValueCompatible(this.body.Type, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Type is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["type", TomPropCategory.Type, 4, false] = JsonPropertyHelper.ConvertEnumToJsonValue<BindingInfoType>(this.body.Type);
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

		// Token: 0x060000EB RID: 235 RVA: 0x00007978 File Offset: 0x00005B78
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name == "name")
			{
				this.body.Name = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
				return true;
			}
			if (name == "description")
			{
				this.body.Description = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
				return true;
			}
			if (!(name == "type"))
			{
				if (!(name == "extendedProperties"))
				{
					if (!(name == "annotations"))
					{
						return false;
					}
					JsonPropertyHelper.ReadObjectCollection(this.Annotations, jsonProp.Value, options, mode, dbCompatibilityLevel);
					return true;
				}
				else
				{
					if (!JsonPropertyHelper.IsEmptyObjectCollection(jsonProp.Value) && !CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
					{
						return false;
					}
					JsonPropertyHelper.ReadObjectCollection(this.ExtendedProperties, jsonProp.Value, options, mode, dbCompatibilityLevel);
					return true;
				}
			}
			else
			{
				BindingInfoType bindingInfoType = JsonPropertyHelper.ConvertJsonValueToEnum<BindingInfoType>(jsonProp.Value);
				if (jsonProp.Value.Type != 10 && !PropertyHelper.IsBindingInfoTypeValueCompatible(bindingInfoType, mode, dbCompatibilityLevel))
				{
					return false;
				}
				this.body.Type = bindingInfoType;
				return true;
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00007A88 File Offset: 0x00005C88
		internal static BindingInfo CreateFromMetadataStream(SerializationActivityContext context, IMetadataReader reader)
		{
			if (!reader.TryMoveToProperty("type"))
			{
				throw reader.CreateInvalidDataException(context, TomSR.Exception_CanNotReadFindTypePropObject("BindingInfo"), null);
			}
			BindingInfoType bindingInfoType = reader.ReadEnumProperty<BindingInfoType>();
			reader.Reset();
			if (bindingInfoType != BindingInfoType.Unknown && bindingInfoType == BindingInfoType.DataBindingHint)
			{
				return new DataBindingHint();
			}
			throw reader.CreateInvalidDataException(context, TomSR.Exception_UnrecognizedValueOfType("BindingInfoType", bindingInfoType.ToString()), null);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00007AEE File Offset: 0x00005CEE
		internal override string GetFormattedObjectPath()
		{
			return TomSR.ObjectPath_BindingInfo_1Arg(this.Name);
		}

		// Token: 0x040000CD RID: 205
		internal BindingInfo.ObjectBody body;

		// Token: 0x040000CE RID: 206
		private BindingInfoAnnotationCollection _Annotations;

		// Token: 0x040000CF RID: 207
		private BindingInfoExtendedPropertyCollection _ExtendedProperties;

		// Token: 0x040000D0 RID: 208
		internal static Func<BindingInfo, BindingInfo, bool> CompareBindingInfoType = (BindingInfo bindingInfo1, BindingInfo bindingInfo2) => bindingInfo1.Type == bindingInfo2.Type;

		// Token: 0x0200022A RID: 554
		internal class ObjectBody : NamedMetadataObjectBody<BindingInfo>
		{
			// Token: 0x06001EC1 RID: 7873 RVA: 0x000CCD57 File Offset: 0x000CAF57
			public ObjectBody(BindingInfo owner)
				: base(owner)
			{
				this.ModelID = new ParentLink<BindingInfo, Model>(owner, "Model");
			}

			// Token: 0x06001EC2 RID: 7874 RVA: 0x000CCD71 File Offset: 0x000CAF71
			public override string GetObjectName()
			{
				return this.Name;
			}

			// Token: 0x06001EC3 RID: 7875 RVA: 0x000CCD7C File Offset: 0x000CAF7C
			internal bool IsEqualTo(BindingInfo.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.Type, other.Type) && PropertyHelper.AreValuesIdentical(this.ConnectionId, other.ConnectionId) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.ModelID.IsEqualTo(other.ModelID, context));
			}

			// Token: 0x06001EC4 RID: 7876 RVA: 0x000CCE08 File Offset: 0x000CB008
			internal void CopyFromImpl(BindingInfo.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				base.Owner.Name = other.Name;
				this.Description = other.Description;
				this.Type = other.Type;
				this.ConnectionId = other.ConnectionId;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModelID.CopyFrom(other.ModelID, context);
				}
			}

			// Token: 0x06001EC5 RID: 7877 RVA: 0x000CCE84 File Offset: 0x000CB084
			internal void CopyFromImpl(BindingInfo.ObjectBody other)
			{
				this.Name = other.Name;
				this.Description = other.Description;
				this.Type = other.Type;
				this.ConnectionId = other.ConnectionId;
				this.ModelID.CopyFrom(other.ModelID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x06001EC6 RID: 7878 RVA: 0x000CCED7 File Offset: 0x000CB0D7
			public override void CopyFrom(MetadataObjectBody<BindingInfo> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((BindingInfo.ObjectBody)other, context);
			}

			// Token: 0x06001EC7 RID: 7879 RVA: 0x000CCEF0 File Offset: 0x000CB0F0
			internal bool IsEqualTo(BindingInfo.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.Type, other.Type) && PropertyHelper.AreValuesIdentical(this.ConnectionId, other.ConnectionId) && this.ModelID.IsEqualTo(other.ModelID);
			}

			// Token: 0x06001EC8 RID: 7880 RVA: 0x000CCF67 File Offset: 0x000CB167
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((BindingInfo.ObjectBody)other);
			}

			// Token: 0x06001EC9 RID: 7881 RVA: 0x000CCF80 File Offset: 0x000CB180
			internal void CompareWith(BindingInfo.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.Name, other.Name))
				{
					context.RegisterPropertyChange(base.Owner, "Name", typeof(string), base.RenameRequestedThroughAPI ? PropertyFlags.User : PropertyFlags.DdlAndUser, other.Name, this.Name);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Description, other.Description))
				{
					context.RegisterPropertyChange(base.Owner, "Description", typeof(string), PropertyFlags.DdlAndUser, other.Description, this.Description);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Type, other.Type))
				{
					context.RegisterPropertyChange(base.Owner, "Type", typeof(BindingInfoType), PropertyFlags.DdlAndUser, other.Type, this.Type);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ConnectionId, other.ConnectionId))
				{
					context.RegisterPropertyChange(base.Owner, "ConnectionId", typeof(string), PropertyFlags.DdlAndUser, other.ConnectionId, this.ConnectionId);
				}
				this.ModelID.CompareWith(other.ModelID, "ModelID", "Model", PropertyFlags.ReadOnly, context);
			}

			// Token: 0x06001ECA RID: 7882 RVA: 0x000CD0AB File Offset: 0x000CB2AB
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((BindingInfo.ObjectBody)other, context);
			}

			// Token: 0x04000728 RID: 1832
			internal string Name;

			// Token: 0x04000729 RID: 1833
			internal string Description;

			// Token: 0x0400072A RID: 1834
			internal BindingInfoType Type;

			// Token: 0x0400072B RID: 1835
			internal string ConnectionId;

			// Token: 0x0400072C RID: 1836
			internal ParentLink<BindingInfo, Model> ModelID;
		}
	}
}
