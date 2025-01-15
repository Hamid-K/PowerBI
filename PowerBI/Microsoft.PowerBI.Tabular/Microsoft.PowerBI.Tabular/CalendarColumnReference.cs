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
	// Token: 0x0200003F RID: 63
	[EditorBrowsable(EditorBrowsableState.Never)]
	[CompatibilityRequirement("Preview")]
	internal sealed class CalendarColumnReference : NamedMetadataObject, ILinkedMetadataObject
	{
		// Token: 0x060001F9 RID: 505 RVA: 0x0000E8CD File Offset: 0x0000CACD
		public CalendarColumnReference()
		{
			this.InitBodyAndCollections();
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000E8DB File Offset: 0x0000CADB
		internal CalendarColumnReference(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections();
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000E8E9 File Offset: 0x0000CAE9
		private void InitBodyAndCollections()
		{
			this.body = new CalendarColumnReference.ObjectBody(this);
			this.body.IsPrimaryColumn = false;
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000E903 File Offset: 0x0000CB03
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.CalendarColumnReference;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000E907 File Offset: 0x0000CB07
		// (set) Token: 0x060001FE RID: 510 RVA: 0x0000E919 File Offset: 0x0000CB19
		public override MetadataObject Parent
		{
			get
			{
				return this.body.TimeUnitColumnAssociationID.Object;
			}
			internal set
			{
				if (this.body.TimeUnitColumnAssociationID.Object != value)
				{
					MetadataObject.UpdateMetadataObjectParent<CalendarColumnReference, TimeUnitColumnAssociation>(this.body.TimeUnitColumnAssociationID, (TimeUnitColumnAssociation)value, null, null);
				}
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000E946 File Offset: 0x0000CB46
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.TimeUnitColumnAssociationID.ObjectID;
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000E958 File Offset: 0x0000CB58
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			requiredLevel = CompatibilityRestrictions.CalendarColumnReference[mode];
			requestingPath = ((!CompatibilityRestrictionSet.IsUnbound(requiredLevel)) ? string.Format("[{0}]", this.GetFormattedObjectPath()) : string.Empty);
			int num = requiredLevel;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000201 RID: 513 RVA: 0x0000E98F File Offset: 0x0000CB8F
		// (set) Token: 0x06000202 RID: 514 RVA: 0x0000E997 File Offset: 0x0000CB97
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (CalendarColumnReference.ObjectBody)value;
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000E9A5 File Offset: 0x0000CBA5
		internal override ITxObjectBody CreateBody()
		{
			return new CalendarColumnReference.ObjectBody(this);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000E9AD File Offset: 0x0000CBAD
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new CalendarColumnReference();
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000E9B4 File Offset: 0x0000CBB4
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return ((TimeUnitColumnAssociation)parent).CalendarColumnReferences;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000E9C4 File Offset: 0x0000CBC4
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			TimeUnitColumnAssociation timeUnitColumnAssociation = MetadataObject.ResolveMetadataObjectParentById<CalendarColumnReference, TimeUnitColumnAssociation>(this.body.TimeUnitColumnAssociationID, objectMap, throwIfCantResolve, null, null);
			this.body.ColumnID.ResolveById(objectMap, throwIfCantResolve);
			if (timeUnitColumnAssociation != null)
			{
				timeUnitColumnAssociation.CalendarColumnReferences.Add(this);
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000EA08 File Offset: 0x0000CC08
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			this.body.ColumnID.ResolveById(objectMap, throwIfCantResolve);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000EA20 File Offset: 0x0000CC20
		internal override void TryResolveCrossLinksAfterCopy(CopyContext copyContext)
		{
			if (!this.body.ColumnID.TryResolveAfterCopy(copyContext) && this.body.ColumnID.Path != null && !this.body.ColumnID.Path.IsEmpty)
			{
				this.body._name = this.body.ColumnID.Path[this.body.ColumnID.Path.Count - 1].Value;
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000EAA8 File Offset: 0x0000CCA8
		internal override void ValidateObjectImpl(ValidationResult result, bool throwOnError)
		{
			this.body.ColumnID.Validate(result, throwOnError);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000EABC File Offset: 0x0000CCBC
		internal override bool ContainsUnresolvedCrossLinksImpl()
		{
			return !this.body.ColumnID.IsResolved || base.ContainsUnresolvedCrossLinksImpl();
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000EAD8 File Offset: 0x0000CCD8
		// (set) Token: 0x0600020C RID: 524 RVA: 0x0000EAF9 File Offset: 0x0000CCF9
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

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000EB1A File Offset: 0x0000CD1A
		// (set) Token: 0x0600020E RID: 526 RVA: 0x0000EB28 File Offset: 0x0000CD28
		public bool IsPrimaryColumn
		{
			get
			{
				return this.body.IsPrimaryColumn;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.IsPrimaryColumn, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "IsPrimaryColumn", typeof(bool), this.body.IsPrimaryColumn, value);
					bool isPrimaryColumn = this.body.IsPrimaryColumn;
					this.body.IsPrimaryColumn = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "IsPrimaryColumn", typeof(bool), isPrimaryColumn, value);
				}
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000EBAC File Offset: 0x0000CDAC
		// (set) Token: 0x06000210 RID: 528 RVA: 0x0000EBBC File Offset: 0x0000CDBC
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

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000EC40 File Offset: 0x0000CE40
		// (set) Token: 0x06000212 RID: 530 RVA: 0x0000EC54 File Offset: 0x0000CE54
		public TimeUnitColumnAssociation TimeUnitColumnAssociation
		{
			get
			{
				return this.body.TimeUnitColumnAssociationID.Object;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.TimeUnitColumnAssociationID.Object, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "TimeUnitColumnAssociation", typeof(TimeUnitColumnAssociation), this.body.TimeUnitColumnAssociationID.Object, value);
					TimeUnitColumnAssociation @object = this.body.TimeUnitColumnAssociationID.Object;
					this.body.TimeUnitColumnAssociationID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "TimeUnitColumnAssociation", typeof(TimeUnitColumnAssociation), @object, value);
				}
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000ECD8 File Offset: 0x0000CED8
		// (set) Token: 0x06000214 RID: 532 RVA: 0x0000ECEA File Offset: 0x0000CEEA
		internal ObjectId _TimeUnitColumnAssociationID
		{
			get
			{
				return this.body.TimeUnitColumnAssociationID.ObjectID;
			}
			set
			{
				this.body.TimeUnitColumnAssociationID.ObjectID = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000ECFD File Offset: 0x0000CEFD
		// (set) Token: 0x06000216 RID: 534 RVA: 0x0000ED10 File Offset: 0x0000CF10
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
						throw new InvalidOperationException(TomSR.Exception_CantChangeImmutableProperty("Column", "CalendarColumnReference"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "Column", typeof(Column), this.body.ColumnID.Object, value);
					Column @object = this.body.ColumnID.Object;
					this.body.ColumnID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Column", typeof(Column), @object, value);
				}
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000EDBE File Offset: 0x0000CFBE
		// (set) Token: 0x06000218 RID: 536 RVA: 0x0000EDD0 File Offset: 0x0000CFD0
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

		// Token: 0x06000219 RID: 537 RVA: 0x0000EDE4 File Offset: 0x0000CFE4
		internal void CopyFrom(CalendarColumnReference other, CopyContext context)
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
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000EE75 File Offset: 0x0000D075
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((CalendarColumnReference)other, context);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000EE84 File Offset: 0x0000D084
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(CalendarColumnReference other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000EEA0 File Offset: 0x0000D0A0
		public void CopyTo(CalendarColumnReference other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000EEBC File Offset: 0x0000D0BC
		public CalendarColumnReference Clone()
		{
			return base.CloneInternal<CalendarColumnReference>();
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000EEC4 File Offset: 0x0000D0C4
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.CalendarColumnReference.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object CalendarColumnReference is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (this.body.TimeUnitColumnAssociationID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "TimeUnitColumnAssociationID", this.body.TimeUnitColumnAssociationID.Object);
			}
			this.body.ColumnID.Validate(null, true);
			if (this.body.ColumnID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "ColumnID", this.body.ColumnID.Object);
			}
			if (this.body.IsPrimaryColumn)
			{
				writer.WriteProperty<bool>(options, "IsPrimaryColumn", this.body.IsPrimaryColumn);
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000EFA4 File Offset: 0x0000D1A4
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("TimeUnitColumnAssociationID", out objectId))
			{
				this.body.TimeUnitColumnAssociationID.ObjectID = objectId;
			}
			ObjectId objectId2;
			if (reader.TryReadProperty<ObjectId>("ColumnID", out objectId2))
			{
				this.body.ColumnID.ObjectID = objectId2;
			}
			bool flag;
			if (reader.TryReadProperty<bool>("IsPrimaryColumn", out flag))
			{
				this.body.IsPrimaryColumn = flag;
			}
			DateTime dateTime;
			if (reader.TryReadProperty<DateTime>("ModifiedTime", out dateTime))
			{
				this.body.ModifiedTime = dateTime;
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000F030 File Offset: 0x0000D230
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.CalendarColumnReference.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object CalendarColumnReference is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataBodyProperties(context, writer);
			if (this.body.TimeUnitColumnAssociationID.Object != null && writer.ShouldIncludeProperty("TimeUnitColumnAssociationID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
			{
				writer.WriteObjectIdProperty("TimeUnitColumnAssociationID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly, this.body.TimeUnitColumnAssociationID.Object);
			}
			this.body.ColumnID.Validate(null, true);
			if (this.body.ColumnID.Object != null && writer.ShouldIncludeProperty("ColumnID", MetadataPropertyNature.CrossLinkProperty))
			{
				writer.WriteObjectIdProperty("ColumnID", MetadataPropertyNature.CrossLinkProperty, this.body.ColumnID.Object);
			}
			if (this.body.IsPrimaryColumn && writer.ShouldIncludeProperty("IsPrimaryColumn", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("IsPrimaryColumn", MetadataPropertyNature.RegularProperty, this.body.IsPrimaryColumn);
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000F14E File Offset: 0x0000D34E
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			throw new TomInternalException("Objects of type CalendarColumnReference are not expected to be serialized to the metadata-stream in standard way!");
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000F15C File Offset: 0x0000D35C
		private protected override bool TryReadNextMetadataProperty(SerializationActivityContext context, IMetadataReader reader, out UnexpectedPropertyClassification classification)
		{
			if (context.SerializationMode != MetadataSerializationMode.Xmla)
			{
				throw TomInternalException.Create("Objects of type CalendarColumnReference are not expected to be serialized in {0} mode!", new object[] { context.SerializationMode });
			}
			if (base.TryReadNextMetadataProperty(context, reader, out classification))
			{
				return true;
			}
			string propertyName = reader.PropertyName;
			if (propertyName == "TimeUnitColumnAssociationID")
			{
				this.body.TimeUnitColumnAssociationID.ObjectID = reader.ReadObjectIdProperty();
				return true;
			}
			if (propertyName == "ColumnID")
			{
				this.body.ColumnID.ObjectID = reader.ReadObjectIdProperty();
				return true;
			}
			if (propertyName == "IsPrimaryColumn")
			{
				this.body.IsPrimaryColumn = reader.ReadBooleanProperty();
				return true;
			}
			if (!(propertyName == "ModifiedTime"))
			{
				classification = UnexpectedPropertyClassification.UnknownProperty;
				return false;
			}
			this.body.ModifiedTime = reader.ReadDateTimeProperty();
			return true;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000F235 File Offset: 0x0000D435
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			throw new TomInternalException("Objects of type CalendarColumnReference are not expected to be serialized to JSON!");
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000F241 File Offset: 0x0000D441
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			throw new TomInternalException("Objects of type CalendarColumnReference are not expected to be serialized to JSON!");
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000F250 File Offset: 0x0000D450
		internal override bool BuildIndirectNameCrossLinkPathIfNeeded()
		{
			if (this.body.ColumnID.Object != null)
			{
				return true;
			}
			if (string.IsNullOrEmpty(this.body._name) || this.TimeUnitColumnAssociation == null || this.TimeUnitColumnAssociation.Calendar == null || this.TimeUnitColumnAssociation.Calendar == null || this.TimeUnitColumnAssociation.Calendar.Table == null || string.IsNullOrEmpty(this.TimeUnitColumnAssociation.Calendar.Table.Name))
			{
				return false;
			}
			if (this.body.ColumnID.Path == null || this.body.ColumnID.Path.IsEmpty)
			{
				this.body.ColumnID.Path = new ObjectPath(new KeyValuePair<ObjectType, string>[]
				{
					new KeyValuePair<ObjectType, string>(ObjectType.Table, this.TimeUnitColumnAssociation.Calendar.Table.Name),
					new KeyValuePair<ObjectType, string>(ObjectType.Column, this.Name)
				});
			}
			return true;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000F34F File Offset: 0x0000D54F
		void ILinkedMetadataObject.GetLinkedObjectTarget(out ObjectId objectId, out ObjectPath objectPath, out MetadataObject @object, out string property)
		{
			objectId = this.body.ColumnID.ObjectID;
			objectPath = this.body.ColumnID.Path;
			@object = this.body.ColumnID.Object;
			property = null;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000F390 File Offset: 0x0000D590
		internal override bool TryResolveCrossLinksByPath(ICollection<string> linksFailedToResolve)
		{
			if (!this.body.ColumnID.IsResolved)
			{
				if (!this.body.ColumnID.TryResolveByPath())
				{
					if (linksFailedToResolve != null)
					{
						linksFailedToResolve.Add(string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "Column"));
					}
					return false;
				}
			}
			else if (this.body.ColumnID.Object == null && !string.IsNullOrEmpty(this.body._name))
			{
				if (this.Parent == null || this.Parent.Parent == null || this.Parent.Parent.Parent == null)
				{
					if (linksFailedToResolve != null)
					{
						linksFailedToResolve.Add(string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "Column"));
					}
					return false;
				}
				string name = ((Table)this.Parent.Parent.Parent).Name;
				this.body.ColumnID.Path = new ObjectPath(new KeyValuePair<ObjectType, string>[]
				{
					new KeyValuePair<ObjectType, string>(ObjectType.Table, name),
					new KeyValuePair<ObjectType, string>(ObjectType.Column, this.body._name)
				});
				if (!this.body.ColumnID.TryResolveByPath())
				{
					if (linksFailedToResolve != null)
					{
						linksFailedToResolve.Add(string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "Column"));
					}
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000F4E4 File Offset: 0x0000D6E4
		internal override string GetFormattedObjectPath()
		{
			if (this.TimeUnitColumnAssociation == null)
			{
				return TomSR.ObjectPath_CalendarColumnReference_1Arg(this.Name);
			}
			if (this.TimeUnitColumnAssociation.Calendar == null)
			{
				return TomSR.ObjectPath_CalendarColumnReference_2Args(this.Name, this.TimeUnitColumnAssociation.TimeUnit.ToString());
			}
			if (this.TimeUnitColumnAssociation.Calendar.Table == null)
			{
				return TomSR.ObjectPath_CalendarColumnReference_3Args(this.Name, this.TimeUnitColumnAssociation.TimeUnit.ToString(), this.TimeUnitColumnAssociation.Calendar.Name);
			}
			return TomSR.ObjectPath_CalendarColumnReference_4Args(this.Name, this.TimeUnitColumnAssociation.TimeUnit.ToString(), this.TimeUnitColumnAssociation.Calendar.Name, this.TimeUnitColumnAssociation.Calendar.Table.Name);
		}

		// Token: 0x040000DE RID: 222
		internal CalendarColumnReference.ObjectBody body;

		// Token: 0x02000240 RID: 576
		internal class ObjectBody : NamedMetadataObjectBody<CalendarColumnReference>
		{
			// Token: 0x06001F4E RID: 8014 RVA: 0x000CECF0 File Offset: 0x000CCEF0
			public ObjectBody(CalendarColumnReference owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.TimeUnitColumnAssociationID = new ParentLink<CalendarColumnReference, TimeUnitColumnAssociation>(owner, "TimeUnitColumnAssociation");
				this.ColumnID = new CrossLink<CalendarColumnReference, Column>(owner, "Column");
			}

			// Token: 0x06001F4F RID: 8015 RVA: 0x000CED26 File Offset: 0x000CCF26
			public override string GetObjectName()
			{
				if (this.ColumnID.Object == null)
				{
					return this._name;
				}
				return this.ColumnID.Object.Name;
			}

			// Token: 0x06001F50 RID: 8016 RVA: 0x000CED4C File Offset: 0x000CCF4C
			internal bool IsEqualTo(CalendarColumnReference.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.IsPrimaryColumn, other.IsPrimaryColumn) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.TimeUnitColumnAssociationID.IsEqualTo(other.TimeUnitColumnAssociationID, context)) && this.ColumnID.IsEqualTo(other.ColumnID, context);
			}

			// Token: 0x06001F51 RID: 8017 RVA: 0x000CEDD8 File Offset: 0x000CCFD8
			internal void CopyFromImpl(CalendarColumnReference.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				this.IsPrimaryColumn = other.IsPrimaryColumn;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModifiedTime = other.ModifiedTime;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.TimeUnitColumnAssociationID.CopyFrom(other.TimeUnitColumnAssociationID, context);
				}
				this.ColumnID.CopyFrom(other.ColumnID, context);
				this._name = other._name;
			}

			// Token: 0x06001F52 RID: 8018 RVA: 0x000CEE68 File Offset: 0x000CD068
			internal void CopyFromImpl(CalendarColumnReference.ObjectBody other)
			{
				this.IsPrimaryColumn = other.IsPrimaryColumn;
				this.ModifiedTime = other.ModifiedTime;
				this.TimeUnitColumnAssociationID.CopyFrom(other.TimeUnitColumnAssociationID, ObjectChangeTracker.BodyCloneContext);
				this.ColumnID.CopyFrom(other.ColumnID, ObjectChangeTracker.BodyCloneContext);
				this._name = other._name;
			}

			// Token: 0x06001F53 RID: 8019 RVA: 0x000CEEC5 File Offset: 0x000CD0C5
			public override void CopyFrom(MetadataObjectBody<CalendarColumnReference> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((CalendarColumnReference.ObjectBody)other, context);
			}

			// Token: 0x06001F54 RID: 8020 RVA: 0x000CEEDC File Offset: 0x000CD0DC
			internal bool IsEqualTo(CalendarColumnReference.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.IsPrimaryColumn, other.IsPrimaryColumn) && PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && this.TimeUnitColumnAssociationID.IsEqualTo(other.TimeUnitColumnAssociationID) && this.ColumnID.IsEqualTo(other.ColumnID);
			}

			// Token: 0x06001F55 RID: 8021 RVA: 0x000CEF3E File Offset: 0x000CD13E
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((CalendarColumnReference.ObjectBody)other);
			}

			// Token: 0x06001F56 RID: 8022 RVA: 0x000CEF58 File Offset: 0x000CD158
			internal void CompareWith(CalendarColumnReference.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.IsPrimaryColumn, other.IsPrimaryColumn))
				{
					context.RegisterPropertyChange(base.Owner, "IsPrimaryColumn", typeof(bool), PropertyFlags.DdlAndUser, other.IsPrimaryColumn, this.IsPrimaryColumn);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				this.TimeUnitColumnAssociationID.CompareWith(other.TimeUnitColumnAssociationID, "TimeUnitColumnAssociationID", "TimeUnitColumnAssociation", PropertyFlags.ReadOnly, context);
				this.ColumnID.CompareWith(other.ColumnID, "ColumnID", "Column", PropertyFlags.None, context);
			}

			// Token: 0x06001F57 RID: 8023 RVA: 0x000CF029 File Offset: 0x000CD229
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((CalendarColumnReference.ObjectBody)other, context);
			}

			// Token: 0x0400077E RID: 1918
			internal bool IsPrimaryColumn;

			// Token: 0x0400077F RID: 1919
			internal DateTime ModifiedTime;

			// Token: 0x04000780 RID: 1920
			internal ParentLink<CalendarColumnReference, TimeUnitColumnAssociation> TimeUnitColumnAssociationID;

			// Token: 0x04000781 RID: 1921
			internal CrossLink<CalendarColumnReference, Column> ColumnID;

			// Token: 0x04000782 RID: 1922
			internal string _name;
		}
	}
}
