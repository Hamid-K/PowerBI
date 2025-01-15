using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.EntityClient
{
	// Token: 0x020005DD RID: 1501
	public sealed class EntityConnectionStringBuilder : DbConnectionStringBuilder
	{
		// Token: 0x0600490A RID: 18698 RVA: 0x00103D86 File Offset: 0x00101F86
		public EntityConnectionStringBuilder()
		{
		}

		// Token: 0x0600490B RID: 18699 RVA: 0x00103D8E File Offset: 0x00101F8E
		public EntityConnectionStringBuilder(string connectionString)
		{
			base.ConnectionString = connectionString;
		}

		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x0600490C RID: 18700 RVA: 0x00103D9D File Offset: 0x00101F9D
		// (set) Token: 0x0600490D RID: 18701 RVA: 0x00103DAE File Offset: 0x00101FAE
		[DisplayName("Name")]
		[EntityResCategory("EntityDataCategory_NamedConnectionString")]
		[EntityResDescription("EntityConnectionString_Name")]
		[RefreshProperties(RefreshProperties.All)]
		public string Name
		{
			get
			{
				return this._namedConnectionName ?? "";
			}
			set
			{
				this._namedConnectionName = value;
				base["name"] = value;
			}
		}

		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x0600490E RID: 18702 RVA: 0x00103DC3 File Offset: 0x00101FC3
		// (set) Token: 0x0600490F RID: 18703 RVA: 0x00103DD4 File Offset: 0x00101FD4
		[DisplayName("Provider")]
		[EntityResCategory("EntityDataCategory_Source")]
		[EntityResDescription("EntityConnectionString_Provider")]
		[RefreshProperties(RefreshProperties.All)]
		public string Provider
		{
			get
			{
				return this._providerName ?? "";
			}
			set
			{
				this._providerName = value;
				base["provider"] = value;
			}
		}

		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x06004910 RID: 18704 RVA: 0x00103DE9 File Offset: 0x00101FE9
		// (set) Token: 0x06004911 RID: 18705 RVA: 0x00103DFA File Offset: 0x00101FFA
		[DisplayName("Metadata")]
		[EntityResCategory("EntityDataCategory_Context")]
		[EntityResDescription("EntityConnectionString_Metadata")]
		[RefreshProperties(RefreshProperties.All)]
		public string Metadata
		{
			get
			{
				return this._metadataLocations ?? "";
			}
			set
			{
				this._metadataLocations = value;
				base["metadata"] = value;
			}
		}

		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x06004912 RID: 18706 RVA: 0x00103E0F File Offset: 0x0010200F
		// (set) Token: 0x06004913 RID: 18707 RVA: 0x00103E20 File Offset: 0x00102020
		[DisplayName("Provider Connection String")]
		[EntityResCategory("EntityDataCategory_Source")]
		[EntityResDescription("EntityConnectionString_ProviderConnectionString")]
		[RefreshProperties(RefreshProperties.All)]
		public string ProviderConnectionString
		{
			get
			{
				return this._storeProviderConnectionString ?? "";
			}
			set
			{
				this._storeProviderConnectionString = value;
				base["provider connection string"] = value;
			}
		}

		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x06004914 RID: 18708 RVA: 0x00103E35 File Offset: 0x00102035
		public override bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x06004915 RID: 18709 RVA: 0x00103E38 File Offset: 0x00102038
		public override ICollection Keys
		{
			get
			{
				return new ReadOnlyCollection<string>(EntityConnectionStringBuilder.ValidKeywords);
			}
		}

		// Token: 0x17000E69 RID: 3689
		public override object this[string keyword]
		{
			get
			{
				Check.NotNull<string>(keyword, "keyword");
				if (string.Compare(keyword, "metadata", StringComparison.OrdinalIgnoreCase) == 0)
				{
					return this.Metadata;
				}
				if (string.Compare(keyword, "provider connection string", StringComparison.OrdinalIgnoreCase) == 0)
				{
					return this.ProviderConnectionString;
				}
				if (string.Compare(keyword, "name", StringComparison.OrdinalIgnoreCase) == 0)
				{
					return this.Name;
				}
				if (string.Compare(keyword, "provider", StringComparison.OrdinalIgnoreCase) == 0)
				{
					return this.Provider;
				}
				throw new ArgumentException(Strings.EntityClient_KeywordNotSupported(keyword));
			}
			set
			{
				Check.NotNull<string>(keyword, "keyword");
				if (value == null)
				{
					this.Remove(keyword);
					return;
				}
				string text = value as string;
				if (text == null)
				{
					throw new ArgumentException(Strings.EntityClient_ValueNotString, "value");
				}
				if (string.Compare(keyword, "metadata", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.Metadata = text;
					return;
				}
				if (string.Compare(keyword, "provider connection string", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.ProviderConnectionString = text;
					return;
				}
				if (string.Compare(keyword, "name", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.Name = text;
					return;
				}
				if (string.Compare(keyword, "provider", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.Provider = text;
					return;
				}
				throw new ArgumentException(Strings.EntityClient_KeywordNotSupported(keyword));
			}
		}

		// Token: 0x06004918 RID: 18712 RVA: 0x00103F5E File Offset: 0x0010215E
		public override void Clear()
		{
			base.Clear();
			this._namedConnectionName = null;
			this._providerName = null;
			this._metadataLocations = null;
			this._storeProviderConnectionString = null;
		}

		// Token: 0x06004919 RID: 18713 RVA: 0x00103F84 File Offset: 0x00102184
		public override bool ContainsKey(string keyword)
		{
			Check.NotNull<string>(keyword, "keyword");
			string[] validKeywords = EntityConnectionStringBuilder.ValidKeywords;
			for (int i = 0; i < validKeywords.Length; i++)
			{
				if (validKeywords[i].Equals(keyword, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600491A RID: 18714 RVA: 0x00103FC0 File Offset: 0x001021C0
		public override bool TryGetValue(string keyword, out object value)
		{
			Check.NotNull<string>(keyword, "keyword");
			if (this.ContainsKey(keyword))
			{
				value = this[keyword];
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600491B RID: 18715 RVA: 0x00103FE8 File Offset: 0x001021E8
		public override bool Remove(string keyword)
		{
			if (string.Compare(keyword, "metadata", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this._metadataLocations = null;
			}
			else if (string.Compare(keyword, "provider connection string", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this._storeProviderConnectionString = null;
			}
			else if (string.Compare(keyword, "name", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this._namedConnectionName = null;
			}
			else if (string.Compare(keyword, "provider", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this._providerName = null;
			}
			return base.Remove(keyword);
		}

		// Token: 0x040019DB RID: 6619
		internal const string NameParameterName = "name";

		// Token: 0x040019DC RID: 6620
		internal const string MetadataParameterName = "metadata";

		// Token: 0x040019DD RID: 6621
		internal const string ProviderParameterName = "provider";

		// Token: 0x040019DE RID: 6622
		internal const string ProviderConnectionStringParameterName = "provider connection string";

		// Token: 0x040019DF RID: 6623
		internal static readonly string[] ValidKeywords = new string[] { "name", "metadata", "provider", "provider connection string" };

		// Token: 0x040019E0 RID: 6624
		private string _namedConnectionName;

		// Token: 0x040019E1 RID: 6625
		private string _providerName;

		// Token: 0x040019E2 RID: 6626
		private string _metadataLocations;

		// Token: 0x040019E3 RID: 6627
		private string _storeProviderConnectionString;
	}
}
