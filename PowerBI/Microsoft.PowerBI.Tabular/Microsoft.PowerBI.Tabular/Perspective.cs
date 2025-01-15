using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000091 RID: 145
	public sealed class Perspective : NamedMetadataObject
	{
		// Token: 0x060008FC RID: 2300 RVA: 0x0004DF29 File Offset: 0x0004C129
		public Perspective()
		{
			this.InitBodyAndCollections(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0004DF3C File Offset: 0x0004C13C
		internal Perspective(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections(comparer);
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0004DF4C File Offset: 0x0004C14C
		private void InitBodyAndCollections(IEqualityComparer<string> comparer)
		{
			this.body = new Perspective.ObjectBody(this);
			this.body.Name = string.Empty;
			this.body.Description = string.Empty;
			this._PerspectiveTables = new PerspectiveTableCollection(this, comparer);
			this._Annotations = new PerspectiveAnnotationCollection(this, comparer);
			this._ExtendedProperties = new PerspectiveExtendedPropertyCollection(this, comparer);
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x0004DFAC File Offset: 0x0004C1AC
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.Perspective;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x0004DFB0 File Offset: 0x0004C1B0
		// (set) Token: 0x06000901 RID: 2305 RVA: 0x0004DFC2 File Offset: 0x0004C1C2
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
					MetadataObject.UpdateMetadataObjectParent<Perspective, Model>(this.body.ModelID, (Model)value, null, null);
				}
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x0004DFEF File Offset: 0x0004C1EF
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.ModelID.ObjectID;
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0004E004 File Offset: 0x0004C204
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.Perspective, null, "Perspective object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
				{
					writer.WriteProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, typeof(string));
				}
				if (writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
				{
					writer.WriteProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, typeof(string));
				}
				if (writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				if (writer.ShouldIncludeProperty("tables", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "tables", MetadataPropertyNature.ChildCollection, ObjectType.PerspectiveTable);
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

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x0004E138 File Offset: 0x0004C338
		// (set) Token: 0x06000905 RID: 2309 RVA: 0x0004E140 File Offset: 0x0004C340
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (Perspective.ObjectBody)value;
			}
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0004E14E File Offset: 0x0004C34E
		internal override ITxObjectBody CreateBody()
		{
			return new Perspective.ObjectBody(this);
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0004E156 File Offset: 0x0004C356
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new Perspective();
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0004E15D File Offset: 0x0004C35D
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return ((Model)parent).Perspectives;
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0004E16C File Offset: 0x0004C36C
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			Model model = MetadataObject.ResolveMetadataObjectParentById<Perspective, Model>(this.body.ModelID, objectMap, throwIfCantResolve, null, null);
			if (model != null)
			{
				model.Perspectives.Add(this);
			}
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0004E19D File Offset: 0x0004C39D
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0004E19F File Offset: 0x0004C39F
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._PerspectiveTables;
			yield return this._Annotations;
			yield return this._ExtendedProperties;
			yield break;
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x0004E1AF File Offset: 0x0004C3AF
		public PerspectiveTableCollection PerspectiveTables
		{
			get
			{
				return this._PerspectiveTables;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x0004E1B7 File Offset: 0x0004C3B7
		public PerspectiveAnnotationCollection Annotations
		{
			get
			{
				return this._Annotations;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x0004E1BF File Offset: 0x0004C3BF
		[CompatibilityRequirement("1400")]
		public PerspectiveExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this._ExtendedProperties;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x0004E1C7 File Offset: 0x0004C3C7
		// (set) Token: 0x06000910 RID: 2320 RVA: 0x0004E1D4 File Offset: 0x0004C3D4
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
					if (!Utils.IsSyntacticallyValidName(value, ObjectType.Perspective, out text))
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

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x0004E257 File Offset: 0x0004C457
		// (set) Token: 0x06000912 RID: 2322 RVA: 0x0004E264 File Offset: 0x0004C464
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

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x0004E2D4 File Offset: 0x0004C4D4
		// (set) Token: 0x06000914 RID: 2324 RVA: 0x0004E2E4 File Offset: 0x0004C4E4
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

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x0004E368 File Offset: 0x0004C568
		// (set) Token: 0x06000916 RID: 2326 RVA: 0x0004E37A File Offset: 0x0004C57A
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

		// Token: 0x06000917 RID: 2327 RVA: 0x0004E390 File Offset: 0x0004C590
		internal void CopyFrom(Perspective other, CopyContext context)
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
				this.PerspectiveTables.CopyFrom(other.PerspectiveTables, context);
				this.Annotations.CopyFrom(other.Annotations, context);
				this.ExtendedProperties.CopyFrom(other.ExtendedProperties, context);
			}
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0004E464 File Offset: 0x0004C664
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((Perspective)other, context);
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0004E473 File Offset: 0x0004C673
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(Perspective other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0004E48F File Offset: 0x0004C68F
		public void CopyTo(Perspective other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0004E4AB File Offset: 0x0004C6AB
		public Perspective Clone()
		{
			return base.CloneInternal<Perspective>();
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0004E4B4 File Offset: 0x0004C6B4
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
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0004E520 File Offset: 0x0004C720
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
			DateTime dateTime;
			if (reader.TryReadProperty<DateTime>("ModifiedTime", out dateTime))
			{
				this.body.ModifiedTime = dateTime;
			}
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0004E5A8 File Offset: 0x0004C7A8
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataBodyProperties(context, writer);
			if (!string.IsNullOrEmpty(this.body.Name) && writer.ShouldIncludeProperty("Name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("Name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, this.body.Description);
			}
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0004E63C File Offset: 0x0004C83C
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataTree(context, writer);
			if (!string.IsNullOrEmpty(this.body.Name) && writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, this.body.Description);
			}
			if (this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.ModifiedTime);
			}
			if (this.PerspectiveTables.Count > 0 && writer.ShouldIncludeProperty("tables", MetadataPropertyNature.ChildCollection))
			{
				writer.WriteChildCollection(context, "tables", MetadataPropertyNature.ChildCollection, this.PerspectiveTables);
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

		// Token: 0x06000920 RID: 2336 RVA: 0x0004E7E8 File Offset: 0x0004C9E8
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
				switch (length)
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
				case 8:
				case 9:
				case 10:
					break;
				case 6:
					if (propertyName == "tables")
					{
						using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
						{
							foreach (PerspectiveTable perspectiveTable in reader.ReadChildCollectionProperty<PerspectiveTable>(context))
							{
								try
								{
									this.PerspectiveTables.Add(perspectiveTable);
								}
								catch (Exception ex)
								{
									throw reader.CreateInvalidChildException(context, perspectiveTable, TomSR.Exception_FailedAddDeserializedNamedObject("PerspectiveTable", (perspectiveTable != null) ? perspectiveTable.Name : null, ex.Message), ex);
								}
							}
						}
						return true;
					}
					break;
				case 7:
					if (propertyName == "ModelID")
					{
						this.body.ModelID.ObjectID = reader.ReadObjectIdProperty();
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
									catch (Exception ex2)
									{
										throw reader.CreateInvalidChildException(context, annotation, TomSR.Exception_FailedAddDeserializedNamedObject("Annotation", (annotation != null) ? annotation.Name : null, ex2.Message), ex2);
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
				default:
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
									catch (Exception ex3)
									{
										throw reader.CreateInvalidChildException(context, extendedProperty, TomSR.Exception_FailedAddDeserializedNamedObject("ExtendedProperty", (extendedProperty != null) ? extendedProperty.Name : null, ex3.Message), ex3);
									}
								}
							}
							return true;
						}
					}
					break;
				}
			}
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0004EBDC File Offset: 0x0004CDDC
		[Obsolete("Deprecated. Use RequestRename method instead.", false)]
		public void Rename(string newName)
		{
			this.RequestRename(newName);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0004EBE5 File Offset: 0x0004CDE5
		public void RequestRename(string newName)
		{
			ObjectChangeTracker.RegisterObjectRenaming(this);
			this.Name = newName;
			this.body.RenameRequestedThroughAPI = true;
			ObjectChangeTracker.RegisterObjectRenamed(this);
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0004EC08 File Offset: 0x0004CE08
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!string.IsNullOrEmpty(this.body.Name))
			{
				result["name", TomPropCategory.Name, 2, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Name, SplitMultilineOptions.None);
			}
			if (!string.IsNullOrEmpty(this.body.Description))
			{
				result["description", TomPropCategory.Regular, 3, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Description, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["modifiedTime", TomPropCategory.Regular, 4, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.ModifiedTime);
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations)
			{
				if (!options.IncludeTranslatablePropertiesOnly)
				{
					IEnumerable<PerspectiveTable> enumerable;
					if (!options.IgnoreInferredObjects)
					{
						IEnumerable<PerspectiveTable> perspectiveTables = this.PerspectiveTables;
						enumerable = perspectiveTables;
					}
					else
					{
						enumerable = this.PerspectiveTables.Where((PerspectiveTable o) => !ObjectTreeHelper.IsInferredObject(o));
					}
					IEnumerable<PerspectiveTable> enumerable2 = enumerable;
					if (enumerable2.Any<PerspectiveTable>())
					{
						object[] array = enumerable2.Select((PerspectiveTable obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
						object[] array2 = array;
						result["tables", TomPropCategory.ChildCollection, 29, false] = array2;
					}
				}
				if (!options.IncludeTranslatablePropertiesOnly)
				{
					IEnumerable<ExtendedProperty> enumerable3;
					if (!options.IgnoreInferredObjects)
					{
						IEnumerable<ExtendedProperty> extendedProperties = this.ExtendedProperties;
						enumerable3 = extendedProperties;
					}
					else
					{
						enumerable3 = this.ExtendedProperties.Where((ExtendedProperty o) => !ObjectTreeHelper.IsInferredObject(o));
					}
					IEnumerable<ExtendedProperty> enumerable4 = enumerable3;
					if (enumerable4.Any<ExtendedProperty>())
					{
						if (!CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
						{
							throw TomInternalException.Create("A child ExtendedProperty is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
						}
						object[] array = enumerable4.Select((ExtendedProperty obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
						object[] array3 = array;
						result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array3;
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

		// Token: 0x06000924 RID: 2340 RVA: 0x0004EED4 File Offset: 0x0004D0D4
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
			if (name == "modifiedTime")
			{
				this.body.ModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
				return true;
			}
			if (name == "tables")
			{
				JsonPropertyHelper.ReadObjectCollection(this.PerspectiveTables, jsonProp.Value, options, mode, dbCompatibilityLevel);
				return true;
			}
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

		// Token: 0x06000925 RID: 2341 RVA: 0x0004EFEC File Offset: 0x0004D1EC
		internal override void OnAfterDeserialize(DeserializeOptions options)
		{
			foreach (MetadataObject metadataObject in base.GetAllDescendants())
			{
				metadataObject.BuildIndirectNameCrossLinkPathIfNeeded();
			}
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0004F038 File Offset: 0x0004D238
		internal override string GetFormattedObjectPath()
		{
			return TomSR.ObjectPath_Perspective_1Arg(this.Name);
		}

		// Token: 0x0400014C RID: 332
		internal Perspective.ObjectBody body;

		// Token: 0x0400014D RID: 333
		private PerspectiveTableCollection _PerspectiveTables;

		// Token: 0x0400014E RID: 334
		private PerspectiveAnnotationCollection _Annotations;

		// Token: 0x0400014F RID: 335
		private PerspectiveExtendedPropertyCollection _ExtendedProperties;

		// Token: 0x020002A3 RID: 675
		internal class ObjectBody : NamedMetadataObjectBody<Perspective>
		{
			// Token: 0x060021F8 RID: 8696 RVA: 0x000DC27F File Offset: 0x000DA47F
			public ObjectBody(Perspective owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.ModelID = new ParentLink<Perspective, Model>(owner, "Model");
			}

			// Token: 0x060021F9 RID: 8697 RVA: 0x000DC2A4 File Offset: 0x000DA4A4
			public override string GetObjectName()
			{
				return this.Name;
			}

			// Token: 0x060021FA RID: 8698 RVA: 0x000DC2AC File Offset: 0x000DA4AC
			internal bool IsEqualTo(Perspective.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.ModelID.IsEqualTo(other.ModelID, context));
			}

			// Token: 0x060021FB RID: 8699 RVA: 0x000DC338 File Offset: 0x000DA538
			internal void CopyFromImpl(Perspective.ObjectBody other, CopyContext context)
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
					this.ModifiedTime = other.ModifiedTime;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModelID.CopyFrom(other.ModelID, context);
				}
			}

			// Token: 0x060021FC RID: 8700 RVA: 0x000DC3B9 File Offset: 0x000DA5B9
			internal void CopyFromImpl(Perspective.ObjectBody other)
			{
				this.Name = other.Name;
				this.Description = other.Description;
				this.ModifiedTime = other.ModifiedTime;
				this.ModelID.CopyFrom(other.ModelID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x060021FD RID: 8701 RVA: 0x000DC3F5 File Offset: 0x000DA5F5
			public override void CopyFrom(MetadataObjectBody<Perspective> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((Perspective.ObjectBody)other, context);
			}

			// Token: 0x060021FE RID: 8702 RVA: 0x000DC40C File Offset: 0x000DA60C
			internal bool IsEqualTo(Perspective.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && this.ModelID.IsEqualTo(other.ModelID);
			}

			// Token: 0x060021FF RID: 8703 RVA: 0x000DC46E File Offset: 0x000DA66E
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((Perspective.ObjectBody)other);
			}

			// Token: 0x06002200 RID: 8704 RVA: 0x000DC488 File Offset: 0x000DA688
			internal void CompareWith(Perspective.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.Name, other.Name))
				{
					context.RegisterPropertyChange(base.Owner, "Name", typeof(string), base.RenameRequestedThroughAPI ? PropertyFlags.User : PropertyFlags.DdlAndUser, other.Name, this.Name);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Description, other.Description))
				{
					context.RegisterPropertyChange(base.Owner, "Description", typeof(string), PropertyFlags.DdlAndUser, other.Description, this.Description);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				this.ModelID.CompareWith(other.ModelID, "ModelID", "Model", PropertyFlags.ReadOnly, context);
			}

			// Token: 0x06002201 RID: 8705 RVA: 0x000DC578 File Offset: 0x000DA778
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((Perspective.ObjectBody)other, context);
			}

			// Token: 0x04000991 RID: 2449
			internal string Name;

			// Token: 0x04000992 RID: 2450
			internal string Description;

			// Token: 0x04000993 RID: 2451
			internal DateTime ModifiedTime;

			// Token: 0x04000994 RID: 2452
			internal ParentLink<Perspective, Model> ModelID;
		}
	}
}
