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
	// Token: 0x020000A1 RID: 161
	[CompatibilityRequirement(Pbi = "1400")]
	public sealed class PerspectiveSet : NamedMetadataObject, ILinkedMetadataObject
	{
		// Token: 0x060009D0 RID: 2512 RVA: 0x0005244B File Offset: 0x0005064B
		public PerspectiveSet()
		{
			this.InitBodyAndCollections(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0005245E File Offset: 0x0005065E
		internal PerspectiveSet(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections(comparer);
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0005246D File Offset: 0x0005066D
		private void InitBodyAndCollections(IEqualityComparer<string> comparer)
		{
			this.body = new PerspectiveSet.ObjectBody(this);
			this._Annotations = new PerspectiveSetAnnotationCollection(this, comparer);
			this._ExtendedProperties = new PerspectiveSetExtendedPropertyCollection(this, comparer);
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x00052495 File Offset: 0x00050695
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.PerspectiveSet;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x00052499 File Offset: 0x00050699
		// (set) Token: 0x060009D5 RID: 2517 RVA: 0x000524AB File Offset: 0x000506AB
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
					MetadataObject.UpdateMetadataObjectParent<PerspectiveSet, PerspectiveTable>(this.body.PerspectiveTableID, (PerspectiveTable)value, null, null);
				}
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x000524D8 File Offset: 0x000506D8
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.PerspectiveTableID.ObjectID;
			}
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x000524EC File Offset: 0x000506EC
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.PerspectiveSet, null, "PerspectiveSet object of Tabular Object Model (TOM)", new bool?(false)))
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

		// Token: 0x060009D8 RID: 2520 RVA: 0x000525CC File Offset: 0x000507CC
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			requiredLevel = CompatibilityRestrictions.PerspectiveSet[mode];
			requestingPath = ((!CompatibilityRestrictionSet.IsUnbound(requiredLevel)) ? string.Format("[{0}]", this.GetFormattedObjectPath()) : string.Empty);
			int num = requiredLevel;
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x00052603 File Offset: 0x00050803
		// (set) Token: 0x060009DA RID: 2522 RVA: 0x0005260B File Offset: 0x0005080B
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (PerspectiveSet.ObjectBody)value;
			}
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x00052619 File Offset: 0x00050819
		internal override ITxObjectBody CreateBody()
		{
			return new PerspectiveSet.ObjectBody(this);
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00052621 File Offset: 0x00050821
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new PerspectiveSet();
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00052628 File Offset: 0x00050828
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return ((PerspectiveTable)parent).PerspectiveSets;
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00052638 File Offset: 0x00050838
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			PerspectiveTable perspectiveTable = MetadataObject.ResolveMetadataObjectParentById<PerspectiveSet, PerspectiveTable>(this.body.PerspectiveTableID, objectMap, throwIfCantResolve, null, null);
			this.body.SetID.ResolveById(objectMap, throwIfCantResolve);
			if (perspectiveTable != null)
			{
				perspectiveTable.PerspectiveSets.Add(this);
			}
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0005267C File Offset: 0x0005087C
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			this.body.SetID.ResolveById(objectMap, throwIfCantResolve);
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x00052694 File Offset: 0x00050894
		internal override bool TryResolveCrossLinksByPath(ICollection<string> linksFailedToResolve)
		{
			bool flag = true;
			if (!this.body.SetID.IsResolved && !this.body.SetID.TryResolveByPath())
			{
				if (linksFailedToResolve != null)
				{
					linksFailedToResolve.Add(string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "Set"));
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x000526E8 File Offset: 0x000508E8
		internal override void TryResolveCrossLinksAfterCopy(CopyContext copyContext)
		{
			if (!this.body.SetID.TryResolveAfterCopy(copyContext) && this.body.SetID.Path != null && !this.body.SetID.Path.IsEmpty)
			{
				this.body._name = this.body.SetID.Path[this.body.SetID.Path.Count - 1].Value;
			}
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x00052770 File Offset: 0x00050970
		internal override void ValidateObjectImpl(ValidationResult result, bool throwOnError)
		{
			this.body.SetID.Validate(result, throwOnError);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00052784 File Offset: 0x00050984
		internal override bool ContainsUnresolvedCrossLinksImpl()
		{
			return !this.body.SetID.IsResolved || base.ContainsUnresolvedCrossLinksImpl();
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x000527A0 File Offset: 0x000509A0
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._Annotations;
			yield return this._ExtendedProperties;
			yield break;
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x000527B0 File Offset: 0x000509B0
		public PerspectiveSetAnnotationCollection Annotations
		{
			get
			{
				return this._Annotations;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x000527B8 File Offset: 0x000509B8
		public PerspectiveSetExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this._ExtendedProperties;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x000527C0 File Offset: 0x000509C0
		// (set) Token: 0x060009E8 RID: 2536 RVA: 0x000527E1 File Offset: 0x000509E1
		public override string Name
		{
			get
			{
				if (this.Set != null)
				{
					return this.Set.Name;
				}
				return this.body._name;
			}
			set
			{
				if (this.Set != null)
				{
					throw new InvalidOperationException(TomSR.Exception_NameCannotBeSetForReferencedObjects);
				}
				this.body._name = value;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x00052802 File Offset: 0x00050A02
		// (set) Token: 0x060009EA RID: 2538 RVA: 0x00052810 File Offset: 0x00050A10
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

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x00052894 File Offset: 0x00050A94
		// (set) Token: 0x060009EC RID: 2540 RVA: 0x000528A8 File Offset: 0x00050AA8
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

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x0005292C File Offset: 0x00050B2C
		// (set) Token: 0x060009EE RID: 2542 RVA: 0x0005293E File Offset: 0x00050B3E
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

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x00052951 File Offset: 0x00050B51
		// (set) Token: 0x060009F0 RID: 2544 RVA: 0x00052964 File Offset: 0x00050B64
		public Set Set
		{
			get
			{
				return this.body.SetID.Object;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.SetID.Object, value))
				{
					if (this.body.SetID.Object != null)
					{
						throw new InvalidOperationException(TomSR.Exception_CantChangeImmutableProperty("Set", "PerspectiveSet"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "Set", typeof(Set), this.body.SetID.Object, value);
					Set @object = this.body.SetID.Object;
					this.body.SetID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Set", typeof(Set), @object, value);
				}
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x00052A12 File Offset: 0x00050C12
		// (set) Token: 0x060009F2 RID: 2546 RVA: 0x00052A24 File Offset: 0x00050C24
		internal ObjectId _SetID
		{
			get
			{
				return this.body.SetID.ObjectID;
			}
			set
			{
				this.body.SetID.ObjectID = value;
			}
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00052A38 File Offset: 0x00050C38
		internal void CopyFrom(PerspectiveSet other, CopyContext context)
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

		// Token: 0x060009F4 RID: 2548 RVA: 0x00052AFA File Offset: 0x00050CFA
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((PerspectiveSet)other, context);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00052B09 File Offset: 0x00050D09
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(PerspectiveSet other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00052B25 File Offset: 0x00050D25
		public void CopyTo(PerspectiveSet other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00052B41 File Offset: 0x00050D41
		public PerspectiveSet Clone()
		{
			return base.CloneInternal<PerspectiveSet>();
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00052B4C File Offset: 0x00050D4C
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.PerspectiveSet.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object PerspectiveSet is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (this.body.PerspectiveTableID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "PerspectiveTableID", this.body.PerspectiveTableID.Object);
			}
			this.body.SetID.Validate(null, true);
			if (this.body.SetID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "SetID", this.body.SetID.Object);
			}
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00052C08 File Offset: 0x00050E08
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("PerspectiveTableID", out objectId))
			{
				this.body.PerspectiveTableID.ObjectID = objectId;
			}
			ObjectId objectId2;
			if (reader.TryReadProperty<ObjectId>("SetID", out objectId2))
			{
				this.body.SetID.ObjectID = objectId2;
			}
			DateTime dateTime;
			if (reader.TryReadProperty<DateTime>("ModifiedTime", out dateTime))
			{
				this.body.ModifiedTime = dateTime;
			}
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00052C7C File Offset: 0x00050E7C
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.PerspectiveSet.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object PerspectiveSet is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataBodyProperties(context, writer);
			if (this.body.PerspectiveTableID.Object != null && writer.ShouldIncludeProperty("PerspectiveTableID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
			{
				writer.WriteObjectIdProperty("PerspectiveTableID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly, this.body.PerspectiveTableID.Object);
			}
			this.body.SetID.Validate(null, true);
			if (this.body.SetID.Object != null && writer.ShouldIncludeProperty("SetID", MetadataPropertyNature.CrossLinkProperty))
			{
				writer.WriteObjectIdProperty("SetID", MetadataPropertyNature.CrossLinkProperty, this.body.SetID.Object);
			}
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00052D68 File Offset: 0x00050F68
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.PerspectiveSet.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object PerspectiveSet is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
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

		// Token: 0x060009FC RID: 2556 RVA: 0x00052ED8 File Offset: 0x000510D8
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
				if (length <= 5)
				{
					if (length != 4)
					{
						if (length == 5)
						{
							if (propertyName == "SetID")
							{
								this.body.SetID.ObjectID = reader.ReadObjectIdProperty();
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
				else if (length != 11)
				{
					if (length == 12)
					{
						char c = propertyName[0];
						if (c != 'M')
						{
							if (c != 'm')
							{
								goto IL_0272;
							}
							if (!(propertyName == "modifiedTime"))
							{
								goto IL_0272;
							}
						}
						else if (!(propertyName == "ModifiedTime"))
						{
							goto IL_0272;
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
											catch (Exception ex)
											{
												throw reader.CreateInvalidChildException(context, extendedProperty, TomSR.Exception_FailedAddDeserializedNamedObject("ExtendedProperty", (extendedProperty != null) ? extendedProperty.Name : null, ex.Message), ex);
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
				else if (propertyName == "annotations")
				{
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
			IL_0272:
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x000531A8 File Offset: 0x000513A8
		private protected override void OnDeserializeEnd(SerializationActivityContext context)
		{
			base.OnDeserializeEnd(context);
			if (context.SerializationMode != MetadataSerializationMode.Xmla)
			{
				context.RegistrerObjectForMasterReferenceCrossLinkReconstruction(this);
			}
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x000531C4 File Offset: 0x000513C4
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.PerspectiveSet.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object PerspectiveSet is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
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

		// Token: 0x060009FF RID: 2559 RVA: 0x00053400 File Offset: 0x00051600
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

		// Token: 0x06000A00 RID: 2560 RVA: 0x000534C4 File Offset: 0x000516C4
		internal override bool BuildIndirectNameCrossLinkPathIfNeeded()
		{
			if (this.body.SetID.Object != null)
			{
				return true;
			}
			if (string.IsNullOrEmpty(this.body._name) || this.PerspectiveTable == null || string.IsNullOrEmpty(this.PerspectiveTable.Name))
			{
				return false;
			}
			if (this.body.SetID.Path == null || this.body.SetID.Path.IsEmpty)
			{
				this.body.SetID.Path = new ObjectPath(new KeyValuePair<ObjectType, string>[]
				{
					new KeyValuePair<ObjectType, string>(ObjectType.Table, this.PerspectiveTable.Name),
					new KeyValuePair<ObjectType, string>(ObjectType.Set, this.body._name)
				});
			}
			return true;
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00053589 File Offset: 0x00051789
		void ILinkedMetadataObject.GetLinkedObjectTarget(out ObjectId objectId, out ObjectPath objectPath, out MetadataObject @object, out string property)
		{
			objectId = this.body.SetID.ObjectID;
			objectPath = this.body.SetID.Path;
			@object = this.body.SetID.Object;
			property = null;
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x000535CC File Offset: 0x000517CC
		internal override string GetFormattedObjectPath()
		{
			if (this.Set == null)
			{
				return TomSR.ObjectPath_PerspectiveSet_0Args;
			}
			if (this.PerspectiveTable == null || this.PerspectiveTable.Table == null)
			{
				return TomSR.ObjectPath_PerspectiveSet_1Args(this.Set.Name);
			}
			if (this.PerspectiveTable.Perspective != null)
			{
				return TomSR.ObjectPath_PerspectiveSet_3Args(this.Set.Name, this.PerspectiveTable.Table.Name, this.PerspectiveTable.Perspective.Name);
			}
			return TomSR.ObjectPath_PerspectiveSet_2Args(this.Set.Name, this.PerspectiveTable.Table.Name);
		}

		// Token: 0x04000159 RID: 345
		internal PerspectiveSet.ObjectBody body;

		// Token: 0x0400015A RID: 346
		private PerspectiveSetAnnotationCollection _Annotations;

		// Token: 0x0400015B RID: 347
		private PerspectiveSetExtendedPropertyCollection _ExtendedProperties;

		// Token: 0x020002B3 RID: 691
		internal class ObjectBody : NamedMetadataObjectBody<PerspectiveSet>
		{
			// Token: 0x0600225A RID: 8794 RVA: 0x000DD38F File Offset: 0x000DB58F
			public ObjectBody(PerspectiveSet owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.PerspectiveTableID = new ParentLink<PerspectiveSet, PerspectiveTable>(owner, "PerspectiveTable");
				this.SetID = new CrossLink<PerspectiveSet, Set>(owner, "Set");
			}

			// Token: 0x0600225B RID: 8795 RVA: 0x000DD3C5 File Offset: 0x000DB5C5
			public override string GetObjectName()
			{
				if (this.SetID.Object == null)
				{
					return this._name;
				}
				return this.SetID.Object.Name;
			}

			// Token: 0x0600225C RID: 8796 RVA: 0x000DD3EC File Offset: 0x000DB5EC
			internal bool IsEqualTo(PerspectiveSet.ObjectBody other, CopyContext context)
			{
				return ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.PerspectiveTableID.IsEqualTo(other.PerspectiveTableID, context)) && this.SetID.IsEqualTo(other.SetID, context);
			}

			// Token: 0x0600225D RID: 8797 RVA: 0x000DD464 File Offset: 0x000DB664
			internal void CopyFromImpl(PerspectiveSet.ObjectBody other, CopyContext context)
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
				this.SetID.CopyFrom(other.SetID, context);
				this._name = other._name;
			}

			// Token: 0x0600225E RID: 8798 RVA: 0x000DD4E8 File Offset: 0x000DB6E8
			internal void CopyFromImpl(PerspectiveSet.ObjectBody other)
			{
				this.ModifiedTime = other.ModifiedTime;
				this.PerspectiveTableID.CopyFrom(other.PerspectiveTableID, ObjectChangeTracker.BodyCloneContext);
				this.SetID.CopyFrom(other.SetID, ObjectChangeTracker.BodyCloneContext);
				this._name = other._name;
			}

			// Token: 0x0600225F RID: 8799 RVA: 0x000DD539 File Offset: 0x000DB739
			public override void CopyFrom(MetadataObjectBody<PerspectiveSet> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((PerspectiveSet.ObjectBody)other, context);
			}

			// Token: 0x06002260 RID: 8800 RVA: 0x000DD550 File Offset: 0x000DB750
			internal bool IsEqualTo(PerspectiveSet.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && this.PerspectiveTableID.IsEqualTo(other.PerspectiveTableID) && this.SetID.IsEqualTo(other.SetID);
			}

			// Token: 0x06002261 RID: 8801 RVA: 0x000DD59D File Offset: 0x000DB79D
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((PerspectiveSet.ObjectBody)other);
			}

			// Token: 0x06002262 RID: 8802 RVA: 0x000DD5B8 File Offset: 0x000DB7B8
			internal void CompareWith(PerspectiveSet.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				this.PerspectiveTableID.CompareWith(other.PerspectiveTableID, "PerspectiveTableID", "PerspectiveTable", PropertyFlags.ReadOnly, context);
				this.SetID.CompareWith(other.SetID, "SetID", "Set", PropertyFlags.None, context);
			}

			// Token: 0x06002263 RID: 8803 RVA: 0x000DD644 File Offset: 0x000DB844
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((PerspectiveSet.ObjectBody)other, context);
			}

			// Token: 0x040009C6 RID: 2502
			internal DateTime ModifiedTime;

			// Token: 0x040009C7 RID: 2503
			internal ParentLink<PerspectiveSet, PerspectiveTable> PerspectiveTableID;

			// Token: 0x040009C8 RID: 2504
			internal CrossLink<PerspectiveSet, Set> SetID;

			// Token: 0x040009C9 RID: 2505
			internal string _name;
		}
	}
}
