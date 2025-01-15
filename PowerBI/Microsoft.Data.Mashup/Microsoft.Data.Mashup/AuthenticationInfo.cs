using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000008 RID: 8
	public sealed class AuthenticationInfo
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00002BEF File Offset: 0x00000DEF
		internal AuthenticationInfo(string kind, ICollection<CredentialProperty> properties, ICollection<CredentialProperty> applicationProperties = null, string label = null)
		{
			this.kind = kind;
			this.properties = properties;
			this.applicationProperties = applicationProperties ?? AuthenticationInfo.emptyCredentialProperties;
			this.label = label;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002C1D File Offset: 0x00000E1D
		public string Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002C25 File Offset: 0x00000E25
		public string Label
		{
			get
			{
				return this.label ?? this.kind;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002C37 File Offset: 0x00000E37
		public ICollection<CredentialProperty> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002C3F File Offset: 0x00000E3F
		public ICollection<CredentialProperty> ApplicationProperties
		{
			get
			{
				return this.applicationProperties;
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002C48 File Offset: 0x00000E48
		internal AuthenticationInfo SetLabelsAndProperties(AuthenticationInfo info)
		{
			List<CredentialProperty> list = new List<CredentialProperty>();
			if (info.ApplicationProperties != null)
			{
				foreach (CredentialProperty credentialProperty in info.ApplicationProperties)
				{
					list.Add(new CredentialProperty
					{
						Name = credentialProperty.Name,
						Label = (info.GetPropertyLabel(credentialProperty.Name) ?? credentialProperty.Label),
						IsRequired = credentialProperty.IsRequired,
						IsSecret = credentialProperty.IsSecret,
						PropertyType = credentialProperty.PropertyType,
						AllowNull = credentialProperty.AllowNull
					});
				}
			}
			return new AuthenticationInfo(this.kind, this.properties.Select((CredentialProperty p) => AuthenticationInfo.ReplaceLabel(p, info)).ToList<CredentialProperty>().AsReadOnly(), list, info.Label);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002D58 File Offset: 0x00000F58
		private static CredentialProperty ReplaceLabel(CredentialProperty source, AuthenticationInfo info)
		{
			string propertyLabel = info.GetPropertyLabel(source.Name);
			if (propertyLabel == null)
			{
				return source;
			}
			return new CredentialProperty
			{
				Name = source.Name,
				Label = propertyLabel,
				IsRequired = source.IsRequired,
				IsSecret = source.IsSecret,
				PropertyType = source.PropertyType,
				AllowNull = source.AllowNull
			};
		}

		// Token: 0x0400000B RID: 11
		private static readonly ICollection<CredentialProperty> emptyCredentialProperties = new List<CredentialProperty>().AsReadOnly();

		// Token: 0x0400000C RID: 12
		private readonly ICollection<CredentialProperty> properties;

		// Token: 0x0400000D RID: 13
		private readonly ICollection<CredentialProperty> applicationProperties;

		// Token: 0x0400000E RID: 14
		private readonly string kind;

		// Token: 0x0400000F RID: 15
		private readonly string label;
	}
}
