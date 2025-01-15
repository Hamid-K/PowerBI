using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000072 RID: 114
	public sealed class LinguisticMetadata : MetadataObject
	{
		// Token: 0x06000626 RID: 1574 RVA: 0x0002F618 File Offset: 0x0002D818
		public LinguisticMetadata()
		{
			this.InitBodyAndCollections(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0002F62B File Offset: 0x0002D82B
		internal LinguisticMetadata(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections(comparer);
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0002F63C File Offset: 0x0002D83C
		private void InitBodyAndCollections(IEqualityComparer<string> comparer)
		{
			this.body = new LinguisticMetadata.ObjectBody(this);
			this.body.Content = string.Empty;
			this.body.ContentType = ContentType.Xml;
			this._Annotations = new LinguisticMetadataAnnotationCollection(this, comparer);
			this._ExtendedProperties = new LinguisticMetadataExtendedPropertyCollection(this, comparer);
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x0002F68B File Offset: 0x0002D88B
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.LinguisticMetadata;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x0002F68F File Offset: 0x0002D88F
		// (set) Token: 0x0600062B RID: 1579 RVA: 0x0002F6A1 File Offset: 0x0002D8A1
		public override MetadataObject Parent
		{
			get
			{
				return this.body.CultureID.Object;
			}
			internal set
			{
				if (this.body.CultureID.Object != value)
				{
					MetadataObject.UpdateMetadataObjectParent<LinguisticMetadata, Culture>(this.body.CultureID, (Culture)value, "LinguisticMetadata", null);
				}
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0002F6D2 File Offset: 0x0002D8D2
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.CultureID.ObjectID;
			}
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0002F6E4 File Offset: 0x0002D8E4
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.LinguisticMetadata, null, "LinguisticMetadata object of Tabular Object Model (TOM)", new bool?(false)))
			{
				LinguisticMetadata.WriteMetadataSchemaOfContent(context, writer);
				if (writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				if (CompatibilityRestrictions.LinguisticMetadata_ContentType.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("contentType", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<ContentType>("contentType", MetadataPropertyNature.RegularProperty, null);
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

		// Token: 0x0600062E RID: 1582 RVA: 0x0002F7DC File Offset: 0x0002D9DC
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			base.GetCompatibilityRequirementByMembers(mode, out requiredLevel, out requestingPath);
			if (requiredLevel == -2)
			{
				return;
			}
			if (this.body.ContentType != ContentType.Xml)
			{
				int num;
				CompatibilityRestrictionSet.MergeLevelDemand(CompatibilityRestrictions.LinguisticMetadata_ContentType[mode], PropertyHelper.GetContentTypeCompatibilityRestrictions(this.body.ContentType)[mode], out num);
				if (CompatibilityRestrictionSet.CompareLevel(num, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ContentType");
					requiredLevel = num;
					int num2 = requiredLevel;
					return;
				}
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x0002F85B File Offset: 0x0002DA5B
		// (set) Token: 0x06000630 RID: 1584 RVA: 0x0002F863 File Offset: 0x0002DA63
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (LinguisticMetadata.ObjectBody)value;
			}
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0002F871 File Offset: 0x0002DA71
		internal override ITxObjectBody CreateBody()
		{
			return new LinguisticMetadata.ObjectBody(this);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0002F879 File Offset: 0x0002DA79
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new LinguisticMetadata();
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0002F880 File Offset: 0x0002DA80
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return null;
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0002F884 File Offset: 0x0002DA84
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			Culture culture = MetadataObject.ResolveMetadataObjectParentById<LinguisticMetadata, Culture>(this.body.CultureID, objectMap, throwIfCantResolve, "LinguisticMetadata", null);
			if (culture != null && culture.Model != null)
			{
				foreach (MetadataObject metadataObject in base.GetChildren(false))
				{
					culture.Model.NotifySubtreeAdded(metadataObject);
				}
			}
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0002F8FC File Offset: 0x0002DAFC
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0002F8FE File Offset: 0x0002DAFE
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._Annotations;
			yield return this._ExtendedProperties;
			yield break;
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x0002F90E File Offset: 0x0002DB0E
		public LinguisticMetadataAnnotationCollection Annotations
		{
			get
			{
				return this._Annotations;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x0002F916 File Offset: 0x0002DB16
		[CompatibilityRequirement("1400")]
		public LinguisticMetadataExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this._ExtendedProperties;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x0002F91E File Offset: 0x0002DB1E
		// (set) Token: 0x0600063A RID: 1594 RVA: 0x0002F92C File Offset: 0x0002DB2C
		public string Content
		{
			get
			{
				return this.body.Content;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Content, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Content", typeof(string), this.body.Content, value);
					LinguisticMetadata.ValidateContent(value, this.ContentType);
					string content = this.body.Content;
					this.body.Content = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Content", typeof(string), content, value);
				}
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x0002F9A8 File Offset: 0x0002DBA8
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x0002F9B8 File Offset: 0x0002DBB8
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

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x0002FA3C File Offset: 0x0002DC3C
		// (set) Token: 0x0600063E RID: 1598 RVA: 0x0002FA4C File Offset: 0x0002DC4C
		[CompatibilityRequirement("1465")]
		public ContentType ContentType
		{
			get
			{
				return this.body.ContentType;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.ContentType, value))
				{
					CompatibilityRestrictionSet compatibilityRestrictionSet = CompatibilityRestrictions.LinguisticMetadata_ContentType.Merge(PropertyHelper.GetContentTypeCompatibilityRestrictions(value));
					CompatibilityRestrictionSet compatibilityRestrictionSet2 = CompatibilityRestrictions.LinguisticMetadata_ContentType.Merge(PropertyHelper.GetContentTypeCompatibilityRestrictions(this.body.ContentType));
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					RestrictionsComapreResult restrictionsComapreResult = compatibilityRestrictionSet.Compare(compatibilityRestrictionSet2);
					if (restrictionsComapreResult == RestrictionsComapreResult.MoreRestrictive || restrictionsComapreResult == RestrictionsComapreResult.Incomparable || (restrictionsComapreResult == RestrictionsComapreResult.Equal && value != ContentType.Xml))
					{
						array = base.ValidateCompatibilityRequirement(compatibilityRestrictionSet, string.Format("[{0}]::[{1}]=[{2}]", this.GetFormattedObjectPath(), "ContentType", value));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "ContentType", typeof(ContentType), this.body.ContentType, value);
					switch (restrictionsComapreResult)
					{
					case RestrictionsComapreResult.Incomparable:
						base.ResetCompatibilityRequirement();
						base.SetCompatibilityRequirement(compatibilityRestrictionSet, array);
						break;
					case RestrictionsComapreResult.LessRestrictive:
						base.ResetCompatibilityRequirement();
						break;
					case RestrictionsComapreResult.Equal:
						if (array != null)
						{
							base.SetCompatibilityRequirement(compatibilityRestrictionSet, array);
						}
						break;
					case RestrictionsComapreResult.MoreRestrictive:
						base.SetCompatibilityRequirement(compatibilityRestrictionSet, array);
						break;
					}
					LinguisticMetadata.ValidateContent(this.Content, value);
					ContentType contentType = this.body.ContentType;
					this.body.ContentType = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "ContentType", typeof(ContentType), contentType, value);
				}
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x0002FB8D File Offset: 0x0002DD8D
		// (set) Token: 0x06000640 RID: 1600 RVA: 0x0002FBA0 File Offset: 0x0002DDA0
		public Culture Culture
		{
			get
			{
				return this.body.CultureID.Object;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.CultureID.Object, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Culture", typeof(Culture), this.body.CultureID.Object, value);
					Culture @object = this.body.CultureID.Object;
					this.body.CultureID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Culture", typeof(Culture), @object, value);
				}
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x0002FC24 File Offset: 0x0002DE24
		// (set) Token: 0x06000642 RID: 1602 RVA: 0x0002FC36 File Offset: 0x0002DE36
		internal ObjectId _CultureID
		{
			get
			{
				return this.body.CultureID.ObjectID;
			}
			set
			{
				this.body.CultureID.ObjectID = value;
			}
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0002FC4C File Offset: 0x0002DE4C
		internal void CopyFrom(LinguisticMetadata other, CopyContext context)
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

		// Token: 0x06000644 RID: 1604 RVA: 0x0002FD0E File Offset: 0x0002DF0E
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((LinguisticMetadata)other, context);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0002FD1D File Offset: 0x0002DF1D
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(LinguisticMetadata other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0002FD39 File Offset: 0x0002DF39
		public void CopyTo(LinguisticMetadata other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0002FD55 File Offset: 0x0002DF55
		public LinguisticMetadata Clone()
		{
			return base.CloneInternal<LinguisticMetadata>();
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0002FD60 File Offset: 0x0002DF60
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (this.body.CultureID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "CultureID", this.body.CultureID.Object);
			}
			if (!string.IsNullOrEmpty(this.body.Content))
			{
				writer.WriteProperty<string>(options, "Content", this.body.Content);
			}
			if (this.body.ContentType != ContentType.Xml)
			{
				if (!CompatibilityRestrictions.LinguisticMetadata_ContentType.IsCompatible(mode, dbCompatibilityLevel) || !PropertyHelper.IsContentTypeValueCompatible(this.body.ContentType, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ContentType is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<ContentType>(options, "ContentType", this.body.ContentType);
			}
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0002FE3C File Offset: 0x0002E03C
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("CultureID", out objectId))
			{
				this.body.CultureID.ObjectID = objectId;
			}
			string text;
			if (reader.TryReadProperty<string>("Content", out text))
			{
				this.body.Content = text;
			}
			DateTime dateTime;
			if (reader.TryReadProperty<DateTime>("ModifiedTime", out dateTime))
			{
				this.body.ModifiedTime = dateTime;
			}
			ContentType contentType;
			if (CompatibilityRestrictions.LinguisticMetadata_ContentType.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<ContentType>("ContentType", out contentType))
			{
				this.body.ContentType = contentType;
			}
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0002FED4 File Offset: 0x0002E0D4
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataBodyProperties(context, writer);
			if (this.body.CultureID.Object != null && writer.ShouldIncludeProperty("CultureID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
			{
				writer.WriteObjectIdProperty("CultureID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly, this.body.CultureID.Object);
			}
			if (!string.IsNullOrEmpty(this.body.Content) && writer.ShouldIncludeProperty("Content", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty))
			{
				writer.WriteStringProperty("Content", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty, this.body.Content);
			}
			if (this.body.ContentType != ContentType.Xml)
			{
				if (!CompatibilityRestrictions.LinguisticMetadata_ContentType.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) || !PropertyHelper.IsContentTypeValueCompatible(this.body.ContentType, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ContentType is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("ContentType", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<ContentType>("ContentType", MetadataPropertyNature.RegularProperty, this.body.ContentType);
				}
			}
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00030004 File Offset: 0x0002E204
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataTree(context, writer);
			this.WriteContentMetadataPropertyToMetadataStream(context, writer);
			if (this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.ModifiedTime);
			}
			if (this.body.ContentType != ContentType.Xml)
			{
				if (!CompatibilityRestrictions.LinguisticMetadata_ContentType.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) || !PropertyHelper.IsContentTypeValueCompatible(this.body.ContentType, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ContentType is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("contentType", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<ContentType>("contentType", MetadataPropertyNature.RegularProperty, this.body.ContentType);
				}
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

		// Token: 0x0600064C RID: 1612 RVA: 0x000301A0 File Offset: 0x0002E3A0
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
				case 7:
					if (propertyName == "Content")
					{
						this.body.Content = reader.ReadStringProperty();
						return true;
					}
					break;
				case 8:
				case 10:
					break;
				case 9:
					if (propertyName == "CultureID")
					{
						this.body.CultureID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				case 11:
				{
					char c = propertyName[0];
					if (c != 'C')
					{
						if (c != 'a')
						{
							if (c != 'c')
							{
								break;
							}
							if (!(propertyName == "contentType"))
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
					else if (!(propertyName == "ContentType"))
					{
						break;
					}
					if (!CompatibilityRestrictions.LinguisticMetadata_ContentType.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) || !CompatibilityRestrictions.ContentType.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.ContentType = reader.ReadEnumProperty<ContentType>();
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
									catch (Exception ex2)
									{
										throw reader.CreateInvalidChildException(context, extendedProperty, TomSR.Exception_FailedAddDeserializedNamedObject("ExtendedProperty", (extendedProperty != null) ? extendedProperty.Name : null, ex2.Message), ex2);
									}
								}
							}
							return true;
						}
					}
					break;
				}
			}
			if (context.SerializationMode != MetadataSerializationMode.Xmla && this.TryReadContentPropertyFromMetadataStream(context, reader))
			{
				return true;
			}
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x000304E0 File Offset: 0x0002E6E0
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["modifiedTime", TomPropCategory.Regular, 3, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.ModifiedTime);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.ContentType != ContentType.Xml)
			{
				if (!CompatibilityRestrictions.LinguisticMetadata_ContentType.IsCompatible(mode, dbCompatibilityLevel) || !PropertyHelper.IsContentTypeValueCompatible(this.body.ContentType, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ContentType is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["contentType", TomPropCategory.Regular, 4, false] = JsonPropertyHelper.ConvertEnumToJsonValue<ContentType>(this.body.ContentType);
			}
			this.SerializeAdditionalDataToJsonObject(result, options, mode, dbCompatibilityLevel);
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

		// Token: 0x0600064E RID: 1614 RVA: 0x00030768 File Offset: 0x0002E968
		private void SerializeAdditionalDataToJsonObject(JsonObject jsonObj, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.Content))
			{
				if (this.ContentType == ContentType.Json)
				{
					jsonObj["content", TomPropCategory.Regular, 2, false] = JsonPropertyHelper.ConvertStringToJsonObject(this.body.Content, "Content");
					return;
				}
				jsonObj["content", TomPropCategory.Regular, 2, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Content, SplitMultilineOptions.None);
			}
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x000307DC File Offset: 0x0002E9DC
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name == "modifiedTime")
			{
				this.body.ModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
				return true;
			}
			if (!(name == "contentType"))
			{
				if (!(name == "extendedProperties"))
				{
					if (!(name == "annotations"))
					{
						bool flag = false;
						this.ReadAdditionalPropertyFromJson(jsonProp, options, mode, dbCompatibilityLevel, ref flag);
						return flag;
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
				ContentType contentType = JsonPropertyHelper.ConvertJsonValueToEnum<ContentType>(jsonProp.Value);
				if (jsonProp.Value.Type != 10 && (!CompatibilityRestrictions.LinguisticMetadata_ContentType.IsCompatible(mode, dbCompatibilityLevel) || !PropertyHelper.IsContentTypeValueCompatible(contentType, mode, dbCompatibilityLevel)))
				{
					return false;
				}
				this.body.ContentType = contentType;
				return true;
			}
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x000308E8 File Offset: 0x0002EAE8
		private void ReadAdditionalPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel, ref bool wasRead)
		{
			if (jsonProp.Name == "content")
			{
				if (this.body.ContentType == ContentType.Json)
				{
					this.body.Content = JsonPropertyHelper.ConvertJsonContentToString(jsonProp.Value);
				}
				else
				{
					this.body.Content = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
				}
				wasRead = true;
				return;
			}
			wasRead = false;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0003094C File Offset: 0x0002EB4C
		private protected override void ReadMetadataProperties(SerializationActivityContext context, IMetadataReader reader)
		{
			base.ReadMetadataProperties(context, reader);
			try
			{
				LinguisticMetadata.ValidateContent(this.body.Content, this.body.ContentType);
			}
			catch (Exception ex)
			{
				throw reader.CreateInvalidDataException(context, string.Format("The context '{0}' does not comply with the {1} content-type!", this.body.Content, this.body.ContentType), ex);
			}
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x000309C0 File Offset: 0x0002EBC0
		private static void ValidateContent(string content, ContentType type)
		{
			if (!string.IsNullOrEmpty(content))
			{
				if (type == ContentType.Xml)
				{
					XDocument.Parse(content);
					return;
				}
				if (type == ContentType.Json)
				{
					JsonPropertyHelper.ConvertStringToJsonObject(content, "Content");
				}
			}
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x000309E5 File Offset: 0x0002EBE5
		internal override string GetFormattedObjectPath()
		{
			if (this.Culture != null)
			{
				return TomSR.ObjectPath_LinguisticMetadata_1Arg(this.Culture.Name);
			}
			return TomSR.ObjectPath_LinguisticMetadata_0Args;
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00030A08 File Offset: 0x0002EC08
		private static void WriteMetadataSchemaOfContent(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			if (writer.ShouldIncludeProperty("content", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteProperty("content", MetadataPropertyNature.RegularProperty, null);
				using (writer.CreateChoiceScope())
				{
					writer.WriteProperty("content", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty, typeof(string));
					if (PropertyHelper.IsContentTypeValueCompatible(ContentType.Json, context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						writer.WriteProperty("content", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.JsonString | MetadataPropertyNature.DefaultProperty, typeof(string));
					}
				}
			}
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00030A9C File Offset: 0x0002EC9C
		private void WriteContentMetadataPropertyToMetadataStream(SerializationActivityContext context, IMetadataWriter writer)
		{
			MetadataPropertyNature metadataPropertyNature = MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty;
			if (this.body.ContentType == ContentType.Json)
			{
				metadataPropertyNature |= MetadataPropertyNature.JsonString;
			}
			if (!string.IsNullOrEmpty(this.body.Content) && writer.ShouldIncludeProperty("content", metadataPropertyNature))
			{
				writer.WriteStringProperty("content", metadataPropertyNature, this.body.Content);
			}
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00030AFC File Offset: 0x0002ECFC
		private bool TryReadContentPropertyFromMetadataStream(SerializationActivityContext context, IMetadataReader reader)
		{
			if (reader.PropertyName == "content")
			{
				this.body.Content = reader.ReadStringProperty();
				return true;
			}
			return false;
		}

		// Token: 0x04000112 RID: 274
		internal LinguisticMetadata.ObjectBody body;

		// Token: 0x04000113 RID: 275
		private LinguisticMetadataAnnotationCollection _Annotations;

		// Token: 0x04000114 RID: 276
		private LinguisticMetadataExtendedPropertyCollection _ExtendedProperties;

		// Token: 0x0200027B RID: 635
		internal class ObjectBody : MetadataObjectBody<LinguisticMetadata>
		{
			// Token: 0x060020C1 RID: 8385 RVA: 0x000D650C File Offset: 0x000D470C
			public ObjectBody(LinguisticMetadata owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.CultureID = new ParentLink<LinguisticMetadata, Culture>(owner, "Culture");
			}

			// Token: 0x060020C2 RID: 8386 RVA: 0x000D6534 File Offset: 0x000D4734
			internal bool IsEqualTo(LinguisticMetadata.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.Content, other.Content) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && PropertyHelper.AreValuesIdentical(this.ContentType, other.ContentType) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.CultureID.IsEqualTo(other.CultureID, context));
			}

			// Token: 0x060020C3 RID: 8387 RVA: 0x000D65C0 File Offset: 0x000D47C0
			internal void CopyFromImpl(LinguisticMetadata.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				this.Content = other.Content;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModifiedTime = other.ModifiedTime;
				}
				this.ContentType = other.ContentType;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.CultureID.CopyFrom(other.CultureID, context);
				}
			}

			// Token: 0x060020C4 RID: 8388 RVA: 0x000D663C File Offset: 0x000D483C
			internal void CopyFromImpl(LinguisticMetadata.ObjectBody other)
			{
				this.Content = other.Content;
				this.ModifiedTime = other.ModifiedTime;
				this.ContentType = other.ContentType;
				this.CultureID.CopyFrom(other.CultureID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x060020C5 RID: 8389 RVA: 0x000D6678 File Offset: 0x000D4878
			public override void CopyFrom(MetadataObjectBody<LinguisticMetadata> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((LinguisticMetadata.ObjectBody)other, context);
			}

			// Token: 0x060020C6 RID: 8390 RVA: 0x000D6690 File Offset: 0x000D4890
			internal bool IsEqualTo(LinguisticMetadata.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.Content, other.Content) && PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && PropertyHelper.AreValuesIdentical(this.ContentType, other.ContentType) && this.CultureID.IsEqualTo(other.CultureID);
			}

			// Token: 0x060020C7 RID: 8391 RVA: 0x000D66F2 File Offset: 0x000D48F2
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((LinguisticMetadata.ObjectBody)other);
			}

			// Token: 0x060020C8 RID: 8392 RVA: 0x000D670C File Offset: 0x000D490C
			internal void CompareWith(LinguisticMetadata.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.Content, other.Content))
				{
					context.RegisterPropertyChange(base.Owner, "Content", typeof(string), PropertyFlags.DdlAndUser, other.Content, this.Content);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ContentType, other.ContentType))
				{
					context.RegisterPropertyChange(base.Owner, "ContentType", typeof(ContentType), PropertyFlags.DdlAndUser, other.ContentType, this.ContentType);
				}
				this.CultureID.CompareWith(other.CultureID, "CultureID", "Culture", PropertyFlags.ReadOnly, context);
			}

			// Token: 0x060020C9 RID: 8393 RVA: 0x000D67FB File Offset: 0x000D49FB
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((LinguisticMetadata.ObjectBody)other, context);
			}

			// Token: 0x04000896 RID: 2198
			internal string Content;

			// Token: 0x04000897 RID: 2199
			internal DateTime ModifiedTime;

			// Token: 0x04000898 RID: 2200
			internal ContentType ContentType;

			// Token: 0x04000899 RID: 2201
			internal ParentLink<LinguisticMetadata, Culture> CultureID;
		}
	}
}
