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
	// Token: 0x02000099 RID: 153
	public sealed class PerspectiveHierarchy : NamedMetadataObject, ILinkedMetadataObject
	{
		// Token: 0x06000962 RID: 2402 RVA: 0x000501C6 File Offset: 0x0004E3C6
		public PerspectiveHierarchy()
		{
			this.InitBodyAndCollections(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x000501D9 File Offset: 0x0004E3D9
		internal PerspectiveHierarchy(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections(comparer);
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x000501E8 File Offset: 0x0004E3E8
		private void InitBodyAndCollections(IEqualityComparer<string> comparer)
		{
			this.body = new PerspectiveHierarchy.ObjectBody(this);
			this._Annotations = new PerspectiveHierarchyAnnotationCollection(this, comparer);
			this._ExtendedProperties = new PerspectiveHierarchyExtendedPropertyCollection(this, comparer);
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x00050210 File Offset: 0x0004E410
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.PerspectiveHierarchy;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x00050214 File Offset: 0x0004E414
		// (set) Token: 0x06000967 RID: 2407 RVA: 0x00050226 File Offset: 0x0004E426
		public override MetadataObject Parent
		{
			get
			{
				return this.body.PerspectiveTableID.Object;
			}
			internal set
			{
				if (this.body.PerspectiveTableID.Object != value)
				{
					MetadataObject.UpdateMetadataObjectParent<PerspectiveHierarchy, PerspectiveTable>(this.body.PerspectiveTableID, (PerspectiveTable)value, null, null);
				}
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x00050253 File Offset: 0x0004E453
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.PerspectiveTableID.ObjectID;
			}
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00050268 File Offset: 0x0004E468
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.PerspectiveHierarchy, null, "PerspectiveHierarchy object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty))
				{
					writer.WriteProperty("name", MetadataPropertyNature.NameProperty, typeof(string));
				}
				if (writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
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

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x00050348 File Offset: 0x0004E548
		// (set) Token: 0x0600096B RID: 2411 RVA: 0x00050350 File Offset: 0x0004E550
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (PerspectiveHierarchy.ObjectBody)value;
			}
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0005035E File Offset: 0x0004E55E
		internal override ITxObjectBody CreateBody()
		{
			return new PerspectiveHierarchy.ObjectBody(this);
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x00050366 File Offset: 0x0004E566
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new PerspectiveHierarchy();
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0005036D File Offset: 0x0004E56D
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return ((PerspectiveTable)parent).PerspectiveHierarchies;
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0005037C File Offset: 0x0004E57C
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			PerspectiveTable perspectiveTable = MetadataObject.ResolveMetadataObjectParentById<PerspectiveHierarchy, PerspectiveTable>(this.body.PerspectiveTableID, objectMap, throwIfCantResolve, null, null);
			this.body.HierarchyID.ResolveById(objectMap, throwIfCantResolve);
			if (perspectiveTable != null)
			{
				perspectiveTable.PerspectiveHierarchies.Add(this);
			}
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x000503C0 File Offset: 0x0004E5C0
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			this.body.HierarchyID.ResolveById(objectMap, throwIfCantResolve);
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x000503D8 File Offset: 0x0004E5D8
		internal override bool TryResolveCrossLinksByPath(ICollection<string> linksFailedToResolve)
		{
			bool flag = true;
			if (!this.body.HierarchyID.IsResolved && !this.body.HierarchyID.TryResolveByPath())
			{
				if (linksFailedToResolve != null)
				{
					linksFailedToResolve.Add(string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "Hierarchy"));
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0005042C File Offset: 0x0004E62C
		internal override void TryResolveCrossLinksAfterCopy(CopyContext copyContext)
		{
			if (!this.body.HierarchyID.TryResolveAfterCopy(copyContext) && this.body.HierarchyID.Path != null && !this.body.HierarchyID.Path.IsEmpty)
			{
				this.body._name = this.body.HierarchyID.Path[this.body.HierarchyID.Path.Count - 1].Value;
			}
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x000504B4 File Offset: 0x0004E6B4
		internal override void ValidateObjectImpl(ValidationResult result, bool throwOnError)
		{
			this.body.HierarchyID.Validate(result, throwOnError);
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x000504C8 File Offset: 0x0004E6C8
		internal override bool ContainsUnresolvedCrossLinksImpl()
		{
			return !this.body.HierarchyID.IsResolved || base.ContainsUnresolvedCrossLinksImpl();
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x000504E4 File Offset: 0x0004E6E4
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._Annotations;
			yield return this._ExtendedProperties;
			yield break;
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x000504F4 File Offset: 0x0004E6F4
		public PerspectiveHierarchyAnnotationCollection Annotations
		{
			get
			{
				return this._Annotations;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x000504FC File Offset: 0x0004E6FC
		[CompatibilityRequirement("1400")]
		public PerspectiveHierarchyExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this._ExtendedProperties;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x00050504 File Offset: 0x0004E704
		// (set) Token: 0x06000979 RID: 2425 RVA: 0x00050525 File Offset: 0x0004E725
		public override string Name
		{
			get
			{
				if (this.Hierarchy != null)
				{
					return this.Hierarchy.Name;
				}
				return this.body._name;
			}
			set
			{
				if (this.Hierarchy != null)
				{
					throw new InvalidOperationException(TomSR.Exception_NameCannotBeSetForReferencedObjects);
				}
				this.body._name = value;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x00050546 File Offset: 0x0004E746
		// (set) Token: 0x0600097B RID: 2427 RVA: 0x00050554 File Offset: 0x0004E754
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

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x000505D8 File Offset: 0x0004E7D8
		// (set) Token: 0x0600097D RID: 2429 RVA: 0x000505EC File Offset: 0x0004E7EC
		public PerspectiveTable PerspectiveTable
		{
			get
			{
				return this.body.PerspectiveTableID.Object;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.PerspectiveTableID.Object, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "PerspectiveTable", typeof(PerspectiveTable), this.body.PerspectiveTableID.Object, value);
					PerspectiveTable @object = this.body.PerspectiveTableID.Object;
					this.body.PerspectiveTableID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "PerspectiveTable", typeof(PerspectiveTable), @object, value);
				}
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x00050670 File Offset: 0x0004E870
		// (set) Token: 0x0600097F RID: 2431 RVA: 0x00050682 File Offset: 0x0004E882
		internal ObjectId _PerspectiveTableID
		{
			get
			{
				return this.body.PerspectiveTableID.ObjectID;
			}
			set
			{
				this.body.PerspectiveTableID.ObjectID = value;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x00050695 File Offset: 0x0004E895
		// (set) Token: 0x06000981 RID: 2433 RVA: 0x000506A8 File Offset: 0x0004E8A8
		public Hierarchy Hierarchy
		{
			get
			{
				return this.body.HierarchyID.Object;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.HierarchyID.Object, value))
				{
					if (this.body.HierarchyID.Object != null)
					{
						throw new InvalidOperationException(TomSR.Exception_CantChangeImmutableProperty("Hierarchy", "PerspectiveHierarchy"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "Hierarchy", typeof(Hierarchy), this.body.HierarchyID.Object, value);
					Hierarchy @object = this.body.HierarchyID.Object;
					this.body.HierarchyID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Hierarchy", typeof(Hierarchy), @object, value);
				}
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x00050756 File Offset: 0x0004E956
		// (set) Token: 0x06000983 RID: 2435 RVA: 0x00050768 File Offset: 0x0004E968
		internal ObjectId _HierarchyID
		{
			get
			{
				return this.body.HierarchyID.ObjectID;
			}
			set
			{
				this.body.HierarchyID.ObjectID = value;
			}
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0005077C File Offset: 0x0004E97C
		internal void CopyFrom(PerspectiveHierarchy other, CopyContext context)
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

		// Token: 0x06000985 RID: 2437 RVA: 0x0005083E File Offset: 0x0004EA3E
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((PerspectiveHierarchy)other, context);
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0005084D File Offset: 0x0004EA4D
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(PerspectiveHierarchy other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00050869 File Offset: 0x0004EA69
		public void CopyTo(PerspectiveHierarchy other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x00050885 File Offset: 0x0004EA85
		public PerspectiveHierarchy Clone()
		{
			return base.CloneInternal<PerspectiveHierarchy>();
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x00050890 File Offset: 0x0004EA90
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (this.body.PerspectiveTableID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "PerspectiveTableID", this.body.PerspectiveTableID.Object);
			}
			this.body.HierarchyID.Validate(null, true);
			if (this.body.HierarchyID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "HierarchyID", this.body.HierarchyID.Object);
			}
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00050918 File Offset: 0x0004EB18
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("PerspectiveTableID", out objectId))
			{
				this.body.PerspectiveTableID.ObjectID = objectId;
			}
			ObjectId objectId2;
			if (reader.TryReadProperty<ObjectId>("HierarchyID", out objectId2))
			{
				this.body.HierarchyID.ObjectID = objectId2;
			}
			DateTime dateTime;
			if (reader.TryReadProperty<DateTime>("ModifiedTime", out dateTime))
			{
				this.body.ModifiedTime = dateTime;
			}
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0005098C File Offset: 0x0004EB8C
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataBodyProperties(context, writer);
			if (this.body.PerspectiveTableID.Object != null && writer.ShouldIncludeProperty("PerspectiveTableID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
			{
				writer.WriteObjectIdProperty("PerspectiveTableID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly, this.body.PerspectiveTableID.Object);
			}
			this.body.HierarchyID.Validate(null, true);
			if (this.body.HierarchyID.Object != null && writer.ShouldIncludeProperty("HierarchyID", MetadataPropertyNature.CrossLinkProperty))
			{
				writer.WriteObjectIdProperty("HierarchyID", MetadataPropertyNature.CrossLinkProperty, this.body.HierarchyID.Object);
			}
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x00050A34 File Offset: 0x0004EC34
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataTree(context, writer);
			if (!string.IsNullOrEmpty(this.Name) && writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty))
			{
				writer.WriteStringProperty("name", MetadataPropertyNature.NameProperty, this.Name);
			}
			if (this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.ModifiedTime);
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

		// Token: 0x0600098D RID: 2445 RVA: 0x00050B60 File Offset: 0x0004ED60
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
				if (length <= 11)
				{
					if (length != 4)
					{
						if (length == 11)
						{
							char c = propertyName[0];
							if (c != 'H')
							{
								if (c == 'a')
								{
									if (propertyName == "annotations")
									{
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
							}
							else if (propertyName == "HierarchyID")
							{
								this.body.HierarchyID.ObjectID = reader.ReadObjectIdProperty();
								return true;
							}
						}
					}
					else if (propertyName == "name")
					{
						this.Name = reader.ReadStringProperty();
						return true;
					}
				}
				else
				{
					if (length == 12)
					{
						char c = propertyName[0];
						if (c != 'M')
						{
							if (c != 'm')
							{
								goto IL_0283;
							}
							if (!(propertyName == "modifiedTime"))
							{
								goto IL_0283;
							}
						}
						else if (!(propertyName == "ModifiedTime"))
						{
							goto IL_0283;
						}
						this.body.ModifiedTime = reader.ReadDateTimeProperty();
						return true;
					}
					if (length == 18)
					{
						char c = propertyName[0];
						if (c != 'P')
						{
							if (c == 'e')
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
						else if (propertyName == "PerspectiveTableID")
						{
							this.body.PerspectiveTableID.ObjectID = reader.ReadObjectIdProperty();
							return true;
						}
					}
				}
			}
			IL_0283:
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x00050E40 File Offset: 0x0004F040
		private protected override void OnDeserializeEnd(SerializationActivityContext context)
		{
			base.OnDeserializeEnd(context);
			if (context.SerializationMode != MetadataSerializationMode.Xmla)
			{
				context.RegistrerObjectForMasterReferenceCrossLinkReconstruction(this);
			}
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x00050E5C File Offset: 0x0004F05C
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!string.IsNullOrEmpty(this.Name))
			{
				result["name", TomPropCategory.Name, 0, true] = JsonPropertyHelper.ConvertStringToJsonValue(this.Name, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["modifiedTime", TomPropCategory.Regular, 4, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.ModifiedTime);
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

		// Token: 0x06000990 RID: 2448 RVA: 0x00051050 File Offset: 0x0004F250
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name == "name")
			{
				this.Name = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
				return true;
			}
			if (name == "modifiedTime")
			{
				this.body.ModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
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

		// Token: 0x06000991 RID: 2449 RVA: 0x00051114 File Offset: 0x0004F314
		internal override bool BuildIndirectNameCrossLinkPathIfNeeded()
		{
			if (this.body.HierarchyID.Object != null)
			{
				return true;
			}
			if (string.IsNullOrEmpty(this.body._name) || this.PerspectiveTable == null || string.IsNullOrEmpty(this.PerspectiveTable.Name))
			{
				return false;
			}
			if (this.body.HierarchyID.Path == null || this.body.HierarchyID.Path.IsEmpty)
			{
				this.body.HierarchyID.Path = new ObjectPath(new KeyValuePair<ObjectType, string>[]
				{
					new KeyValuePair<ObjectType, string>(ObjectType.Table, this.PerspectiveTable.Name),
					new KeyValuePair<ObjectType, string>(ObjectType.Hierarchy, this.body._name)
				});
			}
			return true;
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x000511D9 File Offset: 0x0004F3D9
		void ILinkedMetadataObject.GetLinkedObjectTarget(out ObjectId objectId, out ObjectPath objectPath, out MetadataObject @object, out string property)
		{
			objectId = this.body.HierarchyID.ObjectID;
			objectPath = this.body.HierarchyID.Path;
			@object = this.body.HierarchyID.Object;
			property = null;
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0005121C File Offset: 0x0004F41C
		internal override string GetFormattedObjectPath()
		{
			if (this.Hierarchy == null)
			{
				return TomSR.ObjectPath_PerspectiveHierarchy_0Args;
			}
			if (this.PerspectiveTable == null || this.PerspectiveTable.Table == null)
			{
				return TomSR.ObjectPath_PerspectiveHierarchy_1Args(this.Hierarchy.Name);
			}
			if (this.PerspectiveTable.Perspective != null)
			{
				return TomSR.ObjectPath_PerspectiveHierarchy_3Args(this.Hierarchy.Name, this.PerspectiveTable.Table.Name, this.PerspectiveTable.Perspective.Name);
			}
			return TomSR.ObjectPath_PerspectiveHierarchy_2Args(this.Hierarchy.Name, this.PerspectiveTable.Table.Name);
		}

		// Token: 0x04000153 RID: 339
		internal PerspectiveHierarchy.ObjectBody body;

		// Token: 0x04000154 RID: 340
		private PerspectiveHierarchyAnnotationCollection _Annotations;

		// Token: 0x04000155 RID: 341
		private PerspectiveHierarchyExtendedPropertyCollection _ExtendedProperties;

		// Token: 0x020002AB RID: 683
		internal class ObjectBody : NamedMetadataObjectBody<PerspectiveHierarchy>
		{
			// Token: 0x0600222A RID: 8746 RVA: 0x000DCB4F File Offset: 0x000DAD4F
			public ObjectBody(PerspectiveHierarchy owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.PerspectiveTableID = new ParentLink<PerspectiveHierarchy, PerspectiveTable>(owner, "PerspectiveTable");
				this.HierarchyID = new CrossLink<PerspectiveHierarchy, Hierarchy>(owner, "Hierarchy");
			}

			// Token: 0x0600222B RID: 8747 RVA: 0x000DCB85 File Offset: 0x000DAD85
			public override string GetObjectName()
			{
				if (this.HierarchyID.Object == null)
				{
					return this._name;
				}
				return this.HierarchyID.Object.Name;
			}

			// Token: 0x0600222C RID: 8748 RVA: 0x000DCBAC File Offset: 0x000DADAC
			internal bool IsEqualTo(PerspectiveHierarchy.ObjectBody other, CopyContext context)
			{
				return ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.PerspectiveTableID.IsEqualTo(other.PerspectiveTableID, context)) && this.HierarchyID.IsEqualTo(other.HierarchyID, context);
			}

			// Token: 0x0600222D RID: 8749 RVA: 0x000DCC24 File Offset: 0x000DAE24
			internal void CopyFromImpl(PerspectiveHierarchy.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModifiedTime = other.ModifiedTime;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.PerspectiveTableID.CopyFrom(other.PerspectiveTableID, context);
				}
				this.HierarchyID.CopyFrom(other.HierarchyID, context);
				this._name = other._name;
			}

			// Token: 0x0600222E RID: 8750 RVA: 0x000DCCA8 File Offset: 0x000DAEA8
			internal void CopyFromImpl(PerspectiveHierarchy.ObjectBody other)
			{
				this.ModifiedTime = other.ModifiedTime;
				this.PerspectiveTableID.CopyFrom(other.PerspectiveTableID, ObjectChangeTracker.BodyCloneContext);
				this.HierarchyID.CopyFrom(other.HierarchyID, ObjectChangeTracker.BodyCloneContext);
				this._name = other._name;
			}

			// Token: 0x0600222F RID: 8751 RVA: 0x000DCCF9 File Offset: 0x000DAEF9
			public override void CopyFrom(MetadataObjectBody<PerspectiveHierarchy> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((PerspectiveHierarchy.ObjectBody)other, context);
			}

			// Token: 0x06002230 RID: 8752 RVA: 0x000DCD10 File Offset: 0x000DAF10
			internal bool IsEqualTo(PerspectiveHierarchy.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && this.PerspectiveTableID.IsEqualTo(other.PerspectiveTableID) && this.HierarchyID.IsEqualTo(other.HierarchyID);
			}

			// Token: 0x06002231 RID: 8753 RVA: 0x000DCD5D File Offset: 0x000DAF5D
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((PerspectiveHierarchy.ObjectBody)other);
			}

			// Token: 0x06002232 RID: 8754 RVA: 0x000DCD78 File Offset: 0x000DAF78
			internal void CompareWith(PerspectiveHierarchy.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				this.PerspectiveTableID.CompareWith(other.PerspectiveTableID, "PerspectiveTableID", "PerspectiveTable", PropertyFlags.ReadOnly, context);
				this.HierarchyID.CompareWith(other.HierarchyID, "HierarchyID", "Hierarchy", PropertyFlags.None, context);
			}

			// Token: 0x06002233 RID: 8755 RVA: 0x000DCE04 File Offset: 0x000DB004
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((PerspectiveHierarchy.ObjectBody)other, context);
			}

			// Token: 0x040009AC RID: 2476
			internal DateTime ModifiedTime;

			// Token: 0x040009AD RID: 2477
			internal ParentLink<PerspectiveHierarchy, PerspectiveTable> PerspectiveTableID;

			// Token: 0x040009AE RID: 2478
			internal CrossLink<PerspectiveHierarchy, Hierarchy> HierarchyID;

			// Token: 0x040009AF RID: 2479
			internal string _name;
		}
	}
}
