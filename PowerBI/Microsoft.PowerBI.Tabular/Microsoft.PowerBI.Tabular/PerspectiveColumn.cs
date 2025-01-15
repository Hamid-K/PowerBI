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
	// Token: 0x02000094 RID: 148
	public sealed class PerspectiveColumn : NamedMetadataObject, ILinkedMetadataObject
	{
		// Token: 0x06000929 RID: 2345 RVA: 0x0004F05F File Offset: 0x0004D25F
		public PerspectiveColumn()
		{
			this.InitBodyAndCollections(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0004F072 File Offset: 0x0004D272
		internal PerspectiveColumn(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections(comparer);
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0004F081 File Offset: 0x0004D281
		private void InitBodyAndCollections(IEqualityComparer<string> comparer)
		{
			this.body = new PerspectiveColumn.ObjectBody(this);
			this._Annotations = new PerspectiveColumnAnnotationCollection(this, comparer);
			this._ExtendedProperties = new PerspectiveColumnExtendedPropertyCollection(this, comparer);
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x0004F0A9 File Offset: 0x0004D2A9
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.PerspectiveColumn;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x0004F0AD File Offset: 0x0004D2AD
		// (set) Token: 0x0600092E RID: 2350 RVA: 0x0004F0BF File Offset: 0x0004D2BF
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
					MetadataObject.UpdateMetadataObjectParent<PerspectiveColumn, PerspectiveTable>(this.body.PerspectiveTableID, (PerspectiveTable)value, null, null);
				}
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x0004F0EC File Offset: 0x0004D2EC
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.PerspectiveTableID.ObjectID;
			}
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0004F100 File Offset: 0x0004D300
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.PerspectiveColumn, null, "PerspectiveColumn object of Tabular Object Model (TOM)", new bool?(false)))
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

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x0004F1E0 File Offset: 0x0004D3E0
		// (set) Token: 0x06000932 RID: 2354 RVA: 0x0004F1E8 File Offset: 0x0004D3E8
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (PerspectiveColumn.ObjectBody)value;
			}
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0004F1F6 File Offset: 0x0004D3F6
		internal override ITxObjectBody CreateBody()
		{
			return new PerspectiveColumn.ObjectBody(this);
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0004F1FE File Offset: 0x0004D3FE
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new PerspectiveColumn();
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0004F205 File Offset: 0x0004D405
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return ((PerspectiveTable)parent).PerspectiveColumns;
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0004F214 File Offset: 0x0004D414
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			PerspectiveTable perspectiveTable = MetadataObject.ResolveMetadataObjectParentById<PerspectiveColumn, PerspectiveTable>(this.body.PerspectiveTableID, objectMap, throwIfCantResolve, null, null);
			this.body.ColumnID.ResolveById(objectMap, throwIfCantResolve);
			if (perspectiveTable != null)
			{
				perspectiveTable.PerspectiveColumns.Add(this);
			}
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0004F258 File Offset: 0x0004D458
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			this.body.ColumnID.ResolveById(objectMap, throwIfCantResolve);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0004F270 File Offset: 0x0004D470
		internal override bool TryResolveCrossLinksByPath(ICollection<string> linksFailedToResolve)
		{
			bool flag = true;
			if (!this.body.ColumnID.IsResolved && !this.body.ColumnID.TryResolveByPath())
			{
				if (linksFailedToResolve != null)
				{
					linksFailedToResolve.Add(string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "Column"));
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0004F2C4 File Offset: 0x0004D4C4
		internal override void TryResolveCrossLinksAfterCopy(CopyContext copyContext)
		{
			if (!this.body.ColumnID.TryResolveAfterCopy(copyContext) && this.body.ColumnID.Path != null && !this.body.ColumnID.Path.IsEmpty)
			{
				this.body._name = this.body.ColumnID.Path[this.body.ColumnID.Path.Count - 1].Value;
			}
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0004F34C File Offset: 0x0004D54C
		internal override void ValidateObjectImpl(ValidationResult result, bool throwOnError)
		{
			this.body.ColumnID.Validate(result, throwOnError);
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0004F360 File Offset: 0x0004D560
		internal override bool ContainsUnresolvedCrossLinksImpl()
		{
			return !this.body.ColumnID.IsResolved || base.ContainsUnresolvedCrossLinksImpl();
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0004F37C File Offset: 0x0004D57C
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._Annotations;
			yield return this._ExtendedProperties;
			yield break;
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x0004F38C File Offset: 0x0004D58C
		public PerspectiveColumnAnnotationCollection Annotations
		{
			get
			{
				return this._Annotations;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x0004F394 File Offset: 0x0004D594
		[CompatibilityRequirement("1400")]
		public PerspectiveColumnExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this._ExtendedProperties;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x0004F39C File Offset: 0x0004D59C
		// (set) Token: 0x06000940 RID: 2368 RVA: 0x0004F3BD File Offset: 0x0004D5BD
		public override string Name
		{
			get
			{
				if (this.Column != null)
				{
					return this.Column.Name;
				}
				return this.body._name;
			}
			set
			{
				if (this.Column != null)
				{
					throw new InvalidOperationException(TomSR.Exception_NameCannotBeSetForReferencedObjects);
				}
				this.body._name = value;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x0004F3DE File Offset: 0x0004D5DE
		// (set) Token: 0x06000942 RID: 2370 RVA: 0x0004F3EC File Offset: 0x0004D5EC
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

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x0004F470 File Offset: 0x0004D670
		// (set) Token: 0x06000944 RID: 2372 RVA: 0x0004F484 File Offset: 0x0004D684
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

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x0004F508 File Offset: 0x0004D708
		// (set) Token: 0x06000946 RID: 2374 RVA: 0x0004F51A File Offset: 0x0004D71A
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

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x0004F52D File Offset: 0x0004D72D
		// (set) Token: 0x06000948 RID: 2376 RVA: 0x0004F540 File Offset: 0x0004D740
		public Column Column
		{
			get
			{
				return this.body.ColumnID.Object;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.ColumnID.Object, value))
				{
					if (this.body.ColumnID.Object != null)
					{
						throw new InvalidOperationException(TomSR.Exception_CantChangeImmutableProperty("Column", "PerspectiveColumn"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "Column", typeof(Column), this.body.ColumnID.Object, value);
					Column @object = this.body.ColumnID.Object;
					this.body.ColumnID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Column", typeof(Column), @object, value);
				}
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x0004F5EE File Offset: 0x0004D7EE
		// (set) Token: 0x0600094A RID: 2378 RVA: 0x0004F600 File Offset: 0x0004D800
		internal ObjectId _ColumnID
		{
			get
			{
				return this.body.ColumnID.ObjectID;
			}
			set
			{
				this.body.ColumnID.ObjectID = value;
			}
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0004F614 File Offset: 0x0004D814
		internal void CopyFrom(PerspectiveColumn other, CopyContext context)
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

		// Token: 0x0600094C RID: 2380 RVA: 0x0004F6D6 File Offset: 0x0004D8D6
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((PerspectiveColumn)other, context);
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0004F6E5 File Offset: 0x0004D8E5
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(PerspectiveColumn other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0004F701 File Offset: 0x0004D901
		public void CopyTo(PerspectiveColumn other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0004F71D File Offset: 0x0004D91D
		public PerspectiveColumn Clone()
		{
			return base.CloneInternal<PerspectiveColumn>();
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0004F728 File Offset: 0x0004D928
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (this.body.PerspectiveTableID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "PerspectiveTableID", this.body.PerspectiveTableID.Object);
			}
			this.body.ColumnID.Validate(null, true);
			if (this.body.ColumnID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "ColumnID", this.body.ColumnID.Object);
			}
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0004F7B0 File Offset: 0x0004D9B0
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("PerspectiveTableID", out objectId))
			{
				this.body.PerspectiveTableID.ObjectID = objectId;
			}
			ObjectId objectId2;
			if (reader.TryReadProperty<ObjectId>("ColumnID", out objectId2))
			{
				this.body.ColumnID.ObjectID = objectId2;
			}
			DateTime dateTime;
			if (reader.TryReadProperty<DateTime>("ModifiedTime", out dateTime))
			{
				this.body.ModifiedTime = dateTime;
			}
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0004F824 File Offset: 0x0004DA24
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataBodyProperties(context, writer);
			if (this.body.PerspectiveTableID.Object != null && writer.ShouldIncludeProperty("PerspectiveTableID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
			{
				writer.WriteObjectIdProperty("PerspectiveTableID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly, this.body.PerspectiveTableID.Object);
			}
			this.body.ColumnID.Validate(null, true);
			if (this.body.ColumnID.Object != null && writer.ShouldIncludeProperty("ColumnID", MetadataPropertyNature.CrossLinkProperty))
			{
				writer.WriteObjectIdProperty("ColumnID", MetadataPropertyNature.CrossLinkProperty, this.body.ColumnID.Object);
			}
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0004F8CC File Offset: 0x0004DACC
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

		// Token: 0x06000954 RID: 2388 RVA: 0x0004F9F8 File Offset: 0x0004DBF8
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
				if (length != 4)
				{
					switch (length)
					{
					case 8:
						if (propertyName == "ColumnID")
						{
							this.body.ColumnID.ObjectID = reader.ReadObjectIdProperty();
							return true;
						}
						break;
					case 9:
					case 10:
						break;
					case 11:
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
						break;
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
						break;
					}
				}
				else if (propertyName == "name")
				{
					this.Name = reader.ReadStringProperty();
					return true;
				}
			}
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0004FCCC File Offset: 0x0004DECC
		private protected override void OnDeserializeEnd(SerializationActivityContext context)
		{
			base.OnDeserializeEnd(context);
			if (context.SerializationMode != MetadataSerializationMode.Xmla)
			{
				context.RegistrerObjectForMasterReferenceCrossLinkReconstruction(this);
			}
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0004FCE8 File Offset: 0x0004DEE8
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

		// Token: 0x06000957 RID: 2391 RVA: 0x0004FEDC File Offset: 0x0004E0DC
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

		// Token: 0x06000958 RID: 2392 RVA: 0x0004FFA0 File Offset: 0x0004E1A0
		internal override bool BuildIndirectNameCrossLinkPathIfNeeded()
		{
			if (this.body.ColumnID.Object != null)
			{
				return true;
			}
			if (string.IsNullOrEmpty(this.body._name) || this.PerspectiveTable == null || string.IsNullOrEmpty(this.PerspectiveTable.Name))
			{
				return false;
			}
			if (this.body.ColumnID.Path == null || this.body.ColumnID.Path.IsEmpty)
			{
				this.body.ColumnID.Path = new ObjectPath(new KeyValuePair<ObjectType, string>[]
				{
					new KeyValuePair<ObjectType, string>(ObjectType.Table, this.PerspectiveTable.Name),
					new KeyValuePair<ObjectType, string>(ObjectType.Column, this.body._name)
				});
			}
			return true;
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00050064 File Offset: 0x0004E264
		void ILinkedMetadataObject.GetLinkedObjectTarget(out ObjectId objectId, out ObjectPath objectPath, out MetadataObject @object, out string property)
		{
			objectId = this.body.ColumnID.ObjectID;
			objectPath = this.body.ColumnID.Path;
			@object = this.body.ColumnID.Object;
			property = null;
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x000500A4 File Offset: 0x0004E2A4
		internal override string GetFormattedObjectPath()
		{
			if (this.Column == null)
			{
				return TomSR.ObjectPath_PerspectiveColumn_0Args;
			}
			if (this.PerspectiveTable == null || this.PerspectiveTable.Table == null)
			{
				return TomSR.ObjectPath_PerspectiveColumn_1Args(this.Column.Name);
			}
			if (this.PerspectiveTable.Perspective != null)
			{
				return TomSR.ObjectPath_PerspectiveColumn_3Args(this.Column.Name, this.PerspectiveTable.Table.Name, this.PerspectiveTable.Perspective.Name);
			}
			return TomSR.ObjectPath_PerspectiveColumn_2Args(this.Column.Name, this.PerspectiveTable.Table.Name);
		}

		// Token: 0x04000150 RID: 336
		internal PerspectiveColumn.ObjectBody body;

		// Token: 0x04000151 RID: 337
		private PerspectiveColumnAnnotationCollection _Annotations;

		// Token: 0x04000152 RID: 338
		private PerspectiveColumnExtendedPropertyCollection _ExtendedProperties;

		// Token: 0x020002A7 RID: 679
		internal class ObjectBody : NamedMetadataObjectBody<PerspectiveColumn>
		{
			// Token: 0x06002212 RID: 8722 RVA: 0x000DC72F File Offset: 0x000DA92F
			public ObjectBody(PerspectiveColumn owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.PerspectiveTableID = new ParentLink<PerspectiveColumn, PerspectiveTable>(owner, "PerspectiveTable");
				this.ColumnID = new CrossLink<PerspectiveColumn, Column>(owner, "Column");
			}

			// Token: 0x06002213 RID: 8723 RVA: 0x000DC765 File Offset: 0x000DA965
			public override string GetObjectName()
			{
				if (this.ColumnID.Object == null)
				{
					return this._name;
				}
				return this.ColumnID.Object.Name;
			}

			// Token: 0x06002214 RID: 8724 RVA: 0x000DC78C File Offset: 0x000DA98C
			internal bool IsEqualTo(PerspectiveColumn.ObjectBody other, CopyContext context)
			{
				return ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.PerspectiveTableID.IsEqualTo(other.PerspectiveTableID, context)) && this.ColumnID.IsEqualTo(other.ColumnID, context);
			}

			// Token: 0x06002215 RID: 8725 RVA: 0x000DC804 File Offset: 0x000DAA04
			internal void CopyFromImpl(PerspectiveColumn.ObjectBody other, CopyContext context)
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
				this.ColumnID.CopyFrom(other.ColumnID, context);
				this._name = other._name;
			}

			// Token: 0x06002216 RID: 8726 RVA: 0x000DC888 File Offset: 0x000DAA88
			internal void CopyFromImpl(PerspectiveColumn.ObjectBody other)
			{
				this.ModifiedTime = other.ModifiedTime;
				this.PerspectiveTableID.CopyFrom(other.PerspectiveTableID, ObjectChangeTracker.BodyCloneContext);
				this.ColumnID.CopyFrom(other.ColumnID, ObjectChangeTracker.BodyCloneContext);
				this._name = other._name;
			}

			// Token: 0x06002217 RID: 8727 RVA: 0x000DC8D9 File Offset: 0x000DAAD9
			public override void CopyFrom(MetadataObjectBody<PerspectiveColumn> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((PerspectiveColumn.ObjectBody)other, context);
			}

			// Token: 0x06002218 RID: 8728 RVA: 0x000DC8F0 File Offset: 0x000DAAF0
			internal bool IsEqualTo(PerspectiveColumn.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && this.PerspectiveTableID.IsEqualTo(other.PerspectiveTableID) && this.ColumnID.IsEqualTo(other.ColumnID);
			}

			// Token: 0x06002219 RID: 8729 RVA: 0x000DC93D File Offset: 0x000DAB3D
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((PerspectiveColumn.ObjectBody)other);
			}

			// Token: 0x0600221A RID: 8730 RVA: 0x000DC958 File Offset: 0x000DAB58
			internal void CompareWith(PerspectiveColumn.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				this.PerspectiveTableID.CompareWith(other.PerspectiveTableID, "PerspectiveTableID", "PerspectiveTable", PropertyFlags.ReadOnly, context);
				this.ColumnID.CompareWith(other.ColumnID, "ColumnID", "Column", PropertyFlags.None, context);
			}

			// Token: 0x0600221B RID: 8731 RVA: 0x000DC9E4 File Offset: 0x000DABE4
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((PerspectiveColumn.ObjectBody)other, context);
			}

			// Token: 0x0400099F RID: 2463
			internal DateTime ModifiedTime;

			// Token: 0x040009A0 RID: 2464
			internal ParentLink<PerspectiveColumn, PerspectiveTable> PerspectiveTableID;

			// Token: 0x040009A1 RID: 2465
			internal CrossLink<PerspectiveColumn, Column> ColumnID;

			// Token: 0x040009A2 RID: 2466
			internal string _name;
		}
	}
}
