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
	// Token: 0x02000051 RID: 81
	[CompatibilityRequirement("1603")]
	public sealed class DataCoverageDefinition : MetadataObject
	{
		// Token: 0x0600039B RID: 923 RVA: 0x0001C512 File Offset: 0x0001A712
		public DataCoverageDefinition()
		{
			this.InitBodyAndCollections(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0001C525 File Offset: 0x0001A725
		internal DataCoverageDefinition(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections(comparer);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0001C534 File Offset: 0x0001A734
		private void InitBodyAndCollections(IEqualityComparer<string> comparer)
		{
			this.body = new DataCoverageDefinition.ObjectBody(this);
			this.body.Description = string.Empty;
			this.body.Expression = string.Empty;
			this.body.State = ObjectState.Ready;
			this.body.ErrorMessage = string.Empty;
			this._Annotations = new DataCoverageDefinitionAnnotationCollection(this, comparer);
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0001C596 File Offset: 0x0001A796
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.DataCoverageDefinition;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600039F RID: 927 RVA: 0x0001C59A File Offset: 0x0001A79A
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x0001C5AC File Offset: 0x0001A7AC
		public override MetadataObject Parent
		{
			get
			{
				return this.body.PartitionID.Object;
			}
			internal set
			{
				if (this.body.PartitionID.Object != value)
				{
					MetadataObject.UpdateMetadataObjectParent<DataCoverageDefinition, Partition>(this.body.PartitionID, (Partition)value, "DataCoverageDefinition", CompatibilityRestrictions.Partition_DataCoverageDefinition);
				}
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0001C5E1 File Offset: 0x0001A7E1
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.PartitionID.ObjectID;
			}
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0001C5F4 File Offset: 0x0001A7F4
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.DataCoverageDefinition, null, "DataCoverageDefinition object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty))
				{
					writer.WriteProperty("expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty, typeof(string));
				}
				if (writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
				{
					writer.WriteProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, typeof(string));
				}
				if (writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				if (writer.ShouldIncludeProperty("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
				{
					writer.WriteEnumProperty<ObjectState>("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, PropertyHelper.GetObjectStateCompatibleValues(context.CompatibilityMode, context.DbCompatibilityLevel));
				}
				if (writer.ShouldIncludeProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
				{
					writer.WriteProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, typeof(string));
				}
				if (writer.ShouldIncludeProperty("annotations", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "annotations", MetadataPropertyNature.ChildCollection, ObjectType.Annotation);
				}
			}
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0001C740 File Offset: 0x0001A940
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			requiredLevel = CompatibilityRestrictions.DataCoverageDefinition[mode];
			requestingPath = ((!CompatibilityRestrictionSet.IsUnbound(requiredLevel)) ? string.Format("[{0}]", this.GetFormattedObjectPath()) : string.Empty);
			int num = requiredLevel;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x0001C777 File Offset: 0x0001A977
		// (set) Token: 0x060003A5 RID: 933 RVA: 0x0001C77F File Offset: 0x0001A97F
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (DataCoverageDefinition.ObjectBody)value;
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0001C78D File Offset: 0x0001A98D
		internal override ITxObjectBody CreateBody()
		{
			return new DataCoverageDefinition.ObjectBody(this);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0001C795 File Offset: 0x0001A995
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new DataCoverageDefinition();
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0001C79C File Offset: 0x0001A99C
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return null;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0001C7A0 File Offset: 0x0001A9A0
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			Partition partition = MetadataObject.ResolveMetadataObjectParentById<DataCoverageDefinition, Partition>(this.body.PartitionID, objectMap, throwIfCantResolve, "DataCoverageDefinition", CompatibilityRestrictions.Partition_DataCoverageDefinition);
			if (partition != null && partition.Model != null)
			{
				foreach (MetadataObject metadataObject in base.GetChildren(false))
				{
					partition.Model.NotifySubtreeAdded(metadataObject);
				}
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0001C81C File Offset: 0x0001AA1C
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0001C81E File Offset: 0x0001AA1E
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._Annotations;
			yield break;
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0001C82E File Offset: 0x0001AA2E
		public DataCoverageDefinitionAnnotationCollection Annotations
		{
			get
			{
				return this._Annotations;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0001C836 File Offset: 0x0001AA36
		// (set) Token: 0x060003AE RID: 942 RVA: 0x0001C844 File Offset: 0x0001AA44
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

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0001C8B4 File Offset: 0x0001AAB4
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x0001C8C4 File Offset: 0x0001AAC4
		public string Expression
		{
			get
			{
				return this.body.Expression;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Expression, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Expression", typeof(string), this.body.Expression, value);
					string expression = this.body.Expression;
					this.body.Expression = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Expression", typeof(string), expression, value);
				}
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0001C934 File Offset: 0x0001AB34
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x0001C944 File Offset: 0x0001AB44
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

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0001C9C8 File Offset: 0x0001ABC8
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x0001C9D8 File Offset: 0x0001ABD8
		public ObjectState State
		{
			get
			{
				return this.body.State;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.State, value))
				{
					CompatibilityRestrictionSet objectStateCompatibilityRestrictions = PropertyHelper.GetObjectStateCompatibilityRestrictions(value);
					CompatibilityRestrictionSet objectStateCompatibilityRestrictions2 = PropertyHelper.GetObjectStateCompatibilityRestrictions(this.body.State);
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					RestrictionsComapreResult restrictionsComapreResult = objectStateCompatibilityRestrictions.Compare(objectStateCompatibilityRestrictions2);
					if (restrictionsComapreResult == RestrictionsComapreResult.MoreRestrictive || restrictionsComapreResult == RestrictionsComapreResult.Incomparable || (restrictionsComapreResult == RestrictionsComapreResult.Equal && value != ObjectState.Ready))
					{
						array = base.ValidateCompatibilityRequirement(objectStateCompatibilityRestrictions, string.Format("[{0}]::[{1}]=[{2}]", this.GetFormattedObjectPath(), "State", value));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "State", typeof(ObjectState), this.body.State, value);
					switch (restrictionsComapreResult)
					{
					case RestrictionsComapreResult.Incomparable:
						base.ResetCompatibilityRequirement();
						base.SetCompatibilityRequirement(objectStateCompatibilityRestrictions, array);
						break;
					case RestrictionsComapreResult.LessRestrictive:
						base.ResetCompatibilityRequirement();
						break;
					case RestrictionsComapreResult.Equal:
						if (array != null)
						{
							base.SetCompatibilityRequirement(objectStateCompatibilityRestrictions, array);
						}
						break;
					case RestrictionsComapreResult.MoreRestrictive:
						base.SetCompatibilityRequirement(objectStateCompatibilityRestrictions, array);
						break;
					}
					ObjectState state = this.body.State;
					this.body.State = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "State", typeof(ObjectState), state, value);
				}
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0001CAFA File Offset: 0x0001ACFA
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x0001CB08 File Offset: 0x0001AD08
		public string ErrorMessage
		{
			get
			{
				return this.body.ErrorMessage;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.ErrorMessage, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "ErrorMessage", typeof(string), this.body.ErrorMessage, value);
					string errorMessage = this.body.ErrorMessage;
					this.body.ErrorMessage = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "ErrorMessage", typeof(string), errorMessage, value);
				}
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x0001CB78 File Offset: 0x0001AD78
		// (set) Token: 0x060003B8 RID: 952 RVA: 0x0001CB8C File Offset: 0x0001AD8C
		public Partition Partition
		{
			get
			{
				return this.body.PartitionID.Object;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.PartitionID.Object, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Partition", typeof(Partition), this.body.PartitionID.Object, value);
					Partition @object = this.body.PartitionID.Object;
					this.body.PartitionID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Partition", typeof(Partition), @object, value);
				}
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0001CC10 File Offset: 0x0001AE10
		// (set) Token: 0x060003BA RID: 954 RVA: 0x0001CC22 File Offset: 0x0001AE22
		internal ObjectId _PartitionID
		{
			get
			{
				return this.body.PartitionID.ObjectID;
			}
			set
			{
				this.body.PartitionID.ObjectID = value;
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0001CC38 File Offset: 0x0001AE38
		internal void CopyFrom(DataCoverageDefinition other, CopyContext context)
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
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0001CCE8 File Offset: 0x0001AEE8
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((DataCoverageDefinition)other, context);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0001CCF7 File Offset: 0x0001AEF7
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(DataCoverageDefinition other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0001CD13 File Offset: 0x0001AF13
		public void CopyTo(DataCoverageDefinition other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0001CD2F File Offset: 0x0001AF2F
		public DataCoverageDefinition Clone()
		{
			return base.CloneInternal<DataCoverageDefinition>();
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0001CD38 File Offset: 0x0001AF38
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.DataCoverageDefinition.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object DataCoverageDefinition is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (this.body.PartitionID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "PartitionID", this.body.PartitionID.Object);
			}
			if (!string.IsNullOrEmpty(this.body.Description))
			{
				writer.WriteProperty<string>(options, "Description", this.body.Description);
			}
			if (!string.IsNullOrEmpty(this.body.Expression))
			{
				writer.WriteProperty<string>(options, "Expression", this.body.Expression);
			}
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0001CE04 File Offset: 0x0001B004
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("PartitionID", out objectId))
			{
				this.body.PartitionID.ObjectID = objectId;
			}
			string text;
			if (reader.TryReadProperty<string>("Description", out text))
			{
				this.body.Description = text;
			}
			string text2;
			if (reader.TryReadProperty<string>("Expression", out text2))
			{
				this.body.Expression = text2;
			}
			DateTime dateTime;
			if (reader.TryReadProperty<DateTime>("ModifiedTime", out dateTime))
			{
				this.body.ModifiedTime = dateTime;
			}
			ObjectState objectState;
			if (reader.TryReadProperty<ObjectState>("State", out objectState))
			{
				this.body.State = objectState;
			}
			string text3;
			if (reader.TryReadProperty<string>("ErrorMessage", out text3))
			{
				this.body.ErrorMessage = text3;
			}
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0001CEC4 File Offset: 0x0001B0C4
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.DataCoverageDefinition.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object DataCoverageDefinition is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataBodyProperties(context, writer);
			if (this.body.PartitionID.Object != null && writer.ShouldIncludeProperty("PartitionID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
			{
				writer.WriteObjectIdProperty("PartitionID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly, this.body.PartitionID.Object);
			}
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.body.Description);
			}
			if (!string.IsNullOrEmpty(this.body.Expression) && writer.ShouldIncludeProperty("Expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty))
			{
				writer.WriteStringProperty("Expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty, this.body.Expression);
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0001CFE0 File Offset: 0x0001B1E0
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.DataCoverageDefinition.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object DataCoverageDefinition is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataTree(context, writer);
			if (!string.IsNullOrEmpty(this.body.Expression) && writer.ShouldIncludeProperty("expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty))
			{
				writer.WriteStringProperty("expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty, this.body.Expression);
			}
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.body.Description);
			}
			if (this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.ModifiedTime);
			}
			if (this.body.State != ObjectState.Ready)
			{
				if (!PropertyHelper.IsObjectStateValueCompatible(this.body.State, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member State is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
				{
					writer.WriteEnumProperty<ObjectState>("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, this.body.State);
				}
			}
			if (!string.IsNullOrEmpty(this.body.ErrorMessage) && writer.ShouldIncludeProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
			{
				writer.WriteStringProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, this.body.ErrorMessage);
			}
			if (this.Annotations.Count > 0 && writer.ShouldIncludeProperty("annotations", MetadataPropertyNature.ChildCollection))
			{
				writer.WriteChildCollection(context, "annotations", MetadataPropertyNature.ChildCollection, this.Annotations);
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0001D1F0 File Offset: 0x0001B3F0
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
				if (length != 5)
				{
					switch (length)
					{
					case 10:
					{
						char c = propertyName[0];
						if (c != 'E')
						{
							if (c != 'e')
							{
								break;
							}
							if (!(propertyName == "expression"))
							{
								break;
							}
						}
						else if (!(propertyName == "Expression"))
						{
							break;
						}
						this.body.Expression = reader.ReadStringProperty();
						return true;
					}
					case 11:
					{
						char c = propertyName[0];
						if (c <= 'P')
						{
							if (c != 'D')
							{
								if (c != 'P')
								{
									break;
								}
								if (!(propertyName == "PartitionID"))
								{
									break;
								}
								this.body.PartitionID.ObjectID = reader.ReadObjectIdProperty();
								return true;
							}
							else if (!(propertyName == "Description"))
							{
								break;
							}
						}
						else if (c != 'a')
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
									catch (Exception ex)
									{
										throw reader.CreateInvalidChildException(context, annotation, TomSR.Exception_FailedAddDeserializedNamedObject("Annotation", (annotation != null) ? annotation.Name : null, ex.Message), ex);
									}
								}
							}
							return true;
						}
						this.body.Description = reader.ReadStringProperty();
						return true;
					}
					case 12:
					{
						char c = propertyName[0];
						if (c <= 'M')
						{
							if (c != 'E')
							{
								if (c != 'M')
								{
									break;
								}
								if (!(propertyName == "ModifiedTime"))
								{
									break;
								}
							}
							else
							{
								if (!(propertyName == "ErrorMessage"))
								{
									break;
								}
								goto IL_025F;
							}
						}
						else if (c != 'e')
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
						else
						{
							if (!(propertyName == "errorMessage"))
							{
								break;
							}
							goto IL_025F;
						}
						this.body.ModifiedTime = reader.ReadDateTimeProperty();
						return true;
						IL_025F:
						this.body.ErrorMessage = reader.ReadStringProperty();
						return true;
					}
					}
				}
				else
				{
					char c = propertyName[0];
					if (c != 'S')
					{
						if (c != 's')
						{
							goto IL_02F9;
						}
						if (!(propertyName == "state"))
						{
							goto IL_02F9;
						}
					}
					else if (!(propertyName == "State"))
					{
						goto IL_02F9;
					}
					ObjectState objectState = reader.ReadEnumProperty<ObjectState>();
					if (!PropertyHelper.IsObjectStateValueCompatible(objectState, context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatiblePropertyValue;
						return false;
					}
					this.body.State = objectState;
					return true;
				}
			}
			IL_02F9:
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0001D524 File Offset: 0x0001B724
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.DataCoverageDefinition.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object DataCoverageDefinition is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.Description))
			{
				result["description", TomPropCategory.Regular, 2, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Description, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.Expression))
			{
				result["expression", TomPropCategory.Regular, 3, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Expression, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["modifiedTime", TomPropCategory.Regular, 4, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.ModifiedTime);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreInferredProperties && this.body.State != ObjectState.Ready)
			{
				if (!PropertyHelper.IsObjectStateValueCompatible(this.body.State, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member State is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["state", TomPropCategory.Regular, 5, true] = JsonPropertyHelper.ConvertEnumToJsonValue<ObjectState>(this.body.State);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreInferredProperties && !string.IsNullOrEmpty(this.body.ErrorMessage))
			{
				result["errorMessage", TomPropCategory.Regular, 6, true] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.ErrorMessage, SplitMultilineOptions.None);
			}
			if (!options.IgnoreChildren && !options.IncludeTranslatablePropertiesOnly && this.Annotations.Any<Annotation>())
			{
				object[] array = this.Annotations.Select((Annotation obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array2;
			}
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0001D7C4 File Offset: 0x0001B9C4
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name == "description")
			{
				this.body.Description = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
				return true;
			}
			if (name == "expression")
			{
				this.body.Expression = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
				return true;
			}
			if (name == "modifiedTime")
			{
				this.body.ModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
				return true;
			}
			if (!(name == "state"))
			{
				if (name == "errorMessage")
				{
					this.body.ErrorMessage = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
					return true;
				}
				if (!(name == "annotations"))
				{
					return false;
				}
				JsonPropertyHelper.ReadObjectCollection(this.Annotations, jsonProp.Value, options, mode, dbCompatibilityLevel);
				return true;
			}
			else
			{
				ObjectState objectState = JsonPropertyHelper.ConvertJsonValueToEnum<ObjectState>(jsonProp.Value);
				if (jsonProp.Value.Type != 10 && !PropertyHelper.IsObjectStateValueCompatible(objectState, mode, dbCompatibilityLevel))
				{
					return false;
				}
				this.body.State = objectState;
				return true;
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0001D8E0 File Offset: 0x0001BAE0
		internal override string GetFormattedObjectPath()
		{
			Partition partition = (Partition)this.Parent;
			if (partition == null)
			{
				return TomSR.ObjectPath_DataCoverageDefinition_0Args;
			}
			return TomSR.ObjectPath_DataCoverageDefinition_1Args(partition.Name);
		}

		// Token: 0x040000EE RID: 238
		internal DataCoverageDefinition.ObjectBody body;

		// Token: 0x040000EF RID: 239
		private DataCoverageDefinitionAnnotationCollection _Annotations;

		// Token: 0x02000257 RID: 599
		internal class ObjectBody : MetadataObjectBody<DataCoverageDefinition>
		{
			// Token: 0x06001FDC RID: 8156 RVA: 0x000D1D27 File Offset: 0x000CFF27
			public ObjectBody(DataCoverageDefinition owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.PartitionID = new ParentLink<DataCoverageDefinition, Partition>(owner, "Partition");
			}

			// Token: 0x06001FDD RID: 8157 RVA: 0x000D1D4C File Offset: 0x000CFF4C
			internal bool IsEqualTo(DataCoverageDefinition.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.Expression, other.Expression) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.State, other.State)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ErrorMessage, other.ErrorMessage)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.PartitionID.IsEqualTo(other.PartitionID, context));
			}

			// Token: 0x06001FDE RID: 8158 RVA: 0x000D1E28 File Offset: 0x000D0028
			internal void CopyFromImpl(DataCoverageDefinition.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				this.Description = other.Description;
				this.Expression = other.Expression;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModifiedTime = other.ModifiedTime;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.State = other.State;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ErrorMessage = other.ErrorMessage;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.PartitionID.CopyFrom(other.PartitionID, context);
				}
			}

			// Token: 0x06001FDF RID: 8159 RVA: 0x000D1EE4 File Offset: 0x000D00E4
			internal void CopyFromImpl(DataCoverageDefinition.ObjectBody other)
			{
				this.Description = other.Description;
				this.Expression = other.Expression;
				this.ModifiedTime = other.ModifiedTime;
				this.State = other.State;
				this.ErrorMessage = other.ErrorMessage;
				this.PartitionID.CopyFrom(other.PartitionID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x06001FE0 RID: 8160 RVA: 0x000D1F43 File Offset: 0x000D0143
			public override void CopyFrom(MetadataObjectBody<DataCoverageDefinition> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((DataCoverageDefinition.ObjectBody)other, context);
			}

			// Token: 0x06001FE1 RID: 8161 RVA: 0x000D1F5C File Offset: 0x000D015C
			internal bool IsEqualTo(DataCoverageDefinition.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.Expression, other.Expression) && PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && PropertyHelper.AreValuesIdentical(this.State, other.State) && PropertyHelper.AreValuesIdentical(this.ErrorMessage, other.ErrorMessage) && this.PartitionID.IsEqualTo(other.PartitionID);
			}

			// Token: 0x06001FE2 RID: 8162 RVA: 0x000D1FE8 File Offset: 0x000D01E8
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((DataCoverageDefinition.ObjectBody)other);
			}

			// Token: 0x06001FE3 RID: 8163 RVA: 0x000D2004 File Offset: 0x000D0204
			internal void CompareWith(DataCoverageDefinition.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.Description, other.Description))
				{
					context.RegisterPropertyChange(base.Owner, "Description", typeof(string), PropertyFlags.DdlAndUser, other.Description, this.Description);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Expression, other.Expression))
				{
					context.RegisterPropertyChange(base.Owner, "Expression", typeof(string), PropertyFlags.DdlAndUser, other.Expression, this.Expression);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				if (!PropertyHelper.AreValuesIdentical(this.State, other.State))
				{
					context.RegisterPropertyChange(base.Owner, "State", typeof(ObjectState), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.State, this.State);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ErrorMessage, other.ErrorMessage))
				{
					context.RegisterPropertyChange(base.Owner, "ErrorMessage", typeof(string), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ErrorMessage, this.ErrorMessage);
				}
				this.PartitionID.CompareWith(other.PartitionID, "PartitionID", "Partition", PropertyFlags.ReadOnly, context);
			}

			// Token: 0x06001FE4 RID: 8164 RVA: 0x000D2169 File Offset: 0x000D0369
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((DataCoverageDefinition.ObjectBody)other, context);
			}

			// Token: 0x040007F4 RID: 2036
			internal string Description;

			// Token: 0x040007F5 RID: 2037
			internal string Expression;

			// Token: 0x040007F6 RID: 2038
			internal DateTime ModifiedTime;

			// Token: 0x040007F7 RID: 2039
			internal ObjectState State;

			// Token: 0x040007F8 RID: 2040
			internal string ErrorMessage;

			// Token: 0x040007F9 RID: 2041
			internal ParentLink<DataCoverageDefinition, Partition> PartitionID;
		}
	}
}
