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
	// Token: 0x0200009D RID: 157
	public sealed class PerspectiveMeasure : NamedMetadataObject, ILinkedMetadataObject
	{
		// Token: 0x06000999 RID: 2457 RVA: 0x00051313 File Offset: 0x0004F513
		public PerspectiveMeasure()
		{
			this.InitBodyAndCollections(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00051326 File Offset: 0x0004F526
		internal PerspectiveMeasure(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections(comparer);
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00051335 File Offset: 0x0004F535
		private void InitBodyAndCollections(IEqualityComparer<string> comparer)
		{
			this.body = new PerspectiveMeasure.ObjectBody(this);
			this._Annotations = new PerspectiveMeasureAnnotationCollection(this, comparer);
			this._ExtendedProperties = new PerspectiveMeasureExtendedPropertyCollection(this, comparer);
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x0005135D File Offset: 0x0004F55D
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.PerspectiveMeasure;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x00051361 File Offset: 0x0004F561
		// (set) Token: 0x0600099E RID: 2462 RVA: 0x00051373 File Offset: 0x0004F573
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
					MetadataObject.UpdateMetadataObjectParent<PerspectiveMeasure, PerspectiveTable>(this.body.PerspectiveTableID, (PerspectiveTable)value, null, null);
				}
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x000513A0 File Offset: 0x0004F5A0
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.PerspectiveTableID.ObjectID;
			}
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x000513B4 File Offset: 0x0004F5B4
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.PerspectiveMeasure, null, "PerspectiveMeasure object of Tabular Object Model (TOM)", new bool?(false)))
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

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x00051494 File Offset: 0x0004F694
		// (set) Token: 0x060009A2 RID: 2466 RVA: 0x0005149C File Offset: 0x0004F69C
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (PerspectiveMeasure.ObjectBody)value;
			}
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x000514AA File Offset: 0x0004F6AA
		internal override ITxObjectBody CreateBody()
		{
			return new PerspectiveMeasure.ObjectBody(this);
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x000514B2 File Offset: 0x0004F6B2
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new PerspectiveMeasure();
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x000514B9 File Offset: 0x0004F6B9
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return ((PerspectiveTable)parent).PerspectiveMeasures;
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x000514C8 File Offset: 0x0004F6C8
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			PerspectiveTable perspectiveTable = MetadataObject.ResolveMetadataObjectParentById<PerspectiveMeasure, PerspectiveTable>(this.body.PerspectiveTableID, objectMap, throwIfCantResolve, null, null);
			this.body.MeasureID.ResolveById(objectMap, throwIfCantResolve);
			if (perspectiveTable != null)
			{
				perspectiveTable.PerspectiveMeasures.Add(this);
			}
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0005150C File Offset: 0x0004F70C
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			this.body.MeasureID.ResolveById(objectMap, throwIfCantResolve);
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x00051524 File Offset: 0x0004F724
		internal override bool TryResolveCrossLinksByPath(ICollection<string> linksFailedToResolve)
		{
			bool flag = true;
			if (!this.body.MeasureID.IsResolved && !this.body.MeasureID.TryResolveByPath())
			{
				if (linksFailedToResolve != null)
				{
					linksFailedToResolve.Add(string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "Measure"));
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00051578 File Offset: 0x0004F778
		internal override void TryResolveCrossLinksAfterCopy(CopyContext copyContext)
		{
			if (!this.body.MeasureID.TryResolveAfterCopy(copyContext) && this.body.MeasureID.Path != null && !this.body.MeasureID.Path.IsEmpty)
			{
				this.body._name = this.body.MeasureID.Path[this.body.MeasureID.Path.Count - 1].Value;
			}
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x00051600 File Offset: 0x0004F800
		internal override void ValidateObjectImpl(ValidationResult result, bool throwOnError)
		{
			this.body.MeasureID.Validate(result, throwOnError);
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00051614 File Offset: 0x0004F814
		internal override bool ContainsUnresolvedCrossLinksImpl()
		{
			return !this.body.MeasureID.IsResolved || base.ContainsUnresolvedCrossLinksImpl();
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00051630 File Offset: 0x0004F830
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._Annotations;
			yield return this._ExtendedProperties;
			yield break;
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x00051640 File Offset: 0x0004F840
		public PerspectiveMeasureAnnotationCollection Annotations
		{
			get
			{
				return this._Annotations;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x00051648 File Offset: 0x0004F848
		[CompatibilityRequirement("1400")]
		public PerspectiveMeasureExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this._ExtendedProperties;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x00051650 File Offset: 0x0004F850
		// (set) Token: 0x060009B0 RID: 2480 RVA: 0x00051671 File Offset: 0x0004F871
		public override string Name
		{
			get
			{
				if (this.Measure != null)
				{
					return this.Measure.Name;
				}
				return this.body._name;
			}
			set
			{
				if (this.Measure != null)
				{
					throw new InvalidOperationException(TomSR.Exception_NameCannotBeSetForReferencedObjects);
				}
				this.body._name = value;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x00051692 File Offset: 0x0004F892
		// (set) Token: 0x060009B2 RID: 2482 RVA: 0x000516A0 File Offset: 0x0004F8A0
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

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x00051724 File Offset: 0x0004F924
		// (set) Token: 0x060009B4 RID: 2484 RVA: 0x00051738 File Offset: 0x0004F938
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

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x000517BC File Offset: 0x0004F9BC
		// (set) Token: 0x060009B6 RID: 2486 RVA: 0x000517CE File Offset: 0x0004F9CE
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

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x000517E1 File Offset: 0x0004F9E1
		// (set) Token: 0x060009B8 RID: 2488 RVA: 0x000517F4 File Offset: 0x0004F9F4
		public Measure Measure
		{
			get
			{
				return this.body.MeasureID.Object;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.MeasureID.Object, value))
				{
					if (this.body.MeasureID.Object != null)
					{
						throw new InvalidOperationException(TomSR.Exception_CantChangeImmutableProperty("Measure", "PerspectiveMeasure"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "Measure", typeof(Measure), this.body.MeasureID.Object, value);
					Measure @object = this.body.MeasureID.Object;
					this.body.MeasureID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Measure", typeof(Measure), @object, value);
				}
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x000518A2 File Offset: 0x0004FAA2
		// (set) Token: 0x060009BA RID: 2490 RVA: 0x000518B4 File Offset: 0x0004FAB4
		internal ObjectId _MeasureID
		{
			get
			{
				return this.body.MeasureID.ObjectID;
			}
			set
			{
				this.body.MeasureID.ObjectID = value;
			}
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x000518C8 File Offset: 0x0004FAC8
		internal void CopyFrom(PerspectiveMeasure other, CopyContext context)
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

		// Token: 0x060009BC RID: 2492 RVA: 0x0005198A File Offset: 0x0004FB8A
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((PerspectiveMeasure)other, context);
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00051999 File Offset: 0x0004FB99
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(PerspectiveMeasure other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x000519B5 File Offset: 0x0004FBB5
		public void CopyTo(PerspectiveMeasure other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x000519D1 File Offset: 0x0004FBD1
		public PerspectiveMeasure Clone()
		{
			return base.CloneInternal<PerspectiveMeasure>();
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x000519DC File Offset: 0x0004FBDC
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (this.body.PerspectiveTableID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "PerspectiveTableID", this.body.PerspectiveTableID.Object);
			}
			this.body.MeasureID.Validate(null, true);
			if (this.body.MeasureID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "MeasureID", this.body.MeasureID.Object);
			}
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x00051A64 File Offset: 0x0004FC64
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("PerspectiveTableID", out objectId))
			{
				this.body.PerspectiveTableID.ObjectID = objectId;
			}
			ObjectId objectId2;
			if (reader.TryReadProperty<ObjectId>("MeasureID", out objectId2))
			{
				this.body.MeasureID.ObjectID = objectId2;
			}
			DateTime dateTime;
			if (reader.TryReadProperty<DateTime>("ModifiedTime", out dateTime))
			{
				this.body.ModifiedTime = dateTime;
			}
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00051AD8 File Offset: 0x0004FCD8
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataBodyProperties(context, writer);
			if (this.body.PerspectiveTableID.Object != null && writer.ShouldIncludeProperty("PerspectiveTableID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
			{
				writer.WriteObjectIdProperty("PerspectiveTableID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly, this.body.PerspectiveTableID.Object);
			}
			this.body.MeasureID.Validate(null, true);
			if (this.body.MeasureID.Object != null && writer.ShouldIncludeProperty("MeasureID", MetadataPropertyNature.CrossLinkProperty))
			{
				writer.WriteObjectIdProperty("MeasureID", MetadataPropertyNature.CrossLinkProperty, this.body.MeasureID.Object);
			}
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00051B80 File Offset: 0x0004FD80
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

		// Token: 0x060009C4 RID: 2500 RVA: 0x00051CAC File Offset: 0x0004FEAC
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
					case 9:
						if (propertyName == "MeasureID")
						{
							this.body.MeasureID.ObjectID = reader.ReadObjectIdProperty();
							return true;
						}
						break;
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

		// Token: 0x060009C5 RID: 2501 RVA: 0x00051F7C File Offset: 0x0005017C
		private protected override void OnDeserializeEnd(SerializationActivityContext context)
		{
			base.OnDeserializeEnd(context);
			if (context.SerializationMode != MetadataSerializationMode.Xmla)
			{
				context.RegistrerObjectForMasterReferenceCrossLinkReconstruction(this);
			}
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00051F98 File Offset: 0x00050198
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

		// Token: 0x060009C7 RID: 2503 RVA: 0x0005218C File Offset: 0x0005038C
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

		// Token: 0x060009C8 RID: 2504 RVA: 0x00052250 File Offset: 0x00050450
		internal override bool BuildIndirectNameCrossLinkPathIfNeeded()
		{
			if (this.body.MeasureID.Object != null)
			{
				return true;
			}
			if (string.IsNullOrEmpty(this.body._name) || this.PerspectiveTable == null || string.IsNullOrEmpty(this.PerspectiveTable.Name))
			{
				return false;
			}
			if (this.body.MeasureID.Path == null || this.body.MeasureID.Path.IsEmpty)
			{
				this.body.MeasureID.Path = new ObjectPath(new KeyValuePair<ObjectType, string>[]
				{
					new KeyValuePair<ObjectType, string>(ObjectType.Table, this.PerspectiveTable.Name),
					new KeyValuePair<ObjectType, string>(ObjectType.Measure, this.body._name)
				});
			}
			return true;
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00052314 File Offset: 0x00050514
		void ILinkedMetadataObject.GetLinkedObjectTarget(out ObjectId objectId, out ObjectPath objectPath, out MetadataObject @object, out string property)
		{
			objectId = this.body.MeasureID.ObjectID;
			objectPath = this.body.MeasureID.Path;
			@object = this.body.MeasureID.Object;
			property = null;
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x00052354 File Offset: 0x00050554
		internal override string GetFormattedObjectPath()
		{
			if (this.Measure == null)
			{
				return TomSR.ObjectPath_PerspectiveMeasure_0Args;
			}
			if (this.PerspectiveTable == null || this.PerspectiveTable.Table == null)
			{
				return TomSR.ObjectPath_PerspectiveMeasure_1Args(this.Measure.Name);
			}
			if (this.PerspectiveTable.Perspective != null)
			{
				return TomSR.ObjectPath_PerspectiveMeasure_3Args(this.Measure.Name, this.PerspectiveTable.Table.Name, this.PerspectiveTable.Perspective.Name);
			}
			return TomSR.ObjectPath_PerspectiveMeasure_2Args(this.Measure.Name, this.PerspectiveTable.Table.Name);
		}

		// Token: 0x04000156 RID: 342
		internal PerspectiveMeasure.ObjectBody body;

		// Token: 0x04000157 RID: 343
		private PerspectiveMeasureAnnotationCollection _Annotations;

		// Token: 0x04000158 RID: 344
		private PerspectiveMeasureExtendedPropertyCollection _ExtendedProperties;

		// Token: 0x020002AF RID: 687
		internal class ObjectBody : NamedMetadataObjectBody<PerspectiveMeasure>
		{
			// Token: 0x06002242 RID: 8770 RVA: 0x000DCF6F File Offset: 0x000DB16F
			public ObjectBody(PerspectiveMeasure owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.PerspectiveTableID = new ParentLink<PerspectiveMeasure, PerspectiveTable>(owner, "PerspectiveTable");
				this.MeasureID = new CrossLink<PerspectiveMeasure, Measure>(owner, "Measure");
			}

			// Token: 0x06002243 RID: 8771 RVA: 0x000DCFA5 File Offset: 0x000DB1A5
			public override string GetObjectName()
			{
				if (this.MeasureID.Object == null)
				{
					return this._name;
				}
				return this.MeasureID.Object.Name;
			}

			// Token: 0x06002244 RID: 8772 RVA: 0x000DCFCC File Offset: 0x000DB1CC
			internal bool IsEqualTo(PerspectiveMeasure.ObjectBody other, CopyContext context)
			{
				return ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.PerspectiveTableID.IsEqualTo(other.PerspectiveTableID, context)) && this.MeasureID.IsEqualTo(other.MeasureID, context);
			}

			// Token: 0x06002245 RID: 8773 RVA: 0x000DD044 File Offset: 0x000DB244
			internal void CopyFromImpl(PerspectiveMeasure.ObjectBody other, CopyContext context)
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
				this.MeasureID.CopyFrom(other.MeasureID, context);
				this._name = other._name;
			}

			// Token: 0x06002246 RID: 8774 RVA: 0x000DD0C8 File Offset: 0x000DB2C8
			internal void CopyFromImpl(PerspectiveMeasure.ObjectBody other)
			{
				this.ModifiedTime = other.ModifiedTime;
				this.PerspectiveTableID.CopyFrom(other.PerspectiveTableID, ObjectChangeTracker.BodyCloneContext);
				this.MeasureID.CopyFrom(other.MeasureID, ObjectChangeTracker.BodyCloneContext);
				this._name = other._name;
			}

			// Token: 0x06002247 RID: 8775 RVA: 0x000DD119 File Offset: 0x000DB319
			public override void CopyFrom(MetadataObjectBody<PerspectiveMeasure> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((PerspectiveMeasure.ObjectBody)other, context);
			}

			// Token: 0x06002248 RID: 8776 RVA: 0x000DD130 File Offset: 0x000DB330
			internal bool IsEqualTo(PerspectiveMeasure.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && this.PerspectiveTableID.IsEqualTo(other.PerspectiveTableID) && this.MeasureID.IsEqualTo(other.MeasureID);
			}

			// Token: 0x06002249 RID: 8777 RVA: 0x000DD17D File Offset: 0x000DB37D
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((PerspectiveMeasure.ObjectBody)other);
			}

			// Token: 0x0600224A RID: 8778 RVA: 0x000DD198 File Offset: 0x000DB398
			internal void CompareWith(PerspectiveMeasure.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				this.PerspectiveTableID.CompareWith(other.PerspectiveTableID, "PerspectiveTableID", "PerspectiveTable", PropertyFlags.ReadOnly, context);
				this.MeasureID.CompareWith(other.MeasureID, "MeasureID", "Measure", PropertyFlags.None, context);
			}

			// Token: 0x0600224B RID: 8779 RVA: 0x000DD224 File Offset: 0x000DB424
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((PerspectiveMeasure.ObjectBody)other, context);
			}

			// Token: 0x040009B9 RID: 2489
			internal DateTime ModifiedTime;

			// Token: 0x040009BA RID: 2490
			internal ParentLink<PerspectiveMeasure, PerspectiveTable> PerspectiveTableID;

			// Token: 0x040009BB RID: 2491
			internal CrossLink<PerspectiveMeasure, Measure> MeasureID;

			// Token: 0x040009BC RID: 2492
			internal string _name;
		}
	}
}
