using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200008B RID: 139
	public sealed class ObjectTranslation : MetadataObject, ILinkedMetadataObject
	{
		// Token: 0x0600083F RID: 2111 RVA: 0x00047077 File Offset: 0x00045277
		public ObjectTranslation()
		{
			this.InitBodyAndCollections();
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00047085 File Offset: 0x00045285
		internal ObjectTranslation(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections();
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00047093 File Offset: 0x00045293
		private void InitBodyAndCollections()
		{
			this.body = new ObjectTranslation.ObjectBody(this);
			this.body.Property = (TranslatedProperty)(-1);
			this.body.Value = string.Empty;
			this.body.Altered = false;
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x000470C9 File Offset: 0x000452C9
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.ObjectTranslation;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x000470CD File Offset: 0x000452CD
		// (set) Token: 0x06000844 RID: 2116 RVA: 0x000470DF File Offset: 0x000452DF
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
					MetadataObject.UpdateMetadataObjectParent<ObjectTranslation, Culture>(this.body.CultureID, (Culture)value, null, null);
				}
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x0004710C File Offset: 0x0004530C
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.CultureID.ObjectID;
			}
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00047120 File Offset: 0x00045320
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			base.GetCompatibilityRequirementByMembers(mode, out requiredLevel, out requestingPath);
			if (requiredLevel == -2)
			{
				return;
			}
			if (this.body.Altered)
			{
				int num = CompatibilityRestrictions.ObjectTranslation_Altered[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "Altered");
					requiredLevel = num;
					int num2 = requiredLevel;
					return;
				}
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x00047182 File Offset: 0x00045382
		// (set) Token: 0x06000848 RID: 2120 RVA: 0x0004718A File Offset: 0x0004538A
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (ObjectTranslation.ObjectBody)value;
			}
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00047198 File Offset: 0x00045398
		internal override ITxObjectBody CreateBody()
		{
			return new ObjectTranslation.ObjectBody(this);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x000471A0 File Offset: 0x000453A0
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new ObjectTranslation();
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x000471A7 File Offset: 0x000453A7
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return ((Culture)parent).ObjectTranslations;
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x000471B4 File Offset: 0x000453B4
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			if (this.Culture != null)
			{
				this.Culture.ObjectTranslations.RemoveFromIndex(this);
			}
			Culture culture = MetadataObject.ResolveMetadataObjectParentById<ObjectTranslation, Culture>(this.body.CultureID, objectMap, throwIfCantResolve, null, null);
			this.body.ObjectID.ResolveById(objectMap, throwIfCantResolve);
			if (culture != null)
			{
				culture.ObjectTranslations.Add(this);
			}
			if (this.Culture != null)
			{
				this.Culture.ObjectTranslations.AddToIndex(this);
			}
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0004722C File Offset: 0x0004542C
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			if (this.Culture != null)
			{
				this.Culture.ObjectTranslations.RemoveFromIndex(this);
			}
			this.body.ObjectID.ResolveById(objectMap, throwIfCantResolve);
			if (this.Culture != null)
			{
				this.Culture.ObjectTranslations.AddToIndex(this);
			}
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00047280 File Offset: 0x00045480
		internal override bool TryResolveCrossLinksByPath(ICollection<string> linksFailedToResolve)
		{
			if (this.Culture != null)
			{
				this.Culture.ObjectTranslations.RemoveFromIndex(this);
			}
			bool flag = true;
			if (!this.body.ObjectID.IsResolved && !this.body.ObjectID.TryResolveByPath())
			{
				if (linksFailedToResolve != null)
				{
					linksFailedToResolve.Add(string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "Object"));
				}
				flag = false;
			}
			if (this.Culture != null)
			{
				this.Culture.ObjectTranslations.AddToIndex(this);
			}
			return flag;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00047308 File Offset: 0x00045508
		internal override void TryResolveCrossLinksAfterCopy(CopyContext copyContext)
		{
			if (this.Culture != null)
			{
				this.Culture.ObjectTranslations.RemoveFromIndex(this);
			}
			this.body.ObjectID.TryResolveAfterCopy(copyContext);
			if (this.Culture != null)
			{
				this.Culture.ObjectTranslations.AddToIndex(this);
			}
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00047359 File Offset: 0x00045559
		internal override void ValidateObjectImpl(ValidationResult result, bool throwOnError)
		{
			this.body.ObjectID.Validate(result, throwOnError);
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0004736D File Offset: 0x0004556D
		internal override bool ContainsUnresolvedCrossLinksImpl()
		{
			return !this.body.ObjectID.IsResolved || base.ContainsUnresolvedCrossLinksImpl();
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x00047389 File Offset: 0x00045589
		// (set) Token: 0x06000853 RID: 2131 RVA: 0x00047398 File Offset: 0x00045598
		public TranslatedProperty Property
		{
			get
			{
				return this.body.Property;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Property, value))
				{
					if (this.Parent != null)
					{
						throw new InvalidOperationException(TomSR.Exception_CantChangeImmutableProperty("Property", "ObjectTranslation"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "Property", typeof(TranslatedProperty), this.body.Property, value);
					TranslatedProperty property = this.body.Property;
					this.body.Property = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Property", typeof(TranslatedProperty), property, value);
				}
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x0004743C File Offset: 0x0004563C
		// (set) Token: 0x06000855 RID: 2133 RVA: 0x0004744C File Offset: 0x0004564C
		public string Value
		{
			get
			{
				return this.body.Value;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Value, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Value", typeof(string), this.body.Value, value);
					string value2 = this.body.Value;
					this.body.Value = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Value", typeof(string), value2, value);
				}
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x000474BC File Offset: 0x000456BC
		// (set) Token: 0x06000857 RID: 2135 RVA: 0x000474CC File Offset: 0x000456CC
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

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x00047550 File Offset: 0x00045750
		// (set) Token: 0x06000859 RID: 2137 RVA: 0x00047560 File Offset: 0x00045760
		[CompatibilityRequirement("1571")]
		public bool Altered
		{
			get
			{
				return this.body.Altered;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Altered, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (value)
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.ObjectTranslation_Altered, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "Altered"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "Altered", typeof(bool), this.body.Altered, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.ObjectTranslation_Altered, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					bool altered = this.body.Altered;
					this.body.Altered = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Altered", typeof(bool), altered, value);
				}
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x00047624 File Offset: 0x00045824
		// (set) Token: 0x0600085B RID: 2139 RVA: 0x00047638 File Offset: 0x00045838
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

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x000476BC File Offset: 0x000458BC
		// (set) Token: 0x0600085D RID: 2141 RVA: 0x000476CE File Offset: 0x000458CE
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

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x000476E1 File Offset: 0x000458E1
		// (set) Token: 0x0600085F RID: 2143 RVA: 0x000476F4 File Offset: 0x000458F4
		public MetadataObject Object
		{
			get
			{
				return this.body.ObjectID.Object;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.ObjectID.Object, value))
				{
					if (this.body.ObjectID.Object != null)
					{
						throw new InvalidOperationException(TomSR.Exception_CantChangeImmutableProperty("Object", "ObjectTranslation"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "Object", typeof(MetadataObject), this.body.ObjectID.Object, value);
					MetadataObject @object = this.body.ObjectID.Object;
					this.body.ObjectID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Object", typeof(MetadataObject), @object, value);
				}
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x000477A2 File Offset: 0x000459A2
		// (set) Token: 0x06000861 RID: 2145 RVA: 0x000477B4 File Offset: 0x000459B4
		internal ObjectId _ObjectID
		{
			get
			{
				return this.body.ObjectID.ObjectID;
			}
			set
			{
				this.body.ObjectID.ObjectID = value;
			}
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x000477C8 File Offset: 0x000459C8
		internal void CopyFrom(ObjectTranslation other, CopyContext context)
		{
			if (this.Culture != null)
			{
				this.Culture.ObjectTranslations.RemoveFromIndex(this);
			}
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
			if (this.Culture != null)
			{
				this.Culture.ObjectTranslations.AddToIndex(this);
			}
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0004788B File Offset: 0x00045A8B
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((ObjectTranslation)other, context);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0004789A File Offset: 0x00045A9A
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(ObjectTranslation other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x000478B6 File Offset: 0x00045AB6
		public void CopyTo(ObjectTranslation other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x000478D2 File Offset: 0x00045AD2
		public ObjectTranslation Clone()
		{
			return base.CloneInternal<ObjectTranslation>();
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x000478DC File Offset: 0x00045ADC
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (this.body.CultureID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "CultureID", this.body.CultureID.Object);
			}
			this.body.ObjectID.Validate(null, true);
			if (this.body.ObjectID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "ObjectID", this.body.ObjectID.Object);
				writer.WriteProperty<int>(options, "ObjectType", (int)this.body.ObjectID.Object.ObjectType);
			}
			if (this.body.Property != (TranslatedProperty)(-1))
			{
				writer.WriteProperty<TranslatedProperty>(options, "Property", this.body.Property);
			}
			if (!string.IsNullOrEmpty(this.body.Value))
			{
				writer.WriteProperty<string>(options, "Value", this.body.Value);
			}
			if (this.body.Altered)
			{
				if (!CompatibilityRestrictions.ObjectTranslation_Altered.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Altered is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<bool>(options, "Altered", this.body.Altered);
			}
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00047A28 File Offset: 0x00045C28
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("CultureID", out objectId))
			{
				this.body.CultureID.ObjectID = objectId;
			}
			ObjectId objectId2;
			if (reader.TryReadProperty<ObjectId>("ObjectID", out objectId2))
			{
				this.body.ObjectID.ObjectID = objectId2;
			}
			TranslatedProperty translatedProperty;
			if (reader.TryReadProperty<TranslatedProperty>("Property", out translatedProperty))
			{
				this.body.Property = translatedProperty;
			}
			string text;
			if (reader.TryReadProperty<string>("Value", out text))
			{
				this.body.Value = text;
			}
			DateTime dateTime;
			if (reader.TryReadProperty<DateTime>("ModifiedTime", out dateTime))
			{
				this.body.ModifiedTime = dateTime;
			}
			bool flag;
			if (CompatibilityRestrictions.ObjectTranslation_Altered.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<bool>("Altered", out flag))
			{
				this.body.Altered = flag;
			}
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00047AFC File Offset: 0x00045CFC
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataBodyProperties(context, writer);
			if (this.body.CultureID.Object != null && writer.ShouldIncludeProperty("CultureID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
			{
				writer.WriteObjectIdProperty("CultureID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly, this.body.CultureID.Object);
			}
			this.body.ObjectID.Validate(null, true);
			if (this.body.ObjectID.Object != null && writer.ShouldIncludeProperty("ObjectID", MetadataPropertyNature.CrossLinkProperty))
			{
				writer.WriteObjectIdProperty("ObjectID", MetadataPropertyNature.CrossLinkProperty, this.body.ObjectID.Object);
				writer.WriteObjectTypeProperty("ObjectType", MetadataPropertyNature.LinkTypeProperty, this.body.ObjectID.Object.ObjectType);
			}
			if (this.body.Property != (TranslatedProperty)(-1) && writer.ShouldIncludeProperty("Property", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<TranslatedProperty>("Property", MetadataPropertyNature.RegularProperty, this.body.Property);
			}
			if (!string.IsNullOrEmpty(this.body.Value) && writer.ShouldIncludeProperty("Value", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("Value", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.body.Value);
			}
			if (this.body.Altered)
			{
				if (!CompatibilityRestrictions.ObjectTranslation_Altered.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Altered is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("Altered", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteBooleanProperty("Altered", MetadataPropertyNature.RegularProperty, this.body.Altered);
				}
			}
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00047CAD File Offset: 0x00045EAD
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			throw new TomInternalException("Objects of type ObjectTranslation are not expected to be serialized to the metadata-stream in standard way!");
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00047CBC File Offset: 0x00045EBC
		private protected override bool TryReadNextMetadataProperty(SerializationActivityContext context, IMetadataReader reader, out UnexpectedPropertyClassification classification)
		{
			if (context.SerializationMode != MetadataSerializationMode.Xmla)
			{
				throw TomInternalException.Create("Objects of type ObjectTranslation are not expected to be serialized in {0} mode!", new object[] { context.SerializationMode });
			}
			if (base.TryReadNextMetadataProperty(context, reader, out classification))
			{
				return true;
			}
			string propertyName = reader.PropertyName;
			if (propertyName == "CultureID")
			{
				this.body.CultureID.ObjectID = reader.ReadObjectIdProperty();
				return true;
			}
			if (propertyName == "ObjectID")
			{
				this.body.ObjectID.ObjectID = reader.ReadObjectIdProperty();
				return true;
			}
			if (propertyName == "Property")
			{
				this.body.Property = reader.ReadEnumProperty<TranslatedProperty>();
				return true;
			}
			if (propertyName == "Value")
			{
				this.body.Value = reader.ReadStringProperty();
				return true;
			}
			if (propertyName == "ModifiedTime")
			{
				this.body.ModifiedTime = reader.ReadDateTimeProperty();
				return true;
			}
			if (!(propertyName == "Altered"))
			{
				classification = UnexpectedPropertyClassification.UnknownProperty;
				return false;
			}
			if (!CompatibilityRestrictions.ObjectTranslation_Altered.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				classification = UnexpectedPropertyClassification.IncompatibleProperty;
				return false;
			}
			this.body.Altered = reader.ReadBooleanProperty();
			return true;
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00047DF5 File Offset: 0x00045FF5
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			throw new TomInternalException("Objects of type ObjectTranslation are not expected to be serialized to JSON!");
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00047E01 File Offset: 0x00046001
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			throw new TomInternalException("Objects of type ObjectTranslation are not expected to be serialized to JSON!");
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00047E10 File Offset: 0x00046010
		void ILinkedMetadataObject.GetLinkedObjectTarget(out ObjectId objectId, out ObjectPath objectPath, out MetadataObject @object, out string property)
		{
			objectId = this.body.ObjectID.ObjectID;
			objectPath = this.body.ObjectID.Path;
			@object = this.body.ObjectID.Object;
			property = this.body.Property.ToString();
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00047E70 File Offset: 0x00046070
		internal override string GetFormattedObjectPath()
		{
			if (this.Culture != null)
			{
				return TomSR.ObjectPath_ObjectTranslation_1Arg(this.Culture.Name);
			}
			return TomSR.ObjectPath_ObjectTranslation_0Args;
		}

		// Token: 0x04000146 RID: 326
		internal ObjectTranslation.ObjectBody body;

		// Token: 0x02000299 RID: 665
		internal class ObjectBody : MetadataObjectBody<ObjectTranslation>
		{
			// Token: 0x060021AC RID: 8620 RVA: 0x000DA608 File Offset: 0x000D8808
			public ObjectBody(ObjectTranslation owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.CultureID = new ParentLink<ObjectTranslation, Culture>(owner, "Culture");
				this.ObjectID = new UntypedCrossLink<ObjectTranslation>(owner, "Object");
			}

			// Token: 0x060021AD RID: 8621 RVA: 0x000DA640 File Offset: 0x000D8840
			internal bool IsEqualTo(ObjectTranslation.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.Property, other.Property) && PropertyHelper.AreValuesIdentical(this.Value, other.Value) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && PropertyHelper.AreValuesIdentical(this.Altered, other.Altered) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.CultureID.IsEqualTo(other.CultureID, context)) && this.ObjectID.IsEqualTo(other.ObjectID, context);
			}

			// Token: 0x060021AE RID: 8622 RVA: 0x000DA6F4 File Offset: 0x000D88F4
			internal void CopyFromImpl(ObjectTranslation.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				this.Property = other.Property;
				this.Value = other.Value;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModifiedTime = other.ModifiedTime;
				}
				this.Altered = other.Altered;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.CultureID.CopyFrom(other.CultureID, context);
				}
				this.ObjectID.CopyFrom(other.ObjectID, context);
			}

			// Token: 0x060021AF RID: 8623 RVA: 0x000DA790 File Offset: 0x000D8990
			internal void CopyFromImpl(ObjectTranslation.ObjectBody other)
			{
				this.Property = other.Property;
				this.Value = other.Value;
				this.ModifiedTime = other.ModifiedTime;
				this.Altered = other.Altered;
				this.CultureID.CopyFrom(other.CultureID, ObjectChangeTracker.BodyCloneContext);
				this.ObjectID.CopyFrom(other.ObjectID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x060021B0 RID: 8624 RVA: 0x000DA7F9 File Offset: 0x000D89F9
			public override void CopyFrom(MetadataObjectBody<ObjectTranslation> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((ObjectTranslation.ObjectBody)other, context);
			}

			// Token: 0x060021B1 RID: 8625 RVA: 0x000DA810 File Offset: 0x000D8A10
			internal bool IsEqualTo(ObjectTranslation.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.Property, other.Property) && PropertyHelper.AreValuesIdentical(this.Value, other.Value) && PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && PropertyHelper.AreValuesIdentical(this.Altered, other.Altered) && this.CultureID.IsEqualTo(other.CultureID) && this.ObjectID.IsEqualTo(other.ObjectID);
			}

			// Token: 0x060021B2 RID: 8626 RVA: 0x000DA89C File Offset: 0x000D8A9C
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((ObjectTranslation.ObjectBody)other);
			}

			// Token: 0x060021B3 RID: 8627 RVA: 0x000DA8B8 File Offset: 0x000D8AB8
			internal void CompareWith(ObjectTranslation.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.Property, other.Property))
				{
					context.RegisterPropertyChange(base.Owner, "Property", typeof(TranslatedProperty), PropertyFlags.DdlAndUser, other.Property, this.Property);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Value, other.Value))
				{
					context.RegisterPropertyChange(base.Owner, "Value", typeof(string), PropertyFlags.DdlAndUser, other.Value, this.Value);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Altered, other.Altered))
				{
					context.RegisterPropertyChange(base.Owner, "Altered", typeof(bool), PropertyFlags.DdlAndUser, other.Altered, this.Altered);
				}
				this.CultureID.CompareWith(other.CultureID, "CultureID", "Culture", PropertyFlags.ReadOnly, context);
				this.ObjectID.CompareWith(other.ObjectID, "ObjectID", "Object", PropertyFlags.None, context);
			}

			// Token: 0x060021B4 RID: 8628 RVA: 0x000DAA09 File Offset: 0x000D8C09
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((ObjectTranslation.ObjectBody)other, context);
			}

			// Token: 0x0400095A RID: 2394
			internal TranslatedProperty Property;

			// Token: 0x0400095B RID: 2395
			internal string Value;

			// Token: 0x0400095C RID: 2396
			internal DateTime ModifiedTime;

			// Token: 0x0400095D RID: 2397
			internal bool Altered;

			// Token: 0x0400095E RID: 2398
			internal ParentLink<ObjectTranslation, Culture> CultureID;

			// Token: 0x0400095F RID: 2399
			internal UntypedCrossLink<ObjectTranslation> ObjectID;
		}
	}
}
