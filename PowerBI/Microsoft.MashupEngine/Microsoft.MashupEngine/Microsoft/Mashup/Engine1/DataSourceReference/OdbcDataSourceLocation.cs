using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.Odbc;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018F9 RID: 6393
	internal sealed class OdbcDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A2F4 RID: 41716 RVA: 0x0021BF78 File Offset: 0x0021A178
		public OdbcDataSourceLocation()
		{
			base.Protocol = "odbc";
		}

		// Token: 0x170029AA RID: 10666
		// (get) Token: 0x0600A2F5 RID: 41717 RVA: 0x00092F67 File Offset: 0x00091167
		public override string ResourceKind
		{
			get
			{
				return "Odbc";
			}
		}

		// Token: 0x170029AB RID: 10667
		// (get) Token: 0x0600A2F6 RID: 41718 RVA: 0x0021BF8C File Offset: 0x0021A18C
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
				return "ODBC";
			}
		}

		// Token: 0x170029AC RID: 10668
		// (get) Token: 0x0600A2F7 RID: 41719 RVA: 0x0021BFD0 File Offset: 0x0021A1D0
		// (set) Token: 0x0600A2F8 RID: 41720 RVA: 0x00218E4B File Offset: 0x0021704B
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

		// Token: 0x0600A2F9 RID: 41721 RVA: 0x0021C00C File Offset: 0x0021A20C
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			if (base.Address.ContainsKey("connectionstring"))
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidDataSourceReference, null);
			}
			IFormulaCreationResult formulaCreationResult2;
			try
			{
				if (!string.IsNullOrEmpty(base.Query))
				{
					IFormulaCreationResult formulaCreationResult = this.CreateQueryFormula(optionsJson);
					if (formulaCreationResult.Success)
					{
						return formulaCreationResult;
					}
				}
				RecordValue recordValue = null;
				try
				{
					recordValue = OdbcModule.OptionRecord.FromJson(optionsJson);
				}
				catch (ArgumentException ex)
				{
					return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
				}
				string stringOrNull = base.Address.GetStringOrNull("database");
				string stringOrNull2 = base.Address.GetStringOrNull("schema");
				string stringOrNull3 = base.Address.GetStringOrNull("object");
				ExpressionBuilder instance = ExpressionBuilder.Instance;
				IExpression expression = instance.Invoke("Odbc.DataSource", 1, new object[]
				{
					this.GetConnectionString(),
					recordValue
				});
				if (stringOrNull3 != null)
				{
					IExpression expression2 = instance.Record(new VariableInitializer[]
					{
						instance.Declare("Catalog", stringOrNull, stringOrNull != null),
						instance.Declare("Schema", stringOrNull2, stringOrNull2 != null),
						instance.Declare("Item", stringOrNull3, true)
					});
					formulaCreationResult2 = new FormulaCreationResult(instance.Member(instance.Index(expression, expression2), "Data"));
				}
				else if (stringOrNull != null || stringOrNull2 != null)
				{
					IExpression expression3 = instance.Each(instance.And(new IExpression[]
					{
						(stringOrNull == null) ? null : instance.Equals(instance.Member(Identifier.Underscore, "Catalog"), stringOrNull),
						(stringOrNull2 == null) ? null : instance.Equals(instance.Member(Identifier.Underscore, "Schema"), stringOrNull2)
					}));
					formulaCreationResult2 = new FormulaCreationResult(instance.Invoke("Table.SelectRows", new object[] { expression, expression3 }));
				}
				else
				{
					formulaCreationResult2 = new FormulaCreationResult(expression);
				}
			}
			catch (FormatException)
			{
				formulaCreationResult2 = new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidDataSourceReference, null);
			}
			return formulaCreationResult2;
		}

		// Token: 0x0600A2FA RID: 41722 RVA: 0x0021C22C File Offset: 0x0021A42C
		private IFormulaCreationResult CreateQueryFormula(string optionsJson)
		{
			RecordValue recordValue = null;
			try
			{
				recordValue = OdbcModule.QueryOptionRecord.FromJson(optionsJson);
			}
			catch (ArgumentException ex)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
			}
			return DataSourceLocation.FormatInvocation("Odbc.Query", 3, new object[]
			{
				TextValue.New(this.GetConnectionString()),
				TextValue.New(base.Query),
				recordValue ?? RecordValue.Empty
			});
		}

		// Token: 0x0600A2FB RID: 41723 RVA: 0x0021C2A8 File Offset: 0x0021A4A8
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
			string text2;
			if (!OdbcConnectionStringService.Instance.TryBuildConnectionString(dictionary, out text2))
			{
				throw new FormatException();
			}
			return text2;
		}

		// Token: 0x0600A2FC RID: 41724 RVA: 0x0021C340 File Offset: 0x0021A540
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
			string text2;
			if (!OdbcConnectionStringService.Instance.TryBuildConnectionString(dictionary2, out text2))
			{
				resource = null;
				return false;
			}
			resource = Resource.New(this.ResourceKind, text2);
			return true;
		}

		// Token: 0x0600A2FD RID: 41725 RVA: 0x0021C3FC File Offset: 0x0021A5FC
		public override void Normalize()
		{
			base.NormalizeConnectionString(OdbcConnectionStringService.Instance);
			base.Normalize();
		}

		// Token: 0x040054E2 RID: 21730
		public static readonly DataSourceLocationFactory Factory = new OdbcDataSourceLocation.DslFactory();

		// Token: 0x040054E3 RID: 21731
		private const string DefaultFriendlyName = "ODBC";

		// Token: 0x040054E4 RID: 21732
		private const string Odbc_Query_Function = "Odbc.Query";

		// Token: 0x040054E5 RID: 21733
		private const string Odbc_DataSource_Function = "Odbc.DataSource";

		// Token: 0x020018FA RID: 6394
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x170029AD RID: 10669
			// (get) Token: 0x0600A2FF RID: 41727 RVA: 0x0021C41B File Offset: 0x0021A61B
			public override string Protocol
			{
				get
				{
					return "odbc";
				}
			}

			// Token: 0x0600A300 RID: 41728 RVA: 0x0021C422 File Offset: 0x0021A622
			public override IDataSourceLocation New()
			{
				return new OdbcDataSourceLocation();
			}

			// Token: 0x0600A301 RID: 41729 RVA: 0x0021C42C File Offset: 0x0021A62C
			protected override bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
			{
				Dictionary<string, string> dictionary;
				if (OdbcConnectionStringService.Instance.TryParseConnectionString(resourcePath, out dictionary))
				{
					OdbcDataSourceLocation odbcDataSourceLocation = new OdbcDataSourceLocation();
					Dictionary<string, object> options = odbcDataSourceLocation.Options;
					foreach (KeyValuePair<string, string> keyValuePair in dictionary)
					{
						options[keyValuePair.Key] = keyValuePair.Value;
					}
					location = odbcDataSourceLocation;
					return true;
				}
				location = null;
				return false;
			}
		}
	}
}
