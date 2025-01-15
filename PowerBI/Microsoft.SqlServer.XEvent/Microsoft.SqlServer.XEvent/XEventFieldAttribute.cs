using System;
using System.Runtime.InteropServices;
using Microsoft.SqlServer.XEvent.Internal;

namespace Microsoft.SqlServer.XEvent
{
	// Token: 0x02000007 RID: 7
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	public sealed class XEventFieldAttribute : Attribute
	{
		// Token: 0x0600014F RID: 335 RVA: 0x00002E38 File Offset: 0x00002E38
		public XEventFieldAttribute(string name, string descriptionKey, ComplianceTag complianceTag, string privacyTags)
		{
			XEventPackageRegistrar.CheckNamingRules(name);
			this.m_name = name;
			this.m_descriptionKey = descriptionKey;
			this.m_complianceTag = complianceTag;
			this.m_privacyTags = this.ValidateAndExtractPrivacyTags(privacyTags);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00002DF4 File Offset: 0x00002DF4
		public XEventFieldAttribute(string name, string descriptionKey, ComplianceTag complianceTag)
		{
			XEventPackageRegistrar.CheckNamingRules(name);
			this.m_name = name;
			this.m_descriptionKey = descriptionKey;
			this.m_complianceTag = complianceTag;
			this.m_privacyTags = this.ValidateAndExtractPrivacyTags(null);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00002DAC File Offset: 0x00002DAC
		public XEventFieldAttribute(string name, string descriptionKey)
		{
			XEventPackageRegistrar.CheckNamingRules(name);
			this.m_name = name;
			this.m_descriptionKey = descriptionKey;
			this.m_complianceTag = ComplianceTag.Untagged;
			this.m_privacyTags = this.ValidateAndExtractPrivacyTags(null);
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00002968 File Offset: 0x00002968
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00002984 File Offset: 0x00002984
		public string DescriptionKey
		{
			get
			{
				return this.m_descriptionKey;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000154 RID: 340 RVA: 0x000029A0 File Offset: 0x000029A0
		public ComplianceTag EventFieldComplianceTag
		{
			get
			{
				return this.m_complianceTag;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000155 RID: 341 RVA: 0x000029BC File Offset: 0x000029BC
		public string[] PrivacyTags
		{
			get
			{
				return this.m_privacyTags;
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00002CE4 File Offset: 0x00002CE4
		private string[] ValidateAndExtractPrivacyTags(string privacyTags)
		{
			if (privacyTags == null)
			{
				return new string[0];
			}
			string[] array = privacyTags.Split(new string[] { ",", " " }, StringSplitOptions.None);
			bool flag = true;
			int num = 0;
			if (0 < array.Length)
			{
				do
				{
					flag = this.ValidatePrivacyTag(array[num]) && flag;
					num++;
				}
				while (num < array.Length);
				if (!flag)
				{
					throw new ArgumentException(string.Format("Error validating privacy tags, please check for typos: {0}", privacyTags, "privacyTags"));
				}
			}
			return array;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000029D8 File Offset: 0x000029D8
		[return: MarshalAs(UnmanagedType.U1)]
		private bool ValidatePrivacyTag(string privacyTag)
		{
			bool flag = privacyTag.EndsWith("*");
			int num = 0;
			if (0 < XEventFieldAttribute.PrivacyTagList.Length)
			{
				do
				{
					int num2;
					if (!flag && !XEventFieldAttribute.PrivacyTagList[num].Equals(privacyTag))
					{
						num2 = 0;
					}
					else
					{
						num2 = 1;
					}
					flag = (byte)num2 != 0;
					if (flag)
					{
						break;
					}
					num++;
				}
				while (num < XEventFieldAttribute.PrivacyTagList.Length);
			}
			return flag;
		}

		// Token: 0x040000E9 RID: 233
		private string m_name;

		// Token: 0x040000EA RID: 234
		private string m_descriptionKey;

		// Token: 0x040000EB RID: 235
		private ComplianceTag m_complianceTag;

		// Token: 0x040000EC RID: 236
		private string[] m_privacyTags;

		// Token: 0x040000ED RID: 237
		private static string[] PrivacyTagList = new string[]
		{
			"Privacy.Subject.EmailAcount", "Privacy.Subject.IPAddress", "Privacy.Subject.Name", "Privacy.Subject.User.OtherIdentifier", "Privacy.Subject.Device.OtherIdentifier", "Privacy.Subject.User.ObjectId", "Privacy.Subject.User.Puid", "Privacy.Subject.User.Anid", "Privacy.Subject.User.Xuid", "Privacy.Subject.OrgIDPuid",
			"Privacy.Subject.ExternalIdentifier", "Privacy.Subject.User.SID", "Privacy.Subject.MicrosoftEmployee.EmailAccount", "Privacy.Subject.MicrosoftEmployee.EmployeeId", "Privacy.Subject.PhoneNumber", "Privacy.Subject.Foreign", "Privacy.DataType.PreciseUserLocation.Related", "Privacy.DataType.AccountData.Payment", "Privacy.DataType.Account.Related", "Privacy.DataType.Support.Related",
			"Privacy.DataType.PublicPersonalData.Related", "Privacy.DataType.Privacy.PSA", "Privacy.DataType.CustomerContent.Related", "Privacy.NotRelated", "Privacy.Asset.Tenant", "Privacy.DataType.SoftwareSetupAndInventory.Related", "Privacy.DataType.DeviceConnectivityAndConfiguration.Related"
		};
	}
}
