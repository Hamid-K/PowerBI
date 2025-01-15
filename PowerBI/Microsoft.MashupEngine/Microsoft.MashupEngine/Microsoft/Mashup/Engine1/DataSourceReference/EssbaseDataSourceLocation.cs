using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.Essbase;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018D9 RID: 6361
	internal sealed class EssbaseDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A22F RID: 41519 RVA: 0x0021A2C8 File Offset: 0x002184C8
		public EssbaseDataSourceLocation()
		{
			base.Protocol = "essbase";
		}

		// Token: 0x1700297A RID: 10618
		// (get) Token: 0x0600A230 RID: 41520 RVA: 0x0021A2DB File Offset: 0x002184DB
		public override IEnumerable<string> DisplayAddressFields
		{
			get
			{
				return EssbaseDataSourceLocation.displayAddressFields;
			}
		}

		// Token: 0x1700297B RID: 10619
		// (get) Token: 0x0600A231 RID: 41521 RVA: 0x0021A2E4 File Offset: 0x002184E4
		public override string FriendlyName
		{
			get
			{
				IResource resource;
				if (this.TryGetResource(out resource))
				{
					return resource.Path;
				}
				return base.FriendlyName;
			}
		}

		// Token: 0x1700297C RID: 10620
		// (get) Token: 0x0600A232 RID: 41522 RVA: 0x0012BB63 File Offset: 0x00129D63
		public override string ResourceKind
		{
			get
			{
				return "Essbase";
			}
		}

		// Token: 0x1700297D RID: 10621
		// (get) Token: 0x0600A233 RID: 41523 RVA: 0x0021A0B8 File Offset: 0x002182B8
		// (set) Token: 0x0600A234 RID: 41524 RVA: 0x0021A0CA File Offset: 0x002182CA
		public string ProviderUrl
		{
			get
			{
				return base.Address.GetStringOrNull("url");
			}
			set
			{
				base.Address["url"] = value;
			}
		}

		// Token: 0x1700297E RID: 10622
		// (get) Token: 0x0600A235 RID: 41525 RVA: 0x0021A308 File Offset: 0x00218508
		// (set) Token: 0x0600A236 RID: 41526 RVA: 0x0021A31A File Offset: 0x0021851A
		public string ServerName
		{
			get
			{
				return base.Address.GetStringOrNull("servername");
			}
			set
			{
				base.Address["servername"] = value;
			}
		}

		// Token: 0x1700297F RID: 10623
		// (get) Token: 0x0600A237 RID: 41527 RVA: 0x0021A32D File Offset: 0x0021852D
		// (set) Token: 0x0600A238 RID: 41528 RVA: 0x0021A33F File Offset: 0x0021853F
		public string ApplicationName
		{
			get
			{
				return base.Address.GetStringOrNull("catalogname");
			}
			set
			{
				base.Address["catalogname"] = value;
			}
		}

		// Token: 0x0600A239 RID: 41529 RVA: 0x0021A354 File Offset: 0x00218554
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			RecordValue recordValue;
			try
			{
				recordValue = EssbaseModule.OptionRecord.FromJson(optionsJson);
			}
			catch (ArgumentException ex)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
			}
			string providerUrl = this.ProviderUrl;
			if (providerUrl == null)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidDataSourceReference, null);
			}
			string serverName = this.ServerName;
			string applicationName = this.ApplicationName;
			string query = base.Query;
			ExpressionBuilder instance = ExpressionBuilder.Instance;
			IExpression expression = instance.Invoke("Essbase.Cubes", 1, new object[]
			{
				TextValue.New(providerUrl),
				recordValue
			});
			if (query == null && applicationName != null && serverName != null)
			{
				return new FormulaCreationResult(instance.Let(new VariableInitializer[]
				{
					instance.Declare("Source", expression, true),
					instance.Declare("Server", instance.Navigate(instance.Identifier("Source"), "Name", serverName, "Data"), true),
					instance.Declare("Application", instance.Navigate(instance.Identifier("Server"), "Name", applicationName, "Data"), true)
				}));
			}
			return new FormulaCreationResult(expression);
		}

		// Token: 0x0600A23A RID: 41530 RVA: 0x0021A118 File Offset: 0x00218318
		public override bool TryGetResource(out IResource resource)
		{
			return base.TryGetUrlResource(out resource);
		}

		// Token: 0x040054B6 RID: 21686
		public static readonly DataSourceLocationFactory Factory = new EssbaseDataSourceLocation.DslFactory();

		// Token: 0x040054B7 RID: 21687
		private const string ServerNameField = "servername";

		// Token: 0x040054B8 RID: 21688
		private const string ApplicationNameField = "catalogname";

		// Token: 0x040054B9 RID: 21689
		private static readonly string[] displayAddressFields = new string[] { "url" };

		// Token: 0x020018DA RID: 6362
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x17002980 RID: 10624
			// (get) Token: 0x0600A23C RID: 41532 RVA: 0x0021A4B7 File Offset: 0x002186B7
			public override string Protocol
			{
				get
				{
					return "essbase";
				}
			}

			// Token: 0x0600A23D RID: 41533 RVA: 0x0021A4BE File Offset: 0x002186BE
			public override IDataSourceLocation New()
			{
				return new EssbaseDataSourceLocation();
			}

			// Token: 0x0600A23E RID: 41534 RVA: 0x0021A4C5 File Offset: 0x002186C5
			protected override bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
			{
				return DataSourceLocationFactory.TryCreateUrlLocation("essbase", resourcePath, out location);
			}
		}
	}
}
