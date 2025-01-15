using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Serialization;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200005A RID: 90
	public class ExternalModelRoleMember : ModelRoleMember
	{
		// Token: 0x06000493 RID: 1171 RVA: 0x00023377 File Offset: 0x00021577
		public ExternalModelRoleMember()
		{
			this.OnAfterConstructor();
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00023385 File Offset: 0x00021585
		internal ExternalModelRoleMember(IEqualityComparer<string> comparer)
			: base(comparer)
		{
			this.OnAfterConstructor();
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00023394 File Offset: 0x00021594
		private void OnAfterConstructor()
		{
			this.body.IdentityProvider = "default";
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x000233A6 File Offset: 0x000215A6
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new ExternalModelRoleMember();
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x000233AD File Offset: 0x000215AD
		// (set) Token: 0x06000498 RID: 1176 RVA: 0x000233B5 File Offset: 0x000215B5
		public new string IdentityProvider
		{
			get
			{
				return base.IdentityProvider;
			}
			set
			{
				if (this.Parent != null)
				{
					throw new InvalidOperationException(TomSR.Exception_CantChangeImmutableProperty("IdentityProvider", "ExternalModelRoleMember"));
				}
				this.OnPropertySettingIdentityProvider(value);
				base.IdentityProvider = value;
			}
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x000233E2 File Offset: 0x000215E2
		private void OnPropertySettingIdentityProvider(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentException(TomSR.Exception_ExternalRoleMemberEmptyIdentityProvider);
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x000233F7 File Offset: 0x000215F7
		// (set) Token: 0x0600049B RID: 1179 RVA: 0x000233FF File Offset: 0x000215FF
		public new RoleMemberType MemberType
		{
			get
			{
				return base.MemberType;
			}
			set
			{
				if (this.Parent != null)
				{
					throw new InvalidOperationException(TomSR.Exception_CantChangeImmutableProperty("MemberType", "ExternalModelRoleMember"));
				}
				base.MemberType = value;
			}
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00023428 File Offset: 0x00021628
		private protected override void WriteRegularPropertiesToMetadataStream(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteRegularPropertiesToMetadataStream(context, writer);
			if (!string.IsNullOrEmpty(this.body.IdentityProvider) && writer.ShouldIncludeProperty("identityProvider", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("identityProvider", MetadataPropertyNature.RegularProperty, this.body.IdentityProvider);
			}
			if (this.body.MemberType != RoleMemberType.Auto && writer.ShouldIncludeProperty("memberType", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<RoleMemberType>("memberType", MetadataPropertyNature.RegularProperty, this.body.MemberType);
			}
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x000234A8 File Offset: 0x000216A8
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
			if (propertyName == "identityProvider")
			{
				this.body.IdentityProvider = reader.ReadStringProperty();
				return true;
			}
			if (!(propertyName == "memberType"))
			{
				classification = UnexpectedPropertyClassification.UnknownProperty;
				return false;
			}
			this.body.MemberType = reader.ReadEnumProperty<RoleMemberType>();
			return true;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00023518 File Offset: 0x00021718
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.SerializeToJsonObject(result, options, mode, dbCompatibilityLevel);
			if (options.IncludeTranslatablePropertiesOnly)
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.body.IdentityProvider))
			{
				result["identityProvider", TomPropCategory.Regular, 5, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.IdentityProvider, SplitMultilineOptions.None);
			}
			if (this.body.MemberType != RoleMemberType.Auto)
			{
				result["memberType", TomPropCategory.Regular, 6, false] = JsonPropertyHelper.ConvertEnumToJsonValue<RoleMemberType>(this.MemberType);
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00023594 File Offset: 0x00021794
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (base.ReadPropertyFromJson(jsonProp, options, mode, dbCompatibilityLevel))
			{
				return true;
			}
			string name = jsonProp.Name;
			if (name == "identityProvider")
			{
				this.IdentityProvider = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
				return true;
			}
			if (!(name == "memberType"))
			{
				return false;
			}
			this.MemberType = JsonPropertyHelper.ConvertJsonValueToEnum<RoleMemberType>(jsonProp.Value);
			return true;
		}

		// Token: 0x040000F8 RID: 248
		private const string DefaultIdentityProvider = "default";
	}
}
