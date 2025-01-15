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
	// Token: 0x0200004B RID: 75
	public sealed class Culture : NamedMetadataObject
	{
		// Token: 0x06000339 RID: 825 RVA: 0x000196D4 File Offset: 0x000178D4
		public Culture()
		{
			this.InitBodyAndCollections(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x000196E7 File Offset: 0x000178E7
		internal Culture(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections(comparer);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x000196F8 File Offset: 0x000178F8
		private void InitBodyAndCollections(IEqualityComparer<string> comparer)
		{
			this.body = new Culture.ObjectBody(this);
			this.body.Name = string.Empty;
			this._ObjectTranslations = new ObjectTranslationCollection(this);
			this._Annotations = new CultureAnnotationCollection(this, comparer);
			this._ExtendedProperties = new CultureExtendedPropertyCollection(this, comparer);
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00019747 File Offset: 0x00017947
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.Culture;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0001974B File Offset: 0x0001794B
		// (set) Token: 0x0600033E RID: 830 RVA: 0x0001975D File Offset: 0x0001795D
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
					MetadataObject.UpdateMetadataObjectParent<Culture, Model>(this.body.ModelID, (Model)value, null, null);
				}
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0001978A File Offset: 0x0001798A
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.ModelID.ObjectID;
			}
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0001979C File Offset: 0x0001799C
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.Culture, null, "Culture object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
				{
					writer.WriteProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, typeof(string));
				}
				if (writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				if (writer.ShouldIncludeProperty("structureModifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("structureModifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				if (writer.ShouldIncludeProperty("linguisticMetadata", MetadataPropertyNature.ChildProperty))
				{
					writer.WriteSingleChild(context, "linguisticMetadata", MetadataPropertyNature.ChildProperty, ObjectType.LinguisticMetadata);
				}
				Culture.WriteMetadataSchemaOfObjectTranslations(context, writer);
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

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000341 RID: 833 RVA: 0x000198D4 File Offset: 0x00017AD4
		// (set) Token: 0x06000342 RID: 834 RVA: 0x000198DC File Offset: 0x00017ADC
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (Culture.ObjectBody)value;
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x000198EA File Offset: 0x00017AEA
		internal override ITxObjectBody CreateBody()
		{
			return new Culture.ObjectBody(this);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x000198F2 File Offset: 0x00017AF2
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new Culture();
		}

		// Token: 0x06000345 RID: 837 RVA: 0x000198F9 File Offset: 0x00017AF9
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return ((Model)parent).Cultures;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00019908 File Offset: 0x00017B08
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			Model model = MetadataObject.ResolveMetadataObjectParentById<Culture, Model>(this.body.ModelID, objectMap, throwIfCantResolve, null, null);
			this.body.LinguisticMetadataID.ResolveById(objectMap, throwIfCantResolve);
			if (model != null)
			{
				model.Cultures.Add(this);
			}
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0001994C File Offset: 0x00017B4C
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0001994E File Offset: 0x00017B4E
		internal override IEnumerable<MetadataObject> GetDirectChildren(bool isLogicalStructure)
		{
			if (this.body.LinguisticMetadataID.Object != null)
			{
				yield return this.body.LinguisticMetadataID.Object;
			}
			yield break;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0001995E File Offset: 0x00017B5E
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._ObjectTranslations;
			yield return this._Annotations;
			yield return this._ExtendedProperties;
			yield break;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00019970 File Offset: 0x00017B70
		private protected override void SetDirectChildImpl(MetadataObject child)
		{
			if (child.ObjectType == ObjectType.LinguisticMetadata)
			{
				ObjectChangeTracker.RegisterPropertyChanging(this, "LinguisticMetadata", typeof(LinguisticMetadata), this.body.LinguisticMetadataID.Object, child);
				LinguisticMetadata @object = this.body.LinguisticMetadataID.Object;
				this.body.LinguisticMetadataID.Object = (LinguisticMetadata)child;
				ObjectChangeTracker.RegisterPropertyChanged(this, "LinguisticMetadata", typeof(LinguisticMetadata), @object, child);
				return;
			}
			base.SetDirectChildImpl(child);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x000199F4 File Offset: 0x00017BF4
		private protected override void RemoveDirectChildImpl(MetadataObject child)
		{
			if (child.ObjectType == ObjectType.LinguisticMetadata)
			{
				if (this.body.LinguisticMetadataID.ObjectID == child.Id)
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "LinguisticMetadata", typeof(LinguisticMetadata), this.body.LinguisticMetadataID.Object, null);
					LinguisticMetadata @object = this.body.LinguisticMetadataID.Object;
					this.body.LinguisticMetadataID.Object = null;
					ObjectChangeTracker.RegisterPropertyChanged(this, "LinguisticMetadata", typeof(LinguisticMetadata), @object, null);
					return;
				}
			}
			else
			{
				base.RemoveDirectChildImpl(child);
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600034C RID: 844 RVA: 0x00019A8F File Offset: 0x00017C8F
		public ObjectTranslationCollection ObjectTranslations
		{
			get
			{
				return this._ObjectTranslations;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00019A97 File Offset: 0x00017C97
		public CultureAnnotationCollection Annotations
		{
			get
			{
				return this._Annotations;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00019A9F File Offset: 0x00017C9F
		[CompatibilityRequirement("1400")]
		public CultureExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this._ExtendedProperties;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00019AA7 File Offset: 0x00017CA7
		// (set) Token: 0x06000350 RID: 848 RVA: 0x00019AB4 File Offset: 0x00017CB4
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
					if (!Utils.IsSyntacticallyValidName(value, ObjectType.Culture, out text))
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

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00019B37 File Offset: 0x00017D37
		// (set) Token: 0x06000352 RID: 850 RVA: 0x00019B44 File Offset: 0x00017D44
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

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000353 RID: 851 RVA: 0x00019BC8 File Offset: 0x00017DC8
		// (set) Token: 0x06000354 RID: 852 RVA: 0x00019BD8 File Offset: 0x00017DD8
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

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00019C5C File Offset: 0x00017E5C
		// (set) Token: 0x06000356 RID: 854 RVA: 0x00019C6E File Offset: 0x00017E6E
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

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00019C81 File Offset: 0x00017E81
		// (set) Token: 0x06000358 RID: 856 RVA: 0x00019C94 File Offset: 0x00017E94
		public LinguisticMetadata LinguisticMetadata
		{
			get
			{
				return this.body.LinguisticMetadataID.Object;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.LinguisticMetadataID.Object, value))
				{
					if (value != null)
					{
						base.ValidateCompatibilityRequirement(value, "LinguisticMetadata", null);
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "LinguisticMetadata", typeof(LinguisticMetadata), this.body.LinguisticMetadataID.Object, value);
					LinguisticMetadata @object = this.body.LinguisticMetadataID.Object;
					this.body.LinguisticMetadataID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "LinguisticMetadata", typeof(LinguisticMetadata), @object, value);
				}
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00019D29 File Offset: 0x00017F29
		// (set) Token: 0x0600035A RID: 858 RVA: 0x00019D3B File Offset: 0x00017F3B
		internal ObjectId _LinguisticMetadataID
		{
			get
			{
				return this.body.LinguisticMetadataID.ObjectID;
			}
			set
			{
				this.body.LinguisticMetadataID.ObjectID = value;
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00019D50 File Offset: 0x00017F50
		internal void CopyFrom(Culture other, CopyContext context)
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
			else if ((context.Flags & CopyFlags.ShallowCopy) != CopyFlags.ShallowCopy && this.body.LinguisticMetadataID.Object != null && other.body.LinguisticMetadataID.Object != null)
			{
				this.body.LinguisticMetadataID.Object.CopyFrom(other.body.LinguisticMetadataID.Object, context);
			}
			if ((context.Flags & CopyFlags.ShallowCopy) != CopyFlags.ShallowCopy)
			{
				this.ObjectTranslations.CopyFrom(other.ObjectTranslations, context);
				this.Annotations.CopyFrom(other.Annotations, context);
				this.ExtendedProperties.CopyFrom(other.ExtendedProperties, context);
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00019E9D File Offset: 0x0001809D
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((Culture)other, context);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00019EAC File Offset: 0x000180AC
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(Culture other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00019EC8 File Offset: 0x000180C8
		public void CopyTo(Culture other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00019EE4 File Offset: 0x000180E4
		public Culture Clone()
		{
			return base.CloneInternal<Culture>();
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00019EEC File Offset: 0x000180EC
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (!string.IsNullOrEmpty(this.body.Name))
			{
				writer.WriteProperty<string>(options, "Name", this.body.Name);
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00019F24 File Offset: 0x00018124
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("ModelID", out objectId))
			{
				this.body.ModelID.ObjectID = objectId;
			}
			ObjectId objectId2;
			if (reader.TryReadProperty<ObjectId>("LinguisticMetadataID", out objectId2))
			{
				this.body.LinguisticMetadataID.ObjectID = objectId2;
			}
			string text;
			if (reader.TryReadProperty<string>("Name", out text))
			{
				this.body.Name = text;
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
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00019FCC File Offset: 0x000181CC
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataBodyProperties(context, writer);
			if (!string.IsNullOrEmpty(this.body.Name) && writer.ShouldIncludeProperty("Name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("Name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0001A020 File Offset: 0x00018220
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataTree(context, writer);
			if (!string.IsNullOrEmpty(this.body.Name) && writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
			if (this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.ModifiedTime);
			}
			if (this.body.StructureModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("structureModifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("structureModifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.StructureModifiedTime);
			}
			if (this.body.LinguisticMetadataID.Object != null && writer.ShouldIncludeProperty("linguisticMetadata", MetadataPropertyNature.ChildProperty))
			{
				writer.WriteSingleChild(context, "linguisticMetadata", MetadataPropertyNature.ChildProperty, this.body.LinguisticMetadataID.Object);
			}
			if (this.ObjectTranslations.Count > 0)
			{
				this.WriteObjectTranslationsToMetadataStream(context, writer);
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

		// Token: 0x06000364 RID: 868 RVA: 0x0001A1F4 File Offset: 0x000183F4
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
					if (length == 4)
					{
						char c = propertyName[0];
						if (c != 'N')
						{
							if (c != 'n')
							{
								goto IL_039D;
							}
							if (!(propertyName == "name"))
							{
								goto IL_039D;
							}
						}
						else if (!(propertyName == "Name"))
						{
							goto IL_039D;
						}
						this.body.Name = reader.ReadStringProperty();
						return true;
					}
					if (length == 7)
					{
						if (propertyName == "ModelID")
						{
							this.body.ModelID.ObjectID = reader.ReadObjectIdProperty();
							return true;
						}
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
								goto IL_039D;
							}
							if (!(propertyName == "modifiedTime"))
							{
								goto IL_039D;
							}
						}
						else if (!(propertyName == "ModifiedTime"))
						{
							goto IL_039D;
						}
						this.body.ModifiedTime = reader.ReadDateTimeProperty();
						return true;
					}
					switch (length)
					{
					case 18:
					{
						char c = propertyName[0];
						if (c != 'e')
						{
							if (c == 'l')
							{
								if (propertyName == "linguisticMetadata")
								{
									using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
									{
										LinguisticMetadata linguisticMetadata = reader.ReadSingleChildProperty<LinguisticMetadata>(context);
										try
										{
											this.body.LinguisticMetadataID.Object = linguisticMetadata;
										}
										catch (Exception ex)
										{
											throw reader.CreateInvalidChildException(context, linguisticMetadata, TomSR.Exception_FailedAddDeserializedObject("LinguisticMetadata", ex.Message), ex);
										}
									}
									return true;
								}
							}
						}
						else if (propertyName == "extendedProperties")
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
					case 20:
						if (propertyName == "LinguisticMetadataID")
						{
							this.body.LinguisticMetadataID.ObjectID = reader.ReadObjectIdProperty();
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
							catch (Exception ex3)
							{
								throw reader.CreateInvalidChildException(context, annotation, TomSR.Exception_FailedAddDeserializedNamedObject("Annotation", (annotation != null) ? annotation.Name : null, ex3.Message), ex3);
							}
						}
					}
					return true;
				}
			}
			IL_039D:
			if (context.SerializationMode != MetadataSerializationMode.Xmla && this.TryReadObjectTranslationsFromMetadataStream(context, reader))
			{
				return true;
			}
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0001A61C File Offset: 0x0001881C
		[Obsolete("Deprecated. Use RequestRename method instead.", false)]
		public void Rename(string newName)
		{
			this.RequestRename(newName);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0001A625 File Offset: 0x00018825
		public void RequestRename(string newName)
		{
			ObjectChangeTracker.RegisterObjectRenaming(this);
			this.Name = newName;
			this.body.RenameRequestedThroughAPI = true;
			ObjectChangeTracker.RegisterObjectRenamed(this);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0001A648 File Offset: 0x00018848
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!string.IsNullOrEmpty(this.body.Name))
			{
				result["name", TomPropCategory.Name, 2, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Name, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["modifiedTime", TomPropCategory.Regular, 4, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.ModifiedTime);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.StructureModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["structureModifiedTime", TomPropCategory.Regular, 5, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.StructureModifiedTime);
			}
			this.SerializeAdditionalDataToJsonObject(result, options, mode, dbCompatibilityLevel);
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations && this.body.LinguisticMetadataID.Object != null && !options.IncludeTranslatablePropertiesOnly && (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(this.body.LinguisticMetadataID.Object)))
			{
				result["linguisticMetadata", TomPropCategory.ChildLink, 3, false] = this.body.LinguisticMetadataID.Object.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject();
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

		// Token: 0x06000368 RID: 872 RVA: 0x0001A958 File Offset: 0x00018B58
		private void SerializeAdditionalDataToJsonObject(JsonObject jsonObj, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (options.IncludeTranslatablePropertiesOnly)
			{
				Utils.Verify(!ObjectTreeHelper.HasTranslatableDescendants(ObjectType.Culture), "Serialization code for Culture assumes it doesn't have translatable descendants");
				return;
			}
			if (this.ObjectTranslations.Any<ObjectTranslation>())
			{
				JsonObject jsonObject = this.ObjectTranslations.SerializeToJsonObject();
				jsonObj["translations", TomPropCategory.ChildCollection, 13, false] = jsonObject.ToDictObject();
			}
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0001A9B0 File Offset: 0x00018BB0
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name == "name")
			{
				this.body.Name = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
				return true;
			}
			if (name == "modifiedTime")
			{
				this.body.ModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
				return true;
			}
			if (name == "structureModifiedTime")
			{
				this.body.StructureModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
				return true;
			}
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

		// Token: 0x0600036A RID: 874 RVA: 0x0001AAB4 File Offset: 0x00018CB4
		private void ReadAdditionalPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel, ref bool wasRead)
		{
			string name = jsonProp.Name;
			if (name == "translations")
			{
				jsonProp.Value.VerifyTokenType(1);
				this.ObjectTranslations.DeserializeFromJsonObject((JObject)jsonProp.Value, mode, dbCompatibilityLevel);
				wasRead = true;
				return;
			}
			if (!(name == "linguisticMetadata"))
			{
				wasRead = false;
				return;
			}
			if (jsonProp.Value.Type != 10)
			{
				jsonProp.Value.VerifyTokenType(1);
				JObject jobject = (JObject)jsonProp.Value;
				LinguisticMetadata linguisticMetadata = new LinguisticMetadata();
				JToken jtoken;
				if (CompatibilityRestrictions.LinguisticMetadata_ContentType.IsCompatible(mode, dbCompatibilityLevel) && jobject.TryGetValue("contentType", ref jtoken))
				{
					ContentType contentType = JsonPropertyHelper.ConvertJsonValueToEnum<ContentType>(jtoken);
					if (PropertyHelper.IsContentTypeValueCompatible(contentType, mode, dbCompatibilityLevel))
					{
						linguisticMetadata.ContentType = contentType;
					}
				}
				linguisticMetadata.DeserializeFromJsonObject(jobject, options, mode, dbCompatibilityLevel);
				this.body.LinguisticMetadataID.Object = linguisticMetadata;
			}
			wasRead = true;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0001ABA0 File Offset: 0x00018DA0
		private static void WriteMetadataSchemaOfObjectTranslations(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			if (!writer.ShouldIncludeProperty("translations", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translation))
			{
				return;
			}
			writer.WriteProperty("translations", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation, null);
			using (writer.CreateComplexPropertyScope("translations", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation, null, null, new bool?(false)))
			{
				Culture.WriteMetadataSchemaOfTranslationsForModel(context, writer);
			}
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0001AC10 File Offset: 0x00018E10
		private static void WriteMetadataSchemaOfTranslationsForModel(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateComplexPropertyScope("model", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.Translation, null, null, new bool?(false)))
			{
				writer.WriteProperty("name", MetadataPropertyNature.NameProperty, typeof(string));
				writer.WriteProperty(JsonPropertyName.Misc.GetTranslatedPropertyName(context, TranslatedProperty.Caption), MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation, typeof(string));
				writer.WriteProperty(JsonPropertyName.Misc.GetTranslatedPropertyName(context, TranslatedProperty.Description), MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation | MetadataPropertyNature.MultilineString, typeof(string));
				using (writer.CreateCollectionScope(ObjectTreeHelper.GetChildCollectionJsonPropertyName(ObjectType.Model, ObjectType.Table), MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translation))
				{
					Culture.WriteMetadataSchemaOfTranslationsForTable(context, writer);
				}
				using (writer.CreateCollectionScope(ObjectTreeHelper.GetChildCollectionJsonPropertyName(ObjectType.Model, ObjectType.Perspective), MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translation))
				{
					Culture.WriteMetadataSchemaOfTranslationsForPerspective(context, writer);
				}
				using (writer.CreateCollectionScope(ObjectTreeHelper.GetChildCollectionJsonPropertyName(ObjectType.Model, ObjectType.Role), MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translation))
				{
					Culture.WriteMetadataSchemaOfTranslationsForRole(context, writer);
				}
				if (CompatibilityRestrictions.NamedExpression.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					using (writer.CreateCollectionScope(ObjectTreeHelper.GetChildCollectionJsonPropertyName(ObjectType.Model, ObjectType.Expression), MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translation))
					{
						Culture.WriteMetadataSchemaOfTranslationsForExpression(context, writer);
					}
				}
				if (CompatibilityRestrictions.QueryGroup.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					using (writer.CreateCollectionScope(ObjectTreeHelper.GetChildCollectionJsonPropertyName(ObjectType.Model, ObjectType.QueryGroup), MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translation))
					{
						Culture.WriteMetadataSchemaOfTranslationsForQueryGroup(context, writer);
					}
				}
				if (CompatibilityRestrictions.Function.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					using (writer.CreateCollectionScope(ObjectTreeHelper.GetChildCollectionJsonPropertyName(ObjectType.Model, ObjectType.Function), MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translation))
					{
						Culture.WriteMetadataSchemaOfTranslationsForFunction(context, writer);
					}
				}
				if (CompatibilityRestrictions.BindingInfo.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					using (writer.CreateCollectionScope(ObjectTreeHelper.GetChildCollectionJsonPropertyName(ObjectType.Model, ObjectType.BindingInfo), MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translation))
					{
						Culture.WriteMetadataSchemaOfTranslationsForBindingInfo(context, writer);
					}
				}
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0001AEC4 File Offset: 0x000190C4
		private static void WriteMetadataSchemaOfTranslationsForTable(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateComplexPropertyScope(null, MetadataPropertyNature.None, null, null, new bool?(false)))
			{
				writer.WriteProperty("name", MetadataPropertyNature.NameProperty, typeof(string));
				writer.WriteProperty(JsonPropertyName.Misc.GetTranslatedPropertyName(context, TranslatedProperty.Caption), MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation, typeof(string));
				writer.WriteProperty(JsonPropertyName.Misc.GetTranslatedPropertyName(context, TranslatedProperty.Description), MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation | MetadataPropertyNature.MultilineString, typeof(string));
				using (writer.CreateCollectionScope(ObjectTreeHelper.GetChildCollectionJsonPropertyName(ObjectType.Table, ObjectType.Column), MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translation))
				{
					Culture.WriteMetadataSchemaOfTranslationsForColumn(context, writer);
				}
				using (writer.CreateCollectionScope(ObjectTreeHelper.GetChildCollectionJsonPropertyName(ObjectType.Table, ObjectType.Measure), MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translation))
				{
					Culture.WriteMetadataSchemaOfTranslationsForMeasure(context, writer);
				}
				using (writer.CreateCollectionScope(ObjectTreeHelper.GetChildCollectionJsonPropertyName(ObjectType.Table, ObjectType.Hierarchy), MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translation))
				{
					Culture.WriteMetadataSchemaOfTranslationsForHierarchy(context, writer);
				}
				if (CompatibilityRestrictions.Set.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					using (writer.CreateCollectionScope(ObjectTreeHelper.GetChildCollectionJsonPropertyName(ObjectType.Table, ObjectType.Set), MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translation))
					{
						Culture.WriteMetadataSchemaOfTranslationsForSet(context, writer);
					}
				}
				if (CompatibilityRestrictions.Calendar.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					using (writer.CreateCollectionScope(ObjectTreeHelper.GetChildCollectionJsonPropertyName(ObjectType.Table, ObjectType.Calendar), MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translation))
					{
						Culture.WriteMetadataSchemaOfTranslationsForCalendar(context, writer);
					}
				}
			}
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0001B0C4 File Offset: 0x000192C4
		private static void WriteMetadataSchemaOfTranslationsForColumn(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateComplexPropertyScope(null, MetadataPropertyNature.None, null, null, new bool?(false)))
			{
				writer.WriteProperty("name", MetadataPropertyNature.NameProperty, typeof(string));
				writer.WriteProperty(JsonPropertyName.Misc.GetTranslatedPropertyName(context, TranslatedProperty.Caption), MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation, typeof(string));
				writer.WriteProperty(JsonPropertyName.Misc.GetTranslatedPropertyName(context, TranslatedProperty.Description), MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation | MetadataPropertyNature.MultilineString, typeof(string));
				writer.WriteProperty(JsonPropertyName.Misc.GetTranslatedPropertyName(context, TranslatedProperty.DisplayFolder), MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation, typeof(string));
				if (CompatibilityRestrictions.Variation.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					using (writer.CreateCollectionScope(ObjectTreeHelper.GetChildCollectionJsonPropertyName(ObjectType.Column, ObjectType.Variation), MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translation))
					{
						Culture.WriteMetadataSchemaOfTranslationsForVariation(context, writer);
					}
				}
			}
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0001B1B4 File Offset: 0x000193B4
		private static void WriteMetadataSchemaOfTranslationsForMeasure(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateComplexPropertyScope(null, MetadataPropertyNature.None, null, null, new bool?(false)))
			{
				writer.WriteProperty("name", MetadataPropertyNature.NameProperty, typeof(string));
				writer.WriteProperty(JsonPropertyName.Misc.GetTranslatedPropertyName(context, TranslatedProperty.Caption), MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation, typeof(string));
				writer.WriteProperty(JsonPropertyName.Misc.GetTranslatedPropertyName(context, TranslatedProperty.Description), MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation | MetadataPropertyNature.MultilineString, typeof(string));
				writer.WriteProperty(JsonPropertyName.Misc.GetTranslatedPropertyName(context, TranslatedProperty.DisplayFolder), MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation, typeof(string));
				writer.WriteProperty(ObjectTreeHelper.GetChildJsonPropertyName(ObjectType.Measure, ObjectType.KPI), MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.Translation, null);
				Culture.WriteMetadataSchemaOfTranslationsForKPI(context, writer);
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0001B274 File Offset: 0x00019474
		private static void WriteMetadataSchemaOfTranslationsForHierarchy(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateComplexPropertyScope(null, MetadataPropertyNature.None, null, null, new bool?(false)))
			{
				writer.WriteProperty("name", MetadataPropertyNature.NameProperty, typeof(string));
				writer.WriteProperty(JsonPropertyName.Misc.GetTranslatedPropertyName(context, TranslatedProperty.Caption), MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation, typeof(string));
				writer.WriteProperty(JsonPropertyName.Misc.GetTranslatedPropertyName(context, TranslatedProperty.Description), MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation | MetadataPropertyNature.MultilineString, typeof(string));
				writer.WriteProperty(JsonPropertyName.Misc.GetTranslatedPropertyName(context, TranslatedProperty.DisplayFolder), MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation, typeof(string));
				using (writer.CreateCollectionScope(ObjectTreeHelper.GetChildCollectionJsonPropertyName(ObjectType.Hierarchy, ObjectType.Level), MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translation))
				{
					Culture.WriteMetadataSchemaOfTranslationsForLevel(context, writer);
				}
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0001B34C File Offset: 0x0001954C
		private static void WriteMetadataSchemaOfTranslationsForLevel(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateComplexPropertyScope(null, MetadataPropertyNature.None, null, null, new bool?(false)))
			{
				writer.WriteProperty("name", MetadataPropertyNature.NameProperty, typeof(string));
				writer.WriteProperty(JsonPropertyName.Misc.GetTranslatedPropertyName(context, TranslatedProperty.Caption), MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation, typeof(string));
				writer.WriteProperty(JsonPropertyName.Misc.GetTranslatedPropertyName(context, TranslatedProperty.Description), MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation | MetadataPropertyNature.MultilineString, typeof(string));
			}
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0001B3D4 File Offset: 0x000195D4
		private static void WriteMetadataSchemaOfTranslationsForKPI(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateComplexPropertyScope(ObjectTreeHelper.GetChildJsonPropertyName(ObjectType.Measure, ObjectType.KPI), MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.Translation, null, null, new bool?(false)))
			{
				writer.WriteProperty(JsonPropertyName.Misc.GetTranslatedPropertyName(context, TranslatedProperty.Description), MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation | MetadataPropertyNature.MultilineString, typeof(string));
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0001B438 File Offset: 0x00019638
		private static void WriteMetadataSchemaOfTranslationsForPerspective(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateComplexPropertyScope(null, MetadataPropertyNature.None, null, null, new bool?(false)))
			{
				writer.WriteProperty("name", MetadataPropertyNature.NameProperty, typeof(string));
				writer.WriteProperty(JsonPropertyName.Misc.GetTranslatedPropertyName(context, TranslatedProperty.Caption), MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation, typeof(string));
				writer.WriteProperty(JsonPropertyName.Misc.GetTranslatedPropertyName(context, TranslatedProperty.Description), MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation | MetadataPropertyNature.MultilineString, typeof(string));
			}
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0001B4C0 File Offset: 0x000196C0
		private static void WriteMetadataSchemaOfTranslationsForRole(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateComplexPropertyScope(null, MetadataPropertyNature.None, null, null, new bool?(false)))
			{
				writer.WriteProperty("name", MetadataPropertyNature.NameProperty, typeof(string));
				writer.WriteProperty(JsonPropertyName.Misc.GetTranslatedPropertyName(context, TranslatedProperty.Description), MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation | MetadataPropertyNature.MultilineString, typeof(string));
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0001B52C File Offset: 0x0001972C
		private static void WriteMetadataSchemaOfTranslationsForVariation(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateComplexPropertyScope(null, MetadataPropertyNature.None, null, null, new bool?(false)))
			{
				writer.WriteProperty("name", MetadataPropertyNature.NameProperty, typeof(string));
				writer.WriteProperty(JsonPropertyName.Misc.GetTranslatedPropertyName(context, TranslatedProperty.Caption), MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation, typeof(string));
				writer.WriteProperty(JsonPropertyName.Misc.GetTranslatedPropertyName(context, TranslatedProperty.Description), MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation | MetadataPropertyNature.MultilineString, typeof(string));
			}
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0001B5B4 File Offset: 0x000197B4
		private static void WriteMetadataSchemaOfTranslationsForSet(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateComplexPropertyScope(null, MetadataPropertyNature.None, null, null, new bool?(false)))
			{
				writer.WriteProperty("name", MetadataPropertyNature.NameProperty, typeof(string));
				writer.WriteProperty(JsonPropertyName.Misc.GetTranslatedPropertyName(context, TranslatedProperty.Caption), MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation, typeof(string));
				writer.WriteProperty(JsonPropertyName.Misc.GetTranslatedPropertyName(context, TranslatedProperty.Description), MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation | MetadataPropertyNature.MultilineString, typeof(string));
				writer.WriteProperty(JsonPropertyName.Misc.GetTranslatedPropertyName(context, TranslatedProperty.DisplayFolder), MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation, typeof(string));
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0001B658 File Offset: 0x00019858
		private static void WriteMetadataSchemaOfTranslationsForExpression(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateComplexPropertyScope(null, MetadataPropertyNature.None, null, null, new bool?(false)))
			{
				writer.WriteProperty("name", MetadataPropertyNature.NameProperty, typeof(string));
				writer.WriteProperty(JsonPropertyName.Misc.GetTranslatedPropertyName(context, TranslatedProperty.Caption), MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation, typeof(string));
				writer.WriteProperty(JsonPropertyName.Misc.GetTranslatedPropertyName(context, TranslatedProperty.Description), MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation | MetadataPropertyNature.MultilineString, typeof(string));
			}
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0001B6E0 File Offset: 0x000198E0
		private static void WriteMetadataSchemaOfTranslationsForQueryGroup(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateComplexPropertyScope(null, MetadataPropertyNature.None, null, null, new bool?(false)))
			{
				writer.WriteProperty("name", MetadataPropertyNature.NameProperty, typeof(string));
				writer.WriteProperty(JsonPropertyName.Misc.GetTranslatedPropertyName(context, TranslatedProperty.Description), MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation | MetadataPropertyNature.MultilineString, typeof(string));
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0001B74C File Offset: 0x0001994C
		private static void WriteMetadataSchemaOfTranslationsForCalendar(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateComplexPropertyScope(null, MetadataPropertyNature.None, null, null, new bool?(false)))
			{
				writer.WriteProperty("name", MetadataPropertyNature.NameProperty, typeof(string));
				writer.WriteProperty(JsonPropertyName.Misc.GetTranslatedPropertyName(context, TranslatedProperty.Caption), MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation, typeof(string));
				writer.WriteProperty(JsonPropertyName.Misc.GetTranslatedPropertyName(context, TranslatedProperty.Description), MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation | MetadataPropertyNature.MultilineString, typeof(string));
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0001B7D4 File Offset: 0x000199D4
		private static void WriteMetadataSchemaOfTranslationsForFunction(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateComplexPropertyScope(null, MetadataPropertyNature.None, null, null, new bool?(false)))
			{
				writer.WriteProperty("name", MetadataPropertyNature.NameProperty, typeof(string));
				writer.WriteProperty(JsonPropertyName.Misc.GetTranslatedPropertyName(context, TranslatedProperty.Caption), MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation, typeof(string));
				writer.WriteProperty(JsonPropertyName.Misc.GetTranslatedPropertyName(context, TranslatedProperty.Description), MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation | MetadataPropertyNature.MultilineString, typeof(string));
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0001B85C File Offset: 0x00019A5C
		private static void WriteMetadataSchemaOfTranslationsForBindingInfo(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateComplexPropertyScope(null, MetadataPropertyNature.None, null, null, new bool?(false)))
			{
				writer.WriteProperty("name", MetadataPropertyNature.NameProperty, typeof(string));
				writer.WriteProperty(JsonPropertyName.Misc.GetTranslatedPropertyName(context, TranslatedProperty.Description), MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation | MetadataPropertyNature.MultilineString, typeof(string));
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0001B8C8 File Offset: 0x00019AC8
		private static void AddObjectTranslationToSpawningTree(ICollection<MetadataObject> objects, ObjectTranslation translation)
		{
			MetadataObject metadataObject = translation.body.ObjectID.Object;
			while (metadataObject != null && !objects.Contains(metadataObject))
			{
				objects.Add(metadataObject);
				metadataObject = metadataObject.Parent;
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0001B904 File Offset: 0x00019B04
		private void WriteObjectTranslationsToMetadataStream(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!writer.ShouldIncludeProperty("translations", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translation))
			{
				return;
			}
			HashSet<MetadataObject> hashSet = new HashSet<MetadataObject>();
			foreach (ObjectTranslation objectTranslation in this._ObjectTranslations)
			{
				Culture.AddObjectTranslationToSpawningTree(hashSet, objectTranslation);
			}
			using (writer.CreateComplexPropertyScope("translations", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translation))
			{
				this.WriteTranslationsToMetadataStreamImpl(context, writer, hashSet, base.Model, ObjectType.Null, true);
			}
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0001B9A4 File Offset: 0x00019BA4
		private void WriteTranslationsToMetadataStreamImpl(SerializationActivityContext context, IMetadataWriter writer, ICollection<MetadataObject> objects, MetadataObject @object, ObjectType parent, bool isSingleChild)
		{
			string text;
			if (isSingleChild)
			{
				if (@object.ObjectType == ObjectType.CalculationExpression)
				{
					CalculationGroupSelectionMode selectionMode = ((CalculationGroupExpression)@object).SelectionMode;
					if (selectionMode != CalculationGroupSelectionMode.MultipleOrEmptySelection)
					{
						if (selectionMode != CalculationGroupSelectionMode.NoSelection)
						{
							throw TomInternalException.Create("Invalid request for a child property name - ObjectType.{0} with CalculationGroupSelectionMode.{1} is not a valid type of child for ObjectType.{2}", new object[]
							{
								@object.ObjectType.ToString(),
								((CalculationGroupExpression)@object).SelectionMode,
								parent.ToString()
							});
						}
						text = "noSelectionExpression";
					}
					else
					{
						text = "multipleOrEmptySelectionExpression";
					}
				}
				else
				{
					text = ObjectTreeHelper.GetChildJsonPropertyName(parent, @object.ObjectType);
				}
			}
			else
			{
				text = null;
			}
			using (writer.CreateComplexPropertyScope(text, MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.Translation))
			{
				if (ObjectTreeHelper.IsNamedObject(@object.ObjectType))
				{
					writer.WriteStringProperty("name", MetadataPropertyNature.NameProperty, ((NamedMetadataObject)@object).Name);
				}
				else if (ObjectTreeHelper.IsKeyedObject(@object.ObjectType))
				{
					writer.WriteStringProperty(ObjectTreeHelper.GetKeyedObjectKeyPropertyName(@object.ObjectType).ToJsonCase(), MetadataPropertyNature.NameProperty, ((IKeyedMetadataObject)@object).LogicalPathElement);
				}
				foreach (TranslatablePropertyInfo translatablePropertyInfo in ObjectTreeHelper.GetTranslatedProperties(@object.ObjectType, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					ObjectTranslation objectTranslation = this._ObjectTranslations[@object, translatablePropertyInfo.Property];
					if (objectTranslation != null)
					{
						MetadataPropertyNature metadataPropertyNature = MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translation;
						if (translatablePropertyInfo.IsMultiline)
						{
							metadataPropertyNature |= MetadataPropertyNature.MultilineString;
						}
						writer.WriteStringProperty(JsonPropertyName.Misc.GetTranslatedPropertyName(context, translatablePropertyInfo.Property), metadataPropertyNature, objectTranslation.Value);
					}
				}
				IEnumerable<MetadataObject> directChildren = @object.GetDirectChildren(true);
				Func<MetadataObject, bool> <>9__0;
				Func<MetadataObject, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (MetadataObject o) => objects.Contains(o));
				}
				foreach (MetadataObject metadataObject in directChildren.Where(func))
				{
					if (ObjectTreeHelper.IsObjectComplientWithCompatibilityRestriction(@object.ObjectType, context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						this.WriteTranslationsToMetadataStreamImpl(context, writer, objects, metadataObject, @object.ObjectType, true);
					}
				}
				Func<MetadataObject, bool> <>9__1;
				foreach (IMetadataObjectCollection metadataObjectCollection in @object.GetChildrenCollections(true))
				{
					if (metadataObjectCollection.Count != 0 && ObjectTreeHelper.HasTranslatableDescendants(metadataObjectCollection.ItemType) && ObjectTreeHelper.IsObjectComplientWithCompatibilityRestriction(metadataObjectCollection.ItemType, context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						IDisposable disposable2 = null;
						try
						{
							IEnumerable<MetadataObject> objects2 = metadataObjectCollection.GetObjects();
							Func<MetadataObject, bool> func2;
							if ((func2 = <>9__1) == null)
							{
								func2 = (<>9__1 = (MetadataObject o) => objects.Contains(o));
							}
							foreach (MetadataObject metadataObject2 in objects2.Where(func2))
							{
								if (disposable2 == null)
								{
									disposable2 = writer.CreateComplexPropertyCollectionScope(ObjectTreeHelper.GetChildCollectionJsonPropertyName(@object.ObjectType, metadataObjectCollection.ItemType), MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translation);
								}
								this.WriteTranslationsToMetadataStreamImpl(context, writer, objects, metadataObject2, @object.ObjectType, false);
							}
						}
						finally
						{
							if (disposable2 != null)
							{
								disposable2.Dispose();
							}
						}
					}
				}
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0001BD8C File Offset: 0x00019F8C
		private bool TryReadObjectTranslationsFromMetadataStream(SerializationActivityContext context, IMetadataReader reader)
		{
			return !(reader.PropertyName != "translations") && this.TryReadTranslationsFromMetadataStreamImpl(context, reader.ReadComplexProperty(false), null, ObjectType.Null);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0001BDB8 File Offset: 0x00019FB8
		private bool TryReadTranslationsFromMetadataStreamImpl(SerializationActivityContext context, IMetadataReader reader, IList<KeyValuePair<ObjectType, string>> parentPath, ObjectType currentObject)
		{
			if (parentPath != null)
			{
				string text = null;
				string text2 = null;
				if (ObjectTreeHelper.IsNamedObject(currentObject))
				{
					if (reader.TryMoveToProperty("name"))
					{
						text2 = reader.ReadStringProperty();
					}
				}
				else if (ObjectTreeHelper.IsKeyedObject(currentObject))
				{
					text = ObjectTreeHelper.GetKeyedObjectKeyPropertyName(currentObject).ToJsonCase();
					if (reader.TryMoveToProperty(text))
					{
						text2 = reader.ReadStringProperty();
					}
				}
				reader.Reset();
				if (currentObject != ObjectType.Model)
				{
					parentPath.Add(new KeyValuePair<ObjectType, string>(currentObject, text2));
				}
				try
				{
					List<KeyValuePair<TranslatedProperty, string>> list = new List<KeyValuePair<TranslatedProperty, string>>();
					while (reader.IsOnProperty())
					{
						if (reader.PropertyName == "name" || (text != null && reader.PropertyName == text))
						{
							reader.Skip();
						}
						else
						{
							ObjectType objectType;
							bool flag;
							if (ObjectTreeHelper.IsChildJsonPropertyName(currentObject, reader.PropertyName, out objectType, out flag))
							{
								if (!ObjectTreeHelper.IsObjectComplientWithCompatibilityRestriction(objectType, context.CompatibilityMode, context.DbCompatibilityLevel))
								{
									return false;
								}
								if (flag)
								{
									if (!this.TryReadTranslationsFromMetadataStreamImpl(context, reader.ReadComplexProperty(true), parentPath, objectType))
									{
										return false;
									}
									continue;
								}
								else
								{
									using (IEnumerator<IMetadataReader> enumerator = reader.ReadComplexPropertyCollection(true).GetEnumerator())
									{
										while (enumerator.MoveNext())
										{
											IMetadataReader metadataReader = enumerator.Current;
											if (!this.TryReadTranslationsFromMetadataStreamImpl(context, metadataReader, parentPath, objectType))
											{
												return false;
											}
										}
										continue;
									}
								}
							}
							if (currentObject == ObjectType.CalculationGroup && (reader.PropertyName == "multipleOrEmptySelectionExpression" || reader.PropertyName == "noSelectionExpression"))
							{
								if (!ObjectTreeHelper.IsObjectComplientWithCompatibilityRestriction(ObjectType.CalculationExpression, context.CompatibilityMode, context.DbCompatibilityLevel))
								{
									return false;
								}
								if (!this.TryReadTranslationsFromMetadataStreamImpl(context, reader.ReadComplexProperty(true), parentPath, ObjectType.CalculationExpression))
								{
									return false;
								}
							}
							else
							{
								TranslatedProperty? translatedProperty = null;
								foreach (TranslatablePropertyInfo translatablePropertyInfo in ObjectTreeHelper.GetTranslatedProperties(currentObject, context.CompatibilityMode, context.DbCompatibilityLevel))
								{
									if (string.Compare(reader.PropertyName, JsonPropertyName.Misc.GetTranslatedPropertyName(context, translatablePropertyInfo.Property), StringComparison.InvariantCulture) == 0)
									{
										translatedProperty = new TranslatedProperty?(translatablePropertyInfo.Property);
										break;
									}
								}
								if (translatedProperty == null)
								{
									return false;
								}
								list.Add(new KeyValuePair<TranslatedProperty, string>(translatedProperty.Value, reader.ReadStringProperty()));
							}
						}
					}
					if (list.Count > 0)
					{
						ObjectPath objectPath = new ObjectPath(parentPath, false);
						for (int i = 0; i < list.Count; i++)
						{
							ObjectTranslation objectTranslation = new ObjectTranslation();
							objectTranslation.body.ObjectID.Path = objectPath;
							objectTranslation.Property = list[i].Key;
							objectTranslation.Value = list[i].Value;
							this._ObjectTranslations.Add(objectTranslation);
						}
					}
				}
				finally
				{
					if (currentObject != ObjectType.Model)
					{
						parentPath.RemoveAt(parentPath.Count - 1);
					}
				}
				return true;
			}
			ObjectType objectType2;
			bool flag2;
			if (!ObjectTreeHelper.IsChildJsonPropertyName(ObjectType.Null, reader.PropertyName, out objectType2, out flag2))
			{
				return false;
			}
			Utils.Verify(objectType2 == ObjectType.Model, "The root of the translations' tree must be the model!");
			Utils.Verify(flag2, "The root of the translations' tree must be the model!");
			parentPath = new List<KeyValuePair<ObjectType, string>>();
			if (!this.TryReadTranslationsFromMetadataStreamImpl(context, reader.ReadComplexProperty(true), parentPath, objectType2))
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0001C140 File Offset: 0x0001A340
		internal override string GetFormattedObjectPath()
		{
			return TomSR.ObjectPath_Culture_1Arg(this.Name);
		}

		// Token: 0x040000EA RID: 234
		internal Culture.ObjectBody body;

		// Token: 0x040000EB RID: 235
		private ObjectTranslationCollection _ObjectTranslations;

		// Token: 0x040000EC RID: 236
		private CultureAnnotationCollection _Annotations;

		// Token: 0x040000ED RID: 237
		private CultureExtendedPropertyCollection _ExtendedProperties;

		// Token: 0x02000250 RID: 592
		internal class ObjectBody : NamedMetadataObjectBody<Culture>
		{
			// Token: 0x06001FB1 RID: 8113 RVA: 0x000D1608 File Offset: 0x000CF808
			public ObjectBody(Culture owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.StructureModifiedTime = DateTime.MinValue;
				this.ModelID = new ParentLink<Culture, Model>(owner, "Model");
				this.LinguisticMetadataID = new ChildLink<Culture, LinguisticMetadata>(owner, "LinguisticMetadata");
			}

			// Token: 0x06001FB2 RID: 8114 RVA: 0x000D1654 File Offset: 0x000CF854
			public override string GetObjectName()
			{
				return this.Name;
			}

			// Token: 0x06001FB3 RID: 8115 RVA: 0x000D165C File Offset: 0x000CF85C
			internal bool IsEqualTo(Culture.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.StructureModifiedTime, other.StructureModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.ModelID.IsEqualTo(other.ModelID, context)) && this.LinguisticMetadataID.IsEqualTo(other.LinguisticMetadataID, context);
			}

			// Token: 0x06001FB4 RID: 8116 RVA: 0x000D1710 File Offset: 0x000CF910
			internal void CopyFromImpl(Culture.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				base.Owner.Name = other.Name;
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
					this.ModelID.CopyFrom(other.ModelID, context);
				}
				this.LinguisticMetadataID.CopyFrom(other.LinguisticMetadataID, context);
			}

			// Token: 0x06001FB5 RID: 8117 RVA: 0x000D17B8 File Offset: 0x000CF9B8
			internal void CopyFromImpl(Culture.ObjectBody other)
			{
				this.Name = other.Name;
				this.ModifiedTime = other.ModifiedTime;
				this.StructureModifiedTime = other.StructureModifiedTime;
				this.ModelID.CopyFrom(other.ModelID, ObjectChangeTracker.BodyCloneContext);
				this.LinguisticMetadataID.CopyFrom(other.LinguisticMetadataID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x06001FB6 RID: 8118 RVA: 0x000D1815 File Offset: 0x000CFA15
			public override void CopyFrom(MetadataObjectBody<Culture> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((Culture.ObjectBody)other, context);
			}

			// Token: 0x06001FB7 RID: 8119 RVA: 0x000D182C File Offset: 0x000CFA2C
			internal bool IsEqualTo(Culture.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && PropertyHelper.AreValuesIdentical(this.StructureModifiedTime, other.StructureModifiedTime) && this.ModelID.IsEqualTo(other.ModelID) && this.LinguisticMetadataID.IsEqualTo(other.LinguisticMetadataID);
			}

			// Token: 0x06001FB8 RID: 8120 RVA: 0x000D18A3 File Offset: 0x000CFAA3
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((Culture.ObjectBody)other);
			}

			// Token: 0x06001FB9 RID: 8121 RVA: 0x000D18BC File Offset: 0x000CFABC
			internal void CompareWith(Culture.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.Name, other.Name))
				{
					context.RegisterPropertyChange(base.Owner, "Name", typeof(string), base.RenameRequestedThroughAPI ? PropertyFlags.User : PropertyFlags.DdlAndUser, other.Name, this.Name);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				if (!PropertyHelper.AreValuesIdentical(this.StructureModifiedTime, other.StructureModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "StructureModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.StructureModifiedTime, this.StructureModifiedTime);
				}
				this.ModelID.CompareWith(other.ModelID, "ModelID", "Model", PropertyFlags.ReadOnly, context);
				this.LinguisticMetadataID.CompareWith(other.LinguisticMetadataID, "LinguisticMetadataID", "LinguisticMetadata", PropertyFlags.None, context);
			}

			// Token: 0x06001FBA RID: 8122 RVA: 0x000D19D3 File Offset: 0x000CFBD3
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((Culture.ObjectBody)other, context);
			}

			// Token: 0x040007DC RID: 2012
			internal string Name;

			// Token: 0x040007DD RID: 2013
			internal DateTime ModifiedTime;

			// Token: 0x040007DE RID: 2014
			internal DateTime StructureModifiedTime;

			// Token: 0x040007DF RID: 2015
			internal ParentLink<Culture, Model> ModelID;

			// Token: 0x040007E0 RID: 2016
			internal ChildLink<Culture, LinguisticMetadata> LinguisticMetadataID;
		}
	}
}
