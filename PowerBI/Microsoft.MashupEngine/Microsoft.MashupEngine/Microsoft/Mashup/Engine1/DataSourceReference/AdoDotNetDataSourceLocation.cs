using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.AdoDotNet;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018C8 RID: 6344
	internal sealed class AdoDotNetDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A1BD RID: 41405 RVA: 0x00218DB6 File Offset: 0x00216FB6
		public AdoDotNetDataSourceLocation()
		{
			base.Protocol = "ado-dot-net";
		}

		// Token: 0x17002957 RID: 10583
		// (get) Token: 0x0600A1BE RID: 41406 RVA: 0x0016368C File Offset: 0x0016188C
		public override string ResourceKind
		{
			get
			{
				return "AdoDotNet";
			}
		}

		// Token: 0x17002958 RID: 10584
		// (get) Token: 0x0600A1BF RID: 41407 RVA: 0x00218DCC File Offset: 0x00216FCC
		public override string FriendlyName
		{
			get
			{
				try
				{
					string connectionString = this.GetConnectionString();
					if (connectionString != string.Empty)
					{
						return connectionString;
					}
				}
				catch (FormatException)
				{
				}
				return "ADO.NET";
			}
		}

		// Token: 0x17002959 RID: 10585
		// (get) Token: 0x0600A1C0 RID: 41408 RVA: 0x00218E10 File Offset: 0x00217010
		// (set) Token: 0x0600A1C1 RID: 41409 RVA: 0x00218E4B File Offset: 0x0021704B
		public Dictionary<string, object> Options
		{
			get
			{
				object obj;
				Dictionary<string, object> dictionary;
				if (base.Address.TryGetValue("options", out obj))
				{
					dictionary = obj as Dictionary<string, object>;
					if (dictionary != null)
					{
						return dictionary;
					}
				}
				dictionary = new Dictionary<string, object>();
				this.Options = dictionary;
				return dictionary;
			}
			set
			{
				base.Address["options"] = value;
				base.Address.Remove("connectionstring");
			}
		}

		// Token: 0x1700295A RID: 10586
		// (get) Token: 0x0600A1C2 RID: 41410 RVA: 0x00218E70 File Offset: 0x00217070
		private string ProviderName
		{
			get
			{
				string stringOrNull = base.Address.GetStringOrNull("provider");
				if (stringOrNull != null)
				{
					return stringOrNull;
				}
				string stringOrNull2 = base.Address.GetStringOrNull("class");
				if (stringOrNull2 != null)
				{
					return stringOrNull2;
				}
				return null;
			}
		}

		// Token: 0x0600A1C3 RID: 41411 RVA: 0x00218EAC File Offset: 0x002170AC
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			RecordValue recordValue = RecordValue.Empty;
			try
			{
				recordValue = AdoDotNetModule.OptionRecord.FromJson(optionsJson);
			}
			catch (ArgumentException ex)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
			}
			IFormulaCreationResult formulaCreationResult;
			try
			{
				if (this.ProviderName == null)
				{
					throw new FormatException();
				}
				if (base.Address.ContainsKey("connectionstring"))
				{
					throw new FormatException();
				}
				if (!string.IsNullOrEmpty(base.Query))
				{
					formulaCreationResult = DataSourceLocation.FormatInvocation("AdoDotNet.Query", 4, new object[]
					{
						this.ProviderName,
						this.GetConnectionString(),
						base.Query,
						recordValue
					});
				}
				else
				{
					formulaCreationResult = DataSourceLocation.FormatInvocation("AdoDotNet.DataSource", 3, new object[]
					{
						this.ProviderName,
						this.GetConnectionString(),
						recordValue
					});
				}
			}
			catch (FormatException)
			{
				formulaCreationResult = new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidDataSourceReference, null);
			}
			return formulaCreationResult;
		}

		// Token: 0x0600A1C4 RID: 41412 RVA: 0x00218F9C File Offset: 0x0021719C
		private string GetConnectionString()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (KeyValuePair<string, object> keyValuePair in this.Options)
			{
				if (keyValuePair.Value != null)
				{
					string text = keyValuePair.Value as string;
					if (text == null)
					{
						throw new FormatException();
					}
					dictionary.Add(keyValuePair.Key, text);
				}
			}
			IConnectionStringService connectionStringService;
			string text2;
			if (this.ProviderName == null || !AdoDotNetConnectionStringService.Handler.TryGetConnectionStringService(this.ProviderName, false, out connectionStringService) || !connectionStringService.TryBuildConnectionString(dictionary, out text2))
			{
				throw new FormatException();
			}
			return text2;
		}

		// Token: 0x0600A1C5 RID: 41413 RVA: 0x00219050 File Offset: 0x00217250
		public override bool TryGetResource(out IResource resource)
		{
			Dictionary<string, object> dictionary = base.Address.GetValueOrDefault("options", null) as Dictionary<string, object>;
			if (dictionary == null)
			{
				resource = null;
				return false;
			}
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
			foreach (KeyValuePair<string, object> keyValuePair in dictionary)
			{
				string text = keyValuePair.Value as string;
				if (text == null)
				{
					resource = null;
					return false;
				}
				dictionary2[keyValuePair.Key] = text;
			}
			IConnectionStringService connectionStringService;
			string text2;
			if (this.ProviderName == null || !AdoDotNetConnectionStringService.Handler.TryGetConnectionStringService(this.ProviderName, false, out connectionStringService) || !connectionStringService.TryBuildConnectionString(dictionary2, out text2))
			{
				resource = null;
				return false;
			}
			string text3;
			return AdoDotNetResourceKindInfo.Instance.TryCreateResource(this.ProviderName, text2, out resource, out text3);
		}

		// Token: 0x0600A1C6 RID: 41414 RVA: 0x0021912C File Offset: 0x0021732C
		public override void Normalize()
		{
			base.NormalizeConnectionString(AdoDotNetConnectionStringService.Instance);
			base.Normalize();
		}

		// Token: 0x0400549B RID: 21659
		public static readonly DataSourceLocationFactory Factory = new AdoDotNetDataSourceLocation.DslFactory();

		// Token: 0x0400549C RID: 21660
		private const string DefaultFriendlyName = "ADO.NET";

		// Token: 0x0400549D RID: 21661
		private const string AdoDotNet_Query_Function = "AdoDotNet.Query";

		// Token: 0x0400549E RID: 21662
		private const string AdoDotNet_DataSource_Function = "AdoDotNet.DataSource";

		// Token: 0x020018C9 RID: 6345
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x1700295B RID: 10587
			// (get) Token: 0x0600A1C8 RID: 41416 RVA: 0x0021914B File Offset: 0x0021734B
			public override string Protocol
			{
				get
				{
					return "ado-dot-net";
				}
			}

			// Token: 0x0600A1C9 RID: 41417 RVA: 0x00219152 File Offset: 0x00217352
			public override IDataSourceLocation New()
			{
				return new AdoDotNetDataSourceLocation();
			}

			// Token: 0x0600A1CA RID: 41418 RVA: 0x0021915C File Offset: 0x0021735C
			protected override bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
			{
				string text;
				string text2;
				IConnectionStringService connectionStringService;
				Dictionary<string, string> dictionary;
				if (AdoDotNetResourceKindInfo.Instance.TryGetProvider(resourcePath, out text, out text2) && AdoDotNetConnectionStringService.Handler.TryGetConnectionStringService(text, false, out connectionStringService) && connectionStringService.TryParseConnectionString(text2, out dictionary))
				{
					AdoDotNetDataSourceLocation adoDotNetDataSourceLocation = new AdoDotNetDataSourceLocation();
					adoDotNetDataSourceLocation.Address["provider"] = text;
					Dictionary<string, object> options = adoDotNetDataSourceLocation.Options;
					foreach (KeyValuePair<string, string> keyValuePair in dictionary)
					{
						options[keyValuePair.Key] = keyValuePair.Value;
					}
					location = adoDotNetDataSourceLocation;
					return true;
				}
				location = null;
				return false;
			}
		}
	}
}
