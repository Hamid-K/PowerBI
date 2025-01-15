using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.OleDb;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018FB RID: 6395
	internal sealed class OleDbDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A303 RID: 41731 RVA: 0x0021C4B0 File Offset: 0x0021A6B0
		public OleDbDataSourceLocation()
		{
			base.Protocol = "ole-db";
		}

		// Token: 0x170029AE RID: 10670
		// (get) Token: 0x0600A304 RID: 41732 RVA: 0x00088E34 File Offset: 0x00087034
		public override string ResourceKind
		{
			get
			{
				return "OleDb";
			}
		}

		// Token: 0x170029AF RID: 10671
		// (get) Token: 0x0600A305 RID: 41733 RVA: 0x0021C4C4 File Offset: 0x0021A6C4
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
				return "OLE DB";
			}
		}

		// Token: 0x170029B0 RID: 10672
		// (get) Token: 0x0600A306 RID: 41734 RVA: 0x0021C508 File Offset: 0x0021A708
		// (set) Token: 0x0600A307 RID: 41735 RVA: 0x00218E4B File Offset: 0x0021704B
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

		// Token: 0x0600A308 RID: 41736 RVA: 0x0021C544 File Offset: 0x0021A744
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
					recordValue = OleDbModule.DataSourceOptionRecord.FromJson(optionsJson);
				}
				catch (ArgumentException ex)
				{
					return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
				}
				if (!string.IsNullOrEmpty(base.Query))
				{
					recordValue = recordValue ?? RecordValue.Empty;
					recordValue = recordValue.Concatenate(RecordValue.New(Keys.New("Query"), new Value[] { TextValue.New(base.Query) })).AsRecord;
				}
				formulaCreationResult2 = DataSourceLocation.FormatInvocation("OleDb.DataSource", 1, new object[]
				{
					TextValue.New(this.GetConnectionString()),
					recordValue
				});
			}
			catch (FormatException)
			{
				formulaCreationResult2 = new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidDataSourceReference, null);
			}
			return formulaCreationResult2;
		}

		// Token: 0x0600A309 RID: 41737 RVA: 0x0021C648 File Offset: 0x0021A848
		private IFormulaCreationResult CreateQueryFormula(string optionsJson)
		{
			RecordValue recordValue = null;
			try
			{
				recordValue = OleDbModule.QueryOptionRecord.FromJson(optionsJson);
			}
			catch (ArgumentException ex)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
			}
			return DataSourceLocation.FormatInvocation("OleDb.Query", 3, new object[]
			{
				TextValue.New(this.GetConnectionString()),
				TextValue.New(base.Query),
				recordValue ?? RecordValue.Empty
			});
		}

		// Token: 0x0600A30A RID: 41738 RVA: 0x0021C6C4 File Offset: 0x0021A8C4
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
			if (!OleDbConnectionStringService.Instance.TryBuildConnectionString(dictionary, out text2))
			{
				throw new FormatException();
			}
			return text2;
		}

		// Token: 0x0600A30B RID: 41739 RVA: 0x0021C75C File Offset: 0x0021A95C
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
			if (!OleDbConnectionStringService.Instance.TryBuildConnectionString(dictionary2, out text2))
			{
				resource = null;
				return false;
			}
			resource = Resource.New(this.ResourceKind, text2);
			return true;
		}

		// Token: 0x0600A30C RID: 41740 RVA: 0x0021C818 File Offset: 0x0021AA18
		public override void Normalize()
		{
			base.NormalizeConnectionString(OleDbConnectionStringService.Instance);
			base.Normalize();
		}

		// Token: 0x040054E6 RID: 21734
		public static readonly DataSourceLocationFactory Factory = new OleDbDataSourceLocation.DslFactory();

		// Token: 0x040054E7 RID: 21735
		private const string DefaultFriendlyName = "OLE DB";

		// Token: 0x040054E8 RID: 21736
		private const string OleDb_DataSource_Function = "OleDb.DataSource";

		// Token: 0x040054E9 RID: 21737
		private const string OleDb_Query_Function = "OleDb.Query";

		// Token: 0x040054EA RID: 21738
		private const string NotImplementedSyntax = "...";

		// Token: 0x020018FC RID: 6396
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x170029B1 RID: 10673
			// (get) Token: 0x0600A30E RID: 41742 RVA: 0x0021C837 File Offset: 0x0021AA37
			public override string Protocol
			{
				get
				{
					return "ole-db";
				}
			}

			// Token: 0x0600A30F RID: 41743 RVA: 0x0021C83E File Offset: 0x0021AA3E
			public override IDataSourceLocation New()
			{
				return new OleDbDataSourceLocation();
			}

			// Token: 0x0600A310 RID: 41744 RVA: 0x0021C848 File Offset: 0x0021AA48
			protected override bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
			{
				Dictionary<string, string> dictionary;
				if (OleDbConnectionStringService.Instance.TryParseConnectionString(resourcePath, out dictionary))
				{
					OleDbDataSourceLocation oleDbDataSourceLocation = new OleDbDataSourceLocation();
					Dictionary<string, object> options = oleDbDataSourceLocation.Options;
					foreach (KeyValuePair<string, string> keyValuePair in dictionary)
					{
						options[keyValuePair.Key] = keyValuePair.Value;
					}
					location = oleDbDataSourceLocation;
					return true;
				}
				location = null;
				return false;
			}
		}
	}
}
