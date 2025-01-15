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
	// Token: 0x020000BD RID: 189
	[CompatibilityRequirement("1400")]
	public class StructuredDataSource : DataSource
	{
		// Token: 0x06000B9E RID: 2974 RVA: 0x0005F104 File Offset: 0x0005D304
		public StructuredDataSource()
		{
			this.OnAfterConstructor();
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0005F112 File Offset: 0x0005D312
		internal StructuredDataSource(IEqualityComparer<string> comparer)
			: base(comparer)
		{
			this.OnAfterConstructor();
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0005F121 File Offset: 0x0005D321
		private void OnAfterConstructor()
		{
			this.body.Type = DataSourceType.Structured;
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0005F130 File Offset: 0x0005D330
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			base.GetCompatibilityRequirementByMembers(mode, out requiredLevel, out requestingPath);
			if (CompatibilityRestrictionSet.CompareLevel(CompatibilityRestrictions.StructuredDataSource[mode], requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
			{
				requiredLevel = CompatibilityRestrictions.StructuredDataSource[mode];
				requestingPath = string.Format("[{0}]", this.GetFormattedObjectPath());
			}
			if (requiredLevel == -2)
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.body.ConnectionDetails) || this.connectionDetails.IsDirty)
			{
				int num = CompatibilityRestrictions.StructuredDataSource_ConnectionDetails[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ConnectionDetails");
					requiredLevel = num;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (!string.IsNullOrEmpty(this.body.Options) || this.options.IsDirty)
			{
				int num2 = CompatibilityRestrictions.StructuredDataSource_Options[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num2, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "Options");
					requiredLevel = num2;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (!string.IsNullOrEmpty(this.body.Credential) || this.credential.IsDirty)
			{
				int num3 = CompatibilityRestrictions.StructuredDataSource_Credential[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num3, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "Credential");
					requiredLevel = num3;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (!string.IsNullOrEmpty(this.ContextExpression))
			{
				int num4 = CompatibilityRestrictions.StructuredDataSource_ContextExpression[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num4, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ContextExpression");
					requiredLevel = num4;
					int num5 = requiredLevel;
					return;
				}
			}
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0005F2CB File Offset: 0x0005D4CB
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new StructuredDataSource();
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x0005F2D2 File Offset: 0x0005D4D2
		// (set) Token: 0x06000BA4 RID: 2980 RVA: 0x0005F2EC File Offset: 0x0005D4EC
		public ConnectionDetails ConnectionDetails
		{
			get
			{
				return this.connectionDetails.GetProperty(this, this.body.ConnectionDetails);
			}
			set
			{
				if (!this.connectionDetails.IsSamePropertyReference(value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (value != null)
					{
						if (((ICustomProperty<StructuredDataSource, string>)value).Owner != null)
						{
							throw new ArgumentException(TomSR.Exception_CustomPropertyAssignedToMultipleObjects("ConnectionDetails"), "value");
						}
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.StructuredDataSource_ConnectionDetails, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ConnectionDetails"));
					}
					string text = this.body.ConnectionDetails;
					this.connectionDetails.ExtractMetadataValueIfNeeded(ref text, false);
					string text2 = ((value != null) ? ((ICustomProperty<StructuredDataSource, string>)value).Convert() : string.Empty);
					ObjectChangeTracker.RegisterPropertyChanging(this, "ConnectionDetails", typeof(string), text, text2);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.StructuredDataSource_ConnectionDetails, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					this.body.ConnectionDetails = text2;
					this.connectionDetails.SetProperty(this, value, false);
					ObjectChangeTracker.RegisterPropertyChanged(this, "ConnectionDetails", typeof(string), text, text2);
				}
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x0005F3D7 File Offset: 0x0005D5D7
		// (set) Token: 0x06000BA6 RID: 2982 RVA: 0x0005F3F0 File Offset: 0x0005D5F0
		public DataSourceOptions Options
		{
			get
			{
				return this.options.GetProperty(this, this.body.Options);
			}
			set
			{
				if (!this.options.IsSamePropertyReference(value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (value != null)
					{
						if (((ICustomProperty<StructuredDataSource, string>)value).Owner != null)
						{
							throw new ArgumentException(TomSR.Exception_CustomPropertyAssignedToMultipleObjects("Options"), "value");
						}
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.StructuredDataSource_Options, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "Options"));
					}
					string text = this.body.Options;
					this.options.ExtractMetadataValueIfNeeded(ref text, false);
					string text2 = ((value != null) ? ((ICustomProperty<StructuredDataSource, string>)value).Convert() : string.Empty);
					ObjectChangeTracker.RegisterPropertyChanging(this, "Options", typeof(string), text, text2);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.StructuredDataSource_Options, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					this.body.Options = text2;
					this.options.SetProperty(this, value, false);
					ObjectChangeTracker.RegisterPropertyChanged(this, "Options", typeof(string), text, text2);
				}
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x0005F4DB File Offset: 0x0005D6DB
		// (set) Token: 0x06000BA8 RID: 2984 RVA: 0x0005F4F4 File Offset: 0x0005D6F4
		public Credential Credential
		{
			get
			{
				return this.credential.GetProperty(this, this.body.Credential);
			}
			set
			{
				if (!this.credential.IsSamePropertyReference(value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (value != null)
					{
						if (((ICustomProperty<StructuredDataSource, string>)value).Owner != null)
						{
							throw new ArgumentException(TomSR.Exception_CustomPropertyAssignedToMultipleObjects("Credential"), "value");
						}
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.StructuredDataSource_Credential, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "Credential"));
					}
					string text = this.body.Credential;
					this.credential.ExtractMetadataValueIfNeeded(ref text, false);
					string text2 = ((value != null) ? ((ICustomProperty<StructuredDataSource, string>)value).Convert() : string.Empty);
					ObjectChangeTracker.RegisterPropertyChanging(this, "Credential", typeof(string), text, text2);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.StructuredDataSource_Credential, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					this.body.Credential = text2;
					this.credential.SetProperty(this, value, false);
					ObjectChangeTracker.RegisterPropertyChanged(this, "Credential", typeof(string), text, text2);
				}
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x0005F5DF File Offset: 0x0005D7DF
		// (set) Token: 0x06000BAA RID: 2986 RVA: 0x0005F5E7 File Offset: 0x0005D7E7
		public new string ContextExpression
		{
			get
			{
				return base.ContextExpression;
			}
			set
			{
				base.ContextExpression = value;
			}
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0005F5F0 File Offset: 0x0005D7F0
		internal override void BeforeBodyCompareWith()
		{
			base.BeforeBodyCompareWith();
			this.connectionDetails.ExtractMetadataValueIfNeeded(ref this.body.ConnectionDetails, true);
			this.options.ExtractMetadataValueIfNeeded(ref this.body.Options, true);
			this.credential.ExtractMetadataValueIfNeeded(ref this.body.Credential, true);
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0005F648 File Offset: 0x0005D848
		internal override void OnAfterBodyReverted()
		{
			base.OnAfterBodyReverted();
			this.connectionDetails.UpdateProperty(this.body.ConnectionDetails);
			this.options.UpdateProperty(this.body.Options);
			this.credential.UpdateProperty(this.body.Credential);
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0005F6A0 File Offset: 0x0005D8A0
		internal override void CopyFrom(DataSource other, CopyContext context)
		{
			((StructuredDataSource)other).connectionDetails.ExtractMetadataValueIfNeeded(ref other.body.ConnectionDetails, true);
			((StructuredDataSource)other).options.ExtractMetadataValueIfNeeded(ref other.body.Options, true);
			((StructuredDataSource)other).credential.ExtractMetadataValueIfNeeded(ref other.body.Credential, true);
			base.CopyFrom(other, context);
			this.connectionDetails.UpdateProperty(this.body.ConnectionDetails);
			this.options.UpdateProperty(this.body.Options);
			this.credential.UpdateProperty(this.body.Credential);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0005F74C File Offset: 0x0005D94C
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (!string.IsNullOrEmpty(this.body.ConnectionDetails) || this.connectionDetails.IsDirty)
			{
				this.connectionDetails.ExtractMetadataValueIfNeeded(ref this.body.ConnectionDetails, true);
				if (!string.IsNullOrEmpty(this.body.ConnectionDetails))
				{
					writer.WriteProperty<string>(options, "ConnectionDetails", this.body.ConnectionDetails);
				}
			}
			if (!string.IsNullOrEmpty(this.body.Options) || this.options.IsDirty)
			{
				this.options.ExtractMetadataValueIfNeeded(ref this.body.Options, true);
				if (!string.IsNullOrEmpty(this.body.Options))
				{
					writer.WriteProperty<string>(options, "Options", this.body.Options);
				}
			}
			if (!string.IsNullOrEmpty(this.body.Credential) || this.credential.IsDirty)
			{
				this.credential.ExtractMetadataValueIfNeeded(ref this.body.Credential, true);
				if (!string.IsNullOrEmpty(this.body.Credential))
				{
					writer.WriteProperty<string>(options, "Credential", this.body.Credential);
				}
			}
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0005F884 File Offset: 0x0005DA84
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			string text;
			if (reader.TryReadProperty<string>("ConnectionDetails", out text))
			{
				this.body.ConnectionDetails = text;
				this.connectionDetails.UpdateProperty(this.body.ConnectionDetails);
			}
			string text2;
			if (reader.TryReadProperty<string>("Options", out text2))
			{
				this.body.Options = text2;
				this.options.UpdateProperty(this.body.Options);
			}
			string text3;
			if (reader.TryReadProperty<string>("Credential", out text3))
			{
				this.body.Credential = text3;
				this.credential.UpdateProperty(this.body.Credential);
			}
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0005F930 File Offset: 0x0005DB30
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataBodyProperties(context, writer);
			if (!string.IsNullOrEmpty(this.body.ConnectionDetails) || this.connectionDetails.IsDirty)
			{
				if (!CompatibilityRestrictions.StructuredDataSource_ConnectionDetails.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Property ConnectionDetails is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("ConnectionDetails", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.JsonString))
				{
					this.connectionDetails.ExtractMetadataValueIfNeeded(ref this.body.ConnectionDetails, true);
					if (!string.IsNullOrEmpty(this.body.ConnectionDetails))
					{
						writer.WriteStringProperty("ConnectionDetails", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.JsonString, this.body.ConnectionDetails);
					}
				}
			}
			if (!string.IsNullOrEmpty(this.body.Options) || this.options.IsDirty)
			{
				if (!CompatibilityRestrictions.StructuredDataSource_Options.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Property Options is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("Options", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.JsonString))
				{
					this.options.ExtractMetadataValueIfNeeded(ref this.body.Options, true);
					if (!string.IsNullOrEmpty(this.body.Options))
					{
						writer.WriteStringProperty("Options", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.JsonString, this.body.Options);
					}
				}
			}
			if (!string.IsNullOrEmpty(this.body.Credential) || this.credential.IsDirty)
			{
				if (!CompatibilityRestrictions.StructuredDataSource_Credential.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Property Credential is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("Credential", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.Restricted | MetadataPropertyNature.JsonString))
				{
					this.credential.ExtractMetadataValueIfNeeded(ref this.body.Credential, true);
					if (!string.IsNullOrEmpty(this.body.Credential))
					{
						writer.WriteStringProperty("Credential", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.Restricted | MetadataPropertyNature.JsonString, this.body.Credential);
					}
				}
			}
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0005FB7C File Offset: 0x0005DD7C
		private protected override void WriteRegularPropertiesToMetadataStream(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteRegularPropertiesToMetadataStream(context, writer);
			if (!string.IsNullOrEmpty(this.body.ContextExpression))
			{
				if (!CompatibilityRestrictions.StructuredDataSource_ContextExpression.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Property ContextExpression is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("contextExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
				{
					writer.WriteStringProperty("contextExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.body.ContextExpression);
				}
			}
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0005FC18 File Offset: 0x0005DE18
		private protected override void WriteDirectChildrenToMetadataStream(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteDirectChildrenToMetadataStream(context, writer);
			if (!string.IsNullOrEmpty(this.body.ConnectionDetails) || this.connectionDetails.IsDirty)
			{
				if (!CompatibilityRestrictions.StructuredDataSource_ConnectionDetails.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Property ConnectionDetails is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("connectionDetails", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.JsonString))
				{
					JToken json = this.ConnectionDetails.GetJson();
					if (json != null)
					{
						writer.WriteCustomJsonProperty("connectionDetails", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.JsonString, json);
					}
				}
			}
			if (!string.IsNullOrEmpty(this.body.Options) || this.options.IsDirty)
			{
				if (!CompatibilityRestrictions.StructuredDataSource_Options.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Property Options is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("options", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.JsonString))
				{
					JToken json2 = this.Options.GetJson();
					if (json2 != null)
					{
						writer.WriteCustomJsonProperty("options", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.JsonString, json2);
					}
				}
			}
			if (!string.IsNullOrEmpty(this.body.Credential) || this.credential.IsDirty)
			{
				if (!CompatibilityRestrictions.StructuredDataSource_Credential.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Property Credential is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("credential", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.Restricted | MetadataPropertyNature.JsonString))
				{
					JToken json3 = this.Credential.GetJson();
					if (json3 != null)
					{
						writer.WriteCustomJsonProperty("credential", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.Restricted | MetadataPropertyNature.JsonString, json3);
					}
				}
			}
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0005FDF0 File Offset: 0x0005DFF0
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.StructuredDataSource.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object StructuredDataSource is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataTree(context, writer);
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0005FE4C File Offset: 0x0005E04C
		private protected override bool TryReadNextMetadataProperty(SerializationActivityContext context, IMetadataReader reader, out UnexpectedPropertyClassification classification)
		{
			if (base.TryReadNextMetadataProperty(context, reader, out classification))
			{
				return true;
			}
			if (classification != UnexpectedPropertyClassification.Unclassified)
			{
				return false;
			}
			string propertyName = reader.PropertyName;
			if (propertyName != null)
			{
				int length = propertyName.Length;
				if (length != 7)
				{
					if (length != 10)
					{
						if (length == 17)
						{
							char c = propertyName[0];
							if (c != 'C')
							{
								if (c != 'c')
								{
									goto IL_0313;
								}
								if (!(propertyName == "connectionDetails"))
								{
									if (!(propertyName == "contextExpression"))
									{
										goto IL_0313;
									}
									if (!CompatibilityRestrictions.StructuredDataSource_ContextExpression.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
									{
										classification = UnexpectedPropertyClassification.IncompatibleProperty;
										return false;
									}
									this.body.ContextExpression = reader.ReadStringProperty();
									return true;
								}
							}
							else if (!(propertyName == "ConnectionDetails"))
							{
								goto IL_0313;
							}
							if (!CompatibilityRestrictions.StructuredDataSource_ConnectionDetails.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
							{
								classification = UnexpectedPropertyClassification.IncompatibleProperty;
								return false;
							}
							JToken jtoken;
							if (context.SerializationMode != MetadataSerializationMode.Xmla && reader.TryReadCustomJsonProperty(out jtoken))
							{
								if (!this.connectionDetails.TryUpdatePropertyFromJson(jtoken, out this.body.ConnectionDetails))
								{
									this.body.ConnectionDetails = JsonPropertyHelper.ConvertJsonContentToString(jtoken);
									this.connectionDetails.UpdateProperty(this.body.ConnectionDetails);
								}
							}
							else
							{
								this.body.ConnectionDetails = reader.ReadStringProperty();
								this.connectionDetails.UpdateProperty(this.body.ConnectionDetails);
							}
							return true;
						}
					}
					else
					{
						char c = propertyName[0];
						if (c != 'C')
						{
							if (c != 'c')
							{
								goto IL_0313;
							}
							if (!(propertyName == "credential"))
							{
								goto IL_0313;
							}
						}
						else if (!(propertyName == "Credential"))
						{
							goto IL_0313;
						}
						if (!CompatibilityRestrictions.StructuredDataSource_Credential.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							classification = UnexpectedPropertyClassification.IncompatibleProperty;
							return false;
						}
						JToken jtoken2;
						if (context.SerializationMode != MetadataSerializationMode.Xmla && reader.TryReadCustomJsonProperty(out jtoken2))
						{
							if (!this.credential.TryUpdatePropertyFromJson(jtoken2, out this.body.Credential))
							{
								this.body.Credential = JsonPropertyHelper.ConvertJsonContentToString(jtoken2);
								this.credential.UpdateProperty(this.body.Credential);
							}
						}
						else
						{
							this.body.Credential = reader.ReadStringProperty();
							this.credential.UpdateProperty(this.body.Credential);
						}
						return true;
					}
				}
				else
				{
					char c = propertyName[0];
					if (c != 'O')
					{
						if (c != 'o')
						{
							goto IL_0313;
						}
						if (!(propertyName == "options"))
						{
							goto IL_0313;
						}
					}
					else if (!(propertyName == "Options"))
					{
						goto IL_0313;
					}
					if (!CompatibilityRestrictions.StructuredDataSource_Options.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					JToken jtoken3;
					if (context.SerializationMode != MetadataSerializationMode.Xmla && reader.TryReadCustomJsonProperty(out jtoken3))
					{
						if (!this.options.TryUpdatePropertyFromJson(jtoken3, out this.body.Options))
						{
							this.body.Options = JsonPropertyHelper.ConvertJsonContentToString(jtoken3);
							this.options.UpdateProperty(this.body.Options);
						}
					}
					else
					{
						this.body.Options = reader.ReadStringProperty();
						this.options.UpdateProperty(this.body.Options);
					}
					return true;
				}
			}
			IL_0313:
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00060170 File Offset: 0x0005E370
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.SerializeToJsonObject(result, options, mode, dbCompatibilityLevel);
			if (options.IncludeTranslatablePropertiesOnly)
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.body.ConnectionDetails) || this.connectionDetails.IsDirty)
			{
				if (!CompatibilityRestrictions.StructuredDataSource_ConnectionDetails.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Property ConnectionDetails is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				this.connectionDetails.ExtractMetadataValueIfNeeded(ref this.body.ConnectionDetails, true);
				if (!string.IsNullOrEmpty(this.body.ConnectionDetails))
				{
					result["connectionDetails", TomPropCategory.Regular, 14, false] = JsonPropertyHelper.ConvertStringToJsonObject(this.body.ConnectionDetails, "ConnectionDetails");
				}
			}
			if (!string.IsNullOrEmpty(this.body.Options) || this.options.IsDirty)
			{
				if (!CompatibilityRestrictions.StructuredDataSource_Options.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Property Options is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				this.options.ExtractMetadataValueIfNeeded(ref this.body.Options, true);
				if (!string.IsNullOrEmpty(this.body.Options))
				{
					result["options", TomPropCategory.Regular, 15, false] = JsonPropertyHelper.ConvertStringToJsonObject(this.body.Options, "Options");
				}
			}
			if (!string.IsNullOrEmpty(this.body.Credential) || this.credential.IsDirty)
			{
				if (!CompatibilityRestrictions.StructuredDataSource_Credential.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Property Credential is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				this.credential.ExtractMetadataValueIfNeeded(ref this.body.Credential, true);
				if (!string.IsNullOrEmpty(this.body.Credential))
				{
					result["credential", TomPropCategory.Regular, 16, false] = JsonPropertyHelper.ConvertStringToJsonObject(options.IncludeRestrictedInformation ? this.body.Credential : PropertyHelper.GetCuratedValueForCredential(this.body.Credential), "Credential");
				}
			}
			if (!string.IsNullOrEmpty(this.body.ContextExpression))
			{
				if (!CompatibilityRestrictions.StructuredDataSource_ContextExpression.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Property ContextExpression is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["contextExpression", TomPropCategory.Regular, 17, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.ContextExpression, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x00060404 File Offset: 0x0005E604
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (base.ReadPropertyFromJson(jsonProp, options, mode, dbCompatibilityLevel))
			{
				return true;
			}
			string name = jsonProp.Name;
			if (!(name == "connectionDetails"))
			{
				if (!(name == "options"))
				{
					if (!(name == "credential"))
					{
						if (!(name == "contextExpression"))
						{
							return false;
						}
						if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.StructuredDataSource_ContextExpression.IsCompatible(mode, dbCompatibilityLevel))
						{
							return false;
						}
						this.ContextExpression = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					else
					{
						if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.StructuredDataSource_Credential.IsCompatible(mode, dbCompatibilityLevel))
						{
							return false;
						}
						if (!this.credential.TryUpdatePropertyFromJson(jsonProp.Value, out this.body.Credential))
						{
							this.body.Credential = JsonPropertyHelper.ConvertJsonContentToString(jsonProp.Value);
							this.credential.UpdateProperty(this.body.Credential);
						}
						return true;
					}
				}
				else
				{
					if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.StructuredDataSource_Options.IsCompatible(mode, dbCompatibilityLevel))
					{
						return false;
					}
					if (!this.options.TryUpdatePropertyFromJson(jsonProp.Value, out this.body.Options))
					{
						this.body.Options = JsonPropertyHelper.ConvertJsonContentToString(jsonProp.Value);
						this.options.UpdateProperty(this.body.Options);
					}
					return true;
				}
			}
			else
			{
				if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.StructuredDataSource_ConnectionDetails.IsCompatible(mode, dbCompatibilityLevel))
				{
					return false;
				}
				if (!this.connectionDetails.TryUpdatePropertyFromJson(jsonProp.Value, out this.body.ConnectionDetails))
				{
					this.body.ConnectionDetails = JsonPropertyHelper.ConvertJsonContentToString(jsonProp.Value);
					this.connectionDetails.UpdateProperty(this.body.ConnectionDetails);
				}
				return true;
			}
		}

		// Token: 0x04000174 RID: 372
		internal CustomProperty<StructuredDataSource, string, ConnectionDetails> connectionDetails;

		// Token: 0x04000175 RID: 373
		internal CustomProperty<StructuredDataSource, string, DataSourceOptions> options;

		// Token: 0x04000176 RID: 374
		internal CustomProperty<StructuredDataSource, string, Credential> credential;
	}
}
